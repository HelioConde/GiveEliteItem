using BepInEx;
using R2API;
using R2API.Utils;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using MonoMod.Cil;
using System;
using System.Reflection;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using static RoR2.MasterSpawnSlotController;

namespace GiveEliteItem
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [BepInDependency("com.KingEnderBrine.InLobbyConfig", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]
    public class GiveEliteItem : BaseUnityPlugin
    {
        public static PluginInfo PInfo { get; private set; }
        public static GiveEliteItem instance;

        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "AlchemyFlame";
        public const string PluginName = "GiveEliteItem";
        public const string PluginVersion = "1.0.0";

        private Dictionary<string, CharacterMaster> playerNameToMaster = new Dictionary<string, CharacterMaster>();
        private Dictionary<string, int> playerNameToSlot = new Dictionary<string, int>();
        public class PlayerStorage
        {
            public NetworkUser user = null;
            public CharacterMaster master = null;
            public GameObject origPrefab = null;
            public bool isDead = false;
            public Inventory inventory = null;
            public bool giftedAffix = false;
            public bool hadAncientScepter = false;
            public EquipmentIndex previousEquipment = EquipmentIndex.None;
            public NetworkUser lastDamagedBy = null;
            public float lastDamagedTime = 0;
            public string chosenEquipment = null; // Adicione esta linha
            public uint chosenEquipmentSlot = 1; // Adicione esta linha
        }


        // The actual class to use.
        public List<PlayerStorage> playerStorage = new List<PlayerStorage>();
        private List<string> respawnMethodCheck = new List<string>() { "RefightRespawn" };
        public float respawnTime; // For an added penalty per death.

        private string[] equipmentOptions = new string[]
{
            "EliteEarthEquipment",
            "EliteFireEquipment",
            "EliteHauntedEquipment",
            "EliteIceEquipment",
            "EliteLightningEquipment",
            "EliteLunarEquipment",
            "ElitePoisonEquipment"
};

        public void Awake()
        {
            PInfo = Info;
            instance = this;
            BepConfig.Init();

            Debug.Log("Carregando Mod2!");
            SetupHooks();
            AnalyzeEquip();
        }

        private void AnalyzeEquip()
        {
            On.RoR2.EquipmentSlot.PerformEquipmentAction += (orig, self, equipmentIndex) =>
            {
                CharacterBody characterBody = self.characterBody;

                if (characterBody)
                {
                    foreach (PlayerCharacterMasterController playerCharacterMaster in PlayerCharacterMasterController.instances)
                    {
                        if (playerCharacterMaster && playerCharacterMaster.master && playerCharacterMaster.master.GetBody() == characterBody)
                        {
                            // O jogador associado a este personagem ativou o equipamento.
                            NetworkUser networkUser = playerCharacterMaster.networkUser;
                            if (networkUser != null)
                            {
                                // Você pode acessar as informações do jogador aqui.
                                string playerName = networkUser.userName;
                                // Faça o que for necessário com as informações do jogador.

                                // Agora, vamos encontrar o PlayerStorage associado a este jogador.
                                PlayerStorage playerStorage = FindPlayerStorageForUser(networkUser);
                                if (playerStorage != null)
                                {
                                    // Você pode acessar o PlayerStorage e fazer o que precisa com ele.
                                    // Exemplo:
                                    EquipmentIndex chosenEquipmentIndex = EquipmentCatalog.FindEquipmentIndex(playerStorage.chosenEquipment);
                                    if (playerStorage.chosenEquipmentSlot == 1)
                                    {
                                        playerStorage.chosenEquipmentSlot = 0;
                                        uint slot = 0;
                                        uint slot2 = 1;
                                        playerCharacterMaster.master.inventory.SetEquipmentIndexForSlot(playerCharacterMaster.master.inventory.GetEquipment(slot).equipmentIndex, 1);
                                        playerCharacterMaster.master.inventory.SetEquipmentIndexForSlot(playerCharacterMaster.master.inventory.GetEquipment(slot2).equipmentIndex, 0);
                                    } else
                                    {
                                        playerStorage.chosenEquipmentSlot = 1;
                                        uint slot = 1;
                                        uint slot2 = 0;
                                        playerCharacterMaster.master.inventory.SetEquipmentIndexForSlot(playerCharacterMaster.master.inventory.GetEquipment(slot).equipmentIndex, 0);
                                        playerCharacterMaster.master.inventory.SetEquipmentIndexForSlot(playerCharacterMaster.master.inventory.GetEquipment(slot2).equipmentIndex, 1);
                                    }

                                }
                            }
                        }
                    }
                }

                return orig(self, equipmentIndex);
            };
        }

        private PlayerStorage FindPlayerStorageForUser(NetworkUser user)
        {
            foreach (PlayerStorage playerStorage in playerStorage)
            {
                if (playerStorage.user == user)
                {
                    return playerStorage;
                }
            }
            return null; // Retorna null se nenhum PlayerStorage for encontrado para o usuário.
        }
        private void SetupHooks()
        {
            On.RoR2.Run.Start += Run_Start;
            On.RoR2.Run.OnUserAdded += Run_OnUserAdded;
            On.RoR2.Run.OnUserRemoved += Run_OnUserRemoved;
        }
        private void Run_Start(On.RoR2.Run.orig_Start orig, Run self)
        {
            orig(self);
            SetupPlayers();
        }

        private void SetupPlayers(bool StageUpdate = true)
        {
            Debug.Log("Configurando jogadores...");
            if (StageUpdate) playerStorage.Clear();
            List<int> usedIndexes = new List<int>(); // Manter o controle de índices já usados.

            foreach (PlayerCharacterMasterController playerCharacterMaster in PlayerCharacterMasterController.instances)
            {
                // Skipping over Disconnected Players.
                if (playerStorage != null && playerCharacterMaster.networkUser == null)
                {
                    Debug.Log("Um jogador desconectou! Ignorando o que resta deles...");
                    continue;
                }

                // Se isso for executado no meio do estágio, basta pular os jogadores existentes e adicionar qualquer um que tenha entrado.
                if (!StageUpdate && playerStorage != null)
                {
                    // Ignorando jogadores que já estão no jogo.
                    bool flag = false;
                    foreach (PlayerStorage player in playerStorage)
                    {
                        if (player.master == playerCharacterMaster.master)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag) continue;
                }
                PlayerStorage newPlayer = new PlayerStorage();
                if (playerCharacterMaster.networkUser) newPlayer.user = playerCharacterMaster.networkUser;
                if (playerCharacterMaster.master) newPlayer.master = playerCharacterMaster.master;
                if (playerCharacterMaster.master.bodyPrefab) newPlayer.origPrefab = playerCharacterMaster.master.bodyPrefab;
                if (playerCharacterMaster.master.inventory) newPlayer.inventory = new Inventory();


                System.Random random = new System.Random();
                int randomEquipmentIndex;
                // Garanta que o jogador tenha um índice de equipamento único.
                do
                {
                    randomEquipmentIndex = random.Next(0, equipmentOptions.Length);
                } while (usedIndexes.Contains(randomEquipmentIndex));

                usedIndexes.Add(randomEquipmentIndex);

                string chosenEquipment = equipmentOptions[randomEquipmentIndex];
                newPlayer.chosenEquipment = chosenEquipment; // Atribua o equipamento escolhido ao jogador
                newPlayer.chosenEquipmentSlot = 1;

                playerStorage.Add(newPlayer);
                Debug.Log(newPlayer.user.userName + " adicionado ao PlayerStorage!");

                if (playerCharacterMaster.master.inventory)
                {
                    playerCharacterMaster.master.inventory.SetEquipmentIndexForSlot(EquipmentCatalog.FindEquipmentIndex(newPlayer.chosenEquipment), newPlayer.chosenEquipmentSlot);
                    playerCharacterMaster.master.inventory.SetEquipmentIndexForSlot(EquipmentCatalog.FindEquipmentIndex("Scanner"), 0);
                }

                Debug.Log(newPlayer.chosenEquipment + " adicionado ao Jogador!");

            }
            Debug.Log("A configuração dos jogadores foi concluída.");
        }


        private void Run_OnUserAdded(On.RoR2.Run.orig_OnUserAdded orig, Run self, NetworkUser user)
        {
            orig(self, user);
            if (Run.instance.time > 1f)
                SetupPlayers(false); // For players who enter the game late.
        }

        private void Run_OnUserRemoved(On.RoR2.Run.orig_OnUserRemoved orig, Run self, NetworkUser user)
        {
            orig(self, user);
            if (Run.instance.time > 1f)
            {
                PlayerStorage target = null;
                foreach (PlayerStorage player in playerStorage)
                {
                    if (player.user.userName == user.userName)
                    {
                        target = player;
                        Logger.LogInfo(user.userName + " left. Removing them from PlayerStorage.");
                        break;
                    }
                }
                playerStorage.Remove(target);
            }
        }

    }
}

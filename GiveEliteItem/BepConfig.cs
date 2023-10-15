using BepInEx.Configuration;
using RoR2;
using UnityEngine;
using R2API;
using System.Collections.Generic;
using static GiveEliteItem.BepConfig;
using System.Linq;
using static GiveEliteItem.EnumCollection;

namespace GiveEliteItem
{
    public class BepConfig
    {
        // start bonus
        public static ConfigEntry<int> StartingCash;
        public static ConfigEntry<ItemWhiteEnum> StartingItemWhite;
        public static ConfigEntry<int> StartingItemWhiteCount;
        public static ConfigEntry<ItemGreenEnum> StartingItemGreen;
        public static ConfigEntry<int> StartingItemGreenCount;
        public static ConfigEntry<ItemRedEnum> StartingItemRed;
        public static ConfigEntry<int> StartingItemRedCount;
        public static ConfigEntry<ItemBossEnum> StartingItemBoss;
        public static ConfigEntry<int> StartingItemBossCount;
        public static ConfigEntry<ItemLunarEnum> StartingItemLunar;
        public static ConfigEntry<int> StartingItemLunarCount;
        public static ConfigEntry<ItemVoidEnum> StartingItemVoid;
        public static ConfigEntry<int> StartingItemVoidCount;
        public static ConfigEntry<ItemEquipEnum> StartingItemEquip;

        public static void Init()
        {
            var config = GiveEliteItem.instance.Config;
            // --- Start Bonus ---
            {
                StartingCash = config.Bind("Start Bonus", "Cash", 500, new ConfigDescription("How much starting cash each player receives."));
                StartingItemWhite = config.Bind("Start Bonus", "White Item", ItemWhiteEnum.None, new ConfigDescription("Which white item each player shall receive at the start."));
                StartingItemWhiteCount = config.Bind("Start Bonus", "White Item Count", 1, new ConfigDescription("How many of the white item each player shall receive."));
                StartingItemGreen = config.Bind("Start Bonus", "Green Item", ItemGreenEnum.None, new ConfigDescription("Which green item each player shall receive at the start."));
                StartingItemGreenCount = config.Bind("Start Bonus", "Green Item Count", 1, new ConfigDescription("How many of the green item each player shall receive."));
                StartingItemRed = config.Bind("Start Bonus", "Red Item", ItemRedEnum.None, new ConfigDescription("Which red item each player shall receive at the start."));
                StartingItemRedCount = config.Bind("Start Bonus", "Red Item Count", 1, new ConfigDescription("How many of the red item each player shall receive."));
                StartingItemBoss = config.Bind("Start Bonus", "Boss Item", ItemBossEnum.None, new ConfigDescription("Which boss item each player shall receive at the start."));
                StartingItemBossCount = config.Bind("Start Bonus", "Boss Item Count", 1, new ConfigDescription("How many of the boss item each player shall receive."));
                StartingItemLunar = config.Bind("Start Bonus", "Lunar Item", ItemLunarEnum.None, new ConfigDescription("Which lunar item each player shall receive at the start."));
                StartingItemLunarCount = config.Bind("Start Bonus", "Lunar Item Count", 1, new ConfigDescription("How many of the lunar item each player shall receive."));
                StartingItemVoid = config.Bind("Start Bonus", "Void Item", ItemVoidEnum.None, new ConfigDescription("Which void item each player shall receive at the start."));
                StartingItemVoidCount = config.Bind("Start Bonus", "Void Item Count", 1, new ConfigDescription("How many of the void item each player shall receive."));
                StartingItemEquip = config.Bind("Start Bonus", "Equipment", ItemEquipEnum.None, new ConfigDescription("Which equipment each player shall receive at the start."));
            }
            if (ModCompatibilityInLobbyConfig.enabled)
            {
                ModCompatibilityInLobbyConfig.CreateFromBepInExConfigFile(config, "Start Bonus");
            }
        }
    }
}
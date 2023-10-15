using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GiveEliteItem
{
    public class EnumCollection
    {
        public enum ItemWhiteEnum
        {
            None,
            ArmorPiercingRounds,
            BackupMagazine,
            BisonSteak,
            BundleOfFireworks,
            BustlingFungus,
            CautiousSlug,
            Crowbar,
            DelicateWatch,
            EnergyDrink,
            FocusCrystal,
            Gasoline,
            ItemScrapWhite,
            LensMakersGlasses,
            Medkit,
            Mocha,
            MonsterTooth,
            OddlyshapedOpal,
            PaulsGoatHoof,
            PersonalShieldGenerator,
            PowerElixir,
            RepulsionArmorPlate,
            RollOfPennies,
            RustedKey,
            SoldiersSyringe,
            StickyBomb,
            StunGrenade,
            TopazBrooch,
            TougherTimes,
            TriTipDagger,
            Warbanner
        }
        public enum ItemGreenEnum
        {
            None,
            AtGMissileMk1,
            Bandolier,
            BerzerkersPauldron,
            Chronobauble,
            DeathMark,
            FuelCell,
            GhorsTome,
            HarvestersScythe,
            HopooFeather,
            HuntersHarpoon,
            IgnitionTank,
            Infusion,
            ItemScrapGreen,
            KjarosBand,
            LeechingSeed,
            LeptonDaisy,
            OldGuillotine,
            OldWarStealthkit,
            PredatoryInstincts,
            Razorwire,
            RedWhip,
            RegeneratingScrap,
            RoseBuckler,
            RunaldsBand,
            ShippingRequestForm,
            Shuriken,
            SquidPolyp,
            Ukulele,
            WarHorn,
            WaxQuail,
            WillOTheWisp
        }
        public enum ItemRedEnum
        {
            None,
            _57LeafClover,
            Aegis,
            AlienHead,
            BensRaincoat,
            BottledChaos,
            Brainstalks,
            BrilliantBehemoth,
            CeremonialDagger,
            DefensiveMicrobots,
            DiosBestFriend,
            FrostRelic,
            H3AD5Tv2,
            HappiestMask,
            HardlightAfterburner,
            InterstellarDeskPlant,
            ItemScrapRed,
            LaserScope,
            NkuhanasOpinion,
            PocketICBM,
            RejuvenationRack,
            ResonanceDisc,
            SentientMeatHook,
            ShatteringJustice,
            SoulboundCatalyst,
            SpareDroneParts,
            SymbioticScorpion,
            UnstableTeslaCoil,
            WakeOfVultures
        }
        public enum ItemBossEnum
        {
            None,
            ChargedPerforator,
            DefenseNucleus,
            EmpathyCores,
            GenesisLoop,
            HalcyonSeed,
            IrradiantPearl,
            ItemScrapYellow,
            LittleDisciple,
            MiredUrn,
            MoltenPerforator,
            Pearl,
            Planula,
            QueensGland,
            Shatterspleen,
            TitanicKnurl
        }
        public enum ItemVoidEnum
        {
            None,
            BenthicBloom,
            EncrustedKey,
            LostSeersLenses,
            LysateCell,
            Needletick,
            NewlyHatchedZoea,
            PlasmaShrimp,
            PluripotentLarva,
            Polylute,
            SaferSpaces,
            SingularityBand,
            Tentabauble,
            VoidsentFlame,
            WeepingFungus
        }
        public enum ItemLunarEnum
        {
            None,
            BeadsOfFealty,
            BrittleCrown,
            Corpsebloom,
            DefiantGouge,
            Egocentrism,
            EssenceOfHeresy,
            EulogyZero,
            FocusedConvergence,
            GestureOfTheDrowned,
            HooksOfHeresy,
            LightFluxPauldron,
            MercurialRachis,
            Purity,
            ShapedGlass,
            StoneFluxPauldron,
            StridesOfHeresy,
            Transcendence,
            VisionsOfHeresy
        }
        public enum ItemEquipEnum
        {
            None,
            BeyondTheLimits,
            BlastShower,
            CovenOfGold,
            DisposableMissileLauncher,
            EccentricVase,
            EffigyOfGrief,
            ElegyOfExtinction,
            ExecutiveCard,
            ForeignFruit,
            ForgiveMePlease,
            FuelArray,
            GlowingMeteorite,
            GnarledWoodsprite,
            GooboJr,
            GoragsOpus,
            HelfireTincture,
            HerBitingEmbrace,
            HisReassurance,
            IfritsDistinction,
            JadeElephant,
            JarOfSouls,
            MilkyChrysalis,
            Molotov,
            NkuhanasRetort,
            OcularHUD,
            OverloadingExcavator,
            PreonAccumulator,
            PrimordialCube,
            RadarScanner,
            ReapersRemorse,
            Recycler,
            RemoteCaffeinator,
            RoyalCapacitor,
            Sawmerang,
            SharedDesign,
            SilenceBetweenTwoStrikes,
            SpectralCirclet,
            SpinelTonic,
            SuperMassiveLeech,
            TheBackup,
            TheCrowdfunder,
            TrophyHuntersTricorn,
            VolcanicEgg
        }
        private static ItemDef ToItemDef(String itemName)
        {
            if (itemName == null)
                return null;
            var index = ItemCatalog.FindItemIndex(itemName);
            if (index == ItemIndex.None)
                return null;
            return ItemCatalog.GetItemDef(index);
        }
        public static ItemDef ToItemDef(ItemWhiteEnum value) { return ToItemDef(getItemName(value)); }
        public static ItemDef ToItemDef(ItemGreenEnum value) { return ToItemDef(getItemName(value)); }
        public static ItemDef ToItemDef(ItemRedEnum value)   { return ToItemDef(getItemName(value)); }
        public static ItemDef ToItemDef(ItemBossEnum value)  { return ToItemDef(getItemName(value)); }
        public static ItemDef ToItemDef(ItemVoidEnum value)  { return ToItemDef(getItemName(value)); }
        public static ItemDef ToItemDef(ItemLunarEnum value) { return ToItemDef(getItemName(value)); }
        public static EquipmentIndex ToEquipIndex(ItemEquipEnum value) {
            var equipName = getItemName(value);
            if (equipName == null)
                return EquipmentIndex.None;
            return EquipmentCatalog.FindEquipmentIndex(equipName);
        }
        private static string getItemName(ItemWhiteEnum item)
        {
            switch (item)
            {
                case ItemWhiteEnum.ArmorPiercingRounds: return "BossDamageBonus"; 
                case ItemWhiteEnum.BackupMagazine: return "SecondarySkillMagazine";
                case ItemWhiteEnum.BisonSteak: return "FlatHealth";
                case ItemWhiteEnum.BundleOfFireworks: return "Firework";
                case ItemWhiteEnum.BustlingFungus: return "Mushroom";
                case ItemWhiteEnum.CautiousSlug: return "HealWhileSafe";
                case ItemWhiteEnum.Crowbar: return "Crowbar";
                case ItemWhiteEnum.DelicateWatch: return "FragileDamageBonus";
                case ItemWhiteEnum.EnergyDrink: return "SprintBonus";
                case ItemWhiteEnum.FocusCrystal: return "NearbyDamageBonus";
                case ItemWhiteEnum.Gasoline: return "IgniteOnKill";
                case ItemWhiteEnum.ItemScrapWhite: return "ScrapWhite";
                case ItemWhiteEnum.LensMakersGlasses: return "CritGlasses";
                case ItemWhiteEnum.Medkit: return "Medkit";
                case ItemWhiteEnum.Mocha: return "AttackSpeedAndMoveSpeed";
                case ItemWhiteEnum.MonsterTooth: return "Tooth";
                case ItemWhiteEnum.OddlyshapedOpal: return "OutOfCombatArmor";
                case ItemWhiteEnum.PaulsGoatHoof: return "Hoof";
                case ItemWhiteEnum.PersonalShieldGenerator: return "PersonalShield";
                case ItemWhiteEnum.PowerElixir: return "HealingPotion";
                case ItemWhiteEnum.RepulsionArmorPlate: return "ArmorPlate";
                case ItemWhiteEnum.RollOfPennies: return "GoldOnHurt";
                case ItemWhiteEnum.RustedKey: return "TreasureCache";
                case ItemWhiteEnum.SoldiersSyringe: return "Syringe";
                case ItemWhiteEnum.StickyBomb: return "StickyBomb";
                case ItemWhiteEnum.StunGrenade: return "StunChanceOnHit";
                case ItemWhiteEnum.TopazBrooch: return "BarrierOnKill";
                case ItemWhiteEnum.TougherTimes: return "Bear";
                case ItemWhiteEnum.TriTipDagger: return "BleedOnHit";
                case ItemWhiteEnum.Warbanner: return "WardOnLevel";
            };
            return null;
        }
        private static string getItemName(ItemGreenEnum item)
        {
            switch (item)
            {
                case ItemGreenEnum.AtGMissileMk1: return "Missile";
                case ItemGreenEnum.Bandolier: return "Bandolier";
                case ItemGreenEnum.BerzerkersPauldron: return "WarCryOnMultiKill";
                case ItemGreenEnum.Chronobauble: return "SlowOnHit";
                case ItemGreenEnum.DeathMark: return "DeathMark";
                case ItemGreenEnum.FuelCell: return "EquipmentMagazine";
                case ItemGreenEnum.GhorsTome: return "BonusGoldPackOnKill";
                case ItemGreenEnum.HarvestersScythe: return "HealOnCrit";
                case ItemGreenEnum.HopooFeather: return "Feather";
                case ItemGreenEnum.HuntersHarpoon: return "MoveSpeedOnKill";
                case ItemGreenEnum.IgnitionTank: return "StrengthenBurn";
                case ItemGreenEnum.Infusion: return "Infusion";
                case ItemGreenEnum.ItemScrapGreen: return "ScrapGreen";
                case ItemGreenEnum.KjarosBand: return "FireRing";
                case ItemGreenEnum.LeechingSeed: return "Seed";
                case ItemGreenEnum.LeptonDaisy: return "TPHealingNova";
                case ItemGreenEnum.OldGuillotine: return "ExecuteLowHealthElite";
                case ItemGreenEnum.OldWarStealthkit: return "Phasing";
                case ItemGreenEnum.PredatoryInstincts: return "AttackSpeedOnCrit";
                case ItemGreenEnum.Razorwire: return "Thorns";
                case ItemGreenEnum.RedWhip: return "SprintOutOfCombat";
                case ItemGreenEnum.RegeneratingScrap: return "RegeneratingScrap";
                case ItemGreenEnum.RoseBuckler: return "SprintArmor";
                case ItemGreenEnum.RunaldsBand: return "IceRing";
                case ItemGreenEnum.ShippingRequestForm: return "FreeChest";
                case ItemGreenEnum.Shuriken: return "PrimarySkillShuriken";
                case ItemGreenEnum.SquidPolyp: return "Squid";
                case ItemGreenEnum.Ukulele: return "ChainLightning";
                case ItemGreenEnum.WarHorn: return "EnergizedOnEquipmentUse";
                case ItemGreenEnum.WaxQuail: return "JumpBoost";
                case ItemGreenEnum.WillOTheWisp: return "ExplodeOnDeath";
            }
            return null;
        }
        private static string getItemName(ItemRedEnum item)
        {
            switch (item)
            {
                case ItemRedEnum._57LeafClover: return "Clover";
                case ItemRedEnum.Aegis: return "BarrierOnOverHeal";
                case ItemRedEnum.AlienHead: return "AlienHead";
                case ItemRedEnum.BensRaincoat: return "ImmuneToDebuff";
                case ItemRedEnum.BottledChaos: return "RandomEquipmentTrigger";
                case ItemRedEnum.Brainstalks: return "KillEliteFrenzy";
                case ItemRedEnum.BrilliantBehemoth: return "Behemoth";
                case ItemRedEnum.CeremonialDagger: return "Dagger";
                case ItemRedEnum.DefensiveMicrobots: return "CaptainDefenseMatrix";
                case ItemRedEnum.DiosBestFriend: return "ExtraLife";
                case ItemRedEnum.FrostRelic: return "Icicle";
                case ItemRedEnum.H3AD5Tv2: return "FallBoots";
                case ItemRedEnum.HappiestMask: return "GhostOnKill";
                case ItemRedEnum.HardlightAfterburner: return "UtilitySkillMagazine";
                case ItemRedEnum.InterstellarDeskPlant: return "Plant";
                case ItemRedEnum.ItemScrapRed: return "ScrapRed";
                case ItemRedEnum.LaserScope: return "CritDamage";
                case ItemRedEnum.NkuhanasOpinion: return "NovaOnHeal";
                case ItemRedEnum.PocketICBM: return "MoreMissile";
                case ItemRedEnum.RejuvenationRack: return "IncreaseHealing";
                case ItemRedEnum.ResonanceDisc: return "LaserTurbine";
                case ItemRedEnum.SentientMeatHook: return "BounceNearby";
                case ItemRedEnum.ShatteringJustice: return "ArmorReductionOnHit";
                case ItemRedEnum.SoulboundCatalyst: return "Talisman";
                case ItemRedEnum.SpareDroneParts: return "DroneWeapons";
                case ItemRedEnum.SymbioticScorpion: return "PermanentDebuffOnHit";
                case ItemRedEnum.UnstableTeslaCoil: return "ShockNearby";
                case ItemRedEnum.WakeOfVultures: return "HeadHunter";
            }
            return null;
        }
        private static string getItemName(ItemBossEnum item)
        {
            switch (item)
            {
                case ItemBossEnum.ChargedPerforator: return "LightningStrikeOnHit";
                case ItemBossEnum.DefenseNucleus: return "MinorConstructOnKill";
                case ItemBossEnum.EmpathyCores: return "RoboBallBuddy";
                case ItemBossEnum.GenesisLoop: return "NovaOnLowHealth";
                case ItemBossEnum.HalcyonSeed: return "TitanGoldDuringTP";
                case ItemBossEnum.IrradiantPearl: return "ShinyPearl";
                case ItemBossEnum.ItemScrapYellow: return "ScrapYellow";
                case ItemBossEnum.LittleDisciple: return "SprintWisp";
                case ItemBossEnum.MiredUrn: return "SiphonOnLowHealth";
                case ItemBossEnum.MoltenPerforator: return "FireballsOnHit";
                case ItemBossEnum.Pearl: return "Pearl";
                case ItemBossEnum.Planula: return "ParentEgg";
                case ItemBossEnum.QueensGland: return "BeetleGland";
                case ItemBossEnum.Shatterspleen: return "BleedOnHitAndExplode";
                case ItemBossEnum.TitanicKnurl: return "Knurl";
            }
            return null;
        }
        private static string getItemName(ItemVoidEnum item)
        {
            switch (item)
            {
                case ItemVoidEnum.BenthicBloom: return "CloverVoid";
                case ItemVoidEnum.EncrustedKey: return "TreasureCacheVoid";
                case ItemVoidEnum.LostSeersLenses: return "CritGlassesVoid";
                case ItemVoidEnum.LysateCell: return "EquipmentMagazineVoid";
                case ItemVoidEnum.Needletick: return "BleedOnHitVoid";
                case ItemVoidEnum.NewlyHatchedZoea: return "VoidMegaCrabItem";
                case ItemVoidEnum.PlasmaShrimp: return "MissileVoid";
                case ItemVoidEnum.PluripotentLarva: return "ExtraLifeVoid";
                case ItemVoidEnum.Polylute: return "ChainLightningVoid";
                case ItemVoidEnum.SaferSpaces: return "BearVoid";
                case ItemVoidEnum.SingularityBand: return "ElementalRingVoid";
                case ItemVoidEnum.Tentabauble: return "SlowOnHitVoid";
                case ItemVoidEnum.VoidsentFlame: return "ExplodeOnDeathVoid";
                case ItemVoidEnum.WeepingFungus: return "MushroomVoid";
            }
            return null;
        }
        private static string getItemName(ItemLunarEnum item)
        {
            switch (item)
            {
                case ItemLunarEnum.BeadsOfFealty: return "LunarTrinket";
                case ItemLunarEnum.BrittleCrown: return "GoldOnHit";
                case ItemLunarEnum.Corpsebloom: return "RepeatHeal";
                case ItemLunarEnum.DefiantGouge: return "MonstersOnShrineUse";
                case ItemLunarEnum.Egocentrism: return "LunarSun";
                case ItemLunarEnum.EssenceOfHeresy: return "LunarSpecialReplacement";
                case ItemLunarEnum.EulogyZero: return "RandomlyLunar";
                case ItemLunarEnum.FocusedConvergence: return "FocusConvergence";
                case ItemLunarEnum.GestureOfTheDrowned: return "AutoCastEquipment";
                case ItemLunarEnum.HooksOfHeresy: return "LunarSecondaryReplacement";
                case ItemLunarEnum.LightFluxPauldron: return "HalfAttackSpeedHalfCooldowns";
                case ItemLunarEnum.MercurialRachis: return "RandomDamageZone";
                case ItemLunarEnum.Purity: return "LunarBadLuck";
                case ItemLunarEnum.ShapedGlass: return "LunarDagger";
                case ItemLunarEnum.StoneFluxPauldron: return "HalfSpeedDoubleHealth";
                case ItemLunarEnum.StridesOfHeresy: return "LunarUtilityReplacement";
                case ItemLunarEnum.Transcendence: return "ShieldOnly";
                case ItemLunarEnum.VisionsOfHeresy: return "LunarPrimaryReplacement";
            }
            return null;
        }
        private static string getItemName(ItemEquipEnum item)
        {
            switch (item)
            {
                case ItemEquipEnum.BeyondTheLimits: return "EliteSecretSpeedEquipment";
                case ItemEquipEnum.BlastShower: return "Cleanse";
                case ItemEquipEnum.CovenOfGold: return "EliteGoldEquipment";
                case ItemEquipEnum.DisposableMissileLauncher: return "CommandMissile";
                case ItemEquipEnum.EccentricVase: return "Gateway";
                case ItemEquipEnum.EffigyOfGrief: return "CrippleWard";
                case ItemEquipEnum.ElegyOfExtinction: return "LunarPortalOnUse";
                case ItemEquipEnum.ExecutiveCard: return "MultiShopCard";
                case ItemEquipEnum.ForeignFruit: return "Fruit";
                case ItemEquipEnum.ForgiveMePlease: return "DeathProjectile";
                case ItemEquipEnum.FuelArray: return "QuestVolatileBattery";
                case ItemEquipEnum.GlowingMeteorite: return "Meteor";
                case ItemEquipEnum.GnarledWoodsprite: return "PassiveHealing";
                case ItemEquipEnum.GooboJr: return "GummyClone";
                case ItemEquipEnum.GoragsOpus: return "TeamWarCry";
                case ItemEquipEnum.HelfireTincture: return "BurnNearby";
                case ItemEquipEnum.HerBitingEmbrace: return "EliteIceEquipment";
                case ItemEquipEnum.HisReassurance: return "EliteEarthEquipment";
                case ItemEquipEnum.IfritsDistinction: return "EliteFireEquipment";
                case ItemEquipEnum.JadeElephant: return "GainArmor";
                case ItemEquipEnum.JarOfSouls: return "SoulJar";
                case ItemEquipEnum.MilkyChrysalis: return "Jetpack";
                case ItemEquipEnum.Molotov: return "Molotov";
                case ItemEquipEnum.NkuhanasRetort: return "ElitePoisonEquipment";
                case ItemEquipEnum.OcularHUD: return "CritOnUse";
                case ItemEquipEnum.OverloadingExcavator: return "IrradiatingLaser";
                case ItemEquipEnum.PreonAccumulator: return "BFG";
                case ItemEquipEnum.PrimordialCube: return "Blackhole";
                case ItemEquipEnum.RadarScanner: return "Scanner";
                case ItemEquipEnum.ReapersRemorse: return "GhostGun";
                case ItemEquipEnum.Recycler: return "Recycle";
                case ItemEquipEnum.RemoteCaffeinator: return "VendingMachine";
                case ItemEquipEnum.RoyalCapacitor: return "Lightning";
                case ItemEquipEnum.Sawmerang: return "Saw";
                case ItemEquipEnum.SharedDesign: return "EliteLunarEquipment";
                case ItemEquipEnum.SilenceBetweenTwoStrikes: return "EliteLightningEquipment";
                case ItemEquipEnum.SpectralCirclet: return "EliteHauntedEquipment";
                case ItemEquipEnum.SpinelTonic: return "Tonic";
                case ItemEquipEnum.SuperMassiveLeech: return "LifestealOnHit";
                case ItemEquipEnum.TheBackup: return "DroneBackup";
                case ItemEquipEnum.TheCrowdfunder: return "GoldGat";
                case ItemEquipEnum.TrophyHuntersTricorn: return "BossHunter";
                case ItemEquipEnum.VolcanicEgg: return "FireBallDash";
            }
            return null;
        }
    }
}

using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.SQL;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class HotfixHandler
    {
        public static void AchievementHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            AchievementHotfix1200 hotfix = new AchievementHotfix1200();

            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.Title = packet.ReadCString("Title", indexes);
            hotfix.Reward = packet.ReadCString("Reward", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.InstanceID = packet.ReadInt16("InstanceID", indexes);
            hotfix.Faction = packet.ReadSByte("Faction", indexes);
            hotfix.Supercedes = packet.ReadInt32("Supercedes", indexes);
            hotfix.Category = packet.ReadInt16("Category", indexes);
            hotfix.MinimumCriteria = packet.ReadSByte("MinimumCriteria", indexes);
            hotfix.Points = packet.ReadSByte("Points", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.UiOrder = packet.ReadUInt16("UiOrder", indexes);
            hotfix.IconFileID = packet.ReadInt32("IconFileID", indexes);
            hotfix.RewardItemID = packet.ReadInt32("RewardItemID", indexes);
            hotfix.CriteriaTree = packet.ReadUInt32("CriteriaTree", indexes);
            hotfix.SharesCriteria = packet.ReadInt16("SharesCriteria", indexes);
            hotfix.CovenantID = packet.ReadInt32("CovenantID", indexes);
            hotfix.HiddenBeforeDisplaySeason = packet.ReadInt32("HiddenBeforeDisplaySeason", indexes);
            hotfix.LegacyAfterTimeEvent = packet.ReadInt32("LegacyAfterTimeEvent", indexes);

            Storage.AchievementHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AchievementLocaleHotfix1200 hotfixLocale = new AchievementLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                    TitleLang = hotfix.Title,
                    RewardLang = hotfix.Reward,
                };
                Storage.AchievementHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void AchievementCategoryHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            AchievementCategoryHotfix1200 hotfix = new AchievementCategoryHotfix1200();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Parent = packet.ReadInt16("Parent", indexes);
            hotfix.UiOrder = packet.ReadByte("UiOrder", indexes);

            Storage.AchievementCategoryHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AchievementCategoryLocaleHotfix1200 hotfixLocale = new AchievementCategoryLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.AchievementCategoryHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void AdventureJournalHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            AdventureJournalHotfix1200 hotfix = new AdventureJournalHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ButtonText = packet.ReadCString("ButtonText", indexes);
            hotfix.RewardDescription = packet.ReadCString("RewardDescription", indexes);
            hotfix.ContinueDescription = packet.ReadCString("ContinueDescription", indexes);
            hotfix.Type = packet.ReadByte("Type", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ButtonActionType = packet.ReadByte("ButtonActionType", indexes);
            hotfix.TextureFileDataID = packet.ReadInt32("TextureFileDataID", indexes);
            hotfix.LfgDungeonID = packet.ReadUInt16("LfgDungeonID", indexes);
            hotfix.QuestID = packet.ReadInt32("QuestID", indexes);
            hotfix.BattleMasterListID = packet.ReadUInt16("BattleMasterListID", indexes);
            hotfix.PriorityMin = packet.ReadByte("PriorityMin", indexes);
            hotfix.PriorityMax = packet.ReadByte("PriorityMax", indexes);
            hotfix.CurrencyType = packet.ReadUInt16("CurrencyType", indexes);
            hotfix.CurrencyQuantity = packet.ReadUInt32("CurrencyQuantity", indexes);
            hotfix.UiMapID = packet.ReadUInt16("UiMapID", indexes);
            hotfix.BonusPlayerConditionID = new uint?[2];
            for (int i = 0; i < 2; i++)
                hotfix.BonusPlayerConditionID[i] = packet.ReadUInt32("BonusPlayerConditionID", indexes, i);
            hotfix.BonusValue = new byte?[2];
            for (int i = 0; i < 2; i++)
                hotfix.BonusValue[i] = packet.ReadByte("BonusValue", indexes, i);

            Storage.AdventureJournalHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AdventureJournalLocaleHotfix1200 hotfixLocale = new AdventureJournalLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                    ButtonTextLang = hotfix.ButtonText,
                    RewardDescriptionLang = hotfix.RewardDescription,
                    ContinueDescriptionLang = hotfix.ContinueDescription,
                };
                Storage.AdventureJournalHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void AdventureMapPoiHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            AdventureMapPoiHotfix1200 hotfix = new AdventureMapPoiHotfix1200();

            hotfix.ID = entry;
            hotfix.Title = packet.ReadCString("Title", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.WorldPositionX = packet.ReadSingle("WorldPositionX", indexes);
            hotfix.WorldPositionY = packet.ReadSingle("WorldPositionY", indexes);
            hotfix.Type = packet.ReadSByte("Type", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.QuestID = packet.ReadUInt32("QuestID", indexes);
            hotfix.LfgDungeonID = packet.ReadUInt32("LfgDungeonID", indexes);
            hotfix.RewardItemID = packet.ReadInt32("RewardItemID", indexes);
            hotfix.UiTextureAtlasMemberID = packet.ReadUInt32("UiTextureAtlasMemberID", indexes);
            hotfix.UiTextureKitID = packet.ReadUInt32("UiTextureKitID", indexes);
            hotfix.MapID = packet.ReadInt32("MapID", indexes);
            hotfix.AreaTableID = packet.ReadUInt32("AreaTableID", indexes);

            Storage.AdventureMapPoiHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AdventureMapPoiLocaleHotfix1200 hotfixLocale = new AdventureMapPoiLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    TitleLang = hotfix.Title,
                    DescriptionLang = hotfix.Description,
                };
                Storage.AdventureMapPoiHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void AnimationDataHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            AnimationDataHotfix1200 hotfix = new AnimationDataHotfix1200();

            hotfix.ID = entry;
            hotfix.Fallback = packet.ReadUInt16("Fallback", indexes);
            hotfix.BehaviorTier = packet.ReadSByte("BehaviorTier", indexes);
            hotfix.BehaviorID = packet.ReadInt16("BehaviorID", indexes);
            hotfix.Flags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.AnimationDataHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void AnimKitHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            AnimKitHotfix1200 hotfix = new AnimKitHotfix1200();

            hotfix.ID = entry;
            hotfix.OneShotDuration = packet.ReadUInt32("OneShotDuration", indexes);
            hotfix.OneShotStopAnimKitID = packet.ReadUInt16("OneShotStopAnimKitID", indexes);
            hotfix.LowDefAnimKitID = packet.ReadUInt16("LowDefAnimKitID", indexes);

            Storage.AnimKitHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void AreaGroupMemberHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            AreaGroupMemberHotfix1200 hotfix = new AreaGroupMemberHotfix1200();

            hotfix.ID = entry;
            hotfix.AreaID = packet.ReadUInt16("AreaID", indexes);
            hotfix.AreaGroupID = packet.ReadUInt32("AreaGroupID", indexes);

            Storage.AreaGroupMemberHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void AreaTableHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            AreaTableHotfix1200 hotfix = new AreaTableHotfix1200();

            hotfix.ID = entry;
            hotfix.ZoneName = packet.ReadCString("ZoneName", indexes);
            hotfix.AreaName = packet.ReadCString("AreaName", indexes);
            hotfix.ContinentID = packet.ReadUInt16("ContinentID", indexes);
            hotfix.ParentAreaID = packet.ReadUInt16("ParentAreaID", indexes);
            hotfix.AreaBit = packet.ReadInt16("AreaBit", indexes);
            hotfix.SoundProviderPref = packet.ReadByte("SoundProviderPref", indexes);
            hotfix.SoundProviderPrefUnderwater = packet.ReadByte("SoundProviderPrefUnderwater", indexes);
            hotfix.AmbienceID = packet.ReadUInt16("AmbienceID", indexes);
            hotfix.UwAmbience = packet.ReadUInt16("UwAmbience", indexes);
            hotfix.ZoneMusic = packet.ReadUInt16("ZoneMusic", indexes);
            hotfix.UwZoneMusic = packet.ReadUInt16("UwZoneMusic", indexes);
            hotfix.IntroSound = packet.ReadUInt16("IntroSound", indexes);
            hotfix.UwIntroSound = packet.ReadUInt32("UwIntroSound", indexes);
            hotfix.FactionGroupMask = packet.ReadByte("FactionGroupMask", indexes);
            hotfix.AmbientMultiplier = packet.ReadSingle("AmbientMultiplier", indexes);
            hotfix.MountFlags = packet.ReadInt32("MountFlags", indexes);
            hotfix.PvpCombatWorldStateID = packet.ReadInt32("PvpCombatWorldStateID", indexes);
            hotfix.WildBattlePetLevelMin = packet.ReadByte("WildBattlePetLevelMin", indexes);
            hotfix.WildBattlePetLevelMax = packet.ReadByte("WildBattlePetLevelMax", indexes);
            hotfix.WindSettingsID = packet.ReadByte("WindSettingsID", indexes);
            hotfix.ContentTuningID = packet.ReadInt32("ContentTuningID", indexes);
            hotfix.Flags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);
            hotfix.LiquidTypeID = new ushort?[4];
            for (int i = 0; i < 4; i++)
                hotfix.LiquidTypeID[i] = packet.ReadUInt16("LiquidTypeID", indexes, i);

            Storage.AreaTableHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AreaTableLocaleHotfix1200 hotfixLocale = new AreaTableLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    AreaNameLang = hotfix.AreaName,
                };
                Storage.AreaTableHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void AreaTriggerHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            AreaTriggerHotfix1200 hotfix = new AreaTriggerHotfix1200();

            hotfix.PosX = packet.ReadSingle("PosX", indexes);
            hotfix.PosY = packet.ReadSingle("PosY", indexes);
            hotfix.PosZ = packet.ReadSingle("PosZ", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ContinentID = packet.ReadUInt16("ContinentID", indexes);
            hotfix.PhaseUseFlags = packet.ReadInt32("PhaseUseFlags", indexes);
            hotfix.PhaseID = packet.ReadInt16("PhaseID", indexes);
            hotfix.PhaseGroupID = packet.ReadInt16("PhaseGroupID", indexes);
            hotfix.Radius = packet.ReadSingle("Radius", indexes);
            hotfix.BoxLength = packet.ReadSingle("BoxLength", indexes);
            hotfix.BoxWidth = packet.ReadSingle("BoxWidth", indexes);
            hotfix.BoxHeight = packet.ReadSingle("BoxHeight", indexes);
            hotfix.BoxYaw = packet.ReadSingle("BoxYaw", indexes);
            hotfix.ShapeType = packet.ReadSByte("ShapeType", indexes);
            hotfix.ShapeID = packet.ReadInt16("ShapeID", indexes);
            hotfix.AreaTriggerActionSetID = packet.ReadInt32("AreaTriggerActionSetID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.AreaTriggerHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void AreaTriggerActionSetHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            AreaTriggerActionSetHotfix1200 hotfix = new AreaTriggerActionSetHotfix1200();

            hotfix.ID = entry;
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.AreaTriggerActionSetHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ArmorLocationHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ArmorLocationHotfix1200 hotfix = new ArmorLocationHotfix1200();

            hotfix.ID = entry;
            hotfix.Clothmodifier = packet.ReadSingle("Clothmodifier", indexes);
            hotfix.Leathermodifier = packet.ReadSingle("Leathermodifier", indexes);
            hotfix.Chainmodifier = packet.ReadSingle("Chainmodifier", indexes);
            hotfix.Platemodifier = packet.ReadSingle("Platemodifier", indexes);
            hotfix.Modifier = packet.ReadSingle("Modifier", indexes);

            Storage.ArmorLocationHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ArtifactHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ArtifactHotfix1200 hotfix = new ArtifactHotfix1200();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.UiTextureKitID = packet.ReadUInt16("UiTextureKitID", indexes);
            hotfix.UiNameColor = packet.ReadInt32("UiNameColor", indexes);
            hotfix.UiBarOverlayColor = packet.ReadInt32("UiBarOverlayColor", indexes);
            hotfix.UiBarBackgroundColor = packet.ReadInt32("UiBarBackgroundColor", indexes);
            hotfix.ChrSpecializationID = packet.ReadUInt16("ChrSpecializationID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ArtifactCategoryID = packet.ReadByte("ArtifactCategoryID", indexes);
            hotfix.UiModelSceneID = packet.ReadUInt32("UiModelSceneID", indexes);
            hotfix.SpellVisualKitID = packet.ReadUInt32("SpellVisualKitID", indexes);

            Storage.ArtifactHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ArtifactLocaleHotfix1200 hotfixLocale = new ArtifactLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ArtifactHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ArtifactAppearanceHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ArtifactAppearanceHotfix1200 hotfix = new ArtifactAppearanceHotfix1200();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ArtifactAppearanceSetID = packet.ReadUInt16("ArtifactAppearanceSetID", indexes);
            hotfix.DisplayIndex = packet.ReadByte("DisplayIndex", indexes);
            hotfix.UnlockPlayerConditionID = packet.ReadUInt32("UnlockPlayerConditionID", indexes);
            hotfix.ItemAppearanceModifierID = packet.ReadByte("ItemAppearanceModifierID", indexes);
            hotfix.UiSwatchColor = packet.ReadInt32("UiSwatchColor", indexes);
            hotfix.UiModelSaturation = packet.ReadSingle("UiModelSaturation", indexes);
            hotfix.UiModelOpacity = packet.ReadSingle("UiModelOpacity", indexes);
            hotfix.OverrideShapeshiftFormID = packet.ReadByte("OverrideShapeshiftFormID", indexes);
            hotfix.OverrideShapeshiftDisplayID = packet.ReadUInt32("OverrideShapeshiftDisplayID", indexes);
            hotfix.UiItemAppearanceID = packet.ReadUInt32("UiItemAppearanceID", indexes);
            hotfix.UiAltItemAppearanceID = packet.ReadUInt32("UiAltItemAppearanceID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.UiCameraID = packet.ReadUInt16("UiCameraID", indexes);
            hotfix.UsablePlayerConditionID = packet.ReadUInt32("UsablePlayerConditionID", indexes);

            Storage.ArtifactAppearanceHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ArtifactAppearanceLocaleHotfix1200 hotfixLocale = new ArtifactAppearanceLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ArtifactAppearanceHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ArtifactAppearanceSetHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ArtifactAppearanceSetHotfix1200 hotfix = new ArtifactAppearanceSetHotfix1200();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.DisplayIndex = packet.ReadByte("DisplayIndex", indexes);
            hotfix.UiCameraID = packet.ReadUInt16("UiCameraID", indexes);
            hotfix.AltHandUICameraID = packet.ReadUInt16("AltHandUICameraID", indexes);
            hotfix.ForgeAttachmentOverride = packet.ReadSByte("ForgeAttachmentOverride", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ArtifactID = packet.ReadUInt32("ArtifactID", indexes);

            Storage.ArtifactAppearanceSetHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ArtifactAppearanceSetLocaleHotfix1200 hotfixLocale = new ArtifactAppearanceSetLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.ArtifactAppearanceSetHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ArtifactCategoryHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ArtifactCategoryHotfix1200 hotfix = new ArtifactCategoryHotfix1200();

            hotfix.ID = entry;
            hotfix.XpMultCurrencyID = packet.ReadInt16("XpMultCurrencyID", indexes);
            hotfix.XpMultCurveID = packet.ReadInt16("XpMultCurveID", indexes);

            Storage.ArtifactCategoryHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ArtifactPowerHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ArtifactPowerHotfix1200 hotfix = new ArtifactPowerHotfix1200();

            hotfix.DisplayPosX = packet.ReadSingle("DisplayPosX", indexes);
            hotfix.DisplayPosY = packet.ReadSingle("DisplayPosY", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ArtifactID = packet.ReadByte("ArtifactID", indexes);
            hotfix.MaxPurchasableRank = packet.ReadByte("MaxPurchasableRank", indexes);
            hotfix.Label = packet.ReadInt32("Label", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.Tier = packet.ReadByte("Tier", indexes);

            Storage.ArtifactPowerHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ArtifactPowerLinkHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ArtifactPowerLinkHotfix1200 hotfix = new ArtifactPowerLinkHotfix1200();

            hotfix.ID = entry;
            hotfix.PowerA = packet.ReadUInt16("PowerA", indexes);
            hotfix.PowerB = packet.ReadUInt16("PowerB", indexes);

            Storage.ArtifactPowerLinkHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ArtifactPowerPickerHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ArtifactPowerPickerHotfix1200 hotfix = new ArtifactPowerPickerHotfix1200();

            hotfix.ID = entry;
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);

            Storage.ArtifactPowerPickerHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ArtifactPowerRankHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ArtifactPowerRankHotfix1200 hotfix = new ArtifactPowerRankHotfix1200();

            hotfix.ID = entry;
            hotfix.RankIndex = packet.ReadByte("RankIndex", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.ItemBonusListID = packet.ReadUInt16("ItemBonusListID", indexes);
            hotfix.AuraPointsOverride = packet.ReadSingle("AuraPointsOverride", indexes);
            hotfix.ArtifactPowerID = packet.ReadUInt32("ArtifactPowerID", indexes);

            Storage.ArtifactPowerRankHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ArtifactQuestXpHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ArtifactQuestXpHotfix1200 hotfix = new ArtifactQuestXpHotfix1200();

            hotfix.ID = entry;
            hotfix.Difficulty = new uint?[10];
            for (int i = 0; i < 10; i++)
                hotfix.Difficulty[i] = packet.ReadUInt32("Difficulty", indexes, i);

            Storage.ArtifactQuestXpHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ArtifactTierHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ArtifactTierHotfix1200 hotfix = new ArtifactTierHotfix1200();

            hotfix.ID = entry;
            hotfix.ArtifactTier = packet.ReadUInt32("ArtifactTier", indexes);
            hotfix.MaxNumTraits = packet.ReadUInt32("MaxNumTraits", indexes);
            hotfix.MaxArtifactKnowledge = packet.ReadUInt32("MaxArtifactKnowledge", indexes);
            hotfix.KnowledgePlayerCondition = packet.ReadUInt32("KnowledgePlayerCondition", indexes);
            hotfix.MinimumEmpowerKnowledge = packet.ReadUInt32("MinimumEmpowerKnowledge", indexes);

            Storage.ArtifactTierHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ArtifactUnlockHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ArtifactUnlockHotfix1200 hotfix = new ArtifactUnlockHotfix1200();

            hotfix.ID = entry;
            hotfix.PowerID = packet.ReadUInt32("PowerID", indexes);
            hotfix.PowerRank = packet.ReadByte("PowerRank", indexes);
            hotfix.ItemBonusListID = packet.ReadUInt16("ItemBonusListID", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.ArtifactID = packet.ReadUInt32("ArtifactID", indexes);

            Storage.ArtifactUnlockHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void AuctionHouseHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            AuctionHouseHotfix1200 hotfix = new AuctionHouseHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.FactionID = packet.ReadUInt16("FactionID", indexes);
            hotfix.DepositRate = packet.ReadByte("DepositRate", indexes);
            hotfix.ConsignmentRate = packet.ReadByte("ConsignmentRate", indexes);

            Storage.AuctionHouseHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AuctionHouseLocaleHotfix1200 hotfixLocale = new AuctionHouseLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.AuctionHouseHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void AzeriteEmpoweredItemHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            AzeriteEmpoweredItemHotfix1200 hotfix = new AzeriteEmpoweredItemHotfix1200();

            hotfix.ID = entry;
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);
            hotfix.AzeriteTierUnlockSetID = packet.ReadUInt32("AzeriteTierUnlockSetID", indexes);
            hotfix.AzeritePowerSetID = packet.ReadUInt32("AzeritePowerSetID", indexes);

            Storage.AzeriteEmpoweredItemHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void AzeriteEssenceHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            AzeriteEssenceHotfix1200 hotfix = new AzeriteEssenceHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.SpecSetID = packet.ReadInt32("SpecSetID", indexes);

            Storage.AzeriteEssenceHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AzeriteEssenceLocaleHotfix1200 hotfixLocale = new AzeriteEssenceLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.AzeriteEssenceHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void AzeriteEssencePowerHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            AzeriteEssencePowerHotfix1200 hotfix = new AzeriteEssencePowerHotfix1200();

            hotfix.ID = entry;
            hotfix.SourceAlliance = packet.ReadCString("SourceAlliance", indexes);
            hotfix.SourceHorde = packet.ReadCString("SourceHorde", indexes);
            hotfix.AzeriteEssenceID = packet.ReadInt32("AzeriteEssenceID", indexes);
            hotfix.Tier = packet.ReadByte("Tier", indexes);
            hotfix.MajorPowerDescription = packet.ReadInt32("MajorPowerDescription", indexes);
            hotfix.MinorPowerDescription = packet.ReadInt32("MinorPowerDescription", indexes);
            hotfix.MajorPowerActual = packet.ReadInt32("MajorPowerActual", indexes);
            hotfix.MinorPowerActual = packet.ReadInt32("MinorPowerActual", indexes);

            Storage.AzeriteEssencePowerHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AzeriteEssencePowerLocaleHotfix1200 hotfixLocale = new AzeriteEssencePowerLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    SourceAllianceLang = hotfix.SourceAlliance,
                    SourceHordeLang = hotfix.SourceHorde,
                };
                Storage.AzeriteEssencePowerHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void AzeriteItemHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            AzeriteItemHotfix1200 hotfix = new AzeriteItemHotfix1200();

            hotfix.ID = entry;
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);

            Storage.AzeriteItemHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void AzeriteItemMilestonePowerHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            AzeriteItemMilestonePowerHotfix1200 hotfix = new AzeriteItemMilestonePowerHotfix1200();

            hotfix.ID = entry;
            hotfix.RequiredLevel = packet.ReadInt32("RequiredLevel", indexes);
            hotfix.AzeritePowerID = packet.ReadInt32("AzeritePowerID", indexes);
            hotfix.Type = packet.ReadInt32("Type", indexes);
            hotfix.AutoUnlock = packet.ReadInt32("AutoUnlock", indexes);

            Storage.AzeriteItemMilestonePowerHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void AzeriteKnowledgeMultiplierHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            AzeriteKnowledgeMultiplierHotfix1200 hotfix = new AzeriteKnowledgeMultiplierHotfix1200();

            hotfix.ID = entry;
            hotfix.Multiplier = packet.ReadSingle("Multiplier", indexes);

            Storage.AzeriteKnowledgeMultiplierHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void AzeriteLevelInfoHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            AzeriteLevelInfoHotfix1200 hotfix = new AzeriteLevelInfoHotfix1200();

            hotfix.ID = entry;
            hotfix.BaseExperienceToNextLevel = packet.ReadUInt64("BaseExperienceToNextLevel", indexes);
            hotfix.MinimumExperienceToNextLevel = packet.ReadUInt64("MinimumExperienceToNextLevel", indexes);
            hotfix.ItemLevel = packet.ReadInt32("ItemLevel", indexes);

            Storage.AzeriteLevelInfoHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void AzeritePowerHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            AzeritePowerHotfix1200 hotfix = new AzeritePowerHotfix1200();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.ItemBonusListID = packet.ReadInt32("ItemBonusListID", indexes);
            hotfix.SpecSetID = packet.ReadInt32("SpecSetID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.AzeritePowerHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void AzeritePowerSetMemberHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            AzeritePowerSetMemberHotfix1200 hotfix = new AzeritePowerSetMemberHotfix1200();

            hotfix.ID = entry;
            hotfix.AzeritePowerSetID = packet.ReadUInt32("AzeritePowerSetID", indexes);
            hotfix.AzeritePowerID = packet.ReadInt32("AzeritePowerID", indexes);
            hotfix.Class = packet.ReadSByte("Class", indexes);
            hotfix.Tier = packet.ReadByte("Tier", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);

            Storage.AzeritePowerSetMemberHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void AzeriteTierUnlockHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            AzeriteTierUnlockHotfix1200 hotfix = new AzeriteTierUnlockHotfix1200();

            hotfix.ID = entry;
            hotfix.ItemCreationContext = packet.ReadByte("ItemCreationContext", indexes);
            hotfix.Tier = packet.ReadByte("Tier", indexes);
            hotfix.AzeriteLevel = packet.ReadByte("AzeriteLevel", indexes);
            hotfix.AzeriteTierUnlockSetID = packet.ReadUInt32("AzeriteTierUnlockSetID", indexes);

            Storage.AzeriteTierUnlockHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void AzeriteTierUnlockSetHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            AzeriteTierUnlockSetHotfix1200 hotfix = new AzeriteTierUnlockSetHotfix1200();

            hotfix.ID = entry;
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.AzeriteTierUnlockSetHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void AzeriteUnlockMappingHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            AzeriteUnlockMappingHotfix1200 hotfix = new AzeriteUnlockMappingHotfix1200();

            hotfix.ID = entry;
            hotfix.ItemLevel = packet.ReadInt32("ItemLevel", indexes);
            hotfix.ItemBonusListHead = packet.ReadInt32("ItemBonusListHead", indexes);
            hotfix.ItemBonusListShoulders = packet.ReadInt32("ItemBonusListShoulders", indexes);
            hotfix.ItemBonusListChest = packet.ReadInt32("ItemBonusListChest", indexes);
            hotfix.AzeriteUnlockMappingSetID = packet.ReadUInt32("AzeriteUnlockMappingSetID", indexes);

            Storage.AzeriteUnlockMappingHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void BankTabHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            BankTabHotfix1200 hotfix = new BankTabHotfix1200();

            hotfix.ID = entry;
            hotfix.Cost = packet.ReadUInt64("Cost", indexes);
            hotfix.BankType = packet.ReadByte("BankType", indexes);
            hotfix.OrderIndex = packet.ReadSByte("OrderIndex", indexes);
            hotfix.PlayerConditionID = packet.ReadInt32("PlayerConditionID", indexes);
            hotfix.PurchasePromptTitle = packet.ReadInt32("PurchasePromptTitle", indexes);
            hotfix.PurchasePromptBody = packet.ReadInt32("PurchasePromptBody", indexes);
            hotfix.PurchasePromptConfirmation = packet.ReadInt32("PurchasePromptConfirmation", indexes);
            hotfix.TabCleanupConfirmation = packet.ReadInt32("TabCleanupConfirmation", indexes);
            hotfix.TabNameEditBoxHeader = packet.ReadInt32("TabNameEditBoxHeader", indexes);

            Storage.BankTabHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void BannedAddonsHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            BannedAddonsHotfix1200 hotfix = new BannedAddonsHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Version = packet.ReadCString("Version", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.BannedAddonsHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void BarberShopStyleHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            BarberShopStyleHotfix1200 hotfix = new BarberShopStyleHotfix1200();

            hotfix.ID = entry;
            hotfix.DisplayName = packet.ReadCString("DisplayName", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.Type = packet.ReadByte("Type", indexes);
            hotfix.CostModifier = packet.ReadSingle("CostModifier", indexes);
            hotfix.Race = packet.ReadSByte("Race", indexes);
            hotfix.Sex = packet.ReadSByte("Sex", indexes);
            hotfix.Data = packet.ReadByte("Data", indexes);

            Storage.BarberShopStyleHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                BarberShopStyleLocaleHotfix1200 hotfixLocale = new BarberShopStyleLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    DisplayNameLang = hotfix.DisplayName,
                    DescriptionLang = hotfix.Description,
                };
                Storage.BarberShopStyleHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void BattlePetAbilityHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            BattlePetAbilityHotfix1200 hotfix = new BattlePetAbilityHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.IconFileDataID = packet.ReadInt32("IconFileDataID", indexes);
            hotfix.PetTypeEnum = packet.ReadSByte("PetTypeEnum", indexes);
            hotfix.Cooldown = packet.ReadUInt32("Cooldown", indexes);
            hotfix.BattlePetVisualID = packet.ReadUInt16("BattlePetVisualID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.BattlePetAbilityHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                BattlePetAbilityLocaleHotfix1200 hotfixLocale = new BattlePetAbilityLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.BattlePetAbilityHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void BattlePetBreedQualityHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            BattlePetBreedQualityHotfix1200 hotfix = new BattlePetBreedQualityHotfix1200();

            hotfix.ID = entry;
            hotfix.MaxQualityRoll = packet.ReadInt32("MaxQualityRoll", indexes);
            hotfix.StateMultiplier = packet.ReadSingle("StateMultiplier", indexes);
            hotfix.QualityEnum = packet.ReadByte("QualityEnum", indexes);

            Storage.BattlePetBreedQualityHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void BattlePetBreedStateHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            BattlePetBreedStateHotfix1200 hotfix = new BattlePetBreedStateHotfix1200();

            hotfix.ID = entry;
            hotfix.BattlePetStateID = packet.ReadInt32("BattlePetStateID", indexes);
            hotfix.Value = packet.ReadUInt16("Value", indexes);
            hotfix.BattlePetBreedID = packet.ReadUInt32("BattlePetBreedID", indexes);

            Storage.BattlePetBreedStateHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void BattlePetSpeciesHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            BattlePetSpeciesHotfix1200 hotfix = new BattlePetSpeciesHotfix1200();

            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.SourceText = packet.ReadCString("SourceText", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.CreatureID = packet.ReadInt32("CreatureID", indexes);
            hotfix.SummonSpellID = packet.ReadInt32("SummonSpellID", indexes);
            hotfix.IconFileDataID = packet.ReadInt32("IconFileDataID", indexes);
            hotfix.PetTypeEnum = packet.ReadSByte("PetTypeEnum", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.SourceTypeEnum = packet.ReadSByte("SourceTypeEnum", indexes);
            hotfix.CardUIModelSceneID = packet.ReadInt32("CardUIModelSceneID", indexes);
            hotfix.LoadoutUIModelSceneID = packet.ReadInt32("LoadoutUIModelSceneID", indexes);
            hotfix.CovenantID = packet.ReadInt32("CovenantID", indexes);

            Storage.BattlePetSpeciesHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                BattlePetSpeciesLocaleHotfix1200 hotfixLocale = new BattlePetSpeciesLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                    SourceTextLang = hotfix.SourceText,
                };
                Storage.BattlePetSpeciesHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void BattlePetSpeciesStateHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            BattlePetSpeciesStateHotfix1200 hotfix = new BattlePetSpeciesStateHotfix1200();

            hotfix.ID = entry;
            hotfix.BattlePetStateID = packet.ReadUInt16("BattlePetStateID", indexes);
            hotfix.Value = packet.ReadInt32("Value", indexes);
            hotfix.BattlePetSpeciesID = packet.ReadUInt32("BattlePetSpeciesID", indexes);

            Storage.BattlePetSpeciesStateHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void BattlemasterListHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            BattlemasterListHotfix1200 hotfix = new BattlemasterListHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.GameType = packet.ReadCString("GameType", indexes);
            hotfix.ShortDescription = packet.ReadCString("ShortDescription", indexes);
            hotfix.LongDescription = packet.ReadCString("LongDescription", indexes);
            hotfix.InstanceType = packet.ReadSByte("InstanceType", indexes);
            hotfix.PvpType = packet.ReadInt32("PvpType", indexes);
            hotfix.MinLevel = packet.ReadSByte("MinLevel", indexes);
            hotfix.MaxLevel = packet.ReadSByte("MaxLevel", indexes);
            hotfix.RatedPlayers = packet.ReadSByte("RatedPlayers", indexes);
            hotfix.MinPlayers = packet.ReadSByte("MinPlayers", indexes);
            hotfix.MaxPlayers = packet.ReadInt32("MaxPlayers", indexes);
            hotfix.GroupsAllowed = packet.ReadSByte("GroupsAllowed", indexes);
            hotfix.MaxGroupSize = packet.ReadSByte("MaxGroupSize", indexes);
            hotfix.HolidayWorldState = packet.ReadInt16("HolidayWorldState", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.IconFileDataID = packet.ReadInt32("IconFileDataID", indexes);
            hotfix.RequiredPlayerConditionID = packet.ReadInt32("RequiredPlayerConditionID", indexes);

            Storage.BattlemasterListHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                BattlemasterListLocaleHotfix1200 hotfixLocale = new BattlemasterListLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    GameTypeLang = hotfix.GameType,
                    ShortDescriptionLang = hotfix.ShortDescription,
                    LongDescriptionLang = hotfix.LongDescription,
                };
                Storage.BattlemasterListHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void BattlemasterListXMapHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            BattlemasterListXMapHotfix1200 hotfix = new BattlemasterListXMapHotfix1200();

            hotfix.ID = entry;
            hotfix.MapID = packet.ReadInt32("MapID", indexes);
            hotfix.BattlemasterListID = packet.ReadUInt32("BattlemasterListID", indexes);

            Storage.BattlemasterListXMapHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void BroadcastTextHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            BroadcastTextHotfix1200 hotfix = new BroadcastTextHotfix1200();

            hotfix.Text = packet.ReadCString("Text", indexes);
            hotfix.Text1 = packet.ReadCString("Text1", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.LanguageID = packet.ReadInt32("LanguageID", indexes);
            hotfix.ConditionID = packet.ReadInt32("ConditionID", indexes);
            hotfix.EmotesID = packet.ReadUInt16("EmotesID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ChatBubbleDurationMs = packet.ReadUInt32("ChatBubbleDurationMs", indexes);
            hotfix.VoiceOverPriorityID = packet.ReadInt32("VoiceOverPriorityID", indexes);
            hotfix.SoundKitID = new uint?[2];
            for (int i = 0; i < 2; i++)
                hotfix.SoundKitID[i] = packet.ReadUInt32("SoundKitID", indexes, i);
            hotfix.EmoteID = new ushort?[3];
            for (int i = 0; i < 3; i++)
                hotfix.EmoteID[i] = packet.ReadUInt16("EmoteID", indexes, i);
            hotfix.EmoteDelay = new ushort?[3];
            for (int i = 0; i < 3; i++)
                hotfix.EmoteDelay[i] = packet.ReadUInt16("EmoteDelay", indexes, i);

            Storage.BroadcastTextHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                BroadcastTextLocaleHotfix1200 hotfixLocale = new BroadcastTextLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    TextLang = hotfix.Text,
                    Text1Lang = hotfix.Text1,
                };
                Storage.BroadcastTextHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void BroadcastTextDurationHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            BroadcastTextDurationHotfix1200 hotfix = new BroadcastTextDurationHotfix1200();

            hotfix.ID = entry;
            hotfix.Locale = packet.ReadInt32("Locale", indexes);
            hotfix.Duration = packet.ReadInt32("Duration", indexes);
            hotfix.BroadcastTextID = packet.ReadUInt32("BroadcastTextID", indexes);

            Storage.BroadcastTextDurationHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void CfgCategoriesHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            CfgCategoriesHotfix1200 hotfix = new CfgCategoriesHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.LocaleMask = packet.ReadUInt16("LocaleMask", indexes);
            hotfix.CreateCharsetMask = packet.ReadByte("CreateCharsetMask", indexes);
            hotfix.ExistingCharsetMask = packet.ReadByte("ExistingCharsetMask", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.Order = packet.ReadSByte("Order", indexes);

            Storage.CfgCategoriesHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void CfgRegionsHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            CfgRegionsHotfix1200 hotfix = new CfgRegionsHotfix1200();

            hotfix.ID = entry;
            hotfix.Tag = packet.ReadCString("Tag", indexes);
            hotfix.RegionID = packet.ReadUInt16("RegionID", indexes);
            hotfix.Raidorigin = packet.ReadUInt32("Raidorigin", indexes);
            hotfix.RegionGroupMask = packet.ReadByte("RegionGroupMask", indexes);
            hotfix.ChallengeOrigin = packet.ReadUInt32("ChallengeOrigin", indexes);
            hotfix.TimeEventRegionGroupID = packet.ReadInt32("TimeEventRegionGroupID", indexes);

            Storage.CfgRegionsHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ChallengeModeItemBonusOverrideHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ChallengeModeItemBonusOverrideHotfix1200 hotfix = new ChallengeModeItemBonusOverrideHotfix1200();

            hotfix.ID = entry;
            hotfix.ItemBonusTreeGroupID = packet.ReadInt32("ItemBonusTreeGroupID", indexes);
            hotfix.DstItemBonusTreeID = packet.ReadInt32("DstItemBonusTreeID", indexes);
            hotfix.Value = packet.ReadInt32("Value", indexes);
            hotfix.RequiredTimeEventPassed = packet.ReadInt32("RequiredTimeEventPassed", indexes);
            hotfix.RequiredTimeEventNotPassed = packet.ReadInt32("RequiredTimeEventNotPassed", indexes);
            hotfix.SrcItemBonusTreeID = packet.ReadUInt32("SrcItemBonusTreeID", indexes);

            Storage.ChallengeModeItemBonusOverrideHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void CharBaseInfoHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            CharBaseInfoHotfix1200 hotfix = new CharBaseInfoHotfix1200();

            hotfix.ID = entry;
            hotfix.RaceID = packet.ReadSByte("RaceID", indexes);
            hotfix.ClassID = packet.ReadSByte("ClassID", indexes);
            hotfix.OtherFactionRaceID = packet.ReadSByte("OtherFactionRaceID", indexes);

            Storage.CharBaseInfoHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void CharTitlesHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            CharTitlesHotfix1200 hotfix = new CharTitlesHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Name1 = packet.ReadCString("Name1", indexes);
            hotfix.MaskID = packet.ReadInt16("MaskID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.CharTitlesHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CharTitlesLocaleHotfix1200 hotfixLocale = new CharTitlesLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    Name1Lang = hotfix.Name1,
                };
                Storage.CharTitlesHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CharacterLoadoutHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            CharacterLoadoutHotfix1200 hotfix = new CharacterLoadoutHotfix1200();

            hotfix.ID = entry;
            hotfix.RaceMask = packet.ReadInt64("RaceMask", indexes);
            hotfix.ChrClassID = packet.ReadSByte("ChrClassID", indexes);
            hotfix.Purpose = packet.ReadInt32("Purpose", indexes);
            hotfix.ItemContext = packet.ReadByte("ItemContext", indexes);

            Storage.CharacterLoadoutHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void CharacterLoadoutItemHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            CharacterLoadoutItemHotfix1200 hotfix = new CharacterLoadoutItemHotfix1200();

            hotfix.ID = entry;
            hotfix.CharacterLoadoutID = packet.ReadUInt16("CharacterLoadoutID", indexes);
            hotfix.ItemID = packet.ReadUInt32("ItemID", indexes);

            Storage.CharacterLoadoutItemHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ChatChannelsHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ChatChannelsHotfix1200 hotfix = new ChatChannelsHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Shortcut = packet.ReadCString("Shortcut", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.FactionGroup = packet.ReadByte("FactionGroup", indexes);
            hotfix.Ruleset = packet.ReadInt32("Ruleset", indexes);

            Storage.ChatChannelsHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChatChannelsLocaleHotfix1200 hotfixLocale = new ChatChannelsLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    ShortcutLang = hotfix.Shortcut,
                };
                Storage.ChatChannelsHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ChrClassUiDisplayHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ChrClassUiDisplayHotfix1200 hotfix = new ChrClassUiDisplayHotfix1200();

            hotfix.ID = entry;
            hotfix.ChrClassesID = packet.ReadSByte("ChrClassesID", indexes);
            hotfix.AdvGuidePlayerConditionID = packet.ReadUInt32("AdvGuidePlayerConditionID", indexes);
            hotfix.SplashPlayerConditionID = packet.ReadUInt32("SplashPlayerConditionID", indexes);

            Storage.ChrClassUiDisplayHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrClassesHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ChrClassesHotfix1200 hotfix = new ChrClassesHotfix1200();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Filename = packet.ReadCString("Filename", indexes);
            hotfix.NameMale = packet.ReadCString("NameMale", indexes);
            hotfix.NameFemale = packet.ReadCString("NameFemale", indexes);
            hotfix.PetNameToken = packet.ReadCString("PetNameToken", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.RoleInfoString = packet.ReadCString("RoleInfoString", indexes);
            hotfix.DisabledString = packet.ReadCString("DisabledString", indexes);
            hotfix.HyphenatedNameMale = packet.ReadCString("HyphenatedNameMale", indexes);
            hotfix.HyphenatedNameFemale = packet.ReadCString("HyphenatedNameFemale", indexes);
            hotfix.CreateScreenFileDataID = packet.ReadUInt32("CreateScreenFileDataID", indexes);
            hotfix.SelectScreenFileDataID = packet.ReadUInt32("SelectScreenFileDataID", indexes);
            hotfix.IconFileDataID = packet.ReadUInt32("IconFileDataID", indexes);
            hotfix.LowResScreenFileDataID = packet.ReadUInt32("LowResScreenFileDataID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.StartingLevel = packet.ReadInt32("StartingLevel", indexes);
            hotfix.SpellTextureBlobFileDataID = packet.ReadUInt32("SpellTextureBlobFileDataID", indexes);
            hotfix.ArmorTypeMask = packet.ReadUInt32("ArmorTypeMask", indexes);
            hotfix.CharStartKitUnknown901 = packet.ReadInt32("CharStartKitUnknown901", indexes);
            hotfix.MaleCharacterCreationVisualFallback = packet.ReadInt32("MaleCharacterCreationVisualFallback", indexes);
            hotfix.MaleCharacterCreationIdleVisualFallback = packet.ReadInt32("MaleCharacterCreationIdleVisualFallback", indexes);
            hotfix.FemaleCharacterCreationVisualFallback = packet.ReadInt32("FemaleCharacterCreationVisualFallback", indexes);
            hotfix.FemaleCharacterCreationIdleVisualFallback = packet.ReadInt32("FemaleCharacterCreationIdleVisualFallback", indexes);
            hotfix.CharacterCreationIdleGroundVisualFallback = packet.ReadInt32("CharacterCreationIdleGroundVisualFallback", indexes);
            hotfix.CharacterCreationGroundVisualFallback = packet.ReadInt32("CharacterCreationGroundVisualFallback", indexes);
            hotfix.AlteredFormCharacterCreationIdleVisualFallback = packet.ReadInt32("AlteredFormCharacterCreationIdleVisualFallback", indexes);
            hotfix.CharacterCreationAnimLoopWaitTimeMsFallback = packet.ReadInt32("CharacterCreationAnimLoopWaitTimeMsFallback", indexes);
            hotfix.CinematicSequenceID = packet.ReadUInt16("CinematicSequenceID", indexes);
            hotfix.DefaultSpec = packet.ReadUInt16("DefaultSpec", indexes);
            hotfix.ID = packet.ReadByte("ID", indexes);
            hotfix.HasStrengthBonus = packet.ReadByte("HasStrengthBonus", indexes);
            hotfix.PrimaryStatPriority = packet.ReadSByte("PrimaryStatPriority", indexes);
            hotfix.DisplayPower = packet.ReadSByte("DisplayPower", indexes);
            hotfix.RangedAttackPowerPerAgility = packet.ReadByte("RangedAttackPowerPerAgility", indexes);
            hotfix.AttackPowerPerAgility = packet.ReadByte("AttackPowerPerAgility", indexes);
            hotfix.AttackPowerPerStrength = packet.ReadByte("AttackPowerPerStrength", indexes);
            hotfix.SpellClassSet = packet.ReadByte("SpellClassSet", indexes);
            hotfix.ClassColorR = packet.ReadByte("ClassColorR", indexes);
            hotfix.ClassColorG = packet.ReadByte("ClassColorG", indexes);
            hotfix.ClassColorB = packet.ReadByte("ClassColorB", indexes);
            hotfix.RolesMask = packet.ReadByte("RolesMask", indexes);
            hotfix.DamageBonusStat = packet.ReadByte("DamageBonusStat", indexes);
            hotfix.HasRelicSlot = packet.ReadByte("HasRelicSlot", indexes);

            Storage.ChrClassesHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChrClassesLocaleHotfix1200 hotfixLocale = new ChrClassesLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    NameMaleLang = hotfix.NameMale,
                    NameFemaleLang = hotfix.NameFemale,
                    DescriptionLang = hotfix.Description,
                    RoleInfoStringLang = hotfix.RoleInfoString,
                    DisabledStringLang = hotfix.DisabledString,
                    HyphenatedNameMaleLang = hotfix.HyphenatedNameMale,
                    HyphenatedNameFemaleLang = hotfix.HyphenatedNameFemale,
                };
                Storage.ChrClassesHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ChrClassesXPowerTypesHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ChrClassesXPowerTypesHotfix1200 hotfix = new ChrClassesXPowerTypesHotfix1200();

            hotfix.ID = entry;
            hotfix.PowerType = packet.ReadSByte("PowerType", indexes);
            hotfix.ClassID = packet.ReadUInt32("ClassID", indexes);

            Storage.ChrClassesXPowerTypesHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrCustomizationChoiceHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationChoiceHotfix1200 hotfix = new ChrCustomizationChoiceHotfix1200();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ChrCustomizationOptionID = packet.ReadUInt32("ChrCustomizationOptionID", indexes);
            hotfix.ChrCustomizationReqID = packet.ReadInt32("ChrCustomizationReqID", indexes);
            hotfix.ChrCustomizationVisReqID = packet.ReadInt32("ChrCustomizationVisReqID", indexes);
            hotfix.SortOrder = packet.ReadUInt16("SortOrder", indexes);
            hotfix.UiOrderIndex = packet.ReadUInt16("UiOrderIndex", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.AddedInPatch = packet.ReadInt32("AddedInPatch", indexes);
            hotfix.SoundKitID = packet.ReadInt32("SoundKitID", indexes);
            hotfix.SwatchColor = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.SwatchColor[i] = packet.ReadInt32("SwatchColor", indexes, i);

            Storage.ChrCustomizationChoiceHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChrCustomizationChoiceLocaleHotfix1200 hotfixLocale = new ChrCustomizationChoiceLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ChrCustomizationChoiceHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ChrCustomizationDisplayInfoHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationDisplayInfoHotfix1200 hotfix = new ChrCustomizationDisplayInfoHotfix1200();

            hotfix.ID = entry;
            hotfix.ShapeshiftFormID = packet.ReadInt32("ShapeshiftFormID", indexes);
            hotfix.DisplayID = packet.ReadInt32("DisplayID", indexes);
            hotfix.BarberShopMinCameraDistance = packet.ReadSingle("BarberShopMinCameraDistance", indexes);
            hotfix.BarberShopHeightOffset = packet.ReadSingle("BarberShopHeightOffset", indexes);
            hotfix.BarberShopCameraZoomOffset = packet.ReadSingle("BarberShopCameraZoomOffset", indexes);

            Storage.ChrCustomizationDisplayInfoHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrCustomizationElementHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationElementHotfix1200 hotfix = new ChrCustomizationElementHotfix1200();

            hotfix.ID = entry;
            hotfix.ChrCustomizationChoiceID = packet.ReadInt32("ChrCustomizationChoiceID", indexes);
            hotfix.RelatedChrCustomizationChoiceID = packet.ReadInt32("RelatedChrCustomizationChoiceID", indexes);
            hotfix.ChrCustomizationGeosetID = packet.ReadInt32("ChrCustomizationGeosetID", indexes);
            hotfix.ChrCustomizationSkinnedModelID = packet.ReadInt32("ChrCustomizationSkinnedModelID", indexes);
            hotfix.ChrCustomizationMaterialID = packet.ReadInt32("ChrCustomizationMaterialID", indexes);
            hotfix.ChrCustomizationBoneSetID = packet.ReadInt32("ChrCustomizationBoneSetID", indexes);
            hotfix.ChrCustomizationCondModelID = packet.ReadInt32("ChrCustomizationCondModelID", indexes);
            hotfix.ChrCustomizationDisplayInfoID = packet.ReadInt32("ChrCustomizationDisplayInfoID", indexes);
            hotfix.ChrCustItemGeoModifyID = packet.ReadInt32("ChrCustItemGeoModifyID", indexes);
            hotfix.ChrCustomizationVoiceID = packet.ReadInt32("ChrCustomizationVoiceID", indexes);
            hotfix.AnimKitID = packet.ReadInt32("AnimKitID", indexes);
            hotfix.ParticleColorID = packet.ReadInt32("ParticleColorID", indexes);
            hotfix.ChrCustGeoComponentLinkID = packet.ReadInt32("ChrCustGeoComponentLinkID", indexes);

            Storage.ChrCustomizationElementHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrCustomizationOptionHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationOptionHotfix1200 hotfix = new ChrCustomizationOptionHotfix1200();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.SecondaryID = packet.ReadUInt16("SecondaryID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ChrModelID = packet.ReadUInt32("ChrModelID", indexes);
            hotfix.SortIndex = packet.ReadInt32("SortIndex", indexes);
            hotfix.ChrCustomizationCategoryID = packet.ReadInt32("ChrCustomizationCategoryID", indexes);
            hotfix.OptionType = packet.ReadInt32("OptionType", indexes);
            hotfix.BarberShopCostModifier = packet.ReadSingle("BarberShopCostModifier", indexes);
            hotfix.ChrCustomizationID = packet.ReadInt32("ChrCustomizationID", indexes);
            hotfix.ChrCustomizationReqID = packet.ReadInt32("ChrCustomizationReqID", indexes);
            hotfix.UiOrderIndex = packet.ReadInt32("UiOrderIndex", indexes);
            hotfix.AddedInPatch = packet.ReadInt32("AddedInPatch", indexes);

            Storage.ChrCustomizationOptionHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChrCustomizationOptionLocaleHotfix1200 hotfixLocale = new ChrCustomizationOptionLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ChrCustomizationOptionHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ChrCustomizationReqHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationReqHotfix1200 hotfix = new ChrCustomizationReqHotfix1200();

            hotfix.ID = entry;
            hotfix.RaceMask = packet.ReadInt64("RaceMask", indexes);
            hotfix.ReqSource = packet.ReadCString("ReqSource", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ClassMask = packet.ReadInt32("ClassMask", indexes);
            hotfix.RegionGroupMask = packet.ReadInt32("RegionGroupMask", indexes);
            hotfix.AchievementID = packet.ReadInt32("AchievementID", indexes);
            hotfix.QuestID = packet.ReadInt32("QuestID", indexes);
            hotfix.OverrideArchive = packet.ReadInt32("OverrideArchive", indexes);
            hotfix.ItemModifiedAppearanceID = packet.ReadInt32("ItemModifiedAppearanceID", indexes);

            Storage.ChrCustomizationReqHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChrCustomizationReqLocaleHotfix1200 hotfixLocale = new ChrCustomizationReqLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    ReqSourceLang = hotfix.ReqSource,
                };
                Storage.ChrCustomizationReqHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ChrCustomizationReqChoiceHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationReqChoiceHotfix1200 hotfix = new ChrCustomizationReqChoiceHotfix1200();

            hotfix.ID = entry;
            hotfix.ChrCustomizationChoiceID = packet.ReadInt32("ChrCustomizationChoiceID", indexes);
            hotfix.ChrCustomizationReqID = packet.ReadUInt32("ChrCustomizationReqID", indexes);

            Storage.ChrCustomizationReqChoiceHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrModelHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ChrModelHotfix1200 hotfix = new ChrModelHotfix1200();

            hotfix.FaceCustomizationOffset = new float?[3];
            for (int i = 0; i < 3; i++)
                hotfix.FaceCustomizationOffset[i] = packet.ReadSingle("FaceCustomizationOffset", indexes, i);
            hotfix.CustomizeOffset = new float?[3];
            for (int i = 0; i < 3; i++)
                hotfix.CustomizeOffset[i] = packet.ReadSingle("CustomizeOffset", indexes, i);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Sex = packet.ReadSByte("Sex", indexes);
            hotfix.DisplayID = packet.ReadUInt32("DisplayID", indexes);
            hotfix.CharComponentTextureLayoutID = packet.ReadInt32("CharComponentTextureLayoutID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.SkeletonFileDataID = packet.ReadInt32("SkeletonFileDataID", indexes);
            hotfix.ModelFallbackChrModelID = packet.ReadInt32("ModelFallbackChrModelID", indexes);
            hotfix.TextureFallbackChrModelID = packet.ReadInt32("TextureFallbackChrModelID", indexes);
            hotfix.HelmVisFallbackChrModelID = packet.ReadInt32("HelmVisFallbackChrModelID", indexes);
            hotfix.CustomizeScale = packet.ReadSingle("CustomizeScale", indexes);
            hotfix.CustomizeFacing = packet.ReadSingle("CustomizeFacing", indexes);
            hotfix.CameraDistanceOffset = packet.ReadSingle("CameraDistanceOffset", indexes);
            hotfix.BarberShopCameraOffsetScale = packet.ReadSingle("BarberShopCameraOffsetScale", indexes);
            hotfix.BarberShopCameraHeightOffsetScale = packet.ReadSingle("BarberShopCameraHeightOffsetScale", indexes);
            hotfix.BarberShopCameraRotationOffset = packet.ReadSingle("BarberShopCameraRotationOffset", indexes);

            Storage.ChrModelHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrRaceXChrModelHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ChrRaceXChrModelHotfix1200 hotfix = new ChrRaceXChrModelHotfix1200();

            hotfix.ID = entry;
            hotfix.ChrRacesID = packet.ReadByte("ChrRacesID", indexes);
            hotfix.ChrModelID = packet.ReadInt32("ChrModelID", indexes);
            hotfix.Sex = packet.ReadSByte("Sex", indexes);
            hotfix.AllowedTransmogSlots = packet.ReadInt32("AllowedTransmogSlots", indexes);

            Storage.ChrRaceXChrModelHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrRacesHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ChrRacesHotfix1200 hotfix = new ChrRacesHotfix1200();

            hotfix.ID = entry;
            hotfix.ClientPrefix = packet.ReadCString("ClientPrefix", indexes);
            hotfix.ClientFileString = packet.ReadCString("ClientFileString", indexes);
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.NameFemale = packet.ReadCString("NameFemale", indexes);
            hotfix.NameLowercase = packet.ReadCString("NameLowercase", indexes);
            hotfix.NameFemaleLowercase = packet.ReadCString("NameFemaleLowercase", indexes);
            hotfix.LoreName = packet.ReadCString("LoreName", indexes);
            hotfix.LoreNameFemale = packet.ReadCString("LoreNameFemale", indexes);
            hotfix.LoreNameLower = packet.ReadCString("LoreNameLower", indexes);
            hotfix.LoreNameLowerFemale = packet.ReadCString("LoreNameLowerFemale", indexes);
            hotfix.LoreDescription = packet.ReadCString("LoreDescription", indexes);
            hotfix.ShortName = packet.ReadCString("ShortName", indexes);
            hotfix.ShortNameFemale = packet.ReadCString("ShortNameFemale", indexes);
            hotfix.ShortNameLower = packet.ReadCString("ShortNameLower", indexes);
            hotfix.ShortNameLowerFemale = packet.ReadCString("ShortNameLowerFemale", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.FactionID = packet.ReadInt32("FactionID", indexes);
            hotfix.CinematicSequenceID = packet.ReadInt32("CinematicSequenceID", indexes);
            hotfix.ResSicknessSpellID = packet.ReadInt32("ResSicknessSpellID", indexes);
            hotfix.SplashSoundID = packet.ReadInt32("SplashSoundID", indexes);
            hotfix.CreateScreenFileDataID = packet.ReadInt32("CreateScreenFileDataID", indexes);
            hotfix.SelectScreenFileDataID = packet.ReadInt32("SelectScreenFileDataID", indexes);
            hotfix.LowResScreenFileDataID = packet.ReadInt32("LowResScreenFileDataID", indexes);
            hotfix.AlteredFormStartVisualKitID = new int?[3];
            for (int i = 0; i < 3; i++)
                hotfix.AlteredFormStartVisualKitID[i] = packet.ReadInt32("AlteredFormStartVisualKitID", indexes, i);
            hotfix.AlteredFormFinishVisualKitID = new int?[3];
            for (int i = 0; i < 3; i++)
                hotfix.AlteredFormFinishVisualKitID[i] = packet.ReadInt32("AlteredFormFinishVisualKitID", indexes, i);
            hotfix.HeritageArmorAchievementID = packet.ReadInt32("HeritageArmorAchievementID", indexes);
            hotfix.StartingLevel = packet.ReadInt32("StartingLevel", indexes);
            hotfix.UiDisplayOrder = packet.ReadInt32("UiDisplayOrder", indexes);
            hotfix.PlayableRaceBit = packet.ReadInt32("PlayableRaceBit", indexes);
            hotfix.TransmogrifyDisabledSlotMask = packet.ReadInt32("TransmogrifyDisabledSlotMask", indexes);
            hotfix.AlteredFormCustomizeOffsetFallback = new float?[3];
            for (int i = 0; i < 3; i++)
                hotfix.AlteredFormCustomizeOffsetFallback[i] = packet.ReadSingle("AlteredFormCustomizeOffsetFallback", indexes, i);
            hotfix.AlteredFormCustomizeRotationFallback = packet.ReadSingle("AlteredFormCustomizeRotationFallback", indexes);
            hotfix.Unknown910_1 = new float?[3];
            for (int i = 0; i < 3; i++)
                hotfix.Unknown910_1[i] = packet.ReadSingle("Unknown910_1", indexes, i);
            hotfix.Unknown910_2 = new float?[3];
            for (int i = 0; i < 3; i++)
                hotfix.Unknown910_2[i] = packet.ReadSingle("Unknown910_2", indexes, i);
            hotfix.BaseLanguage = packet.ReadSByte("BaseLanguage", indexes);
            hotfix.CreatureType = packet.ReadByte("CreatureType", indexes);
            hotfix.Alliance = packet.ReadSByte("Alliance", indexes);
            hotfix.RaceRelated = packet.ReadSByte("RaceRelated", indexes);
            hotfix.UnalteredVisualRaceID = packet.ReadSByte("UnalteredVisualRaceID", indexes);
            hotfix.DefaultClassID = packet.ReadSByte("DefaultClassID", indexes);
            hotfix.NeutralRaceID = packet.ReadSByte("NeutralRaceID", indexes);
            hotfix.MaleModelFallbackRaceID = packet.ReadSByte("MaleModelFallbackRaceID", indexes);
            hotfix.MaleModelFallbackSex = packet.ReadSByte("MaleModelFallbackSex", indexes);
            hotfix.FemaleModelFallbackRaceID = packet.ReadSByte("FemaleModelFallbackRaceID", indexes);
            hotfix.FemaleModelFallbackSex = packet.ReadSByte("FemaleModelFallbackSex", indexes);
            hotfix.MaleTextureFallbackRaceID = packet.ReadSByte("MaleTextureFallbackRaceID", indexes);
            hotfix.MaleTextureFallbackSex = packet.ReadSByte("MaleTextureFallbackSex", indexes);
            hotfix.FemaleTextureFallbackRaceID = packet.ReadSByte("FemaleTextureFallbackRaceID", indexes);
            hotfix.FemaleTextureFallbackSex = packet.ReadSByte("FemaleTextureFallbackSex", indexes);
            hotfix.HelmetAnimScalingRaceID = packet.ReadSByte("HelmetAnimScalingRaceID", indexes);
            hotfix.UnalteredVisualCustomizationRaceID = packet.ReadSByte("UnalteredVisualCustomizationRaceID", indexes);

            Storage.ChrRacesHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChrRacesLocaleHotfix1200 hotfixLocale = new ChrRacesLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    NameFemaleLang = hotfix.NameFemale,
                    NameLowercaseLang = hotfix.NameLowercase,
                    NameFemaleLowercaseLang = hotfix.NameFemaleLowercase,
                    LoreNameLang = hotfix.LoreName,
                    LoreNameFemaleLang = hotfix.LoreNameFemale,
                    LoreNameLowerLang = hotfix.LoreNameLower,
                    LoreNameLowerFemaleLang = hotfix.LoreNameLowerFemale,
                    LoreDescriptionLang = hotfix.LoreDescription,
                    ShortNameLang = hotfix.ShortName,
                    ShortNameFemaleLang = hotfix.ShortNameFemale,
                    ShortNameLowerLang = hotfix.ShortNameLower,
                    ShortNameLowerFemaleLang = hotfix.ShortNameLowerFemale,
                };
                Storage.ChrRacesHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ChrSpecializationHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ChrSpecializationHotfix1200 hotfix = new ChrSpecializationHotfix1200();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.FemaleName = packet.ReadCString("FemaleName", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ClassID = packet.ReadByte("ClassID", indexes);
            hotfix.OrderIndex = packet.ReadSByte("OrderIndex", indexes);
            hotfix.PetTalentType = packet.ReadSByte("PetTalentType", indexes);
            hotfix.Role = packet.ReadSByte("Role", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.SpellIconFileID = packet.ReadInt32("SpellIconFileID", indexes);
            hotfix.PrimaryStatPriority = packet.ReadSByte("PrimaryStatPriority", indexes);
            hotfix.AnimReplacements = packet.ReadInt32("AnimReplacements", indexes);
            hotfix.MasterySpellID = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.MasterySpellID[i] = packet.ReadInt32("MasterySpellID", indexes, i);

            Storage.ChrSpecializationHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChrSpecializationLocaleHotfix1200 hotfixLocale = new ChrSpecializationLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    FemaleNameLang = hotfix.FemaleName,
                    DescriptionLang = hotfix.Description,
                };
                Storage.ChrSpecializationHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CinematicCameraHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            CinematicCameraHotfix1200 hotfix = new CinematicCameraHotfix1200();

            hotfix.ID = entry;
            hotfix.OriginX = packet.ReadSingle("OriginX", indexes);
            hotfix.OriginY = packet.ReadSingle("OriginY", indexes);
            hotfix.OriginZ = packet.ReadSingle("OriginZ", indexes);
            hotfix.SoundID = packet.ReadUInt32("SoundID", indexes);
            hotfix.OriginFacing = packet.ReadSingle("OriginFacing", indexes);
            hotfix.FileDataID = packet.ReadUInt32("FileDataID", indexes);
            hotfix.ConversationID = packet.ReadUInt32("ConversationID", indexes);

            Storage.CinematicCameraHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void CinematicSequencesHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            CinematicSequencesHotfix1200 hotfix = new CinematicSequencesHotfix1200();

            hotfix.ID = entry;
            hotfix.SoundID = packet.ReadUInt32("SoundID", indexes);
            hotfix.Camera = new ushort?[8];
            for (int i = 0; i < 8; i++)
                hotfix.Camera[i] = packet.ReadUInt16("Camera", indexes, i);

            Storage.CinematicSequencesHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ConditionalChrModelHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ConditionalChrModelHotfix1200 hotfix = new ConditionalChrModelHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ChrModelID = packet.ReadUInt32("ChrModelID", indexes);
            hotfix.ChrCustomizationReqID = packet.ReadInt32("ChrCustomizationReqID", indexes);
            hotfix.PlayerConditionID = packet.ReadInt32("PlayerConditionID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ChrCustomizationCategoryID = packet.ReadInt32("ChrCustomizationCategoryID", indexes);

            Storage.ConditionalChrModelHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ConditionalContentTuningHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ConditionalContentTuningHotfix1200 hotfix = new ConditionalContentTuningHotfix1200();

            hotfix.ID = entry;
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.RedirectContentTuningID = packet.ReadInt32("RedirectContentTuningID", indexes);
            hotfix.RedirectFlag = packet.ReadInt32("RedirectFlag", indexes);
            hotfix.RedirectEnum = packet.ReadInt32("RedirectEnum", indexes);
            hotfix.ParentContentTuningID = packet.ReadUInt32("ParentContentTuningID", indexes);

            Storage.ConditionalContentTuningHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ContentTuningHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ContentTuningHotfix1200 hotfix = new ContentTuningHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ExpansionID = packet.ReadInt32("ExpansionID", indexes);
            hotfix.HealthItemLevelCurveID = packet.ReadInt32("HealthItemLevelCurveID", indexes);
            hotfix.DamageItemLevelCurveID = packet.ReadInt32("DamageItemLevelCurveID", indexes);
            hotfix.HealthPrimaryStatCurveID = packet.ReadInt32("HealthPrimaryStatCurveID", indexes);
            hotfix.DamagePrimaryStatCurveID = packet.ReadInt32("DamagePrimaryStatCurveID", indexes);
            hotfix.PrimaryStatScalingModPlayerDataElementCharacterID = packet.ReadInt32("PrimaryStatScalingModPlayerDataElementCharacterID", indexes);
            hotfix.PrimaryStatScalingModPlayerDataElementCharacterMultiplier = packet.ReadSingle("PrimaryStatScalingModPlayerDataElementCharacterMultiplier", indexes);
            hotfix.MinLevel = packet.ReadInt32("MinLevel", indexes);
            hotfix.MaxLevel = packet.ReadInt32("MaxLevel", indexes);
            hotfix.MinLevelType = packet.ReadInt32("MinLevelType", indexes);
            hotfix.MaxLevelType = packet.ReadInt32("MaxLevelType", indexes);
            hotfix.TargetLevelDelta = packet.ReadInt32("TargetLevelDelta", indexes);
            hotfix.TargetLevelMaxDelta = packet.ReadInt32("TargetLevelMaxDelta", indexes);
            hotfix.TargetLevelMin = packet.ReadInt32("TargetLevelMin", indexes);
            hotfix.TargetLevelMax = packet.ReadInt32("TargetLevelMax", indexes);
            hotfix.MinItemLevel = packet.ReadInt32("MinItemLevel", indexes);
            hotfix.QuestXpMultiplier = packet.ReadSingle("QuestXpMultiplier", indexes);

            Storage.ContentTuningHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ContentTuningXExpectedHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ContentTuningXExpectedHotfix1200 hotfix = new ContentTuningXExpectedHotfix1200();

            hotfix.ID = entry;
            hotfix.ExpectedStatModID = packet.ReadInt32("ExpectedStatModID", indexes);
            hotfix.MinMythicPlusSeasonID = packet.ReadInt32("MinMythicPlusSeasonID", indexes);
            hotfix.MaxMythicPlusSeasonID = packet.ReadInt32("MaxMythicPlusSeasonID", indexes);
            hotfix.ContentTuningID = packet.ReadUInt32("ContentTuningID", indexes);

            Storage.ContentTuningXExpectedHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ContentTuningXLabelHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ContentTuningXLabelHotfix1200 hotfix = new ContentTuningXLabelHotfix1200();

            hotfix.ID = entry;
            hotfix.LabelID = packet.ReadInt32("LabelID", indexes);
            hotfix.ContentTuningID = packet.ReadUInt32("ContentTuningID", indexes);

            Storage.ContentTuningXLabelHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ConversationLineHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ConversationLineHotfix1200 hotfix = new ConversationLineHotfix1200();

            hotfix.ID = entry;
            hotfix.BroadcastTextID = packet.ReadUInt32("BroadcastTextID", indexes);
            hotfix.Unused1020 = packet.ReadUInt32("Unused1020", indexes);
            hotfix.SpellVisualKitID = packet.ReadUInt32("SpellVisualKitID", indexes);
            hotfix.AdditionalDuration = packet.ReadInt32("AdditionalDuration", indexes);
            hotfix.NextConversationLineID = packet.ReadUInt16("NextConversationLineID", indexes);
            hotfix.AnimKitID = packet.ReadUInt16("AnimKitID", indexes);
            hotfix.SpeechType = packet.ReadByte("SpeechType", indexes);
            hotfix.StartAnimation = packet.ReadByte("StartAnimation", indexes);
            hotfix.EndAnimation = packet.ReadByte("EndAnimation", indexes);

            Storage.ConversationLineHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void CorruptionEffectsHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            CorruptionEffectsHotfix1200 hotfix = new CorruptionEffectsHotfix1200();

            hotfix.ID = entry;
            hotfix.MinCorruption = packet.ReadSingle("MinCorruption", indexes);
            hotfix.Aura = packet.ReadInt32("Aura", indexes);
            hotfix.PlayerConditionID = packet.ReadInt32("PlayerConditionID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.CorruptionEffectsHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void CraftingQualityHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            CraftingQualityHotfix1200 hotfix = new CraftingQualityHotfix1200();

            hotfix.ID = entry;
            hotfix.QualityTier = packet.ReadInt32("QualityTier", indexes);
            hotfix.CraftingQualityAtlasSetID = packet.ReadInt32("CraftingQualityAtlasSetID", indexes);

            Storage.CraftingQualityHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void CreatureDisplayInfoHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            CreatureDisplayInfoHotfix1200 hotfix = new CreatureDisplayInfoHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ModelID = packet.ReadUInt16("ModelID", indexes);
            hotfix.SoundID = packet.ReadUInt16("SoundID", indexes);
            hotfix.SizeClass = packet.ReadSByte("SizeClass", indexes);
            hotfix.CreatureModelScale = packet.ReadSingle("CreatureModelScale", indexes);
            hotfix.CreatureModelAlpha = packet.ReadByte("CreatureModelAlpha", indexes);
            hotfix.BloodID = packet.ReadByte("BloodID", indexes);
            hotfix.ExtendedDisplayInfoID = packet.ReadInt32("ExtendedDisplayInfoID", indexes);
            hotfix.NPCSoundID = packet.ReadUInt16("NPCSoundID", indexes);
            hotfix.ParticleColorID = packet.ReadUInt16("ParticleColorID", indexes);
            hotfix.PortraitCreatureDisplayInfoID = packet.ReadInt32("PortraitCreatureDisplayInfoID", indexes);
            hotfix.PortraitTextureFileDataID = packet.ReadInt32("PortraitTextureFileDataID", indexes);
            hotfix.ObjectEffectPackageID = packet.ReadUInt16("ObjectEffectPackageID", indexes);
            hotfix.AnimReplacementSetID = packet.ReadUInt16("AnimReplacementSetID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.StateSpellVisualKitID = packet.ReadInt32("StateSpellVisualKitID", indexes);
            hotfix.PlayerOverrideScale = packet.ReadSingle("PlayerOverrideScale", indexes);
            hotfix.PetInstanceScale = packet.ReadSingle("PetInstanceScale", indexes);
            hotfix.UnarmedWeaponType = packet.ReadSByte("UnarmedWeaponType", indexes);
            hotfix.MountPoofSpellVisualKitID = packet.ReadInt32("MountPoofSpellVisualKitID", indexes);
            hotfix.DissolveEffectID = packet.ReadInt32("DissolveEffectID", indexes);
            hotfix.Gender = packet.ReadSByte("Gender", indexes);
            hotfix.DissolveOutEffectID = packet.ReadInt32("DissolveOutEffectID", indexes);
            hotfix.CreatureModelMinLod = packet.ReadSByte("CreatureModelMinLod", indexes);
            hotfix.ConditionalCreatureModelID = packet.ReadUInt16("ConditionalCreatureModelID", indexes);
            hotfix.Unknown_1100_1 = packet.ReadSingle("Unknown_1100_1", indexes);
            hotfix.Unknown_1100_2 = packet.ReadUInt16("Unknown_1100_2", indexes);

            hotfix.TextureVariationFileDataID = new int?[4];
            for (int i = 0; i < 4; i++)
                hotfix.TextureVariationFileDataID[i] = packet.ReadInt32("TextureVariationFileDataID", indexes, i);

            Storage.CreatureDisplayInfoHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void CreatureDisplayInfoExtraHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            CreatureDisplayInfoExtraHotfix1200 hotfix = new CreatureDisplayInfoExtraHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.DisplayRaceID = packet.ReadSByte("DisplayRaceID", indexes);
            hotfix.DisplaySexID = packet.ReadSByte("DisplaySexID", indexes);
            hotfix.DisplayClassID = packet.ReadSByte("DisplayClassID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.BakeMaterialResourcesID = packet.ReadInt32("BakeMaterialResourcesID", indexes);
            hotfix.HDBakeMaterialResourcesID = packet.ReadInt32("HDBakeMaterialResourcesID", indexes);

            Storage.CreatureDisplayInfoExtraHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void CreatureFamilyHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            CreatureFamilyHotfix1200 hotfix = new CreatureFamilyHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.MinScale = packet.ReadSingle("MinScale", indexes);
            hotfix.MinScaleLevel = packet.ReadSByte("MinScaleLevel", indexes);
            hotfix.MaxScale = packet.ReadSingle("MaxScale", indexes);
            hotfix.MaxScaleLevel = packet.ReadSByte("MaxScaleLevel", indexes);
            hotfix.PetFoodMask = packet.ReadInt16("PetFoodMask", indexes);
            hotfix.PetTalentType = packet.ReadSByte("PetTalentType", indexes);
            hotfix.CategoryEnumID = packet.ReadInt32("CategoryEnumID", indexes);
            hotfix.IconFileID = packet.ReadInt32("IconFileID", indexes);
            hotfix.SkillLine = new short?[2];
            for (int i = 0; i < 2; i++)
                hotfix.SkillLine[i] = packet.ReadInt16("SkillLine", indexes, i);

            Storage.CreatureFamilyHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CreatureFamilyLocaleHotfix1200 hotfixLocale = new CreatureFamilyLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.CreatureFamilyHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CreatureLabelHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            CreatureLabelHotfix1200 hotfix = new CreatureLabelHotfix1200();

            hotfix.ID = entry;
            hotfix.LabelID = packet.ReadInt32("LabelID", indexes);
            hotfix.CreatureDifficultyID = packet.ReadUInt32("CreatureDifficultyID", indexes);
            Storage.CreatureLabelHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void CreatureModelDataHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            CreatureModelDataHotfix1200 hotfix = new CreatureModelDataHotfix1200();

            hotfix.ID = entry;
            hotfix.GeoBox = new float?[6];
            for (int i = 0; i < 6; i++)
                hotfix.GeoBox[i] = packet.ReadSingle("GeoBox", indexes, i);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.FileDataID = packet.ReadUInt32("FileDataID", indexes);
            hotfix.WalkSpeed = packet.ReadSingle("WalkSpeed", indexes);
            hotfix.RunSpeed = packet.ReadSingle("RunSpeed", indexes);
            hotfix.BloodID = packet.ReadUInt32("BloodID", indexes);
            hotfix.FootprintTextureID = packet.ReadUInt32("FootprintTextureID", indexes);
            hotfix.FootprintTextureLength = packet.ReadSingle("FootprintTextureLength", indexes);
            hotfix.FootprintTextureWidth = packet.ReadSingle("FootprintTextureWidth", indexes);
            hotfix.FootprintParticleScale = packet.ReadSingle("FootprintParticleScale", indexes);
            hotfix.FoleyMaterialID = packet.ReadUInt32("FoleyMaterialID", indexes);
            hotfix.FootstepCameraEffectID = packet.ReadUInt32("FootstepCameraEffectID", indexes);
            hotfix.DeathThudCameraEffectID = packet.ReadUInt32("DeathThudCameraEffectID", indexes);
            hotfix.SoundID = packet.ReadUInt32("SoundID", indexes);
            hotfix.SizeClass = packet.ReadSByte("SizeClass", indexes);
            hotfix.CollisionWidth = packet.ReadSingle("CollisionWidth", indexes);
            hotfix.CollisionHeight = packet.ReadSingle("CollisionHeight", indexes);
            hotfix.WorldEffectScale = packet.ReadSingle("WorldEffectScale", indexes);
            hotfix.CreatureGeosetDataID = packet.ReadUInt32("CreatureGeosetDataID", indexes);
            hotfix.HoverHeight = packet.ReadSingle("HoverHeight", indexes);
            hotfix.AttachedEffectScale = packet.ReadSingle("AttachedEffectScale", indexes);
            hotfix.ModelScale = packet.ReadSingle("ModelScale", indexes);
            hotfix.MissileCollisionRadius = packet.ReadSingle("MissileCollisionRadius", indexes);
            hotfix.MissileCollisionPush = packet.ReadSingle("MissileCollisionPush", indexes);
            hotfix.MissileCollisionRaise = packet.ReadSingle("MissileCollisionRaise", indexes);
            hotfix.MountHeight = packet.ReadSingle("MountHeight", indexes);
            hotfix.OverrideLootEffectScale = packet.ReadSingle("OverrideLootEffectScale", indexes);
            hotfix.OverrideNameScale = packet.ReadSingle("OverrideNameScale", indexes);
            hotfix.OverrideSelectionRadius = packet.ReadSingle("OverrideSelectionRadius", indexes);
            hotfix.TamedPetBaseScale = packet.ReadSingle("TamedPetBaseScale", indexes);
            hotfix.MountScaleOtherIndex = packet.ReadSByte("MountScaleOtherIndex", indexes);
            hotfix.MountScaleSelf = packet.ReadSingle("MountScaleSelf", indexes);
            hotfix.Unknown1100 = packet.ReadUInt16("Unknown1100", indexes);
            hotfix.MountScaleOther = new float?[2];
            for (int i = 0; i < 2; i++)
                hotfix.MountScaleOther[i] = packet.ReadSingle("MountScaleOther", indexes, i);

            Storage.CreatureModelDataHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void CreatureTypeHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            CreatureTypeHotfix1200 hotfix = new CreatureTypeHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.CreatureTypeHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CreatureTypeLocaleHotfix1200 hotfixLocale = new CreatureTypeLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.CreatureTypeHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CriteriaHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            CriteriaHotfix1200 hotfix = new CriteriaHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Type = packet.ReadInt16("Type", indexes);
            hotfix.Asset = packet.ReadInt32("Asset", indexes);
            hotfix.ModifierTreeId = packet.ReadUInt32("ModifierTreeId", indexes);
            hotfix.StartEvent = packet.ReadInt32("StartEvent", indexes);
            hotfix.StartAsset = packet.ReadInt32("StartAsset", indexes);
            hotfix.StartTimer = packet.ReadUInt16("StartTimer", indexes);
            hotfix.FailEvent = packet.ReadInt32("FailEvent", indexes);
            hotfix.FailAsset = packet.ReadInt32("FailAsset", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.EligibilityWorldStateID = packet.ReadInt16("EligibilityWorldStateID", indexes);
            hotfix.EligibilityWorldStateValue = packet.ReadSByte("EligibilityWorldStateValue", indexes);

            Storage.CriteriaHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void CriteriaTreeHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            CriteriaTreeHotfix1200 hotfix = new CriteriaTreeHotfix1200();

            hotfix.ID = entry;
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.Parent = packet.ReadUInt32("Parent", indexes);
            hotfix.Amount = packet.ReadUInt32("Amount", indexes);
            hotfix.Operator = packet.ReadInt32("Operator", indexes);
            hotfix.CriteriaID = packet.ReadUInt32("CriteriaID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.CriteriaTreeHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CriteriaTreeLocaleHotfix1200 hotfixLocale = new CriteriaTreeLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                };
                Storage.CriteriaTreeHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CurrencyContainerHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            CurrencyContainerHotfix1200 hotfix = new CurrencyContainerHotfix1200();

            hotfix.ID = entry;
            hotfix.ContainerName = packet.ReadCString("ContainerName", indexes);
            hotfix.ContainerDescription = packet.ReadCString("ContainerDescription", indexes);
            hotfix.MinAmount = packet.ReadInt32("MinAmount", indexes);
            hotfix.MaxAmount = packet.ReadInt32("MaxAmount", indexes);
            hotfix.ContainerIconID = packet.ReadInt32("ContainerIconID", indexes);
            hotfix.ContainerQuality = packet.ReadSByte("ContainerQuality", indexes);
            hotfix.OnLootSpellVisualKitID = packet.ReadInt32("OnLootSpellVisualKitID", indexes);
            hotfix.CurrencyTypesID = packet.ReadUInt32("CurrencyTypesID", indexes);

            Storage.CurrencyContainerHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CurrencyContainerLocaleHotfix1200 hotfixLocale = new CurrencyContainerLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    ContainerNameLang = hotfix.ContainerName,
                    ContainerDescriptionLang = hotfix.ContainerDescription,
                };
                Storage.CurrencyContainerHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CurrencyTypesHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            CurrencyTypesHotfix1200 hotfix = new CurrencyTypesHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.CategoryID = packet.ReadInt32("CategoryID", indexes);
            hotfix.InventoryIconFileID = packet.ReadInt32("InventoryIconFileID", indexes);
            hotfix.SpellWeight = packet.ReadUInt32("SpellWeight", indexes);
            hotfix.SpellCategory = packet.ReadByte("SpellCategory", indexes);
            hotfix.MaxQty = packet.ReadUInt32("MaxQty", indexes);
            hotfix.MaxEarnablePerWeek = packet.ReadUInt32("MaxEarnablePerWeek", indexes);
            hotfix.Quality = packet.ReadSByte("Quality", indexes);
            hotfix.FactionID = packet.ReadInt32("FactionID", indexes);
            hotfix.ItemGroupSoundsID = packet.ReadInt32("ItemGroupSoundsID", indexes);
            hotfix.XpQuestDifficulty = packet.ReadInt32("XpQuestDifficulty", indexes);
            hotfix.AwardConditionID = packet.ReadInt32("AwardConditionID", indexes);
            hotfix.MaxQtyWorldStateID = packet.ReadInt32("MaxQtyWorldStateID", indexes);
            hotfix.RechargingAmountPerCycle = packet.ReadUInt32("RechargingAmountPerCycle", indexes);
            hotfix.RechargingCycleDurationMS = packet.ReadUInt32("RechargingCycleDurationMS", indexes);
            hotfix.AccountTransferPercentage = packet.ReadSingle("AccountTransferPercentage", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);
            hotfix.RecraftReagentCountPercentage = packet.ReadSingle("RecraftReagentCountPercentage", indexes);
            hotfix.OrderSource = packet.ReadByte("OrderSource", indexes);
            hotfix.MCRCurrencyID = packet.ReadInt32("MCRCurrencyID", indexes);
            hotfix.Flags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.CurrencyTypesHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CurrencyTypesLocaleHotfix1200 hotfixLocale = new CurrencyTypesLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.CurrencyTypesHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CurveHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            CurveHotfix1200 hotfix = new CurveHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Type = packet.ReadByte("Type", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.CurveHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void CurvePointHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            CurvePointHotfix1200 hotfix = new CurvePointHotfix1200();

            hotfix.PosX = packet.ReadSingle("PosX", indexes);
            hotfix.PosY = packet.ReadSingle("PosY", indexes);
            hotfix.PreSLSquishPosX = packet.ReadSingle("PreSLSquishPosX", indexes);
            hotfix.PreSLSquishPosY = packet.ReadSingle("PreSLSquishPosY", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.CurveID = packet.ReadUInt32("CurveID", indexes);
            hotfix.OrderIndex = packet.ReadUInt32("OrderIndex", indexes);

            Storage.CurvePointHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void DestructibleModelDataHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            DestructibleModelDataHotfix1200 hotfix = new DestructibleModelDataHotfix1200();

            hotfix.ID = entry;
            hotfix.State0ImpactEffectDoodadSet = packet.ReadSByte("State0ImpactEffectDoodadSet", indexes);
            hotfix.State0AmbientDoodadSet = packet.ReadByte("State0AmbientDoodadSet", indexes);
            hotfix.State1Wmo = packet.ReadInt32("State1Wmo", indexes);
            hotfix.State1DestructionDoodadSet = packet.ReadSByte("State1DestructionDoodadSet", indexes);
            hotfix.State1ImpactEffectDoodadSet = packet.ReadSByte("State1ImpactEffectDoodadSet", indexes);
            hotfix.State1AmbientDoodadSet = packet.ReadByte("State1AmbientDoodadSet", indexes);
            hotfix.State2Wmo = packet.ReadInt32("State2Wmo", indexes);
            hotfix.State2DestructionDoodadSet = packet.ReadSByte("State2DestructionDoodadSet", indexes);
            hotfix.State2ImpactEffectDoodadSet = packet.ReadSByte("State2ImpactEffectDoodadSet", indexes);
            hotfix.State2AmbientDoodadSet = packet.ReadByte("State2AmbientDoodadSet", indexes);
            hotfix.State3Wmo = packet.ReadInt32("State3Wmo", indexes);
            hotfix.State3InitDoodadSet = packet.ReadByte("State3InitDoodadSet", indexes);
            hotfix.State3AmbientDoodadSet = packet.ReadByte("State3AmbientDoodadSet", indexes);
            hotfix.EjectDirection = packet.ReadByte("EjectDirection", indexes);
            hotfix.DoNotHighlight = packet.ReadByte("DoNotHighlight", indexes);
            hotfix.State0Wmo = packet.ReadInt32("State0Wmo", indexes);
            hotfix.HealEffect = packet.ReadByte("HealEffect", indexes);
            hotfix.HealEffectSpeed = packet.ReadUInt16("HealEffectSpeed", indexes);
            hotfix.State0NameSet = packet.ReadSByte("State0NameSet", indexes);
            hotfix.State1NameSet = packet.ReadSByte("State1NameSet", indexes);
            hotfix.State2NameSet = packet.ReadSByte("State2NameSet", indexes);
            hotfix.State3NameSet = packet.ReadSByte("State3NameSet", indexes);

            Storage.DestructibleModelDataHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void DifficultyHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            DifficultyHotfix1200 hotfix = new DifficultyHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.InstanceType = packet.ReadByte("InstanceType", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);
            hotfix.OldEnumValue = packet.ReadSByte("OldEnumValue", indexes);
            hotfix.FallbackDifficultyID = packet.ReadInt16("FallbackDifficultyID", indexes);
            hotfix.MinPlayers = packet.ReadByte("MinPlayers", indexes);
            hotfix.MaxPlayers = packet.ReadByte("MaxPlayers", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ItemContext = packet.ReadByte("ItemContext", indexes);
            hotfix.ToggleDifficultyID = packet.ReadInt16("ToggleDifficultyID", indexes);
            hotfix.GroupSizeHealthCurveID = packet.ReadUInt32("GroupSizeHealthCurveID", indexes);
            hotfix.GroupSizeDmgCurveID = packet.ReadUInt32("GroupSizeDmgCurveID", indexes);
            hotfix.GroupSizeSpellPointsCurveID = packet.ReadUInt32("GroupSizeSpellPointsCurveID", indexes);
            hotfix.Unknown1105 = packet.ReadInt32("Unknown1105", indexes);

            Storage.DifficultyHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                DifficultyLocaleHotfix1200 hotfixLocale = new DifficultyLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.DifficultyHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void DungeonEncounterHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            DungeonEncounterHotfix1200 hotfix = new DungeonEncounterHotfix1200();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.MapID = packet.ReadUInt16("MapID", indexes);
            hotfix.DifficultyID = packet.ReadInt16("DifficultyID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.CompleteWorldStateID = packet.ReadInt32("CompleteWorldStateID", indexes);
            hotfix.Bit = packet.ReadSByte("Bit", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.SpellIconFileID = packet.ReadInt32("SpellIconFileID", indexes);
            hotfix.Faction = packet.ReadInt32("Faction", indexes);
            hotfix.Unknown1115 = packet.ReadInt32("Unknown1115", indexes);

            Storage.DungeonEncounterHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                DungeonEncounterLocaleHotfix1200 hotfixLocale = new DungeonEncounterLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.DungeonEncounterHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void DurabilityCostsHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            DurabilityCostsHotfix1200 hotfix = new DurabilityCostsHotfix1200();

            hotfix.ID = entry;
            hotfix.WeaponSubClassCost = new ushort?[21];
            for (int i = 0; i < 21; i++)
                hotfix.WeaponSubClassCost[i] = packet.ReadUInt16("WeaponSubClassCost", indexes, i);
            hotfix.ArmorSubClassCost = new ushort?[8];
            for (int i = 0; i < 8; i++)
                hotfix.ArmorSubClassCost[i] = packet.ReadUInt16("ArmorSubClassCost", indexes, i);

            Storage.DurabilityCostsHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void DurabilityQualityHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            DurabilityQualityHotfix1200 hotfix = new DurabilityQualityHotfix1200();

            hotfix.ID = entry;
            hotfix.Data = packet.ReadSingle("Data", indexes);

            Storage.DurabilityQualityHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void EmotesHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            EmotesHotfix1200 hotfix = new EmotesHotfix1200();

            hotfix.ID = entry;
            hotfix.RaceMask = packet.ReadInt64("RaceMask", indexes);
            hotfix.EmoteSlashCommand = packet.ReadCString("EmoteSlashCommand", indexes);
            hotfix.AnimID = packet.ReadInt16("AnimID", indexes);
            hotfix.EmoteFlags = packet.ReadInt32("EmoteFlags", indexes);
            hotfix.EmoteSpecProc = packet.ReadInt32("EmoteSpecProc", indexes);
            hotfix.EmoteSpecProcParam = packet.ReadUInt32("EmoteSpecProcParam", indexes);
            hotfix.EventSoundID = packet.ReadUInt32("EventSoundID", indexes);
            hotfix.SpellVisualKitID = packet.ReadUInt32("SpellVisualKitID", indexes);
            hotfix.ClassMask = packet.ReadInt32("ClassMask", indexes);

            Storage.EmotesHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void EmotesTextHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            EmotesTextHotfix1200 hotfix = new EmotesTextHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.EmoteID = packet.ReadUInt16("EmoteID", indexes);

            Storage.EmotesTextHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void EmotesTextSoundHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            EmotesTextSoundHotfix1200 hotfix = new EmotesTextSoundHotfix1200();

            hotfix.ID = entry;
            hotfix.RaceID = packet.ReadSByte("RaceID", indexes);
            hotfix.ClassID = packet.ReadSByte("ClassID", indexes);
            hotfix.SexID = packet.ReadSByte("SexID", indexes);
            hotfix.SoundID = packet.ReadUInt32("SoundID", indexes);
            hotfix.EmotesTextID = packet.ReadUInt32("EmotesTextID", indexes);

            Storage.EmotesTextSoundHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ExpectedStatHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ExpectedStatHotfix1200 hotfix = new ExpectedStatHotfix1200();

            hotfix.ID = entry;
            hotfix.ExpansionID = packet.ReadInt32("ExpansionID", indexes);
            hotfix.CreatureHealth = packet.ReadSingle("CreatureHealth", indexes);
            hotfix.PlayerHealth = packet.ReadSingle("PlayerHealth", indexes);
            hotfix.CreatureAutoAttackDps = packet.ReadSingle("CreatureAutoAttackDps", indexes);
            hotfix.CreatureArmor = packet.ReadSingle("CreatureArmor", indexes);
            hotfix.PlayerMana = packet.ReadSingle("PlayerMana", indexes);
            hotfix.PlayerPrimaryStat = packet.ReadSingle("PlayerPrimaryStat", indexes);
            hotfix.PlayerSecondaryStat = packet.ReadSingle("PlayerSecondaryStat", indexes);
            hotfix.ArmorConstant = packet.ReadSingle("ArmorConstant", indexes);
            hotfix.CreatureSpellDamage = packet.ReadSingle("CreatureSpellDamage", indexes);
            hotfix.ContentSetID = packet.ReadInt32("ContentSetID", indexes);
            hotfix.Lvl = packet.ReadUInt32("Lvl", indexes);

            Storage.ExpectedStatHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ExpectedStatModHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ExpectedStatModHotfix1200 hotfix = new ExpectedStatModHotfix1200();

            hotfix.ID = entry;
            hotfix.CreatureHealthMod = packet.ReadSingle("CreatureHealthMod", indexes);
            hotfix.PlayerHealthMod = packet.ReadSingle("PlayerHealthMod", indexes);
            hotfix.CreatureAutoAttackDPSMod = packet.ReadSingle("CreatureAutoAttackDPSMod", indexes);
            hotfix.CreatureArmorMod = packet.ReadSingle("CreatureArmorMod", indexes);
            hotfix.PlayerManaMod = packet.ReadSingle("PlayerManaMod", indexes);
            hotfix.PlayerPrimaryStatMod = packet.ReadSingle("PlayerPrimaryStatMod", indexes);
            hotfix.PlayerSecondaryStatMod = packet.ReadSingle("PlayerSecondaryStatMod", indexes);
            hotfix.ArmorConstantMod = packet.ReadSingle("ArmorConstantMod", indexes);
            hotfix.CreatureSpellDamageMod = packet.ReadSingle("CreatureSpellDamageMod", indexes);

            Storage.ExpectedStatModHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void FactionHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            FactionHotfix1200 hotfix = new FactionHotfix1200();

            hotfix.ID = entry;
            hotfix.ReputationRaceMask = new long?[4];
            for (int i = 0; i < 4; i++)
                hotfix.ReputationRaceMask[i] = packet.ReadInt64("ReputationRaceMask", indexes, i);
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ReputationIndex = packet.ReadInt16("ReputationIndex", indexes);
            hotfix.ParentFactionID = packet.ReadUInt16("ParentFactionID", indexes);
            hotfix.Expansion = packet.ReadByte("Expansion", indexes);
            hotfix.FriendshipRepID = packet.ReadUInt32("FriendshipRepID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ParagonFactionID = packet.ReadUInt16("ParagonFactionID", indexes);
            hotfix.RenownFactionID = packet.ReadInt32("RenownFactionID", indexes);
            hotfix.RenownCurrencyID = packet.ReadInt32("RenownCurrencyID", indexes);
            hotfix.ReputationClassMask = new short?[4];
            for (int i = 0; i < 4; i++)
                hotfix.ReputationClassMask[i] = packet.ReadInt16("ReputationClassMask", indexes, i);
            hotfix.ReputationFlags = new ushort?[4];
            for (int i = 0; i < 4; i++)
                hotfix.ReputationFlags[i] = packet.ReadUInt16("ReputationFlags", indexes, i);
            hotfix.ReputationBase = new int?[4];
            for (int i = 0; i < 4; i++)
                hotfix.ReputationBase[i] = packet.ReadInt32("ReputationBase", indexes, i);
            hotfix.ReputationMax = new int?[4];
            for (int i = 0; i < 4; i++)
                hotfix.ReputationMax[i] = packet.ReadInt32("ReputationMax", indexes, i);
            hotfix.ParentFactionMod = new float?[2];
            for (int i = 0; i < 2; i++)
                hotfix.ParentFactionMod[i] = packet.ReadSingle("ParentFactionMod", indexes, i);
            hotfix.ParentFactionCap = new byte?[2];
            for (int i = 0; i < 2; i++)
                hotfix.ParentFactionCap[i] = packet.ReadByte("ParentFactionCap", indexes, i);

            Storage.FactionHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                FactionLocaleHotfix1200 hotfixLocale = new FactionLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.FactionHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void FactionTemplateHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            FactionTemplateHotfix1200 hotfix = new FactionTemplateHotfix1200();

            hotfix.ID = entry;
            hotfix.Faction = packet.ReadUInt16("Faction", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.FactionGroup = packet.ReadByte("FactionGroup", indexes);
            hotfix.FriendGroup = packet.ReadByte("FriendGroup", indexes);
            hotfix.EnemyGroup = packet.ReadByte("EnemyGroup", indexes);
            hotfix.Enemies = new ushort?[8];
            for (int i = 0; i < 8; i++)
                hotfix.Enemies[i] = packet.ReadUInt16("Enemies", indexes, i);
            hotfix.Friend = new ushort?[8];
            for (int i = 0; i < 8; i++)
                hotfix.Friend[i] = packet.ReadUInt16("Friend", indexes, i);

            Storage.FactionTemplateHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void FlightCapabilityHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            FlightCapabilityHotfix1200 hotfix = new FlightCapabilityHotfix1200();

            hotfix.ID = entry;
            hotfix.AirFriction = packet.ReadSingle("AirFriction", indexes);
            hotfix.MaxVel = packet.ReadSingle("MaxVel", indexes);
            hotfix.Unknown1000_2 = packet.ReadSingle("Unknown1000_2", indexes);
            hotfix.DoubleJumpVelMod = packet.ReadSingle("DoubleJumpVelMod", indexes);
            hotfix.LiftCoefficient = packet.ReadSingle("LiftCoefficient", indexes);
            hotfix.GlideStartMinHeight = packet.ReadSingle("GlideStartMinHeight", indexes);
            hotfix.AddImpulseMaxSpeed = packet.ReadSingle("AddImpulseMaxSpeed", indexes);
            hotfix.BankingRateMin = packet.ReadSingle("BankingRateMin", indexes);
            hotfix.BankingRateMax = packet.ReadSingle("BankingRateMax", indexes);
            hotfix.PitchingRateDownMin = packet.ReadSingle("PitchingRateDownMin", indexes);
            hotfix.PitchingRateDownMax = packet.ReadSingle("PitchingRateDownMax", indexes);
            hotfix.PitchingRateUpMin = packet.ReadSingle("PitchingRateUpMin", indexes);
            hotfix.PitchingRateUpMax = packet.ReadSingle("PitchingRateUpMax", indexes);
            hotfix.TurnVelocityThresholdMin = packet.ReadSingle("TurnVelocityThresholdMin", indexes);
            hotfix.TurnVelocityThresholdMax = packet.ReadSingle("TurnVelocityThresholdMax", indexes);
            hotfix.SurfaceFriction = packet.ReadSingle("SurfaceFriction", indexes);
            hotfix.OverMaxDeceleration = packet.ReadSingle("OverMaxDeceleration", indexes);
            hotfix.Unknown1000_17 = packet.ReadSingle("Unknown1000_17", indexes);
            hotfix.Unknown1000_18 = packet.ReadSingle("Unknown1000_18", indexes);
            hotfix.Unknown1000_19 = packet.ReadSingle("Unknown1000_19", indexes);
            hotfix.Unknown1000_20 = packet.ReadSingle("Unknown1000_20", indexes);
            hotfix.Unknown1000_21 = packet.ReadSingle("Unknown1000_21", indexes);
            hotfix.LaunchSpeedCoefficient = packet.ReadSingle("LaunchSpeedCoefficient", indexes);
            hotfix.VigorRegenMaxVelCoefficient = packet.ReadSingle("VigorRegenMaxVelCoefficient", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);

            Storage.FlightCapabilityHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void FriendshipRepReactionHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            FriendshipRepReactionHotfix1200 hotfix = new FriendshipRepReactionHotfix1200();

            hotfix.ID = entry;
            hotfix.Reaction = packet.ReadCString("Reaction", indexes);
            hotfix.FriendshipRepID = packet.ReadUInt32("FriendshipRepID", indexes);
            hotfix.ReactionThreshold = packet.ReadInt32("ReactionThreshold", indexes);
            hotfix.OverrideColor = packet.ReadInt32("OverrideColor", indexes);

            Storage.FriendshipRepReactionHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                FriendshipRepReactionLocaleHotfix1200 hotfixLocale = new FriendshipRepReactionLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    ReactionLang = hotfix.Reaction,
                };
                Storage.FriendshipRepReactionHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void FriendshipReputationHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            FriendshipReputationHotfix1200 hotfix = new FriendshipReputationHotfix1200();

            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.StandingModified = packet.ReadCString("StandingModified", indexes);
            hotfix.StandingChanged = packet.ReadCString("StandingChanged", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.FactionID = packet.ReadInt32("FactionID", indexes);
            hotfix.TextureFileID = packet.ReadInt32("TextureFileID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.FriendshipReputationHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                FriendshipReputationLocaleHotfix1200 hotfixLocale = new FriendshipReputationLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                    StandingModifiedLang = hotfix.StandingModified,
                    StandingChangedLang = hotfix.StandingChanged,
                };
                Storage.FriendshipReputationHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void GameobjectArtKitHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GameobjectArtKitHotfix1200 hotfix = new GameobjectArtKitHotfix1200();

            hotfix.ID = entry;
            hotfix.AttachModelFileID = packet.ReadInt32("AttachModelFileID", indexes);
            hotfix.TextureVariationFileID = new int?[3];
            for (int i = 0; i < 3; i++)
                hotfix.TextureVariationFileID[i] = packet.ReadInt32("TextureVariationFileID", indexes, i);

            Storage.GameobjectArtKitHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void GameobjectDisplayInfoHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GameobjectDisplayInfoHotfix1200 hotfix = new GameobjectDisplayInfoHotfix1200();

            hotfix.ID = entry;
            hotfix.GeoBoxMinX = packet.ReadSingle("GeoBoxMinX", indexes);
            hotfix.GeoBoxMinY = packet.ReadSingle("GeoBoxMinY", indexes);
            hotfix.GeoBoxMinZ = packet.ReadSingle("GeoBoxMinZ", indexes);
            hotfix.GeoBoxMaxX = packet.ReadSingle("GeoBoxMaxX", indexes);
            hotfix.GeoBoxMaxY = packet.ReadSingle("GeoBoxMaxY", indexes);
            hotfix.GeoBoxMaxZ = packet.ReadSingle("GeoBoxMaxZ", indexes);
            hotfix.FileDataID = packet.ReadInt32("FileDataID", indexes);
            hotfix.ObjectEffectPackageID = packet.ReadInt16("ObjectEffectPackageID", indexes);
            hotfix.OverrideLootEffectScale = packet.ReadSingle("OverrideLootEffectScale", indexes);
            hotfix.OverrideNameScale = packet.ReadSingle("OverrideNameScale", indexes);
            hotfix.AlternateDisplayType = packet.ReadInt32("AlternateDisplayType", indexes);
            hotfix.ClientCreatureDisplayInfoID = packet.ReadInt32("ClientCreatureDisplayInfoID", indexes);
            hotfix.ClientItemID = packet.ReadInt32("ClientItemID", indexes);
            hotfix.Unknown1100 = packet.ReadUInt16("Unknown1100", indexes);
            Storage.GameobjectDisplayInfoHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void GameobjectLabelHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GameobjectLabelHotfix1200 hotfix = new GameobjectLabelHotfix1200();

            hotfix.ID = entry;
            hotfix.LabelID = packet.ReadInt32("LabelID", indexes);
            hotfix.GameObjectID = packet.ReadUInt32("GameObjectID", indexes);
            Storage.GameobjectLabelHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void GameobjectsHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GameobjectsHotfix1200 hotfix = new GameobjectsHotfix1200();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.PosX = packet.ReadSingle("PosX", indexes);
            hotfix.PosY = packet.ReadSingle("PosY", indexes);
            hotfix.PosZ = packet.ReadSingle("PosZ", indexes);
            hotfix.Rot = new float?[4];
            for (int i = 0; i < 4; i++)
                hotfix.Rot[i] = packet.ReadSingle("Rot", indexes, i);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.OwnerID = packet.ReadUInt32("OwnerID", indexes);
            hotfix.DisplayID = packet.ReadInt32("DisplayID", indexes);
            hotfix.Scale = packet.ReadSingle("Scale", indexes);
            hotfix.TypeID = packet.ReadInt32("TypeID", indexes);
            hotfix.PhaseUseFlags = packet.ReadInt32("PhaseUseFlags", indexes);
            hotfix.PhaseID = packet.ReadInt32("PhaseID", indexes);
            hotfix.PhaseGroupID = packet.ReadInt32("PhaseGroupID", indexes);
            hotfix.Unknown1100 = packet.ReadUInt16("Unknown1100", indexes);
            hotfix.PropValue = new int?[8];
            for (int i = 0; i < 8; i++)
                hotfix.PropValue[i] = packet.ReadInt32("PropValue", indexes, i);

            Storage.GameobjectsHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                GameobjectsLocaleHotfix1200 hotfixLocale = new GameobjectsLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.GameobjectsHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void GarrAbilityHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GarrAbilityHotfix1200 hotfix = new GarrAbilityHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.GarrAbilityCategoryID = packet.ReadByte("GarrAbilityCategoryID", indexes);
            hotfix.GarrFollowerTypeID = packet.ReadSByte("GarrFollowerTypeID", indexes);
            hotfix.IconFileDataID = packet.ReadInt32("IconFileDataID", indexes);
            hotfix.FactionChangeGarrAbilityID = packet.ReadUInt16("FactionChangeGarrAbilityID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.GarrAbilityHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                GarrAbilityLocaleHotfix1200 hotfixLocale = new GarrAbilityLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.GarrAbilityHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void GarrBuildingHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GarrBuildingHotfix1200 hotfix = new GarrBuildingHotfix1200();

            hotfix.ID = entry;
            hotfix.HordeName = packet.ReadCString("HordeName", indexes);
            hotfix.AllianceName = packet.ReadCString("AllianceName", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.Tooltip = packet.ReadCString("Tooltip", indexes);
            hotfix.GarrTypeID = packet.ReadSByte("GarrTypeID", indexes);
            hotfix.BuildingType = packet.ReadByte("BuildingType", indexes);
            hotfix.HordeGameObjectID = packet.ReadInt32("HordeGameObjectID", indexes);
            hotfix.AllianceGameObjectID = packet.ReadInt32("AllianceGameObjectID", indexes);
            hotfix.GarrSiteID = packet.ReadInt32("GarrSiteID", indexes);
            hotfix.UpgradeLevel = packet.ReadByte("UpgradeLevel", indexes);
            hotfix.BuildSeconds = packet.ReadInt32("BuildSeconds", indexes);
            hotfix.CurrencyTypeID = packet.ReadUInt16("CurrencyTypeID", indexes);
            hotfix.CurrencyQty = packet.ReadInt32("CurrencyQty", indexes);
            hotfix.HordeUiTextureKitID = packet.ReadUInt16("HordeUiTextureKitID", indexes);
            hotfix.AllianceUiTextureKitID = packet.ReadUInt16("AllianceUiTextureKitID", indexes);
            hotfix.IconFileDataID = packet.ReadInt32("IconFileDataID", indexes);
            hotfix.AllianceSceneScriptPackageID = packet.ReadUInt16("AllianceSceneScriptPackageID", indexes);
            hotfix.HordeSceneScriptPackageID = packet.ReadUInt16("HordeSceneScriptPackageID", indexes);
            hotfix.MaxAssignments = packet.ReadInt32("MaxAssignments", indexes);
            hotfix.ShipmentCapacity = packet.ReadByte("ShipmentCapacity", indexes);
            hotfix.GarrAbilityID = packet.ReadUInt16("GarrAbilityID", indexes);
            hotfix.BonusGarrAbilityID = packet.ReadUInt16("BonusGarrAbilityID", indexes);
            hotfix.GoldCost = packet.ReadUInt16("GoldCost", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.GarrBuildingHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                GarrBuildingLocaleHotfix1200 hotfixLocale = new GarrBuildingLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    HordeNameLang = hotfix.HordeName,
                    AllianceNameLang = hotfix.AllianceName,
                    DescriptionLang = hotfix.Description,
                    TooltipLang = hotfix.Tooltip,
                };
                Storage.GarrBuildingHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void GarrBuildingPlotInstHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GarrBuildingPlotInstHotfix1200 hotfix = new GarrBuildingPlotInstHotfix1200();

            hotfix.MapOffsetX = packet.ReadSingle("MapOffsetX", indexes);
            hotfix.MapOffsetY = packet.ReadSingle("MapOffsetY", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.GarrBuildingID = packet.ReadByte("GarrBuildingID", indexes);
            hotfix.GarrSiteLevelPlotInstID = packet.ReadUInt16("GarrSiteLevelPlotInstID", indexes);
            hotfix.UiTextureAtlasMemberID = packet.ReadUInt16("UiTextureAtlasMemberID", indexes);

            Storage.GarrBuildingPlotInstHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void GarrClassSpecHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GarrClassSpecHotfix1200 hotfix = new GarrClassSpecHotfix1200();

            hotfix.ID = entry;
            hotfix.ClassSpec = packet.ReadCString("ClassSpec", indexes);
            hotfix.ClassSpecMale = packet.ReadCString("ClassSpecMale", indexes);
            hotfix.ClassSpecFemale = packet.ReadCString("ClassSpecFemale", indexes);
            hotfix.UiTextureAtlasMemberID = packet.ReadUInt16("UiTextureAtlasMemberID", indexes);
            hotfix.GarrFollItemSetID = packet.ReadUInt16("GarrFollItemSetID", indexes);
            hotfix.FollowerClassLimit = packet.ReadByte("FollowerClassLimit", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.GarrClassSpecHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                GarrClassSpecLocaleHotfix1200 hotfixLocale = new GarrClassSpecLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    ClassSpecLang = hotfix.ClassSpec,
                    ClassSpecMaleLang = hotfix.ClassSpecMale,
                    ClassSpecFemaleLang = hotfix.ClassSpecFemale,
                };
                Storage.GarrClassSpecHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void GarrFollowerHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GarrFollowerHotfix1200 hotfix = new GarrFollowerHotfix1200();

            hotfix.ID = entry;
            hotfix.HordeSourceText = packet.ReadCString("HordeSourceText", indexes);
            hotfix.AllianceSourceText = packet.ReadCString("AllianceSourceText", indexes);
            hotfix.TitleName = packet.ReadCString("TitleName", indexes);
            hotfix.GarrTypeID = packet.ReadSByte("GarrTypeID", indexes);
            hotfix.GarrFollowerTypeID = packet.ReadSByte("GarrFollowerTypeID", indexes);
            hotfix.HordeCreatureID = packet.ReadInt32("HordeCreatureID", indexes);
            hotfix.AllianceCreatureID = packet.ReadInt32("AllianceCreatureID", indexes);
            hotfix.HordeGarrFollRaceID = packet.ReadByte("HordeGarrFollRaceID", indexes);
            hotfix.AllianceGarrFollRaceID = packet.ReadByte("AllianceGarrFollRaceID", indexes);
            hotfix.HordeGarrClassSpecID = packet.ReadInt32("HordeGarrClassSpecID", indexes);
            hotfix.AllianceGarrClassSpecID = packet.ReadInt32("AllianceGarrClassSpecID", indexes);
            hotfix.Quality = packet.ReadInt32("Quality", indexes);
            hotfix.FollowerLevel = packet.ReadByte("FollowerLevel", indexes);
            hotfix.ItemLevelWeapon = packet.ReadUInt16("ItemLevelWeapon", indexes);
            hotfix.ItemLevelArmor = packet.ReadUInt16("ItemLevelArmor", indexes);
            hotfix.HordeSourceTypeEnum = packet.ReadSByte("HordeSourceTypeEnum", indexes);
            hotfix.AllianceSourceTypeEnum = packet.ReadSByte("AllianceSourceTypeEnum", indexes);
            hotfix.HordeIconFileDataID = packet.ReadInt32("HordeIconFileDataID", indexes);
            hotfix.AllianceIconFileDataID = packet.ReadInt32("AllianceIconFileDataID", indexes);
            hotfix.HordeGarrFollItemSetID = packet.ReadUInt16("HordeGarrFollItemSetID", indexes);
            hotfix.AllianceGarrFollItemSetID = packet.ReadUInt16("AllianceGarrFollItemSetID", indexes);
            hotfix.HordeUITextureKitID = packet.ReadUInt16("HordeUITextureKitID", indexes);
            hotfix.AllianceUITextureKitID = packet.ReadUInt16("AllianceUITextureKitID", indexes);
            hotfix.Vitality = packet.ReadByte("Vitality", indexes);
            hotfix.HordeFlavorGarrStringID = packet.ReadByte("HordeFlavorGarrStringID", indexes);
            hotfix.AllianceFlavorGarrStringID = packet.ReadByte("AllianceFlavorGarrStringID", indexes);
            hotfix.HordeSlottingBroadcastTextID = packet.ReadUInt32("HordeSlottingBroadcastTextID", indexes);
            hotfix.AllySlottingBroadcastTextID = packet.ReadUInt32("AllySlottingBroadcastTextID", indexes);
            hotfix.ChrClassID = packet.ReadByte("ChrClassID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.Gender = packet.ReadSByte("Gender", indexes);
            hotfix.AutoCombatantID = packet.ReadInt32("AutoCombatantID", indexes);
            hotfix.CovenantID = packet.ReadInt32("CovenantID", indexes);

            Storage.GarrFollowerHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                GarrFollowerLocaleHotfix1200 hotfixLocale = new GarrFollowerLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    HordeSourceTextLang = hotfix.HordeSourceText,
                    AllianceSourceTextLang = hotfix.AllianceSourceText,
                    TitleNameLang = hotfix.TitleName,
                };
                Storage.GarrFollowerHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void GarrFollowerXAbilityHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GarrFollowerXAbilityHotfix1200 hotfix = new GarrFollowerXAbilityHotfix1200();

            hotfix.ID = entry;
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);
            hotfix.FactionIndex = packet.ReadByte("FactionIndex", indexes);
            hotfix.GarrAbilityID = packet.ReadUInt16("GarrAbilityID", indexes);
            hotfix.GarrFollowerID = packet.ReadUInt32("GarrFollowerID", indexes);

            Storage.GarrFollowerXAbilityHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void GarrMissionHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GarrMissionHotfix1200 hotfix = new GarrMissionHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Location = packet.ReadCString("Location", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.MapPosX = packet.ReadSingle("MapPosX", indexes);
            hotfix.MapPosY = packet.ReadSingle("MapPosY", indexes);
            hotfix.WorldPosX = packet.ReadSingle("WorldPosX", indexes);
            hotfix.WorldPosY = packet.ReadSingle("WorldPosY", indexes);
            hotfix.GarrTypeID = packet.ReadSByte("GarrTypeID", indexes);
            hotfix.GarrMissionTypeID = packet.ReadByte("GarrMissionTypeID", indexes);
            hotfix.GarrFollowerTypeID = packet.ReadSByte("GarrFollowerTypeID", indexes);
            hotfix.MaxFollowers = packet.ReadByte("MaxFollowers", indexes);
            hotfix.MissionCost = packet.ReadUInt32("MissionCost", indexes);
            hotfix.MissionCostCurrencyTypesID = packet.ReadUInt16("MissionCostCurrencyTypesID", indexes);
            hotfix.OfferedGarrMissionTextureID = packet.ReadByte("OfferedGarrMissionTextureID", indexes);
            hotfix.UiTextureKitID = packet.ReadUInt16("UiTextureKitID", indexes);
            hotfix.EnvGarrMechanicID = packet.ReadUInt32("EnvGarrMechanicID", indexes);
            hotfix.EnvGarrMechanicTypeID = packet.ReadInt32("EnvGarrMechanicTypeID", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.GarrMissionSetID = packet.ReadUInt32("GarrMissionSetID", indexes);
            hotfix.TargetLevel = packet.ReadSByte("TargetLevel", indexes);
            hotfix.TargetItemLevel = packet.ReadUInt16("TargetItemLevel", indexes);
            hotfix.MissionDuration = packet.ReadInt32("MissionDuration", indexes);
            hotfix.TravelDuration = packet.ReadInt32("TravelDuration", indexes);
            hotfix.OfferDuration = packet.ReadUInt32("OfferDuration", indexes);
            hotfix.BaseCompletionChance = packet.ReadByte("BaseCompletionChance", indexes);
            hotfix.BaseFollowerXP = packet.ReadUInt32("BaseFollowerXP", indexes);
            hotfix.OvermaxRewardPackID = packet.ReadUInt32("OvermaxRewardPackID", indexes);
            hotfix.FollowerDeathChance = packet.ReadByte("FollowerDeathChance", indexes);
            hotfix.AreaID = packet.ReadUInt32("AreaID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.AutoMissionScalar = packet.ReadSingle("AutoMissionScalar", indexes);
            hotfix.AutoMissionScalarCurveID = packet.ReadInt32("AutoMissionScalarCurveID", indexes);
            hotfix.AutoCombatantEnvCasterID = packet.ReadInt32("AutoCombatantEnvCasterID", indexes);

            Storage.GarrMissionHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                GarrMissionLocaleHotfix1200 hotfixLocale = new GarrMissionLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    LocationLang = hotfix.Location,
                    DescriptionLang = hotfix.Description,
                };
                Storage.GarrMissionHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void GarrPlotHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GarrPlotHotfix1200 hotfix = new GarrPlotHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.PlotType = packet.ReadByte("PlotType", indexes);
            hotfix.HordeConstructObjID = packet.ReadInt32("HordeConstructObjID", indexes);
            hotfix.AllianceConstructObjID = packet.ReadInt32("AllianceConstructObjID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.UpgradeRequirement = new uint?[2];
            for (int i = 0; i < 2; i++)
                hotfix.UpgradeRequirement[i] = packet.ReadUInt32("UpgradeRequirement", indexes, i);

            Storage.GarrPlotHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void GarrPlotBuildingHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GarrPlotBuildingHotfix1200 hotfix = new GarrPlotBuildingHotfix1200();

            hotfix.ID = entry;
            hotfix.GarrPlotID = packet.ReadByte("GarrPlotID", indexes);
            hotfix.GarrBuildingID = packet.ReadByte("GarrBuildingID", indexes);

            Storage.GarrPlotBuildingHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void GarrPlotInstanceHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GarrPlotInstanceHotfix1200 hotfix = new GarrPlotInstanceHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.GarrPlotID = packet.ReadByte("GarrPlotID", indexes);

            Storage.GarrPlotInstanceHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void GarrSiteLevelHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GarrSiteLevelHotfix1200 hotfix = new GarrSiteLevelHotfix1200();

            hotfix.ID = entry;
            hotfix.TownHallUiPosX = packet.ReadSingle("TownHallUiPosX", indexes);
            hotfix.TownHallUiPosY = packet.ReadSingle("TownHallUiPosY", indexes);
            hotfix.GarrSiteID = packet.ReadUInt32("GarrSiteID", indexes);
            hotfix.GarrLevel = packet.ReadByte("GarrLevel", indexes);
            hotfix.MapID = packet.ReadUInt16("MapID", indexes);
            hotfix.UpgradeMovieID = packet.ReadUInt16("UpgradeMovieID", indexes);
            hotfix.UiTextureKitID = packet.ReadUInt16("UiTextureKitID", indexes);
            hotfix.MaxBuildingLevel = packet.ReadByte("MaxBuildingLevel", indexes);
            hotfix.UpgradeCost = packet.ReadUInt16("UpgradeCost", indexes);
            hotfix.UpgradeGoldCost = packet.ReadUInt16("UpgradeGoldCost", indexes);

            Storage.GarrSiteLevelHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void GarrSiteLevelPlotInstHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GarrSiteLevelPlotInstHotfix1200 hotfix = new GarrSiteLevelPlotInstHotfix1200();

            hotfix.ID = entry;
            hotfix.UiMarkerPosX = packet.ReadSingle("UiMarkerPosX", indexes);
            hotfix.UiMarkerPosY = packet.ReadSingle("UiMarkerPosY", indexes);
            hotfix.GarrSiteLevelID = packet.ReadUInt16("GarrSiteLevelID", indexes);
            hotfix.GarrPlotInstanceID = packet.ReadByte("GarrPlotInstanceID", indexes);
            hotfix.UiMarkerSize = packet.ReadByte("UiMarkerSize", indexes);

            Storage.GarrSiteLevelPlotInstHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void GarrTalentTreeHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GarrTalentTreeHotfix1200 hotfix = new GarrTalentTreeHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.GarrTypeID = packet.ReadSByte("GarrTypeID", indexes);
            hotfix.ClassID = packet.ReadInt32("ClassID", indexes);
            hotfix.MaxTiers = packet.ReadSByte("MaxTiers", indexes);
            hotfix.UiOrder = packet.ReadSByte("UiOrder", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.UiTextureKitID = packet.ReadUInt16("UiTextureKitID", indexes);
            hotfix.GarrTalentTreeType = packet.ReadInt32("GarrTalentTreeType", indexes);
            hotfix.PlayerConditionID = packet.ReadInt32("PlayerConditionID", indexes);
            hotfix.FeatureTypeIndex = packet.ReadByte("FeatureTypeIndex", indexes);
            hotfix.FeatureSubtypeIndex = packet.ReadByte("FeatureSubtypeIndex", indexes);
            hotfix.CurrencyID = packet.ReadInt32("CurrencyID", indexes);

            Storage.GarrTalentTreeHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                GarrTalentTreeLocaleHotfix1200 hotfixLocale = new GarrTalentTreeLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.GarrTalentTreeHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void GemPropertiesHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GemPropertiesHotfix1200 hotfix = new GemPropertiesHotfix1200();

            hotfix.ID = entry;
            hotfix.EnchantId = packet.ReadUInt16("EnchantId", indexes);
            hotfix.Type = packet.ReadInt32("Type", indexes);

            Storage.GemPropertiesHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void GlobalCurveHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GlobalCurveHotfix1200 hotfix = new GlobalCurveHotfix1200();

            hotfix.ID = entry;
            hotfix.CurveID = packet.ReadInt32("CurveID", indexes);
            hotfix.Type = packet.ReadInt32("Type", indexes);

            Storage.GlobalCurveHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void GlyphBindableSpellHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GlyphBindableSpellHotfix1200 hotfix = new GlyphBindableSpellHotfix1200();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.GlyphPropertiesID = packet.ReadUInt32("GlyphPropertiesID", indexes);

            Storage.GlyphBindableSpellHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void GlyphPropertiesHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GlyphPropertiesHotfix1200 hotfix = new GlyphPropertiesHotfix1200();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadUInt32("SpellID", indexes);
            hotfix.GlyphType = packet.ReadByte("GlyphType", indexes);
            hotfix.GlyphExclusiveCategoryID = packet.ReadByte("GlyphExclusiveCategoryID", indexes);
            hotfix.SpellIconFileDataID = packet.ReadInt32("SpellIconFileDataID", indexes);

            Storage.GlyphPropertiesHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void GlyphRequiredSpecHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GlyphRequiredSpecHotfix1200 hotfix = new GlyphRequiredSpecHotfix1200();

            hotfix.ID = entry;
            hotfix.ChrSpecializationID = packet.ReadUInt16("ChrSpecializationID", indexes);
            hotfix.GlyphPropertiesID = packet.ReadUInt32("GlyphPropertiesID", indexes);

            Storage.GlyphRequiredSpecHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void GossipNpcOptionHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GossipNpcOptionHotfix1200 hotfix = new GossipNpcOptionHotfix1200();

            hotfix.ID = entry;
            hotfix.GossipNpcOption = packet.ReadInt32("GossipNpcOption", indexes);
            hotfix.LFGDungeonsID = packet.ReadInt32("LFGDungeonsID", indexes);
            hotfix.TrainerID = packet.ReadInt32("TrainerID", indexes);
            hotfix.GarrFollowerTypeID = packet.ReadSByte("GarrFollowerTypeID", indexes);
            hotfix.CharShipmentID = packet.ReadInt32("CharShipmentID", indexes);
            hotfix.GarrTalentTreeID = packet.ReadInt32("GarrTalentTreeID", indexes);
            hotfix.UiMapID = packet.ReadInt32("UiMapID", indexes);
            hotfix.UiItemInteractionID = packet.ReadInt32("UiItemInteractionID", indexes);
            hotfix.Unknown_1000_8 = packet.ReadInt32("Unknown_1000_8", indexes);
            hotfix.Unknown_1000_9 = packet.ReadInt32("Unknown_1000_9", indexes);
            hotfix.CovenantID = packet.ReadInt32("CovenantID", indexes);
            hotfix.GossipOptionID = packet.ReadInt32("GossipOptionID", indexes);
            hotfix.TraitTreeID = packet.ReadInt32("TraitTreeID", indexes);
            hotfix.ProfessionID = packet.ReadInt32("ProfessionID", indexes);
            hotfix.Unknown_1002_14 = packet.ReadInt32("Unknown_1002_14", indexes);
            hotfix.NeighborhoodMapID = packet.ReadInt32("NeighborhoodMapID", indexes);
            hotfix.SkillLineID = packet.ReadInt32("SkillLineID", indexes);

            Storage.GossipNPCOptionHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void GuildColorBackgroundHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GuildColorBackgroundHotfix1200 hotfix = new GuildColorBackgroundHotfix1200();

            hotfix.ID = entry;
            hotfix.Red = packet.ReadByte("Red", indexes);
            hotfix.Blue = packet.ReadByte("Blue", indexes);
            hotfix.Green = packet.ReadByte("Green", indexes);

            Storage.GuildColorBackgroundHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void GuildColorBorderHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GuildColorBorderHotfix1200 hotfix = new GuildColorBorderHotfix1200();

            hotfix.ID = entry;
            hotfix.Red = packet.ReadByte("Red", indexes);
            hotfix.Blue = packet.ReadByte("Blue", indexes);
            hotfix.Green = packet.ReadByte("Green", indexes);

            Storage.GuildColorBorderHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void GuildColorEmblemHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GuildColorEmblemHotfix1200 hotfix = new GuildColorEmblemHotfix1200();

            hotfix.ID = entry;
            hotfix.Red = packet.ReadByte("Red", indexes);
            hotfix.Blue = packet.ReadByte("Blue", indexes);
            hotfix.Green = packet.ReadByte("Green", indexes);

            Storage.GuildColorEmblemHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void GuildPerkSpellsHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            GuildPerkSpellsHotfix1200 hotfix = new GuildPerkSpellsHotfix1200();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);

            Storage.GuildPerkSpellsHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void HeirloomHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            HeirloomHotfix1200 hotfix = new HeirloomHotfix1200();

            hotfix.SourceText = packet.ReadCString("SourceText", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);
            hotfix.LegacyUpgradedItemID = packet.ReadInt32("LegacyUpgradedItemID", indexes);
            hotfix.StaticUpgradedItemID = packet.ReadInt32("StaticUpgradedItemID", indexes);
            hotfix.SourceTypeEnum = packet.ReadSByte("SourceTypeEnum", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.LegacyItemID = packet.ReadInt32("LegacyItemID", indexes);
            hotfix.UpgradeItemID = new int?[6];
            for (int i = 0; i < 6; i++)
                hotfix.UpgradeItemID[i] = packet.ReadInt32("UpgradeItemID", indexes, i);
            hotfix.UpgradeItemBonusListID = new ushort?[6];
            for (int i = 0; i < 6; i++)
                hotfix.UpgradeItemBonusListID[i] = packet.ReadUInt16("UpgradeItemBonusListID", indexes, i);

            Storage.HeirloomHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                HeirloomLocaleHotfix1200 hotfixLocale = new HeirloomLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    SourceTextLang = hotfix.SourceText,
                };
                Storage.HeirloomHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void HolidaysHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            HolidaysHotfix1200 hotfix = new HolidaysHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Region = packet.ReadUInt16("Region", indexes);
            hotfix.Looping = packet.ReadByte("Looping", indexes);
            hotfix.HolidayNameID = packet.ReadUInt32("HolidayNameID", indexes);
            hotfix.HolidayDescriptionID = packet.ReadUInt32("HolidayDescriptionID", indexes);
            hotfix.Priority = packet.ReadByte("Priority", indexes);
            hotfix.CalendarFilterType = packet.ReadByte("CalendarFilterType", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.Duration = new ushort?[10];
            for (int i = 0; i < 10; i++)
                hotfix.Duration[i] = packet.ReadUInt16("Duration", indexes, i);
            hotfix.Date = new uint?[26];
            for (int i = 0; i < 26; i++)
                hotfix.Date[i] = packet.ReadUInt32("Date", indexes, i);
            hotfix.CalendarFlags = new byte?[10];
            for (int i = 0; i < 10; i++)
                hotfix.CalendarFlags[i] = packet.ReadByte("CalendarFlags", indexes, i);
            hotfix.TextureFileDataID = new int?[3];
            for (int i = 0; i < 3; i++)
                hotfix.TextureFileDataID[i] = packet.ReadInt32("TextureFileDataID", indexes, i);

            Storage.HolidaysHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ImportPriceArmorHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ImportPriceArmorHotfix1200 hotfix = new ImportPriceArmorHotfix1200();

            hotfix.ID = entry;
            hotfix.ClothModifier = packet.ReadSingle("ClothModifier", indexes);
            hotfix.LeatherModifier = packet.ReadSingle("LeatherModifier", indexes);
            hotfix.ChainModifier = packet.ReadSingle("ChainModifier", indexes);
            hotfix.PlateModifier = packet.ReadSingle("PlateModifier", indexes);

            Storage.ImportPriceArmorHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ImportPriceQualityHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ImportPriceQualityHotfix1200 hotfix = new ImportPriceQualityHotfix1200();

            hotfix.ID = entry;
            hotfix.Data = packet.ReadSingle("Data", indexes);

            Storage.ImportPriceQualityHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ImportPriceShieldHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ImportPriceShieldHotfix1200 hotfix = new ImportPriceShieldHotfix1200();

            hotfix.ID = entry;
            hotfix.Data = packet.ReadSingle("Data", indexes);

            Storage.ImportPriceShieldHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ImportPriceWeaponHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ImportPriceWeaponHotfix1200 hotfix = new ImportPriceWeaponHotfix1200();

            hotfix.ID = entry;
            hotfix.Data = packet.ReadSingle("Data", indexes);

            Storage.ImportPriceWeaponHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemHotfix1200 hotfix = new ItemHotfix1200();

            hotfix.ID = entry;
            hotfix.ClassID = packet.ReadInt32("ClassID", indexes);
            hotfix.SubclassID = packet.ReadByte("SubclassID", indexes);
            hotfix.Material = packet.ReadByte("Material", indexes);
            hotfix.InventoryType = packet.ReadSByte("InventoryType", indexes);
            hotfix.SheatheType = packet.ReadByte("SheatheType", indexes);
            hotfix.SoundOverrideSubclassID = packet.ReadSByte("SoundOverrideSubclassID", indexes);
            hotfix.IconFileDataID = packet.ReadInt32("IconFileDataID", indexes);
            hotfix.ItemGroupSoundsID = packet.ReadUInt32("ItemGroupSoundsID", indexes);
            hotfix.ContentTuningID = packet.ReadInt32("ContentTuningID", indexes);
            hotfix.ModifiedCraftingReagentItemID = packet.ReadInt32("ModifiedCraftingReagentItemID", indexes);
            hotfix.Unknown1200 = packet.ReadByte("Unknown1200", indexes);
            hotfix.CraftingQualityID = packet.ReadInt32("CraftingQualityID", indexes);
            hotfix.ItemSquishEraID = packet.ReadInt32("ItemSquishEraID", indexes);
            hotfix.RecraftReagentCountPercentage = packet.ReadSingle("RecraftReagentCountPercentage", indexes);
            hotfix.OrderSource = packet.ReadByte("OrderSource", indexes);

            Storage.ItemHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemAppearanceHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemAppearanceHotfix1200 hotfix = new ItemAppearanceHotfix1200();

            hotfix.ID = entry;
            hotfix.DisplayType = packet.ReadSByte("DisplayType", indexes);
            hotfix.ItemDisplayInfoID = packet.ReadInt32("ItemDisplayInfoID", indexes);
            hotfix.DefaultIconFileDataID = packet.ReadInt32("DefaultIconFileDataID", indexes);
            hotfix.UiOrder = packet.ReadInt32("UiOrder", indexes);
            hotfix.PlayerConditionID = packet.ReadInt32("PlayerConditionID", indexes);

            Storage.ItemAppearanceHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemArmorQualityHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemArmorQualityHotfix1200 hotfix = new ItemArmorQualityHotfix1200();

            hotfix.ID = entry;
            hotfix.Qualitymod = new float?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Qualitymod[i] = packet.ReadSingle("Qualitymod", indexes, i);

            Storage.ItemArmorQualityHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemArmorShieldHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemArmorShieldHotfix1200 hotfix = new ItemArmorShieldHotfix1200();

            hotfix.ID = entry;
            hotfix.Quality = new float?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Quality[i] = packet.ReadSingle("Quality", indexes, i);
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);

            Storage.ItemArmorShieldHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemArmorTotalHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemArmorTotalHotfix1200 hotfix = new ItemArmorTotalHotfix1200();

            hotfix.ID = entry;
            hotfix.ItemLevel = packet.ReadInt16("ItemLevel", indexes);
            hotfix.Cloth = packet.ReadSingle("Cloth", indexes);
            hotfix.Leather = packet.ReadSingle("Leather", indexes);
            hotfix.Mail = packet.ReadSingle("Mail", indexes);
            hotfix.Plate = packet.ReadSingle("Plate", indexes);

            Storage.ItemArmorTotalHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemBagFamilyHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemBagFamilyHotfix1200 hotfix = new ItemBagFamilyHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);

            Storage.ItemBagFamilyHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemBagFamilyLocaleHotfix1200 hotfixLocale = new ItemBagFamilyLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ItemBagFamilyHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemBonusHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemBonusHotfix1200 hotfix = new ItemBonusHotfix1200();

            hotfix.ID = entry;
            hotfix.Value = new int?[4];
            for (int i = 0; i < 4; i++)
                hotfix.Value[i] = packet.ReadInt32("Value", indexes, i);
            hotfix.ParentItemBonusListID = packet.ReadUInt16("ParentItemBonusListID", indexes);
            hotfix.Type = packet.ReadByte("Type", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);

            Storage.ItemBonusHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemBonusListGroupEntryHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemBonusListGroupEntryHotfix1200 hotfix = new ItemBonusListGroupEntryHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ItemBonusListGroupID = packet.ReadUInt32("ItemBonusListGroupID", indexes);
            hotfix.ItemBonusListID = packet.ReadInt32("ItemBonusListID", indexes);
            hotfix.ItemLevelSelectorID = packet.ReadInt32("ItemLevelSelectorID", indexes);
            hotfix.SequenceValue = packet.ReadInt32("SequenceValue", indexes);
            hotfix.ItemExtendedCostID = packet.ReadInt32("ItemExtendedCostID", indexes);
            hotfix.PlayerConditionID = packet.ReadInt32("PlayerConditionID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ItemLogicalCostGroupID = packet.ReadInt32("ItemLogicalCostGroupID", indexes);

            Storage.ItemBonusListGroupEntryHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemBonusListLevelDeltaHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemBonusListLevelDeltaHotfix1200 hotfix = new ItemBonusListLevelDeltaHotfix1200();

            hotfix.ItemLevelDelta = packet.ReadInt16("ItemLevelDelta", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);

            Storage.ItemBonusListLevelDeltaHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemBonusTreeHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemBonusTreeHotfix1200 hotfix = new ItemBonusTreeHotfix1200();

            hotfix.ID = entry;
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.InventoryTypeSlotMask = packet.ReadInt32("InventoryTypeSlotMask", indexes);

            Storage.ItemBonusTreeHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemBonusTreeNodeHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemBonusTreeNodeHotfix1200 hotfix = new ItemBonusTreeNodeHotfix1200();

            hotfix.ID = entry;
            hotfix.ItemContext = packet.ReadByte("ItemContext", indexes);
            hotfix.ChildItemBonusTreeID = packet.ReadUInt16("ChildItemBonusTreeID", indexes);
            hotfix.ChildItemBonusListID = packet.ReadUInt16("ChildItemBonusListID", indexes);
            hotfix.ChildItemLevelSelectorID = packet.ReadUInt16("ChildItemLevelSelectorID", indexes);
            hotfix.ChildItemBonusListGroupID = packet.ReadInt32("ChildItemBonusListGroupID", indexes);
            hotfix.IblGroupPointsModSetID = packet.ReadInt32("IblGroupPointsModSetID", indexes);
            hotfix.MinMythicPlusLevel = packet.ReadInt32("MinMythicPlusLevel", indexes);
            hotfix.MaxMythicPlusLevel = packet.ReadInt32("MaxMythicPlusLevel", indexes);
            hotfix.ItemCreationContextGroupID = packet.ReadInt32("ItemCreationContextGroupID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ParentItemBonusTreeID = packet.ReadUInt32("ParentItemBonusTreeID", indexes);

            Storage.ItemBonusTreeNodeHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemChildEquipmentHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemChildEquipmentHotfix1200 hotfix = new ItemChildEquipmentHotfix1200();

            hotfix.ID = entry;
            hotfix.ParentItemID = packet.ReadUInt32("ParentItemID", indexes);
            hotfix.ChildItemID = packet.ReadInt32("ChildItemID", indexes);
            hotfix.ChildItemEquipSlot = packet.ReadInt32("ChildItemEquipSlot", indexes);

            Storage.ItemChildEquipmentHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemClassHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemClassHotfix1200 hotfix = new ItemClassHotfix1200();

            hotfix.ID = entry;
            hotfix.ClassName = packet.ReadCString("ClassName", indexes);
            hotfix.ClassID = packet.ReadSByte("ClassID", indexes);
            hotfix.PriceModifier = packet.ReadSingle("PriceModifier", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.ItemClassHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemClassLocaleHotfix1200 hotfixLocale = new ItemClassLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    ClassNameLang = hotfix.ClassName,
                };
                Storage.ItemClassHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemContextPickerEntryHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemContextPickerEntryHotfix1200 hotfix = new ItemContextPickerEntryHotfix1200();

            hotfix.ID = entry;
            hotfix.ItemCreationContext = packet.ReadByte("ItemCreationContext", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);
            hotfix.PVal = packet.ReadInt32("PVal", indexes);
            hotfix.LabelID = packet.ReadInt32("LabelID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.ItemContextPickerID = packet.ReadUInt32("ItemContextPickerID", indexes);

            Storage.ItemContextPickerEntryHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemCreationContextHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemCreationContextHotfix1200 hotfix = new ItemCreationContextHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ItemContext = packet.ReadByte("ItemContext", indexes);
            hotfix.ItemCreationContextGroupID = packet.ReadUInt32("ItemCreationContextGroupID", indexes);

            Storage.ItemCreationContextHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemCurrencyCostHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemCurrencyCostHotfix1200 hotfix = new ItemCurrencyCostHotfix1200();

            hotfix.ID = entry;
            hotfix.ItemID = packet.ReadUInt32("ItemID", indexes);

            Storage.ItemCurrencyCostHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemDamageAmmoHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemDamageAmmoHotfix1200 hotfix = new ItemDamageAmmoHotfix1200();

            hotfix.ID = entry;
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);
            hotfix.Quality = new float?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Quality[i] = packet.ReadSingle("Quality", indexes, i);

            Storage.ItemDamageAmmoHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemDamageOneHandHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemDamageOneHandHotfix1200 hotfix = new ItemDamageOneHandHotfix1200();

            hotfix.ID = entry;
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);
            hotfix.Quality = new float?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Quality[i] = packet.ReadSingle("Quality", indexes, i);

            Storage.ItemDamageOneHandHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemDamageOneHandCasterHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemDamageOneHandCasterHotfix1200 hotfix = new ItemDamageOneHandCasterHotfix1200();

            hotfix.ID = entry;
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);
            hotfix.Quality = new float?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Quality[i] = packet.ReadSingle("Quality", indexes, i);

            Storage.ItemDamageOneHandCasterHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemDamageTwoHandHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemDamageTwoHandHotfix1200 hotfix = new ItemDamageTwoHandHotfix1200();

            hotfix.ID = entry;
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);
            hotfix.Quality = new float?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Quality[i] = packet.ReadSingle("Quality", indexes, i);

            Storage.ItemDamageTwoHandHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemDamageTwoHandCasterHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemDamageTwoHandCasterHotfix1200 hotfix = new ItemDamageTwoHandCasterHotfix1200();

            hotfix.ID = entry;
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);
            hotfix.Quality = new float?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Quality[i] = packet.ReadSingle("Quality", indexes, i);

            Storage.ItemDamageTwoHandCasterHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemDisenchantLootHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemDisenchantLootHotfix1200 hotfix = new ItemDisenchantLootHotfix1200();

            hotfix.ID = entry;
            hotfix.Subclass = packet.ReadSByte("Subclass", indexes);
            hotfix.Quality = packet.ReadByte("Quality", indexes);
            hotfix.MinLevel = packet.ReadUInt16("MinLevel", indexes);
            hotfix.MaxLevel = packet.ReadUInt16("MaxLevel", indexes);
            hotfix.SkillRequired = packet.ReadUInt16("SkillRequired", indexes);
            hotfix.ExpansionID = packet.ReadSByte("ExpansionID", indexes);
            hotfix.Class = packet.ReadUInt32("Class", indexes);

            Storage.ItemDisenchantLootHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemEffectHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemEffectHotfix1200 hotfix = new ItemEffectHotfix1200();

            hotfix.ID = entry;
            hotfix.LegacySlotIndex = packet.ReadByte("LegacySlotIndex", indexes);
            hotfix.TriggerType = packet.ReadByte("TriggerType", indexes);
            hotfix.Charges = packet.ReadInt16("Charges", indexes);
            hotfix.CoolDownMSec = packet.ReadInt32("CoolDownMSec", indexes);
            hotfix.CategoryCoolDownMSec = packet.ReadInt32("CategoryCoolDownMSec", indexes);
            hotfix.SpellCategoryID = packet.ReadUInt16("SpellCategoryID", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.ChrSpecializationID = packet.ReadUInt16("ChrSpecializationID", indexes);
            hotfix.PlayerConditionID = packet.ReadInt32("PlayerConditionID", indexes);

            Storage.ItemEffectHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemExtendedCostHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemExtendedCostHotfix1200 hotfix = new ItemExtendedCostHotfix1200();

            hotfix.ID = entry;
            hotfix.RequiredArenaRating = packet.ReadUInt16("RequiredArenaRating", indexes);
            hotfix.ArenaBracket = packet.ReadSByte("ArenaBracket", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.MinFactionID = packet.ReadByte("MinFactionID", indexes);
            hotfix.MinReputation = packet.ReadInt32("MinReputation", indexes);
            hotfix.RequiredAchievement = packet.ReadByte("RequiredAchievement", indexes);
            hotfix.ItemID = new int?[5];
            for (int i = 0; i < 5; i++)
                hotfix.ItemID[i] = packet.ReadInt32("ItemID", indexes, i);
            hotfix.ItemCount = new ushort?[5];
            for (int i = 0; i < 5; i++)
                hotfix.ItemCount[i] = packet.ReadUInt16("ItemCount", indexes, i);
            hotfix.CurrencyID = new ushort?[5];
            for (int i = 0; i < 5; i++)
                hotfix.CurrencyID[i] = packet.ReadUInt16("CurrencyID", indexes, i);
            hotfix.CurrencyCount = new uint?[5];
            for (int i = 0; i < 5; i++)
                hotfix.CurrencyCount[i] = packet.ReadUInt32("CurrencyCount", indexes, i);

            Storage.ItemExtendedCostHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemLevelSelectorHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemLevelSelectorHotfix1200 hotfix = new ItemLevelSelectorHotfix1200();

            hotfix.ID = entry;
            hotfix.MinItemLevel = packet.ReadUInt16("MinItemLevel", indexes);
            hotfix.ItemLevelSelectorQualitySetID = packet.ReadUInt16("ItemLevelSelectorQualitySetID", indexes);
            hotfix.AzeriteUnlockMappingSet = packet.ReadUInt16("AzeriteUnlockMappingSet", indexes);

            Storage.ItemLevelSelectorHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemLevelSelectorQualityHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemLevelSelectorQualityHotfix1200 hotfix = new ItemLevelSelectorQualityHotfix1200();

            hotfix.ID = entry;
            hotfix.QualityItemBonusListID = packet.ReadInt32("QualityItemBonusListID", indexes);
            hotfix.Quality = packet.ReadSByte("Quality", indexes);
            hotfix.ParentILSQualitySetID = packet.ReadUInt32("ParentILSQualitySetID", indexes);

            Storage.ItemLevelSelectorQualityHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemLevelSelectorQualitySetHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemLevelSelectorQualitySetHotfix1200 hotfix = new ItemLevelSelectorQualitySetHotfix1200();

            hotfix.ID = entry;
            hotfix.IlvlRare = packet.ReadInt16("IlvlRare", indexes);
            hotfix.IlvlEpic = packet.ReadInt16("IlvlEpic", indexes);

            Storage.ItemLevelSelectorQualitySetHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemLimitCategoryHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemLimitCategoryHotfix1200 hotfix = new ItemLimitCategoryHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Quantity = packet.ReadByte("Quantity", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.ItemLimitCategoryHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemLimitCategoryLocaleHotfix1200 hotfixLocale = new ItemLimitCategoryLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ItemLimitCategoryHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemLimitCategoryConditionHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemLimitCategoryConditionHotfix1200 hotfix = new ItemLimitCategoryConditionHotfix1200();

            hotfix.ID = entry;
            hotfix.AddQuantity = packet.ReadSByte("AddQuantity", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.ParentItemLimitCategoryID = packet.ReadUInt32("ParentItemLimitCategoryID", indexes);

            Storage.ItemLimitCategoryConditionHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemModifiedAppearanceHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemModifiedAppearanceHotfix1200 hotfix = new ItemModifiedAppearanceHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ItemID = packet.ReadUInt32("ItemID", indexes);
            hotfix.ItemAppearanceModifierID = packet.ReadInt32("ItemAppearanceModifierID", indexes);
            hotfix.ItemAppearanceID = packet.ReadInt32("ItemAppearanceID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.TransmogSourceTypeEnum = packet.ReadByte("TransmogSourceTypeEnum", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.ItemModifiedAppearanceHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemModifiedAppearanceExtraHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemModifiedAppearanceExtraHotfix1200 hotfix = new ItemModifiedAppearanceExtraHotfix1200();

            hotfix.ID = entry;
            hotfix.IconFileDataID = packet.ReadInt32("IconFileDataID", indexes);
            hotfix.UnequippedIconFileDataID = packet.ReadInt32("UnequippedIconFileDataID", indexes);
            hotfix.SheatheType = packet.ReadByte("SheatheType", indexes);
            hotfix.DisplayWeaponSubclassID = packet.ReadSByte("DisplayWeaponSubclassID", indexes);
            hotfix.DisplayInventoryType = packet.ReadSByte("DisplayInventoryType", indexes);

            Storage.ItemModifiedAppearanceExtraHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemNameDescriptionHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemNameDescriptionHotfix1200 hotfix = new ItemNameDescriptionHotfix1200();

            hotfix.ID = entry;
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.Color = packet.ReadInt32("Color", indexes);

            Storage.ItemNameDescriptionHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemNameDescriptionLocaleHotfix1200 hotfixLocale = new ItemNameDescriptionLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                };
                Storage.ItemNameDescriptionHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemOffsetCurveHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemOffsetCurveHotfix1200 hotfix = new ItemOffsetCurveHotfix1200();

            hotfix.ID = entry;
            hotfix.CurveID = packet.ReadInt32("CurveID", indexes);
            hotfix.Offset = packet.ReadInt32("Offset", indexes);

            Storage.ItemOffsetCurveHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemPriceBaseHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemPriceBaseHotfix1200 hotfix = new ItemPriceBaseHotfix1200();

            hotfix.ID = entry;
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);
            hotfix.Armor = packet.ReadSingle("Armor", indexes);
            hotfix.Weapon = packet.ReadSingle("Weapon", indexes);

            Storage.ItemPriceBaseHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemScalingConfigHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemScalingConfigHotfix1200 hotfix = new ItemScalingConfigHotfix1200();

            hotfix.ID = entry;
            hotfix.ItemOffsetCurveID = packet.ReadInt32("ItemOffsetCurveID", indexes);
            hotfix.ItemLevel = packet.ReadInt32("ItemLevel", indexes);
            hotfix.RequiredLevel = packet.ReadInt32("RequiredLevel", indexes);
            hotfix.ItemSquishEraID = packet.ReadInt32("ItemSquishEraID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.ItemScalingConfigHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemSearchNameHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemSearchNameHotfix1200 hotfix = new ItemSearchNameHotfix1200();

            hotfix.ID = entry;
            hotfix.AllowableRace = packet.ReadInt64("AllowableRace", indexes);
            hotfix.Display = packet.ReadCString("Display", indexes);
            hotfix.OverallQualityID = packet.ReadByte("OverallQualityID", indexes);
            hotfix.ExpansionID = packet.ReadInt32("ExpansionID", indexes);
            hotfix.MinFactionID = packet.ReadUInt16("MinFactionID", indexes);
            hotfix.MinReputation = packet.ReadInt32("MinReputation", indexes);
            hotfix.AllowableClass = packet.ReadInt32("AllowableClass", indexes);
            hotfix.RequiredLevel = packet.ReadSByte("RequiredLevel", indexes);
            hotfix.RequiredSkill = packet.ReadUInt16("RequiredSkill", indexes);
            hotfix.RequiredSkillRank = packet.ReadUInt16("RequiredSkillRank", indexes);
            hotfix.RequiredAbility = packet.ReadUInt32("RequiredAbility", indexes);
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);
            hotfix.Flags = new int?[5];
            for (int i = 0; i < 5; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.ItemSearchNameHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemSearchNameLocaleHotfix1200 hotfixLocale = new ItemSearchNameLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    DisplayLang = hotfix.Display,
                };
                Storage.ItemSearchNameHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemSetHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemSetHotfix1200 hotfix = new ItemSetHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.SetFlags = packet.ReadInt32("SetFlags", indexes);
            hotfix.RequiredSkill = packet.ReadUInt32("RequiredSkill", indexes);
            hotfix.RequiredSkillRank = packet.ReadUInt16("RequiredSkillRank", indexes);
            hotfix.ItemID = new uint?[17];
            for (int i = 0; i < 17; i++)
                hotfix.ItemID[i] = packet.ReadUInt32("ItemID", indexes, i);

            Storage.ItemSetHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemSetLocaleHotfix1200 hotfixLocale = new ItemSetLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ItemSetHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemSetSpellHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemSetSpellHotfix1200 hotfix = new ItemSetSpellHotfix1200();

            hotfix.ID = entry;
            hotfix.ChrSpecID = packet.ReadUInt16("ChrSpecID", indexes);
            hotfix.SpellID = packet.ReadUInt32("SpellID", indexes);
            hotfix.TraitSubTreeID = packet.ReadUInt16("TraitSubTreeID", indexes);
            hotfix.Threshold = packet.ReadByte("Threshold", indexes);
            hotfix.ItemSetID = packet.ReadUInt32("ItemSetID", indexes);

            Storage.ItemSetSpellHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemSparseHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemSparseHotfix1200 hotfix = new ItemSparseHotfix1200();

            hotfix.ID = entry;
            hotfix.AllowableRace = packet.ReadInt64("AllowableRace", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.Display3 = packet.ReadCString("Display3", indexes);
            hotfix.Display2 = packet.ReadCString("Display2", indexes);
            hotfix.Display1 = packet.ReadCString("Display1", indexes);
            hotfix.Display = packet.ReadCString("Display", indexes);
            hotfix.ExpansionID = packet.ReadInt32("ExpansionID", indexes);
            hotfix.DmgVariance = packet.ReadSingle("DmgVariance", indexes);
            hotfix.LimitCategory = packet.ReadInt32("LimitCategory", indexes);
            hotfix.DurationInInventory = packet.ReadUInt32("DurationInInventory", indexes);
            hotfix.QualityModifier = packet.ReadSingle("QualityModifier", indexes);
            hotfix.BagFamily = packet.ReadUInt32("BagFamily", indexes);
            hotfix.StartQuestID = packet.ReadInt32("StartQuestID", indexes);
            hotfix.LanguageID = packet.ReadInt32("LanguageID", indexes);
            hotfix.ItemRange = packet.ReadSingle("ItemRange", indexes);
            hotfix.StatPercentageOfSocket = new float?[10];
            for (int i = 0; i < 10; i++)
                hotfix.StatPercentageOfSocket[i] = packet.ReadSingle("StatPercentageOfSocket", indexes, i);
            hotfix.StatPercentEditor = new int?[10];
            for (int i = 0; i < 10; i++)
                hotfix.StatPercentEditor[i] = packet.ReadInt32("StatPercentEditor", indexes, i);
            hotfix.StatModifierBonusStat = new int?[10];
            for (int i = 0; i < 10; i++)
                hotfix.StatModifierBonusStat[i] = packet.ReadInt32("StatModifierBonusStat", indexes, i);
            hotfix.Stackable = packet.ReadInt32("Stackable", indexes);
            hotfix.MaxCount = packet.ReadInt32("MaxCount", indexes);
            hotfix.MinReputation = packet.ReadInt32("MinReputation", indexes);
            hotfix.RequiredAbility = packet.ReadUInt32("RequiredAbility", indexes);
            hotfix.SellPrice = packet.ReadUInt32("SellPrice", indexes);
            hotfix.BuyPrice = packet.ReadUInt32("BuyPrice", indexes);
            hotfix.VendorStackCount = packet.ReadUInt32("VendorStackCount", indexes);
            hotfix.PriceVariance = packet.ReadSingle("PriceVariance", indexes);
            hotfix.PriceRandomValue = packet.ReadSingle("PriceRandomValue", indexes);
            hotfix.Flags = new int?[5];
            for (int i = 0; i < 5; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);
            hotfix.FactionRelated = packet.ReadInt32("FactionRelated", indexes);
            hotfix.ModifiedCraftingReagentItemID = packet.ReadInt32("ModifiedCraftingReagentItemID", indexes);
            hotfix.ContentTuningID = packet.ReadInt32("ContentTuningID", indexes);
            hotfix.PlayerLevelToItemLevelCurveID = packet.ReadInt32("PlayerLevelToItemLevelCurveID", indexes);
            hotfix.ItemLevelOffsetCurveID = packet.ReadInt32("ItemLevelOffsetCurveID", indexes);
            hotfix.ItemLevelOffsetItemLevel = packet.ReadInt32("ItemLevelOffsetItemLevel", indexes);
            hotfix.ItemSquishEraID = packet.ReadInt32("ItemSquishEraID", indexes);
            hotfix.ItemNameDescriptionID = packet.ReadUInt16("ItemNameDescriptionID", indexes);
            hotfix.RequiredTransmogHoliday = packet.ReadUInt16("RequiredTransmogHoliday", indexes);
            hotfix.RequiredHoliday = packet.ReadUInt16("RequiredHoliday", indexes);
            hotfix.GemProperties = packet.ReadUInt16("GemProperties", indexes);
            hotfix.SocketMatchEnchantmentId = packet.ReadUInt16("SocketMatchEnchantmentId", indexes);
            hotfix.TotemCategoryID = packet.ReadUInt16("TotemCategoryID", indexes);
            hotfix.InstanceBound = packet.ReadUInt16("InstanceBound", indexes);
            hotfix.ZoneBound = new ushort?[2];
            for (int i = 0; i < 2; i++)
                hotfix.ZoneBound[i] = packet.ReadUInt16("ZoneBound", indexes, i);
            hotfix.ItemSet = packet.ReadUInt16("ItemSet", indexes);
            hotfix.LockID = packet.ReadUInt16("LockID", indexes);
            hotfix.PageID = packet.ReadUInt16("PageID", indexes);
            hotfix.ItemDelay = packet.ReadUInt16("ItemDelay", indexes);
            hotfix.MinFactionID = packet.ReadUInt16("MinFactionID", indexes);
            hotfix.RequiredSkillRank = packet.ReadUInt16("RequiredSkillRank", indexes);
            hotfix.RequiredSkill = packet.ReadUInt16("RequiredSkill", indexes);
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);
            hotfix.AllowableClass = packet.ReadInt16("AllowableClass", indexes);
            hotfix.ArtifactID = packet.ReadByte("ArtifactID", indexes);
            hotfix.SpellWeight = packet.ReadByte("SpellWeight", indexes);
            hotfix.SpellWeightCategory = packet.ReadByte("SpellWeightCategory", indexes);
            hotfix.SocketType = new byte?[3];
            for (int i = 0; i < 3; i++)
                hotfix.SocketType[i] = packet.ReadByte("SocketType", indexes, i);
            hotfix.SheatheType = packet.ReadByte("SheatheType", indexes);
            hotfix.Material = packet.ReadByte("Material", indexes);
            hotfix.PageMaterialID = packet.ReadByte("PageMaterialID", indexes);
            hotfix.Bonding = packet.ReadByte("Bonding", indexes);
            hotfix.DamageDamageType = packet.ReadByte("DamageDamageType", indexes);
            hotfix.ContainerSlots = packet.ReadByte("ContainerSlots", indexes);
            hotfix.RequiredPVPMedal = packet.ReadByte("RequiredPVPMedal", indexes);
            hotfix.RequiredPVPRank = packet.ReadSByte("RequiredPVPRank", indexes);
            hotfix.RequiredLevel = packet.ReadSByte("RequiredLevel", indexes);
            hotfix.InventoryType = packet.ReadSByte("InventoryType", indexes);
            hotfix.OverallQualityID = packet.ReadSByte("OverallQualityID", indexes);

            Storage.ItemSparseHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemSparseLocaleHotfix1200 hotfixLocale = new ItemSparseLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                    Display3Lang = hotfix.Display3,
                    Display2Lang = hotfix.Display2,
                    Display1Lang = hotfix.Display1,
                    DisplayLang = hotfix.Display,
                };
                Storage.ItemSparseHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemSpecHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemSpecHotfix1200 hotfix = new ItemSpecHotfix1200();

            hotfix.ID = entry;
            hotfix.MinLevel = packet.ReadByte("MinLevel", indexes);
            hotfix.MaxLevel = packet.ReadByte("MaxLevel", indexes);
            hotfix.ItemType = packet.ReadByte("ItemType", indexes);
            hotfix.PrimaryStat = packet.ReadByte("PrimaryStat", indexes);
            hotfix.SecondaryStat = packet.ReadByte("SecondaryStat", indexes);
            hotfix.SpecializationID = packet.ReadUInt16("SpecializationID", indexes);

            Storage.ItemSpecHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemSpecOverrideHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemSpecOverrideHotfix1200 hotfix = new ItemSpecOverrideHotfix1200();

            hotfix.ID = entry;
            hotfix.SpecID = packet.ReadUInt16("SpecID", indexes);
            hotfix.ItemID = packet.ReadUInt32("ItemID", indexes);

            Storage.ItemSpecOverrideHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemSquishEraHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemSquishEraHotfix1200 hotfix = new ItemSquishEraHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Patch = packet.ReadInt32("Patch", indexes);
            hotfix.CurveID = packet.ReadInt32("CurveID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.ItemSquishEraHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemXBonusTreeHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemXBonusTreeHotfix1200 hotfix = new ItemXBonusTreeHotfix1200();

            hotfix.ID = entry;
            hotfix.ItemBonusTreeID = packet.ReadUInt16("ItemBonusTreeID", indexes);
            hotfix.ItemID = packet.ReadUInt32("ItemID", indexes);

            Storage.ItemXBonusTreeHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemXItemEffectHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ItemXItemEffectHotfix1200 hotfix = new ItemXItemEffectHotfix1200();

            hotfix.ID = entry;
            hotfix.ItemEffectID = packet.ReadInt32("ItemEffectID", indexes);
            hotfix.ItemID = packet.ReadUInt32("ItemID", indexes);

            Storage.ItemXItemEffectHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void JournalEncounterHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            JournalEncounterHotfix1200 hotfix = new JournalEncounterHotfix1200();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.MapX = packet.ReadSingle("MapX", indexes);
            hotfix.MapY = packet.ReadSingle("MapY", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.JournalInstanceID = packet.ReadUInt16("JournalInstanceID", indexes);
            hotfix.DungeonEncounterID = packet.ReadUInt16("DungeonEncounterID", indexes);
            hotfix.OrderIndex = packet.ReadUInt32("OrderIndex", indexes);
            hotfix.FirstSectionID = packet.ReadUInt16("FirstSectionID", indexes);
            hotfix.UiMapID = packet.ReadUInt16("UiMapID", indexes);
            hotfix.MapDisplayConditionID = packet.ReadUInt32("MapDisplayConditionID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.DifficultyMask = packet.ReadSByte("DifficultyMask", indexes);

            Storage.JournalEncounterHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                JournalEncounterLocaleHotfix1200 hotfixLocale = new JournalEncounterLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.JournalEncounterHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void JournalEncounterSectionHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            JournalEncounterSectionHotfix1200 hotfix = new JournalEncounterSectionHotfix1200();

            hotfix.ID = entry;
            hotfix.Title = packet.ReadCString("Title", indexes);
            hotfix.BodyText = packet.ReadCString("BodyText", indexes);
            hotfix.JournalEncounterID = packet.ReadUInt16("JournalEncounterID", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);
            hotfix.ParentSectionID = packet.ReadUInt16("ParentSectionID", indexes);
            hotfix.FirstChildSectionID = packet.ReadUInt16("FirstChildSectionID", indexes);
            hotfix.NextSiblingSectionID = packet.ReadUInt16("NextSiblingSectionID", indexes);
            hotfix.Type = packet.ReadByte("Type", indexes);
            hotfix.IconCreatureDisplayInfoID = packet.ReadUInt32("IconCreatureDisplayInfoID", indexes);
            hotfix.UiModelSceneID = packet.ReadInt32("UiModelSceneID", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.IconFileDataID = packet.ReadInt32("IconFileDataID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.IconFlags = packet.ReadInt32("IconFlags", indexes);
            hotfix.DifficultyMask = packet.ReadSByte("DifficultyMask", indexes);

            Storage.JournalEncounterSectionHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                JournalEncounterSectionLocaleHotfix1200 hotfixLocale = new JournalEncounterSectionLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    TitleLang = hotfix.Title,
                    BodyTextLang = hotfix.BodyText,
                };
                Storage.JournalEncounterSectionHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void JournalInstanceHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            JournalInstanceHotfix1200 hotfix = new JournalInstanceHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.MapID = packet.ReadUInt16("MapID", indexes);
            hotfix.BackgroundFileDataID = packet.ReadInt32("BackgroundFileDataID", indexes);
            hotfix.ButtonFileDataID = packet.ReadInt32("ButtonFileDataID", indexes);
            hotfix.ButtonSmallFileDataID = packet.ReadInt32("ButtonSmallFileDataID", indexes);
            hotfix.LoreFileDataID = packet.ReadInt32("LoreFileDataID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.AreaID = packet.ReadUInt16("AreaID", indexes);
            hotfix.CovenantID = packet.ReadUInt16("CovenantID", indexes);

            Storage.JournalInstanceHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                JournalInstanceLocaleHotfix1200 hotfixLocale = new JournalInstanceLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.JournalInstanceHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void JournalTierHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            JournalTierHotfix1200 hotfix = new JournalTierHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Expansion = packet.ReadInt32("Expansion", indexes);
            hotfix.PlayerConditionID = packet.ReadInt32("PlayerConditionID", indexes);

            Storage.JournalTierHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                JournalTierLocaleHotfix1200 hotfixLocale = new JournalTierLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.JournalTierHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void KeychainHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            KeychainHotfix1200 hotfix = new KeychainHotfix1200();

            hotfix.ID = entry;
            hotfix.Key = new byte?[32];
            for (int i = 0; i < 32; i++)
                hotfix.Key[i] = packet.ReadByte("Key", indexes, i);

            Storage.KeychainHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void KeystoneAffixHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            KeystoneAffixHotfix1200 hotfix = new KeystoneAffixHotfix1200();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.FiledataID = packet.ReadInt32("FiledataID", indexes);

            Storage.KeystoneAffixHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                KeystoneAffixLocaleHotfix1200 hotfixLocale = new KeystoneAffixLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.KeystoneAffixHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void LanguageWordsHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            LanguageWordsHotfix1200 hotfix = new LanguageWordsHotfix1200();

            hotfix.ID = entry;
            hotfix.Word = packet.ReadCString("Word", indexes);
            hotfix.LanguageID = packet.ReadUInt32("LanguageID", indexes);

            Storage.LanguageWordsHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void LanguagesHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            LanguagesHotfix1200 hotfix = new LanguagesHotfix1200();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.UiTextureKitID = packet.ReadInt32("UiTextureKitID", indexes);
            hotfix.UiTextureKitElementCount = packet.ReadInt32("UiTextureKitElementCount", indexes);
            hotfix.LearningCurveID = packet.ReadInt32("LearningCurveID", indexes);

            Storage.LanguagesHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                LanguagesLocaleHotfix1200 hotfixLocale = new LanguagesLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.LanguagesHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void LfgDungeonsHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            LfgDungeonsHotfix1200 hotfix = new LfgDungeonsHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.TypeID = packet.ReadByte("TypeID", indexes);
            hotfix.Subtype = packet.ReadByte("Subtype", indexes);
            hotfix.Faction = packet.ReadSByte("Faction", indexes);
            hotfix.IconTextureFileID = packet.ReadInt32("IconTextureFileID", indexes);
            hotfix.RewardsBgTextureFileID = packet.ReadInt32("RewardsBgTextureFileID", indexes);
            hotfix.PopupBgTextureFileID = packet.ReadInt32("PopupBgTextureFileID", indexes);
            hotfix.ExpansionLevel = packet.ReadByte("ExpansionLevel", indexes);
            hotfix.MapID = packet.ReadInt16("MapID", indexes);
            hotfix.DifficultyID = packet.ReadInt16("DifficultyID", indexes);
            hotfix.MinGear = packet.ReadSingle("MinGear", indexes);
            hotfix.GroupID = packet.ReadByte("GroupID", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);
            hotfix.RequiredPlayerConditionId = packet.ReadUInt32("RequiredPlayerConditionId", indexes);
            hotfix.RandomID = packet.ReadUInt16("RandomID", indexes);
            hotfix.ScenarioID = packet.ReadUInt16("ScenarioID", indexes);
            hotfix.FinalEncounterID = packet.ReadUInt16("FinalEncounterID", indexes);
            hotfix.CountTank = packet.ReadByte("CountTank", indexes);
            hotfix.CountHealer = packet.ReadByte("CountHealer", indexes);
            hotfix.CountDamage = packet.ReadByte("CountDamage", indexes);
            hotfix.MinCountTank = packet.ReadByte("MinCountTank", indexes);
            hotfix.MinCountHealer = packet.ReadByte("MinCountHealer", indexes);
            hotfix.MinCountDamage = packet.ReadByte("MinCountDamage", indexes);
            hotfix.MaxPremadeCountTank = packet.ReadByte("MaxPremadeCountTank", indexes);
            hotfix.MaxPremadeCountHealer = packet.ReadByte("MaxPremadeCountHealer", indexes);
            hotfix.MaxPremadeCountDamage = packet.ReadByte("MaxPremadeCountDamage", indexes);
            hotfix.BonusReputationAmount = packet.ReadUInt16("BonusReputationAmount", indexes);
            hotfix.MentorItemLevel = packet.ReadUInt16("MentorItemLevel", indexes);
            hotfix.MentorCharLevel = packet.ReadByte("MentorCharLevel", indexes);
            hotfix.MaxPremadeGroupSize = packet.ReadByte("MaxPremadeGroupSize", indexes);
            hotfix.ContentTuningID = packet.ReadInt32("ContentTuningID", indexes);
            hotfix.Flags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.LfgDungeonsHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                LfgDungeonsLocaleHotfix1200 hotfixLocale = new LfgDungeonsLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.LfgDungeonsHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void LightHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            LightHotfix1200 hotfix = new LightHotfix1200();

            hotfix.ID = entry;
            hotfix.GameCoordsX = packet.ReadSingle("GameCoordsX", indexes);
            hotfix.GameCoordsY = packet.ReadSingle("GameCoordsY", indexes);
            hotfix.GameCoordsZ = packet.ReadSingle("GameCoordsZ", indexes);
            hotfix.GameFalloffStart = packet.ReadSingle("GameFalloffStart", indexes);
            hotfix.GameFalloffEnd = packet.ReadSingle("GameFalloffEnd", indexes);
            hotfix.ContinentID = packet.ReadInt16("ContinentID", indexes);
            hotfix.LightParamsID = new ushort?[8];
            for (int i = 0; i < 8; i++)
                hotfix.LightParamsID[i] = packet.ReadUInt16("LightParamsID", indexes, i);

            Storage.LightHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void LiquidTypeHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            LiquidTypeHotfix1200 hotfix = new LiquidTypeHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Texture = new string[6];
            for (int i = 0; i < 6; i++)
                hotfix.Texture[i] = packet.ReadCString("Texture", indexes, i);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.SoundBank = packet.ReadByte("SoundBank", indexes);
            hotfix.SoundID = packet.ReadUInt32("SoundID", indexes);
            hotfix.SpellID = packet.ReadUInt32("SpellID", indexes);
            hotfix.MaxDarkenDepth = packet.ReadSingle("MaxDarkenDepth", indexes);
            hotfix.FogDarkenIntensity = packet.ReadSingle("FogDarkenIntensity", indexes);
            hotfix.AmbDarkenIntensity = packet.ReadSingle("AmbDarkenIntensity", indexes);
            hotfix.DirDarkenIntensity = packet.ReadSingle("DirDarkenIntensity", indexes);
            hotfix.LightID = packet.ReadUInt16("LightID", indexes);
            hotfix.ParticleScale = packet.ReadSingle("ParticleScale", indexes);
            hotfix.ParticleMovement = packet.ReadByte("ParticleMovement", indexes);
            hotfix.ParticleTexSlots = packet.ReadByte("ParticleTexSlots", indexes);
            hotfix.MaterialID = packet.ReadByte("MaterialID", indexes);
            hotfix.MinimapStaticCol = packet.ReadInt32("MinimapStaticCol", indexes);
            hotfix.FrameCountTexture = new byte?[6];
            for (int i = 0; i < 6; i++)
                hotfix.FrameCountTexture[i] = packet.ReadByte("FrameCountTexture", indexes, i);
            hotfix.Color = new int?[3];
            for (int i = 0; i < 3; i++)
                hotfix.Color[i] = packet.ReadInt32("Color", indexes, i);
            hotfix.Float = new float?[38];
            for (int i = 0; i < 38; i++)
                hotfix.Float[i] = packet.ReadSingle("Float", indexes, i);
            hotfix.Int = new uint?[4];
            for (int i = 0; i < 4; i++)
                hotfix.Int[i] = packet.ReadUInt32("Int", indexes, i);
            hotfix.Coefficient = new float?[4];
            for (int i = 0; i < 4; i++)
                hotfix.Coefficient[i] = packet.ReadSingle("Coefficient", indexes, i);

            Storage.LiquidTypeHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void LocationHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            LocationHotfix1200 hotfix = new LocationHotfix1200();

            hotfix.ID = entry;
            hotfix.PosX = packet.ReadSingle("PosX");
            hotfix.PosY = packet.ReadSingle("PosY");
            hotfix.PosZ = packet.ReadSingle("PosZ");
            hotfix.Rot = new float?[3];
            for (int i = 0; i < 3; i++)
                hotfix.Rot[i] = packet.ReadSingle("Rot", indexes, i);

            Storage.LocationHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void LockHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            LockHotfix1200 hotfix = new LockHotfix1200();

            hotfix.ID = entry;
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.Index = new int?[8];
            for (int i = 0; i < 8; i++)
                hotfix.Index[i] = packet.ReadInt32("Index", indexes, i);
            hotfix.Skill = new ushort?[8];
            for (int i = 0; i < 8; i++)
                hotfix.Skill[i] = packet.ReadUInt16("Skill", indexes, i);
            hotfix.Type = new byte?[8];
            for (int i = 0; i < 8; i++)
                hotfix.Type[i] = packet.ReadByte("Type", indexes, i);
            hotfix.Action = new byte?[8];
            for (int i = 0; i < 8; i++)
                hotfix.Action[i] = packet.ReadByte("Action", indexes, i);

            Storage.LockHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void MailTemplateHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            MailTemplateHotfix1200 hotfix = new MailTemplateHotfix1200();

            hotfix.ID = entry;
            hotfix.Body = packet.ReadCString("Body", indexes);

            Storage.MailTemplateHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                MailTemplateLocaleHotfix1200 hotfixLocale = new MailTemplateLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    BodyLang = hotfix.Body,
                };
                Storage.MailTemplateHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void MapHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            MapHotfix1200 hotfix = new MapHotfix1200();

            hotfix.ID = entry;
            hotfix.Directory = packet.ReadCString("Directory", indexes);
            hotfix.MapName = packet.ReadCString("MapName", indexes);
            hotfix.MapDescription0 = packet.ReadCString("MapDescription0", indexes);
            hotfix.MapDescription1 = packet.ReadCString("MapDescription1", indexes);
            hotfix.PvpShortDescription = packet.ReadCString("PvpShortDescription", indexes);
            hotfix.PvpLongDescription = packet.ReadCString("PvpLongDescription", indexes);
            hotfix.CorpseX = packet.ReadSingle("CorpseX", indexes);
            hotfix.CorpseY = packet.ReadSingle("CorpseY", indexes);
            hotfix.MapType = packet.ReadByte("MapType", indexes);
            hotfix.InstanceType = packet.ReadSByte("InstanceType", indexes);
            hotfix.ExpansionID = packet.ReadByte("ExpansionID", indexes);
            hotfix.AreaTableID = packet.ReadUInt16("AreaTableID", indexes);
            hotfix.LoadingScreenID = packet.ReadInt16("LoadingScreenID", indexes);
            hotfix.TimeOfDayOverride = packet.ReadInt16("TimeOfDayOverride", indexes);
            hotfix.ParentMapID = packet.ReadInt16("ParentMapID", indexes);
            hotfix.CosmeticParentMapID = packet.ReadInt16("CosmeticParentMapID", indexes);
            hotfix.TimeOffset = packet.ReadByte("TimeOffset", indexes);
            hotfix.MinimapIconScale = packet.ReadSingle("MinimapIconScale", indexes);
            hotfix.CorpseMapID = packet.ReadInt16("CorpseMapID", indexes);
            hotfix.MaxPlayers = packet.ReadByte("MaxPlayers", indexes);
            hotfix.WindSettingsID = packet.ReadInt16("WindSettingsID", indexes);
            hotfix.ZmpFileDataID = packet.ReadInt32("ZmpFileDataID", indexes);
            hotfix.WdtFileDataID = packet.ReadInt32("WdtFileDataID", indexes);
            hotfix.NavigationMaxDistance = packet.ReadInt32("NavigationMaxDistance", indexes);
            hotfix.PreloadFileDataID = packet.ReadInt32("PreloadFileDataID", indexes);
            hotfix.Flags = new int?[3];
            for (int i = 0; i < 3; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.MapHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                MapLocaleHotfix1200 hotfixLocale = new MapLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    MapNameLang = hotfix.MapName,
                    MapDescription0Lang = hotfix.MapDescription0,
                    MapDescription1Lang = hotfix.MapDescription1,
                    PvpShortDescriptionLang = hotfix.PvpShortDescription,
                    PvpLongDescriptionLang = hotfix.PvpLongDescription,
                };
                Storage.MapHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void MapChallengeModeHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            MapChallengeModeHotfix1200 hotfix = new MapChallengeModeHotfix1200();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.MapID = packet.ReadUInt16("MapID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.MaxMedals = packet.ReadInt32("MaxMedals", indexes);
            hotfix.ExpansionLevel = packet.ReadUInt32("ExpansionLevel", indexes);
            hotfix.RequiredWorldStateID = packet.ReadInt32("RequiredWorldStateID", indexes);
            hotfix.CriteriaCount = new short?[3];
            for (int i = 0; i < 5; i++)
                hotfix.CriteriaCount[i] = packet.ReadInt16("CriteriaCount", indexes, i);
            for (int i = 0; i < 6; i++)
                hotfix.FirstRewardQuestID[i] = packet.ReadInt32("FirstRewardQuestID", indexes, i);
            for (int i = 0; i < 6; i++)
                hotfix.RewardQuestID[i] = packet.ReadInt32("RewardQuestID", indexes, i);

            Storage.MapChallengeModeHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                MapChallengeModeLocaleHotfix1200 hotfixLocale = new MapChallengeModeLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.MapChallengeModeHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void MapDifficultyHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            MapDifficultyHotfix1200 hotfix = new MapDifficultyHotfix1200();

            hotfix.Message = packet.ReadCString("Message", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.DifficultyID = packet.ReadInt16("DifficultyID", indexes);
            hotfix.LockID = packet.ReadInt32("LockID", indexes);
            hotfix.ResetInterval = packet.ReadByte("ResetInterval", indexes);
            hotfix.MaxPlayers = packet.ReadInt32("MaxPlayers", indexes);
            hotfix.ItemContext = packet.ReadByte("ItemContext", indexes);
            hotfix.ItemContextPickerID = packet.ReadInt32("ItemContextPickerID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ContentTuningID = packet.ReadInt32("ContentTuningID", indexes);
            hotfix.WorldStateExpressionID = packet.ReadInt32("WorldStateExpressionID", indexes);
            hotfix.MapID = packet.ReadUInt32("MapID", indexes);

            Storage.MapDifficultyHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                MapDifficultyLocaleHotfix1200 hotfixLocale = new MapDifficultyLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    MessageLang = hotfix.Message,
                };
                Storage.MapDifficultyHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void MapDifficultyXConditionHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            MapDifficultyXConditionHotfix1200 hotfix = new MapDifficultyXConditionHotfix1200();

            hotfix.ID = entry;
            hotfix.FailureDescription = packet.ReadCString("FailureDescription", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.MapDifficultyID = packet.ReadUInt32("MapDifficultyID", indexes);

            Storage.MapDifficultyXConditionHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                MapDifficultyXConditionLocaleHotfix1200 hotfixLocale = new MapDifficultyXConditionLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    FailureDescriptionLang = hotfix.FailureDescription,
                };
                Storage.MapDifficultyXConditionHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void MawPowerHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            MawPowerHotfix1200 hotfix = new MawPowerHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.SpellID = packet.ReadUInt32("SpellID", indexes);
            hotfix.MawPowerRarityID = packet.ReadInt32("MawPowerRarityID", indexes);

            Storage.MawPowerHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ModifiedCraftingItemHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ModifiedCraftingItemHotfix1200 hotfix = new ModifiedCraftingItemHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ModifiedCraftingReagentItemID = packet.ReadInt32("ModifiedCraftingReagentItemID", indexes);
            hotfix.CraftingQualityID = packet.ReadInt32("CraftingQualityID", indexes);

            Storage.ModifiedCraftingItemHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ModifierTreeHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ModifierTreeHotfix1200 hotfix = new ModifierTreeHotfix1200();

            hotfix.ID = entry;
            hotfix.Parent = packet.ReadUInt32("Parent", indexes);
            hotfix.Operator = packet.ReadSByte("Operator", indexes);
            hotfix.Amount = packet.ReadSByte("Amount", indexes);
            hotfix.Type = packet.ReadInt32("Type", indexes);
            hotfix.Asset = packet.ReadInt32("Asset", indexes);
            hotfix.SecondaryAsset = packet.ReadInt32("SecondaryAsset", indexes);
            hotfix.TertiaryAsset = packet.ReadInt32("TertiaryAsset", indexes);

            Storage.ModifierTreeHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void MountHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            MountHotfix1200 hotfix = new MountHotfix1200();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.SourceText = packet.ReadCString("SourceText", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.MountTypeID = packet.ReadUInt16("MountTypeID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.SourceTypeEnum = packet.ReadSByte("SourceTypeEnum", indexes);
            hotfix.SourceSpellID = packet.ReadInt32("SourceSpellID", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.MountFlyRideHeight = packet.ReadSingle("MountFlyRideHeight", indexes);
            hotfix.UiModelSceneID = packet.ReadInt32("UiModelSceneID", indexes);
            hotfix.MountSpecialRiderAnimKitID = packet.ReadInt32("MountSpecialRiderAnimKitID", indexes);
            hotfix.MountSpecialSpellVisualKitID = packet.ReadInt32("MountSpecialSpellVisualKitID", indexes);

            Storage.MountHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                MountLocaleHotfix1200 hotfixLocale = new MountLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    SourceTextLang = hotfix.SourceText,
                    DescriptionLang = hotfix.Description,
                };
                Storage.MountHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void MountCapabilityHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            MountCapabilityHotfix1200 hotfix = new MountCapabilityHotfix1200();

            hotfix.ID = entry;
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ReqRidingSkill = packet.ReadUInt16("ReqRidingSkill", indexes);
            hotfix.ReqAreaID = packet.ReadUInt16("ReqAreaID", indexes);
            hotfix.ReqSpellAuraID = packet.ReadUInt32("ReqSpellAuraID", indexes);
            hotfix.ReqSpellKnownID = packet.ReadInt32("ReqSpellKnownID", indexes);
            hotfix.ModSpellAuraID = packet.ReadInt32("ModSpellAuraID", indexes);
            hotfix.ReqMapID = packet.ReadInt16("ReqMapID", indexes);
            hotfix.PlayerConditionID = packet.ReadInt32("PlayerConditionID", indexes);
            hotfix.FlightCapabilityID = packet.ReadInt32("FlightCapabilityID", indexes);
            hotfix.DriveCapabilityID = packet.ReadInt32("DriveCapabilityID", indexes);

            Storage.MountCapabilityHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void MountEquipmentHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            MountEquipmentHotfix1200 hotfix = new MountEquipmentHotfix1200();

            hotfix.ID = entry;
            hotfix.Item = packet.ReadInt32("Item", indexes);
            hotfix.BuffSpell = packet.ReadInt32("BuffSpell", indexes);
            hotfix.Unknown820 = packet.ReadInt32("Unknown820", indexes);
            hotfix.LearnedBySpell = packet.ReadUInt32("LearnedBySpell", indexes);

            Storage.MountEquipmentHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void MountTypeXCapabilityHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            MountTypeXCapabilityHotfix1200 hotfix = new MountTypeXCapabilityHotfix1200();

            hotfix.ID = entry;
            hotfix.MountTypeID = packet.ReadUInt16("MountTypeID", indexes);
            hotfix.MountCapabilityID = packet.ReadUInt16("MountCapabilityID", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);

            Storage.MountTypeXCapabilityHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void MountXDisplayHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            MountXDisplayHotfix1200 hotfix = new MountXDisplayHotfix1200();

            hotfix.ID = entry;
            hotfix.CreatureDisplayInfoID = packet.ReadInt32("CreatureDisplayInfoID", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.Unknown1100 = packet.ReadUInt16("Unknown1100", indexes);
            hotfix.MountID = packet.ReadUInt32("MountID", indexes);

            Storage.MountXDisplayHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void MovieHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            MovieHotfix1200 hotfix = new MovieHotfix1200();

            hotfix.ID = entry;
            hotfix.Volume = packet.ReadByte("Volume", indexes);
            hotfix.KeyID = packet.ReadByte("KeyID", indexes);
            hotfix.AudioFileDataID = packet.ReadUInt32("AudioFileDataID", indexes);
            hotfix.SubtitleFileDataID = packet.ReadUInt32("SubtitleFileDataID", indexes);
            hotfix.SubtitleFileFormat = packet.ReadUInt32("SubtitleFileFormat", indexes);

            Storage.MovieHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void MythicPlusSeasonHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            MythicPlusSeasonHotfix1200 hotfix = new MythicPlusSeasonHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.MilestoneSeason = packet.ReadInt32("MilestoneSeason", indexes);
            hotfix.StartTimeEvent = packet.ReadInt32("StartTimeEvent", indexes);
            hotfix.ExpansionLevel = packet.ReadInt32("ExpansionLevel", indexes);
            hotfix.HeroicLFGDungeonMinGear = packet.ReadInt32("HeroicLFGDungeonMinGear", indexes);

            Storage.MythicPlusSeasonHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void NameGenHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            NameGenHotfix1200 hotfix = new NameGenHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.RaceID = packet.ReadSByte("RaceID", indexes);
            hotfix.Sex = packet.ReadSByte("Sex", indexes);

            Storage.NameGenHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void NamesProfanityHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            NamesProfanityHotfix1200 hotfix = new NamesProfanityHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Language = packet.ReadSByte("Language", indexes);

            Storage.NamesProfanityHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void NamesReservedHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            NamesReservedHotfix1200 hotfix = new NamesReservedHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);

            Storage.NamesReservedHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void NamesReservedLocaleHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            NamesReservedLocaleHotfix1200 hotfix = new NamesReservedLocaleHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.LocaleMask = packet.ReadByte("LocaleMask", indexes);

            Storage.NamesReservedLocaleHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void NumTalentsAtLevelHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            NumTalentsAtLevelHotfix1200 hotfix = new NumTalentsAtLevelHotfix1200();

            hotfix.ID = entry;
            hotfix.NumTalents = packet.ReadInt32("NumTalents", indexes);
            hotfix.NumTalentsDeathKnight = packet.ReadInt32("NumTalentsDeathKnight", indexes);
            hotfix.NumTalentsDemonHunter = packet.ReadInt32("NumTalentsDemonHunter", indexes);
            hotfix.Unknown1115 = packet.ReadSingle("Unknown1115", indexes);

            Storage.NumTalentsAtLevelHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void OverrideSpellDataHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            OverrideSpellDataHotfix1200 hotfix = new OverrideSpellDataHotfix1200();

            hotfix.ID = entry;
            hotfix.Spells = new int?[10];
            for (int i = 0; i < 10; i++)
                hotfix.Spells[i] = packet.ReadInt32("Spells", indexes, i);
            hotfix.PlayerActionBarFileDataID = packet.ReadInt32("PlayerActionBarFileDataID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.OverrideSpellDataHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ParagonReputationHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ParagonReputationHotfix1200 hotfix = new ParagonReputationHotfix1200();

            hotfix.ID = entry;
            hotfix.FactionID = packet.ReadInt32("FactionID", indexes);
            hotfix.LevelThreshold = packet.ReadInt32("LevelThreshold", indexes);
            hotfix.QuestID = packet.ReadInt32("QuestID", indexes);

            Storage.ParagonReputationHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void PathHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            PathHotfix1200 hotfix = new PathHotfix1200();

            hotfix.ID = entry;
            hotfix.Type = packet.ReadByte("Type", indexes);
            hotfix.SplineType = packet.ReadByte("SplineType", indexes);
            hotfix.Red = packet.ReadByte("Red", indexes);
            hotfix.Green = packet.ReadByte("Green", indexes);
            hotfix.Blue = packet.ReadByte("Blue", indexes);
            hotfix.Alpha = packet.ReadByte("Alpha", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.PathHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void PathNodeHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            PathNodeHotfix1200 hotfix = new PathNodeHotfix1200();

            hotfix.ID = entry;
            hotfix.PathID = packet.ReadUInt16("PathID", indexes);
            hotfix.Sequence = packet.ReadInt16("Sequence", indexes);
            hotfix.LocationID = packet.ReadInt32("LocationID", indexes);

            Storage.PathNodeHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void PathPropertyHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            PathPropertyHotfix1200 hotfix = new PathPropertyHotfix1200();

            hotfix.ID = entry;
            hotfix.PathID = packet.ReadUInt16("PathID", indexes);
            hotfix.PropertyIndex = packet.ReadByte("PropertyIndex", indexes);
            hotfix.Value = packet.ReadInt32("Value", indexes);

            Storage.PathPropertyHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void PerksActivityHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            PerksActivityHotfix1200 hotfix = new PerksActivityHotfix1200();

            hotfix.ActivityName = packet.ReadCString("ActivityName", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.CriteriaTreeID = packet.ReadInt32("CriteriaTreeID", indexes);
            hotfix.ThresholdContributionAmount = packet.ReadInt32("ThresholdContributionAmount", indexes);
            hotfix.Supersedes = packet.ReadInt32("Supersedes", indexes);
            hotfix.Priority = packet.ReadInt32("Priority", indexes);

            Storage.PerksActivityHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                PerksActivityLocaleHotfix1200 hotfixLocale = new PerksActivityLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    ActivityNameLang = hotfix.ActivityName,
                    DescriptionLang = hotfix.Description,
                };
                Storage.PerksActivityHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void PhaseHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            PhaseHotfix1200 hotfix = new PhaseHotfix1200();

            hotfix.ID = entry;
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.PhaseHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void PhaseXPhaseGroupHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            PhaseXPhaseGroupHotfix1200 hotfix = new PhaseXPhaseGroupHotfix1200();

            hotfix.ID = entry;
            hotfix.PhaseID = packet.ReadUInt16("PhaseID", indexes);
            hotfix.PhaseGroupID = packet.ReadUInt32("PhaseGroupID", indexes);

            Storage.PhaseXPhaseGroupHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void PlayerConditionHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            PlayerConditionHotfix1200 hotfix = new PlayerConditionHotfix1200();

            hotfix.ID = entry;
            hotfix.RaceMask = packet.ReadInt64("RaceMask", indexes);
            hotfix.FailureDescription = packet.ReadCString("FailureDescription", indexes);
            hotfix.MinLevel = packet.ReadUInt16("MinLevel", indexes);
            hotfix.MaxLevel = packet.ReadUInt16("MaxLevel", indexes);
            hotfix.ClassMask = packet.ReadInt32("ClassMask", indexes);
            hotfix.SkillLogic = packet.ReadUInt32("SkillLogic", indexes);
            hotfix.LanguageID = packet.ReadInt32("LanguageID", indexes);
            hotfix.MinLanguage = packet.ReadByte("MinLanguage", indexes);
            hotfix.MaxLanguage = packet.ReadInt32("MaxLanguage", indexes);
            hotfix.MaxFactionID = packet.ReadUInt16("MaxFactionID", indexes);
            hotfix.MaxReputation = packet.ReadByte("MaxReputation", indexes);
            hotfix.ReputationLogic = packet.ReadUInt32("ReputationLogic", indexes);
            hotfix.CurrentPvpFaction = packet.ReadSByte("CurrentPvpFaction", indexes);
            hotfix.PvpMedal = packet.ReadByte("PvpMedal", indexes);
            hotfix.PrevQuestLogic = packet.ReadUInt32("PrevQuestLogic", indexes);
            hotfix.CurrQuestLogic = packet.ReadUInt32("CurrQuestLogic", indexes);
            hotfix.CurrentCompletedQuestLogic = packet.ReadUInt32("CurrentCompletedQuestLogic", indexes);
            hotfix.SpellLogic = packet.ReadUInt32("SpellLogic", indexes);
            hotfix.ItemLogic = packet.ReadUInt32("ItemLogic", indexes);
            hotfix.ItemFlags = packet.ReadInt32("ItemFlags", indexes);
            hotfix.AuraSpellLogic = packet.ReadUInt32("AuraSpellLogic", indexes);
            hotfix.WorldStateExpressionID = packet.ReadUInt16("WorldStateExpressionID", indexes);
            hotfix.WeatherID = packet.ReadInt32("WeatherID", indexes);
            hotfix.PartyStatus = packet.ReadByte("PartyStatus", indexes);
            hotfix.LifetimeMaxPVPRank = packet.ReadSByte("LifetimeMaxPVPRank", indexes);
            hotfix.AchievementLogic = packet.ReadUInt32("AchievementLogic", indexes);
            hotfix.Gender = packet.ReadSByte("Gender", indexes);
            hotfix.NativeGender = packet.ReadSByte("NativeGender", indexes);
            hotfix.AreaLogic = packet.ReadUInt32("AreaLogic", indexes);
            hotfix.LfgLogic = packet.ReadUInt32("LfgLogic", indexes);
            hotfix.CurrencyLogic = packet.ReadUInt32("CurrencyLogic", indexes);
            hotfix.QuestKillID = packet.ReadInt32("QuestKillID", indexes);
            hotfix.QuestKillLogic = packet.ReadUInt32("QuestKillLogic", indexes);
            hotfix.MinExpansionLevel = packet.ReadSByte("MinExpansionLevel", indexes);
            hotfix.MaxExpansionLevel = packet.ReadSByte("MaxExpansionLevel", indexes);
            hotfix.MinAvgItemLevel = packet.ReadInt32("MinAvgItemLevel", indexes);
            hotfix.MaxAvgItemLevel = packet.ReadInt32("MaxAvgItemLevel", indexes);
            hotfix.MinAvgEquippedItemLevel = packet.ReadUInt16("MinAvgEquippedItemLevel", indexes);
            hotfix.MaxAvgEquippedItemLevel = packet.ReadUInt16("MaxAvgEquippedItemLevel", indexes);
            hotfix.PhaseUseFlags = packet.ReadInt32("PhaseUseFlags", indexes);
            hotfix.PhaseID = packet.ReadUInt16("PhaseID", indexes);
            hotfix.PhaseGroupID = packet.ReadUInt32("PhaseGroupID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ChrSpecializationIndex = packet.ReadSByte("ChrSpecializationIndex", indexes);
            hotfix.ChrSpecializationRole = packet.ReadSByte("ChrSpecializationRole", indexes);
            hotfix.ModifierTreeID = packet.ReadUInt32("ModifierTreeID", indexes);
            hotfix.PowerType = packet.ReadSByte("PowerType", indexes);
            hotfix.PowerTypeComp = packet.ReadByte("PowerTypeComp", indexes);
            hotfix.PowerTypeValue = packet.ReadSByte("PowerTypeValue", indexes);
            hotfix.WeaponSubclassMask = packet.ReadInt32("WeaponSubclassMask", indexes);
            hotfix.MaxGuildLevel = packet.ReadByte("MaxGuildLevel", indexes);
            hotfix.MinGuildLevel = packet.ReadByte("MinGuildLevel", indexes);
            hotfix.MaxExpansionTier = packet.ReadSByte("MaxExpansionTier", indexes);
            hotfix.MinExpansionTier = packet.ReadSByte("MinExpansionTier", indexes);
            hotfix.MinPVPRank = packet.ReadSByte("MinPVPRank", indexes);
            hotfix.MaxPVPRank = packet.ReadSByte("MaxPVPRank", indexes);
            hotfix.ContentTuningID = packet.ReadInt32("ContentTuningID", indexes);
            hotfix.CovenantID = packet.ReadInt32("CovenantID", indexes);
            hotfix.TraitNodeEntryLogic = packet.ReadUInt32("TraitNodeEntryLogic", indexes);
            hotfix.SkillID = new ushort?[4];
            for (int i = 0; i < 4; i++)
                hotfix.SkillID[i] = packet.ReadUInt16("SkillID", indexes, i);
            hotfix.MinSkill = new ushort?[4];
            for (int i = 0; i < 4; i++)
                hotfix.MinSkill[i] = packet.ReadUInt16("MinSkill", indexes, i);
            hotfix.MaxSkill = new ushort?[4];
            for (int i = 0; i < 4; i++)
                hotfix.MaxSkill[i] = packet.ReadUInt16("MaxSkill", indexes, i);
            hotfix.MinFactionID = new uint?[3];
            for (int i = 0; i < 3; i++)
                hotfix.MinFactionID[i] = packet.ReadUInt32("MinFactionID", indexes, i);
            hotfix.MinReputation = new byte?[3];
            for (int i = 0; i < 3; i++)
                hotfix.MinReputation[i] = packet.ReadByte("MinReputation", indexes, i);
            hotfix.PrevQuestID = new int?[4];
            for (int i = 0; i < 4; i++)
                hotfix.PrevQuestID[i] = packet.ReadInt32("PrevQuestID", indexes, i);
            hotfix.CurrQuestID = new int?[4];
            for (int i = 0; i < 4; i++)
                hotfix.CurrQuestID[i] = packet.ReadInt32("CurrQuestID", indexes, i);
            hotfix.CurrentCompletedQuestID = new int?[4];
            for (int i = 0; i < 4; i++)
                hotfix.CurrentCompletedQuestID[i] = packet.ReadInt32("CurrentCompletedQuestID", indexes, i);
            hotfix.SpellID = new int?[4];
            for (int i = 0; i < 4; i++)
                hotfix.SpellID[i] = packet.ReadInt32("SpellID", indexes, i);
            hotfix.ItemID = new int?[4];
            for (int i = 0; i < 4; i++)
                hotfix.ItemID[i] = packet.ReadInt32("ItemID", indexes, i);
            hotfix.ItemCount = new uint?[4];
            for (int i = 0; i < 4; i++)
                hotfix.ItemCount[i] = packet.ReadUInt32("ItemCount", indexes, i);
            hotfix.Explored = new ushort?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Explored[i] = packet.ReadUInt16("Explored", indexes, i);
            hotfix.Time = new uint?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Time[i] = packet.ReadUInt32("Time", indexes, i);
            hotfix.AuraSpellID = new int?[4];
            for (int i = 0; i < 4; i++)
                hotfix.AuraSpellID[i] = packet.ReadInt32("AuraSpellID", indexes, i);
            hotfix.AuraStacks = new byte?[4];
            for (int i = 0; i < 4; i++)
                hotfix.AuraStacks[i] = packet.ReadByte("AuraStacks", indexes, i);
            hotfix.Achievement = new uint?[4];
            for (int i = 0; i < 4; i++)
                hotfix.Achievement[i] = packet.ReadUInt32("Achievement", indexes, i);
            hotfix.AreaID = new ushort?[4];
            for (int i = 0; i < 4; i++)
                hotfix.AreaID[i] = packet.ReadUInt16("AreaID", indexes, i);
            hotfix.LfgStatus = new byte?[4];
            for (int i = 0; i < 4; i++)
                hotfix.LfgStatus[i] = packet.ReadByte("LfgStatus", indexes, i);
            hotfix.LfgCompare = new byte?[4];
            for (int i = 0; i < 4; i++)
                hotfix.LfgCompare[i] = packet.ReadByte("LfgCompare", indexes, i);
            hotfix.LfgValue = new uint?[4];
            for (int i = 0; i < 4; i++)
                hotfix.LfgValue[i] = packet.ReadUInt32("LfgValue", indexes, i);
            hotfix.CurrencyID = new uint?[4];
            for (int i = 0; i < 4; i++)
                hotfix.CurrencyID[i] = packet.ReadUInt32("CurrencyID", indexes, i);
            hotfix.CurrencyCount = new uint?[4];
            for (int i = 0; i < 4; i++)
                hotfix.CurrencyCount[i] = packet.ReadUInt32("CurrencyCount", indexes, i);
            hotfix.QuestKillMonster = new uint?[6];
            for (int i = 0; i < 6; i++)
                hotfix.QuestKillMonster[i] = packet.ReadUInt32("QuestKillMonster", indexes, i);
            hotfix.MovementFlags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.MovementFlags[i] = packet.ReadInt32("MovementFlags", indexes, i);
            hotfix.TraitNodeEntryID = new int?[4];
            for (int i = 0; i < 4; i++)
                hotfix.TraitNodeEntryID[i] = packet.ReadInt32("TraitNodeEntryID", indexes, i);
            hotfix.TraitNodeEntryMinRank = new ushort?[4];
            for (int i = 0; i < 4; i++)
                hotfix.TraitNodeEntryMinRank[i] = packet.ReadUInt16("TraitNodeEntryMinRank", indexes, i);
            hotfix.TraitNodeEntryMaxRank = new ushort?[4];
            for (int i = 0; i < 4; i++)
                hotfix.TraitNodeEntryMaxRank[i] = packet.ReadUInt16("TraitNodeEntryMaxRank", indexes, i);

            Storage.PlayerConditionHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                PlayerConditionLocaleHotfix1200 hotfixLocale = new PlayerConditionLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    FailureDescriptionLang = hotfix.FailureDescription,
                };
                Storage.PlayerConditionHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void PlayerDataElementAccountHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            PlayerDataElementAccountHotfix1200 hotfix = new PlayerDataElementAccountHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.StorageIndex = packet.ReadInt32("StorageIndex", indexes);
            hotfix.Type = packet.ReadInt32("Type", indexes);
            hotfix.Unknown1125 = packet.ReadInt32("Unknown1125", indexes);

            Storage.PlayerDataElementAccountHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void PlayerDataElementCharacterHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            PlayerDataElementCharacterHotfix1200 hotfix = new PlayerDataElementCharacterHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.StorageIndex = packet.ReadInt32("StorageIndex", indexes);
            hotfix.Type = packet.ReadInt32("Type", indexes);
            hotfix.Unknown1125 = packet.ReadInt32("Unknown1125", indexes);

            Storage.PlayerDataElementCharacterHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void PlayerDataFlagAccountHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            PlayerDataFlagAccountHotfix1200 hotfix = new PlayerDataFlagAccountHotfix1200();

            hotfix.ID = entry;
            hotfix.StorageIndex = packet.ReadInt32("StorageIndex", indexes);
            hotfix.Unknown1107 = packet.ReadInt32("Unknown1107", indexes);
            hotfix.Unknown1125 = packet.ReadInt32("Unknown1125", indexes);

            Storage.PlayerDataFlagAccountHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void PlayerDataFlagCharacterHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            PlayerDataFlagCharacterHotfix1200 hotfix = new PlayerDataFlagCharacterHotfix1200();

            hotfix.ID = entry;
            hotfix.StorageIndex = packet.ReadInt32("StorageIndex", indexes);
            hotfix.Unknown1107 = packet.ReadInt32("Unknown1107", indexes);
            hotfix.Unknown1125 = packet.ReadInt32("Unknown1125", indexes);

            Storage.PlayerDataFlagCharacterHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void PowerDisplayHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            PowerDisplayHotfix1200 hotfix = new PowerDisplayHotfix1200();

            hotfix.ID = entry;
            hotfix.GlobalStringBaseTag = packet.ReadCString("GlobalStringBaseTag", indexes);
            hotfix.ActualType = packet.ReadSByte("ActualType", indexes);
            hotfix.Red = packet.ReadByte("Red", indexes);
            hotfix.Green = packet.ReadByte("Green", indexes);
            hotfix.Blue = packet.ReadByte("Blue", indexes);

            Storage.PowerDisplayHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void PowerTypeHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            PowerTypeHotfix1200 hotfix = new PowerTypeHotfix1200();

            hotfix.NameGlobalStringTag = packet.ReadCString("NameGlobalStringTag", indexes);
            hotfix.CostGlobalStringTag = packet.ReadCString("CostGlobalStringTag", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.PowerTypeEnum = packet.ReadSByte("PowerTypeEnum", indexes);
            hotfix.MinPower = packet.ReadInt32("MinPower", indexes);
            hotfix.MaxBasePower = packet.ReadInt32("MaxBasePower", indexes);
            hotfix.CenterPower = packet.ReadInt32("CenterPower", indexes);
            hotfix.DefaultPower = packet.ReadInt32("DefaultPower", indexes);
            hotfix.DisplayModifier = packet.ReadInt32("DisplayModifier", indexes);
            hotfix.RegenInterruptTimeMS = packet.ReadInt32("RegenInterruptTimeMS", indexes);
            hotfix.RegenPeace = packet.ReadSingle("RegenPeace", indexes);
            hotfix.RegenCombat = packet.ReadSingle("RegenCombat", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.PowerTypeHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void PrestigeLevelInfoHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            PrestigeLevelInfoHotfix1200 hotfix = new PrestigeLevelInfoHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.PrestigeLevel = packet.ReadInt32("PrestigeLevel", indexes);
            hotfix.BadgeTextureFileDataID = packet.ReadInt32("BadgeTextureFileDataID", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.AwardedAchievementID = packet.ReadInt32("AwardedAchievementID", indexes);

            Storage.PrestigeLevelInfoHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                PrestigeLevelInfoLocaleHotfix1200 hotfixLocale = new PrestigeLevelInfoLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.PrestigeLevelInfoHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void PvpDifficultyHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            PvpDifficultyHotfix1200 hotfix = new PvpDifficultyHotfix1200();

            hotfix.ID = entry;
            hotfix.RangeIndex = packet.ReadByte("RangeIndex", indexes);
            hotfix.MinLevel = packet.ReadByte("MinLevel", indexes);
            hotfix.MaxLevel = packet.ReadByte("MaxLevel", indexes);
            hotfix.MapID = packet.ReadUInt32("MapID", indexes);

            Storage.PvpDifficultyHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void PvpSeasonHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            PvpSeasonHotfix1200 hotfix = new PvpSeasonHotfix1200();

            hotfix.ID = entry;
            hotfix.MilestoneSeason = packet.ReadInt32("MilestoneSeason", indexes);
            hotfix.AllianceAchievementID = packet.ReadInt32("AllianceAchievementID", indexes);
            hotfix.HordeAchievementID = packet.ReadInt32("HordeAchievementID", indexes);

            Storage.PvpSeasonHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void PvpStatHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            PvpStatHotfix1200 hotfix = new PvpStatHotfix1200();

            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.MapID = packet.ReadUInt32("MapID", indexes);

            Storage.PvpStatHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                PvpStatLocaleHotfix1200 hotfixLocale = new PvpStatLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                };
                Storage.PvpStatHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void PvpTalentHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            PvpTalentHotfix1200 hotfix = new PvpTalentHotfix1200();

            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.SpecID = packet.ReadUInt32("SpecID", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.OverridesSpellID = packet.ReadInt32("OverridesSpellID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ActionBarSpellID = packet.ReadInt32("ActionBarSpellID", indexes);
            hotfix.PvpTalentCategoryID = packet.ReadInt32("PvpTalentCategoryID", indexes);
            hotfix.LevelRequired = packet.ReadInt32("LevelRequired", indexes);
            hotfix.PlayerConditionID = packet.ReadInt32("PlayerConditionID", indexes);

            Storage.PvpTalentHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                PvpTalentLocaleHotfix1200 hotfixLocale = new PvpTalentLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                };
                Storage.PvpTalentHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void PvpTalentCategoryHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            PvpTalentCategoryHotfix1200 hotfix = new PvpTalentCategoryHotfix1200();

            hotfix.ID = entry;
            hotfix.TalentSlotMask = packet.ReadByte("TalentSlotMask", indexes);

            Storage.PvpTalentCategoryHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void PvpTalentSlotUnlockHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            PvpTalentSlotUnlockHotfix1200 hotfix = new PvpTalentSlotUnlockHotfix1200();

            hotfix.ID = entry;
            hotfix.Slot = packet.ReadSByte("Slot", indexes);
            hotfix.LevelRequired = packet.ReadInt32("LevelRequired", indexes);
            hotfix.DeathKnightLevelRequired = packet.ReadInt32("DeathKnightLevelRequired", indexes);
            hotfix.DemonHunterLevelRequired = packet.ReadInt32("DemonHunterLevelRequired", indexes);

            Storage.PvpTalentSlotUnlockHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void PvpTierHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            PvpTierHotfix1200 hotfix = new PvpTierHotfix1200();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.MinRating = packet.ReadInt16("MinRating", indexes);
            hotfix.MaxRating = packet.ReadInt16("MaxRating", indexes);
            hotfix.PrevTier = packet.ReadInt32("PrevTier", indexes);
            hotfix.NextTier = packet.ReadInt32("NextTier", indexes);
            hotfix.BracketID = packet.ReadByte("BracketID", indexes);
            hotfix.Rank = packet.ReadSByte("Rank", indexes);
            hotfix.RankIconFileDataID = packet.ReadInt32("RankIconFileDataID", indexes);

            Storage.PvpTierHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                PvpTierLocaleHotfix1200 hotfixLocale = new PvpTierLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.PvpTierHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void QuestFactionRewardHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            QuestFactionRewardHotfix1200 hotfix = new QuestFactionRewardHotfix1200();

            hotfix.ID = entry;
            hotfix.Difficulty = new short?[10];
            for (int i = 0; i < 10; i++)
                hotfix.Difficulty[i] = packet.ReadInt16("Difficulty", indexes, i);

            Storage.QuestFactionRewardHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void QuestInfoHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            QuestInfoHotfix1200 hotfix = new QuestInfoHotfix1200();

            hotfix.ID = entry;
            hotfix.InfoName = packet.ReadCString("InfoName", indexes);
            hotfix.Type = packet.ReadSByte("Type", indexes);
            hotfix.Modifiers = packet.ReadInt32("Modifiers", indexes);
            hotfix.Profession = packet.ReadUInt16("Profession", indexes);

            Storage.QuestInfoHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                QuestInfoLocaleHotfix1200 hotfixLocale = new QuestInfoLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    InfoNameLang = hotfix.InfoName,
                };
                Storage.QuestInfoHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void QuestLineXQuestHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            QuestLineXQuestHotfix1200 hotfix = new QuestLineXQuestHotfix1200();

            hotfix.ID = entry;
            hotfix.QuestLineID = packet.ReadUInt32("QuestLineID", indexes);
            hotfix.QuestID = packet.ReadUInt32("QuestID", indexes);
            hotfix.OrderIndex = packet.ReadUInt32("OrderIndex", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.Unknown1110 = packet.ReadInt32("Unknown1110", indexes);

            Storage.QuestLineXQuestHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void QuestMoneyRewardHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            QuestMoneyRewardHotfix1200 hotfix = new QuestMoneyRewardHotfix1200();

            hotfix.ID = entry;
            hotfix.Difficulty = new uint?[10];
            for (int i = 0; i < 10; i++)
                hotfix.Difficulty[i] = packet.ReadUInt32("Difficulty", indexes, i);

            Storage.QuestMoneyRewardHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void QuestPackageItemHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            QuestPackageItemHotfix1200 hotfix = new QuestPackageItemHotfix1200();

            hotfix.ID = entry;
            hotfix.PackageID = packet.ReadUInt16("PackageID", indexes);
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);
            hotfix.ItemQuantity = packet.ReadUInt32("ItemQuantity", indexes);
            hotfix.DisplayType = packet.ReadByte("DisplayType", indexes);

            Storage.QuestPackageItemHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void QuestSortHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            QuestSortHotfix1200 hotfix = new QuestSortHotfix1200();

            hotfix.ID = entry;
            hotfix.SortName = packet.ReadCString("SortName", indexes);
            hotfix.UiOrderIndex = packet.ReadSByte("UiOrderIndex", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.QuestSortHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                QuestSortLocaleHotfix1200 hotfixLocale = new QuestSortLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    SortNameLang = hotfix.SortName,
                };
                Storage.QuestSortHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void QuestV2Handler1200(Packet packet, uint entry, params object[] indexes)
        {
            QuestV2Hotfix1200 hotfix = new QuestV2Hotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.UniqueBitFlag = packet.ReadInt32("UniqueBitFlag", indexes);
            hotfix.UiQuestDetailsTheme = packet.ReadInt32("UiQuestDetailsTheme", indexes);

            Storage.QuestV2Hotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void QuestXpHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            QuestXpHotfix1200 hotfix = new QuestXpHotfix1200();

            hotfix.ID = entry;
            hotfix.Difficulty = new ushort?[10];
            for (int i = 0; i < 10; i++)
                hotfix.Difficulty[i] = packet.ReadUInt16("Difficulty", indexes, i);

            Storage.QuestXpHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void RandPropPointsHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            RandPropPointsHotfix1200 hotfix = new RandPropPointsHotfix1200();

            hotfix.ID = entry;
            hotfix.DamageReplaceStatF = packet.ReadSingle("DamageReplaceStatF", indexes);
            hotfix.DamageSecondaryF = packet.ReadSingle("DamageSecondaryF", indexes);
            hotfix.DamageReplaceStat = packet.ReadInt32("DamageReplaceStat", indexes);
            hotfix.DamageSecondary = packet.ReadInt32("DamageSecondary", indexes);
            hotfix.EpicF = new float?[5];
            for (int i = 0; i < 5; i++)
                hotfix.EpicF[i] = packet.ReadSingle("EpicF", indexes, i);
            hotfix.SuperiorF = new float?[5];
            for (int i = 0; i < 5; i++)
                hotfix.SuperiorF[i] = packet.ReadSingle("SuperiorF", indexes, i);
            hotfix.GoodF = new float?[5];
            for (int i = 0; i < 5; i++)
                hotfix.GoodF[i] = packet.ReadSingle("GoodF", indexes, i);
            hotfix.Epic = new uint?[5];
            for (int i = 0; i < 5; i++)
                hotfix.Epic[i] = packet.ReadUInt32("Epic", indexes, i);
            hotfix.Superior = new uint?[5];
            for (int i = 0; i < 5; i++)
                hotfix.Superior[i] = packet.ReadUInt32("Superior", indexes, i);
            hotfix.Good = new uint?[5];
            for (int i = 0; i < 5; i++)
                hotfix.Good[i] = packet.ReadUInt32("Good", indexes, i);

            Storage.RandPropPointsHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void RewardPackHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            RewardPackHotfix1200 hotfix = new RewardPackHotfix1200();

            hotfix.ID = entry;
            hotfix.CharTitleID = packet.ReadInt32("CharTitleID", indexes);
            hotfix.Money = packet.ReadUInt32("Money", indexes);
            hotfix.ArtifactXPDifficulty = packet.ReadSByte("ArtifactXPDifficulty", indexes);
            hotfix.ArtifactXPMultiplier = packet.ReadSingle("ArtifactXPMultiplier", indexes);
            hotfix.ArtifactXPCategoryID = packet.ReadByte("ArtifactXPCategoryID", indexes);
            hotfix.TreasurePickerID = packet.ReadUInt32("TreasurePickerID", indexes);

            Storage.RewardPackHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void RewardPackXCurrencyTypeHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            RewardPackXCurrencyTypeHotfix1200 hotfix = new RewardPackXCurrencyTypeHotfix1200();

            hotfix.ID = entry;
            hotfix.CurrencyTypeID = packet.ReadUInt32("CurrencyTypeID", indexes);
            hotfix.Quantity = packet.ReadInt32("Quantity", indexes);
            hotfix.RewardPackID = packet.ReadUInt32("RewardPackID", indexes);

            Storage.RewardPackXCurrencyTypeHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void RewardPackXItemHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            RewardPackXItemHotfix1200 hotfix = new RewardPackXItemHotfix1200();

            hotfix.ID = entry;
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);
            hotfix.ItemQuantity = packet.ReadInt32("ItemQuantity", indexes);
            hotfix.RewardPackID = packet.ReadUInt32("RewardPackID", indexes);

            Storage.RewardPackXItemHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ScenarioHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ScenarioHotfix1200 hotfix = new ScenarioHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.AreaTableID = packet.ReadUInt16("AreaTableID", indexes);
            hotfix.Type = packet.ReadInt32("Type", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.UiTextureKitID = packet.ReadUInt32("UiTextureKitID", indexes);

            Storage.ScenarioHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ScenarioLocaleHotfix1200 hotfixLocale = new ScenarioLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ScenarioHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ScenarioStepHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ScenarioStepHotfix1200 hotfix = new ScenarioStepHotfix1200();

            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.Title = packet.ReadCString("Title", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ScenarioID = packet.ReadUInt16("ScenarioID", indexes);
            hotfix.Criteriatreeid = packet.ReadUInt32("Criteriatreeid", indexes);
            hotfix.RewardQuestID = packet.ReadInt32("RewardQuestID", indexes);
            hotfix.RelatedStep = packet.ReadInt32("RelatedStep", indexes);
            hotfix.Supersedes = packet.ReadUInt16("Supersedes", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.VisibilityPlayerConditionID = packet.ReadUInt32("VisibilityPlayerConditionID", indexes);
            hotfix.WidgetSetID = packet.ReadUInt16("WidgetSetID", indexes);

            Storage.ScenarioStepHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ScenarioStepLocaleHotfix1200 hotfixLocale = new ScenarioStepLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                    TitleLang = hotfix.Title,
                };
                Storage.ScenarioStepHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SceneScriptHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SceneScriptHotfix1200 hotfix = new SceneScriptHotfix1200();

            hotfix.ID = entry;
            hotfix.FirstSceneScriptID = packet.ReadUInt32("FirstSceneScriptID", indexes);
            hotfix.NextSceneScriptID = packet.ReadUInt32("NextSceneScriptID", indexes);
            hotfix.Unknown915 = packet.ReadInt32("Unknown915", indexes);

            Storage.SceneScriptHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SceneScriptGlobalTextHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SceneScriptGlobalTextHotfix1200 hotfix = new SceneScriptGlobalTextHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Script = packet.ReadCString("Script", indexes);

            Storage.SceneScriptGlobalTextHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SceneScriptPackageHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SceneScriptPackageHotfix1200 hotfix = new SceneScriptPackageHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.Unknown915 = packet.ReadInt32("Unknown915", indexes);

            Storage.SceneScriptPackageHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SceneScriptTextHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SceneScriptTextHotfix1200 hotfix = new SceneScriptTextHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Script = packet.ReadCString("Script", indexes);

            Storage.SceneScriptTextHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void ServerMessagesHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ServerMessagesHotfix1200 hotfix = new ServerMessagesHotfix1200();

            hotfix.ID = entry;
            hotfix.Text = packet.ReadCString("Text", indexes);

            Storage.ServerMessagesHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ServerMessagesLocaleHotfix1200 hotfixLocale = new ServerMessagesLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    TextLang = hotfix.Text,
                };
                Storage.ServerMessagesHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SkillLineHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SkillLineHotfix1200 hotfix = new SkillLineHotfix1200();

            hotfix.DisplayName = packet.ReadCString("DisplayName", indexes);
            hotfix.AlternateVerb = packet.ReadCString("AlternateVerb", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.HordeDisplayName = packet.ReadCString("HordeDisplayName", indexes);
            hotfix.OverrideSourceInfoDisplayName = packet.ReadCString("OverrideSourceInfoDisplayName", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.CategoryID = packet.ReadSByte("CategoryID", indexes);
            hotfix.SpellIconFileID = packet.ReadInt32("SpellIconFileID", indexes);
            hotfix.CanLink = packet.ReadSByte("CanLink", indexes);
            hotfix.ParentSkillLineID = packet.ReadUInt32("ParentSkillLineID", indexes);
            hotfix.ParentTierIndex = packet.ReadInt32("ParentTierIndex", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.SpellBookSpellID = packet.ReadInt32("SpellBookSpellID", indexes);
            hotfix.ExpansionNameSharedStringID = packet.ReadInt32("ExpansionNameSharedStringID", indexes);
            hotfix.HordeExpansionNameSharedStringID = packet.ReadInt32("HordeExpansionNameSharedStringID", indexes);

            Storage.SkillLineHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SkillLineLocaleHotfix1200 hotfixLocale = new SkillLineLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    DisplayNameLang = hotfix.DisplayName,
                    AlternateVerbLang = hotfix.AlternateVerb,
                    DescriptionLang = hotfix.Description,
                    HordeDisplayNameLang = hotfix.HordeDisplayName,
                };
                Storage.SkillLineHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SkillLineAbilityHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SkillLineAbilityHotfix1200 hotfix = new SkillLineAbilityHotfix1200();

            hotfix.RaceMask = packet.ReadInt64("RaceMask", indexes);
            hotfix.AbilityVerb = packet.ReadCString("AbilityVerb", indexes);
            hotfix.AbilityAllVerb = packet.ReadCString("AbilityAllVerb", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.SkillLine = packet.ReadUInt16("SkillLine", indexes);
            hotfix.Spell = packet.ReadInt32("Spell", indexes);
            hotfix.MinSkillLineRank = packet.ReadInt16("MinSkillLineRank", indexes);
            hotfix.ClassMask = packet.ReadInt32("ClassMask", indexes);
            hotfix.SupercedesSpell = packet.ReadInt32("SupercedesSpell", indexes);
            hotfix.AcquireMethod = packet.ReadInt32("AcquireMethod", indexes);
            hotfix.TrivialSkillLineRankHigh = packet.ReadInt16("TrivialSkillLineRankHigh", indexes);
            hotfix.TrivialSkillLineRankLow = packet.ReadInt16("TrivialSkillLineRankLow", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.NumSkillUps = packet.ReadSByte("NumSkillUps", indexes);
            hotfix.UniqueBit = packet.ReadInt16("UniqueBit", indexes);
            hotfix.TradeSkillCategoryID = packet.ReadInt16("TradeSkillCategoryID", indexes);
            hotfix.SkillupSkillLineID = packet.ReadInt16("SkillupSkillLineID", indexes);

            Storage.SkillLineAbilityHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SkillLineAbilityLocaleHotfix1200 hotfixLocale = new SkillLineAbilityLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    AbilityVerbLang = hotfix.AbilityVerb,
                    AbilityAllVerbLang = hotfix.AbilityAllVerb,
                };
                Storage.SkillLineAbilityHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SkillLineXTraitTreeHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SkillLineXTraitTreeHotfix1200 hotfix = new SkillLineXTraitTreeHotfix1200();

            hotfix.ID = entry;
            hotfix.SkillLineID = packet.ReadUInt32("SkillLineID", indexes);
            hotfix.TraitTreeID = packet.ReadInt32("TraitTreeID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);

            Storage.SkillLineXTraitTreeHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SkillRaceClassInfoHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SkillRaceClassInfoHotfix1200 hotfix = new SkillRaceClassInfoHotfix1200();

            hotfix.ID = entry;
            hotfix.RaceMask = packet.ReadInt64("RaceMask", indexes);
            hotfix.SkillID = packet.ReadUInt16("SkillID", indexes);
            hotfix.ClassMask = packet.ReadInt32("ClassMask", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.Availability = packet.ReadInt32("Availability", indexes);
            hotfix.MinLevel = packet.ReadSByte("MinLevel", indexes);
            hotfix.SkillTierID = packet.ReadInt16("SkillTierID", indexes);

            Storage.SkillRaceClassInfoHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SoulbindConduitRankHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SoulbindConduitRankHotfix1200 hotfix = new SoulbindConduitRankHotfix1200();

            hotfix.ID = entry;
            hotfix.RankIndex = packet.ReadInt32("RankIndex", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.AuraPointsOverride = packet.ReadSingle("AuraPointsOverride", indexes);
            hotfix.SoulbindConduitID = packet.ReadUInt32("SoulbindConduitID", indexes);

            Storage.SoulbindConduitRankHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SoundKitHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SoundKitHotfix1200 hotfix = new SoundKitHotfix1200();

            hotfix.ID = entry;
            hotfix.SoundType = packet.ReadInt32("SoundType", indexes);
            hotfix.VolumeFloat = packet.ReadSingle("VolumeFloat", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.MinDistance = packet.ReadSingle("MinDistance", indexes);
            hotfix.DistanceCutoff = packet.ReadSingle("DistanceCutoff", indexes);
            hotfix.EAXDef = packet.ReadByte("EAXDef", indexes);
            hotfix.SoundKitAdvancedID = packet.ReadUInt32("SoundKitAdvancedID", indexes);
            hotfix.VolumeVariationPlus = packet.ReadSingle("VolumeVariationPlus", indexes);
            hotfix.VolumeVariationMinus = packet.ReadSingle("VolumeVariationMinus", indexes);
            hotfix.PitchVariationPlus = packet.ReadSingle("PitchVariationPlus", indexes);
            hotfix.PitchVariationMinus = packet.ReadSingle("PitchVariationMinus", indexes);
            hotfix.DialogType = packet.ReadSByte("DialogType", indexes);
            hotfix.PitchAdjust = packet.ReadSingle("PitchAdjust", indexes);
            hotfix.BusOverwriteID = packet.ReadUInt16("BusOverwriteID", indexes);
            hotfix.MaxInstances = packet.ReadByte("MaxInstances", indexes);
            hotfix.SoundMixGroupID = packet.ReadUInt32("SoundMixGroupID", indexes);

            Storage.SoundKitHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpecializationSpellsHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpecializationSpellsHotfix1200 hotfix = new SpecializationSpellsHotfix1200();

            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.SpecID = packet.ReadUInt16("SpecID", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.OverridesSpellID = packet.ReadInt32("OverridesSpellID", indexes);
            hotfix.DisplayOrder = packet.ReadByte("DisplayOrder", indexes);

            Storage.SpecializationSpellsHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SpecializationSpellsLocaleHotfix1200 hotfixLocale = new SpecializationSpellsLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                };
                Storage.SpecializationSpellsHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SpecSetMemberHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpecSetMemberHotfix1200 hotfix = new SpecSetMemberHotfix1200();

            hotfix.ID = entry;
            hotfix.ChrSpecializationID = packet.ReadInt32("ChrSpecializationID", indexes);
            hotfix.SpecSetID = packet.ReadUInt32("SpecSetID", indexes);

            Storage.SpecSetMemberHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellAuraOptionsHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellAuraOptionsHotfix1200 hotfix = new SpellAuraOptionsHotfix1200();

            hotfix.ID = entry;
            hotfix.DifficultyID = packet.ReadInt16("DifficultyID", indexes);
            hotfix.CumulativeAura = packet.ReadUInt16("CumulativeAura", indexes);
            hotfix.ProcCategoryRecovery = packet.ReadInt32("ProcCategoryRecovery", indexes);
            hotfix.ProcChance = packet.ReadByte("ProcChance", indexes);
            hotfix.ProcCharges = packet.ReadInt32("ProcCharges", indexes);
            hotfix.SpellProcsPerMinuteID = packet.ReadUInt16("SpellProcsPerMinuteID", indexes);
            hotfix.ProcTypeMask = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.ProcTypeMask[i] = packet.ReadInt32("ProcTypeMask", indexes, i);
            hotfix.SpellID = packet.ReadUInt32("SpellID", indexes);

            Storage.SpellAuraOptionsHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellAuraRestrictionsHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellAuraRestrictionsHotfix1200 hotfix = new SpellAuraRestrictionsHotfix1200();

            hotfix.ID = entry;
            hotfix.DifficultyID = packet.ReadInt16("DifficultyID", indexes);
            hotfix.CasterAuraState = packet.ReadInt32("CasterAuraState", indexes);
            hotfix.TargetAuraState = packet.ReadInt32("TargetAuraState", indexes);
            hotfix.ExcludeCasterAuraState = packet.ReadInt32("ExcludeCasterAuraState", indexes);
            hotfix.ExcludeTargetAuraState = packet.ReadInt32("ExcludeTargetAuraState", indexes);
            hotfix.CasterAuraSpell = packet.ReadInt32("CasterAuraSpell", indexes);
            hotfix.TargetAuraSpell = packet.ReadInt32("TargetAuraSpell", indexes);
            hotfix.ExcludeCasterAuraSpell = packet.ReadInt32("ExcludeCasterAuraSpell", indexes);
            hotfix.ExcludeTargetAuraSpell = packet.ReadInt32("ExcludeTargetAuraSpell", indexes);
            hotfix.CasterAuraType = packet.ReadInt16("CasterAuraType", indexes);
            hotfix.TargetAuraType = packet.ReadInt16("TargetAuraType", indexes);
            hotfix.ExcludeCasterAuraType = packet.ReadInt16("ExcludeCasterAuraType", indexes);
            hotfix.ExcludeTargetAuraType = packet.ReadInt16("ExcludeTargetAuraType", indexes);
            hotfix.SpellID = packet.ReadUInt32("SpellID", indexes);

            Storage.SpellAuraRestrictionsHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellCastTimesHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellCastTimesHotfix1200 hotfix = new SpellCastTimesHotfix1200();

            hotfix.ID = entry;
            hotfix.Base = packet.ReadInt32("Base", indexes);
            hotfix.Minimum = packet.ReadInt32("Minimum", indexes);

            Storage.SpellCastTimesHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellCastingRequirementsHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellCastingRequirementsHotfix1200 hotfix = new SpellCastingRequirementsHotfix1200();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.FacingCasterFlags = packet.ReadInt32("FacingCasterFlags", indexes);
            hotfix.MinFactionID = packet.ReadUInt16("MinFactionID", indexes);
            hotfix.MinReputation = packet.ReadInt32("MinReputation", indexes);
            hotfix.RequiredAreasID = packet.ReadUInt16("RequiredAreasID", indexes);
            hotfix.RequiredAuraVision = packet.ReadByte("RequiredAuraVision", indexes);
            hotfix.RequiresSpellFocus = packet.ReadUInt16("RequiresSpellFocus", indexes);

            Storage.SpellCastingRequirementsHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellCategoriesHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellCategoriesHotfix1200 hotfix = new SpellCategoriesHotfix1200();

            hotfix.ID = entry;
            hotfix.DifficultyID = packet.ReadInt16("DifficultyID", indexes);
            hotfix.Category = packet.ReadInt16("Category", indexes);
            hotfix.DefenseType = packet.ReadSByte("DefenseType", indexes);
            hotfix.DiminishType = packet.ReadInt32("DiminishType", indexes);
            hotfix.DispelType = packet.ReadSByte("DispelType", indexes);
            hotfix.Mechanic = packet.ReadSByte("Mechanic", indexes);
            hotfix.PreventionType = packet.ReadInt32("PreventionType", indexes);
            hotfix.StartRecoveryCategory = packet.ReadInt16("StartRecoveryCategory", indexes);
            hotfix.ChargeCategory = packet.ReadInt16("ChargeCategory", indexes);
            hotfix.SpellID = packet.ReadUInt32("SpellID", indexes);

            Storage.SpellCategoriesHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellCategoryHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellCategoryHotfix1200 hotfix = new SpellCategoryHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.UsesPerWeek = packet.ReadInt32("UsesPerWeek", indexes);
            hotfix.MaxCharges = packet.ReadInt32("MaxCharges", indexes);
            hotfix.ChargeRecoveryTime = packet.ReadInt32("ChargeRecoveryTime", indexes);
            hotfix.TypeMask = packet.ReadInt32("TypeMask", indexes);

            Storage.SpellCategoryHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SpellCategoryLocaleHotfix1200 hotfixLocale = new SpellCategoryLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.SpellCategoryHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SpellClassOptionsHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellClassOptionsHotfix1200 hotfix = new SpellClassOptionsHotfix1200();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.ModalNextSpell = packet.ReadUInt32("ModalNextSpell", indexes);
            hotfix.SpellClassSet = packet.ReadByte("SpellClassSet", indexes);
            hotfix.SpellClassMask = new int?[4];
            for (int i = 0; i < 4; i++)
                hotfix.SpellClassMask[i] = packet.ReadInt32("SpellClassMask", indexes, i);

            Storage.SpellClassOptionsHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellCooldownsHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellCooldownsHotfix1200 hotfix = new SpellCooldownsHotfix1200();

            hotfix.ID = entry;
            hotfix.DifficultyID = packet.ReadInt16("DifficultyID", indexes);
            hotfix.CategoryRecoveryTime = packet.ReadInt32("CategoryRecoveryTime", indexes);
            hotfix.RecoveryTime = packet.ReadInt32("RecoveryTime", indexes);
            hotfix.StartRecoveryTime = packet.ReadInt32("StartRecoveryTime", indexes);
            hotfix.AuraSpellID = packet.ReadInt32("AuraSpellID", indexes);
            hotfix.SpellID = packet.ReadUInt32("SpellID", indexes);

            Storage.SpellCooldownsHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellDurationHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellDurationHotfix1200 hotfix = new SpellDurationHotfix1200();

            hotfix.ID = entry;
            hotfix.Duration = packet.ReadInt32("Duration", indexes);
            hotfix.MaxDuration = packet.ReadInt32("MaxDuration", indexes);
            hotfix.DurationPerResource = packet.ReadInt32("DurationPerResource", indexes);

            Storage.SpellDurationHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellEffectHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellEffectHotfix1200 hotfix = new SpellEffectHotfix1200();

            hotfix.ID = entry;
            hotfix.EffectAura = packet.ReadInt16("EffectAura", indexes);
            hotfix.DifficultyID = packet.ReadInt16("DifficultyID", indexes);
            hotfix.EffectIndex = packet.ReadInt32("EffectIndex", indexes);
            hotfix.Effect = packet.ReadUInt32("Effect", indexes);
            hotfix.EffectAmplitude = packet.ReadSingle("EffectAmplitude", indexes);
            hotfix.EffectAttributes = packet.ReadInt32("EffectAttributes", indexes);
            hotfix.EffectAuraPeriod = packet.ReadInt32("EffectAuraPeriod", indexes);
            hotfix.EffectBonusCoefficient = packet.ReadSingle("EffectBonusCoefficient", indexes);
            hotfix.EffectChainAmplitude = packet.ReadSingle("EffectChainAmplitude", indexes);
            hotfix.EffectChainTargets = packet.ReadInt32("EffectChainTargets", indexes);
            hotfix.EffectItemType = packet.ReadInt32("EffectItemType", indexes);
            hotfix.EffectMechanic = packet.ReadInt32("EffectMechanic", indexes);
            hotfix.EffectPointsPerResource = packet.ReadSingle("EffectPointsPerResource", indexes);
            hotfix.EffectPosFacing = packet.ReadSingle("EffectPosFacing", indexes);
            hotfix.EffectRealPointsPerLevel = packet.ReadSingle("EffectRealPointsPerLevel", indexes);
            hotfix.EffectTriggerSpell = packet.ReadInt32("EffectTriggerSpell", indexes);
            hotfix.BonusCoefficientFromAP = packet.ReadSingle("BonusCoefficientFromAP", indexes);
            hotfix.PvpMultiplier = packet.ReadSingle("PvpMultiplier", indexes);
            hotfix.Coefficient = packet.ReadSingle("Coefficient", indexes);
            hotfix.Variance = packet.ReadSingle("Variance", indexes);
            hotfix.ResourceCoefficient = packet.ReadSingle("ResourceCoefficient", indexes);
            hotfix.GroupSizeBasePointsCoefficient = packet.ReadSingle("GroupSizeBasePointsCoefficient", indexes);
            hotfix.EffectBasePoints = packet.ReadSingle("EffectBasePoints", indexes);
            hotfix.ScalingClass = packet.ReadInt32("ScalingClass", indexes);
            hotfix.TargetNodeGraph = packet.ReadInt32("TargetNodeGraph", indexes);
            hotfix.EffectMiscValue = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.EffectMiscValue[i] = packet.ReadInt32("EffectMiscValue", indexes, i);
            hotfix.EffectRadiusIndex = new uint?[2];
            for (int i = 0; i < 2; i++)
                hotfix.EffectRadiusIndex[i] = packet.ReadUInt32("EffectRadiusIndex", indexes, i);
            hotfix.EffectSpellClassMask = new int?[4];
            for (int i = 0; i < 4; i++)
                hotfix.EffectSpellClassMask[i] = packet.ReadInt32("EffectSpellClassMask", indexes, i);
            hotfix.ImplicitTarget = new short?[2];
            for (int i = 0; i < 2; i++)
                hotfix.ImplicitTarget[i] = packet.ReadInt16("ImplicitTarget", indexes, i);
            hotfix.SpellID = packet.ReadUInt32("SpellID", indexes);

            Storage.SpellEffectHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellEmpowerHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellEmpowerHotfix1200 hotfix = new SpellEmpowerHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.Unused1000 = packet.ReadInt32("Unused1000", indexes);

            Storage.SpellEmpowerHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellEmpowerStageHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellEmpowerStageHotfix1200 hotfix = new SpellEmpowerStageHotfix1200();

            hotfix.ID = entry;
            hotfix.Stage = packet.ReadInt32("Stage", indexes);
            hotfix.DurationMs = packet.ReadInt32("DurationMs", indexes);
            hotfix.SpellEmpowerID = packet.ReadUInt32("SpellEmpowerID", indexes);

            Storage.SpellEmpowerStageHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellEquippedItemsHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellEquippedItemsHotfix1200 hotfix = new SpellEquippedItemsHotfix1200();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.EquippedItemClass = packet.ReadInt32("EquippedItemClass", indexes);
            hotfix.EquippedItemInvTypes = packet.ReadInt32("EquippedItemInvTypes", indexes);
            hotfix.EquippedItemSubclass = packet.ReadInt32("EquippedItemSubclass", indexes);

            Storage.SpellEquippedItemsHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellFocusObjectHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellFocusObjectHotfix1200 hotfix = new SpellFocusObjectHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);

            Storage.SpellFocusObjectHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SpellFocusObjectLocaleHotfix1200 hotfixLocale = new SpellFocusObjectLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.SpellFocusObjectHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SpellInterruptsHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellInterruptsHotfix1200 hotfix = new SpellInterruptsHotfix1200();

            hotfix.ID = entry;
            hotfix.DifficultyID = packet.ReadInt16("DifficultyID", indexes);
            hotfix.InterruptFlags = packet.ReadInt32("InterruptFlags", indexes);
            hotfix.AuraInterruptFlags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.AuraInterruptFlags[i] = packet.ReadInt32("AuraInterruptFlags", indexes, i);
            hotfix.ChannelInterruptFlags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.ChannelInterruptFlags[i] = packet.ReadInt32("ChannelInterruptFlags", indexes, i);
            hotfix.SpellID = packet.ReadUInt32("SpellID", indexes);

            Storage.SpellInterruptsHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellItemEnchantmentHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellItemEnchantmentHotfix1200 hotfix = new SpellItemEnchantmentHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.HordeName = packet.ReadCString("HordeName", indexes);
            hotfix.Duration = packet.ReadInt32("Duration", indexes);
            hotfix.EffectArg = new uint?[3];
            for (int i = 0; i < 3; i++)
                hotfix.EffectArg[i] = packet.ReadUInt32("EffectArg", indexes, i);
            hotfix.EffectScalingPoints = new float?[3];
            for (int i = 0; i < 3; i++)
                hotfix.EffectScalingPoints[i] = packet.ReadSingle("EffectScalingPoints", indexes, i);
            hotfix.IconFileDataID = packet.ReadUInt32("IconFileDataID", indexes);
            hotfix.MinItemLevel = packet.ReadInt32("MinItemLevel", indexes);
            hotfix.MaxItemLevel = packet.ReadInt32("MaxItemLevel", indexes);
            hotfix.TransmogUseConditionID = packet.ReadUInt32("TransmogUseConditionID", indexes);
            hotfix.TransmogCost = packet.ReadUInt32("TransmogCost", indexes);
            hotfix.EffectPointsMin = new short?[3];
            for (int i = 0; i < 3; i++)
                hotfix.EffectPointsMin[i] = packet.ReadInt16("EffectPointsMin", indexes, i);
            hotfix.ItemVisual = packet.ReadUInt16("ItemVisual", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.RequiredSkillID = packet.ReadUInt16("RequiredSkillID", indexes);
            hotfix.RequiredSkillRank = packet.ReadUInt16("RequiredSkillRank", indexes);
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);
            hotfix.Charges = packet.ReadByte("Charges", indexes);
            hotfix.Effect = new byte?[3];
            for (int i = 0; i < 3; i++)
                hotfix.Effect[i] = packet.ReadByte("Effect", indexes, i);
            hotfix.ScalingClass = packet.ReadSByte("ScalingClass", indexes);
            hotfix.ScalingClassRestricted = packet.ReadSByte("ScalingClassRestricted", indexes);
            hotfix.ConditionID = packet.ReadByte("ConditionID", indexes);
            hotfix.MinLevel = packet.ReadByte("MinLevel", indexes);
            hotfix.MaxLevel = packet.ReadByte("MaxLevel", indexes);

            Storage.SpellItemEnchantmentHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SpellItemEnchantmentLocaleHotfix1200 hotfixLocale = new SpellItemEnchantmentLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    HordeNameLang = hotfix.HordeName,
                };
                Storage.SpellItemEnchantmentHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SpellKeyboundOverrideHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellKeyboundOverrideHotfix1200 hotfix = new SpellKeyboundOverrideHotfix1200();

            hotfix.ID = entry;
            hotfix.Function = packet.ReadCString("Function", indexes);
            hotfix.Type = packet.ReadSByte("Type", indexes);
            hotfix.Data = packet.ReadInt32("Data", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.SpellKeyboundOverrideHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellItemEnchantmentConditionHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellItemEnchantmentConditionHotfix1200 hotfix = new SpellItemEnchantmentConditionHotfix1200();

            hotfix.ID = entry;
            hotfix.LtOperandType = new byte?[5];
            for (int i = 0; i < 5; i++)
                hotfix.LtOperandType[i] = packet.ReadByte("LtOperandType", indexes, i);
            hotfix.LtOperand = new uint?[5];
            for (int i = 0; i < 5; i++)
                hotfix.LtOperand[i] = packet.ReadUInt32("LtOperand", indexes, i);
            hotfix.Operator = new byte?[5];
            for (int i = 0; i < 5; i++)
                hotfix.Operator[i] = packet.ReadByte("Operator", indexes, i);
            hotfix.RtOperandType = new byte?[5];
            for (int i = 0; i < 5; i++)
                hotfix.RtOperandType[i] = packet.ReadByte("RtOperandType", indexes, i);
            hotfix.RtOperand = new byte?[5];
            for (int i = 0; i < 5; i++)
                hotfix.RtOperand[i] = packet.ReadByte("RtOperand", indexes, i);
            hotfix.Logic = new byte?[5];
            for (int i = 0; i < 5; i++)
                hotfix.Logic[i] = packet.ReadByte("Logic", indexes, i);

            Storage.SpellItemEnchantmentConditionHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellLabelHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellLabelHotfix1200 hotfix = new SpellLabelHotfix1200();

            hotfix.ID = entry;
            hotfix.LabelID = packet.ReadUInt32("LabelID", indexes);
            hotfix.SpellID = packet.ReadUInt32("SpellID", indexes);

            Storage.SpellLabelHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellLearnSpellHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellLearnSpellHotfix1200 hotfix = new SpellLearnSpellHotfix1200();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadUInt32("SpellID", indexes);
            hotfix.LearnSpellID = packet.ReadInt32("LearnSpellID", indexes);
            hotfix.OverridesSpellID = packet.ReadInt32("OverridesSpellID", indexes);

            Storage.SpellLearnSpellHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellLevelsHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellLevelsHotfix1200 hotfix = new SpellLevelsHotfix1200();

            hotfix.ID = entry;
            hotfix.DifficultyID = packet.ReadInt16("DifficultyID", indexes);
            hotfix.MaxLevel = packet.ReadInt16("MaxLevel", indexes);
            hotfix.MaxPassiveAuraLevel = packet.ReadByte("MaxPassiveAuraLevel", indexes);
            hotfix.BaseLevel = packet.ReadInt32("BaseLevel", indexes);
            hotfix.SpellLevel = packet.ReadInt32("SpellLevel", indexes);
            hotfix.SpellID = packet.ReadUInt32("SpellID", indexes);

            Storage.SpellLevelsHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellMiscHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellMiscHotfix1200 hotfix = new SpellMiscHotfix1200();

            hotfix.ID = entry;
            hotfix.Attributes = new int?[17];
            for (int i = 0; i < 17; i++)
                hotfix.Attributes[i] = packet.ReadInt32("Attributes", indexes, i);
            hotfix.DifficultyID = packet.ReadInt16("DifficultyID", indexes);
            hotfix.CastingTimeIndex = packet.ReadUInt16("CastingTimeIndex", indexes);
            hotfix.DurationIndex = packet.ReadUInt16("DurationIndex", indexes);
            hotfix.PvPDurationIndex = packet.ReadUInt16("PvPDurationIndex", indexes);
            hotfix.RangeIndex = packet.ReadUInt16("RangeIndex", indexes);
            hotfix.SchoolMask = packet.ReadByte("SchoolMask", indexes);
            hotfix.Speed = packet.ReadSingle("Speed", indexes);
            hotfix.LaunchDelay = packet.ReadSingle("LaunchDelay", indexes);
            hotfix.MinDuration = packet.ReadSingle("MinDuration", indexes);
            hotfix.SpellIconFileDataID = packet.ReadInt32("SpellIconFileDataID", indexes);
            hotfix.ActiveIconFileDataID = packet.ReadInt32("ActiveIconFileDataID", indexes);
            hotfix.ContentTuningID = packet.ReadInt32("ContentTuningID", indexes);
            hotfix.ShowFutureSpellPlayerConditionID = packet.ReadInt32("ShowFutureSpellPlayerConditionID", indexes);
            hotfix.SpellVisualScript = packet.ReadInt32("SpellVisualScript", indexes);
            hotfix.ActiveSpellVisualScript = packet.ReadInt32("ActiveSpellVisualScript", indexes);
            hotfix.SpellID = packet.ReadUInt32("SpellID", indexes);

            Storage.SpellMiscHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellNameHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellNameHotfix1200 hotfix = new SpellNameHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);

            Storage.SpellNameHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SpellNameLocaleHotfix1200 hotfixLocale = new SpellNameLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.SpellNameHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SpellPowerHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellPowerHotfix1200 hotfix = new SpellPowerHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);
            hotfix.ManaCost = packet.ReadInt32("ManaCost", indexes);
            hotfix.ManaCostPerLevel = packet.ReadInt32("ManaCostPerLevel", indexes);
            hotfix.ManaPerSecond = packet.ReadInt32("ManaPerSecond", indexes);
            hotfix.PowerDisplayID = packet.ReadUInt32("PowerDisplayID", indexes);
            hotfix.AltPowerBarID = packet.ReadInt32("AltPowerBarID", indexes);
            hotfix.PowerCostPct = packet.ReadSingle("PowerCostPct", indexes);
            hotfix.PowerCostMaxPct = packet.ReadSingle("PowerCostMaxPct", indexes);
            hotfix.OptionalCostPct = packet.ReadSingle("OptionalCostPct", indexes);
            hotfix.PowerPctPerSecond = packet.ReadSingle("PowerPctPerSecond", indexes);
            hotfix.PowerType = packet.ReadSByte("PowerType", indexes);
            hotfix.RequiredAuraSpellID = packet.ReadInt32("RequiredAuraSpellID", indexes);
            hotfix.OptionalCost = packet.ReadUInt32("OptionalCost", indexes);
            hotfix.SpellID = packet.ReadUInt32("SpellID", indexes);

            Storage.SpellPowerHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellPowerDifficultyHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellPowerDifficultyHotfix1200 hotfix = new SpellPowerDifficultyHotfix1200();

            hotfix.ID = entry;
            hotfix.DifficultyID = packet.ReadInt16("DifficultyID", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);

            Storage.SpellPowerDifficultyHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellProcsPerMinuteHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellProcsPerMinuteHotfix1200 hotfix = new SpellProcsPerMinuteHotfix1200();

            hotfix.ID = entry;
            hotfix.BaseProcRate = packet.ReadSingle("BaseProcRate", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.SpellProcsPerMinuteHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellProcsPerMinuteModHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellProcsPerMinuteModHotfix1200 hotfix = new SpellProcsPerMinuteModHotfix1200();

            hotfix.ID = entry;
            hotfix.Type = packet.ReadInt32("Type", indexes);
            hotfix.Param = packet.ReadInt32("Param", indexes);
            hotfix.Coeff = packet.ReadSingle("Coeff", indexes);
            hotfix.SpellProcsPerMinuteID = packet.ReadUInt32("SpellProcsPerMinuteID", indexes);

            Storage.SpellProcsPerMinuteModHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellRadiusHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellRadiusHotfix1200 hotfix = new SpellRadiusHotfix1200();

            hotfix.ID = entry;
            hotfix.Radius = packet.ReadSingle("Radius", indexes);
            hotfix.RadiusPerLevel = packet.ReadSingle("RadiusPerLevel", indexes);
            hotfix.RadiusMin = packet.ReadSingle("RadiusMin", indexes);
            hotfix.RadiusMax = packet.ReadSingle("RadiusMax", indexes);

            Storage.SpellRadiusHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellRangeHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellRangeHotfix1200 hotfix = new SpellRangeHotfix1200();

            hotfix.ID = entry;
            hotfix.DisplayName = packet.ReadCString("DisplayName", indexes);
            hotfix.DisplayNameShort = packet.ReadCString("DisplayNameShort", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.RangeMin = new float?[2];
            for (int i = 0; i < 2; i++)
                hotfix.RangeMin[i] = packet.ReadSingle("RangeMin", indexes, i);
            hotfix.RangeMax = new float?[2];
            for (int i = 0; i < 2; i++)
                hotfix.RangeMax[i] = packet.ReadSingle("RangeMax", indexes, i);

            Storage.SpellRangeHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SpellRangeLocaleHotfix1200 hotfixLocale = new SpellRangeLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    DisplayNameLang = hotfix.DisplayName,
                    DisplayNameShortLang = hotfix.DisplayNameShort,
                };
                Storage.SpellRangeHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SpellReagentsHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellReagentsHotfix1200 hotfix = new SpellReagentsHotfix1200();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadUInt32("SpellID", indexes);
            hotfix.Reagent = new int?[8];
            for (int i = 0; i < 8; i++)
                hotfix.Reagent[i] = packet.ReadInt32("Reagent", indexes, i);
            hotfix.ReagentCount = new short?[8];
            for (int i = 0; i < 8; i++)
                hotfix.ReagentCount[i] = packet.ReadInt16("ReagentCount", indexes, i);
            hotfix.ReagentRecraftCount = new short?[8];
            for (int i = 0; i < 8; i++)
                hotfix.ReagentRecraftCount[i] = packet.ReadInt16("ReagentReCraftCount", indexes, i);
            hotfix.ReagentSource = new byte?[8];
            for (int i = 0; i < 8; i++)
                hotfix.ReagentSource[i] = packet.ReadByte("ReagentSource", indexes, i);

            Storage.SpellReagentsHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellReagentsCurrencyHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellReagentsCurrencyHotfix1200 hotfix = new SpellReagentsCurrencyHotfix1200();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.CurrencyTypesID = packet.ReadInt32("CurrencyTypesID", indexes);
            hotfix.CurrencyCount = packet.ReadInt32("CurrencyCount", indexes);
            hotfix.OverrideRecraftCurrencyCount = packet.ReadInt32("OverrideRecraftCurrencyCount", indexes);
            hotfix.OrderSource = packet.ReadByte("OrderSource", indexes);

            Storage.SpellReagentsCurrencyHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellScalingHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellScalingHotfix1200 hotfix = new SpellScalingHotfix1200();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.MinScalingLevel = packet.ReadUInt32("MinScalingLevel", indexes);
            hotfix.MaxScalingLevel = packet.ReadUInt32("MaxScalingLevel", indexes);

            Storage.SpellScalingHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellShapeshiftHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellShapeshiftHotfix1200 hotfix = new SpellShapeshiftHotfix1200();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.StanceBarOrder = packet.ReadSByte("StanceBarOrder", indexes);
            hotfix.ShapeshiftExclude = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.ShapeshiftExclude[i] = packet.ReadInt32("ShapeshiftExclude", indexes, i);
            hotfix.ShapeshiftMask = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.ShapeshiftMask[i] = packet.ReadInt32("ShapeshiftMask", indexes, i);

            Storage.SpellShapeshiftHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellShapeshiftFormHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellShapeshiftFormHotfix1200 hotfix = new SpellShapeshiftFormHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.CreatureDisplayID = packet.ReadUInt32("CreatureDisplayID", indexes);
            hotfix.CreatureType = packet.ReadByte("CreatureType", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.AttackIconFileID = packet.ReadInt32("AttackIconFileID", indexes);
            hotfix.BonusActionBar = packet.ReadSByte("BonusActionBar", indexes);
            hotfix.CombatRoundTime = packet.ReadInt16("CombatRoundTime", indexes);
            hotfix.DamageVariance = packet.ReadSingle("DamageVariance", indexes);
            hotfix.MountTypeID = packet.ReadUInt16("MountTypeID", indexes);
            hotfix.PresetSpellID = new uint?[8];
            for (int i = 0; i < 8; i++)
                hotfix.PresetSpellID[i] = packet.ReadUInt32("PresetSpellID", indexes, i);

            Storage.SpellShapeshiftFormHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SpellShapeshiftFormLocaleHotfix1200 hotfixLocale = new SpellShapeshiftFormLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.SpellShapeshiftFormHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SpellTargetRestrictionsHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellTargetRestrictionsHotfix1200 hotfix = new SpellTargetRestrictionsHotfix1200();

            hotfix.ID = entry;
            hotfix.DifficultyID = packet.ReadInt16("DifficultyID", indexes);
            hotfix.ConeDegrees = packet.ReadSingle("ConeDegrees", indexes);
            hotfix.MaxTargets = packet.ReadByte("MaxTargets", indexes);
            hotfix.MaxTargetLevel = packet.ReadUInt32("MaxTargetLevel", indexes);
            hotfix.TargetCreatureType = packet.ReadInt16("TargetCreatureType", indexes);
            hotfix.Targets = packet.ReadInt32("Targets", indexes);
            hotfix.Width = packet.ReadSingle("Width", indexes);
            hotfix.SpellID = packet.ReadUInt32("SpellID", indexes);

            Storage.SpellTargetRestrictionsHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellTotemsHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellTotemsHotfix1200 hotfix = new SpellTotemsHotfix1200();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.RequiredTotemCategoryID = new ushort?[2];
            for (int i = 0; i < 2; i++)
                hotfix.RequiredTotemCategoryID[i] = packet.ReadUInt16("RequiredTotemCategoryID", indexes, i);
            hotfix.Totem = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Totem[i] = packet.ReadInt32("Totem", indexes, i);

            Storage.SpellTotemsHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellVisualHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellVisualHotfix1200 hotfix = new SpellVisualHotfix1200();

            hotfix.ID = entry;
            hotfix.MissileCastOffset = new float?[3];
            for (int i = 0; i < 3; i++)
                hotfix.MissileCastOffset[i] = packet.ReadSingle("MissileCastOffset", indexes, i);
            hotfix.MissileImpactOffset = new float?[3];
            for (int i = 0; i < 3; i++)
                hotfix.MissileImpactOffset[i] = packet.ReadSingle("MissileImpactOffset", indexes, i);
            hotfix.StateKit = packet.ReadInt32("StateKit", indexes);
            hotfix.AnimEventSoundID = packet.ReadUInt32("AnimEventSoundID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.MissileAttachment = packet.ReadSByte("MissileAttachment", indexes);
            hotfix.MissileDestinationAttachment = packet.ReadSByte("MissileDestinationAttachment", indexes);
            hotfix.MissileCastPositionerID = packet.ReadUInt32("MissileCastPositionerID", indexes);
            hotfix.MissileImpactPositionerID = packet.ReadUInt32("MissileImpactPositionerID", indexes);
            hotfix.MissileTargetingKit = packet.ReadInt32("MissileTargetingKit", indexes);
            hotfix.HostileSpellVisualID = packet.ReadUInt32("HostileSpellVisualID", indexes);
            hotfix.CasterSpellVisualID = packet.ReadUInt32("CasterSpellVisualID", indexes);
            hotfix.SpellVisualMissileSetID = packet.ReadUInt16("SpellVisualMissileSetID", indexes);
            hotfix.DamageNumberDelay = packet.ReadUInt16("DamageNumberDelay", indexes);
            hotfix.LowViolenceSpellVisualID = packet.ReadUInt32("LowViolenceSpellVisualID", indexes);
            hotfix.RaidSpellVisualMissileSetID = packet.ReadUInt32("RaidSpellVisualMissileSetID", indexes);
            hotfix.ReducedUnexpectedCameraMovementSpellVisualID = packet.ReadInt32("ReducedUnexpectedCameraMovementSpellVisualID", indexes);

            Storage.SpellVisualHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellVisualEffectNameHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellVisualEffectNameHotfix1200 hotfix = new SpellVisualEffectNameHotfix1200();

            hotfix.ID = entry;
            hotfix.ModelFileDataID = packet.ReadInt32("ModelFileDataID", indexes);
            hotfix.BaseMissileSpeed = packet.ReadSingle("BaseMissileSpeed", indexes);
            hotfix.Scale = packet.ReadSingle("Scale", indexes);
            hotfix.MinAllowedScale = packet.ReadSingle("MinAllowedScale", indexes);
            hotfix.MaxAllowedScale = packet.ReadSingle("MaxAllowedScale", indexes);
            hotfix.Alpha = packet.ReadSingle("Alpha", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.TextureFileDataID = packet.ReadInt32("TextureFileDataID", indexes);
            hotfix.EffectRadius = packet.ReadSingle("EffectRadius", indexes);
            hotfix.Type = packet.ReadInt32("Type", indexes);
            hotfix.GenericID = packet.ReadInt32("GenericID", indexes);
            hotfix.RibbonQualityID = packet.ReadUInt32("RibbonQualityID", indexes);
            hotfix.DissolveEffectID = packet.ReadInt32("DissolveEffectID", indexes);
            hotfix.ModelPosition = packet.ReadInt32("ModelPosition", indexes);
            hotfix.Unknown901 = packet.ReadSByte("Unknown901", indexes);
            hotfix.Unknown1100 = packet.ReadUInt16("Unknown1100", indexes);

            Storage.SpellVisualEffectNameHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellVisualKitHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellVisualKitHotfix1200 hotfix = new SpellVisualKitHotfix1200();

            hotfix.ID = entry;
            hotfix.ClutterLevel = packet.ReadInt32("ClutterLevel", indexes);
            hotfix.FallbackSpellVisualKitId = packet.ReadInt32("FallbackSpellVisualKitId", indexes);
            hotfix.DelayMin = packet.ReadUInt16("DelayMin", indexes);
            hotfix.DelayMax = packet.ReadUInt16("DelayMax", indexes);
            hotfix.MinimumSpellVisualDensityFilterType = packet.ReadInt32("MinimumSpellVisualDensityFilterType", indexes);
            hotfix.MinimumSpellVisualDensityFilterParam = packet.ReadInt32("MinimumSpellVisualDensityFilterParam", indexes);
            hotfix.ReducedSpellVisualDensityFilterType = packet.ReadInt32("ReducedSpellVisualDensityFilterType", indexes);
            hotfix.ReducedSpellVisualDensityFilterParam = packet.ReadInt32("ReducedSpellVisualDensityFilterParam", indexes);
            hotfix.Flags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.SpellVisualKitHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellVisualMissileHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellVisualMissileHotfix1200 hotfix = new SpellVisualMissileHotfix1200();

            hotfix.CastOffset = new float?[3];
            for (int i = 0; i < 3; i++)
                hotfix.CastOffset[i] = packet.ReadSingle("CastOffset", indexes, i);
            hotfix.ImpactOffset = new float?[3];
            for (int i = 0; i < 3; i++)
                hotfix.ImpactOffset[i] = packet.ReadSingle("ImpactOffset", indexes, i);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.SpellVisualEffectNameID = packet.ReadUInt16("SpellVisualEffectNameID", indexes);
            hotfix.SoundEntriesID = packet.ReadUInt32("SoundEntriesID", indexes);
            hotfix.Attachment = packet.ReadSByte("Attachment", indexes);
            hotfix.DestinationAttachment = packet.ReadSByte("DestinationAttachment", indexes);
            hotfix.CastPositionerID = packet.ReadUInt16("CastPositionerID", indexes);
            hotfix.ImpactPositionerID = packet.ReadUInt16("ImpactPositionerID", indexes);
            hotfix.FollowGroundHeight = packet.ReadInt32("FollowGroundHeight", indexes);
            hotfix.FollowGroundDropSpeed = packet.ReadUInt32("FollowGroundDropSpeed", indexes);
            hotfix.FollowGroundApproach = packet.ReadUInt16("FollowGroundApproach", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.SpellMissileMotionID = packet.ReadUInt16("SpellMissileMotionID", indexes);
            hotfix.AnimKitID = packet.ReadUInt32("AnimKitID", indexes);
            hotfix.ClutterLevel = packet.ReadInt32("ClutterLevel", indexes);
            hotfix.DecayTimeAfterImpact = packet.ReadInt32("DecayTimeAfterImpact", indexes);
            hotfix.Unused1100 = packet.ReadUInt16("Unused1100", indexes);
            hotfix.SpellVisualMissileSetID = packet.ReadUInt32("SpellVisualMissileSetID", indexes);

            Storage.SpellVisualMissileHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellXSpellVisualHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SpellXSpellVisualHotfix1200 hotfix = new SpellXSpellVisualHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.DifficultyID = packet.ReadInt16("DifficultyID", indexes);
            hotfix.SpellVisualID = packet.ReadUInt32("SpellVisualID", indexes);
            hotfix.Probability = packet.ReadSingle("Probability", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.Priority = packet.ReadInt32("Priority", indexes);
            hotfix.SpellIconFileID = packet.ReadInt32("SpellIconFileID", indexes);
            hotfix.ActiveIconFileID = packet.ReadInt32("ActiveIconFileID", indexes);
            hotfix.ViewerUnitConditionID = packet.ReadUInt16("ViewerUnitConditionID", indexes);
            hotfix.ViewerPlayerConditionID = packet.ReadUInt32("ViewerPlayerConditionID", indexes);
            hotfix.CasterUnitConditionID = packet.ReadUInt16("CasterUnitConditionID", indexes);
            hotfix.CasterPlayerConditionID = packet.ReadUInt32("CasterPlayerConditionID", indexes);
            hotfix.SpellID = packet.ReadUInt32("SpellID", indexes);

            Storage.SpellXSpellVisualHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void SummonPropertiesHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            SummonPropertiesHotfix1200 hotfix = new SummonPropertiesHotfix1200();

            hotfix.ID = entry;
            hotfix.Control = packet.ReadInt32("Control", indexes);
            hotfix.Faction = packet.ReadInt32("Faction", indexes);
            hotfix.Title = packet.ReadInt32("Title", indexes);
            hotfix.Slot = packet.ReadInt32("Slot", indexes);
            hotfix.Flags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.SummonPropertiesHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TactKeyHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TactKeyHotfix1200 hotfix = new TactKeyHotfix1200();

            hotfix.ID = entry;
            hotfix.Key = new byte?[16];
            for (int i = 0; i < 16; i++)
                hotfix.Key[i] = packet.ReadByte("Key", indexes, i);

            Storage.TactKeyHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TalentHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TalentHotfix1200 hotfix = new TalentHotfix1200();

            hotfix.ID = entry;
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.TierID = packet.ReadByte("TierID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ColumnIndex = packet.ReadByte("ColumnIndex", indexes);
            hotfix.TabID = packet.ReadUInt16("TabID", indexes);
            hotfix.ClassID = packet.ReadSByte("ClassID", indexes);
            hotfix.SpecID = packet.ReadUInt16("SpecID", indexes);
            hotfix.SpellID = packet.ReadUInt32("SpellID", indexes);
            hotfix.OverridesSpellID = packet.ReadUInt32("OverridesSpellID", indexes);
            hotfix.RequiredSpellID = packet.ReadUInt32("RequiredSpellID", indexes);
            hotfix.CategoryMask = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.CategoryMask[i] = packet.ReadInt32("CategoryMask", indexes, i);
            hotfix.SpellRank = new uint?[9];
            for (int i = 0; i < 9; i++)
                hotfix.SpellRank[i] = packet.ReadUInt32("SpellRank", indexes, i);
            hotfix.PrereqTalent = new uint?[3];
            for (int i = 0; i < 3; i++)
                hotfix.PrereqTalent[i] = packet.ReadUInt32("PrereqTalent", indexes, i);
            hotfix.PrereqRank = new byte?[3];
            for (int i = 0; i < 3; i++)
                hotfix.PrereqRank[i] = packet.ReadByte("PrereqRank", indexes, i);

            Storage.TalentHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                TalentLocaleHotfix1200 hotfixLocale = new TalentLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                };
                Storage.TalentHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void TaxiNodesHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TaxiNodesHotfix1200 hotfix = new TaxiNodesHotfix1200();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.PosX = packet.ReadSingle("PosX", indexes);
            hotfix.PosY = packet.ReadSingle("PosY", indexes);
            hotfix.PosZ = packet.ReadSingle("PosZ", indexes);
            hotfix.MapOffsetX = packet.ReadSingle("MapOffsetX", indexes);
            hotfix.MapOffsetY = packet.ReadSingle("MapOffsetY", indexes);
            hotfix.FlightMapOffsetX = packet.ReadSingle("FlightMapOffsetX", indexes);
            hotfix.FlightMapOffsetY = packet.ReadSingle("FlightMapOffsetY", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ContinentID = packet.ReadUInt16("ContinentID", indexes);
            hotfix.ConditionID = packet.ReadInt32("ConditionID", indexes);
            hotfix.CharacterBitNumber = packet.ReadUInt16("CharacterBitNumber", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.UiTextureKitID = packet.ReadInt32("UiTextureKitID", indexes);
            hotfix.MinimapAtlasMemberID = packet.ReadInt32("MinimapAtlasMemberID", indexes);
            hotfix.Facing = packet.ReadSingle("Facing", indexes);
            hotfix.SpecialIconConditionID = packet.ReadUInt32("SpecialIconConditionID", indexes);
            hotfix.VisibilityConditionID = packet.ReadUInt32("VisibilityConditionID", indexes);
            hotfix.MountCreatureID = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.MountCreatureID[i] = packet.ReadInt32("MountCreatureID", indexes, i);

            Storage.TaxiNodesHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                TaxiNodesLocaleHotfix1200 hotfixLocale = new TaxiNodesLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.TaxiNodesHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void TaxiPathHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TaxiPathHotfix1200 hotfix = new TaxiPathHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.FromTaxiNode = packet.ReadUInt16("FromTaxiNode", indexes);
            hotfix.ToTaxiNode = packet.ReadUInt16("ToTaxiNode", indexes);
            hotfix.Cost = packet.ReadUInt32("Cost", indexes);

            Storage.TaxiPathHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TaxiPathNodeHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TaxiPathNodeHotfix1200 hotfix = new TaxiPathNodeHotfix1200();

            hotfix.LocX = packet.ReadSingle("LocX", indexes);
            hotfix.LocY = packet.ReadSingle("LocY", indexes);
            hotfix.LocZ = packet.ReadSingle("LocZ", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.PathID = packet.ReadUInt16("PathID", indexes);
            hotfix.NodeIndex = packet.ReadInt32("NodeIndex", indexes);
            hotfix.ContinentID = packet.ReadUInt16("ContinentID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.Delay = packet.ReadUInt32("Delay", indexes);
            hotfix.ArrivalEventID = packet.ReadInt32("ArrivalEventID", indexes);
            hotfix.DepartureEventID = packet.ReadInt32("DepartureEventID", indexes);

            Storage.TaxiPathNodeHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TotemCategoryHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TotemCategoryHotfix1200 hotfix = new TotemCategoryHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.TotemCategoryType = packet.ReadByte("TotemCategoryType", indexes);
            hotfix.TotemCategoryMask = packet.ReadInt32("TotemCategoryMask", indexes);

            Storage.TotemCategoryHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                TotemCategoryLocaleHotfix1200 hotfixLocale = new TotemCategoryLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.TotemCategoryHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ToyHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            ToyHotfix1200 hotfix = new ToyHotfix1200();

            hotfix.SourceText = packet.ReadCString("SourceText", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.SourceTypeEnum = packet.ReadSByte("SourceTypeEnum", indexes);

            Storage.ToyHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ToyLocaleHotfix1200 hotfixLocale = new ToyLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    SourceTextLang = hotfix.SourceText,
                };
                Storage.ToyHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void TraitCondHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitCondHotfix1200 hotfix = new TraitCondHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.CondType = packet.ReadInt32("CondType", indexes);
            hotfix.TraitTreeID = packet.ReadUInt32("TraitTreeID", indexes);
            hotfix.GrantedRanks = packet.ReadInt32("GrantedRanks", indexes);
            hotfix.QuestID = packet.ReadInt32("QuestID", indexes);
            hotfix.AchievementID = packet.ReadInt32("AchievementID", indexes);
            hotfix.SpecSetID = packet.ReadInt32("SpecSetID", indexes);
            hotfix.TraitNodeGroupID = packet.ReadInt32("TraitNodeGroupID", indexes);
            hotfix.TraitNodeID = packet.ReadInt32("TraitNodeID", indexes);
            hotfix.TraitNodeEntryID = packet.ReadInt32("TraitNodeEntryID", indexes);
            hotfix.TraitCurrencyID = packet.ReadInt32("TraitCurrencyID", indexes);
            hotfix.SpentAmountRequired = packet.ReadInt32("SpentAmountRequired", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.RequiredLevel = packet.ReadInt32("RequiredLevel", indexes);
            hotfix.FreeSharedStringID = packet.ReadInt32("FreeSharedStringID", indexes);
            hotfix.SpendMoreSharedStringID = packet.ReadInt32("SpendMoreSharedStringID", indexes);
            hotfix.TraitCondAccountElementID = packet.ReadInt32("TraitCondAccountElementID", indexes);

            Storage.TraitCondHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitCondAccountElementHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitCondAccountElementHotfix1200 hotfix = new TraitCondAccountElementHotfix1200();

            hotfix.ElementValueInt = packet.ReadInt64("ElementValueInt", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.PlayerDataElementAccountID = packet.ReadUInt32("PlayerDataElementAccountID", indexes);
            hotfix.Comparison = packet.ReadByte("Comparison", indexes);
            hotfix.Unused1110 = packet.ReadInt32("Unused1110", indexes);
            hotfix.PlayerDataElementCharacterID = packet.ReadInt32("PlayerDataElementCharacterID", indexes);

            Storage.TraitCondAccountElementHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitCostHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitCostHotfix1200 hotfix = new TraitCostHotfix1200();

            hotfix.InternalName = packet.ReadCString("InternalName", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Amount = packet.ReadInt32("Amount", indexes);
            hotfix.TraitCurrencyID = packet.ReadInt32("TraitCurrencyID", indexes);
            hotfix.CurveID = packet.ReadInt32("CurveID", indexes);

            Storage.TraitCostHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitCurrencyHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitCurrencyHotfix1200 hotfix = new TraitCurrencyHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Type = packet.ReadInt32("Type", indexes);
            hotfix.CurrencyTypesID = packet.ReadInt32("CurrencyTypesID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.Icon = packet.ReadInt32("Icon", indexes);
            hotfix.PlayerDataElementAccountID = packet.ReadInt32("PlayerDataElementAccountID", indexes);
            hotfix.PlayerDataElementCharacterID = packet.ReadInt32("PlayerDataElementCharacterID", indexes);

            Storage.TraitCurrencyHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitCurrencySourceHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitCurrencySourceHotfix1200 hotfix = new TraitCurrencySourceHotfix1200();

            hotfix.Requirement = packet.ReadCString("Requirement", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitCurrencyID = packet.ReadUInt32("TraitCurrencyID", indexes);
            hotfix.Amount = packet.ReadInt32("Amount", indexes);
            hotfix.QuestID = packet.ReadInt32("QuestID", indexes);
            hotfix.AchievementID = packet.ReadInt32("AchievementID", indexes);
            hotfix.PlayerLevel = packet.ReadInt32("PlayerLevel", indexes);
            hotfix.TraitNodeEntryID = packet.ReadInt32("TraitNodeEntryID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);

            Storage.TraitCurrencySourceHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                TraitCurrencySourceLocaleHotfix1200 hotfixLocale = new TraitCurrencySourceLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    RequirementLang = hotfix.Requirement,
                };
                Storage.TraitCurrencySourceHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void TraitDefinitionHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitDefinitionHotfix1200 hotfix = new TraitDefinitionHotfix1200();

            hotfix.OverrideName = packet.ReadCString("OverrideName", indexes);
            hotfix.OverrideSubtext = packet.ReadCString("OverrideSubtext", indexes);
            hotfix.OverrideDescription = packet.ReadCString("OverrideDescription", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.OverrideIcon = packet.ReadInt32("OverrideIcon", indexes);
            hotfix.OverridesSpellID = packet.ReadInt32("OverridesSpellID", indexes);
            hotfix.VisibleSpellID = packet.ReadInt32("VisibleSpellID", indexes);

            Storage.TraitDefinitionHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                TraitDefinitionLocaleHotfix1200 hotfixLocale = new TraitDefinitionLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    OverrideNameLang = hotfix.OverrideName,
                    OverrideSubtextLang = hotfix.OverrideSubtext,
                    OverrideDescriptionLang = hotfix.OverrideDescription,
                };
                Storage.TraitDefinitionHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void TraitDefinitionEffectPointsHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitDefinitionEffectPointsHotfix1200 hotfix = new TraitDefinitionEffectPointsHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitDefinitionID = packet.ReadUInt32("TraitDefinitionID", indexes);
            hotfix.EffectIndex = packet.ReadInt32("EffectIndex", indexes);
            hotfix.OperationType = packet.ReadInt32("OperationType", indexes);
            hotfix.CurveID = packet.ReadInt32("CurveID", indexes);

            Storage.TraitDefinitionEffectPointsHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitEdgeHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitEdgeHotfix1200 hotfix = new TraitEdgeHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.VisualStyle = packet.ReadInt32("VisualStyle", indexes);
            hotfix.LeftTraitNodeID = packet.ReadUInt32("LeftTraitNodeID", indexes);
            hotfix.RightTraitNodeID = packet.ReadInt32("RightTraitNodeID", indexes);
            hotfix.Type = packet.ReadByte("Type", indexes);

            Storage.TraitEdgeHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitNodeHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitNodeHotfix1200 hotfix = new TraitNodeHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitTreeID = packet.ReadUInt32("TraitTreeID", indexes);
            hotfix.PosX = packet.ReadInt32("PosX", indexes);
            hotfix.PosY = packet.ReadInt32("PosY", indexes);
            hotfix.Type = packet.ReadByte("Type", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.TraitSubTreeID = packet.ReadInt32("TraitSubTreeID", indexes);

            Storage.TraitNodeHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitNodeEntryHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitNodeEntryHotfix1200 hotfix = new TraitNodeEntryHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitDefinitionID = packet.ReadInt32("TraitDefinitionID", indexes);
            hotfix.MaxRanks = packet.ReadInt32("MaxRanks", indexes);
            hotfix.NodeEntryType = packet.ReadByte("NodeEntryType", indexes);
            hotfix.TraitSubTreeID = packet.ReadInt32("TraitSubTreeID", indexes);

            Storage.TraitNodeEntryHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitNodeEntryXTraitCondHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitNodeEntryXTraitCondHotfix1200 hotfix = new TraitNodeEntryXTraitCondHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitCondID = packet.ReadInt32("TraitCondID", indexes);
            hotfix.TraitNodeEntryID = packet.ReadUInt32("TraitNodeEntryID", indexes);

            Storage.TraitNodeEntryXTraitCondHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitNodeEntryXTraitCostHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitNodeEntryXTraitCostHotfix1200 hotfix = new TraitNodeEntryXTraitCostHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitNodeEntryID = packet.ReadUInt32("TraitNodeEntryID", indexes);
            hotfix.TraitCostID = packet.ReadInt32("TraitCostID", indexes);

            Storage.TraitNodeEntryXTraitCostHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitNodeGroupHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitNodeGroupHotfix1200 hotfix = new TraitNodeGroupHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitTreeID = packet.ReadUInt32("TraitTreeID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.TraitNodeGroupHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitNodeGroupXTraitCondHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitNodeGroupXTraitCondHotfix1200 hotfix = new TraitNodeGroupXTraitCondHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitCondID = packet.ReadInt32("TraitCondID", indexes);
            hotfix.TraitNodeGroupID = packet.ReadUInt32("TraitNodeGroupID", indexes);

            Storage.TraitNodeGroupXTraitCondHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitNodeGroupXTraitCostHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitNodeGroupXTraitCostHotfix1200 hotfix = new TraitNodeGroupXTraitCostHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitNodeGroupID = packet.ReadUInt32("TraitNodeGroupID", indexes);
            hotfix.TraitCostID = packet.ReadInt32("TraitCostID", indexes);

            Storage.TraitNodeGroupXTraitCostHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitNodeGroupXTraitNodeHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitNodeGroupXTraitNodeHotfix1200 hotfix = new TraitNodeGroupXTraitNodeHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitNodeGroupID = packet.ReadUInt32("TraitNodeGroupID", indexes);
            hotfix.TraitNodeID = packet.ReadInt32("TraitNodeID", indexes);
            hotfix.Index = packet.ReadInt32("Index", indexes);

            Storage.TraitNodeGroupXTraitNodeHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitNodeXTraitCondHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitNodeXTraitCondHotfix1200 hotfix = new TraitNodeXTraitCondHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitCondID = packet.ReadInt32("TraitCondID", indexes);
            hotfix.TraitNodeID = packet.ReadUInt32("TraitNodeID", indexes);

            Storage.TraitNodeXTraitCondHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitNodeXTraitCostHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitNodeXTraitCostHotfix1200 hotfix = new TraitNodeXTraitCostHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitNodeID = packet.ReadUInt32("TraitNodeID", indexes);
            hotfix.TraitCostID = packet.ReadInt32("TraitCostID", indexes);

            Storage.TraitNodeXTraitCostHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitNodeXTraitNodeEntryHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitNodeXTraitNodeEntryHotfix1200 hotfix = new TraitNodeXTraitNodeEntryHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitNodeID = packet.ReadUInt32("TraitNodeID", indexes);
            hotfix.TraitNodeEntryID = packet.ReadInt32("TraitNodeEntryID", indexes);
            hotfix.Index = packet.ReadInt32("Index", indexes);

            Storage.TraitNodeXTraitNodeEntryHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitSubTreeHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitSubTreeHotfix1200 hotfix = new TraitSubTreeHotfix1200();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.UiTextureAtlasElementID = packet.ReadInt32("UiTextureAtlasElementID", indexes);
            hotfix.TraitTreeID = packet.ReadUInt32("TraitTreeID", indexes);

            Storage.TraitSubTreeHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                TraitSubTreeLocaleHotfix1200 hotfixLocale = new TraitSubTreeLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description
                };
                Storage.TraitSubTreeHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void TraitSystemHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitSystemHotfix1200 hotfix = new TraitSystemHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.WidgetSetID = packet.ReadInt32("WidgetSetID", indexes);
            hotfix.TraitChangeSpell = packet.ReadInt32("TraitChangeSpell", indexes);
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);
            hotfix.VariationType = packet.ReadInt32("VariationType", indexes);

            Storage.TraitSystemHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitTreeHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitTreeHotfix1200 hotfix = new TraitTreeHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitSystemID = packet.ReadUInt32("TraitSystemID", indexes);
            hotfix.Unused1000_1 = packet.ReadInt32("Unused1000_1", indexes);
            hotfix.FirstTraitNodeID = packet.ReadInt32("FirstTraitNodeID", indexes);
            hotfix.PlayerConditionID = packet.ReadInt32("PlayerConditionID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.Unused1000_2 = packet.ReadSingle("Unused1000_2", indexes);
            hotfix.Unused1000_3 = packet.ReadSingle("Unused1000_3", indexes);

            Storage.TraitTreeHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitTreeLoadoutHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitTreeLoadoutHotfix1200 hotfix = new TraitTreeLoadoutHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitTreeID = packet.ReadUInt32("TraitTreeID", indexes);
            hotfix.ChrSpecializationID = packet.ReadInt32("ChrSpecializationID", indexes);

            Storage.TraitTreeLoadoutHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitTreeLoadoutEntryHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitTreeLoadoutEntryHotfix1200 hotfix = new TraitTreeLoadoutEntryHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitTreeLoadoutID = packet.ReadUInt32("TraitTreeLoadoutID", indexes);
            hotfix.SelectedTraitNodeID = packet.ReadInt32("SelectedTraitNodeID", indexes);
            hotfix.SelectedTraitNodeEntryID = packet.ReadInt32("SelectedTraitNodeEntryID", indexes);
            hotfix.NumPoints = packet.ReadInt32("NumPoints", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);

            Storage.TraitTreeLoadoutEntryHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitTreeXTraitCostHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitTreeXTraitCostHotfix1200 hotfix = new TraitTreeXTraitCostHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitTreeID = packet.ReadUInt32("TraitTreeID", indexes);
            hotfix.TraitCostID = packet.ReadInt32("TraitCostID", indexes);

            Storage.TraitTreeXTraitCostHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitTreeXTraitCurrencyHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TraitTreeXTraitCurrencyHotfix1200 hotfix = new TraitTreeXTraitCurrencyHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Index = packet.ReadInt32("Index", indexes);
            hotfix.TraitTreeID = packet.ReadUInt32("TraitTreeID", indexes);
            hotfix.TraitCurrencyID = packet.ReadInt32("TraitCurrencyID", indexes);

            Storage.TraitTreeXTraitCurrencyHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TransmogHolidayHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TransmogHolidayHotfix1200 hotfix = new TransmogHolidayHotfix1200();

            hotfix.ID = entry;
            hotfix.RequiredTransmogHoliday = packet.ReadInt32("RequiredTransmogHoliday", indexes);

            Storage.TransmogHolidayHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TransmogIllusionHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TransmogIllusionHotfix1200 hotfix = new TransmogIllusionHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.UnlockConditionID = packet.ReadInt32("UnlockConditionID", indexes);
            hotfix.TransmogCost = packet.ReadInt32("TransmogCost", indexes);
            hotfix.SpellItemEnchantmentID = packet.ReadInt32("SpellItemEnchantmentID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.TransmogIllusionHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TransmogSetHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TransmogSetHotfix1200 hotfix = new TransmogSetHotfix1200();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ClassMask = packet.ReadInt32("ClassMask", indexes);
            hotfix.TrackingQuestID = packet.ReadInt32("TrackingQuestID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.TransmogSetGroupID = packet.ReadInt32("TransmogSetGroupID", indexes);
            hotfix.ItemNameDescriptionID = packet.ReadInt32("ItemNameDescriptionID", indexes);
            hotfix.ParentTransmogSetID = packet.ReadUInt32("ParentTransmogSetID", indexes);
            hotfix.Unknown810 = packet.ReadInt32("Unknown810", indexes);
            hotfix.ExpansionID = packet.ReadInt32("ExpansionID", indexes);
            hotfix.PatchID = packet.ReadInt32("PatchID", indexes);
            hotfix.UiOrder = packet.ReadInt32("UiOrder", indexes);
            hotfix.PlayerConditionID = packet.ReadInt32("PlayerConditionID", indexes);

            Storage.TransmogSetHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                TransmogSetLocaleHotfix1200 hotfixLocale = new TransmogSetLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.TransmogSetHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void TransmogSetGroupHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TransmogSetGroupHotfix1200 hotfix = new TransmogSetGroupHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);

            Storage.TransmogSetGroupHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                TransmogSetGroupLocaleHotfix1200 hotfixLocale = new TransmogSetGroupLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.TransmogSetGroupHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void TransmogSetItemHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TransmogSetItemHotfix1200 hotfix = new TransmogSetItemHotfix1200();

            hotfix.ID = entry;
            hotfix.TransmogSetID = packet.ReadUInt32("TransmogSetID", indexes);
            hotfix.ItemModifiedAppearanceID = packet.ReadUInt32("ItemModifiedAppearanceID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.TransmogSetItemHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TransportAnimationHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TransportAnimationHotfix1200 hotfix = new TransportAnimationHotfix1200();

            hotfix.ID = entry;
            hotfix.PosX = packet.ReadSingle("PosX", indexes);
            hotfix.PosY = packet.ReadSingle("PosY", indexes);
            hotfix.PosZ = packet.ReadSingle("PosZ", indexes);
            hotfix.SequenceID = packet.ReadByte("SequenceID", indexes);
            hotfix.TimeIndex = packet.ReadUInt32("TimeIndex", indexes);
            hotfix.TransportID = packet.ReadUInt32("TransportID", indexes);

            Storage.TransportAnimationHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void TransportRotationHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            TransportRotationHotfix1200 hotfix = new TransportRotationHotfix1200();

            hotfix.ID = entry;
            hotfix.Rot = new float?[4];
            for (int i = 0; i < 4; i++)
                hotfix.Rot[i] = packet.ReadSingle("Rot", indexes, i);
            hotfix.TimeIndex = packet.ReadUInt32("TimeIndex", indexes);
            hotfix.GameObjectsID = packet.ReadUInt32("GameObjectsID", indexes);

            Storage.TransportRotationHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void UiMapHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            UiMapHotfix1200 hotfix = new UiMapHotfix1200();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ParentUiMapID = packet.ReadUInt32("ParentUiMapID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.System = packet.ReadByte("System", indexes);
            hotfix.Type = packet.ReadByte("Type", indexes);
            hotfix.BountySetID = packet.ReadInt32("BountySetID", indexes);
            hotfix.BountyDisplayLocation = packet.ReadUInt32("BountyDisplayLocation", indexes);
            hotfix.VisibilityPlayerConditionID = packet.ReadInt32("VisibilityPlayerConditionID", indexes);
            hotfix.HelpTextPosition = packet.ReadSByte("HelpTextPosition", indexes);
            hotfix.BkgAtlasID = packet.ReadInt32("BkgAtlasID", indexes);
            hotfix.AlternateUiMapGroup = packet.ReadInt32("AlternateUiMapGroup", indexes);
            hotfix.ContentTuningID = packet.ReadInt32("ContentTuningID", indexes);
            hotfix.AdventureMapTextureKitID = packet.ReadInt32("AdventureMapTextureKitID", indexes);
            hotfix.MapArtZoneTextPosition = packet.ReadSByte("MapArtZoneTextPosition", indexes);

            Storage.UiMapHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                UiMapLocaleHotfix1200 hotfixLocale = new UiMapLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.UiMapHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void UiMapAssignmentHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            UiMapAssignmentHotfix1200 hotfix = new UiMapAssignmentHotfix1200();

            hotfix.UiMinX = packet.ReadSingle("UiMinX", indexes);
            hotfix.UiMinY = packet.ReadSingle("UiMinY", indexes);
            hotfix.UiMaxX = packet.ReadSingle("UiMaxX", indexes);
            hotfix.UiMaxY = packet.ReadSingle("UiMaxY", indexes);
            hotfix.Region1X = packet.ReadSingle("Region1X", indexes);
            hotfix.Region1Y = packet.ReadSingle("Region1Y", indexes);
            hotfix.Region1Z = packet.ReadSingle("Region1Z", indexes);
            hotfix.Region2X = packet.ReadSingle("Region2X", indexes);
            hotfix.Region2Y = packet.ReadSingle("Region2Y", indexes);
            hotfix.Region2Z = packet.ReadSingle("Region2Z", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.UiMapID = packet.ReadUInt32("UiMapID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.MapID = packet.ReadInt32("MapID", indexes);
            hotfix.AreaID = packet.ReadInt32("AreaID", indexes);
            hotfix.WmoDoodadPlacementID = packet.ReadInt32("WmoDoodadPlacementID", indexes);
            hotfix.WmoGroupID = packet.ReadInt32("WmoGroupID", indexes);
            hotfix.Unknown1125 = packet.ReadInt32("Unknown1125", indexes);

            Storage.UiMapAssignmentHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void UiMapLinkHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            UiMapLinkHotfix1200 hotfix = new UiMapLinkHotfix1200();

            hotfix.UiMinX = packet.ReadSingle("UiMinX", indexes);
            hotfix.UiMinY = packet.ReadSingle("UiMinY", indexes);
            hotfix.UiMaxX = packet.ReadSingle("UiMaxX", indexes);
            hotfix.UiMaxY = packet.ReadSingle("UiMaxY", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ParentUiMapID = packet.ReadUInt32("ParentUiMapID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.ChildUiMapID = packet.ReadInt32("ChildUiMapID", indexes);
            hotfix.PlayerConditionID = packet.ReadInt32("PlayerConditionID", indexes);
            hotfix.OverrideHighlightFileDataID = packet.ReadInt32("OverrideHighlightFileDataID", indexes);
            hotfix.OverrideHighlightAtlasID = packet.ReadInt32("OverrideHighlightAtlasID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.UiMapLinkHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void UiMapXMapArtHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            UiMapXMapArtHotfix1200 hotfix = new UiMapXMapArtHotfix1200();

            hotfix.ID = entry;
            hotfix.PhaseID = packet.ReadInt32("PhaseID", indexes);
            hotfix.UiMapArtID = packet.ReadInt32("UiMapArtID", indexes);
            hotfix.UiMapID = packet.ReadUInt32("UiMapID", indexes);

            Storage.UiMapXMapArtHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void UiSplashScreenHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            UiSplashScreenHotfix1200 hotfix = new UiSplashScreenHotfix1200();

            hotfix.ID = entry;
            hotfix.Header = packet.ReadCString("Header", indexes);
            hotfix.TopLeftFeatureTitle = packet.ReadCString("TopLeftFeatureTitle", indexes);
            hotfix.TopLeftFeatureDesc = packet.ReadCString("TopLeftFeatureDesc", indexes);
            hotfix.BottomLeftFeatureTitle = packet.ReadCString("BottomLeftFeatureTitle", indexes);
            hotfix.BottomLeftFeatureDesc = packet.ReadCString("BottomLeftFeatureDesc", indexes);
            hotfix.RightFeatureTitle = packet.ReadCString("RightFeatureTitle", indexes);
            hotfix.RightFeatureDesc = packet.ReadCString("RightFeatureDesc", indexes);
            hotfix.AllianceQuestID = packet.ReadInt32("AllianceQuestID", indexes);
            hotfix.HordeQuestID = packet.ReadInt32("HordeQuestID", indexes);
            hotfix.ScreenType = packet.ReadByte("ScreenType", indexes);
            hotfix.TextureKitID = packet.ReadInt32("TextureKitID", indexes);
            hotfix.SoundKitID = packet.ReadInt32("SoundKitID", indexes);
            hotfix.PlayerConditionID = packet.ReadInt32("PlayerConditionID", indexes);
            hotfix.CharLevelConditionID = packet.ReadInt32("CharLevelConditionID", indexes);
            hotfix.RequiredTimeEventPassed = packet.ReadInt32("RequiredTimeEventPassed", indexes);

            Storage.UiSplashScreenHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                UiSplashScreenLocaleHotfix1200 hotfixLocale = new UiSplashScreenLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    HeaderLang = hotfix.Header,
                    TopLeftFeatureTitleLang = hotfix.TopLeftFeatureTitle,
                    TopLeftFeatureDescLang = hotfix.TopLeftFeatureDesc,
                    BottomLeftFeatureTitleLang = hotfix.BottomLeftFeatureTitle,
                    BottomLeftFeatureDescLang = hotfix.BottomLeftFeatureDesc,
                    RightFeatureTitleLang = hotfix.RightFeatureTitle,
                    RightFeatureDescLang = hotfix.RightFeatureDesc,
                };
                Storage.UiSplashScreenHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void UnitConditionHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            UnitConditionHotfix1200 hotfix = new UnitConditionHotfix1200();

            hotfix.ID = entry;
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.Variable = new byte?[8];
            for (int i = 0; i < 8; i++)
                hotfix.Variable[i] = packet.ReadByte("Variable", indexes, i);
            hotfix.Op = new byte?[8];
            for (int i = 0; i < 8; i++)
                hotfix.Op[i] = packet.ReadByte("Op", indexes, i);
            hotfix.Value = new int?[8];
            for (int i = 0; i < 8; i++)
                hotfix.Value[i] = packet.ReadInt32("Value", indexes, i);

            Storage.UnitConditionHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void UnitPowerBarHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            UnitPowerBarHotfix1200 hotfix = new UnitPowerBarHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Cost = packet.ReadCString("Cost", indexes);
            hotfix.OutOfError = packet.ReadCString("OutOfError", indexes);
            hotfix.ToolTip = packet.ReadCString("ToolTip", indexes);
            hotfix.MinPower = packet.ReadUInt32("MinPower", indexes);
            hotfix.MaxPower = packet.ReadUInt32("MaxPower", indexes);
            hotfix.StartPower = packet.ReadUInt32("StartPower", indexes);
            hotfix.CenterPower = packet.ReadByte("CenterPower", indexes);
            hotfix.RegenerationPeace = packet.ReadSingle("RegenerationPeace", indexes);
            hotfix.RegenerationCombat = packet.ReadSingle("RegenerationCombat", indexes);
            hotfix.BarType = packet.ReadByte("BarType", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.StartInset = packet.ReadSingle("StartInset", indexes);
            hotfix.EndInset = packet.ReadSingle("EndInset", indexes);
            hotfix.FileDataID = new int?[6];
            for (int i = 0; i < 6; i++)
                hotfix.FileDataID[i] = packet.ReadInt32("FileDataID", indexes, i);
            hotfix.Color = new int?[6];
            for (int i = 0; i < 6; i++)
                hotfix.Color[i] = packet.ReadInt32("Color", indexes, i);

            Storage.UnitPowerBarHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                UnitPowerBarLocaleHotfix1200 hotfixLocale = new UnitPowerBarLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    CostLang = hotfix.Cost,
                    OutOfErrorLang = hotfix.OutOfError,
                    ToolTipLang = hotfix.ToolTip,
                };
                Storage.UnitPowerBarHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void VehicleHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            VehicleHotfix1200 hotfix = new VehicleHotfix1200();

            hotfix.ID = entry;
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.FlagsB = packet.ReadInt32("FlagsB", indexes);
            hotfix.TurnSpeed = packet.ReadSingle("TurnSpeed", indexes);
            hotfix.PitchSpeed = packet.ReadSingle("PitchSpeed", indexes);
            hotfix.PitchMin = packet.ReadSingle("PitchMin", indexes);
            hotfix.PitchMax = packet.ReadSingle("PitchMax", indexes);
            hotfix.MouseLookOffsetPitch = packet.ReadSingle("MouseLookOffsetPitch", indexes);
            hotfix.CameraFadeDistScalarMin = packet.ReadSingle("CameraFadeDistScalarMin", indexes);
            hotfix.CameraFadeDistScalarMax = packet.ReadSingle("CameraFadeDistScalarMax", indexes);
            hotfix.CameraPitchOffset = packet.ReadSingle("CameraPitchOffset", indexes);
            hotfix.FacingLimitRight = packet.ReadSingle("FacingLimitRight", indexes);
            hotfix.FacingLimitLeft = packet.ReadSingle("FacingLimitLeft", indexes);
            hotfix.CameraYawOffset = packet.ReadSingle("CameraYawOffset", indexes);
            hotfix.VehicleUIIndicatorID = packet.ReadUInt16("VehicleUIIndicatorID", indexes);
            hotfix.MissileTargetingID = packet.ReadInt32("MissileTargetingID", indexes);
            hotfix.VehiclePOITypeID = packet.ReadUInt16("VehiclePOITypeID", indexes);
            hotfix.SeatID = new ushort?[8];
            for (int i = 0; i < 8; i++)
                hotfix.SeatID[i] = packet.ReadUInt16("SeatID", indexes, i);
            hotfix.PowerDisplayID = new ushort?[3];
            for (int i = 0; i < 3; i++)
                hotfix.PowerDisplayID[i] = packet.ReadUInt16("PowerDisplayID", indexes, i);

            Storage.VehicleHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void VehicleSeatHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            VehicleSeatHotfix1200 hotfix = new VehicleSeatHotfix1200();

            hotfix.ID = entry;
            hotfix.AttachmentOffsetX = packet.ReadSingle("AttachmentOffsetX", indexes);
            hotfix.AttachmentOffsetY = packet.ReadSingle("AttachmentOffsetY", indexes);
            hotfix.AttachmentOffsetZ = packet.ReadSingle("AttachmentOffsetZ", indexes);
            hotfix.CameraOffsetX = packet.ReadSingle("CameraOffsetX", indexes);
            hotfix.CameraOffsetY = packet.ReadSingle("CameraOffsetY", indexes);
            hotfix.CameraOffsetZ = packet.ReadSingle("CameraOffsetZ", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.FlagsB = packet.ReadInt32("FlagsB", indexes);
            hotfix.FlagsC = packet.ReadInt32("FlagsC", indexes);
            hotfix.AttachmentID = packet.ReadInt32("AttachmentID", indexes);
            hotfix.EnterPreDelay = packet.ReadSingle("EnterPreDelay", indexes);
            hotfix.EnterSpeed = packet.ReadSingle("EnterSpeed", indexes);
            hotfix.EnterGravity = packet.ReadSingle("EnterGravity", indexes);
            hotfix.EnterMinDuration = packet.ReadSingle("EnterMinDuration", indexes);
            hotfix.EnterMaxDuration = packet.ReadSingle("EnterMaxDuration", indexes);
            hotfix.EnterMinArcHeight = packet.ReadSingle("EnterMinArcHeight", indexes);
            hotfix.EnterMaxArcHeight = packet.ReadSingle("EnterMaxArcHeight", indexes);
            hotfix.EnterAnimStart = packet.ReadInt16("EnterAnimStart", indexes);
            hotfix.EnterAnimLoop = packet.ReadInt16("EnterAnimLoop", indexes);
            hotfix.RideAnimStart = packet.ReadInt16("RideAnimStart", indexes);
            hotfix.RideAnimLoop = packet.ReadInt16("RideAnimLoop", indexes);
            hotfix.RideUpperAnimStart = packet.ReadInt16("RideUpperAnimStart", indexes);
            hotfix.RideUpperAnimLoop = packet.ReadInt16("RideUpperAnimLoop", indexes);
            hotfix.ExitPreDelay = packet.ReadSingle("ExitPreDelay", indexes);
            hotfix.ExitSpeed = packet.ReadSingle("ExitSpeed", indexes);
            hotfix.ExitGravity = packet.ReadSingle("ExitGravity", indexes);
            hotfix.ExitMinDuration = packet.ReadSingle("ExitMinDuration", indexes);
            hotfix.ExitMaxDuration = packet.ReadSingle("ExitMaxDuration", indexes);
            hotfix.ExitMinArcHeight = packet.ReadSingle("ExitMinArcHeight", indexes);
            hotfix.ExitMaxArcHeight = packet.ReadSingle("ExitMaxArcHeight", indexes);
            hotfix.ExitAnimStart = packet.ReadInt16("ExitAnimStart", indexes);
            hotfix.ExitAnimLoop = packet.ReadInt16("ExitAnimLoop", indexes);
            hotfix.ExitAnimEnd = packet.ReadInt16("ExitAnimEnd", indexes);
            hotfix.VehicleEnterAnim = packet.ReadInt16("VehicleEnterAnim", indexes);
            hotfix.VehicleEnterAnimBone = packet.ReadSByte("VehicleEnterAnimBone", indexes);
            hotfix.VehicleExitAnim = packet.ReadInt16("VehicleExitAnim", indexes);
            hotfix.VehicleExitAnimBone = packet.ReadSByte("VehicleExitAnimBone", indexes);
            hotfix.VehicleRideAnimLoop = packet.ReadInt16("VehicleRideAnimLoop", indexes);
            hotfix.VehicleRideAnimLoopBone = packet.ReadSByte("VehicleRideAnimLoopBone", indexes);
            hotfix.PassengerAttachmentID = packet.ReadSByte("PassengerAttachmentID", indexes);
            hotfix.PassengerYaw = packet.ReadSingle("PassengerYaw", indexes);
            hotfix.PassengerPitch = packet.ReadSingle("PassengerPitch", indexes);
            hotfix.PassengerRoll = packet.ReadSingle("PassengerRoll", indexes);
            hotfix.VehicleEnterAnimDelay = packet.ReadSingle("VehicleEnterAnimDelay", indexes);
            hotfix.VehicleExitAnimDelay = packet.ReadSingle("VehicleExitAnimDelay", indexes);
            hotfix.VehicleAbilityDisplay = packet.ReadSByte("VehicleAbilityDisplay", indexes);
            hotfix.EnterUISoundID = packet.ReadUInt32("EnterUISoundID", indexes);
            hotfix.ExitUISoundID = packet.ReadUInt32("ExitUISoundID", indexes);
            hotfix.UiSkinFileDataID = packet.ReadInt32("UiSkinFileDataID", indexes);
            hotfix.CameraEnteringDelay = packet.ReadSingle("CameraEnteringDelay", indexes);
            hotfix.CameraEnteringDuration = packet.ReadSingle("CameraEnteringDuration", indexes);
            hotfix.CameraExitingDelay = packet.ReadSingle("CameraExitingDelay", indexes);
            hotfix.CameraExitingDuration = packet.ReadSingle("CameraExitingDuration", indexes);
            hotfix.CameraPosChaseRate = packet.ReadSingle("CameraPosChaseRate", indexes);
            hotfix.CameraFacingChaseRate = packet.ReadSingle("CameraFacingChaseRate", indexes);
            hotfix.CameraEnteringZoom = packet.ReadSingle("CameraEnteringZoom", indexes);
            hotfix.CameraSeatZoomMin = packet.ReadSingle("CameraSeatZoomMin", indexes);
            hotfix.CameraSeatZoomMax = packet.ReadSingle("CameraSeatZoomMax", indexes);
            hotfix.EnterAnimKitID = packet.ReadInt32("EnterAnimKitID", indexes);
            hotfix.RideAnimKitID = packet.ReadInt32("RideAnimKitID", indexes);
            hotfix.ExitAnimKitID = packet.ReadInt32("ExitAnimKitID", indexes);
            hotfix.VehicleEnterAnimKitID = packet.ReadInt32("VehicleEnterAnimKitID", indexes);
            hotfix.VehicleRideAnimKitID = packet.ReadInt32("VehicleRideAnimKitID", indexes);
            hotfix.VehicleExitAnimKitID = packet.ReadInt32("VehicleExitAnimKitID", indexes);
            hotfix.CameraModeID = packet.ReadInt16("CameraModeID", indexes);

            Storage.VehicleSeatHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void VignetteHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            VignetteHotfix1200 hotfix = new VignetteHotfix1200();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.VisibleTrackingQuestID = packet.ReadUInt32("VisibleTrackingQuestID", indexes);
            hotfix.QuestFeedbackEffectID = packet.ReadUInt32("QuestFeedbackEffectID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.MaxHeight = packet.ReadSingle("MaxHeight", indexes);
            hotfix.MinHeight = packet.ReadSingle("MinHeight", indexes);
            hotfix.VignetteType = packet.ReadSByte("VignetteType", indexes);
            hotfix.RewardQuestID = packet.ReadInt32("RewardQuestID", indexes);
            hotfix.UiWidgetSetID = packet.ReadInt32("UiWidgetSetID", indexes);
            hotfix.UiMapPinInfoID = packet.ReadInt32("UiMapPinInfoID", indexes);
            hotfix.ObjectiveType = packet.ReadSByte("ObjectiveType", indexes);

            Storage.VignetteHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                VignetteLocaleHotfix1200 hotfixLocale = new VignetteLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.VignetteHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void WarbandSceneHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            WarbandSceneHotfix1200 hotfix = new WarbandSceneHotfix1200();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.PositionX = packet.ReadSingle("PositionX", indexes);
            hotfix.PositionY = packet.ReadSingle("PositionY", indexes);
            hotfix.PositionZ = packet.ReadSingle("PositionZ", indexes);
            hotfix.LookAtX = packet.ReadSingle("LookAtX", indexes);
            hotfix.LookAtY = packet.ReadSingle("LookAtY", indexes);
            hotfix.LookAtZ = packet.ReadSingle("LookAtZ", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.MapID = packet.ReadUInt32("MapID", indexes);
            hotfix.Fov = packet.ReadSingle("Fov", indexes);
            hotfix.TimeOfDay = packet.ReadInt32("TimeOfDay", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.SoundAmbienceID = packet.ReadInt32("SoundAmbienceID", indexes);
            hotfix.Quality = packet.ReadSByte("Quality", indexes);
            hotfix.TextureKit = packet.ReadInt32("TextureKit", indexes);
            hotfix.DefaultScenePriority = packet.ReadInt32("DefaultScenePriority", indexes);

            Storage.WarbandSceneHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                WarbandSceneLocaleHotfix1200 hotfixLocale = new WarbandSceneLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.WarbandSceneHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void WmoAreaTableHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            WmoAreaTableHotfix1200 hotfix = new WmoAreaTableHotfix1200();

            hotfix.AreaName = packet.ReadCString("AreaName", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.WmoID = packet.ReadUInt16("WmoID", indexes);
            hotfix.NameSetID = packet.ReadByte("NameSetID", indexes);
            hotfix.WmoGroupID = packet.ReadInt32("WmoGroupID", indexes);
            hotfix.SoundProviderPref = packet.ReadByte("SoundProviderPref", indexes);
            hotfix.SoundProviderPrefUnderwater = packet.ReadByte("SoundProviderPrefUnderwater", indexes);
            hotfix.AmbienceID = packet.ReadUInt16("AmbienceID", indexes);
            hotfix.UwAmbience = packet.ReadUInt16("UwAmbience", indexes);
            hotfix.ZoneMusic = packet.ReadUInt16("ZoneMusic", indexes);
            hotfix.UwZoneMusic = packet.ReadUInt32("UwZoneMusic", indexes);
            hotfix.IntroSound = packet.ReadUInt16("IntroSound", indexes);
            hotfix.UwIntroSound = packet.ReadUInt16("UwIntroSound", indexes);
            hotfix.AreaTableID = packet.ReadUInt16("AreaTableID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.WmoAreaTableHotfixes1200.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                WmoAreaTableLocaleHotfix1200 hotfixLocale = new WmoAreaTableLocaleHotfix1200
                {
                    ID = hotfix.ID,
                    AreaNameLang = hotfix.AreaName,
                };
                Storage.WmoAreaTableHotfixesLocale1200.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void WorldEffectHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            WorldEffectHotfix1200 hotfix = new WorldEffectHotfix1200();

            hotfix.ID = entry;
            hotfix.QuestFeedbackEffectID = packet.ReadUInt32("QuestFeedbackEffectID", indexes);
            hotfix.WhenToDisplay = packet.ReadInt32("WhenToDisplay", indexes);
            hotfix.TargetType = packet.ReadByte("TargetType", indexes);
            hotfix.TargetAsset = packet.ReadInt32("TargetAsset", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.CombatConditionID = packet.ReadUInt16("CombatConditionID", indexes);

            Storage.WorldEffectHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void WorldMapOverlayHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            WorldMapOverlayHotfix1200 hotfix = new WorldMapOverlayHotfix1200();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.UiMapArtID = packet.ReadUInt32("UiMapArtID", indexes);
            hotfix.TextureWidth = packet.ReadUInt16("TextureWidth", indexes);
            hotfix.TextureHeight = packet.ReadUInt16("TextureHeight", indexes);
            hotfix.OffsetX = packet.ReadInt32("OffsetX", indexes);
            hotfix.OffsetY = packet.ReadInt32("OffsetY", indexes);
            hotfix.HitRectTop = packet.ReadInt32("HitRectTop", indexes);
            hotfix.HitRectBottom = packet.ReadInt32("HitRectBottom", indexes);
            hotfix.HitRectLeft = packet.ReadInt32("HitRectLeft", indexes);
            hotfix.HitRectRight = packet.ReadInt32("HitRectRight", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.AreaID = new uint?[4];
            for (int i = 0; i < 4; i++)
                hotfix.AreaID[i] = packet.ReadUInt32("AreaID", indexes, i);

            Storage.WorldMapOverlayHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        public static void WorldStateExpressionHandler1200(Packet packet, uint entry, params object[] indexes)
        {
            WorldStateExpressionHotfix1200 hotfix = new WorldStateExpressionHotfix1200();

            hotfix.ID = entry;
            hotfix.Expression = packet.ReadCString("Expression", indexes);

            Storage.WorldStateExpressionHotfixes1200.Add(hotfix, packet.TimeSpan);
        }

        private static void HotfixBlobHandler(Packet packet, DB2Hash type, int entry, params object[] indexes)
        {
            packet.WriteLine($"(Entry: {entry} TableHash: {type}) has missing structure. HotfixBlob entry generated!", indexes);
            packet.AsHex();
            packet.ReadToEnd();

            HotfixBlob hotfixBlob = new HotfixBlob
            {
                TableHash = type,
                RecordID = entry,
                Blob = new Blob(packet.GetStream(0))
            };

            Storage.HotfixBlobs.Add(hotfixBlob);
        }

        public class HotfixRecord
        {
            public uint HotfixId;
            public uint UniqueId;
            public DB2Hash Type;
            public int RecordId;
            public int HotfixDataSize;
            public HotfixStatus Status;
        }

        static void ReadHotfix(Packet packet, DB2Hash type, int entry, HotfixStatus status, Packet db2File, params object[] indexes)
        {
            switch (status)
            {
                case HotfixStatus.Valid:
                {
                    packet.AddSniffData(StoreNameType.None, entry, type.ToString());

                    switch (type)
                    {
                        case DB2Hash.Achievement:
                        {
                            AchievementHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AchievementCategory:
                        {
                            AchievementCategoryHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AdventureJournal:
                        {
                            AdventureJournalHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AdventureMapPoi:
                        {
                            AdventureMapPoiHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AnimationData:
                        {
                            AnimationDataHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AnimKit:
                        {
                            AnimKitHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AreaGroupMember:
                        {
                            AreaGroupMemberHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AreaTable:
                        {
                            AreaTableHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AreaTrigger:
                        {
                            AreaTriggerHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AreaTriggerActionSet:
                        {
                            AreaTriggerActionSetHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ArmorLocation:
                        {
                            ArmorLocationHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Artifact:
                        {
                            ArtifactHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ArtifactAppearance:
                        {
                            ArtifactAppearanceHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ArtifactAppearanceSet:
                        {
                            ArtifactAppearanceSetHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ArtifactCategory:
                        {
                            ArtifactCategoryHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ArtifactPower:
                        {
                            ArtifactPowerHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ArtifactPowerLink:
                        {
                            ArtifactPowerLinkHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ArtifactPowerPicker:
                        {
                            ArtifactPowerPickerHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ArtifactPowerRank:
                        {
                            ArtifactPowerRankHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ArtifactQuestXp:
                        {
                            ArtifactQuestXpHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ArtifactTier:
                        {
                            ArtifactTierHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ArtifactUnlock:
                        {
                            ArtifactUnlockHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AuctionHouse:
                        {
                            AuctionHouseHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AzeriteEmpoweredItem:
                        {
                            AzeriteEmpoweredItemHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AzeriteEssence:
                        {
                            AzeriteEssenceHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AzeriteEssencePower:
                        {
                            AzeriteEssencePowerHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AzeriteItem:
                        {
                            AzeriteItemHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AzeriteItemMilestonePower:
                        {
                            AzeriteItemMilestonePowerHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AzeriteKnowledgeMultiplier:
                        {
                            AzeriteKnowledgeMultiplierHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AzeriteLevelInfo:
                        {
                            AzeriteLevelInfoHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AzeritePower:
                        {
                            AzeritePowerHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AzeritePowerSetMember:
                        {
                            AzeritePowerSetMemberHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AzeriteTierUnlock:
                        {
                            AzeriteTierUnlockHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AzeriteTierUnlockSet:
                        {
                            AzeriteTierUnlockSetHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AzeriteUnlockMapping:
                        {
                            AzeriteUnlockMappingHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.BankTab:
                        {
                            BankTabHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.BannedAddons:
                        {
                            BannedAddonsHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.BarberShopStyle:
                        {
                            BarberShopStyleHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.BattlePetAbility:
                        {
                            BattlePetAbilityHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.BattlePetBreedQuality:
                        {
                            BattlePetBreedQualityHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.BattlePetBreedState:
                        {
                            BattlePetBreedStateHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.BattlePetSpecies:
                        {
                            BattlePetSpeciesHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.BattlePetSpeciesState:
                        {
                            BattlePetSpeciesStateHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.BattlemasterList:
                        {
                            BattlemasterListHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.BattlemasterListXMap:
                        {
                            BattlemasterListXMapHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.BroadcastText:
                        {
                            BroadcastTextHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.BroadcastTextDuration:
                        {
                            BroadcastTextDurationHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CfgCategories:
                        {
                            CfgCategoriesHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CfgRegions:
                        {
                            CfgRegionsHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChallengeModeItemBonusOverride:
                        {
                            ChallengeModeItemBonusOverrideHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CharBaseInfo:
                        {
                            CharBaseInfoHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CharTitles:
                        {
                            CharTitlesHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CharacterLoadout:
                        {
                            CharacterLoadoutHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CharacterLoadoutItem:
                        {
                            CharacterLoadoutItemHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChatChannels:
                        {
                            ChatChannelsHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrClassUiDisplay:
                        {
                            ChrClassUiDisplayHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrClasses:
                        {
                            ChrClassesHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrClassesXPowerTypes:
                        {
                            ChrClassesXPowerTypesHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrCustomizationChoice:
                        {
                            ChrCustomizationChoiceHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrCustomizationDisplayInfo:
                        {
                            ChrCustomizationDisplayInfoHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrCustomizationElement:
                        {
                            ChrCustomizationElementHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrCustomizationOption:
                        {
                            ChrCustomizationOptionHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrCustomizationReq:
                        {
                            ChrCustomizationReqHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrCustomizationReqChoice:
                        {
                            ChrCustomizationReqChoiceHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrModel:
                        {
                            ChrModelHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrRaceXChrModel:
                        {
                            ChrRaceXChrModelHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrRaces:
                        {
                            ChrRacesHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrSpecialization:
                        {
                            ChrSpecializationHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CinematicCamera:
                        {
                            CinematicCameraHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CinematicSequences:
                        {
                            CinematicSequencesHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ConditionalChrModel:
                        {
                            ConditionalChrModelHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ConditionalContentTuning:
                        {
                            ConditionalContentTuningHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ContentTuning:
                        {
                            ContentTuningHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ContentTuningXExpected:
                        {
                            ContentTuningXExpectedHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ContentTuningXLabel:
                        {
                            ContentTuningXLabelHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ConversationLine:
                        {
                            ConversationLineHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CorruptionEffects:
                        {
                            CorruptionEffectsHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CraftingQuality:
                        {
                            CraftingQualityHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CreatureDisplayInfo:
                        {
                            CreatureDisplayInfoHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CreatureDisplayInfoExtra:
                        {
                            CreatureDisplayInfoExtraHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CreatureFamily:
                        {
                            CreatureFamilyHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CreatureLabel:
                        {
                            CreatureLabelHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CreatureModelData:
                        {
                            CreatureModelDataHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CreatureType:
                        {
                            CreatureTypeHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Criteria:
                        {
                            CriteriaHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CriteriaTree:
                        {
                            CriteriaTreeHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CurrencyContainer:
                        {
                            CurrencyContainerHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CurrencyTypes:
                        {
                            CurrencyTypesHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Curve:
                        {
                            CurveHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CurvePoint:
                        {
                            CurvePointHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.DestructibleModelData:
                        {
                            DestructibleModelDataHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Difficulty:
                        {
                            DifficultyHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.DungeonEncounter:
                        {
                            DungeonEncounterHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.DurabilityCosts:
                        {
                            DurabilityCostsHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.DurabilityQuality:
                        {
                            DurabilityQualityHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Emotes:
                        {
                            EmotesHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.EmotesText:
                        {
                            EmotesTextHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.EmotesTextSound:
                        {
                            EmotesTextSoundHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ExpectedStat:
                        {
                            ExpectedStatHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ExpectedStatMod:
                        {
                            ExpectedStatModHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Faction:
                        {
                            FactionHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.FactionTemplate:
                        {
                            FactionTemplateHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.FlightCapability:
                        {
                            FlightCapabilityHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.FriendshipRepReaction:
                        {
                            FriendshipRepReactionHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.FriendshipReputation:
                        {
                            FriendshipReputationHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GameobjectArtKit:
                        {
                            GameobjectArtKitHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GameobjectDisplayInfo:
                        {
                            GameobjectDisplayInfoHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GameobjectLabel:
                        {
                            GameobjectLabelHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Gameobjects:
                        {
                            GameobjectsHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GarrAbility:
                        {
                            GarrAbilityHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GarrBuilding:
                        {
                            GarrBuildingHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GarrBuildingPlotInst:
                        {
                            GarrBuildingPlotInstHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GarrClassSpec:
                        {
                            GarrClassSpecHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GarrFollower:
                        {
                            GarrFollowerHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GarrFollowerXAbility:
                        {
                            GarrFollowerXAbilityHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GarrMission:
                        {
                            GarrMissionHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GarrPlot:
                        {
                            GarrPlotHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GarrPlotBuilding:
                        {
                            GarrPlotBuildingHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GarrPlotInstance:
                        {
                            GarrPlotInstanceHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GarrSiteLevel:
                        {
                            GarrSiteLevelHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GarrSiteLevelPlotInst:
                        {
                            GarrSiteLevelPlotInstHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GarrTalentTree:
                        {
                            GarrTalentTreeHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GemProperties:
                        {
                            GemPropertiesHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GlobalCurve:
                        {
                            GlobalCurveHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GlyphBindableSpell:
                        {
                            GlyphBindableSpellHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GlyphProperties:
                        {
                            GlyphPropertiesHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GlyphRequiredSpec:
                        {
                            GlyphRequiredSpecHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GossipNpcOption:
                        {
                            GossipNpcOptionHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GuildColorBackground:
                        {
                            GuildColorBackgroundHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GuildColorBorder:
                        {
                            GuildColorBorderHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GuildColorEmblem:
                        {
                            GuildColorEmblemHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GuildPerkSpells:
                        {
                            GuildPerkSpellsHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Heirloom:
                        {
                            HeirloomHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Holidays:
                        {
                            HolidaysHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ImportPriceArmor:
                        {
                            ImportPriceArmorHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ImportPriceQuality:
                        {
                            ImportPriceQualityHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ImportPriceShield:
                        {
                            ImportPriceShieldHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ImportPriceWeapon:
                        {
                            ImportPriceWeaponHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Item:
                        {
                            ItemHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemAppearance:
                        {
                            ItemAppearanceHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemArmorQuality:
                        {
                            ItemArmorQualityHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemArmorShield:
                        {
                            ItemArmorShieldHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemArmorTotal:
                        {
                            ItemArmorTotalHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemBagFamily:
                        {
                            ItemBagFamilyHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemBonus:
                        {
                            ItemBonusHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemBonusListGroupEntry:
                        {
                            ItemBonusListGroupEntryHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemBonusListLevelDelta:
                        {
                            ItemBonusListLevelDeltaHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemBonusTree:
                        {
                            ItemBonusTreeHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemBonusTreeNode:
                        {
                            ItemBonusTreeNodeHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemChildEquipment:
                        {
                            ItemChildEquipmentHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemClass:
                        {
                            ItemClassHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemContextPickerEntry:
                        {
                            ItemContextPickerEntryHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemCreationContext:
                        {
                            ItemCreationContextHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemCurrencyCost:
                        {
                            ItemCurrencyCostHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemDamageAmmo:
                        {
                            ItemDamageAmmoHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemDamageOneHand:
                        {
                            ItemDamageOneHandHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemDamageOneHandCaster:
                        {
                            ItemDamageOneHandCasterHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemDamageTwoHand:
                        {
                            ItemDamageTwoHandHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemDamageTwoHandCaster:
                        {
                            ItemDamageTwoHandCasterHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemDisenchantLoot:
                        {
                            ItemDisenchantLootHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemEffect:
                        {
                            ItemEffectHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemExtendedCost:
                        {
                            ItemExtendedCostHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemLevelSelector:
                        {
                            ItemLevelSelectorHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemLevelSelectorQuality:
                        {
                            ItemLevelSelectorQualityHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemLevelSelectorQualitySet:
                        {
                            ItemLevelSelectorQualitySetHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemLimitCategory:
                        {
                            ItemLimitCategoryHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemLimitCategoryCondition:
                        {
                            ItemLimitCategoryConditionHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemModifiedAppearance:
                        {
                            ItemModifiedAppearanceHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemModifiedAppearanceExtra:
                        {
                            ItemModifiedAppearanceExtraHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemNameDescription:
                        {
                            ItemNameDescriptionHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemOffsetCurve:
                        {
                            ItemOffsetCurveHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemPriceBase:
                        {
                            ItemPriceBaseHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemScalingConfig:
                        {
                            ItemScalingConfigHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemSearchName:
                        {
                            ItemSearchNameHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemSet:
                        {
                            ItemSetHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemSetSpell:
                        {
                            ItemSetSpellHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemSparse:
                        {
                            ItemSparseHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemSpec:
                        {
                            ItemSpecHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemSpecOverride:
                        {
                            ItemSpecOverrideHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemSquishEra:
                        {
                            ItemSquishEraHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemXBonusTree:
                        {
                            ItemXBonusTreeHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemXItemEffect:
                        {
                            ItemXItemEffectHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.JournalEncounter:
                        {
                            JournalEncounterHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.JournalEncounterSection:
                        {
                            JournalEncounterSectionHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.JournalInstance:
                        {
                            JournalInstanceHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.JournalTier:
                        {
                            JournalTierHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Keychain:
                        {
                            KeychainHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.KeystoneAffix:
                        {
                            KeystoneAffixHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.LanguageWords:
                        {
                            LanguageWordsHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Languages:
                        {
                            LanguagesHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.LfgDungeons:
                        {
                            LfgDungeonsHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Light:
                        {
                            LightHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.LiquidType:
                        {
                            LiquidTypeHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Location:
                        {
                            LocationHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Lock:
                        {
                            LockHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.MailTemplate:
                        {
                            MailTemplateHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Map:
                        {
                            MapHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.MapChallengeMode:
                        {
                            MapChallengeModeHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.MapDifficulty:
                        {
                            MapDifficultyHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.MapDifficultyXCondition:
                        {
                            MapDifficultyXConditionHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.MawPower:
                        {
                            MawPowerHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ModifiedCraftingItem:
                        {
                            ModifiedCraftingItemHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ModifierTree:
                        {
                            ModifierTreeHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Mount:
                        {
                            MountHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.MountCapability:
                        {
                            MountCapabilityHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.MountEquipment:
                        {
                            MountEquipmentHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.MountTypeXCapability:
                        {
                            MountTypeXCapabilityHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.MountXDisplay:
                        {
                            MountXDisplayHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Movie:
                        {
                            MovieHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.MythicPlusSeason:
                        {
                            MythicPlusSeasonHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.NameGen:
                        {
                            NameGenHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.NamesProfanity:
                        {
                            NamesProfanityHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.NamesReserved:
                        {
                            NamesReservedHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.NamesReservedLocale:
                        {
                            NamesReservedLocaleHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.NumTalentsAtLevel:
                        {
                            NumTalentsAtLevelHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.OverrideSpellData:
                        {
                            OverrideSpellDataHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ParagonReputation:
                        {
                            ParagonReputationHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Path:
                        {
                            PathHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PathNode:
                        {
                            PathNodeHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PathProperty:
                        {
                            PathPropertyHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PerksActivity:
                        {
                            PerksActivityHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Phase:
                        {
                            PhaseHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PhaseXPhaseGroup:
                        {
                            PhaseXPhaseGroupHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PlayerCondition:
                        {
                            PlayerConditionHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PlayerDataElementAccount:
                        {
                            PlayerDataElementAccountHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PlayerDataElementCharacter:
                        {
                            PlayerDataElementCharacterHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PlayerDataFlagAccount:
                        {
                            PlayerDataFlagAccountHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PlayerDataFlagCharacter:
                        {
                            PlayerDataFlagCharacterHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PowerDisplay:
                        {
                            PowerDisplayHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PowerType:
                        {
                            PowerTypeHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PrestigeLevelInfo:
                        {
                            PrestigeLevelInfoHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PvpDifficulty:
                        {
                            PvpDifficultyHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PvpSeason:
                        {
                            PvpSeasonHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PvpStat:
                        {
                            PvpStatHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PvpTalent:
                        {
                            PvpTalentHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PvpTalentCategory:
                        {
                            PvpTalentCategoryHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PvpTalentSlotUnlock:
                        {
                            PvpTalentSlotUnlockHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PvpTier:
                        {
                            PvpTierHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.QuestFactionReward:
                        {
                            QuestFactionRewardHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.QuestInfo:
                        {
                            QuestInfoHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.QuestLineXQuest:
                        {
                            QuestLineXQuestHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.QuestMoneyReward:
                        {
                            QuestMoneyRewardHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.QuestPackageItem:
                        {
                            QuestPackageItemHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.QuestSort:
                        {
                            QuestSortHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.QuestV2:
                        {
                            QuestV2Handler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.QuestXp:
                        {
                            QuestXpHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.RandPropPoints:
                        {
                            RandPropPointsHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.RewardPack:
                        {
                            RewardPackHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.RewardPackXCurrencyType:
                        {
                            RewardPackXCurrencyTypeHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.RewardPackXItem:
                        {
                            RewardPackXItemHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Scenario:
                        {
                            ScenarioHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ScenarioStep:
                        {
                            ScenarioStepHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SceneScript:
                        {
                            SceneScriptHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SceneScriptGlobalText:
                        {
                            SceneScriptGlobalTextHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SceneScriptPackage:
                        {
                            SceneScriptPackageHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SceneScriptText:
                        {
                            SceneScriptTextHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ServerMessages:
                        {
                            ServerMessagesHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SkillLine:
                        {
                            SkillLineHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SkillLineAbility:
                        {
                            SkillLineAbilityHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SkillLineXTraitTree:
                        {
                            SkillLineXTraitTreeHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SkillRaceClassInfo:
                        {
                            SkillRaceClassInfoHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SoulbindConduitRank:
                        {
                            SoulbindConduitRankHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SoundKit:
                        {
                            SoundKitHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpecializationSpells:
                        {
                            SpecializationSpellsHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpecSetMember:
                        {
                            SpecSetMemberHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellAuraOptions:
                        {
                            SpellAuraOptionsHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellAuraRestrictions:
                        {
                            SpellAuraRestrictionsHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellCastTimes:
                        {
                            SpellCastTimesHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellCastingRequirements:
                        {
                            SpellCastingRequirementsHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellCategories:
                        {
                            SpellCategoriesHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellCategory:
                        {
                            SpellCategoryHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellClassOptions:
                        {
                            SpellClassOptionsHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellCooldowns:
                        {
                            SpellCooldownsHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellDuration:
                        {
                            SpellDurationHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellEffect:
                        {
                            SpellEffectHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellEmpower:
                        {
                            SpellEmpowerHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellEmpowerStage:
                        {
                            SpellEmpowerStageHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellEquippedItems:
                        {
                            SpellEquippedItemsHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellFocusObject:
                        {
                            SpellFocusObjectHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellInterrupts:
                        {
                            SpellInterruptsHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellItemEnchantment:
                        {
                            SpellItemEnchantmentHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellKeyboundOverride:
                        {
                            SpellKeyboundOverrideHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellItemEnchantmentCondition:
                        {
                            SpellItemEnchantmentConditionHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellLabel:
                        {
                            SpellLabelHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellLearnSpell:
                        {
                            SpellLearnSpellHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellLevels:
                        {
                            SpellLevelsHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellMisc:
                        {
                            SpellMiscHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellName:
                        {
                            SpellNameHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellPower:
                        {
                            SpellPowerHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellPowerDifficulty:
                        {
                            SpellPowerDifficultyHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellProcsPerMinute:
                        {
                            SpellProcsPerMinuteHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellProcsPerMinuteMod:
                        {
                            SpellProcsPerMinuteModHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellRadius:
                        {
                            SpellRadiusHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellRange:
                        {
                            SpellRangeHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellReagents:
                        {
                            SpellReagentsHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellReagentsCurrency:
                        {
                            SpellReagentsCurrencyHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellScaling:
                        {
                            SpellScalingHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellShapeshift:
                        {
                            SpellShapeshiftHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellShapeshiftForm:
                        {
                            SpellShapeshiftFormHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellTargetRestrictions:
                        {
                            SpellTargetRestrictionsHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellTotems:
                        {
                            SpellTotemsHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellVisual:
                        {
                            SpellVisualHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellVisualEffectName:
                        {
                            SpellVisualEffectNameHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellVisualMissile:
                        {
                            SpellVisualMissileHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellVisualKit:
                        {
                            SpellVisualKitHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellXSpellVisual:
                        {
                            SpellXSpellVisualHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SummonProperties:
                        {
                            SummonPropertiesHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TactKey:
                        {
                            TactKeyHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Talent:
                        {
                            TalentHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TaxiNodes:
                        {
                            TaxiNodesHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TaxiPath:
                        {
                            TaxiPathHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TaxiPathNode:
                        {
                            TaxiPathNodeHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TotemCategory:
                        {
                            TotemCategoryHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Toy:
                        {
                            ToyHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TransmogHoliday:
                        {
                            TransmogHolidayHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitCond:
                        {
                            TraitCondHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitCondAccountElement:
                        {
                            TraitCondAccountElementHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitCost:
                        {
                            TraitCostHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitCurrency:
                        {
                            TraitCurrencyHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitCurrencySource:
                        {
                            TraitCurrencySourceHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitDefinition:
                        {
                            TraitDefinitionHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitDefinitionEffectPoints:
                        {
                            TraitDefinitionEffectPointsHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitEdge:
                        {
                            TraitEdgeHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitNode:
                        {
                            TraitNodeHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitNodeEntry:
                        {
                            TraitNodeEntryHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitNodeEntryXTraitCond:
                        {
                            TraitNodeEntryXTraitCondHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitNodeEntryXTraitCost:
                        {
                            TraitNodeEntryXTraitCostHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitNodeGroup:
                        {
                            TraitNodeGroupHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitNodeGroupXTraitCond:
                        {
                            TraitNodeGroupXTraitCondHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitNodeGroupXTraitCost:
                        {
                            TraitNodeGroupXTraitCostHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitNodeGroupXTraitNode:
                        {
                            TraitNodeGroupXTraitNodeHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitNodeXTraitCond:
                        {
                            TraitNodeXTraitCondHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitNodeXTraitCost:
                        {
                            TraitNodeXTraitCostHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitNodeXTraitNodeEntry:
                        {
                            TraitNodeXTraitNodeEntryHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitSubTree:
                        {
                            TraitSubTreeHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitSystem:
                        {
                            TraitSystemHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitTree:
                        {
                            TraitTreeHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitTreeLoadout:
                        {
                            TraitTreeLoadoutHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitTreeLoadoutEntry:
                        {
                            TraitTreeLoadoutEntryHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitTreeXTraitCost:
                        {
                            TraitTreeXTraitCostHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TraitTreeXTraitCurrency:
                        {
                            TraitTreeXTraitCurrencyHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TransmogIllusion:
                        {
                            TransmogIllusionHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TransmogSet:
                        {
                            TransmogSetHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TransmogSetGroup:
                        {
                            TransmogSetGroupHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TransmogSetItem:
                        {
                            TransmogSetItemHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TransportAnimation:
                        {
                            TransportAnimationHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TransportRotation:
                        {
                            TransportRotationHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.UiMap:
                        {
                            UiMapHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.UiMapAssignment:
                        {
                            UiMapAssignmentHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.UiMapLink:
                        {
                            UiMapLinkHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.UiMapXMapArt:
                        {
                            UiMapXMapArtHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.UiSplashScreen:
                        {
                            UiSplashScreenHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.UnitCondition:
                        {
                            UnitConditionHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.UnitPowerBar:
                        {
                            UnitPowerBarHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Vehicle:
                        {
                            VehicleHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.VehicleSeat:
                        {
                            VehicleSeatHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Vignette:
                        {
                            VignetteHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.WarbandScene:
                        {
                            WarbandSceneHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.WmoAreaTable:
                        {
                            WmoAreaTableHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.WorldEffect:
                        {
                            WorldEffectHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.WorldMapOverlay:
                        {
                            WorldMapOverlayHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.WorldStateExpression:
                        {
                            WorldStateExpressionHandler1200(db2File, (uint)entry, indexes);
                            break;
                        }
                        default:
                        {
                            HotfixBlobHandler(db2File, type, entry, indexes);
                            break;
                        }
                    }

                    if (db2File.Position != db2File.Length)
                        HandleHotfixOptionalData(packet, type, entry, db2File);

                    db2File.ClosePacket(false);
                    break;
                }
                case HotfixStatus.RecordRemoved:
                {
                    packet.AddLine($"Row {entry} has been removed.", indexes);
                    break;
                }
                case HotfixStatus.Invalid:
                {
                    packet.AddLine($"Row {entry} is invalid.", indexes);
                    break;
                }
                case HotfixStatus.NotPublic:
                {
                    packet.AddLine($"Row {entry} is not public.", indexes);
                    break;
                }
                default:
                {
                    packet.AddLine($"Unhandled status: {status}", indexes);
                    break;
                }
            }
        }

        static void ReadHotfixData(Packet packet, List<HotfixRecord> records, params object[] indexes)
        {
            int count = 0;
            foreach (var record in records)
            {
                var hotfixId = packet.AddValue("HotfixID", record.HotfixId, count, indexes, "HotfixRecord");
                var uniqueId = packet.AddValue("UniqueID", record.UniqueId, count, indexes, "HotfixRecord");
                var type = packet.AddValue("TableHash", record.Type, count, indexes, "HotfixRecord");
                var entry = packet.AddValue("RecordID", record.RecordId, count, indexes, "HotfixRecord");
                var dataSize = packet.AddValue("Size", record.HotfixDataSize, count, indexes, "HotfixRecord");
                var status = packet.AddValue("Status", record.Status, count, indexes, "HotfixRecord");
                var data = packet.ReadBytes(dataSize);
                var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

                ReadHotfix(packet, type, entry, status, db2File, count, indexes);

                HotfixData hotfixData = new HotfixData
                {
                    ID = hotfixId,
                    UniqueID = uniqueId,
                    TableHash = type,
                    RecordID = entry,
                    Status = status
                };

                Storage.HotfixDatas.Add(hotfixData);
                count++;
            }
        }

        private static void HandleHotfixOptionalData(Packet packet, DB2Hash type, int entry, Packet db2File)
        {
            var leftSize = db2File.Length - db2File.Position;
            var backupPosition = db2File.Position;

            // 28 bytes = size of TactKey optional data
            if (leftSize % 28 == 0)
            {
                var tactKeyCount = leftSize / 28;

                for (int i = 0; i < tactKeyCount; ++i)
                {
                    // get hash, we need to verify
                    var hash = db2File.ReadUInt32E<DB2Hash>();

                    // check if hash is valid hash, we only support TactKey optional data yet
                    if (hash == DB2Hash.TactKey)
                    {
                        // read optional data
                        var optionalData = db2File.ReadBytes(24);

                        packet.AddValue($"(OptionalData) [{i}] Key:", hash);
                        packet.AddValue($"(OptionalData) [{i}] OptionalData:", Convert.ToHexString(optionalData));

                        HotfixOptionalData hotfixOptionalData = new HotfixOptionalData
                        {
                            // data to link the optional data to correct hotfix
                            TableHash = type,
                            RecordID = entry,
                            Key = hash,

                            Data = new Blob(optionalData)
                        };

                        Storage.HotfixOptionalDatas.Add(hotfixOptionalData);
                    }
                    else
                    {
                        db2File.SetPosition(backupPosition);
                        db2File.WriteLine($"(Entry: {entry} TableHash: {type}) has incorrect structure OR optional data. PacketLength: {db2File.Length} CurrentPosition: {db2File.Position}");
                        db2File.AsHex();
                    }
                }
            }
            else
            {
                db2File.WriteLine($"(Entry: {entry} TableHash: {type}) has incorrect structure OR optional data. PacketLength: {db2File.Length} CurrentPosition: {db2File.Position}");
                db2File.AsHex();
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_HOTFIX_MESSAGE)]
        [Parser(Opcode.SMSG_HOTFIX_CONNECT)]
        public static void HandleHotfixData(Packet packet)
        {
            var hotfixRecords = new List<HotfixRecord>();
            var hotfixCount = packet.ReadUInt32("HotfixCount");

            for (var i = 0u; i < hotfixCount; ++i)
            {
                var hotfixRecord = new HotfixRecord();
                packet.ResetBitReader();

                hotfixRecord.HotfixId = packet.ReadUInt32();
                hotfixRecord.UniqueId = packet.ReadUInt32();
                hotfixRecord.Type = packet.ReadUInt32E<DB2Hash>();
                hotfixRecord.RecordId = packet.ReadInt32();
                hotfixRecord.HotfixDataSize = packet.ReadInt32();
                packet.ResetBitReader();
                hotfixRecord.Status = (HotfixStatus)packet.ReadBits(3);

                hotfixRecords.Add(hotfixRecord);
            }

            var dataSize = packet.ReadInt32("HotfixDataSize");
            var data = packet.ReadBytes(dataSize);
            var hotfixData = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

            ReadHotfixData(hotfixData, hotfixRecords, "HotfixData");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            var dbReply = packet.Holder.DbReply = new();
            var type = packet.ReadUInt32E<DB2Hash>("TableHash");
            dbReply.TableHash = (uint)type;
            var entry = dbReply.RecordId = packet.ReadInt32("RecordID");
            var timeStamp = packet.ReadUInt32();
            var time = packet.AddValue("Timestamp", Utilities.GetDateTimeFromUnixTime(timeStamp));
            dbReply.Time = Timestamp.FromDateTime(DateTime.SpecifyKind(time, DateTimeKind.Utc));
            int statusBits = 3;
            var status = packet.ReadBitsE<HotfixStatus>("Status", statusBits);
            switch (status)
            {
                case HotfixStatus.Valid:
                    dbReply.Status = PacketDbReplyRecordStatus.RecordStatusValid;
                    break;
                case HotfixStatus.RecordRemoved:
                    dbReply.Status = PacketDbReplyRecordStatus.RecordStatusRecordRemoved;
                    break;
                case HotfixStatus.Invalid:
                    dbReply.Status = PacketDbReplyRecordStatus.RecordStatusInvalid;
                    break;
                case HotfixStatus.NotPublic:
                    dbReply.Status = PacketDbReplyRecordStatus.RecordStatusNotPublic;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

            ReadHotfix(packet, type, entry, status, db2File);
        }
    }
}

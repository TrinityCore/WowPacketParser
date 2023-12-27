using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class HotfixHandler
    {
        public static void AchievementHandler340(Packet packet, uint entry, params object[] indexes)
        {
            AchievementHotfix340 hotfix = new AchievementHotfix340();

            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.Title = packet.ReadCString("Title", indexes);
            hotfix.Reward = packet.ReadCString("Reward", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.InstanceID = packet.ReadInt16("InstanceID", indexes);
            hotfix.Faction = packet.ReadSByte("Faction", indexes);
            hotfix.Supercedes = packet.ReadInt16("Supercedes", indexes);
            hotfix.Category = packet.ReadInt16("Category", indexes);
            hotfix.MinimumCriteria = packet.ReadSByte("MinimumCriteria", indexes);
            hotfix.Points = packet.ReadSByte("Points", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.UiOrder = packet.ReadInt16("UiOrder", indexes);
            hotfix.IconFileID = packet.ReadInt32("IconFileID", indexes);
            hotfix.CriteriaTree = packet.ReadUInt32("CriteriaTree", indexes);
            hotfix.SharesCriteria = packet.ReadInt16("SharesCriteria", indexes);

            Storage.AchievementHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AchievementLocaleHotfix340 hotfixLocale = new AchievementLocaleHotfix340
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                    TitleLang = hotfix.Title,
                    RewardLang = hotfix.Reward,
                };
                Storage.AchievementHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void AchievementHandler343(Packet packet, uint entry, params object[] indexes)
        {
            AchievementHotfix343 hotfix = new AchievementHotfix343();

            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.Title = packet.ReadCString("Title", indexes);
            hotfix.Reward = packet.ReadCString("Reward", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.InstanceID = packet.ReadInt16("InstanceID", indexes);
            hotfix.Faction = packet.ReadSByte("Faction", indexes);
            hotfix.Supercedes = packet.ReadInt16("Supercedes", indexes);
            hotfix.Category = packet.ReadInt16("Category", indexes);
            hotfix.MinimumCriteria = packet.ReadSByte("MinimumCriteria", indexes);
            hotfix.Points = packet.ReadSByte("Points", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.UiOrder = packet.ReadInt16("UiOrder", indexes);
            hotfix.IconFileID = packet.ReadInt32("IconFileID", indexes);
            hotfix.CriteriaTree = packet.ReadUInt32("CriteriaTree", indexes);
            hotfix.SharesCriteria = packet.ReadInt16("SharesCriteria", indexes);

            Storage.AchievementHotfixes343.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AchievementLocaleHotfix343 hotfixLocale = new AchievementLocaleHotfix343
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                    TitleLang = hotfix.Title,
                    RewardLang = hotfix.Reward,
                };
                Storage.AchievementHotfixesLocale343.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void AchievementCategoryHandler340(Packet packet, uint entry, params object[] indexes)
        {
            AchievementCategoryHotfix340 hotfix = new AchievementCategoryHotfix340();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Parent = packet.ReadInt16("Parent", indexes);
            hotfix.UiOrder = packet.ReadSByte("UiOrder", indexes);

            Storage.AchievementCategoryHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AchievementCategoryLocaleHotfix340 hotfixLocale = new AchievementCategoryLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.AchievementCategoryHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void AdventureJournalHandler340(Packet packet, uint entry, params object[] indexes)
        {
            AdventureJournalHotfix340 hotfix = new AdventureJournalHotfix340();

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
            hotfix.QuestID = packet.ReadUInt32("QuestID", indexes);
            hotfix.BattleMasterListID = packet.ReadUInt16("BattleMasterListID", indexes);
            hotfix.PriorityMin = packet.ReadByte("PriorityMin", indexes);
            hotfix.PriorityMax = packet.ReadByte("PriorityMax", indexes);
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);
            hotfix.ItemQuantity = packet.ReadUInt32("ItemQuantity", indexes);
            hotfix.CurrencyType = packet.ReadUInt16("CurrencyType", indexes);
            hotfix.CurrencyQuantity = packet.ReadByte("CurrencyQuantity", indexes);
            hotfix.UIMapID = packet.ReadUInt16("UIMapID", indexes);
            hotfix.BonusPlayerConditionID = new uint?[2];
            for (int i = 0; i < 2; i++)
                hotfix.BonusPlayerConditionID[i] = packet.ReadUInt32("BonusPlayerConditionID", indexes, i);
            hotfix.BonusValue = new byte?[2];
            for (int i = 0; i < 2; i++)
                hotfix.BonusValue[i] = packet.ReadByte("BonusValue", indexes, i);

            Storage.AdventureJournalHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AdventureJournalLocaleHotfix340 hotfixLocale = new AdventureJournalLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                    ButtonTextLang = hotfix.ButtonText,
                    RewardDescriptionLang = hotfix.RewardDescription,
                    ContinueDescriptionLang = hotfix.ContinueDescription,
                };
                Storage.AdventureJournalHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void AdventureMapPOIHandler340(Packet packet, uint entry, params object[] indexes)
        {
            AdventureMapPOIHotfix340 hotfix = new AdventureMapPOIHotfix340();

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

            Storage.AdventureMapPOIHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AdventureMapPOILocaleHotfix340 hotfixLocale = new AdventureMapPOILocaleHotfix340
                {
                    ID = hotfix.ID,
                    TitleLang = hotfix.Title,
                    DescriptionLang = hotfix.Description,
                };
                Storage.AdventureMapPOIHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void AnimationDataHandler340(Packet packet, uint entry, params object[] indexes)
        {
            AnimationDataHotfix340 hotfix = new AnimationDataHotfix340();

            hotfix.ID = entry;
            hotfix.Fallback = packet.ReadUInt16("Fallback", indexes);
            hotfix.BehaviorTier = packet.ReadByte("BehaviorTier", indexes);
            hotfix.BehaviorID = packet.ReadInt32("BehaviorID", indexes);
            hotfix.Flags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.AnimationDataHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void AnimKitHandler340(Packet packet, uint entry, params object[] indexes)
        {
            AnimKitHotfix340 hotfix = new AnimKitHotfix340();

            hotfix.ID = entry;
            hotfix.OneShotDuration = packet.ReadUInt32("OneShotDuration", indexes);
            hotfix.OneShotStopAnimKitID = packet.ReadUInt16("OneShotStopAnimKitID", indexes);
            hotfix.LowDefAnimKitID = packet.ReadUInt16("LowDefAnimKitID", indexes);

            Storage.AnimKitHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void AreaGroupMemberHandler340(Packet packet, uint entry, params object[] indexes)
        {
            AreaGroupMemberHotfix340 hotfix = new AreaGroupMemberHotfix340();

            hotfix.ID = entry;
            hotfix.AreaID = packet.ReadUInt16("AreaID", indexes);
            hotfix.AreaGroupID = packet.ReadInt32("AreaGroupID", indexes);

            Storage.AreaGroupMemberHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void AreaTableHandler340(Packet packet, uint entry, params object[] indexes)
        {
            AreaTableHotfix340 hotfix = new AreaTableHotfix340();

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
            hotfix.ExplorationLevel = packet.ReadSByte("ExplorationLevel", indexes);
            hotfix.IntroSound = packet.ReadUInt16("IntroSound", indexes);
            hotfix.UwIntroSound = packet.ReadUInt32("UwIntroSound", indexes);
            hotfix.FactionGroupMask = packet.ReadByte("FactionGroupMask", indexes);
            hotfix.AmbientMultiplier = packet.ReadSingle("AmbientMultiplier", indexes);
            hotfix.MountFlags = packet.ReadByte("MountFlags", indexes);
            hotfix.PvpCombatWorldStateID = packet.ReadInt16("PvpCombatWorldStateID", indexes);
            hotfix.WildBattlePetLevelMin = packet.ReadByte("WildBattlePetLevelMin", indexes);
            hotfix.WildBattlePetLevelMax = packet.ReadByte("WildBattlePetLevelMax", indexes);
            hotfix.WindSettingsID = packet.ReadByte("WindSettingsID", indexes);
            hotfix.Flags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);
            hotfix.LiquidTypeID = new ushort?[4];
            for (int i = 0; i < 4; i++)
                hotfix.LiquidTypeID[i] = packet.ReadUInt16("LiquidTypeID", indexes, i);

            Storage.AreaTableHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AreaTableLocaleHotfix340 hotfixLocale = new AreaTableLocaleHotfix340
                {
                    ID = hotfix.ID,
                    AreaNameLang = hotfix.AreaName,
                };
                Storage.AreaTableHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void AreaTableHandler343(Packet packet, uint entry, params object[] indexes)
        {
            AreaTableHotfix343 hotfix = new AreaTableHotfix343();

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
            hotfix.ExplorationLevel = packet.ReadSByte("ExplorationLevel", indexes);
            hotfix.IntroSound = packet.ReadUInt16("IntroSound", indexes);
            hotfix.UwIntroSound = packet.ReadUInt32("UwIntroSound", indexes);
            hotfix.FactionGroupMask = packet.ReadByte("FactionGroupMask", indexes);
            hotfix.AmbientMultiplier = packet.ReadSingle("AmbientMultiplier", indexes);
            hotfix.MountFlags = packet.ReadInt32("MountFlags", indexes);
            hotfix.PvpCombatWorldStateID = packet.ReadInt16("PvpCombatWorldStateID", indexes);
            hotfix.WildBattlePetLevelMin = packet.ReadByte("WildBattlePetLevelMin", indexes);
            hotfix.WildBattlePetLevelMax = packet.ReadByte("WildBattlePetLevelMax", indexes);
            hotfix.WindSettingsID = packet.ReadByte("WindSettingsID", indexes);
            hotfix.Flags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);
            hotfix.LiquidTypeID = new ushort?[4];
            for (int i = 0; i < 4; i++)
                hotfix.LiquidTypeID[i] = packet.ReadUInt16("LiquidTypeID", indexes, i);

            Storage.AreaTableHotfixes343.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AreaTableLocaleHotfix343 hotfixLocale = new AreaTableLocaleHotfix343
                {
                    ID = hotfix.ID,
                    AreaNameLang = hotfix.AreaName,
                };
                Storage.AreaTableHotfixesLocale343.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void AreaTriggerHandler340(Packet packet, uint entry, params object[] indexes)
        {
            AreaTriggerHotfix340 hotfix = new AreaTriggerHotfix340();

            hotfix.Message = packet.ReadCString("Message", indexes);
            hotfix.PosX = packet.ReadSingle("PosX", indexes);
            hotfix.PosY = packet.ReadSingle("PosY", indexes);
            hotfix.PosZ = packet.ReadSingle("PosZ", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ContinentID = packet.ReadInt16("ContinentID", indexes);
            hotfix.PhaseUseFlags = packet.ReadSByte("PhaseUseFlags", indexes);
            hotfix.PhaseID = packet.ReadInt16("PhaseID", indexes);
            hotfix.PhaseGroupID = packet.ReadInt16("PhaseGroupID", indexes);
            hotfix.Radius = packet.ReadSingle("Radius", indexes);
            hotfix.BoxLength = packet.ReadSingle("BoxLength", indexes);
            hotfix.BoxWidth = packet.ReadSingle("BoxWidth", indexes);
            hotfix.BoxHeight = packet.ReadSingle("BoxHeight", indexes);
            hotfix.BoxYaw = packet.ReadSingle("BoxYaw", indexes);
            hotfix.ShapeType = packet.ReadSByte("ShapeType", indexes);
            hotfix.ShapeID = packet.ReadInt16("ShapeID", indexes);
            hotfix.AreaTriggerActionSetID = packet.ReadInt16("AreaTriggerActionSetID", indexes);
            hotfix.Flags = packet.ReadSByte("Flags", indexes);

            Storage.AreaTriggerHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AreaTriggerLocaleHotfix340 hotfixLocale = new AreaTriggerLocaleHotfix340
                {
                    ID = hotfix.ID,
                    MessageLang = hotfix.Message,
                };
                Storage.AreaTriggerHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ArmorLocationHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ArmorLocationHotfix340 hotfix = new ArmorLocationHotfix340();

            hotfix.ID = entry;
            hotfix.Clothmodifier = packet.ReadSingle("Clothmodifier", indexes);
            hotfix.Leathermodifier = packet.ReadSingle("Leathermodifier", indexes);
            hotfix.Chainmodifier = packet.ReadSingle("Chainmodifier", indexes);
            hotfix.Platemodifier = packet.ReadSingle("Platemodifier", indexes);
            hotfix.Modifier = packet.ReadSingle("Modifier", indexes);

            Storage.ArmorLocationHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ArtifactHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ArtifactHotfix340 hotfix = new ArtifactHotfix340();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.UiTextureKitID = packet.ReadUInt16("UiTextureKitID", indexes);
            hotfix.UiNameColor = packet.ReadInt32("UiNameColor", indexes);
            hotfix.UiBarOverlayColor = packet.ReadInt32("UiBarOverlayColor", indexes);
            hotfix.UiBarBackgroundColor = packet.ReadInt32("UiBarBackgroundColor", indexes);
            hotfix.ChrSpecializationID = packet.ReadUInt16("ChrSpecializationID", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.ArtifactCategoryID = packet.ReadByte("ArtifactCategoryID", indexes);
            hotfix.UiModelSceneID = packet.ReadUInt32("UiModelSceneID", indexes);
            hotfix.SpellVisualKitID = packet.ReadUInt32("SpellVisualKitID", indexes);

            Storage.ArtifactHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ArtifactLocaleHotfix340 hotfixLocale = new ArtifactLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ArtifactHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ArtifactAppearanceHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ArtifactAppearanceHotfix340 hotfix = new ArtifactAppearanceHotfix340();

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
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.UiCameraID = packet.ReadUInt16("UiCameraID", indexes);

            Storage.ArtifactAppearanceHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ArtifactAppearanceLocaleHotfix340 hotfixLocale = new ArtifactAppearanceLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ArtifactAppearanceHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ArtifactAppearanceSetHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ArtifactAppearanceSetHotfix340 hotfix = new ArtifactAppearanceSetHotfix340();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.DisplayIndex = packet.ReadByte("DisplayIndex", indexes);
            hotfix.UiCameraID = packet.ReadUInt16("UiCameraID", indexes);
            hotfix.AltHandUICameraID = packet.ReadUInt16("AltHandUICameraID", indexes);
            hotfix.ForgeAttachmentOverride = packet.ReadSByte("ForgeAttachmentOverride", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.ArtifactID = packet.ReadInt32("ArtifactID", indexes);

            Storage.ArtifactAppearanceSetHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ArtifactAppearanceSetLocaleHotfix340 hotfixLocale = new ArtifactAppearanceSetLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.ArtifactAppearanceSetHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ArtifactCategoryHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ArtifactCategoryHotfix340 hotfix = new ArtifactCategoryHotfix340();

            hotfix.ID = entry;
            hotfix.XpMultCurrencyID = packet.ReadInt16("XpMultCurrencyID", indexes);
            hotfix.XpMultCurveID = packet.ReadInt16("XpMultCurveID", indexes);

            Storage.ArtifactCategoryHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ArtifactPowerHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ArtifactPowerHotfix340 hotfix = new ArtifactPowerHotfix340();

            hotfix.DisplayPosX = packet.ReadSingle("DisplayPosX", indexes);
            hotfix.DisplayPosY = packet.ReadSingle("DisplayPosY", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ArtifactID = packet.ReadByte("ArtifactID", indexes);
            hotfix.MaxPurchasableRank = packet.ReadByte("MaxPurchasableRank", indexes);
            hotfix.Label = packet.ReadInt32("Label", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.Tier = packet.ReadByte("Tier", indexes);

            Storage.ArtifactPowerHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ArtifactPowerLinkHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ArtifactPowerLinkHotfix340 hotfix = new ArtifactPowerLinkHotfix340();

            hotfix.ID = entry;
            hotfix.PowerA = packet.ReadUInt16("PowerA", indexes);
            hotfix.PowerB = packet.ReadUInt16("PowerB", indexes);

            Storage.ArtifactPowerLinkHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ArtifactPowerPickerHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ArtifactPowerPickerHotfix340 hotfix = new ArtifactPowerPickerHotfix340();

            hotfix.ID = entry;
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);

            Storage.ArtifactPowerPickerHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ArtifactPowerRankHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ArtifactPowerRankHotfix340 hotfix = new ArtifactPowerRankHotfix340();

            hotfix.ID = entry;
            hotfix.RankIndex = packet.ReadByte("RankIndex", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.ItemBonusListID = packet.ReadUInt16("ItemBonusListID", indexes);
            hotfix.AuraPointsOverride = packet.ReadSingle("AuraPointsOverride", indexes);
            hotfix.ArtifactPowerID = packet.ReadInt32("ArtifactPowerID", indexes);

            Storage.ArtifactPowerRankHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ArtifactQuestXpHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ArtifactQuestXpHotfix340 hotfix = new ArtifactQuestXpHotfix340();

            hotfix.ID = entry;
            hotfix.Difficulty = new uint?[10];
            for (int i = 0; i < 10; i++)
                hotfix.Difficulty[i] = packet.ReadUInt32("Difficulty", indexes, i);

            Storage.ArtifactQuestXpHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ArtifactTierHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ArtifactTierHotfix340 hotfix = new ArtifactTierHotfix340();

            hotfix.ID = entry;
            hotfix.ArtifactTier = packet.ReadUInt32("ArtifactTier", indexes);
            hotfix.MaxNumTraits = packet.ReadUInt32("MaxNumTraits", indexes);
            hotfix.MaxArtifactKnowledge = packet.ReadUInt32("MaxArtifactKnowledge", indexes);
            hotfix.KnowledgePlayerCondition = packet.ReadUInt32("KnowledgePlayerCondition", indexes);
            hotfix.MinimumEmpowerKnowledge = packet.ReadUInt32("MinimumEmpowerKnowledge", indexes);

            Storage.ArtifactTierHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ArtifactUnlockHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ArtifactUnlockHotfix340 hotfix = new ArtifactUnlockHotfix340();

            hotfix.ID = entry;
            hotfix.PowerID = packet.ReadUInt32("PowerID", indexes);
            hotfix.PowerRank = packet.ReadByte("PowerRank", indexes);
            hotfix.ItemBonusListID = packet.ReadUInt16("ItemBonusListID", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.ArtifactID = packet.ReadInt32("ArtifactID", indexes);

            Storage.ArtifactUnlockHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void AuctionHouseHandler340(Packet packet, uint entry, params object[] indexes)
        {
            AuctionHouseHotfix340 hotfix = new AuctionHouseHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.FactionID = packet.ReadUInt16("FactionID", indexes);
            hotfix.DepositRate = packet.ReadByte("DepositRate", indexes);
            hotfix.ConsignmentRate = packet.ReadByte("ConsignmentRate", indexes);

            Storage.AuctionHouseHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AuctionHouseLocaleHotfix340 hotfixLocale = new AuctionHouseLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.AuctionHouseHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void AzeriteEmpoweredItemHandler340(Packet packet, uint entry, params object[] indexes)
        {
            AzeriteEmpoweredItemHotfix340 hotfix = new AzeriteEmpoweredItemHotfix340();

            hotfix.ID = entry;
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);
            hotfix.AzeriteTierUnlockSetID = packet.ReadUInt32("AzeriteTierUnlockSetID", indexes);
            hotfix.AzeritePowerSetID = packet.ReadUInt32("AzeritePowerSetID", indexes);

            Storage.AzeriteEmpoweredItemHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void AzeriteEssenceHandler340(Packet packet, uint entry, params object[] indexes)
        {
            AzeriteEssenceHotfix340 hotfix = new AzeriteEssenceHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.SpecSetID = packet.ReadInt32("SpecSetID", indexes);

            Storage.AzeriteEssenceHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AzeriteEssenceLocaleHotfix340 hotfixLocale = new AzeriteEssenceLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.AzeriteEssenceHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void AzeriteEssencePowerHandler340(Packet packet, uint entry, params object[] indexes)
        {
            AzeriteEssencePowerHotfix340 hotfix = new AzeriteEssencePowerHotfix340();

            hotfix.ID = entry;
            hotfix.SourceAlliance = packet.ReadCString("SourceAlliance", indexes);
            hotfix.SourceHorde = packet.ReadCString("SourceHorde", indexes);
            hotfix.AzeriteEssenceID = packet.ReadInt32("AzeriteEssenceID", indexes);
            hotfix.Tier = packet.ReadByte("Tier", indexes);
            hotfix.MajorPowerDescription = packet.ReadInt32("MajorPowerDescription", indexes);
            hotfix.MinorPowerDescription = packet.ReadInt32("MinorPowerDescription", indexes);
            hotfix.MajorPowerActual = packet.ReadInt32("MajorPowerActual", indexes);
            hotfix.MinorPowerActual = packet.ReadInt32("MinorPowerActual", indexes);

            Storage.AzeriteEssencePowerHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AzeriteEssencePowerLocaleHotfix340 hotfixLocale = new AzeriteEssencePowerLocaleHotfix340
                {
                    ID = hotfix.ID,
                    SourceAllianceLang = hotfix.SourceAlliance,
                    SourceHordeLang = hotfix.SourceHorde,
                };
                Storage.AzeriteEssencePowerHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void AzeriteItemHandler340(Packet packet, uint entry, params object[] indexes)
        {
            AzeriteItemHotfix340 hotfix = new AzeriteItemHotfix340();

            hotfix.ID = entry;
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);

            Storage.AzeriteItemHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void AzeriteItemMilestonePowerHandler340(Packet packet, uint entry, params object[] indexes)
        {
            AzeriteItemMilestonePowerHotfix340 hotfix = new AzeriteItemMilestonePowerHotfix340();

            hotfix.ID = entry;
            hotfix.RequiredLevel = packet.ReadInt32("RequiredLevel", indexes);
            hotfix.AzeritePowerID = packet.ReadInt32("AzeritePowerID", indexes);
            hotfix.Type = packet.ReadInt32("Type", indexes);
            hotfix.AutoUnlock = packet.ReadInt32("AutoUnlock", indexes);

            Storage.AzeriteItemMilestonePowerHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void AzeriteKnowledgeMultiplierHandler340(Packet packet, uint entry, params object[] indexes)
        {
            AzeriteKnowledgeMultiplierHotfix340 hotfix = new AzeriteKnowledgeMultiplierHotfix340();

            hotfix.ID = entry;
            hotfix.Multiplier = packet.ReadSingle("Multiplier", indexes);

            Storage.AzeriteKnowledgeMultiplierHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void AzeriteLevelInfoHandler340(Packet packet, uint entry, params object[] indexes)
        {
            AzeriteLevelInfoHotfix340 hotfix = new AzeriteLevelInfoHotfix340();

            hotfix.ID = entry;
            hotfix.BaseExperienceToNextLevel = packet.ReadUInt64("BaseExperienceToNextLevel", indexes);
            hotfix.MinimumExperienceToNextLevel = packet.ReadUInt64("MinimumExperienceToNextLevel", indexes);
            hotfix.ItemLevel = packet.ReadInt32("ItemLevel", indexes);

            Storage.AzeriteLevelInfoHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void AzeritePowerHandler340(Packet packet, uint entry, params object[] indexes)
        {
            AzeritePowerHotfix340 hotfix = new AzeritePowerHotfix340();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.ItemBonusListID = packet.ReadInt32("ItemBonusListID", indexes);
            hotfix.SpecSetID = packet.ReadInt32("SpecSetID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.AzeritePowerHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void AzeritePowerSetMemberHandler340(Packet packet, uint entry, params object[] indexes)
        {
            AzeritePowerSetMemberHotfix340 hotfix = new AzeritePowerSetMemberHotfix340();

            hotfix.ID = entry;
            hotfix.AzeritePowerSetID = packet.ReadInt32("AzeritePowerSetID", indexes);
            hotfix.AzeritePowerID = packet.ReadInt32("AzeritePowerID", indexes);
            hotfix.Class = packet.ReadInt32("Class", indexes);
            hotfix.Tier = packet.ReadByte("Tier", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);

            Storage.AzeritePowerSetMemberHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void AzeriteTierUnlockHandler340(Packet packet, uint entry, params object[] indexes)
        {
            AzeriteTierUnlockHotfix340 hotfix = new AzeriteTierUnlockHotfix340();

            hotfix.ID = entry;
            hotfix.ItemCreationContext = packet.ReadByte("ItemCreationContext", indexes);
            hotfix.Tier = packet.ReadByte("Tier", indexes);
            hotfix.AzeriteLevel = packet.ReadByte("AzeriteLevel", indexes);
            hotfix.AzeriteTierUnlockSetID = packet.ReadInt32("AzeriteTierUnlockSetID", indexes);

            Storage.AzeriteTierUnlockHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void AzeriteTierUnlockSetHandler340(Packet packet, uint entry, params object[] indexes)
        {
            AzeriteTierUnlockSetHotfix340 hotfix = new AzeriteTierUnlockSetHotfix340();

            hotfix.ID = entry;
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.AzeriteTierUnlockSetHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void BankBagSlotPricesHandler340(Packet packet, uint entry, params object[] indexes)
        {
            BankBagSlotPricesHotfix340 hotfix = new BankBagSlotPricesHotfix340();

            hotfix.ID = entry;
            hotfix.Cost = packet.ReadUInt32("Cost", indexes);

            Storage.BankBagSlotPricesHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void BannedAddonsHandler340(Packet packet, uint entry, params object[] indexes)
        {
            BannedAddonsHotfix340 hotfix = new BannedAddonsHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Version = packet.ReadCString("Version", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);

            Storage.BannedAddonsHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void BarberShopStyleHandler340(Packet packet, uint entry, params object[] indexes)
        {
            BarberShopStyleHotfix340 hotfix = new BarberShopStyleHotfix340();

            hotfix.DisplayName = packet.ReadCString("DisplayName", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Type = packet.ReadByte("Type", indexes);
            hotfix.CostModifier = packet.ReadSingle("CostModifier", indexes);
            hotfix.Race = packet.ReadByte("Race", indexes);
            hotfix.Sex = packet.ReadByte("Sex", indexes);
            hotfix.Data = packet.ReadByte("Data", indexes);

            Storage.BarberShopStyleHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                BarberShopStyleLocaleHotfix340 hotfixLocale = new BarberShopStyleLocaleHotfix340
                {
                    ID = hotfix.ID,
                    DisplayNameLang = hotfix.DisplayName,
                    DescriptionLang = hotfix.Description,
                };
                Storage.BarberShopStyleHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void BattlePetAbilityHandler340(Packet packet, uint entry, params object[] indexes)
        {
            BattlePetAbilityHotfix340 hotfix = new BattlePetAbilityHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.IconFileDataID = packet.ReadInt32("IconFileDataID", indexes);
            hotfix.PetTypeEnum = packet.ReadSByte("PetTypeEnum", indexes);
            hotfix.Cooldown = packet.ReadUInt32("Cooldown", indexes);
            hotfix.BattlePetVisualID = packet.ReadUInt16("BattlePetVisualID", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);

            Storage.BattlePetAbilityHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                BattlePetAbilityLocaleHotfix340 hotfixLocale = new BattlePetAbilityLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.BattlePetAbilityHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void BattlePetBreedQualityHandler340(Packet packet, uint entry, params object[] indexes)
        {
            BattlePetBreedQualityHotfix340 hotfix = new BattlePetBreedQualityHotfix340();

            hotfix.ID = entry;
            hotfix.StateMultiplier = packet.ReadSingle("StateMultiplier", indexes);
            hotfix.QualityEnum = packet.ReadByte("QualityEnum", indexes);

            Storage.BattlePetBreedQualityHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void BattlePetBreedStateHandler340(Packet packet, uint entry, params object[] indexes)
        {
            BattlePetBreedStateHotfix340 hotfix = new BattlePetBreedStateHotfix340();

            hotfix.ID = entry;
            hotfix.BattlePetStateID = packet.ReadByte("BattlePetStateID", indexes);
            hotfix.Value = packet.ReadUInt16("Value", indexes);
            hotfix.BattlePetBreedID = packet.ReadInt32("BattlePetBreedID", indexes);

            Storage.BattlePetBreedStateHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void BattlePetSpeciesHandler340(Packet packet, uint entry, params object[] indexes)
        {
            BattlePetSpeciesHotfix340 hotfix = new BattlePetSpeciesHotfix340();

            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.SourceText = packet.ReadCString("SourceText", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.CreatureID = packet.ReadInt32("CreatureID", indexes);
            hotfix.SummonSpellID = packet.ReadInt32("SummonSpellID", indexes);
            hotfix.IconFileDataID = packet.ReadInt32("IconFileDataID", indexes);
            hotfix.PetTypeEnum = packet.ReadByte("PetTypeEnum", indexes);
            hotfix.Flags = packet.ReadUInt16("Flags", indexes);
            hotfix.SourceTypeEnum = packet.ReadSByte("SourceTypeEnum", indexes);
            hotfix.CardUIModelSceneID = packet.ReadInt32("CardUIModelSceneID", indexes);
            hotfix.LoadoutUIModelSceneID = packet.ReadInt32("LoadoutUIModelSceneID", indexes);

            Storage.BattlePetSpeciesHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                BattlePetSpeciesLocaleHotfix340 hotfixLocale = new BattlePetSpeciesLocaleHotfix340
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                    SourceTextLang = hotfix.SourceText,
                };
                Storage.BattlePetSpeciesHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void BattlePetSpeciesHandler341(Packet packet, uint entry, params object[] indexes)
        {
            BattlePetSpeciesHotfix341 hotfix = new BattlePetSpeciesHotfix341();

            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.SourceText = packet.ReadCString("SourceText", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.CreatureID = packet.ReadInt32("CreatureID", indexes);
            hotfix.SummonSpellID = packet.ReadInt32("SummonSpellID", indexes);
            hotfix.IconFileDataID = packet.ReadInt32("IconFileDataID", indexes);
            hotfix.PetTypeEnum = packet.ReadByte("PetTypeEnum", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.SourceTypeEnum = packet.ReadSByte("SourceTypeEnum", indexes);
            hotfix.CardUIModelSceneID = packet.ReadInt32("CardUIModelSceneID", indexes);
            hotfix.LoadoutUIModelSceneID = packet.ReadInt32("LoadoutUIModelSceneID", indexes);

            Storage.BattlePetSpeciesHotfixes341.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                BattlePetSpeciesLocaleHotfix341 hotfixLocale = new BattlePetSpeciesLocaleHotfix341
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                    SourceTextLang = hotfix.SourceText,
                };
                Storage.BattlePetSpeciesHotfixesLocale341.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void BattlePetSpeciesStateHandler340(Packet packet, uint entry, params object[] indexes)
        {
            BattlePetSpeciesStateHotfix340 hotfix = new BattlePetSpeciesStateHotfix340();

            hotfix.ID = entry;
            hotfix.BattlePetStateID = packet.ReadByte("BattlePetStateID", indexes);
            hotfix.Value = packet.ReadInt32("Value", indexes);
            hotfix.BattlePetSpeciesID = packet.ReadInt32("BattlePetSpeciesID", indexes);

            Storage.BattlePetSpeciesStateHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void BattlemasterListHandler340(Packet packet, uint entry, params object[] indexes)
        {
            BattlemasterListHotfix340 hotfix = new BattlemasterListHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.GameType = packet.ReadCString("GameType", indexes);
            hotfix.ShortDescription = packet.ReadCString("ShortDescription", indexes);
            hotfix.LongDescription = packet.ReadCString("LongDescription", indexes);
            hotfix.InstanceType = packet.ReadSByte("InstanceType", indexes);
            hotfix.MinLevel = packet.ReadSByte("MinLevel", indexes);
            hotfix.MaxLevel = packet.ReadSByte("MaxLevel", indexes);
            hotfix.RatedPlayers = packet.ReadSByte("RatedPlayers", indexes);
            hotfix.MinPlayers = packet.ReadSByte("MinPlayers", indexes);
            hotfix.MaxPlayers = packet.ReadInt32("MaxPlayers", indexes);
            hotfix.GroupsAllowed = packet.ReadSByte("GroupsAllowed", indexes);
            hotfix.MaxGroupSize = packet.ReadSByte("MaxGroupSize", indexes);
            hotfix.HolidayWorldState = packet.ReadInt16("HolidayWorldState", indexes);
            hotfix.Flags = packet.ReadSByte("Flags", indexes);
            hotfix.IconFileDataID = packet.ReadInt32("IconFileDataID", indexes);
            hotfix.RequiredPlayerConditionID = packet.ReadInt32("RequiredPlayerConditionID", indexes);
            hotfix.MapID = new short?[16];
            for (int i = 0; i < 16; i++)
                hotfix.MapID[i] = packet.ReadInt16("MapID", indexes, i);

            Storage.BattlemasterListHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                BattlemasterListLocaleHotfix340 hotfixLocale = new BattlemasterListLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    GameTypeLang = hotfix.GameType,
                    ShortDescriptionLang = hotfix.ShortDescription,
                    LongDescriptionLang = hotfix.LongDescription,
                };
                Storage.BattlemasterListHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void BroadcastTextHandler340(Packet packet, uint entry, params object[] indexes)
        {
            BroadcastTextHotfix340 hotfix = new BroadcastTextHotfix340();

            hotfix.Text = packet.ReadCString("Text", indexes);
            hotfix.Text1 = packet.ReadCString("Text1", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.LanguageID = packet.ReadInt32("LanguageID", indexes);
            hotfix.ConditionID = packet.ReadInt32("ConditionID", indexes);
            hotfix.EmotesID = packet.ReadUInt16("EmotesID", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
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

            Storage.BroadcastTextHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                BroadcastTextLocaleHotfix340 hotfixLocale = new BroadcastTextLocaleHotfix340
                {
                    ID = hotfix.ID,
                    TextLang = hotfix.Text,
                    Text1Lang = hotfix.Text1,
                };
                Storage.BroadcastTextHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CfgCategoriesHandler340(Packet packet, uint entry, params object[] indexes)
        {
            CfgCategoriesHotfix340 hotfix = new CfgCategoriesHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.LocaleMask = packet.ReadUInt16("LocaleMask", indexes);
            hotfix.CreateCharsetMask = packet.ReadByte("CreateCharsetMask", indexes);
            hotfix.ExistingCharsetMask = packet.ReadByte("ExistingCharsetMask", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.Order = packet.ReadSByte("Order", indexes);

            Storage.CfgCategoriesHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CfgCategoriesLocaleHotfix340 hotfixLocale = new CfgCategoriesLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.CfgCategoriesHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CfgRegionsHandler340(Packet packet, uint entry, params object[] indexes)
        {
            CfgRegionsHotfix340 hotfix = new CfgRegionsHotfix340();

            hotfix.ID = entry;
            hotfix.Tag = packet.ReadCString("Tag", indexes);
            hotfix.RegionID = packet.ReadUInt16("RegionID", indexes);
            hotfix.Raidorigin = packet.ReadUInt32("Raidorigin", indexes);
            hotfix.RegionGroupMask = packet.ReadByte("RegionGroupMask", indexes);
            hotfix.ChallengeOrigin = packet.ReadUInt32("ChallengeOrigin", indexes);

            Storage.CfgRegionsHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void CharTitlesHandler340(Packet packet, uint entry, params object[] indexes)
        {
            CharTitlesHotfix340 hotfix = new CharTitlesHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Name1 = packet.ReadCString("Name1", indexes);
            hotfix.MaskID = packet.ReadInt16("MaskID", indexes);
            hotfix.Flags = packet.ReadSByte("Flags", indexes);

            Storage.CharTitlesHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CharTitlesLocaleHotfix340 hotfixLocale = new CharTitlesLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    Name1Lang = hotfix.Name1,
                };
                Storage.CharTitlesHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CharacterLoadoutHandler340(Packet packet, uint entry, params object[] indexes)
        {
            CharacterLoadoutHotfix340 hotfix = new CharacterLoadoutHotfix340();

            hotfix.Racemask = packet.ReadInt64("Racemask", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ChrClassID = packet.ReadSByte("ChrClassID", indexes);
            hotfix.Purpose = packet.ReadInt32("Purpose", indexes);
            hotfix.ItemContext = packet.ReadSByte("ItemContext", indexes);

            Storage.CharacterLoadoutHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void CharacterLoadoutItemHandler340(Packet packet, uint entry, params object[] indexes)
        {
            CharacterLoadoutItemHotfix340 hotfix = new CharacterLoadoutItemHotfix340();

            hotfix.ID = entry;
            hotfix.CharacterLoadoutID = packet.ReadUInt16("CharacterLoadoutID", indexes);
            hotfix.ItemID = packet.ReadUInt32("ItemID", indexes);

            Storage.CharacterLoadoutItemHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ChatChannelsHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ChatChannelsHotfix340 hotfix = new ChatChannelsHotfix340();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Shortcut = packet.ReadCString("Shortcut", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.FactionGroup = packet.ReadSByte("FactionGroup", indexes);
            hotfix.Ruleset = packet.ReadInt32("Ruleset", indexes);

            Storage.ChatChannelsHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChatChannelsLocaleHotfix340 hotfixLocale = new ChatChannelsLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    ShortcutLang = hotfix.Shortcut,
                };
                Storage.ChatChannelsHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ChatChannelsHandler343(Packet packet, uint entry, params object[] indexes)
        {
            ChatChannelsHotfix343 hotfix = new ChatChannelsHotfix343();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Shortcut = packet.ReadCString("Shortcut", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.FactionGroup = packet.ReadSByte("FactionGroup", indexes);
            hotfix.Ruleset = packet.ReadInt32("Ruleset", indexes);

            Storage.ChatChannelsHotfixes343.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChatChannelsLocaleHotfix343 hotfixLocale = new ChatChannelsLocaleHotfix343
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    ShortcutLang = hotfix.Shortcut,
                };
                Storage.ChatChannelsHotfixesLocale343.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ChrClassUiDisplayHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ChrClassUiDisplayHotfix340 hotfix = new ChrClassUiDisplayHotfix340();

            hotfix.ID = entry;
            hotfix.ChrClassesID = packet.ReadByte("ChrClassesID", indexes);
            hotfix.AdvGuidePlayerConditionID = packet.ReadUInt32("AdvGuidePlayerConditionID", indexes);
            hotfix.SplashPlayerConditionID = packet.ReadUInt32("SplashPlayerConditionID", indexes);

            Storage.ChrClassUiDisplayHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrClassesHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ChrClassesHotfix340 hotfix = new ChrClassesHotfix340();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Filename = packet.ReadCString("Filename", indexes);
            hotfix.NameMale = packet.ReadCString("NameMale", indexes);
            hotfix.NameFemale = packet.ReadCString("NameFemale", indexes);
            hotfix.PetNameToken = packet.ReadCString("PetNameToken", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.CreateScreenFileDataID = packet.ReadUInt32("CreateScreenFileDataID", indexes);
            hotfix.SelectScreenFileDataID = packet.ReadUInt32("SelectScreenFileDataID", indexes);
            hotfix.IconFileDataID = packet.ReadUInt32("IconFileDataID", indexes);
            hotfix.LowResScreenFileDataID = packet.ReadUInt32("LowResScreenFileDataID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.StartingLevel = packet.ReadInt32("StartingLevel", indexes);
            hotfix.RolesMask = packet.ReadUInt32("RolesMask", indexes);
            hotfix.ArmorTypeMask = packet.ReadUInt32("ArmorTypeMask", indexes);
            hotfix.CinematicSequenceID = packet.ReadUInt16("CinematicSequenceID", indexes);
            hotfix.DefaultSpec = packet.ReadUInt16("DefaultSpec", indexes);
            hotfix.HasStrengthAttackBonus = packet.ReadByte("HasStrengthAttackBonus", indexes);
            hotfix.PrimaryStatPriority = packet.ReadByte("PrimaryStatPriority", indexes);
            hotfix.DisplayPower = packet.ReadByte("DisplayPower", indexes);
            hotfix.RangedAttackPowerPerAgility = packet.ReadByte("RangedAttackPowerPerAgility", indexes);
            hotfix.AttackPowerPerAgility = packet.ReadByte("AttackPowerPerAgility", indexes);
            hotfix.AttackPowerPerStrength = packet.ReadByte("AttackPowerPerStrength", indexes);
            hotfix.SpellClassSet = packet.ReadByte("SpellClassSet", indexes);
            hotfix.DamageBonusStat = packet.ReadByte("DamageBonusStat", indexes);
            hotfix.HasRelicSlot = packet.ReadByte("HasRelicSlot", indexes);

            Storage.ChrClassesHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChrClassesLocaleHotfix340 hotfixLocale = new ChrClassesLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    NameMaleLang = hotfix.NameMale,
                    NameFemaleLang = hotfix.NameFemale,
                };
                Storage.ChrClassesHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ChrClassesHandler343(Packet packet, uint entry, params object[] indexes)
        {
            ChrClassesHotfix343 hotfix = new ChrClassesHotfix343();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Filename = packet.ReadCString("Filename", indexes);
            hotfix.NameMale = packet.ReadCString("NameMale", indexes);
            hotfix.NameFemale = packet.ReadCString("NameFemale", indexes);
            hotfix.PetNameToken = packet.ReadCString("PetNameToken", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.CreateScreenFileDataID = packet.ReadUInt32("CreateScreenFileDataID", indexes);
            hotfix.SelectScreenFileDataID = packet.ReadUInt32("SelectScreenFileDataID", indexes);
            hotfix.IconFileDataID = packet.ReadUInt32("IconFileDataID", indexes);
            hotfix.LowResScreenFileDataID = packet.ReadUInt32("LowResScreenFileDataID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.StartingLevel = packet.ReadInt32("StartingLevel", indexes);
            hotfix.ArmorTypeMask = packet.ReadUInt32("ArmorTypeMask", indexes);
            hotfix.CinematicSequenceID = packet.ReadUInt16("CinematicSequenceID", indexes);
            hotfix.DefaultSpec = packet.ReadUInt16("DefaultSpec", indexes);
            hotfix.HasStrengthAttackBonus = packet.ReadByte("HasStrengthAttackBonus", indexes);
            hotfix.PrimaryStatPriority = packet.ReadByte("PrimaryStatPriority", indexes);
            hotfix.DisplayPower = packet.ReadByte("DisplayPower", indexes);
            hotfix.RangedAttackPowerPerAgility = packet.ReadByte("RangedAttackPowerPerAgility", indexes);
            hotfix.AttackPowerPerAgility = packet.ReadByte("AttackPowerPerAgility", indexes);
            hotfix.AttackPowerPerStrength = packet.ReadByte("AttackPowerPerStrength", indexes);
            hotfix.SpellClassSet = packet.ReadByte("SpellClassSet", indexes);
            hotfix.RolesMask = packet.ReadByte("RolesMask", indexes);
            hotfix.DamageBonusStat = packet.ReadByte("DamageBonusStat", indexes);
            hotfix.HasRelicSlot = packet.ReadByte("HasRelicSlot", indexes);

            Storage.ChrClassesHotfixes343.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChrClassesLocaleHotfix343 hotfixLocale = new ChrClassesLocaleHotfix343
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    NameMaleLang = hotfix.NameMale,
                    NameFemaleLang = hotfix.NameFemale,
                };
                Storage.ChrClassesHotfixesLocale343.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ChrClassesXPowerTypesHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ChrClassesXPowerTypesHotfix340 hotfix = new ChrClassesXPowerTypesHotfix340();

            hotfix.ID = entry;
            hotfix.PowerType = packet.ReadSByte("PowerType", indexes);
            hotfix.ClassID = packet.ReadInt32("ClassID", indexes);

            Storage.ChrClassesXPowerTypesHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrCustomizationChoiceHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationChoiceHotfix340 hotfix = new ChrCustomizationChoiceHotfix340();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ChrCustomizationOptionID = packet.ReadInt32("ChrCustomizationOptionID", indexes);
            hotfix.ChrCustomizationReqID = packet.ReadInt32("ChrCustomizationReqID", indexes);
            hotfix.SortOrder = packet.ReadUInt16("SortOrder", indexes);
            hotfix.UiOrderIndex = packet.ReadUInt16("UiOrderIndex", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.AddedInPatch = packet.ReadInt32("AddedInPatch", indexes);
            hotfix.SwatchColor = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.SwatchColor[i] = packet.ReadInt32("SwatchColor", indexes, i);

            Storage.ChrCustomizationChoiceHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChrCustomizationChoiceLocaleHotfix340 hotfixLocale = new ChrCustomizationChoiceLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ChrCustomizationChoiceHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ChrCustomizationChoiceHandler341(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationChoiceHotfix341 hotfix = new ChrCustomizationChoiceHotfix341();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ChrCustomizationOptionID = packet.ReadInt32("ChrCustomizationOptionID", indexes);
            hotfix.ChrCustomizationReqID = packet.ReadInt32("ChrCustomizationReqID", indexes);
            hotfix.ChrCustomizationVisReqID = packet.ReadInt32("ChrCustomizationVisReqID", indexes);
            hotfix.SortOrder = packet.ReadUInt16("SortOrder", indexes);
            hotfix.UiOrderIndex = packet.ReadUInt16("UiOrderIndex", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.AddedInPatch = packet.ReadInt32("AddedInPatch", indexes);
            hotfix.SwatchColor = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.SwatchColor[i] = packet.ReadInt32("SwatchColor", indexes, i);

            Storage.ChrCustomizationChoiceHotfixes341.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChrCustomizationChoiceLocaleHotfix341 hotfixLocale = new ChrCustomizationChoiceLocaleHotfix341
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ChrCustomizationChoiceHotfixesLocale341.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ChrCustomizationChoiceHandler342(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationChoiceHotfix342 hotfix = new ChrCustomizationChoiceHotfix342();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ChrCustomizationOptionID = packet.ReadInt32("ChrCustomizationOptionID", indexes);
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

            Storage.ChrCustomizationChoiceHotfixes342.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChrCustomizationChoiceLocaleHotfix342 hotfixLocale = new ChrCustomizationChoiceLocaleHotfix342
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ChrCustomizationChoiceHotfixesLocale342.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ChrCustomizationDisplayInfoHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationDisplayInfoHotfix340 hotfix = new ChrCustomizationDisplayInfoHotfix340();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ShapeshiftFormID = packet.ReadInt32("ShapeshiftFormID", indexes);
            hotfix.DisplayID = packet.ReadInt32("DisplayID", indexes);
            hotfix.BarberShopMinCameraDistance = packet.ReadSingle("BarberShopMinCameraDistance", indexes);
            hotfix.BarberShopHeightOffset = packet.ReadSingle("BarberShopHeightOffset", indexes);

            Storage.ChrCustomizationDisplayInfoHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrCustomizationElementHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationElementHotfix340 hotfix = new ChrCustomizationElementHotfix340();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ChrCustomizationChoiceID = packet.ReadInt32("ChrCustomizationChoiceID", indexes);
            hotfix.RelatedChrCustomizationChoiceID = packet.ReadInt32("RelatedChrCustomizationChoiceID", indexes);
            hotfix.ChrCustomizationGeosetID = packet.ReadInt32("ChrCustomizationGeosetID", indexes);
            hotfix.ChrCustomizationSkinnedModelID = packet.ReadInt32("ChrCustomizationSkinnedModelID", indexes);
            hotfix.ChrCustomizationMaterialID = packet.ReadInt32("ChrCustomizationMaterialID", indexes);
            hotfix.ChrCustomizationBoneSetID = packet.ReadInt32("ChrCustomizationBoneSetID", indexes);
            hotfix.ChrCustomizationCondModelID = packet.ReadInt32("ChrCustomizationCondModelID", indexes);
            hotfix.ChrCustomizationDisplayInfoID = packet.ReadInt32("ChrCustomizationDisplayInfoID", indexes);
            hotfix.ChrCustItemGeoModifyID = packet.ReadInt32("ChrCustItemGeoModifyID", indexes);

            Storage.ChrCustomizationElementHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrCustomizationElementHandler341(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationElementHotfix341 hotfix = new ChrCustomizationElementHotfix341();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
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

            Storage.ChrCustomizationElementHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrCustomizationElementHandler342(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationElementHotfix342 hotfix = new ChrCustomizationElementHotfix342();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
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

            Storage.ChrCustomizationElementHotfixes342.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrCustomizationElementHandler343(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationElementHotfix343 hotfix = new ChrCustomizationElementHotfix343();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
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

            Storage.ChrCustomizationElementHotfixes343.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrCustomizationOptionHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationOptionHotfix340 hotfix = new ChrCustomizationOptionHotfix340();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.SecondaryID = packet.ReadUInt16("SecondaryID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ChrModelID = packet.ReadInt32("ChrModelID", indexes);
            hotfix.SortIndex = packet.ReadInt32("SortIndex", indexes);
            hotfix.ChrCustomizationCategoryID = packet.ReadInt32("ChrCustomizationCategoryID", indexes);
            hotfix.OptionType = packet.ReadInt32("OptionType", indexes);
            hotfix.BarberShopCostModifier = packet.ReadSingle("BarberShopCostModifier", indexes);
            hotfix.ChrCustomizationID = packet.ReadInt32("ChrCustomizationID", indexes);
            hotfix.ChrCustomizationReqID = packet.ReadInt32("ChrCustomizationReqID", indexes);
            hotfix.UiOrderIndex = packet.ReadInt32("UiOrderIndex", indexes);

            Storage.ChrCustomizationOptionHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChrCustomizationOptionLocaleHotfix340 hotfixLocale = new ChrCustomizationOptionLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ChrCustomizationOptionHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ChrCustomizationReqHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationReqHotfix340 hotfix = new ChrCustomizationReqHotfix340();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ClassMask = packet.ReadInt32("ClassMask", indexes);
            hotfix.AchievementID = packet.ReadInt32("AchievementID", indexes);
            hotfix.OverrideArchive = packet.ReadInt32("OverrideArchive", indexes);
            hotfix.ItemModifiedAppearanceID = packet.ReadInt32("ItemModifiedAppearanceID", indexes);

            Storage.ChrCustomizationReqHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrCustomizationReqHandler341(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationReqHotfix341 hotfix = new ChrCustomizationReqHotfix341();

            hotfix.ReqSource = packet.ReadCString("ReqSource", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ClassMask = packet.ReadInt32("ClassMask", indexes);
            hotfix.AchievementID = packet.ReadInt32("AchievementID", indexes);
            hotfix.QuestID = packet.ReadInt32("QuestID", indexes);
            hotfix.OverrideArchive = packet.ReadInt32("OverrideArchive", indexes);
            hotfix.ItemModifiedAppearanceID = packet.ReadInt32("ItemModifiedAppearanceID", indexes);

            Storage.ChrCustomizationReqHotfixes341.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChrCustomizationReqLocaleHotfix341 hotfixLocale = new ChrCustomizationReqLocaleHotfix341
                {
                    ID = hotfix.ID,
                    ReqSourceLang = hotfix.ReqSource,
                };
                Storage.ChrCustomizationReqHotfixesLocale341.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ChrCustomizationReqHandler343(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationReqHotfix343 hotfix = new ChrCustomizationReqHotfix343();

            hotfix.RaceMask = packet.ReadInt64("RaceMask", indexes);
            hotfix.ReqSource = packet.ReadCString("ReqSource", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ClassMask = packet.ReadInt32("ClassMask", indexes);
            hotfix.AchievementID = packet.ReadInt32("AchievementID", indexes);
            hotfix.QuestID = packet.ReadInt32("QuestID", indexes);
            hotfix.OverrideArchive = packet.ReadInt32("OverrideArchive", indexes);
            hotfix.ItemModifiedAppearanceID = packet.ReadInt32("ItemModifiedAppearanceID", indexes);

            Storage.ChrCustomizationReqHotfixes343.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChrCustomizationReqLocaleHotfix343 hotfixLocale = new ChrCustomizationReqLocaleHotfix343
                {
                    ID = hotfix.ID,
                    ReqSourceLang = hotfix.ReqSource,
                };
                Storage.ChrCustomizationReqHotfixesLocale343.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ChrCustomizationReqChoiceHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationReqChoiceHotfix340 hotfix = new ChrCustomizationReqChoiceHotfix340();

            hotfix.ID = entry;
            hotfix.ChrCustomizationChoiceID = packet.ReadInt32("ChrCustomizationChoiceID", indexes);
            hotfix.ChrCustomizationReqID = packet.ReadInt32("ChrCustomizationReqID", indexes);

            Storage.ChrCustomizationReqChoiceHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrModelHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ChrModelHotfix340 hotfix = new ChrModelHotfix340();

            hotfix.FaceCustomizationOffset = new float?[3];
            for (int i = 0; i < 3; i++)
                hotfix.FaceCustomizationOffset[i] = packet.ReadSingle("FaceCustomizationOffset", indexes, i);
            hotfix.CustomizeOffset = new float?[3];
            for (int i = 0; i < 3; i++)
                hotfix.CustomizeOffset[i] = packet.ReadSingle("CustomizeOffset", indexes, i);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Sex = packet.ReadSByte("Sex", indexes);
            hotfix.DisplayID = packet.ReadInt32("DisplayID", indexes);
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

            Storage.ChrModelHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrModelHandler341(Packet packet, uint entry, params object[] indexes)
        {
            ChrModelHotfix341 hotfix = new ChrModelHotfix341();

            hotfix.FaceCustomizationOffset = new float?[3];
            for (int i = 0; i < 3; i++)
                hotfix.FaceCustomizationOffset[i] = packet.ReadSingle("FaceCustomizationOffset", indexes, i);
            hotfix.CustomizeOffset = new float?[3];
            for (int i = 0; i < 3; i++)
                hotfix.CustomizeOffset[i] = packet.ReadSingle("CustomizeOffset", indexes, i);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Sex = packet.ReadSByte("Sex", indexes);
            hotfix.DisplayID = packet.ReadInt32("DisplayID", indexes);
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

            Storage.ChrModelHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrRaceXChrModelHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ChrRaceXChrModelHotfix340 hotfix = new ChrRaceXChrModelHotfix340();

            hotfix.ID = entry;
            hotfix.ChrRacesID = packet.ReadInt32("ChrRacesID", indexes);
            hotfix.ChrModelID = packet.ReadInt32("ChrModelID", indexes);

            Storage.ChrRaceXChrModelHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrRaceXChrModelHandler341(Packet packet, uint entry, params object[] indexes)
        {
            ChrRaceXChrModelHotfix341 hotfix = new ChrRaceXChrModelHotfix341();

            hotfix.ID = entry;
            hotfix.ChrRacesID = packet.ReadInt32("ChrRacesID", indexes);
            hotfix.ChrModelID = packet.ReadInt32("ChrModelID", indexes);
            hotfix.Sex = packet.ReadInt32("Sex", indexes);
            hotfix.AllowedTransmogSlots = packet.ReadInt32("AllowedTransmogSlots", indexes);

            Storage.ChrRaceXChrModelHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrRacesHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ChrRacesHotfix340 hotfix = new ChrRacesHotfix340();

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
            hotfix.MaleDisplayID = packet.ReadUInt32("MaleDisplayID", indexes);
            hotfix.FemaleDisplayID = packet.ReadUInt32("FemaleDisplayID", indexes);
            hotfix.HighResMaleDisplayID = packet.ReadUInt32("HighResMaleDisplayID", indexes);
            hotfix.HighResFemaleDisplayID = packet.ReadUInt32("HighResFemaleDisplayID", indexes);
            hotfix.ResSicknessSpellID = packet.ReadInt32("ResSicknessSpellID", indexes);
            hotfix.SplashSoundID = packet.ReadInt32("SplashSoundID", indexes);
            hotfix.CreateScreenFileDataID = packet.ReadInt32("CreateScreenFileDataID", indexes);
            hotfix.SelectScreenFileDataID = packet.ReadInt32("SelectScreenFileDataID", indexes);
            hotfix.LowResScreenFileDataID = packet.ReadInt32("LowResScreenFileDataID", indexes);
            hotfix.AlteredFormStartVisualKitID = new uint?[3];
            for (int i = 0; i < 3; i++)
                hotfix.AlteredFormStartVisualKitID[i] = packet.ReadUInt32("AlteredFormStartVisualKitID", indexes, i);
            hotfix.AlteredFormFinishVisualKitID = new uint?[3];
            for (int i = 0; i < 3; i++)
                hotfix.AlteredFormFinishVisualKitID[i] = packet.ReadUInt32("AlteredFormFinishVisualKitID", indexes, i);
            hotfix.HeritageArmorAchievementID = packet.ReadInt32("HeritageArmorAchievementID", indexes);
            hotfix.StartingLevel = packet.ReadInt32("StartingLevel", indexes);
            hotfix.UiDisplayOrder = packet.ReadInt32("UiDisplayOrder", indexes);
            hotfix.PlayableRaceBit = packet.ReadInt32("PlayableRaceBit", indexes);
            hotfix.FemaleSkeletonFileDataID = packet.ReadInt32("FemaleSkeletonFileDataID", indexes);
            hotfix.MaleSkeletonFileDataID = packet.ReadInt32("MaleSkeletonFileDataID", indexes);
            hotfix.HelmetAnimScalingRaceID = packet.ReadInt32("HelmetAnimScalingRaceID", indexes);
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
            hotfix.FactionID = packet.ReadInt16("FactionID", indexes);
            hotfix.CinematicSequenceID = packet.ReadInt16("CinematicSequenceID", indexes);
            hotfix.BaseLanguage = packet.ReadSByte("BaseLanguage", indexes);
            hotfix.CreatureType = packet.ReadSByte("CreatureType", indexes);
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
            hotfix.UnalteredVisualCustomizationRaceID = packet.ReadSByte("UnalteredVisualCustomizationRaceID", indexes);

            Storage.ChrRacesHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChrRacesLocaleHotfix340 hotfixLocale = new ChrRacesLocaleHotfix340
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
                Storage.ChrRacesHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ChrSpecializationHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ChrSpecializationHotfix340 hotfix = new ChrSpecializationHotfix340();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.FemaleName = packet.ReadCString("FemaleName", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ClassID = packet.ReadSByte("ClassID", indexes);
            hotfix.OrderIndex = packet.ReadSByte("OrderIndex", indexes);
            hotfix.PetTalentType = packet.ReadSByte("PetTalentType", indexes);
            hotfix.Role = packet.ReadSByte("Role", indexes);
            hotfix.Flags = packet.ReadUInt32("Flags", indexes);
            hotfix.SpellIconFileID = packet.ReadInt32("SpellIconFileID", indexes);
            hotfix.PrimaryStatPriority = packet.ReadSByte("PrimaryStatPriority", indexes);
            hotfix.AnimReplacements = packet.ReadInt32("AnimReplacements", indexes);
            hotfix.MasterySpellID = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.MasterySpellID[i] = packet.ReadInt32("MasterySpellID", indexes, i);

            Storage.ChrSpecializationHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChrSpecializationLocaleHotfix340 hotfixLocale = new ChrSpecializationLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    FemaleNameLang = hotfix.FemaleName,
                    DescriptionLang = hotfix.Description,
                };
                Storage.ChrSpecializationHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CinematicCameraHandler340(Packet packet, uint entry, params object[] indexes)
        {
            CinematicCameraHotfix340 hotfix = new CinematicCameraHotfix340();

            hotfix.ID = entry;
            hotfix.OriginX = packet.ReadSingle("OriginX", indexes);
            hotfix.OriginY = packet.ReadSingle("OriginY", indexes);
            hotfix.OriginZ = packet.ReadSingle("OriginZ", indexes);
            hotfix.SoundID = packet.ReadUInt32("SoundID", indexes);
            hotfix.OriginFacing = packet.ReadSingle("OriginFacing", indexes);
            hotfix.FileDataID = packet.ReadUInt32("FileDataID", indexes);

            Storage.CinematicCameraHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void CinematicSequencesHandler340(Packet packet, uint entry, params object[] indexes)
        {
            CinematicSequencesHotfix340 hotfix = new CinematicSequencesHotfix340();

            hotfix.ID = entry;
            hotfix.SoundID = packet.ReadUInt32("SoundID", indexes);
            hotfix.Camera = new ushort?[8];
            for (int i = 0; i < 8; i++)
                hotfix.Camera[i] = packet.ReadUInt16("Camera", indexes, i);

            Storage.CinematicSequencesHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ConditionalChrModelHandler343(Packet packet, uint entry, params object[] indexes)
        {
            ConditionalChrModelHotfix343 hotfix = new ConditionalChrModelHotfix343();

            hotfix.ID = packet.ReadInt32("ID", indexes);
            hotfix.ChrModelID = packet.ReadUInt32("ChrModelID", indexes);
            hotfix.ChrCustomizationReqID = packet.ReadInt32("ChrCustomizationReqID", indexes);
            hotfix.PlayerConditionID = packet.ReadInt32("PlayerConditionID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ChrCustomizationCategoryID = packet.ReadInt32("ChrCustomizationCategoryID", indexes);

            Storage.ConditionalChrModelHotfixes343.Add(hotfix, packet.TimeSpan);
        }

        public static void ConditionalContentTuningHandler343(Packet packet, uint entry, params object[] indexes)
        {
            ConditionalContentTuningHotfix343 hotfix = new ConditionalContentTuningHotfix343();

            hotfix.ID = entry;
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.RedirectContentTuningID = packet.ReadInt32("RedirectContentTuningID", indexes);
            hotfix.RedirectFlag = packet.ReadInt32("RedirectFlag", indexes);
            hotfix.ParentContentTuningID = packet.ReadInt32("ParentContentTuningID", indexes);

            Storage.ConditionalContentTuningHotfixes343.Add(hotfix, packet.TimeSpan);
        }

        public static void ContentTuningHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ContentTuningHotfix340 hotfix = new ContentTuningHotfix340();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.MinLevel = packet.ReadInt32("MinLevel", indexes);
            hotfix.MaxLevel = packet.ReadInt32("MaxLevel", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ExpectedStatModID = packet.ReadInt32("ExpectedStatModID", indexes);
            hotfix.DifficultyESMID = packet.ReadInt32("DifficultyESMID", indexes);

            Storage.ContentTuningHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ConversationLineHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ConversationLineHotfix340 hotfix = new ConversationLineHotfix340();

            hotfix.ID = entry;
            hotfix.BroadcastTextID = packet.ReadUInt32("BroadcastTextID", indexes);
            hotfix.SpellVisualKitID = packet.ReadUInt32("SpellVisualKitID", indexes);
            hotfix.AdditionalDuration = packet.ReadInt32("AdditionalDuration", indexes);
            hotfix.NextConversationLineID = packet.ReadUInt16("NextConversationLineID", indexes);
            hotfix.AnimKitID = packet.ReadUInt16("AnimKitID", indexes);
            hotfix.SpeechType = packet.ReadByte("SpeechType", indexes);
            hotfix.StartAnimation = packet.ReadByte("StartAnimation", indexes);
            hotfix.EndAnimation = packet.ReadByte("EndAnimation", indexes);

            Storage.ConversationLineHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void CreatureDisplayInfoHandler340(Packet packet, uint entry, params object[] indexes)
        {
            CreatureDisplayInfoHotfix340 hotfix = new CreatureDisplayInfoHotfix340();

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
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.StateSpellVisualKitID = packet.ReadInt32("StateSpellVisualKitID", indexes);
            hotfix.PlayerOverrideScale = packet.ReadSingle("PlayerOverrideScale", indexes);
            hotfix.PetInstanceScale = packet.ReadSingle("PetInstanceScale", indexes);
            hotfix.UnarmedWeaponType = packet.ReadSByte("UnarmedWeaponType", indexes);
            hotfix.MountPoofSpellVisualKitID = packet.ReadInt32("MountPoofSpellVisualKitID", indexes);
            hotfix.DissolveEffectID = packet.ReadInt32("DissolveEffectID", indexes);
            hotfix.Gender = packet.ReadSByte("Gender", indexes);
            hotfix.DissolveOutEffectID = packet.ReadInt32("DissolveOutEffectID", indexes);
            hotfix.CreatureModelMinLod = packet.ReadSByte("CreatureModelMinLod", indexes);
            hotfix.TextureVariationFileDataID = new int?[3];
            for (int i = 0; i < 3; i++)
                hotfix.TextureVariationFileDataID[i] = packet.ReadInt32("TextureVariationFileDataID", indexes, i);

            Storage.CreatureDisplayInfoHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void CreatureDisplayInfoHandler341(Packet packet, uint entry, params object[] indexes)
        {
            CreatureDisplayInfoHotfix341 hotfix = new CreatureDisplayInfoHotfix341();

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
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.StateSpellVisualKitID = packet.ReadInt32("StateSpellVisualKitID", indexes);
            hotfix.PlayerOverrideScale = packet.ReadSingle("PlayerOverrideScale", indexes);
            hotfix.PetInstanceScale = packet.ReadSingle("PetInstanceScale", indexes);
            hotfix.UnarmedWeaponType = packet.ReadSByte("UnarmedWeaponType", indexes);
            hotfix.MountPoofSpellVisualKitID = packet.ReadInt32("MountPoofSpellVisualKitID", indexes);
            hotfix.DissolveEffectID = packet.ReadInt32("DissolveEffectID", indexes);
            hotfix.Gender = packet.ReadSByte("Gender", indexes);
            hotfix.DissolveOutEffectID = packet.ReadInt32("DissolveOutEffectID", indexes);
            hotfix.CreatureModelMinLod = packet.ReadSByte("CreatureModelMinLod", indexes);
            hotfix.TextureVariationFileDataID = new int?[4];
            for (int i = 0; i < 4; i++)
                hotfix.TextureVariationFileDataID[i] = packet.ReadInt32("TextureVariationFileDataID", indexes, i);

            Storage.CreatureDisplayInfoHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void CreatureDisplayInfoExtraHandler340(Packet packet, uint entry, params object[] indexes)
        {
            CreatureDisplayInfoExtraHotfix340 hotfix = new CreatureDisplayInfoExtraHotfix340();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.DisplayRaceID = packet.ReadSByte("DisplayRaceID", indexes);
            hotfix.DisplaySexID = packet.ReadSByte("DisplaySexID", indexes);
            hotfix.DisplayClassID = packet.ReadSByte("DisplayClassID", indexes);
            hotfix.SkinID = packet.ReadSByte("SkinID", indexes);
            hotfix.FaceID = packet.ReadSByte("FaceID", indexes);
            hotfix.HairStyleID = packet.ReadSByte("HairStyleID", indexes);
            hotfix.HairColorID = packet.ReadSByte("HairColorID", indexes);
            hotfix.FacialHairID = packet.ReadSByte("FacialHairID", indexes);
            hotfix.Flags = packet.ReadSByte("Flags", indexes);
            hotfix.BakeMaterialResourcesID = packet.ReadInt32("BakeMaterialResourcesID", indexes);
            hotfix.HDBakeMaterialResourcesID = packet.ReadInt32("HDBakeMaterialResourcesID", indexes);
            hotfix.CustomDisplayOption = new byte?[3];
            for (int i = 0; i < 3; i++)
                hotfix.CustomDisplayOption[i] = packet.ReadByte("CustomDisplayOption", indexes, i);

            Storage.CreatureDisplayInfoExtraHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void CreatureFamilyHandler340(Packet packet, uint entry, params object[] indexes)
        {
            CreatureFamilyHotfix340 hotfix = new CreatureFamilyHotfix340();

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

            Storage.CreatureFamilyHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CreatureFamilyLocaleHotfix340 hotfixLocale = new CreatureFamilyLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.CreatureFamilyHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CreatureModelDataHandler340(Packet packet, uint entry, params object[] indexes)
        {
            CreatureModelDataHotfix340 hotfix = new CreatureModelDataHotfix340();

            hotfix.ID = entry;
            hotfix.GeoBox = new float?[6];
            for (int i = 0; i < 6; i++)
                hotfix.GeoBox[i] = packet.ReadSingle("GeoBox", indexes, i);
            hotfix.Flags = packet.ReadUInt32("Flags", indexes);
            hotfix.FileDataID = packet.ReadUInt32("FileDataID", indexes);
            hotfix.BloodID = packet.ReadUInt32("BloodID", indexes);
            hotfix.FootprintTextureID = packet.ReadUInt32("FootprintTextureID", indexes);
            hotfix.FootprintTextureLength = packet.ReadSingle("FootprintTextureLength", indexes);
            hotfix.FootprintTextureWidth = packet.ReadSingle("FootprintTextureWidth", indexes);
            hotfix.FootprintParticleScale = packet.ReadSingle("FootprintParticleScale", indexes);
            hotfix.FoleyMaterialID = packet.ReadUInt32("FoleyMaterialID", indexes);
            hotfix.FootstepCameraEffectID = packet.ReadUInt32("FootstepCameraEffectID", indexes);
            hotfix.DeathThudCameraEffectID = packet.ReadUInt32("DeathThudCameraEffectID", indexes);
            hotfix.SoundID = packet.ReadUInt32("SoundID", indexes);
            hotfix.SizeClass = packet.ReadUInt32("SizeClass", indexes);
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

            Storage.CreatureModelDataHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void CreatureTypeHandler340(Packet packet, uint entry, params object[] indexes)
        {
            CreatureTypeHotfix340 hotfix = new CreatureTypeHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);

            Storage.CreatureTypeHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CreatureTypeLocaleHotfix340 hotfixLocale = new CreatureTypeLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.CreatureTypeHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CriteriaHandler340(Packet packet, uint entry, params object[] indexes)
        {
            CriteriaHotfix340 hotfix = new CriteriaHotfix340();

            hotfix.ID = entry;
            hotfix.Type = packet.ReadInt16("Type", indexes);
            hotfix.Asset = packet.ReadInt32("Asset", indexes);
            hotfix.ModifierTreeID = packet.ReadUInt32("ModifierTreeID", indexes);
            hotfix.StartEvent = packet.ReadByte("StartEvent", indexes);
            hotfix.StartAsset = packet.ReadInt32("StartAsset", indexes);
            hotfix.StartTimer = packet.ReadUInt16("StartTimer", indexes);
            hotfix.FailEvent = packet.ReadByte("FailEvent", indexes);
            hotfix.FailAsset = packet.ReadInt32("FailAsset", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.EligibilityWorldStateID = packet.ReadInt16("EligibilityWorldStateID", indexes);
            hotfix.EligibilityWorldStateValue = packet.ReadSByte("EligibilityWorldStateValue", indexes);

            Storage.CriteriaHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void CriteriaHandler343(Packet packet, uint entry, params object[] indexes)
        {
            CriteriaHotfix343 hotfix = new CriteriaHotfix343();

            hotfix.ID = entry;
            hotfix.Type = packet.ReadInt16("Type", indexes);
            hotfix.Asset = packet.ReadInt32("Asset", indexes);
            hotfix.ModifierTreeID = packet.ReadUInt32("ModifierTreeID", indexes);
            hotfix.StartEvent = packet.ReadInt32("StartEvent", indexes);
            hotfix.StartAsset = packet.ReadInt32("StartAsset", indexes);
            hotfix.StartTimer = packet.ReadUInt16("StartTimer", indexes);
            hotfix.FailEvent = packet.ReadInt32("FailEvent", indexes);
            hotfix.FailAsset = packet.ReadInt32("FailAsset", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.EligibilityWorldStateID = packet.ReadInt16("EligibilityWorldStateID", indexes);
            hotfix.EligibilityWorldStateValue = packet.ReadSByte("EligibilityWorldStateValue", indexes);

            Storage.CriteriaHotfixes343.Add(hotfix, packet.TimeSpan);
        }

        public static void CriteriaTreeHandler340(Packet packet, uint entry, params object[] indexes)
        {
            CriteriaTreeHotfix340 hotfix = new CriteriaTreeHotfix340();

            hotfix.ID = entry;
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.Parent = packet.ReadUInt32("Parent", indexes);
            hotfix.Amount = packet.ReadUInt32("Amount", indexes);
            hotfix.Operator = packet.ReadSByte("Operator", indexes);
            hotfix.CriteriaID = packet.ReadUInt32("CriteriaID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.Flags = packet.ReadInt16("Flags", indexes);

            Storage.CriteriaTreeHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CriteriaTreeLocaleHotfix340 hotfixLocale = new CriteriaTreeLocaleHotfix340
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                };
                Storage.CriteriaTreeHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CriteriaTreeHandler343(Packet packet, uint entry, params object[] indexes)
        {
            CriteriaTreeHotfix343 hotfix = new CriteriaTreeHotfix343();

            hotfix.ID = entry;
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.Parent = packet.ReadUInt32("Parent", indexes);
            hotfix.Amount = packet.ReadUInt32("Amount", indexes);
            hotfix.Operator = packet.ReadInt32("Operator", indexes);
            hotfix.CriteriaID = packet.ReadUInt32("CriteriaID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.CriteriaTreeHotfixes343.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CriteriaTreeLocaleHotfix343 hotfixLocale = new CriteriaTreeLocaleHotfix343
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                };
                Storage.CriteriaTreeHotfixesLocale343.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CurrencyContainerHandler340(Packet packet, uint entry, params object[] indexes)
        {
            CurrencyContainerHotfix340 hotfix = new CurrencyContainerHotfix340();

            hotfix.ID = entry;
            hotfix.ContainerName = packet.ReadCString("ContainerName", indexes);
            hotfix.ContainerDescription = packet.ReadCString("ContainerDescription", indexes);
            hotfix.MinAmount = packet.ReadInt32("MinAmount", indexes);
            hotfix.MaxAmount = packet.ReadInt32("MaxAmount", indexes);
            hotfix.ContainerIconID = packet.ReadInt32("ContainerIconID", indexes);
            hotfix.ContainerQuality = packet.ReadInt32("ContainerQuality", indexes);
            hotfix.OnLootSpellVisualKitID = packet.ReadInt32("OnLootSpellVisualKitID", indexes);
            hotfix.CurrencyTypesID = packet.ReadInt32("CurrencyTypesID", indexes);

            Storage.CurrencyContainerHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CurrencyContainerLocaleHotfix340 hotfixLocale = new CurrencyContainerLocaleHotfix340
                {
                    ID = hotfix.ID,
                    ContainerNameLang = hotfix.ContainerName,
                    ContainerDescriptionLang = hotfix.ContainerDescription,
                };
                Storage.CurrencyContainerHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CurrencyTypesHandler340(Packet packet, uint entry, params object[] indexes)
        {
            CurrencyTypesHotfix340 hotfix = new CurrencyTypesHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.CategoryID = packet.ReadByte("CategoryID", indexes);
            hotfix.InventoryIconFileID = packet.ReadInt32("InventoryIconFileID", indexes);
            hotfix.SpellWeight = packet.ReadUInt32("SpellWeight", indexes);
            hotfix.SpellCategory = packet.ReadByte("SpellCategory", indexes);
            hotfix.MaxQty = packet.ReadUInt32("MaxQty", indexes);
            hotfix.MaxEarnablePerWeek = packet.ReadUInt32("MaxEarnablePerWeek", indexes);
            hotfix.Quality = packet.ReadSByte("Quality", indexes);
            hotfix.FactionID = packet.ReadInt32("FactionID", indexes);
            hotfix.Flags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.CurrencyTypesHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CurrencyTypesLocaleHotfix340 hotfixLocale = new CurrencyTypesLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.CurrencyTypesHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CurrencyTypesHandler343(Packet packet, uint entry, params object[] indexes)
        {
            CurrencyTypesHotfix343 hotfix = new CurrencyTypesHotfix343();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.CategoryID = packet.ReadByte("CategoryID", indexes);
            hotfix.InventoryIconFileID = packet.ReadInt32("InventoryIconFileID", indexes);
            hotfix.SpellWeight = packet.ReadUInt32("SpellWeight", indexes);
            hotfix.SpellCategory = packet.ReadByte("SpellCategory", indexes);
            hotfix.MaxQty = packet.ReadUInt32("MaxQty", indexes);
            hotfix.MaxEarnablePerWeek = packet.ReadUInt32("MaxEarnablePerWeek", indexes);
            hotfix.Quality = packet.ReadSByte("Quality", indexes);
            hotfix.FactionID = packet.ReadInt32("FactionID", indexes);
            hotfix.AwardConditionID = packet.ReadInt32("AwardConditionID", indexes);
            hotfix.Flags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.CurrencyTypesHotfixes343.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CurrencyTypesLocaleHotfix343 hotfixLocale = new CurrencyTypesLocaleHotfix343
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.CurrencyTypesHotfixesLocale343.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CurveHandler340(Packet packet, uint entry, params object[] indexes)
        {
            CurveHotfix340 hotfix = new CurveHotfix340();

            hotfix.ID = entry;
            hotfix.Type = packet.ReadByte("Type", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);

            Storage.CurveHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void CurveHandler341(Packet packet, uint entry, params object[] indexes)
        {
            CurveHotfix341 hotfix = new CurveHotfix341();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Type = packet.ReadByte("Type", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);

            Storage.CurveHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void CurvePointHandler340(Packet packet, uint entry, params object[] indexes)
        {
            CurvePointHotfix340 hotfix = new CurvePointHotfix340();

            hotfix.ID = entry;
            hotfix.PosX = packet.ReadSingle("PosX", indexes);
            hotfix.PosY = packet.ReadSingle("PosY", indexes);
            hotfix.CurveID = packet.ReadUInt16("CurveID", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);

            Storage.CurvePointHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void CurvePointHandler341(Packet packet, uint entry, params object[] indexes)
        {
            CurvePointHotfix341 hotfix = new CurvePointHotfix341();

            hotfix.PosX = packet.ReadSingle("PosX", indexes);
            hotfix.PosY = packet.ReadSingle("PosY", indexes);
            hotfix.PreSLSquishPosX = packet.ReadSingle("PreSLSquishPosX", indexes);
            hotfix.PreSLSquishPosY = packet.ReadSingle("PreSLSquishPosY", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.CurveID = packet.ReadUInt16("CurveID", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);

            Storage.CurvePointHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void CurvePointHandler342(Packet packet, uint entry, params object[] indexes)
        {
            CurvePointHotfix342 hotfix = new CurvePointHotfix342();

            hotfix.PosX = packet.ReadSingle("PosX", indexes);
            hotfix.PosY = packet.ReadSingle("PosY", indexes);
            hotfix.PosPreSquishX = packet.ReadSingle("PosPreSquishX", indexes);
            hotfix.PosPreSquishY = packet.ReadSingle("PosPreSquishY", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.CurveID = packet.ReadInt32("CurveID", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);

            Storage.CurvePointHotfixes342.Add(hotfix, packet.TimeSpan);
        }

        public static void DestructibleModelDataHandler340(Packet packet, uint entry, params object[] indexes)
        {
            DestructibleModelDataHotfix340 hotfix = new DestructibleModelDataHotfix340();

            hotfix.ID = entry;
            hotfix.State0ImpactEffectDoodadSet = packet.ReadSByte("State0ImpactEffectDoodadSet", indexes);
            hotfix.State0AmbientDoodadSet = packet.ReadByte("State0AmbientDoodadSet", indexes);
            hotfix.State1Wmo = packet.ReadUInt32("State1Wmo", indexes);
            hotfix.State1DestructionDoodadSet = packet.ReadSByte("State1DestructionDoodadSet", indexes);
            hotfix.State1ImpactEffectDoodadSet = packet.ReadSByte("State1ImpactEffectDoodadSet", indexes);
            hotfix.State1AmbientDoodadSet = packet.ReadByte("State1AmbientDoodadSet", indexes);
            hotfix.State2Wmo = packet.ReadUInt32("State2Wmo", indexes);
            hotfix.State2DestructionDoodadSet = packet.ReadSByte("State2DestructionDoodadSet", indexes);
            hotfix.State2ImpactEffectDoodadSet = packet.ReadSByte("State2ImpactEffectDoodadSet", indexes);
            hotfix.State2AmbientDoodadSet = packet.ReadByte("State2AmbientDoodadSet", indexes);
            hotfix.State3Wmo = packet.ReadUInt32("State3Wmo", indexes);
            hotfix.State3InitDoodadSet = packet.ReadByte("State3InitDoodadSet", indexes);
            hotfix.State3AmbientDoodadSet = packet.ReadByte("State3AmbientDoodadSet", indexes);
            hotfix.EjectDirection = packet.ReadByte("EjectDirection", indexes);
            hotfix.DoNotHighlight = packet.ReadByte("DoNotHighlight", indexes);
            hotfix.State0Wmo = packet.ReadUInt32("State0Wmo", indexes);
            hotfix.HealEffect = packet.ReadByte("HealEffect", indexes);
            hotfix.HealEffectSpeed = packet.ReadUInt16("HealEffectSpeed", indexes);
            hotfix.State0NameSet = packet.ReadSByte("State0NameSet", indexes);
            hotfix.State1NameSet = packet.ReadSByte("State1NameSet", indexes);
            hotfix.State2NameSet = packet.ReadSByte("State2NameSet", indexes);
            hotfix.State3NameSet = packet.ReadSByte("State3NameSet", indexes);

            Storage.DestructibleModelDataHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void DifficultyHandler340(Packet packet, uint entry, params object[] indexes)
        {
            DifficultyHotfix340 hotfix = new DifficultyHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.InstanceType = packet.ReadByte("InstanceType", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);
            hotfix.OldEnumValue = packet.ReadSByte("OldEnumValue", indexes);
            hotfix.FallbackDifficultyID = packet.ReadByte("FallbackDifficultyID", indexes);
            hotfix.MinPlayers = packet.ReadByte("MinPlayers", indexes);
            hotfix.MaxPlayers = packet.ReadByte("MaxPlayers", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.ItemContext = packet.ReadByte("ItemContext", indexes);
            hotfix.ToggleDifficultyID = packet.ReadByte("ToggleDifficultyID", indexes);
            hotfix.GroupSizeHealthCurveID = packet.ReadUInt16("GroupSizeHealthCurveID", indexes);
            hotfix.GroupSizeDmgCurveID = packet.ReadUInt16("GroupSizeDmgCurveID", indexes);
            hotfix.GroupSizeSpellPointsCurveID = packet.ReadUInt16("GroupSizeSpellPointsCurveID", indexes);

            Storage.DifficultyHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                DifficultyLocaleHotfix340 hotfixLocale = new DifficultyLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.DifficultyHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void DungeonEncounterHandler340(Packet packet, uint entry, params object[] indexes)
        {
            DungeonEncounterHotfix340 hotfix = new DungeonEncounterHotfix340();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.MapID = packet.ReadInt16("MapID", indexes);
            hotfix.DifficultyID = packet.ReadInt32("DifficultyID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.Bit = packet.ReadSByte("Bit", indexes);
            hotfix.CreatureDisplayID = packet.ReadInt32("CreatureDisplayID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.DungeonEncounterHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                DungeonEncounterLocaleHotfix340 hotfixLocale = new DungeonEncounterLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.DungeonEncounterHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void DungeonEncounterHandler341(Packet packet, uint entry, params object[] indexes)
        {
            DungeonEncounterHotfix341 hotfix = new DungeonEncounterHotfix341();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.MapID = packet.ReadInt16("MapID", indexes);
            hotfix.DifficultyID = packet.ReadInt32("DifficultyID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.Bit = packet.ReadSByte("Bit", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.DungeonEncounterHotfixes341.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                DungeonEncounterLocaleHotfix341 hotfixLocale = new DungeonEncounterLocaleHotfix341
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.DungeonEncounterHotfixesLocale341.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void DungeonEncounterHandler343(Packet packet, uint entry, params object[] indexes)
        {
            DungeonEncounterHotfix343 hotfix = new DungeonEncounterHotfix343();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.MapID = packet.ReadInt16("MapID", indexes);
            hotfix.DifficultyID = packet.ReadInt32("DifficultyID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.Bit = packet.ReadSByte("Bit", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.Faction = packet.ReadInt32("Faction", indexes);

            Storage.DungeonEncounterHotfixes343.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                DungeonEncounterLocaleHotfix343 hotfixLocale = new DungeonEncounterLocaleHotfix343
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.DungeonEncounterHotfixesLocale343.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void DurabilityCostsHandler340(Packet packet, uint entry, params object[] indexes)
        {
            DurabilityCostsHotfix340 hotfix = new DurabilityCostsHotfix340();

            hotfix.ID = entry;
            hotfix.WeaponSubClassCost = new ushort?[21];
            for (int i = 0; i < 21; i++)
                hotfix.WeaponSubClassCost[i] = packet.ReadUInt16("WeaponSubClassCost", indexes, i);
            hotfix.ArmorSubClassCost = new ushort?[8];
            for (int i = 0; i < 8; i++)
                hotfix.ArmorSubClassCost[i] = packet.ReadUInt16("ArmorSubClassCost", indexes, i);

            Storage.DurabilityCostsHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void DurabilityQualityHandler340(Packet packet, uint entry, params object[] indexes)
        {
            DurabilityQualityHotfix340 hotfix = new DurabilityQualityHotfix340();

            hotfix.ID = entry;
            hotfix.Data = packet.ReadSingle("Data", indexes);

            Storage.DurabilityQualityHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void EmotesHandler340(Packet packet, uint entry, params object[] indexes)
        {
            EmotesHotfix340 hotfix = new EmotesHotfix340();

            hotfix.ID = entry;
            hotfix.RaceMask = packet.ReadInt64("RaceMask", indexes);
            hotfix.EmoteSlashCommand = packet.ReadCString("EmoteSlashCommand", indexes);
            hotfix.AnimID = packet.ReadInt32("AnimID", indexes);
            hotfix.EmoteFlags = packet.ReadUInt32("EmoteFlags", indexes);
            hotfix.EmoteSpecProc = packet.ReadByte("EmoteSpecProc", indexes);
            hotfix.EmoteSpecProcParam = packet.ReadUInt32("EmoteSpecProcParam", indexes);
            hotfix.EventSoundID = packet.ReadUInt32("EventSoundID", indexes);
            hotfix.SpellVisualKitID = packet.ReadUInt32("SpellVisualKitID", indexes);
            hotfix.ClassMask = packet.ReadInt32("ClassMask", indexes);

            Storage.EmotesHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void EmotesTextHandler340(Packet packet, uint entry, params object[] indexes)
        {
            EmotesTextHotfix340 hotfix = new EmotesTextHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.EmoteID = packet.ReadUInt16("EmoteID", indexes);

            Storage.EmotesTextHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void EmotesTextSoundHandler340(Packet packet, uint entry, params object[] indexes)
        {
            EmotesTextSoundHotfix340 hotfix = new EmotesTextSoundHotfix340();

            hotfix.ID = entry;
            hotfix.RaceID = packet.ReadByte("RaceID", indexes);
            hotfix.ClassID = packet.ReadByte("ClassID", indexes);
            hotfix.SexID = packet.ReadByte("SexID", indexes);
            hotfix.SoundID = packet.ReadUInt32("SoundID", indexes);
            hotfix.EmotesTextID = packet.ReadInt32("EmotesTextID", indexes);

            Storage.EmotesTextSoundHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ExpectedStatHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ExpectedStatHotfix340 hotfix = new ExpectedStatHotfix340();

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
            hotfix.Lvl = packet.ReadInt32("Lvl", indexes);

            Storage.ExpectedStatHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ExpectedStatModHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ExpectedStatModHotfix340 hotfix = new ExpectedStatModHotfix340();

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

            Storage.ExpectedStatModHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void FactionHandler340(Packet packet, uint entry, params object[] indexes)
        {
            FactionHotfix340 hotfix = new FactionHotfix340();

            hotfix.ReputationRaceMask = new long?[4];
            for (int i = 0; i < 4; i++)
                hotfix.ReputationRaceMask[i] = packet.ReadInt64("ReputationRaceMask", indexes, i);
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ReputationIndex = packet.ReadInt16("ReputationIndex", indexes);
            hotfix.ParentFactionID = packet.ReadUInt16("ParentFactionID", indexes);
            hotfix.Expansion = packet.ReadByte("Expansion", indexes);
            hotfix.FriendshipRepID = packet.ReadByte("FriendshipRepID", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.ParagonFactionID = packet.ReadUInt16("ParagonFactionID", indexes);
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

            Storage.FactionHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                FactionLocaleHotfix340 hotfixLocale = new FactionLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.FactionHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void FactionHandler341(Packet packet, uint entry, params object[] indexes)
        {
            FactionHotfix341 hotfix = new FactionHotfix341();

            hotfix.ReputationRaceMask = new long?[4];
            for (int i = 0; i < 4; i++)
                hotfix.ReputationRaceMask[i] = packet.ReadInt64("ReputationRaceMask", indexes, i);
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ReputationIndex = packet.ReadInt16("ReputationIndex", indexes);
            hotfix.ParentFactionID = packet.ReadUInt16("ParentFactionID", indexes);
            hotfix.Expansion = packet.ReadByte("Expansion", indexes);
            hotfix.FriendshipRepID = packet.ReadByte("FriendshipRepID", indexes);
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

            Storage.FactionHotfixes341.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                FactionLocaleHotfix341 hotfixLocale = new FactionLocaleHotfix341
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.FactionHotfixesLocale341.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void FactionTemplateHandler340(Packet packet, uint entry, params object[] indexes)
        {
            FactionTemplateHotfix340 hotfix = new FactionTemplateHotfix340();

            hotfix.ID = entry;
            hotfix.Faction = packet.ReadUInt16("Faction", indexes);
            hotfix.Flags = packet.ReadUInt16("Flags", indexes);
            hotfix.FactionGroup = packet.ReadByte("FactionGroup", indexes);
            hotfix.FriendGroup = packet.ReadByte("FriendGroup", indexes);
            hotfix.EnemyGroup = packet.ReadByte("EnemyGroup", indexes);
            hotfix.Enemies = new ushort?[4];
            for (int i = 0; i < 4; i++)
                hotfix.Enemies[i] = packet.ReadUInt16("Enemies", indexes, i);
            hotfix.Friend = new ushort?[4];
            for (int i = 0; i < 4; i++)
                hotfix.Friend[i] = packet.ReadUInt16("Friend", indexes, i);

            Storage.FactionTemplateHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void FactionTemplateHandler341(Packet packet, uint entry, params object[] indexes)
        {
            FactionTemplateHotfix341 hotfix = new FactionTemplateHotfix341();

            hotfix.ID = entry;
            hotfix.Faction = packet.ReadUInt16("Faction", indexes);
            hotfix.Flags = packet.ReadUInt16("Flags", indexes);
            hotfix.FactionGroup = packet.ReadByte("FactionGroup", indexes);
            hotfix.FriendGroup = packet.ReadByte("FriendGroup", indexes);
            hotfix.EnemyGroup = packet.ReadByte("EnemyGroup", indexes);
            hotfix.Enemies = new ushort?[8];
            for (int i = 0; i < 8; i++)
                hotfix.Enemies[i] = packet.ReadUInt16("Enemies", indexes, i);
            hotfix.Friend = new ushort?[8];
            for (int i = 0; i < 8; i++)
                hotfix.Friend[i] = packet.ReadUInt16("Friend", indexes, i);

            Storage.FactionTemplateHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void FriendshipRepReactionHandler340(Packet packet, uint entry, params object[] indexes)
        {
            FriendshipRepReactionHotfix340 hotfix = new FriendshipRepReactionHotfix340();

            hotfix.ID = entry;
            hotfix.Reaction = packet.ReadCString("Reaction", indexes);
            hotfix.FriendshipRepID = packet.ReadByte("FriendshipRepID", indexes);
            hotfix.ReactionThreshold = packet.ReadUInt16("ReactionThreshold", indexes);

            Storage.FriendshipRepReactionHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                FriendshipRepReactionLocaleHotfix340 hotfixLocale = new FriendshipRepReactionLocaleHotfix340
                {
                    ID = hotfix.ID,
                    ReactionLang = hotfix.Reaction,
                };
                Storage.FriendshipRepReactionHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void FriendshipReputationHandler340(Packet packet, uint entry, params object[] indexes)
        {
            FriendshipReputationHotfix340 hotfix = new FriendshipReputationHotfix340();

            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.FactionID = packet.ReadUInt16("FactionID", indexes);
            hotfix.TextureFileID = packet.ReadInt32("TextureFileID", indexes);

            Storage.FriendshipReputationHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                FriendshipReputationLocaleHotfix340 hotfixLocale = new FriendshipReputationLocaleHotfix340
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                };
                Storage.FriendshipReputationHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void FriendshipReputationHandler341(Packet packet, uint entry, params object[] indexes)
        {
            FriendshipReputationHotfix341 hotfix = new FriendshipReputationHotfix341();

            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Field34146722002 = packet.ReadInt32("Field34146722002", indexes);
            hotfix.Field34146722003 = packet.ReadInt32("Field34146722003", indexes);

            Storage.FriendshipReputationHotfixes341.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                FriendshipReputationLocaleHotfix341 hotfixLocale = new FriendshipReputationLocaleHotfix341
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                };
                Storage.FriendshipReputationHotfixesLocale341.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void GameobjectArtKitHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GameobjectArtKitHotfix340 hotfix = new GameobjectArtKitHotfix340();

            hotfix.ID = entry;
            hotfix.AttachModelFileID = packet.ReadInt32("AttachModelFileID", indexes);
            hotfix.TextureVariationFileID = new int?[3];
            for (int i = 0; i < 3; i++)
                hotfix.TextureVariationFileID[i] = packet.ReadInt32("TextureVariationFileID", indexes, i);

            Storage.GameobjectArtKitHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void GameobjectDisplayInfoHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GameobjectDisplayInfoHotfix340 hotfix = new GameobjectDisplayInfoHotfix340();

            hotfix.ID = entry;
            hotfix.ModelName = packet.ReadCString("ModelName", indexes);
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

            Storage.GameobjectDisplayInfoHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void GameobjectsHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GameobjectsHotfix340 hotfix = new GameobjectsHotfix340();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.PosX = packet.ReadSingle("PosX", indexes);
            hotfix.PosY = packet.ReadSingle("PosY", indexes);
            hotfix.PosZ = packet.ReadSingle("PosZ", indexes);
            hotfix.Rot = new float?[4];
            for (int i = 0; i < 4; i++)
                hotfix.Rot[i] = packet.ReadSingle("Rot", indexes, i);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.OwnerID = packet.ReadUInt16("OwnerID", indexes);
            hotfix.DisplayID = packet.ReadUInt32("DisplayID", indexes);
            hotfix.Scale = packet.ReadSingle("Scale", indexes);
            hotfix.TypeID = packet.ReadByte("TypeID", indexes);
            hotfix.PhaseUseFlags = packet.ReadByte("PhaseUseFlags", indexes);
            hotfix.PhaseID = packet.ReadUInt16("PhaseID", indexes);
            hotfix.PhaseGroupID = packet.ReadUInt16("PhaseGroupID", indexes);
            hotfix.PropValue = new int?[8];
            for (int i = 0; i < 8; i++)
                hotfix.PropValue[i] = packet.ReadInt32("PropValue", indexes, i);

            Storage.GameobjectsHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                GameobjectsLocaleHotfix340 hotfixLocale = new GameobjectsLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.GameobjectsHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void GarrAbilityHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GarrAbilityHotfix340 hotfix = new GarrAbilityHotfix340();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.GarrAbilityCategoryID = packet.ReadByte("GarrAbilityCategoryID", indexes);
            hotfix.GarrFollowerTypeID = packet.ReadByte("GarrFollowerTypeID", indexes);
            hotfix.IconFileDataID = packet.ReadInt32("IconFileDataID", indexes);
            hotfix.FactionChangeGarrAbilityID = packet.ReadUInt16("FactionChangeGarrAbilityID", indexes);
            hotfix.Flags = packet.ReadUInt16("Flags", indexes);

            Storage.GarrAbilityHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                GarrAbilityLocaleHotfix340 hotfixLocale = new GarrAbilityLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.GarrAbilityHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void GarrBuildingHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GarrBuildingHotfix340 hotfix = new GarrBuildingHotfix340();

            hotfix.ID = entry;
            hotfix.HordeName = packet.ReadCString("HordeName", indexes);
            hotfix.AllianceName = packet.ReadCString("AllianceName", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.Tooltip = packet.ReadCString("Tooltip", indexes);
            hotfix.GarrTypeID = packet.ReadByte("GarrTypeID", indexes);
            hotfix.BuildingType = packet.ReadByte("BuildingType", indexes);
            hotfix.HordeGameObjectID = packet.ReadInt32("HordeGameObjectID", indexes);
            hotfix.AllianceGameObjectID = packet.ReadInt32("AllianceGameObjectID", indexes);
            hotfix.GarrSiteID = packet.ReadByte("GarrSiteID", indexes);
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
            hotfix.Flags = packet.ReadByte("Flags", indexes);

            Storage.GarrBuildingHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                GarrBuildingLocaleHotfix340 hotfixLocale = new GarrBuildingLocaleHotfix340
                {
                    ID = hotfix.ID,
                    HordeNameLang = hotfix.HordeName,
                    AllianceNameLang = hotfix.AllianceName,
                    DescriptionLang = hotfix.Description,
                    TooltipLang = hotfix.Tooltip,
                };
                Storage.GarrBuildingHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void GarrBuildingPlotInstHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GarrBuildingPlotInstHotfix340 hotfix = new GarrBuildingPlotInstHotfix340();

            hotfix.MapOffsetX = packet.ReadSingle("MapOffsetX", indexes);
            hotfix.MapOffsetY = packet.ReadSingle("MapOffsetY", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.GarrBuildingID = packet.ReadByte("GarrBuildingID", indexes);
            hotfix.GarrSiteLevelPlotInstID = packet.ReadUInt16("GarrSiteLevelPlotInstID", indexes);
            hotfix.UiTextureAtlasMemberID = packet.ReadUInt16("UiTextureAtlasMemberID", indexes);

            Storage.GarrBuildingPlotInstHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void GarrClassSpecHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GarrClassSpecHotfix340 hotfix = new GarrClassSpecHotfix340();

            hotfix.ClassSpec = packet.ReadCString("ClassSpec", indexes);
            hotfix.ClassSpecMale = packet.ReadCString("ClassSpecMale", indexes);
            hotfix.ClassSpecFemale = packet.ReadCString("ClassSpecFemale", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.UiTextureAtlasMemberID = packet.ReadUInt16("UiTextureAtlasMemberID", indexes);
            hotfix.GarrFollItemSetID = packet.ReadUInt16("GarrFollItemSetID", indexes);
            hotfix.FollowerClassLimit = packet.ReadByte("FollowerClassLimit", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);

            Storage.GarrClassSpecHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                GarrClassSpecLocaleHotfix340 hotfixLocale = new GarrClassSpecLocaleHotfix340
                {
                    ID = hotfix.ID,
                    ClassSpecLang = hotfix.ClassSpec,
                    ClassSpecMaleLang = hotfix.ClassSpecMale,
                    ClassSpecFemaleLang = hotfix.ClassSpecFemale,
                };
                Storage.GarrClassSpecHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void GarrFollowerHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GarrFollowerHotfix340 hotfix = new GarrFollowerHotfix340();

            hotfix.HordeSourceText = packet.ReadCString("HordeSourceText", indexes);
            hotfix.AllianceSourceText = packet.ReadCString("AllianceSourceText", indexes);
            hotfix.TitleName = packet.ReadCString("TitleName", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.GarrTypeID = packet.ReadByte("GarrTypeID", indexes);
            hotfix.GarrFollowerTypeID = packet.ReadByte("GarrFollowerTypeID", indexes);
            hotfix.HordeCreatureID = packet.ReadInt32("HordeCreatureID", indexes);
            hotfix.AllianceCreatureID = packet.ReadInt32("AllianceCreatureID", indexes);
            hotfix.HordeGarrFollRaceID = packet.ReadByte("HordeGarrFollRaceID", indexes);
            hotfix.AllianceGarrFollRaceID = packet.ReadByte("AllianceGarrFollRaceID", indexes);
            hotfix.HordeGarrClassSpecID = packet.ReadByte("HordeGarrClassSpecID", indexes);
            hotfix.AllianceGarrClassSpecID = packet.ReadByte("AllianceGarrClassSpecID", indexes);
            hotfix.Quality = packet.ReadByte("Quality", indexes);
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
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.Gender = packet.ReadByte("Gender", indexes);

            Storage.GarrFollowerHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                GarrFollowerLocaleHotfix340 hotfixLocale = new GarrFollowerLocaleHotfix340
                {
                    ID = hotfix.ID,
                    HordeSourceTextLang = hotfix.HordeSourceText,
                    AllianceSourceTextLang = hotfix.AllianceSourceText,
                    TitleNameLang = hotfix.TitleName,
                };
                Storage.GarrFollowerHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void GarrFollowerXAbilityHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GarrFollowerXAbilityHotfix340 hotfix = new GarrFollowerXAbilityHotfix340();

            hotfix.ID = entry;
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);
            hotfix.FactionIndex = packet.ReadByte("FactionIndex", indexes);
            hotfix.GarrAbilityID = packet.ReadUInt16("GarrAbilityID", indexes);
            hotfix.GarrFollowerID = packet.ReadInt32("GarrFollowerID", indexes);

            Storage.GarrFollowerXAbilityHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void GarrMissionHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GarrMissionHotfix340 hotfix = new GarrMissionHotfix340();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Location = packet.ReadCString("Location", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.MapPosX = packet.ReadSingle("MapPosX", indexes);
            hotfix.MapPosY = packet.ReadSingle("MapPosY", indexes);
            hotfix.WorldPosX = packet.ReadSingle("WorldPosX", indexes);
            hotfix.WorldPosY = packet.ReadSingle("WorldPosY", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.GarrTypeID = packet.ReadByte("GarrTypeID", indexes);
            hotfix.GarrMissionTypeID = packet.ReadByte("GarrMissionTypeID", indexes);
            hotfix.GarrFollowerTypeID = packet.ReadByte("GarrFollowerTypeID", indexes);
            hotfix.MaxFollowers = packet.ReadByte("MaxFollowers", indexes);
            hotfix.MissionCost = packet.ReadUInt32("MissionCost", indexes);
            hotfix.MissionCostCurrencyTypesID = packet.ReadUInt16("MissionCostCurrencyTypesID", indexes);
            hotfix.OfferedGarrMissionTextureID = packet.ReadByte("OfferedGarrMissionTextureID", indexes);
            hotfix.UiTextureKitID = packet.ReadUInt16("UiTextureKitID", indexes);
            hotfix.EnvGarrMechanicID = packet.ReadUInt32("EnvGarrMechanicID", indexes);
            hotfix.EnvGarrMechanicTypeID = packet.ReadByte("EnvGarrMechanicTypeID", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
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
            hotfix.Flags = packet.ReadUInt32("Flags", indexes);
            hotfix.GarrMissionSetID = packet.ReadInt32("GarrMissionSetID", indexes);

            Storage.GarrMissionHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                GarrMissionLocaleHotfix340 hotfixLocale = new GarrMissionLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    LocationLang = hotfix.Location,
                    DescriptionLang = hotfix.Description,
                };
                Storage.GarrMissionHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void GarrPlotHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GarrPlotHotfix340 hotfix = new GarrPlotHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.PlotType = packet.ReadByte("PlotType", indexes);
            hotfix.HordeConstructObjID = packet.ReadInt32("HordeConstructObjID", indexes);
            hotfix.AllianceConstructObjID = packet.ReadInt32("AllianceConstructObjID", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.UiCategoryID = packet.ReadByte("UiCategoryID", indexes);
            hotfix.UpgradeRequirement = new uint?[2];
            for (int i = 0; i < 2; i++)
                hotfix.UpgradeRequirement[i] = packet.ReadUInt32("UpgradeRequirement", indexes, i);

            Storage.GarrPlotHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void GarrPlotBuildingHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GarrPlotBuildingHotfix340 hotfix = new GarrPlotBuildingHotfix340();

            hotfix.ID = entry;
            hotfix.GarrPlotID = packet.ReadByte("GarrPlotID", indexes);
            hotfix.GarrBuildingID = packet.ReadByte("GarrBuildingID", indexes);

            Storage.GarrPlotBuildingHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void GarrPlotInstanceHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GarrPlotInstanceHotfix340 hotfix = new GarrPlotInstanceHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.GarrPlotID = packet.ReadByte("GarrPlotID", indexes);

            Storage.GarrPlotInstanceHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void GarrSiteLevelHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GarrSiteLevelHotfix340 hotfix = new GarrSiteLevelHotfix340();

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

            Storage.GarrSiteLevelHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void GarrSiteLevelPlotInstHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GarrSiteLevelPlotInstHotfix340 hotfix = new GarrSiteLevelPlotInstHotfix340();

            hotfix.ID = entry;
            hotfix.UiMarkerPosX = packet.ReadSingle("UiMarkerPosX", indexes);
            hotfix.UiMarkerPosY = packet.ReadSingle("UiMarkerPosY", indexes);
            hotfix.GarrSiteLevelID = packet.ReadUInt16("GarrSiteLevelID", indexes);
            hotfix.GarrPlotInstanceID = packet.ReadByte("GarrPlotInstanceID", indexes);
            hotfix.UiMarkerSize = packet.ReadByte("UiMarkerSize", indexes);

            Storage.GarrSiteLevelPlotInstHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void GarrTalentTreeHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GarrTalentTreeHotfix340 hotfix = new GarrTalentTreeHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.GarrTypeID = packet.ReadInt32("GarrTypeID", indexes);
            hotfix.ClassID = packet.ReadInt32("ClassID", indexes);
            hotfix.MaxTiers = packet.ReadSByte("MaxTiers", indexes);
            hotfix.UiOrder = packet.ReadSByte("UiOrder", indexes);
            hotfix.Flags = packet.ReadSByte("Flags", indexes);
            hotfix.UiTextureKitID = packet.ReadUInt16("UiTextureKitID", indexes);

            Storage.GarrTalentTreeHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                GarrTalentTreeLocaleHotfix340 hotfixLocale = new GarrTalentTreeLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.GarrTalentTreeHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void GemPropertiesHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GemPropertiesHotfix340 hotfix = new GemPropertiesHotfix340();

            hotfix.ID = entry;
            hotfix.EnchantID = packet.ReadUInt16("EnchantID", indexes);
            hotfix.Type = packet.ReadInt32("Type", indexes);
            hotfix.MinItemLevel = packet.ReadUInt16("MinItemLevel", indexes);

            Storage.GemPropertiesHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void GlyphBindableSpellHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GlyphBindableSpellHotfix340 hotfix = new GlyphBindableSpellHotfix340();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.GlyphPropertiesID = packet.ReadInt32("GlyphPropertiesID", indexes);

            Storage.GlyphBindableSpellHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void GlyphSlotHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GlyphSlotHotfix340 hotfix = new GlyphSlotHotfix340();

            hotfix.ID = entry;
            hotfix.Tooltip = packet.ReadInt32("Tooltip", indexes);
            hotfix.Type = packet.ReadUInt32("Type", indexes);

            Storage.GlyphSlotHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void GlyphPropertiesHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GlyphPropertiesHotfix340 hotfix = new GlyphPropertiesHotfix340();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadUInt32("SpellID", indexes);
            hotfix.GlyphType = packet.ReadByte("GlyphType", indexes);
            hotfix.GlyphExclusiveCategoryID = packet.ReadByte("GlyphExclusiveCategoryID", indexes);
            hotfix.SpellIconFileDataID = packet.ReadInt32("SpellIconFileDataID", indexes);
            hotfix.GlyphSlotFlags = packet.ReadUInt32("GlyphSlotFlags", indexes);

            Storage.GlyphPropertiesHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void GlyphRequiredSpecHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GlyphRequiredSpecHotfix340 hotfix = new GlyphRequiredSpecHotfix340();

            hotfix.ID = entry;
            hotfix.ChrSpecializationID = packet.ReadUInt16("ChrSpecializationID", indexes);
            hotfix.GlyphPropertiesID = packet.ReadInt32("GlyphPropertiesID", indexes);

            Storage.GlyphRequiredSpecHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void GossipNPCOptionHandler341(Packet packet, uint entry, params object[] indexes)
        {
            GossipNPCOptionHotfix341 hotfix = new GossipNPCOptionHotfix341();

            hotfix.ID = entry;
            hotfix.GossipNpcOption = packet.ReadInt32("GossipNpcOption", indexes);
            hotfix.LFGDungeonsID = packet.ReadInt32("LFGDungeonsID", indexes);
            hotfix.Unk341_1 = packet.ReadInt32("Unk341_1", indexes);
            hotfix.Unk341_2 = packet.ReadInt32("Unk341_2", indexes);
            hotfix.Unk341_3 = packet.ReadInt32("Unk341_3", indexes);
            hotfix.Unk341_4 = packet.ReadInt32("Unk341_4", indexes);
            hotfix.Unk341_5 = packet.ReadInt32("Unk341_5", indexes);
            hotfix.Unk341_6 = packet.ReadInt32("Unk341_6", indexes);
            hotfix.Unk341_7 = packet.ReadInt32("Unk341_7", indexes);
            hotfix.Unk341_8 = packet.ReadInt32("Unk341_8", indexes);
            hotfix.Unk341_9 = packet.ReadInt32("Unk341_9", indexes);
            hotfix.GossipOptionID = packet.ReadInt32("GossipOptionID", indexes);

            Storage.GossipNPCOptionHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void GuildColorBackgroundHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GuildColorBackgroundHotfix340 hotfix = new GuildColorBackgroundHotfix340();

            hotfix.ID = entry;
            hotfix.Red = packet.ReadByte("Red", indexes);
            hotfix.Blue = packet.ReadByte("Blue", indexes);
            hotfix.Green = packet.ReadByte("Green", indexes);

            Storage.GuildColorBackgroundHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void GuildColorBorderHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GuildColorBorderHotfix340 hotfix = new GuildColorBorderHotfix340();

            hotfix.ID = entry;
            hotfix.Red = packet.ReadByte("Red", indexes);
            hotfix.Blue = packet.ReadByte("Blue", indexes);
            hotfix.Green = packet.ReadByte("Green", indexes);

            Storage.GuildColorBorderHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void GuildColorEmblemHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GuildColorEmblemHotfix340 hotfix = new GuildColorEmblemHotfix340();

            hotfix.ID = entry;
            hotfix.Red = packet.ReadByte("Red", indexes);
            hotfix.Blue = packet.ReadByte("Blue", indexes);
            hotfix.Green = packet.ReadByte("Green", indexes);

            Storage.GuildColorEmblemHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void GuildPerkSpellsHandler340(Packet packet, uint entry, params object[] indexes)
        {
            GuildPerkSpellsHotfix340 hotfix = new GuildPerkSpellsHotfix340();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);

            Storage.GuildPerkSpellsHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void HeirloomHandler340(Packet packet, uint entry, params object[] indexes)
        {
            HeirloomHotfix340 hotfix = new HeirloomHotfix340();

            hotfix.SourceText = packet.ReadCString("SourceText", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);
            hotfix.LegacyUpgradedItemID = packet.ReadInt32("LegacyUpgradedItemID", indexes);
            hotfix.StaticUpgradedItemID = packet.ReadInt32("StaticUpgradedItemID", indexes);
            hotfix.SourceTypeEnum = packet.ReadSByte("SourceTypeEnum", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.LegacyItemID = packet.ReadInt32("LegacyItemID", indexes);
            hotfix.UpgradeItemID = new int?[6];
            for (int i = 0; i < 6; i++)
                hotfix.UpgradeItemID[i] = packet.ReadInt32("UpgradeItemID", indexes, i);
            hotfix.UpgradeItemBonusListID = new ushort?[6];
            for (int i = 0; i < 6; i++)
                hotfix.UpgradeItemBonusListID[i] = packet.ReadUInt16("UpgradeItemBonusListID", indexes, i);

            Storage.HeirloomHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                HeirloomLocaleHotfix340 hotfixLocale = new HeirloomLocaleHotfix340
                {
                    ID = hotfix.ID,
                    SourceTextLang = hotfix.SourceText,
                };
                Storage.HeirloomHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void HolidaysHandler340(Packet packet, uint entry, params object[] indexes)
        {
            HolidaysHotfix340 hotfix = new HolidaysHotfix340();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Region = packet.ReadUInt16("Region", indexes);
            hotfix.Looping = packet.ReadByte("Looping", indexes);
            hotfix.HolidayNameID = packet.ReadUInt32("HolidayNameID", indexes);
            hotfix.HolidayDescriptionID = packet.ReadUInt32("HolidayDescriptionID", indexes);
            hotfix.Priority = packet.ReadByte("Priority", indexes);
            hotfix.CalendarFilterType = packet.ReadSByte("CalendarFilterType", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.WorldStateExpressionID = packet.ReadUInt32("WorldStateExpressionID", indexes);
            hotfix.Duration = new ushort?[10];
            for (int i = 0; i < 10; i++)
                hotfix.Duration[i] = packet.ReadUInt16("Duration", indexes, i);
            hotfix.Date = new uint?[16];
            for (int i = 0; i < 16; i++)
                hotfix.Date[i] = packet.ReadUInt32("Date", indexes, i);
            hotfix.CalendarFlags = new byte?[10];
            for (int i = 0; i < 10; i++)
                hotfix.CalendarFlags[i] = packet.ReadByte("CalendarFlags", indexes, i);
            hotfix.TextureFileDataID = new int?[3];
            for (int i = 0; i < 3; i++)
                hotfix.TextureFileDataID[i] = packet.ReadInt32("TextureFileDataID", indexes, i);

            Storage.HolidaysHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ImportPriceArmorHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ImportPriceArmorHotfix340 hotfix = new ImportPriceArmorHotfix340();

            hotfix.ID = entry;
            hotfix.ClothModifier = packet.ReadSingle("ClothModifier", indexes);
            hotfix.LeatherModifier = packet.ReadSingle("LeatherModifier", indexes);
            hotfix.ChainModifier = packet.ReadSingle("ChainModifier", indexes);
            hotfix.PlateModifier = packet.ReadSingle("PlateModifier", indexes);

            Storage.ImportPriceArmorHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ImportPriceQualityHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ImportPriceQualityHotfix340 hotfix = new ImportPriceQualityHotfix340();

            hotfix.ID = entry;
            hotfix.Data = packet.ReadSingle("Data", indexes);

            Storage.ImportPriceQualityHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ImportPriceShieldHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ImportPriceShieldHotfix340 hotfix = new ImportPriceShieldHotfix340();

            hotfix.ID = entry;
            hotfix.Data = packet.ReadSingle("Data", indexes);

            Storage.ImportPriceShieldHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ImportPriceWeaponHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ImportPriceWeaponHotfix340 hotfix = new ImportPriceWeaponHotfix340();

            hotfix.ID = entry;
            hotfix.Data = packet.ReadSingle("Data", indexes);

            Storage.ImportPriceWeaponHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemHotfix340 hotfix = new ItemHotfix340();

            hotfix.ID = entry;
            hotfix.ClassID = packet.ReadByte("ClassID", indexes);
            hotfix.SubclassID = packet.ReadByte("SubclassID", indexes);
            hotfix.Material = packet.ReadByte("Material", indexes);
            hotfix.InventoryType = packet.ReadSByte("InventoryType", indexes);
            hotfix.RequiredLevel = packet.ReadInt32("RequiredLevel", indexes);
            hotfix.SheatheType = packet.ReadByte("SheatheType", indexes);
            hotfix.RandomSelect = packet.ReadUInt16("RandomSelect", indexes);
            hotfix.ItemRandomSuffixGroupID = packet.ReadUInt16("ItemRandomSuffixGroupID", indexes);
            hotfix.SoundOverrideSubclassID = packet.ReadSByte("SoundOverrideSubclassID", indexes);
            hotfix.ScalingStatDistributionID = packet.ReadUInt16("ScalingStatDistributionID", indexes);
            hotfix.IconFileDataID = packet.ReadInt32("IconFileDataID", indexes);
            hotfix.ItemGroupSoundsID = packet.ReadByte("ItemGroupSoundsID", indexes);
            hotfix.ContentTuningID = packet.ReadInt32("ContentTuningID", indexes);
            hotfix.MaxDurability = packet.ReadUInt32("MaxDurability", indexes);
            hotfix.AmmunitionType = packet.ReadByte("AmmunitionType", indexes);
            hotfix.DamageType = new byte?[5];
            for (int i = 0; i < 5; i++)
                hotfix.DamageType[i] = packet.ReadByte("DamageType", indexes, i);
            hotfix.Resistances = new short?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Resistances[i] = packet.ReadInt16("Resistances", indexes, i);
            hotfix.MinDamage = new ushort?[5];
            for (int i = 0; i < 5; i++)
                hotfix.MinDamage[i] = packet.ReadUInt16("MinDamage", indexes, i);
            hotfix.MaxDamage = new ushort?[5];
            for (int i = 0; i < 5; i++)
                hotfix.MaxDamage[i] = packet.ReadUInt16("MaxDamage", indexes, i);

            Storage.ItemHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemHandler341(Packet packet, uint entry, params object[] indexes)
        {
            ItemHotfix341 hotfix = new ItemHotfix341();

            hotfix.ID = entry;
            hotfix.ClassID = packet.ReadByte("ClassID", indexes);
            hotfix.SubclassID = packet.ReadByte("SubclassID", indexes);
            hotfix.Material = packet.ReadByte("Material", indexes);
            hotfix.InventoryType = packet.ReadSByte("InventoryType", indexes);
            hotfix.RequiredLevel = packet.ReadInt32("RequiredLevel", indexes);
            hotfix.SheatheType = packet.ReadByte("SheatheType", indexes);
            hotfix.RandomSelect = packet.ReadUInt16("RandomSelect", indexes);
            hotfix.ItemRandomSuffixGroupID = packet.ReadUInt16("ItemRandomSuffixGroupID", indexes);
            hotfix.SoundOverrideSubclassID = packet.ReadSByte("SoundOverrideSubclassID", indexes);
            hotfix.ScalingStatDistributionID = packet.ReadUInt16("ScalingStatDistributionID", indexes);
            hotfix.IconFileDataID = packet.ReadInt32("IconFileDataID", indexes);
            hotfix.ItemGroupSoundsID = packet.ReadByte("ItemGroupSoundsID", indexes);
            hotfix.ContentTuningID = packet.ReadInt32("ContentTuningID", indexes);
            hotfix.MaxDurability = packet.ReadUInt32("MaxDurability", indexes);
            hotfix.AmmunitionType = packet.ReadByte("AmmunitionType", indexes);
            hotfix.ScalingStatValue = packet.ReadInt32("ScalingStatValue", indexes);
            hotfix.DamageType = new byte?[5];
            for (int i = 0; i < 5; i++)
                hotfix.DamageType[i] = packet.ReadByte("DamageType", indexes, i);
            hotfix.Resistances = new short?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Resistances[i] = packet.ReadInt16("Resistances", indexes, i);
            hotfix.MinDamage = new ushort?[5];
            for (int i = 0; i < 5; i++)
                hotfix.MinDamage[i] = packet.ReadUInt16("MinDamage", indexes, i);
            hotfix.MaxDamage = new ushort?[5];
            for (int i = 0; i < 5; i++)
                hotfix.MaxDamage[i] = packet.ReadUInt16("MaxDamage", indexes, i);

            Storage.ItemHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemAppearanceHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemAppearanceHotfix340 hotfix = new ItemAppearanceHotfix340();

            hotfix.ID = entry;
            hotfix.DisplayType = packet.ReadByte("DisplayType", indexes);
            hotfix.ItemDisplayInfoID = packet.ReadInt32("ItemDisplayInfoID", indexes);
            hotfix.DefaultIconFileDataID = packet.ReadInt32("DefaultIconFileDataID", indexes);
            hotfix.UiOrder = packet.ReadInt32("UiOrder", indexes);

            Storage.ItemAppearanceHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemArmorQualityHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemArmorQualityHotfix340 hotfix = new ItemArmorQualityHotfix340();

            hotfix.ID = entry;
            hotfix.Qualitymod = new float?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Qualitymod[i] = packet.ReadSingle("Qualitymod", indexes, i);

            Storage.ItemArmorQualityHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemArmorShieldHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemArmorShieldHotfix340 hotfix = new ItemArmorShieldHotfix340();

            hotfix.ID = entry;
            hotfix.Quality = new float?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Quality[i] = packet.ReadSingle("Quality", indexes, i);
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);

            Storage.ItemArmorShieldHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemArmorTotalHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemArmorTotalHotfix340 hotfix = new ItemArmorTotalHotfix340();

            hotfix.ID = entry;
            hotfix.ItemLevel = packet.ReadInt16("ItemLevel", indexes);
            hotfix.Cloth = packet.ReadSingle("Cloth", indexes);
            hotfix.Leather = packet.ReadSingle("Leather", indexes);
            hotfix.Mail = packet.ReadSingle("Mail", indexes);
            hotfix.Plate = packet.ReadSingle("Plate", indexes);

            Storage.ItemArmorTotalHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemBagFamilyHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemBagFamilyHotfix340 hotfix = new ItemBagFamilyHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);

            Storage.ItemBagFamilyHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemBagFamilyLocaleHotfix340 hotfixLocale = new ItemBagFamilyLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ItemBagFamilyHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemBonusHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemBonusHotfix340 hotfix = new ItemBonusHotfix340();

            hotfix.ID = entry;
            hotfix.Value = new int?[4];
            for (int i = 0; i < 4; i++)
                hotfix.Value[i] = packet.ReadInt32("Value", indexes, i);
            hotfix.ParentItemBonusListID = packet.ReadUInt16("ParentItemBonusListID", indexes);
            hotfix.Type = packet.ReadByte("Type", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);

            Storage.ItemBonusHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemBonusListLevelDeltaHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemBonusListLevelDeltaHotfix340 hotfix = new ItemBonusListLevelDeltaHotfix340();

            hotfix.ItemLevelDelta = packet.ReadInt16("ItemLevelDelta", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);

            Storage.ItemBonusListLevelDeltaHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemBonusTreeNodeHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemBonusTreeNodeHotfix340 hotfix = new ItemBonusTreeNodeHotfix340();

            hotfix.ID = entry;
            hotfix.ItemContext = packet.ReadByte("ItemContext", indexes);
            hotfix.ChildItemBonusTreeID = packet.ReadUInt16("ChildItemBonusTreeID", indexes);
            hotfix.ChildItemBonusListID = packet.ReadUInt16("ChildItemBonusListID", indexes);
            hotfix.ChildItemLevelSelectorID = packet.ReadUInt16("ChildItemLevelSelectorID", indexes);
            hotfix.ParentItemBonusTreeID = packet.ReadInt32("ParentItemBonusTreeID", indexes);

            Storage.ItemBonusTreeNodeHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemChildEquipmentHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemChildEquipmentHotfix340 hotfix = new ItemChildEquipmentHotfix340();

            hotfix.ID = entry;
            hotfix.ChildItemID = packet.ReadInt32("ChildItemID", indexes);
            hotfix.ChildItemEquipSlot = packet.ReadByte("ChildItemEquipSlot", indexes);
            hotfix.ParentItemID = packet.ReadInt32("ParentItemID", indexes);

            Storage.ItemChildEquipmentHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemClassHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemClassHotfix340 hotfix = new ItemClassHotfix340();

            hotfix.ID = entry;
            hotfix.ClassName = packet.ReadCString("ClassName", indexes);
            hotfix.ClassID = packet.ReadSByte("ClassID", indexes);
            hotfix.PriceModifier = packet.ReadSingle("PriceModifier", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);

            Storage.ItemClassHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemClassLocaleHotfix340 hotfixLocale = new ItemClassLocaleHotfix340
                {
                    ID = hotfix.ID,
                    ClassNameLang = hotfix.ClassName,
                };
                Storage.ItemClassHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemContextPickerEntryHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemContextPickerEntryHotfix340 hotfix = new ItemContextPickerEntryHotfix340();

            hotfix.ID = entry;
            hotfix.ItemCreationContext = packet.ReadByte("ItemCreationContext", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);
            hotfix.PVal = packet.ReadInt32("PVal", indexes);
            hotfix.Flags = packet.ReadUInt32("Flags", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.ItemContextPickerID = packet.ReadInt32("ItemContextPickerID", indexes);

            Storage.ItemContextPickerEntryHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemCurrencyCostHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemCurrencyCostHotfix340 hotfix = new ItemCurrencyCostHotfix340();

            hotfix.ID = entry;
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);

            Storage.ItemCurrencyCostHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemDamageAmmoHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemDamageAmmoHotfix340 hotfix = new ItemDamageAmmoHotfix340();

            hotfix.ID = entry;
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);
            hotfix.Quality = new float?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Quality[i] = packet.ReadSingle("Quality", indexes, i);

            Storage.ItemDamageAmmoHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemDamageOneHandHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemDamageOneHandHotfix340 hotfix = new ItemDamageOneHandHotfix340();

            hotfix.ID = entry;
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);
            hotfix.Quality = new float?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Quality[i] = packet.ReadSingle("Quality", indexes, i);

            Storage.ItemDamageOneHandHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemDamageOneHandCasterHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemDamageOneHandCasterHotfix340 hotfix = new ItemDamageOneHandCasterHotfix340();

            hotfix.ID = entry;
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);
            hotfix.Quality = new float?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Quality[i] = packet.ReadSingle("Quality", indexes, i);

            Storage.ItemDamageOneHandCasterHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemDamageTwoHandHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemDamageTwoHandHotfix340 hotfix = new ItemDamageTwoHandHotfix340();

            hotfix.ID = entry;
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);
            hotfix.Quality = new float?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Quality[i] = packet.ReadSingle("Quality", indexes, i);

            Storage.ItemDamageTwoHandHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemDamageTwoHandCasterHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemDamageTwoHandCasterHotfix340 hotfix = new ItemDamageTwoHandCasterHotfix340();

            hotfix.ID = entry;
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);
            hotfix.Quality = new float?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Quality[i] = packet.ReadSingle("Quality", indexes, i);

            Storage.ItemDamageTwoHandCasterHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemDisenchantLootHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemDisenchantLootHotfix340 hotfix = new ItemDisenchantLootHotfix340();

            hotfix.ID = entry;
            hotfix.Subclass = packet.ReadSByte("Subclass", indexes);
            hotfix.Quality = packet.ReadByte("Quality", indexes);
            hotfix.MinLevel = packet.ReadUInt16("MinLevel", indexes);
            hotfix.MaxLevel = packet.ReadUInt16("MaxLevel", indexes);
            hotfix.SkillRequired = packet.ReadUInt16("SkillRequired", indexes);
            hotfix.ExpansionID = packet.ReadSByte("ExpansionID", indexes);
            hotfix.Class = packet.ReadInt32("Class", indexes);

            Storage.ItemDisenchantLootHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemEffectHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemEffectHotfix340 hotfix = new ItemEffectHotfix340();

            hotfix.ID = entry;
            hotfix.LegacySlotIndex = packet.ReadByte("LegacySlotIndex", indexes);
            hotfix.TriggerType = packet.ReadSByte("TriggerType", indexes);
            hotfix.Charges = packet.ReadInt16("Charges", indexes);
            hotfix.CoolDownMSec = packet.ReadInt32("CoolDownMSec", indexes);
            hotfix.CategoryCoolDownMSec = packet.ReadInt32("CategoryCoolDownMSec", indexes);
            hotfix.SpellCategoryID = packet.ReadUInt16("SpellCategoryID", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.ChrSpecializationID = packet.ReadUInt16("ChrSpecializationID", indexes);
            hotfix.ParentItemID = packet.ReadInt32("ParentItemID", indexes);

            Storage.ItemEffectHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemExtendedCostHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemExtendedCostHotfix340 hotfix = new ItemExtendedCostHotfix340();

            hotfix.ID = entry;
            hotfix.RequiredArenaRating = packet.ReadUInt16("RequiredArenaRating", indexes);
            hotfix.ArenaBracket = packet.ReadSByte("ArenaBracket", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.MinFactionID = packet.ReadByte("MinFactionID", indexes);
            hotfix.MinReputation = packet.ReadByte("MinReputation", indexes);
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

            Storage.ItemExtendedCostHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemExtendedCostHandler341(Packet packet, uint entry, params object[] indexes)
        {
            ItemExtendedCostHotfix341 hotfix = new ItemExtendedCostHotfix341();

            hotfix.ID = entry;
            hotfix.RequiredArenaRating = packet.ReadUInt16("RequiredArenaRating", indexes);
            hotfix.ArenaBracket = packet.ReadSByte("ArenaBracket", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
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

            Storage.ItemExtendedCostHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemLevelSelectorHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemLevelSelectorHotfix340 hotfix = new ItemLevelSelectorHotfix340();

            hotfix.ID = entry;
            hotfix.MinItemLevel = packet.ReadUInt16("MinItemLevel", indexes);
            hotfix.ItemLevelSelectorQualitySetID = packet.ReadUInt16("ItemLevelSelectorQualitySetID", indexes);

            Storage.ItemLevelSelectorHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemLevelSelectorQualityHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemLevelSelectorQualityHotfix340 hotfix = new ItemLevelSelectorQualityHotfix340();

            hotfix.ID = entry;
            hotfix.QualityItemBonusListID = packet.ReadInt32("QualityItemBonusListID", indexes);
            hotfix.Quality = packet.ReadSByte("Quality", indexes);
            hotfix.ParentILSQualitySetID = packet.ReadInt32("ParentILSQualitySetID", indexes);

            Storage.ItemLevelSelectorQualityHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemLevelSelectorQualitySetHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemLevelSelectorQualitySetHotfix340 hotfix = new ItemLevelSelectorQualitySetHotfix340();

            hotfix.ID = entry;
            hotfix.IlvlRare = packet.ReadInt16("IlvlRare", indexes);
            hotfix.IlvlEpic = packet.ReadInt16("IlvlEpic", indexes);

            Storage.ItemLevelSelectorQualitySetHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemLimitCategoryHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemLimitCategoryHotfix340 hotfix = new ItemLimitCategoryHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Quantity = packet.ReadByte("Quantity", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);

            Storage.ItemLimitCategoryHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemLimitCategoryLocaleHotfix340 hotfixLocale = new ItemLimitCategoryLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ItemLimitCategoryHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemLimitCategoryConditionHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemLimitCategoryConditionHotfix340 hotfix = new ItemLimitCategoryConditionHotfix340();

            hotfix.ID = entry;
            hotfix.AddQuantity = packet.ReadSByte("AddQuantity", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.ParentItemLimitCategoryID = packet.ReadInt32("ParentItemLimitCategoryID", indexes);

            Storage.ItemLimitCategoryConditionHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemModifiedAppearanceHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemModifiedAppearanceHotfix340 hotfix = new ItemModifiedAppearanceHotfix340();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);
            hotfix.ItemAppearanceModifierID = packet.ReadInt32("ItemAppearanceModifierID", indexes);
            hotfix.ItemAppearanceID = packet.ReadInt32("ItemAppearanceID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.TransmogSourceTypeEnum = packet.ReadSByte("TransmogSourceTypeEnum", indexes);

            Storage.ItemModifiedAppearanceHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemModifiedAppearanceExtraHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemModifiedAppearanceExtraHotfix340 hotfix = new ItemModifiedAppearanceExtraHotfix340();

            hotfix.ID = entry;
            hotfix.IconFileDataID = packet.ReadInt32("IconFileDataID", indexes);
            hotfix.UnequippedIconFileDataID = packet.ReadInt32("UnequippedIconFileDataID", indexes);
            hotfix.SheatheType = packet.ReadByte("SheatheType", indexes);
            hotfix.DisplayWeaponSubclassID = packet.ReadSByte("DisplayWeaponSubclassID", indexes);
            hotfix.DisplayInventoryType = packet.ReadSByte("DisplayInventoryType", indexes);

            Storage.ItemModifiedAppearanceExtraHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemNameDescriptionHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemNameDescriptionHotfix340 hotfix = new ItemNameDescriptionHotfix340();

            hotfix.ID = entry;
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.Color = packet.ReadInt32("Color", indexes);

            Storage.ItemNameDescriptionHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemNameDescriptionLocaleHotfix340 hotfixLocale = new ItemNameDescriptionLocaleHotfix340
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                };
                Storage.ItemNameDescriptionHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemPriceBaseHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemPriceBaseHotfix340 hotfix = new ItemPriceBaseHotfix340();

            hotfix.ID = entry;
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);
            hotfix.Armor = packet.ReadSingle("Armor", indexes);
            hotfix.Weapon = packet.ReadSingle("Weapon", indexes);

            Storage.ItemPriceBaseHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemRandomPropertiesHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemRandomPropertiesHotfix340 hotfix = new ItemRandomPropertiesHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Enchantment = new ushort?[5];
            for (int i = 0; i < 5; i++)
                hotfix.Enchantment[i] = packet.ReadUInt16("Enchantment", indexes, i);

            Storage.ItemRandomPropertiesHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemRandomPropertiesLocaleHotfix340 hotfixLocale = new ItemRandomPropertiesLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ItemRandomPropertiesHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemRandomSuffixHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemRandomSuffixHotfix340 hotfix = new ItemRandomSuffixHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Enchantment = new ushort?[5];
            for (int i = 0; i < 5; i++)
                hotfix.Enchantment[i] = packet.ReadUInt16("Enchantment", indexes, i);
            hotfix.AllocationPct = new ushort?[5];
            for (int i = 0; i < 5; i++)
                hotfix.AllocationPct[i] = packet.ReadUInt16("AllocationPct", indexes, i);

            Storage.ItemRandomSuffixHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemRandomSuffixLocaleHotfix340 hotfixLocale = new ItemRandomSuffixLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ItemRandomSuffixHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemSearchNameHandler342(Packet packet, uint entry, params object[] indexes)
        {
            ItemSearchNameHotfix342 hotfix = new ItemSearchNameHotfix342();

            hotfix.AllowableRace = packet.ReadInt64("AllowableRace", indexes);
            hotfix.Display = packet.ReadCString("Display", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.OverallQualityID = packet.ReadByte("OverallQualityID", indexes);
            hotfix.ExpansionID = packet.ReadSByte("ExpansionID", indexes);
            hotfix.MinFactionID = packet.ReadUInt16("MinFactionID", indexes);
            hotfix.MinReputation = packet.ReadInt32("MinReputation", indexes);
            hotfix.AllowableClass = packet.ReadInt32("AllowableClass", indexes);
            hotfix.RequiredLevel = packet.ReadSByte("RequiredLevel", indexes);
            hotfix.RequiredSkill = packet.ReadUInt16("RequiredSkill", indexes);
            hotfix.RequiredSkillRank = packet.ReadUInt16("RequiredSkillRank", indexes);
            hotfix.RequiredAbility = packet.ReadUInt32("RequiredAbility", indexes);
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);
            hotfix.Flags = new int?[4];
            for (int i = 0; i < 4; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.ItemSearchNameHotfixes342.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemSearchNameLocaleHotfix342 hotfixLocale = new ItemSearchNameLocaleHotfix342
                {
                    ID = hotfix.ID,
                    DisplayLang = hotfix.Display,
                };
                Storage.ItemSearchNameHotfixesLocale342.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemSetHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemSetHotfix340 hotfix = new ItemSetHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.SetFlags = packet.ReadUInt32("SetFlags", indexes);
            hotfix.RequiredSkill = packet.ReadUInt32("RequiredSkill", indexes);
            hotfix.RequiredSkillRank = packet.ReadUInt16("RequiredSkillRank", indexes);
            hotfix.ItemID = new uint?[17];
            for (int i = 0; i < 17; i++)
                hotfix.ItemID[i] = packet.ReadUInt32("ItemID", indexes, i);

            Storage.ItemSetHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemSetLocaleHotfix340 hotfixLocale = new ItemSetLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ItemSetHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemSetSpellHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemSetSpellHotfix340 hotfix = new ItemSetSpellHotfix340();

            hotfix.ID = entry;
            hotfix.ChrSpecID = packet.ReadUInt16("ChrSpecID", indexes);
            hotfix.SpellID = packet.ReadUInt32("SpellID", indexes);
            hotfix.Threshold = packet.ReadByte("Threshold", indexes);
            hotfix.ItemSetID = packet.ReadInt32("ItemSetID", indexes);

            Storage.ItemSetSpellHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemSparseHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemSparseHotfix340 hotfix = new ItemSparseHotfix340();

            hotfix.ID = entry;
            hotfix.AllowableRace = packet.ReadInt64("AllowableRace", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.Display3 = packet.ReadCString("Display3", indexes);
            hotfix.Display2 = packet.ReadCString("Display2", indexes);
            hotfix.Display1 = packet.ReadCString("Display1", indexes);
            hotfix.Display = packet.ReadCString("Display", indexes);
            hotfix.DmgVariance = packet.ReadSingle("DmgVariance", indexes);
            hotfix.DurationInInventory = packet.ReadUInt32("DurationInInventory", indexes);
            hotfix.QualityModifier = packet.ReadSingle("QualityModifier", indexes);
            hotfix.BagFamily = packet.ReadUInt32("BagFamily", indexes);
            hotfix.StartQuestID = packet.ReadInt32("StartQuestID", indexes);
            hotfix.ItemRange = packet.ReadSingle("ItemRange", indexes);
            hotfix.StatPercentageOfSocket = new float?[10];
            for (int i = 0; i < 10; i++)
                hotfix.StatPercentageOfSocket[i] = packet.ReadSingle("StatPercentageOfSocket", indexes, i);
            hotfix.StatPercentEditor = new int?[10];
            for (int i = 0; i < 10; i++)
                hotfix.StatPercentEditor[i] = packet.ReadInt32("StatPercentEditor", indexes, i);
            hotfix.Stackable = packet.ReadInt32("Stackable", indexes);
            hotfix.MaxCount = packet.ReadInt32("MaxCount", indexes);
            hotfix.RequiredAbility = packet.ReadUInt32("RequiredAbility", indexes);
            hotfix.SellPrice = packet.ReadUInt32("SellPrice", indexes);
            hotfix.BuyPrice = packet.ReadUInt32("BuyPrice", indexes);
            hotfix.VendorStackCount = packet.ReadUInt32("VendorStackCount", indexes);
            hotfix.PriceVariance = packet.ReadSingle("PriceVariance", indexes);
            hotfix.PriceRandomValue = packet.ReadSingle("PriceRandomValue", indexes);
            hotfix.Flags = new int?[4];
            for (int i = 0; i < 4; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);
            hotfix.FactionRelated = packet.ReadInt32("FactionRelated", indexes);
            hotfix.ModifiedCraftingReagentItemID = packet.ReadInt32("ModifiedCraftingReagentItemID", indexes);
            hotfix.ContentTuningID = packet.ReadInt32("ContentTuningID", indexes);
            hotfix.PlayerLevelToItemLevelCurveID = packet.ReadInt32("PlayerLevelToItemLevelCurveID", indexes);
            hotfix.MaxDurability = packet.ReadUInt32("MaxDurability", indexes);
            hotfix.ItemNameDescriptionID = packet.ReadUInt16("ItemNameDescriptionID", indexes);
            hotfix.RequiredTransmogHoliday = packet.ReadUInt16("RequiredTransmogHoliday", indexes);
            hotfix.RequiredHoliday = packet.ReadUInt16("RequiredHoliday", indexes);
            hotfix.LimitCategory = packet.ReadUInt16("LimitCategory", indexes);
            hotfix.GemProperties = packet.ReadUInt16("GemProperties", indexes);
            hotfix.SocketMatchEnchantmentID = packet.ReadUInt16("SocketMatchEnchantmentID", indexes);
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
            hotfix.ItemRandomSuffixGroupID = packet.ReadUInt16("ItemRandomSuffixGroupID", indexes);
            hotfix.RandomSelect = packet.ReadUInt16("RandomSelect", indexes);
            hotfix.MinDamage = new ushort?[5];
            for (int i = 0; i < 5; i++)
                hotfix.MinDamage[i] = packet.ReadUInt16("MinDamage", indexes, i);
            hotfix.MaxDamage = new ushort?[5];
            for (int i = 0; i < 5; i++)
                hotfix.MaxDamage[i] = packet.ReadUInt16("MaxDamage", indexes, i);
            hotfix.Resistances = new short?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Resistances[i] = packet.ReadInt16("Resistances", indexes, i);
            hotfix.ScalingStatDistributionID = packet.ReadUInt16("ScalingStatDistributionID", indexes);
            hotfix.StatModifierBonusAmount = new short?[10];
            for (int i = 0; i < 10; i++)
                hotfix.StatModifierBonusAmount[i] = packet.ReadInt16("StatModifierBonusAmount", indexes, i);
            hotfix.ExpansionID = packet.ReadByte("ExpansionID", indexes);
            hotfix.ArtifactID = packet.ReadByte("ArtifactID", indexes);
            hotfix.SpellWeight = packet.ReadByte("SpellWeight", indexes);
            hotfix.SpellWeightCategory = packet.ReadByte("SpellWeightCategory", indexes);
            hotfix.SocketType = new byte?[3];
            for (int i = 0; i < 3; i++)
                hotfix.SocketType[i] = packet.ReadByte("SocketType", indexes, i);
            hotfix.SheatheType = packet.ReadByte("SheatheType", indexes);
            hotfix.Material = packet.ReadByte("Material", indexes);
            hotfix.PageMaterialID = packet.ReadByte("PageMaterialID", indexes);
            hotfix.LanguageID = packet.ReadByte("LanguageID", indexes);
            hotfix.Bonding = packet.ReadByte("Bonding", indexes);
            hotfix.DamageDamageType = packet.ReadByte("DamageDamageType", indexes);
            hotfix.StatModifierBonusStat = new sbyte?[10];
            for (int i = 0; i < 10; i++)
                hotfix.StatModifierBonusStat[i] = packet.ReadSByte("StatModifierBonusStat", indexes, i);
            hotfix.ContainerSlots = packet.ReadByte("ContainerSlots", indexes);
            hotfix.MinReputation = packet.ReadByte("MinReputation", indexes);
            hotfix.RequiredPVPMedal = packet.ReadByte("RequiredPVPMedal", indexes);
            hotfix.RequiredPVPRank = packet.ReadByte("RequiredPVPRank", indexes);
            hotfix.InventoryType = packet.ReadSByte("InventoryType", indexes);
            hotfix.OverallQualityID = packet.ReadSByte("OverallQualityID", indexes);
            hotfix.AmmunitionType = packet.ReadByte("AmmunitionType", indexes);
            hotfix.RequiredLevel = packet.ReadSByte("RequiredLevel", indexes);

            Storage.ItemSparseHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemSparseLocaleHotfix340 hotfixLocale = new ItemSparseLocaleHotfix340
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                    Display3Lang = hotfix.Display3,
                    Display2Lang = hotfix.Display2,
                    Display1Lang = hotfix.Display1,
                    DisplayLang = hotfix.Display,
                };
                Storage.ItemSparseHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemSparseHandler341(Packet packet, uint entry, params object[] indexes)
        {
            ItemSparseHotfix341 hotfix = new ItemSparseHotfix341();

            hotfix.ID = entry;
            hotfix.AllowableRace = packet.ReadInt64("AllowableRace", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.Display3 = packet.ReadCString("Display3", indexes);
            hotfix.Display2 = packet.ReadCString("Display2", indexes);
            hotfix.Display1 = packet.ReadCString("Display1", indexes);
            hotfix.Display = packet.ReadCString("Display", indexes);
            hotfix.DmgVariance = packet.ReadSingle("DmgVariance", indexes);
            hotfix.DurationInInventory = packet.ReadUInt32("DurationInInventory", indexes);
            hotfix.QualityModifier = packet.ReadSingle("QualityModifier", indexes);
            hotfix.BagFamily = packet.ReadUInt32("BagFamily", indexes);
            hotfix.StartQuestID = packet.ReadInt32("StartQuestID", indexes);
            hotfix.ItemRange = packet.ReadSingle("ItemRange", indexes);
            hotfix.StatPercentageOfSocket = new float?[10];
            for (int i = 0; i < 10; i++)
                hotfix.StatPercentageOfSocket[i] = packet.ReadSingle("StatPercentageOfSocket", indexes, i);
            hotfix.StatPercentEditor = new int?[10];
            for (int i = 0; i < 10; i++)
                hotfix.StatPercentEditor[i] = packet.ReadInt32("StatPercentEditor", indexes, i);
            hotfix.Stackable = packet.ReadInt32("Stackable", indexes);
            hotfix.MaxCount = packet.ReadInt32("MaxCount", indexes);
            hotfix.MinReputation = packet.ReadInt32("MinReputation", indexes);
            hotfix.RequiredAbility = packet.ReadUInt32("RequiredAbility", indexes);
            hotfix.SellPrice = packet.ReadUInt32("SellPrice", indexes);
            hotfix.BuyPrice = packet.ReadUInt32("BuyPrice", indexes);
            hotfix.VendorStackCount = packet.ReadUInt32("VendorStackCount", indexes);
            hotfix.PriceVariance = packet.ReadSingle("PriceVariance", indexes);
            hotfix.PriceRandomValue = packet.ReadSingle("PriceRandomValue", indexes);
            hotfix.Flags = new int?[4];
            for (int i = 0; i < 4; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);
            hotfix.FactionRelated = packet.ReadInt32("FactionRelated", indexes);
            hotfix.ModifiedCraftingReagentItemID = packet.ReadInt32("ModifiedCraftingReagentItemID", indexes);
            hotfix.ContentTuningID = packet.ReadInt32("ContentTuningID", indexes);
            hotfix.PlayerLevelToItemLevelCurveID = packet.ReadInt32("PlayerLevelToItemLevelCurveID", indexes);
            hotfix.MaxDurability = packet.ReadUInt32("MaxDurability", indexes);
            hotfix.ItemNameDescriptionID = packet.ReadUInt16("ItemNameDescriptionID", indexes);
            hotfix.RequiredTransmogHoliday = packet.ReadUInt16("RequiredTransmogHoliday", indexes);
            hotfix.RequiredHoliday = packet.ReadUInt16("RequiredHoliday", indexes);
            hotfix.LimitCategory = packet.ReadUInt16("LimitCategory", indexes);
            hotfix.GemProperties = packet.ReadUInt16("GemProperties", indexes);
            hotfix.SocketMatchEnchantmentID = packet.ReadUInt16("SocketMatchEnchantmentID", indexes);
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
            hotfix.ItemRandomSuffixGroupID = packet.ReadUInt16("ItemRandomSuffixGroupID", indexes);
            hotfix.RandomSelect = packet.ReadUInt16("RandomSelect", indexes);
            hotfix.MinDamage = new ushort?[5];
            for (int i = 0; i < 5; i++)
                hotfix.MinDamage[i] = packet.ReadUInt16("MinDamage", indexes, i);
            hotfix.MaxDamage = new ushort?[5];
            for (int i = 0; i < 5; i++)
                hotfix.MaxDamage[i] = packet.ReadUInt16("MaxDamage", indexes, i);
            hotfix.Resistances = new short?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Resistances[i] = packet.ReadInt16("Resistances", indexes, i);
            hotfix.ScalingStatDistributionID = packet.ReadUInt16("ScalingStatDistributionID", indexes);
            hotfix.StatModifierBonusAmount = new short?[10];
            for (int i = 0; i < 10; i++)
                hotfix.StatModifierBonusAmount[i] = packet.ReadInt16("StatModifierBonusAmount", indexes, i);
            hotfix.ExpansionID = packet.ReadByte("ExpansionID", indexes);
            hotfix.ArtifactID = packet.ReadByte("ArtifactID", indexes);
            hotfix.SpellWeight = packet.ReadByte("SpellWeight", indexes);
            hotfix.SpellWeightCategory = packet.ReadByte("SpellWeightCategory", indexes);
            hotfix.SocketType = new byte?[3];
            for (int i = 0; i < 3; i++)
                hotfix.SocketType[i] = packet.ReadByte("SocketType", indexes, i);
            hotfix.SheatheType = packet.ReadByte("SheatheType", indexes);
            hotfix.Material = packet.ReadByte("Material", indexes);
            hotfix.PageMaterialID = packet.ReadByte("PageMaterialID", indexes);
            hotfix.LanguageID = packet.ReadByte("LanguageID", indexes);
            hotfix.Bonding = packet.ReadByte("Bonding", indexes);
            hotfix.DamageDamageType = packet.ReadByte("DamageDamageType", indexes);
            hotfix.StatModifierBonusStat = new sbyte?[10];
            for (int i = 0; i < 10; i++)
                hotfix.StatModifierBonusStat[i] = packet.ReadSByte("StatModifierBonusStat", indexes, i);
            hotfix.ContainerSlots = packet.ReadByte("ContainerSlots", indexes);
            hotfix.RequiredPVPMedal = packet.ReadByte("RequiredPVPMedal", indexes);
            hotfix.RequiredPVPRank = packet.ReadByte("RequiredPVPRank", indexes);
            hotfix.InventoryType = packet.ReadSByte("InventoryType", indexes);
            hotfix.OverallQualityID = packet.ReadSByte("OverallQualityID", indexes);
            hotfix.AmmunitionType = packet.ReadByte("AmmunitionType", indexes);
            hotfix.RequiredLevel = packet.ReadSByte("RequiredLevel", indexes);

            Storage.ItemSparseHotfixes341.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemSparseLocaleHotfix341 hotfixLocale = new ItemSparseLocaleHotfix341
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                    Display3Lang = hotfix.Display3,
                    Display2Lang = hotfix.Display2,
                    Display1Lang = hotfix.Display1,
                    DisplayLang = hotfix.Display,
                };
                Storage.ItemSparseHotfixesLocale341.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemSpecHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemSpecHotfix340 hotfix = new ItemSpecHotfix340();

            hotfix.ID = entry;
            hotfix.MinLevel = packet.ReadByte("MinLevel", indexes);
            hotfix.MaxLevel = packet.ReadByte("MaxLevel", indexes);
            hotfix.ItemType = packet.ReadByte("ItemType", indexes);
            hotfix.PrimaryStat = packet.ReadByte("PrimaryStat", indexes);
            hotfix.SecondaryStat = packet.ReadByte("SecondaryStat", indexes);
            hotfix.SpecializationID = packet.ReadUInt16("SpecializationID", indexes);

            Storage.ItemSpecHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemSpecOverrideHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemSpecOverrideHotfix340 hotfix = new ItemSpecOverrideHotfix340();

            hotfix.ID = entry;
            hotfix.SpecID = packet.ReadUInt16("SpecID", indexes);
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);

            Storage.ItemSpecOverrideHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemXBonusTreeHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ItemXBonusTreeHotfix340 hotfix = new ItemXBonusTreeHotfix340();

            hotfix.ID = entry;
            hotfix.ItemBonusTreeID = packet.ReadUInt16("ItemBonusTreeID", indexes);
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);

            Storage.ItemXBonusTreeHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void JournalEncounterHandler340(Packet packet, uint entry, params object[] indexes)
        {
            JournalEncounterHotfix340 hotfix = new JournalEncounterHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.MapX = packet.ReadSingle("MapX", indexes);
            hotfix.MapY = packet.ReadSingle("MapY", indexes);
            hotfix.JournalInstanceID = packet.ReadUInt16("JournalInstanceID", indexes);
            hotfix.OrderIndex = packet.ReadUInt32("OrderIndex", indexes);
            hotfix.FirstSectionID = packet.ReadUInt16("FirstSectionID", indexes);
            hotfix.UiMapID = packet.ReadUInt16("UiMapID", indexes);
            hotfix.MapDisplayConditionID = packet.ReadUInt32("MapDisplayConditionID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.DifficultyMask = packet.ReadSByte("DifficultyMask", indexes);

            Storage.JournalEncounterHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                JournalEncounterLocaleHotfix340 hotfixLocale = new JournalEncounterLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.JournalEncounterHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void JournalEncounterSectionHandler340(Packet packet, uint entry, params object[] indexes)
        {
            JournalEncounterSectionHotfix340 hotfix = new JournalEncounterSectionHotfix340();

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

            Storage.JournalEncounterSectionHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                JournalEncounterSectionLocaleHotfix340 hotfixLocale = new JournalEncounterSectionLocaleHotfix340
                {
                    ID = hotfix.ID,
                    TitleLang = hotfix.Title,
                    BodyTextLang = hotfix.BodyText,
                };
                Storage.JournalEncounterSectionHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void JournalInstanceHandler340(Packet packet, uint entry, params object[] indexes)
        {
            JournalInstanceHotfix340 hotfix = new JournalInstanceHotfix340();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.MapID = packet.ReadUInt16("MapID", indexes);
            hotfix.BackgroundFileDataID = packet.ReadInt32("BackgroundFileDataID", indexes);
            hotfix.ButtonFileDataID = packet.ReadInt32("ButtonFileDataID", indexes);
            hotfix.ButtonSmallFileDataID = packet.ReadInt32("ButtonSmallFileDataID", indexes);
            hotfix.LoreFileDataID = packet.ReadInt32("LoreFileDataID", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.AreaID = packet.ReadUInt16("AreaID", indexes);

            Storage.JournalInstanceHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                JournalInstanceLocaleHotfix340 hotfixLocale = new JournalInstanceLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.JournalInstanceHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void JournalInstanceHandler341(Packet packet, uint entry, params object[] indexes)
        {
            JournalInstanceHotfix341 hotfix = new JournalInstanceHotfix341();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.MapID = packet.ReadUInt16("MapID", indexes);
            hotfix.BackgroundFileDataID = packet.ReadInt32("BackgroundFileDataID", indexes);
            hotfix.ButtonFileDataID = packet.ReadInt32("ButtonFileDataID", indexes);
            hotfix.ButtonSmallFileDataID = packet.ReadInt32("ButtonSmallFileDataID", indexes);
            hotfix.LoreFileDataID = packet.ReadInt32("LoreFileDataID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.AreaID = packet.ReadUInt16("AreaID", indexes);

            Storage.JournalInstanceHotfixes341.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                JournalInstanceLocaleHotfix341 hotfixLocale = new JournalInstanceLocaleHotfix341
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.JournalInstanceHotfixesLocale341.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void JournalTierHandler340(Packet packet, uint entry, params object[] indexes)
        {
            JournalTierHotfix340 hotfix = new JournalTierHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);

            Storage.JournalTierHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                JournalTierLocaleHotfix340 hotfixLocale = new JournalTierLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.JournalTierHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void KeychainHandler340(Packet packet, uint entry, params object[] indexes)
        {
            KeychainHotfix340 hotfix = new KeychainHotfix340();

            hotfix.ID = entry;
            hotfix.Key = new byte?[32];
            for (int i = 0; i < 32; i++)
                hotfix.Key[i] = packet.ReadByte("Key", indexes, i);

            Storage.KeychainHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void KeystoneAffixHandler340(Packet packet, uint entry, params object[] indexes)
        {
            KeystoneAffixHotfix340 hotfix = new KeystoneAffixHotfix340();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.FiledataID = packet.ReadInt32("FiledataID", indexes);

            Storage.KeystoneAffixHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                KeystoneAffixLocaleHotfix340 hotfixLocale = new KeystoneAffixLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.KeystoneAffixHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void LanguageWordsHandler340(Packet packet, uint entry, params object[] indexes)
        {
            LanguageWordsHotfix340 hotfix = new LanguageWordsHotfix340();

            hotfix.ID = entry;
            hotfix.Word = packet.ReadCString("Word", indexes);
            hotfix.LanguageID = packet.ReadByte("LanguageID", indexes);

            Storage.LanguageWordsHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void LanguagesHandler340(Packet packet, uint entry, params object[] indexes)
        {
            LanguagesHotfix340 hotfix = new LanguagesHotfix340();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.UiTextureKitID = packet.ReadInt32("UiTextureKitID", indexes);
            hotfix.UiTextureKitElementCount = packet.ReadInt32("UiTextureKitElementCount", indexes);

            Storage.LanguagesHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                LanguagesLocaleHotfix340 hotfixLocale = new LanguagesLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.LanguagesHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void LanguagesHandler342(Packet packet, uint entry, params object[] indexes)
        {
            LanguagesHotfix342 hotfix = new LanguagesHotfix342();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.UiTextureKitID = packet.ReadInt32("UiTextureKitID", indexes);
            hotfix.UiTextureKitElementCount = packet.ReadInt32("UiTextureKitElementCount", indexes);
            hotfix.LearningCurveID = packet.ReadInt32("LearningCurveID", indexes);

            Storage.LanguagesHotfixes342.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                LanguagesLocaleHotfix342 hotfixLocale = new LanguagesLocaleHotfix342
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.LanguagesHotfixesLocale342.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void LfgDungeonsHandler340(Packet packet, uint entry, params object[] indexes)
        {
            LfgDungeonsHotfix340 hotfix = new LfgDungeonsHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.MinLevel = packet.ReadByte("MinLevel", indexes);
            hotfix.MaxLevel = packet.ReadUInt16("MaxLevel", indexes);
            hotfix.TypeID = packet.ReadByte("TypeID", indexes);
            hotfix.Subtype = packet.ReadByte("Subtype", indexes);
            hotfix.Faction = packet.ReadSByte("Faction", indexes);
            hotfix.IconTextureFileID = packet.ReadInt32("IconTextureFileID", indexes);
            hotfix.RewardsBgTextureFileID = packet.ReadInt32("RewardsBgTextureFileID", indexes);
            hotfix.PopupBgTextureFileID = packet.ReadInt32("PopupBgTextureFileID", indexes);
            hotfix.ExpansionLevel = packet.ReadByte("ExpansionLevel", indexes);
            hotfix.MapID = packet.ReadInt16("MapID", indexes);
            hotfix.DifficultyID = packet.ReadByte("DifficultyID", indexes);
            hotfix.MinGear = packet.ReadSingle("MinGear", indexes);
            hotfix.GroupID = packet.ReadByte("GroupID", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);
            hotfix.RequiredPlayerConditionID = packet.ReadUInt32("RequiredPlayerConditionID", indexes);
            hotfix.TargetLevel = packet.ReadByte("TargetLevel", indexes);
            hotfix.TargetLevelMin = packet.ReadByte("TargetLevelMin", indexes);
            hotfix.TargetLevelMax = packet.ReadUInt16("TargetLevelMax", indexes);
            hotfix.RandomID = packet.ReadUInt16("RandomID", indexes);
            hotfix.ScenarioID = packet.ReadUInt16("ScenarioID", indexes);
            hotfix.FinalEncounterID = packet.ReadUInt16("FinalEncounterID", indexes);
            hotfix.CountTank = packet.ReadByte("CountTank", indexes);
            hotfix.CountHealer = packet.ReadByte("CountHealer", indexes);
            hotfix.CountDamage = packet.ReadByte("CountDamage", indexes);
            hotfix.MinCountTank = packet.ReadByte("MinCountTank", indexes);
            hotfix.MinCountHealer = packet.ReadByte("MinCountHealer", indexes);
            hotfix.MinCountDamage = packet.ReadByte("MinCountDamage", indexes);
            hotfix.BonusReputationAmount = packet.ReadUInt16("BonusReputationAmount", indexes);
            hotfix.MentorItemLevel = packet.ReadUInt16("MentorItemLevel", indexes);
            hotfix.MentorCharLevel = packet.ReadByte("MentorCharLevel", indexes);
            hotfix.Flags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.LfgDungeonsHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                LfgDungeonsLocaleHotfix340 hotfixLocale = new LfgDungeonsLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.LfgDungeonsHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void LfgDungeonsHandler341(Packet packet, uint entry, params object[] indexes)
        {
            LfgDungeonsHotfix341 hotfix = new LfgDungeonsHotfix341();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.MinLevel = packet.ReadByte("MinLevel", indexes);
            hotfix.MaxLevel = packet.ReadUInt16("MaxLevel", indexes);
            hotfix.TypeID = packet.ReadByte("TypeID", indexes);
            hotfix.Subtype = packet.ReadByte("Subtype", indexes);
            hotfix.Faction = packet.ReadSByte("Faction", indexes);
            hotfix.IconTextureFileID = packet.ReadInt32("IconTextureFileID", indexes);
            hotfix.RewardsBgTextureFileID = packet.ReadInt32("RewardsBgTextureFileID", indexes);
            hotfix.PopupBgTextureFileID = packet.ReadInt32("PopupBgTextureFileID", indexes);
            hotfix.ExpansionLevel = packet.ReadByte("ExpansionLevel", indexes);
            hotfix.MapID = packet.ReadInt16("MapID", indexes);
            hotfix.DifficultyID = packet.ReadByte("DifficultyID", indexes);
            hotfix.MinGear = packet.ReadSingle("MinGear", indexes);
            hotfix.GroupID = packet.ReadByte("GroupID", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);
            hotfix.RequiredPlayerConditionID = packet.ReadUInt32("RequiredPlayerConditionID", indexes);
            hotfix.TargetLevel = packet.ReadByte("TargetLevel", indexes);
            hotfix.TargetLevelMin = packet.ReadByte("TargetLevelMin", indexes);
            hotfix.TargetLevelMax = packet.ReadUInt16("TargetLevelMax", indexes);
            hotfix.RandomID = packet.ReadUInt16("RandomID", indexes);
            hotfix.ScenarioID = packet.ReadUInt16("ScenarioID", indexes);
            hotfix.FinalEncounterID = packet.ReadUInt16("FinalEncounterID", indexes);
            hotfix.CountTank = packet.ReadByte("CountTank", indexes);
            hotfix.CountHealer = packet.ReadByte("CountHealer", indexes);
            hotfix.CountDamage = packet.ReadByte("CountDamage", indexes);
            hotfix.MinCountTank = packet.ReadByte("MinCountTank", indexes);
            hotfix.MinCountHealer = packet.ReadByte("MinCountHealer", indexes);
            hotfix.MinCountDamage = packet.ReadByte("MinCountDamage", indexes);
            hotfix.BonusReputationAmount = packet.ReadUInt16("BonusReputationAmount", indexes);
            hotfix.MentorItemLevel = packet.ReadUInt16("MentorItemLevel", indexes);
            hotfix.MentorCharLevel = packet.ReadByte("MentorCharLevel", indexes);
            hotfix.Flags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.LfgDungeonsHotfixes341.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                LfgDungeonsLocaleHotfix341 hotfixLocale = new LfgDungeonsLocaleHotfix341
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.LfgDungeonsHotfixesLocale341.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void LightHandler340(Packet packet, uint entry, params object[] indexes)
        {
            LightHotfix340 hotfix = new LightHotfix340();

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

            Storage.LightHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void LiquidTypeHandler340(Packet packet, uint entry, params object[] indexes)
        {
            LiquidTypeHotfix340 hotfix = new LiquidTypeHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Texture = new string[6];
            for (int i = 0; i < 6; i++)
                hotfix.Texture[i] = packet.ReadCString("Texture", indexes, i);
            hotfix.Flags = packet.ReadUInt16("Flags", indexes);
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
            hotfix.Color = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Color[i] = packet.ReadInt32("Color", indexes, i);
            hotfix.Float = new float?[18];
            for (int i = 0; i < 18; i++)
                hotfix.Float[i] = packet.ReadSingle("Float", indexes, i);
            hotfix.Int = new uint?[4];
            for (int i = 0; i < 4; i++)
                hotfix.Int[i] = packet.ReadUInt32("Int", indexes, i);
            hotfix.Coefficient = new float?[4];
            for (int i = 0; i < 4; i++)
                hotfix.Coefficient[i] = packet.ReadSingle("Coefficient", indexes, i);

            Storage.LiquidTypeHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void LockHandler340(Packet packet, uint entry, params object[] indexes)
        {
            LockHotfix340 hotfix = new LockHotfix340();

            hotfix.ID = entry;
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

            Storage.LockHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void MailTemplateHandler340(Packet packet, uint entry, params object[] indexes)
        {
            MailTemplateHotfix340 hotfix = new MailTemplateHotfix340();

            hotfix.ID = entry;
            hotfix.Body = packet.ReadCString("Body", indexes);

            Storage.MailTemplateHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                MailTemplateLocaleHotfix340 hotfixLocale = new MailTemplateLocaleHotfix340
                {
                    ID = hotfix.ID,
                    BodyLang = hotfix.Body,
                };
                Storage.MailTemplateHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void MapHandler340(Packet packet, uint entry, params object[] indexes)
        {
            MapHotfix340 hotfix = new MapHotfix340();

            hotfix.ID = entry;
            hotfix.Directory = packet.ReadCString("Directory", indexes);
            hotfix.MapName = packet.ReadCString("MapName", indexes);
            hotfix.MapDescription0 = packet.ReadCString("MapDescription0", indexes);
            hotfix.MapDescription1 = packet.ReadCString("MapDescription1", indexes);
            hotfix.PvpShortDescription = packet.ReadCString("PvpShortDescription", indexes);
            hotfix.PvpLongDescription = packet.ReadCString("PvpLongDescription", indexes);
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
            hotfix.RaidOffset = packet.ReadInt32("RaidOffset", indexes);
            hotfix.CorpseMapID = packet.ReadInt16("CorpseMapID", indexes);
            hotfix.MaxPlayers = packet.ReadByte("MaxPlayers", indexes);
            hotfix.WindSettingsID = packet.ReadInt16("WindSettingsID", indexes);
            hotfix.ZmpFileDataID = packet.ReadInt32("ZmpFileDataID", indexes);
            hotfix.Flags = new int?[3];
            for (int i = 0; i < 3; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.MapHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                MapLocaleHotfix340 hotfixLocale = new MapLocaleHotfix340
                {
                    ID = hotfix.ID,
                    MapNameLang = hotfix.MapName,
                    MapDescription0Lang = hotfix.MapDescription0,
                    MapDescription1Lang = hotfix.MapDescription1,
                    PvpShortDescriptionLang = hotfix.PvpShortDescription,
                    PvpLongDescriptionLang = hotfix.PvpLongDescription,
                };
                Storage.MapHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void MapChallengeModeHandler340(Packet packet, uint entry, params object[] indexes)
        {
            MapChallengeModeHotfix340 hotfix = new MapChallengeModeHotfix340();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.MapID = packet.ReadUInt16("MapID", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.ExpansionLevel = packet.ReadUInt32("ExpansionLevel", indexes);
            hotfix.RequiredWorldStateID = packet.ReadInt32("RequiredWorldStateID", indexes);
            hotfix.CriteriaCount = new short?[3];
            for (int i = 0; i < 3; i++)
                hotfix.CriteriaCount[i] = packet.ReadInt16("CriteriaCount", indexes, i);

            Storage.MapChallengeModeHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                MapChallengeModeLocaleHotfix340 hotfixLocale = new MapChallengeModeLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.MapChallengeModeHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void MapDifficultyHandler340(Packet packet, uint entry, params object[] indexes)
        {
            MapDifficultyHotfix340 hotfix = new MapDifficultyHotfix340();

            hotfix.ID = entry;
            hotfix.Message = packet.ReadCString("Message", indexes);
            hotfix.ItemContextPickerID = packet.ReadUInt32("ItemContextPickerID", indexes);
            hotfix.ContentTuningID = packet.ReadInt32("ContentTuningID", indexes);
            hotfix.DifficultyID = packet.ReadByte("DifficultyID", indexes);
            hotfix.LockID = packet.ReadByte("LockID", indexes);
            hotfix.ResetInterval = packet.ReadByte("ResetInterval", indexes);
            hotfix.MaxPlayers = packet.ReadByte("MaxPlayers", indexes);
            hotfix.ItemContext = packet.ReadByte("ItemContext", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.MapID = packet.ReadInt32("MapID", indexes);

            Storage.MapDifficultyHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                MapDifficultyLocaleHotfix340 hotfixLocale = new MapDifficultyLocaleHotfix340
                {
                    ID = hotfix.ID,
                    MessageLang = hotfix.Message,
                };
                Storage.MapDifficultyHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void MapDifficultyXConditionHandler340(Packet packet, uint entry, params object[] indexes)
        {
            MapDifficultyXConditionHotfix340 hotfix = new MapDifficultyXConditionHotfix340();

            hotfix.ID = entry;
            hotfix.FailureDescription = packet.ReadCString("FailureDescription", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.MapDifficultyID = packet.ReadInt32("MapDifficultyID", indexes);

            Storage.MapDifficultyXConditionHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                MapDifficultyXConditionLocaleHotfix340 hotfixLocale = new MapDifficultyXConditionLocaleHotfix340
                {
                    ID = hotfix.ID,
                    FailureDescriptionLang = hotfix.FailureDescription,
                };
                Storage.MapDifficultyXConditionHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ModifierTreeHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ModifierTreeHotfix340 hotfix = new ModifierTreeHotfix340();

            hotfix.ID = entry;
            hotfix.Parent = packet.ReadUInt32("Parent", indexes);
            hotfix.Operator = packet.ReadSByte("Operator", indexes);
            hotfix.Amount = packet.ReadSByte("Amount", indexes);
            hotfix.Type = packet.ReadInt32("Type", indexes);
            hotfix.Asset = packet.ReadInt32("Asset", indexes);
            hotfix.SecondaryAsset = packet.ReadInt32("SecondaryAsset", indexes);
            hotfix.TertiaryAsset = packet.ReadSByte("TertiaryAsset", indexes);

            Storage.ModifierTreeHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ModifierTreeHandler343(Packet packet, uint entry, params object[] indexes)
        {
            ModifierTreeHotfix343 hotfix = new ModifierTreeHotfix343();

            hotfix.ID = entry;
            hotfix.Parent = packet.ReadUInt32("Parent", indexes);
            hotfix.Operator = packet.ReadSByte("Operator", indexes);
            hotfix.Amount = packet.ReadSByte("Amount", indexes);
            hotfix.Type = packet.ReadInt32("Type", indexes);
            hotfix.Asset = packet.ReadInt32("Asset", indexes);
            hotfix.SecondaryAsset = packet.ReadInt32("SecondaryAsset", indexes);
            hotfix.TertiaryAsset = packet.ReadSByte("TertiaryAsset", indexes);

            Storage.ModifierTreeHotfixes343.Add(hotfix, packet.TimeSpan);
        }

        public static void MountHandler340(Packet packet, uint entry, params object[] indexes)
        {
            MountHotfix340 hotfix = new MountHotfix340();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.SourceText = packet.ReadCString("SourceText", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.MountTypeID = packet.ReadUInt16("MountTypeID", indexes);
            hotfix.Flags = packet.ReadUInt16("Flags", indexes);
            hotfix.SourceTypeEnum = packet.ReadSByte("SourceTypeEnum", indexes);
            hotfix.SourceSpellID = packet.ReadInt32("SourceSpellID", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.MountFlyRideHeight = packet.ReadSingle("MountFlyRideHeight", indexes);
            hotfix.UiModelSceneID = packet.ReadInt32("UiModelSceneID", indexes);

            Storage.MountHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                MountLocaleHotfix340 hotfixLocale = new MountLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    SourceTextLang = hotfix.SourceText,
                    DescriptionLang = hotfix.Description,
                };
                Storage.MountHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void MountCapabilityHandler340(Packet packet, uint entry, params object[] indexes)
        {
            MountCapabilityHotfix340 hotfix = new MountCapabilityHotfix340();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.ReqRidingSkill = packet.ReadUInt16("ReqRidingSkill", indexes);
            hotfix.ReqAreaID = packet.ReadUInt16("ReqAreaID", indexes);
            hotfix.ReqSpellAuraID = packet.ReadUInt32("ReqSpellAuraID", indexes);
            hotfix.ReqSpellKnownID = packet.ReadInt32("ReqSpellKnownID", indexes);
            hotfix.ModSpellAuraID = packet.ReadInt32("ModSpellAuraID", indexes);
            hotfix.ReqMapID = packet.ReadInt16("ReqMapID", indexes);

            Storage.MountCapabilityHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void MountTypeXCapabilityHandler340(Packet packet, uint entry, params object[] indexes)
        {
            MountTypeXCapabilityHotfix340 hotfix = new MountTypeXCapabilityHotfix340();

            hotfix.ID = entry;
            hotfix.MountTypeID = packet.ReadUInt16("MountTypeID", indexes);
            hotfix.MountCapabilityID = packet.ReadUInt16("MountCapabilityID", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);

            Storage.MountTypeXCapabilityHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void MountXDisplayHandler340(Packet packet, uint entry, params object[] indexes)
        {
            MountXDisplayHotfix340 hotfix = new MountXDisplayHotfix340();

            hotfix.ID = entry;
            hotfix.CreatureDisplayInfoID = packet.ReadInt32("CreatureDisplayInfoID", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.MountID = packet.ReadInt32("MountID", indexes);

            Storage.MountXDisplayHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void MovieHandler340(Packet packet, uint entry, params object[] indexes)
        {
            MovieHotfix340 hotfix = new MovieHotfix340();

            hotfix.ID = entry;
            hotfix.Volume = packet.ReadByte("Volume", indexes);
            hotfix.KeyID = packet.ReadByte("KeyID", indexes);
            hotfix.AudioFileDataID = packet.ReadUInt32("AudioFileDataID", indexes);
            hotfix.SubtitleFileDataID = packet.ReadUInt32("SubtitleFileDataID", indexes);

            Storage.MovieHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void MythicPlusSeasonHandler342(Packet packet, uint entry, params object[] indexes)
        {
            MythicPlusSeasonHotfix342 hotfix = new MythicPlusSeasonHotfix342();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.MilestoneSeason = packet.ReadInt32("MilestoneSeason", indexes);
            hotfix.ExpansionLevel = packet.ReadInt32("ExpansionLevel", indexes);
            hotfix.HeroicLFGDungeonMinGear = packet.ReadInt32("HeroicLFGDungeonMinGear", indexes);

            Storage.MythicPlusSeasonHotfixes342.Add(hotfix, packet.TimeSpan);
        }

        public static void NameGenHandler340(Packet packet, uint entry, params object[] indexes)
        {
            NameGenHotfix340 hotfix = new NameGenHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.RaceID = packet.ReadByte("RaceID", indexes);
            hotfix.Sex = packet.ReadByte("Sex", indexes);

            Storage.NameGenHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void NamesProfanityHandler340(Packet packet, uint entry, params object[] indexes)
        {
            NamesProfanityHotfix340 hotfix = new NamesProfanityHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Language = packet.ReadSByte("Language", indexes);

            Storage.NamesProfanityHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void NamesReservedHandler340(Packet packet, uint entry, params object[] indexes)
        {
            NamesReservedHotfix340 hotfix = new NamesReservedHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);

            Storage.NamesReservedHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void NamesReservedLocaleHandler340(Packet packet, uint entry, params object[] indexes)
        {
            NamesReservedLocaleHotfix340 hotfix = new NamesReservedLocaleHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.LocaleMask = packet.ReadByte("LocaleMask", indexes);

            Storage.NamesReservedLocaleHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void NumTalentsAtLevelHandler340(Packet packet, uint entry, params object[] indexes)
        {
            NumTalentsAtLevelHotfix340 hotfix = new NumTalentsAtLevelHotfix340();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.NumTalents = packet.ReadInt32("NumTalents", indexes);
            hotfix.NumTalentsDeathKnight = packet.ReadInt32("NumTalentsDeathKnight", indexes);
            hotfix.NumTalentsDemonHunter = packet.ReadInt32("NumTalentsDemonHunter", indexes);

            Storage.NumTalentsAtLevelHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void OverrideSpellDataHandler340(Packet packet, uint entry, params object[] indexes)
        {
            OverrideSpellDataHotfix340 hotfix = new OverrideSpellDataHotfix340();

            hotfix.ID = entry;
            hotfix.Spells = new int?[10];
            for (int i = 0; i < 10; i++)
                hotfix.Spells[i] = packet.ReadInt32("Spells", indexes, i);
            hotfix.PlayerActionbarFileDataID = packet.ReadInt32("PlayerActionbarFileDataID", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);

            Storage.OverrideSpellDataHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ParagonReputationHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ParagonReputationHotfix340 hotfix = new ParagonReputationHotfix340();

            hotfix.ID = entry;
            hotfix.FactionID = packet.ReadInt32("FactionID", indexes);
            hotfix.LevelThreshold = packet.ReadInt32("LevelThreshold", indexes);
            hotfix.QuestID = packet.ReadInt32("QuestID", indexes);

            Storage.ParagonReputationHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void PhaseHandler340(Packet packet, uint entry, params object[] indexes)
        {
            PhaseHotfix340 hotfix = new PhaseHotfix340();

            hotfix.ID = entry;
            hotfix.Flags = packet.ReadUInt16("Flags", indexes);

            Storage.PhaseHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void PhaseXPhaseGroupHandler340(Packet packet, uint entry, params object[] indexes)
        {
            PhaseXPhaseGroupHotfix340 hotfix = new PhaseXPhaseGroupHotfix340();

            hotfix.ID = entry;
            hotfix.PhaseID = packet.ReadUInt16("PhaseID", indexes);
            hotfix.PhaseGroupID = packet.ReadInt32("PhaseGroupID", indexes);

            Storage.PhaseXPhaseGroupHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void PlayerConditionHandler340(Packet packet, uint entry, params object[] indexes)
        {
            PlayerConditionHotfix340 hotfix = new PlayerConditionHotfix340();

            hotfix.RaceMask = packet.ReadInt64("RaceMask", indexes);
            hotfix.FailureDescription = packet.ReadCString("FailureDescription", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.MinLevel = packet.ReadUInt16("MinLevel", indexes);
            hotfix.MaxLevel = packet.ReadUInt16("MaxLevel", indexes);
            hotfix.ClassMask = packet.ReadInt32("ClassMask", indexes);
            hotfix.SkillLogic = packet.ReadUInt32("SkillLogic", indexes);
            hotfix.LanguageID = packet.ReadByte("LanguageID", indexes);
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
            hotfix.ItemFlags = packet.ReadByte("ItemFlags", indexes);
            hotfix.AuraSpellLogic = packet.ReadUInt32("AuraSpellLogic", indexes);
            hotfix.WorldStateExpressionID = packet.ReadUInt16("WorldStateExpressionID", indexes);
            hotfix.WeatherID = packet.ReadByte("WeatherID", indexes);
            hotfix.PartyStatus = packet.ReadByte("PartyStatus", indexes);
            hotfix.LifetimeMaxPVPRank = packet.ReadByte("LifetimeMaxPVPRank", indexes);
            hotfix.AchievementLogic = packet.ReadUInt32("AchievementLogic", indexes);
            hotfix.Gender = packet.ReadSByte("Gender", indexes);
            hotfix.NativeGender = packet.ReadSByte("NativeGender", indexes);
            hotfix.AreaLogic = packet.ReadUInt32("AreaLogic", indexes);
            hotfix.LfgLogic = packet.ReadUInt32("LfgLogic", indexes);
            hotfix.CurrencyLogic = packet.ReadUInt32("CurrencyLogic", indexes);
            hotfix.QuestKillID = packet.ReadUInt32("QuestKillID", indexes);
            hotfix.QuestKillLogic = packet.ReadUInt32("QuestKillLogic", indexes);
            hotfix.MinExpansionLevel = packet.ReadSByte("MinExpansionLevel", indexes);
            hotfix.MaxExpansionLevel = packet.ReadSByte("MaxExpansionLevel", indexes);
            hotfix.MinAvgItemLevel = packet.ReadInt32("MinAvgItemLevel", indexes);
            hotfix.MaxAvgItemLevel = packet.ReadInt32("MaxAvgItemLevel", indexes);
            hotfix.MinAvgEquippedItemLevel = packet.ReadUInt16("MinAvgEquippedItemLevel", indexes);
            hotfix.MaxAvgEquippedItemLevel = packet.ReadUInt16("MaxAvgEquippedItemLevel", indexes);
            hotfix.PhaseUseFlags = packet.ReadByte("PhaseUseFlags", indexes);
            hotfix.PhaseID = packet.ReadUInt16("PhaseID", indexes);
            hotfix.PhaseGroupID = packet.ReadUInt32("PhaseGroupID", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.ChrSpecializationIndex = packet.ReadSByte("ChrSpecializationIndex", indexes);
            hotfix.ChrSpecializationRole = packet.ReadSByte("ChrSpecializationRole", indexes);
            hotfix.ModifierTreeID = packet.ReadUInt32("ModifierTreeID", indexes);
            hotfix.PowerType = packet.ReadSByte("PowerType", indexes);
            hotfix.PowerTypeComp = packet.ReadByte("PowerTypeComp", indexes);
            hotfix.PowerTypeValue = packet.ReadByte("PowerTypeValue", indexes);
            hotfix.WeaponSubclassMask = packet.ReadInt32("WeaponSubclassMask", indexes);
            hotfix.MaxGuildLevel = packet.ReadByte("MaxGuildLevel", indexes);
            hotfix.MinGuildLevel = packet.ReadByte("MinGuildLevel", indexes);
            hotfix.MaxExpansionTier = packet.ReadSByte("MaxExpansionTier", indexes);
            hotfix.MinExpansionTier = packet.ReadSByte("MinExpansionTier", indexes);
            hotfix.MinPVPRank = packet.ReadByte("MinPVPRank", indexes);
            hotfix.MaxPVPRank = packet.ReadByte("MaxPVPRank", indexes);
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
            hotfix.PrevQuestID = new uint?[4];
            for (int i = 0; i < 4; i++)
                hotfix.PrevQuestID[i] = packet.ReadUInt32("PrevQuestID", indexes, i);
            hotfix.CurrQuestID = new uint?[4];
            for (int i = 0; i < 4; i++)
                hotfix.CurrQuestID[i] = packet.ReadUInt32("CurrQuestID", indexes, i);
            hotfix.CurrentCompletedQuestID = new uint?[4];
            for (int i = 0; i < 4; i++)
                hotfix.CurrentCompletedQuestID[i] = packet.ReadUInt32("CurrentCompletedQuestID", indexes, i);
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
            hotfix.Achievement = new ushort?[4];
            for (int i = 0; i < 4; i++)
                hotfix.Achievement[i] = packet.ReadUInt16("Achievement", indexes, i);
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

            Storage.PlayerConditionHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                PlayerConditionLocaleHotfix340 hotfixLocale = new PlayerConditionLocaleHotfix340
                {
                    ID = hotfix.ID,
                    FailureDescriptionLang = hotfix.FailureDescription,
                };
                Storage.PlayerConditionHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void PowerDisplayHandler340(Packet packet, uint entry, params object[] indexes)
        {
            PowerDisplayHotfix340 hotfix = new PowerDisplayHotfix340();

            hotfix.ID = entry;
            hotfix.GlobalStringBaseTag = packet.ReadCString("GlobalStringBaseTag", indexes);
            hotfix.ActualType = packet.ReadByte("ActualType", indexes);
            hotfix.Red = packet.ReadByte("Red", indexes);
            hotfix.Green = packet.ReadByte("Green", indexes);
            hotfix.Blue = packet.ReadByte("Blue", indexes);

            Storage.PowerDisplayHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void PowerTypeHandler340(Packet packet, uint entry, params object[] indexes)
        {
            PowerTypeHotfix340 hotfix = new PowerTypeHotfix340();

            hotfix.ID = entry;
            hotfix.NameGlobalStringTag = packet.ReadCString("NameGlobalStringTag", indexes);
            hotfix.CostGlobalStringTag = packet.ReadCString("CostGlobalStringTag", indexes);
            hotfix.PowerTypeEnum = packet.ReadSByte("PowerTypeEnum", indexes);
            hotfix.MinPower = packet.ReadSByte("MinPower", indexes);
            hotfix.MaxBasePower = packet.ReadUInt32("MaxBasePower", indexes);
            hotfix.CenterPower = packet.ReadSByte("CenterPower", indexes);
            hotfix.DefaultPower = packet.ReadSByte("DefaultPower", indexes);
            hotfix.DisplayModifier = packet.ReadUInt16("DisplayModifier", indexes);
            hotfix.RegenInterruptTimeMS = packet.ReadInt16("RegenInterruptTimeMS", indexes);
            hotfix.RegenPeace = packet.ReadSingle("RegenPeace", indexes);
            hotfix.RegenCombat = packet.ReadSingle("RegenCombat", indexes);
            hotfix.Flags = packet.ReadInt16("Flags", indexes);

            Storage.PowerTypeHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void PowerTypeHandler341(Packet packet, uint entry, params object[] indexes)
        {
            PowerTypeHotfix341 hotfix = new PowerTypeHotfix341();

            hotfix.ID = entry;
            hotfix.NameGlobalStringTag = packet.ReadCString("NameGlobalStringTag", indexes);
            hotfix.CostGlobalStringTag = packet.ReadCString("CostGlobalStringTag", indexes);
            hotfix.PowerTypeEnum = packet.ReadSByte("PowerTypeEnum", indexes);
            hotfix.MinPower = packet.ReadInt32("MinPower", indexes);
            hotfix.MaxBasePower = packet.ReadInt32("MaxBasePower", indexes);
            hotfix.CenterPower = packet.ReadInt32("CenterPower", indexes);
            hotfix.DefaultPower = packet.ReadInt32("DefaultPower", indexes);
            hotfix.DisplayModifier = packet.ReadInt32("DisplayModifier", indexes);
            hotfix.RegenInterruptTimeMS = packet.ReadInt32("RegenInterruptTimeMS", indexes);
            hotfix.RegenPeace = packet.ReadSingle("RegenPeace", indexes);
            hotfix.RegenCombat = packet.ReadSingle("RegenCombat", indexes);
            hotfix.Flags = packet.ReadInt16("Flags", indexes);

            Storage.PowerTypeHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void PrestigeLevelInfoHandler340(Packet packet, uint entry, params object[] indexes)
        {
            PrestigeLevelInfoHotfix340 hotfix = new PrestigeLevelInfoHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.PrestigeLevel = packet.ReadInt32("PrestigeLevel", indexes);
            hotfix.BadgeTextureFileDataID = packet.ReadInt32("BadgeTextureFileDataID", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.AwardedAchievementID = packet.ReadInt32("AwardedAchievementID", indexes);

            Storage.PrestigeLevelInfoHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                PrestigeLevelInfoLocaleHotfix340 hotfixLocale = new PrestigeLevelInfoLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.PrestigeLevelInfoHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void PvpDifficultyHandler340(Packet packet, uint entry, params object[] indexes)
        {
            PvpDifficultyHotfix340 hotfix = new PvpDifficultyHotfix340();

            hotfix.ID = entry;
            hotfix.RangeIndex = packet.ReadByte("RangeIndex", indexes);
            hotfix.MinLevel = packet.ReadByte("MinLevel", indexes);
            hotfix.MaxLevel = packet.ReadByte("MaxLevel", indexes);
            hotfix.MapID = packet.ReadInt32("MapID", indexes);

            Storage.PvpDifficultyHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void PvpItemHandler340(Packet packet, uint entry, params object[] indexes)
        {
            PvpItemHotfix340 hotfix = new PvpItemHotfix340();

            hotfix.ID = entry;
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);
            hotfix.ItemLevelDelta = packet.ReadByte("ItemLevelDelta", indexes);

            Storage.PvpItemHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void PvpSeasonHandler342(Packet packet, uint entry, params object[] indexes)
        {
            PvpSeasonHotfix342 hotfix = new PvpSeasonHotfix342();

            hotfix.ID = entry;
            hotfix.MilestoneSeason = packet.ReadInt32("MilestoneSeason", indexes);
            hotfix.AllianceAchievementID = packet.ReadInt32("AllianceAchievementID", indexes);
            hotfix.HordeAchievementID = packet.ReadInt32("HordeAchievementID", indexes);

            Storage.PvpSeasonHotfixes342.Add(hotfix, packet.TimeSpan);
        }

        public static void PvpTalentHandler340(Packet packet, uint entry, params object[] indexes)
        {
            PvpTalentHotfix340 hotfix = new PvpTalentHotfix340();

            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.SpecID = packet.ReadInt32("SpecID", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.OverridesSpellID = packet.ReadInt32("OverridesSpellID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ActionBarSpellID = packet.ReadInt32("ActionBarSpellID", indexes);
            hotfix.PvpTalentCategoryID = packet.ReadInt32("PvpTalentCategoryID", indexes);
            hotfix.LevelRequired = packet.ReadInt32("LevelRequired", indexes);

            Storage.PvpTalentHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                PvpTalentLocaleHotfix340 hotfixLocale = new PvpTalentLocaleHotfix340
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                };
                Storage.PvpTalentHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void PvpTalentCategoryHandler340(Packet packet, uint entry, params object[] indexes)
        {
            PvpTalentCategoryHotfix340 hotfix = new PvpTalentCategoryHotfix340();

            hotfix.ID = entry;
            hotfix.TalentSlotMask = packet.ReadByte("TalentSlotMask", indexes);

            Storage.PvpTalentCategoryHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void PvpTalentSlotUnlockHandler340(Packet packet, uint entry, params object[] indexes)
        {
            PvpTalentSlotUnlockHotfix340 hotfix = new PvpTalentSlotUnlockHotfix340();

            hotfix.ID = entry;
            hotfix.Slot = packet.ReadSByte("Slot", indexes);
            hotfix.LevelRequired = packet.ReadInt32("LevelRequired", indexes);
            hotfix.DeathKnightLevelRequired = packet.ReadInt32("DeathKnightLevelRequired", indexes);
            hotfix.DemonHunterLevelRequired = packet.ReadInt32("DemonHunterLevelRequired", indexes);

            Storage.PvpTalentSlotUnlockHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void PvpTierHandler340(Packet packet, uint entry, params object[] indexes)
        {
            PvpTierHotfix340 hotfix = new PvpTierHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.MinRating = packet.ReadInt16("MinRating", indexes);
            hotfix.MaxRating = packet.ReadInt16("MaxRating", indexes);
            hotfix.PrevTier = packet.ReadInt32("PrevTier", indexes);
            hotfix.NextTier = packet.ReadInt32("NextTier", indexes);
            hotfix.BracketID = packet.ReadSByte("BracketID", indexes);
            hotfix.Rank = packet.ReadSByte("Rank", indexes);
            hotfix.RankIconFileDataID = packet.ReadInt32("RankIconFileDataID", indexes);

            Storage.PvpTierHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                PvpTierLocaleHotfix340 hotfixLocale = new PvpTierLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.PvpTierHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void QuestFactionRewardHandler340(Packet packet, uint entry, params object[] indexes)
        {
            QuestFactionRewardHotfix340 hotfix = new QuestFactionRewardHotfix340();

            hotfix.ID = entry;
            hotfix.Difficulty = new short?[10];
            for (int i = 0; i < 10; i++)
                hotfix.Difficulty[i] = packet.ReadInt16("Difficulty", indexes, i);

            Storage.QuestFactionRewardHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void QuestInfoHandler340(Packet packet, uint entry, params object[] indexes)
        {
            QuestInfoHotfix340 hotfix = new QuestInfoHotfix340();

            hotfix.ID = entry;
            hotfix.InfoName = packet.ReadCString("InfoName", indexes);
            hotfix.Type = packet.ReadSByte("Type", indexes);
            hotfix.Modifiers = packet.ReadInt32("Modifiers", indexes);
            hotfix.Profession = packet.ReadUInt16("Profession", indexes);

            Storage.QuestInfoHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                QuestInfoLocaleHotfix340 hotfixLocale = new QuestInfoLocaleHotfix340
                {
                    ID = hotfix.ID,
                    InfoNameLang = hotfix.InfoName,
                };
                Storage.QuestInfoHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void QuestLineXQuestHandler340(Packet packet, uint entry, params object[] indexes)
        {
            QuestLineXQuestHotfix340 hotfix = new QuestLineXQuestHotfix340();

            hotfix.ID = entry;
            hotfix.QuestLineID = packet.ReadUInt32("QuestLineID", indexes);
            hotfix.QuestID = packet.ReadUInt32("QuestID", indexes);
            hotfix.OrderIndex = packet.ReadUInt32("OrderIndex", indexes);

            Storage.QuestLineXQuestHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void QuestMoneyRewardHandler340(Packet packet, uint entry, params object[] indexes)
        {
            QuestMoneyRewardHotfix340 hotfix = new QuestMoneyRewardHotfix340();

            hotfix.ID = entry;
            hotfix.Difficulty = new uint?[10];
            for (int i = 0; i < 10; i++)
                hotfix.Difficulty[i] = packet.ReadUInt32("Difficulty", indexes, i);

            Storage.QuestMoneyRewardHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void QuestPackageItemHandler340(Packet packet, uint entry, params object[] indexes)
        {
            QuestPackageItemHotfix340 hotfix = new QuestPackageItemHotfix340();

            hotfix.ID = entry;
            hotfix.PackageID = packet.ReadUInt16("PackageID", indexes);
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);
            hotfix.ItemQuantity = packet.ReadUInt32("ItemQuantity", indexes);
            hotfix.DisplayType = packet.ReadByte("DisplayType", indexes);

            Storage.QuestPackageItemHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void QuestSortHandler340(Packet packet, uint entry, params object[] indexes)
        {
            QuestSortHotfix340 hotfix = new QuestSortHotfix340();

            hotfix.ID = entry;
            hotfix.SortName = packet.ReadCString("SortName", indexes);
            hotfix.UiOrderIndex = packet.ReadSByte("UiOrderIndex", indexes);

            Storage.QuestSortHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                QuestSortLocaleHotfix340 hotfixLocale = new QuestSortLocaleHotfix340
                {
                    ID = hotfix.ID,
                    SortNameLang = hotfix.SortName,
                };
                Storage.QuestSortHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void QuestV2Handler340(Packet packet, uint entry, params object[] indexes)
        {
            QuestV2Hotfix340 hotfix = new QuestV2Hotfix340();

            hotfix.ID = entry;
            hotfix.UniqueBitFlag = packet.ReadUInt16("UniqueBitFlag", indexes);

            Storage.QuestV2Hotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void QuestXpHandler340(Packet packet, uint entry, params object[] indexes)
        {
            QuestXpHotfix340 hotfix = new QuestXpHotfix340();

            hotfix.ID = entry;
            hotfix.Difficulty = new ushort?[10];
            for (int i = 0; i < 10; i++)
                hotfix.Difficulty[i] = packet.ReadUInt16("Difficulty", indexes, i);

            Storage.QuestXpHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void RandPropPointsHandler340(Packet packet, uint entry, params object[] indexes)
        {
            RandPropPointsHotfix340 hotfix = new RandPropPointsHotfix340();

            hotfix.ID = entry;
            hotfix.DamageReplaceStat = packet.ReadInt32("DamageReplaceStat", indexes);
            hotfix.Epic = new uint?[5];
            for (int i = 0; i < 5; i++)
                hotfix.Epic[i] = packet.ReadUInt32("Epic", indexes, i);
            hotfix.Superior = new uint?[5];
            for (int i = 0; i < 5; i++)
                hotfix.Superior[i] = packet.ReadUInt32("Superior", indexes, i);
            hotfix.Good = new uint?[5];
            for (int i = 0; i < 5; i++)
                hotfix.Good[i] = packet.ReadUInt32("Good", indexes, i);

            Storage.RandPropPointsHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void RewardPackHandler340(Packet packet, uint entry, params object[] indexes)
        {
            RewardPackHotfix340 hotfix = new RewardPackHotfix340();

            hotfix.ID = entry;
            hotfix.CharTitleID = packet.ReadInt32("CharTitleID", indexes);
            hotfix.Money = packet.ReadUInt32("Money", indexes);
            hotfix.ArtifactXPDifficulty = packet.ReadSByte("ArtifactXPDifficulty", indexes);
            hotfix.ArtifactXPMultiplier = packet.ReadSingle("ArtifactXPMultiplier", indexes);
            hotfix.ArtifactXPCategoryID = packet.ReadByte("ArtifactXPCategoryID", indexes);
            hotfix.TreasurePickerID = packet.ReadUInt32("TreasurePickerID", indexes);

            Storage.RewardPackHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void RewardPackXCurrencyTypeHandler340(Packet packet, uint entry, params object[] indexes)
        {
            RewardPackXCurrencyTypeHotfix340 hotfix = new RewardPackXCurrencyTypeHotfix340();

            hotfix.ID = entry;
            hotfix.CurrencyTypeID = packet.ReadUInt32("CurrencyTypeID", indexes);
            hotfix.Quantity = packet.ReadInt32("Quantity", indexes);
            hotfix.RewardPackID = packet.ReadInt32("RewardPackID", indexes);

            Storage.RewardPackXCurrencyTypeHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void RewardPackXItemHandler340(Packet packet, uint entry, params object[] indexes)
        {
            RewardPackXItemHotfix340 hotfix = new RewardPackXItemHotfix340();

            hotfix.ID = entry;
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);
            hotfix.ItemQuantity = packet.ReadInt32("ItemQuantity", indexes);
            hotfix.RewardPackID = packet.ReadInt32("RewardPackID", indexes);

            Storage.RewardPackXItemHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ScenarioHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ScenarioHotfix340 hotfix = new ScenarioHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.AreaTableID = packet.ReadUInt16("AreaTableID", indexes);
            hotfix.Type = packet.ReadByte("Type", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.UiTextureKitID = packet.ReadUInt32("UiTextureKitID", indexes);

            Storage.ScenarioHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ScenarioLocaleHotfix340 hotfixLocale = new ScenarioLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ScenarioHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ScenarioStepHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ScenarioStepHotfix340 hotfix = new ScenarioStepHotfix340();

            hotfix.ID = entry;
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.Title = packet.ReadCString("Title", indexes);
            hotfix.ScenarioID = packet.ReadUInt16("ScenarioID", indexes);
            hotfix.CriteriatreeID = packet.ReadUInt32("CriteriatreeID", indexes);
            hotfix.RewardQuestID = packet.ReadUInt32("RewardQuestID", indexes);
            hotfix.RelatedStep = packet.ReadInt32("RelatedStep", indexes);
            hotfix.Supersedes = packet.ReadUInt16("Supersedes", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.VisibilityPlayerConditionID = packet.ReadUInt32("VisibilityPlayerConditionID", indexes);
            hotfix.WidgetSetID = packet.ReadUInt16("WidgetSetID", indexes);

            Storage.ScenarioStepHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ScenarioStepLocaleHotfix340 hotfixLocale = new ScenarioStepLocaleHotfix340
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                    TitleLang = hotfix.Title,
                };
                Storage.ScenarioStepHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ScalingStatDistributionHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ScalingStatDistributionHotfix340 hotfix = new ScalingStatDistributionHotfix340();

            hotfix.ID = entry;
            hotfix.PlayerLevelToItemLevelCurveID = packet.ReadUInt16("PlayerLevelToItemLevelCurveID", indexes);
            hotfix.MinLevel = packet.ReadInt32("MinLevel", indexes);
            hotfix.MaxLevel = packet.ReadInt32("MaxLevel", indexes);

            Storage.ScalingStatDistributionHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ScalingStatDistributionHandler341(Packet packet, uint entry, params object[] indexes)
        {
            ScalingStatDistributionHotfix341 hotfix = new ScalingStatDistributionHotfix341();

            hotfix.ID = entry;
            hotfix.PlayerLevelToItemLevelCurveID = packet.ReadUInt16("PlayerLevelToItemLevelCurveID", indexes);
            hotfix.MinLevel = packet.ReadInt32("MinLevel", indexes);
            hotfix.MaxLevel = packet.ReadInt32("MaxLevel", indexes);
            hotfix.Bonus = new int?[10];
            for (int i = 0; i < 10; i++)
                hotfix.Bonus[i] = packet.ReadInt32("Bonus", indexes, i);
            hotfix.StatID = new int?[10];
            for (int i = 0; i < 10; i++)
                hotfix.StatID[i] = packet.ReadInt32("StatID", indexes, i);

            Storage.ScalingStatDistributionHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void ScalingStatValuesHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ScalingStatValuesHotfix340 hotfix = new ScalingStatValuesHotfix340();

            hotfix.ID = entry;
            hotfix.Charlevel = packet.ReadInt32("Charlevel", indexes);
            hotfix.WeaponDPS1H = packet.ReadInt32("WeaponDPS1H", indexes);
            hotfix.WeaponDPS2H = packet.ReadInt32("WeaponDPS2H", indexes);
            hotfix.SpellcasterDPS1H = packet.ReadInt32("SpellcasterDPS1H", indexes);
            hotfix.SpellcasterDPS2H = packet.ReadInt32("SpellcasterDPS2H", indexes);
            hotfix.RangedDPS = packet.ReadInt32("RangedDPS", indexes);
            hotfix.WandDPS = packet.ReadInt32("WandDPS", indexes);
            hotfix.SpellPower = packet.ReadInt32("SpellPower", indexes);
            hotfix.ShoulderBudget = packet.ReadInt32("ShoulderBudget", indexes);
            hotfix.TrinketBudget = packet.ReadInt32("TrinketBudget", indexes);
            hotfix.WeaponBudget1H = packet.ReadInt32("WeaponBudget1H", indexes);
            hotfix.PrimaryBudget = packet.ReadInt32("PrimaryBudget", indexes);
            hotfix.RangedBudget = packet.ReadInt32("RangedBudget", indexes);
            hotfix.TertiaryBudget = packet.ReadInt32("TertiaryBudget", indexes);
            hotfix.ClothShoulderArmor = packet.ReadInt32("ClothShoulderArmor", indexes);
            hotfix.LeatherShoulderArmor = packet.ReadInt32("LeatherShoulderArmor", indexes);
            hotfix.MailShoulderArmor = packet.ReadInt32("MailShoulderArmor", indexes);
            hotfix.PlateShoulderArmor = packet.ReadInt32("PlateShoulderArmor", indexes);
            hotfix.ClothCloakArmor = packet.ReadInt32("ClothCloakArmor", indexes);
            hotfix.ClothChestArmor = packet.ReadInt32("ClothChestArmor", indexes);
            hotfix.LeatherChestArmor = packet.ReadInt32("LeatherChestArmor", indexes);
            hotfix.MailChestArmor = packet.ReadInt32("MailChestArmor", indexes);
            hotfix.PlateChestArmor = packet.ReadInt32("PlateChestArmor", indexes);

            Storage.ScalingStatValuesHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SceneScriptHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SceneScriptHotfix340 hotfix = new SceneScriptHotfix340();

            hotfix.ID = entry;
            hotfix.FirstSceneScriptID = packet.ReadUInt16("FirstSceneScriptID", indexes);
            hotfix.NextSceneScriptID = packet.ReadUInt16("NextSceneScriptID", indexes);
            hotfix.Unknown915 = packet.ReadInt32("Unknown915", indexes);

            Storage.SceneScriptHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SceneScriptGlobalTextHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SceneScriptGlobalTextHotfix340 hotfix = new SceneScriptGlobalTextHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Script = packet.ReadCString("Script", indexes);

            Storage.SceneScriptGlobalTextHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SceneScriptPackageHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SceneScriptPackageHotfix340 hotfix = new SceneScriptPackageHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);

            Storage.SceneScriptPackageHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SceneScriptTextHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SceneScriptTextHotfix340 hotfix = new SceneScriptTextHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Script = packet.ReadCString("Script", indexes);

            Storage.SceneScriptTextHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void ServerMessagesHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ServerMessagesHotfix340 hotfix = new ServerMessagesHotfix340();

            hotfix.ID = entry;
            hotfix.Text = packet.ReadCString("Text", indexes);

            Storage.ServerMessagesHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ServerMessagesLocaleHotfix340 hotfixLocale = new ServerMessagesLocaleHotfix340
                {
                    ID = hotfix.ID,
                    TextLang = hotfix.Text,
                };
                Storage.ServerMessagesHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SkillLineHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SkillLineHotfix340 hotfix = new SkillLineHotfix340();

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
            hotfix.Flags = packet.ReadUInt16("Flags", indexes);
            hotfix.SpellBookSpellID = packet.ReadInt32("SpellBookSpellID", indexes);

            Storage.SkillLineHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SkillLineLocaleHotfix340 hotfixLocale = new SkillLineLocaleHotfix340
                {
                    ID = hotfix.ID,
                    DisplayNameLang = hotfix.DisplayName,
                    AlternateVerbLang = hotfix.AlternateVerb,
                    DescriptionLang = hotfix.Description,
                    HordeDisplayNameLang = hotfix.HordeDisplayName,
                };
                Storage.SkillLineHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SkillLineAbilityHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SkillLineAbilityHotfix340 hotfix = new SkillLineAbilityHotfix340();

            hotfix.RaceMask = packet.ReadInt64("RaceMask", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.SkillLine = packet.ReadInt16("SkillLine", indexes);
            hotfix.Spell = packet.ReadInt32("Spell", indexes);
            hotfix.MinSkillLineRank = packet.ReadInt16("MinSkillLineRank", indexes);
            hotfix.ClassMask = packet.ReadInt32("ClassMask", indexes);
            hotfix.SupercedesSpell = packet.ReadInt32("SupercedesSpell", indexes);
            hotfix.AcquireMethod = packet.ReadSByte("AcquireMethod", indexes);
            hotfix.TrivialSkillLineRankHigh = packet.ReadInt16("TrivialSkillLineRankHigh", indexes);
            hotfix.TrivialSkillLineRankLow = packet.ReadInt16("TrivialSkillLineRankLow", indexes);
            hotfix.Flags = packet.ReadSByte("Flags", indexes);
            hotfix.NumSkillUps = packet.ReadSByte("NumSkillUps", indexes);
            hotfix.UniqueBit = packet.ReadInt16("UniqueBit", indexes);
            hotfix.TradeSkillCategoryID = packet.ReadInt16("TradeSkillCategoryID", indexes);
            hotfix.SkillupSkillLineID = packet.ReadInt16("SkillupSkillLineID", indexes);
            hotfix.CharacterPoints = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.CharacterPoints[i] = packet.ReadInt32("CharacterPoints", indexes, i);

            Storage.SkillLineAbilityHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SkillLineAbilityLocaleHotfix340 hotfixLocale = new SkillLineAbilityLocaleHotfix340
                {
                    ID = hotfix.ID,
                };
                Storage.SkillLineAbilityHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SkillLineXTraitTreeHandler341(Packet packet, uint entry, params object[] indexes)
        {
            SkillLineXTraitTreeHotfix341 hotfix = new SkillLineXTraitTreeHotfix341();

            hotfix.ID = entry;
            hotfix.SkillLineID = packet.ReadInt32("SkillLineID", indexes);
            hotfix.TraitTreeID = packet.ReadInt32("TraitTreeID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);

            Storage.SkillLineXTraitTreeHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void SkillRaceClassInfoHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SkillRaceClassInfoHotfix340 hotfix = new SkillRaceClassInfoHotfix340();

            hotfix.ID = entry;
            hotfix.RaceMask = packet.ReadInt64("RaceMask", indexes);
            hotfix.SkillID = packet.ReadInt16("SkillID", indexes);
            hotfix.ClassMask = packet.ReadInt32("ClassMask", indexes);
            hotfix.Flags = packet.ReadUInt16("Flags", indexes);
            hotfix.Availability = packet.ReadSByte("Availability", indexes);
            hotfix.MinLevel = packet.ReadSByte("MinLevel", indexes);
            hotfix.SkillTierID = packet.ReadInt16("SkillTierID", indexes);

            Storage.SkillRaceClassInfoHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SoundKitHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SoundKitHotfix340 hotfix = new SoundKitHotfix340();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.SoundType = packet.ReadByte("SoundType", indexes);
            hotfix.VolumeFloat = packet.ReadSingle("VolumeFloat", indexes);
            hotfix.Flags = packet.ReadUInt16("Flags", indexes);
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

            Storage.SoundKitHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SoundKitHandler341(Packet packet, uint entry, params object[] indexes)
        {
            SoundKitHotfix341 hotfix = new SoundKitHotfix341();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.SoundType = packet.ReadByte("SoundType", indexes);
            hotfix.VolumeFloat = packet.ReadSingle("VolumeFloat", indexes);
            hotfix.Flags = packet.ReadUInt16("Flags", indexes);
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

            Storage.SoundKitHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void SpecializationSpellsHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpecializationSpellsHotfix340 hotfix = new SpecializationSpellsHotfix340();

            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.SpecID = packet.ReadUInt16("SpecID", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.OverridesSpellID = packet.ReadInt32("OverridesSpellID", indexes);
            hotfix.DisplayOrder = packet.ReadByte("DisplayOrder", indexes);

            Storage.SpecializationSpellsHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SpecializationSpellsLocaleHotfix340 hotfixLocale = new SpecializationSpellsLocaleHotfix340
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                };
                Storage.SpecializationSpellsHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SpecSetMemberHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpecSetMemberHotfix340 hotfix = new SpecSetMemberHotfix340();

            hotfix.ID = entry;
            hotfix.ChrSpecializationID = packet.ReadInt32("ChrSpecializationID", indexes);
            hotfix.SpecSetID = packet.ReadInt32("SpecSetID", indexes);

            Storage.SpecSetMemberHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellAuraOptionsHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellAuraOptionsHotfix340 hotfix = new SpellAuraOptionsHotfix340();

            hotfix.ID = entry;
            hotfix.DifficultyID = packet.ReadByte("DifficultyID", indexes);
            hotfix.CumulativeAura = packet.ReadUInt32("CumulativeAura", indexes);
            hotfix.ProcCategoryRecovery = packet.ReadInt32("ProcCategoryRecovery", indexes);
            hotfix.ProcChance = packet.ReadByte("ProcChance", indexes);
            hotfix.ProcCharges = packet.ReadInt32("ProcCharges", indexes);
            hotfix.SpellProcsPerMinuteID = packet.ReadUInt16("SpellProcsPerMinuteID", indexes);
            hotfix.ProcTypeMask = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.ProcTypeMask[i] = packet.ReadInt32("ProcTypeMask", indexes, i);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);

            Storage.SpellAuraOptionsHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellAuraRestrictionsHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellAuraRestrictionsHotfix340 hotfix = new SpellAuraRestrictionsHotfix340();

            hotfix.ID = entry;
            hotfix.DifficultyID = packet.ReadByte("DifficultyID", indexes);
            hotfix.CasterAuraState = packet.ReadByte("CasterAuraState", indexes);
            hotfix.TargetAuraState = packet.ReadByte("TargetAuraState", indexes);
            hotfix.ExcludeCasterAuraState = packet.ReadByte("ExcludeCasterAuraState", indexes);
            hotfix.ExcludeTargetAuraState = packet.ReadByte("ExcludeTargetAuraState", indexes);
            hotfix.CasterAuraSpell = packet.ReadInt32("CasterAuraSpell", indexes);
            hotfix.TargetAuraSpell = packet.ReadInt32("TargetAuraSpell", indexes);
            hotfix.ExcludeCasterAuraSpell = packet.ReadInt32("ExcludeCasterAuraSpell", indexes);
            hotfix.ExcludeTargetAuraSpell = packet.ReadInt32("ExcludeTargetAuraSpell", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);

            Storage.SpellAuraRestrictionsHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellCastTimesHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellCastTimesHotfix340 hotfix = new SpellCastTimesHotfix340();

            hotfix.ID = entry;
            hotfix.Base = packet.ReadInt32("Base", indexes);
            hotfix.PerLevel = packet.ReadInt16("PerLevel", indexes);
            hotfix.Minimum = packet.ReadInt32("Minimum", indexes);

            Storage.SpellCastTimesHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellCastingRequirementsHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellCastingRequirementsHotfix340 hotfix = new SpellCastingRequirementsHotfix340();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.FacingCasterFlags = packet.ReadByte("FacingCasterFlags", indexes);
            hotfix.MinFactionID = packet.ReadUInt16("MinFactionID", indexes);
            hotfix.MinReputation = packet.ReadSByte("MinReputation", indexes);
            hotfix.RequiredAreasID = packet.ReadUInt16("RequiredAreasID", indexes);
            hotfix.RequiredAuraVision = packet.ReadByte("RequiredAuraVision", indexes);
            hotfix.RequiresSpellFocus = packet.ReadUInt16("RequiresSpellFocus", indexes);

            Storage.SpellCastingRequirementsHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellCastingRequirementsHandler341(Packet packet, uint entry, params object[] indexes)
        {
            SpellCastingRequirementsHotfix341 hotfix = new SpellCastingRequirementsHotfix341();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.FacingCasterFlags = packet.ReadByte("FacingCasterFlags", indexes);
            hotfix.MinFactionID = packet.ReadUInt16("MinFactionID", indexes);
            hotfix.MinReputation = packet.ReadInt32("MinReputation", indexes);
            hotfix.RequiredAreasID = packet.ReadUInt16("RequiredAreasID", indexes);
            hotfix.RequiredAuraVision = packet.ReadByte("RequiredAuraVision", indexes);
            hotfix.RequiresSpellFocus = packet.ReadUInt16("RequiresSpellFocus", indexes);

            Storage.SpellCastingRequirementsHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellCategoriesHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellCategoriesHotfix340 hotfix = new SpellCategoriesHotfix340();

            hotfix.ID = entry;
            hotfix.DifficultyID = packet.ReadByte("DifficultyID", indexes);
            hotfix.Category = packet.ReadInt16("Category", indexes);
            hotfix.DefenseType = packet.ReadSByte("DefenseType", indexes);
            hotfix.DispelType = packet.ReadSByte("DispelType", indexes);
            hotfix.Mechanic = packet.ReadSByte("Mechanic", indexes);
            hotfix.PreventionType = packet.ReadSByte("PreventionType", indexes);
            hotfix.StartRecoveryCategory = packet.ReadInt16("StartRecoveryCategory", indexes);
            hotfix.ChargeCategory = packet.ReadInt16("ChargeCategory", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);

            Storage.SpellCategoriesHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellCategoryHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellCategoryHotfix340 hotfix = new SpellCategoryHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Flags = packet.ReadSByte("Flags", indexes);
            hotfix.UsesPerWeek = packet.ReadByte("UsesPerWeek", indexes);
            hotfix.MaxCharges = packet.ReadSByte("MaxCharges", indexes);
            hotfix.ChargeRecoveryTime = packet.ReadInt32("ChargeRecoveryTime", indexes);
            hotfix.TypeMask = packet.ReadInt32("TypeMask", indexes);

            Storage.SpellCategoryHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SpellCategoryLocaleHotfix340 hotfixLocale = new SpellCategoryLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.SpellCategoryHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SpellCategoryHandler343(Packet packet, uint entry, params object[] indexes)
        {
            SpellCategoryHotfix343 hotfix = new SpellCategoryHotfix343();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.UsesPerWeek = packet.ReadByte("UsesPerWeek", indexes);
            hotfix.MaxCharges = packet.ReadSByte("MaxCharges", indexes);
            hotfix.ChargeRecoveryTime = packet.ReadInt32("ChargeRecoveryTime", indexes);
            hotfix.TypeMask = packet.ReadInt32("TypeMask", indexes);

            Storage.SpellCategoryHotfixes343.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SpellCategoryLocaleHotfix343 hotfixLocale = new SpellCategoryLocaleHotfix343
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.SpellCategoryHotfixesLocale343.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SpellClassOptionsHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellClassOptionsHotfix340 hotfix = new SpellClassOptionsHotfix340();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.ModalNextSpell = packet.ReadUInt32("ModalNextSpell", indexes);
            hotfix.SpellClassSet = packet.ReadByte("SpellClassSet", indexes);
            hotfix.SpellClassMask = new int?[4];
            for (int i = 0; i < 4; i++)
                hotfix.SpellClassMask[i] = packet.ReadInt32("SpellClassMask", indexes, i);

            Storage.SpellClassOptionsHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellCooldownsHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellCooldownsHotfix340 hotfix = new SpellCooldownsHotfix340();

            hotfix.ID = entry;
            hotfix.DifficultyID = packet.ReadByte("DifficultyID", indexes);
            hotfix.CategoryRecoveryTime = packet.ReadInt32("CategoryRecoveryTime", indexes);
            hotfix.RecoveryTime = packet.ReadInt32("RecoveryTime", indexes);
            hotfix.StartRecoveryTime = packet.ReadInt32("StartRecoveryTime", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);

            Storage.SpellCooldownsHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellDurationHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellDurationHotfix340 hotfix = new SpellDurationHotfix340();

            hotfix.ID = entry;
            hotfix.Duration = packet.ReadInt32("Duration", indexes);
            hotfix.DurationPerLevel = packet.ReadUInt32("DurationPerLevel", indexes);
            hotfix.MaxDuration = packet.ReadInt32("MaxDuration", indexes);

            Storage.SpellDurationHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellEffectHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellEffectHotfix340 hotfix = new SpellEffectHotfix340();

            hotfix.ID = entry;
            hotfix.DifficultyID = packet.ReadInt32("DifficultyID", indexes);
            hotfix.EffectIndex = packet.ReadInt32("EffectIndex", indexes);
            hotfix.Effect = packet.ReadUInt32("Effect", indexes);
            hotfix.EffectAmplitude = packet.ReadSingle("EffectAmplitude", indexes);
            hotfix.EffectAttributes = packet.ReadInt32("EffectAttributes", indexes);
            hotfix.EffectAura = packet.ReadInt16("EffectAura", indexes);
            hotfix.EffectAuraPeriod = packet.ReadInt32("EffectAuraPeriod", indexes);
            hotfix.EffectBasePoints = packet.ReadInt32("EffectBasePoints", indexes);
            hotfix.EffectBonusCoefficient = packet.ReadSingle("EffectBonusCoefficient", indexes);
            hotfix.EffectChainAmplitude = packet.ReadSingle("EffectChainAmplitude", indexes);
            hotfix.EffectChainTargets = packet.ReadInt32("EffectChainTargets", indexes);
            hotfix.EffectDieSides = packet.ReadInt32("EffectDieSides", indexes);
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
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);

            Storage.SpellEffectHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellEquippedItemsHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellEquippedItemsHotfix340 hotfix = new SpellEquippedItemsHotfix340();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.EquippedItemClass = packet.ReadSByte("EquippedItemClass", indexes);
            hotfix.EquippedItemInvTypes = packet.ReadInt32("EquippedItemInvTypes", indexes);
            hotfix.EquippedItemSubclass = packet.ReadInt32("EquippedItemSubclass", indexes);

            Storage.SpellEquippedItemsHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellFocusObjectHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellFocusObjectHotfix340 hotfix = new SpellFocusObjectHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);

            Storage.SpellFocusObjectHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SpellFocusObjectLocaleHotfix340 hotfixLocale = new SpellFocusObjectLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.SpellFocusObjectHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SpellInterruptsHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellInterruptsHotfix340 hotfix = new SpellInterruptsHotfix340();

            hotfix.ID = entry;
            hotfix.DifficultyID = packet.ReadByte("DifficultyID", indexes);
            hotfix.InterruptFlags = packet.ReadInt16("InterruptFlags", indexes);
            hotfix.AuraInterruptFlags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.AuraInterruptFlags[i] = packet.ReadInt32("AuraInterruptFlags", indexes, i);
            hotfix.ChannelInterruptFlags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.ChannelInterruptFlags[i] = packet.ReadInt32("ChannelInterruptFlags", indexes, i);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);

            Storage.SpellInterruptsHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellItemEnchantmentHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellItemEnchantmentHotfix340 hotfix = new SpellItemEnchantmentHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.HordeName = packet.ReadCString("HordeName", indexes);
            hotfix.EffectArg = new uint?[3];
            for (int i = 0; i < 3; i++)
                hotfix.EffectArg[i] = packet.ReadUInt32("EffectArg", indexes, i);
            hotfix.EffectScalingPoints = new float?[3];
            for (int i = 0; i < 3; i++)
                hotfix.EffectScalingPoints[i] = packet.ReadSingle("EffectScalingPoints", indexes, i);
            hotfix.GemItemID = packet.ReadUInt32("GemItemID", indexes);
            hotfix.TransmogUnlockConditionID = packet.ReadUInt32("TransmogUnlockConditionID", indexes);
            hotfix.TransmogCost = packet.ReadUInt32("TransmogCost", indexes);
            hotfix.IconFileDataID = packet.ReadUInt32("IconFileDataID", indexes);
            hotfix.EffectPointsMin = new short?[3];
            for (int i = 0; i < 3; i++)
                hotfix.EffectPointsMin[i] = packet.ReadInt16("EffectPointsMin", indexes, i);
            hotfix.ItemVisual = packet.ReadUInt16("ItemVisual", indexes);
            hotfix.Flags = packet.ReadUInt16("Flags", indexes);
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

            Storage.SpellItemEnchantmentHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SpellItemEnchantmentLocaleHotfix340 hotfixLocale = new SpellItemEnchantmentLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    HordeNameLang = hotfix.HordeName,
                };
                Storage.SpellItemEnchantmentHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SpellItemEnchantmentConditionHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellItemEnchantmentConditionHotfix340 hotfix = new SpellItemEnchantmentConditionHotfix340();

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

            Storage.SpellItemEnchantmentConditionHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellKeyboundOverrideHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellKeyboundOverrideHotfix340 hotfix = new SpellKeyboundOverrideHotfix340();

            hotfix.ID = entry;
            hotfix.Function = packet.ReadCString("Function", indexes);
            hotfix.Type = packet.ReadSByte("Type", indexes);
            hotfix.Data = packet.ReadInt32("Data", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.SpellKeyboundOverrideHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellLabelHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellLabelHotfix340 hotfix = new SpellLabelHotfix340();

            hotfix.ID = entry;
            hotfix.LabelID = packet.ReadUInt32("LabelID", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);

            Storage.SpellLabelHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellLearnSpellHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellLearnSpellHotfix340 hotfix = new SpellLearnSpellHotfix340();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.LearnSpellID = packet.ReadInt32("LearnSpellID", indexes);
            hotfix.OverridesSpellID = packet.ReadInt32("OverridesSpellID", indexes);

            Storage.SpellLearnSpellHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellLevelsHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellLevelsHotfix340 hotfix = new SpellLevelsHotfix340();

            hotfix.ID = entry;
            hotfix.DifficultyID = packet.ReadByte("DifficultyID", indexes);
            hotfix.BaseLevel = packet.ReadInt16("BaseLevel", indexes);
            hotfix.MaxLevel = packet.ReadInt16("MaxLevel", indexes);
            hotfix.SpellLevel = packet.ReadInt16("SpellLevel", indexes);
            hotfix.MaxPassiveAuraLevel = packet.ReadByte("MaxPassiveAuraLevel", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);

            Storage.SpellLevelsHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellMiscHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellMiscHotfix340 hotfix = new SpellMiscHotfix340();

            hotfix.ID = entry;
            hotfix.DifficultyID = packet.ReadByte("DifficultyID", indexes);
            hotfix.CastingTimeIndex = packet.ReadUInt16("CastingTimeIndex", indexes);
            hotfix.DurationIndex = packet.ReadUInt16("DurationIndex", indexes);
            hotfix.RangeIndex = packet.ReadUInt16("RangeIndex", indexes);
            hotfix.SchoolMask = packet.ReadByte("SchoolMask", indexes);
            hotfix.Speed = packet.ReadSingle("Speed", indexes);
            hotfix.LaunchDelay = packet.ReadSingle("LaunchDelay", indexes);
            hotfix.MinDuration = packet.ReadSingle("MinDuration", indexes);
            hotfix.SpellIconFileDataID = packet.ReadInt32("SpellIconFileDataID", indexes);
            hotfix.ActiveIconFileDataID = packet.ReadInt32("ActiveIconFileDataID", indexes);
            hotfix.ContentTuningID = packet.ReadInt32("ContentTuningID", indexes);
            hotfix.ShowFutureSpellPlayerConditionID = packet.ReadInt32("ShowFutureSpellPlayerConditionID", indexes);
            hotfix.Attributes = new int?[14];
            for (int i = 0; i < 14; i++)
                hotfix.Attributes[i] = packet.ReadInt32("Attributes", indexes, i);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);

            Storage.SpellMiscHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellMiscHandler341(Packet packet, uint entry, params object[] indexes)
        {
            SpellMiscHotfix341 hotfix = new SpellMiscHotfix341();

            hotfix.ID = entry;
            hotfix.Attributes = new int?[15];
            for (int i = 0; i < 15; i++)
                hotfix.Attributes[i] = packet.ReadInt32("Attributes", indexes, i);
            hotfix.DifficultyID = packet.ReadByte("DifficultyID", indexes);
            hotfix.CastingTimeIndex = packet.ReadUInt16("CastingTimeIndex", indexes);
            hotfix.DurationIndex = packet.ReadUInt16("DurationIndex", indexes);
            hotfix.RangeIndex = packet.ReadUInt16("RangeIndex", indexes);
            hotfix.SchoolMask = packet.ReadByte("SchoolMask", indexes);
            hotfix.Speed = packet.ReadSingle("Speed", indexes);
            hotfix.LaunchDelay = packet.ReadSingle("LaunchDelay", indexes);
            hotfix.MinDuration = packet.ReadSingle("MinDuration", indexes);
            hotfix.SpellIconFileDataID = packet.ReadInt32("SpellIconFileDataID", indexes);
            hotfix.ActiveIconFileDataID = packet.ReadInt32("ActiveIconFileDataID", indexes);
            hotfix.ContentTuningID = packet.ReadInt32("ContentTuningID", indexes);
            hotfix.ShowFutureSpellPlayerConditionID = packet.ReadInt32("ShowFutureSpellPlayerConditionID", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);

            Storage.SpellMiscHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellNameHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellNameHotfix340 hotfix = new SpellNameHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);

            Storage.SpellNameHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SpellNameLocaleHotfix340 hotfixLocale = new SpellNameLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.SpellNameHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SpellPowerHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellPowerHotfix340 hotfix = new SpellPowerHotfix340();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);
            hotfix.ManaCost = packet.ReadInt32("ManaCost", indexes);
            hotfix.ManaCostPerLevel = packet.ReadInt32("ManaCostPerLevel", indexes);
            hotfix.ManaPerSecond = packet.ReadInt32("ManaPerSecond", indexes);
            hotfix.PowerDisplayID = packet.ReadUInt32("PowerDisplayID", indexes);
            hotfix.AltPowerBarID = packet.ReadInt32("AltPowerBarID", indexes);
            hotfix.PowerCostPct = packet.ReadSingle("PowerCostPct", indexes);
            hotfix.PowerCostMaxPct = packet.ReadSingle("PowerCostMaxPct", indexes);
            hotfix.PowerPctPerSecond = packet.ReadSingle("PowerPctPerSecond", indexes);
            hotfix.PowerType = packet.ReadSByte("PowerType", indexes);
            hotfix.RequiredAuraSpellID = packet.ReadInt32("RequiredAuraSpellID", indexes);
            hotfix.OptionalCost = packet.ReadUInt32("OptionalCost", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);

            Storage.SpellPowerHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellPowerDifficultyHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellPowerDifficultyHotfix340 hotfix = new SpellPowerDifficultyHotfix340();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.DifficultyID = packet.ReadByte("DifficultyID", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);

            Storage.SpellPowerDifficultyHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellProcsPerMinuteHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellProcsPerMinuteHotfix340 hotfix = new SpellProcsPerMinuteHotfix340();

            hotfix.ID = entry;
            hotfix.BaseProcRate = packet.ReadSingle("BaseProcRate", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);

            Storage.SpellProcsPerMinuteHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellProcsPerMinuteModHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellProcsPerMinuteModHotfix340 hotfix = new SpellProcsPerMinuteModHotfix340();

            hotfix.ID = entry;
            hotfix.Type = packet.ReadByte("Type", indexes);
            hotfix.Param = packet.ReadInt16("Param", indexes);
            hotfix.Coeff = packet.ReadSingle("Coeff", indexes);
            hotfix.SpellProcsPerMinuteID = packet.ReadInt32("SpellProcsPerMinuteID", indexes);

            Storage.SpellProcsPerMinuteModHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellRadiusHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellRadiusHotfix340 hotfix = new SpellRadiusHotfix340();

            hotfix.ID = entry;
            hotfix.Radius = packet.ReadSingle("Radius", indexes);
            hotfix.RadiusPerLevel = packet.ReadSingle("RadiusPerLevel", indexes);
            hotfix.RadiusMin = packet.ReadSingle("RadiusMin", indexes);
            hotfix.RadiusMax = packet.ReadSingle("RadiusMax", indexes);

            Storage.SpellRadiusHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellRangeHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellRangeHotfix340 hotfix = new SpellRangeHotfix340();

            hotfix.ID = entry;
            hotfix.DisplayName = packet.ReadCString("DisplayName", indexes);
            hotfix.DisplayNameShort = packet.ReadCString("DisplayNameShort", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.RangeMin = new float?[2];
            for (int i = 0; i < 2; i++)
                hotfix.RangeMin[i] = packet.ReadSingle("RangeMin", indexes, i);
            hotfix.RangeMax = new float?[2];
            for (int i = 0; i < 2; i++)
                hotfix.RangeMax[i] = packet.ReadSingle("RangeMax", indexes, i);

            Storage.SpellRangeHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SpellRangeLocaleHotfix340 hotfixLocale = new SpellRangeLocaleHotfix340
                {
                    ID = hotfix.ID,
                    DisplayNameLang = hotfix.DisplayName,
                    DisplayNameShortLang = hotfix.DisplayNameShort,
                };
                Storage.SpellRangeHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SpellReagentsHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellReagentsHotfix340 hotfix = new SpellReagentsHotfix340();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.Reagent = new int?[8];
            for (int i = 0; i < 8; i++)
                hotfix.Reagent[i] = packet.ReadInt32("Reagent", indexes, i);
            hotfix.ReagentCount = new short?[8];
            for (int i = 0; i < 8; i++)
                hotfix.ReagentCount[i] = packet.ReadInt16("ReagentCount", indexes, i);

            Storage.SpellReagentsHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellReagentsCurrencyHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellReagentsCurrencyHotfix340 hotfix = new SpellReagentsCurrencyHotfix340();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.CurrencyTypesID = packet.ReadUInt16("CurrencyTypesID", indexes);
            hotfix.CurrencyCount = packet.ReadUInt16("CurrencyCount", indexes);

            Storage.SpellReagentsCurrencyHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellScalingHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellScalingHotfix340 hotfix = new SpellScalingHotfix340();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.Class = packet.ReadInt32("Class", indexes);
            hotfix.MinScalingLevel = packet.ReadUInt32("MinScalingLevel", indexes);
            hotfix.MaxScalingLevel = packet.ReadUInt32("MaxScalingLevel", indexes);
            hotfix.ScalesFromItemLevel = packet.ReadInt16("ScalesFromItemLevel", indexes);

            Storage.SpellScalingHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellShapeshiftHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellShapeshiftHotfix340 hotfix = new SpellShapeshiftHotfix340();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.StanceBarOrder = packet.ReadSByte("StanceBarOrder", indexes);
            hotfix.ShapeshiftExclude = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.ShapeshiftExclude[i] = packet.ReadInt32("ShapeshiftExclude", indexes, i);
            hotfix.ShapeshiftMask = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.ShapeshiftMask[i] = packet.ReadInt32("ShapeshiftMask", indexes, i);

            Storage.SpellShapeshiftHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellShapeshiftFormHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellShapeshiftFormHotfix340 hotfix = new SpellShapeshiftFormHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.CreatureType = packet.ReadSByte("CreatureType", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.AttackIconFileID = packet.ReadInt32("AttackIconFileID", indexes);
            hotfix.BonusActionBar = packet.ReadSByte("BonusActionBar", indexes);
            hotfix.CombatRoundTime = packet.ReadInt16("CombatRoundTime", indexes);
            hotfix.DamageVariance = packet.ReadSingle("DamageVariance", indexes);
            hotfix.MountTypeID = packet.ReadUInt16("MountTypeID", indexes);
            hotfix.CreatureDisplayID = new uint?[4];
            for (int i = 0; i < 4; i++)
                hotfix.CreatureDisplayID[i] = packet.ReadUInt32("CreatureDisplayID", indexes, i);
            hotfix.PresetSpellID = new uint?[8];
            for (int i = 0; i < 8; i++)
                hotfix.PresetSpellID[i] = packet.ReadUInt32("PresetSpellID", indexes, i);

            Storage.SpellShapeshiftFormHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SpellShapeshiftFormLocaleHotfix340 hotfixLocale = new SpellShapeshiftFormLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.SpellShapeshiftFormHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SpellTargetRestrictionsHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellTargetRestrictionsHotfix340 hotfix = new SpellTargetRestrictionsHotfix340();

            hotfix.ID = entry;
            hotfix.DifficultyID = packet.ReadByte("DifficultyID", indexes);
            hotfix.ConeDegrees = packet.ReadSingle("ConeDegrees", indexes);
            hotfix.MaxTargets = packet.ReadByte("MaxTargets", indexes);
            hotfix.MaxTargetLevel = packet.ReadUInt32("MaxTargetLevel", indexes);
            hotfix.TargetCreatureType = packet.ReadInt16("TargetCreatureType", indexes);
            hotfix.Targets = packet.ReadInt32("Targets", indexes);
            hotfix.Width = packet.ReadSingle("Width", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);

            Storage.SpellTargetRestrictionsHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellTotemsHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellTotemsHotfix340 hotfix = new SpellTotemsHotfix340();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.RequiredTotemCategoryID = new ushort?[2];
            for (int i = 0; i < 2; i++)
                hotfix.RequiredTotemCategoryID[i] = packet.ReadUInt16("RequiredTotemCategoryID", indexes, i);
            hotfix.Totem = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Totem[i] = packet.ReadInt32("Totem", indexes, i);

            Storage.SpellTotemsHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellVisualHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellVisualHotfix340 hotfix = new SpellVisualHotfix340();

            hotfix.ID = entry;
            hotfix.MissileCastOffset = new float?[3];
            for (int i = 0; i < 3; i++)
                hotfix.MissileCastOffset[i] = packet.ReadSingle("MissileCastOffset", indexes, i);
            hotfix.MissileImpactOffset = new float?[3];
            for (int i = 0; i < 3; i++)
                hotfix.MissileImpactOffset[i] = packet.ReadSingle("MissileImpactOffset", indexes, i);
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
            hotfix.AreaModel = packet.ReadUInt16("AreaModel", indexes);
            hotfix.HasMissile = packet.ReadSByte("HasMissile", indexes);

            Storage.SpellVisualHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellVisualEffectNameHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellVisualEffectNameHotfix340 hotfix = new SpellVisualEffectNameHotfix340();

            hotfix.ID = entry;
            hotfix.ModelFileDataID = packet.ReadInt32("ModelFileDataID", indexes);
            hotfix.BaseMissileSpeed = packet.ReadSingle("BaseMissileSpeed", indexes);
            hotfix.Scale = packet.ReadSingle("Scale", indexes);
            hotfix.MinAllowedScale = packet.ReadSingle("MinAllowedScale", indexes);
            hotfix.MaxAllowedScale = packet.ReadSingle("MaxAllowedScale", indexes);
            hotfix.Alpha = packet.ReadSingle("Alpha", indexes);
            hotfix.Flags = packet.ReadUInt32("Flags", indexes);
            hotfix.TextureFileDataID = packet.ReadInt32("TextureFileDataID", indexes);
            hotfix.EffectRadius = packet.ReadSingle("EffectRadius", indexes);
            hotfix.Type = packet.ReadUInt32("Type", indexes);
            hotfix.GenericID = packet.ReadInt32("GenericID", indexes);
            hotfix.RibbonQualityID = packet.ReadUInt32("RibbonQualityID", indexes);
            hotfix.DissolveEffectID = packet.ReadInt32("DissolveEffectID", indexes);
            hotfix.ModelPosition = packet.ReadInt32("ModelPosition", indexes);

            Storage.SpellVisualEffectNameHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellVisualMissileHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellVisualMissileHotfix340 hotfix = new SpellVisualMissileHotfix340();

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
            hotfix.Flags = packet.ReadUInt32("Flags", indexes);
            hotfix.SpellMissileMotionID = packet.ReadUInt16("SpellMissileMotionID", indexes);
            hotfix.AnimKitID = packet.ReadUInt32("AnimKitID", indexes);
            hotfix.SpellVisualMissileSetID = packet.ReadInt16("SpellVisualMissileSetID", indexes);

            Storage.SpellVisualMissileHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellVisualKitHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellVisualKitHotfix340 hotfix = new SpellVisualKitHotfix340();

            hotfix.ID = entry;
            hotfix.FallbackSpellVisualKitID = packet.ReadUInt32("FallbackSpellVisualKitID", indexes);
            hotfix.DelayMin = packet.ReadUInt16("DelayMin", indexes);
            hotfix.DelayMax = packet.ReadUInt16("DelayMax", indexes);
            hotfix.FallbackPriority = packet.ReadSingle("FallbackPriority", indexes);
            hotfix.Flags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.SpellVisualKitHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellXSpellVisualHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SpellXSpellVisualHotfix340 hotfix = new SpellXSpellVisualHotfix340();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.DifficultyID = packet.ReadByte("DifficultyID", indexes);
            hotfix.SpellVisualID = packet.ReadUInt32("SpellVisualID", indexes);
            hotfix.Probability = packet.ReadSingle("Probability", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.Priority = packet.ReadInt32("Priority", indexes);
            hotfix.SpellIconFileID = packet.ReadInt32("SpellIconFileID", indexes);
            hotfix.ActiveIconFileID = packet.ReadInt32("ActiveIconFileID", indexes);
            hotfix.ViewerUnitConditionID = packet.ReadUInt16("ViewerUnitConditionID", indexes);
            hotfix.ViewerPlayerConditionID = packet.ReadUInt32("ViewerPlayerConditionID", indexes);
            hotfix.CasterUnitConditionID = packet.ReadUInt16("CasterUnitConditionID", indexes);
            hotfix.CasterPlayerConditionID = packet.ReadUInt32("CasterPlayerConditionID", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);

            Storage.SpellXSpellVisualHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void SummonPropertiesHandler340(Packet packet, uint entry, params object[] indexes)
        {
            SummonPropertiesHotfix340 hotfix = new SummonPropertiesHotfix340();

            hotfix.ID = entry;
            hotfix.Control = packet.ReadInt32("Control", indexes);
            hotfix.Faction = packet.ReadInt32("Faction", indexes);
            hotfix.Title = packet.ReadInt32("Title", indexes);
            hotfix.Slot = packet.ReadInt32("Slot", indexes);
            hotfix.Flags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.SummonPropertiesHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void TactKeyHandler340(Packet packet, uint entry, params object[] indexes)
        {
            TactKeyHotfix340 hotfix = new TactKeyHotfix340();

            hotfix.ID = entry;
            hotfix.Key = new byte?[16];
            for (int i = 0; i < 16; i++)
                hotfix.Key[i] = packet.ReadByte("Key", indexes, i);

            Storage.TactKeyHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void TalentHandler340(Packet packet, uint entry, params object[] indexes)
        {
            TalentHotfix340 hotfix = new TalentHotfix340();

            hotfix.ID = entry;
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.TierID = packet.ReadByte("TierID", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.ColumnIndex = packet.ReadByte("ColumnIndex", indexes);
            hotfix.TabID = packet.ReadUInt16("TabID", indexes);
            hotfix.ClassID = packet.ReadByte("ClassID", indexes);
            hotfix.SpecID = packet.ReadUInt16("SpecID", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.OverridesSpellID = packet.ReadInt32("OverridesSpellID", indexes);
            hotfix.RequiredSpellID = packet.ReadInt32("RequiredSpellID", indexes);
            hotfix.CategoryMask = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.CategoryMask[i] = packet.ReadInt32("CategoryMask", indexes, i);
            hotfix.SpellRank = new int?[9];
            for (int i = 0; i < 9; i++)
                hotfix.SpellRank[i] = packet.ReadInt32("SpellRank", indexes, i);
            hotfix.PrereqTalent = new int?[3];
            for (int i = 0; i < 3; i++)
                hotfix.PrereqTalent[i] = packet.ReadInt32("PrereqTalent", indexes, i);
            hotfix.PrereqRank = new int?[3];
            for (int i = 0; i < 3; i++)
                hotfix.PrereqRank[i] = packet.ReadInt32("PrereqRank", indexes, i);

            Storage.TalentHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                TalentLocaleHotfix340 hotfixLocale = new TalentLocaleHotfix340
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                };
                Storage.TalentHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void TalentTabHandler340(Packet packet, uint entry, params object[] indexes)
        {
            TalentTabHotfix340 hotfix = new TalentTabHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.BackgroundFile = packet.ReadCString("BackgroundFile", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.RaceMask = packet.ReadInt32("RaceMask", indexes);
            hotfix.ClassMask = packet.ReadInt32("ClassMask", indexes);
            hotfix.PetTalentMask = packet.ReadInt32("PetTalentMask", indexes);
            hotfix.SpellIconID = packet.ReadInt32("SpellIconID", indexes);

            Storage.TalentTabHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                TalentTabLocaleHotfix340 hotfixLocale = new TalentTabLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.TalentTabHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void TaxiNodesHandler340(Packet packet, uint entry, params object[] indexes)
        {
            TaxiNodesHotfix340 hotfix = new TaxiNodesHotfix340();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.PosX = packet.ReadSingle("PosX", indexes);
            hotfix.PosY = packet.ReadSingle("PosY", indexes);
            hotfix.PosZ = packet.ReadSingle("PosZ", indexes);
            hotfix.MapOffsetX = packet.ReadSingle("MapOffsetX", indexes);
            hotfix.MapOffsetY = packet.ReadSingle("MapOffsetY", indexes);
            hotfix.FlightMapOffsetX = packet.ReadSingle("FlightMapOffsetX", indexes);
            hotfix.FlightMapOffsetY = packet.ReadSingle("FlightMapOffsetY", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ContinentID = packet.ReadUInt32("ContinentID", indexes);
            hotfix.ConditionID = packet.ReadUInt32("ConditionID", indexes);
            hotfix.CharacterBitNumber = packet.ReadUInt16("CharacterBitNumber", indexes);
            hotfix.Flags = packet.ReadUInt16("Flags", indexes);
            hotfix.UiTextureKitID = packet.ReadInt32("UiTextureKitID", indexes);
            hotfix.Facing = packet.ReadSingle("Facing", indexes);
            hotfix.SpecialIconConditionID = packet.ReadUInt32("SpecialIconConditionID", indexes);
            hotfix.VisibilityConditionID = packet.ReadUInt32("VisibilityConditionID", indexes);
            hotfix.MountCreatureID = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.MountCreatureID[i] = packet.ReadInt32("MountCreatureID", indexes, i);

            Storage.TaxiNodesHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                TaxiNodesLocaleHotfix340 hotfixLocale = new TaxiNodesLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.TaxiNodesHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void TaxiNodesHandler343(Packet packet, uint entry, params object[] indexes)
        {
            TaxiNodesHotfix343 hotfix = new TaxiNodesHotfix343();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.PosX = packet.ReadSingle("PosX", indexes);
            hotfix.PosY = packet.ReadSingle("PosY", indexes);
            hotfix.PosZ = packet.ReadSingle("PosZ", indexes);
            hotfix.MapOffsetX = packet.ReadSingle("MapOffsetX", indexes);
            hotfix.MapOffsetY = packet.ReadSingle("MapOffsetY", indexes);
            hotfix.FlightMapOffsetX = packet.ReadSingle("FlightMapOffsetX", indexes);
            hotfix.FlightMapOffsetY = packet.ReadSingle("FlightMapOffsetY", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ContinentID = packet.ReadUInt32("ContinentID", indexes);
            hotfix.ConditionID = packet.ReadUInt32("ConditionID", indexes);
            hotfix.CharacterBitNumber = packet.ReadUInt16("CharacterBitNumber", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.UiTextureKitID = packet.ReadInt32("UiTextureKitID", indexes);
            hotfix.Facing = packet.ReadSingle("Facing", indexes);
            hotfix.SpecialIconConditionID = packet.ReadUInt32("SpecialIconConditionID", indexes);
            hotfix.VisibilityConditionID = packet.ReadUInt32("VisibilityConditionID", indexes);
            hotfix.MountCreatureID = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.MountCreatureID[i] = packet.ReadInt32("MountCreatureID", indexes, i);

            Storage.TaxiNodesHotfixes343.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                TaxiNodesLocaleHotfix343 hotfixLocale = new TaxiNodesLocaleHotfix343
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.TaxiNodesHotfixesLocale343.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void TaxiPathHandler340(Packet packet, uint entry, params object[] indexes)
        {
            TaxiPathHotfix340 hotfix = new TaxiPathHotfix340();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.FromTaxiNode = packet.ReadUInt16("FromTaxiNode", indexes);
            hotfix.ToTaxiNode = packet.ReadUInt16("ToTaxiNode", indexes);
            hotfix.Cost = packet.ReadUInt32("Cost", indexes);

            Storage.TaxiPathHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void TaxiPathNodeHandler340(Packet packet, uint entry, params object[] indexes)
        {
            TaxiPathNodeHotfix340 hotfix = new TaxiPathNodeHotfix340();

            hotfix.LocX = packet.ReadSingle("LocX", indexes);
            hotfix.LocY = packet.ReadSingle("LocY", indexes);
            hotfix.LocZ = packet.ReadSingle("LocZ", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.PathID = packet.ReadUInt16("PathID", indexes);
            hotfix.NodeIndex = packet.ReadInt32("NodeIndex", indexes);
            hotfix.ContinentID = packet.ReadUInt16("ContinentID", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.Delay = packet.ReadUInt32("Delay", indexes);
            hotfix.ArrivalEventID = packet.ReadUInt32("ArrivalEventID", indexes);
            hotfix.DepartureEventID = packet.ReadUInt32("DepartureEventID", indexes);

            Storage.TaxiPathNodeHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void TaxiPathNodeHandler343(Packet packet, uint entry, params object[] indexes)
        {
            TaxiPathNodeHotfix343 hotfix = new TaxiPathNodeHotfix343();

            hotfix.LocX = packet.ReadSingle("LocX", indexes);
            hotfix.LocY = packet.ReadSingle("LocY", indexes);
            hotfix.LocZ = packet.ReadSingle("LocZ", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.PathID = packet.ReadUInt16("PathID", indexes);
            hotfix.NodeIndex = packet.ReadInt32("NodeIndex", indexes);
            hotfix.ContinentID = packet.ReadUInt16("ContinentID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.Delay = packet.ReadUInt32("Delay", indexes);
            hotfix.ArrivalEventID = packet.ReadUInt32("ArrivalEventID", indexes);
            hotfix.DepartureEventID = packet.ReadUInt32("DepartureEventID", indexes);

            Storage.TaxiPathNodeHotfixes343.Add(hotfix, packet.TimeSpan);
        }

        public static void TotemCategoryHandler340(Packet packet, uint entry, params object[] indexes)
        {
            TotemCategoryHotfix340 hotfix = new TotemCategoryHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.TotemCategoryType = packet.ReadByte("TotemCategoryType", indexes);
            hotfix.TotemCategoryMask = packet.ReadInt32("TotemCategoryMask", indexes);

            Storage.TotemCategoryHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                TotemCategoryLocaleHotfix340 hotfixLocale = new TotemCategoryLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.TotemCategoryHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ToyHandler340(Packet packet, uint entry, params object[] indexes)
        {
            ToyHotfix340 hotfix = new ToyHotfix340();

            hotfix.SourceText = packet.ReadCString("SourceText", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.SourceTypeEnum = packet.ReadSByte("SourceTypeEnum", indexes);

            Storage.ToyHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ToyLocaleHotfix340 hotfixLocale = new ToyLocaleHotfix340
                {
                    ID = hotfix.ID,
                    SourceTextLang = hotfix.SourceText,
                };
                Storage.ToyHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void TransmogHolidayHandler340(Packet packet, uint entry, params object[] indexes)
        {
            TransmogHolidayHotfix340 hotfix = new TransmogHolidayHotfix340();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.RequiredTransmogHoliday = packet.ReadInt32("RequiredTransmogHoliday", indexes);

            Storage.TransmogHolidayHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitCondHandler341(Packet packet, uint entry, params object[] indexes)
        {
            TraitCondHotfix341 hotfix = new TraitCondHotfix341();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.CondType = packet.ReadInt32("CondType", indexes);
            hotfix.TraitTreeID = packet.ReadInt32("TraitTreeID", indexes);
            hotfix.GrantedRanks = packet.ReadInt32("GrantedRanks", indexes);
            hotfix.QuestID = packet.ReadInt32("QuestID", indexes);
            hotfix.AchievementID = packet.ReadInt32("AchievementID", indexes);
            hotfix.SpecSetID = packet.ReadInt32("SpecSetID", indexes);
            hotfix.TraitNodeGroupID = packet.ReadInt32("TraitNodeGroupID", indexes);
            hotfix.TraitNodeID = packet.ReadInt32("TraitNodeID", indexes);
            hotfix.TraitCurrencyID = packet.ReadInt32("TraitCurrencyID", indexes);
            hotfix.SpentAmountRequired = packet.ReadInt32("SpentAmountRequired", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.RequiredLevel = packet.ReadInt32("RequiredLevel", indexes);
            hotfix.FreeSharedStringID = packet.ReadInt32("FreeSharedStringID", indexes);
            hotfix.SpendMoreSharedStringID = packet.ReadInt32("SpendMoreSharedStringID", indexes);

            Storage.TraitCondHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitCostHandler341(Packet packet, uint entry, params object[] indexes)
        {
            TraitCostHotfix341 hotfix = new TraitCostHotfix341();

            hotfix.InternalName = packet.ReadCString("InternalName", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Amount = packet.ReadInt32("Amount", indexes);
            hotfix.TraitCurrencyID = packet.ReadInt32("TraitCurrencyID", indexes);

            Storage.TraitCostHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitCurrencyHandler341(Packet packet, uint entry, params object[] indexes)
        {
            TraitCurrencyHotfix341 hotfix = new TraitCurrencyHotfix341();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Type = packet.ReadInt32("Type", indexes);
            hotfix.CurrencyTypesID = packet.ReadInt32("CurrencyTypesID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.Icon = packet.ReadInt32("Icon", indexes);

            Storage.TraitCurrencyHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitCurrencySourceHandler341(Packet packet, uint entry, params object[] indexes)
        {
            TraitCurrencySourceHotfix341 hotfix = new TraitCurrencySourceHotfix341();

            hotfix.Requirement = packet.ReadCString("Requirement", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitCurrencyID = packet.ReadInt32("TraitCurrencyID", indexes);
            hotfix.Amount = packet.ReadInt32("Amount", indexes);
            hotfix.QuestID = packet.ReadInt32("QuestID", indexes);
            hotfix.AchievementID = packet.ReadInt32("AchievementID", indexes);
            hotfix.PlayerLevel = packet.ReadInt32("PlayerLevel", indexes);
            hotfix.TraitNodeEntryID = packet.ReadInt32("TraitNodeEntryID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);

            Storage.TraitCurrencySourceHotfixes341.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                TraitCurrencySourceLocaleHotfix341 hotfixLocale = new TraitCurrencySourceLocaleHotfix341
                {
                    ID = hotfix.ID,
                    RequirementLang = hotfix.Requirement,
                };
                Storage.TraitCurrencySourceHotfixesLocale341.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void TraitDefinitionHandler341(Packet packet, uint entry, params object[] indexes)
        {
            TraitDefinitionHotfix341 hotfix = new TraitDefinitionHotfix341();

            hotfix.OverrideName = packet.ReadCString("OverrideName", indexes);
            hotfix.OverrideSubtext = packet.ReadCString("OverrideSubtext", indexes);
            hotfix.OverrideDescription = packet.ReadCString("OverrideDescription", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.OverrideIcon = packet.ReadInt32("OverrideIcon", indexes);
            hotfix.OverridesSpellID = packet.ReadInt32("OverridesSpellID", indexes);
            hotfix.VisibleSpellID = packet.ReadInt32("VisibleSpellID", indexes);

            Storage.TraitDefinitionHotfixes341.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                TraitDefinitionLocaleHotfix341 hotfixLocale = new TraitDefinitionLocaleHotfix341
                {
                    ID = hotfix.ID,
                    OverrideNameLang = hotfix.OverrideName,
                    OverrideSubtextLang = hotfix.OverrideSubtext,
                    OverrideDescriptionLang = hotfix.OverrideDescription,
                };
                Storage.TraitDefinitionHotfixesLocale341.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void TraitDefinitionEffectPointsHandler341(Packet packet, uint entry, params object[] indexes)
        {
            TraitDefinitionEffectPointsHotfix341 hotfix = new TraitDefinitionEffectPointsHotfix341();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitDefinitionID = packet.ReadInt32("TraitDefinitionID", indexes);
            hotfix.EffectIndex = packet.ReadInt32("EffectIndex", indexes);
            hotfix.OperationType = packet.ReadInt32("OperationType", indexes);
            hotfix.CurveID = packet.ReadInt32("CurveID", indexes);

            Storage.TraitDefinitionEffectPointsHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitEdgeHandler341(Packet packet, uint entry, params object[] indexes)
        {
            TraitEdgeHotfix341 hotfix = new TraitEdgeHotfix341();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.VisualStyle = packet.ReadInt32("VisualStyle", indexes);
            hotfix.LeftTraitNodeID = packet.ReadInt32("LeftTraitNodeID", indexes);
            hotfix.RightTraitNodeID = packet.ReadInt32("RightTraitNodeID", indexes);
            hotfix.Type = packet.ReadInt32("Type", indexes);

            Storage.TraitEdgeHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitNodeHandler341(Packet packet, uint entry, params object[] indexes)
        {
            TraitNodeHotfix341 hotfix = new TraitNodeHotfix341();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitTreeID = packet.ReadInt32("TraitTreeID", indexes);
            hotfix.PosX = packet.ReadInt32("PosX", indexes);
            hotfix.PosY = packet.ReadInt32("PosY", indexes);
            hotfix.Type = packet.ReadByte("Type", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.TraitNodeHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitNodeEntryHandler341(Packet packet, uint entry, params object[] indexes)
        {
            TraitNodeEntryHotfix341 hotfix = new TraitNodeEntryHotfix341();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitDefinitionID = packet.ReadInt32("TraitDefinitionID", indexes);
            hotfix.MaxRanks = packet.ReadInt32("MaxRanks", indexes);
            hotfix.NodeEntryType = packet.ReadByte("NodeEntryType", indexes);

            Storage.TraitNodeEntryHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitNodeEntryXTraitCondHandler341(Packet packet, uint entry, params object[] indexes)
        {
            TraitNodeEntryXTraitCondHotfix341 hotfix = new TraitNodeEntryXTraitCondHotfix341();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitCondID = packet.ReadInt32("TraitCondID", indexes);
            hotfix.TraitNodeEntryID = packet.ReadInt32("TraitNodeEntryID", indexes);

            Storage.TraitNodeEntryXTraitCondHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitNodeEntryXTraitCostHandler341(Packet packet, uint entry, params object[] indexes)
        {
            TraitNodeEntryXTraitCostHotfix341 hotfix = new TraitNodeEntryXTraitCostHotfix341();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitNodeEntryID = packet.ReadInt32("TraitNodeEntryID", indexes);
            hotfix.TraitCostID = packet.ReadInt32("TraitCostID", indexes);

            Storage.TraitNodeEntryXTraitCostHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitNodeGroupHandler341(Packet packet, uint entry, params object[] indexes)
        {
            TraitNodeGroupHotfix341 hotfix = new TraitNodeGroupHotfix341();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitTreeID = packet.ReadInt32("TraitTreeID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.TraitNodeGroupHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitNodeGroupXTraitCondHandler341(Packet packet, uint entry, params object[] indexes)
        {
            TraitNodeGroupXTraitCondHotfix341 hotfix = new TraitNodeGroupXTraitCondHotfix341();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitCondID = packet.ReadInt32("TraitCondID", indexes);
            hotfix.TraitNodeGroupID = packet.ReadInt32("TraitNodeGroupID", indexes);

            Storage.TraitNodeGroupXTraitCondHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitNodeGroupXTraitCostHandler341(Packet packet, uint entry, params object[] indexes)
        {
            TraitNodeGroupXTraitCostHotfix341 hotfix = new TraitNodeGroupXTraitCostHotfix341();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitNodeGroupID = packet.ReadInt32("TraitNodeGroupID", indexes);
            hotfix.TraitCostID = packet.ReadInt32("TraitCostID", indexes);

            Storage.TraitNodeGroupXTraitCostHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitNodeGroupXTraitNodeHandler341(Packet packet, uint entry, params object[] indexes)
        {
            TraitNodeGroupXTraitNodeHotfix341 hotfix = new TraitNodeGroupXTraitNodeHotfix341();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitNodeGroupID = packet.ReadInt32("TraitNodeGroupID", indexes);
            hotfix.TraitNodeID = packet.ReadInt32("TraitNodeID", indexes);
            hotfix.Index = packet.ReadInt32("Index", indexes);

            Storage.TraitNodeGroupXTraitNodeHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitNodeXTraitCondHandler341(Packet packet, uint entry, params object[] indexes)
        {
            TraitNodeXTraitCondHotfix341 hotfix = new TraitNodeXTraitCondHotfix341();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitCondID = packet.ReadInt32("TraitCondID", indexes);
            hotfix.TraitNodeID = packet.ReadInt32("TraitNodeID", indexes);

            Storage.TraitNodeXTraitCondHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitNodeXTraitCostHandler341(Packet packet, uint entry, params object[] indexes)
        {
            TraitNodeXTraitCostHotfix341 hotfix = new TraitNodeXTraitCostHotfix341();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitNodeID = packet.ReadInt32("TraitNodeID", indexes);
            hotfix.TraitCostID = packet.ReadInt32("TraitCostID", indexes);

            Storage.TraitNodeXTraitCostHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitNodeXTraitNodeEntryHandler341(Packet packet, uint entry, params object[] indexes)
        {
            TraitNodeXTraitNodeEntryHotfix341 hotfix = new TraitNodeXTraitNodeEntryHotfix341();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitNodeID = packet.ReadInt32("TraitNodeID", indexes);
            hotfix.TraitNodeEntryID = packet.ReadInt32("TraitNodeEntryID", indexes);
            hotfix.Index = packet.ReadInt32("Index", indexes);

            Storage.TraitNodeXTraitNodeEntryHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitTreeHandler341(Packet packet, uint entry, params object[] indexes)
        {
            TraitTreeHotfix341 hotfix = new TraitTreeHotfix341();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitSystemID = packet.ReadInt32("TraitSystemID", indexes);
            hotfix.Unused1000_1 = packet.ReadInt32("Unused1000_1", indexes);
            hotfix.FirstTraitNodeID = packet.ReadInt32("FirstTraitNodeID", indexes);
            hotfix.PlayerConditionID = packet.ReadInt32("PlayerConditionID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.Unused1000_2 = packet.ReadSingle("Unused1000_2", indexes);
            hotfix.Unused1000_3 = packet.ReadSingle("Unused1000_3", indexes);

            Storage.TraitTreeHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitTreeLoadoutHandler341(Packet packet, uint entry, params object[] indexes)
        {
            TraitTreeLoadoutHotfix341 hotfix = new TraitTreeLoadoutHotfix341();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitTreeID = packet.ReadInt32("TraitTreeID", indexes);
            hotfix.ChrSpecializationID = packet.ReadInt32("ChrSpecializationID", indexes);

            Storage.TraitTreeLoadoutHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitTreeLoadoutEntryHandler341(Packet packet, uint entry, params object[] indexes)
        {
            TraitTreeLoadoutEntryHotfix341 hotfix = new TraitTreeLoadoutEntryHotfix341();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitTreeLoadoutID = packet.ReadInt32("TraitTreeLoadoutID", indexes);
            hotfix.SelectedTraitNodeID = packet.ReadInt32("SelectedTraitNodeID", indexes);
            hotfix.SelectedTraitNodeEntryID = packet.ReadInt32("SelectedTraitNodeEntryID", indexes);
            hotfix.NumPoints = packet.ReadInt32("NumPoints", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);

            Storage.TraitTreeLoadoutEntryHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitTreeXTraitCostHandler341(Packet packet, uint entry, params object[] indexes)
        {
            TraitTreeXTraitCostHotfix341 hotfix = new TraitTreeXTraitCostHotfix341();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TraitTreeID = packet.ReadInt32("TraitTreeID", indexes);
            hotfix.TraitCostID = packet.ReadInt32("TraitCostID", indexes);

            Storage.TraitTreeXTraitCostHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void TraitTreeXTraitCurrencyHandler341(Packet packet, uint entry, params object[] indexes)
        {
            TraitTreeXTraitCurrencyHotfix341 hotfix = new TraitTreeXTraitCurrencyHotfix341();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Index = packet.ReadInt32("Index", indexes);
            hotfix.TraitTreeID = packet.ReadInt32("TraitTreeID", indexes);
            hotfix.TraitCurrencyID = packet.ReadInt32("TraitCurrencyID", indexes);

            Storage.TraitTreeXTraitCurrencyHotfixes341.Add(hotfix, packet.TimeSpan);
        }

        public static void TransmogSetHandler340(Packet packet, uint entry, params object[] indexes)
        {
            TransmogSetHotfix340 hotfix = new TransmogSetHotfix340();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ClassMask = packet.ReadInt32("ClassMask", indexes);
            hotfix.TrackingQuestID = packet.ReadUInt32("TrackingQuestID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.TransmogSetGroupID = packet.ReadUInt32("TransmogSetGroupID", indexes);
            hotfix.ItemNameDescriptionID = packet.ReadInt32("ItemNameDescriptionID", indexes);
            hotfix.ParentTransmogSetID = packet.ReadUInt16("ParentTransmogSetID", indexes);
            hotfix.ExpansionID = packet.ReadByte("ExpansionID", indexes);
            hotfix.UiOrder = packet.ReadInt16("UiOrder", indexes);

            Storage.TransmogSetHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                TransmogSetLocaleHotfix340 hotfixLocale = new TransmogSetLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.TransmogSetHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void TransmogSetGroupHandler340(Packet packet, uint entry, params object[] indexes)
        {
            TransmogSetGroupHotfix340 hotfix = new TransmogSetGroupHotfix340();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);

            Storage.TransmogSetGroupHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                TransmogSetGroupLocaleHotfix340 hotfixLocale = new TransmogSetGroupLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.TransmogSetGroupHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void TransmogSetItemHandler340(Packet packet, uint entry, params object[] indexes)
        {
            TransmogSetItemHotfix340 hotfix = new TransmogSetItemHotfix340();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TransmogSetID = packet.ReadUInt32("TransmogSetID", indexes);
            hotfix.ItemModifiedAppearanceID = packet.ReadUInt32("ItemModifiedAppearanceID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.TransmogSetItemHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void TransportAnimationHandler340(Packet packet, uint entry, params object[] indexes)
        {
            TransportAnimationHotfix340 hotfix = new TransportAnimationHotfix340();

            hotfix.ID = entry;
            hotfix.PosX = packet.ReadSingle("PosX", indexes);
            hotfix.PosY = packet.ReadSingle("PosY", indexes);
            hotfix.PosZ = packet.ReadSingle("PosZ", indexes);
            hotfix.SequenceID = packet.ReadByte("SequenceID", indexes);
            hotfix.TimeIndex = packet.ReadUInt32("TimeIndex", indexes);
            hotfix.TransportID = packet.ReadInt32("TransportID", indexes);

            Storage.TransportAnimationHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void TransportRotationHandler340(Packet packet, uint entry, params object[] indexes)
        {
            TransportRotationHotfix340 hotfix = new TransportRotationHotfix340();

            hotfix.ID = entry;
            hotfix.Rot = new float?[4];
            for (int i = 0; i < 4; i++)
                hotfix.Rot[i] = packet.ReadSingle("Rot", indexes, i);
            hotfix.TimeIndex = packet.ReadUInt32("TimeIndex", indexes);
            hotfix.GameObjectsID = packet.ReadInt32("GameObjectsID", indexes);

            Storage.TransportRotationHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void UiMapHandler340(Packet packet, uint entry, params object[] indexes)
        {
            UiMapHotfix340 hotfix = new UiMapHotfix340();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ParentUiMapID = packet.ReadInt32("ParentUiMapID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.System = packet.ReadUInt32("System", indexes);
            hotfix.Type = packet.ReadUInt32("Type", indexes);
            hotfix.BountySetID = packet.ReadInt32("BountySetID", indexes);
            hotfix.BountyDisplayLocation = packet.ReadUInt32("BountyDisplayLocation", indexes);
            hotfix.VisibilityPlayerConditionID = packet.ReadInt32("VisibilityPlayerConditionID", indexes);
            hotfix.HelpTextPosition = packet.ReadSByte("HelpTextPosition", indexes);
            hotfix.BkgAtlasID = packet.ReadInt32("BkgAtlasID", indexes);
            hotfix.LevelRangeMin = packet.ReadUInt32("LevelRangeMin", indexes);
            hotfix.LevelRangeMax = packet.ReadUInt32("LevelRangeMax", indexes);

            Storage.UiMapHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                UiMapLocaleHotfix340 hotfixLocale = new UiMapLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.UiMapHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void UiMapHandler343(Packet packet, uint entry, params object[] indexes)
        {
            UiMapHotfix343 hotfix = new UiMapHotfix343();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ParentUiMapID = packet.ReadInt32("ParentUiMapID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.System = packet.ReadByte("System", indexes);
            hotfix.Type = packet.ReadByte("Type", indexes);
            hotfix.BountySetID = packet.ReadInt32("BountySetID", indexes);
            hotfix.BountyDisplayLocation = packet.ReadUInt32("BountyDisplayLocation", indexes);
            hotfix.VisibilityPlayerConditionID2 = packet.ReadInt32("VisibilityPlayerConditionID2", indexes);
            hotfix.VisibilityPlayerConditionID = packet.ReadInt32("VisibilityPlayerConditionID", indexes);
            hotfix.HelpTextPosition = packet.ReadSByte("HelpTextPosition", indexes);
            hotfix.BkgAtlasID = packet.ReadInt32("BkgAtlasID", indexes);
            hotfix.AlternateUiMapGroup = packet.ReadUInt32("AlternateUiMapGroup", indexes);
            hotfix.ContentTuningID = packet.ReadUInt32("ContentTuningID", indexes);

            Storage.UiMapHotfixes343.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                UiMapLocaleHotfix343 hotfixLocale = new UiMapLocaleHotfix343
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.UiMapHotfixesLocale343.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void UiMapAssignmentHandler340(Packet packet, uint entry, params object[] indexes)
        {
            UiMapAssignmentHotfix340 hotfix = new UiMapAssignmentHotfix340();

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
            hotfix.UiMapID = packet.ReadInt32("UiMapID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.MapID = packet.ReadInt32("MapID", indexes);
            hotfix.AreaID = packet.ReadInt32("AreaID", indexes);
            hotfix.WmoDoodadPlacementID = packet.ReadInt32("WmoDoodadPlacementID", indexes);
            hotfix.WmoGroupID = packet.ReadInt32("WmoGroupID", indexes);

            Storage.UiMapAssignmentHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void UiMapLinkHandler340(Packet packet, uint entry, params object[] indexes)
        {
            UiMapLinkHotfix340 hotfix = new UiMapLinkHotfix340();

            hotfix.UiMinX = packet.ReadSingle("UiMinX", indexes);
            hotfix.UiMinY = packet.ReadSingle("UiMinY", indexes);
            hotfix.UiMaxX = packet.ReadSingle("UiMaxX", indexes);
            hotfix.UiMaxY = packet.ReadSingle("UiMaxY", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ParentUiMapID = packet.ReadInt32("ParentUiMapID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.ChildUiMapID = packet.ReadInt32("ChildUiMapID", indexes);
            hotfix.OverrideHighlightFileDataID = packet.ReadInt32("OverrideHighlightFileDataID", indexes);
            hotfix.OverrideHighlightAtlasID = packet.ReadInt32("OverrideHighlightAtlasID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.UiMapLinkHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void UiMapXMapArtHandler340(Packet packet, uint entry, params object[] indexes)
        {
            UiMapXMapArtHotfix340 hotfix = new UiMapXMapArtHotfix340();

            hotfix.ID = entry;
            hotfix.PhaseID = packet.ReadInt32("PhaseID", indexes);
            hotfix.UiMapArtID = packet.ReadInt32("UiMapArtID", indexes);
            hotfix.UiMapID = packet.ReadInt32("UiMapID", indexes);

            Storage.UiMapXMapArtHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void UnitConditionHandler340(Packet packet, uint entry, params object[] indexes)
        {
            UnitConditionHotfix340 hotfix = new UnitConditionHotfix340();

            hotfix.ID = entry;
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.Variable = new byte?[8];
            for (int i = 0; i < 8; i++)
                hotfix.Variable[i] = packet.ReadByte("Variable", indexes, i);
            hotfix.Op = new sbyte?[8];
            for (int i = 0; i < 8; i++)
                hotfix.Op[i] = packet.ReadSByte("Op", indexes, i);
            hotfix.Value = new int?[8];
            for (int i = 0; i < 8; i++)
                hotfix.Value[i] = packet.ReadInt32("Value", indexes, i);

            Storage.UnitConditionHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void UnitPowerBarHandler340(Packet packet, uint entry, params object[] indexes)
        {
            UnitPowerBarHotfix340 hotfix = new UnitPowerBarHotfix340();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Cost = packet.ReadCString("Cost", indexes);
            hotfix.OutOfError = packet.ReadCString("OutOfError", indexes);
            hotfix.ToolTip = packet.ReadCString("ToolTip", indexes);
            hotfix.MinPower = packet.ReadUInt32("MinPower", indexes);
            hotfix.MaxPower = packet.ReadUInt32("MaxPower", indexes);
            hotfix.StartPower = packet.ReadUInt16("StartPower", indexes);
            hotfix.CenterPower = packet.ReadByte("CenterPower", indexes);
            hotfix.RegenerationPeace = packet.ReadSingle("RegenerationPeace", indexes);
            hotfix.RegenerationCombat = packet.ReadSingle("RegenerationCombat", indexes);
            hotfix.BarType = packet.ReadByte("BarType", indexes);
            hotfix.Flags = packet.ReadUInt16("Flags", indexes);
            hotfix.StartInset = packet.ReadSingle("StartInset", indexes);
            hotfix.EndInset = packet.ReadSingle("EndInset", indexes);
            hotfix.FileDataID = new int?[6];
            for (int i = 0; i < 6; i++)
                hotfix.FileDataID[i] = packet.ReadInt32("FileDataID", indexes, i);
            hotfix.Color = new int?[6];
            for (int i = 0; i < 6; i++)
                hotfix.Color[i] = packet.ReadInt32("Color", indexes, i);

            Storage.UnitPowerBarHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                UnitPowerBarLocaleHotfix340 hotfixLocale = new UnitPowerBarLocaleHotfix340
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    CostLang = hotfix.Cost,
                    OutOfErrorLang = hotfix.OutOfError,
                    ToolTipLang = hotfix.ToolTip,
                };
                Storage.UnitPowerBarHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void VehicleHandler340(Packet packet, uint entry, params object[] indexes)
        {
            VehicleHotfix340 hotfix = new VehicleHotfix340();

            hotfix.ID = entry;
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.FlagsB = packet.ReadByte("FlagsB", indexes);
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
            hotfix.UiLocomotionType = packet.ReadByte("UiLocomotionType", indexes);
            hotfix.SeatID = new ushort?[8];
            for (int i = 0; i < 8; i++)
                hotfix.SeatID[i] = packet.ReadUInt16("SeatID", indexes, i);
            hotfix.PowerDisplayID = new ushort?[3];
            for (int i = 0; i < 3; i++)
                hotfix.PowerDisplayID[i] = packet.ReadUInt16("PowerDisplayID", indexes, i);

            Storage.VehicleHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void VehicleHandler342(Packet packet, uint entry, params object[] indexes)
        {
            VehicleHotfix342 hotfix = new VehicleHotfix342();

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
            hotfix.UiLocomotionType = packet.ReadInt32("UiLocomotionType", indexes);
            hotfix.SeatID = new ushort?[8];
            for (int i = 0; i < 8; i++)
                hotfix.SeatID[i] = packet.ReadUInt16("SeatID", indexes, i);
            hotfix.PowerDisplayID = new ushort?[3];
            for (int i = 0; i < 3; i++)
                hotfix.PowerDisplayID[i] = packet.ReadUInt16("PowerDisplayID", indexes, i);

            Storage.VehicleHotfixes342.Add(hotfix, packet.TimeSpan);
        }

        public static void VehicleSeatHandler340(Packet packet, uint entry, params object[] indexes)
        {
            VehicleSeatHotfix340 hotfix = new VehicleSeatHotfix340();

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
            hotfix.AttachmentID = packet.ReadSByte("AttachmentID", indexes);
            hotfix.EnterPreDelay = packet.ReadSingle("EnterPreDelay", indexes);
            hotfix.EnterSpeed = packet.ReadSingle("EnterSpeed", indexes);
            hotfix.EnterGravity = packet.ReadSingle("EnterGravity", indexes);
            hotfix.EnterMinDuration = packet.ReadSingle("EnterMinDuration", indexes);
            hotfix.EnterMaxDuration = packet.ReadSingle("EnterMaxDuration", indexes);
            hotfix.EnterMinArcHeight = packet.ReadSingle("EnterMinArcHeight", indexes);
            hotfix.EnterMaxArcHeight = packet.ReadSingle("EnterMaxArcHeight", indexes);
            hotfix.EnterAnimStart = packet.ReadInt32("EnterAnimStart", indexes);
            hotfix.EnterAnimLoop = packet.ReadInt32("EnterAnimLoop", indexes);
            hotfix.RideAnimStart = packet.ReadInt32("RideAnimStart", indexes);
            hotfix.RideAnimLoop = packet.ReadInt32("RideAnimLoop", indexes);
            hotfix.RideUpperAnimStart = packet.ReadInt32("RideUpperAnimStart", indexes);
            hotfix.RideUpperAnimLoop = packet.ReadInt32("RideUpperAnimLoop", indexes);
            hotfix.ExitPreDelay = packet.ReadSingle("ExitPreDelay", indexes);
            hotfix.ExitSpeed = packet.ReadSingle("ExitSpeed", indexes);
            hotfix.ExitGravity = packet.ReadSingle("ExitGravity", indexes);
            hotfix.ExitMinDuration = packet.ReadSingle("ExitMinDuration", indexes);
            hotfix.ExitMaxDuration = packet.ReadSingle("ExitMaxDuration", indexes);
            hotfix.ExitMinArcHeight = packet.ReadSingle("ExitMinArcHeight", indexes);
            hotfix.ExitMaxArcHeight = packet.ReadSingle("ExitMaxArcHeight", indexes);
            hotfix.ExitAnimStart = packet.ReadInt32("ExitAnimStart", indexes);
            hotfix.ExitAnimLoop = packet.ReadInt32("ExitAnimLoop", indexes);
            hotfix.ExitAnimEnd = packet.ReadInt32("ExitAnimEnd", indexes);
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
            hotfix.UiSkin = packet.ReadInt32("UiSkin", indexes);
            hotfix.CameraEnteringDelay = packet.ReadSingle("CameraEnteringDelay", indexes);
            hotfix.CameraEnteringDuration = packet.ReadSingle("CameraEnteringDuration", indexes);
            hotfix.CameraExitingDelay = packet.ReadSingle("CameraExitingDelay", indexes);
            hotfix.CameraExitingDuration = packet.ReadSingle("CameraExitingDuration", indexes);
            hotfix.CameraPosChaseRate = packet.ReadSingle("CameraPosChaseRate", indexes);
            hotfix.CameraFacingChaseRate = packet.ReadSingle("CameraFacingChaseRate", indexes);
            hotfix.CameraEnteringZoom = packet.ReadSingle("CameraEnteringZoom", indexes);
            hotfix.CameraSeatZoomMin = packet.ReadSingle("CameraSeatZoomMin", indexes);
            hotfix.CameraSeatZoomMax = packet.ReadSingle("CameraSeatZoomMax", indexes);
            hotfix.EnterAnimKitID = packet.ReadInt16("EnterAnimKitID", indexes);
            hotfix.RideAnimKitID = packet.ReadInt16("RideAnimKitID", indexes);
            hotfix.ExitAnimKitID = packet.ReadInt16("ExitAnimKitID", indexes);
            hotfix.VehicleEnterAnimKitID = packet.ReadInt16("VehicleEnterAnimKitID", indexes);
            hotfix.VehicleRideAnimKitID = packet.ReadInt16("VehicleRideAnimKitID", indexes);
            hotfix.VehicleExitAnimKitID = packet.ReadInt16("VehicleExitAnimKitID", indexes);
            hotfix.CameraModeID = packet.ReadInt16("CameraModeID", indexes);

            Storage.VehicleSeatHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void VehicleSeatHandler342(Packet packet, uint entry, params object[] indexes)
        {
            VehicleSeatHotfix342 hotfix = new VehicleSeatHotfix342();

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
            hotfix.AttachmentID = packet.ReadSByte("AttachmentID", indexes);
            hotfix.EnterPreDelay = packet.ReadSingle("EnterPreDelay", indexes);
            hotfix.EnterSpeed = packet.ReadSingle("EnterSpeed", indexes);
            hotfix.EnterGravity = packet.ReadSingle("EnterGravity", indexes);
            hotfix.EnterMinDuration = packet.ReadSingle("EnterMinDuration", indexes);
            hotfix.EnterMaxDuration = packet.ReadSingle("EnterMaxDuration", indexes);
            hotfix.EnterMinArcHeight = packet.ReadSingle("EnterMinArcHeight", indexes);
            hotfix.EnterMaxArcHeight = packet.ReadSingle("EnterMaxArcHeight", indexes);
            hotfix.EnterAnimStart = packet.ReadInt32("EnterAnimStart", indexes);
            hotfix.EnterAnimLoop = packet.ReadInt32("EnterAnimLoop", indexes);
            hotfix.RideAnimStart = packet.ReadInt32("RideAnimStart", indexes);
            hotfix.RideAnimLoop = packet.ReadInt32("RideAnimLoop", indexes);
            hotfix.RideUpperAnimStart = packet.ReadInt32("RideUpperAnimStart", indexes);
            hotfix.RideUpperAnimLoop = packet.ReadInt32("RideUpperAnimLoop", indexes);
            hotfix.ExitPreDelay = packet.ReadSingle("ExitPreDelay", indexes);
            hotfix.ExitSpeed = packet.ReadSingle("ExitSpeed", indexes);
            hotfix.ExitGravity = packet.ReadSingle("ExitGravity", indexes);
            hotfix.ExitMinDuration = packet.ReadSingle("ExitMinDuration", indexes);
            hotfix.ExitMaxDuration = packet.ReadSingle("ExitMaxDuration", indexes);
            hotfix.ExitMinArcHeight = packet.ReadSingle("ExitMinArcHeight", indexes);
            hotfix.ExitMaxArcHeight = packet.ReadSingle("ExitMaxArcHeight", indexes);
            hotfix.ExitAnimStart = packet.ReadInt32("ExitAnimStart", indexes);
            hotfix.ExitAnimLoop = packet.ReadInt32("ExitAnimLoop", indexes);
            hotfix.ExitAnimEnd = packet.ReadInt32("ExitAnimEnd", indexes);
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
            hotfix.UiSkin = packet.ReadInt32("UiSkin", indexes);
            hotfix.CameraEnteringDelay = packet.ReadSingle("CameraEnteringDelay", indexes);
            hotfix.CameraEnteringDuration = packet.ReadSingle("CameraEnteringDuration", indexes);
            hotfix.CameraExitingDelay = packet.ReadSingle("CameraExitingDelay", indexes);
            hotfix.CameraExitingDuration = packet.ReadSingle("CameraExitingDuration", indexes);
            hotfix.CameraPosChaseRate = packet.ReadSingle("CameraPosChaseRate", indexes);
            hotfix.CameraFacingChaseRate = packet.ReadSingle("CameraFacingChaseRate", indexes);
            hotfix.CameraEnteringZoom = packet.ReadSingle("CameraEnteringZoom", indexes);
            hotfix.CameraSeatZoomMin = packet.ReadSingle("CameraSeatZoomMin", indexes);
            hotfix.CameraSeatZoomMax = packet.ReadSingle("CameraSeatZoomMax", indexes);
            hotfix.EnterAnimKitID = packet.ReadInt16("EnterAnimKitID", indexes);
            hotfix.RideAnimKitID = packet.ReadInt16("RideAnimKitID", indexes);
            hotfix.ExitAnimKitID = packet.ReadInt16("ExitAnimKitID", indexes);
            hotfix.VehicleEnterAnimKitID = packet.ReadInt16("VehicleEnterAnimKitID", indexes);
            hotfix.VehicleRideAnimKitID = packet.ReadInt16("VehicleRideAnimKitID", indexes);
            hotfix.VehicleExitAnimKitID = packet.ReadInt16("VehicleExitAnimKitID", indexes);
            hotfix.CameraModeID = packet.ReadInt16("CameraModeID", indexes);

            Storage.VehicleSeatHotfixes342.Add(hotfix, packet.TimeSpan);
        }

        public static void WmoAreaTableHandler340(Packet packet, uint entry, params object[] indexes)
        {
            WmoAreaTableHotfix340 hotfix = new WmoAreaTableHotfix340();

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
            hotfix.Flags = packet.ReadByte("Flags", indexes);

            Storage.WmoAreaTableHotfixes340.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                WmoAreaTableLocaleHotfix340 hotfixLocale = new WmoAreaTableLocaleHotfix340
                {
                    ID = hotfix.ID,
                    AreaNameLang = hotfix.AreaName,
                };
                Storage.WmoAreaTableHotfixesLocale340.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void WorldEffectHandler340(Packet packet, uint entry, params object[] indexes)
        {
            WorldEffectHotfix340 hotfix = new WorldEffectHotfix340();

            hotfix.ID = entry;
            hotfix.QuestFeedbackEffectID = packet.ReadUInt32("QuestFeedbackEffectID", indexes);
            hotfix.WhenToDisplay = packet.ReadByte("WhenToDisplay", indexes);
            hotfix.TargetType = packet.ReadByte("TargetType", indexes);
            hotfix.TargetAsset = packet.ReadInt32("TargetAsset", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.CombatConditionID = packet.ReadUInt16("CombatConditionID", indexes);

            Storage.WorldEffectHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void WorldMapOverlayHandler340(Packet packet, uint entry, params object[] indexes)
        {
            WorldMapOverlayHotfix340 hotfix = new WorldMapOverlayHotfix340();

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
            hotfix.Flags = packet.ReadUInt32("Flags", indexes);
            hotfix.AreaID = new uint?[4];
            for (int i = 0; i < 4; i++)
                hotfix.AreaID[i] = packet.ReadUInt32("AreaID", indexes, i);

            Storage.WorldMapOverlayHotfixes340.Add(hotfix, packet.TimeSpan);
        }

        public static void WorldStateExpressionHandler340(Packet packet, uint entry, params object[] indexes)
        {
            WorldStateExpressionHotfix340 hotfix = new WorldStateExpressionHotfix340();

            hotfix.ID = entry;
            hotfix.Expression = packet.ReadCString("Expression", indexes);

            Storage.WorldStateExpressionHotfixes340.Add(hotfix, packet.TimeSpan);
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

                switch (status)
                {
                    case HotfixStatus.Valid:
                    {
                        packet.AddSniffData(StoreNameType.None, entry, type.ToString());

                        switch (type)
                        {
                            case DB2Hash.Achievement:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51666))
                                    AchievementHandler343(db2File, (uint)entry, count);
                                else
                                    AchievementHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.AchievementCategory:
                            {
                                AchievementCategoryHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.AdventureJournal:
                            {
                                AdventureJournalHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.AdventureMapPoi:
                            {
                                AdventureMapPOIHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.AnimationData:
                            {
                                AnimationDataHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.AnimKit:
                            {
                                AnimKitHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.AreaGroupMember:
                            {
                                AreaGroupMemberHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.AreaTable:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51666))
                                    AreaTableHandler343(db2File, (uint)entry, count);
                                else
                                    AreaTableHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.AreaTrigger:
                            {
                                AreaTriggerHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ArmorLocation:
                            {
                                ArmorLocationHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.Artifact:
                            {
                                ArtifactHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ArtifactAppearance:
                            {
                                ArtifactAppearanceHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ArtifactAppearanceSet:
                            {
                                ArtifactAppearanceSetHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ArtifactCategory:
                            {
                                ArtifactCategoryHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ArtifactPower:
                            {
                                ArtifactPowerHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ArtifactPowerLink:
                            {
                                ArtifactPowerLinkHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ArtifactPowerPicker:
                            {
                                ArtifactPowerPickerHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ArtifactPowerRank:
                            {
                                ArtifactPowerRankHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ArtifactQuestXp:
                            {
                                ArtifactQuestXpHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ArtifactTier:
                            {
                                ArtifactTierHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ArtifactUnlock:
                            {
                                ArtifactUnlockHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.AuctionHouse:
                            {
                                AuctionHouseHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.AzeriteEmpoweredItem:
                            {
                                AzeriteEmpoweredItemHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.AzeriteEssence:
                            {
                                AzeriteEssenceHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.AzeriteEssencePower:
                            {
                                AzeriteEssencePowerHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.AzeriteItem:
                            {
                                AzeriteItemHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.AzeriteItemMilestonePower:
                            {
                                AzeriteItemMilestonePowerHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.AzeriteKnowledgeMultiplier:
                            {
                                AzeriteKnowledgeMultiplierHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.AzeriteLevelInfo:
                            {
                                AzeriteLevelInfoHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.AzeritePower:
                            {
                                AzeritePowerHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.AzeritePowerSetMember:
                            {
                                AzeritePowerSetMemberHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.AzeriteTierUnlock:
                            {
                                AzeriteTierUnlockHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.AzeriteTierUnlockSet:
                            {
                                AzeriteTierUnlockSetHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.BankBagSlotPrices:
                            {
                                BankBagSlotPricesHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.BannedAddons:
                            {
                                BannedAddonsHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.BarberShopStyle:
                            {
                                BarberShopStyleHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.BattlePetAbility:
                            {
                                BattlePetAbilityHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.BattlePetBreedQuality:
                            {
                                BattlePetBreedQualityHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.BattlePetBreedState:
                            {
                                BattlePetBreedStateHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.BattlePetSpecies:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    BattlePetSpeciesHandler341(db2File, (uint)entry, count);
                                else
                                    BattlePetSpeciesHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.BattlePetSpeciesState:
                            {
                                BattlePetSpeciesStateHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.BattlemasterList:
                            {
                                BattlemasterListHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.BroadcastText:
                            {
                                BroadcastTextHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.CfgCategories:
                            {
                                CfgCategoriesHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.CfgRegions:
                            {
                                CfgRegionsHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.CharTitles:
                            {
                                CharTitlesHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.CharacterLoadout:
                            {
                                CharacterLoadoutHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.CharacterLoadoutItem:
                            {
                                CharacterLoadoutItemHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ChatChannels:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51666))
                                    ChatChannelsHandler343(db2File, (uint)entry, count);
                                else
                                    ChatChannelsHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ChrClassUiDisplay:
                            {
                                ChrClassUiDisplayHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ChrClasses:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51666))
                                    ChrClassesHandler343(db2File, (uint)entry, count);
                                else
                                    ChrClassesHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ChrClassesXPowerTypes:
                            {
                                ChrClassesXPowerTypesHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ChrCustomizationChoice:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_2_50129))
                                    ChrCustomizationChoiceHandler342(db2File, (uint)entry, count);
                                else if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    ChrCustomizationChoiceHandler341(db2File, (uint)entry, count);
                                else
                                    ChrCustomizationChoiceHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ChrCustomizationDisplayInfo:
                            {
                                ChrCustomizationDisplayInfoHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ChrCustomizationElement:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51666))
                                    ChrCustomizationElementHandler343(db2File, (uint)entry, count);
                                else if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_2_50129))
                                    ChrCustomizationElementHandler342(db2File, (uint)entry, count);
                                else if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    ChrCustomizationElementHandler341(db2File, (uint)entry, count);
                                else
                                    ChrCustomizationElementHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ChrCustomizationOption:
                            {
                                ChrCustomizationOptionHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ChrCustomizationReq:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51666))
                                    ChrCustomizationReqHandler343(db2File, (uint)entry, count);
                                else if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    ChrCustomizationReqHandler341(db2File, (uint)entry, count);
                                else
                                    ChrCustomizationReqHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ChrCustomizationReqChoice:
                            {
                                ChrCustomizationReqChoiceHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ChrModel:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    ChrModelHandler341(db2File, (uint)entry, count);
                                else
                                    ChrModelHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ChrRaceXChrModel:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    ChrRaceXChrModelHandler341(db2File, (uint)entry, count);
                                else
                                    ChrRaceXChrModelHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ChrRaces:
                            {
                                ChrRacesHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ChrSpecialization:
                            {
                                ChrSpecializationHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.CinematicCamera:
                            {
                                CinematicCameraHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.CinematicSequences:
                            {
                                CinematicSequencesHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ConditionalChrModel:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51666))
                                    ConditionalChrModelHandler343(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ConditionalContentTuning:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51666))
                                    ConditionalContentTuningHandler343(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ContentTuning:
                            {
                                ContentTuningHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ConversationLine:
                            {
                                ConversationLineHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.CreatureDisplayInfo:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    CreatureDisplayInfoHandler341(db2File, (uint)entry, count);
                                else
                                    CreatureDisplayInfoHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.CreatureDisplayInfoExtra:
                            {
                                CreatureDisplayInfoExtraHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.CreatureFamily:
                            {
                                CreatureFamilyHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.CreatureModelData:
                            {
                                CreatureModelDataHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.CreatureType:
                            {
                                CreatureTypeHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.Criteria:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51666))
                                    CriteriaHandler343(db2File, (uint)entry, count);
                                else
                                    CriteriaHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.CriteriaTree:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51666))
                                    CriteriaTreeHandler343(db2File, (uint)entry, count);
                                else
                                    CriteriaTreeHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.CurrencyContainer:
                            {
                                CurrencyContainerHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.CurrencyTypes:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51666))
                                    CurrencyTypesHandler343(db2File, (uint)entry, count);
                                else
                                    CurrencyTypesHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.Curve:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    CurveHandler341(db2File, (uint)entry, count);
                                else
                                    CurveHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.CurvePoint:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_2_50129))
                                    CurvePointHandler342(db2File, (uint)entry, count);
                                else if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    CurvePointHandler341(db2File, (uint)entry, count);
                                else
                                    CurvePointHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.DestructibleModelData:
                            {
                                DestructibleModelDataHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.Difficulty:
                            {
                                DifficultyHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.DungeonEncounter:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51666))
                                    DungeonEncounterHandler343(db2File, (uint)entry, count);
                                else if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    DungeonEncounterHandler341(db2File, (uint)entry, count);
                                else
                                    DungeonEncounterHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.DurabilityCosts:
                            {
                                DurabilityCostsHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.DurabilityQuality:
                            {
                                DurabilityQualityHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.Emotes:
                            {
                                EmotesHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.EmotesText:
                            {
                                EmotesTextHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.EmotesTextSound:
                            {
                                EmotesTextSoundHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ExpectedStat:
                            {
                                ExpectedStatHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ExpectedStatMod:
                            {
                                ExpectedStatModHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.Faction:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    FactionHandler341(db2File, (uint)entry, count);
                                else
                                    FactionHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.FactionTemplate:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    FactionTemplateHandler341(db2File, (uint)entry, count);
                                else
                                    FactionTemplateHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.FriendshipRepReaction:
                            {
                                FriendshipRepReactionHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.FriendshipReputation:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    FriendshipReputationHandler341(db2File, (uint)entry, count);
                                else
                                    FriendshipReputationHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GameobjectArtKit:
                            {
                                GameobjectArtKitHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GameobjectDisplayInfo:
                            {
                                GameobjectDisplayInfoHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.Gameobjects:
                            {
                                GameobjectsHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GarrAbility:
                            {
                                GarrAbilityHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GarrBuilding:
                            {
                                GarrBuildingHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GarrBuildingPlotInst:
                            {
                                GarrBuildingPlotInstHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GarrClassSpec:
                            {
                                GarrClassSpecHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GarrFollower:
                            {
                                GarrFollowerHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GarrFollowerXAbility:
                            {
                                GarrFollowerXAbilityHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GarrMission:
                            {
                                GarrMissionHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GarrPlot:
                            {
                                GarrPlotHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GarrPlotBuilding:
                            {
                                GarrPlotBuildingHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GarrPlotInstance:
                            {
                                GarrPlotInstanceHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GarrSiteLevel:
                            {
                                GarrSiteLevelHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GarrSiteLevelPlotInst:
                            {
                                GarrSiteLevelPlotInstHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GarrTalentTree:
                            {
                                GarrTalentTreeHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GemProperties:
                            {
                                GemPropertiesHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GlyphBindableSpell:
                            {
                                GlyphBindableSpellHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GlyphSlot:
                            {
                                GlyphSlotHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GlyphProperties:
                            {
                                GlyphPropertiesHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GlyphRequiredSpec:
                            {
                                GlyphRequiredSpecHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GossipNpcOption:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    GossipNPCOptionHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GuildColorBackground:
                            {
                                GuildColorBackgroundHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GuildColorBorder:
                            {
                                GuildColorBorderHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GuildColorEmblem:
                            {
                                GuildColorEmblemHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.GuildPerkSpells:
                            {
                                GuildPerkSpellsHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.Heirloom:
                            {
                                HeirloomHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.Holidays:
                            {
                                HolidaysHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ImportPriceArmor:
                            {
                                ImportPriceArmorHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ImportPriceQuality:
                            {
                                ImportPriceQualityHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ImportPriceShield:
                            {
                                ImportPriceShieldHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ImportPriceWeapon:
                            {
                                ImportPriceWeaponHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.Item:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    ItemHandler341(db2File, (uint)entry, count);
                                else
                                    ItemHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemAppearance:
                            {
                                ItemAppearanceHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemArmorQuality:
                            {
                                ItemArmorQualityHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemArmorShield:
                            {
                                ItemArmorShieldHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemArmorTotal:
                            {
                                ItemArmorTotalHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemBagFamily:
                            {
                                ItemBagFamilyHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemBonus:
                            {
                                ItemBonusHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemBonusListLevelDelta:
                            {
                                ItemBonusListLevelDeltaHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemBonusTreeNode:
                            {
                                ItemBonusTreeNodeHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemChildEquipment:
                            {
                                ItemChildEquipmentHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemClass:
                            {
                                ItemClassHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemContextPickerEntry:
                            {
                                ItemContextPickerEntryHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemCurrencyCost:
                            {
                                ItemCurrencyCostHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemDamageAmmo:
                            {
                                ItemDamageAmmoHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemDamageOneHand:
                            {
                                ItemDamageOneHandHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemDamageOneHandCaster:
                            {
                                ItemDamageOneHandCasterHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemDamageTwoHand:
                            {
                                ItemDamageTwoHandHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemDamageTwoHandCaster:
                            {
                                ItemDamageTwoHandCasterHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemDisenchantLoot:
                            {
                                ItemDisenchantLootHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemEffect:
                            {
                                ItemEffectHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemExtendedCost:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    ItemExtendedCostHandler341(db2File, (uint)entry, count);
                                else
                                    ItemExtendedCostHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemLevelSelector:
                            {
                                ItemLevelSelectorHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemLevelSelectorQuality:
                            {
                                ItemLevelSelectorQualityHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemLevelSelectorQualitySet:
                            {
                                ItemLevelSelectorQualitySetHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemLimitCategory:
                            {
                                ItemLimitCategoryHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemLimitCategoryCondition:
                            {
                                ItemLimitCategoryConditionHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemModifiedAppearance:
                            {
                                ItemModifiedAppearanceHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemModifiedAppearanceExtra:
                            {
                                ItemModifiedAppearanceExtraHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemNameDescription:
                            {
                                ItemNameDescriptionHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemPriceBase:
                            {
                                ItemPriceBaseHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemSearchName:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_2_50129))
                                    ItemSearchNameHandler342(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemSet:
                            {
                                ItemSetHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemSetSpell:
                            {
                                ItemSetSpellHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemSparse:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    ItemSparseHandler341(db2File, (uint)entry, count);
                                else
                                    ItemSparseHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemSpec:
                            {
                                ItemSpecHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemSpecOverride:
                            {
                                ItemSpecOverrideHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ItemXBonusTree:
                            {
                                ItemXBonusTreeHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.JournalEncounter:
                            {
                                JournalEncounterHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.JournalEncounterSection:
                            {
                                JournalEncounterSectionHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.JournalInstance:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    JournalInstanceHandler341(db2File, (uint)entry, count);
                                else
                                    JournalInstanceHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.JournalTier:
                            {
                                JournalTierHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.Keychain:
                            {
                                KeychainHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.KeystoneAffix:
                            {
                                KeystoneAffixHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.LanguageWords:
                            {
                                LanguageWordsHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.Languages:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_2_50129))
                                    LanguagesHandler342(db2File, (uint)entry, count);
                                else
                                    LanguagesHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.LfgDungeons:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    LfgDungeonsHandler341(db2File, (uint)entry, count);
                                else
                                    LfgDungeonsHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.Light:
                            {
                                LightHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.LiquidType:
                            {
                                LiquidTypeHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.Lock:
                            {
                                LockHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.MailTemplate:
                            {
                                MailTemplateHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.Map:
                            {
                                MapHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.MapChallengeMode:
                            {
                                MapChallengeModeHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.MapDifficulty:
                            {
                                MapDifficultyHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.MapDifficultyXCondition:
                            {
                                MapDifficultyXConditionHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ModifierTree:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51666))
                                    ModifierTreeHandler343(db2File, (uint)entry, count);
                                else
                                    ModifierTreeHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.Mount:
                            {
                                MountHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.MountCapability:
                            {
                                MountCapabilityHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.MountTypeXCapability:
                            {
                                MountTypeXCapabilityHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.MountXDisplay:
                            {
                                MountXDisplayHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.Movie:
                            {
                                MovieHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.MythicPlusSeason:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_2_50129))
                                    MythicPlusSeasonHandler342(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.NameGen:
                            {
                                NameGenHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.NamesProfanity:
                            {
                                NamesProfanityHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.NamesReserved:
                            {
                                NamesReservedHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.NamesReservedLocale:
                            {
                                NamesReservedLocaleHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.NumTalentsAtLevel:
                            {
                                NumTalentsAtLevelHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.OverrideSpellData:
                            {
                                OverrideSpellDataHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ParagonReputation:
                            {
                                ParagonReputationHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.Phase:
                            {
                                PhaseHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.PhaseXPhaseGroup:
                            {
                                PhaseXPhaseGroupHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.PlayerCondition:
                            {
                                PlayerConditionHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.PowerDisplay:
                            {
                                PowerDisplayHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.PowerType:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    PowerTypeHandler341(db2File, (uint)entry, count);
                                else
                                    PowerTypeHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.PrestigeLevelInfo:
                            {
                                PrestigeLevelInfoHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.PvpDifficulty:
                            {
                                PvpDifficultyHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.PvpItem:
                            {
                                PvpItemHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.PvpSeason:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_2_50129))
                                    PvpSeasonHandler342(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.PvpTalent:
                            {
                                PvpTalentHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.PvpTalentCategory:
                            {
                                PvpTalentCategoryHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.PvpTalentSlotUnlock:
                            {
                                PvpTalentSlotUnlockHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.PvpTier:
                            {
                                PvpTierHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.QuestFactionReward:
                            {
                                QuestFactionRewardHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.QuestInfo:
                            {
                                QuestInfoHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.QuestLineXQuest:
                            {
                                QuestLineXQuestHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.QuestMoneyReward:
                            {
                                QuestMoneyRewardHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.QuestPackageItem:
                            {
                                QuestPackageItemHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.QuestSort:
                            {
                                QuestSortHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.QuestV2:
                            {
                                QuestV2Handler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.QuestXp:
                            {
                                QuestXpHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.RandPropPoints:
                            {
                                RandPropPointsHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.RewardPack:
                            {
                                RewardPackHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.RewardPackXCurrencyType:
                            {
                                RewardPackXCurrencyTypeHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.RewardPackXItem:
                            {
                                RewardPackXItemHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.Scenario:
                            {
                                ScenarioHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ScenarioStep:
                            {
                                ScenarioStepHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ScalingStatDistribution:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    ScalingStatDistributionHandler341(db2File, (uint)entry, count);
                                else
                                    ScalingStatDistributionHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ScalingStatValues:
                            {
                                ScalingStatValuesHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SceneScript:
                            {
                                SceneScriptHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SceneScriptGlobalText:
                            {
                                SceneScriptGlobalTextHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SceneScriptPackage:
                            {
                                SceneScriptPackageHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SceneScriptText:
                            {
                                SceneScriptTextHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.ServerMessages:
                            {
                                ServerMessagesHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SkillLine:
                            {
                                SkillLineHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SkillLineAbility:
                            {
                                SkillLineAbilityHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SkillLineXTraitTree:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    SkillLineXTraitTreeHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SkillRaceClassInfo:
                            {
                                SkillRaceClassInfoHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SoundKit:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    SoundKitHandler341(db2File, (uint)entry, count);
                                else
                                    SoundKitHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpecializationSpells:
                            {
                                SpecializationSpellsHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpecSetMember:
                            {
                                SpecSetMemberHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellAuraOptions:
                            {
                                SpellAuraOptionsHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellAuraRestrictions:
                            {
                                SpellAuraRestrictionsHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellCastTimes:
                            {
                                SpellCastTimesHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellCastingRequirements:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    SpellCastingRequirementsHandler341(db2File, (uint)entry, count);
                                else
                                    SpellCastingRequirementsHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellCategories:
                            {
                                SpellCategoriesHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellCategory:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51666))
                                    SpellCategoryHandler343(db2File, (uint)entry, count);
                                else
                                    SpellCategoryHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellClassOptions:
                            {
                                SpellClassOptionsHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellCooldowns:
                            {
                                SpellCooldownsHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellDuration:
                            {
                                SpellDurationHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellEffect:
                            {
                                SpellEffectHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellEquippedItems:
                            {
                                SpellEquippedItemsHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellFocusObject:
                            {
                                SpellFocusObjectHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellInterrupts:
                            {
                                SpellInterruptsHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellItemEnchantment:
                            {
                                SpellItemEnchantmentHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellItemEnchantmentCondition:
                            {
                                SpellItemEnchantmentConditionHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellKeyboundOverride:
                            {
                                SpellKeyboundOverrideHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellLabel:
                            {
                                SpellLabelHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellLearnSpell:
                            {
                                SpellLearnSpellHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellLevels:
                            {
                                SpellLevelsHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellMisc:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    SpellMiscHandler341(db2File, (uint)entry, count);
                                else
                                    SpellMiscHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellName:
                            {
                                SpellNameHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellPower:
                            {
                                SpellPowerHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellPowerDifficulty:
                            {
                                SpellPowerDifficultyHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellProcsPerMinute:
                            {
                                SpellProcsPerMinuteHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellProcsPerMinuteMod:
                            {
                                SpellProcsPerMinuteModHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellRadius:
                            {
                                SpellRadiusHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellRange:
                            {
                                SpellRangeHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellReagents:
                            {
                                SpellReagentsHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellReagentsCurrency:
                            {
                                SpellReagentsCurrencyHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellScaling:
                            {
                                SpellScalingHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellShapeshift:
                            {
                                SpellShapeshiftHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellShapeshiftForm:
                            {
                                SpellShapeshiftFormHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellTargetRestrictions:
                            {
                                SpellTargetRestrictionsHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellTotems:
                            {
                                SpellTotemsHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellVisual:
                            {
                                SpellVisualHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellVisualEffectName:
                            {
                                SpellVisualEffectNameHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellVisualMissile:
                            {
                                SpellVisualMissileHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellVisualKit:
                            {
                                SpellVisualKitHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SpellXSpellVisual:
                            {
                                SpellXSpellVisualHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.SummonProperties:
                            {
                                SummonPropertiesHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TactKey:
                            {
                                TactKeyHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.Talent:
                            {
                                TalentHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TalentTab:
                            {
                                TalentTabHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TaxiNodes:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51666))
                                    TaxiNodesHandler343(db2File, (uint)entry, count);
                                else
                                    TaxiNodesHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TaxiPath:
                            {
                                TaxiPathHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TaxiPathNode:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51666))
                                    TaxiPathNodeHandler343(db2File, (uint)entry, count);
                                else
                                    TaxiPathNodeHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TotemCategory:
                            {
                                TotemCategoryHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.Toy:
                            {
                                ToyHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TransmogHoliday:
                            {
                                TransmogHolidayHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TraitCond:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    TraitCondHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TraitCost:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    TraitCostHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TraitCurrency:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    TraitCurrencyHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TraitCurrencySource:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    TraitCurrencySourceHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TraitDefinition:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    TraitDefinitionHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TraitDefinitionEffectPoints:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    TraitDefinitionEffectPointsHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TraitEdge:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    TraitEdgeHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TraitNode:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    TraitNodeHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TraitNodeEntry:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    TraitNodeEntryHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TraitNodeEntryXTraitCond:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    TraitNodeEntryXTraitCondHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TraitNodeEntryXTraitCost:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    TraitNodeEntryXTraitCostHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TraitNodeGroup:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    TraitNodeGroupHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TraitNodeGroupXTraitCond:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    TraitNodeGroupXTraitCondHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TraitNodeGroupXTraitCost:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    TraitNodeGroupXTraitCostHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TraitNodeGroupXTraitNode:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    TraitNodeGroupXTraitNodeHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TraitNodeXTraitCond:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    TraitNodeXTraitCondHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TraitNodeXTraitCost:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    TraitNodeXTraitCostHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TraitNodeXTraitNodeEntry:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    TraitNodeXTraitNodeEntryHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TraitTree:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    TraitTreeHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TraitTreeLoadout:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    TraitTreeLoadoutHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TraitTreeLoadoutEntry:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    TraitTreeLoadoutEntryHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TraitTreeXTraitCost:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    TraitTreeXTraitCostHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TraitTreeXTraitCurrency:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                                    TraitTreeXTraitCurrencyHandler341(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TransmogSet:
                            {
                                TransmogSetHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TransmogSetGroup:
                            {
                                TransmogSetGroupHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TransmogSetItem:
                            {
                                TransmogSetItemHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TransportAnimation:
                            {
                                TransportAnimationHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.TransportRotation:
                            {
                                TransportRotationHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.UiMap:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51666))
                                    UiMapHandler343(db2File, (uint)entry, count);
                                else
                                    UiMapHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.UiMapAssignment:
                            {
                                UiMapAssignmentHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.UiMapLink:
                            {
                                UiMapLinkHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.UiMapXMapArt:
                            {
                                UiMapXMapArtHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.UnitCondition:
                            {
                                UnitConditionHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.UnitPowerBar:
                            {
                                UnitPowerBarHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.Vehicle:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_2_50129))
                                    VehicleHandler342(db2File, (uint)entry, count);
                                else
                                    VehicleHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.VehicleSeat:
                            {
                                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_2_50129))
                                    VehicleSeatHandler342(db2File, (uint)entry, count);
                                else
                                    VehicleSeatHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.WmoAreaTable:
                            {
                                WmoAreaTableHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.WorldEffect:
                            {
                                WorldEffectHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.WorldMapOverlay:
                            {
                                WorldMapOverlayHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            case DB2Hash.WorldStateExpression:
                            {
                                WorldStateExpressionHandler340(db2File, (uint)entry, count);
                                break;
                            }
                            default:
                            {
                                db2File.WriteLine($"(Entry: {entry} TableHash: {type}) has missing structure. HotfixBlob entry generated!");
                                db2File.AsHex();
                                db2File.ReadToEnd();

                                HotfixBlob hotfixBlob = new HotfixBlob
                                {
                                    TableHash = type,
                                    RecordID = entry,
                                    Blob = new Blob(data)
                                };

                                Storage.HotfixBlobs.Add(hotfixBlob);
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
                        packet.WriteLine($"Row {entry} has been removed.");
                        HotfixStoreMgr.RemoveRecord(type, entry);
                        break;
                    }
                    case HotfixStatus.Invalid:
                    {
                        // sniffs from others may have the data
                        packet.WriteLine($"Row {entry} is invalid.");
                        break;
                    }
                    default:
                    {
                        packet.WriteLine($"Unhandled status: {status}");
                        break;
                    }
                }

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
        public static void HandleHotixData815(Packet packet)
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
    }
}

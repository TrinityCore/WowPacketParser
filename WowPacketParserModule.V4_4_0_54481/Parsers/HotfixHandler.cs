using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class HotfixHandler
    {
        public static void AchievementHandler440(Packet packet, uint entry, params object[] indexes)
        {
            AchievementHotfix440 hotfix = new AchievementHotfix440();

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

            Storage.AchievementHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AchievementLocaleHotfix440 hotfixLocale = new AchievementLocaleHotfix440
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                    TitleLang = hotfix.Title,
                    RewardLang = hotfix.Reward,
                };
                Storage.AchievementHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void AchievementCategoryHandler440(Packet packet, uint entry, params object[] indexes)
        {
            AchievementCategoryHotfix440 hotfix = new AchievementCategoryHotfix440();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Parent = packet.ReadInt16("Parent", indexes);
            hotfix.UiOrder = packet.ReadSByte("UiOrder", indexes);

            Storage.AchievementCategoryHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AchievementCategoryLocaleHotfix440 hotfixLocale = new AchievementCategoryLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.AchievementCategoryHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void AnimationDataHandler440(Packet packet, uint entry, params object[] indexes)
        {
            AnimationDataHotfix440 hotfix = new AnimationDataHotfix440();

            hotfix.ID = entry;
            hotfix.Fallback = packet.ReadUInt16("Fallback", indexes);
            hotfix.BehaviorTier = packet.ReadByte("BehaviorTier", indexes);
            hotfix.BehaviorID = packet.ReadInt32("BehaviorID", indexes);
            hotfix.Flags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.AnimationDataHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void AnimationDataHandler441(Packet packet, uint entry, params object[] indexes)
        {
            AnimationDataHotfix441 hotfix = new AnimationDataHotfix441();

            hotfix.ID = entry;
            hotfix.Fallback = packet.ReadUInt16("Fallback", indexes);
            hotfix.BehaviorTier = packet.ReadByte("BehaviorTier", indexes);
            hotfix.BehaviorID = packet.ReadInt16("BehaviorID", indexes);
            hotfix.Flags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.AnimationDataHotfixes441.Add(hotfix, packet.TimeSpan);
        }

        public static void AnimKitHandler440(Packet packet, uint entry, params object[] indexes)
        {
            AnimKitHotfix440 hotfix = new AnimKitHotfix440();

            hotfix.ID = entry;
            hotfix.OneShotDuration = packet.ReadUInt32("OneShotDuration", indexes);
            hotfix.OneShotStopAnimKitID = packet.ReadUInt16("OneShotStopAnimKitID", indexes);
            hotfix.LowDefAnimKitID = packet.ReadUInt16("LowDefAnimKitID", indexes);

            Storage.AnimKitHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void AreaGroupMemberHandler440(Packet packet, uint entry, params object[] indexes)
        {
            AreaGroupMemberHotfix440 hotfix = new AreaGroupMemberHotfix440();

            hotfix.ID = entry;
            hotfix.AreaID = packet.ReadUInt16("AreaID", indexes);
            hotfix.AreaGroupID = packet.ReadInt32("AreaGroupID", indexes);

            Storage.AreaGroupMemberHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void AreaTableHandler440(Packet packet, uint entry, params object[] indexes)
        {
            AreaTableHotfix440 hotfix = new AreaTableHotfix440();

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

            Storage.AreaTableHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AreaTableLocaleHotfix440 hotfixLocale = new AreaTableLocaleHotfix440
                {
                    ID = hotfix.ID,
                    AreaNameLang = hotfix.AreaName,
                };
                Storage.AreaTableHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void AreaTriggerHandler440(Packet packet, uint entry, params object[] indexes)
        {
            AreaTriggerHotfix440 hotfix = new AreaTriggerHotfix440();

            hotfix.Message = packet.ReadCString("Message", indexes);
            hotfix.PosX = packet.ReadSingle("PosX", indexes);
            hotfix.PosY = packet.ReadSingle("PosY", indexes);
            hotfix.PosZ = packet.ReadSingle("PosZ", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ContinentID = packet.ReadInt16("ContinentID", indexes);
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
            hotfix.AreaTriggerActionSetID = packet.ReadInt16("AreaTriggerActionSetID", indexes);
            hotfix.Flags = packet.ReadSByte("Flags", indexes);

            Storage.AreaTriggerHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AreaTriggerLocaleHotfix440 hotfixLocale = new AreaTriggerLocaleHotfix440
                {
                    ID = hotfix.ID,
                    MessageLang = hotfix.Message,
                };
                Storage.AreaTriggerHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void AreaTriggerHandler441(Packet packet, uint entry, params object[] indexes)
        {
            AreaTriggerHotfix441 hotfix = new AreaTriggerHotfix441();

            hotfix.Message = packet.ReadCString("Message", indexes);
            hotfix.PosX = packet.ReadSingle("PosX", indexes);
            hotfix.PosY = packet.ReadSingle("PosY", indexes);
            hotfix.PosZ = packet.ReadSingle("PosZ", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ContinentID = packet.ReadInt16("ContinentID", indexes);
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
            hotfix.AreaTriggerActionSetID = packet.ReadInt16("AreaTriggerActionSetID", indexes);
            hotfix.Flags = packet.ReadSByte("Flags", indexes);

            Storage.AreaTriggerHotfixes441.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AreaTriggerLocaleHotfix441 hotfixLocale = new AreaTriggerLocaleHotfix441
                {
                    ID = hotfix.ID,
                    MessageLang = hotfix.Message,
                };
                Storage.AreaTriggerHotfixesLocale441.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void AreaTriggerActionSetHandler440(Packet packet, uint entry, params object[] indexes)
        {
            AreaTriggerActionSetHotfix440 hotfix = new AreaTriggerActionSetHotfix440();

            hotfix.ID = entry;
            hotfix.Flags = packet.ReadInt16("Flags", indexes);

            Storage.AreaTriggerActionSetHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ArmorLocationHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ArmorLocationHotfix440 hotfix = new ArmorLocationHotfix440();

            hotfix.ID = entry;
            hotfix.Clothmodifier = packet.ReadSingle("Clothmodifier", indexes);
            hotfix.Leathermodifier = packet.ReadSingle("Leathermodifier", indexes);
            hotfix.Chainmodifier = packet.ReadSingle("Chainmodifier", indexes);
            hotfix.Platemodifier = packet.ReadSingle("Platemodifier", indexes);
            hotfix.Modifier = packet.ReadSingle("Modifier", indexes);

            Storage.ArmorLocationHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void AuctionHouseHandler440(Packet packet, uint entry, params object[] indexes)
        {
            AuctionHouseHotfix440 hotfix = new AuctionHouseHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.FactionID = packet.ReadUInt16("FactionID", indexes);
            hotfix.DepositRate = packet.ReadByte("DepositRate", indexes);
            hotfix.ConsignmentRate = packet.ReadByte("ConsignmentRate", indexes);

            Storage.AuctionHouseHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                AuctionHouseLocaleHotfix440 hotfixLocale = new AuctionHouseLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.AuctionHouseHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void BankBagSlotPricesHandler440(Packet packet, uint entry, params object[] indexes)
        {
            BankBagSlotPricesHotfix440 hotfix = new BankBagSlotPricesHotfix440();

            hotfix.ID = entry;
            hotfix.Cost = packet.ReadUInt32("Cost", indexes);

            Storage.BankBagSlotPricesHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void BannedAddonsHandler440(Packet packet, uint entry, params object[] indexes)
        {
            BannedAddonsHotfix440 hotfix = new BannedAddonsHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Version = packet.ReadCString("Version", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);

            Storage.BannedAddonsHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void BarberShopStyleHandler440(Packet packet, uint entry, params object[] indexes)
        {
            BarberShopStyleHotfix440 hotfix = new BarberShopStyleHotfix440();

            hotfix.DisplayName = packet.ReadCString("DisplayName", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Type = packet.ReadByte("Type", indexes);
            hotfix.CostModifier = packet.ReadSingle("CostModifier", indexes);
            hotfix.Race = packet.ReadByte("Race", indexes);
            hotfix.Sex = packet.ReadByte("Sex", indexes);
            hotfix.Data = packet.ReadByte("Data", indexes);

            Storage.BarberShopStyleHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                BarberShopStyleLocaleHotfix440 hotfixLocale = new BarberShopStyleLocaleHotfix440
                {
                    ID = hotfix.ID,
                    DisplayNameLang = hotfix.DisplayName,
                    DescriptionLang = hotfix.Description,
                };
                Storage.BarberShopStyleHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void BattlePetAbilityHandler440(Packet packet, uint entry, params object[] indexes)
        {
            BattlePetAbilityHotfix440 hotfix = new BattlePetAbilityHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.IconFileDataID = packet.ReadInt32("IconFileDataID", indexes);
            hotfix.PetTypeEnum = packet.ReadSByte("PetTypeEnum", indexes);
            hotfix.Cooldown = packet.ReadUInt32("Cooldown", indexes);
            hotfix.BattlePetVisualID = packet.ReadUInt16("BattlePetVisualID", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);

            Storage.BattlePetAbilityHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                BattlePetAbilityLocaleHotfix440 hotfixLocale = new BattlePetAbilityLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.BattlePetAbilityHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void BattlePetBreedQualityHandler440(Packet packet, uint entry, params object[] indexes)
        {
            BattlePetBreedQualityHotfix440 hotfix = new BattlePetBreedQualityHotfix440();

            hotfix.ID = entry;
            hotfix.StateMultiplier = packet.ReadSingle("StateMultiplier", indexes);
            hotfix.QualityEnum = packet.ReadByte("QualityEnum", indexes);

            Storage.BattlePetBreedQualityHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void BattlePetBreedStateHandler440(Packet packet, uint entry, params object[] indexes)
        {
            BattlePetBreedStateHotfix440 hotfix = new BattlePetBreedStateHotfix440();

            hotfix.ID = entry;
            hotfix.BattlePetStateID = packet.ReadByte("BattlePetStateID", indexes);
            hotfix.Value = packet.ReadUInt16("Value", indexes);
            hotfix.BattlePetBreedID = packet.ReadInt32("BattlePetBreedID", indexes);

            Storage.BattlePetBreedStateHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void BattlePetSpeciesHandler440(Packet packet, uint entry, params object[] indexes)
        {
            BattlePetSpeciesHotfix440 hotfix = new BattlePetSpeciesHotfix440();

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

            Storage.BattlePetSpeciesHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                BattlePetSpeciesLocaleHotfix440 hotfixLocale = new BattlePetSpeciesLocaleHotfix440
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                    SourceTextLang = hotfix.SourceText,
                };
                Storage.BattlePetSpeciesHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void BattlePetSpeciesHandler441(Packet packet, uint entry, params object[] indexes)
        {
            BattlePetSpeciesHotfix441 hotfix = new BattlePetSpeciesHotfix441();

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

            Storage.BattlePetSpeciesHotfixes441.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                BattlePetSpeciesLocaleHotfix441 hotfixLocale = new BattlePetSpeciesLocaleHotfix441
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                    SourceTextLang = hotfix.SourceText,
                };
                Storage.BattlePetSpeciesHotfixesLocale441.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void BattlePetSpeciesStateHandler440(Packet packet, uint entry, params object[] indexes)
        {
            BattlePetSpeciesStateHotfix440 hotfix = new BattlePetSpeciesStateHotfix440();

            hotfix.ID = entry;
            hotfix.BattlePetStateID = packet.ReadByte("BattlePetStateID", indexes);
            hotfix.Value = packet.ReadInt32("Value", indexes);
            hotfix.BattlePetSpeciesID = packet.ReadInt32("BattlePetSpeciesID", indexes);

            Storage.BattlePetSpeciesStateHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void BattlemasterListHandler440(Packet packet, uint entry, params object[] indexes)
        {
            BattlemasterListHotfix440 hotfix = new BattlemasterListHotfix440();

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
            hotfix.Unknown1153_0 = packet.ReadInt32("Unknown1153_0", indexes);
            hotfix.Unknown1153_1 = packet.ReadInt32("Unknown1153_1", indexes);
            hotfix.MapID = new short?[16];
            for (int i = 0; i < 16; i++)
                hotfix.MapID[i] = packet.ReadInt16("MapID", indexes, i);

            Storage.BattlemasterListHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                BattlemasterListLocaleHotfix440 hotfixLocale = new BattlemasterListLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    GameTypeLang = hotfix.GameType,
                    ShortDescriptionLang = hotfix.ShortDescription,
                    LongDescriptionLang = hotfix.LongDescription,
                };
                Storage.BattlemasterListHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void BattlemasterListHandler441(Packet packet, uint entry, params object[] indexes)
        {
            BattlemasterListHotfix441 hotfix = new BattlemasterListHotfix441();

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
            hotfix.Unknown1153_0 = packet.ReadInt32("Unknown1153_0", indexes);
            hotfix.Unknown1153_1 = packet.ReadInt32("Unknown1153_1", indexes);

            Storage.BattlemasterListHotfixes441.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                BattlemasterListLocaleHotfix441 hotfixLocale = new BattlemasterListLocaleHotfix441
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    GameTypeLang = hotfix.GameType,
                    ShortDescriptionLang = hotfix.ShortDescription,
                    LongDescriptionLang = hotfix.LongDescription,
                };
                Storage.BattlemasterListHotfixesLocale441.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void BattlemasterListXMapHandler441(Packet packet, uint entry, params object[] indexes)
        {
            BattlemasterListXMapHotfix441 hotfix = new BattlemasterListXMapHotfix441();

            hotfix.ID = entry;
            hotfix.MapID = packet.ReadInt32("MapID", indexes);
            hotfix.BattlemasterListID = packet.ReadInt32("BattlemasterListID", indexes);

            Storage.BattlemasterListXMapHotfixes441.Add(hotfix, packet.TimeSpan);
        }

        public static void BroadcastTextHandler440(Packet packet, uint entry, params object[] indexes)
        {
            BroadcastTextHotfix440 hotfix = new BroadcastTextHotfix440();

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

            Storage.BroadcastTextHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                BroadcastTextLocaleHotfix440 hotfixLocale = new BroadcastTextLocaleHotfix440
                {
                    ID = hotfix.ID,
                    TextLang = hotfix.Text,
                    Text1Lang = hotfix.Text1,
                };
                Storage.BroadcastTextHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void BroadcastTextHandler441(Packet packet, uint entry, params object[] indexes)
        {
            BroadcastTextHotfix441 hotfix = new BroadcastTextHotfix441();

            hotfix.Text = packet.ReadCString("Text", indexes);
            hotfix.Text1 = packet.ReadCString("Text1", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.LanguageID = packet.ReadInt32("LanguageID", indexes);
            hotfix.ConditionID = packet.ReadInt32("ConditionID", indexes);
            hotfix.EmotesID = packet.ReadUInt16("EmotesID", indexes);
            hotfix.Flags = packet.ReadUInt16("Flags", indexes);
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

            Storage.BroadcastTextHotfixes441.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                BroadcastTextLocaleHotfix441 hotfixLocale = new BroadcastTextLocaleHotfix441
                {
                    ID = hotfix.ID,
                    TextLang = hotfix.Text,
                    Text1Lang = hotfix.Text1,
                };
                Storage.BroadcastTextHotfixesLocale441.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CfgCategoriesHandler440(Packet packet, uint entry, params object[] indexes)
        {
            CfgCategoriesHotfix440 hotfix = new CfgCategoriesHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.LocaleMask = packet.ReadUInt16("LocaleMask", indexes);
            hotfix.CreateCharsetMask = packet.ReadByte("CreateCharsetMask", indexes);
            hotfix.ExistingCharsetMask = packet.ReadByte("ExistingCharsetMask", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.Order = packet.ReadSByte("Order", indexes);

            Storage.CfgCategoriesHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CfgCategoriesLocaleHotfix440 hotfixLocale = new CfgCategoriesLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.CfgCategoriesHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CfgRegionsHandler440(Packet packet, uint entry, params object[] indexes)
        {
            CfgRegionsHotfix440 hotfix = new CfgRegionsHotfix440();

            hotfix.ID = entry;
            hotfix.Tag = packet.ReadCString("Tag", indexes);
            hotfix.RegionID = packet.ReadUInt16("RegionID", indexes);
            hotfix.Raidorigin = packet.ReadUInt32("Raidorigin", indexes);
            hotfix.RegionGroupMask = packet.ReadByte("RegionGroupMask", indexes);
            hotfix.ChallengeOrigin = packet.ReadUInt32("ChallengeOrigin", indexes);

            Storage.CfgRegionsHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void CharTitlesHandler440(Packet packet, uint entry, params object[] indexes)
        {
            CharTitlesHotfix440 hotfix = new CharTitlesHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Name1 = packet.ReadCString("Name1", indexes);
            hotfix.MaskID = packet.ReadInt16("MaskID", indexes);
            hotfix.Flags = packet.ReadSByte("Flags", indexes);

            Storage.CharTitlesHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CharTitlesLocaleHotfix440 hotfixLocale = new CharTitlesLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    Name1Lang = hotfix.Name1,
                };
                Storage.CharTitlesHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CharacterLoadoutHandler440(Packet packet, uint entry, params object[] indexes)
        {
            CharacterLoadoutHotfix440 hotfix = new CharacterLoadoutHotfix440();

            hotfix.Racemask = packet.ReadInt64("Racemask", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ChrClassID = packet.ReadSByte("ChrClassID", indexes);
            hotfix.Purpose = packet.ReadInt32("Purpose", indexes);
            hotfix.ItemContext = packet.ReadSByte("ItemContext", indexes);

            Storage.CharacterLoadoutHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void CharacterLoadoutHandler441(Packet packet, uint entry, params object[] indexes)
        {
            CharacterLoadoutHotfix441 hotfix = new CharacterLoadoutHotfix441();

            hotfix.Racemask = packet.ReadInt64("Racemask", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ChrClassID = packet.ReadSByte("ChrClassID", indexes);
            hotfix.Purpose = packet.ReadInt32("Purpose", indexes);
            hotfix.ItemContext = packet.ReadByte("ItemContext", indexes);

            Storage.CharacterLoadoutHotfixes441.Add(hotfix, packet.TimeSpan);
        }

        public static void CharacterLoadoutItemHandler440(Packet packet, uint entry, params object[] indexes)
        {
            CharacterLoadoutItemHotfix440 hotfix = new CharacterLoadoutItemHotfix440();

            hotfix.ID = entry;
            hotfix.CharacterLoadoutID = packet.ReadUInt16("CharacterLoadoutID", indexes);
            hotfix.ItemID = packet.ReadUInt32("ItemID", indexes);

            Storage.CharacterLoadoutItemHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ChatChannelsHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ChatChannelsHotfix440 hotfix = new ChatChannelsHotfix440();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Shortcut = packet.ReadCString("Shortcut", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.FactionGroup = packet.ReadSByte("FactionGroup", indexes);
            hotfix.Ruleset = packet.ReadInt32("Ruleset", indexes);

            Storage.ChatChannelsHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChatChannelsLocaleHotfix440 hotfixLocale = new ChatChannelsLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    ShortcutLang = hotfix.Shortcut,
                };
                Storage.ChatChannelsHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ChrClassUiDisplayHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ChrClassUiDisplayHotfix440 hotfix = new ChrClassUiDisplayHotfix440();

            hotfix.ID = entry;
            hotfix.ChrClassesID = packet.ReadByte("ChrClassesID", indexes);
            hotfix.AdvGuidePlayerConditionID = packet.ReadUInt32("AdvGuidePlayerConditionID", indexes);
            hotfix.SplashPlayerConditionID = packet.ReadUInt32("SplashPlayerConditionID", indexes);

            Storage.ChrClassUiDisplayHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrClassesHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ChrClassesHotfix440 hotfix = new ChrClassesHotfix440();

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

            Storage.ChrClassesHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChrClassesLocaleHotfix440 hotfixLocale = new ChrClassesLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    NameMaleLang = hotfix.NameMale,
                    NameFemaleLang = hotfix.NameFemale,
                };
                Storage.ChrClassesHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ChrClassesHandler441(Packet packet, uint entry, params object[] indexes)
        {
            ChrClassesHotfix441 hotfix = new ChrClassesHotfix441();

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
            hotfix.DisplayPower = packet.ReadSByte("DisplayPower", indexes);
            hotfix.RangedAttackPowerPerAgility = packet.ReadByte("RangedAttackPowerPerAgility", indexes);
            hotfix.AttackPowerPerAgility = packet.ReadByte("AttackPowerPerAgility", indexes);
            hotfix.AttackPowerPerStrength = packet.ReadByte("AttackPowerPerStrength", indexes);
            hotfix.SpellClassSet = packet.ReadByte("SpellClassSet", indexes);
            hotfix.RolesMask = packet.ReadByte("RolesMask", indexes);
            hotfix.DamageBonusStat = packet.ReadByte("DamageBonusStat", indexes);
            hotfix.HasRelicSlot = packet.ReadByte("HasRelicSlot", indexes);

            Storage.ChrClassesHotfixes441.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChrClassesLocaleHotfix441 hotfixLocale = new ChrClassesLocaleHotfix441
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    NameMaleLang = hotfix.NameMale,
                    NameFemaleLang = hotfix.NameFemale,
                };
                Storage.ChrClassesHotfixesLocale441.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ChrClassesXPowerTypesHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ChrClassesXPowerTypesHotfix440 hotfix = new ChrClassesXPowerTypesHotfix440();

            hotfix.ID = entry;
            hotfix.PowerType = packet.ReadSByte("PowerType", indexes);
            hotfix.ClassID = packet.ReadInt32("ClassID", indexes);

            Storage.ChrClassesXPowerTypesHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrCustomizationChoiceHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationChoiceHotfix440 hotfix = new ChrCustomizationChoiceHotfix440();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ChrCustomizationOptionID = packet.ReadInt32("ChrCustomizationOptionID", indexes);
            hotfix.ChrCustomizationReqID = packet.ReadInt32("ChrCustomizationReqID", indexes);
            hotfix.ChrCustomizationVisReqID = packet.ReadInt32("ChrCustomizationVisReqID", indexes);
            hotfix.OrderIndex = packet.ReadUInt16("OrderIndex", indexes);
            hotfix.UiOrderIndex = packet.ReadUInt16("UiOrderIndex", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.AddedInPatch = packet.ReadInt32("AddedInPatch", indexes);
            hotfix.SoundKitID = packet.ReadInt32("SoundKitID", indexes);
            hotfix.SwatchColor = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.SwatchColor[i] = packet.ReadInt32("SwatchColor", indexes, i);

            Storage.ChrCustomizationChoiceHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChrCustomizationChoiceLocaleHotfix440 hotfixLocale = new ChrCustomizationChoiceLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ChrCustomizationChoiceHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ChrCustomizationDisplayInfoHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationDisplayInfoHotfix440 hotfix = new ChrCustomizationDisplayInfoHotfix440();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.SpellShapeshiftFormID = packet.ReadInt32("SpellShapeshiftFormID", indexes);
            hotfix.CreatureDisplayInfoID = packet.ReadInt32("CreatureDisplayInfoID", indexes);
            hotfix.BarberShopMinCameraDistance = packet.ReadSingle("BarberShopMinCameraDistance", indexes);
            hotfix.BarberShopHeightOffset = packet.ReadSingle("BarberShopHeightOffset", indexes);
            hotfix.BarberShopCameraZoomOffset = packet.ReadSingle("BarberShopCameraZoomOffset", indexes);

            Storage.ChrCustomizationDisplayInfoHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrCustomizationElementHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationElementHotfix440 hotfix = new ChrCustomizationElementHotfix440();

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
            hotfix.ChrCustGeoComponentLinkID = packet.ReadInt32("ChrCustGeoComponentLinkID", indexes);

            Storage.ChrCustomizationElementHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrCustomizationOptionHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationOptionHotfix440 hotfix = new ChrCustomizationOptionHotfix440();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.SecondaryID = packet.ReadUInt16("SecondaryID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_0_55141))
            {
                hotfix.ChrRacesID = packet.ReadInt32("ChrRacesID", indexes);
                hotfix.Sex = packet.ReadInt32("Sex", indexes);
            }

            hotfix.ChrModelID = packet.ReadInt32("ChrModelID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.ChrCustomizationCategoryID = packet.ReadInt32("ChrCustomizationCategoryID", indexes);
            hotfix.OptionType = packet.ReadInt32("OptionType", indexes);
            hotfix.BarberShopCostModifier = packet.ReadSingle("BarberShopCostModifier", indexes);
            hotfix.ChrCustomizationID = packet.ReadInt32("ChrCustomizationID", indexes);
            hotfix.ChrCustomizationReqID = packet.ReadInt32("ChrCustomizationReqID", indexes);
            hotfix.SecondaryOrderIndex = packet.ReadInt32("SecondaryOrderIndex", indexes);

            Storage.ChrCustomizationOptionHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChrCustomizationOptionLocaleHotfix440 hotfixLocale = new ChrCustomizationOptionLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ChrCustomizationOptionHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ChrCustomizationReqHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationReqHotfix440 hotfix = new ChrCustomizationReqHotfix440();

            hotfix.RaceMask = packet.ReadInt64("RaceMask", indexes);
            hotfix.ReqSource = packet.ReadCString("ReqSource", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ReqType = packet.ReadInt32("ReqType", indexes);
            hotfix.ClassMask = packet.ReadInt32("ClassMask", indexes);
            hotfix.ReqAchievementID = packet.ReadInt32("ReqAchievementID", indexes);
            hotfix.ReqQuestID = packet.ReadInt32("ReqQuestID", indexes);
            hotfix.OverrideArchive = packet.ReadInt32("OverrideArchive", indexes);
            hotfix.ReqItemModifiedAppearanceID = packet.ReadInt32("ReqItemModifiedAppearanceID", indexes);

            Storage.ChrCustomizationReqHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChrCustomizationReqLocaleHotfix440 hotfixLocale = new ChrCustomizationReqLocaleHotfix440
                {
                    ID = hotfix.ID,
                    ReqSourceLang = hotfix.ReqSource,
                };
                Storage.ChrCustomizationReqHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ChrCustomizationReqChoiceHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ChrCustomizationReqChoiceHotfix440 hotfix = new ChrCustomizationReqChoiceHotfix440();

            hotfix.ID = entry;
            hotfix.ChrCustomizationChoiceID = packet.ReadInt32("ChrCustomizationChoiceID", indexes);
            hotfix.ChrCustomizationReqID = packet.ReadInt32("ChrCustomizationReqID", indexes);

            Storage.ChrCustomizationReqChoiceHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrModelHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ChrModelHotfix440 hotfix = new ChrModelHotfix440();

            hotfix.FaceCustomizationOffsetX = packet.ReadSingle("FaceCustomizationOffsetX", indexes);
            hotfix.FaceCustomizationOffsetY = packet.ReadSingle("FaceCustomizationOffsetY", indexes);
            hotfix.FaceCustomizationOffsetZ = packet.ReadSingle("FaceCustomizationOffsetZ", indexes);
            hotfix.CustomizeOffsetX = packet.ReadSingle("CustomizeOffsetX", indexes);
            hotfix.CustomizeOffsetY = packet.ReadSingle("CustomizeOffsetY", indexes);
            hotfix.CustomizeOffsetZ = packet.ReadSingle("CustomizeOffsetZ", indexes);
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
            hotfix.BarberShopCameraRotationFacing = packet.ReadSingle("BarberShopCameraRotationFacing", indexes);
            hotfix.BarberShopCameraRotationOffset = packet.ReadSingle("BarberShopCameraRotationOffset", indexes);

            Storage.ChrModelHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrRaceXChrModelHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ChrRaceXChrModelHotfix440 hotfix = new ChrRaceXChrModelHotfix440();

            hotfix.ID = entry;
            hotfix.ChrRacesID = packet.ReadInt32("ChrRacesID", indexes);
            hotfix.ChrModelID = packet.ReadInt32("ChrModelID", indexes);
            hotfix.Sex = packet.ReadInt32("Sex", indexes);
            hotfix.AllowedTransmogSlots = packet.ReadInt32("AllowedTransmogSlots", indexes);

            Storage.ChrRaceXChrModelHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ChrRacesHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ChrRacesHotfix440 hotfix = new ChrRacesHotfix440();

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

            Storage.ChrRacesHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ChrRacesLocaleHotfix440 hotfixLocale = new ChrRacesLocaleHotfix440
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
                Storage.ChrRacesHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CinematicCameraHandler440(Packet packet, uint entry, params object[] indexes)
        {
            CinematicCameraHotfix440 hotfix = new CinematicCameraHotfix440();

            hotfix.ID = entry;
            hotfix.OriginX = packet.ReadSingle("OriginX", indexes);
            hotfix.OriginY = packet.ReadSingle("OriginY", indexes);
            hotfix.OriginZ = packet.ReadSingle("OriginZ", indexes);
            hotfix.SoundID = packet.ReadUInt32("SoundID", indexes);
            hotfix.OriginFacing = packet.ReadSingle("OriginFacing", indexes);
            hotfix.FileDataID = packet.ReadUInt32("FileDataID", indexes);

            Storage.CinematicCameraHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void CinematicSequencesHandler440(Packet packet, uint entry, params object[] indexes)
        {
            CinematicSequencesHotfix440 hotfix = new CinematicSequencesHotfix440();

            hotfix.ID = entry;
            hotfix.SoundID = packet.ReadUInt32("SoundID", indexes);
            hotfix.Camera = new ushort?[8];
            for (int i = 0; i < 8; i++)
                hotfix.Camera[i] = packet.ReadUInt16("Camera", indexes, i);

            Storage.CinematicSequencesHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ConditionalChrModelHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ConditionalChrModelHotfix440 hotfix = new ConditionalChrModelHotfix440();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ChrModelID = packet.ReadInt32("ChrModelID", indexes);
            hotfix.ChrCustomizationReqID = packet.ReadInt32("ChrCustomizationReqID", indexes);
            hotfix.PlayerConditionID = packet.ReadInt32("PlayerConditionID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ChrCustomizationCategoryID = packet.ReadInt32("ChrCustomizationCategoryID", indexes);

            Storage.ConditionalChrModelHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ConditionalContentTuningHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ConditionalContentTuningHotfix440 hotfix = new ConditionalContentTuningHotfix440();

            hotfix.ID = entry;
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.RedirectContentTuningID = packet.ReadInt32("RedirectContentTuningID", indexes);
            hotfix.RedirectFlag = packet.ReadInt32("RedirectFlag", indexes);
            hotfix.ParentContentTuningID = packet.ReadInt32("ParentContentTuningID", indexes);

            Storage.ConditionalContentTuningHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ContentTuningHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ContentTuningHotfix440 hotfix = new ContentTuningHotfix440();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.MinLevel = packet.ReadInt32("MinLevel", indexes);
            hotfix.MaxLevel = packet.ReadInt32("MaxLevel", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ExpectedStatModID = packet.ReadInt32("ExpectedStatModID", indexes);
            hotfix.DifficultyESMID = packet.ReadInt32("DifficultyESMID", indexes);

            Storage.ContentTuningHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ConversationLineHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ConversationLineHotfix440 hotfix = new ConversationLineHotfix440();

            hotfix.ID = entry;
            hotfix.BroadcastTextID = packet.ReadUInt32("BroadcastTextID", indexes);
            hotfix.SpellVisualKitID = packet.ReadUInt32("SpellVisualKitID", indexes);
            hotfix.AdditionalDuration = packet.ReadInt32("AdditionalDuration", indexes);
            hotfix.NextConversationLineID = packet.ReadUInt16("NextConversationLineID", indexes);
            hotfix.AnimKitID = packet.ReadUInt16("AnimKitID", indexes);
            hotfix.SpeechType = packet.ReadByte("SpeechType", indexes);
            hotfix.StartAnimation = packet.ReadByte("StartAnimation", indexes);
            hotfix.EndAnimation = packet.ReadByte("EndAnimation", indexes);

            Storage.ConversationLineHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void CreatureDisplayInfoHandler440(Packet packet, uint entry, params object[] indexes)
        {
            CreatureDisplayInfoHotfix440 hotfix = new CreatureDisplayInfoHotfix440();

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

            Storage.CreatureDisplayInfoHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void CreatureDisplayInfoHandler441(Packet packet, uint entry, params object[] indexes)
        {
            CreatureDisplayInfoHotfix441 hotfix = new CreatureDisplayInfoHotfix441();

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
            hotfix.Unknown1154 = packet.ReadUInt16("Unknown1154", indexes);
            hotfix.TextureVariationFileDataID = new int?[4];
            for (int i = 0; i < 4; i++)
                hotfix.TextureVariationFileDataID[i] = packet.ReadInt32("TextureVariationFileDataID", indexes, i);

            Storage.CreatureDisplayInfoHotfixes441.Add(hotfix, packet.TimeSpan);
        }

        public static void CreatureDisplayInfoExtraHandler440(Packet packet, uint entry, params object[] indexes)
        {
            CreatureDisplayInfoExtraHotfix440 hotfix = new CreatureDisplayInfoExtraHotfix440();

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

            Storage.CreatureDisplayInfoExtraHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void CreatureFamilyHandler440(Packet packet, uint entry, params object[] indexes)
        {
            CreatureFamilyHotfix440 hotfix = new CreatureFamilyHotfix440();

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

            Storage.CreatureFamilyHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CreatureFamilyLocaleHotfix440 hotfixLocale = new CreatureFamilyLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.CreatureFamilyHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CreatureModelDataHandler440(Packet packet, uint entry, params object[] indexes)
        {
            CreatureModelDataHotfix440 hotfix = new CreatureModelDataHotfix440();

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

            Storage.CreatureModelDataHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void CreatureModelDataHandler441(Packet packet, uint entry, params object[] indexes)
        {
            CreatureModelDataHotfix441 hotfix = new CreatureModelDataHotfix441();

            hotfix.ID = entry;
            hotfix.GeoBox = new float?[6];
            for (int i = 0; i < 6; i++)
                hotfix.GeoBox[i] = packet.ReadSingle("GeoBox", indexes, i);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
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
            hotfix.Unknown1154 = packet.ReadUInt16("Unknown1154", indexes);

            Storage.CreatureModelDataHotfixes441.Add(hotfix, packet.TimeSpan);
        }

        public static void CreatureTypeHandler440(Packet packet, uint entry, params object[] indexes)
        {
            CreatureTypeHotfix440 hotfix = new CreatureTypeHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);

            Storage.CreatureTypeHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CreatureTypeLocaleHotfix440 hotfixLocale = new CreatureTypeLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.CreatureTypeHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CriteriaHandler440(Packet packet, uint entry, params object[] indexes)
        {
            CriteriaHotfix440 hotfix = new CriteriaHotfix440();

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

            Storage.CriteriaHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void CriteriaTreeHandler440(Packet packet, uint entry, params object[] indexes)
        {
            CriteriaTreeHotfix440 hotfix = new CriteriaTreeHotfix440();

            hotfix.ID = entry;
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.Parent = packet.ReadUInt32("Parent", indexes);
            hotfix.Amount = packet.ReadUInt32("Amount", indexes);
            hotfix.Operator = packet.ReadInt32("Operator", indexes);
            hotfix.CriteriaID = packet.ReadUInt32("CriteriaID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.CriteriaTreeHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CriteriaTreeLocaleHotfix440 hotfixLocale = new CriteriaTreeLocaleHotfix440
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                };
                Storage.CriteriaTreeHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CurrencyContainerHandler440(Packet packet, uint entry, params object[] indexes)
        {
            CurrencyContainerHotfix440 hotfix = new CurrencyContainerHotfix440();

            hotfix.ID = entry;
            hotfix.ContainerName = packet.ReadCString("ContainerName", indexes);
            hotfix.ContainerDescription = packet.ReadCString("ContainerDescription", indexes);
            hotfix.MinAmount = packet.ReadInt32("MinAmount", indexes);
            hotfix.MaxAmount = packet.ReadInt32("MaxAmount", indexes);
            hotfix.ContainerIconID = packet.ReadInt32("ContainerIconID", indexes);
            hotfix.ContainerQuality = packet.ReadInt32("ContainerQuality", indexes);
            hotfix.OnLootSpellVisualKitID = packet.ReadInt32("OnLootSpellVisualKitID", indexes);
            hotfix.CurrencyTypesID = packet.ReadInt32("CurrencyTypesID", indexes);

            Storage.CurrencyContainerHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CurrencyContainerLocaleHotfix440 hotfixLocale = new CurrencyContainerLocaleHotfix440
                {
                    ID = hotfix.ID,
                    ContainerNameLang = hotfix.ContainerName,
                    ContainerDescriptionLang = hotfix.ContainerDescription,
                };
                Storage.CurrencyContainerHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CurrencyTypesHandler440(Packet packet, uint entry, params object[] indexes)
        {
            CurrencyTypesHotfix440 hotfix = new CurrencyTypesHotfix440();

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
            hotfix.MaxQtyWorldStateID = packet.ReadInt32("MaxQtyWorldStateID", indexes);
            hotfix.Flags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.CurrencyTypesHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CurrencyTypesLocaleHotfix440 hotfixLocale = new CurrencyTypesLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.CurrencyTypesHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CurrencyTypesHandler441(Packet packet, uint entry, params object[] indexes)
        {
            CurrencyTypesHotfix441 hotfix = new CurrencyTypesHotfix441();

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
            hotfix.MaxQtyWorldStateID = packet.ReadInt32("MaxQtyWorldStateID", indexes);
            hotfix.Unknown1154 = packet.ReadSingle("Unknown1154", indexes);
            hotfix.Flags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.CurrencyTypesHotfixes441.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CurrencyTypesLocaleHotfix441 hotfixLocale = new CurrencyTypesLocaleHotfix441
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.CurrencyTypesHotfixesLocale441.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void CurveHandler440(Packet packet, uint entry, params object[] indexes)
        {
            CurveHotfix440 hotfix = new CurveHotfix440();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Type = packet.ReadByte("Type", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);

            Storage.CurveHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void CurvePointHandler440(Packet packet, uint entry, params object[] indexes)
        {
            CurvePointHotfix440 hotfix = new CurvePointHotfix440();

            hotfix.PosX = packet.ReadSingle("PosX", indexes);
            hotfix.PosY = packet.ReadSingle("PosY", indexes);
            hotfix.PreSLSquishPosX = packet.ReadSingle("PreSLSquishPosX", indexes);
            hotfix.PreSLSquishPosY = packet.ReadSingle("PreSLSquishPosY", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.CurveID = packet.ReadUInt32("CurveID", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);

            Storage.CurvePointHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void DestructibleModelDataHandler440(Packet packet, uint entry, params object[] indexes)
        {
            DestructibleModelDataHotfix440 hotfix = new DestructibleModelDataHotfix440();

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

            Storage.DestructibleModelDataHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void DifficultyHandler440(Packet packet, uint entry, params object[] indexes)
        {
            DifficultyHotfix440 hotfix = new DifficultyHotfix440();

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

            Storage.DifficultyHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                DifficultyLocaleHotfix440 hotfixLocale = new DifficultyLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.DifficultyHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void DifficultyHandler441(Packet packet, uint entry, params object[] indexes)
        {
            DifficultyHotfix441 hotfix = new DifficultyHotfix441();

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
            hotfix.Unknown1154 = packet.ReadInt32("Unknown1154", indexes);

            Storage.DifficultyHotfixes441.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                DifficultyLocaleHotfix441 hotfixLocale = new DifficultyLocaleHotfix441
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.DifficultyHotfixesLocale441.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void DungeonEncounterHandler440(Packet packet, uint entry, params object[] indexes)
        {
            DungeonEncounterHotfix440 hotfix = new DungeonEncounterHotfix440();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.MapID = packet.ReadInt16("MapID", indexes);
            hotfix.DifficultyID = packet.ReadInt32("DifficultyID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.CompleteWorldStateID = packet.ReadInt32("CompleteWorldStateID", indexes);
            hotfix.Bit = packet.ReadSByte("Bit", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.SpellIconFileID = packet.ReadInt32("SpellIconFileID", indexes);
            hotfix.Faction = packet.ReadInt32("Faction", indexes);

            Storage.DungeonEncounterHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                DungeonEncounterLocaleHotfix440 hotfixLocale = new DungeonEncounterLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.DungeonEncounterHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void DurabilityCostsHandler440(Packet packet, uint entry, params object[] indexes)
        {
            DurabilityCostsHotfix440 hotfix = new DurabilityCostsHotfix440();

            hotfix.ID = entry;
            hotfix.WeaponSubClassCost = new ushort?[21];
            for (int i = 0; i < 21; i++)
                hotfix.WeaponSubClassCost[i] = packet.ReadUInt16("WeaponSubClassCost", indexes, i);
            hotfix.ArmorSubClassCost = new ushort?[8];
            for (int i = 0; i < 8; i++)
                hotfix.ArmorSubClassCost[i] = packet.ReadUInt16("ArmorSubClassCost", indexes, i);

            Storage.DurabilityCostsHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void DurabilityQualityHandler440(Packet packet, uint entry, params object[] indexes)
        {
            DurabilityQualityHotfix440 hotfix = new DurabilityQualityHotfix440();

            hotfix.ID = entry;
            hotfix.Data = packet.ReadSingle("Data", indexes);

            Storage.DurabilityQualityHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void EmotesHandler440(Packet packet, uint entry, params object[] indexes)
        {
            EmotesHotfix440 hotfix = new EmotesHotfix440();

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

            Storage.EmotesHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void EmotesHandler441(Packet packet, uint entry, params object[] indexes)
        {
            EmotesHotfix441 hotfix = new EmotesHotfix441();

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

            Storage.EmotesHotfixes441.Add(hotfix, packet.TimeSpan);
        }

        public static void EmotesTextHandler440(Packet packet, uint entry, params object[] indexes)
        {
            EmotesTextHotfix440 hotfix = new EmotesTextHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.EmoteID = packet.ReadUInt16("EmoteID", indexes);

            Storage.EmotesTextHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void EmotesTextSoundHandler440(Packet packet, uint entry, params object[] indexes)
        {
            EmotesTextSoundHotfix440 hotfix = new EmotesTextSoundHotfix440();

            hotfix.ID = entry;
            hotfix.RaceID = packet.ReadByte("RaceID", indexes);
            hotfix.ClassID = packet.ReadByte("ClassID", indexes);
            hotfix.SexID = packet.ReadByte("SexID", indexes);
            hotfix.SoundID = packet.ReadUInt32("SoundID", indexes);
            hotfix.EmotesTextID = packet.ReadInt32("EmotesTextID", indexes);

            Storage.EmotesTextSoundHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ExpectedStatHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ExpectedStatHotfix440 hotfix = new ExpectedStatHotfix440();

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

            Storage.ExpectedStatHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ExpectedStatModHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ExpectedStatModHotfix440 hotfix = new ExpectedStatModHotfix440();

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

            Storage.ExpectedStatModHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void FactionHandler440(Packet packet, uint entry, params object[] indexes)
        {
            FactionHotfix440 hotfix = new FactionHotfix440();

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

            Storage.FactionHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                FactionLocaleHotfix440 hotfixLocale = new FactionLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.FactionHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void FactionTemplateHandler440(Packet packet, uint entry, params object[] indexes)
        {
            FactionTemplateHotfix440 hotfix = new FactionTemplateHotfix440();

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

            Storage.FactionTemplateHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void FactionTemplateHandler441(Packet packet, uint entry, params object[] indexes)
        {
            FactionTemplateHotfix441 hotfix = new FactionTemplateHotfix441();

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

            Storage.FactionTemplateHotfixes441.Add(hotfix, packet.TimeSpan);
        }

        public static void FriendshipRepReactionHandler440(Packet packet, uint entry, params object[] indexes)
        {
            FriendshipRepReactionHotfix440 hotfix = new FriendshipRepReactionHotfix440();

            hotfix.ID = entry;
            hotfix.Reaction = packet.ReadCString("Reaction", indexes);
            hotfix.FriendshipRepID = packet.ReadByte("FriendshipRepID", indexes);
            hotfix.ReactionThreshold = packet.ReadUInt16("ReactionThreshold", indexes);

            Storage.FriendshipRepReactionHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                FriendshipRepReactionLocaleHotfix440 hotfixLocale = new FriendshipRepReactionLocaleHotfix440
                {
                    ID = hotfix.ID,
                    ReactionLang = hotfix.Reaction,
                };
                Storage.FriendshipRepReactionHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void FriendshipReputationHandler440(Packet packet, uint entry, params object[] indexes)
        {
            FriendshipReputationHotfix440 hotfix = new FriendshipReputationHotfix440();

            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Unknown341_0 = packet.ReadInt32("Unknown341_0", indexes);
            hotfix.Unknown341_1 = packet.ReadInt32("Unknown341_1", indexes);

            Storage.FriendshipReputationHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                FriendshipReputationLocaleHotfix440 hotfixLocale = new FriendshipReputationLocaleHotfix440
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                };
                Storage.FriendshipReputationHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void GameobjectArtKitHandler440(Packet packet, uint entry, params object[] indexes)
        {
            GameobjectArtKitHotfix440 hotfix = new GameobjectArtKitHotfix440();

            hotfix.ID = entry;
            hotfix.AttachModelFileID = packet.ReadInt32("AttachModelFileID", indexes);
            hotfix.TextureVariationFileID = new int?[3];
            for (int i = 0; i < 3; i++)
                hotfix.TextureVariationFileID[i] = packet.ReadInt32("TextureVariationFileID", indexes, i);

            Storage.GameobjectArtKitHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void GameobjectDisplayInfoHandler440(Packet packet, uint entry, params object[] indexes)
        {
            GameobjectDisplayInfoHotfix440 hotfix = new GameobjectDisplayInfoHotfix440();

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

            Storage.GameobjectDisplayInfoHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void GameobjectDisplayInfoHandler441(Packet packet, uint entry, params object[] indexes)
        {
            GameobjectDisplayInfoHotfix441 hotfix = new GameobjectDisplayInfoHotfix441();

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
            hotfix.Unknown1154 = packet.ReadUInt16("Unknown1154", indexes);

            Storage.GameobjectDisplayInfoHotfixes441.Add(hotfix, packet.TimeSpan);
        }

        public static void GameobjectsHandler440(Packet packet, uint entry, params object[] indexes)
        {
            GameobjectsHotfix440 hotfix = new GameobjectsHotfix440();

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
            hotfix.PhaseUseFlags = packet.ReadInt32("PhaseUseFlags", indexes);
            hotfix.PhaseID = packet.ReadUInt16("PhaseID", indexes);
            hotfix.PhaseGroupID = packet.ReadUInt16("PhaseGroupID", indexes);
            hotfix.PropValue = new int?[8];
            for (int i = 0; i < 8; i++)
                hotfix.PropValue[i] = packet.ReadInt32("PropValue", indexes, i);

            Storage.GameobjectsHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                GameobjectsLocaleHotfix440 hotfixLocale = new GameobjectsLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.GameobjectsHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void GameobjectsHandler441(Packet packet, uint entry, params object[] indexes)
        {
            GameobjectsHotfix441 hotfix = new GameobjectsHotfix441();

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
            hotfix.PhaseUseFlags = packet.ReadInt32("PhaseUseFlags", indexes);
            hotfix.PhaseID = packet.ReadUInt16("PhaseID", indexes);
            hotfix.PhaseGroupID = packet.ReadUInt16("PhaseGroupID", indexes);
            hotfix.Unknown1100 = packet.ReadUInt16("Unknown1100", indexes);
            hotfix.PropValue = new int?[8];
            for (int i = 0; i < 8; i++)
                hotfix.PropValue[i] = packet.ReadInt32("PropValue", indexes, i);

            Storage.GameobjectsHotfixes441.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                GameobjectsLocaleHotfix441 hotfixLocale = new GameobjectsLocaleHotfix441
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.GameobjectsHotfixesLocale441.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void GemPropertiesHandler440(Packet packet, uint entry, params object[] indexes)
        {
            GemPropertiesHotfix440 hotfix = new GemPropertiesHotfix440();

            hotfix.ID = entry;
            hotfix.EnchantID = packet.ReadUInt16("EnchantID", indexes);
            hotfix.Type = packet.ReadInt32("Type", indexes);
            hotfix.MinItemLevel = packet.ReadUInt16("MinItemLevel", indexes);

            Storage.GemPropertiesHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void GlyphBindableSpellHandler440(Packet packet, uint entry, params object[] indexes)
        {
            GlyphBindableSpellHotfix440 hotfix = new GlyphBindableSpellHotfix440();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.GlyphPropertiesID = packet.ReadInt32("GlyphPropertiesID", indexes);

            Storage.GlyphBindableSpellHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void GlyphSlotHandler440(Packet packet, uint entry, params object[] indexes)
        {
            GlyphSlotHotfix440 hotfix = new GlyphSlotHotfix440();

            hotfix.ID = entry;
            hotfix.Tooltip = packet.ReadInt32("Tooltip", indexes);
            hotfix.Type = packet.ReadUInt32("Type", indexes);

            Storage.GlyphSlotHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void GlyphPropertiesHandler440(Packet packet, uint entry, params object[] indexes)
        {
            GlyphPropertiesHotfix440 hotfix = new GlyphPropertiesHotfix440();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadUInt32("SpellID", indexes);
            hotfix.GlyphType = packet.ReadByte("GlyphType", indexes);
            hotfix.GlyphExclusiveCategoryID = packet.ReadByte("GlyphExclusiveCategoryID", indexes);
            hotfix.SpellIconFileDataID = packet.ReadInt32("SpellIconFileDataID", indexes);
            hotfix.GlyphSlotFlags = packet.ReadUInt32("GlyphSlotFlags", indexes);

            Storage.GlyphPropertiesHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void GlyphRequiredSpecHandler440(Packet packet, uint entry, params object[] indexes)
        {
            GlyphRequiredSpecHotfix440 hotfix = new GlyphRequiredSpecHotfix440();

            hotfix.ID = entry;
            hotfix.ChrSpecializationID = packet.ReadUInt16("ChrSpecializationID", indexes);
            hotfix.GlyphPropertiesID = packet.ReadInt32("GlyphPropertiesID", indexes);

            Storage.GlyphRequiredSpecHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void GossipNPCOptionHandler440(Packet packet, uint entry, params object[] indexes)
        {
            GossipNPCOptionHotfix440 hotfix = new GossipNPCOptionHotfix440();

            hotfix.ID = entry;
            hotfix.GossipNpcOption = packet.ReadInt32("GossipNpcOption", indexes);
            hotfix.LFGDungeonsID = packet.ReadInt32("LFGDungeonsID", indexes);
            hotfix.TrainerID = packet.ReadInt32("TrainerID", indexes);
            hotfix.Unknown341_0 = packet.ReadInt32("Unknown341_0", indexes);
            hotfix.Unknown341_1 = packet.ReadInt32("Unknown341_1", indexes);
            hotfix.Unknown341_2 = packet.ReadInt32("Unknown341_2", indexes);
            hotfix.Unknown341_3 = packet.ReadInt32("Unknown341_3", indexes);
            hotfix.Unknown341_4 = packet.ReadInt32("Unknown341_4", indexes);
            hotfix.Unknown341_5 = packet.ReadInt32("Unknown341_5", indexes);
            hotfix.Unknown341_6 = packet.ReadInt32("Unknown341_6", indexes);
            hotfix.Unknown341_7 = packet.ReadInt32("Unknown341_7", indexes);
            hotfix.GossipOptionID = packet.ReadInt32("GossipOptionID", indexes);

            Storage.GossipNPCOptionHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void GuildColorBackgroundHandler440(Packet packet, uint entry, params object[] indexes)
        {
            GuildColorBackgroundHotfix440 hotfix = new GuildColorBackgroundHotfix440();

            hotfix.ID = entry;
            hotfix.Red = packet.ReadByte("Red", indexes);
            hotfix.Blue = packet.ReadByte("Blue", indexes);
            hotfix.Green = packet.ReadByte("Green", indexes);

            Storage.GuildColorBackgroundHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void GuildColorBorderHandler440(Packet packet, uint entry, params object[] indexes)
        {
            GuildColorBorderHotfix440 hotfix = new GuildColorBorderHotfix440();

            hotfix.ID = entry;
            hotfix.Red = packet.ReadByte("Red", indexes);
            hotfix.Blue = packet.ReadByte("Blue", indexes);
            hotfix.Green = packet.ReadByte("Green", indexes);

            Storage.GuildColorBorderHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void GuildColorEmblemHandler440(Packet packet, uint entry, params object[] indexes)
        {
            GuildColorEmblemHotfix440 hotfix = new GuildColorEmblemHotfix440();

            hotfix.ID = entry;
            hotfix.Red = packet.ReadByte("Red", indexes);
            hotfix.Blue = packet.ReadByte("Blue", indexes);
            hotfix.Green = packet.ReadByte("Green", indexes);

            Storage.GuildColorEmblemHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void GuildPerkSpellsHandler440(Packet packet, uint entry, params object[] indexes)
        {
            GuildPerkSpellsHotfix440 hotfix = new GuildPerkSpellsHotfix440();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);

            Storage.GuildPerkSpellsHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void HeirloomHandler440(Packet packet, uint entry, params object[] indexes)
        {
            HeirloomHotfix440 hotfix = new HeirloomHotfix440();

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

            Storage.HeirloomHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                HeirloomLocaleHotfix440 hotfixLocale = new HeirloomLocaleHotfix440
                {
                    ID = hotfix.ID,
                    SourceTextLang = hotfix.SourceText,
                };
                Storage.HeirloomHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void HolidaysHandler440(Packet packet, uint entry, params object[] indexes)
        {
            HolidaysHotfix440 hotfix = new HolidaysHotfix440();

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

            Storage.HolidaysHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ImportPriceArmorHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ImportPriceArmorHotfix440 hotfix = new ImportPriceArmorHotfix440();

            hotfix.ID = entry;
            hotfix.ClothModifier = packet.ReadSingle("ClothModifier", indexes);
            hotfix.LeatherModifier = packet.ReadSingle("LeatherModifier", indexes);
            hotfix.ChainModifier = packet.ReadSingle("ChainModifier", indexes);
            hotfix.PlateModifier = packet.ReadSingle("PlateModifier", indexes);

            Storage.ImportPriceArmorHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ImportPriceQualityHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ImportPriceQualityHotfix440 hotfix = new ImportPriceQualityHotfix440();

            hotfix.ID = entry;
            hotfix.Data = packet.ReadSingle("Data", indexes);

            Storage.ImportPriceQualityHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ImportPriceShieldHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ImportPriceShieldHotfix440 hotfix = new ImportPriceShieldHotfix440();

            hotfix.ID = entry;
            hotfix.Data = packet.ReadSingle("Data", indexes);

            Storage.ImportPriceShieldHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ImportPriceWeaponHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ImportPriceWeaponHotfix440 hotfix = new ImportPriceWeaponHotfix440();

            hotfix.ID = entry;
            hotfix.Data = packet.ReadSingle("Data", indexes);

            Storage.ImportPriceWeaponHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemHotfix440 hotfix = new ItemHotfix440();

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
            hotfix.Resistances = new int?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Resistances[i] = packet.ReadInt32("Resistances", indexes, i);
            hotfix.MinDamage = new int?[5];
            for (int i = 0; i < 5; i++)
                hotfix.MinDamage[i] = packet.ReadInt32("MinDamage", indexes, i);
            hotfix.MaxDamage = new int?[5];
            for (int i = 0; i < 5; i++)
                hotfix.MaxDamage[i] = packet.ReadInt32("MaxDamage", indexes, i);

            Storage.ItemHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemAppearanceHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemAppearanceHotfix440 hotfix = new ItemAppearanceHotfix440();

            hotfix.ID = entry;
            hotfix.DisplayType = packet.ReadByte("DisplayType", indexes);
            hotfix.ItemDisplayInfoID = packet.ReadInt32("ItemDisplayInfoID", indexes);
            hotfix.DefaultIconFileDataID = packet.ReadInt32("DefaultIconFileDataID", indexes);
            hotfix.UiOrder = packet.ReadInt32("UiOrder", indexes);

            Storage.ItemAppearanceHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemAppearanceHandler441(Packet packet, uint entry, params object[] indexes)
        {
            ItemAppearanceHotfix441 hotfix = new ItemAppearanceHotfix441();

            hotfix.ID = entry;
            hotfix.DisplayType = packet.ReadSByte("DisplayType", indexes);
            hotfix.ItemDisplayInfoID = packet.ReadInt32("ItemDisplayInfoID", indexes);
            hotfix.DefaultIconFileDataID = packet.ReadInt32("DefaultIconFileDataID", indexes);
            hotfix.UiOrder = packet.ReadInt32("UiOrder", indexes);

            Storage.ItemAppearanceHotfixes441.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemArmorQualityHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemArmorQualityHotfix440 hotfix = new ItemArmorQualityHotfix440();

            hotfix.ID = entry;
            hotfix.Qualitymod = new float?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Qualitymod[i] = packet.ReadSingle("Qualitymod", indexes, i);

            Storage.ItemArmorQualityHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemArmorShieldHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemArmorShieldHotfix440 hotfix = new ItemArmorShieldHotfix440();

            hotfix.ID = entry;
            hotfix.Quality = new float?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Quality[i] = packet.ReadSingle("Quality", indexes, i);
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);

            Storage.ItemArmorShieldHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemArmorTotalHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemArmorTotalHotfix440 hotfix = new ItemArmorTotalHotfix440();

            hotfix.ID = entry;
            hotfix.ItemLevel = packet.ReadInt16("ItemLevel", indexes);
            hotfix.Cloth = packet.ReadSingle("Cloth", indexes);
            hotfix.Leather = packet.ReadSingle("Leather", indexes);
            hotfix.Mail = packet.ReadSingle("Mail", indexes);
            hotfix.Plate = packet.ReadSingle("Plate", indexes);

            Storage.ItemArmorTotalHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemBagFamilyHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemBagFamilyHotfix440 hotfix = new ItemBagFamilyHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);

            Storage.ItemBagFamilyHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemBagFamilyLocaleHotfix440 hotfixLocale = new ItemBagFamilyLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ItemBagFamilyHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemBonusHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemBonusHotfix440 hotfix = new ItemBonusHotfix440();

            hotfix.ID = entry;
            hotfix.Value = new int?[4];
            for (int i = 0; i < 4; i++)
                hotfix.Value[i] = packet.ReadInt32("Value", indexes, i);
            hotfix.ParentItemBonusListID = packet.ReadUInt16("ParentItemBonusListID", indexes);
            hotfix.Type = packet.ReadByte("Type", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);

            Storage.ItemBonusHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemBonusListLevelDeltaHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemBonusListLevelDeltaHotfix440 hotfix = new ItemBonusListLevelDeltaHotfix440();

            hotfix.ItemLevelDelta = packet.ReadInt16("ItemLevelDelta", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);

            Storage.ItemBonusListLevelDeltaHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemBonusTreeNodeHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemBonusTreeNodeHotfix440 hotfix = new ItemBonusTreeNodeHotfix440();

            hotfix.ID = entry;
            hotfix.ItemContext = packet.ReadByte("ItemContext", indexes);
            hotfix.ChildItemBonusTreeID = packet.ReadUInt16("ChildItemBonusTreeID", indexes);
            hotfix.ChildItemBonusListID = packet.ReadUInt16("ChildItemBonusListID", indexes);
            hotfix.ChildItemLevelSelectorID = packet.ReadUInt16("ChildItemLevelSelectorID", indexes);
            hotfix.ChildItemBonusListGroupID = packet.ReadInt32("ChildItemBonusListGroupID", indexes);
            hotfix.IblGroupPointsModSetID = packet.ReadInt32("IblGroupPointsModSetID", indexes);
            hotfix.MinMythicPlusLevel = packet.ReadInt32("MinMythicPlusLevel", indexes);
            hotfix.MaxMythicPlusLevel = packet.ReadInt32("MaxMythicPlusLevel", indexes);
            hotfix.ParentItemBonusTreeID = packet.ReadInt32("ParentItemBonusTreeID", indexes);

            Storage.ItemBonusTreeNodeHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemChildEquipmentHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemChildEquipmentHotfix440 hotfix = new ItemChildEquipmentHotfix440();

            hotfix.ID = entry;
            hotfix.ChildItemID = packet.ReadInt32("ChildItemID", indexes);
            hotfix.ChildItemEquipSlot = packet.ReadByte("ChildItemEquipSlot", indexes);
            hotfix.ParentItemID = packet.ReadInt32("ParentItemID", indexes);

            Storage.ItemChildEquipmentHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemClassHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemClassHotfix440 hotfix = new ItemClassHotfix440();

            hotfix.ID = entry;
            hotfix.ClassName = packet.ReadCString("ClassName", indexes);
            hotfix.ClassID = packet.ReadSByte("ClassID", indexes);
            hotfix.PriceModifier = packet.ReadSingle("PriceModifier", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);

            Storage.ItemClassHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemClassLocaleHotfix440 hotfixLocale = new ItemClassLocaleHotfix440
                {
                    ID = hotfix.ID,
                    ClassNameLang = hotfix.ClassName,
                };
                Storage.ItemClassHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemContextPickerEntryHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemContextPickerEntryHotfix440 hotfix = new ItemContextPickerEntryHotfix440();

            hotfix.ID = entry;
            hotfix.ItemCreationContext = packet.ReadByte("ItemCreationContext", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);
            hotfix.PVal = packet.ReadInt32("PVal", indexes);
            hotfix.Flags = packet.ReadUInt32("Flags", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.ItemContextPickerID = packet.ReadInt32("ItemContextPickerID", indexes);

            Storage.ItemContextPickerEntryHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemCurrencyCostHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemCurrencyCostHotfix440 hotfix = new ItemCurrencyCostHotfix440();

            hotfix.ID = entry;
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);

            Storage.ItemCurrencyCostHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemDamageAmmoHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemDamageAmmoHotfix440 hotfix = new ItemDamageAmmoHotfix440();

            hotfix.ID = entry;
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);
            hotfix.Quality = new float?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Quality[i] = packet.ReadSingle("Quality", indexes, i);

            Storage.ItemDamageAmmoHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemDamageOneHandHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemDamageOneHandHotfix440 hotfix = new ItemDamageOneHandHotfix440();

            hotfix.ID = entry;
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);
            hotfix.Quality = new float?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Quality[i] = packet.ReadSingle("Quality", indexes, i);

            Storage.ItemDamageOneHandHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemDamageOneHandCasterHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemDamageOneHandCasterHotfix440 hotfix = new ItemDamageOneHandCasterHotfix440();

            hotfix.ID = entry;
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);
            hotfix.Quality = new float?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Quality[i] = packet.ReadSingle("Quality", indexes, i);

            Storage.ItemDamageOneHandCasterHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemDamageTwoHandHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemDamageTwoHandHotfix440 hotfix = new ItemDamageTwoHandHotfix440();

            hotfix.ID = entry;
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);
            hotfix.Quality = new float?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Quality[i] = packet.ReadSingle("Quality", indexes, i);

            Storage.ItemDamageTwoHandHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemDamageTwoHandCasterHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemDamageTwoHandCasterHotfix440 hotfix = new ItemDamageTwoHandCasterHotfix440();

            hotfix.ID = entry;
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);
            hotfix.Quality = new float?[7];
            for (int i = 0; i < 7; i++)
                hotfix.Quality[i] = packet.ReadSingle("Quality", indexes, i);

            Storage.ItemDamageTwoHandCasterHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemDisenchantLootHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemDisenchantLootHotfix440 hotfix = new ItemDisenchantLootHotfix440();

            hotfix.ID = entry;
            hotfix.Subclass = packet.ReadSByte("Subclass", indexes);
            hotfix.Quality = packet.ReadByte("Quality", indexes);
            hotfix.MinLevel = packet.ReadUInt16("MinLevel", indexes);
            hotfix.MaxLevel = packet.ReadUInt16("MaxLevel", indexes);
            hotfix.SkillRequired = packet.ReadUInt16("SkillRequired", indexes);
            hotfix.ExpansionID = packet.ReadSByte("ExpansionID", indexes);
            hotfix.Class = packet.ReadInt32("Class", indexes);

            Storage.ItemDisenchantLootHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemEffectHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemEffectHotfix440 hotfix = new ItemEffectHotfix440();

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

            Storage.ItemEffectHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemExtendedCostHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemExtendedCostHotfix440 hotfix = new ItemExtendedCostHotfix440();

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

            Storage.ItemExtendedCostHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemLevelSelectorHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemLevelSelectorHotfix440 hotfix = new ItemLevelSelectorHotfix440();

            hotfix.ID = entry;
            hotfix.MinItemLevel = packet.ReadUInt16("MinItemLevel", indexes);
            hotfix.ItemLevelSelectorQualitySetID = packet.ReadUInt16("ItemLevelSelectorQualitySetID", indexes);

            Storage.ItemLevelSelectorHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemLevelSelectorQualityHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemLevelSelectorQualityHotfix440 hotfix = new ItemLevelSelectorQualityHotfix440();

            hotfix.ID = entry;
            hotfix.QualityItemBonusListID = packet.ReadInt32("QualityItemBonusListID", indexes);
            hotfix.Quality = packet.ReadSByte("Quality", indexes);
            hotfix.ParentILSQualitySetID = packet.ReadInt32("ParentILSQualitySetID", indexes);

            Storage.ItemLevelSelectorQualityHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemLevelSelectorQualitySetHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemLevelSelectorQualitySetHotfix440 hotfix = new ItemLevelSelectorQualitySetHotfix440();

            hotfix.ID = entry;
            hotfix.IlvlRare = packet.ReadInt16("IlvlRare", indexes);
            hotfix.IlvlEpic = packet.ReadInt16("IlvlEpic", indexes);

            Storage.ItemLevelSelectorQualitySetHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemLimitCategoryHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemLimitCategoryHotfix440 hotfix = new ItemLimitCategoryHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Quantity = packet.ReadByte("Quantity", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);

            Storage.ItemLimitCategoryHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemLimitCategoryLocaleHotfix440 hotfixLocale = new ItemLimitCategoryLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ItemLimitCategoryHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemLimitCategoryConditionHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemLimitCategoryConditionHotfix440 hotfix = new ItemLimitCategoryConditionHotfix440();

            hotfix.ID = entry;
            hotfix.AddQuantity = packet.ReadSByte("AddQuantity", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.ParentItemLimitCategoryID = packet.ReadInt32("ParentItemLimitCategoryID", indexes);

            Storage.ItemLimitCategoryConditionHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemModifiedAppearanceHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemModifiedAppearanceHotfix440 hotfix = new ItemModifiedAppearanceHotfix440();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);
            hotfix.ItemAppearanceModifierID = packet.ReadInt32("ItemAppearanceModifierID", indexes);
            hotfix.ItemAppearanceID = packet.ReadInt32("ItemAppearanceID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.TransmogSourceTypeEnum = packet.ReadByte("TransmogSourceTypeEnum", indexes);

            Storage.ItemModifiedAppearanceHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemModifiedAppearanceExtraHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemModifiedAppearanceExtraHotfix440 hotfix = new ItemModifiedAppearanceExtraHotfix440();

            hotfix.ID = entry;
            hotfix.IconFileDataID = packet.ReadInt32("IconFileDataID", indexes);
            hotfix.UnequippedIconFileDataID = packet.ReadInt32("UnequippedIconFileDataID", indexes);
            hotfix.SheatheType = packet.ReadByte("SheatheType", indexes);
            hotfix.DisplayWeaponSubclassID = packet.ReadSByte("DisplayWeaponSubclassID", indexes);
            hotfix.DisplayInventoryType = packet.ReadSByte("DisplayInventoryType", indexes);

            Storage.ItemModifiedAppearanceExtraHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemNameDescriptionHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemNameDescriptionHotfix440 hotfix = new ItemNameDescriptionHotfix440();

            hotfix.ID = entry;
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.Color = packet.ReadInt32("Color", indexes);

            Storage.ItemNameDescriptionHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemNameDescriptionLocaleHotfix440 hotfixLocale = new ItemNameDescriptionLocaleHotfix440
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                };
                Storage.ItemNameDescriptionHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemPriceBaseHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemPriceBaseHotfix440 hotfix = new ItemPriceBaseHotfix440();

            hotfix.ID = entry;
            hotfix.ItemLevel = packet.ReadUInt16("ItemLevel", indexes);
            hotfix.Armor = packet.ReadSingle("Armor", indexes);
            hotfix.Weapon = packet.ReadSingle("Weapon", indexes);

            Storage.ItemPriceBaseHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemReforgeHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemReforgeHotfix440 hotfix = new ItemReforgeHotfix440();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.SourceStat = packet.ReadUInt16("SourceStat", indexes);
            hotfix.SourceMultiplier = packet.ReadSingle("SourceMultiplier", indexes);
            hotfix.TargetStat = packet.ReadUInt16("TargetStat", indexes);
            hotfix.TargetMultiplier = packet.ReadSingle("TargetMultiplier", indexes);
            hotfix.LegacyItemReforgeID = packet.ReadUInt16("LegacyItemReforgeID", indexes);

            Storage.ItemReforgeHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemSearchNameHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemSearchNameHotfix440 hotfix = new ItemSearchNameHotfix440();

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

            Storage.ItemSearchNameHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemSearchNameLocaleHotfix440 hotfixLocale = new ItemSearchNameLocaleHotfix440
                {
                    ID = hotfix.ID,
                    DisplayLang = hotfix.Display,
                };
                Storage.ItemSearchNameHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemSearchNameHandler441(Packet packet, uint entry, params object[] indexes)
        {
            ItemSearchNameHotfix441 hotfix = new ItemSearchNameHotfix441();

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
            hotfix.Flags = new int?[5];
            for (int i = 0; i < 5; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.ItemSearchNameHotfixes441.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemSearchNameLocaleHotfix441 hotfixLocale = new ItemSearchNameLocaleHotfix441
                {
                    ID = hotfix.ID,
                    DisplayLang = hotfix.Display,
                };
                Storage.ItemSearchNameHotfixesLocale441.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemSetHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemSetHotfix440 hotfix = new ItemSetHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.SetFlags = packet.ReadUInt32("SetFlags", indexes);
            hotfix.RequiredSkill = packet.ReadUInt32("RequiredSkill", indexes);
            hotfix.RequiredSkillRank = packet.ReadUInt16("RequiredSkillRank", indexes);
            hotfix.ItemID = new uint?[17];
            for (int i = 0; i < 17; i++)
                hotfix.ItemID[i] = packet.ReadUInt32("ItemID", indexes, i);

            Storage.ItemSetHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemSetLocaleHotfix440 hotfixLocale = new ItemSetLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ItemSetHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemSetSpellHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemSetSpellHotfix440 hotfix = new ItemSetSpellHotfix440();

            hotfix.ID = entry;
            hotfix.ChrSpecID = packet.ReadUInt16("ChrSpecID", indexes);
            hotfix.SpellID = packet.ReadUInt32("SpellID", indexes);
            hotfix.Threshold = packet.ReadByte("Threshold", indexes);
            hotfix.ItemSetID = packet.ReadInt32("ItemSetID", indexes);

            Storage.ItemSetSpellHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ItemSparseHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemSparseHotfix440 hotfix = new ItemSparseHotfix440();

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
            hotfix.Unknown440_1 = new int?[10];
            for (int i = 0; i < 10; i++)
                hotfix.Unknown440_1[i] = packet.ReadInt32("Unknown440_1", indexes, i);
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
            hotfix.OppositeFactionItemID = packet.ReadInt32("OppositeFactionItemID", indexes);
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

            Storage.ItemSparseHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemSparseLocaleHotfix440 hotfixLocale = new ItemSparseLocaleHotfix440
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                    Display3Lang = hotfix.Display3,
                    Display2Lang = hotfix.Display2,
                    Display1Lang = hotfix.Display1,
                    DisplayLang = hotfix.Display,
                };
                Storage.ItemSparseHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemSparseHandler441(Packet packet, uint entry, params object[] indexes)
        {
            ItemSparseHotfix441 hotfix = new ItemSparseHotfix441();

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
            hotfix.Unknown1153 = new int?[10];
            for (int i = 0; i < 10; i++)
                hotfix.Unknown1153[i] = packet.ReadInt32("Unknown1153", indexes, i);
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
            hotfix.OppositeFactionItemID = packet.ReadInt32("OppositeFactionItemID", indexes);
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
            hotfix.ContainerSlots = packet.ReadByte("ContainerSlots", indexes);
            hotfix.RequiredPVPMedal = packet.ReadByte("RequiredPVPMedal", indexes);
            hotfix.RequiredPVPRank = packet.ReadByte("RequiredPVPRank", indexes);
            hotfix.InventoryType = packet.ReadSByte("InventoryType", indexes);
            hotfix.OverallQualityID = packet.ReadSByte("OverallQualityID", indexes);
            hotfix.AmmunitionType = packet.ReadByte("AmmunitionType", indexes);
            hotfix.RequiredLevel = packet.ReadSByte("RequiredLevel", indexes);

            Storage.ItemSparseHotfixes441.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ItemSparseLocaleHotfix441 hotfixLocale = new ItemSparseLocaleHotfix441
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                    Display3Lang = hotfix.Display3,
                    Display2Lang = hotfix.Display2,
                    Display1Lang = hotfix.Display1,
                    DisplayLang = hotfix.Display,
                };
                Storage.ItemSparseHotfixesLocale441.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ItemXBonusTreeHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ItemXBonusTreeHotfix440 hotfix = new ItemXBonusTreeHotfix440();

            hotfix.ID = entry;
            hotfix.ItemBonusTreeID = packet.ReadUInt16("ItemBonusTreeID", indexes);
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);

            Storage.ItemXBonusTreeHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void JournalEncounterHandler440(Packet packet, uint entry, params object[] indexes)
        {
            JournalEncounterHotfix440 hotfix = new JournalEncounterHotfix440();

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

            Storage.JournalEncounterHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                JournalEncounterLocaleHotfix440 hotfixLocale = new JournalEncounterLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.JournalEncounterHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void JournalEncounterSectionHandler440(Packet packet, uint entry, params object[] indexes)
        {
            JournalEncounterSectionHotfix440 hotfix = new JournalEncounterSectionHotfix440();

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

            Storage.JournalEncounterSectionHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                JournalEncounterSectionLocaleHotfix440 hotfixLocale = new JournalEncounterSectionLocaleHotfix440
                {
                    ID = hotfix.ID,
                    TitleLang = hotfix.Title,
                    BodyTextLang = hotfix.BodyText,
                };
                Storage.JournalEncounterSectionHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void JournalInstanceHandler440(Packet packet, uint entry, params object[] indexes)
        {
            JournalInstanceHotfix440 hotfix = new JournalInstanceHotfix440();

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

            Storage.JournalInstanceHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                JournalInstanceLocaleHotfix440 hotfixLocale = new JournalInstanceLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.JournalInstanceHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void JournalTierHandler440(Packet packet, uint entry, params object[] indexes)
        {
            JournalTierHotfix440 hotfix = new JournalTierHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);

            Storage.JournalTierHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                JournalTierLocaleHotfix440 hotfixLocale = new JournalTierLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.JournalTierHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void KeychainHandler440(Packet packet, uint entry, params object[] indexes)
        {
            KeychainHotfix440 hotfix = new KeychainHotfix440();

            hotfix.ID = entry;
            hotfix.Key = new byte?[32];
            for (int i = 0; i < 32; i++)
                hotfix.Key[i] = packet.ReadByte("Key", indexes, i);

            Storage.KeychainHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void KeystoneAffixHandler440(Packet packet, uint entry, params object[] indexes)
        {
            KeystoneAffixHotfix440 hotfix = new KeystoneAffixHotfix440();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.FiledataID = packet.ReadInt32("FiledataID", indexes);

            Storage.KeystoneAffixHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                KeystoneAffixLocaleHotfix440 hotfixLocale = new KeystoneAffixLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.KeystoneAffixHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void LanguageWordsHandler440(Packet packet, uint entry, params object[] indexes)
        {
            LanguageWordsHotfix440 hotfix = new LanguageWordsHotfix440();

            hotfix.ID = entry;
            hotfix.Word = packet.ReadCString("Word", indexes);
            hotfix.LanguageID = packet.ReadByte("LanguageID", indexes);

            Storage.LanguageWordsHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void LanguagesHandler440(Packet packet, uint entry, params object[] indexes)
        {
            LanguagesHotfix440 hotfix = new LanguagesHotfix440();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.UiTextureKitID = packet.ReadInt32("UiTextureKitID", indexes);
            hotfix.UiTextureKitElementCount = packet.ReadInt32("UiTextureKitElementCount", indexes);
            hotfix.LearningCurveID = packet.ReadInt32("LearningCurveID", indexes);

            Storage.LanguagesHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                LanguagesLocaleHotfix440 hotfixLocale = new LanguagesLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.LanguagesHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void LfgDungeonsHandler440(Packet packet, uint entry, params object[] indexes)
        {
            LfgDungeonsHotfix440 hotfix = new LfgDungeonsHotfix440();

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

            Storage.LfgDungeonsHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                LfgDungeonsLocaleHotfix440 hotfixLocale = new LfgDungeonsLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.LfgDungeonsHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void LightHandler440(Packet packet, uint entry, params object[] indexes)
        {
            LightHotfix440 hotfix = new LightHotfix440();

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

            Storage.LightHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void LiquidTypeHandler440(Packet packet, uint entry, params object[] indexes)
        {
            LiquidTypeHotfix440 hotfix = new LiquidTypeHotfix440();

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

            Storage.LiquidTypeHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void LocationHandler440(Packet packet, uint entry, params object[] indexes)
        {
            LocationHotfix440 hotfix = new LocationHotfix440();

            hotfix.ID = entry;
            hotfix.PosX = packet.ReadSingle("PosX", indexes);
            hotfix.PosY = packet.ReadSingle("PosY", indexes);
            hotfix.PosZ = packet.ReadSingle("PosZ", indexes);
            hotfix.Rot = new float?[3];
            for (int i = 0; i < 3; i++)
                hotfix.Rot[i] = packet.ReadSingle("Rot", indexes, i);

            Storage.LocationHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void LockHandler440(Packet packet, uint entry, params object[] indexes)
        {
            LockHotfix440 hotfix = new LockHotfix440();

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

            Storage.LockHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void MailTemplateHandler440(Packet packet, uint entry, params object[] indexes)
        {
            MailTemplateHotfix440 hotfix = new MailTemplateHotfix440();

            hotfix.ID = entry;
            hotfix.Body = packet.ReadCString("Body", indexes);

            Storage.MailTemplateHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                MailTemplateLocaleHotfix440 hotfixLocale = new MailTemplateLocaleHotfix440
                {
                    ID = hotfix.ID,
                    BodyLang = hotfix.Body,
                };
                Storage.MailTemplateHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void MapHandler440(Packet packet, uint entry, params object[] indexes)
        {
            MapHotfix440 hotfix = new MapHotfix440();

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

            Storage.MapHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                MapLocaleHotfix440 hotfixLocale = new MapLocaleHotfix440
                {
                    ID = hotfix.ID,
                    MapNameLang = hotfix.MapName,
                    MapDescription0Lang = hotfix.MapDescription0,
                    MapDescription1Lang = hotfix.MapDescription1,
                    PvpShortDescriptionLang = hotfix.PvpShortDescription,
                    PvpLongDescriptionLang = hotfix.PvpLongDescription,
                };
                Storage.MapHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void MapHandler441(Packet packet, uint entry, params object[] indexes)
        {
            MapHotfix441 hotfix = new MapHotfix441();

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
            hotfix.PreloadFileDataID = packet.ReadInt32("PreloadFileDataID", indexes);
            hotfix.Flags = new int?[3];
            for (int i = 0; i < 3; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.MapHotfixes441.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                MapLocaleHotfix441 hotfixLocale = new MapLocaleHotfix441
                {
                    ID = hotfix.ID,
                    MapNameLang = hotfix.MapName,
                    MapDescription0Lang = hotfix.MapDescription0,
                    MapDescription1Lang = hotfix.MapDescription1,
                    PvpShortDescriptionLang = hotfix.PvpShortDescription,
                    PvpLongDescriptionLang = hotfix.PvpLongDescription,
                };
                Storage.MapHotfixesLocale441.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void MapChallengeModeHandler440(Packet packet, uint entry, params object[] indexes)
        {
            MapChallengeModeHotfix440 hotfix = new MapChallengeModeHotfix440();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.MapID = packet.ReadUInt16("MapID", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.ExpansionLevel = packet.ReadUInt32("ExpansionLevel", indexes);
            hotfix.RequiredWorldStateID = packet.ReadInt32("RequiredWorldStateID", indexes);
            hotfix.CriteriaCount = new short?[3];
            for (int i = 0; i < 3; i++)
                hotfix.CriteriaCount[i] = packet.ReadInt16("CriteriaCount", indexes, i);

            Storage.MapChallengeModeHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                MapChallengeModeLocaleHotfix440 hotfixLocale = new MapChallengeModeLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.MapChallengeModeHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void MapDifficultyHandler440(Packet packet, uint entry, params object[] indexes)
        {
            MapDifficultyHotfix440 hotfix = new MapDifficultyHotfix440();

            hotfix.ID = entry;
            hotfix.Message = packet.ReadCString("Message", indexes);
            hotfix.ItemContextPickerID = packet.ReadUInt32("ItemContextPickerID", indexes);
            hotfix.ContentTuningID = packet.ReadInt32("ContentTuningID", indexes);
            hotfix.ItemContext = packet.ReadInt32("ItemContext", indexes);
            hotfix.DifficultyID = packet.ReadByte("DifficultyID", indexes);
            hotfix.LockID = packet.ReadByte("LockID", indexes);
            hotfix.ResetInterval = packet.ReadByte("ResetInterval", indexes);
            hotfix.MaxPlayers = packet.ReadByte("MaxPlayers", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.MapID = packet.ReadInt32("MapID", indexes);

            Storage.MapDifficultyHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                MapDifficultyLocaleHotfix440 hotfixLocale = new MapDifficultyLocaleHotfix440
                {
                    ID = hotfix.ID,
                    MessageLang = hotfix.Message,
                };
                Storage.MapDifficultyHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void MapDifficultyXConditionHandler440(Packet packet, uint entry, params object[] indexes)
        {
            MapDifficultyXConditionHotfix440 hotfix = new MapDifficultyXConditionHotfix440();

            hotfix.ID = entry;
            hotfix.FailureDescription = packet.ReadCString("FailureDescription", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.MapDifficultyID = packet.ReadInt32("MapDifficultyID", indexes);

            Storage.MapDifficultyXConditionHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                MapDifficultyXConditionLocaleHotfix440 hotfixLocale = new MapDifficultyXConditionLocaleHotfix440
                {
                    ID = hotfix.ID,
                    FailureDescriptionLang = hotfix.FailureDescription,
                };
                Storage.MapDifficultyXConditionHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ModifierTreeHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ModifierTreeHotfix440 hotfix = new ModifierTreeHotfix440();

            hotfix.ID = entry;
            hotfix.Parent = packet.ReadUInt32("Parent", indexes);
            hotfix.Operator = packet.ReadSByte("Operator", indexes);
            hotfix.Amount = packet.ReadSByte("Amount", indexes);
            hotfix.Type = packet.ReadInt32("Type", indexes);
            hotfix.Asset = packet.ReadInt32("Asset", indexes);
            hotfix.SecondaryAsset = packet.ReadInt32("SecondaryAsset", indexes);
            hotfix.TertiaryAsset = packet.ReadSByte("TertiaryAsset", indexes);

            Storage.ModifierTreeHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void MountHandler440(Packet packet, uint entry, params object[] indexes)
        {
            MountHotfix440 hotfix = new MountHotfix440();

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

            Storage.MountHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                MountLocaleHotfix440 hotfixLocale = new MountLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    SourceTextLang = hotfix.SourceText,
                    DescriptionLang = hotfix.Description,
                };
                Storage.MountHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void MountCapabilityHandler440(Packet packet, uint entry, params object[] indexes)
        {
            MountCapabilityHotfix440 hotfix = new MountCapabilityHotfix440();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ReqRidingSkill = packet.ReadUInt16("ReqRidingSkill", indexes);
            hotfix.ReqAreaID = packet.ReadUInt16("ReqAreaID", indexes);
            hotfix.ReqSpellAuraID = packet.ReadUInt32("ReqSpellAuraID", indexes);
            hotfix.ReqSpellKnownID = packet.ReadInt32("ReqSpellKnownID", indexes);
            hotfix.ModSpellAuraID = packet.ReadInt32("ModSpellAuraID", indexes);
            hotfix.ReqMapID = packet.ReadInt16("ReqMapID", indexes);

            Storage.MountCapabilityHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void MountCapabilityHandler441(Packet packet, uint entry, params object[] indexes)
        {
            MountCapabilityHotfix441 hotfix = new MountCapabilityHotfix441();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.ReqRidingSkill = packet.ReadUInt16("ReqRidingSkill", indexes);
            hotfix.ReqAreaID = packet.ReadUInt16("ReqAreaID", indexes);
            hotfix.ReqSpellAuraID = packet.ReadUInt32("ReqSpellAuraID", indexes);
            hotfix.ReqSpellKnownID = packet.ReadInt32("ReqSpellKnownID", indexes);
            hotfix.ModSpellAuraID = packet.ReadInt32("ModSpellAuraID", indexes);
            hotfix.ReqMapID = packet.ReadInt16("ReqMapID", indexes);
            hotfix.PlayerConditionID = packet.ReadInt32("PlayerConditionID", indexes);
            hotfix.FlightCapabilityID = packet.ReadInt32("FlightCapabilityID", indexes);

            Storage.MountCapabilityHotfixes441.Add(hotfix, packet.TimeSpan);
        }

        public static void MountTypeXCapabilityHandler440(Packet packet, uint entry, params object[] indexes)
        {
            MountTypeXCapabilityHotfix440 hotfix = new MountTypeXCapabilityHotfix440();

            hotfix.ID = entry;
            hotfix.MountTypeID = packet.ReadUInt16("MountTypeID", indexes);
            hotfix.MountCapabilityID = packet.ReadUInt16("MountCapabilityID", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);

            Storage.MountTypeXCapabilityHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void MountXDisplayHandler440(Packet packet, uint entry, params object[] indexes)
        {
            MountXDisplayHotfix440 hotfix = new MountXDisplayHotfix440();

            hotfix.ID = entry;
            hotfix.CreatureDisplayInfoID = packet.ReadInt32("CreatureDisplayInfoID", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.MountID = packet.ReadInt32("MountID", indexes);

            Storage.MountXDisplayHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void MountXDisplayHandler441(Packet packet, uint entry, params object[] indexes)
        {
            MountXDisplayHotfix441 hotfix = new MountXDisplayHotfix441();

            hotfix.ID = entry;
            hotfix.CreatureDisplayInfoID = packet.ReadInt32("CreatureDisplayInfoID", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.Unknown1100 = packet.ReadUInt16("Unknown1100", indexes);
            hotfix.MountID = packet.ReadInt32("MountID", indexes);

            Storage.MountXDisplayHotfixes441.Add(hotfix, packet.TimeSpan);
        }

        public static void MovieHandler440(Packet packet, uint entry, params object[] indexes)
        {
            MovieHotfix440 hotfix = new MovieHotfix440();

            hotfix.ID = entry;
            hotfix.Volume = packet.ReadByte("Volume", indexes);
            hotfix.KeyID = packet.ReadByte("KeyID", indexes);
            hotfix.AudioFileDataID = packet.ReadUInt32("AudioFileDataID", indexes);
            hotfix.SubtitleFileDataID = packet.ReadUInt32("SubtitleFileDataID", indexes);

            Storage.MovieHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void MythicPlusSeasonHandler440(Packet packet, uint entry, params object[] indexes)
        {
            MythicPlusSeasonHotfix440 hotfix = new MythicPlusSeasonHotfix440();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.MilestoneSeason = packet.ReadInt32("MilestoneSeason", indexes);
            hotfix.ExpansionLevel = packet.ReadInt32("ExpansionLevel", indexes);
            hotfix.HeroicLFGDungeonMinGear = packet.ReadInt32("HeroicLFGDungeonMinGear", indexes);

            Storage.MythicPlusSeasonHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void MythicPlusSeasonHandler441(Packet packet, uint entry, params object[] indexes)
        {
            MythicPlusSeasonHotfix441 hotfix = new MythicPlusSeasonHotfix441();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.MilestoneSeason = packet.ReadInt32("MilestoneSeason", indexes);
            hotfix.StartTimeEvent = packet.ReadInt32("StartTimeEvent", indexes);
            hotfix.ExpansionLevel = packet.ReadInt32("ExpansionLevel", indexes);
            hotfix.HeroicLFGDungeonMinGear = packet.ReadInt32("HeroicLFGDungeonMinGear", indexes);

            Storage.MythicPlusSeasonHotfixes441.Add(hotfix, packet.TimeSpan);
        }

        public static void NameGenHandler440(Packet packet, uint entry, params object[] indexes)
        {
            NameGenHotfix440 hotfix = new NameGenHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.RaceID = packet.ReadByte("RaceID", indexes);
            hotfix.Sex = packet.ReadByte("Sex", indexes);

            Storage.NameGenHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void NamesProfanityHandler440(Packet packet, uint entry, params object[] indexes)
        {
            NamesProfanityHotfix440 hotfix = new NamesProfanityHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Language = packet.ReadSByte("Language", indexes);

            Storage.NamesProfanityHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void NamesReservedHandler440(Packet packet, uint entry, params object[] indexes)
        {
            NamesReservedHotfix440 hotfix = new NamesReservedHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);

            Storage.NamesReservedHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void NamesReservedLocaleHandler440(Packet packet, uint entry, params object[] indexes)
        {
            NamesReservedLocaleHotfix440 hotfix = new NamesReservedLocaleHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.LocaleMask = packet.ReadByte("LocaleMask", indexes);

            Storage.NamesReservedLocaleHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void NumTalentsAtLevelHandler440(Packet packet, uint entry, params object[] indexes)
        {
            NumTalentsAtLevelHotfix440 hotfix = new NumTalentsAtLevelHotfix440();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.NumTalents = packet.ReadInt32("NumTalents", indexes);
            hotfix.NumTalentsDeathKnight = packet.ReadInt32("NumTalentsDeathKnight", indexes);
            hotfix.NumTalentsDemonHunter = packet.ReadInt32("NumTalentsDemonHunter", indexes);
            hotfix.NumberOfTalents = packet.ReadSingle("NumberOfTalents", indexes);

            Storage.NumTalentsAtLevelHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void OverrideSpellDataHandler440(Packet packet, uint entry, params object[] indexes)
        {
            OverrideSpellDataHotfix440 hotfix = new OverrideSpellDataHotfix440();

            hotfix.ID = entry;
            hotfix.Spells = new int?[10];
            for (int i = 0; i < 10; i++)
                hotfix.Spells[i] = packet.ReadInt32("Spells", indexes, i);
            hotfix.PlayerActionbarFileDataID = packet.ReadInt32("PlayerActionbarFileDataID", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);

            Storage.OverrideSpellDataHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ParagonReputationHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ParagonReputationHotfix440 hotfix = new ParagonReputationHotfix440();

            hotfix.ID = entry;
            hotfix.FactionID = packet.ReadInt32("FactionID", indexes);
            hotfix.LevelThreshold = packet.ReadInt32("LevelThreshold", indexes);
            hotfix.QuestID = packet.ReadInt32("QuestID", indexes);

            Storage.ParagonReputationHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void PathHandler440(Packet packet, uint entry, params object[] indexes)
        {
            PathHotfix440 hotfix = new PathHotfix440();

            hotfix.ID = entry;
            hotfix.Type = packet.ReadByte("Type", indexes);
            hotfix.SplineType = packet.ReadByte("SplineType", indexes);
            hotfix.Red = packet.ReadByte("Red", indexes);
            hotfix.Green = packet.ReadByte("Green", indexes);
            hotfix.Blue = packet.ReadByte("Blue", indexes);
            hotfix.Alpha = packet.ReadByte("Alpha", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);

            Storage.PathHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void PathNodeHandler440(Packet packet, uint entry, params object[] indexes)
        {
            PathNodeHotfix440 hotfix = new PathNodeHotfix440();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.PathID = packet.ReadUInt16("PathID", indexes);
            hotfix.Sequence = packet.ReadInt16("Sequence", indexes);
            hotfix.LocationID = packet.ReadInt32("LocationID", indexes);

            Storage.PathNodeHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void PathPropertyHandler440(Packet packet, uint entry, params object[] indexes)
        {
            PathPropertyHotfix440 hotfix = new PathPropertyHotfix440();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.PathID = packet.ReadUInt16("PathID", indexes);
            hotfix.PropertyIndex = packet.ReadByte("PropertyIndex", indexes);
            hotfix.Value = packet.ReadInt32("Value", indexes);

            Storage.PathPropertyHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void PhaseHandler440(Packet packet, uint entry, params object[] indexes)
        {
            PhaseHotfix440 hotfix = new PhaseHotfix440();

            hotfix.ID = entry;
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.PhaseHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void PhaseXPhaseGroupHandler440(Packet packet, uint entry, params object[] indexes)
        {
            PhaseXPhaseGroupHotfix440 hotfix = new PhaseXPhaseGroupHotfix440();

            hotfix.ID = entry;
            hotfix.PhaseID = packet.ReadUInt16("PhaseID", indexes);
            hotfix.PhaseGroupID = packet.ReadInt32("PhaseGroupID", indexes);

            Storage.PhaseXPhaseGroupHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void PlayerConditionHandler440(Packet packet, uint entry, params object[] indexes)
        {
            PlayerConditionHotfix440 hotfix = new PlayerConditionHotfix440();

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

            Storage.PlayerConditionHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                PlayerConditionLocaleHotfix440 hotfixLocale = new PlayerConditionLocaleHotfix440
                {
                    ID = hotfix.ID,
                    FailureDescriptionLang = hotfix.FailureDescription,
                };
                Storage.PlayerConditionHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void PlayerConditionHandler441(Packet packet, uint entry, params object[] indexes)
        {
            PlayerConditionHotfix441 hotfix = new PlayerConditionHotfix441();

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
            hotfix.PhaseUseFlags = packet.ReadInt32("PhaseUseFlags", indexes);
            hotfix.PhaseID = packet.ReadUInt16("PhaseID", indexes);
            hotfix.PhaseGroupID = packet.ReadUInt32("PhaseGroupID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
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

            Storage.PlayerConditionHotfixes441.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                PlayerConditionLocaleHotfix441 hotfixLocale = new PlayerConditionLocaleHotfix441
                {
                    ID = hotfix.ID,
                    FailureDescriptionLang = hotfix.FailureDescription,
                };
                Storage.PlayerConditionHotfixesLocale441.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void PowerDisplayHandler440(Packet packet, uint entry, params object[] indexes)
        {
            PowerDisplayHotfix440 hotfix = new PowerDisplayHotfix440();

            hotfix.ID = entry;
            hotfix.GlobalStringBaseTag = packet.ReadCString("GlobalStringBaseTag", indexes);
            hotfix.ActualType = packet.ReadByte("ActualType", indexes);
            hotfix.Red = packet.ReadByte("Red", indexes);
            hotfix.Green = packet.ReadByte("Green", indexes);
            hotfix.Blue = packet.ReadByte("Blue", indexes);

            Storage.PowerDisplayHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void PowerDisplayHandler441(Packet packet, uint entry, params object[] indexes)
        {
            PowerDisplayHotfix441 hotfix = new PowerDisplayHotfix441();

            hotfix.ID = entry;
            hotfix.GlobalStringBaseTag = packet.ReadCString("GlobalStringBaseTag", indexes);
            hotfix.ActualType = packet.ReadSByte("ActualType", indexes);
            hotfix.Red = packet.ReadByte("Red", indexes);
            hotfix.Green = packet.ReadByte("Green", indexes);
            hotfix.Blue = packet.ReadByte("Blue", indexes);

            Storage.PowerDisplayHotfixes441.Add(hotfix, packet.TimeSpan);
        }

        public static void PowerTypeHandler440(Packet packet, uint entry, params object[] indexes)
        {
            PowerTypeHotfix440 hotfix = new PowerTypeHotfix440();

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

            Storage.PowerTypeHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void PowerTypeHandler441(Packet packet, uint entry, params object[] indexes)
        {
            PowerTypeHotfix441 hotfix = new PowerTypeHotfix441();

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
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.PowerTypeHotfixes441.Add(hotfix, packet.TimeSpan);
        }

        public static void PrestigeLevelInfoHandler440(Packet packet, uint entry, params object[] indexes)
        {
            PrestigeLevelInfoHotfix440 hotfix = new PrestigeLevelInfoHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.HonorLevel = packet.ReadInt32("HonorLevel", indexes);
            hotfix.BadgeTextureFileDataID = packet.ReadInt32("BadgeTextureFileDataID", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.AwardedAchievementID = packet.ReadInt32("AwardedAchievementID", indexes);

            Storage.PrestigeLevelInfoHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                PrestigeLevelInfoLocaleHotfix440 hotfixLocale = new PrestigeLevelInfoLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.PrestigeLevelInfoHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void PvpDifficultyHandler440(Packet packet, uint entry, params object[] indexes)
        {
            PvpDifficultyHotfix440 hotfix = new PvpDifficultyHotfix440();

            hotfix.ID = entry;
            hotfix.RangeIndex = packet.ReadByte("RangeIndex", indexes);
            hotfix.MinLevel = packet.ReadByte("MinLevel", indexes);
            hotfix.MaxLevel = packet.ReadByte("MaxLevel", indexes);
            hotfix.MapID = packet.ReadInt32("MapID", indexes);

            Storage.PvpDifficultyHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void PvpItemHandler440(Packet packet, uint entry, params object[] indexes)
        {
            PvpItemHotfix440 hotfix = new PvpItemHotfix440();

            hotfix.ID = entry;
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);
            hotfix.ItemLevelDelta = packet.ReadByte("ItemLevelDelta", indexes);

            Storage.PvpItemHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void PvpSeasonHandler440(Packet packet, uint entry, params object[] indexes)
        {
            PvpSeasonHotfix440 hotfix = new PvpSeasonHotfix440();

            hotfix.ID = entry;
            hotfix.MilestoneSeason = packet.ReadInt32("MilestoneSeason", indexes);
            hotfix.AllianceAchievementID = packet.ReadInt32("AllianceAchievementID", indexes);
            hotfix.HordeAchievementID = packet.ReadInt32("HordeAchievementID", indexes);

            Storage.PvpSeasonHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void PvpTierHandler440(Packet packet, uint entry, params object[] indexes)
        {
            PvpTierHotfix440 hotfix = new PvpTierHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.MinRating = packet.ReadInt16("MinRating", indexes);
            hotfix.MaxRating = packet.ReadInt16("MaxRating", indexes);
            hotfix.PrevTier = packet.ReadInt32("PrevTier", indexes);
            hotfix.NextTier = packet.ReadInt32("NextTier", indexes);
            hotfix.BracketID = packet.ReadSByte("BracketID", indexes);
            hotfix.Rank = packet.ReadSByte("Rank", indexes);
            hotfix.RankIconFileDataID = packet.ReadInt32("RankIconFileDataID", indexes);

            Storage.PvpTierHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                PvpTierLocaleHotfix440 hotfixLocale = new PvpTierLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.PvpTierHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void QuestFactionRewardHandler440(Packet packet, uint entry, params object[] indexes)
        {
            QuestFactionRewardHotfix440 hotfix = new QuestFactionRewardHotfix440();

            hotfix.ID = entry;
            hotfix.Difficulty = new short?[10];
            for (int i = 0; i < 10; i++)
                hotfix.Difficulty[i] = packet.ReadInt16("Difficulty", indexes, i);

            Storage.QuestFactionRewardHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void QuestInfoHandler440(Packet packet, uint entry, params object[] indexes)
        {
            QuestInfoHotfix440 hotfix = new QuestInfoHotfix440();

            hotfix.ID = entry;
            hotfix.InfoName = packet.ReadCString("InfoName", indexes);
            hotfix.Type = packet.ReadSByte("Type", indexes);
            hotfix.Modifiers = packet.ReadInt32("Modifiers", indexes);
            hotfix.Profession = packet.ReadUInt16("Profession", indexes);

            Storage.QuestInfoHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                QuestInfoLocaleHotfix440 hotfixLocale = new QuestInfoLocaleHotfix440
                {
                    ID = hotfix.ID,
                    InfoNameLang = hotfix.InfoName,
                };
                Storage.QuestInfoHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void QuestLineXQuestHandler440(Packet packet, uint entry, params object[] indexes)
        {
            QuestLineXQuestHotfix440 hotfix = new QuestLineXQuestHotfix440();

            hotfix.ID = entry;
            hotfix.QuestLineID = packet.ReadUInt32("QuestLineID", indexes);
            hotfix.QuestID = packet.ReadUInt32("QuestID", indexes);
            hotfix.OrderIndex = packet.ReadUInt32("OrderIndex", indexes);

            Storage.QuestLineXQuestHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void QuestMoneyRewardHandler440(Packet packet, uint entry, params object[] indexes)
        {
            QuestMoneyRewardHotfix440 hotfix = new QuestMoneyRewardHotfix440();

            hotfix.ID = entry;
            hotfix.Difficulty = new uint?[10];
            for (int i = 0; i < 10; i++)
                hotfix.Difficulty[i] = packet.ReadUInt32("Difficulty", indexes, i);

            Storage.QuestMoneyRewardHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void QuestPackageItemHandler440(Packet packet, uint entry, params object[] indexes)
        {
            QuestPackageItemHotfix440 hotfix = new QuestPackageItemHotfix440();

            hotfix.ID = entry;
            hotfix.PackageID = packet.ReadUInt16("PackageID", indexes);
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);
            hotfix.ItemQuantity = packet.ReadUInt32("ItemQuantity", indexes);
            hotfix.DisplayType = packet.ReadByte("DisplayType", indexes);

            Storage.QuestPackageItemHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void QuestSortHandler440(Packet packet, uint entry, params object[] indexes)
        {
            QuestSortHotfix440 hotfix = new QuestSortHotfix440();

            hotfix.ID = entry;
            hotfix.SortName = packet.ReadCString("SortName", indexes);
            hotfix.UiOrderIndex = packet.ReadSByte("UiOrderIndex", indexes);

            Storage.QuestSortHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                QuestSortLocaleHotfix440 hotfixLocale = new QuestSortLocaleHotfix440
                {
                    ID = hotfix.ID,
                    SortNameLang = hotfix.SortName,
                };
                Storage.QuestSortHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void QuestV2Handler440(Packet packet, uint entry, params object[] indexes)
        {
            QuestV2Hotfix440 hotfix = new QuestV2Hotfix440();

            hotfix.ID = entry;
            hotfix.UniqueBitFlag = packet.ReadUInt16("UniqueBitFlag", indexes);

            Storage.QuestV2Hotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void QuestXpHandler440(Packet packet, uint entry, params object[] indexes)
        {
            QuestXpHotfix440 hotfix = new QuestXpHotfix440();

            hotfix.ID = entry;
            hotfix.Difficulty = new int?[10];
            for (int i = 0; i < 10; i++)
                hotfix.Difficulty[i] = packet.ReadInt32("Difficulty", indexes, i);

            Storage.QuestXpHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void RandPropPointsHandler440(Packet packet, uint entry, params object[] indexes)
        {
            RandPropPointsHotfix440 hotfix = new RandPropPointsHotfix440();

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

            Storage.RandPropPointsHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void RewardPackHandler440(Packet packet, uint entry, params object[] indexes)
        {
            RewardPackHotfix440 hotfix = new RewardPackHotfix440();

            hotfix.ID = entry;
            hotfix.CharTitleID = packet.ReadInt32("CharTitleID", indexes);
            hotfix.Money = packet.ReadUInt32("Money", indexes);
            hotfix.ArtifactXPDifficulty = packet.ReadSByte("ArtifactXPDifficulty", indexes);
            hotfix.ArtifactXPMultiplier = packet.ReadSingle("ArtifactXPMultiplier", indexes);
            hotfix.ArtifactXPCategoryID = packet.ReadByte("ArtifactXPCategoryID", indexes);
            hotfix.TreasurePickerID = packet.ReadUInt32("TreasurePickerID", indexes);

            Storage.RewardPackHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void RewardPackXCurrencyTypeHandler440(Packet packet, uint entry, params object[] indexes)
        {
            RewardPackXCurrencyTypeHotfix440 hotfix = new RewardPackXCurrencyTypeHotfix440();

            hotfix.ID = entry;
            hotfix.CurrencyTypeID = packet.ReadUInt32("CurrencyTypeID", indexes);
            hotfix.Quantity = packet.ReadInt32("Quantity", indexes);
            hotfix.RewardPackID = packet.ReadInt32("RewardPackID", indexes);

            Storage.RewardPackXCurrencyTypeHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void RewardPackXItemHandler440(Packet packet, uint entry, params object[] indexes)
        {
            RewardPackXItemHotfix440 hotfix = new RewardPackXItemHotfix440();

            hotfix.ID = entry;
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);
            hotfix.ItemQuantity = packet.ReadInt32("ItemQuantity", indexes);
            hotfix.RewardPackID = packet.ReadInt32("RewardPackID", indexes);

            Storage.RewardPackXItemHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ScenarioHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ScenarioHotfix440 hotfix = new ScenarioHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.AreaTableID = packet.ReadUInt16("AreaTableID", indexes);
            hotfix.Type = packet.ReadByte("Type", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.UiTextureKitID = packet.ReadUInt32("UiTextureKitID", indexes);

            Storage.ScenarioHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ScenarioLocaleHotfix440 hotfixLocale = new ScenarioLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.ScenarioHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ScenarioStepHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ScenarioStepHotfix440 hotfix = new ScenarioStepHotfix440();

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

            Storage.ScenarioStepHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ScenarioStepLocaleHotfix440 hotfixLocale = new ScenarioStepLocaleHotfix440
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                    TitleLang = hotfix.Title,
                };
                Storage.ScenarioStepHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SceneScriptHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SceneScriptHotfix440 hotfix = new SceneScriptHotfix440();

            hotfix.ID = entry;
            hotfix.FirstSceneScriptID = packet.ReadUInt16("FirstSceneScriptID", indexes);
            hotfix.NextSceneScriptID = packet.ReadUInt16("NextSceneScriptID", indexes);
            hotfix.Unknown915 = packet.ReadInt32("Unknown915", indexes);

            Storage.SceneScriptHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SceneScriptGlobalTextHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SceneScriptGlobalTextHotfix440 hotfix = new SceneScriptGlobalTextHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Script = packet.ReadCString("Script", indexes);

            Storage.SceneScriptGlobalTextHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SceneScriptPackageHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SceneScriptPackageHotfix440 hotfix = new SceneScriptPackageHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);

            Storage.SceneScriptPackageHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SceneScriptTextHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SceneScriptTextHotfix440 hotfix = new SceneScriptTextHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Script = packet.ReadCString("Script", indexes);

            Storage.SceneScriptTextHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void ServerMessagesHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ServerMessagesHotfix440 hotfix = new ServerMessagesHotfix440();

            hotfix.ID = entry;
            hotfix.Text = packet.ReadCString("Text", indexes);

            Storage.ServerMessagesHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ServerMessagesLocaleHotfix440 hotfixLocale = new ServerMessagesLocaleHotfix440
                {
                    ID = hotfix.ID,
                    TextLang = hotfix.Text,
                };
                Storage.ServerMessagesHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SkillLineHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SkillLineHotfix440 hotfix = new SkillLineHotfix440();

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

            Storage.SkillLineHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SkillLineLocaleHotfix440 hotfixLocale = new SkillLineLocaleHotfix440
                {
                    ID = hotfix.ID,
                    DisplayNameLang = hotfix.DisplayName,
                    AlternateVerbLang = hotfix.AlternateVerb,
                    DescriptionLang = hotfix.Description,
                    HordeDisplayNameLang = hotfix.HordeDisplayName,
                };
                Storage.SkillLineHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SkillLineHandler441(Packet packet, uint entry, params object[] indexes)
        {
            SkillLineHotfix441 hotfix = new SkillLineHotfix441();

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

            Storage.SkillLineHotfixes441.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SkillLineLocaleHotfix441 hotfixLocale = new SkillLineLocaleHotfix441
                {
                    ID = hotfix.ID,
                    DisplayNameLang = hotfix.DisplayName,
                    AlternateVerbLang = hotfix.AlternateVerb,
                    DescriptionLang = hotfix.Description,
                    HordeDisplayNameLang = hotfix.HordeDisplayName,
                };
                Storage.SkillLineHotfixesLocale441.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SkillLineAbilityHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SkillLineAbilityHotfix440 hotfix = new SkillLineAbilityHotfix440();

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

            Storage.SkillLineAbilityHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SkillLineAbilityLocaleHotfix440 hotfixLocale = new SkillLineAbilityLocaleHotfix440
                {
                    ID = hotfix.ID,
                };
                Storage.SkillLineAbilityHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SkillLineAbilityHandler441(Packet packet, uint entry, params object[] indexes)
        {
            SkillLineAbilityHotfix441 hotfix = new SkillLineAbilityHotfix441();

            hotfix.RaceMask = packet.ReadInt64("RaceMask", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.SkillLine = packet.ReadInt16("SkillLine", indexes);
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
            hotfix.CharacterPoints = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.CharacterPoints[i] = packet.ReadInt32("CharacterPoints", indexes, i);

            Storage.SkillLineAbilityHotfixes441.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SkillLineAbilityLocaleHotfix441 hotfixLocale = new SkillLineAbilityLocaleHotfix441
                {
                    ID = hotfix.ID,
                };
                Storage.SkillLineAbilityHotfixesLocale441.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SkillRaceClassInfoHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SkillRaceClassInfoHotfix440 hotfix = new SkillRaceClassInfoHotfix440();

            hotfix.ID = entry;
            hotfix.RaceMask = packet.ReadInt64("RaceMask", indexes);
            hotfix.SkillID = packet.ReadInt16("SkillID", indexes);
            hotfix.ClassMask = packet.ReadInt32("ClassMask", indexes);
            hotfix.Flags = packet.ReadUInt16("Flags", indexes);
            hotfix.Availability = packet.ReadSByte("Availability", indexes);
            hotfix.MinLevel = packet.ReadSByte("MinLevel", indexes);
            hotfix.SkillTierID = packet.ReadInt16("SkillTierID", indexes);
            hotfix.Unknown1150 = packet.ReadInt32("Unknown1150", indexes);

            Storage.SkillRaceClassInfoHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SkillRaceClassInfoHandler441(Packet packet, uint entry, params object[] indexes)
        {
            SkillRaceClassInfoHotfix441 hotfix = new SkillRaceClassInfoHotfix441();

            hotfix.ID = entry;
            hotfix.RaceMask = packet.ReadInt64("RaceMask", indexes);
            hotfix.SkillID = packet.ReadInt16("SkillID", indexes);
            hotfix.ClassMask = packet.ReadInt32("ClassMask", indexes);
            hotfix.Flags = packet.ReadUInt16("Flags", indexes);
            hotfix.Availability = packet.ReadInt32("Availability", indexes);
            hotfix.MinLevel = packet.ReadSByte("MinLevel", indexes);
            hotfix.SkillTierID = packet.ReadInt16("SkillTierID", indexes);
            hotfix.Unknown1150 = packet.ReadInt32("Unknown1150", indexes);

            Storage.SkillRaceClassInfoHotfixes441.Add(hotfix, packet.TimeSpan);
        }

        public static void SoundKitHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SoundKitHotfix440 hotfix = new SoundKitHotfix440();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.SoundType = packet.ReadByte("SoundType", indexes);
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

            Storage.SoundKitHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellAuraOptionsHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellAuraOptionsHotfix440 hotfix = new SpellAuraOptionsHotfix440();

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

            Storage.SpellAuraOptionsHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellAuraRestrictionsHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellAuraRestrictionsHotfix440 hotfix = new SpellAuraRestrictionsHotfix440();

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

            Storage.SpellAuraRestrictionsHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellCastTimesHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellCastTimesHotfix440 hotfix = new SpellCastTimesHotfix440();

            hotfix.ID = entry;
            hotfix.Base = packet.ReadInt32("Base", indexes);
            hotfix.PerLevel = packet.ReadInt16("PerLevel", indexes);
            hotfix.Minimum = packet.ReadInt32("Minimum", indexes);

            Storage.SpellCastTimesHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellCastingRequirementsHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellCastingRequirementsHotfix440 hotfix = new SpellCastingRequirementsHotfix440();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.FacingCasterFlags = packet.ReadByte("FacingCasterFlags", indexes);
            hotfix.MinFactionID = packet.ReadUInt16("MinFactionID", indexes);
            hotfix.MinReputation = packet.ReadInt32("MinReputation", indexes);
            hotfix.RequiredAreasID = packet.ReadUInt16("RequiredAreasID", indexes);
            hotfix.RequiredAuraVision = packet.ReadByte("RequiredAuraVision", indexes);
            hotfix.RequiresSpellFocus = packet.ReadUInt16("RequiresSpellFocus", indexes);

            Storage.SpellCastingRequirementsHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellCategoriesHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellCategoriesHotfix440 hotfix = new SpellCategoriesHotfix440();

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

            Storage.SpellCategoriesHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellCategoryHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellCategoryHotfix440 hotfix = new SpellCategoryHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.UsesPerWeek = packet.ReadByte("UsesPerWeek", indexes);
            hotfix.MaxCharges = packet.ReadSByte("MaxCharges", indexes);
            hotfix.ChargeRecoveryTime = packet.ReadInt32("ChargeRecoveryTime", indexes);
            hotfix.TypeMask = packet.ReadInt32("TypeMask", indexes);

            Storage.SpellCategoryHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SpellCategoryLocaleHotfix440 hotfixLocale = new SpellCategoryLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.SpellCategoryHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SpellClassOptionsHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellClassOptionsHotfix440 hotfix = new SpellClassOptionsHotfix440();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.ModalNextSpell = packet.ReadUInt32("ModalNextSpell", indexes);
            hotfix.SpellClassSet = packet.ReadByte("SpellClassSet", indexes);
            hotfix.SpellClassMask = new int?[4];
            for (int i = 0; i < 4; i++)
                hotfix.SpellClassMask[i] = packet.ReadInt32("SpellClassMask", indexes, i);

            Storage.SpellClassOptionsHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellCooldownsHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellCooldownsHotfix440 hotfix = new SpellCooldownsHotfix440();

            hotfix.ID = entry;
            hotfix.DifficultyID = packet.ReadByte("DifficultyID", indexes);
            hotfix.CategoryRecoveryTime = packet.ReadInt32("CategoryRecoveryTime", indexes);
            hotfix.RecoveryTime = packet.ReadInt32("RecoveryTime", indexes);
            hotfix.StartRecoveryTime = packet.ReadInt32("StartRecoveryTime", indexes);
            hotfix.AuraSpellID = packet.ReadInt32("AuraSpellID", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);

            Storage.SpellCooldownsHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellDurationHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellDurationHotfix440 hotfix = new SpellDurationHotfix440();

            hotfix.ID = entry;
            hotfix.Duration = packet.ReadInt32("Duration", indexes);
            hotfix.DurationPerLevel = packet.ReadUInt32("DurationPerLevel", indexes);
            hotfix.MaxDuration = packet.ReadInt32("MaxDuration", indexes);

            Storage.SpellDurationHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellEffectHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellEffectHotfix440 hotfix = new SpellEffectHotfix440();

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

            Storage.SpellEffectHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellEquippedItemsHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellEquippedItemsHotfix440 hotfix = new SpellEquippedItemsHotfix440();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.EquippedItemClass = packet.ReadSByte("EquippedItemClass", indexes);
            hotfix.EquippedItemInvTypes = packet.ReadInt32("EquippedItemInvTypes", indexes);
            hotfix.EquippedItemSubclass = packet.ReadInt32("EquippedItemSubclass", indexes);

            Storage.SpellEquippedItemsHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellFocusObjectHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellFocusObjectHotfix440 hotfix = new SpellFocusObjectHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);

            Storage.SpellFocusObjectHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SpellFocusObjectLocaleHotfix440 hotfixLocale = new SpellFocusObjectLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.SpellFocusObjectHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SpellInterruptsHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellInterruptsHotfix440 hotfix = new SpellInterruptsHotfix440();

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

            Storage.SpellInterruptsHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellItemEnchantmentHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellItemEnchantmentHotfix440 hotfix = new SpellItemEnchantmentHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.HordeName = packet.ReadCString("HordeName", indexes);
            hotfix.Charges = packet.ReadInt32("Charges", indexes);
            hotfix.Effect = new int?[3];
            for (int i = 0; i < 3; i++)
                hotfix.Effect[i] = packet.ReadInt32("Effect", indexes, i);
            hotfix.EffectPointsMin = new int?[3];
            for (int i = 0; i < 3; i++)
                hotfix.EffectPointsMin[i] = packet.ReadInt32("EffectPointsMin", indexes, i);
            hotfix.EffectPointsMax = new int?[3];
            for (int i = 0; i < 3; i++)
                hotfix.EffectPointsMax[i] = packet.ReadInt32("EffectPointsMax", indexes, i);
            hotfix.EffectArg = new int?[3];
            for (int i = 0; i < 3; i++)
                hotfix.EffectArg[i] = packet.ReadInt32("EffectArg", indexes, i);
            hotfix.ItemVisual = packet.ReadInt32("ItemVisual", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.EffectScalingPoints = new float?[3];
            for (int i = 0; i < 3; i++)
                hotfix.EffectScalingPoints[i] = packet.ReadSingle("EffectScalingPoints", indexes, i);
            hotfix.ScalingClass = packet.ReadInt32("ScalingClass", indexes);
            hotfix.ScalingClassRestricted = packet.ReadInt32("ScalingClassRestricted", indexes);
            hotfix.GemItemID = packet.ReadInt32("GemItemID", indexes);
            hotfix.ConditionID = packet.ReadInt32("ConditionID", indexes);
            hotfix.RequiredSkillID = packet.ReadInt32("RequiredSkillID", indexes);
            hotfix.RequiredSkillRank = packet.ReadInt32("RequiredSkillRank", indexes);
            hotfix.MinLevel = packet.ReadInt32("MinLevel", indexes);
            hotfix.MaxLevel = packet.ReadInt32("MaxLevel", indexes);
            hotfix.Unknown1153_0 = packet.ReadInt32("Unknown1153_0", indexes);
            hotfix.ItemLevel = packet.ReadInt32("ItemLevel", indexes);
            hotfix.Unknown1153_1 = packet.ReadInt32("Unknown1153_1", indexes);
            hotfix.Unknown1153_2 = packet.ReadInt32("Unknown1153_2", indexes);

            Storage.SpellItemEnchantmentHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SpellItemEnchantmentLocaleHotfix440 hotfixLocale = new SpellItemEnchantmentLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    HordeNameLang = hotfix.HordeName,
                };
                Storage.SpellItemEnchantmentHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SpellItemEnchantmentHandler441(Packet packet, uint entry, params object[] indexes)
        {
            SpellItemEnchantmentHotfix441 hotfix = new SpellItemEnchantmentHotfix441();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.HordeName = packet.ReadCString("HordeName", indexes);
            hotfix.Charges = packet.ReadInt32("Charges", indexes);
            hotfix.Effect = new int?[3];
            for (int i = 0; i < 3; i++)
                hotfix.Effect[i] = packet.ReadInt32("Effect", indexes, i);
            hotfix.EffectPointsMin = new int?[3];
            for (int i = 0; i < 3; i++)
                hotfix.EffectPointsMin[i] = packet.ReadInt32("EffectPointsMin", indexes, i);
            hotfix.EffectPointsMax = new int?[3];
            for (int i = 0; i < 3; i++)
                hotfix.EffectPointsMax[i] = packet.ReadInt32("EffectPointsMax", indexes, i);
            hotfix.EffectArg = new int?[3];
            for (int i = 0; i < 3; i++)
                hotfix.EffectArg[i] = packet.ReadInt32("EffectArg", indexes, i);
            hotfix.ItemVisual = packet.ReadInt32("ItemVisual", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);
            hotfix.EffectScalingPoints = new float?[3];
            for (int i = 0; i < 3; i++)
                hotfix.EffectScalingPoints[i] = packet.ReadSingle("EffectScalingPoints", indexes, i);
            hotfix.ScalingClass = packet.ReadInt32("ScalingClass", indexes);
            hotfix.ScalingClassRestricted = packet.ReadInt32("ScalingClassRestricted", indexes);
            hotfix.GemItemID = packet.ReadInt32("GemItemID", indexes);
            hotfix.ConditionID = packet.ReadInt32("ConditionID", indexes);
            hotfix.RequiredSkillID = packet.ReadInt32("RequiredSkillID", indexes);
            hotfix.RequiredSkillRank = packet.ReadInt32("RequiredSkillRank", indexes);
            hotfix.MinLevel = packet.ReadInt32("MinLevel", indexes);
            hotfix.MaxLevel = packet.ReadInt32("MaxLevel", indexes);
            hotfix.Unknown1153_0 = packet.ReadInt32("Unknown1153_0", indexes);
            hotfix.ItemLevel = packet.ReadInt32("ItemLevel", indexes);
            hotfix.Unknown1153_1 = packet.ReadInt32("Unknown1153_1", indexes);
            hotfix.Unknown1153_2 = packet.ReadInt32("Unknown1153_2", indexes);

            Storage.SpellItemEnchantmentHotfixes441.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SpellItemEnchantmentLocaleHotfix441 hotfixLocale = new SpellItemEnchantmentLocaleHotfix441
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    HordeNameLang = hotfix.HordeName,
                };
                Storage.SpellItemEnchantmentHotfixesLocale441.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SpellItemEnchantmentConditionHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellItemEnchantmentConditionHotfix440 hotfix = new SpellItemEnchantmentConditionHotfix440();

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

            Storage.SpellItemEnchantmentConditionHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellKeyboundOverrideHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellKeyboundOverrideHotfix440 hotfix = new SpellKeyboundOverrideHotfix440();

            hotfix.ID = entry;
            hotfix.Function = packet.ReadCString("Function", indexes);
            hotfix.Type = packet.ReadSByte("Type", indexes);
            hotfix.Data = packet.ReadInt32("Data", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.SpellKeyboundOverrideHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellLabelHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellLabelHotfix440 hotfix = new SpellLabelHotfix440();

            hotfix.ID = entry;
            hotfix.LabelID = packet.ReadUInt32("LabelID", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);

            Storage.SpellLabelHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellLearnSpellHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellLearnSpellHotfix440 hotfix = new SpellLearnSpellHotfix440();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.LearnSpellID = packet.ReadInt32("LearnSpellID", indexes);
            hotfix.OverridesSpellID = packet.ReadInt32("OverridesSpellID", indexes);

            Storage.SpellLearnSpellHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellLevelsHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellLevelsHotfix440 hotfix = new SpellLevelsHotfix440();

            hotfix.ID = entry;
            hotfix.DifficultyID = packet.ReadByte("DifficultyID", indexes);
            hotfix.BaseLevel = packet.ReadInt16("BaseLevel", indexes);
            hotfix.MaxLevel = packet.ReadInt16("MaxLevel", indexes);
            hotfix.SpellLevel = packet.ReadInt16("SpellLevel", indexes);
            hotfix.MaxPassiveAuraLevel = packet.ReadByte("MaxPassiveAuraLevel", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);

            Storage.SpellLevelsHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellMiscHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellMiscHotfix440 hotfix = new SpellMiscHotfix440();

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

            Storage.SpellMiscHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellMiscHandler441(Packet packet, uint entry, params object[] indexes)
        {
            SpellMiscHotfix441 hotfix = new SpellMiscHotfix441();

            hotfix.ID = entry;
            hotfix.Attributes = new int?[16];
            for (int i = 0; i < 16; i++)
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

            Storage.SpellMiscHotfixes441.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellNameHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellNameHotfix440 hotfix = new SpellNameHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);

            Storage.SpellNameHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SpellNameLocaleHotfix440 hotfixLocale = new SpellNameLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.SpellNameHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SpellPowerHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellPowerHotfix440 hotfix = new SpellPowerHotfix440();

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

            Storage.SpellPowerHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellPowerDifficultyHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellPowerDifficultyHotfix440 hotfix = new SpellPowerDifficultyHotfix440();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.DifficultyID = packet.ReadByte("DifficultyID", indexes);
            hotfix.OrderIndex = packet.ReadByte("OrderIndex", indexes);

            Storage.SpellPowerDifficultyHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellProcsPerMinuteHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellProcsPerMinuteHotfix440 hotfix = new SpellProcsPerMinuteHotfix440();

            hotfix.ID = entry;
            hotfix.BaseProcRate = packet.ReadSingle("BaseProcRate", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);

            Storage.SpellProcsPerMinuteHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellProcsPerMinuteModHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellProcsPerMinuteModHotfix440 hotfix = new SpellProcsPerMinuteModHotfix440();

            hotfix.ID = entry;
            hotfix.Type = packet.ReadByte("Type", indexes);
            hotfix.Param = packet.ReadInt16("Param", indexes);
            hotfix.Coeff = packet.ReadSingle("Coeff", indexes);
            hotfix.SpellProcsPerMinuteID = packet.ReadInt32("SpellProcsPerMinuteID", indexes);

            Storage.SpellProcsPerMinuteModHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellRadiusHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellRadiusHotfix440 hotfix = new SpellRadiusHotfix440();

            hotfix.ID = entry;
            hotfix.Radius = packet.ReadSingle("Radius", indexes);
            hotfix.RadiusPerLevel = packet.ReadSingle("RadiusPerLevel", indexes);
            hotfix.RadiusMin = packet.ReadSingle("RadiusMin", indexes);
            hotfix.RadiusMax = packet.ReadSingle("RadiusMax", indexes);

            Storage.SpellRadiusHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellRangeHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellRangeHotfix440 hotfix = new SpellRangeHotfix440();

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

            Storage.SpellRangeHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SpellRangeLocaleHotfix440 hotfixLocale = new SpellRangeLocaleHotfix440
                {
                    ID = hotfix.ID,
                    DisplayNameLang = hotfix.DisplayName,
                    DisplayNameShortLang = hotfix.DisplayNameShort,
                };
                Storage.SpellRangeHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SpellReagentsHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellReagentsHotfix440 hotfix = new SpellReagentsHotfix440();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.Reagent = new int?[8];
            for (int i = 0; i < 8; i++)
                hotfix.Reagent[i] = packet.ReadInt32("Reagent", indexes, i);
            hotfix.ReagentCount = new short?[8];
            for (int i = 0; i < 8; i++)
                hotfix.ReagentCount[i] = packet.ReadInt16("ReagentCount", indexes, i);

            Storage.SpellReagentsHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellReagentsCurrencyHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellReagentsCurrencyHotfix440 hotfix = new SpellReagentsCurrencyHotfix440();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.CurrencyTypesID = packet.ReadUInt16("CurrencyTypesID", indexes);
            hotfix.CurrencyCount = packet.ReadUInt16("CurrencyCount", indexes);

            Storage.SpellReagentsCurrencyHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellScalingHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellScalingHotfix440 hotfix = new SpellScalingHotfix440();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.Class = packet.ReadInt32("Class", indexes);
            hotfix.MinScalingLevel = packet.ReadUInt32("MinScalingLevel", indexes);
            hotfix.MaxScalingLevel = packet.ReadUInt32("MaxScalingLevel", indexes);
            hotfix.ScalesFromItemLevel = packet.ReadInt16("ScalesFromItemLevel", indexes);
            hotfix.CastTimeMin = packet.ReadInt32("CastTimeMin", indexes);
            hotfix.CastTimeMax = packet.ReadInt32("CastTimeMax", indexes);
            hotfix.CastTimeMaxLevel = packet.ReadInt32("CastTimeMaxLevel", indexes);
            hotfix.NerfFactor = packet.ReadSingle("NerfFactor", indexes);
            hotfix.NerfMaxLevel = packet.ReadInt32("NerfMaxLevel", indexes);

            Storage.SpellScalingHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellShapeshiftHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellShapeshiftHotfix440 hotfix = new SpellShapeshiftHotfix440();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.StanceBarOrder = packet.ReadSByte("StanceBarOrder", indexes);
            hotfix.ShapeshiftExclude = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.ShapeshiftExclude[i] = packet.ReadInt32("ShapeshiftExclude", indexes, i);
            hotfix.ShapeshiftMask = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.ShapeshiftMask[i] = packet.ReadInt32("ShapeshiftMask", indexes, i);

            Storage.SpellShapeshiftHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellShapeshiftFormHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellShapeshiftFormHotfix440 hotfix = new SpellShapeshiftFormHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.Unknown1150 = packet.ReadUInt32("Unknown1150", indexes);
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

            Storage.SpellShapeshiftFormHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                SpellShapeshiftFormLocaleHotfix440 hotfixLocale = new SpellShapeshiftFormLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.SpellShapeshiftFormHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void SpellTargetRestrictionsHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellTargetRestrictionsHotfix440 hotfix = new SpellTargetRestrictionsHotfix440();

            hotfix.ID = entry;
            hotfix.DifficultyID = packet.ReadByte("DifficultyID", indexes);
            hotfix.ConeDegrees = packet.ReadSingle("ConeDegrees", indexes);
            hotfix.MaxTargets = packet.ReadByte("MaxTargets", indexes);
            hotfix.MaxTargetLevel = packet.ReadUInt32("MaxTargetLevel", indexes);
            hotfix.TargetCreatureType = packet.ReadInt16("TargetCreatureType", indexes);
            hotfix.Targets = packet.ReadInt32("Targets", indexes);
            hotfix.Width = packet.ReadSingle("Width", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);

            Storage.SpellTargetRestrictionsHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellTotemsHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellTotemsHotfix440 hotfix = new SpellTotemsHotfix440();

            hotfix.ID = entry;
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.RequiredTotemCategoryID = new ushort?[2];
            for (int i = 0; i < 2; i++)
                hotfix.RequiredTotemCategoryID[i] = packet.ReadUInt16("RequiredTotemCategoryID", indexes, i);
            hotfix.Totem = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Totem[i] = packet.ReadInt32("Totem", indexes, i);

            Storage.SpellTotemsHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellVisualHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellVisualHotfix440 hotfix = new SpellVisualHotfix440();

            hotfix.ID = entry;
            hotfix.MissileCastOffsetX = packet.ReadSingle("MissileCastOffsetX", indexes);
            hotfix.MissileCastOffsetY = packet.ReadSingle("MissileCastOffsetY", indexes);
            hotfix.MissileCastOffsetZ = packet.ReadSingle("MissileCastOffsetZ", indexes);
            hotfix.MissileImpactOffsetX = packet.ReadSingle("MissileImpactOffsetX", indexes);
            hotfix.MissileImpactOffsetY = packet.ReadSingle("MissileImpactOffsetY", indexes);
            hotfix.MissileImpactOffsetZ = packet.ReadSingle("MissileImpactOffsetZ", indexes);
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

            Storage.SpellVisualHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellVisualEffectNameHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellVisualEffectNameHotfix440 hotfix = new SpellVisualEffectNameHotfix440();

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

            Storage.SpellVisualEffectNameHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellVisualEffectNameHandler441(Packet packet, uint entry, params object[] indexes)
        {
            SpellVisualEffectNameHotfix441 hotfix = new SpellVisualEffectNameHotfix441();

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
            hotfix.Unknown1154 = packet.ReadUInt16("Unknown1154", indexes);

            Storage.SpellVisualEffectNameHotfixes441.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellVisualMissileHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellVisualMissileHotfix440 hotfix = new SpellVisualMissileHotfix440();

            hotfix.CastOffsetX = packet.ReadSingle("CastOffsetX", indexes);
            hotfix.CastOffsetY = packet.ReadSingle("CastOffsetY", indexes);
            hotfix.CastOffsetZ = packet.ReadSingle("CastOffsetZ", indexes);
            hotfix.ImpactOffsetX = packet.ReadSingle("ImpactOffsetX", indexes);
            hotfix.ImpactOffsetY = packet.ReadSingle("ImpactOffsetY", indexes);
            hotfix.ImpactOffsetZ = packet.ReadSingle("ImpactOffsetZ", indexes);
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
            hotfix.SpellVisualMissileSetID = packet.ReadInt32("SpellVisualMissileSetID", indexes);

            Storage.SpellVisualMissileHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellVisualMissileHandler441(Packet packet, uint entry, params object[] indexes)
        {
            SpellVisualMissileHotfix441 hotfix = new SpellVisualMissileHotfix441();

            hotfix.CastOffsetX = packet.ReadSingle("CastOffsetX", indexes);
            hotfix.CastOffsetY = packet.ReadSingle("CastOffsetY", indexes);
            hotfix.CastOffsetZ = packet.ReadSingle("CastOffsetZ", indexes);
            hotfix.ImpactOffsetX = packet.ReadSingle("ImpactOffsetX", indexes);
            hotfix.ImpactOffsetY = packet.ReadSingle("ImpactOffsetY", indexes);
            hotfix.ImpactOffsetZ = packet.ReadSingle("ImpactOffsetZ", indexes);
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
            hotfix.Field115456400015 = packet.ReadUInt16("Field115456400015", indexes);
            hotfix.SpellVisualMissileSetID = packet.ReadInt32("SpellVisualMissileSetID", indexes);

            Storage.SpellVisualMissileHotfixes441.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellVisualKitHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellVisualKitHotfix440 hotfix = new SpellVisualKitHotfix440();

            hotfix.ID = entry;
            hotfix.FallbackSpellVisualKitID = packet.ReadUInt32("FallbackSpellVisualKitID", indexes);
            hotfix.DelayMin = packet.ReadUInt16("DelayMin", indexes);
            hotfix.DelayMax = packet.ReadUInt16("DelayMax", indexes);
            hotfix.FallbackPriority = packet.ReadSingle("FallbackPriority", indexes);
            hotfix.Flags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.SpellVisualKitHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SpellXSpellVisualHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SpellXSpellVisualHotfix440 hotfix = new SpellXSpellVisualHotfix440();

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

            Storage.SpellXSpellVisualHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void SummonPropertiesHandler440(Packet packet, uint entry, params object[] indexes)
        {
            SummonPropertiesHotfix440 hotfix = new SummonPropertiesHotfix440();

            hotfix.ID = entry;
            hotfix.Control = packet.ReadInt32("Control", indexes);
            hotfix.Faction = packet.ReadInt32("Faction", indexes);
            hotfix.Title = packet.ReadInt32("Title", indexes);
            hotfix.Slot = packet.ReadInt32("Slot", indexes);
            hotfix.Flags = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.Flags[i] = packet.ReadInt32("Flags", indexes, i);

            Storage.SummonPropertiesHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void TactKeyHandler440(Packet packet, uint entry, params object[] indexes)
        {
            TactKeyHotfix440 hotfix = new TactKeyHotfix440();

            hotfix.ID = entry;
            hotfix.Key = new byte?[16];
            for (int i = 0; i < 16; i++)
                hotfix.Key[i] = packet.ReadByte("Key", indexes, i);

            Storage.TactKeyHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void TalentHandler440(Packet packet, uint entry, params object[] indexes)
        {
            TalentHotfix440 hotfix = new TalentHotfix440();

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

            Storage.TalentHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                TalentLocaleHotfix440 hotfixLocale = new TalentLocaleHotfix440
                {
                    ID = hotfix.ID,
                    DescriptionLang = hotfix.Description,
                };
                Storage.TalentHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void TalentTabHandler440(Packet packet, uint entry, params object[] indexes)
        {
            TalentTabHotfix440 hotfix = new TalentTabHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.BackgroundFile = packet.ReadCString("BackgroundFile", indexes);
            hotfix.Description = packet.ReadCString("Description", indexes);
            hotfix.OrderIndex = packet.ReadInt32("OrderIndex", indexes);
            hotfix.RaceMask = packet.ReadInt32("RaceMask", indexes);
            hotfix.ClassMask = packet.ReadInt32("ClassMask", indexes);
            hotfix.CategoryEnumID = packet.ReadInt32("CategoryEnumID", indexes);
            hotfix.SpellIconID = packet.ReadInt32("SpellIconID", indexes);
            hotfix.RoleMask = packet.ReadInt32("RoleMask", indexes);
            hotfix.MasterySpellID = new int?[2];
            for (int i = 0; i < 2; i++)
                hotfix.MasterySpellID[i] = packet.ReadInt32("MasterySpellID", indexes, i);

            Storage.TalentTabHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                TalentTabLocaleHotfix440 hotfixLocale = new TalentTabLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    DescriptionLang = hotfix.Description,
                };
                Storage.TalentTabHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void TalentTreePrimarySpellsHandler440(Packet packet, uint entry, params object[] indexes)
        {
            TalentTreePrimarySpellsHotfix440 hotfix = new TalentTreePrimarySpellsHotfix440();

            hotfix.ID = entry;
            hotfix.TalentTabID = packet.ReadInt32("TalentTabID", indexes);
            hotfix.SpellID = packet.ReadInt32("SpellID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.TalentTreePrimarySpellsHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void TaxiNodesHandler440(Packet packet, uint entry, params object[] indexes)
        {
            TaxiNodesHotfix440 hotfix = new TaxiNodesHotfix440();

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

            Storage.TaxiNodesHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                TaxiNodesLocaleHotfix440 hotfixLocale = new TaxiNodesLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.TaxiNodesHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void TaxiPathHandler440(Packet packet, uint entry, params object[] indexes)
        {
            TaxiPathHotfix440 hotfix = new TaxiPathHotfix440();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.FromTaxiNode = packet.ReadUInt16("FromTaxiNode", indexes);
            hotfix.ToTaxiNode = packet.ReadUInt16("ToTaxiNode", indexes);
            hotfix.Cost = packet.ReadUInt32("Cost", indexes);

            Storage.TaxiPathHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void TaxiPathNodeHandler440(Packet packet, uint entry, params object[] indexes)
        {
            TaxiPathNodeHotfix440 hotfix = new TaxiPathNodeHotfix440();

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

            Storage.TaxiPathNodeHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void TotemCategoryHandler440(Packet packet, uint entry, params object[] indexes)
        {
            TotemCategoryHotfix440 hotfix = new TotemCategoryHotfix440();

            hotfix.ID = entry;
            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.TotemCategoryType = packet.ReadByte("TotemCategoryType", indexes);
            hotfix.TotemCategoryMask = packet.ReadInt32("TotemCategoryMask", indexes);

            Storage.TotemCategoryHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                TotemCategoryLocaleHotfix440 hotfixLocale = new TotemCategoryLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.TotemCategoryHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void ToyHandler440(Packet packet, uint entry, params object[] indexes)
        {
            ToyHotfix440 hotfix = new ToyHotfix440();

            hotfix.SourceText = packet.ReadCString("SourceText", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.ItemID = packet.ReadInt32("ItemID", indexes);
            hotfix.Flags = packet.ReadByte("Flags", indexes);
            hotfix.SourceTypeEnum = packet.ReadSByte("SourceTypeEnum", indexes);

            Storage.ToyHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                ToyLocaleHotfix440 hotfixLocale = new ToyLocaleHotfix440
                {
                    ID = hotfix.ID,
                    SourceTextLang = hotfix.SourceText,
                };
                Storage.ToyHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void TransmogHolidayHandler440(Packet packet, uint entry, params object[] indexes)
        {
            TransmogHolidayHotfix440 hotfix = new TransmogHolidayHotfix440();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.RequiredTransmogHoliday = packet.ReadInt32("RequiredTransmogHoliday", indexes);

            Storage.TransmogHolidayHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void TransmogSetHandler440(Packet packet, uint entry, params object[] indexes)
        {
            TransmogSetHotfix440 hotfix = new TransmogSetHotfix440();

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

            Storage.TransmogSetHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                TransmogSetLocaleHotfix440 hotfixLocale = new TransmogSetLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.TransmogSetHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void TransmogSetGroupHandler440(Packet packet, uint entry, params object[] indexes)
        {
            TransmogSetGroupHotfix440 hotfix = new TransmogSetGroupHotfix440();

            hotfix.Name = packet.ReadCString("Name", indexes);
            hotfix.ID = packet.ReadUInt32("ID", indexes);

            Storage.TransmogSetGroupHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                TransmogSetGroupLocaleHotfix440 hotfixLocale = new TransmogSetGroupLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.TransmogSetGroupHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void TransmogSetItemHandler440(Packet packet, uint entry, params object[] indexes)
        {
            TransmogSetItemHotfix440 hotfix = new TransmogSetItemHotfix440();

            hotfix.ID = packet.ReadUInt32("ID", indexes);
            hotfix.TransmogSetID = packet.ReadUInt32("TransmogSetID", indexes);
            hotfix.ItemModifiedAppearanceID = packet.ReadUInt32("ItemModifiedAppearanceID", indexes);
            hotfix.Flags = packet.ReadInt32("Flags", indexes);

            Storage.TransmogSetItemHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void TransportAnimationHandler440(Packet packet, uint entry, params object[] indexes)
        {
            TransportAnimationHotfix440 hotfix = new TransportAnimationHotfix440();

            hotfix.ID = entry;
            hotfix.PosX = packet.ReadSingle("PosX", indexes);
            hotfix.PosY = packet.ReadSingle("PosY", indexes);
            hotfix.PosZ = packet.ReadSingle("PosZ", indexes);
            hotfix.SequenceID = packet.ReadByte("SequenceID", indexes);
            hotfix.TimeIndex = packet.ReadUInt32("TimeIndex", indexes);
            hotfix.TransportID = packet.ReadInt32("TransportID", indexes);

            Storage.TransportAnimationHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void TransportRotationHandler440(Packet packet, uint entry, params object[] indexes)
        {
            TransportRotationHotfix440 hotfix = new TransportRotationHotfix440();

            hotfix.ID = entry;
            hotfix.Rot = new float?[4];
            for (int i = 0; i < 4; i++)
                hotfix.Rot[i] = packet.ReadSingle("Rot", indexes, i);
            hotfix.TimeIndex = packet.ReadUInt32("TimeIndex", indexes);
            hotfix.GameObjectsID = packet.ReadInt32("GameObjectsID", indexes);

            Storage.TransportRotationHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void UiMapHandler440(Packet packet, uint entry, params object[] indexes)
        {
            UiMapHotfix440 hotfix = new UiMapHotfix440();

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

            Storage.UiMapHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                UiMapLocaleHotfix440 hotfixLocale = new UiMapLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.UiMapHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void UiMapAssignmentHandler440(Packet packet, uint entry, params object[] indexes)
        {
            UiMapAssignmentHotfix440 hotfix = new UiMapAssignmentHotfix440();

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

            Storage.UiMapAssignmentHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void UiMapLinkHandler440(Packet packet, uint entry, params object[] indexes)
        {
            UiMapLinkHotfix440 hotfix = new UiMapLinkHotfix440();

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

            Storage.UiMapLinkHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void UiMapXMapArtHandler440(Packet packet, uint entry, params object[] indexes)
        {
            UiMapXMapArtHotfix440 hotfix = new UiMapXMapArtHotfix440();

            hotfix.ID = entry;
            hotfix.PhaseID = packet.ReadInt32("PhaseID", indexes);
            hotfix.UiMapArtID = packet.ReadInt32("UiMapArtID", indexes);
            hotfix.UiMapID = packet.ReadInt32("UiMapID", indexes);

            Storage.UiMapXMapArtHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void UnitConditionHandler440(Packet packet, uint entry, params object[] indexes)
        {
            UnitConditionHotfix440 hotfix = new UnitConditionHotfix440();

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

            Storage.UnitConditionHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void UnitConditionHandler441(Packet packet, uint entry, params object[] indexes)
        {
            UnitConditionHotfix441 hotfix = new UnitConditionHotfix441();

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

            Storage.UnitConditionHotfixes441.Add(hotfix, packet.TimeSpan);
        }

        public static void UnitPowerBarHandler440(Packet packet, uint entry, params object[] indexes)
        {
            UnitPowerBarHotfix440 hotfix = new UnitPowerBarHotfix440();

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

            Storage.UnitPowerBarHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                UnitPowerBarLocaleHotfix440 hotfixLocale = new UnitPowerBarLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                    CostLang = hotfix.Cost,
                    OutOfErrorLang = hotfix.OutOfError,
                    ToolTipLang = hotfix.ToolTip,
                };
                Storage.UnitPowerBarHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void VehicleHandler440(Packet packet, uint entry, params object[] indexes)
        {
            VehicleHotfix440 hotfix = new VehicleHotfix440();

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

            Storage.VehicleHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void VehicleSeatHandler440(Packet packet, uint entry, params object[] indexes)
        {
            VehicleSeatHotfix440 hotfix = new VehicleSeatHotfix440();

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

            Storage.VehicleSeatHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void VehicleSeatHandler441(Packet packet, uint entry, params object[] indexes)
        {
            VehicleSeatHotfix441 hotfix = new VehicleSeatHotfix441();

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

            Storage.VehicleSeatHotfixes441.Add(hotfix, packet.TimeSpan);
        }

        public static void VignetteHandler440(Packet packet, uint entry, params object[] indexes)
        {
            VignetteHotfix440 hotfix = new VignetteHotfix440();

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

            Storage.VignetteHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                VignetteLocaleHotfix440 hotfixLocale = new VignetteLocaleHotfix440
                {
                    ID = hotfix.ID,
                    NameLang = hotfix.Name,
                };
                Storage.VignetteHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void WmoAreaTableHandler440(Packet packet, uint entry, params object[] indexes)
        {
            WmoAreaTableHotfix440 hotfix = new WmoAreaTableHotfix440();

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

            Storage.WmoAreaTableHotfixes440.Add(hotfix, packet.TimeSpan);

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                WmoAreaTableLocaleHotfix440 hotfixLocale = new WmoAreaTableLocaleHotfix440
                {
                    ID = hotfix.ID,
                    AreaNameLang = hotfix.AreaName,
                };
                Storage.WmoAreaTableHotfixesLocale440.Add(hotfixLocale, packet.TimeSpan);
            }
        }

        public static void WorldEffectHandler440(Packet packet, uint entry, params object[] indexes)
        {
            WorldEffectHotfix440 hotfix = new WorldEffectHotfix440();

            hotfix.ID = entry;
            hotfix.QuestFeedbackEffectID = packet.ReadUInt32("QuestFeedbackEffectID", indexes);
            hotfix.WhenToDisplay = packet.ReadByte("WhenToDisplay", indexes);
            hotfix.TargetType = packet.ReadByte("TargetType", indexes);
            hotfix.TargetAsset = packet.ReadInt32("TargetAsset", indexes);
            hotfix.PlayerConditionID = packet.ReadUInt32("PlayerConditionID", indexes);
            hotfix.CombatConditionID = packet.ReadUInt16("CombatConditionID", indexes);

            Storage.WorldEffectHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void WorldMapOverlayHandler440(Packet packet, uint entry, params object[] indexes)
        {
            WorldMapOverlayHotfix440 hotfix = new WorldMapOverlayHotfix440();

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

            Storage.WorldMapOverlayHotfixes440.Add(hotfix, packet.TimeSpan);
        }

        public static void WorldStateExpressionHandler440(Packet packet, uint entry, params object[] indexes)
        {
            WorldStateExpressionHotfix440 hotfix = new WorldStateExpressionHotfix440();

            hotfix.ID = entry;
            hotfix.Expression = packet.ReadCString("Expression", indexes);

            Storage.WorldStateExpressionHotfixes440.Add(hotfix, packet.TimeSpan);
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
                            AchievementHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AchievementCategory:
                        {
                            AchievementCategoryHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AnimationData:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                AnimationDataHandler441(db2File, (uint)entry, indexes);
                            else
                                AnimationDataHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AnimKit:
                        {
                            AnimKitHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AreaGroupMember:
                        {
                            AreaGroupMemberHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AreaTable:
                        {
                            AreaTableHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AreaTrigger:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                AreaTriggerHandler441(db2File, (uint)entry, indexes);
                            else
                                AreaTriggerHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AreaTriggerActionSet:
                        {
                            AreaTriggerActionSetHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ArmorLocation:
                        {
                            ArmorLocationHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.AuctionHouse:
                        {
                            AuctionHouseHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.BankBagSlotPrices:
                        {
                            BankBagSlotPricesHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.BannedAddons:
                        {
                            BannedAddonsHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.BarberShopStyle:
                        {
                            BarberShopStyleHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.BattlePetAbility:
                        {
                            BattlePetAbilityHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.BattlePetBreedQuality:
                        {
                            BattlePetBreedQualityHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.BattlePetBreedState:
                        {
                            BattlePetBreedStateHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.BattlePetSpecies:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                BattlePetSpeciesHandler441(db2File, (uint)entry, indexes);
                            else
                                BattlePetSpeciesHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.BattlePetSpeciesState:
                        {
                            BattlePetSpeciesStateHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.BattlemasterList:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                BattlemasterListHandler441(db2File, (uint)entry, indexes);
                            else
                                BattlemasterListHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.BattlemasterListXMap:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                BattlemasterListXMapHandler441(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.BroadcastText:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                BroadcastTextHandler441(db2File, (uint)entry, indexes);
                            else
                                BroadcastTextHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CfgCategories:
                        {
                            CfgCategoriesHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CfgRegions:
                        {
                            CfgRegionsHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CharTitles:
                        {
                            CharTitlesHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CharacterLoadout:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                CharacterLoadoutHandler441(db2File, (uint)entry, indexes);
                            else
                                CharacterLoadoutHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CharacterLoadoutItem:
                        {
                            CharacterLoadoutItemHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChatChannels:
                        {
                            ChatChannelsHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrClassUiDisplay:
                        {
                            ChrClassUiDisplayHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrClasses:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                ChrClassesHandler441(db2File, (uint)entry, indexes);
                            else
                                ChrClassesHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrClassesXPowerTypes:
                        {
                            ChrClassesXPowerTypesHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrCustomizationChoice:
                        {
                            ChrCustomizationChoiceHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrCustomizationDisplayInfo:
                        {
                            ChrCustomizationDisplayInfoHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrCustomizationElement:
                        {
                            ChrCustomizationElementHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrCustomizationOption:
                        {
                            ChrCustomizationOptionHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrCustomizationReq:
                        {
                            ChrCustomizationReqHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrCustomizationReqChoice:
                        {
                            ChrCustomizationReqChoiceHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrModel:
                        {
                            ChrModelHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrRaceXChrModel:
                        {
                            ChrRaceXChrModelHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ChrRaces:
                        {
                            ChrRacesHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CinematicCamera:
                        {
                            CinematicCameraHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CinematicSequences:
                        {
                            CinematicSequencesHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ConditionalChrModel:
                        {
                            ConditionalChrModelHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ConditionalContentTuning:
                        {
                            ConditionalContentTuningHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ContentTuning:
                        {
                            ContentTuningHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ConversationLine:
                        {
                            ConversationLineHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CreatureDisplayInfo:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                CreatureDisplayInfoHandler441(db2File, (uint)entry, indexes);
                            else
                                CreatureDisplayInfoHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CreatureDisplayInfoExtra:
                        {
                            CreatureDisplayInfoExtraHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CreatureFamily:
                        {
                            CreatureFamilyHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CreatureModelData:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                CreatureModelDataHandler441(db2File, (uint)entry, indexes);
                            else
                                CreatureModelDataHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CreatureType:
                        {
                            CreatureTypeHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Criteria:
                        {
                            CriteriaHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CriteriaTree:
                        {
                            CriteriaTreeHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CurrencyContainer:
                        {
                            CurrencyContainerHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CurrencyTypes:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                CurrencyTypesHandler441(db2File, (uint)entry, indexes);
                            else
                                CurrencyTypesHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Curve:
                        {
                            CurveHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.CurvePoint:
                        {
                            CurvePointHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.DestructibleModelData:
                        {
                            DestructibleModelDataHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Difficulty:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                DifficultyHandler441(db2File, (uint)entry, indexes);
                            else
                                DifficultyHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.DungeonEncounter:
                        {
                            DungeonEncounterHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.DurabilityCosts:
                        {
                            DurabilityCostsHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.DurabilityQuality:
                        {
                            DurabilityQualityHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Emotes:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                EmotesHandler441(db2File, (uint)entry, indexes);
                            else
                                EmotesHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.EmotesText:
                        {
                            EmotesTextHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.EmotesTextSound:
                        {
                            EmotesTextSoundHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ExpectedStat:
                        {
                            ExpectedStatHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ExpectedStatMod:
                        {
                            ExpectedStatModHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Faction:
                        {
                            FactionHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.FactionTemplate:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                FactionTemplateHandler441(db2File, (uint)entry, indexes);
                            else
                                FactionTemplateHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.FriendshipRepReaction:
                        {
                            FriendshipRepReactionHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.FriendshipReputation:
                        {
                            FriendshipReputationHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GameobjectArtKit:
                        {
                            GameobjectArtKitHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GameobjectDisplayInfo:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                GameobjectDisplayInfoHandler441(db2File, (uint)entry, indexes);
                            else
                                GameobjectDisplayInfoHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Gameobjects:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                GameobjectsHandler441(db2File, (uint)entry, indexes);
                            else
                                GameobjectsHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GemProperties:
                        {
                            GemPropertiesHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GlyphBindableSpell:
                        {
                            GlyphBindableSpellHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GlyphSlot:
                        {
                            GlyphSlotHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GlyphProperties:
                        {
                            GlyphPropertiesHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GlyphRequiredSpec:
                        {
                            GlyphRequiredSpecHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GossipNpcOption:
                        {
                            GossipNPCOptionHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GuildColorBackground:
                        {
                            GuildColorBackgroundHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GuildColorBorder:
                        {
                            GuildColorBorderHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GuildColorEmblem:
                        {
                            GuildColorEmblemHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.GuildPerkSpells:
                        {
                            GuildPerkSpellsHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Heirloom:
                        {
                            HeirloomHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Holidays:
                        {
                            HolidaysHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ImportPriceArmor:
                        {
                            ImportPriceArmorHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ImportPriceQuality:
                        {
                            ImportPriceQualityHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ImportPriceShield:
                        {
                            ImportPriceShieldHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ImportPriceWeapon:
                        {
                            ImportPriceWeaponHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Item:
                        {
                            ItemHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemAppearance:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                ItemAppearanceHandler441(db2File, (uint)entry, indexes);
                            else
                                ItemAppearanceHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemArmorQuality:
                        {
                            ItemArmorQualityHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemArmorShield:
                        {
                            ItemArmorShieldHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemArmorTotal:
                        {
                            ItemArmorTotalHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemBagFamily:
                        {
                            ItemBagFamilyHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemBonus:
                        {
                            ItemBonusHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemBonusListLevelDelta:
                        {
                            ItemBonusListLevelDeltaHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemBonusTreeNode:
                        {
                            ItemBonusTreeNodeHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemChildEquipment:
                        {
                            ItemChildEquipmentHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemClass:
                        {
                            ItemClassHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemContextPickerEntry:
                        {
                            ItemContextPickerEntryHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemCurrencyCost:
                        {
                            ItemCurrencyCostHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemDamageAmmo:
                        {
                            ItemDamageAmmoHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemDamageOneHand:
                        {
                            ItemDamageOneHandHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemDamageOneHandCaster:
                        {
                            ItemDamageOneHandCasterHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemDamageTwoHand:
                        {
                            ItemDamageTwoHandHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemDamageTwoHandCaster:
                        {
                            ItemDamageTwoHandCasterHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemDisenchantLoot:
                        {
                            ItemDisenchantLootHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemEffect:
                        {
                            ItemEffectHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemExtendedCost:
                        {
                            ItemExtendedCostHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemLevelSelector:
                        {
                            ItemLevelSelectorHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemLevelSelectorQuality:
                        {
                            ItemLevelSelectorQualityHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemLevelSelectorQualitySet:
                        {
                            ItemLevelSelectorQualitySetHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemLimitCategory:
                        {
                            ItemLimitCategoryHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemLimitCategoryCondition:
                        {
                            ItemLimitCategoryConditionHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemModifiedAppearance:
                        {
                            ItemModifiedAppearanceHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemModifiedAppearanceExtra:
                        {
                            ItemModifiedAppearanceExtraHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemNameDescription:
                        {
                            ItemNameDescriptionHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemPriceBase:
                        {
                            ItemPriceBaseHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemReforge:
                        {
                            ItemReforgeHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemSearchName:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                ItemSearchNameHandler441(db2File, (uint)entry, indexes);
                            else
                                ItemSearchNameHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemSet:
                        {
                            ItemSetHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemSetSpell:
                        {
                            ItemSetSpellHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemSparse:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                ItemSparseHandler441(db2File, (uint)entry, indexes);
                            else
                                ItemSparseHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ItemXBonusTree:
                        {
                            ItemXBonusTreeHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.JournalEncounter:
                        {
                            JournalEncounterHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.JournalEncounterSection:
                        {
                            JournalEncounterSectionHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.JournalInstance:
                        {
                            JournalInstanceHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.JournalTier:
                        {
                            JournalTierHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Keychain:
                        {
                            KeychainHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.KeystoneAffix:
                        {
                            KeystoneAffixHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.LanguageWords:
                        {
                            LanguageWordsHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Languages:
                        {
                            LanguagesHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.LfgDungeons:
                        {
                            LfgDungeonsHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Light:
                        {
                            LightHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.LiquidType:
                        {
                            LiquidTypeHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Location:
                        {
                            LocationHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Lock:
                        {
                            LockHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.MailTemplate:
                        {
                            MailTemplateHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Map:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                MapHandler441(db2File, (uint)entry, indexes);
                            else
                                MapHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.MapChallengeMode:
                        {
                            MapChallengeModeHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.MapDifficulty:
                        {
                            MapDifficultyHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.MapDifficultyXCondition:
                        {
                            MapDifficultyXConditionHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ModifierTree:
                        {
                            ModifierTreeHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Mount:
                        {
                            MountHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.MountCapability:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                MountCapabilityHandler441(db2File, (uint)entry, indexes);
                            else
                                MountCapabilityHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.MountTypeXCapability:
                        {
                            MountTypeXCapabilityHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.MountXDisplay:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                MountXDisplayHandler441(db2File, (uint)entry, indexes);
                            else
                                MountXDisplayHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Movie:
                        {
                            MovieHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.MythicPlusSeason:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                MythicPlusSeasonHandler441(db2File, (uint)entry, indexes);
                            else
                                MythicPlusSeasonHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.NameGen:
                        {
                            NameGenHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.NamesProfanity:
                        {
                            NamesProfanityHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.NamesReserved:
                        {
                            NamesReservedHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.NamesReservedLocale:
                        {
                            NamesReservedLocaleHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.NumTalentsAtLevel:
                        {
                            NumTalentsAtLevelHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.OverrideSpellData:
                        {
                            OverrideSpellDataHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ParagonReputation:
                        {
                            ParagonReputationHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Path:
                        {
                            PathHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PathNode:
                        {
                            PathNodeHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PathProperty:
                        {
                            PathPropertyHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Phase:
                        {
                            PhaseHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PhaseXPhaseGroup:
                        {
                            PhaseXPhaseGroupHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PlayerCondition:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                PlayerConditionHandler441(db2File, (uint)entry, indexes);
                            else
                                PlayerConditionHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PowerDisplay:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                PowerDisplayHandler441(db2File, (uint)entry, indexes);
                            else
                                PowerDisplayHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PowerType:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                PowerTypeHandler441(db2File, (uint)entry, indexes);
                            else
                                PowerTypeHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PrestigeLevelInfo:
                        {
                            PrestigeLevelInfoHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PvpDifficulty:
                        {
                            PvpDifficultyHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PvpItem:
                        {
                            PvpItemHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PvpSeason:
                        {
                            PvpSeasonHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.PvpTier:
                        {
                            PvpTierHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.QuestFactionReward:
                        {
                            QuestFactionRewardHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.QuestInfo:
                        {
                            QuestInfoHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.QuestLineXQuest:
                        {
                            QuestLineXQuestHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.QuestMoneyReward:
                        {
                            QuestMoneyRewardHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.QuestPackageItem:
                        {
                            QuestPackageItemHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.QuestSort:
                        {
                            QuestSortHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.QuestV2:
                        {
                            QuestV2Handler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.QuestXp:
                        {
                            QuestXpHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.RandPropPoints:
                        {
                            RandPropPointsHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.RewardPack:
                        {
                            RewardPackHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.RewardPackXCurrencyType:
                        {
                            RewardPackXCurrencyTypeHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.RewardPackXItem:
                        {
                            RewardPackXItemHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Scenario:
                        {
                            ScenarioHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ScenarioStep:
                        {
                            ScenarioStepHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SceneScript:
                        {
                            SceneScriptHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SceneScriptGlobalText:
                        {
                            SceneScriptGlobalTextHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SceneScriptPackage:
                        {
                            SceneScriptPackageHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SceneScriptText:
                        {
                            SceneScriptTextHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.ServerMessages:
                        {
                            ServerMessagesHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SkillLine:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                SkillLineHandler441(db2File, (uint)entry, indexes);
                            else
                                SkillLineHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SkillLineAbility:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                SkillLineAbilityHandler441(db2File, (uint)entry, indexes);
                            else
                                SkillLineAbilityHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SkillRaceClassInfo:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                SkillRaceClassInfoHandler441(db2File, (uint)entry, indexes);
                            else
                                SkillRaceClassInfoHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SoundKit:
                        {
                            SoundKitHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellAuraOptions:
                        {
                            SpellAuraOptionsHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellAuraRestrictions:
                        {
                            SpellAuraRestrictionsHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellCastTimes:
                        {
                            SpellCastTimesHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellCastingRequirements:
                        {
                            SpellCastingRequirementsHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellCategories:
                        {
                            SpellCategoriesHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellCategory:
                        {
                            SpellCategoryHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellClassOptions:
                        {
                            SpellClassOptionsHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellCooldowns:
                        {
                            SpellCooldownsHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellDuration:
                        {
                            SpellDurationHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellEffect:
                        {
                            SpellEffectHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellEquippedItems:
                        {
                            SpellEquippedItemsHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellFocusObject:
                        {
                            SpellFocusObjectHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellInterrupts:
                        {
                            SpellInterruptsHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellItemEnchantment:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                SpellItemEnchantmentHandler441(db2File, (uint)entry, indexes);
                            else
                                SpellItemEnchantmentHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellItemEnchantmentCondition:
                        {
                            SpellItemEnchantmentConditionHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellKeyboundOverride:
                        {
                            SpellKeyboundOverrideHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellLabel:
                        {
                            SpellLabelHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellLearnSpell:
                        {
                            SpellLearnSpellHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellLevels:
                        {
                            SpellLevelsHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellMisc:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                SpellMiscHandler441(db2File, (uint)entry, indexes);
                            else
                                SpellMiscHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellName:
                        {
                            SpellNameHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellPower:
                        {
                            SpellPowerHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellPowerDifficulty:
                        {
                            SpellPowerDifficultyHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellProcsPerMinute:
                        {
                            SpellProcsPerMinuteHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellProcsPerMinuteMod:
                        {
                            SpellProcsPerMinuteModHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellRadius:
                        {
                            SpellRadiusHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellRange:
                        {
                            SpellRangeHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellReagents:
                        {
                            SpellReagentsHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellReagentsCurrency:
                        {
                            SpellReagentsCurrencyHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellScaling:
                        {
                            SpellScalingHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellShapeshift:
                        {
                            SpellShapeshiftHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellShapeshiftForm:
                        {
                            SpellShapeshiftFormHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellTargetRestrictions:
                        {
                            SpellTargetRestrictionsHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellTotems:
                        {
                            SpellTotemsHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellVisual:
                        {
                            SpellVisualHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellVisualEffectName:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                SpellVisualEffectNameHandler441(db2File, (uint)entry, indexes);
                            else
                                SpellVisualEffectNameHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellVisualMissile:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                SpellVisualMissileHandler441(db2File, (uint)entry, indexes);
                            else
                                SpellVisualMissileHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellVisualKit:
                        {
                            SpellVisualKitHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SpellXSpellVisual:
                        {
                            SpellXSpellVisualHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.SummonProperties:
                        {
                            SummonPropertiesHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TactKey:
                        {
                            TactKeyHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Talent:
                        {
                            TalentHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TalentTab:
                        {
                            TalentTabHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TalentTreePrimarySpells:
                        {
                            TalentTreePrimarySpellsHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TaxiNodes:
                        {
                            TaxiNodesHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TaxiPath:
                        {
                            TaxiPathHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TaxiPathNode:
                        {
                            TaxiPathNodeHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TotemCategory:
                        {
                            TotemCategoryHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Toy:
                        {
                            ToyHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TransmogHoliday:
                        {
                            TransmogHolidayHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TransmogSet:
                        {
                            TransmogSetHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TransmogSetGroup:
                        {
                            TransmogSetGroupHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TransmogSetItem:
                        {
                            TransmogSetItemHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TransportAnimation:
                        {
                            TransportAnimationHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.TransportRotation:
                        {
                            TransportRotationHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.UiMap:
                        {
                            UiMapHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.UiMapAssignment:
                        {
                            UiMapAssignmentHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.UiMapLink:
                        {
                            UiMapLinkHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.UiMapXMapArt:
                        {
                            UiMapXMapArtHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.UnitCondition:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                UnitConditionHandler441(db2File, (uint)entry, indexes);
                            else
                                UnitConditionHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.UnitPowerBar:
                        {
                            UnitPowerBarHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Vehicle:
                        {
                            VehicleHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.VehicleSeat:
                        {
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                                VehicleSeatHandler441(db2File, (uint)entry, indexes);
                            else
                                VehicleSeatHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.Vignette:
                        {
                            VignetteHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.WmoAreaTable:
                        {
                            WmoAreaTableHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.WorldEffect:
                        {
                            WorldEffectHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.WorldMapOverlay:
                        {
                            WorldMapOverlayHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        case DB2Hash.WorldStateExpression:
                        {
                            WorldStateExpressionHandler440(db2File, (uint)entry, indexes);
                            break;
                        }
                        default:
                        {
                            db2File.WriteLine($"(Entry: {entry} TableHash: {type}) has missing structure. HotfixBlob entry generated!", indexes);
                            db2File.AsHex();
                            db2File.ReadToEnd();

                            HotfixBlob hotfixBlob = new HotfixBlob
                            {
                                TableHash = type,
                                RecordID = entry,
                                Blob = new Blob(db2File.GetStream(0))
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
            int statusBits = ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_0_39185) ? 3 : 2;
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

        [Parser(Opcode.SMSG_AVAILABLE_HOTFIXES)]
        public static void HandleAvailableHotfixes915(Packet packet)
        {
            packet.ReadInt32("VirtualRealmAddress");
            var hotfixCount = packet.ReadUInt32("HotfixCount");
            for (var i = 0u; i < hotfixCount; ++i)
            {
                packet.ReadInt32("PushID", i, "HotfixUniqueID");
                packet.ReadUInt32("UniqueID", i, "HotfixUniqueID");
            }
        }

        [Parser(Opcode.CMSG_HOTFIX_REQUEST)]
        public static void HandleHotfixRequest905(Packet packet)
        {
            packet.ReadUInt32("CurrentBuild");
            packet.ReadUInt32("InternalBuild");
            var hotfixCount = packet.ReadUInt32("HotfixCount");
            for (var i = 0u; i < hotfixCount; ++i)
                packet.ReadInt32("HotfixID", i);
        }

        [Parser(Opcode.CMSG_DB_QUERY_BULK)]
        public static void HandleDbQueryBulk(Packet packet)
        {
            packet.ReadInt32E<DB2Hash>("TableHash");

            var count = packet.ReadBits("Count", 13);
            for (var i = 0; i < count; ++i)
                packet.ReadInt32("RecordID", i);
        }
    }
}

using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class CharacterHandler
    {
        public static PlayerGuidLookupData ReadPlayerGuidLookupData(Packet packet, params object[] idx)
        {
            PlayerGuidLookupData data = new PlayerGuidLookupData();

            packet.ResetBitReader();
            packet.ReadBit("IsDeleted", idx);
            var nameLength = (int)packet.ReadBits(6);

            var count = new int[5];
            for (var i = 0; i < 5; ++i)
                count[i] = (int)packet.ReadBits(7);

            for (var i = 0; i < 5; ++i)
                packet.ReadWoWString("Name Declined", count[i], i, idx);

            packet.ReadPackedGuid128("AccountID", idx);
            packet.ReadPackedGuid128("BnetAccountID", idx);
            packet.ReadPackedGuid128("Player Guid", idx);

            packet.ReadUInt64("GuildClubMemberID", idx);
            packet.ReadUInt32("VirtualRealmAddress", idx);

            data.Race = packet.ReadByteE<Race>("Race", idx);
            data.Gender = packet.ReadByteE<Gender>("Gender", idx);
            data.Class = packet.ReadByteE<Class>("Class", idx);
            data.Level = packet.ReadByte("Level", idx);
            packet.ReadByte("Unused915", idx);
            packet.ReadInt32("TimerunningSeasonID");

            data.Name = packet.ReadWoWString("Name", nameLength, idx);

            return data;
        }

        public static void ReadChrCustomizationChoice(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("ChrCustomizationOptionID", indexes);
            packet.ReadUInt32("ChrCustomizationChoiceID", indexes);
        }

        public static void ReadUnlockedConditionalAppearance(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("AchievementId", indexes);
            packet.ReadUInt32("Unused", indexes);
        }

        public static void ReadRaceLimitDisableInfo(Packet packet, params object[] idx)
        {
            packet.ReadSByteE<Race>("RaceID", idx);
            packet.ReadInt32("BlockReason", idx);
        }

        public static void ReadVisualItemInfo(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("DisplayID", idx);
            packet.ReadByteE<InventoryType>("InvType", idx);
            packet.ReadUInt32("DisplayEnchantID", idx);
            packet.ReadByte("Subclass", idx);
            packet.ReadInt32("SecondaryItemModifiedAppearanceID", idx);
            packet.ReadInt32("ItemID", idx);
            packet.ReadInt32("TransmogrifiedItemID", idx);
        }

        public static void ReadCustomTabardInfo(Packet packet, params object[] idx)
        {
            packet.ReadInt32("EmblemStyle", idx);
            packet.ReadInt32("EmblemColor", idx);
            packet.ReadInt32("BorderStyle", idx);
            packet.ReadInt32("BorderColor", idx);
            packet.ReadInt32("BackgroundColor", idx);
        }

        public static void ReadBasicCharacterListEntry(Packet packet, params object[] idx)
        {
            var playerGuid = packet.ReadPackedGuid128("Guid", idx);
            packet.ReadUInt32("VirtualRealmAddress", idx);
            packet.ReadUInt16("ListPosition", idx);
            var race = packet.ReadByteE<Race>("RaceID", idx);
            packet.ReadByteE<Gender>("SexID", idx);
            var @class = packet.ReadByteE<Class>("ClassID", idx);
            packet.ReadInt16("SpecID", idx);
            var customizationCount = packet.ReadUInt32();
            var level = packet.ReadByte("ExperienceLevel", idx);
            var mapId = packet.ReadInt32<MapId>("MapID", idx);
            var zone = packet.ReadInt32<ZoneId>("ZoneID", idx);
            var pos = packet.ReadVector3("PreloadPos", idx);
            packet.ReadUInt64("GuildClubMemberID", idx);
            packet.ReadPackedGuid128("GuildGUID", idx);
            packet.ReadUInt32("Flags", idx);
            packet.ReadUInt32("Flags2", idx);
            packet.ReadUInt32("Flags3", idx);
            packet.ReadUInt32("Flags4", idx);
            packet.ReadByte("CantLoginReason", idx);
            packet.ReadUInt32("PetCreatureDisplayID", idx);
            packet.ReadUInt32("PetExperienceLevel", idx);
            packet.ReadUInt32("PetCreatureFamilyID", idx);

            for (uint j = 0; j < 19; ++j)
                ReadVisualItemInfo(packet, idx, "VisualItems", j);

            packet.ReadInt32("SaveVersion", idx);
            packet.ReadTime64("LastPlayedTime", idx);
            packet.ReadInt32("LastLoginVersion", idx);

            ReadCustomTabardInfo(packet, idx, "PersonalTabard");

            for (uint j = 0; j < 2; ++j)
                packet.ReadInt32("ProfessionIDs", idx, j);

            packet.ReadInt32("TimerunningSeasonID", idx);
            packet.ReadUInt32("OverrideSelectScreenFileDataID", idx);
            packet.ReadUInt32("Unused1110_1", idx);

            for (var j = 0u; j < customizationCount; ++j)
                ReadChrCustomizationChoice(packet, idx, "Customizations", j);

            packet.ResetBitReader();

            var nameLength = packet.ReadBits(6);
            var firstLogin = packet.ReadBit("FirstLogin", idx);
            packet.ReadBit("Unused1110_2", idx);
            packet.ReadBit("Unused1110_3", idx);

            var name = packet.ReadWoWString("Character Name", nameLength, idx);

            if (firstLogin)
            {
                PlayerCreateInfo startPos = new PlayerCreateInfo { Race = race, Class = @class, Map = (uint)mapId, Zone = (uint)zone, Position = pos, Orientation = 0 };
                Storage.StartPositions.Add(startPos, packet.TimeSpan);
            }

            var playerInfo = new Player { Race = race, Class = @class, Name = name, FirstLogin = firstLogin, Level = level, Type = ObjectType.Player };
            if (Storage.Objects.ContainsKey(playerGuid))
                Storage.Objects[playerGuid] = new Tuple<WoWObject, TimeSpan?>(playerInfo, packet.TimeSpan);
            else
                Storage.Objects.Add(playerGuid, playerInfo, packet.TimeSpan);
        }

        public static void ReadCharacterRestrictionAndMailData(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            packet.ReadBit("BoostInProgress", idx);
            packet.ReadBits("EraChoiceState", 2, idx);
            packet.ReadBit("RpeResetAvailable", idx);
            packet.ReadBit("RpeResetQuestClearAvailable", idx);

            packet.ReadUInt32("RestrictionFlags", idx);
            var mailSenderLengths = new uint[packet.ReadUInt32()];
            var mailSenderTypes = packet.ReadUInt32();

            for (var j = 0; j < mailSenderTypes; ++j)
                packet.ReadUInt32("MailSenderType", idx, j);

            packet.ResetBitReader();

            for (var j = 0; j < mailSenderLengths.Length; ++j)
                mailSenderLengths[j] = packet.ReadBits(6);

            for (var j = 0; j < mailSenderLengths.Length; ++j)
                packet.ReadDynamicString("MailSender", mailSenderLengths[j], idx);
        }

        public static void ReadCharacterListEntry(Packet packet, params object[] idx)
        {
            ReadBasicCharacterListEntry(packet, idx, "Basic");
            ReadCharacterRestrictionAndMailData(packet, idx, "RestrictionsAndMails");
        }

        public static void ReadRegionwideCharacterListEntry(Packet packet, params object[] idx)
        {
            ReadBasicCharacterListEntry(packet, idx, "Basic");
            packet.ReadUInt64("Money", idx);
            packet.ReadSingle("AvgEquippedItemLevel", idx);
            packet.ReadSingle("CurrentSeasonMythicPlusOverallScore", idx);
            packet.ReadInt32("CurrentSeasonBestPvpRating", idx);
            packet.ReadSByte("PvpRatingBracket", idx);
            packet.ReadInt16("PvpRatingAssociatedSpecID", idx);
        }

        public static void ReadRaceUnlockData(Packet packet, params object[] idx)
        {
            packet.ReadByteE<Race>("RaceID", idx);
            packet.ResetBitReader();
            packet.ReadBit("HasExpansion", idx);
            packet.ReadBit("HasAchievement", idx);
            packet.ReadBit("HasHeritageArmor", idx);
            packet.ReadBit("IsLocked", idx);
            packet.ReadBit("Unused1027", idx);
        }

        public static void ReadWarbandGroupMember(Packet packet, params object[] idx)
        {
            packet.ReadInt32("WarbandScenePlacementID", idx);
            var type = packet.ReadInt32("Type", idx);
            if (type == 0)
                packet.ReadPackedGuid128("Guid", idx);
        }

        public static void ReadWarbandGroup(Packet packet, params object[] idx)
        {
            packet.ReadUInt64("GroupID", idx);
            packet.ReadByte("OrderIndex", idx);
            packet.ReadInt32("WarbandSceneID", idx);
            packet.ReadInt32("Flags", idx);
            var memberCount = packet.ReadUInt32();
            for (var i = 0u; i < memberCount; ++i)
                ReadWarbandGroupMember(packet, idx, "Members", i);

            var nameLength = packet.ReadBits(9);
            packet.ReadWoWString("Name", nameLength, idx);
        }

        public static void ReadAzeriteEssenceData(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("Index", idx);
            packet.ReadUInt32("AzeriteEssenceID", idx);
            packet.ReadUInt32("Rank", idx);
            packet.ReadBit("SlotUnlocked", idx);
            packet.ResetBitReader();
        }

        public static void ReadInspectItemData(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("CreatorGUID", idx);
            packet.ReadByte("Index", idx);

            var azeritePowerCount = packet.ReadUInt32("AzeritePowersCount", idx);
            var azeriteEssenceCount = packet.ReadUInt32("AzeriteEssenceCount", idx);

            for (int j = 0; j < azeritePowerCount; j++)
                packet.ReadInt32("AzeritePowerId", idx, j);

            Substructures.ItemHandler.ReadItemInstance(packet, idx);

            packet.ReadBit("Usable", idx);
            var enchantsCount = packet.ReadBits("EnchantsCount", 4, idx);
            var gemsCount = packet.ReadBits("GemsCount", 2, idx);
            packet.ResetBitReader();

            for (int i = 0; i < azeriteEssenceCount; i++)
                ReadAzeriteEssenceData(packet, "AzeriteEssence", i);

            for (int i = 0; i < enchantsCount; i++)
            {
                packet.ReadUInt32("Id", idx, i);
                packet.ReadByte("Index", idx, i);
            }

            for (int i = 0; i < gemsCount; i++)
            {
                packet.ReadByte("Slot", idx, i);
                Substructures.ItemHandler.ReadItemInstance(packet, idx, i);
            }
        }

        public static void ReadPlayerModelDisplayInfo(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("InspecteeGUID", idx);
            packet.ReadInt32("SpecializationID", idx);
            var itemCount = packet.ReadUInt32();
            var nameLen = packet.ReadBits(6);
            packet.ResetBitReader();
            packet.ReadByteE<Gender>("GenderID", idx);
            packet.ReadByteE<Race>("Race", idx);
            packet.ReadByteE<Class>("ClassID", idx);
            var customizationCount = packet.ReadUInt32();
            packet.ReadWoWString("Name", nameLen, idx);

            for (var j = 0u; j < customizationCount; ++j)
                ReadChrCustomizationChoice(packet, idx, "Customizations", j);

            for (int i = 0; i < itemCount; i++)
                ReadInspectItemData(packet, idx, i);
        }

        public static void ReadPVPBracketData(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            packet.ReadByte("Bracket", idx);
            packet.ReadInt32("RatingID", idx);
            packet.ReadInt32("Rating", idx);
            packet.ReadInt32("Rank", idx);
            packet.ReadInt32("WeeklyPlayed", idx);
            packet.ReadInt32("WeeklyWon", idx);
            packet.ReadInt32("SeasonPlayed", idx);
            packet.ReadInt32("SeasonWon", idx);
            packet.ReadInt32("WeeklyBestRating", idx);
            packet.ReadInt32("LastWeeksBestRating", idx);
            packet.ReadInt32("Tier", idx);
            packet.ReadInt32("WeeklyBestTier", idx);
            packet.ReadInt32("SeasonBestRating", idx);
            packet.ReadByte("SeasonBestTierEnum", idx);
            packet.ReadInt32("RoundsSeasonPlayed", idx);
            packet.ReadInt32("RoundsSeasonWon", idx);
            packet.ReadInt32("RoundsWeeklyPlayed", idx);
            packet.ReadInt32("RoundsWeeklyWon", idx);

            packet.ReadBit("Disqualified", idx);
        }

        [Parser(Opcode.SMSG_FAILED_PLAYER_CONDITION)]
        public static void HandleFailedPlayerCondition(Packet packet)
        {
            packet.ReadInt32("Id");
        }

        [Parser(Opcode.SMSG_SET_PLAYER_DECLINED_NAMES_RESULT)]
        public static void HandleSetPlayerDeclinedNamesResult(Packet packet)
        {
            packet.ReadInt32("ResultCode");
            packet.ReadPackedGuid128("Player");
        }

        [Parser(Opcode.SMSG_PLAYER_AZERITE_ITEM_GAINS)]
        public static void HandlePlayerAzeriteItemGains(Packet packet)
        {
            packet.ReadPackedGuid128("Item");
            packet.ReadUInt64("AzeriteXPGained");
        }

        [Parser(Opcode.SMSG_PLAYER_AZERITE_ITEM_EQUIPPED_STATUS_CHANGED)]
        public static void HandlePlayerAzeriteItemEquippedStatusChanged(Packet packet)
        {
            packet.ReadBit("IsHeartEquipped");
        }

        [Parser(Opcode.SMSG_INSPECT_HONOR_STATS)]
        public static void HandleInspectHonorStats(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadByte("LifetimeMaxRank");
            packet.ReadInt16("SessionHK");
            packet.ReadInt16("SessionDK");
            packet.ReadInt16("YesterdayHK");
            packet.ReadInt16("YesterdayDK");
            packet.ReadInt16("LastWeekHK");
            packet.ReadInt16("LastWeekDK");
            packet.ReadInt16("ThisWeekHK");
            packet.ReadInt16("ThisWeekDK");
            packet.ReadInt32("LifetimeHK");
            packet.ReadInt32("LifetimeDK");
            packet.ReadInt32("YesterdayHonor");
            packet.ReadInt32("LastWeekHonor");
            packet.ReadInt32("ThisWeekHonor");
            packet.ReadInt32("LastweekStanding");
            packet.ReadByte("RankProgress");
        }

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT)]
        public static void HandleEnumCharactersResult(Packet packet)
        {
            packet.ReadBit("Success");
            packet.ReadBit("Realmless");
            packet.ReadBit("IsDeletedCharacters");
            packet.ReadBit("IsNewPlayerRestrictionSkipped");
            packet.ReadBit("IsNewPlayerRestricted");
            packet.ReadBit("IsNewPlayer");
            packet.ReadBit("IsTrialAccountRestricted");
            var hasDisabledClassesMask = packet.ReadBit("HasDisabledClassesMask");

            packet.ReadBit("DontCreateCharacterDisplays");

            var charsCount = packet.ReadUInt32("CharactersCount");
            var regionwideCharsCount = packet.ReadUInt32("RegionwideCharactersCount");
            packet.ReadInt32("MaxCharacterLevel");
            var raceUnlockCount = packet.ReadUInt32("RaceUnlockCount");
            var unlockedConditionalAppearanceCount = packet.ReadUInt32("UnlockedConditionalAppearanceCount");
            var raceLimitDisablesCount = packet.ReadUInt32("RaceLimitDisablesCount");
            var warbandGroupsCount = packet.ReadUInt32("WarbandGroupsCount");

            if (hasDisabledClassesMask)
                packet.ReadUInt32("DisabledClassesMask");

            for (var i = 0u; i < unlockedConditionalAppearanceCount; ++i)
                ReadUnlockedConditionalAppearance(packet, "UnlockedConditionalAppearances", i);

            for (var i = 0u; i < raceLimitDisablesCount; i++)
                ReadRaceLimitDisableInfo(packet, "RaceLimitDisableInfo", i);

            for (var i = 0u; i < charsCount; ++i)
                ReadCharacterListEntry(packet, i, "Characters");

            for (var i = 0u; i < regionwideCharsCount; ++i)
                ReadRegionwideCharacterListEntry(packet, i, "RegionwideCharacters");

            for (var i = 0u; i < raceUnlockCount; ++i)
                ReadRaceUnlockData(packet, i, "RaceUnlockData");

            for (var i = 0u; i < warbandGroupsCount; ++i)
                ReadWarbandGroup(packet, i, "WarbandGroups");
        }

        [Parser(Opcode.SMSG_CHECK_CHARACTER_NAME_AVAILABILITY_RESULT)]
        public static void HandleCheckCharacterNameAvailabilityResult(Packet packet)
        {
            packet.ReadUInt32("SequenceIndex");
            packet.ReadUInt32("Result");
        }

        [Parser(Opcode.SMSG_XP_GAIN_ABORTED)]
        public static void HandleXPGainAborted(Packet packet)
        {
            packet.ReadPackedGuid128("Victim");

            packet.ReadInt32("XpToAdd");
            packet.ReadInt32("XpGainReason");
            packet.ReadInt32("XpAbortReason");
        }

        [Parser(Opcode.SMSG_NEUTRAL_PLAYER_FACTION_SELECT_RESULT)]
        public static void HandleNeutralPlayerFactionSelectResult(Packet packet)
        {
            packet.ReadBit("Success");
            packet.ReadByteE<Race>("NewRaceID");
        }

        [Parser(Opcode.SMSG_INSPECT_RESULT)]
        public static void HandleInspectResult(Packet packet)
        {
            ReadPlayerModelDisplayInfo(packet, "DisplayInfo");
            var glyphCount = packet.ReadUInt32("GlyphsCount");
            var talentCount = packet.ReadUInt32("TalentsCount");
            var pvpTalentCount = packet.ReadUInt32("PvpTalentsCount");
            packet.ReadInt32("ItemLevel");
            packet.ReadByte("LifetimeMaxRank");
            packet.ReadUInt16("TodayHK");
            packet.ReadUInt16("YesterdayHK");
            packet.ReadUInt32("LifetimeHK");
            packet.ReadUInt32("HonorLevel");

            for (int i = 0; i < glyphCount; i++)
                packet.ReadUInt16("Glyphs", i);

            for (int i = 0; i < talentCount; i++)
                packet.ReadUInt16("Talents", i);

            for (int i = 0; i < pvpTalentCount; i++)
                packet.ReadUInt16("PvpTalents", i);

            SpellHandler.ReadTalentInfoUpdateClassic(packet, "TalentInfo");

            packet.ResetBitReader();
            var hasGuildData = packet.ReadBit("HasGuildData");
            var hasAzeriteLevel = packet.ReadBit("HasAzeriteLevel");

            for (int i = 0; i < 9; i++)
                ReadPVPBracketData(packet, i, "PVPBracketData");

            if (hasGuildData)
            {
                packet.ReadPackedGuid128("GuildGUID");
                packet.ReadInt32("NumGuildMembers");
                packet.ReadInt32("GuildAchievementPoints");
            }
            if (hasAzeriteLevel)
                packet.ReadInt32("AzeriteLevel");

            packet.ReadInt32("Level", "TraitInspectData");
            packet.ReadInt32("ChrSpecializationID", "TraitInspectData");
            TraitHandler.ReadTraitConfig(packet, "TraitInspectData", "Traits");
        }

        [Parser(Opcode.SMSG_PLAYER_CHOICE_CLEAR)]
        [Parser(Opcode.SMSG_SHOW_NEUTRAL_PLAYER_FACTION_SELECT_UI)]
        public static void HandleCharacterEmpty(Packet packet)
        {
        }
    }
}

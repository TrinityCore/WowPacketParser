using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V11_0_0_55666.Parsers
{
    public static class CharacterHandler
    {
        public static void ReadVisualItemInfo(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("DisplayID", idx);
            packet.ReadByteE<InventoryType>("InvType", idx);
            packet.ReadUInt32("DisplayEnchantID", idx);
            packet.ReadByte("Subclass", idx);
            packet.ReadInt32("SecondaryItemModifiedAppearanceID", idx);
            packet.ReadInt32("ItemID", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_2_55959))
                packet.ReadInt32("TransmogrifiedItemID", idx);
        }

        public static void ReadBasicCharacterListEntry(Packet packet, params object[] idx)
        {
            var playerGuid = packet.ReadPackedGuid128("Guid", idx);
            packet.ReadUInt32("VirtualRealmAddress", idx);
            packet.ReadByte("ListPosition", idx);
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
            packet.ReadByte("UnkWod61x", idx);
            packet.ReadUInt32("PetCreatureDisplayID", idx);
            packet.ReadUInt32("PetExperienceLevel", idx);
            packet.ReadUInt32("PetCreatureFamilyID", idx);

            for (uint j = 0; j < 19; ++j)
                ReadVisualItemInfo(packet, idx, "VisualItems", j);

            packet.ReadInt32("Unknown703", idx);
            packet.ReadTime64("LastPlayedTime", idx);
            packet.ReadInt32("LastLoginVersion", idx);

            V9_0_1_36216.Parsers.CharacterHandler.ReadCustomTabardInfo(packet, idx, "PersonalTabard");

            for (uint j = 0; j < 2; ++j)
                packet.ReadInt32("ProfessionIDs", idx, j);

            packet.ReadInt32("TimerunningSeasonID", idx);
            packet.ReadUInt32("OverrideSelectScreenFileDataID", idx);

            for (var j = 0u; j < customizationCount; ++j)
                V9_0_1_36216.Parsers.CharacterHandler.ReadChrCustomizationChoice(packet, idx, "Customizations", j);

            packet.ResetBitReader();

            var nameLength = packet.ReadBits(6);
            var firstLogin = packet.ReadBit("FirstLogin", idx);

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
            packet.ReadBit("RpeResetAvailable", idx);
            packet.ReadBit("RpeResetQuestClearAvailable", idx);

            packet.ReadUInt32("Flags4", idx);
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
            packet.ReadSingle("CurrentSeasonMythicPlusOverallScore", idx);
            packet.ReadInt32("CurrentSeasonBestPvpRating", idx);
            packet.ReadSByte("PvpRatingBracket", idx);
            packet.ReadInt16("PvpRatingAssociatedSpecID", idx);
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
            packet.ReadByte("Unknown_1100", idx);
            packet.ReadInt32("Flags", idx);
            var memberCount = packet.ReadUInt32();
            for (var i = 0u; i < memberCount; ++i)
                ReadWarbandGroupMember(packet, idx, "Members", i);
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
                V8_0_1_27101.Parsers.CharacterHandler.ReadUnlockedConditionalAppearance(packet, "UnlockedConditionalAppearances", i);

            for (var i = 0u; i < raceLimitDisablesCount; i++)
                V9_0_1_36216.Parsers.CharacterHandler.ReadRaceLimitDisableInfo(packet, "RaceLimitDisableInfo", i);

            for (var i = 0u; i < warbandGroupsCount; ++i)
                ReadWarbandGroup(packet, i, "WarbandGroups");

            for (var i = 0u; i < charsCount; ++i)
                ReadCharacterListEntry(packet, i, "Characters");

            for (var i = 0u; i < regionwideCharsCount; ++i)
                ReadRegionwideCharacterListEntry(packet, i, "RegionwideCharacters");

            for (var i = 0u; i < raceUnlockCount; ++i)
                V7_0_3_22248.Parsers.CharacterHandler.ReadRaceUnlockData(packet, i, "RaceUnlockData");

        }
    }
}

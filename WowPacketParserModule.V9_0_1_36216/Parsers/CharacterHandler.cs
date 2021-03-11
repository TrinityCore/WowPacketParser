using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class CharacterHandler
    {
        public static void ReadChrCustomizationChoice(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("ChrCustomizationOptionID", indexes);
            packet.ReadUInt32("ChrCustomizationChoiceID", indexes);
        }

        public static void ReadCharactersListEntry(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("Guid", idx);

            packet.ReadUInt64("GuildClubMemberID", idx);

            packet.ReadByte("ListPosition", idx);
            var race = packet.ReadByteE<Race>("RaceID", idx);
            var @class = packet.ReadByteE<Class>("ClassID", idx);
            packet.ReadByteE<Gender>("SexID", idx);
            var customizationCount = packet.ReadUInt32();

            packet.ReadByte("ExperienceLevel", idx);
            var zone = packet.ReadInt32<ZoneId>("ZoneID", idx);
            var mapId = packet.ReadInt32<MapId>("MapID", idx);

            var pos = packet.ReadVector3("PreloadPos", idx);

            packet.ReadPackedGuid128("GuildGUID", idx);

            packet.ReadUInt32("Flags", idx);
            packet.ReadUInt32("Flags2", idx);
            packet.ReadUInt32("Flags3", idx);
            packet.ReadUInt32("PetCreatureDisplayID", idx);
            packet.ReadUInt32("PetExperienceLevel", idx);
            packet.ReadUInt32("PetCreatureFamilyID", idx);

            for (uint j = 0; j < 2; ++j)
                packet.ReadInt32("ProfessionIDs", idx, j);

            for (uint j = 0; j < 23; ++j)
            {
                packet.ReadUInt32("DisplayID", idx, "VisualItems", j);
                packet.ReadUInt32("DisplayEnchantID", idx, "VisualItems", j);
                packet.ReadInt32("ItemModifiedAppearanceID", idx, "VisualItems", j);
                packet.ReadByteE<InventoryType>("InvType", idx, "VisualItems", j);
                packet.ReadByte("Subclass", idx, "VisualItems", j);
            }

            packet.ReadTime("LastPlayedTime", idx);

            packet.ReadInt16("SpecID", idx);
            packet.ReadInt32("Unknown703", idx);
            packet.ReadInt32("InterfaceVersion", idx);
            packet.ReadUInt32("Flags4", idx);
            var mailSenderLengths = new uint[packet.ReadUInt32()];
            var mailSenderTypes = ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_2_36639) ? new uint[packet.ReadUInt32()] : Array.Empty<uint>();
            packet.ReadUInt32("OverrideSelectScreenFileDataID", idx);

            for (var j = 0u; j < customizationCount; ++j)
                ReadChrCustomizationChoice(packet, idx, "Customizations", j);

            for (var j = 0; j < mailSenderTypes.Length; ++j)
                packet.ReadUInt32("MailSenderType", idx, j);

            packet.ResetBitReader();

            var nameLength = packet.ReadBits("Character Name Length", 6, idx);
            var firstLogin = packet.ReadBit("FirstLogin", idx);
            packet.ReadBit("BoostInProgress", idx);
            packet.ReadBits("UnkWod61x", 5, idx);

            for (var j = 0; j < mailSenderLengths.Length; ++j)
                mailSenderLengths[j] = packet.ReadBits(6);

            for (var j = 0; j < mailSenderLengths.Length; ++j)
                if (mailSenderLengths[j] > 1)
                    packet.ReadDynamicString("MailSender", mailSenderLengths[j], idx);

            packet.ReadWoWString("Character Name", nameLength, idx);

            if (firstLogin)
            {
                PlayerCreateInfo startPos = new PlayerCreateInfo { Race = race, Class = @class, Map = (uint)mapId, Zone = (uint)zone, Position = pos, Orientation = 0 };
                Storage.StartPositions.Add(startPos, packet.TimeSpan);
            }
        }

        public static void ReadPlayerModelDisplayInfo(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("InspecteeGUID", idx);
            packet.ReadInt32("SpecializationID", idx);
            var itemCount = packet.ReadUInt32();
            packet.ResetBitReader();
            var nameLen = packet.ReadBits(6);
            packet.ReadByteE<Gender>("GenderID", idx);
            packet.ReadByteE<Race>("Race", idx);
            packet.ReadByteE<Class>("ClassID", idx);
            var customizationCount = packet.ReadUInt32();
            packet.ReadWoWString("Name", nameLen, idx);

            for (var j = 0u; j < customizationCount; ++j)
                ReadChrCustomizationChoice(packet, idx, "Customizations", j);

            for (int i = 0; i < itemCount; i++)
                V8_0_1_27101.Parsers.CharacterHandler.ReadInspectItemData(packet, idx, i);
        }

        [Parser(Opcode.SMSG_PLAYER_CHOICE_CLEAR)]
        public static void HandleEmpty(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT)]
        public static void HandleEnumCharactersResult(Packet packet)
        {
            packet.ReadBit("Success");
            packet.ReadBit("IsDeletedCharacters");
            packet.ReadBit("IsNewPlayerRestrictionSkipped");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_2_36639))
                packet.ReadBit("IsNewPlayerRestricted");

            packet.ReadBit("IsNewPlayer");

            var hasDisabledClassesMask = packet.ReadBit("HasDisabledClassesMask");
            packet.ReadBit("IsAlliedRacesCreationAllowed");

            var charsCount = packet.ReadUInt32("CharactersCount");
            packet.ReadInt32("MaxCharacterLevel");
            var raceUnlockCount = packet.ReadUInt32("RaceUnlockCount");
            var unlockedConditionalAppearanceCount = packet.ReadUInt32("UnlockedConditionalAppearanceCount");

            if (hasDisabledClassesMask)
                packet.ReadUInt32("DisabledClassesMask");

            for (var i = 0u; i < unlockedConditionalAppearanceCount; ++i)
                V8_0_1_27101.Parsers.CharacterHandler.ReadUnlockedConditionalAppearance(packet, "UnlockedConditionalAppearances", i);

            for (var i = 0u; i < charsCount; ++i)
                ReadCharactersListEntry(packet, i, "Characters");

            for (var i = 0u; i < raceUnlockCount; ++i)
                V7_0_3_22248.Parsers.CharacterHandler.ReadRaceUnlockData(packet, i, "RaceUnlockData");
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

            packet.ResetBitReader();
            var hasGuildData = packet.ReadBit("HasGuildData");
            var hasAzeriteLevel = packet.ReadBit("HasAzeriteLevel");

            for (int i = 0; i < 6; i++)
            {
                packet.ReadByte("Bracket", i);
                packet.ReadInt32("Rating", i);
                packet.ReadInt32("Rank", i);
                packet.ReadInt32("WeeklyPlayed", i);
                packet.ReadInt32("WeeklyWon", i);
                packet.ReadInt32("SeasonPlayed", i);
                packet.ReadInt32("SeasonWon", i);
                packet.ReadInt32("WeeklyBestRating", i);
                packet.ReadInt32("Unk710", i);
                packet.ReadInt32("Unk801_1", i);
                packet.ResetBitReader();
                packet.ReadBit("Unk801_2", i);
            }

            if (hasGuildData)
            {
                packet.ReadPackedGuid128("GuildGUID");
                packet.ReadInt32("NumGuildMembers");
                packet.ReadInt32("GuildAchievementPoints");
            }
            if (hasAzeriteLevel)
                packet.ReadInt32("AzeriteLevel");
        }

        [Parser(Opcode.CMSG_CREATE_CHARACTER)]
        public static void HandleClientCharCreate(Packet packet)
        {
            var nameLen = packet.ReadBits(6);
            var hasTemplateSet = packet.ReadBit("HasTemplateSet");
            packet.ReadBit("IsTrialBoost");
            packet.ReadBit("UseNPE");

            packet.ReadByteE<Race>("RaceID");
            packet.ReadByteE<Class>("ClassID");
            packet.ReadByteE<Gender>("SexID");

            var customizationCount = packet.ReadUInt32();

            packet.ReadWoWString("Name", nameLen);

            if (hasTemplateSet)
                packet.ReadInt32("TemplateSetID");

            for (var i = 0u; i < customizationCount; ++i)
                ReadChrCustomizationChoice(packet, "Customizations", i);
        }
      
        [Parser(Opcode.SMSG_CHECK_CHARACTER_NAME_AVAILABILITY_RESULT)]
        public static void HandleCheckCharacterNameAvailabilityResult(Packet packet)
        {
            packet.ReadUInt32("SequenceIndex");
            packet.ReadUInt32("Result");
        }

        [Parser(Opcode.CMSG_CHECK_CHARACTER_NAME_AVAILABILITY)]
        public static void HandleCheckCharacterNameAvailability(Packet packet)
        {
            packet.ReadUInt32("SequenceIndex");
            packet.ReadWoWString("Character Name", packet.ReadBits(6));
        }
    }
}

using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class CharacterHandler
    {
        [Parser(Opcode.SMSG_QUERY_PLAYER_NAME_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            var hasData = packet.ReadByte("HasData");

            packet.ReadPackedGuid128("Player Guid");

            if (hasData == 0)
            {
                packet.ReadBit("IsDeleted");
                var bits15 = (int)packet.ReadBits(6);

                var count = new int[5];
                for (var i = 0; i < 5; ++i)
                    count[i] = (int)packet.ReadBits(7);

                for (var i = 0; i < 5; ++i)
                    packet.ReadWoWString("Name Declined", count[i], i);

                packet.ReadPackedGuid128("AccountID");
                packet.ReadPackedGuid128("BnetAccountID");
                packet.ReadPackedGuid128("Player Guid");

                packet.ReadUInt64("GuildClubMemberID");
                packet.ReadUInt32("VirtualRealmAddress");

                packet.ReadByteE<Race>("Race");
                packet.ReadByteE<Gender>("Gender");
                packet.ReadByteE<Class>("Class");
                packet.ReadByte("Level");

                packet.ReadWoWString("Name", bits15);
            }
        }

        public static void ReadCharactersData(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("Guid", idx);

            packet.ReadUInt64("GuildClubMemberID", idx);

            packet.ReadByte("ListPosition", idx);
            var race = packet.ReadByteE<Race>("RaceID", idx);
            var klass = packet.ReadByteE<Class>("ClassID", idx);
            packet.ReadByteE<Gender>("SexID", idx);
            packet.ReadByte("SkinID", idx);
            packet.ReadByte("FaceID", idx);
            packet.ReadByte("HairStyle", idx);
            packet.ReadByte("HairColor", idx);
            packet.ReadByte("FacialHairStyle", idx);

            for (uint j = 0; j < 3; ++j)
                packet.ReadByte("CustomDisplay", idx, j);

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
                packet.ReadUInt32("InventoryItem DisplayID", idx, j);
                packet.ReadUInt32("InventoryItem DisplayEnchantID", idx, j);
                packet.ReadByteE<InventoryType>("InventoryItem InvType", idx, j);
            }

            packet.ReadTime("LastPlayedTime", idx);

            packet.ReadInt16("SpecID", idx);
            packet.ReadInt32("Unknown703", idx);
            packet.ReadInt32("LastLoginBuild", idx);
            packet.ReadUInt32("Flags4", idx);

            packet.ResetBitReader();

            var nameLength = packet.ReadBits("Character Name Length", 6, idx);
            var firstLogin = packet.ReadBit("FirstLogin", idx);
            packet.ReadBit("BoostInProgress", idx);
            packet.ReadBits("UnkWod61x", 5, idx);

            packet.ReadWoWString("Character Name", nameLength, idx);

            if (firstLogin)
            {
                PlayerCreateInfo startPos = new PlayerCreateInfo { Race = race, Class = klass, Map = (uint)mapId, Zone = (uint)zone, Position = pos, Orientation = 0 };
                Storage.StartPositions.Add(startPos, packet.TimeSpan);
            }
        }

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT)]
        public static void HandleEnumCharactersResult(Packet packet)
        {
            packet.ReadBit("Success");
            packet.ReadBit("IsDeletedCharacters");
            packet.ReadBit("IsTestDemonHunterCreationAllowed");
            packet.ReadBit("HasDemonHunterOnRealm");
            packet.ReadBit("IsDemonHunterCreationAllowed");

            var hasDisabledClassesMask = packet.ReadBit("HasDisabledClassesMask");
            packet.ReadBit("IsAlliedRacesCreationAllowed");

            var charsCount = packet.ReadUInt32("CharactersCount");
            packet.ReadInt32("MaxCharacterLevel");
            var raceUnlockCount = packet.ReadUInt32("RaceUnlockCount");

            uint unkCount = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                unkCount = packet.ReadUInt32("UnkCount_810");

            if (hasDisabledClassesMask)
                packet.ReadUInt32("DisabledClassesMask");

            for (uint i = 0; i < unkCount; ++i)
            {
                packet.ReadUInt32("UnkUInt32_1", i);
                packet.ReadUInt32("UnkUInt32_2", i);
            }

            for (uint i = 0; i < charsCount; ++i)
                ReadCharactersData(packet, i, "CharactersData");

            for (var i = 0; i < raceUnlockCount; ++i)
                V7_0_3_22248.Parsers.CharacterHandler.ReadRaceUnlockData(packet, i, "RaceUnlockData");
        }

        [Parser(Opcode.SMSG_INSPECT_RESULT, ClientVersionBuild.V8_0_1_27101, ClientVersionBuild.V8_1_5_29683)]
        public static void HandleInspectResult(Packet packet)
        {
            packet.ReadPackedGuid128("InspecteeGUID");

            var itemCount = packet.ReadUInt32("ItemsCount");
            var glyphCount = packet.ReadUInt32("GlyphsCount");
            var talentCount = packet.ReadUInt32("TalentsCount");
            var pvpTalentCount = packet.ReadUInt32("PvpTalentsCount");
            packet.ReadInt32E<Class>("ClassID");
            packet.ReadInt32("SpecializationID");
            packet.ReadInt32E<Gender>("Gender");

            for (int i = 0; i < glyphCount; i++)
                packet.ReadUInt16("Glyphs", i);

            for (int i = 0; i < talentCount; i++)
                packet.ReadUInt16("Talents", i);

            for (int i = 0; i < pvpTalentCount; i++)
                packet.ReadUInt16("PvpTalents", i);

            packet.ResetBitReader();
            var hasGuildData = packet.ReadBit("HasGuildData");
            var hasAzeriteLevel = packet.ReadBit("HasAzeriteLevel");

            for (int i = 0; i < itemCount; i++)
            {
                packet.ReadPackedGuid128("CreatorGUID", i);
                packet.ReadByte("Index", i);

                var azeritePowerCount = packet.ReadUInt32("AzeritePowersCount", i);

                for (int j = 0; j < azeritePowerCount; j++)
                    packet.ReadInt32("AzeritePowerId", i, j);

                V6_0_2_19033.Parsers.ItemHandler.ReadItemInstance(packet, i);

                packet.ResetBitReader();
                packet.ReadBit("Usable", i);
                var enchantsCount = packet.ReadBits("EnchantsCount", 4, i);
                var gemsCount = packet.ReadBits("GemsCount", 2, i);

                for (int j = 0; j < gemsCount; j++)
                {
                    packet.ReadByte("Slot", i, j);
                    V6_0_2_19033.Parsers.ItemHandler.ReadItemInstance(packet, i, j);
                }

                for (int j = 0; j < enchantsCount; j++)
                {
                    packet.ReadUInt32("Id", i, j);
                    packet.ReadByte("Index", i, j);
                }
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

        [Parser(Opcode.SMSG_INSPECT_RESULT, ClientVersionBuild.V8_1_5_29683)]
        public static void HandleInspectResult815(Packet packet)
        {
            packet.ReadPackedGuid128("InspecteeGUID");

            var itemCount = packet.ReadUInt32("ItemsCount");
            var glyphCount = packet.ReadUInt32("GlyphsCount");
            var talentCount = packet.ReadUInt32("TalentsCount");
            var pvpTalentCount = packet.ReadUInt32("PvpTalentsCount");
            packet.ReadInt32E<Class>("ClassID");
            packet.ReadInt32("SpecializationID");
            packet.ReadInt32E<Gender>("Gender");
            packet.ReadByte("UnkByte_0");
            packet.ReadUInt16("UnkUint16_0");
            packet.ReadUInt16("UnkUint16_1");
            packet.ReadUInt32("UnkUint16_2");
            packet.ReadUInt32("UnkUint16_3");

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
                packet.ReadByte("UnkByte_1", i);
                packet.ReadInt32("UnkInt32_0", i);
                packet.ReadInt32("UnkInt32_1", i);
                packet.ReadInt32("UnkInt32_2", i);
                packet.ReadInt32("UnkInt32_3", i);
                packet.ReadInt32("UnkInt32_4", i);
                packet.ReadInt32("UnkInt32_5", i);
                packet.ReadInt32("UnkInt32_6", i);
                packet.ReadInt32("UnkInt32_7", i);
                packet.ReadInt32("UnkInt32_8", i);
                packet.ResetBitReader();
                packet.ReadBit("UnkBool_0", i);
                packet.ReadBit("UnkBool_1", i);
            }

            if (hasGuildData)
            {
                packet.ReadPackedGuid128("GuildGUID");
                packet.ReadInt32("NumGuildMembers");
                packet.ReadInt32("GuildAchievementPoints");
            }
            if (hasAzeriteLevel)
                packet.ReadInt32("AzeriteLevel");

            for (int i = 0; i < itemCount; i++)
            {
                packet.ReadPackedGuid128("CreatorGUID", i);
                packet.ReadByte("Index", i);

                var azeritePowerCount = packet.ReadUInt32("AzeritePowersCount", i);

                for (int j = 0; j < azeritePowerCount; j++)
                    packet.ReadInt32("AzeritePowerId", i, j);

                ItemHandler.ReadItemInstance815(packet, i);

                packet.ResetBitReader();
                packet.ReadBit("Usable", i);
                var enchantsCount = packet.ReadBits("EnchantsCount", 4, i);
                var gemsCount = packet.ReadBits("GemsCount", 2, i);

                for (int j = 0; j < gemsCount; j++)
                {
                    packet.ReadByte("Slot", i, j);
                    ItemHandler.ReadItemInstance815(packet, i, j);
                }

                for (int j = 0; j < enchantsCount; j++)
                {
                    packet.ReadUInt32("Id", i, j);
                    packet.ReadByte("Index", i, j);
                }
            }
        }

        [Parser(Opcode.CMSG_CREATE_CHARACTER)]
        public static void HandleClientCharCreate(Packet packet)
        {
            var nameLen = packet.ReadBits(6);
            var hasTemplateSet = packet.ReadBit();

            packet.ReadBit("IsTrialBoost");
            packet.ReadByteE<Race>("RaceID");
            packet.ReadByteE<Class>("ClassID");
            packet.ReadByteE<Gender>("SexID");
            packet.ReadByte("SkinID");
            packet.ReadByte("FaceID");
            packet.ReadByte("HairStyleID");
            packet.ReadByte("HairColorID");
            packet.ReadByte("FacialHairStyleID");
            packet.ReadByte("OutfitID");

            packet.ReadWoWString("Name", nameLen);

            if (hasTemplateSet)
                packet.ReadInt32("TemplateSetID");
        }

        [Parser(Opcode.SMSG_CREATE_CHAR)]
        public static void HandleCharResponse(Packet packet)
        {
            packet.ReadByteE<ResponseCode>("Response");
            packet.ReadPackedGuid128("GUID");
        }

        public static void ReadPVPBracketData(Packet packet, params object[] idx)
        {
            packet.ReadByte("Bracket", idx);
            packet.ReadInt32("Rating", idx);
            packet.ReadInt32("Rank", idx);
            packet.ReadInt32("WeeklyPlayed", idx);
            packet.ReadInt32("WeeklyWon", idx);
            packet.ReadInt32("SeasonPlayed", idx);
            packet.ReadInt32("SeasonWon", idx);
            packet.ReadInt32("WeeklyBestRating", idx);
            packet.ReadInt32("Unk710");
            packet.ReadInt32("Unk801");
            packet.ReadBit("Unk801_Bit");
        }

        [Parser(Opcode.SMSG_INSPECT_PVP)]
        public static void HandleInspectPVP(Packet packet)
        {
            packet.ReadPackedGuid128("ClientGUID");

            var bracketCount = packet.ReadBits(3);
            for (var i = 0; i < bracketCount; i++)
                ReadPVPBracketData(packet, i, "PVPBracketData");
        }

        [Parser(Opcode.SMSG_LEVEL_UP_INFO)]
        public static void HandleLevelUpInfo(Packet packet)
        {
            packet.ReadInt32("Level");
            packet.ReadInt32("HealthDelta");

            for (var i = 0; i < 6; i++)
                packet.ReadInt32("PowerDelta", (PowerType)i);

            for (var i = 0; i < 4; i++)
                packet.ReadInt32("StatDelta", (StatType)i);

            packet.ReadInt32("NumNewTalents");
            packet.ReadInt32("NumNewPvpTalentSlots");
        }

        [Parser(Opcode.SMSG_AZERITE_XP_GAIN)]
        public static void HandleAzeriteXpGain(Packet packet)
        {
            packet.ReadPackedGuid128("Item");
            packet.ReadUInt64("AzeriteXPGained");
        }
    }
}

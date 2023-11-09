using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class CharacterHandler
    {
        public static void ReadCharactersData(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("Guid", idx);

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
            var zone = packet.ReadUInt32<ZoneId>("ZoneID", idx);
            var mapId = packet.ReadUInt32<MapId>("MapID", idx);

            var pos = packet.ReadVector3("PreloadPos", idx);

            packet.ReadPackedGuid128("GuildGUID", idx);

            packet.ReadUInt32("Flags", idx);
            packet.ReadUInt32("Flags2", idx);
            packet.ReadUInt32("Flags3", idx);
            packet.ReadUInt32("PetCreatureDisplayID", idx);
            packet.ReadUInt32("PetExperienceLevel", idx);
            packet.ReadUInt32("PetCreatureFamilyID", idx);

            for (uint j = 0; j < 2; ++j)
                packet.ReadUInt32("ProfessionIDs", idx, j);

            for (uint j = 0; j < 23; ++j)
            {
                packet.ReadUInt32("InventoryItem DisplayID", idx, j);
                packet.ReadUInt32("InventoryItem DisplayEnchantID", idx, j);
                packet.ReadByteE<InventoryType>("InventoryItem InvType", idx, j);
            }

            packet.ReadTime("LastPlayedTime", idx);

            packet.ReadUInt16("SpecID", idx);
            packet.ReadUInt32("Unknown703", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_5_25848))
                packet.ReadUInt32("LastLoginVersion", idx);
            packet.ReadUInt32("Flags4", idx);

            packet.ResetBitReader();

            var nameLength = packet.ReadBits("Character Name Length", 6, idx);
            var firstLogin = packet.ReadBit("FirstLogin", idx);
            packet.ReadBit("BoostInProgress", idx);
            packet.ReadBits("UnkWod61x", 5, idx);

            packet.ReadWoWString("Character Name", nameLength, idx);

            if (firstLogin)
            {
                PlayerCreateInfo startPos = new PlayerCreateInfo { Race = race, Class = klass, Map = mapId, Zone = zone, Position = pos, Orientation = 0 };
                Storage.StartPositions.Add(startPos, packet.TimeSpan);
            }
        }

        public static void ReadRaceUnlockData(Packet packet, params object[] idx)
        {
            packet.ReadInt32E<Race>("RaceID", idx);
            packet.ResetBitReader();
            packet.ReadBit("HasExpansion", idx);
            packet.ReadBit("HasAchievement", idx);
            packet.ReadBit("HasHeritageArmor", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_0_52038))
                packet.ReadBit("IsLocked");
        }

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT)]
        public static void HandleEnumCharactersResult(Packet packet)
        {
            packet.ReadBit("Success");
            packet.ReadBit("IsDeletedCharacters");
            packet.ReadBit("IsDemonHunterCreationAllowed");
            packet.ReadBit("HasDemonHunterOnRealm");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V7_3_5_25848))
                packet.ReadBit("HasLevel70OnRealm");
            packet.ReadBit("Unknown7x");

            var hasDisabledClassesMask = packet.ReadBit("HasDisabledClassesMask");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_5_25848))
                packet.ReadBit("IsAlliedRacesCreationAllowed");

            var charsCount = packet.ReadUInt32("CharactersCount");
            uint count = 0;
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V7_3_5_25848))
                count = packet.ReadUInt32("FactionChangeRestrictionsCount");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_5_25848))
            {
                packet.ReadInt32("MaxCharacterLevel");
                count = packet.ReadUInt32("RaceUnlockCount");
            }

            if (hasDisabledClassesMask)
                packet.ReadUInt32("DisabledClassesMask");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V7_3_5_25848))
            {
                for (var i = 0; i < count; ++i)
                    V6_0_2_19033.Parsers.CharacterHandler.ReadFactionChangeRestrictionsData(packet, i, "FactionChangeRestrictionsData");
            }

            for (uint i = 0; i < charsCount; ++i)
                ReadCharactersData(packet, i, "CharactersData");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_5_25848))
            {
                for (var i = 0; i < count; ++i)
                    ReadRaceUnlockData(packet, i, "RaceUnlockData");
            }
        }

        [Parser(Opcode.CMSG_CREATE_CHARACTER)]
        public static void HandleClientCharCreate(Packet packet)
        {
            var nameLen = packet.ReadBits(6);
            var hasTemplateSet = packet.ReadBit();

            packet.ReadByteE<Race>("RaceID");
            packet.ReadByteE<Class>("ClassID");
            packet.ReadByteE<Gender>("SexID");
            packet.ReadByte("SkinID");
            packet.ReadByte("FaceID");
            packet.ReadByte("HairStyleID");
            packet.ReadByte("HairColorID");
            packet.ReadByte("FacialHairStyleID");
            packet.ReadByte("OutfitID");

            for (var i = 0; i < 3; i++)
                packet.ReadByte("CustomDisplay", i);

            packet.ReadWoWString("Name", nameLen);

            if (hasTemplateSet)
                packet.ReadInt32("TemplateSetID");
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

            packet.ReadInt32("Cp");
        }

        [Parser(Opcode.SMSG_HEALTH_UPDATE)]
        public static void HandleHealthUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadInt64("Health");
        }

        [Parser(Opcode.CMSG_ALTER_APPEARANCE)]
        public static void HandleAlterAppearance(Packet packet)
        {
            packet.ReadUInt32("NewHairStyle");
            packet.ReadUInt32("NewHairColor");
            packet.ReadUInt32("NewFacialHair");
            packet.ReadUInt32("NewSkinColor");
            packet.ReadUInt32("NewFace");

            for (uint i = 0; i < 3; ++i)
                packet.ReadUInt32("NewCustomDisplay", i);
        }

        public static void ReadPVPBracketData(Packet packet, params object[] idx)
        {
            packet.ReadInt32("Rating", idx);
            packet.ReadInt32("Rank", idx);
            packet.ReadInt32("WeeklyPlayed", idx);
            packet.ReadInt32("WeeklyWon", idx);
            packet.ReadInt32("SeasonPlayed", idx);
            packet.ReadInt32("SeasonWon", idx);
            packet.ReadInt32("WeeklyBestRating", idx);
            packet.ReadInt32("Unk710");
            packet.ReadByte("Bracket", idx);
        }

        [Parser(Opcode.SMSG_INSPECT_PVP)]
        public static void HandleInspectPVP(Packet packet)
        {
            packet.ReadPackedGuid128("ClientGUID");

            var bracketCount = packet.ReadBits(3);
            for (var i = 0; i < bracketCount; i++)
                ReadPVPBracketData(packet, i, "PVPBracketData");
        }

        [Parser(Opcode.CMSG_LEARN_PVP_TALENTS)]
        public static void HandleLearnPvPTalents(Packet packet)
        {
            var talentCount = packet.ReadBits("TalentCount", 6);
            for (int i = 0; i < talentCount; i++)
                packet.ReadUInt16("Talents");
        }

        [Parser(Opcode.SMSG_LEARN_PVP_TALENT_FAILED)]
        public static void HandleLearnPvPTalentFailed(Packet packet)
        {
            packet.ReadBits("Reason", 4);
            packet.ReadUInt32<SpellId>("SpellID");

            var talentCount = packet.ReadUInt32("TalentCount");
            for (int i = 0; i < talentCount; i++)
                packet.ReadUInt16("Talents");
        }

        [Parser(Opcode.SMSG_INSPECT_RESULT)]
        public static void HandleInspectResult(Packet packet)
        {
            packet.ReadPackedGuid128("InspecteeGUID");

            var int48 = packet.ReadInt32("ItemsCount");
            var int80 = packet.ReadInt32("GlyphsCount");
            var int112 = packet.ReadInt32("TalentsCount");
            var int144 = packet.ReadInt32("PvpTalentsCount");
            packet.ReadUInt32E<Class>("ClassID");
            packet.ReadUInt32("SpecializationID");
            packet.ReadUInt32E<Gender>("Gender");

            for (int i = 0; i < int80; i++)
                packet.ReadInt16("Glyphs", i);

            for (int i = 0; i < int112; i++)
                packet.ReadInt16("Talents", i);

            for (int i = 0; i < int144; i++)
                packet.ReadInt16("PvpTalents", i);

            packet.ResetBitReader();
            var hasGuildData = packet.ReadBit("HasGuildData");

            for (int i = 0; i < int48; i++)
            {
                packet.ReadPackedGuid128("CreatorGUID", i);
                packet.ReadByte("Index", i);

                Substructures.ItemHandler.ReadItemInstance(packet, i);

                packet.ResetBitReader();
                packet.ReadBit("Usable", i);
                var enchantsCount = packet.ReadBits("EnchantsCount", 4, i);
                var gemsCount = packet.ReadBits("GemsCount", 2, i);

                for (int j = 0; j < gemsCount; j++)
                {
                    packet.ReadByte("Slot", i, j);
                    Substructures.ItemHandler.ReadItemInstance(packet, i, j);
                }

                for (int j = 0; j < enchantsCount; j++)
                {
                    packet.ReadInt32("Id", i, j);
                    packet.ReadByte("Index", i, j);
                }
            }

            if (hasGuildData)
            {
                packet.ReadPackedGuid128("GuildGUID");
                packet.ReadUInt32("NumGuildMembers");
                packet.ReadUInt32("GuildAchievementPoints");
            }
        }

        [Parser(Opcode.CMSG_CHAR_CUSTOMIZE)]
        public static void HandleClientCharCustomize(Packet packet)
        {
            packet.ReadPackedGuid128("CharGUID");

            packet.ReadByte("SexID");
            packet.ReadByte("SkinID");
            packet.ReadByte("HairColorID");
            packet.ReadByte("HairStyleID");
            packet.ReadByte("FacialHairStyleID");
            packet.ReadByte("FaceID");

            for (uint i = 0; i < 3; ++i)
                packet.ReadByte("CustomDisplay", i);

            packet.ResetBitReader();
            var bits19 = packet.ReadBits(6);
            packet.ReadWoWString("CharName", bits19);
        }


        [Parser(Opcode.CMSG_CHAR_RACE_OR_FACTION_CHANGE)]
        public static void HandleCharRaceOrFactionChange(Packet packet)
        {
            packet.ReadBit("FactionChange");

            var nameLen = packet.ReadBits(6);

            packet.ReadPackedGuid128("Guid");
            packet.ReadByte("SexID");
            packet.ReadByteE<Race>("RaceID");
            packet.ReadByte("SkinID");
            packet.ReadByte("HairColorID");
            packet.ReadByte("HairStyleID");
            packet.ReadByte("FacialHairStyleID");
            packet.ReadByte("FaceID");
            for (uint i = 0; i < 3; ++i)
                packet.ReadByte("CustomDisplay", i);
            packet.ReadWoWString("Name", nameLen);
        }

        [Parser(Opcode.SMSG_CHAR_FACTION_CHANGE_RESULT)]
        public static void HandleCharFactionChangeResult(Packet packet)
        {
            packet.ReadByte("Result");
            packet.ReadPackedGuid128("Guid");

            packet.ResetBitReader();

            var bit72 = packet.ReadBit("HasDisplayInfo");
            if (bit72)
            {
                packet.ResetBitReader();
                var bits55 = packet.ReadBits(6);

                packet.ReadByte("SexID");
                packet.ReadByte("SkinID");
                packet.ReadByte("HairColorID");
                packet.ReadByte("HairStyleID");
                packet.ReadByte("FacialHairStyleID");
                packet.ReadByte("FaceID");
                packet.ReadByte("RaceID");

                for (uint i = 0; i < 3; ++i)
                    packet.ReadByte("CustomDisplay", i);
                packet.ReadWoWString("Name", bits55);
            }
        }

        [Parser(Opcode.SMSG_CHAR_CUSTOMIZE_SUCCESS)]
        public static void HandleServerCharCustomize(Packet packet)
        {
            packet.ReadPackedGuid128("CharGUID");
            packet.ReadByte("SexID");
            packet.ReadByte("SkinID");
            packet.ReadByte("HairColorID");
            packet.ReadByte("HairStyleID");
            packet.ReadByte("FacialHairStyleID");
            packet.ReadByte("FaceID");
            for (uint i = 0; i < 3; ++i)
                packet.ReadByte("CustomDisplay", i);

            packet.ResetBitReader();
            var bits55 = packet.ReadBits(6);
            packet.ReadWoWString("Name", bits55);
        }
    }
}

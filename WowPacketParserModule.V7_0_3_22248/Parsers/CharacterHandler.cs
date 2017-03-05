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
            packet.Translator.ReadPackedGuid128("Guid", idx);

            packet.Translator.ReadByte("ListPosition", idx);
            var race = packet.Translator.ReadByteE<Race>("RaceID", idx);
            var klass = packet.Translator.ReadByteE<Class>("ClassID", idx);
            packet.Translator.ReadByte("SexID", idx);
            packet.Translator.ReadByte("SkinID", idx);
            packet.Translator.ReadByte("FaceID", idx);
            packet.Translator.ReadByte("HairStyle", idx);
            packet.Translator.ReadByte("HairColor", idx);
            packet.Translator.ReadByte("FacialHairStyle", idx);

            for (uint j = 0; j < 3; ++j)
                packet.Translator.ReadByte("CustomDisplay", idx, j);

            packet.Translator.ReadByte("ExperienceLevel", idx);
            var zone = packet.Translator.ReadUInt32("ZoneID", idx);
            var mapId = packet.Translator.ReadUInt32("MapID", idx);

            var pos = packet.Translator.ReadVector3("PreloadPos", idx);

            packet.Translator.ReadPackedGuid128("GuildGUID", idx);

            packet.Translator.ReadUInt32("Flags", idx);
            packet.Translator.ReadUInt32("Flags2", idx);
            packet.Translator.ReadUInt32("Flags3", idx);
            packet.Translator.ReadUInt32("PetCreatureDisplayID", idx);
            packet.Translator.ReadUInt32("PetExperienceLevel", idx);
            packet.Translator.ReadUInt32("PetCreatureFamilyID", idx);

            for (uint j = 0; j < 2; ++j)
                packet.Translator.ReadUInt32("ProfessionIDs", idx, j);

            for (uint j = 0; j < 23; ++j)
            {
                packet.Translator.ReadUInt32("InventoryItem DisplayID", idx, j);
                packet.Translator.ReadUInt32("InventoryItem DisplayEnchantID", idx, j);
                packet.Translator.ReadByteE<InventoryType>("InventoryItem InvType", idx, j);
            }

            packet.Translator.ReadTime("LastPlayedTime", idx);

            packet.Translator.ReadUInt16("SpecID", idx);
            packet.Translator.ReadUInt32("Unknown703", idx);
            packet.Translator.ReadUInt32("Flags4", idx);

            packet.Translator.ResetBitReader();

            var nameLength = packet.Translator.ReadBits("Character Name Length", 6, idx);
            var firstLogin = packet.Translator.ReadBit("FirstLogin", idx);
            packet.Translator.ReadBit("BoostInProgress", idx);
            packet.Translator.ReadBits("UnkWod61x", 5, idx);

            packet.Translator.ReadWoWString("Character Name", nameLength, idx);

            if (firstLogin)
            {
                PlayerCreateInfo startPos = new PlayerCreateInfo { Race = race, Class = klass, Map = mapId, Zone = zone, Position = pos, Orientation = 0 };
                Storage.StartPositions.Add(startPos, packet.TimeSpan);
            }
        }

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT)]
        public static void HandleEnumCharactersResult(Packet packet)
        {
            packet.Translator.ReadBit("Success");
            packet.Translator.ReadBit("IsDeletedCharacters");
            packet.Translator.ReadBit("IsDemonHunterCreationAllowed");
            packet.Translator.ReadBit("HasDemonHunterOnRealm");
            packet.Translator.ReadBit("HasLevel70OnRealm");
            packet.Translator.ReadBit("Unknown7x");

            var hasDisabledClassesMask = packet.Translator.ReadBit("HasDisabledClassesMask");

            var charsCount = packet.Translator.ReadUInt32("CharactersCount");
            var restrictionsCount = packet.Translator.ReadUInt32("FactionChangeRestrictionsCount");

            if (hasDisabledClassesMask)
                packet.Translator.ReadUInt32("DisabledClassesMask");

            for (var i = 0; i < restrictionsCount; ++i)
                V6_0_2_19033.Parsers.CharacterHandler.ReadFactionChangeRestrictionsData(packet, i, "FactionChangeRestrictionsData");

            for (uint i = 0; i < charsCount; ++i)
                ReadCharactersData(packet, i, "CharactersData");
        }

        [Parser(Opcode.CMSG_CREATE_CHARACTER)]
        public static void HandleClientCharCreate(Packet packet)
        {
            var nameLen = packet.Translator.ReadBits(6);
            var hasTemplateSet = packet.Translator.ReadBit();

            packet.Translator.ReadByteE<Race>("RaceID");
            packet.Translator.ReadByteE<Class>("ClassID");
            packet.Translator.ReadByteE<Gender>("SexID");
            packet.Translator.ReadByte("SkinID");
            packet.Translator.ReadByte("FaceID");
            packet.Translator.ReadByte("HairStyleID");
            packet.Translator.ReadByte("HairColorID");
            packet.Translator.ReadByte("FacialHairStyleID");
            packet.Translator.ReadByte("OutfitID");

            for (uint i = 0; i < 3; ++i)
                packet.Translator.ReadByte("CustomDisplay", i);

            packet.Translator.ReadWoWString("Name", nameLen);

            if (hasTemplateSet)
                packet.Translator.ReadInt32("TemplateSetID");
        }

        [Parser(Opcode.SMSG_LEVEL_UP_INFO)]
        public static void HandleLevelUpInfo(Packet packet)
        {
            packet.Translator.ReadInt32("Level");
            packet.Translator.ReadInt32("HealthDelta");

            for (var i = 0; i < 6; i++)
                packet.Translator.ReadInt32("PowerDelta", (PowerType)i);

            for (var i = 0; i < 4; i++)
                packet.Translator.ReadInt32("StatDelta", (StatType)i);

            packet.Translator.ReadInt32("Cp");
        }

        [Parser(Opcode.SMSG_HEALTH_UPDATE)]
        public static void HandleHealthUpdate(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");
            packet.Translator.ReadInt64("Health");
        }

        [Parser(Opcode.CMSG_ALTER_APPEARANCE)]
        public static void HandleAlterAppearance(Packet packet)
        {
            packet.Translator.ReadUInt32("NewHairStyle");
            packet.Translator.ReadUInt32("NewHairColor");
            packet.Translator.ReadUInt32("NewFacialHair");
            packet.Translator.ReadUInt32("NewSkinColor");
            packet.Translator.ReadUInt32("NewFace");

            for (uint i = 0; i < 3; ++i)
                packet.Translator.ReadUInt32("NewCustomDisplay", i);
        }

        public static void ReadPVPBracketData(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("Rating", idx);
            packet.Translator.ReadInt32("Rank", idx);
            packet.Translator.ReadInt32("WeeklyPlayed", idx);
            packet.Translator.ReadInt32("WeeklyWon", idx);
            packet.Translator.ReadInt32("SeasonPlayed", idx);
            packet.Translator.ReadInt32("SeasonWon", idx);
            packet.Translator.ReadInt32("WeeklyBestRating", idx);
            packet.Translator.ReadInt32("Unk710");
            packet.Translator.ReadByte("Bracket", idx);
        }

        [Parser(Opcode.SMSG_INSPECT_PVP)]
        public static void HandleInspectPVP(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("ClientGUID");

            var bracketCount = packet.Translator.ReadBits(3);
            for (var i = 0; i < bracketCount; i++)
                ReadPVPBracketData(packet, i, "PVPBracketData");
        }

        [Parser(Opcode.SMSG_POWER_UPDATE)]
        public static void HandlePowerUpdate(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");

            var int32 = packet.Translator.ReadInt32("Count");
            for (var i = 0; i < int32; i++)
            {
                packet.Translator.ReadInt32("Power", i);
                packet.Translator.ReadByteE<PowerType>("PowerType", i);
            }
        }

        [Parser(Opcode.CMSG_LEARN_PVP_TALENTS)]
        public static void HandleLearnPvPTalents(Packet packet)
        {
            var talentCount = packet.Translator.ReadBits("TalentCount", 6);
            for (int i = 0; i < talentCount; i++)
                packet.Translator.ReadUInt16("Talents");
        }

        [Parser(Opcode.SMSG_LEARN_PVP_TALENTS_FAILED)]
        public static void HandleLearnPvPTalentsFailed(Packet packet)
        {
            packet.Translator.ReadBits("Reason", 4);
            packet.Translator.ReadUInt32<SpellId>("SpellID");

            var talentCount = packet.Translator.ReadUInt32("TalentCount");
            for (int i = 0; i < talentCount; i++)
                packet.Translator.ReadUInt16("Talents");
        }
    }
}

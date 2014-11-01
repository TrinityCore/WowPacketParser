using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParserModule.V6_0_2_19033.Enums;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class CharacterHandler
    {
        [Parser(Opcode.SMSG_CHAR_ENUM)]
        public static void HandleCharEnum(Packet packet)
        {
            packet.ReadBit("Success");
            packet.ReadBit("IsDeletedCharacters");
            var charsCount = packet.ReadUInt32("Characters Count");
            var restrictionsCount = packet.ReadUInt32("FactionChangeRestrictionsCount");

            for (uint i = 0; i < charsCount; ++i)
            {
                packet.ReadPackedGuid128("Guid", i);

                packet.ReadByte("ListPosition", i);
                packet.ReadEnum<Race>("RaceID", TypeCode.Byte, i);
                packet.ReadEnum<Class>("ClassID", TypeCode.Byte, i);
                packet.ReadByte("SexID", i);
                packet.ReadByte("SkinID", i);
                packet.ReadByte("FaceID", i);
                packet.ReadByte("HairStyle", i);
                packet.ReadByte("HairColor", i);
                packet.ReadByte("FacialHairStyle", i);
                packet.ReadByte("ExperienceLevel", i);
                packet.ReadUInt32("ZoneID", i);
                packet.ReadUInt32("MapID", i);
                
                packet.ReadVector3("PreloadPos", i);

                packet.ReadPackedGuid128("GuildGUID", i);

                packet.ReadUInt32("Flags", i);
                packet.ReadUInt32("Flags2", i);
                packet.ReadUInt32("Flags3", i);
                packet.ReadUInt32("PetCreatureDisplayID", i);
                packet.ReadUInt32("PetExperienceLevel", i);
                packet.ReadUInt32("PetCreatureFamilyID", i);

                packet.ReadUInt32("ProfessionIDs", i, 0);
                packet.ReadUInt32("ProfessionIDs", i, 1);

                for (uint j = 0; j < 23; ++j)
                {
                    packet.ReadUInt32("InventoryItem DisplayID", i, j);
                    packet.ReadUInt32("InventoryItem DisplayEnchantID", i, j);
                    packet.ReadEnum<InventoryType>("InventoryItem InvType", TypeCode.Byte, i, j);
                }

                packet.ResetBitReader();
                var nameLength = packet.ReadBits("Character Name Length", 6, i);
                packet.ReadBit("FirstLogin", i);
                packet.ReadBit("BoostInProgress", i);
                packet.ReadWoWString("Character Name", nameLength, i);
            }

            for (var i = 0; i < restrictionsCount; ++i)
            {
                packet.ReadUInt32("FactionChangeRestriction Mask", i);
                packet.ReadByte("FactionChangeRestriction RaceID", i);
            }
        }

        [Parser(Opcode.CMSG_CHAR_CREATE)]
        public static void HandleClientCharCreate(Packet packet)
        {
            var bits29 = packet.ReadBits(6);
            var bit24 = packet.ReadBit();

            packet.ReadEnum<Race>("RaceID", TypeCode.Byte);
            packet.ReadEnum<Class>("ClassID", TypeCode.Byte);
            packet.ReadEnum<Gender>("SexID", TypeCode.Byte);
            packet.ReadByte("SkinID");
            packet.ReadByte("FaceID");
            packet.ReadByte("HairStyleID");
            packet.ReadByte("HairColorID");
            packet.ReadByte("FacialHairStyleID");
            packet.ReadByte("OutfitID");

            packet.ReadWoWString("Name", bits29);

            if (bit24)
                packet.ReadInt32("TemplateSetID");
        }

        [Parser(Opcode.SMSG_INIT_CURRENCY)]
        public static void HandleInitCurrency(Packet packet)
        {
            var count = packet.ReadInt32("SetupCurrencyRecord");

            // ClientSetupCurrencyRecord
            for (var i = 0; i < count; ++i)
            {
                packet.ReadUInt32("Type", i);
                packet.ReadUInt32("Quantity", i);

                var hasWeeklyQuantity = packet.ReadBit();
                var hasMaxWeeklyQuantity = packet.ReadBit();
                var hasTrackedQuantity = packet.ReadBit();
                packet.ReadBits("Flags", 5, i);

                if (hasWeeklyQuantity)
                    packet.ReadUInt32("WeeklyQuantity", i);

                if (hasMaxWeeklyQuantity)
                    packet.ReadUInt32("MaxWeeklyQuantity", i);

                if (hasTrackedQuantity)
                    packet.ReadUInt32("TrackedQuantity", i);
            }
        }

        [Parser(Opcode.CMSG_UNDELETE_CHARACTER)]
        public static void HandleUndeleteCharacter(Packet packet)
        {
            packet.ReadInt32("ClientToken");
            packet.ReadPackedGuid128("CharacterGuid");
        }

        [Parser(Opcode.SMSG_UNDELETE_CHARACTER_RESPONSE)]
        public static void HandleUndeleteCharacterResponse(Packet packet)
        {
            packet.ReadEnum<CharacterUndeleteResult>("Result", TypeCode.Int32);
            packet.ReadInt32("ClientToken");
            packet.ReadPackedGuid128("CharacterGuid");
        }

        [Parser(Opcode.SMSG_UNDELETE_COOLDOWN_STATUS_RESPONSE)]
        public static void HandleUndeleteCooldownStatusResponse(Packet packet)
        {
            packet.ReadBit("OnCooldown");
            packet.ReadInt32("MaxCooldown"); // In Sec
            packet.ReadInt32("CurrentCooldown"); // In Sec
        }

        [Parser(Opcode.SMSG_POWER_UPDATE)]
        public static void HandlePowerUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");

            var int32 = packet.ReadInt32("Count");
            for (var i = 0; i < int32; i++)
            {
                packet.ReadInt32("Power", i);
                packet.ReadEnum<PowerType>("PowerType", TypeCode.Byte, i);
            }
        }

        [Parser(Opcode.CMSG_REORDER_CHARACTERS)]
        public static void HandleReorderCharacters(Packet packet)
        {
            var count = packet.ReadBits("CharactersCount", 9);

            for (var i = 0; i < count; ++i)
            {
                packet.ReadPackedGuid128("PlayerGUID");
                packet.ReadByte("NewPosition", i);
            }
        }
    }
}

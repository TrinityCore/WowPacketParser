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
            packet.ReadInt32("ClientToken");
            packet.ReadEnum<CharacterUndeleteResult>("Result", TypeCode.Int32);
            packet.ReadPackedGuid128("CharacterGuid");
        }

        [Parser(Opcode.CMSG_UNDELETE_COOLDOWN_STATUS_QUERY)]
        public static void HandleUndeleteCooldownStatusQuery(Packet packet)
        {
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

        [Parser(Opcode.CMSG_CHAR_DELETE)]
        public static void HandleClientCharDelete(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGUID");
        }

        [Parser(Opcode.CMSG_RANDOMIZE_CHAR_NAME)]
        public static void HandleGenerateRandomCharacterNameQuery(Packet packet)
        {
            packet.ReadEnum<Race>("Race", TypeCode.Byte);
            packet.ReadEnum<Gender>("Sex", TypeCode.Byte);
        }

        [Parser(Opcode.SMSG_RANDOMIZE_CHAR_NAME)]
        public static void HandleGenerateRandomCharacterNameResponse(Packet packet)
        {
            packet.ReadBit("Success");
            var bits17 = packet.ReadBits(6);

            packet.ReadWoWString("Name", bits17);
        }

        [Parser(Opcode.CMSG_CHAR_RENAME)]
        public static void HandleClientCharRename(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");

            packet.ResetBitReader();

            var bits16 = packet.ReadBits(6);
            packet.ReadWoWString("NewName", bits16);
        }

        [Parser(Opcode.SMSG_CHAR_RENAME)]
        public static void HandleServerCharRename(Packet packet)
        {
            packet.ReadByte("Result");

            packet.ResetBitReader();
            var bit32 = packet.ReadBit("HasGuid");
            var bits41 = packet.ReadBits(6);

            if (bit32)
                packet.ReadPackedGuid128("Guid");

            packet.ReadWoWString("Name", bits41);
        }

        [Parser(Opcode.CMSG_CHAR_RACE_OR_FACTION_CHANGE)]
        public static void HandleCharRaceOrFactionChange(Packet packet)
        {
            packet.ReadBit("FactionChange");

            var bits20 = packet.ReadBits(6);

            var bit93 = packet.ReadBit("HasSkinID");
            var bit96 = packet.ReadBit("HasHairColor");
            var bit89 = packet.ReadBit("HasHairStyleID");
            var bit17 = packet.ReadBit("HasFacialHairStyleID");
            var bit19 = packet.ReadBit("HasFaceID");

            packet.ReadPackedGuid128("Guid");
            packet.ReadByte("SexID");
            packet.ReadByte("RaceID");

            packet.ReadWoWString("Name", bits20);

            if (bit93)
                packet.ReadByte("SkinID");

            if (bit96)
                packet.ReadByte("HairColorID");

            if (bit89)
                packet.ReadByte("HairStyleID");

            if (bit17)
                packet.ReadByte("FacialHairStyleID");

            if (bit19)
                packet.ReadByte("FaceID");
        }

        [Parser(Opcode.SMSG_CHAR_FACTION_CHANGE)]
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

                packet.ReadWoWString("Name", bits55);
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

            packet.ResetBitReader();

            var bits19 = packet.ReadBits(6);
            packet.ReadWoWString("CharName", bits19);
        }

        [Parser(Opcode.SMSG_CHAR_CUSTOMIZE)]
        public static void HandleServerCharCustomize(Packet packet)
        {
            packet.ReadPackedGuid128("CharGUID");

            packet.ResetBitReader();
            var bits55 = packet.ReadBits(6);

            packet.ReadByte("SexID");

            packet.ReadByte("SkinID");
            packet.ReadByte("HairColorID");
            packet.ReadByte("HairStyleID");
            packet.ReadByte("FacialHairStyleID");
            packet.ReadByte("FaceID");

            packet.ReadWoWString("Name", bits55);
        }

        [Parser(Opcode.SMSG_CHAR_CUSTOMIZE_RESULT)]
        public static void HandleServerCharCustomizeResult(Packet packet)
        {
            packet.ReadByte("Result");
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.CMSG_SET_LOOT_SPECIALIZATION)]
        public static void HandleSetLootSpecialization(Packet packet)
        {
            packet.ReadInt32("SpecID");
        }

        [Parser(Opcode.SMSG_LOG_XPGAIN)]
        public static void HandleLogXPGain(Packet packet)
        {
            packet.ReadPackedGuid128("Victim");
            packet.ReadInt32("Original");

            packet.ReadByte("Reason");
            packet.ReadInt32("Amount");
            packet.ReadSingle("GroupBonus");

            packet.ReadBit("ReferAFriend");
        }

        [Parser(Opcode.CMSG_NAME_QUERY)]
        public static void HandleNameQuery(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");

            var bit4 = packet.ReadBit();
            var bit12 = packet.ReadBit();

            if (bit4)
                packet.ReadInt32("VirtualRealmAddress");

            if (bit12)
                packet.ReadInt32("NativeRealmAddress");
        }

        [Parser(Opcode.SMSG_NAME_QUERY_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            var hasData = packet.ReadByte("HasData");

            packet.ReadPackedGuid128("Player Guid");

            if (hasData == 0)
            {
                var bits15 = (int)packet.ReadBits(7);

                var count = new int[5];
                for (var i = 0; i < 5; ++i)
                    count[i] = (int)packet.ReadBits(7);

                for (var i = 0; i < 5; ++i)
                    packet.ReadWoWString("Name Declined", count[i], i);

                packet.ReadPackedGuid128("AccountID");
                packet.ReadPackedGuid128("BnetAccountID");
                packet.ReadPackedGuid128("Player Guid");

                packet.ReadUInt32("VirtualRealmAddress");

                packet.ReadEnum<Race>("Race", TypeCode.Byte);
                packet.ReadEnum<Gender>("Gender", TypeCode.Byte);
                packet.ReadEnum<Class>("Class", TypeCode.Byte);
                packet.ReadByte("Level");

                packet.ReadWoWString("Name", bits15);
            }
        }
    }
}

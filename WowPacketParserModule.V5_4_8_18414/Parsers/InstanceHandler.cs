using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class InstanceHandler
    {
        [Parser(Opcode.CMSG_SET_DUNGEON_DIFFICULTY)]
        [Parser(Opcode.CMSG_SET_RAID_DIFFICULTY)]
        [Parser(Opcode.SMSG_SET_RAID_DIFFICULTY)]
        public static void HandleSetDifficulty(Packet packet)
        {
            packet.ReadEnum<MapDifficulty>("Difficulty", TypeCode.Int32);
        }

        [Parser(Opcode.CMSG_SAVE_CUF_PROFILES)]
        public static void HandleSaveCUFProfiles(Packet packet)
        {
            var count = packet.ReadBits("count", 19);
            var len = new uint[count];
            for (var i = 0; i < count; i++)
            {
                packet.ReadBitVisible("unk166", i);
                packet.ReadBitVisible("unk136", i);
                packet.ReadBitVisible("unk140", i);
                packet.ReadBitVisible("unk161", i);
                packet.ReadBitVisible("unk159", i);
                packet.ReadBitVisible("unk156", i);
                packet.ReadBitVisible("unk164", i);
                packet.ReadBitVisible("unk158", i);
                packet.ReadBitVisible("unk134", i);
                packet.ReadBitVisible("unk142", i);
                packet.ReadBitVisible("unk163", i);
                packet.ReadBitVisible("unk145", i);
                len[i] = packet.ReadBits("len", 7, i);
                packet.ReadBitVisible("unk135", i);
                packet.ReadBitVisible("unk167", i);
                packet.ReadBitVisible("unk139", i);
                packet.ReadBitVisible("unk144", i);
                packet.ReadBitVisible("unk162", i);
                packet.ReadBitVisible("unk157", i);
                packet.ReadBitVisible("unk141", i);
                packet.ReadBitVisible("unk143", i);
                packet.ReadBitVisible("unk165", i);
                packet.ReadBitVisible("unk160", i);
                packet.ReadBitVisible("unk168", i);
                packet.ReadBitVisible("unk137", i);
                packet.ReadBitVisible("unk138", i);
            }
            for (var i = 0; i < count; i++)
            {
                packet.ReadInt16("unk128", i);
                packet.ReadByte("unk146", i);
                packet.ReadByte("unk133", i);
                packet.ReadInt16("unk130", i);
                packet.ReadByte("unk148", i);
                packet.ReadByte("unk132", i);
                packet.ReadInt16("unk150", i);
                packet.ReadWoWString("str", len[i], i);
                packet.ReadByte("unk147", i);
                packet.ReadInt16("unk152", i);
                packet.ReadInt16("unk154", i);
            }
        }

        [Parser(Opcode.SMSG_CORPSE_NOT_IN_INSTANCE)]
        public static void HandleCorpseNotInInstance(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_LOAD_CUF_PROFILES)]
        public static void HandleLoadCUFProfiles(Packet packet)
        {
            var count = packet.ReadBits("count", 19);
            var len = new uint[count];

            for (var i = 0; i < count; i++)
            {
                packet.ReadBitVisible("unk165", i);
                packet.ReadBitVisible("unk159", i);
                packet.ReadBitVisible("unk157", i);
                packet.ReadBitVisible("unk161", i);
                packet.ReadBitVisible("unk164", i);
                packet.ReadBitVisible("unk141", i);
                packet.ReadBitVisible("unk142", i);
                packet.ReadBitVisible("unk134", i);
                packet.ReadBitVisible("unk140", i);
                len[i] = packet.ReadBits("len", 7, i);
                packet.ReadBitVisible("unk135", i);
                packet.ReadBitVisible("unk138", i);
                packet.ReadBitVisible("unk145", i);
                packet.ReadBitVisible("unk167", i);
                packet.ReadBitVisible("unk156", i);
                packet.ReadBitVisible("unk136", i);
                packet.ReadBitVisible("unk144", i);
                packet.ReadBitVisible("unk143", i);
                packet.ReadBitVisible("unk166", i);
                packet.ReadBitVisible("unk137", i);
                packet.ReadBitVisible("unk139", i);
                packet.ReadBitVisible("unk163", i);
                packet.ReadBitVisible("unk168", i);
                packet.ReadBitVisible("unk160", i);
                packet.ReadBitVisible("unk162", i);
                packet.ReadBitVisible("unk158", i);
            }
            for (var i = 0; i < count; i++)
            {
                packet.ReadInt16("unk152", i);
                packet.ReadInt16("unk154", i);
                packet.ReadByte("unk133", i);
                packet.ReadWoWString("Profile", len[i], i);
                packet.ReadByte("unk147", i);
                packet.ReadByte("unk146", i);
                packet.ReadInt16("unk128", i);
                packet.ReadByte("unk148", i);
                packet.ReadByte("unk132", i);
                packet.ReadInt16("unk130", i);
                packet.ReadInt16("unk150", i);
            }
        }

        [Parser(Opcode.SMSG_SET_DUNGEON_DIFFICULTY)]
        public static void HandleSetDungeonDifficulty(Packet packet)
        {
            packet.ReadInt32("Difficulty");
        }

        [Parser(Opcode.CMSG_RESET_INSTANCES)]
        [Parser(Opcode.SMSG_UPDATE_DUNGEON_ENCOUNTER_FOR_LOOT)]
        public static void HandleInstanceNull(Packet packet)
        {
        }

        [Parser(Opcode.MSG_SET_RAID_DIFFICULTY)]
        [Parser(Opcode.SMSG_INSTANCE_RESET)]
        [Parser(Opcode.SMSG_UPDATE_LAST_INSTANCE)]
        public static void HandleServerSetDifficulty(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.SMSG_WEATHER)]
        public static void HandleWeatherStatus(Packet packet)
        {
            packet.ReadSingle("Grade");
            packet.ReadEnum<WeatherState>("State", TypeCode.Int32);
            packet.ReadBit("Unk Bit"); // Type
        }

        [Parser(Opcode.SMSG_WEEKLY_SPELL_USAGE)]
        public static void HandleWeeklySpellUsage(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);

            for (int i = 0; i < count; ++i)
            {
                packet.ReadByte("Unk Int8");
                packet.ReadInt32("Unk Int32");
            }
        }

        [Parser(Opcode.SMSG_PLAY_SOUND)]
        public static void HandlePlaySound(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 1, 6, 7, 5, 4, 3, 0, 2);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 4);
            var sound = packet.ReadUInt32("Sound Id");
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 7);

            packet.WriteGuid("Guid", guid);

            Storage.Sounds.Add(sound, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_SET_SELECTION)]
        public static void HandleSetSelection(Packet packet)
        {
            var guid = packet.StartBitStream(3, 5, 6, 7, 2, 4, 1, 0);
            packet.ParseBitStream(guid, 5, 0, 4, 3, 1, 7, 2, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_INSPECT)]
        public static void HandleClientInspect(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 3, 7, 0, 4, 1, 6, 5, 2);
            packet.ParseBitStream(guid, 0, 2, 4, 7, 6, 3, 1, 5);

            packet.WriteGuid("Player GUID: ", guid);
        }

        [Parser(Opcode.CMSG_TIME_SYNC_RESP)]
        public static void HandleTimeSyncResp(Packet packet)
        {
            packet.ReadUInt32("Counter");
            packet.ReadUInt32("Ticks");
        }

        [Parser(Opcode.SMSG_WORLD_SERVER_INFO)]
        public static void HandleWorldServerInfo(Packet packet)
        {
            var bit14 = packet.ReadBit();
            var bit30 = packet.ReadBit();
            var bit38 = packet.ReadBit();
            var bit24 = packet.ReadBit();

            if (bit38)
                packet.ReadInt32("Int34");

            packet.ReadTime("Last Weekly Reset");
            packet.ReadInt32("Instance Difficulty ID");
            packet.ReadBoolean("Is On Tournament Realm");

            if (bit14)
                packet.ReadInt32("Int10");

            if (bit24)
                packet.ReadInt32("Int1C");

            if (bit30)
                packet.ReadInt32("Int2C");
        }

        [Parser(Opcode.CMSG_AREATRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            packet.ReadInt32("Area Trigger Id");
            packet.ReadBit("Unk bit1");
            packet.ReadBit("Unk bit2");
        }
    }
}

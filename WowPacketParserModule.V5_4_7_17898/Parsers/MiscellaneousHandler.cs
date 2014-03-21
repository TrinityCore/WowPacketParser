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

            packet.StartBitStream(guid, 5, 0, 7, 4, 6, 2, 1, 3);
            packet.ParseBitStream(guid, 5, 6, 3, 4, 0, 1, 7, 2);

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

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS)]
        public static void HandleFeatureSystemStatus(Packet packet)
        {
            packet.ReadByte("Complain System Status");
            packet.ReadInt32("Scroll of Resurrections Remaining");
            packet.ReadInt32("Realm Id?");
            packet.ReadInt32("Scroll of Resurrections Per Day");
            packet.ReadInt32("Unused Int32");

            packet.ReadBit("bit26");
            var sessionTimeAlert = packet.ReadBit("Session Time Alert");
            packet.ReadBit("bit54");
            packet.ReadBit("bit30");
            var quickTicket = packet.ReadBit("EuropaTicketSystemEnabled");
            packet.ReadBit("bit38");
            packet.ReadBit("bit25");
            packet.ReadBit("bit24");
            packet.ReadBit("bit28");

            if (quickTicket)
            {
                packet.ReadInt32("Unk5");
                packet.ReadInt32("Unk6");
                packet.ReadInt32("Unk7");
                packet.ReadInt32("Unk8");
            }

            if (sessionTimeAlert)
            {
                packet.ReadInt32("Int10");
                packet.ReadInt32("Int18");
                packet.ReadInt32("Int14");
            }
        }

        [Parser(Opcode.CMSG_INSPECT_HONOR_STATS)]
        public static void HandleInspectHonorStats(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 1, 7, 0, 5, 2, 3, 6, 4);
            packet.ParseBitStream(guid, 3, 7, 5, 4, 0, 1, 6, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_INSPECT_HONOR_STATS)]
        public static void HandleInspectHonorStatsResponse(Packet packet)
        {
            var guid = new byte[8];

            // Might be swapped, unsure
            packet.ReadInt16("Yesterday Honorable Kills");
            packet.ReadByte("Lifetime Max Rank");
            packet.ReadInt32("Life Time Kills");
            packet.ReadInt16("Today Honorable Kills");

            packet.StartBitStream(guid, 4, 3, 5, 7, 6, 0, 2, 1);
            packet.ParseBitStream(guid, 3, 0, 5, 2, 6, 7, 4, 1);

            packet.WriteGuid("Guid", guid);
        }
    }
}

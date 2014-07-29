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

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class MiscellaneousHandler
    {
        [HasSniffData]
        [Parser(Opcode.CMSG_LOAD_SCREEN)]
        public static void HandleClientEnterWorld(Packet packet)
        {
            var mapId = packet.ReadEntryWithName<UInt32>(StoreNameType.Map, "Map");
            packet.ReadBit("Loading");
        }

        [Parser(Opcode.CMSG_AREATRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            var entry = packet.ReadEntry("Area Trigger Id");
            packet.ReadBit("Unk bit1");
            packet.ReadBit("Unk bit2");

            packet.AddSniffData(StoreNameType.AreaTrigger, entry.Key, "AREATRIGGER");
        }

        [Parser(Opcode.CMSG_TIME_SYNC_RESP)]
        public static void HandleTimeSyncResp(Packet packet)
        {
            packet.ReadUInt32("Counter");
            packet.ReadUInt32("Ticks");
        }

        [Parser(Opcode.SMSG_WHO)]
        public static void HandleWho(Packet packet)
        {
            var counter = (int)packet.ReadBits("List count", 6);

            var accountId = new byte[counter][];
            var playerGUID = new byte[counter][];
            var guildGUID = new byte[counter][];

            var guildNameLength = new uint[counter];
            var playerNameLength = new uint[counter];
            var bits14 = new uint[counter][];
            var bitED = new bool[counter];
            var bit214 = new bool[counter];

            for (var i = 0; i < counter; ++i)
            {
                accountId[i] = new byte[8];
                playerGUID[i] = new byte[8];
                guildGUID[i] = new byte[8];

                accountId[i][2] = packet.ReadBit();
                playerGUID[i][2] = packet.ReadBit();
                accountId[i][7] = packet.ReadBit();
                guildGUID[i][5] = packet.ReadBit();
                guildNameLength[i] = packet.ReadBits(7);
                accountId[i][1] = packet.ReadBit();
                accountId[i][5] = packet.ReadBit();
                guildGUID[i][7] = packet.ReadBit();
                playerGUID[i][5] = packet.ReadBit();
                bitED[i] = packet.ReadBit();
                guildGUID[i][1] = packet.ReadBit();
                playerGUID[i][6] = packet.ReadBit();
                guildGUID[i][2] = packet.ReadBit();
                playerGUID[i][4] = packet.ReadBit();
                guildGUID[i][0] = packet.ReadBit();
                guildGUID[i][3] = packet.ReadBit();
                accountId[i][6] = packet.ReadBit();
                bit214[i] = packet.ReadBit();
                playerGUID[i][1] = packet.ReadBit();
                guildGUID[i][4] = packet.ReadBit();
                accountId[i][0] = packet.ReadBit();

                bits14[i] = new uint[5];
                for (var j = 0; j < 5; ++j)
                    bits14[i][j] = packet.ReadBits(7);

                playerGUID[i][3] = packet.ReadBit();
                guildGUID[i][6] = packet.ReadBit();
                playerGUID[i][0] = packet.ReadBit();
                accountId[i][4] = packet.ReadBit();
                accountId[i][3] = packet.ReadBit();
                playerGUID[i][7] = packet.ReadBit();
                playerNameLength[i] = packet.ReadBits(6);
            }

            for (var i = 0; i < counter; ++i)
            {
                packet.ReadXORByte(playerGUID[i], 1);
                packet.ReadInt32("RealmID", i);
                packet.ReadXORByte(playerGUID[i], 7);
                packet.ReadInt32("RealmID", i);
                packet.ReadXORByte(playerGUID[i], 4);
                packet.ReadWoWString("Player Name", playerNameLength[i], i);
                packet.ReadXORByte(guildGUID[i], 1);
                packet.ReadXORByte(playerGUID[i], 0);
                packet.ReadXORByte(guildGUID[i], 2);
                packet.ReadXORByte(guildGUID[i], 0);
                packet.ReadXORByte(guildGUID[i], 4);
                packet.ReadXORByte(playerGUID[i], 3);
                packet.ReadXORByte(guildGUID[i], 6);
                packet.ReadInt32("Unk1", i);
                packet.ReadWoWString("Guild Name", guildNameLength[i], i);
                packet.ReadXORByte(guildGUID[i], 3);
                packet.ReadXORByte(accountId[i], 4);
                packet.ReadEnum<Class>("Class", TypeCode.Byte, i);
                packet.ReadXORByte(accountId[i], 7);
                packet.ReadXORByte(playerGUID[i], 6);
                packet.ReadXORByte(playerGUID[i], 2);

                for (var j = 0; j < 5; ++j)
                    packet.ReadWoWString("String14", bits14[i][j]);

                packet.ReadXORByte(accountId[i], 2);
                packet.ReadXORByte(accountId[i], 3);
                packet.ReadEnum<Race>("Race", TypeCode.Byte, i);
                packet.ReadXORByte(guildGUID[i], 7);
                packet.ReadXORByte(accountId[i], 1);
                packet.ReadXORByte(accountId[i], 5);
                packet.ReadXORByte(accountId[i], 6);
                packet.ReadXORByte(playerGUID[i], 5);
                packet.ReadXORByte(accountId[i], 0);
                packet.ReadEnum<Gender>("Gender", TypeCode.Byte, i);
                packet.ReadXORByte(guildGUID[i], 5);
                packet.ReadByte("Level", i);
                packet.ReadEntryWithName<Int32>(StoreNameType.Zone, "Zone Id", i);

                packet.WriteGuid("PlayerGUID", playerGUID[i], i);
                packet.WriteGuid("GuildGUID", guildGUID[i], i);
                packet.WriteLine("[{0}] Account: {1}", i, BitConverter.ToUInt64(accountId[i], 0));
            }
        }

        [Parser(Opcode.CMSG_SET_SELECTION)]
        public static void HandleSetSelection(Packet packet)
        {
            var guid = packet.StartBitStream(7, 6, 5, 4, 3, 2, 1, 0);
            packet.ParseBitStream(guid, 0, 7, 3, 5, 1, 4, 6, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_WHO)]
        public static void HandleWhoRequest(Packet packet)
        {
            packet.ReadInt32("ClassMask");
            packet.ReadInt32("RaceMask");
            packet.ReadInt32("Max Level");
            packet.ReadInt32("Min Level");


            packet.ReadBit("bit2C4");
            packet.ReadBit("bit2C6");
            var bit2D4 = packet.ReadBit();
            var bits1AB = packet.ReadBits(9);
            packet.ReadBit("bit2C5");
            var PlayerNameLen = packet.ReadBits(6);
            var zones = packet.ReadBits(4);
            var bits73 = packet.ReadBits(9);
            var guildNameLen = packet.ReadBits(7);
            var patterns = packet.ReadBits(3);

            var bits2B8 = new uint[patterns];
            for (var i = 0; i < patterns; ++i)
                bits2B8[i] = packet.ReadBits(7);

            for (var i = 0; i < patterns; ++i)
                packet.ReadWoWString("Pattern", bits2B8[i], i);

            packet.ReadWoWString("string1AB", bits1AB);

            for (var i = 0; i < zones; ++i)
                packet.ReadEntryWithName<Int32>(StoreNameType.Zone, "Zone Id");

            packet.ReadWoWString("Player Name", PlayerNameLen);

            packet.ReadWoWString("string73", bits73);

            packet.ReadWoWString("Guild Name", guildNameLen);

            if (bit2D4)
            {
                packet.ReadInt32("Int2C8");
                packet.ReadInt32("Int2D0");
                packet.ReadInt32("Int2CC");
            }
        }

        [Parser(Opcode.SMSG_WEATHER)]
        public static void HandleWeatherStatus(Packet packet)
        {
            packet.ReadEnum<WeatherState>("State", TypeCode.Int32);
            packet.ReadSingle("Grade");
            packet.ReadBit("Unk Bit"); // Type
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS)]
        public static void HandleFeatureSystemStatus(Packet packet)
        {
            packet.ReadInt32("Scroll of Resurrections Per Day");
            packet.ReadInt32("Scroll of Resurrections Remaining");

            packet.ReadInt32("Realm Id?");
            packet.ReadByte("Complain System Status");
            packet.ReadInt32("Unused Int32");

            packet.ReadBit("bit26");
            packet.ReadBit("Shop Enabled");

            packet.ReadBit("bit54");
            packet.ReadBit("bit30");
            packet.ReadBit("bit38");
            packet.ReadBit("bit25");
            packet.ReadBit("bit24");

            var sessionTimeAlert = packet.ReadBit("Session Time Alert");

            packet.ReadBit("bit28");

            var quickTicket = packet.ReadBit("EuropaTicketSystemEnabled");

            if (sessionTimeAlert)
            {
                packet.ReadInt32("Int10");
                packet.ReadInt32("Int18");
                packet.ReadInt32("Int14");
            }

            if (quickTicket)
            {
                packet.ReadInt32("Unk5");
                packet.ReadInt32("Unk6");
                packet.ReadInt32("Unk7");
                packet.ReadInt32("Unk8");
            }
        }

        [Parser(Opcode.CMSG_SPELLCLICK)]
        public static void HandleSpellClick(Packet packet)
        {
            var guid = new byte[8];
            packet.StartBitStream(guid, 7, 4, 0, 3, 6, 5);
            packet.ReadBit("unk");
            packet.StartBitStream(guid, 1, 2);
            packet.ParseBitStream(guid, 6, 1, 5, 4, 7, 2, 3, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_NEUTRALPLAYERFACTIONSELECTRESULT)]
        public static void HandleFactionSelect(Packet packet)
        {
            packet.ReadUInt32("Option");
        }

        [Parser(Opcode.SMSG_NOTIFICATION)]
        public static void HandleNotification2(Packet packet)
        {
            var len = packet.ReadBits(11);
            packet.ReadWoWString("Text", len);
        }

        [Parser(Opcode.SMSG_ACTIVATETAXIREPLY)]
        public static void HandleActivateTaxiReply(Packet packet)
        {
            packet.ReadEnum<TaxiError>("Result", 4);
        }

        [Parser(Opcode.CMSG_ACTIVATETAXI)]
        public static void HandleActivateTaxi(Packet packet)
        {
            packet.ReadUInt32("Node 2 ID");
            packet.ReadUInt32("Node 1y ID");
            var guid = new byte[8];
            packet.StartBitStream(guid, 4, 0, 1, 2, 5, 6, 7, 3);
            packet.ReadXORBytes(guid, 1, 0, 6, 5, 2, 4, 3, 7);
            packet.WriteGuid("Guid", guid);

        }
        [Parser(Opcode.CMSG_TAXIQUERYAVAILABLENODES)]
        public static void HandleTaxiStatusQuery(Packet packet)
        {
            var guid = packet.StartBitStream(7, 1, 0, 4, 2, 5, 6, 3);
            packet.ParseBitStream(guid, 0, 3, 7, 5, 2, 6, 4, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SHOWTAXINODES)]
        public static void HandleShowTaxiNodes434(Packet packet)
        {
            packet.ReadBit("unk");
            var guid = packet.StartBitStream(3, 0, 4, 2, 1, 7, 6, 5);
            var count = packet.ReadBits("Count", 24);
            packet.ParseBitStream(guid, 0, 3);
            packet.ReadUInt32("Current Node ID");
            packet.ParseBitStream(guid, 5, 2, 6, 1, 7, 4);

            for (int i = 0; i < count; ++i)
                packet.ReadByte("NodeMask", i);

            packet.WriteGuid("Guid", guid);
        }
        [Parser(Opcode.CMSG_ACTIVATETAXIEXPRESS)]
        public static void HandleActiaveTaxiExpress(Packet packet)
        {
            var guid = new byte[8];
            packet.StartBitStream(guid, 6, 7);
            var count = packet.ReadBits("Count", 22);
            packet.StartBitStream(guid, 2, 0, 4, 3, 1, 5);

            packet.ParseBitStream(guid, 2, 7, 1);
            for (int i = 0; i < count; ++i)
                packet.ReadUInt32("Node", i);

            packet.ParseBitStream(guid, 0, 5, 3, 6, 4);
            packet.WriteGuid("Guid", guid);
        }
    }
}


using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.SMSG_WEATHER)]
        public static void HandleWeatherStatus(Packet packet)
        {
            var grade = packet.ReadSingle("Grade");
            var state = packet.ReadInt32E<WeatherState>("State");
            var unk = packet.ReadBit("Unk Bit"); // Type

            Storage.WeatherUpdates.Add(new WeatherUpdate
            {
                MapId = CoreParsers.MovementHandler.CurrentMapId,
                ZoneId = 0, // fixme
                State = state,
                Grade = grade,
                Unk = unk
            }, packet.TimeSpan);
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

        [Parser(Opcode.CMSG_TIME_SYNC_RESPONSE)]
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
            packet.ReadBool("Is On Tournament Realm");

            if (bit14)
                packet.ReadInt32("Int10");

            if (bit24)
                packet.ReadInt32("Int1C");

            if (bit30)
                packet.ReadInt32("Int2C");
        }

        [Parser(Opcode.CMSG_AREA_TRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            var entry = packet.ReadEntry("Area Trigger Id");
            packet.ReadBit("Unk bit1");
            packet.ReadBit("Unk bit2");

            packet.AddSniffData(StoreNameType.AreaTrigger, entry.Key, "AREATRIGGER");
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

        [Parser(Opcode.CMSG_REQUEST_HONOR_STATS)]
        public static void HandleRequestHonorStats(Packet packet)
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

        [Parser(Opcode.CMSG_WHO)]
        public static void HandleWhoRequest(Packet packet)
        {
            packet.ReadInt32("RaceMask");
            packet.ReadInt32("Max Level");
            packet.ReadInt32("Min Level");
            packet.ReadInt32("ClassMask");
            var guildNameLen = packet.ReadBits(7);
            packet.ReadBit("bit2C6");
            var patterns = packet.ReadBits(3);
            packet.ReadBit("bit2C5");
            var zones = packet.ReadBits(4);

            var bits2B8 = new uint[patterns];

            for (var i = 0; i < patterns; ++i)
                bits2B8[i] = packet.ReadBits(7);

            var bits73 = packet.ReadBits(9);
            var PlayerNameLen = packet.ReadBits(6);
            packet.ReadBit("bit2C4");
            var bit2D4 = packet.ReadBit();
            var bits1AB = packet.ReadBits(9);

            packet.ReadWoWString("string73", bits73);

            for (var i = 0; i < zones; ++i)
                packet.ReadInt32<ZoneId>("Zone Id");

            packet.ReadWoWString("Guild Name", guildNameLen);
            packet.ReadWoWString("string1AB", bits1AB);
            packet.ReadWoWString("Player Name", PlayerNameLen);

            for (var i = 0; i < patterns; ++i)
                packet.ReadWoWString("Pattern", bits2B8[i], i);

            if (bit2D4)
            {
                packet.ReadInt32("Int2C8");
                packet.ReadInt32("Int2D0");
                packet.ReadInt32("Int2CC");
            }
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

                playerGUID[i][1] = packet.ReadBit();
                playerGUID[i][2] = packet.ReadBit();
                guildGUID[i][3] = packet.ReadBit();
                guildNameLength[i] = packet.ReadBits(7);
                guildGUID[i][0] = packet.ReadBit();
                accountId[i][6] = packet.ReadBit();
                playerGUID[i][6] = packet.ReadBit();
                playerGUID[i][4] = packet.ReadBit();
                playerGUID[i][7] = packet.ReadBit();
                accountId[i][4] = packet.ReadBit();
                guildGUID[i][1] = packet.ReadBit();
                accountId[i][0] = packet.ReadBit();
                guildGUID[i][4] = packet.ReadBit();
                playerGUID[i][0] = packet.ReadBit();
                guildGUID[i][5] = packet.ReadBit();
                bitED[i] = packet.ReadBit();
                bit214[i] = packet.ReadBit();
                accountId[i][7] = packet.ReadBit();

                bits14[i] = new uint[5];
                for (var j = 0; j < 5; ++j)
                    bits14[i][j] = packet.ReadBits(7);

                guildGUID[i][7] = packet.ReadBit();
                guildGUID[i][2] = packet.ReadBit();
                accountId[i][2] = packet.ReadBit();
                accountId[i][5] = packet.ReadBit();
                accountId[i][3] = packet.ReadBit();
                playerNameLength[i] = packet.ReadBits(6);
                playerGUID[i][3] = packet.ReadBit();
                accountId[i][1] = packet.ReadBit();
                playerGUID[i][5] = packet.ReadBit();
                guildGUID[i][6] = packet.ReadBit();
            }

            for (var i = 0; i < counter; ++i)
            {
                packet.ReadXORByte(accountId[i], 7);
                packet.ReadByte("Level", i);
                packet.ReadXORByte(playerGUID[i], 3);
                packet.ReadInt32("RealmID", i);
                packet.ReadXORByte(playerGUID[i], 5);
                packet.ReadXORByte(guildGUID[i], 1);
                packet.ReadByteE<Gender>("Gender", i);
                packet.ReadXORByte(playerGUID[i], 7);
                packet.ReadInt32("Unk1", i);
                packet.ReadByteE<Race>("Race", i);
                packet.ReadXORByte(guildGUID[i], 0);
                packet.ReadXORByte(guildGUID[i], 4);
                packet.ReadXORByte(accountId[i], 0);
                packet.ReadXORByte(playerGUID[i], 4);
                packet.ReadXORByte(guildGUID[i], 3);
                packet.ReadXORByte(playerGUID[i], 0);
                packet.ReadWoWString("Guild Name", guildNameLength[i], i);
                packet.ReadXORByte(accountId[i], 2);
                packet.ReadXORByte(playerGUID[i], 2);
                packet.ReadXORByte(playerGUID[i], 6);

                packet.ReadByteE<Class>("Class", i);

                packet.ReadXORByte(accountId[i], 5);
                packet.ReadXORByte(guildGUID[i], 2);

                packet.ReadWoWString("Player Name", playerNameLength[i], i);

                packet.ReadInt32("RealmID", i);

                packet.ReadXORByte(playerGUID[i], 1);
                packet.ReadXORByte(accountId[i], 1);

                packet.ReadInt32<ZoneId>("Zone Id", i);

                packet.ReadXORByte(guildGUID[i], 7);
                packet.ReadXORByte(guildGUID[i], 6);
                packet.ReadXORByte(accountId[i], 3);
                packet.ReadXORByte(accountId[i], 4);
                packet.ReadXORByte(accountId[i], 6);
                packet.ReadXORByte(guildGUID[i], 5);

                for (var j = 0; j < 5; ++j)
                    packet.ReadWoWString("String14", bits14[i][j]);

                packet.WriteGuid("PlayerGUID", playerGUID[i], i);
                packet.WriteGuid("GuildGUID", guildGUID[i], i);
                packet.AddValue("Account", BitConverter.ToUInt64(accountId[i], 0), i);
            }
        }

        [Parser(Opcode.TEST_422_13022)]
        public static void Handletest(Packet packet)
        {
            var guid2 = new byte[8];

            guid2[5] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            var bit1D = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            packet.ReadBit("bit1C");
            guid2[0] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 5);
            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 7);

            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_VIGNETTE_UPDATE)]
        public static void HandleSetVignette(Packet packet)
        {

            packet.ReadBit("bit20");
            var bits10 = packet.ReadBits(24);

            var guid1 = new byte[bits10][];

            for (var i = 0; i < bits10; ++i)
            {
                guid1[i] = new byte[8];
                packet.StartBitStream(guid1[i], 0, 4, 3, 6, 5, 2, 7, 1);
            }

            var bits34 = packet.ReadBits(20);

            var guid2 = new byte[bits34][];

            for (var i = 0; i < bits34; ++i)
            {
                guid2[i] = new byte[8];
                packet.StartBitStream(guid2[i], 6, 3, 7, 1, 5, 4, 0, 2);
            }

            var bits44 = packet.ReadBits(24);

            var guid3 = new byte[bits44][];

            for (var i = 0; i < bits44; ++i)
            {
                guid3[i] = new byte[8];
                packet.StartBitStream(guid3[i], 0, 6, 7, 1, 4, 3, 2, 5);
            }

            var bits24 = packet.ReadBits(24);

            var guid4 = new byte[bits24][];

            for (var i = 0; i < bits24; ++i)
            {
                guid4[i] = new byte[8];
                packet.StartBitStream(guid4[i], 4, 5, 2, 3, 1, 6, 7, 0);
            }

            var bits54 = packet.ReadBits(20);

            var guid5 = new byte[bits54][];

            for (var i = 0; i < bits54; ++i)
            {
                guid5[i] = new byte[8];
                packet.StartBitStream(guid5[i], 6, 1, 7, 3, 5, 2, 0, 4);
            }

            for (var i = 0; i < bits54; ++i)
            {
                packet.ReadXORByte(guid5[i], 5);
                packet.ReadXORByte(guid5[i], 0);
                packet.ReadXORByte(guid5[i], 7);
                packet.ReadSingle("1");
                packet.ReadSingle("2");
                packet.ReadXORByte(guid5[i], 1);
                packet.ReadXORByte(guid5[i], 3);
                packet.ReadXORByte(guid5[i], 4);
                packet.ReadInt32("Vignette Id");
                packet.ReadXORByte(guid5[i], 6);
                packet.ReadXORByte(guid5[i], 7);
                packet.ReadSingle("3");

                packet.WriteGuid("Guid5", guid5[i]);
            }

            for (var i = 0; i < bits34; ++i)
            {
                packet.ReadXORByte(guid2[i], 0);
                packet.ReadXORByte(guid2[i], 5);
                packet.ReadXORByte(guid2[i], 6);
                packet.ReadXORByte(guid2[i], 2);
                packet.ReadInt32("Vignette Id");
                packet.ReadXORByte(guid2[i], 4);
                packet.ReadXORByte(guid2[i], 7);
                packet.ReadSingle("1");
                packet.ReadSingle("2");
                packet.ReadXORByte(guid2[i], 3);
                packet.ReadSingle("3");
                packet.ReadXORByte(guid2[i], 1);

                packet.WriteGuid("Guid2", guid2[i]);
            }

            for (var i = 0; i < bits24; ++i)
            {
                packet.ParseBitStream(guid4[i], 5, 2, 1, 0, 7, 4, 3, 6);
                packet.WriteGuid("Guid4", guid4[i]);
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.ParseBitStream(guid1[i], 5, 1, 2, 0, 6, 7, 4, 3);
                packet.WriteGuid("Guid1", guid1[i]);
            }

            for (var i = 0; i < bits44; ++i)
            {
                packet.ParseBitStream(guid3[i], 3, 6, 4, 2, 1, 7, 5, 0);
                packet.WriteGuid("Guid3", guid3[i]);
            }
        }
    }
}

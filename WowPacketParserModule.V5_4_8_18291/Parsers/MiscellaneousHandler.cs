using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class MiscellaneousHandler
    {
        [HasSniffData]
        [Parser(Opcode.CMSG_LOADING_SCREEN_NOTIFY)]
        public static void HandleClientEnterWorld(Packet packet)
        {
            var mapId = packet.ReadInt32<MapId>("MapID");
            packet.ReadBit("Showing");

            packet.AddSniffData(StoreNameType.Map, mapId, "LOAD_SCREEN");
        }

        [Parser(Opcode.CMSG_AREA_TRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            var entry = packet.ReadEntry("Area Trigger Id");
            packet.ReadBit("Unk bit1");
            packet.ReadBit("Unk bit2");

            packet.AddSniffData(StoreNameType.AreaTrigger, entry.Key, "AREATRIGGER");
        }

        [Parser(Opcode.CMSG_TIME_SYNC_RESPONSE)]
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
                packet.ReadByteE<Class>("Class", i);
                packet.ReadXORByte(accountId[i], 7);
                packet.ReadXORByte(playerGUID[i], 6);
                packet.ReadXORByte(playerGUID[i], 2);

                for (var j = 0; j < 5; ++j)
                    packet.ReadWoWString("String14", bits14[i][j]);

                packet.ReadXORByte(accountId[i], 2);
                packet.ReadXORByte(accountId[i], 3);
                packet.ReadByteE<Race>("Race", i);
                packet.ReadXORByte(guildGUID[i], 7);
                packet.ReadXORByte(accountId[i], 1);
                packet.ReadXORByte(accountId[i], 5);
                packet.ReadXORByte(accountId[i], 6);
                packet.ReadXORByte(playerGUID[i], 5);
                packet.ReadXORByte(accountId[i], 0);
                packet.ReadByteE<Gender>("Gender", i);
                packet.ReadXORByte(guildGUID[i], 5);
                packet.ReadByte("Level", i);
                packet.ReadInt32<ZoneId>("Zone Id", i);

                packet.WriteGuid("PlayerGUID", playerGUID[i], i);
                packet.WriteGuid("GuildGUID", guildGUID[i], i);
                packet.AddValue("Account", BitConverter.ToUInt64(accountId[i], 0), i);
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
                packet.ReadInt32<ZoneId>("Zone Id");

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
            var state = packet.ReadInt32E<WeatherState>("State");
            var grade = packet.ReadSingle("Grade");
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

        [Parser(Opcode.CMSG_SPELL_CLICK)]
        public static void HandleSpellClick(Packet packet)
        {
            var guidBytes = new byte[8];
            packet.StartBitStream(guidBytes, 7, 4, 0, 3, 6, 5);
            packet.ReadBit("unk");
            packet.StartBitStream(guidBytes, 1, 2);
            packet.ParseBitStream(guidBytes, 6, 1, 5, 4, 7, 2, 3, 0);

            packet.WriteGuid("Guid", guidBytes);

            var guid = new WowGuid64(BitConverter.ToUInt64(guidBytes, 0));
            if (guid.GetObjectType() == ObjectType.Unit)
                Storage.NpcSpellClicks.Add(guid, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_NEUTRAL_PLAYER_SELECT_FACTION)]
        public static void HandleFactionSelect(Packet packet)
        {
            packet.ReadUInt32("Option");
        }

        [Parser(Opcode.SMSG_PLAY_SOUND)]
        public static void HandlePlaySound(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 2, 3, 7, 6, 0, 5, 4, 1);

            var sound = packet.ReadUInt32("Sound Id");

            packet.ParseBitStream(guid, 3, 2, 4, 7, 5, 0, 6, 1);

            packet.WriteGuid("Guid", guid);

            Storage.Sounds.Add(sound, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_PLAY_OBJECT_SOUND)]
        public static void HandlePlayObjectSound(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[5] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[0] = packet.ReadBit();

            packet.ReadXORBytes(guid1, 6, 2);
            packet.ReadXORBytes(guid2, 2, 5);
            packet.ReadXORBytes(guid1, 7, 5, 3, 1);
            packet.ReadXORBytes(guid2, 3, 1);

            var sound = packet.ReadUInt32("Sound Id");

            packet.ReadXORByte(guid1, 4);
            packet.ReadXORBytes(guid2, 4, 7, 0, 6);
            packet.ReadXORByte(guid1, 0);

            packet.WriteGuid("Guid 1", guid1);
            packet.WriteGuid("Guid 2", guid2);

            Storage.Sounds.Add(sound, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_ACTIVATE_TAXI_REPLY)]
        public static void HandleActivateTaxiReply(Packet packet)
        {
            packet.ReadBitsE<TaxiError>("Result", 4);
        }

        [Parser(Opcode.CMSG_ACTIVATE_TAXI)]
        public static void HandleActivateTaxi(Packet packet)
        {
            packet.ReadUInt32("Node 2 ID");
            packet.ReadUInt32("Node 1y ID");
            var guid = new byte[8];
            packet.StartBitStream(guid, 4, 0, 1, 2, 5, 6, 7, 3);
            packet.ReadXORBytes(guid, 1, 0, 6, 5, 2, 4, 3, 7);
            packet.WriteGuid("Guid", guid);

        }
        [Parser(Opcode.CMSG_TAXI_QUERY_AVAILABLE_NODES)]
        public static void HandleTaxiStatusQuery(Packet packet)
        {
            var guid = packet.StartBitStream(7, 1, 0, 4, 2, 5, 6, 3);
            packet.ParseBitStream(guid, 0, 3, 7, 5, 2, 6, 4, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SHOW_TAXI_NODES)]
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
        [Parser(Opcode.CMSG_ACTIVATE_TAXI_EXPRESS)]
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


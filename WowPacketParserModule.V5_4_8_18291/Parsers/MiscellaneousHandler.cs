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
            var mapId = packet.Translator.ReadInt32<MapId>("MapID");
            packet.Translator.ReadBit("Showing");

            packet.AddSniffData(StoreNameType.Map, mapId, "LOAD_SCREEN");
        }

        [Parser(Opcode.CMSG_AREA_TRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            var entry = packet.Translator.ReadEntry("Area Trigger Id");
            packet.Translator.ReadBit("Unk bit1");
            packet.Translator.ReadBit("Unk bit2");

            packet.AddSniffData(StoreNameType.AreaTrigger, entry.Key, "AREATRIGGER");
        }

        [Parser(Opcode.CMSG_TIME_SYNC_RESPONSE)]
        public static void HandleTimeSyncResp(Packet packet)
        {
            packet.Translator.ReadUInt32("Counter");
            packet.Translator.ReadUInt32("Ticks");
        }

        [Parser(Opcode.SMSG_WHO)]
        public static void HandleWho(Packet packet)
        {
            var counter = (int)packet.Translator.ReadBits("List count", 6);

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

                accountId[i][2] = packet.Translator.ReadBit();
                playerGUID[i][2] = packet.Translator.ReadBit();
                accountId[i][7] = packet.Translator.ReadBit();
                guildGUID[i][5] = packet.Translator.ReadBit();
                guildNameLength[i] = packet.Translator.ReadBits(7);
                accountId[i][1] = packet.Translator.ReadBit();
                accountId[i][5] = packet.Translator.ReadBit();
                guildGUID[i][7] = packet.Translator.ReadBit();
                playerGUID[i][5] = packet.Translator.ReadBit();
                bitED[i] = packet.Translator.ReadBit();
                guildGUID[i][1] = packet.Translator.ReadBit();
                playerGUID[i][6] = packet.Translator.ReadBit();
                guildGUID[i][2] = packet.Translator.ReadBit();
                playerGUID[i][4] = packet.Translator.ReadBit();
                guildGUID[i][0] = packet.Translator.ReadBit();
                guildGUID[i][3] = packet.Translator.ReadBit();
                accountId[i][6] = packet.Translator.ReadBit();
                bit214[i] = packet.Translator.ReadBit();
                playerGUID[i][1] = packet.Translator.ReadBit();
                guildGUID[i][4] = packet.Translator.ReadBit();
                accountId[i][0] = packet.Translator.ReadBit();

                bits14[i] = new uint[5];
                for (var j = 0; j < 5; ++j)
                    bits14[i][j] = packet.Translator.ReadBits(7);

                playerGUID[i][3] = packet.Translator.ReadBit();
                guildGUID[i][6] = packet.Translator.ReadBit();
                playerGUID[i][0] = packet.Translator.ReadBit();
                accountId[i][4] = packet.Translator.ReadBit();
                accountId[i][3] = packet.Translator.ReadBit();
                playerGUID[i][7] = packet.Translator.ReadBit();
                playerNameLength[i] = packet.Translator.ReadBits(6);
            }

            for (var i = 0; i < counter; ++i)
            {
                packet.Translator.ReadXORByte(playerGUID[i], 1);
                packet.Translator.ReadInt32("RealmID", i);
                packet.Translator.ReadXORByte(playerGUID[i], 7);
                packet.Translator.ReadInt32("RealmID", i);
                packet.Translator.ReadXORByte(playerGUID[i], 4);
                packet.Translator.ReadWoWString("Player Name", playerNameLength[i], i);
                packet.Translator.ReadXORByte(guildGUID[i], 1);
                packet.Translator.ReadXORByte(playerGUID[i], 0);
                packet.Translator.ReadXORByte(guildGUID[i], 2);
                packet.Translator.ReadXORByte(guildGUID[i], 0);
                packet.Translator.ReadXORByte(guildGUID[i], 4);
                packet.Translator.ReadXORByte(playerGUID[i], 3);
                packet.Translator.ReadXORByte(guildGUID[i], 6);
                packet.Translator.ReadInt32("Unk1", i);
                packet.Translator.ReadWoWString("Guild Name", guildNameLength[i], i);
                packet.Translator.ReadXORByte(guildGUID[i], 3);
                packet.Translator.ReadXORByte(accountId[i], 4);
                packet.Translator.ReadByteE<Class>("Class", i);
                packet.Translator.ReadXORByte(accountId[i], 7);
                packet.Translator.ReadXORByte(playerGUID[i], 6);
                packet.Translator.ReadXORByte(playerGUID[i], 2);

                for (var j = 0; j < 5; ++j)
                    packet.Translator.ReadWoWString("String14", bits14[i][j]);

                packet.Translator.ReadXORByte(accountId[i], 2);
                packet.Translator.ReadXORByte(accountId[i], 3);
                packet.Translator.ReadByteE<Race>("Race", i);
                packet.Translator.ReadXORByte(guildGUID[i], 7);
                packet.Translator.ReadXORByte(accountId[i], 1);
                packet.Translator.ReadXORByte(accountId[i], 5);
                packet.Translator.ReadXORByte(accountId[i], 6);
                packet.Translator.ReadXORByte(playerGUID[i], 5);
                packet.Translator.ReadXORByte(accountId[i], 0);
                packet.Translator.ReadByteE<Gender>("Gender", i);
                packet.Translator.ReadXORByte(guildGUID[i], 5);
                packet.Translator.ReadByte("Level", i);
                packet.Translator.ReadInt32<ZoneId>("Zone Id", i);

                packet.Translator.WriteGuid("PlayerGUID", playerGUID[i], i);
                packet.Translator.WriteGuid("GuildGUID", guildGUID[i], i);
                packet.AddValue("Account", BitConverter.ToUInt64(accountId[i], 0), i);
            }
        }

        [Parser(Opcode.CMSG_SET_SELECTION)]
        public static void HandleSetSelection(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(7, 6, 5, 4, 3, 2, 1, 0);
            packet.Translator.ParseBitStream(guid, 0, 7, 3, 5, 1, 4, 6, 2);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_WHO)]
        public static void HandleWhoRequest(Packet packet)
        {
            packet.Translator.ReadInt32("ClassMask");
            packet.Translator.ReadInt32("RaceMask");
            packet.Translator.ReadInt32("Max Level");
            packet.Translator.ReadInt32("Min Level");


            packet.Translator.ReadBit("bit2C4");
            packet.Translator.ReadBit("bit2C6");
            var bit2D4 = packet.Translator.ReadBit();
            var bits1AB = packet.Translator.ReadBits(9);
            packet.Translator.ReadBit("bit2C5");
            var PlayerNameLen = packet.Translator.ReadBits(6);
            var zones = packet.Translator.ReadBits(4);
            var bits73 = packet.Translator.ReadBits(9);
            var guildNameLen = packet.Translator.ReadBits(7);
            var patterns = packet.Translator.ReadBits(3);

            var bits2B8 = new uint[patterns];
            for (var i = 0; i < patterns; ++i)
                bits2B8[i] = packet.Translator.ReadBits(7);

            for (var i = 0; i < patterns; ++i)
                packet.Translator.ReadWoWString("Pattern", bits2B8[i], i);

            packet.Translator.ReadWoWString("string1AB", bits1AB);

            for (var i = 0; i < zones; ++i)
                packet.Translator.ReadInt32<ZoneId>("Zone Id");

            packet.Translator.ReadWoWString("Player Name", PlayerNameLen);

            packet.Translator.ReadWoWString("string73", bits73);

            packet.Translator.ReadWoWString("Guild Name", guildNameLen);

            if (bit2D4)
            {
                packet.Translator.ReadInt32("Int2C8");
                packet.Translator.ReadInt32("Int2D0");
                packet.Translator.ReadInt32("Int2CC");
            }
        }

        [Parser(Opcode.SMSG_WEATHER)]
        public static void HandleWeatherStatus(Packet packet)
        {
            WeatherState state = packet.Translator.ReadInt32E<WeatherState>("State");
            float grade = packet.Translator.ReadSingle("Grade");
            Bit unk = packet.Translator.ReadBit("Unk Bit"); // Type

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
            packet.Translator.ReadInt32("Scroll of Resurrections Per Day");
            packet.Translator.ReadInt32("Scroll of Resurrections Remaining");

            packet.Translator.ReadInt32("Realm Id?");
            packet.Translator.ReadByte("Complain System Status");
            packet.Translator.ReadInt32("Unused Int32");

            packet.Translator.ReadBit("bit26");
            packet.Translator.ReadBit("Shop Enabled");

            packet.Translator.ReadBit("bit54");
            packet.Translator.ReadBit("bit30");
            packet.Translator.ReadBit("bit38");
            packet.Translator.ReadBit("bit25");
            packet.Translator.ReadBit("bit24");

            var sessionTimeAlert = packet.Translator.ReadBit("Session Time Alert");

            packet.Translator.ReadBit("bit28");

            var quickTicket = packet.Translator.ReadBit("EuropaTicketSystemEnabled");

            if (sessionTimeAlert)
            {
                packet.Translator.ReadInt32("Int10");
                packet.Translator.ReadInt32("Int18");
                packet.Translator.ReadInt32("Int14");
            }

            if (quickTicket)
            {
                packet.Translator.ReadInt32("Unk5");
                packet.Translator.ReadInt32("Unk6");
                packet.Translator.ReadInt32("Unk7");
                packet.Translator.ReadInt32("Unk8");
            }
        }

        [Parser(Opcode.CMSG_SPELL_CLICK)]
        public static void HandleSpellClick(Packet packet)
        {
            var guidBytes = new byte[8];
            packet.Translator.StartBitStream(guidBytes, 7, 4, 0, 3, 6, 5);
            packet.Translator.ReadBit("unk");
            packet.Translator.StartBitStream(guidBytes, 1, 2);
            packet.Translator.ParseBitStream(guidBytes, 6, 1, 5, 4, 7, 2, 3, 0);

            packet.Translator.WriteGuid("Guid", guidBytes);

            WowGuid64 guid = new WowGuid64(BitConverter.ToUInt64(guidBytes, 0));
            if (guid.GetObjectType() == ObjectType.Unit)
                Storage.NpcSpellClicks.Add(guid, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_NEUTRAL_PLAYER_SELECT_FACTION)]
        public static void HandleFactionSelect(Packet packet)
        {
            packet.Translator.ReadUInt32("Option");
        }

        [Parser(Opcode.SMSG_PLAY_SOUND)]
        public static void HandlePlaySound(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 2, 3, 7, 6, 0, 5, 4, 1);

            uint sound = packet.Translator.ReadUInt32("Sound Id");

            packet.Translator.ParseBitStream(guid, 3, 2, 4, 7, 5, 0, 6, 1);
            packet.Translator.WriteGuid("Guid", guid);

            Storage.Sounds.Add(sound, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_PLAY_OBJECT_SOUND)]
        public static void HandlePlayObjectSound(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[5] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();

            packet.Translator.ReadXORBytes(guid1, 6, 2);
            packet.Translator.ReadXORBytes(guid2, 2, 5);
            packet.Translator.ReadXORBytes(guid1, 7, 5, 3, 1);
            packet.Translator.ReadXORBytes(guid2, 3, 1);

            uint sound = packet.Translator.ReadUInt32("Sound Id");

            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORBytes(guid2, 4, 7, 0, 6);
            packet.Translator.ReadXORByte(guid1, 0);

            packet.Translator.WriteGuid("Guid 1", guid1);
            packet.Translator.WriteGuid("Guid 2", guid2);

            Storage.Sounds.Add(sound, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_ACTIVATE_TAXI_REPLY)]
        public static void HandleActivateTaxiReply(Packet packet)
        {
            packet.Translator.ReadBitsE<TaxiError>("Result", 4);
        }

        [Parser(Opcode.CMSG_ACTIVATE_TAXI)]
        public static void HandleActivateTaxi(Packet packet)
        {
            packet.Translator.ReadUInt32("Node 2 ID");
            packet.Translator.ReadUInt32("Node 1y ID");
            var guid = new byte[8];
            packet.Translator.StartBitStream(guid, 4, 0, 1, 2, 5, 6, 7, 3);
            packet.Translator.ReadXORBytes(guid, 1, 0, 6, 5, 2, 4, 3, 7);
            packet.Translator.WriteGuid("Guid", guid);

        }
        [Parser(Opcode.CMSG_TAXI_QUERY_AVAILABLE_NODES)]
        public static void HandleTaxiStatusQuery(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(7, 1, 0, 4, 2, 5, 6, 3);
            packet.Translator.ParseBitStream(guid, 0, 3, 7, 5, 2, 6, 4, 1);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SHOW_TAXI_NODES)]
        public static void HandleShowTaxiNodes434(Packet packet)
        {
            packet.Translator.ReadBit("unk");
            var guid = packet.Translator.StartBitStream(3, 0, 4, 2, 1, 7, 6, 5);
            var count = packet.Translator.ReadBits("Count", 24);
            packet.Translator.ParseBitStream(guid, 0, 3);
            packet.Translator.ReadUInt32("Current Node ID");
            packet.Translator.ParseBitStream(guid, 5, 2, 6, 1, 7, 4);

            for (int i = 0; i < count; ++i)
                packet.Translator.ReadByte("NodeMask", i);

            packet.Translator.WriteGuid("Guid", guid);
        }
        [Parser(Opcode.CMSG_ACTIVATE_TAXI_EXPRESS)]
        public static void HandleActiaveTaxiExpress(Packet packet)
        {
            var guid = new byte[8];
            packet.Translator.StartBitStream(guid, 6, 7);
            var count = packet.Translator.ReadBits("Count", 22);
            packet.Translator.StartBitStream(guid, 2, 0, 4, 3, 1, 5);

            packet.Translator.ParseBitStream(guid, 2, 7, 1);
            for (int i = 0; i < count; ++i)
                packet.Translator.ReadUInt32("Node", i);

            packet.Translator.ParseBitStream(guid, 0, 5, 3, 6, 4);
            packet.Translator.WriteGuid("Guid", guid);
        }
    }
}


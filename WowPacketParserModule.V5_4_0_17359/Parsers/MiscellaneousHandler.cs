using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.CMSG_UNKNOWN_903)]
        public static void HandleUnknow903(Packet packet)
        {
            uint len = packet.Translator.ReadBits(9);
            packet.Translator.ReadWoWString("File name", len);
        }

        [HasSniffData]
        [Parser(Opcode.CMSG_LOADING_SCREEN_NOTIFY)]
        public static void HandleClientEnterWorld(Packet packet)
        {
            uint mapId = packet.Translator.ReadUInt32<MapId>("MapID");
            packet.Translator.ReadBit("Showing");
        }

        [Parser(Opcode.CMSG_SET_SELECTION)]
        public static void HandleSetSelection(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(1, 3, 4, 6, 0, 5, 7, 2);
            packet.Translator.ParseBitStream(guid, 7, 6, 0, 2, 3, 1, 4, 5);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_REALM_SPLIT)]
        public static void HandleClientRealmSplit(Packet packet)
        {
            packet.Translator.ReadInt32E<ClientSplitState>("Client State");
        }

        [Parser(Opcode.SMSG_SET_TIME_ZONE_INFORMATION)]
        public static void HandleSetTimeZoneInformation(Packet packet)
        {
            uint len1 = packet.Translator.ReadBits(7);
            uint len2 = packet.Translator.ReadBits(7);
            packet.Translator.ReadWoWString("Server Location", len1);
            packet.Translator.ReadWoWString("Server Location", len2);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD)]
        public static void HandleNewWorld(Packet packet)
        {
            Vector4 pos = new Vector4();
            pos.Y = packet.Translator.ReadSingle();
            pos.O = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            pos.X = packet.Translator.ReadSingle();
            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.Translator.ReadInt32<MapId>("Map");
            packet.AddValue("Position", pos);

            packet.AddSniffData(StoreNameType.Map, (int)CoreParsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }

        [Parser(Opcode.SMSG_REALM_SPLIT)]
        public static void HandleServerRealmSplit(Packet packet)
        {
            uint len = packet.Translator.ReadBits(7);
            packet.Translator.ReadInt32E<PendingSplitState>("Split State");
            packet.Translator.ReadInt32E<ClientSplitState>("Client State");
            packet.Translator.ReadWoWString("Split Date", len);
        }

        [Parser(Opcode.CMSG_AREA_TRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            var entry = packet.Translator.ReadEntry("Area Trigger Id");
            packet.Translator.ReadBit("Unk bit1");
            packet.Translator.ReadBit("Unk bit2");

            packet.AddSniffData(StoreNameType.AreaTrigger, entry.Key, "AREATRIGGER");
        }

        [Parser(Opcode.SMSG_WEATHER)]
        public static void HandleWeatherStatus(Packet packet)
        {
            Bit unk = packet.Translator.ReadBit("Unk bit");
            float grade = packet.Translator.ReadSingle("Grade");
            WeatherState state = packet.Translator.ReadInt32E<WeatherState>("State");

            Storage.WeatherUpdates.Add(new WeatherUpdate
            {
                MapId = CoreParsers.MovementHandler.CurrentMapId,
                ZoneId = 0, // fixme
                State = state,
                Grade = grade,
                Unk = unk
            }, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_HOTFIX_NOTIFY_BLOB)]
        public static void HandleHotfixInfo(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 20);

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("Hotfixed entry", i);
                packet.Translator.ReadTime("Hotfix date", i);
                packet.Translator.ReadInt32E<DB2Hash>("Hotfix DB2 File", i);
            }
        }

        [Parser(Opcode.SMSG_PLAY_SOUND)]
        public static void HandlePlaySound(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 6, 7, 5, 2, 1, 4, 0, 3);
            packet.Translator.ParseBitStream(guid, 7, 0, 5, 4, 3, 1, 2, 6);

            var sound = packet.Translator.ReadUInt32("Sound Id");
            packet.Translator.WriteGuid("Guid", guid);

            Storage.Sounds.Add(sound, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_WORLD_SERVER_INFO)]
        public static void HandleWorldServerInfo(Packet packet)
        {
            packet.Translator.ReadInt32("Instance Difficulty ID");
            packet.Translator.ReadTime("Last Weekly Reset");
            packet.Translator.ReadByte("Byte18");

            var bit20 = packet.Translator.ReadBit();
            var bit38 = packet.Translator.ReadBit();
            var bit2C = packet.Translator.ReadBit();
            var bit14 = packet.Translator.ReadBit();

            if (bit20)
                packet.Translator.ReadInt32("Int1C");
            if (bit38)
                packet.Translator.ReadInt32("Int34");
            if (bit2C)
                packet.Translator.ReadInt32("Int28");
            if (bit14)
                packet.Translator.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS)]
        public static void HandleFeatureSystemStatus(Packet packet)
        {
            packet.Translator.ReadInt32("Scroll of Resurrections Remaining");
            packet.Translator.ReadInt32("Realm Id?");
            packet.Translator.ReadByte("Complain System Status");
            packet.Translator.ReadInt32("Unused Int32");
            packet.Translator.ReadInt32("Scroll of Resurrections Per Day");

            packet.Translator.ReadBit("bit26");
            packet.Translator.ReadBit("bit54");
            packet.Translator.ReadBit("bit30");
            var sessionTimeAlert = packet.Translator.ReadBit("Session Time Alert");
            packet.Translator.ReadBit("bit38");
            var quickTicket = packet.Translator.ReadBit("EuropaTicketSystemEnabled");
            packet.Translator.ReadBit("bit25");
            packet.Translator.ReadBit("bit24");
            packet.Translator.ReadBit("bit28");

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

        [Parser(Opcode.SMSG_PLAY_SCENE)]
        public static void HandleUnknown425(Packet packet)
        {
            var guid = new byte[8];
            // Positions where the scene should start?
            packet.Translator.ReadSingle("Z");
            packet.Translator.ReadSingle("Y");
            packet.Translator.ReadSingle("X");

            var bit34 = !packet.Translator.ReadBit();
            var bit1C = !packet.Translator.ReadBit();
            var bit24 = !packet.Translator.ReadBit();

            packet.Translator.ReadBit(); // fake bit

            packet.Translator.StartBitStream(guid, 4, 2, 3, 6, 1, 5, 0, 7);

            var bit18 = !packet.Translator.ReadBit();
            var bit0 = !packet.Translator.ReadBit();
            if (bit34)
                packet.Translator.ReadSingle("O");

            packet.Translator.ParseBitStream(guid, 4, 6, 0, 5, 2, 7, 3, 1);

            if (bit0)
                packet.Translator.ReadInt32("Unk 1");

            if (bit24)
                packet.Translator.ReadInt32("Scene Script Package Id");

            if (bit18)
                packet.Translator.ReadInt32("Unk 2");

            if (bit1C)
                packet.Translator.ReadInt32("Unk 3");

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_REQUEST_CEMETERY_LIST_RESPONSE)]
        public static void HandleRequestCemeteryListResponse(Packet packet)
        {
            packet.Translator.ReadBit("Is MicroDungeon"); // Named in WorldMapFrame.lua
            var count = packet.Translator.ReadBits("Count", 22);

            for (var i = 0; i < count; ++i)
                packet.Translator.ReadInt32("Cemetery Id", i);
        }

        [Parser(Opcode.SMSG_UNKNOWN_274)]
        public static void HandleUnknow274(Packet packet)
        {
            byte[][] guid1;
            byte[][] guid2;

            var counter = packet.Translator.ReadBits(19);

            guid1 = new byte[counter][];
            guid2 = new byte[counter][];
            for (var i = 0; i < counter; ++i)
            {
                guid1[i] = new byte[8];
                guid2[i] = new byte[8];

                packet.Translator.StartBitStream(guid2[i], 6, 3);
                packet.Translator.StartBitStream(guid1[i], 6, 1, 2, 3);
                packet.Translator.ReadBits(4);
                packet.Translator.StartBitStream(guid1[i], 5, 0, 7);
                packet.Translator.StartBitStream(guid2[i], 0, 7, 5);
                guid1[i][4] = packet.Translator.ReadBit();
                packet.Translator.StartBitStream(guid2[i], 1, 4, 2);
            }

            for (var i = 0; i < counter; ++i)
            {
                packet.Translator.ReadXORByte(guid1[i], 1);
                packet.Translator.ReadInt32("Int3Ch", i);
                packet.Translator.ReadXORByte(guid1[i], 2);
                packet.Translator.ReadXORByte(guid2[i], 6);
                packet.Translator.ReadXORByte(guid1[i], 4);
                packet.Translator.ReadXORByte(guid2[i], 7);
                packet.Translator.ReadXORByte(guid1[i], 6);
                packet.Translator.ReadXORByte(guid1[i], 7);

                packet.Translator.ReadInt32("Int40h", i);

                packet.Translator.ReadXORByte(guid2[i], 5);
                packet.Translator.ReadXORByte(guid1[i], 5);
                packet.Translator.ReadXORByte(guid2[i], 2);

                packet.Translator.ReadPackedTime("Time", i);
                packet.Translator.ReadInt32("Int14h", i);

                packet.Translator.ReadXORByte(guid2[i], 0);
                packet.Translator.ReadXORByte(guid1[i], 3);
                packet.Translator.ReadXORByte(guid2[i], 1);
                packet.Translator.ReadXORByte(guid1[i], 0);
                packet.Translator.ReadXORByte(guid2[i], 4);
                packet.Translator.ReadXORByte(guid2[i], 3);

                packet.Translator.WriteGuid("Guid1", guid1[i], i);
                packet.Translator.WriteGuid("Guid2", guid2[i], i);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_1024)]
        public static void HandleUnknown1024(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 7, 4, 1, 5, 0, 3, 6);
            packet.Translator.ReadBits("bits0", 3);
            guid[2] = packet.Translator.ReadBit();
            packet.Translator.ParseBitStream(guid, 3, 6, 0, 5, 1, 7, 4, 2);

            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_6775)]
        public static void HandleUnknown6775(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 6, 5, 7, 2, 4, 0, 1, 3);
            packet.Translator.ParseBitStream(guid, 1, 5, 0, 6, 4, 2, 3, 7);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_3)]
        public static void HandleUnknown3(Packet packet)
        {
            var guid = new byte[8];
            packet.Translator.StartBitStream(guid, 1, 3, 4, 0, 5, 6, 2, 7);
            packet.Translator.ParseBitStream(guid, 1, 4, 0, 6, 3, 7, 5, 2);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1457)]
        public static void HandleUnknown1457(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 5, 2, 3, 6, 7, 1, 0, 4);
            packet.Translator.ParseBitStream(guid, 1, 3);
            packet.Translator.ReadInt32("int18h");
            packet.Translator.ParseBitStream(guid, 7, 0, 5, 4, 2, 6);

            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5738)]
        public static void HandleUnknown5738(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 3, 0, 5, 4, 2, 7, 1, 6);
            packet.Translator.ParseBitStream(guid, 2, 6, 3);
            packet.Translator.ReadSingle("Speed");
            packet.Translator.ParseBitStream(guid, 0, 4, 1, 5, 7);

            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_56)]
        public static void HandleUnknown56(Packet packet)
        {
            packet.Translator.ReadInt32("Int14");
            packet.Translator.ReadInt32("Int10");
        }

        [Parser(Opcode.MSG_UNKNOWN_5125)]
        public static void HandleUnknown5125(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                var guid = new byte[8];

                packet.Translator.ReadByte("Unk 1 byte");
                packet.Translator.ReadByte("Unk 2 byte");

                packet.Translator.StartBitStream(guid, 1, 5, 7, 2, 3, 4, 0, 6);
                packet.Translator.ParseBitStream(guid, 2, 3, 7, 0, 6, 5, 1, 4);

                packet.Translator.WriteGuid("Guid", guid);
            }
            else
            {
                var guid = new byte[8];

                packet.Translator.StartBitStream(guid, 2, 7, 0, 6, 5, 3, 1, 4);
                packet.Translator.ParseBitStream(guid, 2, 0, 1, 7, 4, 5);
                packet.Translator.ReadInt32("Int18");
                packet.Translator.ParseBitStream(guid, 3, 6);

                packet.Translator.WriteGuid("GUID", guid);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_57)]
        public static void HandleUnknown57(Packet packet)
        {
            packet.Translator.ReadByte("Byte20");
            packet.Translator.ReadInt32("Int1C");
            packet.Translator.ReadUInt32<SpellId>("Spell ID");

            var bit10 = !packet.Translator.ReadBit();
            var bit14 = !packet.Translator.ReadBit();

            if (bit10)
                packet.Translator.ReadInt32("Int10");
            if (bit14)
                packet.Translator.ReadInt32("Int14");
        }

        [Parser(Opcode.SMSG_UNKNOWN_170)]
        public static void HandleUnknown170(Packet packet)
        {
            packet.Translator.ReadInt32("Int14");
            packet.Translator.ReadBits("bits10", 2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5963)]
        public static void HandleUnknown5963(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 1, 7, 4, 5, 2, 6, 0, 3);
            packet.Translator.ParseBitStream(guid, 1, 6, 5, 0);
            packet.Translator.ReadSingle("Speed");
            packet.Translator.ParseBitStream(guid, 4, 3, 2, 7);

            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_316)]
        public static void HandleUnknown316(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadInt32("Int10"); // sub_7B4A0D
            var len = packet.Translator.ReadInt32("Int78");

            packet.Translator.ReadBytes(len);

            guid[1] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("bit20");
            packet.Translator.StartBitStream(guid, 7, 2, 6, 3, 4, 5, 0);

            packet.Translator.ReadBit("bit21");
            packet.Translator.ParseBitStream(guid, 6, 1, 4, 2, 5, 0, 3, 7);

            packet.Translator.WriteGuid("Guid3", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2048)]
        public static void HandleUnknown2048(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid1[7] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid2, 6, 5);
            packet.Translator.StartBitStream(guid1, 3, 0, 6);
            guid2[2] = packet.Translator.ReadBit();
            var bit18 = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 1, 4);
            packet.Translator.StartBitStream(guid2, 4, 7, 3);
            guid1[2] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadSingle("Float2C");
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadSingle("Float30");
            packet.Translator.ReadSingle("Float1C");
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadInt32("Int24"); // SpellVisualKit?
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadInt16("Int20");
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadInt16("Int40");
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadSingle("Float28");
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 1);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_6418)]
        public static void HandleUnknown6418(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 0, 5, 7, 2, 1, 4, 6, 3);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadByte("Byte18");
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadByte("Byte19");
            packet.Translator.ReadXORByte(guid, 5);

            packet.Translator.WriteGuid("Guid2", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_443)]
        public static void HandleUnknown443(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.Translator.StartBitStream(guid1, 6, 2);
            packet.Translator.StartBitStream(guid2, 5, 6, 7, 4, 3, 2, 1);
            packet.Translator.StartBitStream(guid1, 1, 5, 7, 4, 3);
            guid2[0] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid2, 5);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_67)]
        public static void HandleUnknown67(Packet packet)
        {
            packet.Translator.ReadInt32("Count?");
        }

        [Parser(Opcode.SMSG_UNKNOWN_442)]
        public static void HandleUnknown442(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 5, 6, 7, 0, 4, 3, 1, 2);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadByte("Byte1D");
            packet.Translator.ReadByte("Byte1C");
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadXORByte(guid, 5);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2093)]
        public static void HandleUnknown2093(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 2, 6, 0, 7, 1, 3, 4, 5);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadSingle("Speed");
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 2);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4501)]
        public static void HandleUnknown4501(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var guid3 = new byte[8];

            var bit20 = false;
            var bit24 = false;

            var bit28 = packet.Translator.ReadBit();
            if (bit28)
            {
                guid1[0] = packet.Translator.ReadBit();
                guid1[1] = packet.Translator.ReadBit();
                guid1[6] = packet.Translator.ReadBit();
                guid1[4] = packet.Translator.ReadBit();
                bit24 = !packet.Translator.ReadBit();
                guid1[3] = packet.Translator.ReadBit();
                bit20 = !packet.Translator.ReadBit();

                packet.Translator.ReadBit(); // fake bit

                guid2[2] = packet.Translator.ReadBit();
                guid2[3] = packet.Translator.ReadBit();
                guid2[4] = packet.Translator.ReadBit();
                guid2[6] = packet.Translator.ReadBit();
                guid2[7] = packet.Translator.ReadBit();
                guid2[5] = packet.Translator.ReadBit();
                guid2[1] = packet.Translator.ReadBit();
                guid2[0] = packet.Translator.ReadBit();
                guid1[7] = packet.Translator.ReadBit();
                guid1[5] = packet.Translator.ReadBit();
                guid1[2] = packet.Translator.ReadBit();
            }

            var bit38 = packet.Translator.ReadBit();
            guid3[6] = packet.Translator.ReadBit();
            guid3[0] = packet.Translator.ReadBit();
            guid3[4] = packet.Translator.ReadBit();
            guid3[1] = packet.Translator.ReadBit();
            guid3[2] = packet.Translator.ReadBit();
            guid3[3] = packet.Translator.ReadBit();
            guid3[5] = packet.Translator.ReadBit();
            guid3[7] = packet.Translator.ReadBit();

            if (bit38)
            {
                packet.Translator.ReadInt32("Int30");
                packet.Translator.ReadInt32("Int34");
            }

            if (bit28)
            {
                packet.Translator.ReadXORByte(guid1, 0);

                if (bit20)
                    packet.Translator.ReadInt32("Int20");

                packet.Translator.ReadXORByte(guid1, 4);
                packet.Translator.ReadXORByte(guid1, 5);
                packet.Translator.ReadXORByte(guid2, 7);
                packet.Translator.ReadXORByte(guid2, 6);
                packet.Translator.ReadXORByte(guid2, 1);
                packet.Translator.ReadXORByte(guid2, 4);
                packet.Translator.ReadXORByte(guid2, 0);
                packet.Translator.ReadXORByte(guid2, 2);
                packet.Translator.ReadXORByte(guid2, 5);
                packet.Translator.ReadXORByte(guid2, 3);
                packet.Translator.ReadXORByte(guid1, 3);

                if (bit24)
                    packet.Translator.ReadByte("Byte24");

                packet.Translator.ReadXORByte(guid1, 1);
                packet.Translator.ReadXORByte(guid1, 7);
                packet.Translator.ReadXORByte(guid1, 2);
                packet.Translator.ReadXORByte(guid1, 6);

                packet.Translator.WriteGuid("Guid2", guid1);
                packet.Translator.WriteGuid("Guid3", guid2);
            }

            packet.Translator.ReadXORByte(guid3, 5);
            packet.Translator.ReadXORByte(guid3, 6);
            packet.Translator.ReadXORByte(guid3, 4);
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadXORByte(guid3, 7);
            packet.Translator.ReadXORByte(guid3, 1);
            packet.Translator.ReadXORByte(guid3, 3);
            packet.Translator.ReadInt32("Duration");
            packet.Translator.ReadXORByte(guid3, 2);
            packet.Translator.ReadXORByte(guid3, 0);

            packet.Translator.WriteGuid("Guid8", guid3);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1194)]
        public static void HandleUnknown1194(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 0, 3, 5, 4, 6, 7, 1, 2);
            packet.Translator.ParseBitStream(guid, 4, 6, 7, 0, 2, 5, 1, 3);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1166)]
        public static void HandleUnknown1166(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 2, 1, 7, 0, 5, 3, 4, 6);
            packet.Translator.ReadByte("Byte1D");
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadByte("Byte1C");
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 2);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_24)]
        public static void HandleUnknown24(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 3, 4, 2, 0, 5, 6, 1, 7);
            packet.Translator.ParseBitStream(guid, 4, 6, 5, 2, 1, 7, 3, 0);
            packet.Translator.ReadInt32("Current health");

            packet.Translator.WriteGuid("Guid2", guid);
        }

        [Parser(Opcode.MSG_UNKNOWN_4262)]
        public static void HandleUnknown4262(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
                packet.Translator.ReadInt32("Int10");
            else
            {
                var guid1 = new byte[8];
                byte[][] guid2;
                byte[][][] guid3 = null;
                byte[][][] guid4 = null;
                byte[][][] guid5 = null;
                byte[][][] guid6 = null;

                packet.Translator.StartBitStream(guid1, 6, 5, 7, 3, 1, 4);

                var bits10 = packet.Translator.ReadBits(19);

                var bits44 = new uint[bits10];
                var bit90 = new bool[bits10];
                var bits14 = new uint[bits10];
                var bits24 = new uint[bits10];
                var bits7C = new uint[bits10];
                var bits34 = new uint[bits10];
                var bits54 = new uint[bits10];
                var bits4 = new uint[bits10];

                guid2 = new byte[bits10][];
                guid3 = new byte[bits10][][];
                guid4 = new byte[bits10][][];
                guid5 = new byte[bits10][][];
                guid6 = new byte[bits10][][];

                for (var i = 0; i < bits10; ++i)
                {
                    bits44[i] = packet.Translator.ReadBits(22);
                    bit90[i] = packet.Translator.ReadBit();
                    bits14[i] = packet.Translator.ReadBits(21);

                    guid3[i] = new byte[bits14[i]][];
                    for (var j = 0; j < bits14[i]; ++j)
                    {
                        guid3[i][j] = new byte[8];
                        packet.Translator.StartBitStream(guid3[i][j], 7, 2, 6, 4, 1, 3, 5, 0);
                    }

                    bits24[i] = packet.Translator.ReadBits(21);

                    guid4[i] = new byte[bits24[i]][];
                    for (var j = 0; j < bits24[i]; ++j)
                    {
                        guid4[i][j] = new byte[8];
                        packet.Translator.StartBitStream(guid4[i][j], 4, 2, 1, 7, 6, 5, 3, 0);
                    }

                    if (bit90[i])
                    {
                        guid2[i] = new byte[8];
                        packet.Translator.StartBitStream(guid2[i], 0, 5, 6, 2, 1, 4, 3, 7);
                        bits7C[i] = packet.Translator.ReadBits(21);
                    }

                    bits34[i] = packet.Translator.ReadBits(24);

                    guid5[i] = new byte[bits34[i]][];
                    for (var j = 0; j < bits34[i]; ++j)
                    {
                        guid5[i][j] = new byte[8];
                        packet.Translator.StartBitStream(guid5[i][j], 7, 1, 2, 5, 0, 3, 6, 4);
                    }

                    bits54[i] = packet.Translator.ReadBits(22);
                    bits4[i] = packet.Translator.ReadBits(20);

                    guid6[i] = new byte[bits4[i]][];
                    for (var j = 0; j < bits4[i]; ++j)
                    {
                        guid6[i][j] = new byte[8];
                        packet.Translator.StartBitStream(guid6[i][j], 6, 4, 1, 0, 5, 7, 3, 2);
                    }
                }

                packet.Translator.StartBitStream(guid1, 2, 0);

                for (var i = 0; i < bits10; ++i)
                {
                    for (var j = 0; j < bits44[i]; ++j)
                        packet.Translator.ReadInt32("Int44", i, j);

                    for (var j = 0; j < bits4[i]; ++j)
                    {
                        packet.Translator.ReadXORByte(guid6[i][j], 3);
                        packet.Translator.ReadXORByte(guid6[i][j], 7);
                        packet.Translator.ReadInt32("IntEB", i, j);
                        packet.Translator.ReadXORByte(guid6[i][j], 5);
                        packet.Translator.ReadXORByte(guid6[i][j], 0);
                        packet.Translator.ReadInt32("IntEB", i, j);
                        packet.Translator.ReadXORByte(guid6[i][j], 2);
                        packet.Translator.ReadSingle("FloatEB", i, j);
                        packet.Translator.ReadXORByte(guid6[i][j], 1);
                        packet.Translator.ReadXORByte(guid6[i][j], 6);
                        packet.Translator.ReadXORByte(guid6[i][j], 4);
                        packet.Translator.WriteGuid("Guid6", guid6[i][j], i, j);
                    }

                    for (var j = 0; j < bits14[i]; ++j)
                    {
                        packet.Translator.ReadInt32("IntEB");
                        packet.Translator.ParseBitStream(guid3[i][j], 7, 2, 1, 6, 4, 0, 3, 5);
                        packet.Translator.WriteGuid("Guid3", guid3[i][j], i, j);
                    }

                    for (var j = 0; j < bits24[i]; ++j)
                    {
                        packet.Translator.ReadXORByte(guid4[i][j], 7);
                        packet.Translator.ReadInt32("IntEB", i, j);
                        packet.Translator.ReadXORByte(guid4[i][j], 5);
                        packet.Translator.ReadInt32("IntEB", i, j);
                        packet.Translator.ReadXORByte(guid4[i][j], 6);
                        packet.Translator.ReadXORByte(guid4[i][j], 1);
                        packet.Translator.ReadXORByte(guid4[i][j], 2);
                        packet.Translator.ReadXORByte(guid4[i][j], 0);
                        packet.Translator.ReadXORByte(guid4[i][j], 4);
                        packet.Translator.ReadXORByte(guid4[i][j], 3);
                        packet.Translator.WriteGuid("Guid4", guid4[i][j], i, j);
                    }

                    if (bit90[i])
                    {
                        packet.Translator.ReadInt32("IntEB", i);
                        packet.Translator.ReadXORByte(guid2[i], 3);

                        for (var j = 0; j < bits7C[i]; ++j)
                        {
                            packet.Translator.ReadInt32("IntEB", i, j);
                            packet.Translator.ReadInt32("IntEB", i, j);
                        }

                        packet.Translator.ReadXORByte(guid2[i], 7);
                        packet.Translator.ReadXORByte(guid2[i], 2);
                        packet.Translator.ReadXORByte(guid2[i], 4);
                        packet.Translator.ReadXORByte(guid2[i], 6);
                        packet.Translator.ReadInt32("IntEB");
                        packet.Translator.ReadXORByte(guid2[i], 5);
                        packet.Translator.ReadXORByte(guid2[i], 1);
                        packet.Translator.ReadInt32("IntEB");
                        packet.Translator.ReadXORByte(guid2[i], 0);
                        packet.Translator.WriteGuid("Guid2", guid2[i]);
                    }

                    for (var j = 0; j < bits34[i]; ++j)
                    {
                        packet.Translator.ParseBitStream(guid5[i][j], 1, 0, 2, 3, 7, 4, 5, 6);
                        packet.Translator.WriteGuid("Guid5", guid5[i][j], i, j);
                    }

                    packet.Translator.ReadInt32("Int14", i);

                    for (var j = 0; j < bits54[i]; ++j)
                        packet.Translator.ReadInt32("IntEB", i, j);
                }

                packet.Translator.ReadXORByte(guid1, 1);
                packet.Translator.ReadXORByte(guid1, 6);

                packet.Translator.ReadUInt32<SpellId>("Spell ID");

                packet.Translator.ReadXORByte(guid1, 4);
                packet.Translator.ReadXORByte(guid1, 5);
                packet.Translator.ReadXORByte(guid1, 2);
                packet.Translator.ReadXORByte(guid1, 3);
                packet.Translator.ReadXORByte(guid1, 0);
                packet.Translator.ReadXORByte(guid1, 7);

                packet.Translator.WriteGuid("Guid1", guid1);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_6327)]
        public static void HandleUnknown6327(Packet packet)
        {
            var guid1 = new byte[8];

            var bits1C =  packet.Translator.ReadBits(24);

            var guid2 = new byte[bits1C][];
            for (var i = 0; i < bits1C; ++i)
            {
                guid2[i] = new byte[8];
                packet.Translator.StartBitStream(guid2[i], 2, 3, 4, 6, 7, 5, 1, 0);
            }

            packet.Translator.StartBitStream(guid1, 6, 5, 4, 0, 3, 7, 1, 2);

            for (var i = 0; i < bits1C; ++i)
            {
                packet.Translator.ParseBitStream(guid2[i], 5, 0, 4, 1, 3, 2, 7, 6);
                packet.Translator.WriteGuid("Guid2", guid2[i]);
            }

            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 4);

            packet.Translator.WriteGuid("Guid1", guid1);
        }

        [Parser(Opcode.SMSG_UNKNOWN_8)]
        public static void HandleUnknown8(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[0] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid2, 4, 6);
            packet.Translator.ReadBit("bit34");
            packet.Translator.StartBitStream(guid2, 1, 5);
            packet.Translator.ReadBit("bit48");
            packet.Translator.StartBitStream(guid1, 3, 4, 5);
            packet.Translator.StartBitStream(guid2, 3, 7);
            packet.Translator.StartBitStream(guid1, 7, 2, 0, 1);
            packet.Translator.ReadBit("bit24");
            packet.Translator.ReadBit("bit40");
            guid2[2] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadInt32("Int38");
            packet.Translator.ReadInt32("Int28");
            packet.Translator.ReadInt32("Int30");
            packet.Translator.ReadInt32("Int4C");
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadInt32("Int2C");
            packet.Translator.ReadInt32("Int3C");
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadInt32("Int20");
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadByte("Byte35");
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadInt32("Int58");
            packet.Translator.ReadInt32("Int44");
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadInt32("Item ID");

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_305)]
        public static void HandleUnknown305(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 2, 3, 0, 5, 6, 4, 7, 1);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadInt32("Int20");
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadInt32("Int1C");
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 2);

            packet.Translator.WriteGuid("Guid", guid);

        }

        [Parser(Opcode.SMSG_UNKNOWN_1321)]
        public static void HandleUnknown1321(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.Translator.StartBitStream(guid1, 1, 3, 7, 0);
            packet.Translator.StartBitStream(guid2, 4, 7);
            guid1[2] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("bit18");
            packet.Translator.StartBitStream(guid2, 2, 6);
            packet.Translator.StartBitStream(guid1, 4, 5);
            packet.Translator.StartBitStream(guid2, 1, 0, 5, 3);
            guid1[6] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid2, 6);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_275)]
        public static void HandleUnknown275(Packet packet)
        {
            packet.Translator.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_1036)]
        public static void HandleUnknown1036(Packet packet)
        {
            packet.Translator.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_429)]
        public static void HandleUnknown429(Packet packet)
        {
            packet.Translator.ReadByte("Byte24");
            packet.Translator.ReadByte("Byte2C");
            packet.Translator.ReadInt32("Int10");

            var bit28 = !packet.Translator.ReadBit();
            var bits14 = (int)packet.Translator.ReadBits(24);

            if (bit28)
                packet.Translator.ReadInt32("Int28");

            for (var i = 0; i < bits14; ++i)
                packet.Translator.ReadByte("Byte18", i);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1135)]
        public static void HandleUnknown1135(Packet packet)
        {
            var bits10 = packet.Translator.ReadBits(15);

            for (var i = 0; i < bits10; ++i)
            {
                packet.Translator.ReadInt32("Int14", i);

                for (var j = 0; j < 300; ++j)
                    packet.Translator.ReadByte("ByteEB", i, j);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_2321)]
        public static void HandleUnknown2321(Packet packet)
        {
            packet.Translator.ReadInt32("Time?");
        }

        [Parser(Opcode.SMSG_UNKNOWN_1139)]
        public static void HandleUnknown1139(Packet packet)
        {
            packet.Translator.ReadInt64("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_2057)]
        public static void HandleUnknown2057(Packet packet)
        {
            packet.Translator.ReadInt32("Int10");
        }

        [Parser(Opcode.MSG_MULTIPLE_PACKETS)]
        public static void MultiplePackets(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.Formatter.AppendItem("ClientToServer: CMSG_ADDON_REGISTERED_PREFIXES");
                var count = packet.Translator.ReadBits("Count", 24);
                var lengths = new int[count];
                for (var i = 0; i < count; ++i)
                    lengths[i] = (int)packet.Translator.ReadBits(5);

                for (var i = 0; i < count; ++i)
                    packet.Translator.ReadWoWString("Addon", lengths[i], i);
            }
            else
            {
                packet.Formatter.AppendItem("ServerToClient: SMSG_QUEST_GIVER_QUEST_COMPLETE");

                packet.Translator.ReadInt32("Reward XP");
                packet.Translator.ReadInt32<QuestId>("Quest ID");
                packet.Translator.ReadInt32("Money");
                packet.Translator.ReadInt32("Int1C");
                packet.Translator.ReadInt32("Int24");
                packet.Translator.ReadInt32("Int20");
                packet.Translator.ReadBit("bit28");
                packet.Translator.ReadBit("bit18");
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_4522)]
        public static void HandleUnknown4522(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 6, 5, 0, 1, 2, 4, 3, 7);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 6);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2205)] // Battle Pet?
        public static void HandleUnknown2205(Packet packet)
        {
            var count = 5;

            var bits25 = packet.Translator.ReadBit();

            var bits26 = 0u;
            var bitsA7 = new int[count];
            if (bits25) // has custom Name?
            {
                bits26 = packet.Translator.ReadBits(8);

                for (var i = 0; i < count; ++i)
                    bitsA7[i] = (int)packet.Translator.ReadBits(7);

                packet.Translator.ReadBit();
            }

            if (bits25) // has custom Name?
            {
                for (var i = 0; i < count; ++i)
                    packet.Translator.ReadWoWString("StringA7", bitsA7[i]);

                packet.Translator.ReadWoWString("Name", bits26);
            }

            packet.Translator.ReadInt64("Int18");
            packet.Translator.ReadInt32("Entry");
            packet.Translator.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_2080)]
        public static void HandleUnknown2080(Packet packet)
        {
            var bits18 = 0;
            var bits7FA = 0;

            var bitC00 = packet.Translator.ReadBit();
            if (bitC00)
            {
                bits7FA = (int)packet.Translator.ReadBits(10);
                bits18 = (int)packet.Translator.ReadBits(11);
            }

            if (bitC00)
            {
                packet.Translator.ReadInt32("Int7F0");
                packet.Translator.ReadWoWString("String18", bits18);
                packet.Translator.ReadByte("Byte7E9");
                packet.Translator.ReadWoWString("String7FA", bits7FA);
                packet.Translator.ReadByte("Byte7F9");
                packet.Translator.ReadInt32("Int7F4");
                packet.Translator.ReadInt32("IntBFC");
                packet.Translator.ReadInt32("Int7EC");
                packet.Translator.ReadByte("Byte7F8");
                packet.Translator.ReadInt32("Int14");
            }

            packet.Translator.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_438)]
        public static void HandleUnknown438(Packet packet)
        {
            packet.Translator.ReadSingle("Float10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_2087)]
        public static void HandleUnknown2087(Packet packet)
        {
            for (var i = 0; i < 4; ++i)
            {
                packet.Translator.ReadInt32("Unk 1", i);
                packet.Translator.ReadInt32("Unk 2", i);
                packet.Translator.ReadInt32("Unk 3", i);
                packet.Translator.ReadInt32("Unk 4", i);
                packet.Translator.ReadInt32("Unk 5", i);
                packet.Translator.ReadInt32("Unk 6", i);
                packet.Translator.ReadInt32("Unk 7", i);
                packet.Translator.ReadInt32("Unk 8", i);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_169)]
        public static void HandleUnknown169(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadInt32("Int1C");
            packet.Translator.ReadInt32("Int18");
            var len = packet.Translator.ReadInt32("Int1");
            packet.Translator.ReadBytes(len);
            packet.Translator.StartBitStream(guid, 4, 2, 0);
            packet.Translator.ReadBits("bits20", 3);
            packet.Translator.StartBitStream(guid, 4, 5, 1, 3, 6);
            packet.Translator.ParseBitStream(guid, 4, 2, 7, 5, 3, 1, 6, 0);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_3139)] // Send Name and RealmId
        public static void HandleUnknown3139(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadBit("bit48");
            var bits11 = packet.Translator.ReadBits(6);
            packet.Translator.ReadBit("bit10");

            packet.Translator.StartBitStream(guid, 0, 2, 6, 7, 3, 4, 5, 1);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadWoWString("Name", bits11);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadInt32("Realm Id");
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 3);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4374)]
        public static void HandleUnknown4374(Packet packet)
        {
            for (var i = 0; i < packet.Translator.ReadBits("Count", 22); ++i)
                packet.Translator.ReadInt32("IntED", i);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1317)]
        public static void HandleUnknown1317(Packet packet)
        {
            packet.Translator.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_1060)]
        public static void HandleUnknown1060(Packet packet)
        {
            for (var i = 0; i < packet.Translator.ReadBits("Count", 21); ++i)
            {
                packet.Translator.ReadInt32("Unk 1");
                packet.Translator.ReadInt32("Unk 1");
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_2338)]
        public static void HandleUnknown2338(Packet packet) // Item opcode?
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var guid3 = new byte[8];
            var guid4 = new byte[8];

            var bit38 = packet.Translator.ReadBit();

            guid4[6] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid4[5] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();

            var bits24 = 0u;
            if (bit38)
            {
                packet.Translator.StartBitStream(guid1, 1, 4, 5, 2, 6);
                bits24 = packet.Translator.ReadBits(21);
                packet.Translator.StartBitStream(guid1, 3, 0, 7);
            }

            guid4[4] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid3, 1, 6, 3, 7);
            packet.Translator.StartBitStream(guid2, 7, 1);
            guid3[4] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            guid4[0] = packet.Translator.ReadBit();
            guid3[0] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid4, 3, 1);
            guid2[3] = packet.Translator.ReadBit();
            guid3[5] = packet.Translator.ReadBit();
            guid4[7] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid3[2] = packet.Translator.ReadBit();
            guid4[2] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();

            if (bit38)
            {
                packet.Translator.ReadXORByte(guid1, 3);
                packet.Translator.ReadXORByte(guid1, 2);
                packet.Translator.ReadXORByte(guid1, 6);
                packet.Translator.ReadInt32("Int18");

                for (var i = 0; i < bits24; ++i)
                {
                    packet.Translator.ReadInt32("IntED", i);
                    packet.Translator.ReadInt32("IntED", i);
                }

                packet.Translator.ReadXORByte(guid1, 0);
                packet.Translator.ReadXORByte(guid1, 4);
                packet.Translator.ReadXORByte(guid1, 7);
                packet.Translator.ReadXORByte(guid1, 1);
                packet.Translator.ReadInt32("Int20");
                packet.Translator.ReadXORByte(guid1, 5);
                packet.Translator.ReadInt32("Int1C");
                packet.Translator.WriteGuid("Guid1", guid1);
            }

            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid3, 5);
            packet.Translator.ReadXORByte(guid3, 4);
            packet.Translator.ReadInt32("Int68");
            packet.Translator.ReadXORByte(guid3, 2);
            packet.Translator.ReadXORByte(guid4, 2);
            packet.Translator.ReadXORByte(guid3, 1);
            packet.Translator.ReadXORByte(guid4, 0);
            packet.Translator.ReadXORByte(guid3, 0);
            packet.Translator.ReadXORByte(guid3, 7);
            packet.Translator.ReadXORByte(guid4, 6);
            packet.Translator.ReadInt32("Item Entry");
            packet.Translator.ReadXORByte(guid3, 3);
            packet.Translator.ReadXORByte(guid4, 7);
            packet.Translator.ReadXORByte(guid4, 1);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid4, 4);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid4, 5);
            packet.Translator.ReadXORByte(guid4, 3);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadInt32("Int48");
            packet.Translator.ReadXORByte(guid3, 6);
            packet.Translator.ReadXORByte(guid2, 3);

            packet.Translator.WriteGuid("Guid2", guid2);
            packet.Translator.WriteGuid("Item GUID", guid3);
            packet.Translator.WriteGuid("Guid4", guid4);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5553)]
        public static void HandleUnknown5553(Packet packet) // Item opcode?
        {
            var guid = new byte[8];
            var powerGUID = new byte[8];

            packet.Translator.StartBitStream(guid, 6, 1, 5, 0, 3, 7, 2);
            var hasPowerData = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();

            if (hasPowerData)
            {
                packet.Translator.StartBitStream(powerGUID, 6, 5, 2, 1, 0, 7);
                var powerCount = packet.Translator.ReadBits(21);
                packet.Translator.StartBitStream(powerGUID, 4, 3);

                packet.Translator.ReadXORByte(powerGUID, 1);
                packet.Translator.ReadInt32("Attack power");
                packet.Translator.ReadXORByte(powerGUID, 2);
                packet.Translator.ReadInt32("Spell power");
                packet.Translator.ReadXORByte(powerGUID, 5);

                for (var i = 0; i < powerCount; ++i)
                {
                    packet.Translator.ReadInt32E<PowerType>("Power type", i); // Actually powertype for class
                    packet.Translator.ReadInt32("Value", i);
                }

                packet.Translator.ReadXORByte(powerGUID, 7);
                packet.Translator.ReadXORByte(powerGUID, 6);
                packet.Translator.ReadXORByte(powerGUID, 3);
                packet.Translator.ReadXORByte(powerGUID, 4);
                packet.Translator.ReadXORByte(powerGUID, 0);
                packet.Translator.ReadInt32("Current health");
                packet.Translator.WriteGuid("Power GUID", powerGUID);
            }

            packet.Translator.ReadInt32("Int1C");
            packet.Translator.ReadInt32("Int24");
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadByte("Byte20");
            packet.Translator.ReadXORByte(guid, 0);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1444)]
        public static void HandleUnknown1444(Packet packet) // Item opcode?
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 6, 5, 2, 4, 0, 7, 3);
            packet.Translator.ReadBit("bit10");
            guid[1] = packet.Translator.ReadBit();
            packet.Translator.ParseBitStream(guid, 5, 2, 6, 3, 1, 0, 4, 7);
            packet.Translator.ReadUInt32<SpellId>("Spell ID");

            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1464)]
        public static void HandleUnknown1464(Packet packet) // Item opcode?
        {
            var powerGUID = new byte[8];
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[2] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid2, 7, 0, 5);
            packet.Translator.StartBitStream(guid1, 7, 4);
            packet.Translator.StartBitStream(guid2, 3, 1, 4);
            guid1[0] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            var hasPowerData = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 1, 5, 2);

            var powerCount = 0u;
            if (hasPowerData)
            {
                packet.Translator.StartBitStream(powerGUID, 6, 0, 4, 3, 7, 2, 1, 5);
                powerCount = packet.Translator.ReadBits(21);
            }

            guid1[3] = packet.Translator.ReadBit();

            if (hasPowerData)
            {
                packet.Translator.ReadXORByte(powerGUID, 3);
                packet.Translator.ReadInt32("Current health");
                packet.Translator.ReadXORByte(powerGUID, 6);
                packet.Translator.ReadXORByte(powerGUID, 7);
                packet.Translator.ReadXORByte(powerGUID, 1);
                packet.Translator.ReadXORByte(powerGUID, 5);
                packet.Translator.ReadInt32("Attack power");
                packet.Translator.ReadXORByte(powerGUID, 4);
                packet.Translator.ReadXORByte(powerGUID, 0);

                for (var i = 0; i < powerCount; ++i)
                {
                    packet.Translator.ReadInt32E<PowerType>("Power type", i); // Actually powertype for class
                    packet.Translator.ReadInt32("Value", i);
                }

                packet.Translator.ReadInt32("Spell power");
                packet.Translator.ReadXORByte(powerGUID, 2);

                packet.Translator.WriteGuid("Power GUID", powerGUID);
            }

            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid1, 1);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4518)]
        public static void HandleUnknown4518(Packet packet) // Item opcode?
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[2] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            var bit28 = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 4, 0);
            guid2[3] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 2, 3);
            packet.Translator.StartBitStream(guid2, 5, 7);
            guid1[6] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid2, 0, 6);
            guid1[5] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 6);
            if (bit28)
            {
                packet.Translator.ReadInt32("Int30");
                packet.Translator.ReadInt32("Int2C");
            }

            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadInt32("Int24");
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadInt32("Int20");
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 6);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1454)]
        public static void HandleUnknown1454(Packet packet)
        {
            packet.Translator.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_5525)]
        public static void HandleUnknown5525(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 4, 6, 3, 7, 2, 5, 0, 1);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadXORByte(guid, 3);

            packet.Translator.WriteGuid("Guid2", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2235)]
        public static void HandleUnknown2235(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadInt16("Int18");

            packet.Translator.StartBitStream(guid, 7, 2, 0, 1, 6, 3, 4, 5);
            packet.Translator.ParseBitStream(guid, 7, 0, 4, 5, 6, 2, 3, 1);

            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1182)] // GameObject opcode
        public static void HandleUnknown1182(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 4, 6, 1, 3, 2, 5, 0, 7);
            packet.Translator.ParseBitStream(guid, 5, 4, 2, 3, 7, 0, 1, 6);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_147)]
        public static void HandleUnknown147(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 1, 6, 2, 3, 5, 7, 4, 0);
            packet.Translator.ParseBitStream(guid, 2, 4, 6, 1, 5, 3, 0, 7);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4822)]
        public static void HandleUnknown4822(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 6, 5, 0, 1, 2, 4, 3, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadSingle("Float1C");
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadInt32("Int18");

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2327)]
        public static void HandleUnknown2327(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 3, 2, 7, 0, 4, 6, 1, 5);
            packet.Translator.ParseBitStream(guid, 0, 5, 2, 6, 7, 4, 1, 3);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_62)]
        public static void HandleUnknown62(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 6, 4, 3, 5, 1, 7, 0, 2);
            packet.Translator.ParseBitStream(guid, 1, 7, 5, 4, 6, 2, 0, 3);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2178)]
        public static void HandleUnknown2178(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 0, 7, 5, 2);
            packet.Translator.ReadBit("bit10");
            packet.Translator.StartBitStream(guid, 3, 6, 4, 1);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadInt32("Int20");  // SpellId?
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 1);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_6454)]
        public static void HandleUnknown6454(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.Translator.ReadByte("Byte10");
            packet.Translator.ReadByte("Byte38");
            packet.Translator.ReadByte("Byte26");
            packet.Translator.ReadByte("Byte41");
            packet.Translator.ReadByte("Byte40");
            packet.Translator.ReadByte("Byte25");
            packet.Translator.ReadInt32("Int3C");
            packet.Translator.ReadByte("Byte24");
            packet.Translator.ReadByte("Byte42");

            guid2[7] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 3, 1, 5, 7);
            guid2[6] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            var bits14 = packet.Translator.ReadBits(22);
            packet.Translator.StartBitStream(guid2, 5, 1, 4);
            packet.Translator.StartBitStream(guid1, 2, 0);
            packet.Translator.StartBitStream(guid2, 0, 2);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 6);

            for (var i = 0; i < bits14; ++i)
                packet.Translator.ReadInt32("IntED", i);

            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid1, 7);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4763)]
        public static void HandleUnknown4763(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 6, 1, 5, 4, 7, 2, 3, 0);
            packet.Translator.ParseBitStream(guid, 0, 5, 3, 1, 7, 2, 6, 4);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1162)]
        public static void HandleUnknown1162(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 6, 3, 0, 1, 5, 7, 4, 2);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 0);

            packet.Translator.WriteGuid("Guid2", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_389)]
        public static void HandleUnknown389(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 5, 1, 4, 2, 3, 0, 6, 7);
            packet.Translator.ParseBitStream(guid, 5, 2, 0, 6, 3, 1, 4, 7);
            packet.Translator.ReadUInt32<SpellId>("Spell ID");

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1189)]
        public static void HandleUnknown1189(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 1, 4, 3, 6, 0, 5, 7, 2);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadXORByte(guid, 2);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4710)]
        public static void HandleUnknown4710(Packet packet)
        {
            var guid = new byte[8];

            var bits24 = (int)packet.Translator.ReadBits(2);
            packet.Translator.StartBitStream(guid, 5, 1, 7, 0);
            var bit28 = !packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid, 2, 4, 3, 6);
            packet.Translator.ReadXORByte(guid, 7);

            if (bit28)
                packet.Translator.ReadInt32("Int28");

            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadSingle("Float1C");
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadSingle("Float20");
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 4);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1059)]
        public static void HandleUnknown1059(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 2, 7, 6, 0, 5, 3, 4, 1);
            packet.Translator.ParseBitStream(guid, 3, 7, 1, 0, 4, 2, 6, 5);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_163)]
        public static void HandleUnknown163(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.Translator.StartBitStream(guid2, 3, 2, 1, 0);
            guid1[1] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 5, 2, 0);
            guid2[4] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 6, 3);
            guid2[5] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadInt32("Int20");
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 3);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1347)]
        public static void HandleUnknown1347(Packet packet)
        {
            packet.Translator.ReadInt64("Time?");
        }

        [Parser(Opcode.SMSG_UNKNOWN_1197)] // Energize opcode
        public static void HandleUnknown1197(Packet packet)
        {
            var powerGUID = new byte[8];

            packet.Translator.StartBitStream(powerGUID, 7, 2, 4, 3, 6, 1);
            var powerCount = (int)packet.Translator.ReadBits(21);
            packet.Translator.StartBitStream(powerGUID, 0, 5);

            for (var i = 0; i < powerCount; ++i)
            {
                packet.Translator.ReadInt32("Value", i);
                packet.Translator.ReadByteE<PowerType>("Power type", i);
            }

            packet.Translator.ParseBitStream(powerGUID, 4, 2, 3, 7, 0, 6, 1, 5);

            packet.Translator.WriteGuid("Power GUID", powerGUID);
        }

        [Parser(Opcode.SMSG_UNKNOWN_6285)]
        public static void HandleUnknown6285(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 21);

            var guid = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                guid[i] = new byte[8];
                packet.Translator.StartBitStream(guid[i], 6, 3, 4, 2, 5, 1, 7, 0);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadXORByte(guid[i], 7);
                packet.Translator.ReadXORByte(guid[i], 1);
                packet.Translator.ReadXORByte(guid[i], 2);
                packet.Translator.ReadInt32("Int14"); // flags?
                packet.Translator.ReadXORByte(guid[i], 3);
                packet.Translator.ReadXORByte(guid[i], 5);
                packet.Translator.ReadXORByte(guid[i], 4);
                packet.Translator.ReadXORByte(guid[i], 0);
                packet.Translator.ReadXORByte(guid[i], 6);

                packet.Translator.WriteGuid("Guid", guid[i]);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_1358)]
        public static void HandleUnknown1358(Packet packet)
        {
            var bits10 = packet.Translator.ReadBits(19);

            var bits38 = new uint[bits10];
            var guid1 = new byte[bits10][];
            var guid2 = new byte[bits10][][];

            for (var i = 0; i < bits10; ++i)
            {
                bits38[i] = packet.Translator.ReadBits(24);

                guid1[i] = new byte[8];
                guid2[i] = new byte[bits38[i]][];

                for (var j = 0; j < bits38[i]; ++j)
                {
                    guid2[i][j] = new byte[8];
                    packet.Translator.StartBitStream(guid2[i][j], 5, 2, 6, 0, 1, 4, 3, 7);
                }

                packet.Translator.StartBitStream(guid1[i], 2, 0, 4, 1, 7, 3, 5, 6);
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.Translator.ReadXORByte(guid1[i], 2);

                for (var j = 0; j < bits38[i]; ++j)
                {
                    packet.Translator.ParseBitStream(guid2[i][j], 4, 0, 1, 5, 3, 7, 2, 6);
                    packet.Translator.WriteGuid("Guid2", guid2[i][j], i, j);
                }

                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadPackedTime("Date");
                packet.Translator.ReadXORByte(guid1[i], 1);
                packet.Translator.ReadXORByte(guid1[i], 4);
                packet.Translator.ReadXORByte(guid1[i], 6);
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadXORByte(guid1[i], 3);
                packet.Translator.ReadXORByte(guid1[i], 7);
                packet.Translator.ReadInt32("Item Id", i);
                packet.Translator.ReadXORByte(guid1[i], 5);
                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadXORByte(guid1[i], 0);

                packet.Translator.WriteGuid("Guid1", guid1[i], i);
            }
        }

        [Parser(Opcode.SMSG_VIGNETTE_UPDATE)]
        public static void HandleUnknown177(Packet packet)
        {

            var bits10 = packet.Translator.ReadBits(24);

            var guid1 = new byte[bits10][];

            for (var i = 0; i < bits10; ++i)
            {
                guid1[i] = new byte[8];
                packet.Translator.StartBitStream(guid1[i], 4, 1, 0, 5, 7, 2, 3, 6);
            }

            var bits34 = packet.Translator.ReadBits(20);

            var guid2 = new byte[bits34][];

            for (var i = 0; i < bits34; ++i)
            {
                guid2[i] = new byte[8];
                packet.Translator.StartBitStream(guid2[i], 7, 4, 1, 5, 6, 2, 3, 0);
            }

            var bits44 = packet.Translator.ReadBits(24);

            var guid3 = new byte[bits44][];

            for (var i = 0; i < bits44; ++i)
            {
                guid3[i] = new byte[8];
                packet.Translator.StartBitStream(guid3[i], 3, 4, 6, 7, 1, 0, 2, 5);
            }

            var bits24 = packet.Translator.ReadBits(24);

            var guid4 = new byte[bits24][];

            for (var i = 0; i < bits24; ++i)
            {
                guid4[i] = new byte[8];
                packet.Translator.StartBitStream(guid4[i], 0, 1, 6, 4, 5, 3, 2, 7);
            }

            var bit20 = packet.Translator.ReadBit();
            var bits54 = packet.Translator.ReadBits(20);

            var guid5 = new byte[bits54][];

            for (var i = 0; i < bits54; ++i)
            {
                guid5[i] = new byte[8];
                packet.Translator.StartBitStream(guid5[i], 3, 2, 7, 5, 6, 0, 1, 4);
            }

            for (var i = 0; i < bits54; ++i)
            {
                packet.Translator.ReadXORByte(guid5[i], 1);
                packet.Translator.ReadXORByte(guid5[i], 2);
                packet.Translator.ReadXORByte(guid5[i], 3);
                packet.Translator.ReadXORByte(guid5[i], 4);
                packet.Translator.ReadXORByte(guid5[i], 0);
                packet.Translator.ReadXORByte(guid5[i], 5);
                packet.Translator.ReadInt32("Vignette Id");
                packet.Translator.ReadXORByte(guid5[i], 7);
                packet.Translator.ReadSingle("Z");
                packet.Translator.ReadSingle("Y");
                packet.Translator.ReadXORByte(guid5[i], 6);
                packet.Translator.ReadSingle("X");

                packet.Translator.WriteGuid("Guid5", guid5[i]);
            }

            for (var i = 0; i < bits34; ++i)
            {
                packet.Translator.ReadSingle("Y");
                packet.Translator.ReadInt32("Vignette Id");
                packet.Translator.ReadXORByte(guid2[i], 0);
                packet.Translator.ReadSingle("Z");
                packet.Translator.ReadXORByte(guid2[i], 3);
                packet.Translator.ReadSingle("X");
                packet.Translator.ReadXORByte(guid2[i], 4);
                packet.Translator.ReadXORByte(guid2[i], 7);
                packet.Translator.ReadXORByte(guid2[i], 6);
                packet.Translator.ReadXORByte(guid2[i], 2);
                packet.Translator.ReadXORByte(guid2[i], 1);
                packet.Translator.ReadXORByte(guid2[i], 5);

                packet.Translator.WriteGuid("Guid2", guid2[i]);
            }

            for (var i = 0; i < bits24; ++i)
            {
                packet.Translator.ParseBitStream(guid4[i], 5, 1, 3, 4, 2, 7, 0, 6);
                packet.Translator.WriteGuid("Guid4", guid4[i]);
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.Translator.ParseBitStream(guid1[i], 0, 1, 4, 7, 5, 6, 2, 3);
                packet.Translator.WriteGuid("Guid1", guid1[i]);
            }

            for (var i = 0; i < bits44; ++i)
            {
                packet.Translator.ParseBitStream(guid3[i], 2, 4, 3, 0, 6, 7, 1, 5);
                packet.Translator.WriteGuid("Guid1", guid3[i]);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_1318)]
        public static void HandleUnknown1318(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid1[7] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("bit4C");
            packet.Translator.StartBitStream(guid2, 2, 5);
            packet.Translator.StartBitStream(guid1, 6, 3);
            var bit41 = !packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 5, 2);
            packet.Translator.ReadBits("bits48", 3);
            guid2[4] = packet.Translator.ReadBit();
            packet.Translator.ReadBits("bits44", 2);
            guid2[3] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("bit28");
            guid2[7] = packet.Translator.ReadBit();
            var bit40 = !packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            packet.Translator.ReadInt32("Item Display Id");
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadInt32("Int38");
            packet.Translator.ReadInt32("Item Id");
            packet.Translator.ReadInt32("Int1C");
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadByte("Byte18");
            packet.Translator.ReadInt32("Int3C");
            if (bit41)
                packet.Translator.ReadByte("Byte41");
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 1);
            var len = packet.Translator.ReadInt32("Int0");
            packet.Translator.ReadBytes(len);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadInt32("Int34");
            if (bit40)
                packet.Translator.ReadByte("Byte40");

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5727)]
        public static void HandleUnknown5727(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 7, 1, 5, 6, 0, 4, 2, 3);
            packet.Translator.ParseBitStream(guid, 0, 5, 3, 4, 7, 1, 6, 2);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1119)]
        public static void HandleUnknown1119(Packet packet)
        {
            var bits10 = packet.Translator.ReadBits(17);

            var guid = new byte[bits10][];
            var bits30 = new uint[bits10];
            var bit16E = new bool[bits10];
            var bits0 = new uint[bits10];
            var bitsA2 = new uint[bits10];
            var bit16F = new bool[bits10];

            for (var i = 0; i < bits10; ++i)
            {
                guid[i] = new byte[8];
                bits30[i] = packet.Translator.ReadBits(6);
                packet.Translator.StartBitStream(guid[i], 4, 0, 2, 3);
                bit16E[i] = packet.Translator.ReadBit();
                guid[i][7] = packet.Translator.ReadBit();
                bits0[i] = packet.Translator.ReadBits(8);
                guid[i][1] = packet.Translator.ReadBit();
                bitsA2[i] = packet.Translator.ReadBits(8);
                packet.Translator.StartBitStream(guid[i], 6, 5);
                bit16F[i] = packet.Translator.ReadBit();
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.Translator.ReadByte("Byte14", i);
                packet.Translator.ReadInt32("Realm Id", i);
                packet.Translator.ReadXORByte(guid[i], 7);
                packet.Translator.ReadInt32<AreaId>("Area Id");
                packet.Translator.ReadXORByte(guid[i], 0);
                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadSingle("Float14", i);

                for (var j = 0; j < 2; ++j) // skill?
                {
                    packet.Translator.ReadInt32("Int14", i, j);
                    packet.Translator.ReadInt32("Int14", i, j);
                    packet.Translator.ReadInt32("Int14", i, j);
                }

                packet.Translator.ReadXORByte(guid[i], 3);
                packet.Translator.ReadWoWString("Name", bits30[i], i);
                packet.Translator.ReadXORByte(guid[i], 1);
                packet.Translator.ReadWoWString("Note?", bitsA2[i], i);
                packet.Translator.ReadByteE<Class>("Class");
                packet.Translator.ReadXORByte(guid[i], 4);
                packet.Translator.ReadInt64("Int8", i);
                packet.Translator.ReadByte("Byte14", i);
                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadXORByte(guid[i], 2);
                packet.Translator.ReadXORByte(guid[i], 6);
                packet.Translator.ReadXORByte(guid[i], 5);
                packet.Translator.ReadWoWString("String14", bits0[i], i);
                packet.Translator.ReadInt64("IntED", i);
                packet.Translator.ReadInt32<ZoneId>("Zone Id");
                packet.Translator.ReadByte("Level", i);
                packet.Translator.WriteGuid("Guid", guid[i], i);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_140)]
        public static void HandleUnknown140(Packet packet)
        {
            packet.Translator.ReadInt32("Int14");
            packet.Translator.ReadInt32("Unix Time");
        }

        [Parser(Opcode.SMSG_UNKNOWN_141)]
        public static void HandleUnknown141(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadInt32("Int38");
            packet.Translator.ReadInt32("Unix Time");
            packet.Translator.ReadInt32("Int30");
            packet.Translator.ReadInt32("Int20");
            packet.Translator.ReadInt32("Int24");
            packet.Translator.ReadInt32("Int10");
            for (var i = 0; i < 3; ++i)
            {
                packet.Translator.ReadInt32("Unk 1", i);
                packet.Translator.ReadByte("Byte48", i);
            }
            packet.Translator.ReadInt32("Int34");
            packet.Translator.StartBitStream(guid, 7, 6, 5, 2, 3, 0, 4, 1);
            packet.Translator.ParseBitStream(guid, 4, 7, 2, 3, 1, 6, 5, 0);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2066)]
        public static void HandleUnknown2066(Packet packet)
        {
            packet.Translator.ReadInt32("Int14");
            packet.Translator.ReadInt32("Int10");
            packet.Translator.ReadBit("bit18");
        }

        [Parser(Opcode.SMSG_UNKNOWN_23)]
        public static void HandleUnknown23(Packet packet)
        {
            var guid1 = new byte[8];

            packet.Translator.StartBitStream(guid1, 4, 5, 0, 2);

            var bits2C = (int)packet.Translator.ReadBits(22);

            var guid2 = new byte[bits2C][];
            var bits8 = new uint[bits2C];

            for (var i = 0; i < bits2C; ++i)
            {
                guid2[i] = new byte[8];
                packet.Translator.StartBitStream(guid2[i], 4, 5, 6, 0);
                bits8[i] = packet.Translator.ReadBits(20);
                packet.Translator.StartBitStream(guid2[i], 7, 3, 2, 1);
            }

            packet.Translator.StartBitStream(guid1, 1, 3, 6, 7);

            for (var i = 0; i < bits2C; ++i)
            {

                for (var j = 0; j < bits8[i]; ++j)
                {
                    packet.Translator.ReadInt32("Int30", i);
                    packet.Translator.ReadInt32("Int0", i);
                    packet.Translator.ReadInt32("Int30", i);
                    packet.Translator.ReadInt32("Int30", i);
                }

                packet.Translator.ParseBitStream(guid2[i], 0, 1, 2, 5, 3, 6, 4, 7);

                packet.Translator.WriteGuid("Guid2", guid2[i], i);
            }

            packet.Translator.ReadByte("Byte3C");
            packet.Translator.ReadByte("Byte28");
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadInt32("Int1C");
            packet.Translator.ReadInt32("Int20");
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 6);

            packet.Translator.WriteGuid("Guid1", guid1);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2055)]
        public static void HandleUnknown2055(Packet packet)
        {
            byte[][] guid1;
            var guid2 = new byte[8];

            for (var i = 0; i < 3; ++i)
                packet.Translator.ReadByte("Byte15B", i);

            packet.Translator.ReadInt32("Int14C");
            packet.Translator.ReadInt32("Int128");
            packet.Translator.ReadByte("Byte12");
            packet.Translator.ReadByte("Byte11");
            packet.Translator.ReadInt32("Int148");
            packet.Translator.ReadInt32("Unix Time");

            guid2[5] = packet.Translator.ReadBit();
            var bits10 = packet.Translator.ReadBits(8);
            packet.Translator.ReadBit("bit15");
            packet.Translator.ReadBit("bit15A");
            packet.Translator.ReadBit("bit158");
            guid2[3] = packet.Translator.ReadBit();
            var bits14 = packet.Translator.ReadBits(24);
            guid2[4] = packet.Translator.ReadBit();

            guid1 = new byte[bits14][];

            for (var i = 0; i < bits14; ++i)
            {
                guid1[i] = new byte[8];
                packet.Translator.StartBitStream(guid1[i], 0, 5, 4, 1, 6, 3, 7, 2);
            }

            guid2[7] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("bit24");
            var bits12C = packet.Translator.ReadBits(22);
            packet.Translator.ReadBit("bit159");

            for (var i = 0; i < bits14; ++i)
            {
                packet.Translator.ParseBitStream(guid1[i], 3, 0, 7, 4, 2, 6, 1, 5);
                packet.Translator.WriteGuid("Guid1", guid1[i]);
            }

            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 3);

            for (var i = 0; i < bits12C; ++i)
                packet.Translator.ReadInt32("IntEA", i);

            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 2);
            if (bits10 > 0)
                packet.Translator.ReadWoWString("String25", bits10);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 4);

            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2202)]
        public static void HandleUnknown2202(Packet packet) // Ticket stuff? browser? mhm
        {
            packet.Translator.ReadInt32("Unix Time");
            packet.Translator.ReadInt32("Int10");

            var bits18 = packet.Translator.ReadBits(20);

            var guid = new byte[bits18][];
            var bit410 = new bool[bits18];
            var bitsC = new uint[bits18];
            var bit820 = new bool[bits18];
            var bits420 = new uint[bits18];

            for (var i = 0; i < bits18; ++i)
            {
                guid[i] = new byte[8];
                bit410[i] = !packet.Translator.ReadBit();
                bitsC[i] = packet.Translator.ReadBits(11);
                packet.Translator.ReadBit(); // fake bit
                packet.Translator.StartBitStream(guid[i], 7, 6, 1, 2, 5, 3, 0, 4);
                bit820[i] = !packet.Translator.ReadBit();
                bits420[i] = packet.Translator.ReadBits(10);
            }

            for (var i = 0; i < bits18; ++i)
            {
                packet.Translator.ParseBitStream(guid[i], 6, 0, 7, 3, 5, 1, 4, 2);

                if (bit410[i])
                    packet.Translator.ReadInt32("Int1C", i);

                packet.Translator.ReadWoWString("Status Text", bits420[i], i);

                if (bit820[i])
                    packet.Translator.ReadInt32("Int1C", i);

                packet.Translator.ReadInt32("Ticket Id", i);
                packet.Translator.ReadInt32("Int1C", i);
                packet.Translator.ReadInt32("Int1C", i);
                packet.Translator.ReadWoWString("URL", bitsC[i], i);

                packet.Translator.WriteGuid("GUID", guid[i], i);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_2305)] // Guild opcode?
        public static void HandleUnknown2305(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.Translator.StartBitStream(guid2, 7, 3);
            var bit28 = packet.Translator.ReadBit();
            if (bit28)
                packet.Translator.StartBitStream(guid1, 6, 7, 4, 5, 2, 3, 1, 0);

            var bits44 = packet.Translator.ReadBits(23);
            var bits60 = packet.Translator.ReadBits(23);
            packet.Translator.StartBitStream(guid2, 5, 2, 6);
            var bits34 = packet.Translator.ReadBits(20);

            var guid3 = new byte[bits34][];
            var bit14 = new bool[bits34];
            var bitE = new bool[bits34];
            var bits1C = new uint[bits34];
            var bit2C = new bool[bits34];

            for (var i = 0; i < bits34; ++i)
            {
                guid3[i] = new byte[8];
                guid3[i][0] = packet.Translator.ReadBit();
                bit14[i] = packet.Translator.ReadBit();
                packet.Translator.StartBitStream(guid3[i], 6, 7);
                bitE[i] = packet.Translator.ReadBit();
                packet.Translator.StartBitStream(guid3[i], 3, 2, 1);
                bits1C[i] = packet.Translator.ReadBits(21);
                bit2C[i] = packet.Translator.ReadBit();
                packet.Translator.StartBitStream(guid3[i], 5, 4);
            }

            packet.Translator.StartBitStream(guid2, 4, 1, 0);

            for (var i = 0; i < bits34; ++i)
            {
                if (bitE[i])
                    packet.Translator.ReadInt16("IntE", i);

                packet.Translator.ReadXORByte(guid3[i], 3);
                packet.Translator.ReadXORByte(guid3[i], 4);

                for (var j = 0; j < bits1C[i]; ++j)
                {
                    packet.Translator.ReadByte("ByteED", i, j);
                    packet.Translator.ReadInt32("IntED", i, j);
                }

                if (bit14[i])
                    packet.Translator.ReadInt32("Int14", i);

                packet.Translator.ReadXORByte(guid3[i], 0);
                packet.Translator.ReadXORByte(guid3[i], 2);
                packet.Translator.ReadXORByte(guid3[i], 6);

                var len = packet.Translator.ReadInt32("Int30", i);

                packet.Translator.ReadBytes(len);

                packet.Translator.ReadXORByte(guid3[i], 1);
                packet.Translator.ReadXORByte(guid3[i], 7);
                packet.Translator.ReadXORByte(guid3[i], 5);

                packet.Translator.ReadByte("ByteED", i);
                packet.Translator.ReadInt32("IntED", i);

                packet.Translator.WriteGuid("Guid3", guid3[i], i);
            }

            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid2, 0);

            packet.Translator.ReadInt32("Int30");

            if (bit28)
            {
                packet.Translator.ReadInt32("Int20");
                packet.Translator.ReadXORByte(guid1, 1);
                packet.Translator.ReadXORByte(guid1, 3);
                packet.Translator.ReadInt32("Int24");
                packet.Translator.ReadXORByte(guid1, 6);
                packet.Translator.ReadXORByte(guid1, 2);
                packet.Translator.ReadXORByte(guid1, 5);
                packet.Translator.ReadXORByte(guid1, 4);
                packet.Translator.ReadXORByte(guid1, 0);
                packet.Translator.ReadXORByte(guid1, 7);
                packet.Translator.ReadInt64("Int18");
                packet.Translator.WriteGuid("Guid1", guid1);
            }

            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 3);

            for (var i = 0; i < bits44; ++i)
                packet.Translator.ReadInt16("Int44", i);

            for (var i = 0; i < bits60; ++i)
            packet.Translator.ReadInt16("Int66", i);

            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_19)]
        public static void HandleUnknown19(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 7, 2, 5, 1, 3, 0, 6, 4);
            packet.Translator.ParseBitStream(guid, 2, 5, 1, 0, 7, 3, 4, 6);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1394)]
        public static void HandleUnknown1394(Packet packet)
        {
            packet.Translator.ReadInt64("Int18");
            packet.Translator.ReadInt64("Int28");
            packet.Translator.ReadInt64("Int20");
            packet.Translator.ReadInt64("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_1203)] // Instance stuff?
        public static void HandleUnknown1203(Packet packet)
        {
            var bits10 = packet.Translator.ReadBits(20);

            var guid = new byte[bits10][];

            for (var i = 0; i < bits10; ++i)
            {
                guid[i] = new byte[8];
                packet.Translator.StartBitStream(guid[i], 0, 7, 5);
                packet.Translator.ReadBit();
                packet.Translator.StartBitStream(guid[i], 2, 1);
                packet.Translator.ReadBit();
                packet.Translator.StartBitStream(guid[i], 3, 4, 6);
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadXORByte(guid[i], 5);
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadXORByte(guid[i], 1);
                packet.Translator.ReadXORByte(guid[i], 4);
                packet.Translator.ReadXORByte(guid[i], 6);
                packet.Translator.ReadXORByte(guid[i], 3);
                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadXORByte(guid[i], 2);
                packet.Translator.ReadXORByte(guid[i], 0);
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadXORByte(guid[i], 7);
                packet.Translator.WriteGuid("Guid", guid[i], i);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_152)]
        public static void HandleUnknown152(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 6);

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("IntEB", i);
                packet.Translator.ReadInt32("IntEB", i);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_6528)]
        public static void HandleUnknown6528(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var powerGUID = new byte[8];

            guid1[5] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid2, 5, 4);
            guid1[2] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            var hasPowerData = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();

            var powerCount = 0u;
            if (hasPowerData)
            {
                packet.Translator.StartBitStream(powerGUID, 1, 6, 3);
                powerCount = packet.Translator.ReadBits(21);
                packet.Translator.StartBitStream(powerGUID, 2, 0, 7, 4, 5);
            }

            packet.Translator.StartBitStream(guid1, 4, 1);
            guid2[2] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid1, 3);

            if (hasPowerData)
            {
                packet.Translator.ReadXORByte(powerGUID, 5);
                packet.Translator.ReadInt32("Current health");
                packet.Translator.ReadXORByte(powerGUID, 1);
                packet.Translator.ReadXORByte(powerGUID, 7);
                packet.Translator.ReadInt32("Attack power");
                packet.Translator.ReadXORByte(powerGUID, 6);
                packet.Translator.ReadInt32("Spell power");
                packet.Translator.ReadXORByte(powerGUID, 4);
                packet.Translator.ReadXORByte(powerGUID, 0);

                for (var i = 0; i < powerCount; ++i)
                {
                    packet.Translator.ReadInt32E<PowerType>("Power type", i); // Actually powertype for class
                    packet.Translator.ReadInt32("Value", i);
                }

                packet.Translator.ReadXORByte(powerGUID, 2);
                packet.Translator.ReadXORByte(powerGUID, 3);

                packet.Translator.WriteGuid("Power GUID", powerGUID);
            }

            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadInt32("Int1C"); // spellId?
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid2, 2);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_54)]
        public static void HandleUnknown54(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 6, 2, 5, 4, 7, 0, 1, 3);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadByte("Byte18");
            packet.Translator.ReadXORByte(guid, 6);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5298)]
        public static void HandleUnknown5298(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var powerGUID = new byte[8];

            guid2[3] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid2, 1, 2);
            packet.Translator.StartBitStream(guid1, 2, 4);
            packet.Translator.StartBitStream(guid2, 7, 0);
            packet.Translator.StartBitStream(guid1, 7, 6);
            guid2[4] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 5, 0);
            packet.Translator.StartBitStream(guid2, 6, 5);

            var hasPowerData = packet.Translator.ReadBit();
            var powerCount = 0u;
            if (hasPowerData)
            {
                packet.Translator.StartBitStream(powerGUID, 3, 4, 0, 7);
                powerCount = packet.Translator.ReadBits(21);
                packet.Translator.StartBitStream(powerGUID, 2, 5, 1, 6);
            }

            guid1[1] = packet.Translator.ReadBit();
            if (hasPowerData)
            {
                packet.Translator.ReadXORByte(powerGUID, 5);

                for (var i = 0; i < powerCount; ++i)
                {
                    packet.Translator.ReadInt32("Value", i);
                    packet.Translator.ReadInt32E<PowerType>("Power type", i); // Actually powertype for class
                }

                packet.Translator.ReadInt32("Current health");
                packet.Translator.ReadXORByte(powerGUID, 7);
                packet.Translator.ReadInt32("Spell power");
                packet.Translator.ReadXORByte(powerGUID, 1);
                packet.Translator.ReadXORByte(powerGUID, 0);
                packet.Translator.ReadInt32("Attack power");
                packet.Translator.ReadXORByte(powerGUID, 2);
                packet.Translator.ReadXORByte(powerGUID, 3);
                packet.Translator.ReadXORByte(powerGUID, 4);
                packet.Translator.ReadXORByte(powerGUID, 6);
                packet.Translator.WriteGuid("Power GUID", powerGUID);
            }

            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadInt32("Int48"); // SpellId?
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid2, 6);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_410)] // SMSG_GAME_OBJECT_CUSTOM_ANIM???
        public static void HandleUnknown410(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadBit("bit14");
            packet.Translator.StartBitStream(guid, 6, 4, 0, 2);
            var bit10 = !packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid, 5, 7, 3, 1);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 7);

            if (bit10)
                packet.Translator.ReadInt32("Int10"); // Anim?

            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1183)]
        public static void HandleUnknown1183(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[1] = packet.Translator.ReadBit();
            var bit15 = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 0, 1);
            guid2[4] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 3, 5);
            var bits40 = packet.Translator.ReadBits(21);
            guid1[4] = packet.Translator.ReadBit();
            var bit14 = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            for (var i = 0; i < bits40; ++i)
            {
                packet.Translator.ReadBit("bit5", i);
                packet.Translator.ReadBit("bit7", i);
                packet.Translator.ReadBit("bit4", i);
                packet.Translator.ReadBit("bit8", i);
                packet.Translator.ReadBit("bit6", i);
            }

            packet.Translator.StartBitStream(guid2, 5, 7, 3);
            packet.Translator.StartBitStream(guid1, 2, 6);
            guid2[0] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadInt32("Int54");
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadByte("Byte50");
            packet.Translator.ReadInt32("Int10");
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadInt32("Int20");
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadInt32("Unix Time");
            packet.Translator.ReadXORByte(guid2, 7);

            for (var i = 0; i < bits40; ++i)
                packet.Translator.ReadInt32("Int44", i);

            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadInt32("Int34");
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadInt32("Int30");

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1431)]
        public static void HandleUnknown1431(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.Translator.StartBitStream(guid1, 2, 4);
            packet.Translator.StartBitStream(guid2, 1, 2);
            packet.Translator.StartBitStream(guid1, 0, 5);
            guid2[4] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid2, 7, 0, 3, 6);
            packet.Translator.StartBitStream(guid1, 1, 6);
            guid2[5] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadByte("Byte21");
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadByte("Byte20");

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1460)]
        public static void HandleUnknown1460(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.Translator.ReadPackedTime("Date");
            packet.Translator.ReadInt32("Int20"); // RealmId?
            packet.Translator.ReadInt32("Int24"); // AchievementId?
            packet.Translator.ReadInt32("Int18"); // RealmId?
            guid1[6] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid2, 5, 3, 0, 1, 6);
            guid1[2] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 1, 4, 5, 7);
            guid2[7] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("bit1C"); // Flags?
            guid1[3] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 2);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4393)]
        public static void HandleUnknown4393(Packet packet)
        {
            packet.Translator.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_22)]
        public static void HandleUnknown22(Packet packet)
        {
            packet.Translator.ReadInt32("Int18");
            var bit10 = !packet.Translator.ReadBit();
            var bit3C = packet.Translator.ReadBit();
            var bits14 = packet.Translator.ReadBits("bits14", 2);

            if (bits14 == 2)
                packet.Translator.ReadBit("bit34");

            if (bits14 == 2)
            {
                packet.Translator.ReadInt32("Int24");
                packet.Translator.ReadInt32("Int2C");
                packet.Translator.ReadInt32("Int30");
                packet.Translator.ReadInt32("Int1C");
                packet.Translator.ReadInt32("Int20");
                packet.Translator.ReadInt32("Int28");
            }

            if (bit10)
                packet.Translator.ReadByte("Byte10");
            if (bits14 == 1)
                packet.Translator.ReadInt32("Int38");
        }

        [Parser(Opcode.SMSG_UNKNOWN_6018)]
        public static void HandleUnknown6018(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadSingle("Float1C");
            packet.Translator.ReadInt32("Int18");

            packet.Translator.StartBitStream(guid, 6, 3, 1, 4, 0, 5, 2, 7);
            packet.Translator.ParseBitStream(guid, 5, 3, 1, 7, 0, 6, 4, 2);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4950)]
        public static void HandleUnknown4950(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 0, 5, 2, 6, 7, 4, 1, 3);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadInt32("Int1C");
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadSingle("Float18");

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_6047)]
        public static void HandleUnknown6047(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 1, 5, 4, 0, 6, 3, 2, 7);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadInt32("Int1C");
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadSingle("Float18");

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5659)]
        public static void HandleUnknown5659(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 3, 0, 1, 5, 2, 7, 6, 4);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadSingle("Float1C");
            packet.Translator.ReadXORByte(guid, 4);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5971)]
        public static void HandleUnknown5971(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 4, 0, 7, 3, 1, 6, 2, 5);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 1);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5702)]
        public static void HandleUnknown5702(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 3, 2, 6, 1, 0, 7, 4, 5);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 7);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1406)]
        public static void HandleUnknown1406(Packet packet)
        {
            var bits10 = packet.Translator.ReadBits(19);

            var guid1 = new byte[bits10][];
            var guid2 = new byte[bits10][];

            for (var i = 0; i < bits10; ++i)
            {
                guid1[i] = new byte[8];
                guid2[i] = new byte[8];

                packet.Translator.StartBitStream(guid1[i], 3, 6);
                packet.Translator.StartBitStream(guid2[i], 5, 4);
                guid1[i][2] = packet.Translator.ReadBit();
                guid2[i][0] = packet.Translator.ReadBit();
                guid1[i][7] = packet.Translator.ReadBit();
                packet.Translator.StartBitStream(guid2[i], 6, 7, 3);
                packet.Translator.StartBitStream(guid1[i], 5, 0);
                guid2[i][2] = packet.Translator.ReadBit();
                packet.Translator.StartBitStream(guid1[i], 2, 4);
                guid2[i][1] = packet.Translator.ReadBit();
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.Translator.ReadXORByte(guid2[i], 4);

                packet.Translator.ReadInt32("Int14");

                packet.Translator.ReadXORByte(guid1[i], 2);
                packet.Translator.ReadXORByte(guid1[i], 0);

                packet.Translator.ReadInt32("Int14");

                packet.Translator.ReadXORByte(guid2[i], 7);
                packet.Translator.ReadXORByte(guid2[i], 1);

                packet.Translator.ReadInt32("Int14");
                packet.Translator.ReadInt32("Int14");

                packet.Translator.ReadXORByte(guid2[i], 6);
                packet.Translator.ReadXORByte(guid1[i], 1);
                packet.Translator.ReadXORByte(guid2[i], 3);
                packet.Translator.ReadXORByte(guid1[i], 4);

                packet.Translator.ReadInt32("Int14");

                packet.Translator.ReadXORByte(guid1[i], 5);
                packet.Translator.ReadXORByte(guid1[i], 3);
                packet.Translator.ReadXORByte(guid2[i], 0);
                packet.Translator.ReadXORByte(guid1[i], 6);
                packet.Translator.ReadXORByte(guid1[i], 7);
                packet.Translator.ReadXORByte(guid2[i], 2);
                packet.Translator.ReadXORByte(guid2[i], 5);

                packet.Translator.WriteGuid("Guid1", guid1[i]);
                packet.Translator.WriteGuid("Guid2", guid2[i]);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_1090)]
        public static void HandleUnknown1090(Packet packet)
        {
            packet.Translator.ReadInt32("Int10");
            packet.Translator.ReadInt32("Int28");
            packet.Translator.ReadInt32("Int2C");
            packet.Translator.ReadInt32("Int24");

            var bits14 = (int)packet.Translator.ReadBits(21);

            for (var i = 0; i < bits14; ++i)
            {
                packet.Translator.ReadInt32("Int8", i);
                packet.Translator.ReadInt32("Int84", i);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_2227)]
        public static void HandleUnknown2227(Packet packet)
        {
            packet.Translator.ReadInt32("Int20");
            packet.Translator.ReadSingle("Float10");
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadSingle("Float14");
            packet.Translator.ReadSingle("Float1C");
        }

        [Parser(Opcode.SMSG_UNKNOWN_159)]
        public static void HandleUnknown159(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 3, 2, 5, 0, 7);
            packet.Translator.ReadBits("bits10", 2);
            packet.Translator.StartBitStream(guid, 6, 4, 1);
            packet.Translator.ParseBitStream(guid, 7, 3, 2, 1, 4, 6, 0, 5);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4930)]
        public static void HandleUnknown4930(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.Translator.ReadSingle("Float34");
            packet.Translator.ReadSingle("Float14");
            packet.Translator.ReadSingle("Float10");
            packet.Translator.ReadSingle("Float18");
            packet.Translator.ReadInt32("Int30");
            var bit3B = packet.Translator.ReadBit();

            packet.Translator.StartBitStream(guid2, 3, 2);

            var bit28 = packet.Translator.ReadBit();

            packet.Translator.StartBitStream(guid2, 7, 1);

            if (bit28)
                packet.Translator.StartBitStream(guid1, 2, 5, 3, 6, 1, 4, 7, 0);

            guid2[4] = packet.Translator.ReadBit();
            if (bit3B)
            {
                packet.Translator.ReadBit("bit3A");
                packet.Translator.ReadBit("bit39");
            }

            packet.Translator.StartBitStream(guid2, 6, 0, 5);

            packet.Translator.ReadXORByte(guid2, 7);

            if (bit28)
            {
                packet.Translator.ParseBitStream(guid1, 3, 0, 1, 7, 2, 6, 5, 4);

                packet.Translator.WriteGuid("Guid1", guid1);
            }

            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 6);

            if (bit3B)
                packet.Translator.ReadByte("Byte38");

            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4703)]
        public static void HandleUnknown4703(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 0, 2, 1, 6, 7, 3, 4, 5);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 4);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5838)]
        public static void HandleUnknown5838(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 2, 1, 5, 0, 3, 4, 6, 7);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 0);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1299)]
        public static void HandleUnknown1299(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadBit("bit10");
            packet.Translator.StartBitStream(guid, 2, 1, 4, 6, 5, 7, 0, 3);
            packet.Translator.ParseBitStream(guid, 5, 1, 2, 4, 0, 3, 7, 6);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2307)]
        public static void HandleUnknown2307(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadInt32("Int20"); // mapId?
            packet.Translator.ReadSingle("Float14");
            packet.Translator.ReadInt32("Int30"); // mapId?
            packet.Translator.ReadSingle("Float18");
            packet.Translator.ReadSingle("Float1C");

            packet.Translator.StartBitStream(guid, 5, 3, 4, 2, 6, 0, 7, 1);
            packet.Translator.ReadBit("bit10");
            packet.Translator.ParseBitStream(guid, 4, 5, 2, 0, 1, 6, 7, 3);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_6174)]
        public static void HandleUnknown6174(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 22);
            for (var i = 0; i < count; ++i)
                packet.Translator.ReadInt32("Int14", i);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5376)]
        public static void HandleUnknown5376(Packet packet)
        {
            var bits10 = packet.Translator.ReadBits(19);
            for (var i = 0; i < bits10; ++i)
                packet.Translator.ReadBit("bit14", i);

            for (var i = 0; i < bits10; ++i)
            {
                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadInt32("Int14", i);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_1031)]
        public static void HandleUnknown1031(Packet packet)
        {
            for (var i = 0; i < 256; ++i)
                packet.Translator.ReadBit("bit10", i);
        }

        [Parser(Opcode.SMSG_UNKNOWN_272)]
        public static void HandleUnknown272(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var guid3 = new byte[8];

            packet.Translator.StartBitStream(guid1, 4, 5, 2, 3, 7);

            var bit30 = packet.Translator.ReadBit();
            if (bit30)
                packet.Translator.StartBitStream(guid3, 5, 1, 3, 0, 7, 6, 2, 4);

            var bit20 = packet.Translator.ReadBit();
            if (bit20)
                packet.Translator.StartBitStream(guid2, 6, 4, 0, 2, 7, 5, 1, 3);

            packet.Translator.StartBitStream(guid1, 1, 6, 0);

            if (bit20)
            {
                packet.Translator.ParseBitStream(guid2, 4, 7, 5, 1, 2, 6, 3, 0);
                packet.Translator.WriteGuid("Guid2", guid2);
            }

            if (bit30)
            {
                packet.Translator.ParseBitStream(guid3, 3, 2, 7, 0, 5, 1, 6, 4);
                packet.Translator.WriteGuid("Guid3", guid3);
            }

            packet.Translator.ParseBitStream(guid1, 0, 2, 5, 4, 3, 1, 6, 7);
            packet.Translator.WriteGuid("Guid1", guid1);
        }

        [Parser(Opcode.CMSG_UNKNOWN_515)] // CMSG_TIME_SYNC_RESP?
        public static void HandleUnknown515(Packet packet)
        {
            packet.Translator.ReadInt32("Int10"); // Count?
            packet.Translator.ReadInt32("Int20"); // Ticks?
        }

        [Parser(Opcode.CMSG_UNKNOWN_6083)] // Guild opcode?
        public static void HandleUnknown6083(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[2] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid2, 6, 1);
            packet.Translator.StartBitStream(guid1, 5, 4, 6, 1);
            guid2[3] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid2, 5, 7);


            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid2, 1);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.MSG_MULTIPLE_PACKETS1)] // CMSG_TIME_SYNC_RESP?
        public static void HandleMultiplePackets1(Packet packet)
        {
            packet.Formatter.AppendItem("ClientToServer: CMSG_UNKNOWN_4278"); // Addon?
            var len1 = packet.Translator.ReadByte();
            var len2 = packet.Translator.ReadBits(5);

            packet.Translator.ReadWoWString("string1", len2);
            packet.Translator.ReadWoWString("Text", len1);
        }

        [Parser(Opcode.CMSG_UNKNOWN_4524)]
        public static void HandleUnknown4524(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.Translator.StartBitStream(guid1, 6, 3, 1);
            packet.Translator.StartBitStream(guid2, 0, 1);
            packet.Translator.StartBitStream(guid1, 4, 2);
            packet.Translator.StartBitStream(guid2, 3, 7, 4);
            guid1[5] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid2, 6, 5);
            guid1[0] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid1, 7);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.CMSG_UNKNOWN_6774)] // Item opcode?
        public static void HandleUnknown6774(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 7, 6, 0, 4, 5, 3, 2, 1);
            packet.Translator.ParseBitStream(guid, 2, 0, 3, 1, 6, 7, 4, 5);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_UNKNOWN_5412)]
        public static void HandleUnknown5412(Packet packet)
        {
            var bit20 = !packet.Translator.ReadBit();
            var bit16 = !packet.Translator.ReadBit();

            if (bit16)
                packet.Translator.ReadInt32("Int16"); // spellId?

            if (bit20)
                packet.Translator.ReadByte("Byte20");
        }

        [Parser(Opcode.MSG_UNKNOWN_6315)] // Item opcode?
        public static void HandleUnknown6315(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                var guid = new byte[8];

                packet.Translator.ReadInt32("Int10");

                packet.Translator.StartBitStream(guid, 0, 4, 1, 7, 6, 2, 5, 3);
                packet.Translator.ParseBitStream(guid, 4, 0, 6, 7, 3, 2, 1, 5);

                packet.Translator.WriteGuid("Guid", guid);
            }
            else
                packet.Translator.ReadInt32("Int10");
        }

        [Parser(Opcode.CMSG_UNKNOWN_6062)]
        public static void HandleUnknown6062(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 3, 1, 2, 6, 4, 7, 0, 5);
            packet.Translator.ParseBitStream(guid, 3, 0, 5, 2, 7, 6, 1, 4);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2109)]
        public static void HandleUnknown2109(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 1, 5, 3, 0, 6, 4, 7, 2);
            packet.Translator.ReadInt32("Int1C");
            packet.Translator.ParseBitStream(guid, 0, 5, 1, 7, 2, 4, 6, 3);
            packet.Translator.ReadInt32("Int18");

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.MSG_UNKNOWN_5383)]
        public static void HandleUnknown5383(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                var guid = new byte[8];

                packet.Translator.ReadInt32("Int10");

                packet.Translator.StartBitStream(guid, 3, 6, 2, 0, 4, 1, 7, 5);
                packet.Translator.ParseBitStream(guid, 4, 0, 1, 7, 5, 3, 2, 6);

                packet.Translator.WriteGuid("Guid", guid);
            }
            else
            {
                var guid1 = new byte[8];
                var guid2 = new byte[8];
                var powerGUID = new byte[8];

                packet.Translator.ReadInt32("Int50");
                packet.Translator.ReadInt32("Int10");
                packet.Translator.ReadInt32("Int64");
                packet.Translator.ReadInt32("Int14");
                packet.Translator.ReadInt32("Int60");

                packet.Translator.StartBitStream(guid2, 1, 6);
                packet.Translator.StartBitStream(guid1, 3, 2, 7);
                packet.Translator.StartBitStream(guid2, 3, 2);
                guid1[6] = packet.Translator.ReadBit();
                var hasPowerData = packet.Translator.ReadBit();
                guid1[4] = packet.Translator.ReadBit();

                var powerCount = 0u;
                if (hasPowerData)
                {
                    packet.Translator.StartBitStream(powerGUID, 0, 7);
                    powerCount = packet.Translator.ReadBits(21);
                    packet.Translator.StartBitStream(powerGUID, 3, 2, 6, 4, 1, 5);
                }

                guid1[0] = packet.Translator.ReadBit();
                guid2[5] = packet.Translator.ReadBit();
                guid1[1] = packet.Translator.ReadBit();
                guid2[7] = packet.Translator.ReadBit();
                guid2[0] = packet.Translator.ReadBit();
                guid1[5] = packet.Translator.ReadBit();
                guid2[4] = packet.Translator.ReadBit();
                packet.Translator.ReadXORByte(guid2, 3);
                packet.Translator.ReadXORByte(guid1, 5);
                packet.Translator.ReadXORByte(guid1, 1);
                if (hasPowerData)
                {
                    packet.Translator.ReadXORByte(powerGUID, 2);

                    for (var i = 0; i < powerCount; i++)
                    {
                        packet.Translator.ReadInt32("Value", i);
                        packet.Translator.ReadUInt32E<PowerType>("Power type", i);
                    }

                    packet.Translator.ReadInt32("Current Health");
                    packet.Translator.ReadXORByte(powerGUID, 6);
                    packet.Translator.ReadInt32("Spell power");
                    packet.Translator.ReadXORByte(powerGUID, 4);
                    packet.Translator.ReadXORByte(powerGUID, 1);
                    packet.Translator.ReadXORByte(powerGUID, 3);
                    packet.Translator.ReadInt32("Attack Power");
                    packet.Translator.ReadXORByte(powerGUID, 0);
                    packet.Translator.ReadXORByte(powerGUID, 7);
                    packet.Translator.ReadXORByte(powerGUID, 5);
                    packet.Translator.WriteGuid("Power GUID", powerGUID);
                }

                packet.Translator.ReadXORByte(guid2, 4);
                packet.Translator.ReadXORByte(guid1, 4);
                packet.Translator.ReadXORByte(guid1, 6);
                packet.Translator.ReadXORByte(guid2, 7);
                packet.Translator.ReadXORByte(guid1, 2);
                packet.Translator.ReadXORByte(guid2, 2);
                packet.Translator.ReadXORByte(guid1, 0);
                packet.Translator.ReadXORByte(guid2, 6);
                packet.Translator.ReadXORByte(guid2, 1);
                packet.Translator.ReadXORByte(guid2, 0);
                packet.Translator.ReadXORByte(guid2, 5);
                packet.Translator.ReadXORByte(guid1, 3);
                packet.Translator.ReadXORByte(guid1, 7);

                packet.Translator.WriteGuid("Guid1", guid1);
                packet.Translator.WriteGuid("Guid2", guid2);
            }
        }

        [Parser(Opcode.CMSG_UNKNOWN_5177)]
        public static void HandleUnknown5177(Packet packet)
        {
            var len1 = packet.Translator.ReadBits(8);
            var len2 = packet.Translator.ReadBits(5);
            packet.Translator.ReadWoWString("String1", len2);
            packet.Translator.ReadWoWString("String1", len1);
        }

        [Parser(Opcode.CMSG_UNKNOWN_5758)]
        public static void HandleUnknown5758(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 2, 5, 1, 6, 7, 4, 0, 3);
            packet.Translator.ParseBitStream(guid, 0, 1, 5, 6, 4, 2, 3, 7);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_UNKNOWN_1827)]
        public static void HandleUnknown1827(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            var bit92 = false;
            var bit100 = false;
            var bit136 = false;

            packet.Translator.ReadSingle("float44");
            packet.Translator.ReadSingle("float36");
            packet.Translator.ReadSingle("float40");

            var bit48 = !packet.Translator.ReadBit();
            var bit144 = !packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 4, 3);
            var bit104 = packet.Translator.ReadBit();
            var bit172 = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            var bit24 = !packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            var bits152 = packet.Translator.ReadBits(22);
            guid1[6] = packet.Translator.ReadBit();
            var bit168 = !packet.Translator.ReadBit();
            var bit28 = !packet.Translator.ReadBit();
            var bit32 = !packet.Translator.ReadBit();
            packet.Translator.ReadBit("bit149");
            var bit140 = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("bit148");
            packet.Translator.StartBitStream(guid1, 7, 1);

            var bit112 = !packet.Translator.ReadBit();

            if (bit104)
            {
                packet.Translator.StartBitStream(guid2, 4, 3, 2);
                bit92 = packet.Translator.ReadBit();
                bit100 = packet.Translator.ReadBit();
                packet.Translator.StartBitStream(guid2, 7, 1, 0, 5, 6);
            }

            if (bit24)
                packet.Translator.ReadBits("bits24", 30);

            if (bit140)
                bit136 = packet.Translator.ReadBit();

            if (bit28)
                packet.Translator.ReadBits("bits28", 13);

            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 2);

            for (var i = 0; i < bits152; ++i)
                packet.Translator.ReadInt32("Int152");

            packet.Translator.ReadXORByte(guid1, 3);

            if (bit104)
            {
                packet.Translator.ReadXORByte(guid2, 0);
                packet.Translator.ReadXORByte(guid2, 5);
                packet.Translator.ReadXORByte(guid2, 6);
                packet.Translator.ReadXORByte(guid2, 7);

                if (bit100)
                    packet.Translator.ReadInt32("Int100");

                packet.Translator.ReadXORByte(guid2, 2);

                packet.Translator.ReadSingle("float76");
                packet.Translator.ReadInt32("Int84");
                packet.Translator.ReadByte("byte80");

                packet.Translator.ReadXORByte(guid2, 1);

                packet.Translator.ReadSingle("float72");
                packet.Translator.ReadSingle("float64");

                packet.Translator.ReadXORByte(guid2, 4);

                if (bit92)
                    packet.Translator.ReadInt32("Int88");

                packet.Translator.ReadXORByte(guid2, 3);
                packet.Translator.ReadSingle("float68");

                packet.Translator.WriteGuid("Guid2", guid2);
            }

            if (bit32)
                packet.Translator.ReadInt32("Int32");

            if (bit112)
                packet.Translator.ReadSingle("float112");

            if (bit140)
            {
                packet.Translator.ReadInt32("Int116");

                if (bit136)
                {
                    packet.Translator.ReadSingle("float132");
                    packet.Translator.ReadSingle("float128");
                    packet.Translator.ReadSingle("float124");
                }

                packet.Translator.ReadSingle("float120");
            }

            if (bit168)
                packet.Translator.ReadInt32("Int116");

            if (bit48)
                packet.Translator.ReadSingle("float48");

            if (bit144)
                packet.Translator.ReadSingle("float144");

            packet.Translator.WriteGuid("Guid1", guid1);
        }

        [Parser(Opcode.CMSG_UNKNOWN_4831)]
        public static void HandleUnknown4831(Packet packet)
        {
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadInt32("Int18");
            var len = packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadBytes(len);
            packet.Translator.ReadBits("bit3", 3);
        }

        [Parser(Opcode.CMSG_UNKNOWN_822)]
        public static void HandleUnknown822(Packet packet)
        {
            var bit16 = !packet.Translator.ReadBit();

            if (bit16)
                packet.Translator.ReadInt32("Int16");
        }

        [Parser(Opcode.SMSG_UNKNOWN_289)]
        public static void HandleUnknown289(Packet packet)
        {
            packet.Translator.ReadInt32("Unk1 Int32");
        }

        [Parser(Opcode.CMSG_UNKNOWN_5079)]
        public static void HandleUnknown5079(Packet packet)
        {
            packet.Translator.ReadInt32("Unk1 Int32");
        }

        [Parser(Opcode.SMSG_UNKNOWN_1446)]
        public static void HandleUnknown1446(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.Translator.StartBitStream(guid1, 7, 0, 5);
            guid2[5] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid2, 2, 3);
            guid1[2] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid2, 1, 7);
            guid1[1] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid2, 4, 6);
            packet.Translator.StartBitStream(guid1, 4, 3);
            guid2[0] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadByte("Byte20");
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid1, 0);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_435)]
        public static void HandleUnknown435(Packet packet)
        {
            packet.Translator.ReadInt32("Unk1 Int32");
        }

        [Parser(Opcode.SMSG_UNKNOWN_4998)] // Pet opcode?
        public static void HandleUnknown4998(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 6, 5, 0, 3, 1, 7, 4, 2);
            packet.Translator.ParseBitStream(guid, 7, 6, 5, 1, 4, 3, 2, 0);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2187)]
        public static void HandleUnknown2187(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.Translator.ReadInt32("Int20");
            packet.Translator.ReadByte("Byte28");
            packet.Translator.ReadInt32("Int24");
            packet.Translator.StartBitStream(guid1, 0, 3);
            packet.Translator.StartBitStream(guid2, 1, 7);
            guid1[5] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid2, 4, 3);
            packet.Translator.StartBitStream(guid1, 2, 7, 6);
            guid2[6] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid2, 5, 2);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 5);

            packet.Translator.WriteGuid("Guid2", guid1);
            packet.Translator.WriteGuid("Guid3", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_256)]
        public static void HandleUnknown256(Packet packet)
        {
            packet.Translator.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_1032)]
        public static void HandleUnknown1032(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 6, 5, 1, 2, 4, 0, 3, 7);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadSingle("Float18");
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadSingle("Float1C");
            packet.Translator.ReadXORByte(guid, 6);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_UNKNOWN_5091)]
        public static void HandleUnknown5091(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadByte("Unk byte");

            packet.Translator.StartBitStream(guid, 5, 3, 0, 7, 4, 6, 2, 1);
            packet.Translator.ParseBitStream(guid, 7, 2, 0, 6, 5, 3, 4, 1);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_6199)]
        public static void HandleUnknown6199(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var powerGUID = new byte[8];

            packet.Translator.ReadInt32("Int50"); // SpellId?

            packet.Translator.StartBitStream(guid1, 4, 1, 5);
            packet.Translator.StartBitStream(guid2, 2, 1);
            guid1[3] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 7, 0);
            packet.Translator.StartBitStream(guid2, 0, 7, 3);
            packet.Translator.StartBitStream(guid1, 6, 2);
            var hasPowerData = packet.Translator.ReadBit();

            var powerCount = 0u;
            if (hasPowerData)
            {
                packet.Translator.StartBitStream(powerGUID, 4, 7, 3, 1, 2, 0, 6, 5);
                powerCount = packet.Translator.ReadBits(21);
            }

            guid2[4] = packet.Translator.ReadBit();
            var bit54 = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 3);

            if (hasPowerData)
            {
                packet.Translator.ReadXORByte(powerGUID, 6);

                for (var i = 0; i < powerCount; ++i)
                {
                    packet.Translator.ReadUInt32E<PowerType>("Power type", i);
                    packet.Translator.ReadInt32("Value", i);
                }

                packet.Translator.ReadXORByte(powerGUID, 4);
                packet.Translator.ReadXORByte(powerGUID, 0);
                packet.Translator.ReadXORByte(powerGUID, 5);
                packet.Translator.ReadXORByte(powerGUID, 7);
                packet.Translator.ReadInt32("Current Health");
                packet.Translator.ReadXORByte(powerGUID, 1);
                packet.Translator.ReadInt32("Spell power");
                packet.Translator.ReadInt32("Attack Power");
                packet.Translator.ReadXORByte(powerGUID, 3);
                packet.Translator.ReadXORByte(powerGUID, 2);
                packet.Translator.WriteGuid("Power GUID", powerGUID);
            }

            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 1);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.CMSG_UNKNOWN_5134)]
        public static void HandleUnknown5134(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadInt32("Int10");
            packet.Translator.ReadBit(); // fake bit?
            packet.Translator.StartBitStream(guid, 0, 2, 4, 1, 3, 7, 5, 6);
            packet.Translator.ParseBitStream(guid, 5, 1, 4, 6, 0, 7, 3, 2);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_UNKNOWN_2951)]
        public static void HandleUnknown2951(Packet packet)
        {
            packet.Translator.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_1155)]
        public static void HandleUnknown1155(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 7, 4, 2, 5, 3, 6, 0, 1);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 5);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4277)]
        public static void HandleUnknown4277(Packet packet)
        {
            var guid = new byte[8];

            var bits18 = packet.Translator.ReadBits(21);
            var bit28 = !packet.Translator.ReadBit();

            packet.Translator.StartBitStream(guid, 4, 2, 5, 6, 0, 3, 7, 1);

            for (var i = 0; i < bits18; ++i)
            {
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadInt32("IntED", i);
            }

            packet.Translator.ReadXORByte(guid, 4);
            if (bit28)
                packet.Translator.ReadByte("Byte28");
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 3);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_3162)]
        public static void HandleUnknown3162(Packet packet)
        {
            packet.Translator.ReadInt32("Int10");

            var bits14 = packet.Translator.ReadBits("Count", 19);

            var bits4 = new uint[bits14];
            for (var i = 0; i < bits14; ++i)
                bits4[i] = packet.Translator.ReadBits(22);

            for (var i = 0; i < bits14; ++i)
            {
                packet.Translator.ReadInt32("Int18", i);
                packet.Translator.ReadInt64("IntED", i);
                packet.Translator.ReadInt32("IntED", i);

                for (var j = 0; j < bits4[i]; ++j)
                    packet.Translator.ReadInt32("IntED", i, j);

                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadInt32("IntED", i);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_1355)]
        public static void HandleUnknown1355(Packet packet)
        {
            uint count = 6;

            for (var i = 0; i < count; ++i)
                packet.Translator.ReadInt32("Int10", i);

            for (var i = 0; i < count; ++i)
                packet.Translator.ReadInt32("Int28", i);

            for (var i = 0; i < count; ++i)
                packet.Translator.ReadInt32("Int40", i);

            for (var i = 0; i < count; ++i)
                packet.Translator.ReadInt32("Int58", i);

            for (var i = 0; i < count; ++i)
                packet.Translator.ReadInt32("Int70", i);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1374)]
        public static void HandleUnknown1374(Packet packet)
        {
            var bits28 = 0;

            var bit428 = packet.Translator.ReadBit();
            if (bit428)
            {
                packet.Translator.ReadBit("bit10");
                bits28 = (int)packet.Translator.ReadBits(10);
            }

            if (bit428)
            {
                packet.Translator.ReadInt32("Int24");
                packet.Translator.ReadWoWString("String28", bits28);
                packet.Translator.ReadInt32("Int14");
                packet.Translator.ReadInt32("Int20");
                packet.Translator.ReadInt32("Int1C");
                packet.Translator.ReadInt32("Int18");
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_1367)]
        public static void HandleUnknown1367(Packet packet)
        {
            packet.Translator.ReadInt64("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_4947)]
        public static void HandleUnknown4947(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 0, 1, 6, 5, 7, 2, 3, 4);
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ParseBitStream(guid, 4, 3, 2, 0, 1, 5, 7, 6);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4799)]
        public static void HandleUnknown4799(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 6, 4, 2, 0, 3, 7, 5, 1);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadXORByte(guid, 5);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_281)]
        public static void HandleUnknown281(Packet packet)
        {
            packet.Translator.ReadBits("bits10", 4);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1411)]
        public static void HandleUnknown1411(Packet packet)
        {
            packet.Translator.ReadInt32("Int10");
            packet.Translator.ReadInt32("Int14");
        }

        [Parser(Opcode.SMSG_UNKNOWN_4990)]
        public static void HandleUnknown4990(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 7, 0, 5, 6, 3, 1, 4, 2);
            packet.Translator.ParseBitStream(guid, 3, 1, 7, 5, 2, 6, 0, 4);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2097)]
        public static void HandleUnknown2097(Packet packet)
        {
            packet.Translator.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_301)] // Group opcode?
        public static void HandleUnknown301(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            packet.Translator.StartBitStream(guid2, 5, 3, 2);
            packet.Translator.StartBitStream(guid1, 1, 3, 2);
            packet.Translator.StartBitStream(guid2, 4, 0, 1);
            packet.Translator.StartBitStream(guid1, 5, 4, 0, 7);
            guid2[6] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadByte("Byte1C");
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadInt32("Int18");

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_133)]
        public static void HandleUnknown133(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 3, 2, 6, 1, 0, 7, 5, 4);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadByte("Byte18");
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 1);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5882)]
        public static void HandleUnknown5882(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 4, 6, 3, 7, 2, 5, 0, 1);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadSingle("Float28");
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadSingle("Float1C");
            packet.Translator.ReadSingle("Float20");
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadSingle("Float24");
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_6166)]
        public static void HandleUnknown6166(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 2, 1, 6, 4, 3, 7, 0, 5);
            packet.Translator.ParseBitStream(guid, 1, 7, 2, 4, 6, 3, 0, 5);
            packet.Translator.ReadInt32("Int18");

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5959)]
        public static void HandleUnknown5959(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 2, 7, 0, 6, 5, 3, 1, 4);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 6);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4679)]
        public static void HandleUnknown4679(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadInt32("Int18");
            packet.Translator.StartBitStream(guid, 2, 7, 1, 3, 5, 6, 4, 0);
            packet.Translator.ParseBitStream(guid, 4, 2, 1, 6, 5, 7, 0, 3);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1432)]
        public static void HandleUnknown1432(Packet packet)
        {
            var bit10 = !packet.Translator.ReadBit();
            if (bit10)
                packet.Translator.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_2181)]
        public static void HandleUnknown2181(Packet packet)
        {
            packet.Translator.ReadSingle("Float18");
            packet.Translator.ReadSingle("Float1C");
            packet.Translator.ReadInt32("Int10");
            packet.Translator.ReadSingle("Float14");
        }

        [Parser(Opcode.SMSG_UNKNOWN_130)]
        public static void HandleUnknown130(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 4, 6, 3, 2, 7, 1, 0, 5);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadByte("Byte18");
            packet.Translator.ReadXORByte(guid, 2);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2083)]
        public static void HandleUnknown2083(Packet packet)
        {
            var bits10 = (int)packet.Translator.ReadBits(22);
            for (var i = 0; i < bits10; ++i)
            {
                packet.Translator.ReadBits("bits0", 8, i);
                packet.Translator.ReadBits("bits0", 8, i);
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.Translator.ReadByte("Byte14", i);
                packet.Translator.ReadByte("Byte14", i);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_1297)]
        public static void HandleUnknown1297(Packet packet)
        {
            var guid = new byte[8];

            var bits28 = (int)packet.Translator.ReadBits(6);
            packet.Translator.StartBitStream(guid, 0, 5);
            var bit10 = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            var bit60 = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid, 2, 1, 4, 6, 3);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadInt32("Int5C");
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadWoWString("String28", bits28);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadInt32("Int14");
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadXORByte(guid, 6);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_48)]
        public static void HandleUnknown48(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 6, 3, 1, 4, 0, 5, 2, 7);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 7);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_310)]
        public static void HandleUnknown310(Packet packet)
        {
            packet.Translator.ReadInt32("Int14");
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadInt32("Int10");
            packet.Translator.ReadInt32("Int1C");
        }

        [Parser(Opcode.SMSG_UNKNOWN_441)]
        public static void HandleUnknown441(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 5, 7, 0, 3, 4, 6, 1, 2);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadInt16("Int18");
            packet.Translator.ReadXORByte(guid, 5);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2224)]
        public static void HandleUnknown2224(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 5, 0, 1, 7, 6, 3, 2, 4);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadInt32("Int20");
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadInt32("Int1C");

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_13)]
        public static void HandleUnknown13(Packet packet)
        {
            packet.Translator.ReadByte("Byte10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_1366)]
        public static void HandleUnknown1366(Packet packet)
        {
            var guid = new byte[8];

            var bits14 = packet.Translator.ReadBits(6);
            packet.Translator.StartBitStream(guid, 7, 1, 3, 0, 5, 6, 2, 4);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadWoWString("String14", bits14);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadInt32("Int10");
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 7);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1386)]
        public static void HandleUnknown1386(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.Translator.StartBitStream(guid1, 2, 6);
            packet.Translator.ReadBit("bit10");
            packet.Translator.StartBitStream(guid1, 0, 1, 5);
            packet.Translator.StartBitStream(guid2, 2, 7);
            guid1[4] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid2, 4, 5, 6, 0);
            packet.Translator.StartBitStream(guid1, 7, 3);
            packet.Translator.StartBitStream(guid2, 1, 3);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadInt32("Int28");
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid2, 2);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_180)]
        public static void HandleUnknown180(Packet packet)
        {
            var bit14 = packet.Translator.ReadBit();
            packet.Translator.ReadBit("bit1C");
            var bit28 = packet.Translator.ReadBit();
            if (bit28)
                packet.Translator.ReadInt32("Int24");
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadInt32("Int20");
            packet.Translator.ReadInt32("Int2C");
            if (bit14)
                packet.Translator.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_1442)]
        public static void HandleUnknown1442(Packet packet)
        {
            packet.Translator.ReadInt32("Int2C");
            packet.Translator.ReadInt32("Int24");
            packet.Translator.ReadInt32("Int20");
            packet.Translator.ReadInt32("Int28");
            var bits10 = packet.Translator.ReadBits(20);

            for (var i = 0; i < bits10; ++i)
                packet.Translator.ReadBit("bit10", i);

            for (var i = 0; i < bits10; ++i)
            {
                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadInt32("Int14", i);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_28)]
        public static void HandleUnknown28(Packet packet)
        {
            var guid = new byte[8];
            var bit161 = packet.Translator.ReadBit();
            var bit129 = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            var bits130 = packet.Translator.ReadBits(6);
            guid[2] = packet.Translator.ReadBit();
            var bit21 = packet.Translator.ReadBit();
            var bit20 = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid, 3, 7);
            var bits28 = packet.Translator.ReadBits(9);
            packet.Translator.StartBitStream(guid, 0, 5);
            var bits10 = packet.Translator.ReadBits(22);
            packet.Translator.StartBitStream(guid, 6, 1);
            packet.Translator.ReadInt32("Int12C");
            packet.Translator.ReadInt64("Int170");
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadInt32("Int164");
            packet.Translator.ReadWoWString("String28", bits28); // Realm?
            packet.Translator.ReadWoWString("String130", bits130); // Name?
            packet.Translator.ReadInt32("Int24");
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 1);
            for (var i = 0; i < bits10; ++i)
                packet.Translator.ReadInt32("IntEA", i);

            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 0);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_146)]
        public static void HandleUnknown146(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 0, 4, 2, 6, 3, 5, 1, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 1);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1076)]
        public static void HandleUnknown1076(Packet packet)
        {
            packet.Translator.ReadInt32("Int14");
            packet.Translator.ReadInt32("Int1C");
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadInt32("Int24");
            packet.Translator.ReadInt32("Int20");
            packet.Translator.ReadBit("bit10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_2212)] // Group opcode?
        public static void HandleUnknown2212(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            packet.Translator.StartBitStream(guid2, 7, 1, 4);
            packet.Translator.StartBitStream(guid1, 3, 6);
            guid2[5] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid2, 0, 6);
            packet.Translator.StartBitStream(guid1, 2, 5, 4, 0);
            guid2[2] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid2, 3);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1310)]
        public static void HandleUnknown1310(Packet packet)
        {
            var bits0 = (int)packet.Translator.ReadBits(8);
            packet.Translator.ReadWoWString("String10", packet.Translator.ReadBits(8));
        }

        [Parser(Opcode.SMSG_UNKNOWN_2063)]
        public static void HandleUnknown2063(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 0, 5, 7, 6, 1, 2, 4, 3);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadByte("Byte18");
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 1);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2091)]
        public static void HandleUnknown2091(Packet packet)
        {
            packet.Translator.ReadInt32("Int1C");
            packet.Translator.ReadSingle("Float10");
            packet.Translator.ReadSingle("Float14");
            packet.Translator.ReadSingle("Float18");
        }

        [Parser(Opcode.SMSG_UNKNOWN_6536)]
        public static void HandleUnknown6536(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 7, 3, 6, 5, 0, 1, 4, 2);
            packet.Translator.ReadInt16("Int20");
            packet.Translator.ReadByte("Byte24");
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadInt32("Int1C");
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadInt16("Int22");

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_UNKNOWN_5766)]
        public static void HandleUnknown5766(Packet packet)
        {
            packet.Translator.ReadBits("bits3", 3);
        }

        [Parser(Opcode.CMSG_UNKNOWN_4266)]
        public static void HandleUnknown4266(Packet packet)
        {
            packet.Translator.ReadBit("Unk bit");
        }

        [Parser(Opcode.CMSG_UNKNOWN_2851)]
        public static void HandleUnknown2851(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadBit("Unk1 bit"); // 0 has guid / 1 has not guid?

            packet.Translator.StartBitStream(guid, 4, 7, 6, 0, 5, 3, 1, 2);
            packet.Translator.ParseBitStream(guid, 7, 0, 3, 6, 5, 2, 4, 1);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_UNKNOWN_5675)]
        public static void HandleUnknown5675(Packet packet)
        {
            packet.Translator.ReadByte("Unk Byte");
        }

        [Parser(Opcode.CMSG_UNKNOWN_5915)]
        public static void HandleUnknown5915(Packet packet)
        {
            packet.Translator.ReadBit("Unk1 bit");
            packet.Translator.ReadBit("Unk2 bit");
        }

        [Parser(Opcode.CMSG_UNKNOWN_597)]
        public static void HandleUnknown597(Packet packet)
        {
            var bits10 = packet.Translator.ReadBits("Unk bits22", 22);

            for (var i = 0; i < bits10; ++i)
                packet.Translator.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.CMSG_UNKNOWN_6910)]
        public static void HandleUnknown6910(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadByte("Unk Byte");
            packet.Translator.ReadInt32("Unk Int32");

            packet.Translator.StartBitStream(guid, 7, 4, 6, 5, 0, 1, 3, 2);
            packet.Translator.ParseBitStream(guid, 5, 4, 2, 6, 3, 7, 0, 1);

            packet.Translator.WriteGuid("Guid", guid);
        }
    }
}

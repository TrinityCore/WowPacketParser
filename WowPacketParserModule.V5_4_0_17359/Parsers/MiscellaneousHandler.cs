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
            var len = packet.ReadBits(9);
            packet.ReadWoWString("File name", len);
        }

        [HasSniffData]
        [Parser(Opcode.CMSG_LOADING_SCREEN_NOTIFY)]
        public static void HandleClientEnterWorld(Packet packet)
        {
            var mapId = packet.ReadUInt32<MapId>("MapID");
            packet.ReadBit("Showing");
        }

        [Parser(Opcode.CMSG_SET_SELECTION)]
        public static void HandleSetSelection(Packet packet)
        {
            var guid = packet.StartBitStream(1, 3, 4, 6, 0, 5, 7, 2);
            packet.ParseBitStream(guid, 7, 6, 0, 2, 3, 1, 4, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_REALM_SPLIT)]
        public static void HandleClientRealmSplit(Packet packet)
        {
            packet.ReadInt32E<ClientSplitState>("Client State");
        }

        [Parser(Opcode.SMSG_SET_TIME_ZONE_INFORMATION)]
        public static void HandleSetTimeZoneInformation(Packet packet)
        {
            var len1 = packet.ReadBits(7);
            var len2 = packet.ReadBits(7);
            packet.ReadWoWString("Server Location", len1);
            packet.ReadWoWString("Server Location", len2);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD)]
        public static void HandleNewWorld(Packet packet)
        {
            var pos = new Vector4();
            pos.Y = packet.ReadSingle();
            pos.O = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadInt32<MapId>("Map");
            packet.AddValue("Position", pos);

            packet.AddSniffData(StoreNameType.Map, (int)CoreParsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }

        [Parser(Opcode.SMSG_REALM_SPLIT)]
        public static void HandleServerRealmSplit(Packet packet)
        {
            var len = packet.ReadBits(7);
            packet.ReadInt32E<PendingSplitState>("Split State");
            packet.ReadInt32E<ClientSplitState>("Client State");
            packet.ReadWoWString("Split Date", len);
        }

        [Parser(Opcode.CMSG_AREA_TRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            var entry = packet.ReadEntry("Area Trigger Id");
            packet.ReadBit("Unk bit1");
            packet.ReadBit("Unk bit2");

            packet.AddSniffData(StoreNameType.AreaTrigger, entry.Key, "AREATRIGGER");
        }

        [Parser(Opcode.SMSG_WEATHER)]
        public static void HandleWeatherStatus(Packet packet)
        {
            var unk = packet.ReadBit("Unk bit");
            var grade = packet.ReadSingle("Grade");
            var state = packet.ReadInt32E<WeatherState>("State");

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
            var count = packet.ReadBits("Count", 20);

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Hotfixed entry", i);
                packet.ReadTime("Hotfix date", i);
                packet.ReadInt32E<DB2Hash>("Hotfix DB2 File", i);
            }
        }

        [Parser(Opcode.SMSG_PLAY_SOUND)]
        public static void HandlePlaySound(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 6, 7, 5, 2, 1, 4, 0, 3);
            packet.ParseBitStream(guid, 7, 0, 5, 4, 3, 1, 2, 6);

            var sound = packet.ReadUInt32("Sound Id");
            packet.WriteGuid("Guid", guid);

            Storage.Sounds.Add(sound, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_WORLD_SERVER_INFO)]
        public static void HandleWorldServerInfo(Packet packet)
        {
            packet.ReadInt32("Instance Difficulty ID");
            packet.ReadTime("Last Weekly Reset");
            packet.ReadByte("Byte18");

            var bit20 = packet.ReadBit();
            var bit38 = packet.ReadBit();
            var bit2C = packet.ReadBit();
            var bit14 = packet.ReadBit();

            if (bit20)
                packet.ReadInt32("Int1C");
            if (bit38)
                packet.ReadInt32("Int34");
            if (bit2C)
                packet.ReadInt32("Int28");
            if (bit14)
                packet.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS)]
        public static void HandleFeatureSystemStatus(Packet packet)
        {
            packet.ReadInt32("Scroll of Resurrections Remaining");
            packet.ReadInt32("Realm Id?");
            packet.ReadByte("Complain System Status");
            packet.ReadInt32("Unused Int32");
            packet.ReadInt32("Scroll of Resurrections Per Day");

            packet.ReadBit("bit26");
            packet.ReadBit("bit54");
            packet.ReadBit("bit30");
            var sessionTimeAlert = packet.ReadBit("Session Time Alert");
            packet.ReadBit("bit38");
            var quickTicket = packet.ReadBit("EuropaTicketSystemEnabled");
            packet.ReadBit("bit25");
            packet.ReadBit("bit24");
            packet.ReadBit("bit28");

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

        [Parser(Opcode.SMSG_PLAY_SCENE)]
        public static void HandleUnknown425(Packet packet)
        {
            var guid = new byte[8];
            // Positions where the scene should start?
            packet.ReadSingle("Z");
            packet.ReadSingle("Y");
            packet.ReadSingle("X");

            var bit34 = !packet.ReadBit();
            var bit1C = !packet.ReadBit();
            var bit24 = !packet.ReadBit();

            packet.ReadBit(); // fake bit

            packet.StartBitStream(guid, 4, 2, 3, 6, 1, 5, 0, 7);

            var bit18 = !packet.ReadBit();
            var bit0 = !packet.ReadBit();
            if (bit34)
                packet.ReadSingle("O");

            packet.ParseBitStream(guid, 4, 6, 0, 5, 2, 7, 3, 1);

            if (bit0)
                packet.ReadInt32("Unk 1");

            if (bit24)
                packet.ReadInt32("Scene Script Package Id");

            if (bit18)
                packet.ReadInt32("Unk 2");

            if (bit1C)
                packet.ReadInt32("Unk 3");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_REQUEST_CEMETERY_LIST_RESPONSE)]
        public static void HandleRequestCemeteryListResponse(Packet packet)
        {
            packet.ReadBit("Is MicroDungeon"); // Named in WorldMapFrame.lua
            var count = packet.ReadBits("Count", 22);

            for (var i = 0; i < count; ++i)
                packet.ReadInt32("Cemetery Id", i);
        }

        [Parser(Opcode.SMSG_UNKNOWN_274)]
        public static void HandleUnknow274(Packet packet)
        {
            byte[][] guid1;
            byte[][] guid2;

            var counter = packet.ReadBits(19);

            guid1 = new byte[counter][];
            guid2 = new byte[counter][];
            for (var i = 0; i < counter; ++i)
            {
                guid1[i] = new byte[8];
                guid2[i] = new byte[8];

                packet.StartBitStream(guid2[i], 6, 3);
                packet.StartBitStream(guid1[i], 6, 1, 2, 3);
                packet.ReadBits(4);
                packet.StartBitStream(guid1[i], 5, 0, 7);
                packet.StartBitStream(guid2[i], 0, 7, 5);
                guid1[i][4] = packet.ReadBit();
                packet.StartBitStream(guid2[i], 1, 4, 2);
            }

            for (var i = 0; i < counter; ++i)
            {
                packet.ReadXORByte(guid1[i], 1);
                packet.ReadInt32("Int3Ch", i);
                packet.ReadXORByte(guid1[i], 2);
                packet.ReadXORByte(guid2[i], 6);
                packet.ReadXORByte(guid1[i], 4);
                packet.ReadXORByte(guid2[i], 7);
                packet.ReadXORByte(guid1[i], 6);
                packet.ReadXORByte(guid1[i], 7);

                packet.ReadInt32("Int40h", i);

                packet.ReadXORByte(guid2[i], 5);
                packet.ReadXORByte(guid1[i], 5);
                packet.ReadXORByte(guid2[i], 2);

                packet.ReadPackedTime("Time", i);
                packet.ReadInt32("Int14h", i);

                packet.ReadXORByte(guid2[i], 0);
                packet.ReadXORByte(guid1[i], 3);
                packet.ReadXORByte(guid2[i], 1);
                packet.ReadXORByte(guid1[i], 0);
                packet.ReadXORByte(guid2[i], 4);
                packet.ReadXORByte(guid2[i], 3);

                packet.WriteGuid("Guid1", guid1[i], i);
                packet.WriteGuid("Guid2", guid2[i], i);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_1024)]
        public static void HandleUnknown1024(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 7, 4, 1, 5, 0, 3, 6);
            packet.ReadBits("bits0", 3);
            guid[2] = packet.ReadBit();
            packet.ParseBitStream(guid, 3, 6, 0, 5, 1, 7, 4, 2);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_6775)]
        public static void HandleUnknown6775(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 6, 5, 7, 2, 4, 0, 1, 3);
            packet.ParseBitStream(guid, 1, 5, 0, 6, 4, 2, 3, 7);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_3)]
        public static void HandleUnknown3(Packet packet)
        {
            var guid = new byte[8];
            packet.StartBitStream(guid, 1, 3, 4, 0, 5, 6, 2, 7);
            packet.ParseBitStream(guid, 1, 4, 0, 6, 3, 7, 5, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1457)]
        public static void HandleUnknown1457(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 5, 2, 3, 6, 7, 1, 0, 4);
            packet.ParseBitStream(guid, 1, 3);
            packet.ReadInt32("int18h");
            packet.ParseBitStream(guid, 7, 0, 5, 4, 2, 6);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5738)]
        public static void HandleUnknown5738(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 3, 0, 5, 4, 2, 7, 1, 6);
            packet.ParseBitStream(guid, 2, 6, 3);
            packet.ReadSingle("Speed");
            packet.ParseBitStream(guid, 0, 4, 1, 5, 7);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_56)]
        public static void HandleUnknown56(Packet packet)
        {
            packet.ReadInt32("Int14");
            packet.ReadInt32("Int10");
        }

        [Parser(Opcode.MSG_UNKNOWN_5125)]
        public static void HandleUnknown5125(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                var guid = new byte[8];

                packet.ReadByte("Unk 1 byte");
                packet.ReadByte("Unk 2 byte");

                packet.StartBitStream(guid, 1, 5, 7, 2, 3, 4, 0, 6);
                packet.ParseBitStream(guid, 2, 3, 7, 0, 6, 5, 1, 4);

                packet.WriteGuid("Guid", guid);
            }
            else
            {
                var guid = new byte[8];

                packet.StartBitStream(guid, 2, 7, 0, 6, 5, 3, 1, 4);
                packet.ParseBitStream(guid, 2, 0, 1, 7, 4, 5);
                packet.ReadInt32("Int18");
                packet.ParseBitStream(guid, 3, 6);

                packet.WriteGuid("GUID", guid);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_57)]
        public static void HandleUnknown57(Packet packet)
        {
            packet.ReadByte("Byte20");
            packet.ReadInt32("Int1C");
            packet.ReadUInt32<SpellId>("Spell ID");

            var bit10 = !packet.ReadBit();
            var bit14 = !packet.ReadBit();

            if (bit10)
                packet.ReadInt32("Int10");
            if (bit14)
                packet.ReadInt32("Int14");
        }

        [Parser(Opcode.SMSG_UNKNOWN_170)]
        public static void HandleUnknown170(Packet packet)
        {
            packet.ReadInt32("Int14");
            packet.ReadBits("bits10", 2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5963)]
        public static void HandleUnknown5963(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 1, 7, 4, 5, 2, 6, 0, 3);
            packet.ParseBitStream(guid, 1, 6, 5, 0);
            packet.ReadSingle("Speed");
            packet.ParseBitStream(guid, 4, 3, 2, 7);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_316)]
        public static void HandleUnknown316(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("Int10"); // sub_7B4A0D
            var len = packet.ReadInt32("Int78");

            packet.ReadBytes(len);

            guid[1] = packet.ReadBit();
            packet.ReadBit("bit20");
            packet.StartBitStream(guid, 7, 2, 6, 3, 4, 5, 0);

            packet.ReadBit("bit21");
            packet.ParseBitStream(guid, 6, 1, 4, 2, 5, 0, 3, 7);

            packet.WriteGuid("Guid3", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2048)]
        public static void HandleUnknown2048(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid1[7] = packet.ReadBit();
            packet.StartBitStream(guid2, 6, 5);
            packet.StartBitStream(guid1, 3, 0, 6);
            guid2[2] = packet.ReadBit();
            var bit18 = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            packet.StartBitStream(guid1, 1, 4);
            packet.StartBitStream(guid2, 4, 7, 3);
            guid1[2] = packet.ReadBit();
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 0);
            packet.ReadSingle("Float2C");
            packet.ReadXORByte(guid2, 6);
            packet.ReadSingle("Float30");
            packet.ReadSingle("Float1C");
            packet.ReadXORByte(guid1, 0);
            packet.ReadInt32("Int24"); // SpellVisualKit?
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 3);
            packet.ReadInt16("Int20");
            packet.ReadXORByte(guid1, 7);
            packet.ReadInt16("Int40");
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 2);
            packet.ReadSingle("Float28");
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 1);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_6418)]
        public static void HandleUnknown6418(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 0, 5, 7, 2, 1, 4, 6, 3);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 7);
            packet.ReadByte("Byte18");
            packet.ReadXORByte(guid, 1);
            packet.ReadByte("Byte19");
            packet.ReadXORByte(guid, 5);

            packet.WriteGuid("Guid2", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_443)]
        public static void HandleUnknown443(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.StartBitStream(guid1, 6, 2);
            packet.StartBitStream(guid2, 5, 6, 7, 4, 3, 2, 1);
            packet.StartBitStream(guid1, 1, 5, 7, 4, 3);
            guid2[0] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 5);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_67)]
        public static void HandleUnknown67(Packet packet)
        {
            packet.ReadInt32("Count?");
        }

        [Parser(Opcode.SMSG_UNKNOWN_442)]
        public static void HandleUnknown442(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 5, 6, 7, 0, 4, 3, 1, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadByte("Byte1D");
            packet.ReadByte("Byte1C");
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 3);
            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadXORByte(guid, 5);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2093)]
        public static void HandleUnknown2093(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 2, 6, 0, 7, 1, 3, 4, 5);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 6);
            packet.ReadSingle("Speed");
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4501)]
        public static void HandleUnknown4501(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var guid3 = new byte[8];

            var bit20 = false;
            var bit24 = false;

            var bit28 = packet.ReadBit();
            if (bit28)
            {
                guid1[0] = packet.ReadBit();
                guid1[1] = packet.ReadBit();
                guid1[6] = packet.ReadBit();
                guid1[4] = packet.ReadBit();
                bit24 = !packet.ReadBit();
                guid1[3] = packet.ReadBit();
                bit20 = !packet.ReadBit();

                packet.ReadBit(); // fake bit

                guid2[2] = packet.ReadBit();
                guid2[3] = packet.ReadBit();
                guid2[4] = packet.ReadBit();
                guid2[6] = packet.ReadBit();
                guid2[7] = packet.ReadBit();
                guid2[5] = packet.ReadBit();
                guid2[1] = packet.ReadBit();
                guid2[0] = packet.ReadBit();
                guid1[7] = packet.ReadBit();
                guid1[5] = packet.ReadBit();
                guid1[2] = packet.ReadBit();
            }

            var bit38 = packet.ReadBit();
            guid3[6] = packet.ReadBit();
            guid3[0] = packet.ReadBit();
            guid3[4] = packet.ReadBit();
            guid3[1] = packet.ReadBit();
            guid3[2] = packet.ReadBit();
            guid3[3] = packet.ReadBit();
            guid3[5] = packet.ReadBit();
            guid3[7] = packet.ReadBit();

            if (bit38)
            {
                packet.ReadInt32("Int30");
                packet.ReadInt32("Int34");
            }

            if (bit28)
            {
                packet.ReadXORByte(guid1, 0);

                if (bit20)
                    packet.ReadInt32("Int20");

                packet.ReadXORByte(guid1, 4);
                packet.ReadXORByte(guid1, 5);
                packet.ReadXORByte(guid2, 7);
                packet.ReadXORByte(guid2, 6);
                packet.ReadXORByte(guid2, 1);
                packet.ReadXORByte(guid2, 4);
                packet.ReadXORByte(guid2, 0);
                packet.ReadXORByte(guid2, 2);
                packet.ReadXORByte(guid2, 5);
                packet.ReadXORByte(guid2, 3);
                packet.ReadXORByte(guid1, 3);

                if (bit24)
                    packet.ReadByte("Byte24");

                packet.ReadXORByte(guid1, 1);
                packet.ReadXORByte(guid1, 7);
                packet.ReadXORByte(guid1, 2);
                packet.ReadXORByte(guid1, 6);

                packet.WriteGuid("Guid2", guid1);
                packet.WriteGuid("Guid3", guid2);
            }

            packet.ReadXORByte(guid3, 5);
            packet.ReadXORByte(guid3, 6);
            packet.ReadXORByte(guid3, 4);
            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadXORByte(guid3, 7);
            packet.ReadXORByte(guid3, 1);
            packet.ReadXORByte(guid3, 3);
            packet.ReadInt32("Duration");
            packet.ReadXORByte(guid3, 2);
            packet.ReadXORByte(guid3, 0);

            packet.WriteGuid("Guid8", guid3);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1194)]
        public static void HandleUnknown1194(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 0, 3, 5, 4, 6, 7, 1, 2);
            packet.ParseBitStream(guid, 4, 6, 7, 0, 2, 5, 1, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1166)]
        public static void HandleUnknown1166(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 2, 1, 7, 0, 5, 3, 4, 6);
            packet.ReadByte("Byte1D");
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.ReadByte("Byte1C");
            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_24)]
        public static void HandleUnknown24(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 3, 4, 2, 0, 5, 6, 1, 7);
            packet.ParseBitStream(guid, 4, 6, 5, 2, 1, 7, 3, 0);
            packet.ReadInt32("Current health");

            packet.WriteGuid("Guid2", guid);
        }

        [Parser(Opcode.MSG_UNKNOWN_4262)]
        public static void HandleUnknown4262(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
                packet.ReadInt32("Int10");
            else
            {
                var guid1 = new byte[8];
                byte[][] guid2;
                byte[][][] guid3 = null;
                byte[][][] guid4 = null;
                byte[][][] guid5 = null;
                byte[][][] guid6 = null;

                packet.StartBitStream(guid1, 6, 5, 7, 3, 1, 4);

                var bits10 = packet.ReadBits(19);

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
                    bits44[i] = packet.ReadBits(22);
                    bit90[i] = packet.ReadBit();
                    bits14[i] = packet.ReadBits(21);

                    guid3[i] = new byte[bits14[i]][];
                    for (var j = 0; j < bits14[i]; ++j)
                    {
                        guid3[i][j] = new byte[8];
                        packet.StartBitStream(guid3[i][j], 7, 2, 6, 4, 1, 3, 5, 0);
                    }

                    bits24[i] = packet.ReadBits(21);

                    guid4[i] = new byte[bits24[i]][];
                    for (var j = 0; j < bits24[i]; ++j)
                    {
                        guid4[i][j] = new byte[8];
                        packet.StartBitStream(guid4[i][j], 4, 2, 1, 7, 6, 5, 3, 0);
                    }

                    if (bit90[i])
                    {
                        guid2[i] = new byte[8];
                        packet.StartBitStream(guid2[i], 0, 5, 6, 2, 1, 4, 3, 7);
                        bits7C[i] = packet.ReadBits(21);
                    }

                    bits34[i] = packet.ReadBits(24);

                    guid5[i] = new byte[bits34[i]][];
                    for (var j = 0; j < bits34[i]; ++j)
                    {
                        guid5[i][j] = new byte[8];
                        packet.StartBitStream(guid5[i][j], 7, 1, 2, 5, 0, 3, 6, 4);
                    }

                    bits54[i] = packet.ReadBits(22);
                    bits4[i] = packet.ReadBits(20);

                    guid6[i] = new byte[bits4[i]][];
                    for (var j = 0; j < bits4[i]; ++j)
                    {
                        guid6[i][j] = new byte[8];
                        packet.StartBitStream(guid6[i][j], 6, 4, 1, 0, 5, 7, 3, 2);
                    }
                }

                packet.StartBitStream(guid1, 2, 0);

                for (var i = 0; i < bits10; ++i)
                {
                    for (var j = 0; j < bits44[i]; ++j)
                        packet.ReadInt32("Int44", i, j);

                    for (var j = 0; j < bits4[i]; ++j)
                    {
                        packet.ReadXORByte(guid6[i][j], 3);
                        packet.ReadXORByte(guid6[i][j], 7);
                        packet.ReadInt32("IntEB", i, j);
                        packet.ReadXORByte(guid6[i][j], 5);
                        packet.ReadXORByte(guid6[i][j], 0);
                        packet.ReadInt32("IntEB", i, j);
                        packet.ReadXORByte(guid6[i][j], 2);
                        packet.ReadSingle("FloatEB", i, j);
                        packet.ReadXORByte(guid6[i][j], 1);
                        packet.ReadXORByte(guid6[i][j], 6);
                        packet.ReadXORByte(guid6[i][j], 4);
                        packet.WriteGuid("Guid6", guid6[i][j], i, j);
                    }

                    for (var j = 0; j < bits14[i]; ++j)
                    {
                        packet.ReadInt32("IntEB");
                        packet.ParseBitStream(guid3[i][j], 7, 2, 1, 6, 4, 0, 3, 5);
                        packet.WriteGuid("Guid3", guid3[i][j], i, j);
                    }

                    for (var j = 0; j < bits24[i]; ++j)
                    {
                        packet.ReadXORByte(guid4[i][j], 7);
                        packet.ReadInt32("IntEB", i, j);
                        packet.ReadXORByte(guid4[i][j], 5);
                        packet.ReadInt32("IntEB", i, j);
                        packet.ReadXORByte(guid4[i][j], 6);
                        packet.ReadXORByte(guid4[i][j], 1);
                        packet.ReadXORByte(guid4[i][j], 2);
                        packet.ReadXORByte(guid4[i][j], 0);
                        packet.ReadXORByte(guid4[i][j], 4);
                        packet.ReadXORByte(guid4[i][j], 3);
                        packet.WriteGuid("Guid4", guid4[i][j], i, j);
                    }

                    if (bit90[i])
                    {
                        packet.ReadInt32("IntEB", i);
                        packet.ReadXORByte(guid2[i], 3);

                        for (var j = 0; j < bits7C[i]; ++j)
                        {
                            packet.ReadInt32("IntEB", i, j);
                            packet.ReadInt32("IntEB", i, j);
                        }

                        packet.ReadXORByte(guid2[i], 7);
                        packet.ReadXORByte(guid2[i], 2);
                        packet.ReadXORByte(guid2[i], 4);
                        packet.ReadXORByte(guid2[i], 6);
                        packet.ReadInt32("IntEB");
                        packet.ReadXORByte(guid2[i], 5);
                        packet.ReadXORByte(guid2[i], 1);
                        packet.ReadInt32("IntEB");
                        packet.ReadXORByte(guid2[i], 0);
                        packet.WriteGuid("Guid2", guid2[i]);
                    }

                    for (var j = 0; j < bits34[i]; ++j)
                    {
                        packet.ParseBitStream(guid5[i][j], 1, 0, 2, 3, 7, 4, 5, 6);
                        packet.WriteGuid("Guid5", guid5[i][j], i, j);
                    }

                    packet.ReadInt32("Int14", i);

                    for (var j = 0; j < bits54[i]; ++j)
                        packet.ReadInt32("IntEB", i, j);
                }

                packet.ReadXORByte(guid1, 1);
                packet.ReadXORByte(guid1, 6);

                packet.ReadUInt32<SpellId>("Spell ID");

                packet.ReadXORByte(guid1, 4);
                packet.ReadXORByte(guid1, 5);
                packet.ReadXORByte(guid1, 2);
                packet.ReadXORByte(guid1, 3);
                packet.ReadXORByte(guid1, 0);
                packet.ReadXORByte(guid1, 7);

                packet.WriteGuid("Guid1", guid1);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_6327)]
        public static void HandleUnknown6327(Packet packet)
        {
            var guid1 = new byte[8];

            var bits1C =  packet.ReadBits(24);

            var guid2 = new byte[bits1C][];
            for (var i = 0; i < bits1C; ++i)
            {
                guid2[i] = new byte[8];
                packet.StartBitStream(guid2[i], 2, 3, 4, 6, 7, 5, 1, 0);
            }

            packet.StartBitStream(guid1, 6, 5, 4, 0, 3, 7, 1, 2);

            for (var i = 0; i < bits1C; ++i)
            {
                packet.ParseBitStream(guid2[i], 5, 0, 4, 1, 3, 2, 7, 6);
                packet.WriteGuid("Guid2", guid2[i]);
            }

            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 5);
            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 4);

            packet.WriteGuid("Guid1", guid1);
        }

        [Parser(Opcode.SMSG_UNKNOWN_8)]
        public static void HandleUnknown8(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[0] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            packet.StartBitStream(guid2, 4, 6);
            packet.ReadBit("bit34");
            packet.StartBitStream(guid2, 1, 5);
            packet.ReadBit("bit48");
            packet.StartBitStream(guid1, 3, 4, 5);
            packet.StartBitStream(guid2, 3, 7);
            packet.StartBitStream(guid1, 7, 2, 0, 1);
            packet.ReadBit("bit24");
            packet.ReadBit("bit40");
            guid2[2] = packet.ReadBit();
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 7);
            packet.ReadInt32("Int38");
            packet.ReadInt32("Int28");
            packet.ReadInt32("Int30");
            packet.ReadInt32("Int4C");
            packet.ReadXORByte(guid2, 3);
            packet.ReadInt32("Int2C");
            packet.ReadInt32("Int3C");
            packet.ReadXORByte(guid1, 7);
            packet.ReadInt32("Int20");
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 6);
            packet.ReadByte("Byte35");
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 4);
            packet.ReadInt32("Int58");
            packet.ReadInt32("Int44");
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 2);
            packet.ReadInt32("Item ID");

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_305)]
        public static void HandleUnknown305(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 2, 3, 0, 5, 6, 4, 7, 1);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 3);
            packet.ReadInt32("Int20");
            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadInt32("Int1C");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);

            packet.WriteGuid("Guid", guid);

        }

        [Parser(Opcode.SMSG_UNKNOWN_1321)]
        public static void HandleUnknown1321(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.StartBitStream(guid1, 1, 3, 7, 0);
            packet.StartBitStream(guid2, 4, 7);
            guid1[2] = packet.ReadBit();
            packet.ReadBit("bit18");
            packet.StartBitStream(guid2, 2, 6);
            packet.StartBitStream(guid1, 4, 5);
            packet.StartBitStream(guid2, 1, 0, 5, 3);
            guid1[6] = packet.ReadBit();
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 6);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_275)]
        public static void HandleUnknown275(Packet packet)
        {
            packet.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_1036)]
        public static void HandleUnknown1036(Packet packet)
        {
            packet.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_429)]
        public static void HandleUnknown429(Packet packet)
        {
            packet.ReadByte("Byte24");
            packet.ReadByte("Byte2C");
            packet.ReadInt32("Int10");

            var bit28 = !packet.ReadBit();
            var bits14 = (int)packet.ReadBits(24);

            if (bit28)
                packet.ReadInt32("Int28");

            for (var i = 0; i < bits14; ++i)
                packet.ReadByte("Byte18", i);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1135)]
        public static void HandleUnknown1135(Packet packet)
        {
            var bits10 = packet.ReadBits(15);

            for (var i = 0; i < bits10; ++i)
            {
                packet.ReadInt32("Int14", i);

                for (var j = 0; j < 300; ++j)
                    packet.ReadByte("ByteEB", i, j);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_2321)]
        public static void HandleUnknown2321(Packet packet)
        {
            packet.ReadInt32("Time?");
        }

        [Parser(Opcode.SMSG_UNKNOWN_1139)]
        public static void HandleUnknown1139(Packet packet)
        {
            packet.ReadInt64("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_2057)]
        public static void HandleUnknown2057(Packet packet)
        {
            packet.ReadInt32("Int10");
        }

        [Parser(Opcode.MSG_MULTIPLE_PACKETS)]
        public static void MultiplePackets(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.WriteLine("ClientToServer: CMSG_ADDON_REGISTERED_PREFIXES");
                var count = packet.ReadBits("Count", 24);
                var lengths = new int[count];
                for (var i = 0; i < count; ++i)
                    lengths[i] = (int)packet.ReadBits(5);

                for (var i = 0; i < count; ++i)
                    packet.ReadWoWString("Addon", lengths[i], i);
            }
            else
            {
                packet.WriteLine("ServerToClient: SMSG_QUEST_GIVER_QUEST_COMPLETE");

                packet.ReadInt32("Reward XP");
                packet.ReadInt32<QuestId>("Quest ID");
                packet.ReadInt32("Money");
                packet.ReadInt32("Int1C");
                packet.ReadInt32("Int24");
                packet.ReadInt32("Int20");
                packet.ReadBit("bit28");
                packet.ReadBit("bit18");
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_4522)]
        public static void HandleUnknown4522(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 6, 5, 0, 1, 2, 4, 3, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 6);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2205)] // Battle Pet?
        public static void HandleUnknown2205(Packet packet)
        {
            var count = 5;

            var bits25 = packet.ReadBit();

            var bits26 = 0u;
            var bitsA7 = new int[count];
            if (bits25) // has custom Name?
            {
                bits26 = packet.ReadBits(8);

                for (var i = 0; i < count; ++i)
                    bitsA7[i] = (int)packet.ReadBits(7);

                packet.ReadBit();
            }

            if (bits25) // has custom Name?
            {
                for (var i = 0; i < count; ++i)
                    packet.ReadWoWString("StringA7", bitsA7[i]);

                packet.ReadWoWString("Name", bits26);
            }

            packet.ReadInt64("Int18");
            packet.ReadInt32("Entry");
            packet.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_2080)]
        public static void HandleUnknown2080(Packet packet)
        {
            var bits18 = 0;
            var bits7FA = 0;

            var bitC00 = packet.ReadBit();
            if (bitC00)
            {
                bits7FA = (int)packet.ReadBits(10);
                bits18 = (int)packet.ReadBits(11);
            }

            if (bitC00)
            {
                packet.ReadInt32("Int7F0");
                packet.ReadWoWString("String18", bits18);
                packet.ReadByte("Byte7E9");
                packet.ReadWoWString("String7FA", bits7FA);
                packet.ReadByte("Byte7F9");
                packet.ReadInt32("Int7F4");
                packet.ReadInt32("IntBFC");
                packet.ReadInt32("Int7EC");
                packet.ReadByte("Byte7F8");
                packet.ReadInt32("Int14");
            }

            packet.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_438)]
        public static void HandleUnknown438(Packet packet)
        {
            packet.ReadSingle("Float10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_2087)]
        public static void HandleUnknown2087(Packet packet)
        {
            for (var i = 0; i < 4; ++i)
            {
                packet.ReadInt32("Unk 1", i);
                packet.ReadInt32("Unk 2", i);
                packet.ReadInt32("Unk 3", i);
                packet.ReadInt32("Unk 4", i);
                packet.ReadInt32("Unk 5", i);
                packet.ReadInt32("Unk 6", i);
                packet.ReadInt32("Unk 7", i);
                packet.ReadInt32("Unk 8", i);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_169)]
        public static void HandleUnknown169(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("Int1C");
            packet.ReadInt32("Int18");
            var len = packet.ReadInt32("Int1");
            packet.ReadBytes(len);
            packet.StartBitStream(guid, 4, 2, 0);
            packet.ReadBits("bits20", 3);
            packet.StartBitStream(guid, 4, 5, 1, 3, 6);
            packet.ParseBitStream(guid, 4, 2, 7, 5, 3, 1, 6, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_3139)] // Send Name and RealmId
        public static void HandleUnknown3139(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadBit("bit48");
            var bits11 = packet.ReadBits(6);
            packet.ReadBit("bit10");

            packet.StartBitStream(guid, 0, 2, 6, 7, 3, 4, 5, 1);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadWoWString("Name", bits11);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 1);
            packet.ReadInt32("Realm Id");
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4374)]
        public static void HandleUnknown4374(Packet packet)
        {
            for (var i = 0; i < packet.ReadBits("Count", 22); ++i)
                packet.ReadInt32("IntED", i);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1317)]
        public static void HandleUnknown1317(Packet packet)
        {
            packet.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_1060)]
        public static void HandleUnknown1060(Packet packet)
        {
            for (var i = 0; i < packet.ReadBits("Count", 21); ++i)
            {
                packet.ReadInt32("Unk 1");
                packet.ReadInt32("Unk 1");
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_2338)]
        public static void HandleUnknown2338(Packet packet) // Item opcode?
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var guid3 = new byte[8];
            var guid4 = new byte[8];

            var bit38 = packet.ReadBit();

            guid4[6] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid4[5] = packet.ReadBit();
            guid2[0] = packet.ReadBit();

            var bits24 = 0u;
            if (bit38)
            {
                packet.StartBitStream(guid1, 1, 4, 5, 2, 6);
                bits24 = packet.ReadBits(21);
                packet.StartBitStream(guid1, 3, 0, 7);
            }

            guid4[4] = packet.ReadBit();
            packet.StartBitStream(guid3, 1, 6, 3, 7);
            packet.StartBitStream(guid2, 7, 1);
            guid3[4] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid4[0] = packet.ReadBit();
            guid3[0] = packet.ReadBit();
            packet.StartBitStream(guid4, 3, 1);
            guid2[3] = packet.ReadBit();
            guid3[5] = packet.ReadBit();
            guid4[7] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid3[2] = packet.ReadBit();
            guid4[2] = packet.ReadBit();
            guid2[2] = packet.ReadBit();

            if (bit38)
            {
                packet.ReadXORByte(guid1, 3);
                packet.ReadXORByte(guid1, 2);
                packet.ReadXORByte(guid1, 6);
                packet.ReadInt32("Int18");

                for (var i = 0; i < bits24; ++i)
                {
                    packet.ReadInt32("IntED", i);
                    packet.ReadInt32("IntED", i);
                }

                packet.ReadXORByte(guid1, 0);
                packet.ReadXORByte(guid1, 4);
                packet.ReadXORByte(guid1, 7);
                packet.ReadXORByte(guid1, 1);
                packet.ReadInt32("Int20");
                packet.ReadXORByte(guid1, 5);
                packet.ReadInt32("Int1C");
                packet.WriteGuid("Guid1", guid1);
            }

            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid3, 5);
            packet.ReadXORByte(guid3, 4);
            packet.ReadInt32("Int68");
            packet.ReadXORByte(guid3, 2);
            packet.ReadXORByte(guid4, 2);
            packet.ReadXORByte(guid3, 1);
            packet.ReadXORByte(guid4, 0);
            packet.ReadXORByte(guid3, 0);
            packet.ReadXORByte(guid3, 7);
            packet.ReadXORByte(guid4, 6);
            packet.ReadInt32("Item Entry");
            packet.ReadXORByte(guid3, 3);
            packet.ReadXORByte(guid4, 7);
            packet.ReadXORByte(guid4, 1);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid4, 4);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid4, 5);
            packet.ReadXORByte(guid4, 3);
            packet.ReadXORByte(guid2, 7);
            packet.ReadInt32("Int48");
            packet.ReadXORByte(guid3, 6);
            packet.ReadXORByte(guid2, 3);

            packet.WriteGuid("Guid2", guid2);
            packet.WriteGuid("Item GUID", guid3);
            packet.WriteGuid("Guid4", guid4);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5553)]
        public static void HandleUnknown5553(Packet packet) // Item opcode?
        {
            var guid = new byte[8];
            var powerGUID = new byte[8];

            packet.StartBitStream(guid, 6, 1, 5, 0, 3, 7, 2);
            var hasPowerData = packet.ReadBit();
            guid[4] = packet.ReadBit();

            if (hasPowerData)
            {
                packet.StartBitStream(powerGUID, 6, 5, 2, 1, 0, 7);
                var powerCount = packet.ReadBits(21);
                packet.StartBitStream(powerGUID, 4, 3);

                packet.ReadXORByte(powerGUID, 1);
                packet.ReadInt32("Attack power");
                packet.ReadXORByte(powerGUID, 2);
                packet.ReadInt32("Spell power");
                packet.ReadXORByte(powerGUID, 5);

                for (var i = 0; i < powerCount; ++i)
                {
                    packet.ReadInt32E<PowerType>("Power type", i); // Actually powertype for class
                    packet.ReadInt32("Value", i);
                }

                packet.ReadXORByte(powerGUID, 7);
                packet.ReadXORByte(powerGUID, 6);
                packet.ReadXORByte(powerGUID, 3);
                packet.ReadXORByte(powerGUID, 4);
                packet.ReadXORByte(powerGUID, 0);
                packet.ReadInt32("Current health");
                packet.WriteGuid("Power GUID", powerGUID);
            }

            packet.ReadInt32("Int1C");
            packet.ReadInt32("Int24");
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 1);
            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 6);
            packet.ReadByte("Byte20");
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1444)]
        public static void HandleUnknown1444(Packet packet) // Item opcode?
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 6, 5, 2, 4, 0, 7, 3);
            packet.ReadBit("bit10");
            guid[1] = packet.ReadBit();
            packet.ParseBitStream(guid, 5, 2, 6, 3, 1, 0, 4, 7);
            packet.ReadUInt32<SpellId>("Spell ID");

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1464)]
        public static void HandleUnknown1464(Packet packet) // Item opcode?
        {
            var powerGUID = new byte[8];
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[2] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            packet.StartBitStream(guid2, 7, 0, 5);
            packet.StartBitStream(guid1, 7, 4);
            packet.StartBitStream(guid2, 3, 1, 4);
            guid1[0] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            var hasPowerData = packet.ReadBit();
            packet.StartBitStream(guid1, 1, 5, 2);

            var powerCount = 0u;
            if (hasPowerData)
            {
                packet.StartBitStream(powerGUID, 6, 0, 4, 3, 7, 2, 1, 5);
                powerCount = packet.ReadBits(21);
            }

            guid1[3] = packet.ReadBit();

            if (hasPowerData)
            {
                packet.ReadXORByte(powerGUID, 3);
                packet.ReadInt32("Current health");
                packet.ReadXORByte(powerGUID, 6);
                packet.ReadXORByte(powerGUID, 7);
                packet.ReadXORByte(powerGUID, 1);
                packet.ReadXORByte(powerGUID, 5);
                packet.ReadInt32("Attack power");
                packet.ReadXORByte(powerGUID, 4);
                packet.ReadXORByte(powerGUID, 0);

                for (var i = 0; i < powerCount; ++i)
                {
                    packet.ReadInt32E<PowerType>("Power type", i); // Actually powertype for class
                    packet.ReadInt32("Value", i);
                }

                packet.ReadInt32("Spell power");
                packet.ReadXORByte(powerGUID, 2);

                packet.WriteGuid("Power GUID", powerGUID);
            }

            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 1);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4518)]
        public static void HandleUnknown4518(Packet packet) // Item opcode?
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[2] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            var bit28 = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            packet.StartBitStream(guid1, 4, 0);
            guid2[3] = packet.ReadBit();
            packet.StartBitStream(guid1, 2, 3);
            packet.StartBitStream(guid2, 5, 7);
            guid1[6] = packet.ReadBit();
            packet.StartBitStream(guid2, 0, 6);
            guid1[5] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 6);
            if (bit28)
            {
                packet.ReadInt32("Int30");
                packet.ReadInt32("Int2C");
            }

            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadInt32("Int24");
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 7);
            packet.ReadInt32("Int20");
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 6);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1454)]
        public static void HandleUnknown1454(Packet packet)
        {
            packet.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_5525)]
        public static void HandleUnknown5525(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 4, 6, 3, 7, 2, 5, 0, 1);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid, 3);

            packet.WriteGuid("Guid2", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2235)]
        public static void HandleUnknown2235(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt16("Int18");

            packet.StartBitStream(guid, 7, 2, 0, 1, 6, 3, 4, 5);
            packet.ParseBitStream(guid, 7, 0, 4, 5, 6, 2, 3, 1);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1182)] // GameObject opcode
        public static void HandleUnknown1182(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 4, 6, 1, 3, 2, 5, 0, 7);
            packet.ParseBitStream(guid, 5, 4, 2, 3, 7, 0, 1, 6);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_147)]
        public static void HandleUnknown147(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 1, 6, 2, 3, 5, 7, 4, 0);
            packet.ParseBitStream(guid, 2, 4, 6, 1, 5, 3, 0, 7);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4822)]
        public static void HandleUnknown4822(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 6, 5, 0, 1, 2, 4, 3, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 3);
            packet.ReadSingle("Float1C");
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadInt32("Int18");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2327)]
        public static void HandleUnknown2327(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 3, 2, 7, 0, 4, 6, 1, 5);
            packet.ParseBitStream(guid, 0, 5, 2, 6, 7, 4, 1, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_62)]
        public static void HandleUnknown62(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 6, 4, 3, 5, 1, 7, 0, 2);
            packet.ParseBitStream(guid, 1, 7, 5, 4, 6, 2, 0, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2178)]
        public static void HandleUnknown2178(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 0, 7, 5, 2);
            packet.ReadBit("bit10");
            packet.StartBitStream(guid, 3, 6, 4, 1);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);
            packet.ReadInt32("Int20");  // SpellId?
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 1);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_6454)]
        public static void HandleUnknown6454(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.ReadByte("Byte10");
            packet.ReadByte("Byte38");
            packet.ReadByte("Byte26");
            packet.ReadByte("Byte41");
            packet.ReadByte("Byte40");
            packet.ReadByte("Byte25");
            packet.ReadInt32("Int3C");
            packet.ReadByte("Byte24");
            packet.ReadByte("Byte42");

            guid2[7] = packet.ReadBit();
            packet.StartBitStream(guid1, 3, 1, 5, 7);
            guid2[6] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            var bits14 = packet.ReadBits(22);
            packet.StartBitStream(guid2, 5, 1, 4);
            packet.StartBitStream(guid1, 2, 0);
            packet.StartBitStream(guid2, 0, 2);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 6);

            for (var i = 0; i < bits14; ++i)
                packet.ReadInt32("IntED", i);

            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 7);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4763)]
        public static void HandleUnknown4763(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 6, 1, 5, 4, 7, 2, 3, 0);
            packet.ParseBitStream(guid, 0, 5, 3, 1, 7, 2, 6, 4);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1162)]
        public static void HandleUnknown1162(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 6, 3, 0, 1, 5, 7, 4, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Guid2", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_389)]
        public static void HandleUnknown389(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 5, 1, 4, 2, 3, 0, 6, 7);
            packet.ParseBitStream(guid, 5, 2, 0, 6, 3, 1, 4, 7);
            packet.ReadUInt32<SpellId>("Spell ID");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1189)]
        public static void HandleUnknown1189(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 1, 4, 3, 6, 0, 5, 7, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4710)]
        public static void HandleUnknown4710(Packet packet)
        {
            var guid = new byte[8];

            var bits24 = (int)packet.ReadBits(2);
            packet.StartBitStream(guid, 5, 1, 7, 0);
            var bit28 = !packet.ReadBit();
            packet.StartBitStream(guid, 2, 4, 3, 6);
            packet.ReadXORByte(guid, 7);

            if (bit28)
                packet.ReadInt32("Int28");

            packet.ReadXORByte(guid, 1);
            packet.ReadSingle("Float1C");
            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 2);
            packet.ReadSingle("Float20");
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 4);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1059)]
        public static void HandleUnknown1059(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 2, 7, 6, 0, 5, 3, 4, 1);
            packet.ParseBitStream(guid, 3, 7, 1, 0, 4, 2, 6, 5);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_163)]
        public static void HandleUnknown163(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.StartBitStream(guid2, 3, 2, 1, 0);
            guid1[1] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            packet.StartBitStream(guid1, 5, 2, 0);
            guid2[4] = packet.ReadBit();
            packet.StartBitStream(guid1, 6, 3);
            guid2[5] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 1);
            packet.ReadInt32("Int20");
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 3);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1347)]
        public static void HandleUnknown1347(Packet packet)
        {
            packet.ReadInt64("Time?");
        }

        [Parser(Opcode.SMSG_UNKNOWN_1197)] // Energize opcode
        public static void HandleUnknown1197(Packet packet)
        {
            var powerGUID = new byte[8];

            packet.StartBitStream(powerGUID, 7, 2, 4, 3, 6, 1);
            var powerCount = (int)packet.ReadBits(21);
            packet.StartBitStream(powerGUID, 0, 5);

            for (var i = 0; i < powerCount; ++i)
            {
                packet.ReadInt32("Value", i);
                packet.ReadByteE<PowerType>("Power type", i);
            }

            packet.ParseBitStream(powerGUID, 4, 2, 3, 7, 0, 6, 1, 5);

            packet.WriteGuid("Power GUID", powerGUID);
        }

        [Parser(Opcode.SMSG_UNKNOWN_6285)]
        public static void HandleUnknown6285(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);

            var guid = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                guid[i] = new byte[8];
                packet.StartBitStream(guid[i], 6, 3, 4, 2, 5, 1, 7, 0);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guid[i], 7);
                packet.ReadXORByte(guid[i], 1);
                packet.ReadXORByte(guid[i], 2);
                packet.ReadInt32("Int14"); // flags?
                packet.ReadXORByte(guid[i], 3);
                packet.ReadXORByte(guid[i], 5);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadXORByte(guid[i], 0);
                packet.ReadXORByte(guid[i], 6);

                packet.WriteGuid("Guid", guid[i]);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_1358)]
        public static void HandleUnknown1358(Packet packet)
        {
            var bits10 = packet.ReadBits(19);

            var bits38 = new uint[bits10];
            var guid1 = new byte[bits10][];
            var guid2 = new byte[bits10][][];

            for (var i = 0; i < bits10; ++i)
            {
                bits38[i] = packet.ReadBits(24);

                guid1[i] = new byte[8];
                guid2[i] = new byte[bits38[i]][];

                for (var j = 0; j < bits38[i]; ++j)
                {
                    guid2[i][j] = new byte[8];
                    packet.StartBitStream(guid2[i][j], 5, 2, 6, 0, 1, 4, 3, 7);
                }

                packet.StartBitStream(guid1[i], 2, 0, 4, 1, 7, 3, 5, 6);
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.ReadXORByte(guid1[i], 2);

                for (var j = 0; j < bits38[i]; ++j)
                {
                    packet.ParseBitStream(guid2[i][j], 4, 0, 1, 5, 3, 7, 2, 6);
                    packet.WriteGuid("Guid2", guid2[i][j], i, j);
                }

                packet.ReadInt32("IntED", i);
                packet.ReadPackedTime("Date");
                packet.ReadXORByte(guid1[i], 1);
                packet.ReadXORByte(guid1[i], 4);
                packet.ReadXORByte(guid1[i], 6);
                packet.ReadInt32("IntED", i);
                packet.ReadXORByte(guid1[i], 3);
                packet.ReadXORByte(guid1[i], 7);
                packet.ReadInt32("Item Id", i);
                packet.ReadXORByte(guid1[i], 5);
                packet.ReadInt32("Int14", i);
                packet.ReadInt32("IntED", i);
                packet.ReadXORByte(guid1[i], 0);

                packet.WriteGuid("Guid1", guid1[i], i);
            }
        }

        [Parser(Opcode.SMSG_VIGNETTE_UPDATE)]
        public static void HandleUnknown177(Packet packet)
        {

            var bits10 = packet.ReadBits(24);

            var guid1 = new byte[bits10][];

            for (var i = 0; i < bits10; ++i)
            {
                guid1[i] = new byte[8];
                packet.StartBitStream(guid1[i], 4, 1, 0, 5, 7, 2, 3, 6);
            }

            var bits34 = packet.ReadBits(20);

            var guid2 = new byte[bits34][];

            for (var i = 0; i < bits34; ++i)
            {
                guid2[i] = new byte[8];
                packet.StartBitStream(guid2[i], 7, 4, 1, 5, 6, 2, 3, 0);
            }

            var bits44 = packet.ReadBits(24);

            var guid3 = new byte[bits44][];

            for (var i = 0; i < bits44; ++i)
            {
                guid3[i] = new byte[8];
                packet.StartBitStream(guid3[i], 3, 4, 6, 7, 1, 0, 2, 5);
            }

            var bits24 = packet.ReadBits(24);

            var guid4 = new byte[bits24][];

            for (var i = 0; i < bits24; ++i)
            {
                guid4[i] = new byte[8];
                packet.StartBitStream(guid4[i], 0, 1, 6, 4, 5, 3, 2, 7);
            }

            var bit20 = packet.ReadBit();
            var bits54 = packet.ReadBits(20);

            var guid5 = new byte[bits54][];

            for (var i = 0; i < bits54; ++i)
            {
                guid5[i] = new byte[8];
                packet.StartBitStream(guid5[i], 3, 2, 7, 5, 6, 0, 1, 4);
            }

            for (var i = 0; i < bits54; ++i)
            {
                packet.ReadXORByte(guid5[i], 1);
                packet.ReadXORByte(guid5[i], 2);
                packet.ReadXORByte(guid5[i], 3);
                packet.ReadXORByte(guid5[i], 4);
                packet.ReadXORByte(guid5[i], 0);
                packet.ReadXORByte(guid5[i], 5);
                packet.ReadInt32("Vignette Id");
                packet.ReadXORByte(guid5[i], 7);
                packet.ReadSingle("Z");
                packet.ReadSingle("Y");
                packet.ReadXORByte(guid5[i], 6);
                packet.ReadSingle("X");

                packet.WriteGuid("Guid5", guid5[i]);
            }

            for (var i = 0; i < bits34; ++i)
            {
                packet.ReadSingle("Y");
                packet.ReadInt32("Vignette Id");
                packet.ReadXORByte(guid2[i], 0);
                packet.ReadSingle("Z");
                packet.ReadXORByte(guid2[i], 3);
                packet.ReadSingle("X");
                packet.ReadXORByte(guid2[i], 4);
                packet.ReadXORByte(guid2[i], 7);
                packet.ReadXORByte(guid2[i], 6);
                packet.ReadXORByte(guid2[i], 2);
                packet.ReadXORByte(guid2[i], 1);
                packet.ReadXORByte(guid2[i], 5);

                packet.WriteGuid("Guid2", guid2[i]);
            }

            for (var i = 0; i < bits24; ++i)
            {
                packet.ParseBitStream(guid4[i], 5, 1, 3, 4, 2, 7, 0, 6);
                packet.WriteGuid("Guid4", guid4[i]);
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.ParseBitStream(guid1[i], 0, 1, 4, 7, 5, 6, 2, 3);
                packet.WriteGuid("Guid1", guid1[i]);
            }

            for (var i = 0; i < bits44; ++i)
            {
                packet.ParseBitStream(guid3[i], 2, 4, 3, 0, 6, 7, 1, 5);
                packet.WriteGuid("Guid1", guid3[i]);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_1318)]
        public static void HandleUnknown1318(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid1[7] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            packet.ReadBit("bit4C");
            packet.StartBitStream(guid2, 2, 5);
            packet.StartBitStream(guid1, 6, 3);
            var bit41 = !packet.ReadBit();
            guid2[0] = packet.ReadBit();
            packet.StartBitStream(guid1, 5, 2);
            packet.ReadBits("bits48", 3);
            guid2[4] = packet.ReadBit();
            packet.ReadBits("bits44", 2);
            guid2[3] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            packet.ReadBit("bit28");
            guid2[7] = packet.ReadBit();
            var bit40 = !packet.ReadBit();
            guid1[1] = packet.ReadBit();
            packet.ReadInt32("Item Display Id");
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 2);
            packet.ReadInt32("Int38");
            packet.ReadInt32("Item Id");
            packet.ReadInt32("Int1C");
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 4);
            packet.ReadByte("Byte18");
            packet.ReadInt32("Int3C");
            if (bit41)
                packet.ReadByte("Byte41");
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 1);
            var len = packet.ReadInt32("Int0");
            packet.ReadBytes(len);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 1);
            packet.ReadInt32("Int34");
            if (bit40)
                packet.ReadByte("Byte40");

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5727)]
        public static void HandleUnknown5727(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 7, 1, 5, 6, 0, 4, 2, 3);
            packet.ParseBitStream(guid, 0, 5, 3, 4, 7, 1, 6, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1119)]
        public static void HandleUnknown1119(Packet packet)
        {
            var bits10 = packet.ReadBits(17);

            var guid = new byte[bits10][];
            var bits30 = new uint[bits10];
            var bit16E = new bool[bits10];
            var bits0 = new uint[bits10];
            var bitsA2 = new uint[bits10];
            var bit16F = new bool[bits10];

            for (var i = 0; i < bits10; ++i)
            {
                guid[i] = new byte[8];
                bits30[i] = packet.ReadBits(6);
                packet.StartBitStream(guid[i], 4, 0, 2, 3);
                bit16E[i] = packet.ReadBit();
                guid[i][7] = packet.ReadBit();
                bits0[i] = packet.ReadBits(8);
                guid[i][1] = packet.ReadBit();
                bitsA2[i] = packet.ReadBits(8);
                packet.StartBitStream(guid[i], 6, 5);
                bit16F[i] = packet.ReadBit();
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.ReadByte("Byte14", i);
                packet.ReadInt32("Realm Id", i);
                packet.ReadXORByte(guid[i], 7);
                packet.ReadInt32<AreaId>("Area Id");
                packet.ReadXORByte(guid[i], 0);
                packet.ReadInt32("Int14", i);
                packet.ReadSingle("Float14", i);

                for (var j = 0; j < 2; ++j) // skill?
                {
                    packet.ReadInt32("Int14", i, j);
                    packet.ReadInt32("Int14", i, j);
                    packet.ReadInt32("Int14", i, j);
                }

                packet.ReadXORByte(guid[i], 3);
                packet.ReadWoWString("Name", bits30[i], i);
                packet.ReadXORByte(guid[i], 1);
                packet.ReadWoWString("Note?", bitsA2[i], i);
                packet.ReadByteE<Class>("Class");
                packet.ReadXORByte(guid[i], 4);
                packet.ReadInt64("Int8", i);
                packet.ReadByte("Byte14", i);
                packet.ReadInt32("Int14", i);
                packet.ReadInt32("Int14", i);
                packet.ReadXORByte(guid[i], 2);
                packet.ReadXORByte(guid[i], 6);
                packet.ReadXORByte(guid[i], 5);
                packet.ReadWoWString("String14", bits0[i], i);
                packet.ReadInt64("IntED", i);
                packet.ReadInt32<ZoneId>("Zone Id");
                packet.ReadByte("Level", i);
                packet.WriteGuid("Guid", guid[i], i);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_140)]
        public static void HandleUnknown140(Packet packet)
        {
            packet.ReadInt32("Int14");
            packet.ReadInt32("Unix Time");
        }

        [Parser(Opcode.SMSG_UNKNOWN_141)]
        public static void HandleUnknown141(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("Int38");
            packet.ReadInt32("Unix Time");
            packet.ReadInt32("Int30");
            packet.ReadInt32("Int20");
            packet.ReadInt32("Int24");
            packet.ReadInt32("Int10");
            for (var i = 0; i < 3; ++i)
            {
                packet.ReadInt32("Unk 1", i);
                packet.ReadByte("Byte48", i);
            }
            packet.ReadInt32("Int34");
            packet.StartBitStream(guid, 7, 6, 5, 2, 3, 0, 4, 1);
            packet.ParseBitStream(guid, 4, 7, 2, 3, 1, 6, 5, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2066)]
        public static void HandleUnknown2066(Packet packet)
        {
            packet.ReadInt32("Int14");
            packet.ReadInt32("Int10");
            packet.ReadBit("bit18");
        }

        [Parser(Opcode.SMSG_UNKNOWN_23)]
        public static void HandleUnknown23(Packet packet)
        {
            var guid1 = new byte[8];

            packet.StartBitStream(guid1, 4, 5, 0, 2);

            var bits2C = (int)packet.ReadBits(22);

            var guid2 = new byte[bits2C][];
            var bits8 = new uint[bits2C];

            for (var i = 0; i < bits2C; ++i)
            {
                guid2[i] = new byte[8];
                packet.StartBitStream(guid2[i], 4, 5, 6, 0);
                bits8[i] = packet.ReadBits(20);
                packet.StartBitStream(guid2[i], 7, 3, 2, 1);
            }

            packet.StartBitStream(guid1, 1, 3, 6, 7);

            for (var i = 0; i < bits2C; ++i)
            {

                for (var j = 0; j < bits8[i]; ++j)
                {
                    packet.ReadInt32("Int30", i);
                    packet.ReadInt32("Int0", i);
                    packet.ReadInt32("Int30", i);
                    packet.ReadInt32("Int30", i);
                }

                packet.ParseBitStream(guid2[i], 0, 1, 2, 5, 3, 6, 4, 7);

                packet.WriteGuid("Guid2", guid2[i], i);
            }

            packet.ReadByte("Byte3C");
            packet.ReadByte("Byte28");
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 5);
            packet.ReadInt32("Int1C");
            packet.ReadInt32("Int20");
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 3);
            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 6);

            packet.WriteGuid("Guid1", guid1);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2055)]
        public static void HandleUnknown2055(Packet packet)
        {
            byte[][] guid1;
            var guid2 = new byte[8];

            for (var i = 0; i < 3; ++i)
                packet.ReadByte("Byte15B", i);

            packet.ReadInt32("Int14C");
            packet.ReadInt32("Int128");
            packet.ReadByte("Byte12");
            packet.ReadByte("Byte11");
            packet.ReadInt32("Int148");
            packet.ReadInt32("Unix Time");

            guid2[5] = packet.ReadBit();
            var bits10 = packet.ReadBits(8);
            packet.ReadBit("bit15");
            packet.ReadBit("bit15A");
            packet.ReadBit("bit158");
            guid2[3] = packet.ReadBit();
            var bits14 = packet.ReadBits(24);
            guid2[4] = packet.ReadBit();

            guid1 = new byte[bits14][];

            for (var i = 0; i < bits14; ++i)
            {
                guid1[i] = new byte[8];
                packet.StartBitStream(guid1[i], 0, 5, 4, 1, 6, 3, 7, 2);
            }

            guid2[7] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            packet.ReadBit("bit24");
            var bits12C = packet.ReadBits(22);
            packet.ReadBit("bit159");

            for (var i = 0; i < bits14; ++i)
            {
                packet.ParseBitStream(guid1[i], 3, 0, 7, 4, 2, 6, 1, 5);
                packet.WriteGuid("Guid1", guid1[i]);
            }

            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 3);

            for (var i = 0; i < bits12C; ++i)
                packet.ReadInt32("IntEA", i);

            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 2);
            if (bits10 > 0)
                packet.ReadWoWString("String25", bits10);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 4);

            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2202)]
        public static void HandleUnknown2202(Packet packet) // Ticket stuff? browser? mhm
        {
            packet.ReadInt32("Unix Time");
            packet.ReadInt32("Int10");

            var bits18 = packet.ReadBits(20);

            var guid = new byte[bits18][];
            var bit410 = new bool[bits18];
            var bitsC = new uint[bits18];
            var bit820 = new bool[bits18];
            var bits420 = new uint[bits18];

            for (var i = 0; i < bits18; ++i)
            {
                guid[i] = new byte[8];
                bit410[i] = !packet.ReadBit();
                bitsC[i] = packet.ReadBits(11);
                packet.ReadBit(); // fake bit
                packet.StartBitStream(guid[i], 7, 6, 1, 2, 5, 3, 0, 4);
                bit820[i] = !packet.ReadBit();
                bits420[i] = packet.ReadBits(10);
            }

            for (var i = 0; i < bits18; ++i)
            {
                packet.ParseBitStream(guid[i], 6, 0, 7, 3, 5, 1, 4, 2);

                if (bit410[i])
                    packet.ReadInt32("Int1C", i);

                packet.ReadWoWString("Status Text", bits420[i], i);

                if (bit820[i])
                    packet.ReadInt32("Int1C", i);

                packet.ReadInt32("Ticket Id", i);
                packet.ReadInt32("Int1C", i);
                packet.ReadInt32("Int1C", i);
                packet.ReadWoWString("URL", bitsC[i], i);

                packet.WriteGuid("GUID", guid[i], i);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_2305)] // Guild opcode?
        public static void HandleUnknown2305(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.StartBitStream(guid2, 7, 3);
            var bit28 = packet.ReadBit();
            if (bit28)
                packet.StartBitStream(guid1, 6, 7, 4, 5, 2, 3, 1, 0);

            var bits44 = packet.ReadBits(23);
            var bits60 = packet.ReadBits(23);
            packet.StartBitStream(guid2, 5, 2, 6);
            var bits34 = packet.ReadBits(20);

            var guid3 = new byte[bits34][];
            var bit14 = new bool[bits34];
            var bitE = new bool[bits34];
            var bits1C = new uint[bits34];
            var bit2C = new bool[bits34];

            for (var i = 0; i < bits34; ++i)
            {
                guid3[i] = new byte[8];
                guid3[i][0] = packet.ReadBit();
                bit14[i] = packet.ReadBit();
                packet.StartBitStream(guid3[i], 6, 7);
                bitE[i] = packet.ReadBit();
                packet.StartBitStream(guid3[i], 3, 2, 1);
                bits1C[i] = packet.ReadBits(21);
                bit2C[i] = packet.ReadBit();
                packet.StartBitStream(guid3[i], 5, 4);
            }

            packet.StartBitStream(guid2, 4, 1, 0);

            for (var i = 0; i < bits34; ++i)
            {
                if (bitE[i])
                    packet.ReadInt16("IntE", i);

                packet.ReadXORByte(guid3[i], 3);
                packet.ReadXORByte(guid3[i], 4);

                for (var j = 0; j < bits1C[i]; ++j)
                {
                    packet.ReadByte("ByteED", i, j);
                    packet.ReadInt32("IntED", i, j);
                }

                if (bit14[i])
                    packet.ReadInt32("Int14", i);

                packet.ReadXORByte(guid3[i], 0);
                packet.ReadXORByte(guid3[i], 2);
                packet.ReadXORByte(guid3[i], 6);

                var len = packet.ReadInt32("Int30", i);

                packet.ReadBytes(len);

                packet.ReadXORByte(guid3[i], 1);
                packet.ReadXORByte(guid3[i], 7);
                packet.ReadXORByte(guid3[i], 5);

                packet.ReadByte("ByteED", i);
                packet.ReadInt32("IntED", i);

                packet.WriteGuid("Guid3", guid3[i], i);
            }

            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 0);

            packet.ReadInt32("Int30");

            if (bit28)
            {
                packet.ReadInt32("Int20");
                packet.ReadXORByte(guid1, 1);
                packet.ReadXORByte(guid1, 3);
                packet.ReadInt32("Int24");
                packet.ReadXORByte(guid1, 6);
                packet.ReadXORByte(guid1, 2);
                packet.ReadXORByte(guid1, 5);
                packet.ReadXORByte(guid1, 4);
                packet.ReadXORByte(guid1, 0);
                packet.ReadXORByte(guid1, 7);
                packet.ReadInt64("Int18");
                packet.WriteGuid("Guid1", guid1);
            }

            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 3);

            for (var i = 0; i < bits44; ++i)
                packet.ReadInt16("Int44", i);

            for (var i = 0; i < bits60; ++i)
            packet.ReadInt16("Int66", i);

            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_19)]
        public static void HandleUnknown19(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 7, 2, 5, 1, 3, 0, 6, 4);
            packet.ParseBitStream(guid, 2, 5, 1, 0, 7, 3, 4, 6);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1394)]
        public static void HandleUnknown1394(Packet packet)
        {
            packet.ReadInt64("Int18");
            packet.ReadInt64("Int28");
            packet.ReadInt64("Int20");
            packet.ReadInt64("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_1203)] // Instance stuff?
        public static void HandleUnknown1203(Packet packet)
        {
            var bits10 = packet.ReadBits(20);

            var guid = new byte[bits10][];

            for (var i = 0; i < bits10; ++i)
            {
                guid[i] = new byte[8];
                packet.StartBitStream(guid[i], 0, 7, 5);
                packet.ReadBit();
                packet.StartBitStream(guid[i], 2, 1);
                packet.ReadBit();
                packet.StartBitStream(guid[i], 3, 4, 6);
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.ReadInt32("IntED", i);
                packet.ReadXORByte(guid[i], 5);
                packet.ReadInt32("IntED", i);
                packet.ReadXORByte(guid[i], 1);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadXORByte(guid[i], 6);
                packet.ReadXORByte(guid[i], 3);
                packet.ReadInt32("Int14", i);
                packet.ReadXORByte(guid[i], 2);
                packet.ReadXORByte(guid[i], 0);
                packet.ReadInt32("IntED", i);
                packet.ReadXORByte(guid[i], 7);
                packet.WriteGuid("Guid", guid[i], i);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_152)]
        public static void HandleUnknown152(Packet packet)
        {
            var count = packet.ReadBits("Count", 6);

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("IntEB", i);
                packet.ReadInt32("IntEB", i);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_6528)]
        public static void HandleUnknown6528(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var powerGUID = new byte[8];

            guid1[5] = packet.ReadBit();
            packet.StartBitStream(guid2, 5, 4);
            guid1[2] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            var hasPowerData = packet.ReadBit();
            guid2[6] = packet.ReadBit();

            var powerCount = 0u;
            if (hasPowerData)
            {
                packet.StartBitStream(powerGUID, 1, 6, 3);
                powerCount = packet.ReadBits(21);
                packet.StartBitStream(powerGUID, 2, 0, 7, 4, 5);
            }

            packet.StartBitStream(guid1, 4, 1);
            guid2[2] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid2[7] = packet.ReadBit();

            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 3);

            if (hasPowerData)
            {
                packet.ReadXORByte(powerGUID, 5);
                packet.ReadInt32("Current health");
                packet.ReadXORByte(powerGUID, 1);
                packet.ReadXORByte(powerGUID, 7);
                packet.ReadInt32("Attack power");
                packet.ReadXORByte(powerGUID, 6);
                packet.ReadInt32("Spell power");
                packet.ReadXORByte(powerGUID, 4);
                packet.ReadXORByte(powerGUID, 0);

                for (var i = 0; i < powerCount; ++i)
                {
                    packet.ReadInt32E<PowerType>("Power type", i); // Actually powertype for class
                    packet.ReadInt32("Value", i);
                }

                packet.ReadXORByte(powerGUID, 2);
                packet.ReadXORByte(powerGUID, 3);

                packet.WriteGuid("Power GUID", powerGUID);
            }

            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 5);
            packet.ReadInt32("Int1C"); // spellId?
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid2, 2);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_54)]
        public static void HandleUnknown54(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 6, 2, 5, 4, 7, 0, 1, 3);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadByte("Byte18");
            packet.ReadXORByte(guid, 6);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5298)]
        public static void HandleUnknown5298(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var powerGUID = new byte[8];

            guid2[3] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            packet.StartBitStream(guid2, 1, 2);
            packet.StartBitStream(guid1, 2, 4);
            packet.StartBitStream(guid2, 7, 0);
            packet.StartBitStream(guid1, 7, 6);
            guid2[4] = packet.ReadBit();
            packet.StartBitStream(guid1, 5, 0);
            packet.StartBitStream(guid2, 6, 5);

            var hasPowerData = packet.ReadBit();
            var powerCount = 0u;
            if (hasPowerData)
            {
                packet.StartBitStream(powerGUID, 3, 4, 0, 7);
                powerCount = packet.ReadBits(21);
                packet.StartBitStream(powerGUID, 2, 5, 1, 6);
            }

            guid1[1] = packet.ReadBit();
            if (hasPowerData)
            {
                packet.ReadXORByte(powerGUID, 5);

                for (var i = 0; i < powerCount; ++i)
                {
                    packet.ReadInt32("Value", i);
                    packet.ReadInt32E<PowerType>("Power type", i); // Actually powertype for class
                }

                packet.ReadInt32("Current health");
                packet.ReadXORByte(powerGUID, 7);
                packet.ReadInt32("Spell power");
                packet.ReadXORByte(powerGUID, 1);
                packet.ReadXORByte(powerGUID, 0);
                packet.ReadInt32("Attack power");
                packet.ReadXORByte(powerGUID, 2);
                packet.ReadXORByte(powerGUID, 3);
                packet.ReadXORByte(powerGUID, 4);
                packet.ReadXORByte(powerGUID, 6);
                packet.WriteGuid("Power GUID", powerGUID);
            }

            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 1);
            packet.ReadInt32("Int48"); // SpellId?
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 6);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_410)] // SMSG_GAME_OBJECT_CUSTOM_ANIM???
        public static void HandleUnknown410(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadBit("bit14");
            packet.StartBitStream(guid, 6, 4, 0, 2);
            var bit10 = !packet.ReadBit();
            packet.StartBitStream(guid, 5, 7, 3, 1);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 7);

            if (bit10)
                packet.ReadInt32("Int10"); // Anim?

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1183)]
        public static void HandleUnknown1183(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[1] = packet.ReadBit();
            var bit15 = packet.ReadBit();
            packet.StartBitStream(guid1, 0, 1);
            guid2[4] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            packet.StartBitStream(guid1, 3, 5);
            var bits40 = packet.ReadBits(21);
            guid1[4] = packet.ReadBit();
            var bit14 = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            for (var i = 0; i < bits40; ++i)
            {
                packet.ReadBit("bit5", i);
                packet.ReadBit("bit7", i);
                packet.ReadBit("bit4", i);
                packet.ReadBit("bit8", i);
                packet.ReadBit("bit6", i);
            }

            packet.StartBitStream(guid2, 5, 7, 3);
            packet.StartBitStream(guid1, 2, 6);
            guid2[0] = packet.ReadBit();
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 5);
            packet.ReadInt32("Int54");
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 7);
            packet.ReadByte("Byte50");
            packet.ReadInt32("Int10");
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 6);
            packet.ReadInt32("Int20");
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 6);
            packet.ReadInt32("Unix Time");
            packet.ReadXORByte(guid2, 7);

            for (var i = 0; i < bits40; ++i)
                packet.ReadInt32("Int44", i);

            packet.ReadXORByte(guid2, 1);
            packet.ReadInt32("Int34");
            packet.ReadXORByte(guid1, 0);
            packet.ReadInt32("Int30");

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1431)]
        public static void HandleUnknown1431(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.StartBitStream(guid1, 2, 4);
            packet.StartBitStream(guid2, 1, 2);
            packet.StartBitStream(guid1, 0, 5);
            guid2[4] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            packet.StartBitStream(guid2, 7, 0, 3, 6);
            packet.StartBitStream(guid1, 1, 6);
            guid2[5] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 4);
            packet.ReadByte("Byte21");
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 3);
            packet.ReadByte("Byte20");

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1460)]
        public static void HandleUnknown1460(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.ReadPackedTime("Date");
            packet.ReadInt32("Int20"); // RealmId?
            packet.ReadInt32("Int24"); // AchievementId?
            packet.ReadInt32("Int18"); // RealmId?
            guid1[6] = packet.ReadBit();
            packet.StartBitStream(guid2, 5, 3, 0, 1, 6);
            guid1[2] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            packet.StartBitStream(guid1, 1, 4, 5, 7);
            guid2[7] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            packet.ReadBit("bit1C"); // Flags?
            guid1[3] = packet.ReadBit();
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 2);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4393)]
        public static void HandleUnknown4393(Packet packet)
        {
            packet.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_22)]
        public static void HandleUnknown22(Packet packet)
        {
            packet.ReadInt32("Int18");
            var bit10 = !packet.ReadBit();
            var bit3C = packet.ReadBit();
            var bits14 = packet.ReadBits("bits14", 2);

            if (bits14 == 2)
                packet.ReadBit("bit34");

            if (bits14 == 2)
            {
                packet.ReadInt32("Int24");
                packet.ReadInt32("Int2C");
                packet.ReadInt32("Int30");
                packet.ReadInt32("Int1C");
                packet.ReadInt32("Int20");
                packet.ReadInt32("Int28");
            }

            if (bit10)
                packet.ReadByte("Byte10");
            if (bits14 == 1)
                packet.ReadInt32("Int38");
        }

        [Parser(Opcode.SMSG_UNKNOWN_6018)]
        public static void HandleUnknown6018(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadSingle("Float1C");
            packet.ReadInt32("Int18");

            packet.StartBitStream(guid, 6, 3, 1, 4, 0, 5, 2, 7);
            packet.ParseBitStream(guid, 5, 3, 1, 7, 0, 6, 4, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4950)]
        public static void HandleUnknown4950(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 0, 5, 2, 6, 7, 4, 1, 3);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);
            packet.ReadInt32("Int1C");
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 7);
            packet.ReadSingle("Float18");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_6047)]
        public static void HandleUnknown6047(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 1, 5, 4, 0, 6, 3, 2, 7);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadInt32("Int1C");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadSingle("Float18");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5659)]
        public static void HandleUnknown5659(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 3, 0, 1, 5, 2, 7, 6, 4);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);
            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadSingle("Float1C");
            packet.ReadXORByte(guid, 4);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5971)]
        public static void HandleUnknown5971(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 4, 0, 7, 3, 1, 6, 2, 5);
            packet.ReadXORByte(guid, 4);
            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 1);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5702)]
        public static void HandleUnknown5702(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 3, 2, 6, 1, 0, 7, 4, 5);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 7);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1406)]
        public static void HandleUnknown1406(Packet packet)
        {
            var bits10 = packet.ReadBits(19);

            var guid1 = new byte[bits10][];
            var guid2 = new byte[bits10][];

            for (var i = 0; i < bits10; ++i)
            {
                guid1[i] = new byte[8];
                guid2[i] = new byte[8];

                packet.StartBitStream(guid1[i], 3, 6);
                packet.StartBitStream(guid2[i], 5, 4);
                guid1[i][2] = packet.ReadBit();
                guid2[i][0] = packet.ReadBit();
                guid1[i][7] = packet.ReadBit();
                packet.StartBitStream(guid2[i], 6, 7, 3);
                packet.StartBitStream(guid1[i], 5, 0);
                guid2[i][2] = packet.ReadBit();
                packet.StartBitStream(guid1[i], 2, 4);
                guid2[i][1] = packet.ReadBit();
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.ReadXORByte(guid2[i], 4);

                packet.ReadInt32("Int14");

                packet.ReadXORByte(guid1[i], 2);
                packet.ReadXORByte(guid1[i], 0);

                packet.ReadInt32("Int14");

                packet.ReadXORByte(guid2[i], 7);
                packet.ReadXORByte(guid2[i], 1);

                packet.ReadInt32("Int14");
                packet.ReadInt32("Int14");

                packet.ReadXORByte(guid2[i], 6);
                packet.ReadXORByte(guid1[i], 1);
                packet.ReadXORByte(guid2[i], 3);
                packet.ReadXORByte(guid1[i], 4);

                packet.ReadInt32("Int14");

                packet.ReadXORByte(guid1[i], 5);
                packet.ReadXORByte(guid1[i], 3);
                packet.ReadXORByte(guid2[i], 0);
                packet.ReadXORByte(guid1[i], 6);
                packet.ReadXORByte(guid1[i], 7);
                packet.ReadXORByte(guid2[i], 2);
                packet.ReadXORByte(guid2[i], 5);

                packet.WriteGuid("Guid1", guid1[i]);
                packet.WriteGuid("Guid2", guid2[i]);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_1090)]
        public static void HandleUnknown1090(Packet packet)
        {
            packet.ReadInt32("Int10");
            packet.ReadInt32("Int28");
            packet.ReadInt32("Int2C");
            packet.ReadInt32("Int24");

            var bits14 = (int)packet.ReadBits(21);

            for (var i = 0; i < bits14; ++i)
            {
                packet.ReadInt32("Int8", i);
                packet.ReadInt32("Int84", i);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_2227)]
        public static void HandleUnknown2227(Packet packet)
        {
            packet.ReadInt32("Int20");
            packet.ReadSingle("Float10");
            packet.ReadInt32("Int18");
            packet.ReadSingle("Float14");
            packet.ReadSingle("Float1C");
        }

        [Parser(Opcode.SMSG_UNKNOWN_159)]
        public static void HandleUnknown159(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 3, 2, 5, 0, 7);
            packet.ReadBits("bits10", 2);
            packet.StartBitStream(guid, 6, 4, 1);
            packet.ParseBitStream(guid, 7, 3, 2, 1, 4, 6, 0, 5);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4930)]
        public static void HandleUnknown4930(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.ReadSingle("Float34");
            packet.ReadSingle("Float14");
            packet.ReadSingle("Float10");
            packet.ReadSingle("Float18");
            packet.ReadInt32("Int30");
            var bit3B = packet.ReadBit();

            packet.StartBitStream(guid2, 3, 2);

            var bit28 = packet.ReadBit();

            packet.StartBitStream(guid2, 7, 1);

            if (bit28)
                packet.StartBitStream(guid1, 2, 5, 3, 6, 1, 4, 7, 0);

            guid2[4] = packet.ReadBit();
            if (bit3B)
            {
                packet.ReadBit("bit3A");
                packet.ReadBit("bit39");
            }

            packet.StartBitStream(guid2, 6, 0, 5);

            packet.ReadXORByte(guid2, 7);

            if (bit28)
            {
                packet.ParseBitStream(guid1, 3, 0, 1, 7, 2, 6, 5, 4);

                packet.WriteGuid("Guid1", guid1);
            }

            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 6);

            if (bit3B)
                packet.ReadByte("Byte38");

            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4703)]
        public static void HandleUnknown4703(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 0, 2, 1, 6, 7, 3, 4, 5);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 3);
            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 4);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5838)]
        public static void HandleUnknown5838(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 2, 1, 5, 0, 3, 4, 6, 7);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);
            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1299)]
        public static void HandleUnknown1299(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadBit("bit10");
            packet.StartBitStream(guid, 2, 1, 4, 6, 5, 7, 0, 3);
            packet.ParseBitStream(guid, 5, 1, 2, 4, 0, 3, 7, 6);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2307)]
        public static void HandleUnknown2307(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("Int20"); // mapId?
            packet.ReadSingle("Float14");
            packet.ReadInt32("Int30"); // mapId?
            packet.ReadSingle("Float18");
            packet.ReadSingle("Float1C");

            packet.StartBitStream(guid, 5, 3, 4, 2, 6, 0, 7, 1);
            packet.ReadBit("bit10");
            packet.ParseBitStream(guid, 4, 5, 2, 0, 1, 6, 7, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_6174)]
        public static void HandleUnknown6174(Packet packet)
        {
            var count = packet.ReadBits("Count", 22);
            for (var i = 0; i < count; ++i)
                packet.ReadInt32("Int14", i);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5376)]
        public static void HandleUnknown5376(Packet packet)
        {
            var bits10 = packet.ReadBits(19);
            for (var i = 0; i < bits10; ++i)
                packet.ReadBit("bit14", i);

            for (var i = 0; i < bits10; ++i)
            {
                packet.ReadInt32("Int14", i);
                packet.ReadInt32("Int14", i);
                packet.ReadInt32("Int14", i);
                packet.ReadInt32("Int14", i);
                packet.ReadInt32("Int14", i);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_1031)]
        public static void HandleUnknown1031(Packet packet)
        {
            for (var i = 0; i < 256; ++i)
                packet.ReadBit("bit10", i);
        }

        [Parser(Opcode.SMSG_UNKNOWN_272)]
        public static void HandleUnknown272(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var guid3 = new byte[8];

            packet.StartBitStream(guid1, 4, 5, 2, 3, 7);

            var bit30 = packet.ReadBit();
            if (bit30)
                packet.StartBitStream(guid3, 5, 1, 3, 0, 7, 6, 2, 4);

            var bit20 = packet.ReadBit();
            if (bit20)
                packet.StartBitStream(guid2, 6, 4, 0, 2, 7, 5, 1, 3);

            packet.StartBitStream(guid1, 1, 6, 0);

            if (bit20)
            {
                packet.ParseBitStream(guid2, 4, 7, 5, 1, 2, 6, 3, 0);
                packet.WriteGuid("Guid2", guid2);
            }

            if (bit30)
            {
                packet.ParseBitStream(guid3, 3, 2, 7, 0, 5, 1, 6, 4);
                packet.WriteGuid("Guid3", guid3);
            }

            packet.ParseBitStream(guid1, 0, 2, 5, 4, 3, 1, 6, 7);
            packet.WriteGuid("Guid1", guid1);
        }

        [Parser(Opcode.CMSG_UNKNOWN_515)] // CMSG_TIME_SYNC_RESP?
        public static void HandleUnknown515(Packet packet)
        {
            packet.ReadInt32("Int10"); // Count?
            packet.ReadInt32("Int20"); // Ticks?
        }

        [Parser(Opcode.CMSG_UNKNOWN_6083)] // Guild opcode?
        public static void HandleUnknown6083(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[2] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            packet.StartBitStream(guid2, 6, 1);
            packet.StartBitStream(guid1, 5, 4, 6, 1);
            guid2[3] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            packet.StartBitStream(guid2, 5, 7);


            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 1);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.MSG_MULTIPLE_PACKETS1)] // CMSG_TIME_SYNC_RESP?
        public static void HandleMultiplePackets1(Packet packet)
        {
            packet.WriteLine("ClientToServer: CMSG_UNKNOWN_4278"); // Addon?
            var len1 = packet.ReadByte();
            var len2 = packet.ReadBits(5);

            packet.ReadWoWString("string1", len2);
            packet.ReadWoWString("Text", len1);
        }

        [Parser(Opcode.CMSG_QUERY_WORLD_COUNTDOWN_TIMER)] // CMSG_TIME_SYNC_RESP?
        public static void HandleQueryWorldCountdownTimer(Packet packet)
        {
            packet.ReadInt32("Int10");
        }

        [Parser(Opcode.CMSG_UNKNOWN_4524)]
        public static void HandleUnknown4524(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.StartBitStream(guid1, 6, 3, 1);
            packet.StartBitStream(guid2, 0, 1);
            packet.StartBitStream(guid1, 4, 2);
            packet.StartBitStream(guid2, 3, 7, 4);
            guid1[5] = packet.ReadBit();
            packet.StartBitStream(guid2, 6, 5);
            guid1[0] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid1[7] = packet.ReadBit();

            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 7);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.CMSG_UNKNOWN_6774)] // Item opcode?
        public static void HandleUnknown6774(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 7, 6, 0, 4, 5, 3, 2, 1);
            packet.ParseBitStream(guid, 2, 0, 3, 1, 6, 7, 4, 5);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_UNKNOWN_5412)]
        public static void HandleUnknown5412(Packet packet)
        {
            var bit20 = !packet.ReadBit();
            var bit16 = !packet.ReadBit();

            if (bit16)
                packet.ReadInt32("Int16"); // spellId?

            if (bit20)
                packet.ReadByte("Byte20");
        }

        [Parser(Opcode.MSG_UNKNOWN_6315)] // Item opcode?
        public static void HandleUnknown6315(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                var guid = new byte[8];

                packet.ReadInt32("Int10");

                packet.StartBitStream(guid, 0, 4, 1, 7, 6, 2, 5, 3);
                packet.ParseBitStream(guid, 4, 0, 6, 7, 3, 2, 1, 5);

                packet.WriteGuid("Guid", guid);
            }
            else
                packet.ReadInt32("Int10");
        }

        [Parser(Opcode.CMSG_UNKNOWN_6062)]
        public static void HandleUnknown6062(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 3, 1, 2, 6, 4, 7, 0, 5);
            packet.ParseBitStream(guid, 3, 0, 5, 2, 7, 6, 1, 4);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2109)]
        public static void HandleUnknown2109(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 1, 5, 3, 0, 6, 4, 7, 2);
            packet.ReadInt32("Int1C");
            packet.ParseBitStream(guid, 0, 5, 1, 7, 2, 4, 6, 3);
            packet.ReadInt32("Int18");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.MSG_UNKNOWN_5383)]
        public static void HandleUnknown5383(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                var guid = new byte[8];

                packet.ReadInt32("Int10");

                packet.StartBitStream(guid, 3, 6, 2, 0, 4, 1, 7, 5);
                packet.ParseBitStream(guid, 4, 0, 1, 7, 5, 3, 2, 6);

                packet.WriteGuid("Guid", guid);
            }
            else
            {
                var guid1 = new byte[8];
                var guid2 = new byte[8];
                var powerGUID = new byte[8];

                packet.ReadInt32("Int50");
                packet.ReadInt32("Int10");
                packet.ReadInt32("Int64");
                packet.ReadInt32("Int14");
                packet.ReadInt32("Int60");

                packet.StartBitStream(guid2, 1, 6);
                packet.StartBitStream(guid1, 3, 2, 7);
                packet.StartBitStream(guid2, 3, 2);
                guid1[6] = packet.ReadBit();
                var hasPowerData = packet.ReadBit();
                guid1[4] = packet.ReadBit();

                var powerCount = 0u;
                if (hasPowerData)
                {
                    packet.StartBitStream(powerGUID, 0, 7);
                    powerCount = packet.ReadBits(21);
                    packet.StartBitStream(powerGUID, 3, 2, 6, 4, 1, 5);
                }

                guid1[0] = packet.ReadBit();
                guid2[5] = packet.ReadBit();
                guid1[1] = packet.ReadBit();
                guid2[7] = packet.ReadBit();
                guid2[0] = packet.ReadBit();
                guid1[5] = packet.ReadBit();
                guid2[4] = packet.ReadBit();
                packet.ReadXORByte(guid2, 3);
                packet.ReadXORByte(guid1, 5);
                packet.ReadXORByte(guid1, 1);
                if (hasPowerData)
                {
                    packet.ReadXORByte(powerGUID, 2);

                    for (var i = 0; i < powerCount; i++)
                    {
                        packet.ReadInt32("Value", i);
                        packet.ReadUInt32E<PowerType>("Power type", i);
                    }

                    packet.ReadInt32("Current Health");
                    packet.ReadXORByte(powerGUID, 6);
                    packet.ReadInt32("Spell power");
                    packet.ReadXORByte(powerGUID, 4);
                    packet.ReadXORByte(powerGUID, 1);
                    packet.ReadXORByte(powerGUID, 3);
                    packet.ReadInt32("Attack Power");
                    packet.ReadXORByte(powerGUID, 0);
                    packet.ReadXORByte(powerGUID, 7);
                    packet.ReadXORByte(powerGUID, 5);
                    packet.WriteGuid("Power GUID", powerGUID);
                }

                packet.ReadXORByte(guid2, 4);
                packet.ReadXORByte(guid1, 4);
                packet.ReadXORByte(guid1, 6);
                packet.ReadXORByte(guid2, 7);
                packet.ReadXORByte(guid1, 2);
                packet.ReadXORByte(guid2, 2);
                packet.ReadXORByte(guid1, 0);
                packet.ReadXORByte(guid2, 6);
                packet.ReadXORByte(guid2, 1);
                packet.ReadXORByte(guid2, 0);
                packet.ReadXORByte(guid2, 5);
                packet.ReadXORByte(guid1, 3);
                packet.ReadXORByte(guid1, 7);

                packet.WriteGuid("Guid1", guid1);
                packet.WriteGuid("Guid2", guid2);
            }
        }

        [Parser(Opcode.CMSG_UNKNOWN_5177)]
        public static void HandleUnknown5177(Packet packet)
        {
            var len1 = packet.ReadBits(8);
            var len2 = packet.ReadBits(5);
            packet.ReadWoWString("String1", len2);
            packet.ReadWoWString("String1", len1);
        }

        [Parser(Opcode.CMSG_UNKNOWN_5758)]
        public static void HandleUnknown5758(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 2, 5, 1, 6, 7, 4, 0, 3);
            packet.ParseBitStream(guid, 0, 1, 5, 6, 4, 2, 3, 7);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_UNKNOWN_1827)]
        public static void HandleUnknown1827(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            var bit92 = false;
            var bit100 = false;
            var bit136 = false;

            packet.ReadSingle("float44");
            packet.ReadSingle("float36");
            packet.ReadSingle("float40");

            var bit48 = !packet.ReadBit();
            var bit144 = !packet.ReadBit();
            packet.StartBitStream(guid1, 4, 3);
            var bit104 = packet.ReadBit();
            var bit172 = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            var bit24 = !packet.ReadBit();
            guid1[0] = packet.ReadBit();
            var bits152 = packet.ReadBits(22);
            guid1[6] = packet.ReadBit();
            var bit168 = !packet.ReadBit();
            var bit28 = !packet.ReadBit();
            var bit32 = !packet.ReadBit();
            packet.ReadBit("bit149");
            var bit140 = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            packet.ReadBit("bit148");
            packet.StartBitStream(guid1, 7, 1);

            var bit112 = !packet.ReadBit();

            if (bit104)
            {
                packet.StartBitStream(guid2, 4, 3, 2);
                bit92 = packet.ReadBit();
                bit100 = packet.ReadBit();
                packet.StartBitStream(guid2, 7, 1, 0, 5, 6);
            }

            if (bit24)
                packet.ReadBits("bits24", 30);

            if (bit140)
                bit136 = packet.ReadBit();

            if (bit28)
                packet.ReadBits("bits28", 13);

            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 2);

            for (var i = 0; i < bits152; ++i)
                packet.ReadInt32("Int152");

            packet.ReadXORByte(guid1, 3);

            if (bit104)
            {
                packet.ReadXORByte(guid2, 0);
                packet.ReadXORByte(guid2, 5);
                packet.ReadXORByte(guid2, 6);
                packet.ReadXORByte(guid2, 7);

                if (bit100)
                    packet.ReadInt32("Int100");

                packet.ReadXORByte(guid2, 2);

                packet.ReadSingle("float76");
                packet.ReadInt32("Int84");
                packet.ReadByte("byte80");

                packet.ReadXORByte(guid2, 1);

                packet.ReadSingle("float72");
                packet.ReadSingle("float64");

                packet.ReadXORByte(guid2, 4);

                if (bit92)
                    packet.ReadInt32("Int88");

                packet.ReadXORByte(guid2, 3);
                packet.ReadSingle("float68");

                packet.WriteGuid("Guid2", guid2);
            }

            if (bit32)
                packet.ReadInt32("Int32");

            if (bit112)
                packet.ReadSingle("float112");

            if (bit140)
            {
                packet.ReadInt32("Int116");

                if (bit136)
                {
                    packet.ReadSingle("float132");
                    packet.ReadSingle("float128");
                    packet.ReadSingle("float124");
                }

                packet.ReadSingle("float120");
            }

            if (bit168)
                packet.ReadInt32("Int116");

            if (bit48)
                packet.ReadSingle("float48");

            if (bit144)
                packet.ReadSingle("float144");

            packet.WriteGuid("Guid1", guid1);
        }

        [Parser(Opcode.CMSG_UNKNOWN_4831)]
        public static void HandleUnknown4831(Packet packet)
        {
            packet.ReadInt32("Int18");
            packet.ReadInt32("Int18");
            var len = packet.ReadInt32("Int18");
            packet.ReadBytes(len);
            packet.ReadBits("bit3", 3);
        }

        [Parser(Opcode.CMSG_UNKNOWN_822)]
        public static void HandleUnknown822(Packet packet)
        {
            var bit16 = !packet.ReadBit();

            if (bit16)
                packet.ReadInt32("Int16");
        }

        [Parser(Opcode.SMSG_UNKNOWN_289)]
        public static void HandleUnknown289(Packet packet)
        {
            packet.ReadInt32("Unk1 Int32");
        }

        [Parser(Opcode.CMSG_UNKNOWN_5079)]
        public static void HandleUnknown5079(Packet packet)
        {
            packet.ReadInt32("Unk1 Int32");
        }

        [Parser(Opcode.SMSG_UNKNOWN_1446)]
        public static void HandleUnknown1446(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.StartBitStream(guid1, 7, 0, 5);
            guid2[5] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            packet.StartBitStream(guid2, 2, 3);
            guid1[2] = packet.ReadBit();
            packet.StartBitStream(guid2, 1, 7);
            guid1[1] = packet.ReadBit();
            packet.StartBitStream(guid2, 4, 6);
            packet.StartBitStream(guid1, 4, 3);
            guid2[0] = packet.ReadBit();
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 0);
            packet.ReadByte("Byte20");
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 0);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_435)]
        public static void HandleUnknown435(Packet packet)
        {
            packet.ReadInt32("Unk1 Int32");
        }

        [Parser(Opcode.SMSG_UNKNOWN_4998)] // Pet opcode?
        public static void HandleUnknown4998(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 6, 5, 0, 3, 1, 7, 4, 2);
            packet.ParseBitStream(guid, 7, 6, 5, 1, 4, 3, 2, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2187)]
        public static void HandleUnknown2187(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.ReadInt32("Int20");
            packet.ReadByte("Byte28");
            packet.ReadInt32("Int24");
            packet.StartBitStream(guid1, 0, 3);
            packet.StartBitStream(guid2, 1, 7);
            guid1[5] = packet.ReadBit();
            packet.StartBitStream(guid2, 4, 3);
            packet.StartBitStream(guid1, 2, 7, 6);
            guid2[6] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            packet.StartBitStream(guid2, 5, 2);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 5);

            packet.WriteGuid("Guid2", guid1);
            packet.WriteGuid("Guid3", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_256)]
        public static void HandleUnknown256(Packet packet)
        {
            packet.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_1032)]
        public static void HandleUnknown1032(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 6, 5, 1, 2, 4, 0, 3, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 2);
            packet.ReadSingle("Float18");
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadSingle("Float1C");
            packet.ReadXORByte(guid, 6);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_UNKNOWN_5091)]
        public static void HandleUnknown5091(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadByte("Unk byte");

            packet.StartBitStream(guid, 5, 3, 0, 7, 4, 6, 2, 1);
            packet.ParseBitStream(guid, 7, 2, 0, 6, 5, 3, 4, 1);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_6199)]
        public static void HandleUnknown6199(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var powerGUID = new byte[8];

            packet.ReadInt32("Int50"); // SpellId?

            packet.StartBitStream(guid1, 4, 1, 5);
            packet.StartBitStream(guid2, 2, 1);
            guid1[3] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            packet.StartBitStream(guid1, 7, 0);
            packet.StartBitStream(guid2, 0, 7, 3);
            packet.StartBitStream(guid1, 6, 2);
            var hasPowerData = packet.ReadBit();

            var powerCount = 0u;
            if (hasPowerData)
            {
                packet.StartBitStream(powerGUID, 4, 7, 3, 1, 2, 0, 6, 5);
                powerCount = packet.ReadBits(21);
            }

            guid2[4] = packet.ReadBit();
            var bit54 = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 3);

            if (hasPowerData)
            {
                packet.ReadXORByte(powerGUID, 6);

                for (var i = 0; i < powerCount; ++i)
                {
                    packet.ReadUInt32E<PowerType>("Power type", i);
                    packet.ReadInt32("Value", i);
                }

                packet.ReadXORByte(powerGUID, 4);
                packet.ReadXORByte(powerGUID, 0);
                packet.ReadXORByte(powerGUID, 5);
                packet.ReadXORByte(powerGUID, 7);
                packet.ReadInt32("Current Health");
                packet.ReadXORByte(powerGUID, 1);
                packet.ReadInt32("Spell power");
                packet.ReadInt32("Attack Power");
                packet.ReadXORByte(powerGUID, 3);
                packet.ReadXORByte(powerGUID, 2);
                packet.WriteGuid("Power GUID", powerGUID);
            }

            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 1);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.CMSG_UNKNOWN_5134)]
        public static void HandleUnknown5134(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("Int10");
            packet.ReadBit(); // fake bit?
            packet.StartBitStream(guid, 0, 2, 4, 1, 3, 7, 5, 6);
            packet.ParseBitStream(guid, 5, 1, 4, 6, 0, 7, 3, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_UNKNOWN_2951)]
        public static void HandleUnknown2951(Packet packet)
        {
            packet.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_1155)]
        public static void HandleUnknown1155(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 7, 4, 2, 5, 3, 6, 0, 1);
            packet.ReadXORByte(guid, 0);
            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4277)]
        public static void HandleUnknown4277(Packet packet)
        {
            var guid = new byte[8];

            var bits18 = packet.ReadBits(21);
            var bit28 = !packet.ReadBit();

            packet.StartBitStream(guid, 4, 2, 5, 6, 0, 3, 7, 1);

            for (var i = 0; i < bits18; ++i)
            {
                packet.ReadInt32("IntED", i);
                packet.ReadInt32("IntED", i);
            }

            packet.ReadXORByte(guid, 4);
            if (bit28)
                packet.ReadByte("Byte28");
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_3162)]
        public static void HandleUnknown3162(Packet packet)
        {
            packet.ReadInt32("Int10");

            var bits14 = packet.ReadBits("Count", 19);

            var bits4 = new uint[bits14];
            for (var i = 0; i < bits14; ++i)
                bits4[i] = packet.ReadBits(22);

            for (var i = 0; i < bits14; ++i)
            {
                packet.ReadInt32("Int18", i);
                packet.ReadInt64("IntED", i);
                packet.ReadInt32("IntED", i);

                for (var j = 0; j < bits4[i]; ++j)
                    packet.ReadInt32("IntED", i, j);

                packet.ReadInt32("IntED", i);
                packet.ReadInt32("IntED", i);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_1355)]
        public static void HandleUnknown1355(Packet packet)
        {
            uint count = 6;

            for (var i = 0; i < count; ++i)
                packet.ReadInt32("Int10", i);

            for (var i = 0; i < count; ++i)
                packet.ReadInt32("Int28", i);

            for (var i = 0; i < count; ++i)
                packet.ReadInt32("Int40", i);

            for (var i = 0; i < count; ++i)
                packet.ReadInt32("Int58", i);

            for (var i = 0; i < count; ++i)
                packet.ReadInt32("Int70", i);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1374)]
        public static void HandleUnknown1374(Packet packet)
        {
            var bits28 = 0;

            var bit428 = packet.ReadBit();
            if (bit428)
            {
                packet.ReadBit("bit10");
                bits28 = (int)packet.ReadBits(10);
            }

            if (bit428)
            {
                packet.ReadInt32("Int24");
                packet.ReadWoWString("String28", bits28);
                packet.ReadInt32("Int14");
                packet.ReadInt32("Int20");
                packet.ReadInt32("Int1C");
                packet.ReadInt32("Int18");
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_1367)]
        public static void HandleUnknown1367(Packet packet)
        {
            packet.ReadInt64("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_4947)]
        public static void HandleUnknown4947(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 0, 1, 6, 5, 7, 2, 3, 4);
            packet.ReadInt32("Int18");
            packet.ParseBitStream(guid, 4, 3, 2, 0, 1, 5, 7, 6);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4799)]
        public static void HandleUnknown4799(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 6, 4, 2, 0, 3, 7, 5, 1);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid, 5);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_281)]
        public static void HandleUnknown281(Packet packet)
        {
            packet.ReadBits("bits10", 4);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1411)]
        public static void HandleUnknown1411(Packet packet)
        {
            packet.ReadInt32("Int10");
            packet.ReadInt32("Int14");
        }

        [Parser(Opcode.SMSG_UNKNOWN_4990)]
        public static void HandleUnknown4990(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 7, 0, 5, 6, 3, 1, 4, 2);
            packet.ParseBitStream(guid, 3, 1, 7, 5, 2, 6, 0, 4);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2097)]
        public static void HandleUnknown2097(Packet packet)
        {
            packet.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_301)] // Group opcode?
        public static void HandleUnknown301(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            packet.StartBitStream(guid2, 5, 3, 2);
            packet.StartBitStream(guid1, 1, 3, 2);
            packet.StartBitStream(guid2, 4, 0, 1);
            packet.StartBitStream(guid1, 5, 4, 0, 7);
            guid2[6] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 3);
            packet.ReadByte("Byte1C");
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 6);
            packet.ReadInt32("Int18");

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_133)]
        public static void HandleUnknown133(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 3, 2, 6, 1, 0, 7, 5, 4);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadByte("Byte18");
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 1);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5882)]
        public static void HandleUnknown5882(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 4, 6, 3, 7, 2, 5, 0, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadSingle("Float28");
            packet.ReadXORByte(guid, 3);
            packet.ReadSingle("Float1C");
            packet.ReadSingle("Float20");
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 1);
            packet.ReadInt32("Int18");
            packet.ReadSingle("Float24");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_6166)]
        public static void HandleUnknown6166(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 2, 1, 6, 4, 3, 7, 0, 5);
            packet.ParseBitStream(guid, 1, 7, 2, 4, 6, 3, 0, 5);
            packet.ReadInt32("Int18");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5959)]
        public static void HandleUnknown5959(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 2, 7, 0, 6, 5, 3, 1, 4);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 6);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_4679)]
        public static void HandleUnknown4679(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("Int18");
            packet.StartBitStream(guid, 2, 7, 1, 3, 5, 6, 4, 0);
            packet.ParseBitStream(guid, 4, 2, 1, 6, 5, 7, 0, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1432)]
        public static void HandleUnknown1432(Packet packet)
        {
            var bit10 = !packet.ReadBit();
            if (bit10)
                packet.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_2181)]
        public static void HandleUnknown2181(Packet packet)
        {
            packet.ReadSingle("Float18");
            packet.ReadSingle("Float1C");
            packet.ReadInt32("Int10");
            packet.ReadSingle("Float14");
        }

        [Parser(Opcode.SMSG_UNKNOWN_130)]
        public static void HandleUnknown130(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 4, 6, 3, 2, 7, 1, 0, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 0);
            packet.ReadByte("Byte18");
            packet.ReadXORByte(guid, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2083)]
        public static void HandleUnknown2083(Packet packet)
        {
            var bits10 = (int)packet.ReadBits(22);
            for (var i = 0; i < bits10; ++i)
            {
                packet.ReadBits("bits0", 8, i);
                packet.ReadBits("bits0", 8, i);
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.ReadByte("Byte14", i);
                packet.ReadByte("Byte14", i);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_1297)]
        public static void HandleUnknown1297(Packet packet)
        {
            var guid = new byte[8];

            var bits28 = (int)packet.ReadBits(6);
            packet.StartBitStream(guid, 0, 5);
            var bit10 = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var bit60 = packet.ReadBit();
            packet.StartBitStream(guid, 2, 1, 4, 6, 3);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadInt32("Int5C");
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);
            packet.ReadWoWString("String28", bits28);
            packet.ReadXORByte(guid, 7);
            packet.ReadInt32("Int14");
            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid, 6);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_48)]
        public static void HandleUnknown48(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 6, 3, 1, 4, 0, 5, 2, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_310)]
        public static void HandleUnknown310(Packet packet)
        {
            packet.ReadInt32("Int14");
            packet.ReadInt32("Int18");
            packet.ReadInt32("Int10");
            packet.ReadInt32("Int1C");
        }

        [Parser(Opcode.SMSG_UNKNOWN_441)]
        public static void HandleUnknown441(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 5, 7, 0, 3, 4, 6, 1, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);
            packet.ReadInt16("Int18");
            packet.ReadXORByte(guid, 5);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2224)]
        public static void HandleUnknown2224(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 5, 0, 1, 7, 6, 3, 2, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);
            packet.ReadInt32("Int20");
            packet.ReadXORByte(guid, 1);
            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 7);
            packet.ReadInt32("Int1C");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_13)]
        public static void HandleUnknown13(Packet packet)
        {
            packet.ReadByte("Byte10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_1366)]
        public static void HandleUnknown1366(Packet packet)
        {
            var guid = new byte[8];

            var bits14 = packet.ReadBits(6);
            packet.StartBitStream(guid, 7, 1, 3, 0, 5, 6, 2, 4);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 2);
            packet.ReadWoWString("String14", bits14);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 5);
            packet.ReadInt32("Int10");
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 7);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1386)]
        public static void HandleUnknown1386(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.StartBitStream(guid1, 2, 6);
            packet.ReadBit("bit10");
            packet.StartBitStream(guid1, 0, 1, 5);
            packet.StartBitStream(guid2, 2, 7);
            guid1[4] = packet.ReadBit();
            packet.StartBitStream(guid2, 4, 5, 6, 0);
            packet.StartBitStream(guid1, 7, 3);
            packet.StartBitStream(guid2, 1, 3);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 5);
            packet.ReadInt32("Int28");
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 2);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_180)]
        public static void HandleUnknown180(Packet packet)
        {
            var bit14 = packet.ReadBit();
            packet.ReadBit("bit1C");
            var bit28 = packet.ReadBit();
            if (bit28)
                packet.ReadInt32("Int24");
            packet.ReadInt32("Int18");
            packet.ReadInt32("Int20");
            packet.ReadInt32("Int2C");
            if (bit14)
                packet.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_1442)]
        public static void HandleUnknown1442(Packet packet)
        {
            packet.ReadInt32("Int2C");
            packet.ReadInt32("Int24");
            packet.ReadInt32("Int20");
            packet.ReadInt32("Int28");
            var bits10 = packet.ReadBits(20);

            for (var i = 0; i < bits10; ++i)
                packet.ReadBit("bit10", i);

            for (var i = 0; i < bits10; ++i)
            {
                packet.ReadInt32("Int14", i);
                packet.ReadInt32("Int14", i);
                packet.ReadInt32("Int14", i);
                packet.ReadInt32("Int14", i);
            }
        }

        [Parser(Opcode.SMSG_UNKNOWN_28)]
        public static void HandleUnknown28(Packet packet)
        {
            var guid = new byte[8];
            var bit161 = packet.ReadBit();
            var bit129 = packet.ReadBit();
            guid[4] = packet.ReadBit();
            var bits130 = packet.ReadBits(6);
            guid[2] = packet.ReadBit();
            var bit21 = packet.ReadBit();
            var bit20 = packet.ReadBit();
            packet.StartBitStream(guid, 3, 7);
            var bits28 = packet.ReadBits(9);
            packet.StartBitStream(guid, 0, 5);
            var bits10 = packet.ReadBits(22);
            packet.StartBitStream(guid, 6, 1);
            packet.ReadInt32("Int12C");
            packet.ReadInt64("Int170");
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadInt32("Int164");
            packet.ReadWoWString("String28", bits28); // Realm?
            packet.ReadWoWString("String130", bits130); // Name?
            packet.ReadInt32("Int24");
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 1);
            for (var i = 0; i < bits10; ++i)
                packet.ReadInt32("IntEA", i);

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_146)]
        public static void HandleUnknown146(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 0, 4, 2, 6, 3, 5, 1, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 1);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1076)]
        public static void HandleUnknown1076(Packet packet)
        {
            packet.ReadInt32("Int14");
            packet.ReadInt32("Int1C");
            packet.ReadInt32("Int18");
            packet.ReadInt32("Int24");
            packet.ReadInt32("Int20");
            packet.ReadBit("bit10");
        }

        [Parser(Opcode.SMSG_UNKNOWN_2212)] // Group opcode?
        public static void HandleUnknown2212(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            packet.StartBitStream(guid2, 7, 1, 4);
            packet.StartBitStream(guid1, 3, 6);
            guid2[5] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            packet.StartBitStream(guid2, 0, 6);
            packet.StartBitStream(guid1, 2, 5, 4, 0);
            guid2[2] = packet.ReadBit();
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 3);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1310)]
        public static void HandleUnknown1310(Packet packet)
        {
            var bits0 = (int)packet.ReadBits(8);
            packet.ReadWoWString("String10", packet.ReadBits(8));
        }

        [Parser(Opcode.SMSG_UNKNOWN_2063)]
        public static void HandleUnknown2063(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 0, 5, 7, 6, 1, 2, 4, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 2);
            packet.ReadByte("Byte18");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_2091)]
        public static void HandleUnknown2091(Packet packet)
        {
            packet.ReadInt32("Int1C");
            packet.ReadSingle("Float10");
            packet.ReadSingle("Float14");
            packet.ReadSingle("Float18");
        }

        [Parser(Opcode.SMSG_UNKNOWN_6536)]
        public static void HandleUnknown6536(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 7, 3, 6, 5, 0, 1, 4, 2);
            packet.ReadInt16("Int20");
            packet.ReadByte("Byte24");
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 5);
            packet.ReadInt32("Int1C");
            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadInt16("Int22");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_UNKNOWN_5766)]
        public static void HandleUnknown5766(Packet packet)
        {
            packet.ReadBits("bits3", 3);
        }

        [Parser(Opcode.CMSG_UNKNOWN_4266)]
        public static void HandleUnknown4266(Packet packet)
        {
            packet.ReadBit("Unk bit");
        }

        [Parser(Opcode.CMSG_UNKNOWN_2851)]
        public static void HandleUnknown2851(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadBit("Unk1 bit"); // 0 has guid / 1 has not guid?

            packet.StartBitStream(guid, 4, 7, 6, 0, 5, 3, 1, 2);
            packet.ParseBitStream(guid, 7, 0, 3, 6, 5, 2, 4, 1);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_UNKNOWN_5675)]
        public static void HandleUnknown5675(Packet packet)
        {
            packet.ReadByte("Unk Byte");
        }

        [Parser(Opcode.CMSG_UNKNOWN_5915)]
        public static void HandleUnknown5915(Packet packet)
        {
            packet.ReadBit("Unk1 bit");
            packet.ReadBit("Unk2 bit");
        }

        [Parser(Opcode.CMSG_UNKNOWN_597)]
        public static void HandleUnknown597(Packet packet)
        {
            var bits10 = packet.ReadBits("Unk bits22", 22);

            for (var i = 0; i < bits10; ++i)
                packet.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.CMSG_UNKNOWN_6910)]
        public static void HandleUnknown6910(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadByte("Unk Byte");
            packet.ReadInt32("Unk Int32");

            packet.StartBitStream(guid, 7, 4, 6, 5, 0, 1, 3, 2);
            packet.ParseBitStream(guid, 5, 4, 2, 6, 3, 7, 0, 1);

            packet.WriteGuid("Guid", guid);
        }
    }
}

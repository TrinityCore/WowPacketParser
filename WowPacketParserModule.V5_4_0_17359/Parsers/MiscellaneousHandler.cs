using System;
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
        [Parser(Opcode.CMSG_LOAD_SCREEN)]
        public static void HandleClientEnterWorld(Packet packet)
        {
            var mapId = packet.ReadEntryWithName<UInt32>(StoreNameType.Map, "Map");
            packet.ReadBit("Loading");
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
            packet.ReadEnum<ClientSplitState>("Client State", TypeCode.Int32);
        }

        [Parser(Opcode.CMSG_VIOLENCE_LEVEL)]
        public static void HandleSetViolenceLevel(Packet packet)
        {
            packet.ReadByte("Level");
        }

        [Parser(Opcode.SMSG_SEND_SERVER_LOCATION)]
        public static void HandleSendServerLocation(Packet packet)
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
            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map");
            packet.WriteLine("Position: {0}", pos);

            packet.AddSniffData(StoreNameType.Map, (int)CoreParsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }

        [Parser(Opcode.SMSG_REALM_SPLIT)]
        public static void HandleServerRealmSplit(Packet packet)
        {
            var len = packet.ReadBits(7);
            packet.ReadEnum<PendingSplitState>("Split State", TypeCode.Int32);
            packet.ReadEnum<ClientSplitState>("Client State", TypeCode.Int32);
            packet.ReadWoWString("Split Date", len);
        }

        [Parser(Opcode.CMSG_AREATRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            packet.ReadInt32("Area Trigger Id");
            packet.ReadBit("Unk bit1");
            packet.ReadBit("Unk bit2");
        }

        [Parser(Opcode.SMSG_WEATHER)]
        public static void HandleWeatherStatus(Packet packet)
        {
            packet.ReadBit("Unk bit");
            packet.ReadSingle("Grade");
            packet.ReadEnum<WeatherState>("State", TypeCode.Int32);
        }

        [Parser(Opcode.SMSG_HOTFIX_INFO)]
        public static void HandleHotfixInfo(Packet packet)
        {
            var count = packet.ReadBits("Count", 20);

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Hotfixed entry", i);
                packet.ReadTime("Hotfix date", i);
                packet.ReadEnum<DB2Hash>("Hotfix DB2 File", TypeCode.Int32, i);
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

        [Parser(Opcode.SMSG_UNKNOWN_5125)]
        public static void HandleUnknown5125(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 2, 7, 0, 6, 5, 3, 1, 4);
            packet.ParseBitStream(guid, 2, 0, 1, 7, 4, 5);
            packet.ReadInt32("Int18");
            packet.ParseBitStream(guid, 3, 6);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_UNKNOWN_57)]
        public static void HandleUnknown57(Packet packet)
        {
            packet.ReadByte("Byte20");
            packet.ReadInt32("Int1C");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");

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

        [Parser(Opcode.SMSG_UNKNOWN_5730)]
        public static void HandleUnknown5730(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 6, 3, 4, 2, 5, 7, 0, 1);
            packet.ReadXORByte(guid, 4);
            packet.ReadSingle("Speed");
            packet.ParseBitStream(guid, 7, 1, 6, 3, 0, 2, 5);

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
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
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
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
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
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
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

        [Parser(Opcode.SMSG_UNKNOWN_4262)]
        public static void HandleUnknown4262(Packet packet)
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

            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");

            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 7);

            packet.WriteGuid("Guid1", guid1);
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
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
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
                packet.WriteLine("ServerToClient: SMSG_QUESTGIVER_QUEST_COMPLETE");

                packet.ReadInt32("Reward XP");
                packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Quest ID");
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
                    packet.ReadEnum<PowerType>("Power type", TypeCode.Int32, i); // Actually powertype for class
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
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");

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
                    packet.ReadEnum<PowerType>("Power type", TypeCode.Int32, i); // Actually powertype for class
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

            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
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
            packet.ReadInt32("Int18");

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
    }
}

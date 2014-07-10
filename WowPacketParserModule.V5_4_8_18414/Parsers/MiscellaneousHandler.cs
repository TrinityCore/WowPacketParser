using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParserModule.V5_4_8_18414.Enums;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.CMSG_ADD_FRIEND)]
        public static void HandleAddFriend(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_ADD_IGNORE)]
        public static void HandleAddIgnore(Packet packet)
        {
            packet.ReadCString("Str");
        }

        [Parser(Opcode.CMSG_AREATRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_BUY_BANK_SLOT)]
        public static void HandleBuyBankSlot(Packet packet)
        {
            var guid = packet.StartBitStream(7, 6, 1, 3, 2, 0, 4, 5);
            packet.ParseBitStream(guid, 3, 5, 1, 6, 7, 2, 0, 4);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_DEL_FRIEND)]
        public static void HandleDelFriend(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_DEL_IGNORE)]
        public static void HandleDelIgnore(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_LOAD_SCREEN)]
        public static void HandleClientEnterWorld(Packet packet)
        {
            var mapId = packet.ReadEntryWithName<UInt32>(StoreNameType.Map, "Map Id");
            packet.ReadBit("Loading");

            CoreParsers.MovementHandler.CurrentMapId = (uint)mapId;

            packet.AddSniffData(StoreNameType.Map, mapId, "LOAD_SCREEN");
        }

        [Parser(Opcode.CMSG_LOG_DISCONNECT)]
        public static void HandleLogDisconnect(Packet packet)
        {
            packet.ReadUInt32("Disconnect Reason");
            // 4 is inability for client to decrypt RSA
            // 3 is not receiving "WORLD OF WARCRAFT CONNECTION - SERVER TO CLIENT"
            // 11 is sent on receiving opcode 0x140 with some specific data
        }

        [Parser(Opcode.CMSG_MINIMAP_PING)]
        public static void HandleMinimapPing(Packet packet)
        {
            packet.ReadSingle("Y");
            packet.ReadSingle("X");
            packet.ReadByte("byte24");
        }

        [Parser(Opcode.CMSG_OPENING_CINEMATIC)]
        public static void HandleOpeningCinematic(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_PING)]
        public static void HandleClientPing(Packet packet)
        {
            packet.ReadUInt32("Latency");
            packet.ReadUInt32("Ping");
        }

        [Parser(Opcode.CMSG_RAID_READY_CHECK)]
       public static void HandleRaidReadyCheck(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadToEnd();
            }
            else
            {
                packet.WriteLine("              : SMSG_UNK_0817"); // sub_C8CBBE
                var guid = packet.StartBitStream(5, 0, 6, 3, 7, 2, 4, 1);
                packet.ReadInt32("Counter"); // 28
                packet.ParseBitStream(guid, 1, 3);
                packet.ReadSingle("Speed"); // 24
                packet.ParseBitStream(guid, 6, 7, 0, 5, 2, 4);
                packet.WriteGuid("Guid", guid);
            }
        }

        [Parser(Opcode.CMSG_RAID_READY_CHECK_CONFIRM)]
        public static void HandleRaidReadyCheckConfirm(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_RANDOM_ROLL)]

        public static void HandleRandomRoll(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadToEnd();
            }
            else
            {
                packet.WriteLine("              : SMSG_???");
                packet.ReadToEnd();
            }
        }

        [Parser(Opcode.CMSG_REALM_SPLIT)]
        [Parser(Opcode.CMSG_UNK_1A87)]

        public static void HandleClientRealmSplit(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_SET_CONTACT_NOTES)]
        public static void HandleSetContactNotes(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_SET_DUNGEON_DIFFICULTY)]
        public static void HandleSetDungeonDifficulty(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_SET_SELECTION)]
        public static void HandleSetSelection(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_TIME_SYNC_RESP)]
        public static void HandleTimeSyncResp(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadUInt32("Counter");
                packet.ReadUInt32("Client Ticks");
            }
            else
            {
                packet.WriteLine("              : SMSG_???");
                packet.ReadToEnd();
            }

        }

        [Parser(Opcode.CMSG_TIME_SYNC_RESP_FAILED)]
        public static void HandleTimeSyncRespFailed(Packet packet)
        {
            packet.ReadUInt32("Unk Uint32");
        }

        [Parser(Opcode.CMSG_UNK_0249)]
        public static void HandleUnk249(Packet packet)
        {
            packet.ReadUInt32("unk1");
            packet.ReadBit("unkb");
        }

        [Parser(Opcode.SMSG_CLIENTCACHE_VERSION)]
        public static void HandleClientCacheVersion(Packet packet)
        {
            packet.ReadUInt32("Version");
        }

        [Parser(Opcode.SMSG_HOTFIX_INFO)]
        public static void HandleHotfixInfo(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_PLAY_SOUND)]
        public static void HandlePlaySound(Packet packet)
        {
            var guid = packet.StartBitStream(2, 3, 7, 6, 0, 5, 4, 1);
            packet.ReadInt32("Sound");
            packet.ParseBitStream(guid, 3, 2, 4, 7, 5, 0, 6, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SET_TIMEZONE_INFORMATION)]
        public static void HandleServerTimezone(Packet packet)
        {
            var Location2Lenght = packet.ReadBits(7);
            var Location1Lenght = packet.ReadBits(7);

            packet.ReadWoWString("Timezone Location1", Location1Lenght);
            packet.ReadWoWString("Timezone Location2", Location2Lenght);
        }

        [Parser(Opcode.SMSG_WORLD_SERVER_INFO)]
        public static void HandleWorldServerInfo(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
            {
                packet.ReadToEnd();
            }
            else
            {
                packet.WriteLine("              : CMSG_???");
                packet.ReadToEnd();
            }
        }

        [Parser(Opcode.SMSG_UNK_001F)]
        public static void HandleUnk001F(Packet packet)
        {
            //var guid = packet.StartBitStream(4, 7, 1, 0, 5, 3, 2);
            var guid = new byte[8];
            guid[4] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var bit80 = packet.ReadBit("Byte80");
            guid[6] = packet.ReadBit();
            packet.ReadBit("Byte16");
            if (!bit80)
                packet.ReadInt32("Dword80");

            packet.ParseBitStream(guid, 5, 6, 7, 3, 4, 0, 2, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_0063)] // sub_C89A3D
        public static void HandleUnk0063(Packet packet)
        {
            var guid = packet.StartBitStream(6, 4, 2, 7, 1, 3, 0, 5);
            packet.ParseBitStream(guid, 2, 7, 5, 1, 4, 6, 0, 3);
            packet.WriteGuid("Guid", guid);
            packet.ReadSingle("unk");
        }

        [Parser(Opcode.SMSG_UNK_00A3)]
        public static void HandleUnk00A3(Packet packet)
        {
            packet.ReadInt32("Dword4");
        }

        [Parser(Opcode.SMSG_UNK_01E1)] // sub_C8B308
        public static void HandleUnk01E1(Packet packet)
        {
            var guid = packet.StartBitStream(1, 5, 2, 0, 3, 6, 4, 7);
            packet.ParseBitStream(guid, 2, 7, 1, 3, 5, 0, 4, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_043F)]
        public static void HandleUnk043F(Packet packet)
        {
            packet.ReadInt32("Dword8");
            packet.ReadBits("unk", 19);
        }

        [Parser(Opcode.SMSG_UNK_05F3)]
        public static void HandleUnk05F3(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];
            guid2[1] = packet.ReadBit(); // 81
            guid[2] = packet.ReadBit(); // 34
            var unk72 = packet.ReadBit("unk72"); // 72
            guid[6] = packet.ReadBit(); // 38
            guid2[3] = packet.ReadBit(); // 83
            guid[4] = packet.ReadBit(); // 36
            guid2[2] = packet.ReadBit(); // 82
            guid2[5] = packet.ReadBit(); // 85
            guid2[6] = packet.ReadBit(); // 86
            guid[3] = packet.ReadBit(); // 35
            guid2[0] = packet.ReadBit(); // 80
            guid[5] = packet.ReadBit(); // 37
            guid[1] = packet.ReadBit(); // 33
            guid[0] = packet.ReadBit(); // 32
            guid2[7] = packet.ReadBit(); // 87
            guid2[4] = packet.ReadBit(); // 84
            guid[7] = packet.ReadBit(); // 39

            if (unk72)
            {
                var unk56 = packet.ReadBits("unk56", 21); // 56
                packet.ReadInt32("unk48"); // 48
                for ( var i = 0; i < unk56; i++ )
                {
                    packet.ReadInt32("unk64", i); // 64
                    packet.ReadInt32("unk60", i); // 60
                }
                packet.ReadInt32("unk44"); // 44
                packet.ReadInt32("unk52"); // 52
            }

            packet.ReadXORByte(guid2, 2); // 82
            packet.ReadXORByte(guid, 6); // 38
            packet.ReadXORByte(guid2, 6); // 86
            packet.ReadXORByte(guid2, 4); // 84
            packet.ReadXORByte(guid, 3); // 35
            packet.ReadXORByte(guid2, 7); // 87
            packet.ReadInt32("unk40"); // 40
            packet.ReadXORByte(guid, 4); // 36
            packet.ReadXORByte(guid2, 1); // 81
            packet.ReadInt32("unk24"); // 24
            packet.ReadXORByte(guid, 7); // 39
            packet.ReadInt32("unk16"); // 16
            packet.ReadInt32("unk20"); // 20
            packet.ReadXORByte(guid2, 5); // 85
            packet.ReadXORByte(guid, 5); // 37
            packet.ReadXORByte(guid2, 0); // 80
            packet.ReadXORByte(guid, 1); // 33
            packet.ReadXORByte(guid, 0); // 32
            packet.ReadXORByte(guid, 2); // 34
            packet.ReadInt32("unk88"); // 88
            packet.ReadXORByte(guid2, 3); // 83

            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNK_0632)]
        public static void HandleUnk0632(Packet packet)
        {
            var guid = new byte[8];
            guid[5] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            var count = packet.ReadBits("Count", 21);
            var guid1 = new byte[count][];
            for (var i = 0; i < count; i++)
            {
                guid1[i] = packet.StartBitStream(2, 3, 6, 5, 1, 4, 0, 7);
            }

            guid[2] = packet.ReadBit();

            for (var i = 0; i < count; i++)
            {
                packet.ParseBitStream(guid1[i], 6, 7, 0, 1, 2, 5, 3, 4);
                packet.WriteGuid("Guid", guid1[i], i);
                packet.ReadInt32("Int20", i);
            }

            packet.ParseBitStream(guid, 1, 4, 2, 3, 5, 6, 0, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_069B)]
        public static void HandleUnk069B(Packet packet)
        {
            packet.ReadInt32("Dword20");
            packet.ReadInt32("Dword16");
        }

        [Parser(Opcode.SMSG_UNK_0728)] // sub_C8CED2
        public static void HandleUnk0728(Packet packet)
        {
            var guid = packet.StartBitStream(3, 7, 2, 4, 5, 6, 0, 1);
            packet.ParseBitStream(guid, 2, 4, 5, 7, 1, 0, 3, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_08B2)] // sub_C8A6C4
        public static void HandleUnk08B2(Packet packet)
        {
            var guid = packet.StartBitStream(4, 1, 7, 6, 3, 2, 5, 0);
            packet.ParseBitStream(guid, 2, 3, 1, 0, 6, 5, 4, 7);
            packet.WriteGuid("Guid", guid);
            packet.ReadSingle("unk24");
        }

        [Parser(Opcode.SMSG_UNK_0987)]
        public static void HandleUnk0987(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_UNK_0A0B)]
        public static void HandleUnk0A0B(Packet packet)
        {
            for (var i = 0; i < 256; i++)
                packet.ReadBit("Byte16", i);
        }

        [Parser(Opcode.SMSG_UNK_0E9B)]
        public static void HandleUnk0E9B(Packet packet)
        {
            var guid = packet.StartBitStream(4, 6, 2, 3, 7, 1, 5, 0);
            packet.ParseBitStream(guid, 3, 6, 2);
            packet.ReadUInt32("Int72");
            packet.ReadUInt32("Int76");
            packet.ParseBitStream(guid, 5, 1);
            packet.ReadInt32("Int28");
            packet.ParseBitStream(guid, 4);
            packet.ReadUInt32("Int24");
            packet.ReadUInt32("Int80");
            packet.ParseBitStream(guid, 7, 0);
            packet.WriteGuid("Guid", guid);
            packet.ReadUInt64("QW16");
        }

        [Parser(Opcode.SMSG_UNK_103B)]
        public static void HandleUnk103B(Packet packet)
        {
            packet.ReadInt32("DWord16");
            packet.ReadInt32("DWord20");
            packet.ReadInt32("DWord24");
            var count = packet.ReadInt32("Count");
            var bytes = new byte[count];
            bytes = packet.ReadBytes(count);
        }

        [Parser(Opcode.SMSG_UNK_121E)]
        public static void HandleUnk121E(Packet packet)
        {
            packet.ReadBit("Bit in Byte16");
            packet.ReadBit("Bit in Byte18");
            packet.ReadBit("Bit in Byte17");
        }

        [Parser(Opcode.SMSG_UNK_129A)]
        public static void HandleUnk129A(Packet packet)
        {
            var count = packet.ReadBits("Count", 22);
            packet.ReadBit("Byte16");
            for (var i = 0; i < count; i++)
                packet.ReadUInt32("Dword24", i);
        }

        [Parser(Opcode.SMSG_UNK_1440)]
        public static void HandleUnk1440(Packet packet)
        {
            packet.ReadUInt32("Dword16");
            packet.ReadByte("Byte20");
        }

        [Parser(Opcode.SMSG_UNK_16BF)]
        public static void HandleUnk16BF(Packet packet)
        {
            var count = packet.ReadBits("Count", 20); // 16
            var guid = new byte[count][];

            for ( var i = 0; i < count; i++ )
            {
                guid[i][1] = packet.ReadBit(); // 29
                guid[i][2] = packet.ReadBit(); // 30
                guid[i][6] = packet.ReadBit(); // 34
                packet.ReadBit("unk37"); // 37
                guid[i][0] = packet.ReadBit(); // 28
                guid[i][5] = packet.ReadBit(); // 33
                guid[i][4] = packet.ReadBit(); // 32
                packet.ReadBit("unk36"); // 36
                guid[i][7] = packet.ReadBit(); // 35
                guid[i][3] = packet.ReadBit(); // 31
            }
            for ( var i = 0; i < count; i++ )
            {
                packet.ParseBitStream(guid[i], 7, 6, 4, 2, 0);
                packet.ReadInt32("unk40", i); // 40
                packet.ReadInt32("unk44", i); // 44
                packet.ParseBitStream(guid[i], 1);
                packet.ReadInt32("unk20", i); // 20
                packet.ReadInt32("unk24", i); // 24
                packet.ParseBitStream(guid[i], 3, 5);

                packet.WriteGuid("Guid", guid[i], i);
            }
        }

        [Parser(Opcode.SMSG_UNK_1D8E)]
        public static void HandleUnk1D8E(Packet packet)
        {
            var guid = packet.StartBitStream(5, 6, 7, 3, 4, 2, 1, 0);
            packet.ParseBitStream(guid, 4, 1, 6, 7, 3);
            packet.ReadSingle("unk24"); // 24
            packet.ParseBitStream(guid, 5, 0, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_1DAB)]
        public static void HandleUnk1DAB(Packet packet)
        {
            packet.ReadSingle("unk24"); // 24
            var guid = packet.StartBitStream(1, 4, 7, 3, 2, 6, 5, 0);
            packet.ParseBitStream(guid, 5, 1, 0, 6, 2, 4, 7, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_1E9B)]
        public static void HandleUnk1E9B(Packet packet)
        {
            packet.ReadUInt32("Dword8");
            packet.ReadUInt32("Dword5");
            packet.ReadUInt32("Dword6");
            packet.ReadUInt32("Dword7");
            packet.ReadBit("Bit in Byte16");
        }

        [Parser(Opcode.SMSG_UNK_1F9F)]
        public static void HandleUnk1F9F(Packet packet)
        {
            var guid = packet.StartBitStream(7, 4, 0, 3, 2, 5, 6, 1);
            packet.ParseBitStream(guid, 6, 4, 1, 5, 2, 3, 7);
            packet.ReadSingle("unk24"); // 24
            packet.ParseBitStream(guid, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_ATTACKSTOP)]
        [Parser(Opcode.CMSG_GET_TIMEZONE_INFORMATION)]
        [Parser(Opcode.CMSG_QUESTGIVER_STATUS_MULTIPLE_QUERY)]
        [Parser(Opcode.CMSG_SPELLCLICK)]
        [Parser(Opcode.CMSG_WORLD_STATE_UI_TIMER_UPDATE)]
        [Parser(Opcode.MSG_MOVE_WORLDPORT_ACK)]
        public static void HandleNullMisc(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
            {
                packet.WriteLine("              : SMSG_???");
                packet.ReadToEnd();
            }
            else
            {
                packet.ReadToEnd();
            }
        }
    }
}

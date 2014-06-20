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

        [Parser(Opcode.CMSG_LOAD_SCREEN)]
        public static void HandleClientEnterWorld(Packet packet)
        {
            var mapId = packet.ReadEntryWithName<UInt32>(StoreNameType.Map, "Map Id");
            packet.ReadBit("Loading");

            CoreParsers.MovementHandler.CurrentMapId = (uint)mapId;

            packet.AddSniffData(StoreNameType.Map, mapId, "LOAD_SCREEN");
        }

        [Parser(Opcode.CMSG_REALM_SPLIT)]
        [Parser(Opcode.CMSG_UNK_1A87)]

        public static void HandleClientRealmSplit(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_CLIENTCACHE_VERSION)]
        public static void HandleClientCacheVersion(Packet packet)
        {
            packet.ReadUInt32("Version");
        }

        [Parser(Opcode.SMSG_SERVER_TIMEZONE)]
        public static void HandleServerTimezone(Packet packet)
        {
            var Location2Lenght = packet.ReadBits(7);
            var Location1Lenght = packet.ReadBits(7);

            packet.ReadWoWString("Timezone Location1", Location1Lenght);
            packet.ReadWoWString("Timezone Location2", Location2Lenght);
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

        [Parser(Opcode.SMSG_UNK_00A3)]
        public static void HandleUnk00A3(Packet packet)
        {
            packet.ReadInt32("Dword4");
        }

        [Parser(Opcode.SMSG_UNK_043F)]
        public static void HandleUnk043F(Packet packet)
        {
            packet.ReadInt32("Dword8");
            packet.ReadBits("unk", 19);
        }

        [Parser(Opcode.SMSG_UNK_069B)]
        public static void HandleUnk069B(Packet packet)
        {
            packet.ReadInt32("Dword20");
            packet.ReadInt32("Dword16");
        }

        [Parser(Opcode.SMSG_UNK_0A0B)]
        public static void HandleUnk0A0B(Packet packet)
        {
            for (var i = 0; i < 256; i++)
                packet.ReadBit("Byte16", i);
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

        [Parser(Opcode.SMSG_UNK_121B)]
        public static void HandleUnk121B(Packet packet)
        {
            packet.ReadBit("Bit in Byte16");
            packet.ReadUInt32("Dword24");
            packet.ReadUInt32("Dword20");
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

        [Parser(Opcode.SMSG_UNK_1E9B)]
        public static void HandleUnk1E9B(Packet packet)
        {
            packet.ReadUInt32("Dword8");
            packet.ReadUInt32("Dword5");
            packet.ReadUInt32("Dword6");
            packet.ReadUInt32("Dword7");
            packet.ReadBit("Bit in Byte16");
        }
    }
}

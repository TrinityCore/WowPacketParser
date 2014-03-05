using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17956.Parsers
{
    public static class ItemHandler
    {
        [Parser(Opcode.CMSG_AUTOEQUIP_ITEM, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_AUTOEQUIP_ITEM)]
        public static void HandleAutoEquipItem547(Packet packet)
        {
            packet.ReadByte("Slot");
            packet.ReadSByte("Bag");
        }

        [Parser(Opcode.CMSG_BUY_ITEM, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_BUY_ITEM)]
        public static void HandleBuyItem547(Packet packet)
        {
            packet.ReadUInt32("Bag Slot");
            packet.ReadUInt32("Item Entry");
            packet.ReadUInt32("Item Count");
            packet.ReadUInt32("Vendor Slot");

            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[2] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid2[0] = packet.ReadBit();

            packet.ReadBits("Item Type", 2);

            guid2[6] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid1[6] = packet.ReadBit();

            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 7);

            packet.WriteGuid("Vendor Guid", guid1);
            packet.WriteGuid("Bag Guid", guid2);
        }

        [Parser(Opcode.CMSG_SELL_ITEM, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_SELL_ITEM)]
        public static void HandleSellItem547(Packet packet)
        {
            packet.ReadUInt32("Count");

            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid1[7] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid1[1] = packet.ReadBit();

            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 2);

            packet.WriteGuid("Item Guid", guid1);
            packet.WriteGuid("Vendor Guid", guid2);
        }

        [Parser(Opcode.CMSG_REQUEST_HOTFIX, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_REQUEST_HOTFIX)]
        public static void HandleItemRequestHotfix547(Packet packet)
        {
            packet.ReadUInt32("Type");

            var count = packet.ReadBits("Count", 21);

            var guidBytes = new byte[count][];

            for (var i = 0; i < count; ++i)
                guidBytes[i] = packet.StartBitStream(2, 4, 3, 6, 7, 1, 5, 0);

            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guidBytes[i], 5);
                packet.ReadXORByte(guidBytes[i], 4);
                packet.ReadXORByte(guidBytes[i], 3);

                packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry", i);

                packet.ReadXORByte(guidBytes[i], 7);
                packet.ReadXORByte(guidBytes[i], 0);
                packet.ReadXORByte(guidBytes[i], 2);
                packet.ReadXORByte(guidBytes[i], 1);
                packet.ReadXORByte(guidBytes[i], 6);

                packet.WriteGuid("GUID", guidBytes[i], i);
            }
        }

        [Parser(Opcode.CMSG_SWAP_ITEM, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_SWAP_ITEM)]
        public static void HandleSwapItem547(Packet packet)
        {
            packet.ReadByte("Unk 1");
            packet.ReadByte("Unk 2");

            packet.ReadBits("Item Count", 2);

            packet.ReadByte("Destination Slot");
            packet.ReadSByte("Destination Bag");
            packet.ReadByte("Initial Slot");
            packet.ReadSByte("Initial Bag");
        }
    }
}

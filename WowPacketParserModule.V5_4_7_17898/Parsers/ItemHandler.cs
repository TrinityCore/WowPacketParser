using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class ItemHandler
    {
        [Parser(Opcode.SMSG_SET_PROFICIENCY)]
        public static void HandleSetProficency(Packet packet)
        {
            packet.ReadEnum<UnknownFlags>("Mask", TypeCode.UInt32);
            packet.ReadEnum<ItemClass>("Class", TypeCode.Byte);
        }

        [Parser(Opcode.SMSG_ITEM_ENCHANT_TIME_UPDATE)]
        public static void HandleItemEnchantTimeUpdate(Packet packet)
        {
            var itemGuid = new byte[8];
            var playerGuid = new byte[8];

            packet.ReadInt32("Slot");
            packet.ReadInt32("Duration");

            itemGuid[5] = packet.ReadBit();
            itemGuid[0] = packet.ReadBit();
            playerGuid[7] = packet.ReadBit();
            playerGuid[4] = packet.ReadBit();
            itemGuid[4] = packet.ReadBit();
            itemGuid[3] = packet.ReadBit();
            playerGuid[3] = packet.ReadBit();
            playerGuid[0] = packet.ReadBit();
            playerGuid[5] = packet.ReadBit();
            itemGuid[2] = packet.ReadBit();
            playerGuid[1] = packet.ReadBit();
            itemGuid[7] = packet.ReadBit();
            playerGuid[6] = packet.ReadBit();
            itemGuid[6] = packet.ReadBit();
            playerGuid[2] = packet.ReadBit();
            itemGuid[1] = packet.ReadBit();

            packet.ReadXORByte(itemGuid, 1);
            packet.ReadXORByte(playerGuid, 3);
            packet.ReadXORByte(itemGuid, 0);
            packet.ReadXORByte(itemGuid, 6);
            packet.ReadXORByte(playerGuid, 6);
            packet.ReadXORByte(playerGuid, 1);
            packet.ReadXORByte(playerGuid, 7);
            packet.ReadXORByte(itemGuid, 3);
            packet.ReadXORByte(playerGuid, 0);
            packet.ReadXORByte(itemGuid, 2);
            packet.ReadXORByte(itemGuid, 4);
            packet.ReadXORByte(itemGuid, 5);
            packet.ReadXORByte(playerGuid, 2);
            packet.ReadXORByte(itemGuid, 7);
            packet.ReadXORByte(playerGuid, 5);
            packet.ReadXORByte(playerGuid, 4);

            packet.WriteGuid("Player GUID", playerGuid);
            packet.WriteGuid("Item GUID", itemGuid);
        }

        [Parser(Opcode.CMSG_REFORGE_ITEM)]
        public static void HandleItemSendReforge(Packet packet)
        {
            packet.ReadInt32("Bag");
            packet.ReadInt32("Reforge Entry");
            packet.ReadInt32("Slot");

            var guid = packet.StartBitStream(3, 5, 4, 6, 1, 0, 7, 2);
            packet.ParseBitStream(guid, 2, 0, 6, 4, 3, 5, 1, 7);
            packet.WriteGuid("Reforger Guid", guid);

        }

        [Parser(Opcode.SMSG_REFORGE_RESULT)]
        public static void HandleItemReforgeResult(Packet packet)
        {
            packet.ReadBit("Successful");
        }

        [Parser(Opcode.CMSG_ITEM_UPGRADE)]
        public static void HandleItemSendUpgrade(Packet packet)
        {
            var itemGUID = new byte[8];
            var npcGUID = new byte[8];

            packet.ReadInt32("Bag");
            packet.ReadInt32("Slot");
            packet.ReadInt32("Reforge Entry");

            itemGUID[7] = packet.ReadBit();
            itemGUID[4] = packet.ReadBit();
            npcGUID[3] = packet.ReadBit();
            itemGUID[0] = packet.ReadBit();
            npcGUID[5] = packet.ReadBit();
            npcGUID[0] = packet.ReadBit();
            itemGUID[1] = packet.ReadBit();
            itemGUID[2] = packet.ReadBit();
            npcGUID[2] = packet.ReadBit();
            itemGUID[3] = packet.ReadBit();
            npcGUID[4] = packet.ReadBit();
            npcGUID[6] = packet.ReadBit();
            itemGUID[5] = packet.ReadBit();
            npcGUID[7] = packet.ReadBit();
            npcGUID[1] = packet.ReadBit();
            itemGUID[6] = packet.ReadBit();

            packet.ReadXORByte(itemGUID, 6);
            packet.ReadXORByte(itemGUID, 1);
            packet.ReadXORByte(npcGUID, 7);
            packet.ReadXORByte(itemGUID, 5);
            packet.ReadXORByte(itemGUID, 4);
            packet.ReadXORByte(npcGUID, 6);
            packet.ReadXORByte(itemGUID, 0);
            packet.ReadXORByte(npcGUID, 3);
            packet.ReadXORByte(itemGUID, 7);
            packet.ReadXORByte(npcGUID, 2);
            packet.ReadXORByte(npcGUID, 4);
            packet.ReadXORByte(npcGUID, 5);
            packet.ReadXORByte(itemGUID, 3);
            packet.ReadXORByte(npcGUID, 1);
            packet.ReadXORByte(npcGUID, 0);
            packet.ReadXORByte(itemGUID, 2);

            packet.WriteGuid("Item GUID", itemGUID);
            packet.WriteGuid("NPC GUID", npcGUID);
        }

        [Parser(Opcode.SMSG_ITEM_UPGRADE_RESULT)]
        public static void HandleItemUpgradeResult(Packet packet)
        {
            packet.ReadBit("Successful");
        }

        [Parser(Opcode.CMSG_ITEM_REFUND_INFO)]
        public static void HandleItemRefundInfo(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 0, 4, 6, 3, 2, 1, 7, 5);
            packet.ParseBitStream(guid, 5, 3, 7, 2, 1, 6, 0, 4);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_ITEM_PUSH_RESULT)]
        public static void HandleItemPushResult(Packet packet)
        {
            var playerGUID = new byte[8];
            var guid2 = new byte[8];

            packet.ReadBit("Result in Combatlog");
            packet.ReadBit("Created");
            playerGUID[2] = packet.ReadBit();
            playerGUID[0] = packet.ReadBit();
            playerGUID[4] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            packet.ReadBit("bit24");
            playerGUID[5] = packet.ReadBit();
            playerGUID[1] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            playerGUID[6] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            playerGUID[7] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            playerGUID[3] = packet.ReadBit();

            packet.ReadBit("From NPC");
            packet.ReadXORByte(guid2, 6);
            packet.ReadInt32("Suffix Factor");
            packet.ReadXORByte(playerGUID, 1);
            packet.ReadInt32("Int14");
            packet.ReadUInt32("Count");
            packet.ReadInt32("Int54");
            packet.ReadInt32("Random Property ID");
            packet.ReadXORByte(playerGUID, 3);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(playerGUID, 5);
            packet.ReadUInt32("Unk Uint32");
            packet.ReadXORByte(playerGUID, 2);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(playerGUID, 7);
            packet.ReadByte("Slot");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");
            packet.ReadInt32("Int40");
            packet.ReadXORByte(playerGUID, 0);
            packet.ReadXORByte(playerGUID, 4);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 2);
            packet.ReadUInt32("Count of Items in inventory");
            packet.ReadInt32("Item Slot");
            packet.ReadXORByte(playerGUID, 6);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 4);

            packet.WriteGuid("Player GUID", playerGUID);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.CMSG_AUTOSTORE_LOOT_ITEM)]
        public static void HandleAutoStoreLootItem510(Packet packet)
        {
            var counter = packet.ReadBits("Count", 23);

            var guid = new byte[counter][];

            for (var i = 0; i < counter; ++i)
            {
                guid[i] = new byte[8];
                packet.StartBitStream(guid[i], 2, 1, 5, 7, 4, 3, 0, 6);
            }

            packet.ResetBitReader();

            for (var i = 0; i < counter; ++i)
            {
                packet.ReadXORByte(guid[i], 0);
                packet.ReadXORByte(guid[i], 3);
                packet.ReadByte("Slot", i);
                packet.ReadXORByte(guid[i], 7);
                packet.ReadXORByte(guid[i], 2);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadXORByte(guid[i], 1);
                packet.ReadXORByte(guid[i], 6);
                packet.ReadXORByte(guid[i], 5);

                packet.WriteGuid("Looter GUID", guid[i], i);
            }
        }
    }
}

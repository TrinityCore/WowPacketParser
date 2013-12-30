using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_2_17688.Parsers
{
    public static class NpcHandler
    {
        [Parser(Opcode.SMSG_LIST_INVENTORY)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            var npcVendor = new NpcVendor();
            var itemCount = packet.ReadBits("Item Count", 18);

            var guid = new byte[8];
            packet.StartBitStream(guid, 0);

            var hasExtendedCost = new bool[itemCount];
            var hasCondition = new bool[itemCount];
            for (int i = 0; i < itemCount; ++i)
            {
                packet.ReadBit("Unk bit", i);
                hasExtendedCost[i] = !packet.ReadBit();
                hasCondition[i] = !packet.ReadBit();
            }

            packet.StartBitStream(guid, 3, 7, 6, 5, 2, 1, 4);
            packet.ResetBitReader();
            packet.ReadXORBytes(guid, 7, 6);

            npcVendor.VendorItems = new List<VendorItem>((int)itemCount);
            for (int i = 0; i < itemCount; ++i)
            {
                var vendorItem = new VendorItem();

                vendorItem.Type = packet.ReadUInt32("Type", i);              // 1 - item, 2 - currency

                var buyCount = packet.ReadUInt32("Buy Count", i);
                var maxCount = packet.ReadInt32("Max Count", i);

                packet.ReadInt32("Display ID", i);

                vendorItem.Slot = packet.ReadUInt32("Item Position", i);

                packet.ReadInt32("Item Upgrade ID", i);
                packet.ReadInt32("Price", i);

                if (hasExtendedCost[i])
                    vendorItem.ExtendedCostId = packet.ReadUInt32("Extended Cost", i);

                packet.ReadInt32("Max Durability", i);

                vendorItem.ItemId = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Item ID", i);

                if (hasCondition[i])
                    packet.ReadInt32("Condition ID", i);

                vendorItem.MaxCount = maxCount == -1 ? 0 : maxCount;        // TDB
                if (vendorItem.Type == 2)
                    vendorItem.MaxCount = (int)buyCount;

                npcVendor.VendorItems.Add(vendorItem);
            }

            packet.ReadByte("Unk Byte");
            packet.ReadXORBytes(guid, 2, 3, 5, 1, 0, 4);

            packet.WriteGuid("GUID", guid);
            var GUID = new Guid(BitConverter.ToUInt64(guid, 0));

            Storage.NpcVendors.Add(GUID.GetEntry(), npcVendor, packet.TimeSpan);
        }
    }
}

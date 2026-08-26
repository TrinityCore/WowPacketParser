using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class NpcHandler
    {
        [Parser(Opcode.SMSG_VENDOR_INVENTORY, ClientVersionBuild.V12_1_0_69214)]
        public static void HandleVendorInventory(Packet packet)
        {
            var entry = packet.ReadPackedGuid128("VendorGUID").GetEntry();
            packet.ReadInt32("Reason");
            var count = packet.ReadUInt32("VendorItems");

            for (var i = 0; i < count; ++i)
            {
                packet.ReadUInt64("Price", i);
                var vendor = new NpcVendor
                {
                    Entry = entry,
                    Slot = (int)packet.ReadUInt32("MuID", i),
                    Type = (uint)packet.ReadInt32("Type", i)
                };

                var buyCount = packet.ReadInt32("StackCount", i);
                var maxCount = packet.ReadInt32("Quantity", i);
                vendor.ExtendedCost = (uint)packet.ReadInt32("ExtendedCostID", i);
                vendor.PlayerConditionID = (uint)packet.ReadInt32("PlayerConditionFailed", i);
                vendor.Item = Substructures.ItemHandler.ReadItemInstance(packet, i).ItemID;
                packet.ResetBitReader();
                packet.ReadBit("Locked", i);
                vendor.IgnoreFiltering = packet.ReadBit("DoNotFilterOnVendor", i);
                packet.ReadBit("Refundable", i);
                packet.ResetBitReader();


                vendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                if (vendor.Type == 2)
                    vendor.MaxCount = (uint)buyCount;

                Storage.NpcVendors.Add(vendor, packet.TimeSpan);
            }

            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
        }
    }
}

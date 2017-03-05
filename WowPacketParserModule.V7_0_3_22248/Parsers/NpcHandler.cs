using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class NpcHandler
    {
        public static void ReadGossipQuestTextData(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("QuestID", idx);
            packet.Translator.ReadInt32("QuestType", idx);
            packet.Translator.ReadInt32("QuestLevel", idx);

            for (int j = 0; j < 2; ++j)
                packet.Translator.ReadInt32("QuestFlags", idx, j);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("Repeatable");
            packet.Translator.ReadBit("Ignored");

            uint questTitleLen = packet.Translator.ReadBits(9);

            packet.Translator.ReadWoWString("QuestTitle", questTitleLen, idx);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            GossipMenu gossip = new GossipMenu();

            WowGuid guid = packet.Translator.ReadPackedGuid128("GossipGUID");

            gossip.ObjectType = guid.GetObjectType();
            gossip.ObjectEntry = guid.GetEntry();

            int menuId = packet.Translator.ReadInt32("GossipID");
            gossip.Entry = (uint)menuId;

            packet.Translator.ReadInt32("FriendshipFactionID");

            gossip.TextID = (uint)packet.Translator.ReadInt32("TextID");

            int int44 = packet.Translator.ReadInt32("GossipOptions");
            int int60 = packet.Translator.ReadInt32("GossipText");

            for (int i = 0; i < int44; ++i)
                V6_0_2_19033.Parsers.NpcHandler.ReadGossipOptionsData((uint)menuId, packet, i, "GossipOptions");

            for (int i = 0; i < int60; ++i)
                ReadGossipQuestTextData(packet, i, "GossipQuestText");

            if (guid.GetObjectType() == ObjectType.Unit)
                if (Storage.Objects.ContainsKey(guid))
                    ((Unit)Storage.Objects[guid].Item1).GossipId = (uint)menuId;

            Storage.Gossips.Add(gossip, packet.TimeSpan);

            packet.AddSniffData(StoreNameType.Gossip, menuId, guid.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY)]
        public static void HandleVendorInventory(Packet packet)
        {
            uint entry = packet.Translator.ReadPackedGuid128("VendorGUID").GetEntry();
            packet.Translator.ReadByte("Reason");
            int count = packet.Translator.ReadInt32("VendorItems");

            for (int i = 0; i < count; ++i)
            {
                NpcVendor vendor = new NpcVendor
                {
                    Entry = entry,
                    Slot = packet.Translator.ReadInt32("Muid", i),
                    Type = (uint)packet.Translator.ReadInt32("Type", i)
                };

                int maxCount = packet.Translator.ReadInt32("Quantity", i);
                packet.Translator.ReadInt64("Price", i);
                packet.Translator.ReadInt32("Durability", i);
                int buyCount = packet.Translator.ReadInt32("StackCount", i);
                vendor.ExtendedCost = packet.Translator.ReadUInt32("ExtendedCostID", i);
                vendor.PlayerConditionID = packet.Translator.ReadUInt32("PlayerConditionFailed", i);

                vendor.Item = V6_0_2_19033.Parsers.ItemHandler.ReadItemInstance(packet, i);
                vendor.IgnoreFiltering = packet.Translator.ReadBit("DoNotFilterOnVendor", i);

                vendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                if (vendor.Type == 2)
                    vendor.MaxCount = (uint)buyCount;

                Storage.NpcVendors.Add(vendor, packet.TimeSpan);
            }
        }
    }
}

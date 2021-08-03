using System;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WoWPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class NpcHandler
    {
        public static GossipQuestOption ReadGossipQuestTextData(Packet packet, params object[] idx)
        {
            var gossipQuest = new GossipQuestOption();
            gossipQuest.QuestId = (uint)packet.ReadInt32("QuestID", idx);
            if (ClientVersion.AddedInVersion(ClientType.Shadowlands))
                packet.ReadInt32("ContentTuningID", idx);

            packet.ReadInt32("QuestType", idx);
            if (ClientVersion.RemovedInVersion(ClientType.Shadowlands))
            {
                packet.ReadInt32("QuestLevel", idx);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_5_25848))
                    packet.ReadInt32("QuestMaxScalingLevel", idx);
            }

            for (int j = 0; j < 2; ++j)
                packet.ReadInt32("QuestFlags", idx, j);

            packet.ResetBitReader();

            packet.ReadBit("Repeatable", idx);
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V7_2_0_23826))
                packet.ReadBit("Ignored");

            uint questTitleLen = packet.ReadBits(9);

            gossipQuest.Title = packet.ReadWoWString("QuestTitle", questTitleLen, idx);

            return gossipQuest;
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            PacketGossipMessage packetGossip = packet.Holder.GossipMessage = new();
            GossipMenu gossip = new GossipMenu();

            WowGuid guid = packet.ReadPackedGuid128("GossipGUID");
            packetGossip.GossipSource = guid;

            gossip.ObjectType = guid.GetObjectType();
            gossip.ObjectEntry = guid.GetEntry();

            int menuId = packet.ReadInt32("GossipID");
            gossip.Entry = packetGossip.MenuId = (uint)menuId;

            packet.ReadInt32("FriendshipFactionID");

            gossip.TextID = packetGossip.TextId = (uint)packet.ReadInt32("TextID");

            int int44 = packet.ReadInt32("GossipOptions");
            int int60 = packet.ReadInt32("GossipText");

            for (int i = 0; i < int44; ++i)
                packetGossip.Options.Add(V6_0_2_19033.Parsers.NpcHandler.ReadGossipOptionsData((uint)menuId, packet, i, "GossipOptions"));

            for (int i = 0; i < int60; ++i)
                packetGossip.Quests.Add(ReadGossipQuestTextData(packet, i, "GossipQuestText"));

            if (guid.GetObjectType() == ObjectType.Unit)
                if (Storage.Objects.ContainsKey(guid))
                    ((Unit)Storage.Objects[guid].Item1).GossipId = (uint)menuId;

            Storage.Gossips.Add(gossip, packet.TimeSpan);
            var lastGossipOption = CoreParsers.NpcHandler.LastGossipOption;
            var tempGossipOptionPOI = CoreParsers.NpcHandler.TempGossipOptionPOI;

            if (lastGossipOption.HasSelection)
            {
                if ((packet.TimeSpan - lastGossipOption.TimeSpan).Duration() <= TimeSpan.FromMilliseconds(2500))
                {
                    Storage.GossipMenuOptionActions.Add(new GossipMenuOptionAction { MenuId = lastGossipOption.MenuId, OptionIndex = lastGossipOption.OptionIndex, ActionMenuId = gossip.Entry, ActionPoiId = lastGossipOption.ActionPoiId }, packet.TimeSpan);

                    //keep temp data (for case SMSG_GOSSIP_POI is delayed)
                    tempGossipOptionPOI.Guid = lastGossipOption.Guid;
                    tempGossipOptionPOI.MenuId = lastGossipOption.MenuId;
                    tempGossipOptionPOI.OptionIndex = lastGossipOption.OptionIndex;
                    tempGossipOptionPOI.ActionMenuId = gossip.Entry;
                    tempGossipOptionPOI.TimeSpan = lastGossipOption.TimeSpan;

                    // clear lastgossip so no faulty linkings appear
                    lastGossipOption.Reset();
                }
                else
                {
                    lastGossipOption.Reset();
                    tempGossipOptionPOI.Reset();

                }
            }

            packet.AddSniffData(StoreNameType.Gossip, menuId, guid.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY)]
        public static void HandleVendorInventory(Packet packet)
        {
            uint entry = packet.ReadPackedGuid128("VendorGUID").GetEntry();
            packet.ReadByte("Reason");
            int count = packet.ReadInt32("VendorItems");

            for (int i = 0; i < count; ++i)
            {
                NpcVendor vendor = new NpcVendor
                {
                    Entry = entry,
                    Slot = packet.ReadInt32("Muid", i),
                    Type = (uint)packet.ReadInt32("Type", i)
                };

                int maxCount = packet.ReadInt32("Quantity", i);
                packet.ReadInt64("Price", i);
                packet.ReadInt32("Durability", i);
                int buyCount = packet.ReadInt32("StackCount", i);
                vendor.ExtendedCost = packet.ReadUInt32("ExtendedCostID", i);
                vendor.PlayerConditionID = packet.ReadUInt32("PlayerConditionFailed", i);

                vendor.Item = Substructures.ItemHandler.ReadItemInstance(packet, i).ItemID;
                vendor.IgnoreFiltering = packet.ReadBit("DoNotFilterOnVendor", i);

                vendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                if (vendor.Type == 2)
                    vendor.MaxCount = (uint)buyCount;

                Storage.NpcVendors.Add(vendor, packet.TimeSpan);
            }

            var lastGossipOption = CoreParsers.NpcHandler.LastGossipOption;
            var tempGossipOptionPOI = CoreParsers.NpcHandler.TempGossipOptionPOI;

            lastGossipOption.Guid = null;
            lastGossipOption.MenuId = null;
            lastGossipOption.OptionIndex = null;
            lastGossipOption.ActionMenuId = null;
            lastGossipOption.ActionPoiId = null;

            tempGossipOptionPOI.Guid = null;
            tempGossipOptionPOI.MenuId = null;
            tempGossipOptionPOI.OptionIndex = null;
            tempGossipOptionPOI.ActionMenuId = null;
            tempGossipOptionPOI.ActionPoiId = null;
        }
    }
}

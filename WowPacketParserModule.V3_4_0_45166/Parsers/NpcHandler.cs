using System;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class NpcHandler
    {
        public static GossipMessageOption ReadGossipOptionsData(uint menuId, WowGuid npcGuid, Packet packet, params object[] idx)
        {
            GossipMessageOption gossipMessageOption = new();
            GossipMenuOption gossipOption = new GossipMenuOption
            {
                MenuID = menuId
            };

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_4_1_47014))
                gossipOption.OptionID = gossipMessageOption.OptionIndex = (uint)packet.ReadInt32("OptionID", idx);
            else
                gossipOption.GossipOptionID = packet.ReadInt32("GossipOptionID", idx);
            gossipOption.OptionNpc = (GossipOptionNpc?)packet.ReadByte("OptionNPC", idx);
            gossipMessageOption.OptionNpc = (int)gossipOption.OptionNpc;
            gossipOption.BoxCoded = gossipMessageOption.BoxCoded = packet.ReadByte("OptionFlags", idx) != 0;
            gossipOption.BoxMoney = gossipMessageOption.BoxCost = (uint)packet.ReadInt32("OptionCost", idx);
            gossipOption.Language = packet.ReadUInt32E<Language>("Language", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
            {
                gossipOption.Flags = packet.ReadInt32("Flags", idx);
                gossipOption.OptionID = gossipMessageOption.OptionIndex = (uint)packet.ReadInt32("OrderIndex", idx);
            }

            packet.ResetBitReader();
            uint textLen = packet.ReadBits(12);
            uint confirmLen = packet.ReadBits(12);
            bool hasSpellId = false;
            bool hasOverrideIconId = false;
            packet.ReadBits("Status", 2, idx);
            hasSpellId = packet.ReadBit();
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                hasOverrideIconId = packet.ReadBit();

            uint rewardsCount = packet.ReadUInt32();
            for (uint i = 0; i < rewardsCount; ++i)
            {
                packet.ResetBitReader();
                packet.ReadBits("Type", 1, idx, "TreasureItem", i);
                packet.ReadInt32("ID", idx, "TreasureItem", i);
                packet.ReadInt32("Quantity", idx, "TreasureItem", i);
            }

            gossipOption.OptionText = gossipMessageOption.Text = packet.ReadWoWString("Text", textLen, idx);
            gossipMessageOption.BoxText = packet.ReadWoWString("Confirm", confirmLen, idx);

            if (!string.IsNullOrEmpty(gossipMessageOption.BoxText))
                gossipOption.BoxText = gossipMessageOption.BoxText;

            if (hasSpellId)
                gossipOption.SpellID = packet.ReadInt32("SpellID", idx);

            if (hasOverrideIconId)
                gossipOption.OverrideIconID = packet.ReadInt32("OverrideIconID", idx);

            gossipOption.FillBroadcastTextIDs();

            if (Settings.TargetedDatabase < TargetedDatabase.Shadowlands)
                gossipOption.FillOptionType(npcGuid);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                Storage.GossipOptionIdToOrderIndexMap.Add((gossipOption.MenuID.GetValueOrDefault(), gossipOption.GossipOptionID.GetValueOrDefault()), gossipOption.OptionID.GetValueOrDefault());
            Storage.GossipMenuOptions.Add((gossipOption.MenuID, gossipOption.OptionID), gossipOption, packet.TimeSpan);

            return gossipMessageOption;
        }

        public static GossipQuestOption ReadGossipQuestTextData(Packet packet, params object[] idx)
        {
            var gossipQuest = new GossipQuestOption();
            gossipQuest.QuestId = (uint)packet.ReadInt32("QuestID", idx);
            packet.ReadInt32("ContentTuningID", idx);
            packet.ReadInt32("QuestType", idx);
            packet.ReadInt32("QuestLevel", idx);
            packet.ReadInt32("QuestMaxScalingLevel", idx);

            for (int j = 0; j < 2; ++j)
                packet.ReadInt32("QuestFlags", idx, j);

            packet.ResetBitReader();

            packet.ReadBit("Repeatable", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51505)) // assumption
                packet.ReadBit("Important", idx);

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
            gossip.MenuID = packetGossip.MenuId = (uint)menuId;

            int friendshipFactionID = packet.ReadInt32("FriendshipFactionID");
            CoreParsers.NpcHandler.AddGossipAddon(packetGossip.MenuId, friendshipFactionID, guid, packet.TimeSpan);

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_4_1_47014))
                gossip.TextID = packetGossip.TextId = (uint)packet.ReadInt32("TextID");

            int optionsCount = packet.ReadInt32("GossipOptionsCount");
            int questsCount = packet.ReadInt32("GossipQuestsCount");

            bool hasBroadcastTextID = false;
            bool hasBroadcastTextID2 = false;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
            {
                hasBroadcastTextID = packet.ReadBit("HasBroadcastTextID");
                hasBroadcastTextID2 = packet.ReadBit("HasBroadcastTextID2");
            }

            for (int i = 0; i < optionsCount; ++i)
                packetGossip.Options.Add(ReadGossipOptionsData((uint)menuId, guid, packet, i, "GossipOptions"));

            if (hasBroadcastTextID)
                gossip.TextID = (uint)packet.ReadInt32("BroadcastTextID");

            if (hasBroadcastTextID2)
                gossip.TextID = (uint)packet.ReadInt32("BroadcastTextID2");

            for (int i = 0; i < questsCount; ++i)
                packetGossip.Quests.Add(ReadGossipQuestTextData(packet, i, "GossipQuests"));

            if (guid.GetObjectType() == ObjectType.Unit)
            {
                CreatureTemplateGossip creatureTemplateGossip = new()
                {
                    CreatureID = guid.GetEntry(),
                    MenuID = (uint)menuId
                };
                Storage.CreatureTemplateGossips.Add(creatureTemplateGossip);
                Storage.CreatureDefaultGossips.Add(guid.GetEntry(), (uint)menuId);
            }

            Storage.Gossips.Add(gossip, packet.TimeSpan);
            CoreParsers.NpcHandler.UpdateLastGossipOptionActionMessage(packet.TimeSpan, gossip.MenuID);

            packet.AddSniffData(StoreNameType.Gossip, menuId, guid.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY, ClientVersionBuild.V3_4_3_51505)]
        public static void HandleVendorInventory(Packet packet)
        {
            uint entry = packet.ReadPackedGuid128("VendorGUID").GetEntry();
            packet.ReadByte("Reason");
            int count = packet.ReadInt32("VendorItems");

            for (int i = 0; i < count; ++i)
            {
                packet.ReadInt64("Price", i);

                NpcVendor vendor = new NpcVendor
                {
                    Entry = entry,
                    Slot = packet.ReadInt32("Muid", i),
                    Type = (uint)packet.ReadInt32("Type", i)
                };

                packet.ReadInt32("Durability", i);
                int buyCount = packet.ReadInt32("StackCount", i);
                int maxCount = packet.ReadInt32("Quantity", i);
                vendor.ExtendedCost = packet.ReadUInt32("ExtendedCostID", i);
                vendor.PlayerConditionID = packet.ReadUInt32("PlayerConditionFailed", i);
                packet.ReadBit("Locked", i);
                vendor.IgnoreFiltering = packet.ReadBit("DoNotFilterOnVendor", i);
                packet.ReadBit("Refundable", i);

                vendor.Item = Substructures.ItemHandler.ReadItemInstance(packet, i).ItemID;

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

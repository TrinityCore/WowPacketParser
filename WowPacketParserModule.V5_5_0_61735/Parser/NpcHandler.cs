using System;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.SQL;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class NpcHandler
    {
        public static void AddBroadcastTextToGossip(uint menuID, uint broadcastTextID, WowGuid guid)
        {
            NpcText925 npcText = null;
            if (!Storage.GossipToNpcTextMap.TryGetValue(menuID, out npcText))
            {
                npcText = new NpcText925();
                npcText.ObjectType = guid.GetObjectType();
                npcText.ObjectEntry = guid.GetEntry();
                Storage.GossipToNpcTextMap.Add(menuID, npcText);
            }
            npcText.AddBroadcastTextIfNotExists(broadcastTextID, 1.0f);
        }

        public static GossipMessageOption ReadGossipOptionsData(uint menuId, WowGuid npcGuid, Packet packet, params object[] idx)
        {
            GossipMessageOption gossipMessageOption = new();
            GossipMenuOption gossipOption = new GossipMenuOption
            {
                MenuID = menuId
            };

            gossipOption.GossipOptionID = packet.ReadInt32("GossipOptionID", idx);
            gossipOption.OptionNpc = (GossipOptionNpc?)packet.ReadByte("OptionNPC", idx);
            gossipMessageOption.OptionNpc = (int)gossipOption.OptionNpc;
            gossipOption.BoxCoded = gossipMessageOption.BoxCoded = packet.ReadByte("OptionFlags", idx) != 0;
            gossipOption.BoxMoney = packet.ReadUInt64("OptionCost", idx);
            gossipMessageOption.BoxCost = (uint)gossipOption.BoxMoney;
            gossipOption.Language = packet.ReadUInt32E<Language>("Language", idx);
            gossipOption.Flags = packet.ReadInt32("Flags", idx);
            gossipOption.OptionID = gossipMessageOption.OptionIndex = (uint)packet.ReadInt32("OrderIndex", idx);

            packet.ResetBitReader();
            uint textLen = packet.ReadBits(12);
            uint confirmLen = packet.ReadBits(12);
            packet.ReadBits("Status", 2, idx);
            bool hasSpellId = packet.ReadBit();
            bool hasOverrideIconId = packet.ReadBit();
            uint failureDescriptionLength = packet.ReadBits(8);

            uint rewardsCount = packet.ReadUInt32();
            for (uint i = 0; i < rewardsCount; ++i)
            {
                packet.ResetBitReader();
                packet.ReadBits("Type", 1, idx, "TreasureItem", i);
                packet.ReadInt32("ID", idx, "TreasureItem", i);
                packet.ReadInt32("Quantity", idx, "TreasureItem", i);
                packet.ReadByte("ItemContext", idx, "TreasureItem", i);
            }

            gossipOption.OptionText = gossipMessageOption.Text = packet.ReadWoWString("Text", textLen, idx);
            gossipMessageOption.BoxText = packet.ReadWoWString("Confirm", confirmLen, idx);

            if (!string.IsNullOrEmpty(gossipMessageOption.BoxText))
                gossipOption.BoxText = gossipMessageOption.BoxText;

            if (hasSpellId)
                gossipOption.SpellID = packet.ReadInt32("SpellID", idx);

            if (hasOverrideIconId)
                gossipOption.OverrideIconID = packet.ReadInt32("OverrideIconID", idx);

            if (failureDescriptionLength > 1)
                packet.ReadDynamicString("FailureDescription", failureDescriptionLength);

            gossipOption.FillBroadcastTextIDs();

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

            packet.ReadInt32("Unused1102", idx);
            packet.ReadInt32E<QuestFlags>("Flags", idx);
            packet.ReadInt32E<QuestFlagsEx>("FlagsEx", idx);
            packet.ReadInt32E<QuestFlagsEx2>("FlagsEx2_Unused550", idx);
            packet.ReadInt32("FlagsEx3_Unused550", idx);

            packet.ResetBitReader();

            packet.ReadBit("Repeatable", idx);
            packet.ReadBit("ResetByScheduler", idx);
            packet.ReadBit("Important", idx);
            packet.ReadBit("Meta", idx);

            uint questTitleLen = packet.ReadBits(9);
            gossipQuest.Title = packet.ReadWoWString("QuestTitle", questTitleLen, idx);

            return gossipQuest;
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            PacketGossipMessage packetGossip = packet.Holder.GossipMessage = new();

            WowGuid guid = packet.ReadPackedGuid128("GossipGUID");
            packetGossip.GossipSource = guid;

            int menuId = packet.ReadInt32("GossipID");
            packetGossip.MenuId = (uint)menuId;

            packet.ReadInt32("LfgDungeonsID");

            int friendshipFactionID = packet.ReadInt32("FriendshipFactionID");
            CoreParsers.NpcHandler.AddGossipAddon(packetGossip.MenuId, friendshipFactionID, 0, guid, packet.TimeSpan);

            uint broadcastTextID = 0;
            uint npcTextID = 0;
            int optionsCount = packet.ReadInt32("GossipOptionsCount");
            int questsCount = packet.ReadInt32("GossipQuestsCount");

            bool hasTextID = false;
            bool hasBroadcastTextID = false;
            hasTextID = packet.ReadBit("HasTextID");
            hasBroadcastTextID = packet.ReadBit("HasBroadcastTextID");

            for (int i = 0; i < optionsCount; ++i)
                packetGossip.Options.Add(ReadGossipOptionsData((uint)menuId, guid, packet, i, "GossipOptions"));

            if (hasTextID)
                npcTextID = (uint)packet.ReadInt32("TextID");

            if (hasBroadcastTextID)
                broadcastTextID = (uint)packet.ReadInt32("BroadcastTextID");

            if (!hasTextID && hasBroadcastTextID)
                npcTextID = SQLDatabase.GetNPCTextIDByMenuIDAndBroadcastText(menuId, broadcastTextID);

            if (npcTextID != 0)
            {
                GossipMenu gossip = new();
                gossip.MenuID = packetGossip.MenuId;
                gossip.TextID = packetGossip.TextId = npcTextID;
                gossip.ObjectType = guid.GetObjectType();
                gossip.ObjectEntry = guid.GetEntry();
                Storage.Gossips.Add(gossip, packet.TimeSpan);
            }
            else if (hasBroadcastTextID)
                AddBroadcastTextToGossip(packetGossip.MenuId, broadcastTextID, guid);

            for (int i = 0; i < questsCount; ++i)
                packetGossip.Quests.Add(ReadGossipQuestTextData(packet, i, "GossipQuests"));

            if (guid.GetObjectType() == ObjectType.Unit && !CoreParsers.NpcHandler.HasLastGossipOption(packet.TimeSpan, (uint)menuId))
            {
                CreatureTemplateGossip creatureTemplateGossip = new()
                {
                    CreatureID = guid.GetEntry(),
                    MenuID = (uint)menuId
                };
                Storage.CreatureTemplateGossips.Add(creatureTemplateGossip);
                Storage.CreatureDefaultGossips.Add(guid.GetEntry(), (uint)menuId);
            }

            CoreParsers.NpcHandler.UpdateLastGossipOptionActionMessage(packet.TimeSpan, (uint)menuId);

            packet.AddSniffData(StoreNameType.Gossip, menuId, guid.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.SMSG_GOSSIP_COMPLETE)]
        public static void HandleGossipComplete(Packet packet)
        {
            packet.ReadBit("SuppressSound");
        }

        [Parser(Opcode.SMSG_GOSSIP_OPTION_NPC_INTERACTION)]
        public static void HandleGossipOptionNPCInteraction(Packet packet)
        {
            packet.ReadPackedGuid128("GossipGUID");
            var gossipNpcOptionID = packet.ReadInt32("GossipNpcOptionID");
            var hasFriendshipFactionID = packet.ReadBit();

            if (hasFriendshipFactionID)
                packet.ReadInt32("FriendshipFactionID");

            CoreParsers.NpcHandler.AddGossipNpcOption(gossipNpcOptionID, packet.TimeSpan, true);
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY)]
        public static void HandleVendorInventory(Packet packet)
        {
            uint entry = packet.ReadPackedGuid128("VendorGUID").GetEntry();
            packet.ReadInt32("Reason");

            int count = packet.ReadInt32("VendorItems");

            for (int i = 0; i < count; ++i)
            {
                NpcVendor vendor = new NpcVendor
                {
                    Entry = entry,
                };

                packet.ReadInt64("Price", i);
                vendor.Slot = packet.ReadInt32("Muid", i);
                vendor.Type = (uint)packet.ReadInt32("Type", i);
                int buyCount = packet.ReadInt32("StackCount", i);
                int maxCount = packet.ReadInt32("Quantity", i);
                vendor.ExtendedCost = packet.ReadUInt32("ExtendedCostID", i);
                vendor.PlayerConditionID = packet.ReadUInt32("PlayerConditionFailed", i);

                packet.ResetBitReader();
                packet.ReadBit("Locked", i);
                vendor.IgnoreFiltering = packet.ReadBit("DoNotFilterOnVendor", i);
                packet.ReadBit("Refundable", i);

                packet.ResetBitReader();
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

using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.SQL;
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

        public static void ReadTreasureLootList(Packet packet, params object[] indexes)
        {
            var rewardsCount = packet.ReadUInt32();
            for (var i = 0u; i < rewardsCount; ++i)
                V6_0_2_19033.Parsers.NpcHandler.ReadTreasureItem(packet, indexes, "Items", i);
        }

        public static GossipMessageOption ReadGossipOptionsData(uint menuId, WowGuid npcGuid, Packet packet, params object[] idx)
        {
            var gossipOption = new GossipMenuOption
            {
                MenuID = menuId
            };

            gossipOption.GossipOptionID = packet.ReadInt32("GossipOptionID", idx);
            gossipOption.OptionNpc = packet.ReadUInt32E<GossipOptionNpc>("OptionNPC", idx);
            gossipOption.BoxCoded = packet.ReadByte("OptionFlags", idx) != 0;
            gossipOption.BoxMoney = packet.ReadUInt64("OptionCost", idx);
            gossipOption.Language = packet.ReadUInt32E<Language>("Language", idx);
            ReadTreasureLootList(packet, idx, "Treasure");
            gossipOption.Flags = packet.ReadInt32("Flags", idx);
            gossipOption.OptionID = (uint)packet.ReadInt32("OrderIndex", idx);

            packet.ResetBitReader();
            var textLen = packet.ReadBits(12);
            var confirmLen = packet.ReadBits(12);
            packet.ReadBits("Status", 2, idx);
            var hasSpellId = packet.ReadBit();
            var hasOverrideIconId = packet.ReadBit();
            var failureDescriptionLength = packet.ReadBits(8);

            gossipOption.OptionText = packet.ReadWoWString("Text", textLen, idx);
            gossipOption.BoxText = packet.ReadWoWString("Confirm", confirmLen, idx);

            if (hasSpellId)
                gossipOption.SpellID = packet.ReadInt32("SpellID", idx);

            if (hasOverrideIconId)
                gossipOption.OverrideIconID = packet.ReadInt32("OverrideIconID", idx);

            packet.ReadDynamicString("FailureDescription", failureDescriptionLength, idx);

            gossipOption.FillBroadcastTextIDs();

            if (Settings.TargetedDatabase < TargetedDatabase.Shadowlands)
                gossipOption.FillOptionType(npcGuid);

            Storage.GossipOptionIdToOrderIndexMap.Add((gossipOption.MenuID.GetValueOrDefault(), gossipOption.GossipOptionID.GetValueOrDefault()), gossipOption.OptionID.GetValueOrDefault());
            Storage.GossipMenuOptions.Add((gossipOption.MenuID, gossipOption.OptionID), gossipOption, packet.TimeSpan);

            return new GossipMessageOption
            {
                OptionNpc = (int)gossipOption.OptionNpc,
                BoxCoded = gossipOption.BoxCoded.GetValueOrDefault(),
                BoxCost = (uint)gossipOption.BoxMoney,
                OptionIndex = gossipOption.OptionID.GetValueOrDefault(),
                Text = gossipOption.OptionText,
                BoxText = gossipOption.BoxText
            };
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE, ClientVersionBuild.V12_1_0_69214)]
        public static void HandleNpcGossip(Packet packet)
        {
            PacketGossipMessage packetGossip = packet.Holder.GossipMessage = new PacketGossipMessage();

            var guid = packet.ReadPackedGuid128("GossipGUID");
            packetGossip.GossipSource = guid;

            var menuId = packet.ReadInt32("GossipID");
            packetGossip.MenuId = (uint)menuId;

            var lfgDungeonsId = packet.ReadInt32("LfgDungeonsID");
            var friendshipFactionId = packet.ReadInt32("FriendshipFactionID");

            var optionsCount = packet.ReadUInt32("GossipOptionsCount");
            var questsCount = packet.ReadUInt32("GossipQuestsCount");

            for (var i = 0u; i < optionsCount; ++i)
                packetGossip.Options.Add(ReadGossipOptionsData((uint)menuId, guid, packet, i, "GossipOptions"));

            for (var i = 0u; i < questsCount; ++i)
                packetGossip.Quests.Add(V7_0_3_22248.Parsers.NpcHandler.ReadGossipQuestTextData(packet, i, "GossipQuests"));

            uint? broadcastTextId = null;
            var npcTextId = 0u;

            packet.ResetBitReader();
            var hasRandomTextId = packet.ReadBit("HasRandomTextID");
            var hasBroadcastTextId = packet.ReadBit("HasBroadcastTextID2");

            if (hasRandomTextId)
                broadcastTextId = packet.ReadUInt32("RandomTextID");

            if (hasBroadcastTextId)
                broadcastTextId = packet.ReadUInt32("BroadcastTextID");

            CoreParsers.NpcHandler.AddGossipAddon(packetGossip.MenuId, friendshipFactionId, lfgDungeonsId, guid, packet.TimeSpan);

            if (broadcastTextId.HasValue)
                npcTextId = SQLDatabase.GetNPCTextIDByMenuIDAndBroadcastText(menuId, broadcastTextId.Value);

            if (npcTextId != 0)
            {
                GossipMenu gossip = new()
                {
                    MenuID = packetGossip.MenuId,
                    TextID = packetGossip.TextId = npcTextId,
                    ObjectType = guid.GetObjectType(),
                    ObjectEntry = guid.GetEntry()
                };

                Storage.Gossips.Add(gossip, packet.TimeSpan);
            }
            else if (broadcastTextId.HasValue)
                V9_0_1_36216.Parsers.NpcHandler.AddBroadcastTextToGossip(packetGossip.MenuId, broadcastTextId.Value, guid);

            CoreParsers.NpcHandler.AddCreatureTemplateGossip(guid, (uint)menuId, packet.TimeSpan);
            CoreParsers.NpcHandler.UpdateLastGossipOptionActionMessage(packet.TimeSpan, (uint)menuId);

            packet.AddSniffData(StoreNameType.Gossip, menuId, guid.GetEntry().ToString(CultureInfo.InvariantCulture));
        }
    }
}

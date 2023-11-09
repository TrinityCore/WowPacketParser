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


namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class NpcHandler
    {
        [Parser(Opcode.SMSG_RUNEFORGE_LEGENDARY_CRAFTING_OPEN_NPC)]
        public static void HandleRuneforgeLegendaryCraftingOpenNpc(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
            packet.ReadBit("IsUpgrade"); // Correct?

            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
        }

        [Parser(Opcode.SMSG_GOSSIP_POI)]
        public static void HandleGossipPoi(Packet packet)
        {
            var protoPoi = packet.Holder.GossipPoi = new();
            PointsOfInterest gossipPOI = new PointsOfInterest();

            gossipPOI.ID = protoPoi.Id = packet.ReadInt32("ID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_7_48676))
                gossipPOI.Flags = protoPoi.Flags = (uint)packet.ReadInt32("Flags");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_0_39185))
            {
                Vector3 pos = packet.ReadVector3("Coordinates");
                gossipPOI.PositionX = pos.X;
                gossipPOI.PositionY = pos.Y;
                gossipPOI.PositionZ = pos.Z;
                protoPoi.Coordinates = new Vec2() { X = pos.X, Y = pos.Y };
                protoPoi.Height = pos.Z;
            }
            else
            {
                Vector2 pos = packet.ReadVector2("Coordinates");
                gossipPOI.PositionX = pos.X;
                gossipPOI.PositionY = pos.Y;
                protoPoi.Coordinates = pos;
            }

            gossipPOI.Icon = packet.ReadInt32E<GossipPOIIcon>("Icon");
            gossipPOI.Importance = protoPoi.Importance = (uint)packet.ReadInt32("Importance");
            protoPoi.Icon = (uint)gossipPOI.Icon;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_5_37503))
                gossipPOI.WMOGroupID = packet.ReadInt32("WMOGroupID");

            packet.ResetBitReader();
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V10_0_7_48676))
                gossipPOI.Flags = protoPoi.Flags = packet.ReadBits("Flags", 14);
            uint bit84 = packet.ReadBits(6);
            gossipPOI.Name = protoPoi.Name = packet.ReadWoWString("Name", bit84);

            Storage.GossipPOIs.Add(gossipPOI, packet.TimeSpan);
            CoreParsers.NpcHandler.UpdateTempGossipOptionActionPOI(packet.TimeSpan, gossipPOI.ID);
        }

        [Parser(Opcode.SMSG_CHROMIE_TIME_OPEN_NPC)]
        public static void HandleChromieTimeOpenNpc(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
        }

        [Parser(Opcode.CMSG_CHROMIE_TIME_SELECT_EXPANSION)]
        public static void HandleChromieTimeSelectExpansion(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
            packet.ReadUInt32("Expansion");
        }

        [Parser(Opcode.SMSG_COVENANT_PREVIEW_OPEN_NPC)]
        public static void HandleGarrisonCovenantPreviewOpenNpc(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGUID");
            packet.ReadInt32("CovenantID");
        }

        [Parser(Opcode.SMSG_ADVENTURE_MAP_OPEN_NPC)]
        public static void HandleAdventureMapOpenNpc(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGUID");
            packet.ReadInt32("UiMapID");
        }

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

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE, ClientVersionBuild.V9_2_5_43903)]
        public static void HandleNpcGossip(Packet packet)
        {
            PacketGossipMessage packetGossip = packet.Holder.GossipMessage = new();

            WowGuid guid = packet.ReadPackedGuid128("GossipGUID");
            packetGossip.GossipSource = guid;

            int menuId = packet.ReadInt32("GossipID");
            packetGossip.MenuId = (uint)menuId;

            int friendshipFactionID = packet.ReadInt32("FriendshipFactionID");
            CoreParsers.NpcHandler.AddGossipAddon(packetGossip.MenuId, friendshipFactionID, guid, packet.TimeSpan);

            uint broadcastTextID = 0;
            uint npcTextID = 0;
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V10_0_0_46181))
                broadcastTextID = packet.ReadUInt32("BroadcastTextID");

            int optionsCount = packet.ReadInt32("GossipOptionsCount");
            int questsCount = packet.ReadInt32("GossipQuestsCount");

            bool hasTextID = false;
            bool hasBroadcastTextID = false;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_0_46181))
            {
                hasTextID = packet.ReadBit("HasTextID");
                hasBroadcastTextID = packet.ReadBit("HasBroadcastTextID");
            }

            for (int i = 0; i < optionsCount; ++i)
                packetGossip.Options.Add(V6_0_2_19033.Parsers.NpcHandler.ReadGossipOptionsData((uint)menuId, guid, packet, i, "GossipOptions"));

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
                packetGossip.Quests.Add(V7_0_3_22248.Parsers.NpcHandler.ReadGossipQuestTextData(packet, i, "GossipQuests"));

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

            CoreParsers.NpcHandler.UpdateLastGossipOptionActionMessage(packet.TimeSpan, (uint)menuId);

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
                packet.ResetBitReader();
                packet.ReadBit("Locked", i);
                vendor.IgnoreFiltering = packet.ReadBit("DoNotFilterOnVendor", i);
                packet.ReadBit("Refundable", i);

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

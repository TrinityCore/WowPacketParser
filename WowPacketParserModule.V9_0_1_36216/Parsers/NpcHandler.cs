using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
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
            PointsOfInterest gossipPOI = new PointsOfInterest();

            gossipPOI.ID = packet.ReadInt32("ID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_0_39185))
            {
                Vector3 pos = packet.ReadVector3("Coordinates");
                gossipPOI.PositionX = pos.X;
                gossipPOI.PositionY = pos.Y;
                gossipPOI.PositionZ = pos.Z;
            }
            else
            {
                Vector2 pos = packet.ReadVector2("Coordinates");
                gossipPOI.PositionX = pos.X;
                gossipPOI.PositionY = pos.Y;
            }

            gossipPOI.Icon = packet.ReadInt32E<GossipPOIIcon>("Icon");
            gossipPOI.Importance = (uint)packet.ReadInt32("Importance");

            packet.ResetBitReader();
            gossipPOI.Flags = packet.ReadBits("Flags", 14);
            uint bit84 = packet.ReadBits(6);
            gossipPOI.Name = packet.ReadWoWString("Name", bit84);

            var lastGossipOption = CoreParsers.NpcHandler.LastGossipOption;
            var tempGossipOptionPOI = CoreParsers.NpcHandler.TempGossipOptionPOI;

            lastGossipOption.ActionPoiId = gossipPOI.ID;
            tempGossipOptionPOI.ActionPoiId = gossipPOI.ID;

            Storage.GossipPOIs.Add(gossipPOI, packet.TimeSpan);

            if (tempGossipOptionPOI.HasSelection)
            {
                if ((packet.TimeSpan - tempGossipOptionPOI.TimeSpan).Duration() <= TimeSpan.FromMilliseconds(2500))
                {
                    if (tempGossipOptionPOI.ActionMenuId != null)
                    {
                        Storage.GossipMenuOptionActions.Add(new GossipMenuOptionAction { MenuId = tempGossipOptionPOI.MenuId, OptionIndex = tempGossipOptionPOI.OptionIndex, ActionMenuId = tempGossipOptionPOI.ActionMenuId, ActionPoiId = gossipPOI.ID }, packet.TimeSpan);
                        //clear temp
                        tempGossipOptionPOI.Reset();
                    }
                }
                else
                {
                    lastGossipOption.Reset();
                    tempGossipOptionPOI.Reset();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class NpcHandler
    {
        public static uint LastGossipPOIEntry = 0;

        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        [Parser(Opcode.CMSG_LIST_INVENTORY)]
        [Parser(Opcode.SMSG_SHOW_BANK)]
        public static void HandleNpcHello(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.CMSG_NPC_TEXT_QUERY)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            packet.ReadInt32("Entry");
            packet.ReadPackedGuid128("Guid");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NPC_TEXT_UPDATE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var npcText = new NpcTextMop();

            var entry = packet.ReadEntry("Entry");
            if (entry.Value) // Can be masked
                return;

            var hasData = packet.ReadBit("Has Data");
            if (!hasData)
                return; // nothing to do

            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);

            var pkt = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);
            npcText.Probabilities = new float[8];
            npcText.BroadcastTextId = new uint[8];
            for (var i = 0; i < 8; ++i)
                npcText.Probabilities[i] = pkt.ReadSingle("Probability", i);
            for (var i = 0; i < 8; ++i)
                npcText.BroadcastTextId[i] = pkt.ReadUInt32("Broadcast Text Id", i);

            pkt.ClosePacket(false);

            packet.AddSniffData(StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");

            Storage.NpcTextsMop.Add((uint)entry.Key, npcText, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");

            var gossipId = packet.ReadUInt32("Gossip Id");
            var menuEntry = packet.ReadUInt32("Menu Id");

            var bits8 = packet.ReadBits(8);
            packet.ResetBitReader();
            packet.ReadWoWString("Box Text", bits8);

            Storage.GossipSelects.Add(Tuple.Create(menuEntry, gossipId), null, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_GOSSIP_POI)]
        public static void HandleGossipPoi(Packet packet)
        {
            ++LastGossipPOIEntry;

            var gossipPOI = new GossipPOI();

            gossipPOI.Flags = (uint)packet.ReadBits("Flags", 14);
            var bit84 = packet.ReadBits(6);
            var pos = packet.ReadVector2("Coordinates");
            gossipPOI.Icon = packet.ReadEnum<GossipPOIIcon>("Icon", TypeCode.UInt32);
            gossipPOI.Data = packet.ReadUInt32("Data");
            gossipPOI.IconName = packet.ReadWoWString("Icon Name", bit84);

            gossipPOI.XPos = pos.X;
            gossipPOI.YPos = pos.Y;

            Storage.GossipPOIs.Add(LastGossipPOIEntry, gossipPOI, packet.TimeSpan);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            var gossip = new Gossip();

            var guid = packet.ReadPackedGuid128("GossipGUID");

            gossip.ObjectType = guid.GetObjectType();
            gossip.ObjectEntry = guid.GetEntry();

            var menuId = packet.ReadInt32("GossipID");
            packet.ReadInt32("FriendshipFactionID");
            var textId = packet.ReadInt32("TextID");

            var int44 = packet.ReadInt32("GossipOptions");
            var int60 = packet.ReadInt32("GossipText");

            // ClientGossipOptions
            gossip.GossipOptions = new List<GossipOption>(int44);
            for (var i = 0; i < int44; ++i)
            {
                var gossipOption = new GossipOption();

                gossipOption.Index = (uint)packet.ReadInt32("ClientOption", i);
                packet.ReadByte("OptionNPC", i);
                packet.ReadByte("OptionFlags", i);
                gossipOption.RequiredMoney = (uint)packet.ReadInt32("OptionCost", i);

                var bits3 = packet.ReadBits(12);
                var bits753 = packet.ReadBits(12);

                gossipOption.OptionText = packet.ReadWoWString("Text", bits3, i);
                gossipOption.BoxText = packet.ReadWoWString("Confirm", bits753, i);

                gossip.GossipOptions.Add(gossipOption);
            }

            // ClientGossipOptions
            for (var i = 0; i < int60; ++i)
            {
                packet.ReadInt32("QuestID", i);
                packet.ReadInt32("QuestType", i);
                packet.ReadInt32("QuestLevel", i);

                for (var j = 0; j < 2; ++j)
                    packet.ReadInt32("QuestFlags", i, j);

                packet.ResetBitReader();

                packet.ReadBit("Repeatable");

                var bits13 = packet.ReadBits(9);

                packet.ReadWoWString("QuestTitle", bits13, i);

                if (guid.GetObjectType() == ObjectType.Unit)
                    if (Storage.Objects.ContainsKey(guid))
                        ((Unit)Storage.Objects[guid].Item1).GossipId = (uint)menuId;

                if (Storage.Gossips.ContainsKey(Tuple.Create((uint)menuId, (uint)textId)))
                {
                    var oldGossipOptions = Storage.Gossips[Tuple.Create((uint)menuId, (uint)textId)];
                    if (oldGossipOptions != null)
                        foreach (var gossipOptions in gossip.GossipOptions)
                            oldGossipOptions.Item1.GossipOptions.Add(gossipOptions);
                }
                else
                    Storage.Gossips.Add(Tuple.Create((uint)menuId, (uint)textId), gossip, packet.TimeSpan);

                packet.AddSniffData(StoreNameType.Gossip, (int)menuId, guid.GetEntry().ToString(CultureInfo.InvariantCulture));
            }
        }
    }
}

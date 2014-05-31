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

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class NpcHandler
    {
        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            var gossip = new Gossip();

            var guid = new byte[8];

            uint[] titleLen;
            uint[] BoxTextLen;
            uint[] OptionTextLen;

            var questgossips = packet.ReadBits(19);

            titleLen = new uint[questgossips];
            for (var i = 0; i < questgossips; ++i)
            {
                packet.ReadBit("Change Icon", i);
                titleLen[i] = packet.ReadBits(9);
            }

            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();

            var AmountOfOptions = packet.ReadBits(20);

            guid[6] = packet.ReadBit();
            guid[2] = packet.ReadBit();

            BoxTextLen = new uint[AmountOfOptions];
            OptionTextLen = new uint[AmountOfOptions];
            for (var i = 0; i < AmountOfOptions; ++i)
            {
                BoxTextLen[i] = packet.ReadBits(12);
                OptionTextLen[i] = packet.ReadBits(12);
            }

            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            for (var i = 0; i < questgossips; ++i)
            {
                packet.ReadWoWString("Title", titleLen[i], i);
                packet.ReadEnum<QuestFlags>("Flags", TypeCode.UInt32, i);//528
                packet.ReadInt32("Level", i);//8
                packet.ReadUInt32("Icon", i);//4
                packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest ID", i); //528
                packet.ReadEnum<QuestFlags2>("Flags 2", TypeCode.UInt32, i);//532
            }

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 0);

            gossip.GossipOptions = new List<GossipOption>((int)AmountOfOptions);
            for (var i = 0; i < AmountOfOptions; ++i)
            {
                var gossipOption = new GossipOption
                {
                    RequiredMoney = packet.ReadUInt32("Required money", i),//3012
                    BoxText = packet.ReadWoWString("Box Text", BoxTextLen[i], i),//12 
                    Index = packet.ReadUInt32("Index", i),//0
                    Box = packet.ReadBoolean("Box", i),
                    OptionText = packet.ReadWoWString("Text", OptionTextLen[i], i),
                    OptionIcon = packet.ReadEnum<GossipOptionIcon>("Icon", TypeCode.Byte, i),//4
                };

                gossip.GossipOptions.Add(gossipOption);
            }

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);
            var menuId = packet.ReadUInt32("Menu Id"); 
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadUInt32("Friendship Faction");
            packet.ReadXORByte(guid, 7);
            var textId = packet.ReadUInt32("Text Id");

            packet.WriteGuid("Guid", guid);

            var GUID = new Guid(BitConverter.ToUInt64(guid, 0));
            gossip.ObjectType = GUID.GetObjectType();
            gossip.ObjectEntry = GUID.GetEntry();

            Storage.Gossips.Add(Tuple.Create(menuId, textId), gossip, packet.TimeSpan);
            packet.AddSniffData(StoreNameType.Gossip, (int)menuId, GUID.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NPC_TEXT_UPDATE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var npcText = new NpcTextMop();

            var entry = packet.ReadEntry("Entry");
            if (entry.Value) // Can be masked
                return;

            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);

            var pkt = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);
            npcText.Probabilities = new float[8];
            npcText.BroadcastTextId = new uint[8];
            for (var i = 0; i < 8; ++i)
                npcText.Probabilities[i] = pkt.ReadSingle("Probability", i);
            for (var i = 0; i < 8; ++i)
                npcText.BroadcastTextId[i] = pkt.ReadUInt32("Broadcast Text Id", i);

            var hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            packet.AddSniffData(StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");

            Storage.NpcTextsMop.Add((uint)entry.Key, npcText, packet.TimeSpan);
        }
    }
}

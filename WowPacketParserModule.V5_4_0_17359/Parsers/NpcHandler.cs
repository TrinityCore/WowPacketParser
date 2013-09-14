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

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class NpcHandler
    {
        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        public static void HandleGossipHello(Packet packet)
        {
            var guid = new byte[8];
            guid[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            var guid = new byte[8];
            uint[] titleLen;
            uint[] OptionTextLen;
            uint[] BoxTextLen;
                    
            var textId = packet.ReadUInt32("Text Id");
            packet.ReadUInt32("Friendship Faction");
            var menuId = packet.ReadUInt32("Menu Id");
            packet.StartBitStream(guid, 0, 1);         
            var AmountOfOptions = packet.ReadBits("Amount of Options", 20);
            packet.StartBitStream(guid, 6, 7);
            
            OptionTextLen = new uint[AmountOfOptions];
            BoxTextLen = new uint[AmountOfOptions];
            for (var i = 0; i < AmountOfOptions; ++i)
            {
                BoxTextLen[i] = packet.ReadBits(12);
                OptionTextLen[i] = packet.ReadBits(12);
            }
            packet.StartBitStream(guid, 4, 3, 2);
                      
            var questgossips = packet.ReadBits("Amount of Quest gossips", 19);
            
            titleLen = new uint[questgossips];
            for (var i = 0; i < questgossips; ++i)
            {
                titleLen[i] = packet.ReadBits(9);
                packet.ReadBit("Change Icon", i);
                //packet.ReadBit("bit12");
            }
            guid[5] = packet.ReadBit();
            packet.ResetBitReader();
            
            for (var i = 0; i < questgossips; i++)
            {
                packet.ReadEnum<QuestFlags2>("Flags 2", TypeCode.UInt32, i);
                packet.ReadUInt32("Icon", i);    
                packet.ReadWoWString("Title", titleLen[i], i);
                packet.ReadEnum<QuestFlags>("Flags", TypeCode.UInt32, i);
                packet.ReadInt32("Level", i);
                packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest ID", i);                      
            }
            
            var gossip = new Gossip();

            gossip.GossipOptions = new List<GossipOption>((int)AmountOfOptions);
            for (var i = 0; i < AmountOfOptions; ++i)
            {
                var gossipOption = new GossipOption
                {
                    RequiredMoney = packet.ReadUInt32("Required money", i),
                    Index = packet.ReadUInt32("Index", i),
                    BoxText = packet.ReadWoWString("Box Text", BoxTextLen[i], i),
                    Box = packet.ReadBoolean("Box", i),
                    OptionText = packet.ReadWoWString("Text", OptionTextLen[i], i),
                    OptionIcon = packet.ReadEnum<GossipOptionIcon>("Icon", TypeCode.Byte, i),
                    
                };

                gossip.GossipOptions.Add(gossipOption);
            }
            
            packet.ParseBitStream(guid, 3, 4, 7, 2, 1, 6, 0, 5);
            packet.WriteGuid("GUID", guid);

            var GUID = new Guid(BitConverter.ToUInt64(guid, 0));
            gossip.ObjectType = GUID.GetObjectType();
            gossip.ObjectEntry = GUID.GetEntry();

            Storage.Gossips.Add(Tuple.Create(menuId, textId), gossip, packet.TimeSpan);
            packet.AddSniffData(StoreNameType.Gossip, (int)menuId, GUID.GetEntry().ToString(CultureInfo.InvariantCulture));
        }
    }
}
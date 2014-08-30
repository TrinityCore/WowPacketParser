using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.WowGuid;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class NpcHandler
    {
        [Parser(Opcode.CMSG_AUCTION_HELLO)]
        public static void HandleAuctionHello(Packet packet)
        {
            var GUID = packet.StartBitStream(1, 5, 2, 0, 3, 6, 4, 7);
            packet.ParseBitStream(GUID, 2, 7, 1, 3, 5, 0, 4, 6);
            packet.WriteGuid("GUID", GUID);
        }

        [Parser(Opcode.CMSG_BANKER_ACTIVATE)]
        public static void HandleBankerActivate(Packet packet)
        {
            var GUID = packet.StartBitStream(4, 5, 0, 6, 1, 2, 7, 3);
            packet.ParseBitStream(GUID, 1, 7, 2, 5, 6, 3, 0, 4);
            packet.WriteGuid("GUID", GUID);
        }

        [Parser(Opcode.CMSG_BINDER_ACTIVATE)]
        public static void HandleBinderActivate(Packet packet)
        {
            var GUID = packet.StartBitStream(0, 5, 4, 7, 6, 2, 1, 3);
            packet.ParseBitStream(GUID, 0, 4, 2, 3, 7, 1, 5, 6);
            packet.WriteGuid("GUID", GUID);
        }

        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        public static void HandleNpcHello(Packet packet)
        {
            var guid = packet.StartBitStream(2, 4, 0, 3, 6, 7, 5, 1);
            packet.ParseBitStream(guid, 4, 7, 1, 0, 5, 3, 6, 2);

            packet.WriteGuid("NPC Guid", guid);
        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            var guid = new byte[8];

            var menuEntry = packet.ReadUInt32("Menu Id"); // 272
            var gossipId = packet.ReadUInt32("Gossip Id"); // 288

            guid[3] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[6] = packet.ReadBit();

            var boxTextLength = packet.ReadBits("size", 8);

            guid[2] = packet.ReadBit();

            //flushbits

            packet.ParseBitStream(guid, 7, 3, 4, 6, 0, 5);

            packet.ReadWoWString("Box Text", boxTextLength);

            packet.ParseBitStream(guid, 2, 1);

            packet.WriteGuid("NPC Guid", guid);

            Storage.GossipSelects.Add(Tuple.Create(menuEntry, gossipId), null, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_LIST_INVENTORY)]
        public static void HandleNpcListInventory(Packet packet)
        {
            var guid = packet.StartBitStream(6, 7, 3, 1, 2, 0, 4, 5);
            packet.ParseBitStream(guid, 0, 7, 1, 6, 4, 3, 5, 2);
            packet.WriteGuid("NPC Guid", guid);
        }

        [Parser(Opcode.CMSG_NPC_TEXT_QUERY)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            packet.ReadUInt32("Text Id");
            var guid = packet.StartBitStream(4, 5, 1, 7, 0, 2, 6, 3);
            packet.ParseBitStream(guid, 4, 0, 2, 5, 1, 7, 3, 6);
            packet.WriteGuid("NPC Guid", guid);
        }

        [Parser(Opcode.CMSG_SPIRIT_HEALER_ACTIVATE)]
        public static void HandleSpiritHealerActivate(Packet packet)
        {
            var guid = packet.StartBitStream(2, 7, 6, 0, 5, 4, 1, 3);
            packet.ParseBitStream(guid, 1, 5, 6, 3, 2, 0, 7, 4);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_TRAINER_BUY_SPELL)]
        public static void HandleTrainerBuySpell(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_TRAINER_LIST)]
        public static void HandleTrainerList(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_AUCTION_HELLO)]
        public static void HandleAuctionHelloResponse(Packet packet)
        {
            var GUID = new byte[8];
            GUID[6] = packet.ReadBit();
            GUID[7] = packet.ReadBit();
            GUID[3] = packet.ReadBit();
            var inUse = packet.ReadBit("inUse");
            GUID[4] = packet.ReadBit();
            GUID[2] = packet.ReadBit();
            GUID[5] = packet.ReadBit();
            GUID[0] = packet.ReadBit();
            GUID[1] = packet.ReadBit();

            packet.ReadXORByte(GUID, 3);
            var AHID = packet.ReadUInt32("Entry");
            packet.ParseBitStream(GUID, 4, 2, 7, 1, 0, 6, 5);
            packet.WriteGuid("GUID", GUID);
        }

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
                packet.ReadEntry<UInt32>(StoreNameType.Quest, "Quest ID", i); //528
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

            if (Storage.Gossips.ContainsKey(Tuple.Create(menuId, textId)))
            {
                var oldGossipOptions = Storage.Gossips[Tuple.Create(menuId, textId)];
                if (oldGossipOptions != null)
                {
                    foreach (var gossipOptions in gossip.GossipOptions)
                        oldGossipOptions.Item1.GossipOptions.Add(gossipOptions);
                }
            }
            else
                Storage.Gossips.Add(Tuple.Create(menuId, textId), gossip, packet.TimeSpan);

            packet.AddSniffData(StoreNameType.Gossip, (int)menuId, GUID.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.SMSG_GOSSIP_POI)]
        public static void HandleGossipPoi(Packet packet)
        {
            var Flags = packet.ReadUInt32("Flags: ");
            var x = packet.ReadSingle("X: ");
            var y = packet.ReadSingle("Y: ");
            var Icon = packet.ReadUInt32("Icon: ");
            var Data = packet.ReadUInt32("Data: ");
            var Text = packet.ReadCString("Text: ");
        }

        [Parser(Opcode.SMSG_HIGHEST_THREAT_UPDATE)]
        public static void HandleHighestThreatlistUpdate(Packet packet)
        {
            var guid = new byte[8];
            var newHighestGUID = new byte[8];

            guid[3] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            newHighestGUID[3] = packet.ReadBit();
            newHighestGUID[6] = packet.ReadBit();
            newHighestGUID[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            newHighestGUID[2] = packet.ReadBit();
            newHighestGUID[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            newHighestGUID[4] = packet.ReadBit();

            var count32 = packet.ReadBits("cnt32", 21);
            var guids = new byte[count32][];

            for (var i = 0; i < count32; i++)
                guids[i] = packet.StartBitStream(6, 1, 0, 2, 7, 4, 3, 5);

            newHighestGUID[7] = packet.ReadBit();
            newHighestGUID[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();

            packet.ReadXORByte(newHighestGUID, 4);

            for (var i = 0; i < count32; i++)
            {
                packet.ParseBitStream(guids[i], 6);
                packet.ReadInt32("Threat", i); // 8
                packet.ParseBitStream(guids[i], 4, 0, 3, 5, 2, 1, 7);

                packet.WriteGuid("Hostile", guids[i], i);
            }
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(newHighestGUID, 5);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(newHighestGUID, 1);
            packet.ReadXORByte(newHighestGUID, 0);
            packet.ReadXORByte(newHighestGUID, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(newHighestGUID, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(newHighestGUID, 3);
            packet.ReadXORByte(newHighestGUID, 6);
            packet.ReadXORByte(guid, 5);

            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("New Highest", newHighestGUID);
        }

        [Parser(Opcode.SMSG_LIST_INVENTORY)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SHOW_BANK)]
        public static void HandleShowBank(Packet packet)
        {
            var GUID = packet.StartBitStream(2, 4, 3, 6, 5, 1, 7, 0);
            packet.ParseBitStream(GUID, 7, 0, 5, 3, 6, 1, 4, 2);
            packet.WriteGuid("GUID", GUID);
        }

        [Parser(Opcode.SMSG_THREAT_CLEAR)]
        public static void HandleClearThreatlist(Packet packet)
        {
            var guid = packet.StartBitStream(6, 7, 4, 5, 2, 1, 0, 3);
            packet.ParseBitStream(guid, 7, 0, 4, 3, 2, 1, 6, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_THREAT_REMOVE)]
        public static void HandleRemoveThreatlist(Packet packet)
        {
            var hostileGUID = new byte[8];
            var victimGUID = new byte[8];

            victimGUID[0] = packet.ReadBit();
            victimGUID[1] = packet.ReadBit();
            victimGUID[5] = packet.ReadBit();
            hostileGUID[4] = packet.ReadBit();
            hostileGUID[0] = packet.ReadBit();
            victimGUID[4] = packet.ReadBit();
            victimGUID[6] = packet.ReadBit();
            hostileGUID[7] = packet.ReadBit();
            hostileGUID[6] = packet.ReadBit();
            hostileGUID[3] = packet.ReadBit();
            victimGUID[2] = packet.ReadBit();
            hostileGUID[1] = packet.ReadBit();
            victimGUID[3] = packet.ReadBit();
            victimGUID[7] = packet.ReadBit();
            hostileGUID[5] = packet.ReadBit();
            hostileGUID[2] = packet.ReadBit();

            packet.ReadXORByte(hostileGUID, 3);
            packet.ReadXORByte(hostileGUID, 0);
            packet.ReadXORByte(hostileGUID, 2);
            packet.ReadXORByte(victimGUID, 5);
            packet.ReadXORByte(victimGUID, 4);
            packet.ReadXORByte(victimGUID, 7);
            packet.ReadXORByte(victimGUID, 3);
            packet.ReadXORByte(victimGUID, 0);
            packet.ReadXORByte(hostileGUID, 4);
            packet.ReadXORByte(victimGUID, 1);
            packet.ReadXORByte(hostileGUID, 1);
            packet.ReadXORByte(victimGUID, 6);
            packet.ReadXORByte(hostileGUID, 7);
            packet.ReadXORByte(hostileGUID, 6);
            packet.ReadXORByte(victimGUID, 2);
            packet.ReadXORByte(hostileGUID, 5);

            packet.WriteGuid("Hostile GUID", hostileGUID);
            packet.WriteGuid("GUID", victimGUID);
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}

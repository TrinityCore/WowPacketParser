using System;
using System.Collections.Generic;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class NpcHandler
    {
        public static uint LastGossipPOIEntry;

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

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            var guid = new byte[8];
            var gossipId = packet.ReadUInt32("Gossip Id");
            var menuEntry = packet.ReadUInt32("Menu Id");
            guid[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var bits8 = packet.ReadBits(8);
            guid[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[0] = packet.ReadBit();

            packet.ReadXORBytes(guid, 1, 0, 6, 3, 7, 5, 2);
            packet.ReadWoWString("Box Text", bits8);
            packet.ReadXORByte(guid, 4);

            Storage.GossipSelects.Add(Tuple.Create(menuEntry, gossipId), null, packet.TimeSpan);
            packet.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            var guidBytes = new byte[8];

            var menuId = packet.ReadUInt32("Menu Id");
            packet.ReadUInt32("Friendship Faction");
            var textId = packet.ReadUInt32("Text Id");
            packet.StartBitStream(guidBytes, 0, 1);
            var amountOfOptions = packet.ReadBits("Amount of Options", 20);
            packet.StartBitStream(guidBytes, 6, 7);

            var optionTextLen = new uint[amountOfOptions];
            var boxTextLen = new uint[amountOfOptions];
            for (var i = 0; i < amountOfOptions; ++i)
            {
                optionTextLen[i] = packet.ReadBits(12);
                boxTextLen[i] = packet.ReadBits(12);
            }
            packet.StartBitStream(guidBytes, 4, 3, 2);

            var questgossips = packet.ReadBits("Amount of Quest gossips", 19);

            var titleLen = new uint[questgossips];
            for (var i = 0; i < questgossips; ++i)
            {
                titleLen[i] = packet.ReadBits(9);
                packet.ReadBit("Change Icon", i);
            }
            guidBytes[5] = packet.ReadBit();
            packet.ResetBitReader();

            for (var i = 0; i < questgossips; i++)
            {
                packet.ReadUInt32E<QuestFlags2>("Flags 2", i);
                packet.ReadUInt32("Icon", i);
                packet.ReadWoWString("Title", titleLen[i], i);
                packet.ReadUInt32E<QuestFlags>("Flags", i);
                packet.ReadInt32("Level", i);
                packet.ReadUInt32<QuestId>("Quest ID", i);
            }

            var gossip = new Gossip();

            gossip.GossipOptions = new List<GossipOption>((int)amountOfOptions);
            for (var i = 0; i < amountOfOptions; ++i)
            {
                var gossipOption = new GossipOption
                {
                    RequiredMoney = packet.ReadUInt32("Required money", i),
                    Index = packet.ReadUInt32("Index", i),
                    BoxText = packet.ReadWoWString("Box Text", boxTextLen[i], i),
                    Box = packet.ReadBool("Box", i),
                    OptionText = packet.ReadWoWString("Text", optionTextLen[i], i),
                    OptionIcon = packet.ReadByteE<GossipOptionIcon>("Icon", i)
                };

                gossip.GossipOptions.Add(gossipOption);
            }

            packet.ParseBitStream(guidBytes, 3, 4, 7, 2, 1, 6, 0, 5);
            var guid = packet.WriteGuid("GUID", guidBytes);

            gossip.ObjectType = guid.GetObjectType();
            gossip.ObjectEntry = guid.GetEntry();

            if (guid.GetObjectType() == ObjectType.Unit)
                if (Storage.Objects.ContainsKey(guid))
                    ((Unit)Storage.Objects[guid].Item1).GossipId = menuId;

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

            packet.AddSniffData(StoreNameType.Gossip, (int)menuId, guid.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.SMSG_GOSSIP_POI)]
        public static void HandleGossipPoi(Packet packet)
        {
            LastGossipPOIEntry++;

            var gossipPOI = new GossipPOI();

            gossipPOI.Flags = (uint) packet.ReadInt32E<UnknownFlags>("Flags");
            var pos = packet.ReadVector2("Coordinates");
            gossipPOI.Icon = packet.ReadUInt32E<GossipPOIIcon>("Icon");
            gossipPOI.Importance = packet.ReadUInt32("Data");
            gossipPOI.Name = packet.ReadCString("Icon Name");

            gossipPOI.PositionX = pos.X;
            gossipPOI.PositionY = pos.Y;

            Storage.GossipPOIs.Add(LastGossipPOIEntry, gossipPOI, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_HIGHEST_THREAT_UPDATE)]
        public static void HandleHighestThreatlistUpdate(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid1[5] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            packet.StartBitStream(guid2, 5, 3);
            guid1[1] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            packet.StartBitStream(guid1, 2, 0, 6);
            guid2[7] = packet.ReadBit();

            var count = packet.ReadBits(21);

            var guid = new byte[count][];

            for (var i = 0; i < count; i++)
            {
                guid[i] = new byte[8];

                packet.StartBitStream(guid[i], 5, 0, 6, 2, 7, 3, 4, 1);
            }

            packet.StartBitStream(guid2, 4, 2);

            guid1[3] = packet.ReadBit();

            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 4);

            for (var i = 0; i < count; i++)
            {
                packet.ReadXORByte(guid[i], 6);
                packet.ReadXORByte(guid[i], 3);
                packet.ReadXORByte(guid[i], 2);
                packet.ReadXORByte(guid[i], 0);
                packet.ReadXORByte(guid[i], 5);

                packet.ReadInt32("Threat", i);

                packet.ReadXORByte(guid[i], 1);
                packet.ReadXORByte(guid[i], 7);
                packet.ReadXORByte(guid[i], 4);

                packet.WriteGuid("Hostile", guid[i], i);
            }

            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 1);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("GUID2", guid2);
        }

        [Parser(Opcode.SMSG_THREAT_UPDATE)]
        public static void HandleThreatlistUpdate(Packet packet)
        {
            var guid1 = new byte[8];

            var count = packet.ReadBits("Size", 21);

            var guid2 = new byte[count][];
            for (var i = 0; i < count; ++i)
            {
                guid2[i] = new byte[8];
                packet.StartBitStream(guid2[i], 7, 4, 3, 2, 6, 1, 0, 5);
            }

            packet.StartBitStream(guid1, 2, 7, 4, 0, 1, 6, 3, 5);

            for (var i = 0; i < count; ++i)
            {
                packet.ParseBitStream(guid2[i], 2, 5, 6, 0, 1, 4);
                packet.ReadInt32("IntED", i);
                packet.ParseBitStream(guid2[i], 7, 3);
                packet.WriteGuid("Guid1D", guid2[i], i);

            }

            packet.ParseBitStream(guid1, 1, 0, 6, 3, 2, 7, 5, 4);

            packet.WriteGuid("Guid1", guid1);
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            var npcVendor = new NpcVendor();

            var guid = new byte[8];

            guid[5] = packet.ReadBit();
            var itemCount = packet.ReadBits(18);
            guid[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();

            var hasExtendedCost = new bool[itemCount];
            var hasCondition = new bool[itemCount];

            for (int i = 0; i < itemCount; ++i)
            {
                packet.ReadBit("Unk bit", i);
                hasExtendedCost[i] = !packet.ReadBit();
                hasCondition[i] = !packet.ReadBit();
            }

            packet.StartBitStream(guid, 3, 7, 1, 6, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);

            npcVendor.VendorItems = new List<VendorItem>((int)itemCount);
            for (int i = 0; i < itemCount; ++i)
            {
                var vendorItem = new VendorItem();

                var maxCount = packet.ReadInt32("Max Count", i);
                vendorItem.Type = packet.ReadUInt32("Type", i); // 1 - item, 2 - currency

                if (hasCondition[i])
                    packet.ReadInt32("Condition ID", i);

                packet.ReadInt32("Max Durability", i);
                packet.ReadInt32("Item Upgrade ID", i);
                packet.ReadInt32("Price", i);
                packet.ReadInt32("Display ID", i);
                vendorItem.ItemId = (uint)packet.ReadInt32<ItemId>("Item ID", i);
                vendorItem.Slot = packet.ReadUInt32("Item Position", i);

                if (hasExtendedCost[i])
                    vendorItem.ExtendedCostId = packet.ReadUInt32("Extended Cost", i);

                var buyCount = packet.ReadUInt32("Buy Count", i);

                vendorItem.MaxCount = maxCount == -1 ? 0 : maxCount; // TDB
                if (vendorItem.Type == 2)
                    vendorItem.MaxCount = (int)buyCount;

                npcVendor.VendorItems.Add(vendorItem);
            }

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadByte("Byte10");
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);

            packet.WriteGuid("Guid", guid);

            var vendorGUID = new WowGuid64(BitConverter.ToUInt64(guid, 0));
            Storage.NpcVendors.Add(vendorGUID.GetEntry(), npcVendor, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            var guid = new byte[8];
            var npcTrainer = new NpcTrainer();

            guid[0] = packet.ReadBit();
            var titleLen = packet.ReadBits(11);
            packet.StartBitStream(guid, 5, 6, 1, 2, 7, 4, 3);
            var count = (int)packet.ReadBits("Count", 19);

            npcTrainer.TrainerSpells = new List<TrainerSpell>(count);
            for (var i = 0; i < count; ++i)
            {
                //var trainerSpell = new TrainerSpell();
                packet.ReadInt32("Int824", i);
                packet.ReadInt32("Int824", i);
                packet.ReadByte("Byte824", i);
                for (var j = 0; j < 3; ++j)
                    packet.ReadInt32("Int824", i, j);
                packet.ReadInt32("Int824", i);
                packet.ReadByte("Byte824", i);
                packet.ReadInt32("Int824", i);

                //npcTrainer.TrainerSpells.Add(trainerSpell);
            }

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 6);
            npcTrainer.Title = packet.ReadWoWString("Title", titleLen);
            packet.ReadInt32("Int818");
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 4);
            packet.ReadInt32("Int81C");

            packet.WriteGuid("GUID", guid);
            //var GUID = new Guid(BitConverter.ToUInt64(guid, 0));
            //Storage.NpcTrainers.Add(GUID.GetEntry(), npcTrainer, packet.TimeSpan);
        }
    }
}

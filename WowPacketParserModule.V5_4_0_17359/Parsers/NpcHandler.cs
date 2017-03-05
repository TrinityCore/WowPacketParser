using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class NpcHandler
    {
        public static uint LastGossipPOIEntry;

        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        public static void HandleGossipHello(Packet packet)
        {
            var guid = new byte[8];
            guid[2] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            var guid = new byte[8];
            var gossipId = packet.Translator.ReadUInt32("GossipMenu Id");
            var menuEntry = packet.Translator.ReadUInt32("Menu Id");
            guid[7] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            var bits8 = packet.Translator.ReadBits(8);
            guid[5] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();

            packet.Translator.ReadXORBytes(guid, 1, 0, 6, 3, 7, 5, 2);
            packet.Translator.ReadWoWString("Box Text", bits8);
            packet.Translator.ReadXORByte(guid, 4);

            Storage.GossipSelects.Add(Tuple.Create(menuEntry, gossipId), null, packet.TimeSpan);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            var guidBytes = new byte[8];

            uint menuId = packet.Translator.ReadUInt32("Menu Id");
            packet.Translator.ReadUInt32("Friendship Faction");
            uint textId = packet.Translator.ReadUInt32("Text Id");
            packet.Translator.StartBitStream(guidBytes, 0, 1);
            uint amountOfOptions = packet.Translator.ReadBits("Amount of Options", 20);
            packet.Translator.StartBitStream(guidBytes, 6, 7);

            var optionTextLen = new uint[amountOfOptions];
            var boxTextLen = new uint[amountOfOptions];
            for (int i = 0; i < amountOfOptions; ++i)
            {
                optionTextLen[i] = packet.Translator.ReadBits(12);
                boxTextLen[i] = packet.Translator.ReadBits(12);
            }
            packet.Translator.StartBitStream(guidBytes, 4, 3, 2);

            uint questgossips = packet.Translator.ReadBits("Amount of Quest gossips", 19);

            var titleLen = new uint[questgossips];
            for (int i = 0; i < questgossips; ++i)
            {
                titleLen[i] = packet.Translator.ReadBits(9);
                packet.Translator.ReadBit("Change Icon", i);
            }
            guidBytes[5] = packet.Translator.ReadBit();
            packet.Translator.ResetBitReader();

            for (int i = 0; i < questgossips; i++)
            {
                packet.Translator.ReadUInt32E<QuestFlags2>("Flags 2", i);
                packet.Translator.ReadUInt32("Icon", i);
                packet.Translator.ReadWoWString("Title", titleLen[i], i);
                packet.Translator.ReadUInt32E<QuestFlags>("Flags", i);
                packet.Translator.ReadInt32("Level", i);
                packet.Translator.ReadUInt32<QuestId>("Quest ID", i);
            }

            for (int i = 0; i < amountOfOptions; ++i)
            {
                GossipMenuOption gossipOption = new GossipMenuOption
                {
                    MenuID = menuId,
                    BoxMoney = packet.Translator.ReadUInt32("Required money", i),
                    ID = packet.Translator.ReadUInt32("Index", i),
                    BoxText = packet.Translator.ReadWoWString("Box Text", boxTextLen[i], i),
                    BoxCoded = packet.Translator.ReadBool("Box", i),
                    OptionText = packet.Translator.ReadWoWString("Text", optionTextLen[i], i),
                    OptionIcon = packet.Translator.ReadByteE<GossipOptionIcon>("Icon", i)
                };

                Storage.GossipMenuOptions.Add(gossipOption, packet.TimeSpan);
            }

            packet.Translator.ParseBitStream(guidBytes, 3, 4, 7, 2, 1, 6, 0, 5);

            GossipMenu gossip = new GossipMenu
            {
                Entry = menuId,
                TextID = textId
            };

            WowGuid guid = packet.Translator.WriteGuid("GUID", guidBytes);

            gossip.ObjectType = guid.GetObjectType();
            gossip.ObjectEntry = guid.GetEntry();

            if (guid.GetObjectType() == ObjectType.Unit)
                if (Storage.Objects.ContainsKey(guid))
                    ((Unit)Storage.Objects[guid].Item1).GossipId = menuId;

            Storage.Gossips.Add(gossip, packet.TimeSpan);

            packet.AddSniffData(StoreNameType.Gossip, (int)menuId, guid.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.SMSG_GOSSIP_POI)]
        public static void HandleGossipPoi(Packet packet)
        {
            PointsOfInterest gossipPOI = new PointsOfInterest();
            gossipPOI.ID = ++LastGossipPOIEntry;

            gossipPOI.Flags = (uint) packet.Translator.ReadInt32E<UnknownFlags>("Flags");

            Vector2 pos = packet.Translator.ReadVector2("Coordinates");
            gossipPOI.PositionX = pos.X;
            gossipPOI.PositionY = pos.Y;

            gossipPOI.Icon = packet.Translator.ReadUInt32E<GossipPOIIcon>("Icon");
            gossipPOI.Importance = packet.Translator.ReadUInt32("Data");
            gossipPOI.Name = packet.Translator.ReadCString("Icon Name");

            Storage.GossipPOIs.Add(gossipPOI, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_HIGHEST_THREAT_UPDATE)]
        public static void HandleHighestThreatlistUpdate(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid1[5] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid2, 5, 3);
            guid1[1] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 2, 0, 6);
            guid2[7] = packet.Translator.ReadBit();

            var count = packet.Translator.ReadBits(21);

            var guid = new byte[count][];

            for (var i = 0; i < count; i++)
            {
                guid[i] = new byte[8];

                packet.Translator.StartBitStream(guid[i], 5, 0, 6, 2, 7, 3, 4, 1);
            }

            packet.Translator.StartBitStream(guid2, 4, 2);

            guid1[3] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 4);

            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadXORByte(guid[i], 6);
                packet.Translator.ReadXORByte(guid[i], 3);
                packet.Translator.ReadXORByte(guid[i], 2);
                packet.Translator.ReadXORByte(guid[i], 0);
                packet.Translator.ReadXORByte(guid[i], 5);

                packet.Translator.ReadInt32("Threat", i);

                packet.Translator.ReadXORByte(guid[i], 1);
                packet.Translator.ReadXORByte(guid[i], 7);
                packet.Translator.ReadXORByte(guid[i], 4);

                packet.Translator.WriteGuid("Hostile", guid[i], i);
            }

            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 1);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("GUID2", guid2);
        }

        [Parser(Opcode.SMSG_THREAT_UPDATE)]
        public static void HandleThreatlistUpdate(Packet packet)
        {
            var guid1 = new byte[8];

            var count = packet.Translator.ReadBits("Size", 21);

            var guid2 = new byte[count][];
            for (var i = 0; i < count; ++i)
            {
                guid2[i] = new byte[8];
                packet.Translator.StartBitStream(guid2[i], 7, 4, 3, 2, 6, 1, 0, 5);
            }

            packet.Translator.StartBitStream(guid1, 2, 7, 4, 0, 1, 6, 3, 5);

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ParseBitStream(guid2[i], 2, 5, 6, 0, 1, 4);
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ParseBitStream(guid2[i], 7, 3);
                packet.Translator.WriteGuid("Guid1D", guid2[i], i);

            }

            packet.Translator.ParseBitStream(guid1, 1, 0, 6, 3, 2, 7, 5, 4);

            packet.Translator.WriteGuid("Guid1", guid1);
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            

            var guid = new byte[8];

            guid[5] = packet.Translator.ReadBit();
            uint count = packet.Translator.ReadBits(18);
            guid[0] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();

            var hasExtendedCost = new bool[count];
            var hasCondition = new bool[count];

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadBit("Unk bit", i);
                hasExtendedCost[i] = !packet.Translator.ReadBit();
                hasCondition[i] = !packet.Translator.ReadBit();
            }

            packet.Translator.StartBitStream(guid, 3, 7, 1, 6, 4);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 1);

            var tempList = new List<NpcVendor>();
            for (int i = 0; i < count; ++i)
            {
                NpcVendor vendor = new NpcVendor();

                int maxCount = packet.Translator.ReadInt32("Max Count", i);
                vendor.Type = packet.Translator.ReadUInt32("Type", i); // 1 - item, 2 - currency

                if (hasCondition[i])
                    vendor.PlayerConditionID = packet.Translator.ReadUInt32("Condition ID", i);

                packet.Translator.ReadInt32("Max Durability", i);
                packet.Translator.ReadInt32("Item Upgrade ID", i);
                packet.Translator.ReadInt32("Price", i);
                packet.Translator.ReadInt32("Display ID", i);
                vendor.Item = packet.Translator.ReadInt32<ItemId>("Item ID", i);
                vendor.Slot = packet.Translator.ReadInt32("Item Position", i);

                if (hasExtendedCost[i])
                    vendor.ExtendedCost = packet.Translator.ReadUInt32("Extended Cost", i);

                uint buyCount = packet.Translator.ReadUInt32("Buy Count", i);
                vendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                if (vendor.Type == 2)
                    vendor.MaxCount = buyCount;

                tempList.Add(vendor);
            }

            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadByte("Byte10");
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 3);

            uint entry = packet.Translator.WriteGuid("GUID", guid).GetEntry();
            tempList.ForEach(v =>
            {
                v.Entry = entry;
                Storage.NpcVendors.Add(v, packet.TimeSpan);
            });
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            var guid = new byte[8];

            guid[0] = packet.Translator.ReadBit();
            uint titleLen = packet.Translator.ReadBits(11);
            packet.Translator.StartBitStream(guid, 5, 6, 1, 2, 7, 4, 3);
            uint count = packet.Translator.ReadBits("Count", 19);

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("Int824", i);
                packet.Translator.ReadInt32("Int824", i);
                packet.Translator.ReadByte("Byte824", i);
                for (int j = 0; j < 3; ++j)
                    packet.Translator.ReadInt32("Int824", i, j);
                packet.Translator.ReadInt32("Int824", i);
                packet.Translator.ReadByte("Byte824", i);
                packet.Translator.ReadInt32("Int824", i);
            }

            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadWoWString("Title", titleLen);
            packet.Translator.ReadInt32("Int818");
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadInt32("Int81C");
        }
    }
}

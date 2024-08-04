using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

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

            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
            var gossipGuid = CoreParsers.NpcHandler.LastGossipOption.Guid = packet.WriteGuid("Guid", guid);

            packet.Holder.GossipHello = new PacketGossipHello { GossipSource = gossipGuid };
        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            PacketGossipSelect packetGossip = packet.Holder.GossipSelect = new();

            var guid = new byte[8];
            var optionID = packetGossip.OptionId = packet.ReadUInt32("OptionID");
            var menuID = packetGossip.MenuId = packet.ReadUInt32("MenuID");

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

            Storage.GossipSelects.Add(Tuple.Create(menuID, optionID), null, packet.TimeSpan);
            packetGossip.GossipUnit = packet.WriteGuid("GUID", guid);

            CoreParsers.NpcHandler.LastGossipOption.GossipSelectOption(menuID, optionID, packet.TimeSpan);
            CoreParsers.NpcHandler.TempGossipOptionPOI.GossipSelectOption(menuID, optionID, packet.TimeSpan);
            CoreParsers.NpcHandler.TempGossipOptionPOI.Guid = CoreParsers.NpcHandler.LastGossipOption.Guid;
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            PacketGossipMessage packetGossip = packet.Holder.GossipMessage = new();
            var guidBytes = new byte[8];

            uint menuId = packetGossip.MenuId = packet.ReadUInt32("Menu Id");
            uint friendshipFactionID = packet.ReadUInt32("Friendship Faction");
            uint textId = packetGossip.TextId = packet.ReadUInt32("Text Id");
            packet.StartBitStream(guidBytes, 0, 1);
            uint amountOfOptions = packet.ReadBits("Amount of Options", 20);
            packet.StartBitStream(guidBytes, 6, 7);

            var optionTextLen = new uint[amountOfOptions];
            var boxTextLen = new uint[amountOfOptions];
            for (int i = 0; i < amountOfOptions; ++i)
            {
                optionTextLen[i] = packet.ReadBits(12);
                boxTextLen[i] = packet.ReadBits(12);
            }
            packet.StartBitStream(guidBytes, 4, 3, 2);

            uint questgossips = packet.ReadBits("Amount of Quest gossips", 19);

            var titleLen = new uint[questgossips];
            for (int i = 0; i < questgossips; ++i)
            {
                titleLen[i] = packet.ReadBits(9);
                packet.ReadBit("Change Icon", i);
            }
            guidBytes[5] = packet.ReadBit();
            packet.ResetBitReader();

            for (int i = 0; i < questgossips; i++)
            {
                packet.ReadUInt32E<QuestFlagsEx>("Flags 2", i);
                packet.ReadUInt32("Icon", i);
                var title = packet.ReadWoWString("Title", titleLen[i], i);
                packet.ReadUInt32E<QuestFlags>("Flags", i);
                packet.ReadInt32("Level", i);
                var quest = packet.ReadUInt32<QuestId>("Quest ID", i);

                packetGossip.Quests.Add(new GossipQuestOption()
                {
                    Title = title,
                    QuestId = quest
                });
            }

            for (int i = 0; i < amountOfOptions; ++i)
            {
                GossipMenuOption gossipOption = new GossipMenuOption
                {
                    MenuID = menuId
                };

                gossipOption.BoxMoney = packet.ReadUInt32("Required money", i);
                gossipOption.OptionID = packet.ReadUInt32("OptionID", i);
                var boxText = packet.ReadWoWString("Box Text", boxTextLen[i], i);
                gossipOption.BoxCoded = packet.ReadBool("Box", i);
                gossipOption.OptionText = packet.ReadWoWString("Text", optionTextLen[i], i);
                gossipOption.OptionNpc = packet.ReadByteE<GossipOptionNpc>("OptionNPC", i);

                if (!string.IsNullOrEmpty(boxText))
                    gossipOption.BoxText = boxText;

                Storage.GossipMenuOptions.Add((gossipOption.MenuID, gossipOption.OptionID), gossipOption, packet.TimeSpan);

                packetGossip.Options.Add(new GossipMessageOption()
                {
                    OptionIndex = gossipOption.OptionID.Value,
                    OptionNpc = (int)gossipOption.OptionNpc,
                    BoxCoded = gossipOption.BoxCoded.Value,
                    BoxCost = gossipOption.BoxMoney.Value,
                    Text = gossipOption.OptionText,
                    BoxText = gossipOption.BoxText
                });
            }

            packet.ParseBitStream(guidBytes, 3, 4, 7, 2, 1, 6, 0, 5);

            GossipMenu gossip = new GossipMenu
            {
                MenuID = menuId,
                TextID = textId
            };

            WowGuid guid = packet.WriteGuid("GUID", guidBytes);
            packetGossip.GossipSource = guid;

            gossip.ObjectType = guid.GetObjectType();
            gossip.ObjectEntry = guid.GetEntry();

            if (guid.GetObjectType() == ObjectType.Unit)
            {
                CreatureTemplateGossip creatureTemplateGossip = new()
                {
                    CreatureID = guid.GetEntry(),
                    MenuID = menuId
                };
                Storage.CreatureTemplateGossips.Add(creatureTemplateGossip);
                Storage.CreatureDefaultGossips.Add(guid.GetEntry(), menuId);
            }

            Storage.Gossips.Add(gossip, packet.TimeSpan);

            CoreParsers.NpcHandler.AddGossipAddon(packetGossip.MenuId, (int)friendshipFactionID, 0, guid, packet.TimeSpan);

            CoreParsers.NpcHandler.UpdateLastGossipOptionActionMessage(packet.TimeSpan, menuId);

            packet.AddSniffData(StoreNameType.Gossip, (int)menuId, guid.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.SMSG_GOSSIP_POI)]
        public static void HandleGossipPoi(Packet packet)
        {
            ++LastGossipPOIEntry;
            var protoPoi = packet.Holder.GossipPoi = new();
            PointsOfInterest gossipPOI = new PointsOfInterest();
            gossipPOI.ID = "@ID+" + LastGossipPOIEntry.ToString();

            gossipPOI.Flags = protoPoi.Flags = (uint) packet.ReadInt32E<UnknownFlags>("Flags");

            Vector2 pos = packet.ReadVector2("Coordinates");
            protoPoi.Coordinates = pos;
            gossipPOI.PositionX = pos.X;
            gossipPOI.PositionY = pos.Y;

            gossipPOI.Icon = packet.ReadUInt32E<GossipPOIIcon>("Icon");
            gossipPOI.Importance = protoPoi.Importance = packet.ReadUInt32("Data");
            gossipPOI.Name = protoPoi.Name = packet.ReadCString("Icon Name");
            protoPoi.Icon = (uint)gossipPOI.Icon;

            Storage.GossipPOIs.Add(gossipPOI, packet.TimeSpan);
            CoreParsers.NpcHandler.UpdateTempGossipOptionActionPOI(packet.TimeSpan, gossipPOI.ID);
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
            var guid = new byte[8];

            guid[5] = packet.ReadBit();
            uint count = packet.ReadBits(18);
            guid[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();

            var hasExtendedCost = new bool[count];
            var hasCondition = new bool[count];

            for (int i = 0; i < count; ++i)
            {
                packet.ReadBit("Unk bit", i);
                hasExtendedCost[i] = !packet.ReadBit();
                hasCondition[i] = !packet.ReadBit();
            }

            packet.StartBitStream(guid, 3, 7, 1, 6, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);

            var tempList = new List<NpcVendor>();
            for (int i = 0; i < count; ++i)
            {
                NpcVendor vendor = new NpcVendor();

                int maxCount = packet.ReadInt32("Max Count", i);
                vendor.Type = packet.ReadUInt32("Type", i); // 1 - item, 2 - currency

                if (hasCondition[i])
                    vendor.PlayerConditionID = packet.ReadUInt32("Condition ID", i);

                packet.ReadInt32("Max Durability", i);
                packet.ReadInt32("Item Upgrade ID", i);
                packet.ReadInt32("Price", i);
                packet.ReadInt32("Display ID", i);
                vendor.Item = packet.ReadInt32<ItemId>("Item ID", i);
                vendor.Slot = packet.ReadInt32("Item Position", i);

                if (hasExtendedCost[i])
                    vendor.ExtendedCost = packet.ReadUInt32("Extended Cost", i);

                uint buyCount = packet.ReadUInt32("Buy Count", i);
                vendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                if (vendor.Type == 2)
                    vendor.MaxCount = buyCount;

                tempList.Add(vendor);
            }

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadByte("Byte10");
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);

            uint entry = packet.WriteGuid("GUID", guid).GetEntry();
            tempList.ForEach(v =>
            {
                v.Entry = entry;
                Storage.NpcVendors.Add(v, packet.TimeSpan);
            });

            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            var guid = new byte[8];

            guid[0] = packet.ReadBit();
            uint titleLen = packet.ReadBits(11);
            packet.StartBitStream(guid, 5, 6, 1, 2, 7, 4, 3);
            uint count = packet.ReadBits("Count", 19);

            for (int i = 0; i < count; ++i)
            {
                packet.ReadInt32("Int824", i);
                packet.ReadInt32("Int824", i);
                packet.ReadByte("Byte824", i);
                for (int j = 0; j < 3; ++j)
                    packet.ReadInt32("Int824", i, j);
                packet.ReadInt32("Int824", i);
                packet.ReadByte("Byte824", i);
                packet.ReadInt32("Int824", i);
            }

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 6);
            packet.ReadWoWString("Title", titleLen);
            packet.ReadInt32("Int818");
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 4);
            packet.ReadInt32("Int81C");
        }
    }
}

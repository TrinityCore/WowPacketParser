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

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class NpcHandler
    {
        public static uint LastGossipPOIEntry = 0;

        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        public static void HandleGossipHello(Packet packet)
        {
            var guid = new byte[8];
            packet.StartBitStream(guid, 6, 3, 4, 5, 1, 7, 2, 0);
            packet.ParseBitStream(guid, 6, 0, 1, 7, 2, 5, 4, 3);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            var guid = new byte[8];

            var menuEntry = packet.ReadUInt32("Menu Id");
            var gossipId = packet.ReadUInt32("Gossip Id");

            guid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var bits8 = packet.ReadBits(8);

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadWoWString("Box Text", bits8);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 4);

            Storage.GossipSelects.Add(Tuple.Create(menuEntry, gossipId), null, packet.TimeSpan);
            packet.WriteGuid("GUID", guid);
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

            guid[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            var questgossips = packet.ReadBits(19);

            guid[0] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();

            var AmountOfOptions = packet.ReadBits(20);

            BoxTextLen = new uint[AmountOfOptions];
            OptionTextLen = new uint[AmountOfOptions];
            for (var i = 0; i < AmountOfOptions; ++i)
            {
                BoxTextLen[i] = packet.ReadBits(12);
                OptionTextLen[i] = packet.ReadBits(12);
            }

            titleLen = new uint[questgossips];
            for (var i = 0; i < questgossips; ++i)
            {
                packet.ReadBit("Change Icon", i);
                titleLen[i] = packet.ReadBits(9);
            }
            
            for (var i = 0; i < questgossips; ++i)
            {
                packet.ReadEnum<QuestFlags2>("Flags 2", TypeCode.UInt32, i);
                packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest ID", i);
                packet.ReadInt32("Level", i);
                packet.ReadUInt32("Icon", i);
                packet.ReadEnum<QuestFlags>("Flags", TypeCode.UInt32, i);
                packet.ReadWoWString("Title", titleLen[i], i);
            }

            packet.ReadXORByte(guid, 6);

            gossip.GossipOptions = new List<GossipOption>((int)AmountOfOptions);
            for (var i = 0; i < AmountOfOptions; ++i)
            {
                var gossipOption = new GossipOption
                {
                    Index = packet.ReadUInt32("Index", i),
                    OptionText = packet.ReadWoWString("Text", OptionTextLen[i], i),
                    RequiredMoney = packet.ReadUInt32("Required money", i),
                    OptionIcon = packet.ReadEnum<GossipOptionIcon>("Icon", TypeCode.Byte, i),
                    BoxText = packet.ReadWoWString("Box Text", BoxTextLen[i], i),
                    Box = packet.ReadBoolean("Box", i),
                };

                gossip.GossipOptions.Add(gossipOption);
            }

            packet.ReadXORByte(guid, 2);

            var textId = packet.ReadUInt32("Text Id");

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);

            var menuId = packet.ReadUInt32("Menu Id");
            packet.ReadUInt32("Friendship Faction");

            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Guid", guid);

            var GUID = new Guid(BitConverter.ToUInt64(guid, 0));
            gossip.ObjectType = GUID.GetObjectType();
            gossip.ObjectEntry = GUID.GetEntry();

            Storage.Gossips.Add(Tuple.Create(menuId, textId), gossip, packet.TimeSpan);
            packet.AddSniffData(StoreNameType.Gossip, (int)menuId, GUID.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.CMSG_NPC_TEXT_QUERY)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            var entry = packet.ReadInt32("Entry");

            var guid = new byte[8];
            packet.StartBitStream(guid, 0, 1, 2, 6, 4, 3, 7, 5);
            packet.ParseBitStream(guid, 3, 1, 4, 6, 2, 0, 5, 7);
            packet.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NPC_TEXT_UPDATE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var npcText = new NpcTextMop();

            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);

            var pkt = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);
            npcText.Probabilities = new float[8];
            npcText.BroadcastTextId = new uint[8];
            for (var i = 0; i < 8; ++i)
                npcText.Probabilities[i] = pkt.ReadSingle("Probability", i);
            for (var i = 0; i < 8; ++i)
                npcText.BroadcastTextId[i] = pkt.ReadUInt32("Broadcast Text Id", i);

            var entry = packet.ReadEntry("Entry");
            if (entry.Value) // Can be masked
                return;

            var hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            packet.AddSniffData(StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");

            Storage.NpcTextsMop.Add((uint)entry.Key, npcText, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_GOSSIP_POI)]
        public static void HandleGossipPoi(Packet packet)
        {
            LastGossipPOIEntry++;

            var gossipPOI = new GossipPOI();

            gossipPOI.Flags = (uint)packet.ReadEnum<UnknownFlags>("Flags", TypeCode.Int32);
            var pos = packet.ReadVector2("Coordinates");
            gossipPOI.Icon = packet.ReadEnum<GossipPOIIcon>("Icon", TypeCode.UInt32);
            gossipPOI.Data = packet.ReadUInt32("Data");
            gossipPOI.IconName = packet.ReadCString("Icon Name");

            gossipPOI.XPos = pos.X;
            gossipPOI.YPos = pos.Y;

            Storage.GossipPOIs.Add(LastGossipPOIEntry, gossipPOI, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            var npcTrainer = new NpcTrainer();

            var guid = new byte[8];

            var count = (int)packet.ReadBits(19);
            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var titleLen = packet.ReadBits(11);
            guid[6] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            packet.ReadXORByte(guid, 3);

            npcTrainer.TrainerSpells = new List<TrainerSpell>(count);
            for (var i = 0; i < count; ++i)
            {
                var trainerSpell = new TrainerSpell();
                packet.ReadEnum<TrainerSpellState>("State", TypeCode.Byte, i);
                trainerSpell.Spell = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", i);
                trainerSpell.RequiredSkill = packet.ReadUInt32("Required Skill", i);
                trainerSpell.Cost = packet.ReadUInt32("Cost", i);
                for (var j = 0; j < 3; ++j)
                    packet.ReadInt32("Int818", i, j);
                trainerSpell.RequiredLevel = packet.ReadByte("Required Level", i);
                trainerSpell.RequiredSkillLevel = packet.ReadUInt32("Required Skill Level", i);

                npcTrainer.TrainerSpells.Add(trainerSpell);
            }

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);
            npcTrainer.Title = packet.ReadWoWString("Title", titleLen);
            npcTrainer.Type = packet.ReadEnum<TrainerType>("Type", TypeCode.Int32);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadInt32("Unk Int32"); // Same unk exists in CMSG_TRAINER_BUY_SPELL

            packet.WriteGuid("Guid", guid);
            var GUID = new Guid(BitConverter.ToUInt64(guid, 0));

            if (Storage.NpcTrainers.ContainsKey(GUID.GetEntry()))
            {
                var oldTrainer = Storage.NpcTrainers[GUID.GetEntry()];
                if (oldTrainer != null)
                {
                    foreach (var trainerSpell in npcTrainer.TrainerSpells)
                        oldTrainer.Item1.TrainerSpells.Add(trainerSpell);
                }
            }
            else
                Storage.NpcTrainers.Add(GUID.GetEntry(), npcTrainer, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_LIST_INVENTORY)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            var npcVendor = new NpcVendor();

            var guid = new byte[8];

            guid[4] = packet.ReadBit();
            var itemCount = packet.ReadBits(18);
            var hasCondition = new bool[itemCount];
            var hasExtendedCost = new bool[itemCount];
            
            for (int i = 0; i < itemCount; ++i)
            {
                packet.ReadBit("Unk bit", i);
                hasCondition[i]= !packet.ReadBit();
                hasExtendedCost[i] = !packet.ReadBit();
            }

            guid[1] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[3] = packet.ReadBit();

            packet.ReadXORByte(guid, 3);

            npcVendor.VendorItems = new List<VendorItem>((int)itemCount);
            for (int i = 0; i < itemCount; ++i)
            {
                var vendorItem = new VendorItem();
                packet.ReadInt32("Max Durability", i);
                vendorItem.Type = packet.ReadUInt32("Type", i); // 1 - item, 2 - currency
                var buyCount = packet.ReadUInt32("Buy Count", i);

                if (hasExtendedCost[i])
                    vendorItem.ExtendedCostId = packet.ReadUInt32("Extended Cost", i);

                packet.ReadInt32("Price", i);
                vendorItem.ItemId = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Item ID", i);
                vendorItem.Slot = packet.ReadUInt32("Item Position", i);

                if (hasCondition[i])
                    packet.ReadInt32("Condition ID", i);

                var maxCount = packet.ReadInt32("Max Count", i);
                packet.ReadInt32("Item Upgrade ID", i);
                packet.ReadInt32("Display ID", i);

                npcVendor.VendorItems.Add(vendorItem);
            }

            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadByte("Byte10");
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);

            packet.WriteGuid("Guid5", guid);

            var vendorGUID = new Guid(BitConverter.ToUInt64(guid, 0));
            Storage.NpcVendors.Add(vendorGUID.GetEntry(), npcVendor, packet.TimeSpan);
        }
    }
}

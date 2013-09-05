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

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class NpcHandler
    {
        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        public static void HandleGossipHello(Packet packet)
        {
            var guid = new byte[8];
            guid[2] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.WriteGuid("GUID", guid);

        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            var guid = new byte[8];
            var menuEntry = packet.ReadUInt32("Menu Id");
            var gossipId = packet.ReadUInt32("Gossip Id");
            packet.StartBitStream(guid, 2, 0, 6, 4, 1, 5, 3, 7);

            var bits8 = packet.ReadBits(8);

            packet.ReadXORByte(guid, 0);
            packet.ReadWoWString("Box Text", bits8);
            packet.ReadXORBytes(guid, 6, 5, 7, 4, 3, 2, 1);

            Storage.GossipSelects.Add(Tuple.Create(menuEntry, gossipId), null, packet.TimeSpan);
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

            packet.StartBitStream(guid, 0, 1);
            var questgossips = packet.ReadBits("Amount of Quest gossips", 19);
            guid[2] = packet.ReadBit();
            var AmountOfOptions = packet.ReadBits("Amount of Options", 20);

            titleLen = new uint[questgossips];
            for (var i = 0; i < questgossips; ++i)
            {
                packet.ReadBit("Change Icon", i);
                titleLen[i] = packet.ReadBits(9);
            }
            guid[3] = packet.ReadBit();

            OptionTextLen = new uint[AmountOfOptions];
            BoxTextLen = new uint[AmountOfOptions];
            for (var i = 0; i < AmountOfOptions; ++i)
            {
                BoxTextLen[i] = packet.ReadBits(12);
                OptionTextLen[i] = packet.ReadBits(12);
            }

            packet.StartBitStream(guid, 5, 4, 6, 7);
            packet.ResetBitReader();
            packet.ReadXORByte(guid, 6);

            var gossip = new Gossip();

            gossip.GossipOptions = new List<GossipOption>((int)AmountOfOptions);
            for (var i = 0; i < AmountOfOptions; ++i)
            {
                var gossipOption = new GossipOption
                {
                    OptionText = packet.ReadWoWString("Text", OptionTextLen[i], i),
                    BoxText = packet.ReadWoWString("Box Text", BoxTextLen[i], i),
                    RequiredMoney = packet.ReadUInt32("Required money", i),
                    OptionIcon = packet.ReadEnum<GossipOptionIcon>("Icon", TypeCode.Byte, i),
                    Box = packet.ReadBoolean("Box", i),
                    Index = packet.ReadUInt32("Index", i)
                };

                gossip.GossipOptions.Add(gossipOption);
            }
            packet.ReadXORByte(guid, 0);

            for (var i = 0; i < questgossips; i++)
            {
                packet.ReadEnum<QuestFlags>("Flags", TypeCode.UInt32, i);
                packet.ReadWoWString("Title", titleLen[i], i);
                packet.ReadUInt32("Icon", i);
                packet.ReadEnum<QuestFlags2>("Flags 2", TypeCode.UInt32, i);
                packet.ReadInt32("Level", i);
                packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest ID", i);
            }

            var textId = packet.ReadUInt32("Text Id");
            packet.ReadXORBytes(guid, 4, 3);
            var menuId = packet.ReadUInt32("Menu Id");
            packet.ReadUInt32("Friendship Faction");
            packet.ReadXORBytes(guid, 7, 1, 5, 2);
            packet.WriteGuid("GUID", guid);

            var GUID = new Guid(BitConverter.ToUInt64(guid, 0));
            gossip.ObjectType = GUID.GetObjectType();
            gossip.ObjectEntry = GUID.GetEntry();

            Storage.Gossips.Add(Tuple.Create(menuId, textId), gossip, packet.TimeSpan);
            packet.AddSniffData(StoreNameType.Gossip, (int)menuId, GUID.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            var guid = new byte[8];
            var npcTrainer = new NpcTrainer();

            guid[0] = packet.ReadBit();
            var count = (int)packet.ReadBits("Count", 19);
            packet.StartBitStream(guid, 2, 6);
            var titleLen = packet.ReadBits(11);
            packet.StartBitStream(guid, 3, 7, 1, 4, 5);
            packet.ResetBitReader();

            npcTrainer.TrainerSpells = new List<TrainerSpell>(count);
            for (var i = 0; i < count; ++i)
            {
                var trainerSpell = new TrainerSpell();
                trainerSpell.RequiredSkill = packet.ReadUInt32("Required Skill", i);
                trainerSpell.RequiredSkillLevel = packet.ReadUInt32("Required Skill Level", i);
                trainerSpell.Cost = packet.ReadUInt32("Cost", i);

                trainerSpell.RequiredLevel = packet.ReadByte("Required Level", i);
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Required Spell ID", i);
                packet.ReadInt32("Profession Dialog", i);
                packet.ReadInt32("Profession Button", i);

                trainerSpell.Spell = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", i);
                packet.ReadEnum<TrainerSpellState>("State", TypeCode.Byte, i);

                npcTrainer.TrainerSpells.Add(trainerSpell);
            }

            packet.ReadXORBytes(guid, 3, 2);
            npcTrainer.Title = packet.ReadWoWString("Title", titleLen);
            packet.ReadXORBytes(guid, 7, 6, 4, 1, 0, 5);

            npcTrainer.Type = packet.ReadEnum<TrainerType>("Type", TypeCode.Int32);
            packet.ReadInt32("Unk Int32"); // Same unk exists in CMSG_TRAINER_BUY_SPELL

            packet.WriteGuid("GUID", guid);
            var GUID = new Guid(BitConverter.ToUInt64(guid, 0));
            Storage.NpcTrainers.Add(GUID.GetEntry(), npcTrainer, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_LIST_INVENTORY)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            var npcVendor = new NpcVendor();
            var guid = new byte[8];

            packet.StartBitStream(guid, 5, 4, 7, 1, 2, 3);
            var itemCount = packet.ReadBits("Item Count", 18);

            var hasExtendedCost = new bool[itemCount];
            var hasCondition = new bool[itemCount];
            for (int i = 0; i < itemCount; ++i)
            {
                hasCondition[i] = !packet.ReadBit();
                hasExtendedCost[i] = !packet.ReadBit();
                packet.ReadBit("Unk bit", i);
            }

            packet.StartBitStream(guid, 6, 0);
            packet.ResetBitReader();
            packet.ReadXORBytes(guid, 3, 4);

            npcVendor.VendorItems = new List<VendorItem>((int)itemCount);
            for (int i = 0; i < itemCount; ++i)
            {
                var vendorItem = new VendorItem();

                vendorItem.ItemId = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Item ID", i);
                vendorItem.Slot = packet.ReadUInt32("Item Position", i);
                packet.ReadInt32("Item Upgrade ID", i);
                packet.ReadInt32("Display ID", i);
                var maxCount = packet.ReadInt32("Max Count", i);
                var buyCount = packet.ReadUInt32("Buy Count", i);
                packet.ReadInt32("Price", i);

                if (hasCondition[i])
                    packet.ReadInt32("Condition ID", i);

                vendorItem.Type = packet.ReadUInt32("Type", i); // 1 - item, 2 - currency
                packet.ReadInt32("Max Durability", i);
                if (hasExtendedCost[i])
                    vendorItem.ExtendedCostId = packet.ReadUInt32("Extended Cost", i);

                vendorItem.MaxCount = maxCount == -1 ? 0 : maxCount; // TDB
                if (vendorItem.Type == 2)
                    vendorItem.MaxCount = (int)buyCount;

                npcVendor.VendorItems.Add(vendorItem);
            }

            packet.ReadXORBytes(guid, 1, 2, 7);
            packet.ReadByte("Unk Byte");
            packet.ReadXORBytes(guid, 6, 0, 5);

            packet.WriteGuid("GUID", guid);
            var GUID = new Guid(BitConverter.ToUInt64(guid, 0));

            Storage.NpcVendors.Add(GUID.GetEntry(), npcVendor, packet.TimeSpan);
        }

    }
}

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

namespace WowPacketParserModule.V5_4_7_18019.Parsers
{
    public static class NpcHandler
    {
        [Parser(Opcode.CMSG_BANKER_ACTIVATE)]
        public static void HandleBankerActivate(Packet packet)
        {
            var guid = packet.StartBitStream(1, 4, 2, 7, 5, 0, 3, 6);
            packet.ParseBitStream(guid, 4, 2, 5, 7, 0, 6, 3, 1);
            packet.WriteGuid("NPC Guid", guid);
        }

        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        public static void HandleNpcHello(Packet packet)
        {
            var guid = packet.StartBitStream(6, 3, 4, 5, 1, 7, 2, 0);
            packet.ParseBitStream(guid, 6, 0, 1, 7, 2, 5, 4, 3);

            packet.WriteGuid("NPC Guid", guid);
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

            var boxTextLength = packet.ReadBits(8);

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);

            packet.ReadWoWString("Box Text", boxTextLength);

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 4);

            packet.WriteGuid("NPC Guid", guid);

            Storage.GossipSelects.Add(Tuple.Create(menuEntry, gossipId), null, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_LIST_INVENTORY)]
        public static void HandleNpcListInventory(Packet packet)
        {
            var guid = packet.StartBitStream(1, 0, 6, 3, 5, 4, 7, 2);
            packet.ParseBitStream(guid, 0, 5, 6, 7, 1, 3, 4, 2);

            packet.WriteGuid("NPC Guid", guid);
        }

        [Parser(Opcode.CMSG_TRAINER_BUY_SPELL)]
        public static void HandleTrainerBuySpell(Packet packet)
        {
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell Id");
            packet.ReadUInt32("Trainer Id");

            var guid = packet.StartBitStream(6, 2, 0, 7, 5, 3, 1, 4);
            packet.ParseBitStream(guid, 6, 0, 5, 1, 7, 4, 2, 3);

            packet.WriteGuid("NPC Guid", guid);
        }

        [Parser(Opcode.CMSG_TRAINER_LIST)]
        public static void HandleTrainerList(Packet packet)
        {
            var guid = packet.StartBitStream(2, 7, 1, 0, 3, 5, 4, 6);
            packet.ParseBitStream(guid, 3, 0, 2, 1, 7, 6, 4, 5);

            packet.WriteGuid("NPC Guid", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            var guid = new byte[8];
            uint[] QuestTitleLength;
            uint[] OptionsMessageLength;
            uint[] BoxMessageLength;

            guid[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            var QuestsCount = packet.ReadBits("Quests Count", 19);

            guid[0] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();

            var OptionsCount = packet.ReadBits("Gossip Options Count", 20);

            OptionsMessageLength = new uint[OptionsCount];
            BoxMessageLength = new uint[OptionsCount];
            for (var i = 0; i < OptionsCount; ++i)
            {
                BoxMessageLength[i] = packet.ReadBits(12);
                OptionsMessageLength[i] = packet.ReadBits(12);
            }

            QuestTitleLength = new uint[QuestsCount];
            for (var i = 0; i < QuestsCount; ++i)
            {
                packet.ReadBit("Quest Icon Change", i);
                QuestTitleLength[i] = packet.ReadBits(9);
            }

            for (var i = 0; i < QuestsCount; i++)
            {
                packet.ReadEnum<QuestFlags2>("Quest Special Flags", TypeCode.UInt32, i);
                packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest Id", i);
                packet.ReadUInt32("Quest Level", i);
                packet.ReadUInt32("Quest Icon", i);
                packet.ReadEnum<QuestFlags>("Quest Flags", TypeCode.UInt32, i);
                packet.ReadWoWString("Quest Titles", QuestTitleLength[i], i);
            }

            packet.ReadXORByte(guid, 6);

            var ObjectGossip = new Gossip();
            ObjectGossip.GossipOptions = new List<GossipOption>((int)OptionsCount);
            for (var i = 0; i < OptionsCount; ++i)
            {
                var gossipMenuOption = new GossipOption
                {
                    RequiredMoney = packet.ReadUInt32("Message Box Required Money", i),
                    OptionText = packet.ReadWoWString("Gossip Option Text", OptionsMessageLength[i], i),
                    Index = packet.ReadUInt32("Gossip Option Index", i),
                    OptionIcon = packet.ReadEnum<GossipOptionIcon>("Gossip Option Icon", TypeCode.Byte, i),
                    BoxText = packet.ReadWoWString("Message Box Text", BoxMessageLength[i], i),
                    Box = packet.ReadBoolean("Message Box Coded", i), // True if it has a password.
                };

                ObjectGossip.GossipOptions.Add(gossipMenuOption);
            }

            packet.ReadXORByte(guid, 2);

            var GossipMenuTextId = packet.ReadUInt32("Gossip Menu Text Id");

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);

            var GossipMenuId = packet.ReadUInt32("Gossip Menu Id");
            packet.ReadUInt32("Friendly Faction Id");

            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Object Guid", guid);

            var ObjectGuid = new Guid(BitConverter.ToUInt64(guid, 0));
            ObjectGossip.ObjectEntry = ObjectGuid.GetEntry();
            ObjectGossip.ObjectType = ObjectGuid.GetObjectType();

            if (ObjectGuid.GetObjectType() == ObjectType.Unit)
                if (Storage.Objects.ContainsKey(ObjectGuid))
                    ((Unit)Storage.Objects[ObjectGuid].Item1).GossipId = GossipMenuId;

            Storage.Gossips.Add(Tuple.Create(GossipMenuId, GossipMenuTextId), ObjectGossip, packet.TimeSpan);
            packet.AddSniffData(StoreNameType.Gossip, (int)GossipMenuId, ObjectGuid.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.SMSG_LIST_INVENTORY)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            var npcVendor = new NpcVendor();

            var guidBytes = new byte[8];

            guidBytes[4] = packet.ReadBit();

            var itemCount = packet.ReadBits("Item Count", 18);

            var hasExtendedCost = new bool[itemCount];
            var hasCondition = new bool[itemCount];
            var hasCondition2 = new bool[itemCount];
            for (int i = 0; i < itemCount; ++i)
            {
                hasCondition[i] = !packet.ReadBit();
                hasCondition2[i] = !packet.ReadBit();
                hasExtendedCost[i] = !packet.ReadBit();
            }

            guidBytes[1] = packet.ReadBit();
            guidBytes[6] = packet.ReadBit();
            guidBytes[2] = packet.ReadBit();
            guidBytes[5] = packet.ReadBit();
            guidBytes[7] = packet.ReadBit();
            guidBytes[0] = packet.ReadBit();
            guidBytes[3] = packet.ReadBit();

            packet.ReadXORByte(guidBytes, 3);

            npcVendor.VendorItems = new List<VendorItem>((int)itemCount);
            for (int i = 0; i < itemCount; ++i)
            {
                var vendorItem = new VendorItem();

                //packet.ReadInt32("Max Durability", i);
                packet.ReadInt32("Unk1", i);
                vendorItem.Type = packet.ReadUInt32("Type", i); // 1 - item, 2 - currency
                var buyCount = packet.ReadUInt32("Buy Count", i);
                if (hasExtendedCost[i])
                    vendorItem.ExtendedCostId = packet.ReadUInt32("Extended Cost", i);
                packet.ReadInt32("Price", i);
                vendorItem.ItemId = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Item ID", i);
                vendorItem.Slot = packet.ReadUInt32("Item Position", i);
                packet.ReadInt32("LeftInStock", i);
                packet.ReadInt32("Unk2", i);
                packet.ReadInt32("Display ID", i);
                /*if (hasCondition[i])
                    packet.ReadInt32("Condition ID", i);
                var maxCount = packet.ReadInt32("Max Count", i);
                vendorItem.MaxCount = maxCount == -1 ? 0 : maxCount; // TDB

                if (vendorItem.Type == 2)
                    vendorItem.MaxCount = (int)buyCount;*/

                npcVendor.VendorItems.Add(vendorItem);
            }

            packet.ReadXORByte(guidBytes, 6);
            packet.ReadXORByte(guidBytes, 0);
            packet.ReadXORByte(guidBytes, 2);
            packet.ReadXORByte(guidBytes, 5);

            packet.ReadByte("rawItemCount");

            packet.ReadXORByte(guidBytes, 1);
            packet.ReadXORByte(guidBytes, 4);
            packet.ReadXORByte(guidBytes, 7);

            var guid = new Guid(BitConverter.ToUInt64(guidBytes, 0));
            packet.WriteLine("GUID: {0}", guid);

            Storage.NpcVendors.Add(guid.GetEntry(), npcVendor, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_SHOW_BANK)]
        public static void HandleShowBank(Packet packet)
        {
            var guid = packet.StartBitStream(7, 1, 6, 4, 3, 5, 0, 2);
            packet.ParseBitStream(guid, 6, 0, 7, 3, 5, 1, 4, 2);

            packet.WriteGuid("NPC Guid", guid);
        }

        [Parser(Opcode.SMSG_TRAINER_BUY_SUCCEEDED)]
        public static void HandleServerTrainerBuySucceedeed(Packet packet)
        {
            packet.AsHex();
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            var npcTrainer = new NpcTrainer();

            var count = packet.ReadBits("Count", 19);

            var guid = new byte[8];

            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var size = packet.ReadBits("Title size", 11);
            guid[6] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            //npcTrainer.Type = packet.ReadEnum<TrainerType>("Type", TypeCode.Int32);

            packet.ReadXORByte(guid, 3);

            npcTrainer.TrainerSpells = new List<TrainerSpell>((Int32)count);

            for (var i = 0; i < count; ++i)
            {
                var trainerSpell = new TrainerSpell();

                packet.ReadEnum<TrainerSpellState>("State", TypeCode.Byte, i);

                trainerSpell.Spell = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", i);

                trainerSpell.RequiredSkill = packet.ReadUInt32("Required Skill", i);
                trainerSpell.Cost = packet.ReadUInt32("Cost", i);
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Chain Spell ID", i, 0);
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Chain Spell ID", i, 1);
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Chain Spell ID", i, 2);
                trainerSpell.RequiredLevel = packet.ReadByte("Required Level", i);
                trainerSpell.RequiredSkillLevel = packet.ReadUInt32("Required Skill Level", i);

                npcTrainer.TrainerSpells.Add(trainerSpell);
            }

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);

            npcTrainer.Title = packet.ReadWoWString("Title", size);
            npcTrainer.Type = packet.ReadEnum<TrainerType>("Type", TypeCode.Int32);

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);

            packet.ReadInt32("unk1");

            var guiD = new Guid(BitConverter.ToUInt64(guid, 0));

            Storage.NpcTrainers.Add(guiD.GetEntry(), npcTrainer, packet.TimeSpan);
        }

        /*[Parser(Opcode.SMSG_SHOW_BANK)]
        public static void HandleShowBank(Packet packet)
        {
            var guid = packet.StartBitStream(6, 0, 4, 3, 2, 1, 7, 5);
            packet.ParseBitStream(guid, 7, 2, 0, 1, 6, 5, 3, 4);
            packet.WriteGuid("NPC Guid", guid);
        }*/

    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class NpcHandler
    {
        public static uint LastGossipPOIEntry;

        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        public static void HandleGossipHello(Packet packet)
        {
            var guid = new byte[8];
            packet.StartBitStream(guid, 5, 2, 0, 4, 7, 1, 6, 3);
            packet.ParseBitStream(guid, 3, 4, 6, 1, 0, 2, 7, 5);

            packet.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            var gossip = new Gossip();

            var guidBytes = new byte[8];

            guidBytes[7] = packet.ReadBit();
            guidBytes[6] = packet.ReadBit();
            guidBytes[0] = packet.ReadBit();

            var questgossips = (int)packet.ReadBits(19);

            guidBytes[4] = packet.ReadBit();

            var titleLen = new uint[questgossips];
            for (var i = 0; i < questgossips; ++i)
            {
                titleLen[i] = packet.ReadBits(9);
                packet.ReadBit("Change Icon", i);
            }

            guidBytes[3] = packet.ReadBit();
            guidBytes[2] = packet.ReadBit();

            var amountOfOptions = packet.ReadBits(20);

            var boxTextLen = new uint[amountOfOptions];
            var optionTextLen = new uint[amountOfOptions];
            for (var i = 0; i < amountOfOptions; ++i)
            {
                boxTextLen[i] = packet.ReadBits(12);
                optionTextLen[i] = packet.ReadBits(12);
            }

            guidBytes[1] = packet.ReadBit();
            guidBytes[5] = packet.ReadBit();

            packet.ReadXORByte(guidBytes, 2);

            gossip.GossipOptions = new List<GossipOption>((int)amountOfOptions);
            for (var i = 0; i < amountOfOptions; ++i)
            {
                var gossipOption = new GossipOption
                {
                    BoxText = packet.ReadWoWString("Box Text", boxTextLen[i], i),
                    OptionText = packet.ReadWoWString("Text", optionTextLen[i], i),
                    Index = packet.ReadUInt32("Index", i),
                    Box = packet.ReadBool("Box", i),
                    OptionIcon = packet.ReadByteE<GossipOptionIcon>("Icon", i),
                    RequiredMoney = packet.ReadUInt32("Required money", i)
                };

                gossip.GossipOptions.Add(gossipOption);
            }

            for (var i = 0; i < questgossips; ++i)
            {
                packet.ReadInt32("Level", i);
                packet.ReadUInt32E<QuestFlags>("Flags", i);
                packet.ReadUInt32("Icon", i);
                packet.ReadWoWString("Title", titleLen[i], i);
                packet.ReadUInt32<QuestId>("Quest ID", i);
                packet.ReadUInt32E<QuestFlags2>("Flags 2", i);
            }

            packet.ReadXORByte(guidBytes, 7);

            packet.ReadUInt32("Friendship Faction");

            packet.ReadXORByte(guidBytes, 3);
            packet.ReadXORByte(guidBytes, 1);

            var textId = packet.ReadUInt32("Text Id");

            packet.ReadXORByte(guidBytes, 5);

            var menuId = packet.ReadUInt32("Menu Id");

            packet.ReadXORByte(guidBytes, 6);
            packet.ReadXORByte(guidBytes, 4);
            packet.ReadXORByte(guidBytes, 0);

            var guid = packet.WriteGuid("Guid", guidBytes);

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

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            var guid = new byte[8];

            var menuEntry = packet.ReadUInt32("Menu Id");
            var gossipId = packet.ReadUInt32("Gossip Id");

            guid[3] = packet.ReadBit();
            var bits8 = packet.ReadBits(8);
            guid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 0);
            packet.ReadWoWString("Box Text", bits8);
            packet.ReadXORByte(guid, 3);

            Storage.GossipSelects.Add(Tuple.Create(menuEntry, gossipId), null, packet.TimeSpan);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_GOSSIP_POI)]
        public static void HandleGossipPoi(Packet packet)
        {
            LastGossipPOIEntry++;

            var gossipPOI = new GossipPOI();

            gossipPOI.Flags = (uint)packet.ReadInt32E<UnknownFlags>("Flags");
            var pos = packet.ReadVector2("Coordinates");
            gossipPOI.Icon = packet.ReadUInt32E<GossipPOIIcon>("Icon");
            gossipPOI.Importance = packet.ReadUInt32("Data");
            gossipPOI.Name = packet.ReadCString("Icon Name");

            gossipPOI.PositionX = pos.X;
            gossipPOI.PositionY = pos.Y;

            Storage.GossipPOIs.Add(LastGossipPOIEntry, gossipPOI, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_QUERY_NPC_TEXT)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            packet.ReadInt32("Entry");

            var guid = new byte[8];
            packet.StartBitStream(guid, 5, 6, 7, 4, 3, 0, 2, 1);
            packet.ParseBitStream(guid, 0, 7, 1, 4, 3, 5, 2, 6);
            packet.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var npcText = new NpcTextMop();

            var hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

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

            pkt.ClosePacket(false);

            packet.AddSniffData(StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");

            Storage.NpcTextsMop.Add((uint)entry.Key, npcText, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            var npcVendor = new NpcVendor();

            var guid = new byte[8];

            var itemCount = (int)packet.ReadBits(18);

            guid[0] = packet.ReadBit();

            var hasExtendedCost = new bool[itemCount];
            var hasCondition = new bool[itemCount];

            for (int i = 0; i < itemCount; ++i)
            {
                packet.ReadBit("Unk bit", i);
                hasExtendedCost[i] = !packet.ReadBit();
                hasCondition[i] = !packet.ReadBit();
            }

            guid[3] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 6);

            npcVendor.VendorItems = new List<VendorItem>(itemCount);
            for (int i = 0; i < itemCount; ++i)
            {
                var vendorItem = new VendorItem();

                vendorItem.Type = packet.ReadUInt32("Type", i); // 1 - item, 2 - currency
                var buyCount = packet.ReadUInt32("Buy Count", i);
                var maxCount = packet.ReadInt32("Max Count", i);
                packet.ReadInt32("Display ID", i);
                vendorItem.Slot = packet.ReadUInt32("Item Position", i);
                packet.ReadInt32("Max Durability", i);
                packet.ReadInt32("Price", i);

                if (hasExtendedCost[i])
                    vendorItem.ExtendedCostId = packet.ReadUInt32("Extended Cost", i);

                packet.ReadInt32("Item Upgrade ID", i);
                vendorItem.ItemId = (uint)packet.ReadInt32<ItemId>("Item ID", i);

                if (hasCondition[i])
                    packet.ReadInt32("Condition ID", i);

                npcVendor.VendorItems.Add(vendorItem);
            }

            packet.ReadByte("Byte28");

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 4);

            packet.WriteGuid("Guid", guid);

            var vendorGUID = new WowGuid64(BitConverter.ToUInt64(guid, 0));
            Storage.NpcVendors.Add(vendorGUID.GetEntry(), npcVendor, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            var npcTrainer = new NpcTrainer();

            var guidBytes = new byte[8];

            guidBytes[1] = packet.ReadBit();
            guidBytes[6] = packet.ReadBit();

            var titleLen = packet.ReadBits(11);

            guidBytes[3] = packet.ReadBit();
            guidBytes[2] = packet.ReadBit();
            guidBytes[5] = packet.ReadBit();
            guidBytes[4] = packet.ReadBit();
            guidBytes[0] = packet.ReadBit();
            guidBytes[7] = packet.ReadBit();

            var count = (int)packet.ReadBits("Count", 19);

            npcTrainer.Type = packet.ReadInt32E<TrainerType>("Type");

            npcTrainer.TrainerSpells = new List<TrainerSpell>(count);
            for (var i = 0; i < count; ++i)
            {
                var trainerSpell = new TrainerSpell();
                for (var j = 0; j < 3; ++j)
                    packet.ReadInt32("Int818", i, j);

                packet.ReadByteE<TrainerSpellState>("State", i);
                trainerSpell.RequiredSkill = packet.ReadUInt32("Required Skill", i);
                trainerSpell.Spell = (uint)packet.ReadInt32<SpellId>("Spell ID", i);
                trainerSpell.Cost = packet.ReadUInt32("Cost", i);
                trainerSpell.RequiredLevel = packet.ReadByte("Required Level", i);
                trainerSpell.RequiredSkillLevel = packet.ReadUInt32("Required Skill Level", i);

                npcTrainer.TrainerSpells.Add(trainerSpell);
            }

            packet.ReadXORByte(guidBytes, 7);
            packet.ReadXORByte(guidBytes, 5);
            packet.ReadXORByte(guidBytes, 2);
            packet.ReadXORByte(guidBytes, 0);
            packet.ReadXORByte(guidBytes, 1);

            npcTrainer.Title = packet.ReadWoWString("Title", titleLen);

            packet.ReadXORByte(guidBytes, 6);
            packet.ReadXORByte(guidBytes, 4);

            packet.ReadInt32("Unk Int32"); // Same unk exists in CMSG_TRAINER_BUY_SPELL

            packet.ReadXORByte(guidBytes, 3);

            var guid = packet.WriteGuid("Guid", guidBytes);

            if (Storage.NpcTrainers.ContainsKey(guid.GetEntry()))
            {
                var oldTrainer = Storage.NpcTrainers[guid.GetEntry()];
                if (oldTrainer != null)
                {
                    foreach (var trainerSpell in npcTrainer.TrainerSpells)
                        oldTrainer.Item1.TrainerSpells.Add(trainerSpell);
                }
            }
            else
                Storage.NpcTrainers.Add(guid.GetEntry(), npcTrainer, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_BANKER_ACTIVATE)]
        public static void HandleBankerActivate(Packet packet)
        {
            var guid = packet.StartBitStream(0, 1, 5, 2, 4, 3, 7, 6);
            packet.ParseBitStream(guid, 3, 6, 2, 0, 7, 4, 5, 1);

            packet.WriteGuid("Guid", guid);
        }
    }
}

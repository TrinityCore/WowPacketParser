using System;
using System.Collections.Generic;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class NpcHandler
    {
        public static uint LastGossipPOIEntry;

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

            var guidBytes = new byte[8];

            guidBytes[7] = packet.ReadBit();
            guidBytes[6] = packet.ReadBit();
            guidBytes[1] = packet.ReadBit();

            var questgossips = packet.ReadBits(19);

            guidBytes[0] = packet.ReadBit();
            guidBytes[4] = packet.ReadBit();
            guidBytes[5] = packet.ReadBit();
            guidBytes[2] = packet.ReadBit();
            guidBytes[3] = packet.ReadBit();

            var amountOfOptions = packet.ReadBits(20);

            var boxTextLen = new uint[amountOfOptions];
            var optionTextLen = new uint[amountOfOptions];
            for (var i = 0; i < amountOfOptions; ++i)
            {
                boxTextLen[i] = packet.ReadBits(12);
                optionTextLen[i] = packet.ReadBits(12);
            }

            var titleLen = new uint[questgossips];
            for (var i = 0; i < questgossips; ++i)
            {
                packet.ReadBit("Change Icon", i);
                titleLen[i] = packet.ReadBits(9);
            }

            for (var i = 0; i < questgossips; ++i)
            {
                packet.ReadUInt32E<QuestFlags2>("Flags 2", i);
                packet.ReadUInt32<QuestId>("Quest ID", i);
                packet.ReadInt32("Level", i);
                packet.ReadUInt32("Icon", i);
                packet.ReadUInt32E<QuestFlags>("Flags", i);
                packet.ReadWoWString("Title", titleLen[i], i);
            }

            packet.ReadXORByte(guidBytes, 6);

            gossip.GossipOptions = new List<GossipOption>((int)amountOfOptions);
            for (var i = 0; i < amountOfOptions; ++i)
            {
                var gossipOption = new GossipOption
                {
                    RequiredMoney = packet.ReadUInt32("Required money", i),
                    OptionText = packet.ReadWoWString("Text", optionTextLen[i], i),
                    Index = packet.ReadUInt32("Index", i),
                    OptionIcon = packet.ReadByteE<GossipOptionIcon>("Icon", i),
                    BoxText = packet.ReadWoWString("Box Text", boxTextLen[i], i),
                    Box = packet.ReadBool("Box", i)
                };

                gossip.GossipOptions.Add(gossipOption);
            }

            packet.ReadXORByte(guidBytes, 2);

            var textId = packet.ReadUInt32("Text Id");

            packet.ReadXORByte(guidBytes, 1);
            packet.ReadXORByte(guidBytes, 5);

            var menuId = packet.ReadUInt32("Menu Id");
            packet.ReadUInt32("Friendship Faction");

            packet.ReadXORByte(guidBytes, 4);
            packet.ReadXORByte(guidBytes, 7);
            packet.ReadXORByte(guidBytes, 3);
            packet.ReadXORByte(guidBytes, 0);

            packet.WriteGuid("Guid", guidBytes);

            var guid = new WowGuid64(BitConverter.ToUInt64(guidBytes, 0));
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

        [Parser(Opcode.CMSG_QUERY_NPC_TEXT)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            packet.ReadInt32("Entry");

            var guid = new byte[8];
            packet.StartBitStream(guid, 0, 1, 2, 6, 4, 3, 7, 5);
            packet.ParseBitStream(guid, 3, 1, 4, 6, 2, 0, 5, 7);
            packet.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE)]
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

            pkt.ClosePacket(false);

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

            gossipPOI.Flags = (uint)packet.ReadInt32E<UnknownFlags>("Flags");
            var pos = packet.ReadVector2("Coordinates");
            gossipPOI.Icon = packet.ReadUInt32E<GossipPOIIcon>("Icon");
            gossipPOI.Importance = packet.ReadUInt32("Data");
            gossipPOI.Name = packet.ReadCString("Icon Name");

            gossipPOI.PositionX = pos.X;
            gossipPOI.PositionY = pos.Y;

            Storage.GossipPOIs.Add(LastGossipPOIEntry, gossipPOI, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            var npcTrainer = new NpcTrainer();

            var guidBytes = new byte[8];

            var count = (int)packet.ReadBits(19);
            guidBytes[3] = packet.ReadBit();
            guidBytes[2] = packet.ReadBit();
            guidBytes[0] = packet.ReadBit();
            guidBytes[7] = packet.ReadBit();
            guidBytes[1] = packet.ReadBit();
            guidBytes[5] = packet.ReadBit();
            var titleLen = packet.ReadBits(11);
            guidBytes[6] = packet.ReadBit();
            guidBytes[4] = packet.ReadBit();

            packet.ReadXORByte(guidBytes, 3);

            npcTrainer.TrainerSpells = new List<TrainerSpell>(count);
            for (var i = 0; i < count; ++i)
            {
                var trainerSpell = new TrainerSpell();
                packet.ReadByteE<TrainerSpellState>("State", i);
                trainerSpell.Spell = (uint)packet.ReadInt32<SpellId>("Spell ID", i);
                trainerSpell.RequiredSkill = packet.ReadUInt32("Required Skill", i);
                trainerSpell.Cost = packet.ReadUInt32("Cost", i);
                for (var j = 0; j < 3; ++j)
                    packet.ReadInt32("Int818", i, j);
                trainerSpell.RequiredLevel = packet.ReadByte("Required Level", i);
                trainerSpell.RequiredSkillLevel = packet.ReadUInt32("Required Skill Level", i);

                npcTrainer.TrainerSpells.Add(trainerSpell);
            }

            packet.ReadXORByte(guidBytes, 1);
            packet.ReadXORByte(guidBytes, 6);
            packet.ReadXORByte(guidBytes, 0);
            npcTrainer.Title = packet.ReadWoWString("Title", titleLen);
            npcTrainer.Type = packet.ReadInt32E<TrainerType>("Type");
            packet.ReadXORByte(guidBytes, 2);
            packet.ReadXORByte(guidBytes, 4);
            packet.ReadXORByte(guidBytes, 5);
            packet.ReadXORByte(guidBytes, 7);
            packet.ReadInt32("Unk Int32"); // Same unk exists in CMSG_TRAINER_BUY_SPELL

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

        [Parser(Opcode.CMSG_TRAINER_BUY_SPELL)]
        public static void HandleTrainerBuySpell(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("Unknown Int32"); // same unk exists in SMSG_TRAINER_LIST
            packet.ReadInt32<SpellId>("Spell ID");

            packet.StartBitStream(guid, 6, 2, 0, 7, 5, 3, 1, 4);
            packet.ParseBitStream(guid, 6, 0, 5, 1, 7, 4, 2, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_LIST_INVENTORY)]
        public static void HandleNpcListInventory(Packet packet)
        {
            var guid = packet.StartBitStream(1, 0, 6, 3, 5, 4, 7, 2);
            packet.ParseBitStream(guid, 0, 5, 6, 7, 1, 3, 4, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY)]
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
                vendorItem.ItemId = (uint)packet.ReadInt32<ItemId>("Item ID", i);
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

            packet.WriteGuid("Guid", guid);

            var vendorGUID = new WowGuid64(BitConverter.ToUInt64(guid, 0));
            Storage.NpcVendors.Add(vendorGUID.GetEntry(), npcVendor, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_BUY_BANK_SLOT)]
        public static void HandleBuyBankSlot(Packet packet)
        {
            var guid2 = new byte[8];

            guid2[7] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[6] = packet.ReadBit();

            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 5);

            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.CMSG_BANKER_ACTIVATE)]
        public static void HandleBankerActivate(Packet packet)
        {
            var guid2 = new byte[8];

            guid2[3] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[2] = packet.ReadBit();

            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 5);

            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_SHOW_BANK)]
        public static void HandleShowBank(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 7, 1, 6, 4, 3, 5, 0, 2);
            packet.ParseBitStream(guid, 6, 0, 7, 3, 5, 1, 4, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_THREAT_UPDATE)]
        public static void HandleThreatlistUpdate(Packet packet)
        {
            var guid = new byte[8];

            var count = packet.ReadBits("Size", 21);

            var hostileGUID = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                hostileGUID[i] = new byte[8];
                packet.StartBitStream(hostileGUID[i], 6, 5, 3, 0, 4, 7, 2, 1);
            }

            packet.StartBitStream(guid, 7, 2, 4, 5, 0, 6, 1, 3);

            for (var i = 0; i < count; ++i)
            {
                packet.ParseBitStream(hostileGUID[i], 3, 7, 0, 1, 6, 5);
                packet.ReadUInt32("Threat", i);
                packet.ParseBitStream(hostileGUID[i], 4, 2);
                packet.WriteGuid("Hostile", hostileGUID[i], i);

            }

            packet.ParseBitStream(guid, 4, 6, 5, 3, 0, 1, 7, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_HIGHEST_THREAT_UPDATE)]
        public static void HandleHighestThreatlistUpdate(Packet packet)
        {
            var newHighestGUID = new byte[8];
            var guid = new byte[8];

            newHighestGUID[1] = packet.ReadBit();
            newHighestGUID[5] = packet.ReadBit();
            newHighestGUID[3] = packet.ReadBit();
            newHighestGUID[4] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            newHighestGUID[6] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var count = packet.ReadBits(21);
            guid[3] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            var hostileGUID = new byte[count][];

            for (var i = 0; i < count; i++)
            {
                hostileGUID[i] = new byte[8];

                packet.StartBitStream(hostileGUID[i], 7, 6, 3, 2, 0, 5, 1, 4);
            }

            guid[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            newHighestGUID[7] = packet.ReadBit();
            newHighestGUID[2] = packet.ReadBit();
            newHighestGUID[0] = packet.ReadBit();
            packet.ReadXORByte(newHighestGUID, 7);

            for (var i = 0; i < count; i++)
            {
                packet.ReadXORByte(hostileGUID[i], 1);
                packet.ReadXORByte(hostileGUID[i], 2);
                packet.ReadXORByte(hostileGUID[i], 3);
                packet.ReadXORByte(hostileGUID[i], 0);
                packet.ReadXORByte(hostileGUID[i], 4);
                packet.ReadXORByte(hostileGUID[i], 7);

                packet.ReadInt32("Threat", i);

                packet.ReadXORByte(hostileGUID[i], 6);
                packet.ReadXORByte(hostileGUID[i], 5);

                packet.WriteGuid("Hostile", hostileGUID[i], i);
            }

            packet.ReadXORByte(newHighestGUID, 4);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(newHighestGUID, 3);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(newHighestGUID, 0);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(newHighestGUID, 5);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(newHighestGUID, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(newHighestGUID, 6);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(newHighestGUID, 1);

            packet.WriteGuid("New Highest", newHighestGUID);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_THREAT_REMOVE)]
        public static void HandleRemoveThreatlist(Packet packet)
        {
            var hostileGUID = new byte[8];
            var victimGUID = new byte[8];

            victimGUID[2] = packet.ReadBit();
            victimGUID[1] = packet.ReadBit();
            hostileGUID[3] = packet.ReadBit();
            victimGUID[4] = packet.ReadBit();
            victimGUID[6] = packet.ReadBit();
            hostileGUID[6] = packet.ReadBit();
            victimGUID[3] = packet.ReadBit();
            hostileGUID[7] = packet.ReadBit();
            hostileGUID[4] = packet.ReadBit();
            victimGUID[5] = packet.ReadBit();
            hostileGUID[2] = packet.ReadBit();
            hostileGUID[5] = packet.ReadBit();
            hostileGUID[1] = packet.ReadBit();
            hostileGUID[0] = packet.ReadBit();
            victimGUID[7] = packet.ReadBit();
            victimGUID[0] = packet.ReadBit();

            packet.ReadXORByte(victimGUID, 1);
            packet.ReadXORByte(hostileGUID, 3);
            packet.ReadXORByte(victimGUID, 3);
            packet.ReadXORByte(victimGUID, 6);
            packet.ReadXORByte(victimGUID, 0);
            packet.ReadXORByte(hostileGUID, 0);
            packet.ReadXORByte(hostileGUID, 4);
            packet.ReadXORByte(hostileGUID, 1);
            packet.ReadXORByte(hostileGUID, 6);
            packet.ReadXORByte(victimGUID, 4);
            packet.ReadXORByte(victimGUID, 5);
            packet.ReadXORByte(hostileGUID, 7);
            packet.ReadXORByte(victimGUID, 7);
            packet.ReadXORByte(victimGUID, 2);
            packet.ReadXORByte(hostileGUID, 2);
            packet.ReadXORByte(hostileGUID, 5);

            packet.WriteGuid("Hostile GUID", hostileGUID);
            packet.WriteGuid("GUID", victimGUID);
        }

        [Parser(Opcode.SMSG_THREAT_CLEAR)]
        public static void HandleClearThreatlist(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 5, 7, 1, 2, 6, 3, 4, 0);
            packet.ParseBitStream(guid, 2, 0, 6, 7, 5, 4, 1, 3);

            packet.WriteGuid("Guid", guid);
        }
    }
}

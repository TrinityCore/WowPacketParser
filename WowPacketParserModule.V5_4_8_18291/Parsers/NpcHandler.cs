using System;
using System.Collections.Generic;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class NpcHandler
    {
        public static uint LastGossipPOIEntry;

        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        public static void HandleGossipHello(Packet packet)
        {
            var guid = new byte[8];
            packet.StartBitStream(guid, 2, 4, 0, 3, 6, 7, 5, 1);
            packet.ParseBitStream(guid, 4, 7, 1, 0, 5, 3, 6, 2);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            var guid = new byte[8];

            var menuEntry = packet.ReadUInt32("Menu Id");
            var gossipId = packet.ReadUInt32("Gossip Id");

            packet.StartBitStream(guid, 3, 0, 1, 4, 7, 5, 6);

            var bits8 = packet.ReadBits(8);

            guid[2] = packet.ReadBit();

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadWoWString("Box Text", bits8);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 1);

            Storage.GossipSelects.Add(Tuple.Create(menuEntry, gossipId), null, packet.TimeSpan);
            packet.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            var gossip = new Gossip();

            var guidBytes = new byte[8];

            var questgossips = packet.ReadBits(19);

            var titleLen = new uint[questgossips];
            for (var i = 0; i < questgossips; ++i)
            {
                packet.ReadBit("Change Icon", i);
                titleLen[i] = packet.ReadBits(9);
            }

            guidBytes[5] = packet.ReadBit();
            guidBytes[7] = packet.ReadBit();
            guidBytes[4] = packet.ReadBit();
            guidBytes[0] = packet.ReadBit();

            var amountOfOptions = packet.ReadBits(20);

            guidBytes[6] = packet.ReadBit();
            guidBytes[2] = packet.ReadBit();

            var boxTextLen = new uint[amountOfOptions];
            var optionTextLen = new uint[amountOfOptions];
            for (var i = 0; i < amountOfOptions; ++i)
            {
                boxTextLen[i] = packet.ReadBits(12);
                optionTextLen[i] = packet.ReadBits(12);
            }

            guidBytes[3] = packet.ReadBit();
            guidBytes[1] = packet.ReadBit();

            for (var i = 0; i < questgossips; ++i)
            {
                packet.ReadWoWString("Title", titleLen[i], i);
                packet.ReadUInt32E<QuestFlags>("Flags", i);//528
                packet.ReadInt32("Level", i);//8
                packet.ReadUInt32("Icon", i);//4
                packet.ReadUInt32<QuestId>("Quest ID", i); //528
                packet.ReadUInt32E<QuestFlags2>("Flags 2", i);//532
            }

            packet.ReadXORByte(guidBytes, 1);
            packet.ReadXORByte(guidBytes, 0);

            gossip.GossipOptions = new List<GossipOption>((int)amountOfOptions);
            for (var i = 0; i < amountOfOptions; ++i)
            {
                var gossipOption = new GossipOption
                {
                    RequiredMoney = packet.ReadUInt32("Required money", i),//3012
                    BoxText = packet.ReadWoWString("Box Text", boxTextLen[i], i),//12
                    Index = packet.ReadUInt32("Index", i),//0
                    Box = packet.ReadBool("Box", i),
                    OptionText = packet.ReadWoWString("Text", optionTextLen[i], i),
                    OptionIcon = packet.ReadByteE<GossipOptionIcon>("Icon", i)//4
                };

                gossip.GossipOptions.Add(gossipOption);
            }

            packet.ReadXORByte(guidBytes, 5);
            packet.ReadXORByte(guidBytes, 3);
            var menuId = packet.ReadUInt32("Menu Id");
            packet.ReadXORByte(guidBytes, 2);
            packet.ReadXORByte(guidBytes, 6);
            packet.ReadXORByte(guidBytes, 4);
            packet.ReadUInt32("Friendship Faction");
            packet.ReadXORByte(guidBytes, 7);
            var textId = packet.ReadUInt32("Text Id");

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
                    foreach (var gossipOptions in gossip.GossipOptions)
                        oldGossipOptions.Item1.GossipOptions.Add(gossipOptions);
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

            gossipPOI.Flags = (uint)packet.ReadInt32E<UnknownFlags>("Flags");
            var pos = packet.ReadVector2("Coordinates");
            gossipPOI.Icon = packet.ReadUInt32E<GossipPOIIcon>("Icon");
            gossipPOI.Importance = packet.ReadUInt32("Importance");
            gossipPOI.Name = packet.ReadCString("Icon Name");

            gossipPOI.PositionX = pos.X;
            gossipPOI.PositionY = pos.Y;

            Storage.GossipPOIs.Add(LastGossipPOIEntry, gossipPOI, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_QUERY_NPC_TEXT)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            var entry = packet.ReadInt32("Entry");

            var guid = new byte[8];
            packet.StartBitStream(guid, 4, 5, 1, 7, 0, 2, 6, 3);
            packet.ParseBitStream(guid, 4, 0, 2, 5, 1, 7, 3, 6);
            packet.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var npcText = new NpcTextMop();

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

            var hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            packet.AddSniffData(StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");

            Storage.NpcTextsMop.Add((uint)entry.Key, npcText, packet.TimeSpan);
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

        [Parser(Opcode.SMSG_THREAT_CLEAR)]
        public static void HandleClearThreatlist(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 6, 7, 4, 5, 2, 1, 0, 3);
            packet.ParseBitStream(guid, 7, 0, 4, 3, 2, 1, 6, 5);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_THREAT_UPDATE)]
        public static void HandleThreatlistUpdate(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 5, 6, 1, 3, 7, 0, 4);

            var count = packet.ReadBits("Size", 21);

            var hostileGUID = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                hostileGUID[i] = new byte[8];
                packet.StartBitStream(hostileGUID[i], 2, 3, 6, 5, 1, 4, 0, 7);
            }

            guid[2] = packet.ReadBit();

            for (var i = 0; i < count; ++i)
            {
                packet.ParseBitStream(hostileGUID[i], 6, 7, 0, 1, 2, 5, 3, 4);
                packet.ReadUInt32("Threat", i);
                packet.WriteGuid("Hostile", hostileGUID[i], i);

            }

            packet.ParseBitStream(guid, 1, 4, 2, 3, 5, 6, 0, 7);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_TRAINER_LIST)]
        public static void HandleClientTrainerList(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 0, 2, 7, 6, 1, 4, 5, 3);
            packet.ParseBitStream(guid, 3, 6, 7, 5, 1, 0, 2, 4);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            var npcTrainer = new NpcTrainer();

            var guid = new byte[8];

            guid[4] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var count = (int)packet.ReadBits(19);
            var titleLen = packet.ReadBits(11);
            guid[6] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[0] = packet.ReadBit();

            packet.ReadXORByte(guid, 4);

            npcTrainer.TrainerSpells = new List<TrainerSpell>(count);
            for (var i = 0; i < count; ++i)
            {
                var trainerSpell = new TrainerSpell();
                trainerSpell.RequiredLevel = packet.ReadByte("Required Level", i);
                trainerSpell.Cost = packet.ReadUInt32("Cost", i);
                trainerSpell.Spell = (uint)packet.ReadInt32<SpellId>("Spell ID", i);
                for (var j = 0; j < 3; ++j)
                    packet.ReadInt32("Int818", i, j);
                trainerSpell.RequiredSkill = packet.ReadUInt32("Required Skill", i);
                trainerSpell.RequiredSkillLevel = packet.ReadUInt32("Required Skill Level", i);
                packet.ReadByteE<TrainerSpellState>("State", i);

                npcTrainer.TrainerSpells.Add(trainerSpell);
            }

            npcTrainer.Title = packet.ReadWoWString("Title", titleLen);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 3);
            packet.ReadInt32("Unk Int32"); // Same unk exists in CMSG_TRAINER_BUY_SPELL
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            npcTrainer.Type = packet.ReadInt32E<TrainerType>("Type");

            packet.WriteGuid("Guid", guid);
            var GUID = new WowGuid64(BitConverter.ToUInt64(guid, 0));

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

        [Parser(Opcode.CMSG_LIST_INVENTORY)]
        public static void HandleNpcListInventory(Packet packet)
        {
            var guid = packet.StartBitStream(6, 7, 3, 1, 2, 0, 4, 5);
            packet.ParseBitStream(guid, 0, 7, 1, 6, 4, 3, 5, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            var npcVendor = new NpcVendor();

            var guid = new byte[8];

            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[6] = packet.ReadBit();

            var itemCount = packet.ReadBits(18);

            var unkBit = new uint[itemCount];
            var hasExtendedCost = new bool[itemCount];
            var hasCondition = new bool[itemCount];

            for (var i = 0; i < itemCount; ++i)
            {
                unkBit[i] = packet.ReadBit();
                hasExtendedCost[i] = !packet.ReadBit(); // +44
                hasCondition[i] = !packet.ReadBit(); // +36
            }

            guid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();

            packet.ReadByte("Byte10");

            npcVendor.VendorItems = new List<VendorItem>((int)itemCount);
            for (var i = 0; i < itemCount; ++i)
            {
                var vendorItem = new VendorItem();

                packet.AddValue("unkBit", unkBit[i], i);

                packet.ReadInt32("Max Durability", i);   // +16
                packet.ReadInt32("Price", i);   // +20
                vendorItem.Type = packet.ReadUInt32("Type", i);    // +4
                var maxCount = packet.ReadInt32("Max Count", i);     // +24
                packet.ReadInt32("Display ID", i);    // +12
                var buyCount = packet.ReadUInt32("Buy Count", i);    // +28

                vendorItem.ItemId = (uint)packet.ReadInt32<ItemId>("Item ID", i);   // +8

                if (hasExtendedCost[i])
                    vendorItem.ExtendedCostId = packet.ReadUInt32("Extended Cost", i);    // +36

                packet.ReadInt32("Item Upgrade ID", i);   // +32

                if (hasCondition[i])
                    packet.ReadInt32("Condition ID", i);    // +40

                vendorItem.Slot = packet.ReadUInt32("Item Position", i);    // +0

                npcVendor.VendorItems.Add(vendorItem);
            }

            packet.ParseBitStream(guid, 3, 7, 0, 6, 2, 1, 4, 5);

            packet.WriteGuid("Guid", guid);

            var vendorGUID = new WowGuid64(BitConverter.ToUInt64(guid, 0));
            Storage.NpcVendors.Add(vendorGUID.GetEntry(), npcVendor, packet.TimeSpan);
        }


        [Parser(Opcode.SMSG_HIGHEST_THREAT_UPDATE)]
        public static void HandleHighestThreatlistUpdate(Packet packet)
        {
            var newHighestGUID = new byte[8];
            var guid = new byte[8];


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
            var count = packet.ReadBits(21);

            var hostileGUID = new byte[count][];

            for (var i = 0; i < count; i++)
            {
                hostileGUID[i] = new byte[8];

                packet.StartBitStream(hostileGUID[i], 6, 1, 0, 2, 7, 4, 3, 5);
            }

            newHighestGUID[7] = packet.ReadBit();
            newHighestGUID[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();

            packet.ReadXORByte(newHighestGUID, 4);

            for (var i = 0; i < count; i++)
            {
                packet.ReadXORByte(hostileGUID[i], 6);
                packet.ReadInt32("Threat", i);
                packet.ReadXORByte(hostileGUID[i], 4);
                packet.ReadXORByte(hostileGUID[i], 0);
                packet.ReadXORByte(hostileGUID[i], 3);
                packet.ReadXORByte(hostileGUID[i], 5);
                packet.ReadXORByte(hostileGUID[i], 2);
                packet.ReadXORByte(hostileGUID[i], 1);
                packet.ReadXORByte(hostileGUID[i], 7);

                packet.WriteGuid("Hostile", hostileGUID[i], i);
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

            packet.WriteGuid("New Highest", newHighestGUID);
            packet.WriteGuid("Guid", guid);
        }
    }
}

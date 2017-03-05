using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_1_17538.Parsers
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class NpcHandler
    {
        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            var guidBytes = new byte[8];

            uint amountOfOptions = packet.ReadBits(20);
            packet.StartBitStream(guidBytes, 5, 1, 7, 2);
            uint questgossips = packet.ReadBits(19);
            packet.StartBitStream(guidBytes, 6, 4, 0);

            var titleLen = new uint[questgossips];
            for (int i = 0; i < questgossips; ++i)
            {
                packet.ReadBit("Change Icon", i);
                titleLen[i] = packet.ReadBits(9);
            }

            var optionTextLen = new uint[amountOfOptions];
            var boxTextLen = new uint[amountOfOptions];
            for (int i = 0; i < amountOfOptions; ++i)
            {
                boxTextLen[i] = packet.ReadBits(12);
                optionTextLen[i] = packet.ReadBits(12);
            }

            guidBytes[3] = packet.ReadBit();
            for (int i = 0; i < questgossips; i++)
            {
                packet.ReadUInt32E<QuestFlags>("Flags", i);
                packet.ReadUInt32("Icon", i);
                packet.ReadInt32("Level", i);
                packet.ReadWoWString("Title", titleLen[i], i);
                packet.ReadUInt32<QuestId>("Quest ID", i);
                packet.ReadUInt32E<QuestFlags2>("Flags 2", i);
            }

            packet.ReadXORByte(guidBytes, 2);
            packet.ReadXORByte(guidBytes, 1);

            uint menuId = packet.ReadUInt32("Menu Id");

            for (int i = 0; i < amountOfOptions; ++i)
            {
                GossipMenuOption gossipOption = new GossipMenuOption
                {
                    MenuID = menuId,
                    OptionText = packet.ReadWoWString("Text", optionTextLen[i], i),
                    BoxCoded = packet.ReadBool("Box", i),
                    OptionIcon = packet.ReadByteE<GossipOptionIcon>("Icon", i),
                    BoxMoney = packet.ReadUInt32("Required money", i),
                    BoxText = packet.ReadWoWString("Box Text", boxTextLen[i], i),
                    ID = packet.ReadUInt32("Index", i)
                };

                Storage.GossipMenuOptions.Add(gossipOption, packet.TimeSpan);
            }

            packet.ReadXORByte(guidBytes, 7);
            packet.ReadXORByte(guidBytes, 4);
            packet.ReadXORByte(guidBytes, 6);
            packet.ReadUInt32("Friendship Faction");
            packet.ReadXORByte(guidBytes, 0);
            packet.ReadXORByte(guidBytes, 5);
            uint textId = packet.ReadUInt32("Text Id");
            packet.ReadXORByte(guidBytes, 3);

            GossipMenu gossip = new GossipMenu
            {
                Entry = menuId,
                TextID = textId
            };

            WowGuid guid = packet.WriteGuid("Guid", guidBytes);

            gossip.ObjectType = guid.GetObjectType();
            gossip.ObjectEntry = guid.GetEntry();

            if (guid.GetObjectType() == ObjectType.Unit)
                if (Storage.Objects.ContainsKey(guid))
                    ((Unit)Storage.Objects[guid].Item1).GossipId = menuId;

            Storage.Gossips.Add(gossip, packet.TimeSpan);

            packet.AddSniffData(StoreNameType.Gossip, (int)menuId, guid.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var entry = packet.ReadEntry("Entry");
            if (entry.Value) // Can be masked
                return;

            int size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);
            Bit hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            NpcTextMop npcText = new NpcTextMop
            {
                ID = (uint)entry.Key
            };

            Packet pkt = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Formatter, packet.FileName);
            npcText.Probabilities = new float[8];
            npcText.BroadcastTextId = new uint[8];
            for (int i = 0; i < 8; ++i)
                npcText.Probabilities[i] = pkt.ReadSingle("Probability", i);
            for (int i = 0; i < 8; ++i)
                npcText.BroadcastTextId[i] = pkt.ReadUInt32("Broadcast Text Id", i);

            pkt.ClosePacket(false);

            packet.AddSniffData(StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");

            Storage.NpcTextsMop.Add(npcText, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        public static void HandleGossipHello(Packet packet)
        {
            var guid = new byte[8];
            packet.StartBitStream(guid, 2, 0, 1, 5, 7, 6, 4, 3);
            packet.ParseBitStream(guid, 6, 3, 2, 0, 5, 1, 7, 4);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_QUERY_NPC_TEXT)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            packet.ReadInt32("Entry");

            var guid = new byte[8];
            packet.StartBitStream(guid, 4, 7, 2, 5, 3, 0, 1, 6);
            packet.ParseBitStream(guid, 6, 4, 1, 3, 2, 5, 7, 0);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            var guid = new byte[8];

            var gossipId = packet.ReadUInt32("GossipMenu Id");
            var menuEntry = packet.ReadUInt32("Menu Id");

            packet.StartBitStream(guid, 4, 0, 6, 3, 2, 7, 1);

            var bits8 = packet.ReadBits(8);

            guid[5] = packet.ReadBit();

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 1);

            packet.ReadWoWString("Box Text", bits8);

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);

            Storage.GossipSelects.Add(Tuple.Create(menuEntry, gossipId), null, packet.TimeSpan);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadByte("Byte18");

            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();

            uint count = packet.ReadBits("itemCount", 18);

            guid[4] = packet.ReadBit();

            var hasExtendedCost = new bool[count];
            var hasCondition = new bool[count];

            for (int i = 0; i < count; ++i)
            {
                hasExtendedCost[i] = !packet.ReadBit();
                hasCondition[i] = !packet.ReadBit();
                packet.ReadBit("Unk bit", i);
            }

            guid[3] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[7] = packet.ReadBit();

            var tempList = new List<NpcVendor>();
            for (int i = 0; i < count; ++i)
            {
                NpcVendor vendor = new NpcVendor();

                vendor.Item = packet.ReadInt32<ItemId>("Item ID", i);

                if (hasCondition[i])
                    vendor.PlayerConditionID = packet.ReadUInt32("Condition ID", i);

                packet.ReadInt32("Price", i);

                if (hasExtendedCost[i])
                    vendor.ExtendedCost = packet.ReadUInt32("Extended Cost", i);

                packet.ReadInt32("Display ID", i);
                uint buyCount = packet.ReadUInt32("Buy Count", i);
                vendor.Slot = packet.ReadInt32("Item Position", i);
                int maxCount = packet.ReadInt32("Max Count", i);
                vendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                if (vendor.Type == 2)
                    vendor.MaxCount = buyCount;

                vendor.Type = packet.ReadUInt32("Type", i); // 1 - item, 2 - currency
                packet.ReadInt32("Item Upgrade ID", i);
                packet.ReadInt32("Max Durability", i);

                tempList.Add(vendor);
            }

            packet.ParseBitStream(guid, 0, 2, 1, 3, 5, 7, 4, 6);

            uint entry = packet.WriteGuid("GUID", guid).GetEntry();
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

            guid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            uint count = packet.ReadBits(19);

            guid[3] = packet.ReadBit();
            guid[7] = packet.ReadBit();

            uint titleLen = packet.ReadBits(11);

            guid[5] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            var tempList = new List<NpcTrainer>();
            for (int i = 0; i < count; ++i)
            {
                NpcTrainer trainer = new NpcTrainer
                {
                    SpellID = packet.ReadInt32<SpellId>("Spell ID", i)
                };

                for (var j = 0; j < 3; ++j)
                    packet.ReadInt32("Int818", i, j);

                packet.ReadByteE<TrainerSpellState>("State", i);
                trainer.ReqLevel = packet.ReadByte("Required Level", i);
                trainer.ReqSkillRank = packet.ReadUInt32("Required Skill Level", i);
                trainer.MoneyCost = packet.ReadUInt32("Cost", i);
                trainer.ReqSkillLine = packet.ReadUInt32("Required Skill", i);

                tempList.Add(trainer);
            }

            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);

            packet.ReadInt32E<TrainerType>("Type");
            packet.ReadWoWString("Title", titleLen);
            packet.ReadInt32("Unk Int32"); // Same unk exists in CMSG_TRAINER_BUY_SPELL

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);

            uint entry = packet.WriteGuid("Guid", guid).GetEntry();
            tempList.ForEach(v =>
            {
                v.ID = entry;
                Storage.NpcTrainers.Add(v, packet.TimeSpan);
            });
        }
    }
}

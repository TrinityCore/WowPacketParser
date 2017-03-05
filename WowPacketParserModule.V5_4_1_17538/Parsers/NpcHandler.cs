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

            uint amountOfOptions = packet.Translator.ReadBits(20);
            packet.Translator.StartBitStream(guidBytes, 5, 1, 7, 2);
            uint questgossips = packet.Translator.ReadBits(19);
            packet.Translator.StartBitStream(guidBytes, 6, 4, 0);

            var titleLen = new uint[questgossips];
            for (int i = 0; i < questgossips; ++i)
            {
                packet.Translator.ReadBit("Change Icon", i);
                titleLen[i] = packet.Translator.ReadBits(9);
            }

            var optionTextLen = new uint[amountOfOptions];
            var boxTextLen = new uint[amountOfOptions];
            for (int i = 0; i < amountOfOptions; ++i)
            {
                boxTextLen[i] = packet.Translator.ReadBits(12);
                optionTextLen[i] = packet.Translator.ReadBits(12);
            }

            guidBytes[3] = packet.Translator.ReadBit();
            for (int i = 0; i < questgossips; i++)
            {
                packet.Translator.ReadUInt32E<QuestFlags>("Flags", i);
                packet.Translator.ReadUInt32("Icon", i);
                packet.Translator.ReadInt32("Level", i);
                packet.Translator.ReadWoWString("Title", titleLen[i], i);
                packet.Translator.ReadUInt32<QuestId>("Quest ID", i);
                packet.Translator.ReadUInt32E<QuestFlags2>("Flags 2", i);
            }

            packet.Translator.ReadXORByte(guidBytes, 2);
            packet.Translator.ReadXORByte(guidBytes, 1);

            uint menuId = packet.Translator.ReadUInt32("Menu Id");

            for (int i = 0; i < amountOfOptions; ++i)
            {
                GossipMenuOption gossipOption = new GossipMenuOption
                {
                    MenuID = menuId,
                    OptionText = packet.Translator.ReadWoWString("Text", optionTextLen[i], i),
                    BoxCoded = packet.Translator.ReadBool("Box", i),
                    OptionIcon = packet.Translator.ReadByteE<GossipOptionIcon>("Icon", i),
                    BoxMoney = packet.Translator.ReadUInt32("Required money", i),
                    BoxText = packet.Translator.ReadWoWString("Box Text", boxTextLen[i], i),
                    ID = packet.Translator.ReadUInt32("Index", i)
                };

                Storage.GossipMenuOptions.Add(gossipOption, packet.TimeSpan);
            }

            packet.Translator.ReadXORByte(guidBytes, 7);
            packet.Translator.ReadXORByte(guidBytes, 4);
            packet.Translator.ReadXORByte(guidBytes, 6);
            packet.Translator.ReadUInt32("Friendship Faction");
            packet.Translator.ReadXORByte(guidBytes, 0);
            packet.Translator.ReadXORByte(guidBytes, 5);
            uint textId = packet.Translator.ReadUInt32("Text Id");
            packet.Translator.ReadXORByte(guidBytes, 3);

            GossipMenu gossip = new GossipMenu
            {
                Entry = menuId,
                TextID = textId
            };

            WowGuid guid = packet.Translator.WriteGuid("Guid", guidBytes);

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
            var entry = packet.Translator.ReadEntry("Entry");
            if (entry.Value) // Can be masked
                return;

            int size = packet.Translator.ReadInt32("Size");
            var data = packet.Translator.ReadBytes(size);
            Bit hasData = packet.Translator.ReadBit();
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
                npcText.Probabilities[i] = pkt.Translator.ReadSingle("Probability", i);
            for (int i = 0; i < 8; ++i)
                npcText.BroadcastTextId[i] = pkt.Translator.ReadUInt32("Broadcast Text Id", i);

            pkt.ClosePacket(false);

            packet.AddSniffData(StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");

            Storage.NpcTextsMop.Add(npcText, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        public static void HandleGossipHello(Packet packet)
        {
            var guid = new byte[8];
            packet.Translator.StartBitStream(guid, 2, 0, 1, 5, 7, 6, 4, 3);
            packet.Translator.ParseBitStream(guid, 6, 3, 2, 0, 5, 1, 7, 4);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_QUERY_NPC_TEXT)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            packet.Translator.ReadInt32("Entry");

            var guid = new byte[8];
            packet.Translator.StartBitStream(guid, 4, 7, 2, 5, 3, 0, 1, 6);
            packet.Translator.ParseBitStream(guid, 6, 4, 1, 3, 2, 5, 7, 0);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            var guid = new byte[8];

            var gossipId = packet.Translator.ReadUInt32("GossipMenu Id");
            var menuEntry = packet.Translator.ReadUInt32("Menu Id");

            packet.Translator.StartBitStream(guid, 4, 0, 6, 3, 2, 7, 1);

            var bits8 = packet.Translator.ReadBits(8);

            guid[5] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 1);

            packet.Translator.ReadWoWString("Box Text", bits8);

            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);

            Storage.GossipSelects.Add(Tuple.Create(menuEntry, gossipId), null, packet.TimeSpan);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadByte("Byte18");

            guid[1] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();

            uint count = packet.Translator.ReadBits("itemCount", 18);

            guid[4] = packet.Translator.ReadBit();

            var hasExtendedCost = new bool[count];
            var hasCondition = new bool[count];

            for (int i = 0; i < count; ++i)
            {
                hasExtendedCost[i] = !packet.Translator.ReadBit();
                hasCondition[i] = !packet.Translator.ReadBit();
                packet.Translator.ReadBit("Unk bit", i);
            }

            guid[3] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();

            var tempList = new List<NpcVendor>();
            for (int i = 0; i < count; ++i)
            {
                NpcVendor vendor = new NpcVendor();

                vendor.Item = packet.Translator.ReadInt32<ItemId>("Item ID", i);

                if (hasCondition[i])
                    vendor.PlayerConditionID = packet.Translator.ReadUInt32("Condition ID", i);

                packet.Translator.ReadInt32("Price", i);

                if (hasExtendedCost[i])
                    vendor.ExtendedCost = packet.Translator.ReadUInt32("Extended Cost", i);

                packet.Translator.ReadInt32("Display ID", i);
                uint buyCount = packet.Translator.ReadUInt32("Buy Count", i);
                vendor.Slot = packet.Translator.ReadInt32("Item Position", i);
                int maxCount = packet.Translator.ReadInt32("Max Count", i);
                vendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                if (vendor.Type == 2)
                    vendor.MaxCount = buyCount;

                vendor.Type = packet.Translator.ReadUInt32("Type", i); // 1 - item, 2 - currency
                packet.Translator.ReadInt32("Item Upgrade ID", i);
                packet.Translator.ReadInt32("Max Durability", i);

                tempList.Add(vendor);
            }

            packet.Translator.ParseBitStream(guid, 0, 2, 1, 3, 5, 7, 4, 6);

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

            guid[4] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            uint count = packet.Translator.ReadBits(19);

            guid[3] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();

            uint titleLen = packet.Translator.ReadBits(11);

            guid[5] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();

            var tempList = new List<NpcTrainer>();
            for (int i = 0; i < count; ++i)
            {
                NpcTrainer trainer = new NpcTrainer
                {
                    SpellID = packet.Translator.ReadInt32<SpellId>("Spell ID", i)
                };

                for (var j = 0; j < 3; ++j)
                    packet.Translator.ReadInt32("Int818", i, j);

                packet.Translator.ReadByteE<TrainerSpellState>("State", i);
                trainer.ReqLevel = packet.Translator.ReadByte("Required Level", i);
                trainer.ReqSkillRank = packet.Translator.ReadUInt32("Required Skill Level", i);
                trainer.MoneyCost = packet.Translator.ReadUInt32("Cost", i);
                trainer.ReqSkillLine = packet.Translator.ReadUInt32("Required Skill", i);

                tempList.Add(trainer);
            }

            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 6);

            packet.Translator.ReadInt32E<TrainerType>("Type");
            packet.Translator.ReadWoWString("Title", titleLen);
            packet.Translator.ReadInt32("Unk Int32"); // Same unk exists in CMSG_TRAINER_BUY_SPELL

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);

            uint entry = packet.Translator.WriteGuid("Guid", guid).GetEntry();
            tempList.ForEach(v =>
            {
                v.ID = entry;
                Storage.NpcTrainers.Add(v, packet.TimeSpan);
            });
        }
    }
}

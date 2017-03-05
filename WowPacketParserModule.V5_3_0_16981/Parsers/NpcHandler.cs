using System;
using System.Collections.Generic;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class NpcHandler
    {
        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        public static void HandleGossipHello(Packet packet)
        {
            var guid = new byte[8];
            guid[2] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.WriteGuid("GUID", guid);

        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            var guid = new byte[8];
            var menuEntry = packet.Translator.ReadUInt32("Menu Id");
            var gossipId = packet.Translator.ReadUInt32("GossipMenu Id");
            packet.Translator.StartBitStream(guid, 2, 0, 6, 4, 1, 5, 3, 7);

            var bits8 = packet.Translator.ReadBits(8);

            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadWoWString("Box Text", bits8);
            packet.Translator.ReadXORBytes(guid, 6, 5, 7, 4, 3, 2, 1);

            Storage.GossipSelects.Add(Tuple.Create(menuEntry, gossipId), null, packet.TimeSpan);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            var guidBytes = new byte[8];

            packet.Translator.StartBitStream(guidBytes, 0, 1);
            uint questgossips = packet.Translator.ReadBits("Amount of Quest gossips", 19);
            guidBytes[2] = packet.Translator.ReadBit();
            uint amountOfOptions = packet.Translator.ReadBits("Amount of Options", 20);

            var titleLen = new uint[questgossips];
            for (int i = 0; i < questgossips; ++i)
            {
                packet.Translator.ReadBit("Change Icon", i);
                titleLen[i] = packet.Translator.ReadBits(9);
            }
            guidBytes[3] = packet.Translator.ReadBit();

            var optionTextLen = new uint[amountOfOptions];
            var boxTextLen = new uint[amountOfOptions];
            for (int i = 0; i < amountOfOptions; ++i)
            {
                boxTextLen[i] = packet.Translator.ReadBits(12);
                optionTextLen[i] = packet.Translator.ReadBits(12);
            }

            packet.Translator.StartBitStream(guidBytes, 5, 4, 6, 7);
            packet.Translator.ResetBitReader();
            packet.Translator.ReadXORByte(guidBytes, 6);

            var gossipOptions = new List<GossipMenuOption>((int)amountOfOptions);
            for (int i = 0; i < amountOfOptions; ++i)
            {
                GossipMenuOption gossipOption = new GossipMenuOption
                {
                    OptionText = packet.Translator.ReadWoWString("Text", optionTextLen[i], i),
                    BoxText = packet.Translator.ReadWoWString("Box Text", boxTextLen[i], i),
                    BoxMoney = packet.Translator.ReadUInt32("Required money", i),
                    OptionIcon = packet.Translator.ReadByteE<GossipOptionIcon>("Icon", i),
                    BoxCoded = packet.Translator.ReadBool("Box", i),
                    ID = packet.Translator.ReadUInt32("Index", i)
                };

                gossipOptions.Add(gossipOption);
            }
            packet.Translator.ReadXORByte(guidBytes, 0);

            for (int i = 0; i < questgossips; i++)
            {
                packet.Translator.ReadUInt32E<QuestFlags>("Flags", i);
                packet.Translator.ReadWoWString("Title", titleLen[i], i);
                packet.Translator.ReadUInt32("Icon", i);
                packet.Translator.ReadUInt32E<QuestFlags2>("Flags 2", i);
                packet.Translator.ReadInt32("Level", i);
                packet.Translator.ReadUInt32<QuestId>("Quest ID", i);
            }

            uint textId = packet.Translator.ReadUInt32("Text Id");
            packet.Translator.ReadXORBytes(guidBytes, 4, 3);
            uint menuId = packet.Translator.ReadUInt32("Menu Id");
            packet.Translator.ReadUInt32("Friendship Faction");
            packet.Translator.ReadXORBytes(guidBytes, 7, 1, 5, 2);

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

            gossipOptions.ForEach(g =>
            {
                g.MenuID = menuId;
                Storage.GossipMenuOptions.Add(g, packet.TimeSpan);
            });

            Storage.Gossips.Add(gossip, packet.TimeSpan);

            packet.AddSniffData(StoreNameType.Gossip, (int)menuId, guid.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            var guidBytes = new byte[8];
            
            guidBytes[0] = packet.Translator.ReadBit();
            uint count = packet.Translator.ReadBits("Count", 19);
            packet.Translator.StartBitStream(guidBytes, 2, 6);
            uint titleLen = packet.Translator.ReadBits(11);
            packet.Translator.StartBitStream(guidBytes, 3, 7, 1, 4, 5);
            packet.Translator.ResetBitReader();

            var tempList = new List<NpcTrainer>();
            for (int i = 0; i < count; ++i)
            {
                NpcTrainer trainer = new NpcTrainer();
                trainer.ReqSkillLine = packet.Translator.ReadUInt32("Required Skill", i);
                trainer.ReqSkillRank = packet.Translator.ReadUInt32("Required Skill Level", i);
                trainer.MoneyCost = packet.Translator.ReadUInt32("Cost", i);

                trainer.ReqLevel = packet.Translator.ReadByte("Required Level", i);
                packet.Translator.ReadInt32<SpellId>("Required Spell ID", i);
                packet.Translator.ReadInt32("Profession Dialog", i);
                packet.Translator.ReadInt32("Profession Button", i);

                trainer.SpellID = packet.Translator.ReadInt32<SpellId>("Spell ID", i);
                packet.Translator.ReadByteE<TrainerSpellState>("State", i);

                tempList.Add(trainer);
            }

            packet.Translator.ReadXORBytes(guidBytes, 3, 2);
            packet.Translator.ReadWoWString("Title", titleLen);
            packet.Translator.ReadXORBytes(guidBytes, 7, 6, 4, 1, 0, 5);

            packet.Translator.ReadInt32E<TrainerType>("Type");
            packet.Translator.ReadInt32("Unk Int32"); // Same unk exists in CMSG_TRAINER_BUY_SPELL

            uint entry = packet.Translator.WriteGuid("Guid", guidBytes).GetEntry();
            tempList.ForEach(v =>
            {
                v.ID = entry;
                Storage.NpcTrainers.Add(v, packet.TimeSpan);
            });
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY)]
        public static void HandleVendorInventoryList(Packet packet)
        {   
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 5, 4, 7, 1, 2, 3);
            uint itemCount = packet.Translator.ReadBits("Item Count", 18);

            var hasExtendedCost = new bool[itemCount];
            var hasCondition = new bool[itemCount];
            for (int i = 0; i < itemCount; ++i)
            {
                hasCondition[i] = !packet.Translator.ReadBit();
                hasExtendedCost[i] = !packet.Translator.ReadBit();
                packet.Translator.ReadBit("Unk bit", i);
            }

            packet.Translator.StartBitStream(guid, 6, 0);
            packet.Translator.ResetBitReader();
            packet.Translator.ReadXORBytes(guid, 3, 4);

            var tempList = new List<NpcVendor>();
            for (int i = 0; i < itemCount; ++i)
            {
                NpcVendor vendor = new NpcVendor
                {
                    Item = packet.Translator.ReadInt32<ItemId>("Item ID", i),
                    Slot = packet.Translator.ReadInt32("Item Position", i)
                };

                packet.Translator.ReadInt32("Item Upgrade ID", i);
                packet.Translator.ReadInt32("Display ID", i);
                int maxCount = packet.Translator.ReadInt32("Max Count", i);
                uint buyCount = packet.Translator.ReadUInt32("Buy Count", i);
                packet.Translator.ReadInt32("Price", i);

                if (hasCondition[i])
                    packet.Translator.ReadInt32("Condition ID", i);

                vendor.Type = packet.Translator.ReadUInt32("Type", i); // 1 - item, 2 - currency
                packet.Translator.ReadInt32("Max Durability", i);
                if (hasExtendedCost[i])
                    vendor.ExtendedCost = packet.Translator.ReadUInt32("Extended Cost", i);

                vendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                if (vendor.Type == 2)
                    vendor.MaxCount = buyCount;

                tempList.Add(vendor);
            }

            packet.Translator.ReadXORBytes(guid, 1, 2, 7);
            packet.Translator.ReadByte("Unk Byte");
            packet.Translator.ReadXORBytes(guid, 6, 0, 5);

            uint entry = packet.Translator.WriteGuid("GUID", guid).GetEntry();
            tempList.ForEach(v =>
            {
                v.Entry = entry;
                Storage.NpcVendors.Add(v, packet.TimeSpan);
            });
        }
    }
}

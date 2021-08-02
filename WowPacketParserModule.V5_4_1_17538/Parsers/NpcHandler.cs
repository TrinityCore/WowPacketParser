using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WoWPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_1_17538.Parsers
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class NpcHandler
    {
        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            PacketGossipMessage packetGossip = packet.Holder.GossipMessage = new();
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
                var title = packet.ReadWoWString("Title", titleLen[i], i);
                var quest = packet.ReadUInt32<QuestId>("Quest ID", i);
                packet.ReadUInt32E<QuestFlagsEx>("Flags 2", i);

                packetGossip.Quests.Add(new GossipQuestOption()
                {
                    Title = title,
                    QuestId = quest
                });
            }

            packet.ReadXORByte(guidBytes, 2);
            packet.ReadXORByte(guidBytes, 1);

            uint menuId = packetGossip.MenuId = packet.ReadUInt32("Menu Id");

            for (int i = 0; i < amountOfOptions; ++i)
            {
                GossipMenuOption gossipOption = new GossipMenuOption
                {
                    MenuId = menuId
                };
                GossipMenuOptionBox gossipMenuOptionBox = new GossipMenuOptionBox
                {
                    MenuId = menuId
                };

                gossipOption.OptionText = packet.ReadWoWString("Text", optionTextLen[i], i);
                gossipMenuOptionBox.BoxCoded = packet.ReadBool("Box", i);
                gossipOption.OptionIcon = packet.ReadByteE<GossipOptionIcon>("Icon", i);
                gossipMenuOptionBox.BoxMoney = packet.ReadUInt32("Required money", i);
                gossipMenuOptionBox.BoxText = packet.ReadWoWString("Box Text", boxTextLen[i], i);
                gossipOption.OptionIndex = gossipMenuOptionBox.OptionIndex = packet.ReadUInt32("Index", i);

                Storage.GossipMenuOptions.Add(gossipOption, packet.TimeSpan);
                if (!gossipMenuOptionBox.IsEmpty)
                    Storage.GossipMenuOptionBoxes.Add(gossipMenuOptionBox, packet.TimeSpan);
                
                packetGossip.Options.Add(new GossipMessageOption()
                {
                    OptionIndex = gossipOption.OptionIndex.Value,
                    OptionIcon = (int)gossipOption.OptionIcon,
                    BoxCoded = gossipMenuOptionBox.BoxCoded.Value,
                    BoxCost = gossipMenuOptionBox.BoxMoney.Value,
                    Text = gossipOption.OptionText,
                    BoxText = gossipMenuOptionBox.BoxText
                });
            }

            packet.ReadXORByte(guidBytes, 7);
            packet.ReadXORByte(guidBytes, 4);
            packet.ReadXORByte(guidBytes, 6);
            packet.ReadUInt32("Friendship Faction");
            packet.ReadXORByte(guidBytes, 0);
            packet.ReadXORByte(guidBytes, 5);
            uint textId = packetGossip.TextId = packet.ReadUInt32("Text Id");
            packet.ReadXORByte(guidBytes, 3);

            GossipMenu gossip = new GossipMenu
            {
                Entry = menuId,
                TextID = textId
            };

            WowGuid guid = packet.WriteGuid("Guid", guidBytes);
            packetGossip.GossipSource = guid;

            gossip.ObjectType = guid.GetObjectType();
            gossip.ObjectEntry = guid.GetEntry();

            if (guid.GetObjectType() == ObjectType.Unit)
                if (Storage.Objects.ContainsKey(guid))
                    ((Unit)Storage.Objects[guid].Item1).GossipId = menuId;

            Storage.Gossips.Add(gossip, packet.TimeSpan);
            var lastGossipOption = CoreParsers.NpcHandler.LastGossipOption;
            var tempGossipOptionPOI = CoreParsers.NpcHandler.TempGossipOptionPOI;
            if (lastGossipOption.HasSelection)
            {
                if ((packet.TimeSpan - lastGossipOption.TimeSpan).Duration() <= TimeSpan.FromMilliseconds(2500))
                {
                    Storage.GossipMenuOptionActions.Add(new GossipMenuOptionAction { MenuId = lastGossipOption.MenuId, OptionIndex = lastGossipOption.OptionIndex, ActionMenuId = menuId }, packet.TimeSpan);

                    //keep temp data (for case SMSG_GOSSIP_POI is delayed)
                    tempGossipOptionPOI.Guid = lastGossipOption.Guid;
                    tempGossipOptionPOI.MenuId = lastGossipOption.MenuId;
                    tempGossipOptionPOI.OptionIndex = lastGossipOption.OptionIndex;
                    tempGossipOptionPOI.ActionMenuId = gossip.Entry;
                    tempGossipOptionPOI.TimeSpan = lastGossipOption.TimeSpan;

                    // clear lastgossip so no faulty linkings appear
                    lastGossipOption.Reset();
                }
                else
                {
                    lastGossipOption.Reset();
                    tempGossipOptionPOI.Reset();

                }
            }

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

            Packet pkt = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);
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

            CoreParsers.NpcHandler.LastGossipOption.Reset();
            var gossipGuid = CoreParsers.NpcHandler.LastGossipOption.Guid = packet.WriteGuid("Guid", guid);

            packet.Holder.GossipHello = new PacketGossipHello { GossipSource = gossipGuid };
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
            PacketGossipSelect packetGossip = packet.Holder.GossipSelect = new();

            var guid = new byte[8];

            var gossipId = packetGossip.OptionId = packet.ReadUInt32("GossipMenu Id");
            var menuEntry = packetGossip.MenuId = packet.ReadUInt32("Menu Id");

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
            packetGossip.GossipUnit = packet.WriteGuid("GUID", guid);
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
            uint count = packet.ReadBits("Spells", 19);

            guid[3] = packet.ReadBit();
            guid[7] = packet.ReadBit();

            uint titleLen = packet.ReadBits(11);

            guid[5] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            var tempList = new List<TrainerSpell>();
            for (int i = 0; i < count; ++i)
            {
                TrainerSpell trainerSpell = new TrainerSpell
                {
                    SpellId = packet.ReadUInt32<SpellId>("SpellID", i),
                    ReqAbility = new uint[3]
                };

                for (var j = 0; j < 3; ++j)
                    trainerSpell.ReqAbility[j] = packet.ReadUInt32<SpellId>("ReqAbility", i, j);

                packet.ReadByteE<TrainerSpellState>("Usable", i);
                trainerSpell.ReqLevel = packet.ReadByte("ReqLevel", i);
                trainerSpell.ReqSkillRank = packet.ReadUInt32("ReqSkillRank", i);
                trainerSpell.MoneyCost = packet.ReadUInt32("MoneyCost", i);
                trainerSpell.ReqSkillLine = packet.ReadUInt32("ReqSkillLine", i);

                tempList.Add(trainerSpell);
            }

            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);

            Trainer trainer = new Trainer
            {
                Type = packet.ReadInt32E<TrainerType>("Type"),
                Greeting = packet.ReadWoWString("Title", titleLen),
                Id = packet.ReadUInt32("TrainerID")
            };

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("TrainerGUID", guid);
            Storage.Trainers.Add(trainer, packet.TimeSpan);
            tempList.ForEach(trainerSpell =>
            {
                trainerSpell.TrainerId = trainer.Id;
                Storage.TrainerSpells.Add(trainerSpell, packet.TimeSpan);
            });
            var lastGossipOption = CoreParsers.NpcHandler.LastGossipOption;
            if (lastGossipOption.HasSelection)
            {
                if ((packet.TimeSpan - lastGossipOption.TimeSpan).Duration() <= TimeSpan.FromMilliseconds(2500))
                    Storage.CreatureTrainers.Add(new CreatureTrainer { CreatureId = lastGossipOption.Guid.GetEntry(), MenuID = lastGossipOption.MenuId, OptionIndex = lastGossipOption.OptionIndex, TrainerId = trainer.Id }, packet.TimeSpan);
            }
            else
                Storage.CreatureTrainers.Add(new CreatureTrainer { CreatureId = lastGossipOption.Guid.GetEntry(), MenuID = 0, OptionIndex = 0, TrainerId = trainer.Id }, packet.TimeSpan);
        }
    }
}

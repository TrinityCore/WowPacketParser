using System;
using System.Collections.Generic;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

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

            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
            var gossipGuid = CoreParsers.NpcHandler.LastGossipOption.Guid = packet.WriteGuid("Guid", guid);

            packet.Holder.GossipHello = new PacketGossipHello { GossipSource = gossipGuid };
        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            PacketGossipSelect packetGossip = packet.Holder.GossipSelect = new();

            var guid = new byte[8];
            var menuID = packetGossip.MenuId = packet.ReadUInt32("MenuID");
            var optionID = packetGossip.OptionId = packet.ReadUInt32("OptionID");
            packet.StartBitStream(guid, 2, 0, 6, 4, 1, 5, 3, 7);

            var bits8 = packet.ReadBits(8);

            packet.ReadXORByte(guid, 0);
            packet.ReadWoWString("Box Text", bits8);
            packet.ReadXORBytes(guid, 6, 5, 7, 4, 3, 2, 1);

            Storage.GossipSelects.Add(Tuple.Create(menuID, optionID), null, packet.TimeSpan);
            packetGossip.GossipUnit = packet.WriteGuid("GUID", guid);

            CoreParsers.NpcHandler.LastGossipOption.GossipSelectOption(menuID, optionID, packet.TimeSpan);
            CoreParsers.NpcHandler.TempGossipOptionPOI.GossipSelectOption(menuID, optionID, packet.TimeSpan);
            CoreParsers.NpcHandler.TempGossipOptionPOI.Guid = CoreParsers.NpcHandler.LastGossipOption.Guid;
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            PacketGossipMessage packetGossip = packet.Holder.GossipMessage = new();
            var guidBytes = new byte[8];

            packet.StartBitStream(guidBytes, 0, 1);
            uint questgossips = packet.ReadBits("Amount of Quest gossips", 19);
            guidBytes[2] = packet.ReadBit();
            uint amountOfOptions = packet.ReadBits("Amount of Options", 20);

            var titleLen = new uint[questgossips];
            for (int i = 0; i < questgossips; ++i)
            {
                packet.ReadBit("Change Icon", i);
                titleLen[i] = packet.ReadBits(9);
            }
            guidBytes[3] = packet.ReadBit();

            var optionTextLen = new uint[amountOfOptions];
            var boxTextLen = new uint[amountOfOptions];
            for (int i = 0; i < amountOfOptions; ++i)
            {
                boxTextLen[i] = packet.ReadBits(12);
                optionTextLen[i] = packet.ReadBits(12);
            }

            packet.StartBitStream(guidBytes, 5, 4, 6, 7);
            packet.ResetBitReader();
            packet.ReadXORByte(guidBytes, 6);

            var gossipOptions = new List<GossipMenuOption>((int)amountOfOptions);
            for (int i = 0; i < amountOfOptions; ++i)
            {
                GossipMenuOption gossipOption = new GossipMenuOption();
                gossipOption.OptionText = packet.ReadWoWString("Text", optionTextLen[i], i);
                var boxText = packet.ReadWoWString("Box Text", boxTextLen[i], i);
                gossipOption.BoxMoney = packet.ReadUInt32("Required money", i);
                gossipOption.OptionNpc = packet.ReadByteE<GossipOptionNpc>("OptionNPC", i);
                gossipOption.BoxCoded = packet.ReadBool("Box", i);
                gossipOption.OptionID = packet.ReadUInt32("OptionID", i);

                if (!string.IsNullOrEmpty(boxText))
                    gossipOption.BoxText = boxText;

                gossipOptions.Add(gossipOption);

                packetGossip.Options.Add(new GossipMessageOption()
                {
                    OptionIndex = gossipOption.OptionID.Value,
                    OptionNpc = (int)gossipOption.OptionNpc,
                    BoxCoded = gossipOption.BoxCoded.Value,
                    BoxCost = gossipOption.BoxMoney.Value,
                    Text = gossipOption.OptionText,
                    BoxText = gossipOption.BoxText
                });
            }
            packet.ReadXORByte(guidBytes, 0);

            for (int i = 0; i < questgossips; i++)
            {
                packet.ReadUInt32E<QuestFlags>("Flags", i);
                var title = packet.ReadWoWString("Title", titleLen[i], i);
                packet.ReadUInt32("Icon", i);
                packet.ReadUInt32E<QuestFlagsEx>("Flags 2", i);
                packet.ReadInt32("Level", i);
                var quest = packet.ReadUInt32<QuestId>("Quest ID", i);

                packetGossip.Quests.Add(new GossipQuestOption()
                {
                    Title = title,
                    QuestId = quest
                });
            }

            uint textId = packetGossip.TextId = packet.ReadUInt32("Text Id");
            packet.ReadXORBytes(guidBytes, 4, 3);
            uint menuId = packetGossip.MenuId = packet.ReadUInt32("Menu Id");
            uint friendshipFactionID = packet.ReadUInt32("Friendship Faction");
            packet.ReadXORBytes(guidBytes, 7, 1, 5, 2);

            GossipMenu gossip = new GossipMenu
            {
                MenuID = menuId,
                TextID = textId
            };

            WowGuid guid = packet.WriteGuid("GUID", guidBytes);
            packetGossip.GossipSource = guid;

            gossip.ObjectType = guid.GetObjectType();
            gossip.ObjectEntry = guid.GetEntry();

            if (guid.GetObjectType() == ObjectType.Unit && !CoreParsers.NpcHandler.HasLastGossipOption(packet.TimeSpan, (uint)menuId))
            {
                CreatureTemplateGossip creatureTemplateGossip = new()
                {
                    CreatureID = guid.GetEntry(),
                    MenuID = menuId
                };
                Storage.CreatureTemplateGossips.Add(creatureTemplateGossip);
                Storage.CreatureDefaultGossips.Add(guid.GetEntry(), menuId);
            }

            gossipOptions.ForEach(g =>
            {
                g.MenuID = menuId;
                g.FillBroadcastTextIDs();
                Storage.GossipMenuOptions.Add((g.MenuID, g.OptionID), g, packet.TimeSpan);
            });

            Storage.Gossips.Add(gossip, packet.TimeSpan);

            CoreParsers.NpcHandler.AddGossipAddon(packetGossip.MenuId, (int)friendshipFactionID, 0, guid, packet.TimeSpan);

            CoreParsers.NpcHandler.UpdateLastGossipOptionActionMessage(packet.TimeSpan, menuId);

            packet.AddSniffData(StoreNameType.Gossip, (int)menuId, guid.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            var guidBytes = new byte[8];

            guidBytes[0] = packet.ReadBit();
            uint count = packet.ReadBits("Spells", 19);
            packet.StartBitStream(guidBytes, 2, 6);
            uint titleLen = packet.ReadBits(11);
            packet.StartBitStream(guidBytes, 3, 7, 1, 4, 5);
            packet.ResetBitReader();

            var tempList = new List<TrainerSpell>();
            for (int i = 0; i < count; ++i)
            {
                TrainerSpell trainerSpell = new TrainerSpell
                {
                    ReqSkillLine = packet.ReadUInt32("ReqSkillLine", i),
                    ReqSkillRank = packet.ReadUInt32("ReqSkillRank", i),
                    MoneyCost = packet.ReadUInt32("MoneyCost", i),
                    ReqLevel = packet.ReadByte("ReqLevel", i),
                    ReqAbility = new uint[3]
                };
                for (var j = 0; j < 3; ++j)
                    trainerSpell.ReqAbility[j] = packet.ReadUInt32<SpellId>("ReqAbility", i);

                trainerSpell.SpellId = packet.ReadUInt32<SpellId>("SpellID", i);
                packet.ReadByteE<TrainerSpellState>("Usable", i);

                tempList.Add(trainerSpell);
            }

            Trainer trainer = new Trainer();
            packet.ReadXORBytes(guidBytes, 3, 2);
            trainer.Greeting = packet.ReadWoWString("Title", titleLen);
            packet.ReadXORBytes(guidBytes, 7, 6, 4, 1, 0, 5);

            trainer.Type = packet.ReadInt32E<TrainerType>("Type");
            trainer.Id = packet.ReadUInt32("TrainerID");

            packet.WriteGuid("TrainerGUID", guidBytes);
            Storage.Trainers.Add(trainer, packet.TimeSpan);
            tempList.ForEach(trainerSpell =>
            {
                trainerSpell.TrainerId = trainer.Id;
                Storage.TrainerSpells.Add(trainerSpell, packet.TimeSpan);
            });

            CoreParsers.NpcHandler.AddToCreatureTrainers(trainer.Id, packet.TimeSpan, true);
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 5, 4, 7, 1, 2, 3);
            uint itemCount = packet.ReadBits("Item Count", 18);

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

            var tempList = new List<NpcVendor>();
            for (int i = 0; i < itemCount; ++i)
            {
                NpcVendor vendor = new NpcVendor
                {
                    Item = packet.ReadInt32<ItemId>("Item ID", i),
                    Slot = packet.ReadInt32("Item Position", i)
                };

                packet.ReadInt32("Item Upgrade ID", i);
                packet.ReadInt32("Display ID", i);
                int maxCount = packet.ReadInt32("Max Count", i);
                uint buyCount = packet.ReadUInt32("Buy Count", i);
                packet.ReadInt32("Price", i);

                if (hasCondition[i])
                    packet.ReadInt32("Condition ID", i);

                vendor.Type = packet.ReadUInt32("Type", i); // 1 - item, 2 - currency
                packet.ReadInt32("Max Durability", i);
                if (hasExtendedCost[i])
                    vendor.ExtendedCost = packet.ReadUInt32("Extended Cost", i);

                vendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                if (vendor.Type == 2)
                    vendor.MaxCount = buyCount;

                tempList.Add(vendor);
            }

            packet.ReadXORBytes(guid, 1, 2, 7);
            packet.ReadByte("Unk Byte");
            packet.ReadXORBytes(guid, 6, 0, 5);

            uint entry = packet.WriteGuid("GUID", guid).GetEntry();
            tempList.ForEach(v =>
            {
                v.Entry = entry;
                Storage.NpcVendors.Add(v, packet.TimeSpan);
            });

            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
        }
    }
}

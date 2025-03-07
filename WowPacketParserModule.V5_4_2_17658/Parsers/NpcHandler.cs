using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class NpcHandler
    {
        public static uint LastGossipPOIEntry;

        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        public static void HandleGossipHello(Packet packet)
        {
            var guid = new byte[8];
            packet.StartBitStream(guid, 5, 2, 0, 4, 7, 1, 6, 3);
            packet.ParseBitStream(guid, 3, 4, 6, 1, 0, 2, 7, 5);

            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
            var gossipGuid = CoreParsers.NpcHandler.LastGossipOption.Guid = packet.WriteGuid("Guid", guid);

            packet.Holder.GossipHello = new PacketGossipHello { GossipSource = gossipGuid };
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            PacketGossipMessage packetGossip = packet.Holder.GossipMessage = new();
            var guidBytes = new byte[8];

            guidBytes[7] = packet.ReadBit();
            guidBytes[6] = packet.ReadBit();
            guidBytes[0] = packet.ReadBit();

            int questgossips = (int)packet.ReadBits(19);

            guidBytes[4] = packet.ReadBit();

            var titleLen = new uint[questgossips];
            for (int i = 0; i < questgossips; ++i)
            {
                titleLen[i] = packet.ReadBits(9);
                packet.ReadBit("Change Icon", i);
            }

            guidBytes[3] = packet.ReadBit();
            guidBytes[2] = packet.ReadBit();

            uint amountOfOptions = packet.ReadBits(20);

            var boxTextLen = new uint[amountOfOptions];
            var optionTextLen = new uint[amountOfOptions];
            for (int i = 0; i < amountOfOptions; ++i)
            {
                boxTextLen[i] = packet.ReadBits(12);
                optionTextLen[i] = packet.ReadBits(12);
            }

            guidBytes[1] = packet.ReadBit();
            guidBytes[5] = packet.ReadBit();

            packet.ReadXORByte(guidBytes, 2);

            var gossipOptions = new List<GossipMenuOption>((int)amountOfOptions);
            for (int i = 0; i < amountOfOptions; ++i)
            {
                GossipMenuOption gossipOption = new GossipMenuOption();

                var boxText = packet.ReadWoWString("Box Text", boxTextLen[i], i);
                gossipOption.OptionText = packet.ReadWoWString("Text", optionTextLen[i], i);
                gossipOption.OptionID = packet.ReadUInt32("OptionID", i);
                gossipOption.BoxCoded = packet.ReadBool("Box", i);
                gossipOption.OptionNpc = packet.ReadByteE<GossipOptionNpc>("OptionNPC", i);
                gossipOption.BoxMoney = packet.ReadUInt32("Required money", i);

                if (!string.IsNullOrEmpty(boxText))
                    gossipOption.BoxText = boxText;

                gossipOptions.Add(gossipOption);

                packetGossip.Options.Add(new GossipMessageOption()
                {
                    OptionIndex = gossipOption.OptionID.Value,
                    OptionNpc = (int)gossipOption.OptionNpc,
                    BoxCoded = gossipOption.BoxCoded.Value,
                    BoxCost = (uint)gossipOption.BoxMoney.Value,
                    Text = gossipOption.OptionText,
                    BoxText = gossipOption.BoxText
                });
            }

            for (int i = 0; i < questgossips; ++i)
            {
                packet.ReadInt32("Level", i);
                packet.ReadUInt32E<QuestFlags>("Flags", i);
                packet.ReadUInt32("Icon", i);
                var title = packet.ReadWoWString("Title", titleLen[i], i);
                var quest = packet.ReadUInt32<QuestId>("Quest ID", i);
                packet.ReadUInt32E<QuestFlagsEx>("Flags 2", i);

                packetGossip.Quests.Add(new GossipQuestOption()
                {
                    Title = title,
                    QuestId = quest
                });
            }

            packet.ReadXORByte(guidBytes, 7);

            uint friendshipFactionID = packet.ReadUInt32("Friendship Faction");

            packet.ReadXORByte(guidBytes, 3);
            packet.ReadXORByte(guidBytes, 1);

            uint textId = packetGossip.TextId = packet.ReadUInt32("Text Id");

            packet.ReadXORByte(guidBytes, 5);

            uint menuId = packetGossip.MenuId = packet.ReadUInt32("Menu Id");

            packet.ReadXORByte(guidBytes, 6);
            packet.ReadXORByte(guidBytes, 4);
            packet.ReadXORByte(guidBytes, 0);

            GossipMenu gossip = new GossipMenu
            {
                MenuID = menuId,
                TextID = textId
            };

            WowGuid guid = packet.WriteGuid("Guid", guidBytes);
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

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            PacketGossipSelect packetGossip = packet.Holder.GossipSelect = new();

            var guid = new byte[8];

            var menuID = packetGossip.MenuId = packet.ReadUInt32("MenuID");
            var optionID = packetGossip.OptionId = packet.ReadUInt32("OptionID");

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

            Storage.GossipSelects.Add(Tuple.Create(menuID, optionID), null, packet.TimeSpan);
            packetGossip.GossipUnit = packet.WriteGuid("GUID", guid);

            CoreParsers.NpcHandler.LastGossipOption.GossipSelectOption(menuID, optionID, packet.TimeSpan);
            CoreParsers.NpcHandler.TempGossipOptionPOI.GossipSelectOption(menuID, optionID, packet.TimeSpan);
            CoreParsers.NpcHandler.TempGossipOptionPOI.Guid = CoreParsers.NpcHandler.LastGossipOption.Guid;
        }

        [Parser(Opcode.SMSG_GOSSIP_POI)]
        public static void HandleGossipPoi(Packet packet)
        {
            ++LastGossipPOIEntry;
            var protoPoi = packet.Holder.GossipPoi = new();
            PointsOfInterest gossipPOI = new PointsOfInterest();
            gossipPOI.ID = "@ID+" + LastGossipPOIEntry.ToString();

            gossipPOI.Flags = protoPoi.Flags = (uint)packet.ReadInt32E<UnknownFlags>("Flags");

            Vector2 pos = packet.ReadVector2("Coordinates");
            protoPoi.Coordinates = pos;
            gossipPOI.PositionX = pos.X;
            gossipPOI.PositionY = pos.Y;

            gossipPOI.Icon = packet.ReadUInt32E<GossipPOIIcon>("Icon");
            gossipPOI.Importance = protoPoi.Importance = packet.ReadUInt32("Data");
            gossipPOI.Name = protoPoi.Name = packet.ReadCString("Icon Name");
            protoPoi.Icon = (uint)gossipPOI.Icon;

            Storage.GossipPOIs.Add(gossipPOI, packet.TimeSpan);
            CoreParsers.NpcHandler.UpdateTempGossipOptionActionPOI(packet.TimeSpan, gossipPOI.ID);
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
            Bit hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            var entry = packet.ReadEntry("Entry");
            if (entry.Value) // Can be masked
                return;

            NpcTextMop npcText = new NpcTextMop
            {
                ID = (uint)entry.Key
            };

            int size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);

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
            var proto = packet.Holder.NpcText = new() { Entry = npcText.ID.Value };
            for (int i = 0; i < 8; ++i)
                proto.Texts.Add(new PacketNpcTextEntry(){Probability = npcText.Probabilities[i], BroadcastTextId = npcText.BroadcastTextId[i]});
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            var guid = new byte[8];

            int count = (int)packet.ReadBits(18);

            guid[0] = packet.ReadBit();

            var hasExtendedCost = new bool[count];
            var hasCondition = new bool[count];

            for (int i = 0; i < count; ++i)
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

            var tempList = new List<NpcVendor>();
            for (int i = 0; i < count; ++i)
            {
                NpcVendor vendor = new NpcVendor
                {
                    Type = packet.ReadUInt32("Type", i)
                };

                uint buyCount = packet.ReadUInt32("Buy Count", i);
                int maxCount = packet.ReadInt32("Max Count", i);
                vendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                if (vendor.Type == 2)
                    vendor.MaxCount = buyCount;

                packet.ReadInt32("Display ID", i);
                vendor.Slot = packet.ReadInt32("Item Position", i);
                packet.ReadInt32("Max Durability", i);
                packet.ReadInt32("Price", i);

                if (hasExtendedCost[i])
                    vendor.ExtendedCost = packet.ReadUInt32("Extended Cost", i);

                packet.ReadInt32("Item Upgrade ID", i);
                vendor.Item = packet.ReadInt32<ItemId>("Item ID", i);

                if (hasCondition[i])
                    vendor.PlayerConditionID = packet.ReadUInt32("Condition ID", i);

                tempList.Add(vendor);
            }

            packet.ReadByte("Byte28");

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 4);

            packet.WriteGuid("Guid", guid);

            uint entry = packet.WriteGuid("GUID", guid).GetEntry();
            tempList.ForEach(v =>
            {
                v.Entry = entry;
                Storage.NpcVendors.Add(v, packet.TimeSpan);
            });

            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            var guidBytes = new byte[8];

            guidBytes[1] = packet.ReadBit();
            guidBytes[6] = packet.ReadBit();

            uint titleLen = packet.ReadBits(11);

            guidBytes[3] = packet.ReadBit();
            guidBytes[2] = packet.ReadBit();
            guidBytes[5] = packet.ReadBit();
            guidBytes[4] = packet.ReadBit();
            guidBytes[0] = packet.ReadBit();
            guidBytes[7] = packet.ReadBit();

            uint count = packet.ReadBits("Spells", 19);

            Trainer trainer = new Trainer
            {
                Type = packet.ReadUInt32E<TrainerType>("TrainerType")
            };

            var tempList = new List<TrainerSpell>();
            for (int i = 0; i < count; ++i)
            {
                TrainerSpell trainerSpell = new TrainerSpell
                {
                    ReqAbility = new uint[3]
                };
                for (int j = 0; j < 3; ++j)
                    trainerSpell.ReqAbility[j] = packet.ReadUInt32<SpellId>("ReqAbility", i, j);

                packet.ReadByteE<TrainerSpellState>("Usable", i);
                trainerSpell.ReqSkillLine = packet.ReadUInt32("ReqSkillLine", i);
                trainerSpell.SpellId = packet.ReadUInt32<SpellId>("SpellID", i);
                trainerSpell.MoneyCost = packet.ReadUInt32("MoneyCost", i);
                trainerSpell.ReqLevel = packet.ReadByte("ReqLevel", i);
                trainerSpell.ReqSkillRank = packet.ReadUInt32("ReqSkillRank", i);

                tempList.Add(trainerSpell);
            }

            packet.ReadXORByte(guidBytes, 7);
            packet.ReadXORByte(guidBytes, 5);
            packet.ReadXORByte(guidBytes, 2);
            packet.ReadXORByte(guidBytes, 0);
            packet.ReadXORByte(guidBytes, 1);

            trainer.Greeting = packet.ReadWoWString("Greeting", titleLen);

            packet.ReadXORByte(guidBytes, 6);
            packet.ReadXORByte(guidBytes, 4);

            trainer.Id = packet.ReadUInt32("TrainerID");

            packet.ReadXORByte(guidBytes, 3);

            packet.WriteGuid("TrainerGUID", guidBytes);
            Storage.Trainers.Add(trainer, packet.TimeSpan);
            tempList.ForEach(trainerSpell =>
            {
                trainerSpell.TrainerId = trainer.Id;
                Storage.TrainerSpells.Add(trainerSpell, packet.TimeSpan);
            });

            CoreParsers.NpcHandler.AddToCreatureTrainers(trainer.Id, packet.TimeSpan, true);
        }

        [Parser(Opcode.CMSG_BANKER_ACTIVATE)]
        public static void HandleBankerActivate(Packet packet)
        {
            var guid = packet.StartBitStream(0, 1, 5, 2, 4, 3, 7, 6);
            packet.ParseBitStream(guid, 3, 6, 2, 0, 7, 4, 5, 1);

            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
            CoreParsers.NpcHandler.LastGossipOption.Guid = packet.WriteGuid("Guid", guid);
        }
    }
}

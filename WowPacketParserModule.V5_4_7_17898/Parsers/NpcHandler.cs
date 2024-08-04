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

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class NpcHandler
    {
        public static uint LastGossipPOIEntry;

        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        public static void HandleGossipHello(Packet packet)
        {
            var guid = new byte[8];
            packet.StartBitStream(guid, 6, 3, 4, 5, 1, 7, 2, 0);
            packet.ParseBitStream(guid, 6, 0, 1, 7, 2, 5, 4, 3);

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

            Storage.GossipSelects.Add(Tuple.Create(menuID, optionID), null, packet.TimeSpan);
            packetGossip.GossipUnit = packet.WriteGuid("GUID", guid);

            CoreParsers.NpcHandler.LastGossipOption.GossipSelectOption(menuID, optionID, packet.TimeSpan);
            CoreParsers.NpcHandler.TempGossipOptionPOI.GossipSelectOption(menuID, optionID, packet.TimeSpan);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            PacketGossipMessage packetGossip = packet.Holder.GossipMessage = new();
            var guidBytes = new byte[8];

            guidBytes[7] = packet.ReadBit();
            guidBytes[6] = packet.ReadBit();
            guidBytes[1] = packet.ReadBit();

            uint questgossips = packet.ReadBits(19);

            guidBytes[0] = packet.ReadBit();
            guidBytes[4] = packet.ReadBit();
            guidBytes[5] = packet.ReadBit();
            guidBytes[2] = packet.ReadBit();
            guidBytes[3] = packet.ReadBit();

            uint amountOfOptions = packet.ReadBits(20);

            var boxTextLen = new uint[amountOfOptions];
            var optionTextLen = new uint[amountOfOptions];
            for (int i = 0; i < amountOfOptions; ++i)
            {
                boxTextLen[i] = packet.ReadBits(12);
                optionTextLen[i] = packet.ReadBits(12);
            }

            var titleLen = new uint[questgossips];
            for (int i = 0; i < questgossips; ++i)
            {
                packet.ReadBit("Change Icon", i);
                titleLen[i] = packet.ReadBits(9);
            }

            for (var i = 0; i < questgossips; ++i)
            {
                packet.ReadUInt32E<QuestFlagsEx>("Flags 2", i);
                var quest = packet.ReadUInt32<QuestId>("Quest ID", i);
                packet.ReadInt32("Level", i);
                packet.ReadUInt32("Icon", i);
                packet.ReadUInt32E<QuestFlags>("Flags", i);
                var title = packet.ReadWoWString("Title", titleLen[i], i);

                packetGossip.Quests.Add(new GossipQuestOption()
                {
                    Title = title,
                    QuestId = quest
                });
            }

            packet.ReadXORByte(guidBytes, 6);

            var gossipOptions = new List<GossipMenuOption>((int)amountOfOptions);
            for (int i = 0; i < amountOfOptions; ++i)
            {
                GossipMenuOption gossipMenuOption = new GossipMenuOption();

                gossipMenuOption.BoxMoney = packet.ReadUInt32("Required money", i);
                gossipMenuOption.OptionText = packet.ReadWoWString("Text", optionTextLen[i], i);
                gossipMenuOption.OptionID = packet.ReadUInt32("OptionID", i);
                gossipMenuOption.OptionNpc = packet.ReadByteE<GossipOptionNpc>("OptionNPC", i);
                var boxText = packet.ReadWoWString("Box Text", boxTextLen[i], i);
                gossipMenuOption.BoxCoded = packet.ReadBool("Box", i);

                if (!string.IsNullOrEmpty(boxText))
                    gossipMenuOption.BoxText = boxText;

                gossipOptions.Add(gossipMenuOption);

                packetGossip.Options.Add(new GossipMessageOption()
                {
                    OptionIndex = gossipMenuOption.OptionID.Value,
                    OptionNpc = (int)gossipMenuOption.OptionNpc,
                    BoxCoded = gossipMenuOption.BoxCoded.Value,
                    BoxCost = gossipMenuOption.BoxMoney.Value,
                    Text = gossipMenuOption.OptionText,
                    BoxText = gossipMenuOption.BoxText
                });
            }

            packet.ReadXORByte(guidBytes, 2);

            uint textId = packetGossip.TextId = packet.ReadUInt32("Text Id");

            packet.ReadXORByte(guidBytes, 1);
            packet.ReadXORByte(guidBytes, 5);

            uint menuId = packetGossip.MenuId = packet.ReadUInt32("Menu Id");
            uint friendshipFactionID = packet.ReadUInt32("Friendship Faction");

            packet.ReadXORByte(guidBytes, 4);
            packet.ReadXORByte(guidBytes, 7);
            packet.ReadXORByte(guidBytes, 3);
            packet.ReadXORByte(guidBytes, 0);

            var guid = packet.WriteGuid("Guid", guidBytes);
            packetGossip.GossipSource = guid;

            GossipMenu gossip = new GossipMenu
            {
                MenuID = menuId,
                TextID = textId
            };

            gossip.ObjectType = guid.GetObjectType();
            gossip.ObjectEntry = guid.GetEntry();

            if (guid.GetObjectType() == ObjectType.Unit)
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

                if (Settings.TargetedDatabase < TargetedDatabase.Shadowlands)
                    g.FillOptionType(guid);

                Storage.GossipMenuOptions.Add((g.MenuID, g.OptionID), g, packet.TimeSpan);
            });

            Storage.Gossips.Add(gossip, packet.TimeSpan);

            CoreParsers.NpcHandler.AddGossipAddon(packetGossip.MenuId, (int)friendshipFactionID, 0, guid, packet.TimeSpan);

            CoreParsers.NpcHandler.UpdateLastGossipOptionActionMessage(packet.TimeSpan, gossip.MenuID);

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
            NpcTextMop npcText = new NpcTextMop();

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

            var entry = packet.ReadEntry("Entry");
            npcText.ID = (uint)entry.Key;

            if (entry.Value) // Can be masked
                return;

            Bit hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            packet.AddSniffData(StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");

            Storage.NpcTextsMop.Add(npcText, packet.TimeSpan);
            var proto = packet.Holder.NpcText = new() { Entry = npcText.ID.Value };
            for (int i = 0; i < 8; ++i)
                proto.Texts.Add(new PacketNpcTextEntry(){Probability = npcText.Probabilities[i], BroadcastTextId = npcText.BroadcastTextId[i]});
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

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            var guidBytes = new byte[8];

            uint count = packet.ReadBits("Spells", 19);
            guidBytes[3] = packet.ReadBit();
            guidBytes[2] = packet.ReadBit();
            guidBytes[0] = packet.ReadBit();
            guidBytes[7] = packet.ReadBit();
            guidBytes[1] = packet.ReadBit();
            guidBytes[5] = packet.ReadBit();
            uint titleLen = packet.ReadBits(11);
            guidBytes[6] = packet.ReadBit();
            guidBytes[4] = packet.ReadBit();

            packet.ReadXORByte(guidBytes, 3);

            var tempList = new List<TrainerSpell>();
            for (int i = 0; i < count; ++i)
            {
                packet.ReadByteE<TrainerSpellState>("Usable", i);
                TrainerSpell trainerSpell = new TrainerSpell
                {
                    SpellId = packet.ReadUInt32<SpellId>("SpellID", i),
                    ReqSkillLine = packet.ReadUInt32("ReqSkillLine", i),
                    MoneyCost = packet.ReadUInt32("MoneyCost", i),
                    ReqAbility = new uint[3]
                };
                for (int j = 0; j < 3; ++j)
                    trainerSpell.ReqAbility[j] = packet.ReadUInt32<SpellId>("ReqAbility", i, j);

                trainerSpell.ReqLevel = packet.ReadByte("ReqLevel", i);
                trainerSpell.ReqSkillRank = packet.ReadUInt32("ReqSkillRank", i);

                tempList.Add(trainerSpell);
            }

            Trainer trainer = new Trainer();
            packet.ReadXORByte(guidBytes, 1);
            packet.ReadXORByte(guidBytes, 6);
            packet.ReadXORByte(guidBytes, 0);
            trainer.Greeting = packet.ReadWoWString("Greeting", titleLen);
            trainer.Type = packet.ReadInt32E<TrainerType>("TrainerType");
            packet.ReadXORByte(guidBytes, 2);
            packet.ReadXORByte(guidBytes, 4);
            packet.ReadXORByte(guidBytes, 5);
            packet.ReadXORByte(guidBytes, 7);
            trainer.Id = packet.ReadUInt32("TrainerID");

            packet.WriteGuid("TrainerGUID", guidBytes);
            Storage.Trainers.Add(trainer, packet.TimeSpan);
            tempList.ForEach(trainerSpell =>
            {
                trainerSpell.TrainerId = trainer.Id;
                Storage.TrainerSpells.Add(trainerSpell, packet.TimeSpan);
            });
            CoreParsers.NpcHandler.AddToCreatureTrainers(trainer.Id, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_TRAINER_BUY_SPELL)]
        public static void HandleTrainerBuySpell(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadUInt32("TrainerID");
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

            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
            CoreParsers.NpcHandler.LastGossipOption.Guid = packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            var guid = new byte[8];

            guid[4] = packet.ReadBit();
            uint count = packet.ReadBits(18);
            var hasCondition = new bool[count];
            var hasExtendedCost = new bool[count];

            for (int i = 0; i < count; ++i)
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

            var tempList = new List<NpcVendor>();
            for (int i = 0; i < count; ++i)
            {
                NpcVendor vendor = new NpcVendor();
                packet.ReadInt32("Max Durability", i);
                vendor.Type = packet.ReadUInt32("Type", i); // 1 - item, 2 - currency
                uint buyCount = packet.ReadUInt32("Buy Count", i);

                if (hasExtendedCost[i])
                    vendor.ExtendedCost = packet.ReadUInt32("Extended Cost", i);

                packet.ReadInt32("Price", i);
                vendor.Item = packet.ReadInt32<ItemId>("Item ID", i);
                vendor.Slot = packet.ReadInt32("Item Position", i);

                if (hasCondition[i])
                    packet.ReadInt32("Condition ID", i);

                int maxCount = packet.ReadInt32("Max Count", i);
                vendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                if (vendor.Type == 2)
                    vendor.MaxCount = buyCount;

                packet.ReadInt32("Item Upgrade ID", i);
                packet.ReadInt32("Display ID", i);

                tempList.Add(vendor);
            }

            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadByte("Byte10");
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);

            uint entry = packet.WriteGuid("GUID", guid).GetEntry();
            tempList.ForEach(v =>
            {
                v.Entry = entry;
                Storage.NpcVendors.Add(v, packet.TimeSpan);
            });

            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
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

            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
            CoreParsers.NpcHandler.LastGossipOption.Guid = packet.WriteGuid("Guid", guid2);
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

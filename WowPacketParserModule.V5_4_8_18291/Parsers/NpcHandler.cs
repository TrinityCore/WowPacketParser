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

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class NpcHandler
    {
        public static uint LastGossipPOIEntry;

        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        public static void HandleGossipHello(Packet packet)
        {
            var guid = new byte[8];
            packet.StartBitStream(guid, 2, 4, 0, 3, 6, 7, 5, 1);
            packet.ParseBitStream(guid, 4, 7, 1, 0, 5, 3, 6, 2);

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

            uint questgossips = packet.ReadBits(19);

            var titleLen = new uint[questgossips];
            for (int i = 0; i < questgossips; ++i)
            {
                packet.ReadBit("Change Icon", i);
                titleLen[i] = packet.ReadBits(9);
            }

            guidBytes[5] = packet.ReadBit();
            guidBytes[7] = packet.ReadBit();
            guidBytes[4] = packet.ReadBit();
            guidBytes[0] = packet.ReadBit();

            uint amountOfOptions = packet.ReadBits(20);

            guidBytes[6] = packet.ReadBit();
            guidBytes[2] = packet.ReadBit();

            var boxTextLen = new uint[amountOfOptions];
            var optionTextLen = new uint[amountOfOptions];
            for (int i = 0; i < amountOfOptions; ++i)
            {
                boxTextLen[i] = packet.ReadBits(12);
                optionTextLen[i] = packet.ReadBits(12);
            }

            guidBytes[3] = packet.ReadBit();
            guidBytes[1] = packet.ReadBit();

            for (int i = 0; i < questgossips; ++i)
            {
                var title = packet.ReadWoWString("Title", titleLen[i], i);
                packet.ReadUInt32E<QuestFlags>("Flags", i);//528
                packet.ReadInt32("Level", i);//8
                packet.ReadUInt32("Icon", i);//4
                var quest = packet.ReadUInt32<QuestId>("Quest ID", i); //528
                packet.ReadUInt32E<QuestFlagsEx>("Flags 2", i);//532

                packetGossip.Quests.Add(new GossipQuestOption()
                {
                    Title = title,
                    QuestId = quest
                });
            }

            packet.ReadXORByte(guidBytes, 1);
            packet.ReadXORByte(guidBytes, 0);

            var gossipOptions = new List<GossipMenuOption>((int)amountOfOptions);
            for (int i = 0; i < amountOfOptions; ++i)
            {
                GossipMenuOption gossipMenuOption = new GossipMenuOption();

                gossipMenuOption.BoxMoney = packet.ReadUInt32("Required money", i);
                var boxText = packet.ReadWoWString("Box Text", boxTextLen[i], i);
                gossipMenuOption.OptionID = packet.ReadUInt32("OptionID", i);
                gossipMenuOption.BoxCoded = packet.ReadBool("Box", i);
                gossipMenuOption.OptionText = packet.ReadWoWString("Text", optionTextLen[i], i);
                gossipMenuOption.OptionNpc = packet.ReadByteE<GossipOptionNpc>("OptionNPC", i);

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

            packet.ReadXORByte(guidBytes, 5);
            packet.ReadXORByte(guidBytes, 3);
            uint menuId = packetGossip.MenuId = packet.ReadUInt32("Menu Id");
            packet.ReadXORByte(guidBytes, 2);
            packet.ReadXORByte(guidBytes, 6);
            packet.ReadXORByte(guidBytes, 4);
            uint friendshipFactionID = packet.ReadUInt32("Friendship Faction");
            packet.ReadXORByte(guidBytes, 7);
            uint textId = packetGossip.TextId = packet.ReadUInt32("Text Id");

            GossipMenu gossip = new GossipMenu
            {
                MenuID = menuId,
                TextID = textId
            };

            WowGuid guid = packet.WriteGuid("Guid", guidBytes);
            packetGossip.GossipSource = guid;

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

            CoreParsers.NpcHandler.AddGossipAddon(packetGossip.MenuId, (int)friendshipFactionID, guid, packet.TimeSpan);

            CoreParsers.NpcHandler.UpdateLastGossipOptionActionMessage(packet.TimeSpan, gossip.MenuID);

            packet.AddSniffData(StoreNameType.Gossip, (int)menuId, guid.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.SMSG_GOSSIP_POI)]
        public static void HandleGossipPoi(Packet packet)
        {
            var protoPoi = packet.Holder.GossipPoi = new();
            ++LastGossipPOIEntry;
            PointsOfInterest gossipPOI = new PointsOfInterest();
            gossipPOI.ID = "@ID+" + LastGossipPOIEntry.ToString();

            gossipPOI.Flags = protoPoi.Flags = (uint)packet.ReadInt32E<UnknownFlags>("Flags");

            Vector2 pos = packet.ReadVector2("Coordinates");
            protoPoi.Coordinates = pos;
            gossipPOI.PositionX = pos.X;
            gossipPOI.PositionY = pos.Y;

            gossipPOI.Icon = packet.ReadUInt32E<GossipPOIIcon>("Icon");
            gossipPOI.Importance = protoPoi.Importance = packet.ReadUInt32("Importance");
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
            packet.StartBitStream(guid, 4, 5, 1, 7, 0, 2, 6, 3);
            packet.ParseBitStream(guid, 4, 0, 2, 5, 1, 7, 3, 6);
            packet.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
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

            Bit hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            packet.AddSniffData(StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");

            Storage.NpcTextsMop.Add(npcText, packet.TimeSpan);
            var proto = packet.Holder.NpcText = new() { Entry = npcText.ID.Value };
            for (int i = 0; i < 8; ++i)
                proto.Texts.Add(new PacketNpcTextEntry(){Probability = npcText.Probabilities[i], BroadcastTextId = npcText.BroadcastTextId[i]});
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

            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
            CoreParsers.NpcHandler.LastGossipOption.Guid = packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            var guid = new byte[8];

            guid[4] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            int count = (int)packet.ReadBits("Spells", 19);
            uint titleLen = packet.ReadBits(11);
            guid[6] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[0] = packet.ReadBit();

            packet.ReadXORByte(guid, 4);

            var tempList = new List<TrainerSpell>();
            for (int i = 0; i < count; ++i)
            {
                TrainerSpell trainerSpell = new TrainerSpell
                {
                    ReqLevel = packet.ReadByte("ReqLevel", i),
                    MoneyCost = packet.ReadUInt32("MoneyCost", i),
                    SpellId = packet.ReadUInt32<SpellId>("SpellID", i),
                    ReqAbility = new uint[3]
                };

                for (int j = 0; j < 3; ++j)
                    trainerSpell.ReqAbility[j] = packet.ReadUInt32("ReqAbility", i, j);

                trainerSpell.ReqSkillLine = packet.ReadUInt32("ReqSkillLine", i);
                trainerSpell.ReqSkillRank = packet.ReadUInt32("ReqSkillRank", i);
                packet.ReadByteE<TrainerSpellState>("Usable", i);

                tempList.Add(trainerSpell);
            }

            Trainer trainer = new Trainer();
            trainer.Greeting = packet.ReadWoWString("Greeting", titleLen);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 3);
            trainer.Id = (uint)packet.ReadInt32("TrainerID");
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            trainer.Type = packet.ReadInt32E<TrainerType>("TrainerType");

            packet.WriteGuid("TrainerGUID", guid);
            Storage.Trainers.Add(trainer, packet.TimeSpan);
            tempList.ForEach(trainerSpell =>
            {
                trainerSpell.TrainerId = trainer.Id;
                Storage.TrainerSpells.Add(trainerSpell, packet.TimeSpan);
            });
            CoreParsers.NpcHandler.AddToCreatureTrainers(trainer.Id, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_LIST_INVENTORY)]
        public static void HandleNpcListInventory(Packet packet)
        {
            var guid = packet.StartBitStream(6, 7, 3, 1, 2, 0, 4, 5);
            packet.ParseBitStream(guid, 0, 7, 1, 6, 4, 3, 5, 2);

            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
            CoreParsers.NpcHandler.LastGossipOption.Guid = packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            var guid = new byte[8];

            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[6] = packet.ReadBit();

            uint count = packet.ReadBits(18);

            var unkBit = new uint[count];
            var hasExtendedCost = new bool[count];
            var hasCondition = new bool[count];

            for (int i = 0; i < count; ++i)
            {
                unkBit[i] = packet.ReadBit();
                hasExtendedCost[i] = !packet.ReadBit(); // +44
                hasCondition[i] = !packet.ReadBit(); // +36
            }

            guid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();

            packet.ReadByte("Byte10");

            var tempList = new List<NpcVendor>();
            for (int i = 0; i < count; ++i)
            {
                NpcVendor vendor = new NpcVendor();

                packet.AddValue("unkBit", unkBit[i], i);

                packet.ReadInt32("Max Durability", i);   // +16
                packet.ReadInt32("Price", i);   // +20
                vendor.Type = packet.ReadUInt32("Type", i);    // +4
                int maxCount = packet.ReadInt32("Max Count", i);     // +24
                vendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                packet.ReadInt32("Display ID", i);    // +12
                uint buyCount = packet.ReadUInt32("Buy Count", i);    // +28

                if (vendor.Type == 2)
                    vendor.MaxCount = buyCount;

                vendor.Item = packet.ReadInt32<ItemId>("Item ID", i);   // +8

                if (hasExtendedCost[i])
                    vendor.ExtendedCost = packet.ReadUInt32("Extended Cost", i);    // +36

                packet.ReadInt32("Item Upgrade ID", i);   // +32

                if (hasCondition[i])
                    vendor.PlayerConditionID = packet.ReadUInt32("Condition ID", i);    // +40

                vendor.Slot = packet.ReadInt32("Item Position", i);    // +0
                tempList.Add(vendor);
            }

            packet.ParseBitStream(guid, 3, 7, 0, 6, 2, 1, 4, 5);

            uint entry = packet.WriteGuid("Guid", guid).GetEntry();
            tempList.ForEach(v =>
            {
                v.Entry = entry;
                Storage.NpcVendors.Add(v, packet.TimeSpan);
            });

            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
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

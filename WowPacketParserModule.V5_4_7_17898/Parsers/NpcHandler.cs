using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

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
            packet.Translator.StartBitStream(guid, 6, 3, 4, 5, 1, 7, 2, 0);
            packet.Translator.ParseBitStream(guid, 6, 0, 1, 7, 2, 5, 4, 3);

            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            var guid = new byte[8];

            var menuEntry = packet.Translator.ReadUInt32("Menu Id");
            var gossipId = packet.Translator.ReadUInt32("GossipMenu Id");

            guid[2] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var bits8 = packet.Translator.ReadBits(8);

            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadWoWString("Box Text", bits8);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 4);

            Storage.GossipSelects.Add(Tuple.Create(menuEntry, gossipId), null, packet.TimeSpan);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            var guidBytes = new byte[8];

            guidBytes[7] = packet.Translator.ReadBit();
            guidBytes[6] = packet.Translator.ReadBit();
            guidBytes[1] = packet.Translator.ReadBit();

            uint questgossips = packet.Translator.ReadBits(19);

            guidBytes[0] = packet.Translator.ReadBit();
            guidBytes[4] = packet.Translator.ReadBit();
            guidBytes[5] = packet.Translator.ReadBit();
            guidBytes[2] = packet.Translator.ReadBit();
            guidBytes[3] = packet.Translator.ReadBit();

            uint amountOfOptions = packet.Translator.ReadBits(20);

            var boxTextLen = new uint[amountOfOptions];
            var optionTextLen = new uint[amountOfOptions];
            for (int i = 0; i < amountOfOptions; ++i)
            {
                boxTextLen[i] = packet.Translator.ReadBits(12);
                optionTextLen[i] = packet.Translator.ReadBits(12);
            }

            var titleLen = new uint[questgossips];
            for (int i = 0; i < questgossips; ++i)
            {
                packet.Translator.ReadBit("Change Icon", i);
                titleLen[i] = packet.Translator.ReadBits(9);
            }

            for (var i = 0; i < questgossips; ++i)
            {
                packet.Translator.ReadUInt32E<QuestFlags2>("Flags 2", i);
                packet.Translator.ReadUInt32<QuestId>("Quest ID", i);
                packet.Translator.ReadInt32("Level", i);
                packet.Translator.ReadUInt32("Icon", i);
                packet.Translator.ReadUInt32E<QuestFlags>("Flags", i);
                packet.Translator.ReadWoWString("Title", titleLen[i], i);
            }

            packet.Translator.ReadXORByte(guidBytes, 6);

            var gossipOptions = new List<GossipMenuOption>((int)amountOfOptions);
            for (int i = 0; i < amountOfOptions; ++i)
            {
                GossipMenuOption gossipMenuOption = new GossipMenuOption
                {
                    BoxMoney = packet.Translator.ReadUInt32("Required money", i),
                    OptionText = packet.Translator.ReadWoWString("Text", optionTextLen[i], i),
                    ID = packet.Translator.ReadUInt32("Index", i),
                    OptionIcon = packet.Translator.ReadByteE<GossipOptionIcon>("Icon", i),
                    BoxText = packet.Translator.ReadWoWString("Box Text", boxTextLen[i], i),
                    BoxCoded = packet.Translator.ReadBool("Box", i)
                };

                gossipOptions.Add(gossipMenuOption);
            }

            packet.Translator.ReadXORByte(guidBytes, 2);

            uint textId = packet.Translator.ReadUInt32("Text Id");

            packet.Translator.ReadXORByte(guidBytes, 1);
            packet.Translator.ReadXORByte(guidBytes, 5);

            uint menuId = packet.Translator.ReadUInt32("Menu Id");
            packet.Translator.ReadUInt32("Friendship Faction");

            packet.Translator.ReadXORByte(guidBytes, 4);
            packet.Translator.ReadXORByte(guidBytes, 7);
            packet.Translator.ReadXORByte(guidBytes, 3);
            packet.Translator.ReadXORByte(guidBytes, 0);

            packet.Translator.WriteGuid("Guid", guidBytes);

            GossipMenu gossip = new GossipMenu
            {
                Entry = menuId,
                TextID = textId
            };

            WowGuid64 guid = new WowGuid64(BitConverter.ToUInt64(guidBytes, 0));
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

        [Parser(Opcode.CMSG_QUERY_NPC_TEXT)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            packet.Translator.ReadInt32("Entry");

            var guid = new byte[8];
            packet.Translator.StartBitStream(guid, 0, 1, 2, 6, 4, 3, 7, 5);
            packet.Translator.ParseBitStream(guid, 3, 1, 4, 6, 2, 0, 5, 7);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            NpcTextMop npcText = new NpcTextMop();

            int size = packet.Translator.ReadInt32("Size");
            var data = packet.Translator.ReadBytes(size);

            Packet pkt = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Formatter, packet.FileName);
            npcText.Probabilities = new float[8];
            npcText.BroadcastTextId = new uint[8];
            for (int i = 0; i < 8; ++i)
                npcText.Probabilities[i] = pkt.Translator.ReadSingle("Probability", i);
            for (int i = 0; i < 8; ++i)
                npcText.BroadcastTextId[i] = pkt.Translator.ReadUInt32("Broadcast Text Id", i);

            pkt.ClosePacket(false);

            var entry = packet.Translator.ReadEntry("Entry");
            npcText.ID = (uint)entry.Key;

            if (entry.Value) // Can be masked
                return;

            Bit hasData = packet.Translator.ReadBit();
            if (!hasData)
                return; // nothing to do

            packet.AddSniffData(StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");

            Storage.NpcTextsMop.Add(npcText, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_GOSSIP_POI)]
        public static void HandleGossipPoi(Packet packet)
        {
            PointsOfInterest gossipPOI = new PointsOfInterest();
            gossipPOI.ID = ++LastGossipPOIEntry;

            gossipPOI.Flags = (uint)packet.Translator.ReadInt32E<UnknownFlags>("Flags");

            Vector2 pos = packet.Translator.ReadVector2("Coordinates");
            gossipPOI.PositionX = pos.X;
            gossipPOI.PositionY = pos.Y;

            gossipPOI.Icon = packet.Translator.ReadUInt32E<GossipPOIIcon>("Icon");
            gossipPOI.Importance = packet.Translator.ReadUInt32("Data");
            gossipPOI.Name = packet.Translator.ReadCString("Icon Name");

            Storage.GossipPOIs.Add(gossipPOI, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            var guidBytes = new byte[8];

            uint count = packet.Translator.ReadBits(19);
            guidBytes[3] = packet.Translator.ReadBit();
            guidBytes[2] = packet.Translator.ReadBit();
            guidBytes[0] = packet.Translator.ReadBit();
            guidBytes[7] = packet.Translator.ReadBit();
            guidBytes[1] = packet.Translator.ReadBit();
            guidBytes[5] = packet.Translator.ReadBit();
            uint titleLen = packet.Translator.ReadBits(11);
            guidBytes[6] = packet.Translator.ReadBit();
            guidBytes[4] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guidBytes, 3);

            var tempList = new List<NpcTrainer>();
            for (int i = 0; i < count; ++i)
            {
                NpcTrainer trainer = new NpcTrainer();

                packet.Translator.ReadByteE<TrainerSpellState>("State", i);
                trainer.SpellID = packet.Translator.ReadInt32<SpellId>("Spell ID", i);
                trainer.ReqSkillLine = packet.Translator.ReadUInt32("Required Skill", i);
                trainer.MoneyCost = packet.Translator.ReadUInt32("Cost", i);
                for (int j = 0; j < 3; ++j)
                    packet.Translator.ReadInt32("Int818", i, j);
                trainer.ReqLevel = packet.Translator.ReadByte("Required Level", i);
                trainer.ReqSkillRank = packet.Translator.ReadUInt32("Required Skill Level", i);

                tempList.Add(trainer);
            }

            packet.Translator.ReadXORByte(guidBytes, 1);
            packet.Translator.ReadXORByte(guidBytes, 6);
            packet.Translator.ReadXORByte(guidBytes, 0);
            packet.Translator.ReadWoWString("Title", titleLen);
            packet.Translator.ReadInt32E<TrainerType>("Type");
            packet.Translator.ReadXORByte(guidBytes, 2);
            packet.Translator.ReadXORByte(guidBytes, 4);
            packet.Translator.ReadXORByte(guidBytes, 5);
            packet.Translator.ReadXORByte(guidBytes, 7);
            packet.Translator.ReadInt32("Unk Int32"); // Same unk exists in CMSG_TRAINER_BUY_SPELL

            uint entry = packet.Translator.WriteGuid("Guid", guidBytes).GetEntry();
            tempList.ForEach(v =>
            {
                v.ID = entry;
                Storage.NpcTrainers.Add(v, packet.TimeSpan);
            });
        }

        [Parser(Opcode.CMSG_TRAINER_BUY_SPELL)]
        public static void HandleTrainerBuySpell(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadInt32("Unknown Int32"); // same unk exists in SMSG_TRAINER_LIST
            packet.Translator.ReadInt32<SpellId>("Spell ID");

            packet.Translator.StartBitStream(guid, 6, 2, 0, 7, 5, 3, 1, 4);
            packet.Translator.ParseBitStream(guid, 6, 0, 5, 1, 7, 4, 2, 3);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_LIST_INVENTORY)]
        public static void HandleNpcListInventory(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(1, 0, 6, 3, 5, 4, 7, 2);
            packet.Translator.ParseBitStream(guid, 0, 5, 6, 7, 1, 3, 4, 2);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            var guid = new byte[8];

            guid[4] = packet.Translator.ReadBit();
            uint count = packet.Translator.ReadBits(18);
            var hasCondition = new bool[count];
            var hasExtendedCost = new bool[count];

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadBit("Unk bit", i);
                hasCondition[i]= !packet.Translator.ReadBit();
                hasExtendedCost[i] = !packet.Translator.ReadBit();
            }

            guid[1] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 3);

            var tempList = new List<NpcVendor>();
            for (int i = 0; i < count; ++i)
            {
                NpcVendor vendor = new NpcVendor();
                packet.Translator.ReadInt32("Max Durability", i);
                vendor.Type = packet.Translator.ReadUInt32("Type", i); // 1 - item, 2 - currency
                uint buyCount = packet.Translator.ReadUInt32("Buy Count", i);

                if (hasExtendedCost[i])
                    vendor.ExtendedCost = packet.Translator.ReadUInt32("Extended Cost", i);

                packet.Translator.ReadInt32("Price", i);
                vendor.Item = packet.Translator.ReadInt32<ItemId>("Item ID", i);
                vendor.Slot = packet.Translator.ReadInt32("Item Position", i);

                if (hasCondition[i])
                    packet.Translator.ReadInt32("Condition ID", i);

                int maxCount = packet.Translator.ReadInt32("Max Count", i);
                vendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                if (vendor.Type == 2)
                    vendor.MaxCount = buyCount;

                packet.Translator.ReadInt32("Item Upgrade ID", i);
                packet.Translator.ReadInt32("Display ID", i);

                tempList.Add(vendor);
            }

            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadByte("Byte10");
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 7);

            uint entry = packet.Translator.WriteGuid("GUID", guid).GetEntry();
            tempList.ForEach(v =>
            {
                v.Entry = entry;
                Storage.NpcVendors.Add(v, packet.TimeSpan);
            });
        }

        [Parser(Opcode.CMSG_BUY_BANK_SLOT)]
        public static void HandleBuyBankSlot(Packet packet)
        {
            var guid2 = new byte[8];

            guid2[7] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 5);

            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.CMSG_BANKER_ACTIVATE)]
        public static void HandleBankerActivate(Packet packet)
        {
            var guid2 = new byte[8];

            guid2[3] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 5);

            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_SHOW_BANK)]
        public static void HandleShowBank(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 7, 1, 6, 4, 3, 5, 0, 2);
            packet.Translator.ParseBitStream(guid, 6, 0, 7, 3, 5, 1, 4, 2);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_THREAT_UPDATE)]
        public static void HandleThreatlistUpdate(Packet packet)
        {
            var guid = new byte[8];

            var count = packet.Translator.ReadBits("Size", 21);

            var hostileGUID = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                hostileGUID[i] = new byte[8];
                packet.Translator.StartBitStream(hostileGUID[i], 6, 5, 3, 0, 4, 7, 2, 1);
            }

            packet.Translator.StartBitStream(guid, 7, 2, 4, 5, 0, 6, 1, 3);

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ParseBitStream(hostileGUID[i], 3, 7, 0, 1, 6, 5);
                packet.Translator.ReadUInt32("Threat", i);
                packet.Translator.ParseBitStream(hostileGUID[i], 4, 2);
                packet.Translator.WriteGuid("Hostile", hostileGUID[i], i);

            }

            packet.Translator.ParseBitStream(guid, 4, 6, 5, 3, 0, 1, 7, 2);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_HIGHEST_THREAT_UPDATE)]
        public static void HandleHighestThreatlistUpdate(Packet packet)
        {
            var newHighestGUID = new byte[8];
            var guid = new byte[8];

            newHighestGUID[1] = packet.Translator.ReadBit();
            newHighestGUID[5] = packet.Translator.ReadBit();
            newHighestGUID[3] = packet.Translator.ReadBit();
            newHighestGUID[4] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            newHighestGUID[6] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var count = packet.Translator.ReadBits(21);
            guid[3] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();

            var hostileGUID = new byte[count][];

            for (var i = 0; i < count; i++)
            {
                hostileGUID[i] = new byte[8];

                packet.Translator.StartBitStream(hostileGUID[i], 7, 6, 3, 2, 0, 5, 1, 4);
            }

            guid[0] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            newHighestGUID[7] = packet.Translator.ReadBit();
            newHighestGUID[2] = packet.Translator.ReadBit();
            newHighestGUID[0] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(newHighestGUID, 7);

            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadXORByte(hostileGUID[i], 1);
                packet.Translator.ReadXORByte(hostileGUID[i], 2);
                packet.Translator.ReadXORByte(hostileGUID[i], 3);
                packet.Translator.ReadXORByte(hostileGUID[i], 0);
                packet.Translator.ReadXORByte(hostileGUID[i], 4);
                packet.Translator.ReadXORByte(hostileGUID[i], 7);

                packet.Translator.ReadInt32("Threat", i);

                packet.Translator.ReadXORByte(hostileGUID[i], 6);
                packet.Translator.ReadXORByte(hostileGUID[i], 5);

                packet.Translator.WriteGuid("Hostile", hostileGUID[i], i);
            }

            packet.Translator.ReadXORByte(newHighestGUID, 4);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(newHighestGUID, 3);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(newHighestGUID, 0);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(newHighestGUID, 5);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(newHighestGUID, 2);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(newHighestGUID, 6);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(newHighestGUID, 1);

            packet.Translator.WriteGuid("New Highest", newHighestGUID);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_THREAT_REMOVE)]
        public static void HandleRemoveThreatlist(Packet packet)
        {
            var hostileGUID = new byte[8];
            var victimGUID = new byte[8];

            victimGUID[2] = packet.Translator.ReadBit();
            victimGUID[1] = packet.Translator.ReadBit();
            hostileGUID[3] = packet.Translator.ReadBit();
            victimGUID[4] = packet.Translator.ReadBit();
            victimGUID[6] = packet.Translator.ReadBit();
            hostileGUID[6] = packet.Translator.ReadBit();
            victimGUID[3] = packet.Translator.ReadBit();
            hostileGUID[7] = packet.Translator.ReadBit();
            hostileGUID[4] = packet.Translator.ReadBit();
            victimGUID[5] = packet.Translator.ReadBit();
            hostileGUID[2] = packet.Translator.ReadBit();
            hostileGUID[5] = packet.Translator.ReadBit();
            hostileGUID[1] = packet.Translator.ReadBit();
            hostileGUID[0] = packet.Translator.ReadBit();
            victimGUID[7] = packet.Translator.ReadBit();
            victimGUID[0] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(victimGUID, 1);
            packet.Translator.ReadXORByte(hostileGUID, 3);
            packet.Translator.ReadXORByte(victimGUID, 3);
            packet.Translator.ReadXORByte(victimGUID, 6);
            packet.Translator.ReadXORByte(victimGUID, 0);
            packet.Translator.ReadXORByte(hostileGUID, 0);
            packet.Translator.ReadXORByte(hostileGUID, 4);
            packet.Translator.ReadXORByte(hostileGUID, 1);
            packet.Translator.ReadXORByte(hostileGUID, 6);
            packet.Translator.ReadXORByte(victimGUID, 4);
            packet.Translator.ReadXORByte(victimGUID, 5);
            packet.Translator.ReadXORByte(hostileGUID, 7);
            packet.Translator.ReadXORByte(victimGUID, 7);
            packet.Translator.ReadXORByte(victimGUID, 2);
            packet.Translator.ReadXORByte(hostileGUID, 2);
            packet.Translator.ReadXORByte(hostileGUID, 5);

            packet.Translator.WriteGuid("Hostile GUID", hostileGUID);
            packet.Translator.WriteGuid("GUID", victimGUID);
        }

        [Parser(Opcode.SMSG_THREAT_CLEAR)]
        public static void HandleClearThreatlist(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 5, 7, 1, 2, 6, 3, 4, 0);
            packet.Translator.ParseBitStream(guid, 2, 0, 6, 7, 5, 4, 1, 3);

            packet.Translator.WriteGuid("Guid", guid);
        }
    }
}

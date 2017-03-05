using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

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
            packet.Translator.StartBitStream(guid, 2, 4, 0, 3, 6, 7, 5, 1);
            packet.Translator.ParseBitStream(guid, 4, 7, 1, 0, 5, 3, 6, 2);

            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            var guid = new byte[8];

            var menuEntry = packet.Translator.ReadUInt32("Menu Id");
            var gossipId = packet.Translator.ReadUInt32("GossipMenu Id");

            packet.Translator.StartBitStream(guid, 3, 0, 1, 4, 7, 5, 6);

            var bits8 = packet.Translator.ReadBits(8);

            guid[2] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadWoWString("Box Text", bits8);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 1);

            Storage.GossipSelects.Add(Tuple.Create(menuEntry, gossipId), null, packet.TimeSpan);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            var guidBytes = new byte[8];

            uint questgossips = packet.Translator.ReadBits(19);

            var titleLen = new uint[questgossips];
            for (int i = 0; i < questgossips; ++i)
            {
                packet.Translator.ReadBit("Change Icon", i);
                titleLen[i] = packet.Translator.ReadBits(9);
            }

            guidBytes[5] = packet.Translator.ReadBit();
            guidBytes[7] = packet.Translator.ReadBit();
            guidBytes[4] = packet.Translator.ReadBit();
            guidBytes[0] = packet.Translator.ReadBit();

            uint amountOfOptions = packet.Translator.ReadBits(20);

            guidBytes[6] = packet.Translator.ReadBit();
            guidBytes[2] = packet.Translator.ReadBit();

            var boxTextLen = new uint[amountOfOptions];
            var optionTextLen = new uint[amountOfOptions];
            for (int i = 0; i < amountOfOptions; ++i)
            {
                boxTextLen[i] = packet.Translator.ReadBits(12);
                optionTextLen[i] = packet.Translator.ReadBits(12);
            }

            guidBytes[3] = packet.Translator.ReadBit();
            guidBytes[1] = packet.Translator.ReadBit();

            for (int i = 0; i < questgossips; ++i)
            {
                packet.Translator.ReadWoWString("Title", titleLen[i], i);
                packet.Translator.ReadUInt32E<QuestFlags>("Flags", i);//528
                packet.Translator.ReadInt32("Level", i);//8
                packet.Translator.ReadUInt32("Icon", i);//4
                packet.Translator.ReadUInt32<QuestId>("Quest ID", i); //528
                packet.Translator.ReadUInt32E<QuestFlags2>("Flags 2", i);//532
            }

            packet.Translator.ReadXORByte(guidBytes, 1);
            packet.Translator.ReadXORByte(guidBytes, 0);

            var gossipOptions = new List<GossipMenuOption>((int)amountOfOptions);
            for (int i = 0; i < amountOfOptions; ++i)
            {
                GossipMenuOption gossipMenuOption = new GossipMenuOption
                {
                    BoxMoney = packet.Translator.ReadUInt32("Required money", i),//3012
                    BoxText = packet.Translator.ReadWoWString("Box Text", boxTextLen[i], i),//12
                    ID = packet.Translator.ReadUInt32("Index", i),//0
                    BoxCoded = packet.Translator.ReadBool("Box", i),
                    OptionText = packet.Translator.ReadWoWString("Text", optionTextLen[i], i),
                    OptionIcon = packet.Translator.ReadByteE<GossipOptionIcon>("Icon", i)//4
                };

                gossipOptions.Add(gossipMenuOption);
            }

            packet.Translator.ReadXORByte(guidBytes, 5);
            packet.Translator.ReadXORByte(guidBytes, 3);
            uint menuId = packet.Translator.ReadUInt32("Menu Id");
            packet.Translator.ReadXORByte(guidBytes, 2);
            packet.Translator.ReadXORByte(guidBytes, 6);
            packet.Translator.ReadXORByte(guidBytes, 4);
            packet.Translator.ReadUInt32("Friendship Faction");
            packet.Translator.ReadXORByte(guidBytes, 7);
            uint textId = packet.Translator.ReadUInt32("Text Id");

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

            gossipOptions.ForEach(g =>
            {
                g.MenuID = menuId;
                Storage.GossipMenuOptions.Add(g, packet.TimeSpan);
            });

            Storage.Gossips.Add(gossip, packet.TimeSpan);

            packet.AddSniffData(StoreNameType.Gossip, (int)menuId, guid.GetEntry().ToString(CultureInfo.InvariantCulture));
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
            gossipPOI.Importance = packet.Translator.ReadUInt32("Importance");
            gossipPOI.Name = packet.Translator.ReadCString("Icon Name");

            Storage.GossipPOIs.Add(gossipPOI, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_QUERY_NPC_TEXT)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            packet.Translator.ReadInt32("Entry");

            var guid = new byte[8];
            packet.Translator.StartBitStream(guid, 4, 5, 1, 7, 0, 2, 6, 3);
            packet.Translator.ParseBitStream(guid, 4, 0, 2, 5, 1, 7, 3, 6);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var entry = packet.Translator.ReadEntry("Entry");
            if (entry.Value) // Can be masked
                return;

            NpcTextMop npcText = new NpcTextMop
            {
                ID = (uint)entry.Key
            };

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

            Bit hasData = packet.Translator.ReadBit();
            if (!hasData)
                return; // nothing to do

            packet.AddSniffData(StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");

            Storage.NpcTextsMop.Add(npcText, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_THREAT_REMOVE)]
        public static void HandleRemoveThreatlist(Packet packet)
        {
            var hostileGUID = new byte[8];
            var victimGUID = new byte[8];

            victimGUID[0] = packet.Translator.ReadBit();
            victimGUID[1] = packet.Translator.ReadBit();
            victimGUID[5] = packet.Translator.ReadBit();
            hostileGUID[4] = packet.Translator.ReadBit();
            hostileGUID[0] = packet.Translator.ReadBit();
            victimGUID[4] = packet.Translator.ReadBit();
            victimGUID[6] = packet.Translator.ReadBit();
            hostileGUID[7] = packet.Translator.ReadBit();
            hostileGUID[6] = packet.Translator.ReadBit();
            hostileGUID[3] = packet.Translator.ReadBit();
            victimGUID[2] = packet.Translator.ReadBit();
            hostileGUID[1] = packet.Translator.ReadBit();
            victimGUID[3] = packet.Translator.ReadBit();
            victimGUID[7] = packet.Translator.ReadBit();
            hostileGUID[5] = packet.Translator.ReadBit();
            hostileGUID[2] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(hostileGUID, 3);
            packet.Translator.ReadXORByte(hostileGUID, 0);
            packet.Translator.ReadXORByte(hostileGUID, 2);
            packet.Translator.ReadXORByte(victimGUID, 5);
            packet.Translator.ReadXORByte(victimGUID, 4);
            packet.Translator.ReadXORByte(victimGUID, 7);
            packet.Translator.ReadXORByte(victimGUID, 3);
            packet.Translator.ReadXORByte(victimGUID, 0);
            packet.Translator.ReadXORByte(hostileGUID, 4);
            packet.Translator.ReadXORByte(victimGUID, 1);
            packet.Translator.ReadXORByte(hostileGUID, 1);
            packet.Translator.ReadXORByte(victimGUID, 6);
            packet.Translator.ReadXORByte(hostileGUID, 7);
            packet.Translator.ReadXORByte(hostileGUID, 6);
            packet.Translator.ReadXORByte(victimGUID, 2);
            packet.Translator.ReadXORByte(hostileGUID, 5);

            packet.Translator.WriteGuid("Hostile GUID", hostileGUID);
            packet.Translator.WriteGuid("GUID", victimGUID);
        }

        [Parser(Opcode.SMSG_THREAT_CLEAR)]
        public static void HandleClearThreatlist(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 6, 7, 4, 5, 2, 1, 0, 3);
            packet.Translator.ParseBitStream(guid, 7, 0, 4, 3, 2, 1, 6, 5);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_THREAT_UPDATE)]
        public static void HandleThreatlistUpdate(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 5, 6, 1, 3, 7, 0, 4);

            var count = packet.Translator.ReadBits("Size", 21);

            var hostileGUID = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                hostileGUID[i] = new byte[8];
                packet.Translator.StartBitStream(hostileGUID[i], 2, 3, 6, 5, 1, 4, 0, 7);
            }

            guid[2] = packet.Translator.ReadBit();

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ParseBitStream(hostileGUID[i], 6, 7, 0, 1, 2, 5, 3, 4);
                packet.Translator.ReadUInt32("Threat", i);
                packet.Translator.WriteGuid("Hostile", hostileGUID[i], i);

            }

            packet.Translator.ParseBitStream(guid, 1, 4, 2, 3, 5, 6, 0, 7);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_TRAINER_LIST)]
        public static void HandleClientTrainerList(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 0, 2, 7, 6, 1, 4, 5, 3);
            packet.Translator.ParseBitStream(guid, 3, 6, 7, 5, 1, 0, 2, 4);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_TRAINER_LIST)]
        public static void HandleServerTrainerList(Packet packet)
        {
            var guid = new byte[8];

            guid[4] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            int count = (int)packet.Translator.ReadBits(19);
            uint titleLen = packet.Translator.ReadBits(11);
            guid[6] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 4);

            var tempList = new List<NpcTrainer>();
            for (int i = 0; i < count; ++i)
            {
                NpcTrainer trainer = new NpcTrainer
                {
                    ReqLevel = packet.Translator.ReadByte("Required Level", i),
                    MoneyCost = packet.Translator.ReadUInt32("Cost", i),
                    SpellID = packet.Translator.ReadInt32<SpellId>("Spell ID", i)
                };

                for (int j = 0; j < 3; ++j)
                    packet.Translator.ReadInt32("Int818", i, j);
                trainer.ReqSkillLine = packet.Translator.ReadUInt32("Required Skill", i);
                trainer.ReqSkillRank = packet.Translator.ReadUInt32("Required Skill Level", i);
                packet.Translator.ReadByteE<TrainerSpellState>("State", i);

                tempList.Add(trainer);
            }

            packet.Translator.ReadWoWString("Title", titleLen);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadInt32("Unk Int32"); // Same unk exists in CMSG_TRAINER_BUY_SPELL
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadInt32E<TrainerType>("Type");

            uint entry = packet.Translator.WriteGuid("Guid", guid).GetEntry();
            tempList.ForEach(v =>
            {
                v.ID = entry;
                Storage.NpcTrainers.Add(v, packet.TimeSpan);
            });
        }

        [Parser(Opcode.CMSG_LIST_INVENTORY)]
        public static void HandleNpcListInventory(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(6, 7, 3, 1, 2, 0, 4, 5);
            packet.Translator.ParseBitStream(guid, 0, 7, 1, 6, 4, 3, 5, 2);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            var guid = new byte[8];

            guid[5] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();

            uint count = packet.Translator.ReadBits(18);

            var unkBit = new uint[count];
            var hasExtendedCost = new bool[count];
            var hasCondition = new bool[count];

            for (int i = 0; i < count; ++i)
            {
                unkBit[i] = packet.Translator.ReadBit();
                hasExtendedCost[i] = !packet.Translator.ReadBit(); // +44
                hasCondition[i] = !packet.Translator.ReadBit(); // +36
            }

            guid[4] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();

            packet.Translator.ReadByte("Byte10");

            var tempList = new List<NpcVendor>();
            for (int i = 0; i < count; ++i)
            {
                NpcVendor vendor = new NpcVendor();

                packet.AddValue("unkBit", unkBit[i], i);

                packet.Translator.ReadInt32("Max Durability", i);   // +16
                packet.Translator.ReadInt32("Price", i);   // +20
                vendor.Type = packet.Translator.ReadUInt32("Type", i);    // +4
                int maxCount = packet.Translator.ReadInt32("Max Count", i);     // +24
                vendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                packet.Translator.ReadInt32("Display ID", i);    // +12
                uint buyCount = packet.Translator.ReadUInt32("Buy Count", i);    // +28

                if (vendor.Type == 2)
                    vendor.MaxCount = buyCount;

                vendor.Item = packet.Translator.ReadInt32<ItemId>("Item ID", i);   // +8

                if (hasExtendedCost[i])
                    vendor.ExtendedCost = packet.Translator.ReadUInt32("Extended Cost", i);    // +36

                packet.Translator.ReadInt32("Item Upgrade ID", i);   // +32

                if (hasCondition[i])
                    vendor.PlayerConditionID = packet.Translator.ReadUInt32("Condition ID", i);    // +40

                vendor.Slot = packet.Translator.ReadInt32("Item Position", i);    // +0
                tempList.Add(vendor);
            }

            packet.Translator.ParseBitStream(guid, 3, 7, 0, 6, 2, 1, 4, 5);

            uint entry = packet.Translator.WriteGuid("Guid", guid).GetEntry();
            tempList.ForEach(v =>
            {
                v.Entry = entry;
                Storage.NpcVendors.Add(v, packet.TimeSpan);
            });
        }


        [Parser(Opcode.SMSG_HIGHEST_THREAT_UPDATE)]
        public static void HandleHighestThreatlistUpdate(Packet packet)
        {
            var newHighestGUID = new byte[8];
            var guid = new byte[8];


            guid[3] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            newHighestGUID[3] = packet.Translator.ReadBit();
            newHighestGUID[6] = packet.Translator.ReadBit();
            newHighestGUID[1] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            newHighestGUID[2] = packet.Translator.ReadBit();
            newHighestGUID[5] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            newHighestGUID[4] = packet.Translator.ReadBit();
            var count = packet.Translator.ReadBits(21);

            var hostileGUID = new byte[count][];

            for (var i = 0; i < count; i++)
            {
                hostileGUID[i] = new byte[8];

                packet.Translator.StartBitStream(hostileGUID[i], 6, 1, 0, 2, 7, 4, 3, 5);
            }

            newHighestGUID[7] = packet.Translator.ReadBit();
            newHighestGUID[0] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(newHighestGUID, 4);

            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadXORByte(hostileGUID[i], 6);
                packet.Translator.ReadInt32("Threat", i);
                packet.Translator.ReadXORByte(hostileGUID[i], 4);
                packet.Translator.ReadXORByte(hostileGUID[i], 0);
                packet.Translator.ReadXORByte(hostileGUID[i], 3);
                packet.Translator.ReadXORByte(hostileGUID[i], 5);
                packet.Translator.ReadXORByte(hostileGUID[i], 2);
                packet.Translator.ReadXORByte(hostileGUID[i], 1);
                packet.Translator.ReadXORByte(hostileGUID[i], 7);

                packet.Translator.WriteGuid("Hostile", hostileGUID[i], i);
            }

            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(newHighestGUID, 5);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(newHighestGUID, 1);
            packet.Translator.ReadXORByte(newHighestGUID, 0);
            packet.Translator.ReadXORByte(newHighestGUID, 2);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(newHighestGUID, 7);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(newHighestGUID, 3);
            packet.Translator.ReadXORByte(newHighestGUID, 6);
            packet.Translator.ReadXORByte(guid, 5);

            packet.Translator.WriteGuid("New Highest", newHighestGUID);
            packet.Translator.WriteGuid("Guid", guid);
        }
    }
}

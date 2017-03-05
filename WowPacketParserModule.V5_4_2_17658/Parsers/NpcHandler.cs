using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

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
            packet.Translator.StartBitStream(guid, 5, 2, 0, 4, 7, 1, 6, 3);
            packet.Translator.ParseBitStream(guid, 3, 4, 6, 1, 0, 2, 7, 5);

            packet.Translator.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            var guidBytes = new byte[8];

            guidBytes[7] = packet.Translator.ReadBit();
            guidBytes[6] = packet.Translator.ReadBit();
            guidBytes[0] = packet.Translator.ReadBit();

            int questgossips = (int)packet.Translator.ReadBits(19);

            guidBytes[4] = packet.Translator.ReadBit();

            var titleLen = new uint[questgossips];
            for (int i = 0; i < questgossips; ++i)
            {
                titleLen[i] = packet.Translator.ReadBits(9);
                packet.Translator.ReadBit("Change Icon", i);
            }

            guidBytes[3] = packet.Translator.ReadBit();
            guidBytes[2] = packet.Translator.ReadBit();

            uint amountOfOptions = packet.Translator.ReadBits(20);

            var boxTextLen = new uint[amountOfOptions];
            var optionTextLen = new uint[amountOfOptions];
            for (int i = 0; i < amountOfOptions; ++i)
            {
                boxTextLen[i] = packet.Translator.ReadBits(12);
                optionTextLen[i] = packet.Translator.ReadBits(12);
            }

            guidBytes[1] = packet.Translator.ReadBit();
            guidBytes[5] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guidBytes, 2);

            var gossipOptions = new List<GossipMenuOption>((int)amountOfOptions);
            for (int i = 0; i < amountOfOptions; ++i)
            {
                GossipMenuOption gossipOption = new GossipMenuOption
                {
                    BoxText = packet.Translator.ReadWoWString("Box Text", boxTextLen[i], i),
                    OptionText = packet.Translator.ReadWoWString("Text", optionTextLen[i], i),
                    ID = packet.Translator.ReadUInt32("Index", i),
                    BoxCoded = packet.Translator.ReadBool("Box", i),
                    OptionIcon = packet.Translator.ReadByteE<GossipOptionIcon>("Icon", i),
                    BoxMoney = packet.Translator.ReadUInt32("Required money", i)
                };

                gossipOptions.Add(gossipOption);
            }

            for (int i = 0; i < questgossips; ++i)
            {
                packet.Translator.ReadInt32("Level", i);
                packet.Translator.ReadUInt32E<QuestFlags>("Flags", i);
                packet.Translator.ReadUInt32("Icon", i);
                packet.Translator.ReadWoWString("Title", titleLen[i], i);
                packet.Translator.ReadUInt32<QuestId>("Quest ID", i);
                packet.Translator.ReadUInt32E<QuestFlags2>("Flags 2", i);
            }

            packet.Translator.ReadXORByte(guidBytes, 7);

            packet.Translator.ReadUInt32("Friendship Faction");

            packet.Translator.ReadXORByte(guidBytes, 3);
            packet.Translator.ReadXORByte(guidBytes, 1);

            uint textId = packet.Translator.ReadUInt32("Text Id");

            packet.Translator.ReadXORByte(guidBytes, 5);

            uint menuId = packet.Translator.ReadUInt32("Menu Id");

            packet.Translator.ReadXORByte(guidBytes, 6);
            packet.Translator.ReadXORByte(guidBytes, 4);
            packet.Translator.ReadXORByte(guidBytes, 0);

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

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            var guid = new byte[8];

            var menuEntry = packet.Translator.ReadUInt32("Menu Id");
            var gossipId = packet.Translator.ReadUInt32("GossipMenu Id");

            guid[3] = packet.Translator.ReadBit();
            var bits8 = packet.Translator.ReadBits(8);
            guid[7] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadWoWString("Box Text", bits8);
            packet.Translator.ReadXORByte(guid, 3);

            Storage.GossipSelects.Add(Tuple.Create(menuEntry, gossipId), null, packet.TimeSpan);
            packet.Translator.WriteGuid("GUID", guid);
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

        [Parser(Opcode.CMSG_QUERY_NPC_TEXT)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            packet.Translator.ReadInt32("Entry");

            var guid = new byte[8];
            packet.Translator.StartBitStream(guid, 5, 6, 7, 4, 3, 0, 2, 1);
            packet.Translator.ParseBitStream(guid, 0, 7, 1, 4, 3, 5, 2, 6);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE)]
        public static void HandleNpcTextUpdate(Packet packet)
        { 
            Bit hasData = packet.Translator.ReadBit();
            if (!hasData)
                return; // nothing to do

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

            packet.AddSniffData(StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");

            Storage.NpcTextsMop.Add(npcText, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_VENDOR_INVENTORY)]
        public static void HandleVendorInventoryList(Packet packet)
        {
            var guid = new byte[8];

            int count = (int)packet.Translator.ReadBits(18);

            guid[0] = packet.Translator.ReadBit();

            var hasExtendedCost = new bool[count];
            var hasCondition = new bool[count];

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadBit("Unk bit", i);
                hasExtendedCost[i] = !packet.Translator.ReadBit();
                hasCondition[i] = !packet.Translator.ReadBit();
            }

            guid[3] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 6);

            var tempList = new List<NpcVendor>();
            for (int i = 0; i < count; ++i)
            {
                NpcVendor vendor = new NpcVendor
                {
                    Type = packet.Translator.ReadUInt32("Type", i)
                };

                uint buyCount = packet.Translator.ReadUInt32("Buy Count", i);
                int maxCount = packet.Translator.ReadInt32("Max Count", i);
                vendor.MaxCount = maxCount == -1 ? 0 : (uint)maxCount; // TDB
                if (vendor.Type == 2)
                    vendor.MaxCount = buyCount;

                packet.Translator.ReadInt32("Display ID", i);
                vendor.Slot = packet.Translator.ReadInt32("Item Position", i);
                packet.Translator.ReadInt32("Max Durability", i);
                packet.Translator.ReadInt32("Price", i);

                if (hasExtendedCost[i])
                    vendor.ExtendedCost = packet.Translator.ReadUInt32("Extended Cost", i);

                packet.Translator.ReadInt32("Item Upgrade ID", i);
                vendor.Item = packet.Translator.ReadInt32<ItemId>("Item ID", i);

                if (hasCondition[i])
                    vendor.PlayerConditionID = packet.Translator.ReadUInt32("Condition ID", i);

                tempList.Add(vendor);
            }

            packet.Translator.ReadByte("Byte28");

            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 4);

            packet.Translator.WriteGuid("Guid", guid);

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
            var guidBytes = new byte[8];

            guidBytes[1] = packet.Translator.ReadBit();
            guidBytes[6] = packet.Translator.ReadBit();

            uint titleLen = packet.Translator.ReadBits(11);

            guidBytes[3] = packet.Translator.ReadBit();
            guidBytes[2] = packet.Translator.ReadBit();
            guidBytes[5] = packet.Translator.ReadBit();
            guidBytes[4] = packet.Translator.ReadBit();
            guidBytes[0] = packet.Translator.ReadBit();
            guidBytes[7] = packet.Translator.ReadBit();

            uint count = packet.Translator.ReadBits("Count", 19);

            packet.Translator.ReadInt32E<TrainerType>("Type");

            var tempList = new List<NpcTrainer>();
            for (int i = 0; i < count; ++i)
            {
                NpcTrainer trainer = new NpcTrainer();
                for (int j = 0; j < 3; ++j)
                    packet.Translator.ReadInt32("Int818", i, j);

                packet.Translator.ReadByteE<TrainerSpellState>("State", i);
                trainer.ReqSkillLine = packet.Translator.ReadUInt32("Required Skill", i);
                trainer.SpellID = packet.Translator.ReadInt32<SpellId>("Spell ID", i);
                trainer.MoneyCost = packet.Translator.ReadUInt32("Cost", i);
                trainer.ReqLevel = packet.Translator.ReadByte("Required Level", i);
                trainer.ReqSkillRank = packet.Translator.ReadUInt32("Required Skill Level", i);

                tempList.Add(trainer);
            }

            packet.Translator.ReadXORByte(guidBytes, 7);
            packet.Translator.ReadXORByte(guidBytes, 5);
            packet.Translator.ReadXORByte(guidBytes, 2);
            packet.Translator.ReadXORByte(guidBytes, 0);
            packet.Translator.ReadXORByte(guidBytes, 1);

            packet.Translator.ReadWoWString("Title", titleLen);

            packet.Translator.ReadXORByte(guidBytes, 6);
            packet.Translator.ReadXORByte(guidBytes, 4);

            packet.Translator.ReadInt32("Unk Int32"); // Same unk exists in CMSG_TRAINER_BUY_SPELL

            packet.Translator.ReadXORByte(guidBytes, 3);

            uint entry = packet.Translator.WriteGuid("Guid", guidBytes).GetEntry();
            tempList.ForEach(v =>
            {
                v.ID = entry;
                Storage.NpcTrainers.Add(v, packet.TimeSpan);
            });
        }

        [Parser(Opcode.CMSG_BANKER_ACTIVATE)]
        public static void HandleBankerActivate(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(0, 1, 5, 2, 4, 3, 7, 6);
            packet.Translator.ParseBitStream(guid, 3, 6, 2, 0, 7, 4, 5, 1);

            packet.Translator.WriteGuid("Guid", guid);
        }
    }
}

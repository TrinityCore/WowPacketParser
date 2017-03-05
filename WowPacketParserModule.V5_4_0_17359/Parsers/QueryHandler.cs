using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class QueryHandler
    {
        [Parser(Opcode.CMSG_QUERY_CREATURE)]
        public static void HandleCreatureQuery(Packet packet)
        {
            var entry = packet.Translator.ReadInt32("Entry");
        }

        [Parser(Opcode.CMSG_DB_QUERY_BULK)]
        public static void HandleDBQueryBulk(Packet packet)
        {
            packet.Translator.ReadInt32E<DB2Hash>("DB2 File");
            var count = packet.Translator.ReadBits(21);

            var guids = new byte[count][];
            for (var i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                packet.Translator.StartBitStream(guids[i], 3, 4, 7, 2, 5, 1, 6, 0);
            }

            packet.Translator.ResetBitReader();
            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadXORBytes(guids[i], 6, 1, 2);
                packet.Translator.ReadInt32("Entry", i);
                packet.Translator.ReadXORBytes(guids[i], 4, 5, 7, 0, 3);
                packet.Translator.WriteGuid("Guid", guids[i], i);
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_CREATURE_RESPONSE)]
        public static void HandleCreatureQueryResponse(Packet packet)
        {
            var entry = packet.Translator.ReadEntry("Entry");
            Bit hasData = packet.Translator.ReadBit();
            if (!hasData)
                return; // nothing to do

            CreatureTemplate creature = new CreatureTemplate
            {
                Entry = (uint)entry.Key
            };

            var stringLens = new int[4][];
            for (int i = 0; i < 4; i++)
            {
                stringLens[i] = new int[2];
                stringLens[i][0] = (int)packet.Translator.ReadBits(11);
                stringLens[i][1] = (int)packet.Translator.ReadBits(11);
            }

            uint qItemCount = packet.Translator.ReadBits(22);
            uint lenS4 = packet.Translator.ReadBits(6);
            uint lenS3 = packet.Translator.ReadBits(11);
            uint lenS5 = packet.Translator.ReadBits(11);

            creature.RacialLeader = packet.Translator.ReadBit("Racial Leader");

            packet.Translator.ResetBitReader();

            creature.Type = packet.Translator.ReadInt32E<CreatureType>("Type");
            creature.KillCredits = new uint?[2];
            creature.KillCredits[1] = packet.Translator.ReadUInt32("Kill Credit 1");
            creature.ModelIDs = new uint?[4];
            creature.ModelIDs[3] = packet.Translator.ReadUInt32("Display ID 3");
            creature.ModelIDs[2] = packet.Translator.ReadUInt32("Display ID 2");

            //TODO: move to creature_questitems
            //creature.QuestItems = new uint[qItemCount];
            for (int i = 0; i < qItemCount; ++i)
                /*creature.QuestItems[i] = (uint)*/packet.Translator.ReadInt32<ItemId>("Quest Item", i);

            creature.RequiredExpansion = packet.Translator.ReadUInt32E<ClientType>("Expansion");

            var name = new string[8];
            var femaleName = new string[4];
            for (int i = 0; i < 4; ++i)
            {
                if (stringLens[i][0] > 1)
                    name[i] = packet.Translator.ReadCString("Name", i);
                if (stringLens[i][1] > 1)
                    femaleName[i] = packet.Translator.ReadCString("Female Name", i);
            }
            creature.Name = name[0];
            creature.FemaleName = femaleName[0];

            if (lenS5 > 1)
                packet.Translator.ReadCString("string5");

            creature.ManaModifier = packet.Translator.ReadSingle("Modifier 2");
            creature.ModelIDs[0] = packet.Translator.ReadUInt32("Display ID 0");

            if (lenS4 > 1)
                creature.IconName = packet.Translator.ReadCString("Icon Name");

            creature.KillCredits[0] = packet.Translator.ReadUInt32("Kill Credit 0");
            creature.ModelIDs[1] = packet.Translator.ReadUInt32("Display ID 1");

            if (lenS3 > 1)
                creature.SubName = packet.Translator.ReadCString("Sub Name");

            creature.TypeFlags = packet.Translator.ReadUInt32E<CreatureTypeFlag>("Type Flags");
            creature.TypeFlags2 = packet.Translator.ReadUInt32("Creature Type Flags 2"); // Missing enum

            creature.HealthModifier = packet.Translator.ReadSingle("Modifier 1");
            creature.Family = packet.Translator.ReadInt32E<CreatureFamily>("Family");
            creature.Rank = packet.Translator.ReadInt32E<CreatureRank>("Rank");
            creature.MovementID = packet.Translator.ReadUInt32("Movement ID");

            packet.AddSniffData(StoreNameType.Unit, entry.Key, "QUERY_RESPONSE");

            Storage.CreatureTemplates.Add(creature, packet.TimeSpan);

            ObjectName objectName = new ObjectName
            {
                ObjectType = ObjectType.Unit,
                ID = entry.Key,
                Name = creature.Name
            };
            Storage.ObjectNames.Add(objectName, packet.TimeSpan);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            var size = packet.Translator.ReadInt32("Size");
            var data = packet.Translator.ReadBytes(size);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Formatter, packet.FileName);

            var type = packet.Translator.ReadUInt32E<DB2Hash>("DB2 File");
            packet.Translator.ReadTime("Hotfix date");
            var entry = packet.Translator.ReadInt32("Entry");
            if (entry < 0)
            {
                packet.Formatter.AppendItem("Row {0} has been removed.", -entry);
                HotfixStoreMgr.RemoveRecord(type, entry);
            }
            else
            {
                packet.AddSniffData(StoreNameType.None, entry, type.ToString());
                HotfixStoreMgr.AddRecord(type, entry, db2File);
                db2File.ClosePacket(false);
            }
        }

        [Parser(Opcode.SMSG_REALM_QUERY_RESPONSE)]
        public static void HandleRealmQueryResponse(Packet packet)
        {
            packet.Translator.ReadByte("Unk byte");
            packet.Translator.ReadInt32("Realm Id");

            var bits278 = packet.Translator.ReadBits(8);
            packet.Translator.ReadBit();
            var bits22 = packet.Translator.ReadBits(8);

            packet.Translator.ReadWoWString("Realmname", bits22);
            packet.Translator.ReadWoWString("Realmname (without white char)", bits278);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_PAGE_TEXT_RESPONSE)]
        public static void HandlePageTextResponse(Packet packet)
        {
            Bit hasData = packet.Translator.ReadBit();
            if (!hasData)
            {
                packet.Translator.ReadUInt32("Entry");
                return; // nothing to do
            }

            PageText pageText = new PageText();

            uint textLen = packet.Translator.ReadBits(12);

            packet.Translator.ResetBitReader();
            pageText.Text = packet.Translator.ReadWoWString("Page Text", textLen);

            uint entry = packet.Translator.ReadUInt32("Entry");
            pageText.ID = entry;
            pageText.NextPageID = packet.Translator.ReadUInt32("Next Page");
            packet.Translator.ReadUInt32("Entry");

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
            Storage.PageTexts.Add(pageText, packet.TimeSpan);
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

        [Parser(Opcode.SMSG_QUERY_PLAYER_NAME_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            var guid = new byte[8];
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.Translator.StartBitStream(guid, 5, 7, 3, 0, 4, 1, 6, 2);

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);

            var hasData = packet.Translator.ReadByte("Byte18");
            if (hasData == 0)
            {
                packet.Translator.ReadInt32("Int24");
                packet.Translator.ReadByte("Race");
                packet.Translator.ReadByte("Gender");
                packet.Translator.ReadByte("Level");
                packet.Translator.ReadByte("Class");
                packet.Translator.ReadInt32("Realm Id");
            }

            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 2);

            if (hasData == 0)
            {
                guid2[6] = packet.Translator.ReadBit();
                guid1[7] = packet.Translator.ReadBit();

                var bits38 = packet.Translator.ReadBits(6);

                packet.Translator.StartBitStream(guid2, 1, 7, 2);

                guid1[4] = packet.Translator.ReadBit();
                guid2[4] = packet.Translator.ReadBit();
                guid2[0] = packet.Translator.ReadBit();
                guid1[1] = packet.Translator.ReadBit();

                var count = new int[5];
                for (var i = 0; i < 5; ++i)
                    count[i] = (int)packet.Translator.ReadBits(7);

                guid1[3] = packet.Translator.ReadBit();
                guid2[3] = packet.Translator.ReadBit();

                packet.Translator.StartBitStream(guid1, 5, 0);
                guid2[5] = packet.Translator.ReadBit();

                packet.Translator.ReadBit(); // fake bit

                guid1[2] = packet.Translator.ReadBit();
                guid1[6] = packet.Translator.ReadBit();

                packet.Translator.ReadWoWString("Name", bits38);

                packet.Translator.ReadXORByte(guid2, 4);
                packet.Translator.ReadXORByte(guid1, 3);
                packet.Translator.ReadXORByte(guid2, 6);
                packet.Translator.ReadXORByte(guid1, 2);
                packet.Translator.ReadXORByte(guid1, 4);
                packet.Translator.ReadXORByte(guid2, 5);
                packet.Translator.ReadXORByte(guid2, 1);
                packet.Translator.ReadXORByte(guid2, 7);

                for (var i = 0; i < 5; ++i)
                    packet.Translator.ReadWoWString("Name Declined", count[i], i);

                packet.Translator.ReadXORByte(guid2, 3);
                packet.Translator.ReadXORByte(guid1, 7);
                packet.Translator.ReadXORByte(guid1, 1);
                packet.Translator.ReadXORByte(guid1, 6);
                packet.Translator.ReadXORByte(guid2, 0);
                packet.Translator.ReadXORByte(guid1, 0);
                packet.Translator.ReadXORByte(guid2, 2);
                packet.Translator.ReadXORByte(guid1, 5);

                packet.Translator.WriteGuid("Guid1", guid1);
                packet.Translator.WriteGuid("Guid2", guid2);
            }

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_NAME_QUERY)]
        public static void HandleNameQuery(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 5, 7, 0, 1);

            var hasRealmId2 = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var hasRealmId1 = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid, 3, 2, 4);
            packet.Translator.ParseBitStream(guid, 0, 1, 3, 4, 6, 5, 2, 7);

            if (hasRealmId1)
                packet.Translator.ReadInt32("Realm Id 1");

            if (hasRealmId2)
                packet.Translator.ReadInt32("Realm Id 2");

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_QUERY_NPC_TEXT)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            var entry = packet.Translator.ReadInt32("Entry");

            var guid = new byte[8];
            packet.Translator.StartBitStream(guid, 7, 3, 1, 5, 6, 4, 0, 2);
            packet.Translator.ParseBitStream(guid, 1, 5, 2, 7, 3, 6, 4, 0);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_QUERY_PAGE_TEXT)]
        public static void HandlePageTextQuery(Packet packet)
        {
            var guid = new byte[8];
            packet.Translator.ReadInt32("Entry");

            packet.Translator.StartBitStream(guid, 0, 7, 5, 2, 1, 3, 4, 6);
            packet.Translator.ParseBitStream(guid, 7, 4, 6, 5, 2, 3, 0, 1);

            packet.Translator.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE)]
        public static void HandleQuestQueryResponse(Packet packet)
        {
            var hasData = packet.Translator.ReadBit();
            if (!hasData)
            {
                packet.Translator.ReadUInt32("Entry");
                return; // nothing to do
            }

            var quest = new QuestTemplate();

            var bits907 = packet.Translator.ReadBits(12);
            var count = packet.Translator.ReadBits(19);

            var len2949_20 = new uint[count];
            var counter = new uint[count];

            for (var i = 0; i < count; ++i)
            {
                len2949_20[i] = packet.Translator.ReadBits(8);
                counter[i] = packet.Translator.ReadBits(22);
            }

            var bits2432 = packet.Translator.ReadBits(11);
            var bits2048 = packet.Translator.ReadBits(8);
            var bits2112 = packet.Translator.ReadBits(10);
            var bits157 = packet.Translator.ReadBits(12);
            var bits1657 = packet.Translator.ReadBits(9);
            var bits2368 = packet.Translator.ReadBits(8);
            var bits1792 = packet.Translator.ReadBits(10);
            var bits29 = packet.Translator.ReadBits(9);

            packet.Translator.ReadInt32("Int2E34");
            packet.Translator.ReadInt32("Int4C");
            packet.Translator.ReadSingle("Float54");

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("Int2E10", i);
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadInt32("IntEA", i);
                packet.Translator.ReadWoWString("StringEA", len2949_20[i], i);
                packet.Translator.ReadByte("ByteED", i);
                packet.Translator.ReadByte("ByteEA", i);

                for (var j = 0; j < counter[i]; ++j)
                    packet.Translator.ReadInt32("Int118", i, j);
            }

            packet.Translator.ReadInt32("Int58");
            packet.Translator.ReadInt32("Int2E54");
            packet.Translator.ReadSingle("Float6C");
            packet.Translator.ReadWoWString("String2500", bits2368);
            packet.Translator.ReadInt32("Int34");
            packet.Translator.ReadWoWString("String19E4", bits1657);
            packet.Translator.ReadInt32("Int2E74");

            for (var i = 0; i < 4; ++i)
            {
                packet.Translator.ReadInt32("int3001+16", i);
                packet.Translator.ReadInt32("int3001+0", i);
            }

            packet.Translator.ReadInt32("Int1C");
            packet.Translator.ReadSingle("Float68");
            packet.Translator.ReadInt32("Int2E28");

            for (var i = 0; i < 5; ++i)
            {
                packet.Translator.ReadInt32("int2986+40", i);
                packet.Translator.ReadInt32("int2986+0", i);
                packet.Translator.ReadInt32("int2986+20", i);
            }

            packet.Translator.ReadInt32("Int1BE8");
            packet.Translator.ReadInt32("Int2E7C");
            packet.Translator.ReadInt32("Int1BF8");
            packet.Translator.ReadInt32("Int1BFC");
            packet.Translator.ReadInt32("Int2E90");
            packet.Translator.ReadInt32("Int2E48");
            packet.Translator.ReadWoWString("String74", bits29);
            packet.Translator.ReadInt32("Int2E2C");
            packet.Translator.ReadInt32("Int2E50");
            packet.Translator.ReadInt32("Int2E64");
            packet.Translator.ReadInt32("Int1BEC");
            packet.Translator.ReadInt32("Int60");
            packet.Translator.ReadInt32("Int2E88");
            packet.Translator.ReadInt32("Int2E94");
            packet.Translator.ReadInt32("Int2E6C");
            packet.Translator.ReadInt32("Int14");
            packet.Translator.ReadInt32("Int2E20");
            packet.Translator.ReadInt32("Int2E30");
            packet.Translator.ReadInt32("Int2E24");
            packet.Translator.ReadInt32("Int1BF0");
            packet.Translator.ReadInt32("Int2E4C");
            packet.Translator.ReadInt32("Int2E68");
            packet.Translator.ReadInt32("Int20");
            packet.Translator.ReadInt32("Int1BF4");
            packet.Translator.ReadWoWString("String2100", bits2112);
            packet.Translator.ReadInt32("Int2E08");
            packet.Translator.ReadInt32("Int38");
            packet.Translator.ReadInt32("Int5C");
            packet.Translator.ReadWoWString("String2600", bits2432);
            packet.Translator.ReadInt32("Int24");
            packet.Translator.ReadInt32("Int2E58");
            packet.Translator.ReadInt32("Int30");
            packet.Translator.ReadInt32("Int64");
            packet.Translator.ReadInt32("Int44");
            packet.Translator.ReadInt32("Int2E00");
            packet.Translator.ReadInt32("Int2E44");
            packet.Translator.ReadInt32("Int2EA0");
            packet.Translator.ReadInt32("Int28");
            packet.Translator.ReadInt32("Int2E1C");
            packet.Translator.ReadInt32("Int40");
            packet.Translator.ReadWoWString("StringE2C", bits907);
            packet.Translator.ReadInt32("Int2E60");
            packet.Translator.ReadWoWString("String2000", bits2048);
            packet.Translator.ReadInt32("Int2E70");
            packet.Translator.ReadInt32("Int2E5C");
            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadInt32("Int50");
            packet.Translator.ReadInt32("Int1BE4");
            packet.Translator.ReadWoWString("String1C00", bits1792);
            packet.Translator.ReadInt32("Int3C");
            packet.Translator.ReadInt32("Int2C");
            packet.Translator.ReadWoWString("String274", bits157);
            packet.Translator.ReadInt32("Int48");
            packet.Translator.ReadInt32("Int2E80");
            packet.Translator.ReadInt32("Int2E40");
            packet.Translator.ReadInt32("Int2E9C");
            packet.Translator.ReadInt32("Int2E84");
            packet.Translator.ReadInt32("Int2E38");
            packet.Translator.ReadInt32("Int2E04");
            packet.Translator.ReadInt32("Int2E98");
            packet.Translator.ReadInt32("Int2E3C");
            packet.Translator.ReadInt32("Int2E78");
            packet.Translator.ReadInt32("Int70");
            packet.Translator.ReadInt32("Int2E8C");

            var id = packet.Translator.ReadInt32("Int2F00");

            //packet.AddSniffData(StoreNameType.Quest, id.Key, "QUERY_RESPONSE");
            //Storage.QuestTemplates.Add((uint)id.Key, quest, packet.TimeSpan);
        }
    }
}

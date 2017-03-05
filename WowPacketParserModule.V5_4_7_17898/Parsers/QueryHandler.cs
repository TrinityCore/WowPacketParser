using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class QueryHandler
    {
        [Parser(Opcode.CMSG_QUERY_CREATURE)]
        public static void HandleCreatureQuery(Packet packet)
        {
            packet.Translator.ReadInt32("Entry");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_CREATURE_RESPONSE)]
        public static void HandleCreatureQueryResponse(Packet packet)
        {
            var entry = packet.Translator.ReadEntry("Entry");

            CreatureTemplate creature = new CreatureTemplate
            {
                Entry = (uint)entry.Key
            };

            Bit hasData = packet.Translator.ReadBit();
            if (!hasData)
                return; // nothing to do

            creature.ModelIDs = new uint?[4];
            creature.KillCredits = new uint?[2];

            creature.RacialLeader = packet.Translator.ReadBit("Racial Leader");

            uint bits2C = packet.Translator.ReadBits(6);

            var stringLens = new int[4][];
            for (int i = 0; i < 4; i++)
            {
                stringLens[i] = new int[2];
                stringLens[i][1] = (int)packet.Translator.ReadBits(11);
                stringLens[i][0] = (int)packet.Translator.ReadBits(11);
            }

            uint qItemCount = packet.Translator.ReadBits(22);
            uint bits24 = packet.Translator.ReadBits(11);
            uint bits1C = packet.Translator.ReadBits(11);

            creature.ManaModifier = packet.Translator.ReadSingle("Modifier 2");

            var name = new string[4];
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

            creature.HealthModifier = packet.Translator.ReadSingle("Modifier 1");

            creature.KillCredits[1] = packet.Translator.ReadUInt32();
            creature.ModelIDs[2] = packet.Translator.ReadUInt32();

            //TODO: move to creature_questitems
            //creature.QuestItems = new uint[qItemCount];
            for (int i = 0; i < qItemCount; ++i)
                /*creature.QuestItems[i] = (uint)*/packet.Translator.ReadInt32<ItemId>("Quest Item", i);

            creature.Type = packet.Translator.ReadInt32E<CreatureType>("Type");

            if (bits2C > 1)
                creature.IconName = packet.Translator.ReadCString("Icon Name");

            creature.TypeFlags = packet.Translator.ReadUInt32E<CreatureTypeFlag>("Type Flags");
            creature.TypeFlags2 = packet.Translator.ReadUInt32("Creature Type Flags 2"); // Missing enum

            creature.KillCredits[0] = packet.Translator.ReadUInt32();
            creature.Family = packet.Translator.ReadInt32E<CreatureFamily>("Family");
            creature.MovementID = packet.Translator.ReadUInt32("Movement ID");
            creature.RequiredExpansion = packet.Translator.ReadUInt32E<ClientType>("Expansion");

            creature.ModelIDs[0] = packet.Translator.ReadUInt32();
            creature.ModelIDs[1] = packet.Translator.ReadUInt32();

            if (bits1C > 1)
                packet.Translator.ReadCString("String");

            creature.Rank = packet.Translator.ReadInt32E<CreatureRank>("Rank");

            if (bits24 > 1)
                creature.SubName = packet.Translator.ReadCString("Sub Name");

            creature.ModelIDs[3] = packet.Translator.ReadUInt32();

            for (int i = 0; i < 4; ++i)
                packet.AddValue("Display ID", creature.ModelIDs[i], i);
            for (int i = 0; i < 2; ++i)
                packet.AddValue("Kill Credit", creature.KillCredits[i], i);

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

        [Parser(Opcode.CMSG_DB_QUERY_BULK)]
        public static void HandleDBQueryBulk(Packet packet)
        {
            packet.Translator.ReadInt32E<DB2Hash>("DB2 File");
            var count = packet.Translator.ReadBits(21);

            var guids = new byte[count][];
            for (var i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                packet.Translator.StartBitStream(guids[i], 2, 4, 3, 6, 7, 1, 5, 0);
            }

            packet.Translator.ResetBitReader();

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadXORByte(guids[i], 5);
                packet.Translator.ReadXORByte(guids[i], 4);
                packet.Translator.ReadXORByte(guids[i], 3);

                packet.Translator.ReadInt32("Entry", i);

                packet.Translator.ReadXORByte(guids[i], 7);
                packet.Translator.ReadXORByte(guids[i], 0);
                packet.Translator.ReadXORByte(guids[i], 2);
                packet.Translator.ReadXORByte(guids[i], 1);
                packet.Translator.ReadXORByte(guids[i], 6);

                packet.Translator.WriteGuid("Guid", guids[i], i);
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            var entry = packet.Translator.ReadInt32("Entry");

            var type = packet.Translator.ReadUInt32E<DB2Hash>("DB2 File");

            packet.Translator.ReadTime("Hotfix date");

            var size = packet.Translator.ReadInt32("Size");
            var data = packet.Translator.ReadBytes(size);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Formatter, packet.FileName);

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

        [Parser(Opcode.CMSG_NAME_QUERY)]
        public static void HandleNameQuery(Packet packet)
        {
            var guid = new byte[8];

            guid[4] = packet.Translator.ReadBit();
            var hasRealmId2 = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            var hasRealmId1 = packet.Translator.ReadBit();
            packet.Translator.ParseBitStream(guid, 1, 0, 2, 6, 4, 7, 5, 3);

            if (hasRealmId1)
                packet.Translator.ReadInt32("Realm Id 1");

            if (hasRealmId2)
                packet.Translator.ReadInt32("Realm Id 2");

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_QUERY_PLAYER_NAME_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            var guid4 = new byte[8];
            var guid5 = new byte[8];
            var guid1 = new byte[8];

            var bit18 = false;

            var nameLen = 0;

            guid1[4] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid1, 1);

            var hasData = packet.Translator.ReadByte("HasData");
            if (hasData == 0)
            {
                packet.Translator.ReadInt32("Realm Id");
                packet.Translator.ReadInt32("Int1C");
                packet.Translator.ReadByte("Level");
                packet.Translator.ReadByteE<Race>("Race");
                packet.Translator.ReadByteE<Gender>("Gender");
                packet.Translator.ReadByteE<Class>("Class");
            }

            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 6);

            if (hasData == 0)
            {
                guid4[1] = packet.Translator.ReadBit();
                guid5[2] = packet.Translator.ReadBit();
                guid5[5] = packet.Translator.ReadBit();
                guid5[0] = packet.Translator.ReadBit();
                guid5[7] = packet.Translator.ReadBit();
                guid4[5] = packet.Translator.ReadBit();
                guid5[3] = packet.Translator.ReadBit();
                guid4[4] = packet.Translator.ReadBit();
                bit18 = packet.Translator.ReadBit();
                guid5[6] = packet.Translator.ReadBit();
                nameLen = (int)packet.Translator.ReadBits(6);
                guid4[2] = packet.Translator.ReadBit();
                guid4[6] = packet.Translator.ReadBit();
                guid4[0] = packet.Translator.ReadBit();
                guid5[1] = packet.Translator.ReadBit();
                guid5[4] = packet.Translator.ReadBit();

                var count = new int[5];
                for (var i = 0; i < 5; ++i)
                    count[i] = (int)packet.Translator.ReadBits(7);

                guid4[7] = packet.Translator.ReadBit();
                guid4[3] = packet.Translator.ReadBit();

                for (var i = 0; i < 5; ++i)
                    packet.Translator.ReadWoWString("Name Declined", count[i], i);

                packet.Translator.ReadXORByte(guid5, 4);
                packet.Translator.ReadXORByte(guid5, 5);
                packet.Translator.ReadXORByte(guid5, 7);
                packet.Translator.ReadXORByte(guid5, 0);
                packet.Translator.ReadXORByte(guid4, 7);
                packet.Translator.ReadXORByte(guid4, 1);
                packet.Translator.ReadXORByte(guid4, 0);
                packet.Translator.ReadXORByte(guid4, 4);
                packet.Translator.ReadXORByte(guid5, 1);
                packet.Translator.ReadXORByte(guid4, 2);
                packet.Translator.ReadXORByte(guid4, 5);
                packet.Translator.ReadXORByte(guid5, 6);
                packet.Translator.ReadXORByte(guid5, 2);
                packet.Translator.ReadXORByte(guid5, 3);

                packet.Translator.ReadWoWString("Name", nameLen);

                packet.Translator.ReadXORByte(guid4, 3);
                packet.Translator.ReadXORByte(guid4, 6);

                packet.Translator.WriteGuid("Guid4", guid4);
                packet.Translator.WriteGuid("Guid5", guid5);
            }

            packet.Translator.WriteGuid("Guid1", guid1);
        }

        [Parser(Opcode.CMSG_QUERY_REALM_NAME)]
        public static void HandleRealmQuery(Packet packet)
        {
            packet.Translator.ReadInt32("Realm Id");
        }

        [Parser(Opcode.SMSG_REALM_QUERY_RESPONSE)]
        public static void HandleRealmQueryResponse(Packet packet)
        {
            packet.Translator.ReadInt32("Realm Id");
            packet.Translator.ReadByte("Unk byte");

            var bits22 = packet.Translator.ReadBits(8);
            packet.Translator.ReadBit();
            var bits278 = packet.Translator.ReadBits(8);

            packet.Translator.ReadWoWString("Realmname", bits22);
            packet.Translator.ReadWoWString("Realmname (without white char)", bits278);
        }

        [Parser(Opcode.CMSG_QUERY_QUEST_INFO)]
        public static void HandleQuestQuery(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadInt32<QuestId>("Quest ID");
            packet.Translator.StartBitStream(guid, 2, 3, 5, 1, 7, 0, 6, 4);
            packet.Translator.ParseBitStream(guid, 4, 2, 1, 3, 5, 7, 0, 6);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE)]
        public static void HandleQuestQueryResponse(Packet packet)
        {
            var id = packet.Translator.ReadUInt32("Entry");
            var hasData = packet.Translator.ReadBit("hasData");
            if (!hasData)
                return; // nothing to do

            var bits1658 = (int)packet.Translator.ReadBits(9);
            var bits30 = (int)packet.Translator.ReadBits(9);
            var bits1793 = (int)packet.Translator.ReadBits(10);
            var bits2369 = (int)packet.Translator.ReadBits(8);
            var bits158 = (int)packet.Translator.ReadBits(12);
            var bits2049 = (int)packet.Translator.ReadBits(8);
            var bits908 = (int)packet.Translator.ReadBits(12);
            var bits2113 = (int)packet.Translator.ReadBits(10);
            var bits2433 = (int)packet.Translator.ReadBits(11);
            var count = (int)packet.Translator.ReadBits("Requirement Count", 19);

            var bits2949 = new int[count];
            var counter = new int[count];
            for (var i = 0; i < count; ++i)
            {
                counter[i] = (int)packet.Translator.ReadBits(22);
                bits2949[i] = (int)packet.Translator.ReadBits(8);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("byte2949+0", i);
                packet.Translator.ReadWoWString("string2949+20", bits2949[i], i);
                packet.Translator.ReadByte("byte2949+4", i);
                packet.Translator.ReadByte("byte2949+5", i);
                packet.Translator.ReadInt32("int2949+12", i);

                for (var j = 0; j < counter[i]; ++j)
                    packet.Translator.ReadInt32("Unk UInt32", i, j);

                packet.Translator.ReadInt32("int2949+16", i);
                packet.Translator.ReadInt32("int2949+8", i);
            }

            packet.Translator.ReadInt32("int2959");
            packet.Translator.ReadInt32("int2964");
            packet.Translator.ReadInt32("int19");

            packet.Translator.ReadSingle("float27");

            packet.Translator.ReadInt32("int14");
            packet.Translator.ReadInt32("int2970");

            packet.Translator.ReadSingle("float22");

            packet.Translator.ReadInt32("int2955");
            packet.Translator.ReadInt32("int12");
            packet.Translator.ReadInt32("int2981");
            packet.Translator.ReadInt32("int2957");

            packet.Translator.ReadWoWString("string2049", bits2049);

            packet.Translator.ReadInt32("int2977");
            packet.Translator.ReadInt32("int15");
            packet.Translator.ReadInt32("int2966");
            packet.Translator.ReadInt32("int2960");
            packet.Translator.ReadInt32("int8");
            packet.Translator.ReadInt32("int1789");

            packet.Translator.ReadWoWString("string2433", bits2433);
            packet.Translator.ReadWoWString("string1658", bits1658);

            packet.Translator.ReadInt32("int2946");
            packet.Translator.ReadInt32("int2945");
            packet.Translator.ReadInt32("int17");
            packet.Translator.ReadInt32("int2968");
            packet.Translator.ReadInt32("int2947");

            packet.Translator.ReadSingle("float28");

            packet.Translator.ReadInt32("int18");
            packet.Translator.ReadInt32("int29");

            packet.Translator.ReadWoWString("string1793", bits1793);

            for (var i = 0; i < 4; ++i)
            {
                packet.Translator.ReadInt32("int3001+16");
                packet.Translator.ReadInt32("int3001+0");
            }

            packet.Translator.ReadWoWString("string158", bits158);

            packet.Translator.ReadInt32("int2963");
            packet.Translator.ReadInt32("int2965");
            packet.Translator.ReadInt32("int20");

            for (var i = 0; i < 5; ++i)
            {
                packet.Translator.ReadInt32("int2986+20");
                packet.Translator.ReadInt32("int2986+0");
                packet.Translator.ReadInt32("int2986+40");
            }

            packet.Translator.ReadWoWString("string2369", bits2369);

            packet.Translator.ReadInt32("int2974");
            packet.Translator.ReadInt32("int1791");
            packet.Translator.ReadInt32("int1787");
            packet.Translator.ReadInt32("int2952");
            packet.Translator.ReadInt32("int11");
            packet.Translator.ReadInt32("int21");
            packet.Translator.ReadInt32("int2979");
            packet.Translator.ReadInt32("int16");
            packet.Translator.ReadInt32("int2962");

            packet.Translator.ReadWoWString("string908", bits908);

            packet.Translator.ReadInt32("int1792");
            packet.Translator.ReadInt32("int6");
            packet.Translator.ReadInt32("int2975");
            packet.Translator.ReadInt32("int2984");
            packet.Translator.ReadInt32("int2973");
            packet.Translator.ReadInt32("int25");
            packet.Translator.ReadInt32("int10");
            packet.Translator.ReadInt32("int2961");
            packet.Translator.ReadInt32("int1788");
            packet.Translator.ReadInt32("int9");
            packet.Translator.ReadInt32("int1786");
            packet.Translator.ReadInt32("int2980");
            packet.Translator.ReadInt32("int23");
            packet.Translator.ReadInt32("int2976");
            packet.Translator.ReadInt32("int2956");
            packet.Translator.ReadInt32("int2972");
            packet.Translator.ReadInt32("int13");
            packet.Translator.ReadInt32("int26");

            packet.Translator.ReadWoWString("string30", bits30);

            packet.Translator.ReadInt32("int2954");
            packet.Translator.ReadInt32("int2982");
            packet.Translator.ReadInt32("int2967");
            packet.Translator.ReadInt32("int2985");

            packet.Translator.ReadWoWString("string2113", bits2113);

            packet.Translator.ReadInt32("int2983");
            packet.Translator.ReadInt32("int2953");
            packet.Translator.ReadInt32("int2958");
            packet.Translator.ReadInt32("int2969");
            packet.Translator.ReadInt32("int24");
            packet.Translator.ReadInt32("int1790");
            packet.Translator.ReadInt32("int2971");
            packet.Translator.ReadInt32("int7");
            packet.Translator.ReadInt32("int2978");
        }

        [Parser(Opcode.CMSG_QUERY_PAGE_TEXT)]
        public static void HandlePageTextQuery(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadInt32("Entry");

            packet.Translator.StartBitStream(guid, 1, 5, 2, 3, 6, 4, 0, 7);
            packet.Translator.ParseBitStream(guid, 6, 4, 0, 3, 7, 5, 2, 1);

            packet.Translator.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_PAGE_TEXT_RESPONSE)]
        public static void HandlePageTextResponse(Packet packet)
        {
            uint entry = packet.Translator.ReadUInt32("Entry");

            Bit hasData = packet.Translator.ReadBit();
            if (!hasData)
                return; // nothing to do

            PageText pageText = new PageText();
            pageText.ID = entry;

            uint textLen = packet.Translator.ReadBits(12);

            packet.Translator.ResetBitReader();
            packet.Translator.ReadUInt32("Entry");
            pageText.Text = packet.Translator.ReadWoWString("Page Text", textLen);

            pageText.NextPageID = packet.Translator.ReadUInt32("Next Page");

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
            Storage.PageTexts.Add(pageText, packet.TimeSpan);
        }
    }
}

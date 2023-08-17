using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
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
            packet.ReadInt32("Entry");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_CREATURE_RESPONSE)]
        public static void HandleCreatureQueryResponse(Packet packet)
        {
            PacketQueryCreatureResponse response = packet.Holder.QueryCreatureResponse = new PacketQueryCreatureResponse();
            var entry = packet.ReadEntry("Entry");

            CreatureTemplate creature = new CreatureTemplate
            {
                Entry = (uint)entry.Key
            };
            response.Entry = (uint) entry.Key;

            Bit hasData = packet.ReadBit();
            response.HasData = hasData;
            if (!hasData)
                return; // nothing to do

            creature.ModelIDs = new uint?[4];
            creature.KillCredits = new uint?[2];

            creature.RacialLeader = packet.ReadBit("Racial Leader");

            uint bits2C = packet.ReadBits(6);

            var stringLens = new int[4][];
            for (int i = 0; i < 4; i++)
            {
                stringLens[i] = new int[2];
                stringLens[i][1] = (int)packet.ReadBits(11);
                stringLens[i][0] = (int)packet.ReadBits(11);
            }

            uint qItemCount = packet.ReadBits(22);
            uint bits24 = packet.ReadBits(11);
            uint bits1C = packet.ReadBits(11);

            creature.ManaModifier = packet.ReadSingle("Modifier 2");

            var name = new string[4];
            var femaleName = new string[4];
            for (int i = 0; i < 4; ++i)
            {
                if (stringLens[i][0] > 1)
                    name[i] = packet.ReadCString("Name", i);
                if (stringLens[i][1] > 1)
                    femaleName[i] = packet.ReadCString("Female Name", i);
            }

            creature.Name = name[0];
            creature.FemaleName = femaleName[0];

            creature.HealthModifier = packet.ReadSingle("Modifier 1");

            creature.KillCredits[1] = packet.ReadUInt32();
            creature.ModelIDs[2] = packet.ReadUInt32();

            //TODO: move to creature_questitems
            //creature.QuestItems = new uint[qItemCount];
            for (int i = 0; i < qItemCount; ++i)
                /*creature.QuestItems[i] = (uint)*/response.QuestItems.Add((uint)packet.ReadInt32<ItemId>("Quest Item", i));

            creature.Type = packet.ReadInt32E<CreatureType>("Type");

            if (bits2C > 1)
                creature.IconName = packet.ReadCString("Icon Name");

            creature.TypeFlags = packet.ReadUInt32E<CreatureTypeFlag>("Type Flags");
            creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2"); // Missing enum

            creature.KillCredits[0] = packet.ReadUInt32();
            creature.Family = packet.ReadInt32E<CreatureFamily>("Family");
            creature.MovementID = packet.ReadUInt32("Movement ID");
            creature.RequiredExpansion = packet.ReadUInt32E<ClientType>("Expansion");

            creature.ModelIDs[0] = packet.ReadUInt32();
            creature.ModelIDs[1] = packet.ReadUInt32();

            if (bits1C > 1)
                creature.TitleAlt = packet.ReadCString("TitleAlt");

            creature.Rank = packet.ReadInt32E<CreatureRank>("Rank");

            if (bits24 > 1)
                creature.SubName = packet.ReadCString("Sub Name");

            creature.ModelIDs[3] = packet.ReadUInt32();

            for (int i = 0; i < 4; ++i)
            {
                packet.AddValue("Display ID", creature.ModelIDs[i], i);
                response.Models.Add(creature.ModelIDs[i] ?? 0);
            }
            for (int i = 0; i < 2; ++i)
                response.KillCredits.Add(packet.AddValue("Kill Credit", creature.KillCredits[i], i) ?? 0);

            packet.AddSniffData(StoreNameType.Unit, entry.Key, "QUERY_RESPONSE");

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CreatureTemplateLocale localesCreature = new CreatureTemplateLocale
                {
                    ID = (uint)entry.Key,
                    Name = creature.Name,
                    NameAlt = creature.FemaleName,
                    Title = creature.SubName,
                    TitleAlt = creature.TitleAlt
                };

                Storage.LocalesCreatures.Add(localesCreature, packet.TimeSpan);
            }

            Storage.CreatureTemplates.Add(creature.Entry.Value, creature, packet.TimeSpan);

            ObjectName objectName = new ObjectName
            {
                ObjectType = StoreNameType.Unit,
                ID = entry.Key,
                Name = creature.Name
            };
            Storage.ObjectNames.Add(objectName, packet.TimeSpan);

            response.Name = creature.Name;
            response.NameAlt = creature.FemaleName;
            response.Title = creature.SubName;
            response.TitleAlt = creature.TitleAlt;
            response.IconName = creature.IconName;
            response.TypeFlags = (uint?)creature.TypeFlags ?? 0;
            response.TypeFlags2 = creature.TypeFlags2 ?? 0;
            response.Type = (int?)creature.Type ?? 0;
            response.Family = (int?)creature.Family ?? 0;
            response.Rank = (int?)creature.Rank ?? 0;
            response.HpMod = creature.HealthModifier ?? 1.0f;
            response.ManaMod = creature.ManaModifier ?? 1.0f;
            response.Leader = creature.RacialLeader ?? false;
            response.Expansion = (uint?) creature.RequiredExpansion ?? 0;
            response.MovementId = creature.MovementID ?? 0;
        }

        [Parser(Opcode.CMSG_DB_QUERY_BULK)]
        public static void HandleDBQueryBulk(Packet packet)
        {
            packet.ReadInt32E<DB2Hash>("DB2 File");
            var count = packet.ReadBits(21);

            var guids = new byte[count][];
            for (var i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                packet.StartBitStream(guids[i], 2, 4, 3, 6, 7, 1, 5, 0);
            }

            packet.ResetBitReader();

            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guids[i], 5);
                packet.ReadXORByte(guids[i], 4);
                packet.ReadXORByte(guids[i], 3);

                packet.ReadInt32("Entry", i);

                packet.ReadXORByte(guids[i], 7);
                packet.ReadXORByte(guids[i], 0);
                packet.ReadXORByte(guids[i], 2);
                packet.ReadXORByte(guids[i], 1);
                packet.ReadXORByte(guids[i], 6);

                packet.WriteGuid("Guid", guids[i], i);
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            var entry = packet.ReadInt32("Entry");

            var type = packet.ReadUInt32E<DB2Hash>("DB2 File");

            packet.ReadTime("Hotfix date");

            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

            if (entry < 0)
            {
                packet.WriteLine("Row {0} has been removed.", -entry);
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

            guid[4] = packet.ReadBit();
            var hasRealmId2 = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasRealmId1 = packet.ReadBit();
            packet.ParseBitStream(guid, 1, 0, 2, 6, 4, 7, 5, 3);

            if (hasRealmId1)
                packet.ReadInt32("Realm Id 1");

            if (hasRealmId2)
                packet.ReadInt32("Realm Id 2");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_QUERY_PLAYER_NAME_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            PacketQueryPlayerNameResponseWrapper responses = packet.Holder.QueryPlayerNameResponse = new();
            PacketQueryPlayerNameResponse response = new();
            responses.Responses.Add(response);
            var guid4 = new byte[8];
            var guid5 = new byte[8];
            var guid1 = new byte[8];

            var bit18 = false;

            var nameLen = 0;

            guid1[4] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            packet.ReadXORByte(guid1, 1);

            var hasData = packet.ReadByte("HasData");
            if (hasData == 0)
            {
                response.HasData = true;
                packet.ReadInt32("Realm Id");
                packet.ReadInt32("Int1C");
                response.Level = packet.ReadByte("Level");
                response.Race = (uint)packet.ReadByteE<Race>("Race");
                response.Gender = (uint)packet.ReadByteE<Gender>("Gender");
                response.Class = (uint)packet.ReadByteE<Class>("Class");
            }

            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 6);

            if (hasData == 0)
            {
                guid4[1] = packet.ReadBit();
                guid5[2] = packet.ReadBit();
                guid5[5] = packet.ReadBit();
                guid5[0] = packet.ReadBit();
                guid5[7] = packet.ReadBit();
                guid4[5] = packet.ReadBit();
                guid5[3] = packet.ReadBit();
                guid4[4] = packet.ReadBit();
                bit18 = packet.ReadBit();
                guid5[6] = packet.ReadBit();
                nameLen = (int)packet.ReadBits(6);
                guid4[2] = packet.ReadBit();
                guid4[6] = packet.ReadBit();
                guid4[0] = packet.ReadBit();
                guid5[1] = packet.ReadBit();
                guid5[4] = packet.ReadBit();

                var count = new int[5];
                for (var i = 0; i < 5; ++i)
                    count[i] = (int)packet.ReadBits(7);

                guid4[7] = packet.ReadBit();
                guid4[3] = packet.ReadBit();

                for (var i = 0; i < 5; ++i)
                    packet.ReadWoWString("Name Declined", count[i], i);

                packet.ReadXORByte(guid5, 4);
                packet.ReadXORByte(guid5, 5);
                packet.ReadXORByte(guid5, 7);
                packet.ReadXORByte(guid5, 0);
                packet.ReadXORByte(guid4, 7);
                packet.ReadXORByte(guid4, 1);
                packet.ReadXORByte(guid4, 0);
                packet.ReadXORByte(guid4, 4);
                packet.ReadXORByte(guid5, 1);
                packet.ReadXORByte(guid4, 2);
                packet.ReadXORByte(guid4, 5);
                packet.ReadXORByte(guid5, 6);
                packet.ReadXORByte(guid5, 2);
                packet.ReadXORByte(guid5, 3);

                response.PlayerName = packet.ReadWoWString("Name", nameLen);

                packet.ReadXORByte(guid4, 3);
                packet.ReadXORByte(guid4, 6);

                packet.WriteGuid("Guid4", guid4);
                packet.WriteGuid("Guid5", guid5);
            }

            response.PlayerGuid = packet.WriteGuid("Guid1", guid1);
        }

        [Parser(Opcode.CMSG_QUERY_REALM_NAME)]
        public static void HandleRealmQuery(Packet packet)
        {
            packet.ReadInt32("Realm Id");
        }

        [Parser(Opcode.SMSG_REALM_QUERY_RESPONSE)]
        public static void HandleRealmQueryResponse(Packet packet)
        {
            packet.ReadInt32("Realm Id");
            packet.ReadByte("Unk byte");

            var bits22 = packet.ReadBits(8);
            packet.ReadBit();
            var bits278 = packet.ReadBits(8);

            packet.ReadWoWString("Realmname", bits22);
            packet.ReadWoWString("Realmname (without white char)", bits278);
        }

        [Parser(Opcode.CMSG_QUERY_QUEST_INFO)]
        public static void HandleQuestQuery(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32<QuestId>("Quest ID");
            packet.StartBitStream(guid, 2, 3, 5, 1, 7, 0, 6, 4);
            packet.ParseBitStream(guid, 4, 2, 1, 3, 5, 7, 0, 6);

            packet.WriteGuid("Guid", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE)]
        public static void HandleQuestQueryResponse(Packet packet)
        {
            var id = packet.ReadUInt32("Entry");
            var hasData = packet.ReadBit("hasData");
            if (!hasData)
                return; // nothing to do

            var bits1658 = (int)packet.ReadBits(9);
            var bits30 = (int)packet.ReadBits(9);
            var bits1793 = (int)packet.ReadBits(10);
            var bits2369 = (int)packet.ReadBits(8);
            var bits158 = (int)packet.ReadBits(12);
            var bits2049 = (int)packet.ReadBits(8);
            var bits908 = (int)packet.ReadBits(12);
            var bits2113 = (int)packet.ReadBits(10);
            var bits2433 = (int)packet.ReadBits(11);
            var count = (int)packet.ReadBits("Requirement Count", 19);

            var bits2949 = new int[count];
            var counter = new int[count];
            for (var i = 0; i < count; ++i)
            {
                counter[i] = (int)packet.ReadBits(22);
                bits2949[i] = (int)packet.ReadBits(8);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("byte2949+0", i);
                packet.ReadWoWString("string2949+20", bits2949[i], i);
                packet.ReadByte("byte2949+4", i);
                packet.ReadByte("byte2949+5", i);
                packet.ReadInt32("int2949+12", i);

                for (var j = 0; j < counter[i]; ++j)
                    packet.ReadInt32("Unk UInt32", i, j);

                packet.ReadInt32("int2949+16", i);
                packet.ReadInt32("int2949+8", i);
            }

            packet.ReadInt32("int2959");
            packet.ReadInt32("int2964");
            packet.ReadInt32("int19");

            packet.ReadSingle("float27");

            packet.ReadInt32("int14");
            packet.ReadInt32("int2970");

            packet.ReadSingle("float22");

            packet.ReadInt32("int2955");
            packet.ReadInt32("int12");
            packet.ReadInt32("int2981");
            packet.ReadInt32("int2957");

            packet.ReadWoWString("string2049", bits2049);

            packet.ReadInt32("int2977");
            packet.ReadInt32("int15");
            packet.ReadInt32("int2966");
            packet.ReadInt32("int2960");
            packet.ReadInt32("int8");
            packet.ReadInt32("int1789");

            packet.ReadWoWString("string2433", bits2433);
            packet.ReadWoWString("string1658", bits1658);

            packet.ReadInt32("int2946");
            packet.ReadInt32("int2945");
            packet.ReadInt32("int17");
            packet.ReadInt32("int2968");
            packet.ReadInt32("int2947");

            packet.ReadSingle("float28");

            packet.ReadInt32("int18");
            packet.ReadInt32("int29");

            packet.ReadWoWString("string1793", bits1793);

            for (var i = 0; i < 4; ++i)
            {
                packet.ReadInt32("int3001+16");
                packet.ReadInt32("int3001+0");
            }

            packet.ReadWoWString("string158", bits158);

            packet.ReadInt32("int2963");
            packet.ReadInt32("int2965");
            packet.ReadInt32("int20");

            for (var i = 0; i < 5; ++i)
            {
                packet.ReadInt32("int2986+20");
                packet.ReadInt32("int2986+0");
                packet.ReadInt32("int2986+40");
            }

            packet.ReadWoWString("string2369", bits2369);

            packet.ReadInt32("int2974");
            packet.ReadInt32("int1791");
            packet.ReadInt32("int1787");
            packet.ReadInt32("int2952");
            packet.ReadInt32("int11");
            packet.ReadInt32("int21");
            packet.ReadInt32("int2979");
            packet.ReadInt32("int16");
            packet.ReadInt32("int2962");

            packet.ReadWoWString("string908", bits908);

            packet.ReadInt32("int1792");
            packet.ReadInt32("int6");
            packet.ReadInt32("int2975");
            packet.ReadInt32("int2984");
            packet.ReadInt32("int2973");
            packet.ReadInt32("int25");
            packet.ReadInt32("int10");
            packet.ReadInt32("int2961");
            packet.ReadInt32("int1788");
            packet.ReadInt32("int9");
            packet.ReadInt32("int1786");
            packet.ReadInt32("int2980");
            packet.ReadInt32("int23");
            packet.ReadInt32("int2976");
            packet.ReadInt32("int2956");
            packet.ReadInt32("int2972");
            packet.ReadInt32("int13");
            packet.ReadInt32("int26");

            packet.ReadWoWString("string30", bits30);

            packet.ReadInt32("int2954");
            packet.ReadInt32("int2982");
            packet.ReadInt32("int2967");
            packet.ReadInt32("int2985");

            packet.ReadWoWString("string2113", bits2113);

            packet.ReadInt32("int2983");
            packet.ReadInt32("int2953");
            packet.ReadInt32("int2958");
            packet.ReadInt32("int2969");
            packet.ReadInt32("int24");
            packet.ReadInt32("int1790");
            packet.ReadInt32("int2971");
            packet.ReadInt32("int7");
            packet.ReadInt32("int2978");
        }

        [Parser(Opcode.CMSG_QUERY_PAGE_TEXT)]
        public static void HandlePageTextQuery(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("Entry");

            packet.StartBitStream(guid, 1, 5, 2, 3, 6, 4, 0, 7);
            packet.ParseBitStream(guid, 6, 4, 0, 3, 7, 5, 2, 1);

            packet.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_PAGE_TEXT_RESPONSE)]
        public static void HandlePageTextResponse(Packet packet)
        {
            uint entry = packet.ReadUInt32("Entry");

            Bit hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            PageText pageText = new PageText();
            pageText.ID = entry;

            uint textLen = packet.ReadBits(12);

            packet.ResetBitReader();
            packet.ReadUInt32("Entry");
            pageText.Text = packet.ReadWoWString("Page Text", textLen);

            pageText.NextPageID = packet.ReadUInt32("Next Page");

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
            Storage.PageTexts.Add(pageText, packet.TimeSpan);
        }
    }
}

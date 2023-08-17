using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
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
            var entry = packet.ReadInt32("Entry");
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
                packet.StartBitStream(guids[i], 3, 4, 7, 2, 5, 1, 6, 0);
            }

            packet.ResetBitReader();
            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORBytes(guids[i], 6, 1, 2);
                packet.ReadInt32("Entry", i);
                packet.ReadXORBytes(guids[i], 4, 5, 7, 0, 3);
                packet.WriteGuid("Guid", guids[i], i);
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_CREATURE_RESPONSE)]
        public static void HandleCreatureQueryResponse(Packet packet)
        {
            PacketQueryCreatureResponse response = packet.Holder.QueryCreatureResponse = new PacketQueryCreatureResponse();
            var entry = packet.ReadEntry("Entry");
            response.Entry = (uint)entry.Key;
            Bit hasData = packet.ReadBit();
            response.HasData = hasData;
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
                stringLens[i][0] = (int)packet.ReadBits(11);
                stringLens[i][1] = (int)packet.ReadBits(11);
            }

            uint qItemCount = packet.ReadBits(22);
            uint lenS4 = packet.ReadBits(6);
            uint lenS3 = packet.ReadBits(11);
            uint lenS5 = packet.ReadBits(11);

            creature.RacialLeader = packet.ReadBit("Racial Leader");

            packet.ResetBitReader();

            creature.Type = packet.ReadInt32E<CreatureType>("Type");
            creature.KillCredits = new uint?[2];
            creature.KillCredits[1] = packet.ReadUInt32("Kill Credit 1");
            creature.ModelIDs = new uint?[4];
            creature.ModelIDs[3] = packet.ReadUInt32("Display ID 3");
            creature.ModelIDs[2] = packet.ReadUInt32("Display ID 2");

            //TODO: move to creature_questitems
            //creature.QuestItems = new uint[qItemCount];
            for (int i = 0; i < qItemCount; ++i)
                /*creature.QuestItems[i] = (uint)*/response.QuestItems.Add((uint)packet.ReadInt32<ItemId>("Quest Item", i));

            creature.RequiredExpansion = packet.ReadUInt32E<ClientType>("Expansion");

            var name = new string[8];
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

            if (lenS5 > 1)
                creature.TitleAlt = packet.ReadCString("TitleAlt");

            creature.ManaModifier = packet.ReadSingle("Modifier 2");
            creature.ModelIDs[0] = packet.ReadUInt32("Display ID 0");

            if (lenS4 > 1)
                creature.IconName = packet.ReadCString("Icon Name");

            creature.KillCredits[0] = packet.ReadUInt32("Kill Credit 0");
            creature.ModelIDs[1] = packet.ReadUInt32("Display ID 1");

            if (lenS3 > 1)
                creature.SubName = packet.ReadCString("Sub Name");

            creature.TypeFlags = packet.ReadUInt32E<CreatureTypeFlag>("Type Flags");
            creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2"); // Missing enum

            creature.HealthModifier = packet.ReadSingle("Modifier 1");
            creature.Family = packet.ReadInt32E<CreatureFamily>("Family");
            creature.Rank = packet.ReadInt32E<CreatureRank>("Rank");
            creature.MovementID = packet.ReadUInt32("Movement ID");

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

            for (int i = 0; i < 4; ++i)
                response.Models.Add(creature.ModelIDs[i] ?? 0);
            for (int i = 0; i < 2; ++i)
                response.KillCredits.Add(creature.KillCredits[i] ?? 0);
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

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

            var type = packet.ReadUInt32E<DB2Hash>("DB2 File");
            packet.ReadTime("Hotfix date");
            var entry = packet.ReadInt32("Entry");
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

        [Parser(Opcode.SMSG_REALM_QUERY_RESPONSE)]
        public static void HandleRealmQueryResponse(Packet packet)
        {
            packet.ReadByte("Unk byte");
            packet.ReadInt32("Realm Id");

            var bits278 = packet.ReadBits(8);
            packet.ReadBit();
            var bits22 = packet.ReadBits(8);

            packet.ReadWoWString("Realmname", bits22);
            packet.ReadWoWString("Realmname (without white char)", bits278);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_PAGE_TEXT_RESPONSE)]
        public static void HandlePageTextResponse(Packet packet)
        {
            Bit hasData = packet.ReadBit();
            if (!hasData)
            {
                packet.ReadUInt32("Entry");
                return; // nothing to do
            }

            PageText pageText = new PageText();

            uint textLen = packet.ReadBits(12);

            packet.ResetBitReader();
            pageText.Text = packet.ReadWoWString("Page Text", textLen);

            uint entry = packet.ReadUInt32("Entry");
            pageText.ID = entry;
            pageText.NextPageID = packet.ReadUInt32("Next Page");
            packet.ReadUInt32("Entry");

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
            Storage.PageTexts.Add(pageText, packet.TimeSpan);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var entry = packet.ReadEntry("Entry");
            if (entry.Value) // Can be masked
                return;

            int size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);
            Bit hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            NpcTextMop npcText = new NpcTextMop
            {
                ID = (uint)entry.Key
            };

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

        [Parser(Opcode.SMSG_QUERY_PLAYER_NAME_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            PacketQueryPlayerNameResponseWrapper responses = packet.Holder.QueryPlayerNameResponse = new();
            PacketQueryPlayerNameResponse response = new();
            responses.Responses.Add(response);
            var guid = new byte[8];
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.StartBitStream(guid, 5, 7, 3, 0, 4, 1, 6, 2);

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);

            var hasData = packet.ReadByte("Byte18");
            if (hasData == 0)
            {
                response.HasData = true;
                packet.ReadInt32("Int24");
                response.Race = packet.ReadByte("Race");
                response.Gender = packet.ReadByte("Gender");
                response.Level = packet.ReadByte("Level");
                response.Class = packet.ReadByte("Class");
                packet.ReadInt32("Realm Id");
            }

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);

            if (hasData == 0)
            {
                guid2[6] = packet.ReadBit();
                guid1[7] = packet.ReadBit();

                var bits38 = packet.ReadBits(6);

                packet.StartBitStream(guid2, 1, 7, 2);

                guid1[4] = packet.ReadBit();
                guid2[4] = packet.ReadBit();
                guid2[0] = packet.ReadBit();
                guid1[1] = packet.ReadBit();

                var count = new int[5];
                for (var i = 0; i < 5; ++i)
                    count[i] = (int)packet.ReadBits(7);

                guid1[3] = packet.ReadBit();
                guid2[3] = packet.ReadBit();

                packet.StartBitStream(guid1, 5, 0);
                guid2[5] = packet.ReadBit();

                packet.ReadBit(); // fake bit

                guid1[2] = packet.ReadBit();
                guid1[6] = packet.ReadBit();

                response.PlayerName = packet.ReadWoWString("Name", bits38);

                packet.ReadXORByte(guid2, 4);
                packet.ReadXORByte(guid1, 3);
                packet.ReadXORByte(guid2, 6);
                packet.ReadXORByte(guid1, 2);
                packet.ReadXORByte(guid1, 4);
                packet.ReadXORByte(guid2, 5);
                packet.ReadXORByte(guid2, 1);
                packet.ReadXORByte(guid2, 7);

                for (var i = 0; i < 5; ++i)
                    packet.ReadWoWString("Name Declined", count[i], i);

                packet.ReadXORByte(guid2, 3);
                packet.ReadXORByte(guid1, 7);
                packet.ReadXORByte(guid1, 1);
                packet.ReadXORByte(guid1, 6);
                packet.ReadXORByte(guid2, 0);
                packet.ReadXORByte(guid1, 0);
                packet.ReadXORByte(guid2, 2);
                packet.ReadXORByte(guid1, 5);

                packet.WriteGuid("Guid1", guid1);
                packet.WriteGuid("Guid2", guid2);
            }

            response.PlayerGuid = packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_NAME_QUERY)]
        public static void HandleNameQuery(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 5, 7, 0, 1);

            var hasRealmId2 = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasRealmId1 = packet.ReadBit();
            packet.StartBitStream(guid, 3, 2, 4);
            packet.ParseBitStream(guid, 0, 1, 3, 4, 6, 5, 2, 7);

            if (hasRealmId1)
                packet.ReadInt32("Realm Id 1");

            if (hasRealmId2)
                packet.ReadInt32("Realm Id 2");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_QUERY_NPC_TEXT)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            var entry = packet.ReadInt32("Entry");

            var guid = new byte[8];
            packet.StartBitStream(guid, 7, 3, 1, 5, 6, 4, 0, 2);
            packet.ParseBitStream(guid, 1, 5, 2, 7, 3, 6, 4, 0);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_QUERY_PAGE_TEXT)]
        public static void HandlePageTextQuery(Packet packet)
        {
            var guid = new byte[8];
            packet.ReadInt32("Entry");

            packet.StartBitStream(guid, 0, 7, 5, 2, 1, 3, 4, 6);
            packet.ParseBitStream(guid, 7, 4, 6, 5, 2, 3, 0, 1);

            packet.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE)]
        public static void HandleQuestQueryResponse(Packet packet)
        {
            var hasData = packet.ReadBit();
            if (!hasData)
            {
                packet.ReadUInt32("Entry");
                return; // nothing to do
            }

            var quest = new QuestTemplate();

            var bits907 = packet.ReadBits(12);
            var count = packet.ReadBits(19);

            var len2949_20 = new uint[count];
            var counter = new uint[count];

            for (var i = 0; i < count; ++i)
            {
                len2949_20[i] = packet.ReadBits(8);
                counter[i] = packet.ReadBits(22);
            }

            var bits2432 = packet.ReadBits(11);
            var bits2048 = packet.ReadBits(8);
            var bits2112 = packet.ReadBits(10);
            var bits157 = packet.ReadBits(12);
            var bits1657 = packet.ReadBits(9);
            var bits2368 = packet.ReadBits(8);
            var bits1792 = packet.ReadBits(10);
            var bits29 = packet.ReadBits(9);

            packet.ReadInt32("Int2E34");
            packet.ReadInt32("Int4C");
            packet.ReadSingle("Float54");

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Int2E10", i);
                packet.ReadInt32("IntED", i);
                packet.ReadInt32("IntED", i);
                packet.ReadInt32("IntEA", i);
                packet.ReadWoWString("StringEA", len2949_20[i], i);
                packet.ReadByte("ByteED", i);
                packet.ReadByte("ByteEA", i);

                for (var j = 0; j < counter[i]; ++j)
                    packet.ReadInt32("Int118", i, j);
            }

            packet.ReadInt32("Int58");
            packet.ReadInt32("Int2E54");
            packet.ReadSingle("Float6C");
            packet.ReadWoWString("String2500", bits2368);
            packet.ReadInt32("Int34");
            packet.ReadWoWString("String19E4", bits1657);
            packet.ReadInt32("Int2E74");

            for (var i = 0; i < 4; ++i)
            {
                packet.ReadInt32("int3001+16", i);
                packet.ReadInt32("int3001+0", i);
            }

            packet.ReadInt32("Int1C");
            packet.ReadSingle("Float68");
            packet.ReadInt32("Int2E28");

            for (var i = 0; i < 5; ++i)
            {
                packet.ReadInt32("int2986+40", i);
                packet.ReadInt32("int2986+0", i);
                packet.ReadInt32("int2986+20", i);
            }

            packet.ReadInt32("Int1BE8");
            packet.ReadInt32("Int2E7C");
            packet.ReadInt32("Int1BF8");
            packet.ReadInt32("Int1BFC");
            packet.ReadInt32("Int2E90");
            packet.ReadInt32("Int2E48");
            packet.ReadWoWString("String74", bits29);
            packet.ReadInt32("Int2E2C");
            packet.ReadInt32("Int2E50");
            packet.ReadInt32("Int2E64");
            packet.ReadInt32("Int1BEC");
            packet.ReadInt32("Int60");
            packet.ReadInt32("Int2E88");
            packet.ReadInt32("Int2E94");
            packet.ReadInt32("Int2E6C");
            packet.ReadInt32("Int14");
            packet.ReadInt32("Int2E20");
            packet.ReadInt32("Int2E30");
            packet.ReadInt32("Int2E24");
            packet.ReadInt32("Int1BF0");
            packet.ReadInt32("Int2E4C");
            packet.ReadInt32("Int2E68");
            packet.ReadInt32("Int20");
            packet.ReadInt32("Int1BF4");
            packet.ReadWoWString("String2100", bits2112);
            packet.ReadInt32("Int2E08");
            packet.ReadInt32("Int38");
            packet.ReadInt32("Int5C");
            packet.ReadWoWString("String2600", bits2432);
            packet.ReadInt32("Int24");
            packet.ReadInt32("Int2E58");
            packet.ReadInt32("Int30");
            packet.ReadInt32("Int64");
            packet.ReadInt32("Int44");
            packet.ReadInt32("Int2E00");
            packet.ReadInt32("Int2E44");
            packet.ReadInt32("Int2EA0");
            packet.ReadInt32("Int28");
            packet.ReadInt32("Int2E1C");
            packet.ReadInt32("Int40");
            packet.ReadWoWString("StringE2C", bits907);
            packet.ReadInt32("Int2E60");
            packet.ReadWoWString("String2000", bits2048);
            packet.ReadInt32("Int2E70");
            packet.ReadInt32("Int2E5C");
            packet.ReadInt32("Int18");
            packet.ReadInt32("Int50");
            packet.ReadInt32("Int1BE4");
            packet.ReadWoWString("String1C00", bits1792);
            packet.ReadInt32("Int3C");
            packet.ReadInt32("Int2C");
            packet.ReadWoWString("String274", bits157);
            packet.ReadInt32("Int48");
            packet.ReadInt32("Int2E80");
            packet.ReadInt32("Int2E40");
            packet.ReadInt32("Int2E9C");
            packet.ReadInt32("Int2E84");
            packet.ReadInt32("Int2E38");
            packet.ReadInt32("Int2E04");
            packet.ReadInt32("Int2E98");
            packet.ReadInt32("Int2E3C");
            packet.ReadInt32("Int2E78");
            packet.ReadInt32("Int70");
            packet.ReadInt32("Int2E8C");

            var id = packet.ReadInt32("Int2F00");

            //packet.AddSniffData(StoreNameType.Quest, id.Key, "QUERY_RESPONSE");
            //Storage.QuestTemplates.Add((uint)id.Key, quest, packet.TimeSpan);
        }
    }
}

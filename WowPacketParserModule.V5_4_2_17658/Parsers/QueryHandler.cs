using System;
using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
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
            CreatureTemplate creature = new CreatureTemplate();
            Bit hasData = packet.Translator.ReadBit();
            if (!hasData)
                return; // nothing to do

            creature.RacialLeader = packet.Translator.ReadBit("Racial Leader");

            uint bits2C = packet.Translator.ReadBits(6);
            uint bits24 = packet.Translator.ReadBits(11);
            uint qItemCount = packet.Translator.ReadBits(22);

            var stringLens = new int[4][];
            for (int i = 0; i < 4; i++)
            {
                stringLens[i] = new int[2];
                stringLens[i][1] = (int)packet.Translator.ReadBits(11);
                stringLens[i][0] = (int)packet.Translator.ReadBits(11);
            }

            int bits1C = (int)packet.Translator.ReadBits(11);

            creature.ModelIDs = new uint?[4];
            creature.KillCredits = new uint?[2];

            creature.ModelIDs[1] = packet.Translator.ReadUInt32("Display ID 1");
            creature.KillCredits[1] = packet.Translator.ReadUInt32("Kill Credit 2");
            creature.Type = packet.Translator.ReadInt32E<CreatureType>("Type");

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

            creature.ManaModifier = packet.Translator.ReadSingle("Modifier 2");

            creature.TypeFlags = packet.Translator.ReadUInt32E<CreatureTypeFlag>("Type Flags");
            creature.TypeFlags2 = packet.Translator.ReadUInt32("Creature Type Flags 2"); // Missing enum

            creature.Family = packet.Translator.ReadInt32E<CreatureFamily>("Family");
            creature.KillCredits[0] = packet.Translator.ReadUInt32("Kill Credit 1");
            creature.ModelIDs[3] = packet.Translator.ReadUInt32("Display ID 3");

            //TODO: move to creature_questitems
            //creature.QuestItems = new uint[qItemCount];
            for (int i = 0; i < qItemCount; ++i)
                /*creature.QuestItems[i] = (uint)*/packet.Translator.ReadInt32<ItemId>("Quest Item", i);

            creature.HealthModifier = packet.Translator.ReadSingle("Modifier 1");


            if (bits24 > 1)
                packet.Translator.ReadCString("String1C");

            creature.MovementID = packet.Translator.ReadUInt32("Movement ID");
            creature.RequiredExpansion = packet.Translator.ReadUInt32E<ClientType>("Expansion");

            if (bits2C > 1)
                creature.IconName = packet.Translator.ReadCString("Icon Name");

            creature.ModelIDs[2] = packet.Translator.ReadUInt32("Display ID 2");
            creature.ModelIDs[0] = packet.Translator.ReadUInt32("Display ID 0");
            creature.Rank = packet.Translator.ReadInt32E<CreatureRank>("Rank");

            if (bits1C > 1)
                creature.SubName = packet.Translator.ReadCString("Sub Name");

            var entry = packet.Translator.ReadEntry("Entry");
            creature.Entry = (uint)entry.Key;

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
            uint count = packet.Translator.ReadBits(21);

            var guids = new byte[count][];
            for (int i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                packet.Translator.StartBitStream(guids[i], 3, 7, 5, 6, 2, 0, 4, 1);
            }

            packet.Translator.ResetBitReader();
            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadXORBytes(guids[i], 5, 1, 4, 6, 7, 2, 0, 3);
                packet.Translator.ReadInt32("Entry", i);
                packet.Translator.WriteGuid("Guid", guids[i], i);
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            var entry = packet.Translator.ReadInt32("Entry");
            packet.Translator.ReadTime("Hotfix date");

            var size = packet.Translator.ReadInt32("Size");
            var data = packet.Translator.ReadBytes(size);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Formatter, packet.FileName);

            var type = packet.Translator.ReadUInt32E<DB2Hash>("DB2 File");
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

            var hasRealmId2 = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid, 0, 6, 2, 1, 7);

            var hasRealmId1 = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid, 3, 5, 4);
            packet.Translator.ParseBitStream(guid, 0, 5, 2, 4, 7, 6, 1, 3);

            if (hasRealmId1)
                packet.Translator.ReadInt32("Realm Id 1");

            if (hasRealmId2)
                packet.Translator.ReadInt32("Realm Id 2");

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_QUERY_PAGE_TEXT)]
        public static void HandlePageTextQuery(Packet packet)
        {
            var guid = new byte[8];
            packet.Translator.ReadInt32("Entry");

            packet.Translator.StartBitStream(guid, 0, 7, 3, 5, 6, 2, 4, 1);
            packet.Translator.ParseBitStream(guid, 0, 5, 1, 3, 2, 4, 7, 6);

            packet.Translator.WriteGuid("GUID", guid);
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

            pageText.NextPageID = packet.Translator.ReadUInt32("Next Page");
            uint entry = packet.Translator.ReadUInt32("Entry");
            pageText.ID = entry;
            packet.Translator.ReadUInt32("Entry");

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
            Storage.PageTexts.Add(pageText, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_QUERY_PLAYER_NAME_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            var guid1 = new byte[8];
            var accountId = new byte[8];
            var guid2 = new byte[8];

            guid1[4] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid1, 1);

            var hasData = packet.Translator.ReadByte("HasData");
            if (hasData == 0)
            {
                packet.Translator.ReadByteE<Class>("Class");
                packet.Translator.ReadByteE<Gender>("Gender");
                packet.Translator.ReadInt32("Realm Id");
                packet.Translator.ReadByteE<Race>("Race");
                packet.Translator.ReadInt32("Int24");
                packet.Translator.ReadByte("Level");
            }

            packet.Translator.ReadXORByte(guid1, 5);

            if (hasData == 0)
            {
                guid2[3] = packet.Translator.ReadBit();
                accountId[7] = packet.Translator.ReadBit();

                var count = new int[5];
                for (var i = 0; i < 5; ++i)
                    count[i] = (int)packet.Translator.ReadBits(7);

                accountId[3] = packet.Translator.ReadBit();
                guid2[0] = packet.Translator.ReadBit();
                accountId[5] = packet.Translator.ReadBit();
                guid2[4] = packet.Translator.ReadBit();
                accountId[0] = packet.Translator.ReadBit();
                guid2[6] = packet.Translator.ReadBit();
                guid2[7] = packet.Translator.ReadBit();
                accountId[6] = packet.Translator.ReadBit();
                accountId[1] = packet.Translator.ReadBit();
                var bit20 = packet.Translator.ReadBit();
                guid2[1] = packet.Translator.ReadBit();
                var bits38 = (int)packet.Translator.ReadBits(6);
                guid2[2] = packet.Translator.ReadBit();
                accountId[4] = packet.Translator.ReadBit();
                guid2[5] = packet.Translator.ReadBit();
                accountId[2] = packet.Translator.ReadBit();

                packet.Translator.ReadXORByte(guid2, 4);
                packet.Translator.ReadXORByte(guid2, 1);
                packet.Translator.ReadXORByte(guid2, 5);

                for (var i = 0; i < 5; ++i)
                    packet.Translator.ReadWoWString("Name Declined", count[i], i);

                packet.Translator.ReadWoWString("Name", bits38);

                packet.Translator.ReadXORByte(accountId, 2);
                packet.Translator.ReadXORByte(accountId, 5);
                packet.Translator.ReadXORByte(guid2, 0);
                packet.Translator.ReadXORByte(guid2, 3);
                packet.Translator.ReadXORByte(accountId, 0);
                packet.Translator.ReadXORByte(accountId, 6);
                packet.Translator.ReadXORByte(accountId, 1);
                packet.Translator.ReadXORByte(guid2, 7);
                packet.Translator.ReadXORByte(accountId, 4);
                packet.Translator.ReadXORByte(accountId, 3);
                packet.Translator.ReadXORByte(guid2, 6);
                packet.Translator.ReadXORByte(accountId, 7);
                packet.Translator.ReadXORByte(guid2, 2);

                packet.AddValue("Account", BitConverter.ToUInt64(accountId, 0));
                packet.Translator.WriteGuid("Guid2", guid2);

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
            packet.Translator.ReadByte("Unk byte");
            packet.Translator.ReadInt32("Realm Id");

            packet.Translator.ReadBit();

            var bits22 = packet.Translator.ReadBits(8);
            var bits278 = packet.Translator.ReadBits(8);

            packet.Translator.ReadWoWString("Realmname", bits22);
            packet.Translator.ReadWoWString("Realmname (without white char)", bits278);
        }
    }
}

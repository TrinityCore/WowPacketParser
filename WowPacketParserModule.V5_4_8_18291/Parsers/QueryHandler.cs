using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
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
            var entry = packet.Translator.ReadEntry("Entry"); // +5

            CreatureTemplate creature = new CreatureTemplate
            {
                Entry = (uint)entry.Key
            };

            Bit hasData = packet.Translator.ReadBit(); //+16
            if (!hasData)
                return; // nothing to do

            creature.ModelIDs = new uint?[4];
            creature.KillCredits = new uint?[2];

            uint bits24 = packet.Translator.ReadBits(11); //+7
            uint qItemCount = packet.Translator.ReadBits(22); //+72
            int bits1C = (int)packet.Translator.ReadBits(11); //+9

            var stringLens = new int[4][];
            for (int i = 0; i < 4; i++)
            {
                stringLens[i] = new int[2];
                stringLens[i][0] = (int)packet.Translator.ReadBits(11);
                stringLens[i][1] = (int)packet.Translator.ReadBits(11);
            }

            creature.RacialLeader = packet.Translator.ReadBit("Racial Leader"); //+68
            uint bits2C = packet.Translator.ReadBits(6); //+136

            if (bits1C > 1)
                packet.Translator.ReadCString("String1C");

            creature.KillCredits[0] = packet.Translator.ReadUInt32(); //+27
            creature.ModelIDs[3] = packet.Translator.ReadUInt32(); //+32
            creature.ModelIDs[2] = packet.Translator.ReadUInt32(); //+31
            creature.RequiredExpansion = packet.Translator.ReadUInt32E<ClientType>("Expansion"); //+24
            creature.Type = packet.Translator.ReadInt32E<CreatureType>("Type"); //+12
            creature.HealthModifier = packet.Translator.ReadSingle("Modifier 1"); //+15

            creature.TypeFlags = packet.Translator.ReadUInt32E<CreatureTypeFlag>("Type Flags");
            creature.TypeFlags2 = packet.Translator.ReadUInt32("Creature Type Flags 2"); // Missing enum

            creature.Rank = packet.Translator.ReadInt32E<CreatureRank>("Rank"); //+14
            creature.MovementID = packet.Translator.ReadUInt32("Movement ID"); //+23

            var name = new string[4];
            var femaleName = new string[4];
            for (int i = 0; i < 4; ++i)
            {
                if (stringLens[i][1] > 1)
                    femaleName[i] = packet.Translator.ReadCString("Female Name", i);
                if (stringLens[i][0] > 1)
                    name[i] = packet.Translator.ReadCString("Name", i);
            }
            creature.Name = name[0];
            creature.FemaleName = femaleName[0];

            if (bits24 > 1)
                creature.SubName = packet.Translator.ReadCString("Sub Name");

            creature.ModelIDs[0] = packet.Translator.ReadUInt32(); //+29
            creature.ModelIDs[1] = packet.Translator.ReadUInt32(); //+30

            if (bits2C > 1)
                creature.IconName = packet.Translator.ReadCString("Icon Name"); //+100

            //creature.QuestItems = new uint[qItemCount];
            for (int i = 0; i < qItemCount; ++i)
                /*creature.QuestItems[i] = (uint)*/packet.Translator.ReadInt32<ItemId>("Quest Item", i); //+72

            creature.KillCredits[1] = packet.Translator.ReadUInt32(); //+28
            creature.ManaModifier = packet.Translator.ReadSingle("Modifier 2"); //+16
            creature.Family = packet.Translator.ReadInt32E<CreatureFamily>("Family"); //+13

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
                packet.Translator.StartBitStream(guids[i], 6, 3, 0, 1, 4, 5, 7, 2);
            }

            packet.Translator.ResetBitReader();

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadXORByte(guids[i], 1);

                packet.Translator.ReadInt32("Entry", i);

                packet.Translator.ReadXORByte(guids[i], 0);
                packet.Translator.ReadXORByte(guids[i], 5);
                packet.Translator.ReadXORByte(guids[i], 6);
                packet.Translator.ReadXORByte(guids[i], 4);
                packet.Translator.ReadXORByte(guids[i], 7);
                packet.Translator.ReadXORByte(guids[i], 2);
                packet.Translator.ReadXORByte(guids[i], 3);

                packet.Translator.WriteGuid("Guid", guids[i], i);
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            var entry = packet.Translator.ReadInt32("Entry");
            packet.Translator.ReadTime("Hotfix date");
            var type = packet.Translator.ReadUInt32E<DB2Hash>("DB2 File");

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

        [Parser(Opcode.SMSG_QUERY_PLAYER_NAME_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            var guid4 = new byte[8];
            var guid5 = new byte[8];
            var guid1 = new byte[8];

            var bit18 = false;

            guid1[3] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 2);

            var hasData = packet.Translator.ReadByte("HasData");
            if (hasData == 0)
            {
                packet.Translator.ReadInt32("Realm Id");
                packet.Translator.ReadInt32("AccountId");
                packet.Translator.ReadByteE<Class>("Class");
                packet.Translator.ReadByteE<Race>("Race");
                packet.Translator.ReadByte("Level");
                packet.Translator.ReadByteE<Gender>("Gender");
            }

            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 3);

            if (hasData == 0)
            {
                guid4[2] = packet.Translator.ReadBit();
                guid4[7] = packet.Translator.ReadBit();
                guid5[7] = packet.Translator.ReadBit();
                guid5[2] = packet.Translator.ReadBit();
                guid5[0] = packet.Translator.ReadBit();
                bit18 = packet.Translator.ReadBit();

                guid4[4] = packet.Translator.ReadBit();
                guid5[5] = packet.Translator.ReadBit();
                guid4[1] = packet.Translator.ReadBit();
                guid4[3] = packet.Translator.ReadBit();
                guid4[0] = packet.Translator.ReadBit();

                var count = new int[5];
                for (var i = 0; i < 5; ++i)
                    count[i] = (int)packet.Translator.ReadBits(7);

                guid5[6] = packet.Translator.ReadBit();
                guid5[3] = packet.Translator.ReadBit();
                guid4[5] = packet.Translator.ReadBit();
                guid5[1] = packet.Translator.ReadBit();
                guid5[4] = packet.Translator.ReadBit();

                var nameLen = (int)packet.Translator.ReadBits(6);

                guid4[6] = packet.Translator.ReadBit();

                packet.Translator.ReadXORByte(guid5, 6);
                packet.Translator.ReadXORByte(guid5, 0);
                packet.Translator.ReadWoWString("Name", nameLen);
                packet.Translator.ReadXORByte(guid4, 5);
                packet.Translator.ReadXORByte(guid4, 2);
                packet.Translator.ReadXORByte(guid5, 3);
                packet.Translator.ReadXORByte(guid4, 4);
                packet.Translator.ReadXORByte(guid4, 3);
                packet.Translator.ReadXORByte(guid5, 4);
                packet.Translator.ReadXORByte(guid5, 2);
                packet.Translator.ReadXORByte(guid4, 7);

                for (var i = 0; i < 5; ++i)
                    packet.Translator.ReadWoWString("Name Declined", count[i], i);

                packet.Translator.ReadXORByte(guid4, 6);
                packet.Translator.ReadXORByte(guid5, 7);
                packet.Translator.ReadXORByte(guid5, 1);
                packet.Translator.ReadXORByte(guid4, 1);
                packet.Translator.ReadXORByte(guid5, 5);
                packet.Translator.ReadXORByte(guid4, 0);

                packet.Translator.WriteGuid("Guid4", guid4);
                packet.Translator.WriteGuid("Guid5", guid5);
            }

            packet.Translator.WriteGuid("Guid1", guid1);
        }

        [Parser(Opcode.CMSG_NAME_QUERY)]
        public static void HandleNameQuery(Packet packet)
        {
            var guid = new byte[8];

            guid[4] = packet.Translator.ReadBit();
            var hasRealmId2 = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            var hasRealmId1 = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            packet.Translator.ParseBitStream(guid, 7, 5, 1, 2, 6, 3, 0, 4);

            if (hasRealmId2)
                packet.Translator.ReadInt32("Realm Id 2");

            if (hasRealmId1)
                packet.Translator.ReadInt32("Realm Id 1");

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_QUERY_PAGE_TEXT)]
        public static void HandlePageTextQuery(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadInt32("Entry");

            packet.Translator.StartBitStream(guid, 2, 1, 3, 7, 6, 4, 0, 5);
            packet.Translator.ParseBitStream(guid, 0, 6, 3, 5, 1, 7, 4, 2);

            packet.Translator.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_PAGE_TEXT_RESPONSE)]
        public static void HandlePageTextResponse(Packet packet)
        {
            Bit hasData = packet.Translator.ReadBit();
            if (!hasData)
                return; // nothing to do

            PageText pageText = new PageText();

            uint textLen = packet.Translator.ReadBits(12);

            pageText.NextPageID = packet.Translator.ReadUInt32("Next Page");
            packet.Translator.ReadUInt32("Entry");

            pageText.Text = packet.Translator.ReadWoWString("Page Text", textLen);
            uint entry = packet.Translator.ReadUInt32("Entry");
            pageText.ID = entry;

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
            Storage.PageTexts.Add(pageText, packet.TimeSpan);
        }
    }
}

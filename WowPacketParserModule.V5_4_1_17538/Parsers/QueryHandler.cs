using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_1_17538.Parsers
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
            Bit hasData = packet.Translator.ReadBit();
            if (!hasData)
                return; // nothing to do

            CreatureTemplate creature = new CreatureTemplate
            {
                Entry = (uint)entry.Key
            };

            packet.Translator.ReadBits(11);
            uint qItemCount = packet.Translator.ReadBits(22);
            int lenS4 = (int)packet.Translator.ReadBits(6);
            creature.RacialLeader = packet.Translator.ReadBit("Racial Leader");

            var stringLens = new int[4][];
            for (int i = 0; i < 4; i++)
            {
                stringLens[i] = new int[2];
                stringLens[i][0] = (int)packet.Translator.ReadBits(11);
                stringLens[i][1] = (int)packet.Translator.ReadBits(11);
            }

            int lenS5 = (int)packet.Translator.ReadBits(11);

            packet.Translator.ResetBitReader();

            creature.Family = packet.Translator.ReadInt32E<CreatureFamily>("Family");
            creature.RequiredExpansion = packet.Translator.ReadUInt32E<ClientType>("Expansion");
            creature.Type = packet.Translator.ReadInt32E<CreatureType>("Type");

            if (lenS5 > 1)
                creature.SubName = packet.Translator.ReadCString("Sub Name");

            creature.ModelIDs = new uint?[4];
            creature.ModelIDs[0] = packet.Translator.ReadUInt32("Display ID 0");
            creature.ModelIDs[3] = packet.Translator.ReadUInt32("Display ID 3");

            //TODO: move to creature_questitems
            //creature.QuestItems = new uint[qItemCount];
            for (int i = 0; i < qItemCount; ++i)
                /*creature.QuestItems[i] = (uint)*/packet.Translator.ReadInt32<ItemId>("Quest Item", i);

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

            if (lenS4 > 1)
                creature.IconName = packet.Translator.ReadCString("Icon Name");

            creature.TypeFlags = packet.Translator.ReadUInt32E<CreatureTypeFlag>("Type Flags");
            creature.TypeFlags2 = packet.Translator.ReadUInt32("Creature Type Flags 2"); // Missing enum

            creature.HealthModifier = packet.Translator.ReadSingle("Modifier 1");

            creature.Rank = packet.Translator.ReadInt32E<CreatureRank>("Rank");

            creature.KillCredits = new uint?[2];
            for (int i = 0; i < 2; ++i)
                creature.KillCredits[i] = packet.Translator.ReadUInt32("Kill Credit", i);

            creature.ManaModifier = packet.Translator.ReadSingle("Modifier 2");

            creature.MovementID = packet.Translator.ReadUInt32("Movement ID");

            creature.ModelIDs[1] = packet.Translator.ReadUInt32("Display ID 1");
            creature.ModelIDs[2] = packet.Translator.ReadUInt32("Display ID 2");

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

        [Parser(Opcode.CMSG_QUERY_PLAYER_NAME)]
        public static void HandlePlayerQueryName(Packet packet)
        {
            var guid = new byte[8];

            guid[1] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            var bit20 = packet.Translator.ReadBit("bit20");
            guid[0] = packet.Translator.ReadBit();
            var bit28 = packet.Translator.ReadBit("bit28");
            guid[4] = packet.Translator.ReadBit();

            packet.Translator.ParseBitStream(guid, 4, 6, 7, 1, 2, 5, 0, 3);

            if (bit20)
                packet.Translator.ReadUInt32("unk20");

            if (bit28)
                packet.Translator.ReadUInt32("unk28");
            packet.Translator.WriteGuid("Guid", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            packet.Translator.ReadTime("Hotfix date");
            var type = packet.Translator.ReadUInt32E<DB2Hash>("DB2 File");

            var size = packet.Translator.ReadInt32("Size");
            var data = packet.Translator.ReadBytes(size);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Formatter, packet.FileName);

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

        [Parser(Opcode.CMSG_DB_QUERY_BULK)]
        public static void HandleDBQueryBulk(Packet packet)
        {
            packet.Translator.ReadInt32E<DB2Hash>("DB2 File");
            var count = packet.Translator.ReadBits(21);

            var guids = new byte[count][];
            for (var i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                packet.Translator.StartBitStream(guids[i], 1, 7, 2, 5, 0, 6, 3, 4);
            }

            packet.Translator.ResetBitReader();
            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadXORBytes(guids[i], 4, 7, 6, 0, 2, 3);
                packet.Translator.ReadInt32("Entry", i);
                packet.Translator.ReadXORBytes(guids[i], 5, 1);
                packet.Translator.WriteGuid("Guid", guids[i], i);
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
            uint entry = packet.Translator.ReadUInt32("Entry");

            Bit hasData = packet.Translator.ReadBit();
            if (!hasData)
                return; // nothing to do

            PageText pageText = new PageText();
            pageText.ID = entry;

            uint textLen = packet.Translator.ReadBits(12);

            packet.Translator.ResetBitReader();

            pageText.Text = packet.Translator.ReadWoWString("Page Text", textLen);

            pageText.NextPageID = packet.Translator.ReadUInt32("Next Page");
            packet.Translator.ReadUInt32("Entry");

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
            Storage.PageTexts.Add(pageText, packet.TimeSpan);
        }
    }
}

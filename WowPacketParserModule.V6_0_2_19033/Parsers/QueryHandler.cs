using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class QueryHandler
    {
        [Parser(Opcode.CMSG_CREATURE_QUERY)]
        public static void HandleCreatureQuery(Packet packet)
        {
            packet.ReadInt32("Entry");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_CREATURE_QUERY_RESPONSE)]
        public static void HandleCreatureQueryResponse(Packet packet)
        {
            var entry = packet.ReadEntry("Entry");

            var creature = new UnitTemplate();
            var hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            packet.ResetBitReader();
            var bits4 = packet.ReadBits(11);
            var bits16 = packet.ReadBits(11);
            var bits28 = packet.ReadBits(6);
            creature.RacialLeader = packet.ReadBit("Leader");

            var stringLens = new int[4][];
            for (var i = 0; i < 4; i++)
            {
                stringLens[i] = new int[2];
                stringLens[i][0] = (int)packet.ReadBits(11);
                stringLens[i][1] = (int)packet.ReadBits(11);
            }

            var name = new string[4];
            var femaleName = new string[4];
            for (var i = 0; i < 4; ++i)
            {
                if (stringLens[i][0] > 1)
                    creature.Name = packet.ReadCString("Name");
                if (stringLens[i][1] > 1)
                    creature.femaleName = packet.ReadCString("NameAlt");
            }

            //for (var i = 0; i < 2; ++i)
            //{
                creature.TypeFlags = packet.ReadEnum<CreatureTypeFlag>("Type Flags", TypeCode.UInt32);
                creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2");
            //}

            creature.Type = packet.ReadEnum<CreatureType>("CreatureType", TypeCode.Int32);
            creature.Family = packet.ReadEnum<CreatureFamily>("CreatureFamily", TypeCode.Int32);
            creature.Rank = packet.ReadEnum<CreatureRank>("Classification", TypeCode.Int32);

            creature.KillCredits = new uint[2];
            for (var i = 0; i < 2; ++i)
                creature.KillCredits[i] = packet.ReadUInt32("ProxyCreatureID", i);

            creature.DisplayIds = new uint[4];
            for (var i = 0; i < 4; ++i)
                creature.DisplayIds[i] = packet.ReadUInt32("CreatureDisplayID", i);

            creature.Modifier1 = packet.ReadSingle("HpMulti");
            creature.Modifier2 = packet.ReadSingle("EnergyMulti");

            creature.QuestItems = new uint[6];
            var QuestItems = packet.ReadInt32("QuestItems");
            creature.MovementId = packet.ReadUInt32("CreatureMovementInfoID");
            creature.Expansion = packet.ReadEnum<ClientType>("RequiredExpansion", TypeCode.UInt32);
            packet.ReadInt32("FlagQuest");

            if (bits4 > 1)
                creature.SubName = packet.ReadCString("Title");

            if (bits16 > 1)
                packet.ReadCString("TitleAlt");

            if (bits28 > 1)
                creature.IconName = packet.ReadCString("CursorName");

            for (var i = 0; i < QuestItems; ++i)
                creature.QuestItems[i] = (uint)packet.ReadEntry<Int32>(StoreNameType.Item, "Quest Item", i);

            packet.AddSniffData(StoreNameType.Unit, entry.Key, "QUERY_RESPONSE");

            Storage.UnitTemplates.Add((uint)entry.Key, creature, packet.TimeSpan);

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.Unit,
                Name = creature.Name,
            };
            Storage.ObjectNames.Add((uint)entry.Key, objectName, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_DB_QUERY_BULK)]
        public static void HandleDBQueryBulk(Packet packet)
        {
            packet.ReadEnum<DB2Hash>("DB2 File", TypeCode.Int32);
            var count = packet.ReadUInt32("Count");

            for (var i = 0; i < count; ++i)
            {
                packet.ReadPackedGuid128("Guid");
                packet.ReadInt32("Entry", i);
            }
        }

        [Parser(Opcode.CMSG_NAME_QUERY)]
        public static void HandleNameQuery(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");

            var bit4 = packet.ReadBit();
            var bit12 = packet.ReadBit();

            if (bit4)
                packet.ReadInt32("VirtualRealmAddress");

            if (bit12)
                packet.ReadInt32("NativeRealmAddress");
        }


        [Parser(Opcode.SMSG_NAME_QUERY_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            var hasData = packet.ReadByte("HasData");

            packet.ReadPackedGuid128("Player Guid");

            if (hasData == 0)
            {
                var bits15 = (int)packet.ReadBits(7);

                var count = new int[5];
                for (var i = 0; i < 5; ++i)
                    count[i] = (int)packet.ReadBits(7);

                for (var i = 0; i < 5; ++i)
                    packet.ReadWoWString("Name Declined", count[i], i);

                packet.ReadPackedGuid128("BnetAccountID");
                packet.ReadPackedGuid128("AccountID");
                packet.ReadPackedGuid128("Player Guid");

                packet.ReadUInt32("VirtualRealmAddress");

                packet.ReadEnum<Race>("Race", TypeCode.Byte);
                packet.ReadEnum<Gender>("Gender", TypeCode.Byte);
                packet.ReadEnum<Class>("Class", TypeCode.Byte);
                packet.ReadByte("Level");

                packet.ReadWoWString("Name", bits15);
            }
        }

        [Parser(Opcode.CMSG_PAGE_TEXT_QUERY)]
        public static void HandlePageTextQuery(Packet packet)
        {
            packet.ReadInt32("Entry");
            packet.ReadPackedGuid128("Guid");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_PAGE_TEXT_QUERY_RESPONSE)]
        public static void HandlePageTextResponse(Packet packet)
        {
            var pageText = new PageText();

            packet.ReadUInt32("Entry");
            var hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            var entry = packet.ReadUInt32("Entry");
            pageText.NextPageId = packet.ReadUInt32("Next Page");

            packet.ResetBitReader();
            var textLen = packet.ReadBits(12);
            pageText.Text = packet.ReadWoWString("Page Text", textLen);

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
            Storage.PageTexts.Add(entry, pageText, packet.TimeSpan);
        }
    }
}

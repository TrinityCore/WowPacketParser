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

using System;
using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class HotfixHandler
    {
        [Parser(Opcode.CMSG_DB_QUERY_BULK)]
        public static void HandleDbQueryBulk(Packet packet)
        {
            packet.Translator.ReadInt32E<DB2Hash>("DB2 File");

            var count = ClientVersion.AddedInVersion(ClientVersionBuild.V6_0_3_19103) ? packet.Translator.ReadBits("Count", 13) : packet.Translator.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadPackedGuid128("Guid", i);
                packet.Translator.ReadInt32("Entry", i);
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            var type = packet.Translator.ReadUInt32E<DB2Hash>("TableHash");
            var entry = packet.Translator.ReadInt32("RecordID");
            var allow = true;
            var timeStamp = packet.Translator.ReadUInt32();
            packet.AddValue("Timestamp", Utilities.GetDateTimeFromUnixTime(timeStamp));
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                allow = packet.Translator.ReadBit("Allow");

            var size = packet.Translator.ReadInt32("Size");
            var data = packet.Translator.ReadBytes(size);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Formatter, packet.FileName);
            if (entry < 0 || !allow)
            {
                packet.Formatter.AppendItem("Row {0} has been removed.", -entry);
                HotfixStoreMgr.RemoveRecord(type, entry);
                Storage.AddHotfixData(entry, type, true, timeStamp);
            }
            else
            {
                packet.AddSniffData(StoreNameType.None, entry, type.ToString());
                HotfixStoreMgr.AddRecord(type, entry, db2File);
                Storage.AddHotfixData(entry, type, false, timeStamp);
                db2File.ClosePacket(false);
            }
        }

        [Parser(Opcode.SMSG_HOTFIX_NOTIFY_BLOB)]
        public static void HandleHotfixNotifyBlob(Packet packet)
        {
            var count = packet.Translator.ReadUInt32("HotfixCount");

            for (var i = 0; i < count; ++i)
            {
                var tableHash = packet.Translator.ReadUInt32E<DB2Hash>("TableHash", i);
                var recordID = packet.Translator.ReadInt32("RecordID", i);
                var timeStamp = packet.Translator.ReadUInt32();
                packet.AddValue("Timestamp", Utilities.GetDateTimeFromUnixTime(timeStamp), i);
                Storage.AddHotfixData(recordID, tableHash, false, timeStamp);
            }
        }

        [Parser(Opcode.SMSG_HOTFIX_NOTIFY)]
        public static void HandleHotfixNotify(Packet packet)
        {
            var tableHash = packet.Translator.ReadUInt32E<DB2Hash>("TableHash");
            var recordID = packet.Translator.ReadInt32("RecordID");
            var timeStamp = packet.Translator.ReadUInt32();
            packet.AddValue("Timestamp", Utilities.GetDateTimeFromUnixTime(timeStamp));

            Storage.AddHotfixData(recordID, tableHash, false, timeStamp);
        }
    }
}

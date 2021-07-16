using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Google.Protobuf.WellKnownTypes;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WoWPacketParser.Proto;
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
            packet.ReadInt32E<DB2Hash>("DB2 File");

            var count = ClientVersion.AddedInVersion(ClientVersionBuild.V6_0_3_19103) ? packet.ReadBits("Count", 13) : packet.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
            {
                packet.ReadPackedGuid128("Guid", i);
                packet.ReadInt32("Entry", i);
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            var dbReply = packet.Holder.DbReply = new();
            var type = packet.ReadUInt32E<DB2Hash>("TableHash");
            dbReply.TableHash = (uint)type;
            var entry = dbReply.RecordId = packet.ReadInt32("RecordID");
            var allow = true;
            var timeStamp = packet.ReadUInt32();
            var time = packet.AddValue("Timestamp", Utilities.GetDateTimeFromUnixTime(timeStamp));
            dbReply.Time = Timestamp.FromDateTime(DateTime.SpecifyKind(time, DateTimeKind.Utc));
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                allow = packet.ReadBit("Allow");

            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);
            if (entry < 0 || !allow)
            {
                dbReply.Status = PacketDbReplyRecordStatus.RecordStatusRecordRemoved;
                packet.WriteLine("Row {0} has been removed.", -entry);
                HotfixStoreMgr.RemoveRecord(type, entry);
            }
            else
            {
                dbReply.Status = PacketDbReplyRecordStatus.RecordStatusValid;
                packet.AddSniffData(StoreNameType.None, entry, type.ToString());
                HotfixStoreMgr.AddRecord(type, entry, db2File);
                db2File.ClosePacket(false);
            }
        }

        [Parser(Opcode.SMSG_HOTFIX_NOTIFY_BLOB)]
        public static void HandleHotfixNotifyBlob(Packet packet)
        {
            var count = packet.ReadUInt32("HotfixCount");

            for (var i = 0; i < count; ++i)
            {
                packet.ReadUInt32E<DB2Hash>("TableHash", i);
                packet.ReadInt32("RecordID", i);
                packet.ReadUInt32("Timestamp", i);
            }
        }

        [Parser(Opcode.SMSG_HOTFIX_NOTIFY)]
        public static void HandleHotfixNotify(Packet packet)
        {
            var tableHash = packet.ReadUInt32E<DB2Hash>("TableHash");
            var recordID = packet.ReadInt32("RecordID");
            var timeStamp = packet.ReadUInt32();
            packet.AddValue("Timestamp", Utilities.GetDateTimeFromUnixTime(timeStamp));
        }
    }
}

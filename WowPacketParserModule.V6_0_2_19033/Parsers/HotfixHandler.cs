using System;
using System.Collections.Generic;
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
            var type = packet.ReadUInt32E<DB2Hash>("TableHash");
            var entry = packet.ReadInt32("RecordID");
            var allow = true;
            var timeStamp = packet.ReadUInt32();
            packet.AddValue("Timestamp", Utilities.GetDateTimeFromUnixTime(timeStamp));
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                allow = packet.ReadBit("Allow");

            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);
            HotfixData hotfixData = new HotfixData
            {
                TableHash = type,
            };
            if (entry < 0 || !allow)
            {
                packet.WriteLine("Row {0} has been removed.", -entry);
                HotfixStoreMgr.RemoveRecord(type, entry);
                if (HotfixSettings.Instance.ShouldLog(type))
                {
                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)))
                    {
                        hotfixData.Deleted = false;
                        hotfixData.RecordID = (int)entry;
                        hotfixData.Timestamp = Storage.HotfixDataStore[new Tuple<DB2Hash, int>(type, (int)entry)].Item1.Timestamp;
                        Storage.HotfixDatas.Add(hotfixData);
                    }
                }
            }
            else
            {
                packet.AddSniffData(StoreNameType.None, entry, type.ToString());
                HotfixStoreMgr.AddRecord(type, entry, db2File);
                if (HotfixSettings.Instance.ShouldLog(type))
                {
                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, -(int)entry)))
                    {
                        hotfixData.Deleted = true;
                        hotfixData.RecordID = -(int)entry;
                        hotfixData.Timestamp = Storage.HotfixDataStore[new Tuple<DB2Hash, int>(type, -(int)entry)].Item1.Timestamp;
                        Storage.HotfixDatas.Add(hotfixData);
                    }
                }
                db2File.ClosePacket(false);
            }
        }

        [Parser(Opcode.SMSG_HOTFIX_NOTIFY_BLOB)]
        public static void HandleHotfixNotifyBlob(Packet packet)
        {
            var count = packet.ReadUInt32("HotfixCount");

            for (var i = 0; i < count; ++i)
            {
                HotfixData hotfixData = new HotfixData();

                DB2Hash tableHash = packet.ReadUInt32E<DB2Hash>("TableHash", i);
                int recordID = packet.ReadInt32("RecordID", i);
                hotfixData.Timestamp = packet.ReadUInt32("Timestamp", i);

                Storage.HotfixDataStore.Add(Tuple.Create(tableHash, recordID), hotfixData);
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

using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V2_5_1_38835.Parsers
{
    public static class HotfixHandler
    {
        public class HotfixRecord
        {
            public uint HotfixId;
            public uint UniqueId;
            public DB2Hash Type;
            public int RecordId;
            public int HotfixDataSize;
            public HotfixStatus Status;
        }

        static void ReadHotfixData(Packet packet, List<HotfixRecord> records, params object[] indexes)
        {
            for (var i = 0; i < records.Count; ++i)
            {
                var record = records[i];

                packet.AddValue("HotfixId", record.HotfixId, i, indexes, "HotfixRecord");
                packet.AddValue("UniqueId", record.UniqueId, i, indexes, "HotfixRecord");
                packet.AddValue("RecordId", record.RecordId, i, indexes, "HotfixRecord");
                packet.AddValue("TableHash", record.Type, i, indexes, "HotfixRecord");
                packet.AddValue("Status", record.Status, i, indexes, "HotfixRecord");
                packet.AddValue("Size", record.HotfixDataSize, i, indexes, "HotfixRecord");

                switch (record.Status)
                {
                    case HotfixStatus.Valid:
                        {
                            packet.AddSniffData(StoreNameType.None, record.RecordId, record.Type.ToString());

                            var data = packet.ReadBytes(record.HotfixDataSize);
                            using (var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction,
                                packet.Number, packet.Writer, packet.FileName))
                            {
                                HotfixStoreMgr.AddRecord(record.Type, record.RecordId, db2File);

                                if (HotfixStoreMgr.GetStore(record.Type) == null)
                                {
                                    db2File.WriteLine($"(RecordID: {record.RecordId} TableHash: {record.Type}) has missing structure. HotfixBlob entry generated!");
                                    db2File.AsHex();

                                    var hotfixBlob = new HotfixBlob
                                    {
                                        TableHash = record.Type,
                                        RecordID = record.RecordId,
                                        Blob = new Blob(data)
                                    };

                                    Storage.HotfixBlobs.Add(hotfixBlob);
                                }
                                else if (db2File.Position != db2File.Length)
                                    HandleHotfixOptionalData(packet, record.Type, record.RecordId, db2File);
                            }

                            break;
                        }
                    case HotfixStatus.Invalid:
                        {
                            packet.WriteLine($"Row {record.RecordId} is invalid.");
                            break;
                        }
                    case HotfixStatus.RecordRemoved:
                        {
                            packet.WriteLine($"Row {record.RecordId} has been deleted.");
                            HotfixStoreMgr.RemoveRecord(record.Type, record.RecordId);
                            break;
                        }
                    default:
                        {
                            packet.WriteLine($"Row: {record.RecordId} TableHash: {record.Type} has unknown Status: {record.Status}");
                            break;
                        }
                }

                var hotfixData = new HotfixData
                {
                    ID = record.HotfixId,
                    TableHash = record.Type,
                    RecordID = record.RecordId,
                    Status = record.Status
                };
                Storage.HotfixDatas.Add(hotfixData);
            }
        }

        private static void HandleHotfixOptionalData(Packet packet, DB2Hash type, int entry, Packet db2File)
        {
            var leftSize = db2File.Length - db2File.Position;
            var backupPosition = db2File.Position;

            // 28 bytes = size of TactKey optional data
            if (leftSize % 28 == 0)
            {
                var tactKeyCount = leftSize / 28;

                for (int i = 0; i < tactKeyCount; ++i)
                {
                    // get hash, we need to verify
                    var hash = db2File.ReadUInt32E<DB2Hash>();

                    // check if hash is valid hash, we only support TactKey optional data yet
                    if (hash == DB2Hash.TactKey)
                    {
                        // read optional data
                        var optionalData = db2File.ReadBytes(24);

                        packet.AddValue($"(OptionalData) [{i}] Key:", hash);
                        packet.AddValue($"(OptionalData) [{i}] OptionalData:", Convert.ToHexString(optionalData));

                        HotfixOptionalData hotfixOptionalData = new HotfixOptionalData
                        {
                            // data to link the optional data to correct hotfix
                            TableHash = type,
                            RecordID = entry,
                            Key = hash,

                            Data = new Blob(optionalData)
                        };

                        Storage.HotfixOptionalDatas.Add(hotfixOptionalData);
                    }
                    else
                    {
                        db2File.SetPosition(backupPosition);
                        db2File.WriteLine($"(Entry: {entry} TableHash: {type}) has incorrect structure OR optional data. PacketLength: {db2File.Length} CurrentPosition: {db2File.Position} ");
                        db2File.AsHex();
                    }
                }
            }
            else
            {
                db2File.WriteLine($"(Entry: {entry} TableHash: {type}) has incorrect structure OR optional data. PacketLength: {db2File.Length} CurrentPosition: {db2File.Position} ");
                db2File.AsHex();
            }
        }

        [Parser(Opcode.SMSG_AVAILABLE_HOTFIXES)]
        public static void HandleAvailableHotfixes(Packet packet)
        {
            packet.ReadUInt32("VirtualRealmAddress");

            var uniqueIDCount = packet.ReadUInt32("UniqueIDCount");
            for (var i = 0; i < uniqueIDCount; ++i)
            {
                packet.ReadInt32("PushID", i);
                packet.ReadUInt32("UniqueID", i);
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_HOTFIX_MESSAGE)]
        [Parser(Opcode.SMSG_HOTFIX_CONNECT)]
        public static void HandleHotixData815(Packet packet)
        {
            var hotfixRecords = new List<HotfixRecord>();
            var hotfixCount = packet.ReadUInt32("HotfixCount");

            for (var i = 0u; i < hotfixCount; ++i)
            {
                var hotfixRecord = new HotfixRecord();
                packet.ResetBitReader();

                hotfixRecord.HotfixId = packet.ReadUInt32();
                hotfixRecord.UniqueId = packet.ReadUInt32();
                hotfixRecord.Type = packet.ReadInt32E<DB2Hash>();
                hotfixRecord.RecordId = packet.ReadInt32();
                hotfixRecord.HotfixDataSize = packet.ReadInt32();
                packet.ResetBitReader();
                hotfixRecord.Status = (HotfixStatus)packet.ReadBits(3);

                hotfixRecords.Add(hotfixRecord);
            }

            var dataSize = packet.ReadInt32("HotfixDataSize");
            var data = packet.ReadBytes(dataSize);
            var hotfixData = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

            ReadHotfixData(hotfixData, hotfixRecords, "HotfixData");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            WowPacketParserModule.V9_0_1_36216.Parsers.HotfixHandler.HandleDBReply(packet);
        }

        [Parser(Opcode.CMSG_HOTFIX_REQUEST)]
        public static void HandleHotfixRequest(Packet packet)
        {
            WowPacketParserModule.V9_0_1_36216.Parsers.HotfixHandler.HandleHotfixRequest905(packet);
        }

        [Parser(Opcode.CMSG_DB_QUERY_BULK)]
        public static void HandleDbQueryBulk(Packet packet)
        {
            packet.ReadInt32E<DB2Hash>("TableHash");

            var count = packet.ReadBits("Count", 13);

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("RecordID", i);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class HotfixHandler
    {
        public class HotfixRecord
        {
            public uint HotfixId;
            public DB2Hash Type;
            public int RecordId;
            public int HotfixDataSize;
            public bool Allow;
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            var type = packet.ReadUInt32E<DB2Hash>("TableHash");
            var entry = packet.ReadInt32("RecordID");
            var timeStamp = packet.ReadUInt32();
            packet.AddValue("Timestamp", Utilities.GetDateTimeFromUnixTime(timeStamp));
            var allow = packet.ReadBit("Allow");

            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

            if (entry < 0 || !allow)
            {
                packet.WriteLine("Row {0} has been removed.", -entry);
                HotfixStoreMgr.RemoveRecord(type, entry);
            }
            else
            {
                switch (type)
                {
                    case DB2Hash.BroadcastText:
                        {
                            var bct = new BroadcastText()
                            {
                                Text = db2File.ReadCString("Text"),
                                Text1 = db2File.ReadCString("Text1"),
                            };

                            bct.ID = db2File.ReadUInt32("ID");
                            bct.LanguageID = db2File.ReadByte("LanguageID");
                            bct.ConditionID = db2File.ReadUInt32("ConditionID");
                            bct.EmotesID = db2File.ReadUInt16("EmotesID");
                            bct.Flags = db2File.ReadByte("Flags");
                            bct.ChatBubbleDurationMs = db2File.ReadUInt32("ChatBubbleDurationMs");

                            bct.SoundEntriesID = new uint?[2];
                            for (int i = 0; i < 2; ++i)
                                bct.SoundEntriesID[i] = db2File.ReadUInt32("SoundEntriesID", i);

                            bct.EmoteID = new ushort?[3];
                            bct.EmoteDelay = new ushort?[3];
                            for (int i = 0; i < 3; ++i)
                                bct.EmoteID[i] = db2File.ReadUInt16("EmoteID", i);
                            for (int i = 0; i < 3; ++i)
                                bct.EmoteDelay[i] = db2File.ReadUInt16("EmoteDelay", i);

                            Storage.BroadcastTexts.Add(bct, packet.TimeSpan);

                            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
                            {
                                BroadcastTextLocale lbct = new BroadcastTextLocale
                                {
                                    ID = bct.ID,
                                    TextLang = bct.Text,
                                    Text1Lang = bct.Text1
                                };
                                Storage.BroadcastTextLocales.Add(lbct, packet.TimeSpan);
                            }
                            break;
                        }
                    default:
                        HotfixStoreMgr.AddRecord(type, entry, db2File);
                        break;
                }

                db2File.ClosePacket(false);
            }
        }

        [Parser(Opcode.CMSG_HOTFIX_REQUEST)]
        public static void HandleHotfixQuery(Packet packet)
        {
            var hotfixCount = packet.ReadUInt32("HotfixCount");
            for (var i = 0u; i < hotfixCount; ++i)
            {
                var id = packet.ReadUInt64();

                packet.AddValue("HotfixID", Utilities.PAIR64_LOPART(id), i);
                packet.AddValue("TableHash", (DB2Hash)Utilities.PAIR64_HIPART(id), i);
            }
        }

        [Parser(Opcode.SMSG_AVAILABLE_HOTFIXES)]
        public static void HandleHotfixList(Packet packet)
        {
            packet.ReadInt32("HotfixCacheVersion");
            var hotfixCount = packet.ReadUInt32("HotfixCount");
            for (var i = 0u; i < hotfixCount; ++i)
            {
                var id = packet.ReadUInt64();

                packet.AddValue("HotfixID", Utilities.PAIR64_LOPART(id), i);
                packet.AddValue("TableHash", (DB2Hash)Utilities.PAIR64_HIPART(id), i);
            }
        }

        static void ReadHotfixData810(Packet packet, List<HotfixRecord> records, params object[] indexes)
        {
            int count = 0;
            foreach (var record in records)
            {
                var hotfixId = packet.AddValue("HotfixID", record.HotfixId, count, indexes, "HotfixRecord");
                var type = packet.AddValue("TableHash", record.Type, count, indexes, "HotfixRecord");
                var entry = packet.AddValue("RecordID", record.RecordId, count, indexes, "HotfixRecord");
                var dataSize = packet.AddValue("Size", record.HotfixDataSize, count, indexes, "HotfixRecord");
                var allow = packet.AddValue("Allow", record.Allow, count, indexes, "HotfixRecord");
                var data = packet.ReadBytes(dataSize);
                var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

                if (!allow)
                {
                    packet.WriteLine($"Row {entry} has been removed.");
                    HotfixStoreMgr.RemoveRecord(type, entry);
                }
                else
                {
                    packet.AddSniffData(StoreNameType.None, entry, type.ToString());
                    HotfixStoreMgr.AddRecord(type, entry, db2File);

                    if (db2File.Position != db2File.Length)
                    {
                        db2File.WriteLine($"(Entry: {entry} TableHash: {type}) has missing structure");
                        db2File.AsHex();

                        HotfixBlob hotfixBlob = new HotfixBlob
                        {
                            TableHash = type,
                            RecordID = entry,
                            Blob = "0x" + Utilities.ByteArrayToHexString(data)
                        };

                        Storage.HotfixBlobs.Add(hotfixBlob);
                    }

                    db2File.ClosePacket(false);
                }

                HotfixData hotfixData = new HotfixData
                {
                    ID = hotfixId,
                    TableHash = type,
                    RecordID = entry,
                    Deleted = !allow
                };

                Storage.HotfixDatas.Add(hotfixData);
                count++;
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_HOTFIX_MESSAGE, ClientVersionBuild.V8_1_0_28724)]
        [Parser(Opcode.SMSG_HOTFIX_RESPONSE, ClientVersionBuild.V8_1_0_28724)]
        public static void HandleHotixData810(Packet packet)
        {
            var hotfixRecords = new List<HotfixRecord>();
            var hotfixCount = packet.ReadUInt32("HotfixCount");

            for (var i = 0u; i < hotfixCount; ++i)
            {
                var hotfixRecord = new HotfixRecord();
                packet.ResetBitReader();

                var id = packet.ReadUInt64();
                hotfixRecord.HotfixId = Utilities.PAIR64_LOPART(id);
                hotfixRecord.Type = (DB2Hash)Utilities.PAIR64_HIPART(id);
                hotfixRecord.RecordId = packet.ReadInt32();
                hotfixRecord.HotfixDataSize = packet.ReadInt32();
                packet.ResetBitReader();
                hotfixRecord.Allow = packet.ReadBit();

                hotfixRecords.Add(hotfixRecord);
            }

            var dataSize = packet.ReadInt32();
            var data = packet.ReadBytes(dataSize);
            var hotfixData = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

            ReadHotfixData810(hotfixData, hotfixRecords, "HotfixData");
        }

        static void ReadHotfixData(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();

            var id = packet.ReadUInt64();
            var hotfixId = packet.AddValue("HotfixID", Utilities.PAIR64_LOPART(id), indexes);
            var type = packet.AddValue("TableHash", (DB2Hash)Utilities.PAIR64_HIPART(id), indexes);

            var entry = packet.ReadInt32("RecordID", indexes);
            packet.ResetBitReader();
            var allow = packet.ReadBit("Allow", indexes);
            var dataSize = packet.ReadInt32("Size", indexes);
            var data = packet.ReadBytes(dataSize);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

            if (!allow)
            {
                packet.WriteLine($"Row {entry} has been removed.");
                HotfixStoreMgr.RemoveRecord(type, entry);
            }
            else
            {
                packet.AddSniffData(StoreNameType.None, entry, type.ToString());
                HotfixStoreMgr.AddRecord(type, entry, db2File);

                if (db2File.Position != db2File.Length)
                {
                    db2File.WriteLine($"(Entry: {entry} TableHash: {type}) has missing structure, saved as Blob");
                    db2File.AsHex();

                    HotfixBlob hotfixBlob = new HotfixBlob
                    {
                        TableHash = type,
                        RecordID = entry,
                        Blob = "0x" + Utilities.ByteArrayToHexString(data)
                    };

                    Storage.HotfixBlobs.Add(hotfixBlob);
                }

                db2File.ClosePacket(false);
            }

            HotfixData hotfixData = new HotfixData
            {
                ID = hotfixId,
                TableHash = type,
                RecordID = entry,
                Deleted = !allow
            };

            Storage.HotfixDatas.Add(hotfixData);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_HOTFIX_MESSAGE, ClientVersionBuild.V8_0_1_27101, ClientVersionBuild.V8_1_0_28724)]
        [Parser(Opcode.SMSG_HOTFIX_RESPONSE, ClientVersionBuild.V8_0_1_27101, ClientVersionBuild.V8_1_0_28724)]
        public static void HandleHotixData(Packet packet)
        {
            var hotfixCount = packet.ReadUInt32("HotfixCount");
            for (var i = 0u; i < hotfixCount; ++i)
                ReadHotfixData(packet, i, "HotfixData");
        }
    }
}

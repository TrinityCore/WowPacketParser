using System;
using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WoWPacketParser.Proto;
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
            var dbReply = packet.Holder.DbReply = new();
            var type = packet.ReadUInt32E<DB2Hash>("TableHash");
            dbReply.TableHash = (uint)type;
            var entry = dbReply.RecordId = packet.ReadInt32("RecordID");
            var timeStamp = packet.ReadUInt32();
            var time = packet.AddValue("Timestamp", Utilities.GetDateTimeFromUnixTime(timeStamp));
            dbReply.Time = Timestamp.FromDateTime(DateTime.SpecifyKind(time, DateTimeKind.Utc));
            var allow = packet.ReadBit("Allow");

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

                            dbReply.BroadcastText = new PacketDbReplyBroadcastText()
                            {
                                Id = bct.ID.Value,
                                Text0 = bct.Text,
                                Text1 = bct.Text1,
                                Language = bct.LanguageID.Value,
                                ConditionId = bct.ConditionID.Value,
                                EmotesId = bct.EmotesID.Value,
                                Flags = bct.Flags.Value,
                                ChatBubbleDuration = bct.ChatBubbleDurationMs.Value,
                            };
                            dbReply.BroadcastText.Sounds.Add(bct.SoundEntriesID1.Value);
                            dbReply.BroadcastText.Sounds.Add(bct.SoundEntriesID2.Value);
                            for (int i = 0; i < 3; ++i)
                                dbReply.BroadcastText.Emotes.Add(new BroadcastTextEmote(){EmoteId = bct.EmoteID[i].Value, Delay = bct.EmoteDelay[i].Value});
                            break;
                        }
                    default:
                        HotfixStoreMgr.AddRecord(type, entry, db2File);
                        break;
                }

                db2File.ClosePacket(false);
            }
        }

        [Parser(Opcode.CMSG_HOTFIX_REQUEST, ClientVersionBuild.V8_1_0_28724, ClientVersionBuild.V8_1_5_29683)]
        public static void HandleHotfixQuery(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
            {
                packet.ReadUInt32("CurrentBuild");
                packet.ReadUInt32("InternalBuild");
            }
            var hotfixCount = packet.ReadUInt32("HotfixCount");
            for (var i = 0u; i < hotfixCount; ++i)
            {
                var id = packet.ReadUInt64();

                packet.AddValue("HotfixID", Utilities.PAIR64_LOPART(id), i);
                packet.AddValue("TableHash", (DB2Hash)Utilities.PAIR64_HIPART(id), i);
            }
        }

        [Parser(Opcode.CMSG_HOTFIX_REQUEST, ClientVersionBuild.V8_1_5_29683)]
        public static void HandleHotfixQuery815(Packet packet)
        {
            packet.ReadUInt32("CurrentBuild");
            packet.ReadUInt32("InternalBuild");
            var hotfixCount = packet.ReadUInt32("HotfixCount");
            for (var i = 0u; i < hotfixCount; ++i)
            {
                packet.ReadUInt32E<DB2Hash>("TableHash", i);
                packet.ReadInt32("RecordID", i);
                packet.ReadInt32("HotfixID", i);
            }
        }

        [Parser(Opcode.SMSG_AVAILABLE_HOTFIXES, ClientVersionBuild.V8_1_0_28724, ClientVersionBuild.V8_1_5_29683)]
        public static void HandleHotfixList(Packet packet)
        {
            packet.ReadInt32("VirtualRealmAddress");
            var hotfixCount = packet.ReadUInt32("HotfixCount");
            for (var i = 0u; i < hotfixCount; ++i)
            {
                var id = packet.ReadUInt64();

                packet.AddValue("HotfixID", Utilities.PAIR64_LOPART(id), i);
                packet.AddValue("TableHash", (DB2Hash)Utilities.PAIR64_HIPART(id), i);
            }
        }

        [Parser(Opcode.SMSG_AVAILABLE_HOTFIXES, ClientVersionBuild.V8_1_5_29683)]
        public static void HandleHotfixList815(Packet packet)
        {
            packet.ReadInt32("VirtualRealmAddress");
            var hotfixCount = packet.ReadUInt32("HotfixCount");
            for (var i = 0u; i < hotfixCount; ++i)
            {
                packet.ReadUInt32E<DB2Hash>("TableHash", i);
                packet.ReadInt32("RecordID", i);
                packet.ReadInt32("HotfixID", i);
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

                    if (HotfixStoreMgr.GetStore(type) == null)
                    {
                        db2File.WriteLine($"(Entry: {entry} TableHash: {type}) has missing structure. HotfixBlob entry generated!");
                        db2File.AsHex();

                        HotfixBlob hotfixBlob = new HotfixBlob
                        {
                            TableHash = type,
                            RecordID = entry,
                            Blob = new Blob(data)
                        };

                        Storage.HotfixBlobs.Add(hotfixBlob);
                    }
                    else if (db2File.Position != db2File.Length)
                    {
                        db2File.WriteLine($"(Entry: {entry} TableHash: {type}) has incorrect structure");
                        db2File.AsHex();
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
        [Parser(Opcode.SMSG_HOTFIX_MESSAGE, ClientVersionBuild.V8_1_0_28724, ClientVersionBuild.V8_1_5_29683)]
        [Parser(Opcode.SMSG_HOTFIX_CONNECT, ClientVersionBuild.V8_1_0_28724, ClientVersionBuild.V8_1_5_29683)]
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

        [HasSniffData]
        [Parser(Opcode.SMSG_HOTFIX_MESSAGE, ClientVersionBuild.V8_1_5_29683)]
        [Parser(Opcode.SMSG_HOTFIX_CONNECT, ClientVersionBuild.V8_1_5_29683)]
        public static void HandleHotixData815(Packet packet)
        {
            var hotfixRecords = new List<HotfixRecord>();
            var hotfixCount = packet.ReadUInt32("HotfixCount");

            for (var i = 0u; i < hotfixCount; ++i)
            {
                var hotfixRecord = new HotfixRecord();
                packet.ResetBitReader();

                hotfixRecord.Type = packet.ReadUInt32E<DB2Hash>();
                hotfixRecord.RecordId = packet.ReadInt32();
                hotfixRecord.HotfixId = packet.ReadUInt32();
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

                if (HotfixStoreMgr.GetStore(type) == null)
                {
                    db2File.WriteLine($"(Entry: {entry} TableHash: {type}) has missing structure. HotfixBlob entry generated!");
                    db2File.AsHex();

                    HotfixBlob hotfixBlob = new HotfixBlob
                    {
                        TableHash = type,
                        RecordID = entry,
                        Blob = new Blob(data)
                    };

                    Storage.HotfixBlobs.Add(hotfixBlob);
                }
                else if (db2File.Position != db2File.Length)
                {
                    db2File.WriteLine($"(Entry: {entry} TableHash: {type}) has incorrect structure");
                    db2File.AsHex();
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
        [Parser(Opcode.SMSG_HOTFIX_CONNECT, ClientVersionBuild.V8_0_1_27101, ClientVersionBuild.V8_1_0_28724)]
        public static void HandleHotixData(Packet packet)
        {
            var hotfixCount = packet.ReadUInt32("HotfixCount");
            for (var i = 0u; i < hotfixCount; ++i)
                ReadHotfixData(packet, i, "HotfixData");
        }

        [Parser(Opcode.CMSG_DB_QUERY_BULK)]
        public static void HandleDbQueryBulk(Packet packet)
        {
            packet.ReadInt32E<DB2Hash>("TableHash");

            var count = packet.ReadBits("Count", 13);
            for (var i = 0; i < count; ++i)
            {
                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V8_2_5_31921))
                    packet.ReadPackedGuid128("Guid", i);
                packet.ReadInt32("RecordID", i);
            }
        }
    }
}

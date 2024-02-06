using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
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
            int statusBits = ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_0_39185) ? 3 : 2;
            var status = packet.ReadBitsE<HotfixStatus>("Status", statusBits);
            switch (status)
            {
                case HotfixStatus.Valid:
                    dbReply.Status = PacketDbReplyRecordStatus.RecordStatusValid;
                    break;
                case HotfixStatus.RecordRemoved:
                    dbReply.Status = PacketDbReplyRecordStatus.RecordStatusRecordRemoved;
                    break;
                case HotfixStatus.Invalid:
                    dbReply.Status = PacketDbReplyRecordStatus.RecordStatusInvalid;
                    break;
                case HotfixStatus.NotPublic:
                    dbReply.Status = PacketDbReplyRecordStatus.RecordStatusNotPublic;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

            switch (status)
            {
                case HotfixStatus.Valid:
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
                            bct.LanguageID = db2File.ReadInt32("LanguageID");
                            bct.ConditionID = db2File.ReadUInt32("ConditionID");
                            bct.EmotesID = db2File.ReadUInt16("EmotesID");
                            bct.Flags = db2File.ReadByte("Flags");
                            bct.ChatBubbleDurationMs = db2File.ReadUInt32("ChatBubbleDurationMs");
                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
                                bct.VoiceOverPriorityID = db2File.ReadUInt32("VoiceOverPriorityID");

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
                            for (int i = 0; i < 2; ++i)
                                dbReply.BroadcastText.Sounds.Add(bct.SoundEntriesID[i] ?? 0);
                            for (int i = 0; i < 3; ++i)
                                dbReply.BroadcastText.Emotes.Add(new BroadcastTextEmote() { EmoteId = bct.EmoteID[i] ?? 0, Delay = bct.EmoteDelay[i] ?? 0 });
                            break;
                        }
                        case DB2Hash.TactKey:
                            var hotfix = new TactKeyHotfix();
                            hotfix.ID = (uint)entry;
                            hotfix.Key = new byte?[16];
                            for (int i = 0; i < 16; i++)
                                hotfix.Key[i] = db2File.ReadByte("Key",  i);

                            Storage.TactKeyHotfixes.Add(hotfix, packet.TimeSpan);
                            break;
                        default:
                            HotfixStoreMgr.AddRecord(type, entry, db2File);
                            break;
                    }

                    if (db2File.Position != db2File.Length)
                        HandleHotfixOptionalData(packet, type, entry, db2File);

                    db2File.ClosePacket(false);
                    break;
                }
                case HotfixStatus.RecordRemoved:
                {
                    packet.WriteLine($"Row {entry} has been removed.");
                    HotfixStoreMgr.RemoveRecord(type, entry);
                    break;
                }
                case HotfixStatus.Invalid:
                {
                    // sniffs from others may have the data
                    packet.WriteLine($"Row {entry} is invalid.");
                    break;
                }
                case HotfixStatus.NotPublic:
                {
                    packet.WriteLine($"Row {entry} is not public.");
                    break;
                }
                default:
                {
                    packet.WriteLine($"Unhandled status: {status}");
                    break;
                }
            }
        }

        static void ReadHotfixData(Packet packet, List<HotfixRecord> records, params object[] indexes)
        {
            int count = 0;
            foreach (var record in records)
            {
                var hotfixId = packet.AddValue("HotfixID", record.HotfixId, count, indexes, "HotfixRecord");
                var uniqueId = packet.AddValue("UniqueID", record.UniqueId, count, indexes, "HotfixRecord");
                var type = packet.AddValue("TableHash", record.Type, count, indexes, "HotfixRecord");
                var entry = packet.AddValue("RecordID", record.RecordId, count, indexes, "HotfixRecord");
                var dataSize = packet.AddValue("Size", record.HotfixDataSize, count, indexes, "HotfixRecord");
                var status = packet.AddValue("Status", record.Status, count, indexes, "HotfixRecord");
                var data = packet.ReadBytes(dataSize);
                var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

                switch (status)
                {
                    case HotfixStatus.Valid:
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
                            HandleHotfixOptionalData(packet, type, entry, db2File);

                        db2File.ClosePacket(false);
                        break;
                    }
                    case HotfixStatus.RecordRemoved:
                    {
                        packet.WriteLine($"Row {entry} has been removed.");
                        HotfixStoreMgr.RemoveRecord(type, entry);
                        break;
                    }
                    case HotfixStatus.Invalid:
                    {
                        // sniffs from others may have the data
                        packet.WriteLine($"Row {entry} is invalid.");
                        break;
                    }
                    default:
                    {
                        packet.WriteLine($"Unhandled status: {status}");
                        break;
                    }
                }

                HotfixData hotfixData = new HotfixData
                {
                    ID = hotfixId,
                    UniqueID = uniqueId,
                    TableHash = type,
                    RecordID = entry,
                    Status = status
                };

                Storage.HotfixDatas.Add(hotfixData);
                count++;
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
                hotfixRecord.Type = packet.ReadUInt32E<DB2Hash>();
                hotfixRecord.RecordId = packet.ReadInt32();
                hotfixRecord.HotfixDataSize = packet.ReadInt32();
                packet.ResetBitReader();
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_0_39185))
                    hotfixRecord.Status = (HotfixStatus)packet.ReadBits(3);
                else
                    hotfixRecord.Status = (HotfixStatus)packet.ReadBits(2);

                hotfixRecords.Add(hotfixRecord);
            }

            var dataSize = packet.ReadInt32("HotfixDataSize");
            var data = packet.ReadBytes(dataSize);
            var hotfixData = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

            ReadHotfixData(hotfixData, hotfixRecords, "HotfixData");
        }

        [Parser(Opcode.CMSG_HOTFIX_REQUEST, ClientVersionBuild.V9_0_5_37503)]
        public static void HandleHotfixRequest905(Packet packet)
        {
            packet.ReadUInt32("CurrentBuild");
            packet.ReadUInt32("InternalBuild");
            var hotfixCount = packet.ReadUInt32("HotfixCount");
            for (var i = 0u; i < hotfixCount; ++i)
                packet.ReadInt32("HotfixID", i);
        }

        [Parser(Opcode.SMSG_AVAILABLE_HOTFIXES, ClientVersionBuild.V9_0_5_37503, ClientVersionBuild.V9_1_5_40772)]
        public static void HandleAvailableHotfixes905(Packet packet)
        {
            packet.ReadInt32("VirtualRealmAddress");
            var hotfixCount = packet.ReadUInt32("HotfixCount");
            for (var i = 0u; i < hotfixCount; ++i)
                packet.ReadInt32("HotfixID", i);
        }

        [Parser(Opcode.SMSG_AVAILABLE_HOTFIXES, ClientVersionBuild.V9_1_5_40772)]
        public static void HandleAvailableHotfixes915(Packet packet)
        {
            packet.ReadInt32("VirtualRealmAddress");
            var hotfixCount = packet.ReadUInt32("HotfixCount");
            for (var i = 0u; i < hotfixCount; ++i)
            {
                packet.ReadInt32("PushID", i, "HotfixUniqueID");
                packet.ReadUInt32("UniqueID", i, "HotfixUniqueID");
            }
        }
    }
}

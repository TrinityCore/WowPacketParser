using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class HotfixHandler
    {
        public class HotfixRecord
        {
            public uint HotfixId;
            public DB2Hash Type;
            public int RecordId;
            public int HotfixDataSize;
            public HotfixStatus Status;
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            var type = packet.ReadUInt32E<DB2Hash>("TableHash");
            var entry = packet.ReadInt32("RecordID");
            var timeStamp = packet.ReadUInt32();
            packet.AddValue("Timestamp", Utilities.GetDateTimeFromUnixTime(timeStamp));
            var status = packet.ReadBitsE<HotfixStatus>("Status", 2);

            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

            if (entry < 0 || status == HotfixStatus.Invalid)
            {
                packet.WriteLine("Row {0} has been removed.", -entry);
                HotfixStoreMgr.RemoveRecord(type, entry);
            }
            if (status == HotfixStatus.Unavailable)
            {
                packet.WriteLine("Row {0} is unavailable.", entry);
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
                        bct.LanguageID = db2File.ReadInt32("LanguageID");
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

                if (db2File.Position != db2File.Length)
                    HandleHotfixOptionalData(packet, type, entry, db2File);

                db2File.ClosePacket(false);
            }
        }

        static void ReadHotfixData(Packet packet, List<HotfixRecord> records, params object[] indexes)
        {
            int count = 0;
            foreach (var record in records)
            {
                var hotfixId = packet.AddValue("HotfixID", record.HotfixId, count, indexes, "HotfixRecord");
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
                                    Blob = "0x" + Utilities.ByteArrayToHexString(data)
                                };

                                Storage.HotfixBlobs.Add(hotfixBlob);
                            }
                            else if (db2File.Position != db2File.Length)
                                HandleHotfixOptionalData(packet, type, entry, db2File);

                            db2File.ClosePacket(false);
                            break;
                        }
                    case HotfixStatus.Invalid:
                        {
                            packet.WriteLine($"Row {entry} has been removed.");
                            HotfixStoreMgr.RemoveRecord(type, entry);
                            break;
                        }
                    case HotfixStatus.Unavailable:
                        {
                            // sniffs from others may have the data
                            packet.WriteLine($"Row {entry} is unavailable.");
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
                        packet.AddValue($"(OptionalData) [{i}] OptionalData:", Utilities.ByteArrayToHexString(optionalData));

                        HotfixOptionalData hotfixOptionalData = new HotfixOptionalData
                        {
                            // data to link the optional data to correct hotfix
                            TableHash = type,
                            RecordID = entry,
                            Key = hash,

                            Data = "0x" + Utilities.ByteArrayToHexString(optionalData)
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

                hotfixRecord.Type = packet.ReadUInt32E<DB2Hash>();
                hotfixRecord.RecordId = packet.ReadInt32();
                hotfixRecord.HotfixId = packet.ReadUInt32();
                hotfixRecord.HotfixDataSize = packet.ReadInt32();
                packet.ResetBitReader();
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

        [Parser(Opcode.SMSG_AVAILABLE_HOTFIXES, ClientVersionBuild.V9_0_5_37503)]
        public static void HandleAvailableHotfixes905(Packet packet)
        {
            packet.ReadInt32("VirtualRealmAddress");
            var hotfixCount = packet.ReadUInt32("HotfixCount");
            for (var i = 0u; i < hotfixCount; ++i)
                packet.ReadInt32("HotfixID", i);
        }
    }
}

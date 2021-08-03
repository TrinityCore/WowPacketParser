using System;
using Google.Protobuf.WellKnownTypes;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WoWPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class HotfixHandler
    {
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
                            ID = (uint)entry,
                            Text = db2File.ReadCString("Text"),
                            Text1 = db2File.ReadCString("Text1"),
                        };

                        bct.EmoteID = new ushort?[3];
                        bct.EmoteDelay = new ushort?[3];

                        for (int i = 0; i < 3; ++i)
                            bct.EmoteID[i] = db2File.ReadUInt16("EmoteID", i);
                        for (int i = 0; i < 3; ++i)
                            bct.EmoteDelay[i] = db2File.ReadUInt16("EmoteDelay", i);

                        bct.EmotesID = db2File.ReadUInt16("EmotesID");
                        bct.LanguageID = db2File.ReadByte("LanguageID");
                        bct.Flags = db2File.ReadByte("Flags");

                        if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_5_25848))
                            bct.ConditionID = db2File.ReadUInt32("ConditionID");

                        bct.SoundEntriesID = new uint?[2];
                        for (int i = 0; i < 2; ++i)
                            bct.SoundEntriesID[i] = db2File.ReadUInt32("SoundEntriesID", i);

                        if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_0_3_22248) && ClientVersion.RemovedInVersion(ClientVersionBuild.V7_3_5_25848))
                            bct.ConditionID = db2File.ReadUInt32("ConditionID");

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
                            ConditionId = bct.ConditionID ?? 0,
                            EmotesId = bct.EmotesID.Value,
                            Flags = bct.Flags.Value,
                            ChatBubbleDuration = 0,
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

        [Parser(Opcode.CMSG_HOTFIX_REQUEST)]
        public static void HandleHotfixQuery(Packet packet)
        {
            var hotfixCount = packet.ReadUInt32("HotfixCount");
            for (var i = 0u; i < hotfixCount; ++i)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_5_24330))
                {
                    var id = packet.ReadUInt64();

                    packet.AddValue("HotfixID", Utilities.PAIR64_LOPART(id), i);
                    packet.AddValue("TableHash", (DB2Hash)Utilities.PAIR64_HIPART(id), i);
                }
                else
                    packet.ReadInt32("HotfixID", i);
            }
        }

        [Parser(Opcode.SMSG_AVAILABLE_HOTFIXES)]
        public static void HandleHotfixList(Packet packet)
        {
            packet.ReadInt32("VirtualRealmAddress");
            var hotfixCount = packet.ReadUInt32("HotfixCount");
            for (var i = 0u; i < hotfixCount; ++i)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_5_24330))
                {
                    var id = packet.ReadUInt64();

                    packet.AddValue("HotfixID", Utilities.PAIR64_LOPART(id), i);
                    packet.AddValue("TableHash", (DB2Hash)Utilities.PAIR64_HIPART(id), i);
                }
                else
                    packet.ReadInt32("HotfixID", i);
            }
        }

        static void ReadHotfixRecord(Packet packet, int hotfixId, params object[] indexes)
        {
            packet.ResetBitReader();
            var type = packet.ReadUInt32E<DB2Hash>("TableHash", indexes);
            var entry = packet.ReadInt32("RecordID", indexes);
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
                    db2File.WriteLine($"(Entry: {entry} TableHash: {type}) has missing structure");
                    db2File.AsHex();
                }

                db2File.ClosePacket(false);
            }

            HotfixData hotfixData = new HotfixData
            {
                ID = (uint)hotfixId,
                TableHash = type,
                RecordID = entry,
                Deleted = !allow
            };

            Storage.HotfixDatas.Add(hotfixData);
        }

        static void ReadHotfixRecord725(Packet packet, params object[] indexes)
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
                    db2File.WriteLine($"(Entry: {entry} TableHash: {type}) has missing structure");
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

        static void ReadHotfixData(Packet packet, params object[] indexes)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_5_24330))
                ReadHotfixRecord725(packet, indexes, "HotfixRecord");
            else
            {
                var hotfixId = packet.ReadInt32("HotfixID", indexes);
                var recordCount = packet.ReadUInt32("RecordCount");
                for (var i = 0u; i < recordCount; ++i)
                    ReadHotfixRecord(packet, hotfixId, indexes, i, "HotfixRecord");
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_HOTFIX_MESSAGE)]
        [Parser(Opcode.SMSG_HOTFIX_CONNECT)]
        public static void HandleHotixData(Packet packet)
        {
            var hotfixCount = packet.ReadUInt32("HotfixCount");
            for (var i = 0u; i < hotfixCount; ++i)
                ReadHotfixData(packet, i, "HotfixData");
        }
    }
}

using System;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
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
            var type = packet.ReadUInt32E<DB2Hash>("TableHash");
            var entry = packet.ReadInt32("RecordID");
            var timeStamp = packet.ReadUInt32();
            packet.AddValue("Timestamp", Utilities.GetDateTimeFromUnixTime(timeStamp));
            var allow = packet.ReadBit("Allow");

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
                    if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, -(int)entry)))
                    {
                        hotfixData.Deleted = true;
                        hotfixData.RecordID = -(int)entry;
                        hotfixData.Timestamp = Storage.HotfixDataStore[new Tuple<DB2Hash, int>(type, -(int)entry)].Item1.Timestamp;
                        Storage.HotfixDatas.Add(hotfixData);
                    }
                }
            }
            else
            {
                packet.AddSniffData(StoreNameType.None, entry, type.ToString());

                switch (type)
                {
                    case DB2Hash.BroadcastText:
                    {
                        var bct = new BroadcastText()
                        {
                            ID = (uint)entry,
                            MaleText = db2File.ReadCString("MaleText"),
                            FemaleText = db2File.ReadCString("FemaleText"),
                        };

                        bct.EmoteID = new ushort?[3];
                        bct.EmoteDelay = new ushort?[3];

                        for (int i = 0; i < 3; ++i)
                            bct.EmoteID[i] = db2File.ReadUInt16("EmoteID", i);
                        for (int i = 0; i < 3; ++i)
                            bct.EmoteDelay[i] = db2File.ReadUInt16("EmoteDelay", i);

                        bct.UnkEmoteID = db2File.ReadUInt16("UnkEmoteID");
                        bct.Language = db2File.ReadByte("Language");
                        bct.Type = db2File.ReadByte("Type");

                        bct.SoundID = new uint?[2];
                        for (int i = 0; i < 2; ++i)
                            bct.SoundID[i] = db2File.ReadUInt32("SoundID", i);

                        bct.PlayerConditionID = db2File.ReadUInt32("PlayerConditionID");

                        Storage.BroadcastTexts.Add(bct, packet.TimeSpan);
                        break;
                    }
                    default:
                        HotfixStoreMgr.AddRecord(type, entry, db2File);
                        break;
                }

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

                db2File.ClosePacket(false);
            }
        }
    }
}

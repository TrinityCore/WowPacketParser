using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;

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
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer,
                packet.FileName);

            if (entry < 0 || !allow)
            {
                packet.WriteLine("Row {0} has been removed.", -entry);
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

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY_BULK)]
        public static void HandleDBReplyBulk(Packet packet)
        {
            var count = packet.ReadInt32("HotfixCount");

            for (var i = 0; i < count; i++)
            {
                packet.ReadUInt32("Index", i);
                var count2 = packet.ReadInt32("Count", i);

                for (var j = 0; j < count2; j++)
                {
                    var type = packet.ReadUInt32E<DB2Hash>("TableHash", i, j);
                    var entry = packet.ReadInt32("RecordID", i, j);

                    packet.ResetBitReader();

                    var allow = packet.ReadBit("Allow", i, j);

                    var size = packet.ReadInt32("Size", i, j);
                    var data = packet.ReadBytes(size);
                    var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer,
                        packet.FileName);

                    if (entry < 0 || !allow)
                    {
                        packet.WriteLine($"[{ i.ToString() }] [{ j.ToString() }] Row { -entry } has been removed.");
                        HotfixStoreMgr.RemoveRecord(type, entry);
                        //Storage.AddHotfixData(entry, type, true);
                    }
                    else
                    {
                        HotfixStoreMgr.AddRecord(type, entry, db2File);
                        //Storage.AddHotfixData(entry, type, false);
                        db2File.ClosePacket(false);
                    }
                }
            }
        }
    }
}

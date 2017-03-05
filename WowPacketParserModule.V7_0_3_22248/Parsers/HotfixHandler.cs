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
            var type = packet.Translator.ReadUInt32E<DB2Hash>("TableHash");
            var entry = packet.Translator.ReadInt32("RecordID");
            var timeStamp = packet.Translator.ReadUInt32();
            packet.AddValue("Timestamp", Utilities.GetDateTimeFromUnixTime(timeStamp));
            var allow = packet.Translator.ReadBit("Allow");

            var size = packet.Translator.ReadInt32("Size");
            var data = packet.Translator.ReadBytes(size);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Formatter,
                packet.FileName);

            if (entry < 0 || !allow)
            {
                packet.Formatter.AppendItem("Row {0} has been removed.", -entry);
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
    }
}

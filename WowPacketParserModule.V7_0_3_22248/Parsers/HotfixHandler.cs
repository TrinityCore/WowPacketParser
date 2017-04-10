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

        [Parser(Opcode.CMSG_HOTFIX_QUERY)]
        public static void HandleHotfixQuery(Packet packet)
        {
            var hotfixCount = packet.ReadUInt32();
            for (var i = 0u; i < hotfixCount; ++i)
                packet.ReadInt32("HotfixID", i);
        }

        [Parser(Opcode.SMSG_HOTFIX_LIST)]
        public static void HandleHotfixList(Packet packet)
        {
            packet.ReadInt32("HotfixCacheVersion");
            var hotfixCount = packet.ReadUInt32();
            for (var i = 0u; i < hotfixCount; ++i)
                packet.ReadInt32("HotfixID", i);
        }

        static void ReadHotfixRecord(Packet packet, int hotfixId, params object[] indexes)
        {
            packet.ResetBitReader();
            var type = packet.ReadUInt32E<DB2Hash>("TableHash", indexes);
            var entry = packet.ReadInt32("RecordID", indexes);
            var allow = packet.ReadBit("Allow", indexes);
            var dataSize = packet.ReadInt32();
            var data = packet.ReadBytes(dataSize);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer,
                packet.FileName);

            // TODO: new table for hotfix list
            // concept
            // TABLE `hotfixes`
            // COLUMN `HotfixID` PK - critical to maintain between server restarts, client uses it to determine whether it should request the hotfix
            // COLUMN `TableHash` PK
            // COLUMN `RecordID` PK
            // COLUMN `Deleted`
            if (entry < 0 || !allow)
            {
                packet.WriteLine("Row {0} has been removed.", -entry);
                HotfixStoreMgr.RemoveRecord(type, entry);
            }
            else
            {
                packet.AddSniffData(StoreNameType.None, entry, type.ToString());
                HotfixStoreMgr.AddRecord(type, entry, db2File);
                db2File.ClosePacket(false);
            }
        }

        static void ReadHotfixData(Packet packet, params object[] indexes)
        {
            var hotfixId = packet.ReadInt32("HotfixID", indexes);
            var recordCount = packet.ReadUInt32();
            for (var i = 0u; i < recordCount; ++i)
                ReadHotfixRecord(packet, hotfixId, indexes, i, "HotfixRecord");
        }

        //[HasSniffData]
        [Parser(Opcode.SMSG_HOTFIXES)]
        [Parser(Opcode.SMSG_HOTFIX_QUERY_RESPONSE)]
        public static void HandleHotixData(Packet packet)
        {
            var hotfixCount = packet.ReadUInt32();
            for (var i = 0u; i < hotfixCount; ++i)
                ReadHotfixData(packet, i, "HotfixData");
        }
    }
}

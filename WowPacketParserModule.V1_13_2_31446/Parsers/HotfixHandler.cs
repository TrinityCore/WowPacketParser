using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V1_13_2_31446.Parsers
{
    public static class HotfixHandler
    {
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

        [HasSniffData]
        [Parser(Opcode.SMSG_HOTFIX_MESSAGE)]
        [Parser(Opcode.SMSG_HOTFIX_CONNECT)]
        public static void HandleHotixData(Packet packet)
        {
            var hotfixCount = packet.ReadUInt32("HotfixCount");
            for (var i = 0u; i < hotfixCount; ++i)
                ReadHotfixData(packet, i, "HotfixData");
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
    }
}

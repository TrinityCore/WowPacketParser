using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct BugReport
    {
        public ReportType Type;
        public string Text;
        public string DiagInfo;



        [Parser(Opcode.CMSG_BUG_REPORT)]
        public static void HandleBugReport(Packet packet)
        {
            packet.ReadBit("Type");

            var len1 = packet.ReadBits(12);
            var len2 = packet.ReadBits(10);

            packet.ReadWoWString("DiagInfo", len1);
            packet.ReadWoWString("Text", len2);
        }
    }
}

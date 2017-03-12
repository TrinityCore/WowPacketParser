using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatReportIgnored
    {
        public ulong IgnoredGUID;
        public byte Reason;

        [Parser(Opcode.CMSG_CHAT_REPORT_IGNORED)]
        public static void HandleChatIgnored(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadByte("Unk Byte");
        }
    }
}

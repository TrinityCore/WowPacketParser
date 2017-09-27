using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserRouterClient
{
    public unsafe struct LogDisconnect
    {
        public uint Reason;

        [Parser(Opcode.CMSG_LOG_DISCONNECT)]
        public static void HandleLogDisconnect(Packet packet)
        {
            packet.ReadUInt32("Reason");
            // 4 is inability for client to decrypt RSA
            // 3 is not receiving "WORLD OF WARCRAFT CONNECTION - SERVER TO CLIENT"
            // 11 is sent on receiving opcode 0x140 with some specific data
        }
    }
}

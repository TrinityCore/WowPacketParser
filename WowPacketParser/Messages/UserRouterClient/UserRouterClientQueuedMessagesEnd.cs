using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserRouterClient
{
    public unsafe struct UserRouterClientQueuedMessagesEnd
    {
        public uint Timestamp;

        [Parser(Opcode.CMSG_QUEUED_MESSAGES_END)]
        public static void HandleQueuedMessagesEnd(Packet packet)
        {
            packet.ReadInt32("Timestamp");
        }
    }
}

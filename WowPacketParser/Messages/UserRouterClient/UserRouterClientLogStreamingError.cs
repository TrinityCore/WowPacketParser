using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserRouterClient
{
    public unsafe struct UserRouterClientLogStreamingError
    {
        public string Error;

        [Parser(Opcode.CMSG_LOG_STREAMING_ERROR)]
        public static void HandleRouterClientLogStreamingError(Packet packet)
        {
            var bits16 = packet.ReadBits(9);
            packet.ReadWoWString("Error", bits16);
        }
    }
}

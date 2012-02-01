using System.Collections.Generic;

namespace WowPacketParser.Enums.Version//.V4_3_2_15211
{
    public static partial class Opcodes
    {
        private static readonly Dictionary<Opcode, int> _V4_3_2_opcodes = new Dictionary<Opcode, int>
        {
            {Opcode.CMSG_AUTH_SESSION, 16450},
            {Opcode.CMSG_CHAR_ENUM, 16465},
            {Opcode.CMSG_READY_FOR_ACCOUNT_DATA_TIMES, 17707},
            {Opcode.CMSG_REDIRECTION_AUTH_PROOF, 74},
            {Opcode.SMSG_ACCOUNT_DATA_TIMES, 1419},
            {Opcode.SMSG_ADDON_INFO, 28045},
            {Opcode.SMSG_AUTH_CHALLENGE, 297},
            {Opcode.SMSG_AUTH_RESPONSE, 3668},
            {Opcode.SMSG_CHAR_ENUM, 3317},
            {Opcode.SMSG_CLIENTCACHE_VERSION, 17725},
            {Opcode.SMSG_REDIRECT_CLIENT, 4905},
            {Opcode.SMSG_TUTORIAL_FLAGS, 19855},
        };
    }
}

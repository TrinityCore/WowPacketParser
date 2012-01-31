using System.Collections.Generic;

namespace WowPacketParser.Enums.Version//.V4_3_2_15211
{
    public static partial class Opcodes
    {
        private static readonly Dictionary<Opcode, int> _V4_3_2_opcodes = new Dictionary<Opcode, int>
        {
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

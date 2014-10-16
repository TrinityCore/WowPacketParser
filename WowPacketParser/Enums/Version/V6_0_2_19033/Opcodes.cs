using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V6_0_2_19033
{
    public static class Opcodes_6_0_2
    {
        public static BiDictionary<Opcode, int> Opcodes()
        {
            return Opcs;
        }

        private static readonly BiDictionary<Opcode, int> Opcs = new BiDictionary<Opcode, int>
        {
            {Opcode.CMSG_AUTH_SESSION, 0x1B05},

            {Opcode.SMSG_ADDON_INFO, 0x1400},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x10AA},
            {Opcode.SMSG_AUTH_RESPONSE, 0x1333},
            {Opcode.SMSG_CHAR_ENUM, 0x1154},
            {Opcode.SMSG_CLIENTCACHE_VERSION, 0x10EF},
            {Opcode.SMSG_REDIRECT_CLIENT, 0x1082},
        };
    }
}

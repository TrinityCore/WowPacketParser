using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V5_4_0_17359
{
    public static class Opcodes_5_4_0
    {
        public static BiDictionary<Opcode, int> Opcodes()
        {
            return Opcs;
        }

        private static readonly BiDictionary<Opcode, int> Opcs = new BiDictionary<Opcode, int>
        {
            {Opcode.CMSG_LOG_DISCONNECT, 0x0380},
            {Opcode.CMSG_AUTH_SESSION, 0x0790},
            {Opcode.SMSG_ADDON_INFO, 0x0128},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x016A},
            {Opcode.SMSG_AUTH_RESPONSE, 0x090E},
            {Opcode.SMSG_CLIENTCACHE_VERSION, 0x0825},
            {Opcode.SMSG_MOTD, 0x082A},
            {Opcode.SMSG_TUTORIAL_FLAGS, 0x1F35},
        };
    }
}

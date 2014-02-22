using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V5_4_7_17930
{
    public static class Opcodes_5_4_7
    {
        public static BiDictionary<Opcode, int> Opcodes()
        {
            return Opcs;
        }

        private static readonly BiDictionary<Opcode, int> Opcs = new BiDictionary<Opcode, int>
        {
            {Opcode.CMSG_AUTH_SESSION, 0x1A51},
            {Opcode.CMSG_CONNECT_TO_FAILED, 0x16C8},
            {Opcode.CMSG_LOG_DISCONNECT, 0x1A13},
            {Opcode.CMSG_REDIRECTION_AUTH_PROOF, 0x1A5B},
            {Opcode.SMSG_MULTIPLE_PACKETS, 0x05B1},
            {Opcode.SMSG_ADDON_INFO, 0x10E2},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x14B8},
            {Opcode.SMSG_AUTH_RESPONSE, 0x15A0}
        };
    }
}

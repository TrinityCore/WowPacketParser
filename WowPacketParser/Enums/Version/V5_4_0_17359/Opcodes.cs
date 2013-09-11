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
            {Opcode.CMSG_AUTH_SESSION, 0x0790},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x016A},
            {Opcode.SMSG_AUTH_RESPONSE, 0x090E},
        };
    }
}

using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V5_2_0_16650
{
    public static class Opcodes_5_2_0
    {
        public static BiDictionary<Opcode, int> Opcodes()
        {
            return Opcs;
        }

        private static readonly BiDictionary<Opcode, int> Opcs = new BiDictionary<Opcode, int>
        {
            {Opcode.SMSG_UPDATE_OBJECT, 0x1406},
        };
    }
}

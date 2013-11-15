using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V5_4_1_17538
{
    public static class Opcodes_5_4_1
    {
        public static BiDictionary<Opcode, int> Opcodes()
        {
            return Opcs;
        }

        private static readonly BiDictionary<Opcode, int> Opcs = new BiDictionary<Opcode, int>
        {
            {Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x1486},
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE, 0x0916},
            {Opcode.SMSG_REALM_QUERY_RESPONSE, 0x052D},
            
        };
    }
}

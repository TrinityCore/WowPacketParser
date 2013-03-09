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
            {Opcode.CMSG_CREATURE_QUERY, 0x1C83},
            {Opcode.CMSG_GAMEOBJECT_QUERY, 0x0118},
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x01B5},
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE, 0x1024},
            {Opcode.SMSG_UPDATE_OBJECT, 0x1406},
        };
    }
}

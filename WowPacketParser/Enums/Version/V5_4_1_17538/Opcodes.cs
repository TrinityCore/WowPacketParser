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
            {Opcode.CMSG_CREATURE_QUERY, 0x1647},
            {Opcode.CMSG_DB_QUERY_BULK, 0x01E4},
            {Opcode.CMSG_GAMEOBJECT_QUERY, 0x1677},
            {Opcode.SMSG_GOSSIP_MESSAGE, 0x03FC},
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x011C},
            {Opcode.SMSG_DB_REPLY, 0x1406},
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE, 0x0916},
            {Opcode.SMSG_UPDATE_OBJECT, 0x0C22}
        };
    }
}

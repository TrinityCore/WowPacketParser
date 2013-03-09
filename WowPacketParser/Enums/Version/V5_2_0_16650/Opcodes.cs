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
            {Opcode.CMSG_AUTH_SESSION, 0x1A72},
            {Opcode.CMSG_CREATURE_QUERY, 0x1C83},
            {Opcode.CMSG_GAMEOBJECT_QUERY, 0x0118},
            {Opcode.CMSG_QUEST_QUERY, 0x001F},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x0C0B},
            {Opcode.SMSG_CHAR_ENUM, 0x01B4},
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x01B5},
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE, 0x1024},
            {Opcode.SMSG_QUEST_QUERY_RESPONSE, 0x19C5},
            {Opcode.SMSG_TUTORIAL_FLAGS, 0x0B97},
            {Opcode.SMSG_UPDATE_OBJECT, 0x1406},
        };
    }
}

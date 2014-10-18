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
            {Opcode.CMSG_NAME_QUERY, 0x0BA4},

            {Opcode.SMSG_ADDON_INFO, 0x1400},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x10AA},
            {Opcode.SMSG_AUTH_RESPONSE, 0x0564},
            {Opcode.SMSG_CHAR_ENUM, 0x1154},
            {Opcode.SMSG_CLIENTCACHE_VERSION, 0x10EF},
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x0203},
            {Opcode.SMSG_DB_REPLY, 0x1574},
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE, 0x08E3},
            {Opcode.SMSG_MESSAGECHAT, 0x0E09},
            {Opcode.SMSG_MONSTER_MOVE, 0x019C},
            {Opcode.SMSG_NAME_QUERY_RESPONSE, 0x1667},
            {Opcode.SMSG_PLAYER_MOVE, 0x128B},
            {Opcode.SMSG_REDIRECT_CLIENT, 0x1082},
            {Opcode.SMSG_UPDATE_OBJECT, 0x03EF}
        };
    }
}

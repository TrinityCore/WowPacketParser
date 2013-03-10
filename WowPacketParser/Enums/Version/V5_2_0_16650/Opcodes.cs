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
            {Opcode.CMSG_DB_QUERY_BULK, 0x006A},
            {Opcode.CMSG_GAMEOBJECT_QUERY, 0x0118},
            {Opcode.CMSG_LOAD_SCREEN, 0x045D},
            {Opcode.CMSG_QUEST_QUERY, 0x001F},
            {Opcode.CMSG_PLAYER_LOGIN, 0x015D},
            {Opcode.CMSG_VIOLENCE_LEVEL, 0x10AC},
            {Opcode.CMSG_WARDEN_DATA, 0x007C},
            {Opcode.SMSG_ADDON_INFO, 0x1130},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x0C0B},
            {Opcode.SMSG_AUTH_RESPONSE, 0x0101},
            {Opcode.SMSG_CHAR_ENUM, 0x01B4},
            {Opcode.SMSG_CLIENTCACHE_VERSION, 0x0501},
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x01B5},
            {Opcode.SMSG_DB_REPLY, 0x0411},
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE, 0x1024},
            {Opcode.SMSG_QUEST_QUERY_RESPONSE, 0x19C5},
            {Opcode.SMSG_REALM_SPLIT, 0x0274},
            {Opcode.SMSG_TUTORIAL_FLAGS, 0x0B97},
            {Opcode.SMSG_UPDATE_OBJECT, 0x1406},
            {Opcode.SMSG_WARDEN_DATA, 0x0381},
        };
    }
}

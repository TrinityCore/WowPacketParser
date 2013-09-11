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
            {Opcode.CMSG_CREATURE_QUERY, 0x1585},
            {Opcode.CMSG_GAMEOBJECT_QUERY, 0x15A4},
            {Opcode.CMSG_LOG_DISCONNECT, 0x0380},
            {Opcode.CMSG_TIME_SYNC_RESP, 0x0784}, //?
            {Opcode.CMSG_UNKNOWN_903, 0x0387},
            {Opcode.SMSG_ADDON_INFO, 0x0128},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x016A},
            {Opcode.SMSG_AUTH_RESPONSE, 0x090E},
            {Opcode.SMSG_CHAR_ENUM, 0x0193},
            {Opcode.SMSG_CLIENTCACHE_VERSION, 0x0825},
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x01B4},
            {Opcode.SMSG_DB_REPLY, 0x0025},
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE, 0x0015},
            {Opcode.SMSG_MESSAGECHAT, 0x092F},
            {Opcode.SMSG_MOTD, 0x082A},
            {Opcode.SMSG_QUEST_QUERY_RESPONSE, 0x142D},
            {Opcode.SMSG_REALM_SPLIT, 0x0099},
            {Opcode.SMSG_TUTORIAL_FLAGS, 0x1F35},
            {Opcode.SMSG_UPDATE_OBJECT, 0x17D9},
        };
    }
}

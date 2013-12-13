using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V5_4_2_17658
{
    public static class Opcodes_5_4_2
    {
        public static BiDictionary<Opcode, int> Opcodes()
        {
            return Opcs;
        }

        private static readonly BiDictionary<Opcode, int> Opcs = new BiDictionary<Opcode, int>
        {
            {Opcode.CMSG_AUTH_SESSION, 0x196E},
            {Opcode.CMSG_CHANNEL_LIST, 0x0847},
            {Opcode.CMSG_BUY_BANK_SLOT, 0x01AD},
            {Opcode.CMSG_CHAR_CREATE, 0x077B},
            {Opcode.CMSG_CHAR_DELETE, 0x067A},
            {Opcode.CMSG_CHAR_ENUM, 0x047C},
            {Opcode.CMSG_CREATURE_QUERY, 0x0C4A},
            {Opcode.CMSG_DB_QUERY_BULK, 0x0676},
            {Opcode.CMSG_GAMEOBJECT_QUERY, 0x08BC},
            {Opcode.CMSG_INSPECT, 0x0206},
            {Opcode.CMSG_LEARN_TALENT, 0x026C},
            {Opcode.CMSG_LOAD_SCREEN, 0x0650},
            {Opcode.CMSG_LOG_DISCONNECT, 0x19EA},
            {Opcode.CMSG_LOGOUT_CANCEL, 0x0EE9},
            {Opcode.CMSG_NAME_QUERY, 0x05F4},
            {Opcode.CMSG_NPC_TEXT_QUERY, 0x006C},
            {Opcode.CMSG_PING, 0x18E2},
            {Opcode.CMSG_PLAYER_LOGIN, 0x0754},
            {Opcode.CMSG_REALM_NAME_QUERY, 0x0472},
            {Opcode.CMSG_SET_ACTION_BUTTON, 0x0D5E},
            {Opcode.CMSG_SET_SELECTION, 0x0AC5},
            {Opcode.CMSG_TIME_SYNC_RESP, 0x06AB},
            {Opcode.CMSG_VIOLENCE_LEVEL, 0x0448},
            {Opcode.CMSG_WHO, 0x0CFD},
            {Opcode.SMSG_ADDON_INFO, 0x0A9C},
            {Opcode.SMSG_AUTH_RESPONSE, 0x03A8},
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x0E85},
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE, 0x08F3},
            {Opcode.SMSG_MESSAGECHAT, 0x0A5B},
            {Opcode.SMSG_UPDATE_OBJECT, 0x1C89},
        };
    }
}

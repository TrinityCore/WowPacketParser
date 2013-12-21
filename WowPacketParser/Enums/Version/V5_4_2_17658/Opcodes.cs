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
            {Opcode.CMSG_AUTH_SESSION, 0x196E}, // 17658
            {Opcode.CMSG_BUY_BANK_SLOT, 0x01AD}, // 17658
            {Opcode.CMSG_DB_QUERY_BULK, 0x0676}, // 17658
            {Opcode.CMSG_CHAR_CREATE, 0x077B}, // 17658
            {Opcode.CMSG_CHAR_DELETE, 0x067A}, // 17658
            {Opcode.CMSG_CHAR_ENUM, 0x047C}, // 17658
            {Opcode.CMSG_CREATURE_QUERY, 0x0C4A}, // 17658
            {Opcode.CMSG_GAMEOBJECT_QUERY, 0x08BC}, // 17658
            {Opcode.CMSG_INSPECT, 0x0206}, // 17658
            {Opcode.CMSG_LEARN_TALENT, 0x026C}, // 17658
            {Opcode.CMSG_LOAD_SCREEN, 0x0650}, // 17658
            {Opcode.CMSG_LOG_DISCONNECT, 0x19EA}, // 17658
            {Opcode.CMSG_LOGOUT_CANCEL, 0x0EE9}, // 17658
            {Opcode.CMSG_NAME_QUERY, 0x05F4}, // 17658
            {Opcode.CMSG_NPC_TEXT_QUERY, 0x006C}, // 17658
            {Opcode.CMSG_PING, 0x18E2}, // 17658
            {Opcode.CMSG_PLAYER_LOGIN, 0x0754}, // 17658
            {Opcode.CMSG_REALM_QUERY, 0x0472}, // 17658
            {Opcode.CMSG_SET_ACTION_BUTTON, 0x0D5E}, // 17658
            {Opcode.CMSG_SET_SELECTION, 0x0AC5}, // 17658
            {Opcode.CMSG_TIME_SYNC_RESP, 0x06AB}, // 17658
            {Opcode.CMSG_VIOLENCE_LEVEL, 0x0448}, // 17658
            {Opcode.CMSG_WHO, 0x0CFD}, // 17658
        };
    }
}

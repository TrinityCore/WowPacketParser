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
            {Opcode.CMSG_AUCTION_HELLO, 0x02A5}, // 17688
            {Opcode.CMSG_AUTH_SESSION, 0x196E}, // 17688
            {Opcode.CMSG_BANKER_ACTIVATE, 0x02E0}, // 17688
            {Opcode.CMSG_BUY_BANK_SLOT, 0x01AD}, // 17688
            {Opcode.CMSG_CHAR_CREATE, 0x077B}, // 17688
            {Opcode.CMSG_CHAR_DELETE, 0x067A}, // 17688
            {Opcode.CMSG_CHAR_ENUM, 0x047C}, // 17688
            {Opcode.CMSG_CREATURE_QUERY, 0x0C4A}, // 17688
            {Opcode.CMSG_DB_QUERY_BULK, 0x0676}, // 17688
            {Opcode.CMSG_GOSSIP_HELLO, 0x02EF}, // 17688
            {Opcode.CMSG_GAMEOBJECT_QUERY, 0x08BC}, // 17688
            {Opcode.CMSG_INSPECT, 0x0206}, // 17688
            {Opcode.CMSG_LEARN_TALENT, 0x026C}, // 17688
            {Opcode.CMSG_LIST_INVENTORY, 0x08C5}, // 17688
            {Opcode.CMSG_LOAD_SCREEN, 0x0650}, // 17688
            {Opcode.CMSG_LOG_DISCONNECT, 0x19EA}, // 17688
            {Opcode.CMSG_LOGOUT_CANCEL, 0x0EE9}, // 17688
            {Opcode.CMSG_NAME_QUERY, 0x05F4}, // 17688
            {Opcode.CMSG_NPC_TEXT_QUERY, 0x006C}, // 17688
            {Opcode.CMSG_OBJECT_UPDATE_FAILED, 0x0A95}, // 17688
            {Opcode.CMSG_PING, 0x18E2}, // 17688
            {Opcode.CMSG_PLAYER_LOGIN, 0x0754}, // 17688
            {Opcode.CMSG_RANDOMIZE_CHAR_NAME, 0x0DD1}, // 17688
            {Opcode.CMSG_READY_FOR_ACCOUNT_DATA_TIMES, 0x047F}, // 17688
            {Opcode.CMSG_REALM_QUERY, 0x0472}, // 17688
            {Opcode.CMSG_SET_ACTION_BUTTON, 0x0D5E}, // 17688
            {Opcode.CMSG_SET_SELECTION, 0x0AC5}, // 17688
            {Opcode.CMSG_TIME_SYNC_RESP, 0x06AB}, // 17688
            {Opcode.CMSG_VIOLENCE_LEVEL, 0x0448}, // 17688
            {Opcode.CMSG_WHO, 0x0CFD}, // 17688

            {Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x0899}, // 17688
            {Opcode.SMSG_AUTH_CHALLENGE, 0x0C42}, // 17688
            {Opcode.SMSG_AUTH_RESPONSE, 0x03A8}, // 17688
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x0E85}, // 17688
            {Opcode.SMSG_DB_REPLY, 0x089A}, // 17688
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE, 0x08F3}, // 17688
            {Opcode.SMSG_CHAR_CREATE, 0x0FAD}, // 17688
            {Opcode.SMSG_CHAR_DELETE, 0x0A1E}, // 17688
            {Opcode.SMSG_CHAR_ENUM, 0x08B9}, // 17688
            {Opcode.SMSG_INITIAL_SPELLS, 0x0623}, // 17688
            {Opcode.SMSG_LIST_INVENTORY, 0x0ADC}, // 17688
            {Opcode.SMSG_LOGIN_VERIFY_WORLD, 0x0E04}, // 17688
            {Opcode.SMSG_LOGOUT_COMPLETE, 0x0C6D}, // 17688
            {Opcode.SMSG_LOGOUT_RESPONSE, 0x104A}, // 17688
            {Opcode.SMSG_MOTD, 0x08BB}, // 17688
            {Opcode.SMSG_NPC_TEXT_UPDATE, 0x0877}, // 17688
            {Opcode.SMSG_RANDOMIZE_CHAR_NAME, 0x0D24}, // 17688
            {Opcode.SMSG_UPDATE_OBJECT, 0x1C89}, // 17688
        };
    }
}

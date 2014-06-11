using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V5_4_8_18291
{
    public static class Opcodes_5_4_8
    {
        public static BiDictionary<Opcode, int> Opcodes()
        {
            return Opcs;
        }

        private static readonly BiDictionary<Opcode, int> Opcs = new BiDictionary<Opcode, int>
        {
            {Opcode.CMSG_AREATRIGGER,                              0x1C44}, //
            {Opcode.CMSG_AUCTION_HELLO,                            0x0379}, //
            {Opcode.CMSG_AUTH_SESSION,                             0x00B2}, //+-
            {Opcode.CMSG_BANKER_ACTIVATE,                          0x02E9}, //
            {Opcode.CMSG_CANCEL_TRADE,                             0x1941}, //+-
            {Opcode.CMSG_CHAR_ENUM,                                0x00E0}, //++
            {Opcode.CMSG_CREATURE_QUERY,                           0x0842}, //+-
            {Opcode.CMSG_GOSSIP_HELLO,                             0x12F3}, //
            {Opcode.CMSG_LIST_INVENTORY,                           0x02D8}, //
            {Opcode.CMSG_LOAD_SCREEN,                              0x1DBD}, //+-
            {Opcode.CMSG_LOGOUT_REQUEST,                           0x1349}, //+-
            {Opcode.CMSG_PLAYED_TIME,                              0x03F6}, //+-
            {Opcode.CMSG_PLAYER_LOGIN,                             0x158F}, //+-
            {Opcode.CMSG_READY_FOR_ACCOUNT_DATA_TIMES,             0x031C}, //++
            {Opcode.CMSG_REALM_SPLIT,                              0x18B2}, //+-
            {Opcode.CMSG_REQUEST_HOTFIX,                           0x158D}, //+-
            {Opcode.CMSG_TIME_SYNC_RESP,                           0x01DB}, //+-
            {Opcode.CMSG_VIOLENCE_LEVEL,                           0x0040}, //+-
            {Opcode.CMSG_WARDEN_DATA,                              0x1816}, //+-
            {Opcode.CMSG_UNK_1A87,                                 0x1A87}, //+-
		
            {Opcode.SMSG_ACCOUNT_DATA_TIMES,                       0x162B}, //++
            {Opcode.SMSG_ADDON_INFO,                               0x160A}, //+-
            {Opcode.SMSG_AUCTION_HELLO,                            0x10A7}, //-+
            {Opcode.SMSG_AUTH_CHALLENGE,                           0x0949}, //
            {Opcode.SMSG_AUTH_RESPONSE,                            0x0ABA}, //++
            {Opcode.SMSG_CHAR_CREATE,                              0x1CAA}, // ?
            {Opcode.SMSG_CHAR_DELETE,                              0x0C9F}, // ?
            {Opcode.SMSG_CHAR_ENUM,                                0x11C3}, //+-
            {Opcode.SMSG_CLIENTCACHE_VERSION,                      0x002A}, //++
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE,                  0x048B}, //
            {Opcode.SMSG_DB_REPLY,                                 0x189E}, //+-
            //{Opcode.SMSG_FEATURE_SYSTEM_STATUS,                    0x16BB}, //?
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE,                0x06BF}, //?
            {Opcode.SMSG_INITIAL_SPELLS,                           0x045A}, //
            {Opcode.SMSG_LEARNED_SPELL,                            0x129A}, //++
            {Opcode.SMSG_LOGIN_VERIFY_WORLD,                       0x1C0F}, //
            {Opcode.SMSG_LOGOUT_RESPONSE,                          0x008F}, //+-
            {Opcode.SMSG_MOTD,                                     0x183B}, //++
            //{Opcode.SMSG_NAME_QUERY_RESPONSE,                      0x169B}, //?
            {Opcode.SMSG_PLAYER_MOVE,                              0x1A32}, //?
            {Opcode.SMSG_RANDOMIZE_CHAR_NAME,                      0x169F}, //?
            {Opcode.SMSG_REALM_NAME_QUERY_RESPONSE,                0x063E}, //?
            {Opcode.SMSG_SHOW_BANK,                                0x0007}, //-+
            {Opcode.SMSG_TIME_SYNC_REQ,                            0x1A8F}, //+-
            {Opcode.SMSG_NEW_WORLD,                                0x1C3B}, //
            {Opcode.SMSG_SERVER_TIMEZONE,                          0x19C1},  //+-
            {Opcode.SMSG_TUTORIAL_FLAGS,                           0x1B90}, //+-
            {Opcode.SMSG_TRANSFER_PENDING,                         0x061B}, //
            //{Opcode.SMSG_UPDATE_OBJECT,                            0x1792}, //
            {Opcode.SMSG_WARDEN_DATA,                              0x0C0A}, //+-
            {Opcode.SMSG_UNK_001F,                                 0x001F}, //++
            {Opcode.SMSG_UNK_00A3,                                 0x00A3}, //++
            {Opcode.SMSG_UNK_043F,                                 0x043F}, //+-
            {Opcode.SMSG_UNK_069B,                                 0x069B}, //++
            {Opcode.SMSG_UNK_0A0B,                                 0x0A0B}, //++
            {Opcode.SMSG_UNK_103B,                                 0x103B}, //+-
            {Opcode.SMSG_UNK_121B,                                 0x121B}, //++
            {Opcode.SMSG_UNK_121E,                                 0x121E}, //++
            //{Opcode.SMSG_UNK_129A,                                 0x129A}, //++
            {Opcode.SMSG_UNK_1440,                                 0x1440}, //++
            {Opcode.SMSG_UNK_1E9B,                                 0x1E9B}, //+-
        };
    }
}

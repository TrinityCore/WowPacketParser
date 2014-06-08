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
            {Opcode.CMSG_AREATRIGGER, 0x1C44}, // 18291
            {Opcode.CMSG_AUCTION_HELLO, 0x0379}, // 18291
            {Opcode.CMSG_AUTH_SESSION, 0x00B2}, // 18291+-
            {Opcode.CMSG_BANKER_ACTIVATE, 0x02E9}, // 18291
            {Opcode.CMSG_CHAR_ENUM, 0x00E0}, // 18291++
            {Opcode.CMSG_GOSSIP_HELLO, 0x12F3}, // 18291
            {Opcode.CMSG_LIST_INVENTORY, 0x02D8}, // 18291
            {Opcode.CMSG_READY_FOR_ACCOUNT_DATA_TIMES, 0x031C}, // 18291++
            {Opcode.CMSG_REALM_SPLIT, 0x18B2}, // 18291+-
            {Opcode.CMSG_WARDEN_DATA, 0x1816}, // 18291+-
            {Opcode.CMSG_UNK_1A87, 0x1A87}, // 18291+-
		
            {Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x162B}, // 18291++
            {Opcode.SMSG_ADDON_INFO, 0x160A}, // 18291+-
            {Opcode.SMSG_AUCTION_HELLO, 0x10A7}, // 18291
            {Opcode.SMSG_AUTH_CHALLENGE, 0x0949}, // 18291
            {Opcode.SMSG_AUTH_RESPONSE, 0x0ABA}, // 18291++
            {Opcode.SMSG_CHAR_CREATE, 0x1CAA}, // 18291 ?
            {Opcode.SMSG_CHAR_DELETE, 0x0C9F}, // 18291 ?
            {Opcode.SMSG_CHAR_ENUM, 0x11C3}, // 18291+-
            {Opcode.SMSG_CLIENTCACHE_VERSION, 0x002A}, // 18291++
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x048B}, // 18291
            {Opcode.SMSG_DB_REPLY, 0x189E}, // 18291+-
            //{Opcode.SMSG_FEATURE_SYSTEM_STATUS, 0x16BB}, // 18291?
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE, 0x06BF}, // 18291?
            {Opcode.SMSG_INITIAL_SPELLS, 0x045A}, // 18291
            {Opcode.SMSG_LOGIN_VERIFY_WORLD, 0x1C0F}, // 18291
            {Opcode.SMSG_MOTD, 0x183B}, // 18291++
            //{Opcode.SMSG_NAME_QUERY_RESPONSE, 0x169B}, // 18291?
            {Opcode.SMSG_PLAYER_MOVE, 0x1A32}, // 18291?
            {Opcode.SMSG_RANDOMIZE_CHAR_NAME, 0x169F}, // 18291?
            {Opcode.SMSG_REALM_QUERY_RESPONSE, 0x063E}, // 18291?
            {Opcode.SMSG_SHOW_BANK, 0x0007}, // 18291
            {Opcode.SMSG_NEW_WORLD, 0x1C3B},  // 18291
            {Opcode.SMSG_SERVER_TIMEZONE, 0x19C1},  // 18291+-
            {Opcode.SMSG_TUTORIAL_FLAGS, 0x1B90}, // 18291+-
            {Opcode.SMSG_TRANSFER_PENDING, 0x061B}, // 18291
            //{Opcode.SMSG_UPDATE_OBJECT, 0x1792}, // 18291
            {Opcode.SMSG_WARDEN_DATA, 0x0C0A}, // 18291+-
            {Opcode.SMSG_UNK_001F, 0x001F}, // 18291++
            {Opcode.SMSG_UNK_00A3, 0x00A3}, // 18291++
            {Opcode.SMSG_UNK_043F, 0x043F}, // 18291+-
            {Opcode.SMSG_UNK_069B, 0x069B}, // 18291++
            {Opcode.SMSG_UNK_0A0B, 0x0A0B}, // 18291++
            {Opcode.SMSG_UNK_103B, 0x103B}, // 18291+-
            {Opcode.SMSG_UNK_121B, 0x121B}, // 18291++
            {Opcode.SMSG_UNK_121E, 0x121E}, // 18291++
            {Opcode.SMSG_UNK_129A, 0x129A}, // 18291++
            {Opcode.SMSG_UNK_1440, 0x1440}, // 18291++
            {Opcode.SMSG_UNK_1E9B, 0x1E9B}, // 18291+-
        };
    }
}

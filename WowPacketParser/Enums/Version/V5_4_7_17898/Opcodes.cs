using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V5_4_7_17898
{
    public static class Opcodes_5_4_7
    {
        public static BiDictionary<Opcode, int> Opcodes()
        {
            return Opcs;
        }

        private static readonly BiDictionary<Opcode, int> Opcs = new BiDictionary<Opcode, int>
        {
            {Opcode.CMSG_AUTH_SESSION, 0x1A51},
            {Opcode.CMSG_CREATURE_QUERY, 0x1E72},
            {Opcode.CMSG_DB_QUERY_BULK, 0x16C2},
            {Opcode.CMSG_GUILD_QUERY, 0x1280},
            {Opcode.CMSG_REDIRECT_AUTH_PROOF, 0x1A5B},
            {Opcode.CMSG_VIOLENCE_LEVEL, 0x05A0},

            {Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x0F40},
            {Opcode.SMSG_ACTION_BUTTONS, 0x1768},
            {Opcode.SMSG_ADDON_INFO, 0x10E2},
            {Opcode.SMSG_ALL_ACHIEVEMENT_DATA_ACCOUNT, 0x13F0},
            {Opcode.SMSG_ARENA_SEASON_WORLD_STATE, 0x00E1},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x14B8},
            {Opcode.SMSG_AUTH_RESPONSE, 0x15A0},
            {Opcode.SMSG_BINDPOINTUPDATE, 0x11E2},
            {Opcode.SMSG_CHAR_ENUM, 0x040A},
            {Opcode.SMSG_CLIENTCACHE_VERSION, 0x1E41},
            {Opcode.SMSG_CORPSE_RECLAIM_DELAY, 0x1E73},
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x00E0},
            {Opcode.SMSG_CRITERIA_UPDATE_ACCOUNT, 0x12F9},
            {Opcode.SMSG_CRITERIA_UPDATE_PLAYER, 0x13B2},
            {Opcode.SMSG_DB_REPLY, 0x1F01},
            {Opcode.SMSG_EQUIPMENT_SET_LIST, 0x1520},
            {Opcode.SMSG_FEATURE_SYSTEM_STATUS, 0x1560},
            {Opcode.SMSG_FORCE_SEND_QUEUED_PACKETS, 0x01B9},
            {Opcode.SMSG_INITIALIZE_FACTIONS, 0x11E1},
            {Opcode.SMSG_INITIAL_SPELLS, 0x1B05},
            {Opcode.SMSG_INIT_CURRENCY, 0x1E3A},
            {Opcode.SMSG_INIT_WORLD_STATES, 0x0F03},
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE, 0x066A},
            {Opcode.SMSG_GUILD_NEWS_TEXT, 0x1850},
            {Opcode.SMSG_GUILD_QUERY_RESPONSE, 0x1953},
            {Opcode.SMSG_GUILD_RANK, 0x1271},
            {Opcode.SMSG_LEARNED_SPELL, 0x0C99},
            {Opcode.SMSG_LOGIN_VERIFY_WORLD, 0x0603},
            {Opcode.SMSG_LOGIN_SETTIMESPEED, 0x0F4A},
            {Opcode.SMSG_MESSAGECHAT, 0x0E60},
            {Opcode.SMSG_MOTD, 0x0E20},
            {Opcode.SMSG_NAME_QUERY_RESPONSE, 0x1E5B},
            {Opcode.SMSG_NEW_WORLD, 0x13F3},
            {Opcode.SMSG_REDIRECT_CLIENT, 0x05B9},
            {Opcode.SMSG_REMOVED_SPELL, 0x05E3},
            {Opcode.SMSG_PLAYER_MOVE, 0x1CB2},
            {Opcode.SMSG_SEND_SERVER_LOCATION, 0x0C2B},
            {Opcode.SMSG_SEND_UNLEARN_SPELLS, 0x1B3E},
            {Opcode.SMSG_SET_FLAT_SPELL_MODIFIER, 0x0179},
            {Opcode.SMSG_SET_PCT_SPELL_MODIFIER, 0x193C},
            {Opcode.SMSG_SET_PHASE_SHIFT, 0x1D52},
            {Opcode.SMSG_SET_PROFICIENCY, 0x1E3B},
            {Opcode.SMSG_SPELL_CATEGORY_COOLDOWN, 0x053B},
            {Opcode.SMSG_TALENTS_INFO, 0x0C68},
            {Opcode.SMSG_TIME_SYNC_REQ, 0x12F1},
            {Opcode.SMSG_TUTORIAL_FLAGS, 0x10A7},
            {Opcode.SMSG_UPDATE_OBJECT, 0x1725},
            {Opcode.SMSG_UPDATE_WORLD_STATE, 0x1D13},
            {Opcode.SMSG_WARDEN_DATA, 0x14EB},
            {Opcode.SMSG_WEATHER, 0x0F41},
            {Opcode.SMSG_WEEKLY_SPELL_USAGE, 0x1D04},
            {Opcode.SMSG_WORLD_SERVER_INFO, 0x1D01},
        };
    }
}

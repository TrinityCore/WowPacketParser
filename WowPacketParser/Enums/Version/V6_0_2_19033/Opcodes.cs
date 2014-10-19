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
            {Opcode.CMSG_CREATURE_QUERY, 0x14D6},
            {Opcode.CMSG_GAMEOBJECT_QUERY, 0x0D97},
            {Opcode.CMSG_NAME_QUERY, 0x0BA4},
            {Opcode.CMSG_PING, 0x1B75},
            {Opcode.CMSG_REDIRECT_AUTH_PROOF, 0x1806},
            {Opcode.CMSG_WARDEN_DATA, 0x00F3},

            {Opcode.SMSG_ACTION_BUTTONS, 0x03F4},
            {Opcode.SMSG_ADDON_INFO, 0x1400},
            {Opcode.SMSG_ALL_ACHIEVEMENT_DATA_ACCOUNT, 0x1603},
            {Opcode.SMSG_ALL_ACHIEVEMENT_DATA_PLAYER, 0x01A4},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x10AA},
            {Opcode.SMSG_AUTH_RESPONSE, 0x0564},
            {Opcode.SMSG_BINDPOINTUPDATE, 0x1428},
            {Opcode.SMSG_CHAR_ENUM, 0x1154},
            {Opcode.SMSG_CLIENTCACHE_VERSION, 0x10EF},
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x0203},
            {Opcode.SMSG_CORPSE_RECLAIM_DELAY, 0x11C0},
            {Opcode.SMSG_DB_REPLY, 0x1574},
            {Opcode.SMSG_EQUIPMENT_SET_LIST, 0x18E2},
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE, 0x08E3},
            {Opcode.SMSG_GUILD_QUERY_RESPONSE, 0x034A},
            {Opcode.SMSG_INIT_CURRENCY, 0x00A4},
            {Opcode.SMSG_INIT_WORLD_STATES, 0x0BB7},
            {Opcode.SMSG_INITIAL_SPELLS, 0x0297},
            {Opcode.SMSG_LEARNED_SPELL, 0x02D8},
            {Opcode.SMSG_MESSAGECHAT, 0x0E09},
            {Opcode.SMSG_MOTD, 0x0E5D},
            {Opcode.SMSG_MONSTER_MOVE, 0x019C},
            {Opcode.SMSG_NAME_QUERY_RESPONSE, 0x1667},
            {Opcode.SMSG_PLAYER_MOVE, 0x128B},
            {Opcode.SMSG_PONG, 0x1881},
            {Opcode.SMSG_REDIRECT_CLIENT, 0x1082},
            {Opcode.SMSG_SEND_SERVER_LOCATION, 0x1257},
            {Opcode.SMSG_ARENA_SEASON_WORLD_STATE, 0x0618},
            {Opcode.SMSG_SET_FLAT_SPELL_MODIFIER, 0x07B3},
            {Opcode.SMSG_SET_PCT_SPELL_MODIFIER, 0x12D4},
            {Opcode.SMSG_SET_PROFICIENCY, 0x12AF},
            {Opcode.SMSG_TALENTS_INFO, 0x10FF},
            {Opcode.SMSG_TIME_SYNC_REQ, 0x0CA8},
            {Opcode.SMSG_TUTORIAL_FLAGS, 0x0617},
            {Opcode.SMSG_SEND_UNLEARN_SPELLS, 0x07DB},
            {Opcode.SMSG_UPDATE_OBJECT, 0x03EF},
            {Opcode.SMSG_WARDEN_DATA, 0x12EF},
            {Opcode.SMSG_WEATHER, 0x01BF},
        };
    }
}

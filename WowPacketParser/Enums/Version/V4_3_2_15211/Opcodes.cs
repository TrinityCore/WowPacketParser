using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V4_3_2_15211
{
    public static class Opcodes_4_3_2
    {
        public static BiDictionary<Opcode, int> Opcodes(Direction direction)
        {
            if (direction == Direction.ClientToServer || direction == Direction.BNClientToServer)
                return ClientOpcodes;
            if (direction == Direction.ServerToClient || direction == Direction.BNServerToClient)
                return ServerOpcodes;
            return MiscOpcodes;
        }

        private static readonly BiDictionary<Opcode, int> ClientOpcodes = new BiDictionary<Opcode, int>
        {
            {Opcode.CMSG_ACCEPT_LEVEL_GRANT, 0x0D33},
            {Opcode.CMSG_ACTIVATE_TAXI, 0x4DF9},
            {Opcode.CMSG_ACTIVATE_TAXI_EXPRESS, 0x4D0F},
            {Opcode.CMSG_ADDON_REGISTERED_PREFIXES, 0x2860},
            {Opcode.CMSG_ADD_FRIEND, 0x2DBD},
            {Opcode.CMSG_ADD_IGNORE, 0x2541},
            {Opcode.CMSG_AUTH_CONTINUED_SESSION, 0x004A},
            {Opcode.CMSG_AUTH_SESSION, 0x4042},
            {Opcode.CMSG_BANKER_ACTIVATE, 0x0569},
            {Opcode.CMSG_BINDER_ACTIVATE, 0x6DC7},
            {Opcode.CMSG_CAST_SPELL, 0x65E1},
            {Opcode.CMSG_CREATE_CHARACTER, 0x45FF},
            {Opcode.CMSG_CHAR_CUSTOMIZE, 0x0DA1},
            {Opcode.CMSG_CHAR_DELETE, 0x05F5},
            {Opcode.CMSG_ENUM_CHARACTERS, 0x4051},
            {Opcode.CMSG_CHAR_FACTION_CHANGE, 0x250D},
            {Opcode.CMSG_CHAR_RACE_CHANGE, 0x2D45},
            {Opcode.CMSG_CHARACTER_RENAME_REQUEST, 0x2DDB},
            {Opcode.CMSG_CLEAR_CHANNEL_WATCH, 0x4D01},
            {Opcode.CMSG_QUERY_CREATURE, 0x2591},
            {Opcode.CMSG_DEL_FRIEND, 0x0D79},
            {Opcode.CMSG_DEL_IGNORE, 0x0DAB},
            {Opcode.CMSG_QUERY_GAME_OBJECT, 0x4523},
            {Opcode.CMSG_GOSSIP_HELLO, 0x6593},
            {Opcode.CMSG_GOSSIP_SELECT_OPTION, 0x6589},
            {Opcode.CMSG_GUILD_BANK_ACTIVATE, 0x0563},
            {Opcode.CMSG_CHAT_JOIN_CHANNEL, 0x2241},
            {Opcode.CMSG_CHAT_LEAVE_CHANNEL, 0x2800},
            {Opcode.CMSG_LOGOUT_CANCEL, 0x2509},
            {Opcode.CMSG_LOG_DISCONNECT, 0x4019},
            {Opcode.CMSG_CHAT_MESSAGE_CHANNEL, 0x28C1},
            {Opcode.CMSG_CHAT_MESSAGE_SAY, 0x22E0},
            {Opcode.CMSG_CHAT_MESSAGE_YELL, 0x2260},
            {Opcode.CMSG_NAME_QUERY, 0x2DA3},
            {Opcode.CMSG_QUERY_NPC_TEXT, 0x4DF3},
            {Opcode.CMSG_PLAYER_VEHICLE_ENTER, 0x658F},
            {Opcode.CMSG_PLAY_DANCE, 0x0D93},
            {Opcode.CMSG_QUERY_TIME, 0x257F},
            {Opcode.CMSG_QUEST_GIVER_HELLO, 0x0D6D},
            {Opcode.CMSG_READY_FOR_ACCOUNT_DATA_TIMES, 0x452B},
            {Opcode.CMSG_REALM_SPLIT, 0x0DB7},
            {Opcode.CMSG_REQUEST_CEMETERY_LIST, 0x03A4},
            {Opcode.CMSG_TRAINER_LIST, 0x4DD5},
            {Opcode.CMSG_UNLEARN_SKILL, 0x2DC7},
            {Opcode.CMSG_USE_ITEM, 0x2549}
        };

        private static readonly BiDictionary<Opcode, int> ServerOpcodes = new BiDictionary<Opcode, int>
        {
            {Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x058B},
            {Opcode.SMSG_ADDON_INFO, 0x6D8D},
            {Opcode.SMSG_ATTACKER_STATE_UPDATE, 0x2D59},
            {Opcode.SMSG_AURA_UPDATE, 0x4D1B},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x0129},
            {Opcode.SMSG_AUTH_RESPONSE, 0x0E54},
            {Opcode.SMSG_CHANNEL_NOTIFY, 0x65A9},
            {Opcode.SMSG_ENUM_CHARACTERS_RESULT, 0x0CF5},
            {Opcode.SMSG_CACHE_VERSION, 0x453D},
            {Opcode.SMSG_CHAT, 0x0529},
            {Opcode.SMSG_CONTACT_LIST, 0x0DAF},
            {Opcode.SMSG_QUERY_CREATURE_RESPONSE, 0x25FB},
            {Opcode.SMSG_DB_REPLY, 0x06D1},
            {Opcode.SMSG_DEFENSE_MESSAGE, 0x456F},
            {Opcode.SMSG_EMOTE, 0x65F9},
            {Opcode.SMSG_FLIGHT_SPLINE_SYNC, 0x6533},
            {Opcode.SMSG_GAME_OBJECT_CUSTOM_ANIM, 0x4543},
            {Opcode.SMSG_QUERY_GAME_OBJECT_RESPONSE, 0x253D},
            {Opcode.SMSG_GOSSIP_COMPLETE, 0x4575},
            {Opcode.SMSG_GOSSIP_MESSAGE, 0x25B7},
            {Opcode.SMSG_GOSSIP_POI, 0x6565},
            {Opcode.SMSG_GUILD_EVENT, 0x65F5},
            {Opcode.SMSG_QUERY_GUILD_INFO_RESPONSE, 0x2D33},
            {Opcode.SMSG_GUILD_RANKS, 0x1EB1}, // Not confirmed
            {Opcode.SMSG_GUILD_ROSTER, 0x1E94}, // Not confirmed
            {Opcode.SMSG_HIGHEST_THREAT_UPDATE, 0x6527},
            {Opcode.SMSG_SEND_KNOWN_SPELLS, 0x65D3},
            {Opcode.SMSG_INIT_WORLD_STATES, 0x0D25},
            {Opcode.SMSG_LEARNED_SPELL, 0x1611},
            {Opcode.SMSG_LOAD_CUF_PROFILES, 0x1494},
            {Opcode.SMSG_LOAD_EQUIPMENT_SET, 0x4D9B},
            {Opcode.SMSG_LOGIN_VERIFY_WORLD, 0x0DEB},
            {Opcode.SMSG_MIRROR_IMAGE_COMPONENTED_DATA, 0x457D},
            {Opcode.SMSG_MONSTER_MOVE_TRANSPORT, 0x65D5},
            {Opcode.SMSG_MOTD, 0x6D11},
            {Opcode.SMSG_QUERY_PLAYER_NAME_RESPONSE, 0x455D},
            {Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE, 0x0D09},
            {Opcode.SMSG_ON_MONSTER_MOVE, 0x2561},
            {Opcode.SMSG_PLAYER_VEHICLE_DATA, 0x2503},
            {Opcode.SMSG_PLAY_DANCE, 0x4D2F},
            {Opcode.SMSG_PLAY_MUSIC, 0x256D},
            {Opcode.SMSG_PLAY_OBJECT_SOUND, 0x451F},
            {Opcode.SMSG_PLAY_SOUND, 0x2DE5},
            {Opcode.SMSG_QUERY_TIME_RESPONSE, 0x053F},
            {Opcode.SMSG_QUEST_GIVER_QUEST_DETAILS, 0x0D91},
            {Opcode.SMSG_QUEST_POI_QUERY_RESPONSE, 0x05C1},
            {Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE, 0x25CB},
            {Opcode.SMSG_REALM_SPLIT, 0x0581},
            {Opcode.SMSG_CONNECT_TO, 0x1329},
            {Opcode.SMSG_REQUEST_CEMETERY_LIST_RESPONSE, 0x0405},
            {Opcode.SMSG_SPELL_ENERGIZE_LOG, 0x05EF},
            {Opcode.SMSG_SPELL_EXECUTE_LOG, 0x4569},
            {Opcode.SMSG_SPELL_GO, 0x6DEF},
            {Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG, 0x250F},
            {Opcode.SMSG_SPELL_START, 0x6D27},
            {Opcode.SMSG_STAND_STATE_UPDATE, 0x25DF},
            {Opcode.SMSG_THREAT_CLEAR, 0x6DA1},
            {Opcode.SMSG_THREAT_REMOVE, 0x0DD7},
            {Opcode.SMSG_THREAT_UPDATE, 0x05BD},
            {Opcode.SMSG_TRAINER_LIST, 0x6D85},
            {Opcode.SMSG_TUTORIAL_FLAGS, 0x4D8F},
            {Opcode.SMSG_UNLEARNED_SPELLS, 0x0501},
            {Opcode.SMSG_UPDATE_OBJECT, 0x0D63},
            {Opcode.SMSG_UPDATE_WORLD_STATE, 0x45E5},
            {Opcode.SMSG_WARDEN_DATA, 0x0CF0}
        };

        private static readonly BiDictionary<Opcode, int> MiscOpcodes = new BiDictionary<Opcode, int>
        {
            {Opcode.MSG_MOVE_HEARTBEAT, 0x2B81},
            {Opcode.MSG_TABARDVENDOR_ACTIVATE, 0x05FB}
        };
    }
}

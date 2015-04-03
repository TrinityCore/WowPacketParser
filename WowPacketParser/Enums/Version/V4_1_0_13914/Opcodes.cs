using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V4_1_0_13914
{
    public static class Opcodes_4_1_0
    {
        public static BiDictionary<Opcode, int> Opcodes(Direction direction)
        {
            if (direction == Direction.ClientToServer || direction == Direction.BNClientToServer)
                return ClientOpcodes;
            if (direction == Direction.ServerToClient || direction == Direction.BNServerToClient)
                return ServerOpcodes;
            return MiscOpcodes;
        }

        private static readonly BiDictionary<Opcode, int> ClientOpcodes = new BiDictionary<Opcode, int>();

        private static readonly BiDictionary<Opcode, int> ServerOpcodes = new BiDictionary<Opcode, int>
        {
            {Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x0B86D}, // 4.1.0 13914
            {Opcode.SMSG_ACHIEVEMENT_DELETED, 0x03A6D}, // 4.1.0 13914
            {Opcode.SMSG_ACHIEVEMENT_EARNED, 0x0EA6C}, // 4.1.0 13914
            {Opcode.SMSG_AI_REACTION, 0x00B6E}, // 4.1.0 13914
            {Opcode.SMSG_ARENA_ERROR, 0x0787F}, // 4.1.0 13914
            {Opcode.SMSG_ARENA_OPPONENT_UPDATE, 0x0E96F}, // 4.1.0 13914
            {Opcode.SMSG_ARENA_TEAM_EVENT, 0x03A6E}, // 4.1.0 13914
            {Opcode.SMSG_ARENA_TEAM_INVITE, 0x07B3D}, // 4.1.0 13914
            {Opcode.SMSG_ARENA_TEAM_QUERY_RESPONSE, 0x0982D}, // 4.1.0 13914
            {Opcode.SMSG_ARENA_TEAM_ROSTER, 0x0ED6E}, // 4.1.0 13914
            {Opcode.SMSG_AUCTION_BIDDER_NOTIFICATION, 0x01E7C}, // 4.1.0 13914
            {Opcode.SMSG_AUCTION_COMMAND_RESULT, 0x00F3D}, // 4.1.0 13914
            {Opcode.SMSG_AUCTION_LIST_BIDDER_ITEMS_RESULT, 0x0FD6C}, // 4.1.0 13914
            {Opcode.SMSG_AUCTION_LIST_OWNER_ITEMS_RESULT, 0x01A6D}, // 4.1.0 13914
            {Opcode.SMSG_AUCTION_LIST_PENDING_SALES, 0x0AF3C}, // 4.1.0 13914
            {Opcode.SMSG_AUCTION_LIST_RESULT, 0x05D7C}, // 4.1.0 13914
            {Opcode.SMSG_AUCTION_OWNER_NOTIFICATION, 0x00D2C}, // 4.1.0 13914
            {Opcode.SMSG_AUCTION_REMOVED_NOTIFICATION, 0x0ED3D}, // 4.1.0 13914
            {Opcode.SMSG_AURA_UPDATE, 0xE86C},
            {Opcode.SMSG_AVAILABLE_VOICE_CHANNEL, 0x04C7E}, // 4.1.0 13914
            {Opcode.SMSG_BARBER_SHOP_RESULT, 0x0FF2C}, // 4.1.0 13914
            {Opcode.SMSG_BATTLEGROUND_INFO_THROTTLED, 0x02C6D}, // 4.1.0 13914
            {Opcode.SMSG_BINDER_CONFIRM, 0x0CB6C}, // 4.1.0 13914
            {Opcode.SMSG_CALENDAR_COMMAND_RESULT, 0x0BC7C}, // 4.1.0 13914
            {Opcode.SMSG_CANCEL_AUTO_REPEAT, 0x0986E}, // 4.1.0 13914
            {Opcode.SMSG_CANCEL_COMBAT, 0x00C6E}, // 4.1.0 13914
            {Opcode.SMSG_CAST_FAILED, 0x02C2C}, // 4.1.0 13914
            {Opcode.SMSG_CHANNEL_LIST, 0x09E2E}, // 4.1.0 13914
            {Opcode.SMSG_CHANNEL_MEMBER_COUNT, 0x03C3D}, // 4.1.0 13914
            {Opcode.SMSG_CHANNEL_NOTIFY, 0x02E2E}, // 4.1.0 13914
            {Opcode.SMSG_CHAT, 0x0DD7C}, // 4.1.0 13914
            {Opcode.SMSG_CHAT_PLAYER_AMBIGUOUS, 0x0FA3E}, // 4.1.0 13914
            {Opcode.SMSG_CHAT_PLAYER_NOTFOUND, 0x01E2D}, // 4.1.0 13914
            {Opcode.SMSG_CHAT_RESTRICTED, 0x07E7C}, // 4.1.0 13914
            {Opcode.SMSG_CONTROL_UPDATE, 0x0796C}, // 4.1.0 13914
            {Opcode.SMSG_COMBAT_LOG_MULTIPLE, 0x05D7E}, // 4.1.0 13914
            {Opcode.SMSG_COMMENTATOR_MAP_INFO, 0x05E2F}, // 4.1.0 13914
            {Opcode.SMSG_COMMENTATOR_PLAYER_INFO, 0x08E3C}, // 4.1.0 13914
            {Opcode.SMSG_COMMENTATOR_STATE_CHANGED, 0x0482F}, // 4.1.0 13914
            {Opcode.SMSG_COMPLAINT_RESULT, 0x06E6C}, // 4.1.0 13914
            {Opcode.SMSG_COMPRESSED_MOVES, 0x07F2F}, // 4.1.0 13914
            {Opcode.SMSG_COMPRESSED_UPDATE_OBJECT, 0xAE3F}, // 4.1.0 13914
            {Opcode.SMSG_CONTACT_LIST, 0x0CC2D}, // 4.1.0 13914
            {Opcode.SMSG_COOLDOWN_CHEAT, 0x0CE2E}, // 4.1.0 13914
            {Opcode.SMSG_QUERY_CREATURE_RESPONSE, 0x03E3C}, // 4.1.0 13914
            {Opcode.SMSG_CRITERIA_DELETED, 0x0883E}, // 4.1.0 13914
            {Opcode.SMSG_CRITERIA_UPDATE, 0x02D6C}, // 4.1.0 13914
            {Opcode.SMSG_CROSSED_INEBRIATION_THRESHOLD, 0x08D3E}, // 4.1.0 13914
            {Opcode.SMSG_DAMAGE_CALC_LOG, 0x05E2D}, // 4.1.0 13914
            {Opcode.SMSG_DANCE_QUERY_RESPONSE, 0x01E3E}, // 4.1.0 13914
            {Opcode.SMSG_DB_REPLY, 0x47CD}, // 4.1.0 13914
            {Opcode.SMSG_DEATH_RELEASE_LOC, 0x03C7D}, // 4.1.0 13914
            {Opcode.SMSG_DEFENSE_MESSAGE, 0x05C3E}, // 4.1.0 13914
            {Opcode.SMSG_DESTROY_OBJECT, 0x03C6F}, // 4.1.0 13914
            {Opcode.SMSG_DESTRUCTIBLE_BUILDING_DAMAGE, 0x0A83C}, // 4.1.0 13914
            {Opcode.SMSG_DISMOUNT, 0x0B86E}, // 4.1.0 13914
            {Opcode.SMSG_DISPEL_FAILED, 0x00C7E}, // 4.1.0 13914
            {Opcode.SMSG_DUEL_COMPLETE, 0x01C7F}, // 4.1.0 13914
            {Opcode.SMSG_DUEL_COUNTDOWN, 0x03D2D}, // 4.1.0 13914
            {Opcode.SMSG_DUEL_REQUESTED, 0x08D6F}, // 4.1.0 13914
            {Opcode.SMSG_DUEL_WINNER, 0x03D3C}, // 4.1.0 13914
            {Opcode.SMSG_DURABILITY_DAMAGE_DEATH, 0x02A2D}, // 4.1.0 13914
            {Opcode.SMSG_ECHO_PARTY_SQUELCH, 0x03F2C}, // 4.1.0 13914
            {Opcode.SMSG_EMOTE, 0x0AD7C}, // 4.1.0 13914
            {Opcode.SMSG_ENABLE_BARBER_SHOP, 0x0D97E}, // 4.1.0 13914
            {Opcode.SMSG_ENCHANTMENT_LOG, 0x0BB2F}, // 4.1.0 13914
            {Opcode.SMSG_EQUIPMENT_SET_ID, 0x01A7F}, // 4.1.0 13914
            {Opcode.SMSG_EXPECTED_SPAM_RECORDS, 0x01E3F}, // 4.1.0 13914
            {Opcode.SMSG_EXPLORATION_EXPERIENCE, 0x06B3F}, // 4.1.0 13914
            {Opcode.SMSG_FEATURE_SYSTEM_STATUS, 0x0CC6C}, // 4.1.0 13914
            {Opcode.SMSG_FEIGN_DEATH_RESISTED, 0x0C86E}, // 4.1.0 13914
            {Opcode.SMSG_FLIGHT_SPLINE_SYNC, 0x0B92E}, // 4.1.0 13914
            {Opcode.SMSG_FORCEACTIONSHOW, 0x01A2D}, // 4.1.0 13914
            {Opcode.SMSG_FORCED_DEATH_UPDATE, 0x0DC3C}, // 4.1.0 13914
            {Opcode.SMSG_FORCE_DISPLAY_UPDATE, 0x06F7D}, // 4.1.0 13914
            {Opcode.SMSG_FRIEND_STATUS, 0x08C2C}, // 4.1.0 13914
            {Opcode.SMSG_GAMEOBJECT_DESPAWN_ANIM, 0x00A7D}, // 4.1.0 13914
            {Opcode.SMSG_QUERY_GAME_OBJECT_RESPONSE, 0x0AB7C}, // 4.1.0 13914
            {Opcode.SMSG_GAME_OBJECT_RESET_STATE, 0x0B97F}, // 4.1.0 13914
            {Opcode.SMSG_GAME_SPEED_SET, 0x09A2F}, // 4.1.0 13914
            {Opcode.SMSG_GAME_TIME_SET, 0x09F7D}, // 4.1.0 13914
            {Opcode.SMSG_GAME_TIME_UPDATE, 0x0982C}, // 4.1.0 13914
            {Opcode.SMSG_GM_MESSAGECHAT, 0x06C3D}, // 4.1.0 13914
            {Opcode.SMSG_GOSSIP_COMPLETE, 0x0486E}, // 4.1.0 13914
            {Opcode.SMSG_GOSSIP_MESSAGE, 0x0BE3C}, // 4.1.0 13914
            {Opcode.SMSG_GOSSIP_POI, 0x0B93D}, // 4.1.0 13914
            {Opcode.SMSG_GROUP_CANCEL, 0x0486D}, // 4.1.0 13914
            {Opcode.SMSG_GROUP_DECLINE, 0x0AE7F}, // 4.1.0 13914
            {Opcode.SMSG_GROUP_DESTROYED, 0x0D97C}, // 4.1.0 13914
            {Opcode.SMSG_GROUP_INVITE, 0x0AC2C}, // 4.1.0 13914
            {Opcode.SMSG_GROUP_SET_LEADER, 0x0AD2E}, // 4.1.0 13914
            {Opcode.SMSG_GROUP_UNINVITE, 0x03A3F}, // 4.1.0 13914
            {Opcode.SMSG_GUILD_BANK_QUERY_RESULTS, 0x09C6E}, // 4.1.0 13914
            {Opcode.SMSG_GUILD_COMMAND_RESULT, 0x0FF7D}, // 4.1.0 13914
            {Opcode.SMSG_GUILD_DECLINE, 0x0087D}, // 4.1.0 13914
            {Opcode.SMSG_GUILD_EVENT, 0x0BC2D}, // 4.1.0 13914
            {Opcode.SMSG_GUILD_INFO, 0x03D6C}, // 4.1.0 13914
            {Opcode.SMSG_QUERY_GUILD_INFO_RESPONSE, 0x0796E}, // 4.1.0 13914
            {Opcode.SMSG_INITIALIZE_FACTIONS, 0x0F87E}, // 4.1.0 13914
            {Opcode.SMSG_SEND_KNOWN_SPELLS, 0x00A6F}, // 4.1.0 13914
            {Opcode.SMSG_INSPECT_RESULTS_UPDATE, 0x04D7F}, // 4.1.0 13914
            {Opcode.SMSG_INSPECT_TALENT, 0x00F3E}, // 4.1.0 13914
            {Opcode.SMSG_INSTANCE_RESET, 0x0083E}, // 4.1.0 13914
            {Opcode.SMSG_INSTANCE_RESET_FAILED, 0x00B7F}, // 4.1.0 13914
            {Opcode.SMSG_INSTANCE_SAVE_CREATED, 0x05F7E}, // 4.1.0 13914
            {Opcode.SMSG_INVALIDATE_DANCE, 0x0DE6F}, // 4.1.0 13914
            {Opcode.SMSG_INVALIDATE_PLAYER, 0x0E83D}, // 4.1.0 13914
            {Opcode.SMSG_INVENTORY_CHANGE_FAILURE, 0x08D2F}, // 4.1.0 13914
            {Opcode.SMSG_ITEM_COOLDOWN, 0x0FA6D}, // 4.1.0 13914
            {Opcode.SMSG_ITEM_NAME_QUERY_RESPONSE, 0x05D6D}, // 4.1.0 13914
            {Opcode.SMSG_ITEM_PUSH_RESULT, 0x0DC2D}, // 4.1.0 13914
            {Opcode.SMSG_QUERY_ITEM_TEXT_RESPONSE, 0x06A3F}, // 4.1.0 13914
            {Opcode.SMSG_LOAD_EQUIPMENT_SET, 0x0683D}, // 4.1.0 13914
            {Opcode.SMSG_LEARNED_SPELL, 0x0882D}, // 4.1.0 13914
            {Opcode.SMSG_LEVEL_UP_INFO, 0x09F2C}, // 4.1.0 13914
            {Opcode.SMSG_LFG_LFR_LIST, 0x01D7E}, // 4.1.0 13914
            {Opcode.SMSG_LOGIN_SET_TIME_SPEED, 0x0DE2C}, // 4.1.0 13914
            {Opcode.SMSG_LOGIN_VERIFY_WORLD, 0x00F3F}, // 4.1.0 13914
            {Opcode.SMSG_LOG_XP_GAIN, 0x01F7E}, // 4.1.0 13914
            {Opcode.SMSG_LOOT_LIST, 0x00A2E}, // 4.1.0 13914
            {Opcode.SMSG_MAIL_LIST_RESULT, 0x03B7D}, // 4.1.0 13914
            {Opcode.SMSG_MEETINGSTONE_IN_PROGRESS, 0x0AD7D}, // 4.1.0 13914
            {Opcode.SMSG_MEETINGSTONE_MEMBER_ADDED, 0x02F6C}, // 4.1.0 13914
            {Opcode.SMSG_MIRROR_IMAGE_COMPONENTED_DATA, 0x01C3E}, // 4.1.0 13914
            {Opcode.SMSG_MOTD, 0x0987F}, // 4.1.0 13914
            {Opcode.SMSG_MOUNT_SPECIAL_ANIM, 0x0BC2C}, // 4.1.0 13914
            {Opcode.SMSG_MULTIPLE_PACKETS_2, 0x04F2E}, // 4.1.0 13914
            {Opcode.SMSG_NOTIFICATION, 0x0CA7C}, // 4.1.0 13914
            {Opcode.SMSG_NOTIFY_DEST_LOC_SPELL_CAST, 0x0EE3C}, // 4.1.0 13914
            {Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE, 0x05E2E}, // 4.1.0 13914
            {Opcode.SMSG_OFFER_PETITION_ERROR, 0x04B2E}, // 4.1.0 13914
            {Opcode.SMSG_ON_CANCEL_EXPECTED_RIDE_VEHICLE_AURA, 0x01F7F}, // 4.1.0 13914
            {Opcode.SMSG_ON_MONSTER_MOVE, 0xAA6D},
            {Opcode.SMSG_OPEN_LFG_DUNGEON_FINDER, 0x05A7D}, // 4.1.0 13914
            {Opcode.SMSG_PAGE_TEXT, 0x0AC3E}, // 4.1.0 13914
            {Opcode.SMSG_QUERY_PAGE_TEXT_RESPONSE, 0x09B6F}, // 4.1.0 13914
            {Opcode.SMSG_PARTY_COMMAND_RESULT, 0x0E92F}, // 4.1.0 13914
            {Opcode.SMSG_PARTY_KILL_LOG, 0x0982F}, // 4.1.0 13914
            {Opcode.SMSG_SPELL_PERIODIC_AURA_LOG, 0x04C3E}, // 4.1.0 13914
            {Opcode.SMSG_PETITION_QUERY_RESPONSE, 0x0CA2C}, // 4.1.0 13914
            {Opcode.SMSG_PET_ACTION_FEEDBACK, 0x0997C}, // 4.1.0 13914
            {Opcode.SMSG_PET_ACTION_SOUND, 0x04C3C}, // 4.1.0 13914
            {Opcode.SMSG_PET_BROKEN, 0x08B3C}, // 4.1.0 13914
            {Opcode.SMSG_PET_CAST_FAILED, 0x0CA3E}, // 4.1.0 13914
            {Opcode.SMSG_PET_DISMISS_SOUND, 0x01A7C}, // 4.1.0 13914
            {Opcode.SMSG_PET_GUIDS, 0x04B6F}, // 4.1.0 13914
            {Opcode.SMSG_PET_MODE, 0x0E83F}, // 4.1.0 13914
            {Opcode.SMSG_QUERY_PET_NAME_RESPONSE, 0x0587C}, // 4.1.0 13914
            {Opcode.SMSG_PET_SPELLS_MESSAGE, 0x00C2E}, // 4.1.0 13914
            {Opcode.SMSG_PET_TAME_FAILURE, 0x0FC2C}, // 4.1.0 13914
            {Opcode.SMSG_PET_UPDATE_COMBO_POINTS, 0x0D97D}, // 4.1.0 13914
            {Opcode.SMSG_PLAYED_TIME, 0x07F7D}, // 4.1.0 13914
            {Opcode.SMSG_PLAYER_BOUND, 0x0BC7E}, // 4.1.0 13914
            {Opcode.SMSG_PLAYER_DIFFICULTY_CHANGE, 0x09E6D}, // 4.1.0 13914
            {Opcode.SMSG_PLAYER_SKINNED, 0x0BB3D}, // 4.1.0 13914
            {Opcode.SMSG_PLAYER_VEHICLE_DATA, 0x00A7C}, // 4.1.0 13914
            {Opcode.SMSG_PLAY_TIME_WARNING, 0x0EB6F}, // 4.1.0 13914
            {Opcode.SMSG_PRE_RESSURECT, 0x0EA2E}, // 4.1.0 13914
            {Opcode.SMSG_PROC_RESIST, 0x0282E}, // 4.1.0 13914
            {Opcode.SMSG_QUERY_QUESTS_COMPLETED_RESPONSE, 0x0DB3C}, // 4.1.0 13914
            {Opcode.SMSG_QUERY_TIME_RESPONSE, 0x0DF2E}, // 4.1.0 13914
            {Opcode.SMSG_QUEST_GIVER_QUEST_DETAILS, 0xF87C}, // 4.1.0 13914
            {Opcode.SMSG_QUEST_CONFIRM_ACCEPT, 0x03E3E}, // 4.1.0 13914
            {Opcode.SMSG_QUEST_FORCE_REMOVED, 0x06F2E}, // 4.1.0 13914
            {Opcode.SMSG_QUEST_POI_QUERY_RESPONSE, 0x07F3D}, // 4.1.0 13914
            {Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE, 0x04D7E}, // 4.1.0 13914
            {Opcode.SMSG_RAID_INSTANCE_MESSAGE, 0x0EA7C}, // 4.1.0 13914
            {Opcode.SMSG_REAL_GROUP_UPDATE, 0x0692F}, // 4.1.0 13914
            {Opcode.SMSG_RECEIVED_MAIL, 0x0083C}, // 4.1.0 13914
            {Opcode.SMSG_REPORT_PVP_AFK_RESULT, 0x01C6D}, // 4.1.0 13914
            {Opcode.SMSG_RESURRECT_REQUEST, 0x0EF2D}, // 4.1.0 13914
            {Opcode.SMSG_RWHOIS, 0x0AF7D}, // 4.1.0 13914
            {Opcode.SMSG_MAIL_COMMAND_RESULT, 0x02D2C}, // 4.1.0 13914
            {Opcode.SMSG_SEND_UNLEARN_SPELLS, 0x0F82E}, // 4.1.0 13914
            {Opcode.SMSG_SERVERTIME, 0x0CF2D}, // 4.1.0 13914
            {Opcode.SMSG_SERVER_FIRST_ACHIEVEMENT, 0x00E3E}, // 4.1.0 13914
            {Opcode.SMSG_CHAT_SERVER_MESSAGE, 0x04B2C}, // 4.1.0 13914
            {Opcode.SMSG_SET_FACTION_AT_WAR, 0x0283D}, // 4.1.0 13914
            {Opcode.SMSG_SET_FACTION_STANDING, 0x01B6E}, // 4.1.0 13914
            {Opcode.SMSG_SET_PROFICIENCY, 0x0BF3C}, // 4.1.0 13914
            {Opcode.SMSG_SET_PROJECTILE_POSITION, 0x0FD2C}, // 4.1.0 13914
            {Opcode.SMSG_SHOW_MAILBOX, 0x0893C}, // 4.1.0 13914
            {Opcode.SMSG_SPELL_COOLDOWN, 0x0B86C}, // 4.1.0 13914
            {Opcode.SMSG_SPELL_DAMAGE_SHIELD, 0x0AE3C}, // 4.1.0 13914
            {Opcode.SMSG_SPELL_DELAYED, 0x0093C}, // 4.1.0 13914
            {Opcode.SMSG_SPELL_ENERGIZE_LOG, 0x01F6E}, // 4.1.0 13914
            {Opcode.SMSG_SPELL_EXECUTE_LOG, 0x00A7F}, // 4.1.0 13914
            {Opcode.SMSG_SPELL_FAILED_OTHER, 0x07C2E}, // 4.1.0 13914
            {Opcode.SMSG_SPELL_FAILURE, 0x0AF3E}, // 4.1.0 13914
            {Opcode.SMSG_SPELL_GO, 0xEC2D},
            {Opcode.SMSG_SPELL_HEAL_LOG, 0x00F7C}, // 4.1.0 13914
            {Opcode.SMSG_SPELL_INSTAKILL_LOG, 0x0383C}, // 4.1.0 13914
            {Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG, 0x0487C}, // 4.1.0 13914
            {Opcode.SMSG_SPELL_OR_DAMAGE_IMMUNE, 0x00B2F}, // 4.1.0 13914
            {Opcode.SMSG_SPELL_START, 0xDB2F},
            {Opcode.SMSG_SPELL_UPDATE_CHAIN_TARGETS, 0x06B3C}, // 4.1.0 13914
            {Opcode.SMSG_STABLE_RESULT, 0x0896F}, // 4.1.0 13914
            {Opcode.SMSG_STAND_STATE_UPDATE, 0x0D87F}, // 4.1.0 13914
            {Opcode.SMSG_SUMMON_REQUEST, 0x0D87D}, // 4.1.0 13914
            {Opcode.SMSG_SUPERCEDED_SPELLS, 0x02F2E}, // 4.1.0 13914
            {Opcode.SMSG_TEXT_EMOTE, 0x00B3C}, // 4.1.0 13914
            {Opcode.SMSG_THREAT_CLEAR, 0x0786D}, // 4.1.0 13914
            {Opcode.SMSG_THREAT_REMOVE, 0x09B7E}, // 4.1.0 13914
            {Opcode.SMSG_TITLE_EARNED, 0x0BB3C}, // 4.1.0 13914
            {Opcode.SMSG_TRANSFER_ABORTED, 0x03D7F}, // 4.1.0 13914
            {Opcode.SMSG_TRANSFER_PENDING, 0x01B2E}, // 4.1.0 13914
            {Opcode.SMSG_TUTORIAL_FLAGS, 0x0787E}, // 4.1.0 13914
            {Opcode.SMSG_UNLEARNED_SPELLS, 0x0DB6E}, // 4.1.0 13914
            {Opcode.SMSG_UPDATE_ACCOUNT_DATA, 0x01C7D}, // 4.1.0 13914
            {Opcode.SMSG_UPDATE_ACTION_BUTTONS, 0x0FD6F}, // 4.1.0 13914
            {Opcode.SMSG_UPDATE_INSTANCE_ENCOUNTER_UNIT, 0x0CA7D}, // 4.1.0 13914
            {Opcode.SMSG_UPDATE_INSTANCE_OWNERSHIP, 0x05D6E}, // 4.1.0 13914
            {Opcode.SMSG_UPDATE_LAST_INSTANCE, 0x09D2E}, // 4.1.0 13914
            {Opcode.SMSG_UPDATE_OBJECT, 0x0BF6D}, // 4.1.0 13914
            {Opcode.SMSG_USERLIST_ADD, 0x0E83C}, // 4.1.0 13914
            {Opcode.SMSG_USERLIST_REMOVE, 0x05B3F}, // 4.1.0 13914
            {Opcode.SMSG_USERLIST_UPDATE, 0x09C3C}, // 4.1.0 13914
            {Opcode.SMSG_USE_EQUIPMENT_SET_RESULT, 0x0D93D}, // 4.1.0 13914
            {Opcode.SMSG_VOICE_CHAT_STATUS, 0x0C87F}, // 4.1.0 13914
            {Opcode.SMSG_VOICE_PARENTAL_CONTROLS, 0x08F7C}, // 4.1.0 13914
            {Opcode.SMSG_VOICE_SESSION_LEAVE, 0x09A6C}, // 4.1.0 13914
            {Opcode.SMSG_VOICE_SESSION_ROSTER_UPDATE, 0x0DA2E}, // 4.1.0 13914
            {Opcode.SMSG_VOICE_SET_TALKER_MUTED, 0x03B6C}, // 4.1.0 13914
            {Opcode.SMSG_WARDEN_DATA, 0x08C3F}, // 4.1.0 13914
            {Opcode.SMSG_WHO, 0x01C3F}, // 4.1.0 13914
            {Opcode.SMSG_WHO_IS, 0x07D3F}, // 4.1.0 13914
            {Opcode.SMSG_ZONE_UNDER_ATTACK, 0x0FA2F} // 4.1.0 13914
        };

        private static readonly BiDictionary<Opcode, int> MiscOpcodes = new BiDictionary<Opcode, int>
        {
            {Opcode.MSG_AUCTION_HELLO, 0x0FC3D}, // 4.1.0 13914
            {Opcode.MSG_GUILD_EVENT_LOG_QUERY, 0x09A7F}, // 4.1.0 13914
            {Opcode.MSG_GUILD_PERMISSIONS, 0x09B3C}, // 4.1.0 13914
            {Opcode.MSG_INSPECT_ARENA_TEAMS, 0x0AD6D}, // 4.1.0 13914
            {Opcode.MSG_LIST_STABLED_PETS, 0x0FA3C}, // 4.1.0 13914
            {Opcode.MSG_MOVE_TIME_SKIPPED, 0x05C6F}, // 4.1.0 13914
            {Opcode.MSG_NOTIFY_PARTY_SQUELCH, 0x0583D}, // 4.1.0 13914
            {Opcode.MSG_QUERY_GUILD_BANK_TEXT, 0x05F7D}, // 4.1.0 13914
            {Opcode.MSG_QUERY_NEXT_MAIL_TIME, 0x02C3E}, // 4.1.0 13914
            {Opcode.MSG_QUEST_PUSH_RESULT, 0x0B93C}, // 4.1.0 13914
            {Opcode.MSG_RAID_READY_CHECK, 0x0BA2E}, // 4.1.0 13914
            {Opcode.MSG_RAID_READY_CHECK_CONFIRM, 0x0083F}, // 4.1.0 13914
            {Opcode.MSG_RAID_TARGET_UPDATE, 0x00B7E}, // 4.1.0 13914
            {Opcode.MSG_RANDOM_ROLL, 0x0F92C}, // 4.1.0 13914
            {Opcode.MSG_SAVE_GUILD_EMBLEM, 0x0286C}, // 4.1.0 13914
            {Opcode.MSG_TABARDVENDOR_ACTIVATE, 0x02A3E}, // 4.1.0 13914
            {Opcode.MSG_TALENT_WIPE_CONFIRM, 0x00C6F}, // 4.1.0 13914
            {Opcode.MSG_VERIFY_CONNECTIVITY, 0x4F57} // 4.1.0 13914
        };
    }
}

using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V4_0_6_13596
{
    public static class Opcodes_4_0_6
    {
        public static BiDictionary<Opcode, int> Opcodes()
        {
            return Opcs;
        }

        private static readonly BiDictionary<Opcode, int> Opcs = new BiDictionary<Opcode, int>
        {
            {Opcode.CMSG_ACCEPT_LEVEL_GRANT, 0x0B5CC}, // 4.0.6a 13623
            {Opcode.CMSG_ACCEPT_TRADE, 0x00891}, // 4.0.6a 13623
            {Opcode.CMSG_ACTIVATETAXI, 0x039A4}, // 4.0.6a 13623
            {Opcode.CMSG_ACTIVATETAXIEXPRESS, 0x0FC8C}, // 4.0.6a 13623
            {Opcode.CMSG_ACTIVE_PVP_CHEAT, 0x1015E}, // Unknown opcode ID
            {Opcode.CMSG_ADD_FRIEND, 0x03980}, // 4.0.6a 13623
            {Opcode.CMSG_ADD_IGNORE, 0x06780}, // 4.0.6a 13623
            {Opcode.CMSG_ADD_PVP_MEDAL_CHEAT, 0x100D0}, // Unknown opcode ID
            {Opcode.CMSG_ADD_VOICE_IGNORE, 0x0B888}, // 4.0.6a 13623
            {Opcode.CMSG_ALTER_APPEARANCE, 0x034A4}, // 4.0.6a 13623
            {Opcode.CMSG_AREATRIGGER, 0x0ADA8}, // 4.0.6a 13623
            {Opcode.CMSG_AREA_SPIRIT_HEALER_QUERY, 0x0A6C0}, // 4.0.6a 13623
            {Opcode.CMSG_AREA_SPIRIT_HEALER_QUEUE, 0x0F388}, // 4.0.6a 13623
            {Opcode.CMSG_ARENA_TEAM_ACCEPT, 0x061AC}, // 4.0.6a 13623
            {Opcode.CMSG_ARENA_TEAM_CREATE, 0x00509}, // 4.0.6a 13623
            {Opcode.CMSG_ARENA_TEAM_DECLINE, 0x0F2C0}, // 4.0.6a 13623
            {Opcode.CMSG_ARENA_TEAM_DISBAND, 0x0698C}, // 4.0.6a 13623
            {Opcode.CMSG_ARENA_TEAM_INVITE, 0x0E9CC}, // 4.0.6a 13623
            {Opcode.CMSG_ARENA_TEAM_LEADER, 0x0218C}, // 4.0.6a 13623
            {Opcode.CMSG_ARENA_TEAM_LEAVE, 0x064C4}, // 4.0.6a 13623
            {Opcode.CMSG_ARENA_TEAM_QUERY, 0x0B9C8}, // 4.0.6a 13623
            {Opcode.CMSG_ARENA_TEAM_REMOVE, 0x07E84}, // 4.0.6a 13623
            {Opcode.CMSG_ARENA_TEAM_ROSTER, 0x06BAC}, // 4.0.6a 13623
            {Opcode.CMSG_ATTACKSTOP, 0x062C4}, // 4.0.6a 13623
            {Opcode.CMSG_ATTACKSWING, 0x074A8}, // 4.0.6a 13623
            {Opcode.CMSG_AUCTION_LIST_BIDDER_ITEMS, 0x021C0}, // 4.0.6a 13623
            {Opcode.CMSG_AUCTION_LIST_ITEMS, 0x0E48C}, // 4.0.6a 13623
            {Opcode.CMSG_AUCTION_LIST_OWNER_ITEMS, 0x02D8C}, // 4.0.6a 13623
            {Opcode.CMSG_AUCTION_LIST_PENDING_SALES, 0x0EDEC}, // 4.0.6a 13623
            {Opcode.CMSG_AUCTION_PLACE_BID, 0x0A6A0}, // 4.0.6a 13623
            {Opcode.CMSG_AUCTION_REMOVE_ITEM, 0x03EEC}, // 4.0.6a 13623
            {Opcode.CMSG_AUCTION_SELL_ITEM, 0x0EE8C}, // 4.0.6a 13623
            {Opcode.CMSG_AUTH_SESSION, 0x00E0E}, // 4.0.6a 13623
            {Opcode.CMSG_AUTOBANK_ITEM, 0x066E4}, // 4.0.6a 13623
            {Opcode.CMSG_AUTOEQUIP_GROUND_ITEM, 0x10044}, // Unknown opcode ID
            {Opcode.CMSG_AUTOEQUIP_ITEM, 0x0E1C0}, // 4.0.6a 13623
            {Opcode.CMSG_AUTOEQUIP_ITEM_SLOT, 0x0E8A8}, // 4.0.6a 13623
            {Opcode.CMSG_AUTOSTORE_BAG_ITEM, 0x0EDCC}, // 4.0.6a 13623
            {Opcode.CMSG_AUTOSTORE_BANK_ITEM, 0x0F2AC}, // 4.0.6a 13623
            {Opcode.CMSG_AUTOSTORE_GROUND_ITEM, 0x02FC8}, // 4.0.6a 13623
            {Opcode.CMSG_AUTOSTORE_LOOT_ITEM, 0x0B2E8}, // 4.0.6a 13623
            {Opcode.CMSG_AUTO_DECLINE_GUILD_INVITES, 0x0EDAC}, // 4.0.6a 13623
            {Opcode.CMSG_BANKER_ACTIVATE, 0x0E7E0}, // 4.0.6a 13623
            {Opcode.CMSG_BATTLEFIELD_JOIN, 0x0C91}, // 4.0.6a 13623
            {Opcode.CMSG_BATTLEFIELD_LIST, 0x0093}, // 4.0.6a 13623
            {Opcode.CMSG_BATTLEFIELD_MGR_ENTRY_INVITE_RESPONSE, 0x00100}, // 4.0.6a 13623
            {Opcode.CMSG_BATTLEFIELD_MGR_EXIT_REQUEST, 0x08580}, // 4.0.6a 13623
            {Opcode.CMSG_BATTLEFIELD_MGR_QUEUE_INVITE_RESPONSE, 0x08108}, // 4.0.6a 13623
            {Opcode.CMSG_BATTLEFIELD_MGR_QUEUE_REQUEST, 0x00611}, // 4.0.6a 13623 (might be 0x0011D)
            {Opcode.CMSG_BATTLEFIELD_REQUEST_SCORE_DATA, 0x00493}, // 4.0.6a 13623
            {Opcode.CMSG_BATTLEFIELD_STATUS, 0x08188}, // 4.0.6a 13623
            {Opcode.CMSG_BATTLEGROUND_PLAYER_POSITIONS, 0x00293}, // 4.0.6a 13623
            {Opcode.CMSG_BATTLEGROUND_PORT_AND_LEAVE, 0x00E11}, // 4.0.6a 13623
            {Opcode.CMSG_BATTLEMASTER_HELLO, 0x06EC8}, // 4.0.6a 13623
            {Opcode.CMSG_BATTLEMASTER_JOIN, 0x0137}, // 4.0.6a 13623
            {Opcode.CMSG_BATTLEMASTER_JOIN_ARENA, 0x00311}, // 4.0.6a 13623
            {Opcode.CMSG_BATTLEMASTER_JOIN_RATED, 0x00591}, // 4.0.6a 13623
            {Opcode.CMSG_BEGIN_TRADE, 0x00F93}, // 4.0.6a 13623
            {Opcode.CMSG_BINDER_ACTIVATE, 0x0A48C}, // 4.0.6a 13623
            {Opcode.CMSG_BOT_DETECTED, 0x0E757}, // 4.0.3a 13329
            {Opcode.CMSG_BOT_DETECTED2, 0x10002}, // Unknown opcode ID
            {Opcode.CMSG_BUG, 0x034AC}, // 4.0.6a 13623
            {Opcode.CMSG_BUSY_TRADE, 0x1004C}, // Unknown opcode ID
            {Opcode.CMSG_BUYBACK_ITEM, 0x0A4C4}, // 4.0.6a 13623
            {Opcode.CMSG_BUY_BANK_SLOT, 0x02BA0}, // 4.0.6a 13623
            {Opcode.CMSG_BUY_ITEM, 0x0EA84}, // 4.0.6a 13623
            {Opcode.CMSG_BUY_LOTTERY_TICKET_OBSOLETE, 0x00336}, // 4.0.3a 13329
            {Opcode.CMSG_CALENDAR_ADD_EVENT, 0x0F488}, // 4.0.6a 13623
            {Opcode.CMSG_CALENDAR_ARENA_TEAM, 0x0E9AC}, // 4.0.6a 13623
            {Opcode.CMSG_CALENDAR_COMPLAIN, 0x01E75}, // 4.0.6a 13623
            {Opcode.CMSG_CALENDAR_CONTEXT_EVENT_SIGNUP, 0x0AEAC}, // 4.0.6a 13623 (might be 0x0DC74)
            {Opcode.CMSG_CALENDAR_COPY_EVENT, 0x0BF84}, // 4.0.6a 13623
            {Opcode.CMSG_CALENDAR_EVENT_INVITE, 0x0F6C4}, // 4.0.6a 13623
            {Opcode.CMSG_CALENDAR_EVENT_MODERATOR_STATUS, 0x0BDE4}, // 4.0.6a 13623
            {Opcode.CMSG_CALENDAR_EVENT_REMOVE_INVITE, 0x0EBCC}, // 4.0.6a 13623
            {Opcode.CMSG_CALENDAR_EVENT_RSVP, 0x0757F}, // 4.0.6a 13623
            {Opcode.CMSG_CALENDAR_EVENT_STATUS, 0x0BC84}, // 4.0.6a 13623
            {Opcode.CMSG_CALENDAR_GET_CALENDAR, 0x0B2A4}, // 4.0.6a 13623
            {Opcode.CMSG_CALENDAR_GET_EVENT, 0x03580}, // 4.0.6a 13623
            {Opcode.CMSG_CALENDAR_GET_NUM_PENDING, 0x0EFAC}, // 4.0.6a 13623
            {Opcode.CMSG_CALENDAR_GUILD_FILTER, 0x0ADAC}, // 4.0.6a 13623
            {Opcode.CMSG_CALENDAR_REMOVE_EVENT, 0x06C88}, // 4.0.6a 13623
            {Opcode.CMSG_CALENDAR_UPDATE_EVENT, 0x0F084}, // 4.0.6a 13623
            {Opcode.CMSG_CANCEL_AURA, 0x07684}, // 4.0.6a 13623
            {Opcode.CMSG_CANCEL_AUTO_REPEAT_SPELL, 0x075CC}, // 4.0.6a 13623
            {Opcode.CMSG_CANCEL_CAST, 0x0A1C0}, // 4.0.6a 13623
            {Opcode.CMSG_CANCEL_CHANNELLING, 0x0A780}, // 4.0.6a 13623
            {Opcode.CMSG_CANCEL_GROWTH_AURA, 0x0FECC}, // 4.0.6a 13623
            {Opcode.CMSG_CANCEL_MOUNT_AURA, 0x064CC}, // 4.0.6a 13623 not sure
            {Opcode.CMSG_CANCEL_TEMP_ENCHANTMENT, 0x0E484}, // 4.0.6a 13623
            {Opcode.CMSG_CANCEL_TRADE, 0x00013}, // 4.0.6a 13623
            {Opcode.CMSG_CAST_SPELL, 0x065C4}, // 4.0.6a 13623
            {Opcode.CMSG_CHANGE_PERSONAL_ARENA_RATING, 0x101B2}, // Unknown opcode ID
            {Opcode.CMSG_CHANGE_SEATS_ON_CONTROLLED_VEHICLE, 0x0E988}, // 4.0.6a 13623
            {Opcode.CMSG_CHANNEL_ANNOUNCEMENTS, 0x0004B}, // 4.0.6a 13623
            {Opcode.CMSG_CHANNEL_BAN, 0x0000A}, // 4.0.6a 13623
            {Opcode.CMSG_CHANNEL_DISPLAY_LIST, 0x10185}, // Unknown opcode ID
            {Opcode.CMSG_CHANNEL_INVITE, 0x00020}, // 4.0.6a 13623
            {Opcode.CMSG_CHANNEL_KICK, 0x00068}, // 4.0.6a 13623
            {Opcode.CMSG_CHANNEL_LIST, 0x07FE0}, // 4.0.6a 13623
            {Opcode.CMSG_CHANNEL_MODERATE, 0x10018}, // Unknown opcode ID
            {Opcode.CMSG_CHANNEL_MODERATOR, 0x00828}, // 4.0.6a 13623
            {Opcode.CMSG_CHANNEL_MUTE, 0x00023}, // 4.0.6a 13623
            {Opcode.CMSG_CHANNEL_OWNER, 0x00848}, // 4.0.6a 13623
            {Opcode.CMSG_CHANNEL_PASSWORD, 0x0080A}, // 4.0.6a 13623
            {Opcode.CMSG_CHANNEL_ROSTER_INFO, 0x00069}, // 4.0.6a 13623
            {Opcode.CMSG_CHANNEL_SET_OWNER, 0x00800}, // 4.0.6a 13623
            {Opcode.CMSG_CHANNEL_SILENCE_ALL, 0x00021}, // 4.0.6a 13623
            {Opcode.CMSG_CHANNEL_SILENCE_VOICE, 0x00801}, // 4.0.6a 13623
            {Opcode.CMSG_CHANNEL_UNBAN, 0x00048}, // 4.0.6a 13623
            {Opcode.CMSG_CHANNEL_UNMODERATOR, 0x00809}, // 4.0.6a 13623
            {Opcode.CMSG_CHANNEL_UNMUTE, 0x00841}, // 4.0.6a 13623
            {Opcode.CMSG_CHANNEL_UNSILENCE_ALL, 0x00840}, // 4.0.6a 13623
            {Opcode.CMSG_CHANNEL_UNSILENCE_VOICE, 0x0002B}, // 4.0.6a 13623
            {Opcode.CMSG_CHANNEL_VOICE_OFF, 0x07A88}, // 4.0.3a 13329
            {Opcode.CMSG_CHANNEL_VOICE_ON, 0x05A98}, // 4.0.3a 13329
            {Opcode.CMSG_CHARACTER_POINT_CHEAT, 0x100A0}, // Unknown opcode ID
            {Opcode.CMSG_CHAR_CREATE, 0x07EEC}, // 4.0.6a 13623
            {Opcode.CMSG_CHAR_CUSTOMIZE, 0x06484}, // 4.0.6a 13623
            {Opcode.CMSG_CHAR_DELETE, 0x03B84}, // 4.0.6a 13623
            {Opcode.CMSG_CHAR_ENUM, 0x06AA4}, // 4.0.6a 13623
            {Opcode.CMSG_CHAR_FACTION_CHANGE, 0x0BBCC}, // 4.0.6a 13623
            {Opcode.CMSG_CHAR_RACE_CHANGE, 0x06EA4}, // 4.0.6a 13623
            {Opcode.CMSG_CHAR_RENAME, 0x027C4}, // 4.0.6a 13623
            {Opcode.CMSG_CHAT_FILTERED, 0x075A8}, // 4.0.6a 13623 (might be 0x00D5D)
            {Opcode.CMSG_CHAT_IGNORED, 0x0A78C}, // 4.0.6a 13623
            {Opcode.CMSG_CHEAT_DUMP_ITEMS_DEBUG_ONLY, 0x1015F}, // Unknown opcode ID
            {Opcode.CMSG_CHEAT_PLAYER_LOGIN, 0x1017D}, // Unknown opcode ID
            {Opcode.CMSG_CHEAT_PLAYER_LOOKUP, 0x1017E}, // Unknown opcode ID
            {Opcode.CMSG_CHEAT_SET_ARENA_CURRENCY, 0x1014E}, // Unknown opcode ID
            {Opcode.CMSG_CHEAT_SET_HONOR_CURRENCY, 0x1014D}, // Unknown opcode ID
            {Opcode.CMSG_CHECK_LOGIN_CRITERIA, 0x101F8}, // Unknown opcode ID
            {Opcode.CMSG_CLEAR_CHANNEL_WATCH, 0x0A1E0}, // 4.0.6a 13623
            {Opcode.CMSG_CLEAR_EXPLORATION, 0x100A9}, // Unknown opcode ID
            {Opcode.CMSG_CLEAR_SERVER_BUCK_DATA, 0x101AD}, // Unknown opcode ID
            {Opcode.CMSG_CLEAR_TRADE_ITEM, 0x00213}, // 4.0.6a 13623
            {Opcode.CMSG_COMMENTATOR_ENABLE, 0x0B1C4}, // 4.0.6a 13623
            {Opcode.CMSG_COMMENTATOR_ENTER_INSTANCE, 0x07BCC}, // 4.0.6a 13623
            {Opcode.CMSG_COMMENTATOR_EXIT_INSTANCE, 0x0B8E4}, // 4.0.6a 13623
            {Opcode.CMSG_COMMENTATOR_GET_MAP_INFO, 0x028C4}, // 4.0.6a 13623
            {Opcode.CMSG_COMMENTATOR_GET_PLAYER_INFO, 0x0BBA0}, // 4.0.6a 13623
            {Opcode.CMSG_COMMENTATOR_INSTANCE_COMMAND, 0x07D80}, // 4.0.6a 13623
            {Opcode.CMSG_COMMENTATOR_SKIRMISH_QUEUE_COMMAND, 0x06DC8}, // 4.0.6a 13623
            {Opcode.CMSG_COMMENTATOR_START_WARGAME, 0x08588}, // 4.0.6a 13623 (might be CMSG_ITEM_QUERY_SINGLE)
            {Opcode.CMSG_COMPLAIN, 0x068C8}, // 4.0.6a 13623
            {Opcode.CMSG_COMPLETE_ACHIEVEMENT_CHEAT, 0x101DC}, // Unknown opcode ID
            {Opcode.CMSG_COMPLETE_CINEMATIC, 0x02ACC}, // 4.0.6a 13623
            {Opcode.CMSG_COMPLETE_MOVIE, 0x0E188}, // 4.0.6a 13623
            {Opcode.CMSG_CONTACT_LIST, 0x0EAA4}, // 4.0.6a 13623
            {Opcode.CMSG_CORPSE_MAP_POSITION_QUERY, 0x023CC}, // 4.0.6a 13623
            {Opcode.CMSG_CREATURE_QUERY, 0x0268C}, // 4.0.6a 13623
            {Opcode.CMSG_DANCE_QUERY, 0x022A0}, // 4.0.6a 13623
            {Opcode.CMSG_DEBUG_ACTIONS_START, 0x1011B}, // Unknown opcode ID
            {Opcode.CMSG_DEBUG_ACTIONS_STOP, 0x1011C}, //Unknown opcode ID
            {Opcode.CMSG_DEBUG_LIST_TARGETS, 0x10187}, // Unknown opcode ID
            {Opcode.CMSG_DECHARGE, 0x10092}, // Unknown opcode ID
            {Opcode.CMSG_DECLINE_CHANNEL_INVITE, 0x0DF7F}, // 4.0.6a 13623
            {Opcode.CMSG_DELETE_DANCE, 0x101C8}, // Unknown opcode ID
            {Opcode.CMSG_DEL_FRIEND, 0x02980}, // 4.0.6a 13623
            {Opcode.CMSG_DEL_IGNORE, 0x0F384}, // 4.0.6a 13623
            {Opcode.CMSG_DEL_PVP_MEDAL_CHEAT, 0x100D1}, // Unknown opcode ID
            {Opcode.CMSG_DEL_VOICE_IGNORE, 0x07AC0}, // 4.0.6a 13623
            {Opcode.CMSG_DESTROY_ITEM, 0x0B8A8}, // 4.0.6a 13623
            {Opcode.CMSG_DISMISS_CONTROLLED_VEHICLE, 0x0E3C0}, // 4.0.6a 13623
            {Opcode.CMSG_DISMISS_CRITTER, 0x0B7CC}, // 4.0.6a 13623
            {Opcode.CMSG_DUEL_ACCEPTED, 0x0A688}, // 4.0.6a 13623
            {Opcode.CMSG_DUEL_CANCELLED, 0x06F8C}, // 4.0.6a 13623
            {Opcode.CMSG_DUMP_OBJECTS, 0x101ED}, // Unknown opcode ID
            {Opcode.CMSG_EJECT_PASSENGER, 0x0F688}, // 4.0.6a 13623
            {Opcode.CMSG_EMOTE, 0x0FAC4}, // 4.0.6a 13623 (might be 0x07F5C)
            {Opcode.CMSG_ENABLETAXI, 0x0328C}, // 4.0.6a 13623
            {Opcode.CMSG_ENABLE_DAMAGE_LOG, 0x100CB}, // Unknown opcode ID
            {Opcode.CMSG_EQUIPMENT_SET_DELETE, 0x0AEA0}, // 4.0.6a 13623
            {Opcode.CMSG_EQUIPMENT_SET_SAVE, 0x0BFC0}, // 4.0.6a 13623
            {Opcode.CMSG_EQUIPMENT_SET_USE, 0x0E8A0}, // 4.0.6a 13623
            {Opcode.CMSG_EXPIRE_RAID_INSTANCE, 0x101A7}, // Unknown opcode ID
            {Opcode.CMSG_FAR_SIGHT, 0x0B2EC}, // 4.0.6a 13623
            {Opcode.CMSG_FLOOD_GRACE_CHEAT, 0x101F2}, // Unknown opcode ID
            {Opcode.CMSG_FORCE_FLIGHT_BACK_SPEED_CHANGE_ACK, 0x0FBC8}, // 4.0.6a 13623
            {Opcode.CMSG_FORCE_FLIGHT_SPEED_CHANGE_ACK, 0x0A98C}, // 4.0.6a 13623
            {Opcode.CMSG_FORCE_MOVE_ROOT_ACK, 0x07184}, // 4.0.6a 13623
            {Opcode.CMSG_FORCE_MOVE_UNROOT_ACK, 0x07FA8}, // 4.0.6a 13623
            {Opcode.CMSG_FORCE_PITCH_RATE_CHANGE_ACK, 0x0E6C0}, // 4.0.6a 13623
            {Opcode.CMSG_FORCE_RUN_BACK_SPEED_CHANGE_ACK, 0x078E4}, // 4.0.6a 13623
            {Opcode.CMSG_FORCE_RUN_SPEED_CHANGE_ACK, 0x026C8}, // 4.0.6a 13623
            {Opcode.CMSG_FORCE_SAY_CHEAT, 0x101E7}, // Unknown opcode ID
            {Opcode.CMSG_FORCE_SWIM_BACK_SPEED_CHANGE_ACK, 0x0E8C4}, // 4.0.6a 13623
            {Opcode.CMSG_FORCE_SWIM_SPEED_CHANGE_ACK, 0x0B1A0}, // 4.0.6a 13623
            {Opcode.CMSG_FORCE_TURN_RATE_CHANGE_ACK, 0x0E384}, // 4.0.6a 13623
            {Opcode.CMSG_FORCE_WALK_SPEED_CHANGE_ACK, 0x078CC}, // 4.0.6a 13623
            {Opcode.CMSG_GAMEOBJECT_QUERY, 0x072A0}, // 4.0.6a 13623
            {Opcode.CMSG_GAMEOBJ_REPORT_USE, 0x023A0}, // 4.0.6a 13623
            {Opcode.CMSG_GAMEOBJ_USE, 0x029E4}, // 4.0.6a 13623
            {Opcode.CMSG_GAMESPEED_SET, 0x02084}, // 4.0.6a 13623 FIXME SMSG_MOVE_LAND_WALK
            {Opcode.CMSG_GETDEATHBINDZONE, 0x1005E}, // Unknown opcode ID
            {Opcode.CMSG_GET_CHANNEL_MEMBER_COUNT, 0x00009}, // 4.0.6a 13623
            {Opcode.CMSG_GET_MAIL_LIST, 0x0B284}, // 4.0.6a 13623
            {Opcode.CMSG_GET_MIRRORIMAGE_DATA, 0x0A08C}, // 4.0.6a 13623
            {Opcode.CMSG_GHOST, 0x10088}, // Unknown opcode ID
            {Opcode.CMSG_GMRESPONSE_RESOLVE, 0x062C8}, // 4.0.6a 13623
            {Opcode.CMSG_GMSURVEY_SUBMIT, 0x0E280}, // 4.0.6a 13623
            {Opcode.CMSG_GMTICKETSYSTEM_TOGGLE, 0x100D7}, // Unknown opcode ID
            {Opcode.CMSG_GMTICKET_CREATE, 0x06380}, // 4.0.6a 13623
            {Opcode.CMSG_GMTICKET_DELETETICKET, 0x0FBE4}, // 4.0.6a 13623
            {Opcode.CMSG_GMTICKET_GETTICKET, 0x0B4C4}, // 4.0.6a 13623
            {Opcode.CMSG_GMTICKET_SYSTEMSTATUS, 0x0ACE0}, // 4.0.6a 13623
            {Opcode.CMSG_GMTICKET_UPDATETEXT, 0x07F8C}, // 4.0.6a 13623
            {Opcode.CMSG_GM_CHARACTER_RESTORE, 0x10198}, // Unknown opcode ID
            {Opcode.CMSG_GM_CHARACTER_SAVE, 0x10199}, // Unknown opcode ID
            {Opcode.CMSG_GM_CREATE_ITEM_TARGET, 0x10096}, // Unknown opcode ID
            {Opcode.CMSG_GM_DESTROY_ONLINE_CORPSE, 0x10118}, // Unknown opcode ID
            {Opcode.CMSG_GM_INVIS, 0x10089}, // Unknown opcode ID
            {Opcode.CMSG_GM_NUKE, 0x1008F}, // Unknown opcode ID
            {Opcode.CMSG_GM_NUKE_ACCOUNT, 0x10116}, // Unknown opcode ID
            {Opcode.CMSG_GM_REPORT_LAG, 0x03FA0}, // 4.0.6a 13623
            {Opcode.CMSG_GM_SET_SECURITY_GROUP, 0x1008E}, // Unknown opcode ID
            {Opcode.CMSG_GM_SHOW_COMPLAINTS, 0x10181}, // Unknown opcode ID
            {Opcode.CMSG_GM_TEACH, 0x10095}, // Unknown opcode ID
            {Opcode.CMSG_GM_UNSQUELCH, 0x10182}, // Unknown opcode ID
            {Opcode.CMSG_GM_UNTEACH, 0x100FD}, // Unknown opcode ID
            {Opcode.CMSG_GM_UPDATE_TICKET_STATUS, 0x10124}, // Unknown opcode ID
            {Opcode.CMSG_GM_WHISPER, 0x10172}, // Unknown opcode ID
            {Opcode.CMSG_GOSSIP_HELLO, 0x074C8}, // 4.0.6a 13623
            {Opcode.CMSG_GOSSIP_SELECT_OPTION, 0x0FF88}, // 4.0.6a 13623
            {Opcode.CMSG_GRANT_LEVEL, 0x0B980}, // 4.0.6a 13623
            {Opcode.CMSG_GROUP_ACCEPT, 0x0368C}, // 4.0.6a 13623
            {Opcode.CMSG_GROUP_ASSISTANT_LEADER, 0x03F84}, // 4.0.6a 13623
            {Opcode.CMSG_GROUP_CANCEL, 0x1000A}, // Unknown opcode ID
            {Opcode.CMSG_GROUP_CHANGE_SUB_GROUP, 0x03A80}, // 4.0.6a 13623
            {Opcode.CMSG_GROUP_DECLINE, 0x0B4CC}, // 4.0.6a 13623
            {Opcode.CMSG_GROUP_DISBAND, 0x0BE88}, // 4.0.6a 13623
            {Opcode.CMSG_GROUP_INVITE, 0x027C0}, // 4.0.6a 13623
            {Opcode.CMSG_GROUP_RAID_CONVERT, 0x0628C}, // 4.0.6a 13623
            {Opcode.CMSG_GROUP_SET_LEADER, 0x0B6E0}, // 4.0.6a 13623
            {Opcode.CMSG_GROUP_SET_ROLES, 0x08509}, // 4.0.6a 13623
            {Opcode.CMSG_GROUP_SWAP_SUB_GROUP, 0x031C8}, // 4.0.6a 13623 (might be 0x0677F)
            {Opcode.CMSG_GROUP_UNINVITE, 0x04F74}, // 4.0.6a 13623
            {Opcode.CMSG_GROUP_UNINVITE_GUID, 0x0E3C8}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_ACCEPT, 0x03729}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_ACHIEVEMENT_MEMBERS, 0x02509}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_ADD_RANK, 0x02309}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_BANKER_ACTIVATE, 0x0FFC4}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_BANK_BUY_TAB, 0x078AC}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_BANK_DEPOSIT_MONEY, 0x06FE8}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_BANK_QUERY_TAB, 0x0A788}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_BANK_SWAP_ITEMS, 0x0A8C4}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_BANK_UPDATE_TAB, 0x0E3CC}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_BANK_WITHDRAW_MONEY, 0x073A8}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_DECLINE, 0x0352D}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_DEL_RANK, 0x02129}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_DEMOTE, 0x0330D}, // 4.0.6a 13623 (might be 0x01902)
            {Opcode.CMSG_GUILD_DISBAND, 0x0372D}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_INFO, 0x06884}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_INFO_TEXT, 0x0270D}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_INVITE, 0x02DA8}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_LEADER, 0x02650}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_LEAVE, 0x03329}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_MOTD, 0x0272D}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_NEWS_UPDATE_STICKY, 0x0252D}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_PROMOTE, 0x02109}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_QUERY, 0x0AFC4}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_QUERY_NEWS, 0x3529}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_QUERY_TRADESKILL, 0x02329}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_RANK, 0x02709}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_REMOVE, 0x0312D}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_REQUEST_MAX_DAILY_XP, 0x0350D}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_RANKS, 0x3129}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_ROSTER, 0x0250D}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_SET_ACHIEVEMENT_TRACKING, 0x0310D}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_SET_NOTE, 0x0232D}, // 4.0.6a 13623
            {Opcode.CMSG_GUILD_SWITCH_RANK, 0x3309}, // 4.0.6a 13623
            {Opcode.CMSG_HEARTH_AND_RESURRECT, 0x0B6C4}, // 4.0.6a 13623
            {Opcode.CMSG_IGNORE_DIMINISHING_RETURNS_CHEAT, 0x1019D}, // Unknown opcode ID
            {Opcode.CMSG_IGNORE_KNOCKBACK_CHEAT, 0x10126}, // Unknown opcode ID
            {Opcode.CMSG_IGNORE_REQUIREMENTS_CHEAT, 0x1016D}, // Unknown opcode ID
            {Opcode.CMSG_IGNORE_TRADE, 0x1004D}, // Unknown opcode ID
            {Opcode.CMSG_INITIATE_TRADE, 0x00413}, // 4.0.6a 13623
            {Opcode.CMSG_INSPECT, 0x078A8}, // 4.0.6a 13623
            {Opcode.CMSG_INSPECT_HONOR_STATS, 0x00E93}, // 4.0.6a 13623
            {Opcode.CMSG_INSTANCE_LOCK_WARNING_RESPONSE, 0x034C4}, // 4.0.6a 13623 (might be 0x0E476)
            {Opcode.CMSG_ITEM_NAME_QUERY, 0x100E3}, // Unknown opcode ID
            {Opcode.CMSG_ITEM_QUERY_MULTIPLE, 0x10008}, // Unknown opcode ID
            {Opcode.CMSG_ITEM_REFUND, 0x062E8}, // 4.0.6a 13623
            {Opcode.CMSG_ITEM_REFUND_INFO, 0x031E0}, // 4.0.6a 13623
            {Opcode.CMSG_ITEM_TEXT_QUERY, 0x0F280}, // 4.0.6a 13623
            {Opcode.CMSG_JOIN_CHANNEL, 0x00002}, // 4.0.6a 13623
            {Opcode.CMSG_KEEP_ALIVE, 0x02CE0}, // 4.0.6a 13623
            {Opcode.CMSG_LEARN_DANCE_MOVE, 0x101C9}, // Unknown opcode ID
            {Opcode.CMSG_LEARN_PREVIEW_TALENTS, 0x0FFAC}, // 4.0.6a 13623
            {Opcode.CMSG_LEARN_PREVIEW_TALENTS_PET, 0x0BCE8}, // 4.0.6a 13623
            {Opcode.CMSG_LEARN_TALENT, 0x0A7CC}, // 4.0.6a 13623
            {Opcode.CMSG_LEAVE_BATTLEFIELD, 0x07DC4}, // 4.0.6a 13623
            {Opcode.CMSG_LEAVE_CHANNEL, 0x0000B}, // 4.0.6a 13623
            {Opcode.CMSG_LFG_JOIN, 0x063C0}, // 4.0.6a 13623
            {Opcode.CMSG_LFG_LEAVE, 0x03688}, // 4.0.6a 13623
            {Opcode.CMSG_LFG_LFR_JOIN, 0x061CC}, // 4.0.6a 13623
            {Opcode.CMSG_LFG_LFR_LEAVE, 0x0847D}, // 4.0.3a 13329
            {Opcode.CMSG_LFG_PARTY_LOCK_INFO_REQUEST, 0x00574}, // 4.0.3a 13329
            {Opcode.CMSG_LFG_PLAYER_LOCK_INFO_REQUEST, 0x0E5E8}, // 4.0.6a 13623 (might be 0x00C76)
            {Opcode.CMSG_LFG_PROPOSAL_RESULT, 0x0A7A4}, // 4.0.6a 13623
            {Opcode.CMSG_LFG_SET_BOOT_VOTE, 0x0AA84}, // 4.0.6a 13623 (might be 0x0D65D)
            {Opcode.CMSG_LFG_SET_COMMENT, 0x0E1C4}, // 4.0.6a 13623
            {Opcode.CMSG_LFG_SET_NEEDS, 0x10145}, // Unknown opcode ID
            {Opcode.CMSG_LFG_SET_ROLES, 0x0E8CC}, // 4.0.6a 13623
            {Opcode.CMSG_LFG_SET_ROLES_2, 0x1020B}, // Unknown opcode ID
            {Opcode.CMSG_LFG_TELEPORT, 0x0FA88}, // 4.0.6a 13623
            {Opcode.CMSG_LIST_INVENTORY, 0x0EDC8}, // 4.0.6a 13623
            {Opcode.CMSG_LOAD_DANCES, 0x101C4}, // Unknown opcode ID
            {Opcode.CMSG_LOAD_SCREEN, 0x08508}, // 4.0.6a 13623
            {Opcode.CMSG_LOGOUT_CANCEL, 0x039E8}, // 4.0.6a 13623
            {Opcode.CMSG_LOGOUT_REQUEST, 0x0A7A8}, // 4.0.6a 13623
            {Opcode.CMSG_LOG_DISCONNECT, 0x0064C}, // 4.0.6a 13623
            {Opcode.CMSG_LOOT, 0x0FCEC}, // 4.0.6a 13623
            {Opcode.CMSG_LOOT_MASTER_GIVE, 0x03BA4}, // 4.0.6a 13623
            {Opcode.CMSG_LOOT_METHOD, 0x0FCCC}, // 4.0.6a 13623
            {Opcode.CMSG_LOOT_MONEY, 0x079E0}, // 4.0.6a 13623
            {Opcode.CMSG_LOOT_RELEASE, 0x03CE8}, // 4.0.6a 13623
            {Opcode.CMSG_LOOT_ROLL, 0x0BDA8}, // 4.0.6a 13623
            {Opcode.CMSG_LOTTERY_QUERY_OBSOLETE, 0x10129}, // Unknown opcode ID
            {Opcode.CMSG_LUA_USAGE, 0x10122}, // Unknown opcode ID
            {Opcode.CMSG_MAELSTROM_GM_SENT_MAIL, 0x1015D}, // Unknown opcode ID
            {Opcode.CMSG_MAELSTROM_INVALIDATE_CACHE, 0x10155}, // Unknown opcode ID
            {Opcode.CMSG_MAELSTROM_RENAME_GUILD, 0x1019A}, // Unknown opcode ID
            {Opcode.CMSG_MAIL_CREATE_TEXT_ITEM, 0x0FAE4}, // 4.0.6a 13623
            {Opcode.CMSG_MAIL_DELETE, 0x07DE4}, // 4.0.6a 13623
            {Opcode.CMSG_MAIL_MARK_AS_READ, 0x0E8C0}, // 4.0.6a 13623
            {Opcode.CMSG_MAIL_RETURN_TO_SENDER, 0x065A4}, // 4.0.6a 13623
            {Opcode.CMSG_MAIL_TAKE_ITEM, 0x062A8}, // 4.0.6a 13623
            {Opcode.CMSG_MAIL_TAKE_MONEY, 0x0E8EC}, // 4.0.6a 13623
            {Opcode.CMSG_MEETINGSTONE_CHEAT, 0x100D5}, // Unknown opcode ID
            {Opcode.CMSG_MEETINGSTONE_INFO, 0x0F984}, // 4.0.6a 13623
            {Opcode.CMSG_MESSAGECHAT_AFK, 0x0086B}, // 4.0.6a 13623
            {Opcode.CMSG_MESSAGECHAT_BATTLEGROUND, 0x00063}, // 4.0.6a 13623
            {Opcode.CMSG_MESSAGECHAT_BATTLEGROUND_LEADER, 0x00860}, // 4.0.6a 13623
            {Opcode.CMSG_MESSAGECHAT_CHANNEL, 0x00821}, // 4.0.6a 13623
            {Opcode.CMSG_MESSAGECHAT_DND, 0x00003}, // 4.0.6a 13623
            {Opcode.CMSG_MESSAGECHAT_EMOTE, 0x00042}, // 4.0.6a 13623
            {Opcode.CMSG_MESSAGECHAT_GUILD, 0x00823}, // 4.0.6a 13623
            {Opcode.CMSG_MESSAGECHAT_OFFICER, 0x00861}, // 4.0.6a 13623
            {Opcode.CMSG_MESSAGECHAT_PARTY, 0x0084B}, // 4.0.6a 13623
            {Opcode.CMSG_MESSAGECHAT_PARTY_LEADER, 0x0080B}, // 4.0.6a 13623
            {Opcode.CMSG_MESSAGECHAT_RAID, 0x00803}, // 4.0.6a 13623
            {Opcode.CMSG_MESSAGECHAT_RAID_LEADER, 0x00863}, // 4.0.6a 13623
            {Opcode.CMSG_MESSAGECHAT_RAID_WARNING, 0x00061}, // 4.0.6a 13623
            {Opcode.CMSG_MESSAGECHAT_SAY, 0x0002A}, // 4.0.6a 13623
            {Opcode.CMSG_MESSAGECHAT_WHISPER, 0x00000}, // 4.0.6a 13623
            {Opcode.CMSG_MESSAGECHAT_YELL, 0x00802}, // 4.0.6a 13623
            {Opcode.CMSG_MINIGAME_MOVE, 0x0B2E4}, // 4.0.6a 13623
            {Opcode.CMSG_MOUNTSPECIAL_ANIM, 0x02EE4}, // 4.0.6a 13623
            {Opcode.CMSG_MOVE_CHNG_TRANSPORT, 0x10158}, // Unknown opcode ID
            {Opcode.CMSG_MOVE_FALL_RESET, 0x0E680}, //  4.0.6a 13623
            {Opcode.CMSG_MOVE_FEATHER_FALL_ACK, 0x06EA8}, // 4.0.6a 13623
            {Opcode.CMSG_MOVE_FLIGHT_ACK, 0x0A3C8}, // 4.0.6a 13623
            {Opcode.CMSG_MOVE_GRAVITY_DISABLE_ACK, 0x0F0C0}, // 4.0.6a 13623
            {Opcode.CMSG_MOVE_GRAVITY_ENABLE_ACK, 0x07DE8}, // 4.0.6a 13623
            {Opcode.CMSG_MOVE_HOVER_ACK, 0x0F4CC}, // 4.0.6a 13623
            {Opcode.CMSG_MOVE_KNOCK_BACK_ACK, 0x0F580}, // 4.0.6a 13623
            {Opcode.CMSG_MOVE_NOT_ACTIVE_MOVER, 0x0B9A8}, // 4.0.6a 13623
            {Opcode.CMSG_MOVE_SET_CAN_FLY_ACK, 0x0FCAC}, // 4.0.6a 13623
            {Opcode.CMSG_MOVE_SET_FLY, 0x0E0E0}, // 4.0.6a 13623
            {Opcode.CMSG_MOVE_SET_RAW_POSITION, 0x0F0C8}, // 4.0.6a 13623
            {Opcode.CMSG_MOVE_SET_RUN_SPEED, 0x1016F}, // Unknown opcode ID
            {Opcode.CMSG_MOVE_SPLINE_DONE, 0x069E8}, // 4.0.6a 13623
            {Opcode.CMSG_MOVE_START_SWIM_CHEAT, 0x026C0}, // 4.0.6a 13623
            {Opcode.CMSG_MOVE_STOP_SWIM_CHEAT, 0x06988}, // 4.0.6a 13623
            {Opcode.CMSG_MOVE_TIME_SKIPPED, 0x0E180}, // 4.0.6a 13623
            {Opcode.CMSG_MOVE_WATER_WALK_ACK, 0x021C4}, // 4.0.6a 13623
            {Opcode.CMSG_NAME_QUERY, 0x07AAC}, // 4.0.6a 13623
            {Opcode.CMSG_NEW_SPELL_SLOT, 0x10057}, // Unknown opcode ID
            {Opcode.CMSG_NEXT_CINEMATIC_CAMERA, 0x0B2CC}, // 4.0.6a 13623
            {Opcode.CMSG_NO_SPELL_VARIANCE, 0x101A8}, // Unknown opcode ID
            {Opcode.CMSG_NPC_TEXT_QUERY, 0x0A2EC}, // 4.0.6a 13623
            {Opcode.CMSG_OFFER_PETITION, 0x07AC4}, // 4.0.6a 13623
            {Opcode.CMSG_OPENING_CINEMATIC, 0x0B1E8}, // 4.0.6a 13623
            {Opcode.CMSG_OPEN_ITEM, 0x0A2A8}, // 4.0.6a 13623
            {Opcode.CMSG_OPT_OUT_OF_LOOT, 0x075A4}, // 4.0.6a 13623
            {Opcode.CMSG_PAGE_TEXT_QUERY, 0x0AC8C}, // 4.0.6a 13623
            {Opcode.CMSG_PARTY_SILENCE, 0x06CC4}, // 4.0.6a 13623 (might be 0x0F755)
            {Opcode.CMSG_PARTY_UNSILENCE, 0x0FCA4}, // 4.0.6a 13623 (might be 0x02F7D)
            {Opcode.CMSG_PETITION_BUY, 0x0B3E4}, // 4.0.6a 13623
            {Opcode.CMSG_PETITION_QUERY, 0x0B1AC}, // 4.0.6a 13623
            {Opcode.CMSG_PETITION_SHOWLIST, 0x0FCC4}, // 4.0.6a 13623
            {Opcode.CMSG_PETITION_SHOW_SIGNATURES, 0x02CA8}, // 4.0.6a 13623
            {Opcode.CMSG_PETITION_SIGN, 0x03AA0}, // 4.0.6a 13623
            {Opcode.CMSG_PET_ABANDON, 0x0A480}, // 4.0.6a 13623
            {Opcode.CMSG_PET_ACTION, 0x0AFC0}, // 4.0.6a 13623
            {Opcode.CMSG_PET_CANCEL_AURA, 0x0F6C0}, // 4.0.6a 13623
            {Opcode.CMSG_PET_CAST_SPELL, 0x02888}, // 4.0.6a 13623 (might be 0x0B6A4)
            {Opcode.CMSG_PET_LEARN_TALENT, 0x0A7A0}, // 4.0.6a 13623
            {Opcode.CMSG_PET_NAME_QUERY, 0x0F180}, // 4.0.6a 13623
            {Opcode.CMSG_PET_RENAME, 0x038E8}, // 4.0.6a 13623
            {Opcode.CMSG_PET_SET_ACTION, 0x03C8C}, // 4.0.6a 13623
            {Opcode.CMSG_PET_SPELL_AUTOCAST, 0x0B6A4}, // 4.0.6a 13623
            {Opcode.CMSG_PET_STOP_ATTACK, 0x03A88}, // 4.0.6a 13623
            {Opcode.CMSG_PET_UNLEARN, 0x10105}, // Unknown opcode ID
            {Opcode.CMSG_PET_UNLEARN_TALENTS, 0x08F5D}, // 4.0.3a 13329
            {Opcode.CMSG_PING, 0x0064E}, // 4.0.6a 13623
            {Opcode.CMSG_PLAYED_TIME, 0x0F480}, // 4.0.6a 13623
            {Opcode.CMSG_PLAYER_AI_CHEAT, 0x100C2}, // Unknown opcode ID
            {Opcode.CMSG_PLAYER_DIFFICULTY_CHANGE, 0x03F88}, // 4.0.6a 13623
            {Opcode.CMSG_PLAYER_LOGIN, 0x08180}, // 4.0.6a 13623
            {Opcode.CMSG_PLAYER_LOGOUT, 0x0F78C}, // 4.0.6a 13623
            {Opcode.CMSG_PLAYER_VEHICLE_ENTER, 0x0AEC8}, // 4.0.6a 13623
            {Opcode.CMSG_PLAY_DANCE, 0x02288}, // 4.0.6a 13623
            {Opcode.CMSG_PUSHQUESTTOPARTY, 0x029E8}, // 4.0.6a 13623
            {Opcode.CMSG_QUERY_GUILD_REWARDS, 0x02210}, // 4.0.6a 13623
            {Opcode.CMSG_QUERY_GUILD_XP, 0x03509}, // 4.0.6a 13623
            {Opcode.CMSG_QUERY_GUILD_MEMBERS_FOR_RECIPE, 0x0210D}, // 4.0.6a 13623
            {Opcode.CMSG_QUERY_GUILD_MEMBER_RECIPES, 0x0212D}, // 4.0.6a 13623
            {Opcode.CMSG_QUERY_INSPECT_ACHIEVEMENTS, 0x028EC}, // 4.0.6a 13623
            {Opcode.CMSG_QUERY_QUESTS_COMPLETED, 0x0FE84}, // 4.0.6a 13623
            {Opcode.CMSG_QUERY_SERVER_BUCK_DATA, 0x101AC}, // Unknown opcode ID
            {Opcode.CMSG_QUERY_TIME, 0x0B1C0}, // 4.0.6a 13623
            {Opcode.CMSG_QUERY_VEHICLE_STATUS, 0x069A0}, // 4.0.6a 1623
            {Opcode.CMSG_QUESTGIVER_ACCEPT_QUEST, 0x020C4}, // 4.0.6a 13623
            {Opcode.CMSG_QUESTGIVER_CHOOSE_REWARD, 0x06AC0}, // 4.0.6a 13623
            {Opcode.CMSG_QUESTGIVER_COMPLETE_QUEST, 0x0B5AC}, // 4.0.6a 13623
            {Opcode.CMSG_QUESTGIVER_HELLO, 0x036AC}, // 4.0.6a 13623
            {Opcode.CMSG_QUESTGIVER_QUERY_QUEST, 0x02CC0}, // 4.0.6a 13623
            {Opcode.CMSG_QUESTGIVER_QUEST_AUTOLAUNCH, 0x10064}, // Unknown opcode ID
            {Opcode.CMSG_QUESTGIVER_REQUEST_REWARD, 0x023A8}, // 4.0.6a 13623 (might be 0x00E7D)
            {Opcode.CMSG_QUESTGIVER_STATUS_MULTIPLE_QUERY, 0x02DAC}, // 4.0.6a 13623
            {Opcode.CMSG_QUESTGIVER_STATUS_QUERY, 0x0FDEC}, // 4.0.6a 13623
            {Opcode.CMSG_QUESTLOG_REMOVE_QUEST, 0x0EDA8}, // 4.0.6a 13623
            {Opcode.CMSG_QUESTLOG_SWAP_QUEST, 0x10068}, // Unknown opcode ID
            {Opcode.CMSG_QUEST_CONFIRM_ACCEPT, 0x06FCC}, // 4.0.6a 13623
            {Opcode.CMSG_QUEST_NPC_QUERY, 0x00613}, // 4.0.6a 13623
            {Opcode.CMSG_QUEST_POI_QUERY, 0x07DE0}, // 4.0.6a 13623
            {Opcode.CMSG_QUEST_QUERY, 0x0EFE8}, // 4.0.6a 13623
            {Opcode.CMSG_READY_FOR_ACCOUNT_DATA_TIMES, 0x07DA8}, // 4.0.6a 13623
            {Opcode.CMSG_READ_ITEM, 0x0F3C0}, // 4.0.6a 13623
            {Opcode.CMSG_REALM_SPLIT, 0x060AC}, // 4.0.6a 13623
            {Opcode.CMSG_RECLAIM_CORPSE, 0x07CC8}, // 4.0.6a 13623
            {Opcode.CMSG_REDIRECTION_AUTH_PROOF, 0x00E4C}, // 4.0.6a 13623
            {Opcode.CMSG_REDIRECTION_FAILED, 0x10007}, // Unknown opcode ID
            {Opcode.CMSG_REDIRECT_AUTH_PROOF, 0x00100}, // 4.0.6a 13623 (might be 0x08C0A)
            {Opcode.CMSG_REFER_A_FRIEND, 0x030C4}, // 4.0.6a 13623
            {Opcode.CMSG_REFORGE_ITEM, 0x00313}, // 4.0.6a 13623
            {Opcode.CMSG_REMOVE_GLYPH, 0x101EC}, // Unknown opcode ID
            {Opcode.CMSG_REPAIR_ITEM, 0x039E4}, // 4.0.6a 13623
            {Opcode.CMSG_REPOP_REQUEST, 0x0A9E4}, // 4.0.6a 13623
            {Opcode.CMSG_REPORT_PVP_AFK, 0x0E3AC}, // 4.0.6a 13623
            {Opcode.CMSG_REQUEST_ACCOUNT_DATA, 0x0EEAC}, // 4.0.6a 13623
            {Opcode.CMSG_REQUEST_GUILD_PARTY_STATE, 0x02219}, // 4.0.6a 13623
            {Opcode.CMSG_REQUEST_HOTFIX, 0x08589}, // 4.0.6a 13623 (the client sends this after we send SMSG_HOTFIX_NOTIFY[_BLOP], only sent for the items that the player has in his inventory, that are flagged to be hotfixed)
            {Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS, 0x070C8}, // 4.0.6a 13623
            {Opcode.CMSG_REQUEST_PET_INFO, 0x0EAE4}, // 4.0.6a 13623
            {Opcode.CMSG_REQUEST_PLAYER_VEHICLE_EXIT, 0x0208C}, // 4.0.6a 13623
            {Opcode.CMSG_REQUEST_PVP_OPTIONS_ENABLED, 0x00188}, // 4.0.6a 13623
            {Opcode.CMSG_REQUEST_RAID_INFO, 0x03DE4}, // 4.0.6a 13623
            {Opcode.CMSG_REQUEST_RATED_BG_INFO, 0x08501}, // 4.0.6a 13623
            {Opcode.CMSG_REQUEST_VEHICLE_EXIT, 0x0B3CC}, // 4.0.6a 13623
            {Opcode.CMSG_REQUEST_VEHICLE_NEXT_SEAT, 0x064E4}, // 4.0.6a 13623
            {Opcode.CMSG_REQUEST_VEHICLE_PREV_SEAT, 0x02DE4}, // 4.0.6a 13623
            {Opcode.CMSG_REQUEST_VEHICLE_SWITCH_SEAT, 0x0A8CC}, // 4.0.6a 13623
            {Opcode.CMSG_RESET_FACTION_CHEAT, 0x100CD}, // Unknown opcode ID
            {Opcode.CMSG_RESET_INSTANCES, 0x0AAA0}, // 4.0.6a 13623
            {Opcode.CMSG_RESURRECT_RESPONSE, 0x0BBE8}, // 4.0.6a 13623
            {Opcode.CMSG_RETURN_TO_GRAVEYARD, 0x00593}, // 4.0.6a 13623
            {Opcode.CMSG_RUN_SCRIPT, 0x100DF}, // Unknown opcode ID
            {Opcode.CMSG_SAVE_DANCE, 0x101C3}, // Unknown opcode ID
            {Opcode.CMSG_SAVE_PLAYER, 0x1005C}, //
            {Opcode.CMSG_SELF_RES, 0x0AEC4}, // 4.0.6a 13623
            {Opcode.CMSG_SELL_ITEM, 0x063A4}, // 4.0.6a 13623
            {Opcode.CMSG_SEND_COMBAT_TRIGGER, 0x1015C}, // Unknown opcode ID
            {Opcode.CMSG_SEND_GENERAL_TRIGGER, 0x1015B}, // Unknown opcode ID
            {Opcode.CMSG_SEND_LOCAL_EVENT, 0x1015A}, // Unknown opcode ID
            {Opcode.CMSG_SEND_MAIL, 0x02DEC}, // 4.0.6a 13623
            {Opcode.CMSG_SERVERTIME, 0x10005}, // Unknown opcode ID
            {Opcode.CMSG_SERVER_BROADCAST, 0x100DE}, // Unknown opcode ID
            {Opcode.CMSG_SERVER_INFO_QUERY, 0x101F6}, // Unknown opcode ID
            {Opcode.CMSG_SETDEATHBINDPOINT, 0x0A94E}, // 4.0.6a 13623
            {Opcode.CMSG_SETSHEATHED, 0x0AAE4}, // 4.0.6a 13623
            {Opcode.CMSG_SET_ACTIONBAR_TOGGLES, 0x032C4}, // 4.0.6a 13623
            {Opcode.CMSG_SET_ACTION_BUTTON, 0x072E8}, // 4.0.6a 13623
            {Opcode.CMSG_SET_ACTIVE_MOVER, 0x06CA0}, // 4.0.6a 13623
            {Opcode.CMSG_SET_ACTIVE_VOICE_CHANNEL, 0x032A4}, // 4.0.6a 13623
            {Opcode.CMSG_SET_ALLOW_LOW_LEVEL_RAID1, 0x035EC}, // 4.0.6a 13623
            {Opcode.CMSG_SET_ALLOW_LOW_LEVEL_RAID2, 0x029C4}, // 4.0.6a 13623
            {Opcode.CMSG_SET_CHANNEL_WATCH, 0x07FE0}, // 4.0.6a 13623
            {Opcode.CMSG_SET_CONTACT_NOTES, 0x07DCC}, // 4.0.6a 13623
            {Opcode.CMSG_SET_CRITERIA_CHEAT, 0x101DD}, // Unknown opcode ID
            {Opcode.CMSG_SET_DURABILITY_CHEAT, 0x100CE}, // Unknown opcode ID
            {Opcode.CMSG_SET_EXPLORATION, 0x100E1}, // Unknown opcode ID
            {Opcode.CMSG_SET_EXPLORATION_ALL, 0x10120}, // Unknown opcode ID
            {Opcode.CMSG_SET_FACTION_ATWAR, 0x10055}, // Unknown opcode ID
            {Opcode.CMSG_SET_FACTION_CHEAT, 0x10056}, // Unknown opcode ID
            {Opcode.CMSG_SET_FACTION_INACTIVE, 0x0FE84}, // 4.0.6a 13623
            {Opcode.CMSG_SET_GLYPH, 0x101D5}, // Unknown opcode ID
            {Opcode.CMSG_SET_GLYPH_SLOT, 0x101D4}, // Unknown opcode ID
            {Opcode.CMSG_SET_GRANTABLE_LEVELS, 0x101A1}, // Unknown opcode ID
            {Opcode.CMSG_SET_GUILD_BANK_TEXT, 0x07680}, // 4.0.6a 13623
            {Opcode.CMSG_SET_PLAYER_DECLINED_NAMES, 0x0ADC0}, // 4.0.6a 13623
            {Opcode.CMSG_SET_PRIMARY_TALENT_TREE, 0x0FEA8}, // 4.0.6a 13623
            {Opcode.CMSG_SET_PVP_RANK_CHEAT, 0x100CF}, // Unknown opcode ID
            {Opcode.CMSG_SET_PVP_TITLE, 0x100D2}, // Unknown opcode ID
            {Opcode.CMSG_SET_RUNE_COOLDOWN, 0x101CC}, // Unknown opcode ID
            {Opcode.CMSG_SET_RUNE_COUNT, 0x101CB}, // Unknown opcode ID
            {Opcode.CMSG_SET_SAVED_INSTANCE_EXTEND, 0x02588}, // 4.0.6a 13623
            {Opcode.CMSG_SET_SELECTION, 0x06488}, // 4.0.6a 13623
            {Opcode.CMSG_SET_SKILL_CHEAT, 0x10085}, // Unknown opcode ID
            {Opcode.CMSG_SET_TAXI_BENCHMARK_MODE, 0x073C4}, // 4.0.6a 13623
            {Opcode.CMSG_SET_TITLE, 0x0EC8C}, // 4.0.6a 13623
            {Opcode.CMSG_SET_TITLE_SUFFIX, 0x10196}, // Unknown opcode ID
            {Opcode.CMSG_SET_TRADE_GOLD, 0x00C13}, // 4.0.6a 13623
            {Opcode.CMSG_SET_TRADE_ITEM, 0x00A11}, // 4.0.6a 13623
            {Opcode.CMSG_SET_WATCHED_FACTION, 0x035C8}, // 4.0.6a 13623
            {Opcode.CMSG_SHOWING_CLOAK, 0x07AA4}, // 4.0.6a 13623
            {Opcode.CMSG_SHOWING_HELM, 0x0B7C0}, // 4.0.6a 13623
            {Opcode.CMSG_SKILL_BUY_RANK, 0x1009F}, // Unknown opcode ID
            {Opcode.CMSG_SKILL_BUY_STEP, 0x1009E}, // Unknown opcode ID
            {Opcode.CMSG_SOCKET_GEMS, 0x076C4}, // 4.0.6a 13623
            {Opcode.CMSG_SPELLCLICK, 0x0F9A4}, // 4.0.6a 13623
            {Opcode.CMSG_SPIRIT_HEALER_ACTIVATE, 0x0F3AC}, // 4.0.6a 13623
            {Opcode.CMSG_SPLIT_ITEM, 0x0FDAC}, // 4.0.6a 13623
            {Opcode.CMSG_STABLE_CHANGE_SLOT, 0x00291}, // 4.0.6a 13623
            {Opcode.CMSG_STABLE_REVIVE_PET, 0x100C7}, // Unknown opcode ID
            {Opcode.CMSG_STANDSTATECHANGE, 0x0FC88}, // 4.0.6a 13623
            {Opcode.CMSG_STOP_DANCE, 0x03080}, // 4.0.6a 13623
            {Opcode.CMSG_STORE_LOOT_IN_SLOT, 0x07FCC}, // 4.0.6a 13623
            {Opcode.CMSG_SUMMON_RESPONSE, 0x06BA0}, // 4.0.6a 13623
            {Opcode.CMSG_SWAP_INV_ITEM, 0x03EC4}, // 4.0.6a 13623
            {Opcode.CMSG_SWAP_ITEM, 0x0E8AC}, // 4.0.6a 13623
            {Opcode.CMSG_SYNC_DANCE, 0x101C6}, // Unknown opcode ID
            {Opcode.CMSG_TARGET_CAST, 0x10183}, // Unknown opcode ID
            {Opcode.CMSG_TARGET_SCRIPT_CAST, 0x10184}, // Unknown opcode ID
            {Opcode.CMSG_TAXICLEARALLNODES, 0x1006C}, // Never used
            {Opcode.CMSG_TAXICLEARNODE, 0x100AD}, // Unknown opcode ID
            {Opcode.CMSG_TAXIENABLEALLNODES, 0x1006D}, // Never used
            {Opcode.CMSG_TAXIENABLENODE, 0x100AE}, // Unknown opcode ID
            {Opcode.CMSG_TAXINODE_STATUS_QUERY, 0x0A1EC}, // 4.0.6a 13623
            {Opcode.CMSG_TAXIQUERYAVAILABLENODES, 0x0BE8E}, // 4.0.6a 13623
            {Opcode.CMSG_TAXISHOWNODES, 0x0B8E8}, // 4.0.6a 13623
            {Opcode.CMSG_TELEPORT_TO_UNIT, 0x0E1AC}, // 4.0.6a 13623
            {Opcode.CMSG_TEXT_EMOTE, 0x0E9E0}, // 4.0.6a 13623
            {Opcode.CMSG_TIME_SYNC_RESP, 0x0A8AC}, // 4.0.6a 13623
            {Opcode.CMSG_TOGGLE_PVP, 0x06480}, // 4.0.6a 13623
            {Opcode.CMSG_TOTEM_DESTROYED, 0x034A0}, // 4.0.6a 13623
            {Opcode.CMSG_TRAINER_BUY_SPELL, 0x0FDC8}, // 4.0.6a 13623
            {Opcode.CMSG_TRAINER_LIST, 0x0E5AC}, // 4.0.6a 13623
            {Opcode.CMSG_TRANSFORM, 0x04577}, // 4.0.3a 13329
            {Opcode.CMSG_TRIGGER_CINEMATIC_CHEAT, 0x1003D}, // Unknown opcode ID
            {Opcode.CMSG_TURN_IN_PETITION, 0x0A584}, // 4.0.6a 13623
            {Opcode.CMSG_TUTORIAL_CLEAR, 0x0A5E4}, // 4.0.6a 13623
            {Opcode.CMSG_TUTORIAL_FLAG, 0x0E4CC}, // 4.0.6a 13623
            {Opcode.CMSG_TUTORIAL_RESET, 0x0DE75}, // 4.0.6a 13623
            {Opcode.CMSG_UI_TIME_REQUEST, 0x03FA8}, // 4.0.6a 13623
            {Opcode.CMSG_UNACCEPT_TRADE, 0x00811}, // 4.0.6a 13623
            {Opcode.CMSG_UNITANIMTIER_CHEAT, 0x101DE}, // Unknown opcode ID
            {Opcode.CMSG_UNKNOWN_1296, 0x10009}, // Unknown opcode ID
            {Opcode.CMSG_UNKNOWN_1303, 0x06BE0}, // 4.0.6a 13623
            {Opcode.CMSG_UNKNOWN_1320, 0x1025C}, // Unknown opcode ID/Name
            {Opcode.CMSG_UNLEARN_DANCE_MOVE, 0x101CA}, // Unknown opcode ID
            {Opcode.CMSG_UNLEARN_SKILL, 0x063C8}, // 4.0.6a 13623
            {Opcode.CMSG_UNLEARN_SPELL, 0x10090}, // Unknown opcode ID
            {Opcode.CMSG_UNLEARN_TALENTS, 0x10098}, // Unknown opcode ID
            {Opcode.CMSG_UNUSED2, 0x10058}, // Unknown opcode ID
            {Opcode.CMSG_UPDATE_ACCOUNT_DATA, 0x072A4}, // 4.0.6a 13623
            {Opcode.CMSG_UPDATE_MISSILE_TRAJECTORY, 0x00E54}, // 4.0.3a 13329
            {Opcode.CMSG_UPDATE_PROJECTILE_POSITION, 0x0EF7F}, // 4.0.3a 13329
            {Opcode.CMSG_USE_ITEM, 0x07080}, // 4.0.6a 13623
            {Opcode.CMSG_VOICE_SESSION_ENABLE, 0x0FEA4}, // 4.0.6a 13623
            {Opcode.CMSG_VOICE_SET_TALKER_MUTED_REQUEST, 0x10166}, // Unknown opcode ID
            {Opcode.CMSG_WARDEN_DATA, 0x02F84}, // 4.0.6a 13623
            {Opcode.CMSG_WARGAME_ACCEPT, 0x00108}, // 4.0.6a 13623
            {Opcode.CMSG_WARGAME_REQUEST, 0x00501}, // 4.0.6a 13623
            {Opcode.CMSG_WHO, 0x0A4CC}, // 4.0.6a 13623
            {Opcode.CMSG_WHOIS, 0x02180}, // 4.0.6a 13623
            {Opcode.CMSG_WORLD_STATE_UI_TIMER_UPDATE, 0x03FA8}, // 4.0.6a 13623
            {Opcode.CMSG_WORLD_TELEPORT, 0x08581}, // 4.0.6a 13623
            {Opcode.CMSG_WRAP_ITEM, 0x07CC4}, // 4.0.6a 13623
            {Opcode.CMSG_ZONEUPDATE, 0x033E4}, // 4.0.6a 13623
            {Opcode.MSG_AUCTION_HELLO, 0x0B3A0}, // 4.0.6a 13623
            {Opcode.MSG_CHANNEL_START, 0x02BAC}, // 4.0.6a 13623
            {Opcode.MSG_CHANNEL_UPDATE, 0x062AC}, // 4.0.6a 13623
            {Opcode.MSG_CORPSE_QUERY, 0x0E0C8}, // 4.0.6a 13623 (might be 0x0F388)
            {Opcode.MSG_DELAY_GHOST_TELEPORT, 0x10127}, // 4.0.6a 13623 0x03729
            {Opcode.MSG_GM_ACCOUNT_ONLINE, 0x100C3}, // Unknown opcode ID
            {Opcode.MSG_GM_BIND_OTHER, 0x1008B}, // Unknown opcode ID
            {Opcode.MSG_GM_CHANGE_ARENA_RATING, 0x101A3}, // Unknown opcode ID
            {Opcode.MSG_GM_DESTROY_CORPSE, 0x10117}, // Unknown opcode ID
            {Opcode.MSG_GM_GEARRATING, 0x10173}, // Unknown opcode ID
            {Opcode.MSG_GM_RESETINSTANCELIMIT, 0x1012F}, // Unknown opcode ID
            {Opcode.MSG_GM_SHOWLABEL, 0x1008D}, // Unknown opcode ID
            {Opcode.MSG_GM_SUMMON, 0x1008C}, // Unknown opcode ID
            {Opcode.MSG_GUILD_BANK_LOG_QUERY, 0x0F584}, // 4.0.6a 13623
            {Opcode.MSG_GUILD_BANK_MONEY_WITHDRAWN, 0x06CE4}, // 4.0.6a 13623
            {Opcode.MSG_GUILD_EVENT_LOG_QUERY, 0x069EC}, // 4.0.6a 13623
            {Opcode.MSG_GUILD_PERMISSIONS, 0x0F4C4}, // 4.0.6a 13623
            {Opcode.MSG_INSPECT_ARENA_TEAMS, 0x0FDA4}, // 4.0.6a 13623
            {Opcode.MSG_LIST_STABLED_PETS, 0x06EAC}, // 4.0.6a 13623
            {Opcode.MSG_MINIMAP_PING, 0x033A0}, // 4.0.6a 13623 (might be 0x0C475)
            {Opcode.MSG_MOVE_FALL_LAND, 0x0AFAC}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_FEATHER_FALL, 0x0B6A8}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_HEARTBEAT, 0x022EC}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_HOVER, 0x02FCC}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_JUMP, 0x065AC}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_KNOCK_BACK, 0x0B0E8}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_ROOT, 0x0ABA8}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_SET_ALL_SPEED_CHEAT, 0x10025}, // Unknown opcode ID
            {Opcode.MSG_MOVE_SET_FACING, 0x0ABC4}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_SET_FLIGHT_BACK_SPEED, 0x0B484}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_SET_FLIGHT_BACK_SPEED_CHEAT, 0x10150}, // Unknown opcode ID
            {Opcode.MSG_MOVE_SET_FLIGHT_SPEED, 0x0B088}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_SET_FLIGHT_SPEED_CHEAT, 0x1014F}, // Unknown opcode ID
            {Opcode.MSG_MOVE_SET_PITCH, 0x0EBA0}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_SET_PITCH_RATE, 0x0ABA4}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_SET_PITCH_RATE_CHEAT, 0x101CD}, // Unknown opcode ID
            {Opcode.MSG_MOVE_SET_RAW_POSITION_ACK, 0x0F4A0}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_SET_RUN_BACK_SPEED, 0x0B5EC}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_SET_RUN_BACK_SPEED_CHEAT, 0x10021}, // Unknown opcode ID
            {Opcode.MSG_MOVE_SET_RUN_MODE, 0x0E7A8}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_SET_RUN_SPEED, 0x064A0}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_SET_RUN_SPEED_CHEAT, 0x10020}, // Unknown opcode ID
            {Opcode.MSG_MOVE_SET_SWIM_BACK_SPEED, 0x0B0AC}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_SET_SWIM_BACK_SPEED_CHEAT, 0x10024}, // Unknown opcode ID
            {Opcode.MSG_MOVE_SET_SWIM_SPEED, 0x02380}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_SET_SWIM_SPEED_CHEAT, 0x10023}, // Unknown opcode ID
            {Opcode.MSG_MOVE_SET_TURN_RATE, 0x0A3A8}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_SET_TURN_RATE_CHEAT, 0x10026}, // Unknown opcode ID
            {Opcode.MSG_MOVE_SET_WALK_MODE, 0x03FAC}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_SET_WALK_SPEED, 0x0F284}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_SET_WALK_SPEED_CHEAT, 0x10022}, // Unknown opcode ID
            {Opcode.MSG_MOVE_START_ASCEND, 0x0BDC0}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_START_BACKWARD, 0x072E4}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_START_DESCEND, 0x07880}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_START_FORWARD, 0x0EBAC}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_START_PITCH_DOWN, 0x0ADC4}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_START_PITCH_UP, 0x060E4}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_START_STRAFE_LEFT, 0x060E8}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_START_STRAFE_RIGHT, 0x07DA4}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_START_SWIM, 0x026C0}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_START_SWIM_CHEAT, 0x026C0}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_START_TURN_LEFT, 0x0B8C8}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_START_TURN_RIGHT, 0x0F9E4}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_STOP, 0x034E0}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_STOP_ASCEND, 0x0FCA8}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_STOP_PITCH, 0x028E8}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_STOP_STRAFE, 0x0F9A8}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_STOP_SWIM, 0x0FDE8}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_STOP_SWIM_CHEAT, 0x06988}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_STOP_TURN, 0x0E2E8}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_TELEPORT, 0x024E4}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_TELEPORT_ACK, 0x06DAC}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_TELEPORT_CHEAT, 0x0E7EC}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_TIME_SKIPPED, 0x025E4}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_TOGGLE_COLLISION_CHEAT, 0x000D9}, // Unknown opcode ID (might be 0x00028)
            {Opcode.MSG_MOVE_TOGGLE_FALL_LOGGING, 0x1001F}, // Unknown opcode ID
            {Opcode.MSG_MOVE_TOGGLE_GRAVITY_CHEAT, 0x100DD}, // Unknown opcode ID
            {Opcode.MSG_MOVE_TOGGLE_LOGGING, 0x1001D}, // Unknown opcode ID
            {Opcode.MSG_MOVE_UNROOT, 0x02088}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_UPDATE_CAN_FLY, 0x025E0}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_WATER_WALK, 0x0E1A4}, // 4.0.6a 13623
            {Opcode.MSG_MOVE_WORLDPORT_ACK, 0x02FC0}, // 4.0.6a 13623
            {Opcode.MSG_NOTIFY_PARTY_SQUELCH, 0x0E8E8}, // 4.0.6a 13623
            {Opcode.MSG_PARTY_ASSIGNMENT, 0x028AC}, // 4.0.6a 13623
            {Opcode.MSG_PETITION_DECLINE, 0x032E0}, // 4.0.6a 13623
            {Opcode.MSG_PETITION_RENAME, 0x070C0}, // 4.0.6a 13623
            {Opcode.MSG_PVP_LOG_DATA, 0x00C0E}, // 4.0.6a 13623
            {Opcode.MSG_QUERY_GUILD_BANK_TEXT, 0x0A2C4}, // 4.0.6a 13623
            {Opcode.MSG_QUERY_NEXT_MAIL_TIME, 0x025C8}, // 4.0.6a 13623
            {Opcode.MSG_QUEST_PUSH_RESULT, 0x022A4}, // 4.0.6a 13623
            {Opcode.MSG_RAID_READY_CHECK, 0x0FDC0}, // 4.0.6a 13623
            {Opcode.MSG_RAID_READY_CHECK_CONFIRM, 0x0A2AC}, // 4.0.6a 13623
            {Opcode.MSG_RAID_READY_CHECK_FINISHED, 0x0A0A0}, // 4.0.6a 13623
            {Opcode.MSG_RAID_TARGET_UPDATE, 0x0A5AC}, // 4.0.6a 13623
            {Opcode.MSG_RANDOM_ROLL, 0x0B7A4}, // 4.0.6a 13623
            {Opcode.MSG_SAVE_GUILD_EMBLEM, 0x031AC}, // 4.0.6a 13623
            {Opcode.MSG_SET_DUNGEON_DIFFICULTY, 0x074E0}, // 4.0.6a 13623
            {Opcode.MSG_SET_RAID_DIFFICULTY, 0x0B5E8}, // 4.0.6a 13623
            {Opcode.MSG_TABARDVENDOR_ACTIVATE, 0x02C80}, // 4.0.6a 13623
            {Opcode.MSG_TALENT_WIPE_CONFIRM, 0x0BFC4}, // 4.0.6a 13623
            {Opcode.OBSOLETE_DROP_ITEM, 0x10049}, // Unknown opcode ID
            {Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x07280}, // 4.0.6a 13623
            {Opcode.SMSG_ACHIEVEMENT_DELETED, 0x0E6A0}, // 4.0.6a 13623
            {Opcode.SMSG_ACHIEVEMENT_EARNED, 0x0F5E4}, // 4.0.6a 13623
            {Opcode.SMSG_ACTION_BUTTONS, 0x02CCC}, // 4.0.6a 13623
            {Opcode.SMSG_ACTIVATETAXIREPLY, 0x07A84}, // 4.0.6a 13623
            {Opcode.SMSG_ADDON_INFO, 0x0EA80}, // 4.0.6a 13623
            {Opcode.SMSG_ADD_RUNE_POWER, 0x0F5E8}, // 4.0.6a 13623
            {Opcode.SMSG_AI_REACTION, 0x031CC}, // 4.0.6a 13623
            {Opcode.SMSG_ALL_ACHIEVEMENT_DATA, 0x0445E}, // 4.0.6a 13623
            {Opcode.SMSG_AREA_SPIRIT_HEALER_TIME, 0x06D80}, // 4.0.6a 13623
            {Opcode.SMSG_AREA_TRIGGER_MESSAGE, 0x0EAE0}, // 4.0.6a 13623
            {Opcode.SMSG_ARENA_ERROR, 0x02FEC}, // 4.0.6a 13623
            {Opcode.SMSG_ARENA_OPPONENT_UPDATE, 0x028CC}, // 4.0.6a 13623
            {Opcode.SMSG_ARENA_TEAM_CHANGE_FAILED_QUEUED, 0x0B2A0}, // 4.0.6a 13623
            {Opcode.SMSG_ARENA_TEAM_COMMAND_RESULT, 0x0040C}, // 4.0.6a 13623
            {Opcode.SMSG_ARENA_TEAM_EVENT, 0x074C4}, // 4.0.6a 13623
            {Opcode.SMSG_ARENA_TEAM_INVITE, 0x063A0}, // 4.0.6a 13623
            {Opcode.SMSG_ARENA_TEAM_QUERY_RESPONSE, 0x03DE8}, // 4.0.6a 13623
            {Opcode.SMSG_ARENA_TEAM_ROSTER, 0x0EC80}, // 4.0.6a 13623
            {Opcode.SMSG_ARENA_TEAM_STATS, 0x0FA80}, // 4.0.6a 13623
            {Opcode.SMSG_ATTACKERSTATEUPDATE, 0x0BBC0}, // 4.0.6a 13623
            {Opcode.SMSG_ATTACKSTART, 0x0B68C}, // 4.0.6a 13623
            {Opcode.SMSG_ATTACKSTOP, 0x06DCC}, // 4.0.6a 13623 (might be 0x035E0) FIXME SMSG_STANDSTATE_CHANGE_FAILURE_OBSOLETE
            {Opcode.SMSG_ATTACKSWING_BADFACING, 0x067A8}, // 4.0.6a 13623
            {Opcode.SMSG_ATTACKSWING_CANT_ATTACK, 0x06188}, // 4.0.6a 13623
            {Opcode.SMSG_ATTACKSWING_DEADTARGET, 0x0A7C4}, // 4.0.6a 13623
            {Opcode.SMSG_ATTACKSWING_NOTINRANGE, 0x036C4}, // 4.0.6a 13623
            {Opcode.SMSG_AUCTION_BIDDER_LIST_RESULT, 0x0E5A8}, // 4.0.6a 13623 (might be 0x0A1A8)
            {Opcode.SMSG_AUCTION_BIDDER_NOTIFICATION, 0x0ACEC}, // 4.0.6a 13623
            {Opcode.SMSG_AUCTION_COMMAND_RESULT, 0x0AAE0}, // 4.0.6a 13623
            {Opcode.SMSG_AUCTION_LIST_PENDING_SALES, 0x0BAC4}, // 4.0.6a 13623
            {Opcode.SMSG_AUCTION_LIST_RESULT, 0x0A1A8}, // 4.0.6a 13623 (might be 0x0E5A8)
            {Opcode.SMSG_AUCTION_OWNER_LIST_RESULT, 0x03CA8}, // 4.0.6a 13623
            {Opcode.SMSG_AUCTION_OWNER_NOTIFICATION, 0x06C80}, // 4.0.6a 13623
            {Opcode.SMSG_AUCTION_REMOVED_NOTIFICATION, 0x0B1EC}, // 4.0.6a 13623
            {Opcode.SMSG_AURACASTLOG, 0x10080}, // Unknown opcode ID
            {Opcode.SMSG_AURA_UPDATE, 0x065C0}, // 4.0.6a 13623
            {Opcode.SMSG_AURA_UPDATE_ALL, 0x037E0}, // 4.0.6a 13623
            {Opcode.SMSG_AUTH_CHALLENGE, 0x06019}, // 4.0.6a 13623
            {Opcode.SMSG_AUTH_RESPONSE, 0x0B28C}, // 4.0.6a 13623
            {Opcode.SMSG_AVAILABLE_VOICE_CHANNEL, 0x0F8C8}, // 4.0.6a 13623
            {Opcode.SMSG_BARBER_SHOP_RESULT, 0x03188}, // 4.0.6a 13623
            {Opcode.SMSG_BATTLEFIELD_LIST, 0x490C}, // 4.0.6a 13623
            {Opcode.SMSG_BATTLEFIELD_MGR_EJECTED, 0x04C1C}, // 4.0.6a 13623 (might be 0x0011A)
            {Opcode.SMSG_BATTLEFIELD_MGR_EJECT_PENDING, 0x0045E}, // 4.0.6a 13623 (might be 0x0011B)
            {Opcode.SMSG_BATTLEFIELD_MGR_ENTERED, 0x0415C}, // 4.0.6a 13623 (might be 0x00004)
            {Opcode.SMSG_BATTLEFIELD_MGR_ENTRY_INVITE, 0x0455E}, // 4.0.6a 13623
            {Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_INVITE, 0x0054C}, // 4.0.6a 13623
            {Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_REQUEST_RESPONSE, 0x00C0C}, // 4.0.6a 13623 (might be 0x0011C)
            {Opcode.SMSG_BATTLEFIELD_MGR_STATE_CHANGE, 0x0485C}, // 4.0.6a 13623 (might be 0x0352D)
            {Opcode.SMSG_BATTLEFIELD_PORT_DENIED, 0x10059}, // Unknown opcode ID
            {Opcode.SMSG_BATTLEGROUND_EXIT_QUEUE, 0x0454C}, // 4.0.6a 13623
            {Opcode.SMSG_BATTLEGROUND_INFO_THROTTLED, 0x004A6}, // 4.0.6a 13623
            {Opcode.SMSG_BATTLEGROUND_IN_PROGRESS, 0x0051E}, // 4.0.6a 13623
            {Opcode.SMSG_BATTLEGROUND_PLAYER_JOINED, 0x0494C}, // 4.0.6a 13623
            {Opcode.SMSG_BATTLEGROUND_PLAYER_LEFT, 0x00D1C}, // 4.0.6a 13623
            {Opcode.SMSG_BATTLEGROUND_PLAYER_POSITIONS, 0x0045C}, // 4.0.6a 13623
            {Opcode.SMSG_BATTLEGROUND_WAIT_JOIN, 0x0081C}, // 4.0.6a 13623
            {Opcode.SMSG_BATTLEGROUND_WAIT_LEAVE, 0x04C4C}, // 4.0.6a 13623
            {Opcode.SMSG_BINDER_CONFIRM, 0x033C4}, // 4.0.6a 13623
            {Opcode.SMSG_BINDPOINTUPDATE, 0x0A9A0}, // 4.0.6a 13623
            {Opcode.SMSG_BINDZONEREPLY, 0x0ACAC}, // 4.0.6a 13623
            {Opcode.SMSG_BREAK_TARGET, 0x02488}, // 4.0.6a 13623
            {Opcode.SMSG_BUY_FAILED, 0x06CE8}, // 4.0.6a 13623
            {Opcode.SMSG_BUY_ITEM, 0x069CC}, // 4.0.6a 13623
            {Opcode.SMSG_CALENDAR_ACTION_PENDING, 0x07E8C}, // 4.0.6a 13623 (might be 0x0265E)
            {Opcode.SMSG_CALENDAR_ARENA_TEAM, 0x021E0}, // 4.0.6a 13623
            {Opcode.SMSG_CALENDAR_COMMAND_RESULT, 0x0AD80}, // 4.0.6a 13623
            {Opcode.SMSG_CALENDAR_EVENT_INVITE, 0x0A5A0}, // 4.0.6a 13623
            {Opcode.SMSG_CALENDAR_EVENT_INVITE_ALERT, 0x0AFA8}, // 4.0.6a 13623
            {Opcode.SMSG_CALENDAR_EVENT_INVITE_REMOVED, 0x0A9E0}, // 4.0.6a 13623
            {Opcode.SMSG_CALENDAR_EVENT_INVITE_REMOVED_ALERT, 0x0FEE0}, // 4.0.6a 13623
            {Opcode.SMSG_CALENDAR_EVENT_INVITE_STATUS_ALERT, 0x0F7C8}, // 4.0.6a 13623
            {Opcode.SMSG_CALENDAR_EVENT_MODERATOR_STATUS_ALERT, 0x0ABC8}, // 4.0.6a 13623
            {Opcode.SMSG_CALENDAR_EVENT_REMOVED_ALERT, 0x06288}, // 4.0.6a 13623
            {Opcode.SMSG_CALENDAR_EVENT_STATUS, 0x07EC0}, // 4.0.6a 13623
            {Opcode.SMSG_CALENDAR_EVENT_UPDATED_ALERT, 0x029C0}, // 4.0.6a 13623
            {Opcode.SMSG_CALENDAR_FILTER_GUILD, 0x02FC4}, // 4.0.6a 13623
            {Opcode.SMSG_CALENDAR_RAID_LOCKOUT_ADDED, 0x02AE0}, // 4.0.6a 13623
            {Opcode.SMSG_CALENDAR_RAID_LOCKOUT_REMOVED, 0x0FCE0}, // 4.0.6a 13623
            {Opcode.SMSG_CALENDAR_SEND_CALENDAR, 0x0B0A4}, // 4.0.6a 13623
            {Opcode.SMSG_CALENDAR_SEND_EVENT, 0x0FAA4}, // 4.0.6a 13623
            {Opcode.SMSG_CALENDAR_SEND_NUM_PENDING, 0x0A8E0}, // 4.0.6a 13623
            {Opcode.SMSG_CALENDAR_UPDATE_INVITE_LIST, 0x071A0}, // 4.0.6a 13623
            {Opcode.SMSG_CALENDAR_UPDATE_INVITE_LIST2, 0x0EDC4}, // 4.0.6a 13623
            {Opcode.SMSG_CALENDAR_UPDATE_INVITE_LIST3, 0x0A2A0}, // 4.0.6a 13623
            {Opcode.SMSG_CAMERA_SHAKE, 0x0A2E4}, // 4.0.6a 13623
            {Opcode.SMSG_CANCEL_AUTO_REPEAT, 0x03DE0}, // 4.0.6a 13623
            {Opcode.SMSG_CANCEL_COMBAT, 0x0BAE4}, // 4.0.6a 13623
            {Opcode.SMSG_CAST_FAILED, 0x02A8C}, // 4.0.6a 13623
            {Opcode.SMSG_CHANNEL_LIST, 0x069E0}, // 4.0.6a 13623
            {Opcode.SMSG_CHANNEL_MEMBER_COUNT, 0x02CEC}, // 4.0.6a 13623
            {Opcode.SMSG_CHANNEL_NOTIFY, 0x07CAC}, // 4.0.6a 13623
            {Opcode.SMSG_CHARACTER_LOGIN_FAILED, 0x07ACC}, // 4.0.6a 13623
            {Opcode.SMSG_CHARACTER_PROFILE, 0x1012D}, // Unknown opcode ID
            {Opcode.SMSG_CHARACTER_PROFILE_REALM_CONNECTED, 0x1012E}, // Unknown opcode ID
            {Opcode.SMSG_CHAR_CREATE, 0x0F7EC}, // 4.0.6a 13623
            {Opcode.SMSG_CHAR_CUSTOMIZE, 0x02DA4}, // 4.0.6a 13623
            {Opcode.SMSG_CHAR_DELETE, 0x0BC80}, // 4.0.6a 13623
            {Opcode.SMSG_CHAR_ENUM, 0x0ECCC}, // 4.0.6a 13623
            {Opcode.SMSG_CHAR_FACTION_CHANGE, 0x023AC}, // 4.0.6a 13623
            {Opcode.SMSG_CHAR_RENAME, 0x0E0EC}, // 4.0.6a 13623
            {Opcode.SMSG_CHAT_PLAYER_AMBIGUOUS, 0x06AE8}, // 4.0.6a 13623
            {Opcode.SMSG_CHAT_PLAYER_NOT_FOUND, 0x035A0}, // 4.0.6a 13623
            {Opcode.SMSG_CHAT_RESTRICTED, 0x03EC8}, // 4.0.6a 13623
            {Opcode.SMSG_CHAT_WRONG_FACTION, 0x0BB88}, // 4.0.6a 13623
            {Opcode.SMSG_CHEAT_DUMP_ITEMS_DEBUG_ONLY_RESPONSE, 0x10160}, // Unknown opcode ID
            {Opcode.SMSG_CHEAT_DUMP_ITEMS_DEBUG_ONLY_RESPONSE_WRITE_FILE, 0x10161}, // Unknown opcode ID
            {Opcode.SMSG_CHEAT_PLAYER_LOOKUP, 0x1017F}, // Unknown opcode ID
            {Opcode.SMSG_CHECK_FOR_BOTS, 0x0FEC8}, // 4.0.6a 13623
            {Opcode.SMSG_CLEAR_COOLDOWN, 0x036CC}, // 4.0.6a 13623
            {Opcode.SMSG_CLEAR_EXTRA_AURA_INFO_OBSOLETE, 0x1016B}, // Unknown opcode ID
            {Opcode.SMSG_CLEAR_FAR_SIGHT_IMMEDIATE, 0x0BEE4}, // 4.0.6a 13623
            {Opcode.SMSG_CLEAR_TARGET, 0x0A7AC}, // 4.0.6a 13623
            {Opcode.SMSG_CLIENTCACHE_VERSION, 0x02EC4}, // 4.0.6a 13623
            {Opcode.SMSG_CLIENT_CONTROL_UPDATE, 0x03C84}, // 4.0.6a 13623
            {Opcode.SMSG_COMBAT_LOG_MULTIPLE, 0x033A8}, // 4.0.6a 13623
            {Opcode.SMSG_COMMENTATOR_GET_PLAYER_INFO, 0x0758C}, // 4.0.6a 13623 FIXME (SMSG_COMMENTATOR_PLAYER_INFO)
            {Opcode.SMSG_COMMENTATOR_MAP_INFO, 0x0A4E8}, // 4.0.6a 13623
            {Opcode.SMSG_COMMENTATOR_PLAYER_INFO, 0x0758C}, // 4.0.6a 13623 FIXME (SMSG_COMMENTATOR_GET_PLAYER_INFO)
            {Opcode.SMSG_COMMENTATOR_STATE_CHANGED, 0x0E0E4}, // 4.0.6a 13623
            {Opcode.SMSG_COMPLAIN_RESULT, 0x070CC}, // 4.0.6a 13623
            {Opcode.SMSG_COMPRESSED_ACHIEVEMENT_DATA, 0x0C1B0}, // 4.0.6a 13623
            {Opcode.SMSG_COMPRESSED_GUILD_ROSTER, 0x9170}, // 4.0.6a 13623
            {Opcode.SMSG_COMPRESSED_MOVES, 0x06FE4}, // 4.0.6a 13623
            {Opcode.SMSG_COMPRESSED_RESPOND_INSPECT_ACHIEVEMENTS, 0x09130}, // 4.0.6a 13623
            {Opcode.SMSG_COMPRESSED_UPDATE_OBJECT, 0x0EAC0}, // 4.0.6a 13623
            {Opcode.SMSG_COMSAT_CONNECT_FAIL, 0x029A0}, // 4.0.6a 13623
            {Opcode.SMSG_COMSAT_DISCONNECT, 0x0AAC8}, // 4.0.6a 13623
            {Opcode.SMSG_COMSAT_RECONNECT_TRY, 0x0E880}, // 4.0.6a 13623
            {Opcode.SMSG_CONTACT_LIST, 0x0748C}, // 4.0.6a 13623
            {Opcode.SMSG_CONVERT_RUNE, 0x0B4AC}, // 4.0.6a 13623
            {Opcode.SMSG_COOLDOWN_CHEAT, 0x0AAA4}, // 4.0.6a 13623
            {Opcode.SMSG_COOLDOWN_EVENT, 0x0EAEC}, // 4.0.6a 13623
            {Opcode.SMSG_CORPSE_MAP_POSITION_QUERY_RESPONSE, 0x1020A}, // Unknown opcode ID
            {Opcode.SMSG_CORPSE_NOT_IN_INSTANCE, 0x031E4}, // 4.0.6a 13623
            {Opcode.SMSG_CORPSE_RECLAIM_DELAY, 0x031C4}, // 4.0.6a 13623
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x0E6AC}, // 4.0.6a 13623
            {Opcode.SMSG_CRITERIA_DELETED, 0x0AF84}, // 4.0.6a 13623
            {Opcode.SMSG_CRITERIA_UPDATE, 0x0AFC8}, // 4.0.6a 13623 (might be 0x00470)
            {Opcode.SMSG_CROSSED_INEBRIATION_THRESHOLD, 0x0A580}, // 4.0.6a 13623
            {Opcode.SMSG_DAMAGE_CALC_LOG, 0x0FD84}, // 4.0.6a 13623
            {Opcode.SMSG_DAMAGE_DONE_OBSOLETE, 0x1005A}, // Unknown opcode ID
            {Opcode.SMSG_DANCE_QUERY_RESPONSE, 0x031A8}, // 4.0.6a 13623
            {Opcode.SMSG_DB_REPLY, 0x00C4E}, // 4.0.6a 13623 (was SMSG_ITEM_QUERY_SINGLE_RESPONSE)
            {Opcode.SMSG_DEATH_RELEASE_LOC, 0x033C8}, // 4.0.6a 13623
            {Opcode.SMSG_DEBUGAURAPROC, 0x100B6}, // Unknown opcode ID
            {Opcode.SMSG_DEBUG_LIST_TARGETS, 0x10188}, // Unknown opcode ID
            {Opcode.SMSG_DEFENSE_MESSAGE, 0x065EC}, // 4.0.6a 13623
            {Opcode.SMSG_DESTROY_OBJECT, 0x02AA0}, // 4.0.6a 13623
            {Opcode.SMSG_DESTRUCTIBLE_BUILDING_DAMAGE, 0x0A0E0}, // 4.0.6a 13623
            {Opcode.SMSG_DISMOUNT, 0x03CC4}, // 4.0.6a 13623
            {Opcode.SMSG_DISMOUNTRESULT, 0x0F9C0}, // 4.0.6a 13623
            {Opcode.SMSG_DISPEL_FAILED, 0x0AAE8}, // 4.0.6a 13623
            {Opcode.SMSG_DUEL_COMPLETE, 0x0FCE8}, // 4.0.6a 13623
            {Opcode.SMSG_DUEL_COUNTDOWN, 0x0E8E0}, // 4.0.6a 13623
            {Opcode.SMSG_DUEL_INBOUNDS, 0x0ECA4}, // 4.0.6a 13623
            {Opcode.SMSG_DUEL_OUTOFBOUNDS, 0x068C0}, // 4.0.6a 13623
            {Opcode.SMSG_DUEL_REQUESTED, 0x03FC8}, // 4.0.6a 13623
            {Opcode.SMSG_DUEL_WINNER, 0x079E4}, // 4.0.6a 13623
            {Opcode.SMSG_DUMP_OBJECTS_DATA, 0x101EE}, // Unknown opcode ID
            {Opcode.SMSG_DURABILITY_DAMAGE_DEATH, 0x0FDA0}, // 4.0.6a 13623
            {Opcode.SMSG_DYNAMIC_DROP_ROLL_RESULT, 0x101D7}, // Unknown opcode ID
            {Opcode.SMSG_ECHO_PARTY_SQUELCH, 0x02AC4}, // 4.0.6a 13623
            {Opcode.SMSG_EMOTE, 0x0EEA0}, // 4.0.6a 13623
            {Opcode.SMSG_ENABLE_BARBER_SHOP, 0x037E8}, // 4.0.6a 13623
            {Opcode.SMSG_ENCHANTMENTLOG, 0x0F5AC}, // 4.0.6a 13623
            {Opcode.SMSG_ENVIRONMENTALDAMAGELOG, 0x0E1C8}, // 4.0.6a 13623
            {Opcode.SMSG_EQUIPMENT_SET_LIST, 0x0F1A8}, // 4.0.6a 13623
            {Opcode.SMSG_EQUIPMENT_SET_SAVED, 0x0B0C0}, // 4.0.6a 13623
            {Opcode.SMSG_EQUIPMENT_SET_USE_RESULT, 0x076AC}, // 4.0.6a 13623
            {Opcode.SMSG_EXPECTED_SPAM_RECORDS, 0x06084}, // 4.0.6a 13623
            {Opcode.SMSG_EXPLORATION_EXPERIENCE, 0x0A9C4}, // 4.0.6a 13623
            {Opcode.SMSG_FEATURE_SYSTEM_STATUS, 0x03DAC}, // 4.0.6a 13623
            {Opcode.SMSG_FEIGN_DEATH_RESISTED, 0x03BE8}, // 4.0.6a 13623
            {Opcode.SMSG_FISH_ESCAPED, 0x0F080}, // 4.0.6a 13623
            {Opcode.SMSG_FISH_NOT_HOOKED, 0x039EC}, // 4.0.6a 13623
            {Opcode.SMSG_FLIGHT_SPLINE_SYNC, 0x0BFAC}, // 4.0.6a 13623
            {Opcode.SMSG_FORCEACTIONSHOW, 0x0AC84}, // 4.0.6a 13623
            {Opcode.SMSG_FORCED_DEATH_UPDATE, 0x0FFE8}, // 4.0.6a 13623
            {Opcode.SMSG_FORCE_DISPLAY_UPDATE, 0x06788}, // 4.0.6a 13623
            {Opcode.SMSG_FORCE_FLIGHT_BACK_SPEED_CHANGE, 0x10153}, // (might be 0x0B484)
            {Opcode.SMSG_FORCE_FLIGHT_SPEED_CHANGE, 0x0E5CC}, // 4.0.6a 13623
            {Opcode.SMSG_FORCE_MOVE_ROOT, 0x02F88}, // 4.0.6a 13623
            {Opcode.SMSG_FORCE_MOVE_UNROOT, 0x030A0}, // 4.0.6a 13623
            {Opcode.SMSG_FORCE_PITCH_RATE_CHANGE, 0x0BF8C}, // 4.0.6a 13623
            {Opcode.SMSG_FORCE_RUN_BACK_SPEED_CHANGE, 0x068E8}, // 4.0.6a 13623
            {Opcode.SMSG_FORCE_RUN_SPEED_CHANGE, 0x0F1CC}, // 4.0.6a 13623
            {Opcode.SMSG_FORCE_SEND_QUEUED_PACKETS, 0x0E280}, // 4.0.6a 13623
            {Opcode.SMSG_FORCE_SWIM_BACK_SPEED_CHANGE, 0x002DC}, // 4.0.6a 13623
            {Opcode.SMSG_FORCE_SWIM_SPEED_CHANGE, 0x0F5A0}, // 4.0.6a 13623
            {Opcode.SMSG_FORCE_TURN_RATE_CHANGE, 0x0A3A8}, // 4.0.6a 13623 (might be 0x0375E)
            {Opcode.SMSG_FORCE_WALK_SPEED_CHANGE, 0x068E8}, // 4.0.6a 13623
            {Opcode.SMSG_FRIEND_STATUS, 0x0F68C}, // 4.0.6a 13623
            {Opcode.SMSG_GAMEOBJECT_CUSTOM_ANIM, 0x02E8C}, // 4.0.6a 13623 // old one 0x036A0
            {Opcode.SMSG_GAMEOBJECT_DESPAWN_ANIM, 0x0BFA8}, // 4.0.6a 13623
            {Opcode.SMSG_GAMEOBJECT_PAGETEXT, 0x0E5C8}, // 4.0.6a 13623
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE, 0x0F4E8}, // 4.0.6a 13623
            {Opcode.SMSG_GAMEOBJECT_RESET_STATE, 0x022E0}, // 4.0.6a 13623
            {Opcode.SMSG_GAMESPEED_SET, 0x03EC0}, // 4.0.6a 13623
            {Opcode.SMSG_GAMETIMEBIAS_SET, 0x1011A}, // Unknown opcode ID
            {Opcode.SMSG_GAMETIME_SET, 0x07888}, // 4.0.6a 13623
            {Opcode.SMSG_GAMETIME_UPDATE, 0x0F1EC}, // 4.0.6a 13623
            {Opcode.SMSG_GHOSTEE_GONE, 0x10123}, // Unknown opcode ID
            {Opcode.SMSG_GMRESPONSE_DB_ERROR, 0x0E0A0}, // 4.0.6a 13623
            {Opcode.SMSG_GMRESPONSE_RECEIVED, 0x033AC}, // 4.0.6a 13623
            {Opcode.SMSG_GMRESPONSE_STATUS_UPDATE, 0x00101}, // 4.0.6a 13623
            {Opcode.SMSG_GMTICKET_CREATE, 0x0A8A0}, // 4.0.6a 13623
            {Opcode.SMSG_GMTICKET_DELETETICKET, 0x0F48C}, // 4.0.6a 13623
            {Opcode.SMSG_GMTICKET_GETTICKET, 0x02284}, // 4.0.6a 13623
            {Opcode.SMSG_GMTICKET_SYSTEMSTATUS, 0x0B9C0}, // 4.0.6a 13623
            {Opcode.SMSG_GMTICKET_UPDATETEXT, 0x0A5E8}, // 4.0.6a 13623
            {Opcode.SMSG_GM_MESSAGECHAT, 0x03AEC}, // 4.0.6a 13623
            {Opcode.SMSG_GM_TICKET_STATUS_UPDATE, 0x072C4}, // 4.0.6a 13623
            {Opcode.SMSG_GODMODE, 0x023EC}, // 4.0.6a 13623
            {Opcode.SMSG_GOGOGO_OBSOLETE, 0x10195}, // Unknown opcode ID
            {Opcode.SMSG_GOSSIP_COMPLETE, 0x0F0AC}, // 4.0.6a 13623
            {Opcode.SMSG_GOSSIP_MESSAGE, 0x0BBC8}, // 4.0.6a 13623
            {Opcode.SMSG_GOSSIP_POI, 0x0B9AC}, // 4.0.6a 13623
            {Opcode.SMSG_GROUPACTION_THROTTLED, 0x0F4E4}, // 4.0.6a 13623
            {Opcode.SMSG_GROUP_CANCEL, 0x06AAC}, // 4.0.6a 13623
            {Opcode.SMSG_GROUP_DECLINE, 0x0ABAC}, // 4.0.6a 13623
            {Opcode.SMSG_GROUP_DESTROYED, 0x022CC}, // 4.0.6a 13623
            {Opcode.SMSG_GROUP_INVITE, 0x0A8A8}, // 4.0.6a 13623
            {Opcode.SMSG_GROUP_JOINED_BATTLEGROUND, 0x00D1E}, // 4.0.6a 13623
            {Opcode.SMSG_GROUP_LIST, 0x06D8C}, // 4.0.6a 13623
            {Opcode.SMSG_GROUP_SET_LEADER, 0x0E88C}, // 4.0.6a 13623
            {Opcode.SMSG_GROUP_UNINVITE, 0x03ACC}, // 4.0.6a 13623
            {Opcode.SMSG_GUILD_ACHIEVEMENT_DATA, 0x0491E}, // 4.0.6a 13623
            {Opcode.SMSG_GUILD_ACHIEVEMENT_EARNED, 0x00D5C}, // 4.0.6a 13623
            {Opcode.SMSG_GUILD_BANK_LIST, 0x0A6A8}, // 4.0.6a 13623
            {Opcode.SMSG_GUILD_COMMAND_RESULT, 0x023C0}, // 4.0.6a 13623
            {Opcode.SMSG_GUILD_DECLINE, 0x0B78C}, // 4.0.6a 13623 (might be 0x07DEC)
            {Opcode.SMSG_GUILD_EVENT, 0x0B7C4}, // 4.0.6a 13623
            {Opcode.SMSG_GUILD_INFO, 0x020A8}, // 4.0.6a 13623
            {Opcode.SMSG_GUILD_INVITE, 0x0010C}, // 4.0.6a 13623 (might be 0x0B78C)
            {Opcode.SMSG_GUILD_MAX_DAILY_XP, 0x0441C}, // 4.0.6a 13623
            {Opcode.SMSG_GUILD_NEWS_UPDATE, 0x0485E}, // 4.0.6a 13623
            {Opcode.SMSG_GUILD_PARTY_STATE_RESPONSE, 0x0450C}, // 4.0.6a 13623
            {Opcode.SMSG_GUILD_QUERY_RESPONSE, 0x03F80}, // 4.0.6a 13623
            {Opcode.SMSG_GUILD_RANK, 0x0411E}, // 4.0.6a 13623
            {Opcode.SMSG_GUILD_RANKS_UPDATE, 0x0004C}, // 4.0.6a 13623
            {Opcode.SMSG_GUILD_REWARDS_LIST, 0x00C4C}, // 4.0.6a 13623
            {Opcode.SMSG_GUILD_ROSTER, 0x04D5C}, // 4.0.6a 13623 (might be 0x038B0)
            {Opcode.SMSG_GUILD_TRADESKILL_UPDATE, 0x0454E}, // 4.0.6a 13623
            {Opcode.SMSG_GUILD_UPDATE_ROSTER, 0x0085E}, // 4.0.6a 13623
            {Opcode.SMSG_GUILD_XP, 0x0440E}, // 4.0.6a 13623
            {Opcode.SMSG_HEALTH_UPDATE, 0x02AA8}, // 4.0.6a 13623
            {Opcode.SMSG_HIGHEST_THREAT_UPDATE, 0x03F7E}, // 4.0.3a 13329
            {Opcode.SMSG_HOTFIX_INFO, 0x04C1E}, // 4.0.6a 13623 (sent after SMSG_AUTH_RESPONSE)
            {Opcode.SMSG_HOTFIX_NOTIFY, 0x04C0E}, // 4.0.6a 13623 can be sent while ingame
            {Opcode.SMSG_IGNORE_DIMINISHING_RETURNS_CHEAT, 0x0747E}, // 4.0.3a 13329
            {Opcode.SMSG_IGNORE_REQUIREMENTS_CHEAT, 0x07C84}, // 4.0.6a 13623
            {Opcode.SMSG_INITIALIZE_FACTIONS, 0x025C0}, // 4.0.6a 13623
            {Opcode.SMSG_INITIAL_SPELLS, 0x06584}, // 4.0.6a 13623
            {Opcode.SMSG_INIT_CURRENCY, 0x0091C}, // 4.0.6a 13623
            {Opcode.SMSG_INIT_EXTRA_AURA_INFO_OBSOLETE, 0x10168}, // Unknown opcode ID
            {Opcode.SMSG_INIT_WORLD_STATES, 0x0F6E8}, // 4.0.6a 13623
            {Opcode.SMSG_INSPECT_HONOR_STATS, 0x0005E}, // 4.0.6a 13623
            {Opcode.SMSG_INSPECT_RESULTS_UPDATE, 0x07AC8}, // 4.0.6a 13623
            {Opcode.SMSG_INSPECT_TALENT, 0x0F8C0}, // 4.0.6a 13623
            {Opcode.SMSG_INSTANCE_DIFFICULTY, 0x0FFE4}, // 4.0.6a 13623 (might be 0x0A55E)
            {Opcode.SMSG_INSTANCE_LOCK_WARNING_QUERY, 0x07488}, // 4.0.6a 13623
            {Opcode.SMSG_INSTANCE_RESET, 0x030E8}, // 4.0.6a 13623
            {Opcode.SMSG_INSTANCE_RESET_FAILED, 0x03BA0}, // 4.0.6a 13623
            {Opcode.SMSG_INSTANCE_SAVE_CREATED, 0x0BBC4}, // 4.0.6a 13623
            {Opcode.SMSG_INVALIDATE_DANCE, 0x0BEE8}, // 4.0.6a 13623
            {Opcode.SMSG_INVALIDATE_PLAYER, 0x0A884}, // 4.0.6a 13623
            {Opcode.SMSG_INVALID_PROMOTION_CODE, 0x069AC}, // 4.0.6a 13623
            {Opcode.SMSG_INVENTORY_CHANGE_FAILURE, 0x0AFCC}, // 4.0.6a 13623
            {Opcode.SMSG_ITEM_COOLDOWN, 0x06CC8}, // 4.0.6a 13623
            {Opcode.SMSG_ITEM_ENCHANT_TIME_UPDATE, 0x066A0}, // 4.0.6a 13623
            {Opcode.SMSG_ITEM_PUSH_RESULT, 0x0FBE8}, // 4.0.6a 13623
            {Opcode.SMSG_ITEM_REFUND_INFO_RESPONSE, 0x0095E}, // 4.0.6a 13623
            {Opcode.SMSG_ITEM_REFUND_RESULT, 0x0441E}, // 4.0.6a 13623
            {Opcode.SMSG_ITEM_TEXT_QUERY_RESPONSE, 0x077C8}, // 4.0.6a 13623
            {Opcode.SMSG_ITEM_TIME_UPDATE, 0x066A4}, // 4.0.6a 13623
            {Opcode.SMSG_JOINED_BATTLEGROUND_QUEUE, 0x0090E}, // 4.0.6a 13623
            {Opcode.SMSG_KICK_REASON, 0x0EAC8}, // 4.0.6a 13623
            {Opcode.SMSG_LEARNED_DANCE_MOVES, 0x06F80}, // 4.0.6a 13623
            {Opcode.SMSG_LEARNED_SPELL, 0x076E8}, // 4.0.6a 13623
            {Opcode.SMSG_LEVELUP_INFO, 0x0B9C4}, // 4.0.6a 13623
            {Opcode.SMSG_LFG_BOOT_PROPOSAL_UPDATE, 0x079AC}, // 4.0.6a 13623
            {Opcode.SMSG_LFG_DISABLED, 0x0F880}, // 4.0.6a 13623
            {Opcode.SMSG_LFG_JOIN_RESULT, 0x0338C}, // 4.0.6a 13623
            {Opcode.SMSG_LFG_LFR_LIST, 0x0768C}, // 4.0.6a 13623
            {Opcode.SMSG_LFG_OFFER_CONTINUE, 0x063EC}, // 4.0.6a 13623
            {Opcode.SMSG_LFG_PARTY_INFO, 0x060A0}, // 4.0.6a 13623
            {Opcode.SMSG_LFG_PLAYER_INFO, 0x0E088}, // 4.0.6a 13623
            {Opcode.SMSG_LFG_PLAYER_REWARD, 0x02C88}, // 4.0.6a 13623
            {Opcode.SMSG_LFG_PROPOSAL_UPDATE, 0x032AC}, // 4.0.6a 13623
            {Opcode.SMSG_LFG_QUEUE_STATUS, 0x0B388}, // 4.0.6a 13623
            {Opcode.SMSG_LFG_ROLE_CHECK_UPDATE, 0x0F2A8}, // 4.0.6a 13623
            {Opcode.SMSG_LFG_ROLE_CHOSEN, 0x0A9AC}, // 4.0.6a 13623
            {Opcode.SMSG_LFG_TELEPORT_DENIED, 0x0EAA0}, // 4.0.6a 13623
            {Opcode.SMSG_LFG_UPDATE_LIST, 0x03880}, // 4.0.6a 13623
            {Opcode.SMSG_LFG_UPDATE_PARTY, 0x02CE8}, // 4.0.6a 13623
            {Opcode.SMSG_LFG_UPDATE_PLAYER, 0x0E284}, // 4.0.6a 13623
            {Opcode.SMSG_LIST_INVENTORY, 0x0F8AC}, // 4.0.6a 13623
            {Opcode.SMSG_LOGIN_SETTIMESPEED, 0x039AC}, // 4.0.6a 13623
            {Opcode.SMSG_LOGIN_VERIFY_WORLD, 0x028C0}, // 4.0.6a 13623
            {Opcode.SMSG_LOGOUT_CANCEL_ACK, 0x0EE88}, // 4.0.6a 13623
            {Opcode.SMSG_LOGOUT_COMPLETE, 0x0A0A4}, // 4.0.6a 13623
            {Opcode.SMSG_LOGOUT_RESPONSE, 0x0F788}, // 4.0.6a 13623
            {Opcode.SMSG_LOG_XPGAIN, 0x0B880}, // 4.0.6a 13623
            {Opcode.SMSG_LOOT_ALL_PASSED, 0x06AC4}, // 4.0.6a 13623
            {Opcode.SMSG_LOOT_CLEAR_MONEY, 0x03480}, // 4.0.6a 13623
            {Opcode.SMSG_LOOT_ITEM_NOTIFY, 0x0AECC}, // 4.0.6a 13623
            {Opcode.SMSG_LOOT_LIST, 0x0F684}, // 4.0.6a 13623
            {Opcode.SMSG_LOOT_MASTER_LIST, 0x0ECC4}, // 4.0.6a 13623
            {Opcode.SMSG_LOOT_MONEY_NOTIFY, 0x067C0}, // 4.0.6a 13623
            {Opcode.SMSG_LOOT_RELEASE_RESPONSE, 0x023C8}, // 4.0.6a 13623
            {Opcode.SMSG_LOOT_REMOVED, 0x06F88}, // 4.0.6a 13623
            {Opcode.SMSG_LOOT_RESPONSE, 0x0F38C}, // 4.0.6a 13623
            {Opcode.SMSG_LOOT_ROLL, 0x066A8}, // 4.0.6a 13623
            {Opcode.SMSG_LOOT_ROLL_WON, 0x06280}, // 4.0.6a 13623
            {Opcode.SMSG_LOOT_SLOT_CHANGED, 0x031EC}, // 4.0.6a 13623
            {Opcode.SMSG_LOOT_START_ROLL, 0x0EB84}, // 4.0.6a 13623
            {Opcode.SMSG_LOTTERY_QUERY_RESULT_OBSOLETE, 0x1012A}, // Unknown opcode ID
            {Opcode.SMSG_LOTTERY_RESULT_OBSOLETE, 0x1012C}, // Unknown opcode ID
            {Opcode.SMSG_MAIL_LIST_RESULT, 0x0F1C8}, // 4.0.6a 13623
            {Opcode.SMSG_MEETINGSTONE_COMPLETE, 0x0F680}, // 4.0.6a 13623
            {Opcode.SMSG_MEETINGSTONE_IN_PROGRESS, 0x0E7C4}, // 4.0.6a 13623
            {Opcode.SMSG_MEETINGSTONE_MEMBER_ADDED, 0x0B2A8}, // 4.0.6a 13623
            {Opcode.SMSG_MEETINGSTONE_SETQUEUE, 0x0ED88}, // 4.0.6a 13623 (almost 99.9% sure it's wrong)
            {Opcode.SMSG_MESSAGECHAT, 0x061E4}, // 4.0.6a 13623
            {Opcode.SMSG_MINIGAME_MOVE_FAILED, 0x10109}, // Unknown opcode ID
            {Opcode.SMSG_MINIGAME_SETUP, 0x026A4}, // 4.0.6a 13623 (might be 0x01C74)
            {Opcode.SMSG_MINIGAME_STATE, 0x0A5A8}, // 4.0.6a 13623 (might be 0x04C7F)
            {Opcode.SMSG_MIRRORIMAGE_DATA, 0x0E2A4}, // 4.0.6a 13623
            {Opcode.SMSG_MODIFY_COOLDOWN, 0x030CC}, // 4.0.6a 13623
            {Opcode.SMSG_MONSTER_MOVE, 0x0F1A4}, // 4.0.6a 13623
            {Opcode.SMSG_MONSTER_MOVE_TRANSPORT, 0x0248C}, // 4.0.6a 13623
            {Opcode.SMSG_MOTD, 0x077C0}, // 4.0.6a 13623
            {Opcode.SMSG_MOUNTRESULT, 0x02AEC}, // 4.0.6a 13623
            {Opcode.SMSG_MOUNTSPECIAL_ANIM, 0x02388}, // 4.0.6a 13623
            {Opcode.SMSG_MOVE_ABANDON_TRANSPORT, 0x101D0}, // Unknown opcode ID
            {Opcode.SMSG_MOVE_FEATHER_FALL, 0x06088}, // 4.0.6a 13623
            {Opcode.SMSG_MOVE_GRAVITY_DISABLE, 0x10219}, // Unknown opcode ID
            {Opcode.SMSG_MOVE_GRAVITY_ENABLE, 0x1021B}, // Unknown opcode ID
            {Opcode.SMSG_MOVE_KNOCK_BACK, 0x0B180}, // 4.0.6a 13623
            {Opcode.SMSG_MOVE_LAND_WALK, 0x02084}, // 4.0.6a 13623
            {Opcode.SMSG_MOVE_LEVITATING, 0x0B8AC}, // 4.0.6a 13623
            {Opcode.SMSG_MOVE_NORMAL_FALL, 0x06CE0}, // 4.0.6a 13623
            {Opcode.SMSG_MOVE_SET_CAN_FLY, 0x0BDA0}, // 4.0.6a 13623
            {Opcode.SMSG_MOVE_SET_FLIGHT, 0x0C57F}, // 4.0.3a 13329
            {Opcode.SMSG_MOVE_SET_HOVER, 0x06AE0}, // 4.0.6a 13623
            {Opcode.SMSG_MOVE_SET_WALK_IN_AIR, 0x0A980}, // 4.0.6a 13623
            {Opcode.SMSG_MOVE_UNSET_CAN_FLY, 0x03084}, // 4.0.6a 13623
            {Opcode.SMSG_MOVE_UNSET_FLIGHT, 0x0BDA0}, // 4.0.6a 13623 Not sure.
            {Opcode.SMSG_MOVE_UNSET_HOVER, 0x03FE0}, // 4.0.6a 13623
            {Opcode.SMSG_MOVE_UNSET_WALK_IN_AIR, 0x07784}, // 4.0.6a 13623
            {Opcode.SMSG_MOVE_WATER_WALK, 0x02E84}, // 4.0.6a 13623
            {Opcode.SMSG_MULTIPLE_PACKETS_2, 0x0FEC0}, // 4.0.6a 13623
            {Opcode.SMSG_NAME_QUERY_RESPONSE, 0x07BC8}, // 4.0.6a 13623
            {Opcode.SMSG_NEW_TAXI_PATH, 0x0E5E4}, // 4.0.6a 13623
            {Opcode.SMSG_NEW_WORLD, 0x0451E}, // 4.0.6a 13623
            {Opcode.SMSG_NOTIFICATION, 0x0BC88}, // 4.0.6a 13623
            {Opcode.SMSG_NOTIFY_DANCE, 0x03488}, // 4.0.6a 13623
            {Opcode.SMSG_NOTIFY_DEST_LOC_SPELL_CAST, 0x03588}, // 4.0.6a 13623
            {Opcode.SMSG_NPC_TEXT_UPDATE, 0x0B2AC}, // 4.0.6a 13623
            {Opcode.SMSG_NPC_WONT_TALK, 0x10061}, // Unknown opcode ID
            {Opcode.SMSG_OFFER_PETITION_ERROR, 0x07DC0}, // 4.0.6a 13623
            {Opcode.SMSG_ON_CANCEL_EXPECTED_RIDE_VEHICLE_AURA, 0x03380}, // 4.0.6a 13623
            {Opcode.SMSG_OPEN_CONTAINER, 0x03FC4}, // 4.0.6a 13623
            {Opcode.SMSG_OPEN_LFG_DUNGEON_FINDER, 0x071EC}, // 4.0.6a 13623
            {Opcode.SMSG_OVERRIDE_LIGHT, 0x00756}, // 4.0.3a 13329
            {Opcode.SMSG_PAGE_TEXT_QUERY_RESPONSE, 0x0B084}, // 4.0.6a 13623
            {Opcode.SMSG_PARTYKILLLOG, 0x0AB84}, // 4.0.6a 13623
            {Opcode.SMSG_PARTY_COMMAND_RESULT, 0x026E0}, // 4.0.6a 13623
            {Opcode.SMSG_PARTY_MEMBER_STATS, 0x029AC}, // 4.0.6a 13623
            {Opcode.SMSG_PARTY_MEMBER_STATS_FULL, 0x0BA8C}, // 4.0.6a 13623
            {Opcode.SMSG_PAUSE_MIRROR_TIMER, 0x021EC}, // 4.0.6a 13623
            {Opcode.SMSG_PERIODICAURALOG, 0x03388}, // 4.0.6a 13623
            {Opcode.SMSG_PETGODMODE, 0x0F2CC}, // 4.0.6a 13623
            {Opcode.SMSG_PETITION_QUERY_RESPONSE, 0x0FB80}, // 4.0.6a 13623
            {Opcode.SMSG_PETITION_SHOWLIST, 0x0FCC0}, // 4.0.6a 13623
            {Opcode.SMSG_PETITION_SHOW_SIGNATURES, 0x0E2E0}, // 4.0.6a 13623
            {Opcode.SMSG_PETITION_SIGN_RESULTS, 0x0A1A4}, // 4.0.6a 13623
            {Opcode.SMSG_PET_ACTION_FEEDBACK, 0x02584}, // 4.0.6a 13623
            {Opcode.SMSG_PET_ACTION_SOUND, 0x030E4}, // 4.0.6a 13623
            {Opcode.SMSG_PET_BROKEN, 0x075E0}, // 4.0.6a 13623
            {Opcode.SMSG_PET_CAST_FAILED, 0x0A9CC}, // 4.0.6a 13623
            {Opcode.SMSG_PET_DISMISS_SOUND, 0x0B7E0}, // 4.0.6a 13623
            {Opcode.SMSG_PET_GUIDS, 0x0E4E8}, // 4.0.6a 13623
            {Opcode.SMSG_PET_LEARNED_SPELL, 0x0B3C4}, // 4.0.6a 13623
            {Opcode.SMSG_PET_MODE, 0x079C0}, // 4.0.6a 13623
            {Opcode.SMSG_PET_NAME_INVALID, 0x01457}, // 4.0.3a 13329
            {Opcode.SMSG_PET_NAME_QUERY_RESPONSE, 0x068AC}, // 4.0.6a 13623
            {Opcode.SMSG_PET_REMOVED_SPELL, 0x0F28C}, // 4.0.6a 13623
            {Opcode.SMSG_PET_RENAMEABLE, 0x0B6C8}, // 4.0.6a 13623
            {Opcode.SMSG_PET_SPELLS, 0x0B780}, // 4.0.6a 13623
            {Opcode.SMSG_PET_TAME_FAILURE, 0x0FDA8}, // 4.0.6a 13623
            {Opcode.SMSG_PET_UNLEARN_CONFIRM, 0x10106}, // Unknown opcode ID
            {Opcode.SMSG_PET_UPDATE_COMBO_POINTS, 0x07588}, // 4.0.6a 13623
            {Opcode.SMSG_PLAYED_TIME, 0x0E4C8}, // 4.0.6a 13623
            {Opcode.SMSG_PLAYERBINDERROR, 0x0EEC8}, // 4.0.6a 13623
            {Opcode.SMSG_PLAYERBOUND, 0x06BCC}, // 4.0.6a 13623
            {Opcode.SMSG_PLAYER_DIFFICULTY_CHANGE, 0x02A80}, // 4.0.6a 13623
            {Opcode.SMSG_PLAYER_SKINNED, 0x0BAA0}, // 4.0.6a 13623
            {Opcode.SMSG_PLAYER_VEHICLE_DATA, 0x0A5A4}, // 4.0.6a 13623
            {Opcode.SMSG_PLAY_DANCE, 0x0B4EC}, // 4.0.6a 13623
            {Opcode.SMSG_PLAY_MUSIC, 0x0AE54}, // 4.0.6a 13623
            {Opcode.SMSG_PLAY_OBJECT_SOUND, 0x07DA0}, // 4.0.6a 13623
            {Opcode.SMSG_PLAY_SOUND, 0x02EA8}, // 4.0.6a 13623
            {Opcode.SMSG_PLAY_SPELL_IMPACT, 0x0A3E4}, // 4.0.6a 13623
            {Opcode.SMSG_PLAY_SPELL_VISUAL, 0x0FF8C}, // 4.0.6a 13623
            {Opcode.SMSG_PLAY_TIME_WARNING, 0x032EC}, // 4.0.6a 13623
            {Opcode.SMSG_PONG, 0x0A01B}, // 4.0.6a 13623
            {Opcode.SMSG_POWER_UPDATE, 0x065E8}, // 4.0.6a 13623
            {Opcode.SMSG_PRE_RESURRECT, 0x066C4}, // 4.0.6a 13623
            {Opcode.SMSG_PROCRESIST, 0x07E80}, // 4.0.6a 13623
            {Opcode.SMSG_PROPOSE_LEVEL_GRANT, 0x0E0C4}, // 4.0.6a 13623 (might be 0x0B476)
            {Opcode.SMSG_PUREMOUNT_CANCELLED_OBSOLETE, 0x0054E}, // 4.0.6a 13623
            {Opcode.SMSG_PVP_CREDIT, 0x037C8}, // 4.0.6a 13623
            {Opcode.SMSG_QUERY_QUESTS_COMPLETED_RESPONSE, 0x0F1E8}, // 4.0.6a 13623
            {Opcode.SMSG_QUERY_TIME_RESPONSE, 0x0F1AC}, // 4.0.6a 13623
            {Opcode.SMSG_QUESTGIVER_OFFER_REWARD, 0x03FCC}, // 4.0.6a 13623
            {Opcode.SMSG_QUESTGIVER_QUEST_COMPLETE, 0x0050E}, // 4.0.6a 13623 (might be 0x0F5C0)
            {Opcode.SMSG_QUESTGIVER_QUEST_DETAILS, 0x068A0}, // 4.0.6a 13623
            {Opcode.SMSG_QUESTGIVER_QUEST_FAILED, 0x0A084}, // 4.0.6a 13623
            {Opcode.SMSG_QUESTGIVER_QUEST_INVALID, 0x02B88}, // 4.0.6a 13623
            {Opcode.SMSG_QUESTGIVER_QUEST_LIST, 0xBBEC}, // 4.0.6a 13623
            {Opcode.SMSG_QUESTGIVER_REQUEST_ITEMS, 0x06EE0}, // 4.0.6a 13623 (might be 0x023A8)
            {Opcode.SMSG_QUESTGIVER_STATUS, 0x07988}, // 4.0.6a 13623
            {Opcode.SMSG_QUESTGIVER_STATUS_MULTIPLE, 0x0F5C0}, // 4.0.6a 13623
            {Opcode.SMSG_QUESTLOG_FULL, 0x061EC}, // 4.0.6a 13623
            {Opcode.SMSG_QUESTUPDATE_ADD_ITEM, 0x1006B}, // 4.0.6a 13623
            {Opcode.SMSG_QUESTUPDATE_ADD_KILL, 0x0ADA0}, // 4.0.6a 13623
            {Opcode.SMSG_QUESTUPDATE_ADD_PVP_KILL, 0x078E0}, // 4.0.6a 13623
            {Opcode.SMSG_QUESTUPDATE_COMPLETE, 0x0EDA0}, // 4.0.6a 13623
            {Opcode.SMSG_QUESTUPDATE_FAILED, 0x0E588}, // 4.0.6a 13623
            {Opcode.SMSG_QUESTUPDATE_FAILEDTIMER, 0x0F7CC}, // 4.0.6a 13623
            {Opcode.SMSG_QUEST_CONFIRM_ACCEPT, 0x07C8C}, // 4.0.6a 13623
            {Opcode.SMSG_QUEST_FORCE_REMOVE, 0x034E8}, // 4.0.6a 13623
            {Opcode.SMSG_QUEST_NPC_QUERY_RESPONSE, 0x0401C}, // 4.0.6a 13623
            {Opcode.SMSG_QUEST_POI_QUERY_RESPONSE, 0x06AEC}, // 4.0.6a 13623
            {Opcode.SMSG_QUEST_QUERY_RESPONSE, 0x07BA0}, // 4.0.6a 13623
            {Opcode.SMSG_RAID_GROUP_ONLY, 0x00554}, // 4.0.3a 13329
            {Opcode.SMSG_RAID_INSTANCE_INFO, 0x0A18C}, // 4.0.6a 13623
            {Opcode.SMSG_RAID_INSTANCE_MESSAGE, 0x06680}, // 4.0.6a 13623
            {Opcode.SMSG_RAID_READY_CHECK_ERROR, 0x0547E}, // 4.0.3a 13329
            {Opcode.SMSG_READ_ITEM_FAILED, 0x0A4EC}, // 4.0.6a 13623
            {Opcode.SMSG_READ_ITEM_OK, 0x0A988}, // 4.0.6a 13623
            {Opcode.SMSG_REALM_SPLIT, 0x025EC}, // 4.0.6a 13623
            {Opcode.SMSG_REAL_GROUP_UPDATE, 0x031C0}, // 4.0.6a 13623
            {Opcode.SMSG_RECEIVED_MAIL, 0x075E4}, // 4.0.6a 13623
            {Opcode.SMSG_REDIRECT_CLIENT, 0x0201B}, // 4.0.6a 13623
            {Opcode.SMSG_REFER_A_FRIEND_FAILURE, 0x07F7C}, // 4.0.6a 13623
            {Opcode.SMSG_REMOVED_SPELL, 0x07CA0}, // 4.0.6a 13623
            {Opcode.SMSG_REPORT_PVP_AFK_RESULT, 0x078E8}, // 4.0.6a 13623
            {Opcode.SMSG_RESET_FAILED_NOTIFY, 0x0BD5C}, // 4.0.3a 13329
            {Opcode.SMSG_RESISTLOG, 0x10084}, // Unknown opcode ID
            {Opcode.SMSG_RESPOND_INSPECT_ACHIEVEMENTS, 0x0041C}, // 4.0.6a 13623
            {Opcode.SMSG_RESURRECT_FAILED, 0x0D557}, // 4.0.3a 13329
            {Opcode.SMSG_RESURRECT_REQUEST, 0x0F3A0}, // 4.0.6a 13623 (might be 0x022A4)
            {Opcode.SMSG_RESYNC_RUNES, 0x0F8E4}, // 4.0.6a 13623
            {Opcode.SMSG_RWHOIS, 0x071C8}, // 4.0.6a 13623
            {Opcode.SMSG_SCRIPT_MESSAGE, 0x100E0}, // Unknown opcode ID
            {Opcode.SMSG_SELL_ITEM, 0x037AC}, // 4.0.6a 13623
            {Opcode.SMSG_SEND_MAIL_RESULT, 0x0E5C0}, // 4.0.6a 13623
            {Opcode.SMSG_SEND_QUEUED_PACKETS, 0x10012}, // Unknown opcode ID/Name (might be 0x01400)
            {Opcode.SMSG_SEND_UNLEARN_SPELLS, 0x0BDE8}, // 4.0.6a 13623
            {Opcode.SMSG_SERVERTIME, 0x023A4}, // 4.0.6a 13623
            {Opcode.SMSG_SERVER_BUCK_DATA, 0x101AE}, // Unknown opcode ID
            {Opcode.SMSG_SERVER_BUCK_DATA_START, 0x101F9}, // Unknown opcode ID
            {Opcode.SMSG_SERVER_FIRST_ACHIEVEMENT, 0x068CC}, // 4.0.6a 13623
            {Opcode.SMSG_SERVER_INFO_RESPONSE, 0x101F7}, // Unknown opcode ID
            {Opcode.SMSG_SERVER_MESSAGE, 0x078C0}, // 4.0.6a 13623
            {Opcode.SMSG_SET_EXTRA_AURA_INFO_NEED_UPDATE_OBSOLETE, 0x1016A}, // Unknown opcode ID
            {Opcode.SMSG_SET_EXTRA_AURA_INFO_OBSOLETE, 0x10169}, // Unknown opcode ID
            {Opcode.SMSG_SET_FACTION_ATWAR, 0x02780}, // 4.0.6a 13623
            {Opcode.SMSG_SET_FACTION_STANDING, 0x0718C}, // 4.0.6a 13623
            {Opcode.SMSG_SET_FACTION_VISIBLE, 0x03988}, // 4.0.6a 13623
            {Opcode.SMSG_SET_FLAT_SPELL_MODIFIER, 0x02BC8}, // 4.0.6a 13623
            {Opcode.SMSG_SET_FORCED_REACTIONS, 0x0FFA0}, // 4.0.6a 13623
            {Opcode.SMSG_SET_PCT_SPELL_MODIFIER, 0x0A6E8}, // 4.0.6a 13623
            {Opcode.SMSG_SET_PHASE_SHIFT, 0x0044C}, // 4.0.6a 13623
            {Opcode.SMSG_SET_PLAYER_DECLINED_NAMES_RESULT, 0x0BAAC}, // 4.0.6a 13623
            {Opcode.SMSG_SET_PROFICIENCY, 0x0BBA8}, // 4.0.6a 13623
            {Opcode.SMSG_SET_PROJECTILE_POSITION, 0x02C84}, // 4.0.6a 13623
            {Opcode.SMSG_SHOWTAXINODES, 0x02B84}, // 4.0.6a 13623
            {Opcode.SMSG_SHOW_BANK, 0x027A4}, // 4.0.6a 13623
            {Opcode.SMSG_SHOW_REFORGE, 0x00C5C}, // 4.0.6a 13623
            {Opcode.SMSG_SPELLBREAKLOG, 0x0BDAC}, // 4.0.6a 13623
            {Opcode.SMSG_SPELLDAMAGESHIELD, 0x073A0}, // 4.0.6a 13623
            {Opcode.SMSG_SPELLDISPELLOG, 0x0A9C8}, // 4.0.6a 136230 (might be 0x03C56)
            {Opcode.SMSG_SPELLENERGIZELOG, 0x0F0EC}, // 4.0.6a 13623
            {Opcode.SMSG_SPELLHEALLOG, 0x06E84}, // 4.0.6a 13623
            {Opcode.SMSG_SPELLINSTAKILLLOG, 0x061C8}, // 4.0.6a 13623
            {Opcode.SMSG_SPELLLOGEXECUTE, 0x0B6E8}, // 4.0.6a 13623
            {Opcode.SMSG_SPELLLOGMISS, 0x0B4A4}, // 4.0.6a 13623
            {Opcode.SMSG_SPELLNONMELEEDAMAGELOG, 0x074AC}, // 4.0.6a 13623
            {Opcode.SMSG_SPELLORDAMAGE_IMMUNE, 0x073C8}, // 4.0.6a 13623
            {Opcode.SMSG_SPELLSTEALLOG, 0x09F74}, // 4.0.3a 13329
            {Opcode.SMSG_SPELL_CHANCE_PROC_LOG, 0x1016E}, // Unknown opcode ID
            {Opcode.SMSG_SPELL_CHANCE_RESIST_PUSHBACK, 0x1019C}, // Unknown opcode ID
            {Opcode.SMSG_SPELL_COOLDOWN, 0x0F3E8}, // 4.0.6a 13623
            {Opcode.SMSG_SPELL_DELAYED, 0x0A3E8}, // 4.0.6a 13623
            {Opcode.SMSG_SPELL_FAILED_OTHER, 0x0E7A4}, // 4.0.6a 13623
            {Opcode.SMSG_SPELL_FAILURE, 0x0F9CC}, // 4.0.6a 13623
            {Opcode.SMSG_SPELL_GO, 0x030C0}, // 4.0.6a 13623
            {Opcode.SMSG_SPELL_START, 0x06BA8}, // 4.0.6a 13623
            {Opcode.SMSG_SPELL_UPDATE_CHAIN_TARGETS, 0x036A4}, // 4.0.6a 13623
            {Opcode.SMSG_SPIRIT_HEALER_CONFIRM, 0x0B9C0}, // 4.0.6a 13623 FIXME SMSG_GMTICKET_SYSTEMSTATUS
            {Opcode.SMSG_SPLINE_MOVE_FEATHER_FALL, 0x1010C}, // Unknown opcode ID
            {Opcode.SMSG_SPLINE_MOVE_GRAVITY_DISABLE, 0x1021E}, // Unknown opcode ID
            {Opcode.SMSG_SPLINE_MOVE_GRAVITY_ENABLE, 0x1022F}, // 4.0.6a 13623
            {Opcode.SMSG_SPLINE_MOVE_LAND_WALK, 0x0A7C8}, // 4.0.6a 13623
            {Opcode.SMSG_SPLINE_MOVE_NORMAL_FALL, 0x1010D}, // Unknown opcode ID
            {Opcode.SMSG_SPLINE_MOVE_ROOT, 0x01E55}, // 4.0.6a 13623
            {Opcode.SMSG_SPLINE_MOVE_SET_FLYING, 0x05D54}, // 4.0.3a 13329
            {Opcode.SMSG_SPLINE_MOVE_SET_HOVER, 0x1010E}, // Unknown opcode ID
            {Opcode.SMSG_SPLINE_MOVE_SET_RUN_MODE, 0x10114}, // Unknown opcode ID
            {Opcode.SMSG_SPLINE_MOVE_SET_WALK_MODE, 0x10115}, // Unknown opcode ID
            {Opcode.SMSG_SPLINE_MOVE_START_SWIM, 0x10112}, // Unknown opcode ID
            {Opcode.SMSG_SPLINE_MOVE_STOP_SWIM, 0x10113}, // Unknown opcode ID
            {Opcode.SMSG_SPLINE_MOVE_UNROOT, 0x1010B}, // Unknown opcode ID
            {Opcode.SMSG_SPLINE_MOVE_UNSET_FLYING, 0x07688}, // 4.0.6a 13623
            {Opcode.SMSG_SPLINE_MOVE_UNSET_HOVER, 0x1010F}, // Unknown opcode ID
            {Opcode.SMSG_SPLINE_MOVE_WATER_WALK, 0x061C0}, // 4.0.6a 13623
            {Opcode.SMSG_SPLINE_MOVE_SET_FLIGHT_BACK_SPEED, 0x0F2EC}, // 4.0.6a 13623
            {Opcode.SMSG_SPLINE_MOVE_SET_FLIGHT_SPEED, 0x0E0C0}, // 4.0.6a 13623
            {Opcode.SMSG_SPLINE_MOVE_SET_PITCH_RATE, 0x070C4}, // 4.0.6a 13623
            {Opcode.SMSG_SPLINE_MOVE_SET_RUN_BACK_SPEED, 0x0E9C0}, // 4.0.6a 13623
            {Opcode.SMSG_SPLINE_MOVE_SET_RUN_SPEED, 0x0F9E8}, // 4.0.6a 13623
            {Opcode.SMSG_SPLINE_MOVE_SET_SWIM_BACK_SPEED, 0x021C8}, // 4.0.6a 13623
            {Opcode.SMSG_SPLINE_MOVE_SET_SWIM_SPEED, 0x0B2C0}, // 4.0.6a 13623
            {Opcode.SMSG_SPLINE_MOVE_SET_TURN_RATE, 0x07EAC}, // 4.0.6a 13623
            {Opcode.SMSG_SPLINE_MOVE_SET_WALK_SPEED, 0x03EA8}, // 4.0.6a 13623
            {Opcode.SMSG_STABLE_RESULT, 0x0EACC}, // 4.0.6a 13623
            {Opcode.SMSG_STANDSTATE_CHANGE_FAILURE_OBSOLETE, 0x06DCC}, // 4.0.6a 13623 FIXME SMSG_ATTACKSTOP
            {Opcode.SMSG_STANDSTATE_UPDATE, 0x0E6A8}, // 4.0.6a 13623
            {Opcode.SMSG_START_MIRROR_TIMER, 0x0B4A8}, // 4.0.6a 13623 (might be 0x0A68C)
            {Opcode.SMSG_STOP_DANCE, 0x0E0A8}, // 4.0.6a 13623
            {Opcode.SMSG_STOP_MIRROR_TIMER, 0x0A68C}, // 4.0.6a 13623 (might be 0x0B4A8)
            {Opcode.SMSG_SUMMON_CANCEL, 0x070A8}, // 4.0.6a 13623
            {Opcode.SMSG_SUMMON_REQUEST, 0x03AE4}, // 4.0.6a 13623
            {Opcode.SMSG_SUPERCEDED_SPELL, 0x0E8E4}, // 4.0.6a 13623
            {Opcode.SMSG_TALENTS_INFO, 0x075C4}, // 4.0.6a 13623
            {Opcode.SMSG_TALENTS_INVOLUNTARILY_RESET, 0x02A84}, // 4.0.6a 13623
            {Opcode.SMSG_TAXINODE_STATUS, 0x077E8}, // 4.0.6a 13623
            {Opcode.SMSG_TEXT_EMOTE, 0x0BB8C}, // 4.0.6a 13623
            {Opcode.SMSG_THREAT_CLEAR, 0x0FFC8}, // 4.0.6a 13623
            {Opcode.SMSG_THREAT_REMOVE, 0x029E0}, // 4.0.6a 13623
            {Opcode.SMSG_THREAT_UPDATE, 0x0B480}, // 4.0.6a 13623
            {Opcode.SMSG_TIME_SYNC_REQ, 0x0AA80}, // 4.0.6a 13623
            {Opcode.SMSG_TITLE_EARNED, 0x06C8C}, // 4.0.6a 13623
            {Opcode.SMSG_TOGGLE_XP_GAIN, 0x07980}, // 4.0.6a 13623
            {Opcode.SMSG_TOTEM_CREATED, 0x02EAC}, // 4.0.6a 13623
            {Opcode.SMSG_TRADE_STATUS, 0x0494E}, // 4.0.6a 13623
            {Opcode.SMSG_TRADE_STATUS_EXTENDED, 0x0400C}, // 4.0.6a 13623
            {Opcode.SMSG_TRAINER_BUY_RESULT, 0x06DEC}, // 4.0.6a 13623
            {Opcode.SMSG_TRAINER_LIST, 0x0BBE0}, // 4.0.6a 13623 (might be 0x06DEC)
            {Opcode.SMSG_TRANSFER_ABORTED, 0x02BE0}, // 4.0.6a 13623
            {Opcode.SMSG_TRANSFER_PENDING, 0x07BE0}, // 4.0.6a 13623
            {Opcode.SMSG_TRIGGER_CINEMATIC, 0x073A4}, // 4.0.6a 13623
            {Opcode.SMSG_TRIGGER_MOVIE, 0x020C8}, // 4.0.6a 13623
            {Opcode.SMSG_TURN_IN_PETITION_RESULTS, 0x035AC}, // 4.0.6a 13623
            {Opcode.SMSG_TUTORIAL_FLAGS, 0x03384}, // 4.0.6a 13623
            {Opcode.SMSG_UI_TIME, 0x0618C}, // 4.0.6a 13623
            {Opcode.SMSG_UNIT_SPELLCAST_START, 0x0BDE0}, // 4.0.6a 13623
            {Opcode.SMSG_UNKNOWN_1240, 0x0B8E0}, // 4.0.6a 13623
            {Opcode.SMSG_UNKNOWN_1295, 0x10008}, // Unknown opcode ID
            {Opcode.SMSG_UNKNOWN_1302, 0x10010}, // Unknown opcode ID
            {Opcode.SMSG_UNKNOWN_1304, 0x072E0}, // 4.0.6a 13623 - Its movement related MSG_MOVE_SET_XXX
            {Opcode.SMSG_UNKNOWN_1308, 0x10011}, // Unknown opcode ID
            {Opcode.SMSG_UNKNOWN_1310, 0x02E88}, // 4.0.6a 13623
            {Opcode.SMSG_UNKNOWN_1311, 0x01E76}, // Unknown opcode ID/Name
            {Opcode.SMSG_UNKNOWN_1312, 0x04D56}, // Unknown opcode ID/Name
            {Opcode.SMSG_UNKNOWN_1314, 0x10256}, // Unknown opcode ID/Name
            {Opcode.SMSG_UNKNOWN_1315, 0x10257}, // Unknown opcode ID/Name
            {Opcode.SMSG_UNKNOWN_1316, 0x10258}, // Unknown opcode ID/Name
            {Opcode.SMSG_UNKNOWN_1317, 0x10259}, // Unknown opcode ID/Name
            {Opcode.SMSG_UNKNOWN_1329, 0x02D57}, // 4.0.3a 13329
            {Opcode.SMSG_UNKNOWN_1330, 0x0618C}, // 4.0.6a 13623
            {Opcode.SMSG_UNKNOWN_65508, 0x0FFE4}, // Unknown opcode Name, 4.0.6a 13623
            {Opcode.SMSG_UPDATE_ACCOUNT_DATA, 0x0F7A0}, // 4.0.6a 13623
            {Opcode.SMSG_UPDATE_ACCOUNT_DATA_COMPLETE, 0x0B1CC}, // 4.0.6a 13623
            {Opcode.SMSG_UPDATE_COMBO_POINTS, 0x037A8}, // 4.0.6a 13623
            {Opcode.SMSG_UPDATE_CURRENCY, 0x0405E}, // 4.0.6a 13623
            {Opcode.SMSG_UPDATE_CURRENCY_WEEK_LIMIT, 0x04C5C}, // 4.0.6a 13623
            {Opcode.SMSG_UPDATE_INSTANCE_ENCOUNTER_UNIT, 0x0EDA4}, // 4.0.6a 13623
            {Opcode.SMSG_UPDATE_INSTANCE_OWNERSHIP, 0x03B8C}, // 4.0.6a 13623
            {Opcode.SMSG_UPDATE_ITEM_ENCHANTMENTS, 0x020E8}, // 4.0.6a 13623
            {Opcode.SMSG_UPDATE_LAST_INSTANCE, 0x033E8}, // 4.0.6a 13623
            {Opcode.SMSG_UPDATE_OBJECT, 0x03780}, // 4.0.6a 13623
            {Opcode.SMSG_UPDATE_WORLD_STATE, 0x0F784}, // 4.0.6a 13623
            {Opcode.SMSG_USERLIST_ADD, 0x0F8CC}, // 4.0.6a 13623
            {Opcode.SMSG_USERLIST_REMOVE, 0x0EF80}, // 4.0.6a 13623
            {Opcode.SMSG_USERLIST_UPDATE, 0x02C8C}, // 4.0.6a 13623
            {Opcode.SMSG_VOICESESSION_FULL, 0x0A456}, // 4.0.3a 13329
            {Opcode.SMSG_VOICE_CHAT_STATUS, 0x06B88}, // 4.0.6a 13623
            {Opcode.SMSG_VOICE_PARENTAL_CONTROLS, 0x071E8}, // 4.0.6a 13623
            {Opcode.SMSG_VOICE_SESSION_ADJUST_PRIORITY, 0x10165}, // Unknown opcode ID
            {Opcode.SMSG_VOICE_SESSION_ENABLE, 0x10170}, // Unknown opcode ID
            {Opcode.SMSG_VOICE_SESSION_LEAVE, 0x078A4}, // 4.0.6a 13623
            {Opcode.SMSG_VOICE_SESSION_ROSTER_UPDATE, 0x0ACC0}, // 4.0.6a 13623
            {Opcode.SMSG_VOICE_SET_TALKER_MUTED, 0x0E3E4}, // 4.0.6a 13623
            {Opcode.SMSG_WARDEN_DATA, 0x0F8A0}, // 4.0.6a 13623
            {Opcode.SMSG_WARGAME_REQUEST_RESPONSE, 0x0094E}, // 4.0.6a 13623
            {Opcode.SMSG_WARGAME_REQUEST_SENT, 0x00C1C}, // 4.0.6a 13623
            {Opcode.SMSG_WEATHER, 0x079A0}, // 4.0.6a 13623
            {Opcode.SMSG_WHO, 0x0BE8C}, // 4.0.6a 13623
            {Opcode.SMSG_WHOIS, 0x0B1A4}, // 4.0.6a 13623
            {Opcode.SMSG_WORLD_STATE_UI_TIMER_UPDATE, 0x05557}, // 4.0.6a 13623
            {Opcode.SMSG_ZONE_UNDER_ATTACK, 0x0BD80}, // 4.0.6a 13623
        };
    }
}

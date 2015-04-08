using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V4_0_6_13596
{
    public static class Opcodes_4_0_6
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
            {Opcode.CMSG_ACCEPT_LEVEL_GRANT, 0x0B5CC},
            {Opcode.CMSG_ACCEPT_TRADE, 0x00891},
            {Opcode.CMSG_ACTIVATE_TAXI, 0x039A4},
            {Opcode.CMSG_ACTIVATE_TAXI_EXPRESS, 0x0FC8C},
            {Opcode.CMSG_ACTIVE_PVP_CHEAT, 0x1015E}, //UnknownopcodeID
            {Opcode.CMSG_ADD_FRIEND, 0x03980},
            {Opcode.CMSG_ADD_IGNORE, 0x06780},
            {Opcode.CMSG_ADD_PVP_MEDAL_CHEAT, 0x100D0}, //UnknownopcodeID
            {Opcode.CMSG_VOICE_ADD_IGNORE, 0x0B888},
            {Opcode.CMSG_ALTER_APPEARANCE, 0x034A4},
            {Opcode.CMSG_AREA_TRIGGER, 0x0ADA8},
            {Opcode.CMSG_AREA_SPIRIT_HEALER_QUERY, 0x0A6C0},
            {Opcode.CMSG_AREA_SPIRIT_HEALER_QUEUE, 0x0F388},
            {Opcode.CMSG_ARENA_TEAM_ACCEPT, 0x061AC},
            {Opcode.CMSG_ARENA_TEAM_CREATE, 0x00509},
            {Opcode.CMSG_ARENA_TEAM_DECLINE, 0x0F2C0},
            {Opcode.CMSG_ARENA_TEAM_DISBAND, 0x0698C},
            {Opcode.CMSG_ARENA_TEAM_INVITE, 0x0E9CC},
            {Opcode.CMSG_ARENA_TEAM_LEADER, 0x0218C},
            {Opcode.CMSG_ARENA_TEAM_LEAVE, 0x064C4},
            {Opcode.CMSG_ARENA_TEAM_QUERY, 0x0B9C8},
            {Opcode.CMSG_ARENA_TEAM_REMOVE, 0x07E84},
            {Opcode.CMSG_ARENA_TEAM_ROSTER, 0x06BAC},
            {Opcode.CMSG_ATTACK_STOP, 0x062C4},
            {Opcode.CMSG_ATTACK_SWING, 0x074A8},
            {Opcode.CMSG_AUCTION_LIST_BIDDER_ITEMS, 0x021C0},
            {Opcode.CMSG_AUCTION_LIST_ITEMS, 0x0E48C},
            {Opcode.CMSG_AUCTION_LIST_OWNER_ITEMS, 0x02D8C},
            {Opcode.CMSG_AUCTION_LIST_PENDING_SALES, 0x0EDEC},
            {Opcode.CMSG_AUCTION_PLACE_BID, 0x0A6A0},
            {Opcode.CMSG_AUCTION_REMOVE_ITEM, 0x03EEC},
            {Opcode.CMSG_AUCTION_SELL_ITEM, 0x0EE8C},
            {Opcode.CMSG_AUTH_CONTINUED_SESSION, 0x00E4C},
            //{Opcode.CMSG_AUTH_CONTINUED_SESSION, 0x00100},
            {Opcode.CMSG_AUTH_SESSION, 0x00E0E},
            {Opcode.CMSG_AUTOBANK_ITEM, 0x066E4},
            {Opcode.CMSG_AUTO_EQUIP_GROUND_ITEM, 0x10044}, //UnknownopcodeID
            {Opcode.CMSG_AUTO_EQUIP_ITEM, 0x0E1C0},
            {Opcode.CMSG_AUTO_EQUIP_ITEM_SLOT, 0x0E8A8},
            {Opcode.CMSG_AUTOSTORE_BAG_ITEM, 0x0EDCC},
            {Opcode.CMSG_AUTOSTORE_BANK_ITEM, 0x0F2AC},
            {Opcode.CMSG_AUTOSTORE_GROUND_ITEM, 0x02FC8},
            //{Opcode.CMSG_AUTOSTORE_LOOT_CURRENCY, 0x00991},
            {Opcode.CMSG_AUTOSTORE_LOOT_ITEM, 0x0B2E8},
            {Opcode.CMSG_AUTO_DECLINE_GUILD_INVITES, 0x0EDAC},
            {Opcode.CMSG_BANKER_ACTIVATE, 0x0E7E0},
            {Opcode.CMSG_BATTLEFIELD_JOIN, 0x00C91},
            {Opcode.CMSG_BATTLEFIELD_LIST, 0x00508},
            {Opcode.CMSG_BF_MGR_ENTRY_INVITE_RESPONSE, 0x00100},
            {Opcode.CMSG_BF_MGR_QUEUE_EXIT_REQUEST, 0x08581},
            {Opcode.CMSG_BF_MGR_QUEUE_INVITE_RESPONSE, 0x08108},
            {Opcode.CMSG_BF_MGR_QUEUE_REQUEST, 0x00611},
            {Opcode.CMSG_BATTLEFIELD_PORT, 0x00E11},
            {Opcode.CMSG_BATTLEFIELD_REQUEST_SCORE_DATA, 0x00493},
            {Opcode.CMSG_BATTLEFIELD_STATUS, 0x08188},
            {Opcode.CMSG_BATTLEGROUND_PLAYER_POSITIONS, 0x00293},
            //{Opcode.CMSG_BATTLEGROUND_PORT_AND_LEAVE, 0x00E11},
            {Opcode.CMSG_BATTLEMASTER_HELLO, 0x06EC8},
            {Opcode.CMSG_BATTLEMASTER_JOIN, 0x00137}, //UnknownopcodeID
            {Opcode.CMSG_BATTLEMASTER_JOIN_ARENA, 0x00311},
            {Opcode.CMSG_BATTLEMASTER_JOIN_RATED, 0x10104}, //UnknownopcodeID
            {Opcode.CMSG_BEGIN_TRADE, 0x00F93},
            {Opcode.CMSG_BINDER_ACTIVATE, 0x0A48C},
            {Opcode.CMSG_BOT_DETECTED, 0x0E757}, //4.0.3a13329-403
            {Opcode.CMSG_BOT_DETECTED2, 0x10002}, //UnknownopcodeID
            {Opcode.CMSG_BUG, 0x034AC},
            {Opcode.CMSG_BUSY_TRADE, 0x1004C}, //UnknownopcodeID
            {Opcode.CMSG_BUY_BACK_ITEM, 0x0A4C4},
            {Opcode.CMSG_BUY_BANK_SLOT, 0x02BA0},
            {Opcode.CMSG_BUY_ITEM, 0x0EA84},
            {Opcode.CMSG_BUY_LOTTERY_TICKET_OBSOLETE, 0x00336}, //4.0.3a13329-403
            {Opcode.CMSG_CALENDAR_ADD_EVENT, 0x0F488},
            {Opcode.CMSG_CALENDAR_ARENA_TEAM, 0x0E9AC},
            {Opcode.CMSG_CALENDAR_COMPLAIN, 0x01E75},
            {Opcode.CMSG_CALENDAR_COPY_EVENT, 0x0BF84},
            {Opcode.CMSG_CALENDAR_EVENT_INVITE, 0x0F6C4},
            {Opcode.CMSG_CALENDAR_EVENT_INVITE_NOTES, 0x0045F}, //UnknownopcodeID
            {Opcode.CMSG_CALENDAR_EVENT_MODERATOR_STATUS, 0x0BDE4},
            {Opcode.CMSG_CALENDAR_EVENT_REMOVE_INVITE, 0x0EBCC},
            {Opcode.CMSG_CALENDAR_EVENT_RSVP, 0x0757F},
            {Opcode.CMSG_CALENDAR_EVENT_SIGN_UP, 0x0DC74},
            {Opcode.CMSG_CALENDAR_EVENT_STATUS, 0x0BC84},
            //{Opcode.CMSG_CALENDAR_GET_CALENDAR, 0x0B2A4},
            {Opcode.CMSG_CALENDAR_GET_EVENT, 0x03580},
            {Opcode.CMSG_CALENDAR_GET_NUM_PENDING, 0x0EFAC},
            {Opcode.CMSG_CALENDAR_GUILD_FILTER, 0x0ADAC},
            {Opcode.CMSG_CALENDAR_REMOVE_EVENT, 0x06C88},
            {Opcode.CMSG_CALENDAR_UPDATE_EVENT, 0x0F084},
            {Opcode.CMSG_CANCEL_AURA, 0x07684},
            {Opcode.CMSG_CANCEL_AUTO_REPEAT_SPELL, 0x075CC},
            {Opcode.CMSG_CANCEL_CAST, 0x0A1C0},
            {Opcode.CMSG_CANCEL_CHANNELLING, 0x0A780},
            {Opcode.CMSG_CANCEL_GROWTH_AURA, 0x0FECC},
            {Opcode.CMSG_CANCEL_MOUNT_AURA, 0x064CC}, //notsure
            {Opcode.CMSG_CANCEL_TEMP_ENCHANTMENT, 0x0E484},
            {Opcode.CMSG_CANCEL_TRADE, 0x00013},
            {Opcode.CMSG_CAST_SPELL, 0x065C4},
            {Opcode.CMSG_CHANGE_PERSONAL_ARENA_RATING, 0x101B2}, //UnknownopcodeID
            {Opcode.CMSG_CHANGE_SEATS_ON_CONTROLLED_VEHICLE, 0x0E988},
            {Opcode.CMSG_CHAT_CHANNEL_ANNOUNCEMENTS, 0x0004B},
            {Opcode.CMSG_CHAT_CHANNEL_BAN, 0x0000A},
            {Opcode.CMSG_CHAT_CHANNEL_DECLINE_INVITE, 0x0DF7F},
            {Opcode.CMSG_CHAT_CHANNEL_DISPLAY_LIST, 0x10185}, //UnknownopcodeID
            {Opcode.CMSG_CHAT_CHANNEL_INVITE, 0x00020},
            {Opcode.CMSG_CHAT_CHANNEL_KICK, 0x00068},
            {Opcode.CMSG_CHAT_CHANNEL_LIST, 0x07FE0},
            {Opcode.CMSG_CHAT_CHANNEL_MODERATE, 0x10018}, //UnknownopcodeID
            {Opcode.CMSG_CHAT_CHANNEL_MODERATOR, 0x00828},
            {Opcode.CMSG_CHAT_CHANNEL_MUTE, 0x00023},
            {Opcode.CMSG_CHAT_CHANNEL_OWNER, 0x00848},
            {Opcode.CMSG_CHAT_CHANNEL_PASSWORD, 0x0080A},
            {Opcode.CMSG_CHAT_CHANNEL_ROSTER_INFO, 0x00069},
            {Opcode.CMSG_CHAT_CHANNEL_SET_OWNER, 0x00800},
            {Opcode.CMSG_CHAT_CHANNEL_SILENCE_ALL, 0x01A90}, //UnknownopcodeID
            {Opcode.CMSG_CHAT_CHANNEL_SILENCE_VOICE, 0x07A18}, //UnknownopcodeID
            {Opcode.CMSG_CHAT_CHANNEL_UNBAN, 0x00048},
            {Opcode.CMSG_CHAT_CHANNEL_UNMODERATOR, 0x00809},
            {Opcode.CMSG_CHAT_CHANNEL_UNMUTE, 0x00841},
            {Opcode.CMSG_CHAT_CHANNEL_UNSILENCE_ALL, 0x10186}, //UnknownopcodeID
            {Opcode.CMSG_CHAT_CHANNEL_UNSILENCE_VOICE, 0x06A80}, //UnknownopcodeID
            {Opcode.CMSG_CHAT_CHANNEL_VOICE_OFF, 0x07A88}, //4.0.3a13329-403
            {Opcode.CMSG_CHAT_CHANNEL_VOICE_ON, 0x05A98}, //4.0.3a13329-403
            {Opcode.CMSG_CHARACTER_POINT_CHEAT, 0x100A0}, //UnknownopcodeID
            {Opcode.CMSG_CREATE_CHARACTER, 0x07EEC},
            {Opcode.CMSG_CHAR_CUSTOMIZE, 0x06484},
            {Opcode.CMSG_CHAR_DELETE, 0x03B84},
            {Opcode.CMSG_ENUM_CHARACTERS, 0x06AA4},
            {Opcode.CMSG_CHAR_FACTION_CHANGE, 0x0BBCC},
            {Opcode.CMSG_CHAR_RACE_CHANGE, 0x06EA4},
            {Opcode.CMSG_CHARACTER_RENAME_REQUEST, 0x027C4},
            {Opcode.CMSG_CHEAT_DUMP_ITEMS_DEBUG_ONLY, 0x1015F}, //UnknownopcodeID
            {Opcode.CMSG_CHEAT_PLAYER_LOGIN, 0x1017D}, //UnknownopcodeID
            {Opcode.CMSG_CHEAT_PLAYER_LOOKUP, 0x1017E}, //UnknownopcodeID
            {Opcode.CMSG_CHEAT_SET_ARENA_CURRENCY, 0x1014E}, //UnknownopcodeID
            {Opcode.CMSG_CHEAT_SET_HONOR_CURRENCY, 0x1014D}, //UnknownopcodeID
            {Opcode.CMSG_CHECK_LOGIN_CRITERIA, 0x101F8}, //UnknownopcodeID
            {Opcode.CMSG_CLEAR_CHANNEL_WATCH, 0x0A1E0},
            {Opcode.CMSG_CLEAR_EXPLORATION, 0x100A9}, //UnknownopcodeID
            {Opcode.CMSG_CLEAR_RAID_MARKER, 0x02218},
            {Opcode.CMSG_CLEAR_SERVER_BUCK_DATA, 0x101AD}, //UnknownopcodeID
            {Opcode.CMSG_CLEAR_TRADE_ITEM, 0x00213},
            {Opcode.CMSG_COMMENTATOR_ENABLE, 0x0B1C4},
            {Opcode.CMSG_COMMENTATOR_ENTER_INSTANCE, 0x07BCC},
            {Opcode.CMSG_COMMENTATOR_EXIT_INSTANCE, 0x0B8E4},
            {Opcode.CMSG_COMMENTATOR_GET_MAP_INFO, 0x028C4},
            {Opcode.CMSG_COMMENTATOR_GET_PLAYER_INFO, 0x0BBA0},
            {Opcode.CMSG_COMMENTATOR_INSTANCE_COMMAND, 0x07D80},
            {Opcode.CMSG_COMMENTATOR_SKIRMISH_QUEUE_COMMAND, 0x06DC8},
            {Opcode.CMSG_COMMENTATOR_START_WARGAME, 0x08588},
            {Opcode.CMSG_COMPLAINT, 0x068C8},
            //{Opcode.CMSG_COMPLETED_ARTIFACTS, 0x00A13},
            {Opcode.CMSG_COMPLETE_ACHIEVEMENT_CHEAT, 0x101DC}, //UnknownopcodeID
            {Opcode.CMSG_COMPLETE_CINEMATIC, 0x02ACC},
            {Opcode.CMSG_COMPLETE_MOVIE, 0x0E188},
            {Opcode.CMSG_CONNECT_TO_FAILED, 0x10007}, //UnknownopcodeID
            {Opcode.CMSG_CONTACT_LIST, 0x0EAA4},
            {Opcode.CMSG_CORPSE_MAP_POSITION_QUERY, 0x023CC},
            {Opcode.CMSG_QUERY_CREATURE, 0x0268C},
            {Opcode.CMSG_DANCE_QUERY, 0x022A0},
            {Opcode.CMSG_DEBUG_ACTIONS_START, 0x1011B}, //UnknownopcodeID
            {Opcode.CMSG_DEBUG_ACTIONS_STOP, 0x1011C}, //UnknownopcodeID
            {Opcode.CMSG_DEBUG_LIST_TARGETS, 0x10187}, //UnknownopcodeID
            {Opcode.CMSG_DECHARGE, 0x10092}, //UnknownopcodeID
            {Opcode.CMSG_DELETE_DANCE, 0x101C8}, //UnknownopcodeID
            {Opcode.CMSG_DEL_FRIEND, 0x02980},
            {Opcode.CMSG_DEL_IGNORE, 0x0F384},
            {Opcode.CMSG_DEL_PVP_MEDAL_CHEAT, 0x100D1}, //UnknownopcodeID
            {Opcode.CMSG_VOICE_DEL_IGNORE, 0x07AC0},
            //{Opcode.CMSG_DESTROY_ITEM, 0x0B8A8},
            {Opcode.CMSG_DESTROY_ITEMS, 0x0B8A8},
            {Opcode.CMSG_DISMISS_CONTROLLED_VEHICLE, 0x0E3C0},
            {Opcode.CMSG_DISMISS_CRITTER, 0x0B7CC},
            {Opcode.CMSG_DUEL_ACCEPTED, 0x0A688},
            {Opcode.CMSG_DUEL_CANCELLED, 0x06F8C},
            {Opcode.CMSG_DUMP_OBJECTS, 0x101ED}, //UnknownopcodeID
            {Opcode.CMSG_EJECT_PASSENGER, 0x0F688},
            {Opcode.CMSG_EMOTE, 0x0FAC4},
            {Opcode.CMSG_ENABLE_DAMAGE_LOG, 0x100CB}, //UnknownopcodeID
            {Opcode.CMSG_ENABLE_TAXI_NODE, 0x0328C},
            {Opcode.CMSG_EQUIPMENT_SET_DELETE, 0x0AEA0},
            {Opcode.CMSG_EQUIPMENT_SET_SAVE, 0x0BFC0},
            {Opcode.CMSG_EQUIPMENT_SET_USE, 0x0E8A0},
            {Opcode.CMSG_EXPIRE_RAID_INSTANCE, 0x101A7}, //UnknownopcodeID
            {Opcode.CMSG_FAR_SIGHT, 0x0B2EC},
            {Opcode.CMSG_FLOOD_GRACE_CHEAT, 0x101F2}, //UnknownopcodeID
            {Opcode.CMSG_FORCE_FLIGHT_BACK_SPEED_CHANGE_ACK, 0x0FBC8},
            {Opcode.CMSG_FORCE_FLIGHT_SPEED_CHANGE_ACK, 0x0A98C},
            {Opcode.CMSG_FORCE_MOVE_ROOT_ACK, 0x07184},
            {Opcode.CMSG_FORCE_MOVE_UNROOT_ACK, 0x07FA8},
            {Opcode.CMSG_FORCE_PITCH_RATE_CHANGE_ACK, 0x0E6C0},
            {Opcode.CMSG_FORCE_RUN_BACK_SPEED_CHANGE_ACK, 0x078E4},
            {Opcode.CMSG_FORCE_RUN_SPEED_CHANGE_ACK, 0x026C8},
            {Opcode.CMSG_FORCE_SAY_CHEAT, 0x101E7}, //UnknownopcodeID
            {Opcode.CMSG_FORCE_SWIM_BACK_SPEED_CHANGE_ACK, 0x0E8C4},
            {Opcode.CMSG_FORCE_SWIM_SPEED_CHANGE_ACK, 0x0B1A0},
            {Opcode.CMSG_FORCE_TURN_RATE_CHANGE_ACK, 0x0E384},
            {Opcode.CMSG_FORCE_WALK_SPEED_CHANGE_ACK, 0x078CC},
            {Opcode.CMSG_QUERY_GAME_OBJECT, 0x072A0},
            {Opcode.CMSG_GAME_OBJ_REPORT_USE, 0x023A0},
            {Opcode.CMSG_GAME_OBJ_USE, 0x029E4},
            {Opcode.CMSG_GAMESPEED_SET, 0x02084},
            //{Opcode.CMSG_GAMETIME_SET, 0x020C4},
            {Opcode.CMSG_GETDEATHBINDZONE, 0x1005E}, //UnknownopcodeID
            {Opcode.CMSG_GET_CHANNEL_MEMBER_COUNT, 0x00009},
            {Opcode.CMSG_GET_ITEM_PURCHASE_DATA, 0x031E0},
            {Opcode.CMSG_MAIL_GET_LIST, 0x0B284},
            {Opcode.CMSG_GET_MIRROR_IMAGE_DATA, 0x0A08C},
            {Opcode.CMSG_GHOST, 0x10088}, //UnknownopcodeID
            {Opcode.CMSG_GM_TICKET_RESPONSE_RESOLVE, 0x062C8},
            {Opcode.CMSG_GM_SURVEY_SUBMIT, 0x0E280},
            {Opcode.CMSG_GMTICKETSYSTEM_TOGGLE, 0x100D7}, //UnknownopcodeID
            {Opcode.CMSG_GM_TICKET_CREATE, 0x06380},
            {Opcode.CMSG_GM_TICKET_DELETE_TICKET, 0x0FBE4},
            {Opcode.CMSG_GM_TICKET_GET_TICKET, 0x0B4C4},
            {Opcode.CMSG_GM_TICKET_GET_SYSTEM_STATUS, 0x0ACE0},
            {Opcode.CMSG_GM_TICKET_UPDATE_TEXT, 0x07F8C},
            {Opcode.CMSG_GM_CHARACTER_RESTORE, 0x10198}, //UnknownopcodeID
            {Opcode.CMSG_GM_CHARACTER_SAVE, 0x10199}, //UnknownopcodeID
            {Opcode.CMSG_GM_CREATE_ITEM_TARGET, 0x10096}, //UnknownopcodeID
            {Opcode.CMSG_GM_DESTROY_ONLINE_CORPSE, 0x10118}, //UnknownopcodeID
            {Opcode.CMSG_GM_INVIS, 0x10089}, //UnknownopcodeID
            {Opcode.CMSG_GM_LAG_REPORT, 0x03FA0},
            {Opcode.CMSG_GM_NUKE, 0x1008F}, //UnknownopcodeID
            {Opcode.CMSG_GM_NUKE_ACCOUNT, 0x10116}, //UnknownopcodeID
            {Opcode.CMSG_GM_SET_SECURITY_GROUP, 0x1008E}, //UnknownopcodeID
            {Opcode.CMSG_GM_SHOW_COMPLAINTS, 0x10181}, //UnknownopcodeID
            {Opcode.CMSG_GM_TEACH, 0x10095}, //UnknownopcodeID
            {Opcode.CMSG_GM_UNSQUELCH, 0x10182}, //UnknownopcodeID
            {Opcode.CMSG_GM_UNTEACH, 0x100FD}, //UnknownopcodeID
            {Opcode.CMSG_GM_UPDATE_TICKET_STATUS, 0x10124}, //UnknownopcodeID
            {Opcode.CMSG_GM_WHISPER, 0x10172}, //UnknownopcodeID
            {Opcode.CMSG_GOSSIP_HELLO, 0x074C8},
            {Opcode.CMSG_GOSSIP_SELECT_OPTION, 0x0FF88},
            {Opcode.CMSG_GRANT_LEVEL, 0x0B980},
            {Opcode.CMSG_GROUP_ACCEPT, 0x0368C},
            {Opcode.CMSG_GROUP_CANCEL, 0x1000A}, //UnknownopcodeID
            {Opcode.CMSG_GROUP_CHANGE_SUB_GROUP, 0x03A80},
            {Opcode.CMSG_GROUP_DECLINE, 0x0B4CC},
            {Opcode.CMSG_GROUP_DISBAND, 0x0BE88},
            {Opcode.CMSG_GROUP_INVITE, 0x027C0},
            {Opcode.CMSG_GROUP_RAID_CONVERT, 0x0628C},
            {Opcode.CMSG_GROUP_SET_LEADER, 0x0B6E0},
            {Opcode.CMSG_GROUP_SET_ROLES, 0x08509},
            {Opcode.CMSG_GROUP_SWAP_SUB_GROUP, 0x031C8},
            {Opcode.CMSG_GROUP_UNINVITE, 0x04F74},
            {Opcode.CMSG_GROUP_UNINVITE_GUID, 0x0E3C8},
            {Opcode.CMSG_GUILD_ACCEPT, 0x03729},
            {Opcode.CMSG_GUILD_ACHIEVEMENT_MEMBERS, 0x02509},
            {Opcode.CMSG_GUILD_ADD_RANK, 0x02309},
            {Opcode.CMSG_GUILD_BANK_ACTIVATE, 0x0FFC4},
            {Opcode.CMSG_GUILD_BANK_BUY_TAB, 0x078AC},
            {Opcode.CMSG_GUILD_BANK_DEPOSIT_MONEY, 0x06FE8},
            //{Opcode.CMSG_GUILD_BANK_NOTE, 0x07680},
            {Opcode.CMSG_GUILD_BANK_QUERY_TAB, 0x0A788},
            {Opcode.CMSG_GUILD_BANK_SET_TAB_TEXT, 0x07680},
            {Opcode.CMSG_GUILD_BANK_SWAP_ITEMS, 0x0A8C4},
            {Opcode.CMSG_GUILD_BANK_UPDATE_TAB, 0x0E3CC},
            {Opcode.CMSG_GUILD_BANK_WITHDRAW_MONEY, 0x073A8},
            {Opcode.CMSG_GUILD_CREATE, 0x02219},
            {Opcode.CMSG_GUILD_DECLINE_INVITATION, 0x0352D},
            {Opcode.CMSG_GUILD_DELETE_RANK, 0x02129},
            {Opcode.CMSG_GUILD_DEMOTE_MEMBER, 0x0330D},
            {Opcode.CMSG_GUILD_DISBAND, 0x0372D}, //opcode14125
            {Opcode.CMSG_GUILD_GET_ROSTER, 0x0B2A4},
            {Opcode.CMSG_GUILD_INFO, 0x06884},
            {Opcode.CMSG_GUILD_INFO_TEXT, 0x0270D},
            {Opcode.CMSG_GUILD_INVITE, 0x02DA8}, //opcode11688
            {Opcode.CMSG_GUILD_LEADER, 0x02650}, //opcode9808
            {Opcode.CMSG_GUILD_LEAVE, 0x03329}, //opcode13097
            {Opcode.CMSG_GUILD_MOTD, 0x0272D}, //opcode10029
            //{Opcode.CMSG_GUILD_NEWS_SET_STICKY, 0x0252D},
            {Opcode.CMSG_GUILD_OFFICER_REMOVE_MEMBER, 0x0312D}, //opcode12589
            {Opcode.CMSG_GUILD_PROMOTE_MEMBER, 0x02109},
            {Opcode.CMSG_QUERY_GUILD_INFO, 0x0AFC4},
            //{Opcode.CMSG_GUILD_QUERY_MAX_XP, 0x0350D},
            {Opcode.CMSG_GUILD_QUERY_MEMBERS_FOR_RECIPE, 0x0210D},
            {Opcode.CMSG_GUILD_QUERY_MEMBER_RECIPES, 0x0212D},
            {Opcode.CMSG_GUILD_QUERY_NEWS, 0x03529},
            {Opcode.CMSG_GUILD_QUERY_TRADESKILL, 0x02329},
            //{Opcode.CMSG_GUILD_RANK, 0x02709},
            //{Opcode.CMSG_GUILD_RANKS, 0x03129},
            //{Opcode.CMSG_GUILD_REQUEST_NEWS, 0x03129},
            //{Opcode.CMSG_REQUEST_GUILD_PARTY_STATE, 0x02219},
            {Opcode.CMSG_GUILD_SET_NOTE, 0x0232D},
            {Opcode.CMSG_GUILD_SWITCH_RANK, 0x03309}, //UnknownopcodeID
            //{Opcode.CMSG_GUILD_UPDATE_PARTY_STATE, 0x02219},
            {Opcode.CMSG_HEARTH_AND_RESURRECT, 0x0B6C4},
            {Opcode.CMSG_IGNORE_DIMINISHING_RETURNS_CHEAT, 0x1019D}, //UnknownopcodeID
            {Opcode.CMSG_IGNORE_KNOCKBACK_CHEAT, 0x10126}, //UnknownopcodeID
            {Opcode.CMSG_IGNORE_REQUIREMENTS_CHEAT, 0x1016D}, //UnknownopcodeID
            {Opcode.CMSG_IGNORE_TRADE, 0x1004D}, //UnknownopcodeID
            {Opcode.CMSG_INITIATE_TRADE, 0x00413},
            {Opcode.CMSG_INSPECT, 0x078A8},
            {Opcode.CMSG_INSTANCE_LOCK_WARNING_RESPONSE, 0x034C4},
            {Opcode.CMSG_ITEM_NAME_QUERY, 0x100E3}, //UnknownopcodeID
            {Opcode.CMSG_ITEM_PURCHASE_REFUND, 0x062E8},
            {Opcode.CMSG_ITEM_TEXT_QUERY, 0x0F280},
            {Opcode.CMSG_CHAT_JOIN_CHANNEL, 0x00002},
            //{Opcode.CMSG_JOIN_RATED_BATTLEFIELD, 0x00591},
            {Opcode.CMSG_KEEP_ALIVE, 0x02CE0},
            {Opcode.CMSG_LEARN_DANCE_MOVE, 0x101C9}, //UnknownopcodeID
            {Opcode.CMSG_LEARN_PREVIEW_TALENTS, 0x0FFAC},
            {Opcode.CMSG_LEARN_PREVIEW_TALENTS_PET, 0x0BCE8},
            {Opcode.CMSG_LEARN_TALENT, 0x0A7CC},
            {Opcode.CMSG_LEAVE_BATTLEFIELD, 0x07DC4},
            {Opcode.CMSG_CHAT_LEAVE_CHANNEL, 0x0000B},
            //{Opcode.CMSG_LFD_PARTY_LOCK_INFO_REQUEST, 0x00574}, //4.0.3a13329-403
            //{Opcode.CMSG_LFD_PLAYER_LOCK_INFO_REQUEST, 0x0E5E8},// 0x00C76
            {Opcode.CMSG_LFG_JOIN, 0x063C0},
            {Opcode.CMSG_LFG_LEAVE, 0x03688},
            {Opcode.CMSG_LFG_PROPOSAL_RESULT, 0x0A7A4},
            {Opcode.CMSG_LFG_SET_BOOT_VOTE, 0x0D65D}, //UnknownopcodeID
            {Opcode.CMSG_LFG_SET_NEEDS, 0x10145}, //UnknownopcodeID
            {Opcode.CMSG_LFG_SET_ROLES, 0x0E8CC},
            {Opcode.CMSG_LFG_SET_ROLES_2, 0x1020B}, //UnknownopcodeID
            {Opcode.CMSG_LFG_TELEPORT, 0x0FA88},
            {Opcode.CMSG_LIST_INVENTORY, 0x0EDC8},
            {Opcode.CMSG_LOAD_DANCES, 0x101C4}, //UnknownopcodeID
            {Opcode.CMSG_LOGOUT_CANCEL, 0x039E8},
            {Opcode.CMSG_LOGOUT_REQUEST, 0x0A7A8},
            {Opcode.CMSG_LOOT_UNIT, 0x0FCEC},
            {Opcode.CMSG_LOOT_MASTER_GIVE, 0x03BA4},
            {Opcode.CMSG_SET_LOOT_METHOD, 0x0FCCC},
            {Opcode.CMSG_LOOT_MONEY, 0x079E0},
            {Opcode.CMSG_LOOT_RELEASE, 0x03CE8},
            {Opcode.CMSG_LOOT_ROLL, 0x0BDA8},
            {Opcode.CMSG_LOTTERY_QUERY_OBSOLETE, 0x10129}, //UnknownopcodeID
            {Opcode.CMSG_LOW_LEVEL_RAID1, 0x035EC},
            {Opcode.CMSG_LOW_LEVEL_RAID2, 0x029C4},
            {Opcode.CMSG_LUA_USAGE, 0x10122}, //UnknownopcodeID
            {Opcode.CMSG_MAELSTROM_GM_SENT_MAIL, 0x1015D}, //UnknownopcodeID
            {Opcode.CMSG_MAELSTROM_INVALIDATE_CACHE, 0x10155}, //UnknownopcodeID
            {Opcode.CMSG_MAELSTROM_RENAME_GUILD, 0x1019A}, //UnknownopcodeID
            {Opcode.CMSG_MAIL_CREATE_TEXT_ITEM, 0x0FAE4},
            {Opcode.CMSG_MAIL_DELETE, 0x07DE4},
            {Opcode.CMSG_MAIL_MARK_AS_READ, 0x0E8C0},
            {Opcode.CMSG_MAIL_RETURN_TO_SENDER, 0x065A4},
            {Opcode.CMSG_MAIL_TAKE_ITEM, 0x062A8},
            {Opcode.CMSG_MAIL_TAKE_MONEY, 0x0E8EC},
            {Opcode.CMSG_MEETINGSTONE_CHEAT, 0x100D5}, //UnknownopcodeID
            {Opcode.CMSG_MEETINGSTONE_INFO, 0x0F984},
            {Opcode.CMSG_CHAT_MESSAGE_AFK, 0x0086B},
            {Opcode.CMSG_CHAT_MESSAGE_BATTLEGROUND, 0x00063},
            {Opcode.CMSG_CHAT_MESSAGE_BATTLEGROUND_LEADER, 0x00860},
            {Opcode.CMSG_CHAT_MESSAGE_CHANNEL, 0x00821},
            {Opcode.CMSG_CHAT_MESSAGE_DND, 0x00003},
            {Opcode.CMSG_CHAT_MESSAGE_EMOTE, 0x00042},
            {Opcode.CMSG_CHAT_MESSAGE_GUILD, 0x00823},
            {Opcode.CMSG_CHAT_MESSAGE_OFFICER, 0x00861},
            {Opcode.CMSG_CHAT_MESSAGE_PARTY, 0x0084B},
            {Opcode.CMSG_CHAT_MESSAGE_PARTY_LEADER, 0x0080B},
            {Opcode.CMSG_CHAT_MESSAGE_RAID, 0x00803},
            {Opcode.CMSG_CHAT_MESSAGE_RAID_LEADER, 0x00863},
            {Opcode.CMSG_CHAT_MESSAGE_RAID_WARNING, 0x00061},
            {Opcode.CMSG_CHAT_MESSAGE_SAY, 0x0002A},
            {Opcode.CMSG_CHAT_MESSAGE_WHISPER, 0x00000},
            {Opcode.CMSG_CHAT_MESSAGE_YELL, 0x00802},
            {Opcode.CMSG_MINIGAME_MOVE, 0x0B2E4},
            {Opcode.CMSG_MOUNT_SPECIAL_ANIM, 0x02EE4},
            {Opcode.CMSG_MOVE_CHNG_TRANSPORT, 0x10158}, //UnknownopcodeID
            {Opcode.CMSG_MOVE_FALL_RESET, 0x0E680},
            {Opcode.CMSG_MOVE_FEATHER_FALL_ACK, 0x06EA8},
            {Opcode.CMSG_MOVE_FLIGHT_ACK, 0x0A3C8},
            {Opcode.CMSG_MOVE_GRAVITY_DISABLE_ACK, 0x0F0C0},
            {Opcode.CMSG_MOVE_GRAVITY_ENABLE_ACK, 0x07DE8},
            {Opcode.CMSG_MOVE_HOVER_ACK, 0x0F4CC},
            {Opcode.CMSG_MOVE_KNOCK_BACK_ACK, 0x0F580},
            {Opcode.CMSG_MOVE_NOT_ACTIVE_MOVER, 0x0B9A8},
            {Opcode.CMSG_MOVE_SET_CAN_FLY_ACK, 0x0FCAC},
            {Opcode.CMSG_MOVE_SET_FLY, 0x0E0E0},
            {Opcode.CMSG_MOVE_SET_RAW_POSITION, 0x0F0C8},
            {Opcode.CMSG_MOVE_SET_RUN_SPEED, 0x1016F}, //UnknownopcodeID
            {Opcode.CMSG_MOVE_SPLINE_DONE, 0x069E8},
            {Opcode.CMSG_MOVE_START_SWIM_CHEAT, 0x026C0},
            //{Opcode.CMSG_MOVE_STOP_SWIM_CHEAT, 0x06988},
            {Opcode.CMSG_MOVE_TIME_SKIPPED, 0x0E180},
            {Opcode.CMSG_MOVE_WATER_WALK_ACK, 0x021C4},
            {Opcode.CMSG_NAME_QUERY, 0x07AAC},
            {Opcode.CMSG_NEW_SPELL_SLOT, 0x10057}, //UnknownopcodeID
            {Opcode.CMSG_NEXT_CINEMATIC_CAMERA, 0x0B2CC},
            {Opcode.CMSG_NO_SPELL_VARIANCE, 0x101A8}, //UnknownopcodeID
            {Opcode.CMSG_QUERY_NPC_TEXT, 0x0A2EC},
            {Opcode.CMSG_OFFER_PETITION, 0x07AC4},
            {Opcode.CMSG_OPENING_CINEMATIC, 0x0B1E8},
            {Opcode.CMSG_OPEN_ITEM, 0x0A2A8},
            {Opcode.CMSG_OPT_OUT_OF_LOOT, 0x075A4},
            {Opcode.CMSG_QUERY_PAGE_TEXT, 0x0AC8C},
            {Opcode.CMSG_PARTY_SILENCE, 0x06CC4},
            {Opcode.CMSG_PARTY_UNSILENCE, 0x0FCA4},
            {Opcode.CMSG_PETITION_BUY, 0x0B3E4},
            {Opcode.CMSG_PETITION_QUERY, 0x0B1AC},
            {Opcode.CMSG_PETITION_SHOW_LIST, 0x0FCC4},
            {Opcode.CMSG_PETITION_SHOW_SIGNATURES, 0x02CA8},
            {Opcode.CMSG_PETITION_SIGN, 0x03AA0},
            {Opcode.CMSG_PET_ABANDON, 0x0A480},
            {Opcode.CMSG_PET_ACTION, 0x0AFC0},
            {Opcode.CMSG_PET_CANCEL_AURA, 0x0F6C0},
            {Opcode.CMSG_PET_CAST_SPELL, 0x02888},
            {Opcode.CMSG_PET_LEARN_TALENT, 0x0A7A0},
            {Opcode.CMSG_QUERY_PET_NAME, 0x0F180},
            {Opcode.CMSG_PET_RENAME, 0x038E8},
            {Opcode.CMSG_PET_SET_ACTION, 0x03C8C},
            {Opcode.CMSG_PET_SPELL_AUTOCAST, 0x0B6A4},
            {Opcode.CMSG_PET_STOP_ATTACK, 0x03A88},
            {Opcode.CMSG_PET_UNLEARN, 0x10105}, //UnknownopcodeID
            {Opcode.CMSG_PET_UNLEARN_TALENTS, 0x08F5D}, //4.0.3a13329-403
            {Opcode.CMSG_PING, 0x0064E},
            {Opcode.CMSG_REQUEST_PLAYED_TIME, 0x0F480},
            {Opcode.CMSG_PLAYER_AI_CHEAT, 0x100C2}, //UnknownopcodeID
            {Opcode.CMSG_PLAYER_DIFFICULTY_CHANGE, 0x03F88},
            {Opcode.CMSG_PLAYER_LOGIN, 0x08180},
            {Opcode.CMSG_PLAYER_LOGOUT, 0x0F78C},
            {Opcode.CMSG_PLAYER_VEHICLE_ENTER, 0x0AEC8},
            {Opcode.CMSG_PLAY_DANCE, 0x02288},
            {Opcode.CMSG_CLIENT_PORT_GRAVEYARD, 0x00593},
            {Opcode.CMSG_PUSH_QUEST_TO_PARTY, 0x029E8},
            //{Opcode.CMSG_QUERY_INSPECT_ACHIEVEMENTS, 0x069A0},
            {Opcode.CMSG_QUERY_QUESTS_COMPLETED, 0x0ECE8},
            {Opcode.CMSG_QUERY_SERVER_BUCK_DATA, 0x101AC}, //UnknownopcodeID
            {Opcode.CMSG_QUERY_TIME, 0x0B1C0},
            {Opcode.CMSG_QUERY_VEHICLE_STATUS, 0x069A0},
            {Opcode.CMSG_QUEST_GIVER_ACCEPT_QUEST, 0x020C4},
            {Opcode.CMSG_QUEST_GIVER_CANCEL, 0x061EC},
            {Opcode.CMSG_QUEST_GIVER_CHOOSE_REWARD, 0x06AC0},
            {Opcode.CMSG_QUEST_GIVER_COMPLETE_QUEST, 0x0B5AC},
            {Opcode.CMSG_QUEST_GIVER_HELLO, 0x036AC},
            {Opcode.CMSG_QUEST_GIVER_QUERY_QUEST, 0x02CC0},
            {Opcode.CMSG_QUEST_GIVER_QUEST_AUTOLAUNCH, 0x10064}, //UnknownopcodeID
            {Opcode.CMSG_QUEST_GIVER_REQUEST_REWARD, 0x023A8},
            {Opcode.CMSG_QUEST_GIVER_STATUS_MULTIPLE_QUERY, 0x02DAC},
            {Opcode.CMSG_QUEST_GIVER_STATUS_QUERY, 0x0FDEC},
            {Opcode.CMSG_QUEST_LOG_REMOVE_QUEST, 0x0EDA8},
            {Opcode.CMSG_QUEST_LOG_SWAP_QUEST, 0x10068}, //UnknownopcodeID
            {Opcode.CMSG_QUEST_CONFIRM_ACCEPT, 0x06FCC},
            {Opcode.CMSG_QUEST_POI_QUERY, 0x07DE0},
            {Opcode.CMSG_QUERY_QUEST_INFO, 0x0EFE8},
            {Opcode.CMSG_READY_FOR_ACCOUNT_DATA_TIMES, 0x07DA8}, //4.0.613561
            {Opcode.CMSG_READ_ITEM, 0x0F3C0},
            {Opcode.CMSG_REALM_SPLIT, 0x060AC},
            {Opcode.CMSG_RECLAIM_CORPSE, 0x07CC8},
            {Opcode.CMSG_REFER_A_FRIEND, 0x030C4},
            {Opcode.CMSG_REFORGE_ITEM, 0x00313},
            {Opcode.CMSG_REMOVE_GLYPH, 0x101EC}, //UnknownopcodeID
            {Opcode.CMSG_REPAIR_ITEM, 0x039E4},
            {Opcode.CMSG_REPOP_REQUEST, 0x0A9E4},
            {Opcode.CMSG_CHAT_REPORT_FILTERED, 0x075A8},
            {Opcode.CMSG_CHAT_REPORT_IGNORED, 0x0A78C},
            {Opcode.CMSG_REPORT_PVP_PLAYER_AFK, 0x0E3AC},
            {Opcode.CMSG_REQUEST_ACCOUNT_DATA, 0x0EEAC},
            {Opcode.CMSG_REQUEST_GUILD_REWARDS_LIST, 0x02210},
            {Opcode.CMSG_REQUEST_GUILD_XP, 0x03509},
            //{Opcode.CMSG_REQUEST_GUILD_ROSTER, 0x0250D},
            {Opcode.CMSG_REQUEST_HONOR_STATS, 0x00E93},
            {Opcode.CMSG_REQUEST_HOTFIX, 0x08589},// the client sends this after we send SMSG_HOTFIX_NOTIFY[_BLOP]}, only sent for the items that the player has in his inventory}, that are flagged to be hot fixed
            {Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS, 0x070C8},
            {Opcode.CMSG_REQUEST_PET_INFO, 0x0EAE4},
            {Opcode.CMSG_REQUEST_PLAYER_VEHICLE_EXIT, 0x0208C},
            {Opcode.CMSG_GET_PVP_OPTIONS_ENABLED, 0x00188},
            {Opcode.CMSG_REQUEST_RAID_INFO, 0x03DE4},
            {Opcode.CMSG_REQUEST_RATED_BG_INFO, 0x08501},
            {Opcode.CMSG_REQUEST_VEHICLE_EXIT, 0x0B3CC},
            {Opcode.CMSG_REQUEST_VEHICLE_NEXT_SEAT, 0x064E4},
            {Opcode.CMSG_REQUEST_VEHICLE_PREV_SEAT, 0x02DE4},
            {Opcode.CMSG_REQUEST_VEHICLE_SWITCH_SEAT, 0x0A8CC},
            {Opcode.CMSG_RESET_FACTION_CHEAT, 0x100CD}, //UnknownopcodeID
            {Opcode.CMSG_RESET_INSTANCES, 0x0AAA0},
            {Opcode.CMSG_RESURRECT_RESPONSE, 0x0BBE8},
            {Opcode.CMSG_RUN_SCRIPT, 0x100DF}, //UnknownopcodeID
            {Opcode.CMSG_SAVE_DANCE, 0x101C3}, //UnknownopcodeID
            {Opcode.CMSG_SAVE_PLAYER, 0x1005C}, //UnknownopcodeID
            //{Opcode.CMSG_SEARCH_LFG_JOIN, 0x061CC},
            //{Opcode.CMSG_SEARCH_LFG_LEAVE, 0x0847D}, //4.0.3a13329-403
            {Opcode.CMSG_SELF_RES, 0x0AEC4},
            {Opcode.CMSG_SELL_ITEM, 0x063A4},
            {Opcode.CMSG_SEND_COMBAT_TRIGGER, 0x1015C}, //UnknownopcodeID
            {Opcode.CMSG_SEND_GENERAL_TRIGGER, 0x1015B}, //UnknownopcodeID
            {Opcode.CMSG_SEND_LOCAL_EVENT, 0x1015A}, //UnknownopcodeID
            {Opcode.CMSG_SEND_MAIL, 0x02DEC},
            {Opcode.CMSG_SERVERTIME, 0x10005}, //UnknownopcodeID
            {Opcode.CMSG_SERVER_BROADCAST, 0x100DE}, //UnknownopcodeID
            {Opcode.CMSG_SERVER_INFO_QUERY, 0x101F6}, //UnknownopcodeID
            {Opcode.CMSG_SETDEATHBINDPOINT, 0x0A94E},
            {Opcode.CMSG_SET_ACTION_BAR_TOGGLES, 0x032C4},
            {Opcode.CMSG_SET_ACTION_BUTTON, 0x072E8},
            {Opcode.CMSG_SET_ACTIVE_MOVER, 0x06CA0},
            {Opcode.CMSG_SET_ACTIVE_VOICE_CHANNEL, 0x032A4},
            {Opcode.CMSG_SET_ASSISTANT_LEADER, 0x03F84},
            //{Opcode.CMSG_SET_CHANNEL_WATCH, 0x07FE0}, //opcode32736
            {Opcode.CMSG_SET_CONTACT_NOTES, 0x07DCC},
            {Opcode.CMSG_SET_CRITERIA_CHEAT, 0x101DD}, //UnknownopcodeID
            {Opcode.CMSG_SET_DURABILITY_CHEAT, 0x100CE}, //UnknownopcodeID
            {Opcode.CMSG_SET_EXPLORATION, 0x100E1}, //UnknownopcodeID
            {Opcode.CMSG_SET_EXPLORATION_ALL, 0x10120}, //UnknownopcodeID
            {Opcode.CMSG_SET_FACTION_AT_WAR, 0x10055}, //UnknownopcodeID
            {Opcode.CMSG_SET_FACTION_CHEAT, 0x10056}, //UnknownopcodeID
            {Opcode.CMSG_SET_FACTION_INACTIVE, 0x0FE84},
            //{Opcode.CMSG_SET_FOCUSED_ACHIEVEMENT, 0x03109},
            //{Opcode.CMSG_SET_LFG_COMMENT, 0x0E1C4},
            {Opcode.CMSG_SET_GLYPH, 0x101D5}, //UnknownopcodeID
            {Opcode.CMSG_SET_GLYPH_SLOT, 0x101D4}, //UnknownopcodeID
            {Opcode.CMSG_SET_GRANTABLE_LEVELS, 0x101A1}, //UnknownopcodeID
            {Opcode.CMSG_SET_PLAYER_DECLINED_NAMES, 0x0ADC0},
            {Opcode.CMSG_SET_PRIMARY_TALENT_TREE, 0x0FEA8},
            {Opcode.CMSG_SET_PVP_RANK_CHEAT, 0x100CF}, //UnknownopcodeID
            {Opcode.CMSG_SET_PVP_TITLE, 0x100D2}, //UnknownopcodeID
            {Opcode.CMSG_SET_RUNE_COOLDOWN, 0x101CC}, //UnknownopcodeID
            {Opcode.CMSG_SET_RUNE_COUNT, 0x101CB}, //UnknownopcodeID
            {Opcode.CMSG_SET_SAVED_INSTANCE_EXTEND, 0x02588},
            {Opcode.CMSG_SET_SELECTION, 0x06488},
            {Opcode.CMSG_SET_SHEATHED, 0x0AAE4},
            {Opcode.CMSG_SET_SKILL_CHEAT, 0x10085}, //UnknownopcodeID
            {Opcode.CMSG_SET_TAXI_BENCHMARK_MODE, 0x073C4},
            {Opcode.CMSG_SET_TITLE, 0x0EC8C},
            {Opcode.CMSG_SET_TITLE_SUFFIX, 0x10196}, //UnknownopcodeID
            {Opcode.CMSG_SET_TRADE_GOLD, 0x00C13},
            {Opcode.CMSG_SET_TRADE_ITEM, 0x00A11},
            {Opcode.CMSG_SET_WATCHED_FACTION, 0x035C8},
            {Opcode.CMSG_SHOWING_CLOAK, 0x07AA4},
            {Opcode.CMSG_SHOWING_HELM, 0x0B7C0},
            {Opcode.CMSG_SKILL_BUY_RANK, 0x1009F}, //UnknownopcodeID
            {Opcode.CMSG_SKILL_BUY_STEP, 0x1009E}, //UnknownopcodeID
            {Opcode.CMSG_SOCKET_GEMS, 0x076C4},
            {Opcode.CMSG_SPELL_CLICK, 0x0F9A4},
            {Opcode.CMSG_SPIRIT_HEALER_ACTIVATE, 0x0F3AC},
            {Opcode.CMSG_SPLIT_ITEM, 0x0FDAC},
            {Opcode.CMSG_STABLE_CHANGE_SLOT, 0x00291},
            {Opcode.CMSG_STABLE_REVIVE_PET, 0x100C7}, //UnknownopcodeID
            {Opcode.CMSG_STAND_STATE_CHANGE, 0x0FC88},
            //{Opcode.CMSG_START_QUEST, 0x00613},
            {Opcode.CMSG_STOP_DANCE, 0x03080},
            {Opcode.CMSG_STORE_LOOT_IN_SLOT, 0x07FCC},
            {Opcode.CMSG_SUMMON_RESPONSE, 0x06BA0},
            {Opcode.CMSG_SUSPEND_COMMS_ACK, 0x10009}, //UnknownopcodeID
            {Opcode.CMSG_SWAP_INV_ITEM, 0x03EC4},
            {Opcode.CMSG_SWAP_ITEM, 0x0E8AC},
            {Opcode.CMSG_SYNC_DANCE, 0x101C6}, //UnknownopcodeID
            {Opcode.CMSG_TARGET_CAST, 0x10183}, //UnknownopcodeID
            {Opcode.CMSG_TARGET_SCRIPT_CAST, 0x10184}, //UnknownopcodeID
            {Opcode.CMSG_TAXICLEARALLNODES, 0x1006C}, //Neverused
            {Opcode.CMSG_TAXIENABLEALLNODES, 0x1006D}, //Neverused
            {Opcode.CMSG_TAXISHOWNODES, 0x0B8E8},
            {Opcode.CMSG_TAXI_NODE_STATUS_QUERY, 0x0A1EC},
            {Opcode.CMSG_TAXI_QUERY_AVAILABLE_NODES, 0x0BE8E},
            {Opcode.CMSG_TELEPORT_TO_UNIT, 0x0E1AC},
            {Opcode.CMSG_SEND_TEXT_EMOTE, 0x0E9E0},
            {Opcode.CMSG_TIME_SYNC_RESPONSE, 0x0A8AC},
            {Opcode.CMSG_TOGGLE_PVP, 0x06480},
            {Opcode.CMSG_TOTEM_DESTROYED, 0x034A0},
            {Opcode.CMSG_TRAINER_BUY_SPELL, 0x0FDC8},
            {Opcode.CMSG_TRAINER_LIST, 0x0E5AC},
            {Opcode.CMSG_TRANSFORM, 0x04577}, //4.0.3a13329
            {Opcode.CMSG_TRIGGER_CINEMATIC_CHEAT, 0x1003D}, //UnknownopcodeID
            {Opcode.CMSG_TURN_IN_PETITION, 0x0A584},
            {Opcode.CMSG_TUTORIAL_CLEAR, 0x0A5E4},
            {Opcode.CMSG_TUTORIAL_FLAG, 0x0E4CC},
            {Opcode.CMSG_TUTORIAL_RESET, 0x02188},
            {Opcode.CMSG_UI_TIME_REQUEST, 0x03FA8},
            {Opcode.CMSG_UNACCEPT_TRADE, 0x00811},
            {Opcode.CMSG_UNITANIMTIER_CHEAT, 0x101DE}, //UnknownopcodeID
            {Opcode.CMSG_UNKNOWN_1303, 0x06BE0},
            {Opcode.CMSG_UNKNOWN_1320, 0x1025C}, //UnknownopcodeID/Name
            {Opcode.CMSG_UNLEARN_DANCE_MOVE, 0x101CA}, //UnknownopcodeID
            {Opcode.CMSG_UNLEARN_SKILL, 0x063C8},
            {Opcode.CMSG_UNLEARN_SPELL, 0x10090}, //UnknownopcodeID
            {Opcode.CMSG_UNLEARN_TALENTS, 0x10098}, //UnknownopcodeID
            {Opcode.CMSG_UNUSED2, 0x10058}, //UnknownopcodeID
            {Opcode.CMSG_UPDATE_ACCOUNT_DATA, 0x072A4},
            {Opcode.CMSG_UPDATE_MISSILE_TRAJECTORY, 0x00E54}, //4.0.3a13329-403
            {Opcode.CMSG_UPDATE_PROJECTILE_POSITION, 0x0EF7F}, //4.0.3a13329-403
            {Opcode.CMSG_USE_ITEM, 0x07080},
            {Opcode.CMSG_VOICE_SESSION_ENABLE, 0x0FEA4},
            {Opcode.CMSG_VOICE_SET_TALKER_MUTED_REQUEST, 0x10166}, //UnknownopcodeID
            {Opcode.CMSG_WARDEN_DATA, 0x02F84},
            {Opcode.CMSG_WARGAME_ACCEPT, 0x00108},
            {Opcode.CMSG_WARGAME_REQUEST, 0x00501},
            {Opcode.CMSG_WHO, 0x0A4CC},
            {Opcode.CMSG_WHO_IS, 0x02180},
            //{Opcode.CMSG_WORLD_LOGIN, 0x08508},
            {Opcode.CMSG_WORLD_TELEPORT, 0x08100},
            {Opcode.CMSG_WRAP_ITEM, 0x07CC4},
            {Opcode.CMSG_ZONEUPDATE, 0x033E4}
        };

        private static readonly BiDictionary<Opcode, int> ServerOpcodes = new BiDictionary<Opcode, int>
        {
            {Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x07280},
            {Opcode.SMSG_ACHIEVEMENT_DELETED, 0x0E6A0},
            {Opcode.SMSG_ACHIEVEMENT_EARNED, 0x0F5E4},

            {Opcode.SMSG_ACTIVATE_TAXI_REPLY, 0x07A84},
            {Opcode.SMSG_ADDON_INFO, 0x0EA80},
            {Opcode.SMSG_ADD_RUNE_POWER, 0x0F5E8},
            {Opcode.SMSG_AI_REACTION, 0x031CC},
            {Opcode.SMSG_ALL_ACHIEVEMENT_DATA, 0x0445E},
            {Opcode.SMSG_ALL_GUILD_ACHIEVEMENTS, 0x0491E},
            {Opcode.SMSG_AREA_SPIRIT_HEALER_TIME, 0x06D80},
            {Opcode.SMSG_AREA_TRIGGER_MESSAGE, 0x0EAE0},
            {Opcode.SMSG_AREA_TRIGGER_NO_CORPSE, 0x031E4},
            {Opcode.SMSG_ARENA_ERROR, 0x02FEC},
            {Opcode.SMSG_ARENA_OPPONENT_UPDATE, 0x028CC},
            {Opcode.SMSG_ARENA_TEAM_CHANGE_FAILED_QUEUED, 0x0B2A0},
            {Opcode.SMSG_ARENA_TEAM_COMMAND_RESULT, 0x0040C},
            {Opcode.SMSG_ARENA_TEAM_EVENT, 0x074C4},
            {Opcode.SMSG_ARENA_TEAM_INVITE, 0x063A0},
            {Opcode.SMSG_ARENA_TEAM_QUERY_RESPONSE, 0x03DE8},
            {Opcode.SMSG_ARENA_TEAM_ROSTER, 0x0EC80},
            {Opcode.SMSG_ARENA_TEAM_STATS, 0x0FA80},
            //{Opcode.SMSG_ARTIFACT_COMPLETED, 0x0491C},
            {Opcode.SMSG_ATTACKER_STATE_UPDATE, 0x0BBC0},
            {Opcode.SMSG_ATTACK_START, 0x0B68C},
            {Opcode.SMSG_ATTACK_STOP, 0x06DCC},
            {Opcode.SMSG_ATTACKSWING_BADFACING, 0x067A8},
            {Opcode.SMSG_ATTACKSWING_CANT_ATTACK, 0x06188},
            {Opcode.SMSG_ATTACKSWING_DEADTARGET, 0x0A7C4},
            {Opcode.SMSG_ATTACKSWING_NOTINRANGE, 0x036C4},
            {Opcode.SMSG_AUCTION_BIDDER_NOTIFICATION, 0x0ACEC},
            {Opcode.SMSG_AUCTION_COMMAND_RESULT, 0x0AAE0},
            {Opcode.SMSG_AUCTION_LIST_BIDDER_ITEMS_RESULT, 0x0A1A8},
            {Opcode.SMSG_AUCTION_LIST_OWNER_ITEMS_RESULT, 0x03CA8},
            {Opcode.SMSG_AUCTION_LIST_PENDING_SALES, 0x0BAC4},
            {Opcode.SMSG_AUCTION_LIST_RESULT, 0x0E5A8},
            {Opcode.SMSG_AUCTION_OWNER_NOTIFICATION, 0x06C80},
            {Opcode.SMSG_AUCTION_REMOVED_NOTIFICATION, 0x0B1EC},
            {Opcode.SMSG_AURACASTLOG, 0x10080}, //UnknownopcodeID
            {Opcode.SMSG_AURA_UPDATE, 0x065C0},
            {Opcode.SMSG_AURA_UPDATE_ALL, 0x037E0},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x06019},
            {Opcode.SMSG_AUTH_RESPONSE, 0x0B28C},
            {Opcode.SMSG_AVAILABLE_VOICE_CHANNEL, 0x0F8C8},
            {Opcode.SMSG_BARBER_SHOP_RESULT, 0x03188},
            {Opcode.SMSG_BATTLEFIELD_LIST, 0x0490C},
            {Opcode.SMSG_BATTLEFIELD_MGR_EJECTED, 0x04C1C},
            {Opcode.SMSG_BATTLEFIELD_MGR_EJECT_PENDING, 0x0045E},
            {Opcode.SMSG_BATTLEFIELD_MGR_ENTERING, 0x0415C},
            {Opcode.SMSG_BATTLEFIELD_MGR_ENTRY_INVITE, 0x0455E},
            {Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_INVITE, 0x0054C},
            {Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_REQUEST_RESPONSE, 0x00C0C},
            {Opcode.SMSG_BATTLEFIELD_MGR_STATE_CHANGE, 0x0485C},
            {Opcode.SMSG_BATTLEFIELD_PORT_DENIED, 0x10059}, //UnknownopcodeID
            //{Opcode.SMSG_BATTLEFIELD_STATUS1, 0x0454C},
            //{Opcode.SMSG_BATTLEFIELD_STATUS2, 0x0051E},
            //{Opcode.SMSG_BATTLEFIELD_STATUS3, 0x0081C},
            //{Opcode.SMSG_BATTLEFIELD_STATUS4, 0x04C4C},
            {Opcode.SMSG_BATTLEFIELD_STATUS_QUEUED, 0x00D1E},
            {Opcode.SMSG_BATTLEGROUND_EXIT_QUEUE, 0x0454C},
            {Opcode.SMSG_BATTLEGROUND_INFO_THROTTLED, 0x004A6},
            {Opcode.SMSG_BATTLEGROUND_IN_PROGRESS, 0x0051E},
            {Opcode.SMSG_BATTLEGROUND_PLAYER_JOINED, 0x0494C},
            {Opcode.SMSG_BATTLEGROUND_PLAYER_LEFT, 0x00D1C},
            {Opcode.SMSG_BATTLEGROUND_PLAYER_POSITIONS, 0x0045C},
            {Opcode.SMSG_BATTLEGROUND_WAIT_JOIN, 0x0081C},
            {Opcode.SMSG_BINDER_CONFIRM, 0x033C4},
            {Opcode.SMSG_BIND_POINT_UPDATE, 0x0A9A0},
            {Opcode.SMSG_BINDZONEREPLY, 0x0ACAC},
            {Opcode.SMSG_BREAK_TARGET, 0x02488},
            {Opcode.SMSG_BUY_BANK_SLOT_RESULT, 0x0F5C0},
            //{Opcode.SMSG_BUY_FAILED, 0x06CE8},
            {Opcode.SMSG_BUY_SUCCEEDED, 0x069CC},
            {Opcode.SMSG_CALENDAR_ARENA_TEAM, 0x021E0},
            //{Opcode.SMSG_CALENDAR_CLEAR_ACTION_PENDING, 0x0265E},
            {Opcode.SMSG_CALENDAR_COMMAND_RESULT, 0x0AD80},
            {Opcode.SMSG_CALENDAR_EVENT_INVITE, 0x0A5A0},
            {Opcode.SMSG_CALENDAR_EVENT_INVITE_ALERT, 0x0AFA8},
            {Opcode.SMSG_CALENDAR_EVENT_INVITE_NOTES, 0x00460}, //UnknownopcodeID
            {Opcode.SMSG_CALENDAR_EVENT_INVITE_NOTES_ALERT, 0x00461}, //UnknownopcodeID
            {Opcode.SMSG_CALENDAR_EVENT_INVITE_REMOVED, 0x0A9E0},
            {Opcode.SMSG_CALENDAR_EVENT_INVITE_REMOVED_ALERT, 0x0FEE0},
            {Opcode.SMSG_CALENDAR_EVENT_INVITE_STATUS_ALERT, 0x0F7C8},
            {Opcode.SMSG_CALENDAR_EVENT_MODERATOR_STATUS_ALERT, 0x0ABC8},
            {Opcode.SMSG_CALENDAR_EVENT_REMOVED_ALERT, 0x06288},
            {Opcode.SMSG_CALENDAR_EVENT_STATUS, 0x07EC0},
            {Opcode.SMSG_CALENDAR_EVENT_UPDATED_ALERT, 0x029C0},
            //{Opcode.SMSG_CALENDAR_GUILD_FILTER, 0x02FC4},
            {Opcode.SMSG_CALENDAR_RAID_LOCKOUT_ADDED, 0x02AE0},
            {Opcode.SMSG_CALENDAR_RAID_LOCKOUT_REMOVED, 0x0FCE0},
            {Opcode.SMSG_CALENDAR_RAID_LOCKOUT_UPDATED, 0x00471}, //UnknownopcodeID
            {Opcode.SMSG_CALENDAR_SEND_CALENDAR, 0x0B0A4},
            {Opcode.SMSG_CALENDAR_SEND_EVENT, 0x0FAA4},
            {Opcode.SMSG_CALENDAR_SEND_NUM_PENDING, 0x0A8E0},
            {Opcode.SMSG_CALENDAR_UPDATE_INVITE_LIST, 0x071A0},
            {Opcode.SMSG_CALENDAR_UPDATE_INVITE_LIST2, 0x0EDC4},
            {Opcode.SMSG_CALENDAR_UPDATE_INVITE_LIST3, 0x0A2A0},
            {Opcode.SMSG_CAMERA_SHAKE, 0x0A2E4},
            {Opcode.SMSG_CANCEL_AUTO_REPEAT, 0x03DE0},
            {Opcode.SMSG_CANCEL_COMBAT, 0x0BAE4},
            {Opcode.SMSG_CAST_FAILED, 0x02A8C},
            {Opcode.SMSG_CHANNEL_LIST, 0x069E0},
            {Opcode.SMSG_CHANNEL_MEMBER_COUNT, 0x02CEC},
            {Opcode.SMSG_CHANNEL_NOTIFY, 0x07CAC},
            {Opcode.SMSG_CHARACTER_LOGIN_FAILED, 0x07ACC},
            {Opcode.SMSG_CHARACTER_PROFILE, 0x1012D}, //UnknownopcodeID
            {Opcode.SMSG_CHARACTER_PROFILE_REALM_CONNECTED, 0x1012E}, //UnknownopcodeID
            {Opcode.SMSG_CREATE_CHAR, 0x0F7EC},
            {Opcode.SMSG_CHAR_CUSTOMIZE, 0x02DA4},
            {Opcode.SMSG_DELETE_CHAR, 0x0BC80},
            {Opcode.SMSG_ENUM_CHARACTERS_RESULT, 0x0ECCC},
            {Opcode.SMSG_CHAR_FACTION_CHANGE_RESULT, 0x023AC},
            {Opcode.SMSG_CHARACTER_RENAME_RESULT, 0x0E0EC},
            {Opcode.SMSG_CHAT, 0x061E4},
            {Opcode.SMSG_CHAT_PLAYER_AMBIGUOUS, 0x06AE8},
            {Opcode.SMSG_CHAT_PLAYER_NOTFOUND, 0x035A0},
            {Opcode.SMSG_CHAT_RESTRICTED, 0x03EC8},
            {Opcode.SMSG_CHAT_WRONG_FACTION, 0x0BB88},
            {Opcode.SMSG_CHEAT_DUMP_ITEMS_DEBUG_ONLY_RESPONSE, 0x10160}, //UnknownopcodeID
            {Opcode.SMSG_CHEAT_DUMP_ITEMS_DEBUG_ONLY_RESPONSE_WRITE_FILE, 0x10161}, //UnknownopcodeID
            {Opcode.SMSG_CHEAT_PLAYER_LOOKUP, 0x1017F}, //UnknownopcodeID
            {Opcode.SMSG_CHECK_FOR_BOTS, 0x0FEC8},
            {Opcode.SMSG_CLEAR_COOLDOWN, 0x036CC},
            {Opcode.SMSG_CLEAR_EXTRA_AURA_INFO_OBSOLETE, 0x1016B}, //UnknownopcodeID
            {Opcode.SMSG_CLEAR_FAR_SIGHT_IMMEDIATE, 0x0BEE4},
            {Opcode.SMSG_CLEAR_TARGET, 0x0A7AC},
            {Opcode.SMSG_CACHE_VERSION, 0x02EC4},
            {Opcode.SMSG_CONTROL_UPDATE, 0x03C84},
            {Opcode.SMSG_COMBAT_LOG_MULTIPLE, 0x033A8},
            //{Opcode.SMSG_COMMENTATOR_GET_PLAYER_INFO, 0x0758C},
            {Opcode.SMSG_COMMENTATOR_MAP_INFO, 0x0A4E8},
            {Opcode.SMSG_COMMENTATOR_PLAYER_INFO, 0x0758C},
            {Opcode.SMSG_COMMENTATOR_STATE_CHANGED, 0x0E0E4},
            {Opcode.SMSG_COMPLAINT_RESULT, 0x070CC},
            //{Opcode.SMSG_COMPLETED_ARTIFACTS, 0x0044E}, // structure:uint32(count)loop:uint32(id)}, uint32(date)}, uint32(numberoftimescompleted):endloop
            {Opcode.SMSG_COMPRESSED_ACHIEVEMENT_DATA, 0x0C1B0},
            {Opcode.SMSG_COMPRESSED_MOVES, 0x06FE4},
            {Opcode.SMSG_COMPRESSED_RESPOND_INSPECT_ACHIEVEMENTS, 0x09130},
            {Opcode.SMSG_COMPRESSED_UPDATE_OBJECT, 0x0EAC0},
            {Opcode.SMSG_COMSAT_CONNECT_FAIL, 0x029A0},
            {Opcode.SMSG_COMSAT_DISCONNECT, 0x0AAC8},
            {Opcode.SMSG_COMSAT_RECONNECT_TRY, 0x0E880},
            {Opcode.SMSG_CONTACT_LIST, 0x0748C},
            {Opcode.SMSG_CONVERT_RUNE, 0x0B4AC},
            {Opcode.SMSG_COOLDOWN_CHEAT, 0x0AAA4},
            {Opcode.SMSG_COOLDOWN_EVENT, 0x0EAEC},
            {Opcode.SMSG_CORPSE_MAP_POSITION_QUERY_RESPONSE, 0x1020A}, //UnknownopcodeID
            {Opcode.SMSG_CORPSE_RECLAIM_DELAY, 0x031C4},
            {Opcode.SMSG_QUERY_CREATURE_RESPONSE, 0x0E6AC},
            {Opcode.SMSG_CRITERIA_DELETED, 0x0AF84},
            {Opcode.SMSG_CRITERIA_UPDATE, 0x0AFC8},
            {Opcode.SMSG_CROSSED_INEBRIATION_THRESHOLD, 0x0A580},
            {Opcode.SMSG_DAMAGE_CALC_LOG, 0x0FD84},
            {Opcode.SMSG_DAMAGE_DONE_OBSOLETE, 0x1005A}, //UnknownopcodeID
            {Opcode.SMSG_DANCE_QUERY_RESPONSE, 0x031A8},
            {Opcode.SMSG_DB_REPLY, 0x00C4E},
            {Opcode.SMSG_DEATH_RELEASE_LOC, 0x033C8},
            {Opcode.SMSG_DEBUGAURAPROC, 0x100B6}, //UnknownopcodeID
            {Opcode.SMSG_DEBUG_LIST_TARGETS, 0x10188}, //UnknownopcodeID
            {Opcode.SMSG_DEFENSE_MESSAGE, 0x065EC},
            {Opcode.SMSG_DESTROY_OBJECT, 0x02AA0},
            {Opcode.SMSG_DESTRUCTIBLE_BUILDING_DAMAGE, 0x0A0E0},
            {Opcode.SMSG_DISMOUNT, 0x03CC4},
            {Opcode.SMSG_DISMOUNT_RESULT, 0x0F9C0},
            {Opcode.SMSG_DISPEL_FAILED, 0x0AAE8},
            {Opcode.SMSG_DUEL_COMPLETE, 0x0FCE8},
            {Opcode.SMSG_DUEL_COUNTDOWN, 0x0E8E0},
            {Opcode.SMSG_DUEL_IN_BOUNDS, 0x0ECA4},
            {Opcode.SMSG_DUEL_OUT_OF_BOUNDS, 0x068C0},
            {Opcode.SMSG_DUEL_REQUESTED, 0x03FC8},
            {Opcode.SMSG_DUEL_WINNER, 0x079E4},
            {Opcode.SMSG_DUMP_OBJECTS_DATA, 0x101EE}, //UnknownopcodeID
            {Opcode.SMSG_DURABILITY_DAMAGE_DEATH, 0x0FDA0},
            {Opcode.SMSG_DYNAMIC_DROP_ROLL_RESULT, 0x101D7}, //UnknownopcodeID
            {Opcode.SMSG_ECHO_PARTY_SQUELCH, 0x02AC4},
            {Opcode.SMSG_EMOTE, 0x0EEA0},
            {Opcode.SMSG_ENABLE_BARBER_SHOP, 0x037E8},
            {Opcode.SMSG_ENCHANTMENT_LOG, 0x0F5AC},
            {Opcode.SMSG_ENVIRONMENTAL_DAMAGE_LOG, 0x0E1C8},
            {Opcode.SMSG_EQUIPMENT_SET_ID, 0x0B0C0},
            {Opcode.SMSG_EXPECTED_SPAM_RECORDS, 0x06084},
            {Opcode.SMSG_EXPLORATION_EXPERIENCE, 0x0A9C4},
            {Opcode.SMSG_FEATURE_SYSTEM_STATUS, 0x03DAC},
            {Opcode.SMSG_FEIGN_DEATH_RESISTED, 0x03BE8},
            {Opcode.SMSG_FISH_ESCAPED, 0x0F080},
            {Opcode.SMSG_FISH_NOT_HOOKED, 0x039EC},
            {Opcode.SMSG_FLIGHT_SPLINE_SYNC, 0x0BFAC},
            {Opcode.SMSG_FORCEACTIONSHOW, 0x0AC84},
            {Opcode.SMSG_FORCED_DEATH_UPDATE, 0x0FFE8},
            {Opcode.SMSG_FORCE_DISPLAY_UPDATE, 0x06788},
            {Opcode.SMSG_FORCE_FLIGHT_BACK_SPEED_CHANGE, 0x0F1E4},
            {Opcode.SMSG_FORCE_FLIGHT_SPEED_CHANGE, 0x0E5CC},
            {Opcode.SMSG_FORCE_MOVE_ROOT, 0x02F88},
            {Opcode.SMSG_FORCE_MOVE_UNROOT, 0x030A0},
            {Opcode.SMSG_FORCE_PITCH_RATE_CHANGE, 0x0BF8C},
            //{Opcode.SMSG_FORCE_RUN_BACK_SPEED_CHANGE, 0x068E8},
            {Opcode.SMSG_FORCE_RUN_SPEED_CHANGE, 0x0F1CC},
            {Opcode.SMSG_FORCE_SWIM_BACK_SPEED_CHANGE, 0x0AE88},
            {Opcode.SMSG_FORCE_SWIM_SPEED_CHANGE, 0x0F5A0},
            {Opcode.SMSG_FORCE_TURN_RATE_CHANGE, 0x0708C},
            {Opcode.SMSG_FORCE_WALK_SPEED_CHANGE, 0x068E8},
            {Opcode.SMSG_FRIEND_STATUS, 0x0F68C},
            {Opcode.SMSG_GAME_OBJECT_CUSTOM_ANIM, 0x02E8C}, //(0x8230)(0x00B3)//4.0.6a13623
            {Opcode.SMSG_GAMEOBJECT_DESPAWN_ANIM, 0x0BFA8},
            {Opcode.SMSG_QUERY_GAME_OBJECT_RESPONSE, 0x0F4E8},
            {Opcode.SMSG_GAME_OBJECT_RESET_STATE, 0x022E0},
            {Opcode.SMSG_GAME_SPEED_SET, 0x03EC0},
            {Opcode.SMSG_GAMETIMEBIAS_SET, 0x1011A}, //UnknownopcodeID
            {Opcode.SMSG_GAME_TIME_SET, 0x07888},
            {Opcode.SMSG_GAME_TIME_UPDATE, 0x0F1EC},
            {Opcode.SMSG_GHOSTEE_GONE, 0x10123}, //UnknownopcodeID
            {Opcode.SMSG_GMRESPONSE_DB_ERROR, 0x0E0A0},
            {Opcode.SMSG_GMRESPONSE_RECEIVED, 0x033AC},
            {Opcode.SMSG_GMRESPONSE_STATUS_UPDATE, 0x00101},
            {Opcode.SMSG_GM_TICKET_CREATE, 0x0A8A0},
            {Opcode.SMSG_GM_TICKET_DELETE_TICKET, 0x0F48C},
            {Opcode.SMSG_GM_TICKET_GET_TICKET, 0x02284},
            {Opcode.SMSG_GM_TICKET_GET_SYSTEM_STATUS, 0x0B9C0},
            {Opcode.SMSG_GM_TICKET_UPDATE_TEXT, 0x0A5E8},
            {Opcode.SMSG_GM_MESSAGECHAT, 0x03AEC},
            {Opcode.SMSG_GM_TICKET_STATUS_UPDATE, 0x072C4},
            {Opcode.SMSG_GOD_MODE, 0x023EC},
            {Opcode.SMSG_GOGOGO_OBSOLETE, 0x10195}, //UnknownopcodeID
            {Opcode.SMSG_GOSSIP_COMPLETE, 0x0F0AC},
            {Opcode.SMSG_GOSSIP_MESSAGE, 0x0BBC8},
            {Opcode.SMSG_GOSSIP_POI, 0x0B9AC},
            {Opcode.SMSG_GROUP_ACTION_THROTTLED, 0x0F4E4},
            {Opcode.SMSG_GROUP_CANCEL, 0x06AAC},
            {Opcode.SMSG_GROUP_DECLINE, 0x0ABAC},
            {Opcode.SMSG_GROUP_DESTROYED, 0x022CC},
            {Opcode.SMSG_GROUP_INVITE, 0x0A8A8},
            {Opcode.SMSG_GROUP_LIST, 0x06D8C},
            {Opcode.SMSG_GROUP_SET_LEADER, 0x0E88C},
            {Opcode.SMSG_GROUP_UNINVITE, 0x03ACC},
            {Opcode.SMSG_GUILD_ACHIEVEMENT_EARNED, 0x00D5C},
            {Opcode.SMSG_GUILD_ACHIEVEMENT_MEMBERS, 0x0414E},
            {Opcode.SMSG_GUILD_BANK_QUERY_RESULTS, 0x0A6A8},
            {Opcode.SMSG_GUILD_COMMAND_RESULT, 0x023C0},
            {Opcode.SMSG_GUILD_CRITERIA_DATA, 0x0400E},
            {Opcode.SMSG_GUILD_CRITERIA_DELETED, 0x0000C},
            {Opcode.SMSG_GUILD_DECLINE, 0x0B78C},
            {Opcode.SMSG_GUILD_EVENT, 0x0B7C4},
            {Opcode.SMSG_GUILD_INFO, 0x020A8},
            {Opcode.SMSG_GUILD_INVITE, 0x0010C},
            {Opcode.SMSG_GUILD_MAX_DAILY_XP, 0x0441C},
            {Opcode.SMSG_GUILD_NEWS_UPDATE, 0x0485E},
            //{Opcode.SMSG_GUILD_PARTY_STATE_UPDATE, 0x0450C},
            {Opcode.SMSG_QUERY_GUILD_INFO_RESPONSE, 0x03F80},
            {Opcode.SMSG_GUILD_RANKS, 0x0411E},
            {Opcode.SMSG_GUILD_REWARD_LIST, 0x00C4C},
            {Opcode.SMSG_GUILD_ROSTER, 0x04D5C},
            {Opcode.SMSG_GUILD_ROSTER_UPDATE, 0x0085E},
            {Opcode.SMSG_GUILD_SEND_RANK_CHANGE, 0x0004C},
            {Opcode.SMSG_GUILD_TRADESKILL_UPDATE, 0x0454E},
            //{Opcode.SMSG_GUILD_UPDATE_PARTY_STATE, 0x0450C},
            //{Opcode.SMSG_GUILD_XP_LIMIT, 0x0441C},
            //{Opcode.SMSG_GUILD_XP_UPDATE, 0x0440E},
            {Opcode.SMSG_GUILD_XP, 0x0440E},
            {Opcode.SMSG_HEALTH_UPDATE, 0x02AA8},
            {Opcode.SMSG_HIGHEST_THREAT_UPDATE, 0x03F7E}, //4.0.3a13329-403
            //{Opcode.SMSG_HOTFIX_NOTIFY_BLOP, 0x04C1E}, //sent after SMSG_AUTH_RESPONSE
            {Opcode.SMSG_HOTFIX_NOTIFY, 0x04C0E}, //can be sent while in game
            {Opcode.SMSG_IGNORE_DIMINISHING_RETURNS_CHEAT, 0x0747E}, //4.0.3a13329-403
            {Opcode.SMSG_IGNORE_REQUIREMENTS_CHEAT, 0x07C84},
            {Opcode.SMSG_INITIALIZE_FACTIONS, 0x025C0},
            {Opcode.SMSG_SEND_KNOWN_SPELLS, 0x06584},
            {Opcode.SMSG_INIT_EXTRA_AURA_INFO_OBSOLETE, 0x10168}, //UnknownopcodeID
            {Opcode.SMSG_INIT_WORLD_STATES, 0x0F6E8},
            //{Opcode.SMSG_INSPECT, 0x07AC8},
            {Opcode.SMSG_INSPECT_HONOR_STATS, 0x0005E},
            {Opcode.SMSG_INSPECT_TALENT, 0x0F8C0},
            {Opcode.SMSG_INSTANCE_DIFFICULTY, 0x0FFE4},
            {Opcode.SMSG_INSTANCE_LOCK_WARNING_QUERY, 0x07488},
            {Opcode.SMSG_INSTANCE_RESET, 0x030E8},
            {Opcode.SMSG_INSTANCE_RESET_FAILED, 0x03BA0},
            {Opcode.SMSG_INSTANCE_SAVE_CREATED, 0x0BBC4},
            {Opcode.SMSG_INVALIDATE_DANCE, 0x0BEE8},
            {Opcode.SMSG_INVALIDATE_PLAYER, 0x0A884},
            {Opcode.SMSG_INVALID_PROMOTION_CODE, 0x069AC},
            {Opcode.SMSG_INVENTORY_CHANGE_FAILURE, 0x0AFCC},
            {Opcode.SMSG_ITEM_COOLDOWN, 0x06CC8},
            {Opcode.SMSG_ITEM_ENCHANT_TIME_UPDATE, 0x066A0},
            //{Opcode.SMSG_ITEM_NAME_QUERY_RESPONSE, 0x07BC8},
            {Opcode.SMSG_ITEM_PUSH_RESULT, 0x0FBE8},
            {Opcode.SMSG_ITEM_REFUND_INFO_RESPONSE, 0x0095E},
            {Opcode.SMSG_ITEM_PURCHASE_REFUND_RESULT, 0x0441E},
            {Opcode.SMSG_QUERY_ITEM_TEXT_RESPONSE, 0x077C8},
            {Opcode.SMSG_ITEM_TIME_UPDATE, 0x066A4},
            {Opcode.SMSG_JOINED_BATTLEGROUND_QUEUE, 0x0090E},
            {Opcode.SMSG_KICK_REASON, 0x0EAC8},
            {Opcode.SMSG_LEARNED_DANCE_MOVES, 0x06F80},
            {Opcode.SMSG_LEARNED_SPELL, 0x076E8},
            {Opcode.SMSG_LEVEL_UP_INFO, 0x0B9C4},
            {Opcode.SMSG_LFG_BOOT_PROPOSAL_UPDATE, 0x079AC},
            {Opcode.SMSG_LFG_DISABLED, 0x0F880},
            {Opcode.SMSG_LFG_JOIN_RESULT, 0x0338C},
            {Opcode.SMSG_LFG_OFFER_CONTINUE, 0x063EC},
            //{Opcode.SMSG_LFG_OPEN_FROM_GOSSIP, 0x071EC},
            {Opcode.SMSG_LFG_PARTY_INFO, 0x060A0},
            {Opcode.SMSG_LFG_PLAYER_INFO, 0x0E088},
            {Opcode.SMSG_LFG_PLAYER_REWARD, 0x02C88},
            {Opcode.SMSG_LFG_PROPOSAL_UPDATE, 0x032AC},
            {Opcode.SMSG_LFG_QUEUE_STATUS, 0x0B388},
            {Opcode.SMSG_LFG_ROLE_CHECK_UPDATE, 0x0F2A8},
            {Opcode.SMSG_LFG_ROLE_CHOSEN, 0x0A9AC},
            {Opcode.SMSG_LFG_TELEPORT_DENIED, 0x0EAA0},
            {Opcode.SMSG_LFG_UPDATE_LIST, 0x03880},
            {Opcode.SMSG_LFG_UPDATE_PARTY, 0x02CE8},
            {Opcode.SMSG_LFG_UPDATE_PLAYER, 0x0E284},
            {Opcode.SMSG_VENDOR_INVENTORY, 0x0F8AC},
            {Opcode.SMSG_LOAD_EQUIPMENT_SET, 0x0F1A8},
            {Opcode.SMSG_LOGIN_SET_TIME_SPEED, 0x039AC},
            {Opcode.SMSG_LOGIN_VERIFY_WORLD, 0x028C0},
            {Opcode.SMSG_LOGOUT_CANCEL_ACK, 0x0EE88},
            {Opcode.SMSG_LOGOUT_COMPLETE, 0x0A0A4},
            {Opcode.SMSG_LOGOUT_RESPONSE, 0x0F788},
            {Opcode.SMSG_LOG_XP_GAIN, 0x0B880},
            {Opcode.SMSG_LOOT_ALL_PASSED, 0x06AC4},
            {Opcode.SMSG_LOOT_CLEAR_MONEY, 0x03480},
            {Opcode.SMSG_LOOT_ITEM_NOTIFY, 0x0AECC},
            {Opcode.SMSG_LOOT_LIST, 0x0F684},
            {Opcode.SMSG_LOOT_MASTER_LIST, 0x0ECC4},
            {Opcode.SMSG_LOOT_MONEY_NOTIFY, 0x067C0},
            {Opcode.SMSG_LOOT_RELEASE, 0x023C8},
            {Opcode.SMSG_LOOT_REMOVED, 0x06F88},
            {Opcode.SMSG_LOOT_RESPONSE, 0x0F38C},
            {Opcode.SMSG_LOOT_ROLL, 0x066A8},
            {Opcode.SMSG_LOOT_ROLL_WON, 0x06280},
            {Opcode.SMSG_LOOT_SLOT_CHANGED, 0x031EC}, //0x4D4C?
            {Opcode.SMSG_LOOT_START_ROLL, 0x0EB84},
            {Opcode.SMSG_LOTTERY_QUERY_RESULT_OBSOLETE, 0x1012A}, //UnknownopcodeID
            {Opcode.SMSG_LOTTERY_RESULT_OBSOLETE, 0x1012C}, //UnknownopcodeID
            {Opcode.SMSG_MAIL_LIST_RESULT, 0x0F1C8},
            {Opcode.SMSG_MEETINGSTONE_IN_PROGRESS, 0x0E7C4},
            {Opcode.SMSG_MEETINGSTONE_MEMBER_ADDED, 0x0B2A8},
            {Opcode.SMSG_MEETINGSTONE_SETQUEUE, 0x0ED88}, //(almost100%sureit'swrong)
            {Opcode.SMSG_MINIGAME_MOVE_FAILED, 0x10109}, //UnknownopcodeID
            {Opcode.SMSG_MINIGAME_SETUP, 0x026A4},
            {Opcode.SMSG_MINIGAME_STATE, 0x0A5A8},
            {Opcode.SMSG_MIRROR_IMAGE_COMPONENTED_DATA, 0x0E2A4},
            {Opcode.SMSG_MODIFY_COOLDOWN, 0x030CC},
            {Opcode.SMSG_MONSTER_MOVE_TRANSPORT, 0x0248C},
            {Opcode.SMSG_MOTD, 0x077C0},
            {Opcode.SMSG_MOUNT_RESULT, 0x02AEC},
            {Opcode.SMSG_MOUNT_SPECIAL_ANIM, 0x02388},
            {Opcode.SMSG_MOVE_ABANDON_TRANSPORT, 0x101D0}, //UnknownopcodeID
            {Opcode.SMSG_MOVE_DISABLE_GRAVITY, 0x10219}, //UnknownopcodeID
            {Opcode.SMSG_MOVE_ENABLE_GRAVITY, 0x1021B}, //UnknownopcodeID
            {Opcode.SMSG_MOVE_KNOCK_BACK, 0x0B180},
            {Opcode.SMSG_MOVE_LEVITATING, 0x0B8AC},
            //{Opcode.SMSG_MOVE_SET_CAN_FLY, 0x0BDA0},
            {Opcode.SMSG_MOVE_SET_FEATHER_FALL, 0x06088},
            {Opcode.SMSG_MOVE_SET_FLIGHT, 0x0C57F}, //4.0.3a13329-403
            {Opcode.SMSG_MOVE_SET_HOVERING, 0x06AE0},
            //{Opcode.SMSG_MOVE_SET_LAND_WALK, 0x02084},
            {Opcode.SMSG_MOVE_SET_NORMAL_FALL, 0x06CE0},
            {Opcode.SMSG_MOVE_SET_WALK_IN_AIR, 0x0A980},
            {Opcode.SMSG_MOVE_SET_WATER_WALK, 0x02E84},
            {Opcode.SMSG_MOVE_SPLINE_DISABLE_GRAVITY, 0x1021E}, //UnknownopcodeID
            {Opcode.SMSG_MOVE_SPLINE_ENABLE_GRAVITY, 0x1022F},
            {Opcode.SMSG_MOVE_SPLINE_ROOT, 0x01E55},
            {Opcode.SMSG_MOVE_SPLINE_SET_FEATHER_FALL, 0x1010C}, //UnknownopcodeID
            //{Opcode.SMSG_MOVE_SPLINE_SET_FLIGHT_BACK_SPEED, 0x0F2EC},
            //{Opcode.SMSG_MOVE_SPLINE_SET_FLIGHT_SPEED, 0x0E0C0},
            {Opcode.SMSG_MOVE_SPLINE_SET_FLYING, 0x05D54}, //4.0.3a13329-403
            {Opcode.SMSG_MOVE_SPLINE_SET_HOVER, 0x1010E}, //UnknownopcodeID
            {Opcode.SMSG_MOVE_SPLINE_SET_LAND_WALK, 0x0A7C8},
            {Opcode.SMSG_MOVE_SPLINE_SET_NORMAL_FALL, 0x1010D}, //UnknownopcodeID
            //{Opcode.SMSG_MOVE_SPLINE_SET_PITCH_RATE, 0x070C4},
            //{Opcode.SMSG_MOVE_SPLINE_SET_RUN_BACK_SPEED, 0x0E9C0},
            {Opcode.SMSG_MOVE_SPLINE_SET_RUN_MODE, 0x10114}, //UnknownopcodeID
            //{Opcode.SMSG_MOVE_SPLINE_SET_RUN_SPEED, 0x0F9E8},
            //{Opcode.SMSG_MOVE_SPLINE_SET_SWIM_BACK_SPEED, 0x021C8},
            //{Opcode.SMSG_MOVE_SPLINE_SET_SWIM_SPEED, 0x0B2C0},
            //{Opcode.SMSG_MOVE_SPLINE_SET_TURN_RATE, 0x07EAC},
            {Opcode.SMSG_MOVE_SPLINE_SET_WALK_MODE, 0x10115}, //UnknownopcodeID
            //{Opcode.SMSG_MOVE_SPLINE_SET_WALK_SPEED, 0x03EA8},
            {Opcode.SMSG_MOVE_SPLINE_SET_WATER_WALK, 0x061C0},
            {Opcode.SMSG_MOVE_SPLINE_START_SWIM, 0x10112}, //UnknownopcodeID
            {Opcode.SMSG_MOVE_SPLINE_STOP_SWIM, 0x10113}, //UnknownopcodeID
            {Opcode.SMSG_MOVE_SPLINE_UNROOT, 0x1010B}, //UnknownopcodeID
            {Opcode.SMSG_MOVE_SPLINE_UNSET_FLYING, 0x0257D},
            {Opcode.SMSG_MOVE_SPLINE_UNSET_HOVER, 0x1010F}, //UnknownopcodeID
            {Opcode.SMSG_MOVE_UNSET_CAN_FLY, 0x03084},
            {Opcode.SMSG_MOVE_UNSET_FLIGHT, 0x0BDA0}, //Notsure.
            {Opcode.SMSG_MOVE_UNSET_HOVERING, 0x03FE0},
            {Opcode.SMSG_MOVE_UNSET_WALK_IN_AIR, 0x07784},
            {Opcode.SMSG_MULTIPLE_PACKETS, 0x0FEC0},
            {Opcode.SMSG_QUERY_PLAYER_NAME_RESPONSE, 0x07BC8},
            {Opcode.SMSG_NEW_TAXI_PATH, 0x0E5E4},
            {Opcode.SMSG_NEW_WORLD, 0x0451E},
            {Opcode.SMSG_NOTIFICATION, 0x0BC88},
            {Opcode.SMSG_NOTIFY_DANCE, 0x03488},
            {Opcode.SMSG_NOTIFY_DEST_LOC_SPELL_CAST, 0x03588},
            {Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE, 0x0B2AC},
            {Opcode.SMSG_NPC_WONT_TALK, 0x10061}, //UnknownopcodeID
            {Opcode.SMSG_OFFER_PETITION_ERROR, 0x07DC0},
            {Opcode.SMSG_ON_CANCEL_EXPECTED_RIDE_VEHICLE_AURA, 0x03380},
            {Opcode.SMSG_ON_MONSTER_MOVE, 0x0F1A4},
            {Opcode.SMSG_OPEN_CONTAINER, 0x03FC4},
            {Opcode.SMSG_OVERRIDE_LIGHT, 0x00756}, //UnknownopcodeID
            {Opcode.SMSG_PAGE_TEXT, 0x0E5C8},
            {Opcode.SMSG_QUERY_PAGE_TEXT_RESPONSE, 0x0B084},
            {Opcode.SMSG_PARTY_COMMAND_RESULT, 0x026E0},
            {Opcode.SMSG_PARTY_KILL_LOG, 0x0AB84},
            {Opcode.SMSG_PARTY_MEMBER_STATS, 0x029AC},
            {Opcode.SMSG_PARTY_MEMBER_STATS_FULL, 0x0BA8C},
            {Opcode.SMSG_PAUSE_MIRROR_TIMER, 0x021EC},
            {Opcode.SMSG_SPELL_PERIODIC_AURA_LOG, 0x03388},
            {Opcode.SMSG_PETGODMODE, 0x0F2CC},
            {Opcode.SMSG_PETITION_QUERY_RESPONSE, 0x0FB80},
            {Opcode.SMSG_PETITION_SHOW_LIST, 0x0FCC0},
            {Opcode.SMSG_PETITION_SHOW_SIGNATURES, 0x0E2E0},
            {Opcode.SMSG_PETITION_SIGN_RESULTS, 0x0A1A4},
            {Opcode.SMSG_PET_ACTION_FEEDBACK, 0x02584},
            {Opcode.SMSG_PET_ACTION_SOUND, 0x030E4},
            {Opcode.SMSG_PET_BROKEN, 0x075E0},
            {Opcode.SMSG_PET_CAST_FAILED, 0x0A9CC},
            {Opcode.SMSG_PET_DISMISS_SOUND, 0x0B7E0},
            {Opcode.SMSG_PET_GUIDS, 0x0E4E8},
            {Opcode.SMSG_PET_LEARNED_SPELLS, 0x0B3C4},
            {Opcode.SMSG_PET_MODE, 0x079C0},
            {Opcode.SMSG_PET_NAME_INVALID, 0x01457}, //4.0.3a13329-403
            {Opcode.SMSG_QUERY_PET_NAME_RESPONSE, 0x068AC},
            {Opcode.SMSG_PET_UNLEARNED_SPELLS, 0x0F28C},
            {Opcode.SMSG_PET_RENAMEABLE, 0x0B6C8},
            {Opcode.SMSG_PET_SPELLS_MESSAGE, 0x0B780},
            {Opcode.SMSG_PET_TAME_FAILURE, 0x0FDA8},
            {Opcode.SMSG_PET_UNLEARN_CONFIRM, 0x10106}, //UnknownopcodeID
            {Opcode.SMSG_PET_UPDATE_COMBO_POINTS, 0x07588},
            {Opcode.SMSG_PLAYED_TIME, 0x0E4C8},
            {Opcode.SMSG_PLAYERBINDERROR, 0x0EEC8},
            {Opcode.SMSG_PLAYER_BOUND, 0x06BCC},
            {Opcode.SMSG_PLAYER_DIFFICULTY_CHANGE, 0x02A80},
            {Opcode.SMSG_PLAYER_SKINNED, 0x0BAA0},
            {Opcode.SMSG_PLAYER_VEHICLE_DATA, 0x0A5A4},
            {Opcode.SMSG_PLAY_DANCE, 0x0B4EC},
            {Opcode.SMSG_PLAY_MUSIC, 0x09C7F}, //UnknownopcodeID
            {Opcode.SMSG_PLAY_OBJECT_SOUND, 0x07DA0},
            {Opcode.SMSG_PLAY_SOUND, 0x02EA8},
            {Opcode.SMSG_PLAY_SPELL_IMPACT, 0x0A3E4},
            {Opcode.SMSG_PLAY_SPELL_VISUAL, 0x0FF8C},
            {Opcode.SMSG_PLAY_TIME_WARNING, 0x032EC},
            {Opcode.SMSG_PONG, 0x0A01B},
            {Opcode.SMSG_POWER_UPDATE, 0x065E8},
            {Opcode.SMSG_PRE_RESSURECT, 0x066C4},
            {Opcode.SMSG_PROC_RESIST, 0x07E80},
            {Opcode.SMSG_PROPOSE_LEVEL_GRANT, 0x0E0C4},
            {Opcode.SMSG_PUREMOUNT_CANCELLED_OBSOLETE, 0x0054E},
            {Opcode.SMSG_PVP_CREDIT, 0x037C8},
            //{Opcode.SMSG_PVP_RATED_STATS_UPDATE, 0x0015E},
            {Opcode.SMSG_QUERY_QUESTS_COMPLETED_RESPONSE, 0x0F1E8},
            {Opcode.SMSG_QUERY_TIME_RESPONSE, 0x0F1AC},
            {Opcode.SMSG_QUEST_GIVER_OFFER_REWARD_MESSAGE, 0x03FCC},
            {Opcode.SMSG_QUEST_GIVER_QUEST_COMPLETE, 0x0050E},
            {Opcode.SMSG_QUEST_GIVER_QUEST_DETAILS, 0x068A0},
            {Opcode.SMSG_QUEST_GIVER_QUEST_FAILED, 0x0A084},
            //{Opcode.SMSG_QUEST_GIVER_INVALID_QUEST, 0x02B88},
            {Opcode.SMSG_QUEST_GIVER_QUEST_LIST_MESSAGE, 0x02B88},
            {Opcode.SMSG_QUEST_GIVER_REQUEST_ITEMS, 0x06EE0},
            {Opcode.SMSG_QUEST_GIVER_STATUS, 0x07988},
            //{Opcode.SMSG_QUEST_GIVER_STATUS_MULTIPLE, 0x0F5C0},
            {Opcode.SMSG_QUEST_CONFIRM_ACCEPT, 0x07C8C},
            //{Opcode.SMSG_QUEST_FORCE_REMOVED, 0x034E8},
            //{Opcode.SMSG_QUEST_LOG_FULL, 0x061EC},
            {Opcode.SMSG_QUEST_POI_QUERY_RESPONSE, 0x06AEC},
            {Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE, 0x07BA0},
            {Opcode.SMSG_QUEST_UPDATE_ADD_ITEM, 0x1006B},
            {Opcode.SMSG_QUEST_UPDATE_ADD_KILL, 0x0ADA0},
            {Opcode.SMSG_QUEST_UPDATE_ADD_PVP_CREDIT, 0x078E0},
            {Opcode.SMSG_QUEST_UPDATE_COMPLETE, 0x0EDA0},
            {Opcode.SMSG_QUEST_UPDATE_FAILED, 0x0E588},
            {Opcode.SMSG_QUEST_UPDATE_FAILED_TIMER, 0x0F7CC},
            {Opcode.SMSG_RAID_GROUP_ONLY, 0x00554}, //4.0.3a13329
            {Opcode.SMSG_RAID_INSTANCE_INFO, 0x0A18C},
            {Opcode.SMSG_RAID_INSTANCE_MESSAGE, 0x06680},
            //{Opcode.SMSG_RAID_ROLE_CHECK_UPDATE, 0x0484C},
            {Opcode.SMSG_READY_CHECK_ERROR, 0x0547E}, //4.0.3a13329-403
            //{Opcode.SMSG_READY_CHECK_THROTTLED, 0x0EEC4},
            {Opcode.SMSG_READ_ITEM_RESULT_FAILED, 0x0A4EC},
            {Opcode.SMSG_READ_ITEM_RESULT_OK, 0x0A988},
            {Opcode.SMSG_REALM_SPLIT, 0x025EC},
            {Opcode.SMSG_REAL_GROUP_UPDATE, 0x031C0},
            {Opcode.SMSG_RECEIVED_MAIL, 0x075E4},
            {Opcode.SMSG_CONNECT_TO, 0x0201B},
            {Opcode.SMSG_REFER_A_FRIEND_FAILURE, 0x07F7C},
            //{Opcode.SMSG_REFORGE_OPEN_FROM_GOSSIP, 0x00C5C},
            {Opcode.SMSG_REPORT_PVP_AFK_RESULT, 0x078E8},
            //{Opcode.SMSG_REQUEST_WAR_GAME_RESPONSE, 0x00C1C},
            {Opcode.SMSG_RESET_FAILED_NOTIFY, 0x0BD5C}, //4.0.3a13329-403
            {Opcode.SMSG_RESISTLOG, 0x10084}, //UnknownopcodeID
            {Opcode.SMSG_RESPOND_INSPECT_ACHIEVEMENTS, 0x0041C},
            {Opcode.SMSG_RESUME_COMMS, 0x02880},
            {Opcode.SMSG_RESURRECT_FAILED, 0x0D557}, //4.0.3a13329-403
            {Opcode.SMSG_RESURRECT_REQUEST, 0x0F3A0},
            {Opcode.SMSG_RESYNC_RUNES, 0x0F8E4},
            {Opcode.SMSG_RWHOIS, 0x071C8},
            {Opcode.SMSG_SCRIPT_MESSAGE, 0x100E0}, //UnknownopcodeID
            {Opcode.SMSG_SELL_ITEM, 0x06CE8},
            //{Opcode.SMSG_SEND_ERROR_MESSAGE, 0x0480C}, //this has its own enum u8+u32+u32special
            {Opcode.SMSG_MAIL_COMMAND_RESULT, 0x0E5C0},
            //{Opcode.SMSG_SEND_QUEUED_PACKETS, 0x0E280},
            {Opcode.SMSG_SEND_UNLEARN_SPELLS, 0x0BDE8},
            {Opcode.SMSG_SERVERTIME, 0x023A4},
            {Opcode.SMSG_SERVER_BUCK_DATA, 0x101AE}, //UnknownopcodeID
            {Opcode.SMSG_SERVER_BUCK_DATA_START, 0x101F9}, //UnknownopcodeID
            {Opcode.SMSG_SERVER_FIRST_ACHIEVEMENT, 0x068CC},
            {Opcode.SMSG_SERVER_INFO_RESPONSE, 0x101F7}, //UnknownopcodeID
            {Opcode.SMSG_CHAT_SERVER_MESSAGE, 0x078C0},
            //{Opcode.SMSG_SERVER_MESSAGE_BOX, 0x0080C}, //Servermsgbox-like4.0.6a13623
            {Opcode.SMSG_SETUP_CURRENCY, 0x0091C},
            {Opcode.SMSG_SET_EXTRA_AURA_INFO_NEED_UPDATE_OBSOLETE, 0x1016A}, //UnknownopcodeID
            {Opcode.SMSG_SET_EXTRA_AURA_INFO_OBSOLETE, 0x10169}, //UnknownopcodeID
            {Opcode.SMSG_SET_FACTION_AT_WAR, 0x0EEEC},
            {Opcode.SMSG_SET_FACTION_STANDING, 0x0718C},
            {Opcode.SMSG_SET_FACTION_VISIBLE, 0x03988},
            {Opcode.SMSG_SET_FLAT_SPELL_MODIFIER, 0x02BC8},
            {Opcode.SMSG_SET_FORCED_REACTIONS, 0x0FFA0},
            {Opcode.SMSG_SET_PCT_SPELL_MODIFIER, 0x0A6E8},
            {Opcode.SMSG_PHASE_SHIFT_CHANGE, 0x0044C},
            {Opcode.SMSG_SET_PLAYER_DECLINED_NAMES_RESULT, 0x0BAAC},
            {Opcode.SMSG_SET_PROFICIENCY, 0x0BBA8},
            {Opcode.SMSG_SET_PROJECTILE_POSITION, 0x02C84},
            {Opcode.SMSG_SHOW_BANK, 0x027A4},
            {Opcode.SMSG_SHOW_MAILBOX, 0x0F680},
            {Opcode.SMSG_SHOW_TAXI_NODES, 0x02B84},
            {Opcode.SMSG_SOCKET_GEMS, 0x020E8},
            {Opcode.SMSG_SPELL_BREAK_LOG, 0x0BDAC},
            {Opcode.SMSG_SPELL_CHANCE_PROC_LOG, 0x1016E}, //UnknownopcodeID
            {Opcode.SMSG_SPELL_CHANCE_RESIST_PUSHBACK, 0x1019C}, //UnknownopcodeID
            {Opcode.SMSG_SPELL_COOLDOWN, 0x0F3E8},
            {Opcode.SMSG_SPELL_DAMAGE_SHIELD, 0x073A0},
            {Opcode.SMSG_SPELL_DELAYED, 0x0A3E8},
            {Opcode.SMSG_SPELL_DISPELL_LOG, 0xA9C8},
            {Opcode.SMSG_SPELL_ENERGIZE_LOG, 0x0F0EC},
            {Opcode.SMSG_SPELL_EXECUTE_LOG, 0x0B6E8},
            {Opcode.SMSG_SPELL_FAILED_OTHER, 0x0E7A4},
            {Opcode.SMSG_SPELL_FAILURE, 0x0F9CC},
            {Opcode.SMSG_SPELL_GO, 0x030C0},
            {Opcode.SMSG_SPELL_HEAL_LOG, 0x06E84},
            {Opcode.SMSG_SPELL_INSTAKILL_LOG, 0x061C8},
            {Opcode.SMSG_SPELL_MISS_LOG, 0x0BDA4},
            {Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG, 0x074AC},
            {Opcode.SMSG_SPELL_OR_DAMAGE_IMMUNE, 0x073C8},
            {Opcode.SMSG_SPELL_START, 0x06BA8},
            {Opcode.SMSG_SPELL_STEAL_LOG, 0x09F74}, //4.0.3a13329-403
            {Opcode.SMSG_SPELL_UPDATE_CHAIN_TARGETS, 0x036A4},
            {Opcode.SMSG_SPIRIT_HEALER_CONFIRM, 0x067E0},
            {Opcode.SMSG_STABLE_RESULT, 0x0EACC},
            //{Opcode.SMSG_STAND_STATE_CHANGE_FAILURE_OBSOLETE, 0x06DCC},
            {Opcode.SMSG_STAND_STATE_UPDATE, 0x0E6A8},
            {Opcode.SMSG_START_MIRROR_TIMER, 0x0B4A8},
            {Opcode.SMSG_STOP_DANCE, 0x0E0A8},
            {Opcode.SMSG_STOP_MIRROR_TIMER, 0x0A68C},
            {Opcode.SMSG_SUMMON_CANCEL, 0x070A8},
            {Opcode.SMSG_SUMMON_REQUEST, 0x03AE4},
            {Opcode.SMSG_SUPERCEDED_SPELLS, 0x0E8E4},
            {Opcode.SMSG_SUSPEND_COMMS, 0x10008}, //UnknownopcodeID
            //{Opcode.SMSG_TALENT_ERROR, 0x068A4},
            {Opcode.SMSG_UPDATE_TALENT_DATA, 0x075C4},
            {Opcode.SMSG_TALENTS_INVOLUNTARILY_RESET, 0x02A84},
            {Opcode.SMSG_TAXI_NODE_STATUS, 0x077E8},
            {Opcode.SMSG_TEXT_EMOTE, 0x0BB8C},
            {Opcode.SMSG_THREAT_CLEAR, 0x0FFC8},
            {Opcode.SMSG_THREAT_REMOVE, 0x029E0},
            {Opcode.SMSG_THREAT_UPDATE, 0x0B480},
            {Opcode.SMSG_TIME_SYNC_REQUEST, 0x0AA80},
            {Opcode.SMSG_TITLE_EARNED, 0x06C8C},
            {Opcode.SMSG_TOGGLE_XP_GAIN, 0x07980},
            {Opcode.SMSG_TOTEM_CREATED, 0x02EAC},
            {Opcode.SMSG_TRADE_STATUS, 0x0494E},
            {Opcode.SMSG_TRADE_STATUS_EXTENDED, 0x0400C},
            {Opcode.SMSG_TRAINER_BUY_RESULT, 0x06DEC},
            {Opcode.SMSG_TRAINER_LIST, 0x0BBE0},
            {Opcode.SMSG_TRANSFER_ABORTED, 0x02BE0},
            {Opcode.SMSG_TRANSFER_PENDING, 0x07BE0},
            {Opcode.SMSG_TRIGGER_CINEMATIC, 0x073A4},
            {Opcode.SMSG_TRIGGER_MOVIE, 0x020C8},
            {Opcode.SMSG_TURN_IN_PETITION_RESULT, 0x035AC},
            {Opcode.SMSG_TUTORIAL_FLAGS, 0x03384},
            {Opcode.SMSG_UI_TIME, 0x05557},
            {Opcode.SMSG_UNIT_SPELLCAST_START, 0x0BDE0},
            {Opcode.SMSG_UNKNOWN_1240, 0x0B8E0},
            {Opcode.SMSG_UNKNOWN_1276, 0x1023A},
            {Opcode.SMSG_UNKNOWN_1302, 0x10010}, //UnknownopcodeName}, ID
            {Opcode.SMSG_UNKNOWN_1304, 0x072E0},
            {Opcode.SMSG_UNKNOWN_1308, 0x10011}, //UnknownopcodeName}, ID
            {Opcode.SMSG_UNKNOWN_1310, 0x02E88},
            {Opcode.SMSG_UNKNOWN_1311, 0x01E76}, //UnknownopcodeID/Name
            {Opcode.SMSG_UNKNOWN_1312, 0x04D56}, //UnknownopcodeID/Name
            {Opcode.SMSG_UNKNOWN_1314, 0x10256}, //UnknownopcodeID/Name
            {Opcode.SMSG_UNKNOWN_1315, 0x10257}, //UnknownopcodeID/Name
            {Opcode.SMSG_UNKNOWN_1316, 0x10258}, //UnknownopcodeID/Name
            {Opcode.SMSG_UNKNOWN_1317, 0x10259}, //UnknownopcodeID/Name
            {Opcode.SMSG_UNKNOWN_1329, 0x02D57}, //4.0.3a13329
            {Opcode.SMSG_UNKNOWN_1330, 0x0618C},
            //{Opcode.SMSG_UNKNOWN_65508, 0x0FFE4}, //UnknownopcodeName}, 4.0.6a13623
            //{Opcode.SMSG_UNKNOWN_GOLD, 0x0004E}, //gives gold to the player
            {Opcode.SMSG_UNLEARNED_SPELLS, 0x07CA0},
            {Opcode.SMSG_UPDATE_ACCOUNT_DATA, 0x0F7A0},
            {Opcode.SMSG_UPDATE_ACCOUNT_DATA_COMPLETE, 0x0B1CC},
            {Opcode.SMSG_UPDATE_ACTION_BUTTONS, 0x02CCC},
            {Opcode.SMSG_UPDATE_COMBO_POINTS, 0x037A8},
            {Opcode.SMSG_UPDATE_CURRENCY, 0x0405E},
            {Opcode.SMSG_UPDATE_CURRENCY_WEEK_LIMIT, 0x04C5C},
            {Opcode.SMSG_UPDATE_INSTANCE_ENCOUNTER_UNIT, 0x0EDA4},// wtf::SMSG_GAMEOBJECT_SPAWN_ANIM_OBSOLETE
            {Opcode.SMSG_UPDATE_INSTANCE_OWNERSHIP, 0x03B8C},
            {Opcode.SMSG_UPDATE_LAST_INSTANCE, 0x033E8},
            //{Opcode.SMSG_UPDATE_LFG_LIST, 0x0768C},
            {Opcode.SMSG_UPDATE_OBJECT, 0x03780},
            {Opcode.SMSG_UPDATE_WORLD_STATE, 0x0F784},
            {Opcode.SMSG_USERLIST_ADD, 0x0F8CC},
            {Opcode.SMSG_USERLIST_REMOVE, 0x0EF80},
            {Opcode.SMSG_USERLIST_UPDATE, 0x02C8C},
            {Opcode.SMSG_USE_EQUIPMENT_SET_RESULT, 0x076AC},
            {Opcode.SMSG_VOICESESSION_FULL, 0x0A456}, //4.0.3a13329-403
            {Opcode.SMSG_VOICE_CHAT_STATUS, 0x06B88},
            {Opcode.SMSG_VOICE_PARENTAL_CONTROLS, 0x071E8},
            {Opcode.SMSG_VOICE_SESSION_ADJUST_PRIORITY, 0x10165}, //UnknownopcodeID
            {Opcode.SMSG_VOICE_SESSION_ENABLE, 0x10170}, //UnknownopcodeID
            {Opcode.SMSG_VOICE_SESSION_LEAVE, 0x078A4},
            {Opcode.SMSG_VOICE_SESSION_ROSTER_UPDATE, 0x0ACC0},
            {Opcode.SMSG_VOICE_SET_TALKER_MUTED, 0x0E3E4},
            {Opcode.SMSG_WARDEN_DATA, 0x0F8A0},
            {Opcode.SMSG_WARGAME_REQUEST_RESPONSE, 0x0094E},
            {Opcode.SMSG_WARGAME_REQUEST_SENT, 0x00C1C},
            {Opcode.SMSG_WEATHER, 0x079A0},
            {Opcode.SMSG_WHO, 0x0BE8C},
            {Opcode.SMSG_WHO_IS, 0x0B1A4},
            {Opcode.SMSG_ZONE_UNDER_ATTACK, 0x0BD80}
        };

        private static readonly BiDictionary<Opcode, int> MiscOpcodes = new BiDictionary<Opcode, int>
        {
            {Opcode.MSG_AUCTION_HELLO, 0x0B3A0},
            {Opcode.MSG_CHANNEL_START, 0x02BAC},
            {Opcode.MSG_CHANNEL_UPDATE, 0x062AC},
            {Opcode.MSG_CORPSE_QUERY, 0x0E0C8},
            {Opcode.MSG_DELAY_GHOST_TELEPORT, 0x10127}, //0x03729
            {Opcode.MSG_GM_ACCOUNT_ONLINE, 0x100C3}, //UnknownopcodeID
            {Opcode.MSG_GM_BIND_OTHER, 0x1008B}, //UnknownopcodeID
            {Opcode.MSG_GM_CHANGE_ARENA_RATING, 0x101A3}, //UnknownopcodeID
            {Opcode.MSG_GM_DESTROY_CORPSE, 0x10117}, //UnknownopcodeID
            {Opcode.MSG_GM_GEARRATING, 0x10173}, //UnknownopcodeID
            {Opcode.MSG_GM_RESETINSTANCELIMIT, 0x1012F}, //UnknownopcodeID
            {Opcode.MSG_GM_SHOWLABEL, 0x1008D}, //UnknownopcodeID
            {Opcode.MSG_GM_SUMMON, 0x1008C}, //UnknownopcodeID
            {Opcode.MSG_GUILD_BANK_LOG_QUERY, 0x0F584},
            {Opcode.MSG_GUILD_BANK_MONEY_WITHDRAWN, 0x06CE4},
            {Opcode.MSG_GUILD_EVENT_LOG_QUERY, 0x069EC},
            {Opcode.MSG_GUILD_PERMISSIONS, 0x0F4C4},
            {Opcode.MSG_INSPECT_ARENA_TEAMS, 0x0FDA4},
            {Opcode.MSG_LIST_STABLED_PETS, 0x06EAC},
            {Opcode.MSG_MINIMAP_PING, 0x033A0},
            {Opcode.MSG_MOVE_FALL_LAND, 0x0AFAC},
            {Opcode.MSG_MOVE_FEATHER_FALL, 0x0B6A8},
            {Opcode.MSG_MOVE_HEARTBEAT, 0x022EC},
            {Opcode.MSG_MOVE_HOVER, 0x02FCC},
            {Opcode.MSG_MOVE_JUMP, 0x065AC},
            {Opcode.MSG_MOVE_KNOCK_BACK, 0x0B0E8},
            {Opcode.MSG_MOVE_ROOT, 0x0ABA8},
            {Opcode.MSG_MOVE_SET_ALL_SPEED_CHEAT, 0x10025}, //UnknownopcodeID
            {Opcode.MSG_MOVE_SET_FACING, 0x0ABC4},
            {Opcode.MSG_MOVE_SET_FLIGHT_BACK_SPEED, 0x0B484},
            {Opcode.MSG_MOVE_SET_FLIGHT_BACK_SPEED_CHEAT, 0x10150}, //UnknownopcodeID
            {Opcode.MSG_MOVE_SET_FLIGHT_SPEED, 0x0B088},
            {Opcode.MSG_MOVE_SET_FLIGHT_SPEED_CHEAT, 0x1014F}, //UnknownopcodeID
            {Opcode.MSG_MOVE_SET_PITCH, 0x0EBA0},
            {Opcode.MSG_MOVE_SET_PITCH_RATE, 0x0ABA4},
            {Opcode.MSG_MOVE_SET_PITCH_RATE_CHEAT, 0x101CD}, //UnknownopcodeID
            {Opcode.MSG_MOVE_SET_RAW_POSITION_ACK, 0x0F4A0},
            {Opcode.MSG_MOVE_SET_RUN_BACK_SPEED, 0x0B5EC},
            {Opcode.MSG_MOVE_SET_RUN_BACK_SPEED_CHEAT, 0x10021}, //UnknownopcodeID
            {Opcode.MSG_MOVE_SET_RUN_MODE, 0x0E7A8},
            {Opcode.MSG_MOVE_SET_RUN_SPEED, 0x064A0},
            {Opcode.MSG_MOVE_SET_RUN_SPEED_CHEAT, 0x10020}, //UnknownopcodeID
            {Opcode.MSG_MOVE_SET_SWIM_BACK_SPEED, 0x0B0AC},
            {Opcode.MSG_MOVE_SET_SWIM_BACK_SPEED_CHEAT, 0x10024}, //UnknownopcodeID
            {Opcode.MSG_MOVE_SET_SWIM_SPEED, 0x02380},
            {Opcode.MSG_MOVE_SET_SWIM_SPEED_CHEAT, 0x10023}, //UnknownopcodeID
            {Opcode.MSG_MOVE_SET_TURN_RATE, 0x0A3A8}, //UnknownopcodeID
            {Opcode.MSG_MOVE_SET_TURN_RATE_CHEAT, 0x10026}, //UnknownopcodeID
            {Opcode.MSG_MOVE_SET_WALK_MODE, 0x03FAC},
            {Opcode.MSG_MOVE_SET_WALK_SPEED, 0x0F284},
            {Opcode.MSG_MOVE_SET_WALK_SPEED_CHEAT, 0x10022}, //UnknownopcodeID
            {Opcode.MSG_MOVE_START_ASCEND, 0x0BDC0},
            {Opcode.MSG_MOVE_START_BACKWARD, 0x072E4},
            {Opcode.MSG_MOVE_START_DESCEND, 0x07880},
            {Opcode.MSG_MOVE_START_FORWARD, 0x0EBAC},
            {Opcode.MSG_MOVE_START_PITCH_DOWN, 0x0ADC4},
            {Opcode.MSG_MOVE_START_PITCH_UP, 0x060E4},
            {Opcode.MSG_MOVE_START_STRAFE_LEFT, 0x060E8},
            {Opcode.MSG_MOVE_START_STRAFE_RIGHT, 0x07DA4},
            //{Opcode.MSG_MOVE_START_SWIM, 0x026C0},
            //{Opcode.MSG_MOVE_START_SWIM_CHEAT, 0x026C0},
            {Opcode.MSG_MOVE_START_TURN_LEFT, 0x0B8C8},
            {Opcode.MSG_MOVE_START_TURN_RIGHT, 0x0F9E4},
            {Opcode.MSG_MOVE_STOP, 0x034E0},
            {Opcode.MSG_MOVE_STOP_ASCEND, 0x0FCA8},
            {Opcode.MSG_MOVE_STOP_PITCH, 0x028E8},
            {Opcode.MSG_MOVE_STOP_STRAFE, 0x0F9A8},
            {Opcode.MSG_MOVE_STOP_SWIM, 0x0FDE8},
            //{Opcode.MSG_MOVE_STOP_SWIM_CHEAT, 0x06988},
            {Opcode.MSG_MOVE_STOP_TURN, 0x0E2E8},
            {Opcode.MSG_MOVE_TELEPORT, 0x024E4},
            {Opcode.MSG_MOVE_TELEPORT_ACK, 0x06DAC},
            {Opcode.MSG_MOVE_TELEPORT_CHEAT, 0x0E7EC},
            {Opcode.MSG_MOVE_TIME_SKIPPED, 0x025E4},
            {Opcode.MSG_MOVE_TOGGLE_COLLISION_CHEAT, 0x07688},
            {Opcode.MSG_MOVE_TOGGLE_FALL_LOGGING, 0x1001F}, //UnknownopcodeID
            {Opcode.MSG_MOVE_TOGGLE_GRAVITY_CHEAT, 0x100DD}, //UnknownopcodeID
            {Opcode.MSG_MOVE_TOGGLE_LOGGING, 0x1001D}, //UnknownopcodeID
            {Opcode.MSG_MOVE_UNROOT, 0x02088},
            {Opcode.MSG_MOVE_UPDATE_CAN_FLY, 0x025E0},
            {Opcode.MSG_MOVE_WATER_WALK, 0x0E1A4},
            {Opcode.MSG_MOVE_WORLDPORT_ACK, 0x02FC0},
            {Opcode.MSG_NOTIFY_PARTY_SQUELCH, 0x0E8E8},
            {Opcode.MSG_PARTY_ASSIGNMENT, 0x028AC},
            {Opcode.MSG_PETITION_DECLINE, 0x032E0},
            {Opcode.MSG_PETITION_RENAME, 0x070C0},
            {Opcode.MSG_PVP_LOG_DATA, 0x00C0E},
            {Opcode.MSG_QUERY_GUILD_BANK_TEXT, 0x0A2C4},
            {Opcode.MSG_QUERY_NEXT_MAIL_TIME, 0x025C8},
            {Opcode.MSG_QUEST_PUSH_RESULT, 0x022A4},
            {Opcode.MSG_RAID_READY_CHECK, 0x0FDC0},
            {Opcode.MSG_RAID_READY_CHECK_CONFIRM, 0x0A2AC},
            {Opcode.MSG_RAID_READY_CHECK_FINISHED, 0x0A0A0},
            {Opcode.MSG_RAID_TARGET_UPDATE, 0x0A5AC},
            {Opcode.MSG_RANDOM_ROLL, 0x0B7A4},
            {Opcode.MSG_SAVE_GUILD_EMBLEM, 0x031AC},
            {Opcode.MSG_SET_DUNGEON_DIFFICULTY, 0x074E0},
            {Opcode.MSG_SET_RAID_DIFFICULTY, 0x0B5E8},
            {Opcode.MSG_TABARDVENDOR_ACTIVATE, 0x02C80},
            {Opcode.MSG_TALENT_WIPE_CONFIRM, 0x0BFC4},
            {Opcode.OBSOLETE_DROP_ITEM, 0x10049} //UnknownopcodeID
        };
    }
}
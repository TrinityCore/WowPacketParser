using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V4_4_2_59185
{
    public static class Opcodes_4_4_2
    {
        public static BiDictionary<Opcode, int> Opcodes(Direction direction)
        {
            switch (direction)
            {
                case Direction.ClientToServer:
                    return ClientOpcodes;
                case Direction.ServerToClient:
                    return ServerOpcodes;
                default:
                    return MiscOpcodes;
            }
        }

        private static readonly BiDictionary<Opcode, int> ClientOpcodes = new()
        {
            { Opcode.CMSG_ACCEPT_GUILD_INVITE, 0x390029 },
            { Opcode.CMSG_ACCEPT_SOCIAL_CONTRACT, 0x39017C }, // NYI
            { Opcode.CMSG_ACCEPT_TRADE, 0x340005 },
            { Opcode.CMSG_ACCEPT_WARGAME_INVITE, 0x39000C }, // NYI
            { Opcode.CMSG_ACCOUNT_NOTIFICATION_ACKNOWLEDGED, 0x390169 }, // NYI
            { Opcode.CMSG_ACCOUNT_STORE_BEGIN_PURCHASE_OR_REFUND, 0x3900BF },
            { Opcode.CMSG_ACTIVATE_TAXI, 0x350035 },
            { Opcode.CMSG_ADDON_LIST, 0x390004 }, // NYI
            { Opcode.CMSG_ADD_BATTLENET_FRIEND, 0x390086 }, // NYI
            { Opcode.CMSG_ADD_FRIEND, 0x390105 },
            { Opcode.CMSG_ADD_IGNORE, 0x390109 },
            { Opcode.CMSG_ADD_TOY, 0x340152 },
            { Opcode.CMSG_ADVENTURE_MAP_START_QUEST, 0x340194 },
            { Opcode.CMSG_ALTER_APPEARANCE, 0x350088 },
            { Opcode.CMSG_AREA_SPIRIT_HEALER_QUERY, 0x35003A },
            { Opcode.CMSG_AREA_SPIRIT_HEALER_QUEUE, 0x35003B },
            { Opcode.CMSG_AREA_TRIGGER, 0x340084 },
            { Opcode.CMSG_ARENA_TEAM_ACCEPT, 0x3900E6 }, // NYI
            { Opcode.CMSG_ARENA_TEAM_DECLINE, 0x3900E7 }, // NYI
            { Opcode.CMSG_ARENA_TEAM_DISBAND, 0x3900EA }, // NYI
            { Opcode.CMSG_ARENA_TEAM_LEADER, 0x3900EB }, // NYI
            { Opcode.CMSG_ARENA_TEAM_LEAVE, 0x3900E8 }, // NYI
            { Opcode.CMSG_ARENA_TEAM_REMOVE, 0x3900E9 }, // NYI
            { Opcode.CMSG_ARENA_TEAM_ROSTER, 0x3900E5 },
            { Opcode.CMSG_ASSIGN_EQUIPMENT_SET_SPEC, 0x3400B5 }, // NYI
            { Opcode.CMSG_ATTACK_STOP, 0x34010C },
            { Opcode.CMSG_ATTACK_SWING, 0x34010B },
            { Opcode.CMSG_AUCTIONABLE_TOKEN_SELL, 0x39011B }, // NYI
            { Opcode.CMSG_AUCTIONABLE_TOKEN_SELL_AT_MARKET_PRICE, 0x39011C }, // NYI
            { Opcode.CMSG_AUCTION_BROWSE_QUERY, 0x35005C },
            { Opcode.CMSG_AUCTION_CANCEL_COMMODITIES_PURCHASE, 0x350064 },
            { Opcode.CMSG_AUCTION_CONFIRM_COMMODITIES_PURCHASE, 0x350063 },
            { Opcode.CMSG_AUCTION_GET_COMMODITY_QUOTE, 0x350062 },
            { Opcode.CMSG_AUCTION_HELLO_REQUEST, 0x350054 },
            { Opcode.CMSG_AUCTION_LIST_BIDDED_ITEMS, 0x350060 },
            { Opcode.CMSG_AUCTION_LIST_BIDDER_ITEMS, 0x35005A },
            { Opcode.CMSG_AUCTION_LIST_BUCKETS_BY_BUCKET_KEYS, 0x350061 },
            { Opcode.CMSG_AUCTION_LIST_ITEMS, 0x350057 },
            { Opcode.CMSG_AUCTION_LIST_ITEMS_BY_BUCKET_KEY, 0x35005D },
            { Opcode.CMSG_AUCTION_LIST_ITEMS_BY_ITEM_ID, 0x35005E },
            { Opcode.CMSG_AUCTION_LIST_OWNED_ITEMS, 0x35005F },
            { Opcode.CMSG_AUCTION_LIST_OWNER_ITEMS, 0x350059 },
            { Opcode.CMSG_AUCTION_LIST_PENDING_SALES, 0x350066 },
            { Opcode.CMSG_AUCTION_PLACE_BID, 0x35005B },
            { Opcode.CMSG_AUCTION_REMOVE_ITEM, 0x350056 },
            { Opcode.CMSG_AUCTION_REPLICATE_ITEMS, 0x350058 },
            { Opcode.CMSG_AUCTION_SELL_COMMODITY, 0x350065 },
            { Opcode.CMSG_AUCTION_SELL_ITEM, 0x350055 },
            { Opcode.CMSG_AUCTION_SET_FAVORITE_ITEM, 0x39016A },
            { Opcode.CMSG_AUTH_CONTINUED_SESSION, 0x3A0002 },
            { Opcode.CMSG_AUTH_SESSION, 0x3A0001 },
            { Opcode.CMSG_AUTOBANK_ITEM, 0x360003 },
            { Opcode.CMSG_AUTOSTORE_BANK_ITEM, 0x360002 },
            { Opcode.CMSG_AUTO_EQUIP_ITEM, 0x360004 },
            { Opcode.CMSG_AUTO_EQUIP_ITEM_SLOT, 0x360009 },
            { Opcode.CMSG_AUTO_GUILD_BANK_ITEM, 0x350040 },
            { Opcode.CMSG_AUTO_STORE_BAG_ITEM, 0x360005 },
            { Opcode.CMSG_AUTO_STORE_GUILD_BANK_ITEM, 0x350049 },
            { Opcode.CMSG_AZERITE_EMPOWERED_ITEM_SELECT_POWER, 0x3401AD }, // NYI
            { Opcode.CMSG_AZERITE_EMPOWERED_ITEM_VIEWED, 0x34019B }, // NYI
            { Opcode.CMSG_AZERITE_ESSENCE_ACTIVATE_ESSENCE, 0x3401AF }, // NYI
            { Opcode.CMSG_AZERITE_ESSENCE_UNLOCK_MILESTONE, 0x3401AE }, // NYI
            { Opcode.CMSG_BANKER_ACTIVATE, 0x35003D },
            { Opcode.CMSG_BATTLEFIELD_LEAVE, 0x34001E },
            { Opcode.CMSG_BATTLEFIELD_LIST, 0x34002A },
            { Opcode.CMSG_BATTLEFIELD_PORT, 0x3500BB },
            { Opcode.CMSG_BATTLEMASTER_HELLO, 0x34016B },
            { Opcode.CMSG_BATTLEMASTER_JOIN, 0x3500B4 },
            { Opcode.CMSG_BATTLEMASTER_JOIN_ARENA, 0x3500B5 },
            { Opcode.CMSG_BATTLEMASTER_JOIN_SKIRMISH, 0x3500B6 },
            { Opcode.CMSG_BATTLENET_CHALLENGE_RESPONSE, 0x390108 }, // NYI
            { Opcode.CMSG_BATTLENET_REQUEST, 0x390129 },
            { Opcode.CMSG_BATTLE_PAY_ACK_FAILED_RESPONSE, 0x390102 },
            { Opcode.CMSG_BATTLE_PAY_CANCEL_OPEN_CHECKOUT, 0x390147 },
            { Opcode.CMSG_BATTLE_PAY_CONFIRM_PURCHASE_RESPONSE, 0x390101 },
            { Opcode.CMSG_BATTLE_PAY_DISTRIBUTION_ASSIGN_TO_TARGET, 0x3900F8 },
            { Opcode.CMSG_BATTLE_PAY_DISTRIBUTION_ASSIGN_VAS, 0x39016D },
            { Opcode.CMSG_BATTLE_PAY_GET_PRODUCT_LIST, 0x3900F1 },
            { Opcode.CMSG_BATTLE_PAY_GET_PURCHASE_LIST, 0x3900F2 },
            { Opcode.CMSG_BATTLE_PAY_OPEN_CHECKOUT, 0x390140 },
            { Opcode.CMSG_BATTLE_PAY_REQUEST_PRICE_INFO, 0x39013C },
            { Opcode.CMSG_BATTLE_PAY_START_PURCHASE, 0x390100 },
            { Opcode.CMSG_BATTLE_PAY_START_VAS_PURCHASE, 0x390127 },
            { Opcode.CMSG_BATTLE_PET_CLEAR_FANFARE, 0x2E0002 },
            { Opcode.CMSG_BATTLE_PET_REQUEST_JOURNAL, 0x39004E },
            { Opcode.CMSG_BATTLE_PET_REQUEST_JOURNAL_LOCK, 0x39004D },
            { Opcode.CMSG_BATTLE_PET_SET_BATTLE_SLOT, 0x390057 },
            { Opcode.CMSG_BATTLE_PET_SET_FLAGS, 0x39005A },
            { Opcode.CMSG_BATTLE_PET_SUMMON, 0x390053 },
            { Opcode.CMSG_BATTLE_PET_UPDATE_DISPLAY_NOTIFY, 0x34008E }, // NYI
            { Opcode.CMSG_BATTLE_PET_UPDATE_NOTIFY, 0x34008D },
            { Opcode.CMSG_BEGIN_TRADE, 0x340002 },
            { Opcode.CMSG_BINDER_ACTIVATE, 0x35003C },
            { Opcode.CMSG_BLACK_MARKET_OPEN, 0x3500C1 },
            { Opcode.CMSG_BUG_REPORT, 0x3900B1 },
            { Opcode.CMSG_BUSY_TRADE, 0x340003 },
            { Opcode.CMSG_BUY_BACK_ITEM, 0x35002E },
            { Opcode.CMSG_BUY_BANK_SLOT, 0x35003E },
            { Opcode.CMSG_BUY_ITEM, 0x35002D },
            { Opcode.CMSG_CALENDAR_ADD_EVENT, 0x3900A9 },
            { Opcode.CMSG_CALENDAR_COMMUNITY_INVITE, 0x39009D },
            { Opcode.CMSG_CALENDAR_COMPLAIN, 0x3900A5 },
            { Opcode.CMSG_CALENDAR_COPY_EVENT, 0x3900A4 },
            { Opcode.CMSG_CALENDAR_EVENT_SIGN_UP, 0x3900A7 },
            { Opcode.CMSG_CALENDAR_GET, 0x39009B },
            { Opcode.CMSG_CALENDAR_GET_EVENT, 0x39009C },
            { Opcode.CMSG_CALENDAR_GET_NUM_PENDING, 0x3900A6 },
            { Opcode.CMSG_CALENDAR_INVITE, 0x39009E },
            { Opcode.CMSG_CALENDAR_MODERATOR_STATUS, 0x3900A2 },
            { Opcode.CMSG_CALENDAR_REMOVE_EVENT, 0x3900A3 },
            { Opcode.CMSG_CALENDAR_REMOVE_INVITE, 0x39009F },
            { Opcode.CMSG_CALENDAR_RSVP, 0x3900A0 },
            { Opcode.CMSG_CALENDAR_STATUS, 0x3900A1 },
            { Opcode.CMSG_CALENDAR_UPDATE_EVENT, 0x3900AA },
            { Opcode.CMSG_CANCEL_AURA, 0x340058 },
            { Opcode.CMSG_CANCEL_AUTO_REPEAT_SPELL, 0x35007A },
            { Opcode.CMSG_CANCEL_CAST, 0x340158 },
            { Opcode.CMSG_CANCEL_CHANNELLING, 0x340121 },
            { Opcode.CMSG_CANCEL_GROWTH_AURA, 0x340126 },
            { Opcode.CMSG_CANCEL_MOUNT_AURA, 0x340136 },
            { Opcode.CMSG_CANCEL_QUEUED_SPELL, 0x34002B },
            { Opcode.CMSG_CANCEL_TEMP_ENCHANTMENT, 0x350085 },
            { Opcode.CMSG_CANCEL_TRADE, 0x340007 },
            { Opcode.CMSG_CAN_DUEL, 0x39008E },
            { Opcode.CMSG_CAN_REDEEM_TOKEN_FOR_BALANCE, 0x39013B }, // NYI
            { Opcode.CMSG_CAST_SPELL, 0x340155 },
            { Opcode.CMSG_CHANGE_REALM_TICKET, 0x39012D },
            { Opcode.CMSG_CHANGE_SUB_GROUP, 0x390078 },
            { Opcode.CMSG_CHARACTER_CHECK_UPGRADE, 0x3900FB }, // NYI
            { Opcode.CMSG_CHARACTER_RENAME_REQUEST, 0x3900F6 },
            { Opcode.CMSG_CHARACTER_UPGRADE_MANUAL_UNREVOKE_REQUEST, 0x3900F9 }, // NYI
            { Opcode.CMSG_CHARACTER_UPGRADE_START, 0x3900FA }, // NYI
            { Opcode.CMSG_CHAR_CREATE_FINALIZE_REINCARNATION, 0x390195 }, // NYI
            { Opcode.CMSG_CHAR_CUSTOMIZE, 0x3900B7 },
            { Opcode.CMSG_CHAR_DELETE, 0x3900CA },
            { Opcode.CMSG_CHAR_RACE_OR_FACTION_CHANGE, 0x3900BD },
            { Opcode.CMSG_CHAT_ADDON_MESSAGE, 0x300026 },
            { Opcode.CMSG_CHAT_ADDON_MESSAGE_TARGETED, 0x300027 },
            { Opcode.CMSG_CHAT_CHANNEL_ANNOUNCEMENTS, 0x30001B },
            { Opcode.CMSG_CHAT_CHANNEL_BAN, 0x300019 },
            { Opcode.CMSG_CHAT_CHANNEL_DECLINE_INVITE, 0x30001E },
            { Opcode.CMSG_CHAT_CHANNEL_DISPLAY_LIST, 0x30000E },
            { Opcode.CMSG_CHAT_CHANNEL_INVITE, 0x300017 },
            { Opcode.CMSG_CHAT_CHANNEL_KICK, 0x300018 },
            { Opcode.CMSG_CHAT_CHANNEL_LIST, 0x30000D },
            { Opcode.CMSG_CHAT_CHANNEL_MODERATOR, 0x300013 },
            { Opcode.CMSG_CHAT_CHANNEL_OWNER, 0x300011 },
            { Opcode.CMSG_CHAT_CHANNEL_PASSWORD, 0x30000F },
            { Opcode.CMSG_CHAT_CHANNEL_SET_OWNER, 0x300010 },
            { Opcode.CMSG_CHAT_CHANNEL_SILENCE_ALL, 0x30001C },
            { Opcode.CMSG_CHAT_CHANNEL_UNBAN, 0x30001A },
            { Opcode.CMSG_CHAT_CHANNEL_UNMODERATOR, 0x300014 },
            { Opcode.CMSG_CHAT_CHANNEL_UNSILENCE_ALL, 0x30001D },
            { Opcode.CMSG_CHAT_JOIN_CHANNEL, 0x300000 },
            { Opcode.CMSG_CHAT_LEAVE_CHANNEL, 0x300001 },
            { Opcode.CMSG_CHAT_MESSAGE_AFK, 0x30000B },
            { Opcode.CMSG_CHAT_MESSAGE_CHANNEL, 0x300007 },
            { Opcode.CMSG_CHAT_MESSAGE_DND, 0x30000C },
            { Opcode.CMSG_CHAT_MESSAGE_EMOTE, 0x300020 },
            { Opcode.CMSG_CHAT_MESSAGE_GUILD, 0x300009 },
            { Opcode.CMSG_CHAT_MESSAGE_INSTANCE_CHAT, 0x300024 },
            { Opcode.CMSG_CHAT_MESSAGE_OFFICER, 0x30000A },
            { Opcode.CMSG_CHAT_MESSAGE_PARTY, 0x300022 },
            { Opcode.CMSG_CHAT_MESSAGE_RAID, 0x300023 },
            { Opcode.CMSG_CHAT_MESSAGE_RAID_WARNING, 0x300025 },
            { Opcode.CMSG_CHAT_MESSAGE_SAY, 0x30001F },
            { Opcode.CMSG_CHAT_MESSAGE_WHISPER, 0x300008 },
            { Opcode.CMSG_CHAT_MESSAGE_YELL, 0x300021 },
            { Opcode.CMSG_CHAT_REGISTER_ADDON_PREFIXES, 0x300005 },
            { Opcode.CMSG_CHAT_REPORT_FILTERED, 0x300004 }, // NYI
            { Opcode.CMSG_CHAT_REPORT_IGNORED, 0x300003 },
            { Opcode.CMSG_CHAT_UNREGISTER_ALL_ADDON_PREFIXES, 0x300006 },
            { Opcode.CMSG_CHECK_IS_ADVENTURE_MAP_POI_VALID, 0x3400FD },
            { Opcode.CMSG_CLEAR_NEW_APPEARANCE, 0x2E0005 }, // NYI
            { Opcode.CMSG_CLEAR_RAID_MARKER, 0x340050 },
            { Opcode.CMSG_CLEAR_TRADE_ITEM, 0x340009 },
            { Opcode.CMSG_CLIENT_PORT_GRAVEYARD, 0x3500BD },
            { Opcode.CMSG_CLOSE_INTERACTION, 0x35001D },
            { Opcode.CMSG_CLOSE_QUEST_CHOICE, 0x34015C }, // NYI
            { Opcode.CMSG_CLOSE_TRAIT_SYSTEM_INTERACTION, 0x3401C9 }, // NYI
            { Opcode.CMSG_CLUB_FINDER_APPLICATION_RESPONSE, 0x390152 }, // NYI
            { Opcode.CMSG_CLUB_FINDER_GET_APPLICANTS_LIST, 0x390150 }, // NYI
            { Opcode.CMSG_CLUB_FINDER_POST, 0x39014D }, // NYI
            { Opcode.CMSG_CLUB_FINDER_REQUEST_CLUBS_DATA, 0x390154 }, // NYI
            { Opcode.CMSG_CLUB_FINDER_REQUEST_CLUBS_LIST, 0x39014E }, // NYI
            { Opcode.CMSG_CLUB_FINDER_REQUEST_MEMBERSHIP_TO_CLUB, 0x39014F }, // NYI
            { Opcode.CMSG_CLUB_FINDER_REQUEST_PENDING_CLUBS_LIST, 0x390153 }, // NYI
            { Opcode.CMSG_CLUB_FINDER_REQUEST_SUBSCRIBED_CLUB_POSTING_IDS, 0x390155 }, // NYI
            { Opcode.CMSG_CLUB_FINDER_RESPOND_TO_APPLICANT, 0x390151 }, // NYI
            { Opcode.CMSG_CLUB_FINDER_WHISPER_APPLICANT_REQUEST, 0x39016F }, // NYI
            { Opcode.CMSG_CLUB_PRESENCE_SUBSCRIBE, 0x39012B }, // NYI
            { Opcode.CMSG_COLLECTION_ITEM_SET_FAVORITE, 0x39005D },
            { Opcode.CMSG_COMMENTATOR_ENABLE, 0x39001C }, // NYI
            { Opcode.CMSG_COMMENTATOR_ENTER_INSTANCE, 0x390020 }, // NYI
            { Opcode.CMSG_COMMENTATOR_EXIT_INSTANCE, 0x390021 }, // NYI
            { Opcode.CMSG_COMMENTATOR_GET_MAP_INFO, 0x39001D }, // NYI
            { Opcode.CMSG_COMMENTATOR_GET_PLAYER_COOLDOWNS, 0x39001F }, // NYI
            { Opcode.CMSG_COMMENTATOR_GET_PLAYER_INFO, 0x39001E }, // NYI
            { Opcode.CMSG_COMMENTATOR_START_WARGAME, 0x39001B }, // NYI
            { Opcode.CMSG_COMMERCE_TOKEN_GET_COUNT, 0x390119 }, // NYI
            { Opcode.CMSG_COMMERCE_TOKEN_GET_LOG, 0x390123 },
            { Opcode.CMSG_COMMERCE_TOKEN_GET_MARKET_PRICE, 0x39011A },
            { Opcode.CMSG_COMPLAINT, 0x390098 },
            { Opcode.CMSG_COMPLETE_CINEMATIC, 0x3500DB },
            { Opcode.CMSG_COMPLETE_MOVIE, 0x350070 },
            { Opcode.CMSG_CONFIRM_BARBERS_CHOICE, 0x3400BC },
            { Opcode.CMSG_CONFIRM_RESPEC_WIPE, 0x3400BB },
            { Opcode.CMSG_CONNECT_TO_FAILED, 0x390000 },
            { Opcode.CMSG_CONSUMABLE_TOKEN_BUY, 0x39011E }, // NYI
            { Opcode.CMSG_CONSUMABLE_TOKEN_BUY_AT_MARKET_PRICE, 0x39011F }, // NYI
            { Opcode.CMSG_CONSUMABLE_TOKEN_CAN_VETERAN_BUY, 0x39011D }, // NYI
            { Opcode.CMSG_CONSUMABLE_TOKEN_REDEEM, 0x390121 }, // NYI
            { Opcode.CMSG_CONSUMABLE_TOKEN_REDEEM_CONFIRMATION, 0x390122 }, // NYI
            { Opcode.CMSG_CONTRIBUTION_LAST_UPDATE_REQUEST, 0x3500F2 }, // NYI
            { Opcode.CMSG_CONVERSATION_LINE_STARTED, 0x3500DC },
            { Opcode.CMSG_CONVERT_RAID, 0x39007A },
            { Opcode.CMSG_CREATE_CHARACTER, 0x39006E },
            { Opcode.CMSG_DB_QUERY_BULK, 0x390010 },
            { Opcode.CMSG_DECLINE_GUILD_INVITES, 0x3500B1 },
            { Opcode.CMSG_DECLINE_PETITION, 0x3500CA },
            { Opcode.CMSG_DELETE_EQUIPMENT_SET, 0x35009E },
            { Opcode.CMSG_DEL_FRIEND, 0x390106 },
            { Opcode.CMSG_DEL_IGNORE, 0x39010A },
            { Opcode.CMSG_DESTROY_ITEM, 0x34014C },
            { Opcode.CMSG_DF_BOOT_PLAYER_VOTE, 0x390044 },
            { Opcode.CMSG_DF_GET_JOIN_STATUS, 0x390042 },
            { Opcode.CMSG_DF_GET_SYSTEM_INFO, 0x390041 },
            { Opcode.CMSG_DF_JOIN, 0x390037 },
            { Opcode.CMSG_DF_LEAVE, 0x390040 },
            { Opcode.CMSG_DF_PROPOSAL_RESPONSE, 0x390035 },
            { Opcode.CMSG_DF_READY_CHECK_RESPONSE, 0x390048 }, // NYI
            { Opcode.CMSG_DF_SET_ROLES, 0x390043 },
            { Opcode.CMSG_DF_TELEPORT, 0x390045 },
            { Opcode.CMSG_DISCARDED_TIME_SYNC_ACKS, 0x37005E }, // NYI
            { Opcode.CMSG_DISMISS_CRITTER, 0x35008C },
            { Opcode.CMSG_DO_COUNTDOWN, 0x39014C }, // NYI
            { Opcode.CMSG_DO_READY_CHECK, 0x39005E },
            { Opcode.CMSG_DUEL_RESPONSE, 0x350075 },
            { Opcode.CMSG_EJECT_PASSENGER, 0x3400F2 },
            { Opcode.CMSG_EMOTE, 0x3500D7 },
            { Opcode.CMSG_ENABLE_NAGLE, 0x3A0007 },
            { Opcode.CMSG_ENABLE_TAXI_NODE, 0x350033 },
            { Opcode.CMSG_ENGINE_SURVEY, 0x390118 }, // NYI
            { Opcode.CMSG_ENTER_ENCRYPTED_MODE_ACK, 0x3A0003 },
            { Opcode.CMSG_ENUM_CHARACTERS, 0x390014 },
            { Opcode.CMSG_ENUM_CHARACTERS_DELETED_BY_CLIENT, 0x390112 },
            { Opcode.CMSG_FAR_SIGHT, 0x35007B },
            { Opcode.CMSG_GAME_EVENT_DEBUG_DISABLE, 0x34005A }, // NYI
            { Opcode.CMSG_GAME_EVENT_DEBUG_ENABLE, 0x34005B }, // NYI
            { Opcode.CMSG_GAME_OBJ_REPORT_USE, 0x350082 },
            { Opcode.CMSG_GAME_OBJ_USE, 0x350081 },
            { Opcode.CMSG_GENERATE_RANDOM_CHARACTER_NAME, 0x390013 },
            { Opcode.CMSG_GET_ACCOUNT_CHARACTER_LIST, 0x3900EC }, // NYI
            { Opcode.CMSG_GET_ACCOUNT_NOTIFICATIONS, 0x390168 }, // NYI
            { Opcode.CMSG_GET_ITEM_PURCHASE_DATA, 0x3500C5 },
            { Opcode.CMSG_GET_MIRROR_IMAGE_DATA, 0x340150 },
            { Opcode.CMSG_GET_PVP_OPTIONS_ENABLED, 0x39001A },
            { Opcode.CMSG_GET_REMAINING_GAME_TIME, 0x390120 }, // NYI
            { Opcode.CMSG_GET_UNDELETE_CHARACTER_COOLDOWN_STATUS, 0x390114 },
            { Opcode.CMSG_GET_VAS_ACCOUNT_CHARACTER_LIST, 0x390125 },
            { Opcode.CMSG_GET_VAS_TRANSFER_TARGET_REALM_LIST, 0x390126 },
            { Opcode.CMSG_GM_TICKET_ACKNOWLEDGE_SURVEY, 0x3900BB },
            { Opcode.CMSG_GM_TICKET_GET_CASE_STATUS, 0x3900BA },
            { Opcode.CMSG_GM_TICKET_GET_SYSTEM_STATUS, 0x3900B9 },
            { Opcode.CMSG_GOSSIP_SELECT_OPTION, 0x35001E },
            { Opcode.CMSG_GUILD_ADD_BATTLENET_FRIEND, 0x320020 }, // NYI
            { Opcode.CMSG_GUILD_ADD_RANK, 0x320005 },
            { Opcode.CMSG_GUILD_ASSIGN_MEMBER_RANK, 0x320002 },
            { Opcode.CMSG_GUILD_BANK_ACTIVATE, 0x35003F },
            { Opcode.CMSG_GUILD_BANK_BUY_TAB, 0x35004D },
            { Opcode.CMSG_GUILD_BANK_DEPOSIT_MONEY, 0x35004F },
            { Opcode.CMSG_GUILD_BANK_LOG_QUERY, 0x320019 },
            { Opcode.CMSG_GUILD_BANK_QUERY_TAB, 0x35004C },
            { Opcode.CMSG_GUILD_BANK_REMAINING_WITHDRAW_MONEY_QUERY, 0x32001A },
            { Opcode.CMSG_GUILD_BANK_SET_TAB_TEXT, 0x32001D },
            { Opcode.CMSG_GUILD_BANK_TEXT_QUERY, 0x32001E },
            { Opcode.CMSG_GUILD_BANK_UPDATE_TAB, 0x35004E },
            { Opcode.CMSG_GUILD_BANK_WITHDRAW_MONEY, 0x350050 },
            { Opcode.CMSG_GUILD_CHALLENGE_UPDATE_REQUEST, 0x320017 },
            { Opcode.CMSG_GUILD_CHANGE_NAME_REQUEST, 0x320018 },
            { Opcode.CMSG_GUILD_DECLINE_INVITATION, 0x39002A },
            { Opcode.CMSG_GUILD_DELETE, 0x320009 },
            { Opcode.CMSG_GUILD_DELETE_RANK, 0x320006 },
            { Opcode.CMSG_GUILD_DEMOTE_MEMBER, 0x320001 },
            { Opcode.CMSG_GUILD_EVENT_LOG_QUERY, 0x32001C },
            { Opcode.CMSG_GUILD_GET_ACHIEVEMENT_MEMBERS, 0x320012 },
            { Opcode.CMSG_GUILD_GET_RANKS, 0x32000E },
            { Opcode.CMSG_GUILD_GET_ROSTER, 0x320014 },
            { Opcode.CMSG_GUILD_INVITE_BY_NAME, 0x390034 },
            { Opcode.CMSG_GUILD_LEAVE, 0x320003 },
            { Opcode.CMSG_GUILD_NEWS_UPDATE_STICKY, 0x32000F },
            { Opcode.CMSG_GUILD_OFFICER_REMOVE_MEMBER, 0x320004 },
            { Opcode.CMSG_GUILD_PERMISSIONS_QUERY, 0x32001B },
            { Opcode.CMSG_GUILD_PROMOTE_MEMBER, 0x320000 },
            { Opcode.CMSG_GUILD_QUERY_MEMBER_RECIPES, 0x32000A }, // NYI
            { Opcode.CMSG_GUILD_QUERY_NEWS, 0x32000D },
            { Opcode.CMSG_GUILD_QUERY_RECIPES, 0x32000B }, // NYI
            { Opcode.CMSG_GUILD_REPLACE_GUILD_MASTER, 0x32001F },
            { Opcode.CMSG_GUILD_SET_ACHIEVEMENT_TRACKING, 0x320010 },
            { Opcode.CMSG_GUILD_SET_FOCUSED_ACHIEVEMENT, 0x320011 },
            { Opcode.CMSG_GUILD_SET_GUILD_MASTER, 0x3900FD },
            { Opcode.CMSG_GUILD_SET_MEMBER_NOTE, 0x320013 },
            { Opcode.CMSG_GUILD_SET_RANK_PERMISSIONS, 0x320008 },
            { Opcode.CMSG_GUILD_SHIFT_RANK, 0x320007 },
            { Opcode.CMSG_GUILD_UPDATE_INFO_TEXT, 0x320016 },
            { Opcode.CMSG_GUILD_UPDATE_MOTD_TEXT, 0x320015 },
            { Opcode.CMSG_HEARTH_AND_RESURRECT, 0x35009A },
            { Opcode.CMSG_HOTFIX_REQUEST, 0x390011 },
            { Opcode.CMSG_IGNORE_TRADE, 0x340004 },
            { Opcode.CMSG_INITIATE_ROLE_POLL, 0x390006 },
            { Opcode.CMSG_INITIATE_TRADE, 0x340001 },
            { Opcode.CMSG_INSPECT, 0x3500BF },
            { Opcode.CMSG_INSPECT_PVP, 0x3900D0 }, // NYI
            { Opcode.CMSG_INSTANCE_LOCK_RESPONSE, 0x35009F },
            { Opcode.CMSG_ITEM_PURCHASE_REFUND, 0x3500C6 },
            { Opcode.CMSG_ITEM_TEXT_QUERY, 0x340180 },
            { Opcode.CMSG_JOIN_RATED_BATTLEGROUND, 0x340024 }, // NYI
            { Opcode.CMSG_KEEP_ALIVE, 0x3900AB },
            { Opcode.CMSG_KEYBOUND_OVERRIDE, 0x3400D4 },
            { Opcode.CMSG_LATENCY_REPORT, 0x3A000D }, // NYI
            { Opcode.CMSG_LEARN_PREVIEW_TALENTS, 0x3500E9 },
            { Opcode.CMSG_LEARN_PREVIEW_TALENTS_PET, 0x3500EB }, // NYI
            { Opcode.CMSG_LEARN_TALENT, 0x3500E8 },
            { Opcode.CMSG_LEAVE_GROUP, 0x390075 },
            { Opcode.CMSG_LFG_LIST_APPLY_TO_GROUP, 0x39003B }, // NYI
            { Opcode.CMSG_LFG_LIST_CANCEL_APPLICATION, 0x39003C }, // NYI
            { Opcode.CMSG_LFG_LIST_DECLINE_APPLICANT, 0x39003D }, // NYI
            { Opcode.CMSG_LFG_LIST_GET_STATUS, 0x390039 },
            { Opcode.CMSG_LFG_LIST_INVITE_APPLICANT, 0x39003E }, // NYI
            { Opcode.CMSG_LFG_LIST_INVITE_RESPONSE, 0x39003F }, // NYI
            { Opcode.CMSG_LFG_LIST_JOIN, 0x3401AB }, // NYI
            { Opcode.CMSG_LFG_LIST_LEAVE, 0x390038 }, // NYI
            { Opcode.CMSG_LFG_LIST_SEARCH, 0x39003A }, // NYI
            { Opcode.CMSG_LFG_LIST_SET_ROLES, 0x3401BD }, // NYI
            { Opcode.CMSG_LFG_LIST_UPDATE_REQUEST, 0x3401AC }, // NYI
            { Opcode.CMSG_LIST_INVENTORY, 0x35002B },
            { Opcode.CMSG_LIVE_REGION_ACCOUNT_RESTORE, 0x3900EF }, // NYI
            { Opcode.CMSG_LIVE_REGION_CHARACTER_COPY, 0x3900EE }, // NYI
            { Opcode.CMSG_LIVE_REGION_GET_ACCOUNT_CHARACTER_LIST, 0x3900ED }, // NYI
            { Opcode.CMSG_LOADING_SCREEN_NOTIFY, 0x390024 },
            { Opcode.CMSG_LOGOUT_CANCEL, 0x35006B },
            { Opcode.CMSG_LOGOUT_INSTANT, 0x35006C }, // NYI
            { Opcode.CMSG_LOGOUT_REQUEST, 0x35006A },
            { Opcode.CMSG_LOG_DISCONNECT, 0x3A0005 },
            { Opcode.CMSG_LOG_STREAMING_ERROR, 0x3A0009 }, // NYI
            { Opcode.CMSG_LOOT_ITEM, 0x3400BF },
            { Opcode.CMSG_LOOT_MONEY, 0x3400BE },
            { Opcode.CMSG_LOOT_RELEASE, 0x3400C1 },
            { Opcode.CMSG_LOOT_ROLL, 0x3400C2 },
            { Opcode.CMSG_LOOT_UNIT, 0x3400BD },
            { Opcode.CMSG_LOW_LEVEL_RAID1, 0x3500A6 }, // NYI
            { Opcode.CMSG_LOW_LEVEL_RAID2, 0x3900CE }, // NYI
            { Opcode.CMSG_MAIL_CREATE_TEXT_ITEM, 0x3500D1 },
            { Opcode.CMSG_MAIL_DELETE, 0x3400D6 },
            { Opcode.CMSG_MAIL_GET_LIST, 0x3500CC },
            { Opcode.CMSG_MAIL_MARK_AS_READ, 0x3500D0 },
            { Opcode.CMSG_MAIL_RETURN_TO_SENDER, 0x390081 },
            { Opcode.CMSG_MAIL_TAKE_ITEM, 0x3500CE },
            { Opcode.CMSG_MAIL_TAKE_MONEY, 0x3500CD },
            { Opcode.CMSG_MAKE_CONTITIONAL_APPEARANCE_PERMANENT, 0x3400D9 }, // NYI
            { Opcode.CMSG_MASTER_LOOT_ITEM, 0x3400C0 },
            { Opcode.CMSG_MERGE_GUILD_BANK_ITEM_WITH_GUILD_BANK_ITEM, 0x35004A },
            { Opcode.CMSG_MERGE_GUILD_BANK_ITEM_WITH_ITEM, 0x350047 },
            { Opcode.CMSG_MERGE_ITEM_WITH_GUILD_BANK_ITEM, 0x350045 },
            { Opcode.CMSG_MINIMAP_PING, 0x390077 },
            { Opcode.CMSG_MISSILE_TRAJECTORY_COLLISION, 0x340036 },
            { Opcode.CMSG_MOUNT_CLEAR_FANFARE, 0x2E0003 }, // NYI
            { Opcode.CMSG_MOUNT_SET_FAVORITE, 0x39005C },
            { Opcode.CMSG_MOUNT_SPECIAL_ANIM, 0x340137 },
            { Opcode.CMSG_MOVE_ADD_IMPULSE_ACK, 0x37006D }, // NYI
            { Opcode.CMSG_MOVE_APPLY_INERTIA_ACK, 0x37006B }, // NYI
            { Opcode.CMSG_MOVE_APPLY_MOVEMENT_FORCE_ACK, 0x370031 },
            { Opcode.CMSG_MOVE_CHANGE_TRANSPORT, 0x37004C },
            { Opcode.CMSG_MOVE_CHANGE_VEHICLE_SEATS, 0x370051 },
            { Opcode.CMSG_MOVE_COLLISION_DISABLE_ACK, 0x370056 },
            { Opcode.CMSG_MOVE_COLLISION_ENABLE_ACK, 0x370057 },
            { Opcode.CMSG_MOVE_DISMISS_VEHICLE, 0x370050 },
            { Opcode.CMSG_MOVE_DOUBLE_JUMP, 0x370007 },
            { Opcode.CMSG_MOVE_ENABLE_DOUBLE_JUMP_ACK, 0x37003A },
            { Opcode.CMSG_MOVE_ENABLE_FULL_SPEED_TURNING_ACK, 0x370081 }, // NYI
            { Opcode.CMSG_MOVE_ENABLE_SWIM_TO_FLY_TRANS_ACK, 0x370040 },
            { Opcode.CMSG_MOVE_FALL_LAND, 0x370017 },
            { Opcode.CMSG_MOVE_FALL_RESET, 0x370035 },
            { Opcode.CMSG_MOVE_FEATHER_FALL_ACK, 0x370038 },
            { Opcode.CMSG_MOVE_FORCE_FLIGHT_BACK_SPEED_CHANGE_ACK, 0x37004B },
            { Opcode.CMSG_MOVE_FORCE_FLIGHT_SPEED_CHANGE_ACK, 0x37004A },
            { Opcode.CMSG_MOVE_FORCE_PITCH_RATE_CHANGE_ACK, 0x37004F },
            { Opcode.CMSG_MOVE_FORCE_ROOT_ACK, 0x37002A },
            { Opcode.CMSG_MOVE_FORCE_RUN_BACK_SPEED_CHANGE_ACK, 0x370028 },
            { Opcode.CMSG_MOVE_FORCE_RUN_SPEED_CHANGE_ACK, 0x370027 },
            { Opcode.CMSG_MOVE_FORCE_SWIM_BACK_SPEED_CHANGE_ACK, 0x37003E },
            { Opcode.CMSG_MOVE_FORCE_SWIM_SPEED_CHANGE_ACK, 0x370029 },
            { Opcode.CMSG_MOVE_FORCE_TURN_RATE_CHANGE_ACK, 0x37003F },
            { Opcode.CMSG_MOVE_FORCE_UNROOT_ACK, 0x37002B },
            { Opcode.CMSG_MOVE_FORCE_WALK_SPEED_CHANGE_ACK, 0x37003D },
            { Opcode.CMSG_MOVE_GRAVITY_DISABLE_ACK, 0x370052 },
            { Opcode.CMSG_MOVE_GRAVITY_ENABLE_ACK, 0x370053 },
            { Opcode.CMSG_MOVE_GUILD_BANK_ITEM, 0x350044 },
            { Opcode.CMSG_MOVE_HEARTBEAT, 0x37002C },
            { Opcode.CMSG_MOVE_HOVER_ACK, 0x37002F },
            { Opcode.CMSG_MOVE_INERTIA_DISABLE_ACK, 0x370054 },
            { Opcode.CMSG_MOVE_INERTIA_ENABLE_ACK, 0x370055 },
            { Opcode.CMSG_MOVE_INIT_ACTIVE_MOVER_COMPLETE, 0x370063 },
            { Opcode.CMSG_MOVE_JUMP, 0x370006 },
            { Opcode.CMSG_MOVE_KNOCK_BACK_ACK, 0x37002E },
            { Opcode.CMSG_MOVE_REMOVE_INERTIA_ACK, 0x37006C }, // NYI
            { Opcode.CMSG_MOVE_REMOVE_MOVEMENT_FORCES, 0x370033 },
            { Opcode.CMSG_MOVE_REMOVE_MOVEMENT_FORCE_ACK, 0x370032 },
            { Opcode.CMSG_MOVE_SET_ADV_FLY, 0x37006F }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_ADD_IMPULSE_MAX_SPEED_ACK, 0x370075 }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_AIR_FRICTION_ACK, 0x370070 }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_BANKING_RATE_ACK, 0x370076 }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_DOUBLE_JUMP_VEL_MOD_ACK, 0x370073 }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_GLIDE_START_MIN_HEIGHT_ACK, 0x370074 }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_LAUNCH_SPEED_COEFFICIENT_ACK, 0x37007D }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_LIFT_COEFFICIENT_ACK, 0x370072 }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_MAX_VEL_ACK, 0x370071 }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_OVER_MAX_DECELERATION_ACK, 0x37007B }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_PITCHING_RATE_DOWN_ACK, 0x370077 }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_PITCHING_RATE_UP_ACK, 0x370078 }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_SURFACE_FRICTION_ACK, 0x37007A }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_TURN_VELOCITY_THRESHOLD_ACK, 0x370079 }, // NYI
            { Opcode.CMSG_MOVE_SET_CAN_ADV_FLY_ACK, 0x37006E }, // NYI
            { Opcode.CMSG_MOVE_SET_CAN_FLY_ACK, 0x370043 },
            { Opcode.CMSG_MOVE_SET_CAN_TURN_WHILE_FALLING_ACK, 0x370041 },
            { Opcode.CMSG_MOVE_SET_COLLISION_HEIGHT_ACK, 0x370058 },
            { Opcode.CMSG_MOVE_SET_FACING, 0x370025 },
            { Opcode.CMSG_MOVE_SET_FACING_HEARTBEAT, 0x37007C },
            { Opcode.CMSG_MOVE_SET_FLY, 0x370045 },
            { Opcode.CMSG_MOVE_SET_IGNORE_MOVEMENT_FORCES_ACK, 0x370042 },
            { Opcode.CMSG_MOVE_SET_MOD_MOVEMENT_FORCE_MAGNITUDE_ACK, 0x37005F },
            { Opcode.CMSG_MOVE_SET_PITCH, 0x370026 },
            { Opcode.CMSG_MOVE_SET_RUN_MODE, 0x37000E },
            { Opcode.CMSG_MOVE_SET_TURN_RATE_CHEAT, 0x370022 },
            { Opcode.CMSG_MOVE_SET_VEHICLE_REC_ID_ACK, 0x370030 },
            { Opcode.CMSG_MOVE_SET_WALK_MODE, 0x37000F },
            { Opcode.CMSG_MOVE_SPLINE_DONE, 0x370034 },
            { Opcode.CMSG_MOVE_START_ASCEND, 0x370046 },
            { Opcode.CMSG_MOVE_START_BACKWARD, 0x370001 },
            { Opcode.CMSG_MOVE_START_DESCEND, 0x37004D },
            { Opcode.CMSG_MOVE_START_FORWARD, 0x370000 },
            { Opcode.CMSG_MOVE_START_PITCH_DOWN, 0x37000C },
            { Opcode.CMSG_MOVE_START_PITCH_UP, 0x37000B },
            { Opcode.CMSG_MOVE_START_STRAFE_LEFT, 0x370003 },
            { Opcode.CMSG_MOVE_START_STRAFE_RIGHT, 0x370004 },
            { Opcode.CMSG_MOVE_START_SWIM, 0x370018 },
            { Opcode.CMSG_MOVE_START_TURN_LEFT, 0x370008 },
            { Opcode.CMSG_MOVE_START_TURN_RIGHT, 0x370009 },
            { Opcode.CMSG_MOVE_STOP, 0x370002 },
            { Opcode.CMSG_MOVE_STOP_ASCEND, 0x370047 },
            { Opcode.CMSG_MOVE_STOP_PITCH, 0x37000D },
            { Opcode.CMSG_MOVE_STOP_STRAFE, 0x370005 },
            { Opcode.CMSG_MOVE_STOP_SWIM, 0x370019 },
            { Opcode.CMSG_MOVE_STOP_TURN, 0x37000A },
            { Opcode.CMSG_MOVE_TELEPORT_ACK, 0x370016 },
            { Opcode.CMSG_MOVE_TIME_SKIPPED, 0x370037 },
            { Opcode.CMSG_MOVE_UPDATE_FALL_SPEED, 0x370036 },
            { Opcode.CMSG_MOVE_WATER_WALK_ACK, 0x370039 },
            { Opcode.CMSG_NEXT_CINEMATIC_CAMERA, 0x3500DA },
            { Opcode.CMSG_OBJECT_UPDATE_FAILED, 0x34002C },
            { Opcode.CMSG_OBJECT_UPDATE_RESCUED, 0x34002D },
            { Opcode.CMSG_OFFER_PETITION, 0x3401B8 },
            { Opcode.CMSG_OPENING_CINEMATIC, 0x3500D9 },
            { Opcode.CMSG_OPEN_ITEM, 0x340181 },
            { Opcode.CMSG_OPT_OUT_OF_LOOT, 0x350089 },
            { Opcode.CMSG_OVERRIDE_SCREEN_FLASH, 0x3500B2 },
            { Opcode.CMSG_PARTY_INVITE, 0x390030 },
            { Opcode.CMSG_PARTY_INVITE_RESPONSE, 0x390032 },
            { Opcode.CMSG_PARTY_UNINVITE, 0x390073 },
            { Opcode.CMSG_PETITION_BUY, 0x350052 },
            { Opcode.CMSG_PETITION_RENAME_GUILD, 0x3900FE },
            { Opcode.CMSG_PETITION_SHOW_LIST, 0x350051 },
            { Opcode.CMSG_PETITION_SHOW_SIGNATURES, 0x350053 },
            { Opcode.CMSG_PET_ABANDON, 0x350017 },
            { Opcode.CMSG_PET_ACTION, 0x350015 },
            { Opcode.CMSG_PET_CANCEL_AURA, 0x350018 },
            { Opcode.CMSG_PET_CAST_SPELL, 0x340154 },
            { Opcode.CMSG_PET_LEARN_TALENT, 0x3500EA }, // NYI
            { Opcode.CMSG_PET_RENAME, 0x3900B0 },
            { Opcode.CMSG_PET_SET_ACTION, 0x350014 },
            { Opcode.CMSG_PET_SPELL_AUTOCAST, 0x350019 },
            { Opcode.CMSG_PET_STOP_ATTACK, 0x350016 },
            { Opcode.CMSG_PING, 0x3A0004 },
            { Opcode.CMSG_PLAYER_LOGIN, 0x390016 },
            { Opcode.CMSG_PUSH_QUEST_TO_PARTY, 0x350029 },
            { Opcode.CMSG_PVP_LOG_DATA, 0x340028 },
            { Opcode.CMSG_QUERY_ARENA_TEAM, 0x3900D1 }, // NYI
            { Opcode.CMSG_QUERY_BATTLE_PET_NAME, 0x34012D },
            { Opcode.CMSG_QUERY_CORPSE_LOCATION_FROM_CLIENT, 0x39008C },
            { Opcode.CMSG_QUERY_CORPSE_TRANSPORT, 0x39008D },
            { Opcode.CMSG_QUERY_COUNTDOWN_TIMER, 0x340053 },
            { Opcode.CMSG_QUERY_CREATURE, 0x340127 },
            { Opcode.CMSG_QUERY_GAME_OBJECT, 0x340128 },
            { Opcode.CMSG_QUERY_GUILD_INFO, 0x3900B6 },
            { Opcode.CMSG_QUERY_INSPECT_ACHIEVEMENTS, 0x350093 },
            { Opcode.CMSG_QUERY_NEXT_MAIL_TIME, 0x3500CF },
            { Opcode.CMSG_QUERY_NPC_TEXT, 0x340129 },
            { Opcode.CMSG_QUERY_PAGE_TEXT, 0x34012B },
            { Opcode.CMSG_QUERY_PETITION, 0x34012E },
            { Opcode.CMSG_QUERY_PET_NAME, 0x34012C },
            { Opcode.CMSG_QUERY_PLAYER_NAMES, 0x3A000E },
            { Opcode.CMSG_QUERY_PLAYER_NAMES_FOR_COMMUNITY, 0x3A000C }, // NYI
            { Opcode.CMSG_QUERY_PLAYER_NAME_BY_COMMUNITY_ID, 0x3A000B }, // NYI
            { Opcode.CMSG_QUERY_QUEST_COMPLETION_NPCS, 0x340020 },
            { Opcode.CMSG_QUERY_QUEST_INFO, 0x34012A },
            { Opcode.CMSG_QUERY_QUEST_ITEM_USABILITY, 0x340021 }, // NYI
            { Opcode.CMSG_QUERY_REALM_NAME, 0x3900B5 },
            { Opcode.CMSG_QUERY_SCENARIO_POI, 0x390082 },
            { Opcode.CMSG_QUERY_TIME, 0x350069 },
            { Opcode.CMSG_QUERY_TREASURE_PICKER, 0x340197 }, // NYI
            { Opcode.CMSG_QUERY_VOID_STORAGE, 0x34004C },
            { Opcode.CMSG_QUEST_CONFIRM_ACCEPT, 0x350028 },
            { Opcode.CMSG_QUEST_GIVER_ACCEPT_QUEST, 0x350022 },
            { Opcode.CMSG_QUEST_GIVER_CHOOSE_REWARD, 0x350024 },
            { Opcode.CMSG_QUEST_GIVER_CLOSE_QUEST, 0x3500DF },
            { Opcode.CMSG_QUEST_GIVER_COMPLETE_QUEST, 0x350023 },
            { Opcode.CMSG_QUEST_GIVER_HELLO, 0x350020 },
            { Opcode.CMSG_QUEST_GIVER_QUERY_QUEST, 0x350021 },
            { Opcode.CMSG_QUEST_GIVER_REQUEST_REWARD, 0x350025 },
            { Opcode.CMSG_QUEST_GIVER_STATUS_MULTIPLE_QUERY, 0x350027 },
            { Opcode.CMSG_QUEST_GIVER_STATUS_QUERY, 0x350026 },
            { Opcode.CMSG_QUEST_GIVER_STATUS_TRACKED_QUERY, 0x350106 },
            { Opcode.CMSG_QUEST_LOG_REMOVE_QUEST, 0x3500C4 },
            { Opcode.CMSG_QUEST_POI_QUERY, 0x3900DF },
            { Opcode.CMSG_QUEST_PUSH_RESULT, 0x35002A },
            { Opcode.CMSG_QUEUED_MESSAGES_END, 0x3A0008 },
            { Opcode.CMSG_QUICK_JOIN_AUTO_ACCEPT_REQUESTS, 0x390139 }, // NYI
            { Opcode.CMSG_QUICK_JOIN_REQUEST_INVITE, 0x390138 }, // NYI
            { Opcode.CMSG_QUICK_JOIN_RESPOND_TO_INVITE, 0x390137 }, // NYI
            { Opcode.CMSG_RAF_CLAIM_ACTIVITY_REWARD, 0x350097 },
            { Opcode.CMSG_RAF_CLAIM_NEXT_REWARD, 0x390157 },
            { Opcode.CMSG_RAF_GENERATE_RECRUITMENT_LINK, 0x390159 },
            { Opcode.CMSG_RANDOM_ROLL, 0x390080 },
            { Opcode.CMSG_READY_CHECK_RESPONSE, 0x39005F },
            { Opcode.CMSG_READ_ITEM, 0x340182 },
            { Opcode.CMSG_RECLAIM_CORPSE, 0x35006E },
            { Opcode.CMSG_REFORGE_ITEM, 0x340000 },
            { Opcode.CMSG_REMOVE_GLYPH, 0x3401BB }, // NYI
            { Opcode.CMSG_REMOVE_NEW_ITEM, 0x34019A },
            { Opcode.CMSG_REMOVE_RAF_RECRUIT, 0x39015A },
            { Opcode.CMSG_REORDER_CHARACTERS, 0x390015 },
            { Opcode.CMSG_REPAIR_ITEM, 0x35007F },
            { Opcode.CMSG_REPOP_REQUEST, 0x3500BC },
            { Opcode.CMSG_REPORT_CLIENT_VARIABLES, 0x390133 }, // NYI
            { Opcode.CMSG_REPORT_ENABLED_ADDONS, 0x390132 }, // NYI
            { Opcode.CMSG_REPORT_KEYBINDING_EXECUTION_COUNTS, 0x390134 }, // NYI
            { Opcode.CMSG_REPORT_PVP_PLAYER_AFK, 0x350087 },
            { Opcode.CMSG_REPORT_SERVER_LAG, 0x3401B6 }, // NYI
            { Opcode.CMSG_REQUEST_ACCOUNT_DATA, 0x3900C1 },
            { Opcode.CMSG_REQUEST_AREA_POI_UPDATE, 0x340199 }, // NYI
            { Opcode.CMSG_REQUEST_BATTLEFIELD_STATUS, 0x390008 },
            { Opcode.CMSG_REQUEST_CEMETERY_LIST, 0x340022 },
            { Opcode.CMSG_REQUEST_CROWD_CONTROL_SPELL, 0x3500C0 }, // NYI
            { Opcode.CMSG_REQUEST_GUILD_PARTY_STATE, 0x340052 },
            { Opcode.CMSG_REQUEST_GUILD_REWARDS_LIST, 0x340051 },
            { Opcode.CMSG_REQUEST_HONOR_STATS, 0x340027 }, // NYI
            { Opcode.CMSG_REQUEST_LFG_LIST_BLACKLIST, 0x34015E },
            { Opcode.CMSG_REQUEST_PARTY_JOIN_UPDATES, 0x390023 },
            { Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS, 0x39007F },
            { Opcode.CMSG_REQUEST_PET_INFO, 0x35001A },
            { Opcode.CMSG_REQUEST_PLAYED_TIME, 0x340131 },
            { Opcode.CMSG_REQUEST_PVP_REWARDS, 0x34003F },
            { Opcode.CMSG_REQUEST_RAID_INFO, 0x3900FF },
            { Opcode.CMSG_REQUEST_RATED_PVP_INFO, 0x39000F },
            { Opcode.CMSG_REQUEST_SCHEDULED_PVP_INFO, 0x340040 }, // NYI
            { Opcode.CMSG_REQUEST_STABLED_PETS, 0x35001B },
            { Opcode.CMSG_REQUEST_STORE_FRONT_INFO_UPDATE, 0x2E001D },
            { Opcode.CMSG_REQUEST_VEHICLE_EXIT, 0x3400ED },
            { Opcode.CMSG_REQUEST_VEHICLE_NEXT_SEAT, 0x3400EF },
            { Opcode.CMSG_REQUEST_VEHICLE_PREV_SEAT, 0x3400EE },
            { Opcode.CMSG_REQUEST_VEHICLE_SWITCH_SEAT, 0x3400F0 },
            { Opcode.CMSG_REQUEST_WORLD_QUEST_UPDATE, 0x340198 },
            { Opcode.CMSG_RESET_INSTANCES, 0x390094 },
            { Opcode.CMSG_RESURRECT_RESPONSE, 0x3900AF },
            { Opcode.CMSG_RIDE_VEHICLE_INTERACT, 0x3400F1 },
            { Opcode.CMSG_SAVE_ACCOUNT_DATA_EXPORT, 0x390180 }, // NYI
            { Opcode.CMSG_SAVE_CUF_PROFILES, 0x340037 },
            { Opcode.CMSG_SAVE_EQUIPMENT_SET, 0x35009D },
            { Opcode.CMSG_SAVE_GUILD_EMBLEM, 0x340162 },
            { Opcode.CMSG_SCENE_PLAYBACK_CANCELED, 0x3400D1 },
            { Opcode.CMSG_SCENE_PLAYBACK_COMPLETE, 0x3400D0 },
            { Opcode.CMSG_SCENE_TRIGGER_EVENT, 0x3400D2 },
            { Opcode.CMSG_SEAMLESS_TRANSFER_COMPLETE, 0x3401CD }, // NYI
            { Opcode.CMSG_SELF_RES, 0x3500C7 },
            { Opcode.CMSG_SELL_ITEM, 0x35002C },
            { Opcode.CMSG_SEND_CONTACT_LIST, 0x390104 },
            { Opcode.CMSG_SEND_MAIL, 0x390026 },
            { Opcode.CMSG_SEND_TEXT_EMOTE, 0x350012 },
            { Opcode.CMSG_SERVER_TIME_OFFSET_REQUEST, 0x3900C9 },
            { Opcode.CMSG_SET_ACTION_BAR_TOGGLES, 0x3500C8 },
            { Opcode.CMSG_SET_ACTION_BUTTON, 0x3500F4 },
            { Opcode.CMSG_SET_ACTIVE_MOVER, 0x370059 },
            { Opcode.CMSG_SET_ADVANCED_COMBAT_LOGGING, 0x34016E },
            { Opcode.CMSG_SET_AMMO, 0x3500F5 }, // NYI
            { Opcode.CMSG_SET_ASSISTANT_LEADER, 0x39007B },
            { Opcode.CMSG_SET_CONTACT_NOTES, 0x390107 },
            { Opcode.CMSG_SET_CURRENCY_FLAGS, 0x340015 }, // NYI
            { Opcode.CMSG_SET_DIFFICULTY_ID, 0x3400D3 }, // NYI
            { Opcode.CMSG_SET_DUNGEON_DIFFICULTY, 0x3900AE },
            { Opcode.CMSG_SET_EVERYONE_IS_ASSISTANT, 0x390046 },
            { Opcode.CMSG_SET_FACTION_AT_WAR, 0x350071 },
            { Opcode.CMSG_SET_FACTION_INACTIVE, 0x350073 },
            { Opcode.CMSG_SET_FACTION_NOT_AT_WAR, 0x350072 },
            { Opcode.CMSG_SET_GAME_EVENT_DEBUG_VIEW_STATE, 0x340062 }, // NYI
            { Opcode.CMSG_SET_INSERT_ITEMS_LEFT_TO_RIGHT, 0x340185 }, // NYI
            { Opcode.CMSG_SET_LOOT_METHOD, 0x390074 },
            { Opcode.CMSG_SET_PARTY_ASSIGNMENT, 0x39007D },
            { Opcode.CMSG_SET_PARTY_LEADER, 0x390076 },
            { Opcode.CMSG_SET_PET_SLOT, 0x340014 },
            { Opcode.CMSG_SET_PLAYER_DECLINED_NAMES, 0x3900B4 },
            { Opcode.CMSG_SET_PREFERRED_CEMETERY, 0x340023 }, // NYI
            { Opcode.CMSG_SET_PRIMARY_TALENT_TREE, 0x3500ED },
            { Opcode.CMSG_SET_PVP, 0x340166 },
            { Opcode.CMSG_SET_RAID_DIFFICULTY, 0x390110 },
            { Opcode.CMSG_SET_ROLE, 0x390005 },
            { Opcode.CMSG_SET_SAVED_INSTANCE_EXTEND, 0x3900B2 },
            { Opcode.CMSG_SET_SELECTION, 0x3500BE },
            { Opcode.CMSG_SET_SHEATHED, 0x350013 },
            { Opcode.CMSG_SET_TAXI_BENCHMARK_MODE, 0x350086 },
            { Opcode.CMSG_SET_TITLE, 0x340135 },
            { Opcode.CMSG_SET_TRADE_GOLD, 0x34000A },
            { Opcode.CMSG_SET_TRADE_ITEM, 0x340008 },
            { Opcode.CMSG_SET_WATCHED_FACTION, 0x350074 },
            { Opcode.CMSG_SHOWING_CLOAK, 0x350104 }, // NYI
            { Opcode.CMSG_SHOWING_HELM, 0x350103 }, // NYI
            { Opcode.CMSG_SHOW_TRADE_SKILL, 0x3900F7 }, // NYI
            { Opcode.CMSG_SIGN_PETITION, 0x3500C9 },
            { Opcode.CMSG_SILENCE_PARTY_TALKER, 0x39007E }, // NYI
            { Opcode.CMSG_SOCIAL_CONTRACT_REQUEST, 0x39017B },
            { Opcode.CMSG_SOCKET_GEMS, 0x35007E },
            { Opcode.CMSG_SPAWN_TRACKING_UPDATE, 0x340149 }, // NYI
            { Opcode.CMSG_SPELL_CLICK, 0x35001F },
            { Opcode.CMSG_SPIRIT_HEALER_ACTIVATE, 0x350039 },
            { Opcode.CMSG_SPLIT_GUILD_BANK_ITEM, 0x35004B },
            { Opcode.CMSG_SPLIT_GUILD_BANK_ITEM_TO_INVENTORY, 0x350048 },
            { Opcode.CMSG_SPLIT_ITEM, 0x360008 },
            { Opcode.CMSG_SPLIT_ITEM_TO_GUILD_BANK, 0x350046 },
            { Opcode.CMSG_STABLE_PET, 0x340013 }, // NYI
            { Opcode.CMSG_STAND_STATE_CHANGE, 0x340035 },
            { Opcode.CMSG_START_SPECTATOR_WAR_GAME, 0x39000B }, // NYI
            { Opcode.CMSG_START_WAR_GAME, 0x39000A }, // NYI
            { Opcode.CMSG_STORE_GUILD_BANK_ITEM, 0x350041 },
            { Opcode.CMSG_SUBMIT_USER_FEEDBACK, 0x3900C0 },
            { Opcode.CMSG_SUMMON_RESPONSE, 0x390096 },
            { Opcode.CMSG_SUPPORT_TICKET_SUBMIT_BUG, 0x390071 }, // NYI
            { Opcode.CMSG_SUPPORT_TICKET_SUBMIT_COMPLAINT, 0x390070 },
            { Opcode.CMSG_SUPPORT_TICKET_SUBMIT_SUGGESTION, 0x390072 }, // NYI
            { Opcode.CMSG_SUSPEND_COMMS_ACK, 0x3A0000 },
            { Opcode.CMSG_SUSPEND_TOKEN_RESPONSE, 0x3A0006 },
            { Opcode.CMSG_SWAP_GUILD_BANK_ITEM_WITH_GUILD_BANK_ITEM, 0x350043 },
            { Opcode.CMSG_SWAP_INV_ITEM, 0x360007 },
            { Opcode.CMSG_SWAP_ITEM, 0x360006 },
            { Opcode.CMSG_SWAP_ITEM_WITH_GUILD_BANK_ITEM, 0x350042 },
            { Opcode.CMSG_SWAP_SUB_GROUPS, 0x390079 },
            { Opcode.CMSG_SWAP_VOID_ITEM, 0x34004E },
            { Opcode.CMSG_TABARD_VENDOR_ACTIVATE, 0x340163 },
            { Opcode.CMSG_TALK_TO_GOSSIP, 0x35001C },
            { Opcode.CMSG_TAXI_NODE_STATUS_QUERY, 0x350032 },
            { Opcode.CMSG_TAXI_QUERY_AVAILABLE_NODES, 0x350034 },
            { Opcode.CMSG_TAXI_REQUEST_EARLY_LANDING, 0x350036 },
            { Opcode.CMSG_TIME_ADJUSTMENT_RESPONSE, 0x37005D }, // NYI
            { Opcode.CMSG_TIME_SYNC_RESPONSE, 0x37005A },
            { Opcode.CMSG_TIME_SYNC_RESPONSE_DROPPED, 0x37005C }, // NYI
            { Opcode.CMSG_TIME_SYNC_RESPONSE_FAILED, 0x37005B }, // NYI
            { Opcode.CMSG_TOGGLE_DIFFICULTY, 0x390083 }, // NYI
            { Opcode.CMSG_TOGGLE_PVP, 0x340165 },
            { Opcode.CMSG_TOTEM_DESTROYED, 0x35008B },
            { Opcode.CMSG_TOY_CLEAR_FANFARE, 0x2E0004 },
            { Opcode.CMSG_TRAINER_BUY_SPELL, 0x350038 },
            { Opcode.CMSG_TRAINER_LIST, 0x350037 },
            { Opcode.CMSG_TRAITS_COMMIT_CONFIG, 0x3401BE }, // NYI
            { Opcode.CMSG_TRANSMOGRIFY_ITEMS, 0x340041 },
            { Opcode.CMSG_TURN_IN_PETITION, 0x3500CB },
            { Opcode.CMSG_TUTORIAL, 0x390111 },
            { Opcode.CMSG_UNACCEPT_TRADE, 0x340006 },
            { Opcode.CMSG_UNDELETE_CHARACTER, 0x390113 },
            { Opcode.CMSG_UNLEARN_SKILL, 0x350078 },
            { Opcode.CMSG_UNLOCK_VOID_STORAGE, 0x34004B },
            { Opcode.CMSG_UPDATE_AADC_STATUS, 0x39016C },
            { Opcode.CMSG_UPDATE_ACCOUNT_DATA, 0x3900C2 },
            { Opcode.CMSG_UPDATE_AREA_TRIGGER_VISUAL, 0x340157 }, // NYI
            { Opcode.CMSG_UPDATE_CLIENT_SETTINGS, 0x390090 }, // NYI
            { Opcode.CMSG_UPDATE_MISSILE_TRAJECTORY, 0x370060 },
            { Opcode.CMSG_UPDATE_RAID_TARGET, 0x39007C },
            { Opcode.CMSG_UPDATE_SPELL_VISUAL, 0x340156 }, // NYI
            { Opcode.CMSG_UPDATE_VAS_PURCHASE_STATES, 0x390128 },
            { Opcode.CMSG_USED_FOLLOW, 0x340032 }, // NYI
            { Opcode.CMSG_USE_CRITTER_ITEM, 0x3400F7 },
            { Opcode.CMSG_USE_EQUIPMENT_SET, 0x360001 },
            { Opcode.CMSG_USE_ITEM, 0x340151 },
            { Opcode.CMSG_USE_TOY, 0x340153 },
            { Opcode.CMSG_VAS_CHECK_TRANSFER_OK, 0x39013F },
            { Opcode.CMSG_VAS_GET_QUEUE_MINUTES, 0x39013E },
            { Opcode.CMSG_VAS_GET_SERVICE_STATUS, 0x39013D },
            { Opcode.CMSG_VIOLENCE_LEVEL, 0x340030 },
            { Opcode.CMSG_VOICE_CHANNEL_STT_TOKEN_REQUEST, 0x390143 }, // NYI
            { Opcode.CMSG_VOICE_CHAT_JOIN_CHANNEL, 0x390144 }, // NYI
            { Opcode.CMSG_VOICE_CHAT_LOGIN, 0x390142 }, // NYI
            { Opcode.CMSG_VOID_STORAGE_TRANSFER, 0x34004D },
            { Opcode.CMSG_WARDEN3_DATA, 0x390018 },
            { Opcode.CMSG_WHO, 0x3900AD },
            { Opcode.CMSG_WHO_IS, 0x3900AC },
            { Opcode.CMSG_WORLD_PORT_RESPONSE, 0x390025 },
            { Opcode.CMSG_WRAP_ITEM, 0x360000 },
        };

        private static readonly BiDictionary<Opcode, int> ServerOpcodes = new()
        {
            { Opcode.SMSG_ABORT_NEW_WORLD, 0x3B0031 },
            { Opcode.SMSG_ACCOUNT_CHARACTER_CURRENCY_LISTS, 0x3B0336 }, // NYI
            { Opcode.SMSG_ACCOUNT_CRITERIA_UPDATE, 0x3B02EB },
            { Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x3B01A6 },
            { Opcode.SMSG_ACCOUNT_EXPORT_RESPONSE, 0x3B0327 }, // NYI
            { Opcode.SMSG_ACCOUNT_HEIRLOOM_UPDATE, 0x3B0050 },
            { Opcode.SMSG_ACCOUNT_MOUNT_REMOVED, 0x3B0048 }, // NYI
            { Opcode.SMSG_ACCOUNT_MOUNT_UPDATE, 0x3B0047 },
            { Opcode.SMSG_ACCOUNT_NOTIFICATIONS_RESPONSE, 0x3B0305 }, // NYI
            { Opcode.SMSG_ACCOUNT_TOY_UPDATE, 0x3B0049 },
            { Opcode.SMSG_ACCOUNT_TRANSMOG_SET_FAVORITES_UPDATE, 0x3B004D }, // NYI
            { Opcode.SMSG_ACCOUNT_TRANSMOG_UPDATE, 0x3B004C }, // NYI
            { Opcode.SMSG_ACHIEVEMENT_DELETED, 0x3B0184 }, // NYI
            { Opcode.SMSG_ACHIEVEMENT_EARNED, 0x3B00DC },
            { Opcode.SMSG_ACTIVATE_TAXI_REPLY, 0x3B0119 },
            { Opcode.SMSG_ACTIVE_GLYPHS, 0x510043 },
            { Opcode.SMSG_ADDON_LIST_REQUEST, 0x3B00DB }, // NYI
            { Opcode.SMSG_ADD_BATTLENET_FRIEND_RESPONSE, 0x3B00D6 }, // NYI
            { Opcode.SMSG_ADD_ITEM_PASSIVE, 0x3B0043 }, // NYI
            { Opcode.SMSG_ADD_LOSS_OF_CONTROL, 0x3B010C },
            { Opcode.SMSG_ADD_RUNE_POWER, 0x3B0154 },
            { Opcode.SMSG_ADJUST_SPLINE_DURATION, 0x3B0069 },
            { Opcode.SMSG_AE_LOOT_TARGETS, 0x3B00B1 },
            { Opcode.SMSG_AE_LOOT_TARGET_ACK, 0x3B00B2 },
            { Opcode.SMSG_AI_REACTION, 0x3B0151 },
            { Opcode.SMSG_ALLIED_RACE_DETAILS, 0x3B0295 },
            { Opcode.SMSG_ALL_ACCOUNT_CRITERIA, 0x3B0005 },
            { Opcode.SMSG_ALL_ACHIEVEMENT_DATA, 0x3B0004 },
            { Opcode.SMSG_ALL_GUILD_ACHIEVEMENTS, 0x470000 },
            { Opcode.SMSG_ARCHAEOLOGY_SURVERY_CAST, 0x3B001D },
            { Opcode.SMSG_AREA_POI_UPDATE_RESPONSE, 0x4E0018 },
            { Opcode.SMSG_AREA_SPIRIT_HEALER_TIME, 0x3B01DB },
            { Opcode.SMSG_AREA_TRIGGER_DENIED, 0x3C0009 },
            { Opcode.SMSG_AREA_TRIGGER_FORCE_SET_POSITION_AND_FACING, 0x3C0006 }, // NYI
            { Opcode.SMSG_AREA_TRIGGER_MESSAGE, 0x3B0303 },
            { Opcode.SMSG_AREA_TRIGGER_NO_CORPSE, 0x3B01B2 },
            { Opcode.SMSG_AREA_TRIGGER_RE_PATH, 0x3C0003 },
            { Opcode.SMSG_AREA_TRIGGER_RE_SHAPE, 0x3C0008 }, // NYI
            { Opcode.SMSG_AREA_TRIGGER_UNATTACH, 0x3C0007 }, // NYI
            { Opcode.SMSG_ARENA_CROWD_CONTROL_SPELL_RESULT, 0x3B00CB }, // NYI
            { Opcode.SMSG_ARENA_PREP_OPPONENT_SPECIALIZATIONS, 0x3B00E1 }, // NYI
            { Opcode.SMSG_ARENA_TEAM_COMMAND_RESULT, 0x3B01FE }, // NYI
            { Opcode.SMSG_ARENA_TEAM_EVENT, 0x3B01FD }, // NYI
            { Opcode.SMSG_ARENA_TEAM_INVITE, 0x3B01FC }, // NYI
            { Opcode.SMSG_ARENA_TEAM_ROSTER, 0x3B01FB }, // NYI
            { Opcode.SMSG_ARENA_TEAM_STATS, 0x3B01FF }, // NYI
            { Opcode.SMSG_ATTACKER_STATE_UPDATE, 0x410030 },
            { Opcode.SMSG_ATTACK_START, 0x41001B },
            { Opcode.SMSG_ATTACK_STOP, 0x41001C },
            { Opcode.SMSG_ATTACK_SWING_ERROR, 0x41002A },
            { Opcode.SMSG_ATTACK_SWING_LANDED_LOG, 0x41002B }, // NYI
            { Opcode.SMSG_AUCTIONABLE_TOKEN_AUCTION_SOLD, 0x3B0271 }, // NYI
            { Opcode.SMSG_AUCTIONABLE_TOKEN_SELL_AT_MARKET_PRICE_RESPONSE, 0x3B0270 }, // NYI
            { Opcode.SMSG_AUCTIONABLE_TOKEN_SELL_CONFIRM_REQUIRED, 0x3B026F }, // NYI
            { Opcode.SMSG_AUCTION_CLOSED_NOTIFICATION, 0x3B018F },
            { Opcode.SMSG_AUCTION_COMMAND_RESULT, 0x3B018C },
            { Opcode.SMSG_AUCTION_DISABLE_NEW_POSTINGS, 0x3B0322 },
            { Opcode.SMSG_AUCTION_FAVORITE_LIST, 0x3B02F2 },
            { Opcode.SMSG_AUCTION_GET_COMMODITY_QUOTE_RESULT, 0x3B02EA },
            { Opcode.SMSG_AUCTION_HELLO_RESPONSE, 0x3B018A },
            { Opcode.SMSG_AUCTION_LIST_BIDDED_ITEMS_RESULT, 0x3B02E9 },
            { Opcode.SMSG_AUCTION_LIST_BUCKETS_RESULT, 0x3B02E5 },
            { Opcode.SMSG_AUCTION_LIST_ITEMS_RESULT, 0x3B02E6 },
            { Opcode.SMSG_AUCTION_LIST_OWNED_ITEMS_RESULT, 0x3B02E8 },
            { Opcode.SMSG_AUCTION_OUTBID_NOTIFICATION, 0x3B018E },
            { Opcode.SMSG_AUCTION_OWNER_BID_NOTIFICATION, 0x3B0190 },
            { Opcode.SMSG_AUCTION_REPLICATE_RESPONSE, 0x3B018B },
            { Opcode.SMSG_AUCTION_WON_NOTIFICATION, 0x3B018D },
            { Opcode.SMSG_AURA_POINTS_DEPLETED, 0x510012 }, // NYI
            { Opcode.SMSG_AURA_UPDATE, 0x510011 },
            { Opcode.SMSG_AUTH_CHALLENGE, 0x420000 },
            { Opcode.SMSG_AUTH_FAILED, 0x3B0000 }, // NYI
            { Opcode.SMSG_AUTH_RESPONSE, 0x3B0001 },
            { Opcode.SMSG_AVAILABLE_HOTFIXES, 0x3F0001 },
            { Opcode.SMSG_BAG_CLEANUP_FINISHED, 0x520007 },
            { Opcode.SMSG_BARBER_SHOP_RESULT, 0x3B015A },
            { Opcode.SMSG_BATCH_PRESENCE_SUBSCRIPTION, 0x3B02C9 }, // NYI
            { Opcode.SMSG_BATTLEFIELD_LIST, 0x410005 },
            { Opcode.SMSG_BATTLEFIELD_PORT_DENIED, 0x41000B },
            { Opcode.SMSG_BATTLEFIELD_STATUS_ACTIVE, 0x410001 },
            { Opcode.SMSG_BATTLEFIELD_STATUS_FAILED, 0x410004 },
            { Opcode.SMSG_BATTLEFIELD_STATUS_NEED_CONFIRMATION, 0x410000 },
            { Opcode.SMSG_BATTLEFIELD_STATUS_NONE, 0x410003 },
            { Opcode.SMSG_BATTLEFIELD_STATUS_QUEUED, 0x410002 },
            { Opcode.SMSG_BATTLEFIELD_STATUS_WAIT_FOR_GROUPS, 0x41000D }, // NYI
            { Opcode.SMSG_BATTLEGROUND_INFO_THROTTLED, 0x41000C }, // NYI
            { Opcode.SMSG_BATTLEGROUND_INIT, 0x41002D }, // NYI
            { Opcode.SMSG_BATTLEGROUND_PLAYER_JOINED, 0x410009 },
            { Opcode.SMSG_BATTLEGROUND_PLAYER_LEFT, 0x41000A },
            { Opcode.SMSG_BATTLEGROUND_PLAYER_POSITIONS, 0x410006 },
            { Opcode.SMSG_BATTLEGROUND_POINTS, 0x41002C }, // NYI
            { Opcode.SMSG_BATTLENET_CHALLENGE_ABORT, 0x3B022B }, // NYI
            { Opcode.SMSG_BATTLENET_CHALLENGE_START, 0x3B022A }, // NYI
            { Opcode.SMSG_BATTLENET_NOTIFICATION, 0x3B02A1 },
            { Opcode.SMSG_BATTLENET_RESPONSE, 0x3B02A0 },
            { Opcode.SMSG_BATTLE_NET_CONNECTION_STATUS, 0x3B02A2 },
            { Opcode.SMSG_BATTLE_PAY_ACK_FAILED, 0x3B0224 },
            { Opcode.SMSG_BATTLE_PAY_BATTLE_PET_DELIVERED, 0x3B0219 },
            { Opcode.SMSG_BATTLE_PAY_COLLECTION_ITEM_DELIVERED, 0x3B021A },
            { Opcode.SMSG_BATTLE_PAY_CONFIRM_PURCHASE, 0x3B0223 },
            { Opcode.SMSG_BATTLE_PAY_DELIVERY_ENDED, 0x3B0217 },
            { Opcode.SMSG_BATTLE_PAY_DELIVERY_STARTED, 0x3B0216 },
            { Opcode.SMSG_BATTLE_PAY_DISTRIBUTION_ASSIGN_VAS_RESPONSE, 0x3B030A },
            { Opcode.SMSG_BATTLE_PAY_DISTRIBUTION_UNREVOKED, 0x3B0214 },
            { Opcode.SMSG_BATTLE_PAY_DISTRIBUTION_UPDATE, 0x3B0215 },
            { Opcode.SMSG_BATTLE_PAY_GET_DISTRIBUTION_LIST_RESPONSE, 0x3B0213 },
            { Opcode.SMSG_BATTLE_PAY_GET_PRODUCT_LIST_RESPONSE, 0x3B0211 },
            { Opcode.SMSG_BATTLE_PAY_GET_PURCHASE_LIST_RESPONSE, 0x3B0212 },
            { Opcode.SMSG_BATTLE_PAY_MOUNT_DELIVERED, 0x3B0218 },
            { Opcode.SMSG_BATTLE_PAY_PURCHASE_UPDATE, 0x3B0222 },
            { Opcode.SMSG_BATTLE_PAY_START_CHECKOUT, 0x3B02BD },
            { Opcode.SMSG_BATTLE_PAY_START_DISTRIBUTION_ASSIGN_TO_TARGET_RESPONSE, 0x3B0220 },
            { Opcode.SMSG_BATTLE_PAY_START_PURCHASE_RESPONSE, 0x3B021F },
            { Opcode.SMSG_BATTLE_PAY_VALIDATE_PURCHASE_RESPONSE, 0x3B02B1 },
            { Opcode.SMSG_BATTLE_PETS_HEALED, 0x3B008C }, // NYI
            { Opcode.SMSG_BATTLE_PET_CAGE_DATE_ERROR, 0x3B0114 }, // NYI
            { Opcode.SMSG_BATTLE_PET_DELETED, 0x3B0089 },
            { Opcode.SMSG_BATTLE_PET_ERROR, 0x3B00D1 },
            { Opcode.SMSG_BATTLE_PET_JOURNAL, 0x3B0088 },
            { Opcode.SMSG_BATTLE_PET_JOURNAL_LOCK_ACQUIRED, 0x3B0086 },
            { Opcode.SMSG_BATTLE_PET_JOURNAL_LOCK_DENIED, 0x3B0087 },
            { Opcode.SMSG_BATTLE_PET_RESTORED, 0x3B008B }, // NYI
            { Opcode.SMSG_BATTLE_PET_REVOKED, 0x3B008A }, // NYI
            { Opcode.SMSG_BATTLE_PET_TRAP_LEVEL, 0x3B0084 }, // NYI
            { Opcode.SMSG_BATTLE_PET_UPDATES, 0x3B0083 },
            { Opcode.SMSG_BIND_POINT_UPDATE, 0x3B0011 },
            { Opcode.SMSG_BLACK_MARKET_BID_ON_ITEM_RESULT, 0x3B00C1 },
            { Opcode.SMSG_BLACK_MARKET_OUTBID, 0x3B00C2 },
            { Opcode.SMSG_BLACK_MARKET_REQUEST_ITEMS_RESULT, 0x3B00C0 },
            { Opcode.SMSG_BLACK_MARKET_WON, 0x3B00C3 },
            { Opcode.SMSG_BONUS_ROLL_EMPTY, 0x3B00DE }, // NYI
            { Opcode.SMSG_BOSS_KILL, 0x41002F },
            { Opcode.SMSG_BREAK_TARGET, 0x41001A },
            { Opcode.SMSG_BROADCAST_ACHIEVEMENT, 0x400010 },
            { Opcode.SMSG_BROADCAST_LEVELUP, 0x400011 }, // NYI
            { Opcode.SMSG_BUY_FAILED, 0x3B0163 },
            { Opcode.SMSG_BUY_SUCCEEDED, 0x3B0162 },
            { Opcode.SMSG_CACHE_INFO, 0x3F000F },
            { Opcode.SMSG_CACHE_VERSION, 0x3F000E },
            { Opcode.SMSG_CALENDAR_CLEAR_PENDING_ACTION, 0x3B0139 },
            { Opcode.SMSG_CALENDAR_COMMAND_RESULT, 0x3B013A },
            { Opcode.SMSG_CALENDAR_COMMUNITY_INVITE, 0x3B0129 },
            { Opcode.SMSG_CALENDAR_EVENT_REMOVED_ALERT, 0x3B0131 },
            { Opcode.SMSG_CALENDAR_EVENT_UPDATED_ALERT, 0x3B0132 },
            { Opcode.SMSG_CALENDAR_INVITE_ADDED, 0x3B012A },
            { Opcode.SMSG_CALENDAR_INVITE_ALERT, 0x3B012E },
            { Opcode.SMSG_CALENDAR_INVITE_NOTES, 0x3B0133 },
            { Opcode.SMSG_CALENDAR_INVITE_NOTES_ALERT, 0x3B0134 },
            { Opcode.SMSG_CALENDAR_INVITE_REMOVED, 0x3B012B },
            { Opcode.SMSG_CALENDAR_INVITE_REMOVED_ALERT, 0x3B0130 },
            { Opcode.SMSG_CALENDAR_INVITE_STATUS, 0x3B012C },
            { Opcode.SMSG_CALENDAR_INVITE_STATUS_ALERT, 0x3B012F },
            { Opcode.SMSG_CALENDAR_MODERATOR_STATUS, 0x3B012D },
            { Opcode.SMSG_CALENDAR_RAID_LOCKOUT_ADDED, 0x3B0135 },
            { Opcode.SMSG_CALENDAR_RAID_LOCKOUT_REMOVED, 0x3B0136 },
            { Opcode.SMSG_CALENDAR_RAID_LOCKOUT_UPDATED, 0x3B0137 },
            { Opcode.SMSG_CALENDAR_SEND_CALENDAR, 0x3B0127 },
            { Opcode.SMSG_CALENDAR_SEND_EVENT, 0x3B0128 },
            { Opcode.SMSG_CALENDAR_SEND_NUM_PENDING, 0x3B0138 },
            { Opcode.SMSG_CAMERA_EFFECT, 0x3B01C1 }, // NYI
            { Opcode.SMSG_CANCEL_AUTO_REPEAT, 0x3B017A },
            { Opcode.SMSG_CANCEL_COMBAT, 0x410029 },
            { Opcode.SMSG_CANCEL_ORPHAN_SPELL_VISUAL, 0x510035 },
            { Opcode.SMSG_CANCEL_PRELOAD_WORLD, 0x3B002F }, // NYI
            { Opcode.SMSG_CANCEL_SCENE, 0x3B00D0 },
            { Opcode.SMSG_CANCEL_SPELL_VISUAL, 0x510033 },
            { Opcode.SMSG_CANCEL_SPELL_VISUAL_KIT, 0x510037 },
            { Opcode.SMSG_CAN_DUEL_RESULT, 0x410025 },
            { Opcode.SMSG_CAN_REDEEM_TOKEN_FOR_BALANCE_RESPONSE, 0x3B02B0 }, // NYI
            { Opcode.SMSG_CAST_FAILED, 0x510046 },
            { Opcode.SMSG_CHANGE_PLAYER_DIFFICULTY_RESULT, 0x4E000C }, // NYI
            { Opcode.SMSG_CHANGE_REALM_TICKET_RESPONSE, 0x3B02A3 },
            { Opcode.SMSG_CHANNEL_LIST, 0x400019 },
            { Opcode.SMSG_CHANNEL_NOTIFY, 0x400015 },
            { Opcode.SMSG_CHANNEL_NOTIFY_JOINED, 0x400017 },
            { Opcode.SMSG_CHANNEL_NOTIFY_LEFT, 0x400018 },
            { Opcode.SMSG_CHANNEL_NOTIFY_NPE_JOINED_BATCH, 0x400016 },
            { Opcode.SMSG_CHARACTER_CHECK_UPGRADE_RESULT, 0x3B025D }, // NYI
            { Opcode.SMSG_CHARACTER_LOGIN_FAILED, 0x3B01A1 },
            { Opcode.SMSG_CHARACTER_OBJECT_TEST_RESPONSE, 0x3B0229 }, // NYI
            { Opcode.SMSG_CHARACTER_RENAME_RESULT, 0x3B0202 },
            { Opcode.SMSG_CHARACTER_UPGRADE_ABORTED, 0x3B025C }, // NYI
            { Opcode.SMSG_CHARACTER_UPGRADE_COMPLETE, 0x3B025B }, // NYI
            { Opcode.SMSG_CHARACTER_UPGRADE_MANUAL_UNREVOKE_RESULT, 0x3B025E }, // NYI
            { Opcode.SMSG_CHARACTER_UPGRADE_STARTED, 0x3B025A }, // NYI
            { Opcode.SMSG_CHAR_CUSTOMIZE_FAILURE, 0x3B017E },
            { Opcode.SMSG_CHAR_CUSTOMIZE_SUCCESS, 0x3B017F },
            { Opcode.SMSG_CHAR_FACTION_CHANGE_RESULT, 0x3B0247 },
            { Opcode.SMSG_CHAT, 0x400001 },
            { Opcode.SMSG_CHAT_AUTO_RESPONDED, 0x40000C }, // NYI
            { Opcode.SMSG_CHAT_DOWN, 0x400012 }, // NYI
            { Opcode.SMSG_CHAT_IGNORED_ACCOUNT_MUTED, 0x400000 }, // NYI
            { Opcode.SMSG_CHAT_IS_DOWN, 0x400013 }, // NYI
            { Opcode.SMSG_CHAT_NOT_IN_GUILD, 0x400021 }, // NYI
            { Opcode.SMSG_CHAT_NOT_IN_PARTY, 0x400006 }, // NYI
            { Opcode.SMSG_CHAT_PLAYER_AMBIGUOUS, 0x400004 },
            { Opcode.SMSG_CHAT_PLAYER_NOTFOUND, 0x40000B },
            { Opcode.SMSG_CHAT_RECONNECT, 0x400014 }, // NYI
            { Opcode.SMSG_CHAT_RESTRICTED, 0x400007 },
            { Opcode.SMSG_CHAT_SERVER_MESSAGE, 0x40001A },
            { Opcode.SMSG_CHEAT_IGNORE_DIMISHING_RETURNS, 0x510002 }, // NYI
            { Opcode.SMSG_CHECK_WARGAME_ENTRY, 0x3B0028 }, // NYI
            { Opcode.SMSG_CLEAR_ALL_SPELL_CHARGES, 0x510016 },
            { Opcode.SMSG_CLEAR_BOSS_EMOTES, 0x3B0054 },
            { Opcode.SMSG_CLEAR_COOLDOWN, 0x3B0156 },
            { Opcode.SMSG_CLEAR_COOLDOWNS, 0x510015 },
            { Opcode.SMSG_CLEAR_SPELL_CHARGES, 0x510017 },
            { Opcode.SMSG_CLEAR_TARGET, 0x410026 },
            { Opcode.SMSG_CLUB_FINDER_ERROR_MESSAGE, 0x3B02D4 }, // NYI
            { Opcode.SMSG_CLUB_FINDER_GET_CLUB_POSTING_IDS_RESPONSE, 0x3B02D7 }, // NYI
            { Opcode.SMSG_CLUB_FINDER_LOOKUP_CLUB_POSTINGS_LIST, 0x3B02D5 }, // NYI
            { Opcode.SMSG_CLUB_FINDER_RESPONSE_CHARACTER_APPLICATION_LIST, 0x3B02D2 }, // NYI
            { Opcode.SMSG_CLUB_FINDER_RESPONSE_POST_RECRUITMENT_MESSAGE, 0x3B02D6 }, // NYI
            { Opcode.SMSG_CLUB_FINDER_UPDATE_APPLICATIONS, 0x3B02D3 }, // NYI
            { Opcode.SMSG_CLUB_FINDER_WHISPER_APPLICANT_RESPONSE, 0x3B030E }, // NYI
            { Opcode.SMSG_COIN_REMOVED, 0x3B00B0 },
            { Opcode.SMSG_COMBAT_EVENT_FAILED, 0x41001D }, // NYI
            { Opcode.SMSG_COMMENTATOR_MAP_INFO, 0x3B01A3 }, // NYI
            { Opcode.SMSG_COMMENTATOR_PLAYER_INFO, 0x3B01A4 }, // NYI
            { Opcode.SMSG_COMMENTATOR_STATE_CHANGED, 0x3B01A2 }, // NYI
            { Opcode.SMSG_COMMERCE_TOKEN_GET_COUNT_RESPONSE, 0x3B026C }, // NYI
            { Opcode.SMSG_COMMERCE_TOKEN_GET_LOG_RESPONSE, 0x3B0278 },
            { Opcode.SMSG_COMMERCE_TOKEN_GET_MARKET_PRICE_RESPONSE, 0x3B026E },
            { Opcode.SMSG_COMMERCE_TOKEN_UPDATE, 0x3B026D },
            { Opcode.SMSG_COMPLAINT_RESULT, 0x3B0147 },
            { Opcode.SMSG_COMPRESSED_PACKET, 0x42000A },
            { Opcode.SMSG_CONFIRM_BARBERS_CHOICE, 0x3B0159 }, // NYI
            { Opcode.SMSG_CONFIRM_PARTY_INVITE, 0x3B02AF }, // NYI
            { Opcode.SMSG_CONNECT_TO, 0x420005 },
            { Opcode.SMSG_CONSOLE_WRITE, 0x3B00CE }, // NYI
            { Opcode.SMSG_CONSUMABLE_TOKEN_BUY_AT_MARKET_PRICE_RESPONSE, 0x3B0274 }, // NYI
            { Opcode.SMSG_CONSUMABLE_TOKEN_BUY_CHOICE_REQUIRED, 0x3B0273 }, // NYI
            { Opcode.SMSG_CONSUMABLE_TOKEN_CAN_VETERAN_BUY_RESPONSE, 0x3B0272 }, // NYI
            { Opcode.SMSG_CONSUMABLE_TOKEN_REDEEM_CONFIRM_REQUIRED, 0x3B0276 }, // NYI
            { Opcode.SMSG_CONSUMABLE_TOKEN_REDEEM_RESPONSE, 0x3B0277 }, // NYI
            { Opcode.SMSG_CONTACT_LIST, 0x3B0227 },
            { Opcode.SMSG_CONTRIBUTION_LAST_UPDATE_RESPONSE, 0x3B02B6 }, // NYI
            { Opcode.SMSG_CONTROL_UPDATE, 0x3B00E0 },
            { Opcode.SMSG_CONVERT_RUNE, 0x51004F },
            { Opcode.SMSG_COOLDOWN_CHEAT, 0x3B01D4 }, // NYI
            { Opcode.SMSG_COOLDOWN_EVENT, 0x3B0155 },
            { Opcode.SMSG_CORPSE_LOCATION, 0x3B00E8 },
            { Opcode.SMSG_CORPSE_RECLAIM_DELAY, 0x3B01E5 },
            { Opcode.SMSG_CORPSE_TRANSPORT_QUERY, 0x3B01AE },
            { Opcode.SMSG_COVENANT_CALLINGS_AVAILABILITY_RESPONSE, 0x4F0024 }, // NYI
            { Opcode.SMSG_COVENANT_PREVIEW_OPEN_NPC, 0x3B0298 }, // NYI
            { Opcode.SMSG_CREATE_CHAR, 0x3B019D },
            { Opcode.SMSG_CRITERIA_DELETED, 0x3B0183 },
            { Opcode.SMSG_CRITERIA_UPDATE, 0x3B017D },
            { Opcode.SMSG_CROSSED_INEBRIATION_THRESHOLD, 0x3B015E },
            { Opcode.SMSG_CUSTOM_LOAD_SCREEN, 0x3B0064 },
            { Opcode.SMSG_DAILY_QUESTS_RESET, 0x4F0000 },
            { Opcode.SMSG_DAMAGE_CALC_LOG, 0x510051 }, // NYI
            { Opcode.SMSG_DB_REPLY, 0x3F0000 },
            { Opcode.SMSG_DEATH_RELEASE_LOC, 0x3B016F },
            { Opcode.SMSG_DEBUG_MENU_MANAGER_FULL_UPDATE, 0x3B00F0 }, // NYI
            { Opcode.SMSG_DEFENSE_MESSAGE, 0x40000A },
            { Opcode.SMSG_DELETE_CHAR, 0x3B019E },
            { Opcode.SMSG_DESTROY_ARENA_UNIT, 0x3B01DD },
            { Opcode.SMSG_DESTRUCTIBLE_BUILDING_DAMAGE, 0x3B0195 },
            { Opcode.SMSG_DIFFERENT_INSTANCE_FROM_PARTY, 0x3B0020 }, // NYI
            { Opcode.SMSG_DISENCHANT_CREDIT, 0x3B0040 }, // NYI
            { Opcode.SMSG_DISMOUNT, 0x3B014D }, // NYI
            { Opcode.SMSG_DISMOUNT_RESULT, 0x3B0010 }, // NYI
            { Opcode.SMSG_DISPEL_FAILED, 0x51001F },
            { Opcode.SMSG_DISPLAY_GAME_ERROR, 0x3B0036 },
            { Opcode.SMSG_DISPLAY_PLAYER_CHOICE, 0x4E0004 },
            { Opcode.SMSG_DISPLAY_PROMOTION, 0x3B00E5 },
            { Opcode.SMSG_DISPLAY_QUEST_POPUP, 0x4F001E }, // NYI
            { Opcode.SMSG_DISPLAY_TOAST, 0x3B00BD },
            { Opcode.SMSG_DONT_AUTO_PUSH_SPELLS_TO_ACTION_BAR, 0x3B007A }, // NYI
            { Opcode.SMSG_DROP_NEW_CONNECTION, 0x420004 }, // NYI
            { Opcode.SMSG_DUEL_ARRANGED, 0x41001F }, // NYI
            { Opcode.SMSG_DUEL_COMPLETE, 0x410023 },
            { Opcode.SMSG_DUEL_COUNTDOWN, 0x410022 },
            { Opcode.SMSG_DUEL_IN_BOUNDS, 0x410021 },
            { Opcode.SMSG_DUEL_OUT_OF_BOUNDS, 0x410020 },
            { Opcode.SMSG_DUEL_REQUESTED, 0x41001E },
            { Opcode.SMSG_DUEL_WINNER, 0x410024 },
            { Opcode.SMSG_DURABILITY_DAMAGE_DEATH, 0x3B01E0 },
            { Opcode.SMSG_EMOTE, 0x3B0264 },
            { Opcode.SMSG_ENABLE_BARBER_SHOP, 0x3B0158 },
            { Opcode.SMSG_ENCHANTMENT_LOG, 0x3B01AF },
            { Opcode.SMSG_ENCOUNTER_END, 0x3B021E }, // NYI
            { Opcode.SMSG_ENCOUNTER_START, 0x3B021D }, // NYI
            { Opcode.SMSG_END_LIGHTNING_STORM, 0x3B0144 },
            { Opcode.SMSG_ENTER_ENCRYPTED_MODE, 0x420001 },
            { Opcode.SMSG_ENUM_CHARACTERS_RESULT, 0x3B0018 },
            { Opcode.SMSG_ENUM_VAS_PURCHASE_STATES_RESPONSE, 0x3B028E },
            { Opcode.SMSG_ENVIRONMENTAL_DAMAGE_LOG, 0x51000E },
            { Opcode.SMSG_EQUIPMENT_SET_ID, 0x3B014E },
            { Opcode.SMSG_EXPECTED_SPAM_RECORDS, 0x400005 }, // NYI
            { Opcode.SMSG_EXPLORATION_EXPERIENCE, 0x3B01FA },
            { Opcode.SMSG_FACTION_BONUS_INFO, 0x3B01C0 },
            { Opcode.SMSG_FAILED_PLAYER_CONDITION, 0x4E0002 }, // NYI
            { Opcode.SMSG_FAILED_QUEST_TURN_IN, 0x3B02AC }, // NYI
            { Opcode.SMSG_FEATURE_SYSTEM_STATUS, 0x3B0058 },
            { Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN, 0x3B0059 },
            { Opcode.SMSG_FEIGN_DEATH_RESISTED, 0x3B01DF }, // NYI
            { Opcode.SMSG_FISH_ESCAPED, 0x3B016C },
            { Opcode.SMSG_FISH_NOT_HOOKED, 0x3B016B },
            { Opcode.SMSG_FLIGHT_SPLINE_SYNC, 0x4C005B },
            { Opcode.SMSG_FORCED_DEATH_UPDATE, 0x3B0170 }, // NYI
            { Opcode.SMSG_FORCE_ANIM, 0x3B01EC }, // NYI
            { Opcode.SMSG_FORCE_OBJECT_RELINK, 0x3B00E4 }, // NYI
            { Opcode.SMSG_FORCE_RANDOM_TRANSMOG_TOAST, 0x3B004B }, // NYI
            { Opcode.SMSG_FRIEND_STATUS, 0x3B0228 },
            { Opcode.SMSG_GAME_OBJECT_ACTIVATE_ANIM_KIT, 0x3B005C },
            { Opcode.SMSG_GAME_OBJECT_BASE, 0x3B02C3 }, // NYI
            { Opcode.SMSG_GAME_OBJECT_CLOSE_INTERACTION, 0x3B030D },
            { Opcode.SMSG_GAME_OBJECT_CUSTOM_ANIM, 0x3B005D },
            { Opcode.SMSG_GAME_OBJECT_DESPAWN, 0x3B005E },
            { Opcode.SMSG_GAME_OBJECT_INTERACTION, 0x3B030C },
            { Opcode.SMSG_GAME_OBJECT_PLAY_SPELL_VISUAL, 0x51003A },
            { Opcode.SMSG_GAME_OBJECT_PLAY_SPELL_VISUAL_KIT, 0x510039 }, // NYI
            { Opcode.SMSG_GAME_OBJECT_RESET_STATE, 0x3B01B9 }, // NYI
            { Opcode.SMSG_GAME_OBJECT_SET_STATE_LOCAL, 0x3B029F },
            { Opcode.SMSG_GAME_OBJECT_UI_LINK, 0x3B01B6 }, // NYI
            { Opcode.SMSG_GAME_SPEED_SET, 0x3B011D }, // NYI
            { Opcode.SMSG_GAME_TIME_SET, 0x3B01A8 }, // NYI
            { Opcode.SMSG_GAME_TIME_UPDATE, 0x3B01A7 }, // NYI
            { Opcode.SMSG_GENERATE_RANDOM_CHARACTER_NAME_RESULT, 0x3B001C },
            { Opcode.SMSG_GENERATE_SSO_TOKEN_RESPONSE, 0x3B02B7 }, // NYI
            { Opcode.SMSG_GET_ACCOUNT_CHARACTER_LIST_RESULT, 0x3B0200 },
            { Opcode.SMSG_GET_REALM_HIDDEN_RESULT, 0x3B032C }, // NYI
            { Opcode.SMSG_GET_REMAINING_GAME_TIME_RESPONSE, 0x3B0275 }, // NYI
            { Opcode.SMSG_GET_VAS_ACCOUNT_CHARACTER_LIST_RESULT, 0x3B028A },
            { Opcode.SMSG_GET_VAS_TRANSFER_TARGET_REALM_LIST_RESULT, 0x3B028B },
            { Opcode.SMSG_GM_PLAYER_INFO, 0x4E000D }, // NYI
            { Opcode.SMSG_GM_REQUEST_PLAYER_INFO, 0x4E0003 }, // NYI
            { Opcode.SMSG_GM_TICKET_CASE_STATUS, 0x3B013F },
            { Opcode.SMSG_GM_TICKET_SYSTEM_STATUS, 0x3B013E },
            { Opcode.SMSG_GOD_MODE, 0x3B0198 }, // NYI
            { Opcode.SMSG_GOSSIP_COMPLETE, 0x4F0017 },
            { Opcode.SMSG_GOSSIP_MESSAGE, 0x4F0018 },
            { Opcode.SMSG_GOSSIP_OPTION_NPC_INTERACTION, 0x4F0028 },
            { Opcode.SMSG_GOSSIP_POI, 0x3B0233 },
            { Opcode.SMSG_GOSSIP_QUEST_UPDATE, 0x4F0019 }, // NYI
            { Opcode.SMSG_GROUP_ACTION_THROTTLED, 0x3B0025 }, // NYI
            { Opcode.SMSG_GROUP_AUTO_KICK, 0x3B0230 }, // NYI
            { Opcode.SMSG_GROUP_DECLINE, 0x3B022C },
            { Opcode.SMSG_GROUP_DESTROYED, 0x3B022F },
            { Opcode.SMSG_GROUP_NEW_LEADER, 0x3B00C6 },
            { Opcode.SMSG_GROUP_REQUEST_DECLINE, 0x3B022D }, // NYI
            { Opcode.SMSG_GROUP_UNINVITE, 0x3B022E },
            { Opcode.SMSG_GUILD_ACHIEVEMENT_DELETED, 0x47000D },
            { Opcode.SMSG_GUILD_ACHIEVEMENT_EARNED, 0x47000C },
            { Opcode.SMSG_GUILD_ACHIEVEMENT_MEMBERS, 0x47000F },
            { Opcode.SMSG_GUILD_BANK_LOG_QUERY_RESULTS, 0x470027 },
            { Opcode.SMSG_GUILD_BANK_QUERY_RESULTS, 0x470026 },
            { Opcode.SMSG_GUILD_BANK_REMAINING_WITHDRAW_MONEY, 0x470028 },
            { Opcode.SMSG_GUILD_BANK_TEXT_QUERY_RESULT, 0x47002B }, // NYI
            { Opcode.SMSG_GUILD_CHALLENGE_COMPLETED, 0x47001B }, // NYI
            { Opcode.SMSG_GUILD_CHALLENGE_UPDATE, 0x47001A },
            { Opcode.SMSG_GUILD_CHANGE_NAME_RESULT, 0x470025 }, // NYI
            { Opcode.SMSG_GUILD_COMMAND_RESULT, 0x470002 },
            { Opcode.SMSG_GUILD_CRITERIA_DELETED, 0x47000E },
            { Opcode.SMSG_GUILD_CRITERIA_UPDATE, 0x47000B },
            { Opcode.SMSG_GUILD_EVENT_BANK_CONTENTS_CHANGED, 0x470040 },
            { Opcode.SMSG_GUILD_EVENT_BANK_MONEY_CHANGED, 0x47003F },
            { Opcode.SMSG_GUILD_EVENT_DISBANDED, 0x470035 },
            { Opcode.SMSG_GUILD_EVENT_LOG_QUERY_RESULTS, 0x47002A },
            { Opcode.SMSG_GUILD_EVENT_MOTD, 0x470036 },
            { Opcode.SMSG_GUILD_EVENT_NEW_LEADER, 0x470034 },
            { Opcode.SMSG_GUILD_EVENT_PLAYER_JOINED, 0x470032 },
            { Opcode.SMSG_GUILD_EVENT_PLAYER_LEFT, 0x470033 },
            { Opcode.SMSG_GUILD_EVENT_PRESENCE_CHANGE, 0x470037 },
            { Opcode.SMSG_GUILD_EVENT_RANKS_UPDATED, 0x470039 },
            { Opcode.SMSG_GUILD_EVENT_RANK_CHANGED, 0x47003A },
            { Opcode.SMSG_GUILD_EVENT_STATUS_CHANGE, 0x470038 }, // NYI
            { Opcode.SMSG_GUILD_EVENT_TAB_ADDED, 0x47003B },
            { Opcode.SMSG_GUILD_EVENT_TAB_DELETED, 0x47003C }, // NYI
            { Opcode.SMSG_GUILD_EVENT_TAB_MODIFIED, 0x47003D },
            { Opcode.SMSG_GUILD_EVENT_TAB_TEXT_CHANGED, 0x47003E },
            { Opcode.SMSG_GUILD_FLAGGED_FOR_RENAME, 0x470024 },
            { Opcode.SMSG_GUILD_HARDCORE_MEMBER_DEATH, 0x470004 }, // NYI
            { Opcode.SMSG_GUILD_INVITE, 0x470012 },
            { Opcode.SMSG_GUILD_INVITE_DECLINED, 0x470030 }, // NYI
            { Opcode.SMSG_GUILD_INVITE_EXPIRED, 0x470031 }, // NYI
            { Opcode.SMSG_GUILD_ITEM_LOOTED_NOTIFY, 0x47001C }, // NYI
            { Opcode.SMSG_GUILD_KNOWN_RECIPES, 0x470006 }, // NYI
            { Opcode.SMSG_GUILD_MEMBERS_WITH_RECIPE, 0x470007 }, // NYI
            { Opcode.SMSG_GUILD_MEMBER_DAILY_RESET, 0x47002C },
            { Opcode.SMSG_GUILD_MEMBER_RECIPES, 0x470005 }, // NYI
            { Opcode.SMSG_GUILD_MEMBER_UPDATE_NOTE, 0x470011 },
            { Opcode.SMSG_GUILD_MOVED, 0x470022 }, // NYI
            { Opcode.SMSG_GUILD_MOVE_STARTING, 0x470021 }, // NYI
            { Opcode.SMSG_GUILD_NAME_CHANGED, 0x470023 },
            { Opcode.SMSG_GUILD_NEWS, 0x470009 },
            { Opcode.SMSG_GUILD_NEWS_DELETED, 0x47000A }, // NYI
            { Opcode.SMSG_GUILD_PARTY_STATE, 0x470013 },
            { Opcode.SMSG_GUILD_PERMISSIONS_QUERY_RESULTS, 0x470029 },
            { Opcode.SMSG_GUILD_RANKS, 0x470010 },
            { Opcode.SMSG_GUILD_REPUTATION_REACTION_CHANGED, 0x470014 }, // NYI
            { Opcode.SMSG_GUILD_RESET, 0x470020 }, // NYI
            { Opcode.SMSG_GUILD_REWARD_LIST, 0x470008 },
            { Opcode.SMSG_GUILD_ROSTER, 0x470003 },
            { Opcode.SMSG_GUILD_SEND_RANK_CHANGE, 0x470001 },
            { Opcode.SMSG_HARDCORE_DEATH_ALERT, 0x3B0334 }, // NYI
            { Opcode.SMSG_HEALTH_UPDATE, 0x3B016D },
            { Opcode.SMSG_HIGHEST_THREAT_UPDATE, 0x3B0175 },
            { Opcode.SMSG_HOTFIX_CONNECT, 0x3F0003 },
            { Opcode.SMSG_HOTFIX_MESSAGE, 0x3F0002 },
            { Opcode.SMSG_INITIALIZE_FACTIONS, 0x3B01BF },
            { Opcode.SMSG_INITIAL_SETUP, 0x3B0014 },
            { Opcode.SMSG_INIT_WORLD_STATES, 0x3B01E1 },
            { Opcode.SMSG_INSPECT_HONOR_STATS, 0x410011 }, // NYI
            { Opcode.SMSG_INSPECT_PVP, 0x3B01BD }, // NYI
            { Opcode.SMSG_INSPECT_RESULT, 0x3B00CA },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_CHANGE_PRIORITY, 0x3B024D },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_DISENGAGE_UNIT, 0x3B024C },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_END, 0x3B0255 },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_ENGAGE_UNIT, 0x3B024B },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_GAIN_COMBAT_RESURRECTION_CHARGE, 0x3B0257 },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_IN_COMBAT_RESURRECTION, 0x3B0256 },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_OBJECTIVE_COMPLETE, 0x3B0250 },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_OBJECTIVE_START, 0x3B024F },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_OBJECTIVE_UPDATE, 0x3B0254 },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_PHASE_SHIFT_CHANGED, 0x3B0258 },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_START, 0x3B0251 },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_TIMER_START, 0x3B024E },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_UPDATE_ALLOW_RELEASE_IN_PROGRESS, 0x3B0253 }, // NYI
            { Opcode.SMSG_INSTANCE_ENCOUNTER_UPDATE_SUPPRESS_RELEASE, 0x3B0252 }, // NYI
            { Opcode.SMSG_INSTANCE_GROUP_SIZE_CHANGED, 0x3B0196 }, // NYI
            { Opcode.SMSG_INSTANCE_INFO, 0x3B00CD },
            { Opcode.SMSG_INSTANCE_RESET, 0x3B0122 },
            { Opcode.SMSG_INSTANCE_RESET_FAILED, 0x3B0123 },
            { Opcode.SMSG_INSTANCE_SAVE_CREATED, 0x3B021C },
            { Opcode.SMSG_INTERRUPT_POWER_REGEN, 0x510048 },
            { Opcode.SMSG_INVALIDATE_PAGE_TEXT, 0x3F000A }, // NYI
            { Opcode.SMSG_INVALIDATE_PLAYER, 0x4E0007 },
            { Opcode.SMSG_INVALID_PROMOTION_CODE, 0x3B01EE }, // NYI
            { Opcode.SMSG_INVENTORY_CHANGE_FAILURE, 0x520005 },
            { Opcode.SMSG_INVENTORY_FIXUP_COMPLETE, 0x3B02AE }, // NYI
            { Opcode.SMSG_INVENTORY_FULL_OVERFLOW, 0x3B02BF },
            { Opcode.SMSG_ISLAND_AZERITE_GAIN, 0x3B01F7 }, // NYI
            { Opcode.SMSG_ISLAND_COMPLETE, 0x3B01F8 }, // NYI
            { Opcode.SMSG_IS_QUEST_COMPLETE_RESPONSE, 0x4F0004 }, // NYI
            { Opcode.SMSG_ITEM_CHANGED, 0x3B0187 }, // NYI
            { Opcode.SMSG_ITEM_COOLDOWN, 0x3B0263 },
            { Opcode.SMSG_ITEM_ENCHANT_TIME_UPDATE, 0x3B01F0 },
            { Opcode.SMSG_ITEM_EXPIRE_PURCHASE_REFUND, 0x3B0035 },
            { Opcode.SMSG_ITEM_PURCHASE_REFUND_RESULT, 0x3B0033 },
            { Opcode.SMSG_ITEM_PUSH_RESULT, 0x3B00BC },
            { Opcode.SMSG_ITEM_TIME_UPDATE, 0x3B01EF },
            { Opcode.SMSG_KICK_REASON, 0x3B0125 }, // NYI
            { Opcode.SMSG_LATENCY_REPORT_PING, 0x3B0306 }, // NYI
            { Opcode.SMSG_LEARNED_SPELLS, 0x51003C },
            { Opcode.SMSG_LEARN_PVP_TALENT_FAILED, 0x3B006D }, // NYI
            { Opcode.SMSG_LEARN_TALENT_FAILED, 0x3B006C },
            { Opcode.SMSG_LEGACY_LOOT_RULES, 0x3B02C4 },
            { Opcode.SMSG_LEVEL_UP_INFO, 0x3B0185 },
            { Opcode.SMSG_LFG_BOOT_PLAYER, 0x490019 },
            { Opcode.SMSG_LFG_DISABLED, 0x490017 },
            { Opcode.SMSG_LFG_EXPAND_SEARCH_PROMPT, 0x49001F }, // NYI
            { Opcode.SMSG_LFG_INSTANCE_SHUTDOWN_COUNTDOWN, 0x490009 }, // NYI
            { Opcode.SMSG_LFG_JOIN_LOBBY_MATCHMAKER_QUEUE, 0x490020 },
            { Opcode.SMSG_LFG_JOIN_RESULT, 0x490000 },
            { Opcode.SMSG_LFG_LIST_APPLICANT_LIST_UPDATE, 0x49000F }, // NYI
            { Opcode.SMSG_LFG_LIST_APPLICATION_STATUS_UPDATE, 0x49000C }, // NYI
            { Opcode.SMSG_LFG_LIST_APPLY_TO_GROUP_RESULT, 0x49000D }, // NYI
            { Opcode.SMSG_LFG_LIST_JOIN_RESULT, 0x490001 }, // NYI
            { Opcode.SMSG_LFG_LIST_SEARCH_RESULTS, 0x490002 }, // NYI
            { Opcode.SMSG_LFG_LIST_SEARCH_RESULTS_UPDATE, 0x490010 }, // NYI
            { Opcode.SMSG_LFG_LIST_SEARCH_STATUS, 0x490003 }, // NYI
            { Opcode.SMSG_LFG_LIST_UPDATE_BLACKLIST, 0x49000E },
            { Opcode.SMSG_LFG_LIST_UPDATE_EXPIRATION, 0x49000B }, // NYI
            { Opcode.SMSG_LFG_LIST_UPDATE_STATUS, 0x49000A }, // NYI
            { Opcode.SMSG_LFG_OFFER_CONTINUE, 0x490018 },
            { Opcode.SMSG_LFG_PARTY_INFO, 0x49001A },
            { Opcode.SMSG_LFG_PLAYER_INFO, 0x49001B },
            { Opcode.SMSG_LFG_PLAYER_REWARD, 0x49001C },
            { Opcode.SMSG_LFG_PROPOSAL_UPDATE, 0x490011 },
            { Opcode.SMSG_LFG_QUEUE_STATUS, 0x490004 },
            { Opcode.SMSG_LFG_READY_CHECK_RESULT, 0x49001E }, // NYI
            { Opcode.SMSG_LFG_READY_CHECK_UPDATE, 0x490006 }, // NYI
            { Opcode.SMSG_LFG_ROLE_CHECK_UPDATE, 0x490005 },
            { Opcode.SMSG_LFG_SLOT_INVALID, 0x490014 }, // NYI
            { Opcode.SMSG_LFG_TELEPORT_DENIED, 0x490016 },
            { Opcode.SMSG_LFG_UPDATE_STATUS, 0x490008 },
            { Opcode.SMSG_LIVE_REGION_ACCOUNT_RESTORE_RESULT, 0x3B020E }, // NYI
            { Opcode.SMSG_LIVE_REGION_CHARACTER_COPY_RESULT, 0x3B020D }, // NYI
            { Opcode.SMSG_LIVE_REGION_GET_ACCOUNT_CHARACTER_LIST_RESULT, 0x3B0201 }, // NYI
            { Opcode.SMSG_LOAD_CUF_PROFILES, 0x3B0055 },
            { Opcode.SMSG_LOAD_EQUIPMENT_SET, 0x3B01AA },
            { Opcode.SMSG_LOBBY_MATCHMAKER_LOBBY_ACQUIRED_SERVER, 0x3B030F }, // NYI
            { Opcode.SMSG_LOBBY_MATCHMAKER_PARTY_INFO, 0x3B0310 }, // NYI
            { Opcode.SMSG_LOBBY_MATCHMAKER_PARTY_INVITE_REJECTED, 0x3B0311 }, // NYI
            { Opcode.SMSG_LOBBY_MATCHMAKER_QUEUE_PROPOSED, 0x3B0313 },
            { Opcode.SMSG_LOBBY_MATCHMAKER_QUEUE_RESULT, 0x3B0314 },
            { Opcode.SMSG_LOBBY_MATCHMAKER_RECEIVE_INVITE, 0x3B0312 }, // NYI
            { Opcode.SMSG_LOGIN_SET_TIME_SPEED, 0x3B01A9 },
            { Opcode.SMSG_LOGIN_VERIFY_WORLD, 0x3B0030 },
            { Opcode.SMSG_LOGOUT_CANCEL_ACK, 0x3B0121 },
            { Opcode.SMSG_LOGOUT_COMPLETE, 0x3B0120 },
            { Opcode.SMSG_LOGOUT_RESPONSE, 0x3B011F },
            { Opcode.SMSG_LOG_XP_GAIN, 0x3B0181 },
            { Opcode.SMSG_LOOT_ALL_PASSED, 0x3B00BA },
            { Opcode.SMSG_LOOT_LIST, 0x3B01DC },
            { Opcode.SMSG_LOOT_MONEY_NOTIFY, 0x3B00B5 },
            { Opcode.SMSG_LOOT_RELEASE, 0x3B00B4 },
            { Opcode.SMSG_LOOT_RELEASE_ALL, 0x3B00B3 },
            { Opcode.SMSG_LOOT_REMOVED, 0x3B00AE },
            { Opcode.SMSG_LOOT_RESPONSE, 0x3B00AD },
            { Opcode.SMSG_LOOT_ROLL, 0x3B00B7 },
            { Opcode.SMSG_LOOT_ROLLS_COMPLETE, 0x3B00B9 },
            { Opcode.SMSG_LOOT_ROLL_WON, 0x3B00BB },
            { Opcode.SMSG_LOSS_OF_CONTROL_AURA_UPDATE, 0x3B010B }, // NYI
            { Opcode.SMSG_MAIL_COMMAND_RESULT, 0x3B00D4 },
            { Opcode.SMSG_MAIL_LIST_RESULT, 0x3B01F1 },
            { Opcode.SMSG_MAIL_QUERY_NEXT_TIME_RESULT, 0x3B01F2 },
            { Opcode.SMSG_MAP_OBJECTIVES_INIT, 0x41002E }, // NYI
            { Opcode.SMSG_MAP_OBJ_EVENTS, 0x3B005F }, // NYI
            { Opcode.SMSG_MASTER_LOOT_CANDIDATE_LIST, 0x3B00B8 },
            { Opcode.SMSG_MESSAGE_BOX, 0x3B000A }, // NYI
            { Opcode.SMSG_MINIMAP_PING, 0x3B016A },
            { Opcode.SMSG_MIRROR_IMAGE_COMPONENTED_DATA, 0x510004 },
            { Opcode.SMSG_MIRROR_IMAGE_CREATURE_DATA, 0x510003 },
            { Opcode.SMSG_MISSILE_CANCEL, 0x3B0060 },
            { Opcode.SMSG_MODIFY_COOLDOWN, 0x3B0203 },
            { Opcode.SMSG_MOUNT_RESULT, 0x3B000F },
            { Opcode.SMSG_MOVEMENT_ENFORCEMENT_ALERT, 0x3B02CA }, // NYI
            { Opcode.SMSG_MOVE_APPLY_INERTIA, 0x4C005E }, // NYI
            { Opcode.SMSG_MOVE_APPLY_MOVEMENT_FORCE, 0x4C0045 },
            { Opcode.SMSG_MOVE_DISABLE_COLLISION, 0x4C0041 },
            { Opcode.SMSG_MOVE_DISABLE_DOUBLE_JUMP, 0x4C002B },
            { Opcode.SMSG_MOVE_DISABLE_GRAVITY, 0x4C003D },
            { Opcode.SMSG_MOVE_DISABLE_INERTIA, 0x4C003F },
            { Opcode.SMSG_MOVE_DISABLE_TRANSITION_BETWEEN_SWIM_AND_FLY, 0x4C003C },
            { Opcode.SMSG_MOVE_ENABLE_COLLISION, 0x4C0042 },
            { Opcode.SMSG_MOVE_ENABLE_DOUBLE_JUMP, 0x4C002A },
            { Opcode.SMSG_MOVE_ENABLE_GRAVITY, 0x4C003E },
            { Opcode.SMSG_MOVE_ENABLE_INERTIA, 0x4C0040 },
            { Opcode.SMSG_MOVE_ENABLE_TRANSITION_BETWEEN_SWIM_AND_FLY, 0x4C003B },
            { Opcode.SMSG_MOVE_KNOCK_BACK, 0x4C0031 },
            { Opcode.SMSG_MOVE_REMOVE_INERTIA, 0x4C005F }, // NYI
            { Opcode.SMSG_MOVE_REMOVE_MOVEMENT_FORCE, 0x4C0046 },
            { Opcode.SMSG_MOVE_ROOT, 0x4C0027 },
            { Opcode.SMSG_MOVE_SET_ACTIVE_MOVER, 0x4C0003 },
            { Opcode.SMSG_MOVE_SET_CAN_FLY, 0x4C0033 },
            { Opcode.SMSG_MOVE_SET_CAN_TURN_WHILE_FALLING, 0x4C0037 },
            { Opcode.SMSG_MOVE_SET_COLLISION_HEIGHT, 0x4C0043 },
            { Opcode.SMSG_MOVE_SET_COMPOUND_STATE, 0x4C0047 },
            { Opcode.SMSG_MOVE_SET_FEATHER_FALL, 0x4C002D },
            { Opcode.SMSG_MOVE_SET_FLIGHT_BACK_SPEED, 0x4C0023 },
            { Opcode.SMSG_MOVE_SET_FLIGHT_SPEED, 0x4C0022 },
            { Opcode.SMSG_MOVE_SET_HOVERING, 0x4C002F },
            { Opcode.SMSG_MOVE_SET_IGNORE_MOVEMENT_FORCES, 0x4C0039 },
            { Opcode.SMSG_MOVE_SET_LAND_WALK, 0x4C002C },
            { Opcode.SMSG_MOVE_SET_MOD_MOVEMENT_FORCE_MAGNITUDE, 0x4C0014 },
            { Opcode.SMSG_MOVE_SET_NORMAL_FALL, 0x4C002E },
            { Opcode.SMSG_MOVE_SET_PITCH_RATE, 0x4C0026 },
            { Opcode.SMSG_MOVE_SET_RUN_BACK_SPEED, 0x4C001F },
            { Opcode.SMSG_MOVE_SET_RUN_SPEED, 0x4C001E },
            { Opcode.SMSG_MOVE_SET_SWIM_BACK_SPEED, 0x4C0021 },
            { Opcode.SMSG_MOVE_SET_SWIM_SPEED, 0x4C0020 },
            { Opcode.SMSG_MOVE_SET_TURN_RATE, 0x4C0025 },
            { Opcode.SMSG_MOVE_SET_VEHICLE_REC_ID, 0x4C0044 },
            { Opcode.SMSG_MOVE_SET_WALK_SPEED, 0x4C0024 },
            { Opcode.SMSG_MOVE_SET_WATER_WALK, 0x4C0029 },
            { Opcode.SMSG_MOVE_SKIP_TIME, 0x4C0048 },
            { Opcode.SMSG_MOVE_SPLINE_DISABLE_COLLISION, 0x4C004D },
            { Opcode.SMSG_MOVE_SPLINE_DISABLE_GRAVITY, 0x4C004B },
            { Opcode.SMSG_MOVE_SPLINE_ENABLE_COLLISION, 0x4C004E },
            { Opcode.SMSG_MOVE_SPLINE_ENABLE_GRAVITY, 0x4C004C },
            { Opcode.SMSG_MOVE_SPLINE_ROOT, 0x4C0049 },
            { Opcode.SMSG_MOVE_SPLINE_SET_FEATHER_FALL, 0x4C004F },
            { Opcode.SMSG_MOVE_SPLINE_SET_FLIGHT_BACK_SPEED, 0x4C001A },
            { Opcode.SMSG_MOVE_SPLINE_SET_FLIGHT_SPEED, 0x4C0019 },
            { Opcode.SMSG_MOVE_SPLINE_SET_FLYING, 0x4C0059 },
            { Opcode.SMSG_MOVE_SPLINE_SET_HOVER, 0x4C0051 },
            { Opcode.SMSG_MOVE_SPLINE_SET_LAND_WALK, 0x4C0054 },
            { Opcode.SMSG_MOVE_SPLINE_SET_NORMAL_FALL, 0x4C0050 },
            { Opcode.SMSG_MOVE_SPLINE_SET_PITCH_RATE, 0x4C001D },
            { Opcode.SMSG_MOVE_SPLINE_SET_RUN_BACK_SPEED, 0x4C0016 },
            { Opcode.SMSG_MOVE_SPLINE_SET_RUN_MODE, 0x4C0057 },
            { Opcode.SMSG_MOVE_SPLINE_SET_RUN_SPEED, 0x4C0015 },
            { Opcode.SMSG_MOVE_SPLINE_SET_SWIM_BACK_SPEED, 0x4C0018 },
            { Opcode.SMSG_MOVE_SPLINE_SET_SWIM_SPEED, 0x4C0017 },
            { Opcode.SMSG_MOVE_SPLINE_SET_TURN_RATE, 0x4C001C },
            { Opcode.SMSG_MOVE_SPLINE_SET_WALK_MODE, 0x4C0058 },
            { Opcode.SMSG_MOVE_SPLINE_SET_WALK_SPEED, 0x4C001B },
            { Opcode.SMSG_MOVE_SPLINE_SET_WATER_WALK, 0x4C0053 },
            { Opcode.SMSG_MOVE_SPLINE_START_SWIM, 0x4C0055 },
            { Opcode.SMSG_MOVE_SPLINE_STOP_SWIM, 0x4C0056 },
            { Opcode.SMSG_MOVE_SPLINE_UNROOT, 0x4C004A },
            { Opcode.SMSG_MOVE_SPLINE_UNSET_FLYING, 0x4C005A },
            { Opcode.SMSG_MOVE_SPLINE_UNSET_HOVER, 0x4C0052 },
            { Opcode.SMSG_MOVE_TELEPORT, 0x4C0032 },
            { Opcode.SMSG_MOVE_UNROOT, 0x4C0028 },
            { Opcode.SMSG_MOVE_UNSET_CAN_FLY, 0x4C0034 },
            { Opcode.SMSG_MOVE_UNSET_CAN_TURN_WHILE_FALLING, 0x4C0038 },
            { Opcode.SMSG_MOVE_UNSET_HOVERING, 0x4C0030 },
            { Opcode.SMSG_MOVE_UNSET_IGNORE_MOVEMENT_FORCES, 0x4C003A },
            { Opcode.SMSG_MOVE_UPDATE, 0x4C000E },
            { Opcode.SMSG_MOVE_UPDATE_APPLY_INERTIA, 0x4C0060 }, // NYI
            { Opcode.SMSG_MOVE_UPDATE_APPLY_MOVEMENT_FORCE, 0x4C0012 },
            { Opcode.SMSG_MOVE_UPDATE_COLLISION_HEIGHT, 0x4C000D },
            { Opcode.SMSG_MOVE_UPDATE_FLIGHT_BACK_SPEED, 0x4C000A },
            { Opcode.SMSG_MOVE_UPDATE_FLIGHT_SPEED, 0x4C0009 },
            { Opcode.SMSG_MOVE_UPDATE_KNOCK_BACK, 0x4C0010 },
            { Opcode.SMSG_MOVE_UPDATE_MOD_MOVEMENT_FORCE_MAGNITUDE, 0x4C0011 },
            { Opcode.SMSG_MOVE_UPDATE_PITCH_RATE, 0x4C000C },
            { Opcode.SMSG_MOVE_UPDATE_REMOVE_INERTIA, 0x4C0061 }, // NYI
            { Opcode.SMSG_MOVE_UPDATE_REMOVE_MOVEMENT_FORCE, 0x4C0013 },
            { Opcode.SMSG_MOVE_UPDATE_RUN_BACK_SPEED, 0x4C0005 },
            { Opcode.SMSG_MOVE_UPDATE_RUN_SPEED, 0x4C0004 },
            { Opcode.SMSG_MOVE_UPDATE_SWIM_BACK_SPEED, 0x4C0008 },
            { Opcode.SMSG_MOVE_UPDATE_SWIM_SPEED, 0x4C0007 },
            { Opcode.SMSG_MOVE_UPDATE_TELEPORT, 0x4C000F },
            { Opcode.SMSG_MOVE_UPDATE_TURN_RATE, 0x4C000B },
            { Opcode.SMSG_MOVE_UPDATE_WALK_SPEED, 0x4C0006 },
            { Opcode.SMSG_MULTIPLE_PACKETS, 0x420009 },
            { Opcode.SMSG_NEUTRAL_PLAYER_FACTION_SELECT_RESULT, 0x3B0075 }, // NYI
            { Opcode.SMSG_NEW_DATA_BUILD, 0x3B032B }, // NYI
            { Opcode.SMSG_NEW_TAXI_PATH, 0x3B011A },
            { Opcode.SMSG_NEW_WORLD, 0x3B002C },
            { Opcode.SMSG_NOTIFY_DEST_LOC_SPELL_CAST, 0x510032 }, // NYI
            { Opcode.SMSG_NOTIFY_MISSILE_TRAJECTORY_COLLISION, 0x3B0146 },
            { Opcode.SMSG_NOTIFY_MONEY, 0x3B0032 }, // NYI
            { Opcode.SMSG_NOTIFY_RECEIVED_MAIL, 0x3B00D5 },
            { Opcode.SMSG_NPC_INTERACTION_OPEN_RESULT, 0x3B030B },
            { Opcode.SMSG_OFFER_PETITION_ERROR, 0x3B0152 },
            { Opcode.SMSG_ON_CANCEL_EXPECTED_RIDE_VEHICLE_AURA, 0x3B0182 },
            { Opcode.SMSG_ON_MONSTER_MOVE, 0x4C0002 },
            { Opcode.SMSG_OPEN_CONTAINER, 0x520006 }, // NYI
            { Opcode.SMSG_OPEN_LFG_DUNGEON_FINDER, 0x490015 }, // NYI
            { Opcode.SMSG_OVERRIDE_LIGHT, 0x3B0157 },
            { Opcode.SMSG_PAGE_TEXT, 0x3B01B5 },
            { Opcode.SMSG_PARTY_COMMAND_RESULT, 0x3B0231 },
            { Opcode.SMSG_PARTY_INVITE, 0x3B0056 },
            { Opcode.SMSG_PARTY_KILL_LOG, 0x3B01F5 },
            { Opcode.SMSG_PARTY_MEMBER_FULL_STATE, 0x3B01F4 },
            { Opcode.SMSG_PARTY_MEMBER_PARTIAL_STATE, 0x3B01F3 }, // NYI
            { Opcode.SMSG_PARTY_NOTIFY_LFG_LEADER_CHANGE, 0x3B02FA }, // NYI
            { Opcode.SMSG_PARTY_UPDATE, 0x3B008D },
            { Opcode.SMSG_PAUSE_MIRROR_TIMER, 0x3B01AC },
            { Opcode.SMSG_PENDING_RAID_LOCK, 0x3B0194 },
            { Opcode.SMSG_PETITION_ALREADY_SIGNED, 0x3B0038 },
            { Opcode.SMSG_PETITION_RENAME_GUILD_RESPONSE, 0x470042 },
            { Opcode.SMSG_PETITION_SHOW_LIST, 0x3B015B },
            { Opcode.SMSG_PETITION_SHOW_SIGNATURES, 0x3B015C },
            { Opcode.SMSG_PETITION_SIGN_RESULTS, 0x3B01E7 },
            { Opcode.SMSG_PET_ACTION_FEEDBACK, 0x3B01E4 },
            { Opcode.SMSG_PET_ACTION_SOUND, 0x3B013C },
            { Opcode.SMSG_PET_BATTLE_SLOT_UPDATES, 0x3B0085 },
            { Opcode.SMSG_PET_CAST_FAILED, 0x510047 },
            { Opcode.SMSG_PET_CLEAR_SPELLS, 0x510013 }, // NYI
            { Opcode.SMSG_PET_DISMISS_SOUND, 0x3B013D }, // NYI
            { Opcode.SMSG_PET_GOD_MODE, 0x3B0117 }, // NYI
            { Opcode.SMSG_PET_GUIDS, 0x3B01A0 }, // NYI
            { Opcode.SMSG_PET_LEARNED_SPELLS, 0x51003E },
            { Opcode.SMSG_PET_MODE, 0x3B001F },
            { Opcode.SMSG_PET_NAME_INVALID, 0x3B0160 },
            { Opcode.SMSG_PET_NEWLY_TAMED, 0x3B001E }, // NYI
            { Opcode.SMSG_PET_SPELLS_MESSAGE, 0x510014 },
            { Opcode.SMSG_PET_STABLE_RESULT, 0x3B002B },
            { Opcode.SMSG_PET_TAME_FAILURE, 0x3B014F },
            { Opcode.SMSG_PET_UNLEARNED_SPELLS, 0x51003F },
            { Opcode.SMSG_PHASE_SHIFT_CHANGE, 0x3B000C },
            { Opcode.SMSG_PLAYED_TIME, 0x3B0171 },
            { Opcode.SMSG_PLAYER_ACKNOWLEDGE_ARROW_CALLOUT, 0x4E002C }, // NYI
            { Opcode.SMSG_PLAYER_BATTLEFIELD_AUTO_QUEUE, 0x4E0026 }, // NYI
            { Opcode.SMSG_PLAYER_BONUS_ROLL_FAILED, 0x4E0020 }, // NYI
            { Opcode.SMSG_PLAYER_BOUND, 0x4E0000 },
            { Opcode.SMSG_PLAYER_CHOICE_CLEAR, 0x4E0006 }, // NYI
            { Opcode.SMSG_PLAYER_CHOICE_DISPLAY_ERROR, 0x4E0005 }, // NYI
            { Opcode.SMSG_PLAYER_CONDITION_RESULT, 0x4E0012 }, // NYI
            { Opcode.SMSG_PLAYER_END_OF_MATCH_DETAILS, 0x4E002E }, // NYI
            { Opcode.SMSG_PLAYER_HIDE_ARROW_CALLOUT, 0x4E002B }, // NYI
            { Opcode.SMSG_PLAYER_IS_ADVENTURE_MAP_POI_VALID, 0x4E0011 },
            { Opcode.SMSG_PLAYER_SAVE_GUILD_EMBLEM, 0x470041 },
            { Opcode.SMSG_PLAYER_SHOW_ARROW_CALLOUT, 0x4E002A }, // NYI
            { Opcode.SMSG_PLAYER_SHOW_GENERIC_WIDGET_DISPLAY, 0x4E0028 }, // NYI
            { Opcode.SMSG_PLAYER_SHOW_PARTY_POSE_UI, 0x4E0029 }, // NYI
            { Opcode.SMSG_PLAYER_SHOW_UI_EVENT_TOAST, 0x4E0023 }, // NYI
            { Opcode.SMSG_PLAYER_SKINNED, 0x4E000E }, // NYI
            { Opcode.SMSG_PLAYER_TUTORIAL_HIGHLIGHT_SPELL, 0x4E0015 }, // NYI
            { Opcode.SMSG_PLAYER_TUTORIAL_UNHIGHLIGHT_SPELL, 0x4E0014 }, // NYI
            { Opcode.SMSG_PLAYER_WORLD_PVP_QUEUE, 0x4E0027 }, // NYI
            { Opcode.SMSG_PLAY_MUSIC, 0x3B0208 },
            { Opcode.SMSG_PLAY_OBJECT_SOUND, 0x3B020A },
            { Opcode.SMSG_PLAY_ONE_SHOT_ANIM_KIT, 0x3B01CC },
            { Opcode.SMSG_PLAY_ORPHAN_SPELL_VISUAL, 0x510036 },
            { Opcode.SMSG_PLAY_SCENE, 0x3B00CF },
            { Opcode.SMSG_PLAY_SOUND, 0x3B0207 },
            { Opcode.SMSG_PLAY_SPEAKERBOT_SOUND, 0x3B020B },
            { Opcode.SMSG_PLAY_SPELL_VISUAL, 0x510034 },
            { Opcode.SMSG_PLAY_SPELL_VISUAL_KIT, 0x510038 },
            { Opcode.SMSG_PONG, 0x420006 },
            { Opcode.SMSG_POWER_UPDATE, 0x3B016E },
            { Opcode.SMSG_PRELOAD_CHILD_MAP, 0x3B000D }, // NYI
            { Opcode.SMSG_PRELOAD_WORLD, 0x3B002D }, // NYI
            { Opcode.SMSG_PREPOPULATE_NAME_CACHE, 0x3B02CD }, // NYI
            { Opcode.SMSG_PRE_RESSURECT, 0x3B0206 },
            { Opcode.SMSG_PRINT_NOTIFICATION, 0x3B0063 },
            { Opcode.SMSG_PROC_RESIST, 0x3B01F6 },
            { Opcode.SMSG_PROPOSE_LEVEL_GRANT, 0x3B0179 }, // NYI
            { Opcode.SMSG_PUSH_SPELL_TO_ACTION_BAR, 0x510040 }, // NYI
            { Opcode.SMSG_PVP_CREDIT, 0x410028 },
            { Opcode.SMSG_PVP_LOG_DATA, 0x410012 },
            { Opcode.SMSG_PVP_MATCH_INITIALIZE, 0x410034 },
            { Opcode.SMSG_PVP_MATCH_START, 0x410031 },
            { Opcode.SMSG_PVP_OPTIONS_ENABLED, 0x410016 },
            { Opcode.SMSG_PVP_SEASON, 0x3B005A },
            { Opcode.SMSG_QUERY_ARENA_TEAM_RESPONSE, 0x3F0012 },
            { Opcode.SMSG_QUERY_BATTLE_PET_NAME_RESPONSE, 0x3F000C },
            { Opcode.SMSG_QUERY_CREATURE_RESPONSE, 0x3F0006 },
            { Opcode.SMSG_QUERY_GAME_OBJECT_RESPONSE, 0x3F0007 },
            { Opcode.SMSG_QUERY_GUILD_INFO_RESPONSE, 0x47002D },
            { Opcode.SMSG_QUERY_ITEM_TEXT_RESPONSE, 0x3F0010 },
            { Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE, 0x3F0008 },
            { Opcode.SMSG_QUERY_PAGE_TEXT_RESPONSE, 0x3F0009 },
            { Opcode.SMSG_QUERY_PETITION_RESPONSE, 0x3F000D },
            { Opcode.SMSG_QUERY_PET_NAME_RESPONSE, 0x3F000B },
            { Opcode.SMSG_QUERY_PLAYER_NAMES_RESPONSE, 0x4E0025 },
            { Opcode.SMSG_QUERY_PLAYER_NAME_BY_COMMUNITY_ID_RESPONSE, 0x4E000A }, // NYI
            { Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE, 0x4F0016 },
            { Opcode.SMSG_QUERY_TIME_RESPONSE, 0x3B0180 },
            { Opcode.SMSG_QUEST_COMPLETION_NPC_RESPONSE, 0x4F0001 },
            { Opcode.SMSG_QUEST_CONFIRM_ACCEPT, 0x4F000F },
            { Opcode.SMSG_QUEST_FORCE_REMOVED, 0x4F001C },
            { Opcode.SMSG_QUEST_GIVER_INVALID_QUEST, 0x4F0005 },
            { Opcode.SMSG_QUEST_GIVER_OFFER_REWARD_MESSAGE, 0x4F0014 },
            { Opcode.SMSG_QUEST_GIVER_QUEST_COMPLETE, 0x4F0003 },
            { Opcode.SMSG_QUEST_GIVER_QUEST_DETAILS, 0x4F0012 },
            { Opcode.SMSG_QUEST_GIVER_QUEST_FAILED, 0x4F0006 },
            { Opcode.SMSG_QUEST_GIVER_QUEST_LIST_MESSAGE, 0x4F001A },
            { Opcode.SMSG_QUEST_GIVER_REQUEST_ITEMS, 0x4F0013 },
            { Opcode.SMSG_QUEST_GIVER_STATUS, 0x4F001B },
            { Opcode.SMSG_QUEST_GIVER_STATUS_MULTIPLE, 0x4F0011 },
            { Opcode.SMSG_QUEST_ITEM_USABILITY_RESPONSE, 0x4F0002 }, // NYI
            { Opcode.SMSG_QUEST_LOG_FULL, 0x4F0007 },
            { Opcode.SMSG_QUEST_NON_LOG_UPDATE_COMPLETE, 0x4F0008 },
            { Opcode.SMSG_QUEST_POI_QUERY_RESPONSE, 0x4F001D },
            { Opcode.SMSG_QUEST_POI_UPDATE_RESPONSE, 0x4F001F }, // NYI
            { Opcode.SMSG_QUEST_PUSH_RESULT, 0x4F0010 },
            { Opcode.SMSG_QUEST_SESSION_READY_CHECK, 0x3B02DE }, // NYI
            { Opcode.SMSG_QUEST_SESSION_READY_CHECK_RESPONSE, 0x3B02DF }, // NYI
            { Opcode.SMSG_QUEST_SESSION_RESULT, 0x3B02DD }, // NYI
            { Opcode.SMSG_QUEST_UPDATE_ADD_CREDIT, 0x4F000C },
            { Opcode.SMSG_QUEST_UPDATE_ADD_CREDIT_SIMPLE, 0x4F000D },
            { Opcode.SMSG_QUEST_UPDATE_ADD_PVP_CREDIT, 0x4F000E },
            { Opcode.SMSG_QUEST_UPDATE_COMPLETE, 0x4F0009 },
            { Opcode.SMSG_QUEST_UPDATE_FAILED, 0x4F000A }, // NYI
            { Opcode.SMSG_QUEST_UPDATE_FAILED_TIMER, 0x4F000B },
            { Opcode.SMSG_QUEUE_SUMMARY_UPDATE, 0x3B02AD }, // NYI
            { Opcode.SMSG_RAID_DIFFICULTY_SET, 0x3B0248 },
            { Opcode.SMSG_RAID_GROUP_ONLY, 0x3B024A },
            { Opcode.SMSG_RAID_INSTANCE_MESSAGE, 0x400008 },
            { Opcode.SMSG_RAID_MARKERS_CHANGED, 0x3B0039 },
            { Opcode.SMSG_RANDOM_ROLL, 0x3B00C9 },
            { Opcode.SMSG_RATED_PVP_INFO, 0x41000F },
            { Opcode.SMSG_READY_CHECK_COMPLETED, 0x3B0091 },
            { Opcode.SMSG_READY_CHECK_RESPONSE, 0x3B0090 },
            { Opcode.SMSG_READY_CHECK_STARTED, 0x3B008F },
            { Opcode.SMSG_READ_ITEM_RESULT_FAILED, 0x3B0244 },
            { Opcode.SMSG_READ_ITEM_RESULT_OK, 0x3B023C },
            { Opcode.SMSG_REALM_QUERY_RESPONSE, 0x3F0005 },
            { Opcode.SMSG_REATTACH_RESURRECT, 0x3B01E6 }, // NYI
            { Opcode.SMSG_RECRUIT_A_FRIEND_FAILURE, 0x3B015D },
            { Opcode.SMSG_REFER_A_FRIEND_EXPIRED, 0x3B01BE }, // NYI
            { Opcode.SMSG_REFORGE_RESULT, 0x3B0015 }, // NYI
            { Opcode.SMSG_REFRESH_COMPONENT, 0x3B00EA }, // NYI
            { Opcode.SMSG_REFRESH_SPELL_HISTORY, 0x51001B },
            { Opcode.SMSG_REMOVE_ITEM_PASSIVE, 0x3B0044 },
            { Opcode.SMSG_REMOVE_SPELL_FROM_ACTION_BAR, 0x510041 },
            { Opcode.SMSG_REPORT_PVP_PLAYER_AFK_RESULT, 0x4E0009 },
            { Opcode.SMSG_REQUEST_CEMETERY_LIST_RESPONSE, 0x3B0026 },
            { Opcode.SMSG_REQUEST_PVP_REWARDS_RESPONSE, 0x410017 },
            { Opcode.SMSG_REQUEST_SCHEDULED_PVP_INFO_RESPONSE, 0x410018 }, // NYI
            { Opcode.SMSG_RESET_COMPRESSION_CONTEXT, 0x420007 }, // NYI
            { Opcode.SMSG_RESET_FAILED_NOTIFY, 0x3B0153 },
            { Opcode.SMSG_RESET_QUEST_POI, 0x4F0020 }, // NYI
            { Opcode.SMSG_RESET_RANGED_COMBAT_TIMER, 0x410027 }, // NYI
            { Opcode.SMSG_RESET_WEEKLY_CURRENCY, 0x3B0009 },
            { Opcode.SMSG_RESPEC_WIPE_CONFIRM, 0x3B00AB },
            { Opcode.SMSG_RESPOND_INSPECT_ACHIEVEMENTS, 0x3B0006 },
            { Opcode.SMSG_RESUME_CAST, 0x51002A }, // NYI
            { Opcode.SMSG_RESUME_CAST_BAR, 0x51002D }, // NYI
            { Opcode.SMSG_RESUME_COMMS, 0x420003 },
            { Opcode.SMSG_RESUME_TOKEN, 0x3B0042 },
            { Opcode.SMSG_RESURRECT_REQUEST, 0x3B0012 },
            { Opcode.SMSG_RESYNC_RUNES, 0x51004E },
            { Opcode.SMSG_RETURN_APPLICANT_LIST, 0x3B02D1 }, // NYI
            { Opcode.SMSG_RETURN_RECRUITING_CLUBS, 0x3B02D0 }, // NYI
            { Opcode.SMSG_ROLE_CHANGED_INFORM, 0x3B0021 },
            { Opcode.SMSG_ROLE_CHOSEN, 0x49001D },
            { Opcode.SMSG_ROLE_POLL_INFORM, 0x3B0022 },
            { Opcode.SMSG_RUNE_REGEN_DEBUG, 0x3B004F }, // NYI
            { Opcode.SMSG_SCENARIO_COMPLETED, 0x3B0287 },
            { Opcode.SMSG_SCENARIO_POIS, 0x3B00CC },
            { Opcode.SMSG_SCENARIO_PROGRESS_UPDATE, 0x3B00C5 },
            { Opcode.SMSG_SCENARIO_SHOW_CRITERIA, 0x3B029D }, // NYI
            { Opcode.SMSG_SCENARIO_STATE, 0x3B00C4 },
            { Opcode.SMSG_SCENARIO_UI_UPDATE, 0x3B029C }, // NYI
            { Opcode.SMSG_SCENARIO_VACATE, 0x3B0245 },
            { Opcode.SMSG_SCENE_OBJECT_EVENT, 0x3B007B }, // NYI
            { Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_FINAL_ROUND, 0x3B0080 }, // NYI
            { Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_FINISHED, 0x3B0081 }, // NYI
            { Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_FIRST_ROUND, 0x3B007D }, // NYI
            { Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_INITIAL_UPDATE, 0x3B007C }, // NYI
            { Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_REPLACEMENTS_MADE, 0x3B007F }, // NYI
            { Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_ROUND_RESULT, 0x3B007E }, // NYI
            { Opcode.SMSG_SCRIPT_CAST, 0x510045 }, // NYI
            { Opcode.SMSG_SELL_RESPONSE, 0x3B0161 },
            { Opcode.SMSG_SEND_ITEM_PASSIVES, 0x3B0045 },
            { Opcode.SMSG_SEND_KNOWN_SPELLS, 0x510019 },
            { Opcode.SMSG_SEND_RAID_TARGET_UPDATE_ALL, 0x3B00C7 },
            { Opcode.SMSG_SEND_RAID_TARGET_UPDATE_SINGLE, 0x3B00C8 },
            { Opcode.SMSG_SEND_SPELL_CHARGES, 0x51001C },
            { Opcode.SMSG_SEND_SPELL_HISTORY, 0x51001A },
            { Opcode.SMSG_SEND_UNLEARN_SPELLS, 0x51001D },
            { Opcode.SMSG_SERVER_FIRST_ACHIEVEMENTS, 0x3B00E7 }, // NYI
            { Opcode.SMSG_SERVER_TIME, 0x3B011E }, // NYI
            { Opcode.SMSG_SERVER_TIME_OFFSET, 0x3B01B0 },
            { Opcode.SMSG_SETUP_CURRENCY, 0x3B0007 },
            { Opcode.SMSG_SET_AI_ANIM_KIT, 0x3B01CB },
            { Opcode.SMSG_SET_ANIM_TIER, 0x3B01CF },
            { Opcode.SMSG_SET_CHR_UPGRADE_TIER, 0x3B0078 }, // NYI
            { Opcode.SMSG_SET_CURRENCY, 0x3B0008 },
            { Opcode.SMSG_SET_DF_FAST_LAUNCH_RESULT, 0x490012 }, // NYI
            { Opcode.SMSG_SET_DUNGEON_DIFFICULTY, 0x3B0140 },
            { Opcode.SMSG_SET_FACTION_AT_WAR, 0x3B019C }, // NYI
            { Opcode.SMSG_SET_FACTION_NOT_VISIBLE, 0x3B01C6 },
            { Opcode.SMSG_SET_FACTION_STANDING, 0x3B01C7 },
            { Opcode.SMSG_SET_FACTION_VISIBLE, 0x3B01C5 },
            { Opcode.SMSG_SET_FLAT_SPELL_MODIFIER, 0x510025 },
            { Opcode.SMSG_SET_FORGE_MASTER, 0x3B0027 }, // NYI
            { Opcode.SMSG_SET_ITEM_PURCHASE_DATA, 0x3B0034 },
            { Opcode.SMSG_SET_LOOT_METHOD_FAILED, 0x3B026B }, // NYI
            { Opcode.SMSG_SET_MAX_WEEKLY_QUANTITY, 0x3B0037 }, // NYI
            { Opcode.SMSG_SET_MELEE_ANIM_KIT, 0x3B01CE },
            { Opcode.SMSG_SET_MOVEMENT_ANIM_KIT, 0x3B01CD },
            { Opcode.SMSG_SET_PCT_SPELL_MODIFIER, 0x510026 },
            { Opcode.SMSG_SET_PET_SPECIALIZATION, 0x3B00BE },
            { Opcode.SMSG_SET_PLAYER_DECLINED_NAMES_RESULT, 0x4E000B },
            { Opcode.SMSG_SET_PLAY_HOVER_ANIM, 0x3B0053 },
            { Opcode.SMSG_SET_PROFICIENCY, 0x3B01D0 },
            { Opcode.SMSG_SET_SPELL_CHARGES, 0x510018 },
            { Opcode.SMSG_SET_TIME_ZONE_INFORMATION, 0x3B0113 },
            { Opcode.SMSG_SET_VEHICLE_REC_ID, 0x3B0193 },
            { Opcode.SMSG_SHOW_NEUTRAL_PLAYER_FACTION_SELECT_UI, 0x3B0074 }, // NYI
            { Opcode.SMSG_SHOW_QUEST_COMPLETION_TEXT, 0x4F0015 }, // NYI
            { Opcode.SMSG_SHOW_TAXI_NODES, 0x3B0169 },
            { Opcode.SMSG_SHOW_TRADE_SKILL_RESPONSE, 0x3B0210 }, // NYI
            { Opcode.SMSG_SOCIAL_CONTRACT_REQUEST_RESPONSE, 0x3B0316 },
            { Opcode.SMSG_SOCKET_GEMS_FAILURE, 0x3B01C3 }, // NYI
            { Opcode.SMSG_SOCKET_GEMS_SUCCESS, 0x3B01C2 },
            { Opcode.SMSG_SOR_START_EXPERIENCE_INCOMPLETE, 0x3B0076 }, // NYI
            { Opcode.SMSG_SPECIAL_MOUNT_ANIM, 0x3B013B },
            { Opcode.SMSG_SPECTATE_END, 0x3B032A }, // NYI
            { Opcode.SMSG_SPECTATE_PLAYER, 0x3B0329 }, // NYI
            { Opcode.SMSG_SPEC_INVOLUNTARILY_CHANGED, 0x3B01B4 }, // NYI
            { Opcode.SMSG_SPELL_ABSORB_LOG, 0x51000C },
            { Opcode.SMSG_SPELL_CATEGORY_COOLDOWN, 0x510006 }, // NYI
            { Opcode.SMSG_SPELL_CHANNEL_START, 0x510023 },
            { Opcode.SMSG_SPELL_CHANNEL_UPDATE, 0x510024 },
            { Opcode.SMSG_SPELL_COOLDOWN, 0x510005 },
            { Opcode.SMSG_SPELL_DAMAGE_SHIELD, 0x510020 },
            { Opcode.SMSG_SPELL_DELAYED, 0x51002E },
            { Opcode.SMSG_SPELL_DISPELL_LOG, 0x510007 },
            { Opcode.SMSG_SPELL_ENERGIZE_LOG, 0x510009 },
            { Opcode.SMSG_SPELL_EXECUTE_LOG, 0x51002F },
            { Opcode.SMSG_SPELL_FAILED_OTHER, 0x510044 },
            { Opcode.SMSG_SPELL_FAILURE, 0x510042 },
            { Opcode.SMSG_SPELL_FAILURE_MESSAGE, 0x510049 }, // NYI
            { Opcode.SMSG_SPELL_GO, 0x510028 },
            { Opcode.SMSG_SPELL_HEAL_ABSORB_LOG, 0x51000B },
            { Opcode.SMSG_SPELL_HEAL_LOG, 0x51000A },
            { Opcode.SMSG_SPELL_INSTAKILL_LOG, 0x510022 },
            { Opcode.SMSG_SPELL_INTERRUPT_LOG, 0x51000D },
            { Opcode.SMSG_SPELL_MISS_LOG, 0x510030 },
            { Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG, 0x510021 },
            { Opcode.SMSG_SPELL_OR_DAMAGE_IMMUNE, 0x51001E },
            { Opcode.SMSG_SPELL_PERIODIC_AURA_LOG, 0x510008 },
            { Opcode.SMSG_SPELL_PREPARE, 0x510027 },
            { Opcode.SMSG_SPELL_START, 0x510029 },
            { Opcode.SMSG_SPELL_VISUAL_LOAD_SCREEN, 0x3B0065 },
            { Opcode.SMSG_STAND_STATE_UPDATE, 0x3B01B8 },
            { Opcode.SMSG_START_ELAPSED_TIMER, 0x3B009D }, // NYI
            { Opcode.SMSG_START_ELAPSED_TIMERS, 0x3B009F }, // NYI
            { Opcode.SMSG_START_LIGHTNING_STORM, 0x3B0143 },
            { Opcode.SMSG_START_LOOT_ROLL, 0x3B00B6 },
            { Opcode.SMSG_START_MIRROR_TIMER, 0x3B01AB },
            { Opcode.SMSG_START_TIMER, 0x3B003E },
            { Opcode.SMSG_STOP_ELAPSED_TIMER, 0x3B009E }, // NYI
            { Opcode.SMSG_STOP_MIRROR_TIMER, 0x3B01AD },
            { Opcode.SMSG_STOP_SPEAKERBOT_SOUND, 0x3B020C },
            { Opcode.SMSG_STREAMING_MOVIES, 0x3B003D }, // NYI
            { Opcode.SMSG_SUGGEST_INVITE_INFORM, 0x3B0232 }, // NYI
            { Opcode.SMSG_SUMMON_CANCEL, 0x3B014C }, // NYI
            { Opcode.SMSG_SUMMON_RAID_MEMBER_VALIDATE_FAILED, 0x3B0024 }, // NYI
            { Opcode.SMSG_SUMMON_REQUEST, 0x3B01BC },
            { Opcode.SMSG_SUPERCEDED_SPELLS, 0x51003B },
            { Opcode.SMSG_SUSPEND_COMMS, 0x420002 },
            { Opcode.SMSG_SUSPEND_TOKEN, 0x3B0041 },
            { Opcode.SMSG_SYNC_WOW_ENTITLEMENTS, 0x3B02EE }, // NYI
            { Opcode.SMSG_TALENTS_INVOLUNTARILY_RESET, 0x3B01B3 }, // NYI
            { Opcode.SMSG_TALENT_GROUP_ROLE_CHANGED, 0x3B0023 }, // NYI
            { Opcode.SMSG_TAXI_NODE_STATUS, 0x3B0118 },
            { Opcode.SMSG_TEXT_EMOTE, 0x3B0116 },
            { Opcode.SMSG_THREAT_CLEAR, 0x3B0178 },
            { Opcode.SMSG_THREAT_REMOVE, 0x3B0177 },
            { Opcode.SMSG_THREAT_UPDATE, 0x3B0176 },
            { Opcode.SMSG_TIME_ADJUSTMENT, 0x4C0001 }, // NYI
            { Opcode.SMSG_TIME_SYNC_REQUEST, 0x4C0000 },
            { Opcode.SMSG_TITLE_EARNED, 0x3B0173 },
            { Opcode.SMSG_TITLE_LOST, 0x3B0174 },
            { Opcode.SMSG_TOTEM_CREATED, 0x3B0164 },
            { Opcode.SMSG_TOTEM_MOVED, 0x3B0166 },
            { Opcode.SMSG_TRADE_STATUS, 0x3B0017 },
            { Opcode.SMSG_TRADE_UPDATED, 0x3B0016 },
            { Opcode.SMSG_TRAINER_BUY_FAILED, 0x3B017C },
            { Opcode.SMSG_TRAINER_LIST, 0x3B017B },
            { Opcode.SMSG_TRAIT_CONFIG_COMMIT_FAILED, 0x3B006A },
            { Opcode.SMSG_TRANSFER_ABORTED, 0x3B019F },
            { Opcode.SMSG_TRANSFER_PENDING, 0x3B0066 },
            { Opcode.SMSG_TREASURE_PICKER_RESPONSE, 0x3F0011 }, // NYI
            { Opcode.SMSG_TRIGGER_CINEMATIC, 0x3B0265 },
            { Opcode.SMSG_TRIGGER_MOVIE, 0x3B0167 },
            { Opcode.SMSG_TURN_IN_PETITION_RESULT, 0x3B01E9 },
            { Opcode.SMSG_TUTORIAL_FLAGS, 0x3B0259 },
            { Opcode.SMSG_UI_ACTION, 0x3B0209 }, // NYI
            { Opcode.SMSG_UNDELETE_CHARACTER_RESPONSE, 0x3B0266 },
            { Opcode.SMSG_UNDELETE_COOLDOWN_STATUS_RESPONSE, 0x3B0267 },
            { Opcode.SMSG_UNLEARNED_SPELLS, 0x51003D },
            { Opcode.SMSG_UNLOAD_CHILD_MAP, 0x3B000E }, // NYI
            { Opcode.SMSG_UPDATE_AADC_STATUS_RESPONSE, 0x3B0308 },
            { Opcode.SMSG_UPDATE_ACCOUNT_DATA, 0x3B01A5 },
            { Opcode.SMSG_UPDATE_ACTION_BUTTONS, 0x3B0079 },
            { Opcode.SMSG_UPDATE_BNET_SESSION_KEY, 0x3B02BE },
            { Opcode.SMSG_UPDATE_CELESTIAL_BODY, 0x3B02BA }, // NYI
            { Opcode.SMSG_UPDATE_CHARACTER_FLAGS, 0x3B025F }, // NYI
            { Opcode.SMSG_UPDATE_CHARGE_CATEGORY_COOLDOWN, 0x3B0205 }, // NYI
            { Opcode.SMSG_UPDATE_COOLDOWN, 0x3B0204 }, // NYI
            { Opcode.SMSG_UPDATE_EXPANSION_LEVEL, 0x3B00DF }, // NYI
            { Opcode.SMSG_UPDATE_GAME_TIME_STATE, 0x3B02C1 }, // NYI
            { Opcode.SMSG_UPDATE_INSTANCE_OWNERSHIP, 0x3B0145 },
            { Opcode.SMSG_UPDATE_LAST_INSTANCE, 0x3B0124 },
            { Opcode.SMSG_UPDATE_OBJECT, 0x4B0000 },
            { Opcode.SMSG_UPDATE_PRIMARY_SPEC, 0x3B0071 }, // NYI
            //{ Opcode.SMSG_UPDATE_TALENT_DATA, 0x25DA }, // RETAIL
            { Opcode.SMSG_UPDATE_TALENT_DATA, 0x3B0070 },
            { Opcode.SMSG_UPDATE_WORLD_STATE, 0x3B01E3 },
            { Opcode.SMSG_USERLIST_ADD, 0x40000D },
            { Opcode.SMSG_USERLIST_REMOVE, 0x40000E },
            { Opcode.SMSG_USERLIST_UPDATE, 0x40000F },
            { Opcode.SMSG_USE_EQUIPMENT_SET_RESULT, 0x3B01EA },
            { Opcode.SMSG_VAS_CHECK_TRANSFER_OK_RESPONSE, 0x3B02B5 },
            { Opcode.SMSG_VAS_GET_QUEUE_MINUTES_RESPONSE, 0x3B02B3 },
            { Opcode.SMSG_VAS_GET_SERVICE_STATUS_RESPONSE, 0x3B02B2 },
            { Opcode.SMSG_VAS_PURCHASE_COMPLETE, 0x3B028D },
            { Opcode.SMSG_VAS_PURCHASE_STATE_UPDATE, 0x3B028C },
            { Opcode.SMSG_VENDOR_INVENTORY, 0x3B0051 },
            { Opcode.SMSG_VIGNETTE_UPDATE, 0x4E0010 },
            { Opcode.SMSG_VOICE_CHANNEL_INFO_RESPONSE, 0x3B02B9 }, // NYI
            { Opcode.SMSG_VOICE_CHANNEL_STT_TOKEN_RESPONSE, 0x3B0304 }, // NYI
            { Opcode.SMSG_VOICE_LOGIN_RESPONSE, 0x3B02B8 }, // NYI
            { Opcode.SMSG_VOID_ITEM_SWAP_RESPONSE, 0x520004 },
            { Opcode.SMSG_VOID_STORAGE_CONTENTS, 0x520001 },
            { Opcode.SMSG_VOID_STORAGE_FAILED, 0x520000 },
            { Opcode.SMSG_VOID_STORAGE_TRANSFER_CHANGES, 0x520002 },
            { Opcode.SMSG_VOID_TRANSFER_RESULT, 0x520003 },
            { Opcode.SMSG_WAIT_QUEUE_FINISH, 0x3B0003 },
            { Opcode.SMSG_WAIT_QUEUE_UPDATE, 0x3B0002 },
            { Opcode.SMSG_WARDEN3_DATA, 0x3B000B },
            { Opcode.SMSG_WARDEN3_DISABLED, 0x3B02BC }, // NYI
            { Opcode.SMSG_WARDEN3_ENABLED, 0x3B02BB },
            { Opcode.SMSG_WARFRONT_COMPLETE, 0x3B01F9 }, // NYI
            { Opcode.SMSG_WARGAME_REQUEST_OPPONENT_RESPONSE, 0x410015 }, // NYI
            { Opcode.SMSG_WARGAME_REQUEST_SUCCESSFULLY_SENT_TO_OPPONENT, 0x410013 }, // NYI
            { Opcode.SMSG_WEATHER, 0x3B0142 },
            { Opcode.SMSG_WHO, 0x400002 },
            { Opcode.SMSG_WHO_IS, 0x3B0141 },
            { Opcode.SMSG_WILL_BE_KICKED_FOR_ADDED_SUBSCRIPTION_TIME, 0x3B02C0 }, // NYI
            { Opcode.SMSG_WORLD_QUEST_UPDATE_RESPONSE, 0x4E0017 },
            { Opcode.SMSG_WORLD_SERVER_INFO, 0x3B0046 },
            { Opcode.SMSG_WOW_ENTITLEMENT_NOTIFICATION, 0x3B02EF }, // NYI
            { Opcode.SMSG_WOW_LABS_AREA_INFO, 0x3B031B }, // NYI
            { Opcode.SMSG_WOW_LABS_NOTIFY_PLAYERS_MATCH_END, 0x3B0317 }, // NYI
            { Opcode.SMSG_WOW_LABS_NOTIFY_PLAYERS_MATCH_STATE_CHANGED, 0x3B0318 }, // NYI
            { Opcode.SMSG_WOW_LABS_PARTY_ERROR, 0x3B0324 }, // NYI
            { Opcode.SMSG_WOW_LABS_SET_AREA_ID_RESULT, 0x3B0319 }, // NYI
            { Opcode.SMSG_WOW_LABS_SET_PREDICTION_CIRCLE, 0x3B031D }, // NYI
            { Opcode.SMSG_WOW_LABS_SET_SELECTED_AREA_ID, 0x3B031A }, // NYI
            { Opcode.SMSG_XP_GAIN_ABORTED, 0x3B0062 }, // NYI
            { Opcode.SMSG_XP_GAIN_ENABLED, 0x3B0249 }, // NYI
            { Opcode.SMSG_ZONE_UNDER_ATTACK, 0x400009 },
        };

        private static readonly BiDictionary<Opcode, int> MiscOpcodes = new();
    }
}

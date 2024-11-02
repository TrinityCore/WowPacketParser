using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V1_15_4_56738
{
    public static class Opcodes_1_15_4
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
            { Opcode.CMSG_ACCEPT_GUILD_INVITE, 0x35FD },
            { Opcode.CMSG_ACCEPT_SOCIAL_CONTRACT, 0x3749 }, // NYI
            { Opcode.CMSG_ACCEPT_TRADE, 0x315B },
            { Opcode.CMSG_ACCEPT_WARGAME_INVITE, 0x35E0 }, // NYI
            { Opcode.CMSG_ACCOUNT_NOTIFICATION_ACKNOWLEDGED, 0x3739 }, // NYI
            { Opcode.CMSG_ACTIVATE_TAXI, 0x34AB },
            { Opcode.CMSG_ADDON_LIST, 0x35D8 },
            { Opcode.CMSG_ADD_BATTLENET_FRIEND, 0x3658 }, // NYI
            { Opcode.CMSG_ADD_FRIEND, 0x36D4 },
            { Opcode.CMSG_ADD_IGNORE, 0x36D8 },
            { Opcode.CMSG_ADD_TOY, 0x329B },
            { Opcode.CMSG_ADVENTURE_MAP_START_QUEST, 0x32DA },
            { Opcode.CMSG_ALTER_APPEARANCE, 0x34F5 },
            { Opcode.CMSG_AREA_SPIRIT_HEALER_QUERY, 0x34B0 },
            { Opcode.CMSG_AREA_SPIRIT_HEALER_QUEUE, 0x34B1 },
            { Opcode.CMSG_AREA_TRIGGER, 0x31D8 },
            { Opcode.CMSG_ARENA_TEAM_ACCEPT, 0x36B5 }, // NYI
            { Opcode.CMSG_ARENA_TEAM_DECLINE, 0x36B6 }, // NYI
            { Opcode.CMSG_ARENA_TEAM_DISBAND, 0x36B9 }, // NYI
            { Opcode.CMSG_ARENA_TEAM_LEADER, 0x36BA }, // NYI
            { Opcode.CMSG_ARENA_TEAM_LEAVE, 0x36B7 }, // NYI
            { Opcode.CMSG_ARENA_TEAM_REMOVE, 0x36B8 }, // NYI
            { Opcode.CMSG_ARENA_TEAM_ROSTER, 0x36B4 },
            { Opcode.CMSG_ASSIGN_EQUIPMENT_SET_SPEC, 0x3209 }, // NYI
            { Opcode.CMSG_ATTACK_STOP, 0x3258 },
            { Opcode.CMSG_ATTACK_SWING, 0x3257 },
            { Opcode.CMSG_AUCTIONABLE_TOKEN_SELL, 0x36EA }, // NYI
            { Opcode.CMSG_AUCTIONABLE_TOKEN_SELL_AT_MARKET_PRICE, 0x36EB }, // NYI
            { Opcode.CMSG_AUCTION_HELLO_REQUEST, 0x34CA },
            { Opcode.CMSG_AUCTION_LIST_BIDDER_ITEMS, 0x34D0 },
            { Opcode.CMSG_AUCTION_LIST_ITEMS, 0x34CD },
            { Opcode.CMSG_AUCTION_LIST_OWNER_ITEMS, 0x34CF },
            { Opcode.CMSG_AUCTION_LIST_PENDING_SALES, 0x34D2 },
            { Opcode.CMSG_AUCTION_PLACE_BID, 0x34D1 },
            { Opcode.CMSG_AUCTION_REMOVE_ITEM, 0x34CC },
            { Opcode.CMSG_AUCTION_REPLICATE_ITEMS, 0x34CE },
            { Opcode.CMSG_AUCTION_SELL_ITEM, 0x34CB },
            { Opcode.CMSG_AUTH_CONTINUED_SESSION, 0x377A },
            { Opcode.CMSG_AUTH_SESSION, 0x3779 },
            { Opcode.CMSG_AUTOBANK_ITEM, 0x3997 },
            { Opcode.CMSG_AUTOSTORE_BANK_ITEM, 0x3996 },
            { Opcode.CMSG_AUTO_EQUIP_ITEM, 0x3998 },
            { Opcode.CMSG_AUTO_EQUIP_ITEM_SLOT, 0x399D },
            { Opcode.CMSG_AUTO_GUILD_BANK_ITEM, 0x34B6 },
            { Opcode.CMSG_AUTO_STORE_BAG_ITEM, 0x3999 },
            { Opcode.CMSG_AUTO_STORE_GUILD_BANK_ITEM, 0x34BF },
            { Opcode.CMSG_AZERITE_EMPOWERED_ITEM_SELECT_POWER, 0x32F3 }, // NYI
            { Opcode.CMSG_AZERITE_EMPOWERED_ITEM_VIEWED, 0x32E1 }, // NYI
            { Opcode.CMSG_AZERITE_ESSENCE_ACTIVATE_ESSENCE, 0x32F5 }, // NYI
            { Opcode.CMSG_AZERITE_ESSENCE_UNLOCK_MILESTONE, 0x32F4 }, // NYI
            { Opcode.CMSG_BANKER_ACTIVATE, 0x34B3 },
            { Opcode.CMSG_BATTLEFIELD_LEAVE, 0x3177 },
            { Opcode.CMSG_BATTLEFIELD_LIST, 0x3183 },
            { Opcode.CMSG_BATTLEFIELD_PORT, 0x3527 },
            { Opcode.CMSG_BATTLEMASTER_HELLO, 0x32B3 },
            { Opcode.CMSG_BATTLEMASTER_JOIN, 0x3520 },
            { Opcode.CMSG_BATTLEMASTER_JOIN_ARENA, 0x3521 },
            { Opcode.CMSG_BATTLEMASTER_JOIN_SKIRMISH, 0x3522 },
            { Opcode.CMSG_BATTLENET_CHALLENGE_RESPONSE, 0x36D7 }, // NYI
            { Opcode.CMSG_BATTLENET_REQUEST, 0x36F9 },
            { Opcode.CMSG_BATTLE_PAY_ACK_FAILED_RESPONSE, 0x36D1 },
            { Opcode.CMSG_BATTLE_PAY_CANCEL_OPEN_CHECKOUT, 0x3717 },
            { Opcode.CMSG_BATTLE_PAY_CONFIRM_PURCHASE_RESPONSE, 0x36D0 },
            { Opcode.CMSG_BATTLE_PAY_DISTRIBUTION_ASSIGN_TO_TARGET, 0x36C7 },
            { Opcode.CMSG_BATTLE_PAY_DISTRIBUTION_ASSIGN_VAS, 0x373E },
            { Opcode.CMSG_BATTLE_PAY_GET_PRODUCT_LIST, 0x36C0 },
            { Opcode.CMSG_BATTLE_PAY_GET_PURCHASE_LIST, 0x36C1 },
            { Opcode.CMSG_BATTLE_PAY_OPEN_CHECKOUT, 0x3710 },
            { Opcode.CMSG_BATTLE_PAY_REQUEST_PRICE_INFO, 0x370C },
            { Opcode.CMSG_BATTLE_PAY_START_PURCHASE, 0x36CF },
            { Opcode.CMSG_BATTLE_PAY_START_VAS_PURCHASE, 0x36F6 },
            { Opcode.CMSG_BATTLE_PET_CLEAR_FANFARE, 0x3126 },
            { Opcode.CMSG_BATTLE_PET_REQUEST_JOURNAL, 0x3621 },
            { Opcode.CMSG_BATTLE_PET_REQUEST_JOURNAL_LOCK, 0x3620 },
            { Opcode.CMSG_BATTLE_PET_SET_BATTLE_SLOT, 0x362A },
            { Opcode.CMSG_BATTLE_PET_SET_FLAGS, 0x362D },
            { Opcode.CMSG_BATTLE_PET_SUMMON, 0x3626 },
            { Opcode.CMSG_BATTLE_PET_UPDATE_DISPLAY_NOTIFY, 0x31E2 }, // NYI
            { Opcode.CMSG_BATTLE_PET_UPDATE_NOTIFY, 0x31E1 },
            { Opcode.CMSG_BEGIN_TRADE, 0x3158 },
            { Opcode.CMSG_BINDER_ACTIVATE, 0x34B2 },
            { Opcode.CMSG_BLACK_MARKET_OPEN, 0x352D },
            { Opcode.CMSG_BUG_REPORT, 0x3683 },
            { Opcode.CMSG_BUSY_TRADE, 0x3159 },
            { Opcode.CMSG_BUY_BACK_ITEM, 0x34A4 },
            { Opcode.CMSG_BUY_BANK_SLOT, 0x34B4 },
            { Opcode.CMSG_BUY_ITEM, 0x34A3 },
            { Opcode.CMSG_BUY_STABLE_SLOT, 0x316C }, // NYI
            { Opcode.CMSG_CALENDAR_ADD_EVENT, 0x367B },
            { Opcode.CMSG_CALENDAR_COMMUNITY_INVITE, 0x366F },
            { Opcode.CMSG_CALENDAR_COMPLAIN, 0x3677 },
            { Opcode.CMSG_CALENDAR_COPY_EVENT, 0x3676 },
            { Opcode.CMSG_CALENDAR_EVENT_SIGN_UP, 0x3679 },
            { Opcode.CMSG_CALENDAR_GET, 0x366D },
            { Opcode.CMSG_CALENDAR_GET_EVENT, 0x366E },
            { Opcode.CMSG_CALENDAR_GET_NUM_PENDING, 0x3678 },
            { Opcode.CMSG_CALENDAR_INVITE, 0x3670 },
            { Opcode.CMSG_CALENDAR_MODERATOR_STATUS, 0x3674 },
            { Opcode.CMSG_CALENDAR_REMOVE_EVENT, 0x3675 },
            { Opcode.CMSG_CALENDAR_REMOVE_INVITE, 0x3671 },
            { Opcode.CMSG_CALENDAR_RSVP, 0x3672 },
            { Opcode.CMSG_CALENDAR_STATUS, 0x3673 },
            { Opcode.CMSG_CALENDAR_UPDATE_EVENT, 0x367C },
            { Opcode.CMSG_CANCEL_AURA, 0x31B1 },
            { Opcode.CMSG_CANCEL_AUTO_REPEAT_SPELL, 0x34E7 },
            { Opcode.CMSG_CANCEL_CAST, 0x32A1 },
            { Opcode.CMSG_CANCEL_CHANNELLING, 0x326C },
            { Opcode.CMSG_CANCEL_GROWTH_AURA, 0x3271 },
            { Opcode.CMSG_CANCEL_MOUNT_AURA, 0x3281 },
            { Opcode.CMSG_CANCEL_QUEUED_SPELL, 0x3184 },
            { Opcode.CMSG_CANCEL_TEMP_ENCHANTMENT, 0x34F2 },
            { Opcode.CMSG_CANCEL_TRADE, 0x315D },
            { Opcode.CMSG_CAN_DUEL, 0x3660 },
            { Opcode.CMSG_CAN_REDEEM_TOKEN_FOR_BALANCE, 0x370B }, // NYI
            { Opcode.CMSG_CAST_SPELL, 0x329E },
            { Opcode.CMSG_CHANGE_REALM_TICKET, 0x36FD },
            { Opcode.CMSG_CHANGE_SUB_GROUP, 0x364B },
            { Opcode.CMSG_CHARACTER_CHECK_UPGRADE, 0x36CA }, // NYI
            { Opcode.CMSG_CHARACTER_RENAME_REQUEST, 0x36C5 },
            { Opcode.CMSG_CHARACTER_UPGRADE_MANUAL_UNREVOKE_REQUEST, 0x36C8 }, // NYI
            { Opcode.CMSG_CHARACTER_UPGRADE_START, 0x36C9 }, // NYI
            { Opcode.CMSG_CHAR_CREATE_FINALIZE_REINCARNATION, 0x375B }, // NYI
            { Opcode.CMSG_CHAR_CUSTOMIZE, 0x3688 },
            { Opcode.CMSG_CHAR_DELETE, 0x3699 },
            { Opcode.CMSG_CHAR_RACE_OR_FACTION_CHANGE, 0x368E },
            { Opcode.CMSG_CHAT_ADDON_MESSAGE, 0x3802 },
            { Opcode.CMSG_CHAT_ADDON_MESSAGE_TARGETED, 0x3803 },
            { Opcode.CMSG_CHAT_CHANNEL_ANNOUNCEMENTS, 0x37F7 },
            { Opcode.CMSG_CHAT_CHANNEL_BAN, 0x37F5 },
            { Opcode.CMSG_CHAT_CHANNEL_DECLINE_INVITE, 0x37FA },
            { Opcode.CMSG_CHAT_CHANNEL_DISPLAY_LIST, 0x37EA },
            { Opcode.CMSG_CHAT_CHANNEL_INVITE, 0x37F3 },
            { Opcode.CMSG_CHAT_CHANNEL_KICK, 0x37F4 },
            { Opcode.CMSG_CHAT_CHANNEL_LIST, 0x37E9 },
            { Opcode.CMSG_CHAT_CHANNEL_MODERATOR, 0x37EF },
            { Opcode.CMSG_CHAT_CHANNEL_OWNER, 0x37ED },
            { Opcode.CMSG_CHAT_CHANNEL_PASSWORD, 0x37EB },
            { Opcode.CMSG_CHAT_CHANNEL_SET_OWNER, 0x37EC },
            { Opcode.CMSG_CHAT_CHANNEL_SILENCE_ALL, 0x37F8 },
            { Opcode.CMSG_CHAT_CHANNEL_UNBAN, 0x37F6 },
            { Opcode.CMSG_CHAT_CHANNEL_UNMODERATOR, 0x37F0 },
            { Opcode.CMSG_CHAT_CHANNEL_UNSILENCE_ALL, 0x37F9 },
            { Opcode.CMSG_CHAT_JOIN_CHANNEL, 0x37DC },
            { Opcode.CMSG_CHAT_LEAVE_CHANNEL, 0x37DD },
            { Opcode.CMSG_CHAT_MESSAGE_AFK, 0x37E7 },
            { Opcode.CMSG_CHAT_MESSAGE_CHANNEL, 0x37E3 },
            { Opcode.CMSG_CHAT_MESSAGE_DND, 0x37E8 },
            { Opcode.CMSG_CHAT_MESSAGE_EMOTE, 0x37FC },
            { Opcode.CMSG_CHAT_MESSAGE_GUILD, 0x37E5 },
            { Opcode.CMSG_CHAT_MESSAGE_INSTANCE_CHAT, 0x3800 },
            { Opcode.CMSG_CHAT_MESSAGE_OFFICER, 0x37E6 },
            { Opcode.CMSG_CHAT_MESSAGE_PARTY, 0x37FE },
            { Opcode.CMSG_CHAT_MESSAGE_RAID, 0x37FF },
            { Opcode.CMSG_CHAT_MESSAGE_RAID_WARNING, 0x3801 },
            { Opcode.CMSG_CHAT_MESSAGE_SAY, 0x37FB },
            { Opcode.CMSG_CHAT_MESSAGE_WHISPER, 0x37E4 },
            { Opcode.CMSG_CHAT_MESSAGE_YELL, 0x37FD },
            { Opcode.CMSG_CHAT_REGISTER_ADDON_PREFIXES, 0x37E1 },
            { Opcode.CMSG_CHAT_REPORT_FILTERED, 0x37E0 }, // NYI
            { Opcode.CMSG_CHAT_REPORT_IGNORED, 0x37DF },
            { Opcode.CMSG_CHAT_UNREGISTER_ALL_ADDON_PREFIXES, 0x37E2 },
            { Opcode.CMSG_CHECK_IS_ADVENTURE_MAP_POI_VALID, 0x3249 },
            { Opcode.CMSG_CLEAR_NEW_APPEARANCE, 0x3129 }, // NYI
            { Opcode.CMSG_CLEAR_RAID_MARKER, 0x31A9 },
            { Opcode.CMSG_CLEAR_TRADE_ITEM, 0x315F },
            { Opcode.CMSG_CLIENT_PORT_GRAVEYARD, 0x3529 },
            { Opcode.CMSG_CLOSE_INTERACTION, 0x3493 },
            { Opcode.CMSG_CLOSE_QUEST_CHOICE, 0x32A4 }, // NYI
            { Opcode.CMSG_CLOSE_TRAIT_SYSTEM_INTERACTION, 0x330F }, // NYI
            { Opcode.CMSG_CLUB_FINDER_APPLICATION_RESPONSE, 0x3722 }, // NYI
            { Opcode.CMSG_CLUB_FINDER_GET_APPLICANTS_LIST, 0x3720 }, // NYI
            { Opcode.CMSG_CLUB_FINDER_POST, 0x371D }, // NYI
            { Opcode.CMSG_CLUB_FINDER_REQUEST_CLUBS_DATA, 0x3724 }, // NYI
            { Opcode.CMSG_CLUB_FINDER_REQUEST_CLUBS_LIST, 0x371E }, // NYI
            { Opcode.CMSG_CLUB_FINDER_REQUEST_MEMBERSHIP_TO_CLUB, 0x371F }, // NYI
            { Opcode.CMSG_CLUB_FINDER_REQUEST_PENDING_CLUBS_LIST, 0x3723 }, // NYI
            { Opcode.CMSG_CLUB_FINDER_REQUEST_SUBSCRIBED_CLUB_POSTING_IDS, 0x3725 }, // NYI
            { Opcode.CMSG_CLUB_FINDER_RESPOND_TO_APPLICANT, 0x3721 }, // NYI
            { Opcode.CMSG_CLUB_FINDER_WHISPER_APPLICANT_REQUEST, 0x3740 }, // NYI
            { Opcode.CMSG_CLUB_PRESENCE_SUBSCRIBE, 0x36FB }, // NYI
            { Opcode.CMSG_COLLECTION_ITEM_SET_FAVORITE, 0x3630 },
            { Opcode.CMSG_COMMENTATOR_ENABLE, 0x35F0 }, // NYI
            { Opcode.CMSG_COMMENTATOR_ENTER_INSTANCE, 0x35F4 }, // NYI
            { Opcode.CMSG_COMMENTATOR_EXIT_INSTANCE, 0x35F5 }, // NYI
            { Opcode.CMSG_COMMENTATOR_GET_MAP_INFO, 0x35F1 }, // NYI
            { Opcode.CMSG_COMMENTATOR_GET_PLAYER_COOLDOWNS, 0x35F3 }, // NYI
            { Opcode.CMSG_COMMENTATOR_GET_PLAYER_INFO, 0x35F2 }, // NYI
            { Opcode.CMSG_COMMENTATOR_START_WARGAME, 0x35EF }, // NYI
            { Opcode.CMSG_COMMERCE_TOKEN_GET_COUNT, 0x36E8 }, // NYI
            { Opcode.CMSG_COMMERCE_TOKEN_GET_LOG, 0x36F2 },
            { Opcode.CMSG_COMMERCE_TOKEN_GET_MARKET_PRICE, 0x36E9 },
            { Opcode.CMSG_COMPLAINT, 0x366A },
            { Opcode.CMSG_COMPLETE_CINEMATIC, 0x3547 },
            { Opcode.CMSG_COMPLETE_MOVIE, 0x34DD },
            { Opcode.CMSG_CONFIRM_BARBERS_CHOICE, 0x3210 },
            { Opcode.CMSG_CONFIRM_RESPEC_WIPE, 0x320F },
            { Opcode.CMSG_CONNECT_TO_FAILED, 0x35D4 },
            { Opcode.CMSG_CONSUMABLE_TOKEN_BUY, 0x36ED }, // NYI
            { Opcode.CMSG_CONSUMABLE_TOKEN_BUY_AT_MARKET_PRICE, 0x36EE }, // NYI
            { Opcode.CMSG_CONSUMABLE_TOKEN_CAN_VETERAN_BUY, 0x36EC }, // NYI
            { Opcode.CMSG_CONSUMABLE_TOKEN_REDEEM, 0x36F0 }, // NYI
            { Opcode.CMSG_CONSUMABLE_TOKEN_REDEEM_CONFIRMATION, 0x36F1 }, // NYI
            { Opcode.CMSG_CONTRIBUTION_LAST_UPDATE_REQUEST, 0x355E }, // NYI
            { Opcode.CMSG_CONVERSATION_LINE_STARTED, 0x3548 },
            { Opcode.CMSG_CONVERT_RAID, 0x364D },
            { Opcode.CMSG_CREATE_CHARACTER, 0x3641 },
            { Opcode.CMSG_DB_QUERY_BULK, 0x35E4 },
            { Opcode.CMSG_DECLINE_GUILD_INVITES, 0x351D },
            { Opcode.CMSG_DECLINE_PETITION, 0x3536 },
            { Opcode.CMSG_DELETE_EQUIPMENT_SET, 0x350A },
            { Opcode.CMSG_DEL_FRIEND, 0x36D5 },
            { Opcode.CMSG_DEL_IGNORE, 0x36D9 },
            { Opcode.CMSG_DESTROY_ITEM, 0x3295 },
            { Opcode.CMSG_DF_BOOT_PLAYER_VOTE, 0x3617 },
            { Opcode.CMSG_DF_GET_JOIN_STATUS, 0x3615 },
            { Opcode.CMSG_DF_GET_SYSTEM_INFO, 0x3614 },
            { Opcode.CMSG_DF_JOIN, 0x360A },
            { Opcode.CMSG_DF_LEAVE, 0x3613 },
            { Opcode.CMSG_DF_PROPOSAL_RESPONSE, 0x3608 },
            { Opcode.CMSG_DF_READY_CHECK_RESPONSE, 0x361B }, // NYI
            { Opcode.CMSG_DF_SET_ROLES, 0x3616 },
            { Opcode.CMSG_DF_TELEPORT, 0x3618 },
            { Opcode.CMSG_DISCARDED_TIME_SYNC_ACKS, 0x3A41 }, // NYI
            { Opcode.CMSG_DISMISS_CRITTER, 0x34F9 },
            { Opcode.CMSG_DO_COUNTDOWN, 0x371C }, // NYI
            { Opcode.CMSG_DO_READY_CHECK, 0x3631 },
            { Opcode.CMSG_DUEL_RESPONSE, 0x34E2 },
            { Opcode.CMSG_EJECT_PASSENGER, 0x323E },
            { Opcode.CMSG_EMOTE, 0x3543 },
            { Opcode.CMSG_ENABLE_NAGLE, 0x377F },
            { Opcode.CMSG_ENABLE_TAXI_NODE, 0x34A9 },
            { Opcode.CMSG_ENGINE_SURVEY, 0x36E7 }, // NYI
            { Opcode.CMSG_ENTER_ENCRYPTED_MODE_ACK, 0x377B },
            { Opcode.CMSG_ENUM_CHARACTERS, 0x35E8 },
            { Opcode.CMSG_ENUM_CHARACTERS_DELETED_BY_CLIENT, 0x36E1 },
            { Opcode.CMSG_FAR_SIGHT, 0x34E8 },
            { Opcode.CMSG_GAME_EVENT_DEBUG_DISABLE, 0x31B3 }, // NYI
            { Opcode.CMSG_GAME_EVENT_DEBUG_ENABLE, 0x31B4 }, // NYI
            { Opcode.CMSG_GAME_OBJ_REPORT_USE, 0x34EF },
            { Opcode.CMSG_GAME_OBJ_USE, 0x34EE },
            { Opcode.CMSG_GENERATE_RANDOM_CHARACTER_NAME, 0x35E7 },
            { Opcode.CMSG_GET_ACCOUNT_CHARACTER_LIST, 0x36BB }, // NYI
            { Opcode.CMSG_GET_ACCOUNT_NOTIFICATIONS, 0x3738 }, // NYI
            { Opcode.CMSG_GET_ITEM_PURCHASE_DATA, 0x3531 },
            { Opcode.CMSG_GET_MIRROR_IMAGE_DATA, 0x3299 },
            { Opcode.CMSG_GET_PVP_OPTIONS_ENABLED, 0x35EE },
            { Opcode.CMSG_GET_REMAINING_GAME_TIME, 0x36EF }, // NYI
            { Opcode.CMSG_GET_UNDELETE_CHARACTER_COOLDOWN_STATUS, 0x36E3 },
            { Opcode.CMSG_GET_VAS_ACCOUNT_CHARACTER_LIST, 0x36F4 },
            { Opcode.CMSG_GET_VAS_TRANSFER_TARGET_REALM_LIST, 0x36F5 },
            { Opcode.CMSG_GM_TICKET_ACKNOWLEDGE_SURVEY, 0x368C },
            { Opcode.CMSG_GM_TICKET_GET_CASE_STATUS, 0x368B },
            { Opcode.CMSG_GM_TICKET_GET_SYSTEM_STATUS, 0x368A },
            { Opcode.CMSG_GOSSIP_SELECT_OPTION, 0x3494 },
            { Opcode.CMSG_GUILD_ADD_BATTLENET_FRIEND, 0x5036 }, // NYI
            { Opcode.CMSG_GUILD_ADD_RANK, 0x501B },
            { Opcode.CMSG_GUILD_ASSIGN_MEMBER_RANK, 0x5016 },
            { Opcode.CMSG_GUILD_AUTO_DECLINE_INVITATION, 0x5018 }, // NYI
            { Opcode.CMSG_GUILD_BANK_ACTIVATE, 0x34B5 },
            { Opcode.CMSG_GUILD_BANK_BUY_TAB, 0x34C3 },
            { Opcode.CMSG_GUILD_BANK_DEPOSIT_MONEY, 0x34C5 },
            { Opcode.CMSG_GUILD_BANK_LOG_QUERY, 0x502F },
            { Opcode.CMSG_GUILD_BANK_QUERY_TAB, 0x34C2 },
            { Opcode.CMSG_GUILD_BANK_REMAINING_WITHDRAW_MONEY_QUERY, 0x5030 },
            { Opcode.CMSG_GUILD_BANK_SET_TAB_TEXT, 0x5033 },
            { Opcode.CMSG_GUILD_BANK_TEXT_QUERY, 0x5034 },
            { Opcode.CMSG_GUILD_BANK_UPDATE_TAB, 0x34C4 },
            { Opcode.CMSG_GUILD_BANK_WITHDRAW_MONEY, 0x34C6 },
            { Opcode.CMSG_GUILD_CHALLENGE_UPDATE_REQUEST, 0x502D },
            { Opcode.CMSG_GUILD_CHANGE_NAME_REQUEST, 0x502E },
            { Opcode.CMSG_GUILD_DECLINE_INVITATION, 0x5017 },
            { Opcode.CMSG_GUILD_DELETE, 0x501F },
            { Opcode.CMSG_GUILD_DELETE_RANK, 0x501C },
            { Opcode.CMSG_GUILD_DEMOTE_MEMBER, 0x5015 },
            { Opcode.CMSG_GUILD_EVENT_LOG_QUERY, 0x5032 },
            { Opcode.CMSG_GUILD_GET_ACHIEVEMENT_MEMBERS, 0x5028 },
            { Opcode.CMSG_GUILD_GET_RANKS, 0x5024 },
            { Opcode.CMSG_GUILD_GET_ROSTER, 0x502A },
            { Opcode.CMSG_GUILD_INVITE_BY_NAME, 0x3607 },
            { Opcode.CMSG_GUILD_LEAVE, 0x5019 },
            { Opcode.CMSG_GUILD_NEWS_UPDATE_STICKY, 0x5025 },
            { Opcode.CMSG_GUILD_OFFICER_REMOVE_MEMBER, 0x501A },
            { Opcode.CMSG_GUILD_PERMISSIONS_QUERY, 0x5031 },
            { Opcode.CMSG_GUILD_PROMOTE_MEMBER, 0x5014 },
            { Opcode.CMSG_GUILD_QUERY_MEMBER_RECIPES, 0x5020 }, // NYI
            { Opcode.CMSG_GUILD_QUERY_NEWS, 0x5023 },
            { Opcode.CMSG_GUILD_QUERY_RECIPES, 0x5021 }, // NYI
            { Opcode.CMSG_GUILD_REPLACE_GUILD_MASTER, 0x5035 },
            { Opcode.CMSG_GUILD_SET_ACHIEVEMENT_TRACKING, 0x5026 },
            { Opcode.CMSG_GUILD_SET_FOCUSED_ACHIEVEMENT, 0x5027 },
            { Opcode.CMSG_GUILD_SET_GUILD_MASTER, 0x36CC },
            { Opcode.CMSG_GUILD_SET_MEMBER_NOTE, 0x5029 },
            { Opcode.CMSG_GUILD_SET_RANK_PERMISSIONS, 0x501E },
            { Opcode.CMSG_GUILD_SHIFT_RANK, 0x501D },
            { Opcode.CMSG_GUILD_UPDATE_INFO_TEXT, 0x502C },
            { Opcode.CMSG_GUILD_UPDATE_MOTD_TEXT, 0x502B },
            { Opcode.CMSG_HEARTH_AND_RESURRECT, 0x3506 },
            { Opcode.CMSG_HOTFIX_REQUEST, 0x35E5 },
            { Opcode.CMSG_IGNORE_TRADE, 0x315A },
            { Opcode.CMSG_INITIATE_ROLE_POLL, 0x35DA },
            { Opcode.CMSG_INITIATE_TRADE, 0x3157 },
            { Opcode.CMSG_INSPECT, 0x352B },
            { Opcode.CMSG_INSPECT_PVP, 0x369F }, // NYI
            { Opcode.CMSG_INSTANCE_LOCK_RESPONSE, 0x350B },
            { Opcode.CMSG_ITEM_PURCHASE_REFUND, 0x3532 },
            { Opcode.CMSG_ITEM_TEXT_QUERY, 0x32C6 },
            { Opcode.CMSG_JOIN_RATED_BATTLEGROUND, 0x317D }, // NYI
            { Opcode.CMSG_KEEP_ALIVE, 0x367D },
            { Opcode.CMSG_KEYBOUND_OVERRIDE, 0x3225 },
            { Opcode.CMSG_LATENCY_REPORT, 0x3785 }, // NYI
            { Opcode.CMSG_LEARN_PREVIEW_TALENTS, 0x3555 },
            { Opcode.CMSG_LEARN_PREVIEW_TALENTS_PET, 0x3557 }, // NYI
            { Opcode.CMSG_LEARN_TALENT, 0x3554 },
            { Opcode.CMSG_LEAVE_GROUP, 0x3648 },
            { Opcode.CMSG_LFG_LIST_APPLY_TO_GROUP, 0x360E }, // NYI
            { Opcode.CMSG_LFG_LIST_CANCEL_APPLICATION, 0x360F }, // NYI
            { Opcode.CMSG_LFG_LIST_DECLINE_APPLICANT, 0x3610 }, // NYI
            { Opcode.CMSG_LFG_LIST_GET_STATUS, 0x360C },
            { Opcode.CMSG_LFG_LIST_INVITE_APPLICANT, 0x3611 }, // NYI
            { Opcode.CMSG_LFG_LIST_INVITE_RESPONSE, 0x3612 }, // NYI
            { Opcode.CMSG_LFG_LIST_JOIN, 0x32F1 }, // NYI
            { Opcode.CMSG_LFG_LIST_LEAVE, 0x360B }, // NYI
            { Opcode.CMSG_LFG_LIST_SEARCH, 0x360D }, // NYI
            { Opcode.CMSG_LFG_LIST_SET_ROLES, 0x3303 }, // NYI
            { Opcode.CMSG_LFG_LIST_UPDATE_REQUEST, 0x32F2 }, // NYI
            { Opcode.CMSG_LIST_INVENTORY, 0x34A1 },
            { Opcode.CMSG_LIVE_REGION_ACCOUNT_RESTORE, 0x36BE }, // NYI
            { Opcode.CMSG_LIVE_REGION_CHARACTER_COPY, 0x36BD }, // NYI
            { Opcode.CMSG_LIVE_REGION_GET_ACCOUNT_CHARACTER_LIST, 0x36BC }, // NYI
            { Opcode.CMSG_LOADING_SCREEN_NOTIFY, 0x35F8 },
            { Opcode.CMSG_LOGOUT_CANCEL, 0x34D8 },
            { Opcode.CMSG_LOGOUT_INSTANT, 0x34D9 }, // NYI
            { Opcode.CMSG_LOGOUT_REQUEST, 0x34D6 },
            { Opcode.CMSG_LOG_DISCONNECT, 0x377D },
            { Opcode.CMSG_LOG_STREAMING_ERROR, 0x3781 }, // NYI
            { Opcode.CMSG_LOOT_ITEM, 0x3213 },
            { Opcode.CMSG_LOOT_MONEY, 0x3212 },
            { Opcode.CMSG_LOOT_RELEASE, 0x3215 },
            { Opcode.CMSG_LOOT_ROLL, 0x3216 },
            { Opcode.CMSG_LOOT_UNIT, 0x3211 },
            { Opcode.CMSG_LOW_LEVEL_RAID1, 0x3512 }, // NYI
            { Opcode.CMSG_LOW_LEVEL_RAID2, 0x369D }, // NYI
            { Opcode.CMSG_MAIL_CREATE_TEXT_ITEM, 0x353D },
            { Opcode.CMSG_MAIL_DELETE, 0x3227 },
            { Opcode.CMSG_MAIL_GET_LIST, 0x3538 },
            { Opcode.CMSG_MAIL_MARK_AS_READ, 0x353C },
            { Opcode.CMSG_MAIL_RETURN_TO_SENDER, 0x3654 },
            { Opcode.CMSG_MAIL_TAKE_ITEM, 0x353A },
            { Opcode.CMSG_MAIL_TAKE_MONEY, 0x3539 },
            { Opcode.CMSG_MAKE_CONTITIONAL_APPEARANCE_PERMANENT, 0x322A }, // NYI
            { Opcode.CMSG_MASTER_LOOT_ITEM, 0x3214 },
            { Opcode.CMSG_MERGE_GUILD_BANK_ITEM_WITH_GUILD_BANK_ITEM, 0x34C0 },
            { Opcode.CMSG_MERGE_GUILD_BANK_ITEM_WITH_ITEM, 0x34BD },
            { Opcode.CMSG_MERGE_ITEM_WITH_GUILD_BANK_ITEM, 0x34BB },
            { Opcode.CMSG_MINIMAP_PING, 0x364A },
            { Opcode.CMSG_MISSILE_TRAJECTORY_COLLISION, 0x318F },
            { Opcode.CMSG_MOUNT_CLEAR_FANFARE, 0x3127 }, // NYI
            { Opcode.CMSG_MOUNT_SET_FAVORITE, 0x362F },
            { Opcode.CMSG_MOUNT_SPECIAL_ANIM, 0x3282 },
            { Opcode.CMSG_MOVE_ADD_IMPULSE_ACK, 0x3A50 }, // NYI
            { Opcode.CMSG_MOVE_APPLY_INERTIA_ACK, 0x3A4E }, // NYI
            { Opcode.CMSG_MOVE_APPLY_MOVEMENT_FORCE_ACK, 0x3A15 },
            { Opcode.CMSG_MOVE_CHANGE_TRANSPORT, 0x3A2F },
            { Opcode.CMSG_MOVE_CHANGE_VEHICLE_SEATS, 0x3A34 },
            { Opcode.CMSG_MOVE_COLLISION_DISABLE_ACK, 0x3A39 },
            { Opcode.CMSG_MOVE_COLLISION_ENABLE_ACK, 0x3A3A },
            { Opcode.CMSG_MOVE_DISMISS_VEHICLE, 0x3A33 },
            { Opcode.CMSG_MOVE_DOUBLE_JUMP, 0x39EB },
            { Opcode.CMSG_MOVE_ENABLE_DOUBLE_JUMP_ACK, 0x3A1E },
            { Opcode.CMSG_MOVE_ENABLE_SWIM_TO_FLY_TRANS_ACK, 0x3A24 },
            { Opcode.CMSG_MOVE_FALL_LAND, 0x39FB },
            { Opcode.CMSG_MOVE_FALL_RESET, 0x3A19 },
            { Opcode.CMSG_MOVE_FEATHER_FALL_ACK, 0x3A1C },
            { Opcode.CMSG_MOVE_FORCE_FLIGHT_BACK_SPEED_CHANGE_ACK, 0x3A2E },
            { Opcode.CMSG_MOVE_FORCE_FLIGHT_SPEED_CHANGE_ACK, 0x3A2D },
            { Opcode.CMSG_MOVE_FORCE_PITCH_RATE_CHANGE_ACK, 0x3A32 },
            { Opcode.CMSG_MOVE_FORCE_ROOT_ACK, 0x3A0E },
            { Opcode.CMSG_MOVE_FORCE_RUN_BACK_SPEED_CHANGE_ACK, 0x3A0C },
            { Opcode.CMSG_MOVE_FORCE_RUN_SPEED_CHANGE_ACK, 0x3A0B },
            { Opcode.CMSG_MOVE_FORCE_SWIM_BACK_SPEED_CHANGE_ACK, 0x3A22 },
            { Opcode.CMSG_MOVE_FORCE_SWIM_SPEED_CHANGE_ACK, 0x3A0D },
            { Opcode.CMSG_MOVE_FORCE_TURN_RATE_CHANGE_ACK, 0x3A23 },
            { Opcode.CMSG_MOVE_FORCE_UNROOT_ACK, 0x3A0F },
            { Opcode.CMSG_MOVE_FORCE_WALK_SPEED_CHANGE_ACK, 0x3A21 },
            { Opcode.CMSG_MOVE_GRAVITY_DISABLE_ACK, 0x3A35 },
            { Opcode.CMSG_MOVE_GRAVITY_ENABLE_ACK, 0x3A36 },
            { Opcode.CMSG_MOVE_GUILD_BANK_ITEM, 0x34BA },
            { Opcode.CMSG_MOVE_HEARTBEAT, 0x3A10 },
            { Opcode.CMSG_MOVE_HOVER_ACK, 0x3A13 },
            { Opcode.CMSG_MOVE_INERTIA_DISABLE_ACK, 0x3A37 },
            { Opcode.CMSG_MOVE_INERTIA_ENABLE_ACK, 0x3A38 },
            { Opcode.CMSG_MOVE_INIT_ACTIVE_MOVER_COMPLETE, 0x3A46 },
            { Opcode.CMSG_MOVE_JUMP, 0x39EA },
            { Opcode.CMSG_MOVE_KNOCK_BACK_ACK, 0x3A12 },
            { Opcode.CMSG_MOVE_REMOVE_INERTIA_ACK, 0x3A4F }, // NYI
            { Opcode.CMSG_MOVE_REMOVE_MOVEMENT_FORCES, 0x3A17 },
            { Opcode.CMSG_MOVE_REMOVE_MOVEMENT_FORCE_ACK, 0x3A16 },
            { Opcode.CMSG_MOVE_SEAMLESS_TRANSFER_COMPLETE, 0x3A44 }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLY, 0x3A52 }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_ADD_IMPULSE_MAX_SPEED_ACK, 0x3A58 }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_AIR_FRICTION_ACK, 0x3A53 }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_BANKING_RATE_ACK, 0x3A59 }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_DOUBLE_JUMP_VEL_MOD_ACK, 0x3A56 }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_GLIDE_START_MIN_HEIGHT_ACK, 0x3A57 }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_LAUNCH_SPEED_COEFFICIENT_ACK, 0x3A60 }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_LIFT_COEFFICIENT_ACK, 0x3A55 }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_MAX_VEL_ACK, 0x3A54 }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_OVER_MAX_DECELERATION_ACK, 0x3A5E }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_PITCHING_RATE_DOWN_ACK, 0x3A5A }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_PITCHING_RATE_UP_ACK, 0x3A5B }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_SURFACE_FRICTION_ACK, 0x3A5D }, // NYI
            { Opcode.CMSG_MOVE_SET_ADV_FLYING_TURN_VELOCITY_THRESHOLD_ACK, 0x3A5C }, // NYI
            { Opcode.CMSG_MOVE_SET_CAN_ADV_FLY_ACK, 0x3A51 }, // NYI
            { Opcode.CMSG_MOVE_SET_CAN_FLY_ACK, 0x3A27 },
            { Opcode.CMSG_MOVE_SET_CAN_TURN_WHILE_FALLING_ACK, 0x3A25 },
            { Opcode.CMSG_MOVE_SET_COLLISION_HEIGHT_ACK, 0x3A3B },
            { Opcode.CMSG_MOVE_SET_FACING, 0x3A09 },
            { Opcode.CMSG_MOVE_SET_FACING_HEARTBEAT, 0x3A5F },
            { Opcode.CMSG_MOVE_SET_FLY, 0x3A28 },
            { Opcode.CMSG_MOVE_SET_IGNORE_MOVEMENT_FORCES_ACK, 0x3A26 },
            { Opcode.CMSG_MOVE_SET_MOD_MOVEMENT_FORCE_MAGNITUDE_ACK, 0x3A42 },
            { Opcode.CMSG_MOVE_SET_PITCH, 0x3A0A },
            { Opcode.CMSG_MOVE_SET_RUN_MODE, 0x39F2 },
            { Opcode.CMSG_MOVE_SET_VEHICLE_REC_ID_ACK, 0x3A14 },
            { Opcode.CMSG_MOVE_SET_WALK_MODE, 0x39F3 },
            { Opcode.CMSG_MOVE_SPLINE_DONE, 0x3A18 },
            { Opcode.CMSG_MOVE_START_ASCEND, 0x3A29 },
            { Opcode.CMSG_MOVE_START_BACKWARD, 0x39E5 },
            { Opcode.CMSG_MOVE_START_DESCEND, 0x3A30 },
            { Opcode.CMSG_MOVE_START_FORWARD, 0x39E4 },
            { Opcode.CMSG_MOVE_START_PITCH_DOWN, 0x39F0 },
            { Opcode.CMSG_MOVE_START_PITCH_UP, 0x39EF },
            { Opcode.CMSG_MOVE_START_STRAFE_LEFT, 0x39E7 },
            { Opcode.CMSG_MOVE_START_STRAFE_RIGHT, 0x39E8 },
            { Opcode.CMSG_MOVE_START_SWIM, 0x39FC },
            { Opcode.CMSG_MOVE_START_TURN_LEFT, 0x39EC },
            { Opcode.CMSG_MOVE_START_TURN_RIGHT, 0x39ED },
            { Opcode.CMSG_MOVE_STOP, 0x39E6 },
            { Opcode.CMSG_MOVE_STOP_ASCEND, 0x3A2A },
            { Opcode.CMSG_MOVE_STOP_PITCH, 0x39F1 },
            { Opcode.CMSG_MOVE_STOP_STRAFE, 0x39E9 },
            { Opcode.CMSG_MOVE_STOP_SWIM, 0x39FD },
            { Opcode.CMSG_MOVE_STOP_TURN, 0x39EE },
            { Opcode.CMSG_MOVE_TELEPORT_ACK, 0x39FA },
            { Opcode.CMSG_MOVE_TIME_SKIPPED, 0x3A1B },
            { Opcode.CMSG_MOVE_UPDATE_FALL_SPEED, 0x3A1A },
            { Opcode.CMSG_MOVE_WATER_WALK_ACK, 0x3A1D },
            { Opcode.CMSG_NEXT_CINEMATIC_CAMERA, 0x3546 },
            { Opcode.CMSG_OBJECT_UPDATE_FAILED, 0x3185 },
            { Opcode.CMSG_OBJECT_UPDATE_RESCUED, 0x3186 },
            { Opcode.CMSG_OFFER_PETITION, 0x32FE },
            { Opcode.CMSG_OPENING_CINEMATIC, 0x3545 },
            { Opcode.CMSG_OPEN_ITEM, 0x32C7 },
            { Opcode.CMSG_OPT_OUT_OF_LOOT, 0x34F6 },
            { Opcode.CMSG_OVERRIDE_SCREEN_FLASH, 0x351E },
            { Opcode.CMSG_PARTY_INVITE, 0x3603 },
            { Opcode.CMSG_PARTY_INVITE_RESPONSE, 0x3605 },
            { Opcode.CMSG_PARTY_UNINVITE, 0x3646 },
            { Opcode.CMSG_PETITION_BUY, 0x34C8 },
            { Opcode.CMSG_PETITION_RENAME_GUILD, 0x36CD },
            { Opcode.CMSG_PETITION_SHOW_LIST, 0x34C7 },
            { Opcode.CMSG_PETITION_SHOW_SIGNATURES, 0x34C9 },
            { Opcode.CMSG_PET_ABANDON, 0x348D },
            { Opcode.CMSG_PET_ACTION, 0x348B },
            { Opcode.CMSG_PET_CANCEL_AURA, 0x348E },
            { Opcode.CMSG_PET_CAST_SPELL, 0x329D },
            { Opcode.CMSG_PET_LEARN_TALENT, 0x3556 }, // NYI
            { Opcode.CMSG_PET_RENAME, 0x3682 },
            { Opcode.CMSG_PET_SET_ACTION, 0x348A },
            { Opcode.CMSG_PET_SPELL_AUTOCAST, 0x348F },
            { Opcode.CMSG_PET_STOP_ATTACK, 0x348C },
            { Opcode.CMSG_PING, 0x377C },
            { Opcode.CMSG_PLAYER_LOGIN, 0x35EA },
            { Opcode.CMSG_PUSH_QUEST_TO_PARTY, 0x349F },
            { Opcode.CMSG_PVP_LOG_DATA, 0x3181 },
            { Opcode.CMSG_QUERY_ARENA_TEAM, 0x36A0 }, // NYI
            { Opcode.CMSG_QUERY_BATTLE_PET_NAME, 0x3278 },
            { Opcode.CMSG_QUERY_CORPSE_LOCATION_FROM_CLIENT, 0x365E },
            { Opcode.CMSG_QUERY_CORPSE_TRANSPORT, 0x365F },
            { Opcode.CMSG_QUERY_COUNTDOWN_TIMER, 0x31AC },
            { Opcode.CMSG_QUERY_CREATURE, 0x3272 },
            { Opcode.CMSG_QUERY_GAME_OBJECT, 0x3273 },
            { Opcode.CMSG_QUERY_GUILD_INFO, 0x3687 },
            { Opcode.CMSG_QUERY_INSPECT_ACHIEVEMENTS, 0x3500 },
            { Opcode.CMSG_QUERY_NEXT_MAIL_TIME, 0x353B },
            { Opcode.CMSG_QUERY_NPC_TEXT, 0x3274 },
            { Opcode.CMSG_QUERY_PAGE_TEXT, 0x3276 },
            { Opcode.CMSG_QUERY_PETITION, 0x3279 },
            { Opcode.CMSG_QUERY_PET_NAME, 0x3277 },
            { Opcode.CMSG_QUERY_PLAYER_NAMES, 0x3786 },
            { Opcode.CMSG_QUERY_PLAYER_NAMES_FOR_COMMUNITY, 0x3784 }, // NYI
            { Opcode.CMSG_QUERY_PLAYER_NAME_BY_COMMUNITY_ID, 0x3783 }, // NYI
            { Opcode.CMSG_QUERY_QUEST_COMPLETION_NPCS, 0x3179 },
            { Opcode.CMSG_QUERY_QUEST_INFO, 0x3275 },
            { Opcode.CMSG_QUERY_QUEST_ITEM_USABILITY, 0x317A }, // NYI
            { Opcode.CMSG_QUERY_REALM_NAME, 0x3686 },
            { Opcode.CMSG_QUERY_TIME, 0x34D5 },
            { Opcode.CMSG_QUERY_TREASURE_PICKER, 0x32DD }, // NYI
            { Opcode.CMSG_QUERY_VOID_STORAGE, 0x31A5 },
            { Opcode.CMSG_QUEST_CONFIRM_ACCEPT, 0x349E },
            { Opcode.CMSG_QUEST_GIVER_ACCEPT_QUEST, 0x3498 },
            { Opcode.CMSG_QUEST_GIVER_CHOOSE_REWARD, 0x349A },
            { Opcode.CMSG_QUEST_GIVER_CLOSE_QUEST, 0x354B },
            { Opcode.CMSG_QUEST_GIVER_COMPLETE_QUEST, 0x3499 },
            { Opcode.CMSG_QUEST_GIVER_HELLO, 0x3496 },
            { Opcode.CMSG_QUEST_GIVER_QUERY_QUEST, 0x3497 },
            { Opcode.CMSG_QUEST_GIVER_REQUEST_REWARD, 0x349B },
            { Opcode.CMSG_QUEST_GIVER_STATUS_MULTIPLE_QUERY, 0x349D },
            { Opcode.CMSG_QUEST_GIVER_STATUS_QUERY, 0x349C },
            { Opcode.CMSG_QUEST_GIVER_STATUS_TRACKED_QUERY, 0x356E },
            { Opcode.CMSG_QUEST_LOG_REMOVE_QUEST, 0x3530 },
            { Opcode.CMSG_QUEST_POI_QUERY, 0x36AE },
            { Opcode.CMSG_QUEST_PUSH_RESULT, 0x34A0 },
            { Opcode.CMSG_QUEUED_MESSAGES_END, 0x3780 },
            { Opcode.CMSG_QUICK_JOIN_AUTO_ACCEPT_REQUESTS, 0x3709 }, // NYI
            { Opcode.CMSG_QUICK_JOIN_REQUEST_INVITE, 0x3708 }, // NYI
            { Opcode.CMSG_QUICK_JOIN_RESPOND_TO_INVITE, 0x3707 }, // NYI
            { Opcode.CMSG_RANDOM_ROLL, 0x3653 },
            { Opcode.CMSG_READY_CHECK_RESPONSE, 0x3632 },
            { Opcode.CMSG_READ_ITEM, 0x32C8 },
            { Opcode.CMSG_RECLAIM_CORPSE, 0x34DB },
            { Opcode.CMSG_REFORGE_ITEM, 0x3156 },
            { Opcode.CMSG_REMOVE_GLYPH, 0x3301 }, // NYI
            { Opcode.CMSG_REMOVE_NEW_ITEM, 0x32E0 },
            { Opcode.CMSG_REORDER_CHARACTERS, 0x35E9 },
            { Opcode.CMSG_REPAIR_ITEM, 0x34EC },
            { Opcode.CMSG_REPOP_REQUEST, 0x3528 },
            { Opcode.CMSG_REPORT_CLIENT_VARIABLES, 0x3703 }, // NYI
            { Opcode.CMSG_REPORT_ENABLED_ADDONS, 0x3702 }, // NYI
            { Opcode.CMSG_REPORT_FROZEN_WHILE_LOADING_MAP, 0x36A6 }, // NYI
            { Opcode.CMSG_REPORT_KEYBINDING_EXECUTION_COUNTS, 0x3704 }, // NYI
            { Opcode.CMSG_REPORT_PVP_PLAYER_AFK, 0x34F4 },
            { Opcode.CMSG_REPORT_SERVER_LAG, 0x32FC }, // NYI
            { Opcode.CMSG_REQUEST_ACCOUNT_DATA, 0x3690 },
            { Opcode.CMSG_REQUEST_AREA_POI_UPDATE, 0x32DF }, // NYI
            { Opcode.CMSG_REQUEST_BATTLEFIELD_STATUS, 0x35DC },
            { Opcode.CMSG_REQUEST_CEMETERY_LIST, 0x317B },
            { Opcode.CMSG_REQUEST_CROWD_CONTROL_SPELL, 0x352C }, // NYI
            { Opcode.CMSG_REQUEST_FORCED_REACTIONS, 0x3207 },
            { Opcode.CMSG_REQUEST_GUILD_PARTY_STATE, 0x31AB },
            { Opcode.CMSG_REQUEST_GUILD_REWARDS_LIST, 0x31AA },
            { Opcode.CMSG_REQUEST_HONOR_STATS, 0x3180 }, // NYI
            { Opcode.CMSG_REQUEST_LFG_LIST_BLACKLIST, 0x32A6 },
            { Opcode.CMSG_REQUEST_PARTY_JOIN_UPDATES, 0x35F7 },
            { Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS, 0x3652 },
            { Opcode.CMSG_REQUEST_PET_INFO, 0x3490 },
            { Opcode.CMSG_REQUEST_PLAYED_TIME, 0x327C },
            { Opcode.CMSG_REQUEST_PVP_REWARDS, 0x3198 },
            { Opcode.CMSG_REQUEST_RAID_INFO, 0x36CE },
            { Opcode.CMSG_REQUEST_RATED_PVP_INFO, 0x35E3 },
            { Opcode.CMSG_REQUEST_SCHEDULED_PVP_INFO, 0x3199 }, // NYI
            { Opcode.CMSG_REQUEST_STABLED_PETS, 0x3491 },
            { Opcode.CMSG_REQUEST_VEHICLE_EXIT, 0x3239 },
            { Opcode.CMSG_REQUEST_VEHICLE_NEXT_SEAT, 0x323B },
            { Opcode.CMSG_REQUEST_VEHICLE_PREV_SEAT, 0x323A },
            { Opcode.CMSG_REQUEST_VEHICLE_SWITCH_SEAT, 0x323C },
            { Opcode.CMSG_REQUEST_WORLD_QUEST_UPDATE, 0x32DE },
            { Opcode.CMSG_RESET_INSTANCES, 0x3666 },
            { Opcode.CMSG_RESURRECT_RESPONSE, 0x3681 },
            { Opcode.CMSG_RIDE_VEHICLE_INTERACT, 0x323D },
            { Opcode.CMSG_SAVE_ACCOUNT_DATA_EXPORT, 0x374D }, // NYI
            { Opcode.CMSG_SAVE_CUF_PROFILES, 0x3190 },
            { Opcode.CMSG_SAVE_EQUIPMENT_SET, 0x3509 },
            { Opcode.CMSG_SAVE_GUILD_EMBLEM, 0x32AA },
            { Opcode.CMSG_SCENE_PLAYBACK_CANCELED, 0x3222 },
            { Opcode.CMSG_SCENE_PLAYBACK_COMPLETE, 0x3221 },
            { Opcode.CMSG_SCENE_TRIGGER_EVENT, 0x3223 },
            { Opcode.CMSG_SELF_RES, 0x3533 },
            { Opcode.CMSG_SELL_ITEM, 0x34A2 },
            { Opcode.CMSG_SEND_CONTACT_LIST, 0x36D3 },
            { Opcode.CMSG_SEND_MAIL, 0x35FA },
            { Opcode.CMSG_SEND_TEXT_EMOTE, 0x3488 },
            { Opcode.CMSG_SERVER_TIME_OFFSET_REQUEST, 0x3698 },
            { Opcode.CMSG_SET_ACTION_BAR_TOGGLES, 0x3534 },
            { Opcode.CMSG_SET_ACTION_BUTTON, 0x3560 },
            { Opcode.CMSG_SET_ACTIVE_MOVER, 0x3A3C },
            { Opcode.CMSG_SET_ADVANCED_COMBAT_LOGGING, 0x32B6 },
            { Opcode.CMSG_SET_AMMO, 0x3561 }, // NYI
            { Opcode.CMSG_SET_ASSISTANT_LEADER, 0x364E },
            { Opcode.CMSG_SET_CONTACT_NOTES, 0x36D6 },
            { Opcode.CMSG_SET_CURRENCY_FLAGS, 0x316E }, // NYI
            { Opcode.CMSG_SET_DIFFICULTY_ID, 0x3224 }, // NYI
            { Opcode.CMSG_SET_DUNGEON_DIFFICULTY, 0x3680 },
            { Opcode.CMSG_SET_EVERYONE_IS_ASSISTANT, 0x3619 },
            { Opcode.CMSG_SET_FACTION_AT_WAR, 0x34DE },
            { Opcode.CMSG_SET_FACTION_INACTIVE, 0x34E0 },
            { Opcode.CMSG_SET_FACTION_NOT_AT_WAR, 0x34DF },
            { Opcode.CMSG_SET_GAME_EVENT_DEBUG_VIEW_STATE, 0x31BB }, // NYI
            { Opcode.CMSG_SET_INSERT_ITEMS_LEFT_TO_RIGHT, 0x32CB }, // NYI
            { Opcode.CMSG_SET_LOOT_METHOD, 0x3647 },
            { Opcode.CMSG_SET_PARTY_ASSIGNMENT, 0x3650 },
            { Opcode.CMSG_SET_PARTY_LEADER, 0x3649 },
            { Opcode.CMSG_SET_PET_SLOT, 0x316D },
            { Opcode.CMSG_SET_PLAYER_DECLINED_NAMES, 0x3685 },
            { Opcode.CMSG_SET_PREFERRED_CEMETERY, 0x317C }, // NYI
            { Opcode.CMSG_SET_PRIMARY_TALENT_TREE, 0x3559 },
            { Opcode.CMSG_SET_PVP, 0x32AE },
            { Opcode.CMSG_SET_RAID_DIFFICULTY, 0x36DF },
            { Opcode.CMSG_SET_ROLE, 0x35D9 },
            { Opcode.CMSG_SET_SELECTION, 0x352A },
            { Opcode.CMSG_SET_SHEATHED, 0x3489 },
            { Opcode.CMSG_SET_TAXI_BENCHMARK_MODE, 0x34F3 },
            { Opcode.CMSG_SET_TITLE, 0x3280 },
            { Opcode.CMSG_SET_TRADE_GOLD, 0x3160 },
            { Opcode.CMSG_SET_TRADE_ITEM, 0x315E },
            { Opcode.CMSG_SET_WATCHED_FACTION, 0x34E1 },
            { Opcode.CMSG_SHOWING_CLOAK, 0x356C }, // NYI
            { Opcode.CMSG_SHOWING_HELM, 0x356B }, // NYI
            { Opcode.CMSG_SHOW_TRADE_SKILL, 0x36C6 }, // NYI
            { Opcode.CMSG_SIGN_PETITION, 0x3535 },
            { Opcode.CMSG_SILENCE_PARTY_TALKER, 0x3651 }, // NYI
            { Opcode.CMSG_SOCIAL_CONTRACT_REQUEST, 0x3748 },
            { Opcode.CMSG_SOCKET_GEMS, 0x34EB },
            { Opcode.CMSG_SPAWN_TRACKING_UPDATE, 0x3294 }, // NYI
            { Opcode.CMSG_SPELL_CLICK, 0x3495 },
            { Opcode.CMSG_SPIRIT_HEALER_ACTIVATE, 0x34AF },
            { Opcode.CMSG_SPLIT_GUILD_BANK_ITEM, 0x34C1 },
            { Opcode.CMSG_SPLIT_GUILD_BANK_ITEM_TO_INVENTORY, 0x34BE },
            { Opcode.CMSG_SPLIT_ITEM, 0x399C },
            { Opcode.CMSG_SPLIT_ITEM_TO_GUILD_BANK, 0x34BC },
            { Opcode.CMSG_STABLE_PET, 0x3169 }, // NYI
            { Opcode.CMSG_STABLE_SWAP_PET, 0x316B }, // NYI
            { Opcode.CMSG_STAND_STATE_CHANGE, 0x318E },
            { Opcode.CMSG_START_SPECTATOR_WAR_GAME, 0x35DF }, // NYI
            { Opcode.CMSG_START_WAR_GAME, 0x35DE }, // NYI
            { Opcode.CMSG_STORE_GUILD_BANK_ITEM, 0x34B7 },
            { Opcode.CMSG_SUBMIT_USER_FEEDBACK, 0x368F },
            { Opcode.CMSG_SUMMON_RESPONSE, 0x3668 },
            { Opcode.CMSG_SUPPORT_TICKET_SUBMIT_BUG, 0x3644 }, // NYI
            { Opcode.CMSG_SUPPORT_TICKET_SUBMIT_COMPLAINT, 0x3643 },
            { Opcode.CMSG_SUPPORT_TICKET_SUBMIT_SUGGESTION, 0x3645 }, // NYI
            { Opcode.CMSG_SUSPEND_COMMS_ACK, 0x3778 },
            { Opcode.CMSG_SUSPEND_TOKEN_RESPONSE, 0x377E },
            { Opcode.CMSG_SWAP_GUILD_BANK_ITEM_WITH_GUILD_BANK_ITEM, 0x34B9 },
            { Opcode.CMSG_SWAP_INV_ITEM, 0x399B },
            { Opcode.CMSG_SWAP_ITEM, 0x399A },
            { Opcode.CMSG_SWAP_ITEM_WITH_GUILD_BANK_ITEM, 0x34B8 },
            { Opcode.CMSG_SWAP_SUB_GROUPS, 0x364C },
            { Opcode.CMSG_SWAP_VOID_ITEM, 0x31A7 },
            { Opcode.CMSG_TABARD_VENDOR_ACTIVATE, 0x32AB },
            { Opcode.CMSG_TALK_TO_GOSSIP, 0x3492 },
            { Opcode.CMSG_TAXI_NODE_STATUS_QUERY, 0x34A8 },
            { Opcode.CMSG_TAXI_QUERY_AVAILABLE_NODES, 0x34AA },
            { Opcode.CMSG_TAXI_REQUEST_EARLY_LANDING, 0x34AC },
            { Opcode.CMSG_TIME_ADJUSTMENT_RESPONSE, 0x3A40 }, // NYI
            { Opcode.CMSG_TIME_SYNC_RESPONSE, 0x3A3D },
            { Opcode.CMSG_TIME_SYNC_RESPONSE_DROPPED, 0x3A3F }, // NYI
            { Opcode.CMSG_TIME_SYNC_RESPONSE_FAILED, 0x3A3E }, // NYI
            { Opcode.CMSG_TOGGLE_DIFFICULTY, 0x3655 }, // NYI
            { Opcode.CMSG_TOGGLE_PVP, 0x32AD },
            { Opcode.CMSG_TOTEM_DESTROYED, 0x34F8 },
            { Opcode.CMSG_TOY_CLEAR_FANFARE, 0x3128 },
            { Opcode.CMSG_TRAINER_BUY_SPELL, 0x34AE },
            { Opcode.CMSG_TRAINER_LIST, 0x34AD },
            { Opcode.CMSG_TRAITS_COMMIT_CONFIG, 0x3304 }, // NYI
            { Opcode.CMSG_TRANSMOGRIFY_ITEMS, 0x319A },
            { Opcode.CMSG_TURN_IN_PETITION, 0x3537 },
            { Opcode.CMSG_TUTORIAL, 0x36E0 },
            { Opcode.CMSG_UNACCEPT_TRADE, 0x315C },
            { Opcode.CMSG_UNDELETE_CHARACTER, 0x36E2 },
            { Opcode.CMSG_UNLEARN_SKILL, 0x34E5 },
            { Opcode.CMSG_UNLOCK_VOID_STORAGE, 0x31A4 },
            { Opcode.CMSG_UNSTABLE_PET, 0x316A }, // NYI
            { Opcode.CMSG_UPDATE_AADC_STATUS, 0x373D },
            { Opcode.CMSG_UPDATE_ACCOUNT_DATA, 0x3691 },
            { Opcode.CMSG_UPDATE_AREA_TRIGGER_VISUAL, 0x32A0 }, // NYI
            { Opcode.CMSG_UPDATE_CLIENT_SETTINGS, 0x3662 }, // NYI
            { Opcode.CMSG_UPDATE_MISSILE_TRAJECTORY, 0x3A43 },
            { Opcode.CMSG_UPDATE_RAID_TARGET, 0x364F },
            { Opcode.CMSG_UPDATE_SPELL_VISUAL, 0x329F }, // NYI
            { Opcode.CMSG_UPDATE_VAS_PURCHASE_STATES, 0x36F7 },
            { Opcode.CMSG_USED_FOLLOW, 0x318B }, // NYI
            { Opcode.CMSG_USE_CRITTER_ITEM, 0x3243 },
            { Opcode.CMSG_USE_EQUIPMENT_SET, 0x3995 },
            { Opcode.CMSG_USE_ITEM, 0x329A },
            { Opcode.CMSG_USE_TOY, 0x329C },
            { Opcode.CMSG_VAS_CHECK_TRANSFER_OK, 0x370F },
            { Opcode.CMSG_VAS_GET_QUEUE_MINUTES, 0x370E },
            { Opcode.CMSG_VAS_GET_SERVICE_STATUS, 0x370D },
            { Opcode.CMSG_VIOLENCE_LEVEL, 0x3189 },
            { Opcode.CMSG_VOICE_CHANNEL_STT_TOKEN_REQUEST, 0x3713 }, // NYI
            { Opcode.CMSG_VOICE_CHAT_JOIN_CHANNEL, 0x3714 }, // NYI
            { Opcode.CMSG_VOICE_CHAT_LOGIN, 0x3712 }, // NYI
            { Opcode.CMSG_VOID_STORAGE_TRANSFER, 0x31A6 },
            { Opcode.CMSG_WARDEN3_DATA, 0x35EC },
            { Opcode.CMSG_WHO, 0x367F },
            { Opcode.CMSG_WHO_IS, 0x367E },
            { Opcode.CMSG_WORLD_PORT_RESPONSE, 0x35F9 },
            { Opcode.CMSG_WRAP_ITEM, 0x3994 },
        };

        private static readonly BiDictionary<Opcode, int> ServerOpcodes = new()
        {
            { Opcode.SMSG_ABORT_NEW_WORLD, 0x259A },
            { Opcode.SMSG_ACCOUNT_CRITERIA_UPDATE, 0x286A },
            { Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x270E },
            { Opcode.SMSG_ACCOUNT_EXPORT_RESPONSE, 0x28AB }, // NYI
            { Opcode.SMSG_ACCOUNT_HEIRLOOM_UPDATE, 0x25B3 },
            { Opcode.SMSG_ACCOUNT_MOUNT_REMOVED, 0x25B1 }, // NYI
            { Opcode.SMSG_ACCOUNT_MOUNT_UPDATE, 0x25B0 },
            { Opcode.SMSG_ACCOUNT_NOTIFICATIONS_RESPONSE, 0x2885 }, // NYI
            { Opcode.SMSG_ACCOUNT_TOY_UPDATE, 0x25B2 },
            { Opcode.SMSG_ACCOUNT_TRANSMOG_SET_FAVORITES_UPDATE, 0x25B6 }, // NYI
            { Opcode.SMSG_ACCOUNT_TRANSMOG_UPDATE, 0x25B5 }, // NYI
            { Opcode.SMSG_ACHIEVEMENT_DELETED, 0x26ED }, // NYI
            { Opcode.SMSG_ACHIEVEMENT_EARNED, 0x2645 },
            { Opcode.SMSG_ACTIVATE_TAXI_REPLY, 0x2682 },
            { Opcode.SMSG_ACTIVE_GLYPHS, 0x2C53 },
            { Opcode.SMSG_ADDON_LIST_REQUEST, 0x2644 }, // NYI
            { Opcode.SMSG_ADD_BATTLENET_FRIEND_RESPONSE, 0x263F }, // NYI
            { Opcode.SMSG_ADD_ITEM_PASSIVE, 0x25AC }, // NYI
            { Opcode.SMSG_ADD_LOSS_OF_CONTROL, 0x2675 },
            { Opcode.SMSG_ADD_RUNE_POWER, 0x26BC },
            { Opcode.SMSG_ADJUST_SPLINE_DURATION, 0x25D2 },
            { Opcode.SMSG_AE_LOOT_TARGETS, 0x261A },
            { Opcode.SMSG_AE_LOOT_TARGET_ACK, 0x261B },
            { Opcode.SMSG_AI_REACTION, 0x26B9 },
            { Opcode.SMSG_ALLIED_RACE_DETAILS, 0x27FE },
            { Opcode.SMSG_ALL_ACCOUNT_CRITERIA, 0x2571 },
            { Opcode.SMSG_ALL_ACHIEVEMENT_DATA, 0x2570 },
            { Opcode.SMSG_ALL_GUILD_ACHIEVEMENTS, 0x29B8 },
            { Opcode.SMSG_ARCHAEOLOGY_SURVERY_CAST, 0x2587 },
            { Opcode.SMSG_AREA_POI_UPDATE_RESPONSE, 0x3010 },
            { Opcode.SMSG_AREA_SPIRIT_HEALER_TIME, 0x2744 },
            { Opcode.SMSG_AREA_TRIGGER_DENIED, 0x2903 },
            { Opcode.SMSG_AREA_TRIGGER_FORCE_SET_POSITION_AND_FACING, 0x2900 }, // NYI
            { Opcode.SMSG_AREA_TRIGGER_MESSAGE, 0x2882 },
            { Opcode.SMSG_AREA_TRIGGER_NO_CORPSE, 0x271A },
            { Opcode.SMSG_AREA_TRIGGER_RE_PATH, 0x28FD },
            { Opcode.SMSG_AREA_TRIGGER_RE_SHAPE, 0x2902 }, // NYI
            { Opcode.SMSG_AREA_TRIGGER_UNATTACH, 0x2901 }, // NYI
            { Opcode.SMSG_ARENA_CROWD_CONTROL_SPELL_RESULT, 0x2634 }, // NYI
            { Opcode.SMSG_ARENA_PREP_OPPONENT_SPECIALIZATIONS, 0x264A }, // NYI
            { Opcode.SMSG_ARENA_TEAM_COMMAND_RESULT, 0x2767 }, // NYI
            { Opcode.SMSG_ARENA_TEAM_EVENT, 0x2766 }, // NYI
            { Opcode.SMSG_ARENA_TEAM_INVITE, 0x2765 }, // NYI
            { Opcode.SMSG_ARENA_TEAM_ROSTER, 0x2764 }, // NYI
            { Opcode.SMSG_ARENA_TEAM_STATS, 0x2768 }, // NYI
            { Opcode.SMSG_ATTACKER_STATE_UPDATE, 0x2952 },
            { Opcode.SMSG_ATTACK_START, 0x293D },
            { Opcode.SMSG_ATTACK_STOP, 0x293E },
            { Opcode.SMSG_ATTACK_SWING_ERROR, 0x294C },
            { Opcode.SMSG_ATTACK_SWING_LANDED_LOG, 0x294D }, // NYI
            { Opcode.SMSG_AUCTIONABLE_TOKEN_AUCTION_SOLD, 0x27DA }, // NYI
            { Opcode.SMSG_AUCTIONABLE_TOKEN_SELL_AT_MARKET_PRICE_RESPONSE, 0x27D9 }, // NYI
            { Opcode.SMSG_AUCTIONABLE_TOKEN_SELL_CONFIRM_REQUIRED, 0x27D8 }, // NYI
            { Opcode.SMSG_AUCTION_CLOSED_NOTIFICATION, 0x26F7 },
            { Opcode.SMSG_AUCTION_COMMAND_RESULT, 0x26F4 },
            { Opcode.SMSG_AUCTION_HELLO_RESPONSE, 0x26F2 },
            { Opcode.SMSG_AUCTION_LIST_BIDDER_ITEMS_RESULT, 0x287F },
            { Opcode.SMSG_AUCTION_LIST_ITEMS_RESULT, 0x2865 },
            { Opcode.SMSG_AUCTION_LIST_OWNER_ITEMS_RESULT, 0x287E },
            { Opcode.SMSG_AUCTION_LIST_PENDING_SALES_RESULT, 0x26F9 },
            { Opcode.SMSG_AUCTION_OUTBID_NOTIFICATION, 0x26F6 },
            { Opcode.SMSG_AUCTION_OWNER_BID_NOTIFICATION, 0x26F8 },
            { Opcode.SMSG_AUCTION_REPLICATE_RESPONSE, 0x26F3 },
            { Opcode.SMSG_AUCTION_WON_NOTIFICATION, 0x26F5 },
            { Opcode.SMSG_AURA_POINTS_DEPLETED, 0x2C22 }, // NYI
            { Opcode.SMSG_AURA_UPDATE, 0x2C21 },
            { Opcode.SMSG_AUTH_CHALLENGE, 0x3048 },
            { Opcode.SMSG_AUTH_FAILED, 0x256C }, // NYI
            { Opcode.SMSG_AUTH_RESPONSE, 0x256D },
            { Opcode.SMSG_AVAILABLE_HOTFIXES, 0x290F },
            { Opcode.SMSG_BAG_CLEANUP_FINISHED, 0x2DA7 },
            { Opcode.SMSG_BARBER_SHOP_RESULT, 0x26C2 },
            { Opcode.SMSG_BATCH_PRESENCE_SUBSCRIPTION, 0x2848 }, // NYI
            { Opcode.SMSG_BATTLEFIELD_LIST, 0x2927 },
            { Opcode.SMSG_BATTLEFIELD_PORT_DENIED, 0x292D },
            { Opcode.SMSG_BATTLEFIELD_STATUS_ACTIVE, 0x2923 },
            { Opcode.SMSG_BATTLEFIELD_STATUS_FAILED, 0x2926 },
            { Opcode.SMSG_BATTLEFIELD_STATUS_NEED_CONFIRMATION, 0x2922 },
            { Opcode.SMSG_BATTLEFIELD_STATUS_NONE, 0x2925 },
            { Opcode.SMSG_BATTLEFIELD_STATUS_QUEUED, 0x2924 },
            { Opcode.SMSG_BATTLEFIELD_STATUS_WAIT_FOR_GROUPS, 0x292F }, // NYI
            { Opcode.SMSG_BATTLEGROUND_INFO_THROTTLED, 0x292E }, // NYI
            { Opcode.SMSG_BATTLEGROUND_INIT, 0x294F }, // NYI
            { Opcode.SMSG_BATTLEGROUND_PLAYER_JOINED, 0x292B },
            { Opcode.SMSG_BATTLEGROUND_PLAYER_LEFT, 0x292C },
            { Opcode.SMSG_BATTLEGROUND_PLAYER_POSITIONS, 0x2928 },
            { Opcode.SMSG_BATTLEGROUND_POINTS, 0x294E }, // NYI
            { Opcode.SMSG_BATTLENET_CHALLENGE_ABORT, 0x2793 }, // NYI
            { Opcode.SMSG_BATTLENET_CHALLENGE_START, 0x2792 }, // NYI
            { Opcode.SMSG_BATTLENET_NOTIFICATION, 0x280A },
            { Opcode.SMSG_BATTLENET_RESPONSE, 0x2809 },
            { Opcode.SMSG_BATTLE_NET_CONNECTION_STATUS, 0x280B },
            { Opcode.SMSG_BATTLE_PAY_ACK_FAILED, 0x278C },
            { Opcode.SMSG_BATTLE_PAY_BATTLE_PET_DELIVERED, 0x2781 },
            { Opcode.SMSG_BATTLE_PAY_COLLECTION_ITEM_DELIVERED, 0x2782 },
            { Opcode.SMSG_BATTLE_PAY_CONFIRM_PURCHASE, 0x278B },
            { Opcode.SMSG_BATTLE_PAY_DELIVERY_ENDED, 0x277F },
            { Opcode.SMSG_BATTLE_PAY_DELIVERY_STARTED, 0x277E },
            { Opcode.SMSG_BATTLE_PAY_DISTRIBUTION_ASSIGN_VAS_RESPONSE, 0x288A },
            { Opcode.SMSG_BATTLE_PAY_DISTRIBUTION_UNREVOKED, 0x277C },
            { Opcode.SMSG_BATTLE_PAY_DISTRIBUTION_UPDATE, 0x277D },
            { Opcode.SMSG_BATTLE_PAY_GET_DISTRIBUTION_LIST_RESPONSE, 0x277B },
            { Opcode.SMSG_BATTLE_PAY_GET_PRODUCT_LIST_RESPONSE, 0x2779 },
            { Opcode.SMSG_BATTLE_PAY_GET_PURCHASE_LIST_RESPONSE, 0x277A },
            { Opcode.SMSG_BATTLE_PAY_MOUNT_DELIVERED, 0x2780 },
            { Opcode.SMSG_BATTLE_PAY_PURCHASE_UPDATE, 0x278A },
            { Opcode.SMSG_BATTLE_PAY_START_CHECKOUT, 0x2826 },
            { Opcode.SMSG_BATTLE_PAY_START_DISTRIBUTION_ASSIGN_TO_TARGET_RESPONSE, 0x2788 },
            { Opcode.SMSG_BATTLE_PAY_START_PURCHASE_RESPONSE, 0x2787 },
            { Opcode.SMSG_BATTLE_PAY_VALIDATE_PURCHASE_RESPONSE, 0x281A },
            { Opcode.SMSG_BATTLE_PETS_HEALED, 0x25F5 }, // NYI
            { Opcode.SMSG_BATTLE_PET_CAGE_DATE_ERROR, 0x267C }, // NYI
            { Opcode.SMSG_BATTLE_PET_DELETED, 0x25F2 },
            { Opcode.SMSG_BATTLE_PET_ERROR, 0x263A },
            { Opcode.SMSG_BATTLE_PET_JOURNAL, 0x25F1 },
            { Opcode.SMSG_BATTLE_PET_JOURNAL_LOCK_ACQUIRED, 0x25EF },
            { Opcode.SMSG_BATTLE_PET_JOURNAL_LOCK_DENIED, 0x25F0 },
            { Opcode.SMSG_BATTLE_PET_RESTORED, 0x25F4 }, // NYI
            { Opcode.SMSG_BATTLE_PET_REVOKED, 0x25F3 }, // NYI
            { Opcode.SMSG_BATTLE_PET_TRAP_LEVEL, 0x25ED }, // NYI
            { Opcode.SMSG_BATTLE_PET_UPDATES, 0x25EC },
            { Opcode.SMSG_BIND_POINT_UPDATE, 0x257D },
            { Opcode.SMSG_BLACK_MARKET_BID_ON_ITEM_RESULT, 0x262A },
            { Opcode.SMSG_BLACK_MARKET_OUTBID, 0x262B },
            { Opcode.SMSG_BLACK_MARKET_REQUEST_ITEMS_RESULT, 0x2629 },
            { Opcode.SMSG_BLACK_MARKET_WON, 0x262C },
            { Opcode.SMSG_BONUS_ROLL_EMPTY, 0x2647 }, // NYI
            { Opcode.SMSG_BOSS_KILL, 0x2951 },
            { Opcode.SMSG_BREAK_TARGET, 0x293C },
            { Opcode.SMSG_BROADCAST_ACHIEVEMENT, 0x2BBC },
            { Opcode.SMSG_BROADCAST_LEVELUP, 0x2BBD }, // NYI
            { Opcode.SMSG_BUY_FAILED, 0x26CB },
            { Opcode.SMSG_BUY_SUCCEEDED, 0x26CA },
            { Opcode.SMSG_CACHE_INFO, 0x291D },
            { Opcode.SMSG_CACHE_VERSION, 0x291C },
            { Opcode.SMSG_CALENDAR_CLEAR_PENDING_ACTION, 0x26A1 },
            { Opcode.SMSG_CALENDAR_COMMAND_RESULT, 0x26A2 },
            { Opcode.SMSG_CALENDAR_COMMUNITY_INVITE, 0x2691 },
            { Opcode.SMSG_CALENDAR_EVENT_REMOVED_ALERT, 0x2699 },
            { Opcode.SMSG_CALENDAR_EVENT_UPDATED_ALERT, 0x269A },
            { Opcode.SMSG_CALENDAR_INVITE_ADDED, 0x2692 },
            { Opcode.SMSG_CALENDAR_INVITE_ALERT, 0x2696 },
            { Opcode.SMSG_CALENDAR_INVITE_NOTES, 0x269B },
            { Opcode.SMSG_CALENDAR_INVITE_NOTES_ALERT, 0x269C },
            { Opcode.SMSG_CALENDAR_INVITE_REMOVED, 0x2693 },
            { Opcode.SMSG_CALENDAR_INVITE_REMOVED_ALERT, 0x2698 },
            { Opcode.SMSG_CALENDAR_INVITE_STATUS, 0x2694 },
            { Opcode.SMSG_CALENDAR_INVITE_STATUS_ALERT, 0x2697 },
            { Opcode.SMSG_CALENDAR_MODERATOR_STATUS, 0x2695 },
            { Opcode.SMSG_CALENDAR_RAID_LOCKOUT_ADDED, 0x269D },
            { Opcode.SMSG_CALENDAR_RAID_LOCKOUT_REMOVED, 0x269E },
            { Opcode.SMSG_CALENDAR_RAID_LOCKOUT_UPDATED, 0x269F },
            { Opcode.SMSG_CALENDAR_SEND_CALENDAR, 0x268F },
            { Opcode.SMSG_CALENDAR_SEND_EVENT, 0x2690 },
            { Opcode.SMSG_CALENDAR_SEND_NUM_PENDING, 0x26A0 },
            { Opcode.SMSG_CAMERA_EFFECT, 0x272A }, // NYI
            { Opcode.SMSG_CANCEL_AUTO_REPEAT, 0x26E2 },
            { Opcode.SMSG_CANCEL_COMBAT, 0x294B },
            { Opcode.SMSG_CANCEL_ORPHAN_SPELL_VISUAL, 0x2C45 },
            { Opcode.SMSG_CANCEL_PRELOAD_WORLD, 0x2598 }, // NYI
            { Opcode.SMSG_CANCEL_SCENE, 0x2639 },
            { Opcode.SMSG_CANCEL_SPELL_VISUAL, 0x2C43 },
            { Opcode.SMSG_CANCEL_SPELL_VISUAL_KIT, 0x2C47 },
            { Opcode.SMSG_CAN_DUEL_RESULT, 0x2947 },
            { Opcode.SMSG_CAN_REDEEM_TOKEN_FOR_BALANCE_RESPONSE, 0x2819 }, // NYI
            { Opcode.SMSG_CAST_FAILED, 0x2C56 },
            { Opcode.SMSG_CHANGE_PLAYER_DIFFICULTY_RESULT, 0x3004 }, // NYI
            { Opcode.SMSG_CHANGE_REALM_TICKET_RESPONSE, 0x280C },
            { Opcode.SMSG_CHANNEL_LIST, 0x2BC4 },
            { Opcode.SMSG_CHANNEL_NOTIFY, 0x2BC1 },
            { Opcode.SMSG_CHANNEL_NOTIFY_JOINED, 0x2BC2 },
            { Opcode.SMSG_CHANNEL_NOTIFY_LEFT, 0x2BC3 },
            { Opcode.SMSG_CHARACTER_CHECK_UPGRADE_RESULT, 0x27C5 }, // NYI
            { Opcode.SMSG_CHARACTER_LOGIN_FAILED, 0x2709 },
            { Opcode.SMSG_CHARACTER_OBJECT_TEST_RESPONSE, 0x2791 }, // NYI
            { Opcode.SMSG_CHARACTER_RENAME_RESULT, 0x276B },
            { Opcode.SMSG_CHARACTER_UPGRADE_ABORTED, 0x27C4 }, // NYI
            { Opcode.SMSG_CHARACTER_UPGRADE_COMPLETE, 0x27C3 }, // NYI
            { Opcode.SMSG_CHARACTER_UPGRADE_MANUAL_UNREVOKE_RESULT, 0x27C6 }, // NYI
            { Opcode.SMSG_CHARACTER_UPGRADE_STARTED, 0x27C2 }, // NYI
            { Opcode.SMSG_CHAR_CUSTOMIZE_FAILURE, 0x26E7 },
            { Opcode.SMSG_CHAR_CUSTOMIZE_SUCCESS, 0x26E8 },
            { Opcode.SMSG_CHAR_FACTION_CHANGE_RESULT, 0x27AF },
            { Opcode.SMSG_CHAT, 0x2BAD },
            { Opcode.SMSG_CHAT_AUTO_RESPONDED, 0x2BB8 }, // NYI
            { Opcode.SMSG_CHAT_DOWN, 0x2BBE }, // NYI
            { Opcode.SMSG_CHAT_IGNORED_ACCOUNT_MUTED, 0x2BAC }, // NYI
            { Opcode.SMSG_CHAT_IS_DOWN, 0x2BBF }, // NYI
            { Opcode.SMSG_CHAT_NOT_IN_PARTY, 0x2BB2 }, // NYI
            { Opcode.SMSG_CHAT_PLAYER_AMBIGUOUS, 0x2BB0 },
            { Opcode.SMSG_CHAT_PLAYER_NOTFOUND, 0x2BB7 },
            { Opcode.SMSG_CHAT_RECONNECT, 0x2BC0 }, // NYI
            { Opcode.SMSG_CHAT_RESTRICTED, 0x2BB3 },
            { Opcode.SMSG_CHAT_SERVER_MESSAGE, 0x2BC5 },
            { Opcode.SMSG_CHEAT_IGNORE_DIMISHING_RETURNS, 0x2C12 }, // NYI
            { Opcode.SMSG_CHECK_WARGAME_ENTRY, 0x2592 }, // NYI
            { Opcode.SMSG_CLEAR_ALL_SPELL_CHARGES, 0x2C26 },
            { Opcode.SMSG_CLEAR_BOSS_EMOTES, 0x25BD },
            { Opcode.SMSG_CLEAR_COOLDOWN, 0x26BE },
            { Opcode.SMSG_CLEAR_COOLDOWNS, 0x2C25 },
            { Opcode.SMSG_CLEAR_SPELL_CHARGES, 0x2C27 },
            { Opcode.SMSG_CLEAR_TARGET, 0x2948 },
            { Opcode.SMSG_COIN_REMOVED, 0x2619 },
            { Opcode.SMSG_COMBAT_EVENT_FAILED, 0x293F }, // NYI
            { Opcode.SMSG_COMMENTATOR_MAP_INFO, 0x270B }, // NYI
            { Opcode.SMSG_COMMENTATOR_PLAYER_INFO, 0x270C }, // NYI
            { Opcode.SMSG_COMMENTATOR_STATE_CHANGED, 0x270A }, // NYI
            { Opcode.SMSG_COMMERCE_TOKEN_GET_COUNT_RESPONSE, 0x27D5 }, // NYI
            { Opcode.SMSG_COMMERCE_TOKEN_GET_LOG_RESPONSE, 0x27E1 },
            { Opcode.SMSG_COMMERCE_TOKEN_GET_MARKET_PRICE_RESPONSE, 0x27D7 },
            { Opcode.SMSG_COMMERCE_TOKEN_UPDATE, 0x27D6 },
            { Opcode.SMSG_COMPLAINT_RESULT, 0x26AF },
            { Opcode.SMSG_COMPRESSED_PACKET, 0x3052 },
            { Opcode.SMSG_CONFIRM_BARBERS_CHOICE, 0x26C1 }, // NYI
            { Opcode.SMSG_CONFIRM_PARTY_INVITE, 0x2818 }, // NYI
            { Opcode.SMSG_CONNECT_TO, 0x304D },
            { Opcode.SMSG_CONSOLE_WRITE, 0x2637 }, // NYI
            { Opcode.SMSG_CONSUMABLE_TOKEN_BUY_AT_MARKET_PRICE_RESPONSE, 0x27DD }, // NYI
            { Opcode.SMSG_CONSUMABLE_TOKEN_BUY_CHOICE_REQUIRED, 0x27DC }, // NYI
            { Opcode.SMSG_CONSUMABLE_TOKEN_CAN_VETERAN_BUY_RESPONSE, 0x27DB }, // NYI
            { Opcode.SMSG_CONSUMABLE_TOKEN_REDEEM_CONFIRM_REQUIRED, 0x27DF }, // NYI
            { Opcode.SMSG_CONSUMABLE_TOKEN_REDEEM_RESPONSE, 0x27E0 }, // NYI
            { Opcode.SMSG_CONTACT_LIST, 0x278F },
            { Opcode.SMSG_CONTRIBUTION_LAST_UPDATE_RESPONSE, 0x281F }, // NYI
            { Opcode.SMSG_CONTROL_UPDATE, 0x2649 },
            { Opcode.SMSG_CONVERT_RUNE, 0x2C5F },
            { Opcode.SMSG_COOLDOWN_CHEAT, 0x273D }, // NYI
            { Opcode.SMSG_COOLDOWN_EVENT, 0x26BD },
            { Opcode.SMSG_CORPSE_LOCATION, 0x2651 },
            { Opcode.SMSG_CORPSE_RECLAIM_DELAY, 0x274E },
            { Opcode.SMSG_CORPSE_TRANSPORT_QUERY, 0x2716 },
            { Opcode.SMSG_COVENANT_CALLINGS_AVAILABILITY_RESPONSE, 0x2AA3 },
            { Opcode.SMSG_CREATE_CHAR, 0x2705 },
            { Opcode.SMSG_CRITERIA_DELETED, 0x26EC },
            { Opcode.SMSG_CRITERIA_UPDATE, 0x26E5 },
            { Opcode.SMSG_CROSSED_INEBRIATION_THRESHOLD, 0x26C6 },
            { Opcode.SMSG_CUSTOM_LOAD_SCREEN, 0x25CD },
            { Opcode.SMSG_DAILY_QUESTS_RESET, 0x2A80 },
            { Opcode.SMSG_DAMAGE_CALC_LOG, 0x2C61 }, // NYI
            { Opcode.SMSG_DB_REPLY, 0x290E },
            { Opcode.SMSG_DEATH_RELEASE_LOC, 0x26D7 },
            { Opcode.SMSG_DEBUG_MENU_MANAGER_FULL_UPDATE, 0x2659 }, // NYI
            { Opcode.SMSG_DEFENSE_MESSAGE, 0x2BB6 },
            { Opcode.SMSG_DELETE_CHAR, 0x2706 },
            { Opcode.SMSG_DESTROY_ARENA_UNIT, 0x2746 },
            { Opcode.SMSG_DESTRUCTIBLE_BUILDING_DAMAGE, 0x26FD },
            { Opcode.SMSG_DIFFERENT_INSTANCE_FROM_PARTY, 0x258A }, // NYI
            { Opcode.SMSG_DISENCHANT_CREDIT, 0x25A9 }, // NYI
            { Opcode.SMSG_DISMOUNT, 0x26B5 }, // NYI
            { Opcode.SMSG_DISMOUNT_RESULT, 0x257C }, // NYI
            { Opcode.SMSG_DISPEL_FAILED, 0x2C2F },
            { Opcode.SMSG_DISPLAY_GAME_ERROR, 0x259F },
            { Opcode.SMSG_DISPLAY_PLAYER_CHOICE, 0x2FFC },
            { Opcode.SMSG_DISPLAY_PROMOTION, 0x264E },
            { Opcode.SMSG_DISPLAY_QUEST_POPUP, 0x2A9E }, // NYI
            { Opcode.SMSG_DISPLAY_TOAST, 0x2626 },
            { Opcode.SMSG_DONT_AUTO_PUSH_SPELLS_TO_ACTION_BAR, 0x25E3 }, // NYI
            { Opcode.SMSG_DROP_NEW_CONNECTION, 0x304C }, // NYI
            { Opcode.SMSG_DUEL_ARRANGED, 0x2941 }, // NYI
            { Opcode.SMSG_DUEL_COMPLETE, 0x2945 },
            { Opcode.SMSG_DUEL_COUNTDOWN, 0x2944 },
            { Opcode.SMSG_DUEL_IN_BOUNDS, 0x2943 },
            { Opcode.SMSG_DUEL_OUT_OF_BOUNDS, 0x2942 },
            { Opcode.SMSG_DUEL_REQUESTED, 0x2940 },
            { Opcode.SMSG_DUEL_WINNER, 0x2946 },
            { Opcode.SMSG_DURABILITY_DAMAGE_DEATH, 0x2749 },
            { Opcode.SMSG_EMOTE, 0x27CC },
            { Opcode.SMSG_ENABLE_BARBER_SHOP, 0x26C0 },
            { Opcode.SMSG_ENCHANTMENT_LOG, 0x2717 },
            { Opcode.SMSG_ENCOUNTER_END, 0x2786 }, // NYI
            { Opcode.SMSG_ENCOUNTER_START, 0x2785 }, // NYI
            { Opcode.SMSG_END_LIGHTNING_STORM, 0x26AC },
            { Opcode.SMSG_ENSURE_WORLD_LOADED, 0x288B }, // NYI
            { Opcode.SMSG_ENTER_ENCRYPTED_MODE, 0x3049 },
            { Opcode.SMSG_ENUM_CHARACTERS_RESULT, 0x2584 },
            { Opcode.SMSG_ENUM_VAS_PURCHASE_STATES_RESPONSE, 0x27F7 },
            { Opcode.SMSG_ENVIRONMENTAL_DAMAGE_LOG, 0x2C1E },
            { Opcode.SMSG_EQUIPMENT_SET_ID, 0x26B6 },
            { Opcode.SMSG_EXPECTED_SPAM_RECORDS, 0x2BB1 }, // NYI
            { Opcode.SMSG_EXPLORATION_EXPERIENCE, 0x2763 },
            { Opcode.SMSG_FACTION_BONUS_INFO, 0x2729 },
            { Opcode.SMSG_FAILED_PLAYER_CONDITION, 0x2FFA }, // NYI
            { Opcode.SMSG_FAILED_QUEST_TURN_IN, 0x2815 }, // NYI
            { Opcode.SMSG_FEATURE_SYSTEM_STATUS, 0x25C1 },
            { Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN, 0x25C2 },
            { Opcode.SMSG_FEIGN_DEATH_RESISTED, 0x2748 }, // NYI
            { Opcode.SMSG_FISH_ESCAPED, 0x26D4 },
            { Opcode.SMSG_FISH_NOT_HOOKED, 0x26D3 },
            { Opcode.SMSG_FLIGHT_SPLINE_SYNC, 0x2E2B },
            { Opcode.SMSG_FORCED_DEATH_UPDATE, 0x26D8 }, // NYI
            { Opcode.SMSG_FORCE_ANIM, 0x2755 }, // NYI
            { Opcode.SMSG_FORCE_OBJECT_RELINK, 0x264D }, // NYI
            { Opcode.SMSG_FORCE_RANDOM_TRANSMOG_TOAST, 0x25B4 }, // NYI
            { Opcode.SMSG_FRIEND_STATUS, 0x2790 },
            { Opcode.SMSG_GAME_OBJECT_ACTIVATE_ANIM_KIT, 0x25C5 },
            { Opcode.SMSG_GAME_OBJECT_BASE, 0x282C }, // NYI
            { Opcode.SMSG_GAME_OBJECT_CLOSE_INTERACTION, 0x288E },
            { Opcode.SMSG_GAME_OBJECT_CUSTOM_ANIM, 0x25C6 },
            { Opcode.SMSG_GAME_OBJECT_DESPAWN, 0x25C7 },
            { Opcode.SMSG_GAME_OBJECT_INTERACTION, 0x288D },
            { Opcode.SMSG_GAME_OBJECT_PLAY_SPELL_VISUAL, 0x2C4A },
            { Opcode.SMSG_GAME_OBJECT_PLAY_SPELL_VISUAL_KIT, 0x2C49 }, // NYI
            { Opcode.SMSG_GAME_OBJECT_RESET_STATE, 0x2722 }, // NYI
            { Opcode.SMSG_GAME_OBJECT_SET_STATE_LOCAL, 0x2808 },
            { Opcode.SMSG_GAME_OBJECT_UI_LINK, 0x271E }, // NYI
            { Opcode.SMSG_GAME_SPEED_SET, 0x2685 }, // NYI
            { Opcode.SMSG_GAME_TIME_SET, 0x2710 }, // NYI
            { Opcode.SMSG_GAME_TIME_UPDATE, 0x270F }, // NYI
            { Opcode.SMSG_GENERATE_RANDOM_CHARACTER_NAME_RESULT, 0x2586 },
            { Opcode.SMSG_GENERATE_SSO_TOKEN_RESPONSE, 0x2820 }, // NYI
            { Opcode.SMSG_GET_ACCOUNT_CHARACTER_LIST_RESULT, 0x2769 },
            { Opcode.SMSG_GET_REALM_HIDDEN_RESULT, 0x28B0 }, // NYI
            { Opcode.SMSG_GET_REMAINING_GAME_TIME_RESPONSE, 0x27DE }, // NYI
            { Opcode.SMSG_GET_VAS_ACCOUNT_CHARACTER_LIST_RESULT, 0x27F3 },
            { Opcode.SMSG_GET_VAS_TRANSFER_TARGET_REALM_LIST_RESULT, 0x27F4 },
            { Opcode.SMSG_GM_PLAYER_INFO, 0x3005 }, // NYI
            { Opcode.SMSG_GM_REQUEST_PLAYER_INFO, 0x2FFB }, // NYI
            { Opcode.SMSG_GM_TICKET_CASE_STATUS, 0x26A7 },
            { Opcode.SMSG_GM_TICKET_SYSTEM_STATUS, 0x26A6 },
            { Opcode.SMSG_GOD_MODE, 0x2700 }, // NYI
            { Opcode.SMSG_GOSSIP_COMPLETE, 0x2A97 },
            { Opcode.SMSG_GOSSIP_MESSAGE, 0x2A98 },
            { Opcode.SMSG_GOSSIP_OPTION_NPC_INTERACTION, 0x2AA7 },
            { Opcode.SMSG_GOSSIP_POI, 0x279B },
            { Opcode.SMSG_GOSSIP_QUEST_UPDATE, 0x2A99 }, // NYI
            { Opcode.SMSG_GROUP_ACTION_THROTTLED, 0x258F }, // NYI
            { Opcode.SMSG_GROUP_AUTO_KICK, 0x2798 }, // NYI
            { Opcode.SMSG_GROUP_DECLINE, 0x2794 },
            { Opcode.SMSG_GROUP_DESTROYED, 0x2797 },
            { Opcode.SMSG_GROUP_NEW_LEADER, 0x262F },
            { Opcode.SMSG_GROUP_REQUEST_DECLINE, 0x2795 }, // NYI
            { Opcode.SMSG_GROUP_UNINVITE, 0x2796 },
            { Opcode.SMSG_GUILD_ACHIEVEMENT_DELETED, 0x29C6 },
            { Opcode.SMSG_GUILD_ACHIEVEMENT_EARNED, 0x29C5 },
            { Opcode.SMSG_GUILD_ACHIEVEMENT_MEMBERS, 0x29C8 },
            { Opcode.SMSG_GUILD_BANK_LOG_QUERY_RESULTS, 0x29E0 },
            { Opcode.SMSG_GUILD_BANK_QUERY_RESULTS, 0x29DF },
            { Opcode.SMSG_GUILD_BANK_REMAINING_WITHDRAW_MONEY, 0x29E1 },
            { Opcode.SMSG_GUILD_BANK_TEXT_QUERY_RESULT, 0x29E4 }, // NYI
            { Opcode.SMSG_GUILD_CHALLENGE_COMPLETED, 0x29D4 }, // NYI
            { Opcode.SMSG_GUILD_CHALLENGE_UPDATE, 0x29D3 },
            { Opcode.SMSG_GUILD_CHANGE_NAME_RESULT, 0x29DE }, // NYI
            { Opcode.SMSG_GUILD_COMMAND_RESULT, 0x29BA },
            { Opcode.SMSG_GUILD_CRITERIA_DELETED, 0x29C7 },
            { Opcode.SMSG_GUILD_CRITERIA_UPDATE, 0x29C4 },
            { Opcode.SMSG_GUILD_EVENT_BANK_CONTENTS_CHANGED, 0x29F9 },
            { Opcode.SMSG_GUILD_EVENT_BANK_MONEY_CHANGED, 0x29F8 },
            { Opcode.SMSG_GUILD_EVENT_DISBANDED, 0x29EE },
            { Opcode.SMSG_GUILD_EVENT_LOG_QUERY_RESULTS, 0x29E3 },
            { Opcode.SMSG_GUILD_EVENT_MOTD, 0x29EF },
            { Opcode.SMSG_GUILD_EVENT_NEW_LEADER, 0x29ED },
            { Opcode.SMSG_GUILD_EVENT_PLAYER_JOINED, 0x29EB },
            { Opcode.SMSG_GUILD_EVENT_PLAYER_LEFT, 0x29EC },
            { Opcode.SMSG_GUILD_EVENT_PRESENCE_CHANGE, 0x29F0 },
            { Opcode.SMSG_GUILD_EVENT_RANKS_UPDATED, 0x29F2 },
            { Opcode.SMSG_GUILD_EVENT_RANK_CHANGED, 0x29F3 },
            { Opcode.SMSG_GUILD_EVENT_STATUS_CHANGE, 0x29F1 },
            { Opcode.SMSG_GUILD_EVENT_TAB_ADDED, 0x29F4 },
            { Opcode.SMSG_GUILD_EVENT_TAB_DELETED, 0x29F5 }, // NYI
            { Opcode.SMSG_GUILD_EVENT_TAB_MODIFIED, 0x29F6 },
            { Opcode.SMSG_GUILD_EVENT_TAB_TEXT_CHANGED, 0x29F7 },
            { Opcode.SMSG_GUILD_FLAGGED_FOR_RENAME, 0x29DD },
            { Opcode.SMSG_GUILD_HARDCORE_MEMBER_DEATH, 0x29BD },
            { Opcode.SMSG_GUILD_INVITE, 0x29CB },
            { Opcode.SMSG_GUILD_INVITE_DECLINED, 0x29E9 }, // NYI
            { Opcode.SMSG_GUILD_INVITE_EXPIRED, 0x29EA }, // NYI
            { Opcode.SMSG_GUILD_ITEM_LOOTED_NOTIFY, 0x29D5 }, // NYI
            { Opcode.SMSG_GUILD_KNOWN_RECIPES, 0x29BF }, // NYI
            { Opcode.SMSG_GUILD_MEMBERS_WITH_RECIPE, 0x29C0 }, // NYI
            { Opcode.SMSG_GUILD_MEMBER_DAILY_RESET, 0x29E5 },
            { Opcode.SMSG_GUILD_MEMBER_RECIPES, 0x29BE }, // NYI
            { Opcode.SMSG_GUILD_MEMBER_UPDATE_NOTE, 0x29CA },
            { Opcode.SMSG_GUILD_MOVED, 0x29DB }, // NYI
            { Opcode.SMSG_GUILD_MOVE_STARTING, 0x29DA }, // NYI
            { Opcode.SMSG_GUILD_NAME_CHANGED, 0x29DC },
            { Opcode.SMSG_GUILD_NEWS, 0x29C2 },
            { Opcode.SMSG_GUILD_NEWS_DELETED, 0x29C3 }, // NYI
            { Opcode.SMSG_GUILD_PARTY_STATE, 0x29CC },
            { Opcode.SMSG_GUILD_PERMISSIONS_QUERY_RESULTS, 0x29E2 },
            { Opcode.SMSG_GUILD_RANKS, 0x29C9 },
            { Opcode.SMSG_GUILD_REPUTATION_REACTION_CHANGED, 0x29CD }, // NYI
            { Opcode.SMSG_GUILD_RESET, 0x29D9 }, // NYI
            { Opcode.SMSG_GUILD_REWARD_LIST, 0x29C1 },
            { Opcode.SMSG_GUILD_ROSTER, 0x29BB },
            { Opcode.SMSG_GUILD_ROSTER_UPDATE, 0x29BC },
            { Opcode.SMSG_GUILD_SEND_RANK_CHANGE, 0x29B9 },
            { Opcode.SMSG_HARDCORE_DEATH_ALERT, 0x28B5 },
            { Opcode.SMSG_HEALTH_UPDATE, 0x26D5 },
            { Opcode.SMSG_HIGHEST_THREAT_UPDATE, 0x26DD },
            { Opcode.SMSG_HOTFIX_CONNECT, 0x2911 },
            { Opcode.SMSG_HOTFIX_MESSAGE, 0x2910 },
            { Opcode.SMSG_INITIALIZE_FACTIONS, 0x2728 },
            { Opcode.SMSG_INITIAL_SETUP, 0x2580 },
            { Opcode.SMSG_INIT_WORLD_STATES, 0x274A },
            { Opcode.SMSG_INSPECT_HONOR_STATS, 0x2933 }, // NYI
            { Opcode.SMSG_INSPECT_PVP, 0x2726 }, // NYI
            { Opcode.SMSG_INSPECT_RESULT, 0x2633 },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_CHANGE_PRIORITY, 0x27B5 },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_DISENGAGE_UNIT, 0x27B4 },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_END, 0x27BD },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_ENGAGE_UNIT, 0x27B3 },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_GAIN_COMBAT_RESURRECTION_CHARGE, 0x27BF },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_IN_COMBAT_RESURRECTION, 0x27BE },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_OBJECTIVE_COMPLETE, 0x27B8 },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_OBJECTIVE_START, 0x27B7 },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_OBJECTIVE_UPDATE, 0x27BC },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_PHASE_SHIFT_CHANGED, 0x27C0 },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_START, 0x27B9 },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_TIMER_START, 0x27B6 },
            { Opcode.SMSG_INSTANCE_ENCOUNTER_UPDATE_ALLOW_RELEASE_IN_PROGRESS, 0x27BB }, // NYI
            { Opcode.SMSG_INSTANCE_ENCOUNTER_UPDATE_SUPPRESS_RELEASE, 0x27BA }, // NYI
            { Opcode.SMSG_INSTANCE_GROUP_SIZE_CHANGED, 0x26FE }, // NYI
            { Opcode.SMSG_INSTANCE_INFO, 0x2636 },
            { Opcode.SMSG_INSTANCE_RESET, 0x268A },
            { Opcode.SMSG_INSTANCE_RESET_FAILED, 0x268B },
            { Opcode.SMSG_INSTANCE_SAVE_CREATED, 0x2784 },
            { Opcode.SMSG_INTERRUPT_POWER_REGEN, 0x2C58 },
            { Opcode.SMSG_INVALIDATE_PAGE_TEXT, 0x2918 }, // NYI
            { Opcode.SMSG_INVALIDATE_PLAYER, 0x2FFF },
            { Opcode.SMSG_INVALID_PROMOTION_CODE, 0x2757 }, // NYI
            { Opcode.SMSG_INVENTORY_CHANGE_FAILURE, 0x2DA5 },
            { Opcode.SMSG_INVENTORY_FIXUP_COMPLETE, 0x2817 }, // NYI
            { Opcode.SMSG_INVENTORY_FULL_OVERFLOW, 0x2828 },
            { Opcode.SMSG_ISLAND_AZERITE_GAIN, 0x2760 }, // NYI
            { Opcode.SMSG_ISLAND_COMPLETE, 0x2761 }, // NYI
            { Opcode.SMSG_IS_QUEST_COMPLETE_RESPONSE, 0x2A84 }, // NYI
            { Opcode.SMSG_ITEM_CHANGED, 0x26EF }, // NYI
            { Opcode.SMSG_ITEM_COOLDOWN, 0x27CB },
            { Opcode.SMSG_ITEM_ENCHANT_TIME_UPDATE, 0x2759 },
            { Opcode.SMSG_ITEM_EXPIRE_PURCHASE_REFUND, 0x259E },
            { Opcode.SMSG_ITEM_PURCHASE_REFUND_RESULT, 0x259C },
            { Opcode.SMSG_ITEM_PUSH_RESULT, 0x2625 },
            { Opcode.SMSG_ITEM_TIME_UPDATE, 0x2758 },
            { Opcode.SMSG_KICK_REASON, 0x268D }, // NYI
            { Opcode.SMSG_LATENCY_REPORT_PING, 0x2886 }, // NYI
            { Opcode.SMSG_LEARNED_SPELLS, 0x2C4C },
            { Opcode.SMSG_LEARN_PVP_TALENT_FAILED, 0x25D6 }, // NYI
            { Opcode.SMSG_LEARN_TALENT_FAILED, 0x25D5 },
            { Opcode.SMSG_LEGACY_LOOT_RULES, 0x282D },
            { Opcode.SMSG_LEVEL_UP_INFO, 0x26EE },
            { Opcode.SMSG_LFG_BOOT_PLAYER, 0x2A35 },
            { Opcode.SMSG_LFG_DISABLED, 0x2A33 },
            { Opcode.SMSG_LFG_EXPAND_SEARCH_PROMPT, 0x2A3B }, // NYI
            { Opcode.SMSG_LFG_INSTANCE_SHUTDOWN_COUNTDOWN, 0x2A25 }, // NYI
            { Opcode.SMSG_LFG_JOIN_RESULT, 0x2A1C },
            { Opcode.SMSG_LFG_LIST_APPLICANT_LIST_UPDATE, 0x2A2B }, // NYI
            { Opcode.SMSG_LFG_LIST_APPLICATION_STATUS_UPDATE, 0x2A28 }, // NYI
            { Opcode.SMSG_LFG_LIST_APPLY_TO_GROUP_RESULT, 0x2A29 }, // NYI
            { Opcode.SMSG_LFG_LIST_JOIN_RESULT, 0x2A1D }, // NYI
            { Opcode.SMSG_LFG_LIST_SEARCH_RESULTS, 0x2A1E }, // NYI
            { Opcode.SMSG_LFG_LIST_SEARCH_RESULTS_UPDATE, 0x2A2C }, // NYI
            { Opcode.SMSG_LFG_LIST_SEARCH_STATUS, 0x2A1F }, // NYI
            { Opcode.SMSG_LFG_LIST_UPDATE_BLACKLIST, 0x2A2A },
            { Opcode.SMSG_LFG_LIST_UPDATE_EXPIRATION, 0x2A27 }, // NYI
            { Opcode.SMSG_LFG_LIST_UPDATE_STATUS, 0x2A26 }, // NYI
            { Opcode.SMSG_LFG_OFFER_CONTINUE, 0x2A34 },
            { Opcode.SMSG_LFG_PARTY_INFO, 0x2A36 },
            { Opcode.SMSG_LFG_PLAYER_INFO, 0x2A37 },
            { Opcode.SMSG_LFG_PLAYER_REWARD, 0x2A38 },
            { Opcode.SMSG_LFG_PROPOSAL_UPDATE, 0x2A2D },
            { Opcode.SMSG_LFG_QUEUE_STATUS, 0x2A20 },
            { Opcode.SMSG_LFG_READY_CHECK_RESULT, 0x2A3A }, // NYI
            { Opcode.SMSG_LFG_READY_CHECK_UPDATE, 0x2A22 }, // NYI
            { Opcode.SMSG_LFG_ROLE_CHECK_UPDATE, 0x2A21 },
            { Opcode.SMSG_LFG_SLOT_INVALID, 0x2A30 }, // NYI
            { Opcode.SMSG_LFG_TELEPORT_DENIED, 0x2A32 },
            { Opcode.SMSG_LFG_UPDATE_STATUS, 0x2A24 },
            { Opcode.SMSG_LIVE_REGION_ACCOUNT_RESTORE_RESULT, 0x2776 }, // NYI
            { Opcode.SMSG_LIVE_REGION_CHARACTER_COPY_RESULT, 0x2775 }, // NYI
            { Opcode.SMSG_LIVE_REGION_GET_ACCOUNT_CHARACTER_LIST_RESULT, 0x276A }, // NYI
            { Opcode.SMSG_LOAD_CUF_PROFILES, 0x25BE },
            { Opcode.SMSG_LOAD_EQUIPMENT_SET, 0x2712 },
            { Opcode.SMSG_LOBBY_MATCHMAKER_LOBBY_ACQUIRED_SERVER, 0x2890 }, // NYI
            { Opcode.SMSG_LOBBY_MATCHMAKER_PARTY_INFO, 0x2891 }, // NYI
            { Opcode.SMSG_LOBBY_MATCHMAKER_PARTY_INVITE_REJECTED, 0x2892 }, // NYI
            { Opcode.SMSG_LOBBY_MATCHMAKER_RECEIVE_INVITE, 0x2893 }, // NYI
            { Opcode.SMSG_LOGIN_SET_TIME_SPEED, 0x2711 },
            { Opcode.SMSG_LOGIN_VERIFY_WORLD, 0x2599 },
            { Opcode.SMSG_LOGOUT_CANCEL_ACK, 0x2689 },
            { Opcode.SMSG_LOGOUT_COMPLETE, 0x2688 },
            { Opcode.SMSG_LOGOUT_RESPONSE, 0x2687 },
            { Opcode.SMSG_LOG_XP_GAIN, 0x26EA },
            { Opcode.SMSG_LOOT_ALL_PASSED, 0x2623 },
            { Opcode.SMSG_LOOT_LIST, 0x2745 },
            { Opcode.SMSG_LOOT_MONEY_NOTIFY, 0x261E },
            { Opcode.SMSG_LOOT_RELEASE, 0x261D },
            { Opcode.SMSG_LOOT_RELEASE_ALL, 0x261C },
            { Opcode.SMSG_LOOT_REMOVED, 0x2617 },
            { Opcode.SMSG_LOOT_RESPONSE, 0x2616 },
            { Opcode.SMSG_LOOT_ROLL, 0x2620 },
            { Opcode.SMSG_LOOT_ROLLS_COMPLETE, 0x2622 },
            { Opcode.SMSG_LOOT_ROLL_WON, 0x2624 },
            { Opcode.SMSG_LOSS_OF_CONTROL_AURA_UPDATE, 0x2674 }, // NYI
            { Opcode.SMSG_MAIL_COMMAND_RESULT, 0x263D },
            { Opcode.SMSG_MAIL_LIST_RESULT, 0x275A },
            { Opcode.SMSG_MAIL_QUERY_NEXT_TIME_RESULT, 0x275B },
            { Opcode.SMSG_MAP_OBJECTIVES_INIT, 0x2950 }, // NYI
            { Opcode.SMSG_MAP_OBJ_EVENTS, 0x25C8 }, // NYI
            { Opcode.SMSG_MASTER_LOOT_CANDIDATE_LIST, 0x2621 },
            { Opcode.SMSG_MESSAGE_BOX, 0x2576 }, // NYI
            { Opcode.SMSG_MINIMAP_PING, 0x26D2 },
            { Opcode.SMSG_MIRROR_IMAGE_COMPONENTED_DATA, 0x2C14 },
            { Opcode.SMSG_MIRROR_IMAGE_CREATURE_DATA, 0x2C13 },
            { Opcode.SMSG_MISSILE_CANCEL, 0x25C9 },
            { Opcode.SMSG_MODIFY_COOLDOWN, 0x276C },
            { Opcode.SMSG_MOUNT_RESULT, 0x257B },
            { Opcode.SMSG_MOVEMENT_ENFORCEMENT_ALERT, 0x2849 }, // NYI
            { Opcode.SMSG_MOVE_APPLY_INERTIA, 0x2E2E }, // NYI
            { Opcode.SMSG_MOVE_APPLY_MOVEMENT_FORCE, 0x2E15 },
            { Opcode.SMSG_MOVE_DISABLE_COLLISION, 0x2E11 },
            { Opcode.SMSG_MOVE_DISABLE_DOUBLE_JUMP, 0x2DFD },
            { Opcode.SMSG_MOVE_DISABLE_GRAVITY, 0x2E0D },
            { Opcode.SMSG_MOVE_DISABLE_INERTIA, 0x2E0F },
            { Opcode.SMSG_MOVE_DISABLE_TRANSITION_BETWEEN_SWIM_AND_FLY, 0x2E0C },
            { Opcode.SMSG_MOVE_ENABLE_COLLISION, 0x2E12 },
            { Opcode.SMSG_MOVE_ENABLE_DOUBLE_JUMP, 0x2DFC },
            { Opcode.SMSG_MOVE_ENABLE_GRAVITY, 0x2E0E },
            { Opcode.SMSG_MOVE_ENABLE_INERTIA, 0x2E10 },
            { Opcode.SMSG_MOVE_ENABLE_TRANSITION_BETWEEN_SWIM_AND_FLY, 0x2E0B },
            { Opcode.SMSG_MOVE_KNOCK_BACK, 0x2E03 },
            { Opcode.SMSG_MOVE_REMOVE_INERTIA, 0x2E2F }, // NYI
            { Opcode.SMSG_MOVE_REMOVE_MOVEMENT_FORCE, 0x2E16 },
            { Opcode.SMSG_MOVE_ROOT, 0x2DF9 },
            { Opcode.SMSG_MOVE_SET_ACTIVE_MOVER, 0x2DD5 },
            { Opcode.SMSG_MOVE_SET_CAN_FLY, 0x2E05 },
            { Opcode.SMSG_MOVE_SET_CAN_TURN_WHILE_FALLING, 0x2E07 },
            { Opcode.SMSG_MOVE_SET_COLLISION_HEIGHT, 0x2E13 },
            { Opcode.SMSG_MOVE_SET_COMPOUND_STATE, 0x2E17 },
            { Opcode.SMSG_MOVE_SET_FEATHER_FALL, 0x2DFF },
            { Opcode.SMSG_MOVE_SET_FLIGHT_BACK_SPEED, 0x2DF5 },
            { Opcode.SMSG_MOVE_SET_FLIGHT_SPEED, 0x2DF4 },
            { Opcode.SMSG_MOVE_SET_HOVERING, 0x2E01 },
            { Opcode.SMSG_MOVE_SET_IGNORE_MOVEMENT_FORCES, 0x2E09 },
            { Opcode.SMSG_MOVE_SET_LAND_WALK, 0x2DFE },
            { Opcode.SMSG_MOVE_SET_MOD_MOVEMENT_FORCE_MAGNITUDE, 0x2DE6 },
            { Opcode.SMSG_MOVE_SET_NORMAL_FALL, 0x2E00 },
            { Opcode.SMSG_MOVE_SET_PITCH_RATE, 0x2DF8 },
            { Opcode.SMSG_MOVE_SET_RUN_BACK_SPEED, 0x2DF1 },
            { Opcode.SMSG_MOVE_SET_RUN_SPEED, 0x2DF0 },
            { Opcode.SMSG_MOVE_SET_SWIM_BACK_SPEED, 0x2DF3 },
            { Opcode.SMSG_MOVE_SET_SWIM_SPEED, 0x2DF2 },
            { Opcode.SMSG_MOVE_SET_TURN_RATE, 0x2DF7 },
            { Opcode.SMSG_MOVE_SET_VEHICLE_REC_ID, 0x2E14 },
            { Opcode.SMSG_MOVE_SET_WALK_SPEED, 0x2DF6 },
            { Opcode.SMSG_MOVE_SET_WATER_WALK, 0x2DFB },
            { Opcode.SMSG_MOVE_SKIP_TIME, 0x2E18 },
            { Opcode.SMSG_MOVE_SPLINE_DISABLE_COLLISION, 0x2E1D },
            { Opcode.SMSG_MOVE_SPLINE_DISABLE_GRAVITY, 0x2E1B },
            { Opcode.SMSG_MOVE_SPLINE_ENABLE_COLLISION, 0x2E1E },
            { Opcode.SMSG_MOVE_SPLINE_ENABLE_GRAVITY, 0x2E1C },
            { Opcode.SMSG_MOVE_SPLINE_ROOT, 0x2E19 },
            { Opcode.SMSG_MOVE_SPLINE_SET_FEATHER_FALL, 0x2E1F },
            { Opcode.SMSG_MOVE_SPLINE_SET_FLIGHT_BACK_SPEED, 0x2DEC },
            { Opcode.SMSG_MOVE_SPLINE_SET_FLIGHT_SPEED, 0x2DEB },
            { Opcode.SMSG_MOVE_SPLINE_SET_FLYING, 0x2E29 },
            { Opcode.SMSG_MOVE_SPLINE_SET_HOVER, 0x2E21 },
            { Opcode.SMSG_MOVE_SPLINE_SET_LAND_WALK, 0x2E24 },
            { Opcode.SMSG_MOVE_SPLINE_SET_NORMAL_FALL, 0x2E20 },
            { Opcode.SMSG_MOVE_SPLINE_SET_PITCH_RATE, 0x2DEF },
            { Opcode.SMSG_MOVE_SPLINE_SET_RUN_BACK_SPEED, 0x2DE8 },
            { Opcode.SMSG_MOVE_SPLINE_SET_RUN_MODE, 0x2E27 },
            { Opcode.SMSG_MOVE_SPLINE_SET_RUN_SPEED, 0x2DE7 },
            { Opcode.SMSG_MOVE_SPLINE_SET_SWIM_BACK_SPEED, 0x2DEA },
            { Opcode.SMSG_MOVE_SPLINE_SET_SWIM_SPEED, 0x2DE9 },
            { Opcode.SMSG_MOVE_SPLINE_SET_TURN_RATE, 0x2DEE },
            { Opcode.SMSG_MOVE_SPLINE_SET_WALK_MODE, 0x2E28 },
            { Opcode.SMSG_MOVE_SPLINE_SET_WALK_SPEED, 0x2DED },
            { Opcode.SMSG_MOVE_SPLINE_SET_WATER_WALK, 0x2E23 },
            { Opcode.SMSG_MOVE_SPLINE_START_SWIM, 0x2E25 },
            { Opcode.SMSG_MOVE_SPLINE_STOP_SWIM, 0x2E26 },
            { Opcode.SMSG_MOVE_SPLINE_UNROOT, 0x2E1A },
            { Opcode.SMSG_MOVE_SPLINE_UNSET_FLYING, 0x2E2A },
            { Opcode.SMSG_MOVE_SPLINE_UNSET_HOVER, 0x2E22 },
            { Opcode.SMSG_MOVE_TELEPORT, 0x2E04 },
            { Opcode.SMSG_MOVE_UNROOT, 0x2DFA },
            { Opcode.SMSG_MOVE_UNSET_CAN_FLY, 0x2E06 },
            { Opcode.SMSG_MOVE_UNSET_CAN_TURN_WHILE_FALLING, 0x2E08 },
            { Opcode.SMSG_MOVE_UNSET_HOVERING, 0x2E02 },
            { Opcode.SMSG_MOVE_UNSET_IGNORE_MOVEMENT_FORCES, 0x2E0A },
            { Opcode.SMSG_MOVE_UPDATE, 0x2DE0 },
            { Opcode.SMSG_MOVE_UPDATE_APPLY_INERTIA, 0x2E30 }, // NYI
            { Opcode.SMSG_MOVE_UPDATE_APPLY_MOVEMENT_FORCE, 0x2DE4 },
            { Opcode.SMSG_MOVE_UPDATE_COLLISION_HEIGHT, 0x2DDF },
            { Opcode.SMSG_MOVE_UPDATE_FLIGHT_BACK_SPEED, 0x2DDC },
            { Opcode.SMSG_MOVE_UPDATE_FLIGHT_SPEED, 0x2DDB },
            { Opcode.SMSG_MOVE_UPDATE_KNOCK_BACK, 0x2DE2 },
            { Opcode.SMSG_MOVE_UPDATE_MOD_MOVEMENT_FORCE_MAGNITUDE, 0x2DE3 },
            { Opcode.SMSG_MOVE_UPDATE_PITCH_RATE, 0x2DDE },
            { Opcode.SMSG_MOVE_UPDATE_REMOVE_INERTIA, 0x2E31 }, // NYI
            { Opcode.SMSG_MOVE_UPDATE_REMOVE_MOVEMENT_FORCE, 0x2DE5 },
            { Opcode.SMSG_MOVE_UPDATE_RUN_BACK_SPEED, 0x2DD7 },
            { Opcode.SMSG_MOVE_UPDATE_RUN_SPEED, 0x2DD6 },
            { Opcode.SMSG_MOVE_UPDATE_SWIM_BACK_SPEED, 0x2DDA },
            { Opcode.SMSG_MOVE_UPDATE_SWIM_SPEED, 0x2DD9 },
            { Opcode.SMSG_MOVE_UPDATE_TELEPORT, 0x2DE1 },
            { Opcode.SMSG_MOVE_UPDATE_TURN_RATE, 0x2DDD },
            { Opcode.SMSG_MOVE_UPDATE_WALK_SPEED, 0x2DD8 },
            { Opcode.SMSG_MULTIPLE_PACKETS, 0x3051 },
            { Opcode.SMSG_NEUTRAL_PLAYER_FACTION_SELECT_RESULT, 0x25DE }, // NYI
            { Opcode.SMSG_NEW_DATA_BUILD, 0x28AF }, // NYI
            { Opcode.SMSG_NEW_TAXI_PATH, 0x2683 },
            { Opcode.SMSG_NEW_WORLD, 0x2596 },
            { Opcode.SMSG_NOTIFY_DEST_LOC_SPELL_CAST, 0x2C42 }, // NYI
            { Opcode.SMSG_NOTIFY_MISSILE_TRAJECTORY_COLLISION, 0x26AE },
            { Opcode.SMSG_NOTIFY_MONEY, 0x259B }, // NYI
            { Opcode.SMSG_NOTIFY_RECEIVED_MAIL, 0x263E },
            { Opcode.SMSG_NPC_INTERACTION_OPEN_RESULT, 0x288C },
            { Opcode.SMSG_OFFER_PETITION_ERROR, 0x26BA },
            { Opcode.SMSG_ON_CANCEL_EXPECTED_RIDE_VEHICLE_AURA, 0x26EB },
            { Opcode.SMSG_ON_MONSTER_MOVE, 0x2DD4 },
            { Opcode.SMSG_OPEN_CONTAINER, 0x2DA6 }, // NYI
            { Opcode.SMSG_OPEN_LFG_DUNGEON_FINDER, 0x2A31 }, // NYI
            { Opcode.SMSG_OVERRIDE_LIGHT, 0x26BF },
            { Opcode.SMSG_PAGE_TEXT, 0x271D },
            { Opcode.SMSG_PARTY_COMMAND_RESULT, 0x2799 },
            { Opcode.SMSG_PARTY_INVITE, 0x25BF },
            { Opcode.SMSG_PARTY_KILL_LOG, 0x275E },
            { Opcode.SMSG_PARTY_MEMBER_FULL_STATE, 0x275D },
            { Opcode.SMSG_PARTY_MEMBER_PARTIAL_STATE, 0x275C }, // NYI
            { Opcode.SMSG_PARTY_NOTIFY_LFG_LEADER_CHANGE, 0x2879 }, // NYI
            { Opcode.SMSG_PARTY_UPDATE, 0x25F6 },
            { Opcode.SMSG_PAUSE_MIRROR_TIMER, 0x2714 },
            { Opcode.SMSG_PENDING_RAID_LOCK, 0x26FC },
            { Opcode.SMSG_PETITION_ALREADY_SIGNED, 0x25A1 },
            { Opcode.SMSG_PETITION_RENAME_GUILD_RESPONSE, 0x29FB },
            { Opcode.SMSG_PETITION_SHOW_LIST, 0x26C3 },
            { Opcode.SMSG_PETITION_SHOW_SIGNATURES, 0x26C4 },
            { Opcode.SMSG_PETITION_SIGN_RESULTS, 0x2750 },
            { Opcode.SMSG_PET_ACTION_FEEDBACK, 0x274D },
            { Opcode.SMSG_PET_ACTION_SOUND, 0x26A4 },
            { Opcode.SMSG_PET_BATTLE_SLOT_UPDATES, 0x25EE },
            { Opcode.SMSG_PET_CAST_FAILED, 0x2C57 },
            { Opcode.SMSG_PET_CLEAR_SPELLS, 0x2C23 }, // NYI
            { Opcode.SMSG_PET_DISMISS_SOUND, 0x26A5 }, // NYI
            { Opcode.SMSG_PET_GOD_MODE, 0x267F }, // NYI
            { Opcode.SMSG_PET_GUIDS, 0x2708 }, // NYI
            { Opcode.SMSG_PET_LEARNED_SPELLS, 0x2C4E },
            { Opcode.SMSG_PET_MODE, 0x2589 },
            { Opcode.SMSG_PET_NAME_INVALID, 0x26C8 },
            { Opcode.SMSG_PET_NEWLY_TAMED, 0x2588 }, // NYI
            { Opcode.SMSG_PET_SPELLS_MESSAGE, 0x2C24 },
            { Opcode.SMSG_PET_STABLE_RESULT, 0x2595 },
            { Opcode.SMSG_PET_TAME_FAILURE, 0x26B7 },
            { Opcode.SMSG_PET_UNLEARNED_SPELLS, 0x2C4F },
            { Opcode.SMSG_PHASE_SHIFT_CHANGE, 0x2578 },
            { Opcode.SMSG_PLAYED_TIME, 0x26D9 },
            { Opcode.SMSG_PLAYER_ACKNOWLEDGE_ARROW_CALLOUT, 0x3022 }, // NYI
            { Opcode.SMSG_PLAYER_BATTLEFIELD_AUTO_QUEUE, 0x301C }, // NYI
            { Opcode.SMSG_PLAYER_BONUS_ROLL_FAILED, 0x3016 }, // NYI
            { Opcode.SMSG_PLAYER_BOUND, 0x2FF8 },
            { Opcode.SMSG_PLAYER_CHOICE_CLEAR, 0x2FFE }, // NYI
            { Opcode.SMSG_PLAYER_CHOICE_DISPLAY_ERROR, 0x2FFD }, // NYI
            { Opcode.SMSG_PLAYER_CONDITION_RESULT, 0x300A }, // NYI
            { Opcode.SMSG_PLAYER_HIDE_ARROW_CALLOUT, 0x3021 }, // NYI
            { Opcode.SMSG_PLAYER_IS_ADVENTURE_MAP_POI_VALID, 0x3009 },
            { Opcode.SMSG_PLAYER_SAVE_GUILD_EMBLEM, 0x29FA },
            { Opcode.SMSG_PLAYER_SHOW_ARROW_CALLOUT, 0x3020 }, // NYI
            { Opcode.SMSG_PLAYER_SHOW_GENERIC_WIDGET_DISPLAY, 0x301E }, // NYI
            { Opcode.SMSG_PLAYER_SHOW_PARTY_POSE_UI, 0x301F }, // NYI
            { Opcode.SMSG_PLAYER_SHOW_UI_EVENT_TOAST, 0x3019 }, // NYI
            { Opcode.SMSG_PLAYER_SKINNED, 0x3006 }, // NYI
            { Opcode.SMSG_PLAYER_TUTORIAL_HIGHLIGHT_SPELL, 0x300D }, // NYI
            { Opcode.SMSG_PLAYER_TUTORIAL_UNHIGHLIGHT_SPELL, 0x300C }, // NYI
            { Opcode.SMSG_PLAYER_WORLD_PVP_QUEUE, 0x301D }, // NYI
            { Opcode.SMSG_PLAY_MUSIC, 0x2771 },
            { Opcode.SMSG_PLAY_OBJECT_SOUND, 0x2772 },
            { Opcode.SMSG_PLAY_ONE_SHOT_ANIM_KIT, 0x2735 },
            { Opcode.SMSG_PLAY_ORPHAN_SPELL_VISUAL, 0x2C46 },
            { Opcode.SMSG_PLAY_SCENE, 0x2638 },
            { Opcode.SMSG_PLAY_SOUND, 0x2770 },
            { Opcode.SMSG_PLAY_SPEAKERBOT_SOUND, 0x2773 },
            { Opcode.SMSG_PLAY_SPELL_VISUAL, 0x2C44 },
            { Opcode.SMSG_PLAY_SPELL_VISUAL_KIT, 0x2C48 },
            { Opcode.SMSG_PONG, 0x304E },
            { Opcode.SMSG_POWER_UPDATE, 0x26D6 },
            { Opcode.SMSG_PRELOAD_CHILD_MAP, 0x2579 }, // NYI
            { Opcode.SMSG_PRELOAD_WORLD, 0x2597 }, // NYI
            { Opcode.SMSG_PREPOPULATE_NAME_CACHE, 0x284C }, // NYI
            { Opcode.SMSG_PRE_RESSURECT, 0x276F },
            { Opcode.SMSG_PRINT_NOTIFICATION, 0x25CC },
            { Opcode.SMSG_PROC_RESIST, 0x275F },
            { Opcode.SMSG_PROPOSE_LEVEL_GRANT, 0x26E1 }, // NYI
            { Opcode.SMSG_PUSH_SPELL_TO_ACTION_BAR, 0x2C50 }, // NYI
            { Opcode.SMSG_PVP_CREDIT, 0x294A },
            { Opcode.SMSG_PVP_LOG_DATA, 0x2934 },
            { Opcode.SMSG_PVP_MATCH_INITIALIZE, 0x2956 },
            { Opcode.SMSG_PVP_MATCH_START, 0x2953 },
            { Opcode.SMSG_PVP_OPTIONS_ENABLED, 0x2938 },
            { Opcode.SMSG_PVP_SEASON, 0x25C3 },
            { Opcode.SMSG_QUERY_ARENA_TEAM_RESPONSE, 0x2920 },
            { Opcode.SMSG_QUERY_BATTLE_PET_NAME_RESPONSE, 0x291A },
            { Opcode.SMSG_QUERY_CREATURE_RESPONSE, 0x2914 },
            { Opcode.SMSG_QUERY_GAME_OBJECT_RESPONSE, 0x2915 },
            { Opcode.SMSG_QUERY_GUILD_INFO_RESPONSE, 0x29E6 },
            { Opcode.SMSG_QUERY_ITEM_TEXT_RESPONSE, 0x291E },
            { Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE, 0x2916 },
            { Opcode.SMSG_QUERY_PAGE_TEXT_RESPONSE, 0x2917 },
            { Opcode.SMSG_QUERY_PETITION_RESPONSE, 0x291B },
            { Opcode.SMSG_QUERY_PET_NAME_RESPONSE, 0x2919 },
            { Opcode.SMSG_QUERY_PLAYER_NAMES_RESPONSE, 0x301B },
            { Opcode.SMSG_QUERY_PLAYER_NAME_BY_COMMUNITY_ID_RESPONSE, 0x3002 }, // NYI
            { Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE, 0x2A96 },
            { Opcode.SMSG_QUERY_TIME_RESPONSE, 0x26E9 },
            { Opcode.SMSG_QUEST_COMPLETION_NPC_RESPONSE, 0x2A81 },
            { Opcode.SMSG_QUEST_CONFIRM_ACCEPT, 0x2A8F },
            { Opcode.SMSG_QUEST_FORCE_REMOVED, 0x2A9C },
            { Opcode.SMSG_QUEST_GIVER_INVALID_QUEST, 0x2A85 },
            { Opcode.SMSG_QUEST_GIVER_OFFER_REWARD_MESSAGE, 0x2A94 },
            { Opcode.SMSG_QUEST_GIVER_QUEST_COMPLETE, 0x2A83 },
            { Opcode.SMSG_QUEST_GIVER_QUEST_DETAILS, 0x2A92 },
            { Opcode.SMSG_QUEST_GIVER_QUEST_FAILED, 0x2A86 },
            { Opcode.SMSG_QUEST_GIVER_QUEST_LIST_MESSAGE, 0x2A9A },
            { Opcode.SMSG_QUEST_GIVER_REQUEST_ITEMS, 0x2A93 },
            { Opcode.SMSG_QUEST_GIVER_STATUS, 0x2A9B },
            { Opcode.SMSG_QUEST_GIVER_STATUS_MULTIPLE, 0x2A91 },
            { Opcode.SMSG_QUEST_ITEM_USABILITY_RESPONSE, 0x2A82 }, // NYI
            { Opcode.SMSG_QUEST_LOG_FULL, 0x2A87 },
            { Opcode.SMSG_QUEST_NON_LOG_UPDATE_COMPLETE, 0x2A88 },
            { Opcode.SMSG_QUEST_POI_QUERY_RESPONSE, 0x2A9D },
            { Opcode.SMSG_QUEST_POI_UPDATE_RESPONSE, 0x2A9F }, // NYI
            { Opcode.SMSG_QUEST_PUSH_RESULT, 0x2A90 },
            { Opcode.SMSG_QUEST_SESSION_READY_CHECK, 0x285D }, // NYI
            { Opcode.SMSG_QUEST_SESSION_READY_CHECK_RESPONSE, 0x285E }, // NYI
            { Opcode.SMSG_QUEST_SESSION_RESULT, 0x285C }, // NYI
            { Opcode.SMSG_QUEST_UPDATE_ADD_CREDIT, 0x2A8C },
            { Opcode.SMSG_QUEST_UPDATE_ADD_CREDIT_SIMPLE, 0x2A8D },
            { Opcode.SMSG_QUEST_UPDATE_ADD_PVP_CREDIT, 0x2A8E },
            { Opcode.SMSG_QUEST_UPDATE_COMPLETE, 0x2A89 },
            { Opcode.SMSG_QUEST_UPDATE_FAILED, 0x2A8A }, // NYI
            { Opcode.SMSG_QUEST_UPDATE_FAILED_TIMER, 0x2A8B },
            { Opcode.SMSG_QUEUE_SUMMARY_UPDATE, 0x2816 }, // NYI
            { Opcode.SMSG_RAID_DIFFICULTY_SET, 0x27B0 },
            { Opcode.SMSG_RAID_GROUP_ONLY, 0x27B2 },
            { Opcode.SMSG_RAID_INSTANCE_MESSAGE, 0x2BB4 },
            { Opcode.SMSG_RAID_MARKERS_CHANGED, 0x25A2 },
            { Opcode.SMSG_RANDOM_ROLL, 0x2632 },
            { Opcode.SMSG_RATED_PVP_INFO, 0x2931 },
            { Opcode.SMSG_READY_CHECK_COMPLETED, 0x25FA },
            { Opcode.SMSG_READY_CHECK_RESPONSE, 0x25F9 },
            { Opcode.SMSG_READY_CHECK_STARTED, 0x25F8 },
            { Opcode.SMSG_READ_ITEM_RESULT_FAILED, 0x27AC },
            { Opcode.SMSG_READ_ITEM_RESULT_OK, 0x27A4 },
            { Opcode.SMSG_REALM_QUERY_RESPONSE, 0x2913 },
            { Opcode.SMSG_REATTACH_RESURRECT, 0x274F }, // NYI
            { Opcode.SMSG_RECRUIT_A_FRIEND_FAILURE, 0x26C5 },
            { Opcode.SMSG_REFER_A_FRIEND_EXPIRED, 0x2727 }, // NYI
            { Opcode.SMSG_REFORGE_RESULT, 0x2581 }, // NYI
            { Opcode.SMSG_REFRESH_COMPONENT, 0x2653 }, // NYI
            { Opcode.SMSG_REFRESH_SPELL_HISTORY, 0x2C2B },
            { Opcode.SMSG_REMOVE_ITEM_PASSIVE, 0x25AD },
            { Opcode.SMSG_REPORT_PVP_PLAYER_AFK_RESULT, 0x3001 },
            { Opcode.SMSG_REQUEST_CEMETERY_LIST_RESPONSE, 0x2590 },
            { Opcode.SMSG_REQUEST_PVP_REWARDS_RESPONSE, 0x2939 },
            { Opcode.SMSG_REQUEST_SCHEDULED_PVP_INFO_RESPONSE, 0x293A }, // NYI
            { Opcode.SMSG_RESET_COMPRESSION_CONTEXT, 0x304F }, // NYI
            { Opcode.SMSG_RESET_FAILED_NOTIFY, 0x26BB },
            { Opcode.SMSG_RESET_QUEST_POI, 0x2AA0 }, // NYI
            { Opcode.SMSG_RESET_RANGED_COMBAT_TIMER, 0x2949 }, // NYI
            { Opcode.SMSG_RESET_WEEKLY_CURRENCY, 0x2575 },
            { Opcode.SMSG_RESPEC_WIPE_CONFIRM, 0x2614 },
            { Opcode.SMSG_RESPOND_INSPECT_ACHIEVEMENTS, 0x2572 },
            { Opcode.SMSG_RESUME_CAST, 0x2C3A }, // NYI
            { Opcode.SMSG_RESUME_CAST_BAR, 0x2C3D }, // NYI
            { Opcode.SMSG_RESUME_COMMS, 0x304B },
            { Opcode.SMSG_RESUME_TOKEN, 0x25AB },
            { Opcode.SMSG_RESURRECT_REQUEST, 0x257E },
            { Opcode.SMSG_RESYNC_RUNES, 0x2C5E },
            { Opcode.SMSG_ROLE_CHANGED_INFORM, 0x258B },
            { Opcode.SMSG_ROLE_CHOSEN, 0x2A39 },
            { Opcode.SMSG_ROLE_POLL_INFORM, 0x258C },
            { Opcode.SMSG_RUNE_REGEN_DEBUG, 0x25B8 }, // NYI
            { Opcode.SMSG_SCENARIO_COMPLETED, 0x27F0 },
            { Opcode.SMSG_SCENARIO_POIS, 0x2635 },
            { Opcode.SMSG_SCENARIO_PROGRESS_UPDATE, 0x262E },
            { Opcode.SMSG_SCENARIO_SHOW_CRITERIA, 0x2806 }, // NYI
            { Opcode.SMSG_SCENARIO_STATE, 0x262D },
            { Opcode.SMSG_SCENARIO_UI_UPDATE, 0x2805 }, // NYI
            { Opcode.SMSG_SCENARIO_VACATE, 0x27AD },
            { Opcode.SMSG_SCENE_OBJECT_EVENT, 0x25E4 }, // NYI
            { Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_FINAL_ROUND, 0x25E9 }, // NYI
            { Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_FINISHED, 0x25EA }, // NYI
            { Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_FIRST_ROUND, 0x25E6 }, // NYI
            { Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_INITIAL_UPDATE, 0x25E5 }, // NYI
            { Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_REPLACEMENTS_MADE, 0x25E8 }, // NYI
            { Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_ROUND_RESULT, 0x25E7 }, // NYI
            { Opcode.SMSG_SCRIPT_CAST, 0x2C55 }, // NYI
            { Opcode.SMSG_SELL_RESPONSE, 0x26C9 },
            { Opcode.SMSG_SEND_ITEM_PASSIVES, 0x25AE },
            { Opcode.SMSG_SEND_KNOWN_SPELLS, 0x2C29 },
            { Opcode.SMSG_SEND_RAID_TARGET_UPDATE_ALL, 0x2630 },
            { Opcode.SMSG_SEND_RAID_TARGET_UPDATE_SINGLE, 0x2631 },
            { Opcode.SMSG_SEND_SPELL_CHARGES, 0x2C2C },
            { Opcode.SMSG_SEND_SPELL_HISTORY, 0x2C2A },
            { Opcode.SMSG_SEND_UNLEARN_SPELLS, 0x2C2D },
            { Opcode.SMSG_SERVER_FIRST_ACHIEVEMENTS, 0x2650 }, // NYI
            { Opcode.SMSG_SERVER_TIME, 0x2686 }, // NYI
            { Opcode.SMSG_SERVER_TIME_OFFSET, 0x2718 },
            { Opcode.SMSG_SETUP_CURRENCY, 0x2573 },
            { Opcode.SMSG_SET_AI_ANIM_KIT, 0x2734 },
            { Opcode.SMSG_SET_ANIM_TIER, 0x2738 },
            { Opcode.SMSG_SET_CHR_UPGRADE_TIER, 0x25E1 }, // NYI
            { Opcode.SMSG_SET_CURRENCY, 0x2574 },
            { Opcode.SMSG_SET_DF_FAST_LAUNCH_RESULT, 0x2A2E }, // NYI
            { Opcode.SMSG_SET_DUNGEON_DIFFICULTY, 0x26A8 },
            { Opcode.SMSG_SET_FACTION_AT_WAR, 0x2704 }, // NYI
            { Opcode.SMSG_SET_FACTION_NOT_VISIBLE, 0x272F },
            { Opcode.SMSG_SET_FACTION_STANDING, 0x2730 },
            { Opcode.SMSG_SET_FACTION_VISIBLE, 0x272E },
            { Opcode.SMSG_SET_FLAT_SPELL_MODIFIER, 0x2C35 },
            { Opcode.SMSG_SET_FORCED_REACTIONS, 0x2721 },
            { Opcode.SMSG_SET_FORGE_MASTER, 0x2591 },
            { Opcode.SMSG_SET_ITEM_PURCHASE_DATA, 0x259D },
            { Opcode.SMSG_SET_LOOT_METHOD_FAILED, 0x27D4 }, // NYI
            { Opcode.SMSG_SET_MAX_WEEKLY_QUANTITY, 0x25A0 }, // NYI
            { Opcode.SMSG_SET_MELEE_ANIM_KIT, 0x2737 },
            { Opcode.SMSG_SET_MOVEMENT_ANIM_KIT, 0x2736 },
            { Opcode.SMSG_SET_PCT_SPELL_MODIFIER, 0x2C36 },
            { Opcode.SMSG_SET_PET_SPECIALIZATION, 0x2627 },
            { Opcode.SMSG_SET_PLAYER_DECLINED_NAMES_RESULT, 0x3003 },
            { Opcode.SMSG_SET_PLAY_HOVER_ANIM, 0x25BC },
            { Opcode.SMSG_SET_PROFICIENCY, 0x2739 },
            { Opcode.SMSG_SET_SPELL_CHARGES, 0x2C28 },
            { Opcode.SMSG_SET_TIME_ZONE_INFORMATION, 0x267B },
            { Opcode.SMSG_SET_VEHICLE_REC_ID, 0x26FB },
            { Opcode.SMSG_SHOW_NEUTRAL_PLAYER_FACTION_SELECT_UI, 0x25DD }, // NYI
            { Opcode.SMSG_SHOW_QUEST_COMPLETION_TEXT, 0x2A95 }, // NYI
            { Opcode.SMSG_SHOW_TAXI_NODES, 0x26D1 },
            { Opcode.SMSG_SHOW_TRADE_SKILL_RESPONSE, 0x2778 }, // NYI
            { Opcode.SMSG_SOCIAL_CONTRACT_REQUEST_RESPONSE, 0x2895 },
            { Opcode.SMSG_SOCKET_GEMS_FAILURE, 0x272C }, // NYI
            { Opcode.SMSG_SOCKET_GEMS_SUCCESS, 0x272B },
            { Opcode.SMSG_SOR_START_EXPERIENCE_INCOMPLETE, 0x25DF }, // NYI
            { Opcode.SMSG_SPECIAL_MOUNT_ANIM, 0x26A3 },
            { Opcode.SMSG_SPEC_INVOLUNTARILY_CHANGED, 0x271C }, // NYI
            { Opcode.SMSG_SPELL_ABSORB_LOG, 0x2C1C },
            { Opcode.SMSG_SPELL_CATEGORY_COOLDOWN, 0x2C16 }, // NYI
            { Opcode.SMSG_SPELL_CHANNEL_START, 0x2C33 },
            { Opcode.SMSG_SPELL_CHANNEL_UPDATE, 0x2C34 },
            { Opcode.SMSG_SPELL_COOLDOWN, 0x2C15 },
            { Opcode.SMSG_SPELL_DAMAGE_SHIELD, 0x2C30 },
            { Opcode.SMSG_SPELL_DELAYED, 0x2C3E },
            { Opcode.SMSG_SPELL_DISPELL_LOG, 0x2C17 },
            { Opcode.SMSG_SPELL_ENERGIZE_LOG, 0x2C19 },
            { Opcode.SMSG_SPELL_EXECUTE_LOG, 0x2C3F },
            { Opcode.SMSG_SPELL_FAILED_OTHER, 0x2C54 },
            { Opcode.SMSG_SPELL_FAILURE, 0x2C52 },
            { Opcode.SMSG_SPELL_FAILURE_MESSAGE, 0x2C59 }, // NYI
            { Opcode.SMSG_SPELL_GO, 0x2C38 },
            { Opcode.SMSG_SPELL_HEAL_ABSORB_LOG, 0x2C1B },
            { Opcode.SMSG_SPELL_HEAL_LOG, 0x2C1A },
            { Opcode.SMSG_SPELL_INSTAKILL_LOG, 0x2C32 },
            { Opcode.SMSG_SPELL_INTERRUPT_LOG, 0x2C1D },
            { Opcode.SMSG_SPELL_MISS_LOG, 0x2C40 },
            { Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG, 0x2C31 },
            { Opcode.SMSG_SPELL_OR_DAMAGE_IMMUNE, 0x2C2E },
            { Opcode.SMSG_SPELL_PERIODIC_AURA_LOG, 0x2C18 },
            { Opcode.SMSG_SPELL_PREPARE, 0x2C37 },
            { Opcode.SMSG_SPELL_START, 0x2C39 },
            { Opcode.SMSG_SPELL_VISUAL_LOAD_SCREEN, 0x25CE },
            { Opcode.SMSG_STAND_STATE_UPDATE, 0x2720 },
            { Opcode.SMSG_START_ELAPSED_TIMER, 0x2606 }, // NYI
            { Opcode.SMSG_START_ELAPSED_TIMERS, 0x2608 }, // NYI
            { Opcode.SMSG_START_LIGHTNING_STORM, 0x26AB },
            { Opcode.SMSG_START_LOOT_ROLL, 0x261F },
            { Opcode.SMSG_START_MIRROR_TIMER, 0x2713 },
            { Opcode.SMSG_START_TIMER, 0x25A7 },
            { Opcode.SMSG_STOP_ELAPSED_TIMER, 0x2607 }, // NYI
            { Opcode.SMSG_STOP_MIRROR_TIMER, 0x2715 },
            { Opcode.SMSG_STOP_SPEAKERBOT_SOUND, 0x2774 },
            { Opcode.SMSG_STREAMING_MOVIES, 0x25A6 }, // NYI
            { Opcode.SMSG_SUGGEST_INVITE_INFORM, 0x279A }, // NYI
            { Opcode.SMSG_SUMMON_CANCEL, 0x26B4 }, // NYI
            { Opcode.SMSG_SUMMON_RAID_MEMBER_VALIDATE_FAILED, 0x258E }, // NYI
            { Opcode.SMSG_SUMMON_REQUEST, 0x2725 },
            { Opcode.SMSG_SUPERCEDED_SPELLS, 0x2C4B },
            { Opcode.SMSG_SUSPEND_COMMS, 0x304A },
            { Opcode.SMSG_SUSPEND_TOKEN, 0x25AA },
            { Opcode.SMSG_SYNC_WOW_ENTITLEMENTS, 0x286D }, // NYI
            { Opcode.SMSG_TALENTS_INVOLUNTARILY_RESET, 0x271B }, // NYI
            { Opcode.SMSG_TALENT_GROUP_ROLE_CHANGED, 0x258D }, // NYI
            { Opcode.SMSG_TAXI_NODE_STATUS, 0x2680 },
            { Opcode.SMSG_TEXT_EMOTE, 0x2681 },
            { Opcode.SMSG_THREAT_CLEAR, 0x26E0 },
            { Opcode.SMSG_THREAT_REMOVE, 0x26DF },
            { Opcode.SMSG_THREAT_UPDATE, 0x26DE },
            { Opcode.SMSG_TIME_ADJUSTMENT, 0x2DD3 }, // NYI
            { Opcode.SMSG_TIME_SYNC_REQUEST, 0x2DD2 },
            { Opcode.SMSG_TITLE_EARNED, 0x26DB },
            { Opcode.SMSG_TITLE_LOST, 0x26DC },
            { Opcode.SMSG_TOTEM_CREATED, 0x26CC },
            { Opcode.SMSG_TOTEM_MOVED, 0x26CE },
            { Opcode.SMSG_TRADE_STATUS, 0x2583 },
            { Opcode.SMSG_TRADE_UPDATED, 0x2582 },
            { Opcode.SMSG_TRAINER_BUY_FAILED, 0x26E4 },
            { Opcode.SMSG_TRAINER_LIST, 0x26E6 },
            { Opcode.SMSG_TRAIT_CONFIG_COMMIT_FAILED, 0x25D3 },
            { Opcode.SMSG_TRANSFER_ABORTED, 0x2707 },
            { Opcode.SMSG_TRANSFER_PENDING, 0x25CF },
            { Opcode.SMSG_TREASURE_PICKER_RESPONSE, 0x291F }, // NYI
            { Opcode.SMSG_TRIGGER_CINEMATIC, 0x27CD },
            { Opcode.SMSG_TRIGGER_MOVIE, 0x26CF },
            { Opcode.SMSG_TURN_IN_PETITION_RESULT, 0x2752 },
            { Opcode.SMSG_TUTORIAL_FLAGS, 0x27C1 },
            { Opcode.SMSG_UNDELETE_CHARACTER_RESPONSE, 0x27CF },
            { Opcode.SMSG_UNDELETE_COOLDOWN_STATUS_RESPONSE, 0x27D1 },
            { Opcode.SMSG_UNLEARNED_SPELLS, 0x2C4D },
            { Opcode.SMSG_UNLOAD_CHILD_MAP, 0x257A }, // NYI
            { Opcode.SMSG_UPDATE_AADC_STATUS_RESPONSE, 0x2888 },
            { Opcode.SMSG_UPDATE_ACCOUNT_DATA, 0x270D },
            { Opcode.SMSG_UPDATE_ACTION_BUTTONS, 0x25E2 },
            { Opcode.SMSG_UPDATE_BNET_SESSION_KEY, 0x2827 },
            { Opcode.SMSG_UPDATE_CELESTIAL_BODY, 0x2823 }, // NYI
            { Opcode.SMSG_UPDATE_CHARACTER_FLAGS, 0x27C7 }, // NYI
            { Opcode.SMSG_UPDATE_CHARGE_CATEGORY_COOLDOWN, 0x276E }, // NYI
            { Opcode.SMSG_UPDATE_COOLDOWN, 0x276D }, // NYI
            { Opcode.SMSG_UPDATE_EXPANSION_LEVEL, 0x2648 }, // NYI
            { Opcode.SMSG_UPDATE_GAME_TIME_STATE, 0x282A }, // NYI
            { Opcode.SMSG_UPDATE_INSTANCE_OWNERSHIP, 0x26AD },
            { Opcode.SMSG_UPDATE_LAST_INSTANCE, 0x268C },
            { Opcode.SMSG_UPDATE_OBJECT, 0x27D0 },
            { Opcode.SMSG_UPDATE_PRIMARY_SPEC, 0x25DA }, // NYI
            //{ Opcode.SMSG_UPDATE_TALENT_DATA, 0x25D8 },
            { Opcode.SMSG_UPDATE_TALENT_DATA, 0x25D9 }, // RETAIL
            { Opcode.SMSG_UPDATE_WORLD_STATE, 0x274C },
            { Opcode.SMSG_USERLIST_ADD, 0x2BB9 },
            { Opcode.SMSG_USERLIST_REMOVE, 0x2BBA },
            { Opcode.SMSG_USERLIST_UPDATE, 0x2BBB },
            { Opcode.SMSG_USE_EQUIPMENT_SET_RESULT, 0x2753 },
            { Opcode.SMSG_VAS_CHECK_TRANSFER_OK_RESPONSE, 0x281E },
            { Opcode.SMSG_VAS_GET_QUEUE_MINUTES_RESPONSE, 0x281C },
            { Opcode.SMSG_VAS_GET_SERVICE_STATUS_RESPONSE, 0x281B },
            { Opcode.SMSG_VAS_PURCHASE_COMPLETE, 0x27F6 },
            { Opcode.SMSG_VAS_PURCHASE_STATE_UPDATE, 0x27F5 },
            { Opcode.SMSG_VENDOR_INVENTORY, 0x25BA },
            { Opcode.SMSG_VIGNETTE_UPDATE, 0x3008 },
            { Opcode.SMSG_VOICE_CHANNEL_INFO_RESPONSE, 0x2822 }, // NYI
            { Opcode.SMSG_VOICE_CHANNEL_STT_TOKEN_RESPONSE, 0x2884 }, // NYI
            { Opcode.SMSG_VOICE_LOGIN_RESPONSE, 0x2821 }, // NYI
            { Opcode.SMSG_VOID_ITEM_SWAP_RESPONSE, 0x2DA4 },
            { Opcode.SMSG_VOID_STORAGE_CONTENTS, 0x2DA1 },
            { Opcode.SMSG_VOID_STORAGE_FAILED, 0x2DA0 },
            { Opcode.SMSG_VOID_STORAGE_TRANSFER_CHANGES, 0x2DA2 },
            { Opcode.SMSG_VOID_TRANSFER_RESULT, 0x2DA3 },
            { Opcode.SMSG_WAIT_QUEUE_FINISH, 0x256F },
            { Opcode.SMSG_WAIT_QUEUE_UPDATE, 0x256E },
            { Opcode.SMSG_WARDEN3_DATA, 0x2577 },
            { Opcode.SMSG_WARDEN3_DISABLED, 0x2825 }, // NYI
            { Opcode.SMSG_WARDEN3_ENABLED, 0x2824 },
            { Opcode.SMSG_WARFRONT_COMPLETE, 0x2762 }, // NYI
            { Opcode.SMSG_WARGAME_REQUEST_OPPONENT_RESPONSE, 0x2937 }, // NYI
            { Opcode.SMSG_WARGAME_REQUEST_SUCCESSFULLY_SENT_TO_OPPONENT, 0x2935 }, // NYI
            { Opcode.SMSG_WEATHER, 0x26AA },
            { Opcode.SMSG_WHO, 0x2BAE },
            { Opcode.SMSG_WHO_IS, 0x26A9 },
            { Opcode.SMSG_WILL_BE_KICKED_FOR_ADDED_SUBSCRIPTION_TIME, 0x2829 }, // NYI
            { Opcode.SMSG_WORLD_QUEST_UPDATE_RESPONSE, 0x300F },
            { Opcode.SMSG_WORLD_SERVER_INFO, 0x25AF },
            { Opcode.SMSG_WOW_ENTITLEMENT_NOTIFICATION, 0x286E }, // NYI
            { Opcode.SMSG_WOW_LABS_NOTIFY_PLAYERS_MATCH_END, 0x2896 }, // NYI
            { Opcode.SMSG_WOW_LABS_NOTIFY_PLAYERS_MATCH_STATE_CHANGED, 0x2897 }, // NYI
            { Opcode.SMSG_WOW_LABS_PARTY_ERROR, 0x289E }, // NYI
            { Opcode.SMSG_WOW_LABS_SET_PREDICTION_CIRCLE, 0x2898 }, // NYI
            { Opcode.SMSG_XP_GAIN_ABORTED, 0x25CB }, // NYI
            { Opcode.SMSG_XP_GAIN_ENABLED, 0x27B1 }, // NYI
            { Opcode.SMSG_ZONE_UNDER_ATTACK, 0x2BB5 },
        };

        private static readonly BiDictionary<Opcode, int> MiscOpcodes = new();
    }
}

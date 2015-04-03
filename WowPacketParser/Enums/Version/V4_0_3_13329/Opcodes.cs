using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V4_0_3_13329
{
    public static class Opcodes_4_0_3
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
            {Opcode.CMSG_ACCEPT_LEVEL_GRANT, 0x0420}, // NF()(0x0420) //
            {Opcode.CMSG_ACCEPT_TRADE, 0x011A}, // NF()(0x011A) //
            {Opcode.CMSG_ACTIVATE_TAXI, 0x01AD}, // NF()(0x01AD) //
            {Opcode.CMSG_ACTIVATE_TAXI_EXPRESS, 0x0312}, // NF()(0x0312) //
            {Opcode.CMSG_ACTIVE_PVP_CHEAT, 0x0399}, // NF()(0x0399) //
            {Opcode.CMSG_ADD_FRIEND, 0x6E5F}, // (0xCAB1)(0x0069) //
            {Opcode.CMSG_ADD_IGNORE, 0x8D57}, // (0xCAF5)(0x006C) //
            {Opcode.CMSG_ADD_PVP_MEDAL_CHEAT, 0x0289}, // NF()(0x0289) //
            {Opcode.CMSG_VOICE_ADD_IGNORE, 0x03DB}, // NF()(0x03DB) //
            {Opcode.CMSG_ALTER_APPEARANCE, 0x0426}, // NF()(0x0426) //
            {Opcode.CMSG_AREA_TRIGGER, 0xBD5E}, // (0xAAB4)(0x00B4) //
            {Opcode.CMSG_AREA_SPIRIT_HEALER_QUERY, 0x02E2}, // NF()(0x02E2) //
            {Opcode.CMSG_AREA_SPIRIT_HEALER_QUEUE, 0x02E3}, // NF()(0x02E3) //
            {Opcode.CMSG_ARENA_TEAM_ACCEPT, 0x447C}, // (0xC379)(0x0351) //
            {Opcode.CMSG_ARENA_TEAM_CREATE, 0x0348}, // NF()(0x0348) //
            {Opcode.CMSG_ARENA_TEAM_DECLINE, 0x8C57}, // (0xA250)(0x0352) //
            {Opcode.CMSG_ARENA_TEAM_DISBAND, 0x867C}, // (0x6AD4)(0x0355) //
            {Opcode.CMSG_ARENA_TEAM_INVITE, 0xEF7D}, // (0xAAB0)(0x034F) //
            {Opcode.CMSG_ARENA_TEAM_LEADER, 0x9777}, // (0x0B95)(0x0356) //
            {Opcode.CMSG_ARENA_TEAM_LEAVE, 0x4556}, // (0x6B54)(0x0353) //
            {Opcode.CMSG_ARENA_TEAM_QUERY, 0xFFB7}, // NF(0x023D)(0x034B) //
            {Opcode.CMSG_ARENA_TEAM_REMOVE, 0x9C57}, // (0x429D)(0x0354) //
            {Opcode.CMSG_ARENA_TEAM_ROSTER, 0x034D}, // NF()(0x034D)
            {Opcode.CMSG_ATTACK_STOP, 0x1E7C}, // (0xC3B5)(0x0142) //
            {Opcode.CMSG_ATTACK_SWING, 0xE65D}, // (0x4A5C)(0x0141) //
            {Opcode.CMSG_AUCTION_LIST_BIDDER_ITEMS, 0x0264}, // NF()(0x0264) //
            {Opcode.CMSG_AUCTION_LIST_ITEMS, 0x0258}, // NF()(0x0258) //
            {Opcode.CMSG_AUCTION_LIST_OWNER_ITEMS, 0x0259}, // NF()(0x0259) //
            {Opcode.CMSG_AUCTION_LIST_PENDING_SALES, 0x048F}, // NF()(0x048F) //
            {Opcode.CMSG_AUCTION_PLACE_BID, 0x025A}, // NF()(0x025A) //
            {Opcode.CMSG_AUCTION_REMOVE_ITEM, 0x0257}, // NF()(0x0257) //
            {Opcode.CMSG_AUCTION_SELL_ITEM, 0x0256}, // NF()(0x0256) //
            {Opcode.CMSG_AUTH_CONTINUED_SESSION, 0x0512}, // NF()(0x0512) // // something with networking
            {Opcode.CMSG_AUTH_SESSION, 0x880A}, // (0x3000)(0x01ED) //
            {Opcode.CMSG_AUTOBANK_ITEM, 0x2D55}, // (0x4A18)(0x0283) //
            {Opcode.CMSG_AUTO_EQUIP_GROUND_ITEM, 0x0106}, // NF()(0x0106) //
            {Opcode.CMSG_AUTO_EQUIP_ITEM, 0x8756}, // (0x0391)(0x010A) //
            {Opcode.CMSG_AUTO_EQUIP_ITEM_SLOT, 0x010F}, // NF()(0x010F) //
            {Opcode.CMSG_AUTOSTORE_BAG_ITEM, 0x010B}, // NF()(0x010B) //
            {Opcode.CMSG_AUTOSTORE_BANK_ITEM, 0x1556}, // (0xA2D4)(0x0282) //
            {Opcode.CMSG_AUTOSTORE_GROUND_ITEM, 0x0107}, // NF()(0x0107) //
            {Opcode.CMSG_AUTOSTORE_LOOT_ITEM, 0xF457}, // (0x22DD)(0x0108) //
            {Opcode.CMSG_BANKER_ACTIVATE, 0x01B7}, // NF()(0x01B7) //
            {Opcode.CMSG_BATTLEFIELD_JOIN, 0x023E}, // NF()(0x023E) //
            {Opcode.CMSG_BATTLEFIELD_LIST, 0xFFC3}, // NF(0x1F73)(0x023C) //
            //{Opcode.CMSG_BF_MGR_ENTRY_INVITE_RESPONSE, 0x0100}, // (0x1E11)(0x04DF) //lefieldMgrEntryInviteResponse
            {Opcode.CMSG_BF_MGR_QUEUE_EXIT_REQUEST, 0x0581}, // (0x1601)(0x04E7) //lefieldMgrExitRequest
            {Opcode.CMSG_BF_MGR_QUEUE_INVITE_RESPONSE, 0x05A0}, // (0x5A11)(0x04E2) //lefieldMgrQueueInviteResponse
            {Opcode.CMSG_BF_MGR_QUEUE_REQUEST, 0xFF95}, // NF(0x1B62)(0x04E3) //lefieldMgrQueueRequest
            {Opcode.CMSG_BATTLEFIELD_STATUS, 0x02D3}, // NF()(0x02D3) //
            {Opcode.CMSG_BATTLEGROUND_PORT_AND_LEAVE, 0xFFBD}, // NF(0x1373)(0x02D5) //
            {Opcode.CMSG_BATTLEMASTER_HELLO, 0x02D7}, // NF()(0x02D7) //
            {Opcode.CMSG_BATTLEMASTER_JOIN, 0xFFBA}, // NF(0x8F73)(0x02EE) //
            {Opcode.CMSG_BATTLEMASTER_JOIN_ARENA, 0xFFB5}, // NF(0x8362)(0x0358) //
            {Opcode.CMSG_BATTLEMASTER_JOIN_RATED, 0xFF8E}, // NF13297(0x8363)() // new op ?
            {Opcode.CMSG_BEGIN_TRADE, 0xFFD7}, // NF(0x1773)(0x0117) //
            {Opcode.CMSG_BINDER_ACTIVATE, 0x1D7F}, // (0xE39C)(0x01B5) //
            {Opcode.CMSG_BOT_DETECTED, 0x03C0}, // NF()(0x03C0) //
            {Opcode.CMSG_BOT_DETECTED2, 0xFFFD}, // NF(0x63B0)(0x0017) //
            {Opcode.CMSG_BUG, 0xFFD1}, // NF(0x2B3C)(0x01CA) //
            {Opcode.CMSG_BUSY_TRADE, 0x0118}, // NF()(0x0118) //
            {Opcode.CMSG_BUY_BACK_ITEM, 0x0290}, // NF()(0x0290) //
            {Opcode.CMSG_BUY_BANK_SLOT, 0x01B9}, // NF()(0x01B9) //
            //{Opcode.CMSG_BUY_ITEM, 0x8457}, // (0xCB5C)(0x01A3) // INT INT INT GUID BYTE
            {Opcode.CMSG_BUY_LOTTERY_TICKET_OBSOLETE, 0x0336}, // NF()(0x0336) //
            {Opcode.CMSG_BUY_STABLE_SLOT, 0x0272}, // NF()(0x0272) //
            {Opcode.CMSG_CALENDAR_ADD_EVENT, 0x042D}, // NF()(0x042D) //
            {Opcode.CMSG_CALENDAR_ARENA_TEAM, 0x042C}, // NF()(0x042C) //
            {Opcode.CMSG_CALENDAR_COMPLAIN, 0x0446}, // NF()(0x0446) //
            {Opcode.CMSG_CALENDAR_CONTEXT_EVENT_SIGNUP, 0x04BA}, // NF()(0x04BA) // // CMSG}, uint64}, lua: CalendarContextEventSignUp
            {Opcode.CMSG_CALENDAR_COPY_EVENT, 0x0430}, // NF()(0x0430) //
            {Opcode.CMSG_CALENDAR_EVENT_INVITE, 0x0431}, // NF()(0x0431) //
            {Opcode.CMSG_CALENDAR_EVENT_MODERATOR_STATUS, 0x0435}, // NF()(0x0435) //
            {Opcode.CMSG_CALENDAR_EVENT_REMOVE_INVITE, 0x0433}, // NF()(0x0433) //
            {Opcode.CMSG_CALENDAR_EVENT_RSVP, 0x0432}, // NF()(0x0432) //
            {Opcode.CMSG_CALENDAR_EVENT_STATUS, 0x0434}, // NF()(0x0434) //
            {Opcode.CMSG_CALENDAR_GET_CALENDAR, 0x0429}, // NF()(0x0429) //
            {Opcode.CMSG_CALENDAR_GET_EVENT, 0x042A}, // NF()(0x042A) //
            {Opcode.CMSG_CALENDAR_GET_NUM_PENDING, 0x0447}, // NF()(0x0447) //
            {Opcode.CMSG_CALENDAR_GUILD_FILTER, 0xFFA2}, // NF(0xF000)(0x042B) //
            {Opcode.CMSG_CALENDAR_REMOVE_EVENT, 0x042F}, // NF()(0x042F) //
            {Opcode.CMSG_CALENDAR_UPDATE_EVENT, 0x042E}, // NF()(0x042E) //
            {Opcode.CMSG_CANCEL_AURA, 0x545E}, // (0xAB7C)(0x0136) //
            {Opcode.CMSG_CANCEL_AUTO_REPEAT_SPELL, 0xA45E}, // (0xAB39)(0x026D) //
            {Opcode.CMSG_CANCEL_CAST, 0xFD77}, // (0xEB5D)(0x012F) //
            {Opcode.CMSG_CANCEL_CHANNELLING, 0x957C}, // (0x6A3D)(0x013B) //
            {Opcode.CMSG_CANCEL_GROWTH_AURA, 0x029B}, // NF()(0x029B) //
            {Opcode.CMSG_CANCEL_MOUNT_AURA, 0x0375}, // NF()(0x0375) //
            {Opcode.CMSG_CANCEL_TEMP_ENCHANTMENT, 0x0379}, // NF()(0x0379) //
            {Opcode.CMSG_CANCEL_TRADE, 0x0C2A}, // (0x1F72)(0x011C) //
            {Opcode.CMSG_CAST_SPELL, 0x4C56}, // (0xC390)(0x012E) //
            {Opcode.CMSG_CHANGE_PERSONAL_ARENA_RATING, 0x0425}, // NF()(0x0425) //
            {Opcode.CMSG_CHANGE_SEATS_ON_CONTROLLED_VEHICLE, 0x049B}, // NF()(0x049B) //
            {Opcode.CMSG_CHAT_CHANNEL_ANNOUNCEMENTS, 0x2A10}, // (0x9224)(0x00A7) //
            {Opcode.CMSG_CHAT_CHANNEL_BAN, 0x6A10}, // (0x3A20)(0x00A5) //
            {Opcode.CMSG_CHAT_CHANNEL_DECLINE_INVITE, 0xFFA5}, // NF(0x0290)(0x0410) //
            {Opcode.CMSG_CHAT_CHANNEL_DISPLAY_LIST, 0x03D2}, // NF()(0x03D2) //
            {Opcode.CMSG_CHAT_CHANNEL_INVITE, 0x00A3}, // NF()(0x00A3) //
            {Opcode.CMSG_CHAT_CHANNEL_KICK, 0x6A98}, // (0x9200)(0x00A4) //
            {Opcode.CMSG_CHAT_CHANNEL_LIST, 0x1A88}, // (0x1220)(0x009A) //
            {Opcode.CMSG_CHAT_CHANNEL_MODERATE, 0x00A8}, // NF()(0x00A8) //
            {Opcode.CMSG_CHAT_CHANNEL_MODERATOR, 0x7A08}, // (0xF200)(0x009F) //
            //{Opcode.CMSG_CHAT_CHANNEL_MUTE, 0x4A90}, // (0x7220)(0x00A1) //
            {Opcode.CMSG_CHAT_CHANNEL_OWNER, 0x2A98}, // (0xB220)(0x009E) //
            {Opcode.CMSG_CHAT_CHANNEL_PASSWORD, 0x3A18}, // (0xB224)(0x009C) //
            //{Opcode.CMSG_CHAT_CHANNEL_ROSTER_INFO, 0x3A90}, // (0x5A24)(0x0000) //
            //{Opcode.CMSG_CHAT_CHANNEL_SET_OWNER, 0x6A88}, // (0x3A24)(0x009D) //
            {Opcode.CMSG_CHAT_CHANNEL_SILENCE_ALL, 0x1A90}, // (0xBA00)(0x03CD) //
            {Opcode.CMSG_CHAT_CHANNEL_SILENCE_VOICE, 0x7A18}, // (0xFA04)(0x03CC) //
            {Opcode.CMSG_CHAT_CHANNEL_UNBAN, 0x0A18}, // (0x9A04)(0x00A6) //
            {Opcode.CMSG_CHAT_CHANNEL_UNMODERATOR, 0x2A00}, // (0x1A20)(0x00A0) //
            {Opcode.CMSG_CHAT_CHANNEL_UNMUTE, 0x4A18}, // (0x3200)(0x00A2) //
            {Opcode.CMSG_CHAT_CHANNEL_UNSILENCE_ALL, 0x3A80}, // (0xF220)(0x03CF) //
            {Opcode.CMSG_CHAT_CHANNEL_UNSILENCE_VOICE, 0x6A80}, // (0xBA20)(0x03CE) //
            {Opcode.CMSG_CHAT_CHANNEL_VOICE_OFF, 0x7A88}, // (0x5220)(0x03D7) //
            {Opcode.CMSG_CHAT_CHANNEL_VOICE_ON, 0x5A98}, // (0xB204)(0x03D6) //
            {Opcode.CMSG_CHARACTER_POINT_CHEAT, 0x0223}, // NF()(0x0223) //
            {Opcode.CMSG_CREATE_CHARACTER, 0xF47E}, // (0x2BF0)(0x0036) //
            {Opcode.CMSG_CHAR_CUSTOMIZE, 0x0473}, // NF()(0x0473) //
            {Opcode.CMSG_CHAR_DELETE, 0xAD5E}, // (0x8A78)(0x0038) //
            {Opcode.CMSG_ENUM_CHARACTERS, 0x6655}, // (0x03F8)(0x0037) //
            {Opcode.CMSG_CHAR_FACTION_CHANGE, 0x4F75}, // NF()(0x04D9) // // lua: CreateCharacter (PFC client response)
            {Opcode.CMSG_CHAR_RACE_CHANGE, 0x04F8}, // NF()(0x04F8) // // called from lua: CreateCharacter}, paid race change
            {Opcode.CMSG_CHARACTER_RENAME_REQUEST, 0x02C7}, // NF()(0x02C7) //
            {Opcode.CMSG_CHEAT_DUMP_ITEMS_DEBUG_ONLY, 0x039A}, // NF()(0x039A) //
            {Opcode.CMSG_CHEAT_PLAYER_LOGIN, 0x03C2}, // NF()(0x03C2) //
            {Opcode.CMSG_CHEAT_PLAYER_LOOKUP, 0x03C3}, // NF()(0x03C3) //
            {Opcode.CMSG_CHEAT_SET_ARENA_CURRENCY, 0x037C}, // NF()(0x037C) //
            {Opcode.CMSG_CHEAT_SET_HONOR_CURRENCY, 0x037B}, // NF()(0x037B) //
            {Opcode.CMSG_CHECK_LOGIN_CRITERIA, 0x04A2}, // NF()(0x04A2) // // not found
            {Opcode.CMSG_CLEAR_CHANNEL_WATCH, 0x03F3}, // NF()(0x03F3) //
            {Opcode.CMSG_CLEAR_EXPLORATION, 0x0237}, // NF()(0x0237) //
            {Opcode.CMSG_CLEAR_SERVER_BUCK_DATA, 0x041C}, // NF()(0x041C) //
            {Opcode.CMSG_CLEAR_TRADE_ITEM, 0x011E}, // NF()(0x011E) //
            {Opcode.CMSG_COMMENTATOR_ENABLE, 0x03B5}, // NF()(0x03B5) //
            {Opcode.CMSG_COMMENTATOR_ENTER_INSTANCE, 0x03BC}, // NF()(0x03BC) //
            {Opcode.CMSG_COMMENTATOR_EXIT_INSTANCE, 0x03BD}, // NF()(0x03BD) //
            {Opcode.CMSG_COMMENTATOR_GET_MAP_INFO, 0x03B7}, // NF()(0x03B7) //
            {Opcode.CMSG_COMMENTATOR_GET_PLAYER_INFO, 0x03B9}, // NF()(0x03B9) //
            {Opcode.CMSG_COMMENTATOR_INSTANCE_COMMAND, 0x03BE}, // NF()(0x03BE) //
            {Opcode.CMSG_COMMENTATOR_SKIRMISH_QUEUE_COMMAND, 0x051B}, // NF()(0x051B) // // Lua_CommentatorSetSkirmishMatchmakingMode and Lua_CommentatorRequestSkirmishQueueData
            {Opcode.CMSG_COMPLAINT, 0x03C7}, // NF()(0x03C7) //
            {Opcode.CMSG_COMPLETE_ACHIEVEMENT_CHEAT, 0x046E}, // NF()(0x046E) //
            {Opcode.CMSG_COMPLETE_CINEMATIC, 0x00FC}, // NF()(0x00FC) //
            {Opcode.CMSG_COMPLETE_MOVIE, 0x0465}, // NF()(0x0465) //
            {Opcode.CMSG_CONNECT_TO_FAILED, 0x0509}, // (0x1201)(0x050E) // with networking
            {Opcode.CMSG_CONTACT_LIST, 0xCD5D}, // (0x63D4)(0x0066) //
            {Opcode.CMSG_CORPSE_MAP_POSITION_QUERY, 0x04B6}, // NF()(0x04B6) // // CMSG}, uint32
            {Opcode.CMSG_QUERY_CREATURE, 0x8454}, // (0xE3D5)(0x0060) //
            {Opcode.CMSG_DANCE_QUERY, 0xFFA0}, // NF(0xAB1D)(0x0451) //
            {Opcode.CMSG_DEBUG_ACTIONS_START, 0x0315}, // NF()(0x0315) //
            {Opcode.CMSG_DEBUG_ACTIONS_STOP, 0x0316}, // NF()(0x0316) //
            {Opcode.CMSG_DEBUG_LIST_TARGETS, 0x03D8}, // NF()(0x03D8) //
            {Opcode.CMSG_DECHARGE, 0x0204}, // NF()(0x0204) //
            {Opcode.CMSG_DELETE_DANCE, 0x0454}, // NF()(0x0454) //
            {Opcode.CMSG_DEL_FRIEND, 0x1D5E}, // (0x0B10)(0x006A) //
            {Opcode.CMSG_DEL_IGNORE, 0xD57C}, // (0xC399)(0x006D) //
            {Opcode.CMSG_DEL_PVP_MEDAL_CHEAT, 0x028A}, // NF()(0x028A) //
            {Opcode.CMSG_VOICE_DEL_IGNORE, 0x03DC}, // NF()(0x03DC) //
            {Opcode.CMSG_DESTROY_ITEM, 0x0111}, // NF()(0x0111) //
            {Opcode.CMSG_DESTROY_ITEMS, 0x00B2}, // NF()(0x00B2) //
            {Opcode.CMSG_DISMISS_CONTROLLED_VEHICLE, 0x046D}, // NF()(0x046D) //
            {Opcode.CMSG_DISMISS_CRITTER, 0x048D}, // NF()(0x048D) //
            {Opcode.CMSG_DUEL_ACCEPTED, 0x6F75}, // (0x8295)(0x016C) //
            {Opcode.CMSG_DUEL_CANCELLED, 0xCC7E}, // (0x6BDC)(0x016D) //
            {Opcode.CMSG_DUMP_OBJECTS, 0x048B}, // NF()(0x048B) //
            {Opcode.CMSG_EJECT_PASSENGER, 0x04A9}, // NF()(0x04A9) // // cmsg uint64
            {Opcode.CMSG_EMOTE, 0x7F5C}, // (0x4A50)(0x0102) //
            {Opcode.CMSG_ENABLE_DAMAGE_LOG, 0x027D}, // NF()(0x027D) //
            {Opcode.CMSG_ENABLE_NAGLE, 0x0510}, // NF()(0x0510) // // something with networking
            {Opcode.CMSG_ENABLE_TAXI_NODE, 0x0493}, // NF()(0x0493) //
            {Opcode.CMSG_EQUIPMENT_SET_DELETE, 0xEE7D}, // (0x8BD8)(0x013E) //
            {Opcode.CMSG_EQUIPMENT_SET_SAVE, 0x04BD}, // NF()(0x04BD) // // CMSG}, lua: SaveEquipmentSet
            {Opcode.CMSG_EQUIPMENT_SET_USE, 0x04D5}, // NF()(0x04D5) // // CMSG}, lua: UseEquipmentSet
            {Opcode.CMSG_EXPIRE_RAID_INSTANCE, 0x0415}, // NF()(0x0415) //
            {Opcode.CMSG_FAR_SIGHT, 0x027A}, // NF()(0x027A) //
            {Opcode.CMSG_FLOOD_GRACE_CHEAT, 0x0497}, // NF()(0x0497) //
            {Opcode.CMSG_FORCE_FLIGHT_BACK_SPEED_CHANGE_ACK, 0x0384}, // NF()(0x0384) //
            {Opcode.CMSG_FORCE_FLIGHT_SPEED_CHANGE_ACK, 0x0382}, // NF()(0x0382) //
            {Opcode.CMSG_FORCE_MOVE_ROOT_ACK, 0x00E9}, // NF()(0x00E9) //
            {Opcode.CMSG_FORCE_MOVE_UNROOT_ACK, 0x00EB}, // NF()(0x00EB) //
            {Opcode.CMSG_FORCE_PITCH_RATE_CHANGE_ACK, 0x045D}, // NF()(0x045D) //
            {Opcode.CMSG_FORCE_RUN_BACK_SPEED_CHANGE_ACK, 0x00E5}, // NF()(0x00E5) //
            {Opcode.CMSG_FORCE_RUN_SPEED_CHANGE_ACK, 0x00E3}, // NF()(0x00E3) //
            //{Opcode.CMSG_FORCE_SAY_CHEAT, 0x047E}, // NF()(0x047E) //
            {Opcode.CMSG_FORCE_SWIM_BACK_SPEED_CHANGE_ACK, 0x02DD}, // NF()(0x02DD) //
            {Opcode.CMSG_FORCE_SWIM_SPEED_CHANGE_ACK, 0x00E7}, // NF()(0x00E7) //
            {Opcode.CMSG_FORCE_TURN_RATE_CHANGE_ACK, 0x02DF}, // NF()(0x02DF) //
            {Opcode.CMSG_FORCE_WALK_SPEED_CHANGE_ACK, 0x02DB}, // NF()(0x02DB) //
            {Opcode.CMSG_QUERY_GAME_OBJECT, 0x0455}, // (0x8239)(0x005E) //
            {Opcode.CMSG_GAME_OBJ_REPORT_USE, 0x0481}, // NF()(0x0481) //
            {Opcode.CMSG_GAME_OBJ_USE, 0x00B1}, // NF()(0x00B1) //
            {Opcode.CMSG_GAMESPEED_SET, 0x0046}, // NF()(0x0046) // NOT IN CLIENT 401
            {Opcode.CMSG_GAMETIME_SET, 0x0044}, // NF()(0x0044) // NOT IN CLIENT 335 AND 401
            {Opcode.CMSG_GETDEATHBINDZONE, 0x0156}, // NF()(0x0156) // NOT IN CLIENT 335 AND 401
            //{Opcode.CMSG_GET_CHANNEL_MEMBER_COUNT, 0x3A90}, // (0x5A24)(0x03D4) //
            {Opcode.CMSG_GET_ITEM_PURCHASE_DATA, 0x04B3}, // NF()(0x04B3) // // refund request?
            {Opcode.CMSG_MAIL_GET_LIST, 0x023A}, // NF()(0x023A) //
            {Opcode.CMSG_GET_MIRROR_IMAGE_DATA, 0x0401}, // NF()(0x0401) //
            {Opcode.CMSG_GHOST, 0x01E5}, // NF()(0x01E5) //
            {Opcode.CMSG_GM_TICKET_RESPONSE_RESOLVE, 0x04F0}, // NF()(0x04F0) // // lua: GMResponseResolve
            {Opcode.CMSG_GM_SURVEY_SUBMIT, 0x032A}, // NF()(0x032A) //
            {Opcode.CMSG_GMTICKETSYSTEM_TOGGLE, 0x029A}, // NF()(0x029A) //
            {Opcode.CMSG_GM_TICKET_CREATE, 0x0205}, // NF()(0x0205) //
            {Opcode.CMSG_GM_TICKET_DELETE_TICKET, 0x0217}, // NF()(0x0217) //
            {Opcode.CMSG_GM_TICKET_GET_TICKET, 0x0211}, // NF()(0x0211) //
            {Opcode.CMSG_GM_TICKET_GET_SYSTEM_STATUS, 0x021A}, // NF()(0x021A) //
            {Opcode.CMSG_GM_TICKET_UPDATE_TEXT, 0x0207}, // NF()(0x0207) //
            {Opcode.CMSG_GM_CHARACTER_RESTORE, 0x03FA}, // NF()(0x03FA) //
            {Opcode.CMSG_GM_CHARACTER_SAVE, 0x03FB}, // NF()(0x03FB) //
            {Opcode.CMSG_GM_CREATE_ITEM_TARGET, 0x0210}, // NF()(0x0210) //
            {Opcode.CMSG_GM_DESTROY_ONLINE_CORPSE, 0x0311}, // NF()(0x0311) //
            {Opcode.CMSG_GM_INVIS, 0x01E6}, // NF()(0x01E6) //
            {Opcode.CMSG_GM_LAG_REPORT, 0x0502}, // NF()(0x0502) // // lua: GMReportLag
            {Opcode.CMSG_GM_NUKE, 0x01FA}, // NF()(0x01FA) //
            {Opcode.CMSG_GM_NUKE_ACCOUNT, 0x030F}, // NF()(0x030F) //
            {Opcode.CMSG_GM_SET_SECURITY_GROUP, 0x01F9}, // NF()(0x01F9) //
            {Opcode.CMSG_GM_SHOW_COMPLAINTS, 0x03CA}, // NF()(0x03CA) //
            {Opcode.CMSG_GM_TEACH, 0x020F}, // NF()(0x020F) //
            {Opcode.CMSG_GM_UNSQUELCH, 0x03CB}, // NF()(0x03CB) //
            {Opcode.CMSG_GM_UNTEACH, 0x02E5}, // NF()(0x02E5) //
            {Opcode.CMSG_GM_UPDATE_TICKET_STATUS, 0x0327}, // NF()(0x0327) //
            {Opcode.CMSG_GM_WHISPER, 0xDA24}, // NF(0xDA24)()
            {Opcode.CMSG_GOSSIP_HELLO, 0x1D55}, // (0x2A75)(0x017B) //
            //{Opcode.CMSG_GOSSIP_SELECT_OPTION, 0x8E7C}, // (0x0271)(0x017C) //
            {Opcode.CMSG_GRANT_LEVEL, 0x877C}, // (0xE251)(0x040D) //
            {Opcode.CMSG_GROUP_ACCEPT, 0x9C74}, // (0x8299)(0x0072) //
            {Opcode.CMSG_GROUP_CANCEL, 0x0070}, // NF()(0x0070) //
            {Opcode.CMSG_GROUP_CHANGE_SUB_GROUP, 0xE576}, // (0xEB35)(0x027E) //
            {Opcode.CMSG_GROUP_DECLINE, 0xA47C}, // (0xCADC)(0x0073) //
            {Opcode.CMSG_GROUP_DISBAND, 0x6575}, // (0x4271)(0x007B) //
            {Opcode.CMSG_GROUP_INVITE, 0x9E5E}, // (0x6AF4)(0x006E) //
            {Opcode.CMSG_GROUP_RAID_CONVERT, 0xA75D}, // (0x427C)(0x028E) //
            {Opcode.CMSG_GROUP_SET_LEADER, 0x7454}, // (0x83F5)(0x0078) //
            {Opcode.CMSG_GROUP_SWAP_SUB_GROUP, 0x677F}, // (0x6231)(0x0280) //
            //{Opcode.CMSG_GROUP_UNINVITE, 0x4F74}, // (0x0ABD)(0x0075) //
            //{Opcode.CMSG_GROUP_UNINVITE_GUID, 0x4F74}, // (0x0ABD)(0x0076) //
            {Opcode.CMSG_GUILD_ACCEPT, 0x0084}, // NF()(0x0084) //
            {Opcode.CMSG_GUILD_ADD_RANK, 0x0232}, // NF()(0x0232) //
            {Opcode.CMSG_GUILD_BANK_ACTIVATE, 0x03E6}, // NF()(0x03E6) //
            {Opcode.CMSG_GUILD_BANK_BUY_TAB, 0x03EA}, // NF()(0x03EA) //
            {Opcode.CMSG_GUILD_BANK_DEPOSIT_MONEY, 0x03EC}, // NF()(0x03EC) //
            {Opcode.CMSG_GUILD_BANK_QUERY_TAB, 0x03E7}, // NF()(0x03E7) //
            {Opcode.CMSG_GUILD_BANK_SET_TAB_TEXT, 0x040B}, // NF()(0x040B) //
            {Opcode.CMSG_GUILD_BANK_SWAP_ITEMS, 0x03E9}, // NF()(0x03E9) //
            {Opcode.CMSG_GUILD_BANK_UPDATE_TAB, 0x03EB}, // NF()(0x03EB) //
            {Opcode.CMSG_GUILD_BANK_WITHDRAW_MONEY, 0x03ED}, // NF()(0x03ED) //
            {Opcode.CMSG_GUILD_CREATE, 0x0081}, // NF()(0x0081) //
            {Opcode.CMSG_GUILD_DECLINE_INVITATION, 0x0085}, // NF()(0x0085) //
            {Opcode.CMSG_GUILD_DELETE_RANK, 0xFFC6}, // NF(0x4205)(0x0233) //
            {Opcode.CMSG_GUILD_DEMOTE_MEMBER, 0xFFE4}, // NF(0x4200)(0x008C) //
            {Opcode.CMSG_GUILD_DISBAND, 0x0982}, // (0x0205)(0x008F) //
            {Opcode.CMSG_GUILD_GET_ROSTER, 0x0757}, // (0x22FD)(0x0089) //
            {Opcode.CMSG_GUILD_INFO, 0xAF5E}, // (0x6B75)(0x0087) //
            {Opcode.CMSG_GUILD_INFO_TEXT, 0x02FC}, // NF()(0x02FC) //
            {Opcode.CMSG_GUILD_INVITE, 0x277C}, // (0xCAB9)(0x0082) //
            {Opcode.CMSG_GUILD_LEADER, 0xFFE1}, // NF(0x9900)(0x0090) //
            {Opcode.CMSG_GUILD_LEAVE, 0xFFE3}, // NF(0x0A80)(0x008D) //
            {Opcode.CMSG_GUILD_MOTD, 0xFFE0}, // NF(0x4A05)(0x0091) //
            {Opcode.CMSG_GUILD_OFFICER_REMOVE_MEMBER, 0xFFE2}, // NF(0x4281)(0x008E) //
            {Opcode.CMSG_GUILD_PROMOTE_MEMBER, 0xFFE5}, // NF(0x4A00)(0x008B) //
            {Opcode.CMSG_QUERY_GUILD_INFO, 0xFF88}, // NF(0x4B18)(0x0054) //
            {Opcode.CMSG_GUILD_SET_OFFICER_NOTE, 0x0235}, // NF()(0x0235) //
            {Opcode.CMSG_GUILD_SET_PUBLIC_NOTE, 0x0234}, // NF()(0x0234) //
            //{Opcode.CMSG_GUILD_SET_RANK_PERMISSIONS, 0xFFC7}, // NF(0x4A81)(0x0231) //
            {Opcode.CMSG_GUILD_SWITCH_RANK, 0xFFC5}, // NF(0x0A04)(0x0000) //si up}, 0 si down) uint32(rank) uint64 playerGUID
            {Opcode.CMSG_HEARTH_AND_RESURRECT, 0xD67E}, // (0x0A71)(0x049C) //
            {Opcode.CMSG_IGNORE_DIMINISHING_RETURNS_CHEAT, 0x0405}, // NF()(0x0405) //
            {Opcode.CMSG_IGNORE_KNOCKBACK_CHEAT, 0x032C}, // NF()(0x032C) //
            {Opcode.CMSG_IGNORE_REQUIREMENTS_CHEAT, 0x03A8}, // NF()(0x03A8) //
            {Opcode.CMSG_IGNORE_TRADE, 0x0119}, // NF()(0x0119) //
            {Opcode.CMSG_INITIATE_TRADE, 0xFFD8}, // NF(0x1772)(0x0116) //
            {Opcode.CMSG_INSPECT, 0xAF7F}, // (0x2334)(0x0114) //
            {Opcode.CMSG_INSTANCE_LOCK_WARNING_RESPONSE, 0xE476}, // (0x4A3C)(0x013F) //
            {Opcode.CMSG_ITEM_NAME_QUERY, 0x02C4}, // NF()(0x02C4) //
            {Opcode.CMSG_ITEM_PURCHASE_REFUND, 0x04B4}, // NF()(0x04B4) // // lua: ContainerRefundItemPurchase
            {Opcode.CMSG_ITEM_QUERY_MULTIPLE, 0x0057}, // NF()(0x0057) //
            {Opcode.CMSG_ITEM_QUERY_SINGLE, 0x0180}, // (0x5621)(0x0056) //
            {Opcode.CMSG_ITEM_TEXT_QUERY, 0xFFC2}, // NF(0x237D)(0x0243) //
            //{Opcode.CMSG_CHAT_JOIN_CHANNEL, 0x3A98}, // (0x9A00)(0x0097) //
            {Opcode.CMSG_KEEP_ALIVE, 0x0407}, // NF()(0x0407) //
            {Opcode.CMSG_LEARN_DANCE_MOVE, 0x0456}, // NF()(0x0456) //
            {Opcode.CMSG_LEARN_PREVIEW_TALENTS, 0x4754}, // (0xC3B1)(0x04C1) //: LearnPreviewTalents (for player?)
            {Opcode.CMSG_LEARN_PREVIEW_TALENTS_PET, 0x04C2}, // NF()(0x04C2) // // CMSG}, lua: LearnPreviewTalents (for pet?)
            {Opcode.CMSG_LEARN_TALENT, 0x0251}, // NF()(0x0251) //
            {Opcode.CMSG_LEAVE_BATTLEFIELD, 0x7757}, // (0xAA15)(0x02E1) //
            //{Opcode.CMSG_CHAT_LEAVE_CHANNEL, 0x3A98}, // (0x9A20)(0x0098) //
            {Opcode.CMSG_LFG_JOIN, 0x035C}, // NF()(0x035C) // // CMSG JoinLFG
            {Opcode.CMSG_LFG_LEAVE, 0x035D}, // NF()(0x035D) // // CMSG LeaveLFG
            {Opcode.CMSG_LFG_LFR_JOIN, 0x035E}, // NF()(0x035E) // // CMSG SearchLFGJoin
            {Opcode.CMSG_LFG_LFR_LEAVE, 0x035F}, // NF()(0x035F) // // CMSG SearchLFGLeave
            {Opcode.CMSG_LFG_PARTY_LOCK_INFO_REQUEST, 0x0371}, // NF()(0x0371) // // CMSG RequestLFDPartyLockInfo
            {Opcode.CMSG_LFG_PLAYER_LOCK_INFO_REQUEST, 0x036E}, // NF()(0x036E) // // CMSG RequestLFDPlayerLockInfo
            {Opcode.CMSG_LFG_PROPOSAL_RESULT, 0x0362}, // NF()(0x0362) // // CMSG AcceptProposal}, RejectProposal
            {Opcode.CMSG_LFG_SET_BOOT_VOTE, 0x036C}, // NF()(0x036C) // // CMSG SetLFGBootVote
            {Opcode.CMSG_LFG_SET_COMMENT, 0x0366}, // NF()(0x0366) // // CMSG SetLFGComment
            {Opcode.CMSG_LFG_SET_NEEDS, 0x036B}, // NF()(0x036B) // // CMSG SetLFGNeeds
            {Opcode.CMSG_LFG_SET_ROLES, 0x036A}, // NF()(0x036A) // // CMSG SetLFGRoles
            {Opcode.CMSG_LFG_SET_ROLES_2, 0x04B8}, // NF()(0x04B8) // // not found
            {Opcode.CMSG_LFG_TELEPORT, 0x0370}, // NF()(0x0370) // // CMSG LFGTeleport
            {Opcode.CMSG_LIST_INVENTORY, 0x3E77}, // (0xCA59)(0x019E) //
            {Opcode.CMSG_LOAD_DANCES, 0x044D}, // NF()(0x044D) //
            {Opcode.CMSG_LOGOUT_CANCEL, 0x3F56}, // (0x2B19)(0x004E) //
            {Opcode.CMSG_LOGOUT_REQUEST, 0x8E56}, // (0x62D1)(0x004B) //
            {Opcode.CMSG_LOOT_UNIT, 0xBD77}, // (0x6390)(0x015D) //
            {Opcode.CMSG_LOOT_MASTER_GIVE, 0x02A3}, // NF()(0x02A3) //
            {Opcode.CMSG_SET_LOOT_METHOD, 0x447D}, // (0xEA99)(0x007A) //
            {Opcode.CMSG_LOOT_MONEY, 0x9455}, // (0xE2F0)(0x015E) //
            {Opcode.CMSG_LOOT_RELEASE, 0x947F}, // (0x82F8)(0x015F) //
            {Opcode.CMSG_LOOT_ROLL, 0x02A0}, // NF()(0x02A0) //
            {Opcode.CMSG_LOTTERY_QUERY_OBSOLETE, 0x0334}, // NF()(0x0334) //
            {Opcode.CMSG_LOW_LEVEL_RAID1, 0x0508}, // NF()(0x0508) // // lua: SetAllowLowLevelRaid
            {Opcode.CMSG_LUA_USAGE, 0x0323}, // NF()(0x0323) //
            {Opcode.CMSG_MAELSTROM_GM_SENT_MAIL, 0x0395}, // NF()(0x0395) //
            {Opcode.CMSG_MAELSTROM_INVALIDATE_CACHE, 0x0387}, // NF()(0x0387) //
            {Opcode.CMSG_MAELSTROM_RENAME_GUILD, 0x0400}, // NF()(0x0400) //
            {Opcode.CMSG_MAIL_CREATE_TEXT_ITEM, 0x024A}, // NF()(0x024A) //
            {Opcode.CMSG_MAIL_DELETE, 0x0249}, // NF()(0x0249) //
            {Opcode.CMSG_MAIL_MARK_AS_READ, 0x0247}, // NF()(0x0247) //
            {Opcode.CMSG_MAIL_RETURN_TO_SENDER, 0x0248}, // NF()(0x0248) //
            {Opcode.CMSG_MAIL_TAKE_ITEM, 0x0246}, // NF()(0x0246) //
            {Opcode.CMSG_MAIL_TAKE_MONEY, 0x0245}, // NF()(0x0245) //
            {Opcode.CMSG_MEETINGSTONE_CHEAT, 0x0294}, // NF()(0x0294) // // not found 3.3
            {Opcode.CMSG_MEETINGSTONE_INFO, 0x0296}, // NF()(0x0296) // // EVENT_LFG_UPDATE
            //{Opcode.CMSG_CHAT_MESSAGE_AFK, 0x6A88}, // (0x9A24)(0x0000) //
            {Opcode.CMSG_CHAT_MESSAGE_BATTLEGROUND, 0x1A10}, // (0x7204)(0x0000) //
            {Opcode.CMSG_CHAT_MESSAGE_BATTLEGROUND_LEADER, 0x7A80}, // (0x7A04)(0x0000) //
            {Opcode.CMSG_CHAT_MESSAGE_CHANNEL, 0x3A88}, // (0x5A20)(0x0000) //
            {Opcode.CMSG_CHAT_MESSAGE_DND, 0x3A00}, // (0x3A00)(0x0000) //
            //{Opcode.CMSG_CHAT_MESSAGE_EMOTE, 0x4A90}, // (0xD200)(0x0000) //
            {Opcode.CMSG_CHAT_MESSAGE_GUILD, 0x2A88}, // (0x7A20)(0x0000) //
            {Opcode.CMSG_CHAT_MESSAGE_OFFICER, 0x7A98}, // (0x9204)(0x0000) //
            {Opcode.CMSG_CHAT_MESSAGE_PARTY, 0x2A08}, // (0x1204)(0x0000) //
            {Opcode.CMSG_CHAT_MESSAGE_PARTY_LEADER, 0x6A18}, // (0x1224)(0x0000) //
            {Opcode.CMSG_CHAT_MESSAGE_RAID, 0x7A90}, // (0xF224)(0x0000) //
            {Opcode.CMSG_CHAT_MESSAGE_RAID_LEADER, 0x4A80}, // (0x1A00)(0x0000) //
            {Opcode.CMSG_CHAT_MESSAGE_RAID_WARNING, 0x4A88}, // (0xDA00)(0x0000) //
            {Opcode.CMSG_CHAT_MESSAGE_SAY, 0x5A90}, // (0x5200)(0x0000) //
            {Opcode.CMSG_CHAT_MESSAGE_WHISPER, 0x5A80}, // (0xDA24)(0x0000) //
            {Opcode.CMSG_CHAT_MESSAGE_YELL, 0x3A10}, // (0x7200)(0x0000) //
            {Opcode.CMSG_MINIGAME_MOVE, 0x02F8}, // NF()(0x02F8) //
            {Opcode.CMSG_MOUNT_SPECIAL_ANIM, 0x0171}, // NF()(0x0171) //
            {Opcode.CMSG_MOVE_CHNG_TRANSPORT, 0x038D}, // NF()(0x038D) //
            {Opcode.CMSG_MOVE_FALL_RESET, 0x02CA}, // NF()(0x02CA) //
            {Opcode.CMSG_MOVE_FEATHER_FALL_ACK, 0x02CF}, // NF()(0x02CF) //
            {Opcode.CMSG_MOVE_FLIGHT_ACK, 0x0340}, // NF()(0x0340) //
            {Opcode.CMSG_MOVE_GRAVITY_DISABLE_ACK, 0x04CF}, // NF()(0x04CF) // // movement related
            {Opcode.CMSG_MOVE_GRAVITY_ENABLE_ACK, 0x04D1}, // NF()(0x04D1) // // movement related
            {Opcode.CMSG_MOVE_HOVER_ACK, 0x00F6}, // NF()(0x00F6) //
            {Opcode.CMSG_MOVE_KNOCK_BACK_ACK, 0x00F0}, // NF()(0x00F0) //
            {Opcode.CMSG_MOVE_NOT_ACTIVE_MOVER, 0x02D1}, // NF()(0x02D1) //
            {Opcode.CMSG_MOVE_SET_CAN_FLY_ACK, 0x0345}, // NF()(0x0345) //
            {Opcode.CMSG_MOVE_SET_FLY, 0x0346}, // NF()(0x0346) //
            {Opcode.CMSG_MOVE_SET_RAW_POSITION, 0xFFDA}, // NF(0xCB75)(0x00E1) //
            {Opcode.CMSG_MOVE_SET_RUN_SPEED, 0x03AB}, // NF()(0x03AB) //
            {Opcode.CMSG_MOVE_SPLINE_DONE, 0x02C9}, // NF()(0x02C9) //
            {Opcode.CMSG_MOVE_START_SWIM_CHEAT, 0x02D8}, // NF()(0x02D8) //
            {Opcode.CMSG_MOVE_STOP_SWIM_CHEAT, 0x02D9}, // NF()(0x02D9) //
            {Opcode.CMSG_MOVE_TIME_SKIPPED, 0x02CE}, // NF()(0x02CE) //
            {Opcode.CMSG_MOVE_WATER_WALK_ACK, 0xFFBE}, // NF(0xFFFC)(0x02D0) //
            {Opcode.CMSG_NAME_QUERY, 0xC57E}, // (0x4354)(0x0050) //
            {Opcode.CMSG_NEW_SPELL_SLOT, 0x012D}, // NF()(0x012D) // NOT IN CLIENT 335 AND 401
            {Opcode.CMSG_NEXT_CINEMATIC_CAMERA, 0x00FB}, // NF()(0x00FB) //
            {Opcode.CMSG_NO_SPELL_VARIANCE, 0x0416}, // NF()(0x0416) //
            {Opcode.CMSG_QUERY_NPC_TEXT, 0x5654}, // (0x2BBD)(0x017F) //
            {Opcode.CMSG_OFFER_PETITION, 0x01C3}, // NF()(0x01C3) //
            {Opcode.CMSG_OPENING_CINEMATIC, 0x00F9}, // NF()(0x00F9) //
            {Opcode.CMSG_OPEN_ITEM, 0x00AC}, // NF()(0x00AC) //
            {Opcode.CMSG_OPT_OUT_OF_LOOT, 0x0409}, // NF()(0x0409) //
            {Opcode.CMSG_QUERY_PAGE_TEXT, 0x2C75}, // (0xEABD)(0x005A) //
            {Opcode.CMSG_PARTY_SILENCE, 0xF755}, // (0x6BF0)(0x03DD) //
            {Opcode.CMSG_PARTY_UNSILENCE, 0x2F7D}, // (0xAA19)(0x03DE) //
            {Opcode.CMSG_PETITION_BUY, 0x01BD}, // NF()(0x01BD) //
            {Opcode.CMSG_PETITION_QUERY, 0xFFD3}, // NF(0x6255)(0x01C6) //
            {Opcode.CMSG_PETITION_SHOW_LIST, 0x01BB}, // NF()(0x01BB) //
            {Opcode.CMSG_PETITION_SHOW_SIGNATURES, 0x01BE}, // NF()(0x01BE) //
            {Opcode.CMSG_PETITION_SIGN, 0x01C0}, // NF()(0x01C0) //
            {Opcode.CMSG_PET_ABANDON, 0x3576}, // (0x02D5)(0x0176) //
            {Opcode.CMSG_PET_ACTION, 0x3C55}, // (0x8B19)(0x0175) //
            {Opcode.CMSG_PET_CANCEL_AURA, 0x026B}, // NF()(0x026B) //
            {Opcode.CMSG_PET_CAST_SPELL, 0xC674}, // (0xC355)(0x01F0) //
            {Opcode.CMSG_PET_LEARN_TALENT, 0x047A}, // NF()(0x047A) //
            {Opcode.CMSG_QUERY_PET_NAME, 0xFFF0}, // NF(0xABF1)(0x0052) //
            {Opcode.CMSG_PET_RENAME, 0x7D57}, // (0x4AF8)(0x0177) //
            {Opcode.CMSG_PET_SET_ACTION, 0x977F}, // (0x2318)(0x0174) //
            {Opcode.CMSG_PET_SPELL_AUTOCAST, 0x02F3}, // NF()(0x02F3) //
            {Opcode.CMSG_PET_STOP_ATTACK, 0x02EA}, // NF()(0x02EA) //
            {Opcode.CMSG_PET_UNLEARN, 0x02F0}, // NF()(0x02F0) // // Deprecated 3.x
            {Opcode.CMSG_PET_UNLEARN_TALENTS, 0x047B}, // NF()(0x047B) //
            {Opcode.CMSG_PING, 0x882A}, // ()(0x01DC) //
            {Opcode.CMSG_REQUEST_PLAYED_TIME, 0x7E5E}, // (0x8355)(0x01CC) //
            {Opcode.CMSG_PLAYER_AI_CHEAT, 0x026C}, // NF()(0x026C) //
            {Opcode.CMSG_PLAYER_DIFFICULTY_CHANGE, 0x7E5D}, // (0x03D0)(0x01FD) //
            {Opcode.CMSG_PLAYER_LOGIN, 0x05A1}, // (0x1621)(0x003D) //
            {Opcode.CMSG_PLAYER_LOGOUT, 0x3557}, // (0xCA18)(0x004A) //
            {Opcode.CMSG_PLAYER_VEHICLE_ENTER, 0x04A8}, // NF()(0x04A8) // // cmsg uint64
            {Opcode.CMSG_PLAY_DANCE, 0xBC57}, // (0x0318)(0x044B) //
            {Opcode.CMSG_PUSH_QUEST_TO_PARTY, 0xE755}, // (0x2314)(0x019D) //
            {Opcode.CMSG_QUERY_INSPECT_ACHIEVEMENTS, 0x046B}, // NF()(0x046B) //
            {Opcode.CMSG_QUERY_QUESTS_COMPLETED, 0x0500}, // NF()(0x0500) // // lua: QueryQuestsCompleted
            {Opcode.CMSG_QUERY_SERVER_BUCK_DATA, 0x041B}, // NF()(0x041B) //
            {Opcode.CMSG_QUERY_TIME, 0x01CE}, // NF()(0x01CE) //
            {Opcode.CMSG_QUERY_VEHICLE_STATUS, 0x04A4}, // NF()(0x04A4) // // not found
            {Opcode.CMSG_QUEST_GIVER_ACCEPT_QUEST, 0xED54}, // (0xE239)(0x0189) //
            {Opcode.CMSG_QUEST_GIVER_CANCEL, 0x0190}, // NF()(0x0190) //
            {Opcode.CMSG_QUEST_GIVER_CHOOSE_REWARD, 0xF75F}, // (0xE290)(0x018E) //
            {Opcode.CMSG_QUEST_GIVER_COMPLETE_QUEST, 0x8E55}, // (0x2A5C)(0x018A) //
            {Opcode.CMSG_QUEST_GIVER_HELLO, 0x3656}, // (0x2AB8)(0x0184) //
            {Opcode.CMSG_QUEST_GIVER_QUERY_QUEST, 0x5E7D}, // (0x0AFC)(0x0186) //
            {Opcode.CMSG_QUEST_GIVER_QUEST_AUTOLAUNCH, 0x0187}, // NF()(0x0187) //
            {Opcode.CMSG_QUEST_GIVER_REQUEST_REWARD, 0x0E7D}, // (0xEAF8)(0x018C) //
            {Opcode.CMSG_QUEST_GIVER_STATUS_MULTIPLE_QUERY, 0x0417}, // NF()(0x0417) //
            {Opcode.CMSG_QUEST_GIVER_STATUS_QUERY, 0x0182}, // NF()(0x0182) //
            {Opcode.CMSG_QUEST_LOG_REMOVE_QUEST, 0x0194}, // NF()(0x0194) //
            {Opcode.CMSG_QUEST_LOG_SWAP_QUEST, 0x0193}, // NF()(0x0193) //
            {Opcode.CMSG_QUEST_CONFIRM_ACCEPT, 0xD756}, // (0x8350)(0x019B) //
            {Opcode.CMSG_QUERY_QUEST_COMPLETION_NPCS, 0x0489}, // NF()(0x0489) //
            {Opcode.CMSG_QUEST_POI_QUERY, 0x01E3}, // NF()(0x01E3) //
            {Opcode.CMSG_QUERY_QUEST_INFO, 0xFFEA}, // NF(0xE27C)(0x005C) //
            {Opcode.CMSG_READY_FOR_ACCOUNT_DATA_TIMES, 0xD677}, // (0x6A99)(0x04FF) //yForAccountDataTimes
            {Opcode.CMSG_READ_ITEM, 0x00AD}, // NF()(0x00AD) //
            {Opcode.CMSG_REALM_SPLIT, 0x477D}, // (0xAB58)(0x038C) //
            {Opcode.CMSG_RECLAIM_CORPSE, 0x01D2}, // NF()(0x01D2) //
            {Opcode.CMSG_REFER_A_FRIEND, 0x040E}, // NF()(0x040E) //
            {Opcode.CMSG_REMOVE_GLYPH, 0x048A}, // NF()(0x048A) //
            {Opcode.CMSG_REPAIR_ITEM, 0x02A8}, // NF()(0x02A8) //
            {Opcode.CMSG_CHAT_REPORT_FILTERED, 0x0331}, // NF()(0x0331) //
            {Opcode.CMSG_CHAT_REPORT_IGNORED, 0x0225}, // NF()(0x0225) //
            {Opcode.CMSG_REPOP_REQUEST, 0x057E}, // (0xAB3C)(0x015A) //
            {Opcode.CMSG_REPORT_PVP_PLAYER_AFK, 0x03E4}, // NF()(0x03E4) //
            {Opcode.CMSG_REQUEST_ACCOUNT_DATA, 0x475C}, // (0x0218)(0x020A) //
            {Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS, 0x027F}, // NF()(0x027F) //
            {Opcode.CMSG_REQUEST_PET_INFO, 0x0279}, // NF()(0x0279) //
            {Opcode.CMSG_REQUEST_RAID_INFO, 0x02CD}, // NF()(0x02CD) //
            {Opcode.CMSG_REQUEST_VEHICLE_EXIT, 0x0476}, // NF()(0x0476) //
            {Opcode.CMSG_REQUEST_VEHICLE_NEXT_SEAT, 0x0478}, // NF()(0x0478) //
            {Opcode.CMSG_REQUEST_VEHICLE_PREV_SEAT, 0x0477}, // NF()(0x0477) //
            {Opcode.CMSG_REQUEST_VEHICLE_SWITCH_SEAT, 0x0479}, // NF()(0x0479) //
            {Opcode.CMSG_RESET_FACTION_CHEAT, 0x0281}, // NF()(0x0281) //
            {Opcode.CMSG_RESET_INSTANCES, 0x031D}, // NF()(0x031D) //
            //{Opcode.CMSG_RESURRECT_RESPONSE, 0x8457}, // (0xCB5C)(0x015C) //
            {Opcode.CMSG_RUN_SCRIPT, 0x02B5}, // NF()(0x02B5) //
            {Opcode.CMSG_SAVE_DANCE, 0x0449}, // NF()(0x0449) //
            {Opcode.CMSG_SAVE_PLAYER, 0x0153}, // NF()(0x0153) // NOT IN CLIENT 335 AND 401
            {Opcode.CMSG_SELF_RES, 0xBD5F}, // (0x62D5)(0x02B3) //
            {Opcode.CMSG_SELL_ITEM, 0xDF77}, // (0x8BB5)(0x01A0) //
            {Opcode.CMSG_SEND_COMBAT_TRIGGER, 0x0394}, // NF()(0x0394) //
            {Opcode.CMSG_SEND_GENERAL_TRIGGER, 0x0393}, // NF()(0x0393) //
            {Opcode.CMSG_SEND_LOCAL_EVENT, 0x0392}, // NF()(0x0392) //
            {Opcode.CMSG_SEND_MAIL, 0x0238}, // NF()(0x0238) //
            {Opcode.CMSG_SERVERTIME, 0x0048}, // NF()(0x0048) // NOT IN CLIENT 335 AND 401
            {Opcode.CMSG_SERVER_BROADCAST, 0x02B2}, // NF()(0x02B2) //
            {Opcode.CMSG_SERVER_INFO_QUERY, 0x04A0}, // NF()(0x04A0) // // not found
            {Opcode.CMSG_SETDEATHBINDPOINT, 0x0154}, // NF()(0x0154) // NOT IN CLIENT 335 AND 401
            {Opcode.CMSG_SET_ACTION_BAR_TOGGLES, 0x5754}, // (0x2270)(0x02BF) //
            {Opcode.CMSG_SET_ACTION_BUTTON, 0x355C}, // ()(0x0128) //
            {Opcode.CMSG_SET_ACTIVE_MOVER, 0x0676}, // (0xCB54)(0x026A) //
            {Opcode.CMSG_SET_ACTIVE_VOICE_CHANNEL, 0x03D3}, // NF()(0x03D3) //
            //{Opcode.CMSG_SET_ALLOW_LOW_LEVEL_RAID2, 0x0509}, // NF()(0x0509) // // lua: SetAllowLowLevelRaid
            {Opcode.CMSG_SET_AMMO, 0x0268}, // NF()(0x0268) //
            {Opcode.CMSG_SET_ASSISTANT_LEADER, 0x8556}, // (0xEBD1)(0x028F) //
            {Opcode.CMSG_SET_CHANNEL_WATCH, 0x2D54}, // (0xC37C)(0x03EF) //
            {Opcode.CMSG_SET_CONTACT_NOTES, 0x8C5E}, // (0x03F9)(0x006B) //
            {Opcode.CMSG_SET_CRITERIA_CHEAT, 0x0470}, // NF()(0x0470) //
            {Opcode.CMSG_SET_DURABILITY_CHEAT, 0x0287}, // NF()(0x0287) //
            {Opcode.CMSG_SET_EXPLORATION, 0x02BE}, // NF()(0x02BE) //
            {Opcode.CMSG_SET_EXPLORATION_ALL, 0x031B}, // NF()(0x031B) //
            {Opcode.CMSG_SET_FACTION_AT_WAR, 0x0125}, // NF()(0x0125) //
            {Opcode.CMSG_SET_FACTION_CHEAT, 0x0126}, // NF()(0x0126) //
            {Opcode.CMSG_SET_FACTION_INACTIVE, 0x0317}, // NF()(0x0317) //
            {Opcode.CMSG_SET_GLYPH, 0x0467}, // NF()(0x0467) //
            {Opcode.CMSG_SET_GLYPH_SLOT, 0x0466}, // NF()(0x0466) //
            {Opcode.CMSG_SET_GRANTABLE_LEVELS, 0x040C}, // NF()(0x040C) //
            {Opcode.CMSG_SET_PLAYER_DECLINED_NAMES, 0x0419}, // NF()(0x0419) //
            {Opcode.CMSG_SET_PVP_RANK_CHEAT, 0x0288}, // NF()(0x0288) //
            {Opcode.CMSG_SET_PVP_TITLE, 0x028B}, // NF()(0x028B) //
            {Opcode.CMSG_SET_RUNE_COOLDOWN, 0x0459}, // NF()(0x0459) //
            {Opcode.CMSG_SET_RUNE_COUNT, 0x0458}, // NF()(0x0458) //
            {Opcode.CMSG_SET_SAVED_INSTANCE_EXTEND, 0x0292}, // NF()(0x0292) // // lua: SetSavedInstanceExtend
            {Opcode.CMSG_SET_SELECTION, 0x5577}, // (0xEBB5)(0x013D) //
            {Opcode.CMSG_SET_SHEATHED, 0x01E0}, // NF()(0x01E0) //
            {Opcode.CMSG_SET_SKILL_CHEAT, 0x01D8}, // NF()(0x01D8) //
            {Opcode.CMSG_SET_TAXI_BENCHMARK_MODE, 0x0389}, // NF()(0x0389) //
            {Opcode.CMSG_SET_TITLE, 0x0374}, // NF()(0x0374) //
            {Opcode.CMSG_SET_TITLE_SUFFIX, 0x03F7}, // NF()(0x03F7) //
            {Opcode.CMSG_SET_TRADE_GOLD, 0x011F}, // NF()(0x011F) //
            {Opcode.CMSG_SET_TRADE_ITEM, 0x011D}, // NF()(0x011D) //
            {Opcode.CMSG_SET_WATCHED_FACTION, 0x0318}, // NF()(0x0318) //
            {Opcode.CMSG_SHOWING_CLOAK, 0xBE7F}, // (0xCBF4)(0x02BA) //
            {Opcode.CMSG_SHOWING_HELM, 0x7555}, // (0x0A74)(0x02B9) //
            {Opcode.CMSG_SKILL_BUY_RANK, 0x0220}, // NF()(0x0220) //
            {Opcode.CMSG_SKILL_BUY_STEP, 0x021F}, // NF()(0x021F) //
            {Opcode.CMSG_SOCKET_GEMS, 0x0347}, // NF()(0x0347) //
            {Opcode.CMSG_SPELL_CLICK, 0xFFA6}, // NF(0xF001)(0x03F8) //
            {Opcode.CMSG_SPIRIT_HEALER_ACTIVATE, 0x021C}, // NF()(0x021C) //
            {Opcode.CMSG_SPLIT_ITEM, 0x010E}, // NF()(0x010E) //
            {Opcode.CMSG_STABLE_PET, 0x0270}, // NF()(0x0270) //
            {Opcode.CMSG_STABLE_REVIVE_PET, 0x0274}, // NF()(0x0274) //
            {Opcode.CMSG_STABLE_SWAP_PET, 0x0275}, // NF()(0x0275) //
            {Opcode.CMSG_STAND_STATE_CHANGE, 0x0101}, // NF()(0x0101) //
            {Opcode.CMSG_STOP_DANCE, 0x044E}, // NF()(0x044E) //
            {Opcode.CMSG_STORE_LOOT_IN_SLOT, 0xCD77}, // (0xA374)(0x0109) //
            {Opcode.CMSG_SUMMON_RESPONSE, 0x4D77}, // (0x8A18)(0x02AC) //
            {Opcode.CMSG_SWAP_INV_ITEM, 0xCD75}, // (0x03D4)(0x010D) //
            {Opcode.CMSG_SWAP_ITEM, 0x4D5D}, // (0x2319)(0x010C) //
            {Opcode.CMSG_SYNC_DANCE, 0x0450}, // NF()(0x0450) //
            {Opcode.CMSG_TARGET_CAST, 0x03D0}, // NF()(0x03D0) //
            {Opcode.CMSG_TARGET_SCRIPT_CAST, 0x03D1}, // NF()(0x03D1) //
            {Opcode.CMSG_TAXICLEARALLNODES, 0x01A6}, // NF()(0x01A6) //
            {Opcode.CMSG_TAXICLEARNODE, 0x0241}, // NF()(0x0241) //
            {Opcode.CMSG_TAXIENABLEALLNODES, 0x01A7}, // NF()(0x01A7) //
            {Opcode.CMSG_TAXIENABLENODE, 0x0242}, // NF()(0x0242) //
            {Opcode.CMSG_TAXISHOWNODES, 0x01A8}, // NF()(0x01A8) //
            {Opcode.CMSG_TAXI_NODE_STATUS_QUERY, 0x01AA}, // NF()(0x01AA) //
            {Opcode.CMSG_TAXI_QUERY_AVAILABLE_NODES, 0x01AC}, // NF()(0x01AC) //
            {Opcode.CMSG_TELEPORT_TO_UNIT, 0xFFFF}, // NF(0xCB9D)(0x0009) //
            //{Opcode.CMSG_SEND_TEXT_EMOTE, 0x4A90}, // (0xD200)(0x0104) //
            {Opcode.CMSG_TIME_SYNC_RESPONSE, 0x0D57}, // ()(0x0391) //
            {Opcode.CMSG_TOGGLE_PVP, 0x0253}, // NF()(0x0253) //
            {Opcode.CMSG_TOTEM_DESTROYED, 0x0414}, // NF()(0x0414) //
            {Opcode.CMSG_TRAINER_BUY_SPELL, 0x9754}, // (0x4391)(0x01B2) //
            //{Opcode.CMSG_TRAINER_LIST, 0x8E7C}, // (0x0271)(0x01B0) //
            {Opcode.CMSG_TRIGGER_CINEMATIC_CHEAT, 0x00F8}, // NF()(0x00F8) //
            {Opcode.CMSG_TURN_IN_PETITION, 0x01C4}, // NF()(0x01C4) //
            {Opcode.CMSG_TUTORIAL_CLEAR, 0x00FF}, // NF()(0x00FF) //
            {Opcode.CMSG_TUTORIAL_FLAG, 0x00FE}, // NF()(0x00FE) //
            {Opcode.CMSG_TUTORIAL_RESET, 0x0100}, // NF()(0x0100) //
            {Opcode.CMSG_UI_TIME_REQUEST, 0x3574}, // (0x4A55)(0x0000) //
            {Opcode.CMSG_UNACCEPT_TRADE, 0x011B}, // NF()(0x011B) //
            {Opcode.CMSG_UNITANIMTIER_CHEAT, 0x0472}, // NF()(0x0472) //
            {Opcode.CMSG_UNKNOWN_1303, 0x0517}, // NF()(0x0517) // // something with player movement (move event 58?)
            {Opcode.CMSG_UNKNOWN_1309, 0x051D}, // NF()(0x051D) // // Lua_Transform
            {Opcode.CMSG_UNKNOWN_1320, 0x0528}, // NF()(0x0528) // // setcurrency console command?
            {Opcode.CMSG_UNLEARN_DANCE_MOVE, 0x0457}, // NF()(0x0457) //
            {Opcode.CMSG_UNLEARN_SKILL, 0x0202}, // NF()(0x0202) //
            {Opcode.CMSG_UNLEARN_SPELL, 0x0201}, // NF()(0x0201) //
            {Opcode.CMSG_UNLEARN_TALENTS, 0x0213}, // NF()(0x0213) //
            {Opcode.CMSG_UNSTABLE_PET, 0xFFC0}, // NF(0xFFFF)(0x0271) //
            {Opcode.CMSG_UNUSED2, 0x0140}, // NF()(0x0140) // NOT IN CLIENT 335 AND 401
            {Opcode.CMSG_UPDATE_ACCOUNT_DATA, 0xFF7E}, // (0xEB55)(0x020B) //
            {Opcode.CMSG_UPDATE_MISSILE_TRAJECTORY, 0x0462}, // NF()(0x0462) //
            {Opcode.CMSG_UPDATE_PROJECTILE_POSITION, 0x04BE}, // NF()(0x04BE) // // CMSG}, uint64 caster}, uint32 spellId}, uint8 castId}, vector3 position
            {Opcode.CMSG_USE_ITEM, 0x4E57}, // (0x0330)(0x00AB) //
            {Opcode.CMSG_VOICE_SESSION_ENABLE, 0x477C}, // (0x82B9)(0x03AF) //
            {Opcode.CMSG_VOICE_SET_TALKER_MUTED_REQUEST, 0x03A1}, // NF()(0x03A1) //
            {Opcode.CMSG_WARDEN_DATA, 0x467F}, // (0x233D)(0x02E7) //
            {Opcode.CMSG_WHO, 0x8E5E}, // (0x0AB0)(0x0062) //
            {Opcode.CMSG_WHO_IS, 0xD776}, // (0x235D)(0x0064) //
            {Opcode.CMSG_WORLD_TELEPORT, 0x0589}, // (0x5211)(0x0008) //
            {Opcode.CMSG_WRAP_ITEM, 0x01D3}, // NF()(0x01D3) //
            {Opcode.CMSG_ZONEUPDATE, 0x5C7D} // (0x2AD8)(0x01F4) //
        };

        private static readonly BiDictionary<Opcode, int> ServerOpcodes = new BiDictionary<Opcode, int>
        {
            {Opcode.SMSG_ACCOUNT_DATA_TIMES, 0xFD55}, // (0x82B5)(0x0209) //
            {Opcode.SMSG_ACHIEVEMENT_DELETED, 0x049F}, // NF()(0x049F) // // not changed 9626
            {Opcode.SMSG_ACHIEVEMENT_EARNED, 0x0468}, // NF()(0x0468) //
            {Opcode.SMSG_ACTIVATE_TAXI_REPLY, 0x6F7C}, // (0x23D5)(0x01AE) //
            {Opcode.SMSG_ADDON_INFO, 0xEE5D}, // (0x0AF9)(0x02EF) //
            {Opcode.SMSG_ADD_RUNE_POWER, 0xFF9A}, // NF(0x4A3D)(0x0488) //
            {Opcode.SMSG_AI_REACTION, 0x1E55}, // (0xEBB1)(0x013C) //
            {Opcode.SMSG_ALL_ACHIEVEMENT_DATA, 0xFF9D}, // NF(0xE307)(0x047D) //o 0xC084 before CallHandler
            {Opcode.SMSG_AREA_SPIRIT_HEALER_TIME, 0xA457}, // (0xA338)(0x02E4) //
            {Opcode.SMSG_AREA_TRIGGER_MESSAGE, 0xCC5F}, // (0xAA7D)(0x02B8) //
            {Opcode.SMSG_AREA_TRIGGER_NO_CORPSE, 0x0506}, // NF()(0x0506) //
            {Opcode.SMSG_ARENA_ERROR, 0x5457}, // (0x6295)(0x0376) //
            {Opcode.SMSG_ARENA_OPPONENT_UPDATE, 0x5B29}, // ()(0x04C7) // // uint64}, EVENT_ARENA_OPPONENT_UPDATE
            {Opcode.SMSG_ARENA_TEAM_CHANGE_FAILED_QUEUED, 0xFF96}, // NF(0xA370)(0x04C8) //an't modify arena team while queued or in a match." 3.2
            {Opcode.SMSG_ARENA_TEAM_COMMAND_RESULT, 0x0349}, // NF()(0x0349) //
            {Opcode.SMSG_ARENA_TEAM_EVENT, 0x6554}, // (0xAB70)(0x0357) //
            {Opcode.SMSG_ARENA_TEAM_INVITE, 0xE575}, // (0x2399)(0x0350) //
            {Opcode.SMSG_ARENA_TEAM_QUERY_RESPONSE, 0xC02B}, // (0x21A0)(0x034C) //
            {Opcode.SMSG_ARENA_TEAM_ROSTER, 0xA80A}, // (0x23F1)(0x034E) //
            {Opcode.SMSG_ARENA_TEAM_STATS, 0x9B0B}, // (0x03B4)(0x035B) //
            {Opcode.SMSG_ATTACKER_STATE_UPDATE, 0xBF56}, // (0x8334)(0x014A) //
            {Opcode.SMSG_ATTACK_START, 0x047E}, // (0x63D5)(0x0143) //
            {Opcode.SMSG_ATTACK_STOP, 0x9D5F}, // (0x6355)(0x0144) //
            {Opcode.SMSG_ATTACKSWING_BADFACING, 0x767D}, // (0x8251)(0x0146) //
            {Opcode.SMSG_ATTACKSWING_CANT_ATTACK, 0x3E76}, // (0x8B98)(0x0149) //
            {Opcode.SMSG_ATTACKSWING_DEADTARGET, 0x2677}, // (0x235C)(0x0148) //
            {Opcode.SMSG_ATTACKSWING_NOTINRANGE, 0x2F54}, // (0x4B54)(0x0145) //
            {Opcode.SMSG_AUCTION_BIDDER_NOTIFICATION, 0x3021}, // (0x4250)(0x025E) //
            {Opcode.SMSG_AUCTION_COMMAND_RESULT, 0xEB22}, // (0xAB5D)(0x025B) //
            {Opcode.SMSG_AUCTION_LIST_BIDDER_ITEMS_RESULT, 0xEA0A}, // (0x42D5)(0x0265) //
            {Opcode.SMSG_AUCTION_LIST_OWNER_ITEMS_RESULT, 0xDA22}, // (0x4B94)(0x025D) //
            {Opcode.SMSG_AUCTION_LIST_PENDING_SALES, 0xDB29}, // (0xE2F9)(0x0490) //
            {Opcode.SMSG_AUCTION_LIST_RESULT, 0xAB03}, // (0x827C)(0x025C) //
            {Opcode.SMSG_AUCTION_OWNER_NOTIFICATION, 0xC009}, // (0x42B5)(0x025F) //
            {Opcode.SMSG_AUCTION_REMOVED_NOTIFICATION, 0x0A0A}, // (0x4379)(0x028D) //
            {Opcode.SMSG_AURACASTLOG, 0x01D1}, // NF()(0x01D1) //
            //{Opcode.SMSG_AURA_UPDATE, 0xCE7D}, // (0xA3D4)(0x0496) //
            {Opcode.SMSG_AURA_UPDATE_ALL, 0x1C76}, // (0xE298)(0x0495) //
            {Opcode.SMSG_AUTH_CHALLENGE, 0x3400}, // (0x8500)(0x01EC) //
            {Opcode.SMSG_AUTH_RESPONSE, 0x1454}, // (0xEB58)(0x01EE) //
            {Opcode.SMSG_AVAILABLE_VOICE_CHANNEL, 0xFFA8}, // NF(0xA371)(0x03DA) //
            {Opcode.SMSG_BARBER_SHOP_RESULT, 0xB57D}, // (0x42F9)(0x0428) //
            {Opcode.SMSG_BATTLEFIELD_LIST, 0x023D}, // NF()(0x023D) //
            {Opcode.SMSG_BATTLEFIELD_LOSE_OBSOLETE, 0x0240}, // NF()(0x0240) //
            {Opcode.SMSG_BATTLEFIELD_MGR_EJECTED, 0x04E6}, // NF()(0x04E6) // // uint32}, uint32}, uint8 EVENT_BATTLEFIELD_MGR_EJECTED
            {Opcode.SMSG_BATTLEFIELD_MGR_EJECT_PENDING, 0x04E5}, // NF()(0x04E5) // // uint32 EVENT_BATTLEFIELD_MGR_EJECT_PENDING
            {Opcode.SMSG_BATTLEFIELD_MGR_ENTERING, 0x04E0}, // NF()(0x04E0) // // uint32}, uint8}, uint8 EVENT_BATTLEFIELD_MGR_ENTERED
            {Opcode.SMSG_BATTLEFIELD_MGR_ENTRY_INVITE, 0x04DE}, // NF()(0x04DE) // // uint32}, EVENT_BATTLEFIELD_MGR_ENTRY_INVITE
            {Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_INVITE, 0x04E1}, // NF()(0x04E1) // // uint32 EVENT_BATTLEFIELD_MGR_QUEUE_INVITE
            {Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_REQUEST_RESPONSE, 0x04E4}, // NF()(0x04E4) // // uint32}, uint8 EVENT_BATTLEFIELD_MGR_QUEUE_REQUEST_RESPONSE
            {Opcode.SMSG_BATTLEFIELD_MGR_STATE_CHANGE, 0x04E8}, // NF()(0x04E8) // // uint32}, uint32 EVENT_BATTLEFIELD_MGR_STATE_CHANGE
            {Opcode.SMSG_BATTLEFIELD_PORT_DENIED, 0x014B}, // NF()(0x014B) //
            {Opcode.SMSG_BATTLEFIELD_STATUS, 0x02D4}, // NF()(0x02D4) //
            {Opcode.SMSG_BATTLEFIELD_STATUS_QUEUED, 0x02E8}, // NF()(0x02E8) //
            {Opcode.SMSG_BATTLEFIELD_WIN_OBSOLETE, 0x023F}, // NF()(0x023F) //
            {Opcode.SMSG_BATTLEGROUND_INFO_THROTTLED, 0xFF99}, // NF(0xC251)(0x04A6) //ou can't do that yet"
            {Opcode.SMSG_BATTLEGROUND_PLAYER_JOINED, 0x02EC}, // NF()(0x02EC) //
            {Opcode.SMSG_BATTLEGROUND_PLAYER_LEFT, 0x02ED}, // NF()(0x02ED) //
            {Opcode.SMSG_BINDER_CONFIRM, 0xEF7C}, // (0x4234)(0x02EB) //
            {Opcode.SMSG_BIND_POINT_UPDATE, 0x175D}, // (0xA255)(0x0155) //
            {Opcode.SMSG_BINDZONEREPLY, 0xB477}, // (0xC338)(0x0157) //
            {Opcode.SMSG_BREAK_TARGET, 0x0152}, // NF()(0x0152) // NOT IN CLIENT 335 AND 401
            {Opcode.SMSG_BUY_BANK_SLOT_RESULT, 0x01BA}, // NF()(0x01BA) //
            {Opcode.SMSG_BUY_FAILED, 0xF757}, // (0x83DD)(0x01A5) //
            {Opcode.SMSG_BUY_SUCCEEDED, 0x4676}, // (0x8AF4)(0x01A4) //
            {Opcode.SMSG_CALENDAR_ACTION_PENDING, 0x265E}, // (0x0B71)(0x04BB) //endar related EVENT_CALENDAR_ACTION_PENDING
            {Opcode.SMSG_CALENDAR_ARENA_TEAM, 0x2E7F}, // (0x23F8)(0x0439) //
            {Opcode.SMSG_CALENDAR_COMMAND_RESULT, 0xFFA1}, // NF(0xE2FC)(0x043D) //
            {Opcode.SMSG_CALENDAR_EVENT_INVITE, 0xFC57}, // (0xC298)(0x043A) //
            {Opcode.SMSG_CALENDAR_EVENT_INVITE_ALERT, 0xFD74}, // (0xE3F4)(0x0440) //
            {Opcode.SMSG_CALENDAR_EVENT_INVITE_REMOVED, 0x6D77}, // (0x4238)(0x043B) //
            {Opcode.SMSG_CALENDAR_EVENT_INVITE_REMOVED_ALERT, 0x1C75}, // (0xCB19)(0x0441) //
            {Opcode.SMSG_CALENDAR_EVENT_INVITE_STATUS_ALERT, 0xCE77}, // (0x4A15)(0x0442) //
            {Opcode.SMSG_CALENDAR_EVENT_MODERATOR_STATUS_ALERT, 0x6F54}, // (0xCA98)(0x0445) //
            {Opcode.SMSG_CALENDAR_EVENT_REMOVED_ALERT, 0x667C}, // (0x2315)(0x0443) //
            {Opcode.SMSG_CALENDAR_EVENT_STATUS, 0x8475}, // (0x0BB8)(0x043C) //
            {Opcode.SMSG_CALENDAR_EVENT_UPDATED_ALERT, 0x1654}, // (0xE270)(0x0444) //
            {Opcode.SMSG_CALENDAR_FILTER_GUILD, 0x4D7D}, // (0xEB1C)(0x0438) //
            {Opcode.SMSG_CALENDAR_RAID_LOCKOUT_ADDED, 0x5D7D}, // (0x4A35)(0x043E) //
            {Opcode.SMSG_CALENDAR_RAID_LOCKOUT_REMOVED, 0xFF57}, // (0x62B8)(0x043F) //
            {Opcode.SMSG_CALENDAR_SEND_CALENDAR, 0x147C}, // (0x8B91)(0x0436) //
            {Opcode.SMSG_CALENDAR_SEND_EVENT, 0xC754}, // (0x4294)(0x0437) //
            {Opcode.SMSG_CALENDAR_SEND_NUM_PENDING, 0xD675}, // (0x2BD0)(0x0448) //
            {Opcode.SMSG_CALENDAR_UPDATE_INVITE_LIST, 0xB77E}, // (0x421D)(0x0460) //
            {Opcode.SMSG_CALENDAR_UPDATE_INVITE_LIST2, 0xDE7E}, // (0x6AB4)(0x0461) //
            {Opcode.SMSG_CALENDAR_UPDATE_INVITE_LIST3, 0x4E7F}, // (0xE3B8)(0x0471) //
            {Opcode.SMSG_CAMERA_SHAKE, 0xFF93}, // NF(0xA3B0)(0x050A) //ellEffectCameraShakes.dbc index}, uint32
            //{Opcode.SMSG_CANCEL_AUTO_REPEAT, 0x0456}, // (0xE398)(0x029C) //
            {Opcode.SMSG_CANCEL_COMBAT, 0xD45C}, // (0xEA7D)(0x014E) //
            {Opcode.SMSG_CAST_FAILED, 0xD45D}, // (0x4AB8)(0x0130) //
            {Opcode.SMSG_CHANNEL_LIST, 0x7009}, // (0x0A5D)(0x009B) //
            {Opcode.SMSG_CHANNEL_MEMBER_COUNT, 0x0823}, // (0xAAB1)(0x03D5) //
            {Opcode.SMSG_CHANNEL_NOTIFY, 0xC574}, // (0x6358)(0x0099) //
            {Opcode.SMSG_CHARACTER_LOGIN_FAILED, 0x2C56}, // (0xCBD9)(0x0041) //
            {Opcode.SMSG_CHARACTER_PROFILE, 0x0338}, // NF()(0x0338) //
            {Opcode.SMSG_CHARACTER_PROFILE_REALM_CONNECTED, 0x0339}, // NF()(0x0339) //
            //{Opcode.SMSG_CREATE_CHAR, 0x8C7F}, // (0xC211)(0x003A) //
            {Opcode.SMSG_CHAR_CUSTOMIZE, 0x5F5E}, // (0xE2B5)(0x0474) //
            {Opcode.SMSG_DELETE_CHAR, 0x7F56}, // (0x63BC)(0x003C) //
            {Opcode.SMSG_ENUM_CHARACTERS_RESULT, 0x775E}, // (0x429C)(0x003B) //
            {Opcode.SMSG_CHAR_FACTION_CHANGE_RESULT, 0xCC76}, // (0x42FD)(0x04DA) //to 1241 (PFC server response)
            {Opcode.SMSG_CHARACTER_RENAME_RESULT, 0xA47D}, // (0xA33C)(0x02C8) //
            {Opcode.SMSG_CHAT, 0x867F}, // (0x0BD0)(0x0096) //
            {Opcode.SMSG_CHAT_PLAYER_AMBIGUOUS, 0x032D}, // NF()(0x032D) //
            {Opcode.SMSG_CHAT_PLAYER_NOTFOUND, 0x3654}, // (0xC2D0)(0x02A9) //
            {Opcode.SMSG_CHAT_RESTRICTED, 0x3674}, // (0x23BC)(0x02FD) //
            {Opcode.SMSG_CHAT_WRONG_FACTION, 0xC55F}, // (0xE2D4)(0x0219) //
            {Opcode.SMSG_CHEAT_DUMP_ITEMS_DEBUG_ONLY_RESPONSE, 0xFFB0}, // NF(0x212C)(0x039B) //
            {Opcode.SMSG_CHEAT_DUMP_ITEMS_DEBUG_ONLY_RESPONSE_WRITE_FILE, 0x039C}, // NF()(0x039C) //
            {Opcode.SMSG_CHEAT_PLAYER_LOOKUP, 0x03C4}, // NF()(0x03C4) //
            {Opcode.SMSG_CHECK_FOR_BOTS, 0x1574}, // (0x29A9)(0x0015) //
            {Opcode.SMSG_CLEAR_COOLDOWN, 0x9577}, // (0xCB51)(0x01DE) //
            {Opcode.SMSG_CLEAR_EXTRA_AURA_INFO_OBSOLETE, 0x03A6}, // NF()(0x03A6) //
            {Opcode.SMSG_CLEAR_FAR_SIGHT_IMMEDIATE, 0xBE7D}, // (0xEB98)(0x020D) //
            {Opcode.SMSG_CLEAR_TARGET, 0xC455}, // (0x4ADD)(0x03BF) //
            {Opcode.SMSG_CACHE_VERSION, 0xCE74}, // (0xE2B8)(0x04AB) //
            {Opcode.SMSG_CONTROL_UPDATE, 0xCD55}, // (0x4290)(0x0159) //
            {Opcode.SMSG_COMBAT_LOG_MULTIPLE, 0x8F75}, // (0x4AB4)(0x0514) //
            {Opcode.SMSG_COMMENTATOR_GET_PLAYER_INFO, 0x03BA}, // NF()(0x03BA) //
            {Opcode.SMSG_COMMENTATOR_MAP_INFO, 0x757D}, // (0xAA51)(0x03B8) //
            {Opcode.SMSG_COMMENTATOR_PLAYER_INFO, 0x7F5D}, // (0xC2B1)(0x03BB) //
            {Opcode.SMSG_COMMENTATOR_STATE_CHANGED, 0x3E5D}, // (0x4BF4)(0x03B6) //
            {Opcode.SMSG_COMPLAINT_RESULT, 0xFFAA}, // NF(0x2295)(0x03C8) //
            {Opcode.SMSG_COMPRESSED_MOVES, 0x621C}, // (0x0A54)(0x02FB) //
            {Opcode.SMSG_COMPRESSED_UPDATE_OBJECT, 0x6C7D}, // (0xCB74)(0x01F6) //
            {Opcode.SMSG_COMSAT_CONNECT_FAIL, 0x602A}, // (0x4B59)(0x03E2) //
            {Opcode.SMSG_COMSAT_DISCONNECT, 0x2A03}, // (0xCB71)(0x03E1) //
            {Opcode.SMSG_COMSAT_RECONNECT_TRY, 0x7A2A}, // (0x63F8)(0x03E0) //
            {Opcode.SMSG_CONTACT_LIST, 0x1675}, // (0x439C)(0x0067) //
            {Opcode.SMSG_CONVERT_RUNE, 0xFF9C}, // NF(0x4B75)(0x0486) //
            {Opcode.SMSG_COOLDOWN_CHEAT, 0x2C7F}, // (0x637C)(0x01E1) //
            {Opcode.SMSG_COOLDOWN_EVENT, 0x6576}, // (0xA238)(0x0135) //
            {Opcode.SMSG_CORPSE_MAP_POSITION_QUERY_RESPONSE, 0x04B7}, // NF()(0x04B7) // // SMSG}, 3*float+float
            {Opcode.SMSG_CORPSE_RECLAIM_DELAY, 0x0269}, // NF()(0x0269) //
            {Opcode.SMSG_QUERY_CREATURE_RESPONSE, 0xE45E}, // (0x83B8)(0x0061) //
            {Opcode.SMSG_CRITERIA_DELETED, 0x049E}, // NF()(0x049E) // // not changed 9626
            {Opcode.SMSG_CRITERIA_UPDATE, 0x046A}, // NF()(0x046A) //
            {Opcode.SMSG_CROSSED_INEBRIATION_THRESHOLD, 0xFFAB}, // NF(0xEBB4)(0x03C1) //
            {Opcode.SMSG_DAMAGE_CALC_LOG, 0xB657}, // (0x037D)(0x027C) //
            {Opcode.SMSG_DAMAGE_DONE_OBSOLETE, 0x014C}, // NF()(0x014C) // NOT IN CLIENT 335 AND 401
            {Opcode.SMSG_DANCE_QUERY_RESPONSE, 0x7800}, // (0xCB10)(0x0452) //
            {Opcode.SMSG_DEATH_RELEASE_LOC, 0xFFB4}, // NF(0xABF5)(0x0378) //
            {Opcode.SMSG_DEBUGAURAPROC, 0x024D}, // NF()(0x024D) //
            {Opcode.SMSG_DEBUG_LIST_TARGETS, 0x03D9}, // NF()(0x03D9) //
            {Opcode.SMSG_DEFENSE_MESSAGE, 0x6020}, // (0xA27C)(0x033A) //
            {Opcode.SMSG_DESTROY_OBJECT, 0x6F77}, // (0xE310)(0x00AA) //
            {Opcode.SMSG_DESTRUCTIBLE_BUILDING_DAMAGE, 0xEC5F}, // (0xE3D9)(0x0032) //
            {Opcode.SMSG_DISMOUNT, 0x5756}, // (0xC394)(0x03AC) //
            {Opcode.SMSG_DISMOUNT_RESULT, 0xD77D}, // (0xE39D)(0x016F) //
            {Opcode.SMSG_DISPEL_FAILED, 0xDD54}, // (0x4BB5)(0x0262) //
            {Opcode.SMSG_DUEL_COMPLETE, 0xE323}, // (0x6B74)(0x016A) //
            {Opcode.SMSG_DUEL_COUNTDOWN, 0x8A21}, // (0x8A54)(0x02B7) //
            {Opcode.SMSG_DUEL_IN_BOUNDS, 0x9B00}, // (0x2ADD)(0x0169) //
            {Opcode.SMSG_DUEL_OUT_OF_BOUNDS, 0xB829}, // (0x2350)(0x0168) //
            {Opcode.SMSG_DUEL_REQUESTED, 0x530A}, // (0xCAF1)(0x0167) //
            {Opcode.SMSG_DUEL_WINNER, 0x2329}, // (0xEB95)(0x016B) //
            {Opcode.SMSG_DUMP_OBJECTS_DATA, 0x048C}, // NF()(0x048C) //
            {Opcode.SMSG_DURABILITY_DAMAGE_DEATH, 0xE77C}, // (0x0BF0)(0x02BD) //erted with SMSG_LOG_XP_GAIN
            {Opcode.SMSG_DYNAMIC_DROP_ROLL_RESULT, 0x0469}, // NF()(0x0469) //
            {Opcode.SMSG_ECHO_PARTY_SQUELCH, 0x1303}, // (0xA3DD)(0x03F6) //
            {Opcode.SMSG_EMOTE, 0x6C5C}, // (0x2B98)(0x0103) //
            {Opcode.SMSG_ENABLE_BARBER_SHOP, 0xC55E}, // (0x82D4)(0x0427) //
            {Opcode.SMSG_ENCHANTMENT_LOG, 0xD676}, // (0x435C)(0x01D7) //
            {Opcode.SMSG_ENVIRONMENTAL_DAMAGE_LOG, 0x7455}, // (0x6254)(0x01FC) //
            {Opcode.SMSG_EQUIPMENT_SET_ID, 0xAD55}, // (0xC294)(0x0137) //
            {Opcode.SMSG_EXPECTED_SPAM_RECORDS, 0xA108}, // (0xABDD)(0x0332) //
            {Opcode.SMSG_EXPLORATION_EXPERIENCE, 0xA476}, // (0x8B58)(0x01F8) //
            {Opcode.SMSG_FEATURE_SYSTEM_STATUS, 0x5C74}, // (0xA37C)(0x03C9) //
            {Opcode.SMSG_FEIGN_DEATH_RESISTED, 0x8F54}, // (0x03D9)(0x02B4) //
            {Opcode.SMSG_FISH_ESCAPED, 0x1F77}, // (0x431D)(0x01C9) //
            {Opcode.SMSG_FISH_NOT_HOOKED, 0x3F76}, // (0xCAB5)(0x01C8) //
            {Opcode.SMSG_FLIGHT_SPLINE_SYNC, 0xB47F}, // (0xE2DC)(0x0388) //
            {Opcode.SMSG_FORCEACTIONSHOW, 0x7E77}, // (0xEBF5)(0x001B) //
            {Opcode.SMSG_FORCED_DEATH_UPDATE, 0xFFB3}, // NF(0x43BD)(0x037A) //
            {Opcode.SMSG_FORCE_DISPLAY_UPDATE, 0xE57E}, // (0x43B8)(0x0403) //
            {Opcode.SMSG_FORCE_FLIGHT_BACK_SPEED_CHANGE, 0x0383}, // NF()(0x0383) //
            {Opcode.SMSG_FORCE_FLIGHT_SPEED_CHANGE, 0x0381}, // NF()(0x0381) //
            {Opcode.SMSG_FORCE_MOVE_ROOT, 0x00E8}, // NF()(0x00E8) //
            {Opcode.SMSG_FORCE_MOVE_UNROOT, 0x00EA}, // NF()(0x00EA) //
            {Opcode.SMSG_FORCE_PITCH_RATE_CHANGE, 0x045C}, // NF()(0x045C) //
            {Opcode.SMSG_FORCE_RUN_BACK_SPEED_CHANGE, 0x00E4}, // NF()(0x00E4) //
            {Opcode.SMSG_FORCE_RUN_SPEED_CHANGE, 0x00E2}, // NF()(0x00E2) //
            {Opcode.SMSG_FORCE_SWIM_BACK_SPEED_CHANGE, 0x02DC}, // NF()(0x02DC) //
            {Opcode.SMSG_FORCE_SWIM_SPEED_CHANGE, 0x00E6}, // NF()(0x00E6) //
            {Opcode.SMSG_FORCE_TURN_RATE_CHANGE, 0x02DE}, // NF()(0x02DE) //
            {Opcode.SMSG_FORCE_WALK_SPEED_CHANGE, 0x02DA}, // NF()(0x02DA) //
            {Opcode.SMSG_FRIEND_STATUS, 0xBB22}, // (0xAB14)(0x0068) //
            {Opcode.SMSG_GAME_OBJECT_CUSTOM_ANIM, 0xA655}, // (0x8230)(0x00B3) //
            {Opcode.SMSG_GAMEOBJECT_DESPAWN_ANIM, 0x1E54}, // (0x62D4)(0x0215) //
            {Opcode.SMSG_QUERY_GAME_OBJECT_RESPONSE, 0x0577}, // (0x0231)(0x005F) //
            {Opcode.SMSG_GAME_OBJECT_RESET_STATE, 0x4655}, // (0x0274)(0x02A7) //
            {Opcode.SMSG_GAME_SPEED_SET, 0xFF75}, // (0x2354)(0x0047) //
            {Opcode.SMSG_GAMETIMEBIAS_SET, 0x0314}, // NF()(0x0314) //
            {Opcode.SMSG_GAME_TIME_SET, 0x5D77}, // (0xE2BD)(0x0045) //
            {Opcode.SMSG_GAME_TIME_UPDATE, 0xC675}, // (0xEBF0)(0x0043) //
            {Opcode.SMSG_GHOSTEE_GONE, 0x0326}, // NF()(0x0326) //
            {Opcode.SMSG_GMRESPONSE_DB_ERROR, 0x9576}, // (0x4375)(0x04EE) //
            {Opcode.SMSG_GMRESPONSE_RECEIVED, 0x0F57}, // (0x0A91)(0x04EF) //int32}, string[2000]}, string[4000][4]
            {Opcode.SMSG_GMRESPONSE_STATUS_UPDATE, 0x057C}, // (0x8B39)(0x04F1) //- EVENT_GM_SURVEY_DISPLAY}, 0 - EVENT_UPDATE_TICKET)
            {Opcode.SMSG_GM_TICKET_CREATE, 0x355D}, // (0xAA74)(0x0206) //
            {Opcode.SMSG_GM_TICKET_GET_TICKET, 0xF47F}, // (0x6A7C)(0x0212) //
            {Opcode.SMSG_GM_TICKET_GET_SYSTEM_STATUS, 0x467C}, // (0x6A51)(0x021B) //
            {Opcode.SMSG_GM_TICKET_UPDATE_TEXT, 0x5D5E}, // (0xAA79)(0x0208) //
            {Opcode.SMSG_GM_MESSAGECHAT, 0x2902}, // (0xE3B0)(0x03B3) //
            //{Opcode.SMSG_GM_TICKET_DELETE_TICKET, 0x1656}, // (0x4A90)(0x0218) //
            {Opcode.SMSG_GM_TICKET_STATUS_UPDATE, 0xBF7D}, // (0x22F4)(0x0328) //
            {Opcode.SMSG_GOD_MODE, 0x167E}, // (0x0AF5)(0x0023) //
            {Opcode.SMSG_GOGOGO_OBSOLETE, 0x03F5}, // NF()(0x03F5) //
            {Opcode.SMSG_GOSSIP_COMPLETE, 0x430B}, // (0x6BF9)(0x017E) //
            {Opcode.SMSG_GOSSIP_MESSAGE, 0xD12A}, // (0xAB3D)(0x017D) //
            {Opcode.SMSG_GOSSIP_POI, 0x1002}, // (0x0BD9)(0x0224) //
            {Opcode.SMSG_GROUP_ACTION_THROTTLED, 0xFFA4}, // NF(0xC2D5)(0x0411) //
            {Opcode.SMSG_GROUP_CANCEL, 0x5E54}, // (0xA290)(0x0071) //
            {Opcode.SMSG_GROUP_DECLINE, 0xBE5E}, // (0x8A51)(0x0074) //
            {Opcode.SMSG_GROUP_DESTROYED, 0x7E74}, // (0x83B0)(0x007C) //
            {Opcode.SMSG_GROUP_INVITE, 0x1F7F}, // (0xCBF1)(0x006F) //
            {Opcode.SMSG_GROUP_LIST, 0x8C76}, // (0x229D)(0x007D) //
            {Opcode.SMSG_GROUP_SET_LEADER, 0xED5D}, // (0x6331)(0x0079) //
            {Opcode.SMSG_GROUP_UNINVITE, 0xBC75}, // (0x2355)(0x0077) //
            {Opcode.SMSG_GUILD_BANK_QUERY_RESULTS, 0xB822}, // (0xE3D0)(0x03E8) //
            {Opcode.SMSG_GUILD_COMMAND_RESULT, 0xBE5D}, // (0x8BF4)(0x0093) //
            {Opcode.SMSG_GUILD_DECLINE, 0x375F}, // (0xABD9)(0x0086) //
            {Opcode.SMSG_GUILD_EVENT, 0x577D}, // (0x8B14)(0x0092) //
            {Opcode.SMSG_GUILD_INFO, 0x7C7F}, // (0x02DD)(0x0088) //
            {Opcode.SMSG_GUILD_INVITE, 0x6F74}, // (0xEA19)(0x0083) //
            {Opcode.SMSG_QUERY_GUILD_INFO_RESPONSE, 0x3208}, // (0xEA1D)(0x0055) //
            //{Opcode.SMSG_GUILD_RANKS, 0xFFC7}, // NF(0x4090)() //
            {Opcode.SMSG_GUILD_ROSTER, 0xFFE6}, // NF(0x1303)(0x008A) // 0x400C by callhandler.
            {Opcode.SMSG_HEALTH_UPDATE, 0xE454}, // (0xE3F8)(0x047F) //
            //{Opcode.SMSG_HIGHEST_THREAT_UPDATE, 0x3F7E}, // (0xABF8)(0x0482) //
            {Opcode.SMSG_IGNORE_DIMINISHING_RETURNS_CHEAT, 0x747E}, // (0x4A39)(0x0406) //
            {Opcode.SMSG_IGNORE_REQUIREMENTS_CHEAT, 0x7C55}, // (0x839D)(0x03A9) //
            {Opcode.SMSG_INITIALIZE_FACTIONS, 0xE674}, // (0x6AB1)(0x0122) //
            {Opcode.SMSG_SEND_KNOWN_SPELLS, 0x565D}, // (0xC2B0)(0x012A) //
            {Opcode.SMSG_INIT_EXTRA_AURA_INFO_OBSOLETE, 0x03A3}, // NF()(0x03A3) //
            {Opcode.SMSG_INIT_WORLD_STATES, 0x0D7D}, // (0x8A94)(0x02C2) //
            {Opcode.SMSG_INSPECT_RESULTS_UPDATE, 0x867D}, // (0xC2F4)(0x0115) //
            {Opcode.SMSG_INSPECT_TALENT, 0x857E}, // (0x6B3D)(0x03F4) //
            {Opcode.SMSG_INSTANCE_DIFFICULTY, 0xA55E}, // (0x0B19)(0x033B) //
            {Opcode.SMSG_INSTANCE_LOCK_WARNING_QUERY, 0xB574}, // (0x6379)(0x0147) //
            {Opcode.SMSG_INSTANCE_RESET, 0x1B28}, // (0x2B34)(0x031E) //
            {Opcode.SMSG_INSTANCE_RESET_FAILED, 0xD208}, // (0xCAB8)(0x031F) //
            {Opcode.SMSG_INSTANCE_SAVE_CREATED, 0xAC57}, // (0x2255)(0x02CB) //
            {Opcode.SMSG_INVALIDATE_DANCE, 0x9229}, // (0xC900)(0x0453) //
            {Opcode.SMSG_INVALIDATE_PLAYER, 0xFB0A}, // (0xA8C5)(0x031C) //
            {Opcode.SMSG_INVALID_PROMOTION_CODE, 0x01E7}, // NF()(0x01E7) //
            {Opcode.SMSG_INVENTORY_CHANGE_FAILURE, 0xD655}, // (0x4319)(0x0112) //
            {Opcode.SMSG_ITEM_COOLDOWN, 0xD75F}, // (0x2B58)(0x00B0) //
            {Opcode.SMSG_ITEM_ENCHANT_TIME_UPDATE, 0x3754}, // (0xC370)(0x01EB) //
            {Opcode.SMSG_ITEM_NAME_QUERY_RESPONSE, 0x02C5}, // NF()(0x02C5) //
            {Opcode.SMSG_ITEM_PUSH_RESULT, 0xDB00}, // (0x835D)(0x0166) //
            {Opcode.SMSG_ITEM_QUERY_MULTIPLE_RESPONSE, 0x0059}, // NF()(0x0059) //
            {Opcode.SMSG_ITEM_QUERY_SINGLE_RESPONSE, 0x0828}, // (0x8014)(0x0058) //
            {Opcode.SMSG_ITEM_REFUND_INFO_RESPONSE, 0x04B2}, // NF()(0x04B2) // // refund item info
            {Opcode.SMSG_ITEM_PURCHASE_REFUND_RESULT, 0x04B5}, // NF()(0x04B5) // // refund item result
            {Opcode.SMSG_QUERY_ITEM_TEXT_RESPONSE, 0xA929}, // (0x8000)(0x0244) //
            {Opcode.SMSG_ITEM_TIME_UPDATE, 0x0E5D}, // (0x2A90)(0x01EA) //
            {Opcode.SMSG_JOINED_BATTLEGROUND_QUEUE, 0x038A}, // NF()(0x038A) //
            {Opcode.SMSG_KICK_REASON, 0x3320}, // (0x4A71)(0x03C5) //
            {Opcode.SMSG_LEARNED_DANCE_MOVES, 0xF209}, // (0x22D5)(0x0455) //
            {Opcode.SMSG_LEARNED_SPELL, 0x9004}, // (0xCAFC)(0x012B) //
            {Opcode.SMSG_LEVEL_UP_INFO, 0x01D4}, // NF()(0x01D4) //
            {Opcode.SMSG_LFG_BOOT_PROPOSAL_UPDATE, 0xCC56}, // (0x8399)(0x036D) //8}, uint8}, uint8}, uint64}, uint32}, uint32}, uint32}, uint32
            {Opcode.SMSG_LFG_DISABLED, 0xE554}, // (0x8AD8)(0x0398) //
            {Opcode.SMSG_LFG_JOIN_RESULT, 0xC777}, // (0x0BB5)(0x0364) //32 unk}, uint32}, if (unk == 6) { uint8 count}, for (count) uint64 }
            {Opcode.SMSG_LFG_LFR_LIST, 0xB676}, // (0x4A95)(0x0360) //32}, uint32}, if (uint8) { uint32 count}, for (count) { uint64} }}, uint32 count2}, uint32}, for (count2) { uint64}, uint32 flags}, if (flags & 0x2) {string}}, if (flags & 0x10) {for (3) uint8}}, if (flags & 0x80) {uint64}, uint32}}}, uint32 count3}, uint32}, for (count3) {uint64}, uint32 flags}, if (flags & 0x1) {uint8}, uint8}, uint8}, for (3) uint8}, uint32}, uint32}, uint32}, uint32}, uint32}, uint32}, float}, float}, uint32}, uint32}, uint32}, uint32}, uint32}, float}, uint32}, uint32}, uint32}, uint32}, uint32}, uint32}}, if (flags&0x2) string}, if (flags&0x4) uint8}, if (flags&0x8) uint64}, if (flags&0x10) uint8}, if (flags&0x20) uint32}, if (flags&0x40) uint8}, if (flags& 0x80) {uint64}, uint32}}
            {Opcode.SMSG_LFG_OFFER_CONTINUE, 0xEC57}, // (0xCA78)(0x0293) //
            {Opcode.SMSG_LFG_PARTY_INFO, 0x867E}, // (0x03F5)(0x0372) //8}, for (uint8) uint64
            {Opcode.SMSG_LFG_PLAYER_INFO, 0x1C7F}, // (0x4370)(0x036F) //8}, for (uint8) { uint32}, uint8}, uint32}, uint32}, uint32}, uint32}, uint8}, for (uint8) {uint32,uint32}, uint32}}}, uint32}, for (uint32) {uint32,uint32}
            {Opcode.SMSG_LFG_PLAYER_REWARD, 0xEC5E}, // (0xCBBC)(0x01FF) //int8}, uint32}, uint32}, uint32}, uint32}, uint32}, uint8}, for (uint8) {uint32,uint32,uint32}
            {Opcode.SMSG_LFG_PROPOSAL_UPDATE, 0x9775}, // (0xE274)(0x0361) //32}, uint8}, uint32}, uint32}, uint8}, for (uint8) {uint32,uint8,uint8,uint8,uint8}
            {Opcode.SMSG_LFG_QUEUE_STATUS, 0x977E}, // (0x8A1C)(0x0365) //32 dungeon}, uint32 lfgtype}, uint32}, uint32}, uint32}, uint32}, uint8}, uint8}, uint8}, uint8
            {Opcode.SMSG_LFG_ROLE_CHECK_UPDATE, 0x1D75}, // (0xA3D5)(0x0363) //32}, uint8}, for (uint8) uint32}, uint8}, for (uint8) { uint64}, uint8}, uint32}, uint8}, }
            {Opcode.SMSG_LFG_ROLE_CHOSEN, 0x677C}, // (0x2251)(0x02BB) //
            {Opcode.SMSG_LFG_TELEPORT_DENIED, 0x6C55}, // (0x4B19)(0x0200) //,2,4,6;0,5,7)
            {Opcode.SMSG_LFG_UPDATE_LIST, 0x8674}, // (0x2BD1)(0x0369) //8
            {Opcode.SMSG_LFG_UPDATE_PARTY, 0xA775}, // (0x0B1D)(0x0368) //8}, if (uint8) { uint8}, uint8}, uint8}, for (3) uint8}, uint8}, if (uint8) for (uint8) uint32}, string}
            {Opcode.SMSG_LFG_UPDATE_PLAYER, 0x0C5E}, // (0x0238)(0x0367) //8}, if (uint8) { uint8}, uint8}, uint8}, uint8}, if (uint8) for (uint8) uint32}, string}
            {Opcode.SMSG_VENDOR_INVENTORY, 0x4E5E}, // (0xE375)(0x019F) //
            {Opcode.SMSG_LOAD_EQUIPMENT_SET, 0xDD7E}, // (0xCA99)(0x04BC) //ipment manager list?
            {Opcode.SMSG_LOGIN_SET_TIME_SPEED, 0x047C}, // (0x0A10)(0x0042) //
            {Opcode.SMSG_LOGIN_VERIFY_WORLD, 0xEC7C}, // (0x4A5D)(0x0236) //
            {Opcode.SMSG_LOGOUT_CANCEL_ACK, 0xCD56}, // (0xA395)(0x004F) //
            //{Opcode.SMSG_LOGOUT_COMPLETE, 0x8C7F}, // (0x8311)(0x004D) //
            {Opcode.SMSG_LOGOUT_RESPONSE, 0xFFF4}, // NF(0x63BC)(0x004C) //
            {Opcode.SMSG_LOG_XP_GAIN, 0x7202}, // (0xC3BC)(0x01D0) //or 0x0BF0 for 13164
            {Opcode.SMSG_LOOT_ALL_PASSED, 0x8E54}, // (0x83B1)(0x029E) //
            {Opcode.SMSG_LOOT_CLEAR_MONEY, 0xF77E}, // (0xA2B0)(0x0165) //
            {Opcode.SMSG_LOOT_ITEM_NOTIFY, 0x765D}, // (0xEA3D)(0x0164) //
            {Opcode.SMSG_LOOT_LIST, 0xCE7D}, // (0x0BD1)(0x03F9) //
            {Opcode.SMSG_LOOT_MASTER_LIST, 0x645C}, // (0x2A91)(0x02A4) //
            {Opcode.SMSG_LOOT_MONEY_NOTIFY, 0xD57F}, // (0x2A9C)(0x0163) //
            {Opcode.SMSG_LOOT_RELEASE, 0xB67F}, // (0x4B58)(0x0161) //
            {Opcode.SMSG_LOOT_REMOVED, 0x577C}, // (0x03B1)(0x0162) //
            {Opcode.SMSG_LOOT_RESPONSE, 0x545D}, // (0x8339)(0x0160) //
            {Opcode.SMSG_LOOT_ROLL, 0xB674}, // (0x8BDC)(0x02A2) //
            {Opcode.SMSG_LOOT_ROLL_WON, 0x757E}, // (0xE235)(0x029F) //
            {Opcode.SMSG_LOOT_SLOT_CHANGED, 0x4C7C}, // (0xC3BD)(0x04FD) //T_SLOT_CHANGED
            {Opcode.SMSG_LOOT_START_ROLL, 0x7D7C}, // (0xEB19)(0x02A1) //
            {Opcode.SMSG_LOTTERY_QUERY_RESULT_OBSOLETE, 0x0335}, // NF()(0x0335) //
            {Opcode.SMSG_LOTTERY_RESULT_OBSOLETE, 0x0337}, // NF()(0x0337) //
            {Opcode.SMSG_MAIL_LIST_RESULT, 0x3804}, // (0xABD1)(0x023B) //
            {Opcode.SMSG_MEETINGSTONE_IN_PROGRESS, 0xF57D}, // (0xE318)(0x0298) //ome UPDATE_COOLDOWN events
            {Opcode.SMSG_MEETINGSTONE_MEMBER_ADDED, 0xF77C}, // (0x43FD)(0x0299) //rrors: ERR_NOT_IN_GROUP (2,51) and ERR_NOT_IN_RAID (3,39,40)
            {Opcode.SMSG_MEETINGSTONE_SETQUEUE, 0x245F}, // (0xA215)(0x0295) //howed in console
            {Opcode.SMSG_MINIGAME_MOVE_FAILED, 0x02F9}, // NF()(0x02F9) //
            {Opcode.SMSG_MINIGAME_SETUP, 0x1824}, // (0x82FC)(0x02F6) //
            {Opcode.SMSG_MINIGAME_STATE, 0x482B}, // (0x6B9C)(0x02F7) //
            {Opcode.SMSG_MIRROR_IMAGE_COMPONENTED_DATA, 0xF675}, // (0xA3B9)(0x0402) //
            {Opcode.SMSG_MODIFY_COOLDOWN, 0x5D5D}, // (0x8AD9)(0x0491) //
            {Opcode.SMSG_MONSTER_MOVE_TRANSPORT, 0xFFBF}, // NF(0x001C)(0x02AE) //
            {Opcode.SMSG_MOTD, 0x077C}, // (0x4394)(0x033D) //
            {Opcode.SMSG_MOUNT_RESULT, 0x6E7C}, // (0x82FD)(0x016E) //
            {Opcode.SMSG_MOUNT_SPECIAL_ANIM, 0xCC5D}, // (0xAB59)(0x0172) //
            {Opcode.SMSG_MOVE_ABANDON_TRANSPORT, 0x045F}, // NF()(0x045F) //
            {Opcode.SMSG_MOVE_DISABLE_GRAVITY, 0x04CE}, // NF()(0x04CE) // // SMSG}, movement related
            {Opcode.SMSG_MOVE_ENABLE_GRAVITY, 0x04D0}, // NF()(0x04D0) // // SMSG}, movement related
            {Opcode.SMSG_MOVE_KNOCK_BACK, 0x00EF}, // NF()(0x00EF) //
            //{Opcode.SMSG_MOVE_SET_CAN_FLY, 0x675D}, // (0xEBF1)(0x0343) //
            {Opcode.SMSG_MOVE_SET_FEATHER_FALL, 0x00F2}, // NF()(0x00F2) //
            {Opcode.SMSG_MOVE_SET_FLIGHT, 0x033E}, // NF()(0x033E) //
            {Opcode.SMSG_MOVE_SET_HOVERING, 0x00F4}, // NF()(0x00F4) //
            {Opcode.SMSG_MOVE_SET_LAND_WALK, 0x00DF}, // NF()(0x00DF) //
            {Opcode.SMSG_MOVE_SET_NORMAL_FALL, 0x00F3}, // NF()(0x00F3) //
            {Opcode.SMSG_MOVE_SET_WATER_WALK, 0x00DE}, // NF()(0x00DE) //
            {Opcode.SMSG_MOVE_SPLINE_DISABLE_GRAVITY, 0x04D3}, // NF()(0x04D3) // // SMSG}, movement related
            {Opcode.SMSG_MOVE_SPLINE_ENABLE_GRAVITY, 0x04D4}, // NF()(0x04D4) // // SMSG}, movement related
            {Opcode.SMSG_MOVE_SPLINE_ROOT, 0x031A}, // NF()(0x031A) //
            {Opcode.SMSG_MOVE_SPLINE_SET_FEATHER_FALL, 0x0305}, // NF()(0x0305) //
            {Opcode.SMSG_MOVE_SPLINE_SET_FLIGHT_BACK_SPEED, 0x4F76}, // (0xAA9D)(0x0386) //
            {Opcode.SMSG_MOVE_SPLINE_SET_FLIGHT_SPEED, 0xDD5C}, // (0x0A15)(0x0385) //
            {Opcode.SMSG_MOVE_SPLINE_SET_FLYING, 0x0422}, // NF()(0x0422) //
            {Opcode.SMSG_MOVE_SPLINE_SET_HOVER, 0x0307}, // NF()(0x0307) //
            {Opcode.SMSG_MOVE_SPLINE_SET_LAND_WALK, 0x030A}, // NF()(0x030A) //
            {Opcode.SMSG_MOVE_SPLINE_SET_NORMAL_FALL, 0x0306}, // NF()(0x0306) //
            {Opcode.SMSG_MOVE_SPLINE_SET_PITCH_RATE, 0x8774}, // (0x62F5)(0x045E) //
            {Opcode.SMSG_MOVE_SPLINE_SET_RUN_BACK_SPEED, 0x245D}, // (0xEB71)(0x02FF) //
            {Opcode.SMSG_MOVE_SPLINE_SET_RUN_MODE, 0x030D}, // NF()(0x030D) //
            {Opcode.SMSG_MOVE_SPLINE_SET_RUN_SPEED, 0x055E}, // (0xEAF0)(0x02FE) //
            {Opcode.SMSG_MOVE_SPLINE_SET_SWIM_BACK_SPEED, 0x0D75}, // (0xCA34)(0x0302) //
            {Opcode.SMSG_MOVE_SPLINE_SET_SWIM_SPEED, 0x865E}, // (0x4BD8)(0x0300) //
            {Opcode.SMSG_MOVE_SPLINE_SET_TURN_RATE, 0xCD7E}, // (0x2B1C)(0x0303) //
            {Opcode.SMSG_MOVE_SPLINE_SET_WALK_BACK_SPEED, 0x3F77}, // (0xA315)(0x0301) //
            {Opcode.SMSG_MOVE_SPLINE_SET_WALK_MODE, 0x030E}, // NF()(0x030E) //
            {Opcode.SMSG_MOVE_SPLINE_SET_WATER_WALK, 0x0309}, // NF()(0x0309) //
            {Opcode.SMSG_MOVE_SPLINE_START_SWIM, 0x030B}, // NF()(0x030B) //
            {Opcode.SMSG_MOVE_SPLINE_STOP_SWIM, 0x030C}, // NF()(0x030C) //
            {Opcode.SMSG_MOVE_SPLINE_UNROOT, 0x0304}, // NF()(0x0304) //
            {Opcode.SMSG_MOVE_SPLINE_UNSET_FLYING, 0x0423}, // NF()(0x0423) //
            {Opcode.SMSG_MOVE_SPLINE_UNSET_HOVER, 0x0308}, // NF()(0x0308) //
            {Opcode.SMSG_MOVE_UNSET_CAN_FLY, 0x0344}, // NF()(0x0344) //
            {Opcode.SMSG_MOVE_UNSET_FLIGHT, 0x033F}, // NF()(0x033F) //
            {Opcode.SMSG_MOVE_UNSET_HOVERING, 0x00F5}, // NF()(0x00F5) //
            {Opcode.SMSG_MULTIPLE_PACKETS_2, 0x957E}, // (0x0B5C)(0x04CD) //dles any opcode
            {Opcode.SMSG_QUERY_PLAYER_NAME_RESPONSE, 0x4D5E}, // (0x0A14)(0x0051) //
            {Opcode.SMSG_NEW_TAXI_PATH, 0xAE5E}, // (0xA259)(0x01AF) //
            {Opcode.SMSG_NEW_WORLD, 0x38C8}, // (0xA094)(0x003E) //
            {Opcode.SMSG_NOTIFICATION, 0x620A}, // (0x0A31)(0x01CB) //
            {Opcode.SMSG_NOTIFY_DANCE, 0xE308}, // (0x223D)(0x044A) //
            {Opcode.SMSG_NOTIFY_DEST_LOC_SPELL_CAST, 0x757C}, // (0x027C)(0x048E) //
            {Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE, 0x365E}, // (0x8310)(0x0180) //
            {Opcode.SMSG_NPC_WONT_TALK, 0x0181}, // NF()(0x0181) //
            {Opcode.SMSG_OFFER_PETITION_ERROR, 0xBB2A}, // (0x8B11)(0x038F) //
            {Opcode.SMSG_ON_CANCEL_EXPECTED_RIDE_VEHICLE_AURA, 0xAE75}, // (0x0311)(0x049D) //ed 9626
            {Opcode.SMSG_ON_MONSTER_MOVE, 0xA65D}, // (0x02B0)(0x00DD) //
            {Opcode.SMSG_OPEN_CONTAINER, 0x1C5F}, // (0x6AB0)(0x0113) //
            {Opcode.SMSG_OPEN_LFG_DUNGEON_FINDER, 0xA774}, // (0x0379)(0x0515) //04 (opens dungeon finder}, probably for outdoor bosses)
            {Opcode.SMSG_OVERRIDE_LIGHT, 0x0756}, // (0xABB8)(0x0412) //
            {Opcode.SMSG_PAGE_TEXT, 0xC757}, // (0x2B79)(0x01DF) //
            {Opcode.SMSG_QUERY_PAGE_TEXT_RESPONSE, 0x775F}, // (0x8848)(0x005B) //
            {Opcode.SMSG_PARTY_COMMAND_RESULT, 0x4F7D}, // (0x4275)(0x007F) //
            {Opcode.SMSG_PARTY_KILL_LOG, 0xE775}, // (0xCA39)(0x01F5) //
            {Opcode.SMSG_PARTY_MEMBER_STATS, 0x7654}, // (0x03B5)(0x007E) //
            {Opcode.SMSG_PARTY_MEMBER_STATS_FULL, 0xC67C}, // (0xAB1C)(0x02F2) //
            {Opcode.SMSG_PAUSE_MIRROR_TIMER, 0xBE76}, // (0x0A55)(0x01DA) //
            {Opcode.SMSG_SPELL_PERIODIC_AURA_LOG, 0xF557}, // (0xC35C)(0x024E) //
            {Opcode.SMSG_PETGODMODE, 0x2C54}, // (0x8B99)(0x001D) //
            {Opcode.SMSG_PETITION_QUERY_RESPONSE, 0x7301}, // (0x286D)(0x01C7) //
            {Opcode.SMSG_PETITION_SHOW_LIST, 0x5E5C}, // (0x6BB9)(0x01BC) //
            {Opcode.SMSG_PETITION_SHOW_SIGNATURES, 0x747C}, // (0x0B9D)(0x01BF) //
            {Opcode.SMSG_PETITION_SIGN_RESULTS, 0xAF56}, // (0x4B1C)(0x01C1) //
            {Opcode.SMSG_PET_ACTION_FEEDBACK, 0xA800}, // (0x0370)(0x02C6) //
            {Opcode.SMSG_PET_ACTION_SOUND, 0x6C77}, // (0x62FC)(0x0324) //
            {Opcode.SMSG_PET_BROKEN, 0xE92B}, // (0x6A71)(0x02AF) //
            {Opcode.SMSG_PET_CAST_FAILED, 0xCD74}, // (0x0B51)(0x0138) //
            {Opcode.SMSG_PET_DISMISS_SOUND, 0x3E74}, // (0x43D5)(0x0325) //
            {Opcode.SMSG_PET_GUIDS, 0xFA08}, // (0x03F0)(0x04AA) //
            //{Opcode.SMSG_PET_LEARNED_SPELLS, 0xC47D}, // (0x2290)(0x0499) //
            {Opcode.SMSG_PET_MODE, 0xFA0B}, // (0x4A1C)(0x017A) //
            {Opcode.SMSG_PET_NAME_INVALID, 0x1457}, // (0x42D9)(0x0178) //
            {Opcode.SMSG_QUERY_PET_NAME_RESPONSE, 0xE20A}, // (0x090C)(0x0053) //
            //{Opcode.SMSG_PET_UNLEARNED_SPELLS, 0x5E5D}, // (0x2A79)(0x049A) //
            {Opcode.SMSG_PET_RENAMEABLE, 0x520A}, // (0x22F8)(0x0475) //
            {Opcode.SMSG_PET_SPELLS_MESSAGE, 0x5928}, // (0x03F0)(0x0179) //
            {Opcode.SMSG_PET_TAME_FAILURE, 0x4475}, // (0x63B1)(0x0173) //
            {Opcode.SMSG_PET_UNLEARN_CONFIRM, 0x02F1}, // NF()(0x02F1) // // Deprecated 3.x
            {Opcode.SMSG_PET_UPDATE_COMBO_POINTS, 0xD20B}, // (0x8BF5)(0x0492) //
            {Opcode.SMSG_PLAYED_TIME, 0x4108}, // ()(0x01CD) //
            {Opcode.SMSG_PLAYERBINDERROR, 0x765C}, // (0xC3FC)(0x01B6) //
            {Opcode.SMSG_PLAYER_BOUND, 0x5F77}, // (0x827D)(0x0158) //
            {Opcode.SMSG_PLAYER_DIFFICULTY_CHANGE, 0xF476}, // (0x633C)(0x020E) //
            {Opcode.SMSG_PLAYER_SKINNED, 0x2574}, // (0xC311)(0x02BC) //
            //{Opcode.SMSG_PLAYER_VEHICLE_DATA, 0xE775}, // (0xEB30)(0x04A7) //+uint32 (vehicle)
            {Opcode.SMSG_PLAY_DANCE, 0xC203}, // (0x8BBD)(0x044C) //
            {Opcode.SMSG_PLAY_MUSIC, 0x9C7F}, // (0xC2F9)(0x0277) //
            {Opcode.SMSG_PLAY_OBJECT_SOUND, 0xFC5D}, // (0x4258)(0x0278) //
            {Opcode.SMSG_PLAY_SOUND, 0xA47F}, // (0xA2D1)(0x02D2) //
            {Opcode.SMSG_PLAY_SPELL_IMPACT, 0x3D74}, // (0x4A30)(0x01F7) //
            {Opcode.SMSG_PLAY_SPELL_VISUAL, 0x055C}, // (0x63BD)(0x01F3) //
            {Opcode.SMSG_PLAY_TIME_WARNING, 0xBC74}, // (0xA3D0)(0x02F5) //
            {Opcode.SMSG_PONG, 0xB000}, // (0xC500)(0x01DD) //
            {Opcode.SMSG_POWER_UPDATE, 0xB677}, // (0xAB75)(0x0480) //
            {Opcode.SMSG_PRE_RESSURECT, 0x665F}, // (0xA231)(0x0494) //
            {Opcode.SMSG_PROC_RESIST, 0x6754}, // (0xEBB0)(0x0260) //
            {Opcode.SMSG_PROPOSE_LEVEL_GRANT, 0xB476}, // (0xA358)(0x041F) //
            {Opcode.SMSG_PUREMOUNT_CANCELLED_OBSOLETE, 0x0170}, // NF()(0x0170) // // ERR_REMOVE_FROM_PVP_QUEUE_* events
            {Opcode.SMSG_PVP_CREDIT, 0x8E57}, // (0x8BF1)(0x028C) //
            {Opcode.SMSG_QUERY_QUESTS_COMPLETED_RESPONSE, 0x0501}, // NF()(0x0501) // // response to 0x500
            {Opcode.SMSG_QUERY_TIME_RESPONSE, 0xC47D}, // (0x2290)(0x01CF) //
            {Opcode.SMSG_QUEST_GIVER_OFFER_REWARD_MESSAGE, 0xAD54}, // (0x829C)(0x018D) //
            {Opcode.SMSG_QUEST_GIVER_QUEST_COMPLETE, 0x0191}, // NF()(0x0191) //
            {Opcode.SMSG_QUEST_GIVER_QUEST_DETAILS, 0xF65C}, // (0x0AD8)(0x0188) //
            {Opcode.SMSG_QUEST_GIVER_QUEST_FAILED, 0x2E77}, // (0x6B39)(0x0192) //
            {Opcode.SMSG_QUEST_GIVER_INVALID_QUEST, 0x018F}, // NF()(0x018F) //
            {Opcode.SMSG_QUEST_GIVER_QUEST_LIST_MESSAGE, 0xFFD4}, // NF(0x2780)(0x0185) //
            {Opcode.SMSG_QUEST_GIVER_REQUEST_ITEMS, 0xC45C}, // (0x6ADD)(0x018B) //
            {Opcode.SMSG_QUEST_GIVER_STATUS, 0xAC5D}, // (0x0390)(0x0183) //
            {Opcode.SMSG_QUEST_GIVER_STATUS_MULTIPLE, 0x0418}, // NF()(0x0418) //
            {Opcode.SMSG_QUEST_CONFIRM_ACCEPT, 0x3F55}, // (0x6BFD)(0x019C) //
            {Opcode.SMSG_QUEST_FORCE_REMOVED, 0xFFC8}, // NF(0x0B18)(0x021E) //estid
            {Opcode.SMSG_QUEST_LOG_FULL, 0x0195}, // NF()(0x0195) //
            {Opcode.SMSG_QUEST_POI_QUERY_RESPONSE, 0xFFCE}, // NF(0x8AB8)(0x01E4) //
            {Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE, 0x720B}, // (0x28C4)(0x005D) //
            {Opcode.SMSG_QUEST_UPDATE_ADD_ITEM, 0x019A}, // NF()(0x019A) // NOT IN CLIENT 335 AND 401
            {Opcode.SMSG_QUEST_UPDATE_ADD_KILL, 0x157F}, // (0x0BF1)(0x0199) //
            {Opcode.SMSG_QUEST_UPDATE_ADD_PVP_CREDIT, 0x5D55}, // (0xE210)(0x046F) //
            {Opcode.SMSG_QUEST_UPDATE_COMPLETE, 0x4575}, // (0x4AF5)(0x0198) //
            {Opcode.SMSG_QUEST_UPDATE_FAILED, 0xDC5D}, // (0x0AB5)(0x0196) //
            {Opcode.SMSG_QUEST_UPDATE_FAILED_TIMER, 0x3F5D}, // (0x8BB4)(0x0197) //
            {Opcode.SMSG_RAID_GROUP_ONLY, 0x0554}, // (0x03D8)(0x0286) //
            {Opcode.SMSG_RAID_INSTANCE_INFO, 0x02CC}, // NF()(0x02CC) //
            {Opcode.SMSG_RAID_INSTANCE_MESSAGE, 0xD929}, // (0xEB78)(0x02FA) //
            {Opcode.SMSG_READY_CHECK_ERROR, 0x502A}, // (0xCB50)(0x0408) //
            {Opcode.SMSG_READ_ITEM_RESULT_FAILED, 0xCE56}, // (0xA378)(0x00AF) //
            {Opcode.SMSG_READ_ITEM_RESULT_OK, 0x4D7F}, // (0x6398)(0x00AE) //
            {Opcode.SMSG_REALM_SPLIT, 0x3454}, // (0x4270)(0x038B) //
            {Opcode.SMSG_REAL_GROUP_UPDATE, 0x8D5E}, // (0x237C)(0x0397) //
            {Opcode.SMSG_RECEIVED_MAIL, 0x2122}, // (0x4A54)(0x0285) //
            {Opcode.SMSG_CONNECT_TO, 0x9000}, // (0x8400)(0x050D) //}, uint16 port}, uint32 unk}, uint8[20] hash (ip + port}, seed=sessionkey)
            {Opcode.SMSG_REFER_A_FRIEND_FAILURE, 0x7F7C}, // (0xC259)(0x0421) //
            {Opcode.SMSG_REPORT_PVP_AFK_RESULT, 0xFFA7}, // NF(0x239D)(0x03E5) //
            {Opcode.SMSG_RESET_FAILED_NOTIFY, 0xFFB1}, // NF(0xA258)(0x0396) //
            {Opcode.SMSG_RESISTLOG, 0x01D6}, // NF()(0x01D6) //
            {Opcode.SMSG_RESPOND_INSPECT_ACHIEVEMENTS, 0xFF9E}, // NF(0xE303)(0x046C) //o before CallHandler
            {Opcode.SMSG_RESUME_COMMS, 0x0511}, // NF()(0x0511) // // not found - crash
            {Opcode.SMSG_RESURRECT_FAILED, 0xD557}, // (0xAA78)(0x0252) //
            {Opcode.SMSG_RESURRECT_REQUEST, 0xAE7D}, // (0x8B51)(0x015B) //
            {Opcode.SMSG_RESYNC_RUNES, 0xFF9B}, // NF(0x4AF0)(0x0487) //
            {Opcode.SMSG_RWHOIS, 0x3228}, // (0xC35D)(0x01FE) //
            {Opcode.SMSG_SCRIPT_MESSAGE, 0x02B6}, // NF()(0x02B6) //
            {Opcode.SMSG_SELL_ITEM, 0x2F7E}, // (0x2BB5)(0x01A1) //
            {Opcode.SMSG_MAIL_COMMAND_RESULT, 0x8B23}, // (0xE351)(0x0239) //
            {Opcode.SMSG_SEND_UNLEARN_SPELLS, 0x175E}, // (0xCB58)(0x041E) //
            {Opcode.SMSG_SERVERTIME, 0xCE55}, // (0x8AD0)(0x0049) //
            {Opcode.SMSG_SERVER_BUCK_DATA, 0x041D}, // NF()(0x041D) //
            {Opcode.SMSG_SERVER_BUCK_DATA_START, 0x04A3}, // NF()(0x04A3) // // not found
            {Opcode.SMSG_SERVER_FIRST_ACHIEVEMENT, 0xA92A}, // (0xCA10)(0x0498) //
            {Opcode.SMSG_SERVER_INFO_RESPONSE, 0x04A1}, // NF()(0x04A1) // // not found
            {Opcode.SMSG_CHAT_SERVER_MESSAGE, 0x2100}, // (0x221C)(0x0291) //
            {Opcode.SMSG_SET_EXTRA_AURA_INFO_NEED_UPDATE_OBSOLETE, 0x03A5}, // NF()(0x03A5) //
            {Opcode.SMSG_SET_EXTRA_AURA_INFO_OBSOLETE, 0x03A4}, // NF()(0x03A4) //
            {Opcode.SMSG_SET_FACTION_AT_WAR, 0x4657}, // (0x2A59)(0x0313) //
            {Opcode.SMSG_SET_FACTION_STANDING, 0x367F}, // (0xA211)(0x0124) //
            {Opcode.SMSG_SET_FACTION_VISIBLE, 0xF676}, // (0xA394)(0x0123) //
            {Opcode.SMSG_SET_FLAT_SPELL_MODIFIER, 0x2654}, // (0x4218)(0x0266) //
            {Opcode.SMSG_SET_FORCED_REACTIONS, 0x02A5}, // NF()(0x02A5) //
            {Opcode.SMSG_SET_PCT_SPELL_MODIFIER, 0x5F74}, // (0x8B74)(0x0267) //
            //{Opcode.SMSG_SET_PHASE_SHIFT, 0x047C}, // NF()(0x047C) //
            {Opcode.SMSG_SET_PLAYER_DECLINED_NAMES_RESULT, 0x2676}, // (0x8A34)(0x041A) //
            {Opcode.SMSG_SET_PROFICIENCY, 0xF555}, // (0x22D4)(0x0127) //
            {Opcode.SMSG_SET_PROJECTILE_POSITION, 0x5C56}, // (0xAB5C)(0x04BF) //t64 caster}, uint8 castId}, vector3 position
            {Opcode.SMSG_SHOW_BANK, 0x01B8}, // NF()(0x01B8) //
            {Opcode.SMSG_SHOW_MAILBOX, 0xF575}, // (0xEA14)(0x0297) //L_SHOW
            {Opcode.SMSG_SHOW_TAXI_NODES, 0xD654}, // (0x0399)(0x01A9) //
            {Opcode.SMSG_SOCKET_GEMS, 0xFF92}, // NF(0x6A90)(0x050B) // update packet?
            {Opcode.SMSG_SPELL_BREAK_LOG, 0xED7E}, // (0xEBDC)(0x014F) //
            {Opcode.SMSG_SPELL_CHANCE_PROC_LOG, 0x03AA}, // NF()(0x03AA) //
            {Opcode.SMSG_SPELL_CHANCE_RESIST_PUSHBACK, 0x0404}, // NF()(0x0404) //
            {Opcode.SMSG_SPELL_COOLDOWN, 0xE555}, // (0x2394)(0x0134) //
            {Opcode.SMSG_SPELL_DAMAGE_SHIELD, 0x855E}, // (0x22B1)(0x024F) //
            {Opcode.SMSG_SPELL_DELAYED, 0x9675}, // (0xA21C)(0x01E2) //
            {Opcode.SMSG_SPELL_DISPELL_LOG, 0x3C56}, // (0xC2D9)(0x027B) //
            {Opcode.SMSG_SPELL_ENERGIZE_LOG, 0x8C5D}, // (0x22FC)(0x0151) //
            {Opcode.SMSG_SPELL_EXECUTE_LOG, 0x1674}, // (0x43FC)(0x024C) //
            {Opcode.SMSG_SPELL_FAILED_OTHER, 0x0D54}, // (0x4BBC)(0x02A6) //
            {Opcode.SMSG_SPELL_FAILURE, 0x5657}, // (0x4298)(0x0133) //
            {Opcode.SMSG_SPELL_GO, 0xE654}, // (0x0B3C)(0x0132) //
            {Opcode.SMSG_SPELL_HEAL_LOG, 0x9D7D}, // (0x437C)(0x0150) //
            {Opcode.SMSG_SPELL_INSTAKILL_LOG, 0x3E5F}, // (0x8370)(0x032F) //
            {Opcode.SMSG_SPELL_MISS_LOG, 0x2476}, // (0x0A78)(0x024B) //
            {Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG, 0xA656}, // (0x831C)(0x0250) //
            {Opcode.SMSG_SPELL_OR_DAMAGE_IMMUNE, 0x247F}, // (0x63F5)(0x0263) //
            {Opcode.SMSG_SPELL_START, 0x7C75}, // (0xAADD)(0x0131) //
            {Opcode.SMSG_SPELL_STEAL_LOG, 0x9F74}, // (0x2235)(0x0333) //
            {Opcode.SMSG_SPELL_UPDATE_CHAIN_TARGETS, 0xAD76}, // (0xCA9C)(0x0330) //
            {Opcode.SMSG_SPIRIT_HEALER_CONFIRM, 0x7C57}, // (0x4AF4)(0x0222) //
            {Opcode.SMSG_STABLE_RESULT, 0xE300}, // ()(0x0273) //
            {Opcode.SMSG_STAND_STATE_CHANGE_FAILURE_OBSOLETE, 0x455D}, // (0x8290)(0x0261) //
            {Opcode.SMSG_STAND_STATE_UPDATE, 0x0F77}, // (0xCBD1)(0x029D) //
            {Opcode.SMSG_START_MIRROR_TIMER, 0xAD74}, // (0x6A54)(0x01D9) //
            {Opcode.SMSG_STOP_DANCE, 0x4002}, // (0xCA50)(0x044F) //
            {Opcode.SMSG_STOP_MIRROR_TIMER, 0x2E56}, // (0x2299)(0x01DB) //
            {Opcode.SMSG_SUMMON_CANCEL, 0x645E}, // (0x6B18)(0x0424) //
            {Opcode.SMSG_SUMMON_REQUEST, 0x475D}, // (0x4BF5)(0x02AB) //
            {Opcode.SMSG_SUPERCEDED_SPELLS, 0x267F}, // (0x4378)(0x012C) //
            {Opcode.SMSG_UPDATE_TALENT_DATA, 0x6676}, // (0xC251)(0x04C0) //ents related
            {Opcode.SMSG_TALENTS_INVOLUNTARILY_RESET, 0x4E54}, // (0x0215)(0x04FA) //NT_TALENTS_INVOLUNTARILY_RESET
            {Opcode.SMSG_TAXI_NODE_STATUS, 0x6F76}, // (0xA330)(0x01AB) //
            {Opcode.SMSG_TEXT_EMOTE, 0x5F57}, // (0x83D8)(0x0105) //
            {Opcode.SMSG_THREAT_CLEAR, 0x4E5F}, // (0x43DD)(0x0485) //
            {Opcode.SMSG_THREAT_REMOVE, 0x3F7E}, // (0x623D)(0x0484) //
            //{Opcode.SMSG_THREAT_UPDATE, 0x4E5F}, // (0xEB5C)(0x0483) //
            {Opcode.SMSG_TIME_SYNC_REQUEST, 0X6F5E}, // (0xA318)(0x0390) //
            {Opcode.SMSG_TITLE_EARNED, 0x420B}, // (0x0B91)(0x0373) //
            {Opcode.SMSG_TOGGLE_XP_GAIN, 0x2655}, // (0x63D0)(0x04ED) //sable XP gain console message
            {Opcode.SMSG_TOTEM_CREATED, 0xED77}, // (0x23B9)(0x0413) //
            {Opcode.SMSG_TRADE_STATUS, 0x0120}, // NF()(0x0120) //
            {Opcode.SMSG_TRADE_STATUS_EXTENDED, 0x0121}, // NF()(0x0121) //
            {Opcode.SMSG_TRAINER_BUY_FAILED, 0x257E}, // (0xE371)(0x01B4) //
            {Opcode.SMSG_TRAINER_BUY_SUCCEEDED, 0x01B3}, // NF()(0x01B3) // NOT IN CLIENT 335 AND 401
            {Opcode.SMSG_TRAINER_LIST, 0xAC7E}, // (0xE311)(0x01B1) //
            {Opcode.SMSG_TRANSFER_ABORTED, 0x0A2A}, // (0x0B55)(0x0040) //
            {Opcode.SMSG_TRANSFER_PENDING, 0x502B}, // (0x6210)(0x003F) //
            {Opcode.SMSG_TRIGGER_CINEMATIC, 0x00FA}, // NF()(0x00FA) //
            {Opcode.SMSG_TRIGGER_MOVIE, 0x0464}, // NF()(0x0464) //
            {Opcode.SMSG_TURN_IN_PETITION_RESULT, 0x2E74}, // (0xCA19)(0x01C5) //
            {Opcode.SMSG_TUTORIAL_FLAGS, 0x8657}, // (0x4B31)(0x00FD) //
            {Opcode.SMSG_UI_TIME, 0x5557}, // (0xCA31)(0x04F7) //
            {Opcode.SMSG_UNIT_SPELLCAST_START, 0xBE55}, // (0xC2F1)(0x014D) //
            {Opcode.SMSG_UNKNOWN_1240, 0xA676}, // (0x83DC)(0x04D8) //t64}, string}, doing nothing
            {Opcode.SMSG_UNKNOWN_1276, 0xFF94}, // NF(0x2AD0)(0x04FC) //ing in 10554
            {Opcode.SMSG_UNKNOWN_1295, 0x050F}, // NF()(0x050F) // // not found - disconnect
            {Opcode.SMSG_UNKNOWN_1302, 0x0516}, // NF()(0x0516) // // something with player movement (move event 58?)
            {Opcode.SMSG_UNKNOWN_1304, 0x2E5E}, // (0x4B70)(0x0518) // with player movement (move event 58?)}, speed packet
            {Opcode.SMSG_UNKNOWN_1308, 0x051C}, // NF()(0x051C) // // EVENT_COMMENTATOR_SKIRMISH_QUEUE_REQUEST
            {Opcode.SMSG_UNKNOWN_1310, 0x6D76}, // (0xEA9C)(0x051E) //ED_FORM_CANT_TRANSFORM_RIGHT_NOW or ERR_ALTERED_FORM_CAN_NEVER_TRANSFORM
            {Opcode.SMSG_UNKNOWN_1311, 0x1E76}, // (0x8B5C)(0x051F) //o transform
            {Opcode.SMSG_UNKNOWN_1312, 0x4D56}, // (0x42BC)(0x0520) //o transform
            {Opcode.SMSG_UNKNOWN_1314, 0x0522}, // NF()(0x0522) // // sets unit+4336 to value from packet
            {Opcode.SMSG_UNKNOWN_1315, 0x0523}, // NF()(0x0523) // // related to opcode 0x522
            {Opcode.SMSG_UNKNOWN_1316, 0x0524}, // NF()(0x0524) // // sets unit+4338 to value from packet
            {Opcode.SMSG_UNKNOWN_1317, 0x0525}, // NF()(0x0525) // // sets unit+4340 to value from packet
            {Opcode.SMSG_UNKNOWN_1329, 0x2D57}, // (0x02B8)(0x0531) //elated
            {Opcode.SMSG_UNLEARNED_SPELLS, 0x2456}, // (0x227D)(0x0203) //
            {Opcode.SMSG_UPDATE_ACCOUNT_DATA, 0xBE57}, // (0x61A9)(0x020C) //
            {Opcode.SMSG_UPDATE_ACCOUNT_DATA_COMPLETE, 0x7E76}, // (0x42D4)(0x0463) //
            {Opcode.SMSG_UPDATE_ACTION_BUTTONS, 0x4574}, // (0xEB74)(0x0129) //
            {Opcode.SMSG_UPDATE_COMBO_POINTS, 0x039D}, // NF()(0x039D) //
            {Opcode.SMSG_UPDATE_INSTANCE_ENCOUNTER_UNIT, 0xFFC9}, // NF(0xC08D)(0x0214) //
            {Opcode.SMSG_UPDATE_INSTANCE_OWNERSHIP, 0x8321}, // (0xCB5D)(0x032B) //
            {Opcode.SMSG_UPDATE_LAST_INSTANCE, 0x9B21}, // (0x2B91)(0x0320) //
            {Opcode.SMSG_UPDATE_OBJECT, 0xFC7D}, // (0x8BF0)(0x00A9) //
            {Opcode.SMSG_UPDATE_WORLD_STATE, 0x6D7D}, // (0xCBF8)(0x02C3) //
            {Opcode.SMSG_USERLIST_ADD, 0xBA20}, // (0xC2FC)(0x03F0) //
            {Opcode.SMSG_USERLIST_REMOVE, 0x2228}, // (0xCBB9)(0x03F1) //
            {Opcode.SMSG_USERLIST_UPDATE, 0xE30B}, // (0xAA5C)(0x03F2) //
            {Opcode.SMSG_USE_EQUIPMENT_SET_RESULT, 0xF657}, // (0x02D9)(0x04D6) //EquipmentSetResult?
            {Opcode.SMSG_VOICESESSION_FULL, 0xA456}, // (0xCB90)(0x03FC) //
            {Opcode.SMSG_VOICE_CHAT_STATUS, 0x8923}, // (0x627D)(0x03E3) //
            {Opcode.SMSG_VOICE_PARENTAL_CONTROLS, 0xFFAC}, // NF(0x6AD9)(0x03B1) //
            {Opcode.SMSG_VOICE_SESSION_ADJUST_PRIORITY, 0x03A0}, // NF()(0x03A0) //
            {Opcode.SMSG_VOICE_SESSION_ENABLE, 0x03B0}, // NF()(0x03B0) //
            {Opcode.SMSG_VOICE_SESSION_LEAVE, 0xFFAE}, // NF(0xC255)(0x039F) //
            {Opcode.SMSG_VOICE_SESSION_ROSTER_UPDATE, 0xFFAF}, // NF(0xEBD5)(0x039E) //
            {Opcode.SMSG_VOICE_SET_TALKER_MUTED, 0xFFAD}, // NF(0x8B55)(0x03A2) //
            {Opcode.SMSG_WARDEN_DATA, 0x212B}, // (0x23DD)(0x02E6) //
            {Opcode.SMSG_WEATHER, 0x777F}, // (0x2AF9)(0x02F4) //
            {Opcode.SMSG_WHO, 0xCB28}, // (0xE2D0)(0x0063) //
            {Opcode.SMSG_WHO_IS, 0x3328}, // (0x0AD9)(0x0065) //
            {Opcode.SMSG_ZONE_UNDER_ATTACK, 0xE801} // (0x6215)(0x0254) //
        };

        private static readonly BiDictionary<Opcode, int> MiscOpcodes = new BiDictionary<Opcode, int>
        {
            {Opcode.MSG_AUCTION_HELLO, 0xD574}, // (0x8371)(0x0255) //
            {Opcode.MSG_BATTLEGROUND_PLAYER_POSITIONS, 0xFFBB}, // NF(0x8772)(0x02E9) //
            {Opcode.MSG_CHANNEL_START, 0x8574}, // (0xC3D5)(0x0139) //
            {Opcode.MSG_CHANNEL_UPDATE, 0xC654}, // (0x8B70)(0x013A) //
            {Opcode.MSG_CORPSE_QUERY, 0x275E}, // (0xA3B1)(0x0216) //
            {Opcode.MSG_DELAY_GHOST_TELEPORT, 0x032E}, // NF()(0x032E) //
            {Opcode.MSG_GM_ACCOUNT_ONLINE, 0x026E}, // NF()(0x026E) //
            {Opcode.MSG_GM_BIND_OTHER, 0x01E8}, // NF()(0x01E8) //
            {Opcode.MSG_GM_CHANGE_ARENA_RATING, 0x040F}, // NF()(0x040F) //
            {Opcode.MSG_GM_DESTROY_CORPSE, 0x0310}, // NF()(0x0310) //
            {Opcode.MSG_GM_GEARRATING, 0x03B4}, // NF()(0x03B4) //
            {Opcode.MSG_GM_RESETINSTANCELIMIT, 0x033C}, // NF()(0x033C) //
            {Opcode.MSG_GM_SHOWLABEL, 0x01EF}, // NF()(0x01EF) //
            {Opcode.MSG_GM_SUMMON, 0x01E9}, // NF()(0x01E9) //
            {Opcode.MSG_GUILD_BANK_LOG_QUERY, 0x6308}, // (0xEB34)(0x03EE) //
            {Opcode.MSG_GUILD_BANK_MONEY_WITHDRAWN, 0x302A}, // (0x8B34)(0x03FE) //
            {Opcode.MSG_GUILD_EVENT_LOG_QUERY, 0xFD5E}, // (0x23B8)(0x03FF) //
            {Opcode.MSG_GUILD_PERMISSIONS, 0x2E54}, // (0x02D0)(0x03FD) //
            {Opcode.MSG_INSPECT_ARENA_TEAMS, 0x0377}, // NF()(0x0377) //
            {Opcode.MSG_INSPECT_HONOR_STATS, 0x02D6}, // NF()(0x02D6) //
            {Opcode.MSG_LIST_STABLED_PETS, 0x5E5D}, // (0x2A79)(0x026F) //
            {Opcode.MSG_MINIMAP_PING, 0x01D5}, // NF()(0x01D5) //
            {Opcode.MSG_MOVE_FALL_LAND, 0xF474}, // (0xAA58)(0x00C9) //
            {Opcode.MSG_MOVE_FEATHER_FALL, 0x3F75}, // (0x02FD)(0x02B0) //
            {Opcode.MSG_MOVE_HEARTBEAT, 0x177C}, // (0x0B38)(0x00EE) //
            {Opcode.MSG_MOVE_HOVER, 0xD575}, // (0x0331)(0x00F7) //
            {Opcode.MSG_MOVE_JUMP, 0x7477}, // (0x0A39)(0x00BB) //
            {Opcode.MSG_MOVE_KNOCK_BACK, 0x2555}, // (0xC210)(0x00F1) //
            {Opcode.MSG_MOVE_ROOT, 0x9555}, // (0x0275)(0x00EC) //
            {Opcode.MSG_MOVE_SET_ALL_SPEED_CHEAT, 0x00D6}, // NF()(0x00D6) //
            {Opcode.MSG_MOVE_SET_FACING, 0x865D}, // ()(0x00DA) //
            {Opcode.MSG_MOVE_SET_FLIGHT_BACK_SPEED, 0x5C55}, // (0xE2BC)(0x0380) //
            {Opcode.MSG_MOVE_SET_FLIGHT_BACK_SPEED_CHEAT, 0x037F}, // NF()(0x037F) //
            {Opcode.MSG_MOVE_SET_FLIGHT_SPEED, 0x5576}, // (0x0310)(0x037E) //
            {Opcode.MSG_MOVE_SET_FLIGHT_SPEED_CHEAT, 0x037D}, // NF()(0x037D) //
            {Opcode.MSG_MOVE_SET_PITCH, 0x00DB}, // NF()(0x00DB) //
            {Opcode.MSG_MOVE_SET_PITCH_RATE, 0xED76}, // (0x8274)(0x045B) //
            {Opcode.MSG_MOVE_SET_PITCH_RATE_CHEAT, 0x045A}, // NF()(0x045A) //
            {Opcode.MSG_MOVE_SET_RAW_POSITION_ACK, 0xFFDB}, // NF(0xA23C)(0x00E0) //
            {Opcode.MSG_MOVE_SET_RUN_BACK_SPEED, 0x065F}, // (0x0270)(0x00CF) //
            {Opcode.MSG_MOVE_SET_RUN_BACK_SPEED_CHEAT, 0x00CE}, // NF()(0x00CE) //
            {Opcode.MSG_MOVE_SET_RUN_MODE, 0x7D56}, // (0xE339)(0x00C2) //
            {Opcode.MSG_MOVE_SET_RUN_SPEED, 0xA454}, // (0x8379)(0x00CD) //
            {Opcode.MSG_MOVE_SET_RUN_SPEED_CHEAT, 0x00CC}, // NF()(0x00CC) //
            {Opcode.MSG_MOVE_SET_SWIM_BACK_SPEED, 0x1777}, // (0x4B51)(0x00D5) //
            {Opcode.MSG_MOVE_SET_SWIM_BACK_SPEED_CHEAT, 0x00D4}, // NF()(0x00D4) //
            {Opcode.MSG_MOVE_SET_SWIM_SPEED, 0xDD76}, // (0x6A1D)(0x00D3) //
            {Opcode.MSG_MOVE_SET_SWIM_SPEED_CHEAT, 0x00D2}, // NF()(0x00D2) //
            {Opcode.MSG_MOVE_SET_TURN_RATE, 0xAC75}, // (0xEA58)(0x00D8) //
            {Opcode.MSG_MOVE_SET_TURN_RATE_CHEAT, 0x00D7}, // NF()(0x00D7) //
            {Opcode.MSG_MOVE_SET_WALK_MODE, 0xF75D}, // (0x8A74)(0x00C3) //
            {Opcode.MSG_MOVE_SET_WALK_SPEED, 0xEF57}, // (0xEAB5)(0x00D1) //
            {Opcode.MSG_MOVE_SET_WALK_SPEED_CHEAT, 0x00D0}, // NF()(0x00D0) //
            {Opcode.MSG_MOVE_START_ASCEND, 0x0359}, // NF()(0x0359) //
            {Opcode.MSG_MOVE_START_BACKWARD, 0xCC7C}, // (0x0B50)(0x00B6) //
            {Opcode.MSG_MOVE_START_DESCEND, 0x03A7}, // NF()(0x03A7) //
            {Opcode.MSG_MOVE_START_FORWARD, 0xF576}, // (0x0B31)(0x00B5) //
            {Opcode.MSG_MOVE_START_PITCH_DOWN, 0xCE75}, // (0x2BD5)(0x00C0) // INVERSER
            {Opcode.MSG_MOVE_START_PITCH_UP, 0x0E7C}, // (0xAADC)(0x00BF) // INVERSER
            {Opcode.MSG_MOVE_START_STRAFE_LEFT, 0x5F5C}, // (0xE395)(0x00B8) //
            {Opcode.MSG_MOVE_START_STRAFE_RIGHT, 0x265C}, // (0x6BF4)(0x00B9) //
            {Opcode.MSG_MOVE_START_SWIM, 0xAE57}, // (0x62F8)(0x00CA) //
            {Opcode.MSG_MOVE_START_SWIM_CHEAT, 0x2755}, // (0x0A1C)(0x0341) //
            {Opcode.MSG_MOVE_START_TURN_LEFT, 0x945F}, // (0xAA90)(0x00BC) //
            {Opcode.MSG_MOVE_START_TURN_RIGHT, 0x6657}, // (0x4BFC)(0x00BD) //
            {Opcode.MSG_MOVE_STOP, 0x4E76}, // (0x433C)(0x00B7) //
            {Opcode.MSG_MOVE_STOP_ASCEND, 0x035A}, // NF()(0x035A) //
            {Opcode.MSG_MOVE_STOP_PITCH, 0x4E7D}, // (0x635D)(0x00C1) //
            {Opcode.MSG_MOVE_STOP_STRAFE, 0x0D7F}, // (0xA31C)(0x00BA) //
            {Opcode.MSG_MOVE_STOP_SWIM, 0xAC7D}, // (0xC290)(0x00CB) //
            {Opcode.MSG_MOVE_STOP_SWIM_CHEAT, 0x3D54}, // (0x6AF8)(0x0342) //
            {Opcode.MSG_MOVE_STOP_TURN, 0x6D54}, // (0xC39D)(0x00BE) //
            {Opcode.MSG_MOVE_TELEPORT, 0xC557}, // (0xA254)(0x00C5) //
            {Opcode.MSG_MOVE_TELEPORT_ACK, 0x365D}, // (0x6A39)(0x00C7) //
            {Opcode.MSG_MOVE_TELEPORT_CHEAT, 0xFFDD}, // NF(0xC291)(0x00C6) //
            {Opcode.MSG_MOVE_TIME_SKIPPED, 0x9656}, // (0xCA7C)(0x0319) //
            {Opcode.MSG_MOVE_TOGGLE_COLLISION_CHEAT, 0x00D9}, // NF()(0x00D9) //
            {Opcode.MSG_MOVE_TOGGLE_FALL_LOGGING, 0x00C8}, // NF()(0x00C8) // NOT IN CLIENT 335 AND 401
            {Opcode.MSG_MOVE_TOGGLE_GRAVITY_CHEAT, 0x02AD}, // NF()(0x02AD) //
            {Opcode.MSG_MOVE_TOGGLE_LOGGING, 0x00C4}, // NF()(0x00C4) // NOT IN CLIENT 335 AND 401
            {Opcode.MSG_MOVE_UNKNOWN_1234, 0x04D2}, // NF()(0x04D2) // // SMSG}, movement related
            {Opcode.MSG_MOVE_UNROOT, 0xFC55}, // (0x2338)(0x00ED) //
            //{Opcode.MSG_MOVE_UPDATE_CAN_FLY, 0x675D}, // (0xEBF1)(0x03AD) //
            {Opcode.MSG_MOVE_WATER_WALK, 0x8D7F}, // (0x23D9)(0x02B1) //
            {Opcode.MSG_MOVE_WORLDPORT_ACK, 0xFFDC}, // NF(0x6AD0)(0x00DC) //
            {Opcode.MSG_NOTIFY_PARTY_SQUELCH, 0xE574}, // (0xC39C)(0x03DF) //
            {Opcode.MSG_PARTY_ASSIGNMENT, 0x038E}, // NF()(0x038E) //
            {Opcode.MSG_PETITION_DECLINE, 0xC454}, // (0x42B0)(0x01C2) //
            {Opcode.MSG_PETITION_RENAME, 0x577F}, // (0x4A59)(0x02C1) //
            {Opcode.MSG_PVP_LOG_DATA, 0xFFBC}, // NF(0x0762)(0x02E0) //
            {Opcode.MSG_QUERY_GUILD_BANK_TEXT, 0x2322}, // (0x03DD)(0x040A) //
            {Opcode.MSG_QUERY_NEXT_MAIL_TIME, 0x7457}, // (0x0A51)(0x0284) //
            {Opcode.MSG_QUEST_PUSH_RESULT, 0x855F}, // (0xC231)(0x0276) //
            {Opcode.MSG_RAID_READY_CHECK, 0x8F76}, // (0x82D0)(0x0322) //
            {Opcode.MSG_RAID_READY_CHECK_CONFIRM, 0xB577}, // (0x2250)(0x03AE) //
            {Opcode.MSG_RAID_READY_CHECK_FINISHED, 0xFC75}, // (0x82D5)(0x03C6) //
            {Opcode.MSG_RAID_TARGET_UPDATE, 0x3C5F}, // (0x0B74)(0x0321) //
            {Opcode.MSG_RANDOM_ROLL, 0xE455}, // (0x8A5D)(0x01FB) //
            {Opcode.MSG_SAVE_GUILD_EMBLEM, 0x9D74}, // (0x0AF1)(0x01F1) //
            {Opcode.MSG_SET_DUNGEON_DIFFICULTY, 0x8654}, // (0x4B35)(0x0329) //
            {Opcode.MSG_SET_RAID_DIFFICULTY, 0x04EB}, // NF()(0x04EB) // // lua: SetRaidDifficulty
            {Opcode.MSG_TABARDVENDOR_ACTIVATE, 0xB575}, // (0x0A50)(0x01F2) //
            {Opcode.MSG_TALENT_WIPE_CONFIRM, 0xCC5E}, // (0x2A95)(0x02AA) //
            {Opcode.OBSOLETE_DROP_ITEM, 0x0110}, // NF()(0x0110) //
            {Opcode.UMSG_DELETE_GUILD_CHARTER, 0x02C0}, // NF()(0x02C0) //
            {Opcode.UMSG_UPDATE_ARENA_TEAM_OBSOLETE, 0x0E56}, // (0x8BB1)(0x034A) //
            {Opcode.UMSG_UPDATE_GROUP_MEMBERS, 0x0080}, // NF()(0x0080) //
            {Opcode.UMSG_UPDATE_GUILD, 0x0094} // NF()(0x0094) //
        };
    }
}

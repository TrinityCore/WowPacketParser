using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V5_4_2_17658
{
    public static class Opcodes_5_4_2
    {
        public static BiDictionary<Opcode, int> Opcodes()
        {
            return Opcs;
        }

        private static readonly BiDictionary<Opcode, int> Opcs = new BiDictionary<Opcode, int>
        {
            {Opcode.CMSG_AUCTION_HELLO, 0x02A5}, // 17688
            {Opcode.CMSG_AUTH_SESSION, 0x196E}, // 17688
            {Opcode.CMSG_BANKER_ACTIVATE, 0x02E0}, // 17688
            {Opcode.CMSG_BUY_BANK_SLOT, 0x01AD}, // 17688
            {Opcode.CMSG_CHAR_CREATE, 0x077B}, // 17688
            {Opcode.CMSG_CHAR_DELETE, 0x067A}, // 17688
            {Opcode.CMSG_CHAR_ENUM, 0x047C}, // 17688
            {Opcode.CMSG_CREATURE_QUERY, 0x0C4A}, // 17688
            {Opcode.CMSG_DB_QUERY_BULK, 0x0676}, // 17688
            {Opcode.CMSG_GOSSIP_HELLO, 0x02EF}, // 17688
            {Opcode.CMSG_GAMEOBJECT_QUERY, 0x08BC}, // 17688
            {Opcode.CMSG_INSPECT, 0x0206}, // 17688
            {Opcode.CMSG_LEARN_TALENT, 0x026C}, // 17688
            {Opcode.CMSG_LIST_INVENTORY, 0x08C5}, // 17688
            {Opcode.CMSG_LOAD_SCREEN, 0x0650}, // 17688
            {Opcode.CMSG_LOG_DISCONNECT, 0x19EA}, // 17688
            {Opcode.CMSG_LOGOUT_CANCEL, 0x0EE9}, // 17688
            {Opcode.CMSG_NAME_QUERY, 0x05F4}, // 17688
            {Opcode.CMSG_NPC_TEXT_QUERY, 0x006C}, // 17688
            {Opcode.CMSG_OBJECT_UPDATE_FAILED, 0x0A95}, // 17688
            {Opcode.CMSG_PING, 0x18E2}, // 17688
            {Opcode.CMSG_PLAYER_LOGIN, 0x0754}, // 17688
            {Opcode.CMSG_RANDOMIZE_CHAR_NAME, 0x0DD1}, // 17688
            {Opcode.CMSG_READY_FOR_ACCOUNT_DATA_TIMES, 0x047F}, // 17688
            {Opcode.CMSG_REALM_QUERY, 0x0472}, // 17688
            {Opcode.CMSG_SET_ACTION_BUTTON, 0x0D5E}, // 17688
            {Opcode.CMSG_SET_SELECTION, 0x0AC5}, // 17688
            {Opcode.CMSG_TIME_SYNC_RESP, 0x06AB}, // 17688
            {Opcode.CMSG_VIOLENCE_LEVEL, 0x0448}, // 17688
            {Opcode.CMSG_WHO, 0x0CFD},
            {Opcode.CMSG_ADD_IGNORE, 0x1A01},
            {Opcode.CMSG_ATTACKSTOP, 0x0AC4},
            {Opcode.CMSG_ATTACKSWING, 0x0E4B},
            {Opcode.CMSG_AUCTION_HELLO, 0x02A5},
            {Opcode.CMSG_AUTOEQUIP_ITEM, 0x00D0},
            {Opcode.CMSG_BATTLEFIELD_LEAVE, 0x0226},
            {Opcode.CMSG_BUYBACK_ITEM, 0x0AE2},
            {Opcode.CMSG_BUY_BANK_SLOT, 0x01AD},
            {Opcode.CMSG_BUY_ITEM, 0x0189},
            {Opcode.CMSG_CANCEL_AURA, 0x0A1C},
            {Opcode.CMSG_CANCEL_CAST, 0x0839},
            {Opcode.CMSG_CAST_SPELL, 0x0A57},
            // {Opcode.CMSG_CHALLANGES_INFO, 0x0599}, 
            {Opcode.CMSG_CHANNEL_PASSWORD, 0x1313},
            // {Opcode.CMSG_CLOSE_BROWSER, 0x0C7E},
            {Opcode.CMSG_DEL_FRIEND, 0x1A60},
            {Opcode.CMSG_DEL_IGNORE, 0x1DE9},
            {Opcode.CMSG_EMOTE, 0x02C9},
            {Opcode.CMSG_EQUIPMENT_SET_DELETE, 0x0ACA},
            {Opcode.CMSG_EQUIPMENT_SET_SAVE, 0x0CC3},
            {Opcode.CMSG_EQUIPMENT_SET_USE, 0x0A2A},
            {Opcode.CMSG_GOSSIP_SELECT_OPTION, 0x0ECA},
            {Opcode.CMSG_JOIN_CHANNEL, 0x0847},
            {Opcode.CMSG_LEAVE_CHANNEL, 0x0F80},
            {Opcode.CMSG_LFG_GET_STATUS, 0xFFFF},
            {Opcode.CMSG_LFG_JOIN, 0x045F},
            {Opcode.CMSG_LOGOUT_REQUEST, 0x0183},
            {Opcode.CMSG_MESSAGECHAT_AFK, 0x0C20},
            {Opcode.CMSG_MESSAGECHAT_DND, 0x0000},
            {Opcode.CMSG_MESSAGECHAT_GUILD, 0x0243},
            {Opcode.CMSG_MESSAGECHAT_SAY, 0x0866},
            {Opcode.CMSG_MESSAGECHAT_WHISPER, 0x0D8C},
            {Opcode.CMSG_MESSAGECHAT_YELL, 0x03AB},
            // {Opcode.CMSG_OPEN_BROWSER, 0x04F4},
            {Opcode.CMSG_PETITION_SHOW_SIGNATURES, 0x00A5},
            // {Opcode.CMSG_PET_BATTLE_QUEUE_JOIN, 0x0A3D},
            // {Opcode.CMSG_PET_BATTLE_QUEUE_LEAVE, 0x081F},
            {Opcode.CMSG_REALM_SPLIT, 0x07D4},
            // {Opcode.CMSG_REALM_NAME_QUERY, 0x0472},
            {Opcode.CMSG_REPAIR_ITEM, 0x0AED},
            {Opcode.CMSG_REQUEST_HOTFIX, 0x0676},
            {Opcode.CMSG_RESET_INSTANCES, 0x0DFA},
            // {Opcode.CMSG_SELECT_LOOT_SPEC, 0x00EF},
            {Opcode.CMSG_SELL_ITEM, 0x0EEB},
            // {Opcode.CMSG_SET_DUNGEON_DIFFICULTY, 0x06D3},
            {Opcode.CMSG_SET_PRIMARY_TALENT_TREE, 0x085F},
            {Opcode.CMSG_SET_TITLE, 0x08D8},
            // {Opcode.CMSG_SHOP_CATEGORY, 0x0676},
            {Opcode.CMSG_SWAP_INV_ITEM, 0x00B9},
            {Opcode.CMSG_SWAP_ITEM, 0x012B},
            {Opcode.CMSG_TEXT_EMOTE, 0x0E0C},
            {Opcode.CMSG_TOGGLE_PVP, 0x0C4E},
            {Opcode.CMSG_TRAINER_LIST, 0x0CEA},
            {Opcode.CMSG_USE_ITEM, 0x0A9E},
            {Opcode.CMSG_WORLD_STATE_UI_TIMER_UPDATE, 0x0574},

            {Opcode.MSG_CHANNEL_UPDATE, 0x1C04},
            {Opcode.MSG_MOVE_FALL_LAND, 0x00BC},
            {Opcode.MSG_MOVE_HEARTBEAT, 0x04AD},
            {Opcode.MSG_MOVE_JUMP, 0x042F},
            {Opcode.MSG_MOVE_SET_FACING, 0x04AF},
            {Opcode.MSG_MOVE_SET_PITCH, 0x0689},
            {Opcode.MSG_MOVE_SET_RUN_MODE, 0x0426},
            {Opcode.MSG_MOVE_SET_WALK_MODE, 0x0625},
            {Opcode.MSG_MOVE_START_ASCEND, 0x0688},
            {Opcode.MSG_MOVE_START_BACKWARD, 0x02B6},
            {Opcode.MSG_MOVE_START_DESCEND, 0x062A},
            {Opcode.MSG_MOVE_START_FORWARD, 0x112B},
            {Opcode.MSG_MOVE_START_PITCH_DOWN, 0x062B},
            {Opcode.MSG_MOVE_START_PITCH_UP, 0x00B6},
            {Opcode.MSG_MOVE_START_STRAFE_LEFT, 0x0626},
            {Opcode.MSG_MOVE_START_STRAFE_RIGHT, 0x048A},
            {Opcode.MSG_MOVE_START_SWIM, 0x007E},
            {Opcode.MSG_MOVE_START_TURN_LEFT, 0x0409},
            {Opcode.MSG_MOVE_START_TURN_RIGHT, 0x04A0},
            {Opcode.MSG_MOVE_STOP, 0x0609},
            {Opcode.MSG_MOVE_STOP_ASCEND, 0x06AA},
            {Opcode.MSG_MOVE_STOP_PITCH, 0x00F5},
            {Opcode.MSG_MOVE_STOP_STRAFE, 0x027C},
            {Opcode.MSG_MOVE_STOP_SWIM, 0x027F},
            {Opcode.MSG_MOVE_STOP_TURN, 0x0488},
            {Opcode.MSG_MOVE_TELEPORT, 0x0EC5},
            {Opcode.MSG_MOVE_TOGGLE_COLLISION_CHEAT, 0x0037},
            {Opcode.MSG_PETITION_RENAME, 0x0D73},
            {Opcode.MSG_SET_DUNGEON_DIFFICULTY, 0x1885},
            {Opcode.MSG_SET_RAID_DIFFICULTY, 0x1885},
            {Opcode.MSG_VERIFY_CONNECTIVITY, 0x4F57},

            {Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x0899}, // 17688
            {Opcode.SMSG_AUTH_CHALLENGE, 0x0C42}, // 17688
            {Opcode.SMSG_AUTH_RESPONSE, 0x03A8}, // 17688
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x0E85}, // 17688
            {Opcode.SMSG_DB_REPLY, 0x089A}, // 17688
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE, 0x08F3}, // 17688
            {Opcode.SMSG_CHAR_CREATE, 0x0FAD}, // 17688
            {Opcode.SMSG_CHAR_DELETE, 0x0A1E}, // 17688
            {Opcode.SMSG_CHAR_ENUM, 0x08B9}, // 17688
            {Opcode.SMSG_INITIAL_SPELLS, 0x0623}, // 17688
            {Opcode.SMSG_LIST_INVENTORY, 0x0ADC}, // 17688
            {Opcode.SMSG_LOGIN_VERIFY_WORLD, 0x0E04}, // 17688
            {Opcode.SMSG_LOGOUT_COMPLETE, 0x0C6D}, // 17688
            {Opcode.SMSG_LOGOUT_RESPONSE, 0x104A}, // 17688
            {Opcode.SMSG_MOTD, 0x08BB}, // 17688
            {Opcode.SMSG_NPC_TEXT_UPDATE, 0x0877}, // 17688
            {Opcode.SMSG_RANDOMIZE_CHAR_NAME, 0x0D24}, // 17688
            {Opcode.SMSG_UPDATE_OBJECT, 0x1C89}, // 17688
            {Opcode.SMSG_ACTION_BUTTONS, 0x0E2C},
            {Opcode.SMSG_AUCTION_HELLO, 0x0A5C},
            {Opcode.SMSG_AURA_UPDATE, 0x0400},
            {Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_INVITE, 0x0A1A},
            // {Opcode.SMSG_CHALLANGES_INFO, 0x0A71},
            {Opcode.SMSG_CLIENTCACHE_VERSION, 0x0A4C},
            // {Opcode.SMSG_CLOSE_BROWSER, 0x084F},
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x0E85},
            {Opcode.SMSG_DESTROY_OBJECT, 0x0A75},
            {Opcode.SMSG_EQUIPMENT_SET_LIST, 0x0A99},
            {Opcode.SMSG_GOSSIP_COMPLETE, 0x0293},
            {Opcode.SMSG_GOSSIP_MESSAGE, 0x1736},
            {Opcode.SMSG_GOSSIP_POI, 0x1D8D},
            {Opcode.SMSG_JOINED_BATTLEGROUND_QUEUE, 0x0835},
            {Opcode.SMSG_LEARNED_SPELL, 0x0C6E},
            {Opcode.SMSG_LOGIN_SETTIMESPEED, 0x004A},
            {Opcode.SMSG_LOG_XPGAIN, 0x0068},
            {Opcode.SMSG_MESSAGECHAT, 0x0A5B},
            {Opcode.SMSG_MONSTER_MOVE, 0x0256},
            {Opcode.SMSG_MOVE_SET_CAN_FLY, 0x06EB},
            {Opcode.SMSG_MOVE_SET_FLIGHT_SPEED, 0x0207},
            {Opcode.SMSG_MOVE_SET_RUN_SPEED, 0x0183},
            {Opcode.SMSG_MOVE_SET_SWIM_SPEED, 0x0579},
            {Opcode.SMSG_MOVE_SET_WALK_SPEED, 0x08C9},
            {Opcode.SMSG_MOVE_UNSET_CAN_FLY, 0x06C4},
            {Opcode.SMSG_NAME_QUERY_RESPONSE, 0x0E48},
            {Opcode.SMSG_NEW_WORLD, 0x0ABF},
            // {Opcode.SMSG_OPEN_BROWSER, 0x0C6B},
            {Opcode.SMSG_PETITION_SHOW_SIGNATURES, 0x0838},
            {Opcode.SMSG_PET_BATTLE_QUEUE_STATUS, 0x000C},
            {Opcode.SMSG_PLAYER_MOVE, 0x0EC4},
            // {Opcode.SMSG_REALM_NAME_QUERY_RESPONSE, 0x0867},
            // {Opcode.SMSG_SET_DUNGEON_DIFFICULTY, 0x0C24},
            // {Opcode.SMSG_SHOP_BUY, 0x1042},
            {Opcode.SMSG_SPELL_GO, 0x061F},
            {Opcode.SMSG_SPELL_START, 0x003E},
            {Opcode.SMSG_TALENTS_INVOLUNTARILY_RESET, 0x11CA},
            {Opcode.SMSG_TIME_SYNC_REQ, 0x0F05},
            {Opcode.SMSG_TRAINER_LIST, 0x026D},
            {Opcode.SMSG_TRANSFER_PENDING, 0x0819},
            {Opcode.SMSG_TRIGGER_CINEMATIC, 0x1320},
            {Opcode.SMSG_TUTORIAL_FLAGS, 0x138D},
            {Opcode.SMSG_WHO, 0x0872},
            {Opcode.SMSG_WHOIS, 0x050E}
        };
    }
}

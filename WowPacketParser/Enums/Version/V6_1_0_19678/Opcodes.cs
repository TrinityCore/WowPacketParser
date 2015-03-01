using System;
using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V6_1_0_19678
{
    public static class Opcodes_6_1_0
    {
        public static BiDictionary<Opcode, int> Opcodes(Direction direction)
        {
            switch (direction)
            {
                case Direction.ClientToServer:
                case Direction.BNClientToServer:
                    return ClientOpcodes;
                case Direction.ServerToClient:
                case Direction.BNServerToClient:
                    return ServerOpcodes;
            }
            return MiscOpcodes;
        }

        private static readonly BiDictionary<Opcode, int> ClientOpcodes = new BiDictionary<Opcode, int>
        {
            {Opcode.CMSG_ATTACKSTOP, 0x0853},
            {Opcode.CMSG_ATTACKSWING, 0x048B},
            {Opcode.CMSG_AUTH_CONTINUED_SESSION, 0x1A72},
            {Opcode.CMSG_AUTH_SESSION, 0x1872},
            {Opcode.CMSG_BATTLE_PAY_GET_PURCHASE_LIST_QUERY, 0x11AA},
            {Opcode.CMSG_CANCEL_AURA, 0x084B},
            {Opcode.CMSG_CANCEL_TRADE, 0x1114},
            {Opcode.CMSG_CAST_SPELL, 0x1B02},
            {Opcode.CMSG_CHAR_CREATE, 0x133A},
            {Opcode.CMSG_CHAR_ENUM, 0x19E3},
            {Opcode.CMSG_CREATURE_QUERY, 0x007C},
            {Opcode.CMSG_DB_QUERY_BULK, 0x1731},
            {Opcode.CMSG_GAMEOBJECT_QUERY, 0x021A},
            {Opcode.CMSG_GET_UNDELETE_COOLDOWN_STATUS, 0x196A},
            {Opcode.CMSG_GUILD_QUERY, 0x19B3},
            {Opcode.CMSG_JOIN_CHANNEL, 0x0C6C},
            {Opcode.CMSG_LOAD_SCREEN, 0x13E4},
            {Opcode.CMSG_LOG_DISCONNECT, 0x1432},
            {Opcode.CMSG_MESSAGECHAT_AFK, 0x180C},
            {Opcode.CMSG_MESSAGECHAT_DND, 0x0479},
            {Opcode.CMSG_MESSAGECHAT_EMOTE, 0x0C59},
            {Opcode.CMSG_MESSAGECHAT_GUILD, 0x1A4B},
            {Opcode.CMSG_MESSAGECHAT_OFFICER, 0x1243},
            {Opcode.CMSG_MESSAGECHAT_SAY, 0x140B},
            {Opcode.CMSG_MESSAGECHAT_WHISPER, 0x100C},
            {Opcode.CMSG_MESSAGECHAT_YELL, 0x1481},
            {Opcode.CMSG_MOVE_FALL_LAND, 0x0DEA},
            {Opcode.CMSG_MOVE_JUMP, 0x0BCC},
            {Opcode.CMSG_MOVE_START_BACKWARD, 0x0389},
            {Opcode.CMSG_MOVE_START_FORWARD, 0x01EB},
            {Opcode.CMSG_MOVE_START_STRAFE_LEFT, 0x03D2},
            {Opcode.CMSG_MOVE_START_STRAFE_RIGHT, 0x01CB},
            {Opcode.CMSG_MOVE_START_TURN_LEFT, 0x0189},
            {Opcode.CMSG_MOVE_START_TURN_RIGHT, 0x0DAA},
            {Opcode.CMSG_MOVE_STOP, 0x0892},
            {Opcode.CMSG_MOVE_STOP_TURN, 0x05E9},
            {Opcode.CMSG_PING, 0x167B},
            {Opcode.CMSG_PLAYER_LOGIN, 0x1D31},
            {Opcode.CMSG_QUEUED_MESSAGES_END, 0x147B},
            {Opcode.CMSG_ROUTER_CLIENT_LOG_STREAMING_ERROR, 0x1439},
            {Opcode.CMSG_SET_SELECTION, 0x0DC4},
            {Opcode.CMSG_SUSPEND_COMMS_ACK, 0x123C},
            {Opcode.CMSG_TIME_SYNC_RESPONSE, 0x0B8C},
            {Opcode.CMSG_VIOLENCE_LEVEL, 0x0071},
            {Opcode.CMSG_WARDEN_DATA, 0x11E3},
        };

        private static readonly BiDictionary<Opcode, int> ServerOpcodes = new BiDictionary<Opcode, int>
        {
            {Opcode.SMSG_ACCOUNT_CRITERIA_UPDATE, 0x0A7A},
            {Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x15F3},
            {Opcode.SMSG_ACCOUNT_MOUNT_UPDATE, 0x19A2},
            {Opcode.SMSG_ACCOUNT_TOYS_UPDATE, 0x1F1C},
            {Opcode.SMSG_ACTION_BUTTONS, 0x153B},
            {Opcode.SMSG_ADDON_INFO, 0x1F5C},
            {Opcode.SMSG_AI_REACTION, 0x1BDA},
            {Opcode.SMSG_ALL_ACCOUNT_CRITERIA, 0x17AC},
            {Opcode.SMSG_ALL_ACHIEVEMENT_DATA, 0x097A},
            {Opcode.SMSG_ATTACKERSTATEUPDATE, 0x13DC},
            {Opcode.SMSG_ATTACKSTART, 0x19A4},
            {Opcode.SMSG_AURA_UPDATE, 0x070A},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x0403},
            {Opcode.SMSG_AUTH_RESPONSE, 0x0B61},
            {Opcode.SMSG_BATTLE_PAY_GET_DISTRIBUTION_LIST_RESPONSE, 0x17A3},
            {Opcode.SMSG_BATTLE_PAY_GET_PURCHASE_LIST_RESPONSE, 0x1FC9},
            {Opcode.SMSG_BATTLE_PET_JOURNAL_LOCK_ACQUIRED, 0x13A9},
            {Opcode.SMSG_BINDPOINTUPDATE, 0x156C},
            {Opcode.SMSG_CHAR_ENUM, 0x13F2},
            {Opcode.SMSG_CHUNKED_PACKET, 0x0C23},
            {Opcode.SMSG_CLIENTCACHE_VERSION, 0x116C},
            {Opcode.SMSG_COMPRESSED_PACKET, 0x0689},
            {Opcode.SMSG_CONTACT_LIST, 0x15A1},
            {Opcode.SMSG_CORPSE_RECLAIM_DELAY, 0x1B9C},
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x0DC2},
            {Opcode.SMSG_DB_REPLY, 0x097C},
            {Opcode.SMSG_DISPLAY_PROMOTION, 0x01E2},
            {Opcode.SMSG_EQUIPMENT_SET_LIST, 0x111A},
            {Opcode.SMSG_FEATURE_SYSTEM_STATUS, 0x13F3},
            {Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN, 0x117A},
            {Opcode.SMSG_FINAL_CHUNK, 0x0C14},
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE, 0x1559},
            {Opcode.SMSG_GUILD_QUERY_RESPONSE, 0x06F3},
            {Opcode.SMSG_HOTFIX_NOTIFY_BLOB, 0x19B9},
            {Opcode.SMSG_INITIALIZE_FACTIONS, 0x06C3},
            {Opcode.SMSG_INITIAL_SPELLS, 0x12C2},
            {Opcode.SMSG_INIT_WORLD_STATES, 0x11F9},
            {Opcode.SMSG_LEARNED_SPELLS, 0x08E9},
            {Opcode.SMSG_LOAD_CUF_PROFILES, 0x17EA},
            {Opcode.SMSG_LOGIN_SETTIMESPEED, 0x1573},
            {Opcode.SMSG_LOGIN_VERIFY_WORLD, 0x0B31},
            {Opcode.SMSG_MESSAGECHAT, 0x1472},
            {Opcode.SMSG_MOTD, 0x12FB},
            {Opcode.SMSG_MOVE_UPDATE, 0x1514},
            {Opcode.SMSG_MULTIPLE_PACKETS, 0x0C33},
            {Opcode.SMSG_NAME_QUERY_RESPONSE, 0x11A9},
            {Opcode.SMSG_ON_MONSTER_MOVE, 0x0B09},
            {Opcode.SMSG_PONG, 0x0C34},
            {Opcode.SMSG_POWER_UPDATE, 0x1B0A},
            {Opcode.SMSG_PVP_SEASON, 0x13A1},
            {Opcode.SMSG_REDIRECT_CLIENT, 0x0413},
            {Opcode.SMSG_RESUME_COMMS, 0x068A},
            {Opcode.SMSG_SEND_SPELL_CHARGES, 0x0CEB},
            {Opcode.SMSG_SEND_SPELL_HISTORY, 0x168C},
            {Opcode.SMSG_SEND_UNLEARN_SPELLS, 0x16CC},
            {Opcode.SMSG_SETUP_CURRENCY, 0x0969},
            {Opcode.SMSG_SET_PHASE_SHIFT_CHANGE, 0x17F9},
            {Opcode.SMSG_SET_PROFICIENCY, 0x092A},
            {Opcode.SMSG_SET_TIME_ZONE_INFORMATION, 0x15B4},
            {Opcode.SMSG_SET_VIGNETTE, 0x11E4},
            {Opcode.SMSG_SPELL_GO, 0x1281},
            {Opcode.SMSG_SPELL_START, 0x0629},
            {Opcode.SMSG_SUSPEND_COMMS, 0x068B},
            {Opcode.SMSG_SUSPEND_TOKEN, 0x0DA2},
            {Opcode.SMSG_TALENTS_INFO, 0x04C4},
            {Opcode.SMSG_TIME_SYNC_REQ, 0x0A01},
            {Opcode.SMSG_TUTORIAL_FLAGS, 0x0A39},
            {Opcode.SMSG_UI_TIME, 0x05E3},
            {Opcode.SMSG_UNDELETE_COOLDOWN_STATUS_RESPONSE, 0x1DDB},
            {Opcode.SMSG_UPDATE_OBJECT, 0x1762},
            {Opcode.SMSG_UPDATE_WORLD_STATE, 0x15BA},
            {Opcode.SMSG_WARDEN_DATA, 0x110A},
            {Opcode.SMSG_WEATHER, 0x0939},
            {Opcode.SMSG_WEEKLY_SPELL_USAGE, 0x0CEC},
            {Opcode.SMSG_WORLD_SERVER_INFO, 0x0864},
        };

        private static readonly BiDictionary<Opcode, int> MiscOpcodes = new BiDictionary<Opcode, int>();
    }
}

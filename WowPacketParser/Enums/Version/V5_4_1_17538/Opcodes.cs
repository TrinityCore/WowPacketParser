using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V5_4_1_17538
{
    public static class Opcodes_5_4_1
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
            {Opcode.CMSG_ADD_FRIEND, 0x0112},
            {Opcode.CMSG_ADD_IGNORE, 0x0922},
            {Opcode.CMSG_AREA_TRIGGER, 0x1376},
            {Opcode.CMSG_AUTH_SESSION, 0x14DA},
            {Opcode.CMSG_BATTLEFIELD_LIST, 0x1757},
            {Opcode.CMSG_CAST_SPELL, 0x127D},
            {Opcode.CMSG_CHAT_CHANNEL_LIST, 0x1178},
            {Opcode.CMSG_CHAR_DELETE, 0x09C0},
            {Opcode.CMSG_ENUM_CHARACTERS, 0x0848},
            {Opcode.CMSG_QUERY_CREATURE, 0x1647},
            {Opcode.CMSG_DB_QUERY_BULK, 0x01E4},
            {Opcode.CMSG_DESTROY_ITEM, 0x16CF},
            {Opcode.CMSG_QUERY_GAME_OBJECT, 0x1677},
            {Opcode.CMSG_GOSSIP_HELLO, 0x025C},
            {Opcode.CMSG_GOSSIP_SELECT_OPTION, 0x03EE},
            {Opcode.CMSG_GROUP_INVITE, 0x0144},
            {Opcode.CMSG_LEARN_TALENT, 0x1776},
            {Opcode.CMSG_LOADING_SCREEN_NOTIFY, 0x1148},
            {Opcode.CMSG_LOGOUT_REQUEST, 0x03EC},
            {Opcode.CMSG_LOG_DISCONNECT, 0x14FA},
            {Opcode.CMSG_CHAT_MESSAGE_CHANNEL, 0x01DD},
            {Opcode.CMSG_CHAT_MESSAGE_SAY, 0x14FC},
            {Opcode.CMSG_CHAT_MESSAGE_WHISPER, 0x14D8},
            {Opcode.CMSG_CHAT_MESSAGE_YELL, 0x045C},
            {Opcode.CMSG_QUERY_NPC_TEXT, 0x17CF},
            {Opcode.CMSG_OBJECT_UPDATE_FAILED, 0x1A44},
            {Opcode.CMSG_PING, 0x11E6},
            {Opcode.CMSG_PLAYER_LOGIN, 0x01E1},
            {Opcode.CMSG_QUERY_PLAYER_NAME, 0x11E9},
            {Opcode.CMSG_GENERATE_RANDOM_CHARACTER_NAME, 0x184C},
            {Opcode.CMSG_REALM_QUERY, 0x09E1},
            {Opcode.CMSG_REALM_SPLIT, 0x0449},
            {Opcode.CMSG_SET_ACTION_BUTTON, 0x014C},
            {Opcode.CMSG_SET_ACTIVE_MOVER, 0x1A4D},
            {Opcode.CMSG_SET_SELECTION, 0x07CD},
            {Opcode.CMSG_SET_SPECIALIZATION, 0x17DF},
            {Opcode.CMSG_VIOLENCE_LEVEL, 0x13CD},
            {Opcode.CMSG_WHO, 0x1568}
        };

        private static readonly BiDictionary<Opcode, int> ServerOpcodes = new BiDictionary<Opcode, int>
        {
            {Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x1486},
            {Opcode.SMSG_ADDON_INFO, 0x1136},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x0C5D},
            {Opcode.SMSG_AUTH_RESPONSE, 0x0D05},
            {Opcode.SMSG_BATTLEFIELD_LIST, 0x09B7},
            {Opcode.SMSG_BATTLE_PET_JOURNAL, 0x0585},
            {Opcode.SMSG_BIND_POINT_UPDATE, 0x0517},
            {Opcode.SMSG_CHANNEL_NOTIFY, 0x1490},
            {Opcode.SMSG_CHAT, 0x14AC},
            {Opcode.SMSG_CREATE_CHAR, 0x1007},
            {Opcode.SMSG_DELETE_CHAR, 0x0017},
            {Opcode.SMSG_ENUM_CHARACTERS_RESULT, 0x040E},
            {Opcode.SMSG_CACHE_VERSION, 0x1037},
            {Opcode.SMSG_QUERY_CREATURE_RESPONSE, 0x011C},
            {Opcode.SMSG_DB_REPLY, 0x1406},
            {Opcode.SMSG_DEFENSE_MESSAGE, 0x0D14},
            {Opcode.SMSG_DESTROY_OBJECT, 0x04B4},
            {Opcode.SMSG_EMOTE, 0x0D8B},
            {Opcode.SMSG_QUERY_GAME_OBJECT_RESPONSE, 0x0916},
            {Opcode.SMSG_GOSSIP_MESSAGE, 0x03FC},
            {Opcode.SMSG_GROUP_INVITE, 0x09A6},
            {Opcode.SMSG_GROUP_LIST, 0x01B5},
            {Opcode.SMSG_SEND_KNOWN_SPELLS, 0x1164},
            {Opcode.SMSG_LEARNED_SPELLS, 0x118E},
            {Opcode.SMSG_VENDOR_INVENTORY, 0x08BD},
            {Opcode.SMSG_LOGIN_SET_TIME_SPEED, 0x0D17},
            {Opcode.SMSG_LOGOUT_COMPLETE, 0x0D95},
            {Opcode.SMSG_MOTD, 0x04AC},
            {Opcode.SMSG_MOVE_SET_CAN_FLY, 0x0209},
            {Opcode.SMSG_MOVE_UNSET_CAN_FLY, 0x031B},
            {Opcode.SMSG_QUERY_PLAYER_NAME_RESPONSE, 0x1407},
            {Opcode.SMSG_NEW_WORLD, 0x010F},
            {Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE, 0x101F},
            {Opcode.SMSG_QUERY_PAGE_TEXT_RESPONSE, 0x0D2E},
            {Opcode.SMSG_PONG, 0x005D},
            {Opcode.SMSG_REALM_QUERY_RESPONSE, 0x052D},
            {Opcode.SMSG_REALM_SPLIT, 0x0884},
            {Opcode.SMSG_SEND_UNLEARN_SPELLS, 0x049E},
            {Opcode.SMSG_SET_PROFICIENCY, 0x05B6},
            {Opcode.SMSG_SET_TIME_ZONE_INFORMATION, 0x14AF},
            {Opcode.SMSG_SPELL_GO, 0x14EC},
            {Opcode.SMSG_SPELL_START, 0x0CCC},
            {Opcode.SMSG_UPDATE_ACTION_BUTTONS, 0x0406},
            {Opcode.SMSG_UPDATE_TALENT_DATA, 0x0494},
            {Opcode.SMSG_TRAINER_LIST, 0x01BC},
            {Opcode.SMSG_TRANSFER_PENDING, 0x0917},
            {Opcode.SMSG_TRIGGER_CINEMATIC, 0x0198},
            {Opcode.SMSG_TUTORIAL_FLAGS, 0x0D1B},
            {Opcode.SMSG_UPDATE_OBJECT, 0x0C22},
            {Opcode.SMSG_WHO, 0x053C},
            {Opcode.SMSG_ZONE_UNDER_ATTACK, 0x148D}
        };

        private static readonly BiDictionary<Opcode, int> MiscOpcodes = new BiDictionary<Opcode, int>
        {
            {Opcode.MSG_MOVE_JUMP, 0x07C9},
            {Opcode.MSG_MOVE_SET_FLIGHT_SPEED, 0x02B2},
            {Opcode.MSG_MOVE_SET_RUN_SPEED, 0x06A5},
            {Opcode.MSG_MOVE_SET_SWIM_SPEED, 0x0A0D},
            {Opcode.MSG_MOVE_SET_WALK_SPEED, 0x0716},
            {Opcode.MSG_MOVE_START_BACKWARD, 0x12C0},
            {Opcode.MSG_MOVE_START_FORWARD, 0x13C9},
            {Opcode.MSG_MOVE_START_PITCH_DOWN, 0x16E8},
            {Opcode.MSG_MOVE_START_PITCH_UP, 0x0FE1},
            {Opcode.MSG_MOVE_START_STRAFE_LEFT, 0x0EC8},
            {Opcode.MSG_MOVE_START_STRAFE_RIGHT, 0x0269},
            {Opcode.MSG_MOVE_START_TURN_LEFT, 0x0760},
            {Opcode.MSG_MOVE_START_TURN_RIGHT, 0x17C9},
            {Opcode.MSG_MOVE_STOP, 0x0649},
            {Opcode.MSG_MOVE_STOP_STRAFE, 0x12C9},
            {Opcode.MSG_MOVE_STOP_TURN, 0x1749},
            {Opcode.MSG_MOVE_TELEPORT, 0x0A2E}
        };
    }
}

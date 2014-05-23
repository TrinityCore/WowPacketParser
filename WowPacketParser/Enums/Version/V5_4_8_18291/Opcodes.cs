using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V5_4_8_18291
{
    public static class Opcodes_5_4_8
    {
        public static BiDictionary<Opcode, int> Opcodes()
        {
            return Opcs;
        }

        private static readonly BiDictionary<Opcode, int> Opcs = new BiDictionary<Opcode, int>
        {
            {Opcode.CMSG_LOAD_SCREEN, 0x1DBD},
            {Opcode.CMSG_AUTH_SESSION, 0x00B2},
            {Opcode.CMSG_CHAR_DELETE, 0x04E2},
            {Opcode.CMSG_CHAR_ENUM, 0x00E0},
            {Opcode.CMSG_LOG_DISCONNECT, 0x10B3},
            {Opcode.CMSG_PLAYER_LOGIN, 0x158F},
            {Opcode.CMSG_RANDOMIZE_CHAR_NAME, 0x0B1C},
            {Opcode.CMSG_READY_FOR_ACCOUNT_DATA_TIMES, 0x031C},
            {Opcode.CMSG_TUTORIAL_FLAG, 0x1B90},
            {Opcode.CMSG_VIOLENCE_LEVEL, 0x0040},
            {Opcode.MSG_MOVE_FALL_LAND, 0x08FA},
            {Opcode.MSG_MOVE_HEARTBEAT, 0x01F2},
            {Opcode.MSG_MOVE_JUMP, 0x1153},
            {Opcode.MSG_MOVE_SET_FACING, 0x1050},
            {Opcode.MSG_MOVE_SET_PITCH, 0x017A},
            {Opcode.MSG_MOVE_SET_RUN_MODE, 0x0979},
            {Opcode.MSG_MOVE_SET_WALK_MODE, 0x08D1},
            {Opcode.MSG_MOVE_START_ASCEND, 0x11FA},
            {Opcode.MSG_MOVE_START_BACKWARD, 0x09D8},
            {Opcode.MSG_MOVE_START_DESCEND, 0x01D1},
            {Opcode.MSG_MOVE_START_FORWARD, 0x095A},
            {Opcode.MSG_MOVE_START_PITCH_DOWN, 0x08D8},
            {Opcode.MSG_MOVE_START_PITCH_UP, 0x00D8},
            {Opcode.MSG_MOVE_START_STRAFE_LEFT, 0x01F8},
            {Opcode.MSG_MOVE_START_STRAFE_RIGHT, 0x1058},
            {Opcode.MSG_MOVE_START_SWIM, 0x1858},
            {Opcode.MSG_MOVE_START_TURN_LEFT,0x01D0},
            {Opcode.MSG_MOVE_START_TURN_RIGHT, 0x107B},
            {Opcode.MSG_MOVE_STOP, 0x08F1},
            {Opcode.MSG_MOVE_STOP_ASCEND, 0x115A},
            {Opcode.MSG_MOVE_STOP_PITCH, 0x007A},
            {Opcode.MSG_MOVE_STOP_STRAFE, 0x0171},
            {Opcode.MSG_MOVE_STOP_SWIM, 0x0950},
            {Opcode.MSG_MOVE_STOP_TURN, 0x1170},
            {Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x162B},
            {Opcode.SMSG_ADDON_INFO, 0x160A},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x0949},
            {Opcode.SMSG_AUTH_RESPONSE, 0x0ABA},
            {Opcode.SMSG_CHAR_CREATE, 0x1CAA},
            {Opcode.SMSG_CHAR_DELETE, 0x0C9F},
            {Opcode.SMSG_CHAR_ENUM, 0x11C3},
            {Opcode.SMSG_CLIENTCACHE_VERSION, 0x002A},
            {Opcode.SMSG_FEATURE_SYSTEM_STATUS, 0x16BB},
            {Opcode.SMSG_INITIAL_SPELLS, 0x045A},
            {Opcode.SMSG_LOGIN_VERIFY_WORLD, 0x1C0F},
            {Opcode.SMSG_MOTD, 0x183B},
            {Opcode.SMSG_PLAYER_MOVE, 0x1A32},
            {Opcode.SMSG_RANDOMIZE_CHAR_NAME, 0x169F},
            {Opcode.SMSG_UPDATE_OBJECT, 0x1792},
            {Opcode.SMSG_INITIAL_SPELLS, 0x045A},
            {Opcode.SMSG_INITIAL_SPELLS, 0x045A},
            {Opcode.SMSG_INITIAL_SPELLS, 0x045A},
            {Opcode.SMSG_INITIAL_SPELLS, 0x045A},
        };
    }
}
         
using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V5_2_0_16650
{
    public static class Opcodes_5_2_0
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
            {Opcode.CMSG_AUTH_CONTINUED_SESSION, 0x03F3},
            {Opcode.CMSG_AUTH_SESSION, 0x1A72},
            {Opcode.CMSG_QUERY_CREATURE, 0x1C83},
            {Opcode.CMSG_DB_QUERY_BULK, 0x006A},
            {Opcode.CMSG_ENABLE_NAGLE, 0x06D2},
            {Opcode.CMSG_QUERY_GAME_OBJECT, 0x0118},
            {Opcode.CMSG_LOADING_SCREEN_NOTIFY, 0x045D},
            {Opcode.CMSG_PLAYER_LOGIN, 0x015D},
            {Opcode.CMSG_QUERY_QUEST_INFO, 0x001F},
            {Opcode.CMSG_VIOLENCE_LEVEL, 0x10AC},
            {Opcode.CMSG_WARDEN_DATA, 0x007C}
        };

        private static readonly BiDictionary<Opcode, int> ServerOpcodes = new BiDictionary<Opcode, int>
        {
            {Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x1124},
            {Opcode.SMSG_ADDON_INFO, 0x1130},
            {Opcode.SMSG_ALL_ACHIEVEMENT_DATA, 0x18A1},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x0C0B},
            {Opcode.SMSG_AUTH_RESPONSE, 0x0101},
            {Opcode.SMSG_BIND_POINT_UPDATE, 0x1121},
            {Opcode.SMSG_ENUM_CHARACTERS_RESULT, 0x01B4},
            {Opcode.SMSG_CACHE_VERSION, 0x0501},
            {Opcode.SMSG_CONTROL_UPDATE, 0x04F4},
            {Opcode.SMSG_QUERY_CREATURE_RESPONSE, 0x01B5},
            {Opcode.SMSG_DB_REPLY, 0x0411},
            {Opcode.SMSG_FEATURE_SYSTEM_STATUS, 0x04A5},
            {Opcode.SMSG_QUERY_GAME_OBJECT_RESPONSE, 0x1024},
            {Opcode.SMSG_GUILD_RANKS, 0x0DEF},
            {Opcode.SMSG_MOTD, 0x0514},
            {Opcode.SMSG_MOVE_SPLINE_UNROOT, 0x121D},
            {Opcode.SMSG_ON_MONSTER_MOVE, 0x0309},
            {Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE, 0x19C5},
            {Opcode.SMSG_REALM_SPLIT, 0x0274},
            {Opcode.SMSG_CONNECT_TO, 0x0C0E},
            {Opcode.SMSG_RESUME_COMMS, 0x0C18},
            {Opcode.SMSG_SET_PROFICIENCY, 0x0781},
            {Opcode.SMSG_TUTORIAL_FLAGS, 0x0B97},
            {Opcode.SMSG_UPDATE_OBJECT, 0x1406},
            {Opcode.SMSG_WARDEN_DATA, 0x0381},
            {Opcode.SMSG_WEEKLY_SPELL_USAGE, 0x01C4},
            {Opcode.SMSG_WORLD_SERVER_INFO, 0x0754}
        };

        private static readonly BiDictionary<Opcode, int> MiscOpcodes = new BiDictionary<Opcode, int>
        {
            {Opcode.MSG_SET_DUNGEON_DIFFICULTY, 0x0140}
        };
    }
}

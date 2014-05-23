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
            {Opcode.CMSG_AUTH_SESSION, 0x00B2},
            {Opcode.CMSG_DB_QUERY_BULK, 0x158D},
            {Opcode.CMSG_CHAR_ENUM, 0x00E0},
            {Opcode.CMSG_GUILD_QUERY, 0x1AB6},
            {Opcode.CMSG_LOAD_SCREEN, 0x1816},
            {Opcode.CMSG_LOG_DISCONNECT, 0x10B3},
            {Opcode.CMSG_READY_FOR_ACCOUNT_DATA_TIMES, 0x031C},
            {Opcode.CMSG_RESET_FACTION_CHEAT, 0x10B6},
            {Opcode.CMSG_REDIRECT_AUTH_PROOF, 0x0F49},
            {Opcode.CMSG_PLAYER_LOGIN, 0x158F},
            {Opcode.CMSG_SET_RAID_DIFFICULTY, 0x1093},
            {Opcode.CMSG_VIOLENCE_LEVEL, 0x0040},
            {Opcode.CMSG_WARDEN_DATA, 0x1816},

            {Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x162B},
            {Opcode.SMSG_ADDON_INFO, 0x160A},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x0949},
            {Opcode.SMSG_AUTH_RESPONSE, 0x0ABA},
            {Opcode.SMSG_CHAR_ENUM, 0x11C3},
            {Opcode.SMSG_CLIENTCACHE_VERSION, 0x002A},
            {Opcode.SMSG_DB_REPLY, 0x103B},
            {Opcode.SMSG_FORCE_SEND_QUEUED_PACKETS, 0x0969},
            {Opcode.SMSG_GUILD_QUERY_RESPONSE, 0x1B79},
            {Opcode.SMSG_REDIRECT_CLIENT, 0x1149},
            {Opcode.SMSG_SEND_SERVER_LOCATION, 0x19C1},
            {Opcode.SMSG_SUSPEND_COMMS, 0x1D48},
            {Opcode.SMSG_TUTORIAL_FLAGS, 0x1B90},
            {Opcode.SMSG_WARDEN_DATA, 0x14EB},
        };
    }
}
         
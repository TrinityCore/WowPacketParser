using System.Collections.Generic;

namespace WowPacketParser.Enums.Version//.V4_3_3_15354
{
    public static partial class Opcodes
    {
        private static readonly Dictionary<Opcode, int> _V4_3_3_opcodes = new Dictionary<Opcode, int>
        {
            {Opcode.CMSG_AUTH_SESSION, 0x0A02},
            {Opcode.CMSG_CHAR_ENUM, 0x2161},
            {Opcode.CMSG_LOAD_SCREEN, 0x3101},
            {Opcode.CMSG_PLAYER_LOGIN, 0x3320},
            {Opcode.CMSG_READY_FOR_ACCOUNT_DATA_TIMES, 0x2924},
            {Opcode.CMSG_REALM_SPLIT, 0x66AD},
            {Opcode.CMSG_REQUEST_ACCOUNT_DATA, 0x29A4},
            {Opcode.CMSG_REDIRECTION_AUTH_PROOF,0x0A26},
            {Opcode.CMSG_ZONEUPDATE, 0x3125},
            {Opcode.CMSG_WARDEN_DATA, 0x0202},
            {Opcode.SMSG_ADDON_INFO, 0x22AD},
            {Opcode.SMSG_AURA_UPDATE_ALL, 0x7A25},
            {Opcode.SMSG_ALL_ACHIEVEMENT_DATA, 0x390E},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x0055},
            {Opcode.SMSG_AUTH_RESPONSE, 0x3C84},
            {Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x622D},
            {Opcode.SMSG_CHAR_ENUM, 0x30C6},
            {Opcode.SMSG_CLIENTCACHE_VERSION, 0x306C},
            {Opcode.SMSG_CORPSE_RECLAIM_DELAY, 0x79EC},
            {Opcode.SMSG_CONTACT_LIST, 0x2D65},
            {Opcode.SMSG_INITIALIZE_FACTIONS, 0x3865},
            {Opcode.SMSG_INITIAL_SPELLS, 0x7364},
            {Opcode.SMSG_INIT_WORLD_STATES, 0x272C},
            {Opcode.SMSG_ATTACKERSTATEUPDATE, 0x306D},
            {Opcode.SMSG_EQUIPMENT_SET_LIST, 0x6AED},
            {Opcode.SMSG_REDIRECT_CLIENT, 0x0855},
            {Opcode.SMSG_MOTD, 0x3C6D},
            {Opcode.SMSG_MONSTER_MOVE, 0x29A5},
            {Opcode.SMSG_QUESTGIVER_STATUS, 0x6B65},
            {Opcode.SMSG_LOGIN_SETTIMESPEED, 0x6B2D},
            {Opcode.SMSG_LOGIN_VERIFY_WORLD, 0x3EE5},
            {Opcode.SMSG_LEARNED_DANCE_MOVES, 0x2CEC},
            {Opcode.SMSG_SET_PHASE_SHIFT, 0x7104},
            {Opcode.SMSG_SET_PROFICIENCY, 0x7324},
            {Opcode.SMSG_SEND_UNLEARN_SPELLS, 0x78AC},
            {Opcode.SMSG_TALENTS_INFO, 0x7FA5},
            {Opcode.SMSG_TUTORIAL_FLAGS, 0x25A4},
            {Opcode.SMSG_UPDATE_ACTION_BUTTONS, 0x604C},
            {Opcode.SMSG_UPDATE_OBJECT, 0x6264},

          
        };
    }
}

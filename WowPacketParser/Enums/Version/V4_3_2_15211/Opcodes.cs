using System.Collections.Generic;

namespace WowPacketParser.Enums.Version//.V4_3_2_15211
{
    public static partial class Opcodes
    {
        private static readonly Dictionary<Opcode, int> _V4_3_2_opcodes = new Dictionary<Opcode, int>
        {
            {Opcode.CMSG_ADDON_REGISTERED_PREFIXES, 0x2860},
            {Opcode.CMSG_AUTH_SESSION, 0x4042},
            {Opcode.CMSG_CHAR_ENUM, 0x4051},
            {Opcode.CMSG_CREATURE_QUERY, 0x2591},
            {Opcode.CMSG_GAMEOBJECT_QUERY, 0x4523},
            {Opcode.CMSG_JOIN_CHANNEL, 0x2241},
            {Opcode.CMSG_LEAVE_CHANNEL, 0x2800},
            {Opcode.CMSG_LOG_DISCONNECT, 0x4019},
            {Opcode.CMSG_MESSAGECHAT_CHANNEL, 0x28C1},
            {Opcode.CMSG_MESSAGECHAT_SAY, 0x22E0},
            {Opcode.CMSG_MESSAGECHAT_YELL, 0x2260},
            {Opcode.CMSG_NAME_QUERY, 0x2DA3},
            {Opcode.CMSG_READY_FOR_ACCOUNT_DATA_TIMES, 0x452B},
            {Opcode.CMSG_REALM_SPLIT, 0x0DB7},
            {Opcode.CMSG_REDIRECTION_AUTH_PROOF, 0x004A},
            {Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x058B},
            {Opcode.SMSG_ADDON_INFO, 0x6D8D},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x0129},
            {Opcode.SMSG_AUTH_RESPONSE, 0x0E54},
            {Opcode.SMSG_CHANNEL_NOTIFY, 0x65A9},
            {Opcode.SMSG_CHAR_ENUM, 0x0CF5},
            {Opcode.SMSG_CLIENTCACHE_VERSION, 0x453D},
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x25FB},
            {Opcode.SMSG_DB_REPLY, 0x06D1},
            {Opcode.SMSG_EQUIPMENT_SET_LIST, 0x4D9B},
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE, 0x253D},
            {Opcode.SMSG_GOSSIP_MESSAGE, 0x25B7},
            {Opcode.SMSG_GUILD_EVENT, 0x65F5},
            {Opcode.SMSG_GUILD_QUERY_RESPONSE, 0x2D33},
            {Opcode.SMSG_GUILD_RANK, 0x1EB1}, // Not confirmed
            {Opcode.SMSG_GUILD_ROSTER, 0x1E94}, // Not confirmed
            {Opcode.SMSG_MESSAGECHAT, 0x0529},
            {Opcode.SMSG_MOTD, 0x6D11},
            {Opcode.SMSG_NAME_QUERY_RESPONSE, 0x455D},
            {Opcode.SMSG_NPC_TEXT_UPDATE, 0x0D09},
            {Opcode.SMSG_PLAY_MUSIC, 0x256D},
            {Opcode.SMSG_PLAY_OBJECT_SOUND, 0x451F},
            {Opcode.SMSG_PLAY_SOUND, 0x2DE5},
            {Opcode.SMSG_QUESTGIVER_QUEST_DETAILS, 0x0D91},
            {Opcode.SMSG_QUEST_QUERY_RESPONSE, 0x25CB},
            {Opcode.SMSG_REALM_SPLIT, 0x0581},
            {Opcode.SMSG_REDIRECT_CLIENT, 0x1329},
            {Opcode.SMSG_TRAINER_LIST, 0x6D85},
            {Opcode.SMSG_TUTORIAL_FLAGS, 0x4D8F},
            {Opcode.SMSG_WARDEN_DATA, 0x0CF0},
            {Opcode.SMSG_SPELL_GO, 0x6DEF},
            {Opcode.SMSG_SPELL_START, 0x6D27},
            {Opcode.SMSG_EMOTE, 0x65F9},
            {Opcode.SMSG_STANDSTATE_UPDATE, 0x25DF},
            {Opcode.SMSG_AURA_UPDATE, 0x4D1B},
            {Opcode.SMSG_ATTACKERSTATEUPDATE, 0x2D59},
            {Opcode.SMSG_QUERY_TIME_RESPONSE, 0x053F},
            {Opcode.SMSG_MONSTER_MOVE, 0x2561},
            {Opcode.SMSG_FLIGHT_SPLINE_SYNC, 0x6533},
            {Opcode.SMSG_SPELLENERGIZELOG, 0x05EF},
            {Opcode.SMSG_SPELLNONMELEEDAMAGELOG, 0x250F},
        };
    }
}

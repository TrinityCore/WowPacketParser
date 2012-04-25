using System.Collections.Generic;

namespace WowPacketParser.Enums.Version//.V4_3_4_opcodes
{
    public static partial class Opcodes
    {
        private static readonly Dictionary<Opcode, int> _V4_3_4_opcodes = new Dictionary<Opcode, int>
        {
           {Opcode.CMSG_AUTH_SESSION, 0x0449},
           {Opcode.CMSG_GOSSIP_HELLO, 0x4525},
           {Opcode.CMSG_JOIN_CHANNEL, 0x0156},
           {Opcode.CMSG_READY_FOR_ACCOUNT_DATA_TIMES, 0x2B16},
           {Opcode.CMSG_REALM_SPLIT, 0x2906},
           {Opcode.CMSG_REDIRECTION_AUTH_PROOF, 0x044D},
           {Opcode.CMSG_REQUEST_ACCOUNT_DATA, 0x6505},
           {Opcode.CMSG_RESET_FACTION_CHEAT, 0x4469},
           {Opcode.CMSG_UPDATE_ACCOUNT_DATA, 0x4736},
           {Opcode.CMSG_WARDEN_DATA, 0x25A2},
           {Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x4B05},
           {Opcode.SMSG_ADDON_INFO, 0x2C14},
           {Opcode.SMSG_AURA_UPDATE, 0x6916},
           {Opcode.SMSG_AUTH_CHALLENGE, 0x4542},
           {Opcode.SMSG_AUTH_RESPONSE, 0x5DB6},
           {Opcode.SMSG_CHANNEL_NOTIFY, 0x0825},
           {Opcode.SMSG_CHAR_ENUM, 0x10B0},
           {Opcode.SMSG_CLIENTCACHE_VERSION, 0x2734},
           {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x6024},
           {Opcode.SMSG_DB_REPLY, 0x38A4},
           {Opcode.SMSG_DESTROY_OBJECT, 0x4724},
           {Opcode.SMSG_FORCE_SEND_QUEUED_PACKETS, 0x0140},
           {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE, 0x0915},
           {Opcode.SMSG_GOSSIP_MESSAGE, 0x2035},
           {Opcode.SMSG_LOGIN_VERIFY_WORLD, 0x2005},
           {Opcode.SMSG_MESSAGECHAT, 0x2026},
           {Opcode.SMSG_MONSTER_MOVE, 0x6E17},
           {Opcode.SMSG_MOTD, 0x0A35},
           {Opcode.SMSG_NAME_QUERY_RESPONSE, 0x6E04},
           {Opcode.SMSG_NPC_TEXT_UPDATE, 0x4436},
           {Opcode.SMSG_QUESTGIVER_OFFER_REWARD, 0x2427},
           {Opcode.SMSG_QUESTGIVER_QUEST_DETAILS, 0x2425},
           {Opcode.SMSG_QUEST_QUERY_RESPONSE, 0x6936},
           {Opcode.SMSG_SPELL_GO, 0x6E16},
           {Opcode.SMSG_SPELL_START, 0x6415},
           {Opcode.SMSG_TRAINER_LIST, 0x4414},
           {Opcode.SMSG_UPDATE_OBJECT, 0x4715},
        };
    }
}

using System.Collections.Generic;

namespace WowPacketParser.Enums.Version//.V4_3_2_15211
{
    public static partial class Opcodes
    {
        private static readonly Dictionary<Opcode, int> _V4_3_2_opcodes = new Dictionary<Opcode, int>
        {
            {Opcode.CMSG_AUTH_SESSION, 0x4042},
            {Opcode.CMSG_CHAR_ENUM, 0x4051},
            {Opcode.CMSG_REALM_SPLIT, 0x0DB7},
            {Opcode.CMSG_CREATURE_QUERY, 0x2591},
            {Opcode.CMSG_GAMEOBJECT_QUERY, 0x4523},
            {Opcode.CMSG_MESSAGECHAT_SAY, 0x22E0},
            {Opcode.CMSG_MESSAGECHAT_YELL, 0x2260},
            {Opcode.CMSG_READY_FOR_ACCOUNT_DATA_TIMES, 0x452B},
            {Opcode.CMSG_REDIRECTION_AUTH_PROOF, 0x4A},
            {Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x58B},
            {Opcode.SMSG_ADDON_INFO, 0x6D8D},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x129},
            {Opcode.SMSG_AUTH_RESPONSE, 0xE54},
            {Opcode.SMSG_CHAR_ENUM, 0xCF5},
            {Opcode.SMSG_CLIENTCACHE_VERSION, 0x453D},
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x25FB},
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE, 0x253D},
            {Opcode.SMSG_MESSAGECHAT, 0x0529},
            {Opcode.SMSG_PLAY_MUSIC, 0x256D},
            {Opcode.SMSG_PLAY_OBJECT_SOUND, 0x451F},
            {Opcode.SMSG_PLAY_SOUND, 0x2DE5},
            {Opcode.SMSG_REDIRECT_CLIENT, 0x1329},
            {Opcode.SMSG_TUTORIAL_FLAGS, 0x4D8F},
        };
    }
}

using System.Collections.Generic;

namespace WowPacketParser.Enums.Version//.V4_3_0_15005
{
    public static partial class Opcodes
    {
        private static Dictionary<Opcode, int> _V4_3_0_opcodes = new Dictionary<Opcode, int>
        {
            // 0x8723 is huge and sent a LOT... Compressed?
            //{Opcode.CMSG_LOAD_SCREEN, 0x0976},
            //{Opcode.SMSG_DB_REPLY, 0x0723},
            {Opcode.CMSG_AUTH_SESSION, 0x1100},
            {Opcode.CMSG_CHAR_CREATE, 0x2A86},
            {Opcode.CMSG_CHAR_DELETE, 0x38A4},
            {Opcode.CMSG_JOIN_CHANNEL, 0x074C},
            {Opcode.CMSG_KEEP_ALIVE, 0x66A0},
            {Opcode.CMSG_MESSAGECHAT_SAY, 0x136E},
            {Opcode.CMSG_MESSAGECHAT_YELL, 0x034C},
            {Opcode.CMSG_NAME_QUERY, 0x7220},
            {Opcode.CMSG_PING, 0x0100},
            {Opcode.CMSG_PLAYER_LOGIN, 0x0326},
            {Opcode.CMSG_QUESTGIVER_STATUS_QUERY, 0x7E86},
            {Opcode.CMSG_SELL_ITEM, 0x3000},
            {Opcode.CMSG_TAXINODE_STATUS_QUERY, 0x6E02},
            {Opcode.CMSG_TIME_SYNC_RESP, 0x7293},
            {Opcode.CMSG_UPDATE_ACCOUNT_DATA, 0x02A0},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x1723},
            {Opcode.SMSG_CHAR_CREATE, 0x2A82},
            {Opcode.SMSG_CHAR_DELETE, 0x2A02},
            {Opcode.SMSG_MESSAGECHAT, 0x3884},
            {Opcode.SMSG_NAME_QUERY_RESPONSE, 0x5E20},
            {Opcode.SMSG_PARTY_MEMBER_STATS_FULL, 0x5680},
            {Opcode.SMSG_PONG, 0x0302},
            {Opcode.SMSG_QUESTGIVER_QUEST_DETAILS, 0x2004},
            {Opcode.SMSG_QUESTGIVER_QUEST_FAILED, 0x60A2},
            {Opcode.SMSG_QUESTGIVER_QUEST_LIST, 0x5024},
            {Opcode.SMSG_QUESTLOG_FULL, 0x2A84},
            {Opcode.SMSG_QUESTUPDATE_COMPLETE, 0x6284},
            {Opcode.SMSG_QUESTUPDATE_FAILED, 0x5684},
            {Opcode.SMSG_TRAINER_BUY_FAILED, 0x34A2},
            {Opcode.SMSG_TRAINER_LIST, 0x4C80},
            {Opcode.SMSG_UPDATE_ACCOUNT_DATA_COMPLETE, 0x40A4},
        };
    }
}

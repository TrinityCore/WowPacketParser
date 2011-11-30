using System.Collections.Generic;

namespace WowPacketParser.Enums.Version//.V4_3_0_15005
{
    public static partial class Opcodes
    {
        private static Dictionary<Opcode, int> _V4_3_0_opcodes = new Dictionary<Opcode, int>
        {
            {Opcode.CMSG_SELL_ITEM, 0x3000},
            {Opcode.SMSG_MESSAGECHAT, 0x3884},
            {Opcode.CMSG_MESSAGECHAT_YELL, 0x034C},
            {Opcode.CMSG_MESSAGECHAT_SAY, 0x136E},
            {Opcode.SMSG_NAME_QUERY_RESPONSE, 0x5E20},
            {Opcode.CMSG_JOIN_CHANNEL, 0x074C},
            {Opcode.CMSG_CHAR_CREATE, 0x2A86},
            {Opcode.SMSG_CHAR_CREATE, 0x2A82}
        };
    }
}

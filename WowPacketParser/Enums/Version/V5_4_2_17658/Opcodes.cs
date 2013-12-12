using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V5_4_2_17658
{
    public static class Opcodes_5_4_2
    {
        public static BiDictionary<Opcode, int> Opcodes()
        {
            return Opcs;
        }

        private static readonly BiDictionary<Opcode, int> Opcs = new BiDictionary<Opcode, int>
        {
            {Opcode.CMSG_AUTH_SESSION, 0x196E},
            {Opcode.CMSG_CHANNEL_LIST, 0x0847},
            {Opcode.CMSG_CREATURE_QUERY, 0x0C4A},
            {Opcode.CMSG_GAMEOBJECT_QUERY, 0x05F4}, // correct?
            {Opcode.SMSG_ADDON_INFO, 0x0A9C},
            {Opcode.SMSG_AUTH_RESPONSE, 0x03A8},
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x0E85},
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE, 0x08F3},
            {Opcode.SMSG_MESSAGECHAT, 0x0A5B},
            {Opcode.SMSG_UPDATE_OBJECT, 0x1C89},
        };
    }
}

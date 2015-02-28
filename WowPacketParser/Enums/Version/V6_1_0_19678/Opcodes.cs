using System;
using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V6_1_0_19678
{
    public static class Opcodes_6_1_0
    {
        public static BiDictionary<Opcode, int> Opcodes(Direction direction)
        {
            if (direction == Direction.ClientToServer)
                return ClientOpcodes;
            if (direction == Direction.ServerToClient)
                return ServerOpcodes;
            throw new ArgumentOutOfRangeException("direction");
        }

        private static readonly BiDictionary<Opcode, int> ClientOpcodes = new BiDictionary<Opcode, int>
        {
            {Opcode.CMSG_AUTH_SESSION, 0x1872},
            {Opcode.CMSG_GUILD_QUERY, 0x19B3},
            {Opcode.CMSG_LOG_DISCONNECT, 0x1432},
            {Opcode.CMSG_WARDEN_DATA, 0x11E3},
        };

        private static readonly BiDictionary<Opcode, int> ServerOpcodes = new BiDictionary<Opcode, int>
        {
            {Opcode.SMSG_ADDON_INFO, 0x1F5C},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x0403},
            {Opcode.SMSG_AUTH_RESPONSE, 0x0B61},
            {Opcode.SMSG_BATTLE_PAY_GET_DISTRIBUTION_LIST_RESPONSE, 0x17A3},
            {Opcode.SMSG_BATTLE_PAY_GET_PURCHASE_LIST_RESPONSE, 0x1FC9}, 
            {Opcode.SMSG_CLIENTCACHE_VERSION, 0x116C},
            {Opcode.SMSG_CHAR_ENUM, 0x13F2},
            {Opcode.SMSG_CHUNKED_PACKET, 0x0C23},
            {Opcode.SMSG_COMPRESSED_PACKET, 0x0689},
            {Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN, 0x117A},
            {Opcode.SMSG_FINAL_CHUNK, 0x0C14},
            {Opcode.SMSG_GUILD_QUERY_RESPONSE, 0x06F3},
            {Opcode.SMSG_MULTIPLE_PACKETS, 0x0C33},
            {Opcode.SMSG_REDIRECT_CLIENT, 0x0413},
            {Opcode.SMSG_SET_TIME_ZONE_INFORMATION, 0x15B4},
            {Opcode.SMSG_UNDELETE_COOLDOWN_STATUS_RESPONSE, 0x1DDB},
            {Opcode.SMSG_UPDATE_OBJECT, 0x1762},
            {Opcode.SMSG_WARDEN_DATA, 0x110A},
        };

        private static readonly BiDictionary<Opcode, int> MiscOpcodes = new BiDictionary<Opcode, int>();
    }
}

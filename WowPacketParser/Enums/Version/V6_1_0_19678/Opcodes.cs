using System;
using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V6_1_0_19678
{
    public static class Opcodes_6_1_0
    {
        public static BiDictionary<Opcode, int> Opcodes(Direction direction)
        {
            switch (direction)
            {
                case Direction.ClientToServer:
                case Direction.BNClientToServer:
                    return ClientOpcodes;
                case Direction.ServerToClient:
                case Direction.BNServerToClient:
                    return ServerOpcodes;
            }
            return MiscOpcodes;
        }

        private static readonly BiDictionary<Opcode, int> ClientOpcodes = new BiDictionary<Opcode, int>
        {
            {Opcode.CMSG_AUTH_CONTINUED_SESSION, 0x1A72},
            {Opcode.CMSG_AUTH_SESSION, 0x1872},
            {Opcode.CMSG_BATTLE_PAY_GET_PURCHASE_LIST_QUERY, 0x11AA},
            {Opcode.CMSG_DB_QUERY_BULK, 0x1731},
            {Opcode.CMSG_GUILD_QUERY, 0x19B3},
            {Opcode.CMSG_LOG_DISCONNECT, 0x1432},
            {Opcode.CMSG_LOAD_SCREEN, 0x13E4},
            {Opcode.CMSG_QUEUED_MESSAGES_END, 0x147B},
            {Opcode.CMSG_PLAYER_LOGIN, 0x1D31},
            {Opcode.CMSG_ROUTER_CLIENT_LOG_STREAMING_ERROR, 0x1439},
            {Opcode.CMSG_VIOLENCE_LEVEL, 0x0071},
            {Opcode.CMSG_WARDEN_DATA, 0x11E3},
        };

        private static readonly BiDictionary<Opcode, int> ServerOpcodes = new BiDictionary<Opcode, int>
        {
            {Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x15F3},
            {Opcode.SMSG_ADDON_INFO, 0x1F5C},
            {Opcode.SMSG_AURA_UPDATE, 0x070A},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x0403},
            {Opcode.SMSG_AUTH_RESPONSE, 0x0B61},
            {Opcode.SMSG_BATTLE_PAY_GET_DISTRIBUTION_LIST_RESPONSE, 0x17A3},
            {Opcode.SMSG_BATTLE_PAY_GET_PURCHASE_LIST_RESPONSE, 0x1FC9},
            {Opcode.SMSG_BINDPOINTUPDATE, 0x156C},
            {Opcode.SMSG_CLIENTCACHE_VERSION, 0x116C},
            {Opcode.SMSG_CHAR_ENUM, 0x13F2},
            {Opcode.SMSG_CHUNKED_PACKET, 0x0C23},
            {Opcode.SMSG_COMPRESSED_PACKET, 0x0689},
            {Opcode.SMSG_DB_REPLY, 0x097C},
            {Opcode.SMSG_FEATURE_SYSTEM_STATUS, 0x13F3},
            {Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN, 0x117A},
            {Opcode.SMSG_FINAL_CHUNK, 0x0C14},
            {Opcode.SMSG_GUILD_QUERY_RESPONSE, 0x06F3},
            {Opcode.SMSG_HOTFIX_NOTIFY_BLOB, 0x19B9},
            {Opcode.SMSG_LOGIN_VERIFY_WORLD, 0x0B31},
            {Opcode.SMSG_ON_MONSTER_MOVE, 0x0B09},
            {Opcode.SMSG_MOTD, 0x12FB},
            {Opcode.SMSG_MOVE_UPDATE, 0x1514},
            {Opcode.SMSG_MULTIPLE_PACKETS, 0x0C33},
            {Opcode.SMSG_POWER_UPDATE, 0x1B0A},
            {Opcode.SMSG_PVP_SEASON, 0x13A1},
            {Opcode.SMSG_REDIRECT_CLIENT, 0x0413},
            {Opcode.SMSG_SET_PROFICIENCY, 0x092A},
            {Opcode.SMSG_SET_TIME_ZONE_INFORMATION, 0x15B4},
            {Opcode.SMSG_SPELL_GO, 0x1281},
            {Opcode.SMSG_SPELL_START, 0x0629},
            {Opcode.SMSG_TUTORIAL_FLAGS, 0x0A39},
            {Opcode.SMSG_UNDELETE_COOLDOWN_STATUS_RESPONSE, 0x1DDB},
            {Opcode.SMSG_UPDATE_OBJECT, 0x1762},
            {Opcode.SMSG_WARDEN_DATA, 0x110A},
        };

        private static readonly BiDictionary<Opcode, int> MiscOpcodes = new BiDictionary<Opcode, int>();
    }
}

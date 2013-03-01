using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V5_0_5_16048
{
    public static class Opcodes_5_0_5
    {
        public static BiDictionary<Opcode, int> Opcodes()
        {
            return Opcs;
        }

        private static readonly BiDictionary<Opcode, int> Opcs = new BiDictionary<Opcode, int>
        {
            {Opcode.CMSG_AUTH_SESSION, 0x008A},
            {Opcode.CMSG_CHANNEL_MODERATOR, 0x0581},
            {Opcode.CMSG_COMMENTATOR_START_WARGAME, 0x0361},
            {Opcode.CMSG_CREATURE_QUERY, 0x0884},
            {Opcode.CMSG_GAMEOBJECT_QUERY, 0x0CF8},
            {Opcode.CMSG_GUILD_DISBAND, 0x0062},
            {Opcode.CMSG_GROUP_RAID_CONVERT, 0x034F},
            {Opcode.CMSG_GUILD_EVENT_LOG_QUERY, 0x06C3},
            {Opcode.CMSG_REALM_SPLIT, 0x0820},
            {Opcode.SMSG_ADDON_INFO, 0x09C6},
            {Opcode.SMSG_AURA_UPDATE, 0x08AB},
            {Opcode.SMSG_AURA_UPDATE_ALL, 0x0DDA},
            {Opcode.SMSG_AUTH_CHALLENGE, 0x0523},
            {Opcode.SMSG_AUTH_RESPONSE, 0x0E20},
            {Opcode.SMSG_CHAR_ENUM, 0x0E28},
            {Opcode.SMSG_CLIENTCACHE_VERSION, 0x0956},
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x0D6C},
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE, 0x0D21},
            {Opcode.SMSG_GOSSIP_MESSAGE, 0x0DE4},
            {Opcode.SMSG_GUILD_EVENT, 0x08E4},
            {Opcode.SMSG_GUILD_QUERY_RESPONSE, 0x0D6B},
            {Opcode.SMSG_GUILD_ROSTER, 0x0BEA},
            {Opcode.SMSG_MESSAGECHAT, 0x0C9F},
            {Opcode.SMSG_MOTD, 0x0952},
            {Opcode.SMSG_QUEST_NPC_QUERY_RESPONSE, 0x0A2E},
            {Opcode.SMSG_QUEST_POI_QUERY_RESPONSE, 0x0950},
            {Opcode.SMSG_SPELL_START, 0x08FC},
            {Opcode.SMSG_SPELL_GO, 0x0D42},
            {Opcode.SMSG_TUTORIAL_FLAGS, 0x08AE},
            {Opcode.SMSG_UPDATE_OBJECT, 0x08F7},
            // {Opcode.SMSG_MULTIPLE_PACKETS, 0x0826},
            // {Opcode.SMSG_MULTIPLE_PACKETS_2, 0x0B8B},
        };
    }
}

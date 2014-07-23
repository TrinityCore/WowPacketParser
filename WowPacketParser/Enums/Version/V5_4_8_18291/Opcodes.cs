using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V5_4_8_18291
{
    public static class Opcodes_5_4_8
    {
        public static BiDictionary<Opcode, int> Opcodes()
        {
            return Opcs;
        }

        private static readonly BiDictionary<Opcode, int> Opcs = new BiDictionary<Opcode, int>
        {
            {Opcode.CMSG_AREATRIGGER, 0x1C44}, // 18291
            {Opcode.CMSG_AUCTION_HELLO, 0x0379}, // 18291
            {Opcode.CMSG_AUTH_SESSION, 0x00B2}, // 18291
            {Opcode.CMSG_BANKER_ACTIVATE, 0x02E9}, // 18291
            {Opcode.CMSG_BUY_BANK_SLOT, 0x12F2}, // 18291
            {Opcode.CMSG_GAMEOBJ_REPORT_USE, 0x6D9}, // 18291
            {Opcode.CMSG_GAMEOBJ_USE, 0x6D8}, // 18291
            {Opcode.CMSG_GOSSIP_HELLO, 0x12F3}, // 18291
            {Opcode.CMSG_LIST_INVENTORY, 0x02D8}, // 18291
            {Opcode.CMSG_RANDOMIZE_CHAR_NAME, 0x0B1C}, // 18414
            {Opcode.CMSG_CHAR_CREATE, 0x0F1D}, // 18414
            {Opcode.CMSG_READY_FOR_ACCOUNT_DATA_TIMES, 0x031C}, // 18414
            {Opcode.CMSG_CHAR_ENUM, 0x00E0}, // 18414
            {Opcode.CMSG_MOVE_TIME_SKIPPED, 0x0150}, // 18414
            {Opcode.CMSG_REQUEST_HOTFIX, 0x158D}, // 18414
            {Opcode.CMSG_QUESTGIVER_CHOOSE_REWARD, 0x07CB}, // 18414
            {Opcode.CMSG_AUTOSTORE_LOOT_ITEM, 0x0354}, // 18414
            {Opcode.CMSG_QUESTGIVER_STATUS_MULTIPLE_QUERY, 0x02F1}, // 18414
            {Opcode.CMSG_QUEST_POI_QUERY, 0x10C2}, // 18414
			
            {Opcode.SMSG_AUCTION_HELLO, 0x10A7}, // 18291
            {Opcode.SMSG_AUTH_CHALLENGE, 0x0949}, // 18291
            {Opcode.SMSG_AUTH_RESPONSE, 0x0ABA}, // 18291
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x048B}, // 18291
            {Opcode.SMSG_DESTROY_OBJECT, 0x14C2}, // 18291
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE, 0x06BF}, // 18291
            {Opcode.SMSG_GOSSIP_POI, 0x0785}, // 18291
            {Opcode.SMSG_INITIAL_SPELLS, 0x045A}, // 18291
            {Opcode.SMSG_LOGIN_VERIFY_WORLD, 0x1C0F}, // 18291
            {Opcode.SMSG_SHOW_BANK, 0x0007}, // 18291
            {Opcode.SMSG_NEW_WORLD, 0x1C3B},  // 18291
            {Opcode.SMSG_TRANSFER_PENDING, 0x061B}, // 18291
            {Opcode.SMSG_UPDATE_OBJECT, 0x1792}, // 18291
            {Opcode.SMSG_RANDOMIZE_CHAR_NAME, 0x169F}, // 18414
            {Opcode.SMSG_CHAR_CREATE, 0x1CAA}, // 18414
            {Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x162B}, //18414
            {Opcode.SMSG_SET_TIMEZONE_INFORMATION, 0x19C1}, // 18414
            {Opcode.SMSG_CHAR_ENUM, 0x11C3}, // 18414
            {Opcode.SMSG_DB_REPLY, 0x103B}, // 18414
            {Opcode.SMSG_SPELL_START, 0x107A}, // 18414
            {Opcode.SMSG_NAME_QUERY_RESPONSE, 0x169B}, // 18414
            {Opcode.SMSG_VOID_TRANSFER_RESULT, 0x18BA}, // 18414
            {Opcode.SMSG_QUESTGIVER_QUEST_COMPLETE, 0x0346}, // 18414
            {Opcode.SMSG_SET_FACTION_STANDING, 0x10AA}, // 18414
            {Opcode.SMSG_SPELL_GO, 0x09D8}, // 18414
            {Opcode.SMSG_SPELLNONMELEEDAMAGELOG, 0x1450}, // 18414
            {Opcode.SMSG_PLAYER_MOVE, 0x1A32}, // 18414
            {Opcode.SMSG_PLAYED_TIME, 0x11E2}, // 18414
            {Opcode.SMSG_POWER_UPDATE, 0x109F}, // 18414
            {Opcode.SMSG_QUERY_TIME_RESPONSE, 0x100F}, // 18414
            {Opcode.SMSG_QUESTGIVER_OFFER_REWARD, 0x074F}, // 18414
            {Opcode.SMSG_QUESTGIVER_QUEST_DETAILS, 0x134C}, //18414
            {Opcode.SMSG_QUESTGIVER_STATUS, 0x1275}, // 18414
            {Opcode.SMSG_QUESTGIVER_STATUS_MULTIPLE, 0x06CE}, // 18414
            {Opcode.SMSG_QUESTUPDATE_ADD_KILL, 0x1645}, // 18414
            {Opcode.SMSG_QUEST_POI_QUERY_RESPONSE, 0x067F}, // 18414
            {Opcode.SMSG_QUEST_QUERY_RESPONSE, 0x0276}, // 18414
            {Opcode.SMSG_PVP_SEASON, 0x069B}, // 18414
            {Opcode.SMSG_HOTFIX_INFO, 0x1EBA}, // 18414
            {Opcode.SMSG_CONTACT_LIST, 0x1F22}, // 18414
            {Opcode.SMSG_BATTLE_PET_JOURNAL_LOCK_ACQUIRED, 0x1A0F}, // 18414
            {Opcode.SMSG_TALENTS_INFO, 0x0A9B}, // 18414
            {Opcode.SMSG_BINDPOINTUPDATE, 0x0E3B}, // 18414
            {Opcode.SMSG_SET_PROFICIENCY, 0x1440}, // 18414
			
        };
    }
}

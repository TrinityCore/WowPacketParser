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
            {Opcode.CMSG_GOSSIP_HELLO, 0x12F3}, // 18291
            {Opcode.CMSG_LIST_INVENTORY, 0x02D8}, // 18291
			
            {Opcode.SMSG_AUCTION_HELLO, 0x10A7}, // 18291
            {Opcode.SMSG_AUTH_CHALLENGE, 0x0949}, // 18291
            {Opcode.SMSG_AUTH_RESPONSE, 0x0ABA}, // 18291
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x048B}, // 18291
            {Opcode.SMSG_INITIAL_SPELLS, 0x045A}, // 18291
            {Opcode.SMSG_LOGIN_VERIFY_WORLD, 0x1C0F}, // 18291
            {Opcode.SMSG_SHOW_BANK, 0x0007}, // 18291
            {Opcode.SMSG_NEW_WORLD, 0x1C3B},  // 18291
            {Opcode.SMSG_TRANSFER_PENDING, 0x061B}, // 18291
            {Opcode.SMSG_UPDATE_OBJECT, 0x1792}, // 18291
			
        };
    }
}

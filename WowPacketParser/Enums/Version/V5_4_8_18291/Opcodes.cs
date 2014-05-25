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
            {Opcode.CMSG_AUCTION_HELLO, 0x0379}, // 18291
            {Opcode.CMSG_AUTH_SESSION, 0x00B2}, // 18291
            {Opcode.CMSG_GOSSIP_HELLO, 0x12F3}, // 18291
            {Opcode.CMSG_LIST_INVENTORY, 0x02D8}, // 18291
			
            {Opcode.SMSG_AUTH_CHALLENGE, 0x0949}, // 18291
            {Opcode.SMSG_AUTH_RESPONSE, 0x0ABA}, // 18291
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x048B}, // 18291
            {Opcode.SMSG_INITIAL_SPELLS, 0x045A}, // 18291
            {Opcode.SMSG_LOGIN_VERIFY_WORLD, 0x1C0F}, // 18291
            {Opcode.SMSG_UPDATE_OBJECT, 0x1792}, // 18291
			
        };
    }
}

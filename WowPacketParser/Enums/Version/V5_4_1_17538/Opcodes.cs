using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V5_4_1_17538
{
    public static class Opcodes_5_4_1
    {
        public static BiDictionary<Opcode, int> Opcodes()
        {
            return Opcs;
        }

        private static readonly BiDictionary<Opcode, int> Opcs = new BiDictionary<Opcode, int>
        {
            {Opcode.CMSG_BANKER_ACTIVATE, 0x07FD},
            {Opcode.CMSG_GOSSIP_HELLO, 0x025C},
            {Opcode.SMSG_ACCOUNT_DATA_TIMES, 0x1486},
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE, 0x011C},
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE, 0x0916},
            {Opcode.SMSG_LOGIN_VERIFY_WORLD, 0x0C24},
            {Opcode.SMSG_REALM_QUERY_RESPONSE, 0x052D},
            {Opcode.SMSG_SHOW_BANK, 0x008E},
            {Opcode.SMSG_INITIAL_SPELLS, 0x1164},
            
        };
    }
}

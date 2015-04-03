using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V5_0_4_16016
{
    public static class Opcodes_5_0_4
    {
        public static BiDictionary<Opcode, int> Opcodes(Direction direction)
        {
            if (direction == Direction.ClientToServer || direction == Direction.BNClientToServer)
                return ClientOpcodes;
            if (direction == Direction.ServerToClient || direction == Direction.BNServerToClient)
                return ServerOpcodes;
            return MiscOpcodes;
        }

        private static readonly BiDictionary<Opcode, int> ClientOpcodes = new BiDictionary<Opcode, int>
        {
            {Opcode.CMSG_BATTLEFIELD_STATUS, 0x04ED},
            {Opcode.CMSG_BUG, 0x0790},
            {Opcode.CMSG_CAST_SPELL, 0x01BB},
            {Opcode.CMSG_GM_TICKET_UPDATE_TEXT, 0x0599},
            {Opcode.CMSG_GUILD_BANK_QUERY_TAB, 0x0675},
            {Opcode.CMSG_GUILD_MEMBER_SEND_SOR_REQUEST, 0x0688},
            {Opcode.CMSG_PET_ACTION , 0x04BF},
            {Opcode.CMSG_QUEST_GIVER_ACCEPT_QUEST, 0x017D},
            {Opcode.CMSG_QUEST_GIVER_CHOOSE_REWARD, 0x0091},
            {Opcode.CMSG_QUEST_GIVER_COMPLETE_QUEST, 0x01B3},
            {Opcode.CMSG_QUEST_GIVER_SHARE_QUEST, 0x00B3},
            {Opcode.CMSG_QUEST_GIVER_STATUS_MULTIPLE_QUERY, 0x027F},
            {Opcode.CMSG_REQUEST_PET_INFO, 0x01F4},
            {Opcode.CMSG_SELL_ITEM, 0x0493},
            {Opcode.CMSG_SEND_MAIL, 0x09A8},
            {Opcode.CMSG_TAXI_NODE_STATUS_QUERY, 0x029D},
            {Opcode.CMSG_UI_TIME_REQUEST, 0x00D9},
            {Opcode.CMSG_WHO, 0x04D1}
        };

        private static readonly BiDictionary<Opcode, int> ServerOpcodes = new BiDictionary<Opcode, int>();

        private static readonly BiDictionary<Opcode, int> MiscOpcodes = new BiDictionary<Opcode, int>();
    }
}

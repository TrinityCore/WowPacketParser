using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V3_4_5_61815
{
    public static class Opcodes_3_4_5
    {
        public static BiDictionary<Opcode, int> Opcodes(Direction direction)
        {
            switch (direction)
            {
                case Direction.ClientToServer:
                    return ClientOpcodes;
                case Direction.ServerToClient:
                    return ServerOpcodes;
                default:
                    return MiscOpcodes;
            }
        }

        private static readonly BiDictionary<Opcode, int> ClientOpcodes = new()
        {
        };

        private static readonly BiDictionary<Opcode, int> ServerOpcodes = new()
        {
            { Opcode.SMSG_QUERY_CREATURE_RESPONSE, 0x400006 },
            { Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE, 0x500016 },
        };

        private static readonly BiDictionary<Opcode, int> MiscOpcodes = new();
    }
}

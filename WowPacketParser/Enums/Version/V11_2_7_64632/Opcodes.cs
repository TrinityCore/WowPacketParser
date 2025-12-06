using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V11_2_7_64632
{
    public static class Opcodes_11_2_7
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
        };

        private static readonly BiDictionary<Opcode, int> MiscOpcodes = new();
    }
}

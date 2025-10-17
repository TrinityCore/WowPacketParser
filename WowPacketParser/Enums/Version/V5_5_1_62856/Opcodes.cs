using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V5_5_1_62856
{
    public static class Opcodes_5_5_1
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
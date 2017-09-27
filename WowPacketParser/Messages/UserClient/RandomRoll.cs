using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct RandomRoll
    {
        public int Min;
        public int Max;
        public byte PartyIndex;

        [Parser(Opcode.CMSG_RANDOM_ROLL)]
        public static void HandleRandomRoll(Packet packet)
        {
            packet.ReadInt32("Min");
            packet.ReadInt32("Max");
            packet.ReadByte("PartyIndex");
        }
    }
}

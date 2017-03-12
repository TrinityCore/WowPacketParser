using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientArenaError
    {
        public ArenaErrorType ErrorType;
        public byte TeamSize;

        [Parser(Opcode.SMSG_ARENA_ERROR)]
        public static void HandleArenaError(Packet packet)
        {
            var error = packet.ReadUInt32E<ArenaError>("Error");
            if (error == ArenaError.NoTeam)
                packet.ReadByte("Arena Type"); // 2, 3, 5
        }
    }
}

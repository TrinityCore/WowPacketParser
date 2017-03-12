using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientArenaError
    {
        public ArenaErrorType ErrorType;
        public byte TeamSize;
    }
}

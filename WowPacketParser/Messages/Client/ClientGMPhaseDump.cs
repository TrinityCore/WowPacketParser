using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGMPhaseDump
    {
        public PhaseShiftData PhaseShift;
        public ulong Target;
    }
}

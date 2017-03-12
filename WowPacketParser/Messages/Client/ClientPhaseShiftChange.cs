using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientPhaseShiftChange
    {
        public ulong Client;
        public Data PreloadMapIDs;
        public PhaseShiftData PhaseShift;
        public Data UiWorldMapAreaIDSwaps;
        public Data VisibleMapIDs;
    }
}

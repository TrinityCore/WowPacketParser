using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
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

using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientReportPvPPlayerAFKResult
    {
        public ulong Offender;
        public byte NumPlayersIHaveReported;
        public byte NumBlackMarksOnOffender;
        public byte Result;
    }
}

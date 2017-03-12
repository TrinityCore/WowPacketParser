using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct RaceClassAvailability
    {
        public byte RaceOrClassID;
        public byte RequiredExpansion;
    }
}

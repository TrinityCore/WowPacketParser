using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientRestrictedFactionChangeRule
    {
        public int Mask;
        public byte RaceID;
    }
}

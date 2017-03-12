using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ForcedReaction
    {
        public int Faction;
        public int Reaction;
    }
}

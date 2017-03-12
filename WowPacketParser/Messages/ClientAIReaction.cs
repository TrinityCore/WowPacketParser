using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAIReaction
    {
        public ulong UnitGUID;
        public int Reaction;
    }
}

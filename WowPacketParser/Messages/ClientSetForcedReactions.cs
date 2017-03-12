using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSetForcedReactions
    {
        public List<ForcedReaction> Reactions;
    }
}

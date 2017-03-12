using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSetForcedReactions
    {
        public List<ForcedReaction> Reactions;
    }
}

using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveSetCompoundState
    {
        public ulong MoverGUID;
        public List<MoveStateChange> StateChanges;
    }
}

using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveSetCompoundState
    {
        public ulong MoverGUID;
        public List<MoveStateChange> StateChanges;
    }
}

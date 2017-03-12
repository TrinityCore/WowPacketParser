using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientInstanceResetFailed
    {
        public ResetFailedReason ResetFailedReason;
        public uint MapID;
    }
}

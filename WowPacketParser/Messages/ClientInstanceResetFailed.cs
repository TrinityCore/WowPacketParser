using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientInstanceResetFailed
    {
        public ResetFailedReason ResetFailedReason;
        public uint MapID;
    }
}

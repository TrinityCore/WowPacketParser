using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveUpdateApplyMovementForce
    {
        public CliMovementStatus Status;
        public CliMovementForce Force;
    }
}

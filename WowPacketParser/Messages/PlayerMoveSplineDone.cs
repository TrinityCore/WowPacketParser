using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerMoveSplineDone
    {
        public uint SplineID;
        public CliMovementStatus Status;
    }
}

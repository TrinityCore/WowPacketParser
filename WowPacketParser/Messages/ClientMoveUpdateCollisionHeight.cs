using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveUpdateCollisionHeight
    {
        public float Scale;
        public CliMovementStatus Status;
        public float Height;
    }
}

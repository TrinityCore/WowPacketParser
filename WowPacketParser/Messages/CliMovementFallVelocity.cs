using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliMovementFallVelocity
    {
        public Vector2 Direction;
        public float Speed;
    }
}

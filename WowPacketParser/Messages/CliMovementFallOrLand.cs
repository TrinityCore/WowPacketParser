using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliMovementFallOrLand
    {
        public uint Time;
        public float JumpVelocity;
        public CliMovementFallVelocity Velocity; // Optional
    }
}

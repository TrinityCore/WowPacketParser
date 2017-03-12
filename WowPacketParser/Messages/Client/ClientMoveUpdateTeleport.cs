using System.Collections.Generic;
using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveUpdateTeleport
    {
        public CliMovementStatus Status;
        public List<CliMovementForce> MovementForces;
        public float? SwimBackSpeed; // Optional
        public float? FlightSpeed; // Optional
        public float? SwimSpeed; // Optional
        public float? WalkSpeed; // Optional
        public float? TurnRate; // Optional
        public float? RunSpeed; // Optional
        public float? FlightBackSpeed; // Optional
        public float? RunBackSpeed; // Optional
        public float? PitchRate; // Optional
    }
}

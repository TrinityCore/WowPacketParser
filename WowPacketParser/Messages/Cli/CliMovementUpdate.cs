using System.Collections.Generic;

namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliMovementUpdate
    {
        public CliMovementStatus Status;
        public float WalkSpeed;
        public float RunSpeed;
        public float RunBackSpeed;
        public float SwimSpeed;
        public float SwimBackSpeed;
        public float FlightSpeed;
        public float FlightBackSpeed;
        public float TurnRate;
        public float PitchRate;
        public CliMovementSpline? Spline; // Optional
        public List<CliMovementForce> MovementForces;
    }
}

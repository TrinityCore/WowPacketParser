using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliMovementStatus
    {
        public ulong MoverGUID;
        public uint MoveFlags0;
        public uint MoveFlags1;
        public uint MoveTime;
        public Vector3 Position;
        public float Facing;
        public CliMovementTransport? Transport; // Optional
        public float Pitch;
        public CliMovementFallOrLand? Fall; // Optional
        public float StepUpStartElevation;
        public bool HasSpline;
        public bool HeightChangeFailed;
        public List<uint> RemoveForcesIDs;
        public uint MoveIndex;
        public bool RemoteTimeValid;
    }
}

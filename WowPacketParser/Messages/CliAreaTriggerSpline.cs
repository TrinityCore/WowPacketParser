using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliAreaTriggerSpline
    {
        public uint TimeToTarget;
        public uint ElapsedTimeForMovement;
        public List<Vector3> Points;
    }
}

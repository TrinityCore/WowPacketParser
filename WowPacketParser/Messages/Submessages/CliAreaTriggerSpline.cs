using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliAreaTriggerSpline
    {
        public uint TimeToTarget;
        public uint ElapsedTimeForMovement;
        public List<Vector3> Points;
    }
}

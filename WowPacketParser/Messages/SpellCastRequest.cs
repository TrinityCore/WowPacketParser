using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct SpellCastRequest
    {
        public byte CastID;
        public int SpellID;
        public int Misc;
        public byte SendCastFlags;
        public SpellTargetData Target;
        public MissileTrajectoryRequest MissileTrajectory;
        public CliMovementStatus? MoveUpdate; // Optional
        public List<SpellWeight> Weight;
    }
}

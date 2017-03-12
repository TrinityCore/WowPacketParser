using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliAreaTrigger
    {
        public uint ElapsedMs;
        public bool AbsoluteOrientation;
        public bool DynamicShape;
        public bool Attached;
        public bool FaceMovementDir;
        public bool FollowsTerrain;
        public Vector3 RollPitchYaw;
        public Vector3? TargetRollPitchYaw; // Optional
        public uint? ScaleCurveID; // Optional
        public uint? MorphCurveID; // Optional
        public uint? FacingCurveID; // Optional
        public uint? MoveCurveID; // Optional
        public CliAreaTriggerSphere? AreaTriggerSphere; // Optional
        public CliAreaTriggerBox? AreaTriggerBox; // Optional
        public CliAreaTriggerPolygon? AreaTriggerPolygon; // Optional
        public CliAreaTriggerCylinder? AreaTriggerCylinder; // Optional
        public CliAreaTriggerSpline? AreaTriggerSpline; // Optional
    }
}

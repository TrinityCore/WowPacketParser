using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum AreaTriggerCreatePropertiesLegacyFlags : uint
    {
        HasAbsoluteOrientation  = 0x00001,
        HasDynamicShape         = 0x00002,
        HasAttached             = 0x00004,
        FaceMovementDirection   = 0x00008,
        FollowsTerrain          = 0x00010,
        AlwaysExterior          = 0x00020,
        HasTargetRollPitchYaw   = 0x00040,
        HasAnimId               = 0x00080,
        VisualAnimIsDecay       = 0x00100,
        HasAnimKitId            = 0x00200,
        HasOrbit                = 0x00400,
        HasMovementScript       = 0x00800
    }

    [Flags]
    public enum AreaTriggerCreatePropertiesFlags : uint
    {
        HeightIgnoresScale  = 0x0001,
        VisualAnimIsDecay   = 0x0002,
        AbsoluteOrientation = 0x0004,
        FaceMovementDir     = 0x0008,
        FollowsTerrain      = 0x0010,
        AlwaysExterior      = 0x0020,
        UsesUnitRawFacing   = 0x0040
    }
}

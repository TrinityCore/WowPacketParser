﻿using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum AreaTriggerFlags : uint
    {
        HasAbsoluteOrientation  = 0x00001,
        HasDynamicShape         = 0x00002,
        HasAttached             = 0x00004,
        FaceMovementDirection   = 0x00008,
        FollowsTerrain          = 0x00010,
        Unk1                    = 0x00020,
        HasTargetRollPitchYaw   = 0x00040,
        HasAnimId               = 0x00080,
        Unk3                    = 0x00100,
        HasAnimKitId            = 0x00200,
        HasCircularMovement     = 0x00400,
        Unk5                    = 0x00800
    }
}

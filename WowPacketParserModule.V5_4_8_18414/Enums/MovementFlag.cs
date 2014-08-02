using System;

namespace WowPacketParserModule.V5_4_8_18414.Enums
{
    [Flags]
    public enum MovementFlag548 : uint
    {
        NONE                  = 0x00000000,
        FORWARD               = 0x00000001,
        BACKWARD              = 0x00000002,
        STRAFE_LEFT           = 0x00000004,
        STRAFE_RIGHT          = 0x00000008,
        LEFT                  = 0x00000010,
        RIGHT                 = 0x00000020,
        PITCH_UP              = 0x00000040,
        PITCH_DOWN            = 0x00000080,
        WALKING               = 0x00000100,               // Walking
        DISABLE_GRAVITY       = 0x00000200,               // Former MOVEMENTFLAG_LEVITATING. This is used when walking is not possible.
        ROOT                  = 0x00000400,               // Must not be set along with MOVEMENTFLAG_MASK_MOVING
        FALLING               = 0x00000800,               // damage dealt on that type of falling
        FALLING_FAR           = 0x00001000,
        PENDING_STOP          = 0x00002000,
        PENDING_STRAFE_STOP   = 0x00004000,
        PENDING_FORWARD       = 0x00008000,
        PENDING_BACKWARD      = 0x00010000,
        PENDING_STRAFE_LEFT   = 0x00020000,
        PENDING_STRAFE_RIGHT  = 0x00040000,
        PENDING_ROOT          = 0x00080000,
        SWIMMING              = 0x00100000,               // appears with fly flag also
        ASCENDING             = 0x00200000,               // press "space" when flying
        DESCENDING            = 0x00400000,
        CAN_FLY               = 0x00800000,               // Appears when unit can fly AND also walk
        FLYING                = 0x01000000,               // unit is actually flying. pretty sure this is only used for players. creatures use disable_gravity
        SPLINE_ELEVATION      = 0x02000000,               // used for flight paths
        WATERWALKING          = 0x04000000,               // prevent unit from falling through water
        FALLING_SLOW          = 0x08000000,               // active rogue safe fall spell (passive)
        HOVER                 = 0x10000000,               // hover, cannot jump
        DISABLE_COLLISION     = 0x20000000,
    }
}

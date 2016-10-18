using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum AreaTriggerFlags
    {
        AREATRIGGER_FLAG_HAS_ABSOLUTE_ORIENTATION   = 0x00001,
        AREATRIGGER_FLAG_HAS_DYNAMIC_SHAPE          = 0x00002,
        AREATRIGGER_FLAG_HAS_ATTACHED               = 0x00004,
        AREATRIGGER_FLAG_HAS_FACE_MOVEMENT_DIR      = 0x00008,
        AREATRIGGER_FLAG_HAS_FOLLOWS_TERRAIN        = 0x00010,
        AREATRIGGER_FLAG_UNK1                       = 0x00020,
        AREATRIGGER_FLAG_HAS_TARGET_ROLL_PITCH_YAW  = 0x00040,
        AREATRIGGER_FLAG_HAS_SCALE_CURVE            = 0x00080,
        AREATRIGGER_FLAG_HAS_MORPH_CURVE            = 0x00100,
        AREATRIGGER_FLAG_HAS_FACING_CURVE           = 0x00200,
        AREATRIGGER_FLAG_HAS_MOVE_CURVE             = 0x00400,
        AREATRIGGER_FLAG_UNK2                       = 0x00800,
        AREATRIGGER_FLAG_UNK3                       = 0x01000,
        AREATRIGGER_FLAG_UNK4                       = 0x02000,
        AREATRIGGER_FLAG_HAS_SPHERE                 = 0x04000,
        AREATRIGGER_FLAG_HAS_BOX                    = 0x08000,
        AREATRIGGER_FLAG_HAS_POLYGON                = 0x10000,
        AREATRIGGER_FLAG_HAS_CYLINDER               = 0x20000,
        AREATRIGGER_FLAG_HAS_SPLINE                 = 0x40000,
        AREATRIGGER_FLAG_UNK5                       = 0x80000
    };
}

using System;

namespace WowPacketParser.Enums
{
    public enum MovementForceType : uint
    {
        SingleDirectional   = 0, // always in a single direction
        Gravity             = 1  // pushes/pulls away from a single point
    }
}

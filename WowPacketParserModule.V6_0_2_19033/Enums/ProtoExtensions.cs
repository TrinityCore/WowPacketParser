using WowPacketParser.Proto;

namespace WowPacketParserModule.V6_0_2_19033.Enums
{
    public static class ProtoExtensions
    {
        public static UniversalSplineFlag ToUniversal(this SplineFlag flags)
        {
            UniversalSplineFlag universal = UniversalSplineFlag.SplineFlagNone;
            if (flags.HasFlag(SplineFlag.JumpOrientationFixed))
                universal |= UniversalSplineFlag.JumpOrientationFixed;
            if (flags.HasFlag(SplineFlag.FallingSlow))
                universal |= UniversalSplineFlag.FallingSlow;
            if (flags.HasFlag(SplineFlag.Done))
                universal |= UniversalSplineFlag.Done;
            if (flags.HasFlag(SplineFlag.Falling))
                universal |= UniversalSplineFlag.Falling;
            if (flags.HasFlag(SplineFlag.NoSpline))
                universal |= UniversalSplineFlag.NoSpline;
            if (flags.HasFlag(SplineFlag.Flying))
                universal |= UniversalSplineFlag.Flying;
            if (flags.HasFlag(SplineFlag.OrientationFixed))
                universal |= UniversalSplineFlag.OrientationFixed;
            if (flags.HasFlag(SplineFlag.CatmullRom))
                universal |= UniversalSplineFlag.CatmullRom;
            if (flags.HasFlag(SplineFlag.Cyclic))
                universal |= UniversalSplineFlag.Cyclic;
            if (flags.HasFlag(SplineFlag.EnterCycle))
                universal |= UniversalSplineFlag.EnterCycle;
            if (flags.HasFlag(SplineFlag.Turning))
                universal |= UniversalSplineFlag.Turning;
            if (flags.HasFlag(SplineFlag.TransportEnter))
                universal |= UniversalSplineFlag.TransportEnter;
            if (flags.HasFlag(SplineFlag.TransportExit))
                universal |= UniversalSplineFlag.TransportExit;
            if (flags.HasFlag(SplineFlag.Backward))
                universal |= UniversalSplineFlag.Backward;
            if (flags.HasFlag(SplineFlag.SmoothGroundPath))
                universal |= UniversalSplineFlag.SmoothGroundPath;
            if (flags.HasFlag(SplineFlag.CanSwim))
                universal |= UniversalSplineFlag.CanSwim;
            if (flags.HasFlag(SplineFlag.UncompressedPath))
                universal |= UniversalSplineFlag.UncompressedPath;
            if (flags.HasFlag(SplineFlag.FastSteering))
                universal |= UniversalSplineFlag.FastSteering;
            if (flags.HasFlag(SplineFlag.Animation))
                universal |= UniversalSplineFlag.Animation;
            if (flags.HasFlag(SplineFlag.Parabolic))
                universal |= UniversalSplineFlag.Parabolic;
            if (flags.HasFlag(SplineFlag.FadeObject))
                universal |= UniversalSplineFlag.FadeObject;
            if (flags.HasFlag(SplineFlag.Steering))
                universal |= UniversalSplineFlag.Steering;
            if (flags.HasFlag(SplineFlag.UnlimitedSpeed))
                universal |= UniversalSplineFlag.UnlimitedSpeed;
            return universal;
        }
    }
}

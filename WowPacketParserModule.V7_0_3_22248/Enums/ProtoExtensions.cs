using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Proto;

namespace WowPacketParserModule.V7_0_3_22248.Enums
{
    public static class ProtoExtensions
    {
        public static UniversalSplineFlag ToUniversal(this SplineFlag flags)
        {
            UniversalSplineFlag universal = UniversalSplineFlag.SplineFlagNone;
            if (ClientVersion.RemovedInVersion(ClientType.Shadowlands))
            {
                if (flags.HasFlag(SplineFlag.AnimTierSwim))
                    universal |= UniversalSplineFlag.AnimTierSwim;
                if (flags.HasFlag(SplineFlag.AnimTierHover))
                    universal |= UniversalSplineFlag.AnimTierHover;
                if (flags.HasFlag(SplineFlag.AnimTierSubmerged))
                    universal |= UniversalSplineFlag.AnimTierSubmerged;
            }
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
            if (flags.HasFlag(SplineFlag.Catmullrom))
                universal |= UniversalSplineFlag.Catmullrom;
            if (flags.HasFlag(SplineFlag.Cyclic))
                universal |= UniversalSplineFlag.Cyclic;
            if (flags.HasFlag(SplineFlag.EnterCycle))
                universal |= UniversalSplineFlag.EnterCycle;
            if (flags.HasFlag(SplineFlag.Turning))
                universal |= UniversalSplineFlag.Frozen;
            if (flags.HasFlag(SplineFlag.TransportEnter))
                universal |= UniversalSplineFlag.TransportEnter;
            if (flags.HasFlag(SplineFlag.TransportExit))
                universal |= UniversalSplineFlag.TransportExit;
            if (flags.HasFlag(SplineFlag.SmoothGroundPath))
                universal |= UniversalSplineFlag.SmoothGroundPath;
            if (flags.HasFlag(SplineFlag.CanSwim))
                universal |= UniversalSplineFlag.CanSwim;
            if (flags.HasFlag(SplineFlag.UncompressedPath))
                universal |= UniversalSplineFlag.UncompressedPath;
            if (flags.HasFlag(SplineFlag.Animation))
                universal |= UniversalSplineFlag.Animation;
            if (flags.HasFlag(SplineFlag.Parabolic))
                universal |= UniversalSplineFlag.Parabolic;
            if (flags.HasFlag(SplineFlag.FadeObject))
                universal |= UniversalSplineFlag.FadeObject;
            if (flags.HasFlag(SplineFlag.Steering))
                universal |= UniversalSplineFlag.Steering;
            return universal;
        }
    }
}

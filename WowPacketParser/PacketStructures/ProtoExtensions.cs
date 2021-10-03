using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Proto;
// ReSharper disable BitwiseOperatorOnEnumWithoutFlags

namespace WowPacketParser.PacketStructures
{
    public static class ProtoExtensions
    {
        public static UniversalGuid ToUniversal(this WowGuid128 guid)
        {
            return new UniversalGuid()
            {
                Entry = guid.GetEntry(),
                Type = (UniversalHighGuid)(int)guid.HighGuid.GetHighGuidType(),
                Guid128 = new UniversalGuid128()
                {
                    Low = guid.Low,
                    High = guid.High
                }
            };
        }

        public static UniversalGuid ToUniversal(this WowGuid64 guid)
        {
            return new UniversalGuid()
            {
                Entry = guid.GetEntry(),
                Type = (UniversalHighGuid)(int)guid.HighGuid.GetHighGuidType(),
                Guid64 = new UniversalGuid64()
                {
                    Low = guid.Low,
                    High = guid.High
                }
            };
        }

        public static UniversalAuraFlag ToUniversal(this AuraFlagMoP flags)
        {
            UniversalAuraFlag universal = UniversalAuraFlag.None;
            if (flags.HasFlag(AuraFlagMoP.NoCaster))
                universal |= UniversalAuraFlag.NoCaster;
            if (flags.HasFlag(AuraFlagMoP.Positive))
                universal |= UniversalAuraFlag.Positive;
            if (flags.HasFlag(AuraFlagMoP.Duration))
                universal |= UniversalAuraFlag.Duration;
            if (flags.HasFlag(AuraFlagMoP.Scalable))
                universal |= UniversalAuraFlag.Scalable;
            if (flags.HasFlag(AuraFlagMoP.Negative))
                universal |= UniversalAuraFlag.Negative;
            if (flags.HasFlag(AuraFlagMoP.MawPower))
                universal |= UniversalAuraFlag.MawPower;
            return universal;
        }

        public static UniversalAuraFlag ToUniversal(this AuraFlag flags)
        {
            UniversalAuraFlag universal = UniversalAuraFlag.None;
            if (flags.HasFlag(AuraFlag.NotCaster))
                universal |= UniversalAuraFlag.NoCaster;
            if (flags.HasFlag(AuraFlag.Positive))
                universal |= UniversalAuraFlag.Positive;
            if (flags.HasFlag(AuraFlag.Duration))
                universal |= UniversalAuraFlag.Duration;
            if (flags.HasFlag(AuraFlag.Scalable))
                universal |= UniversalAuraFlag.Scalable;
            if (flags.HasFlag(AuraFlag.Negative))
                universal |= UniversalAuraFlag.Negative;
            if (flags.HasFlag(AuraFlag.EffectIndex0))
                universal |= UniversalAuraFlag.EffectIndex0;
            if (flags.HasFlag(AuraFlag.EffectIndex1))
                universal |= UniversalAuraFlag.EffectIndex1;
            if (flags.HasFlag(AuraFlag.EffectIndex2))
                universal |= UniversalAuraFlag.EffectIndex2;
            return universal;
        }

        public static UniversalSplineFlag ToUniversal(this SplineFlag422 flags)
        {
            UniversalSplineFlag universal = UniversalSplineFlag.SplineFlagNone;
            if (flags.HasFlag(SplineFlag422.AnimTierSwim))
                universal |= UniversalSplineFlag.AnimTierSwim;
            if (flags.HasFlag(SplineFlag422.AnimTierHover))
                universal |= UniversalSplineFlag.AnimTierHover;
            if (flags.HasFlag(SplineFlag422.AnimTierSubmerged))
                universal |= UniversalSplineFlag.AnimTierSubmerged;
            if (flags.HasFlag(SplineFlag422.Falling))
                universal |= UniversalSplineFlag.Falling;
            if (flags.HasFlag(SplineFlag422.NoSpline))
                universal |= UniversalSplineFlag.NoSpline;
            if (flags.HasFlag(SplineFlag422.Trajectory))
                universal |= UniversalSplineFlag.Trajectory;
            if (flags.HasFlag(SplineFlag422.Walking))
                universal |= UniversalSplineFlag.Walkmode;
            if (flags.HasFlag(SplineFlag422.Flying))
                universal |= UniversalSplineFlag.Flying;
            if (flags.HasFlag(SplineFlag422.FixedOrientation))
                universal |= UniversalSplineFlag.OrientationFixed;
            if (flags.HasFlag(SplineFlag422.FinalPoint))
                universal |= UniversalSplineFlag.FinalPoint;
            if (flags.HasFlag(SplineFlag422.FinalTarget))
                universal |= UniversalSplineFlag.FinalTarget;
            if (flags.HasFlag(SplineFlag422.FinalOrientation))
                universal |= UniversalSplineFlag.FinalAngle;
            if (flags.HasFlag(SplineFlag422.CatmullRom))
                universal |= UniversalSplineFlag.Catmullrom;
            if (flags.HasFlag(SplineFlag422.Cyclic))
                universal |= UniversalSplineFlag.Cyclic;
            if (flags.HasFlag(SplineFlag422.EnterCicle))
                universal |= UniversalSplineFlag.EnterCycle;
            if (flags.HasFlag(SplineFlag422.AnimationTier))
                universal |= UniversalSplineFlag.AnimationTier;
            if (flags.HasFlag(SplineFlag422.Frozen))
                universal |= UniversalSplineFlag.Frozen;
            if (flags.HasFlag(SplineFlag422.MovingBackwards))
                universal |= UniversalSplineFlag.OrientationInversed;
            if (flags.HasFlag(SplineFlag422.UsePathSmoothing))
                universal |= UniversalSplineFlag.SmoothGroundPath;
            if (flags.HasFlag(SplineFlag422.Animation))
                universal |= UniversalSplineFlag.Animation;
            if (flags.HasFlag(SplineFlag422.UncompressedPath))
                universal |= UniversalSplineFlag.UncompressedPath;
            return universal;
        }

        public static UniversalSplineFlag ToUniversal(this SplineFlag434 flags)
        {
            UniversalSplineFlag universal = UniversalSplineFlag.SplineFlagNone;
            if (flags.HasFlag(SplineFlag434.AnimTierSwim))
                universal |= UniversalSplineFlag.AnimTierSwim;
            if (flags.HasFlag(SplineFlag434.AnimTierHover))
                universal |= UniversalSplineFlag.AnimTierHover;
            if (flags.HasFlag(SplineFlag434.AnimTierSubmerged))
                universal |= UniversalSplineFlag.AnimTierSubmerged;
            if (flags.HasFlag(SplineFlag434.Done))
                universal |= UniversalSplineFlag.Done;
            if (flags.HasFlag(SplineFlag434.Falling))
                universal |= UniversalSplineFlag.Falling;
            if (flags.HasFlag(SplineFlag434.NoSpline))
                universal |= UniversalSplineFlag.NoSpline;
            if (flags.HasFlag(SplineFlag434.Flying))
                universal |= UniversalSplineFlag.Flying;
            if (flags.HasFlag(SplineFlag434.OrientationFixed))
                universal |= UniversalSplineFlag.OrientationFixed;
            if (flags.HasFlag(SplineFlag434.Catmullrom))
                universal |= UniversalSplineFlag.Catmullrom;
            if (flags.HasFlag(SplineFlag434.Cyclic))
                universal |= UniversalSplineFlag.Cyclic;
            if (flags.HasFlag(SplineFlag434.EnterCycle))
                universal |= UniversalSplineFlag.EnterCycle;
            if (flags.HasFlag(SplineFlag434.Frozen))
                universal |= UniversalSplineFlag.Frozen;
            if (flags.HasFlag(SplineFlag434.TransportEnter))
                universal |= UniversalSplineFlag.TransportEnter;
            if (flags.HasFlag(SplineFlag434.TransportExit))
                universal |= UniversalSplineFlag.TransportExit;
            if (flags.HasFlag(SplineFlag434.OrientationInversed))
                universal |= UniversalSplineFlag.OrientationInversed;
            if (flags.HasFlag(SplineFlag434.Walkmode))
                universal |= UniversalSplineFlag.Walkmode;
            if (flags.HasFlag(SplineFlag434.UncompressedPath))
                universal |= UniversalSplineFlag.UncompressedPath;
            if (flags.HasFlag(SplineFlag434.Animation))
                universal |= UniversalSplineFlag.Animation;
            if (flags.HasFlag(SplineFlag434.Parabolic))
                universal |= UniversalSplineFlag.Parabolic;
            if (flags.HasFlag(SplineFlag434.FinalPoint))
                universal |= UniversalSplineFlag.FinalPoint;
            if (flags.HasFlag(SplineFlag434.FinalTarget))
                universal |= UniversalSplineFlag.FinalTarget;
            if (flags.HasFlag(SplineFlag434.FinalAngle))
                universal |= UniversalSplineFlag.FinalAngle;
            return universal;
        }

        public static UniversalSplineFlag ToUniversal(this SplineFlag flags)
        {
            UniversalSplineFlag universal = UniversalSplineFlag.SplineFlagNone;
            if (flags.HasFlag(SplineFlag.AnimTierSwim))
                universal |= UniversalSplineFlag.AnimTierSwim;
            if (flags.HasFlag(SplineFlag.AnimTierHover))
                universal |= UniversalSplineFlag.AnimTierHover;
            if (flags.HasFlag(SplineFlag.AnimTierSubmerged))
                universal |= UniversalSplineFlag.AnimTierSubmerged;
            if (flags.HasFlag(SplineFlag.Done))
                universal |= UniversalSplineFlag.Done;
            if (flags.HasFlag(SplineFlag.Falling))
                universal |= UniversalSplineFlag.Falling;
            if (flags.HasFlag(SplineFlag.NoSpline))
                universal |= UniversalSplineFlag.NoSpline;
            if (flags.HasFlag(SplineFlag.Trajectory))
                universal |= UniversalSplineFlag.Trajectory;
            if (flags.HasFlag(SplineFlag.WalkMode))
                universal |= UniversalSplineFlag.Walkmode;
            if (flags.HasFlag(SplineFlag.Flying))
                universal |= UniversalSplineFlag.Flying;
            if (flags.HasFlag(SplineFlag.Knockback))
                universal |= UniversalSplineFlag.Knockback;
            if (flags.HasFlag(SplineFlag.FinalPoint))
                universal |= UniversalSplineFlag.FinalPoint;
            if (flags.HasFlag(SplineFlag.FinalTarget))
                universal |= UniversalSplineFlag.FinalTarget;
            if (flags.HasFlag(SplineFlag.FinalOrientation))
                universal |= UniversalSplineFlag.FinalAngle;
            if (flags.HasFlag(SplineFlag.CatmullRom))
                universal |= UniversalSplineFlag.Catmullrom;
            if (flags.HasFlag(SplineFlag.Cyclic))
                universal |= UniversalSplineFlag.Cyclic;
            if (flags.HasFlag(SplineFlag.EnterCicle))
                universal |= UniversalSplineFlag.EnterCycle;
            if (flags.HasFlag(SplineFlag.AnimationTier))
                universal |= UniversalSplineFlag.AnimationTier;
            if (flags.HasFlag(SplineFlag.Frozen))
                universal |= UniversalSplineFlag.Frozen;
            if (flags.HasFlag(SplineFlag.Transport))
                universal |= UniversalSplineFlag.TransportEnter;
            if (flags.HasFlag(SplineFlag.TransportExit))
                universal |= UniversalSplineFlag.TransportExit;
            if (flags.HasFlag(SplineFlag.OrientationInverted))
                universal |= UniversalSplineFlag.OrientationInversed;
            if (flags.HasFlag(SplineFlag.UsePathSmoothing))
                universal |= UniversalSplineFlag.SmoothGroundPath;
            if (flags.HasFlag(SplineFlag.Animation))
                universal |= UniversalSplineFlag.Animation;
            if (flags.HasFlag(SplineFlag.UncompressedPath))
                universal |= UniversalSplineFlag.UncompressedPath;
            return universal;
        }
        
        public static CreateObjectType ToCreateObjectType(this UpdateTypeCataclysm updateTypeCataclysm)
        {
            if (updateTypeCataclysm == UpdateTypeCataclysm.CreateObject1)
                return CreateObjectType.InRange;
            if (updateTypeCataclysm == UpdateTypeCataclysm.CreateObject2)
                return CreateObjectType.Spawn;
            throw new ArgumentOutOfRangeException();
        }
    }
}

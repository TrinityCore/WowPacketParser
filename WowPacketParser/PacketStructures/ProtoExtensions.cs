using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WoWPacketParser.Proto;
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
    }
}
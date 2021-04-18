using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum DBCPhaseFlags : uint
    {
        ReadOnly              = 0x00001,
        InternalPhase         = 0x00002,
        UsesPlayerCondition   = 0x00004,
        Normal                = 0x00008,
        Cosmetic              = 0x00010,
        Personal              = 0x00020,
        Expensive             = 0x00040,
        EventsAreObservable   = 0x00080,
        UsesPreloadConditions = 0x00100,
        UnshareablePersonal   = 0x00200,
        ObjectsAreVisible     = 0x00400
    }

    [Flags]
    public enum PhaseFlags : uint
    {
        None     = 0x0,
        Cosmetic = 0x1,
        Personal = 0x2
    }
}

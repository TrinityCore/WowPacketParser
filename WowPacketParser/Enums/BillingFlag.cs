using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum BillingFlag
    {
        None             = 0x00,
        Unused           = 0x01,
        Subscription     = 0x02,
        Trial            = 0x04,
        IGR              = 0x08,
        PersonalPlan     = 0x10,
        TimeMixture      = 0x20,
        Restricted       = 0x40,
        EnableCais       = 0x80
    }
}

using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum BillingFlag
    {
        None             = 0x00,
        Unused           = 0x01,
        RecurringBilling = 0x02,
        Trial1           = 0x04,
        Igr              = 0x08,
        Usage            = 0x10,
        TimeMixture      = 0x20,
        Restricted       = 0x40,
        EnableCais       = 0x80
    }
}

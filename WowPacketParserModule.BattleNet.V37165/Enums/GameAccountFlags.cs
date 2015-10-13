using System;

namespace WowPacketParserModule.BattleNet.V37165.Enums
{
    [Flags]
    public enum GameAccountFlags : ulong
    {
        Gm = 0x00000001,
        NoKick = 0x00000002,
        Collector = 0x00000004,
        WowTrial = 0x00000008,
        Cancelled = 0x00000010,
        Igr = 0x00000020,
        Wholesaler = 0x00000040,
        Privileged = 0x00000080,
        EuForbidELV = 0x00000100,
        EuForbidBilling = 0x00000200,
        WowRestricted = 0x00000400,
        ParentalControl = 0x00000800,
        Referral = 0x00001000,
        Blizzard = 0x00002000,
        RecurringBilling = 0x00004000,
        NoElectup = 0x00008000,
        KrCertificate = 0x00010000,
        ExpansionCollector = 0x00020000,
        DisableVoice = 0x00040000,
        DisableVoiceSpeak = 0x00080000,
        ReferralResurrect = 0x00100000,
        EuForbidCC = 0x00200000,
        OpenbetaDell = 0x00400000,
        Propass = 0x00800000,
        PropassLock = 0x01000000,
        PendingUpgrade = 0x02000000,
        RetailFromTrial = 0x04000000,
        Expansion2Collector = 0x08000000,
        OvermindLinked = 0x10000000,
        Demos = 0x20000000,
        DeathKnightOk = 0x40000000,
    }
}

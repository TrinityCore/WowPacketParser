using System;

namespace WowPacketParserModule.BattleNet.V37165.Enums
{
    [Flags]
    public enum AccountFlags : ulong
    {
        Incomplete = 0x1,
        MailVerified = 0x2,
        Employee = 0x4,
        Admin = 0x8,
        SupportEmployee = 0x10,
        Newsletter = 0x20,
        ParentAgreementKr = 0x40,
        InsiderSmsKr = 0x80,
        CancelledKr = 0x100,
        BetaSignUp = 0x200,
        PurchaseBan = 0x400,
        LegalAccept = 0x800,
        Press = 0x1000,
        OneTimeEvent = 0x2000,
        TrialIncomplete = 0x4000,
        MarketingEmail = 0x8000,
        KrAgeNotVerified = 0x10000,
        SecLockMustRelease = 0x20000,
        SecLocked = 0x40000,
        RidDisabled = 0x80000,
        DontShowToDirectFriend = 0x100000,
        ParentalControl = 0x200000,
        ParentAgree14Kr = 0x400000,
        PlaySummaryEmail = 0x800000,
        PrivNetworkRqd = 0x1000000,
        PrimaryRidInviteRequiresEmployeeFlag = 0x2000000,
        VoiceChatDisabled = 0x4000000,
        VoiceSpeakDisabled = 0x8000000,
        HideFromFacebookFriendFinder = 0x10000000,
        EuForbidElv = 0x20000000,
        PhoneSecureEnhanced = 0x40000000,
    }
}

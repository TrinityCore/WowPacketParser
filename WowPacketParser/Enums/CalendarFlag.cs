﻿using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum CalendarFlag
    {
        None              = 0x000000,
        Normal            = 0x000001,
        Guild             = 0x000002,
        System            = 0x000004,
        Holiday           = 0x000008,
        InvitesLocked     = 0x000010,
        AutoApprove       = 0x000020,
        GuildAnnouncement = 0x000040, // CommunityAnnouncement in 8.x
        RaidLockout       = 0x000080,
        Arena             = 0x000100,
        RaidReset         = 0x000200,
        GuildEvent        = 0x000400, // CommunitySignup in 8.x
        GuildSignup       = 0x000800,
        Unk10000          = 0x010000,
        Unk400000         = 0x400000
    }
}

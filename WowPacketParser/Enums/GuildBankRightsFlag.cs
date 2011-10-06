using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum GuildBankRightsFlag
    {
        None       = 0x00,
        View       = 0x01,
        Withdraw   = 0x02,
        UpdateText = 0x04,
        All        = -1,
    }
}

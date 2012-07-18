using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum GuildBankRightsFlag
    {
        None         = 0x00,
        ViewTab      = 0x01,
        WithdrawItem = 0x02,
        UpdateText   = 0x04,
        All          = -1
    }
}

using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGuildBankWithdrawMoney
    {
        public ulong Banker;
        public ulong Money;
    }
}

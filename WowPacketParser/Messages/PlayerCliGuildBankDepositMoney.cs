using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGuildBankDepositMoney
    {
        public ulong Money;
        public ulong Banker;
    }
}

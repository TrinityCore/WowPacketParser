using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGuildBankBuyTab
    {
        public ulong Banker;
        public byte BankTab;
    }
}

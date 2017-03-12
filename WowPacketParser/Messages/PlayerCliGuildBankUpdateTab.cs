using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGuildBankUpdateTab
    {
        public string Icon;
        public string Name;
        public byte BankTab;
        public ulong Banker;
    }
}

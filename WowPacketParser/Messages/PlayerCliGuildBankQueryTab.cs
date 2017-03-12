using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGuildBankQueryTab
    {
        public ulong Banker;
        public bool FullUpdate;
        public byte Tab;
    }
}

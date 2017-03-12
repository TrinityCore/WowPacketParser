using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGuildBankActivate
    {
        public ulong Banker;
        public bool FullUpdate;
    }
}

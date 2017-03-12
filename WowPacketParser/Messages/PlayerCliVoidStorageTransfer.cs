using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliVoidStorageTransfer
    {
        public List<ulong> Withdrawals;
        public List<ulong> Deposits;
        public ulong Npc;
    }
}

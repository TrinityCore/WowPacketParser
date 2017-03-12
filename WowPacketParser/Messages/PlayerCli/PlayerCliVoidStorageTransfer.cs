using System.Collections.Generic;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliVoidStorageTransfer
    {
        public List<ulong> Withdrawals;
        public List<ulong> Deposits;
        public ulong Npc;
    }
}

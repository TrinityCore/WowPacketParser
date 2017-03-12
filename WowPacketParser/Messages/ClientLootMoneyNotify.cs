using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLootMoneyNotify
    {
        public uint Money;
        public bool SoleLooter;
    }
}

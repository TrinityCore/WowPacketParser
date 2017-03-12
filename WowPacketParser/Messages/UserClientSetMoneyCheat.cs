using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientSetMoneyCheat
    {
        public string Target;
        public ulong Amount;
    }
}

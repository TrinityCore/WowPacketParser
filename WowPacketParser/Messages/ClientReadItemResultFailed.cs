using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientReadItemResultFailed
    {
        public ulong Item;
        public ReadItemFailure Subcode;
        public int Delay;
    }
}

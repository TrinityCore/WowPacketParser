using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
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

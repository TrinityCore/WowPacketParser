using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPetitionShowList
    {
        public ulong Unit;
        public uint Price;
    }
}

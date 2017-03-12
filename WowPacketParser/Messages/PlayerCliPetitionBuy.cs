using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliPetitionBuy
    {
        public ulong Unit;
        public string Title;
    }
}

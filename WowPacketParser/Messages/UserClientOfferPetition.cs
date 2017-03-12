using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientOfferPetition
    {
        public ulong TargetPlayer;
        public ulong ItemGUID;
    }
}

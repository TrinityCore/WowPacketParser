using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliUnitAnimTierCheat
    {
        public ulong TargetGUID;
        public int NewTier;
    }
}

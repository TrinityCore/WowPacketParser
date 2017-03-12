using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDynamicDropRollResult
    {
        public byte DynamicDropResult;
        public float Roll;
        public float Chance;
        public ulong LosingPlayerGUID;
        public uint BiggestLosingStreak;
        public uint ItemID;
    }
}

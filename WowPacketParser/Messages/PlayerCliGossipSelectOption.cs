using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGossipSelectOption
    {
        public ulong GossipUnit;
        public int GossipIndex;
        public int GossipID;
        public string PromotionCode;
    }
}

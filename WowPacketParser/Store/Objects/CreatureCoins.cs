using System;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects
{
    public class CreatureCoins
    {
        public WowGuid WowGuid { get; set; }
        public UInt32 Coins { get; set; }
        public int? Level { get; set; }
    }
}

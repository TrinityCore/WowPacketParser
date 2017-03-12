using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct SpellCastLogData
    {
        public int Health;
        public int AttackPower;
        public int SpellPower;
        public List<SpellLogPowerData> PowerData;
    }
}

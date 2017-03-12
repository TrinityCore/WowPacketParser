using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct SpellCastLogData
    {
        public int Health;
        public int AttackPower;
        public int SpellPower;
        public List<SpellLogPowerData> PowerData;
    }
}

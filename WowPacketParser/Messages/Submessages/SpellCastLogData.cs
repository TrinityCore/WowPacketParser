using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct SpellCastLogData
    {
        public int Health;
        public int AttackPower;
        public int SpellPower;
        public List<SpellLogPowerData> PowerData;

        public static void Read6(Packet packet, params object[] idx)
        {
            packet.ReadInt32("Health", idx);
            packet.ReadInt32("AttackPower", idx);
            packet.ReadInt32("SpellPower", idx);

            var int3 = packet.ReadInt32("SpellLogPowerData", idx);

            // SpellLogPowerData
            for (var i = 0; i < int3; ++i)
            {
                packet.ReadInt32("PowerType", idx, i);
                packet.ReadInt32("Amount", idx, i);
            }

            packet.ResetBitReader();

            var bit32 = packet.ReadBit("bit32", idx);

            if (bit32)
                packet.ReadSingle("Float7", idx);
        }

        public static void Read7(Packet packet, params object[] idx)
        {
            packet.ReadInt64("Health", idx);
            packet.ReadInt32("AttackPower", idx);
            packet.ReadInt32("SpellPower", idx);

            packet.ResetBitReader();

            var spellLogPowerDataCount = packet.ReadBits("SpellLogPowerData", 9, idx);

            // SpellLogPowerData
            for (var i = 0; i < spellLogPowerDataCount; ++i)
            {
                packet.ReadInt32("PowerType", idx, i);
                packet.ReadInt32("Amount", idx, i);
                packet.ReadInt32("Cost", idx, i);
            }
        }
    }
}

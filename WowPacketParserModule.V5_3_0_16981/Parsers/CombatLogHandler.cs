using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class CombatLogHandler
    {
        [Parser(Opcode.SMSG_SPELL_PERIODIC_AURA_LOG)]
        public static void HandlePeriodicAuraLog(Packet packet)
        {
            var casterGUID = new byte[8];
            var targetGUID = new byte[8];
            var powerGUID = new byte[8];

            packet.StartBitStream(casterGUID, 7);
            packet.StartBitStream(targetGUID, 7);
            packet.StartBitStream(casterGUID, 4, 0);
            packet.StartBitStream(targetGUID, 2, 6);
            var hasPowerData = packet.ReadBit();

            var powerCount = 0u;
            if (hasPowerData)
            {
                packet.StartBitStream(powerGUID, 2, 0);
                powerCount = packet.ReadBits(21);
                packet.StartBitStream(powerGUID, 4, 1, 5, 7, 3, 6);
            }

            var periodicAuraLogEffectCount = (int)packet.ReadBits(21);
            packet.StartBitStream(targetGUID, 5, 4, 3);
            packet.StartBitStream(casterGUID, 1);
            packet.StartBitStream(targetGUID, 0);

            var hasOverDamage = new bool[periodicAuraLogEffectCount];
            var hasAbsorb = new bool[periodicAuraLogEffectCount];
            var hasResist = new bool[periodicAuraLogEffectCount];
            var hasSchoolMask = new bool[periodicAuraLogEffectCount];
            var isCrit = new bool[periodicAuraLogEffectCount];

            for (var i = 0; i < periodicAuraLogEffectCount; ++i)
            {
                hasSchoolMask[i] = !packet.ReadBit(); // +12
                hasOverDamage[i] = !packet.ReadBit(); // +8
                isCrit[i] = packet.ReadBit(); // +24
                hasResist[i] = !packet.ReadBit(); // +20
                hasAbsorb[i] = !packet.ReadBit(); // +16
            }

            packet.StartBitStream(casterGUID, 5, 2, 3);
            packet.StartBitStream(targetGUID, 1);
            packet.StartBitStream(casterGUID, 6);

            packet.ResetBitReader();

            if (hasPowerData)
            {
                packet.ReadXORBytes(powerGUID, 7, 5);
                packet.ReadInt32("Spell Power");
                for (var i = 0; i < powerCount; ++i)
                {
                    packet.ReadInt32("Power Amount", i);
                    packet.ReadInt32E<PowerType>("Power Type", i);
                }

                packet.ReadInt32("Attack Power");
                packet.ReadXORBytes(powerGUID, 4, 6, 3);
                packet.ReadInt32("Current Health");
                packet.ReadXORBytes(powerGUID, 0, 1, 2);

                packet.WriteGuid("Power GUID", powerGUID);
            }

            packet.ReadXORByte(targetGUID, 3);
            packet.ReadXORByte(casterGUID, 6);

            for (var i = 0; i < periodicAuraLogEffectCount; ++i)
            {
                packet.ReadInt32E<AuraType>("Aura Type", i);
                packet.ReadInt32("Damage", i);

                packet.AddValue("Crit", isCrit[i], i);

                if (hasOverDamage[i])
                    packet.ReadInt32("Over damage", i); // +8

                if (hasAbsorb[i])
                    packet.ReadInt32("Absorb", i); // +16

                if (hasResist[i])
                    packet.ReadInt32("Resist", i); // +20

                if (hasSchoolMask[i])
                    packet.ReadUInt32E<SpellSchoolMask>("School Mask", i); // +12
            }

            packet.ReadXORByte(casterGUID, 4);
            packet.ReadXORByte(targetGUID, 6);
            packet.ReadInt32<SpellId>("Spell ID");
            packet.ReadXORBytes(casterGUID, 7, 2);
            packet.ReadXORBytes(targetGUID, 4, 7, 0);
            packet.ReadXORByte(casterGUID, 1);
            packet.ReadXORByte(targetGUID, 2);
            packet.ReadXORBytes(casterGUID, 0, 3);
            packet.ReadXORBytes(targetGUID, 5, 1);
            packet.ReadXORByte(casterGUID, 5);

            packet.WriteGuid("Caster GUID", casterGUID);
            packet.WriteGuid("Target GUID", targetGUID);
        }

        [Parser(Opcode.SMSG_SPELL_ENERGIZE_LOG)]
        public static void HandleSpellEnergizeLog(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var powerGUID = new byte[8];

            packet.ReadInt32("Amount");
            packet.ReadUInt32E<PowerType>("Power Type");
            packet.ReadInt32<SpellId>("Spell ID");

            packet.StartBitStream(guid1, 1, 7);
            packet.StartBitStream(guid2, 0);
            packet.StartBitStream(guid1, 5);
            packet.StartBitStream(guid2, 4, 6, 3);
            var hasPowerData = packet.ReadBit();
            packet.StartBitStream(guid1, 4);

            var powerCount = 0u;
            if (hasPowerData)
            {
                powerCount = packet.ReadBits(21);
                packet.StartBitStream(powerGUID, 7, 0, 2, 6, 1, 4, 3, 5);
            }

            packet.StartBitStream(guid2, 1, 7);
            packet.StartBitStream(guid1, 6);
            packet.StartBitStream(guid2, 2);
            packet.StartBitStream(guid1, 3, 2, 0);
            packet.StartBitStream(guid2, 5);

            packet.ResetBitReader();

            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 2);
            if (hasPowerData)
            {
                packet.ReadXORBytes(powerGUID, 0, 1, 7);

                packet.ReadInt32("Current Health");

                for (var i = 0; i < powerCount; ++i)
                {
                    packet.ReadInt32E<PowerType>("Power Type", i);
                    packet.ReadInt32("Power Amount", i);
                }

                packet.ReadXORByte(powerGUID, 4);

                packet.ReadInt32("Attack Power");
                packet.ReadXORBytes(powerGUID, 2, 5, 3, 6);
                packet.ReadInt32("Spell Power");

                packet.WriteGuid("Power GUID", powerGUID);
            }

            packet.ReadXORByte(guid1, 1);
            packet.ReadXORBytes(guid2, 7, 3);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORBytes(guid1, 3, 7, 5);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORBytes(guid1, 6, 2);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORBytes(guid2, 0, 4);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }
    }
}

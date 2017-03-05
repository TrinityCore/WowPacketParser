using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.SMSG_ATTACK_START)]
        public static void HandleAttackStartStart(Packet packet)
        {
            var victimGUID = new byte[8];
            var attackerGUID = new byte[8];

            packet.Translator.StartBitStream(victimGUID, 6, 2, 1);
            attackerGUID[6] = packet.Translator.ReadBit();
            victimGUID[5] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(attackerGUID, 1, 3, 0);
            packet.Translator.StartBitStream(victimGUID, 0, 7);
            packet.Translator.StartBitStream(attackerGUID, 7, 5, 4);
            packet.Translator.StartBitStream(victimGUID, 4, 3);
            attackerGUID[2] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(attackerGUID, 4);
            packet.Translator.ReadXORByte(victimGUID, 4);
            packet.Translator.ReadXORBytes(attackerGUID, 2, 6);
            packet.Translator.ReadXORByte(victimGUID, 7);
            packet.Translator.ReadXORByte(attackerGUID, 0);
            packet.Translator.ReadXORByte(victimGUID, 5);
            packet.Translator.ReadXORByte(attackerGUID, 1);
            packet.Translator.ReadXORBytes(victimGUID, 2, 6);
            packet.Translator.ReadXORByte(attackerGUID, 7);
            packet.Translator.ReadXORByte(victimGUID, 3);
            packet.Translator.ReadXORByte(attackerGUID, 5);
            packet.Translator.ReadXORByte(victimGUID, 0);
            packet.Translator.ReadXORByte(attackerGUID, 3);
            packet.Translator.ReadXORByte(victimGUID, 1);

            packet.Translator.WriteGuid("Attacker GUID", attackerGUID);
            packet.Translator.WriteGuid("Victim GUID", victimGUID);
        }

        [Parser(Opcode.SMSG_ATTACK_STOP)]
        public static void HandleAttackStartStop(Packet packet)
        {
            var victimGUID = new byte[8];
            var attackerGUID = new byte[8];

            victimGUID[4] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(attackerGUID, 1, 3, 0, 6, 5);
            victimGUID[1] = packet.Translator.ReadBit();
            attackerGUID[7] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(victimGUID, 5, 6, 0);
            packet.Translator.StartBitStream(attackerGUID, 2, 4);
            packet.Translator.StartBitStream(victimGUID, 7, 2);
            packet.Translator.ReadBit("Unk bit");
            victimGUID[3] = packet.Translator.ReadBit();

            packet.Translator.ReadXORBytes(victimGUID, 2, 0);
            packet.Translator.ReadXORBytes(attackerGUID, 5, 0, 4);
            packet.Translator.ReadXORBytes(victimGUID, 4, 6, 7);
            packet.Translator.ReadXORBytes(attackerGUID, 2, 3);
            packet.Translator.ReadXORBytes(victimGUID, 5, 1);
            packet.Translator.ReadXORBytes(attackerGUID, 1, 7);
            packet.Translator.ReadXORByte(victimGUID, 3);
            packet.Translator.ReadXORByte(attackerGUID, 6);

            packet.Translator.WriteGuid("Attacker GUID", attackerGUID);
            packet.Translator.WriteGuid("Victim GUID", victimGUID);
        }

        [Parser(Opcode.SMSG_ATTACKER_STATE_UPDATE)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            var guid = new Byte[8];

            var hitInfo = packet.Translator.ReadInt32E<SpellHitInfo>("HitInfo");
            packet.Translator.ReadPackedGuid("AttackerGUID");
            packet.Translator.ReadPackedGuid("TargetGUID");
            packet.Translator.ReadInt32("Damage");
            packet.Translator.ReadInt32("OverDamage");

            var subDmgCount = packet.Translator.ReadByte();

            for (var i = 0; i < subDmgCount; ++i)
            {
                packet.Translator.ReadInt32("SchoolMask", i);
                packet.Translator.ReadSingle("Float Damage", i);
                packet.Translator.ReadInt32("Int Damage", i);
            }

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_ABSORB | SpellHitInfo.HITINFO_FULL_ABSORB))
                for (var i = 0; i < subDmgCount; ++i)
                    packet.Translator.ReadInt32("Damage Absorbed", i);

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_RESIST | SpellHitInfo.HITINFO_FULL_RESIST))
                for (var i = 0; i < subDmgCount; ++i)
                    packet.Translator.ReadInt32("Damage Resisted", i);

            packet.Translator.ReadByteE<VictimStates>("VictimState");
            packet.Translator.ReadInt32("Unk Attacker State 0");

            packet.Translator.ReadInt32<SpellId>("Melee Spell ID ");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK))
                packet.Translator.ReadInt32("Block Amount");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_RAGE_GAIN))
                packet.Translator.ReadInt32("Rage Gained");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_UNK0))
            {
                packet.Translator.ReadInt32("Unk Attacker State 3 1");
                packet.Translator.ReadSingle("Unk Attacker State 3 2");
                packet.Translator.ReadSingle("Unk Attacker State 3 3");
                packet.Translator.ReadSingle("Unk Attacker State 3 4");
                packet.Translator.ReadSingle("Unk Attacker State 3 5");
                packet.Translator.ReadSingle("Unk Attacker State 3 6");
                packet.Translator.ReadSingle("Unk Attacker State 3 7");
                packet.Translator.ReadSingle("Unk Attacker State 3 8");
                packet.Translator.ReadSingle("Unk Attacker State 3 9");
                packet.Translator.ReadSingle("Unk Attacker State 3 10");
                packet.Translator.ReadSingle("Unk Attacker State 3 11");
                packet.Translator.ReadInt32("Unk Attacker State 3 12");
            }

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK | SpellHitInfo.HITINFO_UNK12))
                packet.Translator.ReadSingle("Unk Float");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_UNK26))
            {
                packet.Translator.ReadInt32("Unk4");
                packet.Translator.ReadInt32("Player current HP");
                packet.Translator.ReadInt32("Unk3");

                guid[7] = packet.Translator.ReadBit();
                guid[6] = packet.Translator.ReadBit();
                var counter = packet.Translator.ReadBits(21);
                guid[2] = packet.Translator.ReadBit();
                guid[0] = packet.Translator.ReadBit();
                guid[3] = packet.Translator.ReadBit();
                guid[5] = packet.Translator.ReadBit();
                guid[1] = packet.Translator.ReadBit();
                guid[4] = packet.Translator.ReadBit();

                packet.Translator.ReadXORByte(guid, 0);
                packet.Translator.ReadXORByte(guid, 5);
                packet.Translator.ReadXORByte(guid, 6);
                packet.Translator.ReadXORByte(guid, 2);

                for (var i = 0; i < counter; ++i)
                {
                    packet.Translator.ReadUInt32("unk14");
                    packet.Translator.ReadUInt32("unk6");
                }
                packet.Translator.ReadXORByte(guid, 3);
                packet.Translator.ReadXORByte(guid, 4);
                packet.Translator.ReadXORByte(guid, 1);
                packet.Translator.ReadXORByte(guid, 7);

                packet.Translator.ReadGuid("GUID");
            }
        }
    }
}

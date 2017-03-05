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

            packet.StartBitStream(victimGUID, 6, 2, 1);
            attackerGUID[6] = packet.ReadBit();
            victimGUID[5] = packet.ReadBit();
            packet.StartBitStream(attackerGUID, 1, 3, 0);
            packet.StartBitStream(victimGUID, 0, 7);
            packet.StartBitStream(attackerGUID, 7, 5, 4);
            packet.StartBitStream(victimGUID, 4, 3);
            attackerGUID[2] = packet.ReadBit();

            packet.ReadXORByte(attackerGUID, 4);
            packet.ReadXORByte(victimGUID, 4);
            packet.ReadXORBytes(attackerGUID, 2, 6);
            packet.ReadXORByte(victimGUID, 7);
            packet.ReadXORByte(attackerGUID, 0);
            packet.ReadXORByte(victimGUID, 5);
            packet.ReadXORByte(attackerGUID, 1);
            packet.ReadXORBytes(victimGUID, 2, 6);
            packet.ReadXORByte(attackerGUID, 7);
            packet.ReadXORByte(victimGUID, 3);
            packet.ReadXORByte(attackerGUID, 5);
            packet.ReadXORByte(victimGUID, 0);
            packet.ReadXORByte(attackerGUID, 3);
            packet.ReadXORByte(victimGUID, 1);

            packet.WriteGuid("Attacker GUID", attackerGUID);
            packet.WriteGuid("Victim GUID", victimGUID);
        }

        [Parser(Opcode.SMSG_ATTACK_STOP)]
        public static void HandleAttackStartStop(Packet packet)
        {
            var victimGUID = new byte[8];
            var attackerGUID = new byte[8];

            victimGUID[4] = packet.ReadBit();
            packet.StartBitStream(attackerGUID, 1, 3, 0, 6, 5);
            victimGUID[1] = packet.ReadBit();
            attackerGUID[7] = packet.ReadBit();
            packet.StartBitStream(victimGUID, 5, 6, 0);
            packet.StartBitStream(attackerGUID, 2, 4);
            packet.StartBitStream(victimGUID, 7, 2);
            packet.ReadBit("Unk bit");
            victimGUID[3] = packet.ReadBit();

            packet.ReadXORBytes(victimGUID, 2, 0);
            packet.ReadXORBytes(attackerGUID, 5, 0, 4);
            packet.ReadXORBytes(victimGUID, 4, 6, 7);
            packet.ReadXORBytes(attackerGUID, 2, 3);
            packet.ReadXORBytes(victimGUID, 5, 1);
            packet.ReadXORBytes(attackerGUID, 1, 7);
            packet.ReadXORByte(victimGUID, 3);
            packet.ReadXORByte(attackerGUID, 6);

            packet.WriteGuid("Attacker GUID", attackerGUID);
            packet.WriteGuid("Victim GUID", victimGUID);
        }

        [Parser(Opcode.SMSG_ATTACKER_STATE_UPDATE)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            var guid = new Byte[8];

            var hitInfo = packet.ReadInt32E<SpellHitInfo>("HitInfo");
            packet.ReadPackedGuid("AttackerGUID");
            packet.ReadPackedGuid("TargetGUID");
            packet.ReadInt32("Damage");
            packet.ReadInt32("OverDamage");

            var subDmgCount = packet.ReadByte();

            for (var i = 0; i < subDmgCount; ++i)
            {
                packet.ReadInt32("SchoolMask", i);
                packet.ReadSingle("Float Damage", i);
                packet.ReadInt32("Int Damage", i);
            }

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_ABSORB | SpellHitInfo.HITINFO_FULL_ABSORB))
                for (var i = 0; i < subDmgCount; ++i)
                    packet.ReadInt32("Damage Absorbed", i);

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_RESIST | SpellHitInfo.HITINFO_FULL_RESIST))
                for (var i = 0; i < subDmgCount; ++i)
                    packet.ReadInt32("Damage Resisted", i);

            packet.ReadByteE<VictimStates>("VictimState");
            packet.ReadInt32("Unk Attacker State 0");

            packet.ReadInt32<SpellId>("Melee Spell ID ");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK))
                packet.ReadInt32("Block Amount");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_RAGE_GAIN))
                packet.ReadInt32("Rage Gained");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_UNK0))
            {
                packet.ReadInt32("Unk Attacker State 3 1");
                packet.ReadSingle("Unk Attacker State 3 2");
                packet.ReadSingle("Unk Attacker State 3 3");
                packet.ReadSingle("Unk Attacker State 3 4");
                packet.ReadSingle("Unk Attacker State 3 5");
                packet.ReadSingle("Unk Attacker State 3 6");
                packet.ReadSingle("Unk Attacker State 3 7");
                packet.ReadSingle("Unk Attacker State 3 8");
                packet.ReadSingle("Unk Attacker State 3 9");
                packet.ReadSingle("Unk Attacker State 3 10");
                packet.ReadSingle("Unk Attacker State 3 11");
                packet.ReadInt32("Unk Attacker State 3 12");
            }

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK | SpellHitInfo.HITINFO_UNK12))
                packet.ReadSingle("Unk Float");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_UNK26))
            {
                packet.ReadInt32("Unk4");
                packet.ReadInt32("Player current HP");
                packet.ReadInt32("Unk3");

                guid[7] = packet.ReadBit();
                guid[6] = packet.ReadBit();
                var counter = packet.ReadBits(21);
                guid[2] = packet.ReadBit();
                guid[0] = packet.ReadBit();
                guid[3] = packet.ReadBit();
                guid[5] = packet.ReadBit();
                guid[1] = packet.ReadBit();
                guid[4] = packet.ReadBit();

                packet.ReadXORByte(guid, 0);
                packet.ReadXORByte(guid, 5);
                packet.ReadXORByte(guid, 6);
                packet.ReadXORByte(guid, 2);

                for (var i = 0; i < counter; ++i)
                {
                    packet.ReadUInt32("unk14");
                    packet.ReadUInt32("unk6");
                }
                packet.ReadXORByte(guid, 3);
                packet.ReadXORByte(guid, 4);
                packet.ReadXORByte(guid, 1);
                packet.ReadXORByte(guid, 7);

                packet.ReadGuid("GUID");
            }
        }
    }
}

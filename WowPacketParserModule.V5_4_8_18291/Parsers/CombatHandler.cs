using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.SMSG_ATTACK_START)]
        public static void HandleAttackStartStart(Packet packet)
        {
            var attackerGUID = new byte[8];
            var victimGUID = new byte[8];

            victimGUID[7] = packet.Translator.ReadBit();
            attackerGUID[7] = packet.Translator.ReadBit();
            attackerGUID[3] = packet.Translator.ReadBit();
            victimGUID[3] = packet.Translator.ReadBit();
            victimGUID[5] = packet.Translator.ReadBit();
            attackerGUID[4] = packet.Translator.ReadBit();
            attackerGUID[1] = packet.Translator.ReadBit();
            victimGUID[4] = packet.Translator.ReadBit();
            attackerGUID[0] = packet.Translator.ReadBit();
            victimGUID[6] = packet.Translator.ReadBit();
            attackerGUID[5] = packet.Translator.ReadBit();
            victimGUID[2] = packet.Translator.ReadBit();
            attackerGUID[6] = packet.Translator.ReadBit();
            victimGUID[1] = packet.Translator.ReadBit();
            attackerGUID[2] = packet.Translator.ReadBit();
            victimGUID[0] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(attackerGUID, 5);
            packet.Translator.ReadXORByte(attackerGUID, 0);
            packet.Translator.ReadXORByte(victimGUID, 5);
            packet.Translator.ReadXORByte(attackerGUID, 4);
            packet.Translator.ReadXORByte(attackerGUID, 6);
            packet.Translator.ReadXORByte(victimGUID, 6);
            packet.Translator.ReadXORByte(victimGUID, 1);
            packet.Translator.ReadXORByte(victimGUID, 0);
            packet.Translator.ReadXORByte(attackerGUID, 7);
            packet.Translator.ReadXORByte(victimGUID, 4);
            packet.Translator.ReadXORByte(attackerGUID, 2);
            packet.Translator.ReadXORByte(victimGUID, 3);
            packet.Translator.ReadXORByte(victimGUID, 7);
            packet.Translator.ReadXORByte(victimGUID, 2);
            packet.Translator.ReadXORByte(attackerGUID, 3);
            packet.Translator.ReadXORByte(attackerGUID, 1);

            packet.Translator.WriteGuid("Attacker GUID", attackerGUID);
            packet.Translator.WriteGuid("Victim GUID", victimGUID);
        }

        [Parser(Opcode.SMSG_ATTACK_STOP)]
        public static void HandleAttackStartStop(Packet packet)
        {
            var victimGUID = new byte[8];
            var attackerGUID = new byte[8];

            victimGUID[5] = packet.Translator.ReadBit();
            victimGUID[6] = packet.Translator.ReadBit();
            attackerGUID[3] = packet.Translator.ReadBit();
            attackerGUID[6] = packet.Translator.ReadBit();
            attackerGUID[7] = packet.Translator.ReadBit();
            attackerGUID[2] = packet.Translator.ReadBit();
            attackerGUID[5] = packet.Translator.ReadBit();
            victimGUID[4] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Unk Bit");
            victimGUID[3] = packet.Translator.ReadBit();
            victimGUID[0] = packet.Translator.ReadBit();
            victimGUID[2] = packet.Translator.ReadBit();
            victimGUID[7] = packet.Translator.ReadBit();
            attackerGUID[4] = packet.Translator.ReadBit();
            attackerGUID[1] = packet.Translator.ReadBit();
            attackerGUID[0] = packet.Translator.ReadBit();
            victimGUID[1] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(victimGUID, 0);
            packet.Translator.ReadXORByte(victimGUID, 3);
            packet.Translator.ReadXORByte(victimGUID, 5);
            packet.Translator.ReadXORByte(victimGUID, 2);
            packet.Translator.ReadXORByte(attackerGUID, 0);
            packet.Translator.ReadXORByte(attackerGUID, 6);
            packet.Translator.ReadXORByte(attackerGUID, 3);
            packet.Translator.ReadXORByte(victimGUID, 4);
            packet.Translator.ReadXORByte(attackerGUID, 1);
            packet.Translator.ReadXORByte(attackerGUID, 4);
            packet.Translator.ReadXORByte(victimGUID, 6);
            packet.Translator.ReadXORByte(attackerGUID, 5);
            packet.Translator.ReadXORByte(attackerGUID, 7);
            packet.Translator.ReadXORByte(attackerGUID, 2);
            packet.Translator.ReadXORByte(victimGUID, 1);
            packet.Translator.ReadXORByte(victimGUID, 7);

            packet.Translator.WriteGuid("Attacker GUID", attackerGUID);
            packet.Translator.WriteGuid("Victim GUID", victimGUID);
        }

        [Parser(Opcode.CMSG_ATTACK_SWING)]
        public static void HandleAttackSwing(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 6, 5, 7, 0, 3, 1, 4, 2);
            packet.Translator.ParseBitStream(guid, 6, 7, 1, 3, 2, 0, 4, 5);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_ATTACKER_STATE_UPDATE)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            var bit2C = packet.Translator.ReadBit();
            if (bit2C)
            {
                var bits1C = (int)packet.Translator.ReadBits(21);
                packet.Translator.ReadInt32("Int18");
                for (var i = 0; i < bits1C; ++i)
                {
                    packet.Translator.ReadInt32("IntED", i);
                    packet.Translator.ReadInt32("IntED", i);
                }

                packet.Translator.ReadInt32("Int10");
                packet.Translator.ReadInt32("Int14");
            }

            packet.Translator.ReadInt32("Length");
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

            var state = packet.Translator.ReadByteE<VictimStates>("VictimState");
            packet.Translator.ReadInt32("Unk Attacker State 0");

            packet.Translator.ReadInt32<SpellId>("Melee Spell ID");

            var block = 0;
            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK))
                block = packet.Translator.ReadInt32("Block Amount");

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
        }

        [Parser(Opcode.SMSG_AI_REACTION)]
        public static void HandleAIReaction(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 5, 7, 0, 4, 6, 2, 3, 1);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadInt32E<AIReaction>("Reaction");
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 3);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_ENVIRONMENTAL_DAMAGE_LOG)]
        public static void HandleEnvirenmentalDamageLog(Packet packet)
        {
            var guid = new byte[8];

            guid[5] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            var bit30 = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            if (bit30)
            {
                var bits20 = packet.Translator.ReadBits(21);

                packet.Translator.ReadInt32("Int14");
                for (var i = 0; i < bits20; ++i)
                {
                    packet.Translator.ReadInt32("IntED", i);
                    packet.Translator.ReadInt32("IntED", i);
                }

                packet.Translator.ReadInt32("Int1C");
                packet.Translator.ReadInt32("Int18");
            }

            packet.Translator.ReadInt32("Int3C");
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadByteE<EnvironmentDamage>("Type");
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadInt32("Int38");
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadInt32("Damage");

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_CANCEL_AUTO_REPEAT)]
        public static void HandleCancelAutoRepeat(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 1, 3, 0, 4, 6, 7, 5, 2);
            packet.Translator.ParseBitStream(guid, 7, 6, 2, 5, 0, 4, 1, 3);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_DUEL_REQUESTED)]
        public static void HandleDuelRequested(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid1[5] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid2, 5);

            packet.Translator.WriteGuid("Flag GUID", guid1);
            packet.Translator.WriteGuid("Opponent GUID", guid2);
        }

        [Parser(Opcode.CMSG_DUEL_PROPOSED)]
        public static void HandleDuelProposed(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 1, 5, 4, 6, 3, 2, 7, 0);
            packet.Translator.ParseBitStream(guid, 4, 2, 5, 7, 1, 3, 6, 0);

            packet.Translator.WriteGuid("Opponent GUID", guid);
        }
    }
}

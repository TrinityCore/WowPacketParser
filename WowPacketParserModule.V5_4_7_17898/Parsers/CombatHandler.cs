using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.SMSG_ATTACK_START)]
        public static void HandleAttackStartStart(Packet packet)
        {
            var AttackerGUID = new byte[8];
            var VictimGUID = new byte[8];

            AttackerGUID[4] = packet.Translator.ReadBit();
            VictimGUID[4] = packet.Translator.ReadBit();
            AttackerGUID[6] = packet.Translator.ReadBit();
            AttackerGUID[3] = packet.Translator.ReadBit();
            AttackerGUID[5] = packet.Translator.ReadBit();
            VictimGUID[2] = packet.Translator.ReadBit();
            VictimGUID[6] = packet.Translator.ReadBit();
            AttackerGUID[0] = packet.Translator.ReadBit();
            VictimGUID[5] = packet.Translator.ReadBit();
            VictimGUID[0] = packet.Translator.ReadBit();
            VictimGUID[3] = packet.Translator.ReadBit();
            AttackerGUID[2] = packet.Translator.ReadBit();
            VictimGUID[1] = packet.Translator.ReadBit();
            VictimGUID[7] = packet.Translator.ReadBit();
            AttackerGUID[7] = packet.Translator.ReadBit();
            AttackerGUID[1] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(VictimGUID, 5);
            packet.Translator.ReadXORByte(VictimGUID, 7);
            packet.Translator.ReadXORByte(AttackerGUID, 4);
            packet.Translator.ReadXORByte(AttackerGUID, 5);
            packet.Translator.ReadXORByte(AttackerGUID, 3);
            packet.Translator.ReadXORByte(VictimGUID, 1);
            packet.Translator.ReadXORByte(AttackerGUID, 1);
            packet.Translator.ReadXORByte(VictimGUID, 3);
            packet.Translator.ReadXORByte(AttackerGUID, 7);
            packet.Translator.ReadXORByte(VictimGUID, 0);
            packet.Translator.ReadXORByte(VictimGUID, 2);
            packet.Translator.ReadXORByte(VictimGUID, 4);
            packet.Translator.ReadXORByte(AttackerGUID, 6);
            packet.Translator.ReadXORByte(AttackerGUID, 2);
            packet.Translator.ReadXORByte(VictimGUID, 6);
            packet.Translator.ReadXORByte(AttackerGUID, 0);

            packet.Translator.WriteGuid("Attacker GUID", AttackerGUID);
            packet.Translator.WriteGuid("Victim GUID", VictimGUID);
        }

        [Parser(Opcode.SMSG_ATTACK_STOP)]
        public static void HandleAttackStartStop(Packet packet)
        {
            var VictimGUID = new byte[8];
            var AttackerGUID = new byte[8];

            VictimGUID[5] = packet.Translator.ReadBit();
            AttackerGUID[1] = packet.Translator.ReadBit();
            AttackerGUID[2] = packet.Translator.ReadBit();
            VictimGUID[7] = packet.Translator.ReadBit();
            AttackerGUID[3] = packet.Translator.ReadBit();
            AttackerGUID[7] = packet.Translator.ReadBit();
            AttackerGUID[6] = packet.Translator.ReadBit();

            packet.Translator.ReadBit("Unk bit");

            VictimGUID[0] = packet.Translator.ReadBit();
            VictimGUID[2] = packet.Translator.ReadBit();
            VictimGUID[3] = packet.Translator.ReadBit();
            VictimGUID[6] = packet.Translator.ReadBit();
            VictimGUID[4] = packet.Translator.ReadBit();
            AttackerGUID[0] = packet.Translator.ReadBit();
            VictimGUID[1] = packet.Translator.ReadBit();
            AttackerGUID[4] = packet.Translator.ReadBit();
            AttackerGUID[5] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(AttackerGUID, 2);
            packet.Translator.ReadXORByte(VictimGUID, 3);
            packet.Translator.ReadXORByte(VictimGUID, 5);
            packet.Translator.ReadXORByte(VictimGUID, 2);
            packet.Translator.ReadXORByte(AttackerGUID, 4);
            packet.Translator.ReadXORByte(AttackerGUID, 1);
            packet.Translator.ReadXORByte(AttackerGUID, 0);
            packet.Translator.ReadXORByte(VictimGUID, 1);
            packet.Translator.ReadXORByte(AttackerGUID, 3);
            packet.Translator.ReadXORByte(AttackerGUID, 7);
            packet.Translator.ReadXORByte(AttackerGUID, 6);
            packet.Translator.ReadXORByte(VictimGUID, 4);
            packet.Translator.ReadXORByte(VictimGUID, 6);
            packet.Translator.ReadXORByte(AttackerGUID, 5);
            packet.Translator.ReadXORByte(VictimGUID, 7);
            packet.Translator.ReadXORByte(VictimGUID, 0);

            packet.Translator.WriteGuid("Attacker GUID", AttackerGUID);
            packet.Translator.WriteGuid("Victim GUID", VictimGUID);
        }

        [Parser(Opcode.CMSG_ATTACK_SWING)]
        public static void HandleAttackSwing(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 1, 5, 7, 0, 4, 6, 3, 2);
            packet.Translator.ParseBitStream(guid, 1, 2, 5, 7, 0, 3, 6, 4);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_ATTACKER_STATE_UPDATE)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
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
        }

        [Parser(Opcode.SMSG_CANCEL_AUTO_REPEAT)]
        public static void HandleCancelAutoRepeat(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 6, 3, 0, 1, 4, 2, 7, 5);
            packet.Translator.ParseBitStream(guid, 4, 6, 3, 1, 2, 0, 7, 5);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_PARTY_KILL_LOG)]
        public static void HandlePartyKillLog(Packet packet)
        {
            var playerGUID = new byte[8];
            var victimGUID = new byte[8];

            playerGUID[4] = packet.Translator.ReadBit();
            victimGUID[2] = packet.Translator.ReadBit();
            playerGUID[5] = packet.Translator.ReadBit();
            victimGUID[1] = packet.Translator.ReadBit();
            victimGUID[6] = packet.Translator.ReadBit();
            playerGUID[3] = packet.Translator.ReadBit();
            victimGUID[7] = packet.Translator.ReadBit();
            victimGUID[3] = packet.Translator.ReadBit();
            playerGUID[6] = packet.Translator.ReadBit();
            playerGUID[7] = packet.Translator.ReadBit();
            victimGUID[0] = packet.Translator.ReadBit();
            playerGUID[1] = packet.Translator.ReadBit();
            victimGUID[5] = packet.Translator.ReadBit();
            playerGUID[0] = packet.Translator.ReadBit();
            victimGUID[4] = packet.Translator.ReadBit();
            playerGUID[2] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(victimGUID, 4);
            packet.Translator.ReadXORByte(victimGUID, 3);
            packet.Translator.ReadXORByte(playerGUID, 4);
            packet.Translator.ReadXORByte(playerGUID, 3);
            packet.Translator.ReadXORByte(playerGUID, 6);
            packet.Translator.ReadXORByte(victimGUID, 0);
            packet.Translator.ReadXORByte(playerGUID, 0);
            packet.Translator.ReadXORByte(playerGUID, 5);
            packet.Translator.ReadXORByte(victimGUID, 2);
            packet.Translator.ReadXORByte(playerGUID, 1);
            packet.Translator.ReadXORByte(playerGUID, 7);
            packet.Translator.ReadXORByte(victimGUID, 5);
            packet.Translator.ReadXORByte(victimGUID, 6);
            packet.Translator.ReadXORByte(victimGUID, 7);
            packet.Translator.ReadXORByte(victimGUID, 1);
            packet.Translator.ReadXORByte(playerGUID, 2);

            packet.Translator.WriteGuid("Player GUID", playerGUID);
            packet.Translator.WriteGuid("Victim GUID", victimGUID);
        }

        [Parser(Opcode.SMSG_AI_REACTION)]
        public static void HandleAIReaction(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 2, 1, 4, 3, 6, 5, 7, 0);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadInt32E<AIReaction>("Reaction");
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 5);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_CANCEL_COMBAT)]
        public static void HandleCanelCombat(Packet packet)
        {
            packet.Translator.ReadBits("bits10", 2);
        }
    }
}

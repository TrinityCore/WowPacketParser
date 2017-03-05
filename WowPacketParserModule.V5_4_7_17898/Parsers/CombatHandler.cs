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

            AttackerGUID[4] = packet.ReadBit();
            VictimGUID[4] = packet.ReadBit();
            AttackerGUID[6] = packet.ReadBit();
            AttackerGUID[3] = packet.ReadBit();
            AttackerGUID[5] = packet.ReadBit();
            VictimGUID[2] = packet.ReadBit();
            VictimGUID[6] = packet.ReadBit();
            AttackerGUID[0] = packet.ReadBit();
            VictimGUID[5] = packet.ReadBit();
            VictimGUID[0] = packet.ReadBit();
            VictimGUID[3] = packet.ReadBit();
            AttackerGUID[2] = packet.ReadBit();
            VictimGUID[1] = packet.ReadBit();
            VictimGUID[7] = packet.ReadBit();
            AttackerGUID[7] = packet.ReadBit();
            AttackerGUID[1] = packet.ReadBit();

            packet.ReadXORByte(VictimGUID, 5);
            packet.ReadXORByte(VictimGUID, 7);
            packet.ReadXORByte(AttackerGUID, 4);
            packet.ReadXORByte(AttackerGUID, 5);
            packet.ReadXORByte(AttackerGUID, 3);
            packet.ReadXORByte(VictimGUID, 1);
            packet.ReadXORByte(AttackerGUID, 1);
            packet.ReadXORByte(VictimGUID, 3);
            packet.ReadXORByte(AttackerGUID, 7);
            packet.ReadXORByte(VictimGUID, 0);
            packet.ReadXORByte(VictimGUID, 2);
            packet.ReadXORByte(VictimGUID, 4);
            packet.ReadXORByte(AttackerGUID, 6);
            packet.ReadXORByte(AttackerGUID, 2);
            packet.ReadXORByte(VictimGUID, 6);
            packet.ReadXORByte(AttackerGUID, 0);

            packet.WriteGuid("Attacker GUID", AttackerGUID);
            packet.WriteGuid("Victim GUID", VictimGUID);
        }

        [Parser(Opcode.SMSG_ATTACK_STOP)]
        public static void HandleAttackStartStop(Packet packet)
        {
            var VictimGUID = new byte[8];
            var AttackerGUID = new byte[8];

            VictimGUID[5] = packet.ReadBit();
            AttackerGUID[1] = packet.ReadBit();
            AttackerGUID[2] = packet.ReadBit();
            VictimGUID[7] = packet.ReadBit();
            AttackerGUID[3] = packet.ReadBit();
            AttackerGUID[7] = packet.ReadBit();
            AttackerGUID[6] = packet.ReadBit();

            packet.ReadBit("Unk bit");

            VictimGUID[0] = packet.ReadBit();
            VictimGUID[2] = packet.ReadBit();
            VictimGUID[3] = packet.ReadBit();
            VictimGUID[6] = packet.ReadBit();
            VictimGUID[4] = packet.ReadBit();
            AttackerGUID[0] = packet.ReadBit();
            VictimGUID[1] = packet.ReadBit();
            AttackerGUID[4] = packet.ReadBit();
            AttackerGUID[5] = packet.ReadBit();

            packet.ReadXORByte(AttackerGUID, 2);
            packet.ReadXORByte(VictimGUID, 3);
            packet.ReadXORByte(VictimGUID, 5);
            packet.ReadXORByte(VictimGUID, 2);
            packet.ReadXORByte(AttackerGUID, 4);
            packet.ReadXORByte(AttackerGUID, 1);
            packet.ReadXORByte(AttackerGUID, 0);
            packet.ReadXORByte(VictimGUID, 1);
            packet.ReadXORByte(AttackerGUID, 3);
            packet.ReadXORByte(AttackerGUID, 7);
            packet.ReadXORByte(AttackerGUID, 6);
            packet.ReadXORByte(VictimGUID, 4);
            packet.ReadXORByte(VictimGUID, 6);
            packet.ReadXORByte(AttackerGUID, 5);
            packet.ReadXORByte(VictimGUID, 7);
            packet.ReadXORByte(VictimGUID, 0);

            packet.WriteGuid("Attacker GUID", AttackerGUID);
            packet.WriteGuid("Victim GUID", VictimGUID);
        }

        [Parser(Opcode.CMSG_ATTACK_SWING)]
        public static void HandleAttackSwing(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 1, 5, 7, 0, 4, 6, 3, 2);
            packet.ParseBitStream(guid, 1, 2, 5, 7, 0, 3, 6, 4);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_ATTACKER_STATE_UPDATE)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            packet.ReadInt32("Length");
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

            var state = packet.ReadByteE<VictimStates>("VictimState");
            packet.ReadInt32("Unk Attacker State 0");

            packet.ReadInt32<SpellId>("Melee Spell ID");

            var block = 0;
            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK))
                block = packet.ReadInt32("Block Amount");

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

            var bit2C = packet.ReadBit();

            if (bit2C)
            {
                var bits1C = (int)packet.ReadBits(21);
                packet.ReadInt32("Int18");
                for (var i = 0; i < bits1C; ++i)
                {
                    packet.ReadInt32("IntED", i);
                    packet.ReadInt32("IntED", i);
                }

                packet.ReadInt32("Int10");
                packet.ReadInt32("Int14");
            }
        }

        [Parser(Opcode.SMSG_CANCEL_AUTO_REPEAT)]
        public static void HandleCancelAutoRepeat(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 6, 3, 0, 1, 4, 2, 7, 5);
            packet.ParseBitStream(guid, 4, 6, 3, 1, 2, 0, 7, 5);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_PARTY_KILL_LOG)]
        public static void HandlePartyKillLog(Packet packet)
        {
            var playerGUID = new byte[8];
            var victimGUID = new byte[8];

            playerGUID[4] = packet.ReadBit();
            victimGUID[2] = packet.ReadBit();
            playerGUID[5] = packet.ReadBit();
            victimGUID[1] = packet.ReadBit();
            victimGUID[6] = packet.ReadBit();
            playerGUID[3] = packet.ReadBit();
            victimGUID[7] = packet.ReadBit();
            victimGUID[3] = packet.ReadBit();
            playerGUID[6] = packet.ReadBit();
            playerGUID[7] = packet.ReadBit();
            victimGUID[0] = packet.ReadBit();
            playerGUID[1] = packet.ReadBit();
            victimGUID[5] = packet.ReadBit();
            playerGUID[0] = packet.ReadBit();
            victimGUID[4] = packet.ReadBit();
            playerGUID[2] = packet.ReadBit();
            packet.ReadXORByte(victimGUID, 4);
            packet.ReadXORByte(victimGUID, 3);
            packet.ReadXORByte(playerGUID, 4);
            packet.ReadXORByte(playerGUID, 3);
            packet.ReadXORByte(playerGUID, 6);
            packet.ReadXORByte(victimGUID, 0);
            packet.ReadXORByte(playerGUID, 0);
            packet.ReadXORByte(playerGUID, 5);
            packet.ReadXORByte(victimGUID, 2);
            packet.ReadXORByte(playerGUID, 1);
            packet.ReadXORByte(playerGUID, 7);
            packet.ReadXORByte(victimGUID, 5);
            packet.ReadXORByte(victimGUID, 6);
            packet.ReadXORByte(victimGUID, 7);
            packet.ReadXORByte(victimGUID, 1);
            packet.ReadXORByte(playerGUID, 2);

            packet.WriteGuid("Player GUID", playerGUID);
            packet.WriteGuid("Victim GUID", victimGUID);
        }

        [Parser(Opcode.SMSG_AI_REACTION)]
        public static void HandleAIReaction(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 2, 1, 4, 3, 6, 5, 7, 0);
            packet.ReadXORByte(guid, 1);
            packet.ReadInt32E<AIReaction>("Reaction");
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_CANCEL_COMBAT)]
        public static void HandleCanelCombat(Packet packet)
        {
            packet.ReadBits("bits10", 2);
        }
    }
}

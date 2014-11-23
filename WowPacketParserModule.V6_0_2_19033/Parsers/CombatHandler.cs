using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using SpellParsers = WowPacketParserModule.V6_0_2_19033.Parsers.SpellHandler;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.SMSG_ATTACKERSTATEUPDATE)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            var bit52 = packet.ReadBit("HasLogData");

            if (bit52)
                SpellParsers.ReadSpellCastLogData(ref packet);

            packet.ReadInt32("Size");

            var hitInfo = packet.ReadEnum<SpellHitInfo>("HitInfo", TypeCode.Int32);

            packet.ReadPackedGuid128("Attacker Guid");
            packet.ReadPackedGuid128("Target Guid");

            packet.ReadInt32("Damage");
            packet.ReadInt32("OverDamage");

            var subDmgCount = packet.ReadBoolean("HasSubDmg");
            if (subDmgCount)
            {
                packet.ReadInt32("SchoolMask");
                packet.ReadSingle("Float Damage");
                packet.ReadInt32("Int Damage");

                if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_ABSORB | SpellHitInfo.HITINFO_FULL_ABSORB))
                    packet.ReadInt32("Damage Absorbed");

                if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_RESIST | SpellHitInfo.HITINFO_FULL_RESIST))
                    packet.ReadInt32("Damage Resisted");
            }

            packet.ReadEnum<VictimStates>("VictimState", TypeCode.Byte);
            packet.ReadInt32("Unk Attacker State 0");

            packet.ReadEntry<Int32>(StoreNameType.Spell, "Melee Spell ID");

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
        }

        [Parser(Opcode.SMSG_ATTACKSTART)]
        public static void HandleAttackStartStart(Packet packet)
        {
            packet.ReadPackedGuid128("Attacker Guid");
            packet.ReadPackedGuid128("Victim Guid");
        }

        [Parser(Opcode.SMSG_ATTACKSTOP)]
        public static void HandleAttackStartStop(Packet packet)
        {
            packet.ReadPackedGuid128("Attacker Guid");
            packet.ReadPackedGuid128("Victim Guid");

            packet.ReadBit("NowDead");
        }

        [Parser(Opcode.SMSG_HIGHEST_THREAT_UPDATE)]
        public static void HandleHighestThreatlistUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadPackedGuid128("HighestThreatGUID");

            var count = packet.ReadUInt32("ThreatListCount");

            // ThreatInfo
            for (var i = 0; i < count; i++)
            {
                packet.ReadPackedGuid128("UnitGUID", i);
                packet.ReadUInt32("Threat", i);
            }
        }

        [Parser(Opcode.SMSG_THREAT_UPDATE)]
        public static void HandleThreatlistUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            var int16 = packet.ReadInt32("Targets");

            for (int i = 0; i < int16; i++)
            {
                packet.ReadPackedGuid128("TargetGUID", i);
                packet.ReadInt32("Threat", i);
            }
        }

        [Parser(Opcode.SMSG_THREAT_REMOVE)]
        public static void HandleRemoveThreatlist(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadPackedGuid128("AboutGUID");
        }

        [Parser(Opcode.SMSG_AI_REACTION)]
        public static void HandleAIReaction(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadEnum<AIReaction>("Reaction", TypeCode.Int32);
        }

        [Parser(Opcode.CMSG_ATTACKSWING)]
        public static void HandleAttackSwing(Packet packet)
        {
            packet.ReadPackedGuid128("Victim");
        }

        [Parser(Opcode.SMSG_CANCEL_AUTO_REPEAT)]
        public static void HandleCancelAutoRepeat(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }
    }
}

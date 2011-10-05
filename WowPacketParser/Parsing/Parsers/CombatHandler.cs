using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.SMSG_PVP_CREDIT)]
        public static void HandlePvPCredit(Packet packet)
        {
            packet.ReadUInt32("Honor");
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Rank");
        }

        [Parser(Opcode.SMSG_AI_REACTION)]
        public static void HandleAIReaction(Packet packet)
        {
            packet.ReadGuid("GUID");

            packet.ReadEnum<AIReaction>("Reaction", TypeCode.Int32);
        }

        [Parser(Opcode.SMSG_UPDATE_COMBO_POINTS)]
        public static void HandleUpdateComboPoints(Packet packet)
        {
            packet.ReadPackedGuid("GUID");

            packet.ReadByte("Combo Points");
        }

        [Parser(Opcode.SMSG_ENVIRONMENTALDAMAGELOG)]
        public static void HandleEnvirenmentalDamageLog(Packet packet)
        {
            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);

            packet.ReadEnum<EnvironmentDamageFlags>("Type", TypeCode.Byte);

            packet.ReadInt32("Damage");

            packet.ReadInt32("Absorb");

            packet.ReadInt32("Resist");
        }

        [Parser(Opcode.SMSG_CANCEL_AUTO_REPEAT)]
        public static void HandleCancelAutoRepeat(Packet packet)
        {
            packet.ReadPackedGuid("Target GUID");
        }

        [Parser(Opcode.SMSG_ATTACKERSTATEUPDATE)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            var hitInfo = packet.ReadEnum<SpellHitInfo>("HitInfo", TypeCode.Int32);
            packet.ReadPackedGuid("AttackerGUID");
            packet.ReadPackedGuid("TargetGUID");
            packet.ReadInt32("Damage");
            packet.ReadInt32("OverDamage");
            var subDmgCount = packet.ReadByte();

            for (var i = 0; i < subDmgCount; ++i)
            {
                packet.ReadInt32("[" + i + "] SchoolMask");
                packet.ReadSingle("[" + i + "] Float Damage");
                packet.ReadInt32("[" + i + "] Int Damage");

                if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_ABSORB | SpellHitInfo.HITINFO_FULL_ABSORB))
                {
                    packet.ReadInt32("[" + i + "] Damage Absorbed");
                }

                if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_RESIST | SpellHitInfo.HITINFO_FULL_RESIST))
                {
                    packet.ReadInt32("[" + i + "] Damage Resisted");
                }
            }

            packet.ReadEnum<VictimStates>("VictimState", TypeCode.Byte);

            packet.ReadInt32("Unk Attacker State 0");

            var spellId = packet.ReadInt32();
            Console.WriteLine("Melee Spell ID : " + Extensions.SpellLine(spellId));

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK))
            {
                packet.ReadInt32("Block Amount");
            }

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_RAGE_GAIN))
            {
                packet.ReadInt32("Rage Gained");
            }

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_UNK0))
            {
                packet.ReadInt32("Unk Attacker State 3");
                packet.ReadSingle("Unk Attacker State 3");
                packet.ReadSingle("Unk Attacker State 3");
                packet.ReadSingle("Unk Attacker State 3");
                packet.ReadSingle("Unk Attacker State 3");
                packet.ReadSingle("Unk Attacker State 3");
                packet.ReadSingle("Unk Attacker State 3");
                packet.ReadSingle("Unk Attacker State 3");
                packet.ReadSingle("Unk Attacker State 3");
                packet.ReadSingle("Unk Attacker State 3");
                packet.ReadSingle("Unk Attacker State 3");
                packet.ReadInt32("Unk Attacker State 3");
                packet.ReadInt32("Unk Attacker State 3");
                packet.ReadInt32("Unk Attacker State 3");
            }
        }
    }
}

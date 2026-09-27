using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParserModule.V2_4_3_8606.Enums;

namespace WowPacketParserModule.V2_4_3_8606.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.SMSG_ATTACKER_STATE_UPDATE, ClientVersionBuild.V2_3_0_7561, ClientVersionBuild.V3_0_2_9056)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            var hitInfo = packet.ReadInt32E<SpellHitInfo243>("HitInfo");
            packet.ReadPackedGuid("AttackerGUID");
            packet.ReadPackedGuid("TargetGUID");
            packet.ReadInt32("Damage");

            var subDmgCount = packet.ReadByte();
            for (var i = 0; i < subDmgCount; ++i)
            {
                packet.ReadInt32("SchoolMask", i);
                packet.ReadSingle("Float Damage", i);
                packet.ReadInt32("Int Damage", i);
                packet.ReadInt32("Damage Absorbed", i);
                packet.ReadInt32("Damage Resisted", i);
            }

            packet.ReadInt32E<VictimStates>("VictimState");
            packet.ReadInt32("AttackerState");
            packet.ReadInt32<SpellId>("Melee Spell ID ");
            packet.ReadInt32("Block Amount");

            if (hitInfo.HasAnyFlag(SpellHitInfo243.HITINFO_DEBUG))
            {
                packet.ReadInt32("Armor");
                packet.ReadSingle("CritRollNeeded");
                packet.ReadSingle("CombatRoll");
                packet.ReadSingle("MissChance");
                packet.ReadSingle("DodgeChance");
                packet.ReadSingle("ParryChance");
                packet.ReadSingle("BlockChance");
                packet.ReadSingle("GlanceChance");
                packet.ReadSingle("CrushChance");
                for (var i = 0; i < 5; ++i)
                {
                    packet.ReadSingle("MinDamage", i);
                    packet.ReadSingle("MaxDamage", i);
                }
                packet.ReadInt32("SinceLastSwing");
            }
        }
    }
}

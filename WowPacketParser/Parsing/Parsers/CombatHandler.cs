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

        [Parser(Opcode.CMSG_SETSHEATHED)]
        public static void HandleSetSheathed(Packet packet)
        {
            packet.ReadEnum<SheathState>("Sheath", TypeCode.Int32);
        }

        [Parser(Opcode.SMSG_PARTYKILLLOG)]
        public static void HandlePartyKillLog(Packet packet)
        {
            packet.ReadGuid("Player GUID");
            packet.ReadGuid("Victim GUID");
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

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_3_9183))
                packet.ReadInt32("OverDamage");

            var subDmgCount = packet.ReadByte();
            for (var i = 0; i < subDmgCount; ++i)
            {
                packet.ReadInt32("SchoolMask", i);
                packet.ReadSingle("Float Damage", i);
                packet.ReadInt32("Int Damage", i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_3_9183) ||
                    hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_ABSORB | SpellHitInfo.HITINFO_FULL_ABSORB))
                    packet.ReadInt32("Damage Absorbed", i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_3_9183) ||
                    hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_RESIST | SpellHitInfo.HITINFO_FULL_RESIST))
                    packet.ReadInt32("Damage Resisted", i);
            }

            var victimStateTypeCode = ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_3_9183) ? TypeCode.Byte : TypeCode.Int32;
            packet.ReadEnum<VictimStates>("VictimState", victimStateTypeCode);
            packet.ReadInt32("Unk Attacker State 0");

            Console.WriteLine("Melee Spell ID : " + StoreGetters.GetName(StoreNameType.Spell, packet.ReadInt32()));

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_3_9183) ||
                hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK))
                packet.ReadInt32("Block Amount");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_RAGE_GAIN))
                packet.ReadInt32("Rage Gained");

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

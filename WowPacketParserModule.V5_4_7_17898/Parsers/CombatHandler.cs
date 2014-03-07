using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using SpellHitInfo = WowPacketParser.Enums.SpellHitInfo;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.SMSG_ATTACKSTART)]
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

        [Parser(Opcode.SMSG_ATTACKSTOP)]
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

        [Parser(Opcode.SMSG_ATTACKERSTATEUPDATE)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            packet.ReadInt32("Length");
            var hitInfo = packet.ReadEnum<SpellHitInfo>("HitInfo", TypeCode.Int32);

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

            var state = packet.ReadEnum<VictimStates>("VictimState", TypeCode.Byte);
            packet.ReadInt32("Unk Attacker State 0");

            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Melee Spell ID");

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
    }
}

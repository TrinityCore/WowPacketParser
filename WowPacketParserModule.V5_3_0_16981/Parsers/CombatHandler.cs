using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using SpellHitInfo = WowPacketParser.Enums.SpellHitInfo;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.SMSG_ATTACKSTART)]
        public static void HandleAttackStartStart(Packet packet)
        {
            var VictimGUID = new byte[8];
            var AttackerGUID = new byte[8];

            packet.StartBitStream(VictimGUID, 6, 2, 1);
            AttackerGUID[6] = packet.ReadBit();
            VictimGUID[5] = packet.ReadBit();
            packet.StartBitStream(AttackerGUID, 1, 3, 0);
            packet.StartBitStream(VictimGUID, 0, 7);
            packet.StartBitStream(AttackerGUID, 7, 5, 4);
            packet.StartBitStream(VictimGUID, 4, 3);
            AttackerGUID[2] = packet.ReadBit();

            packet.ReadXORByte(AttackerGUID, 4);
            packet.ReadXORByte(VictimGUID, 4);
            packet.ReadXORBytes(AttackerGUID, 2, 6);
            packet.ReadXORByte(VictimGUID, 7);
            packet.ReadXORByte(AttackerGUID, 0);
            packet.ReadXORByte(VictimGUID, 5);
            packet.ReadXORByte(AttackerGUID, 1);
            packet.ReadXORBytes(VictimGUID, 2, 6);
            packet.ReadXORByte(AttackerGUID, 7);
            packet.ReadXORByte(VictimGUID, 3);
            packet.ReadXORByte(AttackerGUID, 5);
            packet.ReadXORByte(VictimGUID, 0);
            packet.ReadXORByte(AttackerGUID, 3);
            packet.ReadXORByte(VictimGUID, 1);

            packet.WriteGuid("Attacker GUID", AttackerGUID);
            packet.WriteGuid("Victim GUID", VictimGUID);
        }

        [Parser(Opcode.SMSG_ATTACKSTOP)]
        public static void HandleAttackStartStop(Packet packet)
        {
            var VictimGUID = new byte[8];
            var AttackerGUID = new byte[8];

            VictimGUID[4] = packet.ReadBit();
            packet.StartBitStream(AttackerGUID, 1, 3, 0, 6, 5);
            VictimGUID[1] = packet.ReadBit();
            AttackerGUID[7] = packet.ReadBit();
            packet.StartBitStream(VictimGUID, 5, 6, 0);
            packet.StartBitStream(AttackerGUID, 2, 4);
            packet.StartBitStream(VictimGUID, 7, 2);
            packet.ReadBit("Unk bit");
            VictimGUID[3] = packet.ReadBit();

            packet.ReadXORBytes(VictimGUID, 2, 0);
            packet.ReadXORBytes(AttackerGUID, 5, 0, 4);
            packet.ReadXORBytes(VictimGUID, 4, 6, 7);
            packet.ReadXORBytes(AttackerGUID, 2, 3);
            packet.ReadXORBytes(VictimGUID, 5, 1);
            packet.ReadXORBytes(AttackerGUID, 1, 7);
            packet.ReadXORByte(VictimGUID, 3);
            packet.ReadXORByte(AttackerGUID, 6);

            packet.WriteGuid("Attacker GUID", AttackerGUID);
            packet.WriteGuid("Victim GUID", VictimGUID);
        }

        [Parser(Opcode.SMSG_ATTACKERSTATEUPDATE)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            var guid = new Byte[8];
            var counter = 0;
            var unk2 = 0;

            var hitInfo = packet.ReadEnum<SpellHitInfo>("HitInfo", TypeCode.Int32);
            packet.ReadPackedGuid("AttackerGUID");
            packet.ReadPackedGuid("TargetGUID");
            packet.ReadInt32("Damage");
            unk2 = packet.ReadInt32("OverDamage");

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

            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Melee Spell ID ");

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

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_UNK26))
            {
                packet.ReadInt32("Unk4");
                packet.ReadInt32("Player current HP");
                packet.ReadInt32("Unk3");

                guid[7] = packet.ReadBit();
                guid[6] = packet.ReadBit();
                packet.ReadBits(21);
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

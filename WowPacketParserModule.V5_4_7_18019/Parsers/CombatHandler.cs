using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using SpellHitInfo = WowPacketParser.Enums.SpellHitInfo;

namespace WowPacketParserModule.V5_4_7_18019.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.CMSG_ATTACKSWING)]
        public static void HandleAttackSwing(Packet packet)
        {
            var guid = packet.StartBitStream(1, 5, 7, 0, 4, 6, 3, 2);
            packet.ParseBitStream(guid, 1, 2, 5, 7, 0, 3, 6, 4);

            packet.WriteGuid("Target Guid", guid);
        }

        [Parser(Opcode.SMSG_ATTACKERSTATEUPDATE)]
        public static void HandleAttackerStateUpdate547(Packet packet)
        {
            packet.ReadUInt32("Unk");
            var hitInfo = packet.ReadUInt32("HitInfo");
            packet.ReadPackedGuid("AttackerGUID");
            packet.ReadPackedGuid("TargetGUID");
            packet.ReadInt32("Damage");
            packet.ReadInt32("OverDamage");

            var subDmgCount = packet.ReadByte("Count");
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

            packet.ReadByte("VictimState");
            packet.ReadInt32("Unk Attacker State 0");

            packet.ReadInt32("Melee Spell ID ");

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
                packet.ReadSingle("Unk Attacker State 3 12");
                packet.ReadSingle("Unk Attacker State 3 13");
                packet.ReadInt32("Unk Attacker State 3 14");
            }

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK | SpellHitInfo.HITINFO_UNK12))
                packet.ReadSingle("Unk Float");

            var hasUnkFlags = packet.ReadBit("hasUnkFlags");
            if (hasUnkFlags)
            {
                var unkCounter = packet.ReadBits("unkCounter", 21);
                packet.ReadUInt32("unk");

                for (UInt32 i = 0; i < unkCounter; ++i)
                {
                    packet.ReadUInt32("unk1");
                    packet.ReadUInt32("unk2");
                }

                packet.ReadUInt32("unk3");
                packet.ReadUInt32("unk4");
            }
        }

        [Parser(Opcode.SMSG_ATTACKSTART)]
        public static void HandleAttackStart(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[4] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[1] = packet.ReadBit();

            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 0);

            packet.WriteGuid("Attacker Guid", guid1);
            packet.WriteGuid("Victim Guid", guid2);
        }

        [Parser(Opcode.SMSG_ATTACKSTOP)]
        public static void HandleAttackStop(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[5] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid1[6] = packet.ReadBit();

            packet.ReadBit("Unk 1");

            guid2[0] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid1[5] = packet.ReadBit();

            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 0);

            packet.WriteGuid("Attacker Guid", guid1);
            packet.WriteGuid("Victim Guid", guid2);
        }

        [Parser(Opcode.SMSG_CANCEL_COMBAT)]
        public static void HandleCancelCombat(Packet packet)
        {
            var guid = packet.StartBitStream(3, 1, 0, 7, 6, 4, 2, 5);

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);

            packet.ReadByte("Unk 1");

            packet.ReadXORByte(guid, 7);

            packet.ReadByte("Unk 2");

            packet.ReadXORByte(guid, 1);

            packet.ReadByte("Unk 3");

            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Target Guid", guid);
        }
    }
}

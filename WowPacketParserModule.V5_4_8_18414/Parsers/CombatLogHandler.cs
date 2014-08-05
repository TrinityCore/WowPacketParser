using System;
using System.IO;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Parsing;
using SpellHitInfo = WowPacketParser.Enums.SpellHitInfo;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class CombatLogHandler
    {
        [Parser(Opcode.SMSG_COMBAT_LOG_MULTIPLE)]
        public static void HandleCombatLogMultiple(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_PERIODICAURALOG)]
        public static void HandlePeriodicAuraLog(Packet packet)
        {
            var casterGUID = new byte[8];
            var targetGUID = new byte[8];

            casterGUID[7] = packet.ReadBit(); // 23
            targetGUID[0] = packet.ReadBit(); // 80
            targetGUID[7] = packet.ReadBit(); // 87
            casterGUID[1] = packet.ReadBit(); // 17

            var count = packet.ReadBits("Count", 21); // 64
            // 12*4 = count == 0
            casterGUID[0] = packet.ReadBit(); // 16

            var hasOverDamage = new bool[count];
            var hasSpellProto = new bool[count];
            var hasUnk3 = new bool[count];
            var hasUnk4 = new bool[count];

            var cnt36 = 0u;

            for (var i = 0; i < count; i++)
            {
                hasOverDamage[i] = !packet.ReadBit("!hasOverDamage", i); // ! 64*4+8
                hasUnk3[i] = !packet.ReadBit("not unk3", i); // ! 64*4+16
                packet.ReadBit("unk24", i); // 64+24
                hasUnk4[i] = !packet.ReadBit("not unk4", i); // ! 64*4+20
                hasSpellProto[i] = !packet.ReadBit("!hasSpellProto", i); // ! 64*4+12
            }
            casterGUID[5] = packet.ReadBit(); // 21
            casterGUID[3] = packet.ReadBit(); // 19
            targetGUID[1] = packet.ReadBit(); // 81
            casterGUID[2] = packet.ReadBit(); // 18
            targetGUID[6] = packet.ReadBit(); // 86
            targetGUID[3] = packet.ReadBit(); // 83
            targetGUID[4] = packet.ReadBit(); // 84
            var unk52 = packet.ReadBit("unk52"); // 52
            targetGUID[2] = packet.ReadBit(); // 82
            casterGUID[6] = packet.ReadBit(); // 22
            targetGUID[5] = packet.ReadBit(); // 85

            if (unk52)
            {
                cnt36 = packet.ReadBits("cnt36", 21);
            }
            casterGUID[4] = packet.ReadBit(); // 20

            for (var i = 0; i < count; i++)
            {
                if (hasOverDamage[i])
                    packet.ReadInt32("Over Damage", i);
                packet.ReadInt32("Damage", i); // 4
                packet.ReadEnum<AuraType>("Aura Type", TypeCode.Int32, i); // 64*4
                if (hasUnk4[i])
                    packet.ReadInt32("unk4", i);
                if (hasUnk3[i])
                    packet.ReadInt32("unk3", i);
                if (hasSpellProto[i])
                    packet.ReadInt32("Spell Proto", i);
            }
            packet.ReadXORByte(targetGUID, 5); // 85
            packet.ReadXORByte(targetGUID, 3); // 83
            packet.ReadXORByte(casterGUID, 4); // 20
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID"); // 56
            packet.ReadXORByte(casterGUID, 6); // 22
            packet.ReadXORByte(targetGUID, 7); // 87
            packet.ReadXORByte(targetGUID, 1); // 81
            if (unk52)
            {
                for (var i = 0; i < cnt36; i++)
                {
                    packet.ReadInt32("unk44", i);
                    packet.ReadInt32("unk48", i);
                }
                packet.ReadInt32("unk28");
                packet.ReadInt32("unk24");
                packet.ReadInt32("unk32");
            }
            packet.ReadXORByte(casterGUID, 5); // 21
            packet.ReadXORByte(targetGUID, 0); // 80
            packet.ReadXORByte(casterGUID, 1); // 17
            packet.ReadXORByte(casterGUID, 7); // 23
            packet.ReadXORByte(targetGUID, 4); // 84
            packet.ReadXORByte(casterGUID, 3); // 19
            packet.ReadXORByte(targetGUID, 2); // 82
            packet.ReadXORByte(casterGUID, 0); // 16
            packet.ReadXORByte(casterGUID, 2); // 18
            packet.ReadXORByte(targetGUID, 6); // 86

            packet.WriteGuid("Caster Guid", casterGUID);
            packet.WriteGuid("Target Guid", targetGUID);
        }

        [Parser(Opcode.SMSG_SPELLBREAKLOG)]
        public static void HandleSpellBreakLog(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SPELLDAMAGESHIELD)]
        public static void HandleSpellDamageShield(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SPELLDISPELLOG)]
        public static void HandleSpellDispellLog(Packet packet)
        {
            var casterGUID = new byte[8];
            var targetGUID = new byte[8];

            targetGUID[2] = packet.ReadBit();
            casterGUID[4] = packet.ReadBit();
            targetGUID[6] = packet.ReadBit();
            casterGUID[5] = packet.ReadBit();
            var bit38 = packet.ReadBit();
            var bit2C = packet.ReadBit();
            targetGUID[5] = packet.ReadBit();
            targetGUID[7] = packet.ReadBit();
            targetGUID[4] = packet.ReadBit();
            targetGUID[0] = packet.ReadBit();
            targetGUID[1] = packet.ReadBit();
            var bits1C = (int)packet.ReadBits(22);
            casterGUID[0] = packet.ReadBit();

            var bit4 = new int[bits1C];
            var bit14 = new bool[bits1C];
            var bitC = new bool[bits1C];

            for (var i = 0; i < bits1C; ++i)
            {
                bit14[i] = packet.ReadBit();
                bitC[i] = packet.ReadBit();
                bit4[i] = packet.ReadBit();
            }

            casterGUID[3] = packet.ReadBit();
            casterGUID[2] = packet.ReadBit();
            targetGUID[3] = packet.ReadBit();
            casterGUID[1] = packet.ReadBit();
            casterGUID[7] = packet.ReadBit();
            casterGUID[6] = packet.ReadBit();

            for (var i = 0; i < bits1C; ++i)
            {
                packet.WriteLine("[{0}] bit4: {1}", i, bit4[i]);

                if (bit14[i])
                    packet.ReadInt32("Int20", i);

                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell", i);

                if (bitC[i])
                    packet.ReadInt32("Int20", i);
            }

            packet.ReadXORByte(casterGUID, 4);
            packet.ReadXORByte(targetGUID, 3);
            packet.ReadXORByte(casterGUID, 6);
            packet.ReadXORByte(casterGUID, 0);
            packet.ReadXORByte(targetGUID, 5);
            packet.ReadXORByte(targetGUID, 1);
            packet.ReadXORByte(casterGUID, 3);
            packet.ReadXORByte(casterGUID, 2);
            packet.ReadXORByte(casterGUID, 1);
            packet.ReadXORByte(casterGUID, 5);
            packet.ReadXORByte(targetGUID, 0);
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell");
            packet.ReadXORByte(targetGUID, 7);
            packet.ReadXORByte(targetGUID, 6);
            packet.ReadXORByte(targetGUID, 2);
            packet.ReadXORByte(casterGUID, 7);
            packet.ReadXORByte(targetGUID, 4);

            packet.WriteGuid("Caster GUID", casterGUID);
            packet.WriteGuid("Target GUID", targetGUID);
        }

        [Parser(Opcode.SMSG_SPELLENERGIZELOG)]
        public static void HandleSpellEnergizeLog(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];

            guid[7] = packet.ReadBit(); // 63
            guid[3] = packet.ReadBit(); // 59
            guid2[1] = packet.ReadBit(); // 65
            guid[4] = packet.ReadBit(); // 60
            guid[2] = packet.ReadBit(); // 58
            guid2[3] = packet.ReadBit(); // 67
            guid[5] = packet.ReadBit(); // 61
            var unk44 = packet.ReadBit(); // 44
            guid2[7] = packet.ReadBit(); // 71
            guid2[0] = packet.ReadBit(); // 64
            guid2[2] = packet.ReadBit(); // 66

            var count28 = 0u;
            if (unk44)
                count28 = packet.ReadBits("count28", 21);
            guid2[4] = packet.ReadBit(); // 68
            guid2[6] = packet.ReadBit(); // 70
            guid[6] = packet.ReadBit(); // 62
            guid[1] = packet.ReadBit(); // 57
            guid[0] = packet.ReadBit(); // 56
            guid2[5] = packet.ReadBit(); // 69

            packet.ReadXORByte(guid, 0); // 56
            packet.ReadXORByte(guid2, 5); // 69
            packet.ReadXORByte(guid, 6); // 62

            if (unk44)
            {
                packet.ReadInt32("unk20");
                for (var i = 0; i < count28; i++)
                {
                    packet.ReadInt32("unk32", i);
                    packet.ReadInt32("unk36", i);
                }
                packet.ReadInt32("unk24");
                packet.ReadInt32("unk16");
            }
            packet.ReadXORByte(guid2, 6); // 70
            packet.ReadXORByte(guid, 2); // 58
            packet.ReadXORByte(guid2, 0); // 64
            packet.ReadXORByte(guid, 1); // 57
            packet.ReadInt32("unk48");
            packet.ReadXORByte(guid, 4); // 60
            packet.ReadXORByte(guid2, 1); // 65
            packet.ReadXORByte(guid2, 7); // 71
            packet.ReadXORByte(guid, 5); // 61
            packet.ReadXORByte(guid2, 2); // 66
            packet.ReadXORByte(guid2, 3); // 67
            packet.ReadXORByte(guid, 7); // 63
            packet.ReadXORByte(guid2, 4); // 68
            packet.ReadXORByte(guid, 3); // 59

            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID"); // 72
            packet.ReadEnum<PowerType>("Power Type", TypeCode.UInt32); // 52

            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_SPELLHEALLOG)]
        public static void HandleSpellHealLog(Packet packet)
        {
            var casterGUID = new byte[8];
            var targetGUID = new byte[8];

            var bits24 = 0;

            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
            packet.ReadInt32("Absorb");
            packet.ReadInt32("Damage");
            packet.ReadInt32("Overheal");
            targetGUID[0] = packet.ReadBit();
            casterGUID[2] = packet.ReadBit();
            casterGUID[6] = packet.ReadBit();
            targetGUID[2] = packet.ReadBit();
            var bit18 = packet.ReadBit();
            casterGUID[3] = packet.ReadBit();
            casterGUID[0] = packet.ReadBit();
            casterGUID[5] = packet.ReadBit();
            targetGUID[3] = packet.ReadBit();
            var hasPowerData = packet.ReadBit();
            targetGUID[7] = packet.ReadBit();
            targetGUID[5] = packet.ReadBit();
            casterGUID[7] = packet.ReadBit();
            targetGUID[4] = packet.ReadBit();
            if (hasPowerData)
                bits24 = (int)packet.ReadBits(21);
            var bit48 = packet.ReadBit();
            var bit58 = packet.ReadBit();
            casterGUID[4] = packet.ReadBit();
            casterGUID[1] = packet.ReadBit();
            targetGUID[1] = packet.ReadBit();
            targetGUID[6] = packet.ReadBit();
            if (hasPowerData)
            {
                for (var i = 0; i < bits24; ++i)
                {
                    packet.ReadInt32("IntED", i);
                    packet.ReadInt32("IntED", i);
                }

                packet.ReadInt32("Int34");
                packet.ReadInt32("Int3C");
                packet.ReadInt32("Int38");
            }

            packet.ReadXORByte(casterGUID, 2);
            packet.ReadXORByte(targetGUID, 6);
            packet.ReadXORByte(casterGUID, 5);
            packet.ReadXORByte(casterGUID, 3);
            packet.ReadXORByte(targetGUID, 7);
            if (bit58)
                packet.ReadSingle("Float54");
            packet.ReadXORByte(casterGUID, 7);
            packet.ReadXORByte(casterGUID, 6);
            packet.ReadXORByte(casterGUID, 1);
            packet.ReadXORByte(targetGUID, 2);
            packet.ReadXORByte(targetGUID, 4);
            packet.ReadXORByte(targetGUID, 3);
            packet.ReadXORByte(targetGUID, 0);
            packet.ReadXORByte(targetGUID, 5);
            packet.ReadXORByte(casterGUID, 0);
            if (bit48)
                packet.ReadSingle("Float44");
            packet.ReadXORByte(targetGUID, 1);
            packet.ReadXORByte(casterGUID, 4);

            packet.WriteGuid("Caster GUID", casterGUID);
            packet.WriteGuid("Target GUID", targetGUID);
        }

        [Parser(Opcode.SMSG_SPELLLOGEXECUTE)]
        public static void HandleSpellLogExecute(Packet packet)
        {
            var guid = new byte[8];
            byte[][] guid2;
            byte[][][] guid3 = null;
            byte[][][] guid4 = null;
            byte[][][] guid5 = null;
            byte[][][] guid6 = null;

            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var bits10 = packet.ReadBits(19);
            guid[4] = packet.ReadBit();

            var bits44 = new uint[bits10];
            var bits14 = new uint[bits10];
            var bits24 = new uint[bits10];
            var bits7C = new uint[bits10];
            var bits34 = new uint[bits10];
            var bits54 = new uint[bits10];
            var bits4 = new uint[bits10];

            guid2 = new byte[bits10][];
            guid3 = new byte[bits10][][];
            guid4 = new byte[bits10][][];
            guid5 = new byte[bits10][][];
            guid6 = new byte[bits10][][];

            for (var i = 0; i < bits10; ++i)
            {
                bits14[i] = packet.ReadBits(21);
                guid3[i] = new byte[bits14[i]][];
                for (var j = 0; j < bits14[i]; ++j)
                {
                    guid3[i][j] = new byte[8];
                    packet.StartBitStream(guid3[i][j], 5, 4, 2, 3, 1, 0, 6, 7);
                }

                bits4[i] = packet.ReadBits(20);
                guid6[i] = new byte[bits4[i]][];
                for (var j = 0; j < bits4[i]; ++j)
                {
                    guid6[i][j] = new byte[8];
                    packet.StartBitStream(guid6[i][j], 0, 3, 1, 5, 6, 4, 7, 2);
                }

                bits24[i] = packet.ReadBits(21);
                bits54[i] = packet.ReadBits(22);
                bits44[i] = packet.ReadBits(22);

                guid4[i] = new byte[bits24[i]][];
                for (var j = 0; j < bits24[i]; ++j)
                {
                    guid4[i][j] = new byte[8];
                    packet.StartBitStream(guid4[i][j], 7, 2, 0, 5, 6, 3, 4, 1);
                }

                bits34[i] = packet.ReadBits(24);
                guid5[i] = new byte[bits34[i]][];
                for (var j = 0; j < bits34[i]; ++j)
                {
                    guid5[i][j] = new byte[8];
                    packet.StartBitStream(guid5[i][j], 6, 5, 1, 0, 3, 4, 7, 2);
                }
            }

            guid[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();

            var bits38 = 0u;
            var bit48 = packet.ReadBit();
            if (bit48)
                bits38 = packet.ReadBits(21);

            if (bit48)
            {
                for (var i = 0; i < bits38; ++i)
                {
                    packet.ReadInt32("Int38+4", i);
                    packet.ReadInt32("Int38+8", i);
                }

                packet.ReadInt32("Int30");
                packet.ReadInt32("Int2C");
                packet.ReadInt32("Int34");
            }

            for (var i = 0; i < bits10; ++i)
            {
                for (var j = 0; j < bits34[i]; ++j)
                {
                    packet.ParseBitStream(guid5[i][j], 7, 5, 1, 2, 6, 4, 0, 3);
                    packet.WriteGuid("Summoned GUID", guid5[i][j], i, j);
                }

                for (var j = 0; j < bits24[i]; ++j)
                {
                    packet.ReadXORByte(guid4[i][j], 4);
                    packet.ReadXORByte(guid4[i][j], 3);
                    packet.ReadInt32("Int24+0", i, j);
                    packet.ReadXORByte(guid4[i][j], 6);
                    packet.ReadXORByte(guid4[i][j], 5);
                    packet.ReadXORByte(guid4[i][j], 0);
                    packet.ReadXORByte(guid4[i][j], 7);
                    packet.ReadInt32("Int24+4", i, j);
                    packet.ReadXORByte(guid4[i][j], 2);
                    packet.ReadXORByte(guid4[i][j], 1);

                    packet.WriteGuid("Guid4", guid4[i][j], i, j);
                }

                for (var j = 0; j < bits4[i]; ++j)
                {
                    packet.ReadXORByte(guid6[i][j], 3);
                    packet.ReadXORByte(guid6[i][j], 7);
                    packet.ReadXORByte(guid6[i][j], 5);
                    packet.ReadXORByte(guid6[i][j], 2);
                    packet.ReadXORByte(guid6[i][j], 0);
                    packet.ReadInt32("Int4+0", i, j);
                    packet.ReadInt32("Int4+8", i, j);
                    packet.ReadXORByte(guid6[i][j], 4);
                    packet.ReadXORByte(guid6[i][j], 1);
                    packet.ReadSingle("FloatEB", i, j);
                    packet.ReadXORByte(guid6[i][j], 6);

                    packet.WriteGuid("Guid6", guid6[i][j], i, j);
                }

                for (var j = 0; j < bits14[i]; ++j)
                {
                    packet.ReadXORByte(guid3[i][j], 0);
                    packet.ReadXORByte(guid3[i][j], 6);
                    packet.ReadXORByte(guid3[i][j], 4);
                    packet.ReadXORByte(guid3[i][j], 7);
                    packet.ReadXORByte(guid3[i][j], 2);
                    packet.ReadXORByte(guid3[i][j], 5);
                    packet.ReadXORByte(guid3[i][j], 3);
                    packet.ReadInt32("Int14");
                    packet.ReadXORByte(guid3[i][j], 1);

                    packet.WriteGuid("Guid3", guid3[i][j], i, j);
                }

                for (var j = 0; j < bits54[i]; ++j)
                    packet.ReadInt32("Int54", i, j);

                var type = packet.ReadEnum<SpellEffect>("Spell Effect", TypeCode.Int32, i);

                for (var j = 0; j < bits44[i]; ++j)
                    packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Created Item", i, j);
            }

            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
            packet.ParseBitStream(guid, 5, 7, 1, 6, 2, 0, 4, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPELLLOGMISS)]
        public static void HandleSpellLogMiss(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SPELLNONMELEEDAMAGELOG)]
        public static void HandleSpellNonMeleeDmgLog(Packet packet)
        {
            ReadSpellNonMeleeDamageLog(ref packet);
        }

        [Parser(Opcode.SMSG_SPELLSTEALLOG)]
        public static void HandleSpellStealLog(Packet packet)
        {
            packet.ReadToEnd();
        }

        private static void ReadSpellNonMeleeDamageLog(ref Packet packet, int index = -1)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];
            guid2[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var unk92 = packet.ReadBit("unk92", index);
            guid[0] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            var unk20 = packet.ReadBit("unk20", index);
            var unk128 = packet.ReadBit("unk128", index);
            guid2[1] = packet.ReadBit();
            var unk68 = packet.ReadBit("unk68", index);
            var unk3ch = false;
            var unk28h = false;
            var unk24h = false;
            var unk30h = false;
            var unk2ch = false;
            var unk20h = false;
            var unk1ch = false;
            var unk34h = false;
            var unk38h = false;
            var unk40h = false;
            if (unk68)
            {
                unk3ch = !packet.ReadBit("!unk3ch", index);
                unk28h = !packet.ReadBit("!unk28h", index);
                unk24h = !packet.ReadBit("!unk24h", index);
                unk30h = !packet.ReadBit("!unk30h", index);
                unk2ch = !packet.ReadBit("!unk2ch", index);
                unk20h = !packet.ReadBit("!unk20h", index);
                unk1ch = !packet.ReadBit("!unk1ch", index);
                unk34h = !packet.ReadBit("!unk34h", index);
                unk38h = !packet.ReadBit("!unk38h", index);
                unk40h = !packet.ReadBit("!unk40h", index);
            }
            guid2[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            var unk112 = 0u;
            if (unk128)
                unk112 = packet.ReadBits("unk112", 21, index);

            guid2[4] = packet.ReadBit();

            packet.ReadInt32("unk140", index);

            if (unk68)
            {
                if (unk30h)
                    packet.ReadSingle("unk30h", index);
                if (unk20h)
                    packet.ReadSingle("unk20h", index);
                if (unk3ch)
                    packet.ReadSingle("unk3ch", index);
                if (unk2ch)
                    packet.ReadSingle("unk2ch", index);
                if (unk34h)
                    packet.ReadSingle("unk34h", index);
                if (unk24h)
                    packet.ReadSingle("unk24h", index);
                if (unk1ch)
                    packet.ReadSingle("unk1ch", index);
                if (unk40h)
                    packet.ReadSingle("unk40h", index);
                if (unk28h)
                    packet.ReadSingle("unk28h", index);
                if (unk38h)
                    packet.ReadSingle("unk38h", index);
            }

            packet.ParseBitStream(guid, 1);
            packet.ReadInt32("unk16", index);
            packet.ParseBitStream(guid2, 3);
            packet.ParseBitStream(guid, 0);
            packet.ParseBitStream(guid2, 6);
            packet.ParseBitStream(guid2, 4);
            packet.ParseBitStream(guid, 7);
            packet.ReadInt32("unk136", index);
            packet.ReadInt32("unk96", index);

            if (unk128)
            {
                packet.ReadInt32("unk100", index);
                for (var i = 0; i < unk112; i++)
                {
                    packet.ReadInt32("unk116", index, i);
                    packet.ReadInt32("unk120", index, i);
                }
                packet.ReadInt32("unk108", index);
                packet.ReadInt32("unk104", index);
            }
            packet.ParseBitStream(guid, 5);
            packet.ParseBitStream(guid2, 5);
            packet.ParseBitStream(guid, 3);
            packet.ParseBitStream(guid, 2);
            packet.ParseBitStream(guid2, 2);
            packet.ParseBitStream(guid, 6);
            packet.ParseBitStream(guid2, 0);
            packet.ParseBitStream(guid, 4);
            packet.ReadInt32("unk24", index);
            packet.ReadByte("unk132", index);
            packet.ParseBitStream(guid2, 7);
            packet.ReadInt32("unk72", index);
            packet.ParseBitStream(guid2, 1);
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID", index);

            packet.WriteGuid("Caster", guid, index);
            packet.WriteGuid("Target", guid2, index);
        }
    }
}

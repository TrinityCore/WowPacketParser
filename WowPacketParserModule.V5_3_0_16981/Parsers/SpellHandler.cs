using System;
using System.Text;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class SpellHandler
    {
        [Parser(Opcode.CMSG_CAST_SPELL)]
        public static void HandleCastSpell(Packet packet)
        {
            var counter = packet.ReadBits(2);
            var unk_bit = !packet.ReadBit();
            for (var i = 0; i < counter; ++i)
                packet.ReadBits("unk value0", 2, i);

            var HasCastCount = !packet.ReadBit();
            packet.ReadBit("Fake bit? Has TargetGUID"); // TargetGUID
            var hasbit1C = !packet.ReadBit();
            var hasMovment = packet.ReadBit();
            var hasbit78 = !packet.ReadBit();
            var hasbitF8 = !packet.ReadBit();
            var hasGUID2 = packet.ReadBit();
            var hasbitFC = !packet.ReadBit();
            var hasbit18 = !packet.ReadBit();
            var hasGUID3 = packet.ReadBit();
            packet.ReadBit("Fake bit? Has GUID0"); // GUID0
            var hasSpellId = !packet.ReadBit();

            var GUID0 = new byte[8];
            var TargetGUID = new byte[8];
            var GUID2 = new byte[8];
            var GUID3 = new byte[8];

            GUID0 = packet.StartBitStream(0, 5, 1, 7, 4, 3, 6, 2);
            if (hasGUID3)
                GUID3 = packet.StartBitStream(2, 5, 3, 7, 4, 1, 0, 6);

            if (hasGUID2)
                GUID2 = packet.StartBitStream(6, 2, 4, 7, 3, 5, 0, 1);

            TargetGUID = packet.StartBitStream(3, 0, 2, 7, 6, 4, 1, 5);

            if (unk_bit)
                packet.ReadEnum<CastFlag>("Cast Flags", 20);

            if (hasbit1C)
                packet.ReadBits("hasbit1C", 5);

            uint len78 = 0;
            if (hasbit78)
                len78 = packet.ReadBits("hasbit78", 7);
            packet.ResetBitReader();

            for (var i = 0; i < counter; ++i)
            {
                packet.ReadInt32("unk value1", i);
                packet.ReadInt32("unk value2", i);
            }

            if (hasGUID3)
            {
                var pos = new Vector3();
                packet.ReadXORBytes(GUID3, 7, 5, 3);
                pos.X = packet.ReadSingle();
                packet.ReadXORBytes(GUID3, 0, 2, 1, 4, 6);
                pos.Z = packet.ReadSingle();
                pos.Y = packet.ReadSingle();
                packet.WriteGuid("GUID3", GUID3);
                packet.WriteLine("Position: {0}", pos);
            }

            packet.ParseBitStream(TargetGUID, 2, 0, 5, 6, 7, 3, 4, 1);
            packet.WriteGuid("Target GUID", TargetGUID);

            if (hasGUID2)
            {
                var pos = new Vector3();
                packet.ReadXORBytes(GUID2, 5, 7);
                pos.Y = packet.ReadSingle();
                pos.X = packet.ReadSingle();
                packet.ReadXORBytes(GUID2, 3, 1);
                pos.Z = packet.ReadSingle();
                packet.ReadXORBytes(GUID2, 2, 6, 4, 0);
                packet.WriteGuid("GUID2", GUID2);
                packet.WriteLine("Position: {0}", pos);
            }

            packet.ParseBitStream(GUID0, 7, 2, 6, 4, 1, 0, 3, 5);
            packet.WriteGuid("GUID0", GUID0);

            if (hasbit78)
                packet.ReadWoWString("String", (int)len78);

            if (HasCastCount)
                packet.ReadByte("Cast Count");

            if (hasbit18)
                packet.ReadInt32("Int18");

            if (hasMovment)
                MovementHandler.ReadClientMovementBlock(ref packet);

            if (hasSpellId)
                packet.ReadInt32("SpellId");

            if (hasbitF8)
                packet.ReadSingle("FloatF8");

            if (hasbitFC)
                packet.ReadSingle("FloatFC");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellGo(Packet packet)
        {
            //var CasterGUID1 = new byte[8]; // 14
            var CasterGUID2 = new byte[8]; // 112-119
            var guid3 = new byte[8]; // 24-31
            var guid4 = new byte[8]; // 16-23
            //var guid5 = new byte[8]; // 98
            var guid6 = new byte[8]; // 416-423
            var guid7 = new byte[8]; // 168-175
            var guid8 = new byte[8]; // 136-143
            //var guid9 = new byte[8]; // 18
            var TargetGUID = new byte[8];
            var PowerUnitGUID = new byte[8];

            var bits52 = packet.ReadBits("Bits52", 24);
            var CasterGUID1 = new byte[bits52][];
            for (var i = 0; i < bits52; ++i)
            {
                CasterGUID1[i] = new byte[8];
                packet.StartBitStream(CasterGUID1[i], 2, 5, 4, 7, 6, 0, 3, 1);
            }

            packet.ReadBit("bit28");
            var bit106 = !packet.ReadBit("bit106");
            packet.ReadBit("bit30");

            packet.StartBitStream(CasterGUID2, 5, 4, 7, 1, 0, 6, 3, 2);
            packet.StartBitStream(guid4, 5, 6);
            guid3[2] = packet.ReadBit();
            var bit372 = packet.ReadBit("bit372");
            packet.StartBitStream(TargetGUID, 0, 3, 1, 5, 6, 2, 7, 4);

            var HasPowerData = packet.ReadBit("HasPowerData"); // bit432
            uint PowerTypeCount = 0;
            if (HasPowerData)
            {
                packet.StartBitStream(PowerUnitGUID, 6, 7, 3, 5, 0, 4, 2, 1);
                PowerTypeCount = packet.ReadBits("bits460", 21);
            }
            packet.StartBitStream(guid3, 6, 0);
            var bit102 = !packet.ReadBit("bit102");
            var bit101 = !packet.ReadBit("bit101");
            guid4[0] = packet.ReadBit();
            var bits84 = packet.ReadBits("bits84", 25);
            guid3[7] = packet.ReadBit();
            var bit26 = !packet.ReadBit("bit26");
            var bit368 = !packet.ReadBit("bit368");
            var bit336 = !packet.ReadBit("bit336");
            guid4[4] = packet.ReadBit();
            guid3[4] = packet.ReadBit();
            var bit91 = !packet.ReadBit("bit91");
            var bits388 = packet.ReadBits("bits388", 20);

            var guid5 = new byte[bits388][];
            for (var i = 0; i < bits388; i++)
            {
                guid5[i] = new byte[8];
                packet.StartBitStream(guid5[i], 0, 5, 2, 7, 6, 4, 3, 1);

            }
            packet.ReadBit("bit104");
            var bit90 = !packet.ReadBit("bit90");

            for (var i = 0; i < bits84; ++i)
            {
                if (packet.ReadBits("bits22[0]", 4) == 11)
                    packet.ReadBits("bits22[1]", 4);
            }

            packet.StartBitStream(guid6, 2, 1, 7, 0, 6, 3, 5, 4);
            guid4[7] = packet.ReadBit();
            var bit160 = packet.ReadBit("bit160");
            var bit128 = packet.ReadBit("bit128");
            guid4[2] = packet.ReadBit();

            if (bit160)
                packet.StartBitStream(guid7, 3, 7, 1, 0, 5, 6, 4, 2);

            guid4[3] = packet.ReadBit();
            var bit89 = !packet.ReadBit("bit89");

            if (bit128)
                packet.StartBitStream(guid8, 5, 4, 3, 2, 0, 6, 7, 1);

            var bit48 = packet.ReadBit("bit48");

            if (bit26)
                packet.ReadBits("bits26", 20);

            var bit337 = !packet.ReadBit("bit337");

            int bits48 = 0;
            if (!bit48)
                bits48 = (int)packet.ReadBits("bits48", 7);

            var bit428 = !packet.ReadBit("bit428");
            guid3[3] = packet.ReadBit();
            var bits68 = packet.ReadBits("bits68", 24);
            guid3[1] = packet.ReadBit();

            var guid9 = new byte[bits68][];
            for (var i = 0; i < bits68; ++i)
            {
                guid9[i] = new byte[8];
                packet.StartBitStream(guid9[i], 3, 1, 2, 7, 5, 6, 4, 0);
            }

            var bit384 = !packet.ReadBit("bit384");
            guid3[5] = packet.ReadBit();
            guid4[1] = packet.ReadBit();
            var bits320 = packet.ReadBits("bits320", 21);
            var bits340 = packet.ReadBits("bits340", 3);
            packet.ReadBits("bits11", 12);

            if (bit160)
            {
                var pos = new Vector3();
                packet.ReadXORBytes(guid7, 0, 1);
                pos.Z = packet.ReadSingle();
                packet.ReadXORBytes(guid7, 5, 3);
                pos.Y = packet.ReadSingle();
                packet.ReadXORByte(guid7, 2);
                pos.X = packet.ReadSingle();
                packet.ReadXORBytes(guid7, 6, 7, 4);
                packet.WriteGuid("Destination Transport GUID", guid7);
                packet.WriteLine("Destination Position: {0}", pos);
            }
            packet.ParseBitStream(TargetGUID, 1, 0, 5, 2, 3, 4, 7, 6);
            packet.ReadXORByte(guid4, 3);
            packet.WriteGuid("TargetGUID", TargetGUID);
            if (bit372)
            {
                packet.ReadUInt32("unk95");
                packet.ReadUInt32("unk94");
            }

            for (var i = 0; i < bits68; ++i)
            {
                packet.ParseBitStream(guid9[i], 1, 4, 5, 6, 2, 7, 3, 0);
                packet.WriteGuid("GUID9", guid9[i], i);
            }

            for (var i = 0; i < bits52; ++i)
            {
                packet.ParseBitStream(CasterGUID1[i], 0, 4, 3, 2, 7, 1, 5, 6);
                packet.WriteGuid("CasterGUID1", CasterGUID1[i], i);
            }

            packet.ParseBitStream(CasterGUID2, 4, 5, 7, 0, 1, 3, 2, 6);
            packet.WriteGuid("CasterGUID2", CasterGUID2);

            for (var i = 0; i < bits320; ++i)
            {
                packet.ReadEnum<PowerType>("Power Type", TypeCode.Byte, i);
                packet.ReadInt32("Power Value", i);
            }

            if (HasPowerData)
            {
                packet.ReadXORByte(PowerUnitGUID, 0);
                packet.ReadInt32("Current Health");
                packet.ReadXORByte(PowerUnitGUID, 2);
                packet.ReadInt32("Spell power");
                packet.ReadXORByte(PowerUnitGUID, 5);
                packet.ReadInt32("Attack power");
                packet.ReadXORByte(PowerUnitGUID, 1);

                for (var i = 0; i < PowerTypeCount; ++i)
                {
                    packet.ReadEnum<PowerType>("Power Type", TypeCode.UInt32, i);
                    packet.ReadInt32("Power Value", i);
                }
                packet.ReadXORBytes(PowerUnitGUID, 6, 7, 4, 3);
                packet.WriteGuid("PowerUnitGUID", PowerUnitGUID);
            }

            if (bit89)
                packet.ReadUInt32("unk89");

            packet.ParseBitStream(guid6, 1, 7, 4, 3, 5, 2, 0, 6);
            packet.WriteGuid("GUID6", guid6);
            packet.ReadXORBytes(guid3, 3, 4);

            if (bit128)
            {
                var pos = new Vector3();
                packet.ReadXORBytes(guid8, 2, 4, 1);
                pos.Z = packet.ReadSingle();
                packet.ReadXORBytes(guid8, 0, 5, 3);
                pos.X = packet.ReadSingle();
                pos.Y = packet.ReadSingle();
                packet.ReadXORBytes(guid8, 7, 6);
                packet.WriteGuid("Source Transport GUID", guid8);
                packet.WriteLine("Source Position: {0}", pos);
            }

            for (var i = 0; i < bits340; ++i)
                packet.ReadByte("byte86");

            for (var i = 0; i < bits388; ++i)
            {
                var pos = new Vector3();
                packet.ReadXORBytes(guid5[i], 4, 2);
                pos.X = packet.ReadSingle();
                packet.ReadXORBytes(guid5[i], 5, 7, 0);
                pos.Y = packet.ReadSingle();
                packet.ReadXORBytes(guid5[i], 1, 3, 6);
                pos.Z = packet.ReadSingle();
                packet.WriteGuid("GUID5", guid5[i], i);
                packet.WriteLine("[{1}] Position: {0}", pos, i);
            }
            packet.ReadXORByte(guid4, 2);

            if (bit336)
                packet.ReadByte("byte336");

            if (bit90)
                packet.ReadSingle("float90");

            packet.ReadEnum<CastFlag>("Cast Flags", TypeCode.Int32);
            packet.ReadXORByte(guid3, 2);
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
            packet.ReadXORByte(guid4, 0);
            packet.ReadXORByte(guid3, 5);

            if (bit428)
                packet.ReadByte("byte428");

            packet.ReadXORBytes(guid3, 0, 6, 1);
            packet.ReadXORByte(guid4, 1);

            if (bit337)
                packet.ReadByte("byte337");

            if (bit101)
                packet.ReadUInt32("unk101");

            packet.ReadXORByte(guid4, 4);

            if (bit368)
                packet.ReadByte("byte368");

            if (bit384)
                packet.ReadByte("byte384");

            packet.ReadWoWString("String48:", bits48);

            packet.ReadXORByte(guid4, 7);
            packet.ReadByte("Cast Count");

            if (bit102)
                packet.ReadUInt32("unk102");

            packet.ReadXORByte(guid4, 6);

            if (bit106)
                packet.ReadUInt32("Heal");

            packet.ReadUInt32("Cast time");
            packet.ReadXORByte(guid4, 5);

            if (bit91)
                packet.ReadUInt32("unk91");

            packet.ReadXORByte(guid3, 7);
            packet.WriteGuid("GUID3", guid3);
            packet.WriteGuid("GUID4", guid4);
        }

        [Parser(Opcode.SMSG_INITIAL_SPELLS)]
        public static void HandleInitialSpells(Packet packet)
        {
            var count = packet.ReadBits("Spell Count", 22);
            packet.ReadBit("Unk Bit");

            var spells = new List<uint>((int)count);
            for (var i = 0; i < count; i++)
            {
                var spellId = packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID", i);
                spells.Add((uint)spellId);
            }

            var startSpell = new StartSpell { Spells = spells };

            WoWObject character;
            if (Storage.Objects.TryGetValue(SessionHandler.LoginGuid, out character))
            {
                var player = character as Player;
                if (player != null && player.FirstLogin)
                    Storage.StartSpells.Add(new Tuple<Race, Class>(player.Race, player.Class), startSpell, packet.TimeSpan);
            }
        }

        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];
            var guid3 = new byte[8];
            byte[][] guid4;
            byte[][] guid5;
            var guid6 = new byte[8];
            byte[][] guid7;
            var guid8 = new byte[8];
            var guid9 = new byte[8];
            var guid10 = new byte[8];
            var guid11 = new byte[8];

            uint counter5 = 0;

            // main func sub_BF21FA
            guid10[5] = packet.ReadBit();
            var counter = packet.ReadBits(24); // field_74
            var bit476 = !packet.ReadBit("field_1DC");
            var bit404 = !packet.ReadBit("field_193");
            packet.ReadBit("field_A8");
            guid11[6] = packet.ReadBit();
            var bit432 = !packet.ReadBit("field_1B0");
            var counter3 = packet.ReadBits(20);
            packet.StartBitStream(guid, 2, 1, 0, 4, 5, 6, 3, 7);
            packet.StartBitStream(guid10, 0, 1, 3);

            guid7 = new byte[counter][];
            for (var i = 0; i < counter; ++i)
            {
                guid7[i] = new byte[8];
                packet.StartBitStream(guid7[i], 5, 6, 4, 7, 1, 2, 3, 0);
            }

            var bit452 = !packet.ReadBit("field_1C4");
            guid11[5] = packet.ReadBit();
            var bit385 = !packet.ReadBit("field_17F");
            var counter7 = packet.ReadBits(21); // field_16F
            var bit408 = !packet.ReadBit("field_197");
            var counter2 = packet.ReadBits(24); // field_64
            var bit16 = packet.ReadBit(); // field_10
            packet.StartBitStream(guid10, 6, 2);
            guid11[7] = packet.ReadBit();
            var counter6 = packet.ReadBits(3); // field_183
            var bit152 = !packet.ReadBit(); // field_95
            var bit176 = packet.ReadBit(); // field_B0

            if (bit16)
            {
                packet.StartBitStream(guid2, 7, 4, 5, 0, 2, 6, 3, 1);
                counter5 = packet.ReadBits(21); // field_2C
            }

            var bit412 = !packet.ReadBit(); // field_19B

            if (bit176)
                packet.StartBitStream(guid3, 6, 3, 0, 1, 4, 5, 2, 7);

            guid11[1] = packet.ReadBit();

            guid4 = new byte[counter2][];
            for (var i = 0; i < counter2; ++i)
            {
                guid4[i] = new byte[8];
                packet.StartBitStream(guid4[i], 6, 4, 1, 7, 5, 2, 3, 0);
            }

            guid5 = new byte[counter3][];
            for (var i = 0; i < counter3; ++i)
            {
                guid5[i] = new byte[8];
                packet.StartBitStream(guid5[i], 7, 6, 5, 0, 4, 3, 1, 2);
            }
            var bit416 = !packet.ReadBit(); // field_19F
            guid11[0] = packet.ReadBit();
            packet.ReadBits(12); // field_5C
            packet.StartBitStream(guid10, 7, 4);
            var bit208 = packet.ReadBit(); // field_CF

            if (bit208)
                packet.StartBitStream(guid6, 0, 2, 7, 6, 1, 4, 3, 5);

            var bit384 = !packet.ReadBit(); // field_17F
            var bit456 = !packet.ReadBit(); // field_1C8
            guid11[3] = packet.ReadBit();
            var bit420 = packet.ReadBit(); // field_1A3
            var bit472 = !packet.ReadBit(); // field_1D8
            guid11[4] = packet.ReadBit();
            var unkflag27 = packet.ReadBit(); // field_EF

            int bits7 = 0;
            if (!unkflag27)
                bits7 = (int)packet.ReadBits(7); // field_EF

            var counter4 = packet.ReadBits(25); // field_81
            guid11[2] = packet.ReadBit();

            for (var i = 0; i < counter4; ++i)
            {
                var bit136 = packet.ReadBits(4); // field_85

                if (bit136 == 11)
                    packet.ReadBits(4);
            }

            packet.ReadBit("unk464"); // field_1D0
            packet.StartBitStream(guid8, 7, 3, 6, 4, 2, 5, 0, 1);
            packet.ReadBit("unk160"); // field_9D
            packet.StartBitStream(guid9, 3, 5, 2, 1, 4, 0, 6, 7);

            if (bit152)
                packet.ReadBits(20); // field_95

            if (bit176)
            {
                var pos = new Vector3();
                packet.ReadXORBytes(guid3, 3, 7, 4);
                pos.X = packet.ReadSingle();
                packet.ReadXORBytes(guid3, 6, 5, 2);
                pos.Z = packet.ReadSingle();
                pos.Y = packet.ReadSingle();
                packet.ReadXORBytes(guid3, 0, 1);
                packet.WriteGuid("GUID3", guid3);
                packet.WriteLine("Position: {0}", pos);
            }


            for (var i = 0; i < counter3; ++i)
            {
                var pos = new Vector3();
                packet.ReadXORBytes(guid5[i], 4, 0, 7, 6, 2, 1);
                pos.X = packet.ReadSingle();
                packet.ReadXORBytes(guid5[i], 5, 3);
                pos.Y = packet.ReadSingle();
                pos.Z = packet.ReadSingle();
                packet.WriteGuid("GUID5", guid5[i], i);
                packet.WriteLine("Position: {0}", pos);
            }

            for (var i = 0; i < counter; ++i)
            {
                packet.ParseBitStream(guid7[i], 2, 1, 5, 3, 7, 0, 4, 6);
                packet.WriteGuid("GUID7", guid7[i], i);
            }

            if (bit208)
            {
                var pos = new Vector3();
                packet.ReadXORBytes(guid6, 1, 4);
                pos.Z = packet.ReadSingle();
                packet.ReadXORBytes(guid6, 0, 7, 3, 5);
                pos.Y = packet.ReadSingle();
                packet.ReadXORByte(guid6, 6);
                pos.X = packet.ReadSingle();
                packet.ReadXORByte(guid6, 2);
                packet.WriteGuid("GUID6", guid6);
                packet.WriteLine("Position: {0}", pos);
            }
            if (bit408)
                packet.ReadSingle("float198");

            if (bit416)
                packet.ReadByte("unk.416"); // field_19F

            packet.ParseBitStream(guid8, 5, 0, 2, 7, 6, 3, 4, 1);
            packet.WriteGuid("GUID8", guid8);

            for (var i = 0; i < counter2; ++i)
            {
                packet.ParseBitStream(guid4[i], 6, 3, 1, 0, 2, 4, 7, 5);
                packet.WriteGuid("GUID4", guid4[i], i);
            }

            if (bit16)
            {
                packet.ReadXORByte(guid2, 3);
                packet.ReadUInt32("Spell Power"); // field_28
                packet.ReadXORBytes(guid2, 6, 4, 7, 0);
                packet.ReadUInt32("Attack Power"); // field_24
                packet.ReadXORByte(guid2, 2);

                for (var i = 0; i < counter5; ++i)
                {
                    packet.ReadUInt32("uint32 60");
                    packet.ReadUInt32("uint32 48");
                }

                packet.ReadUInt32("Health");
                packet.ReadXORBytes(guid2, 1, 5);
                packet.WriteGuid("GUID2", guid2);
            }

            packet.ParseBitStream(guid9, 2, 3, 4, 7, 5, 1, 6, 0);
            packet.WriteGuid("GUID9", guid9);

            packet.ReadByte("Cast Count"); // field_50

            packet.ParseBitStream(guid, 0, 1, 4, 5, 7, 6, 2, 3);
            packet.WriteGuid("GUID", guid);

            packet.ReadXORByte(guid10, 7);
            if (bit476)
                packet.ReadByte("unk476"); // field_1DC

            if (bit472)
                packet.ReadUInt32("uint32 472"); // field_1D8

            packet.ReadXORByte(guid10, 3);

            packet.ReadWoWString("Text", bits7);

            if (bit404)
                packet.ReadUInt32("uint32 404"); // field_193

            if (bit456)
                packet.ReadUInt32("uint32 456"); // field_1C8

            packet.ReadXORByte(guid10, 2);
            packet.ReadXORByte(guid11, 0);

            if (bit420)
            {
                packet.ReadUInt32("uint32 424"); // field_1A8
                packet.ReadUInt32("uint32 428"); // field_1AC
            }

            if (bit412)
                packet.ReadUInt32("uint32 412"); // field_19B

            packet.ReadXORByte(guid11, 2);
            packet.ReadXORBytes(guid10, 0, 1);
            packet.ReadXORByte(guid11, 3);

            for (var i = 0; i < counter6; ++i) // field_183
                packet.ReadByte("unk392"); // field_187

            if (bit384)
                packet.ReadByte("unk384"); // field_17F

            packet.ReadXORByte(guid11, 1);
            packet.ReadXORByte(guid10, 4);
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID"); // field_54

            if (bit385)
                packet.ReadByte("Effect Count"); // field_17F

            packet.ReadXORByte(guid10, 6);
            packet.ReadXORByte(guid11, 5);
            packet.ReadUInt32("uint32 88"); // field_58
            packet.ReadXORByte(guid11, 4);
            packet.ReadUInt32("uint32 96"); // field_60

            for (var i = 0; i < counter7; ++i)
            {
                packet.ReadUInt32("uint32 372"); // field_173
                packet.ReadByte("unk380"); // field_173
            }
            packet.ReadXORByte(guid10, 5);
            packet.ReadXORByte(guid11, 6);

            if (bit452)
                packet.ReadUInt32("uint 452"); // field_1C4

            packet.ReadXORByte(guid11, 7);

            if (bit432)
                packet.ReadByte("unk432"); // field_1B0

            packet.WriteGuid("GUID10", guid10);
            packet.WriteGuid("GUID11", guid11);
        }
    }
}

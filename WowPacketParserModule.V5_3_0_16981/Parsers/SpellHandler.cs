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
                packet.ReadXORBytes(GUID3, 7, 5, 3);
                packet.ReadSingle("float68");
                packet.ReadXORBytes(GUID3, 0, 2, 1, 4, 6);
                packet.ReadSingle("float70");
                packet.ReadSingle("float6C");
                packet.WriteGuid("GUID3", GUID3);
            }

            packet.ParseBitStream(TargetGUID, 2, 0, 5, 6, 7, 3, 4, 1);
            packet.WriteGuid("Target GUID", TargetGUID);

            if (hasGUID2)
            {
                packet.ReadXORBytes(GUID2, 5, 7);
                packet.ReadSingle("float4C");
                packet.ReadSingle("float48");
                packet.ReadXORBytes(GUID2, 3, 1);
                packet.ReadSingle("float50");
                packet.ReadXORBytes(GUID2, 2, 6, 4, 0);
                packet.WriteGuid("GUID2", GUID2);
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
                CasterGUID1[i][2] = packet.ReadBit();
                CasterGUID1[i][5] = packet.ReadBit();
                CasterGUID1[i][4] = packet.ReadBit();
                CasterGUID1[i][7] = packet.ReadBit();
                CasterGUID1[i][6] = packet.ReadBit();
                CasterGUID1[i][0] = packet.ReadBit();
                CasterGUID1[i][3] = packet.ReadBit();
                CasterGUID1[i][1] = packet.ReadBit();
            }
            var bit28 = packet.ReadBit("bit28");
            var bit106 = !packet.ReadBit("bit106");
            var bit30 = packet.ReadBit("bit30");

            CasterGUID2[5] = packet.ReadBit();
            CasterGUID2[4] = packet.ReadBit();
            CasterGUID2[7] = packet.ReadBit();
            CasterGUID2[1] = packet.ReadBit();
            CasterGUID2[0] = packet.ReadBit();
            CasterGUID2[6] = packet.ReadBit();
            CasterGUID2[3] = packet.ReadBit();
            CasterGUID2[2] = packet.ReadBit();
            guid4[5] = packet.ReadBit();
            guid4[6] = packet.ReadBit();
            guid3[2] = packet.ReadBit();
            var bit372 = packet.ReadBit("bit372");
            TargetGUID[0] = packet.ReadBit();
            TargetGUID[3] = packet.ReadBit();
            TargetGUID[1] = packet.ReadBit();
            TargetGUID[5] = packet.ReadBit();
            TargetGUID[6] = packet.ReadBit();
            TargetGUID[2] = packet.ReadBit();
            TargetGUID[7] = packet.ReadBit();
            TargetGUID[4] = packet.ReadBit();

            var HasPowerData = packet.ReadBit("HasPowerData"); // bit432
            uint PowerTypeCount = 0;
            if (HasPowerData)
            {
                PowerUnitGUID[6] = packet.ReadBit();
                PowerUnitGUID[7] = packet.ReadBit();
                PowerUnitGUID[3] = packet.ReadBit();
                PowerUnitGUID[5] = packet.ReadBit();
                PowerUnitGUID[0] = packet.ReadBit();
                PowerUnitGUID[4] = packet.ReadBit();
                PowerUnitGUID[2] = packet.ReadBit();
                PowerUnitGUID[1] = packet.ReadBit();
                PowerTypeCount = packet.ReadBits("bits460", 21);
            }
            guid3[6] = packet.ReadBit();
            guid3[0] = packet.ReadBit();
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
                guid5[i][0] = packet.ReadBit();
                guid5[i][5] = packet.ReadBit();
                guid5[i][2] = packet.ReadBit();
                guid5[i][7] = packet.ReadBit();
                guid5[i][6] = packet.ReadBit();
                guid5[i][4] = packet.ReadBit();
                guid5[i][3] = packet.ReadBit();
                guid5[i][1] = packet.ReadBit();

            }
            var bit104 = packet.ReadBit("bit104");
            var bit90 = !packet.ReadBit("bit90");

            for (var i = 0; i < bits84; ++i)
            {
                if (packet.ReadBits("bits22[0]", 4) == 11)
                    packet.ReadBits("bits22[1]", 4);
            }

            guid6[2] = packet.ReadBit();
            guid6[1] = packet.ReadBit();
            guid6[7] = packet.ReadBit();
            guid6[0] = packet.ReadBit();
            guid6[6] = packet.ReadBit();
            guid6[3] = packet.ReadBit();
            guid6[5] = packet.ReadBit();
            guid6[4] = packet.ReadBit();
            guid4[7] = packet.ReadBit();
            var bit160 = packet.ReadBit("bit160");
            var bit128 = packet.ReadBit("bit128");
            guid4[2] = packet.ReadBit();

            if (bit160)
            {
                guid7[3] = packet.ReadBit();
                guid7[7] = packet.ReadBit();
                guid7[1] = packet.ReadBit();
                guid7[0] = packet.ReadBit();
                guid7[5] = packet.ReadBit();
                guid7[6] = packet.ReadBit();
                guid7[4] = packet.ReadBit();
                guid7[2] = packet.ReadBit();
            }
            guid4[3] = packet.ReadBit();
            var bit89 = !packet.ReadBit("bit89");

            if (bit128)
            {
                guid8[5] = packet.ReadBit();
                guid8[4] = packet.ReadBit();
                guid8[3] = packet.ReadBit();
                guid8[2] = packet.ReadBit();
                guid8[0] = packet.ReadBit();
                guid8[6] = packet.ReadBit();
                guid8[7] = packet.ReadBit();
                guid8[1] = packet.ReadBit();
            }
            var bit48 = packet.ReadBit("bezunkflag27?");

            if (bit26)
            {
                var bits26 = packet.ReadBits("bits26", 20);
            }

            var bit337 = !packet.ReadBit("bit337");

            int bits48 = 0;
            if (!bit48)
            {
                bits48 = (int)packet.ReadBits("bits48", 7);
            }

            var bit428 = !packet.ReadBit("bit428");
            guid3[3] = packet.ReadBit();
            var bits68 = packet.ReadBits("bits68", 24);
            guid3[1] = packet.ReadBit();

            var guid9 = new byte[bits68][];
            for (var i = 0; i < bits68; ++i)
            {
                guid9[i] = new byte[8];
                guid9[i][3] = packet.ReadBit();
                guid9[i][1] = packet.ReadBit();
                guid9[i][2] = packet.ReadBit();
                guid9[i][7] = packet.ReadBit();
                guid9[i][5] = packet.ReadBit();
                guid9[i][6] = packet.ReadBit();
                guid9[i][4] = packet.ReadBit();
                guid9[i][0] = packet.ReadBit();
            }
            var bit384 = !packet.ReadBit("bit384");
            guid3[5] = packet.ReadBit();
            guid4[1] = packet.ReadBit();
            var bits320 = packet.ReadBits("bits320", 21);
            var bits340 = packet.ReadBits("bits340", 3);
            var bits11 = packet.ReadBits("bits11", 12);

            if (bit160)
            {
                packet.ReadXORByte(guid7, 0);
                packet.ReadXORByte(guid7, 1);
                var float46 = packet.ReadSingle("float46");
                packet.ReadXORByte(guid7, 5);
                packet.ReadXORByte(guid7, 3);
                var float45 = packet.ReadSingle("float45");
                packet.ReadXORByte(guid7, 2);
                var float44 = packet.ReadSingle("float44");
                packet.ReadXORByte(guid7, 6);
                packet.ReadXORByte(guid7, 7);
                packet.ReadXORByte(guid7, 4);
                packet.WriteGuid("GUID7", guid7);
            }
            packet.ReadXORByte(TargetGUID, 1);
            packet.ReadXORByte(TargetGUID, 0);
            packet.ReadXORByte(TargetGUID, 5);
            packet.ReadXORByte(TargetGUID, 2);
            packet.ReadXORByte(TargetGUID, 3);
            packet.ReadXORByte(TargetGUID, 4);
            packet.ReadXORByte(TargetGUID, 7);
            packet.ReadXORByte(TargetGUID, 6);
            packet.ReadXORByte(guid4, 3);
            packet.WriteGuid("TargetGUID", TargetGUID);
            if (bit372)
            {
                packet.ReadUInt32("unk95");
                packet.ReadUInt32("unk94");
            }

            for (var i = 0; i < bits68; ++i)
            {
                packet.ReadXORByte(guid9[i], 1);
                packet.ReadXORByte(guid9[i], 4);
                packet.ReadXORByte(guid9[i], 5);
                packet.ReadXORByte(guid9[i], 6);
                packet.ReadXORByte(guid9[i], 2);
                packet.ReadXORByte(guid9[i], 7);
                packet.ReadXORByte(guid9[i], 3);
                packet.ReadXORByte(guid9[i], 0);
                packet.WriteGuid("GUID9", guid9[i], i);
            }

            for (var i = 0; i < bits52; ++i)
            {
                packet.ReadXORByte(CasterGUID1[i], 0);
                packet.ReadXORByte(CasterGUID1[i], 4);
                packet.ReadXORByte(CasterGUID1[i], 3);
                packet.ReadXORByte(CasterGUID1[i], 2);
                packet.ReadXORByte(CasterGUID1[i], 7);
                packet.ReadXORByte(CasterGUID1[i], 1);
                packet.ReadXORByte(CasterGUID1[i], 5);
                packet.ReadXORByte(CasterGUID1[i], 6);
                packet.WriteGuid("CasterGUID1", CasterGUID1[i], i);
            }

            packet.ReadXORByte(CasterGUID2, 4);
            packet.ReadXORByte(CasterGUID2, 5);
            packet.ReadXORByte(CasterGUID2, 7);
            packet.ReadXORByte(CasterGUID2, 0);
            packet.ReadXORByte(CasterGUID2, 1);
            packet.ReadXORByte(CasterGUID2, 3);
            packet.ReadXORByte(CasterGUID2, 2);
            packet.ReadXORByte(CasterGUID2, 6);
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
                packet.ReadXORByte(PowerUnitGUID, 6);
                packet.ReadXORByte(PowerUnitGUID, 7);
                packet.ReadXORByte(PowerUnitGUID, 4);
                packet.ReadXORByte(PowerUnitGUID, 3);
                packet.WriteGuid("PowerUnitGUID", PowerUnitGUID);
            }

            if (bit89)
                packet.ReadUInt32("unk89");

            packet.ReadXORByte(guid6, 1);
            packet.ReadXORByte(guid6, 7);
            packet.ReadXORByte(guid6, 4);
            packet.ReadXORByte(guid6, 3);
            packet.ReadXORByte(guid6, 5);
            packet.ReadXORByte(guid6, 2);
            packet.ReadXORByte(guid6, 0);
            packet.ReadXORByte(guid6, 6);
            packet.WriteGuid("GUID6", guid6);
            packet.ReadXORByte(guid3, 3);
            packet.ReadXORByte(guid3, 4);

            if (bit128)
            {
                packet.ReadXORByte(guid8, 2);
                packet.ReadXORByte(guid8, 4);
                packet.ReadXORByte(guid8, 1);
                packet.ReadSingle("float38");
                packet.ReadXORByte(guid8, 0);
                packet.ReadXORByte(guid8, 5);
                packet.ReadXORByte(guid8, 3);
                packet.ReadSingle("float36");
                packet.ReadSingle("float37");
                packet.ReadXORByte(guid8, 7);
                packet.ReadXORByte(guid8, 6);
                packet.WriteGuid("GUID8", guid8);
            }

            for (var i = 0; i < bits340; ++i)
            {
                packet.ReadByte("byte86");
            }

            for (var i = 0; i < bits388; ++i)
            {
                packet.ReadXORByte(guid5[i], 4);
                packet.ReadXORByte(guid5[i], 2);
                packet.ReadSingle("float106");
                packet.ReadXORByte(guid5[i], 5);
                packet.ReadXORByte(guid5[i], 7);
                packet.ReadXORByte(guid5[i], 0);
                packet.ReadSingle("float110");
                packet.ReadXORByte(guid5[i], 1);
                packet.ReadXORByte(guid5[i], 3);
                packet.ReadXORByte(guid5[i], 6);
                packet.ReadSingle("float114");
                packet.WriteGuid("GUID5", guid5[i], i);
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

            packet.ReadXORByte(guid3, 0);
            packet.ReadXORByte(guid3, 6);
            packet.ReadXORByte(guid3, 1);
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
    }
}

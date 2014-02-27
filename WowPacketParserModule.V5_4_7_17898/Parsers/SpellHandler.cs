using System;
using System.Text;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.V5_4_7_17898.Parsers
{
    public static class SpellHandler
    {
        [HasSniffData]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            var bits0 = 0;
            var guid2 = new byte[8];
            var bit18 = false;

            guid2[3] = packet.ReadBit();
            bit18 = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            bits0 = (int)packet.ReadBits(24);
            guid2[7] = packet.ReadBit();
            guid2[6] = packet.ReadBit();

            var hasCasterGUID = new bool[bits0];
            var hasDuration = new bool[bits0];
            var hasMaxDuration = new bool[bits0];
            var hasAura = new bool[bits0];
            var effectCount = new uint[bits0];
            var bits48 = new uint[bits0];
            var casterGUID = new byte[bits0][];

            for (var i = 0; i < bits0; ++i)
            {
                hasAura[i] = packet.ReadBit();
                if (hasAura[i])
                {
                    hasMaxDuration[i] = packet.ReadBit(); 
                    bits48[i] = packet.ReadBits(22);
                    hasDuration[i] = packet.ReadBit();
                    hasCasterGUID[i] = packet.ReadBit();
                    effectCount[i] = packet.ReadBits(22);

                    if (hasCasterGUID[i])
                    {
                        casterGUID[i] = new byte[8];
                        packet.StartBitStream(casterGUID[i], 1, 6, 0, 7, 5, 3, 2, 4);
                    }
                }
            }

            guid2[2] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[1] = packet.ReadBit();

            var auras = new List<Aura>();
            for (var i = 0; i < bits0; ++i)
            {
                if (hasAura[i])
                {
                    var aura = new Aura();
                    if (hasCasterGUID[i])
                    {
                        packet.ParseBitStream(casterGUID[i], 2, 5, 6, 7, 0, 1, 4, 3);
                        packet.WriteGuid("Caster GUID", casterGUID[i], i);
                        aura.CasterGuid = new Guid(BitConverter.ToUInt64(casterGUID[i], 0));
                    }

                    for (var j = 0; j < bits48[i]; ++j)
                    {
                        packet.ReadSingle("FloatEM", i, j);
                    }

                    packet.ReadInt32("Effect Mask", i);

                    for (var j = 0; j < effectCount[i]; ++j)
                    {
                        packet.ReadSingle("Effect Value", i, j);
                    }

                    aura.AuraFlags = packet.ReadEnum<AuraFlagMoP>("Flags", TypeCode.Byte, i);
                    aura.SpellId = packet.ReadUInt32("Spell Id", i);
                    aura.Level = packet.ReadUInt16("Caster Level", i);
                    aura.Charges = packet.ReadByte("Charges", i);

                    if (hasMaxDuration[i])
                        aura.MaxDuration = packet.ReadInt32("Max Duration", i);
                    else
                        aura.MaxDuration = 0;

                    if (hasDuration[i])
                        aura.Duration = packet.ReadInt32("Duration", i);
                    else
                        aura.Duration = 0;
                }
                packet.ReadByte("Slot", i);
            }
            packet.ParseBitStream(guid2, 0, 1, 3, 4, 2, 6, 7, 5);
            packet.WriteGuid("GUID2", guid2);
        }
        [Parser(Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellGo(Packet packet)
        {
            var bits2C = 0;
            var bits34 = 0;
            var bits44 = 0;
            var bits54 = 0;
            var bitsC0 = 0;
            var bits140 = 0;
            var bits154 = 0;
            var bits184 = 0;
            var bits1BC = 0;

            
            var guid2 = new byte[8];
            var guid3 = new byte[8];
            
            
            var guid6 = new byte[8];
            var guid7 = new byte[8];
            var guid8 = new byte[8];
            var guid9 = new byte[8];
            var guid10 = new byte[8];

            var bit1 = new bool[bits34];
            var bit2 = new bool[bits34];
            var bit3 = new bool[bits34];
            var bit4 = new bool[bits34];
            var bit5 = new bool[bits34];
            var bit6 = new bool[bits34];
            var bit7 = new bool[bits34];
            var bit38 = new bool[bits34];
            var bit48 = new bool[bits44];

            packet.ReadBit(); // fake bit
            var bit164 = !packet.ReadBit();
            guid2[1] = packet.ReadBit();
            var bit1AC = !packet.ReadBit();
            var bitC0 = !packet.ReadBit(); // some flag
            guid2[3] = packet.ReadBit();
            guid3[3] = packet.ReadBit();
            var bit180 = !packet.ReadBit();
            guid6[4] = packet.ReadBit();
            guid6[2] = packet.ReadBit();
            guid6[3] = packet.ReadBit();
            guid6[7] = packet.ReadBit();
            guid6[1] = packet.ReadBit();
            guid6[5] = packet.ReadBit();
            guid6[6] = packet.ReadBit();
            guid6[0] = packet.ReadBit();

            if (bitC0)
                bitsC0 = (int)packet.ReadBits(7);

            packet.ReadBit(); // fake bit
            guid2[4] = packet.ReadBit();
            var bit1A8 = !packet.ReadBit();
            var hasTargetFlags = !packet.ReadBit();
            guid10[3] = packet.ReadBit();
            guid10[2] = packet.ReadBit();
            guid10[6] = packet.ReadBit();
            guid10[5] = packet.ReadBit();
            guid10[7] = packet.ReadBit();
            guid10[4] = packet.ReadBit();
            guid10[0] = packet.ReadBit();
            guid10[1] = packet.ReadBit();
            bits34 = (int)packet.ReadBits(24);
            guid2[2] = packet.ReadBit();
            guid3[4] = packet.ReadBit();
            guid3[2] = packet.ReadBit();
            guid2[5] = packet.ReadBit();

            var guid = new byte[bits34][];
            for (var i = 0; i < bits34; ++i)
            {
                guid[i] = new byte[8];
                packet.StartBitStream(guid[i], 1, 3, 0, 6, 4, 7, 5, 2);
            }

            guid3[1] = packet.ReadBit();
            var bit151 = !packet.ReadBit();
            guid3[7] = packet.ReadBit();
            var bit16C = !packet.ReadBit();
            guid2[0] = packet.ReadBit();
            var bit170 = !packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid3[0] = packet.ReadBit();
            bits44 = (int)packet.ReadBits(24);
            var bit194 = !packet.ReadBit();

            var guid4 = new byte[bits44][];
            for (var i = 0; i < bits44; ++i)
            {
                guid4[i] = new byte[8];
                packet.StartBitStream(guid4[i], 6, 7, 5, 3, 4, 1, 0, 2);
            }

            var bit150 = !packet.ReadBit();
            bits154 = (int)packet.ReadBits(3);
            bits54 = (int)packet.ReadBits(25);
            bits2C = (int)packet.ReadBits(13);

            for (var i = 0; i < bits54; ++i)
            {
                var bits136 = (int)packet.ReadBits(4);
                if (bits136 == 11)
                    packet.ReadBits("bits140", 4, i);
            }

            var bit17C = packet.ReadBit();
            bits184 = (int)packet.ReadBits(20);

            var guid5 = new byte[bits184][];
            for (var i = 0; i < bits184; ++i)
            {
                guid5[i] = new byte[8];
                packet.StartBitStream(guid5[i], 6, 4, 7, 0, 1, 2, 3, 5);
            }

            packet.ReadBit(); // fake bit
            guid7[1] = packet.ReadBit();
            guid7[4] = packet.ReadBit();
            guid7[5] = packet.ReadBit();
            guid7[6] = packet.ReadBit();
            guid7[0] = packet.ReadBit();
            guid7[3] = packet.ReadBit();
            guid7[2] = packet.ReadBit();
            guid7[7] = packet.ReadBit();

            var bit1CC = packet.ReadBit(); // +460

            if (bit1CC)
                bits1BC = (int)packet.ReadBits(21); // +444

            var bit198 = !packet.ReadBit();
            var bitB8 = packet.ReadBit();

            if (bitB8)
            {
                guid9[0] = packet.ReadBit();
                guid9[5] = packet.ReadBit();
                guid9[6] = packet.ReadBit();
                guid9[3] = packet.ReadBit();
                guid9[1] = packet.ReadBit();
                guid9[2] = packet.ReadBit();
                guid9[7] = packet.ReadBit();
                guid9[4] = packet.ReadBit();
            }

            guid3[6] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            bits140 = (int)packet.ReadBits(21);
            guid3[5] = packet.ReadBit();
            var bit168 = !packet.ReadBit();
            var bit98 = packet.ReadBit();

            if (bit98)
            {
                guid8[6] = packet.ReadBit();
                guid8[2] = packet.ReadBit();
                guid8[7] = packet.ReadBit();
                guid8[0] = packet.ReadBit();
                guid8[1] = packet.ReadBit();
                guid8[3] = packet.ReadBit();
                guid8[5] = packet.ReadBit();
                guid8[4] = packet.ReadBit();
            }

            if (hasTargetFlags)
                packet.ReadEnum<TargetFlag>("Target Flags", 20);
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");

            if (bitB8)
            {
                packet.ReadXORByte(guid9, 4);
                packet.ReadXORByte(guid9, 3);
                packet.ReadXORByte(guid9, 1);
                packet.ReadXORByte(guid9, 0);
                packet.ReadXORByte(guid9, 5);
                packet.ReadSingle("FloatAC");
                packet.ReadXORByte(guid9, 6);
                packet.ReadSingle("FloatA8");
                packet.ReadXORByte(guid9, 7);
                packet.ReadXORByte(guid9, 2);
                packet.ReadSingle("FloatB0");
                packet.WriteGuid("guid9", guid9);
            }

            for (var i = 0; i < bits34; ++i)
            {
                packet.ParseBitStream(guid[i], 4, 7, 5, 1, 0, 3, 6, 2);
                packet.WriteGuid("GUID", guid[i], i);
            }

            packet.ReadXORByte(guid3, 1);
            packet.ReadXORByte(guid6, 4);
            packet.ReadXORByte(guid6, 0);
            packet.ReadXORByte(guid6, 7);
            packet.ReadXORByte(guid6, 5);
            packet.ReadXORByte(guid6, 6);
            packet.ReadXORByte(guid6, 2);
            packet.ReadXORByte(guid6, 1);
            packet.ReadXORByte(guid6, 3);

            if (bit1AC)
                packet.ReadByte("Byte1AC");

            for (var i = 0; i < bits44; ++i)
            {
                packet.ParseBitStream(guid4[i], 6, 5, 2, 0, 3, 4, 1, 7);
                packet.WriteGuid("GUID", guid4[i], i);
            }

            if (bit198)
                packet.ReadInt32("Int198");

            packet.ReadXORByte(guid10, 0);
            packet.ReadXORByte(guid10, 4);
            packet.ReadXORByte(guid10, 6);
            packet.ReadXORByte(guid10, 2);
            packet.ReadXORByte(guid10, 7);
            packet.ReadXORByte(guid10, 3);
            packet.ReadXORByte(guid10, 1);
            packet.ReadXORByte(guid10, 5);

            if (bit98)
            {
                packet.ReadSingle("Float90");
                packet.ReadXORByte(guid8, 5);
                packet.ReadXORByte(guid8, 1);
                packet.ReadXORByte(guid8, 3);
                packet.ReadXORByte(guid8, 2);
                packet.ReadXORByte(guid8, 7);
                packet.ReadSingle("Float88");
                packet.ReadXORByte(guid8, 0);
                packet.ReadXORByte(guid8, 6);
                packet.ReadXORByte(guid8, 4);
                packet.ReadSingle("Float8C");
                packet.WriteGuid("guid8", guid8);
            }

            packet.ReadXORByte(guid3, 7);
            if (bit164)
                packet.ReadInt32("Int164");
            if (bit16C)
                packet.ReadInt32("Int16C");
            if (bit194)
                packet.ReadInt32("Int194");
            packet.ReadXORByte(guid3, 0);
            packet.ReadXORByte(guid3, 6);
            packet.ReadXORByte(guid7, 1);
            packet.ReadXORByte(guid7, 4);
            packet.ReadXORByte(guid7, 5);
            packet.ReadXORByte(guid7, 2);
            packet.ReadXORByte(guid7, 6);
            packet.ReadXORByte(guid7, 7);
            packet.ReadXORByte(guid7, 0);
            packet.ReadXORByte(guid7, 3);

            if (bit1CC)
            {
                packet.ReadInt32("Int1B4");

                for (var i = 0; i < bits1BC; ++i)
                {
                    packet.ReadInt32("IntED", i);
                    packet.ReadInt32("IntED", i);
                }

                packet.ReadInt32("Int1B8");
                packet.ReadInt32("Int1B0");
            }

            packet.ReadXORByte(guid3, 4);

            for (var i = 0; i < bits184; ++i)
            {
                packet.ReadXORByte(guid5[i], 7);
                packet.ReadXORByte(guid5[i], 5);
                packet.ReadXORByte(guid5[i], 2);
                packet.ReadXORByte(guid5[i], 4);
                packet.ReadXORByte(guid5[i], 1);
                packet.ReadSingle("Float16");
                packet.ReadSingle("Float12");
                packet.ReadXORByte(guid5[i], 6);
                packet.ReadSingle("Float8");
                packet.ReadXORByte(guid5[i], 3);
                packet.ReadXORByte(guid5[i], 0);
                packet.WriteGuid("GUID5", guid5[i], i);
            }

            packet.ReadXORByte(guid3, 2);
            packet.ReadWoWString("StringC0", bitsC0);
            packet.ReadXORByte(guid3, 5);

            for (var i = 0; i < bits140; ++i)
            {
                packet.ReadByte("ByteED", i);
                packet.ReadInt32("IntED", i);
            }

            if (bit17C)
            {
                packet.ReadInt32("Int178");
                packet.ReadInt32("Int174");
            }

            packet.ReadInt32("Int28");
            packet.ReadInt32("Int30");
            packet.ReadXORByte(guid2, 7);
            packet.ReadByte("Byte20");

            for (var i = 0; i < bits154; ++i)
            {
                packet.ReadByte("Byte158", i);
            }

            if (bit180)
                packet.ReadByte("Byte180");
            packet.ReadXORByte(guid2, 3);
            if (bit170)
                packet.ReadByte("Byte170");
            packet.ReadXORByte(guid2, 4);
            if (bit168)
                packet.ReadSingle("Float168");
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 6);
            if (bit1A8)
                packet.ReadInt32("Int1A8");
            packet.ReadXORByte(guid3, 3);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 5);
            if (bit151)
                packet.ReadByte("Byte151");
            if (bit150)
                packet.ReadByte("Byte150");

            packet.WriteGuid("Guid2", guid2);
            packet.WriteGuid("Guid3", guid3);
            packet.WriteGuid("Guid6", guid6);
            packet.WriteGuid("Guid7", guid7);
            packet.WriteGuid("Guid10", guid10);

        }
        [Parser(Opcode.SMSG_LEARNED_SPELL)]
        public static void HandleLearnSpell(Packet packet)
        {
            packet.ReadBit("Unk Bits");
            var count = packet.ReadBits("Spell Count", 22);

            for (var i = 0; i < count; ++i)
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", i);
        }

        [Parser(Opcode.SMSG_REMOVED_SPELL)]
        public static void HandleRemovedSpell(Packet packet)
        {
            var count = packet.ReadBits("Spell Count", 22);

            for (var i = 0; i < count; ++i)
                packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID", i);
        }

        [Parser(Opcode.SMSG_INITIAL_SPELLS)]
        public static void HandleInitialSpells(Packet packet)
        {
            packet.ReadBit("Unk Bit");
            var count = packet.ReadBits("Spell Count", 22);

            var spells = new List<uint>((int)count);
            for (var i = 0; i < count; i++)
            {
                var spellId = packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID", i);
                spells.Add((uint)spellId);
            }

            var startSpell = new StartSpell { Spells = spells };

            WoWObject character;
            if (Storage.Objects.TryGetValue(CoreParsers.SessionHandler.LoginGuid, out character))
            {
                var player = character as Player;
                if (player != null && player.FirstLogin)
                    Storage.StartSpells.Add(new Tuple<Race, Class>(player.Race, player.Class), startSpell, packet.TimeSpan);
            }
        }

        [Parser(Opcode.SMSG_SET_FLAT_SPELL_MODIFIER)]
        public static void HandleSetSpellModifierFlat(Packet packet)
        {
            var modCount = packet.ReadBits("Modifier type count", 22);
            var modTypeCount = new uint[modCount];

            for (var j = 0; j < modCount; ++j)
                modTypeCount[j] = packet.ReadBits("Count", 21, j);

            for (var j = 0; j < modCount; ++j)
            {
                packet.ReadEnum<SpellModOp>("Spell Mod", TypeCode.Byte, j);

                for (var i = 0; i < modTypeCount[j]; ++i)
                {
                    packet.ReadSingle("Amount", j, i);
                    packet.ReadByte("Spell Mask bitpos", j, i);
                }
            }
        }

        [Parser(Opcode.SMSG_SET_PCT_SPELL_MODIFIER)]
        public static void HandleSetSpellModifierPct(Packet packet)
        {
            var modCount = packet.ReadBits("Modifier type count", 22);
            var modTypeCount = new uint[modCount];

            for (var j = 0; j < modCount; ++j)
                modTypeCount[j] = packet.ReadBits("Count", 21, j);

            for (var j = 0; j < modCount; ++j)
            {
                for (var i = 0; i < modTypeCount[j]; ++i)
                {
                    packet.ReadSingle("Amount", j, i);
                    packet.ReadByte("Spell Mask bitpos", j, i);
                }

                packet.ReadEnum<SpellModOp>("Spell Mod", TypeCode.Byte, j);
            }
        }

        [Parser(Opcode.SMSG_TALENTS_INFO)]
        public static void ReadTalentInfo(Packet packet)
        {
            packet.ReadByte("Active Spec Group");
            var specCount = packet.ReadBits("Spec Group count", 19);

            var spentTalents = new uint[specCount];

            for (var i = 0; i < specCount; ++i)
                spentTalents[i] = packet.ReadBits("Spec Talent Count", 23, i);

            for (var i = 0; i < specCount; ++i)
            {
                for (var j = 0; j < 6; ++j)
                    packet.ReadUInt16("Glyph", i, j);

                packet.ReadUInt32("Spec Id", i);

                for (var j = 0; j < spentTalents[i]; ++j)
                    packet.ReadUInt16("Talent Id", i, j);
            }
        }

        [Parser(Opcode.SMSG_SPELL_CATEGORY_COOLDOWN)]
        public static void HandleSpellCategoryCooldown(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);

            for (int i = 0; i < count; ++i)
            {
                packet.ReadInt32("Category Cooldown", i);
                packet.ReadInt32("Cooldown", i);
            }
        }
    }
}

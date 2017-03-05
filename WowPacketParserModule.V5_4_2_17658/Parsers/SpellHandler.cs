using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class SpellHandler
    {
        [HasSniffData]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            var guid = new byte[8];

            guid[3] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();

            var bits0 = packet.Translator.ReadBits(24);

            var bit10 = packet.Translator.ReadBit();

            var hasAura = new bool[bits0];
            var hasCasterGUID = new bool[bits0];
            var hasMaxDuration = new bool[bits0];
            var hasDuration = new bool[bits0];
            var bits48 = new uint[bits0];
            var casterGUID = new byte[bits0][];
            var effectCount = new uint[bits0];

            for (var i = 0; i < bits0; ++i)
            {
                hasAura[i] = packet.Translator.ReadBit();

                if (hasAura[i])
                {
                    effectCount[i] = packet.Translator.ReadBits(22);
                    bits48[i] = packet.Translator.ReadBits(22);

                    hasCasterGUID[i] = packet.Translator.ReadBit();
                    if (hasCasterGUID[i])
                    {
                        casterGUID[i] = new byte[8];
                        packet.Translator.StartBitStream(casterGUID[i], 7, 4, 2, 5, 6, 1, 3, 0);
                    }

                    hasMaxDuration[i] = packet.Translator.ReadBit();
                    hasDuration[i] = packet.Translator.ReadBit();
                }
            }

            guid[1] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();

            var auras = new List<Aura>();
            for (var i = 0; i < bits0; ++i)
            {
                if (hasAura[i])
                {
                    var aura = new Aura();
                    if (hasCasterGUID[i])
                    {
                        packet.Translator.ParseBitStream(casterGUID[i], 0, 3, 7, 1, 2, 5, 4, 6);
                        packet.Translator.WriteGuid("Caster GUID", casterGUID[i], i);
                        aura.CasterGuid = new WowGuid64(BitConverter.ToUInt64(casterGUID[i], 0));
                    }

                    aura.Duration = hasDuration[i] ? packet.Translator.ReadInt32("Duration", i) : 0;

                    for (var j = 0; j < bits48[i]; ++j)
                        packet.Translator.ReadSingle("Float3", i, j);

                    for (var j = 0; j < effectCount[i]; ++j)
                        packet.Translator.ReadSingle("Effect Value", i, j);

                    aura.Level = packet.Translator.ReadUInt16("Caster Level", i);

                    aura.MaxDuration = hasMaxDuration[i] ? packet.Translator.ReadInt32("Max Duration", i) : 0;

                    aura.Charges = packet.Translator.ReadByte("Charges", i);
                    aura.SpellId = packet.Translator.ReadUInt32("Spell Id", i);
                    aura.AuraFlags = packet.Translator.ReadByteE<AuraFlagMoP>("Flags", i);
                    packet.Translator.ReadInt32("Effect Mask", i);

                    auras.Add(aura);
                    packet.AddSniffData(StoreNameType.Spell, (int)aura.SpellId, "AURA_UPDATE");
                }

                packet.Translator.ReadByte("Slot", i);
            }

            packet.Translator.ParseBitStream(guid, 5, 1, 2, 6, 0, 7, 4, 3);

            packet.Translator.WriteGuid("Guid", guid);

            var GUID = new WowGuid64(BitConverter.ToUInt64(guid, 0));
            if (Storage.Objects.ContainsKey(GUID))
            {
                var unit = Storage.Objects[GUID].Item1 as Unit;
                if (unit != null)
                {
                    // If this is the first packet that sends auras
                    // (hopefully at spawn time) add it to the "Auras" field,
                    // if not create another row of auras in AddedAuras
                    // (similar to ChangedUpdateFields)

                    if (unit.Auras == null)
                        unit.Auras = auras;
                    else
                        unit.AddedAuras.Add(auras);
                }
            }
        }

        [Parser(Opcode.SMSG_SEND_KNOWN_SPELLS)]
        public static void HandleInitialSpells(Packet packet)
        {
            var count = packet.Translator.ReadBits("Spell Count", 22);
            packet.Translator.ReadBit("InitialLogin");

            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadUInt32<SpellId>("Spell ID", i);
            }
        }

        [Parser(Opcode.SMSG_UPDATE_TALENT_DATA)]
        public static void ReadTalentInfo(Packet packet)
        {
            var specCount = packet.Translator.ReadBits("Spec Group count", 19);

            var spentTalents = new uint[specCount];

            for (var i = 0; i < specCount; ++i)
                spentTalents[i] = packet.Translator.ReadBits("Spec Talent Count", 23, i);

            for (var i = 0; i < specCount; ++i)
            {
                for (var j = 0; j < 6; ++j)
                    packet.Translator.ReadUInt16("Glyph", i, j);

                packet.Translator.ReadUInt32("Spec Id", i);

                for (var j = 0; j < spentTalents[i]; ++j)
                    packet.Translator.ReadUInt16("Talent Id", i, j);
            }

            packet.Translator.ReadByte("Active Spec Group");
        }

        [Parser(Opcode.SMSG_LEARNED_SPELLS)]
        public static void HandleLearnedSpells(Packet packet)
        {
            var count = packet.Translator.ReadBits("Spell Count", 22);

            packet.Translator.ReadBit("SuppressMessaging");

            for (var i = 0; i < count; ++i)
                packet.Translator.ReadInt32<SpellId>("Spell ID", i);
        }

        [Parser(Opcode.SMSG_UNLEARNED_SPELLS)]
        public static void HandleRemovedSpell(Packet packet)
        {
            var count = packet.Translator.ReadBits("Spell Count", 22);

            for (var i = 0; i < count; ++i)
                packet.Translator.ReadUInt32<SpellId>("Spell ID", i);
        }

        [Parser(Opcode.CMSG_CAST_SPELL)]
        public static void HandleCastSpell(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var guid3 = new byte[8];
            var guid4 = new byte[8];
            var guid5 = new byte[8];
            var guid6 = new byte[8];

            var bit110 = false;
            var bit120 = false;
            var bit14C = false;
            var bit154 = false;
            var bit158 = false;
            var bit160 = false;
            var bit178 = false;
            var bit17C = false;
            var bit180 = false;
            var bit198 = false;

            var bits188 = 0;

            var bit1A0 = packet.Translator.ReadBit();
            packet.Translator.ReadBit(); // fake bit
            packet.Translator.ReadBit(); // fake bit
            var bit70 = packet.Translator.ReadBit();
            var hasTargetMask = !packet.Translator.ReadBit();
            var bit78 = !packet.Translator.ReadBit();
            var hasCastCount = !packet.Translator.ReadBit();
            var hasSpellId = !packet.Translator.ReadBit();

            packet.Translator.ReadBit(); // fake bit
            packet.Translator.ReadBit(); // fake bit

            var archeologyCounter = packet.Translator.ReadBits(2);
            for (var i = 0; i < archeologyCounter; ++i)
                packet.Translator.ReadBits("archeologyType", 2, i);

            var bit50 = packet.Translator.ReadBit();
            var bit18 = !packet.Translator.ReadBit();
            var bit1C = !packet.Translator.ReadBit();


            packet.Translator.StartBitStream(guid2, 7, 3, 1, 0, 5, 4, 6, 2);

            if (bit50)
                packet.Translator.StartBitStream(guid3, 7, 2, 0, 6, 4, 5, 1, 3);

            if (bit1A0)
            {
                guid5[0] = packet.Translator.ReadBit();
                bits188 = (int)packet.Translator.ReadBits(22);
                var bit10C = !packet.Translator.ReadBit();
                bit158 = packet.Translator.ReadBit();
                guid5[6] = packet.Translator.ReadBit();

                if (bit158)
                {
                    guid6[0] = packet.Translator.ReadBit();
                    guid6[3] = packet.Translator.ReadBit();
                    guid6[4] = packet.Translator.ReadBit();
                    guid6[5] = packet.Translator.ReadBit();
                    guid6[7] = packet.Translator.ReadBit();
                    guid6[6] = packet.Translator.ReadBit();
                    guid6[2] = packet.Translator.ReadBit();
                    bit154 = packet.Translator.ReadBit();
                    guid6[1] = packet.Translator.ReadBit();
                    bit14C = packet.Translator.ReadBit();
                }

                if (bit10C)
                    packet.Translator.ReadBits("bits10C", 13);

                bit17C = packet.Translator.ReadBit();

                guid5[5] = packet.Translator.ReadBit();

                if (bit17C)
                    bit178 = packet.Translator.ReadBit();

                bit120 = !packet.Translator.ReadBit();
                packet.Translator.ReadBit("bit184");

                guid5[7] = packet.Translator.ReadBit();
                guid5[1] = packet.Translator.ReadBit();
                guid5[3] = packet.Translator.ReadBit();

                bit180 = !packet.Translator.ReadBit();
                packet.Translator.ReadBit("bit185");
                var bit108 = !packet.Translator.ReadBit();
                bit160 = !packet.Translator.ReadBit();
                bit198 = !packet.Translator.ReadBit();

                guid5[2] = packet.Translator.ReadBit();
                guid5[4] = packet.Translator.ReadBit();

                packet.Translator.ReadBit("bit19C");
                bit110 = !packet.Translator.ReadBit();

                if (bit108)
                    packet.Translator.ReadBits("bits108", 30);
            }

            packet.Translator.StartBitStream(guid1, 0, 2, 7, 4, 5, 6, 1, 3);

            if (bit70)
                packet.Translator.StartBitStream(guid4, 2, 6, 1, 4, 5, 3, 7, 0);

            if (hasTargetMask)
                packet.Translator.ReadBitsE<TargetFlag>("Target Flags", 20);

            if (bit78)
                packet.Translator.ReadBits("bit78", 7);

            if (bit1C)
                packet.Translator.ReadBits("bits1C", 5);

            for (var i = 0; i < archeologyCounter; ++i)
            {
                packet.Translator.ReadUInt32("unk1", i);
                packet.Translator.ReadUInt32("unk2", i);
            }

            if (bit70)
            {
                packet.Translator.ReadXORByte(guid4, 2);
                packet.Translator.ReadXORByte(guid4, 1);
                packet.Translator.ReadXORByte(guid4, 6);
                packet.Translator.ReadSingle("Float68");
                packet.Translator.ReadXORByte(guid4, 3);
                packet.Translator.ReadSingle("Float64");
                packet.Translator.ReadXORByte(guid4, 5);
                packet.Translator.ReadSingle("Float60");
                packet.Translator.ReadXORByte(guid4, 0);
                packet.Translator.ReadXORByte(guid4, 7);
                packet.Translator.ReadXORByte(guid4, 4);
                packet.Translator.WriteGuid("Guid4", guid4);
            }

            packet.Translator.ParseBitStream(guid1, 4, 2, 1, 0, 5, 3, 6, 7);

            if (hasSpellId)
                packet.Translator.ReadInt32<SpellId>("Spell ID");

            if (bit1A0)
            {
                packet.Translator.ReadXORByte(guid5, 4);
                packet.Translator.ReadXORByte(guid5, 7);

                for (var i = 0; i < bits188; ++i)
                    packet.Translator.ReadInt32("IntEB", i);

                if (bit17C)
                {
                    packet.Translator.ReadSingle("Float168");
                    packet.Translator.ReadInt32("Int164");

                    if (bit178)
                    {
                        packet.Translator.ReadSingle("Float170");
                        packet.Translator.ReadSingle("Float174");
                        packet.Translator.ReadSingle("Float16C");
                    }
                }

                if (bit120)
                    packet.Translator.ReadSingle("Float120");

                if (bit158)
                {
                    packet.Translator.ReadSingle("Float130");
                    packet.Translator.ReadXORByte(guid6, 3);
                    packet.Translator.ReadSingle("Float13C");

                    if (bit154)
                        packet.Translator.ReadInt32("Int150");

                    packet.Translator.ReadInt32("Int144");
                    packet.Translator.ReadByte("Byte140");

                    packet.Translator.ReadXORByte(guid6, 1);
                    packet.Translator.ReadXORByte(guid6, 0);
                    packet.Translator.ReadXORByte(guid6, 6);
                    packet.Translator.ReadXORByte(guid6, 2);

                    packet.Translator.ReadSingle("Float138");
                    packet.Translator.ReadSingle("Float134");

                    packet.Translator.ReadXORByte(guid6, 4);
                    packet.Translator.ReadXORByte(guid6, 5);
                    packet.Translator.ReadXORByte(guid6, 7);

                    if (bit14C)
                        packet.Translator.ReadInt32("Int148");

                    packet.Translator.WriteGuid("Guid6", guid6);
                }

                if (bit180)
                    packet.Translator.ReadSingle("Float180");

                packet.Translator.ReadXORByte(guid5, 2);

                packet.Translator.ReadSingle("Float11C");

                packet.Translator.ReadXORByte(guid5, 1);
                packet.Translator.ReadXORByte(guid5, 3);

                packet.Translator.ReadSingle("Float114");

                if (bit198)
                    packet.Translator.ReadInt32("Int198");

                packet.Translator.ReadXORByte(guid5, 0);
                packet.Translator.ReadSingle("Float118");
                packet.Translator.ReadXORByte(guid5, 5);

                if (bit160)
                    packet.Translator.ReadSingle("Float160");

                packet.Translator.ReadXORByte(guid5, 6);

                if (bit110)
                    packet.Translator.ReadInt32("Int110");

                packet.Translator.WriteGuid("Guid5", guid5);
            }

            packet.Translator.ParseBitStream(guid2, 7, 2, 6, 3, 5, 0, 1, 4);

            if (bit50)
            {
                packet.Translator.ReadXORByte(guid3, 0);
                packet.Translator.ReadXORByte(guid3, 1);
                packet.Translator.ReadSingle("Float48");
                packet.Translator.ReadSingle("Float44");
                packet.Translator.ReadSingle("Float40");
                packet.Translator.ReadXORByte(guid3, 4);
                packet.Translator.ReadXORByte(guid3, 2);
                packet.Translator.ReadXORByte(guid3, 6);
                packet.Translator.ReadXORByte(guid3, 3);
                packet.Translator.ReadXORByte(guid3, 7);
                packet.Translator.ReadXORByte(guid3, 5);
                packet.Translator.WriteGuid("Guid3", guid3);
            }

            if (bit18)
                packet.Translator.ReadInt32("Int18");

            if (hasCastCount)
                packet.Translator.ReadByte("Cast Count");

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_SPELL_START)]
        public static void HandleSpellStart(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var guid3 = new byte[8];
            var guid4 = new byte[8];
            var guid6 = new byte[8];
            var guid7 = new byte[8];
            var guid8 = new byte[8];
            byte[][] guid9;
            byte[][] guid10;
            byte[][] guid11;

            packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.ReadByte("Byte20");
            packet.Translator.ReadInt32("Int30");
            packet.Translator.ReadInt32("Int28");

            guid1[2] = packet.Translator.ReadBit();

            var bitC0 = !packet.Translator.ReadBit();
            var bit164 = !packet.Translator.ReadBit();
            var bits44 = (int)packet.Translator.ReadBits(24);

            guid2[2] = packet.Translator.ReadBit();
            var bits184 = (int)packet.Translator.ReadBits(20);
            guid2[3] = packet.Translator.ReadBit();
            var bit17C = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();

            guid9 = new byte[bits184][];
            for (var i = 0; i < bits184; ++i)
            {
                guid9[i] = new byte[8];
                packet.Translator.StartBitStream(guid9[i], 0, 3, 2, 4, 1, 5, 7, 6);
            }

            guid2[5] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            packet.Translator.ReadBit(); // fake bit
            var bits54 = (int)packet.Translator.ReadBits(25);
            var bit194 = !packet.Translator.ReadBit();
            var bit170 = !packet.Translator.ReadBit();
            var bit180 = !packet.Translator.ReadBit();
            var hasTargetFlags = !packet.Translator.ReadBit();
            guid4[3] = packet.Translator.ReadBit();
            guid4[1] = packet.Translator.ReadBit();
            guid4[6] = packet.Translator.ReadBit();
            guid4[2] = packet.Translator.ReadBit();
            guid4[7] = packet.Translator.ReadBit();
            guid4[5] = packet.Translator.ReadBit();
            guid4[0] = packet.Translator.ReadBit();
            guid4[4] = packet.Translator.ReadBit();
            var bit1A8 = !packet.Translator.ReadBit();
            var bits34 = (int)packet.Translator.ReadBits(24);

            guid10 = new byte[bits44][];
            for (var i = 0; i < bits44; ++i)
            {
                guid10[i] = new byte[8];
                packet.Translator.StartBitStream(guid10[i], 2, 1, 5, 6, 7, 0, 4, 3);
            }

            var bits140 = (int)packet.Translator.ReadBits(21);

            var bit98 = packet.Translator.ReadBit();
            if (bit98)
                packet.Translator.StartBitStream(guid6, 1, 2, 5, 3, 4, 6, 0, 7);

            guid11 = new byte[bits34][];
            for (var i = 0; i < bits34; ++i)
            {
                guid11[i] = new byte[8];
                packet.Translator.StartBitStream(guid11[i], 7, 3, 1, 4, 0, 5, 6, 2);
            }

            var bit198 = !packet.Translator.ReadBit();
            var bit1AC = !packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            var bits154 = (int)packet.Translator.ReadBits(3);
            guid2[6] = packet.Translator.ReadBit();
            packet.Translator.ReadBit(); // fake bit
            guid3[6] = packet.Translator.ReadBit();
            guid3[1] = packet.Translator.ReadBit();
            guid3[5] = packet.Translator.ReadBit();
            guid3[0] = packet.Translator.ReadBit();
            guid3[7] = packet.Translator.ReadBit();
            guid3[3] = packet.Translator.ReadBit();
            guid3[2] = packet.Translator.ReadBit();
            guid3[4] = packet.Translator.ReadBit();
            var bit168 = !packet.Translator.ReadBit();

            var bitB8 = packet.Translator.ReadBit();
            if (bitB8)
                packet.Translator.StartBitStream(guid7, 3, 1, 4, 5, 6, 0, 7, 2);

            guid1[3] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();

            if (hasTargetFlags)
                packet.Translator.ReadBitsE<TargetFlag>("Target Flags", 20);

            for (var i = 0; i < bits54; ++i)
            {
                var bits136 = packet.Translator.ReadBits(4);

                if (bits136 == 11)
                    packet.Translator.ReadBits("bits140", 4, i);
            }

            var bit151 = !packet.Translator.ReadBit();
            var bit16C = !packet.Translator.ReadBit();
            packet.Translator.ReadBits("bits2C", 13);
            guid2[0] = packet.Translator.ReadBit();
            packet.Translator.ReadBit(); // fake bit
            guid8[7] = packet.Translator.ReadBit();
            guid8[1] = packet.Translator.ReadBit();
            guid8[0] = packet.Translator.ReadBit();
            guid8[4] = packet.Translator.ReadBit();
            guid8[3] = packet.Translator.ReadBit();
            guid8[5] = packet.Translator.ReadBit();
            guid8[6] = packet.Translator.ReadBit();
            guid8[2] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();

            var bitsC0 = 0u;
            if (bitC0)
                bitsC0 = packet.Translator.ReadBits(7);

            var bit150 = !packet.Translator.ReadBit();

            guid1[4] = packet.Translator.ReadBit();

            for (var i = 0; i < bits44; ++i)
            {
                packet.Translator.ParseBitStream(guid10[i], 0, 1, 7, 4, 3, 6, 2, 5);
                packet.Translator.WriteGuid("Guid9", guid10[i], i);
            }

            if (bitB8)
            {
                packet.Translator.ReadSingle("FloatAC");
                packet.Translator.ReadSingle("FloatB0");
                packet.Translator.ReadXORByte(guid7, 0);
                packet.Translator.ReadXORByte(guid7, 1);
                packet.Translator.ReadXORByte(guid7, 2);
                packet.Translator.ReadXORByte(guid7, 7);
                packet.Translator.ReadXORByte(guid7, 5);
                packet.Translator.ReadXORByte(guid7, 3);
                packet.Translator.ReadXORByte(guid7, 4);
                packet.Translator.ReadXORByte(guid7, 6);
                packet.Translator.ReadSingle("FloatA8");
                packet.Translator.WriteGuid("Guid7", guid7);
            }

            for (var i = 0; i < bits184; ++i)
            {
                packet.Translator.ReadXORByte(guid9[i], 4);
                packet.Translator.ReadXORByte(guid9[i], 1);
                packet.Translator.ReadXORByte(guid9[i], 3);
                packet.Translator.ReadXORByte(guid9[i], 5);
                packet.Translator.ReadSingle("Float188");
                packet.Translator.ReadSingle("Float188");
                packet.Translator.ReadXORByte(guid9[i], 0);
                packet.Translator.ReadSingle("Float188");
                packet.Translator.ReadXORByte(guid9[i], 7);
                packet.Translator.ReadXORByte(guid9[i], 2);
                packet.Translator.ReadXORByte(guid9[i], 6);
                packet.Translator.WriteGuid("Guid9", guid9[i], i);
            }

            if (bit98)
            {
                packet.Translator.ReadXORByte(guid6, 1);
                packet.Translator.ReadSingle("Float88");
                packet.Translator.ReadXORByte(guid6, 7);
                packet.Translator.ReadXORByte(guid6, 0);
                packet.Translator.ReadSingle("Float8C");
                packet.Translator.ReadXORByte(guid6, 3);
                packet.Translator.ReadXORByte(guid6, 6);
                packet.Translator.ReadSingle("Float90");
                packet.Translator.ReadXORByte(guid6, 2);
                packet.Translator.ReadXORByte(guid6, 4);
                packet.Translator.ReadXORByte(guid6, 5);
                packet.Translator.WriteGuid("Guid6", guid6);
            }

            if (bit180)
                packet.Translator.ReadByte("Byte180");
            packet.Translator.ReadXORByte(guid2, 6);

            for (var i = 0; i < bits34; ++i)
            {
                packet.Translator.ParseBitStream(guid11[i], 0, 7, 2, 3, 5, 1, 4, 6);
                packet.Translator.WriteGuid("Guid11", guid11[i], i);
            }

            packet.Translator.ReadXORByte(guid8, 0);
            packet.Translator.ReadXORByte(guid8, 1);
            packet.Translator.ReadXORByte(guid8, 3);
            packet.Translator.ReadXORByte(guid8, 7);
            packet.Translator.ReadXORByte(guid8, 5);
            packet.Translator.ReadXORByte(guid8, 2);
            packet.Translator.ReadXORByte(guid8, 4);
            packet.Translator.ReadXORByte(guid8, 6);
            packet.Translator.ReadXORByte(guid1, 4);
            if (bit1A8)
                packet.Translator.ReadInt32("Int1A8");
            for (var i = 0; i < bits140; ++i)
            {
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadByte("ByteED", i);
            }

            if (bit198)
                packet.Translator.ReadInt32("Int198");

            if (bit164)
                packet.Translator.ReadInt32("Int164");

            packet.Translator.ReadXORByte(guid4, 1);
            packet.Translator.ReadXORByte(guid4, 6);
            packet.Translator.ReadXORByte(guid4, 5);
            packet.Translator.ReadXORByte(guid4, 4);
            packet.Translator.ReadXORByte(guid4, 2);
            packet.Translator.ReadXORByte(guid4, 0);
            packet.Translator.ReadXORByte(guid4, 7);
            packet.Translator.ReadXORByte(guid4, 3);
            packet.Translator.ReadXORByte(guid3, 0);
            packet.Translator.ReadXORByte(guid3, 5);
            packet.Translator.ReadXORByte(guid3, 4);
            packet.Translator.ReadXORByte(guid3, 2);
            packet.Translator.ReadXORByte(guid3, 7);
            packet.Translator.ReadXORByte(guid3, 6);
            packet.Translator.ReadXORByte(guid3, 1);
            packet.Translator.ReadXORByte(guid3, 3);
            if (bit194)
                packet.Translator.ReadInt32("Int194");
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadWoWString("StringC0", bitsC0);
            packet.Translator.ReadXORByte(guid2, 7);
            if (bit1AC)
                packet.Translator.ReadByte("Byte1AC");
            if (bit170)
                packet.Translator.ReadByte("Byte170");
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid2, 3);
            if (bit17C)
            {
                packet.Translator.ReadInt32("Int174");
                packet.Translator.ReadInt32("Int178");
            }

            if (bit151)
                packet.Translator.ReadByte("Byte151");
            for (var i = 0; i < bits154; ++i)
                packet.Translator.ReadByte("Byte158", i);

            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 5);

            if (bit16C)
                packet.Translator.ReadInt32("Int16C");

            packet.Translator.ReadXORByte(guid1, 0);

            if (bit168)
                packet.Translator.ReadSingle("Float168");

            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid1, 3);

            if (bit150)
                packet.Translator.ReadByte("Byte150");

            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid1, 7);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
            packet.Translator.WriteGuid("Guid3", guid3);
            packet.Translator.WriteGuid("Guid4", guid4);
            packet.Translator.WriteGuid("Guid8", guid8);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellGo(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var guid3 = new byte[8];
            var guid4 = new byte[8];
            var guid5 = new byte[8];
            var guid6 = new byte[8];
            var guid7 = new byte[8];
            byte[][] guid8;
            byte[][] guid9;
            byte[][] hitGuid;

            var bits4C = (int)packet.Translator.ReadBits(13);
            var bit1A0 = !packet.Translator.ReadBit();
            var bits74 = (int)packet.Translator.ReadBits(25);
            var bit1B4 = !packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            var bits174 = (int)packet.Translator.ReadBits(3);
            var bits64 = (int)packet.Translator.ReadBits(24);

            guid8 = new byte[bits64][];
            for (var i = 0; i < bits64; ++i)
            {
                guid8[i] = new byte[8];
                packet.Translator.StartBitStream(guid8[i], 2, 6, 3, 4, 5, 1, 7, 0);
            }

            var bit1B8 = !packet.Translator.ReadBit();
            var bit171 = !packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            var bits1A4 = (int)packet.Translator.ReadBits(20);
            guid2[3] = packet.Translator.ReadBit();
            packet.Translator.ReadBit(); // fake bit
            var hasTargetFlags = !packet.Translator.ReadBit();
            guid7[2] = packet.Translator.ReadBit();
            guid7[5] = packet.Translator.ReadBit();
            guid7[1] = packet.Translator.ReadBit();
            guid7[6] = packet.Translator.ReadBit();
            guid7[4] = packet.Translator.ReadBit();
            guid7[3] = packet.Translator.ReadBit();
            guid7[7] = packet.Translator.ReadBit();
            guid7[0] = packet.Translator.ReadBit();
            var bit1CC = !packet.Translator.ReadBit();
            guid9 = new byte[bits1A4][];
            for (var i = 0; i < bits1A4; ++i)
            {
                guid9[i] = new byte[8];
                packet.Translator.StartBitStream(guid9[i], 0, 4, 1, 6, 5, 3, 2, 7);
            }

            packet.Translator.ReadBit(); // fake bit
            guid4[6] = packet.Translator.ReadBit();
            guid4[4] = packet.Translator.ReadBit();
            guid4[1] = packet.Translator.ReadBit();
            guid4[3] = packet.Translator.ReadBit();
            guid4[2] = packet.Translator.ReadBit();
            guid4[7] = packet.Translator.ReadBit();
            guid4[0] = packet.Translator.ReadBit();
            guid4[5] = packet.Translator.ReadBit();

            if (hasTargetFlags)
                packet.Translator.ReadBitsE<TargetFlag>("Target Flags", 20);

            var bit19C = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            var bit184 = !packet.Translator.ReadBit();
            var hasSrcLocation = packet.Translator.ReadBit();
            packet.Translator.ReadBit(); // fake bit
            guid3[1] = packet.Translator.ReadBit();
            guid3[3] = packet.Translator.ReadBit();
            guid3[0] = packet.Translator.ReadBit();
            guid3[7] = packet.Translator.ReadBit();
            guid3[4] = packet.Translator.ReadBit();
            guid3[2] = packet.Translator.ReadBit();
            guid3[5] = packet.Translator.ReadBit();
            guid3[6] = packet.Translator.ReadBit();
            var bitE0 = !packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            var bit190 = !packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            var bit188 = !packet.Translator.ReadBit();
            var bit18C = !packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            if (hasSrcLocation)
            {
                guid5[7] = packet.Translator.ReadBit();
                guid5[2] = packet.Translator.ReadBit();
                guid5[5] = packet.Translator.ReadBit();
                guid5[0] = packet.Translator.ReadBit();
                guid5[4] = packet.Translator.ReadBit();
                guid5[6] = packet.Translator.ReadBit();
                guid5[1] = packet.Translator.ReadBit();
                guid5[3] = packet.Translator.ReadBit();
            }

            guid1[5] = packet.Translator.ReadBit();

            var bits0 = 0u;
            if (bitE0)
                bits0 = packet.Translator.ReadBits(7);

            var hitCount = (int)packet.Translator.ReadBits(24);
            var hasDestLocation = packet.Translator.ReadBit();

            hitGuid = new byte[hitCount][];
            for (var i = 0; i < hitCount; ++i)
            {
                hitGuid[i] = new byte[8];
                packet.Translator.StartBitStream(hitGuid[i], 4, 3, 5, 6, 7, 0, 2, 1);
            }

            if (hasDestLocation)
            {
                guid6[7] = packet.Translator.ReadBit();
                guid6[0] = packet.Translator.ReadBit();
                guid6[2] = packet.Translator.ReadBit();
                guid6[1] = packet.Translator.ReadBit();
                guid6[6] = packet.Translator.ReadBit();
                guid6[5] = packet.Translator.ReadBit();
                guid6[3] = packet.Translator.ReadBit();
                guid6[4] = packet.Translator.ReadBit();
            }

            guid2[0] = packet.Translator.ReadBit();
            var bits160 = (int)packet.Translator.ReadBits(21);
            guid2[1] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            var bit170 = !packet.Translator.ReadBit();
            var bit2C = packet.Translator.ReadBit();
            var bit1C8 = !packet.Translator.ReadBit();

            for (var i = 0; i < bits74; ++i)
            {
                if (packet.Translator.ReadBits("bits22[0]", 4, i) == 11)
                    packet.Translator.ReadBits("bits22[1]", 4, i);
            }

            guid2[5] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            if (bit2C)
            {
                var bits1C = (int)packet.Translator.ReadBits("bits1C", 21);
                if (bit2C)
                {
                    for (var i = 0; i < bits1C; ++i)
                    {
                        packet.Translator.ReadInt32("IntED", i);
                        packet.Translator.ReadInt32("IntED", i);
                    }

                    packet.Translator.ReadInt32("Int18");
                    packet.Translator.ReadInt32("Int14");
                    packet.Translator.ReadInt32("Int10");
                }
            }

            if (hasDestLocation)
            {
                Vector3 pos = new Vector3();
                packet.Translator.ReadXORByte(guid6, 0);
                pos.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(guid6, 1);
                pos.Y = packet.Translator.ReadSingle();
                pos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(guid6, 7);
                packet.Translator.ReadXORByte(guid6, 4);
                packet.Translator.ReadXORByte(guid6, 3);
                packet.Translator.ReadXORByte(guid6, 2);
                packet.Translator.ReadXORByte(guid6, 5);
                packet.Translator.ReadXORByte(guid6, 6);
                packet.Translator.WriteGuid("Destination Transport GUID", guid6);
                packet.AddValue("Destination Position", pos);
            }

            if (hasSrcLocation)
            {
                Vector3 pos = new Vector3();
                packet.Translator.ReadXORByte(guid5, 6);
                pos.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(guid5, 4);
                packet.Translator.ReadXORByte(guid5, 5);
                packet.Translator.ReadXORByte(guid5, 1);
                pos.X = packet.Translator.ReadSingle();
                pos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(guid5, 7);
                packet.Translator.ReadXORByte(guid5, 2);
                packet.Translator.ReadXORByte(guid5, 0);
                packet.Translator.ReadXORByte(guid5, 3);
                packet.Translator.WriteGuid("Source Transport GUID", guid5);
                packet.AddValue("Source Position", pos);
            }

            for (var i = 0; i < bits64; ++i)
            {
                packet.Translator.ParseBitStream(guid8[i], 7, 4, 1, 3, 5, 6, 2, 0);
                packet.Translator.WriteGuid("Guid8", guid8[i], i);
            }

            if (bit184)
                packet.Translator.ReadInt32("Int184");

            for (var i = 0; i < hitCount; ++i)
            {
                packet.Translator.ParseBitStream(hitGuid[i], 7, 1, 6, 2, 4, 3, 5, 0);
                packet.Translator.WriteGuid("Hit GUID", hitGuid[i], i);
            }

            packet.Translator.ReadXORByte(guid2, 4);
            for (var i = 0; i < bits1A4; ++i)
            {
                packet.Translator.ReadXORByte(guid9[i], 7);
                packet.Translator.ReadSingle("Float1A8", i);
                packet.Translator.ReadSingle("Float1A8", i);
                packet.Translator.ReadXORByte(guid9[i], 2);
                packet.Translator.ReadXORByte(guid9[i], 0);
                packet.Translator.ReadXORByte(guid9[i], 6);
                packet.Translator.ReadSingle("Float1A8", i);
                packet.Translator.ReadXORByte(guid9[i], 1);
                packet.Translator.ReadXORByte(guid9[i], 3);
                packet.Translator.ReadXORByte(guid9[i], 4);
                packet.Translator.ReadXORByte(guid9[i], 5);

                packet.Translator.WriteGuid("Guid6", guid9[i], i);
            }

            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid7, 5);
            packet.Translator.ReadXORByte(guid7, 2);
            packet.Translator.ReadXORByte(guid7, 7);
            packet.Translator.ReadXORByte(guid7, 4);
            packet.Translator.ReadXORByte(guid7, 1);
            packet.Translator.ReadXORByte(guid7, 6);
            packet.Translator.ReadXORByte(guid7, 3);
            packet.Translator.ReadXORByte(guid7, 0);
            packet.Translator.ReadXORByte(guid3, 0);
            packet.Translator.ReadXORByte(guid3, 4);
            packet.Translator.ReadXORByte(guid3, 5);
            packet.Translator.ReadXORByte(guid3, 7);
            packet.Translator.ReadXORByte(guid3, 6);
            packet.Translator.ReadXORByte(guid3, 3);
            packet.Translator.ReadXORByte(guid3, 2);
            packet.Translator.ReadXORByte(guid3, 1);

            if (bit190)
                packet.Translator.ReadByte("Byte190");

            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 3);
            for (var i = 0; i < bits160; ++i)
            {
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadByte("ByteED", i);
            }

            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid4, 6);
            packet.Translator.ReadXORByte(guid4, 5);
            packet.Translator.ReadXORByte(guid4, 7);
            packet.Translator.ReadXORByte(guid4, 3);
            packet.Translator.ReadXORByte(guid4, 1);
            packet.Translator.ReadXORByte(guid4, 0);
            packet.Translator.ReadXORByte(guid4, 4);
            packet.Translator.ReadXORByte(guid4, 2);
            if (bit19C)
            {
                packet.Translator.ReadInt32("Int198");
                packet.Translator.ReadInt32("Int194");
            }

            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadByte("Byte40");
            packet.Translator.ReadWoWString("StringE0", bits0);
            packet.Translator.ReadInt32("Time");
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid1, 6);
            if (bit1B4)
                packet.Translator.ReadInt32("Int1B4");
            if (bit1B8)
                packet.Translator.ReadInt32("Int1B8");
            if (bit1C8)
                packet.Translator.ReadInt32("Int1C8");
            if (bit171)
                packet.Translator.ReadByte("Byte171");
            if (bit1A0)
                packet.Translator.ReadByte("Byte1A0");
            if (bit1CC)
                packet.Translator.ReadByte("Byte1CC");
            packet.Translator.ReadXORByte(guid1, 5);
            for (var i = 0; i < bits174; ++i)
                packet.Translator.ReadByte("Byte178", i);

            if (bit188)
                packet.Translator.ReadSingle("Float188");

            packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.ReadInt32("Int48");

            if (bit18C)
                packet.Translator.ReadInt32("Int18C");

            packet.Translator.ReadXORByte(guid1, 3);

            if (bit170)
                packet.Translator.ReadByte("Byte170");

            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid2, 5);

            packet.Translator.WriteGuid("Caster GUID", guid1);
            packet.Translator.WriteGuid("Caster Unit GUID", guid2);
            packet.Translator.WriteGuid("Target GUID", guid3);
            packet.Translator.WriteGuid("Guid4", guid4);
            packet.Translator.WriteGuid("Guid7", guid7);
        }

        [Parser(Opcode.SMSG_SET_FLAT_SPELL_MODIFIER)]
        public static void HandleSetSpellModifierFlat(Packet packet)
        {
            var modCount = packet.Translator.ReadBits("Modifier type count", 22);
            var modTypeCount = new uint[modCount];

            for (var j = 0; j < modCount; ++j)
                modTypeCount[j] = packet.Translator.ReadBits("Count", 21, j);

            for (var j = 0; j < modCount; ++j)
            {
                packet.Translator.ReadByteE<SpellModOp>("Spell Mod", j);

                for (var i = 0; i < modTypeCount[j]; ++i)
                {
                    packet.Translator.ReadByte("Spell Mask bitpos", j, i);
                    packet.Translator.ReadSingle("Amount", j, i);
                }
            }
        }

        [Parser(Opcode.SMSG_SET_PCT_SPELL_MODIFIER)]
        public static void HandleSetSpellModifierPct(Packet packet)
        {
            var modCount = packet.Translator.ReadBits("Modifier type count", 22);
            var modTypeCount = new uint[modCount];

            for (var j = 0; j < modCount; ++j)
                modTypeCount[j] = packet.Translator.ReadBits("Count", 21, j);

            for (var j = 0; j < modCount; ++j)
            {
                packet.Translator.ReadByteE<SpellModOp>("Spell Mod", j);
                for (var i = 0; i < modTypeCount[j]; ++i)
                {
                    packet.Translator.ReadSingle("Amount", j, i);
                    packet.Translator.ReadByte("Spell Mask bitpos", j, i);
                }
            }
        }

        [Parser(Opcode.SMSG_CATEGORY_COOLDOWN)]
        public static void HandleSpellCategoryCooldown(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 21);

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("Category Cooldown", i);
                packet.Translator.ReadInt32("Cooldown", i);
            }
        }

        [Parser(Opcode.SMSG_SEND_UNLEARN_SPELLS)]
        public static void HandleSendUnlearnSpells(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 21);
            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadByte("Unk Byte", i);
                packet.Translator.ReadInt32("Unk Int32", i);
                packet.Translator.ReadInt32<SpellId>("Spell ID", i);
            }
        }

        [Parser(Opcode.SMSG_SPELL_CHANNEL_START)]
        public static void HandleSpellChannelStart(Packet packet)
        {
            var targetGUD = new byte[8];
            var guid2 = new byte[8];
            var casterGUID = new byte[8];

            var bit20 = false;
            var bit24 = false;

            casterGUID[3] = packet.Translator.ReadBit();
            casterGUID[2] = packet.Translator.ReadBit();
            casterGUID[5] = packet.Translator.ReadBit();
            casterGUID[0] = packet.Translator.ReadBit();
            casterGUID[4] = packet.Translator.ReadBit();

            var bit28 = packet.Translator.ReadBit();
            if (bit28)
            {
                targetGUD[0] = packet.Translator.ReadBit();
                targetGUD[3] = packet.Translator.ReadBit();
                targetGUD[5] = packet.Translator.ReadBit();

                bit24 = !packet.Translator.ReadBit();

                targetGUD[6] = packet.Translator.ReadBit();
                targetGUD[4] = packet.Translator.ReadBit();

                bit20 = !packet.Translator.ReadBit();
                packet.Translator.ReadBit(); // fake bit

                packet.Translator.StartBitStream(guid2, 2, 5, 0, 6, 3, 7, 4, 1);

                targetGUD[2] = packet.Translator.ReadBit();
                targetGUD[7] = packet.Translator.ReadBit();
                targetGUD[1] = packet.Translator.ReadBit();
            }

            casterGUID[6] = packet.Translator.ReadBit();
            casterGUID[7] = packet.Translator.ReadBit();
            casterGUID[1] = packet.Translator.ReadBit();

            var bit48 = packet.Translator.ReadBit();
            if (bit28)
            {
                packet.Translator.ParseBitStream(guid2, 4, 7, 6, 0, 1, 3, 5, 2);

                packet.Translator.ReadXORByte(targetGUD, 0);

                if (bit20)
                    packet.Translator.ReadInt32("Heal Amount");

                packet.Translator.ReadXORByte(targetGUD, 4);
                packet.Translator.ReadXORByte(targetGUD, 5);
                packet.Translator.ReadXORByte(targetGUD, 3);
                packet.Translator.ReadXORByte(targetGUD, 7);
                packet.Translator.ReadXORByte(targetGUD, 2);
                packet.Translator.ReadXORByte(targetGUD, 1);

                if (bit24)
                    packet.Translator.ReadByte("Type");

                packet.Translator.ReadXORByte(targetGUD, 6);

                packet.Translator.WriteGuid("TargetGUD", targetGUD);
                packet.Translator.WriteGuid("Guid2", guid2);
            }

            packet.Translator.ReadXORByte(casterGUID, 2);
            packet.Translator.ReadXORByte(casterGUID, 4);
            packet.Translator.ReadXORByte(casterGUID, 7);

            if (bit48)
            {
                packet.Translator.ReadInt32("Int44");
                packet.Translator.ReadInt32("Int40");
            }

            packet.Translator.ReadXORByte(casterGUID, 1);
            packet.Translator.ReadXORByte(casterGUID, 5);
            packet.Translator.ReadXORByte(casterGUID, 3);

            packet.Translator.ReadUInt32<SpellId>("Spell ID");

            packet.Translator.ReadXORByte(casterGUID, 0);
            packet.Translator.ReadXORByte(casterGUID, 6);

            packet.Translator.ReadInt32("Duration");

            packet.Translator.WriteGuid("CasterGUID", casterGUID);
        }

        [Parser(Opcode.SMSG_SPELL_CHANNEL_UPDATE)]
        public static void HandleSpellChannelUpdate(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 2, 7, 0, 4, 1, 3, 5, 6);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadInt32("Timestamp");
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);

            packet.Translator.WriteGuid("guid", guid);
        }

        [Parser(Opcode.SMSG_CAST_FAILED)]
        public static void HandleCastFailed(Packet packet)
        {
            var bit18 = !packet.Translator.ReadBit();
            var bit14 = !packet.Translator.ReadBit();

            var result = packet.Translator.ReadInt32E<SpellCastFailureReason>("Reason");

            if (bit18)
                packet.Translator.ReadInt32("Int18");

            if (bit14)
                packet.Translator.ReadInt32("Int14");

            packet.Translator.ReadByte("Cast count");
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
        }

        [Parser(Opcode.SMSG_PET_CAST_FAILED)]
        public static void HandlePetCastFailed(Packet packet)
        {
            var bit10 = !packet.Translator.ReadBit();
            var bit1C = !packet.Translator.ReadBit();

            var result = packet.Translator.ReadInt32E<SpellCastFailureReason>("Reason");

            if (bit1C)
                packet.Translator.ReadInt32("Int1C");

            packet.Translator.ReadUInt32<SpellId>("Spell ID");

            if (bit10)
                packet.Translator.ReadInt32("Int10");

            packet.Translator.ReadByte("Cast count");
        }
    }
}

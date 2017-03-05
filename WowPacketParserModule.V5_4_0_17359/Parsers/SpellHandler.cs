using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class SpellHandler
    {
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

        [Parser(Opcode.SMSG_LEARNED_SPELLS)]
        public static void HandleLearnedSpells(Packet packet)
        {
            packet.Translator.ReadBit("SuppressMessaging");

            var count = packet.Translator.ReadBits("Spell Count", 22);

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

        [Parser(Opcode.SMSG_WEEKLY_SPELL_USAGE)]
        public static void HandleWeeklySpellUsage(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 21);

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadByte("Unk Int8");
                packet.Translator.ReadInt32("Unk Int32");
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

        [HasSniffData]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            var guid = new byte[8];
            var powerGUID = new byte[8];

            packet.Translator.ReadBit(); // fake bit?

            packet.Translator.StartBitStream(guid, 6, 1, 0);

            var bits4 = (int)packet.Translator.ReadBits(24);

            packet.Translator.StartBitStream(guid, 2, 4);

            var hasPowerData = packet.Translator.ReadBit();

            var powerCount = 0u;
            if (hasPowerData)
            {
                packet.Translator.StartBitStream(powerGUID, 7, 0, 6);
                powerCount = packet.Translator.ReadBits(21);
                packet.Translator.StartBitStream(powerGUID, 3, 1, 2, 4, 5);
            }

            packet.Translator.StartBitStream(guid, 7, 3, 5);

            var hasAura = new bool[bits4];
            var hasCasterGUID = new bool[bits4];
            var hasDuration = new bool[bits4];
            var hasMaxDuration = new bool[bits4];
            var effectCount = new uint[bits4];
            var casterGUID = new byte[bits4][];
            var bitsEC = new uint[bits4];

            for (var i = 0; i < bits4; ++i)
            {
                hasAura[i] = packet.Translator.ReadBit();

                if (hasAura[i])
                {
                    hasMaxDuration[i] = packet.Translator.ReadBit();
                    effectCount[i] = packet.Translator.ReadBits(22);
                    hasCasterGUID[i] = packet.Translator.ReadBit();
                    if (hasCasterGUID[i])
                    {
                        casterGUID[i] = new byte[8];
                        packet.Translator.StartBitStream(casterGUID[i], 3, 0, 2, 6, 5, 7, 4, 1);
                    }

                    hasDuration[i] = packet.Translator.ReadBit();
                    bitsEC[i] = packet.Translator.ReadBits(22);
                }
            }

            var auras = new List<Aura>();
            for (var i = 0; i < bits4; ++i)
            {
                if (hasAura[i])
                {
                    var aura = new Aura();

                    aura.Duration = hasDuration[i] ? packet.Translator.ReadInt32("Duration", i) : 0;

                    if (hasCasterGUID[i])
                    {
                        packet.Translator.ParseBitStream(casterGUID[i], 0, 7, 5, 6, 1, 3, 2, 4);
                        packet.Translator.WriteGuid("Caster GUID", casterGUID[i], i);
                        aura.CasterGuid = new WowGuid64(BitConverter.ToUInt64(casterGUID[i], 0));
                    }
                    else
                        aura.CasterGuid = new WowGuid64();

                    aura.AuraFlags = packet.Translator.ReadByteE<AuraFlagMoP>("Flags", i);

                    for (var j = 0; j < effectCount[i]; ++j)
                        packet.Translator.ReadSingle("Effect Value", i, j);

                    aura.SpellId = packet.Translator.ReadUInt32("Spell Id", i);

                    aura.MaxDuration = hasMaxDuration[i] ? packet.Translator.ReadInt32("Max Duration", i) : 0;

                    for (var j = 0; j < bitsEC[i]; ++j)
                        packet.Translator.ReadSingle("FloatEA");

                    aura.Charges = packet.Translator.ReadByte("Charges", i);
                    packet.Translator.ReadInt32("Effect Mask", i);
                    aura.Level = packet.Translator.ReadUInt16("Caster Level", i);
                    auras.Add(aura);
                    packet.AddSniffData(StoreNameType.Spell, (int)aura.SpellId, "AURA_UPDATE");
                }

                packet.Translator.ReadByte("Slot", i);
            }

            if (hasPowerData)
            {
                packet.Translator.ReadXORByte(powerGUID, 7);
                packet.Translator.ReadXORByte(powerGUID, 4);
                packet.Translator.ReadXORByte(powerGUID, 5);
                packet.Translator.ReadXORByte(powerGUID, 1);
                packet.Translator.ReadXORByte(powerGUID, 6);
                for (var i = 0; i < powerCount; ++i)
                {
                    packet.Translator.ReadInt32("Value", i);
                    packet.Translator.ReadInt32E<PowerType>("Power type", i); // Actually powertype for class
                }

                packet.Translator.ReadInt32("Attack power");
                packet.Translator.ReadInt32("Spell power");
                packet.Translator.ReadXORByte(powerGUID, 3);
                packet.Translator.ReadInt32("Current health");
                packet.Translator.ReadXORByte(powerGUID, 0);
                packet.Translator.ReadXORByte(powerGUID, 2);
                packet.Translator.WriteGuid("Power GUID", powerGUID);
            }

            packet.Translator.ParseBitStream(guid, 0, 4, 3, 7, 5, 6, 2, 1);

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

        [Parser(Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellGo(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var guid3 = new byte[8];
            var guid4 = new byte[8];
            var guid5 = new byte[8];
            byte[][] guid6;
            byte[][] guid7;
            var guid8 = new byte[8];
            var powerGUID = new byte[8];
            byte[][] guid10;
            var guid11 = new byte[8];

            var bitE8 = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 6, 7);

            packet.Translator.ReadBit(); // fake bit

            packet.Translator.StartBitStream(guid2, 2, 6, 0, 3, 4, 1, 7, 5);

            guid3[4] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            guid3[2] = packet.Translator.ReadBit();
            var counter1 = (int)packet.Translator.ReadBits(24);
            guid1[0] = packet.Translator.ReadBit();
            var counter2 = (int)packet.Translator.ReadBits(24);
            guid3[5] = packet.Translator.ReadBit();

            if (bitE8)
                packet.Translator.StartBitStream(guid4, 4, 7, 5, 3, 6, 2, 1, 0);

            packet.Translator.ReadBit(); // fake bit

            packet.Translator.StartBitStream(guid5, 5, 3, 4, 6, 7, 1, 2, 0);

            var bitF0 = !packet.Translator.ReadBit();
            var bit19C = !packet.Translator.ReadBit();
            var bit180 = !packet.Translator.ReadBit();
            var bit1D8 = !packet.Translator.ReadBit();

            guid3[7] = packet.Translator.ReadBit();

            guid6 = new byte[counter2][];
            for (var i = 0; i < counter2; ++i)
            {
                guid6[i] = new byte[8];
                packet.Translator.StartBitStream(guid6[i], 5, 3, 1, 4, 6, 0, 2, 7);
            }

            guid7 = new byte[counter1][];
            for (var i = 0; i < counter1; ++i)
            {
                guid7[i] = new byte[8];
                packet.Translator.StartBitStream(guid7[i], 0, 2, 7, 4, 6, 3, 5, 1);
            }

            var hasTargetMask = !packet.Translator.ReadBit();
            var bit1C8 = !packet.Translator.ReadBit();

            var counter3 = packet.Translator.ReadBits(21);

            var hasPowerData = packet.Translator.ReadBit();
            var bitC8 = packet.Translator.ReadBit();
            var bit1A0 = !packet.Translator.ReadBit();

            if (bitC8)
                packet.Translator.StartBitStream(guid8, 4, 7, 6, 3, 2, 0, 5, 1);

            var bit181 = !packet.Translator.ReadBit();
            var bit1DC = !packet.Translator.ReadBit();

            guid1[4] = packet.Translator.ReadBit();

            var powerCount = 0u;
            if (hasPowerData)
            {
                powerGUID[4] = packet.Translator.ReadBit();
                powerCount = packet.Translator.ReadBits(21);
                packet.Translator.StartBitStream(powerGUID, 2, 3, 7, 6, 5, 0, 1);
            }

            var bit198 = !packet.Translator.ReadBit();
            var bit194 = !packet.Translator.ReadBit();

            if (hasTargetMask)
                packet.Translator.ReadBitsE<TargetFlag>("Target Flags", 20);

            var bits5C = packet.Translator.ReadBits(13);
            guid1[3] = packet.Translator.ReadBit();
            var bits184 = packet.Translator.ReadBits(3);

            packet.Translator.ReadBit(); // fake bit

            guid1[2] = packet.Translator.ReadBit();
            var bit1B0 = !packet.Translator.ReadBit();
            guid3[3] = packet.Translator.ReadBit();
            var counter4 = (int)packet.Translator.ReadBits(20);

            guid10 = new byte[counter4][];
            for (var i = 0; i < counter4; ++i)
            {
                guid10[i] = new byte[8];
                packet.Translator.StartBitStream(guid10[i], 5, 1, 4, 7, 3, 6, 0, 2);
            }

            packet.Translator.StartBitStream(guid11, 4, 1, 5, 2, 7, 6, 0, 3);

            guid3[1] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid3[6] = packet.Translator.ReadBit();

            var counter5 = packet.Translator.ReadBits(25);

            guid3[0] = packet.Translator.ReadBit();

            var bitsF0 = 0u;
            if (bitF0)
                bitsF0 = packet.Translator.ReadBits(7);

            var bit1C4 = !packet.Translator.ReadBit();
            var bit1AC = packet.Translator.ReadBit();

            for (var i = 0; i < counter5; ++i)
            {
                var bits136 = packet.Translator.ReadBits(4);

                if (bits136 == 11)
                    packet.Translator.ReadBits("bits140", 4, i);
            }

            packet.Translator.ParseBitStream(guid11, 5, 3, 4, 2, 0, 1, 7, 6);

            packet.Translator.ReadXORByte(guid1, 2);

            packet.Translator.ReadInt32("Int60");

            for (var i = 0; i < counter1; ++i)
            {
                packet.Translator.ParseBitStream(guid7[i], 3, 6, 7, 5, 0, 4, 2, 1);
                packet.Translator.WriteGuid("Guid7", guid7[i], i);
            }

            packet.Translator.ReadXORByte(guid1, 6);

            for (var i = 0; i < counter2; ++i)
            {
                packet.Translator.ParseBitStream(guid6[i], 6, 0, 3, 7, 2, 1, 5, 4);
                packet.Translator.WriteGuid("Guid7", guid7[i], i);
            }

            packet.Translator.ReadXORByte(guid3, 0);

            if (bitC8)
            {
                packet.Translator.ReadXORByte(guid8, 6);
                packet.Translator.ReadXORByte(guid8, 7);
                packet.Translator.ReadXORByte(guid8, 3);
                packet.Translator.ReadXORByte(guid8, 0);

                packet.Translator.ReadSingle("FloatBC");

                packet.Translator.ReadXORByte(guid8, 1);

                packet.Translator.ReadSingle("FloatC0");

                packet.Translator.ReadXORByte(guid8, 4);
                packet.Translator.ReadXORByte(guid8, 2);

                packet.Translator.ReadSingle("FloatB8");

                packet.Translator.ReadXORByte(guid8, 5);

                packet.Translator.WriteGuid("guidG", guid8);
            }

            if (bit19C)
                packet.Translator.ReadInt32("Int19C");

            if (bitE8)
            {
                var pos = new Vector3();

                packet.Translator.ReadXORByte(guid4, 5);
                packet.Translator.ReadXORByte(guid4, 1);
                packet.Translator.ReadXORByte(guid4, 6);
                packet.Translator.ReadXORByte(guid4, 0);
                packet.Translator.ReadXORByte(guid4, 3);
                packet.Translator.ReadXORByte(guid4, 4);

                pos.X = packet.Translator.ReadSingle("FloatD8");

                packet.Translator.ReadXORByte(guid4, 2);

                pos.Y = packet.Translator.ReadSingle("FloatDC");
                pos.Z = packet.Translator.ReadSingle("FloatE0");

                packet.Translator.ReadXORByte(guid4, 7);

                packet.Translator.WriteGuid("Guid4", guid4);
                packet.AddValue("Position", pos);
            }

            if (bit1B0)
                packet.Translator.ReadByte("Byte1B0");

            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 7);

            packet.Translator.ParseBitStream(guid5, 1, 7, 3, 0, 6, 2, 4, 5);

            if (bit1AC)
            {
                packet.Translator.ReadInt32("Int1A8");
                packet.Translator.ReadInt32("Int1A4");
            }

            packet.Translator.ReadXORByte(guid1, 4);

            for (var i = 0; i < counter4; ++i)
            {
                packet.Translator.ReadXORByte(guid10[i], 4);
                packet.Translator.ReadXORByte(guid10[i], 5);

                packet.Translator.ReadSingle("Float1B8", i);

                packet.Translator.ReadXORByte(guid10[i], 0);
                packet.Translator.ReadXORByte(guid10[i], 1);
                packet.Translator.ReadXORByte(guid10[i], 2);
                packet.Translator.ReadXORByte(guid10[i], 3);

                packet.Translator.ReadSingle("Float1B8", i);
                packet.Translator.ReadSingle("Float1B8", i);

                packet.Translator.ReadXORByte(guid10[i], 6);
                packet.Translator.ReadXORByte(guid10[i], 7);

                packet.Translator.WriteGuid("Guid10", guid10[i], i);
            }

            if (hasPowerData)
            {
                packet.Translator.ReadInt32("Spell power");
                packet.Translator.ReadInt32("Attack power");

                for (var i = 0; i < powerCount; ++i)
                {
                    packet.Translator.ReadUInt32E<PowerType>("Power Type", i);
                    packet.Translator.ReadInt32("Power Value", i);
                }

                packet.Translator.ParseBitStream(powerGUID, 4, 7, 0, 2, 3, 5, 6, 1);

                packet.Translator.ReadInt32("Current Health");
                packet.Translator.WriteGuid("Power GUID", powerGUID);
            }

            packet.Translator.ReadXORByte(guid3, 6);
            packet.Translator.ReadXORByte(guid1, 3);

            if (bit180)
                packet.Translator.ReadByte("Byte180");

            packet.Translator.ParseBitStream(guid2, 3, 2, 1, 4, 6, 0, 7, 5);
            packet.Translator.ReadXORByte(guid3, 3);

            for (var i = 0; i < bits184; ++i)
                packet.Translator.ReadByte("Byte188", i);

            packet.Translator.ReadWoWString("StringF0", bitsF0);
            packet.Translator.ReadByte("Cast Count");

            if (bit1C4)
                packet.Translator.ReadInt32("Int1C4");

            packet.Translator.ReadXORByte(guid3, 7);

            if (bit1D8)
                packet.Translator.ReadInt32("Int1D8");

            if (bit198)
                packet.Translator.ReadSingle("Float198");

            packet.Translator.ReadInt32E<CastFlag>("Cast Flags");

            packet.Translator.ReadXORByte(guid3, 4);
            packet.Translator.ReadXORByte(guid3, 1);

            packet.Translator.ReadInt32<SpellId>("Spell ID");

            if (bit1DC)
                packet.Translator.ReadByte("Byte1DC");

            for (var i = 0; i < counter3; ++i)
            {
                packet.Translator.ReadByte("ByteED", i);
                packet.Translator.ReadInt32("IntED", i);
            }

            packet.Translator.ReadXORByte(guid1, 0);

            if (bit1A0)
                packet.Translator.ReadByte("Byte1A0");

            if (bit181)
                packet.Translator.ReadByte("Byte181");

            if (bit194)
                packet.Translator.ReadInt32("Int194");

            packet.Translator.ReadXORByte(guid1, 1);

            if (bit1C8)
                packet.Translator.ReadInt32("Int1C8");

            packet.Translator.ReadXORByte(guid3, 5);
            packet.Translator.ReadXORByte(guid3, 2);

            packet.Translator.WriteGuid("Guid8", guid1);
            packet.Translator.WriteGuid("Guid9", guid3);
            packet.Translator.WriteGuid("Guid2", guid2);
            packet.Translator.WriteGuid("Guid11", guid11);
            packet.Translator.WriteGuid("Guid5", guid5);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_SPELL_START)]
        public static void HandleSpellStart(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var guid3 = new byte[8];
            var guid4 = new byte[8];
            var guid5 = new byte[8];
            byte[][] guid6;
            byte[][] guid7;
            var guid8 = new byte[8];
            var guid9 = new byte[8];
            var powerGUID = new byte[8];
            byte[][] guid11;

            packet.Translator.ReadBit(); // fake bit

            guid1[4] = packet.Translator.ReadBit();

            var bit198 = !packet.Translator.ReadBit();
            var bit170 = !packet.Translator.ReadBit();
            var bit168 = !packet.Translator.ReadBit();

            packet.Translator.ReadBit(); // fake bit

            guid2[7] = packet.Translator.ReadBit();

            packet.Translator.StartBitStream(guid3, 2, 5, 6, 1, 0, 3, 7, 4);

            guid1[1] = packet.Translator.ReadBit();

            var bit151 = !packet.Translator.ReadBit();

            packet.Translator.StartBitStream(guid4, 7, 0, 1, 3, 4, 2, 5, 6);

            guid2[2] = packet.Translator.ReadBit();

            var hasTargetFlags = !packet.Translator.ReadBit();
            var bit180 = !packet.Translator.ReadBit();

            guid1[5] = packet.Translator.ReadBit();

            var bit1AC = !packet.Translator.ReadBit();
            var bits140 = (int)packet.Translator.ReadBits(21);
            var bit16C = !packet.Translator.ReadBit();
            var bits2C = (int)packet.Translator.ReadBits("bits2C", 13);
            var bit17C = packet.Translator.ReadBit();
            var bit98 = packet.Translator.ReadBit();

            guid2[3] = packet.Translator.ReadBit();

            var counter2 = packet.Translator.ReadBits(20);

            guid2[1] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();

            packet.Translator.StartBitStream(guid2, 6, 5);

            var bit194 = !packet.Translator.ReadBit();
            var counter1 = packet.Translator.ReadBits(24);

            guid1[3] = packet.Translator.ReadBit();

            var bit1A8 = !packet.Translator.ReadBit();
            var bitC0 = !packet.Translator.ReadBit();

            if (hasTargetFlags)
                packet.Translator.ReadBitsE<TargetFlag>("Target Flags", 20);

            guid1[2] = packet.Translator.ReadBit();

            if (bit98)
                packet.Translator.StartBitStream(guid5, 2, 4, 6, 3, 7, 5, 1, 0);

            guid6 = new byte[counter1][];
            for (var i = 0; i < counter1; ++i)
            {
                guid6[i] = new byte[8];
                packet.Translator.StartBitStream(guid6[i], 6, 7, 0, 2, 5, 4, 1, 3);
            }

            var bitB8 = packet.Translator.ReadBit();

            packet.Translator.ReadBit(); // fake bit

            guid7 = new byte[counter2][];
            for (var i = 0; i < counter2; ++i)
            {
                guid7[i] = new byte[8];
                packet.Translator.StartBitStream(guid7[i], 0, 4, 3, 1, 6, 7, 2, 5);
            }

            packet.Translator.StartBitStream(guid8, 4, 6, 7, 0, 1, 2, 3, 5);

            var bit164 = !packet.Translator.ReadBit();
            if (bitB8)
                packet.Translator.StartBitStream(guid9, 4, 1, 7, 3, 0, 5, 6, 2);

            var hasPowerData = packet.Translator.ReadBit();
            var counter3 = packet.Translator.ReadBits(24);

            var PowerTypeCount = 0u;
            if (hasPowerData)
            {
                packet.Translator.StartBitStream(powerGUID, 7, 4, 0, 6);
                PowerTypeCount = packet.Translator.ReadBits(21);
                packet.Translator.StartBitStream(powerGUID, 5, 2, 3, 1);
            }

            guid11 = new byte[counter3][];
            for (var i = 0; i < counter3; ++i)
            {
                guid11[i] = new byte[8];
                packet.Translator.StartBitStream(guid11[i], 4, 0, 2, 7, 6, 1, 3, 5);
            }

            packet.Translator.StartBitStream(guid2, 0, 4);

            guid1[6] = packet.Translator.ReadBit();

            var bits154 = packet.Translator.ReadBits(3);
            var bit150 = !packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            var counter4 = packet.Translator.ReadBits(25);

            var bitsC0 = 0u;
            if (bitC0)
                bitsC0 = packet.Translator.ReadBits(7);

            for (var i = 0; i < counter4; ++i)
            {
                if (packet.Translator.ReadBits("bits22[0]", 4, i) == 11)
                    packet.Translator.ReadBits("bits22[1]", 4, i);
            }

            packet.Translator.ReadXORByte(guid1, 7);

            if (hasPowerData)
            {
                packet.Translator.ReadXORByte(powerGUID, 3);
                packet.Translator.ReadXORByte(powerGUID, 5);
                packet.Translator.ReadXORByte(powerGUID, 1);
                packet.Translator.ReadXORByte(powerGUID, 4);

                for (var i = 0; i < PowerTypeCount; ++i)
                {
                    packet.Translator.ReadUInt32E<PowerType>("Power Type", i);
                    packet.Translator.ReadInt32("Power Value", i);
                }

                packet.Translator.ReadXORByte(powerGUID, 6);
                packet.Translator.ReadXORByte(powerGUID, 2);
                packet.Translator.ReadInt32("Current Health");
                packet.Translator.ReadInt32("Attack power");
                packet.Translator.ReadInt32("Spell power");
                packet.Translator.ReadXORByte(powerGUID, 7);
                packet.Translator.ReadXORByte(powerGUID, 0);

                packet.Translator.WriteGuid("Power GUID", powerGUID);
            }

            packet.Translator.ParseBitStream(guid8, 5, 3, 2, 7, 4, 0, 6, 1);

            for (var i = 0; i < counter1; ++i)
            {
                packet.Translator.ParseBitStream(guid6[i], 7, 1, 0, 4, 2, 5, 6, 3);
                packet.Translator.WriteGuid("Guid6", guid6[i], i);
            }

            if (bit98)
            {
                packet.Translator.ReadXORByte(guid5, 2);
                packet.Translator.ReadXORByte(guid5, 6);
                packet.Translator.ReadXORByte(guid5, 0);
                packet.Translator.ReadXORByte(guid5, 3);
                packet.Translator.ReadXORByte(guid5, 4);

                packet.Translator.ReadSingle("Float8C");
                packet.Translator.ReadSingle("Float88");
                packet.Translator.ReadSingle("Float90");

                packet.Translator.ReadXORByte(guid5, 7);
                packet.Translator.ReadXORByte(guid5, 5);
                packet.Translator.ReadXORByte(guid5, 1);

                packet.Translator.WriteGuid("Guid10", guid5);
            }

            packet.Translator.ParseBitStream(guid4, 0, 6, 5, 7, 3, 2, 4, 1);
            packet.Translator.ReadXORByte(guid2, 3);

            for (var i = 0; i < counter3; ++i)
            {
                packet.Translator.ParseBitStream(guid11[i], 6, 0, 7, 1, 2, 5, 3, 4);

                packet.Translator.WriteGuid("Guid12", guid11[i], i);
            }

            if (bit168)
                packet.Translator.ReadSingle("Float168");

            packet.Translator.ParseBitStream(guid3, 3, 1, 5, 0, 7, 6, 4, 2);

            packet.Translator.ReadXORByte(guid1, 3);

            if (bit1AC)
                packet.Translator.ReadByte("Byte1AC");

            packet.Translator.ReadXORByte(guid2, 4);

            if (bit198)
                packet.Translator.ReadInt32("Int198");

            if (bit151)
                packet.Translator.ReadByte("Byte151");

            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid1, 4);

            if (bitB8)
            {
                packet.Translator.ReadSingle("FloatB0");

                packet.Translator.ReadXORByte(guid9, 4);

                packet.Translator.ReadSingle("FloatAC");
                packet.Translator.ReadSingle("FloatA8");

                packet.Translator.ReadXORByte(guid9, 7);
                packet.Translator.ReadXORByte(guid9, 3);
                packet.Translator.ReadXORByte(guid9, 2);
                packet.Translator.ReadXORByte(guid9, 1);
                packet.Translator.ReadXORByte(guid9, 6);
                packet.Translator.ReadXORByte(guid9, 0);
                packet.Translator.ReadXORByte(guid9, 5);

                packet.Translator.WriteGuid("Guid9", guid9);
            }

            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid1, 6);

            if (bit150)
                packet.Translator.ReadByte("Byte150");

            packet.Translator.ReadXORByte(guid1, 1);

            packet.Translator.ReadInt32("Int30");

            for (var i = 0; i < counter2; ++i)
            {
                packet.Translator.ReadXORByte(guid7[i], 2);

                packet.Translator.ReadSingle("Float188", i);

                packet.Translator.ReadXORByte(guid7[i], 6);

                packet.Translator.ReadSingle("Float188", i);
                packet.Translator.ReadSingle("Float188", i);

                packet.Translator.ReadXORByte(guid7[i], 4);
                packet.Translator.ReadXORByte(guid7[i], 1);
                packet.Translator.ReadXORByte(guid7[i], 3);
                packet.Translator.ReadXORByte(guid7[i], 0);
                packet.Translator.ReadXORByte(guid7[i], 7);
                packet.Translator.ReadXORByte(guid7[i], 5);

                packet.Translator.WriteGuid("Guid7", guid7[i], i);
            }

            if (bit17C)
            {
                packet.Translator.ReadInt32("Int178");
                packet.Translator.ReadInt32("Int174");
            }

            packet.Translator.ReadInt32<SpellId>("Spell ID");

            if (bit16C)
                packet.Translator.ReadInt32("Int16C");

            packet.Translator.ReadXORByte(guid2, 6);

            if (bit1A8)
                packet.Translator.ReadInt32("Int1A8");

            for (var i = 0; i < bits140; ++i)
            {
                packet.Translator.ReadByteE<PowerType>("Power type", i);
                packet.Translator.ReadInt32("Value", i);
            }

            for (var i = 0; i < bits154; ++i)
                packet.Translator.ReadByte("Byte158", i);

            packet.Translator.ReadWoWString("StringC0", bitsC0);

            packet.Translator.ReadXORByte(guid2, 1);

            if (bit170)
                packet.Translator.ReadByte("Byte170");

            if (bit164)
                packet.Translator.ReadInt32("Int164");

            if (bit180)
                packet.Translator.ReadByte("Byte180");

            packet.Translator.ReadInt32("Int28");
            packet.Translator.ReadXORByte(guid1, 5);

            if (bit194)
                packet.Translator.ReadInt32("Int194");

            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 2);

            packet.Translator.ReadByte("Byte20");

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid3", guid3);
            packet.Translator.WriteGuid("Guid4", guid4);
            packet.Translator.WriteGuid("Guid5", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5410)]
        public static void HandleUnknow5410(Packet packet)
        {
            var guid1 = new byte[8];
            var powerGUID = new byte[8];
            var guid3 = new byte[8];

            guid1[2] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid3, 2, 1);
            var bit20 = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 0, 5);
            var bit6C = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid3, 7, 5);
            guid1[7] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid3, 0, 4);
            guid1[1] = packet.Translator.ReadBit();
            guid3[3] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid3[6] = packet.Translator.ReadBit();

            var hasPowerData = packet.Translator.ReadBit();
            var counter = packet.Translator.ReadBits(22);

            var powerCount = 0u;
            if (hasPowerData)
            {
                packet.Translator.StartBitStream(powerGUID, 2, 5, 7, 6);
                powerCount = packet.Translator.ReadBits(21);
                packet.Translator.StartBitStream(powerGUID, 4, 3, 0, 1);
            }

            guid1[6] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();

            var bitC = new bool[counter];
            var bit14 = new bool[counter];
            for (var i = 0; i < counter; ++i)
            {
                bitC[i] = packet.Translator.ReadBit();
                bit14[i] = packet.Translator.ReadBit();
                packet.Translator.ReadBit();
            }

            for (var i = 0; i < counter; ++i)
            {
                if (bitC[i])
                    packet.Translator.ReadInt32("IntC", i);

                if (bit14[i])
                    packet.Translator.ReadInt32("Int14", i);

                packet.Translator.ReadInt32<SpellId>("Spell ID", i);
            }

            if (hasPowerData)
            {
                packet.Translator.ReadXORByte(powerGUID, 3);
                packet.Translator.ReadInt32("Spell power");
                packet.Translator.ReadInt32("Current Health");
                packet.Translator.ReadXORByte(powerGUID, 6);
                packet.Translator.ReadXORByte(powerGUID, 7);

                for (var i = 0; i < powerCount; ++i)
                {
                    packet.Translator.ReadInt32("Power Value", i);
                    packet.Translator.ReadUInt32E<PowerType>("Power Type", i);
                }

                packet.Translator.ReadInt32("Attack power");
                packet.Translator.ReadXORByte(powerGUID, 2);
                packet.Translator.ReadXORByte(powerGUID, 4);
                packet.Translator.ReadXORByte(powerGUID, 1);
                packet.Translator.ReadXORByte(powerGUID, 5);
                packet.Translator.ReadXORByte(powerGUID, 0);

                packet.Translator.WriteGuid("Power GUID", powerGUID);
            }

            packet.Translator.ReadInt32("Int68");
            packet.Translator.ReadXORByte(guid3, 0);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid3, 3);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid3, 4);
            packet.Translator.ReadXORByte(guid3, 5);
            packet.Translator.ReadXORByte(guid3, 7);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid3, 2);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid3, 1);
            packet.Translator.ReadXORByte(guid3, 6);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 7);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid3);
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL_KIT)]
        public static void HandleCastVisualKit(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 3, 0, 6, 7, 4, 1, 5, 2);
            packet.Translator.ReadInt32("SpellVisualKit ID");
            packet.Translator.ParseBitStream(guid, 4, 7, 0);
            packet.Translator.ReadInt32("Int20");
            packet.Translator.ParseBitStream(guid, 2, 1, 5, 6, 3);
            packet.Translator.ReadInt32("Int1C");

            packet.Translator.WriteGuid("Guid", guid);
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
                for (var i = 0; i < modTypeCount[j]; ++i)
                {
                    packet.Translator.ReadSingle("Amount", j, i);
                    packet.Translator.ReadByte("Spell Mask bitpos", j, i);
                }

                packet.Translator.ReadByteE<SpellModOp>("Spell Mod", j);
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

        [Parser(Opcode.CMSG_CAST_SPELL)]
        public static void HandleCastSpell(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var guid3 = new byte[8];
            var guid4 = new byte[8];
            var guid5 = new byte[8];
            var guid6 = new byte[8];

            var bit158 = false;
            var bit154 = false;
            var bit14C = false;
            var bit120 = false;
            var bit17C = false;
            var bit178 = false;
            var bit180 = false;
            var bit110 = false;
            var bit198 = false;
            var bit160 = false;

            var hasTargetMask = !packet.Translator.ReadBit();
            var hasCastCount = !packet.Translator.ReadBit();
            var bit18 = !packet.Translator.ReadBit();
            var hasSpellId = !packet.Translator.ReadBit();

            var counter = packet.Translator.ReadBits(2);

            packet.Translator.ReadBit(); // fake bit

            var bitFC = !packet.Translator.ReadBit();

            packet.Translator.ReadBit(); // fake bit

            var bit70 = packet.Translator.ReadBit();
            var bitF8 = !packet.Translator.ReadBit();
            var bit78 = !packet.Translator.ReadBit();
            var bit50 = packet.Translator.ReadBit();
            var bit1A0 = packet.Translator.ReadBit();
            var bit1C = !packet.Translator.ReadBit();

            for (var i = 0; i < counter; ++i)
                packet.Translator.ReadBits("unk value0", 2, i);

            var bits188 = 0u;
            if (bit1A0)
            {
                bit198 = !packet.Translator.ReadBit();
                guid1[3] = packet.Translator.ReadBit();
                bit180 = !packet.Translator.ReadBit();
                bits188 = packet.Translator.ReadBits(22);
                bit17C = packet.Translator.ReadBit();
                var bit108 = !packet.Translator.ReadBit();

                packet.Translator.StartBitStream(guid1, 6, 0);

                if (bit108)
                    packet.Translator.ReadBits("bits108", 30);

                bit160 = !packet.Translator.ReadBit();
                var bit184 = packet.Translator.ReadBit();

                packet.Translator.StartBitStream(guid1, 2, 7, 1, 5);

                bit120 = !packet.Translator.ReadBit();

                if (bit17C)
                    bit178 = packet.Translator.ReadBit();

                var bit19C = packet.Translator.ReadBit();
                guid1[4] = packet.Translator.ReadBit();
                bit158 = packet.Translator.ReadBit();

                if (bit158)
                {
                    bit14C = packet.Translator.ReadBit("bit14C");
                    packet.Translator.StartBitStream(guid2, 6, 3, 1, 0, 4);
                    bit154 = packet.Translator.ReadBit();
                    packet.Translator.StartBitStream(guid2, 7, 2, 5);
                }

                var bit185 = packet.Translator.ReadBit();
                var bit10C = !packet.Translator.ReadBit();

                if (bit10C)
                    packet.Translator.ReadBits("bits10C", 13);

                bit110 = !packet.Translator.ReadBit();
            }

            if (hasTargetMask)
                packet.Translator.ReadBitsE<TargetFlag>("Target Flags", 20);

            packet.Translator.StartBitStream(guid3, 2, 1, 3, 6, 5, 4, 7, 0);

            if (bit70)
                packet.Translator.StartBitStream(guid4, 3, 6, 1, 0, 4, 5, 7, 2);

            if (bit50)
                packet.Translator.StartBitStream(guid5, 6, 3, 5, 2, 0, 4, 1, 7);

            packet.Translator.StartBitStream(guid6, 5, 0, 2, 3, 1, 4, 6, 7);

            var bits2F7 = 0u;
            if (bit78)
                bits2F7 = packet.Translator.ReadBits("bits2F7", 7);

            if (bit1C)
                packet.Translator.ReadBits("bits2F5", 5);

            for (var i = 0; i < counter; ++i)
            {
                packet.Translator.ReadInt32("Int1AC", i);
                packet.Translator.ReadInt32("Int1AC", i);
            }

            if (bit1A0)
            {
                if (bit158)
                {
                    packet.Translator.ReadXORByte(guid2, 5);

                    if (bit154)
                        packet.Translator.ReadInt32("Int150");

                    packet.Translator.ReadSingle("Float13C");
                    packet.Translator.ReadSingle("Float134");

                    if (bit14C)
                        packet.Translator.ReadInt32("Int148");

                    packet.Translator.ParseBitStream(guid2, 7, 2, 0);

                    packet.Translator.ReadInt32("Int144");

                    packet.Translator.ParseBitStream(guid2, 1, 6, 3);

                    packet.Translator.ReadSingle("Float130");

                    packet.Translator.ReadXORByte(guid2, 4);

                    packet.Translator.ReadByte("Byte140");
                    packet.Translator.ReadSingle("Float138");

                    packet.Translator.WriteGuid("Guid2", guid2);
                }

                if (bit120)
                    packet.Translator.ReadSingle("Float120");

                packet.Translator.ParseBitStream(guid1, 6, 4);

                if (bit17C)
                {
                    packet.Translator.ReadInt32("Int164");

                    if (bit178)
                    {
                        packet.Translator.ReadSingle("Float170");
                        packet.Translator.ReadSingle("Float16C");
                        packet.Translator.ReadSingle("Float174");
                    }

                    packet.Translator.ReadSingle("Float168");
                }

                packet.Translator.ParseBitStream(guid1, 3, 2);

                if (bit160)
                    packet.Translator.ReadSingle("Float160");

                if (bit198)
                    packet.Translator.ReadInt32("Int198");

                packet.Translator.ReadSingle("Float11C");

                if (bit110)
                    packet.Translator.ReadInt32("Int110");

                packet.Translator.ParseBitStream(guid1, 5, 0);

                packet.Translator.ReadSingle("Float114");
                packet.Translator.ReadSingle("Float118");

                for (var i = 0; i < bits188; ++i)
                    packet.Translator.ReadInt32("IntEB", i);

                if (bit180)
                    packet.Translator.ReadSingle("Float180");

                packet.Translator.ParseBitStream(guid1, 7, 1);

                packet.Translator.WriteGuid("Guid1", guid1);
            }

            if (hasSpellId)
                packet.Translator.ReadInt32<SpellId>("Spell ID");

            if (bit50)
            {
                packet.Translator.ReadXORByte(guid5, 7);
                packet.Translator.ReadSingle("Float40");
                packet.Translator.ParseBitStream(guid5, 6, 0);
                packet.Translator.ReadSingle("Float44");
                packet.Translator.ParseBitStream(guid5, 1, 4);
                packet.Translator.ReadSingle("Float48");
                packet.Translator.ParseBitStream(guid5, 3, 2, 5);
                packet.Translator.WriteGuid("Guid5", guid5);
            }

            if (bit70)
            {
                packet.Translator.ParseBitStream(guid4, 5, 4, 3, 1);
                packet.Translator.ReadSingle("Float68");
                packet.Translator.ReadSingle("Float64");
                packet.Translator.ParseBitStream(guid4, 2, 6, 7);
                packet.Translator.ReadSingle("Float60");
                packet.Translator.ReadXORByte(guid4, 0);

                packet.Translator.WriteGuid("Guid4", guid4);
            }

            packet.Translator.ParseBitStream(guid6, 7, 2, 6, 0, 4, 5, 1, 3);
            packet.Translator.ParseBitStream(guid3, 1, 0, 2, 3, 5, 6, 7, 4);

            if (hasCastCount)
                packet.Translator.ReadByte("Cast Count");

            if (bitF8)
                packet.Translator.ReadSingle("FloatF8");

            if (bit78)
                packet.Translator.ReadWoWString("string2F7", bits2F7);

            if (bit18)
                packet.Translator.ReadInt32("Int18");

            if (bitFC)
                packet.Translator.ReadSingle("FloatFC");

            packet.Translator.WriteGuid("Guid6", guid6);
            packet.Translator.WriteGuid("Guid3", guid3);
        }

        [Parser(Opcode.SMSG_TOTEM_CREATED)]
        public static void HandleTotemCreated(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 2, 6, 4, 0, 7, 3, 5, 1);
            packet.Translator.ReadUInt32("Duration");
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadUInt32<SpellId>("Spell Id");
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 5);

            packet.Translator.WriteGuid("Guid", guid);
        }
    }
}

using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParser.V5_4_0_17359.Parsers
{
    public static class SpellHandler
    {
        [Parser(Opcode.SMSG_SEND_KNOWN_SPELLS)]
        public static void HandleInitialSpells(Packet packet)
        {
            var count = packet.ReadBits("Spell Count", 22);
            packet.ReadBit("InitialLogin");

            var spells = new List<uint>((int)count);
            for (var i = 0; i < count; i++)
            {
                var spellId = packet.ReadUInt32<SpellId>("Spell ID", i);
                spells.Add(spellId);
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

        [Parser(Opcode.SMSG_LEARNED_SPELLS)]
        public static void HandleLearnedSpells(Packet packet)
        {
            packet.ReadBit("SuppressMessaging");

            var count = packet.ReadBits("Spell Count", 22);

            for (var i = 0; i < count; ++i)
                packet.ReadInt32<SpellId>("Spell ID", i);
        }

        [Parser(Opcode.SMSG_UNLEARNED_SPELLS)]
        public static void HandleRemovedSpell(Packet packet)
        {
            var count = packet.ReadBits("Spell Count", 22);

            for (var i = 0; i < count; ++i)
                packet.ReadUInt32<SpellId>("Spell ID", i);
        }

        [Parser(Opcode.SMSG_CATEGORY_COOLDOWN)]
        public static void HandleSpellCategoryCooldown(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);

            for (int i = 0; i < count; ++i)
            {
                packet.ReadInt32("Category Cooldown", i);
                packet.ReadInt32("Cooldown", i);
            }
        }

        [Parser(Opcode.SMSG_WEEKLY_SPELL_USAGE)]
        public static void HandleWeeklySpellUsage(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);

            for (int i = 0; i < count; ++i)
            {
                packet.ReadByte("Unk Int8");
                packet.ReadInt32("Unk Int32");
            }
        }

        [Parser(Opcode.SMSG_SEND_UNLEARN_SPELLS)]
        public static void HandleSendUnlearnSpells(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);
            for (var i = 0; i < count; i++)
            {
                packet.ReadByte("Unk Byte", i);
                packet.ReadInt32("Unk Int32", i);
                packet.ReadInt32<SpellId>("Spell ID", i);
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            var guid = new byte[8];
            var powerGUID = new byte[8];

            packet.ReadBit(); // fake bit?

            packet.StartBitStream(guid, 6, 1, 0);

            var bits4 = (int)packet.ReadBits(24);

            packet.StartBitStream(guid, 2, 4);

            var hasPowerData = packet.ReadBit();

            var powerCount = 0u;
            if (hasPowerData)
            {
                packet.StartBitStream(powerGUID, 7, 0, 6);
                powerCount = packet.ReadBits(21);
                packet.StartBitStream(powerGUID, 3, 1, 2, 4, 5);
            }

            packet.StartBitStream(guid, 7, 3, 5);

            var hasAura = new bool[bits4];
            var hasCasterGUID = new bool[bits4];
            var hasDuration = new bool[bits4];
            var hasMaxDuration = new bool[bits4];
            var effectCount = new uint[bits4];
            var casterGUID = new byte[bits4][];
            var bitsEC = new uint[bits4];

            for (var i = 0; i < bits4; ++i)
            {
                hasAura[i] = packet.ReadBit();

                if (hasAura[i])
                {
                    hasMaxDuration[i] = packet.ReadBit();
                    effectCount[i] = packet.ReadBits(22);
                    hasCasterGUID[i] = packet.ReadBit();
                    if (hasCasterGUID[i])
                    {
                        casterGUID[i] = new byte[8];
                        packet.StartBitStream(casterGUID[i], 3, 0, 2, 6, 5, 7, 4, 1);
                    }

                    hasDuration[i] = packet.ReadBit();
                    bitsEC[i] = packet.ReadBits(22);
                }
            }

            var auras = new List<Aura>();
            for (var i = 0; i < bits4; ++i)
            {
                if (hasAura[i])
                {
                    var aura = new Aura();

                    aura.Duration = hasDuration[i] ? packet.ReadInt32("Duration", i) : 0;

                    if (hasCasterGUID[i])
                    {
                        packet.ParseBitStream(casterGUID[i], 0, 7, 5, 6, 1, 3, 2, 4);
                        packet.WriteGuid("Caster GUID", casterGUID[i], i);
                        aura.CasterGuid = new WowGuid64(BitConverter.ToUInt64(casterGUID[i], 0));
                    }
                    else
                        aura.CasterGuid = new WowGuid64();

                    aura.AuraFlags = packet.ReadByteE<AuraFlagMoP>("Flags", i);

                    for (var j = 0; j < effectCount[i]; ++j)
                        packet.ReadSingle("Effect Value", i, j);

                    aura.SpellId = packet.ReadUInt32("Spell Id", i);

                    aura.MaxDuration = hasMaxDuration[i] ? packet.ReadInt32("Max Duration", i) : 0;

                    for (var j = 0; j < bitsEC[i]; ++j)
                        packet.ReadSingle("FloatEA");

                    aura.Charges = packet.ReadByte("Charges", i);
                    packet.ReadInt32("Effect Mask", i);
                    aura.Level = packet.ReadUInt16("Caster Level", i);
                    auras.Add(aura);
                    packet.AddSniffData(StoreNameType.Spell, (int)aura.SpellId, "AURA_UPDATE");
                }

                packet.ReadByte("Slot", i);
            }

            if (hasPowerData)
            {
                packet.ReadXORByte(powerGUID, 7);
                packet.ReadXORByte(powerGUID, 4);
                packet.ReadXORByte(powerGUID, 5);
                packet.ReadXORByte(powerGUID, 1);
                packet.ReadXORByte(powerGUID, 6);
                for (var i = 0; i < powerCount; ++i)
                {
                    packet.ReadInt32("Value", i);
                    packet.ReadInt32E<PowerType>("Power type", i); // Actually powertype for class
                }

                packet.ReadInt32("Attack power");
                packet.ReadInt32("Spell power");
                packet.ReadXORByte(powerGUID, 3);
                packet.ReadInt32("Current health");
                packet.ReadXORByte(powerGUID, 0);
                packet.ReadXORByte(powerGUID, 2);
                packet.WriteGuid("Power GUID", powerGUID);
            }

            packet.ParseBitStream(guid, 0, 4, 3, 7, 5, 6, 2, 1);

            packet.WriteGuid("Guid", guid);

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

            var bitE8 = packet.ReadBit();
            packet.StartBitStream(guid1, 6, 7);

            packet.ReadBit(); // fake bit

            packet.StartBitStream(guid2, 2, 6, 0, 3, 4, 1, 7, 5);

            guid3[4] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid3[2] = packet.ReadBit();
            var counter1 = (int)packet.ReadBits(24);
            guid1[0] = packet.ReadBit();
            var counter2 = (int)packet.ReadBits(24);
            guid3[5] = packet.ReadBit();

            if (bitE8)
                packet.StartBitStream(guid4, 4, 7, 5, 3, 6, 2, 1, 0);

            packet.ReadBit(); // fake bit

            packet.StartBitStream(guid5, 5, 3, 4, 6, 7, 1, 2, 0);

            var bitF0 = !packet.ReadBit();
            var bit19C = !packet.ReadBit();
            var bit180 = !packet.ReadBit();
            var bit1D8 = !packet.ReadBit();

            guid3[7] = packet.ReadBit();

            guid6 = new byte[counter2][];
            for (var i = 0; i < counter2; ++i)
            {
                guid6[i] = new byte[8];
                packet.StartBitStream(guid6[i], 5, 3, 1, 4, 6, 0, 2, 7);
            }

            guid7 = new byte[counter1][];
            for (var i = 0; i < counter1; ++i)
            {
                guid7[i] = new byte[8];
                packet.StartBitStream(guid7[i], 0, 2, 7, 4, 6, 3, 5, 1);
            }

            var hasTargetMask = !packet.ReadBit();
            var bit1C8 = !packet.ReadBit();

            var counter3 = packet.ReadBits(21);

            var hasPowerData = packet.ReadBit();
            var bitC8 = packet.ReadBit();
            var bit1A0 = !packet.ReadBit();

            if (bitC8)
                packet.StartBitStream(guid8, 4, 7, 6, 3, 2, 0, 5, 1);

            var bit181 = !packet.ReadBit();
            var bit1DC = !packet.ReadBit();

            guid1[4] = packet.ReadBit();

            var powerCount = 0u;
            if (hasPowerData)
            {
                powerGUID[4] = packet.ReadBit();
                powerCount = packet.ReadBits(21);
                packet.StartBitStream(powerGUID, 2, 3, 7, 6, 5, 0, 1);
            }

            var bit198 = !packet.ReadBit();
            var bit194 = !packet.ReadBit();

            if (hasTargetMask)
                packet.ReadBitsE<TargetFlag>("Target Flags", 20);

            var bits5C = packet.ReadBits(13);
            guid1[3] = packet.ReadBit();
            var bits184 = packet.ReadBits(3);

            packet.ReadBit(); // fake bit

            guid1[2] = packet.ReadBit();
            var bit1B0 = !packet.ReadBit();
            guid3[3] = packet.ReadBit();
            var counter4 = (int)packet.ReadBits(20);

            guid10 = new byte[counter4][];
            for (var i = 0; i < counter4; ++i)
            {
                guid10[i] = new byte[8];
                packet.StartBitStream(guid10[i], 5, 1, 4, 7, 3, 6, 0, 2);
            }

            packet.StartBitStream(guid11, 4, 1, 5, 2, 7, 6, 0, 3);

            guid3[1] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid3[6] = packet.ReadBit();

            var counter5 = packet.ReadBits(25);

            guid3[0] = packet.ReadBit();

            var bitsF0 = 0u;
            if (bitF0)
                bitsF0 = packet.ReadBits(7);

            var bit1C4 = !packet.ReadBit();
            var bit1AC = packet.ReadBit();

            for (var i = 0; i < counter5; ++i)
            {
                var bits136 = packet.ReadBits(4);

                if (bits136 == 11)
                    packet.ReadBits("bits140", 4, i);
            }

            packet.ParseBitStream(guid11, 5, 3, 4, 2, 0, 1, 7, 6);

            packet.ReadXORByte(guid1, 2);

            packet.ReadInt32("Int60");

            for (var i = 0; i < counter1; ++i)
            {
                packet.ParseBitStream(guid7[i], 3, 6, 7, 5, 0, 4, 2, 1);
                packet.WriteGuid("Guid7", guid7[i], i);
            }

            packet.ReadXORByte(guid1, 6);

            for (var i = 0; i < counter2; ++i)
            {
                packet.ParseBitStream(guid6[i], 6, 0, 3, 7, 2, 1, 5, 4);
                packet.WriteGuid("Guid7", guid7[i], i);
            }

            packet.ReadXORByte(guid3, 0);

            if (bitC8)
            {
                packet.ReadXORByte(guid8, 6);
                packet.ReadXORByte(guid8, 7);
                packet.ReadXORByte(guid8, 3);
                packet.ReadXORByte(guid8, 0);

                packet.ReadSingle("FloatBC");

                packet.ReadXORByte(guid8, 1);

                packet.ReadSingle("FloatC0");

                packet.ReadXORByte(guid8, 4);
                packet.ReadXORByte(guid8, 2);

                packet.ReadSingle("FloatB8");

                packet.ReadXORByte(guid8, 5);

                packet.WriteGuid("guidG", guid8);
            }

            if (bit19C)
                packet.ReadInt32("Int19C");

            if (bitE8)
            {
                var pos = new Vector3();

                packet.ReadXORByte(guid4, 5);
                packet.ReadXORByte(guid4, 1);
                packet.ReadXORByte(guid4, 6);
                packet.ReadXORByte(guid4, 0);
                packet.ReadXORByte(guid4, 3);
                packet.ReadXORByte(guid4, 4);

                pos.X = packet.ReadSingle("FloatD8");

                packet.ReadXORByte(guid4, 2);

                pos.Y = packet.ReadSingle("FloatDC");
                pos.Z = packet.ReadSingle("FloatE0");

                packet.ReadXORByte(guid4, 7);

                packet.WriteGuid("Guid4", guid4);
                packet.AddValue("Position", pos);
            }

            if (bit1B0)
                packet.ReadByte("Byte1B0");

            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 7);

            packet.ParseBitStream(guid5, 1, 7, 3, 0, 6, 2, 4, 5);

            if (bit1AC)
            {
                packet.ReadInt32("Int1A8");
                packet.ReadInt32("Int1A4");
            }

            packet.ReadXORByte(guid1, 4);

            for (var i = 0; i < counter4; ++i)
            {
                packet.ReadXORByte(guid10[i], 4);
                packet.ReadXORByte(guid10[i], 5);

                packet.ReadSingle("Float1B8", i);

                packet.ReadXORByte(guid10[i], 0);
                packet.ReadXORByte(guid10[i], 1);
                packet.ReadXORByte(guid10[i], 2);
                packet.ReadXORByte(guid10[i], 3);

                packet.ReadSingle("Float1B8", i);
                packet.ReadSingle("Float1B8", i);

                packet.ReadXORByte(guid10[i], 6);
                packet.ReadXORByte(guid10[i], 7);

                packet.WriteGuid("Guid10", guid10[i], i);
            }

            if (hasPowerData)
            {
                packet.ReadInt32("Spell power");
                packet.ReadInt32("Attack power");

                for (var i = 0; i < powerCount; ++i)
                {
                    packet.ReadUInt32E<PowerType>("Power Type", i);
                    packet.ReadInt32("Power Value", i);
                }

                packet.ParseBitStream(powerGUID, 4, 7, 0, 2, 3, 5, 6, 1);

                packet.ReadInt32("Current Health");
                packet.WriteGuid("Power GUID", powerGUID);
            }

            packet.ReadXORByte(guid3, 6);
            packet.ReadXORByte(guid1, 3);

            if (bit180)
                packet.ReadByte("Byte180");

            packet.ParseBitStream(guid2, 3, 2, 1, 4, 6, 0, 7, 5);
            packet.ReadXORByte(guid3, 3);

            for (var i = 0; i < bits184; ++i)
                packet.ReadByte("Byte188", i);

            packet.ReadWoWString("StringF0", bitsF0);
            packet.ReadByte("Cast Count");

            if (bit1C4)
                packet.ReadInt32("Int1C4");

            packet.ReadXORByte(guid3, 7);

            if (bit1D8)
                packet.ReadInt32("Int1D8");

            if (bit198)
                packet.ReadSingle("Float198");

            packet.ReadInt32E<CastFlag>("Cast Flags");

            packet.ReadXORByte(guid3, 4);
            packet.ReadXORByte(guid3, 1);

            packet.ReadInt32<SpellId>("Spell ID");

            if (bit1DC)
                packet.ReadByte("Byte1DC");

            for (var i = 0; i < counter3; ++i)
            {
                packet.ReadByte("ByteED", i);
                packet.ReadInt32("IntED", i);
            }

            packet.ReadXORByte(guid1, 0);

            if (bit1A0)
                packet.ReadByte("Byte1A0");

            if (bit181)
                packet.ReadByte("Byte181");

            if (bit194)
                packet.ReadInt32("Int194");

            packet.ReadXORByte(guid1, 1);

            if (bit1C8)
                packet.ReadInt32("Int1C8");

            packet.ReadXORByte(guid3, 5);
            packet.ReadXORByte(guid3, 2);

            packet.WriteGuid("Guid8", guid1);
            packet.WriteGuid("Guid9", guid3);
            packet.WriteGuid("Guid2", guid2);
            packet.WriteGuid("Guid11", guid11);
            packet.WriteGuid("Guid5", guid5);
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

            packet.ReadBit(); // fake bit

            guid1[4] = packet.ReadBit();

            var bit198 = !packet.ReadBit();
            var bit170 = !packet.ReadBit();
            var bit168 = !packet.ReadBit();

            packet.ReadBit(); // fake bit

            guid2[7] = packet.ReadBit();

            packet.StartBitStream(guid3, 2, 5, 6, 1, 0, 3, 7, 4);

            guid1[1] = packet.ReadBit();

            var bit151 = !packet.ReadBit();

            packet.StartBitStream(guid4, 7, 0, 1, 3, 4, 2, 5, 6);

            guid2[2] = packet.ReadBit();

            var hasTargetFlags = !packet.ReadBit();
            var bit180 = !packet.ReadBit();

            guid1[5] = packet.ReadBit();

            var bit1AC = !packet.ReadBit();
            var bits140 = (int)packet.ReadBits(21);
            var bit16C = !packet.ReadBit();
            var bits2C = (int)packet.ReadBits("bits2C", 13);
            var bit17C = packet.ReadBit();
            var bit98 = packet.ReadBit();

            guid2[3] = packet.ReadBit();

            var counter2 = packet.ReadBits(20);

            guid2[1] = packet.ReadBit();
            guid1[0] = packet.ReadBit();

            packet.StartBitStream(guid2, 6, 5);

            var bit194 = !packet.ReadBit();
            var counter1 = packet.ReadBits(24);

            guid1[3] = packet.ReadBit();

            var bit1A8 = !packet.ReadBit();
            var bitC0 = !packet.ReadBit();

            if (hasTargetFlags)
                packet.ReadBitsE<TargetFlag>("Target Flags", 20);

            guid1[2] = packet.ReadBit();

            if (bit98)
                packet.StartBitStream(guid5, 2, 4, 6, 3, 7, 5, 1, 0);

            guid6 = new byte[counter1][];
            for (var i = 0; i < counter1; ++i)
            {
                guid6[i] = new byte[8];
                packet.StartBitStream(guid6[i], 6, 7, 0, 2, 5, 4, 1, 3);
            }

            var bitB8 = packet.ReadBit();

            packet.ReadBit(); // fake bit

            guid7 = new byte[counter2][];
            for (var i = 0; i < counter2; ++i)
            {
                guid7[i] = new byte[8];
                packet.StartBitStream(guid7[i], 0, 4, 3, 1, 6, 7, 2, 5);
            }

            packet.StartBitStream(guid8, 4, 6, 7, 0, 1, 2, 3, 5);

            var bit164 = !packet.ReadBit();
            if (bitB8)
                packet.StartBitStream(guid9, 4, 1, 7, 3, 0, 5, 6, 2);

            var hasPowerData = packet.ReadBit();
            var counter3 = packet.ReadBits(24);

            var PowerTypeCount = 0u;
            if (hasPowerData)
            {
                packet.StartBitStream(powerGUID, 7, 4, 0, 6);
                PowerTypeCount = packet.ReadBits(21);
                packet.StartBitStream(powerGUID, 5, 2, 3, 1);
            }

            guid11 = new byte[counter3][];
            for (var i = 0; i < counter3; ++i)
            {
                guid11[i] = new byte[8];
                packet.StartBitStream(guid11[i], 4, 0, 2, 7, 6, 1, 3, 5);
            }

            packet.StartBitStream(guid2, 0, 4);

            guid1[6] = packet.ReadBit();

            var bits154 = packet.ReadBits(3);
            var bit150 = !packet.ReadBit();
            guid1[7] = packet.ReadBit();
            var counter4 = packet.ReadBits(25);

            var bitsC0 = 0u;
            if (bitC0)
                bitsC0 = packet.ReadBits(7);

            for (var i = 0; i < counter4; ++i)
            {
                if (packet.ReadBits("bits22[0]", 4, i) == 11)
                    packet.ReadBits("bits22[1]", 4, i);
            }

            packet.ReadXORByte(guid1, 7);

            if (hasPowerData)
            {
                packet.ReadXORByte(powerGUID, 3);
                packet.ReadXORByte(powerGUID, 5);
                packet.ReadXORByte(powerGUID, 1);
                packet.ReadXORByte(powerGUID, 4);

                for (var i = 0; i < PowerTypeCount; ++i)
                {
                    packet.ReadUInt32E<PowerType>("Power Type", i);
                    packet.ReadInt32("Power Value", i);
                }

                packet.ReadXORByte(powerGUID, 6);
                packet.ReadXORByte(powerGUID, 2);
                packet.ReadInt32("Current Health");
                packet.ReadInt32("Attack power");
                packet.ReadInt32("Spell power");
                packet.ReadXORByte(powerGUID, 7);
                packet.ReadXORByte(powerGUID, 0);

                packet.WriteGuid("Power GUID", powerGUID);
            }

            packet.ParseBitStream(guid8, 5, 3, 2, 7, 4, 0, 6, 1);

            for (var i = 0; i < counter1; ++i)
            {
                packet.ParseBitStream(guid6[i], 7, 1, 0, 4, 2, 5, 6, 3);
                packet.WriteGuid("Guid6", guid6[i], i);
            }

            if (bit98)
            {
                packet.ReadXORByte(guid5, 2);
                packet.ReadXORByte(guid5, 6);
                packet.ReadXORByte(guid5, 0);
                packet.ReadXORByte(guid5, 3);
                packet.ReadXORByte(guid5, 4);

                packet.ReadSingle("Float8C");
                packet.ReadSingle("Float88");
                packet.ReadSingle("Float90");

                packet.ReadXORByte(guid5, 7);
                packet.ReadXORByte(guid5, 5);
                packet.ReadXORByte(guid5, 1);

                packet.WriteGuid("Guid10", guid5);
            }

            packet.ParseBitStream(guid4, 0, 6, 5, 7, 3, 2, 4, 1);
            packet.ReadXORByte(guid2, 3);

            for (var i = 0; i < counter3; ++i)
            {
                packet.ParseBitStream(guid11[i], 6, 0, 7, 1, 2, 5, 3, 4);

                packet.WriteGuid("Guid12", guid11[i], i);
            }

            if (bit168)
                packet.ReadSingle("Float168");

            packet.ParseBitStream(guid3, 3, 1, 5, 0, 7, 6, 4, 2);

            packet.ReadXORByte(guid1, 3);

            if (bit1AC)
                packet.ReadByte("Byte1AC");

            packet.ReadXORByte(guid2, 4);

            if (bit198)
                packet.ReadInt32("Int198");

            if (bit151)
                packet.ReadByte("Byte151");

            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 4);

            if (bitB8)
            {
                packet.ReadSingle("FloatB0");

                packet.ReadXORByte(guid9, 4);

                packet.ReadSingle("FloatAC");
                packet.ReadSingle("FloatA8");

                packet.ReadXORByte(guid9, 7);
                packet.ReadXORByte(guid9, 3);
                packet.ReadXORByte(guid9, 2);
                packet.ReadXORByte(guid9, 1);
                packet.ReadXORByte(guid9, 6);
                packet.ReadXORByte(guid9, 0);
                packet.ReadXORByte(guid9, 5);

                packet.WriteGuid("Guid9", guid9);
            }

            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 6);

            if (bit150)
                packet.ReadByte("Byte150");

            packet.ReadXORByte(guid1, 1);

            packet.ReadInt32("Int30");

            for (var i = 0; i < counter2; ++i)
            {
                packet.ReadXORByte(guid7[i], 2);

                packet.ReadSingle("Float188", i);

                packet.ReadXORByte(guid7[i], 6);

                packet.ReadSingle("Float188", i);
                packet.ReadSingle("Float188", i);

                packet.ReadXORByte(guid7[i], 4);
                packet.ReadXORByte(guid7[i], 1);
                packet.ReadXORByte(guid7[i], 3);
                packet.ReadXORByte(guid7[i], 0);
                packet.ReadXORByte(guid7[i], 7);
                packet.ReadXORByte(guid7[i], 5);

                packet.WriteGuid("Guid7", guid7[i], i);
            }

            if (bit17C)
            {
                packet.ReadInt32("Int178");
                packet.ReadInt32("Int174");
            }

            packet.ReadInt32<SpellId>("Spell ID");

            if (bit16C)
                packet.ReadInt32("Int16C");

            packet.ReadXORByte(guid2, 6);

            if (bit1A8)
                packet.ReadInt32("Int1A8");

            for (var i = 0; i < bits140; ++i)
            {
                packet.ReadByteE<PowerType>("Power type", i);
                packet.ReadInt32("Value", i);
            }

            for (var i = 0; i < bits154; ++i)
                packet.ReadByte("Byte158", i);

            packet.ReadWoWString("StringC0", bitsC0);

            packet.ReadXORByte(guid2, 1);

            if (bit170)
                packet.ReadByte("Byte170");

            if (bit164)
                packet.ReadInt32("Int164");

            if (bit180)
                packet.ReadByte("Byte180");

            packet.ReadInt32("Int28");
            packet.ReadXORByte(guid1, 5);

            if (bit194)
                packet.ReadInt32("Int194");

            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 2);

            packet.ReadByte("Byte20");

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid3", guid3);
            packet.WriteGuid("Guid4", guid4);
            packet.WriteGuid("Guid5", guid2);
        }

        [Parser(Opcode.SMSG_UNKNOWN_5410)]
        public static void HandleUnknow5410(Packet packet)
        {
            var guid1 = new byte[8];
            var powerGUID = new byte[8];
            var guid3 = new byte[8];

            guid1[2] = packet.ReadBit();
            packet.StartBitStream(guid3, 2, 1);
            var bit20 = packet.ReadBit();
            packet.StartBitStream(guid1, 0, 5);
            var bit6C = packet.ReadBit();
            packet.StartBitStream(guid3, 7, 5);
            guid1[7] = packet.ReadBit();
            packet.StartBitStream(guid3, 0, 4);
            guid1[1] = packet.ReadBit();
            guid3[3] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid3[6] = packet.ReadBit();

            var hasPowerData = packet.ReadBit();
            var counter = packet.ReadBits(22);

            var powerCount = 0u;
            if (hasPowerData)
            {
                packet.StartBitStream(powerGUID, 2, 5, 7, 6);
                powerCount = packet.ReadBits(21);
                packet.StartBitStream(powerGUID, 4, 3, 0, 1);
            }

            guid1[6] = packet.ReadBit();
            guid1[4] = packet.ReadBit();

            var bitC = new bool[counter];
            var bit14 = new bool[counter];
            for (var i = 0; i < counter; ++i)
            {
                bitC[i] = packet.ReadBit();
                bit14[i] = packet.ReadBit();
                packet.ReadBit();
            }

            for (var i = 0; i < counter; ++i)
            {
                if (bitC[i])
                    packet.ReadInt32("IntC", i);

                if (bit14[i])
                    packet.ReadInt32("Int14", i);

                packet.ReadInt32<SpellId>("Spell ID", i);
            }

            if (hasPowerData)
            {
                packet.ReadXORByte(powerGUID, 3);
                packet.ReadInt32("Spell power");
                packet.ReadInt32("Current Health");
                packet.ReadXORByte(powerGUID, 6);
                packet.ReadXORByte(powerGUID, 7);

                for (var i = 0; i < powerCount; ++i)
                {
                    packet.ReadInt32("Power Value", i);
                    packet.ReadUInt32E<PowerType>("Power Type", i);
                }

                packet.ReadInt32("Attack power");
                packet.ReadXORByte(powerGUID, 2);
                packet.ReadXORByte(powerGUID, 4);
                packet.ReadXORByte(powerGUID, 1);
                packet.ReadXORByte(powerGUID, 5);
                packet.ReadXORByte(powerGUID, 0);

                packet.WriteGuid("Power GUID", powerGUID);
            }

            packet.ReadInt32("Int68");
            packet.ReadXORByte(guid3, 0);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid3, 3);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid3, 4);
            packet.ReadXORByte(guid3, 5);
            packet.ReadXORByte(guid3, 7);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid3, 2);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid3, 1);
            packet.ReadXORByte(guid3, 6);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 7);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid3);
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL_KIT)]
        public static void HandleCastVisualKit(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 3, 0, 6, 7, 4, 1, 5, 2);
            packet.ReadInt32("SpellVisualKit ID");
            packet.ParseBitStream(guid, 4, 7, 0);
            packet.ReadInt32("Int20");
            packet.ParseBitStream(guid, 2, 1, 5, 6, 3);
            packet.ReadInt32("Int1C");

            packet.WriteGuid("Guid", guid);
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
                for (var i = 0; i < modTypeCount[j]; ++i)
                {
                    packet.ReadSingle("Amount", j, i);
                    packet.ReadByte("Spell Mask bitpos", j, i);
                }

                packet.ReadByteE<SpellModOp>("Spell Mod", j);
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
                packet.ReadByteE<SpellModOp>("Spell Mod", j);
                for (var i = 0; i < modTypeCount[j]; ++i)
                {
                    packet.ReadSingle("Amount", j, i);
                    packet.ReadByte("Spell Mask bitpos", j, i);
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

            var hasTargetMask = !packet.ReadBit();
            var hasCastCount = !packet.ReadBit();
            var bit18 = !packet.ReadBit();
            var hasSpellId = !packet.ReadBit();

            var counter = packet.ReadBits(2);

            packet.ReadBit(); // fake bit

            var bitFC = !packet.ReadBit();

            packet.ReadBit(); // fake bit

            var bit70 = packet.ReadBit();
            var bitF8 = !packet.ReadBit();
            var bit78 = !packet.ReadBit();
            var bit50 = packet.ReadBit();
            var bit1A0 = packet.ReadBit();
            var bit1C = !packet.ReadBit();

            for (var i = 0; i < counter; ++i)
                packet.ReadBits("unk value0", 2, i);

            var bits188 = 0u;
            if (bit1A0)
            {
                bit198 = !packet.ReadBit();
                guid1[3] = packet.ReadBit();
                bit180 = !packet.ReadBit();
                bits188 = packet.ReadBits(22);
                bit17C = packet.ReadBit();
                var bit108 = !packet.ReadBit();

                packet.StartBitStream(guid1, 6, 0);

                if (bit108)
                    packet.ReadBits("bits108", 30);

                bit160 = !packet.ReadBit();
                var bit184 = packet.ReadBit();

                packet.StartBitStream(guid1, 2, 7, 1, 5);

                bit120 = !packet.ReadBit();

                if (bit17C)
                    bit178 = packet.ReadBit();

                var bit19C = packet.ReadBit();
                guid1[4] = packet.ReadBit();
                bit158 = packet.ReadBit();

                if (bit158)
                {
                    bit14C = packet.ReadBit("bit14C");
                    packet.StartBitStream(guid2, 6, 3, 1, 0, 4);
                    bit154 = packet.ReadBit();
                    packet.StartBitStream(guid2, 7, 2, 5);
                }

                var bit185 = packet.ReadBit();
                var bit10C = !packet.ReadBit();

                if (bit10C)
                    packet.ReadBits("bits10C", 13);

                bit110 = !packet.ReadBit();
            }

            if (hasTargetMask)
                packet.ReadBitsE<TargetFlag>("Target Flags", 20);

            packet.StartBitStream(guid3, 2, 1, 3, 6, 5, 4, 7, 0);

            if (bit70)
                packet.StartBitStream(guid4, 3, 6, 1, 0, 4, 5, 7, 2);

            if (bit50)
                packet.StartBitStream(guid5, 6, 3, 5, 2, 0, 4, 1, 7);

            packet.StartBitStream(guid6, 5, 0, 2, 3, 1, 4, 6, 7);

            var bits2F7 = 0u;
            if (bit78)
                bits2F7 = packet.ReadBits("bits2F7", 7);

            if (bit1C)
                packet.ReadBits("bits2F5", 5);

            for (var i = 0; i < counter; ++i)
            {
                packet.ReadInt32("Int1AC", i);
                packet.ReadInt32("Int1AC", i);
            }

            if (bit1A0)
            {
                if (bit158)
                {
                    packet.ReadXORByte(guid2, 5);

                    if (bit154)
                        packet.ReadInt32("Int150");

                    packet.ReadSingle("Float13C");
                    packet.ReadSingle("Float134");

                    if (bit14C)
                        packet.ReadInt32("Int148");

                    packet.ParseBitStream(guid2, 7, 2, 0);

                    packet.ReadInt32("Int144");

                    packet.ParseBitStream(guid2, 1, 6, 3);

                    packet.ReadSingle("Float130");

                    packet.ReadXORByte(guid2, 4);

                    packet.ReadByte("Byte140");
                    packet.ReadSingle("Float138");

                    packet.WriteGuid("Guid2", guid2);
                }

                if (bit120)
                    packet.ReadSingle("Float120");

                packet.ParseBitStream(guid1, 6, 4);

                if (bit17C)
                {
                    packet.ReadInt32("Int164");

                    if (bit178)
                    {
                        packet.ReadSingle("Float170");
                        packet.ReadSingle("Float16C");
                        packet.ReadSingle("Float174");
                    }

                    packet.ReadSingle("Float168");
                }

                packet.ParseBitStream(guid1, 3, 2);

                if (bit160)
                    packet.ReadSingle("Float160");

                if (bit198)
                    packet.ReadInt32("Int198");

                packet.ReadSingle("Float11C");

                if (bit110)
                    packet.ReadInt32("Int110");

                packet.ParseBitStream(guid1, 5, 0);

                packet.ReadSingle("Float114");
                packet.ReadSingle("Float118");

                for (var i = 0; i < bits188; ++i)
                    packet.ReadInt32("IntEB", i);

                if (bit180)
                    packet.ReadSingle("Float180");

                packet.ParseBitStream(guid1, 7, 1);

                packet.WriteGuid("Guid1", guid1);
            }

            if (hasSpellId)
                packet.ReadInt32<SpellId>("Spell ID");

            if (bit50)
            {
                packet.ReadXORByte(guid5, 7);
                packet.ReadSingle("Float40");
                packet.ParseBitStream(guid5, 6, 0);
                packet.ReadSingle("Float44");
                packet.ParseBitStream(guid5, 1, 4);
                packet.ReadSingle("Float48");
                packet.ParseBitStream(guid5, 3, 2, 5);
                packet.WriteGuid("Guid5", guid5);
            }

            if (bit70)
            {
                packet.ParseBitStream(guid4, 5, 4, 3, 1);
                packet.ReadSingle("Float68");
                packet.ReadSingle("Float64");
                packet.ParseBitStream(guid4, 2, 6, 7);
                packet.ReadSingle("Float60");
                packet.ReadXORByte(guid4, 0);

                packet.WriteGuid("Guid4", guid4);
            }

            packet.ParseBitStream(guid6, 7, 2, 6, 0, 4, 5, 1, 3);
            packet.ParseBitStream(guid3, 1, 0, 2, 3, 5, 6, 7, 4);

            if (hasCastCount)
                packet.ReadByte("Cast Count");

            if (bitF8)
                packet.ReadSingle("FloatF8");

            if (bit78)
                packet.ReadWoWString("string2F7", bits2F7);

            if (bit18)
                packet.ReadInt32("Int18");

            if (bitFC)
                packet.ReadSingle("FloatFC");

            packet.WriteGuid("Guid6", guid6);
            packet.WriteGuid("Guid3", guid3);
        }

        [Parser(Opcode.SMSG_TOTEM_CREATED)]
        public static void HandleTotemCreated(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 2, 6, 4, 0, 7, 3, 5, 1);
            packet.ReadUInt32("Duration");
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 3);
            packet.ReadUInt32<SpellId>("Spell Id");
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 0);
            packet.ReadByte("Slot");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);

            packet.WriteGuid("Guid", guid);
        }
    }
}

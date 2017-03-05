using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class SpellHandler
    {
        [HasSniffData]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];

            guid[1] = packet.Translator.ReadBit();
            var hasPowerData = packet.Translator.ReadBit();

            uint bits3C = 0;
            if (hasPowerData)
            {
                packet.Translator.StartBitStream(guid2, 1, 5, 6);
                bits3C = packet.Translator.ReadBits(21);
                packet.Translator.StartBitStream(guid2, 2, 3, 7, 0, 4);
            }

            var bits4 = packet.Translator.ReadBits(24);
            guid[6] = packet.Translator.ReadBit();

            var hasAura = new bool[bits4];
            var hasCasterGUID = new bool[bits4];
            var hasDuration = new bool[bits4];
            var hasMaxDuration = new bool[bits4];
            var effectCount = new uint[bits4];
            var casterGUID = new byte[bits4][];
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
                        packet.Translator.StartBitStream(casterGUID[i], 5, 7, 4, 1, 6, 0, 3, 2);
                    }
                    hasDuration[i] = packet.Translator.ReadBit();
                }
            }
            packet.Translator.StartBitStream(guid, 2, 3, 7, 4);
            packet.Translator.ReadBit("Is AURA_UPDATE_ALL");
            packet.Translator.StartBitStream(guid, 0, 5);
            packet.Translator.ResetBitReader();

            var auras = new List<Aura>();
            for (var i = 0; i < bits4; ++i)
            {
                if (hasAura[i])
                {
                    var aura = new Aura();
                    if (hasCasterGUID[i])
                    {
                        packet.Translator.ParseBitStream(casterGUID[i], 2, 7, 6, 1, 4, 0, 5, 3);
                        packet.Translator.WriteGuid("Caster GUID", casterGUID[i], i);
                        aura.CasterGuid = new WowGuid64(BitConverter.ToUInt64(casterGUID[i], 0));
                    }
                    else
                        aura.CasterGuid = new WowGuid64();

                    aura.Charges = packet.Translator.ReadByte("Charges", i);

                    aura.Duration = hasDuration[i] ? packet.Translator.ReadInt32("Duration", i) : 0;

                    aura.SpellId = packet.Translator.ReadUInt32("Spell Id", i);
                    aura.AuraFlags = packet.Translator.ReadByteE<AuraFlagMoP>("Flags", i);

                    for (var j = 0; j < effectCount[i]; ++j)
                        packet.Translator.ReadSingle("Effect Value", i, j);
                    packet.Translator.ReadInt32("Effect Mask", i);

                    aura.MaxDuration = hasMaxDuration[i] ? packet.Translator.ReadInt32("Max Duration", i) : 0;

                    aura.Level = packet.Translator.ReadUInt16("Caster Level", i);
                    auras.Add(aura);
                    packet.AddSniffData(StoreNameType.Spell, (int)aura.SpellId, "AURA_UPDATE");
                }
                packet.Translator.ReadByte("Slot", i);
            }

            if (hasPowerData)
            {
                packet.Translator.ReadXORBytes(guid2, 7, 0);
                for (var i = 0; i < bits3C; ++i)
                {
                    packet.Translator.ReadUInt32E<PowerType>("Power Type", i);
                    packet.Translator.ReadInt32("Power Value", i);
                }
                packet.Translator.ReadXORBytes(guid2, 2, 5);
                packet.Translator.ReadInt32("Attack power");
                packet.Translator.ReadInt32("Spell power");
                packet.Translator.ReadXORBytes(guid2, 6, 4, 3, 1);
                packet.Translator.ReadInt32("Current Health");
                packet.Translator.WriteGuid("PowerUnitGUID", guid2);
            }
            packet.Translator.ParseBitStream(guid, 0, 5, 7, 2, 1, 4, 3, 6);
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

        [Parser(Opcode.CMSG_CAST_SPELL)]
        public static void HandleCastSpell(Packet packet)
        {
            var counter = packet.Translator.ReadBits(2);
            var unkBit = !packet.Translator.ReadBit();
            for (var i = 0; i < counter; ++i)
                packet.Translator.ReadBits("unk value0", 2, i);

            var hasCastCount = !packet.Translator.ReadBit();
            packet.Translator.ReadBit("Fake bit? Has TargetGUID"); // TargetGUID
            var hasbit1C = !packet.Translator.ReadBit();
            var hasMovment = packet.Translator.ReadBit();
            var hasbit78 = !packet.Translator.ReadBit();
            var hasbitF8 = !packet.Translator.ReadBit();
            var hasGUID2 = packet.Translator.ReadBit();
            var hasbitFC = !packet.Translator.ReadBit();
            var hasbit18 = !packet.Translator.ReadBit();
            var hasGUID3 = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Fake bit? Has GUID0"); // GUID0
            var hasSpellId = !packet.Translator.ReadBit();

            var guid2 = new byte[8];
            var guid3 = new byte[8];

            var guid0 = packet.Translator.StartBitStream(0, 5, 1, 7, 4, 3, 6, 2);
            if (hasGUID3)
                guid3 = packet.Translator.StartBitStream(2, 5, 3, 7, 4, 1, 0, 6);

            if (hasGUID2)
                guid2 = packet.Translator.StartBitStream(6, 2, 4, 7, 3, 5, 0, 1);

            var targetGUID = packet.Translator.StartBitStream(3, 0, 2, 7, 6, 4, 1, 5);

            if (unkBit)
                packet.Translator.ReadBitsE<CastFlag>("Cast Flags", 20);

            if (hasbit1C)
                packet.Translator.ReadBits("hasbit1C", 5);

            uint len78 = 0;
            if (hasbit78)
                len78 = packet.Translator.ReadBits("hasbit78", 7);
            packet.Translator.ResetBitReader();

            for (var i = 0; i < counter; ++i)
            {
                packet.Translator.ReadInt32("unk value1", i);
                packet.Translator.ReadInt32("unk value2", i);
            }

            if (hasGUID3)
            {
                var pos = new Vector3();
                packet.Translator.ReadXORBytes(guid3, 7, 5, 3);
                pos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORBytes(guid3, 0, 2, 1, 4, 6);
                pos.Z = packet.Translator.ReadSingle();
                pos.Y = packet.Translator.ReadSingle();
                packet.Translator.WriteGuid("GUID3", guid3);
                packet.AddValue("Position", pos);
            }

            packet.Translator.ParseBitStream(targetGUID, 2, 0, 5, 6, 7, 3, 4, 1);
            packet.Translator.WriteGuid("Target GUID", targetGUID);

            if (hasGUID2)
            {
                var pos = new Vector3();
                packet.Translator.ReadXORBytes(guid2, 5, 7);
                pos.Y = packet.Translator.ReadSingle();
                pos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORBytes(guid2, 3, 1);
                pos.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadXORBytes(guid2, 2, 6, 4, 0);
                packet.Translator.WriteGuid("GUID2", guid2);
                packet.AddValue("Position", pos);
            }

            packet.Translator.ParseBitStream(guid0, 7, 2, 6, 4, 1, 0, 3, 5);
            packet.Translator.WriteGuid("GUID0", guid0);

            if (hasbit78)
                packet.Translator.ReadWoWString("String", (int)len78);

            if (hasCastCount)
                packet.Translator.ReadByte("Cast Count");

            if (hasbit18)
                packet.Translator.ReadInt32("Int18");

            if (hasMovment)
                MovementHandler.ReadClientMovementBlock(packet);

            if (hasSpellId)
                packet.Translator.ReadInt32<SpellId>("SpellID");

            if (hasbitF8)
                packet.Translator.ReadSingle("FloatF8");

            if (hasbitFC)
                packet.Translator.ReadSingle("FloatFC");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_SPELL_START)]
        public static void HandleSpellStart(Packet packet)
        {
            //var CasterGUID1 = new byte[8]; // 14
            var casterGUID2 = new byte[8]; // 112-119
            var guid3 = new byte[8]; // 24-31
            var guid4 = new byte[8]; // 16-23
            //var guid5 = new byte[8]; // 98
            var guid6 = new byte[8]; // 416-423
            var destinationTransportGUID = new byte[8]; // 168-175
            var sourceTransportGUID = new byte[8]; // 136-143
            //var guid9 = new byte[8]; // 18
            var targetGUID = new byte[8];
            var powerUnitGUID = new byte[8];

            var bits52 = packet.Translator.ReadBits(24);
            var casterGUID1 = new byte[bits52][];
            for (var i = 0; i < bits52; ++i)
            {
                casterGUID1[i] = new byte[8];
                packet.Translator.StartBitStream(casterGUID1[i], 2, 5, 4, 7, 6, 0, 3, 1);
            }

            packet.Translator.ReadBit("bit28");
            var bit106 = !packet.Translator.ReadBit();
            packet.Translator.ReadBit("bit30");

            packet.Translator.StartBitStream(casterGUID2, 5, 4, 7, 1, 0, 6, 3, 2);
            packet.Translator.StartBitStream(guid4, 5, 6);
            guid3[2] = packet.Translator.ReadBit();
            var bit372 = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(targetGUID, 0, 3, 1, 5, 6, 2, 7, 4);

            var hasPowerData = packet.Translator.ReadBit("Has Power Data"); // bit432
            uint powerTypeCount = 0;
            if (hasPowerData)
            {
                packet.Translator.StartBitStream(powerUnitGUID, 6, 7, 3, 5, 0, 4, 2, 1);
                powerTypeCount = packet.Translator.ReadBits("Power Type Count", 21);
            }
            packet.Translator.StartBitStream(guid3, 6, 0);
            var bit102 = !packet.Translator.ReadBit();
            var bit101 = !packet.Translator.ReadBit();
            guid4[0] = packet.Translator.ReadBit();
            var bits84 = packet.Translator.ReadBits(25);
            guid3[7] = packet.Translator.ReadBit();
            var hasTargetFlags = !packet.Translator.ReadBit();
            var bit368 = !packet.Translator.ReadBit();
            var hasRuneStateBefore = !packet.Translator.ReadBit();
            guid4[4] = packet.Translator.ReadBit();
            guid3[4] = packet.Translator.ReadBit();
            var bit91 = !packet.Translator.ReadBit();
            var ExtraTargetsCount = packet.Translator.ReadBits("Extra Targets Count", 20);

            var ExtraTargetsGUID = new byte[ExtraTargetsCount][];
            for (var i = 0; i < ExtraTargetsCount; i++)
            {
                ExtraTargetsGUID[i] = new byte[8];
                packet.Translator.StartBitStream(ExtraTargetsGUID[i], 0, 5, 2, 7, 6, 4, 3, 1);

            }
            packet.Translator.ReadBit("bit104");
            var bit90 = !packet.Translator.ReadBit();

            for (var i = 0; i < bits84; ++i)
            {
                if (packet.Translator.ReadBits("bits22[0]", 4, i) == 11)
                    packet.Translator.ReadBits("bits22[1]", 4, i);
            }

            packet.Translator.StartBitStream(guid6, 2, 1, 7, 0, 6, 3, 5, 4);
            guid4[7] = packet.Translator.ReadBit();
            var HasDestinationData = packet.Translator.ReadBit();
            var hasSourceData = packet.Translator.ReadBit();
            guid4[2] = packet.Translator.ReadBit();

            if (HasDestinationData)
                packet.Translator.StartBitStream(destinationTransportGUID, 3, 7, 1, 0, 5, 6, 4, 2);

            guid4[3] = packet.Translator.ReadBit();
            var bit89 = !packet.Translator.ReadBit();

            if (hasSourceData)
                packet.Translator.StartBitStream(sourceTransportGUID, 5, 4, 3, 2, 0, 6, 7, 1);

            var bit48 = packet.Translator.ReadBit();

            if (hasTargetFlags)
                packet.Translator.ReadBitsE<TargetFlag>("Target Flags", 20);

            var hasRuneStateAfter = !packet.Translator.ReadBit();

            int bits48 = 0;
            if (!bit48)
                bits48 = (int)packet.Translator.ReadBits("bits48", 7);

            var hasPredictedType = !packet.Translator.ReadBit("bit428");
            guid3[3] = packet.Translator.ReadBit();
            var bits68 = packet.Translator.ReadBits("bits68", 24);
            guid3[1] = packet.Translator.ReadBit();

            var guid9 = new byte[bits68][];
            for (var i = 0; i < bits68; ++i)
            {
                guid9[i] = new byte[8];
                packet.Translator.StartBitStream(guid9[i], 3, 1, 2, 7, 5, 6, 4, 0);
            }

            var bit384 = !packet.Translator.ReadBit("bit384");
            guid3[5] = packet.Translator.ReadBit();
            guid4[1] = packet.Translator.ReadBit();
            var bits320 = packet.Translator.ReadBits("bits320", 21);
            var RuneCooldownCount = packet.Translator.ReadBits("Rune Cooldown Count", 3);
            packet.Translator.ReadBits("bits11", 12);

            if (HasDestinationData)
            {
                var pos = new Vector3();
                packet.Translator.ReadXORBytes(destinationTransportGUID, 0, 1);
                pos.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadXORBytes(destinationTransportGUID, 5, 3);
                pos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(destinationTransportGUID, 2);
                pos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORBytes(destinationTransportGUID, 6, 7, 4);
                packet.Translator.WriteGuid("Destination Transport GUID", destinationTransportGUID);
                packet.AddValue("Destination Position", pos);
            }
            packet.Translator.ParseBitStream(targetGUID, 1, 0, 5, 2, 3, 4, 7, 6);
            packet.Translator.ReadXORByte(guid4, 3);
            packet.Translator.WriteGuid("TargetGUID", targetGUID);
            if (bit372)
            {
                packet.Translator.ReadUInt32("unk95");
                packet.Translator.ReadUInt32("unk94");
            }

            for (var i = 0; i < bits68; ++i)
            {
                packet.Translator.ParseBitStream(guid9[i], 1, 4, 5, 6, 2, 7, 3, 0);
                packet.Translator.WriteGuid("GUID9", guid9[i], i);
            }

            for (var i = 0; i < bits52; ++i)
            {
                packet.Translator.ParseBitStream(casterGUID1[i], 0, 4, 3, 2, 7, 1, 5, 6);
                packet.Translator.WriteGuid("CasterGUID1", casterGUID1[i], i);
            }

            packet.Translator.ParseBitStream(casterGUID2, 4, 5, 7, 0, 1, 3, 2, 6);
            packet.Translator.WriteGuid("CasterGUID2", casterGUID2);

            for (var i = 0; i < bits320; ++i)
            {
                packet.Translator.ReadByteE<PowerType>("Power Type", i);
                packet.Translator.ReadInt32("Power Value", i);
            }

            if (hasPowerData)
            {
                packet.Translator.ReadXORByte(powerUnitGUID, 0);
                packet.Translator.ReadInt32("Current Health");
                packet.Translator.ReadXORByte(powerUnitGUID, 2);
                packet.Translator.ReadInt32("Spell power");
                packet.Translator.ReadXORByte(powerUnitGUID, 5);
                packet.Translator.ReadInt32("Attack power");
                packet.Translator.ReadXORByte(powerUnitGUID, 1);

                for (var i = 0; i < powerTypeCount; ++i)
                {
                    packet.Translator.ReadUInt32E<PowerType>("Power Type", i);
                    packet.Translator.ReadInt32("Power Value", i);
                }
                packet.Translator.ReadXORBytes(powerUnitGUID, 6, 7, 4, 3);
                packet.Translator.WriteGuid("PowerUnitGUID", powerUnitGUID);
            }

            if (bit89)
                packet.Translator.ReadUInt32("unk89");

            packet.Translator.ParseBitStream(guid6, 1, 7, 4, 3, 5, 2, 0, 6);
            packet.Translator.WriteGuid("GUID6", guid6);
            packet.Translator.ReadXORBytes(guid3, 3, 4);

            if (hasSourceData)
            {
                var pos = new Vector3();
                packet.Translator.ReadXORBytes(sourceTransportGUID, 2, 4, 1);
                pos.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadXORBytes(sourceTransportGUID, 0, 5, 3);
                pos.X = packet.Translator.ReadSingle();
                pos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORBytes(sourceTransportGUID, 7, 6);
                packet.Translator.WriteGuid("Source Transport GUID", sourceTransportGUID);
                packet.AddValue("Source Position", pos);
            }

            for (var i = 0; i < RuneCooldownCount; ++i)
                packet.Translator.ReadByte("Rune Cooldown Passed", i);

            for (var i = 0; i < ExtraTargetsCount; ++i)
            {
                var pos = new Vector3();
                packet.Translator.ReadXORBytes(ExtraTargetsGUID[i], 4, 2);
                pos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORBytes(ExtraTargetsGUID[i], 5, 7, 0);
                pos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORBytes(ExtraTargetsGUID[i], 1, 3, 6);
                pos.Z = packet.Translator.ReadSingle();
                packet.Translator.WriteGuid("Extra Target GUID", ExtraTargetsGUID[i], i);
                packet.AddValue("Position", pos, i);
            }
            packet.Translator.ReadXORByte(guid4, 2);

            if (hasRuneStateBefore)
                packet.Translator.ReadByte("Rune State Before");

            if (bit90)
                packet.Translator.ReadSingle("float90");

            packet.Translator.ReadInt32E<CastFlag>("Cast Flags");
            packet.Translator.ReadXORByte(guid3, 2);
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.ReadXORByte(guid4, 0);
            packet.Translator.ReadXORByte(guid3, 5);

            if (hasPredictedType)
                packet.Translator.ReadByte("Predicted Type");

            packet.Translator.ReadXORBytes(guid3, 0, 6, 1);
            packet.Translator.ReadXORByte(guid4, 1);

            if (hasRuneStateAfter)
                packet.Translator.ReadByte("Rune State After");

            if (bit101)
                packet.Translator.ReadUInt32("unk101");

            packet.Translator.ReadXORByte(guid4, 4);

            if (bit368)
                packet.Translator.ReadByte("byte368");

            if (bit384)
                packet.Translator.ReadByte("byte384");

            packet.Translator.ReadWoWString("String48:", bits48);

            packet.Translator.ReadXORByte(guid4, 7);
            packet.Translator.ReadByte("Cast Count");

            if (bit102)
                packet.Translator.ReadUInt32("unk102");

            packet.Translator.ReadXORByte(guid4, 6);

            if (bit106)
                packet.Translator.ReadUInt32("Heal");

            packet.Translator.ReadUInt32("Cast time");
            packet.Translator.ReadXORByte(guid4, 5);

            if (bit91)
                packet.Translator.ReadUInt32("unk91");

            packet.Translator.ReadXORByte(guid3, 7);
            packet.Translator.WriteGuid("GUID3", guid3);
            packet.Translator.WriteGuid("GUID4", guid4);
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

        [Parser(Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellGo(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];
            var SourceTransportGUID = new byte[8];
            byte[][] guid4;
            byte[][] guid5;
            var DestinationTransportGUID = new byte[8];
            byte[][] guid7;
            var guid8 = new byte[8];
            var guid9 = new byte[8];
            var guid10 = new byte[8];
            var guid11 = new byte[8];

            uint powerCount = 0;

            guid10[5] = packet.Translator.ReadBit();
            var counter = packet.Translator.ReadBits(24);
            var hasPredictedType = !packet.Translator.ReadBit();
            var bit404 = !packet.Translator.ReadBit();
            packet.Translator.ReadBit("field_A8");
            guid11[6] = packet.Translator.ReadBit();
            var bit432 = !packet.Translator.ReadBit();
            var counter3 = packet.Translator.ReadBits(20);
            packet.Translator.StartBitStream(guid, 2, 1, 0, 4, 5, 6, 3, 7);
            packet.Translator.StartBitStream(guid10, 0, 1, 3);

            guid7 = new byte[counter][];
            for (var i = 0; i < counter; ++i)
            {
                guid7[i] = new byte[8];
                packet.Translator.StartBitStream(guid7[i], 5, 6, 4, 7, 1, 2, 3, 0);
            }

            var hasCastSchoolImmunities = !packet.Translator.ReadBit();
            guid11[5] = packet.Translator.ReadBit();
            var hasRuneStateBefore = !packet.Translator.ReadBit();
            var powerLeftSelf = packet.Translator.ReadBits(21);
            var bit408 = !packet.Translator.ReadBit();
            var counter2 = packet.Translator.ReadBits(24);
            var hasPowerData = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid10, 6, 2);
            guid11[7] = packet.Translator.ReadBit();
            var RuneCooldownCount = packet.Translator.ReadBits(3);
            var hasTargetFlags = !packet.Translator.ReadBit();
            var hasSourceData = packet.Translator.ReadBit();

            if (hasPowerData)
            {
                packet.Translator.StartBitStream(guid2, 7, 4, 5, 0, 2, 6, 3, 1);
                powerCount = packet.Translator.ReadBits(21);
            }

            var bit412 = !packet.Translator.ReadBit();

            if (hasSourceData)
                packet.Translator.StartBitStream(SourceTransportGUID, 6, 3, 0, 1, 4, 5, 2, 7);

            guid11[1] = packet.Translator.ReadBit();

            guid4 = new byte[counter2][];
            for (var i = 0; i < counter2; ++i)
            {
                guid4[i] = new byte[8];
                packet.Translator.StartBitStream(guid4[i], 6, 4, 1, 7, 5, 2, 3, 0);
            }

            guid5 = new byte[counter3][];
            for (var i = 0; i < counter3; ++i)
            {
                guid5[i] = new byte[8];
                packet.Translator.StartBitStream(guid5[i], 7, 6, 5, 0, 4, 3, 1, 2);
            }
            var bit416 = !packet.Translator.ReadBit();
            guid11[0] = packet.Translator.ReadBit();
            packet.Translator.ReadBits("int5C", 12);
            packet.Translator.StartBitStream(guid10, 7, 4);
            var hasDestinationData = packet.Translator.ReadBit();

            if (hasDestinationData)
                packet.Translator.StartBitStream(DestinationTransportGUID, 0, 2, 7, 6, 1, 4, 3, 5);

            var hasRuneStateAfter = !packet.Translator.ReadBit();
            var hasCastImmunities = !packet.Translator.ReadBit();
            guid11[3] = packet.Translator.ReadBit();
            var bit420 = packet.Translator.ReadBit();
            var hasPredictedSpellId = !packet.Translator.ReadBit();
            guid11[4] = packet.Translator.ReadBit();
            var unkflag27 = packet.Translator.ReadBit();

            int bits7 = 0;
            if (!unkflag27)
                bits7 = (int)packet.Translator.ReadBits(7);

            var counter4 = packet.Translator.ReadBits(25);
            guid11[2] = packet.Translator.ReadBit();

            for (var i = 0; i < counter4; ++i)
            {
                var bits136 = packet.Translator.ReadBits("bits136", 4);

                if (bits136 == 11)
                    packet.Translator.ReadBits("bits140", 4);
            }

            packet.Translator.ReadBit("unk464");
            packet.Translator.StartBitStream(guid8, 7, 3, 6, 4, 2, 5, 0, 1);
            packet.Translator.ReadBit("unk160");
            packet.Translator.StartBitStream(guid9, 3, 5, 2, 1, 4, 0, 6, 7);

            if (hasTargetFlags)
                packet.Translator.ReadBitsE<TargetFlag>("Target Flags", 20);

            if (hasSourceData)
            {
                var pos = new Vector3();
                packet.Translator.ReadXORBytes(SourceTransportGUID, 3, 7, 4);
                pos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORBytes(SourceTransportGUID, 6, 5, 2);
                pos.Z = packet.Translator.ReadSingle();
                pos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORBytes(SourceTransportGUID, 0, 1);
                packet.Translator.WriteGuid("Source Transport GUID", SourceTransportGUID);
                packet.AddValue("Source Position", pos);
            }


            for (var i = 0; i < counter3; ++i)
            {
                var pos = new Vector3();
                packet.Translator.ReadXORBytes(guid5[i], 4, 0, 7, 6, 2, 1);
                pos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORBytes(guid5[i], 5, 3);
                pos.Y = packet.Translator.ReadSingle();
                pos.Z = packet.Translator.ReadSingle();
                packet.Translator.WriteGuid("GUID5", guid5[i], i);
                packet.AddValue("Position", pos);
            }

            for (var i = 0; i < counter; ++i)
            {
                packet.Translator.ParseBitStream(guid7[i], 2, 1, 5, 3, 7, 0, 4, 6);
                packet.Translator.WriteGuid("GUID7", guid7[i], i);
            }

            if (hasDestinationData)
            {
                var pos = new Vector3();
                packet.Translator.ReadXORBytes(DestinationTransportGUID, 1, 4);
                pos.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadXORBytes(DestinationTransportGUID, 0, 7, 3, 5);
                pos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(DestinationTransportGUID, 6);
                pos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(DestinationTransportGUID, 2);
                packet.Translator.WriteGuid("Destination Transport GUID", DestinationTransportGUID);
                packet.AddValue("Destination Position", pos);
            }
            if (bit408)
                packet.Translator.ReadSingle("float198");

            if (bit416)
                packet.Translator.ReadByte("unk.416");

            packet.Translator.ParseBitStream(guid8, 5, 0, 2, 7, 6, 3, 4, 1);
            packet.Translator.WriteGuid("GUID8", guid8);

            for (var i = 0; i < counter2; ++i)
            {
                packet.Translator.ParseBitStream(guid4[i], 6, 3, 1, 0, 2, 4, 7, 5);
                packet.Translator.WriteGuid("GUID4", guid4[i], i);
            }

            if (hasPowerData)
            {
                packet.Translator.ReadXORByte(guid2, 3);
                packet.Translator.ReadUInt32("Spell Power");
                packet.Translator.ReadXORBytes(guid2, 6, 4, 7, 0);
                packet.Translator.ReadUInt32("Attack Power");
                packet.Translator.ReadXORByte(guid2, 2);

                for (var i = 0; i < powerCount; ++i)
                {
                    packet.Translator.ReadUInt32("uint32 60");
                    packet.Translator.ReadUInt32("uint32 48");
                }

                packet.Translator.ReadUInt32("Current Health");
                packet.Translator.ReadXORBytes(guid2, 1, 5);
                packet.Translator.WriteGuid("GUID2", guid2);
            }

            packet.Translator.ParseBitStream(guid9, 2, 3, 4, 7, 5, 1, 6, 0);
            packet.Translator.WriteGuid("GUID9", guid9);

            packet.Translator.ReadByte("Cast Count");

            packet.Translator.ParseBitStream(guid, 0, 1, 4, 5, 7, 6, 2, 3);
            packet.Translator.WriteGuid("GUID", guid);

            packet.Translator.ReadXORByte(guid10, 7);
            if (hasPredictedType)
                packet.Translator.ReadByte("Predicted Type");

            if (hasPredictedSpellId)
                packet.Translator.ReadUInt32("Predicted Spell Id");

            packet.Translator.ReadXORByte(guid10, 3);

            packet.Translator.ReadBytes("Bytes", bits7);

            if (bit404)
                packet.Translator.ReadUInt32("uint32 404");

            if (hasCastImmunities)
                packet.Translator.ReadUInt32("Cast Immunities");

            packet.Translator.ReadXORByte(guid10, 2);
            packet.Translator.ReadXORByte(guid11, 0);

            if (bit420)
            {
                packet.Translator.ReadUInt32("uint32 424");
                packet.Translator.ReadUInt32("uint32 428");
            }

            if (bit412)
                packet.Translator.ReadUInt32("uint32 412");

            packet.Translator.ReadXORByte(guid11, 2);
            packet.Translator.ReadXORBytes(guid10, 0, 1);
            packet.Translator.ReadXORByte(guid11, 3);

            for (var i = 0; i < RuneCooldownCount; ++i)
                packet.Translator.ReadByte("Rune Cooldown Passed", i);

            if (hasRuneStateAfter)
                packet.Translator.ReadByte("Rune State After");

            packet.Translator.ReadXORByte(guid11, 1);
            packet.Translator.ReadXORByte(guid10, 4);
            packet.Translator.ReadInt32<SpellId>("Spell ID");

            if (hasRuneStateBefore)
                packet.Translator.ReadByte("Rune State Before");

            packet.Translator.ReadXORByte(guid10, 6);
            packet.Translator.ReadXORByte(guid11, 5);
            packet.Translator.ReadUInt32("uint32 88"); // field_58
            packet.Translator.ReadXORByte(guid11, 4);
            packet.Translator.ReadUInt32("uint32 96"); // field_60

            for (var i = 0; i < powerLeftSelf; ++i)
            {
                packet.Translator.ReadInt32("Power Value", i);
                packet.Translator.ReadByteE<PowerType>("Power Type", i);
            }
            packet.Translator.ReadXORByte(guid10, 5);
            packet.Translator.ReadXORByte(guid11, 6);

            if (hasCastSchoolImmunities)
                packet.Translator.ReadUInt32("Cast School Immunities");

            packet.Translator.ReadXORByte(guid11, 7);

            if (bit432)
                packet.Translator.ReadByte("unk432");

            packet.Translator.WriteGuid("GUID10", guid10);
            packet.Translator.WriteGuid("GUID11", guid11);
        }

        [Parser(Opcode.SMSG_UNLEARNED_SPELLS)]
        public static void HandleRemovedSpell2(Packet packet)
        {
            var count = packet.Translator.ReadBits(22);
            for (var i = 0; i < count; ++i)
                packet.Translator.ReadUInt32<SpellId>("Spell ID", i);
        }
    }
}

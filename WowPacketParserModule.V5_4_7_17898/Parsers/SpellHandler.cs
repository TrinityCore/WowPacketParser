using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
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

            guid2[3] = packet.Translator.ReadBit();
            bit18 = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            bits0 = (int)packet.Translator.ReadBits(24);
            guid2[7] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();

            var hasCasterGUID = new bool[bits0];
            var hasDuration = new bool[bits0];
            var hasMaxDuration = new bool[bits0];
            var hasAura = new bool[bits0];
            var effectCount = new uint[bits0];
            var bits48 = new uint[bits0];
            var casterGUID = new byte[bits0][];

            for (var i = 0; i < bits0; ++i)
            {
                hasAura[i] = packet.Translator.ReadBit();
                if (hasAura[i])
                {
                    hasMaxDuration[i] = packet.Translator.ReadBit();
                    bits48[i] = packet.Translator.ReadBits(22);
                    hasDuration[i] = packet.Translator.ReadBit();
                    hasCasterGUID[i] = packet.Translator.ReadBit();
                    effectCount[i] = packet.Translator.ReadBits(22);

                    if (hasCasterGUID[i])
                    {
                        casterGUID[i] = new byte[8];
                        packet.Translator.StartBitStream(casterGUID[i], 1, 6, 0, 7, 5, 3, 2, 4);
                    }
                }
            }

            guid2[2] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();

            var auras = new List<Aura>();
            for (var i = 0; i < bits0; ++i)
            {
                if (hasAura[i])
                {
                    var aura = new Aura();
                    if (hasCasterGUID[i])
                    {
                        packet.Translator.ParseBitStream(casterGUID[i], 2, 5, 6, 7, 0, 1, 4, 3);
                        packet.Translator.WriteGuid("Caster GUID", casterGUID[i], i);
                        aura.CasterGuid = new WowGuid64(BitConverter.ToUInt64(casterGUID[i], 0));
                    }

                    for (var j = 0; j < bits48[i]; ++j)
                        packet.Translator.ReadSingle("FloatEM", i, j);

                    for (var j = 0; j < effectCount[i]; ++j)
                        packet.Translator.ReadSingle("Effect Value", i, j);

                    packet.Translator.ReadInt32("Effect Mask", i);
                    aura.AuraFlags = packet.Translator.ReadByteE<AuraFlagMoP>("Flags", i);
                    var id = packet.Translator.ReadInt32<SpellId>("Spell ID", i);
                    aura.SpellId = (uint)id;
                    aura.Level = packet.Translator.ReadUInt16("Caster Level", i);
                    aura.Charges = packet.Translator.ReadByte("Charges", i);

                    aura.MaxDuration = hasMaxDuration[i] ? packet.Translator.ReadInt32("Max Duration", i) : 0;

                    aura.Duration = hasDuration[i] ? packet.Translator.ReadInt32("Duration", i) : 0;
                }
                packet.Translator.ReadByte("Slot", i);
            }
            packet.Translator.ParseBitStream(guid2, 0, 1, 3, 4, 2, 6, 7, 5);
            packet.Translator.WriteGuid("GUID2", guid2);
        }

        [Parser(Opcode.CMSG_CAST_SPELL)]
        public static void HandleCastSpell(Packet packet)
        {
            var guid5 = new byte[8];
            var guid6 = new byte[8];
            var guid7 = new byte[8];
            var guidB = new byte[8];
            var guid20 = new byte[8];
            var guid25 = new byte[8];

            var hasMovementFlags = false;
            var hasMovementFlags2 = false;
            var hasTimestamp = false;
            var hasOrientation = false;
            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasTransport = false;
            var hasPitch = false;
            var hasFallDirection = false;
            var hasFallData = false;
            var hasSplineElevation = false;
            var bit184 = false;
            var bit185 = false;
            var hasUnkMovementField = false;
            var bit19C = false;

            var unkMovementLoopCounter = 0;

            var hasCastCount = !packet.Translator.ReadBit();
            var hasSrcLocation = packet.Translator.ReadBit();
            var hasTargetString = !packet.Translator.ReadBit();
            var hasTargetMask = !packet.Translator.ReadBit();
            var hasSpellId = !packet.Translator.ReadBit();
            var hasCastFlags = !packet.Translator.ReadBit();
            var hasDestLocation = packet.Translator.ReadBit();
            packet.Translator.ReadBit(); // fake bit?
            var archeologyCounter = packet.Translator.ReadBits(2);
            var hasMovement = packet.Translator.ReadBit();
            packet.Translator.ReadBit(); // fake bit?
            var hasGlyphIndex = !packet.Translator.ReadBit();

            for (var i = 0; i < archeologyCounter; ++i)
                packet.Translator.ReadBits("archeologyType", 2, i);

            var hasElevation = !packet.Translator.ReadBit();
            var hasMissileSpeed = !packet.Translator.ReadBit();

            if (hasDestLocation)
                packet.Translator.StartBitStream(guidB, 1, 2, 4, 3, 7, 6, 5, 0);

            if (hasMovement)
            {
                hasMovementFlags = !packet.Translator.ReadBit();
                guid20[5] = packet.Translator.ReadBit();

                if (hasMovementFlags)
                    packet.Translator.ReadBits("hasMovementFlags", 30);

                hasSplineElevation = !packet.Translator.ReadBit();
                guid20[0] = packet.Translator.ReadBit();
                guid20[2] = packet.Translator.ReadBit();
                hasFallData = packet.Translator.ReadBit();
                hasPitch = !packet.Translator.ReadBit();
                bit19C = packet.Translator.ReadBit();
                bit185 = packet.Translator.ReadBit();
                guid20[7] = packet.Translator.ReadBit();
                hasTimestamp = !packet.Translator.ReadBit();
                unkMovementLoopCounter = (int)packet.Translator.ReadBits(22);
                guid20[1] = packet.Translator.ReadBit();

                if (hasFallData)
                    hasFallDirection = packet.Translator.ReadBit();

                hasOrientation = !packet.Translator.ReadBit();
                hasUnkMovementField = !packet.Translator.ReadBit();
                guid20[6] = packet.Translator.ReadBit();
                hasMovementFlags2 = !packet.Translator.ReadBit();

                if (hasMovementFlags2)
                    packet.Translator.ReadBits("hasMovementFlags2", 13);

                hasTransport = packet.Translator.ReadBit();
                bit184 = packet.Translator.ReadBit();

                if (hasTransport)
                {
                    guid25[5] = packet.Translator.ReadBit();
                    guid25[0] = packet.Translator.ReadBit();
                    hasTransportTime2 = packet.Translator.ReadBit();
                    hasTransportTime3 = packet.Translator.ReadBit();
                    guid25[2] = packet.Translator.ReadBit();
                    guid25[1] = packet.Translator.ReadBit();
                    guid25[6] = packet.Translator.ReadBit();
                    guid25[7] = packet.Translator.ReadBit();
                    guid25[4] = packet.Translator.ReadBit();
                    guid25[3] = packet.Translator.ReadBit();
                }

                guid20[4] = packet.Translator.ReadBit();
                guid20[3] = packet.Translator.ReadBit();
            }

            packet.Translator.StartBitStream(guid5, 4, 7, 1, 0, 3, 6, 2, 5);

            if (hasSrcLocation)
                packet.Translator.StartBitStream(guid7, 1, 3, 7, 5, 0, 2, 4, 6);

            packet.Translator.StartBitStream(guid6, 0, 5, 7, 1, 4, 6, 2, 3);

            if (hasCastFlags)
                packet.Translator.ReadBits("hasCastFlags", 5);

            if (hasTargetString)
                packet.Translator.ReadBits("hasTargetString", 7);

            if (hasTargetMask)
                packet.Translator.ReadBitsE<TargetFlag>("Target Flags", 20);

            for (var i = 0; i < archeologyCounter; ++i)
            {
                packet.Translator.ReadUInt32("unk1", i);
                packet.Translator.ReadUInt32("unk2", i);
            }

            if (hasGlyphIndex)
                packet.Translator.ReadInt32("hasGlyphIndex");

            packet.Translator.ParseBitStream(guid6, 2, 7, 4, 3, 1, 0, 6, 5);


            if (hasSrcLocation)
            {
                packet.Translator.ReadXORByte(guid7, 3);
                packet.Translator.ReadXORByte(guid7, 4);
                packet.Translator.ReadSingle("Position Z");
                packet.Translator.ReadXORByte(guid7, 1);
                packet.Translator.ReadXORByte(guid7, 0);
                packet.Translator.ReadXORByte(guid7, 2);
                packet.Translator.ReadXORByte(guid7, 7);
                packet.Translator.ReadSingle("Position X");
                packet.Translator.ReadXORByte(guid7, 6);
                packet.Translator.ReadXORByte(guid7, 5);
                packet.Translator.ReadSingle("Position Y");

                packet.Translator.WriteGuid("Guid7", guid7);
            }

            if (hasDestLocation)
            {
                packet.Translator.ReadSingle("Position X");
                packet.Translator.ReadSingle("Position Y");
                packet.Translator.ReadXORByte(guidB, 6);
                packet.Translator.ReadXORByte(guidB, 7);
                packet.Translator.ReadXORByte(guidB, 0);
                packet.Translator.ReadXORByte(guidB, 1);
                packet.Translator.ReadXORByte(guidB, 3);
                packet.Translator.ReadSingle("Position Z");
                packet.Translator.ReadXORByte(guidB, 5);
                packet.Translator.ReadXORByte(guidB, 4);
                packet.Translator.ReadXORByte(guidB, 2);

                packet.Translator.WriteGuid("GuidB", guidB);
            }

            if (hasMovement)
            {
                packet.Translator.ReadSingle("Position Z");

                if (hasFallData)
                {
                    packet.Translator.ReadSingle("Z Speed");

                    if (hasFallDirection)
                    {
                        packet.Translator.ReadSingle("XY Speed");
                        packet.Translator.ReadSingle("SinAngle");
                        packet.Translator.ReadSingle("CosAngle");
                    }

                    packet.Translator.ReadInt32("FallTime");
                }

                if (hasPitch)
                    packet.Translator.ReadSingle("Pitch");

                if (hasTransport)
                {
                    packet.Translator.ReadXORByte(guid25, 4);
                    packet.Translator.ReadSingle("Orientation");
                    packet.Translator.ReadInt32("Transport Time");
                    packet.Translator.ReadXORByte(guid25, 7);
                    packet.Translator.ReadSingle("Position Y");
                    packet.Translator.ReadXORByte(guid25, 1);

                    if (hasTransportTime2)
                        packet.Translator.ReadInt32("hasTransportTime2");

                    packet.Translator.ReadByte("Transport Seat");
                    packet.Translator.ReadSingle("Position Z");
                    packet.Translator.ReadXORByte(guid25, 6);
                    packet.Translator.ReadXORByte(guid25, 5);
                    packet.Translator.ReadXORByte(guid25, 0);
                    packet.Translator.ReadSingle("Position X");
                    packet.Translator.ReadXORByte(guid25, 2);
                    if (hasTransportTime3)
                        packet.Translator.ReadInt32("hasTransportTime3");
                    packet.Translator.ReadXORByte(guid25, 3);

                    packet.Translator.WriteGuid("Guid25", guid25);
                }

                packet.Translator.ReadXORByte(guid20, 6);
                packet.Translator.ReadXORByte(guid20, 7);
                packet.Translator.ReadXORByte(guid20, 0);
                packet.Translator.ReadXORByte(guid20, 5);
                packet.Translator.ReadXORByte(guid20, 2);

                if (hasSplineElevation)
                    packet.Translator.ReadSingle("SplineElevation");

                for (var i = 0; i < unkMovementLoopCounter; ++i)
                    packet.Translator.ReadInt32("MovementLoopCounter", i);

                packet.Translator.ReadSingle("Position Y");

                if (hasTimestamp)
                    packet.Translator.ReadInt32("hasTimestamp");

                if (hasUnkMovementField)
                    packet.Translator.ReadInt32("MovementField");

                packet.Translator.ReadXORByte(guid20, 1);
                packet.Translator.ReadSingle("Position X");

                if (hasOrientation)
                    packet.Translator.ReadSingle("Orientation");

                packet.Translator.ReadXORByte(guid20, 4);
                packet.Translator.ReadXORByte(guid20, 3);

                packet.Translator.WriteGuid("Guid20", guid20);
            }

            if (hasSpellId)
                packet.Translator.ReadInt32<SpellId>("Spell ID");

            packet.Translator.ParseBitStream(guid5, 7, 0, 1, 2, 3, 5, 6, 4);

            if (hasElevation)
                packet.Translator.ReadSingle("hasElevation");

            if (hasMissileSpeed)
                packet.Translator.ReadSingle("missileSpeed");

            if (hasCastCount)
                packet.Translator.ReadByte("Cast Count");

            packet.Translator.WriteGuid("Guid5", guid5);
            packet.Translator.WriteGuid("Guid6", guid6);
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
            var guid6 = new byte[8];
            var guid7 = new byte[8];
            var guid8 = new byte[8];

            var hasTargetFlags = !packet.Translator.ReadBit();
            var bits44 = packet.Translator.ReadBits(24);
            guid2[2] = packet.Translator.ReadBit();
            packet.Translator.ReadBit(); // fake bit
            guid3[2] = packet.Translator.ReadBit();
            var hasPredictedHeal = !packet.Translator.ReadBit();
            var hasRunesStateBefore = !packet.Translator.ReadBit();

            var guid9 = new byte[bits44][];
            for (var i = 0; i < bits44; ++i)
            {
                guid9[i] = new byte[8];
                packet.Translator.StartBitStream(guid9[i], 5, 2, 7, 0, 3, 6, 1, 4);
            }

            packet.Translator.ReadBit(); // fake bit
            guid2[3] = packet.Translator.ReadBit();
            var hitcountPos = (int)packet.Translator.ReadBits(24);
            guid3[5] = packet.Translator.ReadBit();
            var missTypeCount = (int)packet.Translator.ReadBits(25);
            var predictedPowerCount = (int)packet.Translator.ReadBits(21);
            var hasDelayTime = !packet.Translator.ReadBit();
            var extraTargetCount = (int)packet.Translator.ReadBits(20);
            var hasSourceLocation = packet.Translator.ReadBit();

            var guid10 = new byte[extraTargetCount][];
            for (var i = 0; i < extraTargetCount; ++i)
            {
                guid10[i] = new byte[8];
                packet.Translator.StartBitStream(guid10[i], 3, 0, 7, 1, 4, 6, 2, 5);
            }

            packet.Translator.StartBitStream(guid8, 1, 2, 4, 5, 7, 0, 3, 6);

            if (hasSourceLocation)
                packet.Translator.StartBitStream(guid6, 4, 3, 0, 5, 7, 1, 2, 6);

            guid3[1] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();

            packet.Translator.StartBitStream(guid4, 3, 2, 7, 6, 5, 4, 0, 1);

            guid2[1] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();

            for (var i = 0; i < missTypeCount; ++i)
            {
                if (packet.Translator.ReadBits("bits22[0]", 4, i) == 11)
                    packet.Translator.ReadBits("bits22[1]", 4, i);
            }

            var hasDestLocation = packet.Translator.ReadBit();

            var guid11 = new byte[hitcountPos][];
            for (var i = 0; i < hitcountPos; ++i)
            {
                guid11[i] = new byte[8];
                packet.Translator.StartBitStream(guid11[i], 0, 1, 6, 2, 3, 4, 7, 5);
            }

            guid3[7] = packet.Translator.ReadBit();
            if (hasDestLocation)
                packet.Translator.StartBitStream(guid7, 5, 0, 1, 7, 3, 6, 2, 4);

            guid3[3] = packet.Translator.ReadBit();
            var hasAmmoInventoryType = !packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            var hasSplineElevation = !packet.Translator.ReadBit();
            var hasElevation = !packet.Translator.ReadBit();
            var hasAmmoDisplayId = !packet.Translator.ReadBit();

            if (hasTargetFlags)
                packet.Translator.ReadBitsE<TargetFlag>("Target Flags", 20);

            var hasPredictedType = !packet.Translator.ReadBit();
            var hasUnkMovementField = !packet.Translator.ReadBit();
            var runeCooldownPassedCount = (int)packet.Translator.ReadBits(3);
            var hasCastSchoolImmunities = !packet.Translator.ReadBit();
            var hasVisualChain = packet.Translator.ReadBit();
            var hasRunesStateAfter = !packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid3[0] = packet.Translator.ReadBit();
            var bits2C = (int)packet.Translator.ReadBits(13);
            guid2[7] = packet.Translator.ReadBit();

            packet.Translator.ReadBit(); // fake bit
            packet.Translator.StartBitStream(guid5, 6, 4, 0, 3, 2, 1, 5, 7);

            guid3[6] = packet.Translator.ReadBit();
            guid3[4] = packet.Translator.ReadBit();

            var hasTargetString = !packet.Translator.ReadBit();
            var bitsC0 = 0u;
            if (hasTargetString)
                bitsC0 = packet.Translator.ReadBits(7);

            packet.Translator.ReadWoWString("StringC0", bitsC0);

            for (var i = 0; i < bits44; ++i)
            {
                packet.Translator.ParseBitStream(guid9[i], 4, 5, 6, 2, 0, 3, 1, 7);
                packet.Translator.WriteGuid("Guid9", guid9[i]);
            }

            packet.Translator.ParseBitStream(guid4, 7, 5, 2, 4, 0, 1, 3, 6);

            for (var i = 0; i < hitcountPos; ++i)
            {
                packet.Translator.ParseBitStream(guid11[i], 0, 7, 1, 4, 3, 5, 2, 6);
                packet.Translator.WriteGuid("Guid11", guid11[i]);
            }

            packet.Translator.ParseBitStream(guid8, 5, 3, 7, 1, 0, 2, 4, 6);

            if (hasVisualChain)
            {
                packet.Translator.ReadInt32("Int174");
                packet.Translator.ReadInt32("Int178");
            }

            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 1);

            if (hasSourceLocation)
            {
                packet.Translator.ReadXORByte(guid6, 2);
                packet.Translator.ReadSingle("Position Z");
                packet.Translator.ReadXORByte(guid6, 7);
                packet.Translator.ReadXORByte(guid6, 5);
                packet.Translator.ReadXORByte(guid6, 0);
                packet.Translator.ReadXORByte(guid6, 6);
                packet.Translator.ReadSingle("Posiion X");
                packet.Translator.ReadXORByte(guid6, 1);
                packet.Translator.ReadXORByte(guid6, 4);
                packet.Translator.ReadSingle("Position Y");
                packet.Translator.ReadXORByte(guid6, 3);
                packet.Translator.WriteGuid("Guid10", guid6);
            }

            if (hasCastSchoolImmunities)
                packet.Translator.ReadInt32("CastSchoolImmunities");

            packet.Translator.ReadXORByte(guid2, 2);

            packet.Translator.ParseBitStream(guid5, 0, 6, 7, 3, 5, 1, 4, 2);

            packet.Translator.ReadXORByte(guid2, 0);

            for (var i = 0; i < extraTargetCount; ++i)
            {
                packet.Translator.ReadSingle("Float188");

                packet.Translator.ReadXORByte(guid10[i], 2);
                packet.Translator.ReadXORByte(guid10[i], 6);
                packet.Translator.ReadXORByte(guid10[i], 1);

                packet.Translator.ReadSingle("Float188");
                packet.Translator.ReadSingle("Float188");

                packet.Translator.ReadXORByte(guid10[i], 5);
                packet.Translator.ReadXORByte(guid10[i], 0);
                packet.Translator.ReadXORByte(guid10[i], 7);
                packet.Translator.ReadXORByte(guid10[i], 3);
                packet.Translator.ReadXORByte(guid10[i], 4);

                packet.Translator.WriteGuid("guid999", guid10[i]);
            }

            packet.Translator.ReadXORByte(guid3, 7);
            packet.Translator.ReadXORByte(guid3, 1);

            if (hasDelayTime)
                packet.Translator.ReadInt32("hasDelayTime");

            packet.Translator.ReadInt32<SpellId>("Spell ID");

            if (hasSplineElevation)
                packet.Translator.ReadByte("Byte180");

            if (hasPredictedHeal)
                packet.Translator.ReadInt32("PredictedHeal");

            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid3, 2);

            if (hasUnkMovementField)
                packet.Translator.ReadInt32("hasUnkMovementField");

            if (hasDestLocation)
            {
                packet.Translator.ReadSingle("Position Z");
                packet.Translator.ReadXORByte(guid7, 3);
                packet.Translator.ReadSingle("Position X");
                packet.Translator.ReadXORByte(guid7, 4);
                packet.Translator.ReadXORByte(guid7, 5);
                packet.Translator.ReadXORByte(guid7, 1);
                packet.Translator.ReadSingle("Position Y");
                packet.Translator.ReadXORByte(guid7, 2);
                packet.Translator.ReadXORByte(guid7, 0);
                packet.Translator.ReadXORByte(guid7, 6);
                packet.Translator.ReadXORByte(guid7, 7);
                packet.Translator.WriteGuid("Guid7", guid7);
            }

            packet.Translator.ReadXORByte(guid2, 4);

            for (var i = 0; i < predictedPowerCount; ++i)
            {
                packet.Translator.ReadInt32("Value", i);
                packet.Translator.ReadByteE<PowerType>("Power type", i);
            }

            packet.Translator.ReadByte("CastCount");
            packet.Translator.ReadInt32E<CastFlag>("Cast Flags");

            if (hasPredictedType)
                packet.Translator.ReadByte("hasPredictedType");

            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadInt32("CastTime");

            if (hasRunesStateAfter)
                packet.Translator.ReadByte("hasRunesStateAfter");

            if (hasElevation)
                packet.Translator.ReadSingle("Elevation");

            packet.Translator.ReadXORByte(guid3, 3);

            if (hasRunesStateBefore)
                packet.Translator.ReadByte("hasRunesStateBefore");

            if (hasAmmoDisplayId)
                packet.Translator.ReadInt32("AmmoDisplayId");

            if (hasAmmoInventoryType)
                packet.Translator.ReadByte("AmmoInventoryType");

            packet.Translator.ReadXORByte(guid3, 4);

            for (var i = 0; i < runeCooldownPassedCount; ++i)
                packet.Translator.ReadByte("runeCooldownPassedCount", i);

            packet.Translator.ReadXORByte(guid3, 5);
            packet.Translator.ReadXORByte(guid3, 0);
            packet.Translator.ReadXORByte(guid3, 6);
            packet.Translator.ReadXORByte(guid2, 7);

            packet.Translator.WriteGuid("Guid2", guid2);
            packet.Translator.WriteGuid("Guid3", guid3);
            packet.Translator.WriteGuid("Guid4", guid4);
            packet.Translator.WriteGuid("Guid5", guid5);
            packet.Translator.WriteGuid("Guid8", guid8);
        }

        [Parser(Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellGo(Packet packet)
        {
            var bits2C = 0;
            var hitcountPos = 0;
            var bits44 = 0;
            var missTypeCount = 0;
            var bitsC0 = 0;
            var predictedPowerCount = 0;
            var runeCooldownPassedCount = 0;
            var extraTargetCount = 0;
            var bits1BC = 0;


            var guid2 = new byte[8];
            var guid3 = new byte[8];


            var guid6 = new byte[8];
            var guid7 = new byte[8];
            var guid8 = new byte[8];
            var guid9 = new byte[8];
            var guid10 = new byte[8];

            var bit1 = new bool[hitcountPos];
            var bit2 = new bool[hitcountPos];
            var bit3 = new bool[hitcountPos];
            var bit4 = new bool[hitcountPos];
            var bit5 = new bool[hitcountPos];
            var bit6 = new bool[hitcountPos];
            var bit7 = new bool[hitcountPos];
            var bit38 = new bool[hitcountPos];
            var bit48 = new bool[bits44];

            var counter = 0;

            packet.Translator.ReadBit(); // fake bit
            var hasDelayTime = !packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            var hasPredictedType = !packet.Translator.ReadBit();
            var hasTargetString = !packet.Translator.ReadBit(); // some flag
            guid2[3] = packet.Translator.ReadBit();
            guid3[3] = packet.Translator.ReadBit();
            var hasDestUnkByte = !packet.Translator.ReadBit();
            guid6[4] = packet.Translator.ReadBit();
            guid6[2] = packet.Translator.ReadBit();
            guid6[3] = packet.Translator.ReadBit();
            guid6[7] = packet.Translator.ReadBit();
            guid6[1] = packet.Translator.ReadBit();
            guid6[5] = packet.Translator.ReadBit();
            guid6[6] = packet.Translator.ReadBit();
            guid6[0] = packet.Translator.ReadBit();

            if (hasTargetString)
                counter = (int)packet.Translator.ReadBits(7);

            packet.Translator.ReadBit(); // fake bit
            guid2[4] = packet.Translator.ReadBit();
            var hasPredictedHeal = !packet.Translator.ReadBit();
            var hasTargetFlags = !packet.Translator.ReadBit();
            guid10[3] = packet.Translator.ReadBit();
            guid10[2] = packet.Translator.ReadBit();
            guid10[6] = packet.Translator.ReadBit();
            guid10[5] = packet.Translator.ReadBit();
            guid10[7] = packet.Translator.ReadBit();
            guid10[4] = packet.Translator.ReadBit();
            guid10[0] = packet.Translator.ReadBit();
            guid10[1] = packet.Translator.ReadBit();
            hitcountPos = (int)packet.Translator.ReadBits(24);
            guid2[2] = packet.Translator.ReadBit();
            guid3[4] = packet.Translator.ReadBit();
            guid3[2] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();

            var guid = new byte[hitcountPos][];
            for (var i = 0; i < hitcountPos; ++i)
            {
                guid[i] = new byte[8];
                packet.Translator.StartBitStream(guid[i], 1, 3, 0, 6, 4, 7, 5, 2);
            }

            guid3[1] = packet.Translator.ReadBit();
            var hasRunesStateAfter = !packet.Translator.ReadBit();
            guid3[7] = packet.Translator.ReadBit();
            var hasAmmoDisplayId = !packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            var hasAmmoInventoryType = !packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid3[0] = packet.Translator.ReadBit();
            bits44 = (int)packet.Translator.ReadBits(24);
            var hasCastSchoolImmunities = !packet.Translator.ReadBit();

            var guid4 = new byte[bits44][];
            for (var i = 0; i < bits44; ++i)
            {
                guid4[i] = new byte[8];
                packet.Translator.StartBitStream(guid4[i], 6, 7, 5, 3, 4, 1, 0, 2);
            }

            var hasRunesStateBefore = !packet.Translator.ReadBit();
            runeCooldownPassedCount = (int)packet.Translator.ReadBits(3);
            missTypeCount = (int)packet.Translator.ReadBits(25);
            bits2C = (int)packet.Translator.ReadBits(13);

            for (var i = 0; i < missTypeCount; ++i)
            {
                var bits136 = (int)packet.Translator.ReadBits(4);
                if (bits136 == 11)
                    packet.Translator.ReadBits("bits140", 4, i);
            }

            var hasVisualChain = packet.Translator.ReadBit();
            extraTargetCount = (int)packet.Translator.ReadBits(20);

            var guid5 = new byte[extraTargetCount][];
            for (var i = 0; i < extraTargetCount; ++i)
            {
                guid5[i] = new byte[8];
                packet.Translator.StartBitStream(guid5[i], 6, 4, 7, 0, 1, 2, 3, 5);
            }

            packet.Translator.ReadBit(); // fake bit
            guid7[1] = packet.Translator.ReadBit();
            guid7[4] = packet.Translator.ReadBit();
            guid7[5] = packet.Translator.ReadBit();
            guid7[6] = packet.Translator.ReadBit();
            guid7[0] = packet.Translator.ReadBit();
            guid7[3] = packet.Translator.ReadBit();
            guid7[2] = packet.Translator.ReadBit();
            guid7[7] = packet.Translator.ReadBit();

            var bit1CC = packet.Translator.ReadBit(); // +460

            if (bit1CC)
                bits1BC = (int)packet.Translator.ReadBits(21); // +444

            var hasUnkMovementField = !packet.Translator.ReadBit();
            var hasDestLocation = packet.Translator.ReadBit();

            if (hasDestLocation)
            {
                guid9[0] = packet.Translator.ReadBit();
                guid9[5] = packet.Translator.ReadBit();
                guid9[6] = packet.Translator.ReadBit();
                guid9[3] = packet.Translator.ReadBit();
                guid9[1] = packet.Translator.ReadBit();
                guid9[2] = packet.Translator.ReadBit();
                guid9[7] = packet.Translator.ReadBit();
                guid9[4] = packet.Translator.ReadBit();
            }

            guid3[6] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            predictedPowerCount = (int)packet.Translator.ReadBits(21);
            guid3[5] = packet.Translator.ReadBit();
            var hasElevation = !packet.Translator.ReadBit();
            var hasSourceLocation = packet.Translator.ReadBit();

            if (hasSourceLocation)
            {
                guid8[6] = packet.Translator.ReadBit();
                guid8[2] = packet.Translator.ReadBit();
                guid8[7] = packet.Translator.ReadBit();
                guid8[0] = packet.Translator.ReadBit();
                guid8[1] = packet.Translator.ReadBit();
                guid8[3] = packet.Translator.ReadBit();
                guid8[5] = packet.Translator.ReadBit();
                guid8[4] = packet.Translator.ReadBit();
            }

            if (hasTargetFlags)
                packet.Translator.ReadBitsE<TargetFlag>("Target Flags", 20);
            packet.Translator.ReadInt32<SpellId>("Spell ID");

            if (hasDestLocation)
            {
                packet.Translator.ReadXORByte(guid9, 4);
                packet.Translator.ReadXORByte(guid9, 3);
                packet.Translator.ReadXORByte(guid9, 1);
                packet.Translator.ReadXORByte(guid9, 0);
                packet.Translator.ReadXORByte(guid9, 5);
                packet.Translator.ReadSingle("Position Y");
                packet.Translator.ReadXORByte(guid9, 6);
                packet.Translator.ReadSingle("Position X");
                packet.Translator.ReadXORByte(guid9, 7);
                packet.Translator.ReadXORByte(guid9, 2);
                packet.Translator.ReadSingle("Position Z");
                packet.Translator.WriteGuid("guid9", guid9);
            }

            for (var i = 0; i < hitcountPos; ++i)
            {
                packet.Translator.ParseBitStream(guid[i], 4, 7, 5, 1, 0, 3, 6, 2);
                packet.Translator.WriteGuid("GUID", guid[i], i);
            }

            packet.Translator.ReadXORByte(guid3, 1);
            packet.Translator.ReadXORByte(guid6, 4);
            packet.Translator.ReadXORByte(guid6, 0);
            packet.Translator.ReadXORByte(guid6, 7);
            packet.Translator.ReadXORByte(guid6, 5);
            packet.Translator.ReadXORByte(guid6, 6);
            packet.Translator.ReadXORByte(guid6, 2);
            packet.Translator.ReadXORByte(guid6, 1);
            packet.Translator.ReadXORByte(guid6, 3);
            packet.Translator.WriteGuid("GUID6", guid6);

            if (hasPredictedType)
                packet.Translator.ReadByte("hasPredictedType");

            for (var i = 0; i < bits44; ++i)
            {
                packet.Translator.ParseBitStream(guid4[i], 6, 5, 2, 0, 3, 4, 1, 7);
                packet.Translator.WriteGuid("GUID", guid4[i], i);
            }

            if (hasUnkMovementField)
                packet.Translator.ReadInt32("hasUnkMovementField");

            packet.Translator.ReadXORByte(guid10, 0);
            packet.Translator.ReadXORByte(guid10, 4);
            packet.Translator.ReadXORByte(guid10, 6);
            packet.Translator.ReadXORByte(guid10, 2);
            packet.Translator.ReadXORByte(guid10, 7);
            packet.Translator.ReadXORByte(guid10, 3);
            packet.Translator.ReadXORByte(guid10, 1);
            packet.Translator.ReadXORByte(guid10, 5);
            packet.Translator.WriteGuid("GUID10", guid10);

            if (hasSourceLocation)
            {
                packet.Translator.ReadSingle("Position Z");
                packet.Translator.ReadXORByte(guid8, 5);
                packet.Translator.ReadXORByte(guid8, 1);
                packet.Translator.ReadXORByte(guid8, 3);
                packet.Translator.ReadXORByte(guid8, 2);
                packet.Translator.ReadXORByte(guid8, 7);
                packet.Translator.ReadSingle("Position X");
                packet.Translator.ReadXORByte(guid8, 0);
                packet.Translator.ReadXORByte(guid8, 6);
                packet.Translator.ReadXORByte(guid8, 4);
                packet.Translator.ReadSingle("Position Y");
                packet.Translator.WriteGuid("guid8", guid8);
            }

            packet.Translator.ReadXORByte(guid3, 7);

            if (hasDelayTime)
                packet.Translator.ReadInt32("hasDelayTime");

            if (hasAmmoDisplayId)
                packet.Translator.ReadInt32("AmmoDisplayId");

            if (hasCastSchoolImmunities)
                packet.Translator.ReadInt32("CastSchoolImmunities");

            packet.Translator.ReadXORByte(guid3, 0);
            packet.Translator.ReadXORByte(guid3, 6);
            packet.Translator.ReadXORByte(guid7, 1);
            packet.Translator.ReadXORByte(guid7, 4);
            packet.Translator.ReadXORByte(guid7, 5);
            packet.Translator.ReadXORByte(guid7, 2);
            packet.Translator.ReadXORByte(guid7, 6);
            packet.Translator.ReadXORByte(guid7, 7);
            packet.Translator.ReadXORByte(guid7, 0);
            packet.Translator.ReadXORByte(guid7, 3);
            packet.Translator.WriteGuid("GUID7", guid7);
            if (bit1CC)
            {
                packet.Translator.ReadInt32("Int1B4");

                for (var i = 0; i < bits1BC; ++i)
                {
                    packet.Translator.ReadInt32("IntED", i);
                    packet.Translator.ReadInt32("IntED", i);
                }

                packet.Translator.ReadInt32("Int1B8");
                packet.Translator.ReadInt32("Int1B0");
            }

            packet.Translator.ReadXORByte(guid3, 4);

            for (var i = 0; i < extraTargetCount; ++i)
            {
                packet.Translator.ReadXORByte(guid5[i], 7);
                packet.Translator.ReadXORByte(guid5[i], 5);
                packet.Translator.ReadXORByte(guid5[i], 2);
                packet.Translator.ReadXORByte(guid5[i], 4);
                packet.Translator.ReadXORByte(guid5[i], 1);
                packet.Translator.ReadSingle("Float16");
                packet.Translator.ReadSingle("Float12");
                packet.Translator.ReadXORByte(guid5[i], 6);
                packet.Translator.ReadSingle("Float8");
                packet.Translator.ReadXORByte(guid5[i], 3);
                packet.Translator.ReadXORByte(guid5[i], 0);
                packet.Translator.WriteGuid("GUID5", guid5[i], i);
            }

            packet.Translator.ReadXORByte(guid3, 2);
            packet.Translator.ReadWoWString("StringC0", bitsC0);
            packet.Translator.ReadXORByte(guid3, 5);

            for (var i = 0; i < predictedPowerCount; ++i)
            {
                packet.Translator.ReadByteE<PowerType>("Power type", i);
                packet.Translator.ReadInt32("Value", i);
            }

            if (hasVisualChain)
            {
                packet.Translator.ReadInt32("Int178");
                packet.Translator.ReadInt32("Int174");
            }

            packet.Translator.ReadInt32E<CastFlag>("Cast Flags");
            packet.Translator.ReadInt32("getMSTime");
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadByte("CastCount");

            for (var i = 0; i < runeCooldownPassedCount; ++i)
                packet.Translator.ReadByte("runeCooldownPassedCount", i);

            if (hasDestUnkByte)
                packet.Translator.ReadByte("hasDestUnkByte");

            packet.Translator.ReadXORByte(guid2, 3);

            if (hasAmmoInventoryType)
                packet.Translator.ReadByte("AmmoInventoryType");

            packet.Translator.ReadXORByte(guid2, 4);

            if (hasElevation)
                packet.Translator.ReadSingle("Elevation");

            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 6);

            if (hasPredictedHeal)
                packet.Translator.ReadInt32("PredictedHeal");

            packet.Translator.ReadXORByte(guid3, 3);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 5);

            if (hasRunesStateAfter)
                packet.Translator.ReadByte("hasRunesStateAfter");

            if (hasRunesStateBefore)
                packet.Translator.ReadByte("hasRunesStateBefore");

            packet.Translator.WriteGuid("Guid2", guid2);
            packet.Translator.WriteGuid("Guid3", guid3);
            packet.Translator.WriteGuid("Guid6", guid6);
            packet.Translator.WriteGuid("Guid7", guid7);
            packet.Translator.WriteGuid("Guid10", guid10);

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

        [Parser(Opcode.SMSG_SEND_KNOWN_SPELLS)]
        public static void HandleInitialSpells(Packet packet)
        {
            packet.Translator.ReadBit("InitialLogin");
            var count = packet.Translator.ReadBits("Spell Count", 22);

            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadUInt32<SpellId>("Spell ID", i);
            }
        }

        [Parser(Opcode.SMSG_SEND_UNLEARN_SPELLS)]
        public static void HandleSendUnlearnSpells(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 21);

            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadInt32("Unk Int32", i);
                packet.Translator.ReadInt32<SpellId>("Spell ID", i);
                packet.Translator.ReadByte("Unk Byte", i);
            }
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
                    packet.Translator.ReadSingle("Amount", j, i);
                    packet.Translator.ReadByte("Spell Mask bitpos", j, i);
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
                for (var i = 0; i < modTypeCount[j]; ++i)
                {
                    packet.Translator.ReadSingle("Amount", j, i);
                    packet.Translator.ReadByte("Spell Mask bitpos", j, i);
                }

                packet.Translator.ReadByteE<SpellModOp>("Spell Mod", j);
            }
        }

        [Parser(Opcode.SMSG_UPDATE_TALENT_DATA)]
        public static void ReadTalentInfo(Packet packet)
        {
            packet.Translator.ReadByte("Active Spec Group");
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
        }

        [Parser(Opcode.SMSG_COOLDOWN_EVENT)]
        public static void HandleCooldownEvent(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 4, 7, 1, 6, 5, 3, 0, 2);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.ReadXORByte(guid, 7);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPELL_COOLDOWN)]
        public static void HandleSpellCooldown(Packet packet)
        {
            var guid = new byte[8];

            var bit20 = !packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            var bits10 = packet.Translator.ReadBits(21);
            guid[0] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 7);

            for (var i = 0; i < bits10; ++i)
            {
                packet.Translator.ReadUInt32<SpellId>("Spell ID");
                packet.Translator.ReadInt32("Time");
            }

            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 2);

            if (bit20)
                packet.Translator.ReadByte("Unk mask");

            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 6);

            packet.Translator.WriteGuid("Guid", guid);
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

        [Parser(Opcode.SMSG_CLEAR_COOLDOWN)]
        public static void HandleClearCooldown(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 3, 6, 2, 5, 1, 0, 7, 4);

            packet.Translator.ReadBit("bit10");

            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 1);

            packet.Translator.WriteGuid("Guid3", guid);
        }

        [Parser(Opcode.SMSG_CAST_FAILED)]
        [Parser(Opcode.SMSG_PET_CAST_FAILED)]
        public static void HandleCastFailed(Packet packet)
        {
            var result = packet.Translator.ReadInt32E<SpellCastFailureReason>("Reason");
            packet.Translator.ReadByte("Cast count");
            packet.Translator.ReadUInt32<SpellId>("Spell ID");

            var bit18 = !packet.Translator.ReadBit();
            var bit14 = !packet.Translator.ReadBit();

            if (bit18)
                packet.Translator.ReadInt32("Int18");

            if (bit14)
                packet.Translator.ReadInt32("Int14");
        }

        [Parser(Opcode.SMSG_SPELL_FAILURE)]
        public static void HandleSpellFailed(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 1, 5, 3, 4, 2, 7, 0, 6);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadByteE<SpellCastFailureReason>("Reason");
            packet.Translator.ReadByte("Cast count");
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 7);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPELL_FAILED_OTHER)]
        public static void HandleSpellFailedOther(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 3, 1, 0, 7, 6, 4, 2, 5);

            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadByteE<SpellCastFailureReason>("Reason");
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadByte("Cast count");
            packet.Translator.ReadXORByte(guid, 0);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPELL_INTERRUPT_LOG)] // 4.3.4
        public static void HandleSpellInterruptLog(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[5] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadInt32<SpellId>("Interrupted Spell ID");
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadInt32<SpellId>("Interrupt Spell ID");
            packet.Translator.ReadXORByte(guid1, 4);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_TOTEM_CREATED)]
        public static void HandleTotemCreated(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadInt32("Duration");
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.StartBitStream(guid, 5, 2, 4, 7, 6, 1, 3, 0);
            packet.Translator.ParseBitStream(guid, 4, 3, 7, 6, 5, 1, 0, 2);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPELL_EXECUTE_LOG)]
        public static void HandleSpellLogExecute(Packet packet)
        {
            var guid1 = new byte[8];
            byte[][] guid2;
            byte[][][] guid3 = null;
            byte[][][] guid4 = null;
            byte[][][] guid5 = null;
            byte[][][] guid6 = null;

            var bit48 = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();

            var bits10 = packet.Translator.ReadBits(19);

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
                bits14[i] = packet.Translator.ReadBits(21);
                bits4[i] = packet.Translator.ReadBits(20);

                guid6[i] = new byte[bits4[i]][];
                for (var j = 0; j < bits4[i]; ++j)
                {
                    guid6[i][j] = new byte[8];
                    packet.Translator.StartBitStream(guid6[i][j], 4, 6, 5, 7, 0, 1, 2, 3);
                }

                bits44[i] = packet.Translator.ReadBits(22);

                guid3[i] = new byte[bits14[i]][];
                for (var j = 0; j < bits14[i]; ++j)
                {
                    guid3[i][j] = new byte[8];
                    packet.Translator.StartBitStream(guid3[i][j], 4, 6, 7, 3, 2, 0, 5, 1);
                }

                bits54[i] = packet.Translator.ReadBits(22);
                bits24[i] = packet.Translator.ReadBits(21);

                guid4[i] = new byte[bits24[i]][];
                for (var j = 0; j < bits24[i]; ++j)
                {
                    guid4[i][j] = new byte[8];
                    packet.Translator.StartBitStream(guid4[i][j], 1, 5, 7, 0, 2, 6, 4, 3);
                }

                bits34[i] = packet.Translator.ReadBits(24);

                guid5[i] = new byte[bits34[i]][];
                for (var j = 0; j < bits34[i]; ++j)
                {
                    guid5[i][j] = new byte[8];
                    packet.Translator.StartBitStream(guid5[i][j], 4, 6, 2, 7, 0, 5, 1, 3);
                }
            }

            guid1[1] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();

            var bits38 = 0u;
            if (bit48)
                bits38 = packet.Translator.ReadBits(21);

            guid1[7] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();

            for (var i = 0; i < bits10; ++i)
            {

                for (var j = 0; j < bits24[i]; ++j)
                {
                    packet.Translator.ReadInt32("Int24+0", i, j);
                    packet.Translator.ReadXORByte(guid4[i][j], 5);
                    packet.Translator.ReadXORByte(guid4[i][j], 4);
                    packet.Translator.ReadXORByte(guid4[i][j], 2);
                    packet.Translator.ReadInt32("Int24+4", i, j);
                    packet.Translator.ReadXORByte(guid4[i][j], 1);
                    packet.Translator.ReadXORByte(guid4[i][j], 7);
                    packet.Translator.ReadXORByte(guid4[i][j], 6);
                    packet.Translator.ReadXORByte(guid4[i][j], 0);
                    packet.Translator.ReadXORByte(guid4[i][j], 3);

                    packet.Translator.WriteGuid("Guid4", guid4[i][j], i, j);
                }

                for (var j = 0; j < bits14[i]; ++j)
                {
                    packet.Translator.ReadXORByte(guid3[i][j], 1);
                    packet.Translator.ReadXORByte(guid3[i][j], 5);
                    packet.Translator.ReadInt32("Int1C");
                    packet.Translator.ReadXORByte(guid3[i][j], 4);
                    packet.Translator.ReadXORByte(guid3[i][j], 3);
                    packet.Translator.ReadXORByte(guid3[i][j], 2);
                    packet.Translator.ReadXORByte(guid3[i][j], 0);
                    packet.Translator.ReadXORByte(guid3[i][j], 6);
                    packet.Translator.ReadXORByte(guid3[i][j], 7);

                    packet.Translator.WriteGuid("Guid3", guid3[i][j], i, j);
                }

                for (var j = 0; j < bits34[i]; ++j)
                {
                    packet.Translator.ParseBitStream(guid5[i][j], 3, 5, 0, 4, 7, 1, 2, 6);
                    packet.Translator.WriteGuid("Summoned GUID", guid5[i][j], i, j);
                }

                for (var j = 0; j < bits4[i]; ++j)
                {
                    packet.Translator.ReadSingle("FloatEB", i, j);
                    packet.Translator.ReadXORByte(guid6[i][j], 3);
                    packet.Translator.ReadInt32("IntEB", i, j);
                    packet.Translator.ReadXORByte(guid6[i][j], 4);
                    packet.Translator.ReadInt32("IntEB", i, j);
                    packet.Translator.ReadXORByte(guid6[i][j], 1);
                    packet.Translator.ReadXORByte(guid6[i][j], 7);
                    packet.Translator.ReadXORByte(guid6[i][j], 6);
                    packet.Translator.ReadXORByte(guid6[i][j], 0);
                    packet.Translator.ReadXORByte(guid6[i][j], 5);
                    packet.Translator.ReadXORByte(guid6[i][j], 2);

                    packet.Translator.WriteGuid("Guid6", guid6[i][j], i, j);
                }

                for (var j = 0; j < bits54[i]; ++j)
                    packet.Translator.ReadInt32("IntEB", i, j);

                var type = packet.Translator.ReadInt32E<SpellEffect>("Spell Effect", i);

                for (var j = 0; j < bits44[i]; ++j)
                    packet.Translator.ReadInt32<ItemId>("Created Item", i, j);
            }

            packet.Translator.ReadXORByte(guid1, 5);

            if (bit48)
            {
                for (var i = 0; i < bits38; ++i)
                {
                    packet.Translator.ReadInt32("Int38+4", i);
                    packet.Translator.ReadInt32("Int38+8", i);
                }

                packet.Translator.ReadInt32("Int30");
                packet.Translator.ReadInt32("Int34");
                packet.Translator.ReadInt32("Int2C");
            }

            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 4);

            packet.Translator.WriteGuid("Guid1", guid1);
        }

        [Parser(Opcode.MSG_CHANNEL_START)]
        public static void HandleSpellChannelStart(Packet packet)
        {
            var casterGUID = new byte[8];
            var targetGUID = new byte[8];
            var unkGUID = new byte[8];

            var bit38 = false;
            var bit3C = false;

            casterGUID[3] = packet.Translator.ReadBit();
            casterGUID[2] = packet.Translator.ReadBit();
            casterGUID[7] = packet.Translator.ReadBit();
            casterGUID[1] = packet.Translator.ReadBit();
            casterGUID[5] = packet.Translator.ReadBit();
            casterGUID[0] = packet.Translator.ReadBit();
            var bit40 = packet.Translator.ReadBit();
            if (bit40)
            {
                targetGUID[3] = packet.Translator.ReadBit();
                targetGUID[7] = packet.Translator.ReadBit();
                packet.Translator.ReadBit(); // fake bit
                unkGUID[3] = packet.Translator.ReadBit();
                unkGUID[7] = packet.Translator.ReadBit();
                unkGUID[5] = packet.Translator.ReadBit();
                unkGUID[1] = packet.Translator.ReadBit();
                unkGUID[0] = packet.Translator.ReadBit();
                unkGUID[4] = packet.Translator.ReadBit();
                unkGUID[2] = packet.Translator.ReadBit();
                unkGUID[6] = packet.Translator.ReadBit();
                targetGUID[6] = packet.Translator.ReadBit();
                bit3C = !packet.Translator.ReadBit();
                targetGUID[4] = packet.Translator.ReadBit();
                targetGUID[1] = packet.Translator.ReadBit();
                bit38 = !packet.Translator.ReadBit();
                targetGUID[2] = packet.Translator.ReadBit();
                targetGUID[0] = packet.Translator.ReadBit();
                targetGUID[5] = packet.Translator.ReadBit();
            }

            casterGUID[4] = packet.Translator.ReadBit();
            casterGUID[6] = packet.Translator.ReadBit();
            var bit1C = packet.Translator.ReadBit();
            if (bit40)
            {
                packet.Translator.ReadXORByte(targetGUID, 1);
                packet.Translator.ReadXORByte(unkGUID, 7);
                packet.Translator.ReadXORByte(unkGUID, 6);
                packet.Translator.ReadXORByte(unkGUID, 4);
                packet.Translator.ReadXORByte(unkGUID, 3);
                packet.Translator.ReadXORByte(unkGUID, 2);
                packet.Translator.ReadXORByte(unkGUID, 0);
                packet.Translator.ReadXORByte(unkGUID, 1);
                packet.Translator.ReadXORByte(unkGUID, 5);
                packet.Translator.ReadXORByte(targetGUID, 0);
                packet.Translator.ReadXORByte(targetGUID, 3);
                packet.Translator.ReadXORByte(targetGUID, 6);
                if (bit38)
                    packet.Translator.ReadInt32("Heal Amount");
                if (bit3C)
                    packet.Translator.ReadByte("Type");
                packet.Translator.ReadXORByte(targetGUID, 5);
                packet.Translator.ReadXORByte(targetGUID, 7);
                packet.Translator.ReadXORByte(targetGUID, 2);
                packet.Translator.ReadXORByte(targetGUID, 4);
                packet.Translator.WriteGuid("Target GUID", targetGUID);
                packet.Translator.WriteGuid("Unk GUID", unkGUID);
            }

            packet.Translator.ReadXORByte(casterGUID, 6);
            if (bit1C)
            {
                packet.Translator.ReadInt32("Int18");
                packet.Translator.ReadInt32("Int14");
            }

            packet.Translator.ReadXORByte(casterGUID, 7);
            packet.Translator.ReadXORByte(casterGUID, 0);
            packet.Translator.ReadXORByte(casterGUID, 4);
            packet.Translator.ReadXORByte(casterGUID, 5);
            packet.Translator.ReadXORByte(casterGUID, 3);
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadInt32("Duration");
            packet.Translator.ReadXORByte(casterGUID, 1);
            packet.Translator.ReadXORByte(casterGUID, 2);

            packet.Translator.WriteGuid("Caster GUID", casterGUID);
        }

        [Parser(Opcode.MSG_CHANNEL_UPDATE)]
        public static void HandleSpellChannelUpdate(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 5, 3, 6, 0, 1, 4, 2, 7);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadInt32("Timestamp");
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL)]
        public static void HandleCastVisual(Packet packet)
        {
            var pos = new Vector4();
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.Translator.ReadInt32("SpellVisualKit ID");
            pos.Z = packet.Translator.ReadSingle();
            packet.Translator.ReadInt16("Unk Int16 1");
            pos.Y = packet.Translator.ReadSingle();
            packet.Translator.ReadInt16("Unk Int16 2");
            pos.X = packet.Translator.ReadSingle();
            pos.O = packet.Translator.ReadSingle();
            guid1[7] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("bit20");
            guid2[3] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 4);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL_KIT)]
        public static void HandleCastVisualKit(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadUInt32("Unk");
            packet.Translator.ReadInt32("SpellVisualKit ID");
            packet.Translator.ReadUInt32("Unk");

            packet.Translator.StartBitStream(guid, 2, 5, 7, 3, 4, 6, 0, 1);
            packet.Translator.ParseBitStream(guid, 4, 2, 1, 3, 5, 6, 0, 7);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_AURA_POINTS_DEPLETED)]
        public static void HandleAuraPointsDepleted(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 5, 3, 1, 2, 0, 7, 6, 4);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadByte("Points?");
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadByte("Slot ID?");
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 4);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_CANCEL_CAST)]
        public static void HandlePlayerCancelCast(Packet packet)
        {
            var hasCount = !packet.Translator.ReadBit();
            var hasSpellId = !packet.Translator.ReadBit();
            if (hasCount)
                packet.Translator.ReadByte("Count");
            if (hasSpellId)
                packet.Translator.ReadUInt32<SpellId>("Spell Id");
        }

        [Parser(Opcode.CMSG_CANCEL_AURA)]
        public static void HandleCanelAura(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadBit(); // fake bit?
            packet.Translator.StartBitStream(guid, 1, 5, 2, 0, 3, 4, 6, 7);
            packet.Translator.ParseBitStream(guid, 0, 1, 4, 5, 3, 6, 7, 2);

            packet.Translator.WriteGuid("GUID", guid);
        }
    }
}

using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

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
                        aura.CasterGuid = new WowGuid64(BitConverter.ToUInt64(casterGUID[i], 0));
                    }

                    for (var j = 0; j < bits48[i]; ++j)
                        packet.ReadSingle("FloatEM", i, j);

                    for (var j = 0; j < effectCount[i]; ++j)
                        packet.ReadSingle("Effect Value", i, j);

                    packet.ReadInt32("Effect Mask", i);
                    aura.AuraFlags = packet.ReadByteE<AuraFlagMoP>("Flags", i);
                    var id = packet.ReadInt32<SpellId>("Spell ID", i);
                    aura.SpellId = (uint)id;
                    aura.Level = packet.ReadUInt16("Caster Level", i);
                    aura.Charges = packet.ReadByte("Charges", i);

                    aura.MaxDuration = hasMaxDuration[i] ? packet.ReadInt32("Max Duration", i) : 0;

                    aura.Duration = hasDuration[i] ? packet.ReadInt32("Duration", i) : 0;
                }
                packet.ReadByte("Slot", i);
            }
            packet.ParseBitStream(guid2, 0, 1, 3, 4, 2, 6, 7, 5);
            packet.WriteGuid("GUID2", guid2);
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

            var hasCastCount = !packet.ReadBit();
            var hasSrcLocation = packet.ReadBit();
            var hasTargetString = !packet.ReadBit();
            var hasTargetMask = !packet.ReadBit();
            var hasSpellId = !packet.ReadBit();
            var hasCastFlags = !packet.ReadBit();
            var hasDestLocation = packet.ReadBit();
            packet.ReadBit(); // fake bit?
            var archeologyCounter = packet.ReadBits(2);
            var hasMovement = packet.ReadBit();
            packet.ReadBit(); // fake bit?
            var hasGlyphIndex = !packet.ReadBit();

            for (var i = 0; i < archeologyCounter; ++i)
                packet.ReadBits("archeologyType", 2, i);

            var hasElevation = !packet.ReadBit();
            var hasMissileSpeed = !packet.ReadBit();

            if (hasDestLocation)
                packet.StartBitStream(guidB, 1, 2, 4, 3, 7, 6, 5, 0);

            if (hasMovement)
            {
                hasMovementFlags = !packet.ReadBit();
                guid20[5] = packet.ReadBit();

                if (hasMovementFlags)
                    packet.ReadBits("hasMovementFlags", 30);

                hasSplineElevation = !packet.ReadBit();
                guid20[0] = packet.ReadBit();
                guid20[2] = packet.ReadBit();
                hasFallData = packet.ReadBit();
                hasPitch = !packet.ReadBit();
                bit19C = packet.ReadBit();
                bit185 = packet.ReadBit();
                guid20[7] = packet.ReadBit();
                hasTimestamp = !packet.ReadBit();
                unkMovementLoopCounter = (int)packet.ReadBits(22);
                guid20[1] = packet.ReadBit();

                if (hasFallData)
                    hasFallDirection = packet.ReadBit();

                hasOrientation = !packet.ReadBit();
                hasUnkMovementField = !packet.ReadBit();
                guid20[6] = packet.ReadBit();
                hasMovementFlags2 = !packet.ReadBit();

                if (hasMovementFlags2)
                    packet.ReadBits("hasMovementFlags2", 13);

                hasTransport = packet.ReadBit();
                bit184 = packet.ReadBit();

                if (hasTransport)
                {
                    guid25[5] = packet.ReadBit();
                    guid25[0] = packet.ReadBit();
                    hasTransportTime2 = packet.ReadBit();
                    hasTransportTime3 = packet.ReadBit();
                    guid25[2] = packet.ReadBit();
                    guid25[1] = packet.ReadBit();
                    guid25[6] = packet.ReadBit();
                    guid25[7] = packet.ReadBit();
                    guid25[4] = packet.ReadBit();
                    guid25[3] = packet.ReadBit();
                }

                guid20[4] = packet.ReadBit();
                guid20[3] = packet.ReadBit();
            }

            packet.StartBitStream(guid5, 4, 7, 1, 0, 3, 6, 2, 5);

            if (hasSrcLocation)
                packet.StartBitStream(guid7, 1, 3, 7, 5, 0, 2, 4, 6);

            packet.StartBitStream(guid6, 0, 5, 7, 1, 4, 6, 2, 3);

            if (hasCastFlags)
                packet.ReadBits("hasCastFlags", 5);

            if (hasTargetString)
                packet.ReadBits("hasTargetString", 7);

            if (hasTargetMask)
                packet.ReadBitsE<TargetFlag>("Target Flags", 20);

            for (var i = 0; i < archeologyCounter; ++i)
            {
                packet.ReadUInt32("unk1", i);
                packet.ReadUInt32("unk2", i);
            }

            if (hasGlyphIndex)
                packet.ReadInt32("hasGlyphIndex");

            packet.ParseBitStream(guid6, 2, 7, 4, 3, 1, 0, 6, 5);


            if (hasSrcLocation)
            {
                packet.ReadXORByte(guid7, 3);
                packet.ReadXORByte(guid7, 4);
                packet.ReadSingle("Position Z");
                packet.ReadXORByte(guid7, 1);
                packet.ReadXORByte(guid7, 0);
                packet.ReadXORByte(guid7, 2);
                packet.ReadXORByte(guid7, 7);
                packet.ReadSingle("Position X");
                packet.ReadXORByte(guid7, 6);
                packet.ReadXORByte(guid7, 5);
                packet.ReadSingle("Position Y");

                packet.WriteGuid("Guid7", guid7);
            }

            if (hasDestLocation)
            {
                packet.ReadSingle("Position X");
                packet.ReadSingle("Position Y");
                packet.ReadXORByte(guidB, 6);
                packet.ReadXORByte(guidB, 7);
                packet.ReadXORByte(guidB, 0);
                packet.ReadXORByte(guidB, 1);
                packet.ReadXORByte(guidB, 3);
                packet.ReadSingle("Position Z");
                packet.ReadXORByte(guidB, 5);
                packet.ReadXORByte(guidB, 4);
                packet.ReadXORByte(guidB, 2);

                packet.WriteGuid("GuidB", guidB);
            }

            if (hasMovement)
            {
                packet.ReadSingle("Position Z");

                if (hasFallData)
                {
                    packet.ReadSingle("Z Speed");

                    if (hasFallDirection)
                    {
                        packet.ReadSingle("XY Speed");
                        packet.ReadSingle("SinAngle");
                        packet.ReadSingle("CosAngle");
                    }

                    packet.ReadInt32("FallTime");
                }

                if (hasPitch)
                    packet.ReadSingle("Pitch");

                if (hasTransport)
                {
                    packet.ReadXORByte(guid25, 4);
                    packet.ReadSingle("Orientation");
                    packet.ReadInt32("Transport Time");
                    packet.ReadXORByte(guid25, 7);
                    packet.ReadSingle("Position Y");
                    packet.ReadXORByte(guid25, 1);

                    if (hasTransportTime2)
                        packet.ReadInt32("hasTransportTime2");

                    packet.ReadByte("Transport Seat");
                    packet.ReadSingle("Position Z");
                    packet.ReadXORByte(guid25, 6);
                    packet.ReadXORByte(guid25, 5);
                    packet.ReadXORByte(guid25, 0);
                    packet.ReadSingle("Position X");
                    packet.ReadXORByte(guid25, 2);
                    if (hasTransportTime3)
                        packet.ReadInt32("hasTransportTime3");
                    packet.ReadXORByte(guid25, 3);

                    packet.WriteGuid("Guid25", guid25);
                }

                packet.ReadXORByte(guid20, 6);
                packet.ReadXORByte(guid20, 7);
                packet.ReadXORByte(guid20, 0);
                packet.ReadXORByte(guid20, 5);
                packet.ReadXORByte(guid20, 2);

                if (hasSplineElevation)
                    packet.ReadSingle("SplineElevation");

                for (var i = 0; i < unkMovementLoopCounter; ++i)
                    packet.ReadInt32("MovementLoopCounter", i);

                packet.ReadSingle("Position Y");

                if (hasTimestamp)
                    packet.ReadInt32("hasTimestamp");

                if (hasUnkMovementField)
                    packet.ReadInt32("MovementField");

                packet.ReadXORByte(guid20, 1);
                packet.ReadSingle("Position X");

                if (hasOrientation)
                    packet.ReadSingle("Orientation");

                packet.ReadXORByte(guid20, 4);
                packet.ReadXORByte(guid20, 3);

                packet.WriteGuid("Guid20", guid20);
            }

            if (hasSpellId)
                packet.ReadInt32<SpellId>("Spell ID");

            packet.ParseBitStream(guid5, 7, 0, 1, 2, 3, 5, 6, 4);

            if (hasElevation)
                packet.ReadSingle("hasElevation");

            if (hasMissileSpeed)
                packet.ReadSingle("missileSpeed");

            if (hasCastCount)
                packet.ReadByte("Cast Count");

            packet.WriteGuid("Guid5", guid5);
            packet.WriteGuid("Guid6", guid6);
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

            var hasTargetFlags = !packet.ReadBit();
            var bits44 = packet.ReadBits(24);
            guid2[2] = packet.ReadBit();
            packet.ReadBit(); // fake bit
            guid3[2] = packet.ReadBit();
            var hasPredictedHeal = !packet.ReadBit();
            var hasRunesStateBefore = !packet.ReadBit();

            var guid9 = new byte[bits44][];
            for (var i = 0; i < bits44; ++i)
            {
                guid9[i] = new byte[8];
                packet.StartBitStream(guid9[i], 5, 2, 7, 0, 3, 6, 1, 4);
            }

            packet.ReadBit(); // fake bit
            guid2[3] = packet.ReadBit();
            var hitcountPos = (int)packet.ReadBits(24);
            guid3[5] = packet.ReadBit();
            var missTypeCount = (int)packet.ReadBits(25);
            var predictedPowerCount = (int)packet.ReadBits(21);
            var hasDelayTime = !packet.ReadBit();
            var extraTargetCount = (int)packet.ReadBits(20);
            var hasSourceLocation = packet.ReadBit();

            var guid10 = new byte[extraTargetCount][];
            for (var i = 0; i < extraTargetCount; ++i)
            {
                guid10[i] = new byte[8];
                packet.StartBitStream(guid10[i], 3, 0, 7, 1, 4, 6, 2, 5);
            }

            packet.StartBitStream(guid8, 1, 2, 4, 5, 7, 0, 3, 6);

            if (hasSourceLocation)
                packet.StartBitStream(guid6, 4, 3, 0, 5, 7, 1, 2, 6);

            guid3[1] = packet.ReadBit();
            guid2[5] = packet.ReadBit();

            packet.StartBitStream(guid4, 3, 2, 7, 6, 5, 4, 0, 1);

            guid2[1] = packet.ReadBit();
            guid2[0] = packet.ReadBit();

            for (var i = 0; i < missTypeCount; ++i)
            {
                if (packet.ReadBits("bits22[0]", 4, i) == 11)
                    packet.ReadBits("bits22[1]", 4, i);
            }

            var hasDestLocation = packet.ReadBit();

            var guid11 = new byte[hitcountPos][];
            for (var i = 0; i < hitcountPos; ++i)
            {
                guid11[i] = new byte[8];
                packet.StartBitStream(guid11[i], 0, 1, 6, 2, 3, 4, 7, 5);
            }

            guid3[7] = packet.ReadBit();
            if (hasDestLocation)
                packet.StartBitStream(guid7, 5, 0, 1, 7, 3, 6, 2, 4);

            guid3[3] = packet.ReadBit();
            var hasAmmoInventoryType = !packet.ReadBit();
            guid2[6] = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            var hasElevation = !packet.ReadBit();
            var hasAmmoDisplayId = !packet.ReadBit();

            if (hasTargetFlags)
                packet.ReadBitsE<TargetFlag>("Target Flags", 20);

            var hasPredictedType = !packet.ReadBit();
            var hasUnkMovementField = !packet.ReadBit();
            var runeCooldownPassedCount = (int)packet.ReadBits(3);
            var hasCastSchoolImmunities = !packet.ReadBit();
            var hasVisualChain = packet.ReadBit();
            var hasRunesStateAfter = !packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid3[0] = packet.ReadBit();
            var bits2C = (int)packet.ReadBits(13);
            guid2[7] = packet.ReadBit();

            packet.ReadBit(); // fake bit
            packet.StartBitStream(guid5, 6, 4, 0, 3, 2, 1, 5, 7);

            guid3[6] = packet.ReadBit();
            guid3[4] = packet.ReadBit();

            var hasTargetString = !packet.ReadBit();
            var bitsC0 = 0u;
            if (hasTargetString)
                bitsC0 = packet.ReadBits(7);

            packet.ReadWoWString("StringC0", bitsC0);

            for (var i = 0; i < bits44; ++i)
            {
                packet.ParseBitStream(guid9[i], 4, 5, 6, 2, 0, 3, 1, 7);
                packet.WriteGuid("Guid9", guid9[i]);
            }

            packet.ParseBitStream(guid4, 7, 5, 2, 4, 0, 1, 3, 6);

            for (var i = 0; i < hitcountPos; ++i)
            {
                packet.ParseBitStream(guid11[i], 0, 7, 1, 4, 3, 5, 2, 6);
                packet.WriteGuid("Guid11", guid11[i]);
            }

            packet.ParseBitStream(guid8, 5, 3, 7, 1, 0, 2, 4, 6);

            if (hasVisualChain)
            {
                packet.ReadInt32("Int174");
                packet.ReadInt32("Int178");
            }

            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 1);

            if (hasSourceLocation)
            {
                packet.ReadXORByte(guid6, 2);
                packet.ReadSingle("Position Z");
                packet.ReadXORByte(guid6, 7);
                packet.ReadXORByte(guid6, 5);
                packet.ReadXORByte(guid6, 0);
                packet.ReadXORByte(guid6, 6);
                packet.ReadSingle("Posiion X");
                packet.ReadXORByte(guid6, 1);
                packet.ReadXORByte(guid6, 4);
                packet.ReadSingle("Position Y");
                packet.ReadXORByte(guid6, 3);
                packet.WriteGuid("Guid10", guid6);
            }

            if (hasCastSchoolImmunities)
                packet.ReadInt32("CastSchoolImmunities");

            packet.ReadXORByte(guid2, 2);

            packet.ParseBitStream(guid5, 0, 6, 7, 3, 5, 1, 4, 2);

            packet.ReadXORByte(guid2, 0);

            for (var i = 0; i < extraTargetCount; ++i)
            {
                packet.ReadSingle("Float188");

                packet.ReadXORByte(guid10[i], 2);
                packet.ReadXORByte(guid10[i], 6);
                packet.ReadXORByte(guid10[i], 1);

                packet.ReadSingle("Float188");
                packet.ReadSingle("Float188");

                packet.ReadXORByte(guid10[i], 5);
                packet.ReadXORByte(guid10[i], 0);
                packet.ReadXORByte(guid10[i], 7);
                packet.ReadXORByte(guid10[i], 3);
                packet.ReadXORByte(guid10[i], 4);

                packet.WriteGuid("guid999", guid10[i]);
            }

            packet.ReadXORByte(guid3, 7);
            packet.ReadXORByte(guid3, 1);

            if (hasDelayTime)
                packet.ReadInt32("hasDelayTime");

            packet.ReadInt32<SpellId>("Spell ID");

            if (hasSplineElevation)
                packet.ReadByte("Byte180");

            if (hasPredictedHeal)
                packet.ReadInt32("PredictedHeal");

            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid3, 2);

            if (hasUnkMovementField)
                packet.ReadInt32("hasUnkMovementField");

            if (hasDestLocation)
            {
                packet.ReadSingle("Position Z");
                packet.ReadXORByte(guid7, 3);
                packet.ReadSingle("Position X");
                packet.ReadXORByte(guid7, 4);
                packet.ReadXORByte(guid7, 5);
                packet.ReadXORByte(guid7, 1);
                packet.ReadSingle("Position Y");
                packet.ReadXORByte(guid7, 2);
                packet.ReadXORByte(guid7, 0);
                packet.ReadXORByte(guid7, 6);
                packet.ReadXORByte(guid7, 7);
                packet.WriteGuid("Guid7", guid7);
            }

            packet.ReadXORByte(guid2, 4);

            for (var i = 0; i < predictedPowerCount; ++i)
            {
                packet.ReadInt32("Value", i);
                packet.ReadByteE<PowerType>("Power type", i);
            }

            packet.ReadByte("CastCount");
            packet.ReadInt32E<CastFlag>("Cast Flags");

            if (hasPredictedType)
                packet.ReadByte("hasPredictedType");

            packet.ReadXORByte(guid2, 6);
            packet.ReadInt32("CastTime");

            if (hasRunesStateAfter)
                packet.ReadByte("hasRunesStateAfter");

            if (hasElevation)
                packet.ReadSingle("Elevation");

            packet.ReadXORByte(guid3, 3);

            if (hasRunesStateBefore)
                packet.ReadByte("hasRunesStateBefore");

            if (hasAmmoDisplayId)
                packet.ReadInt32("AmmoDisplayId");

            if (hasAmmoInventoryType)
                packet.ReadByte("AmmoInventoryType");

            packet.ReadXORByte(guid3, 4);

            for (var i = 0; i < runeCooldownPassedCount; ++i)
                packet.ReadByte("runeCooldownPassedCount", i);

            packet.ReadXORByte(guid3, 5);
            packet.ReadXORByte(guid3, 0);
            packet.ReadXORByte(guid3, 6);
            packet.ReadXORByte(guid2, 7);

            packet.WriteGuid("Guid2", guid2);
            packet.WriteGuid("Guid3", guid3);
            packet.WriteGuid("Guid4", guid4);
            packet.WriteGuid("Guid5", guid5);
            packet.WriteGuid("Guid8", guid8);
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

            packet.ReadBit(); // fake bit
            var hasDelayTime = !packet.ReadBit();
            guid2[1] = packet.ReadBit();
            var hasPredictedType = !packet.ReadBit();
            var hasTargetString = !packet.ReadBit(); // some flag
            guid2[3] = packet.ReadBit();
            guid3[3] = packet.ReadBit();
            var hasDestUnkByte = !packet.ReadBit();
            guid6[4] = packet.ReadBit();
            guid6[2] = packet.ReadBit();
            guid6[3] = packet.ReadBit();
            guid6[7] = packet.ReadBit();
            guid6[1] = packet.ReadBit();
            guid6[5] = packet.ReadBit();
            guid6[6] = packet.ReadBit();
            guid6[0] = packet.ReadBit();

            if (hasTargetString)
                counter = (int)packet.ReadBits(7);

            packet.ReadBit(); // fake bit
            guid2[4] = packet.ReadBit();
            var hasPredictedHeal = !packet.ReadBit();
            var hasTargetFlags = !packet.ReadBit();
            guid10[3] = packet.ReadBit();
            guid10[2] = packet.ReadBit();
            guid10[6] = packet.ReadBit();
            guid10[5] = packet.ReadBit();
            guid10[7] = packet.ReadBit();
            guid10[4] = packet.ReadBit();
            guid10[0] = packet.ReadBit();
            guid10[1] = packet.ReadBit();
            hitcountPos = (int)packet.ReadBits(24);
            guid2[2] = packet.ReadBit();
            guid3[4] = packet.ReadBit();
            guid3[2] = packet.ReadBit();
            guid2[5] = packet.ReadBit();

            var guid = new byte[hitcountPos][];
            for (var i = 0; i < hitcountPos; ++i)
            {
                guid[i] = new byte[8];
                packet.StartBitStream(guid[i], 1, 3, 0, 6, 4, 7, 5, 2);
            }

            guid3[1] = packet.ReadBit();
            var hasRunesStateAfter = !packet.ReadBit();
            guid3[7] = packet.ReadBit();
            var hasAmmoDisplayId = !packet.ReadBit();
            guid2[0] = packet.ReadBit();
            var hasAmmoInventoryType = !packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid3[0] = packet.ReadBit();
            bits44 = (int)packet.ReadBits(24);
            var hasCastSchoolImmunities = !packet.ReadBit();

            var guid4 = new byte[bits44][];
            for (var i = 0; i < bits44; ++i)
            {
                guid4[i] = new byte[8];
                packet.StartBitStream(guid4[i], 6, 7, 5, 3, 4, 1, 0, 2);
            }

            var hasRunesStateBefore = !packet.ReadBit();
            runeCooldownPassedCount = (int)packet.ReadBits(3);
            missTypeCount = (int)packet.ReadBits(25);
            bits2C = (int)packet.ReadBits(13);

            for (var i = 0; i < missTypeCount; ++i)
            {
                var bits136 = (int)packet.ReadBits(4);
                if (bits136 == 11)
                    packet.ReadBits("bits140", 4, i);
            }

            var hasVisualChain = packet.ReadBit();
            extraTargetCount = (int)packet.ReadBits(20);

            var guid5 = new byte[extraTargetCount][];
            for (var i = 0; i < extraTargetCount; ++i)
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

            var hasUnkMovementField = !packet.ReadBit();
            var hasDestLocation = packet.ReadBit();

            if (hasDestLocation)
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
            predictedPowerCount = (int)packet.ReadBits(21);
            guid3[5] = packet.ReadBit();
            var hasElevation = !packet.ReadBit();
            var hasSourceLocation = packet.ReadBit();

            if (hasSourceLocation)
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
                packet.ReadBitsE<TargetFlag>("Target Flags", 20);
            packet.ReadInt32<SpellId>("Spell ID");

            if (hasDestLocation)
            {
                packet.ReadXORByte(guid9, 4);
                packet.ReadXORByte(guid9, 3);
                packet.ReadXORByte(guid9, 1);
                packet.ReadXORByte(guid9, 0);
                packet.ReadXORByte(guid9, 5);
                packet.ReadSingle("Position Y");
                packet.ReadXORByte(guid9, 6);
                packet.ReadSingle("Position X");
                packet.ReadXORByte(guid9, 7);
                packet.ReadXORByte(guid9, 2);
                packet.ReadSingle("Position Z");
                packet.WriteGuid("guid9", guid9);
            }

            for (var i = 0; i < hitcountPos; ++i)
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
            packet.WriteGuid("GUID6", guid6);

            if (hasPredictedType)
                packet.ReadByte("hasPredictedType");

            for (var i = 0; i < bits44; ++i)
            {
                packet.ParseBitStream(guid4[i], 6, 5, 2, 0, 3, 4, 1, 7);
                packet.WriteGuid("GUID", guid4[i], i);
            }

            if (hasUnkMovementField)
                packet.ReadInt32("hasUnkMovementField");

            packet.ReadXORByte(guid10, 0);
            packet.ReadXORByte(guid10, 4);
            packet.ReadXORByte(guid10, 6);
            packet.ReadXORByte(guid10, 2);
            packet.ReadXORByte(guid10, 7);
            packet.ReadXORByte(guid10, 3);
            packet.ReadXORByte(guid10, 1);
            packet.ReadXORByte(guid10, 5);
            packet.WriteGuid("GUID10", guid10);

            if (hasSourceLocation)
            {
                packet.ReadSingle("Position Z");
                packet.ReadXORByte(guid8, 5);
                packet.ReadXORByte(guid8, 1);
                packet.ReadXORByte(guid8, 3);
                packet.ReadXORByte(guid8, 2);
                packet.ReadXORByte(guid8, 7);
                packet.ReadSingle("Position X");
                packet.ReadXORByte(guid8, 0);
                packet.ReadXORByte(guid8, 6);
                packet.ReadXORByte(guid8, 4);
                packet.ReadSingle("Position Y");
                packet.WriteGuid("guid8", guid8);
            }

            packet.ReadXORByte(guid3, 7);

            if (hasDelayTime)
                packet.ReadInt32("hasDelayTime");

            if (hasAmmoDisplayId)
                packet.ReadInt32("AmmoDisplayId");

            if (hasCastSchoolImmunities)
                packet.ReadInt32("CastSchoolImmunities");

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
            packet.WriteGuid("GUID7", guid7);
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

            for (var i = 0; i < extraTargetCount; ++i)
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

            for (var i = 0; i < predictedPowerCount; ++i)
            {
                packet.ReadByteE<PowerType>("Power type", i);
                packet.ReadInt32("Value", i);
            }

            if (hasVisualChain)
            {
                packet.ReadInt32("Int178");
                packet.ReadInt32("Int174");
            }

            packet.ReadInt32E<CastFlag>("Cast Flags");
            packet.ReadInt32("getMSTime");
            packet.ReadXORByte(guid2, 7);
            packet.ReadByte("CastCount");

            for (var i = 0; i < runeCooldownPassedCount; ++i)
                packet.ReadByte("runeCooldownPassedCount", i);

            if (hasDestUnkByte)
                packet.ReadByte("hasDestUnkByte");

            packet.ReadXORByte(guid2, 3);

            if (hasAmmoInventoryType)
                packet.ReadByte("AmmoInventoryType");

            packet.ReadXORByte(guid2, 4);

            if (hasElevation)
                packet.ReadSingle("Elevation");

            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 6);

            if (hasPredictedHeal)
                packet.ReadInt32("PredictedHeal");

            packet.ReadXORByte(guid3, 3);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 5);

            if (hasRunesStateAfter)
                packet.ReadByte("hasRunesStateAfter");

            if (hasRunesStateBefore)
                packet.ReadByte("hasRunesStateBefore");

            packet.WriteGuid("Guid2", guid2);
            packet.WriteGuid("Guid3", guid3);
            packet.WriteGuid("Guid6", guid6);
            packet.WriteGuid("Guid7", guid7);
            packet.WriteGuid("Guid10", guid10);

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

        [Parser(Opcode.SMSG_SEND_KNOWN_SPELLS)]
        public static void HandleInitialSpells(Packet packet)
        {
            packet.ReadBit("InitialLogin");
            var count = packet.ReadBits("Spell Count", 22);

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

        [Parser(Opcode.SMSG_SEND_UNLEARN_SPELLS)]
        public static void HandleSendUnlearnSpells(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);

            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32("Unk Int32", i);
                packet.ReadInt32<SpellId>("Spell ID", i);
                packet.ReadByte("Unk Byte", i);
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
                packet.ReadByteE<SpellModOp>("Spell Mod", j);

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

                packet.ReadByteE<SpellModOp>("Spell Mod", j);
            }
        }

        [Parser(Opcode.SMSG_UPDATE_TALENT_DATA)]
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

        [Parser(Opcode.SMSG_COOLDOWN_EVENT)]
        public static void HandleCooldownEvent(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 4, 7, 1, 6, 5, 3, 0, 2);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadInt32<SpellId>("Spell ID");
            packet.ReadXORByte(guid, 7);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPELL_COOLDOWN)]
        public static void HandleSpellCooldown(Packet packet)
        {
            var guid = new byte[8];

            var bit20 = !packet.ReadBit();
            guid[2] = packet.ReadBit();
            var bits10 = packet.ReadBits(21);
            guid[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 7);

            for (var i = 0; i < bits10; ++i)
            {
                packet.ReadUInt32<SpellId>("Spell ID");
                packet.ReadInt32("Time");
            }

            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 2);

            if (bit20)
                packet.ReadByte("Unk mask");

            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);

            packet.WriteGuid("Guid", guid);
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

        [Parser(Opcode.SMSG_CLEAR_COOLDOWN)]
        public static void HandleClearCooldown(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 3, 6, 2, 5, 1, 0, 7, 4);

            packet.ReadBit("bit10");

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 1);

            packet.WriteGuid("Guid3", guid);
        }

        [Parser(Opcode.SMSG_CAST_FAILED)]
        [Parser(Opcode.SMSG_PET_CAST_FAILED)]
        public static void HandleCastFailed(Packet packet)
        {
            var result = packet.ReadInt32E<SpellCastFailureReason>("Reason");
            packet.ReadByte("Cast count");
            packet.ReadUInt32<SpellId>("Spell ID");

            var bit18 = !packet.ReadBit();
            var bit14 = !packet.ReadBit();

            if (bit18)
                packet.ReadInt32("Int18");

            if (bit14)
                packet.ReadInt32("Int14");
        }

        [Parser(Opcode.SMSG_SPELL_FAILURE)]
        public static void HandleSpellFailed(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 1, 5, 3, 4, 2, 7, 0, 6);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadByteE<SpellCastFailureReason>("Reason");
            packet.ReadByte("Cast count");
            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 7);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPELL_FAILED_OTHER)]
        public static void HandleSpellFailedOther(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 3, 1, 0, 7, 6, 4, 2, 5);

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadByteE<SpellCastFailureReason>("Reason");
            packet.ReadXORByte(guid, 7);
            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadXORByte(guid, 1);
            packet.ReadByte("Cast count");
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPELL_INTERRUPT_LOG)] // 4.3.4
        public static void HandleSpellInterruptLog(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[5] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 6);
            packet.ReadInt32<SpellId>("Interrupted Spell ID");
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 5);
            packet.ReadInt32<SpellId>("Interrupt Spell ID");
            packet.ReadXORByte(guid1, 4);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_TOTEM_CREATED)]
        public static void HandleTotemCreated(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadByte("Slot");
            packet.ReadInt32("Duration");
            packet.ReadInt32<SpellId>("Spell ID");
            packet.StartBitStream(guid, 5, 2, 4, 7, 6, 1, 3, 0);
            packet.ParseBitStream(guid, 4, 3, 7, 6, 5, 1, 0, 2);

            packet.WriteGuid("Guid", guid);
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

            var bit48 = packet.ReadBit();
            guid1[6] = packet.ReadBit();

            var bits10 = packet.ReadBits(19);

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
                bits4[i] = packet.ReadBits(20);

                guid6[i] = new byte[bits4[i]][];
                for (var j = 0; j < bits4[i]; ++j)
                {
                    guid6[i][j] = new byte[8];
                    packet.StartBitStream(guid6[i][j], 4, 6, 5, 7, 0, 1, 2, 3);
                }

                bits44[i] = packet.ReadBits(22);

                guid3[i] = new byte[bits14[i]][];
                for (var j = 0; j < bits14[i]; ++j)
                {
                    guid3[i][j] = new byte[8];
                    packet.StartBitStream(guid3[i][j], 4, 6, 7, 3, 2, 0, 5, 1);
                }

                bits54[i] = packet.ReadBits(22);
                bits24[i] = packet.ReadBits(21);

                guid4[i] = new byte[bits24[i]][];
                for (var j = 0; j < bits24[i]; ++j)
                {
                    guid4[i][j] = new byte[8];
                    packet.StartBitStream(guid4[i][j], 1, 5, 7, 0, 2, 6, 4, 3);
                }

                bits34[i] = packet.ReadBits(24);

                guid5[i] = new byte[bits34[i]][];
                for (var j = 0; j < bits34[i]; ++j)
                {
                    guid5[i][j] = new byte[8];
                    packet.StartBitStream(guid5[i][j], 4, 6, 2, 7, 0, 5, 1, 3);
                }
            }

            guid1[1] = packet.ReadBit();
            guid1[4] = packet.ReadBit();

            var bits38 = 0u;
            if (bit48)
                bits38 = packet.ReadBits(21);

            guid1[7] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid1[3] = packet.ReadBit();

            for (var i = 0; i < bits10; ++i)
            {

                for (var j = 0; j < bits24[i]; ++j)
                {
                    packet.ReadInt32("Int24+0", i, j);
                    packet.ReadXORByte(guid4[i][j], 5);
                    packet.ReadXORByte(guid4[i][j], 4);
                    packet.ReadXORByte(guid4[i][j], 2);
                    packet.ReadInt32("Int24+4", i, j);
                    packet.ReadXORByte(guid4[i][j], 1);
                    packet.ReadXORByte(guid4[i][j], 7);
                    packet.ReadXORByte(guid4[i][j], 6);
                    packet.ReadXORByte(guid4[i][j], 0);
                    packet.ReadXORByte(guid4[i][j], 3);

                    packet.WriteGuid("Guid4", guid4[i][j], i, j);
                }

                for (var j = 0; j < bits14[i]; ++j)
                {
                    packet.ReadXORByte(guid3[i][j], 1);
                    packet.ReadXORByte(guid3[i][j], 5);
                    packet.ReadInt32("Int1C");
                    packet.ReadXORByte(guid3[i][j], 4);
                    packet.ReadXORByte(guid3[i][j], 3);
                    packet.ReadXORByte(guid3[i][j], 2);
                    packet.ReadXORByte(guid3[i][j], 0);
                    packet.ReadXORByte(guid3[i][j], 6);
                    packet.ReadXORByte(guid3[i][j], 7);

                    packet.WriteGuid("Guid3", guid3[i][j], i, j);
                }

                for (var j = 0; j < bits34[i]; ++j)
                {
                    packet.ParseBitStream(guid5[i][j], 3, 5, 0, 4, 7, 1, 2, 6);
                    packet.WriteGuid("Summoned GUID", guid5[i][j], i, j);
                }

                for (var j = 0; j < bits4[i]; ++j)
                {
                    packet.ReadSingle("FloatEB", i, j);
                    packet.ReadXORByte(guid6[i][j], 3);
                    packet.ReadInt32("IntEB", i, j);
                    packet.ReadXORByte(guid6[i][j], 4);
                    packet.ReadInt32("IntEB", i, j);
                    packet.ReadXORByte(guid6[i][j], 1);
                    packet.ReadXORByte(guid6[i][j], 7);
                    packet.ReadXORByte(guid6[i][j], 6);
                    packet.ReadXORByte(guid6[i][j], 0);
                    packet.ReadXORByte(guid6[i][j], 5);
                    packet.ReadXORByte(guid6[i][j], 2);

                    packet.WriteGuid("Guid6", guid6[i][j], i, j);
                }

                for (var j = 0; j < bits54[i]; ++j)
                    packet.ReadInt32("IntEB", i, j);

                var type = packet.ReadInt32E<SpellEffect>("Spell Effect", i);

                for (var j = 0; j < bits44[i]; ++j)
                    packet.ReadInt32<ItemId>("Created Item", i, j);
            }

            packet.ReadXORByte(guid1, 5);

            if (bit48)
            {
                for (var i = 0; i < bits38; ++i)
                {
                    packet.ReadInt32("Int38+4", i);
                    packet.ReadInt32("Int38+8", i);
                }

                packet.ReadInt32("Int30");
                packet.ReadInt32("Int34");
                packet.ReadInt32("Int2C");
            }

            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 7);
            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 4);

            packet.WriteGuid("Guid1", guid1);
        }

        [Parser(Opcode.MSG_CHANNEL_START)]
        public static void HandleSpellChannelStart(Packet packet)
        {
            var casterGUID = new byte[8];
            var targetGUID = new byte[8];
            var unkGUID = new byte[8];

            var bit38 = false;
            var bit3C = false;

            casterGUID[3] = packet.ReadBit();
            casterGUID[2] = packet.ReadBit();
            casterGUID[7] = packet.ReadBit();
            casterGUID[1] = packet.ReadBit();
            casterGUID[5] = packet.ReadBit();
            casterGUID[0] = packet.ReadBit();
            var bit40 = packet.ReadBit();
            if (bit40)
            {
                targetGUID[3] = packet.ReadBit();
                targetGUID[7] = packet.ReadBit();
                packet.ReadBit(); // fake bit
                unkGUID[3] = packet.ReadBit();
                unkGUID[7] = packet.ReadBit();
                unkGUID[5] = packet.ReadBit();
                unkGUID[1] = packet.ReadBit();
                unkGUID[0] = packet.ReadBit();
                unkGUID[4] = packet.ReadBit();
                unkGUID[2] = packet.ReadBit();
                unkGUID[6] = packet.ReadBit();
                targetGUID[6] = packet.ReadBit();
                bit3C = !packet.ReadBit();
                targetGUID[4] = packet.ReadBit();
                targetGUID[1] = packet.ReadBit();
                bit38 = !packet.ReadBit();
                targetGUID[2] = packet.ReadBit();
                targetGUID[0] = packet.ReadBit();
                targetGUID[5] = packet.ReadBit();
            }

            casterGUID[4] = packet.ReadBit();
            casterGUID[6] = packet.ReadBit();
            var bit1C = packet.ReadBit();
            if (bit40)
            {
                packet.ReadXORByte(targetGUID, 1);
                packet.ReadXORByte(unkGUID, 7);
                packet.ReadXORByte(unkGUID, 6);
                packet.ReadXORByte(unkGUID, 4);
                packet.ReadXORByte(unkGUID, 3);
                packet.ReadXORByte(unkGUID, 2);
                packet.ReadXORByte(unkGUID, 0);
                packet.ReadXORByte(unkGUID, 1);
                packet.ReadXORByte(unkGUID, 5);
                packet.ReadXORByte(targetGUID, 0);
                packet.ReadXORByte(targetGUID, 3);
                packet.ReadXORByte(targetGUID, 6);
                if (bit38)
                    packet.ReadInt32("Heal Amount");
                if (bit3C)
                    packet.ReadByte("Type");
                packet.ReadXORByte(targetGUID, 5);
                packet.ReadXORByte(targetGUID, 7);
                packet.ReadXORByte(targetGUID, 2);
                packet.ReadXORByte(targetGUID, 4);
                packet.WriteGuid("Target GUID", targetGUID);
                packet.WriteGuid("Unk GUID", unkGUID);
            }

            packet.ReadXORByte(casterGUID, 6);
            if (bit1C)
            {
                packet.ReadInt32("Int18");
                packet.ReadInt32("Int14");
            }

            packet.ReadXORByte(casterGUID, 7);
            packet.ReadXORByte(casterGUID, 0);
            packet.ReadXORByte(casterGUID, 4);
            packet.ReadXORByte(casterGUID, 5);
            packet.ReadXORByte(casterGUID, 3);
            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadInt32("Duration");
            packet.ReadXORByte(casterGUID, 1);
            packet.ReadXORByte(casterGUID, 2);

            packet.WriteGuid("Caster GUID", casterGUID);
        }

        [Parser(Opcode.MSG_CHANNEL_UPDATE)]
        public static void HandleSpellChannelUpdate(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 5, 3, 6, 0, 1, 4, 2, 7);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadInt32("Timestamp");
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL)]
        public static void HandleCastVisual(Packet packet)
        {
            var pos = new Vector4();
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.ReadInt32("SpellVisualKit ID");
            pos.Z = packet.ReadSingle();
            packet.ReadInt16("Unk Int16 1");
            pos.Y = packet.ReadSingle();
            packet.ReadInt16("Unk Int16 2");
            pos.X = packet.ReadSingle();
            pos.O = packet.ReadSingle();
            guid1[7] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            packet.ReadBit("bit20");
            guid2[3] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[0] = packet.ReadBit();

            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 4);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL_KIT)]
        public static void HandleCastVisualKit(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadUInt32("Unk");
            packet.ReadInt32("SpellVisualKit ID");
            packet.ReadUInt32("Unk");

            packet.StartBitStream(guid, 2, 5, 7, 3, 4, 6, 0, 1);
            packet.ParseBitStream(guid, 4, 2, 1, 3, 5, 6, 0, 7);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_AURA_POINTS_DEPLETED)]
        public static void HandleAuraPointsDepleted(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 5, 3, 1, 2, 0, 7, 6, 4);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadByte("Points?");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);
            packet.ReadByte("Slot ID?");
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_CANCEL_CAST)]
        public static void HandlePlayerCancelCast(Packet packet)
        {
            var hasCount = !packet.ReadBit();
            var hasSpellId = !packet.ReadBit();
            if (hasCount)
                packet.ReadByte("Count");
            if (hasSpellId)
                packet.ReadUInt32<SpellId>("Spell Id");
        }

        [Parser(Opcode.CMSG_CANCEL_AURA)]
        public static void HandleCanelAura(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadBit(); // fake bit?
            packet.StartBitStream(guid, 1, 5, 2, 0, 3, 4, 6, 7);
            packet.ParseBitStream(guid, 0, 1, 4, 5, 3, 6, 7, 2);

            packet.WriteGuid("GUID", guid);
        }
    }
}

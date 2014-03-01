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
                packet.ReadEnum<CastFlag>("Cast Flags", 20);

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
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");

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
            var bit1A8 = !packet.ReadBit();
            var bit150 = !packet.ReadBit();
            
            var guid9 = new byte[bits44][];
            for (var i = 0; i < bits44; ++i)
            {
                guid9[i] = new byte[8];
                packet.StartBitStream(guid9[i], 5, 2, 7, 0, 3, 6, 1, 4);
            }

            packet.ReadBit(); // fake bit
            guid2[3] = packet.ReadBit();
            var bits34 = (int)packet.ReadBits(24);
            guid3[5] = packet.ReadBit();
            var bits54 = (int)packet.ReadBits(25);
            var bits140 = (int)packet.ReadBits(21);
            var bit164 = !packet.ReadBit();
            var bits184 = (int)packet.ReadBits(20);
            var bit98 = packet.ReadBit();
            
            var guid10 = new byte[bits184][];
            for (var i = 0; i < bits184; ++i)
            {
                guid10[i] = new byte[8];
                packet.StartBitStream(guid10[i], 3, 0, 7, 1, 4, 6, 2, 5);
            }

            packet.StartBitStream(guid8, 1, 2, 4, 5, 7, 0, 3, 6);

            if (bit98)
                packet.StartBitStream(guid6, 4, 3, 0, 5, 7, 1, 2, 6);

            guid3[1] = packet.ReadBit();
            guid2[5] = packet.ReadBit();

            packet.StartBitStream(guid4, 3, 2, 7, 6, 5, 4, 0, 1);

            guid2[1] = packet.ReadBit();
            guid2[0] = packet.ReadBit();            

            for (var i = 0; i < bits54; ++i)
            {
                if (packet.ReadBits("bits22[0]", 4, i) == 11)
                    packet.ReadBits("bits22[1]", 4, i);
            }

            var bitB8 = packet.ReadBit();

            var guid11 = new byte[bits34][];
            for (var i = 0; i < bits34; ++i)
            {
                guid11[i] = new byte[8];
                packet.StartBitStream(guid11[i], 0, 1, 6, 2, 3, 4, 7, 5);
            }

            guid3[7] = packet.ReadBit();
            if (bitB8)
                packet.StartBitStream(guid7, 5, 0, 1, 7, 3, 6, 2, 4);

            guid3[3] = packet.ReadBit();
            var bit170 = !packet.ReadBit();
            guid2[6] = packet.ReadBit();
            var hasSplineElevation = !packet.ReadBit();
            var bit168 = !packet.ReadBit();
            var bit16C = !packet.ReadBit();

            if (hasTargetFlags)
                packet.ReadEnum<TargetFlag>("Target Flags", 20);

            var bit1AC = !packet.ReadBit();
            var hasUnkMovementField = !packet.ReadBit();
            var bits154 = (int)packet.ReadBits(3);
            var bit194 = !packet.ReadBit();
            var bit17C = packet.ReadBit();
            var bit151 = !packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid3[0] = packet.ReadBit();
            var bits2C = (int)packet.ReadBits(13);
            guid2[7] = packet.ReadBit();

            packet.ReadBit(); // fake bit
            packet.StartBitStream(guid5, 6, 4, 0, 3, 2, 1, 5, 7);

            guid3[6] = packet.ReadBit();
            guid3[4] = packet.ReadBit();

            var bitC0 = !packet.ReadBit();
            var bitsC0 = 0u;
            if (bitC0)
                bitsC0 = packet.ReadBits(7);

            packet.ReadWoWString("StringC0", bitsC0);

            for (var i = 0; i < bits44; ++i)
            {
                packet.ParseBitStream(guid9[i], 4, 5, 6, 2, 0, 3, 1, 7);
                packet.WriteGuid("Guid9", guid9[i]);
            }

            packet.ParseBitStream(guid4, 7, 5, 2, 4, 0, 1, 3, 6);
            
            for (var i = 0; i < bits34; ++i)
            {
                packet.ParseBitStream(guid11[i], 0, 7, 1, 4, 3, 5, 2, 6);
                packet.WriteGuid("Guid11", guid11[i]);
            }

            packet.ParseBitStream(guid8, 5, 3, 7, 1, 0, 2, 4, 6);

            if (bit17C)
            {
                packet.ReadInt32("Int174");
                packet.ReadInt32("Int178");
            }

            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 1);

            if (bit98)
            {
                packet.ReadXORByte(guid6, 2);
                packet.ReadSingle("Float90");
                packet.ReadXORByte(guid6, 7);
                packet.ReadXORByte(guid6, 5);
                packet.ReadXORByte(guid6, 0);
                packet.ReadXORByte(guid6, 6);
                packet.ReadSingle("Float88");
                packet.ReadXORByte(guid6, 1);
                packet.ReadXORByte(guid6, 4);
                packet.ReadSingle("Float8C");
                packet.ReadXORByte(guid6, 3);
                packet.WriteGuid("Guid10", guid6);
            }

            if (bit194)
                packet.ReadInt32("Int194");

            packet.ReadXORByte(guid2, 2);

            packet.ParseBitStream(guid5, 0, 6, 7, 3, 5, 1, 4, 2);

            packet.ReadXORByte(guid2, 0);
            
            for (var i = 0; i < bits184; ++i)
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

            if (bit164)
                packet.ReadInt32("Int164");

            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");

            if (hasSplineElevation)
                packet.ReadByte("Byte180");

            if (bit1A8)
                packet.ReadInt32("Int1A8");

            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid3, 2);

            if (hasUnkMovementField)
                packet.ReadInt32("hasUnkMovementField");

            if (bitB8)
            {
                packet.ReadSingle("FloatB0");
                packet.ReadXORByte(guid7, 3);
                packet.ReadSingle("FloatA8");
                packet.ReadXORByte(guid7, 4);
                packet.ReadXORByte(guid7, 5);
                packet.ReadXORByte(guid7, 1);
                packet.ReadSingle("FloatAC");
                packet.ReadXORByte(guid7, 2);
                packet.ReadXORByte(guid7, 0);
                packet.ReadXORByte(guid7, 6);
                packet.ReadXORByte(guid7, 7);
                packet.WriteGuid("Guid7", guid7);
            }

            packet.ReadXORByte(guid2, 4);

            for (var i = 0; i < bits140; ++i)
            {
                packet.ReadInt32("IntED", i);
                packet.ReadByte("ByteED", i);
            }

            packet.ReadByte("Byte20");
            packet.ReadInt32("Int28");

            if (bit1AC)
                packet.ReadByte("Byte1AC");

            packet.ReadXORByte(guid2, 6);
            packet.ReadInt32("Int30");

            if (bit151)
                packet.ReadByte("Byte151");

            if (bit168)
                packet.ReadSingle("Float168");

            packet.ReadXORByte(guid3, 3);

            if (bit150)
                packet.ReadByte("Byte150");

            if (bit16C)
                packet.ReadInt32("Int16C");

            if (bit170)
                packet.ReadByte("Byte170");

            packet.ReadXORByte(guid3, 4);

            for (var i = 0; i < bits154; ++i)
                packet.ReadByte("Byte158", i);

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
            var hasSplineElevation = !packet.ReadBit();
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

            var hasUnkMovementField = !packet.ReadBit();
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

            if (hasSplineElevation)
                packet.ReadByte("hasSplineElevation");
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

        [Parser(Opcode.SMSG_SEND_UNLEARN_SPELLS)]
        public static void HandleSendUnlearnSpells(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);

            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32("Unk Int32", i);
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", i);
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

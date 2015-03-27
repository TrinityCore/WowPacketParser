using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParser.V5_4_8_18291.Parsers
{
    public static class SpellHandler
    {
        [Parser(Opcode.CMSG_CAST_SPELL)]
        public static void HandleCastSpell(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var guid3 = new byte[8];
            var guid4 = new byte[8];

            var transportGUID = new byte[8];
            var guid20 = new byte[8];

            var bit388 = false;
            var bit389 = false;
            var bit412 = false;
            var hasUnkMovementField = false;
            var hasTransport = false;
            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasMovementFlags = false;
            var hasMovementFlags2 = false;
            var hasFallDirection = false;
            var hasFallData = false;
            var hasPitch = false;
            var hasOrientation = false;
            var hasSplineElevation = false;
            var hasTimestamp = false;

            var targetString = 0u;

            packet.ReadBit();                               // v4 + 1
            var hasTargetString = !packet.ReadBit();        // v2 + 120
            packet.ReadBit();                               // v2 + 48
            var hasCastCount = !packet.ReadBit();           // v2 + 16
            var hasSrcLocation = packet.ReadBit();          // v2 + 80
            var hasDestLocation = packet.ReadBit();         // v2 + 112
            var hasSpellId = !packet.ReadBit();             // v2 + 20

            var archeologyCounter = packet.ReadBits(2);     // v2 + 424

            var hasTargetMask = !packet.ReadBit();          // v2 + 32

            var hasMissileSpeed = !packet.ReadBit();

            for (var i = 0; i < archeologyCounter; ++i)
                packet.ReadBits("archeologyType", 2, i);

            var hasGlyphIndex = !packet.ReadBit();          // v2 + 24
            var hasMovement = packet.ReadBit();             // v2 + 416
            var hasElevation = !packet.ReadBit();
            var hasCastFlags = !packet.ReadBit();           // v2 + 28

            packet.StartBitStream(guid1, 5, 4, 2, 7, 1, 6, 3, 0);

            if (hasDestLocation)
                packet.StartBitStream(guid2, 1, 3, 5, 0, 2, 6, 7, 4);


            var unkMovementLoopCounter = 0u;
            if (hasMovement)
            {
                unkMovementLoopCounter = packet.ReadBits(22);

                bit388 = packet.ReadBit(); // v2 + 388
                guid20[4] = packet.ReadBit(); // v2 + 260

                hasTransport = packet.ReadBit(); // v2 + 344
                if (hasTransport)
                {
                    hasTransportTime2 = packet.ReadBit();   // v2 + 332
                    transportGUID[7] = packet.ReadBit();    // v2 + 303
                    transportGUID[5] = packet.ReadBit();    // v2 + 300
                    transportGUID[1] = packet.ReadBit();    // v2 + 297
                    transportGUID[0] = packet.ReadBit();    // v2 + 296
                    transportGUID[6] = packet.ReadBit();    // v2 + 302
                    transportGUID[3] = packet.ReadBit();    // v2 + 299
                    transportGUID[5] = packet.ReadBit();    // v2 + 301
                    hasTransportTime3 = packet.ReadBit();   // v2 + 340
                    transportGUID[2] = packet.ReadBit();    // v2 + 2980
                }

                bit389 = packet.ReadBit();                  // v2 + 389
                guid20[7] = packet.ReadBit();               // v2 + 263
                hasOrientation = !packet.ReadBit();
                guid20[6] = packet.ReadBit();               // v2 + 263
                hasSplineElevation = !packet.ReadBit();
                hasPitch = !packet.ReadBit();
                guid20[0] = packet.ReadBit();               // v2 + 263
                bit412 = packet.ReadBit();                  // v2 + 412
                hasMovementFlags = !packet.ReadBit();       // v2 + 264
                hasTimestamp = !packet.ReadBit();           // v2 + bit272
                hasUnkMovementField = !packet.ReadBit();    // v2 + 264

                if (hasMovementFlags)
                    packet.ReadBits("hasMovementFlags", 30);

                guid20[1] = packet.ReadBit();               // v2 + 257
                guid20[3] = packet.ReadBit();               // v2 + 259
                guid20[2] = packet.ReadBit();               // v2 + 258
                guid20[5] = packet.ReadBit();               // v2 + 261

                hasFallData = packet.ReadBit();
                if (hasFallData)
                    hasFallDirection = packet.ReadBit();

                hasMovementFlags2 = !packet.ReadBit();
                if (hasMovementFlags2)
                    packet.ReadBits("hasMovementFlags2", 13);
            }

            packet.StartBitStream(guid3, 1, 0, 7, 4, 6, 5, 3, 2);

            if (hasSrcLocation)
                packet.StartBitStream(guid4, 4, 5, 3, 0, 7, 1, 6, 2);

            if (hasTargetMask)
                packet.ReadBitsE<TargetFlag>("Target Flags", 20);

            if (hasCastFlags)
                packet.ReadBits("hasCastFlags", 5);

            if (hasTargetString)
                targetString = packet.ReadBits("hasTargetString", 7);

            for (var i = 0; i < archeologyCounter; ++i)
            {
                packet.ReadUInt32("unk1", i);
                packet.ReadUInt32("unk2", i);
            }

            if (hasMovement)
            {
                packet.ReadSingle("Position X");      // v2 + 276
                packet.ReadXORByte(guid20, 0);      // v2 + 256

                if (hasTransport)
                {
                    packet.ReadXORByte(transportGUID, 2);   // v2 + 298
                    packet.ReadByte("Transport Seat");      // v2 + 320
                    packet.ReadXORByte(transportGUID, 3);   // v2 + 299
                    packet.ReadXORByte(transportGUID, 7);   // v2 + 303
                    packet.ReadSingle("float304");          // v2 + 304
                    packet.ReadXORByte(transportGUID, 5);   // v2 + 301

                    if (hasTransportTime3)
                        packet.ReadInt32("hasTransportTime3");


                    packet.ReadSingle("float312");          // v2 + 312
                    packet.ReadSingle("float308");          // v2 + 308
                    packet.ReadXORByte(transportGUID, 6);   // v2 + 302
                    packet.ReadXORByte(transportGUID, 1);   // v2 + 297
                    packet.ReadSingle("float316");          // v2 + 316
                    packet.ReadXORByte(transportGUID, 4);   // v2 + 300

                    if (hasTransportTime2)
                        packet.ReadInt32("hasTransportTime2");

                    packet.ReadXORByte(transportGUID, 0);   // v2 + 296

                    packet.ReadInt32("Transport Time");     // v2 + 324

                    packet.WriteGuid("Transport GUID", transportGUID);
                }

                packet.ReadXORByte(guid20, 5);      // v2 + 256

                if (hasFallData)
                {
                    packet.ReadInt32("FallTime");
                    packet.ReadSingle("Z Speed");

                    if (hasFallDirection)
                    {
                        packet.ReadSingle("SinAngle");
                        packet.ReadSingle("XY Speed");
                        packet.ReadSingle("CosAngle");
                    }
                }

                if (hasSplineElevation)
                    packet.ReadSingle("SplineElevation");   // v2 + 384

                packet.ReadXORByte(guid20, 6);      // v2 + 262

                if (hasUnkMovementField)
                    packet.ReadInt32("Int408");     // v2 + 408

                packet.ReadXORByte(guid20, 4);      // v2 + 260

                if (hasOrientation)
                    packet.ReadSingle("Orientation");   // v2 + 288

                if (hasTimestamp)
                    packet.ReadInt32("hasTimestamp");   // v2 + 288

                packet.ReadXORByte(guid20, 1);      // v2 + 257

                if (hasPitch)
                    packet.ReadSingle("Pitch");     // v2 + 352

                packet.ReadXORByte(guid20, 3);      // v2 + 260

                for (var i = 0; i < unkMovementLoopCounter; ++i)
                    packet.ReadInt32("MovementLoopCounter", i);

                packet.ReadSingle("Position Y");    // v2 + 280
                packet.ReadXORByte(guid20, 7);      // v2 + 260
                packet.ReadSingle("Position Z");    // v2 + 284
                packet.ReadXORByte(guid20, 2);      // v2 + 258

                packet.WriteGuid("Guid20", guid20);
            }

            packet.ParseBitStream(guid3, 4, 2, 1, 5, 7, 3, 6, 0);

            if (hasDestLocation)
            {
                packet.ReadXORByte(guid2, 2);
                packet.ReadSingle("Position X");
                packet.ReadXORByte(guid2, 4);
                packet.ReadXORByte(guid2, 1);
                packet.ReadXORByte(guid2, 0);
                packet.ReadXORByte(guid2, 3);
                packet.ReadSingle("Position Y");
                packet.ReadXORByte(guid2, 7);
                packet.ReadSingle("Position Z");
                packet.ReadXORByte(guid2, 5);
                packet.ReadXORByte(guid2, 6);

                packet.WriteGuid("Guid2", guid2);
            }

            packet.ParseBitStream(guid1, 3, 4, 7, 6, 2, 0, 1, 5);

            if (hasSrcLocation)
            {
                packet.ReadSingle("Position Y");
                packet.ReadXORByte(guid4, 5);
                packet.ReadXORByte(guid4, 1);
                packet.ReadXORByte(guid4, 7);
                packet.ReadXORByte(guid4, 6);
                packet.ReadSingle("Position X");
                packet.ReadXORByte(guid4, 3);
                packet.ReadXORByte(guid4, 2);
                packet.ReadXORByte(guid4, 0);
                packet.ReadXORByte(guid4, 4);
                packet.ReadSingle("Position Z");

                packet.WriteGuid("Guid4", guid4);
            }

            if (hasTargetString)
                packet.ReadWoWString("TargetString", targetString);

            if (hasMissileSpeed)
                packet.ReadSingle("missileSpeed");

            if (hasElevation)
                packet.ReadSingle("hasElevation");

            if (hasCastCount)
                packet.ReadByte("Cast Count");

            if (hasSpellId)
                packet.ReadInt32<SpellId>("Spell ID");

            if (hasGlyphIndex)
                packet.ReadInt32("hasGlyphIndex");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_SPELL_START)]
        public static void HandleSpellStart(Packet packet)
        {
            var casterGUID = new byte[8];
            var casterUnitGUID = new byte[8];
            var itemTargetGUID = new byte[8];
            var guid4 = new byte[8];
            var guid5 = new byte[8];
            var unkGUID = new byte[8];
            var targetGUID = new byte[8];
            var guid8 = new byte[8];

            var bits44 = packet.ReadBits("bits44", 24);
            casterGUID[5] = packet.ReadBit();

            var guid9 = new byte[bits44][]; //18+
            for (var i = 0; i < bits44; ++i)
            {
                guid9[i] = new byte[8];
                packet.StartBitStream(guid9[i], 1, 0, 7, 2, 4, 3, 6, 5);
            }

            var hasSplineElevation = !packet.ReadBit("hasSplineElevation");//384
            packet.ReadBit(); //fake bit
            casterUnitGUID[4] = packet.ReadBit();
            casterGUID[2] = packet.ReadBit();
            var runeCooldownPassedCount = (int)packet.ReadBits(3);//340
            casterUnitGUID[2] = packet.ReadBit();
            casterUnitGUID[6] = packet.ReadBit();
            var missTypeCount = (int)packet.ReadBits(25); //84
            var bits11 = (int)packet.ReadBits(13);
            casterGUID[4] = packet.ReadBit();
            var hitcountPos = (int)packet.ReadBits(24); //52
            casterUnitGUID[7] = packet.ReadBit();

            var guid11 = new byte[hitcountPos][]; //14+
            for (var i = 0; i < hitcountPos; ++i)
            {
                guid11[i] = new byte[8];
                packet.StartBitStream(guid11[i], 5, 0, 3, 4, 7, 2, 6, 1);
            }

            var hasSourceLocation = packet.ReadBit("hasSourceLocation"); //152
            var predictedPowerCount = (int)packet.ReadBits(21); //320
            packet.StartBitStream(itemTargetGUID, 3, 0, 1, 7, 2, 6, 4, 5); //120-127
            var hasElevation = !packet.ReadBit("hasElevation"); //90
            var hasTargetString = !packet.ReadBit("hasTargetString");
            var hasAmmoInventoryType = !packet.ReadBit("hasAmmoInventoryType");//368
            var hasDestLocation = packet.ReadBit("hasDestLocation");//184
            var hasDelayTime = !packet.ReadBit("hasDelayTime"); //89
            casterGUID[3] = packet.ReadBit();

            if (hasDestLocation)
                packet.StartBitStream(guid4, 1, 6, 2, 7, 0, 3, 5, 4); //160-167

            var hasAmmoDisplayId = !packet.ReadBit("hasAmmoDisplayId"); //91

            if (hasSourceLocation)
                packet.StartBitStream(guid5, 4, 3, 5, 1, 7, 0, 6, 2); //128-135

            packet.ReadBit(); //fake bit 104
            casterGUID[6] = packet.ReadBit();
            packet.StartBitStream(unkGUID, 2, 1, 7, 6, 0, 5, 3, 4); //416-423
            var hasTargetFlags = !packet.ReadBit("hasTargetFlags");

            if (hasTargetFlags)
                packet.ReadBitsE<TargetFlag>("Target Flags", 20);

            casterGUID[1] = packet.ReadBit();
            var hasPredictedHeal = !packet.ReadBit("hasPredictedHeal"); //106
            var hasRunesStateBefore = !packet.ReadBit("hasRunesStateBefore"); //336
            var hasCastSchoolImmunities = !packet.ReadBit("hasCastSchoolImmunities"); //101
            casterUnitGUID[5] = packet.ReadBit();
            packet.ReadBit(); //28 fake bit
            var extraTargetCount = (int)packet.ReadBits("extraTargetCount", 20);//388

            var guid10 = new byte[extraTargetCount][];
            for (var i = 0; i < extraTargetCount; ++i) //388
            {
                guid10[i] = new byte[8];
                packet.StartBitStream(guid10[i], 1, 6, 2, 3, 5, 7, 0, 4); //98+
            }
            packet.StartBitStream(targetGUID, 1, 4, 6, 7, 5, 3, 0, 2); //112-119
            casterGUID[0] = packet.ReadBit();
            casterUnitGUID[3] = packet.ReadBit();
            var hasRunesStateAfter = !packet.ReadBit("hasRunesStateAfter");//337

            var bitsC0 = 0u;
            if (hasTargetString)
                bitsC0 = packet.ReadBits(7);//192

            for (var i = 0; i < missTypeCount; ++i)
            {
                if (packet.ReadBits("bits22[0]", 4, i) == 11)
                    packet.ReadBits("bits22[1]", 4, i);
            }
            var hasUnkMovementField = !packet.ReadBit("hasUnkMovementField"); // 102
            casterUnitGUID[1] = packet.ReadBit();
            var hasVisualChain = packet.ReadBit("hasVisualChain");//380
            casterGUID[7] = packet.ReadBit();
            var hasPredictedType = !packet.ReadBit("hasPredictedType");//428
            casterUnitGUID[0] = packet.ReadBit();

            packet.ParseBitStream(itemTargetGUID, 1, 7, 6, 0, 4, 2, 3, 5); //120-127
            packet.WriteGuid("Guid3", itemTargetGUID);

            for (var i = 0; i < hitcountPos; ++i)
            {
                packet.ParseBitStream(guid11[i], 4, 5, 3, 0, 6, 2, 1, 7);//14+
                packet.WriteGuid("Guid11", guid11[i]);
            }
            packet.ParseBitStream(targetGUID, 4, 5, 1, 7, 6, 3, 2, 0); //112-119
            packet.WriteGuid("Guid7", targetGUID);

            packet.ReadInt32("CastTime"); //12

            packet.ParseBitStream(unkGUID, 4, 5, 3, 2, 1, 6, 7, 0); //416-423
            packet.WriteGuid("Guid6", unkGUID);

            if (hasDestLocation)
            {
                packet.ReadXORByte(guid4, 4); //160-167
                packet.ReadXORByte(guid4, 0);
                packet.ReadXORByte(guid4, 5);
                packet.ReadXORByte(guid4, 7);
                packet.ReadXORByte(guid4, 1);
                packet.ReadXORByte(guid4, 2);
                packet.ReadXORByte(guid4, 3);
                packet.ReadSingle("Position Y");
                packet.ReadSingle("Position Z");
                packet.ReadXORByte(guid4, 6);
                packet.ReadSingle("Position X");
                packet.WriteGuid("Guid4", guid4);
            }

            for (var i = 0; i < extraTargetCount; ++i)
            {
                packet.ReadXORByte(guid10[i], 0); //98+
                packet.ReadSingle("Float186");
                packet.ReadXORByte(guid10[i], 1);
                packet.ReadXORByte(guid10[i], 5);
                packet.ReadXORByte(guid10[i], 4);
                packet.ReadXORByte(guid10[i], 7);
                packet.ReadSingle("Float187");
                packet.ReadXORByte(guid10[i], 3);
                packet.ReadSingle("Float188");
                packet.ReadXORByte(guid10[i], 6);
                packet.ReadXORByte(guid10[i], 2);

                packet.WriteGuid("Guid10", guid10[i]);
            }

            if (hasSourceLocation)
            {
                packet.ReadXORByte(unkGUID, 0); //128-135
                packet.ReadXORByte(unkGUID, 5);
                packet.ReadXORByte(unkGUID, 4);
                packet.ReadXORByte(unkGUID, 7);
                packet.ReadXORByte(unkGUID, 3);
                packet.ReadXORByte(unkGUID, 6);
                packet.ReadSingle("Position X");
                packet.ReadXORByte(unkGUID, 2);
                packet.ReadSingle("Position Z");
                packet.ReadXORByte(unkGUID, 1);
                packet.ReadSingle("Position Y");
                packet.WriteGuid("Guid6", unkGUID);
            }
            packet.ReadXORByte(casterGUID, 4);

            for (var i = 0; i < bits44; ++i)
            {
                packet.ParseBitStream(guid9[i], 4, 2, 0, 6, 1, 7, 3, 5); //18+
                packet.WriteGuid("Guid9", guid9[i]);
            }

            if (hasCastSchoolImmunities) // 101
                packet.ReadUInt32("hasCastSchoolImmunities");

            packet.ReadXORByte(casterGUID, 2);

            if (hasUnkMovementField) // 102
                packet.ReadUInt32("hasUnkMovementField");

            if (hasVisualChain) //380
            {
                packet.ReadInt32("Int174");
                packet.ReadInt32("Int178");
            }

            for (var i = 0; i < predictedPowerCount; ++i)
            {
                packet.ReadInt32("Value", i);
                packet.ReadByteE<PowerType>("Power type", i);
            }

            if (hasRunesStateBefore) //336
                packet.ReadByte("hasRunesStateBefore");

            var flags = packet.ReadInt32E<CastFlag>("Cast Flags"); //10

            packet.ReadXORByte(casterGUID, 5);
            packet.ReadXORByte(casterGUID, 7);
            packet.ReadXORByte(casterGUID, 1);
            packet.ReadByte("CastCount");
            packet.ReadXORByte(casterUnitGUID, 7);
            packet.ReadXORByte(casterUnitGUID, 0);
            packet.ReadXORByte(casterGUID, 6);
            packet.ReadXORByte(casterGUID, 0);
            packet.ReadXORByte(casterUnitGUID, 1);

            if (hasAmmoInventoryType) //368
                packet.ReadByte("AmmoInventoryType");

            if (hasPredictedHeal) //106
                packet.ReadUInt32("hasPredictedHeal");

            packet.ReadXORByte(casterUnitGUID, 6);
            packet.ReadXORByte(casterUnitGUID, 3);

            if (hasSplineElevation) //384
                packet.ReadByte("Byte180");

            if (hasDelayTime) //89
                packet.ReadUInt32("hasDelayTime");

            var spellId = packet.ReadInt32<SpellId>("Spell ID"); //9

            if (hasAmmoDisplayId) //91
                packet.ReadUInt32("hasAmmoDisplayId");

            packet.ReadXORByte(casterUnitGUID, 4);
            packet.ReadXORByte(casterUnitGUID, 5);

            if (hasRunesStateAfter)//337
                packet.ReadByte("hasRunesStateAfter");

            packet.ReadXORByte(casterUnitGUID, 2);

            packet.ReadWoWString("Text", bitsC0);

            if (hasPredictedType) //428
                packet.ReadByte("hasPredictedType");

            packet.ReadXORByte(casterGUID, 3);

            if (hasElevation) //90
                packet.ReadSingle("Elevation");

            for (var i = 0; i < runeCooldownPassedCount; ++i) //340
                packet.ReadByte("runeCooldownPassedCount", i);

            if (flags.HasAnyFlag(CastFlag.Unknown21))
            {
                var spellClick = new NpcSpellClick();

                spellClick.SpellId = (uint)spellId;

                spellClick.CasterGUID = new WowGuid64(BitConverter.ToUInt64(casterGUID, 0));
                spellClick.TargetGUID =new WowGuid64(BitConverter.ToUInt64(targetGUID, 0));

                Storage.SpellClicks.Add(spellClick, packet.TimeSpan);
            }

            packet.WriteGuid("casterUnitGUID", casterUnitGUID);
            packet.WriteGuid("CasterGUID", casterGUID);
            packet.WriteGuid("targetGUID", targetGUID);
            packet.WriteGuid("ItemTargetGUID", itemTargetGUID);
            packet.WriteGuid("UnkGUID", unkGUID);
        }

        [Parser(Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellGo(Packet packet)
        {
            var casterUnitGUID = new byte[8];
            var casterGUID = new byte[8];
            var targetGUID = new byte[8];
            var itemTargetGUID = new byte[8];
            var sourceLocationTargetGUID = new byte[8];
            var unkGUID = new byte[8];
            var destLocationTargetGUID = new byte[8];

            casterGUID[2] = packet.ReadBit();
            var bit190 = !packet.ReadBit();
            var hasSourceLocation = packet.ReadBit();
            casterUnitGUID[2] = packet.ReadBit();

            if (hasSourceLocation)
                packet.StartBitStream(sourceLocationTargetGUID, 3, 7, 4, 2, 0, 6, 1, 5);

            casterUnitGUID[6] = packet.ReadBit();
            var bit1A0 = !packet.ReadBit();
            casterGUID[7] = packet.ReadBit();
            var bits1A4 = (int)packet.ReadBits(20);
            var missTypeCount = packet.ReadBits(25);
            var bits64 = (int)packet.ReadBits(24);
            casterGUID[1] = packet.ReadBit();
            casterUnitGUID[0] = packet.ReadBit();
            var bits4C = (int)packet.ReadBits(13);

            var guid6 = new byte[bits64][];
            for (var i = 0; i < bits64; ++i)
            {
                guid6[i] = new byte[8];
                packet.StartBitStream(guid6[i], 1, 3, 6, 4, 5, 2, 0, 7);
            }

            var guid7 = new byte[bits1A4][];
            for (var i = 0; i < bits1A4; ++i)
            {
                guid7[i] = new byte[8];
                packet.StartBitStream(guid7[i], 2, 5, 6, 1, 4, 0, 7, 3);
            }

            casterGUID[5] = packet.ReadBit();

            packet.ReadBit(); // fake bit
            packet.ReadBit(); // fake bit

            var hasTargetString = !packet.ReadBit();
            packet.StartBitStream(itemTargetGUID, 7, 2, 1, 3, 6, 0, 5, 4);

            casterUnitGUID[7] = packet.ReadBit();
            packet.StartBitStream(targetGUID, 0, 6, 5, 7, 4, 2, 3, 1);

            var hasRunesStateBefore = !packet.ReadBit();
            var predictedPowerCount = packet.ReadBits(21);
            casterUnitGUID[1] = packet.ReadBit();
            var hasPredictedType = !packet.ReadBit();
            var hasTargetMask = !packet.ReadBit();
            casterGUID[3] = packet.ReadBit();

            var targetString = 0u;
            if (hasTargetString)
                targetString = packet.ReadBits(7);

            var hasPredictedHeal = !packet.ReadBit();
            var hasPowerData = packet.ReadBit();
            var bit1B8 = !packet.ReadBit();
            casterGUID[6] = packet.ReadBit();
            packet.ReadBit(); // fake bit
            var hasVisualChain = packet.ReadBit();

            packet.StartBitStream(unkGUID, 7, 6, 1, 2, 0, 5, 3, 4);

            var hasDelayTime = !packet.ReadBit();
            var bit1B4 = !packet.ReadBit();
            var runeCooldownPassedCount = packet.ReadBits(3);

            casterGUID[0] = packet.ReadBit();

            for (var i = 0; i < missTypeCount; ++i)
            {
                if (packet.ReadBits("bits22[0]", 4, i) == 11)
                    packet.ReadBits("bits22[1]", 4, i);
            }

            if (hasTargetMask)
                packet.ReadBitsE<TargetFlag>("Target Flags", 20);

            var hasElevation = !packet.ReadBit();
            var hasRunesStateAfter = !packet.ReadBit();

            var powerCount = 0u;
            if (hasPowerData)
                predictedPowerCount = packet.ReadBits(21);

            casterUnitGUID[4] = packet.ReadBit();
            var bit18C = !packet.ReadBit();
            var hasDestLocation = packet.ReadBit();
            casterUnitGUID[5] = packet.ReadBit();
            var bits54 = (int)packet.ReadBits(24);

            if (hasDestLocation)
                packet.StartBitStream(destLocationTargetGUID, 0, 3, 2, 1, 4, 5, 6, 7);

            casterGUID[4] = packet.ReadBit();

            var guid8 = new byte[bits54][];
            for (var i = 0; i < bits54; ++i)
            {
                guid8[i] = new byte[8];
                packet.StartBitStream(guid8[i], 2, 7, 1, 6, 4, 5, 0, 3);
            }

            casterUnitGUID[3] = packet.ReadBit();

            packet.ParseBitStream(targetGUID, 5, 2, 1, 6, 0, 3, 4, 7);
            packet.ParseBitStream(itemTargetGUID, 5, 2, 0, 6, 7, 3, 1, 4);

            packet.ReadXORByte(casterUnitGUID, 2);

            for (var i = 0; i < bits1A4; ++i)
            {
                packet.ReadXORByte(guid7[i], 3);
                packet.ReadXORByte(guid7[i], 1);
                packet.ReadXORByte(guid7[i], 0);
                packet.ReadXORByte(guid7[i], 4);
                packet.ReadXORByte(guid7[i], 7);
                packet.ReadSingle("FloatED", i);
                packet.ReadXORByte(guid7[i], 5);
                packet.ReadSingle("FloatED", i);
                packet.ReadXORByte(guid7[i], 6);
                packet.ReadSingle("FloatED", i);
                packet.ReadXORByte(guid7[i], 2);

                packet.WriteGuid("Guid7", guid7[i], i);
            }

            for (var i = 0; i < bits54; ++i)
            {
                packet.ParseBitStream(guid8[i], 0, 6, 2, 7, 5, 4, 3, 1);
                packet.WriteGuid("Guid8", guid8[i], i);
            }

            packet.ParseBitStream(unkGUID, 6, 2, 7, 1, 4, 3, 5, 0);
            if (hasDelayTime)
                packet.ReadUInt32("hasDelayTime");

            packet.ReadInt32("Int50");
            for (var i = 0; i < bits64; ++i)
            {
                packet.ParseBitStream(guid6[i], 4, 2, 0, 6, 7, 5, 1, 3);
                packet.WriteGuid("Guid6", guid6[i], i);
            }

            if (hasDestLocation)
            {
                var pos = new Vector3();

                pos.Z = packet.ReadSingle();
                pos.Y = packet.ReadSingle();
                packet.ReadXORByte(destLocationTargetGUID, 4);
                packet.ReadXORByte(destLocationTargetGUID, 5);
                packet.ReadXORByte(destLocationTargetGUID, 7);
                packet.ReadXORByte(destLocationTargetGUID, 6);
                packet.ReadXORByte(destLocationTargetGUID, 1);
                packet.ReadXORByte(destLocationTargetGUID, 2);
                pos.X = packet.ReadSingle();
                packet.ReadXORByte(destLocationTargetGUID, 0);
                packet.ReadXORByte(destLocationTargetGUID, 3);
                packet.AddValue("Position", pos);
                packet.WriteGuid("DestLocationTargetGUID", destLocationTargetGUID);
            }

            packet.ReadXORByte(casterUnitGUID, 6);
            packet.ReadXORByte(casterGUID, 7);
            packet.ReadXORByte(casterUnitGUID, 1);
            if (hasPowerData)
            {
                packet.ReadInt32("Int10");
                packet.ReadInt32("Int14");

                for (var i = 0; i < powerCount; ++i)
                {
                    packet.ReadInt32("IntED", i);
                    packet.ReadInt32("IntED", i);
                }

                packet.ReadInt32("Int18");
            }

            if (hasVisualChain)
            {
                packet.ReadInt32("Int198");
                packet.ReadInt32("Int194");
            }

            packet.ReadInt32E<CastFlag>("Cast Flags");

            if (hasSourceLocation)
            {
                var pos = new Vector3();

                packet.ReadXORByte(sourceLocationTargetGUID, 2);
                pos.Y = packet.ReadSingle();
                pos.X = packet.ReadSingle();
                packet.ReadXORByte(sourceLocationTargetGUID, 6);
                packet.ReadXORByte(sourceLocationTargetGUID, 5);
                packet.ReadXORByte(sourceLocationTargetGUID, 1);
                packet.ReadXORByte(sourceLocationTargetGUID, 7);
                pos.Z = packet.ReadSingle();
                packet.ReadXORByte(sourceLocationTargetGUID, 3);
                packet.ReadXORByte(sourceLocationTargetGUID, 0);
                packet.ReadXORByte(sourceLocationTargetGUID, 4);
                packet.AddValue("Position", pos);
                packet.WriteGuid("SourceLocationTargetGUID", sourceLocationTargetGUID);
            }

            packet.ReadXORByte(casterGUID, 6);

            if (hasPredictedType)
                packet.ReadByte("PredictedType");

            packet.ReadXORByte(casterUnitGUID, 4);
            if (bit1B4)
                packet.ReadInt32("Int1B4");
            if (bit1B8)
                packet.ReadInt32("Int1B8");
            if (bit18C)
                packet.ReadInt32("Int18C");
            packet.ReadXORByte(casterGUID, 1);

            if (bit190)
                packet.ReadByte("Byte190");

            for (var i = 0; i < predictedPowerCount; ++i)
            {
                packet.ReadByteE<PowerType>("Power type", i);
                packet.ReadInt32("Value", i);
            }

            if (hasRunesStateAfter)
                packet.ReadByte("RuneState");

            for (var i = 0; i < runeCooldownPassedCount; ++i)
                packet.ReadByte("runeCooldownPassedCount", i);

            if (hasRunesStateBefore)
                packet.ReadByte("RuneState");

            packet.ReadXORByte(casterUnitGUID, 0);

            if (bit1A0)
                packet.ReadByte("Byte1A0");

            if (hasPredictedHeal)
                packet.ReadUInt32("PredictedHeal");

            packet.ReadByte("CastCount");
            packet.ReadXORByte(casterUnitGUID, 5);
            packet.ReadXORByte(casterGUID, 2);
            packet.ReadXORByte(casterUnitGUID, 3);
            packet.ReadXORByte(casterGUID, 5);

            if (hasTargetString)
                packet.ReadWoWString("targetString", targetString);

            packet.ReadInt32<SpellId>("Spell ID");

            if (hasElevation)
                packet.ReadSingle("Elevation");

            packet.ReadXORByte(casterGUID, 0);
            packet.ReadXORByte(casterGUID, 3);
            packet.ReadXORByte(casterGUID, 4);

            packet.ReadXORByte(casterUnitGUID, 7);

            packet.WriteGuid("casterUnitGUID", casterUnitGUID);
            packet.WriteGuid("CasterGUID", casterGUID);
            packet.WriteGuid("targetGUID", targetGUID);
            packet.WriteGuid("ItemTargetGUID", itemTargetGUID);
            packet.WriteGuid("UnkGUID", unkGUID);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            var guid = new byte[8];
            var bits0 = 0;

            guid[7] = packet.ReadBit();
            var bit28 = packet.ReadBit(); // +40
            bits0 = (int)packet.ReadBits(24); //+24
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[5] = packet.ReadBit();

            var hasAura = new bool[bits0];
            var hasCasterGUID = new bool[bits0];
            var casterGUID = new byte[bits0][];
            var effectCount = new uint[bits0];
            var hasMaxDuration = new bool[bits0];
            var hasDuration = new bool[bits0];
            var bits48 = new uint[bits0];

            for (var i = 0; i < bits0; ++i)
            {
                hasAura[i] = packet.ReadBit(); // +88
                if (hasAura[i])
                {
                    effectCount[i] = packet.ReadBits(22);
                    hasCasterGUID[i] = packet.ReadBit();
                    if (hasCasterGUID[i])
                    {
                        casterGUID[i] = new byte[8];
                        packet.StartBitStream(casterGUID[i], 3, 4, 6, 1, 5, 2, 0, 7);
                    }

                    bits48[i] = packet.ReadBits(22); //+72
                    hasMaxDuration[i] = packet.ReadBit(); //+52
                    hasDuration[i] = packet.ReadBit(); //+44
                }
            }

            var auras = new List<Aura>();
            for (var i = 0; i < bits0; ++i)
            {
                if (hasAura[i])
                {
                    var aura = new Aura();
                    if (hasCasterGUID[i])
                    {
                        packet.ParseBitStream(casterGUID[i], 3, 2, 1, 6, 4, 0, 5, 7);
                        packet.WriteGuid("CasterGUID", casterGUID[i], i);
                        aura.CasterGuid = new WowGuid64(BitConverter.ToUInt64(casterGUID[i], 0));
                    }

                    aura.AuraFlags = packet.ReadByteE<AuraFlagMoP>("Flags", i);
                    aura.Level = packet.ReadUInt16("Caster Level", i);
                    var id = packet.ReadInt32<SpellId>("Spell ID", i);
                    aura.SpellId = (uint)id;

                    aura.MaxDuration = hasMaxDuration[i] ? packet.ReadInt32("Max Duration", i) : 0;

                    aura.Duration = hasDuration[i] ? packet.ReadInt32("Duration", i) : 0;

                    for (var j = 0; j < bits48[i]; ++j)
                        packet.ReadSingle("FloatEM", i, j);

                    aura.Charges = packet.ReadByte("Charges", i);
                    packet.ReadInt32("Effect Mask", i);

                    for (var j = 0; j < effectCount[i]; ++j)
                        packet.ReadSingle("Effect Value", i, j);

                    auras.Add(aura);
                    packet.AddSniffData(StoreNameType.Spell, (int)aura.SpellId, "AURA_UPDATE");
                }

                packet.ReadByte("Slot", i);
            }

            packet.ParseBitStream(guid, 2, 6, 7, 1, 3, 4, 0, 5);
            var GUID = packet.WriteGuid("Guid", guid);

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

        [Parser(Opcode.SMSG_LEARNED_SPELLS)]
        public static void HandleLearnedSpells(Packet packet)
        {
            var count = packet.ReadBits("Spell Count", 22);
            packet.ReadBit("SuppressMessaging");

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

        [Parser(Opcode.SMSG_SEND_UNLEARN_SPELLS)]
        public static void HandleSendUnlearnSpells(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);

            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32("Unk Int32", i);
                packet.ReadByte("Unk Byte", i);
                packet.ReadInt32<SpellId>("Spell ID", i);
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

        [Parser(Opcode.SMSG_CAST_FAILED)]
        public static void HandleCastFailed(Packet packet)
        {
            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadInt32E<SpellCastFailureReason>("Reason");
            packet.ReadByte("Cast count");

            var bit14 = !packet.ReadBit();
            var bit18 = !packet.ReadBit();

            if (bit14)
                packet.ReadInt32("Int14");

            if (bit18)
                packet.ReadInt32("Int18");
        }

        [Parser(Opcode.SMSG_SPELL_FAILURE)]
        public static void HandleSpellFailed(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 7, 3, 6, 2, 1, 5, 0, 4);

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadByte("Cast count");
            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadByteE<SpellCastFailureReason>("Reason");
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPELL_FAILED_OTHER)]
        public static void HandleSpellFailedOther(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 7, 0, 5, 6, 1, 4, 3, 2);

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 1);
            packet.ReadByteE<SpellCastFailureReason>("Reason");
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);
            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadByte("Cast count");
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 3);

            packet.WriteGuid("Guid", guid);
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
                for (var j = 0; j < spentTalents[i]; ++j)
                    packet.ReadUInt16("Talent Id", i, j);

                for (var j = 0; j < 6; ++j)
                    packet.ReadUInt16("Glyph", i, j);

                packet.ReadUInt32("Spec Id", i);
            }
        }

        [Parser(Opcode.CMSG_CANCEL_CAST)]
        public static void HandlePlayerCancelCast(Packet packet)
        {
            var hasCount = !packet.ReadBit();
            var hasSpellId = !packet.ReadBit();
            if (hasSpellId)
                packet.ReadUInt32<SpellId>("Spell Id");
            if (hasCount)
                packet.ReadByte("Count");
        }

        [Parser(Opcode.CMSG_CANCEL_MOUNT_AURA)]
        public static void HandleCancelMountAura(Packet packet)
        {
            packet.ReadUInt32("Unk");
        }

        [Parser(Opcode.SMSG_SPELL_CHANNEL_START)]
        public static void HandleSpellChannelStart(Packet packet)
        {
            var targetGUD = new byte[8];
            var guid2 = new byte[8];
            var casterGUID = new byte[8];

            var hasHealAmount = false;
            var hasType = false;

            casterGUID[7] = packet.ReadBit();
            casterGUID[5] = packet.ReadBit();
            casterGUID[4] = packet.ReadBit();
            casterGUID[1] = packet.ReadBit();

            var healPrediction = packet.ReadBit();
            if (healPrediction)
            {
                targetGUD[2] = packet.ReadBit();
                targetGUD[6] = packet.ReadBit();
                targetGUD[4] = packet.ReadBit();

                hasType = !packet.ReadBit();

                targetGUD[3] = packet.ReadBit();
                targetGUD[7] = packet.ReadBit();
                targetGUD[5] = packet.ReadBit();
                targetGUD[1] = packet.ReadBit();
                targetGUD[2] = packet.ReadBit();

                hasHealAmount = !packet.ReadBit();
                packet.ReadBit(); // fake bit

                packet.StartBitStream(guid2, 4, 5, 1, 7, 0, 2, 3, 6);

            }

            casterGUID[3] = packet.ReadBit();
            casterGUID[2] = packet.ReadBit();
            casterGUID[0] = packet.ReadBit();
            casterGUID[6] = packet.ReadBit();

            var hasImmunity = packet.ReadBit();
            if (healPrediction)
            {
                packet.ParseBitStream(guid2, 4, 6, 1, 0, 7, 3, 2, 5);

                if (hasType)
                    packet.ReadByte("Type");

                packet.ReadXORByte(targetGUD, 4);
                packet.ReadXORByte(targetGUD, 5);
                packet.ReadXORByte(targetGUD, 1);
                packet.ReadXORByte(targetGUD, 3);

                if (hasHealAmount)
                    packet.ReadInt32("Heal Amount");

                packet.ReadXORByte(targetGUD, 6);
                packet.ReadXORByte(targetGUD, 7);
                packet.ReadXORByte(targetGUD, 2);
                packet.ReadXORByte(targetGUD, 0);

                packet.WriteGuid("TargetGUD", targetGUD);
                packet.WriteGuid("Guid2", guid2);
            }

            if (hasImmunity)
            {
                packet.ReadInt32("CastSchoolImmunities");
                packet.ReadInt32("CastImmunities");
            }

            packet.ReadXORByte(casterGUID, 6);
            packet.ReadXORByte(casterGUID, 7);
            packet.ReadXORByte(casterGUID, 3);
            packet.ReadXORByte(casterGUID, 1);
            packet.ReadXORByte(casterGUID, 0);

            packet.ReadInt32("Duration");

            packet.ReadXORByte(casterGUID, 5);
            packet.ReadXORByte(casterGUID, 4);
            packet.ReadXORByte(casterGUID, 2);

            packet.ReadUInt32<SpellId>("Spell ID");

            packet.WriteGuid("CasterGUID", casterGUID);
        }

        [Parser(Opcode.SMSG_SPELL_CHANNEL_UPDATE)]
        public static void HandleSpellChannelUpdate(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 0, 3, 4, 1, 5, 2, 6, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 5);
            packet.ReadInt32("Timestamp");
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_CANCEL_AURA)]
        public static void HandleCanelAura(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadBit(); // fake bit?
            packet.StartBitStream(guid, 6, 5, 1, 0, 4, 3, 2, 7);
            packet.ParseBitStream(guid, 3, 2, 1, 0, 4, 7, 5, 6);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_SPELL_EXECUTE_LOG)]
        public static void HandleSpellLogExecute(Packet packet)
        {
            var guid = new byte[8];
            byte[][] guid2;
            byte[][][] guid3 = null;
            byte[][][] guid4 = null;
            byte[][][] guid5 = null;
            byte[][][] guid6 = null;

            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var bits10 = packet.ReadBits(19);
            guid[4] = packet.ReadBit();

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
                guid3[i] = new byte[bits14[i]][];
                for (var j = 0; j < bits14[i]; ++j)
                {
                    guid3[i][j] = new byte[8];
                    packet.StartBitStream(guid3[i][j], 5, 4, 2, 3, 1, 0, 6, 7);
                }

                bits4[i] = packet.ReadBits(20);
                guid6[i] = new byte[bits4[i]][];
                for (var j = 0; j < bits4[i]; ++j)
                {
                    guid6[i][j] = new byte[8];
                    packet.StartBitStream(guid6[i][j], 0, 3, 1, 5, 6, 4, 7, 2);
                }

                bits24[i] = packet.ReadBits(21);
                bits54[i] = packet.ReadBits(22);
                bits44[i] = packet.ReadBits(22);

                guid4[i] = new byte[bits24[i]][];
                for (var j = 0; j < bits24[i]; ++j)
                {
                    guid4[i][j] = new byte[8];
                    packet.StartBitStream(guid4[i][j], 7, 2, 0, 5, 6, 3, 4, 1);
                }

                bits34[i] = packet.ReadBits(24);
                guid5[i] = new byte[bits34[i]][];
                for (var j = 0; j < bits34[i]; ++j)
                {
                    guid5[i][j] = new byte[8];
                    packet.StartBitStream(guid5[i][j], 6, 5, 1, 0, 3, 4, 7, 2);
                }
            }

            guid[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();

            var bits38 = 0u;
            var bit48 = packet.ReadBit();
            if (bit48)
                bits38 = packet.ReadBits(21);

            if (bit48)
            {
                for (var i = 0; i < bits38; ++i)
                {
                    packet.ReadInt32("Int38+4", i);
                    packet.ReadInt32("Int38+8", i);
                }

                packet.ReadInt32("Int30");
                packet.ReadInt32("Int2C");
                packet.ReadInt32("Int34");
            }

            for (var i = 0; i < bits10; ++i)
            {
                for (var j = 0; j < bits34[i]; ++j)
                {
                    packet.ParseBitStream(guid5[i][j], 7, 5, 1, 2, 6, 4, 0, 3);
                    packet.WriteGuid("Summoned GUID", guid5[i][j], i, j);
                }

                for (var j = 0; j < bits24[i]; ++j)
                {
                    packet.ReadXORByte(guid4[i][j], 4);
                    packet.ReadXORByte(guid4[i][j], 3);
                    packet.ReadInt32("Int24+0", i, j);
                    packet.ReadXORByte(guid4[i][j], 6);
                    packet.ReadXORByte(guid4[i][j], 5);
                    packet.ReadXORByte(guid4[i][j], 0);
                    packet.ReadXORByte(guid4[i][j], 7);
                    packet.ReadInt32("Int24+4", i, j);
                    packet.ReadXORByte(guid4[i][j], 2);
                    packet.ReadXORByte(guid4[i][j], 1);

                    packet.WriteGuid("Guid4", guid4[i][j], i, j);
                }

                for (var j = 0; j < bits4[i]; ++j)
                {
                    packet.ReadXORByte(guid6[i][j], 3);
                    packet.ReadXORByte(guid6[i][j], 7);
                    packet.ReadXORByte(guid6[i][j], 5);
                    packet.ReadXORByte(guid6[i][j], 2);
                    packet.ReadXORByte(guid6[i][j], 0);
                    packet.ReadInt32("Int4+0", i, j);
                    packet.ReadInt32("Int4+8", i, j);
                    packet.ReadXORByte(guid6[i][j], 4);
                    packet.ReadXORByte(guid6[i][j], 1);
                    packet.ReadSingle("FloatEB", i, j);
                    packet.ReadXORByte(guid6[i][j], 6);

                    packet.WriteGuid("Guid6", guid6[i][j], i, j);
                }

                for (var j = 0; j < bits14[i]; ++j)
                {
                    packet.ReadXORByte(guid3[i][j], 0);
                    packet.ReadXORByte(guid3[i][j], 6);
                    packet.ReadXORByte(guid3[i][j], 4);
                    packet.ReadXORByte(guid3[i][j], 7);
                    packet.ReadXORByte(guid3[i][j], 2);
                    packet.ReadXORByte(guid3[i][j], 5);
                    packet.ReadXORByte(guid3[i][j], 3);
                    packet.ReadInt32("Int14");
                    packet.ReadXORByte(guid3[i][j], 1);

                    packet.WriteGuid("Guid3", guid3[i][j], i, j);
                }

                for (var j = 0; j < bits54[i]; ++j)
                    packet.ReadInt32("Int54", i, j);

                var type = packet.ReadInt32E<SpellEffect>("Spell Effect", i);

                for (var j = 0; j < bits44[i]; ++j)
                    packet.ReadInt32<ItemId>("Created Item", i, j);
            }

            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ParseBitStream(guid, 5, 7, 1, 6, 2, 0, 4, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_CATEGORY_COOLDOWN)]
        public static void HandleSpellCategoryCooldown(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Cooldown", i);
                packet.ReadInt32("Category Cooldown", i);
            }
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL_KIT)]
        public static void HandleCastVisualKit(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 4, 2, 6, 5, 1, 3, 0, 7);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadUInt32("Unk 1");
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 6);
            packet.ReadUInt32("Unk 2");
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 3);
            packet.ReadUInt32("SpellVisualKit ID");

            packet.WriteGuid("Guid2", guid);
        }
    }
}

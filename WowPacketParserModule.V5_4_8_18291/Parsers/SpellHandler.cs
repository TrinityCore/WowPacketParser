using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
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

            packet.Translator.ReadBit();                               // v4 + 1
            var hasTargetString = !packet.Translator.ReadBit();        // v2 + 120
            packet.Translator.ReadBit();                               // v2 + 48
            var hasCastCount = !packet.Translator.ReadBit();           // v2 + 16
            var hasSrcLocation = packet.Translator.ReadBit();          // v2 + 80
            var hasDestLocation = packet.Translator.ReadBit();         // v2 + 112
            var hasSpellId = !packet.Translator.ReadBit();             // v2 + 20

            var archeologyCounter = packet.Translator.ReadBits(2);     // v2 + 424

            var hasTargetMask = !packet.Translator.ReadBit();          // v2 + 32

            var hasMissileSpeed = !packet.Translator.ReadBit();

            for (var i = 0; i < archeologyCounter; ++i)
                packet.Translator.ReadBits("archeologyType", 2, i);

            var hasGlyphIndex = !packet.Translator.ReadBit();          // v2 + 24
            var hasMovement = packet.Translator.ReadBit();             // v2 + 416
            var hasElevation = !packet.Translator.ReadBit();
            var hasCastFlags = !packet.Translator.ReadBit();           // v2 + 28

            packet.Translator.StartBitStream(guid1, 5, 4, 2, 7, 1, 6, 3, 0);

            if (hasDestLocation)
                packet.Translator.StartBitStream(guid2, 1, 3, 5, 0, 2, 6, 7, 4);


            var unkMovementLoopCounter = 0u;
            if (hasMovement)
            {
                unkMovementLoopCounter = packet.Translator.ReadBits(22);

                bit388 = packet.Translator.ReadBit(); // v2 + 388
                guid20[4] = packet.Translator.ReadBit(); // v2 + 260

                hasTransport = packet.Translator.ReadBit(); // v2 + 344
                if (hasTransport)
                {
                    hasTransportTime2 = packet.Translator.ReadBit();   // v2 + 332
                    transportGUID[7] = packet.Translator.ReadBit();    // v2 + 303
                    transportGUID[5] = packet.Translator.ReadBit();    // v2 + 300
                    transportGUID[1] = packet.Translator.ReadBit();    // v2 + 297
                    transportGUID[0] = packet.Translator.ReadBit();    // v2 + 296
                    transportGUID[6] = packet.Translator.ReadBit();    // v2 + 302
                    transportGUID[3] = packet.Translator.ReadBit();    // v2 + 299
                    transportGUID[5] = packet.Translator.ReadBit();    // v2 + 301
                    hasTransportTime3 = packet.Translator.ReadBit();   // v2 + 340
                    transportGUID[2] = packet.Translator.ReadBit();    // v2 + 2980
                }

                bit389 = packet.Translator.ReadBit();                  // v2 + 389
                guid20[7] = packet.Translator.ReadBit();               // v2 + 263
                hasOrientation = !packet.Translator.ReadBit();
                guid20[6] = packet.Translator.ReadBit();               // v2 + 263
                hasSplineElevation = !packet.Translator.ReadBit();
                hasPitch = !packet.Translator.ReadBit();
                guid20[0] = packet.Translator.ReadBit();               // v2 + 263
                bit412 = packet.Translator.ReadBit();                  // v2 + 412
                hasMovementFlags = !packet.Translator.ReadBit();       // v2 + 264
                hasTimestamp = !packet.Translator.ReadBit();           // v2 + bit272
                hasUnkMovementField = !packet.Translator.ReadBit();    // v2 + 264

                if (hasMovementFlags)
                    packet.Translator.ReadBits("hasMovementFlags", 30);

                guid20[1] = packet.Translator.ReadBit();               // v2 + 257
                guid20[3] = packet.Translator.ReadBit();               // v2 + 259
                guid20[2] = packet.Translator.ReadBit();               // v2 + 258
                guid20[5] = packet.Translator.ReadBit();               // v2 + 261

                hasFallData = packet.Translator.ReadBit();
                if (hasFallData)
                    hasFallDirection = packet.Translator.ReadBit();

                hasMovementFlags2 = !packet.Translator.ReadBit();
                if (hasMovementFlags2)
                    packet.Translator.ReadBits("hasMovementFlags2", 13);
            }

            packet.Translator.StartBitStream(guid3, 1, 0, 7, 4, 6, 5, 3, 2);

            if (hasSrcLocation)
                packet.Translator.StartBitStream(guid4, 4, 5, 3, 0, 7, 1, 6, 2);

            if (hasTargetMask)
                packet.Translator.ReadBitsE<TargetFlag>("Target Flags", 20);

            if (hasCastFlags)
                packet.Translator.ReadBits("hasCastFlags", 5);

            if (hasTargetString)
                targetString = packet.Translator.ReadBits("hasTargetString", 7);

            for (var i = 0; i < archeologyCounter; ++i)
            {
                packet.Translator.ReadUInt32("unk1", i);
                packet.Translator.ReadUInt32("unk2", i);
            }

            if (hasMovement)
            {
                packet.Translator.ReadSingle("Position X");      // v2 + 276
                packet.Translator.ReadXORByte(guid20, 0);      // v2 + 256

                if (hasTransport)
                {
                    packet.Translator.ReadXORByte(transportGUID, 2);   // v2 + 298
                    packet.Translator.ReadByte("Transport Seat");      // v2 + 320
                    packet.Translator.ReadXORByte(transportGUID, 3);   // v2 + 299
                    packet.Translator.ReadXORByte(transportGUID, 7);   // v2 + 303
                    packet.Translator.ReadSingle("float304");          // v2 + 304
                    packet.Translator.ReadXORByte(transportGUID, 5);   // v2 + 301

                    if (hasTransportTime3)
                        packet.Translator.ReadInt32("hasTransportTime3");


                    packet.Translator.ReadSingle("float312");          // v2 + 312
                    packet.Translator.ReadSingle("float308");          // v2 + 308
                    packet.Translator.ReadXORByte(transportGUID, 6);   // v2 + 302
                    packet.Translator.ReadXORByte(transportGUID, 1);   // v2 + 297
                    packet.Translator.ReadSingle("float316");          // v2 + 316
                    packet.Translator.ReadXORByte(transportGUID, 4);   // v2 + 300

                    if (hasTransportTime2)
                        packet.Translator.ReadInt32("hasTransportTime2");

                    packet.Translator.ReadXORByte(transportGUID, 0);   // v2 + 296

                    packet.Translator.ReadInt32("Transport Time");     // v2 + 324

                    packet.Translator.WriteGuid("Transport GUID", transportGUID);
                }

                packet.Translator.ReadXORByte(guid20, 5);      // v2 + 256

                if (hasFallData)
                {
                    packet.Translator.ReadInt32("FallTime");
                    packet.Translator.ReadSingle("Z Speed");

                    if (hasFallDirection)
                    {
                        packet.Translator.ReadSingle("SinAngle");
                        packet.Translator.ReadSingle("XY Speed");
                        packet.Translator.ReadSingle("CosAngle");
                    }
                }

                if (hasSplineElevation)
                    packet.Translator.ReadSingle("SplineElevation");   // v2 + 384

                packet.Translator.ReadXORByte(guid20, 6);      // v2 + 262

                if (hasUnkMovementField)
                    packet.Translator.ReadInt32("Int408");     // v2 + 408

                packet.Translator.ReadXORByte(guid20, 4);      // v2 + 260

                if (hasOrientation)
                    packet.Translator.ReadSingle("Orientation");   // v2 + 288

                if (hasTimestamp)
                    packet.Translator.ReadInt32("hasTimestamp");   // v2 + 288

                packet.Translator.ReadXORByte(guid20, 1);      // v2 + 257

                if (hasPitch)
                    packet.Translator.ReadSingle("Pitch");     // v2 + 352

                packet.Translator.ReadXORByte(guid20, 3);      // v2 + 260

                for (var i = 0; i < unkMovementLoopCounter; ++i)
                    packet.Translator.ReadInt32("MovementLoopCounter", i);

                packet.Translator.ReadSingle("Position Y");    // v2 + 280
                packet.Translator.ReadXORByte(guid20, 7);      // v2 + 260
                packet.Translator.ReadSingle("Position Z");    // v2 + 284
                packet.Translator.ReadXORByte(guid20, 2);      // v2 + 258

                packet.Translator.WriteGuid("Guid20", guid20);
            }

            packet.Translator.ParseBitStream(guid3, 4, 2, 1, 5, 7, 3, 6, 0);

            if (hasDestLocation)
            {
                packet.Translator.ReadXORByte(guid2, 2);
                packet.Translator.ReadSingle("Position X");
                packet.Translator.ReadXORByte(guid2, 4);
                packet.Translator.ReadXORByte(guid2, 1);
                packet.Translator.ReadXORByte(guid2, 0);
                packet.Translator.ReadXORByte(guid2, 3);
                packet.Translator.ReadSingle("Position Y");
                packet.Translator.ReadXORByte(guid2, 7);
                packet.Translator.ReadSingle("Position Z");
                packet.Translator.ReadXORByte(guid2, 5);
                packet.Translator.ReadXORByte(guid2, 6);

                packet.Translator.WriteGuid("Guid2", guid2);
            }

            packet.Translator.ParseBitStream(guid1, 3, 4, 7, 6, 2, 0, 1, 5);

            if (hasSrcLocation)
            {
                packet.Translator.ReadSingle("Position Y");
                packet.Translator.ReadXORByte(guid4, 5);
                packet.Translator.ReadXORByte(guid4, 1);
                packet.Translator.ReadXORByte(guid4, 7);
                packet.Translator.ReadXORByte(guid4, 6);
                packet.Translator.ReadSingle("Position X");
                packet.Translator.ReadXORByte(guid4, 3);
                packet.Translator.ReadXORByte(guid4, 2);
                packet.Translator.ReadXORByte(guid4, 0);
                packet.Translator.ReadXORByte(guid4, 4);
                packet.Translator.ReadSingle("Position Z");

                packet.Translator.WriteGuid("Guid4", guid4);
            }

            if (hasTargetString)
                packet.Translator.ReadWoWString("TargetString", targetString);

            if (hasMissileSpeed)
                packet.Translator.ReadSingle("missileSpeed");

            if (hasElevation)
                packet.Translator.ReadSingle("hasElevation");

            if (hasCastCount)
                packet.Translator.ReadByte("Cast Count");

            if (hasSpellId)
                packet.Translator.ReadInt32<SpellId>("Spell ID");

            if (hasGlyphIndex)
                packet.Translator.ReadInt32("hasGlyphIndex");
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

            uint bits44 = packet.Translator.ReadBits("bits44", 24);
            casterGUID[5] = packet.Translator.ReadBit();

            var guid9 = new byte[bits44][]; //18+
            for (int i = 0; i < bits44; ++i)
            {
                guid9[i] = new byte[8];
                packet.Translator.StartBitStream(guid9[i], 1, 0, 7, 2, 4, 3, 6, 5);
            }

            bool hasSplineElevation = !packet.Translator.ReadBit("hasSplineElevation");//384
            packet.Translator.ReadBit(); //fake bit
            casterUnitGUID[4] = packet.Translator.ReadBit();
            casterGUID[2] = packet.Translator.ReadBit();
            int runeCooldownPassedCount = (int)packet.Translator.ReadBits(3);//340
            casterUnitGUID[2] = packet.Translator.ReadBit();
            casterUnitGUID[6] = packet.Translator.ReadBit();
            int missTypeCount = (int)packet.Translator.ReadBits(25); //84
            int bits11 = (int)packet.Translator.ReadBits(13);
            casterGUID[4] = packet.Translator.ReadBit();
            int hitcountPos = (int)packet.Translator.ReadBits(24); //52
            casterUnitGUID[7] = packet.Translator.ReadBit();

            var guid11 = new byte[hitcountPos][]; //14+
            for (int i = 0; i < hitcountPos; ++i)
            {
                guid11[i] = new byte[8];
                packet.Translator.StartBitStream(guid11[i], 5, 0, 3, 4, 7, 2, 6, 1);
            }

            Bit hasSourceLocation = packet.Translator.ReadBit("hasSourceLocation"); //152
            int predictedPowerCount = (int)packet.Translator.ReadBits(21); //320
            packet.Translator.StartBitStream(itemTargetGUID, 3, 0, 1, 7, 2, 6, 4, 5); //120-127
            bool hasElevation = !packet.Translator.ReadBit("hasElevation"); //90
            bool hasTargetString = !packet.Translator.ReadBit("hasTargetString");
            bool hasAmmoInventoryType = !packet.Translator.ReadBit("hasAmmoInventoryType");//368
            Bit hasDestLocation = packet.Translator.ReadBit("hasDestLocation");//184
            bool hasDelayTime = !packet.Translator.ReadBit("hasDelayTime"); //89
            casterGUID[3] = packet.Translator.ReadBit();

            if (hasDestLocation)
                packet.Translator.StartBitStream(guid4, 1, 6, 2, 7, 0, 3, 5, 4); //160-167

            bool hasAmmoDisplayId = !packet.Translator.ReadBit("hasAmmoDisplayId"); //91

            if (hasSourceLocation)
                packet.Translator.StartBitStream(guid5, 4, 3, 5, 1, 7, 0, 6, 2); //128-135

            packet.Translator.ReadBit(); //fake bit 104
            casterGUID[6] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(unkGUID, 2, 1, 7, 6, 0, 5, 3, 4); //416-423
            bool hasTargetFlags = !packet.Translator.ReadBit("hasTargetFlags");

            if (hasTargetFlags)
                packet.Translator.ReadBitsE<TargetFlag>("Target Flags", 20);

            casterGUID[1] = packet.Translator.ReadBit();
            bool hasPredictedHeal = !packet.Translator.ReadBit("hasPredictedHeal"); //106
            bool hasRunesStateBefore = !packet.Translator.ReadBit("hasRunesStateBefore"); //336
            bool hasCastSchoolImmunities = !packet.Translator.ReadBit("hasCastSchoolImmunities"); //101
            casterUnitGUID[5] = packet.Translator.ReadBit();
            packet.Translator.ReadBit(); //28 fake bit
            int extraTargetCount = (int)packet.Translator.ReadBits("extraTargetCount", 20);//388

            var guid10 = new byte[extraTargetCount][];
            for (int i = 0; i < extraTargetCount; ++i) //388
            {
                guid10[i] = new byte[8];
                packet.Translator.StartBitStream(guid10[i], 1, 6, 2, 3, 5, 7, 0, 4); //98+
            }
            packet.Translator.StartBitStream(targetGUID, 1, 4, 6, 7, 5, 3, 0, 2); //112-119
            casterGUID[0] = packet.Translator.ReadBit();
            casterUnitGUID[3] = packet.Translator.ReadBit();
            bool hasRunesStateAfter = !packet.Translator.ReadBit("hasRunesStateAfter");//337

            uint bitsC0 = 0u;
            if (hasTargetString)
                bitsC0 = packet.Translator.ReadBits(7);//192

            for (int i = 0; i < missTypeCount; ++i)
            {
                if (packet.Translator.ReadBits("bits22[0]", 4, i) == 11)
                    packet.Translator.ReadBits("bits22[1]", 4, i);
            }
            bool hasUnkMovementField = !packet.Translator.ReadBit("hasUnkMovementField"); // 102
            casterUnitGUID[1] = packet.Translator.ReadBit();
            Bit hasVisualChain = packet.Translator.ReadBit("hasVisualChain");//380
            casterGUID[7] = packet.Translator.ReadBit();
            bool hasPredictedType = !packet.Translator.ReadBit("hasPredictedType");//428
            casterUnitGUID[0] = packet.Translator.ReadBit();

            packet.Translator.ParseBitStream(itemTargetGUID, 1, 7, 6, 0, 4, 2, 3, 5); //120-127
            packet.Translator.WriteGuid("Guid3", itemTargetGUID);

            for (int i = 0; i < hitcountPos; ++i)
            {
                packet.Translator.ParseBitStream(guid11[i], 4, 5, 3, 0, 6, 2, 1, 7);//14+
                packet.Translator.WriteGuid("Guid11", guid11[i]);
            }
            packet.Translator.ParseBitStream(targetGUID, 4, 5, 1, 7, 6, 3, 2, 0); //112-119
            packet.Translator.WriteGuid("Guid7", targetGUID);

            packet.Translator.ReadInt32("CastTime"); //12

            packet.Translator.ParseBitStream(unkGUID, 4, 5, 3, 2, 1, 6, 7, 0); //416-423
            packet.Translator.WriteGuid("Guid6", unkGUID);

            if (hasDestLocation)
            {
                packet.Translator.ReadXORByte(guid4, 4); //160-167
                packet.Translator.ReadXORByte(guid4, 0);
                packet.Translator.ReadXORByte(guid4, 5);
                packet.Translator.ReadXORByte(guid4, 7);
                packet.Translator.ReadXORByte(guid4, 1);
                packet.Translator.ReadXORByte(guid4, 2);
                packet.Translator.ReadXORByte(guid4, 3);
                packet.Translator.ReadSingle("Position Y");
                packet.Translator.ReadSingle("Position Z");
                packet.Translator.ReadXORByte(guid4, 6);
                packet.Translator.ReadSingle("Position X");
                packet.Translator.WriteGuid("Guid4", guid4);
            }

            for (int i = 0; i < extraTargetCount; ++i)
            {
                packet.Translator.ReadXORByte(guid10[i], 0); //98+
                packet.Translator.ReadSingle("Float186");
                packet.Translator.ReadXORByte(guid10[i], 1);
                packet.Translator.ReadXORByte(guid10[i], 5);
                packet.Translator.ReadXORByte(guid10[i], 4);
                packet.Translator.ReadXORByte(guid10[i], 7);
                packet.Translator.ReadSingle("Float187");
                packet.Translator.ReadXORByte(guid10[i], 3);
                packet.Translator.ReadSingle("Float188");
                packet.Translator.ReadXORByte(guid10[i], 6);
                packet.Translator.ReadXORByte(guid10[i], 2);

                packet.Translator.WriteGuid("Guid10", guid10[i]);
            }

            if (hasSourceLocation)
            {
                packet.Translator.ReadXORByte(unkGUID, 0); //128-135
                packet.Translator.ReadXORByte(unkGUID, 5);
                packet.Translator.ReadXORByte(unkGUID, 4);
                packet.Translator.ReadXORByte(unkGUID, 7);
                packet.Translator.ReadXORByte(unkGUID, 3);
                packet.Translator.ReadXORByte(unkGUID, 6);
                packet.Translator.ReadSingle("Position X");
                packet.Translator.ReadXORByte(unkGUID, 2);
                packet.Translator.ReadSingle("Position Z");
                packet.Translator.ReadXORByte(unkGUID, 1);
                packet.Translator.ReadSingle("Position Y");
                packet.Translator.WriteGuid("Guid6", unkGUID);
            }
            packet.Translator.ReadXORByte(casterGUID, 4);

            for (int i = 0; i < bits44; ++i)
            {
                packet.Translator.ParseBitStream(guid9[i], 4, 2, 0, 6, 1, 7, 3, 5); //18+
                packet.Translator.WriteGuid("Guid9", guid9[i]);
            }

            if (hasCastSchoolImmunities) // 101
                packet.Translator.ReadUInt32("hasCastSchoolImmunities");

            packet.Translator.ReadXORByte(casterGUID, 2);

            if (hasUnkMovementField) // 102
                packet.Translator.ReadUInt32("hasUnkMovementField");

            if (hasVisualChain) //380
            {
                packet.Translator.ReadInt32("Int174");
                packet.Translator.ReadInt32("Int178");
            }

            for (int i = 0; i < predictedPowerCount; ++i)
            {
                packet.Translator.ReadInt32("Value", i);
                packet.Translator.ReadByteE<PowerType>("Power type", i);
            }

            if (hasRunesStateBefore) //336
                packet.Translator.ReadByte("hasRunesStateBefore");

            CastFlag flags = packet.Translator.ReadInt32E<CastFlag>("Cast Flags"); //10

            packet.Translator.ReadXORByte(casterGUID, 5);
            packet.Translator.ReadXORByte(casterGUID, 7);
            packet.Translator.ReadXORByte(casterGUID, 1);
            packet.Translator.ReadByte("CastCount");
            packet.Translator.ReadXORByte(casterUnitGUID, 7);
            packet.Translator.ReadXORByte(casterUnitGUID, 0);
            packet.Translator.ReadXORByte(casterGUID, 6);
            packet.Translator.ReadXORByte(casterGUID, 0);
            packet.Translator.ReadXORByte(casterUnitGUID, 1);

            if (hasAmmoInventoryType) //368
                packet.Translator.ReadByte("AmmoInventoryType");

            if (hasPredictedHeal) //106
                packet.Translator.ReadUInt32("hasPredictedHeal");

            packet.Translator.ReadXORByte(casterUnitGUID, 6);
            packet.Translator.ReadXORByte(casterUnitGUID, 3);

            if (hasSplineElevation) //384
                packet.Translator.ReadByte("Byte180");

            if (hasDelayTime) //89
                packet.Translator.ReadUInt32("hasDelayTime");

            int spellId = packet.Translator.ReadInt32<SpellId>("Spell ID"); //9

            if (hasAmmoDisplayId) //91
                packet.Translator.ReadUInt32("hasAmmoDisplayId");

            packet.Translator.ReadXORByte(casterUnitGUID, 4);
            packet.Translator.ReadXORByte(casterUnitGUID, 5);

            if (hasRunesStateAfter)//337
                packet.Translator.ReadByte("hasRunesStateAfter");

            packet.Translator.ReadXORByte(casterUnitGUID, 2);

            packet.Translator.ReadWoWString("Text", bitsC0);

            if (hasPredictedType) //428
                packet.Translator.ReadByte("hasPredictedType");

            packet.Translator.ReadXORByte(casterGUID, 3);

            if (hasElevation) //90
                packet.Translator.ReadSingle("Elevation");

            for (int i = 0; i < runeCooldownPassedCount; ++i) //340
                packet.Translator.ReadByte("runeCooldownPassedCount", i);

            if (flags.HasAnyFlag(CastFlag.Unknown21))
            {
                NpcSpellClick spellClick = new NpcSpellClick
                {
                    SpellID = (uint)spellId,
                    CasterGUID = new WowGuid64(BitConverter.ToUInt64(casterGUID, 0)),
                    TargetGUID = new WowGuid64(BitConverter.ToUInt64(targetGUID, 0))
                };

                Storage.SpellClicks.Add(spellClick, packet.TimeSpan);
            }

            packet.Translator.WriteGuid("casterUnitGUID", casterUnitGUID);
            packet.Translator.WriteGuid("CasterGUID", casterGUID);
            packet.Translator.WriteGuid("targetGUID", targetGUID);
            packet.Translator.WriteGuid("ItemTargetGUID", itemTargetGUID);
            packet.Translator.WriteGuid("UnkGUID", unkGUID);
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

            casterGUID[2] = packet.Translator.ReadBit();
            var bit190 = !packet.Translator.ReadBit();
            var hasSourceLocation = packet.Translator.ReadBit();
            casterUnitGUID[2] = packet.Translator.ReadBit();

            if (hasSourceLocation)
                packet.Translator.StartBitStream(sourceLocationTargetGUID, 3, 7, 4, 2, 0, 6, 1, 5);

            casterUnitGUID[6] = packet.Translator.ReadBit();
            var bit1A0 = !packet.Translator.ReadBit();
            casterGUID[7] = packet.Translator.ReadBit();
            var bits1A4 = (int)packet.Translator.ReadBits(20);
            var missTypeCount = packet.Translator.ReadBits(25);
            var bits64 = (int)packet.Translator.ReadBits(24);
            casterGUID[1] = packet.Translator.ReadBit();
            casterUnitGUID[0] = packet.Translator.ReadBit();
            var bits4C = (int)packet.Translator.ReadBits(13);

            var guid6 = new byte[bits64][];
            for (var i = 0; i < bits64; ++i)
            {
                guid6[i] = new byte[8];
                packet.Translator.StartBitStream(guid6[i], 1, 3, 6, 4, 5, 2, 0, 7);
            }

            var guid7 = new byte[bits1A4][];
            for (var i = 0; i < bits1A4; ++i)
            {
                guid7[i] = new byte[8];
                packet.Translator.StartBitStream(guid7[i], 2, 5, 6, 1, 4, 0, 7, 3);
            }

            casterGUID[5] = packet.Translator.ReadBit();

            packet.Translator.ReadBit(); // fake bit
            packet.Translator.ReadBit(); // fake bit

            var hasTargetString = !packet.Translator.ReadBit();
            packet.Translator.StartBitStream(itemTargetGUID, 7, 2, 1, 3, 6, 0, 5, 4);

            casterUnitGUID[7] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(targetGUID, 0, 6, 5, 7, 4, 2, 3, 1);

            var hasRunesStateBefore = !packet.Translator.ReadBit();
            var predictedPowerCount = packet.Translator.ReadBits(21);
            casterUnitGUID[1] = packet.Translator.ReadBit();
            var hasPredictedType = !packet.Translator.ReadBit();
            var hasTargetMask = !packet.Translator.ReadBit();
            casterGUID[3] = packet.Translator.ReadBit();

            var targetString = 0u;
            if (hasTargetString)
                targetString = packet.Translator.ReadBits(7);

            var hasPredictedHeal = !packet.Translator.ReadBit();
            var hasPowerData = packet.Translator.ReadBit();
            var bit1B8 = !packet.Translator.ReadBit();
            casterGUID[6] = packet.Translator.ReadBit();
            packet.Translator.ReadBit(); // fake bit
            var hasVisualChain = packet.Translator.ReadBit();

            packet.Translator.StartBitStream(unkGUID, 7, 6, 1, 2, 0, 5, 3, 4);

            var hasDelayTime = !packet.Translator.ReadBit();
            var bit1B4 = !packet.Translator.ReadBit();
            var runeCooldownPassedCount = packet.Translator.ReadBits(3);

            casterGUID[0] = packet.Translator.ReadBit();

            for (var i = 0; i < missTypeCount; ++i)
            {
                if (packet.Translator.ReadBits("bits22[0]", 4, i) == 11)
                    packet.Translator.ReadBits("bits22[1]", 4, i);
            }

            if (hasTargetMask)
                packet.Translator.ReadBitsE<TargetFlag>("Target Flags", 20);

            var hasElevation = !packet.Translator.ReadBit();
            var hasRunesStateAfter = !packet.Translator.ReadBit();

            var powerCount = 0u;
            if (hasPowerData)
                predictedPowerCount = packet.Translator.ReadBits(21);

            casterUnitGUID[4] = packet.Translator.ReadBit();
            var bit18C = !packet.Translator.ReadBit();
            var hasDestLocation = packet.Translator.ReadBit();
            casterUnitGUID[5] = packet.Translator.ReadBit();
            var bits54 = (int)packet.Translator.ReadBits(24);

            if (hasDestLocation)
                packet.Translator.StartBitStream(destLocationTargetGUID, 0, 3, 2, 1, 4, 5, 6, 7);

            casterGUID[4] = packet.Translator.ReadBit();

            var guid8 = new byte[bits54][];
            for (var i = 0; i < bits54; ++i)
            {
                guid8[i] = new byte[8];
                packet.Translator.StartBitStream(guid8[i], 2, 7, 1, 6, 4, 5, 0, 3);
            }

            casterUnitGUID[3] = packet.Translator.ReadBit();

            packet.Translator.ParseBitStream(targetGUID, 5, 2, 1, 6, 0, 3, 4, 7);
            packet.Translator.ParseBitStream(itemTargetGUID, 5, 2, 0, 6, 7, 3, 1, 4);

            packet.Translator.ReadXORByte(casterUnitGUID, 2);

            for (var i = 0; i < bits1A4; ++i)
            {
                packet.Translator.ReadXORByte(guid7[i], 3);
                packet.Translator.ReadXORByte(guid7[i], 1);
                packet.Translator.ReadXORByte(guid7[i], 0);
                packet.Translator.ReadXORByte(guid7[i], 4);
                packet.Translator.ReadXORByte(guid7[i], 7);
                packet.Translator.ReadSingle("FloatED", i);
                packet.Translator.ReadXORByte(guid7[i], 5);
                packet.Translator.ReadSingle("FloatED", i);
                packet.Translator.ReadXORByte(guid7[i], 6);
                packet.Translator.ReadSingle("FloatED", i);
                packet.Translator.ReadXORByte(guid7[i], 2);

                packet.Translator.WriteGuid("Guid7", guid7[i], i);
            }

            for (var i = 0; i < bits54; ++i)
            {
                packet.Translator.ParseBitStream(guid8[i], 0, 6, 2, 7, 5, 4, 3, 1);
                packet.Translator.WriteGuid("Guid8", guid8[i], i);
            }

            packet.Translator.ParseBitStream(unkGUID, 6, 2, 7, 1, 4, 3, 5, 0);
            if (hasDelayTime)
                packet.Translator.ReadUInt32("hasDelayTime");

            packet.Translator.ReadInt32("Int50");
            for (var i = 0; i < bits64; ++i)
            {
                packet.Translator.ParseBitStream(guid6[i], 4, 2, 0, 6, 7, 5, 1, 3);
                packet.Translator.WriteGuid("Guid6", guid6[i], i);
            }

            if (hasDestLocation)
            {
                var pos = new Vector3();

                pos.Z = packet.Translator.ReadSingle();
                pos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(destLocationTargetGUID, 4);
                packet.Translator.ReadXORByte(destLocationTargetGUID, 5);
                packet.Translator.ReadXORByte(destLocationTargetGUID, 7);
                packet.Translator.ReadXORByte(destLocationTargetGUID, 6);
                packet.Translator.ReadXORByte(destLocationTargetGUID, 1);
                packet.Translator.ReadXORByte(destLocationTargetGUID, 2);
                pos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(destLocationTargetGUID, 0);
                packet.Translator.ReadXORByte(destLocationTargetGUID, 3);
                packet.AddValue("Position", pos);
                packet.Translator.WriteGuid("DestLocationTargetGUID", destLocationTargetGUID);
            }

            packet.Translator.ReadXORByte(casterUnitGUID, 6);
            packet.Translator.ReadXORByte(casterGUID, 7);
            packet.Translator.ReadXORByte(casterUnitGUID, 1);
            if (hasPowerData)
            {
                packet.Translator.ReadInt32("Int10");
                packet.Translator.ReadInt32("Int14");

                for (var i = 0; i < powerCount; ++i)
                {
                    packet.Translator.ReadInt32("IntED", i);
                    packet.Translator.ReadInt32("IntED", i);
                }

                packet.Translator.ReadInt32("Int18");
            }

            if (hasVisualChain)
            {
                packet.Translator.ReadInt32("Int198");
                packet.Translator.ReadInt32("Int194");
            }

            packet.Translator.ReadInt32E<CastFlag>("Cast Flags");

            if (hasSourceLocation)
            {
                var pos = new Vector3();

                packet.Translator.ReadXORByte(sourceLocationTargetGUID, 2);
                pos.Y = packet.Translator.ReadSingle();
                pos.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(sourceLocationTargetGUID, 6);
                packet.Translator.ReadXORByte(sourceLocationTargetGUID, 5);
                packet.Translator.ReadXORByte(sourceLocationTargetGUID, 1);
                packet.Translator.ReadXORByte(sourceLocationTargetGUID, 7);
                pos.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(sourceLocationTargetGUID, 3);
                packet.Translator.ReadXORByte(sourceLocationTargetGUID, 0);
                packet.Translator.ReadXORByte(sourceLocationTargetGUID, 4);
                packet.AddValue("Position", pos);
                packet.Translator.WriteGuid("SourceLocationTargetGUID", sourceLocationTargetGUID);
            }

            packet.Translator.ReadXORByte(casterGUID, 6);

            if (hasPredictedType)
                packet.Translator.ReadByte("PredictedType");

            packet.Translator.ReadXORByte(casterUnitGUID, 4);
            if (bit1B4)
                packet.Translator.ReadInt32("Int1B4");
            if (bit1B8)
                packet.Translator.ReadInt32("Int1B8");
            if (bit18C)
                packet.Translator.ReadInt32("Int18C");
            packet.Translator.ReadXORByte(casterGUID, 1);

            if (bit190)
                packet.Translator.ReadByte("Byte190");

            for (var i = 0; i < predictedPowerCount; ++i)
            {
                packet.Translator.ReadByteE<PowerType>("Power type", i);
                packet.Translator.ReadInt32("Value", i);
            }

            if (hasRunesStateAfter)
                packet.Translator.ReadByte("RuneState");

            for (var i = 0; i < runeCooldownPassedCount; ++i)
                packet.Translator.ReadByte("runeCooldownPassedCount", i);

            if (hasRunesStateBefore)
                packet.Translator.ReadByte("RuneState");

            packet.Translator.ReadXORByte(casterUnitGUID, 0);

            if (bit1A0)
                packet.Translator.ReadByte("Byte1A0");

            if (hasPredictedHeal)
                packet.Translator.ReadUInt32("PredictedHeal");

            packet.Translator.ReadByte("CastCount");
            packet.Translator.ReadXORByte(casterUnitGUID, 5);
            packet.Translator.ReadXORByte(casterGUID, 2);
            packet.Translator.ReadXORByte(casterUnitGUID, 3);
            packet.Translator.ReadXORByte(casterGUID, 5);

            if (hasTargetString)
                packet.Translator.ReadWoWString("targetString", targetString);

            packet.Translator.ReadInt32<SpellId>("Spell ID");

            if (hasElevation)
                packet.Translator.ReadSingle("Elevation");

            packet.Translator.ReadXORByte(casterGUID, 0);
            packet.Translator.ReadXORByte(casterGUID, 3);
            packet.Translator.ReadXORByte(casterGUID, 4);

            packet.Translator.ReadXORByte(casterUnitGUID, 7);

            packet.Translator.WriteGuid("casterUnitGUID", casterUnitGUID);
            packet.Translator.WriteGuid("CasterGUID", casterGUID);
            packet.Translator.WriteGuid("targetGUID", targetGUID);
            packet.Translator.WriteGuid("ItemTargetGUID", itemTargetGUID);
            packet.Translator.WriteGuid("UnkGUID", unkGUID);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            var guid = new byte[8];
            var bits0 = 0;

            guid[7] = packet.Translator.ReadBit();
            var bit28 = packet.Translator.ReadBit(); // +40
            bits0 = (int)packet.Translator.ReadBits(24); //+24
            guid[6] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();

            var hasAura = new bool[bits0];
            var hasCasterGUID = new bool[bits0];
            var casterGUID = new byte[bits0][];
            var effectCount = new uint[bits0];
            var hasMaxDuration = new bool[bits0];
            var hasDuration = new bool[bits0];
            var bits48 = new uint[bits0];

            for (var i = 0; i < bits0; ++i)
            {
                hasAura[i] = packet.Translator.ReadBit(); // +88
                if (hasAura[i])
                {
                    effectCount[i] = packet.Translator.ReadBits(22);
                    hasCasterGUID[i] = packet.Translator.ReadBit();
                    if (hasCasterGUID[i])
                    {
                        casterGUID[i] = new byte[8];
                        packet.Translator.StartBitStream(casterGUID[i], 3, 4, 6, 1, 5, 2, 0, 7);
                    }

                    bits48[i] = packet.Translator.ReadBits(22); //+72
                    hasMaxDuration[i] = packet.Translator.ReadBit(); //+52
                    hasDuration[i] = packet.Translator.ReadBit(); //+44
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
                        packet.Translator.ParseBitStream(casterGUID[i], 3, 2, 1, 6, 4, 0, 5, 7);
                        packet.Translator.WriteGuid("CasterGUID", casterGUID[i], i);
                        aura.CasterGuid = new WowGuid64(BitConverter.ToUInt64(casterGUID[i], 0));
                    }

                    aura.AuraFlags = packet.Translator.ReadByteE<AuraFlagMoP>("Flags", i);
                    aura.Level = packet.Translator.ReadUInt16("Caster Level", i);
                    var id = packet.Translator.ReadInt32<SpellId>("Spell ID", i);
                    aura.SpellId = (uint)id;

                    aura.MaxDuration = hasMaxDuration[i] ? packet.Translator.ReadInt32("Max Duration", i) : 0;

                    aura.Duration = hasDuration[i] ? packet.Translator.ReadInt32("Duration", i) : 0;

                    for (var j = 0; j < bits48[i]; ++j)
                        packet.Translator.ReadSingle("FloatEM", i, j);

                    aura.Charges = packet.Translator.ReadByte("Charges", i);
                    packet.Translator.ReadInt32("Effect Mask", i);

                    for (var j = 0; j < effectCount[i]; ++j)
                        packet.Translator.ReadSingle("Effect Value", i, j);

                    auras.Add(aura);
                    packet.AddSniffData(StoreNameType.Spell, (int)aura.SpellId, "AURA_UPDATE");
                }

                packet.Translator.ReadByte("Slot", i);
            }

            packet.Translator.ParseBitStream(guid, 2, 6, 7, 1, 3, 4, 0, 5);
            var GUID = packet.Translator.WriteGuid("Guid", guid);

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
            packet.Translator.ReadBit("InitialLogin");
            var count = packet.Translator.ReadBits("Spell Count", 22);

            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadUInt32<SpellId>("Spell ID", i);
            }
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

        [Parser(Opcode.SMSG_SEND_UNLEARN_SPELLS)]
        public static void HandleSendUnlearnSpells(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 21);

            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadInt32("Unk Int32", i);
                packet.Translator.ReadByte("Unk Byte", i);
                packet.Translator.ReadInt32<SpellId>("Spell ID", i);
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

        [Parser(Opcode.SMSG_CAST_FAILED)]
        public static void HandleCastFailed(Packet packet)
        {
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadInt32E<SpellCastFailureReason>("Reason");
            packet.Translator.ReadByte("Cast count");

            var bit14 = !packet.Translator.ReadBit();
            var bit18 = !packet.Translator.ReadBit();

            if (bit14)
                packet.Translator.ReadInt32("Int14");

            if (bit18)
                packet.Translator.ReadInt32("Int18");
        }

        [Parser(Opcode.SMSG_SPELL_FAILURE)]
        public static void HandleSpellFailed(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 7, 3, 6, 2, 1, 5, 0, 4);

            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadByte("Cast count");
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadByteE<SpellCastFailureReason>("Reason");
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 5);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPELL_FAILED_OTHER)]
        public static void HandleSpellFailedOther(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 7, 0, 5, 6, 1, 4, 3, 2);

            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadByteE<SpellCastFailureReason>("Reason");
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadByte("Cast count");
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 3);

            packet.Translator.WriteGuid("Guid", guid);
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
                for (var j = 0; j < spentTalents[i]; ++j)
                    packet.Translator.ReadUInt16("Talent Id", i, j);

                for (var j = 0; j < 6; ++j)
                    packet.Translator.ReadUInt16("Glyph", i, j);

                packet.Translator.ReadUInt32("Spec Id", i);
            }
        }

        [Parser(Opcode.CMSG_CANCEL_CAST)]
        public static void HandlePlayerCancelCast(Packet packet)
        {
            var hasCount = !packet.Translator.ReadBit();
            var hasSpellId = !packet.Translator.ReadBit();
            if (hasSpellId)
                packet.Translator.ReadUInt32<SpellId>("Spell Id");
            if (hasCount)
                packet.Translator.ReadByte("Count");
        }

        [Parser(Opcode.CMSG_CANCEL_MOUNT_AURA)]
        public static void HandleCancelMountAura(Packet packet)
        {
            packet.Translator.ReadUInt32("Unk");
        }

        [Parser(Opcode.SMSG_SPELL_CHANNEL_START)]
        public static void HandleSpellChannelStart(Packet packet)
        {
            var targetGUD = new byte[8];
            var guid2 = new byte[8];
            var casterGUID = new byte[8];

            var hasHealAmount = false;
            var hasType = false;

            casterGUID[7] = packet.Translator.ReadBit();
            casterGUID[5] = packet.Translator.ReadBit();
            casterGUID[4] = packet.Translator.ReadBit();
            casterGUID[1] = packet.Translator.ReadBit();

            var healPrediction = packet.Translator.ReadBit();
            if (healPrediction)
            {
                targetGUD[2] = packet.Translator.ReadBit();
                targetGUD[6] = packet.Translator.ReadBit();
                targetGUD[4] = packet.Translator.ReadBit();

                hasType = !packet.Translator.ReadBit();

                targetGUD[3] = packet.Translator.ReadBit();
                targetGUD[7] = packet.Translator.ReadBit();
                targetGUD[5] = packet.Translator.ReadBit();
                targetGUD[1] = packet.Translator.ReadBit();
                targetGUD[2] = packet.Translator.ReadBit();

                hasHealAmount = !packet.Translator.ReadBit();
                packet.Translator.ReadBit(); // fake bit

                packet.Translator.StartBitStream(guid2, 4, 5, 1, 7, 0, 2, 3, 6);

            }

            casterGUID[3] = packet.Translator.ReadBit();
            casterGUID[2] = packet.Translator.ReadBit();
            casterGUID[0] = packet.Translator.ReadBit();
            casterGUID[6] = packet.Translator.ReadBit();

            var hasImmunity = packet.Translator.ReadBit();
            if (healPrediction)
            {
                packet.Translator.ParseBitStream(guid2, 4, 6, 1, 0, 7, 3, 2, 5);

                if (hasType)
                    packet.Translator.ReadByte("Type");

                packet.Translator.ReadXORByte(targetGUD, 4);
                packet.Translator.ReadXORByte(targetGUD, 5);
                packet.Translator.ReadXORByte(targetGUD, 1);
                packet.Translator.ReadXORByte(targetGUD, 3);

                if (hasHealAmount)
                    packet.Translator.ReadInt32("Heal Amount");

                packet.Translator.ReadXORByte(targetGUD, 6);
                packet.Translator.ReadXORByte(targetGUD, 7);
                packet.Translator.ReadXORByte(targetGUD, 2);
                packet.Translator.ReadXORByte(targetGUD, 0);

                packet.Translator.WriteGuid("TargetGUD", targetGUD);
                packet.Translator.WriteGuid("Guid2", guid2);
            }

            if (hasImmunity)
            {
                packet.Translator.ReadInt32("CastSchoolImmunities");
                packet.Translator.ReadInt32("CastImmunities");
            }

            packet.Translator.ReadXORByte(casterGUID, 6);
            packet.Translator.ReadXORByte(casterGUID, 7);
            packet.Translator.ReadXORByte(casterGUID, 3);
            packet.Translator.ReadXORByte(casterGUID, 1);
            packet.Translator.ReadXORByte(casterGUID, 0);

            packet.Translator.ReadInt32("Duration");

            packet.Translator.ReadXORByte(casterGUID, 5);
            packet.Translator.ReadXORByte(casterGUID, 4);
            packet.Translator.ReadXORByte(casterGUID, 2);

            packet.Translator.ReadUInt32<SpellId>("Spell ID");

            packet.Translator.WriteGuid("CasterGUID", casterGUID);
        }

        [Parser(Opcode.SMSG_SPELL_CHANNEL_UPDATE)]
        public static void HandleSpellChannelUpdate(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 0, 3, 4, 1, 5, 2, 6, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadInt32("Timestamp");
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 3);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_CANCEL_AURA)]
        public static void HandleCanelAura(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadBit(); // fake bit?
            packet.Translator.StartBitStream(guid, 6, 5, 1, 0, 4, 3, 2, 7);
            packet.Translator.ParseBitStream(guid, 3, 2, 1, 0, 4, 7, 5, 6);

            packet.Translator.WriteGuid("GUID", guid);
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

            guid[0] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            var bits10 = packet.Translator.ReadBits(19);
            guid[4] = packet.Translator.ReadBit();

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
                guid3[i] = new byte[bits14[i]][];
                for (var j = 0; j < bits14[i]; ++j)
                {
                    guid3[i][j] = new byte[8];
                    packet.Translator.StartBitStream(guid3[i][j], 5, 4, 2, 3, 1, 0, 6, 7);
                }

                bits4[i] = packet.Translator.ReadBits(20);
                guid6[i] = new byte[bits4[i]][];
                for (var j = 0; j < bits4[i]; ++j)
                {
                    guid6[i][j] = new byte[8];
                    packet.Translator.StartBitStream(guid6[i][j], 0, 3, 1, 5, 6, 4, 7, 2);
                }

                bits24[i] = packet.Translator.ReadBits(21);
                bits54[i] = packet.Translator.ReadBits(22);
                bits44[i] = packet.Translator.ReadBits(22);

                guid4[i] = new byte[bits24[i]][];
                for (var j = 0; j < bits24[i]; ++j)
                {
                    guid4[i][j] = new byte[8];
                    packet.Translator.StartBitStream(guid4[i][j], 7, 2, 0, 5, 6, 3, 4, 1);
                }

                bits34[i] = packet.Translator.ReadBits(24);
                guid5[i] = new byte[bits34[i]][];
                for (var j = 0; j < bits34[i]; ++j)
                {
                    guid5[i][j] = new byte[8];
                    packet.Translator.StartBitStream(guid5[i][j], 6, 5, 1, 0, 3, 4, 7, 2);
                }
            }

            guid[1] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();

            var bits38 = 0u;
            var bit48 = packet.Translator.ReadBit();
            if (bit48)
                bits38 = packet.Translator.ReadBits(21);

            if (bit48)
            {
                for (var i = 0; i < bits38; ++i)
                {
                    packet.Translator.ReadInt32("Int38+4", i);
                    packet.Translator.ReadInt32("Int38+8", i);
                }

                packet.Translator.ReadInt32("Int30");
                packet.Translator.ReadInt32("Int2C");
                packet.Translator.ReadInt32("Int34");
            }

            for (var i = 0; i < bits10; ++i)
            {
                for (var j = 0; j < bits34[i]; ++j)
                {
                    packet.Translator.ParseBitStream(guid5[i][j], 7, 5, 1, 2, 6, 4, 0, 3);
                    packet.Translator.WriteGuid("Summoned GUID", guid5[i][j], i, j);
                }

                for (var j = 0; j < bits24[i]; ++j)
                {
                    packet.Translator.ReadXORByte(guid4[i][j], 4);
                    packet.Translator.ReadXORByte(guid4[i][j], 3);
                    packet.Translator.ReadInt32("Int24+0", i, j);
                    packet.Translator.ReadXORByte(guid4[i][j], 6);
                    packet.Translator.ReadXORByte(guid4[i][j], 5);
                    packet.Translator.ReadXORByte(guid4[i][j], 0);
                    packet.Translator.ReadXORByte(guid4[i][j], 7);
                    packet.Translator.ReadInt32("Int24+4", i, j);
                    packet.Translator.ReadXORByte(guid4[i][j], 2);
                    packet.Translator.ReadXORByte(guid4[i][j], 1);

                    packet.Translator.WriteGuid("Guid4", guid4[i][j], i, j);
                }

                for (var j = 0; j < bits4[i]; ++j)
                {
                    packet.Translator.ReadXORByte(guid6[i][j], 3);
                    packet.Translator.ReadXORByte(guid6[i][j], 7);
                    packet.Translator.ReadXORByte(guid6[i][j], 5);
                    packet.Translator.ReadXORByte(guid6[i][j], 2);
                    packet.Translator.ReadXORByte(guid6[i][j], 0);
                    packet.Translator.ReadInt32("Int4+0", i, j);
                    packet.Translator.ReadInt32("Int4+8", i, j);
                    packet.Translator.ReadXORByte(guid6[i][j], 4);
                    packet.Translator.ReadXORByte(guid6[i][j], 1);
                    packet.Translator.ReadSingle("FloatEB", i, j);
                    packet.Translator.ReadXORByte(guid6[i][j], 6);

                    packet.Translator.WriteGuid("Guid6", guid6[i][j], i, j);
                }

                for (var j = 0; j < bits14[i]; ++j)
                {
                    packet.Translator.ReadXORByte(guid3[i][j], 0);
                    packet.Translator.ReadXORByte(guid3[i][j], 6);
                    packet.Translator.ReadXORByte(guid3[i][j], 4);
                    packet.Translator.ReadXORByte(guid3[i][j], 7);
                    packet.Translator.ReadXORByte(guid3[i][j], 2);
                    packet.Translator.ReadXORByte(guid3[i][j], 5);
                    packet.Translator.ReadXORByte(guid3[i][j], 3);
                    packet.Translator.ReadInt32("Int14");
                    packet.Translator.ReadXORByte(guid3[i][j], 1);

                    packet.Translator.WriteGuid("Guid3", guid3[i][j], i, j);
                }

                for (var j = 0; j < bits54[i]; ++j)
                    packet.Translator.ReadInt32("Int54", i, j);

                var type = packet.Translator.ReadInt32E<SpellEffect>("Spell Effect", i);

                for (var j = 0; j < bits44[i]; ++j)
                    packet.Translator.ReadInt32<ItemId>("Created Item", i, j);
            }

            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ParseBitStream(guid, 5, 7, 1, 6, 2, 0, 4, 3);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_CATEGORY_COOLDOWN)]
        public static void HandleSpellCategoryCooldown(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 21);

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("Cooldown", i);
                packet.Translator.ReadInt32("Category Cooldown", i);
            }
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL_KIT)]
        public static void HandleCastVisualKit(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 4, 2, 6, 5, 1, 3, 0, 7);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadUInt32("Unk 1");
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadUInt32("Unk 2");
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadUInt32("SpellVisualKit ID");

            packet.Translator.WriteGuid("Guid2", guid);
        }
    }
}

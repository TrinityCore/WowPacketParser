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

namespace WowPacketParser.V5_4_8_18291.Parsers
{
    public static class SpellHandler
    {

        [HasSniffData]
        [Parser(Opcode.SMSG_SPELL_START)]
        public static void HandleSpellStart(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];
            var guid3 = new byte[8];
            var guid4 = new byte[8];
            var guid5 = new byte[8];
            var guid6 = new byte[8];
            var guid7 = new byte[8];
            var guid8 = new byte[8];


            var bits44 = packet.ReadBits("bits44", 24);
            guid[5] = packet.ReadBit();

            var guid9 = new byte[bits44][]; //18+
            for (var i = 0; i < bits44; ++i)
            {
                guid9[i] = new byte[8];
                packet.StartBitStream(guid9[i], 1, 0, 7, 2, 4, 3, 6, 5);
            }

            var hasSplineElevation = !packet.ReadBit("hasSplineElevation");//384
            packet.ReadBit(); //fake bit
            guid2[4] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var runeCooldownPassedCount = (int)packet.ReadBits(3);//340
            guid2[2] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            var missTypeCount = (int)packet.ReadBits(25); //84
            var bits11 = (int)packet.ReadBits(13);
            guid[4] = packet.ReadBit();
            var hitcountPos = (int)packet.ReadBits(24); //52
            guid2[7] = packet.ReadBit();

            var guid11 = new byte[hitcountPos][]; //14+
            for (var i = 0; i < hitcountPos; ++i)
            {
                guid11[i] = new byte[8];
                packet.StartBitStream(guid11[i], 5, 0, 3, 4, 7, 2, 6, 1);
            }

            var hasSourceLocation = packet.ReadBit("hasSourceLocation"); //152
            var predictedPowerCount = (int)packet.ReadBits(21); //320
            packet.StartBitStream(guid3, 3, 0, 1, 7, 2, 6, 4, 5); //120-127
            var hasElevation = !packet.ReadBit("hasElevation"); //90
            var hasTargetString = !packet.ReadBit("hasTargetString");
            var hasAmmoInventoryType = !packet.ReadBit("hasAmmoInventoryType");//368
            var hasDestLocation = packet.ReadBit("hasDestLocation");//184
            var hasDelayTime = !packet.ReadBit("hasDelayTime"); //89
            guid[3] = packet.ReadBit();

            if (hasDestLocation)
                packet.StartBitStream(guid4, 1, 6, 2, 7, 0, 3, 5, 4); //160-167

            var hasAmmoDisplayId = !packet.ReadBit("hasAmmoDisplayId"); //91

            if (hasSourceLocation)
                packet.StartBitStream(guid5, 4, 3, 5, 1, 7, 0, 6, 2); //128-135

            packet.ReadBit(); //fake bit 104
            guid[6] = packet.ReadBit();
            packet.StartBitStream(guid6, 2, 1, 7, 6, 0, 5, 3, 4); //416-423
            var hasTargetFlags = !packet.ReadBit("hasTargetFlags");

            if (hasTargetFlags)
                packet.ReadEnum<TargetFlag>("Target Flags", 20);

            guid[1] = packet.ReadBit();
            var hasPredictedHeal = !packet.ReadBit("hasPredictedHeal"); //106
            var hasRunesStateBefore = !packet.ReadBit("hasRunesStateBefore"); //336
            var hasCastSchoolImmunities = !packet.ReadBit("hasCastSchoolImmunities"); //101
            guid2[5] = packet.ReadBit();
            packet.ReadBit(); //28 fake bit
            var extraTargetCount = (int)packet.ReadBits("extraTargetCount", 20);//388

            var guid10 = new byte[extraTargetCount][];
            for (var i = 0; i < extraTargetCount; ++i) //388
            {
                guid10[i] = new byte[8];
                packet.StartBitStream(guid10[i], 1, 6, 2, 3, 5, 7, 0, 4); //98+
            }
            packet.StartBitStream(guid7, 1, 4, 6, 7, 5, 3, 0, 2); //112-119
            guid[0] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
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
            guid2[1] = packet.ReadBit();
            var hasVisualChain = packet.ReadBit("hasVisualChain");//380
            guid[7] = packet.ReadBit();
            var hasPredictedType = !packet.ReadBit("hasPredictedType");//428
            guid2[0] = packet.ReadBit();

            packet.ParseBitStream(guid3, 1, 7, 6, 0, 4, 2, 3, 5); //120-127
            packet.WriteGuid("Guid3", guid3);

            for (var i = 0; i < hitcountPos; ++i)
            {
                packet.ParseBitStream(guid11[i], 4, 5, 3, 0, 6, 2, 1, 7);//14+
                packet.WriteGuid("Guid11", guid11[i]);
            }
            packet.ParseBitStream(guid7, 4, 5, 1, 7, 6, 3, 2, 0); //112-119
            packet.WriteGuid("Guid7", guid7);

            packet.ReadInt32("CastTime"); //12

            packet.ParseBitStream(guid6, 4, 5, 3, 2, 1, 6, 7, 0); //416-423
            packet.WriteGuid("Guid6", guid6);

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
                packet.ReadXORByte(guid6, 0); //128-135
                packet.ReadXORByte(guid6, 5);
                packet.ReadXORByte(guid6, 4);
                packet.ReadXORByte(guid6, 7);
                packet.ReadXORByte(guid6, 3);
                packet.ReadXORByte(guid6, 6);
                packet.ReadSingle("Position X");
                packet.ReadXORByte(guid6, 2);
                packet.ReadSingle("Position Z");
                packet.ReadXORByte(guid6, 1);
                packet.ReadSingle("Position Y");
                packet.WriteGuid("Guid6", guid6);
            }
            packet.ReadXORByte(guid, 4);

            for (var i = 0; i < bits44; ++i)
            {
                packet.ParseBitStream(guid9[i], 4, 2, 0, 6, 1, 7, 3, 5); //18+
                packet.WriteGuid("Guid9", guid9[i]);
            }

            if (hasCastSchoolImmunities) // 101
                packet.ReadUInt32("hasCastSchoolImmunities");

            packet.ReadXORByte(guid, 2);

            if (hasUnkMovementField) // 102
                packet.ReadUInt32("hasUnkMovementField");

            if (hasVisualChain) //380
            {
                packet.ReadInt32("Int174");
                packet.ReadInt32("Int178");
            }

            for (var i = 0; i < predictedPowerCount; ++i)
            {
                packet.ReadInt32("IntED", i);
                packet.ReadByte("ByteED", i);
            }

            if (hasRunesStateBefore) //336
                packet.ReadByte("hasRunesStateBefore");

            packet.ReadInt32("CastFlags"); //10

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 1);
            packet.ReadByte("CastCount");
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid2, 1);

            if (hasAmmoInventoryType) //368
                packet.ReadByte("AmmoInventoryType");

            if (hasPredictedHeal) //106
                packet.ReadUInt32("hasPredictedHeal");

            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 3);

            if (hasSplineElevation) //384
                packet.ReadByte("Byte180");

            if (hasDelayTime) //89
                packet.ReadUInt32("hasDelayTime");

            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID"); //9

            if (hasAmmoDisplayId) //91
                packet.ReadUInt32("hasAmmoDisplayId");

            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 5);

            if (hasRunesStateAfter)//337
                packet.ReadByte("hasRunesStateAfter");

            packet.ReadXORByte(guid2, 2);

            packet.ReadWoWString("Text", bitsC0);

            if (hasPredictedType) //428
                packet.ReadByte("hasPredictedType");

            packet.ReadXORByte(guid, 3);

            if (hasElevation) //90
                packet.ReadSingle("Elevation");

            for (var i = 0; i < runeCooldownPassedCount; ++i) //340
                packet.ReadByte("runeCooldownPassedCount", i);

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
                        aura.CasterGuid = new Guid(BitConverter.ToUInt64(casterGUID[i], 0));
                    }

                    aura.AuraFlags = packet.ReadEnum<AuraFlagMoP>("Flags", TypeCode.Byte, i);
                    aura.Level = packet.ReadUInt16("Caster Level", i);
                    var id = packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", i);
                    aura.SpellId = (uint)id;

                    if (hasMaxDuration[i])
                        aura.MaxDuration = packet.ReadInt32("Max Duration", i);
                    else
                        aura.MaxDuration = 0;

                    if (hasDuration[i])
                        aura.Duration = packet.ReadInt32("Duration", i);
                    else
                        aura.Duration = 0;

                    for (var j = 0; j < bits48[i]; ++j)
                    {
                        packet.ReadSingle("FloatEM", i, j);
                    }

                    aura.Charges = packet.ReadByte("Charges", i);
                    packet.ReadInt32("Effect Mask", i);

                    for (var j = 0; j < effectCount[i]; ++j)
                        packet.ReadSingle("Effect Value", i, j);
                }

                packet.ReadByte("Slot", i);
            }

            packet.ParseBitStream(guid, 2, 6, 7, 1, 3, 4, 0, 5);
            packet.WriteGuid("Guid", guid);
        }

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
            var bit272 = false;
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
                bit272 = !packet.ReadBit();                 // v2 + bit272
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
                packet.ReadEnum<CastFlag>("Cast Flags", 20);

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
                packet.ReadSingle("float276");      // v2 + 276
                packet.ReadXORByte(guid20, 6);      // v2 + 256

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
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");

            if (hasGlyphIndex)
                packet.ReadInt32("hasGlyphIndex");
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

        [Parser(Opcode.SMSG_CAST_FAILED)]
        public static void HandleCastFailed(Packet packet)
        {
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
            packet.ReadEnum<SpellCastFailureReason>("Reason", TypeCode.Int32);
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
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
            packet.ReadEnum<SpellCastFailureReason>("Reason", TypeCode.Byte);
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
            packet.ReadEnum<SpellCastFailureReason>("Reason", TypeCode.Byte);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
            packet.ReadByte("Cast count");
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 3);

            packet.WriteGuid("Guid", guid);
        }
    }
}

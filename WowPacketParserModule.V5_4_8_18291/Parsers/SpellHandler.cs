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
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            var bits0 = 0;
            var guid2 = new byte[8];
            var bit18 = false;

            guid2[7] = packet.ReadBit();
            bit18 = packet.ReadBit();
            bits0 = (int)packet.ReadBits(24);
            guid2[6] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[5] = packet.ReadBit();

            var casterGUID = new byte[bits0][];
            var hasCasterGUID = new bool[bits0];
            var hasDuration = new bool[bits0];
            var hasMaxDuration = new bool[bits0];
            var hasAura = new bool[bits0];
            var effectCount = new uint[bits0];
            var bits72 = new uint[bits0];

            for (var i = 0; i < bits0; ++i)
            {
                hasAura[i] = packet.ReadBit();
                if (hasAura[i])
                {
                    effectCount[i] = packet.ReadBits(22);        // +56
                    hasCasterGUID[i] = packet.ReadBit();    // +32

                    if (hasCasterGUID[i])
                    {
                        casterGUID[i] = new byte[8];
                        packet.StartBitStream(casterGUID[i], 3, 4, 6, 1, 5, 2, 0, 7);
                    }

                    bits72[i] = packet.ReadBits(22);    // +72
                    hasDuration[i] = packet.ReadBit();        // +52
                    hasMaxDuration[i] = packet.ReadBit();        // +44
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
                        packet.WriteGuid("Caster GUID", casterGUID[i], i);
                        aura.CasterGuid = new Guid(BitConverter.ToUInt64(casterGUID[i], 0));
                    }

                    aura.Charges = packet.ReadByte("Charges", i);                                   // +28
                    packet.ReadUInt16("Caster Level", i);                                           // +20
                    var id = packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", i);   // +8

                    if (hasMaxDuration[i])
                        aura.MaxDuration = packet.ReadInt32("Max Duration", i);                     // +40
                    else
                        aura.MaxDuration = 0;

                    if (hasDuration[i])
                        aura.Duration = packet.ReadInt32("Duration", i);                            //+48
                    else
                        aura.Duration = 0;

                    for (var j = 0; j < bits72[i]; ++j)
                        packet.ReadSingle("Float72", i, j); // +72

                    aura.AuraFlags = packet.ReadEnum<AuraFlagMoP>("Flags", TypeCode.Byte, i);       // +22
                    packet.ReadInt32("Effect Mask", i);                                             // +16

                    for (var j = 0; j < effectCount[i]; ++j)
                        packet.ReadSingle("Effect Value", i, j);                                    // +56

                }
                packet.ReadByte("Slot", i);
            }

            packet.ParseBitStream(guid2, 2, 6, 7, 1, 3, 4, 0, 5);
            packet.WriteGuid("GUID2", guid2);
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
    }
}

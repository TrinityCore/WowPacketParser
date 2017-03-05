using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class PetHandler
    {
        [Parser(Opcode.CMSG_QUERY_PET_NAME)]
        public static void HandlePetNameQuery(Packet packet)
        {
            var guid = new byte[8];
            var number = new byte[8];

            number[0] = packet.Translator.ReadBit();
            number[5] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            number[7] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            number[3] = packet.Translator.ReadBit();
            number[6] = packet.Translator.ReadBit();
            number[2] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            number[1] = packet.Translator.ReadBit();
            number[4] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(number, 2);
            packet.Translator.ReadXORByte(number, 1);
            packet.Translator.ReadXORByte(number, 0);
            packet.Translator.ReadXORByte(number, 7);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(number, 6);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(number, 5);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(number, 3);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(number, 4);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 7);

            var GUID = new WowGuid64(BitConverter.ToUInt64(number, 0));
            var Number = BitConverter.ToUInt64(number, 0);
            packet.Translator.WriteGuid("Guid", guid);
            packet.AddValue("Pet Number", Number);

            // Store temporary name (will be replaced in SMSG_QUERY_PET_NAME_RESPONSE)
            StoreGetters.AddName(GUID, Number.ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.SMSG_QUERY_PET_NAME_RESPONSE)]
        public static void HandlePetNameQueryResponse(Packet packet)
        {
            var hasData = packet.Translator.ReadBit();
            if (!hasData)
            {
                packet.Translator.ReadUInt64("Pet number");
                return;
            }

            packet.Translator.ReadBit("Declined");

            const int maxDeclinedNameCases = 5;
            var declinedNameLen = new int[maxDeclinedNameCases];
            for (var i = 0; i < maxDeclinedNameCases; ++i)
                declinedNameLen[i] = (int)packet.Translator.ReadBits(7);

            var len = packet.Translator.ReadBits(8);

            for (var i = 0; i < maxDeclinedNameCases; ++i)
                if (declinedNameLen[i] != 0)
                    packet.Translator.ReadWoWString("Declined name", declinedNameLen[i], i);

            var petName = packet.Translator.ReadWoWString("Pet name", len);
            packet.Translator.ReadTime("Time");
            var number = packet.Translator.ReadUInt64("Pet number");

            var guidArray = (from pair in StoreGetters.NameDict where Equals(pair.Value, number) select pair.Key).ToList();
            foreach (var guid in guidArray)
                StoreGetters.NameDict[guid] = petName;
        }

        [Parser(Opcode.SMSG_PET_SPELLS_MESSAGE)]
        public static void HandlePetSpells(Packet packet)
        {
            var guid = new byte[8];

            guid[7] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            var bits44 = (int)packet.Translator.ReadBits(21);
            var bits28 = (int)packet.Translator.ReadBits(22);
            guid[2] = packet.Translator.ReadBit();
            var bits10 = (int)packet.Translator.ReadBits(20);
            guid[5] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();

            const int maxCreatureSpells = 10;
            var spells = new List<uint>(maxCreatureSpells);
            for (var i = 0; i < maxCreatureSpells; i++) // Read pet/vehicle spell ids
            {
                var spell16 = packet.Translator.ReadUInt16();
                var spell8 = packet.Translator.ReadByte();
                var spellId = spell16 + (spell8 << 16);
                var slot = packet.Translator.ReadByte();

                if (spellId <= 4)
                    packet.AddValue("Action", spellId, i);
                else
                    packet.AddValue("Spell", StoreGetters.GetName(StoreNameType.Spell, spellId), i);
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadInt16("Int14", i);
                packet.Translator.ReadInt32("Int14", i);
            }

            packet.Translator.ReadXORByte(guid, 2);
            for (var i = 0; i < bits28; ++i)
                packet.Translator.ReadInt32("IntED", i);

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadInt16("Family");

            for (var i = 0; i < bits44; ++i)
            {
                packet.Translator.ReadInt32("Int48", i);
                packet.Translator.ReadByte("Byte48", i);
                packet.Translator.ReadInt32("Int48", i);
            }

            packet.Translator.ReadInt16("Int40");
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadInt32("Int24");
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadInt32("Int20");

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_PET_ACTION)]
        public static void HandlePetAction(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.Translator.ReadUInt32("Data");
            packet.Translator.ReadSingle("Y");
            packet.Translator.ReadSingle("Z");
            packet.Translator.ReadSingle("X");

            guid2[1] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid2, 0);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.CMSG_PET_CAST_SPELL)]
        public static void HandlePetCastSpell(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var guid3 = new byte[8];
            var guid4 = new byte[8];
            var petGuid = new byte[8];

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

            var hasDestLocation = packet.Translator.ReadBit();         // v2 + 112
            petGuid[7] = packet.Translator.ReadBit();
            var hasMissileSpeed = !packet.Translator.ReadBit();
            var hasSrcLocation = packet.Translator.ReadBit();          // v2 + 80
            petGuid[1] = packet.Translator.ReadBit();
            var archeologyCounter = packet.Translator.ReadBits(2);     // v2 + 424
            var hasTargetMask = !packet.Translator.ReadBit();          // v2 + 32
            petGuid[4] = packet.Translator.ReadBit();
            packet.Translator.ReadBit();
            petGuid[6] = packet.Translator.ReadBit();
            var hasTargetString = !packet.Translator.ReadBit();        // v2 + 120
            packet.Translator.ReadBit();
            var hasMovement = packet.Translator.ReadBit();             // v2 + 416
            var hasCastFlags = !packet.Translator.ReadBit();           // v2 + 28
            var hasSpellId = !packet.Translator.ReadBit();             // v2 + 20
            petGuid[0] = packet.Translator.ReadBit();
            petGuid[5] = packet.Translator.ReadBit();
            petGuid[2] = packet.Translator.ReadBit();
            for (var i = 0; i < archeologyCounter; ++i)
                packet.Translator.ReadBits("archeologyType", 2, i);
            petGuid[3] = packet.Translator.ReadBit();
            var hasGlyphIndex = !packet.Translator.ReadBit();          // v2 + 24
            var hasCastCount = !packet.Translator.ReadBit();           // v2 + 16
            var hasElevation = !packet.Translator.ReadBit();

            var unkMovementLoopCounter = 0u;
            if (hasMovement)
            {
                hasOrientation = !packet.Translator.ReadBit();
                hasSplineElevation = !packet.Translator.ReadBit();
                bit388 = packet.Translator.ReadBit(); // v2 + 388
                guid20[5] = packet.Translator.ReadBit();               // v2 + 261
                guid20[7] = packet.Translator.ReadBit();               // v2 + 263
                hasMovementFlags2 = !packet.Translator.ReadBit();
                hasTimestamp = !packet.Translator.ReadBit();           // v2 + bit272
                hasFallData = packet.Translator.ReadBit();
                hasMovementFlags = !packet.Translator.ReadBit();       // v2 + 264
                hasUnkMovementField = !packet.Translator.ReadBit();    // v2 + 408

                if (hasMovementFlags)
                    packet.Translator.ReadBits("hasMovementFlags", 30);
                bit389 = packet.Translator.ReadBit();                  // v2 + 389
                guid20[6] = packet.Translator.ReadBit();               // v2 + 263
                hasTransport = packet.Translator.ReadBit(); // v2 + 344
                guid20[0] = packet.Translator.ReadBit();               // v2 + 263
                unkMovementLoopCounter = packet.Translator.ReadBits(22);

                if (hasTransport)
                {
                    hasTransportTime2 = packet.Translator.ReadBit();   // v2 + 332
                    hasTransportTime3 = packet.Translator.ReadBit();   // v2 + 340
                    transportGUID[5] = packet.Translator.ReadBit();    // v2 + 300
                    transportGUID[6] = packet.Translator.ReadBit();    // v2 + 302
                    transportGUID[4] = packet.Translator.ReadBit();    // v2 + 301
                    transportGUID[0] = packet.Translator.ReadBit();    // v2 + 296
                    transportGUID[1] = packet.Translator.ReadBit();    // v2 + 297
                    transportGUID[2] = packet.Translator.ReadBit();    // v2 + 2980
                    transportGUID[7] = packet.Translator.ReadBit();    // v2 + 303
                    transportGUID[3] = packet.Translator.ReadBit();    // v2 + 299
                }

                guid20[1] = packet.Translator.ReadBit();               // v2 + 257

                if (hasMovementFlags2)
                    packet.Translator.ReadBits("hasMovementFlags2", 13);

                guid20[3] = packet.Translator.ReadBit();               // v2 + 259
                guid20[2] = packet.Translator.ReadBit();               // v2 + 258
                bit412 = packet.Translator.ReadBit();                  // v2 + 412
                hasPitch = !packet.Translator.ReadBit();
                guid20[4] = packet.Translator.ReadBit(); // v2 + 260

                if (hasFallData)
                    hasFallDirection = packet.Translator.ReadBit();
            }

            if (hasDestLocation)
                packet.Translator.StartBitStream(guid2, 2, 0, 1, 4, 5, 6, 3, 7);

            if (hasCastFlags)
                packet.Translator.ReadBits("hasCastFlags", 5);

            packet.Translator.StartBitStream(guid1, 2, 4, 7, 0, 6, 1, 5, 3);

            if (hasTargetMask)
                packet.Translator.ReadBitsE<TargetFlag>("Target Flags", 20);

            if (hasTargetString)
                targetString = packet.Translator.ReadBits("hasTargetString", 7);

            if (hasSrcLocation)
                packet.Translator.StartBitStream(guid4, 2, 0, 3, 1, 6, 7, 4, 5);

            packet.Translator.StartBitStream(guid3, 6, 0, 3, 4, 2, 1, 5, 7);

            //resetbitreader

            packet.Translator.ReadXORBytes(petGuid, 2, 6, 3);

            for (var i = 0; i < archeologyCounter; ++i)
            {
                packet.Translator.ReadUInt32("unk1", i); // +4
                packet.Translator.ReadUInt32("unk2", i); // +8
            }
            packet.Translator.ReadXORBytes(petGuid, 1, 7, 0, 4, 5);

            if (hasDestLocation)
            {
                packet.Translator.ReadXORByte(guid2, 4);
                packet.Translator.ReadXORByte(guid2, 1);
                packet.Translator.ReadXORByte(guid2, 7);
                packet.Translator.ReadSingle("Position Z");
                packet.Translator.ReadSingle("Position Y");
                packet.Translator.ReadXORByte(guid2, 6);
                packet.Translator.ReadXORByte(guid2, 3);
                packet.Translator.ReadSingle("Position X");
                packet.Translator.ReadXORByte(guid2, 2);
                packet.Translator.ReadXORByte(guid2, 5);
                packet.Translator.ReadXORByte(guid2, 0);

                packet.Translator.WriteGuid("Guid2", guid2);
            }

            if (hasMovement)
            {
                if (hasPitch)
                    packet.Translator.ReadSingle("Pitch");     // v2 + 352

                if (hasTransport)
                {
                    if (hasTransportTime3)
                        packet.Translator.ReadInt32("hasTransportTime3");

                    if (hasTransportTime2)
                        packet.Translator.ReadInt32("hasTransportTime2");

                    packet.Translator.ReadByte("Transport Seat");      // v2 + 320
                    packet.Translator.ReadSingle("O");          // v2 + 304
                    packet.Translator.ReadSingle("Z");          // v2 + 312
                    packet.Translator.ReadXORByte(transportGUID, 2);   // v2 + 298
                    packet.Translator.ReadInt32("Transport Time");     // v2 + 324
                    packet.Translator.ReadXORByte(transportGUID, 3);   // v2 + 299
                    packet.Translator.ReadSingle("X");          // v2 + 308
                    packet.Translator.ReadXORByte(transportGUID, 6);   // v2 + 302
                    packet.Translator.ReadXORByte(transportGUID, 5);   // v2 + 301
                    packet.Translator.ReadXORByte(transportGUID, 7);   // v2 + 303
                    packet.Translator.ReadXORByte(transportGUID, 0);   // v2 + 296
                    packet.Translator.ReadSingle("Y");          // v2 + 316
                    packet.Translator.ReadXORByte(transportGUID, 4);   // v2 + 300
                    packet.Translator.ReadXORByte(transportGUID, 1);   // v2 + 297

                    packet.Translator.WriteGuid("Transport GUID", transportGUID);
                }

                if (hasUnkMovementField)
                    packet.Translator.ReadInt32("Int408");     // v2 + 408

                for (var i = 0; i < unkMovementLoopCounter; ++i)
                    packet.Translator.ReadInt32("MovementLoopCounter", i);

                packet.Translator.ReadXORByte(guid20, 3);      // v2 + 260

                if (hasOrientation)
                    packet.Translator.ReadSingle("Orientation");   // v2 + 288

                packet.Translator.ReadXORByte(guid20, 5);      // v2 + 256

                if (hasFallData)
                {
                    packet.Translator.ReadSingle("Z Speed");

                    if (hasFallDirection)
                    {
                        packet.Translator.ReadSingle("CosAngle");
                        packet.Translator.ReadSingle("XY Speed");
                        packet.Translator.ReadSingle("SinAngle");
                    }
                    packet.Translator.ReadInt32("FallTime");
                }

                if (hasTimestamp)
                    packet.Translator.ReadInt32("hasTimestamp");   // v2 + 288

                packet.Translator.ReadXORByte(guid20, 6);      // v2 + 262
                packet.Translator.ReadSingle("Position X");      // v2 + 276
                packet.Translator.ReadXORByte(guid20, 1);      // v2 + 257
                packet.Translator.ReadSingle("Position Z");    // v2 + 284
                packet.Translator.ReadXORByte(guid20, 2);      // v2 + 258
                packet.Translator.ReadXORByte(guid20, 7);      // v2 + 260
                packet.Translator.ReadXORByte(guid20, 0);      // v2 + 256
                packet.Translator.ReadSingle("Position Y");    // v2 + 280
                packet.Translator.ReadXORByte(guid20, 4);      // v2 + 260

                if (hasSplineElevation)
                    packet.Translator.ReadSingle("SplineElevation");   // v2 + 384

                packet.Translator.WriteGuid("Guid20", guid20);
            }

            if (hasSrcLocation)
            {
                packet.Translator.ReadXORByte(guid4, 3);
                packet.Translator.ReadXORByte(guid4, 4);
                packet.Translator.ReadXORByte(guid4, 2);
                packet.Translator.ReadXORByte(guid4, 1);
                packet.Translator.ReadXORByte(guid4, 0);
                packet.Translator.ReadXORByte(guid4, 7);
                packet.Translator.ReadSingle("Position Z");
                packet.Translator.ReadXORByte(guid4, 6);
                packet.Translator.ReadXORByte(guid4, 5);
                packet.Translator.ReadSingle("Position X");
                packet.Translator.ReadSingle("Position Y");

                packet.Translator.WriteGuid("Guid4", guid4);
            }

            if (hasMissileSpeed)
                packet.Translator.ReadSingle("missileSpeed");

            packet.Translator.ParseBitStream(guid1, 1, 2, 5, 7, 4, 6, 3, 0);
            packet.Translator.WriteGuid("Guid1", guid1);

            packet.Translator.ParseBitStream(guid3, 1, 5, 7, 3, 0, 2, 4, 6);
            packet.Translator.WriteGuid("Guid3", guid3);

            if (hasElevation)
                packet.Translator.ReadSingle("hasElevation");

            if (hasCastCount)
                packet.Translator.ReadByte("Cast Count");

            if (hasTargetString)
                packet.Translator.ReadWoWString("TargetString", targetString);

            if (hasGlyphIndex)
                packet.Translator.ReadInt32("hasGlyphIndex");

            if (hasSpellId)
                packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.WriteGuid("PetGuid", petGuid);
        }
    }
}
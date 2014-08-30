using System;
using System.Text;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using Guid = WowPacketParser.Misc.WowGuid;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class SpellHandler
    {
        [Parser(Opcode.CMSG_CANCEL_AURA)]
        public static void HandleCancelAura(Packet packet)
        {
            packet.ReadEntry<UInt32>(StoreNameType.Spell, "Spell ID");

            packet.ReadBit("Unk");
            var guid = packet.StartBitStream(6, 5, 1, 0, 4, 3, 2, 7);
            packet.ParseBitStream(guid, 3, 2, 1, 0, 4, 7, 5, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_CANCEL_CAST)]
        public static void HandleCancelCast(Packet packet)
        {
            var hasCounter = !packet.ReadBit("!HasCounter"); // 20
            var hasSpellID = !packet.ReadBit("!HasSpellID"); // 16

            if (hasSpellID) // 16
                packet.ReadEntry<UInt32>(StoreNameType.Spell, "Spell ID"); // 16

            if (hasCounter) // 20
                packet.ReadByte("Counter"); // 20
        }

        [Parser(Opcode.CMSG_CANCEL_MOUNT_AURA)]
        public static void HandleCancelMountAura(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_CAST_SPELL)]
        public static void HandleCastSpell(Packet packet)
        {
            var targetGuid = new byte[8];
            var itemTargetGuid = new byte[8];
            var destTransportGuid = new byte[8];
            var srcTransportGuid = new byte[8];
            var movementTransportGuid = new byte[8];
            var movementGuid = new byte[8];

            UInt32 targetStringLength = 0;

            bool hasTransport = false;
            bool hasTransportTime2 = false;
            bool hasTransportTime3 = false;
            bool hasFallData = false;
            bool hasFallDirection = false;
            bool hasTimestamp = false;
            bool hasSplineElevation = false;
            bool hasPitch = false;
            bool hasOrientation = false;
            bool hasUnkMovementField = false;
            UInt32 unkMovementLoopCounter = 0;

            packet.ReadBit("unk40"); // 40
            var hasTargetString = !packet.ReadBit("!Has Target String"); // 120
            packet.ReadBit("unkv6");
            bool hasCastCount = !packet.ReadBit("!Has Cast Count"); // 16
            bool hasSrcLocation = packet.ReadBit("Has Source Location"); // 80
            bool hasDestLocation = packet.ReadBit("Has Destination Location"); // 112
            bool hasSpellId = !packet.ReadBit("!Has Spell ID"); // 20
            var researchDataCount = packet.ReadBits("Research Data Count", 2); // 424
            bool hasTargetMask = !packet.ReadBit("!Has Target Match"); // 32
            bool hasMissileSpeed = !packet.ReadBit("!Has Missile Speed"); // 252

            for (var i = 0; i < researchDataCount; ++i) // 424
                packet.ReadBits("Research data", 2, i); // 428

            bool hasGlyphIndex = !packet.ReadBit("!Has Glyph Index"); // 24
            bool hasMovement = packet.ReadBit("Has Movement"); // 416
            bool hasElevation = !packet.ReadBit("!Has Elevation"); // 248
            bool hasCastFlags = !packet.ReadBit("!Has Cast Flags"); // 28

            targetGuid[5] = packet.ReadBit(); // 45
            targetGuid[4] = packet.ReadBit(); // 44
            targetGuid[2] = packet.ReadBit(); // 42
            targetGuid[7] = packet.ReadBit(); // 47
            targetGuid[1] = packet.ReadBit(); // 41
            targetGuid[6] = packet.ReadBit(); // 46
            targetGuid[3] = packet.ReadBit(); // 43
            targetGuid[0] = packet.ReadBit(); // 40

            if (hasDestLocation) // 112
            {
                destTransportGuid[1] = packet.ReadBit(); // 89
                destTransportGuid[3] = packet.ReadBit(); // 91
                destTransportGuid[5] = packet.ReadBit(); // 93
                destTransportGuid[0] = packet.ReadBit(); // 88
                destTransportGuid[2] = packet.ReadBit(); // 90
                destTransportGuid[6] = packet.ReadBit(); // 94
                destTransportGuid[7] = packet.ReadBit(); // 95
                destTransportGuid[4] = packet.ReadBit(); // 92
            }

            if (hasMovement) // 416
            {
                unkMovementLoopCounter = packet.ReadBits(22); // 392
                packet.ReadBit("unk388"); // 388
                movementGuid[4] = packet.ReadBit(); // 260
                hasTransport = packet.ReadBit("Has Transport"); // 344

                if (hasTransport) // 344
                {
                    hasTransportTime2 = packet.ReadBit("Has Transport Time2"); // 332
                    movementTransportGuid[7] = packet.ReadBit(); // 303
                    movementTransportGuid[4] = packet.ReadBit(); // 300
                    movementTransportGuid[1] = packet.ReadBit(); // 297
                    movementTransportGuid[0] = packet.ReadBit(); // 296
                    movementTransportGuid[6] = packet.ReadBit(); // 302
                    movementTransportGuid[3] = packet.ReadBit(); // 299
                    movementTransportGuid[5] = packet.ReadBit(); // 301
                    hasTransportTime3 = packet.ReadBit("Has Transport time3"); // 340
                    movementTransportGuid[2] = packet.ReadBit(); // 298
                }

                packet.ReadBit("unk389"); // 389
                movementGuid[7] = packet.ReadBit(); // 263
                hasOrientation = !packet.ReadBit("!Has Orientation"); // 288
                movementGuid[6] = packet.ReadBit(); // 262
                hasSplineElevation = !packet.ReadBit("!Has Spline Elevation"); // 384
                hasPitch = !packet.ReadBit("!Has Pitch"); // 352
                movementGuid[0] = packet.ReadBit(); // 256
                packet.ReadBit("unk412"); // 412
                bool hasMovementFlags = !packet.ReadBit("!hasMovementFlags"); // 264
                hasTimestamp = !packet.ReadBit("!hasTimestamp"); // 272
                hasUnkMovementField = !packet.ReadBit("!hasUnkMovementField"); // 408

                if (hasMovementFlags) // 264
                    packet.ReadBits("Flags", 30);

                movementGuid[1] = packet.ReadBit(); // 257
                movementGuid[3] = packet.ReadBit(); // 259
                movementGuid[2] = packet.ReadBit(); // 258
                movementGuid[5] = packet.ReadBit(); // 261
                hasFallData = packet.ReadBit("Has Fall Data"); // 380

                if (hasFallData) // 380
                    hasFallDirection = packet.ReadBit("Has Fall Direction"); // 376

                bool hasMovementFlags2 = !packet.ReadBit("!Has Movement Flags 2"); // 268

                if (hasMovementFlags2) // 268
                    packet.ReadBits("Movement Flags 2", 13);
            }

            itemTargetGuid[1] = packet.ReadBit(); // 49
            itemTargetGuid[0] = packet.ReadBit(); // 48
            itemTargetGuid[7] = packet.ReadBit(); // 55
            itemTargetGuid[4] = packet.ReadBit(); // 52
            itemTargetGuid[6] = packet.ReadBit(); // 54
            itemTargetGuid[5] = packet.ReadBit(); // 53
            itemTargetGuid[3] = packet.ReadBit(); // 51
            itemTargetGuid[2] = packet.ReadBit(); // 50

            if (hasSrcLocation) // 80
            {
                srcTransportGuid[4] = packet.ReadBit(); // 60
                srcTransportGuid[5] = packet.ReadBit(); // 61
                srcTransportGuid[3] = packet.ReadBit(); // 59
                srcTransportGuid[0] = packet.ReadBit(); // 56
                srcTransportGuid[7] = packet.ReadBit(); // 63
                srcTransportGuid[1] = packet.ReadBit(); // 57
                srcTransportGuid[6] = packet.ReadBit(); // 62
                srcTransportGuid[2] = packet.ReadBit(); // 58
            }

            if (hasTargetMask) // 32
                packet.ReadBits("Target Mask", 20);

            if (hasCastFlags) // 28
                packet.ReadBits("Cast Flags", 5);

            if (hasTargetString) // 120
                targetStringLength = packet.ReadBits("Target String Length", 7);

            packet.ResetBitReader();

            for (var i = 0; i < researchDataCount; ++i) // 424
            {
                packet.ReadUInt32("Research Data2", i); // 1720
                packet.ReadUInt32("Research Data", i); // 1716
            }

            if (hasMovement) // 416
            {
                packet.ReadSingle("Position X"); // 276
                packet.ReadXORByte(movementGuid, 0); // 256

                if (hasTransport) // 344
                {
                    packet.ReadXORByte(movementTransportGuid, 2); // 298
                    packet.ReadByte("Transport Seat"); // 320
                    packet.ReadXORByte(movementTransportGuid, 3); // 299
                    packet.ReadXORByte(movementTransportGuid, 7); // 303
                    packet.ReadSingle("Transport Position X"); // 304
                    packet.ReadXORByte(movementTransportGuid, 5); // 301

                    if (hasTransportTime3) // 340
                        packet.ReadUInt32("Transport Time 3"); // 336

                    packet.ReadSingle("Transport Position Z"); // 312
                    packet.ReadSingle("Transport Position Y"); // 308

                    packet.ReadXORByte(movementTransportGuid, 6); // 302
                    packet.ReadXORByte(movementTransportGuid, 1); // 297
                    packet.ReadSingle("Transport Position O"); // 316

                    packet.ReadXORByte(movementTransportGuid, 4); // 300

                    if (hasTransportTime2) // 332
                        packet.ReadUInt32("Transport Time 2"); // 328

                    packet.ReadXORByte(movementTransportGuid, 0); // 296

                    packet.ReadUInt32("Transport Time"); // 324

                    packet.WriteGuid("Transport GUID", movementTransportGuid);
                }

                packet.ReadXORByte(movementGuid, 5); // 261

                if (hasFallData) // 380
                {
                    packet.ReadUInt32("Fall Time"); // 356
                    packet.ReadSingle("Jump Speed Z"); // 360

                    if (hasFallDirection) // 376
                    {
                        packet.ReadSingle("Sin Angle"); // 364
                        packet.ReadSingle("XY Speed"); // 372
                        packet.ReadSingle("Cos Angle"); // 368
                    }
                }

                if (hasSplineElevation) // 384
                    packet.ReadSingle("Spline Elevation");

                packet.ReadXORByte(movementGuid, 6); // 262

                if (hasUnkMovementField) // 408
                    packet.ReadUInt32("unkMovementField"); // 408

                packet.ReadXORByte(movementGuid, 4); // 260

                if (hasOrientation)  // 288
                    packet.ReadSingle("Orientation");

                if (hasTimestamp) // 272
                    packet.ReadUInt32("Time Stamp"); // 272

                packet.ReadXORByte(movementGuid, 1); // 257

                if (hasPitch) // 352
                    packet.ReadSingle("Pitch");

                packet.ReadXORByte(movementGuid, 3); // 259

                for (var i = 0; i != unkMovementLoopCounter; i++) // 392
                    packet.ReadUInt32("unk396", i); // 396

                packet.ReadSingle("Position Y"); // 280
                packet.ReadXORByte(movementGuid, 7); // 263
                packet.ReadSingle("Position Z"); // 284
                packet.ReadXORByte(movementGuid, 2); // 258
            }

            packet.ReadXORByte(itemTargetGuid, 4); // 52
            packet.ReadXORByte(itemTargetGuid, 2); // 50
            packet.ReadXORByte(itemTargetGuid, 1); // 49
            packet.ReadXORByte(itemTargetGuid, 5); // 53
            packet.ReadXORByte(itemTargetGuid, 7); // 55
            packet.ReadXORByte(itemTargetGuid, 3); // 51
            packet.ReadXORByte(itemTargetGuid, 6); // 54
            packet.ReadXORByte(itemTargetGuid, 0); // 48

            packet.WriteGuid("Item Target GUID", itemTargetGuid);

            if (hasDestLocation) // 112
            {
                packet.ReadXORByte(destTransportGuid, 2); // 90
                packet.ReadSingle("Position X"); // 96
                packet.ReadXORByte(destTransportGuid, 4); // 92
                packet.ReadXORByte(destTransportGuid, 1); // 89
                packet.ReadXORByte(destTransportGuid, 0); // 88
                packet.ReadXORByte(destTransportGuid, 3); // 91
                packet.ReadSingle("Position Y"); // 100
                packet.ReadXORByte(destTransportGuid, 7); // 95
                packet.ReadSingle("Position Z"); // 104
                packet.ReadXORByte(destTransportGuid, 5); // 93
                packet.ReadXORByte(destTransportGuid, 6); // 94

                packet.WriteGuid("Destination Transport GUID", destTransportGuid);
            }

            packet.ReadXORByte(targetGuid, 3); // 43
            packet.ReadXORByte(targetGuid, 4); // 44
            packet.ReadXORByte(targetGuid, 7); // 47
            packet.ReadXORByte(targetGuid, 6); // 46
            packet.ReadXORByte(targetGuid, 2); // 42
            packet.ReadXORByte(targetGuid, 0); // 40
            packet.ReadXORByte(targetGuid, 1); // 41
            packet.ReadXORByte(targetGuid, 5); // 45

            packet.WriteGuid("Target GUID", targetGuid);

            if (hasSrcLocation) // 80
            {
                packet.ReadSingle("Position Y"); // 68
                packet.ReadXORByte(srcTransportGuid, 5); // 61
                packet.ReadXORByte(srcTransportGuid, 1); // 57
                packet.ReadXORByte(srcTransportGuid, 7); // 63
                packet.ReadXORByte(srcTransportGuid, 6); // 62
                packet.ReadSingle("Position X"); // 64
                packet.ReadXORByte(srcTransportGuid, 3); // 59
                packet.ReadXORByte(srcTransportGuid, 2); // 58
                packet.ReadXORByte(srcTransportGuid, 0); // 56
                packet.ReadXORByte(srcTransportGuid, 4); // 60
                packet.ReadSingle("Position Z"); // 72

                packet.WriteGuid("Source Transport GUID", srcTransportGuid);
            }

            if (hasTargetString) // 120
                packet.ReadWoWString("Target String", targetStringLength);

            if (hasMissileSpeed) // 248
                packet.ReadSingle("Missile Speed");

            if (hasElevation) // 252
                packet.ReadSingle("Elevation");

            if (hasCastCount) // 16
                packet.ReadByte("Cast Count"); // 16

            if (hasSpellId) // 20
                packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID");

            if (hasGlyphIndex) // 24
                packet.ReadUInt32("Glyph Index"); // 24
        }

        [Parser(Opcode.CMSG_TOTEM_DESTROYED)]
        public static void HandleTotemDestroyed(Packet packet)
        {
            packet.ReadByte("Slot"); // 24
            var guid = packet.StartBitStream(4, 2, 1, 3, 0, 6, 7, 5);
            packet.ParseBitStream(guid, 6, 2, 4, 1, 5, 0, 3, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_UNLEARN_SKILL)]
        public static void HandleUnlearnSkill(Packet packet)
        {
            packet.ReadInt32("SkillID");
        }

        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            var guid = new byte[8];
            guid[7] = packet.ReadBit();
            var unk40 = packet.ReadBit("unk40");
            var count = packet.ReadBits("count", 24);
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[5] = packet.ReadBit();

            var guid2 = new byte[count][];
            var unk164 = new byte[count];
            var unk156 = new byte[count];
            var unk336 = new uint[count];
            var unk144 = new byte[count];
            var unk400 = new uint[count];
            var unk200 = new byte[count];
            for (var i = 0 ; i < count; i++)
            {
                unk200[i] = packet.ReadBitVisible("unk200", i);
                if (unk200[i] > 0)
                {
                    unk336[i] = packet.ReadBits("unk336", 22, i);
                    unk144[i] = packet.ReadBitVisible("unk144", i);
                    if (unk144[i] > 0)
                        guid2[i] = packet.StartBitStream(3, 4, 6, 1, 5, 2, 0, 7);

                    unk400[i] = packet.ReadBits("unk400", 22, i);
                    unk164[i] = packet.ReadBitVisible("unk164", i);
                    unk156[i] = packet.ReadBitVisible("unk156", i);
                }
            }
            for (var i = 0; i < count; i++)
            {
                if (unk200[i] > 0)
                {
                    if (unk144[i] > 0)
                    {
                        packet.ParseBitStream(guid2[i], 3, 2, 1, 6, 4, 0, 5, 7);
                        packet.WriteGuid("Guid2", guid2[i], i);
                    }
                    packet.ReadByteVisible("unk124", i);
                    packet.ReadInt16("unk152", i);
                    packet.ReadInt32Visible("unk144", i);
                    if (unk156[i] > 0)
                        packet.ReadInt32("unk272", i);
                    if (unk164[i] > 0)
                        packet.ReadInt32("unk304", i);
                    for (var j = 0; j < unk400[i]; j++)
                        packet.ReadSingle("unk416", i, j);
                    packet.ReadByteVisible("unk134", i);
                    packet.ReadInt32Visible("unk176", i);
                    for (var j = 0; j < unk336[i]; j++)
                        packet.ReadSingle("unk352", i, j);
                }
                packet.ReadByte("unk112", i);
            }
            packet.ParseBitStream(guid, 2, 6, 7, 1, 3, 4, 0, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_CAST_FAILED)]
        public static void HandleCastFailed(Packet packet)
        {
            packet.ReadEntry<UInt32>(StoreNameType.Spell, "Spell ID");
            packet.ReadInt32("unk28");
            packet.ReadByte("unk32");
            var unk16 = packet.ReadBit("-unk16");
            var unk24 = packet.ReadBit("-unk24");
            if (!unk16)
                packet.ReadInt32("unk16");
            if (!unk24)
                packet.ReadInt32("unk24");
        }

        [Parser(Opcode.SMSG_CHANNEL_START)]
        public static void HandleSpellChannelStart(Packet packet)
        {
            var casterGUID = new byte[8];
            var targetGUID = new byte[8];
            var guid3 = new byte[8];

            var hasType = false;
            var hasHealAmount = false;
            var unk224 = false;

            casterGUID[7] = packet.ReadBit();
            casterGUID[5] = packet.ReadBit();
            casterGUID[4] = packet.ReadBit();
            casterGUID[1] = packet.ReadBit();
            var healPrediction = packet.ReadBit("healPrediction"); // 72
            if (healPrediction)
            {
                targetGUID[6] = packet.ReadBit();
                targetGUID[4] = packet.ReadBit();
                targetGUID[0] = packet.ReadBit();
                hasType = !packet.ReadBit("!hasType"); // 68
                targetGUID[3] = packet.ReadBit();
                targetGUID[7] = packet.ReadBit();
                targetGUID[5] = packet.ReadBit();
                targetGUID[1] = packet.ReadBit();
                targetGUID[2] = packet.ReadBit();
                hasHealAmount = !packet.ReadBit("!hasHealAmount"); // 256
                unk224 = !packet.ReadBit("!unk224"); // 224
                guid3 = packet.StartBitStream(4, 5, 1, 7, 0, 2, 3, 6);
            }
            casterGUID[3] = packet.ReadBit();
            casterGUID[2] = packet.ReadBit();
            casterGUID[0] = packet.ReadBit();
            casterGUID[6] = packet.ReadBit();
            var hasImmunity = packet.ReadBit("hasImmunity"); // 32
            if (healPrediction)
            {
                packet.ParseBitStream(guid3, 4, 6, 1, 0, 7, 3, 2, 5);
                if (hasType)
                    packet.ReadByte("Type"); // 68
                packet.ParseBitStream(targetGUID, 4, 5, 1, 3);
                if (hasHealAmount)
                    packet.ReadInt32("Heal Amount"); // 256
                packet.ParseBitStream(targetGUID, 6, 7, 2, 0);
                packet.WriteGuid("Target GUID", targetGUID);
                packet.WriteGuid("Guid3", guid3);
            }
            if (hasImmunity)
            {
                packet.ReadInt32("CastSchoolImmunities"); // 112
                packet.ReadInt32("CastImmunities"); // 96
            }
            packet.ParseBitStream(casterGUID, 6, 7, 3, 1, 0);
            packet.ReadInt32("Duration"); // 160
            packet.ParseBitStream(casterGUID, 5, 4, 2);
            packet.ReadEntry<UInt32>(StoreNameType.Spell, "Spell ID"); // 144
            packet.WriteGuid("Caster Guid", casterGUID);
        }

        [Parser(Opcode.SMSG_CHANNEL_UPDATE)]
        public static void HandleSpellChannelUpdate(Packet packet)
        {
            var guid = packet.StartBitStream(0, 3, 4, 1, 5, 2, 6, 7);
            packet.ParseBitStream(guid, 4, 7, 1, 2, 6, 5);
            packet.ReadInt32("TimeStamp");
            packet.ParseBitStream(guid, 0, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_COOLDOWN_EVENT)]
        public static void HandleCooldownEvent(Packet packet)
        {
            var guid = packet.StartBitStream(4, 7, 1, 5, 6, 0, 2, 3);
            packet.ParseBitStream(guid, 5, 7);
            packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID"); // 24
            packet.ParseBitStream(guid, 3, 1, 2, 4, 6, 0);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_INITIAL_SPELLS)]
        public static void HandleInitialSpells(Packet packet)
        {
            packet.ReadBit("Unk16");
            var count = packet.ReadBits("Count", 22);

            for (int i = 0; i < count; i++)
            {
                packet.ReadUInt32("Spell", i);
            }
        }

        [Parser(Opcode.SMSG_LEARNED_SPELL)]
        public static void HandleLearnedSpell(Packet packet)
        {
            var count = packet.ReadBits("Count", 22);
            packet.ReadBit("Byte16");
            for (var i = 0; i < count; i++)
                packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID");
        }

        [Parser(Opcode.SMSG_REMOVED_SPELL)]
        public static void HandleRemovedSpell(Packet packet)
        {
            var count = packet.ReadBits("Count", 22);
            for (var i = 0; i < count; i++)
                packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID");
        }

        [Parser(Opcode.SMSG_SEND_UNLEARN_SPELLS)]
        public static void HandleSendUnlearnSpells(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);

            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32("Unk Int32", i);
                packet.ReadByte("Unk Byte", i);
                packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID", i);
            }
        }

        [Parser(Opcode.SMSG_SPELL_CATEGORY_COOLDOWN)]
        public static void HandleSpellCategoryCooldown(Packet packet)
        {
            var count = packet.ReadBits("Count", 23);

            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32("Category Cooldown"); // 24
                packet.ReadInt32("Cooldown"); // 20
            }
        }

        [Parser(Opcode.SMSG_SPELL_COOLDOWN)]
        public static void HandleSpellCooldown(Packet packet)
        {
            var guid = new byte[8];

            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasMask = !packet.ReadBit("!hasMask"); // 40
            guid[7] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var count = packet.ReadBits("count", 21);
            guid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            for (var i = 0; i < count; i++)
            {
                packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID"); // 80
                packet.ReadInt32("Cooldown", i); // 84
            }
            packet.ParseBitStream(guid, 5, 3, 7);
            if (hasMask)
                packet.ReadByte("Mask"); // 40
            packet.ParseBitStream(guid, 4, 1, 0, 2, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPELL_DELAYED)]
        public static void HandleSpellDelayed(Packet packet)
        {
            var guid = new byte[8];
            guid = packet.StartBitStream(6, 7, 2, 0, 4, 3, 1, 5);
            packet.ParseBitStream(guid, 2, 6, 1, 7, 0, 5, 3);
            packet.ReadInt32("Delay");
            packet.ParseBitStream(guid, 4);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPELL_FAILED_OTHER)]
        public static void HandleSpellFailedOther(Packet packet)
        {
            var guid = new byte[8];
            guid = packet.StartBitStream(7, 0, 5, 6, 1, 4, 3, 2);
            packet.ParseBitStream(guid, 0, 1);
            packet.ReadEnum<SpellCastFailureReason>("Reason", TypeCode.Byte); // 29
            packet.ParseBitStream(guid, 7, 5, 6);
            packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID"); // 24
            packet.ReadByte("Cast count"); // 28
            packet.ParseBitStream(guid, 4, 2, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPELL_FAILURE)]
        public static void HandleSpellFailure(Packet packet)
        {
            var guid = new byte[8];
            guid = packet.StartBitStream(7, 3, 6, 2, 1, 5, 0, 4);
            packet.ParseBitStream(guid, 2, 6, 7, 0, 3, 1);
            packet.ReadEnum<SpellCastFailureReason>("Reason", TypeCode.Byte); // 29
            packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID"); // 24
            packet.ReadByte("Cast count"); // 28
            packet.ParseBitStream(guid, 4, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellGo(Packet packet)
        {
            var guid48 = new byte[8];
            var guid56 = new byte[8];

            guid56[2] = packet.ReadBit(); // 58
            var unk400 = !packet.ReadBit("!unk400");
            var unk184 = packet.ReadBit("unk184");
            guid48[2] = packet.ReadBit(); // 50

            var guid160 = new byte[8];
            if (unk184)
                guid160 = packet.StartBitStream(3, 7, 4, 2, 0, 6, 1, 5);

            guid48[6] = packet.ReadBit(); // 54
            var unk416 = !packet.ReadBit("!unk416");
            guid56[7] = packet.ReadBit(); // 63

            var unk420 = packet.ReadBits("unk420", 20);
            var unk116 = packet.ReadBits("unk116", 25);
            var unk100 = packet.ReadBits("unk100", 24);
            guid56[1] = packet.ReadBit(); // 57
            guid48[0] = packet.ReadBit(); // 48
            var unk76 = packet.ReadBits("unk76", 13);

            var guid104 = new byte[unk100][];
            for (var i = 0; i < unk100; i++)
                guid104[i] = packet.StartBitStream(1, 3, 6, 4, 5, 2, 0, 7);

            var guid424 = new byte[unk420][];
            for (var i = 0; i < unk420; i++)
                guid424[i] = packet.StartBitStream(2, 5, 6, 1, 4, 0, 7, 3);

            guid56[5] = packet.ReadBit(); // 61

            var unk144 = !packet.ReadBit("!unk144");

            var unk152 = !packet.ReadBit("!unk152");

            var unk224 = packet.ReadBit("unk224");
            var guid152 = packet.StartBitStream(7, 2, 1, 3, 6, 0, 5, 4);
            guid48[7] = packet.ReadBit(); // 55
            var guid144 = packet.StartBitStream(0, 6, 5, 7, 4, 2, 3, 1);

            var unk368 = !packet.ReadBit("!unk368");

            var unk352 = packet.ReadBits("unk352", 21);

            guid48[1] = packet.ReadBit(); // 49

            var unk460 = !packet.ReadBit("!unk460");
            var unk136 = !packet.ReadBit("unk136");
            guid56[3] = packet.ReadBit(); // 59

            var len224 = 0u;
            if (!unk224)
                len224 = packet.ReadBits("len224", 7);

            var unk456 = !packet.ReadBit("!unk456");
            var unk44 = packet.ReadBit("unk44"); // 44
            var unk440 = !packet.ReadBit("!unk440");
            guid56[6] = packet.ReadBit(); // 62
            var unk448 = !packet.ReadBit("!unk448");
            var unk412 = packet.ReadBit("unk412");
            var guid448 = packet.StartBitStream(7, 6, 1, 2, 0, 5, 3, 4);

            var unk388 = !packet.ReadBit("!unk388");
            var unk436 = !packet.ReadBit("!unk436");

            var unk372 = packet.ReadBits("unk372", 3);

            guid56[0] = packet.ReadBit(); // 56

            for (var i = 0; i < unk116; i++)
            {
                var unk120 = packet.ReadBits("unk120", 4, i);
                if (unk120 == 11)
                    packet.ReadBits("unk124", 4, i);
            }

            if (unk136)
                packet.ReadBits("unk136", 20);

            var unk188h = !packet.ReadBit("!unk188h");

            var unk369 = !packet.ReadBit("!unk369");

            var unk28 = 0u;
            if (unk44)
                unk28 = packet.ReadBits("unk28", 21);

            guid48[4] = packet.ReadBit(); // 52
            var unk396 = !packet.ReadBit("!unk396");
            var unk216 = packet.ReadBit("unk216");
            guid48[5] = packet.ReadBit(); // 53

            var unk84 = packet.ReadBits("unk84*4", 24);
            var guid192 = new byte[8];
            if (unk216)
                guid192 = packet.StartBitStream(0, 3, 2, 1, 4, 5, 6, 7);

            guid56[4] = packet.ReadBit(); // 60

            var guid88 = new byte[unk84][];
            for (var i = 0; i < unk84; i++)
                guid88[i] = packet.StartBitStream(2, 7, 1, 6, 4, 5, 0, 3);

            guid48[3] = packet.ReadBit(); // 51

            packet.ParseBitStream(guid144, 5, 2, 1, 6, 0, 3, 4, 7);
            packet.WriteGuid("guid144", guid144);

            packet.ParseBitStream(guid152, 5, 2, 0, 6, 7, 3, 1, 4);
            packet.WriteGuid("guid152", guid152);

            packet.ParseBitStream(guid48, 2); // 50

            for (var i = 0; i < unk420; i++)
            {
                packet.ParseBitStream(guid424[i], 3, 1, 0, 4, 7);
                packet.ReadSingle("unk0ch", i);
                packet.ParseBitStream(guid424[i], 5);
                packet.ReadSingle("unk08h", i);
                packet.ParseBitStream(guid424[i], 6);
                packet.ReadSingle("unk10h", i);
                packet.ParseBitStream(guid424[i], 2);
                packet.WriteGuid("guid424", guid424[i]);
            }

            for (var i = 0; i < unk84; i++)
            {
                packet.ParseBitStream(guid88[i], 0, 6, 2, 7, 5, 4, 3, 1);
                packet.WriteGuid("guid88", guid88[i]);
            }

            packet.ParseBitStream(guid448, 6, 2, 7, 1, 4, 3, 5, 0);

            if (unk388)
                packet.ReadInt32("unk388");

            packet.ReadInt32("unk80");

            for (var i = 0; i < unk100; i++)
            {
                packet.ParseBitStream(guid104[i], 4, 2, 0, 6, 7, 5, 1, 3);
                packet.WriteGuid("guid104", guid104[i]);
            }

            if (unk216)
            {
                packet.ReadSingle("unkd0h");
                packet.ReadSingle("unkcch");
                packet.ParseBitStream(guid192, 4, 5, 7, 6, 1, 2);
                packet.ReadSingle("unkc8h");
                packet.ParseBitStream(guid192, 0, 3);
                packet.WriteGuid("guid192", guid192);
            }

            packet.ParseBitStream(guid48, 6); // 54

            packet.ParseBitStream(guid56, 7); // 63

            packet.ParseBitStream(guid48, 1); // 49

            if (unk44)
            {
                packet.ReadInt32("unk16");
                packet.ReadInt32("unk20");
                for (var i = 0; i < unk28; i++)
                {
                    packet.ReadInt32("unk36", i);
                    packet.ReadInt32("unk32", i);
                }
                packet.ReadInt32("unk24");
            }

            if (unk412)
            {
                packet.ReadInt32("unk408");
                packet.ReadInt32("unk404");
            }

            packet.ReadInt32("unk72");

            if (unk184)
            {
                packet.ParseBitStream(guid160, 2);
                packet.ReadSingle("unkach");
                packet.ReadSingle("unka8h");
                packet.ParseBitStream(guid160, 6, 5, 1, 7);
                packet.ReadSingle("unkb0h");
                packet.ParseBitStream(guid160, 3, 0, 4);
                packet.WriteGuid("guid160", guid160);
            }

            packet.ParseBitStream(guid56, 6); // 62

            if (unk460)
                packet.ReadByte("unk460");

            packet.ParseBitStream(guid48, 4); // 52

            if (unk436)
                packet.ReadInt32("unk436");

            if (unk440)
                packet.ReadInt32("unk440");

            if (unk396)
                packet.ReadInt32("unk396");

            packet.ParseBitStream(guid56, 1); // 57

            if (unk400)
                packet.ReadByte("unk400");

            for (var i = 0; i < unk352; i++)
            {
                packet.ReadByte("unk360", i);
                packet.ReadInt32("unk356", i);
            }

            if (unk369)
                packet.ReadByte("unk369");

            for (var i = 0; i < unk372; i++)
                packet.ReadByte("unk376", i);

            if (unk368)
                packet.ReadByte("unk368");

            packet.ParseBitStream(guid48, 0); // 48

            if (unk416)
                packet.ReadByte("unk416");

            if (unk456)
                packet.ReadInt32("unk456");

            packet.ReadByte("unk64");

            packet.ParseBitStream(guid48, 5); // 53

            packet.ParseBitStream(guid56, 2); // 58

            packet.ParseBitStream(guid48, 3); // 51

            packet.ParseBitStream(guid56, 5); // 61

            if (len224 > 0)
                packet.ReadWoWString("str224", len224);

            var spellId = packet.ReadEntry<UInt32>(StoreNameType.Spell, "Spell ID"); // 68

            if (unk188h)
                packet.ReadSingle("unk188h");

            packet.ParseBitStream(guid56, 0, 3, 4); // 56 59 60

            packet.ParseBitStream(guid48, 7); // 55

            packet.WriteGuid("Guid48", guid48);
            packet.WriteGuid("Guid56", guid56);
            packet.AddSniffData(StoreNameType.Spell, (int)spellId, "SPELL_GO");
        }

        [Parser(Opcode.SMSG_SPELL_START)]
        public static void HandleSpellStart(Packet packet)
        {
            var count68 = packet.ReadBits("count68", 24); // 68
            var guid = new byte[8];
            var guid2 = new byte[8];

            guid[5] = packet.ReadBit();

            var guid68 = new byte[count68][];
            for (var i = 0; i < count68; i++)
                guid68[i] = packet.StartBitStream(1, 0, 7, 2, 4, 3, 6, 5);

            var unk384 = !packet.ReadBit("!unk384"); // 384
            var unk30 = !packet.ReadBit("!unk30"); // 30

            guid2[4] = packet.ReadBit(); // 28
            guid[2] = packet.ReadBit(); // 18

            var unk340 = packet.ReadBits("unk340", 3);

            guid2[2] = packet.ReadBit();
            guid2[6] = packet.ReadBit();

            var unk84 = packet.ReadBits("unk84", 25);
            var unk11 = packet.ReadBits("unk11", 13);

            guid[4] = packet.ReadBit();

            var unk52 = packet.ReadBits("unk52", 24);

            guid2[7] = packet.ReadBit();

            var guid52 = new byte[unk52][];
            for (var i = 0; i < unk52; i++)
                guid52[i] = packet.StartBitStream(5, 0, 3, 4, 7, 2, 6, 1);

            var unk152 = packet.ReadBit("unk152");
            var unk320 = packet.ReadBits("unk320", 21);

            var guid120 = packet.StartBitStream(3, 0, 1, 7, 2, 6, 4, 5);

            var unk168h = !packet.ReadBit("!unk168h");

            var unk48 = packet.ReadBit("unk48"); // 0x80000000 48

            var unk368 = !packet.ReadBit("!unk368");
            var unk184 = packet.ReadBit("unk184");
            var unk89 = !packet.ReadBit("!unk89");

            guid[3] = packet.ReadBit();

            var guid160 = new byte[8];
            if (unk184)
                packet.StartBitStream(1, 6, 2, 7, 0, 3, 5, 4);

            var unk91 = !packet.ReadBit("!unk91");

            var guid128 = new byte[8];
            if (unk152)
                guid128 = packet.StartBitStream(4, 3, 5, 1, 7, 0, 6, 2);

            var unk104 = !packet.ReadBit("!unk104");

            guid[6] = packet.ReadBit();

            var guid416 = packet.StartBitStream(2, 1, 7, 6, 0, 5, 3, 4);

            var unk26 = !packet.ReadBit("!unk26");
            if (unk26)
                packet.ReadBits("unk26", 20);

            guid[1] = packet.ReadBit();

            var unk106 = !packet.ReadBit("!unk106");
            var unk336 = !packet.ReadBit("!unk336");
            var unk101 = !packet.ReadBit("!unk101");

            guid2[5] = packet.ReadBit();

            var unk28 = !packet.ReadBit("!unk28");

            var unk388 = packet.ReadBits("unk388", 20);

            var guid98 = new byte[unk388][];
            for (var i = 0; i < unk388; i++)
                guid98[i] = packet.StartBitStream(1, 6, 2, 3, 5, 7, 0, 4);

            var guid112 = packet.StartBitStream(1, 4, 6, 7, 5, 3, 0, 2);

            guid[0] = packet.ReadBit();

            guid2[3] = packet.ReadBit();

            var unk337 = !packet.ReadBit("!unk337");

            var count192 = 0u;
            if (!unk48)
                count192 = packet.ReadBits("unk48", 7);

            for (var i = 0; i < unk84; i++)
            {
                packet.ReadBits("unk88", 4, i);
                packet.ReadBits("unk92", 4, i);
            }

            var unk102 = !packet.ReadBit("!unk102");

            guid2[1] = packet.ReadBit();

            var unk380 = packet.ReadBit("unk380");
            guid[7] = packet.ReadBit();
            var unk428 = !packet.ReadBit("!unk428");
            guid2[0] = packet.ReadBit();

            packet.ParseBitStream(guid120, 1, 7, 6, 0, 4, 2, 3, 5);
            packet.WriteGuid("guid120", guid120);

            for (var i = 0; i < unk52; i++)
            {
                packet.ParseBitStream(guid52[i], 4, 5, 3, 0, 6, 2, 1, 7);
                packet.WriteGuid("guid52", guid52[i], i);
            }

            packet.ParseBitStream(guid112, 4, 5, 1, 7, 6, 3, 2, 0);
            packet.WriteGuid("guid112", guid112);

            packet.ReadInt32("unk48");

            packet.ParseBitStream(guid416, 4, 5, 3, 2, 1, 6, 7, 0);
            packet.WriteGuid("guid416", guid416);

            if (unk184)
            {
                packet.ParseBitStream(guid160, 4, 0, 5, 7, 1, 2, 3);
                packet.ReadSingle("unkach");
                packet.ReadSingle("unkb0h");
                packet.ParseBitStream(guid160, 6);
                packet.ReadSingle("unka8h");
                packet.WriteGuid("guid160", guid160);
            }

            for (var i = 0; i < unk388; i++)
            {
                packet.ParseBitStream(guid98[i], 0);
                packet.ReadSingle("unk110", i);
                packet.ParseBitStream(guid98[i], 1, 5, 4, 7);
                packet.ReadSingle("unk114", i);
                packet.ParseBitStream(guid98[i], 3);
                packet.ReadSingle("unk106", i);
                packet.ParseBitStream(guid98[i], 6, 2);
                packet.WriteGuid("guid98", guid98[i], i);
            }

            if (unk152)
            {
                packet.ParseBitStream(guid128, 0, 5, 4, 7, 3, 6);
                packet.ReadSingle("unk88h");
                packet.ParseBitStream(guid128, 2);
                packet.ReadSingle("unk90h");
                packet.ParseBitStream(guid128, 1);
                packet.ReadSingle("unk8ch");
            }

            packet.ParseBitStream(guid, 4);

            for (var i = 0; i < count68; i++)
            {
                packet.ParseBitStream(guid68[i], 4, 2, 0, 6, 1, 7, 3, 5);
                packet.WriteGuid("guid68", guid68[i], i);
            }

            if (unk101)
                packet.ReadInt32("unk404");

            packet.ParseBitStream(guid, 2);

            if (unk102)
                packet.ReadInt32("unk408");

            if (unk380)
            {
                packet.ReadInt32("unk372");
                packet.ReadInt32("unk376");
            }

            for (var i = 0; i < unk320; i++)
            {
                packet.ReadInt32("unk324");
                packet.ReadInt32("unk328");
            }

            if (unk336)
                packet.ReadByte("unk336");

            packet.ReadInt32("unk40");

            packet.ParseBitStream(guid, 5, 7, 1);

            packet.ReadByte("unk32");

            packet.ParseBitStream(guid2, 7, 0);

            packet.ParseBitStream(guid, 6, 0);

            packet.ParseBitStream(guid2, 1);

            if (unk368)
                packet.ReadByte("unk368");

            if (unk106)
                packet.ReadInt32("unk424");

            packet.ParseBitStream(guid2, 6, 3);

            if (unk384)
                packet.ReadByte("unk384");

            if (unk89)
                packet.ReadInt32("unk356");

            packet.ReadEntry<UInt32>(StoreNameType.Spell, "Spell ID"); // 36

            if (unk91)
                packet.ReadInt32("unk364");

            packet.ParseBitStream(guid2, 4, 5);

            if (unk337)
                packet.ReadByte("unk337");

            packet.ParseBitStream(guid2, 2);

            if (count192 > 0)
                packet.ReadWoWString("str", count192);

            if (unk428)
                packet.ReadByte("unk428");

            packet.ParseBitStream(guid, 3);

            if (unk168h)
                packet.ReadSingle("unk168h");

            for (var i = 0; i < unk340; i++)
            {
                packet.ReadByte("unk344", i);
            }

            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_SPELLINTERRUPTLOG)]
        public static void HandleSpellInterruptLog(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];

            guid[7] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid[5] = packet.ReadBit();

            packet.ParseBitStream(guid, 0);
            packet.ParseBitStream(guid2, 2);
            packet.ReadEntry<Int32>(StoreNameType.Spell, "Interrupt Spell ID"); // 128
            packet.ParseBitStream(guid2, 1);
            packet.ParseBitStream(guid, 2);
            packet.ParseBitStream(guid2, 3);
            packet.ReadEntry<Int32>(StoreNameType.Spell, "Interrupted Spell ID"); // 144
            packet.ParseBitStream(guid, 4);
            packet.ParseBitStream(guid2, 4);
            packet.ParseBitStream(guid, 3, 1);
            packet.ParseBitStream(guid2, 5, 6, 7);
            packet.ParseBitStream(guid, 5, 6);
            packet.ParseBitStream(guid2, 0);
            packet.ParseBitStream(guid, 7);

            packet.WriteGuid("Target", guid);
            packet.WriteGuid("Caster", guid2);
        }

        [Parser(Opcode.SMSG_SPIRIT_HEALER_CONFIRM)]
        public static void HandleSpiritHealerConfirm(Packet packet)
        {
            var guid = packet.StartBitStream(6, 5, 7, 1, 4, 2, 3, 0);
            packet.ParseBitStream(guid, 0, 4, 2, 3, 7, 6, 5, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_TOTEM_CREATED)]
        public static void HandleTotemCreated(Packet packet)
        {
            var guid = packet.StartBitStream(6, 1, 2, 5, 3, 4, 7, 0);
            packet.ReadInt32("Duration"); // 112
            packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID"); // 96
            packet.ParseBitStream(guid, 3, 4, 5, 6, 0, 2);
            packet.ReadByte("Slot"); // 32
            packet.ParseBitStream(guid, 1, 7);
            packet.WriteGuid("Guid", guid);
        }
    }
}

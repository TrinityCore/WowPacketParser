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

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class SpellHandler
    {
        [Parser(Opcode.CMSG_CANCEL_AURA)]
        public static void HandleCancelAura(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");

                packet.ReadBit("Unk");
                var guid = packet.StartBitStream(6, 5, 1, 0, 4, 3, 2, 7);
                packet.ParseBitStream(guid, 3, 2, 1, 0, 4, 7, 5, 6);
                packet.WriteGuid("Guid", guid);
            }
            else
            {
                packet.WriteLine("              : SMSG_???");
                packet.ReadToEnd();
            }
        }

        [Parser(Opcode.CMSG_CAST_SPELL)]
        public static void HandleSpellCast(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
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

                        packet.WriteLine("Transport GUID: {0}", new Guid(BitConverter.ToUInt64(movementTransportGuid, 0)));
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

                packet.WriteLine("Item Target GUID: {0}", new Guid(BitConverter.ToUInt64(itemTargetGuid, 0)));

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

                    packet.WriteLine("Destination Transport GUID: {0}", new Guid(BitConverter.ToUInt64(destTransportGuid, 0)));
                }

                packet.ReadXORByte(targetGuid, 3); // 43
                packet.ReadXORByte(targetGuid, 4); // 44
                packet.ReadXORByte(targetGuid, 7); // 47
                packet.ReadXORByte(targetGuid, 6); // 46
                packet.ReadXORByte(targetGuid, 2); // 42
                packet.ReadXORByte(targetGuid, 0); // 40
                packet.ReadXORByte(targetGuid, 1); // 41
                packet.ReadXORByte(targetGuid, 5); // 45

                packet.WriteLine("Target GUID: {0}", new Guid(BitConverter.ToUInt64(targetGuid, 0)));

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

                    packet.WriteLine("Source Transport GUID: {0}", new Guid(BitConverter.ToUInt64(srcTransportGuid, 0)));
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
                    packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");

                if (hasGlyphIndex) // 24
                    packet.ReadUInt32("Glyph Index"); // 24
            }
            else
            {
                packet.WriteLine("              : SMSG_BATTLEGROUND_PLAYER_LEFT");
                BattlegroundHandler.HandleBattleGroundPlayerLeft(packet);
            }
        }

        [Parser(Opcode.CMSG_UNLEARN_SKILL)]
        public static void HandleUnlearnSkill(Packet packet)
        {
            packet.ReadToEnd();
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
                unk200[i] = packet.ReadBit("unk200", i);
                if (unk200[i] > 0)
                {
                    unk336[i] = packet.ReadBits("unk336", 22, i);
                    unk144[i] = packet.ReadBit("unk144", i);
                    if (unk144[i] > 0)
                        guid2[i] = packet.StartBitStream(3, 4, 6, 1, 5, 2, 0, 7);

                    unk400[i] = packet.ReadBits("unk400", 22, i);
                    unk164[i] = packet.ReadBit("unk164", i);
                    unk156[i] = packet.ReadBit("unk156", i);
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
                    packet.ReadByte("unk124", i);
                    packet.ReadInt16("unk152", i);
                    packet.ReadInt32("unk144", i);
                    if (unk156[i] > 0)
                        packet.ReadInt32("unk272", i);
                    if (unk164[i] > 0)
                        packet.ReadInt32("unk304", i);
                    for (var j = 0; j < unk400[i]; j++)
                        packet.ReadSingle("unk416", i, j);
                    packet.ReadByte("unk134", i);
                    packet.ReadInt32("unk176", i);
                    for (var j = 0; j < unk336[i]; j++)
                        packet.ReadSingle("unk352", i, j);
                }
                packet.ReadByte("unk112", i);
            }
            packet.ParseBitStream(guid, 2, 6, 7, 1, 3, 4, 0, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_INITIAL_SPELLS)]
        public static void HandleInitialSpells(Packet packet)
        {
            packet.ReadBit("Unk 1bit");
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
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
        }

        [Parser(Opcode.SMSG_REMOVED_SPELL)]
        public static void HandleRemovedSpell(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SEND_UNLEARN_SPELLS)]
        public static void HandleSendUnlearnSpells(Packet packet)
        {
            var count = packet.ReadBits("count", 22);
            for (var i = 0; i < count; i++)
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", i);
        }

        [Parser(Opcode.SMSG_SPELL_CATEGORY_COOLDOWN)]
        public static void HandleSpellCategoryCooldown(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SPELL_COOLDOWN)]
        public static void HandleSpellCooldown(Packet packet)
        {
            var guid = new byte[8];

            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var unk40 = !packet.ReadBit("!unk40");
            guid[7] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var count = packet.ReadBits("count", 21);
            guid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32("unk80", i);
                packet.ReadInt32("unk84", i);
            }
            packet.ParseBitStream(guid, 5, 3, 7);
            if (unk40)
                packet.ReadByte("unk40");
            packet.ParseBitStream(guid, 4, 1, 0, 2, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPELL_DELAYED)]
        public static void HandleSpellDelayed(Packet packet)
        {
            packet.ReadToEnd();
        }

        [HasSniffData]
        [Parser(Opcode.CMSG_CANCEL_CAST)]
        [Parser(Opcode.CMSG_CANCEL_MOUNT_AURA)]
        public static void HandleCmsgNull(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_CAST_FAILED)]
        [Parser(Opcode.SMSG_SPELL_FAILED_OTHER)]
        [Parser(Opcode.SMSG_SPELL_FAILURE)]
        [Parser(Opcode.SMSG_SPELL_START)]
        public static void HandleSpellStart(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellGo(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
            {
                packet.ReadToEnd();
            }
            else MovementHandler.HandleMoveStartBackWard(packet);
        }
    }
}

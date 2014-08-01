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
		
        [Parser(Opcode.CMSG_CAST_SPELL)]
        public static void HandleSpellCast(Packet packet)
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

            packet.ReadBit(); // Fake bit
            var hasTargetString = !packet.ReadBit("Has Target");
            packet.ReadBit(); // Fake Bit
            bool hasCastCount = !packet.ReadBit("Has Cast Count");
            bool hasSrcLocation = packet.ReadBit("Has Source Location");
            bool hasDestLocation = packet.ReadBit("Has Destination Location");
            bool hasSpellId = !packet.ReadBit("Has Spell ID");
            var researchDataCount = packet.ReadBits("Research Data Count", 2);
            bool hasTargetMask = !packet.ReadBit("Has Target Match");
            bool hasMissileSpeed = !packet.ReadBit("Has Missile Speed");

            for (var i = 0; i < researchDataCount; ++i)
                packet.ReadBits(2);

            bool hasGlyphIndex = !packet.ReadBit("Has Glyph Index");
            bool hasMovement   = packet.ReadBit("Has Movement");
            bool hasElevation  = !packet.ReadBit("Has Elevation");
            bool hasCastFlags  = !packet.ReadBit("Has Cast Flags");
            
            targetGuid[5] = packet.ReadBit();
            targetGuid[4] = packet.ReadBit();
            targetGuid[2] = packet.ReadBit();
            targetGuid[7] = packet.ReadBit();
            targetGuid[1] = packet.ReadBit();
            targetGuid[6] = packet.ReadBit();
            targetGuid[3] = packet.ReadBit();
            targetGuid[0] = packet.ReadBit();

            if (hasDestLocation)
            {
                destTransportGuid[1] = packet.ReadBit();
                destTransportGuid[3] = packet.ReadBit();
                destTransportGuid[5] = packet.ReadBit();
                destTransportGuid[0] = packet.ReadBit();
                destTransportGuid[2] = packet.ReadBit();
                destTransportGuid[6] = packet.ReadBit();
                destTransportGuid[7] = packet.ReadBit();
                destTransportGuid[4] = packet.ReadBit();
            }


            if (hasMovement)
            {
                unkMovementLoopCounter = packet.ReadBits(22);
                packet.ReadBit();
                movementGuid[4] = packet.ReadBit();
                hasTransport = packet.ReadBit("Has Transport");

                if (hasTransport)
                {
                    hasTransportTime2 = packet.ReadBit();
                    movementTransportGuid[7] = packet.ReadBit();
                    movementTransportGuid[4] = packet.ReadBit();
                    movementTransportGuid[1] = packet.ReadBit();
                    movementTransportGuid[0] = packet.ReadBit();
                    movementTransportGuid[6] = packet.ReadBit();
                    movementTransportGuid[3] = packet.ReadBit();
                    movementTransportGuid[5] = packet.ReadBit();
                    hasTransportTime3 = packet.ReadBit();
                    movementTransportGuid[2] = packet.ReadBit();
                }

                packet.ReadBit();
                movementGuid[7] = packet.ReadBit();
                hasOrientation = !packet.ReadBit("Has Orientation");
                movementGuid[6] = packet.ReadBit();
                hasSplineElevation = !packet.ReadBit("Has Spline Elevation");
                hasPitch = !packet.ReadBit("Has Pitch");
                movementGuid[0] = packet.ReadBit();
                packet.ReadBit();
                bool hasMovementFlags = !packet.ReadBit();
                hasTimestamp = !packet.ReadBit();
                hasUnkMovementField = !packet.ReadBit();

                if (hasMovementFlags)
                    packet.ReadBits("Flags", 30);

                movementGuid[1] = packet.ReadBit();
                movementGuid[3] = packet.ReadBit();
                movementGuid[2] = packet.ReadBit();
                movementGuid[5] = packet.ReadBit();
                hasFallData = packet.ReadBit("Has Fall Data");

                if (hasFallData)
                    hasFallDirection = packet.ReadBit("Has Fall Direction");

                bool hasMovementFlags2 = !packet.ReadBit("Has Movement Flags 2");

                if (hasMovementFlags2)
                    packet.ReadBits("Movement Flags 2", 13);
            }

            itemTargetGuid[1] = packet.ReadBit();
            itemTargetGuid[0] = packet.ReadBit();
            itemTargetGuid[7] = packet.ReadBit();
            itemTargetGuid[4] = packet.ReadBit();
            itemTargetGuid[6] = packet.ReadBit();
            itemTargetGuid[5] = packet.ReadBit();
            itemTargetGuid[3] = packet.ReadBit();
            itemTargetGuid[2] = packet.ReadBit();

            if (hasSrcLocation)
            {
                srcTransportGuid[4] = packet.ReadBit();
                srcTransportGuid[5] = packet.ReadBit();
                srcTransportGuid[3] = packet.ReadBit();
                srcTransportGuid[0] = packet.ReadBit();
                srcTransportGuid[7] = packet.ReadBit();
                srcTransportGuid[1] = packet.ReadBit();
                srcTransportGuid[6] = packet.ReadBit();
                srcTransportGuid[2] = packet.ReadBit();
            }

            if (hasTargetMask)
                packet.ReadBits("Target Mask", 20);

            if (hasCastFlags)
                packet.ReadBits("Cast Flags", 5);

            if (hasTargetString)
                packet.ReadBits("Target String Length", 7);

            for (var i = 0; i < researchDataCount; ++i)
            {
                packet.ReadUInt32();
                packet.ReadUInt32();
            }

            if (hasMovement)
            {
                packet.ReadSingle("Position X");
                packet.ReadXORByte(movementGuid, 0);

                if (hasTransport)
                {
                    packet.ReadXORByte(movementTransportGuid, 2);
                    packet.ReadByte("Transport Seat");
                    packet.ReadXORByte(movementTransportGuid, 3);
                    packet.ReadXORByte(movementTransportGuid, 7);
                    packet.ReadSingle("Transport Position X");
                    packet.ReadXORByte(movementTransportGuid, 5);

                    if (hasTransportTime3)
                        packet.ReadUInt32("Transport Time 3");
                    
                    packet.ReadSingle("Transport Position Z");
                    packet.ReadSingle("Transport Position Y");
                    
                    packet.ReadXORByte(movementTransportGuid, 6);
                    packet.ReadXORByte(movementTransportGuid, 1);
                    packet.ReadSingle("Transport Position O");
                    
                    packet.ReadXORByte(movementTransportGuid, 4);

                    if (hasTransportTime2)
                        packet.ReadUInt32("Transport Time 2");
                    
                    packet.ReadXORByte(movementTransportGuid, 0);

                    packet.ReadUInt32("Transport Time");
                    
                    packet.WriteLine("Transport GUID: {0}", new Guid(BitConverter.ToUInt64(movementTransportGuid, 0)));
                }
                
                packet.ReadXORByte(movementGuid, 5);

                if (hasFallData)
                {
                    packet.ReadUInt32("Fall Time");
                    packet.ReadSingle("Jump Speed Z");

                    if (hasFallDirection)
                    {
                        packet.ReadSingle("Sin Angle");
                        packet.ReadSingle("XY Speed");
                        packet.ReadSingle("Cos Angle");
                    }
                }

                if (hasSplineElevation)
                    packet.ReadSingle("Spline Elevation");
                
                packet.ReadXORByte(movementGuid, 6);

                if (hasUnkMovementField)
                    packet.ReadUInt32();
                
                packet.ReadXORByte(movementGuid, 4);

                if (hasOrientation)
                    packet.ReadSingle("Orientation");

                if (hasTimestamp)
                    packet.ReadUInt32("Time Stamp");
                
                packet.ReadXORByte(movementGuid, 1);

                if (hasPitch)
                    packet.ReadSingle("Pitch");

                packet.ReadXORByte(movementGuid, 3);

                for (var i = 0; i != unkMovementLoopCounter; i++)
                    packet.ReadUInt32();

                packet.ReadSingle("Position Y");
                packet.ReadXORByte(movementGuid, 7);
                packet.ReadSingle("Position Z");
                packet.ReadXORByte(movementGuid, 2);
            }

            packet.ReadXORByte(itemTargetGuid, 4);
            packet.ReadXORByte(itemTargetGuid, 2);
            packet.ReadXORByte(itemTargetGuid, 1);
            packet.ReadXORByte(itemTargetGuid, 5);
            packet.ReadXORByte(itemTargetGuid, 7);
            packet.ReadXORByte(itemTargetGuid, 3);
            packet.ReadXORByte(itemTargetGuid, 6);
            packet.ReadXORByte(itemTargetGuid, 0);

            packet.WriteLine("Item Target GUID: {0}", new Guid(BitConverter.ToUInt64(itemTargetGuid, 0)));

            if (hasDestLocation)
            {
                packet.ReadXORByte(destTransportGuid, 2);
                packet.ReadSingle("Position X");
                packet.ReadXORByte(destTransportGuid, 4);
                packet.ReadXORByte(destTransportGuid, 1);
                packet.ReadXORByte(destTransportGuid, 0);
                packet.ReadXORByte(destTransportGuid, 3);
                packet.ReadSingle("Position Y");
                packet.ReadXORByte(destTransportGuid, 7);
                packet.ReadSingle("Position Z");
                packet.ReadXORByte(destTransportGuid, 5);
                packet.ReadXORByte(destTransportGuid, 6);

                packet.WriteLine("Destination Transport GUID: {0}", new Guid(BitConverter.ToUInt64(destTransportGuid, 0)));
            }

            packet.ReadXORByte(targetGuid, 3);
            packet.ReadXORByte(targetGuid, 4);
            packet.ReadXORByte(targetGuid, 7);
            packet.ReadXORByte(targetGuid, 6);
            packet.ReadXORByte(targetGuid, 2);
            packet.ReadXORByte(targetGuid, 0);
            packet.ReadXORByte(targetGuid, 1);
            packet.ReadXORByte(targetGuid, 5);
            
            packet.WriteLine("Target GUID: {0}", new Guid(BitConverter.ToUInt64(targetGuid, 0)));

            if (hasSrcLocation)
            {
                packet.ReadSingle("Position Y");
                packet.ReadXORByte(srcTransportGuid, 5);
                packet.ReadXORByte(srcTransportGuid, 1);
                packet.ReadXORByte(srcTransportGuid, 7);
                packet.ReadXORByte(srcTransportGuid, 6);
                packet.ReadSingle("Position X");
                packet.ReadXORByte(srcTransportGuid, 3);
                packet.ReadXORByte(srcTransportGuid, 2);
                packet.ReadXORByte(srcTransportGuid, 0);
                packet.ReadXORByte(srcTransportGuid, 4);
                packet.ReadSingle("Position Z");

                packet.WriteLine("Source Transport GUID: {0}", new Guid(BitConverter.ToUInt64(srcTransportGuid, 0)));
            }

            if (hasTargetString)
                packet.ReadWoWString("Target String", targetStringLength);

            if (hasMissileSpeed)
                packet.ReadSingle("Missile Speed");

            if (hasElevation)
                packet.ReadSingle("Elevation");

            if (hasCastCount)
                packet.ReadByte("Cast Count");

            if (hasSpellId)
                packet.ReadUInt32("Spell ID");

            if (hasGlyphIndex)
                packet.ReadUInt32("Glyph Index");
        }
    }
}

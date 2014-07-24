using System;
using System.Collections;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using Guid = WowPacketParser.Misc.Guid;
using UpdateFields = WowPacketParser.Enums.Version.UpdateFields;
using WowPacketParserModule.V5_4_8_18414.Enums;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class UpdateHandler
    {
        [Parser(Opcode.SMSG_DESTROY_OBJECT)]
        public static void HandleDestroyObject(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
            {
                var guid = new byte[8];

                guid[3] = packet.ReadBit();
                guid[2] = packet.ReadBit();
                guid[4] = packet.ReadBit();
                guid[1] = packet.ReadBit();

                packet.ReadBit("Despawn Animation");

                guid[7] = packet.ReadBit();
                guid[0] = packet.ReadBit();
                guid[6] = packet.ReadBit();
                guid[5] = packet.ReadBit();

                packet.ParseBitStream(guid, 0, 4, 7, 2, 6, 3, 1, 5);

                packet.WriteGuid("Object Guid", guid);
            }
            else
            {
                packet.WriteLine("              : CMSG_PVP_LOG_DATA");
            }
        }

        [HasSniffData] // in ReadCreateObjectBlock
        [Parser(Opcode.SMSG_UPDATE_OBJECT)]
        public static void HandleUpdateObject(Packet packet)
        {
            packet.WriteLine("pos: " + packet.Position);
            uint map = packet.ReadUInt16("Map");
            var count = packet.ReadUInt32("Count");
            //var type = packet.ReadByte();
            //var typeString = ((UpdateType2)type).ToString();

            for (var i = 0; i < count; i++)
            {
                var type = packet.ReadByte();
                var typeString = ((UpdateTypeCataclysm)type).ToString();

                packet.ResetBitReader();

                packet.WriteLine("[" + i + "] UpdateType: " + typeString);
                switch (typeString)
                {
                    case "Values":
                    {
                        var guid = packet.ReadPackedGuid("GUID", i);

                        WoWObject obj;
                        var updates = CoreParsers.UpdateHandler.ReadValuesUpdateBlock(ref packet, guid.GetObjectType(), i, false);

                        if (Storage.Objects.TryGetValue(guid, out obj))
                        {
                            if (obj.ChangedUpdateFieldsList == null)
                                obj.ChangedUpdateFieldsList = new List<Dictionary<int, UpdateField>>();
                            obj.ChangedUpdateFieldsList.Add(updates);
                        }

                        break;
                    }
                    case "CreateObject1":
                    case "CreateObject2":
                    {
                        var guid = packet.ReadPackedGuid("GUID", i);
                        ReadCreateObjectBlock(ref packet, guid, map, i);
                        break;
                    }
                    case "DestroyObjects":
                    {
                        CoreParsers.UpdateHandler.ReadObjectsBlock(ref packet, i);
                        break;
                    }
                }
            }
        }

        private static void ReadCreateObjectBlock(ref Packet packet, Guid guid, uint map, int index)
        {
            var objType = packet.ReadEnum<ObjectType>("Object Type", TypeCode.Byte, index);
            packet.WriteLine("Pos: " + packet.Position);
            packet.ResetBitReader();
            var moves = ReadMovementUpdateBlock548(ref packet, guid, index);
            packet.WriteLine("Pos: " + packet.Position);
            packet.ResetBitReader();
            var updates = CoreParsers.UpdateHandler.ReadValuesUpdateBlock(ref packet, objType, index, true);

            WoWObject obj;
            switch (objType)
            {
                case ObjectType.Unit:
                    obj = new Unit();
                    break;
                case ObjectType.GameObject:
                    obj = new GameObject();
                    break;
                case ObjectType.Item:
                    obj = new Item();
                    break;
                case ObjectType.Player:
                    obj = new Player();
                    break;
                default:
                    obj = new WoWObject();
                    break;
            }

            obj.Type = objType;
            obj.Movement = moves;
            obj.UpdateFields = updates;
            obj.Map = map;
            obj.Area = CoreParsers.WorldStateHandler.CurrentAreaId;
            obj.PhaseMask = (uint)CoreParsers.MovementHandler.CurrentPhaseMask;

            // If this is the second time we see the same object (same guid,
            // same position) update its phasemask
            if (Storage.Objects.ContainsKey(guid))
            {
                var existObj = Storage.Objects[guid].Item1;
                CoreParsers.UpdateHandler.ProcessExistingObject(ref existObj, obj, guid); // can't do "ref Storage.Objects[guid].Item1 directly
            }
            else
                Storage.Objects.Add(guid, obj, packet.TimeSpan);

            if (guid.HasEntry() && (objType == ObjectType.Unit || objType == ObjectType.GameObject))
                packet.AddSniffData(Utilities.ObjectTypeToStore(objType), (int)guid.GetEntry(), "SPAWN");
        }

        private static MovementInfo ReadMovementUpdateBlock548(ref Packet packet, Guid guid, int index)
        {
            var moveInfo = new MovementInfo();
            
            var guid1            = new byte[8];
            var transportGuid    = new byte[8];
            var goTransportGuid  = new byte[8];
            var targetGuid       = new byte[8];
            var SplineFacingTargetGuid = new byte[8];
            var hasFallDirection = false;
            var hasOrientation = false;
            var hasFallData = false;
            var bit336 = false;
            var hasSplineStartTime = false;
            var splineCount = 0u;
            var splineType = SplineType.Stop;
            var hasSplineVerticalAcceleration = false;
            var hasSplineUnkPart = false;
            var hasTransport = false;
            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasGOTransportTime2 = false;
            var hasGOTransportTime3 = false;
            var hasAnimKit1 = false;
            var hasAnimKit2 = false;
            var hasAnimKit3 = false;
            var Unk2Count = 0u;
            var Unk9Count = 0u;
            var loopcnt = 0u;
            var unk284count = 0u;
            var readUint32 = false;
            var unk144count = 0u;
            var skipFloat = false;
            var skipInt = false;
            var hasFloat3 = false;

            packet.ResetBitReader();

            var hasUnk1 = packet.ReadBit("hasUnk1", index); // 676+
            var hasAnimKits = packet.ReadBit("Has AnimKits", index); // 498+
            var hasLiving = packet.ReadBit("Has Living", index); // 368+-
            var hasUnk2 = packet.ReadBit("hasUnk2", index); // 810+
            var hasUnk3 = packet.ReadBit("hasUnk3", index); // 2-
            var unkLoopCounter = packet.ReadBits("Unknown array size", 22, index); // 1068+
            var hasVehicle = packet.ReadBit("Has Vehicle Data", index); // 488+
            var hasUnk4 = packet.ReadBit("hasUnk4", index); // 1044+
            var hasUnk5 = packet.ReadBit("hasUnk5", index); // 1
            var hasUnk6 = packet.ReadBit("hasUnk6", index); // 476+
            var hasGobjectRotation = packet.ReadBit("Has GameObject Rotation", index); // 512+
            var hasUnk7 = packet.ReadBit("hasUnk7", index); // 3
            var hasUpdateFlagSelf = packet.ReadBit("Has Update Flag Self", index); // 680-
            var hasTarget = packet.ReadBit("Has Target", index); // 464+
            var hasUnk8 = packet.ReadBit("hasUnk8", index); // 1032+
            var hasUnk9 = packet.ReadBit("hasUnk9", index); // 1064+
            var hasUnk10 = packet.ReadBit("hasUnk10", index); // 0
            var hasUnk11 = packet.ReadBit("hasUnk11", index); // 668+
            var hasGOPosition = packet.ReadBit("Has goTransport Position", index); // 424+
            var hasUnk12 = packet.ReadBit("hasUnk12", index); // 681-
            var hasStationaryPosition = packet.ReadBit("Has Stationary Position", index); // 448

            packet.WriteLine("pos448: " + packet.Position);

            //42bits
            if (hasLiving) // 368
            {
                guid1[2] = packet.ReadBit(); // 10
                packet.ReadBit(); // 140-
                skipFloat = packet.ReadBit("skip float", index); // 104+ if (skipFloat) dword ptr [esi+68h] = 0 else dword ptr [esi+68h] = ds:dword_D26EA8
                hasTransport = packet.ReadBit("has transport", index); // 96+
                packet.ReadBit(); // 164-
                
                if (hasTransport) // 96
                {
                    transportGuid[4] = packet.ReadBit(); // 52
                    transportGuid[2] = packet.ReadBit(); // 50
                    hasTransportTime3 = packet.ReadBit("Transport Time3", index); // 92+
                    transportGuid[0] = packet.ReadBit(); // 48
                    transportGuid[1] = packet.ReadBit(); // 49
                    transportGuid[3] = packet.ReadBit(); // 51
                    transportGuid[6] = packet.ReadBit(); // 54
                    transportGuid[7] = packet.ReadBit(); // 55
                    hasTransportTime2 = packet.ReadBit("Transport Time2", index); // 84+
                    transportGuid[5] = packet.ReadBit(); // 53
                }

                readUint32 = !packet.ReadBit("skip readunit32", index); // 24+
                guid1[6] = packet.ReadBit(); // 14
                guid1[4] = packet.ReadBit(); // 12
                guid1[3] = packet.ReadBit(); // 11

                hasOrientation = !packet.ReadBit("skip Orient", index); // 40+ if (hasOrientation) dword ptr [esi+28h] = 0 else dword ptr [esi+28h] = ds:dword_D26EA8
                
                skipInt = !packet.ReadBit("no has Int", index); // 160*4 +
                
                guid1[5] = packet.ReadBit(); // 13
                unk144count = packet.ReadBits("Is Living Unk Counter", 22, index); // 144+
                var hasMovementFlags = !packet.ReadBit("no hasMovementFlags", index); // 16*4 +
                loopcnt = packet.ReadBits("IsLicingUnkLoop", 19, index); // 352+

                for (var i = 0; i < loopcnt; i++ ) // 352
                {
                    packet.ReadBits("unk356", 2, index, i);  // 356
                }

                hasFallData = packet.ReadBit("has fall data", index); // 132+

                if (hasMovementFlags) // 16*4
                    moveInfo.Flags = (WowPacketParser.Enums.MovementFlag)packet.ReadEnum<MovementFlag>("Movement Flags", 30, index);

                hasFloat3 = !packet.ReadBit("skip float3", index); // 136+ if (skipFloat3) dword ptr [esi+88h] = 0 else dword ptr [esi+88h] = ds:dword_D26EA8
                moveInfo.HasSplineData = packet.ReadBit("has Spline", index); // 344+
                packet.ReadBit(); // 141-
                guid1[0] = packet.ReadBit(); // 8
                guid1[7] = packet.ReadBit(); // 15
                guid1[1] = packet.ReadBit(); // 9

                if (moveInfo.HasSplineData) // 344
                {
                    bit336 = packet.ReadBit("Has extended spline data", index); // 336+
                    if (bit336)
                    {
                        hasSplineVerticalAcceleration = packet.ReadBit("Has spline vertical acceleration", index); // 260+
                        hasSplineStartTime = packet.ReadBit("Has spline start time", index); // 252+
                        hasSplineUnkPart = packet.ReadBit(); // 304+
                        splineCount = packet.ReadBits("Spline Waypoints", 20, index); // 264+
                        packet.ReadBits("Unk bits", 2, index); // 280-
                        packet.ReadEnum<SplineFlag434>("Spline flags", 25, index); // 224

                        if (hasSplineUnkPart) // 304
                        {
                            unk284count = packet.ReadBits("Unk dword284", 21, index); // 284
                            packet.ReadBits("Unk word300", 2, index);  // unk word300
                        }
                    }
                }
                
                var hasMovementFlagsExtra = !packet.ReadBit("no hasMovementFlagsExtra", index); // 20*4

                if (hasFallData) // 132
                    hasFallDirection = packet.ReadBit("Has Fall Direction", index); // 128

                if (hasMovementFlagsExtra) // 20*4
                    moveInfo.FlagsExtra = packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13, index); // 20*4
            }
            //107bits

            packet.WriteLine("Pos20: " + packet.Position);

            var guid856 = new byte[2][];

            var guid840 = new byte[8];

            if (hasUnk8) // 1032
            {
                for (var i = 2; i > 0; i--)
                {
                    guid856[i - 1] = new byte[8];

                    guid856[i - 1][5] = packet.ReadBit();
                    guid856[i - 1][2] = packet.ReadBit();
                    guid856[i - 1][3] = packet.ReadBit();
                    guid856[i - 1][1] = packet.ReadBit();
                    guid856[i - 1][6] = packet.ReadBit();
                    guid856[i - 1][7] = packet.ReadBit();
                    packet.ReadBit("unk868", i);
                    packet.ReadBit("unk892", i);
                    var count876 = packet.ReadBits(2);
                    for (var j = 0; j < count876; j++)
                    {
                        var v27 = packet.ReadBits("v27", 21);
                        //sub_70FC09;
                        packet.ReadBit();
                        packet.ReadBit("unk48");
                        packet.ReadBit();
                        packet.ReadBit();
                        packet.ReadBit();
                        packet.ReadBit();
                        packet.ReadBit();
                        packet.ReadBit();
                        packet.ReadBit();
                        packet.ReadBits(7);
                        var v31 = packet.ReadBits(21);
                        //sub_70fb9c
                        for ( var k = 0; k < v31; k++)
                        {
                            packet.ReadBit();
                            packet.ReadBit();
                            packet.ReadBit();
                        }
                        packet.ReadBit();
                        var v44 = packet.ReadBits(20);
                        for (var k = 0; k < v44; k++)
                        {
                            packet.ReadBit();
                        }
                    }
                    guid856[i - 1][4] = packet.ReadBit();
                    packet.ReadBit("!unk14*2");
                    guid856[i - 1][0] = packet.ReadBit();

                }
                packet.ReadBit("unk832");

                for (var i = 3; i > 0; i--)
                {
                    var count16 = packet.ReadBits(21);
                    var v54 = packet.ReadBits(21);
                    for (var j = 0; j < v54; j++)
                    {
                        packet.ReadBit();
                        packet.ReadBit();
                        packet.ReadBit();
                    }
                }

                packet.ReadBit("!unk828*4");
                packet.ReadBit("unk848");
                packet.ReadBit("!unk816*2");
                packet.ReadBit("!unk824*4");
                var unk844 = packet.ReadBit("!unk844") ? 0u : 1u;

                guid840 = packet.StartBitStream(6, 2, 4, 5, 1, 0, 3, 7);

                var unk818 = !packet.ReadBit("!unk818*2");
                var unk833 = !packet.ReadBit("!unk833");
                var unk849 = packet.ReadBit("unk849");
            }

            if (hasGOPosition) // 424
            {
                goTransportGuid[4] = packet.ReadBit(); // 380
                goTransportGuid[1] = packet.ReadBit(); // 377
                goTransportGuid[0] = packet.ReadBit(); // 376
                hasGOTransportTime3 = packet.ReadBit("goTransport Time3", index); // 420
                goTransportGuid[6] = packet.ReadBit(); // 382
                goTransportGuid[5] = packet.ReadBit(); // 381
                goTransportGuid[3] = packet.ReadBit(); // 379
                goTransportGuid[2] = packet.ReadBit(); // 378
                goTransportGuid[7] = packet.ReadBit(); // 383
                hasGOTransportTime2 = packet.ReadBit("goTransport Time2", index); // 412
            }

            var unkBlock1bit664 = false;
            var count648 = 0u;
            var unkBlock1bit644 = false;
            var unkBlock1bit528 = false;
            var unkBlock1bit600 = false;
            var unkBlock1bit544 = false;
            var unkblock1bit526 = false;
            var unkBlock1bit552 = false;
            var unkBlock1bit524 = false;
            var unkBlock1bit572 = false;
            var unkBlock1bit525 = false;
            var unkBlock1bit527 = false;
            var unkBlock1bit536 = false;
            var unkBlock1bit560 = false;
            var count604 = 0u;
            var count620 = 0u;

            if (hasUnk11) // 668
            {
                unkBlock1bit528 = packet.ReadBit(); // 528
                unkBlock1bit600 = packet.ReadBit(); // 600
                unkBlock1bit544 = packet.ReadBit(); // 544
                unkblock1bit526 = packet.ReadBit(); // 526
                unkBlock1bit552 = packet.ReadBit(); // 552
                unkBlock1bit524 = packet.ReadBit(); // 524
                unkBlock1bit572 = packet.ReadBit(); // 572
                unkBlock1bit525 = packet.ReadBit(); // 525
                unkBlock1bit664 = packet.ReadBit(); // 664
                unkBlock1bit527 = packet.ReadBit(); // 527

                if (unkBlock1bit664) // 664
                {
                    count648 = packet.ReadBits("Block1 unk648", 20, index); // 648
                }

                unkBlock1bit536 = packet.ReadBit(); // 536
                unkBlock1bit644 = packet.ReadBit(); // 644
                unkBlock1bit560 = packet.ReadBit(); // 560

                if (unkBlock1bit644) // 644
                {
                    count604 = packet.ReadBits("Block1 unk604", 21, index); // 604
                    count620 = packet.ReadBits("Block1 unk620", 21, index); // 620
                }
            }

            if (hasAnimKits) // 498
            {
                hasAnimKit2 = !packet.ReadBit("skip AnimKit2", index); // 494*2
                hasAnimKit3 = !packet.ReadBit("skip AnimKit3", index); // 496*2
                hasAnimKit1 = !packet.ReadBit("skip AnimKit1", index); // 492*2
            }

            if (hasTarget) // 464
                targetGuid = packet.StartBitStream(4, 6, 5, 2, 0, 1, 3, 7);

            if (hasUnk9) // 1064
            {
                Unk9Count = packet.ReadBits("unk1048", 22, index); // 1048
            }

            if (hasUnk2) // 810
            {
                Unk2Count = packet.ReadBits("unk682", 7, index); // 682*4
            }

            // Reading data
            for (var i = 0u; i < unkLoopCounter; i++) // 1068*4
                packet.ReadUInt32("Unk UInt32", index, (int)i); // 1072*4

            packet.ResetBitReader(); //???

            /*
            if (hasUnk8) // 1032
            {
               ... // not completed
            }
            */

            packet.WriteLine("Pos01: " + packet.Position);

            if (hasLiving) // 368
            {
                if (hasTransport) // 96
                {
                    packet.ReadXORByte(transportGuid, 7); // 55
                    moveInfo.TransportOffset.X = packet.ReadSingle(); // 56

                    if (hasTransportTime3) // 92
                        packet.ReadUInt32("Transport Time 3", index); // 88

                    moveInfo.TransportOffset.O = packet.ReadSingle(); // 68
                    moveInfo.TransportOffset.Y = packet.ReadSingle(); // 60
                    packet.ReadXORByte(transportGuid, 4); // 52
                    packet.ReadXORByte(transportGuid, 1); // 49
                    packet.ReadXORByte(transportGuid, 3); // 51
                    moveInfo.TransportOffset.Z = packet.ReadSingle(); // 64
                    packet.ReadXORByte(transportGuid, 5); // 53

                    if (hasTransportTime2) // 84
                        packet.ReadUInt32("Transport Time 2", index); // 80

                    packet.ReadXORByte(transportGuid, 0); // 48
                    packet.ReadSByte("Transport Seat", index); // 72
                    packet.ReadXORByte(transportGuid, 6); // 54
                    packet.ReadXORByte(transportGuid, 2); // 50
                    packet.ReadUInt32("Transport Time", index); // 76

                    moveInfo.TransportGuid = new Guid(BitConverter.ToUInt64(transportGuid, 0));
                    packet.WriteLine("[{0}] Transport GUID {1}", index, moveInfo.TransportGuid);
                    packet.WriteLine("[{0}] Transport Position: {1}", index, moveInfo.TransportOffset);
                }

                packet.ReadXORByte(guid1, 4); // 12

                for (var i = 0; i < loopcnt; i++ ) // 352
                {
                    packet.ReadSingle("Single364", index); // 364
                    packet.ReadSingle("Single368", index); // 368
                    packet.ReadSingle("Single360", index); // 360
                    packet.ReadInt32("Int356", index); // 356
                    packet.ReadInt32("Int372", index); // 372
                    packet.ReadSingle("Single376", index); // 376
                }

                if (moveInfo.HasSplineData) // 344
                {
                    if (bit336) // 336
                    {
                        packet.ReadUInt32("Spline Time", index); // 232
                        packet.ReadSingle("Spline Duration Multiplier Next", index); // 244

                        if (hasSplineUnkPart) // 304
                        {
                            for (var i = 0; i < unk284count; i++ )
                            {
                                packet.ReadSingle("unk292", index, i); // 292
                                packet.ReadSingle("unk288", index, i); // 288
                            }
                        }

                        packet.ReadSingle("Spline Duration Multiplier", index); // 240

                        for (var i = 0u; i < splineCount; i++) // 264
                        {
                            var wp = new Vector3
                            {
                                X = packet.ReadSingle(), // 268
                                Z = packet.ReadSingle(), // 276
                                Y = packet.ReadSingle(), // 272
                            };

                            packet.WriteLine("[{0}][{1}] Spline Waypoint: {2}", index, i, wp);
                        }

                        if (hasSplineVerticalAcceleration) // 260
                            packet.ReadInt32("Spline Vertical Acceleration", index); // 256

                        splineType = packet.ReadEnum<SplineType>("Spline Type", TypeCode.Byte, index); // 228

                        if (splineType == SplineType.FacingAngle) // 228
                            packet.ReadSingle("Facing Angle", index); // 308

                        if (splineType == SplineType.FacingSpot) // 228
                        {
                            var point = new Vector3
                            {
                                X = packet.ReadSingle(), // 320
                                Z = packet.ReadSingle(), // 328
                                Y = packet.ReadSingle(), // 324
                            };

                            packet.WriteLine("[{0}] Facing Spot: {1}", index, point);
                        }

                        if (hasSplineStartTime) // 252
                            packet.ReadUInt32("Spline Start time", index); // 248

                        packet.ReadUInt32("Spline Full Time", index); // 236
                    }

                    var endPoint = new Vector3();

                    endPoint.X = packet.ReadSingle(); // 212
                    endPoint.Z = packet.ReadSingle(); // 220
                    packet.ReadUInt32("Spline Id", index); // 208
                    endPoint.Y = packet.ReadSingle(); // 216

                    packet.WriteLine("[{0}] Spline Endpoint: {1}", index, endPoint);
                }

                packet.ReadSingle("Flight Speed", index); // 188

                if (skipInt) // 160
                    packet.ReadInt32("unk160", index); // 160

                packet.ReadXORByte(guid1, 2); // 10

                if (hasFallData) // 132
                {
                    if (hasFallDirection) // 128
                    {
                        packet.ReadSingle("Jump Speed", index); // 124
                        packet.ReadSingle("Jump Sin Angle", index); // 116
                        packet.ReadSingle("Jump Cos Angle", index); // 120
                    }

                    packet.ReadUInt32("Fall Time", index); // 108
                    packet.ReadSingle("Jump Z Speed", index); // 112
                }

                packet.ReadXORByte(guid1, 1); // 9
                packet.ReadSingle("Turn Speed", index); // 196

                if (readUint32) // 24
                    packet.ReadInt32("unk24", index); // 24

                packet.ReadSingle("Flight Back Speed", index); // 176

                if (hasFloat3) // 136
                    packet.ReadSingle("unk136", index); // 136

                packet.ReadXORByte(guid1, 7); // 15
                packet.ReadSingle("Pitch Rate", index); // 200

                for (var i = 0u; i < unk144count; i++ ) // 144
                    packet.ReadInt32("unk148", index); // 148

                moveInfo.Position.X = packet.ReadSingle(); // 28

                if (!skipFloat) // 104
                    packet.ReadSingle("unk104", index); // 104

                if (hasOrientation) // 40
                    moveInfo.Orientation = packet.ReadSingle("Orientation", index); // 40

                moveInfo.WalkSpeed = packet.ReadSingle("Walk Speed", index); // 168
                moveInfo.Position.Y = packet.ReadSingle(); // 32

                packet.ReadSingle("Run Back Speed", index); // 192
                packet.ReadXORByte(guid1, 3); // 11
                packet.ReadXORByte(guid1, 5); // 13
                packet.ReadXORByte(guid1, 6); // 14
                packet.ReadXORByte(guid1, 0); // 8

                packet.ReadSingle("Swim Back Speed", index); // 184
                moveInfo.RunSpeed = packet.ReadSingle("Run Speed", index); // 172
                packet.ReadSingle("Swim Speed", index); // 180
                moveInfo.Position.Z = packet.ReadSingle(); // 36

                packet.WriteLine("[{0}] GUID 1: {1}", index, new Guid(BitConverter.ToUInt64(guid1, 0)));
                packet.WriteLine("[{0}] Position: {1}", index, moveInfo.Position);
                packet.WriteLine("[{0}] Orientation: {1}", index, moveInfo.Orientation);
            }

            if (hasUnk11) // 668
            {
                if (unkBlock1bit664) // 664
                {
                    for (var i = 0; i < count648; i++)
                    {
                        packet.ReadSingle("unk652*4+4", i);
                        packet.ReadSingle("unk652*4", i);
                        packet.ReadSingle("unk652*4+8", i);
                    }
                }
                if (unkBlock1bit600)
                {
                    packet.ReadSingle("unk584");
                    packet.ReadSingle("unk580");
                    packet.ReadSingle("unk596");
                    packet.ReadSingle("unk592");
                    packet.ReadSingle("unk576");
                    packet.ReadSingle("unk588");
                }
                if (unkBlock1bit644)
                {
                    for (var i = 0; i < count604; i++)
                    {
                        packet.ReadSingle("unk608*4", i);
                        packet.ReadSingle("unk608*4+4", i);
                    }
                    for (var i = 0; i < count620; i++)
                    {
                        packet.ReadSingle("unk624*4", i);
                        packet.ReadSingle("unk624*4+4", i);
                    }
                    packet.ReadSingle("unk640");
                    packet.ReadSingle("unk636");
                }
                packet.ReadInt32("unk520");
                if (unkBlock1bit544)
                    packet.ReadInt32("unk540");
                if (unkBlock1bit552)
                    packet.ReadInt32("unk548");
                if (unkBlock1bit536)
                    packet.ReadInt32("unk532");
                if (unkBlock1bit560)
                    packet.ReadInt32("unk556");
                if (unkBlock1bit572)
                {
                    packet.ReadSingle("unk564");
                    packet.ReadSingle("unk568");
                }
            }

            if (hasGOPosition) // 424
            {
                if (hasGOTransportTime3) // 420
                    packet.ReadUInt32("GO Transport Time 3", index); // 416

                moveInfo.TransportOffset.Y = packet.ReadSingle(); // 388
                packet.ReadByte("GO Transport Seat", index); // 400
                moveInfo.TransportOffset.X = packet.ReadSingle(); // 384
                packet.ReadXORByte(goTransportGuid, 2); // 378
                packet.ReadXORByte(goTransportGuid, 4); // 380
                packet.ReadXORByte(goTransportGuid, 1); // 377

                if (hasGOTransportTime2) // 412
                    packet.ReadUInt32("GO Transport Time 2", index); // 408

                packet.ReadUInt32("GO Transport Time", index); // 404

                moveInfo.TransportOffset.O = packet.ReadSingle(); // 396
                moveInfo.TransportOffset.Z = packet.ReadSingle(); // 392
                packet.ReadXORByte(goTransportGuid, 6); // 382
                packet.ReadXORByte(goTransportGuid, 0); // 376
                packet.ReadXORByte(goTransportGuid, 5); // 381
                packet.ReadXORByte(goTransportGuid, 3); // 379
                packet.ReadXORByte(goTransportGuid, 7); // 383

                moveInfo.TransportGuid = new Guid(BitConverter.ToUInt64(goTransportGuid, 0));
                packet.WriteLine("[{0}] GO Transport GUID {1}", index, moveInfo.TransportGuid);
                packet.WriteLine("[{0}] GO Transport Position: {1}", index, moveInfo.TransportOffset);
            }

            if (hasTarget) // 464
            {
                packet.ParseBitStream(targetGuid, 7, 1, 5, 2, 6, 3, 0, 4);  // 456
                packet.WriteGuid("Target GUID", targetGuid, index);
            }

            if (hasVehicle) // 488
            {
                moveInfo.VehicleId = packet.ReadUInt32("Vehicle Id", index); // 480
                packet.ReadSingle("Vehicle Orientation", index); // 484
            }

            if (hasStationaryPosition) // 448
            {
                moveInfo.Position.Y = packet.ReadSingle("Stationary Y", index); // 436
                moveInfo.Position.Z = packet.ReadSingle("Stationary Z", index); // 440
                moveInfo.Orientation = packet.ReadSingle("Stationary O", index); // 444
                moveInfo.Position.X = packet.ReadSingle("Stationary X", index); // 432
            }

            if (hasUnk1) // 676
                packet.ReadInt32("unk672", index); // 672

            if (hasAnimKits) // 498
            {
                if (hasAnimKit1) // 492
                    packet.ReadUInt16("AnimKit1", index); // 492
                if (hasAnimKit3) // 496
                    packet.ReadUInt16("AnimKit3", index); // 496
                if (hasAnimKit2) // 494
                    packet.ReadUInt16("AnimKit2", index); // 494
            }

            if (hasUnk2) // 810
                packet.ReadWoWString("Str", Unk2Count, index);

            if (hasUnk6) // 476
                packet.ReadInt32("unk472", index); // 472

            if (hasUnk9) // 1064
                for (var i = 0; i < Unk9Count; i++) // 1048
                    packet.ReadInt32("unk1052", index, i); // 1052

            if (hasGobjectRotation) // 512
                packet.ReadPackedQuaternion("GameObject Rotation", index);

            if (hasUnk4) // 1044
                packet.ReadInt32("unk1040", index); // 1040

            if (hasLiving) // 368
                if (moveInfo.HasSplineData) // 344
                    if (bit336) // 336
                        if (splineType == SplineType.FacingTarget)
                        {
                            SplineFacingTargetGuid = packet.StartBitStream(4, 7, 0, 5, 1, 2, 3, 6);
                            packet.ParseBitStream(SplineFacingTargetGuid, 4, 2, 0, 5, 6, 3, 1, 7);
                            packet.WriteGuid("Spline Facing Target GUID", SplineFacingTargetGuid, index);
                        }

            /* Don't know why this is commented out. Check IDA
            if (hasLiving && hasSpline)
                Movement::PacketBuilder::WriteFacingTargetPart(*ToUnit()->movespline, *data);
            */

            return moveInfo;
        }
    }
}

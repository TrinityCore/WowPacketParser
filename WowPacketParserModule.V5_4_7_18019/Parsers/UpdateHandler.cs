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

namespace WowPacketParserModule.V5_4_7_18019.Parsers
{
    public static class UpdateHandler
    {
        [Parser(Opcode.CMSG_OBJECT_UPDATE_FAILED)]
        public static void HandleObjectUpdateFailed(Packet packet)
        {
            var guid = packet.StartBitStream(4, 6, 3, 0, 7, 5, 1, 2);
            packet.ParseBitStream(guid, 4, 7, 0, 6, 5, 2, 1, 3);

            packet.WriteGuid("Object Guid", guid);
        }

        [Parser(Opcode.SMSG_DESTROY_OBJECT)]
        public static void HandleDestroyObject(Packet packet)
        {
            var guid = new byte[8];

            guid[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();

            packet.ReadBit("Despawn Animation");

            guid[6] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            packet.ParseBitStream(guid, 2, 1, 5, 4, 3, 6, 7, 0);

            packet.WriteGuid("Object Guid", guid);
        }

        [HasSniffData] // in ReadCreateObjectBlock
        [Parser(Opcode.SMSG_UPDATE_OBJECT)]
        public static void HandleUpdateObject(Packet packet)
        {
            uint map = packet.ReadUInt16("Map");
            var count = packet.ReadUInt32("Count");

            for (var i = 0; i < count; i++)
            {
                var type = packet.ReadByte();
                var typeString = ((UpdateTypeCataclysm)type).ToString();

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
            var moves = ReadMovementUpdateBlock547(ref packet, guid, index);
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

        private static MovementInfo ReadMovementUpdateBlock547(ref Packet packet, Guid guid, int index)
        {
            var moveInfo = new MovementInfo();

            var hasUnkDword676 = packet.ReadBit();
            var hasVehicleData = packet.ReadBit("Has Vehicle Data", index);
            var hasUnkDword1044 = packet.ReadBit();
            var hasGameObjectRotation = packet.ReadBit("Has GameObject Rotation", index);
            packet.ReadBit("unk byte0", index);
            var living = packet.ReadBit("Living", index);
            var hasUnkLargeBlock = packet.ReadBit("Has Unk Large Block", index);
            packet.ReadBit("Unk Byte2", index);
            var hasUnkLargeBlock2 = packet.ReadBit("Has Unk Large Block2", index);
            packet.ReadBit("Self", index);
            packet.ReadBit("Unk Byte681", index);
            packet.ReadBit("Unk Byte1", index);
            var hasGameObjectPosition = packet.ReadBit("Has GameObject Position", index);
            var transport = packet.ReadBit("Has Transport Data", index);
            var hasAnimKits = packet.ReadBit("Has AnimKits", index);
            var hasStationaryPosition = packet.ReadBit("Has Stationary Position", index);
            var hasAttackingTarget = packet.ReadBit("Has Attacking Target", index);
            var unkLoopCounter = packet.ReadBits("Unknown array size", 22, index);
            var hasUnkString = packet.ReadBit("Has Unknown String", index);
            var hasTransportFrames = packet.ReadBit("Has Transport Frames", index);

            uint transportFramesCount = 0;
            uint UnkStringLen = 0;
            //int IsLivingUnkCountLoop = 0;
            uint IsLivingUnkCounter = 0;
            var hasOrientation = false;
            var guid2 = new byte[8];
            var hasPitch = false;
            var hasFallData = false;
            var hasSplineElevation = false;
            var hasTransportData = false;
            var hasTimestamp = false;
            var hasMovementCounter = false;
            var transportGuid = new byte[8];
            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var bit216 = false;
            var hasSplineStartTime = false;
            var splineCount = 0u;
            var splineType = SplineType.Stop;
            var facingTargetGuid = new byte[8];
            var hasSplineVerticalAcceleration = false;
            var hasSplineUnkPart = false;
            var hasFallDirection = false;
            var goTransportGuid = new byte[8];
            var hasGOTransportTime2 = false;
            var hasGOTransportTime3 = false;
            var attackingTargetGuid = new byte[8];
            var SplineFacingTargetGuid = new byte[8];
            var hasAnimKit1 = false;
            var hasAnimKit2 = false;
            var hasAnimKit3 = false;
            bool[] UnkLargeBlockBits = new bool[13];
            uint[] UnkLargeBlockCount = new uint[3];

            if(hasTransportFrames)
            {
                transportFramesCount = packet.ReadBits("Transport Frames Count",22,index);
            }

            if (hasGameObjectPosition)
            {
                goTransportGuid[3] = packet.ReadBit();
                goTransportGuid[5] = packet.ReadBit();
                goTransportGuid[2] = packet.ReadBit();
                goTransportGuid[1] = packet.ReadBit();
                goTransportGuid[4] = packet.ReadBit();
                hasGOTransportTime3 = packet.ReadBit();
                hasGOTransportTime2 = packet.ReadBit();
                goTransportGuid[0] = packet.ReadBit();
                goTransportGuid[6] = packet.ReadBit();
                goTransportGuid[7] = packet.ReadBit();
            }

            if (living)
            {
                hasTransportData = packet.ReadBit("Has Transport Data", index);

                if (hasTransportData)
                {
                    transportGuid[4] = packet.ReadBit();
                    transportGuid[0] = packet.ReadBit();
                    transportGuid[5] = packet.ReadBit();
                    transportGuid[2] = packet.ReadBit();
                    transportGuid[3] = packet.ReadBit();
                    hasTransportTime2 = packet.ReadBit();
                    transportGuid[7] = packet.ReadBit();
                    transportGuid[6] = packet.ReadBit();
                    transportGuid[1] = packet.ReadBit();
                    hasTransportTime3 = packet.ReadBit();
                }

                hasPitch = !packet.ReadBit("Lacks pitch", index);
                packet.ReadBit("Has MovementInfo spline", index);
                packet.ReadBits("IsLicingUnkLoop", 19, index);
                guid2[1] = packet.ReadBit();
                var hasExtraMovementFlags = !packet.ReadBit();

                /*var v37 = IsLivingUnkCountLoop == 0;

                if (!v37)
                {
                    for (int i = 0; i < IsLivingUnkCountLoop; ++i)
                    {
                        packet.ReadBits("Unk DWORD24 Loop1", 2, index, (int)i);
                    }
                }*/

                packet.ReadBit("Unk Bit from movementInfo", index);
                hasSplineElevation = !packet.ReadBit("Lacks spline elevation", index);

                if (hasExtraMovementFlags)
                    moveInfo.FlagsExtra = packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13, index);

                hasOrientation = !packet.ReadBit("Lacks orientation", index);
                hasTimestamp = !packet.ReadBit("Has Timestamp", index);
                var hasMovementFlags = !packet.ReadBit();
                hasMovementCounter = !packet.ReadBit("Has Movement Counter", index);
                
                guid2[2] = packet.ReadBit();
                guid2[6] = packet.ReadBit();
                hasFallData = packet.ReadBit("Has Fall Data", index);
                guid2[5] = packet.ReadBit();
                guid2[4] = packet.ReadBit();
                guid2[0] = packet.ReadBit();

                if (hasMovementFlags)
                    moveInfo.Flags = (WowPacketParser.Enums.MovementFlag)packet.ReadEnum<MovementFlag>("Movement Flags", 30, index);

                packet.ReadBit("Unk byte164", index);

                if (hasFallData)
                    hasFallDirection = packet.ReadBit("Has Fall Direction", index);

                packet.ReadBits("Is Living Unk Counter", 22, index);
                guid2[7] = packet.ReadBit();
                moveInfo.HasSplineData = packet.ReadBit("Has Spline Data", index);
                guid2[3] = packet.ReadBit();
                
                if (moveInfo.HasSplineData)
                {
                    bit216 = packet.ReadBit("Has extended spline data", index);
                    if (bit216)
                    {
                        packet.ReadBits("Unk bits", 2, index);
                        hasSplineStartTime = packet.ReadBit("Has spline start time", index);
                        splineCount = packet.ReadBits("Spline Waypoints", 22, index);
                        packet.ReadEnum<SplineFlag434>("Spline flags", 25, index);
                        hasSplineVerticalAcceleration = packet.ReadBit("Has spline vertical acceleration", index);

                        hasSplineUnkPart = packet.ReadBit();

                        if(hasSplineUnkPart)
                        {
                            packet.ReadBits("Unk word300", 2, index);                            //unk word300
                            packet.ReadBits("Unk dword284", 21, index);                          //unk dword284
                        }
                    }
                }
            }

            if (hasUnkLargeBlock2)
            {
                UnkLargeBlockBits[0] = packet.ReadBit();
                UnkLargeBlockBits[1] = packet.ReadBit();
                packet.ReadBit("Unk Byte256", index);
                packet.ReadBit("Unk Byte257", index);
                UnkLargeBlockBits[4] = packet.ReadBit();
                packet.ReadBit("Unk Byte254", index);
                UnkLargeBlockBits[6] = packet.ReadBit();
                packet.ReadBit("Unk Byte255", index);
                UnkLargeBlockBits[8] = packet.ReadBit();

                if (UnkLargeBlockBits[8])
                {
                    UnkLargeBlockCount[0] = packet.ReadBits(21);
                    UnkLargeBlockCount[1] = packet.ReadBits(21);
                }

                UnkLargeBlockBits[9] = packet.ReadBit();

                if (UnkLargeBlockBits[9])
                {
                    UnkLargeBlockCount[2] = packet.ReadBits(20);
                }

                UnkLargeBlockBits[10] = packet.ReadBit();
                packet.ReadBit("Unk Byte258", index);
                UnkLargeBlockBits[12] = packet.ReadBit();
            }

            if (hasAttackingTarget)
                attackingTargetGuid = packet.StartBitStream(4, 6, 3, 5, 0, 2, 7, 1);

            if (hasAnimKits)
            {
                hasAnimKit2 = !packet.ReadBit();
                hasAnimKit3 = !packet.ReadBit();
                hasAnimKit1 = !packet.ReadBit();
            }

            if (hasUnkString)
                UnkStringLen = packet.ReadBits(7);

            packet.ResetBitReader();

            // Reading data
            for (var i = 0u; i < unkLoopCounter; ++i)
                packet.ReadUInt32("Unk UInt32", index, (int)i);

            if (hasUnkLargeBlock2)
            {
                if (UnkLargeBlockBits[10])
                {
                    packet.ReadSingle("Unk Float 234", index);
                    packet.ReadSingle("Unk Float 238", index);
                }

                if (UnkLargeBlockBits[8])
                {
                    for(uint i = 0; i < UnkLargeBlockCount[1]; ++i)
                    {
                        packet.ReadSingle("Unk Float Loop1", index);
                        packet.ReadSingle("Unk Float1 Loop1", index);
                    }

                    packet.ReadSingle("Unk Float 27C", index);

                    for (uint i = 0; i < UnkLargeBlockCount[0]; ++i)
                    {
                        packet.ReadSingle("Unk Float Loop0", index);
                        packet.ReadSingle("Unk Float1 Loop0", index);
                    }

                    packet.ReadSingle("Unk Float 280", index);
                }

                if (UnkLargeBlockBits[1])
                {
                    packet.ReadSingle("Unk Float 244", index);
                    packet.ReadSingle("Unk Float 250", index);
                    packet.ReadSingle("Unk Float 254", index);
                    packet.ReadSingle("Unk Float 248", index);
                    packet.ReadSingle("Unk Float 240", index);
                    packet.ReadSingle("Unk Float 24C", index);
                }

                packet.ReadInt32("Unk DWORD 520", index);

                if (UnkLargeBlockBits[9])
                {
                    for (uint i = 0; i < UnkLargeBlockCount[2]; ++i)
                    {
                        packet.ReadSingle("Unk Float Loop2", index);
                        packet.ReadSingle("Unk Float1 Loop2", index);
                        packet.ReadSingle("Unk Float2 Loop2", index);
                    }
                }

                if (UnkLargeBlockBits[12])
                    packet.ReadInt32("Unk DWORD 540", index);

                if(UnkLargeBlockBits[6])
                    packet.ReadInt32("Unk DWORD 532", index);

                if (UnkLargeBlockBits[0])
                    packet.ReadInt32("Unk DWORD 556", index);

                if (UnkLargeBlockBits[4])
                    packet.ReadInt32("Unk DWORD 548", index);
            }

            if (living)
            {
                if (hasFallData)
                {
                    if (hasFallDirection)
                    {
                        packet.ReadSingle("Jump Sin", index);
                        packet.ReadSingle("Jump Cos", index);
                        packet.ReadSingle("Jump XY Speed", index);
                    }
                    packet.ReadSingle("Fall Z Speed", index);
                    packet.ReadUInt32("Time Fallen", index);
                }

                if (moveInfo.HasSplineData)
                {
                    if (bit216)
                    {
                        packet.ReadSingle("Spline Duration Multiplier Next", index);

                        for (var i = 0u; i < splineCount; ++i)
                        {
                            var wp = new Vector3
                            {
                                Z = packet.ReadSingle(),
                                X = packet.ReadSingle(),
                                Y = packet.ReadSingle(),
                            };

                            packet.WriteLine("[{0}][{1}] Spline Waypoint: {2}", index, i, wp);
                        }

                        splineType = packet.ReadEnum<SplineType>("Spline Type",TypeCode.Byte, index);

                        packet.ReadSingle("Spline Duration Multiplier", index);

                        //unk part goes here

                        if (splineType == SplineType.FacingSpot)
                        {
                            var point = new Vector3
                            {
                                X = packet.ReadSingle(),
                                Z = packet.ReadSingle(),
                                Y = packet.ReadSingle(),
                            };

                            packet.WriteLine("[{0}] Facing Spot: {1}", index, point);
                        }

                        if (hasSplineVerticalAcceleration)
                            packet.ReadSingle("Spline Vertical Acceleration", index);

                        if (splineType == SplineType.FacingAngle)
                            packet.ReadSingle("Facing Angle", index);
                        
                        packet.ReadUInt32("Spline Full Time", index);

                        if (hasSplineStartTime)
                            packet.ReadUInt32("Spline Start time", index);

                        packet.ReadUInt32("Spline Time", index);
                    }

                    packet.ReadUInt32("Spline Id", index);

                    var endPoint = new Vector3
                    {
                        Z = packet.ReadSingle(),
                        X = packet.ReadSingle(),
                        Y = packet.ReadSingle(),
                    };

                    
                    packet.WriteLine("[{0}] Spline Endpoint: {1}", index, endPoint);
                }

                moveInfo.Position.Z = packet.ReadSingle();

                /*if (IsLivingUnkCountLoop > 0)
                {
                    for (int i = 0; i < IsLivingUnkCountLoop; ++i)
                    {
                        packet.ReadSingle("Unk Float14", index, (int)i);
                        packet.ReadInt32("Unk DWORD10", index, (int)i);
                        packet.ReadInt32("Unk DWORD0", index, (int)i);
                        packet.ReadSingle("Unk FloatC", index, (int)i);
                        packet.ReadSingle("Unk Float4", index, (int)i);
                        packet.ReadSingle("Unk Float8", index, (int)i);
                    }
                }*/

                moveInfo.Position.Y = packet.ReadSingle();
                packet.ReadSingle("Fly Speed", index);
                packet.ReadXORByte(guid2, 6);
                packet.ReadSingle("FlyBack Speed", index);

                if (hasTransportData)
                {
                    packet.ReadXORByte(transportGuid, 7);
                    packet.ReadXORByte(transportGuid, 4);

                    if (hasTransportTime3)
                        packet.ReadUInt32("Transport Time 3", index);

                    packet.ReadUInt32("Transport Time", index);
                    
                    if (hasTransportTime2)
                        packet.ReadUInt32("Transport Time 2", index);

                    moveInfo.TransportOffset.O = packet.ReadSingle();
                    moveInfo.TransportOffset.X = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 6);
                    packet.ReadXORByte(transportGuid, 3);
                    packet.ReadXORByte(transportGuid, 2);
                    moveInfo.TransportOffset.Z = packet.ReadSingle();
                    moveInfo.TransportOffset.Y = packet.ReadSingle();
                    packet.ReadSByte("Transport Seat", index);
                    packet.ReadXORByte(transportGuid, 1);
                    packet.ReadXORByte(transportGuid, 0);
                    packet.ReadXORByte(transportGuid, 5);
                    moveInfo.TransportGuid = new Guid(BitConverter.ToUInt64(transportGuid, 0));
                    packet.WriteLine("[{0}] Transport GUID {1}", index, moveInfo.TransportGuid);
                    packet.WriteLine("[{0}] Transport Position: {1}", index, moveInfo.TransportOffset);
                }
                
                moveInfo.Position.X = packet.ReadSingle();
                packet.ReadXORByte(guid2, 2);

                if (hasPitch)
                    packet.ReadSingle("Pitch", index);

                packet.ReadSingle("Swim Speed", index);
                packet.ReadXORByte(guid2, 1);
                packet.ReadSingle("RunBack Speed", index);
                packet.ReadSingle("SwimBack Speed", index);
                packet.ReadXORByte(guid2, 5);

                if (hasSplineElevation)
                    packet.ReadSingle("Spline Elevation", index);

                if (hasMovementCounter)
                    packet.ReadUInt32("Movement Counter", index);

                moveInfo.RunSpeed = packet.ReadSingle("Run Speed", index) / 7.0f;
                packet.ReadXORByte(guid2, 7);
                moveInfo.WalkSpeed = packet.ReadSingle("Walk Speed", index) / 2.5f;
                packet.ReadSingle("Pitch Rate", index);

                if (IsLivingUnkCounter > 0)
                {
                    for (uint i = 0; i < IsLivingUnkCounter; ++i)
                    {
                        packet.ReadUInt32("Unk DWORD148", index, (int)i);
                    }
                }

                if(hasTimestamp)
                    packet.ReadUInt32("Timestamp", index);

                packet.ReadXORByte(guid2, 4);
                packet.ReadXORByte(guid2, 0);
                
                if (hasOrientation)
                    moveInfo.Orientation = packet.ReadSingle();

                packet.WriteLine("[{0}] GUID 2: {1}", index, new Guid(BitConverter.ToUInt64(guid2, 0)));
                packet.WriteLine("[{0}] Position: {1}", index, moveInfo.Position);
                packet.WriteLine("[{0}] Orientation: {1}", index, moveInfo.Orientation);
            }

            if (hasAttackingTarget)
            {
                packet.ParseBitStream(attackingTargetGuid, 5, 1, 2, 0, 3, 4, 6, 7);
                packet.WriteGuid("Attacking Target GUID", attackingTargetGuid, index);
            }

            if (hasStationaryPosition)
            {
                moveInfo.Orientation = packet.ReadSingle("Stationary Orientation", index);
                moveInfo.Position = packet.ReadVector3("Stationary Position", index);
            }

            if (hasGameObjectPosition)
            {
                packet.ReadByte("GO Transport Seat", index);
                moveInfo.TransportOffset.X = packet.ReadSingle();
                packet.ReadXORByte(goTransportGuid, 1);
                packet.ReadXORByte(goTransportGuid, 0);
                packet.ReadXORByte(goTransportGuid, 2);
                packet.ReadXORByte(goTransportGuid, 6);
                packet.ReadXORByte(goTransportGuid, 5);
                packet.ReadXORByte(goTransportGuid, 4);

                if (hasGOTransportTime3)
                    packet.ReadUInt32("GO Transport Time 3", index);

                packet.ReadXORByte(goTransportGuid, 7);
                moveInfo.TransportOffset.O = packet.ReadSingle();
                moveInfo.TransportOffset.Z = packet.ReadSingle();
                moveInfo.TransportOffset.Y = packet.ReadSingle();

                if (hasGOTransportTime2)
                    packet.ReadUInt32("GO Transport Time 2", index);
                
                packet.ReadXORByte(goTransportGuid, 3);
                
                packet.ReadUInt32("GO Transport Time", index);

                moveInfo.TransportGuid = new Guid(BitConverter.ToUInt64(goTransportGuid, 0));
                packet.WriteLine("[{0}] GO Transport GUID {1}", index, moveInfo.TransportGuid);
                packet.WriteLine("[{0}] GO Transport Position: {1}", index, moveInfo.TransportOffset);
            }

            if (transport)
                packet.ReadUInt32("Transport path timer", index);

            if (hasUnkDword676)
                packet.ReadUInt32("Unk DWORD676", index);

            if (hasUnkString)
                packet.ReadWoWString("Unk String", UnkStringLen, index);

            if (hasGameObjectRotation)
                packet.ReadPackedQuaternion("GameObject Rotation", index);

            if (hasVehicleData)
            {
                moveInfo.VehicleId = packet.ReadUInt32("Vehicle Id", index);
                packet.ReadSingle("Vehicle Orientation", index);              
            }

            if (hasUnkDword1044)
                packet.ReadUInt32("Unk DWORD1044", index);

            if (hasAnimKits)
            {
                if (hasAnimKit1)
                    packet.ReadUInt16("Anim Kit 1", index);
                if (hasAnimKit2)
                    packet.ReadUInt16("Anim Kit 2", index);
                if (hasAnimKit3)
                    packet.ReadUInt16("Anim Kit 3", index);
            }  

            /*if (bit456)
            {
                // float[] arr = new float[16];
                // ordering: 13, 4, 7, 15, BYTE, 10, 11, 3, 5, 14, 6, 1, 8, 12, 0, 2, 9
                packet.ReadBytes(4 * 16 + 1);
            }*/

            if(living && moveInfo.HasSplineData && bit216)
            {
                if (splineType == SplineType.FacingTarget)
                {
                    SplineFacingTargetGuid = packet.StartBitStream(6, 7, 3, 0, 5, 1, 4, 2);
                    packet.ParseBitStream(SplineFacingTargetGuid, 4, 2, 5, 6, 0, 7, 1, 3);
                    packet.WriteGuid("Spline Facing Target GUID", SplineFacingTargetGuid, index);
                }
            }

            return moveInfo;
        }
    }
}

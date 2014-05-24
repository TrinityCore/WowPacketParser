using System;
using System.Collections;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using Guid = WowPacketParser.Misc.Guid;
using UpdateFields = WowPacketParser.Enums.Version.UpdateFields;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class UpdateHandler
    {
	/*
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
                    case "CreateObject2": // Might != CreateObject1 on Cata
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
            var moves = ReadMovementUpdateBlock(ref packet, guid, index);
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

        private static MovementInfo ReadMovementUpdateBlock(ref Packet packet, Guid guid, int index)
        {
            var moveInfo = new MovementInfo();

            var guid1 = new byte[8];
            var transportGuid = new byte[8];
            var goTransportGuid = new byte[8];
            var attackingTargetGuid = new byte[8];

            var hasFallData = false;
            var hasFallDirection = false;
            var hasFullSpline = false;
            var hasGOTransportTime2 = false;
            var hasGOTransportTime3 = false;
            var hasMovementFlags = false;
            var hasMoveFlagsExtra = false;
            var hasOrientation = false;
            var hasParabolicAndNotEnded = false;
            var hasParabolicOrAnimation = false;
            var hasPitch = false;
            var hasSplineElevation = false;
            var hasTransportData = false;
            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasTimestamp = false;
            var hasUnkSpline = false;
            
            var hasDWordA0 = false;

            var byte8C = false;
            var byteA4 = false;
            var byte8D = false;

            var splineCount = 0u;
            var Unk19BitsCounter = 0u;
            var UnkSpline_21BitsCounter = 0u;
            var Unk22BitsCounter = 0u;

            var splineType = SplineType.Stop;

            var hasVehicleData = packet.ReadBit("Has Vehicle Data", index);
            var isSelf = packet.ReadBit("Self", index);

            packet.ReadBit(); // fake bit

            var transport = packet.ReadBit("Transport", index);

            packet.ReadBit(); // fake bit
            var byte2A9 = packet.ReadBit("byte2A9", index);

            var hasAttackingTarget = packet.ReadBit("Has Attacking Target", index);
            var hasStationaryPosition = packet.ReadBit("Has Stationary Position", index);

            var isAreaTrigger = packet.ReadBit("Area Trigger", index);

            var byte428 = packet.ReadBit("byte428", index);            
            
            var isSceneObject = packet.ReadBit("Scene Object", index);

            packet.ReadBit(); // fake bit
            var byte32A = packet.ReadBit("byte32A", index);

            var transportFrames = packet.ReadBits(22);
            var living = packet.ReadBit("Living", index);
            var hasGameObjectPosition = packet.ReadBit("Has GameObject Position", index);

            packet.ReadBit(); // fake bit

            var hasGameObjectRotation = packet.ReadBit("Has GameObject Rotation", index);

            var byte2A4 = packet.ReadBit("byte2A4", index);
            var byte414 = packet.ReadBit("byte414", index); 

            var hasAnimKits = packet.ReadBit("Has AnimKits", index);

            if (living)
            {
                // need update
                guid1[5] = packet.ReadBit();
                byte8D = packet.ReadBit();
                hasSplineElevation = !packet.ReadBit();
                guid1[6] = packet.ReadBit();
                byteA4 = packet.ReadBit();
                Unk19BitsCounter = packet.ReadBits(19);

                if (Unk19BitsCounter != 0)
                    for (var i = 0; i < Unk19BitsCounter; ++i)
                        packet.ReadBits("Unk19BitsCounter", 2, index);

                guid1[4] = packet.ReadBit();
                hasOrientation = !packet.ReadBit();
                hasMoveFlagsExtra = !packet.ReadBit();
                hasDWordA0 = !packet.ReadBit();
                guid1[2] = packet.ReadBit();
                guid1[3] = packet.ReadBit();
                guid1[7] = packet.ReadBit();
                Unk22BitsCounter = packet.ReadBits(22);
                hasMovementFlags = !packet.ReadBit();
                hasTimestamp = !packet.ReadBit("Lacks timestamp", index);
                hasPitch = !packet.ReadBit("Lacks pitch", index);
                guid1[1] = packet.ReadBit();
                hasFallData = packet.ReadBit("Has Fall Data", index);
                byte8C = packet.ReadBit();
                
                if (hasMoveFlagsExtra)
                    moveInfo.FlagsExtra = packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13, index);
                
                guid1[0] = packet.ReadBit();
                moveInfo.HasSplineData = packet.ReadBit("Has spline data", index);
                if (moveInfo.HasSplineData)
                    hasFullSpline = packet.ReadBit();
                    if (hasFullSpline)
                    {
                        packet.ReadEnum<SplineFlag542>("Spline flags", 25, index);
                        hasParabolicOrAnimation = packet.ReadBit("Has Parabolic Or Animation", index);
                        splineCount = packet.ReadBits("SplineWaypointsCount", 20, index);
                        hasUnkSpline = packet.ReadBit();
                        if (hasFullSpline)
                        {
                            UnkSpline_21BitsCounter = packet.ReadBits(21);
                            packet.ReadBits("UnkSpline_2BitsFlags", 2, index);
                        }
                        packet.ReadEnum<SplineMode>("Spline Mode", 2, index);
                        hasParabolicAndNotEnded = packet.ReadBit("Has Parabolic And Not Ended", index);
                    }
                   
                hasTransportData = packet.ReadBit("Has Transport Data", index);
                if (hasTransportData)
                {
                    packet.StartBitStream(transportGuid, 6, 1, 2, 5);
                    hasTransportTime3 = packet.ReadBit();
                    packet.StartBitStream(transportGuid, 4, 7, 0);
                    hasTransportTime2 = packet.ReadBit();
                    transportGuid[3] = packet.ReadBit();
                } 
                if (hasMovementFlags)
                    moveInfo.Flags = packet.ReadEnum<MovementFlag>("Movement Flags", 30, index);                
                if (hasFallData)
                    hasFallDirection = packet.ReadBit("Has Fall Direction", index);
            }

            if (hasGameObjectPosition)
            {
                hasGOTransportTime3 = packet.ReadBit();
                hasGOTransportTime2 = packet.ReadBit();
                packet.StartBitStream(goTransportGuid, 4, 2, 7, 6, 3, 0, 1, 5);
            }

            if (hasAnimKits)
            {
                // need update
            }

            if (hasAttackingTarget)
            {
                attackingTargetGuid = packet.StartBitStream(4, 0, 6, 2, 1, 5, 3, 7);
            }

            if (isSceneObject)
            {
                // need update
            }

            // DWORD 418
            uint dword418 = 0;
            if (byte428)
            {
                dword418 = packet.ReadBits(22);
            }

            // DWORD 2AA
            uint dword2AA = 0;
            if (byte32A)
            {
                dword2AA = packet.ReadBits(7);
            }

            packet.ResetBitReader();

            if (living)
            {
                if (hasTransportData)
                {
                    packet.ReadXORByte(transportGuid, 0);
                    packet.ReadXORByte(transportGuid, 5);
                    moveInfo.TransportOffset.Z = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 1);
                    moveInfo.TransportOffset.Y = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 2);
                    packet.ReadUInt32("Transport Time", index);
                    moveInfo.TransportOffset.O = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 7);
                    
                    if (hasTransportTime2)
                        packet.ReadUInt32("Transport Time 2", index);
                        
                    packet.ReadXORByte(transportGuid, 6);
                    packet.ReadXORByte(transportGuid, 4);
                    moveInfo.TransportOffset.X = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 3);
                    packet.ReadByte("Transport Seat", index);
                    
                    if (hasTransportTime3)
                        packet.ReadUInt32("Transport Time 3", index);
                }
                if (moveInfo.HasSplineData)
                {
                    if (hasFullSpline)
                    {
                        if (hasUnkSpline)
                        {
                            for (var i = 0; i < UnkSpline_21BitsCounter; ++i)
                            {
                                packet.ReadSingle("UnkSplineFloat", index, i);
                                packet.ReadSingle("UnkSplineFloat2", index, i);
                            }
                        }
                        packet.ReadSingle("UnkFloat", index);

                        if (splineCount != 0)
                        {
                            for (var i = 0u; i < splineCount; ++i)
                            {
                                var wp = new Vector3
                                {
                                    Y = packet.ReadSingle(),
                                    Z = packet.ReadSingle(),
                                    X = packet.ReadSingle(),
                                };

                                packet.WriteLine("[{0}][{1}] Spline Waypoint: {2}", index, i, wp);
                            }
                        }

                        packet.ReadInt32("Spline Time", index);

                        var type = packet.ReadByte();
                        switch (type)
                        {
                            case 1:
                                splineType = SplineType.Normal;
                                break;
                            case 2:
                                splineType = SplineType.FacingSpot;
                                break;
                            case 3:
                                splineType = SplineType.FacingTarget;
                                break;
                            case 4:
                                splineType = SplineType.FacingAngle;
                                break;
                        }

                        if (splineType == SplineType.FacingAngle)
                            packet.ReadSingle("Facing Angle", index);

                        packet.ReadSingle();

                        if (hasParabolicOrAnimation)
                            packet.ReadInt32("Spline Start Time", index);

                        if (hasParabolicAndNotEnded)
                            packet.ReadSingle("Spline Vertical Acceleration", index);

                        if (splineType == SplineType.FacingSpot)
                        {
                            var point = new Vector3
                            {
                                Y = packet.ReadSingle(),
                                Z = packet.ReadSingle(),
                                X = packet.ReadSingle(),
                            };

                            packet.WriteLine("[{0}] Facing Spot: {1}", index, point);
                        }

                        packet.ReadInt32("Spline Full Time", index);
                    }

                    packet.ReadInt32("Spline Id", index);
                    packet.ReadSingle();
                    packet.ReadSingle();
                    packet.ReadSingle();
                }

                moveInfo.Position.Y = packet.ReadSingle();
                packet.ReadXORByte(guid1, 7);

                for (var i = 0; i < Unk19BitsCounter; ++i)
                {
                    packet.ReadSingle();
                    packet.ReadInt32();
                    packet.ReadInt32();
                    packet.ReadSingle();
                    packet.ReadSingle();
                    packet.ReadSingle();
                }

                if (hasFallData)
                {
                    packet.ReadInt32();
                    if (hasFallDirection)
                    {
                        packet.ReadSingle();
                        packet.ReadSingle();
                        packet.ReadSingle();
                    }

                    packet.ReadSingle();
                }

                if (hasPitch)
                    packet.ReadSingle("Pitch", index);

                if (hasDWordA0)
                    packet.ReadInt32();

                packet.ReadXORByte(guid1, 1);
                packet.ReadSingle("Turn Speed", index);
                packet.ReadSingle("RunBack Speed", index);
                moveInfo.WalkSpeed = packet.ReadSingle("Walk Speed", index) / 2.5f;

                if (hasTimestamp)
                {
                    packet.ReadUInt32("Time", index);
                }

                moveInfo.Position.X = packet.ReadSingle();
                packet.ReadXORByte(guid1, 2);
                packet.ReadSingle("Swim Speed", index);
                packet.ReadSingle("FlyBack Speed", index);

                if (hasOrientation)
                {
                    moveInfo.Orientation = packet.ReadSingle();
                }

                packet.ReadSingle("Fly Speed", index);
                packet.ReadXORByte(guid1, 6);

                if (hasSplineElevation)
                {
                    packet.ReadSingle("Spline Elevation", index);
                }

                moveInfo.RunSpeed = packet.ReadSingle("Run Speed", index) / 7.0f;
                packet.ReadSingle("Pitch Speed", index);
                packet.ReadXORByte(guid1, 0);
                packet.ReadXORByte(guid1, 5);

                for (var i = 0; i < Unk22BitsCounter; ++i)
                {
                    packet.ReadInt32();
                }

                packet.ReadXORByte(guid1, 4);
                packet.ReadSingle("Swim Back Speed", index);
                moveInfo.Position.Z = packet.ReadSingle();
                packet.ReadXORByte(guid1, 3);
            }
            

            if (isSceneObject)
            {
                // need update
            }

            if (hasGameObjectPosition)
            {
                packet.ReadSByte("GO Transport Seat", index);
                
                if (hasGOTransportTime2)
                    packet.ReadUInt32("GO Transport Time 2", index);
                
                packet.ParseBitStream(goTransportGuid, 4, 3);
                
                if (hasGOTransportTime2)
                    packet.ReadUInt32("GO Transport Time 3", index);
                    
                packet.ParseBitStream(goTransportGuid, 7, 6, 5, 0);
                moveInfo.TransportOffset.Z = packet.ReadSingle();
                moveInfo.TransportOffset.X = packet.ReadSingle();
                packet.ReadUInt32("GO Transport Time", index);
                moveInfo.TransportOffset.O = packet.ReadSingle();
                packet.ReadXORByte(goTransportGuid, 1);
                moveInfo.TransportOffset.Y = packet.ReadSingle();
                packet.ReadXORByte(goTransportGuid, 2);
                
                moveInfo.TransportGuid = new Guid(BitConverter.ToUInt64(goTransportGuid, 0));
                packet.WriteLine("[{0}] GO Transport GUID {1}", index, moveInfo.TransportGuid);
                packet.WriteLine("[{0}] GO Transport Position: {1}", index, moveInfo.TransportOffset);
            }

            if (hasVehicleData)
            {
                // need update
            }

            if (hasAttackingTarget)
            {
                packet.ParseBitStream(attackingTargetGuid, 1, 3, 5, 4, 7, 6, 2, 0);
                packet.WriteGuid("Attacking GUID", attackingTargetGuid, index);
            }

            if (hasGameObjectRotation)
            {
                packet.ReadPackedQuaternion("GameObject Rotation", index);
            }

            if (hasAnimKits)
            {
                // need update
            }

            if (hasStationaryPosition)
            {
                moveInfo.Position.X = packet.ReadSingle();
                moveInfo.Position.Z = packet.ReadSingle();
                moveInfo.Position.Y = packet.ReadSingle();
                moveInfo.Orientation = packet.ReadSingle("Stationary Orientation", index);
                packet.WriteLine("[{0}] Stationary Position: {1}", index, moveInfo.Position);
            }

            if (transport)
            {
                // need update
            }

            if (living && moveInfo.HasSplineData && hasFullSpline && splineType == SplineType.FacingTarget)
            {
                var facingTargetGuid = new byte[8];
                facingTargetGuid = packet.StartBitStream(5, 0, 2, 4, 1, 3, 6, 7);
                packet.ParseBitStream(facingTargetGuid, 5, 0, 4, 6, 3, 2, 1, 7);
                packet.WriteGuid("Facing Target GUID", facingTargetGuid, index);
            }

            if (byte32A)
                packet.ReadString();

            if (byte414)
                packet.ReadInt32();

            if (byte2A4)
                packet.ReadInt32();

            if (byte428)
            {
                for (uint l_I = 0; l_I < dword418; l_I++)
                    packet.ReadInt32();
            }

            return moveInfo;
        }

        [Parser(Opcode.SMSG_DESTROY_OBJECT)]
        public static void HandleDestroyObject(Packet packet)
        {
            // need update
        }*/
    }
}

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

            var bit160 = false;
            var bit528 = false;
            var bit600 = false;
            var bit544 = false;
            var bit526 = false;
            var bit552 = false;
            var bit524 = false;
            var bit572 = false;
            var bit525 = false;
            var bit664 = false;
            var bit527 = false;
            var bit536 = false;
            var bit644 = false;
            var bit560 = false;

            var hasAnimKit1 = false;
            var hasAnimKit2 = false;
            var hasAnimKit3 = false;

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
            var bits25C = 0u;
            var bits26C = 0u;
            var bits288 = 0u;
            var bits418 = 0u;
            var bits2AA = 0u;

            var splineType = SplineType.Stop;



            var byte2A4 = packet.ReadBit("byte2A4", index);
            var hasAnimKits = packet.ReadBit("Has AnimKits", index);
            var living = packet.ReadBit("Living", index);

            var byte32A = packet.ReadBit("byte32A", index);

            packet.ReadBit(); // fake bit 2

            var transportFrames = packet.ReadBits(22);

            var hasVehicleData = packet.ReadBit("Has Vehicle Data", index);

            var byte414 = packet.ReadBit("byte414", index);

            packet.ReadBit(); // fake bit 1

            var transport = packet.ReadBit("Transport", index);

            var hasGameObjectRotation = packet.ReadBit("Has GameObject Rotation", index);

            packet.ReadBit(); // fake bit 3

            var isSelf = packet.ReadBit("Self", index);

            var hasAttackingTarget = packet.ReadBit("Has Attacking Target", index);

            var isSceneObject = packet.ReadBit("Scene Object", index);

            var byte428 = packet.ReadBit("byte428", index);

            packet.ReadBit(); // fake bit 0

            var isAreaTrigger = packet.ReadBit("Area Trigger", index);

            var hasGOTransportPosition = packet.ReadBit("Has GO Transport Position", index);

            var byte2A9 = packet.ReadBit("byte2A9", index);

            var hasStationaryPosition = packet.ReadBit("Has Stationary Position", index);

            if (living)
            {
                guid1[2] = packet.ReadBit();
                byte8C = packet.ReadBit("byte8C");
                hasPitch = !packet.ReadBit("Lacks pitch", index);
                hasTransportData = packet.ReadBit("Has Transport Data", index);
                byteA4 = packet.ReadBit("byteA4");

                if (hasTransportData)
                {
                    packet.StartBitStream(transportGuid, 4, 2);
                    hasTransportTime3 = packet.ReadBit("TransportTime3");
                    packet.StartBitStream(transportGuid, 0, 1, 3, 6, 7);
                    hasTransportTime2 = packet.ReadBit("TransportTime2");
                    transportGuid[5] = packet.ReadBit();
                }

                hasTimestamp = !packet.ReadBit("Lacks timestamp", index);
                guid1[6] = packet.ReadBit();
                guid1[4] = packet.ReadBit();
                guid1[3] = packet.ReadBit();

                hasOrientation = !packet.ReadBit("Has Orientation");

                hasDWordA0 = !packet.ReadBit("Has DWordA0");
                guid1[5] = packet.ReadBit();

                Unk22BitsCounter = packet.ReadBits(22);

                hasMovementFlags = !packet.ReadBit("Has MovementFlags");

                Unk19BitsCounter = packet.ReadBits(19);
                /*
                if (Unk19BitsCounter != 0)
                    for (var i = 0; i < Unk19BitsCounter; ++i)
                        packet.ReadBits("Unk19BitsCounter", 2, index);
                */
                hasFallData = packet.ReadBit("Has Fall Data", index);

                if (hasMovementFlags)
                    moveInfo.Flags = packet.ReadEnum<MovementFlag>("Movement Flags", 30, index);

                hasSplineElevation = !packet.ReadBit();
                moveInfo.HasSplineData = packet.ReadBit("Has spline data", index);
                byte8D = packet.ReadBit("byte8D");
                guid1[0] = packet.ReadBit();
                guid1[7] = packet.ReadBit();
                guid1[1] = packet.ReadBit();

                if (moveInfo.HasSplineData)
                {
                    hasFullSpline = packet.ReadBit("Has FullSpline");

                    if (hasFullSpline)
                    {
                        hasParabolicOrAnimation = packet.ReadBit("Has Parabolic Or Animation", index);
                        hasParabolicAndNotEnded = packet.ReadBit("Has Parabolic And Not Ended", index);
                        hasUnkSpline = packet.ReadBit("Has UnkSpline");
                        splineCount = packet.ReadBits("SplineWaypointsCount", 20, index);
                        packet.ReadEnum<SplineMode>("Spline Mode", 2, index);
                        packet.ReadEnum<SplineFlag542>("Spline flags", 25, index);

                        if (hasUnkSpline)
                        {
                            UnkSpline_21BitsCounter = packet.ReadBits(21);
                            packet.ReadBits("UnkSpline_2BitsFlags", 2, index);
                        }
                    }
                }

                hasMoveFlagsExtra = !packet.ReadBit("Has MovementFlagsExtra");

                if (hasFallData)
                    hasFallDirection = packet.ReadBit("Has Fall Direction", index);

                if (hasMoveFlagsExtra)
                    moveInfo.FlagsExtra = packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13, index);
            }

            if (isSceneObject)
            {
                // need update
            }
			
            if (hasGOTransportPosition)
            {
                packet.StartBitStream(goTransportGuid, 4, 1, 0);
                hasGOTransportTime2 = packet.ReadBit();
                packet.StartBitStream(goTransportGuid, 6, 5, 3, 2, 7);
                hasGOTransportTime3 = packet.ReadBit();
            }

             if (isAreaTrigger)
            {
                bit528 = packet.ReadBit();
                bit600 = packet.ReadBit();
                bit544 = packet.ReadBit();
                bit526 = packet.ReadBit();
                bit552 = packet.ReadBit();
                bit524 = packet.ReadBit();
                bit572 = packet.ReadBit();
                bit525 = packet.ReadBit();
                bit664 = packet.ReadBit();
                bit527 = packet.ReadBit();
                 
                if (bit664)
					bits288 = packet.ReadBits(20);
                 
                bit536 = packet.ReadBit();
				bit644 = packet.ReadBit();
				bit560 = packet.ReadBit();
                 
                if (bit664)
                {
					bits25C = packet.ReadBits(21);
					bits26C = packet.ReadBits(21);
				}
			}
            
            if (hasAnimKits)
            {
                hasAnimKit2 = !packet.ReadBit();
                hasAnimKit3 = !packet.ReadBit();
                hasAnimKit1 = !packet.ReadBit();
            }

            if (hasAttackingTarget)
            {
                attackingTargetGuid = packet.StartBitStream(4, 6, 5, 2, 0, 1, 3, 7);
            }

            if (byte428)
                bits418 = packet.ReadBits(22);
            
            if (byte32A)
                bits2AA = packet.ReadBits(7);
            
            packet.ResetBitReader();
            
            for (var i = 0; i < transportFrames; ++i)
                packet.ReadInt32("Transport frame", index, i);
                
            if (living)
            {
                if (hasTransportData)
                {
                    packet.ReadXORByte(transportGuid, 7);
                    moveInfo.TransportOffset.X = packet.ReadSingle();

                    if (hasTransportTime3)
                        packet.ReadUInt32("Transport Time 3", index);

                    moveInfo.TransportOffset.O = packet.ReadSingle();
                    moveInfo.TransportOffset.Y = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 4);
                    packet.ReadXORByte(transportGuid, 1);
                    packet.ReadXORByte(transportGuid, 3);
                    moveInfo.TransportOffset.Z = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 5);

                    if (hasTransportTime2)
                        packet.ReadUInt32("Transport Time 2", index);

                    packet.ReadXORByte(transportGuid, 0);
                    packet.ReadByte("Transport Seat", index);
                    packet.ReadXORByte(transportGuid, 6);
                    packet.ReadXORByte(transportGuid, 2);
                    packet.ReadUInt32("Transport Time", index);
                }

                packet.ReadXORByte(guid1, 4);
                /*
                for (var i = 0; i < Unk19BitsCounter; ++i)
                {
                    packet.ReadSingle();
                    packet.ReadSingle();
                    packet.ReadSingle();
                    packet.ReadInt32();
                    packet.ReadInt32();
                    packet.ReadSingle();
                }*/

                if (moveInfo.HasSplineData)
                {
                    if (hasFullSpline)
                    {
                        packet.ReadInt32("Spline Time", index);
                        packet.ReadSingle("Duration", index);

                        if (hasUnkSpline)
                        {
                            for (var i = 0; i < UnkSpline_21BitsCounter; ++i)
                            {
                                packet.ReadSingle("UnkSplineFloat", index, i);
                                packet.ReadSingle("UnkSplineFloat2", index, i);
                            }
                        }

                        packet.ReadSingle("Modify Duration");

                        if (splineCount != 0)
                        {
                            for (var i = 0u; i < splineCount; ++i)
                            {
                                var wp = new Vector3
                                {
                                    X = packet.ReadSingle(),
                                    Z = packet.ReadSingle(),
                                    Y = packet.ReadSingle(),
                                };

                                packet.WriteLine("[{0}][{1}] Spline Waypoint: {2}", index, i, wp);
                            }
                        }

                        if (hasParabolicOrAnimation)
                            packet.ReadInt32("Spline Start Time", index);

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

                        if (hasParabolicAndNotEnded)
                            packet.ReadSingle("Spline Vertical Acceleration", index);

                        packet.ReadInt32("Spline Full Time", index);
                    }

                    moveInfo.Position.X = packet.ReadSingle();
                    moveInfo.Position.Z = packet.ReadSingle();
                    packet.ReadInt32("Spline Id", index);
                    moveInfo.Position.Y = packet.ReadSingle();
                }

                packet.ReadSingle("Fly Speed", index);

                if (hasDWordA0)
                    packet.ReadInt32();

                packet.ReadXORByte(guid1, 2);

                if (hasFallData)
                {
                    if (hasFallDirection)
                    {
                        packet.ReadSingle("sinAngle:", index);
                        packet.ReadSingle("XY SPEED:", index);
                        packet.ReadSingle("cosAngle:", index);
                    }

                    packet.ReadInt32("FallTime: ", index);
                    packet.ReadSingle("zSpeed:", index);
                }

                packet.ReadXORByte(guid1, 1);

                packet.ReadSingle("Turn Speed Rate", index);

                if (hasTimestamp)
                {
                    packet.ReadUInt32("TimeStamp", index);
                }

                packet.ReadSingle("Swim Speed", index);

                if (hasSplineElevation)
                {
                    packet.ReadSingle("Spline Elevation", index);
                }

                packet.ReadXORByte(guid1, 7);

                packet.ReadSingle("Pitch Speed", index);

                for (var i = 0; i < Unk22BitsCounter; ++i)
                {
                    packet.ReadInt32("22BitsCounterInt");
                }

                moveInfo.Position.X = packet.ReadSingle("X");

                if (hasPitch)
                    packet.ReadSingle("Pitch", index);

                if (hasOrientation)
                {
                    moveInfo.Orientation = packet.ReadSingle("O");
                }

                moveInfo.WalkSpeed = packet.ReadSingle("Walk Speed", index) / 2.5f;
                moveInfo.Position.Y = packet.ReadSingle("Y");
                packet.ReadSingle("FlyBack Speed", index);
                packet.ReadXORByte(guid1, 3);
                packet.ReadXORByte(guid1, 5);
                packet.ReadXORByte(guid1, 6);
                packet.ReadXORByte(guid1, 0);
                packet.ReadSingle("RunBack Speed", index);
                moveInfo.RunSpeed = packet.ReadSingle("Run Speed", index) / 7.0f;
                packet.ReadSingle("Swim Back Speed", index);
                moveInfo.Position.Z = packet.ReadSingle("Z");

                packet.WriteGuid("GUID1", guid1, index);
                packet.WriteLine("[{0}] Position: {1}", index, moveInfo.Position);
                packet.WriteLine("[{0}] Orientation: {1}", index, moveInfo.Orientation);
            }


            if (isSceneObject)
            {
                // need update
            }

            if (isAreaTrigger)
            {
                if (bit664)
                {
                    for (var i = 0; i < bits288; ++i)
                    {
                        packet.ReadSingle("Float652+4", index, i);
                        packet.ReadSingle("Float652", index, i);
                        packet.ReadSingle("Float652+8", index, i);
                    }
                }
                 
                if (bit600)
                {
                    packet.ReadSingle("Float584", index);
                    packet.ReadSingle("Float580", index);
                    packet.ReadSingle("Float596", index);
                    packet.ReadSingle("Float592", index);
                    packet.ReadSingle("Float576", index);
                    packet.ReadSingle("Float588", index);
                }
                 
                if (bit644)
                {
                    for (var i = 0; i < bits25C; ++i)
                    {
                        packet.ReadSingle("Float608", index, i);
                        packet.ReadSingle("Float608+4", index, i);
                    }
                     
                    for (var i = 0; i < bits26C; ++i)
                    {
                        packet.ReadSingle("Float260+0", index, i);
                        packet.ReadSingle("Float260+1", index, i);
                    }
                     
                    packet.ReadSingle("Float624", index);
                    packet.ReadSingle("Float624+4", index);
                }
                 
                packet.ReadUInt32("unk520", index);
                 
                if (bit544)
                    packet.ReadUInt32("unk544", index);
                 
                if (bit552)
                    packet.ReadUInt32("unk548", index);
                 
                if (bit536)
                    packet.ReadUInt32("unk532", index);
                 
                if (bit560)
                    packet.ReadUInt32("unk556", index);
                 
                if (bit572)
                {
                    packet.ReadSingle("Float564", index);
                    packet.ReadSingle("Float568", index);
                }
            }
            
            if (hasGOTransportPosition)
            {
                if (hasGOTransportTime2)
                    packet.ReadUInt32("GO Transport Time 2", index);

                moveInfo.TransportOffset.Y = packet.ReadSingle();

                packet.ReadSByte("GO Transport Seat", index);

                moveInfo.TransportOffset.X = packet.ReadSingle();

                packet.ParseBitStream(goTransportGuid, 2, 4, 1);

                if (hasGOTransportTime3)
                    packet.ReadUInt32("GO Transport Time 3", index);

                packet.ReadUInt32("GO Transport Time", index);

                moveInfo.TransportOffset.O = packet.ReadSingle();
                moveInfo.TransportOffset.Z = packet.ReadSingle();

                packet.ParseBitStream(goTransportGuid, 6, 0, 5, 3, 7); 

                moveInfo.TransportGuid = new Guid(BitConverter.ToUInt64(goTransportGuid, 0));
                packet.WriteLine("[{0}] GO Transport GUID {1}", index, moveInfo.TransportGuid);
                packet.WriteLine("[{0}] GO Transport Position: {1}", index, moveInfo.TransportOffset);

            }

            if (hasAttackingTarget)
            {
                packet.ParseBitStream(attackingTargetGuid, 7, 1, 5, 2, 6, 3, 0, 4);
                packet.WriteGuid("Attacking GUID", attackingTargetGuid, index);
            }

            if (hasVehicleData)
            {
                moveInfo.VehicleId = packet.ReadUInt32("Vehicle Id", index);
                packet.ReadSingle("Vehicle Orientation", index);
            }
            
            if (hasStationaryPosition)
            {
                moveInfo.Position.Y = packet.ReadSingle();
                moveInfo.Position.Z = packet.ReadSingle(); 
                moveInfo.Orientation = packet.ReadSingle("Stationary Orientation", index);
                moveInfo.Position.X = packet.ReadSingle();

                packet.WriteLine("[{0}] Stationary Position: {1}", index, moveInfo.Position);
            }

            if (byte2A4)
                packet.ReadInt32("unk672");

            if (hasAnimKits)
            {
                if (hasAnimKit1)
                    packet.ReadUInt16("Anim Kit 1", index);
                if (hasAnimKit3)
                    packet.ReadUInt16("Anim Kit 3", index);
                if (hasAnimKit2)
                    packet.ReadUInt16("Anim Kit 2", index);
            }
            
            if (byte32A)
            {
                var bytes = packet.ReadBytes((int)bits2AA);
                packet.WriteLine("Bytes {0}", Utilities.ByteArrayToHexString(bytes), index);
            }
                
            if (transport)
            {
                packet.ReadUInt32("unk472");
            }
            
            if (byte428)
            {
                for (var i = 0; i < bits418; ++i)
                    packet.ReadInt32("unk1052+4", index, i);
            }
            
            if (hasGameObjectRotation)
            {
                packet.ReadPackedQuaternion("GameObject Rotation", index);
            }
            
            if (byte414)
            {
                packet.ReadInt32();
            }
                
            if (living && moveInfo.HasSplineData && hasFullSpline && splineType == SplineType.FacingTarget)
            {

                var facingTargetGuid = new byte[8];
                facingTargetGuid = packet.StartBitStream(4, 7, 0, 5, 1, 2, 3, 6);
                packet.ParseBitStream(facingTargetGuid, 4, 2, 0, 5, 6, 3, 1, 7);
                packet.WriteGuid("Facing Target GUID", facingTargetGuid, index);
            }

            return moveInfo;
        }

        [Parser(Opcode.SMSG_DESTROY_OBJECT)]
        public static void HandleDestroyObject(Packet packet)
        {
            var Guid = new byte[8];
            Guid[3] = packet.ReadBit();
            Guid[2] = packet.ReadBit();
            Guid[4] = packet.ReadBit();
            Guid[1] = packet.ReadBit();
            var onDeath = packet.ReadBit();
            Guid[7] = packet.ReadBit();
            Guid[0] = packet.ReadBit();
            Guid[6] = packet.ReadBit();
            Guid[5] = packet.ReadBit();
            packet.ParseBitStream(Guid, 0, 4, 7, 2, 6, 3, 1, 5);
            packet.WriteGuid("GUID", Guid);
        }
    }
}

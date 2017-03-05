using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V4_3_4_15595.Parsers
{
    public static class UpdateHandler
    {
        [HasSniffData] // in ReadCreateObjectBlock
        [Parser(Opcode.SMSG_UPDATE_OBJECT)]
        public static void HandleUpdateObject(Packet packet)
        {
            uint map = packet.Translator.ReadUInt16("Map");
            var count = packet.Translator.ReadUInt32("Count");

            for (var i = 0; i < count; i++)
            {
                var type = packet.Translator.ReadByte();
                var typeString = ((UpdateTypeCataclysm)type).ToString();

                packet.AddValue("UpdateType", typeString, i);
                switch (typeString)
                {
                    case "Values":
                    {
                        var guid = packet.Translator.ReadPackedGuid("GUID", i);

                        WoWObject obj;
                        var updates = CoreParsers.UpdateHandler.ReadValuesUpdateBlock(packet, guid.GetObjectType(), i, false);

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
                        var guid = packet.Translator.ReadPackedGuid("GUID", i);
                        ReadCreateObjectBlock(packet, guid, map, i);
                        break;
                    }
                    case "DestroyObjects":
                    {
                        CoreParsers.UpdateHandler.ReadObjectsBlock(packet, i);
                        break;
                    }
                }
            }
        }

        private static void ReadCreateObjectBlock(Packet packet, WowGuid guid, uint map, object index)
        {
            var objType = packet.Translator.ReadByteE<ObjectType>("Object Type", index);
            var moves = ReadMovementUpdateBlock434(packet, guid, index);
            var updates = CoreParsers.UpdateHandler.ReadValuesUpdateBlock(packet, objType, index, true);

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
            obj.Phases = new HashSet<ushort>(CoreParsers.MovementHandler.ActivePhases);

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

        private static MovementInfo ReadMovementUpdateBlock434(Packet packet, WowGuid guid, object index)
        {
            var moveInfo = new MovementInfo();

            // bits
            /*var bit3 =*/
            packet.Translator.ReadBit();
            /*var bit4 =*/
            packet.Translator.ReadBit();
            var hasGameObjectRotation = packet.Translator.ReadBit("Has GameObject Rotation", index);
            var hasAnimKits = packet.Translator.ReadBit("Has AnimKits", index);
            var hasAttackingTarget = packet.Translator.ReadBit("Has Attacking Target", index);
            packet.Translator.ReadBit("Self", index);
            var hasVehicleData = packet.Translator.ReadBit("Has Vehicle Data", index);
            var living = packet.Translator.ReadBit("Living", index);
            var unkLoopCounter = packet.Translator.ReadBits("Unknown array size", 24, index);
            /*var bit1 =*/
            packet.Translator.ReadBit();
            var hasGameObjectPosition = packet.Translator.ReadBit("Has GameObject Position", index);
            var hasStationaryPosition = packet.Translator.ReadBit("Has Stationary Position", index);
            var bit456 = packet.Translator.ReadBit();
            /*var bit2 =*/
            packet.Translator.ReadBit();
            var transport = packet.Translator.ReadBit("Transport", index);
            var hasOrientation = false;
            var guid2 = new byte[8];
            var hasPitch = false;
            var hasFallData = false;
            var hasSplineElevation = false;
            var hasTransportData = false;
            var hasTimestamp = false;
            var transportGuid = new byte[8];
            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var bit216 = false;
            var hasSplineStartTime = false;
            var splineCount = 0u;
            var splineType = SplineType.Stop;
            var facingTargetGuid = new byte[8];
            var hasSplineVerticalAcceleration = false;
            var hasFallDirection = false;
            var goTransportGuid = new byte[8];
            var hasGOTransportTime2 = false;
            var hasGOTransportTime3 = false;
            var attackingTargetGuid = new byte[8];
            var hasAnimKit1 = false;
            var hasAnimKit2 = false;
            var hasAnimKit3 = false;

            if (living)
            {
                var hasMovementFlags = !packet.Translator.ReadBit();
                hasOrientation = !packet.Translator.ReadBit("Lacks orientation", index);
                guid2[7] = packet.Translator.ReadBit();
                guid2[3] = packet.Translator.ReadBit();
                guid2[2] = packet.Translator.ReadBit();
                if (hasMovementFlags)
                    moveInfo.Flags = (MovementFlag)packet.Translator.ReadBitsE<Enums.MovementFlag>("Movement Flags", 30, index);

                packet.Translator.ReadBit("Has MovementInfo spline", index);
                hasPitch = !packet.Translator.ReadBit("Lacks pitch", index);
                moveInfo.HasSplineData = packet.Translator.ReadBit("Has Spline Data", index);
                hasFallData = packet.Translator.ReadBit("Has Fall Data", index);
                hasSplineElevation = !packet.Translator.ReadBit("Lacks spline elevation", index);
                guid2[5] = packet.Translator.ReadBit();
                hasTransportData = packet.Translator.ReadBit("Has Transport Data", index);
                hasTimestamp = !packet.Translator.ReadBit("Lacks timestamp", index);
                if (hasTransportData)
                {
                    transportGuid[1] = packet.Translator.ReadBit();
                    hasTransportTime2 = packet.Translator.ReadBit();
                    transportGuid[4] = packet.Translator.ReadBit();
                    transportGuid[0] = packet.Translator.ReadBit();
                    transportGuid[6] = packet.Translator.ReadBit();
                    hasTransportTime3 = packet.Translator.ReadBit();
                    transportGuid[7] = packet.Translator.ReadBit();
                    transportGuid[5] = packet.Translator.ReadBit();
                    transportGuid[3] = packet.Translator.ReadBit();
                    transportGuid[2] = packet.Translator.ReadBit();
                }

                guid2[4] = packet.Translator.ReadBit();
                if (moveInfo.HasSplineData)
                {
                    bit216 = packet.Translator.ReadBit("Has extended spline data", index);
                    if (bit216)
                    {
                        /*var splineMode =*/
                        packet.Translator.ReadBitsE<SplineMode>("Spline Mode", 2, index);
                        hasSplineStartTime = packet.Translator.ReadBit("Has spline start time", index);
                        splineCount = packet.Translator.ReadBits("Spline Waypoints", 22, index);
                        var bits57 = packet.Translator.ReadBits(2);
                        switch (bits57)
                        {
                            case 0:
                                splineType = SplineType.FacingAngle;
                                break;
                            case 1:
                                splineType = SplineType.FacingSpot;
                                break;
                            case 2:
                                splineType = SplineType.FacingTarget;
                                break;
                            case 3:
                                splineType = SplineType.Normal;
                                break;
                        }

                        if (splineType == SplineType.FacingTarget)
                            facingTargetGuid = packet.Translator.StartBitStream(4, 3, 7, 2, 6, 1, 0, 5);

                        hasSplineVerticalAcceleration = packet.Translator.ReadBit("Has spline vertical acceleration", index);
                        packet.AddValue("Spline type", splineType, index);
                        /*splineFlags =*/
                        packet.Translator.ReadBitsE<SplineFlag434>("Spline flags", 25, index);
                    }
                }

                guid2[6] = packet.Translator.ReadBit();
                if (hasFallData)
                    hasFallDirection = packet.Translator.ReadBit("Has Fall Direction", index);

                guid2[0] = packet.Translator.ReadBit();
                guid2[1] = packet.Translator.ReadBit();
                packet.Translator.ReadBit();
                if (!packet.Translator.ReadBit())
                    moveInfo.FlagsExtra = packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 12, index);
            }

            if (hasGameObjectPosition)
            {
                goTransportGuid[5] = packet.Translator.ReadBit();
                hasGOTransportTime3 = packet.Translator.ReadBit();
                goTransportGuid[0] = packet.Translator.ReadBit();
                goTransportGuid[3] = packet.Translator.ReadBit();
                goTransportGuid[6] = packet.Translator.ReadBit();
                goTransportGuid[1] = packet.Translator.ReadBit();
                goTransportGuid[4] = packet.Translator.ReadBit();
                goTransportGuid[2] = packet.Translator.ReadBit();
                hasGOTransportTime2 = packet.Translator.ReadBit();
                goTransportGuid[7] = packet.Translator.ReadBit();
            }

            if (hasAttackingTarget)
                attackingTargetGuid = packet.Translator.StartBitStream(2, 7, 0, 4, 5, 6, 1, 3);

            if (hasAnimKits)
            {
                hasAnimKit1 = !packet.Translator.ReadBit();
                hasAnimKit2 = !packet.Translator.ReadBit();
                hasAnimKit3 = !packet.Translator.ReadBit();
            }

            packet.Translator.ResetBitReader();

            // Reading data
            for (var i = 0u; i < unkLoopCounter; ++i)
                packet.Translator.ReadUInt32("Unk UInt32", index, (int)i);

            if (living)
            {
                packet.Translator.ReadXORByte(guid2, 4);

                packet.Translator.ReadSingle("RunBack Speed", index);
                if (hasFallData)
                {
                    if (hasFallDirection)
                    {
                        packet.Translator.ReadSingle("Jump XY Speed", index);
                        packet.Translator.ReadSingle("Jump Cos", index);
                        packet.Translator.ReadSingle("Jump Sin", index);
                    }

                    packet.Translator.ReadInt32("Time Fallen", index);
                    packet.Translator.ReadSingle("Fall Z Speed", index);
                }

                packet.Translator.ReadSingle("SwimBack Speed", index);
                if (hasSplineElevation)
                    packet.Translator.ReadSingle("Spline Elevation", index);

                if (moveInfo.HasSplineData)
                {
                    if (bit216)
                    {
                        if (hasSplineVerticalAcceleration)
                            packet.Translator.ReadSingle("Spline Vertical Acceleration", index);
                        packet.Translator.ReadUInt32("Spline Time", index);
                        if (splineType == SplineType.FacingAngle)
                            packet.Translator.ReadSingle("Facing Angle", index);
                        else if (splineType == SplineType.FacingTarget)
                        {
                            packet.Translator.ParseBitStream(facingTargetGuid, 5, 3, 7, 1, 6, 4, 2, 0);
                            packet.Translator.WriteGuid("Facing Target GUID", facingTargetGuid, index);
                        }

                        for (var i = 0u; i < splineCount; ++i)
                        {
                            var wp = new Vector3
                            {
                                Z = packet.Translator.ReadSingle(),
                                X = packet.Translator.ReadSingle(),
                                Y = packet.Translator.ReadSingle()
                            };

                            packet.AddValue("Spline Waypoint", wp, index, i);
                        }

                        if (splineType == SplineType.FacingSpot)
                        {
                            var point = new Vector3
                            {
                                X = packet.Translator.ReadSingle(),
                                Z = packet.Translator.ReadSingle(),
                                Y = packet.Translator.ReadSingle()
                            };

                            packet.AddValue("Facing Spot", point, index);
                        }

                        packet.Translator.ReadSingle("Spline Duration Multiplier Next", index);
                        packet.Translator.ReadUInt32("Spline Full Time", index);
                        if (hasSplineStartTime)
                            packet.Translator.ReadUInt32("Spline Start time", index);

                        packet.Translator.ReadSingle("Spline Duration Multiplier", index);
                    }

                    var endPoint = new Vector3
                    {
                        Z = packet.Translator.ReadSingle(),
                        X = packet.Translator.ReadSingle(),
                        Y = packet.Translator.ReadSingle()
                    };

                    packet.Translator.ReadUInt32("Spline Id", index);
                    packet.AddValue("Spline Endpoint:", endPoint, index);
                }

                moveInfo.Position.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(guid2, 5);

                if (hasTransportData)
                {
                    packet.Translator.ReadXORByte(transportGuid, 5);
                    packet.Translator.ReadXORByte(transportGuid, 7);

                    packet.Translator.ReadUInt32("Transport Time", index);
                    moveInfo.TransportOffset.O = packet.Translator.ReadSingle();
                    if (hasTransportTime2)
                        packet.Translator.ReadUInt32("Transport Time 2", index);

                    moveInfo.TransportOffset.Y = packet.Translator.ReadSingle();
                    moveInfo.TransportOffset.X = packet.Translator.ReadSingle();
                    packet.Translator.ReadXORByte(transportGuid, 3);

                    moveInfo.TransportOffset.Z = packet.Translator.ReadSingle();
                    packet.Translator.ReadXORByte(transportGuid, 0);

                    if (hasTransportTime3)
                        packet.Translator.ReadUInt32("Transport Time 3", index);

                    var seat = packet.Translator.ReadSByte("Transport Seat", index);
                    packet.Translator.ReadXORByte(transportGuid, 1);
                    packet.Translator.ReadXORByte(transportGuid, 6);
                    packet.Translator.ReadXORByte(transportGuid, 2);
                    packet.Translator.ReadXORByte(transportGuid, 4);
                    moveInfo.TransportGuid = new WowGuid64(BitConverter.ToUInt64(transportGuid, 0));
                    packet.AddValue("Transport GUID", moveInfo.TransportGuid, index);
                    packet.AddValue("Transport Position", moveInfo.TransportOffset, index);

                    if (moveInfo.TransportGuid.HasEntry() && moveInfo.TransportGuid.GetHighType() == HighGuidType.Vehicle &&
                        guid.HasEntry() && guid.GetHighType() == HighGuidType.Creature)
                    {
                        VehicleTemplateAccessory vehicleAccessory = new VehicleTemplateAccessory
                        {
                            Entry = moveInfo.TransportGuid.GetEntry(),
                            AccessoryEntry = guid.GetEntry(),
                            SeatId = seat
                        };
                        Storage.VehicleTemplateAccessories.Add(vehicleAccessory, packet.TimeSpan);
                    }
                }

                moveInfo.Position.X = packet.Translator.ReadSingle();
                packet.Translator.ReadSingle("Pitch Speed", index);
                packet.Translator.ReadXORByte(guid2, 3);
                packet.Translator.ReadXORByte(guid2, 0);

                packet.Translator.ReadSingle("Swim Speed", index);
                moveInfo.Position.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(guid2, 7);
                packet.Translator.ReadXORByte(guid2, 1);
                packet.Translator.ReadXORByte(guid2, 2);

                moveInfo.WalkSpeed = packet.Translator.ReadSingle("Walk Speed", index) / 2.5f;
                if (hasTimestamp)
                    packet.Translator.ReadUInt32("Time", index);

                packet.Translator.ReadSingle("FlyBack Speed", index);
                packet.Translator.ReadXORByte(guid2, 6);

                packet.Translator.ReadSingle("Turn Speed", index);
                if (hasOrientation)
                    moveInfo.Orientation = packet.Translator.ReadSingle();

                moveInfo.RunSpeed = packet.Translator.ReadSingle("Run Speed", index) / 7.0f;
                if (hasPitch)
                    packet.Translator.ReadSingle("Pitch", index);

                packet.Translator.ReadSingle("Fly Speed", index);

                packet.Translator.WriteGuid("GUID 2", guid2);
                packet.AddValue("Position", moveInfo.Position, index);
                packet.AddValue("Orientation", moveInfo.Orientation, index);
            }

            if (hasVehicleData)
            {
                packet.Translator.ReadSingle("Vehicle Orientation", index);
                moveInfo.VehicleId = packet.Translator.ReadUInt32("Vehicle Id", index);
            }

            if (hasGameObjectPosition)
            {
                packet.Translator.ReadXORByte(goTransportGuid, 0);
                packet.Translator.ReadXORByte(goTransportGuid, 5);
                if (hasGOTransportTime3)
                    packet.Translator.ReadUInt32("GO Transport Time 3", index);

                packet.Translator.ReadXORByte(goTransportGuid, 3);

                moveInfo.TransportOffset.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(goTransportGuid, 4);
                packet.Translator.ReadXORByte(goTransportGuid, 6);
                packet.Translator.ReadXORByte(goTransportGuid, 1);

                packet.Translator.ReadSingle("GO Transport Time", index);
                moveInfo.TransportOffset.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(goTransportGuid, 2);
                packet.Translator.ReadXORByte(goTransportGuid, 7);

                moveInfo.TransportOffset.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadByte("GO Transport Seat", index);
                moveInfo.TransportOffset.O = packet.Translator.ReadSingle();
                if (hasGOTransportTime2)
                    packet.Translator.ReadUInt32("GO Transport Time 2", index);

                moveInfo.TransportGuid = new WowGuid64(BitConverter.ToUInt64(goTransportGuid, 0));
                packet.AddValue("GO Transport GUID", moveInfo.TransportGuid, index);
                packet.AddValue("GO Transport Position", moveInfo.TransportOffset, index);
            }

            if (hasGameObjectRotation)
                moveInfo.Rotation = packet.Translator.ReadPackedQuaternion("GameObject Rotation", index);

            if (bit456)
            {
                // float[] arr = new float[16];
                // ordering: 13, 4, 7, 15, BYTE, 10, 11, 3, 5, 14, 6, 1, 8, 12, 0, 2, 9
                packet.Translator.ReadBytes(4 * 16 + 1);
            }

            if (hasStationaryPosition)
            {
                moveInfo.Orientation = packet.Translator.ReadSingle("Stationary Orientation", index);
                moveInfo.Position = packet.Translator.ReadVector3("Stationary Position", index);
            }

            if (hasAttackingTarget)
            {
                packet.Translator.ParseBitStream(attackingTargetGuid, 4, 0, 3, 5, 7, 6, 2, 1);
                packet.Translator.WriteGuid("Attacking Target GUID", attackingTargetGuid, index);
            }

            if (hasAnimKits)
            {
                if (hasAnimKit1)
                    packet.Translator.ReadUInt16("AI Anim Kit Id", index);
                if (hasAnimKit2)
                    packet.Translator.ReadUInt16("Movement Anim Kit Id", index);
                if (hasAnimKit3)
                    packet.Translator.ReadUInt16("Melee Anim Kit Id", index);
            }

            if (transport)
                packet.Translator.ReadUInt32("Transport path timer", index);

            return moveInfo;
        }
    }
}

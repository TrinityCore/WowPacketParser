using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.PacketStructures;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using MovementFlag = WowPacketParser.Enums.v4.MovementFlag;
using MovementFlag2 = WowPacketParser.Enums.v4.MovementFlag2;

namespace WowPacketParserModule.V4_3_4_15595.Parsers
{
    public static class UpdateHandler
    {
        [HasSniffData] // in ReadCreateObjectBlock
        [Parser(Opcode.SMSG_UPDATE_OBJECT)]
        public static void HandleUpdateObject(Packet packet)
        {
            var updateObject = packet.Holder.UpdateObject = new();
            uint map = updateObject.MapId = packet.ReadUInt16("Map");
            var count = packet.ReadUInt32("Count");

            for (var i = 0; i < count; i++)
            {
                var type = (UpdateTypeCataclysm)packet.ReadByte();

                var partWriter = new StringBuilderProtoPart(packet.Writer);
                packet.AddValue("UpdateType", type.ToString(), i);
                switch (type)
                {
                    case UpdateTypeCataclysm.Values:
                    {
                        var guid = packet.ReadPackedGuid("GUID", i);
                        var updateValues = new UpdateValues(){Legacy = new()};
                        CoreParsers.UpdateHandler.ReadValuesUpdateBlock(packet, updateValues.Legacy, guid, i);
                        updateObject.Updated.Add(new UpdateObject{Guid = guid, Values = updateValues, TextStartOffset = partWriter.StartOffset, TextLength = partWriter.Length, Text = partWriter.Text });
                        break;
                    }
                    case UpdateTypeCataclysm.CreateObject1:
                    case UpdateTypeCataclysm.CreateObject2:
                    {
                        var guid = packet.ReadPackedGuid("GUID", i);
                        var createType = type.ToCreateObjectType();
                        var createObject = new CreateObject() { Guid = guid, Values = new(){Legacy = new()}, CreateType = createType };
                        ReadCreateObjectBlock(packet, createObject, guid, map, createType, i);
                        createObject.Text = partWriter.Text;
                        createObject.TextStartOffset = partWriter.StartOffset;
                        createObject.TextLength = partWriter.Length;
                        updateObject.Created.Add(createObject);
                        break;
                    }
                    case UpdateTypeCataclysm.DestroyObjects:
                    {
                        CoreParsers.UpdateHandler.ReadDestroyObjectsBlock(packet, i);
                        break;
                    }
                }
            }
        }

        private static void ReadCreateObjectBlock(Packet packet, CreateObject createObject, WowGuid guid, uint map, CreateObjectType createType, object index)
        {
            ObjectType objType = ObjectTypeConverter.Convert(packet.ReadByteE<ObjectTypeLegacy>("Object Type", index));
            WoWObject obj = CoreParsers.UpdateHandler.CreateObject(objType, guid, map);

            obj.CreateType = createType;
            obj.Movement = ReadMovementUpdateBlock434(packet, guid, index);
            obj.UpdateFields = CoreParsers.UpdateHandler.ReadValuesUpdateBlockOnCreate(packet, createObject.Values.Legacy, objType, index);

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
            packet.ReadBit();
            /*var bit4 =*/
            packet.ReadBit();
            var hasGameObjectRotation = packet.ReadBit("Has GameObject Rotation", index);
            var hasAnimKits = packet.ReadBit("Has AnimKits", index);
            var hasAttackingTarget = packet.ReadBit("Has Attacking Target", index);
            packet.ReadBit("Self", index);
            var hasVehicleData = packet.ReadBit("Has Vehicle Data", index);
            var living = packet.ReadBit("Living", index);
            var unkLoopCounter = packet.ReadBits("Unknown array size", 24, index);
            /*var bit1 =*/
            packet.ReadBit();
            var hasGameObjectPosition = packet.ReadBit("Has GameObject Position", index);
            var hasStationaryPosition = packet.ReadBit("Has Stationary Position", index);
            var hasAreaTrigger = packet.ReadBit();
            /*var bit2 =*/
            packet.ReadBit();
            var transport = packet.ReadBit("Transport", index);
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
                var hasMovementFlags = !packet.ReadBit();
                hasOrientation = !packet.ReadBit("Lacks orientation", index);
                guid2[7] = packet.ReadBit();
                guid2[3] = packet.ReadBit();
                guid2[2] = packet.ReadBit();
                if (hasMovementFlags)
                    moveInfo.Flags = (uint)packet.ReadBitsE<MovementFlag>("Movement Flags", 30, index);

                packet.ReadBit("Has MovementInfo spline", index);
                hasPitch = !packet.ReadBit("Lacks pitch", index);
                moveInfo.HasSplineData = packet.ReadBit("Has Spline Data", index);
                hasFallData = packet.ReadBit("Has Fall Data", index);
                hasSplineElevation = !packet.ReadBit("Lacks spline elevation", index);
                guid2[5] = packet.ReadBit();
                hasTransportData = packet.ReadBit("Has Transport Data", index);
                hasTimestamp = !packet.ReadBit("Lacks timestamp", index);
                if (hasTransportData)
                {
                    transportGuid[1] = packet.ReadBit();
                    hasTransportTime2 = packet.ReadBit();
                    transportGuid[4] = packet.ReadBit();
                    transportGuid[0] = packet.ReadBit();
                    transportGuid[6] = packet.ReadBit();
                    hasTransportTime3 = packet.ReadBit();
                    transportGuid[7] = packet.ReadBit();
                    transportGuid[5] = packet.ReadBit();
                    transportGuid[3] = packet.ReadBit();
                    transportGuid[2] = packet.ReadBit();
                }

                guid2[4] = packet.ReadBit();
                if (moveInfo.HasSplineData)
                {
                    bit216 = packet.ReadBit("Has extended spline data", index);
                    if (bit216)
                    {
                        /*var splineMode =*/
                        packet.ReadBitsE<SplineMode>("Spline Mode", 2, index);
                        hasSplineStartTime = packet.ReadBit("Has spline start time", index);
                        splineCount = packet.ReadBits("Spline Waypoints", 22, index);
                        var bits57 = packet.ReadBits(2);
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
                            facingTargetGuid = packet.StartBitStream(4, 3, 7, 2, 6, 1, 0, 5);

                        hasSplineVerticalAcceleration = packet.ReadBit("Has spline vertical acceleration", index);
                        packet.AddValue("Spline type", splineType, index);
                        /*splineFlags =*/
                        packet.ReadBitsE<SplineFlag434>("Spline flags", 25, index);
                    }
                }

                guid2[6] = packet.ReadBit();
                if (hasFallData)
                    hasFallDirection = packet.ReadBit("Has Fall Direction", index);

                guid2[0] = packet.ReadBit();
                guid2[1] = packet.ReadBit();
                packet.ReadBit();
                if (!packet.ReadBit())
                    moveInfo.Flags2 = (uint)packet.ReadBitsE<MovementFlag2>("Extra Movement Flags", 12, index);
            }

            if (hasGameObjectPosition)
            {
                goTransportGuid[5] = packet.ReadBit();
                hasGOTransportTime3 = packet.ReadBit();
                goTransportGuid[0] = packet.ReadBit();
                goTransportGuid[3] = packet.ReadBit();
                goTransportGuid[6] = packet.ReadBit();
                goTransportGuid[1] = packet.ReadBit();
                goTransportGuid[4] = packet.ReadBit();
                goTransportGuid[2] = packet.ReadBit();
                hasGOTransportTime2 = packet.ReadBit();
                goTransportGuid[7] = packet.ReadBit();
            }

            if (hasAttackingTarget)
                attackingTargetGuid = packet.StartBitStream(2, 7, 0, 4, 5, 6, 1, 3);

            if (hasAnimKits)
            {
                hasAnimKit1 = !packet.ReadBit();
                hasAnimKit2 = !packet.ReadBit();
                hasAnimKit3 = !packet.ReadBit();
            }

            packet.ResetBitReader();

            // Reading data
            for (var i = 0u; i < unkLoopCounter; ++i)
                packet.ReadUInt32("Unk UInt32", index, (int)i);

            if (living)
            {
                packet.ReadXORByte(guid2, 4);

                packet.ReadSingle("RunBack Speed", index);
                if (hasFallData)
                {
                    if (hasFallDirection)
                    {
                        packet.ReadSingle("Jump XY Speed", index);
                        packet.ReadSingle("Jump Cos", index);
                        packet.ReadSingle("Jump Sin", index);
                    }

                    packet.ReadInt32("Time Fallen", index);
                    packet.ReadSingle("Fall Z Speed", index);
                }

                packet.ReadSingle("SwimBack Speed", index);
                if (hasSplineElevation)
                    packet.ReadSingle("Spline Elevation", index);

                if (moveInfo.HasSplineData)
                {
                    if (bit216)
                    {
                        if (hasSplineVerticalAcceleration)
                            packet.ReadSingle("Spline Vertical Acceleration", index);
                        packet.ReadUInt32("Spline Time", index);
                        if (splineType == SplineType.FacingAngle)
                            packet.ReadSingle("Facing Angle", index);
                        else if (splineType == SplineType.FacingTarget)
                        {
                            packet.ParseBitStream(facingTargetGuid, 5, 3, 7, 1, 6, 4, 2, 0);
                            packet.WriteGuid("Facing Target GUID", facingTargetGuid, index);
                        }

                        for (var i = 0u; i < splineCount; ++i)
                        {
                            var wp = new Vector3
                            {
                                Z = packet.ReadSingle(),
                                X = packet.ReadSingle(),
                                Y = packet.ReadSingle()
                            };

                            packet.AddValue("Spline Waypoint", wp, index, i);
                        }

                        if (splineType == SplineType.FacingSpot)
                        {
                            var point = new Vector3
                            {
                                X = packet.ReadSingle(),
                                Z = packet.ReadSingle(),
                                Y = packet.ReadSingle()
                            };

                            packet.AddValue("Facing Spot", point, index);
                        }

                        packet.ReadSingle("Spline Duration Multiplier Next", index);
                        packet.ReadUInt32("Spline Full Time", index);
                        if (hasSplineStartTime)
                            packet.ReadUInt32("Spline Start time", index);

                        packet.ReadSingle("Spline Duration Multiplier", index);
                    }

                    var endPoint = new Vector3
                    {
                        Z = packet.ReadSingle(),
                        X = packet.ReadSingle(),
                        Y = packet.ReadSingle()
                    };

                    packet.ReadUInt32("Spline Id", index);
                    packet.AddValue("Spline Endpoint:", endPoint, index);
                }

                moveInfo.Position.Z = packet.ReadSingle();
                packet.ReadXORByte(guid2, 5);

                if (hasTransportData)
                {
                    moveInfo.Transport = new MovementInfo.TransportInfo();

                    packet.ReadXORByte(transportGuid, 5);
                    packet.ReadXORByte(transportGuid, 7);

                    packet.ReadUInt32("Transport Time", index);
                    moveInfo.Transport.Offset.O = packet.ReadSingle();
                    if (hasTransportTime2)
                        packet.ReadUInt32("Transport Time 2", index);

                    moveInfo.Transport.Offset.Y = packet.ReadSingle();
                    moveInfo.Transport.Offset.X = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 3);

                    moveInfo.Transport.Offset.Z = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 0);

                    if (hasTransportTime3)
                        packet.ReadUInt32("Transport Time 3", index);

                    var seat = packet.ReadSByte("Transport Seat", index);
                    packet.ReadXORByte(transportGuid, 1);
                    packet.ReadXORByte(transportGuid, 6);
                    packet.ReadXORByte(transportGuid, 2);
                    packet.ReadXORByte(transportGuid, 4);
                    moveInfo.Transport.Guid = new WowGuid64(BitConverter.ToUInt64(transportGuid, 0));
                    packet.AddValue("Transport GUID", moveInfo.Transport.Guid, index);
                    packet.AddValue("Transport Position", moveInfo.Transport.Offset, index);

                    if (moveInfo.Transport.Guid.HasEntry() && moveInfo.Transport.Guid.GetHighType() == HighGuidType.Vehicle &&
                        guid.HasEntry() && guid.GetHighType() == HighGuidType.Creature)
                    {
                        VehicleTemplateAccessory vehicleAccessory = new VehicleTemplateAccessory
                        {
                            Entry = moveInfo.Transport.Guid.GetEntry(),
                            AccessoryEntry = guid.GetEntry(),
                            SeatId = seat
                        };
                        Storage.VehicleTemplateAccessories.Add(vehicleAccessory, packet.TimeSpan);
                    }
                }

                moveInfo.Position.X = packet.ReadSingle();
                packet.ReadSingle("Pitch Speed", index);
                packet.ReadXORByte(guid2, 3);
                packet.ReadXORByte(guid2, 0);

                packet.ReadSingle("Swim Speed", index);
                moveInfo.Position.Y = packet.ReadSingle();
                packet.ReadXORByte(guid2, 7);
                packet.ReadXORByte(guid2, 1);
                packet.ReadXORByte(guid2, 2);

                moveInfo.WalkSpeed = packet.ReadSingle("Walk Speed", index) / 2.5f;
                if (hasTimestamp)
                    packet.ReadUInt32("Time", index);

                packet.ReadSingle("FlyBack Speed", index);
                packet.ReadXORByte(guid2, 6);

                packet.ReadSingle("Turn Speed", index);
                if (hasOrientation)
                    moveInfo.Orientation = packet.ReadSingle();

                moveInfo.RunSpeed = packet.ReadSingle("Run Speed", index) / 7.0f;
                if (hasPitch)
                    packet.ReadSingle("Pitch", index);

                packet.ReadSingle("Fly Speed", index);

                packet.WriteGuid("GUID 2", guid2);
                packet.AddValue("Position", moveInfo.Position, index);
                packet.AddValue("Orientation", moveInfo.Orientation, index);
            }

            if (hasVehicleData)
            {
                packet.ReadSingle("Vehicle Orientation", index);
                moveInfo.VehicleId = packet.ReadUInt32("Vehicle Id", index);
            }

            if (hasGameObjectPosition)
            {
                moveInfo.Transport = new MovementInfo.TransportInfo();

                packet.ReadXORByte(goTransportGuid, 0);
                packet.ReadXORByte(goTransportGuid, 5);
                if (hasGOTransportTime3)
                    packet.ReadUInt32("GO Transport Time 3", index);

                packet.ReadXORByte(goTransportGuid, 3);

                moveInfo.Transport.Offset.X = packet.ReadSingle();
                packet.ReadXORByte(goTransportGuid, 4);
                packet.ReadXORByte(goTransportGuid, 6);
                packet.ReadXORByte(goTransportGuid, 1);

                packet.ReadSingle("GO Transport Time", index);
                moveInfo.Transport.Offset.Y = packet.ReadSingle();
                packet.ReadXORByte(goTransportGuid, 2);
                packet.ReadXORByte(goTransportGuid, 7);

                moveInfo.Transport.Offset.Z = packet.ReadSingle();
                packet.ReadByte("GO Transport Seat", index);
                moveInfo.Transport.Offset.O = packet.ReadSingle();
                if (hasGOTransportTime2)
                    packet.ReadUInt32("GO Transport Time 2", index);

                moveInfo.Transport.Guid = new WowGuid64(BitConverter.ToUInt64(goTransportGuid, 0));
                packet.AddValue("GO Transport GUID", moveInfo.Transport.Guid, index);
                packet.AddValue("GO Transport Position", moveInfo.Transport.Offset, index);
            }

            if (hasGameObjectRotation)
                moveInfo.Rotation = packet.ReadPackedQuaternion("GameObject Rotation", index);

            if (hasAreaTrigger)
            {
                // float[] arr = new float[16];
                // ordering: 13, 4, 7, 15, BYTE, 10, 11, 3, 5, 14, 6, 1, 8, 12, 0, 2, 9
                var arr = new float[16];
                var sequence = new int[] { 13, 4, 7, 15, 10, 11, 3, 5, 14, 6, 1, 8, 12, 0, 2, 9 };
                foreach (var idx in sequence)
                {
                    arr[idx] = packet.ReadSingle($"AreaTrigger Data[{idx}]");
                    if (idx == 15)
                        packet.ReadByte("AreaTrigger Byte");
                }
            }

            if (hasStationaryPosition)
            {
                moveInfo.Orientation = packet.ReadSingle("Stationary Orientation", index);
                moveInfo.Position = packet.ReadVector3("Stationary Position", index);
            }

            if (hasAttackingTarget)
            {
                packet.ParseBitStream(attackingTargetGuid, 4, 0, 3, 5, 7, 6, 2, 1);
                packet.WriteGuid("Attacking Target GUID", attackingTargetGuid, index);
            }

            if (hasAnimKits)
            {
                if (hasAnimKit1)
                    packet.ReadUInt16("AI Anim Kit Id", index);
                if (hasAnimKit2)
                    packet.ReadUInt16("Movement Anim Kit Id", index);
                if (hasAnimKit3)
                    packet.ReadUInt16("Melee Anim Kit Id", index);
            }

            if (transport)
                packet.ReadUInt32("Transport path timer", index);

            return moveInfo;
        }
    }
}

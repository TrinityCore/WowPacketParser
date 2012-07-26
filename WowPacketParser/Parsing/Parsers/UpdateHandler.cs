using System;
using System.Collections;
using System.Collections.Generic;
using PacketParser.Enums;
using PacketParser.Enums.Version;
using PacketParser.Misc;
using Guid = PacketParser.DataStructures.Guid;
using PacketParser.Processing;
using PacketParser.DataStructures;
using System.Text;

namespace PacketParser.Parsing.Parsers
{
    public static class UpdateHandler
    {
        [Parser(Opcode.SMSG_UPDATE_OBJECT)]
        public static void HandleUpdateObject(Packet packet)
        {
            uint map = MovementHandler.CurrentMapId;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                map = packet.ReadUInt16("Map");

            var count = packet.ReadUInt32("Count");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_2_9056))
                packet.ReadBoolean("Has Transport");
            var ObjectStore = PacketFileProcessor.Current.GetProcessor<ObjectStore>();
            packet.StoreBeginList("Updates");
            for (var i = 0; i < count; i++)
            {
                var type = packet.ReadByte();
                string typeObj;
                if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
                    typeObj = Enum<UpdateTypeCataclysm>.ToString(type);
                else
                    typeObj = Enum<UpdateType>.ToString(type);

                packet.Store("UpdateType", typeObj, i);
                switch (typeObj)
                {
                    case "Values":
                    {
                        var guid = packet.ReadPackedGuid("GUID", i);

                        WoWObject obj = ObjectStore.GetObjectIfFound(guid);
                        var updates = ReadValuesUpdateBlock(ref packet, guid.GetObjectType(), i);

                        if (obj != null)
                        {
                            if (obj.ChangedUpdateFieldsList == null)
                                obj.ChangedUpdateFieldsList = new List<Dictionary<int, UpdateField>>();
                            obj.ChangedUpdateFieldsList.Add(updates);
                        }

                        break;
                    }
                    case "Movement":
                    {
                        var guid = ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_2_9901) ? packet.ReadPackedGuid("GUID", i) : packet.ReadGuid("GUID", i);
                        ReadMovementUpdateBlock(ref packet, guid, i);
                        // Should we update Storage.Object?
                        break;
                    }
                    case "CreateObject1":
                    case "CreateObject2": // Might != CreateObject1 on Cata
                    {
                        var guid = packet.ReadPackedGuid("GUID", i);
                        ReadCreateObjectBlock(ref packet, guid, map, i);
                        break;
                    }
                    case "FarObjects":
                    case "NearObjects":
                    case "DestroyObjects":
                    {
                        ReadObjectsBlock(ref packet, i);
                        break;
                    }
                }
            }
            packet.StoreEndList();
        }

        private static void ReadCreateObjectBlock(ref Packet packet, Guid guid, uint map, int index)
        {
            var objType = packet.ReadEnum<ObjectType>("Object Type", TypeCode.Byte, index);
            var moves = ReadMovementUpdateBlock(ref packet, guid, index);
            var updates = ReadValuesUpdateBlock(ref packet, objType, index);

            WoWObject obj;
            switch (objType)
            {
                case ObjectType.Unit:       obj = new Unit(); break;
                case ObjectType.GameObject: obj = new GameObject(); break;
                case ObjectType.Item:       obj = new Item(); break;
                case ObjectType.Player:     obj = new Player(); break;
                default:                    obj = new WoWObject(); break;
            }

            obj.Type = objType;
            obj.Movement = moves;
            obj.UpdateFields = updates;
            obj.Map = map;
            obj.Area = WorldStateHandler.CurrentAreaId;
            obj.PhaseMask = (uint) MovementHandler.CurrentPhaseMask;

            // If this is the second time we see the same object (same guid,
            // same position) update its phasemask
            var existObj = PacketFileProcessor.Current.GetProcessor<ObjectStore>().GetObjectIfFound(guid);
            if (existObj != null)
                ProcessExistingObject(ref existObj, obj, guid); // can't do "ref Storage.Objects[guid].Item1 directly
            else
                PacketFileProcessor.Current.GetProcessor<ObjectStore>().AddObject(guid, obj, packet.TimeSpan);
        }

        private static void ProcessExistingObject(ref WoWObject obj, WoWObject newObj, Guid guid)
        {
            obj.PhaseMask |= newObj.PhaseMask;
            if (guid.GetHighType() == HighGuidType.Unit) // skip if not an unit
            {
                if (!obj.Movement.HasWpsOrRandMov)
                    if (obj.Movement.Position != newObj.Movement.Position)
                    {
                        UpdateField uf;
                        if (obj.UpdateFields.TryGetValue((int)Enums.Version.UpdateFields.GetUpdateFieldOffset(UnitField.UNIT_FIELD_FLAGS), out uf))
                            if ((uf.UInt32Value & (uint) UnitFlags.IsInCombat) == 0) // movement could be because of aggro so ignore that
                                obj.Movement.HasWpsOrRandMov = true;
                    }
            }
        }

        private static void ReadObjectsBlock(ref Packet packet, int index)
        {
            var objCount = packet.ReadInt32("Object Count", index);
            packet.StoreBeginList("Objects", index);
            for (var j = 0; j < objCount; j++)
                packet.ReadPackedGuid("Object GUID", index, j);
            packet.StoreEndList();
        }

        private static Dictionary<int, UpdateField> ReadValuesUpdateBlock(ref Packet packet, ObjectType type, int index)
        {
            var maskSize = packet.ReadByte();

            var updateMask = new int[maskSize];
            for (var i = 0; i < maskSize; i++)
                updateMask[i] = packet.ReadInt32();

            var mask = new BitArray(updateMask);
            bool[] m = new bool[mask.Count];
            mask.CopyTo(m, 0);
            var dict = new Dictionary<int, UpdateField>();

            int objectEnd = (int)UpdateFields.GetUpdateFieldOffset(ObjectField.OBJECT_END);

            packet.StoreBeginList("UpdateFields", index);
            for (var i = 0; i < m.Length; i++)
            {
                if (!m[i])
                    continue;

                var blockVal = packet.ReadUpdateField();
                StringBuilder keyBuilder = new StringBuilder(30);

                var enumType = UpdateFields.GetUpdateFieldEnumByOffset(i, type);
                var name = UpdateFields.GetUpdateFieldName(i, enumType);
                if (name == null)
                {
                    keyBuilder.Append("Update field ");
                    keyBuilder.Append(i);
                }
                else
                {
                    keyBuilder.Append(name);
                    keyBuilder.Append(" (");
                    keyBuilder.Append(i);
                    keyBuilder.Append(")");
                }
                packet.Store(keyBuilder.ToString(), blockVal, index, i);
                dict.Add(i, blockVal);
            }
            packet.StoreEndList();

            return dict;
        }

        private static MovementInfo ReadMovementUpdateBlock434(ref Packet packet, Guid guid, int index)
        {
            var moveInfo = new MovementInfo();

            // bits
            /*var bit3 =*/ packet.ReadBit();
            /*var bit4 =*/ packet.ReadBit();
            var hasGameObjectRotation = packet.ReadBit("Has GameObject Rotation", index);
            var hasAnimKits = packet.ReadBit("Has AnimKits", index);
            var hasAttackingTarget = packet.ReadBit("Has Attacking Target", index);
            packet.ReadBit("Self", index);
            var hasVehicleData = packet.ReadBit("Has Vehicle Data", index);
            var living = packet.ReadBit("Living", index);
            var unkLoopCounter = packet.ReadBits(24);
            /*var bit1 =*/ packet.ReadBit();
            var hasGameObjectPosition = packet.ReadBit("Has GameObject Position", index);
            var hasStationaryPosition = packet.ReadBit("Has Stationary Position", index);
            var bit456 = packet.ReadBit();
            /*var bit2 =*/ packet.ReadBit();
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
                hasOrientation = !packet.ReadBit();
                guid2[7] = packet.ReadBit();
                guid2[3] = packet.ReadBit();
                guid2[2] = packet.ReadBit();
                if (hasMovementFlags)
                    moveInfo.Flags = packet.ReadEnum<MovementFlag>("Movement Flags", 30, index);

                packet.ReadBit();
                hasPitch = !packet.ReadBit();
                moveInfo.HasSplineData = packet.ReadBit("Has Spline Data", index);
                hasFallData = packet.ReadBit("Has Fall Data", index);
                hasSplineElevation = !packet.ReadBit();
                guid2[5] = packet.ReadBit();
                hasTransportData = packet.ReadBit("Has Transport Data", index);
                hasTimestamp = !packet.ReadBit();
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
                        /*var splineMode =*/ packet.ReadEnum<SplineMode>("Spline Mode", 2, index);
                        hasSplineStartTime = packet.ReadBit();
                        splineCount = packet.ReadBits("Spline Waypoints Count", 22, index);
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

                        hasSplineVerticalAcceleration = packet.ReadBit();
                        packet.Store("Spline type", splineType, index);
                        /*splineFlags =*/ packet.ReadEnum<SplineFlag434>("Spline flags", 25, index);
                    }
                }

                guid2[6] = packet.ReadBit();
                if (hasFallData)
                    hasFallDirection = packet.ReadBit("Has Fall Direction", index);

                guid2[0] = packet.ReadBit();
                guid2[1] = packet.ReadBit();
                packet.ReadBit();
                if (!packet.ReadBit())
                    moveInfo.FlagsExtra = packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12, index);
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
                        packet.ReadSingle("Jump Cos", index);
                        packet.ReadSingle("Jump Velocity", index);
                        packet.ReadSingle("Jump Sin", index);
                    }

                    packet.ReadInt32("Time Fallen", index);
                    packet.ReadSingle("Fall Start Velocity", index);
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
                            packet.StoreBitstreamGuid("Facing Target GUID", facingTargetGuid, index);
                        }

                        for (var i = 0; i < splineCount; ++i)
                        {
                            var wp = new Vector3
                            {
                                Z = packet.ReadSingle(),
                                X = packet.ReadSingle(),
                                Y = packet.ReadSingle(),
                            };

                            packet.Store("Spline Waypoint", wp, index, i);
                        }

                        if (splineType == SplineType.FacingSpot)
                        {
                            var point = new Vector3
                            {
                                X = packet.ReadSingle(),
                                Z = packet.ReadSingle(),
                                Y = packet.ReadSingle(),
                            };

                            packet.Store("Facing Spot", point, index);
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
                        Y = packet.ReadSingle(),
                    };

                    packet.ReadUInt32("Spline Id", index);
                    packet.Store("Spline Endpoint", endPoint, index);
                }

                moveInfo.Position.Z = packet.ReadSingle();
                packet.ReadXORByte(guid2, 5);

                if (hasTransportData)
                {
                    packet.ReadXORByte(transportGuid, 5);
                    packet.ReadXORByte(transportGuid, 7);

                    packet.ReadUInt32("Transport Time", index);
                    var transPos = new Vector4();
                    transPos.O = packet.ReadSingle();
                    if (hasTransportTime2)
                        packet.ReadUInt32("Transport Time 2", index);

                    transPos.Y = packet.ReadSingle();
                    transPos.X = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 3);

                    transPos.Z = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 0);

                    if (hasTransportTime3)
                        packet.ReadUInt32("Transport Time 3", index);

                    packet.ReadSByte("Transport Seat", index);
                    packet.ReadXORByte(transportGuid, 1);
                    packet.ReadXORByte(transportGuid, 6);
                    packet.ReadXORByte(transportGuid, 2);
                    packet.ReadXORByte(transportGuid, 4);
                    packet.Store("Transport GUID", index);
                    packet.Store("Transport Position", transPos, index);
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

                packet.StoreBitstreamGuid("GUID 2", guid2, index);
                packet.Store("Position", moveInfo.Position, index);
                packet.Store("Orientation", moveInfo.Orientation, index);
            }

            if (hasVehicleData)
            {
                packet.ReadSingle("Vehicle Orientation", index);
                moveInfo.VehicleId = packet.ReadUInt32("Vehicle Id", index);
            }

            if (hasGameObjectPosition)
            {
                var tPos = new Vector4();

                packet.ReadXORByte(goTransportGuid, 0);
                packet.ReadXORByte(goTransportGuid, 5);
                if (hasGOTransportTime3)
                    packet.ReadUInt32("GO Transport Time 3", index);

                packet.ReadXORByte(goTransportGuid, 3);

                tPos.X = packet.ReadSingle();
                packet.ReadXORByte(goTransportGuid, 4);
                packet.ReadXORByte(goTransportGuid, 6);
                packet.ReadXORByte(goTransportGuid, 1);

                packet.ReadSingle("GO Transport Time", index);
                tPos.Y = packet.ReadSingle();
                packet.ReadXORByte(goTransportGuid, 2);
                packet.ReadXORByte(goTransportGuid, 7);

                tPos.Z = packet.ReadSingle();
                packet.ReadByte("GO Transport Seat", index);
                tPos.O = packet.ReadSingle();
                if (hasGOTransportTime2)
                    packet.ReadUInt32("GO Transport Time 2", index);

                packet.StoreBitstreamGuid("GO Transport GUID", goTransportGuid, index);
                packet.Store("GO Transport Position", tPos, index);
            }

            if (hasGameObjectRotation)
                packet.ReadPackedQuaternion("GameObject Rotation", index);

            if (bit456)
            {
                // float[] arr = new float[16];
                // ordering: 13, 4, 7, 15, BYTE, 10, 11, 3, 5, 14, 6, 1, 8, 12, 0, 2, 9
                packet.ReadBytes(4 * 16 + 1);
            }

            if (hasStationaryPosition)
            {
                moveInfo.Orientation = packet.ReadSingle("Stationary Orientation", index);
                moveInfo.Position = packet.ReadVector3("Stationary Position", index);
            }

            if (hasAttackingTarget)
            {
                packet.ParseBitStream(attackingTargetGuid, 4, 0, 3, 5, 7, 6, 2, 1);
                packet.StoreBitstreamGuid("Attacking Target GUID", attackingTargetGuid, index);
            }

            if (hasAnimKits)
            {
                if (hasAnimKit1)
                    packet.ReadUInt16("Anim Kit 1", index);
                if (hasAnimKit2)
                    packet.ReadUInt16("Anim Kit 2", index);
                if (hasAnimKit3)
                    packet.ReadUInt16("Anim Kit 3", index);
            }

            if (transport)
                packet.ReadUInt32("Transport path timer", index);

            return moveInfo;
        }

        private static MovementInfo ReadMovementUpdateBlock433(ref Packet packet, Guid guid, int index)
        {
            var moveInfo = new MovementInfo();

            bool living = packet.ReadBit("Living", index);
            bool hasAttackingTarget = packet.ReadBit("Has Attacking Target", index);
            bool hasVehicleData = packet.ReadBit("Has Vehicle Data", index);
            uint unkLoopCounter = packet.ReadBits(24);
            bool hasStationaryPosition = packet.ReadBit("Has Stationary Position", index);
            /*bool bit1 =*/ packet.ReadBit();
            /*bool bit4 =*/ packet.ReadBit();
            bool unkInt = packet.ReadBit();
            bool unkFloats = packet.ReadBit();
            /*bool bit2 =*/ packet.ReadBit();
            /*bool bit0 =*/ packet.ReadBit();
            /*bool bit3 =*/ packet.ReadBit();
            bool hasGameObjectPosition = packet.ReadBit("Has GameObject Position", index);
            bool hasAnimKits = packet.ReadBit("Has AnimKits", index);
            bool hasGORotation = packet.ReadBit("Has GameObject Rotation", index);
            bool unkFloat1 = false;
            bool hasFallData = false;
            bool unkFloat2 = false;
            bool bit216 = false;
            bool bit256 = false;
            bool hasSplineDurationMult = false;
            SplineType splineType = SplineType.Normal;
            var facingTarget = new byte[8];
            uint splineCount = 0u;
            bool hasTransportData = false;
            var transportGuid = new byte[8];
            bool hasTransportTime2 = false;
            bool hasTransportTime3 = false;
            bool hasFallDirection = false;
            bool hasUnkUInt = false;
            bool hasOrientation = false;
            var attackingTarget = new byte[8];
            var goTransportGuid = new byte[8];
            bool hasGOTransportTime2 = false;
            bool hasGOTransportTime3 = false;
            bool hasAnimKit1 = false;
            bool hasAnimKit2 = false;
            bool hasAnimKit3 = false;
            var guid2 = new byte[8];

            // Reading bits
            if (living)
            {
                guid2[4] = packet.ReadBit();
                /*bool bit149 =*/ packet.ReadBit();
                guid2[5] = packet.ReadBit();
                unkFloat1 = !packet.ReadBit();
                hasFallData = packet.ReadBit("Has Fall Data", index);
                unkFloat2 = !packet.ReadBit();
                guid2[6] = packet.ReadBit();
                moveInfo.HasSplineData = packet.ReadBit("Has Spline Data", index);
                if (moveInfo.HasSplineData)
                {
                    bit216 = packet.ReadBit();
                    if (bit216)
                    {
                        bit256 = packet.ReadBit();
                        /*splineMode =*/ packet.ReadBits(2);
                        hasSplineDurationMult = packet.ReadBit();
                        uint bits57 = packet.ReadBits(2);
                        switch (bits57)
                        {
                            case 0:
                                splineType = SplineType.FacingSpot;
                                break;
                            case 1:
                                splineType = SplineType.Normal;
                                break;
                            case 2:
                                splineType = SplineType.FacingTarget;
                                break;
                            case 3:
                                splineType = SplineType.FacingAngle;
                                break;
                        }

                        if (splineType == SplineType.FacingTarget)
                            facingTarget = packet.StartBitStream(0, 2, 7, 1, 6, 3, 4, 5);

                        /*splineFlags =*/ packet.ReadEnum<SplineFlag422>("Spline Flags", 25, index);
                        splineCount = packet.ReadBits(22);
                    }
                }

                hasTransportData = packet.ReadBit("Has Transport Data", index);
                guid2[1] = packet.ReadBit();
                /*bit148 =*/ packet.ReadBit();
                if (hasTransportData)
                {
                    hasTransportTime2 = packet.ReadBit();
                    transportGuid = packet.StartBitStream(0, 7, 2, 6, 5, 4, 1, 3);
                    hasTransportTime3 = packet.ReadBit();
                }

                guid2[2] = packet.ReadBit();
                if (hasFallData)
                    hasFallDirection = packet.ReadBit("Has Fall Direction", index);

                bool hasMovementFlags = !packet.ReadBit();
                bool hasExtraMovementFlags = !packet.ReadBit();
                hasUnkUInt = !packet.ReadBit();
                guid2[7] = packet.ReadBit();
                if (hasExtraMovementFlags)
                    moveInfo.FlagsExtra = packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12, index);

                guid2[0] = packet.ReadBit();
                if (hasMovementFlags)
                    moveInfo.Flags = packet.ReadEnum<MovementFlag>("Movement Flags", 30, index);

                guid2[3] = packet.ReadBit();
                hasOrientation = !packet.ReadBit();
            }

            if (hasAttackingTarget)
                attackingTarget = packet.StartBitStream(2, 4, 0, 1, 3, 7, 5, 6);

            if (hasGameObjectPosition)
            {
                hasGOTransportTime2 = packet.ReadBit();
                goTransportGuid[1] = packet.ReadBit();
                goTransportGuid[4] = packet.ReadBit();
                goTransportGuid[5] = packet.ReadBit();
                goTransportGuid[0] = packet.ReadBit();
                goTransportGuid[6] = packet.ReadBit();
                goTransportGuid[7] = packet.ReadBit();
                goTransportGuid[3] = packet.ReadBit();
                hasGOTransportTime3 = packet.ReadBit();
                goTransportGuid[2] = packet.ReadBit();
            }

            if (hasAnimKits)
            {
                hasAnimKit3 = !packet.ReadBit();
                hasAnimKit1 = !packet.ReadBit();
                hasAnimKit2 = !packet.ReadBit();
            }

            // Reading data
            packet.StoreBeginList("UnkInts", index);
            for (var i = 0u; i < unkLoopCounter; ++i)
                packet.ReadUInt32("Unk UInt32", index, (int)i);
            packet.StoreEndList();

            if (living)
            {
                moveInfo.WalkSpeed = packet.ReadSingle("Walk Speed", index) / 2.5f;
                if (moveInfo.HasSplineData)
                {
                    if (bit216)
                    {
                        packet.StoreBeginList("Spline Waypoints", index);
                        for (var i = 0; i < splineCount; ++i)
                        {
                            var wp = new Vector3
                            {
                                X = packet.ReadSingle(),
                                Z = packet.ReadSingle(),
                                Y = packet.ReadSingle(),
                            };

                            packet.Store("Spline Waypoint", wp, index, i);
                        }
                        packet.StoreEndList();

                        if (splineType == SplineType.FacingTarget)
                        {
                            packet.ParseBitStream(facingTarget, 0, 6, 5, 4, 1, 3, 7, 2);
                            packet.StoreBitstreamGuid("Facing Target GUID", facingTarget, index);
                        }
                        else if (splineType == SplineType.FacingSpot)
                        {
                            var point = new Vector3
                            {
                                Z = packet.ReadSingle(),
                                Y = packet.ReadSingle(),
                                X = packet.ReadSingle(),
                            };

                            packet.Store("Facing Spot", point, index);
                        }

                        packet.ReadUInt32("Unknown Spline Int32 2", index);
                        if (bit256)
                            packet.ReadUInt32("Unknown Spline Int32 3", index);

                        packet.ReadSingle("Unknown Spline Float 2", index);
                        packet.ReadSingle("Unknown Spline Float 1", index);
                        packet.ReadUInt32("Unknown Spline Int32 1", index);
                        if (splineType == SplineType.FacingAngle)
                            packet.ReadSingle("Facing Angle", index);

                        if (hasSplineDurationMult)
                            packet.ReadSingle("Spline Duration Modifier", index);
                    }

                    var endPoint = new Vector3
                    {
                        Z = packet.ReadSingle(),
                        Y = packet.ReadSingle(),
                    };

                    packet.ReadUInt32("Spline Full Time", index);
                    endPoint.X = packet.ReadSingle();
                    packet.Store("Spline Endpoint", endPoint, index);
                }

                if (hasTransportData)
                {
                    if (hasTransportTime2)
                        packet.ReadInt32("Transport Time 2", index);

                    packet.ReadXORByte(transportGuid, 4);
                    packet.ReadXORByte(transportGuid, 6);
                    packet.ReadXORByte(transportGuid, 5);

                    if (hasTransportTime3)
                        packet.ReadInt32("Transport Time 3", index);

                    packet.ReadXORByte(transportGuid, 7);
                    packet.ReadXORByte(transportGuid, 3);

                    var tPos = new Vector4
                    {
                        X = packet.ReadSingle(),
                        Z = packet.ReadSingle(),
                        O = packet.ReadSingle(),
                    };

                    packet.ReadXORByte(transportGuid, 2);
                    packet.ReadXORByte(transportGuid, 1);
                    packet.ReadXORByte(transportGuid, 0);

                    tPos.Y = packet.ReadSingle();
                    packet.Store("Transport Position", tPos, index);
                    packet.ReadByte("Transport Seat", index);
                    packet.ReadInt32("Transport Time", index);
                }

                if (unkFloat1)
                    packet.ReadSingle("float +28", index);

                packet.ReadSingle("FlyBack Speed", index);
                packet.ReadSingle("Turn Speed", index);
                packet.ReadXORByte(guid2, 5);

                moveInfo.RunSpeed = packet.ReadSingle("Run Speed", index) / 7.0f;
                if (unkFloat2)
                    packet.ReadSingle("float +36", index);

                packet.ReadXORByte(guid2, 0);

                packet.ReadSingle("Pitch Speed", index);
                if (hasFallData)
                {
                    packet.ReadInt32("Time Fallen", index);
                    packet.ReadSingle("Fall Start Velocity", index);
                    if (hasFallDirection)
                    {
                        packet.ReadSingle("Jump Sin", index);
                        packet.ReadSingle("Jump Velocity", index);
                        packet.ReadSingle("Jump Cos", index);
                    }
                }

                packet.ReadSingle("RunBack Speed", index);
                moveInfo.Position = new Vector3();
                moveInfo.Position.X = packet.ReadSingle();
                packet.ReadSingle("SwimBack Speed", index);
                packet.ReadXORByte(guid2, 7);

                moveInfo.Position.Z = packet.ReadSingle();
                packet.ReadXORByte(guid2, 3);
                packet.ReadXORByte(guid2, 2);

                packet.ReadSingle("Fly Speed", index);
                packet.ReadSingle("Swim Speed", index);
                packet.ReadXORByte(guid2, 1);
                packet.ReadXORByte(guid2, 4);
                packet.ReadXORByte(guid2, 6);

                packet.StoreBitstreamGuid("GUID 2", guid2, index);
                moveInfo.Position.Y = packet.ReadSingle();
                if (hasUnkUInt)
                    packet.ReadUInt32();

                if (hasOrientation)
                    moveInfo.Orientation = packet.ReadSingle();

                packet.Store("Position", moveInfo.Position, index);
                packet.Store("Orientation", moveInfo.Orientation, index);
            }

            if (unkFloats)
            {
                int i;
                packet.StoreBeginList("UnkFloats1", index);
                for (i = 0; i < 13; ++i)
                    packet.ReadSingle("Unk float 456", index, i);
                packet.StoreEndList();

                packet.ReadByte("Unk byte 456", index);

                packet.StoreBeginList("UnkFloats2", index);
                for (; i < 16; ++i)
                    packet.ReadSingle("Unk float 456", index, i);
                packet.StoreEndList();
            }

            if (hasGameObjectPosition)
            {
                var tPos = new Vector4();

                packet.ReadXORByte(goTransportGuid, 6);
                packet.ReadXORByte(goTransportGuid, 5);

                tPos.Y = packet.ReadSingle();
                packet.ReadXORByte(goTransportGuid, 4);
                packet.ReadXORByte(goTransportGuid, 2);
                if (hasGOTransportTime3)
                    packet.ReadUInt32("GO Transport Time 3", index);

                tPos.O = packet.ReadSingle();
                tPos.Z = packet.ReadSingle();
                if (hasGOTransportTime2)
                    packet.ReadUInt32("GO Transport Time 2", index);

                packet.ReadByte("GO Transport Seat", index);
                packet.ReadXORByte(goTransportGuid, 7);
                packet.ReadXORByte(goTransportGuid, 1);
                packet.ReadXORByte(goTransportGuid, 0);
                packet.ReadXORByte(goTransportGuid, 3);

                tPos.X = packet.ReadSingle();
                packet.ReadSingle("GO Transport Time", index);
                packet.Store("GO Transport Position", tPos, index);
                packet.StoreBitstreamGuid("GO Transport GUID", goTransportGuid, index);
            }

            if (hasAttackingTarget)
            {
                packet.ParseBitStream(attackingTarget, 2, 4, 7, 3, 0, 1, 5, 6);
                packet.StoreBitstreamGuid("Attacking Target GUID", attackingTarget, index);
            }

            if (hasGORotation)
                moveInfo.Rotation = packet.ReadPackedQuaternion("GO Rotation", index);

            if (unkInt)
                packet.ReadUInt32("uint32 +412", index);

            if (hasAnimKits)
            {
                if (hasAnimKit3)
                    packet.ReadUInt16("Anim Kit 3", index);
                if (hasAnimKit1)
                    packet.ReadUInt16("Anim Kit 1", index);
                if (hasAnimKit2)
                    packet.ReadUInt16("Anim Kit 2", index);
            }

            if (hasStationaryPosition)
            {
                moveInfo.Position = new Vector3
                {
                    Z = packet.ReadSingle(),
                    X = packet.ReadSingle(),
                    Y = packet.ReadSingle(),
                };

                moveInfo.Orientation = packet.ReadSingle();
                packet.Store("Stationary Position", moveInfo.Position, index);
                packet.Store("Stationary Orientation", moveInfo.Orientation, index);
            }

            if (hasVehicleData)
            {
                packet.ReadSingle("Vehicle Orientation", index);
                moveInfo.VehicleId = packet.ReadUInt32("Vehicle Id", index);
            }

            packet.ResetBitReader();
            return moveInfo;
        }

        private static MovementInfo ReadMovementUpdateBlock432(ref Packet packet, Guid guid, int index)
        {
            var moveInfo = new MovementInfo();

            /*bool bit2 = */packet.ReadBit();
            /*bool bit3 = */packet.ReadBit();
            /*bool bit4 = */packet.ReadBit();
            var hasStationaryPosition = packet.ReadBit("Has Stationary Position", index);
            var hasAnimKits = packet.ReadBit("Has AnimKits", index);
            var unkLoopCounter = packet.ReadBits(24);
            /*bool bit1 = */packet.ReadBit();
            bool hasTransportExtra = packet.ReadBit("Has Transport Extra", index);
            bool hasGORotation = packet.ReadBit("Has GameObject Rotation", index);
            bool living = packet.ReadBit("Living", index);
            bool hasGameObjectPosition = packet.ReadBit("Has GameObject Position", index);
            bool hasVehicleData = packet.ReadBit("Has Vehicle Data", index);
            bool hasAttackingTarget = packet.ReadBit("Has Attacking Target", index);
            /*bool bit0 =*/packet.ReadBit();
            bool unkFloats = packet.ReadBit();

            bool unkFloat1 = false;
            bool hasFallData = false;
            bool unkFloat2 = false;
            bool bit216 = false;
            bool bit256 = false;
            bool hasSplineDurationMult = false;
            SplineType splineType = SplineType.Normal;
            var facingTarget = new byte[8];
            uint splineCount = 0u;
            bool hasTransportData = false;
            var transportGuid = new byte[8];
            bool hasTransportTime2 = false;
            bool hasTransportTime3 = false;
            bool hasFallDirection = false;
            bool hasUnkUInt = false;
            bool hasOrientation = false;
            var attackingTarget = new byte[8];
            var goTransportGuid = new byte[8];
            bool hasGOTransportTime2 = false;
            bool hasGOTransportTime3 = false;
            bool hasAnimKit1 = false;
            bool hasAnimKit2 = false;
            bool hasAnimKit3 = false;
            var guid2 = new byte[8];

            if (living)
            {
                unkFloat1 = !packet.ReadBit();
                hasOrientation = !packet.ReadBit();
                bool hasExtraMovementFlags = !packet.ReadBit();
                hasFallData = packet.ReadBit("Has Fall Data", index);
                guid2[0] = packet.ReadBit();
                guid2[5] = packet.ReadBit();
                guid2[4] = packet.ReadBit();
                bool hasMovementFlags = !packet.ReadBit();
                moveInfo.HasSplineData = packet.ReadBit("Has Spline Data", index);
                /*bool bit148 = */packet.ReadBit();

                if (hasExtraMovementFlags)
                    moveInfo.FlagsExtra = packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12, index);

                hasUnkUInt = !packet.ReadBit();
                guid2[3] = packet.ReadBit();
                /*bool bit149 = */packet.ReadBit();

                if (hasMovementFlags)
                    moveInfo.Flags = packet.ReadEnum<MovementFlag>("Movement Flags", 30, index);

                guid2[1] = packet.ReadBit();
                unkFloat2 = !packet.ReadBit();
                hasTransportData = packet.ReadBit("Has Transport Data", index);
                guid2[2] = packet.ReadBit();

                if (hasTransportData)
                {
                    transportGuid[3] = packet.ReadBit();
                    transportGuid[5] = packet.ReadBit();
                    transportGuid[1] = packet.ReadBit();
                    transportGuid[7] = packet.ReadBit();
                    hasTransportTime2 = packet.ReadBit();
                    transportGuid[4] = packet.ReadBit();
                    transportGuid[0] = packet.ReadBit();
                    transportGuid[2] = packet.ReadBit();
                    transportGuid[6] = packet.ReadBit();
                    hasTransportTime3 = packet.ReadBit();
                }

                if (moveInfo.HasSplineData)
                {
                    bit216 = packet.ReadBit();
                    if (bit216)
                    {
                        uint bits57 = packet.ReadBits(2);
                        splineCount = packet.ReadBits(22);
                        switch (bits57)
                        {
                            case 0:
                                splineType = SplineType.FacingTarget;
                                break;
                            case 1:
                                splineType = SplineType.FacingSpot;
                                break;
                            case 2:
                                splineType = SplineType.Normal;
                                break;
                            case 3:
                                splineType = SplineType.FacingAngle;
                                break;
                        }

                        if (splineType == SplineType.FacingTarget)
                            facingTarget = packet.StartBitStream(4, 3, 2, 5, 7, 1, 0, 6);

                        packet.ReadEnum<SplineFlag422>("Spline flags", 25, index);
                        /*splineMode =*/packet.ReadBits(2);
                        hasSplineDurationMult = packet.ReadBit("HasSplineDurationMult", index);
                        bit256 = packet.ReadBit();
                    }
                }

                if (hasFallData)
                    hasFallDirection = packet.ReadBit("Has Fall Direction", index);

                guid2[6] = packet.ReadBit();
                guid2[7] = packet.ReadBit();
            }

            if (hasGameObjectPosition)
            {
                goTransportGuid[5] = packet.ReadBit();
                goTransportGuid[4] = packet.ReadBit();
                hasGOTransportTime3 = packet.ReadBit();
                goTransportGuid[7] = packet.ReadBit();
                goTransportGuid[6] = packet.ReadBit();
                goTransportGuid[1] = packet.ReadBit();
                goTransportGuid[2] = packet.ReadBit();
                hasGOTransportTime2 = packet.ReadBit();
                goTransportGuid[0] = packet.ReadBit();
                goTransportGuid[3] = packet.ReadBit();
            }

            if (hasAnimKits)
            {
                hasAnimKit1 = !packet.ReadBit();
                hasAnimKit3 = !packet.ReadBit();
                hasAnimKit2 = !packet.ReadBit();
            }

            if (hasAttackingTarget)
                attackingTarget = packet.StartBitStream(4, 3, 2, 5, 0, 6, 1, 7);

            packet.StoreBeginList("UnkInts", index);
            for (var i = 0; i < unkLoopCounter; ++i)
            {
                packet.ReadInt32("UnkInt");
            }
            packet.StoreEndList();

            if (hasGameObjectPosition)
            {
                var tPos = new Vector4();

                if (hasGOTransportTime3)
                    packet.ReadInt32("GO Transport Time 3", index);

                packet.ReadXORByte(goTransportGuid, 7);

                tPos.Y = packet.ReadSingle();
                packet.ReadByte("GO Transport Seat", index);
                tPos.O = packet.ReadSingle();
                tPos.Z = packet.ReadSingle();

                packet.ReadXORByte(goTransportGuid, 4);
                packet.ReadXORByte(goTransportGuid, 5);
                packet.ReadXORByte(goTransportGuid, 6);

                tPos.X = packet.ReadSingle();
                packet.ReadInt32("GO Transport Time", index);

                packet.ReadXORByte(goTransportGuid, 1);

                if (hasGOTransportTime2)
                    packet.ReadInt32("GO Transport Time 2", index);

                packet.ReadXORByte(goTransportGuid, 0);
                packet.ReadXORByte(goTransportGuid, 2);
                packet.ReadXORByte(goTransportGuid, 3);

                packet.Store("GO Transport Position", tPos, index);
                packet.StoreBitstreamGuid("GO Transport GUID", goTransportGuid, index);
            }

            if (living)
            {
                if (moveInfo.HasSplineData)
                {
                    if (bit216)
                    {
                        packet.ReadSingle("Unknown Spline Float 2", index);
                        packet.StoreBeginList("Spline Waypoints", index);
                        for (var i = 0; i < splineCount; ++i)
                        {
                            var wp = new Vector3
                            {
                                Y = packet.ReadSingle(),
                                Z = packet.ReadSingle(),
                                X = packet.ReadSingle(),
                            };

                            packet.Store("Spline Waypoint", wp, index, i);
                        }
                        packet.StoreEndList();

                        if (splineType == SplineType.FacingTarget)
                        {
                            packet.ParseBitStream(facingTarget, 2, 1, 3, 7, 0, 5, 4, 6);
                            packet.StoreBitstreamGuid("Facing Target GUID", facingTarget, index);
                        }
                        else if (splineType == SplineType.FacingSpot)
                        {
                            var point = new Vector3
                            {
                                Y = packet.ReadSingle(),
                                Z = packet.ReadSingle(),
                                X = packet.ReadSingle(),
                            };

                            packet.Store("Facing Spot", point, index);
                        }

                        if (hasSplineDurationMult)
                            packet.ReadSingle("Spline Duration Modifier", index);

                        if (bit256)
                            packet.ReadUInt32("Unknown Spline Int32 1", index);

                        packet.ReadUInt32("Unknown Spline Int32 2", index);
                        packet.ReadSingle("Unknown Spline Float 1", index);
                        if (splineType == SplineType.FacingAngle)
                            packet.ReadSingle("Facing Angle", index);

                        packet.ReadUInt32("Unknown Spline Int32 3", index);
                    }

                    packet.ReadUInt32("Spline Full Time", index);
                    var endPoint = new Vector3
                    {
                        Z = packet.ReadSingle(),
                        Y = packet.ReadSingle(),
                        X = packet.ReadSingle(),
                    };

                    packet.Store("Spline Endpoint", endPoint, index);
                }

                if (hasTransportData)
                {
                    var tPos = new Vector4();

                    packet.ReadXORByte(transportGuid, 6);
                    if (hasTransportTime2)
                        packet.ReadInt32("Transport Time 2", index);

                    packet.ReadByte("Transport Seat", index);
                    tPos.O = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 7);
                    tPos.Y = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 3);
                    if (hasTransportTime3)
                        packet.ReadInt32("Transport Time 3", index);

                    packet.ReadInt32("Transport Time", index);
                    packet.ReadXORByte(transportGuid, 0);
                    packet.ReadXORByte(transportGuid, 1);
                    tPos.X = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 4);
                    tPos.Z = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 5);
                    packet.ReadXORByte(transportGuid, 2);

                    packet.Store("Transport Position", tPos, index);
                }

                moveInfo.Position = new Vector3();
                moveInfo.Position.Z = packet.ReadSingle();
                packet.ReadSingle("FlyBack Speed", index);
                moveInfo.Position.Y = packet.ReadSingle();
                packet.ReadXORByte(guid2, 4);
                packet.ReadXORByte(guid2, 0);
                moveInfo.Position.X = packet.ReadSingle();
                if (hasFallData)
                {
                    packet.ReadInt32("Time Fallen", index);
                    if (hasFallDirection)
                    {
                        packet.ReadSingle("Jump Sin", index);
                        packet.ReadSingle("Jump Velocity", index);
                        packet.ReadSingle("Jump Cos", index);
                    }
                    packet.ReadSingle("Fall Start Velocity", index);
                }

                if (hasOrientation)
                    moveInfo.Orientation = packet.ReadSingle();

                packet.Store("Position", moveInfo.Position, index);
                packet.Store("Orientation", moveInfo.Orientation, index);
                packet.ReadSingle("Swim Speed", index);
                moveInfo.RunSpeed = packet.ReadSingle("Run Speed", index) / 7.0f;
                packet.ReadSingle("Fly Speed", index);
                packet.ReadXORByte(guid2, 2);
                if (unkFloat2)
                    packet.ReadSingle("Unk float +36", index);

                if (unkFloat1)
                    packet.ReadSingle("Unk float +28", index);

                packet.ReadXORByte(guid2, 3);
                packet.ReadSingle("RunBack Speed", index);
                packet.ReadXORByte(guid2, 6);
                packet.ReadSingle("Pitch Speed", index);
                packet.ReadXORByte(guid2, 7);
                packet.ReadXORByte(guid2, 5);
                packet.ReadSingle("Turn Speed", index);
                packet.ReadSingle("SwimBack Speed", index);
                packet.ReadXORByte(guid2, 1);
                packet.StoreBitstreamGuid("GUID 2", guid2, index);
                if (hasUnkUInt)
                    packet.ReadInt32();

                moveInfo.WalkSpeed = packet.ReadSingle("Walk Speed", index) / 2.5f;
            }

            if (hasAttackingTarget)
            {
                packet.ParseBitStream(attackingTarget, 6, 5, 3, 2, 0, 1, 7, 4);
                packet.StoreBitstreamGuid("Attacking Target GUID", attackingTarget, index);
            }

            if (unkFloats)
            {
                int i;
                packet.StoreBeginList("UnkFloats1", index);
                for (i = 0; i < 13; ++i)
                    packet.ReadSingle("Unk float 456", index, i);
                packet.StoreEndList();

                packet.ReadByte("Unk byte 456", index);

                packet.StoreBeginList("UnkFloats2", index);
                for (; i < 16; ++i)
                    packet.ReadSingle("Unk float 456", index, i);
                packet.StoreEndList();
            }

            if (hasVehicleData)
            {
                packet.ReadSingle("Vehicle Orientation", index);
                moveInfo.VehicleId = packet.ReadUInt32("Vehicle Id", index);
            }

            if (hasGORotation)
                moveInfo.Rotation = packet.ReadPackedQuaternion("GO Rotation", index);

            if (hasStationaryPosition)
            {
                moveInfo.Position = new Vector3
                {
                    X = packet.ReadSingle(),
                    Z = packet.ReadSingle(),
                    Y = packet.ReadSingle(),
                };

                moveInfo.Orientation = packet.ReadSingle();
                packet.Store("Stationary Position", moveInfo.Position, index);
                packet.Store("Stationary Orientation", moveInfo.Orientation, index);
            }

            if (hasAnimKits)
            {
                if (hasAnimKit3)
                    packet.ReadUInt16("Anim Kit 3", index);
                if (hasAnimKit1)
                    packet.ReadUInt16("Anim Kit 1", index);
                if (hasAnimKit2)
                    packet.ReadUInt16("Anim Kit 2", index);
            }

            if (hasTransportExtra)
                packet.ReadInt32("Transport Time", index);

            packet.ResetBitReader();
            return moveInfo;
        }

        private static MovementInfo ReadMovementUpdateBlock430(ref Packet packet, Guid guid, int index)
        {
            var moveInfo = new MovementInfo();
            bool hasAttackingTarget = packet.ReadBit("Has Attacking Target", index);
            /*bool bit2 = */packet.ReadBit();
            bool hasVehicleData = packet.ReadBit("Has Vehicle Data", index);
            /*bool bit1 = */packet.ReadBit();
            /*bool bit4 = */packet.ReadBit();
            /*bool bit3 = */packet.ReadBit();
            bool hasTransportExtra = packet.ReadBit("Has Transport Extra", index);
            bool hasGameObjectPosition = packet.ReadBit("Has GameObject Position", index);
            bool unkFloats = packet.ReadBit();
            bool hasAnimKits = packet.ReadBit("Has AnimKits", index);
            bool hasGORotation = packet.ReadBit("Has GameObject Rotation", index);
            bool living = packet.ReadBit("Living", index);
            bool hasStationaryPosition = packet.ReadBit("Has Stationary Position", index);
            uint unkLoopCounter = packet.ReadBits(24);
            /*bool bit0 = */packet.ReadBit();

            bool unkFloat1 = false;
            bool hasFallData = false;
            bool unkFloat2 = false;
            bool bit216 = false;
            bool bit256 = false;
            bool hasSplineDurationMult = false;
            SplineType splineType = SplineType.Normal;
            var facingTarget = new byte[8];
            uint splineCount = 0u;
            bool hasTransportData = false;
            var transportGuid = new byte[8];
            bool hasTransportTime2 = false;
            bool hasTransportTime3 = false;
            bool hasFallDirection = false;
            bool hasUnkUInt = false;
            bool hasOrientation = false;
            var attackingTarget = new byte[8];
            var goTransportGuid = new byte[8];
            bool hasGOTransportTime2 = false;
            bool hasGOTransportTime3 = false;
            bool hasAnimKit1 = false;
            bool hasAnimKit2 = false;
            bool hasAnimKit3 = false;
            var guid2 = new byte[8];

            if (living)
            {
                hasTransportData = packet.ReadBit("Has Transport Data", index);
                if (hasTransportData)
                {
                    transportGuid[2] = packet.ReadBit();
                    transportGuid[7] = packet.ReadBit();
                    transportGuid[5] = packet.ReadBit();
                    hasTransportTime3 = packet.ReadBit();
                    transportGuid[3] = packet.ReadBit();
                    transportGuid[0] = packet.ReadBit();
                    transportGuid[4] = packet.ReadBit();
                    transportGuid[1] = packet.ReadBit();
                    hasTransportTime2 = packet.ReadBit();
                    transportGuid[6] = packet.ReadBit();
                }

                moveInfo.HasSplineData = packet.ReadBit("Has Spline Data", index);
                guid2[7] = packet.ReadBit();
                guid2[6] = packet.ReadBit();
                guid2[5] = packet.ReadBit();
                guid2[2] = packet.ReadBit();
                guid2[4] = packet.ReadBit();
                bool hasMovementFlags = !packet.ReadBit();
                guid2[1] = packet.ReadBit();
                /*bool bit148 = */packet.ReadBit();
                hasUnkUInt = !packet.ReadBit();
                bool hasExtraMovementFlags = !packet.ReadBit();
                if (moveInfo.HasSplineData)
                {
                    bit216 = packet.ReadBit();
                    if (bit216)
                    {
                        bit256 = packet.ReadBit();
                        /*splineFlags = */packet.ReadEnum<SplineFlag422>("Spline flags", 25, index);
                        /*splineMode = */packet.ReadBits(2);
                        hasSplineDurationMult = packet.ReadBit();
                        splineCount = packet.ReadBits(22);
                        uint bits57 = packet.ReadBits(2);
                        switch (bits57)
                        {
                            case 0:
                                splineType = SplineType.FacingSpot;
                                break;
                            case 1:
                                splineType = SplineType.Normal;
                                break;
                            case 2:
                                splineType = SplineType.FacingTarget;
                                break;
                            case 3:
                                splineType = SplineType.FacingAngle;
                                break;
                        }

                        if (splineType == SplineType.FacingTarget)
                            facingTarget = packet.StartBitStream(7, 3, 4, 2, 1, 6, 0, 5);
                    }
                }

                guid2[3] = packet.ReadBit();
                if (hasMovementFlags)
                    moveInfo.Flags = packet.ReadEnum<MovementFlag>("Movement Flags", 30, index);

                unkFloat1 = !packet.ReadBit();
                hasFallData = packet.ReadBit("Has Fall Data", index);
                if (hasExtraMovementFlags)
                    moveInfo.FlagsExtra = packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12, index);

                guid2[0] = packet.ReadBit();
                hasOrientation = !packet.ReadBit();
                if (hasFallData)
                    hasFallDirection = packet.ReadBit("Has Fall Direction", index);

                unkFloat2 = !packet.ReadBit();
            }

            if (hasGameObjectPosition)
            {
                goTransportGuid[1] = packet.ReadBit();
                hasGOTransportTime3 = packet.ReadBit();
                goTransportGuid[3] = packet.ReadBit();
                goTransportGuid[2] = packet.ReadBit();
                goTransportGuid[6] = packet.ReadBit();
                goTransportGuid[5] = packet.ReadBit();
                goTransportGuid[0] = packet.ReadBit();
                goTransportGuid[4] = packet.ReadBit();
                hasGOTransportTime2 = packet.ReadBit();
                goTransportGuid[7] = packet.ReadBit();
            }

            if (hasAnimKits)
            {
                hasAnimKit3 = !packet.ReadBit();
                hasAnimKit1 = !packet.ReadBit();
                hasAnimKit2 = !packet.ReadBit();
            }

            if (hasAttackingTarget)
                attackingTarget = packet.StartBitStream(3, 4, 6, 0, 1, 7, 5, 2);

            // Reading data
            packet.StoreBeginList("Unks", index);
            for (var i = 0u; i < unkLoopCounter; ++i)
                packet.ReadUInt32("Unk UInt32", index, (int)i);
            packet.StoreEndList();

            if (hasStationaryPosition)
            {
                moveInfo.Position = new Vector3();
                moveInfo.Position.Z = packet.ReadSingle();
                moveInfo.Orientation = packet.ReadSingle();
                moveInfo.Position.X = packet.ReadSingle();
                moveInfo.Position.Y = packet.ReadSingle();
                packet.Store("Stationary Position", moveInfo.Position, index);
                packet.Store("Stationary Orientation", moveInfo.Orientation, index);
            }

            if (hasVehicleData)
            {
                moveInfo.VehicleId = packet.ReadUInt32("Vehicle Id", index);
                packet.ReadSingle("Vehicle Orientation", index);
            }

            if (hasGameObjectPosition)
            {
                var tPos = new Vector4();
                packet.ReadXORByte(goTransportGuid, 1);
                packet.ReadXORByte(goTransportGuid, 4);
                tPos.Z = packet.ReadSingle();
                if (hasGOTransportTime3)
                    packet.ReadInt32("GO Transport Time 3", index);

                packet.ReadInt32("GO Transport Time", index);
                packet.ReadXORByte(goTransportGuid, 5);
                packet.ReadXORByte(goTransportGuid, 6);
                tPos.X = packet.ReadSingle();
                packet.ReadXORByte(goTransportGuid, 2);
                if (hasGOTransportTime2)
                    packet.ReadInt32("GO Transport Time 2", index);

                packet.ReadByte("GO Transport Seat", index);
                packet.ReadXORByte(goTransportGuid, 3);
                tPos.Y = packet.ReadSingle();
                tPos.O = packet.ReadSingle();
                packet.ReadXORByte(goTransportGuid, 7);
                packet.ReadXORByte(goTransportGuid, 0);

                packet.Store("GO Transport Position", tPos, index);
                packet.StoreBitstreamGuid("GO Transport GUID", goTransportGuid, index);
            }

            if (living)
            {
                if (moveInfo.HasSplineData)
                {
                    if (bit216)
                    {
                        packet.StoreBeginList("Spline Waypoints", index);
                        for (int i = 0; i < splineCount; ++i)
                        {
                            var wp = new Vector3
                            {
                                Y = packet.ReadSingle(),
                                X = packet.ReadSingle(),
                                Z = packet.ReadSingle(),
                            };

                            packet.Store("Spline Waypoint", wp, index, i);
                        }
                        packet.StoreEndList();

                        if (hasSplineDurationMult)
                            packet.ReadSingle("Spline Duration Modifier", index);

                        packet.ReadSingle("Unknown Spline Float 2", index);
                        if (splineType == SplineType.FacingTarget)
                        {
                            packet.ParseBitStream(facingTarget, 3, 4, 5, 7, 2, 0, 6, 1);
                            packet.StoreBitstreamGuid("Facing Target GUID", facingTarget, index);
                        }

                        if (bit256)
                            packet.ReadUInt32("Unknown Spline Int32 3", index);

                        packet.ReadSingle("Unknown Spline Float 1", index);
                        packet.ReadUInt32("Unknown Spline Int32 1", index);
                        if (splineType == SplineType.FacingSpot)
                        {
                            var point = new Vector3
                            {
                                Y = packet.ReadSingle(),
                                Z = packet.ReadSingle(),
                                X = packet.ReadSingle(),
                            };

                            packet.Store("Facing Spot", point, index);
                        }

                        packet.ReadUInt32("Unknown Spline Int32 2", index);
                        if (splineType == SplineType.FacingAngle)
                            packet.ReadSingle("Facing Angle", index);
                    }

                    var endPoint = new Vector3
                    {
                        Z = packet.ReadSingle(),
                        Y = packet.ReadSingle(),
                    };

                    packet.ReadUInt32("Spline Full Time", index);
                    endPoint.X = packet.ReadSingle();
                    packet.Store("Spline Endpoint", endPoint, index);
                }

                packet.ReadSingle("Pitch Speed", index);
                if (hasTransportData)
                {
                    var tPos = new Vector4();
                    packet.ReadXORByte(transportGuid, 4);
                    tPos.Z = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 7);
                    packet.ReadXORByte(transportGuid, 5);
                    packet.ReadXORByte(transportGuid, 1);
                    tPos.X = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 3);
                    packet.ReadXORByte(transportGuid, 6);
                    if (hasTransportTime3)
                        packet.ReadInt32("Transport Time 3", index);

                    tPos.Y = packet.ReadSingle();
                    packet.ReadByte("Transport Seat", index);
                    tPos.O = packet.ReadSingle();
                    if (hasTransportTime2)
                        packet.ReadInt32("Transport Time 2", index);

                    packet.ReadXORByte(transportGuid, 2);
                    packet.ReadInt32("Transport Time", index);
                    packet.ReadXORByte(transportGuid, 0);

                    packet.Store("Transport Position", tPos, index);
                }

                packet.ReadSingle("FlyBack Speed", index);
                moveInfo.Position = new Vector3();
                moveInfo.Position.X = packet.ReadSingle();
                if (unkFloat1)
                    packet.ReadSingle("Unk float +28", index);

                if (hasFallData)
                {
                    packet.ReadInt32("Time Fallen", index);
                    if (hasFallDirection)
                    {
                        packet.ReadSingle("Jump Sin", index);
                        packet.ReadSingle("Jump Velocity", index);
                        packet.ReadSingle("Jump Cos", index);
                    }
                    packet.ReadSingle("Fall Start Velocity", index);
                }

                packet.ReadXORByte(guid2, 7);
                packet.ReadSingle("SwimBack Speed", index);
                packet.ReadXORByte(guid2, 0);
                packet.ReadXORByte(guid2, 5);
                if (hasUnkUInt)
                    packet.ReadUInt32();

                moveInfo.Position.Z = packet.ReadSingle();
                packet.ReadSingle("Fly Speed", index);
                packet.ReadXORByte(guid2, 1);
                packet.ReadSingle("RunBack Speed", index);
                packet.ReadSingle("Turn Speed", index);
                packet.ReadSingle("Swim Speed", index);
                moveInfo.WalkSpeed = packet.ReadSingle("Walk Speed", index) / 2.5f;
                packet.ReadXORByte(guid2, 3);
                packet.ReadXORByte(guid2, 4);
                packet.ReadXORByte(guid2, 2);
                packet.ReadXORByte(guid2, 6);
                packet.StoreBitstreamGuid("GUID 2", guid2, index);
                if (unkFloat2)
                    packet.ReadSingle("Unk float +36", index);

                moveInfo.Position.Y = packet.ReadSingle();
                if (hasOrientation)
                    moveInfo.Orientation = packet.ReadSingle();

                moveInfo.RunSpeed = packet.ReadSingle("Run Speed", index) / 7.0f;
                packet.Store("Position", moveInfo.Position, index);
                packet.Store("Orientation", moveInfo.Orientation, index);
            }

            if (unkFloats)
            {
                packet.StoreBeginList("UnkFloats", index);
                for (int i = 0; i < 16; ++i)
                    packet.ReadSingle("Unk float 456", index, i);
                packet.StoreEndList();
                packet.ReadByte("Unk byte 456", index);
            }

            if (hasTransportExtra)
                packet.ReadInt32("Transport Time", index);

            if (hasAnimKits)
            {
                if (hasAnimKit2)
                    packet.ReadUInt16("Anim Kit 2", index);
                if (hasAnimKit3)
                    packet.ReadUInt16("Anim Kit 3", index);
                if (hasAnimKit1)
                    packet.ReadUInt16("Anim Kit 1", index);
            }

            if (hasGORotation)
                moveInfo.Rotation = packet.ReadPackedQuaternion("GO Rotation", index);

            if (hasAttackingTarget)
            {
                packet.ParseBitStream(attackingTarget, 3, 5, 0, 7, 2, 4, 6, 1);
                packet.StoreBitstreamGuid("Attacking Target GUID", attackingTarget, index);
            }

            packet.ResetBitReader();
            return moveInfo;
        }

        private static MovementInfo ReadMovementUpdateBlock(ref Packet packet, Guid guid, int index)
        {
            if (ClientVersion.Build == ClientVersionBuild.V4_3_4_15595)
                return ReadMovementUpdateBlock434(ref packet, guid, index);

            if (ClientVersion.Build == ClientVersionBuild.V4_3_3_15354)
                return ReadMovementUpdateBlock433(ref packet, guid, index);

            if (ClientVersion.Build == ClientVersionBuild.V4_3_2_15211)
                return ReadMovementUpdateBlock432(ref packet, guid, index);

            if (ClientVersion.Build == ClientVersionBuild.V4_3_0_15005)
                return ReadMovementUpdateBlock430(ref packet, guid, index);

            var moveInfo = new MovementInfo();

            var flagsTypeCode = ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767) ? TypeCode.UInt16 : TypeCode.Byte;
            var flags = packet.ReadEnum<UpdateFlag>("Update Flags", flagsTypeCode, index);

            if (flags.HasAnyFlag(UpdateFlag.Living))
            {
                moveInfo = MovementHandler.ReadMovementInfo(ref packet, guid, index);
                var moveFlags = moveInfo.Flags;

                packet.StoreBeginList("Speeds", index);
                for (var i = 0; i < 9; ++i)
                {
                    var speedType = (SpeedType)i;
                    var speed = packet.ReadSingle(speedType + " Speed", index, i);

                    switch (speedType)
                    {
                        case SpeedType.Walk:
                        {
                            moveInfo.WalkSpeed = speed / 2.5f;
                            break;
                        }
                        case SpeedType.Run:
                        {
                            moveInfo.RunSpeed = speed / 7.0f;
                            break;
                        }
                    }
                }
                packet.StoreEndList();

                // Movement flags seem incorrect for 4.2.2
                // guess in which version they stopped checking movement flag and used bits
                if ((ClientVersion.RemovedInVersion(ClientVersionBuild.V4_2_0_14333) && moveFlags.HasAnyFlag(MovementFlag.SplineEnabled)) || moveInfo.HasSplineData)
                {
                    // Temp solution
                    // TODO: Make Enums version friendly
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_0_14333))
                    {
                        var splineFlags422 = packet.ReadEnum<SplineFlag422>("Spline Flags", TypeCode.Int32, index);
                        if (splineFlags422.HasAnyFlag(SplineFlag422.FinalOrientation))
                        {
                            packet.ReadSingle("Final Spline Orientation", index);
                        }
                        else
                        {
                            if (splineFlags422.HasAnyFlag(SplineFlag422.FinalTarget))
                                packet.ReadGuid("Final Spline Target GUID", index);
                            else if (splineFlags422.HasAnyFlag(SplineFlag422.FinalPoint))
                                packet.ReadVector3("Final Spline Coords", index);
                        }
                    }
                    else
                    {
                        var splineFlags = packet.ReadEnum<SplineFlag>("Spline Flags", TypeCode.Int32, index);
                        if (splineFlags.HasAnyFlag(SplineFlag.FinalTarget))
                            packet.ReadGuid("Final Spline Target GUID", index);
                        else if (splineFlags.HasAnyFlag(SplineFlag.FinalOrientation))
                            packet.ReadSingle("Final Spline Orientation", index);
                        else if (splineFlags.HasAnyFlag(SplineFlag.FinalPoint))
                            packet.ReadVector3("Final Spline Coords", index);
                    }

                    packet.ReadInt32("Spline Time", index);
                    packet.ReadInt32("Spline Full Time", index);
                    packet.ReadInt32("Spline ID", index);

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                    {
                        packet.ReadSingle("Spline Duration Multiplier", index);
                        packet.ReadSingle("Spline Duration Multiplier Next", index);
                        packet.ReadSingle("Spline Vertical Acceleration", index);
                        packet.ReadInt32("Spline Start Time", index);
                    }

                    var splineCount = packet.ReadInt32();
                    packet.StoreBeginList("Spline waypoints", index);
                    for (var i = 0; i < splineCount; i++)
                        packet.ReadVector3("Spline Waypoint", index, i);
                    packet.StoreEndList();

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                        packet.ReadEnum<SplineMode>("Spline Mode", TypeCode.Byte, index);

                    packet.ReadVector3("Spline Endpoint", index);
                }
            }
            else // !UpdateFlag.Living
            {
                if (flags.HasAnyFlag(UpdateFlag.GOPosition))
                {
                    packet.ReadPackedGuid("GO Position GUID", index);

                    moveInfo.Position = packet.ReadVector3("GO Position", index);
                    packet.ReadVector3("GO Transport Position", index);

                    moveInfo.Orientation = packet.ReadSingle("GO Orientation", index);
                    packet.ReadSingle("Corpse Orientation", index);
                }
                else if (flags.HasAnyFlag(UpdateFlag.StationaryObject))
                {
                    moveInfo.Position = packet.ReadVector3();
                    moveInfo.Orientation = packet.ReadSingle();
                    packet.Store("Stationary Position", moveInfo.Position, index);
                    packet.Store("Stationary Orientation", moveInfo.Orientation, index);
                }
            }

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_2_2_14545))
            {
                if (flags.HasAnyFlag(UpdateFlag.Unknown1))
                    packet.ReadUInt32("Unk Int32", index);

                if (flags.HasAnyFlag(UpdateFlag.LowGuid))
                    packet.ReadUInt32("Low GUID", index);
            }

            if (flags.HasAnyFlag(UpdateFlag.AttackingTarget))
                packet.ReadPackedGuid("Target GUID", index);

            if (flags.HasAnyFlag(UpdateFlag.Transport))
                packet.ReadUInt32("Transport unk timer", index);

            if (flags.HasAnyFlag(UpdateFlag.Vehicle))
            {
                moveInfo.VehicleId = packet.ReadUInt32("Vehicle ID", index);
                packet.ReadSingle("Vehicle Orientation", index);
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
            {
                if (flags.HasAnyFlag(UpdateFlag.AnimKits))
                {
                    packet.ReadInt16("Unk Int16 1", index);
                    packet.ReadInt16("Unk Int16 2", index);
                    packet.ReadInt16("Unk Int16 3", index);
                }
            }

            if (flags.HasAnyFlag(UpdateFlag.GORotation))
                moveInfo.Rotation = packet.ReadPackedQuaternion("GO Rotation", index);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
            {
                if (flags.HasAnyFlag(UpdateFlag.TransportUnkArray))
                {
                    var count = packet.ReadByte("Count", index);
                    packet.StoreBeginList("Transport unks", index);
                    for (var i = 0; i < count; i++)
                        packet.ReadInt32("Unk Int32", index, i);
                    packet.StoreEndList();
                }
            }

            return moveInfo;
        }

        [Parser(Opcode.SMSG_COMPRESSED_UPDATE_OBJECT)]
        public static void HandleCompressedUpdateObject(Packet packet)
        {
            packet.Inflate(packet.ReadInt32());
            HandleUpdateObject(packet);
        }

        [Parser(Opcode.SMSG_DESTROY_OBJECT)]
        public static void HandleDestroyObject(Packet packet)
        {
            packet.ReadGuid("GUID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                packet.ReadBoolean("Despawn Animation");
        }

        [Parser(Opcode.CMSG_OBJECT_UPDATE_FAILED)] // 4.3.4
        public static void HandleObjectUpdateFailed(Packet packet)
        {
            var guid = packet.StartBitStream(6, 7, 4, 0, 1, 5, 3, 2);
            packet.ParseBitStream(guid, 6, 7, 2, 3, 1, 4, 0, 5);
            packet.StoreBitstreamGuid("Guid", guid);
        }
    }
}

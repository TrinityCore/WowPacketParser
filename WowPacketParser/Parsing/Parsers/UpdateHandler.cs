using System;
using System.Collections;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid=WowPacketParser.Misc.Guid;

namespace WowPacketParser.Parsing.Parsers
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

            for (var i = 0; i < count; i++)
            {
                var type = packet.ReadByte();
                var typeString = ClientVersion.AddedInVersion(ClientType.Cataclysm) ? ((UpdateTypeCataclysm)type).ToString() : ((UpdateType)type).ToString();

                packet.WriteLine("[" + i + "] UpdateType: " + typeString);
                switch (typeString)
                {
                    case "Values":
                    {
                        var guid = packet.ReadPackedGuid("GUID", i);

                        WoWObject obj;
                        var updates = ReadValuesUpdateBlock(ref packet, guid.GetObjectType(), i);

                        if (Storage.Objects.TryGetValue(guid, out obj))
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
            if (Storage.Objects.ContainsKey(guid))
            {
                var existObj = Storage.Objects[guid].Item1;
                ProcessExistingObject(ref existObj, obj, guid); // can't do "ref Storage.Objects[guid].Item1 directly
            }
            else
                Storage.Objects.Add(guid, obj, packet.TimeSpan);

            if (guid.HasEntry() && (objType == ObjectType.Unit || objType == ObjectType.GameObject))
                packet.AddSniffData(Utilities.ObjectTypeToStore(objType), (int)guid.GetEntry(), "SPAWN");
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
                        if (obj.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_FLAGS), out uf))
                            if ((uf.UInt32Value & (uint) UnitFlags.IsInCombat) == 0) // movement could be because of aggro so ignore that
                                obj.Movement.HasWpsOrRandMov = true;
                    }
            }
        }

        private static void ReadObjectsBlock(ref Packet packet, int index)
        {
            var objCount = packet.ReadInt32("Object Count", index);
            for (var j = 0; j < objCount; j++)
                packet.ReadPackedGuid("Object GUID", index, j);
        }

        private static Dictionary<int, UpdateField> ReadValuesUpdateBlock(ref Packet packet, ObjectType type, int index)
        {
            var maskSize = packet.ReadByte();

            var updateMask = new int[maskSize];
            for (var i = 0; i < maskSize; i++)
                updateMask[i] = packet.ReadInt32();

            var mask = new BitArray(updateMask);
            var dict = new Dictionary<int, UpdateField>();

            int objectEnd = UpdateFields.GetUpdateField(ObjectField.OBJECT_END);

            for (var i = 0; i < mask.Count; i++)
            {
                if (!mask[i])
                    continue;

                var blockVal = packet.ReadUpdateField();
                string key = "Block Value " + i;
                string value = blockVal.UInt32Value + "/" + blockVal.SingleValue;

                if (i < objectEnd)
                    key = UpdateFields.GetUpdateFieldName<ObjectField>(i);
                else
                {
                    switch (type)
                    {
                        case ObjectType.Container:
                        {
                            if (i < UpdateFields.GetUpdateField(ItemField.ITEM_END))
                                goto case ObjectType.Item;

                            key = UpdateFields.GetUpdateFieldName<ContainerField>(i);
                            break;
                        }
                        case ObjectType.Item:
                        {
                            key = UpdateFields.GetUpdateFieldName<ItemField>(i);
                            break;
                        }
                        case ObjectType.Player:
                        {
                            if (i < UpdateFields.GetUpdateField(UnitField.UNIT_END))
                                goto case ObjectType.Unit;

                            key = UpdateFields.GetUpdateFieldName<PlayerField>(i);
                            break;
                        }
                        case ObjectType.Unit:
                        {
                            key = UpdateFields.GetUpdateFieldName<UnitField>(i);
                            break;
                        }
                        case ObjectType.GameObject:
                        {
                            key = UpdateFields.GetUpdateFieldName<GameObjectField>(i);
                            break;
                        }
                        case ObjectType.DynamicObject:
                        {
                            key = UpdateFields.GetUpdateFieldName<DynamicObjectField>(i);
                            break;
                        }
                        case ObjectType.Corpse:
                        {
                            key = UpdateFields.GetUpdateFieldName<CorpseField>(i);
                            break;
                        }
                    }
                }
                packet.WriteLine("[" + index + "] " + key + ": " + value);
                dict.Add(i, blockVal);
            }

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
            var hasGameObjectPosition = packet.ReadBit("Has GameObject position", index);
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
                guid2[7] = (byte)(packet.ReadBit() ? 1 : 0);
                guid2[3] = (byte)(packet.ReadBit() ? 1 : 0);
                guid2[2] = (byte)(packet.ReadBit() ? 1 : 0);
                if (hasMovementFlags)
                    moveInfo.Flags = packet.ReadEnum<MovementFlag>("Movement Flags", 30, index);

                packet.ReadBit();
                hasPitch = !packet.ReadBit();
                moveInfo.HasSplineData = packet.ReadBit("Has Spline Data", index);
                hasFallData = packet.ReadBit("Has Fall Data", index);
                hasSplineElevation = !packet.ReadBit();
                guid2[5] = (byte)(packet.ReadBit() ? 1 : 0);
                hasTransportData = packet.ReadBit("Has transport data", index);
                hasTimestamp = !packet.ReadBit();
                if (hasTransportData)
                {
                    transportGuid[1] = (byte)(packet.ReadBit() ? 1 : 0);
                    hasTransportTime2 = packet.ReadBit();
                    transportGuid[4] = (byte)(packet.ReadBit() ? 1 : 0);
                    transportGuid[0] = (byte)(packet.ReadBit() ? 1 : 0);
                    transportGuid[6] = (byte)(packet.ReadBit() ? 1 : 0);
                    hasTransportTime3 = packet.ReadBit();
                    transportGuid[7] = (byte)(packet.ReadBit() ? 1 : 0);
                    transportGuid[5] = (byte)(packet.ReadBit() ? 1 : 0);
                    transportGuid[3] = (byte)(packet.ReadBit() ? 1 : 0);
                    transportGuid[2] = (byte)(packet.ReadBit() ? 1 : 0);
                }

                guid2[4] = (byte)(packet.ReadBit() ? 1 : 0);
                if (moveInfo.HasSplineData)
                {
                    bit216 = packet.ReadBit();
                    if (bit216)
                    {
                        /*var splineMode =*/ packet.ReadBits(2);
                        hasSplineStartTime = packet.ReadBit();
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
                        {
                            facingTargetGuid[4] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTargetGuid[3] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTargetGuid[7] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTargetGuid[2] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTargetGuid[6] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTargetGuid[1] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTargetGuid[0] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTargetGuid[5] = (byte)(packet.ReadBit() ? 1 : 0);
                        }

                        hasSplineVerticalAcceleration = packet.ReadBit();
                        /*splineFlags =*/ packet.ReadEnum<SplineFlag422>("Spline flags", 25, index);
                    }
                }

                guid2[6] = (byte)(packet.ReadBit() ? 1 : 0);
                if (hasFallData)
                    hasFallDirection = packet.ReadBit("Has Fall Direction", index);

                guid2[0] = (byte)(packet.ReadBit() ? 1 : 0);
                guid2[1] = (byte)(packet.ReadBit() ? 1 : 0);
                packet.ReadBit();
                if (!packet.ReadBit())
                    moveInfo.FlagsExtra = packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12, index);
            }

            if (hasGameObjectPosition)
            {
                goTransportGuid[5] = (byte)(packet.ReadBit() ? 1 : 0);
                hasGOTransportTime3 = packet.ReadBit();
                goTransportGuid[0] = (byte)(packet.ReadBit() ? 1 : 0);
                goTransportGuid[3] = (byte)(packet.ReadBit() ? 1 : 0);
                goTransportGuid[6] = (byte)(packet.ReadBit() ? 1 : 0);
                goTransportGuid[1] = (byte)(packet.ReadBit() ? 1 : 0);
                goTransportGuid[4] = (byte)(packet.ReadBit() ? 1 : 0);
                goTransportGuid[2] = (byte)(packet.ReadBit() ? 1 : 0);
                hasGOTransportTime2 = packet.ReadBit();
                goTransportGuid[7] = (byte)(packet.ReadBit() ? 1 : 0);
            }

            if (hasAttackingTarget)
            {
                attackingTargetGuid[2] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTargetGuid[7] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTargetGuid[0] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTargetGuid[4] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTargetGuid[5] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTargetGuid[6] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTargetGuid[1] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTargetGuid[3] = (byte)(packet.ReadBit() ? 1 : 0);
            }

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
                if (guid2[4] != 0) guid2[4] ^= packet.ReadByte();

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
                            if (facingTargetGuid[5] != 0) facingTargetGuid[5] ^= packet.ReadByte();
                            if (facingTargetGuid[3] != 0) facingTargetGuid[3] ^= packet.ReadByte();
                            if (facingTargetGuid[7] != 0) facingTargetGuid[7] ^= packet.ReadByte();
                            if (facingTargetGuid[1] != 0) facingTargetGuid[1] ^= packet.ReadByte();
                            if (facingTargetGuid[6] != 0) facingTargetGuid[6] ^= packet.ReadByte();
                            if (facingTargetGuid[4] != 0) facingTargetGuid[4] ^= packet.ReadByte();
                            if (facingTargetGuid[2] != 0) facingTargetGuid[2] ^= packet.ReadByte();
                            if (facingTargetGuid[0] != 0) facingTargetGuid[0] ^= packet.ReadByte();
                            packet.WriteLine("[{0}] Facing Target GUID {1}", index, new Guid(BitConverter.ToUInt64(facingTargetGuid, 0)));
                        }

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
                    packet.WriteLine("[{0}] Spline Endpoint: {1}", index, endPoint);
                }

                moveInfo.Position.Z = packet.ReadSingle();
                if (guid2[5] != 0) guid2[5] ^= packet.ReadByte();

                if (hasTransportData)
                {
                    if (transportGuid[5] != 0) transportGuid[5] ^= packet.ReadByte();
                    if (transportGuid[7] != 0) transportGuid[7] ^= packet.ReadByte();

                    packet.ReadUInt32("Transport time", index);
                    var transPos = new Vector4();
                    transPos.O = packet.ReadSingle();
                    if (hasTransportTime2)
                        packet.ReadUInt32("Transport time 2", index);

                    transPos.Y = packet.ReadSingle();
                    transPos.X = packet.ReadSingle();
                    if (transportGuid[3] != 0) transportGuid[3] ^= packet.ReadByte();

                    transPos.Z = packet.ReadSingle();
                    if (transportGuid[0] != 0) transportGuid[0] ^= packet.ReadByte();

                    if (hasTransportTime3)
                        packet.ReadUInt32("Transport time 3", index);

                    packet.ReadSByte("Transport seat", index);
                    if (transportGuid[1] != 0) transportGuid[1] ^= packet.ReadByte();
                    if (transportGuid[6] != 0) transportGuid[6] ^= packet.ReadByte();
                    if (transportGuid[2] != 0) transportGuid[2] ^= packet.ReadByte();
                    if (transportGuid[4] != 0) transportGuid[4] ^= packet.ReadByte();
                    packet.WriteLine("[{0}] Transport GUID {1}", index, new Guid(BitConverter.ToUInt64(transportGuid, 0)));
                    packet.WriteLine("[{0}] Transport Position: {1}", index, transPos);
                }

                moveInfo.Position.X = packet.ReadSingle();
                packet.ReadSingle("Pitch Speed", index);
                if (guid2[3] != 0) guid2[3] ^= packet.ReadByte();
                if (guid2[0] != 0) guid2[0] ^= packet.ReadByte();

                packet.ReadSingle("Swim Speed", index);
                moveInfo.Position.Y = packet.ReadSingle();
                if (guid2[7] != 0) guid2[7] ^= packet.ReadByte();
                if (guid2[1] != 0) guid2[1] ^= packet.ReadByte();
                if (guid2[2] != 0) guid2[2] ^= packet.ReadByte();

                moveInfo.WalkSpeed = packet.ReadSingle("Walk Speed", index) / 2.5f;
                if (hasTimestamp)
                    packet.ReadUInt32("Time", index);

                packet.ReadSingle("FlyBack Speed", index);
                if (guid2[6] != 0) guid2[6] ^= packet.ReadByte();

                packet.ReadSingle("Turn Speed", index);
                if (hasOrientation)
                    moveInfo.Orientation = packet.ReadSingle();

                moveInfo.RunSpeed = packet.ReadSingle("Run Speed", index) / 7.0f;
                if (hasPitch)
                    packet.ReadSingle("Pitch", index);

                packet.ReadSingle("Fly Speed", index);

                packet.WriteLine("[{0}] GUID 2: {1}", index, new Guid(BitConverter.ToUInt64(guid2, 0)));
                packet.WriteLine("[{0}] Position: {1}", index, moveInfo.Position);
                packet.WriteLine("[{0}] Orientation: {1}", index, moveInfo.Orientation);
            }

            if (hasVehicleData)
            {
                packet.ReadSingle("Vehicle Orientation", index);
                moveInfo.VehicleId = packet.ReadUInt32("Vehicle Id", index);
            }

            if (hasGameObjectPosition)
            {
                var tPos = new Vector4();

                if (goTransportGuid[0] != 0) goTransportGuid[0] ^= packet.ReadByte();
                if (goTransportGuid[5] != 0) goTransportGuid[5] ^= packet.ReadByte();
                if (hasGOTransportTime3)
                    packet.ReadUInt32("GO Transport Time 3", index);

                if (goTransportGuid[3] != 0) goTransportGuid[3] ^= packet.ReadByte();

                tPos.X = packet.ReadSingle();
                if (goTransportGuid[4] != 0) goTransportGuid[4] ^= packet.ReadByte();
                if (goTransportGuid[6] != 0) goTransportGuid[6] ^= packet.ReadByte();
                if (goTransportGuid[1] != 0) goTransportGuid[1] ^= packet.ReadByte();

                packet.ReadSingle("GO Transport Time", index);
                tPos.Y = packet.ReadSingle();
                if (goTransportGuid[2] != 0) goTransportGuid[2] ^= packet.ReadByte();
                if (goTransportGuid[7] != 0) goTransportGuid[7] ^= packet.ReadByte();

                tPos.Z = packet.ReadSingle();
                packet.ReadByte("GO Transport Seat", index);
                tPos.O = packet.ReadSingle();
                if (hasGOTransportTime2)
                    packet.ReadUInt32("GO Transport Time 2", index);

                packet.WriteLine("[{0}] GO Transport GUID {1}", index, new Guid(BitConverter.ToUInt64(goTransportGuid, 0)));
                packet.WriteLine("[{0}] GO Transport Position: {1}", index, tPos);
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
                if (attackingTargetGuid[0] != 0) attackingTargetGuid[0] ^= packet.ReadByte();
                if (attackingTargetGuid[3] != 0) attackingTargetGuid[3] ^= packet.ReadByte();
                if (attackingTargetGuid[5] != 0) attackingTargetGuid[5] ^= packet.ReadByte();
                if (attackingTargetGuid[7] != 0) attackingTargetGuid[7] ^= packet.ReadByte();
                if (attackingTargetGuid[6] != 0) attackingTargetGuid[6] ^= packet.ReadByte();
                if (attackingTargetGuid[2] != 0) attackingTargetGuid[2] ^= packet.ReadByte();
                if (attackingTargetGuid[1] != 0) attackingTargetGuid[1] ^= packet.ReadByte();
                if (attackingTargetGuid[4] != 0) attackingTargetGuid[4] ^= packet.ReadByte();
                packet.WriteLine("[{0}] Attacking Target GUID {1}", index, new Guid(BitConverter.ToUInt64(attackingTargetGuid, 0)));
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
            bool hasGameObjectPosition = packet.ReadBit("Has GameObject position", index);
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
                guid2[4] = (byte)(packet.ReadBit() ? 1 : 0);
                /*bool bit149 =*/ packet.ReadBit();
                guid2[5] = (byte)(packet.ReadBit() ? 1 : 0);
                unkFloat1 = !packet.ReadBit();
                hasFallData = packet.ReadBit("Has Fall Data", index);
                unkFloat2 = !packet.ReadBit();
                guid2[6] = (byte)(packet.ReadBit() ? 1 : 0);
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
                        {
                            facingTarget[0] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTarget[2] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTarget[7] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTarget[1] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTarget[6] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTarget[3] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTarget[4] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTarget[5] = (byte)(packet.ReadBit() ? 1 : 0);
                        }

                        /*splineFlags =*/ packet.ReadEnum<SplineFlag422>("Spline flags", 25, index);
                        splineCount = packet.ReadBits(22);
                    }
                }

                hasTransportData = packet.ReadBit("Has transport data", index);
                guid2[1] = (byte)(packet.ReadBit() ? 1 : 0);
                /*bit148 =*/ packet.ReadBit();
                if (hasTransportData)
                {
                    hasTransportTime2 = packet.ReadBit();
                    transportGuid[0] = (byte)(packet.ReadBit() ? 1 : 0);
                    transportGuid[7] = (byte)(packet.ReadBit() ? 1 : 0);
                    transportGuid[2] = (byte)(packet.ReadBit() ? 1 : 0);
                    transportGuid[6] = (byte)(packet.ReadBit() ? 1 : 0);
                    transportGuid[5] = (byte)(packet.ReadBit() ? 1 : 0);
                    transportGuid[4] = (byte)(packet.ReadBit() ? 1 : 0);
                    transportGuid[1] = (byte)(packet.ReadBit() ? 1 : 0);
                    transportGuid[3] = (byte)(packet.ReadBit() ? 1 : 0);
                    hasTransportTime3 = packet.ReadBit();
                }

                guid2[2] = (byte)(packet.ReadBit() ? 1 : 0);
                if (hasFallData)
                    hasFallDirection = packet.ReadBit("Has Fall Direction", index);

                bool hasMovementFlags = !packet.ReadBit();
                bool hasExtraMovementFlags = !packet.ReadBit();
                hasUnkUInt = !packet.ReadBit();
                guid2[7] = (byte)(packet.ReadBit() ? 1 : 0);
                if (hasExtraMovementFlags)
                    moveInfo.FlagsExtra = packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12, index);

                guid2[0] = (byte)(packet.ReadBit() ? 1 : 0);
                if (hasMovementFlags)
                    moveInfo.Flags = packet.ReadEnum<MovementFlag>("Movement Flags", 30, index);

                guid2[3] = (byte)(packet.ReadBit() ? 1 : 0);
                hasOrientation = !packet.ReadBit();
            }

            if (hasAttackingTarget)
            {
                attackingTarget[2] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTarget[4] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTarget[0] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTarget[1] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTarget[3] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTarget[7] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTarget[5] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTarget[6] = (byte)(packet.ReadBit() ? 1 : 0);
            }

            if (hasGameObjectPosition)
            {
                hasGOTransportTime2 = packet.ReadBit();
                goTransportGuid[1] = (byte)(packet.ReadBit() ? 1 : 0);
                goTransportGuid[4] = (byte)(packet.ReadBit() ? 1 : 0);
                goTransportGuid[5] = (byte)(packet.ReadBit() ? 1 : 0);
                goTransportGuid[0] = (byte)(packet.ReadBit() ? 1 : 0);
                goTransportGuid[6] = (byte)(packet.ReadBit() ? 1 : 0);
                goTransportGuid[7] = (byte)(packet.ReadBit() ? 1 : 0);
                goTransportGuid[3] = (byte)(packet.ReadBit() ? 1 : 0);
                hasGOTransportTime3 = packet.ReadBit();
                goTransportGuid[2] = (byte)(packet.ReadBit() ? 1 : 0);
            }

            if (hasAnimKits)
            {
                hasAnimKit3 = !packet.ReadBit();
                hasAnimKit1 = !packet.ReadBit();
                hasAnimKit2 = !packet.ReadBit();
            }

            // Reading data
            for (var i = 0u; i < unkLoopCounter; ++i)
                packet.ReadUInt32("Unk UInt32", index, (int)i);

            if (living)
            {
                moveInfo.WalkSpeed = packet.ReadSingle("Walk Speed", index) / 2.5f;
                if (moveInfo.HasSplineData)
                {
                    if (bit216)
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

                        if (splineType == SplineType.FacingTarget)
                        {
                            if (facingTarget[0] != 0) facingTarget[0] ^= packet.ReadByte();
                            if (facingTarget[6] != 0) facingTarget[6] ^= packet.ReadByte();
                            if (facingTarget[5] != 0) facingTarget[5] ^= packet.ReadByte();
                            if (facingTarget[4] != 0) facingTarget[4] ^= packet.ReadByte();
                            if (facingTarget[1] != 0) facingTarget[1] ^= packet.ReadByte();
                            if (facingTarget[3] != 0) facingTarget[3] ^= packet.ReadByte();
                            if (facingTarget[7] != 0) facingTarget[7] ^= packet.ReadByte();
                            if (facingTarget[2] != 0) facingTarget[2] ^= packet.ReadByte();
                            packet.WriteLine("[{0}] Facing Target GUID {1}", index, new Guid(BitConverter.ToUInt64(facingTarget, 0)));
                        }
                        else if (splineType == SplineType.FacingSpot)
                        {
                            var point = new Vector3
                            {
                                Z = packet.ReadSingle(),
                                Y = packet.ReadSingle(),
                                X = packet.ReadSingle(),
                            };

                            packet.WriteLine("[{0}] Facing Spot: {1}", index, point);
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
                    packet.WriteLine("[{0}] Spline Endpoint: {1}", index, endPoint);
                }

                if (hasTransportData)
                {
                    if (hasTransportTime2)
                        packet.ReadInt32("Transport Time 2", index);

                    if (transportGuid[4] != 0) transportGuid[4] ^= packet.ReadByte();
                    if (transportGuid[6] != 0) transportGuid[6] ^= packet.ReadByte();
                    if (transportGuid[5] != 0) transportGuid[5] ^= packet.ReadByte();

                    if (hasTransportTime3)
                        packet.ReadInt32("Transport Time 3", index);

                    if (transportGuid[7] != 0) transportGuid[7] ^= packet.ReadByte();
                    if (transportGuid[3] != 0) transportGuid[3] ^= packet.ReadByte();

                    var tPos = new Vector4
                    {
                        X = packet.ReadSingle(),
                        Z = packet.ReadSingle(),
                        O = packet.ReadSingle(),
                    };

                    if (transportGuid[2] != 0) transportGuid[2] ^= packet.ReadByte();
                    if (transportGuid[1] != 0) transportGuid[1] ^= packet.ReadByte();
                    if (transportGuid[0] != 0) transportGuid[0] ^= packet.ReadByte();

                    tPos.Y = packet.ReadSingle();
                    packet.WriteLine("[{0}] Transport Position: {1}", index, tPos);
                    packet.ReadByte("Transport Seat", index);
                    packet.ReadInt32("Transport Time", index);
                }

                if (unkFloat1)
                    packet.ReadSingle("float +28", index);

                packet.ReadSingle("FlyBack Speed", index);
                packet.ReadSingle("Turn Speed", index);
                if (guid2[5] != 0) guid2[5] ^= packet.ReadByte();

                moveInfo.RunSpeed = packet.ReadSingle("Run Speed", index) / 7.0f;
                if (unkFloat2)
                    packet.ReadSingle("float +36", index);

                if (guid2[0] != 0) guid2[0] ^= packet.ReadByte();

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
                if (guid2[7] != 0) guid2[7] ^= packet.ReadByte();

                moveInfo.Position.Z = packet.ReadSingle();
                if (guid2[3] != 0) guid2[3] ^= packet.ReadByte();
                if (guid2[2] != 0) guid2[2] ^= packet.ReadByte();

                packet.ReadSingle("Fly Speed", index);
                packet.ReadSingle("Swim Speed", index);
                if (guid2[1] != 0) guid2[1] ^= packet.ReadByte();
                if (guid2[4] != 0) guid2[4] ^= packet.ReadByte();
                if (guid2[6] != 0) guid2[6] ^= packet.ReadByte();

                packet.WriteLine("[{0}] GUID 2 {1}", index, new Guid(BitConverter.ToUInt64(guid2, 0)));
                moveInfo.Position.Y = packet.ReadSingle();
                if (hasUnkUInt)
                    packet.ReadUInt32();

                if (hasOrientation)
                    moveInfo.Orientation = packet.ReadSingle();

                packet.WriteLine("[{0}] Position: {1} Orientation: {2}", index, moveInfo.Position, moveInfo.Orientation);
            }

            if (unkFloats)
            {
                int i;
                for (i = 0; i < 13; ++i)
                    packet.ReadSingle("Unk float 456", index, i);

                packet.ReadByte("Unk byte 456", index);

                for (; i < 16; ++i)
                    packet.ReadSingle("Unk float 456", index, i);
            }

            if (hasGameObjectPosition)
            {
                var tPos = new Vector4();

                if (goTransportGuid[6] != 0) goTransportGuid[6] ^= packet.ReadByte();
                if (goTransportGuid[5] != 0) goTransportGuid[5] ^= packet.ReadByte();

                tPos.Y = packet.ReadSingle();
                if (goTransportGuid[4] != 0) goTransportGuid[4] ^= packet.ReadByte();
                if (goTransportGuid[2] != 0) goTransportGuid[2] ^= packet.ReadByte();
                if (hasGOTransportTime3)
                    packet.ReadUInt32("GO Transport Time 3", index);

                tPos.O = packet.ReadSingle();
                tPos.Z = packet.ReadSingle();
                if (hasGOTransportTime2)
                    packet.ReadUInt32("GO Transport Time 2", index);

                packet.ReadByte("GO Transport Seat", index);
                if (goTransportGuid[7] != 0) goTransportGuid[7] ^= packet.ReadByte();
                if (goTransportGuid[1] != 0) goTransportGuid[1] ^= packet.ReadByte();
                if (goTransportGuid[0] != 0) goTransportGuid[0] ^= packet.ReadByte();
                if (goTransportGuid[3] != 0) goTransportGuid[3] ^= packet.ReadByte();

                tPos.X = packet.ReadSingle();
                packet.ReadSingle("GO Transport Time", index);
                packet.WriteLine("[{0}] GO Transport Position: {1}", index, tPos);
                packet.WriteLine("[{0}] GO Transport GUID {1}", index, new Guid(BitConverter.ToUInt64(goTransportGuid, 0)));
            }

            if (hasAttackingTarget)
            {
                if (attackingTarget[2] != 0) attackingTarget[2] ^= packet.ReadByte();
                if (attackingTarget[4] != 0) attackingTarget[4] ^= packet.ReadByte();
                if (attackingTarget[7] != 0) attackingTarget[7] ^= packet.ReadByte();
                if (attackingTarget[3] != 0) attackingTarget[3] ^= packet.ReadByte();
                if (attackingTarget[0] != 0) attackingTarget[0] ^= packet.ReadByte();
                if (attackingTarget[1] != 0) attackingTarget[1] ^= packet.ReadByte();
                if (attackingTarget[5] != 0) attackingTarget[5] ^= packet.ReadByte();
                if (attackingTarget[6] != 0) attackingTarget[6] ^= packet.ReadByte();
                packet.WriteLine("[{0}] Attacking Target GUID {1}", index, new Guid(BitConverter.ToUInt64(attackingTarget, 0)));
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
                packet.WriteLine("[{0}] Stationary Position: {1}, O: {2}", index, moveInfo.Position, moveInfo.Orientation);
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
                guid2[0] = (byte)(packet.ReadBit() ? 1 : 0);
                guid2[5] = (byte)(packet.ReadBit() ? 1 : 0);
                guid2[4] = (byte)(packet.ReadBit() ? 1 : 0);
                bool hasMovementFlags = !packet.ReadBit();
                moveInfo.HasSplineData = packet.ReadBit("Has Spline Data", index);
                /*bool bit148 = */packet.ReadBit();

                if (hasExtraMovementFlags)
                    moveInfo.FlagsExtra = packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12, index);

                hasUnkUInt = !packet.ReadBit();
                guid2[3] = (byte)(packet.ReadBit() ? 1 : 0);
                /*bool bit149 = */packet.ReadBit();

                if (hasMovementFlags)
                    moveInfo.Flags = packet.ReadEnum<MovementFlag>("Movement Flags", 30, index);

                guid2[1] = (byte)(packet.ReadBit() ? 1 : 0);
                unkFloat2 = !packet.ReadBit();
                hasTransportData = packet.ReadBit("Has Transport Data", index);
                guid2[2] = (byte)(packet.ReadBit() ? 1 : 0);

                if (hasTransportData)
                {
                    transportGuid[3] = (byte)(packet.ReadBit() ? 1 : 0);
                    transportGuid[5] = (byte)(packet.ReadBit() ? 1 : 0);
                    transportGuid[1] = (byte)(packet.ReadBit() ? 1 : 0);
                    transportGuid[7] = (byte)(packet.ReadBit() ? 1 : 0);
                    hasTransportTime2 = packet.ReadBit();
                    transportGuid[4] = (byte)(packet.ReadBit() ? 1 : 0);
                    transportGuid[0] = (byte)(packet.ReadBit() ? 1 : 0);
                    transportGuid[2] = (byte)(packet.ReadBit() ? 1 : 0);
                    transportGuid[6] = (byte)(packet.ReadBit() ? 1 : 0);
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
                        {
                            facingTarget[4] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTarget[3] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTarget[2] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTarget[5] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTarget[7] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTarget[1] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTarget[0] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTarget[6] = (byte)(packet.ReadBit() ? 1 : 0);
                        }

                        packet.ReadEnum<SplineFlag422>("Spline flags", 25, index);
                        /*splineMode =*/packet.ReadBits(2);
                        hasSplineDurationMult = packet.ReadBit("HasSplineDurationMult", index);
                        bit256 = packet.ReadBit();
                    }
                }

                if (hasFallData)
                    hasFallDirection = packet.ReadBit("Has Fall Direction", index);

                guid2[6] = (byte)(packet.ReadBit() ? 1 : 0);
                guid2[7] = (byte)(packet.ReadBit() ? 1 : 0);
            }

            if (hasGameObjectPosition)
            {
                goTransportGuid[5] = (byte)(packet.ReadBit() ? 1 : 0);
                goTransportGuid[4] = (byte)(packet.ReadBit() ? 1 : 0);
                hasGOTransportTime3 = packet.ReadBit();
                goTransportGuid[7] = (byte)(packet.ReadBit() ? 1 : 0);
                goTransportGuid[6] = (byte)(packet.ReadBit() ? 1 : 0);
                goTransportGuid[1] = (byte)(packet.ReadBit() ? 1 : 0);
                goTransportGuid[2] = (byte)(packet.ReadBit() ? 1 : 0);
                hasGOTransportTime2 = packet.ReadBit();
                goTransportGuid[0] = (byte)(packet.ReadBit() ? 1 : 0);
                goTransportGuid[3] = (byte)(packet.ReadBit() ? 1 : 0);
            }

            if (hasAnimKits)
            {
                hasAnimKit1 = !packet.ReadBit();
                hasAnimKit3 = !packet.ReadBit();
                hasAnimKit2 = !packet.ReadBit();
            }

            if (hasAttackingTarget)
            {
                attackingTarget[4] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTarget[3] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTarget[2] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTarget[5] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTarget[0] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTarget[6] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTarget[1] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTarget[7] = (byte)(packet.ReadBit() ? 1 : 0);
            }

            for (var i = 0; i < unkLoopCounter; ++i)
            {
                packet.ReadInt32();
            }

            if (hasGameObjectPosition)
            {
                var tPos = new Vector4();

                if (hasGOTransportTime3)
                    packet.ReadInt32("GO Transport Time 3", index);

                if (goTransportGuid[7] != 0) goTransportGuid[7] ^= packet.ReadByte();

                tPos.Y = packet.ReadSingle();
                packet.ReadByte("GO Transport Seat", index);
                tPos.O = packet.ReadSingle();
                tPos.Z = packet.ReadSingle();

                if (goTransportGuid[4] != 0) goTransportGuid[4] ^= packet.ReadByte();
                if (goTransportGuid[5] != 0) goTransportGuid[5] ^= packet.ReadByte();
                if (goTransportGuid[6] != 0) goTransportGuid[6] ^= packet.ReadByte();

                tPos.X = packet.ReadSingle();
                packet.ReadInt32("GO Transport Time", index);

                if (goTransportGuid[1] != 0) goTransportGuid[1] ^= packet.ReadByte();

                if (hasGOTransportTime2)
                    packet.ReadInt32("GO Transport Time 2", index);

                if (goTransportGuid[0] != 0) goTransportGuid[0] ^= packet.ReadByte();
                if (goTransportGuid[2] != 0) goTransportGuid[2] ^= packet.ReadByte();
                if (goTransportGuid[3] != 0) goTransportGuid[3] ^= packet.ReadByte();

                packet.WriteLine("[{0}] GO Transport Position: {1}", index, tPos);
                packet.WriteLine("[{0}] GO Transport GUID {1}", index, new Guid(BitConverter.ToUInt64(goTransportGuid, 0)));
            }

            if (living)
            {
                if (moveInfo.HasSplineData)
                {
                    if (bit216)
                    {
                        packet.ReadSingle("Unknown Spline Float 2", index);
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

                        if (splineType == SplineType.FacingTarget)
                        {
                            if (facingTarget[2] != 0) facingTarget[2] ^= packet.ReadByte();
                            if (facingTarget[1] != 0) facingTarget[1] ^= packet.ReadByte();
                            if (facingTarget[3] != 0) facingTarget[3] ^= packet.ReadByte();
                            if (facingTarget[7] != 0) facingTarget[7] ^= packet.ReadByte();
                            if (facingTarget[0] != 0) facingTarget[0] ^= packet.ReadByte();
                            if (facingTarget[5] != 0) facingTarget[5] ^= packet.ReadByte();
                            if (facingTarget[4] != 0) facingTarget[4] ^= packet.ReadByte();
                            if (facingTarget[6] != 0) facingTarget[6] ^= packet.ReadByte();
                            packet.WriteLine("[{0}] Facing Target GUID {1}", index, new Guid(BitConverter.ToUInt64(facingTarget, 0)));
                        }
                        else if (splineType == SplineType.FacingSpot)
                        {
                            var point = new Vector3
                            {
                                Y = packet.ReadSingle(),
                                Z = packet.ReadSingle(),
                                X = packet.ReadSingle(),
                            };

                            packet.WriteLine("[{0}] Facing Spot: {1}", index, point);
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

                    packet.WriteLine("[{0}] Spline Endpoint: {1}", index, endPoint);
                }

                if (hasTransportData)
                {
                    var tPos = new Vector4();

                    if (transportGuid[6] != 0) transportGuid[6] ^= packet.ReadByte();
                    if (hasTransportTime2)
                        packet.ReadInt32("Transport Time 2", index);

                    packet.ReadByte("Transport Seat", index);
                    tPos.O = packet.ReadSingle();
                    if (transportGuid[7] != 0) transportGuid[7] ^= packet.ReadByte();
                    tPos.Y = packet.ReadSingle();
                    if (transportGuid[3] != 0) transportGuid[3] ^= packet.ReadByte();
                    if (hasTransportTime3)
                        packet.ReadInt32("Transport Time 3", index);

                    packet.ReadInt32("Transport Time", index);
                    if (transportGuid[0] != 0) transportGuid[0] ^= packet.ReadByte();
                    if (transportGuid[1] != 0) transportGuid[1] ^= packet.ReadByte();
                    tPos.X = packet.ReadSingle();
                    if (transportGuid[4] != 0) transportGuid[4] ^= packet.ReadByte();
                    tPos.Z = packet.ReadSingle();
                    if (transportGuid[5] != 0) transportGuid[5] ^= packet.ReadByte();
                    if (transportGuid[2] != 0) transportGuid[2] ^= packet.ReadByte();

                    packet.WriteLine("[{0}] Transport Position: {1}", index, tPos);
                }

                moveInfo.Position = new Vector3();
                moveInfo.Position.Z = packet.ReadSingle();
                packet.ReadSingle("FlyBack Speed", index);
                moveInfo.Position.Y = packet.ReadSingle();
                if (guid2[4] != 0) guid2[4] ^= packet.ReadByte();
                if (guid2[0] != 0) guid2[0] ^= packet.ReadByte();
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

                packet.WriteLine("[{0}] Position: {1} Orientation: {2}", index, moveInfo.Position, moveInfo.Orientation);
                packet.ReadSingle("Swim Speed", index);
                moveInfo.RunSpeed = packet.ReadSingle("Run Speed", index) / 7.0f;
                packet.ReadSingle("Fly Speed", index);
                if (guid2[2] != 0) guid2[2] ^= packet.ReadByte();
                if (unkFloat2)
                    packet.ReadSingle("Unk float +36", index);

                if (unkFloat1)
                    packet.ReadSingle("Unk float +28", index);

                if (guid2[3] != 0) guid2[3] ^= packet.ReadByte();
                packet.ReadSingle("RunBack Speed", index);
                if (guid2[6] != 0) guid2[6] ^= packet.ReadByte();
                packet.ReadSingle("Pitch Speed", index);
                if (guid2[7] != 0) guid2[7] ^= packet.ReadByte();
                if (guid2[5] != 0) guid2[5] ^= packet.ReadByte();
                packet.ReadSingle("Turn Speed", index);
                packet.ReadSingle("SwimBack Speed", index);
                if (guid2[1] != 0) guid2[1] ^= packet.ReadByte();
                packet.WriteLine("[{0}] GUID 2 {1}", index, new Guid(BitConverter.ToUInt64(guid2, 0)));
                if (hasUnkUInt)
                    packet.ReadInt32();

                moveInfo.WalkSpeed = packet.ReadSingle("Walk Speed", index) / 2.5f;
            }

            if (hasAttackingTarget)
            {
                if (attackingTarget[6] != 0) attackingTarget[6] ^= packet.ReadByte();
                if (attackingTarget[5] != 0) attackingTarget[5] ^= packet.ReadByte();
                if (attackingTarget[3] != 0) attackingTarget[3] ^= packet.ReadByte();
                if (attackingTarget[2] != 0) attackingTarget[2] ^= packet.ReadByte();
                if (attackingTarget[0] != 0) attackingTarget[0] ^= packet.ReadByte();
                if (attackingTarget[1] != 0) attackingTarget[1] ^= packet.ReadByte();
                if (attackingTarget[7] != 0) attackingTarget[7] ^= packet.ReadByte();
                if (attackingTarget[4] != 0) attackingTarget[4] ^= packet.ReadByte();
                packet.WriteLine("[{0}] Attacking Target GUID {1}", index, new Guid(BitConverter.ToUInt64(attackingTarget, 0)));
            }

            if (unkFloats)
            {
                int i;
                for (i = 0; i < 13; ++i)
                    packet.ReadSingle("Unk float 456", index, i);

                packet.ReadByte("Unk byte 456", index);

                for (; i < 16; ++i)
                    packet.ReadSingle("Unk float 456", index, i);
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
                packet.WriteLine("[{0}] Stationary Position: {1}, O: {2}", index, moveInfo.Position, moveInfo.Orientation);
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
                    transportGuid[2] = (byte)(packet.ReadBit() ? 1 : 0);
                    transportGuid[7] = (byte)(packet.ReadBit() ? 1 : 0);
                    transportGuid[5] = (byte)(packet.ReadBit() ? 1 : 0);
                    hasTransportTime3 = packet.ReadBit();
                    transportGuid[3] = (byte)(packet.ReadBit() ? 1 : 0);
                    transportGuid[0] = (byte)(packet.ReadBit() ? 1 : 0);
                    transportGuid[4] = (byte)(packet.ReadBit() ? 1 : 0);
                    transportGuid[1] = (byte)(packet.ReadBit() ? 1 : 0);
                    hasTransportTime2 = packet.ReadBit();
                    transportGuid[6] = (byte)(packet.ReadBit() ? 1 : 0);
                }

                moveInfo.HasSplineData = packet.ReadBit("Has Spline Data", index);
                guid2[7] = (byte)(packet.ReadBit() ? 1 : 0);
                guid2[6] = (byte)(packet.ReadBit() ? 1 : 0);
                guid2[5] = (byte)(packet.ReadBit() ? 1 : 0);
                guid2[2] = (byte)(packet.ReadBit() ? 1 : 0);
                guid2[4] = (byte)(packet.ReadBit() ? 1 : 0);
                bool hasMovementFlags = !packet.ReadBit();
                guid2[1] = (byte)(packet.ReadBit() ? 1 : 0);
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
                        {
                            facingTarget[7] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTarget[3] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTarget[4] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTarget[2] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTarget[1] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTarget[6] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTarget[0] = (byte)(packet.ReadBit() ? 1 : 0);
                            facingTarget[5] = (byte)(packet.ReadBit() ? 1 : 0);
                        }
                    }
                }

                guid2[3] = (byte)(packet.ReadBit() ? 1 : 0);
                if (hasMovementFlags)
                    moveInfo.Flags = packet.ReadEnum<MovementFlag>("Movement Flags", 30, index);

                unkFloat1 = !packet.ReadBit();
                hasFallData = packet.ReadBit("Has Fall Data", index);
                if (hasExtraMovementFlags)
                    moveInfo.FlagsExtra = packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 12, index);

                guid2[0] = (byte)(packet.ReadBit() ? 1 : 0);
                hasOrientation = !packet.ReadBit();
                if (hasFallData)
                    hasFallDirection = packet.ReadBit("Has Fall Direction", index);

                unkFloat2 = !packet.ReadBit();
            }

            if (hasGameObjectPosition)
            {
                goTransportGuid[1] = (byte)(packet.ReadBit() ? 1 : 0);
                hasGOTransportTime3 = packet.ReadBit();
                goTransportGuid[3] = (byte)(packet.ReadBit() ? 1 : 0);
                goTransportGuid[2] = (byte)(packet.ReadBit() ? 1 : 0);
                goTransportGuid[6] = (byte)(packet.ReadBit() ? 1 : 0);
                goTransportGuid[5] = (byte)(packet.ReadBit() ? 1 : 0);
                goTransportGuid[0] = (byte)(packet.ReadBit() ? 1 : 0);
                goTransportGuid[4] = (byte)(packet.ReadBit() ? 1 : 0);
                hasGOTransportTime2 = packet.ReadBit();
                goTransportGuid[7] = (byte)(packet.ReadBit() ? 1 : 0);
            }

            if (hasAnimKits)
            {
                hasAnimKit3 = !packet.ReadBit();
                hasAnimKit1 = !packet.ReadBit();
                hasAnimKit2 = !packet.ReadBit();
            }

            if (hasAttackingTarget)
            {
                attackingTarget[3] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTarget[4] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTarget[6] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTarget[0] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTarget[1] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTarget[7] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTarget[5] = (byte)(packet.ReadBit() ? 1 : 0);
                attackingTarget[2] = (byte)(packet.ReadBit() ? 1 : 0);
            }

            // Reading data
            for (var i = 0u; i < unkLoopCounter; ++i)
                packet.ReadUInt32("Unk UInt32", index, (int)i);

            if (hasStationaryPosition)
            {
                moveInfo.Position = new Vector3();
                moveInfo.Position.Z = packet.ReadSingle();
                moveInfo.Orientation = packet.ReadSingle();
                moveInfo.Position.X = packet.ReadSingle();
                moveInfo.Position.Y = packet.ReadSingle();
                packet.WriteLine("[{0}] Stationary Position: {1}, O: {2}", index, moveInfo.Position, moveInfo.Orientation);
            }

            if (hasVehicleData)
            {
                moveInfo.VehicleId = packet.ReadUInt32("Vehicle Id", index);
                packet.ReadSingle("Vehicle Orientation", index);
            }

            if (hasGameObjectPosition)
            {
                var tPos = new Vector4();
                if (goTransportGuid[1] != 0) goTransportGuid[1] ^= packet.ReadByte();
                if (goTransportGuid[4] != 0) goTransportGuid[4] ^= packet.ReadByte();
                tPos.Z = packet.ReadSingle();
                if (hasGOTransportTime3)
                    packet.ReadInt32("GO Transport Time 3", index);

                packet.ReadInt32("GO Transport Time", index);
                if (goTransportGuid[5] != 0) goTransportGuid[5] ^= packet.ReadByte();
                if (goTransportGuid[6] != 0) goTransportGuid[6] ^= packet.ReadByte();
                tPos.X = packet.ReadSingle();
                if (goTransportGuid[2] != 0) goTransportGuid[2] ^= packet.ReadByte();
                if (hasGOTransportTime2)
                    packet.ReadInt32("GO Transport Time 2", index);

                packet.ReadByte("GO Transport Seat", index);
                if (goTransportGuid[3] != 0) goTransportGuid[3] ^= packet.ReadByte();
                tPos.Y = packet.ReadSingle();
                tPos.O = packet.ReadSingle();
                if (goTransportGuid[7] != 0) goTransportGuid[7] ^= packet.ReadByte();
                if (goTransportGuid[0] != 0) goTransportGuid[0] ^= packet.ReadByte();

                packet.WriteLine("[{0}] GO Transport Position: {1}", index, tPos);
                packet.WriteLine("[{0}] GO Transport GUID {1}", index, new Guid(BitConverter.ToUInt64(goTransportGuid, 0)));
            }

            if (living)
            {
                if (moveInfo.HasSplineData)
                {
                    if (bit216)
                    {
                        for (var i = 0u; i < splineCount; ++i)
                        {
                            var wp = new Vector3
                            {
                                Y = packet.ReadSingle(),
                                X = packet.ReadSingle(),
                                Z = packet.ReadSingle(),
                            };

                            packet.WriteLine("[{0}][{1}] Spline Waypoint: {2}", index, i, wp);
                        }

                        if (hasSplineDurationMult)
                            packet.ReadSingle("Spline Duration Modifier", index);

                        packet.ReadSingle("Unknown Spline Float 2", index);
                        if (splineType == SplineType.FacingTarget)
                        {
                            if (facingTarget[3] != 0) facingTarget[3] ^= packet.ReadByte();
                            if (facingTarget[4] != 0) facingTarget[4] ^= packet.ReadByte();
                            if (facingTarget[5] != 0) facingTarget[5] ^= packet.ReadByte();
                            if (facingTarget[7] != 0) facingTarget[7] ^= packet.ReadByte();
                            if (facingTarget[2] != 0) facingTarget[2] ^= packet.ReadByte();
                            if (facingTarget[0] != 0) facingTarget[0] ^= packet.ReadByte();
                            if (facingTarget[6] != 0) facingTarget[6] ^= packet.ReadByte();
                            if (facingTarget[1] != 0) facingTarget[1] ^= packet.ReadByte();
                            packet.WriteLine("[{0}] Facing Target GUID {1}", index, new Guid(BitConverter.ToUInt64(facingTarget, 0)));
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

                            packet.WriteLine("[{0}] Facing Spot: {1}", index, point);
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
                    packet.WriteLine("[{0}] Spline Endpoint: {1}", index, endPoint);
                }

                packet.ReadSingle("Pitch Speed", index);
                if (hasTransportData)
                {
                    var tPos = new Vector4();
                    if (transportGuid[4] != 0) transportGuid[4] ^= packet.ReadByte();
                    tPos.Z = packet.ReadSingle();
                    if (transportGuid[7] != 0) transportGuid[7] ^= packet.ReadByte();
                    if (transportGuid[5] != 0) transportGuid[5] ^= packet.ReadByte();
                    if (transportGuid[1] != 0) transportGuid[1] ^= packet.ReadByte();
                    tPos.X = packet.ReadSingle();
                    if (transportGuid[3] != 0) transportGuid[3] ^= packet.ReadByte();
                    if (transportGuid[6] != 0) transportGuid[6] ^= packet.ReadByte();
                    if (hasTransportTime3)
                        packet.ReadInt32("Transport Time 3", index);

                    tPos.Y = packet.ReadSingle();
                    packet.ReadByte("Transport Seat", index);
                    tPos.O = packet.ReadSingle();
                    if (hasTransportTime2)
                        packet.ReadInt32("Transport Time 2", index);

                    if (transportGuid[2] != 0) transportGuid[2] ^= packet.ReadByte();
                    packet.ReadInt32("Transport Time", index);
                    if (transportGuid[0] != 0) transportGuid[0] ^= packet.ReadByte();

                    packet.WriteLine("[{0}] Transport Position: {1}", index, tPos);
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

                if (guid2[7] != 0) guid2[7] ^= packet.ReadByte();
                packet.ReadSingle("SwimBack Speed", index);
                if (guid2[0] != 0) guid2[0] ^= packet.ReadByte();
                if (guid2[5] != 0) guid2[5] ^= packet.ReadByte();
                if (hasUnkUInt)
                    packet.ReadUInt32();

                moveInfo.Position.Z = packet.ReadSingle();
                packet.ReadSingle("Fly Speed", index);
                if (guid2[1] != 0) guid2[1] ^= packet.ReadByte();
                packet.ReadSingle("RunBack Speed", index);
                packet.ReadSingle("Turn Speed", index);
                packet.ReadSingle("Swim Speed", index);
                moveInfo.WalkSpeed = packet.ReadSingle("Walk Speed", index) / 2.5f;
                if (guid2[3] != 0) guid2[3] ^= packet.ReadByte();
                if (guid2[4] != 0) guid2[4] ^= packet.ReadByte();
                if (guid2[2] != 0) guid2[2] ^= packet.ReadByte();
                if (guid2[6] != 0) guid2[6] ^= packet.ReadByte();
                packet.WriteLine("[{0}] GUID 2 {1}", index, new Guid(BitConverter.ToUInt64(guid2, 0)));
                if (unkFloat2)
                    packet.ReadSingle("Unk float +36", index);

                moveInfo.Position.Y = packet.ReadSingle();
                if (hasOrientation)
                    moveInfo.Orientation = packet.ReadSingle();

                moveInfo.RunSpeed = packet.ReadSingle("Run Speed", index) / 7.0f;
                packet.WriteLine("[{0}] Position: {1} Orientation: {2}", index, moveInfo.Position, moveInfo.Orientation);
            }

            if (unkFloats)
            {
                for (int i = 0; i < 16; ++i)
                    packet.ReadSingle("Unk float 456", index, i);

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
                if (attackingTarget[3] != 0) attackingTarget[3] ^= packet.ReadByte();
                if (attackingTarget[5] != 0) attackingTarget[5] ^= packet.ReadByte();
                if (attackingTarget[0] != 0) attackingTarget[0] ^= packet.ReadByte();
                if (attackingTarget[7] != 0) attackingTarget[7] ^= packet.ReadByte();
                if (attackingTarget[2] != 0) attackingTarget[2] ^= packet.ReadByte();
                if (attackingTarget[4] != 0) attackingTarget[4] ^= packet.ReadByte();
                if (attackingTarget[6] != 0) attackingTarget[6] ^= packet.ReadByte();
                if (attackingTarget[1] != 0) attackingTarget[1] ^= packet.ReadByte();
                packet.WriteLine("[{0}] Attacking Target GUID {1}", index, new Guid(BitConverter.ToUInt64(attackingTarget, 0)));
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
            var flags = packet.ReadEnum<UpdateFlag>("[" + index + "] Update Flags", flagsTypeCode);

            if (flags.HasAnyFlag(UpdateFlag.Living))
            {
                moveInfo = MovementHandler.ReadMovementInfo(ref packet, guid, index);
                var moveFlags = moveInfo.Flags;

                for (var i = 0; i < 9; ++i)
                {
                    var speedType = (SpeedType)i;
                    var speed = packet.ReadSingle(speedType + " Speed", index);

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
                    for (var i = 0; i < splineCount; i++)
                        packet.ReadVector3("Spline Waypoint", index, i);

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

                    moveInfo.Position = packet.ReadVector3("[" + index + "] GO Position");
                    packet.ReadVector3("GO Transport Position", index);

                    moveInfo.Orientation = packet.ReadSingle("[" + index + "] GO Orientation");
                    packet.ReadSingle("Corpse Orientation", index);
                }
                else if (flags.HasAnyFlag(UpdateFlag.StationaryObject))
                {
                    moveInfo.Position = packet.ReadVector3();
                    moveInfo.Orientation = packet.ReadSingle();
                    packet.WriteLine("[{0}] Stationary Position: {1}, O: {2}", index, moveInfo.Position, moveInfo.Orientation);
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
                moveInfo.VehicleId = packet.ReadUInt32("[" + index + "] Vehicle ID");
                packet.ReadSingle("Vehicle Orientation", index);
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
            {
                if (flags.HasAnyFlag(UpdateFlag.AnimKits))
                {
                    packet.ReadInt16("Unk Int16", index);
                    packet.ReadInt16("Unk Int16", index);
                    packet.ReadInt16("Unk Int16", index);
                }
            }

            if (flags.HasAnyFlag(UpdateFlag.GORotation))
                moveInfo.Rotation = packet.ReadPackedQuaternion("GO Rotation", index);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
            {
                if (flags.HasAnyFlag(UpdateFlag.TransportUnkArray))
                {
                    var count = packet.ReadByte("Count", index);
                    for (var i = 0; i < count; i++)
                        packet.ReadInt32("Unk Int32", index, count);
                }
            }

            return moveInfo;
        }

        [Parser(Opcode.SMSG_COMPRESSED_UPDATE_OBJECT)]
        public static void HandleCompressedUpdateObject(Packet packet)
        {
            using (var packet2 = packet.Inflate(packet.ReadInt32()))
            {
                HandleUpdateObject(packet2);
            }
        }

        [Parser(Opcode.SMSG_DESTROY_OBJECT)]
        public static void HandleDestroyObject(Packet packet)
        {
            packet.ReadGuid("GUID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                packet.ReadBoolean("Despawn Animation");
        }
    }
}

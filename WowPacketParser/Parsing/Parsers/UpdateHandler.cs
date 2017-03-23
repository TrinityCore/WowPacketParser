using System;
using System.Collections;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Parsing.Parsers
{
    public static class UpdateHandler
    {
        [HasSniffData] // in ReadCreateObjectBlock
        [Parser(Opcode.SMSG_UPDATE_OBJECT)]
        public static void HandleUpdateObject(Packet packet)
        {
            uint map = MovementHandler.CurrentMapId;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                map = packet.Translator.ReadUInt16("Map");

            var count = packet.Translator.ReadUInt32("Count");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_2_9056))
                packet.Translator.ReadBool("Has Transport");

            for (var i = 0; i < count; i++)
            {
                var type = packet.Translator.ReadByte();
                var typeString = ClientVersion.AddedInVersion(ClientType.Cataclysm) ? ((UpdateTypeCataclysm)type).ToString() : ((UpdateType)type).ToString();

                packet.AddValue("UpdateType", typeString, i);
                switch (typeString)
                {
                    case "Values":
                    {
                        var guid = packet.Translator.ReadPackedGuid("GUID", i);

                        WoWObject obj;
                        var updates = ReadValuesUpdateBlock(packet, guid.GetObjectType(), i, false);

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
                        var guid = ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_2_9901) ? packet.Translator.ReadPackedGuid("GUID", i) : packet.Translator.ReadGuid("GUID", i);
                        ReadMovementUpdateBlock(packet, guid, i);
                        // Should we update Storage.Object?
                        break;
                    }
                    case "CreateObject1":
                    case "CreateObject2": // Might != CreateObject1 on Cata
                    {
                        var guid = packet.Translator.ReadPackedGuid("GUID", i);
                        ReadCreateObjectBlock(packet, guid, map, i);
                        break;
                    }
                    case "FarObjects":
                    case "NearObjects":
                    case "DestroyObjects":
                    {
                        ReadObjectsBlock(packet, i);
                        break;
                    }
                }
            }
        }

        private static void ReadCreateObjectBlock(Packet packet, WowGuid guid, uint map, object index)
        {
            var objType = packet.Translator.ReadByteE<ObjectType>("Object Type", index);
            var moves = ReadMovementUpdateBlock(packet, guid, index);
            var updates = ReadValuesUpdateBlock(packet, objType, index, true);

            WoWObject obj;
            switch (objType)
            {
                case ObjectType.Unit:       obj = new Unit(); break;
                case ObjectType.GameObject: obj = new GameObject(); break;
                case ObjectType.Item:       obj = new Item(); break;
                case ObjectType.Player:     obj = new Player(); break;
                case ObjectType.AreaTrigger:obj = new SpellAreaTrigger(); break;
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

        public static void ProcessExistingObject(ref WoWObject obj, WoWObject newObj, WowGuid guid)
        {
            obj.PhaseMask |= newObj.PhaseMask;
            if (guid.GetHighType() == HighGuidType.Creature) // skip if not an unit
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

        public static void ReadObjectsBlock(Packet packet, object index)
        {
            var objCount = packet.Translator.ReadInt32("Object Count", index);
            for (var j = 0; j < objCount; j++)
                packet.Translator.ReadPackedGuid("Object GUID", index, j);
        }

        public static Dictionary<int, UpdateField> ReadValuesUpdateBlock(Packet packet, ObjectType type, object index, bool isCreating)
        {
            var maskSize = packet.Translator.ReadByte();

            var updateMask = new int[maskSize];
            for (var i = 0; i < maskSize; i++)
                updateMask[i] = packet.Translator.ReadInt32();

            var mask = new BitArray(updateMask);
            var dict = new Dictionary<int, UpdateField>();

            int objectEnd = UpdateFields.GetUpdateField(ObjectField.OBJECT_END);
            for (var i = 0; i < mask.Count; ++i)
            {
                if (!mask[i])
                    continue;

                var blockVal = packet.Translator.ReadUpdateField();

                // Don't spam 0 values at create
                if (isCreating && blockVal.UInt32Value == 0)
                    continue;

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
                            if (i < UpdateFields.GetUpdateField(UnitField.UNIT_END) || i < UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_END))
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
                        case ObjectType.AreaTrigger:
                        {
                            key = UpdateFields.GetUpdateFieldName<AreaTriggerField>(i);
                            break;
                        }
                        case ObjectType.SceneObject:
                        {
                            key = UpdateFields.GetUpdateFieldName<SceneObjectField>(i);
                            break;
                        }
                        case ObjectType.Conversation:
                        {
                            key = UpdateFields.GetUpdateFieldName<ConversationField>(i);
                            break;
                        }
                    }
                }

                packet.AddValue(key, value, index);
                dict.Add(i, blockVal);
            }

            objectEnd = UpdateFields.GetUpdateField(ObjectDynamicField.OBJECT_DYNAMIC_END);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_0_4_16016))
            {
                maskSize = packet.Translator.ReadByte();
                updateMask = new int[maskSize];
                for (var i = 0; i < maskSize; i++)
                    updateMask[i] = packet.Translator.ReadInt32();

                mask = new BitArray(updateMask);
                for (var i = 0; i < mask.Count; ++i)
                {
                    if (!mask[i])
                        continue;

                    var flag = packet.Translator.ReadByte();

                    if ((flag & 0x80) != 0)
                        packet.Translator.ReadUInt16();

                    var cnt = flag & 0x7F;
                    var vals = new int[cnt];
                    for (var j = 0; j < cnt; ++j)
                        vals[j] = packet.Translator.ReadInt32();


                    string key = "Dynamic Block Value " + i;
                    if (i < objectEnd)
                        key = UpdateFields.GetUpdateFieldName<ObjectDynamicField>(i);
                    else
                    {
                        switch (type)
                        {
                            case ObjectType.Container:
                            {
                                if (i < UpdateFields.GetUpdateField(ItemDynamicField.ITEM_DYNAMIC_END))
                                    goto case ObjectType.Item;

                                key = UpdateFields.GetUpdateFieldName<ContainerDynamicField>(i);
                                break;
                            }
                            case ObjectType.Item:
                            {
                                key = UpdateFields.GetUpdateFieldName<ItemDynamicField>(i);
                                break;
                            }
                            case ObjectType.Player:
                            {
                                if (i < UpdateFields.GetUpdateField(UnitDynamicField.UNIT_DYNAMIC_END))
                                    goto case ObjectType.Unit;

                                key = UpdateFields.GetUpdateFieldName<PlayerDynamicField>(i);
                                break;
                            }
                            case ObjectType.Unit:
                            {
                                key = UpdateFields.GetUpdateFieldName<UnitDynamicField>(i);
                                break;
                            }
                            case ObjectType.GameObject:
                            {
                                key = UpdateFields.GetUpdateFieldName<GameObjectDynamicField>(i);
                                break;
                            }
                            case ObjectType.DynamicObject:
                            {
                                key = UpdateFields.GetUpdateFieldName<DynamicObjectDynamicField>(i);
                                break;
                            }
                            case ObjectType.Corpse:
                            {
                                key = UpdateFields.GetUpdateFieldName<CorpseDynamicField>(i);
                                break;
                            }
                            case ObjectType.AreaTrigger:
                            {
                                key = UpdateFields.GetUpdateFieldName<AreaTriggerDynamicField>(i);
                                break;
                            }
                            case ObjectType.SceneObject:
                            {
                                key = UpdateFields.GetUpdateFieldName<SceneObjectDynamicField>(i);
                                break;
                            }
                            case ObjectType.Conversation:
                            {
                                key = UpdateFields.GetUpdateFieldName<ConversationDynamicField>(i);
                                break;
                            }
                        }
                    }

                    var fieldMask = new BitArray(vals);
                    for (var j = 0; j < fieldMask.Count; ++j)
                    {
                        if (!fieldMask[j])
                            continue;

                        var blockVal = packet.Translator.ReadUpdateField();
                        string value = blockVal.UInt32Value + "/" + blockVal.SingleValue;
                        packet.AddValue(key, value, index, j);
                    }
                }
            }

            return dict;
        }

        private static MovementInfo ReadMovementUpdateBlock510(Packet packet, WowGuid guid, object index)
        {
            var moveInfo = new MovementInfo();

            var bit654 = packet.Translator.ReadBit("Has bit654", index);
            packet.Translator.ReadBit();
            var hasGameObjectRotation = packet.Translator.ReadBit("Has GameObject Rotation", index);
            var hasAttackingTarget = packet.Translator.ReadBit("Has Attacking Target", index);
            /*var bit2 = */ packet.Translator.ReadBit();
            var bit520 = packet.Translator.ReadBit("Has bit520", index);
            var unkLoopCounter = packet.Translator.ReadBits(24);
            var transport = packet.Translator.ReadBit("Transport", index);
            var hasGameObjectPosition = packet.Translator.ReadBit("Has GameObject Position", index);
            /*var bit653 = */ packet.Translator.ReadBit();
            var bit784 = packet.Translator.ReadBit("Has bit784", index);
            /*var isSelf = */ packet.Translator.ReadBit("Self", index);
            /*var bit1 = */ packet.Translator.ReadBit();
            var living = packet.Translator.ReadBit("Living", index);
            /*var bit3 = */ packet.Translator.ReadBit();
            var bit644 = packet.Translator.ReadBit("Has bit644", index);
            var hasStationaryPosition = packet.Translator.ReadBit("Has Stationary Position", index);
            var hasVehicleData = packet.Translator.ReadBit("Has Vehicle Data", index);
            var bits360 = packet.Translator.ReadBits(21);
            var hasAnimKits = packet.Translator.ReadBit("Has AnimKits", index);
            for (var i = 0; i < bits360; ++i)
                packet.Translator.ReadBits(2);

            var guid2 = new byte[8];
            var facingTargetGuid = new byte[8];
            var unkSplineCounter = 0u;
            var attackingTargetGuid = new byte[8];
            var transportGuid = new byte[8];
            var goTransportGuid = new byte[8];
            var hasFallData = false;
            var hasFallDirection = false;
            var hasTimestamp = false;
            var hasOrientation = false;
            var hasPitch = false;
            var hasSplineElevation = false;
            var hasTransportData = false;
            var hasTransportTime2 = false;
            var hasTransportTime3 = false;
            var hasFullSpline = false;
            var hasSplineVerticalAcceleration = false;
            var hasUnkSplineCounter = false;
            var hasSplineStartTime = false;
            var hasGOTransportTime3 = false;
            var hasGOTransportTime2 = false;
            var hasAnimKit1 = false;
            var hasAnimKit2 = false;
            var hasAnimKit3 = false;
            var splineType = SplineType.Stop;
            var unkLoopCounter2 = 0u;
            var splineCount = 0u;

            var field8 = false;
            var bit540 = false;
            var bit552 = false;
            var bit580 = false;
            var bit624 = false;
            var bit147 = 0u;
            var bit151 = 0u;
            var bit158 = 0u;
            var bit198 = 0u;

            if (living)
            {
                guid2[3] = packet.Translator.ReadBit();
                hasFallData = packet.Translator.ReadBit("Has Fall Data", index);
                hasTimestamp = !packet.Translator.ReadBit("Lacks Timestamp", index);
                packet.Translator.ReadBit(); // bit172
                guid2[2] = packet.Translator.ReadBit();
                packet.Translator.ReadBit(); // bit149
                hasPitch = !packet.Translator.ReadBit("Lacks Pitch", index);
                var hasMoveFlagsExtra = !packet.Translator.ReadBit();
                guid2[4] = packet.Translator.ReadBit();
                guid2[5] = packet.Translator.ReadBit();
                unkLoopCounter2 = packet.Translator.ReadBits(24);
                hasSplineElevation = !packet.Translator.ReadBit();
                field8 = !packet.Translator.ReadBit();
                packet.Translator.ReadBit(); // bit148
                guid2[0] = packet.Translator.ReadBit();
                guid2[6] = packet.Translator.ReadBit();
                guid2[7] = packet.Translator.ReadBit();
                hasTransportData = packet.Translator.ReadBit("Has Transport Data", index);
                hasOrientation = !packet.Translator.ReadBit();

                if (hasTransportData)
                {
                    transportGuid[3] = packet.Translator.ReadBit();
                    transportGuid[0] = packet.Translator.ReadBit();
                    transportGuid[4] = packet.Translator.ReadBit();
                    transportGuid[5] = packet.Translator.ReadBit();
                    transportGuid[2] = packet.Translator.ReadBit();
                    transportGuid[7] = packet.Translator.ReadBit();
                    transportGuid[1] = packet.Translator.ReadBit();
                    hasTransportTime2 = packet.Translator.ReadBit();
                    transportGuid[6] = packet.Translator.ReadBit();
                    hasTransportTime3 = packet.Translator.ReadBit();
                }

                if (hasMoveFlagsExtra)
                    moveInfo.FlagsExtra = packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13, index);

                var hasMovementFlags = !packet.Translator.ReadBit();
                guid2[1] = packet.Translator.ReadBit();
                if (hasFallData)
                    hasFallDirection = packet.Translator.ReadBit("Has Fall Direction", index);

                moveInfo.HasSplineData = packet.Translator.ReadBit("Has Spline Data", index);
                if (hasMovementFlags)
                    moveInfo.Flags = packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30, index);

                if (moveInfo.HasSplineData)
                {
                    hasFullSpline = packet.Translator.ReadBit("Has extended spline data", index);
                    if (hasFullSpline)
                    {
                        hasSplineStartTime = packet.Translator.ReadBit();
                        splineCount = packet.Translator.ReadBits("Spline Waypoints", 22, index);
                        /*var splineFlags = */ packet.Translator.ReadBitsE<SplineFlag434>("Spline flags", 25, index);
                        var bits57 = packet.Translator.ReadBits(2);
                        switch (bits57)
                        {
                            case 1:
                                splineType = SplineType.FacingTarget;
                                break;
                            case 0:
                                splineType = SplineType.FacingAngle;
                                break;
                            case 2:
                                splineType = SplineType.Normal;
                                break;
                            case 3:
                                splineType = SplineType.FacingSpot;
                                break;
                        }

                        if (splineType == SplineType.FacingTarget)
                            facingTargetGuid = packet.Translator.StartBitStream(0, 1, 6, 5, 2, 3, 4, 7);

                        hasUnkSplineCounter = packet.Translator.ReadBit();
                        if (hasUnkSplineCounter)
                        {
                            unkSplineCounter = packet.Translator.ReadBits(23);
                            packet.Translator.ReadBits(2);
                        }

                        /*var splineMode = */ packet.Translator.ReadBitsE<SplineMode>("Spline Mode", 2, index);
                        hasSplineVerticalAcceleration = packet.Translator.ReadBit();
                    }
                }
            }

            if (hasGameObjectPosition)
            {
                hasGOTransportTime3 = packet.Translator.ReadBit();
                goTransportGuid[3] = packet.Translator.ReadBit();
                goTransportGuid[1] = packet.Translator.ReadBit();
                goTransportGuid[4] = packet.Translator.ReadBit();
                goTransportGuid[7] = packet.Translator.ReadBit();
                goTransportGuid[2] = packet.Translator.ReadBit();
                goTransportGuid[5] = packet.Translator.ReadBit();
                goTransportGuid[0] = packet.Translator.ReadBit();
                goTransportGuid[6] = packet.Translator.ReadBit();
                hasGOTransportTime2 = packet.Translator.ReadBit();
            }

            if (bit654)
                packet.Translator.ReadBits(9);

            if (bit520)
            {
                bit540 = packet.Translator.ReadBit("bit540", index);
                packet.Translator.ReadBit("bit536", index);
                bit552 = packet.Translator.ReadBit("bit552", index);
                packet.Translator.ReadBit("bit539", index);
                bit624 = packet.Translator.ReadBit("bit624", index);
                bit580 = packet.Translator.ReadBit("bit580", index);
                packet.Translator.ReadBit("bit537", index);

                if (bit580)
                {
                    bit147 = packet.Translator.ReadBits(23);
                    bit151 = packet.Translator.ReadBits(23);
                }

                if (bit624)
                    bit158 = packet.Translator.ReadBits(22);

                packet.Translator.ReadBit("bit538", index);
            }

            if (hasAttackingTarget)
                attackingTargetGuid = packet.Translator.StartBitStream(2, 6, 7, 1, 0, 3, 4, 5);

            if (bit784)
                bit198 = packet.Translator.ReadBits(24);

            if (hasAnimKits)
            {
                hasAnimKit3 = !packet.Translator.ReadBit();
                hasAnimKit1 = !packet.Translator.ReadBit();
                hasAnimKit2 = !packet.Translator.ReadBit();
            }

            packet.Translator.ResetBitReader();

            // Reading data
            for (var i = 0; i < bits360; ++i)
            {
                packet.Translator.ReadSingle();
                packet.Translator.ReadUInt32();
                packet.Translator.ReadSingle();
                packet.Translator.ReadUInt32();
                packet.Translator.ReadSingle();
                packet.Translator.ReadSingle();
            }

            for (var i = 0u; i < unkLoopCounter; ++i)
                packet.Translator.ReadUInt32("Unk UInt32", index, (int)i);

            if (living)
            {
                packet.Translator.ReadSingle("FlyBack Speed", index);
                if (moveInfo.HasSplineData)
                {
                    if (hasFullSpline)
                    {
                        if (hasUnkSplineCounter)
                        {
                            for (var i = 0; i < unkSplineCounter; ++i)
                            {
                                packet.Translator.ReadSingle("Unk Spline Float1", index, i);
                                packet.Translator.ReadSingle("Unk Spline Float2", index, i);
                            }
                        }

                        if (splineType == SplineType.FacingTarget)
                        {
                            packet.Translator.ParseBitStream(facingTargetGuid, 3, 2, 0, 5, 6, 7, 4, 1);
                            packet.Translator.WriteGuid("Facing Target GUID", facingTargetGuid, index);
                        }

                        packet.Translator.ReadUInt32("Spline Time", index);
                        packet.Translator.ReadUInt32("Spline Full Time", index);

                        if (hasSplineVerticalAcceleration)
                            packet.Translator.ReadSingle("Spline Vertical Acceleration", index);

                        packet.Translator.ReadSingle("Spline Duration Multiplier Next", index);
                        packet.Translator.ReadSingle("Spline Duration Multiplier", index);

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

                        if (hasSplineStartTime)
                            packet.Translator.ReadUInt32("Spline Start Time", index);

                        for (var i = 0u; i < splineCount; ++i)
                        {
                            var wp = new Vector3
                            {
                                Y = packet.Translator.ReadSingle(),
                                Z = packet.Translator.ReadSingle(),
                                X = packet.Translator.ReadSingle()
                            };

                            packet.AddValue("Spline Waypoint", wp, index, i);
                        }

                        if (splineType == SplineType.FacingAngle)
                            packet.Translator.ReadSingle("Facing Angle", index);
                    }

                    var endPoint = new Vector3
                    {
                        Y = packet.Translator.ReadSingle(),
                        X = packet.Translator.ReadSingle(),
                        Z = packet.Translator.ReadSingle()
                    };

                    packet.Translator.ReadUInt32("Spline Id", index);
                    packet.AddValue("Spline Endpoint", endPoint, index);
                }

                packet.Translator.ReadSingle("Swim Speed", index);

                if (hasFallData)
                {
                    if (hasFallDirection)
                    {
                        packet.Translator.ReadSingle("Jump Velocity", index);
                        packet.Translator.ReadSingle("Jump Cos", index);
                        packet.Translator.ReadSingle("Jump Sin", index);
                    }

                    packet.Translator.ReadSingle("Fall Start Velocity", index);
                    packet.Translator.ReadInt32("Time Fallen", index);
                }

                if (hasTransportData)
                {
                    moveInfo.TransportOffset.Z = packet.Translator.ReadSingle();
                    packet.Translator.ReadXORByte(transportGuid, 4);
                    moveInfo.TransportOffset.X = packet.Translator.ReadSingle();
                    if (hasTransportTime3)
                        packet.Translator.ReadUInt32("Transport Time 3", index);

                    packet.Translator.ReadXORByte(transportGuid, 6);
                    packet.Translator.ReadXORByte(transportGuid, 5);
                    packet.Translator.ReadXORByte(transportGuid, 1);
                    moveInfo.TransportOffset.O = packet.Translator.ReadSingle();
                    moveInfo.TransportOffset.Y = packet.Translator.ReadSingle();
                    var seat = packet.Translator.ReadSByte("Transport Seat", index);
                    packet.Translator.ReadXORByte(transportGuid, 7);
                    if (hasTransportTime2)
                        packet.Translator.ReadUInt32("Transport Time 2", index);

                    packet.Translator.ReadUInt32("Transport Time", index);
                    packet.Translator.ReadXORByte(transportGuid, 0);
                    packet.Translator.ReadXORByte(transportGuid, 2);
                    packet.Translator.ReadXORByte(transportGuid, 3);

                    moveInfo.TransportGuid = packet.Translator.WriteGuid("Transport GUID", transportGuid, index);
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

                packet.Translator.ReadXORByte(guid2, 1);
                packet.Translator.ReadSingle("Turn Speed", index);
                moveInfo.Position.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(guid2, 3);
                moveInfo.Position.Z = packet.Translator.ReadSingle();
                if (hasOrientation)
                    moveInfo.Orientation = packet.Translator.ReadSingle();

                packet.Translator.ReadSingle("Run Back Speed", index);
                if (hasSplineElevation)
                    packet.Translator.ReadSingle("Spline Elevation", index);

                packet.Translator.ReadXORByte(guid2, 0);
                packet.Translator.ReadXORByte(guid2, 6);
                for (var i = 0u; i < unkLoopCounter2; ++i)
                    packet.Translator.ReadUInt32("Unk2 UInt32", index, (int)i);

                moveInfo.Position.X = packet.Translator.ReadSingle();
                if (hasTimestamp)
                    packet.Translator.ReadUInt32("Time", index);

                moveInfo.WalkSpeed = packet.Translator.ReadSingle("Walk Speed", index) / 2.5f;
                if (hasPitch)
                    packet.Translator.ReadSingle("Pitch", index);

                packet.Translator.ReadXORByte(guid2, 5);
                if (field8)
                    packet.Translator.ReadUInt32("Unk UInt32", index);

                packet.Translator.ReadSingle("Pitch Speed", index);
                packet.Translator.ReadXORByte(guid2, 2);
                moveInfo.RunSpeed = packet.Translator.ReadSingle("Run Speed", index) / 7.0f;
                packet.Translator.ReadXORByte(guid2, 7);
                packet.Translator.ReadSingle("SwimBack Speed", index);
                packet.Translator.ReadXORByte(guid2, 4);
                packet.Translator.ReadSingle("Fly Speed", index);

                packet.Translator.WriteGuid("GUID 2", guid2, index);
                packet.AddValue("Position", moveInfo.Position, index);
                packet.AddValue("Orientation", moveInfo.Orientation, index);
            }

            if (bit520)
            {
                if (bit580)
                {
                    packet.Translator.ReadSingle("field154", index);
                    packet.Translator.ReadSingle("field155", index);

                    for (var i = 0; i < bit147; ++i)
                    {
                        packet.Translator.ReadSingle();
                        packet.Translator.ReadSingle();
                    }

                    for (var i = 0; i < bit151; ++i)
                    {
                        packet.Translator.ReadSingle();
                        packet.Translator.ReadSingle();
                    }
                }

                if (bit540)
                {
                    packet.Translator.ReadSingle("field136", index);
                    packet.Translator.ReadSingle("field134", index);
                }

                if (bit552)
                {
                    packet.Translator.ReadSingle("field143", index);
                    packet.Translator.ReadSingle("field141", index);
                    packet.Translator.ReadSingle("field142", index);
                    packet.Translator.ReadSingle("field140", index);
                    packet.Translator.ReadSingle("field139", index);
                    packet.Translator.ReadSingle("field144", index);
                }

                packet.Translator.ReadSingle("field132", index);
                if (bit624)
                {
                    for (var i = 0; i < bit158; ++i)
                    {
                        packet.Translator.ReadSingle();
                        packet.Translator.ReadSingle();
                        packet.Translator.ReadSingle();
                    }
                }

                packet.Translator.ReadSingle("field133", index);
                packet.Translator.ReadSingle("field131", index);
            }

            if (hasAttackingTarget)
            {
                packet.Translator.ParseBitStream(attackingTargetGuid, 3, 4, 2, 5, 1, 6, 7, 0);
                packet.Translator.WriteGuid("Attacking Target GUID", attackingTargetGuid, index);
            }

            if (hasStationaryPosition)
            {
                moveInfo.Position.X = packet.Translator.ReadSingle();
                moveInfo.Orientation = packet.Translator.ReadSingle("Stationary Orientation", index);
                moveInfo.Position.Y = packet.Translator.ReadSingle();
                moveInfo.Position.Z = packet.Translator.ReadSingle();
                packet.AddValue("Stationary Position", moveInfo.Position,index );
            }

            if (hasGameObjectPosition)
            {
                packet.Translator.ReadXORByte(goTransportGuid, 3);
                packet.Translator.ReadXORByte(goTransportGuid, 1);
                packet.Translator.ReadSByte("GO Transport Seat", index);
                moveInfo.TransportOffset.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(goTransportGuid, 2);
                packet.Translator.ReadXORByte(goTransportGuid, 7);
                if (hasGOTransportTime3)
                    packet.Translator.ReadUInt32("GO Transport Time 3", index);

                packet.Translator.ReadXORByte(goTransportGuid, 6);
                if (hasGOTransportTime2)
                    packet.Translator.ReadUInt32("GO Transport Time 2", index);

                packet.Translator.ReadUInt32("GO Transport Time", index);
                moveInfo.TransportOffset.Y = packet.Translator.ReadSingle();
                moveInfo.TransportOffset.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(goTransportGuid, 0);
                packet.Translator.ReadXORByte(goTransportGuid, 4);
                packet.Translator.ReadXORByte(goTransportGuid, 5);
                moveInfo.TransportOffset.O = packet.Translator.ReadSingle();

                moveInfo.TransportGuid = new WowGuid64(BitConverter.ToUInt64(goTransportGuid, 0));
                packet.AddValue("GO Transport GUID", moveInfo.TransportGuid, index);
                packet.AddValue("GO Transport Position", moveInfo.TransportOffset, index);
            }

            if (hasAnimKits)
            {
                if (hasAnimKit3)
                    packet.Translator.ReadUInt16("Anim Kit 3", index);
                if (hasAnimKit1)
                    packet.Translator.ReadUInt16("Anim Kit 1", index);
                if (hasAnimKit2)
                    packet.Translator.ReadUInt16("Anim Kit 2", index);
            }

            if (hasVehicleData)
            {
                packet.Translator.ReadSingle("Vehicle Orientation", index);
                moveInfo.VehicleId = packet.Translator.ReadUInt32("Vehicle Id", index);
            }

            if (transport)
                packet.Translator.ReadUInt32("Transport Path Timer", index);

            if (bit644)
                packet.Translator.ReadUInt32("field162", index);

            if (bit784)
            {
                for (var i = 0; i < bit198; ++i)
                    packet.Translator.ReadUInt32();
            }

            if (hasGameObjectRotation)
                moveInfo.Rotation = packet.Translator.ReadPackedQuaternion("GameObject Rotation", index);

            return moveInfo;
        }

        private static MovementInfo ReadMovementUpdateBlock504(Packet packet, WowGuid guid, object index)
        {
            var moveInfo = new MovementInfo();

            // bits
            var hasAttackingTarget = packet.Translator.ReadBit("Has Attacking Target", index);
            var hasVehicleData = packet.Translator.ReadBit("Has Vehicle Data", index);
            var unkLoopCounter = packet.Translator.ReadBits(24);
            var bit284 = packet.Translator.ReadBit();
            var hasGameObjectPosition = packet.Translator.ReadBit("Has GameObject Position", index);
            var hasStationaryPosition = packet.Translator.ReadBit("Has Stationary Position", index);
            var bits16C = packet.Translator.ReadBits(21);
            var transport = packet.Translator.ReadBit("Transport", index);
            var bit208 = packet.Translator.ReadBit();
            /*var bit 28C =*/ packet.Translator.ReadBit();
            var living = packet.Translator.ReadBit("Living", index);
            /*var bit1 =*/ packet.Translator.ReadBit();
            var bit28D = packet.Translator.ReadBit();
            /*var bit2 =*/ packet.Translator.ReadBit();
            var hasGameObjectRotation = packet.Translator.ReadBit("Has GameObject Rotation", index);
            var hasAnimKits = packet.Translator.ReadBit("Has AnimKits", index);
            /*var bit3 =*/ packet.Translator.ReadBit();
            packet.Translator.ReadBit("Self", index);
            for (var i = 0; i < bits16C; ++i)
                packet.Translator.ReadBits(2);

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
            var hasFullSpline = false;
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
            var bit228 = false;
            var bit21C = false;
            var bit278 = 0u;
            var bit244 = false;
            var bit24C = 0u;
            var bit25C = 0u;
            var field9C = 0u;
            var hasFieldA8 = false;
            var unkSplineCounter = 0u;

            if (hasGameObjectPosition)
            {
                goTransportGuid[4] = packet.Translator.ReadBit();
                goTransportGuid[3] = packet.Translator.ReadBit();
                goTransportGuid[6] = packet.Translator.ReadBit();
                goTransportGuid[0] = packet.Translator.ReadBit();
                goTransportGuid[5] = packet.Translator.ReadBit();
                goTransportGuid[1] = packet.Translator.ReadBit();
                hasGOTransportTime2 = packet.Translator.ReadBit();
                hasGOTransportTime3 = packet.Translator.ReadBit();
                goTransportGuid[2] = packet.Translator.ReadBit();
                goTransportGuid[7] = packet.Translator.ReadBit();
            }

            if (bit208)
            {
                bit228 = packet.Translator.ReadBit();
                var bit270 = packet.Translator.ReadBit();
                packet.Translator.ReadBit();   // bit219
                packet.Translator.ReadBit();   // bit21A
                bit21C = packet.Translator.ReadBit();
                if (bit270)
                    bit278 = packet.Translator.ReadBits(22);

                bit244 = packet.Translator.ReadBit();
                if (bit244)
                {
                    bit24C = packet.Translator.ReadBits(23);
                    bit25C = packet.Translator.ReadBits(23);
                }

                packet.Translator.ReadBit();   // bit218
            }

            if (living)
            {
                guid2[3] = packet.Translator.ReadBit();
                moveInfo.HasSplineData = packet.Translator.ReadBit("Has Spline Data", index);
                field9C = packet.Translator.ReadBits(24);
                guid2[4] = packet.Translator.ReadBit();
                hasPitch = !packet.Translator.ReadBit("Lacks Pitch", index);
                hasTransportData = packet.Translator.ReadBit("Has Transport Data", index);
                hasFallData = packet.Translator.ReadBit("Has Fall Data", index);
                hasTimestamp = !packet.Translator.ReadBit("Lacks Timestamp", index);
                if (hasTransportData)
                {
                    transportGuid[3] = packet.Translator.ReadBit();
                    hasTransportTime3 = packet.Translator.ReadBit();
                    transportGuid[7] = packet.Translator.ReadBit();
                    transportGuid[0] = packet.Translator.ReadBit();
                    transportGuid[6] = packet.Translator.ReadBit();
                    hasTransportTime2 = packet.Translator.ReadBit();
                    transportGuid[4] = packet.Translator.ReadBit();
                    transportGuid[1] = packet.Translator.ReadBit();
                    transportGuid[2] = packet.Translator.ReadBit();
                    transportGuid[5] = packet.Translator.ReadBit();
                }

                hasFieldA8 = !packet.Translator.ReadBit();
                guid2[7] = packet.Translator.ReadBit();
                var hasMoveFlagsExtra = !packet.Translator.ReadBit();
                guid2[0] = packet.Translator.ReadBit();
                packet.Translator.ReadBit();
                guid2[5] = packet.Translator.ReadBit();
                if (hasMoveFlagsExtra)
                    moveInfo.FlagsExtra = packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13, index);

                guid2[2] = packet.Translator.ReadBit();
                guid2[6] = packet.Translator.ReadBit();
                var hasMovementFlags = !packet.Translator.ReadBit();
                if (hasFallData)
                    hasFallDirection = packet.Translator.ReadBit("Has Fall Direction", index);

                if (hasMovementFlags)
                    moveInfo.Flags = packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30, index);

                hasOrientation = !packet.Translator.ReadBit();
                packet.Translator.ReadBit();
                packet.Translator.ReadBit();

                if (moveInfo.HasSplineData)
                {
                    hasFullSpline = packet.Translator.ReadBit("Has extended spline data", index);
                    if (hasFullSpline)
                    {
                        hasSplineVerticalAcceleration = packet.Translator.ReadBit();
                        /*var splineMode =*/ packet.Translator.ReadBitsE<SplineMode>("Spline Mode", 2, index);
                        var bit134 = packet.Translator.ReadBit();
                        if (bit134)
                        {
                            unkSplineCounter = packet.Translator.ReadBits(23);
                            packet.Translator.ReadBits(2);
                        }

                        /*splineFlags =*/ packet.Translator.ReadBits("Spline flags", 25, index);
                        hasSplineStartTime = packet.Translator.ReadBit();
                        splineCount = packet.Translator.ReadBits("Spline Waypoints", 22, index);
                        var bits57 = packet.Translator.ReadBits(2);
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
                            facingTargetGuid = packet.Translator.StartBitStream(4, 5, 0, 7, 1, 3, 2, 6);

                        packet.AddValue("Spline type", splineType, index);
                    }
                }

                guid2[1] = packet.Translator.ReadBit();
                hasSplineElevation = !packet.Translator.ReadBit();
            }

            if (hasAttackingTarget)
                attackingTargetGuid = packet.Translator.StartBitStream(2, 6, 5, 1, 7, 3, 4, 0);

            if (hasAnimKits)
            {
                hasAnimKit2 = !packet.Translator.ReadBit();
                hasAnimKit3 = !packet.Translator.ReadBit();
                hasAnimKit1 = !packet.Translator.ReadBit();
            }

            if (bit28D)
                packet.Translator.ReadBits(9);

            packet.Translator.ResetBitReader();

            // Reading data
            for (var i = 0; i < bits16C; ++i)
            {
                packet.Translator.ReadUInt32();
                packet.Translator.ReadSingle();
                packet.Translator.ReadSingle();
                packet.Translator.ReadUInt32();
                packet.Translator.ReadSingle();
                packet.Translator.ReadSingle();
            }

            for (var i = 0u; i < unkLoopCounter; ++i)
                packet.Translator.ReadUInt32("Unk UInt32", index, (int)i);

            if (living)
            {
                if (moveInfo.HasSplineData)
                {
                    if (hasFullSpline)
                    {
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
                        else if (splineType == SplineType.FacingTarget)
                        {
                            packet.Translator.ParseBitStream(facingTargetGuid, 5, 6, 0, 1, 2, 4, 7, 3);
                            packet.Translator.WriteGuid("Facing Target GUID", facingTargetGuid, index);
                        }

                        packet.Translator.ReadUInt32("Spline Time", index);
                        if (hasSplineVerticalAcceleration)
                            packet.Translator.ReadSingle("Spline Vertical Acceleration", index);

                        if (hasSplineStartTime)
                            packet.Translator.ReadUInt32("Spline Start time", index);

                        for (var i = 0; i < unkSplineCounter; ++i)
                        {
                            packet.Translator.ReadSingle();
                            packet.Translator.ReadSingle();
                        }

                        if (splineType == SplineType.FacingAngle)
                            packet.Translator.ReadSingle("Facing Angle", index);

                        for (var i = 0u; i < splineCount; ++i)
                        {
                            var wp = new Vector3
                            {
                                X = packet.Translator.ReadSingle(),
                                Y = packet.Translator.ReadSingle(),
                                Z = packet.Translator.ReadSingle()
                            };

                            packet.AddValue("Spline Waypoint", wp, index, i);
                        }

                        packet.Translator.ReadSingle("Spline Duration Multiplier", index);
                        packet.Translator.ReadUInt32("Spline Full Time", index);
                        packet.Translator.ReadSingle("Spline Duration Multiplier Next", index);
                    }

                    var endPoint = new Vector3
                    {
                        Z = packet.Translator.ReadSingle()
                    };
                    packet.Translator.ReadUInt32("Spline Id", index);
                    endPoint.X = packet.Translator.ReadSingle();
                    endPoint.Y = packet.Translator.ReadSingle();

                    packet.AddValue("Spline Endpoint", endPoint, index);
                }

                for (var i = 0; i < field9C; ++i)
                    packet.Translator.ReadUInt32();

                moveInfo.WalkSpeed = packet.Translator.ReadSingle("Walk Speed", index) / 2.5f;
                if (hasTransportData)
                {
                    packet.Translator.ReadXORByte(transportGuid, 4);
                    packet.Translator.ReadXORByte(transportGuid, 0);
                    moveInfo.TransportOffset.Y = packet.Translator.ReadSingle();
                    moveInfo.TransportOffset.X = packet.Translator.ReadSingle();
                    var seat = packet.Translator.ReadSByte("Transport Seat", index);
                    packet.Translator.ReadXORByte(transportGuid, 7);
                    packet.Translator.ReadXORByte(transportGuid, 3);
                    if (hasTransportTime3)
                        packet.Translator.ReadUInt32("Transport Time 3", index);

                    packet.Translator.ReadXORByte(transportGuid, 6);
                    moveInfo.TransportOffset.O = packet.Translator.ReadSingle();
                    packet.Translator.ReadUInt32("Transport Time", index);
                    packet.Translator.ReadXORByte(transportGuid, 2);
                    packet.Translator.ReadXORByte(transportGuid, 1);
                    moveInfo.TransportOffset.Z = packet.Translator.ReadSingle();
                    packet.Translator.ReadXORByte(transportGuid, 5);
                    if (hasTransportTime2)
                        packet.Translator.ReadUInt32("Transport Time 2", index);

                    moveInfo.TransportGuid = new WowGuid64(BitConverter.ToUInt64(transportGuid, 0));
                    packet.AddValue("Transport GUID",  moveInfo.TransportGuid, index);
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

                packet.Translator.ReadXORByte(guid2, 2);
                if (hasFallData)
                {
                    packet.Translator.ReadInt32("Time Fallen", index);
                    if (hasFallDirection)
                    {
                        packet.Translator.ReadSingle("Jump Sin", index);
                        packet.Translator.ReadSingle("Jump Cos", index);
                        packet.Translator.ReadSingle("Jump Velocity", index);
                    }

                    packet.Translator.ReadSingle("Fall Start Velocity", index);
                }

                packet.Translator.ReadXORByte(guid2, 7);
                if (hasTimestamp)
                    packet.Translator.ReadUInt32("Time", index);

                packet.Translator.ReadSingle("Fly Speed", index);
                moveInfo.Position.X = packet.Translator.ReadSingle();
                if (hasFieldA8)
                    packet.Translator.ReadUInt32();

                moveInfo.Position.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(guid2, 5);
                moveInfo.Position.Z = packet.Translator.ReadSingle();
                if (hasPitch)
                    packet.Translator.ReadSingle("Pitch", index);

                packet.Translator.ReadXORByte(guid2, 3);
                packet.Translator.ReadXORByte(guid2, 6);
                packet.Translator.ReadXORByte(guid2, 1);
                if (hasSplineElevation)
                    packet.Translator.ReadSingle("Spline Elevation", index);

                packet.Translator.ReadSingle("Turn Speed", index);
                packet.Translator.ReadSingle("Pitch Speed", index);
                moveInfo.RunSpeed = packet.Translator.ReadSingle("Run Speed", index) / 7.0f;
                if (hasOrientation)
                    moveInfo.Orientation = packet.Translator.ReadSingle();

                packet.Translator.ReadXORByte(guid2, 4);
                packet.Translator.ReadSingle("Swim Speed", index);
                packet.Translator.ReadSingle("SwimBack Speed", index);
                packet.Translator.ReadSingle("FlyBack Speed", index);
                packet.Translator.ReadSingle("RunBack Speed", index);
                packet.Translator.ReadXORByte(guid2, 0);

                packet.Translator.WriteGuid("GUID 2", guid2, index);
                packet.AddValue("Position:", moveInfo.Position, index);
                packet.AddValue("Orientation", moveInfo.Orientation, index);
            }

            if (bit208)
            {
                if (bit228)
                {
                    packet.Translator.ReadSingle();
                    packet.Translator.ReadSingle();
                    packet.Translator.ReadSingle();
                    packet.Translator.ReadSingle();
                    packet.Translator.ReadSingle();
                    packet.Translator.ReadSingle();
                }

                if (bit21C)
                {
                    packet.Translator.ReadSingle();
                    packet.Translator.ReadSingle();
                }

                if (bit244)
                {
                    for (var i = 0; i < bit24C; ++i)
                    {
                        packet.Translator.ReadSingle();
                        packet.Translator.ReadSingle();
                    }

                    packet.Translator.ReadSingle();
                    for (var i = 0; i < bit25C; ++i)
                    {
                        packet.Translator.ReadSingle();
                        packet.Translator.ReadSingle();
                    }

                    packet.Translator.ReadSingle();
                }

                packet.Translator.ReadUInt32();
                for (var i = 0; i < bit278; ++i)
                {
                    packet.Translator.ReadSingle();
                    packet.Translator.ReadSingle();
                    packet.Translator.ReadSingle();
                }

                packet.Translator.ReadSingle();
                packet.Translator.ReadSingle();
            }

            if (hasGameObjectPosition)
            {
                packet.Translator.ReadXORByte(goTransportGuid, 7);
                packet.Translator.ReadXORByte(goTransportGuid, 3);
                packet.Translator.ReadXORByte(goTransportGuid, 5);
                moveInfo.TransportOffset.O = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(goTransportGuid, 6);
                packet.Translator.ReadXORByte(goTransportGuid, 0);
                packet.Translator.ReadXORByte(goTransportGuid, 2);
                packet.Translator.ReadUInt32("GO Transport Time", index);
                if (hasGOTransportTime3)
                    packet.Translator.ReadUInt32("GO Transport Time 3", index);

                packet.Translator.ReadXORByte(goTransportGuid, 1);
                moveInfo.TransportOffset.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadSByte("GO Transport Seat", index);
                if (hasGOTransportTime2)
                    packet.Translator.ReadUInt32("GO Transport Time 2", index);

                moveInfo.TransportOffset.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(goTransportGuid, 4);
                moveInfo.TransportOffset.Y = packet.Translator.ReadSingle();

                moveInfo.TransportGuid = new WowGuid64(BitConverter.ToUInt64(goTransportGuid, 0));
                packet.AddValue("GO Transport GUID", moveInfo.TransportGuid, index);
                packet.AddValue("GO Transport Position", moveInfo.TransportOffset, index);
            }

            if (hasStationaryPosition)
            {
                moveInfo.Position.Y = packet.Translator.ReadSingle();
                moveInfo.Position.Z = packet.Translator.ReadSingle();
                moveInfo.Position.X = packet.Translator.ReadSingle();
                packet.AddValue("Stationary Position", moveInfo.Position, index);
                moveInfo.Orientation = packet.Translator.ReadSingle("Stationary Orientation", index);
            }

            if (hasAttackingTarget)
            {
                packet.Translator.ParseBitStream(attackingTargetGuid, 3, 6, 4, 1, 5, 7, 0, 2);
                packet.Translator.WriteGuid("Attacking Target GUID", attackingTargetGuid, index);
            }

            if (transport)
                packet.Translator.ReadUInt32("Transport path timer", index);

            if (hasGameObjectRotation)
                moveInfo.Rotation = packet.Translator.ReadPackedQuaternion("GameObject Rotation", index);

            if (hasVehicleData)
            {
                moveInfo.VehicleId = packet.Translator.ReadUInt32("Vehicle Id", index);
                packet.Translator.ReadSingle("Vehicle Orientation", index);
            }

            if (hasAnimKits)
            {
                if (hasAnimKit2)
                    packet.Translator.ReadUInt16("Anim Kit 2", index);
                if (hasAnimKit3)
                    packet.Translator.ReadUInt16("Anim Kit 3", index);
                if (hasAnimKit1)
                    packet.Translator.ReadUInt16("Anim Kit 1", index);
            }

            if (bit284)
                packet.Translator.ReadUInt32();

            return moveInfo;
        }

        private static MovementInfo ReadMovementUpdateBlock433(Packet packet, WowGuid guid, object index)
        {
            var moveInfo = new MovementInfo();

            bool living = packet.Translator.ReadBit("Living", index);
            bool hasAttackingTarget = packet.Translator.ReadBit("Has Attacking Target", index);
            bool hasVehicleData = packet.Translator.ReadBit("Has Vehicle Data", index);
            uint unkLoopCounter = packet.Translator.ReadBits(24);
            bool hasStationaryPosition = packet.Translator.ReadBit("Has Stationary Position", index);
            /*bool bit1 =*/ packet.Translator.ReadBit();
            /*bool bit4 =*/ packet.Translator.ReadBit();
            bool unkInt = packet.Translator.ReadBit();
            bool unkFloats = packet.Translator.ReadBit();
            /*bool bit2 =*/ packet.Translator.ReadBit();
            /*bool bit0 =*/ packet.Translator.ReadBit();
            /*bool bit3 =*/ packet.Translator.ReadBit();
            bool hasGameObjectPosition = packet.Translator.ReadBit("Has GameObject Position", index);
            bool hasAnimKits = packet.Translator.ReadBit("Has AnimKits", index);
            bool hasGORotation = packet.Translator.ReadBit("Has GameObject Rotation", index);
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
                guid2[4] = packet.Translator.ReadBit();
                /*bool bit149 =*/ packet.Translator.ReadBit();
                guid2[5] = packet.Translator.ReadBit();
                unkFloat1 = !packet.Translator.ReadBit();
                hasFallData = packet.Translator.ReadBit("Has Fall Data", index);
                unkFloat2 = !packet.Translator.ReadBit();
                guid2[6] = packet.Translator.ReadBit();
                moveInfo.HasSplineData = packet.Translator.ReadBit("Has Spline Data", index);
                if (moveInfo.HasSplineData)
                {
                    bit216 = packet.Translator.ReadBit();
                    if (bit216)
                    {
                        bit256 = packet.Translator.ReadBit();
                        /*splineMode =*/ packet.Translator.ReadBits(2);
                        hasSplineDurationMult = packet.Translator.ReadBit();
                        uint bits57 = packet.Translator.ReadBits(2);
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
                            facingTarget = packet.Translator.StartBitStream(0, 2, 7, 1, 6, 3, 4, 5);

                        /*splineFlags =*/ packet.Translator.ReadBitsE<SplineFlag422>("Spline Flags", 25, index);
                        splineCount = packet.Translator.ReadBits(22);
                    }
                }

                hasTransportData = packet.Translator.ReadBit("Has Transport Data", index);
                guid2[1] = packet.Translator.ReadBit();
                /*bit148 =*/ packet.Translator.ReadBit();
                if (hasTransportData)
                {
                    hasTransportTime2 = packet.Translator.ReadBit();
                    transportGuid = packet.Translator.StartBitStream(0, 7, 2, 6, 5, 4, 1, 3);
                    hasTransportTime3 = packet.Translator.ReadBit();
                }

                guid2[2] = packet.Translator.ReadBit();
                if (hasFallData)
                    hasFallDirection = packet.Translator.ReadBit("Has Fall Direction", index);

                bool hasMovementFlags = !packet.Translator.ReadBit();
                bool hasExtraMovementFlags = !packet.Translator.ReadBit();
                hasUnkUInt = !packet.Translator.ReadBit();
                guid2[7] = packet.Translator.ReadBit();
                if (hasExtraMovementFlags)
                    moveInfo.FlagsExtra = packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 12, index);

                guid2[0] = packet.Translator.ReadBit();
                if (hasMovementFlags)
                    moveInfo.Flags = packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30, index);

                guid2[3] = packet.Translator.ReadBit();
                hasOrientation = !packet.Translator.ReadBit();
            }

            if (hasAttackingTarget)
                attackingTarget = packet.Translator.StartBitStream(2, 4, 0, 1, 3, 7, 5, 6);

            if (hasGameObjectPosition)
            {
                hasGOTransportTime2 = packet.Translator.ReadBit();
                goTransportGuid[1] = packet.Translator.ReadBit();
                goTransportGuid[4] = packet.Translator.ReadBit();
                goTransportGuid[5] = packet.Translator.ReadBit();
                goTransportGuid[0] = packet.Translator.ReadBit();
                goTransportGuid[6] = packet.Translator.ReadBit();
                goTransportGuid[7] = packet.Translator.ReadBit();
                goTransportGuid[3] = packet.Translator.ReadBit();
                hasGOTransportTime3 = packet.Translator.ReadBit();
                goTransportGuid[2] = packet.Translator.ReadBit();
            }

            if (hasAnimKits)
            {
                hasAnimKit3 = !packet.Translator.ReadBit();
                hasAnimKit1 = !packet.Translator.ReadBit();
                hasAnimKit2 = !packet.Translator.ReadBit();
            }

            // Reading data
            for (var i = 0u; i < unkLoopCounter; ++i)
                packet.Translator.ReadUInt32("Unk UInt32", index, (int)i);

            if (living)
            {
                moveInfo.WalkSpeed = packet.Translator.ReadSingle("Walk Speed", index) / 2.5f;
                if (moveInfo.HasSplineData)
                {
                    if (bit216)
                    {
                        for (var i = 0u; i < splineCount; ++i)
                        {
                            var wp = new Vector3
                            {
                                X = packet.Translator.ReadSingle(),
                                Z = packet.Translator.ReadSingle(),
                                Y = packet.Translator.ReadSingle()
                            };

                            packet.AddValue("Spline Waypoint", wp, index, i);
                        }

                        if (splineType == SplineType.FacingTarget)
                        {
                            packet.Translator.ParseBitStream(facingTarget, 0, 6, 5, 4, 1, 3, 7, 2);
                            packet.Translator.WriteGuid("Facing Target GUID", facingTarget, index);
                        }
                        else if (splineType == SplineType.FacingSpot)
                        {
                            var point = new Vector3
                            {
                                Z = packet.Translator.ReadSingle(),
                                Y = packet.Translator.ReadSingle(),
                                X = packet.Translator.ReadSingle()
                            };

                            packet.AddValue("Facing Spot", point, index);
                        }

                        packet.Translator.ReadUInt32("Unknown Spline Int32 2", index);
                        if (bit256)
                            packet.Translator.ReadUInt32("Unknown Spline Int32 3", index);

                        packet.Translator.ReadSingle("Unknown Spline Float 2", index);
                        packet.Translator.ReadSingle("Unknown Spline Float 1", index);
                        packet.Translator.ReadUInt32("Unknown Spline Int32 1", index);
                        if (splineType == SplineType.FacingAngle)
                            packet.Translator.ReadSingle("Facing Angle", index);

                        if (hasSplineDurationMult)
                            packet.Translator.ReadSingle("Spline Duration Modifier", index);
                    }

                    var endPoint = new Vector3
                    {
                        Z = packet.Translator.ReadSingle(),
                        Y = packet.Translator.ReadSingle()
                    };

                    packet.Translator.ReadUInt32("Spline Full Time", index);
                    endPoint.X = packet.Translator.ReadSingle();
                    packet.AddValue("Spline Endpoint", endPoint, index);
                }

                if (hasTransportData)
                {
                    if (hasTransportTime2)
                        packet.Translator.ReadInt32("Transport Time 2", index);

                    packet.Translator.ReadXORByte(transportGuid, 4);
                    packet.Translator.ReadXORByte(transportGuid, 6);
                    packet.Translator.ReadXORByte(transportGuid, 5);

                    if (hasTransportTime3)
                        packet.Translator.ReadInt32("Transport Time 3", index);

                    packet.Translator.ReadXORByte(transportGuid, 7);
                    packet.Translator.ReadXORByte(transportGuid, 3);

                    moveInfo.TransportOffset = new Vector4
                    {
                        X = packet.Translator.ReadSingle(),
                        Z = packet.Translator.ReadSingle(),
                        O = packet.Translator.ReadSingle()
                    };

                    packet.Translator.ReadXORByte(transportGuid, 2);
                    packet.Translator.ReadXORByte(transportGuid, 1);
                    packet.Translator.ReadXORByte(transportGuid, 0);

                    moveInfo.TransportOffset.Y = packet.Translator.ReadSingle();
                    moveInfo.TransportGuid = new WowGuid64(BitConverter.ToUInt64(transportGuid, 0));
                    packet.AddValue("Transport GUID", moveInfo.TransportGuid, index);
                    packet.AddValue("Transport Position", moveInfo.TransportOffset, index);
                    var seat = packet.Translator.ReadByte("Transport Seat", index);
                    packet.Translator.ReadInt32("Transport Time", index);

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

                if (unkFloat1)
                    packet.Translator.ReadSingle("float +28", index);

                packet.Translator.ReadSingle("FlyBack Speed", index);
                packet.Translator.ReadSingle("Turn Speed", index);
                packet.Translator.ReadXORByte(guid2, 5);

                moveInfo.RunSpeed = packet.Translator.ReadSingle("Run Speed", index) / 7.0f;
                if (unkFloat2)
                    packet.Translator.ReadSingle("float +36", index);

                packet.Translator.ReadXORByte(guid2, 0);

                packet.Translator.ReadSingle("Pitch Speed", index);
                if (hasFallData)
                {
                    packet.Translator.ReadInt32("Time Fallen", index);
                    packet.Translator.ReadSingle("Fall Start Velocity", index);
                    if (hasFallDirection)
                    {
                        packet.Translator.ReadSingle("Jump Sin", index);
                        packet.Translator.ReadSingle("Jump Velocity", index);
                        packet.Translator.ReadSingle("Jump Cos", index);
                    }
                }

                packet.Translator.ReadSingle("RunBack Speed", index);
                moveInfo.Position = new Vector3 {X = packet.Translator.ReadSingle()};
                packet.Translator.ReadSingle("SwimBack Speed", index);
                packet.Translator.ReadXORByte(guid2, 7);

                moveInfo.Position.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(guid2, 3);
                packet.Translator.ReadXORByte(guid2, 2);

                packet.Translator.ReadSingle("Fly Speed", index);
                packet.Translator.ReadSingle("Swim Speed", index);
                packet.Translator.ReadXORByte(guid2, 1);
                packet.Translator.ReadXORByte(guid2, 4);
                packet.Translator.ReadXORByte(guid2, 6);

                packet.Translator.WriteGuid("GUID 2", guid2, index);
                moveInfo.Position.Y = packet.Translator.ReadSingle();
                if (hasUnkUInt)
                    packet.Translator.ReadUInt32();

                if (hasOrientation)
                    moveInfo.Orientation = packet.Translator.ReadSingle("Orientation", index);

                packet.AddValue("Position", moveInfo.Position, index);
            }

            if (unkFloats)
            {
                int i;
                for (i = 0; i < 13; ++i)
                    packet.Translator.ReadSingle("Unk float 456", index, i);

                packet.Translator.ReadByte("Unk byte 456", index);

                for (; i < 16; ++i)
                    packet.Translator.ReadSingle("Unk float 456", index, i);
            }

            if (hasGameObjectPosition)
            {
                packet.Translator.ReadXORByte(goTransportGuid, 6);
                packet.Translator.ReadXORByte(goTransportGuid, 5);

                moveInfo.TransportOffset.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(goTransportGuid, 4);
                packet.Translator.ReadXORByte(goTransportGuid, 2);
                if (hasGOTransportTime3)
                    packet.Translator.ReadUInt32("GO Transport Time 3", index);

                moveInfo.TransportOffset.O = packet.Translator.ReadSingle();
                moveInfo.TransportOffset.Z = packet.Translator.ReadSingle();
                if (hasGOTransportTime2)
                    packet.Translator.ReadUInt32("GO Transport Time 2", index);

                packet.Translator.ReadByte("GO Transport Seat", index);
                packet.Translator.ReadXORByte(goTransportGuid, 7);
                packet.Translator.ReadXORByte(goTransportGuid, 1);
                packet.Translator.ReadXORByte(goTransportGuid, 0);
                packet.Translator.ReadXORByte(goTransportGuid, 3);

                moveInfo.TransportOffset.X = packet.Translator.ReadSingle();
                moveInfo.TransportGuid = packet.Translator.WriteGuid("GO Transport GUID", goTransportGuid, index);
                packet.Translator.ReadSingle("GO Transport Time", index);
                packet.AddValue("GO Transport Position: {1}", moveInfo.TransportOffset, index);
            }

            if (hasAttackingTarget)
            {
                packet.Translator.ParseBitStream(attackingTarget, 2, 4, 7, 3, 0, 1, 5, 6);
                packet.Translator.WriteGuid("Attacking Target GUID", attackingTarget, index);
            }

            if (hasGORotation)
                moveInfo.Rotation = packet.Translator.ReadPackedQuaternion("GO Rotation", index);

            if (unkInt)
                packet.Translator.ReadUInt32("uint32 +412", index);

            if (hasAnimKits)
            {
                if (hasAnimKit3)
                    packet.Translator.ReadUInt16("Anim Kit 3", index);
                if (hasAnimKit1)
                    packet.Translator.ReadUInt16("Anim Kit 1", index);
                if (hasAnimKit2)
                    packet.Translator.ReadUInt16("Anim Kit 2", index);
            }

            if (hasStationaryPosition)
            {
                moveInfo.Position = new Vector3
                {
                    Z = packet.Translator.ReadSingle(),
                    X = packet.Translator.ReadSingle(),
                    Y = packet.Translator.ReadSingle()
                };

                moveInfo.Orientation = packet.Translator.ReadSingle("O", index);
                packet.AddValue("Stationary Position", moveInfo.Position, index);
            }

            if (hasVehicleData)
            {
                packet.Translator.ReadSingle("Vehicle Orientation", index);
                moveInfo.VehicleId = packet.Translator.ReadUInt32("Vehicle Id", index);
            }

            packet.Translator.ResetBitReader();
            return moveInfo;
        }

        private static MovementInfo ReadMovementUpdateBlock432(Packet packet, WowGuid guid, object index)
        {
            var moveInfo = new MovementInfo();

            /*bool bit2 = */packet.Translator.ReadBit();
            /*bool bit3 = */packet.Translator.ReadBit();
            /*bool bit4 = */packet.Translator.ReadBit();
            var hasStationaryPosition = packet.Translator.ReadBit("Has Stationary Position", index);
            var hasAnimKits = packet.Translator.ReadBit("Has AnimKits", index);
            var unkLoopCounter = packet.Translator.ReadBits(24);
            /*bool bit1 = */packet.Translator.ReadBit();
            bool hasTransportExtra = packet.Translator.ReadBit("Has Transport Extra", index);
            bool hasGORotation = packet.Translator.ReadBit("Has GameObject Rotation", index);
            bool living = packet.Translator.ReadBit("Living", index);
            bool hasGameObjectPosition = packet.Translator.ReadBit("Has GameObject Position", index);
            bool hasVehicleData = packet.Translator.ReadBit("Has Vehicle Data", index);
            bool hasAttackingTarget = packet.Translator.ReadBit("Has Attacking Target", index);
            /*bool bit0 =*/packet.Translator.ReadBit();
            bool unkFloats = packet.Translator.ReadBit();

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
                unkFloat1 = !packet.Translator.ReadBit();
                hasOrientation = !packet.Translator.ReadBit();
                bool hasExtraMovementFlags = !packet.Translator.ReadBit();
                hasFallData = packet.Translator.ReadBit("Has Fall Data", index);
                guid2[0] = packet.Translator.ReadBit();
                guid2[5] = packet.Translator.ReadBit();
                guid2[4] = packet.Translator.ReadBit();
                bool hasMovementFlags = !packet.Translator.ReadBit();
                moveInfo.HasSplineData = packet.Translator.ReadBit("Has Spline Data", index);
                /*bool bit148 = */packet.Translator.ReadBit();

                if (hasExtraMovementFlags)
                    moveInfo.FlagsExtra = packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 12, index);

                hasUnkUInt = !packet.Translator.ReadBit();
                guid2[3] = packet.Translator.ReadBit();
                /*bool bit149 = */packet.Translator.ReadBit();

                if (hasMovementFlags)
                    moveInfo.Flags = packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30, index);

                guid2[1] = packet.Translator.ReadBit();
                unkFloat2 = !packet.Translator.ReadBit();
                hasTransportData = packet.Translator.ReadBit("Has Transport Data", index);
                guid2[2] = packet.Translator.ReadBit();

                if (hasTransportData)
                {
                    transportGuid[3] = packet.Translator.ReadBit();
                    transportGuid[5] = packet.Translator.ReadBit();
                    transportGuid[1] = packet.Translator.ReadBit();
                    transportGuid[7] = packet.Translator.ReadBit();
                    hasTransportTime2 = packet.Translator.ReadBit();
                    transportGuid[4] = packet.Translator.ReadBit();
                    transportGuid[0] = packet.Translator.ReadBit();
                    transportGuid[2] = packet.Translator.ReadBit();
                    transportGuid[6] = packet.Translator.ReadBit();
                    hasTransportTime3 = packet.Translator.ReadBit();
                }

                if (moveInfo.HasSplineData)
                {
                    bit216 = packet.Translator.ReadBit();
                    if (bit216)
                    {
                        uint bits57 = packet.Translator.ReadBits(2);
                        splineCount = packet.Translator.ReadBits(22);
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
                            facingTarget = packet.Translator.StartBitStream(4, 3, 2, 5, 7, 1, 0, 6);

                        packet.Translator.ReadBitsE<SplineFlag422>("Spline flags", 25, index);
                        /*splineMode =*/packet.Translator.ReadBits(2);
                        hasSplineDurationMult = packet.Translator.ReadBit("HasSplineDurationMult", index);
                        bit256 = packet.Translator.ReadBit();
                    }
                }

                if (hasFallData)
                    hasFallDirection = packet.Translator.ReadBit("Has Fall Direction", index);

                guid2[6] = packet.Translator.ReadBit();
                guid2[7] = packet.Translator.ReadBit();
            }

            if (hasGameObjectPosition)
            {
                goTransportGuid[5] = packet.Translator.ReadBit();
                goTransportGuid[4] = packet.Translator.ReadBit();
                hasGOTransportTime3 = packet.Translator.ReadBit();
                goTransportGuid[7] = packet.Translator.ReadBit();
                goTransportGuid[6] = packet.Translator.ReadBit();
                goTransportGuid[1] = packet.Translator.ReadBit();
                goTransportGuid[2] = packet.Translator.ReadBit();
                hasGOTransportTime2 = packet.Translator.ReadBit();
                goTransportGuid[0] = packet.Translator.ReadBit();
                goTransportGuid[3] = packet.Translator.ReadBit();
            }

            if (hasAnimKits)
            {
                hasAnimKit1 = !packet.Translator.ReadBit();
                hasAnimKit3 = !packet.Translator.ReadBit();
                hasAnimKit2 = !packet.Translator.ReadBit();
            }

            if (hasAttackingTarget)
                attackingTarget = packet.Translator.StartBitStream(4, 3, 2, 5, 0, 6, 1, 7);

            for (var i = 0; i < unkLoopCounter; ++i)
            {
                packet.Translator.ReadInt32();
            }

            if (hasGameObjectPosition)
            {
                if (hasGOTransportTime3)
                    packet.Translator.ReadInt32("GO Transport Time 3", index);

                packet.Translator.ReadXORByte(goTransportGuid, 7);

                moveInfo.TransportOffset.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadByte("GO Transport Seat", index);
                moveInfo.TransportOffset.X = packet.Translator.ReadSingle();
                moveInfo.TransportOffset.Y = packet.Translator.ReadSingle();

                packet.Translator.ReadXORByte(goTransportGuid, 4);
                packet.Translator.ReadXORByte(goTransportGuid, 5);
                packet.Translator.ReadXORByte(goTransportGuid, 6);

                moveInfo.TransportOffset.O = packet.Translator.ReadSingle();
                packet.Translator.ReadInt32("GO Transport Time", index);

                packet.Translator.ReadXORByte(goTransportGuid, 1);

                if (hasGOTransportTime2)
                    packet.Translator.ReadInt32("GO Transport Time 2", index);

                packet.Translator.ReadXORByte(goTransportGuid, 0);
                packet.Translator.ReadXORByte(goTransportGuid, 2);
                packet.Translator.ReadXORByte(goTransportGuid, 3);

                moveInfo.TransportGuid = packet.Translator.WriteGuid("GO Transport GUID", goTransportGuid, index);
                packet.AddValue("GO Transport Position", moveInfo.TransportOffset, index);
            }

            if (living)
            {
                if (moveInfo.HasSplineData)
                {
                    if (bit216)
                    {
                        packet.Translator.ReadSingle("Unknown Spline Float 2", index);
                        for (var i = 0u; i < splineCount; ++i)
                        {
                            var wp = new Vector3
                            {
                                Y = packet.Translator.ReadSingle(),
                                Z = packet.Translator.ReadSingle(),
                                X = packet.Translator.ReadSingle()
                            };

                            packet.AddValue("Spline Waypoint", wp, index, i);
                        }

                        if (splineType == SplineType.FacingTarget)
                        {
                            packet.Translator.ParseBitStream(facingTarget, 2, 1, 3, 7, 0, 5, 4, 6);
                            packet.Translator.WriteGuid("Facing Target GUID", facingTarget, index);
                        }
                        else if (splineType == SplineType.FacingSpot)
                        {
                            var point = new Vector3
                            {
                                Y = packet.Translator.ReadSingle(),
                                Z = packet.Translator.ReadSingle(),
                                X = packet.Translator.ReadSingle()
                            };

                            packet.AddValue("Facing Spot", point, index);
                        }

                        if (hasSplineDurationMult)
                            packet.Translator.ReadSingle("Spline Duration Modifier", index);

                        if (bit256)
                            packet.Translator.ReadUInt32("Unknown Spline Int32 1", index);

                        packet.Translator.ReadUInt32("Unknown Spline Int32 2", index);
                        packet.Translator.ReadSingle("Unknown Spline Float 1", index);
                        if (splineType == SplineType.FacingAngle)
                            packet.Translator.ReadSingle("Facing Angle", index);

                        packet.Translator.ReadUInt32("Unknown Spline Int32 3", index);
                    }

                    packet.Translator.ReadUInt32("Spline Full Time", index);
                    var endPoint = new Vector3
                    {
                        Z = packet.Translator.ReadSingle(),
                        Y = packet.Translator.ReadSingle(),
                        X = packet.Translator.ReadSingle()
                    };

                    packet.AddValue("Spline Endpoint", endPoint, index);
                }

                if (hasTransportData)
                {
                    packet.Translator.ReadXORByte(transportGuid, 6);
                    if (hasTransportTime2)
                        packet.Translator.ReadInt32("Transport Time 2", index);

                    var seat = packet.Translator.ReadByte("Transport Seat", index);
                    moveInfo.TransportOffset.O = packet.Translator.ReadSingle();
                    packet.Translator.ReadXORByte(transportGuid, 7);
                    moveInfo.TransportOffset.Y = packet.Translator.ReadSingle();
                    packet.Translator.ReadXORByte(transportGuid, 3);
                    if (hasTransportTime3)
                        packet.Translator.ReadInt32("Transport Time 3", index);

                    packet.Translator.ReadInt32("Transport Time", index);
                    packet.Translator.ReadXORByte(transportGuid, 0);
                    packet.Translator.ReadXORByte(transportGuid, 1);
                    moveInfo.TransportOffset.X = packet.Translator.ReadSingle();
                    packet.Translator.ReadXORByte(transportGuid, 4);
                    moveInfo.TransportOffset.Z = packet.Translator.ReadSingle();
                    packet.Translator.ReadXORByte(transportGuid, 5);
                    packet.Translator.ReadXORByte(transportGuid, 2);

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

                moveInfo.Position = new Vector3 {Z = packet.Translator.ReadSingle()};
                packet.Translator.ReadSingle("FlyBack Speed", index);
                moveInfo.Position.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(guid2, 4);
                packet.Translator.ReadXORByte(guid2, 0);
                moveInfo.Position.X = packet.Translator.ReadSingle();
                if (hasFallData)
                {
                    packet.Translator.ReadInt32("Time Fallen", index);
                    if (hasFallDirection)
                    {
                        packet.Translator.ReadSingle("Jump Sin", index);
                        packet.Translator.ReadSingle("Jump Velocity", index);
                        packet.Translator.ReadSingle("Jump Cos", index);
                    }
                    packet.Translator.ReadSingle("Fall Start Velocity", index);
                }

                if (hasOrientation)
                    moveInfo.Orientation = packet.Translator.ReadSingle("Orientation");

                packet.AddValue("Position", moveInfo.Position, moveInfo.Orientation, index);
                packet.Translator.ReadSingle("Swim Speed", index);
                moveInfo.RunSpeed = packet.Translator.ReadSingle("Run Speed", index) / 7.0f;
                packet.Translator.ReadSingle("Fly Speed", index);
                packet.Translator.ReadXORByte(guid2, 2);
                if (unkFloat2)
                    packet.Translator.ReadSingle("Unk float +36", index);

                if (unkFloat1)
                    packet.Translator.ReadSingle("Unk float +28", index);

                packet.Translator.ReadXORByte(guid2, 3);
                packet.Translator.ReadSingle("RunBack Speed", index);
                packet.Translator.ReadXORByte(guid2, 6);
                packet.Translator.ReadSingle("Pitch Speed", index);
                packet.Translator.ReadXORByte(guid2, 7);
                packet.Translator.ReadXORByte(guid2, 5);
                packet.Translator.ReadSingle("Turn Speed", index);
                packet.Translator.ReadSingle("SwimBack Speed", index);
                packet.Translator.ReadXORByte(guid2, 1);
                packet.Translator.WriteGuid("GUID 2", guid2, index);
                if (hasUnkUInt)
                    packet.Translator.ReadInt32();

                moveInfo.WalkSpeed = packet.Translator.ReadSingle("Walk Speed", index) / 2.5f;
            }

            if (hasAttackingTarget)
            {
                packet.Translator.ParseBitStream(attackingTarget, 6, 5, 3, 2, 0, 1, 7, 4);
                packet.Translator.WriteGuid("Attacking Target GUID", attackingTarget, index);
            }

            if (unkFloats)
            {
                int i;
                for (i = 0; i < 13; ++i)
                    packet.Translator.ReadSingle("Unk float 456", index, i);

                packet.Translator.ReadByte("Unk byte 456", index);

                for (; i < 16; ++i)
                    packet.Translator.ReadSingle("Unk float 456", index, i);
            }

            if (hasVehicleData)
            {
                packet.Translator.ReadSingle("Vehicle Orientation", index);
                moveInfo.VehicleId = packet.Translator.ReadUInt32("Vehicle Id", index);
            }

            if (hasGORotation)
                moveInfo.Rotation = packet.Translator.ReadPackedQuaternion("GO Rotation", index);

            if (hasStationaryPosition)
            {
                moveInfo.Position = new Vector3
                {
                    X = packet.Translator.ReadSingle(),
                    Z = packet.Translator.ReadSingle(),
                    Y = packet.Translator.ReadSingle()
                };

                moveInfo.Orientation = packet.Translator.ReadSingle("O", index);
                packet.AddValue("Stationary Position", moveInfo.Position, index);
            }

            if (hasAnimKits)
            {
                if (hasAnimKit3)
                    packet.Translator.ReadUInt16("Anim Kit 3", index);
                if (hasAnimKit1)
                    packet.Translator.ReadUInt16("Anim Kit 1", index);
                if (hasAnimKit2)
                    packet.Translator.ReadUInt16("Anim Kit 2", index);
            }

            if (hasTransportExtra)
                packet.Translator.ReadInt32("Transport Time", index);

            packet.Translator.ResetBitReader();
            return moveInfo;
        }

        private static MovementInfo ReadMovementUpdateBlock430(Packet packet, WowGuid guid, object index)
        {
            var moveInfo = new MovementInfo();
            bool hasAttackingTarget = packet.Translator.ReadBit("Has Attacking Target", index);
            /*bool bit2 = */packet.Translator.ReadBit();
            bool hasVehicleData = packet.Translator.ReadBit("Has Vehicle Data", index);
            /*bool bit1 = */packet.Translator.ReadBit();
            /*bool bit4 = */packet.Translator.ReadBit();
            /*bool bit3 = */packet.Translator.ReadBit();
            bool hasTransportExtra = packet.Translator.ReadBit("Has Transport Extra", index);
            bool hasGameObjectPosition = packet.Translator.ReadBit("Has GameObject Position", index);
            bool unkFloats = packet.Translator.ReadBit();
            bool hasAnimKits = packet.Translator.ReadBit("Has AnimKits", index);
            bool hasGORotation = packet.Translator.ReadBit("Has GameObject Rotation", index);
            bool living = packet.Translator.ReadBit("Living", index);
            bool hasStationaryPosition = packet.Translator.ReadBit("Has Stationary Position", index);
            uint unkLoopCounter = packet.Translator.ReadBits(24);
            /*bool bit0 = */packet.Translator.ReadBit();

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
                hasTransportData = packet.Translator.ReadBit("Has Transport Data", index);
                if (hasTransportData)
                {
                    transportGuid[2] = packet.Translator.ReadBit();
                    transportGuid[7] = packet.Translator.ReadBit();
                    transportGuid[5] = packet.Translator.ReadBit();
                    hasTransportTime3 = packet.Translator.ReadBit();
                    transportGuid[3] = packet.Translator.ReadBit();
                    transportGuid[0] = packet.Translator.ReadBit();
                    transportGuid[4] = packet.Translator.ReadBit();
                    transportGuid[1] = packet.Translator.ReadBit();
                    hasTransportTime2 = packet.Translator.ReadBit();
                    transportGuid[6] = packet.Translator.ReadBit();
                }

                moveInfo.HasSplineData = packet.Translator.ReadBit("Has Spline Data", index);
                guid2[7] = packet.Translator.ReadBit();
                guid2[6] = packet.Translator.ReadBit();
                guid2[5] = packet.Translator.ReadBit();
                guid2[2] = packet.Translator.ReadBit();
                guid2[4] = packet.Translator.ReadBit();
                bool hasMovementFlags = !packet.Translator.ReadBit();
                guid2[1] = packet.Translator.ReadBit();
                /*bool bit148 = */packet.Translator.ReadBit();
                hasUnkUInt = !packet.Translator.ReadBit();
                bool hasExtraMovementFlags = !packet.Translator.ReadBit();
                if (moveInfo.HasSplineData)
                {
                    bit216 = packet.Translator.ReadBit();
                    if (bit216)
                    {
                        bit256 = packet.Translator.ReadBit();
                        /*splineFlags = */packet.Translator.ReadBitsE<SplineFlag422>("Spline flags", 25, index);
                        /*splineMode = */packet.Translator.ReadBits(2);
                        hasSplineDurationMult = packet.Translator.ReadBit();
                        splineCount = packet.Translator.ReadBits(22);
                        uint bits57 = packet.Translator.ReadBits(2);
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
                            facingTarget = packet.Translator.StartBitStream(7, 3, 4, 2, 1, 6, 0, 5);
                    }
                }

                guid2[3] = packet.Translator.ReadBit();
                if (hasMovementFlags)
                    moveInfo.Flags = packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30, index);

                unkFloat1 = !packet.Translator.ReadBit();
                hasFallData = packet.Translator.ReadBit("Has Fall Data", index);
                if (hasExtraMovementFlags)
                    moveInfo.FlagsExtra = packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 12, index);

                guid2[0] = packet.Translator.ReadBit();
                hasOrientation = !packet.Translator.ReadBit();
                if (hasFallData)
                    hasFallDirection = packet.Translator.ReadBit("Has Fall Direction", index);

                unkFloat2 = !packet.Translator.ReadBit();
            }

            if (hasGameObjectPosition)
            {
                goTransportGuid[1] = packet.Translator.ReadBit();
                hasGOTransportTime3 = packet.Translator.ReadBit();
                goTransportGuid[3] = packet.Translator.ReadBit();
                goTransportGuid[2] = packet.Translator.ReadBit();
                goTransportGuid[6] = packet.Translator.ReadBit();
                goTransportGuid[5] = packet.Translator.ReadBit();
                goTransportGuid[0] = packet.Translator.ReadBit();
                goTransportGuid[4] = packet.Translator.ReadBit();
                hasGOTransportTime2 = packet.Translator.ReadBit();
                goTransportGuid[7] = packet.Translator.ReadBit();
            }

            if (hasAnimKits)
            {
                hasAnimKit3 = !packet.Translator.ReadBit();
                hasAnimKit1 = !packet.Translator.ReadBit();
                hasAnimKit2 = !packet.Translator.ReadBit();
            }

            if (hasAttackingTarget)
                attackingTarget = packet.Translator.StartBitStream(3, 4, 6, 0, 1, 7, 5, 2);

            // Reading data
            for (var i = 0u; i < unkLoopCounter; ++i)
                packet.Translator.ReadUInt32("Unk UInt32", index, (int)i);

            if (hasStationaryPosition)
            {
                moveInfo.Position = new Vector3 {Z = packet.Translator.ReadSingle()};
                moveInfo.Orientation = packet.Translator.ReadSingle("O", index);
                moveInfo.Position.X = packet.Translator.ReadSingle();
                moveInfo.Position.Y = packet.Translator.ReadSingle();
                packet.AddValue("Stationary Position", moveInfo.Position, moveInfo.Orientation, index);
            }

            if (hasVehicleData)
            {
                moveInfo.VehicleId = packet.Translator.ReadUInt32("Vehicle Id", index);
                packet.Translator.ReadSingle("Vehicle Orientation", index);
            }

            if (hasGameObjectPosition)
            {
                packet.Translator.ReadXORByte(goTransportGuid, 1);
                packet.Translator.ReadXORByte(goTransportGuid, 4);
                moveInfo.TransportOffset.Z = packet.Translator.ReadSingle();
                if (hasGOTransportTime3)
                    packet.Translator.ReadInt32("GO Transport Time 3", index);

                packet.Translator.ReadInt32("GO Transport Time", index);
                packet.Translator.ReadXORByte(goTransportGuid, 5);
                packet.Translator.ReadXORByte(goTransportGuid, 6);
                moveInfo.TransportOffset.X = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(goTransportGuid, 2);
                if (hasGOTransportTime2)
                    packet.Translator.ReadInt32("GO Transport Time 2", index);

                packet.Translator.ReadByte("GO Transport Seat", index);
                packet.Translator.ReadXORByte(goTransportGuid, 3);
                moveInfo.TransportOffset.Y = packet.Translator.ReadSingle();
                moveInfo.TransportOffset.O = packet.Translator.ReadSingle();
                packet.Translator.ReadXORByte(goTransportGuid, 7);
                packet.Translator.ReadXORByte(goTransportGuid, 0);

                moveInfo.TransportGuid = packet.Translator.WriteGuid("GO Transport GUID", goTransportGuid, index);
                packet.AddValue("GO Transport Position", moveInfo.TransportOffset, index);
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
                                Y = packet.Translator.ReadSingle(),
                                X = packet.Translator.ReadSingle(),
                                Z = packet.Translator.ReadSingle()
                            };

                            packet.AddValue("Spline Waypoint", wp, index, i);
                        }

                        if (hasSplineDurationMult)
                            packet.Translator.ReadSingle("Spline Duration Modifier", index);

                        packet.Translator.ReadSingle("Unknown Spline Float 2", index);
                        if (splineType == SplineType.FacingTarget)
                        {
                            packet.Translator.ParseBitStream(facingTarget, 3, 4, 5, 7, 2, 0, 6, 1);
                            packet.Translator.WriteGuid("Facing Target GUID", facingTarget, index);
                        }

                        if (bit256)
                            packet.Translator.ReadUInt32("Unknown Spline Int32 3", index);

                        packet.Translator.ReadSingle("Unknown Spline Float 1", index);
                        packet.Translator.ReadUInt32("Unknown Spline Int32 1", index);
                        if (splineType == SplineType.FacingSpot)
                        {
                            var point = new Vector3
                            {
                                Y = packet.Translator.ReadSingle(),
                                Z = packet.Translator.ReadSingle(),
                                X = packet.Translator.ReadSingle()
                            };

                            packet.AddValue("Facing Spot", point, index);
                        }

                        packet.Translator.ReadUInt32("Unknown Spline Int32 2", index);
                        if (splineType == SplineType.FacingAngle)
                            packet.Translator.ReadSingle("Facing Angle", index);
                    }

                    var endPoint = new Vector3
                    {
                        Z = packet.Translator.ReadSingle(),
                        Y = packet.Translator.ReadSingle()
                    };

                    packet.Translator.ReadUInt32("Spline Full Time", index);
                    endPoint.X = packet.Translator.ReadSingle();
                    packet.AddValue("Spline Endpoint", endPoint, index);
                }

                packet.Translator.ReadSingle("Pitch Speed", index);
                if (hasTransportData)
                {
                    packet.Translator.ReadXORByte(transportGuid, 4);
                    moveInfo.TransportOffset.Z = packet.Translator.ReadSingle();
                    packet.Translator.ReadXORByte(transportGuid, 7);
                    packet.Translator.ReadXORByte(transportGuid, 5);
                    packet.Translator.ReadXORByte(transportGuid, 1);
                    moveInfo.TransportOffset.X = packet.Translator.ReadSingle();
                    packet.Translator.ReadXORByte(transportGuid, 3);
                    packet.Translator.ReadXORByte(transportGuid, 6);
                    if (hasTransportTime3)
                        packet.Translator.ReadInt32("Transport Time 3", index);

                    moveInfo.TransportOffset.Y = packet.Translator.ReadSingle();
                    var seat = packet.Translator.ReadByte("Transport Seat", index);
                    moveInfo.TransportOffset.O = packet.Translator.ReadSingle();
                    if (hasTransportTime2)
                        packet.Translator.ReadInt32("Transport Time 2", index);

                    packet.Translator.ReadXORByte(transportGuid, 2);
                    packet.Translator.ReadInt32("Transport Time", index);
                    packet.Translator.ReadXORByte(transportGuid, 0);

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

                packet.Translator.ReadSingle("FlyBack Speed", index);
                moveInfo.Position = new Vector3 {X = packet.Translator.ReadSingle()};
                if (unkFloat1)
                    packet.Translator.ReadSingle("Unk float +28", index);

                if (hasFallData)
                {
                    packet.Translator.ReadInt32("Time Fallen", index);
                    if (hasFallDirection)
                    {
                        packet.Translator.ReadSingle("Jump Sin", index);
                        packet.Translator.ReadSingle("Jump Velocity", index);
                        packet.Translator.ReadSingle("Jump Cos", index);
                    }
                    packet.Translator.ReadSingle("Fall Start Velocity", index);
                }

                packet.Translator.ReadXORByte(guid2, 7);
                packet.Translator.ReadSingle("SwimBack Speed", index);
                packet.Translator.ReadXORByte(guid2, 0);
                packet.Translator.ReadXORByte(guid2, 5);
                if (hasUnkUInt)
                    packet.Translator.ReadUInt32();

                moveInfo.Position.Z = packet.Translator.ReadSingle();
                packet.Translator.ReadSingle("Fly Speed", index);
                packet.Translator.ReadXORByte(guid2, 1);
                packet.Translator.ReadSingle("RunBack Speed", index);
                packet.Translator.ReadSingle("Turn Speed", index);
                packet.Translator.ReadSingle("Swim Speed", index);
                moveInfo.WalkSpeed = packet.Translator.ReadSingle("Walk Speed", index) / 2.5f;
                packet.Translator.ReadXORByte(guid2, 3);
                packet.Translator.ReadXORByte(guid2, 4);
                packet.Translator.ReadXORByte(guid2, 2);
                packet.Translator.ReadXORByte(guid2, 6);
                packet.Translator.WriteGuid("GUID 2", guid2, index);
                if (unkFloat2)
                    packet.Translator.ReadSingle("Unk float +36", index);

                moveInfo.Position.Y = packet.Translator.ReadSingle();
                if (hasOrientation)
                    moveInfo.Orientation = packet.Translator.ReadSingle("Orientation", index);

                moveInfo.RunSpeed = packet.Translator.ReadSingle("Run Speed", index) / 7.0f;
                packet.AddValue("Position", moveInfo.Position, index);
            }

            if (unkFloats)
            {
                for (int i = 0; i < 16; ++i)
                    packet.Translator.ReadSingle("Unk float 456", index, i);

                packet.Translator.ReadByte("Unk byte 456", index);
            }

            if (hasTransportExtra)
                packet.Translator.ReadInt32("Transport Time", index);

            if (hasAnimKits)
            {
                if (hasAnimKit2)
                    packet.Translator.ReadUInt16("Anim Kit 2", index);
                if (hasAnimKit3)
                    packet.Translator.ReadUInt16("Anim Kit 3", index);
                if (hasAnimKit1)
                    packet.Translator.ReadUInt16("Anim Kit 1", index);
            }

            if (hasGORotation)
                moveInfo.Rotation = packet.Translator.ReadPackedQuaternion("GO Rotation", index);

            if (hasAttackingTarget)
            {
                packet.Translator.ParseBitStream(attackingTarget, 3, 5, 0, 7, 2, 4, 6, 1);
                packet.Translator.WriteGuid("Attacking Target GUID", attackingTarget, index);
            }

            packet.Translator.ResetBitReader();
            return moveInfo;
        }

        private static MovementInfo ReadMovementUpdateBlock(Packet packet, WowGuid guid, object index)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                return ReadMovementUpdateBlock510(packet, guid, index);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_0_4_16016))
                return ReadMovementUpdateBlock504(packet, guid, index);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_3_15354))
                return ReadMovementUpdateBlock433(packet, guid, index);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_2_15211))
                return ReadMovementUpdateBlock432(packet, guid, index);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                return ReadMovementUpdateBlock430(packet, guid, index);

            var moveInfo = new MovementInfo();

            UpdateFlag flags;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                flags = packet.Translator.ReadUInt16E<UpdateFlag>("Update Flags", index);
            else
                flags = packet.Translator.ReadByteE<UpdateFlag>("Update Flags", index);

            if (flags.HasAnyFlag(UpdateFlag.Living))
            {
                moveInfo = MovementHandler.ReadMovementInfo(packet, guid, index);
                var moveFlags = moveInfo.Flags;

                for (var i = 0; i < 9; ++i)
                {
                    var speedType = (SpeedType)i;
                    var speed = packet.Translator.ReadSingle(speedType + " Speed", index);

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
                        var splineFlags422 = packet.Translator.ReadInt32E<SplineFlag422>("Spline Flags", index);
                        if (splineFlags422.HasAnyFlag(SplineFlag422.FinalOrientation))
                        {
                            packet.Translator.ReadSingle("Final Spline Orientation", index);
                        }
                        else
                        {
                            if (splineFlags422.HasAnyFlag(SplineFlag422.FinalTarget))
                                packet.Translator.ReadGuid("Final Spline Target GUID", index);
                            else if (splineFlags422.HasAnyFlag(SplineFlag422.FinalPoint))
                                packet.Translator.ReadVector3("Final Spline Coords", index);
                        }
                    }
                    else
                    {
                        var splineFlags = packet.Translator.ReadInt32E<SplineFlag>("Spline Flags", index);
                        if (splineFlags.HasAnyFlag(SplineFlag.FinalTarget))
                            packet.Translator.ReadGuid("Final Spline Target GUID", index);
                        else if (splineFlags.HasAnyFlag(SplineFlag.FinalOrientation))
                            packet.Translator.ReadSingle("Final Spline Orientation", index);
                        else if (splineFlags.HasAnyFlag(SplineFlag.FinalPoint))
                            packet.Translator.ReadVector3("Final Spline Coords", index);
                    }

                    packet.Translator.ReadInt32("Spline Time", index);
                    packet.Translator.ReadInt32("Spline Full Time", index);
                    packet.Translator.ReadInt32("Spline ID", index);

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                    {
                        packet.Translator.ReadSingle("Spline Duration Multiplier", index);
                        packet.Translator.ReadSingle("Spline Duration Multiplier Next", index);
                        packet.Translator.ReadSingle("Spline Vertical Acceleration", index);
                        packet.Translator.ReadInt32("Spline Start Time", index);
                    }

                    var splineCount = packet.Translator.ReadInt32();
                    for (var i = 0; i < splineCount; i++)
                        packet.Translator.ReadVector3("Spline Waypoint", index, i);

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                        packet.Translator.ReadByteE<SplineMode>("Spline Mode", index);

                    packet.Translator.ReadVector3("Spline Endpoint", index);
                }
            }
            else // !UpdateFlag.Living
            {
                if (flags.HasAnyFlag(UpdateFlag.GOPosition))
                {
                    moveInfo.TransportGuid = packet.Translator.ReadPackedGuid("GO Transport GUID", index);

                    moveInfo.Position = packet.Translator.ReadVector3("GO Position", index);
                    moveInfo.TransportOffset.X = packet.Translator.ReadSingle();
                    moveInfo.TransportOffset.Y = packet.Translator.ReadSingle();
                    moveInfo.TransportOffset.Z = packet.Translator.ReadSingle();

                    moveInfo.Orientation = packet.Translator.ReadSingle("GO Orientation", index);
                    moveInfo.TransportOffset.O = moveInfo.Orientation;

                    packet.AddValue("GO Transport Position", moveInfo.TransportOffset, index);

                    packet.Translator.ReadSingle("Corpse Orientation", index);
                }
                else if (flags.HasAnyFlag(UpdateFlag.StationaryObject))
                {
                    moveInfo.Position = packet.Translator.ReadVector3("Stationary Position", index);
                    moveInfo.Orientation = packet.Translator.ReadSingle("O", index);
                }
            }

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_2_2_14545))
            {
                if (flags.HasAnyFlag(UpdateFlag.Unknown1))
                    packet.Translator.ReadUInt32("Unk Int32", index);

                if (flags.HasAnyFlag(UpdateFlag.LowGuid))
                    packet.Translator.ReadUInt32("Low GUID", index);
            }

            if (flags.HasAnyFlag(UpdateFlag.AttackingTarget))
                packet.Translator.ReadPackedGuid("Target GUID", index);

            if (flags.HasAnyFlag(UpdateFlag.Transport))
                packet.Translator.ReadUInt32("Transport unk timer", index);

            if (flags.HasAnyFlag(UpdateFlag.Vehicle))
            {
                moveInfo.VehicleId = packet.Translator.ReadUInt32("[" + index + "] Vehicle ID");
                packet.Translator.ReadSingle("Vehicle Orientation", index);
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
            {
                if (flags.HasAnyFlag(UpdateFlag.AnimKits))
                {
                    packet.Translator.ReadInt16("Unk Int16", index);
                    packet.Translator.ReadInt16("Unk Int16", index);
                    packet.Translator.ReadInt16("Unk Int16", index);
                }
            }

            if (flags.HasAnyFlag(UpdateFlag.GORotation))
                moveInfo.Rotation = packet.Translator.ReadPackedQuaternion("GO Rotation", index);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
            {
                if (flags.HasAnyFlag(UpdateFlag.TransportUnkArray))
                {
                    var count = packet.Translator.ReadByte("Count", index);
                    for (var i = 0; i < count; i++)
                        packet.Translator.ReadInt32("Unk Int32", index, count);
                }
            }

            return moveInfo;
        }

        [Parser(Opcode.SMSG_COMPRESSED_UPDATE_OBJECT)]
        public static void HandleCompressedUpdateObject(Packet packet)
        {
            using (var packet2 = packet.Inflate(packet.Translator.ReadInt32()))
            {
                HandleUpdateObject(packet2);
            }
        }

        [Parser(Opcode.SMSG_DESTROY_OBJECT)]
        public static void HandleDestroyObject(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                packet.Translator.ReadBool("Despawn Animation");
        }

        [Parser(Opcode.CMSG_OBJECT_UPDATE_FAILED, ClientVersionBuild.Zero, ClientVersionBuild.V5_1_0_16309)] // 4.3.4
        public static void HandleObjectUpdateFailed(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(6, 7, 4, 0, 1, 5, 3, 2);
            packet.Translator.ParseBitStream(guid, 6, 7, 2, 3, 1, 4, 0, 5);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_OBJECT_UPDATE_FAILED, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleObjectUpdateFailed510(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(5, 3, 0, 6, 1, 4, 2, 7);
            packet.Translator.ParseBitStream(guid, 2, 3, 7, 4, 5, 1, 0, 6);
            packet.Translator.WriteGuid("Guid", guid);
        }
    }
}

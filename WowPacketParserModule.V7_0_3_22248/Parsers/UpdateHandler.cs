using System.Collections;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class UpdateHandler
    {
        [HasSniffData] // in ReadCreateObjectBlock
        [Parser(Opcode.SMSG_UPDATE_OBJECT)]
        public static void HandleUpdateObject(Packet packet)
        {
            var count = packet.ReadUInt32("NumObjUpdates");
            uint map = packet.ReadUInt16<MapId>("MapID");
            packet.ResetBitReader();
            var bit552 = packet.ReadBit("HasDestroyObjects");
            if (bit552)
            {
                packet.ReadInt16("Int0");
                var int8 = packet.ReadUInt32("DestroyObjectsCount");
                for (var i = 0; i < int8; i++)
                    packet.ReadPackedGuid128("Object GUID", i);
            }
            packet.ReadUInt32("Data size");

            for (var i = 0; i < count; i++)
            {
                var type = packet.ReadByte();
                var typeString = ((UpdateTypeCataclysm)type).ToString();

                packet.AddValue("UpdateType", typeString, i);
                switch (typeString)
                {
                    case "Values":
                    {
                        var guid = packet.ReadPackedGuid128("Object Guid", i);

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
                    case "CreateObject1":
                    case "CreateObject2":
                    {
                        var guid = packet.ReadPackedGuid128("Object Guid", i);
                        ReadCreateObjectBlock(packet, guid, map, i);
                        break;
                    }
                }
            }
        }

        private static void ReadCreateObjectBlock(Packet packet, WowGuid guid, uint map, object index)
        {
            var objType = packet.ReadByteE<ObjectType>("Object Type", index);
            var moves = ReadMovementUpdateBlock(packet, guid, index);
            var updates = ReadValuesUpdateBlock(packet, objType, index, true);

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

        private static MovementInfo ReadMovementUpdateBlock(Packet packet, WowGuid guid, object index)
        {
            var moveInfo = new MovementInfo();

            packet.ResetBitReader();

            packet.ReadBit("NoBirthAnim", index);
            packet.ReadBit("EnablePortals", index);
            packet.ReadBit("PlayHoverAnim", index);

            var hasMovementUpdate = packet.ReadBit("HasMovementUpdate", index);
            var hasMovementTransport = packet.ReadBit("HasMovementTransport", index);
            var hasStationaryPosition = packet.ReadBit("Stationary", index);
            var hasCombatVictim = packet.ReadBit("HasCombatVictim", index);
            var hasServerTime = packet.ReadBit("HasServerTime", index);
            var hasVehicleCreate = packet.ReadBit("HasVehicleCreate", index);
            var hasAnimKitCreate = packet.ReadBit("HasAnimKitCreate", index);
            var hasRotation = packet.ReadBit("HasRotation", index);
            var hasAreaTrigger = packet.ReadBit("HasAreaTrigger", index);
            var hasGameObject = packet.ReadBit("HasGameObject", index);
            var hasSmoothPhasing = packet.ReadBit("HasSmoothPhasing", index);

            packet.ReadBit("ThisIsYou", index);

            var sceneObjCreate = packet.ReadBit("SceneObjCreate", index);
            var playerCreateData = packet.ReadBit("HasPlayerCreateData", index);

            if (hasMovementUpdate)
            {
                packet.ResetBitReader();
                packet.ReadPackedGuid128("MoverGUID", index);

                packet.ReadUInt32("MoveTime", index);
                moveInfo.Position = packet.ReadVector3("Position", index);
                moveInfo.Orientation = packet.ReadSingle("Orientation", index);

                packet.ReadSingle("Pitch", index);
                packet.ReadSingle("StepUpStartElevation", index);

                var removeForcesIDsCount = packet.ReadInt32();
                packet.ReadInt32("MoveIndex", index);

                for (var i = 0; i < removeForcesIDsCount; i++)
                    packet.ReadPackedGuid128("RemoveForcesIDs", index, i);

                moveInfo.Flags = packet.ReadBitsE<MovementFlag>("Movement Flags", 30, index);
                moveInfo.FlagsExtra = packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 18, index);

                var hasTransport = packet.ReadBit("Has Transport Data", index);
                var hasFall = packet.ReadBit("Has Fall Data", index);
                packet.ReadBit("HasSpline", index);
                packet.ReadBit("HeightChangeFailed", index);
                packet.ReadBit("RemoteTimeValid", index);

                if (hasTransport)
                {
                    packet.ResetBitReader();
                    moveInfo.TransportGuid = packet.ReadPackedGuid128("Transport Guid", index);
                    moveInfo.TransportOffset = packet.ReadVector4("Transport Position", index);
                    packet.ReadSByte("Transport Seat", index);
                    packet.ReadInt32("Transport Time", index);

                    var hasPrevMoveTime = packet.ReadBit("HasPrevMoveTime", index);
                    var hasVehicleRecID = packet.ReadBit("HasVehicleRecID", index);

                    if (hasPrevMoveTime)
                        packet.ReadUInt32("PrevMoveTime", index);

                    if (hasVehicleRecID)
                        packet.ReadUInt32("VehicleRecID", index);
                }

                if (hasFall)
                {
                    packet.ResetBitReader();
                    packet.ReadUInt32("Fall Time", index);
                    packet.ReadSingle("JumpVelocity", index);

                    var hasFallDirection = packet.ReadBit("Has Fall Direction", index);
                    if (hasFallDirection)
                    {
                        packet.ReadVector2("Fall", index);
                        packet.ReadSingle("Horizontal Speed", index);
                    }
                }

                moveInfo.WalkSpeed = packet.ReadSingle("WalkSpeed", index) / 2.5f;
                moveInfo.RunSpeed = packet.ReadSingle("RunSpeed", index) / 7.0f;
                packet.ReadSingle("RunBackSpeed", index);
                packet.ReadSingle("SwimSpeed", index);
                packet.ReadSingle("SwimBackSpeed", index);
                packet.ReadSingle("FlightSpeed", index);
                packet.ReadSingle("FlightBackSpeed", index);
                packet.ReadSingle("TurnRate", index);
                packet.ReadSingle("PitchRate", index);

                var movementForceCount = packet.ReadInt32("MovementForceCount", index);

                packet.ResetBitReader();

                moveInfo.HasSplineData = packet.ReadBit("HasMovementSpline", index);

                for (var i = 0; i < movementForceCount; ++i)
                {
                    packet.ResetBitReader();
                    packet.ReadPackedGuid128("Id", index);
                    packet.ReadVector3("Origin", index);
                    packet.ReadVector3("Direction", index);
                    packet.ReadInt32("TransportID", index);
                    packet.ReadSingle("Magnitude", index);
                    packet.ReadBits("Type", 2, index);
                }

                if (moveInfo.HasSplineData)
                {
                    packet.ResetBitReader();
                    packet.ReadInt32("ID", index);
                    packet.ReadVector3("Destination", index);

                    var hasMovementSplineMove = packet.ReadBit("MovementSplineMove", index);
                    if (hasMovementSplineMove)
                    {
                        packet.ResetBitReader();

                        packet.ReadUInt32E<SplineFlag434>("SplineFlags", index);
                        packet.ReadUInt32("Elapsed", index);
                        packet.ReadUInt32("Duration", index);
                        packet.ReadSingle("DurationModifier", index);
                        packet.ReadSingle("NextDurationModifier", index);

                        var face = packet.ReadBits("Face", 2, index);

                        var hasJumpGravity = packet.ReadBit("HasJumpGravity", index);
                        var hasSpecialTime = packet.ReadBit("HasSpecialTime", index);

                        var pointsCount = packet.ReadBits("PointsCount", 16, index);

                        packet.ReadBitsE<SplineMode>("Mode", 2, index);

                        var hasSplineFilterKey = packet.ReadBit("HasSplineFilterKey", index);
                        var hasSpellEffectExtraData = packet.ReadBit("HasSpellEffectExtraData", index);

                        if (hasSplineFilterKey)
                        {
                            packet.ResetBitReader();
                            var filterKeysCount = packet.ReadUInt32("FilterKeysCount", index);
                            for (var i = 0; i < filterKeysCount; ++i)
                            {
                                packet.ReadSingle("In", index, i);
                                packet.ReadSingle("Out", index, i);
                            }

                            packet.ReadBits("FilterFlags", 2, index);
                        }

                        if (face == 3)
                            packet.ReadSingle("FaceDirection", index);

                        if (face == 2)
                            packet.ReadPackedGuid128("FaceGUID", index);

                        if (face == 1)
                            packet.ReadVector3("FaceSpot", index);

                        if (hasJumpGravity)
                            packet.ReadSingle("JumpGravity", index);

                        if (hasSpecialTime)
                            packet.ReadInt32("SpecialTime", index);

                        for (var i = 0; i < pointsCount; ++i)
                            packet.ReadVector3("Points", index, i);

                        if (hasSpellEffectExtraData)
                        {
                            packet.ReadPackedGuid128("SpellEffectExtraGUID", index);
                            packet.ReadUInt32("SpellEffectExtra int 1", index);
                            packet.ReadUInt32("SpellEffectExtra int 2", index);
                            packet.ReadUInt32("SpellEffectExtra int 3", index);
                        }
                    }
                }
            }

            var pauseTimesCount = packet.ReadUInt32("PauseTimesCount", index);

            if (hasStationaryPosition)
            {
                moveInfo.Position = packet.ReadVector3();
                moveInfo.Orientation = packet.ReadSingle();

                packet.AddValue("Stationary Position", moveInfo.Position, index);
                packet.AddValue("Stationary Orientation", moveInfo.Orientation, index);
            }

            if (hasCombatVictim)
                packet.ReadPackedGuid128("CombatVictim Guid", index);

            if (hasServerTime)
                packet.ReadUInt32("ServerTime", index);

            if (hasVehicleCreate)
            {
                moveInfo.VehicleId = packet.ReadUInt32("RecID", index);
                packet.ReadSingle("InitialRawFacing", index);
            }

            if (hasAnimKitCreate)
            {
                packet.ReadUInt16("AiID", index);
                packet.ReadUInt16("MovementID", index);
                packet.ReadUInt16("MeleeID", index);
            }

            if (hasRotation)
                moveInfo.Rotation = packet.ReadPackedQuaternion("GameObject Rotation", index);

            for (var i = 0; i < pauseTimesCount; ++i)
                packet.ReadInt32("PauseTimes", index, i);

            if (hasMovementTransport)
            {
                packet.ResetBitReader();
                moveInfo.TransportGuid = packet.ReadPackedGuid128("TransportGUID", index);
                moveInfo.TransportOffset = packet.ReadVector4("TransportPosition", index);
                var seat = packet.ReadByte("VehicleSeatIndex", index);
                packet.ReadUInt32("MoveTime", index);

                var hasPrevMoveTime = packet.ReadBit("HasPrevMoveTime", index);
                var hasVehicleRecID = packet.ReadBit("HasVehicleRecID", index);

                if (hasPrevMoveTime)
                    packet.ReadUInt32("PrevMoveTime", index);

                if (hasVehicleRecID)
                    packet.ReadInt32("VehicleRecID", index);

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

            if (hasAreaTrigger)
            {
                packet.ResetBitReader();

                // CliAreaTrigger
                packet.ReadInt32("ElapsedMs", index);

                packet.ReadVector3("RollPitchYaw1", index);

                packet.ReadBit("HasAbsoluteOrientation", index);
                packet.ReadBit("HasDynamicShape", index);
                packet.ReadBit("HasAttached", index);
                packet.ReadBit("HasFaceMovementDir", index);
                packet.ReadBit("HasFollowsTerrain", index);
                packet.ReadBit("Unk bit WoD62x", index);

                var hasTargetRollPitchYaw = packet.ReadBit("HasTargetRollPitchYaw", index);
                var hasScaleCurveID = packet.ReadBit("HasScaleCurveID", index);
                var hasMorphCurveID = packet.ReadBit("HasMorphCurveID", index);
                var hasFacingCurveID = packet.ReadBit("HasFacingCurveID", index);
                var hasMoveCurveID = packet.ReadBit("HasMoveCurveID", index);
                var unkbit4C = packet.ReadBit();
                var unkbit50 = packet.ReadBit();
                var unkbit58 = packet.ReadBit();
                var hasAreaTriggerSphere = packet.ReadBit("HasAreaTriggerSphere", index);
                var hasAreaTriggerBox = packet.ReadBit("HasAreaTriggerBox", index);
                var hasAreaTriggerPolygon = packet.ReadBit("HasAreaTriggerPolygon", index);
                var hasAreaTriggerCylinder = packet.ReadBit("HasAreaTriggerCylinder", index);
                var hasAreaTriggerSpline = packet.ReadBit("HasAreaTriggerSpline", index);
                var hasAreaTriggerUnkType = packet.ReadBit("HasAreaTriggerUnkType", index);

                if (unkbit50)
                    packet.ReadBit();

                if (hasAreaTriggerSpline)
                    AreaTriggerHandler.ReadAreaTriggerSpline(packet, index);

                if (hasTargetRollPitchYaw)
                    packet.ReadVector3("TargetRollPitchYaw", index);

                if (hasScaleCurveID)
                    packet.ReadInt32("ScaleCurveID", index);

                if (hasMorphCurveID)
                    packet.ReadInt32("MorphCurveID", index);

                if (hasFacingCurveID)
                    packet.ReadInt32("FacingCurveID", index);

                if (hasMoveCurveID)
                    packet.ReadInt32("MoveCurveID", index);

                if (unkbit4C)
                    packet.ReadInt32();

                if (unkbit58)
                    packet.ReadUInt32();

                if (hasAreaTriggerSphere)
                {
                    packet.ReadSingle("Radius", index);
                    packet.ReadSingle("RadiusTarget", index);
                }

                if (hasAreaTriggerBox)
                {
                    packet.ReadVector3("Extents", index);
                    packet.ReadVector3("ExtentsTarget", index);
                }

                if (hasAreaTriggerPolygon)
                {
                    var verticesCount = packet.ReadInt32("VerticesCount", index);
                    var verticesTargetCount = packet.ReadInt32("VerticesTargetCount", index);
                    packet.ReadSingle("Height", index);
                    packet.ReadSingle("HeightTarget", index);

                    for (var i = 0; i < verticesCount; ++i)
                        packet.ReadVector2("Vertices", index, i);

                    for (var i = 0; i < verticesTargetCount; ++i)
                        packet.ReadVector2("VerticesTarget", index, i);
                }

                if (hasAreaTriggerCylinder)
                {
                    packet.ReadSingle("Radius", index);
                    packet.ReadSingle("RadiusTarget", index);
                    packet.ReadSingle("Height", index);
                    packet.ReadSingle("HeightTarget", index);
                    packet.ReadSingle("LocationZOffset", index);
                    packet.ReadSingle("LocationZOffsetTarget", index);
                }

                if (hasAreaTriggerUnkType)
                {
                    packet.ResetBitReader();
                    var unk1 = packet.ReadBit("AreaTriggerUnk1");
                    var hasCenter = packet.ReadBit("HasCenter", index);
                    packet.ReadBit("Unk bit 703 1", index);
                    packet.ReadBit("Unk bit 703 2", index);

                    packet.ReadUInt32();
                    packet.ReadInt32();
                    packet.ReadUInt32();
                    packet.ReadSingle("Radius", index);
                    packet.ReadSingle("BlendFromRadius", index);
                    packet.ReadSingle("InitialAngel", index);
                    packet.ReadSingle("ZOffset", index);

                    if (unk1)
                        packet.ReadPackedGuid128("AreaTriggerUnkGUID", index);

                    if (hasCenter)
                        packet.ReadVector3("Center", index);
                }
            }

            if (hasGameObject)
            {
                packet.ResetBitReader();
                packet.ReadInt32("WorldEffectID", index);

                var bit8 = packet.ReadBit("bit8", index);
                if (bit8)
                    packet.ReadInt32("Int1", index);
            }

            if (hasSmoothPhasing)
            {
                packet.ResetBitReader();
                packet.ReadBit("ReplaceActive", index);
                var replaceObject = packet.ReadBit();
                if (replaceObject)
                    packet.ReadPackedGuid128("ReplaceObject", index);
            }

            if (sceneObjCreate)
            {
                packet.ResetBitReader();

                var hasSceneLocalScriptData = packet.ReadBit("HasSceneLocalScriptData", index);
                var petBattleFullUpdate = packet.ReadBit("HasPetBattleFullUpdate", index);

                if (hasSceneLocalScriptData)
                {
                    packet.ResetBitReader();
                    var dataLength = packet.ReadBits(7);
                    packet.ReadWoWString("Data", dataLength, index);
                }

                if (petBattleFullUpdate)
                    V6_0_2_19033.Parsers.BattlePetHandler.ReadPetBattleFullUpdate(packet, index);
            }

            if (playerCreateData)
            {
                packet.ResetBitReader();
                var hasSceneInstanceIDs = packet.ReadBit("ScenePendingInstances", index);
                var hasRuneState = packet.ReadBit("Runes", index);

                if (hasSceneInstanceIDs)
                {
                    var sceneInstanceIDs = packet.ReadInt32("SceneInstanceIDsCount");
                    for (var i = 0; i < sceneInstanceIDs; ++i)
                        packet.ReadInt32("SceneInstanceIDs", index, i);
                }

                if (hasRuneState)
                {
                    packet.ReadByte("RechargingRuneMask", index);
                    packet.ReadByte("UsableRuneMask", index);
                    var runeCount = packet.ReadUInt32();
                    for (var i = 0; i < runeCount; ++i)
                        packet.ReadByte("RuneCooldown", index, i);
                }
            }

            return moveInfo;
        }
        private static Dictionary<int, UpdateField> ReadValuesUpdateBlock(Packet packet, ObjectType type, object index, bool isCreating)
        {
            var maskSize = packet.ReadByte();

            var updateMask = new int[maskSize];
            for (var i = 0; i < maskSize; i++)
                updateMask[i] = packet.ReadInt32();

            var mask = new BitArray(updateMask);
            var dict = new Dictionary<int, UpdateField>();

            int objectEnd = UpdateFields.GetUpdateField(ObjectField.OBJECT_END);
            for (var i = 0; i < mask.Count; ++i)
            {
                if (!mask[i])
                    continue;

                var blockVal = packet.ReadUpdateField();

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
                maskSize = packet.ReadByte();
                updateMask = new int[maskSize];
                for (var i = 0; i < maskSize; i++)
                    updateMask[i] = packet.ReadInt32();

                mask = new BitArray(updateMask);
                for (var i = 0; i < mask.Count; ++i)
                {
                    if (!mask[i])
                        continue;

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

                    var flag = packet.ReadUInt16();
                    var cnt = flag & 0x7FFF;
                    if ((flag & 0x8000) != 0)
                        packet.ReadUInt32(key + " Size", index);

                    var vals = new int[cnt];
                    for (var j = 0; j < cnt; ++j)
                        vals[j] = packet.ReadInt32();

                    var fieldMask = new BitArray(vals);
                    for (var j = 0; j < fieldMask.Count; ++j)
                    {
                        if (!fieldMask[j])
                            continue;

                        var blockVal = packet.ReadUpdateField();
                        string value = blockVal.UInt32Value + "/" + blockVal.SingleValue;
                        packet.AddValue(key, value, index, j);
                    }
                }
            }

            return dict;
        }
    }
}

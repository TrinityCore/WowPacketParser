using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.PacketStructures;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V7_0_3_22248.Enums;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using MovementFlag = WowPacketParser.Enums.v4.MovementFlag;
using MovementFlag2 = WowPacketParser.Enums.v7.MovementFlag2;
using SplineFlag = WowPacketParserModule.V7_0_3_22248.Enums.SplineFlag;

namespace WowPacketParserModule.V1_13_2_31446.Parsers
{
    public static class UpdateHandler
    {
        [HasSniffData] // in ReadCreateObjectBlock
        [Parser(Opcode.SMSG_UPDATE_OBJECT)]
        public static void HandleUpdateObject(Packet packet)
        {
            var updateObject = packet.Holder.UpdateObject = new();
            var count = packet.ReadUInt32("NumObjUpdates");
            uint map = updateObject.MapId = packet.ReadUInt16<MapId>("MapID");
            packet.ResetBitReader();
            var hasRemovedObjects = packet.ReadBit("HasRemovedObjects");
            if (hasRemovedObjects)
            {
                var destroyedObjCount = packet.ReadInt16("DestroyedObjCount");
                var removedObjCount = packet.ReadUInt32("RemovedObjCount"); // destroyed + out of range
                var outOfRangeObjCount = removedObjCount - destroyedObjCount;

                for (var i = 0; i < destroyedObjCount; i++)
                {
                    var partWriter = new StringBuilderProtoPart(packet.Writer);
                    var guid = packet.ReadPackedGuid128("ObjectGUID", "Destroyed", i);
                    updateObject.Destroyed.Add(new DestroyedObject(){Guid=guid, TextStartOffset = partWriter.StartOffset, TextLength = partWriter.Length, Text = partWriter.Text});
                }

                for (var i = 0; i < outOfRangeObjCount; i++)
                {
                    var partWriter = new StringBuilderProtoPart(packet.Writer);
                    var guid = packet.ReadPackedGuid128("ObjectGUID", "OutOfRange", i);
                    updateObject.OutOfRange.Add(new DestroyedObject(){Guid=guid, TextStartOffset = partWriter.StartOffset, TextLength = partWriter.Length, Text = partWriter.Text});
                }
            }
            packet.ReadUInt32("Data size");

            for (var i = 0; i < count; i++)
            {
                var type = (UpdateTypeCataclysm)packet.ReadByte();

                var partWriter = new StringBuilderProtoPart(packet.Writer);
                packet.AddValue("UpdateType", type.ToString(), i);
                switch (type)
                {
                    case UpdateTypeCataclysm.Values:
                    {
                        var guid = packet.ReadPackedGuid128("Object Guid", i);
                        var updateValues = new UpdateValues() {Legacy = new()};
                        CoreParsers.UpdateHandler.ReadValuesUpdateBlock(packet, updateValues.Legacy, guid, i);
                        updateObject.Updated.Add(new UpdateObject{Guid = guid, Values = updateValues, TextStartOffset = partWriter.StartOffset, TextLength = partWriter.Length, Text = partWriter.Text });
                        break;
                    }
                    case UpdateTypeCataclysm.CreateObject1:
                    case UpdateTypeCataclysm.CreateObject2:
                    {
                        var guid = packet.ReadPackedGuid128("Object Guid", i);
                        var createType = type.ToCreateObjectType();
                        var createObject = new CreateObject() { Guid = guid, Values = new() {Legacy = new()}, CreateType = createType };
                        ReadCreateObjectBlock(packet, createObject, guid, map, createType, i);
                        createObject.Text = partWriter.Text;
                        createObject.TextStartOffset = partWriter.StartOffset;
                        createObject.TextLength = partWriter.Length;
                        updateObject.Created.Add(createObject);
                        break;
                    }
                }
            }
        }

        private static void ReadCreateObjectBlock(Packet packet, CreateObject createObject, WowGuid guid, uint map, CreateObjectType createType, object index)
        {
            ObjectType objType = ObjectTypeConverter.Convert(packet.ReadByteE<ObjectType801>("Object Type", index));
            packet.ReadInt32("HeirFlags", index);

            WoWObject obj = CoreParsers.UpdateHandler.CreateObject(objType, guid, map);

            obj.CreateType = createType;
            obj.Movement = ReadMovementUpdateBlock(packet, createObject, guid, obj, index);
            obj.UpdateFields = CoreParsers.UpdateHandler.ReadValuesUpdateBlockOnCreate(packet, createObject.Values.Legacy, objType, index);
            obj.DynamicUpdateFields = CoreParsers.UpdateHandler.ReadDynamicValuesUpdateBlockOnCreate(packet, objType, index);

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

        private static MovementInfo ReadMovementUpdateBlock(Packet packet, CreateObject createObject, WowGuid guid, WoWObject obj, object index)
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
            var hasConversation = packet.ReadBit("HasConversation", index);

            if (hasMovementUpdate)
            {
                var movementUpdate = createObject.Movement = new();
                packet.ResetBitReader();
                movementUpdate.Mover = packet.ReadPackedGuid128("MoverGUID", index);

                movementUpdate.MoveTime = packet.ReadUInt32("MoveTime", index);
                movementUpdate.Position = moveInfo.Position = packet.ReadVector3("Position", index);
                movementUpdate.Orientation = moveInfo.Orientation = packet.ReadSingle("Orientation", index);

                movementUpdate.Pitch = packet.ReadSingle("Pitch", index);
                movementUpdate.StepUpStartElevation = packet.ReadSingle("StepUpStartElevation", index);

                var removeForcesIDsCount = packet.ReadInt32();
                movementUpdate.MoveIndex = packet.ReadInt32("MoveIndex", index);

                for (var i = 0; i < removeForcesIDsCount; i++)
                    packet.ReadPackedGuid128("RemoveForcesIDs", index, i);

                moveInfo.Flags = (uint)packet.ReadBitsE<MovementFlag>("Movement Flags", 30, index);
                moveInfo.Flags2 = (uint)packet.ReadBitsE<MovementFlag2>("Extra Movement Flags", 18, index);

                var hasTransport = packet.ReadBit("Has Transport Data", index);
                var hasFall = packet.ReadBit("Has Fall Data", index);
                packet.ReadBit("HasSpline", index);
                packet.ReadBit("HeightChangeFailed", index);
                packet.ReadBit("RemoteTimeValid", index);

                if (hasTransport)
                    movementUpdate.Transport = V8_0_1_27101.Parsers.UpdateHandler.ReadTransportData(moveInfo, guid, packet, index);

                if (hasFall)
                {
                    packet.ResetBitReader();
                    movementUpdate.FallTime = packet.ReadUInt32("Fall Time", index);
                    movementUpdate.JumpVelocity = packet.ReadSingle("JumpVelocity", index);

                    var hasFallDirection = packet.ReadBit("Has Fall Direction", index);
                    if (hasFallDirection)
                    {
                        packet.ReadVector2("Fall", index);
                        packet.ReadSingle("Horizontal Speed", index);
                    }
                }

                movementUpdate.WalkSpeed = moveInfo.WalkSpeed = packet.ReadSingle("WalkSpeed", index) / 2.5f;
                movementUpdate.RunSpeed = moveInfo.RunSpeed = packet.ReadSingle("RunSpeed", index) / 7.0f;
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
                    var splineData = movementUpdate.SplineData = new();
                    packet.ResetBitReader();
                    splineData.Id = packet.ReadInt32("ID", index);
                    splineData.Destination = packet.ReadVector3("Destination", index);

                    var hasMovementSplineMove = packet.ReadBit("MovementSplineMove", index);
                    if (hasMovementSplineMove)
                    {
                        var splineMove = splineData.MoveData = new();
                        packet.ResetBitReader();

                        splineMove.Flags = packet.ReadUInt32E<SplineFlag>("SplineFlags", index).ToUniversal();
                        splineMove.Elapsed = packet.ReadInt32("Elapsed", index);
                        splineMove.Duration = packet.ReadUInt32("Duration", index);
                        splineMove.DurationModifier = packet.ReadSingle("DurationModifier", index);
                        splineMove.NextDurationModifier = packet.ReadSingle("NextDurationModifier", index);

                        var face = packet.ReadBits("Face", 2, index);
                        var hasSpecialTime = packet.ReadBit("HasSpecialTime", index);

                        var pointsCount = packet.ReadBits("PointsCount", 16, index);

                        packet.ReadBitsE<SplineMode>("Mode", 2, index);

                        var hasSplineFilterKey = packet.ReadBit("HasSplineFilterKey", index);
                        var hasSpellEffectExtraData = packet.ReadBit("HasSpellEffectExtraData", index);
                        var hasJumpExtraData = packet.ReadBit("HasJumpExtraData", index);

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

                        switch (face)
                        {
                            case 1:
                                splineMove.LookPosition = packet.ReadVector3("FaceSpot", index);
                                break;
                            case 2:
                                splineMove.LookTarget = new() { Target = packet.ReadPackedGuid128("FaceGUID", index) };
                                break;
                            case 3:
                                splineMove.LookOrientation = packet.ReadSingle("FaceDirection", index);
                                break;
                            default:
                                break;
                        }

                        if (hasSpecialTime)
                            packet.ReadUInt32("SpecialTime", index);

                        for (var i = 0; i < pointsCount; ++i)
                            splineMove.Points.Add(packet.ReadVector3("Points", index, i));

                        if (hasSpellEffectExtraData)
                            V8_0_1_27101.Parsers.MovementHandler.ReadMonsterSplineSpellEffectExtraData(packet, index);

                        if (hasJumpExtraData)
                            splineMove.Jump = V8_0_1_27101.Parsers.MovementHandler.ReadMonsterSplineJumpExtraData(packet, index);
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
                createObject.Stationary = new() { Position = moveInfo.Position, Orientation = moveInfo.Orientation };
            }

            if (hasCombatVictim)
                packet.ReadPackedGuid128("CombatVictim Guid", index);

            if (hasServerTime)
                packet.ReadUInt32("ServerTime", index);

            if (hasVehicleCreate)
            {
                var vehicle = createObject.Vehicle = new();
                moveInfo.VehicleId = packet.ReadUInt32("RecID", index);
                vehicle.VehicleId = (int)moveInfo.VehicleId;
                vehicle.InitialRawFacing = packet.ReadSingle("InitialRawFacing", index);
            }

            if (hasAnimKitCreate)
            {
                packet.ReadUInt16("AiID", index);
                packet.ReadUInt16("MovementID", index);
                packet.ReadUInt16("MeleeID", index);
            }

            if (hasRotation)
                createObject.Rotation = moveInfo.Rotation = packet.ReadPackedQuaternion("GameObject Rotation", index);

            for (var i = 0; i < pauseTimesCount; ++i)
                packet.ReadInt32("PauseTimes", index, i);

            if (hasMovementTransport)
            {
                moveInfo.Transport = new MovementInfo.TransportInfo();
                var transport = createObject.Transport = new();
                packet.ResetBitReader();
                transport.TransportGuid = moveInfo.Transport.Guid = packet.ReadPackedGuid128("TransportGUID", index);
                transport.Position = moveInfo.Transport.Offset = packet.ReadVector4("TransportPosition", index);
                var seat = packet.ReadByte("VehicleSeatIndex", index);
                transport.Seat = seat;
                transport.MoveTime = packet.ReadUInt32("MoveTime", index);

                var hasPrevMoveTime = packet.ReadBit("HasPrevMoveTime", index);
                var hasVehicleRecID = packet.ReadBit("HasVehicleRecID", index);

                if (hasPrevMoveTime)
                    transport.PrevMoveTime = packet.ReadUInt32("PrevMoveTime", index);

                if (hasVehicleRecID)
                    transport.VehicleId = packet.ReadInt32("VehicleRecID", index);

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

            if (hasAreaTrigger && obj is AreaTriggerCreateProperties)
            {
                AreaTriggerTemplate areaTriggerTemplate = new AreaTriggerTemplate
                {
                    Id = guid.GetEntry()
                };

                AreaTriggerCreateProperties spellAreaTrigger = (AreaTriggerCreateProperties)obj;
                spellAreaTrigger.AreaTriggerId = guid.GetEntry();

                packet.ResetBitReader();

                // CliAreaTrigger
                packet.ReadUInt32("ElapsedMs", index);

                packet.ReadVector3("RollPitchYaw", index);

                areaTriggerTemplate.Flags   = 0;

                if (packet.ReadBit("HasAbsoluteOrientation", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerCreatePropertiesFlags.HasAbsoluteOrientation;

                if (packet.ReadBit("HasDynamicShape", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerCreatePropertiesFlags.HasDynamicShape;

                if (packet.ReadBit("HasAttached", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerCreatePropertiesFlags.HasAttached;

                if (packet.ReadBit("HasFaceMovementDir", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerCreatePropertiesFlags.FaceMovementDirection;

                if (packet.ReadBit("HasFollowsTerrain", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerCreatePropertiesFlags.FollowsTerrain;

                if (packet.ReadBit("Unk bit WoD62x", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerCreatePropertiesFlags.Unk1;

                if (packet.ReadBit("HasTargetRollPitchYaw", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerCreatePropertiesFlags.HasTargetRollPitchYaw;

                bool hasScaleCurveID = packet.ReadBit("HasScaleCurveID", index);
                bool hasMorphCurveID = packet.ReadBit("HasMorphCurveID", index);
                bool hasFacingCurveID = packet.ReadBit("HasFacingCurveID", index);
                bool hasMoveCurveID = packet.ReadBit("HasMoveCurveID", index);

                if (packet.ReadBit("HasAnimID", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerCreatePropertiesFlags.HasAnimId;

                if (packet.ReadBit("HasAnimKitID", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerCreatePropertiesFlags.HasAnimKitId;

                if (packet.ReadBit("HasVisualAnimIsDecay", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerCreatePropertiesFlags.VisualAnimIsDecay;

                bool hasAnimProgress = packet.ReadBit("HasAnimProgress", index);

                if (packet.ReadBit("HasAreaTriggerSphere", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Sphere;

                if (packet.ReadBit("HasAreaTriggerBox", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Box;

                if (packet.ReadBit("HasAreaTriggerPolygon", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Polygon;

                if (packet.ReadBit("HasAreaTriggerCylinder", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Cylinder;

                bool hasAreaTriggerSpline = packet.ReadBit("HasAreaTriggerSpline", index);

                if (packet.ReadBit("HasAreaTriggerOrbit", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerCreatePropertiesFlags.HasOrbit;

                if ((areaTriggerTemplate.Flags & (uint)AreaTriggerCreatePropertiesFlags.VisualAnimIsDecay) != 0)
                    if (!packet.ReadBit("VisualAnimIsDecay", index))
                        areaTriggerTemplate.Flags &= ~(uint)AreaTriggerCreatePropertiesFlags.VisualAnimIsDecay;

                if (hasAreaTriggerSpline)
                    foreach (var splinePoint in V7_0_3_22248.Parsers.AreaTriggerHandler.ReadAreaTriggerSpline(spellAreaTrigger, packet, index, "AreaTriggerSpline"))
                        Storage.AreaTriggerCreatePropertiesSplinePoints.Add(splinePoint);

                if ((areaTriggerTemplate.Flags & (uint)AreaTriggerCreatePropertiesFlags.HasTargetRollPitchYaw) != 0)
                    packet.ReadVector3("TargetRollPitchYaw", index);

                if (hasScaleCurveID)
                    spellAreaTrigger.ScaleCurveId = (int)packet.ReadUInt32("ScaleCurveID", index);

                if (hasMorphCurveID)
                    spellAreaTrigger.MorphCurveId = (int)packet.ReadUInt32("MorphCurveID", index);

                if (hasFacingCurveID)
                    spellAreaTrigger.FacingCurveId = (int)packet.ReadUInt32("FacingCurveID", index);

                if (hasMoveCurveID)
                    spellAreaTrigger.MoveCurveId = (int)packet.ReadUInt32("MoveCurveID", index);

                if ((areaTriggerTemplate.Flags & (int)AreaTriggerCreatePropertiesFlags.HasAnimId) != 0)
                    spellAreaTrigger.AnimId = packet.ReadInt32("AnimId", index);

                if ((areaTriggerTemplate.Flags & (int)AreaTriggerCreatePropertiesFlags.HasAnimKitId) != 0)
                    spellAreaTrigger.AnimKitId = packet.ReadInt32("AnimKitId", index);

                if (hasAnimProgress)
                    packet.ReadUInt32("AnimProgress", index);

                if (areaTriggerTemplate.Type == (byte)AreaTriggerType.Sphere)
                {
                    areaTriggerTemplate.Data[0] = packet.ReadSingle("Radius", index);
                    areaTriggerTemplate.Data[1] = packet.ReadSingle("RadiusTarget", index);
                }

                if (areaTriggerTemplate.Type == (byte)AreaTriggerType.Box)
                {
                    Vector3 Extents = packet.ReadVector3("Extents", index);
                    Vector3 ExtentsTarget = packet.ReadVector3("ExtentsTarget", index);

                    areaTriggerTemplate.Data[0] = Extents.X;
                    areaTriggerTemplate.Data[1] = Extents.Y;
                    areaTriggerTemplate.Data[2] = Extents.Z;

                    areaTriggerTemplate.Data[3] = ExtentsTarget.X;
                    areaTriggerTemplate.Data[4] = ExtentsTarget.Y;
                    areaTriggerTemplate.Data[5] = ExtentsTarget.Z;
                }

                if (areaTriggerTemplate.Type == (byte)AreaTriggerType.Polygon)
                {
                    var verticesCount = packet.ReadUInt32("VerticesCount", index);
                    var verticesTargetCount = packet.ReadUInt32("VerticesTargetCount", index);

                    List<AreaTriggerCreatePropertiesPolygonVertex> verticesList = new List<AreaTriggerCreatePropertiesPolygonVertex>();

                    areaTriggerTemplate.Data[0] = packet.ReadSingle("Height", index);
                    areaTriggerTemplate.Data[1] = packet.ReadSingle("HeightTarget", index);

                    for (uint i = 0; i < verticesCount; ++i)
                    {
                        AreaTriggerCreatePropertiesPolygonVertex spellAreatriggerVertices = new AreaTriggerCreatePropertiesPolygonVertex
                        {
                            areatriggerGuid = guid,
                            Idx = i
                        };

                        Vector2 vertices = packet.ReadVector2("Vertices", index, i);

                        spellAreatriggerVertices.VerticeX = vertices.X;
                        spellAreatriggerVertices.VerticeY = vertices.Y;

                        verticesList.Add(spellAreatriggerVertices);
                    }

                    for (var i = 0; i < verticesTargetCount; ++i)
                    {
                        Vector2 verticesTarget = packet.ReadVector2("VerticesTarget", index, i);

                        verticesList[i].VerticeTargetX = verticesTarget.X;
                        verticesList[i].VerticeTargetY = verticesTarget.Y;
                    }

                    foreach (AreaTriggerCreatePropertiesPolygonVertex vertice in verticesList)
                        Storage.AreaTriggerCreatePropertiesPolygonVertices.Add(vertice);
                }

                if (areaTriggerTemplate.Type == (byte)AreaTriggerType.Cylinder)
                {
                    areaTriggerTemplate.Data[0] = packet.ReadSingle("Radius", index);
                    areaTriggerTemplate.Data[1] = packet.ReadSingle("RadiusTarget", index);
                    areaTriggerTemplate.Data[2] = packet.ReadSingle("Height", index);
                    areaTriggerTemplate.Data[3] = packet.ReadSingle("HeightTarget", index);
                    areaTriggerTemplate.Data[4] = packet.ReadSingle("LocationZOffset", index);
                    areaTriggerTemplate.Data[5] = packet.ReadSingle("LocationZOffsetTarget", index);
                }

                if ((areaTriggerTemplate.Flags & (uint)AreaTriggerCreatePropertiesFlags.HasOrbit) != 0)
                    Storage.AreaTriggerCreatePropertiesOrbits.Add(V7_0_3_22248.Parsers.AreaTriggerHandler.ReadAreaTriggerOrbit(spellAreaTrigger, packet, index, "AreaTriggerOrbit"));

                spellAreaTrigger.Shape = areaTriggerTemplate.Type;
                Array.Copy(areaTriggerTemplate.Data, spellAreaTrigger.ShapeData, Math.Min(areaTriggerTemplate.Data.Length, spellAreaTrigger.ShapeData.Length));

                Storage.AreaTriggerTemplates.Add(areaTriggerTemplate);
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
                var hasActionButtons = packet.ReadBit("Unk1132", index);

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

                if (hasActionButtons)
                {
                    for (int i = 0; i < 132; i++)
                        packet.ReadInt32("Action", index, i);
                }
            }

            if (hasConversation)
            {
                packet.ResetBitReader();
                if (packet.ReadBit("HasTextureKitID", index))
                    (obj as ConversationTemplate).TextureKitId = packet.ReadUInt32("TextureKitID", index);
            }
            return moveInfo;
        }
    }
}

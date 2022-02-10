﻿using System.Collections;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.PacketStructures;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using SplineFlag = WowPacketParserModule.V7_0_3_22248.Enums.SplineFlag;

namespace WowPacketParserModule.V2_5_1_38707.Parsers
{
    public static class UpdateHandler
    {
        [HasSniffData] // in ReadCreateObjectBlock
        [Parser(Opcode.SMSG_UPDATE_OBJECT)]
        public static void HandleUpdateObject(Packet packet)
        {
            var updateObject = packet.Holder.UpdateObject = new();
            var count = packet.ReadUInt32("NumObjUpdates");
            uint map = packet.ReadUInt16<MapId>("MapID");
            packet.ResetBitReader();
            var hasRemovedObjects = packet.ReadBit("HasRemovedObjects");
            if (hasRemovedObjects)
            {
                var destroyedObjCount = packet.ReadInt16("DestroyedObjCount");
                var removedObjCount = packet.ReadUInt32("RemovedObjCount"); // destroyed + out of range
                var outOfRangeObjCount = removedObjCount - destroyedObjCount;

                for (var i = 0; i < destroyedObjCount; i++)
                {
                    WowGuid guid = packet.ReadPackedGuid128("ObjectGUID", "Destroyed", i);
                }
                for (var i = 0; i < outOfRangeObjCount; i++)
                {
                    WowGuid guid = packet.ReadPackedGuid128("ObjectGUID", "OutOfRange", i);
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
                        updateObject.Updated.Add(new UpdateObject { Guid = guid, Values = updateValues, Text = partWriter.Text });
                        break;
                    }
                    case UpdateTypeCataclysm.CreateObject1:
                    case UpdateTypeCataclysm.CreateObject2:
                    {
                        var guid = packet.ReadPackedGuid128("Object Guid", i);
                        var createObject = new CreateObject() { Guid = guid, Values = new(){Legacy = new()}, CreateType = type.ToCreateObjectType()};
                        ReadCreateObjectBlock(packet, createObject, guid, map, i);
                        createObject.Text = partWriter.Text;
                        updateObject.Created.Add(createObject);
                        break;
                    }
                }
            }
        }

        private static void ReadCreateObjectBlock(Packet packet, CreateObject createObject, WowGuid guid, uint map, object index)
        {
            ObjectType objType = ObjectTypeConverter.Convert(packet.ReadByteE<ObjectTypeBCC>("Object Type", index));
            packet.ReadInt32("HeirFlags", index);

            WoWObject obj = CoreParsers.UpdateHandler.CreateObject(objType, guid, map);

            obj.Movement = ReadMovementUpdateBlock(packet, guid, obj, index);
            obj.UpdateFields = CoreParsers.UpdateHandler.ReadValuesUpdateBlockOnCreate(packet, createObject.Values.Legacy, objType, index);
            obj.DynamicUpdateFields = CoreParsers.UpdateHandler.ReadDynamicValuesUpdateBlockOnCreate(packet, objType, index);

            // If this is the second time we see the same object (same guid,
            // same position) update its phasemask
            if (Storage.Objects.ContainsKey(guid))
            {
                var existObj = Storage.Objects[guid].Item1;
                CoreParsers.UpdateHandler.ProcessExistingObject(ref existObj, obj, guid);
            }
            else
                Storage.Objects.Add(guid, obj, packet.TimeSpan);

            if (guid.HasEntry() && (objType == ObjectType.Unit || objType == ObjectType.GameObject))
                packet.AddSniffData(Utilities.ObjectTypeToStore(objType), (int)guid.GetEntry(), "SPAWN");
        }

        public static float GetAngle(float x1, float y1, float x2, float y2)
        {
            float dx = x1 - x2;
            float dy = y1 - y2;

            double ang = System.Math.Atan2(dy, dx);
            ang = (ang >= 0) ? ang : 2 * System.Math.PI + ang;
            return (float)ang;
        }

        private static MovementInfo ReadMovementUpdateBlock(Packet packet, WowGuid guid, WoWObject obj, object index)
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

            var isSelf = packet.ReadBit("ThisIsYou", index);

            var sceneObjCreate = packet.ReadBit("SceneObjCreate", index);
            var playerCreateData = packet.ReadBit("HasPlayerCreateData", index);
            var hasConversation = packet.ReadBit("HasConversation", index);

            if (hasMovementUpdate)
            {
                packet.ResetBitReader();
                packet.ReadPackedGuid128("MoverGUID", index);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V2_5_3_41818))
                {
                    moveInfo.Flags = (MovementFlag)packet.ReadUInt32E<V6_0_2_19033.Enums.MovementFlag>("Movement Flags", index);
                    moveInfo.FlagsExtra = (MovementFlagExtra)packet.ReadUInt32E<V8_0_1_27101.Enums.MovementFlags2>("Extra Movement Flags", index);

                    packet.ReadUInt32("MovementFlags3", index);
                }

                packet.ReadUInt32("MoveTime", index);
                moveInfo.Position = packet.ReadVector3("Position", index);
                moveInfo.Orientation = packet.ReadSingle("Orientation", index);

                packet.ReadSingle("Pitch", index);
                packet.ReadSingle("StepUpStartElevation", index);

                var removeForcesIDsCount = packet.ReadInt32();
                packet.ReadInt32("MoveIndex", index);

                for (var i = 0; i < removeForcesIDsCount; i++)
                    packet.ReadPackedGuid128("RemoveForcesIDs", index, i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V2_5_3_41818))
                {
                    moveInfo.Flags = (MovementFlag)packet.ReadBitsE<V6_0_2_19033.Enums.MovementFlag>("Movement Flags", 30, index);
                    moveInfo.FlagsExtra = (MovementFlagExtra)packet.ReadBitsE<V8_0_1_27101.Enums.MovementFlags2>("Extra Movement Flags", 18, index);
                }

                var hasTransport = packet.ReadBit("Has Transport Data", index);
                var hasFall = packet.ReadBit("Has Fall Data", index);
                packet.ReadBit("HasSpline", index);
                packet.ReadBit("HeightChangeFailed", index);
                packet.ReadBit("RemoteTimeValid", index);

                if (hasTransport)
                    V8_0_1_27101.Parsers.UpdateHandler.ReadTransportData(moveInfo, guid, packet, index);

                if (hasFall)
                {
                    packet.ResetBitReader();
                    packet.ReadUInt32("Jump Fall Time", index);
                    packet.ReadSingle("Jump Vertical Speed", index);

                    var hasFallDirection = packet.ReadBit("Has Fall Direction", index);
                    if (hasFallDirection)
                    {
                        packet.ReadSingle("Jump Sin Angle", index);
                        packet.ReadSingle("Jump Cos Angle", index);
                        packet.ReadSingle("Jump Horizontal Speed", index);
                    }
                }

                moveInfo.WalkSpeed = packet.ReadSingle("WalkSpeed", index);
                moveInfo.RunSpeed = packet.ReadSingle("RunSpeed", index);
                packet.ReadSingle("RunBackSpeed", index);
                packet.ReadSingle("SwimSpeed", index);
                packet.ReadSingle("SwimBackSpeed", index);
                packet.ReadSingle("FlightSpeed", index);
                packet.ReadSingle("FlightBackSpeed", index);
                packet.ReadSingle("TurnRate", index);
                packet.ReadSingle("PitchRate", index);

                var movementForceCount = packet.ReadInt32("MovementForceCount", index);

                packet.ReadSingle("MovementForcesModMagnitude", index);

                packet.ResetBitReader();

                moveInfo.HasSplineData = packet.ReadBit("HasMovementSpline", index);

                for (var i = 0; i < movementForceCount; ++i)
                {
                    packet.ResetBitReader();
                    packet.ReadPackedGuid128("Id", index);
                    packet.ReadVector3("Origin", index);
                    packet.ReadVector3("Direction", index);
                    packet.ReadUInt32("TransportID", index);
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

                        packet.ReadUInt32E<SplineFlag>("SplineFlags", index);
                        packet.ReadInt32("Elapsed", index);
                        packet.ReadUInt32("Duration", index);
                        packet.ReadSingle("DurationModifier", index);
                        packet.ReadSingle("NextDurationModifier", index);

                        var face = packet.ReadBits("Face", 2, index);
                        var hasSpecialTime = packet.ReadBit("HasSpecialTime", index);

                        var pointsCount = packet.ReadBits("PointsCount", 16, index);

                        var hasSplineFilterKey = packet.ReadBit("HasSplineFilterKey", index);
                        var hasSpellEffectExtraData = packet.ReadBit("HasSpellEffectExtraData", index);
                        var hasJumpExtraData = packet.ReadBit("HasJumpExtraData", index);

                        var hasAnimationTierTransition = packet.ReadBit("HasAnimationTierTransition", index);
                        var hasUnknown901 = packet.ReadBit("Unknown901", index);

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

                        float orientation = 100;
                        switch (face)
                        {
                            case 1:
                                var faceSpot = packet.ReadVector3("FaceSpot", index);
                                orientation = GetAngle(moveInfo.Position.X, moveInfo.Position.Y, faceSpot.X, faceSpot.Y);
                                break;
                            case 2:
                                packet.ReadPackedGuid128("FaceGUID", index);
                                break;
                            case 3:
                                orientation = packet.ReadSingle("FaceDirection", index);
                                break;
                            default:
                                break;
                        }

                        if (hasSpecialTime)
                            packet.ReadUInt32("SpecialTime", index);

                        for (var i = 0; i < pointsCount; ++i)
                        {
                            var spot = packet.ReadVector3("Points", index, i);
                        }

                        if (hasSpellEffectExtraData)
                            V8_0_1_27101.Parsers.MovementHandler.ReadMonsterSplineSpellEffectExtraData(packet, index);

                        if (hasJumpExtraData)
                            V8_0_1_27101.Parsers.MovementHandler.ReadMonsterSplineJumpExtraData(packet, index);

                        if (hasAnimationTierTransition)
                        {
                            packet.ReadInt32("TierTransitionID", index);
                            packet.ReadInt32("StartTime", index);
                            packet.ReadInt32("EndTime", index);
                            packet.ReadByte("AnimTier", index);
                        }

                        if (hasUnknown901)
                        {
                            for (var i = 0; i < 16; ++i)
                            {
                                packet.ReadInt32("Unknown1", index, "Unknown901", i);
                                packet.ReadInt32("Unknown2", index, "Unknown901", i);
                                packet.ReadInt32("Unknown3", index, "Unknown901", i);
                                packet.ReadInt32("Unknown4", index, "Unknown901", i);
                            }
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
                sbyte seat;
                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V2_5_3_41818))
                    seat = (sbyte)packet.ReadByte("VehicleSeatIndex", index);
                else
                    seat = packet.ReadSByte("VehicleSeatIndex", index);

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
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasAbsoluteOrientation;

                if (packet.ReadBit("HasDynamicShape", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasDynamicShape;

                if (packet.ReadBit("HasAttached", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasAttached;

                if (packet.ReadBit("HasFaceMovementDir", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.FaceMovementDirection;

                if (packet.ReadBit("HasFollowsTerrain", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.FollowsTerrain;

                if (packet.ReadBit("Unk bit WoD62x", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.Unk1;

                if (packet.ReadBit("HasTargetRollPitchYaw", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasTargetRollPitchYaw;

                bool hasScaleCurveID = packet.ReadBit("HasScaleCurveID", index);
                bool hasMorphCurveID = packet.ReadBit("HasMorphCurveID", index);
                bool hasFacingCurveID = packet.ReadBit("HasFacingCurveID", index);
                bool hasMoveCurveID = packet.ReadBit("HasMoveCurveID", index);

                if (packet.ReadBit("HasAnimID", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasAnimId;

                if (packet.ReadBit("HasAnimKitID", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasAnimKitId;

                if (packet.ReadBit("unkbit50", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.Unk3;

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
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasOrbit;

                if ((areaTriggerTemplate.Flags & (uint)AreaTriggerFlags.Unk3) != 0)
                    packet.ReadBit();

                if (hasAreaTriggerSpline)
                    V7_0_3_22248.Parsers.AreaTriggerHandler.ReadAreaTriggerSpline(spellAreaTrigger, packet, index);

                if ((areaTriggerTemplate.Flags & (uint)AreaTriggerFlags.HasTargetRollPitchYaw) != 0)
                    packet.ReadVector3("TargetRollPitchYaw", index);

                if (hasScaleCurveID)
                    spellAreaTrigger.ScaleCurveId = (int)packet.ReadUInt32("ScaleCurveID", index);

                if (hasMorphCurveID)
                    spellAreaTrigger.MorphCurveId = (int)packet.ReadUInt32("MorphCurveID", index);

                if (hasFacingCurveID)
                    spellAreaTrigger.FacingCurveId = (int)packet.ReadUInt32("FacingCurveID", index);

                if (hasMoveCurveID)
                    spellAreaTrigger.MoveCurveId = (int)packet.ReadUInt32("MoveCurveID", index);

                if ((areaTriggerTemplate.Flags & (int)AreaTriggerFlags.HasAnimId) != 0)
                    spellAreaTrigger.AnimId = packet.ReadInt32("AnimId", index);

                if ((areaTriggerTemplate.Flags & (int)AreaTriggerFlags.HasAnimKitId) != 0)
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

                    areaTriggerTemplate.Data[0] = packet.ReadSingle("Height", index);
                    areaTriggerTemplate.Data[1] = packet.ReadSingle("HeightTarget", index);

                    for (uint i = 0; i < verticesCount; ++i)
                    {
                        Vector2 vertices = packet.ReadVector2("Vertices", index, i);
                    }

                    for (var i = 0; i < verticesTargetCount; ++i)
                    {
                        Vector2 verticesTarget = packet.ReadVector2("VerticesTarget", index, i);
                    }
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

                if ((areaTriggerTemplate.Flags & (uint)AreaTriggerFlags.HasOrbit) != 0)
                    V7_0_3_22248.Parsers.AreaTriggerHandler.ReadAreaTriggerOrbit(guid, packet, "Orbit");

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

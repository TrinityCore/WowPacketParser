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

namespace WowPacketParserModule.V6_0_2_19033.Parsers
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
                    updateObject.Destroyed.Add(new DestroyedObject(){Guid = guid, TextStartOffset = partWriter.StartOffset, TextLength = partWriter.Length, Text = partWriter.Text });
                }
                for (var i = 0; i < outOfRangeObjCount; i++)
                {
                    var partWriter = new StringBuilderProtoPart(packet.Writer);
                    var guid = packet.ReadPackedGuid128("ObjectGUID", "OutOfRange", i);
                    updateObject.OutOfRange.Add(new DestroyedObject(){Guid = guid, TextStartOffset = partWriter.StartOffset, TextLength = partWriter.Length, Text = partWriter.Text });
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
                        var updateValues = new UpdateValues(){Legacy = new()};
                        CoreParsers.UpdateHandler.ReadValuesUpdateBlock(packet, updateValues.Legacy, guid, i);
                        updateObject.Updated.Add(new UpdateObject{Guid = guid, Values = updateValues, TextStartOffset = partWriter.StartOffset, TextLength = partWriter.Length, Text = partWriter.Text});
                        break;
                    }
                    case UpdateTypeCataclysm.CreateObject1:
                    case UpdateTypeCataclysm.CreateObject2: // Might != CreateObject1 on Cata
                    {
                        var guid = packet.ReadPackedGuid128("Object Guid", i);
                        var createType = type.ToCreateObjectType();
                        var createObject = new CreateObject() { Guid = guid, Values = new(){Legacy = new()}, CreateType = createType };
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
            ObjectType objType = ObjectTypeConverter.Convert(packet.ReadByteE<ObjectTypeLegacy>("Object Type", index));
            WoWObject obj = CoreParsers.UpdateHandler.CreateObject(objType, guid, map);

            obj.CreateType = createType;
            obj.Movement = ReadMovementUpdateBlock(packet, createObject, guid, index);
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

        private static MovementInfo ReadMovementUpdateBlock(Packet packet, CreateObject createObject, WowGuid guid, object index)
        {
            var moveInfo = new MovementInfo();

            packet.ResetBitReader();

            packet.ReadBit("NoBirthAnim", index);
            packet.ReadBit("EnablePortals", index);
            packet.ReadBit("PlayHoverAnim", index);
            packet.ReadBit("IsSuppressingGreetings", index);

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

            packet.ReadBit("ThisIsYou", index);
            packet.ReadBit("ReplaceActive", index);

            var sceneObjCreate = packet.ReadBit("SceneObjCreate", index);
            var scenePendingInstances = packet.ReadBit("ScenePendingInstances", index);

            var pauseTimesCount = packet.ReadUInt32("PauseTimesCount", index);

            if (hasMovementUpdate) // 392
            {
                var movementUpdate = createObject.Movement = new();
                moveInfo = ReadMovementStatusData(packet, movementUpdate, index);

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

                for (var i = 0; i < movementForceCount; ++i)
                {
                    packet.ReadPackedGuid128("Id", index);
                    packet.ReadVector3("Direction", index);
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_2_19802)) // correct?
                        packet.ReadVector3("TransportPosition", index);
                    packet.ReadInt32("TransportID", index);
                    packet.ReadSingle("Magnitude", index);
                    packet.ReadByte("Type", index);
                }

                packet.ResetBitReader();

                moveInfo.HasSplineData = packet.ReadBit("HasMovementSpline", index);

                if (moveInfo.HasSplineData)
                {
                    var splineData = movementUpdate.SplineData = new();
                    splineData.Id = packet.ReadInt32("ID", index);
                    splineData.Destination = packet.ReadVector3("Destination", index);

                    packet.ResetBitReader();

                    var hasMovementSplineMove = packet.ReadBit("MovementSplineMove", index);
                    if (hasMovementSplineMove)
                    {
                        var splineMove = splineData.MoveData = new();
                        packet.ResetBitReader();

                        var flags = packet.ReadBitsE<SplineFlag434>("SplineFlags", ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173) ? 28 : 25, index);
                        splineMove.Flags = flags.ToUniversal();
                        var face = packet.ReadBits("Face", 2, index);

                        var hasJumpGravity = packet.ReadBit("HasJumpGravity", index);
                        var hasSpecialTime = packet.ReadBit("HasSpecialTime", index);

                        packet.ReadBitsE<SplineMode>("Mode", 2, index);

                        var hasSplineFilterKey = packet.ReadBit("HasSplineFilterKey", index);

                        splineMove.Elapsed = (int)packet.ReadUInt32("Elapsed", index);
                        splineMove.Duration = packet.ReadUInt32("Duration", index);

                        splineMove.DurationModifier = packet.ReadSingle("DurationModifier", index);
                        splineMove.NextDurationModifier = packet.ReadSingle("NextDurationModifier", index);

                        var pointsCount = packet.ReadUInt32("PointsCount", index);

                        if (face == 3) // FaceDirection
                            splineMove.LookOrientation = packet.ReadSingle("FaceDirection", index);

                        if (face == 2) // FaceGUID
                            splineMove.LookTarget = new() { Target = packet.ReadPackedGuid128("FaceGUID", index) };

                        if (face == 1) // FaceSpot
                            splineMove.LookPosition = packet.ReadVector3("FaceSpot", index);

                        if (hasJumpGravity)
                            splineMove.Jump = new() { Gravity = packet.ReadSingle("JumpGravity", index) };

                        if (hasSpecialTime)
                            packet.ReadInt32("SpecialTime", index);

                        if (hasSplineFilterKey)
                        {
                            var filterKeysCount = packet.ReadUInt32("FilterKeysCount", index);
                            for (var i = 0; i < filterKeysCount; ++i)
                            {
                                packet.ReadSingle("In", index, i);
                                packet.ReadSingle("Out", index, i);
                            }

                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                                packet.ResetBitReader();

                            packet.ReadBits("FilterFlags", 2, index);
                        }

                        for (var i = 0; i < pointsCount; ++i)
                            splineMove.Points.Add(packet.ReadVector3("Points", index, i));
                    }
                }
            }

            if (hasMovementTransport) // 456
            {
                moveInfo.Transport = new MovementInfo.TransportInfo();
                var transport = createObject.Transport = new();
                transport.TransportGuid = moveInfo.Transport.Guid = packet.ReadPackedGuid128("TransportGUID", index);
                transport.Position = moveInfo.Transport.Offset = packet.ReadVector4("TransportPosition", index);
                var seat = packet.ReadByte("VehicleSeatIndex", index);
                transport.Seat = seat;
                transport.MoveTime = packet.ReadUInt32("MoveTime", index);

                packet.ResetBitReader();

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

            if (hasStationaryPosition) // 480
            {
                moveInfo.Position = packet.ReadVector3();
                moveInfo.Orientation = packet.ReadSingle();

                packet.AddValue("Stationary Position", moveInfo.Position, index);
                packet.AddValue("Stationary Orientation", moveInfo.Orientation, index);
                createObject.Stationary = new() { Position = moveInfo.Position, Orientation = moveInfo.Orientation };
            }

            if (hasCombatVictim) // 504
                packet.ReadPackedGuid128("CombatVictim Guid", index);

            if (hasServerTime) // 516
                packet.ReadUInt32("ServerTime", index);

            if (hasVehicleCreate) // 528
            {
                var vehicle = createObject.Vehicle = new();
                moveInfo.VehicleId = packet.ReadUInt32("RecID", index);
                vehicle.VehicleId = (int)moveInfo.VehicleId;
                vehicle.InitialRawFacing = packet.ReadSingle("InitialRawFacing", index);
            }

            if (hasAnimKitCreate) // 538
            {
                packet.ReadUInt16("AiID", index);
                packet.ReadUInt16("MovementID", index);
                packet.ReadUInt16("MeleeID", index);
            }

            if (hasRotation) // 552
                createObject.Rotation = moveInfo.Rotation = packet.ReadPackedQuaternion("GameObject Rotation", index);

            if (hasAreaTrigger) // 772
            {
                // CliAreaTrigger
                packet.ReadInt32("ElapsedMs", index);

                packet.ReadVector3("RollPitchYaw1", index);

                packet.ResetBitReader();

                packet.ReadBit("HasAbsoluteOrientation", index);
                packet.ReadBit("HasDynamicShape", index);
                packet.ReadBit("HasAttached", index);
                packet.ReadBit("HasFaceMovementDir", index);
                packet.ReadBit("HasFollowsTerrain", index);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                    packet.ReadBit("Unk bit WoD62x", index);

                var hasTargetRollPitchYaw = packet.ReadBit("HasTargetRollPitchYaw", index);
                var hasScaleCurveID = packet.ReadBit("HasScaleCurveID", index);
                var hasMorphCurveID = packet.ReadBit("HasMorphCurveID", index);
                var hasFacingCurveID = packet.ReadBit("HasFacingCurveID", index);
                var hasMoveCurveID = packet.ReadBit("HasMoveCurveID", index);
                var hasAreaTriggerSphere = packet.ReadBit("HasAreaTriggerSphere", index);
                var hasAreaTriggerBox = packet.ReadBit("HasAreaTriggerBox", index);
                var hasAreaTriggerPolygon = packet.ReadBit("HasAreaTriggerPolygon", index);
                var hasAreaTriggerCylinder = packet.ReadBit("HasAreaTriggerCylinder", index);
                var hasAreaTriggerSpline = packet.ReadBit("HasAreaTriggerSpline", index);

                if (hasTargetRollPitchYaw)
                    packet.ReadVector3("TargetRollPitchYaw", index);

                if (hasScaleCurveID)
                    packet.ReadInt32("ScaleCurveID, index");

                if (hasMorphCurveID)
                    packet.ReadInt32("MorphCurveID", index);

                if (hasFacingCurveID)
                    packet.ReadInt32("FacingCurveID", index);

                if (hasMoveCurveID)
                    packet.ReadInt32("MoveCurveID", index);

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
                    packet.ReadSingle("Float4", index);
                    packet.ReadSingle("Float5", index);
                }

                if (hasAreaTriggerSpline)
                    AreaTriggerHandler.ReadAreaTriggerSpline(packet, index);
            }

            if (hasGameObject) // 788
            {
                packet.ReadInt32("WorldEffectID", index);

                packet.ResetBitReader();

                var bit8 = packet.ReadBit("bit8", index);
                if (bit8)
                    packet.ReadInt32("Int1", index);
            }

            if (sceneObjCreate) // 1184
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
                    BattlePetHandler.ReadPetBattleFullUpdate(packet, index);
            }

            if (scenePendingInstances) // 1208
            {
                var sceneInstanceIDs = packet.ReadInt32("SceneInstanceIDsCount");

                for (var i = 0; i < sceneInstanceIDs; ++i)
                    packet.ReadInt32("SceneInstanceIDs", index, i);
            }

            for (var i = 0; i < pauseTimesCount; ++i)
                packet.ReadInt32("PauseTimes", index, i);

            return moveInfo;
        }

        private static MovementInfo ReadMovementStatusData(Packet packet, MovementUpdate movementUpdate, object index)
        {
            var moveInfo = new MovementInfo();

            movementUpdate.Mover = packet.ReadPackedGuid128("MoverGUID", index);

            movementUpdate.MoveIndex = (int)packet.ReadUInt32("MoveIndex", index);
            movementUpdate.Position = moveInfo.Position = packet.ReadVector3("Position", index);
            movementUpdate.Orientation = moveInfo.Orientation = packet.ReadSingle("Orientation", index);

            movementUpdate.Pitch = packet.ReadSingle("Pitch", index);
            movementUpdate.StepUpStartElevation = packet.ReadSingle("StepUpStartElevation", index);

            var int152 = packet.ReadInt32("Int152", index);
            packet.ReadInt32("Int168", index);

            for (var i = 0; i < int152; i++)
                packet.ReadPackedGuid128("RemoveForcesIDs", index, i);

            packet.ResetBitReader();

            moveInfo.Flags = (uint)packet.ReadBitsE<MovementFlag>("Movement Flags", 30, index);
            moveInfo.Flags2 = (uint)packet.ReadBitsE<MovementFlag2>("Extra Movement Flags", ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173) ? 16 : 15, index);

            var hasTransport = packet.ReadBit("Has Transport Data", index);
            var hasFall = packet.ReadBit("Has Fall Data", index);
            packet.ReadBit("HasSpline", index);
            packet.ReadBit("HeightChangeFailed", index);
            packet.ReadBit("RemoteTimeValid", index);

            if (hasTransport)
            {
                var transport = movementUpdate.Transport = new();
                transport.TransportGuid = packet.ReadPackedGuid128("Transport Guid", index);
                transport.Position = packet.ReadVector4("Transport Position", index);
                transport.Seat = (uint)packet.ReadSByte("Transport Seat", index);
                transport.MoveTime = (uint)packet.ReadInt32("Transport Time", index);

                packet.ResetBitReader();

                var hasPrevMoveTime = packet.ReadBit("HasPrevMoveTime", index);
                var hasVehicleRecID = packet.ReadBit("HasVehicleRecID", index);

                if (hasPrevMoveTime)
                    transport.PrevMoveTime = packet.ReadUInt32("PrevMoveTime", index);

                if (hasVehicleRecID)
                    transport.VehicleId = (int)packet.ReadUInt32("VehicleRecID", index);
            }

            if (hasFall)
            {
                movementUpdate.FallTime = packet.ReadUInt32("Fall Time", index);
                movementUpdate.JumpVelocity = packet.ReadSingle("JumpVelocity", index);

                packet.ResetBitReader();
                var bit20 = packet.ReadBit("Has Fall Direction", index);
                if (bit20)
                {
                    packet.ReadVector2("Fall", index);
                    packet.ReadSingle("Horizontal Speed", index);
                }
            }

            return moveInfo;
        }

        [Parser(Opcode.SMSG_DESTROY_ARENA_UNIT)]
        public static void HandleDestroyArenaUnit(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_MAP_OBJ_EVENTS)]
        public static void HandleMapObjEvents(Packet packet)
        {
            packet.ReadInt32("UniqueID");
            packet.ReadInt32("DataSize");

            var count = packet.ReadByte("Unk1");
            for (var i = 0; i < count; i++)
            {
                var byte20 = packet.ReadByte("Unk2", i);
                packet.ReadInt32(byte20 == 1 ? "Unk3" : "Unk4", i);
            }
        }

        [Parser(Opcode.SMSG_SET_ANIM_TIER)]
        public static void HandleSetAnimTier(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
            packet.ReadBits("Tier", 3);
        }

        [Parser(Opcode.CMSG_OBJECT_UPDATE_FAILED)]
        [Parser(Opcode.CMSG_OBJECT_UPDATE_RESCUED)]
        public static void HandleObjectUpdateOrRescued(Packet packet)
        {
            if (!ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_0_19702))
                packet.ReadPackedGuid128("ObjectGUID");
        }
    }
}

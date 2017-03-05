using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class UpdateHandler
    {
        [HasSniffData] // in ReadCreateObjectBlock
        [Parser(Opcode.SMSG_UPDATE_OBJECT)]
        public static void HandleUpdateObject(Packet packet)
        {
            var count = packet.Translator.ReadUInt32("NumObjUpdates");
            uint map = packet.Translator.ReadUInt16<MapId>("MapID");
            packet.Translator.ResetBitReader();
            var bit552 = packet.Translator.ReadBit("HasDestroyObjects");
            if (bit552)
            {
                packet.Translator.ReadInt16("Int0");
                var int8 = packet.Translator.ReadUInt32("DestroyObjectsCount");
                for (var i = 0; i < int8; i++)
                    packet.Translator.ReadPackedGuid128("Object GUID", i);
            }
            packet.Translator.ReadUInt32("Data size");

            for (var i = 0; i < count; i++)
            {
                var type = packet.Translator.ReadByte();
                var typeString = ((UpdateTypeCataclysm)type).ToString();

                packet.AddValue("UpdateType", typeString, i);
                switch (typeString)
                {
                    case "Values":
                        {
                            var guid = packet.Translator.ReadPackedGuid128("Object Guid", i);

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
                    case "CreateObject2": // Might != CreateObject1 on Cata
                        {
                            var guid = packet.Translator.ReadPackedGuid128("Object Guid", i);
                            ReadCreateObjectBlock(packet, guid, map, i);
                            break;
                        }
                }
            }
        }

        private static void ReadCreateObjectBlock(Packet packet, WowGuid guid, uint map, object index)
        {
            var objType = packet.Translator.ReadByteE<ObjectType>("Object Type", index);
            var moves = ReadMovementUpdateBlock(packet, guid, index);
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

        private static MovementInfo ReadMovementUpdateBlock(Packet packet, WowGuid guid, object index)
        {
            var moveInfo = new MovementInfo();

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("NoBirthAnim", index);
            packet.Translator.ReadBit("EnablePortals", index);
            packet.Translator.ReadBit("PlayHoverAnim", index);
            packet.Translator.ReadBit("IsSuppressingGreetings", index);

            var hasMovementUpdate = packet.Translator.ReadBit("HasMovementUpdate", index);
            var hasMovementTransport = packet.Translator.ReadBit("HasMovementTransport", index);
            var hasStationaryPosition = packet.Translator.ReadBit("Stationary", index);
            var hasCombatVictim = packet.Translator.ReadBit("HasCombatVictim", index);
            var hasServerTime = packet.Translator.ReadBit("HasServerTime", index);
            var hasVehicleCreate = packet.Translator.ReadBit("HasVehicleCreate", index);
            var hasAnimKitCreate = packet.Translator.ReadBit("HasAnimKitCreate", index);
            var hasRotation = packet.Translator.ReadBit("HasRotation", index);
            var hasAreaTrigger = packet.Translator.ReadBit("HasAreaTrigger", index);
            var hasGameObject = packet.Translator.ReadBit("HasGameObject", index);

            packet.Translator.ReadBit("ThisIsYou", index);
            packet.Translator.ReadBit("ReplaceActive", index);

            var sceneObjCreate = packet.Translator.ReadBit("SceneObjCreate", index);
            var scenePendingInstances = packet.Translator.ReadBit("ScenePendingInstances", index);

            var pauseTimesCount = packet.Translator.ReadUInt32("PauseTimesCount", index);

            if (hasMovementUpdate) // 392
            {
                moveInfo = ReadMovementStatusData(packet, index);

                moveInfo.WalkSpeed = packet.Translator.ReadSingle("WalkSpeed", index) / 2.5f;
                moveInfo.RunSpeed = packet.Translator.ReadSingle("RunSpeed", index) / 7.0f;
                packet.Translator.ReadSingle("RunBackSpeed", index);
                packet.Translator.ReadSingle("SwimSpeed", index);
                packet.Translator.ReadSingle("SwimBackSpeed", index);
                packet.Translator.ReadSingle("FlightSpeed", index);
                packet.Translator.ReadSingle("FlightBackSpeed", index);
                packet.Translator.ReadSingle("TurnRate", index);
                packet.Translator.ReadSingle("PitchRate", index);

                var movementForceCount = packet.Translator.ReadInt32("MovementForceCount", index);

                for (var i = 0; i < movementForceCount; ++i)
                {
                    packet.Translator.ReadPackedGuid128("Id", index);
                    packet.Translator.ReadVector3("Direction", index);
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_2_19802)) // correct?
                        packet.Translator.ReadVector3("TransportPosition", index);
                    packet.Translator.ReadInt32("TransportID", index);
                    packet.Translator.ReadSingle("Magnitude", index);
                    packet.Translator.ReadByte("Type", index);
                }

                packet.Translator.ResetBitReader();

                moveInfo.HasSplineData = packet.Translator.ReadBit("HasMovementSpline", index);

                if (moveInfo.HasSplineData)
                {
                    packet.Translator.ReadInt32("ID", index);
                    packet.Translator.ReadVector3("Destination", index);

                    packet.Translator.ResetBitReader();

                    var hasMovementSplineMove = packet.Translator.ReadBit("MovementSplineMove", index);
                    if (hasMovementSplineMove)
                    {
                        packet.Translator.ResetBitReader();

                        packet.Translator.ReadBitsE<SplineFlag434>("SplineFlags", ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173) ? 28 : 25, index);
                        var face = packet.Translator.ReadBits("Face", 2, index);

                        var hasJumpGravity = packet.Translator.ReadBit("HasJumpGravity", index);
                        var hasSpecialTime = packet.Translator.ReadBit("HasSpecialTime", index);

                        packet.Translator.ReadBitsE<SplineMode>("Mode", 2, index);

                        var hasSplineFilterKey = packet.Translator.ReadBit("HasSplineFilterKey", index);

                        packet.Translator.ReadUInt32("Elapsed", index);
                        packet.Translator.ReadUInt32("Duration", index);

                        packet.Translator.ReadSingle("DurationModifier", index);
                        packet.Translator.ReadSingle("NextDurationModifier", index);

                        var pointsCount = packet.Translator.ReadUInt32("PointsCount", index);

                        if (face == 3) // FaceDirection
                            packet.Translator.ReadSingle("FaceDirection", index);

                        if (face == 2) // FaceGUID
                            packet.Translator.ReadPackedGuid128("FaceGUID", index);

                        if (face == 1) // FaceSpot
                            packet.Translator.ReadVector3("FaceSpot", index);

                        if (hasJumpGravity)
                            packet.Translator.ReadSingle("JumpGravity", index);

                        if (hasSpecialTime)
                            packet.Translator.ReadInt32("SpecialTime", index);

                        if (hasSplineFilterKey)
                        {
                            var filterKeysCount = packet.Translator.ReadUInt32("FilterKeysCount", index);
                            for (var i = 0; i < filterKeysCount; ++i)
                            {
                                packet.Translator.ReadSingle("In", index, i);
                                packet.Translator.ReadSingle("Out", index, i);
                            }

                            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                                packet.Translator.ResetBitReader();

                            packet.Translator.ReadBits("FilterFlags", 2, index);
                        }

                        for (var i = 0; i < pointsCount; ++i)
                            packet.Translator.ReadVector3("Points", index, i);
                    }
                }
            }

            if (hasMovementTransport) // 456
            {
                moveInfo.TransportGuid = packet.Translator.ReadPackedGuid128("TransportGUID", index);
                moveInfo.TransportOffset = packet.Translator.ReadVector4("TransportPosition", index);
                var seat = packet.Translator.ReadByte("VehicleSeatIndex", index);
                packet.Translator.ReadUInt32("MoveTime", index);

                packet.Translator.ResetBitReader();

                var hasPrevMoveTime = packet.Translator.ReadBit("HasPrevMoveTime", index);
                var hasVehicleRecID = packet.Translator.ReadBit("HasVehicleRecID", index);

                if (hasPrevMoveTime)
                    packet.Translator.ReadUInt32("PrevMoveTime", index);

                if (hasVehicleRecID)
                    packet.Translator.ReadInt32("VehicleRecID", index);

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

            if (hasStationaryPosition) // 480
            {
                moveInfo.Position = packet.Translator.ReadVector3();
                moveInfo.Orientation = packet.Translator.ReadSingle();

                packet.AddValue("Stationary Position", moveInfo.Position, index);
                packet.AddValue("Stationary Orientation", moveInfo.Orientation, index);
            }

            if (hasCombatVictim) // 504
                packet.Translator.ReadPackedGuid128("CombatVictim Guid", index);

            if (hasServerTime) // 516
                packet.Translator.ReadUInt32("ServerTime", index);

            if (hasVehicleCreate) // 528
            {
                moveInfo.VehicleId = packet.Translator.ReadUInt32("RecID", index);
                packet.Translator.ReadSingle("InitialRawFacing", index);
            }

            if (hasAnimKitCreate) // 538
            {
                packet.Translator.ReadUInt16("AiID", index);
                packet.Translator.ReadUInt16("MovementID", index);
                packet.Translator.ReadUInt16("MeleeID", index);
            }

            if (hasRotation) // 552
                moveInfo.Rotation = packet.Translator.ReadPackedQuaternion("GameObject Rotation", index);

            if (hasAreaTrigger) // 772
            {
                // CliAreaTrigger
                packet.Translator.ReadInt32("ElapsedMs", index);

                packet.Translator.ReadVector3("RollPitchYaw1", index);

                packet.Translator.ResetBitReader();

                packet.Translator.ReadBit("HasAbsoluteOrientation", index);
                packet.Translator.ReadBit("HasDynamicShape", index);
                packet.Translator.ReadBit("HasAttached", index);
                packet.Translator.ReadBit("HasFaceMovementDir", index);
                packet.Translator.ReadBit("HasFollowsTerrain", index);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                    packet.Translator.ReadBit("Unk bit WoD62x", index);

                var hasTargetRollPitchYaw = packet.Translator.ReadBit("HasTargetRollPitchYaw", index);
                var hasScaleCurveID = packet.Translator.ReadBit("HasScaleCurveID", index);
                var hasMorphCurveID = packet.Translator.ReadBit("HasMorphCurveID", index);
                var hasFacingCurveID = packet.Translator.ReadBit("HasFacingCurveID", index);
                var hasMoveCurveID = packet.Translator.ReadBit("HasMoveCurveID", index);
                var hasAreaTriggerSphere = packet.Translator.ReadBit("HasAreaTriggerSphere", index);
                var hasAreaTriggerBox = packet.Translator.ReadBit("HasAreaTriggerBox", index);
                var hasAreaTriggerPolygon = packet.Translator.ReadBit("HasAreaTriggerPolygon", index);
                var hasAreaTriggerCylinder = packet.Translator.ReadBit("HasAreaTriggerCylinder", index);
                var hasAreaTriggerSpline = packet.Translator.ReadBit("HasAreaTriggerSpline", index);

                if (hasTargetRollPitchYaw)
                    packet.Translator.ReadVector3("TargetRollPitchYaw", index);

                if (hasScaleCurveID)
                    packet.Translator.ReadInt32("ScaleCurveID, index");

                if (hasMorphCurveID)
                    packet.Translator.ReadInt32("MorphCurveID", index);

                if (hasFacingCurveID)
                    packet.Translator.ReadInt32("FacingCurveID", index);

                if (hasMoveCurveID)
                    packet.Translator.ReadInt32("MoveCurveID", index);

                if (hasAreaTriggerSphere)
                {
                    packet.Translator.ReadSingle("Radius", index);
                    packet.Translator.ReadSingle("RadiusTarget", index);
                }

                if (hasAreaTriggerBox)
                {
                    packet.Translator.ReadVector3("Extents", index);
                    packet.Translator.ReadVector3("ExtentsTarget", index);
                }

                if (hasAreaTriggerPolygon)
                {
                    var verticesCount = packet.Translator.ReadInt32("VerticesCount", index);
                    var verticesTargetCount = packet.Translator.ReadInt32("VerticesTargetCount", index);
                    packet.Translator.ReadSingle("Height", index);
                    packet.Translator.ReadSingle("HeightTarget", index);

                    for (var i = 0; i < verticesCount; ++i)
                        packet.Translator.ReadVector2("Vertices", index, i);

                    for (var i = 0; i < verticesTargetCount; ++i)
                        packet.Translator.ReadVector2("VerticesTarget", index, i);
                }

                if (hasAreaTriggerCylinder)
                {
                    packet.Translator.ReadSingle("Radius", index);
                    packet.Translator.ReadSingle("RadiusTarget", index);
                    packet.Translator.ReadSingle("Height", index);
                    packet.Translator.ReadSingle("HeightTarget", index);
                    packet.Translator.ReadSingle("Float4", index);
                    packet.Translator.ReadSingle("Float5", index);
                }

                if (hasAreaTriggerSpline)
                    AreaTriggerHandler.ReadAreaTriggerSpline(packet, index);
            }

            if (hasGameObject) // 788
            {
                packet.Translator.ReadInt32("WorldEffectID", index);

                packet.Translator.ResetBitReader();

                var bit8 = packet.Translator.ReadBit("bit8", index);
                if (bit8)
                    packet.Translator.ReadInt32("Int1", index);
            }

            if (sceneObjCreate) // 1184
            {
                packet.Translator.ResetBitReader();

                var hasSceneLocalScriptData = packet.Translator.ReadBit("HasSceneLocalScriptData", index);
                var petBattleFullUpdate = packet.Translator.ReadBit("HasPetBattleFullUpdate", index);

                if (hasSceneLocalScriptData)
                {
                    packet.Translator.ResetBitReader();
                    var dataLength = packet.Translator.ReadBits(7);
                    packet.Translator.ReadWoWString("Data", dataLength, index);
                }

                if (petBattleFullUpdate)
                    BattlePetHandler.ReadPetBattleFullUpdate(packet, index);
            }

            if (scenePendingInstances) // 1208
            {
                var sceneInstanceIDs = packet.Translator.ReadInt32("SceneInstanceIDsCount");

                for (var i = 0; i < sceneInstanceIDs; ++i)
                    packet.Translator.ReadInt32("SceneInstanceIDs", index, i);
            }

            for (var i = 0; i < pauseTimesCount; ++i)
                packet.Translator.ReadInt32("PauseTimes", index, i);

            return moveInfo;
        }

        private static MovementInfo ReadMovementStatusData(Packet packet, object index)
        {
            var moveInfo = new MovementInfo();

            packet.Translator.ReadPackedGuid128("MoverGUID", index);

            packet.Translator.ReadUInt32("MoveIndex", index);
            moveInfo.Position = packet.Translator.ReadVector3("Position", index);
            moveInfo.Orientation = packet.Translator.ReadSingle("Orientation", index);

            packet.Translator.ReadSingle("Pitch", index);
            packet.Translator.ReadSingle("StepUpStartElevation", index);

            var int152 = packet.Translator.ReadInt32("Int152", index);
            packet.Translator.ReadInt32("Int168", index);

            for (var i = 0; i < int152; i++)
                packet.Translator.ReadPackedGuid128("RemoveForcesIDs", index, i);

            packet.Translator.ResetBitReader();

            moveInfo.Flags = (MovementFlag)packet.Translator.ReadBitsE<Enums.MovementFlag>("Movement Flags", 30, index);
            moveInfo.FlagsExtra = packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173) ? 16 : 15, index);

            var hasTransport = packet.Translator.ReadBit("Has Transport Data", index);
            var hasFall = packet.Translator.ReadBit("Has Fall Data", index);
            packet.Translator.ReadBit("HasSpline", index);
            packet.Translator.ReadBit("HeightChangeFailed", index);
            packet.Translator.ReadBit("RemoteTimeValid", index);

            if (hasTransport)
            {
                packet.Translator.ReadPackedGuid128("Transport Guid", index);
                packet.Translator.ReadVector4("Transport Position", index);
                packet.Translator.ReadSByte("Transport Seat", index);
                packet.Translator.ReadInt32("Transport Time", index);

                packet.Translator.ResetBitReader();

                var hasPrevMoveTime = packet.Translator.ReadBit("HasPrevMoveTime", index);
                var hasVehicleRecID = packet.Translator.ReadBit("HasVehicleRecID", index);

                if (hasPrevMoveTime)
                    packet.Translator.ReadUInt32("PrevMoveTime", index);

                if (hasVehicleRecID)
                    packet.Translator.ReadUInt32("VehicleRecID", index);
            }

            if (hasFall)
            {
                packet.Translator.ReadUInt32("Fall Time", index);
                packet.Translator.ReadSingle("JumpVelocity", index);

                packet.Translator.ResetBitReader();
                var bit20 = packet.Translator.ReadBit("Has Fall Direction", index);
                if (bit20)
                {
                    packet.Translator.ReadVector2("Fall", index);
                    packet.Translator.ReadSingle("Horizontal Speed", index);
                }
            }

            return moveInfo;
        }

        [Parser(Opcode.SMSG_DESTROY_ARENA_UNIT)]
        public static void HandleDestroyArenaUnit(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_MAP_OBJ_EVENTS)]
        public static void HandleMapObjEvents(Packet packet)
        {
            packet.Translator.ReadInt32("UniqueID");
            packet.Translator.ReadInt32("DataSize");

            var count = packet.Translator.ReadByte("Unk1");
            for (var i = 0; i < count; i++)
            {
                var byte20 = packet.Translator.ReadByte("Unk2", i);
                packet.Translator.ReadInt32(byte20 == 1 ? "Unk3" : "Unk4", i);
            }
        }

        [Parser(Opcode.SMSG_SET_ANIM_TIER)]
        public static void HandleSetAnimTier(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Unit");
            packet.Translator.ReadBits("Tier", 3);
        }

        [Parser(Opcode.CMSG_OBJECT_UPDATE_FAILED)]
        [Parser(Opcode.CMSG_OBJECT_UPDATE_RESCUED)]
        public static void HandleObjectUpdateOrRescued(Packet packet)
        {
            if (!ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_0_19702))
                packet.Translator.ReadPackedGuid128("ObjectGUID");
        }
    }
}

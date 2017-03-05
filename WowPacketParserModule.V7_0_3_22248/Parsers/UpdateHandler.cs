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
                case ObjectType.AreaTrigger:
                    obj = new SpellAreaTrigger();
                    break;
                default:
                    obj = new WoWObject();
                    break;
            }

            var moves = ReadMovementUpdateBlock(packet, guid, obj, index);
            var updates = ReadValuesUpdateBlock(packet, objType, index, true);

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

        private static MovementInfo ReadMovementUpdateBlock(Packet packet, WowGuid guid, WoWObject obj, object index)
        {
            var moveInfo = new MovementInfo();

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("NoBirthAnim", index);
            packet.Translator.ReadBit("EnablePortals", index);
            packet.Translator.ReadBit("PlayHoverAnim", index);

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
            var hasSmoothPhasing = packet.Translator.ReadBit("HasSmoothPhasing", index);

            packet.Translator.ReadBit("ThisIsYou", index);

            var sceneObjCreate = packet.Translator.ReadBit("SceneObjCreate", index);
            var playerCreateData = packet.Translator.ReadBit("HasPlayerCreateData", index);

            if (hasMovementUpdate)
            {
                packet.Translator.ResetBitReader();
                packet.Translator.ReadPackedGuid128("MoverGUID", index);

                packet.Translator.ReadUInt32("MoveTime", index);
                moveInfo.Position = packet.Translator.ReadVector3("Position", index);
                moveInfo.Orientation = packet.Translator.ReadSingle("Orientation", index);

                packet.Translator.ReadSingle("Pitch", index);
                packet.Translator.ReadSingle("StepUpStartElevation", index);

                var removeForcesIDsCount = packet.Translator.ReadInt32();
                packet.Translator.ReadInt32("MoveIndex", index);

                for (var i = 0; i < removeForcesIDsCount; i++)
                    packet.Translator.ReadPackedGuid128("RemoveForcesIDs", index, i);

                moveInfo.Flags = (MovementFlag)packet.Translator.ReadBitsE<WowPacketParserModule.V6_0_2_19033.Enums.MovementFlag>("Movement Flags", 30, index);
                moveInfo.FlagsExtra = packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 18, index);

                var hasTransport = packet.Translator.ReadBit("Has Transport Data", index);
                var hasFall = packet.Translator.ReadBit("Has Fall Data", index);
                packet.Translator.ReadBit("HasSpline", index);
                packet.Translator.ReadBit("HeightChangeFailed", index);
                packet.Translator.ReadBit("RemoteTimeValid", index);

                if (hasTransport)
                {
                    packet.Translator.ResetBitReader();
                    moveInfo.TransportGuid = packet.Translator.ReadPackedGuid128("Transport Guid", index);
                    moveInfo.TransportOffset = packet.Translator.ReadVector4("Transport Position", index);
                    packet.Translator.ReadSByte("Transport Seat", index);
                    packet.Translator.ReadInt32("Transport Time", index);

                    var hasPrevMoveTime = packet.Translator.ReadBit("HasPrevMoveTime", index);
                    var hasVehicleRecID = packet.Translator.ReadBit("HasVehicleRecID", index);

                    if (hasPrevMoveTime)
                        packet.Translator.ReadUInt32("PrevMoveTime", index);

                    if (hasVehicleRecID)
                        packet.Translator.ReadUInt32("VehicleRecID", index);
                }

                if (hasFall)
                {
                    packet.Translator.ResetBitReader();
                    packet.Translator.ReadUInt32("Fall Time", index);
                    packet.Translator.ReadSingle("JumpVelocity", index);

                    var hasFallDirection = packet.Translator.ReadBit("Has Fall Direction", index);
                    if (hasFallDirection)
                    {
                        packet.Translator.ReadVector2("Fall", index);
                        packet.Translator.ReadSingle("Horizontal Speed", index);
                    }
                }

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

                packet.Translator.ResetBitReader();

                moveInfo.HasSplineData = packet.Translator.ReadBit("HasMovementSpline", index);

                for (var i = 0; i < movementForceCount; ++i)
                {
                    packet.Translator.ResetBitReader();
                    packet.Translator.ReadPackedGuid128("Id", index);
                    packet.Translator.ReadVector3("Origin", index);
                    packet.Translator.ReadVector3("Direction", index);
                    packet.Translator.ReadInt32("TransportID", index);
                    packet.Translator.ReadSingle("Magnitude", index);
                    packet.Translator.ReadBits("Type", 2, index);
                }

                if (moveInfo.HasSplineData)
                {
                    packet.Translator.ResetBitReader();
                    packet.Translator.ReadInt32("ID", index);
                    packet.Translator.ReadVector3("Destination", index);

                    var hasMovementSplineMove = packet.Translator.ReadBit("MovementSplineMove", index);
                    if (hasMovementSplineMove)
                    {
                        packet.Translator.ResetBitReader();

                        packet.Translator.ReadUInt32E<SplineFlag434>("SplineFlags", index);
                        packet.Translator.ReadUInt32("Elapsed", index);
                        packet.Translator.ReadUInt32("Duration", index);
                        packet.Translator.ReadSingle("DurationModifier", index);
                        packet.Translator.ReadSingle("NextDurationModifier", index);

                        var face = packet.Translator.ReadBits("Face", 2, index);

                        var hasJumpGravity = packet.Translator.ReadBit("HasJumpGravity", index);
                        var hasSpecialTime = packet.Translator.ReadBit("HasSpecialTime", index);

                        var pointsCount = packet.Translator.ReadBits("PointsCount", 16, index);

                        packet.Translator.ReadBitsE<SplineMode>("Mode", 2, index);

                        var hasSplineFilterKey = packet.Translator.ReadBit("HasSplineFilterKey", index);
                        var hasSpellEffectExtraData = packet.Translator.ReadBit("HasSpellEffectExtraData", index);

                        if (hasSplineFilterKey)
                        {
                            packet.Translator.ResetBitReader();
                            var filterKeysCount = packet.Translator.ReadUInt32("FilterKeysCount", index);
                            for (var i = 0; i < filterKeysCount; ++i)
                            {
                                packet.Translator.ReadSingle("In", index, i);
                                packet.Translator.ReadSingle("Out", index, i);
                            }

                            packet.Translator.ReadBits("FilterFlags", 2, index);
                        }

                        if (face == 3)
                            packet.Translator.ReadSingle("FaceDirection", index);

                        if (face == 2)
                            packet.Translator.ReadPackedGuid128("FaceGUID", index);

                        if (face == 1)
                            packet.Translator.ReadVector3("FaceSpot", index);

                        if (hasJumpGravity)
                            packet.Translator.ReadSingle("JumpGravity", index);

                        if (hasSpecialTime)
                            packet.Translator.ReadInt32("SpecialTime", index);

                        for (var i = 0; i < pointsCount; ++i)
                            packet.Translator.ReadVector3("Points", index, i);

                        if (hasSpellEffectExtraData)
                        {
                            packet.Translator.ReadPackedGuid128("TargetGUID", index);
                            packet.Translator.ReadUInt32("SpellVisualID", index);
                            packet.Translator.ReadUInt32("ProgressCurveID", index);
                            packet.Translator.ReadUInt32("ParabolicCurveID", index);
                        }
                    }
                }
            }

            var pauseTimesCount = packet.Translator.ReadUInt32("PauseTimesCount", index);

            if (hasStationaryPosition)
            {
                moveInfo.Position = packet.Translator.ReadVector3();
                moveInfo.Orientation = packet.Translator.ReadSingle();

                packet.AddValue("Stationary Position", moveInfo.Position, index);
                packet.AddValue("Stationary Orientation", moveInfo.Orientation, index);
            }

            if (hasCombatVictim)
                packet.Translator.ReadPackedGuid128("CombatVictim Guid", index);

            if (hasServerTime)
                packet.Translator.ReadUInt32("ServerTime", index);

            if (hasVehicleCreate)
            {
                moveInfo.VehicleId = packet.Translator.ReadUInt32("RecID", index);
                packet.Translator.ReadSingle("InitialRawFacing", index);
            }

            if (hasAnimKitCreate)
            {
                packet.Translator.ReadUInt16("AiID", index);
                packet.Translator.ReadUInt16("MovementID", index);
                packet.Translator.ReadUInt16("MeleeID", index);
            }

            if (hasRotation)
                moveInfo.Rotation = packet.Translator.ReadPackedQuaternion("GameObject Rotation", index);

            for (var i = 0; i < pauseTimesCount; ++i)
                packet.Translator.ReadInt32("PauseTimes", index, i);

            if (hasMovementTransport)
            {
                packet.Translator.ResetBitReader();
                moveInfo.TransportGuid = packet.Translator.ReadPackedGuid128("TransportGUID", index);
                moveInfo.TransportOffset = packet.Translator.ReadVector4("TransportPosition", index);
                var seat = packet.Translator.ReadByte("VehicleSeatIndex", index);
                packet.Translator.ReadUInt32("MoveTime", index);

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

            if (hasAreaTrigger && obj is SpellAreaTrigger)
            {
                AreaTriggerTemplate areaTriggerTemplate = new AreaTriggerTemplate
                {
                    Id = guid.GetEntry()
                };

                SpellAreaTrigger spellAreaTrigger = (SpellAreaTrigger)obj;
                spellAreaTrigger.AreaTriggerId = guid.GetEntry();

                packet.Translator.ResetBitReader();

                // CliAreaTrigger
                packet.Translator.ReadInt32("ElapsedMs", index);

                packet.Translator.ReadVector3("RollPitchYaw1", index);

                areaTriggerTemplate.Flags   = 0;

                if (packet.Translator.ReadBit("HasAbsoluteOrientation", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasAbsoluteOrientation;

                if (packet.Translator.ReadBit("HasDynamicShape", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasDynamicShape;

                if (packet.Translator.ReadBit("HasAttached", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasAttached;

                if (packet.Translator.ReadBit("HasFaceMovementDir", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.FaceMovementDirection;

                if (packet.Translator.ReadBit("HasFollowsTerrain", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.FollowsTerrain;

                if (packet.Translator.ReadBit("Unk bit WoD62x", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.Unk1;

                if (packet.Translator.ReadBit("HasTargetRollPitchYaw", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasTargetRollPitchYaw;

                bool hasScaleCurveID = packet.Translator.ReadBit("HasScaleCurveID", index);
                bool hasMorphCurveID = packet.Translator.ReadBit("HasMorphCurveID", index);
                bool hasFacingCurveID = packet.Translator.ReadBit("HasFacingCurveID", index);
                bool hasMoveCurveID = packet.Translator.ReadBit("HasMoveCurveID", index);

                if (packet.Translator.ReadBit("unkbit4C", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.Unk2;

                if (packet.Translator.ReadBit("unkbit50", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.Unk3;

                if (packet.Translator.ReadBit("unkbit58", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.Unk4;

                if (packet.Translator.ReadBit("HasAreaTriggerSphere", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Sphere;

                if (packet.Translator.ReadBit("HasAreaTriggerBox", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Box;

                if (packet.Translator.ReadBit("HasAreaTriggerPolygon", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Polygon;

                if (packet.Translator.ReadBit("HasAreaTriggerCylinder", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Cylinder;

                bool hasAreaTriggerSpline = packet.Translator.ReadBit("HasAreaTriggerSpline", index);

                if (packet.Translator.ReadBit("HasAreaTriggerUnkType", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.Unk5;

                if ((areaTriggerTemplate.Flags & (uint)AreaTriggerFlags.Unk3) != 0)
                    packet.Translator.ReadBit();

                if (hasAreaTriggerSpline)
                    AreaTriggerHandler.ReadAreaTriggerSpline(packet, index);

                if ((areaTriggerTemplate.Flags & (uint)AreaTriggerFlags.HasTargetRollPitchYaw) != 0)
                    packet.Translator.ReadVector3("TargetRollPitchYaw", index);

                if (hasScaleCurveID)
                    spellAreaTrigger.ScaleCurveId = packet.Translator.ReadInt32("ScaleCurveID", index);

                if (hasMorphCurveID)
                    spellAreaTrigger.MorphCurveId = packet.Translator.ReadInt32("MorphCurveID", index);

                if (hasFacingCurveID)
                    spellAreaTrigger.FacingCurveId = packet.Translator.ReadInt32("FacingCurveID", index);

                if (hasMoveCurveID)
                    spellAreaTrigger.MoveCurveId = packet.Translator.ReadInt32("MoveCurveID", index);

                if ((areaTriggerTemplate.Flags & (int)AreaTriggerFlags.Unk2) != 0)
                    packet.Translator.ReadInt32();

                if ((areaTriggerTemplate.Flags & (int)AreaTriggerFlags.Unk4) != 0)
                    packet.Translator.ReadUInt32();

                if (areaTriggerTemplate.Type == (byte)AreaTriggerType.Sphere)
                {
                    areaTriggerTemplate.Data[0] = packet.Translator.ReadSingle("Radius", index);
                    areaTriggerTemplate.Data[1] = packet.Translator.ReadSingle("RadiusTarget", index);
                }

                if (areaTriggerTemplate.Type == (byte)AreaTriggerType.Box)
                {
                    Vector3 Extents = packet.Translator.ReadVector3("Extents", index);
                    Vector3 ExtentsTarget = packet.Translator.ReadVector3("ExtentsTarget", index);

                    areaTriggerTemplate.Data[0] = Extents.X;
                    areaTriggerTemplate.Data[1] = Extents.Y;
                    areaTriggerTemplate.Data[2] = Extents.Z;

                    areaTriggerTemplate.Data[3] = ExtentsTarget.X;
                    areaTriggerTemplate.Data[4] = ExtentsTarget.Y;
                    areaTriggerTemplate.Data[5] = ExtentsTarget.Z;
                }

                if (areaTriggerTemplate.Type == (byte)AreaTriggerType.Polygon)
                {
                    var verticesCount = packet.Translator.ReadInt32("VerticesCount", index);
                    var verticesTargetCount = packet.Translator.ReadInt32("VerticesTargetCount", index);

                    List<AreaTriggerTemplateVertices> verticesList = new List<AreaTriggerTemplateVertices>();

                    areaTriggerTemplate.Data[0] = packet.Translator.ReadSingle("Height", index);
                    areaTriggerTemplate.Data[1] = packet.Translator.ReadSingle("HeightTarget", index);

                    for (uint i = 0; i < verticesCount; ++i)
                    {
                        AreaTriggerTemplateVertices areaTriggerTemplateVertices = new AreaTriggerTemplateVertices
                        {
                            AreaTriggerId = guid.GetEntry(),
                            Idx = i
                        };

                        Vector2 vertices = packet.Translator.ReadVector2("Vertices", index, i);

                        areaTriggerTemplateVertices.VerticeX = vertices.X;
                        areaTriggerTemplateVertices.VerticeY = vertices.Y;

                        verticesList.Add(areaTriggerTemplateVertices);
                    }

                    for (var i = 0; i < verticesTargetCount; ++i)
                    {
                        Vector2 verticesTarget = packet.Translator.ReadVector2("VerticesTarget", index, i);

                        verticesList[i].VerticeTargetX = verticesTarget.X;
                        verticesList[i].VerticeTargetY = verticesTarget.Y;
                    }

                    foreach (AreaTriggerTemplateVertices vertice in verticesList)
                        Storage.AreaTriggerTemplatesVertices.Add(vertice);
                }

                if (areaTriggerTemplate.Type == (byte)AreaTriggerType.Cylinder)
                {
                    areaTriggerTemplate.Data[0] = packet.Translator.ReadSingle("Radius", index);
                    areaTriggerTemplate.Data[1] = packet.Translator.ReadSingle("RadiusTarget", index);
                    areaTriggerTemplate.Data[2] = packet.Translator.ReadSingle("Height", index);
                    areaTriggerTemplate.Data[3] = packet.Translator.ReadSingle("HeightTarget", index);
                    areaTriggerTemplate.Data[4] = packet.Translator.ReadSingle("LocationZOffset", index);
                    areaTriggerTemplate.Data[5] = packet.Translator.ReadSingle("LocationZOffsetTarget", index);
                }

                if ((areaTriggerTemplate.Flags & (uint)AreaTriggerFlags.Unk5) != 0)
                {
                    packet.Translator.ResetBitReader();
                    var unk1 = packet.Translator.ReadBit("AreaTriggerUnk1");
                    var hasCenter = packet.Translator.ReadBit("HasCenter", index);
                    packet.Translator.ReadBit("Unk bit 703 1", index);
                    packet.Translator.ReadBit("Unk bit 703 2", index);

                    packet.Translator.ReadUInt32();
                    packet.Translator.ReadInt32();
                    packet.Translator.ReadUInt32();
                    packet.Translator.ReadSingle("Radius", index);
                    packet.Translator.ReadSingle("BlendFromRadius", index);
                    packet.Translator.ReadSingle("InitialAngel", index);
                    packet.Translator.ReadSingle("ZOffset", index);

                    if (unk1)
                        packet.Translator.ReadPackedGuid128("AreaTriggerUnkGUID", index);

                    if (hasCenter)
                        packet.Translator.ReadVector3("Center", index);
                }

                Storage.AreaTriggerTemplates.Add(areaTriggerTemplate);
            }

            if (hasGameObject)
            {
                packet.Translator.ResetBitReader();
                packet.Translator.ReadInt32("WorldEffectID", index);

                var bit8 = packet.Translator.ReadBit("bit8", index);
                if (bit8)
                    packet.Translator.ReadInt32("Int1", index);
            }

            if (hasSmoothPhasing)
            {
                packet.Translator.ResetBitReader();
                packet.Translator.ReadBit("ReplaceActive", index);
                var replaceObject = packet.Translator.ReadBit();
                if (replaceObject)
                    packet.Translator.ReadPackedGuid128("ReplaceObject", index);
            }

            if (sceneObjCreate)
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
                    V6_0_2_19033.Parsers.BattlePetHandler.ReadPetBattleFullUpdate(packet, index);
            }

            if (playerCreateData)
            {
                packet.Translator.ResetBitReader();
                var hasSceneInstanceIDs = packet.Translator.ReadBit("ScenePendingInstances", index);
                var hasRuneState = packet.Translator.ReadBit("Runes", index);

                if (hasSceneInstanceIDs)
                {
                    var sceneInstanceIDs = packet.Translator.ReadInt32("SceneInstanceIDsCount");
                    for (var i = 0; i < sceneInstanceIDs; ++i)
                        packet.Translator.ReadInt32("SceneInstanceIDs", index, i);
                }

                if (hasRuneState)
                {
                    packet.Translator.ReadByte("RechargingRuneMask", index);
                    packet.Translator.ReadByte("UsableRuneMask", index);
                    var runeCount = packet.Translator.ReadUInt32();
                    for (var i = 0; i < runeCount; ++i)
                        packet.Translator.ReadByte("RuneCooldown", index, i);
                }
            }

            return moveInfo;
        }
        private static Dictionary<int, UpdateField> ReadValuesUpdateBlock(Packet packet, ObjectType type, object index, bool isCreating)
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

                    var flag = packet.Translator.ReadUInt16();
                    var cnt = flag & 0x7FFF;
                    if ((flag & 0x8000) != 0)
                        packet.Translator.ReadUInt32(key + " Size", index);

                    var vals = new int[cnt];
                    for (var j = 0; j < cnt; ++j)
                        vals[j] = packet.Translator.ReadInt32();

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
    }
}

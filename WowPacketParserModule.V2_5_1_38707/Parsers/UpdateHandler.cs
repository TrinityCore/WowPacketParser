using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.PacketStructures;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Store.Objects.UpdateFields;
using CoreFields = WowPacketParser.Enums.Version;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using MovementFlag = WowPacketParser.Enums.v4.MovementFlag;
using MovementFlag2 = WowPacketParser.Enums.v7.MovementFlag2;
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
                        var updateValues = new UpdateValues();
                        if (ClientVersion.AddedInVersion(ClientVersionBuild.V2_5_4_42695))
                        {
                            var updatefieldSize = packet.ReadUInt32();
                            var handler = CoreFields.UpdateFields.GetHandler();
                            updateValues.Fields = new();
                            using (var fieldsData = new Packet(packet.ReadBytes((int)updatefieldSize), packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName))
                            {
                                WoWObject obj;
                                Storage.Objects.TryGetValue(guid, out obj);

                                var updateTypeFlag = fieldsData.ReadUInt32();
                                if ((updateTypeFlag & 0x0001) != 0)
                                {
                                    var data = handler.ReadUpdateObjectData(fieldsData, i);
                                    if (obj is { ObjectData: IMutableObjectData mut })
                                        mut.UpdateData(data);
                                    else if (obj != null)
                                        obj.ObjectData = data;

                                    updateValues.Fields.UpdateData(data);
                                }
                                if ((updateTypeFlag & 0x0002) != 0)
                                    handler.ReadUpdateItemData(fieldsData, i);
                                if ((updateTypeFlag & 0x0004) != 0)
                                    handler.ReadUpdateContainerData(fieldsData, i);
                                if ((updateTypeFlag & 0x0008) != 0)
                                    handler.ReadUpdateAzeriteEmpoweredItemData(fieldsData, i);
                                if ((updateTypeFlag & 0x0010) != 0)
                                    handler.ReadUpdateAzeriteItemData(fieldsData, i);
                                if ((updateTypeFlag & 0x0020) != 0)
                                {
                                    var unit = obj as Unit;
                                    var data = handler.ReadUpdateUnitData(fieldsData, i);
                                    if (unit is { UnitData: IMutableUnitData mut })
                                        mut.UpdateData(data);
                                    else if (unit != null)
                                        unit.UnitData = data;

                                    updateValues.Fields.UpdateData(data);
                                }
                                if ((updateTypeFlag & 0x0040) != 0)
                                    handler.ReadUpdatePlayerData(fieldsData, i);
                                if ((updateTypeFlag & 0x0080) != 0)
                                    handler.ReadUpdateActivePlayerData(fieldsData, i);
                                if ((updateTypeFlag & 0x0100) != 0)
                                {
                                    var go = obj as GameObject;
                                    var data = handler.ReadUpdateGameObjectData(fieldsData, i);
                                    if (go is { GameObjectData: IMutableGameObjectData mut })
                                        mut.UpdateData(data);
                                    else if (go != null)
                                        go.GameObjectData = data;

                                    updateValues.Fields.UpdateData(data);
                                }
                                if ((updateTypeFlag & 0x0200) != 0)
                                    handler.ReadUpdateDynamicObjectData(fieldsData, i);
                                if ((updateTypeFlag & 0x0400) != 0)
                                    handler.ReadUpdateCorpseData(fieldsData, i);
                                if ((updateTypeFlag & 0x0800) != 0)
                                    handler.ReadUpdateAreaTriggerData(fieldsData, i);
                                if ((updateTypeFlag & 0x1000) != 0)
                                    handler.ReadUpdateSceneObjectData(fieldsData, i);
                                if ((updateTypeFlag & 0x2000) != 0)
                                {
                                    var conversation = obj as ConversationTemplate;
                                    var data = handler.ReadUpdateConversationData(fieldsData, i);
                                    if (conversation is { ConversationData: IMutableConversationData mut })
                                        mut.UpdateData(data);
                                    else if (conversation != null)
                                        conversation.ConversationData = data;
                                }
                            }
                        }
                        else
                        {
                            updateValues.Legacy = new();
                            CoreParsers.UpdateHandler.ReadValuesUpdateBlock(packet, updateValues.Legacy, guid, i);
                        }
                        updateObject.Updated.Add(new UpdateObject{Guid = guid, Values = updateValues, TextStartOffset = partWriter.StartOffset, TextLength = partWriter.Length, Text = partWriter.Text});
                        break;
                    }
                    case UpdateTypeCataclysm.CreateObject1:
                    case UpdateTypeCataclysm.CreateObject2:
                    {
                        var guid = packet.ReadPackedGuid128("Object Guid", i);
                        var createType = type.ToCreateObjectType();
                        var createObject = new CreateObject() { Guid = guid, Values = new(){}, CreateType = createType };
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
            ObjectType objType;
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V2_5_4_42695))
            {
                objType = ObjectTypeConverter.Convert(packet.ReadByteE<ObjectTypeBCC>("Object Type", index));
                packet.ReadInt32("HeirFlags", index);
            }
            else
                objType = ObjectTypeConverter.Convert(packet.ReadByteE<ObjectType801>("Object Type", index));

            WoWObject obj = CoreParsers.UpdateHandler.CreateObject(objType, guid, map);

            obj.CreateType = createType;
            obj.Movement = ReadMovementUpdateBlock(packet, guid, obj, index);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V2_5_4_42695))
            {
                createObject.Values.Fields = new();
                var updatefieldSize = packet.ReadUInt32();
                using (var fieldsData = new Packet(packet.ReadBytes((int)updatefieldSize), packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName))
                {
                    var flags = fieldsData.ReadByteE<UpdateFieldFlag>("FieldFlags", index);
                    var handler = CoreFields.UpdateFields.GetHandler();
                    obj.ObjectData = handler.ReadCreateObjectData(fieldsData, flags, index);
                    createObject.Values.Fields.UpdateData(obj.ObjectData);
                    switch (objType)
                    {
                        case ObjectType.Item:
                            handler.ReadCreateItemData(fieldsData, flags, index);
                            break;
                        case ObjectType.Container:
                            handler.ReadCreateItemData(fieldsData, flags, index);
                            handler.ReadCreateContainerData(fieldsData, flags, index);
                            break;
                        case ObjectType.AzeriteEmpoweredItem:
                            handler.ReadCreateItemData(fieldsData, flags, index);
                            handler.ReadCreateAzeriteEmpoweredItemData(fieldsData, flags, index);
                            break;
                        case ObjectType.AzeriteItem:
                            handler.ReadCreateItemData(fieldsData, flags, index);
                            handler.ReadCreateAzeriteItemData(fieldsData, flags, index);
                            break;
                        case ObjectType.Unit:
                        {
                            var data = (obj as Unit).UnitData = handler.ReadCreateUnitData(fieldsData, flags, index);
                            createObject.Values.Fields.UpdateData(data);
                            break;
                        }
                        case ObjectType.Player:
                            handler.ReadCreateUnitData(fieldsData, flags, index);
                            handler.ReadCreatePlayerData(fieldsData, flags, index);
                            break;
                        case ObjectType.ActivePlayer:
                            handler.ReadCreateUnitData(fieldsData, flags, index);
                            handler.ReadCreatePlayerData(fieldsData, flags, index);
                            handler.ReadCreateActivePlayerData(fieldsData, flags, index);
                            break;
                        case ObjectType.GameObject:
                        {
                            var data = (obj as GameObject).GameObjectData = handler.ReadCreateGameObjectData(fieldsData, flags, index);
                            createObject.Values.Fields.UpdateData(data);
                            break;
                        }
                        case ObjectType.DynamicObject:
                            handler.ReadCreateDynamicObjectData(fieldsData, flags, index);
                            break;
                        case ObjectType.Corpse:
                            handler.ReadCreateCorpseData(fieldsData, flags, index);
                            break;
                        case ObjectType.AreaTrigger:
                            (obj as AreaTriggerCreateProperties).AreaTriggerData = handler.ReadCreateAreaTriggerData(fieldsData, flags, index);
                            break;
                        case ObjectType.SceneObject:
                            (obj as SceneObject).SceneObjectData = handler.ReadCreateSceneObjectData(fieldsData, flags, index);
                            break;
                        case ObjectType.Conversation:
                            (obj as ConversationTemplate).ConversationData = handler.ReadCreateConversationData(fieldsData, flags, index);
                            break;
                    }
                }
            }
            else
            {
                createObject.Values.Legacy = new();
                obj.UpdateFields = CoreParsers.UpdateHandler.ReadValuesUpdateBlockOnCreate(packet, createObject.Values.Legacy, objType, index);
                obj.DynamicUpdateFields = CoreParsers.UpdateHandler.ReadDynamicValuesUpdateBlockOnCreate(packet, objType, index);
            }

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

                if (ClientVersion.AddedInVersion(ClientBranch.Classic, ClientVersionBuild.V1_14_1_40666) ||
                    ClientVersion.AddedInVersion(ClientBranch.TBC, ClientVersionBuild.V2_5_3_41812) ||
                    ClientVersion.AddedInVersion(ClientBranch.WotLK, ClientVersionBuild.V3_4_0_45166))
                {
                    moveInfo.Flags = (uint)packet.ReadUInt32E<MovementFlag>("Movement Flags", index);
                    moveInfo.Flags2 = (uint)packet.ReadUInt32E<MovementFlag2>("Movement Flags 2", index);
                    moveInfo.Flags3 = (uint)packet.ReadUInt32E<MovementFlag3>("Movement Flags 3", index);
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

                if (ClientVersion.RemovedInVersion(ClientBranch.Classic, ClientVersionBuild.V1_14_1_40666) ||
                    ClientVersion.RemovedInVersion(ClientBranch.TBC, ClientVersionBuild.V2_5_3_41812) ||
                    ClientVersion.RemovedInVersion(ClientBranch.WotLK, ClientVersionBuild.V3_4_0_45166))
                {
                    moveInfo.Flags = (uint)packet.ReadBitsE<MovementFlag>("Movement Flags", 30, index);
                    moveInfo.Flags2 = (uint)packet.ReadBitsE<MovementFlag2>("Extra Movement Flags", 18, index);
                }

                var hasStandingOnGameObjectGUID = false;
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51505))
                    hasStandingOnGameObjectGUID = packet.ReadBit("HasStandingOnGameObjectGUID", index);

                var hasTransport = packet.ReadBit("Has Transport Data", index);
                var hasFall = packet.ReadBit("Has Fall Data", index);
                packet.ReadBit("HasSpline", index);
                packet.ReadBit("HeightChangeFailed", index);
                packet.ReadBit("RemoteTimeValid", index);
                var hasInertia = (ClientVersion.AddedInVersion(ClientBranch.Classic, ClientVersionBuild.V1_14_1_40666) ||
                                  ClientVersion.AddedInVersion(ClientBranch.TBC, ClientVersionBuild.V2_5_3_41812) ||
                                  ClientVersion.AddedInVersion(ClientBranch.WotLK, ClientVersionBuild.V3_4_0_45166)) &&
                                  packet.ReadBit("Has Inertia", index);

                var hasAdvFlying = ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014) && packet.ReadBit("HasAdvFlying", index);

                if (hasTransport)
                    V8_0_1_27101.Parsers.UpdateHandler.ReadTransportData(moveInfo, guid, packet, index);

                if (hasStandingOnGameObjectGUID)
                    packet.ReadPackedGuid128("StandingOnGameObjectGUID", index);

                if (hasInertia)
                {
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                        packet.ReadInt32("ID", "Inertia");
                    else
                        packet.ReadPackedGuid128("GUID", index, "Inertia");
                    packet.ReadVector3("Force", index, "Inertia");
                    packet.ReadUInt32("Lifetime", index, "Inertia");
                }

                if (hasAdvFlying)
                {
                    packet.ReadSingle("ForwardVelocity", index, "AdvFlying");
                    packet.ReadSingle("UpVelocity", index, "AdvFlying");
                }

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

                packet.ReadSingle("MovementForcesModMagnitude", index);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47014))
                {
                    packet.ReadSingle("AdvFlyingAirFriction", index);
                    packet.ReadSingle("AdvFlyingMaxVel", index);
                    packet.ReadSingle("AdvFlyingLiftCoefficient", index);
                    packet.ReadSingle("AdvFlyingDoubleJumpVelMod", index);
                    packet.ReadSingle("AdvFlyingGlideStartMinHeight", index);
                    packet.ReadSingle("AdvFlyingAddImpulseMaxSpeed", index);
                    packet.ReadSingle("AdvFlyingMinBankingRate", index);
                    packet.ReadSingle("AdvFlyingMaxBankingRate", index);
                    packet.ReadSingle("AdvFlyingMinPitchingRateDown", index);
                    packet.ReadSingle("AdvFlyingMaxPitchingRateDown", index);
                    packet.ReadSingle("AdvFlyingMinPitchingRateUp", index);
                    packet.ReadSingle("AdvFlyingMaxPitchingRateUp", index);
                    packet.ReadSingle("AdvFlyingMinTurnVelocityThreshold", index);
                    packet.ReadSingle("AdvFlyingMaxTurnVelocityThreshold", index);
                    packet.ReadSingle("AdvFlyingSurfaceFriction", index);
                    packet.ReadSingle("AdvFlyingOverMaxDeceleration", index);
                    packet.ReadSingle("AdvFlyingLaunchSpeedCoefficient", index);
                }

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
                        var hasUnknown901 = false;
                        if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_4_3_51505))
                            hasUnknown901 = packet.ReadBit("Unknown901", index);

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
                moveInfo.Transport = new MovementInfo.TransportInfo();
                packet.ResetBitReader();
                moveInfo.Transport.Guid = packet.ReadPackedGuid128("TransportGUID", index);
                moveInfo.Transport.Offset = packet.ReadVector4("TransportPosition", index);
                sbyte seat;
                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V2_5_3_41812))
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
                bool hasAnimProgress = false;

                if (packet.ReadBit("HasAnimID", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerCreatePropertiesFlags.HasAnimId;

                if (packet.ReadBit("HasAnimKitID", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerCreatePropertiesFlags.HasAnimKitId;

                if (packet.ReadBit("unkbit50", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerCreatePropertiesFlags.Unk3;

                hasAnimProgress = packet.ReadBit("HasAnimProgress", index);

                if (packet.ReadBit("HasAreaTriggerSphere", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Sphere;

                if (packet.ReadBit("HasAreaTriggerBox", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Box;

                if (packet.ReadBit("HasAreaTriggerPolygon", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Polygon;

                if (packet.ReadBit("HasAreaTriggerCylinder", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Cylinder;

                // no idea when added
                if (packet.ReadBit("HasAreaTriggerDisk", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Disk;

                // no idea when added
                if (packet.ReadBit("HasAreaTriggerBoundedPlane", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.BoundedPlane;

                bool hasAreaTriggerSpline = packet.ReadBit("HasAreaTriggerSpline", index);

                if (packet.ReadBit("HasAreaTriggerOrbit", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerCreatePropertiesFlags.HasOrbit;

                // no idea when added
                if (packet.ReadBit("HasAreaTriggerMovementScript", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerCreatePropertiesFlags.HasMovementScript;

                if ((areaTriggerTemplate.Flags & (uint)AreaTriggerCreatePropertiesFlags.Unk3) != 0)
                    packet.ReadBit();

                if (hasAreaTriggerSpline)
                    V7_0_3_22248.Parsers.AreaTriggerHandler.ReadAreaTriggerSpline(spellAreaTrigger, packet, index);

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

                if (areaTriggerTemplate.Type == (byte)AreaTriggerType.Disk)
                {
                    areaTriggerTemplate.Data[0] = packet.ReadSingle("InnerRadius", index);
                    areaTriggerTemplate.Data[1] = packet.ReadSingle("InnerRadiusTarget", index);
                    areaTriggerTemplate.Data[2] = packet.ReadSingle("OuterRadius", index);
                    areaTriggerTemplate.Data[3] = packet.ReadSingle("OuterRadiusTarget", index);
                    areaTriggerTemplate.Data[4] = packet.ReadSingle("Height", index);
                    areaTriggerTemplate.Data[5] = packet.ReadSingle("HeightTarget", index);
                    areaTriggerTemplate.Data[6] = packet.ReadSingle("LocationZOffset", index);
                    areaTriggerTemplate.Data[7] = packet.ReadSingle("LocationZOffsetTarget", index);
                }

                if (areaTriggerTemplate.Type == (byte)AreaTriggerType.BoundedPlane)
                {
                    Vector2 extents = packet.ReadVector2("Extents", index);
                    Vector2 extentsTarget = packet.ReadVector2("ExtentsTarget", index);

                    areaTriggerTemplate.Data[0] = extents.X;
                    areaTriggerTemplate.Data[1] = extents.Y;
                    areaTriggerTemplate.Data[2] = extentsTarget.X;
                    areaTriggerTemplate.Data[3] = extentsTarget.Y;
                }

                if ((areaTriggerTemplate.Flags & (uint)AreaTriggerCreatePropertiesFlags.HasMovementScript) != 0)
                {
                    packet.ReadInt32("SpellScriptID");
                    packet.ReadVector3("Center");
                }

                if ((areaTriggerTemplate.Flags & (uint)AreaTriggerCreatePropertiesFlags.HasOrbit) != 0)
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
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_0_45166))
                    packet.ReadBit("StopAnimKits", index);

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
                var hasActionButtons = packet.ReadBit("HasActionButtons", index);

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
                    var actionButtonCount = (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51505) ? 180 : 132);
                    for (int i = 0; i < actionButtonCount; i++)
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

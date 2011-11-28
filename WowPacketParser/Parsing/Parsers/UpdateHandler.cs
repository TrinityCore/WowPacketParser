using System;
using System.Collections;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.SQL;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid=WowPacketParser.Misc.Guid;

namespace WowPacketParser.Parsing.Parsers
{
    public static class UpdateHandler
    {
        public static Dictionary<uint, Dictionary<Guid, WoWObject>> Objects =
            new Dictionary<uint, Dictionary<Guid, WoWObject>>();

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

                packet.Writer.WriteLine("[" + i + "] UpdateType: " + typeString);
                switch (typeString)
                {
                    case "Values":
                    {
                        var guid = packet.ReadPackedGuid("[" + i + "] GUID");

                        WoWObject obj;
                        var updates = ReadValuesUpdateBlock(ref packet, guid.GetObjectType(), i);

                        if (Objects.ContainsKey(map) && Objects[map].TryGetValue(guid, out obj))
                            HandleUpdateFieldChangedValues(guid, obj.Type, updates, obj.Movement);
                        break;
                    }
                    case "Movement":
                    {
                        Guid guid;
                        if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_2_9901))
                            guid = packet.ReadPackedGuid("[" + i + "] GUID");
                        else
                            guid = packet.ReadGuid("[" + i + "] GUID");

                        ReadMovementUpdateBlock(ref packet, guid, i);
                        break;
                    }
                    case "CreateObject1":
                    case "CreateObject2": // Might != CreateObject1 on Cata
                    {
                        var guid = packet.ReadPackedGuid("[" + i + "] GUID");
                        ReadCreateObjectBlock(ref packet, guid, (uint)map, i);
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

        public static void ReadCreateObjectBlock(ref Packet packet, Guid guid, uint map, int index)
        {
            var objType = packet.ReadEnum<ObjectType>("Object Type", TypeCode.Byte, index);
            var moves = ReadMovementUpdateBlock(ref packet, guid, index);
            var updates = ReadValuesUpdateBlock(ref packet, objType, index);

            var obj = new WoWObject {Type = objType, Movement = moves, UpdateFields = updates, Map = map};

            Stuffing.Objects.TryAdd(guid, obj);

            HandleUpdateFieldChangedValues(guid, objType, updates, moves, true);
        }

        public static void ReadObjectsBlock(ref Packet packet, int index)
        {
            var objCount = packet.ReadInt32("Object Count", index);
            for (var j = 0; j < objCount; j++)
                packet.ReadPackedGuid("Object GUID", index, j);
        }

        public static Dictionary<int, UpdateField> ReadValuesUpdateBlock(ref Packet packet, ObjectType type, int index)
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
                string value = blockVal.Int32Value + "/" + blockVal.SingleValue;
                if (i < objectEnd)
                    key = UpdateFields.GetUpdateFieldName(i, "ObjectField");
                else
                {
                    switch (type)
                    {
                        case ObjectType.Container:
                        {
                            if (i < UpdateFields.GetUpdateField(ItemField.ITEM_END))
                                goto case ObjectType.Item;

                            key = UpdateFields.GetUpdateFieldName(i, "ContainerField");
                            break;
                        }
                        case ObjectType.Item:
                        {
                            key = UpdateFields.GetUpdateFieldName(i, "ItemField");
                            break;
                        }
                        case ObjectType.Player:
                        {
                            if (i < UpdateFields.GetUpdateField(UnitField.UNIT_END))
                                goto case ObjectType.Unit;

                            key = UpdateFields.GetUpdateFieldName(i, "PlayerField");
                            break;
                        }
                        case ObjectType.Unit:
                        {
                            key = UpdateFields.GetUpdateFieldName(i, "UnitField");
                            break;
                        }
                        case ObjectType.GameObject:
                        {
                            key = UpdateFields.GetUpdateFieldName(i, "GameObjectField");
                            break;
                        }
                        case ObjectType.DynamicObject:
                        {
                            key = UpdateFields.GetUpdateFieldName(i, "DynamicObjectField");
                            break;
                        }
                        case ObjectType.Corpse:
                        {
                            key = UpdateFields.GetUpdateFieldName(i, "CorpseField");
                            break;
                        }
                    }
                }
                packet.Writer.WriteLine("[" + index + "] " + key + ": " + value);
                dict.Add(i, blockVal);
            }

            return dict;
        }

        // Outdated, not using new UpdateFields. Plix rewrite.
        public static void HandleUpdateFieldChangedValues(Guid guid, ObjectType objType,
            Dictionary<int, UpdateField> updates, MovementInfo moves, bool updatingObject = false)
        {
           /* bool shouldCommit;
            bool isIntValue;
            bool flags;
            const bool isTemplate = true;
            var isCreated = false;
            string sql;
            string fieldName = null;

            if (objType == ObjectType.Unit && guid.GetHighType() != HighGuidType.Pet)
            {
                if (!updatingObject)
                {
                    SQLStore.WriteData(SQLStore.CreatureUpdates.GetCommand("speed_walk", guid.GetEntry(), moves.WalkSpeed));
                    SQLStore.WriteData(SQLStore.CreatureUpdates.GetCommand("speed_run", guid.GetEntry(), moves.RunSpeed));
                }

                foreach (var upVal in updates)
                {
                    shouldCommit = true;
                    isIntValue = true;
                    flags = false;

                    var idx = (UnitField)upVal.Key;
                    var val = upVal.Value;

                    switch (idx)
                    {
                        case UnitField.UNIT_CREATED_BY_SPELL:
                        case UnitField.UNIT_FIELD_CREATEDBY:
                        case UnitField.UNIT_FIELD_SUMMONEDBY:
                        {
                            isCreated = true;
                            shouldCommit = false;
                            break;
                        }
                        case (UnitField)ObjectField.OBJECT_FIELD_SCALE_X:
                        {
                            fieldName = "scale";
                            isIntValue = false;
                            break;
                        }
                        case UnitField.UNIT_DYNAMIC_FLAGS:
                        {
                            fieldName = "dynamicflags";
                            flags = true;
                            break;
                        }
                        case UnitField.UNIT_NPC_FLAGS:
                        {
                            fieldName = "npcflag";
                            flags = true;
                            break;
                        }
                        case UnitField.UNIT_FIELD_FLAGS:
                        {
                            fieldName = "unit_flags";
                            flags = true;
                            break;
                        }
                        case UnitField.UNIT_FIELD_ATTACK_POWER:
                        {
                            fieldName = "attackpower";
                            break;
                        }
                        case UnitField.UNIT_FIELD_BASEATTACKTIME:
                        {
                            fieldName = "baseattacktime";
                            break;
                        }
                        case UnitField.UNIT_FIELD_LEVEL:
                        {
                            fieldName = "minlevel = " + val.Int32Value + ", maxlevel";
                            break;
                        }
                        case UnitField.UNIT_FIELD_RANGED_ATTACK_POWER:
                        {
                            fieldName = "rangedattackpower";
                            break;
                        }
                        case UnitField.UNIT_FIELD_RANGEDATTACKTIME:
                        {
                            fieldName = "rangeattacktime";
                            break;
                        }
                        case UnitField.UNIT_FIELD_FACTIONTEMPLATE:
                        {
                            fieldName = "faction_A = " + val.Int32Value + ", faction_H";
                            break;
                        }
                        default:
                        {
                            shouldCommit = false;
                            break;
                        }
                    }

                    if (!shouldCommit)
                        continue;

                    var finalValue = isIntValue ? (object)val.Int32Value : val.SingleValue;

                    if (flags)
                        finalValue = "0x" + ((int)finalValue).ToString("X8");

                    if (updatingObject)
                        sql = SQLStore.CreatureSpawnUpdates.GetCommand(fieldName, (uint) guid.GetLow(), finalValue);
                    else sql = SQLStore.CreatureUpdates.GetCommand(fieldName, guid.GetEntry(), finalValue);

                    SQLStore.WriteData(sql);
                }

                if (!isCreated)
                    SQLStore.WriteData(SQLStore.CreatureSpawns.GetCommand(guid.GetEntry(),
                        MovementHandler.CurrentMapId, MovementHandler.CurrentPhaseMask,
                        moves.Position, moves.Orientation));
            }

            if (objType != ObjectType.GameObject)
                return;

            foreach (var upVal in updates)
            {
                shouldCommit = true;
                isIntValue = true;
                flags = false;

                var idx = (GameObjectField)upVal.Key;
                var val = upVal.Value;

                switch (idx)
                {
                    //case GameObjectField.GAMEOBJECT_FIELD_CREATED_BY:
                    //{
                    //    isCreated = true;
                    //    shouldCommit = false;
                    //  break;
                    //}
                    case (GameObjectField)ObjectField.OBJECT_FIELD_SCALE_X:
                    {
                        fieldName = "size";
                        isIntValue = false;
                        break;
                    }
                    case GameObjectField.GAMEOBJECT_FACTION:
                    {
                        fieldName = "faction";
                        break;
                    }
                    case GameObjectField.GAMEOBJECT_FLAGS:
                    {
                        fieldName = "flags";
                        flags = true;
                        break;
                    }
                    default:
                    {
                        shouldCommit = false;
                        break;
                    }
                }

                if (!shouldCommit)
                    continue;

                var finalValue = isIntValue ? (object)val.Int32Value : val.SingleValue;

                if (flags)
                    finalValue = "0x" + ((int)finalValue).ToString("X8");

                if (isTemplate)
                    sql = SQLStore.GameObjectUpdates.GetCommand(fieldName, guid.GetEntry(), finalValue);
                //else
                //    sql = SQLStore.GameObjectSpawnUpdates.GetCommand(fieldName, (uint)guid.GetLow(),
                //        finalValue);

                SQLStore.WriteData(sql);
            }

            if (!isCreated)
                SQLStore.WriteData(SQLStore.GameObjectSpawns.GetCommand(guid.GetEntry(), MovementHandler.CurrentMapId,
                    MovementHandler.CurrentPhaseMask, moves.Position, moves.Orientation));
            * */
        }

        public static MovementInfo ReadMovementUpdateBlock(ref Packet packet, Guid guid, int index)
        {
            var moveInfo = new MovementInfo();

            var flagsTypeCode = ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767) ? TypeCode.UInt16 : TypeCode.Byte;
            var flags = packet.ReadEnum<UpdateFlag>("[" + index + "] Update Flags", flagsTypeCode);

            if (flags.HasAnyFlag(UpdateFlag.Living))
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_0_14333))
                    moveInfo = MovementHandler.ReadMovementInfo420(ref packet, guid, index);
                else
                    moveInfo = MovementHandler.ReadMovementInfo(ref packet, guid, index);
                var moveFlags = moveInfo.Flags;

                var speedCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056) ? 9 : 8;
                int speedShift;
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_1_0_13914) &&
                    ClientVersion.RemovedInVersion(ClientVersionBuild.V4_2_2_14545))
                    speedShift = 1;  // enums shifted by one
                else speedShift = 0;

                for (var i = 0; i < speedCount - speedShift; i++)
                {
                    var speedType = (SpeedType)(i + speedShift);
                    var speed = packet.ReadSingle("["+ index + "] " + speedType + " Speed");

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

                // this is incorrect for certain objects but I can't figure out why
                if (moveFlags.HasAnyFlag(MovementFlag.SplineEnabled) || moveInfo.HasSplineData)
                {
                    var splineFlags = packet.ReadEnum<SplineFlag>("Spline Flags", TypeCode.Int32, index);

                    // HACK: Fix splineflags for next ifs. Need to use different enum for TBC
                    if (ClientVersion.RemovedInVersion(ClientType.WrathOfTheLichKing))
                        splineFlags = (SplineFlag)((int)splineFlags >> 1);

                    if (splineFlags.HasAnyFlag(SplineFlag.FinalTarget))
                        packet.ReadGuid("Final Spline Target GUID", index);
                    else if (splineFlags.HasAnyFlag(SplineFlag.FinalOrientation))
                        packet.ReadSingle("Final Spline Orientation", index);
                    else if (splineFlags.HasAnyFlag(SplineFlag.FinalPoint))
                        packet.ReadVector3("Final Spline Coords", index);

                    packet.ReadInt32("Spline Time", index);
                    packet.ReadInt32("Spline Full Time", index);
                    packet.ReadInt32("Spline Unk Int32 1", index);

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                    {
                        packet.ReadSingle("Spline Duration Multiplier", index);
                        packet.ReadSingle("Spline Unit Interval", index);
                        packet.ReadSingle("Spline Unk Float 2", index);
                        packet.ReadInt32("Spline Height Time", index);
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
                    packet.ReadSingle("GO Transport Orientation", index);
                }
                else if (flags.HasAnyFlag(UpdateFlag.StationaryObject))
                    packet.ReadVector4("Stationary Position", index);
            }

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_2_2_14545))
            {
                if (flags.HasAnyFlag(UpdateFlag.Unknown1))
                    packet.ReadInt32("Unk Int32", index);

                if (flags.HasAnyFlag(UpdateFlag.LowGuid))
                    packet.ReadInt32("Low GUID", index);
            }

            if (flags.HasAnyFlag(UpdateFlag.AttackingTarget))
                packet.ReadPackedGuid("Target GUID", index);

            if (flags.HasAnyFlag(UpdateFlag.Transport))
                packet.ReadInt32("Transport Movement Time (ms)", index);

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
                packet.ReadPackedQuaternion("GO Rotation", index);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
            {
                if (flags.HasAnyFlag(UpdateFlag.TransportUnkArray))
                {
                    var count = packet.ReadByte("Count", index);
                    for (var i = 0; i < count; i++)
                        packet.ReadInt32("Unk Int32", index, count);
                }
            }

            // Initialize fields that are not used by GOs
            if (guid.GetObjectType() == ObjectType.GameObject)
            {
                moveInfo.VehicleId = 0;
                moveInfo.WalkSpeed = 0;
                moveInfo.RunSpeed = 0;
            }

            return moveInfo;
        }

        [Parser(Opcode.SMSG_COMPRESSED_UPDATE_OBJECT)]
        public static void HandleCompressedUpdateObject(Packet packet)
        {
            HandleUpdateObject(packet.Inflate(packet.ReadInt32()));
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

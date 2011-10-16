using System;
using System.Collections;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Misc.Objects;
using WowPacketParser.SQL;
using Guid=WowPacketParser.Misc.Guid;

namespace WowPacketParser.Parsing.Parsers
{
    public static class UpdateHandler
    {
        public static readonly Dictionary<int, Dictionary<Guid, WoWObject>> Objects =
            new Dictionary<int, Dictionary<Guid, WoWObject>>();

        [Parser(Opcode.SMSG_UPDATE_OBJECT)]
        public static void HandleUpdateObject(Packet packet)
        {
            var map = MovementHandler.CurrentMapId;
            if (ClientVersion.Version > ClientVersionBuild.V3_3_5a_12340)
                map = packet.ReadUInt16("Map");

            var count = packet.ReadUInt32("Count");

            for (var i = 0; i < count; i++)
            {
                var type = packet.ReadEnum<UpdateType>("[" + i + "] Update Type", TypeCode.Byte);
                switch (type)
                {
                    case UpdateType.Values:
                    {
                        var guid = packet.ReadPackedGuid("[" + i + "] GUID");
                        
                        WoWObject obj;
                        var updates = ReadValuesUpdateBlock(packet, guid.GetObjectType(), i);

                        if (Objects.ContainsKey(map) && Objects[map].TryGetValue(guid, out obj))
                            HandleUpdateFieldChangedValues(false, guid, obj.Type, updates, obj.Movement);
                        break;
                    }
                    case UpdateType.Movement:
                    {
                        var guid = packet.ReadPackedGuid("[" + i + "] GUID");
                        ReadMovementUpdateBlock(packet, guid, i);
                        break;
                    }
                    case UpdateType.CreateObject2: // Might != CreateObject1 on Cata
                    case UpdateType.CreateObject1:
                    {
                        var guid = packet.ReadPackedGuid("[" + i + "] GUID");
                        ReadCreateObjectBlock(packet, guid, i);
                        break;
                    }
                    case UpdateType.FarObjects:
                    case UpdateType.NearObjects:
                    // case UpdateType.DestroyObjects:
                    {
                        ReadObjectsBlock(packet, i);
                        break;
                    }
                }
            }
        }

        public static void ReadCreateObjectBlock(Packet packet, Guid guid, int index)
        {
            var objType = packet.ReadEnum<ObjectType>("[" + index + "] Object Type", TypeCode.Byte);
            var moves = ReadMovementUpdateBlock(packet, guid, index);
            var updates = ReadValuesUpdateBlock(packet, objType, index);

            var obj = new WoWObject(guid, objType, moves, updates);
            obj.Position = moves.Position;

            try
            {
                var objects = Objects[MovementHandler.CurrentMapId];
                var shouldAdd = true;
                foreach (var woObj in objects.Values)
                {
                    if (woObj.Position != obj.Position && woObj.Guid != guid)
                        continue;

                    shouldAdd = false;
                    break;
                }

                if (!shouldAdd)
                    return;

                objects.Add(guid, obj);
            }
            catch { }

            HandleUpdateFieldChangedValues(true, guid, objType, updates, moves);
        }

        public static void ReadObjectsBlock(Packet packet, int index)
        {
            var objCount = packet.ReadInt32("[" + index + "] Object Count");
            for (var j = 0; j < objCount; j++)
                packet.ReadPackedGuid("[" + index + "][" + j + "] Object GUID");
        }

        public static Dictionary<int, UpdateField> ReadValuesUpdateBlock(Packet packet, ObjectType type, int index)
        {
            var maskSize = packet.ReadByte();

            var updateMask = new int[maskSize];
            for (var i = 0; i < maskSize; i++)
                updateMask[i] = packet.ReadInt32();

            var mask = new BitArray(updateMask);
            var dict = new Dictionary<int, UpdateField>();

            var objectEnd = (int)ObjectField.OBJECT_END;

            string key = "Block Value";
            string value = "";
            for (var i = 0; i < mask.Count; i++)
            {
                if (!mask[i])
                    continue;

                var blockVal = packet.ReadUpdateField();
                key = "Block Value";
                value = blockVal.Int32Value + "/" + blockVal.SingleValue;
                if (i < objectEnd)
                {
                    ObjectField field = (ObjectField)i;
                    key = field.ToString();
                }
                else
                {
                    switch (type)
                    {
                        case ObjectType.Container:
                        {
                            if (i < (int)ItemField.ITEM_END)
                                goto case ObjectType.Item;

                            ContainerField field = (ContainerField)i;
                            key = field.ToString();
                            break;
                        }                                
                        case ObjectType.Item:
                        {
                            ItemField field = (ItemField)i;
                            key = field.ToString();
                            break;
                        }
                        case ObjectType.Player:
                        {
                            if (i < (int)UnitField.UNIT_END)
                                goto case ObjectType.Unit;

                            PlayerField field = (PlayerField)i;
                            key = field.ToString();
                            break;
                        }                                
                        case ObjectType.Unit:
                        {
                            UnitField field = (UnitField)i;
                            key = field.ToString();
                            break;
                        }
                        case ObjectType.GameObject:
                        {
                            GameObjectField field = (GameObjectField)i;
                            key = field.ToString();
                            break;
                        }
                        case ObjectType.DynamicObject:
                        {
                            DynamicObjectField field = (DynamicObjectField)i;
                            key = field.ToString();
                            break;
                        }
                        case ObjectType.Corpse:
                        {
                            CorpseField field = (CorpseField)i;
                            key = field.ToString();
                            break;
                        }
                        default:
                            break;
                    }
                }
                Console.WriteLine("[" + index + "] " + key + ": " + value);
                dict.Add(i, blockVal);
            }

            return dict;
        }

        public static void HandleUpdateFieldChangedValues(bool creating, Guid guid, ObjectType objType,
            Dictionary<int, UpdateField> updates, MovementInfo moves)
        {
            bool shouldCommit;
            bool isIntValue;
            bool flags;
            var isTemplate = true;
            var isCreated = false;
            string sql;
            string fieldName = null;

            if (objType == ObjectType.Unit && guid.GetHighType() != HighGuidType.Pet)
            {
                if (creating)
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

                    if (isTemplate)
                        sql = SQLStore.CreatureUpdates.GetCommand(fieldName, guid.GetEntry(), finalValue);
                    else
                        sql = SQLStore.CreatureSpawnUpdates.GetCommand(fieldName, (uint)guid.GetLow(),
                            finalValue);

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
                    case GameObjectField.GAMEOBJECT_FIELD_CREATED_BY:
                    {
                        isCreated = true;
                        shouldCommit = false;
                        break;
                    }
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
                else
                    sql = SQLStore.GameObjectSpawnUpdates.GetCommand(fieldName, (uint)guid.GetLow(),
                        finalValue);

                SQLStore.WriteData(sql);
            }

            if (!isCreated)
                SQLStore.WriteData(SQLStore.GameObjectSpawns.GetCommand(guid.GetEntry(), MovementHandler.CurrentMapId,
                    MovementHandler.CurrentPhaseMask, moves.Position, moves.Orientation));
        }

        public static MovementInfo ReadMovementUpdateBlock(Packet packet, Guid guid, int index)
        {
            var moveInfo = new MovementInfo();

            var flags = packet.ReadEnum<UpdateFlag>("[" + index + "] Update Flags", TypeCode.UInt16);
            if (flags.HasAnyFlag(UpdateFlag.Living))
            {
                moveInfo = MovementHandler.ReadMovementInfo(packet, guid, index);
                var moveFlags = moveInfo.Flags;

                var maxSpeedCount = ClientVersion.Version >= ClientVersionBuild.V4_1_0_13914 ? 8 : 9; // enums shifted by one
                for (var i = 0; i < maxSpeedCount; i++)
                {
                    var j = (SpeedType)(i + (9 - maxSpeedCount)); // enums shifted by one
                    var speed = packet.ReadSingle("["+ index + "] " + j + " Speed");

                    switch (j)
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

                if (moveFlags.HasAnyFlag(MovementFlag.SplineEnabled))
                {
                    var splineFlags = packet.ReadEnum<SplineFlag>("[" + index + "] Spline Flags", TypeCode.Int32);
                    if (splineFlags.HasAnyFlag(SplineFlag.FinalPoint))
                        packet.ReadVector3("[" + index + "] Final Spline Coords");

                    if (splineFlags.HasAnyFlag(SplineFlag.FinalTarget))
                        packet.ReadGuid("[" + index + "] Final Spline Target GUID");

                    if (splineFlags.HasAnyFlag(SplineFlag.FinalOrientation))
                        packet.ReadSingle("[" + index + "] Final Spline Orientation");

                    packet.ReadInt32("[" + index + "] Spline Time");
                    packet.ReadInt32("[" + index + "] Spline Full Time");
                    packet.ReadInt32("[" + index + "] Spline Unk Int32 1");
                    packet.ReadSingle("[" + index + "] Spline Duration Multiplier");
                    packet.ReadSingle("[" + index + "] Spline Unit Interval");
                    packet.ReadSingle("[" + index + "] Spline Unk Float 2");
                    packet.ReadInt32("[" + index + "] Spline Height Time");
                    var splineCount = packet.ReadInt32();
                    for (var i = 0; i < splineCount; i++)
                        packet.ReadVector3("[" + index + "][" + i + "] Spline Waypoint");

                    packet.ReadEnum<SplineMode>("[" + index + "] Spline Mode", TypeCode.Byte);
                    packet.ReadVector3("[" + index + "] Spline Endpoin");
                }
            }
            else
            {
                if (flags.HasAnyFlag(UpdateFlag.GOPosition))
                {
                    packet.ReadPackedGuid("[" + index + "] GO Position GUID");
                    moveInfo.Position = packet.ReadVector3("[" + index + "] GO Position");
                    packet.ReadVector3("[" + index + "] GO Transport Position");
                    moveInfo.Orientation = packet.ReadSingle("[" + index + "] GO Orientation");
                    packet.ReadSingle("[" + index + "] GO Transport Orientation");
                }
                else if (flags.HasAnyFlag(UpdateFlag.StationaryObject))
                    packet.ReadVector4("[" + index + "] Stationary Position");
            }

            if (flags.HasAnyFlag(UpdateFlag.Unknown1))
                packet.ReadInt32("[" + index + "] Unk Int32");

            if (flags.HasAnyFlag(UpdateFlag.LowGuid))
                packet.ReadInt32("[" + index + "] Low GUID");

            if (flags.HasAnyFlag(UpdateFlag.AttackingTarget))
                packet.ReadPackedGuid("[" + index + "] Target GUID");

            if (flags.HasAnyFlag(UpdateFlag.Transport))
                packet.ReadInt32("[" + index + "] Transport Movement Time (ms)");

            if (flags.HasAnyFlag(UpdateFlag.Vehicle))
            {
                var vehId = packet.ReadInt32("[" + index + "] Vehicle ID");
                packet.ReadSingle("[" + index + "] Vehicle Orientation");
                SQLStore.WriteData(SQLStore.CreatureUpdates.GetCommand("VehicleId", guid.GetEntry(), vehId));
            }

            if (flags.HasAnyFlag(UpdateFlag.GORotation))
                packet.ReadPackedQuaternion("[" + index + "] GO Rotation");
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
            packet.ReadBoolean("Despawn Animation");
        }
    }
}

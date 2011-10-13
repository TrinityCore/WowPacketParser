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
            int map = -1;
            if (ClientVersion.Version > ClientVersionBuild.V3_3_5a_12340)
                map = packet.ReadInt16("Map");
            var count = packet.ReadInt32("Count");

            byte unkByte = 0;
            if (ClientVersion.Version > ClientVersionBuild.V3_3_5a_12340)
            {
                long sposition = packet.GetPosition();
                unkByte = packet.ReadByte();
                if (unkByte != 3)
                    packet.SetPosition(sposition);
                else
                {
                    Console.WriteLine("firstType: " + unkByte);
                    var guidCount = packet.ReadInt32("GUID Count");
                    if (guidCount > 0)
                        for (uint i = 0; i < guidCount; i++)
                            packet.ReadPackedGuid("GUID " + (i + 1));
                }
            }

            int realCount;
            if (ClientVersion.Version > ClientVersionBuild.V3_3_5a_12340)
                realCount = count - ((unkByte == 3) ? 1 : 0);
            else
                realCount = count;

            for (var i = 0; i < realCount; i++)
            {
                var type = packet.ReadEnum<UpdateType>("Update Type #" + (i + 1), TypeCode.Byte);

                switch (type)
                {
                    case UpdateType.Values:
                    {
                        var guid = packet.ReadPackedGuid("GUID");
                        var updates = ReadValuesUpdateBlock(packet);

                        WoWObject obj;

                        if (ClientVersion.Version > ClientVersionBuild.V3_3_5a_12340)
                        {
                            if (Objects.ContainsKey(map) && Objects[map].TryGetValue(guid, out obj))
                                HandleUpdateFieldChangedValues(false, guid, obj.Type, updates, obj.Movement);
                        }
                        else
                            if (Objects[MovementHandler.CurrentMapId].TryGetValue(guid, out obj))
                                // System.Collections.Generic.KeyNotFoundException in the next line
                                HandleUpdateFieldChangedValues(false, guid, obj.Type, updates, obj.Movement);
                        break;
                    }
                    case UpdateType.Movement:
                    {
                        var guid = packet.ReadPackedGuid("GUID");

                        if (ClientVersion.Version > ClientVersionBuild.V3_3_5a_12340)
                            packet.ReadEnum<ObjectType>("Object type");

                        ReadMovementUpdateBlock(packet, guid);

                        if (ClientVersion.Version > ClientVersionBuild.V3_3_5a_12340)
                            ReadValuesUpdateBlock(packet);
                        break;
                    }
                    case UpdateType.CreateObject1:
                    case UpdateType.CreateObject2:
                    {
                        var guid = packet.ReadPackedGuid("GUID");
                        ReadCreateObjectBlock(packet, guid);
                        break;
                    }
                    case UpdateType.FarObjects:
                    case UpdateType.NearObjects:
                    {
                        var objCount = packet.ReadInt32("Object Count");

                        for (var j = 0; j < objCount; j++)
                        {
                            packet.ReadPackedGuid("Object GUID");
                        }
                        break;
                    }
                }
            }
        }

        public static void ReadCreateObjectBlock(Packet packet, Guid guid)
        {
            var objType = (ObjectType)packet.ReadByte();
            Console.WriteLine("Object Type: " + objType);

            var moves = ReadMovementUpdateBlock(packet, guid);
            var updates = ReadValuesUpdateBlock(packet);

            var obj = new WoWObject(guid, objType, moves, updates);
            obj.Position = moves.Position;

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

            HandleUpdateFieldChangedValues(true, guid, objType, updates, moves);
        }

        /// <summary>
        /// Reads update values (a.k.a. Block Values).
        /// </summary>
        /// <param name="packet"></param>
        /// <returns>Dictionary in the form {block value id | value}.</returns>
        public static Dictionary<int, UpdateField> ReadValuesUpdateBlock(Packet packet)
        {
            var maskSize = packet.ReadByte();
            Console.WriteLine("Block Count: " + maskSize);

            var updateMask = new int[maskSize];
            for (var i = 0; i < maskSize; i++)
            {
                var blockIdx = packet.ReadInt32();
                updateMask[i] = blockIdx;
            }

            var mask = new BitArray(updateMask);
            var dict = new Dictionary<int, UpdateField>();

            for (var i = 0; i < mask.Count; i++)
            {
                if (!mask[i])
                    continue;

                var blockVal = packet.ReadUpdateField();
                Console.WriteLine("Block Value " + i + ": " + blockVal.Int32Value + "/" +
                    blockVal.SingleValue);

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

        public static MovementInfo ReadMovementUpdateBlock(Packet packet, Guid guid)
        {
            var moveInfo = new MovementInfo();

            var flags = packet.ReadEnum<UpdateFlag>("Update Flags", TypeCode.Int16);

            if (flags.HasFlag(UpdateFlag.Living))
            {
                moveInfo = MovementHandler.ReadMovementInfo(packet, guid);
                var moveFlags = moveInfo.Flags;

                for (var i = 0; i < 9; i++)
                {
                    var j = (SpeedType)i;
                    var speed = packet.ReadSingle("[" + j + "] Speed");

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

                if (moveFlags.HasFlag(MovementFlag.SplineEnabled))
                {
                    var splineFlags = packet.ReadEnum<SplineFlag>("Spline Flags", TypeCode.Int32);

                    if (splineFlags.HasFlag(SplineFlag.FinalPoint))
                        packet.ReadVector3("Final Spline Coords");

                    if (splineFlags.HasFlag(SplineFlag.FinalTarget))
                        packet.ReadGuid("Final Spline Target GUID");

                    if (splineFlags.HasFlag(SplineFlag.FinalOrientation))
                        packet.ReadSingle("Final Spline Orientation");

                    packet.ReadInt32("Spline Time");
                    packet.ReadInt32("Spline Full Time");
                    packet.ReadInt32("Spline Unk Int32 1");
                    packet.ReadSingle("Spline Duration Multiplier");
                    packet.ReadSingle("Spline Unit Interval");
                    packet.ReadSingle("Spline Unk Float 2");
                    packet.ReadInt32("Spline Height Time");

                    var splineCount = packet.ReadInt32("Spline Count");
                    for (var i = 0; i < splineCount; i++)
                        packet.ReadVector3("[" + i + "] Spline Waypoint");

                    packet.ReadEnum<SplineMode>("Spline Mode", TypeCode.Byte);
                    packet.ReadVector3("Spline Endpoint");
                }
            }
            else
            {
                if (flags.HasFlag(UpdateFlag.GOPosition))
                {
                    packet.ReadPackedGuid("GO Position GUID");
                    var gopos = packet.ReadVector3("GO Position");
                    packet.ReadVector3("GO Transport Position");
                    var goFacing = packet.ReadSingle("GO Orientation");

                    moveInfo.Position = gopos;
                    moveInfo.Orientation = goFacing;

                    packet.ReadSingle("GO Transport Orientation");
                }
                else if (flags.HasFlag(UpdateFlag.StationaryObject))
                {
                    packet.ReadVector4("Stationary Position");
                }
            }

            if (flags.HasFlag(UpdateFlag.Unknown1))
                packet.ReadInt32("Unk Int32");

            if (flags.HasFlag(UpdateFlag.LowGuid))
                packet.ReadInt32("Low GUID");

            if (flags.HasFlag(UpdateFlag.AttackingTarget))
                packet.ReadPackedGuid("Target GUID");

            if (flags.HasFlag(UpdateFlag.Transport))
                packet.ReadInt32("Transport Creation Time");

            if (flags.HasFlag(UpdateFlag.Vehicle))
            {
                var vehId = packet.ReadInt32("Vehicle ID");
                var vehFacing = packet.ReadSingle("Vehicle Orientation");

                SQLStore.WriteData(SQLStore.CreatureUpdates.GetCommand("VehicleId", guid.GetEntry(), vehId));
            }

            if (flags.HasFlag(UpdateFlag.GORotation))
                packet.ReadPackedQuaternion("GO Rotation");

            return moveInfo;
        }

        [Parser(Opcode.SMSG_COMPRESSED_UPDATE_OBJECT)]
        public static void HandleCompressedUpdateObject(Packet packet)
        {
            var decompCount = packet.ReadInt32();
            var pkt = packet.Inflate(decompCount);
            HandleUpdateObject(pkt);
        }

        [Parser(Opcode.SMSG_DESTROY_OBJECT)]
        public static void HandleDestroyObject(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadBoolean("Despawn Animation");
        }
    }
}

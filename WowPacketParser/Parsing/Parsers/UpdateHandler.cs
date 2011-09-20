using System;
using System.Collections;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;
using Guid=WowPacketParser.Misc.Guid;

namespace WowPacketParser.Parsing.Parsers
{
    public static class UpdateHandler
    {
        public static Dictionary<int, Dictionary<Guid, WowObject>> ObjectStore = new Dictionary<int, Dictionary<Guid, WowObject>>();

        [Parser(Opcode.SMSG_UPDATE_OBJECT)]
        public static void HandleUpdateObject(Packet packet)
        {
            var count = packet.ReadInt32();
            Console.WriteLine("Count: " + count);

            for (var i = 0; i < count; i++)
            {
                var type = (UpdateType)packet.ReadByte();
                Console.WriteLine("Update Type: " + type);

                switch (type)
                {
                    case UpdateType.Values:
                    {
                        var guid = packet.ReadPackedGuid();
                        Console.WriteLine("GUID: " + guid);

                        var updates = ReadValuesUpdateBlock(packet);

                        WowObject obj;
                        if (ObjectStore[MovementHandler.CurrentMapId].TryGetValue(guid, out obj))
                            HandleUpdateFieldChangedValues(false, guid, obj.Type, updates, obj.Movement, obj);
                        break;
                    }
                    case UpdateType.Movement:
                    {
                        var guid = packet.ReadPackedGuid();
                        Console.WriteLine("GUID: " + guid);

                        ReadMovementUpdateBlock(packet, guid);
                        break;
                    }
                    case UpdateType.CreateObject1:
                    case UpdateType.CreateObject2:
                    {
                        var guid = packet.ReadPackedGuid();
                        Console.WriteLine("GUID: " + guid);

                        ReadCreateObjectBlock(packet, guid);
                        break;
                    }
                    case UpdateType.FarObjects:
                    case UpdateType.NearObjects:
                    {
                        var objCount = packet.ReadInt32();
                        Console.WriteLine("Object Count: " + objCount);

                        for (var j = 0; j < objCount; j++)
                        {
                            var guid = packet.ReadPackedGuid();
                            Console.WriteLine("Object GUID: " + guid);
                        }
                        break;
                    }
                }
            }
        }

        public static void ReadCreateObjectBlock(Packet packet, Guid guid)
        {
            // Here we can choose to not parse updateblocks for creatures/gameobjects
            // In the future i plan to add a option in the GUI to only parse selected creature/gameobject entries.
            //  bool shouldbeparsed = false;
            //  foreach (var entry in options.Entry)
            //      if (guid.GetEntry() == entry)
            //      {
            //          shouldbeparsed = true;
            //          continue;
            //      }
            //  if (!shouldbeparsed)
            //      return;
            var objType = (ObjectType)packet.ReadByte();
            Console.WriteLine("Object Type: " + objType);

            var moves = ReadMovementUpdateBlock(packet, guid);
            var updates = ReadValuesUpdateBlock(packet);

            var obj = new WowObject(guid, objType, moves);
            obj.Position = moves.Position;

            var objects = ObjectStore[MovementHandler.CurrentMapId];
            var shouldAdd = true;
            foreach (var woObj in objects.Values)
            {
                if (woObj.Position != obj.Position && woObj.Guid == guid)
                    continue;

                shouldAdd = false;
                break;
            }

            if (!shouldAdd)
                return;

            objects.Add(guid, obj);

            HandleUpdateFieldChangedValues(true, guid, objType, updates, moves, obj);
        }

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
            Dictionary<int, UpdateField> updates, MovementInfo moves, WowObject obj)
        {
            if (objType == ObjectType.Unit && guid.GetHighType() != HighGuidType.Pet)
            {
                if (creating)
                {
                    SQLStore.WriteData(SQLStore.CreatureUpdates.GetCommand("speed_walk", guid.GetEntry(), moves.WalkSpeed));
                    SQLStore.WriteData(SQLStore.CreatureUpdates.GetCommand("speed_run", guid.GetEntry(), moves.RunSpeed));
                }

                foreach (var upVal in updates)
                {
                    var idx = (UnitField)upVal.Key;
                    var val = upVal.Value;

                    switch (idx)
                    {
                        case (UnitField)ObjectField.OBJECT_FIELD_SCALE_X:
                        {
                            break;
                        }
                        case UnitField.UNIT_DYNAMIC_FLAGS:
                        {
                            //obj._dynamicflag = val.Int32Value; //(UnitDynamicFlags)
                            break;
                        }
                        case UnitField.UNIT_NPC_FLAGS:
                        {
                            //obj._npcflag = val.Int32Value; //(NPCFlags)
                            break;
                        }
                        case UnitField.UNIT_FIELD_FLAGS:
                        {
                            //obj._unitflag = val.Int32Value; //(UnitFlags)
                            break;
                        }
                        case UnitField.UNIT_FIELD_BASEATTACKTIME:
                        {
                            //obj._BaseAttackTime = val.Int32Value;
                            break;
                        }
                        case UnitField.UNIT_FIELD_LEVEL:
                        {
                            //obj._Level = val.Int32Value;
                            break;
                        }
                        case UnitField.UNIT_FIELD_FACTIONTEMPLATE:
                        {
                            //obj._Faction = val.Int32Value;
                            break;
                        }
                        default:
                            break;
                    }
                    if (creating)
                        obj.AddCreature((uint)guid.GetLow(), guid.GetEntry(), 1, 1, moves.Position.X, moves.Position.Y, moves.Position.Z, moves.Orientation, 0);
                }
            }

            if (objType != ObjectType.GameObject)
                return;

            foreach (var upVal in updates)
            {
                var idx = (GameObjectField)upVal.Key;
                var val = upVal.Value;

                switch (idx)
                {
                    case GameObjectField.GAMEOBJECT_FIELD_CREATED_BY:
                    {
                        break;
                    }
                    case (GameObjectField)ObjectField.OBJECT_FIELD_SCALE_X:
                    {
                        break;
                    }
                    case GameObjectField.GAMEOBJECT_FACTION:
                    {
                        break;
                    }
                    case GameObjectField.GAMEOBJECT_FLAGS:
                    {
                        break;
                    }
                    default:
                        break;
                }
            }
        }

        public static MovementInfo ReadMovementUpdateBlock(Packet packet, Guid guid)
        {
            var moveInfo = new MovementInfo();

            var flags = (UpdateFlag)packet.ReadInt16();
            Console.WriteLine("Update Flags: " + flags);

            if (flags.HasFlag(UpdateFlag.Living))
            {
                moveInfo = MovementHandler.ReadMovementInfo(packet, guid);
                var moveFlags = moveInfo.Flags;

                for (var i = 0; i < 9; i++)
                {
                    var j = (SpeedType)i;
                    var speed = packet.ReadSingle();
                    Console.WriteLine(j + " Speed: " + speed);

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
                    var splineFlags = (SplineFlag)packet.ReadInt32();
                    Console.WriteLine("Spline Flags: " + splineFlags);

                    if (splineFlags.HasFlag(SplineFlag.FinalPoint))
                    {
                        var splineCoords = packet.ReadVector3();
                        Console.WriteLine("Final Spline Coords: " + splineCoords);
                    }

                    if (splineFlags.HasFlag(SplineFlag.FinalTarget))
                    {
                        var splineTarget = packet.ReadGuid();
                        Console.WriteLine("Final Spline Target GUID: " + splineTarget);
                    }

                    if (splineFlags.HasFlag(SplineFlag.FinalOrientation))
                    {
                        var splineOrient = packet.ReadSingle();
                        Console.WriteLine("Final Spline Orientation: " + splineOrient);
                    }

                    var splineTime = packet.ReadInt32();
                    Console.WriteLine("Spline Time: " + splineTime);

                    var fullTime = packet.ReadInt32();
                    Console.WriteLine("Spline Full Time: " + fullTime);

                    var unk1 = packet.ReadInt32();
                    Console.WriteLine("Spline Unk Int32 1: " + unk1);

                    var duraMul = packet.ReadSingle();
                    Console.WriteLine("Spline Duration Multiplier: " + duraMul);

                    var unk3 = packet.ReadSingle();
                    Console.WriteLine("Spline Unit Interval: " + unk3);

                    var unk4 = packet.ReadSingle();
                    Console.WriteLine("Spline Unk Float 2: " + unk4);

                    var unk5 = packet.ReadInt32();
                    Console.WriteLine("Spline Height Time: " + unk5);

                    var splineCount = packet.ReadInt32();
                    for (var i = 0; i < splineCount; i++)
                    {
                        var coords = packet.ReadVector3();
                        Console.WriteLine("Spline Waypoint " + i + ": " + coords);
                    }

                    var mode = (SplineMode)packet.ReadByte();
                    Console.WriteLine("Spline Mode: " + mode);

                    var endpoint = packet.ReadVector3();
                    Console.WriteLine("Spline Endpoint: " + endpoint);
                }
            }
            else
            {
                if (flags.HasFlag(UpdateFlag.GOPosition))
                {
                    var goguid = packet.ReadPackedGuid();
                    Console.WriteLine("GO Position GUID: " + goguid);

                    var gopos = packet.ReadVector3();
                    Console.WriteLine("GO Position: " + gopos);

                    var gopos2 = packet.ReadVector3();
                    Console.WriteLine("GO Transport Position: " + gopos2);

                    var goFacing = packet.ReadSingle();
                    Console.WriteLine("GO Orientation: " + goFacing);

                    moveInfo.Position = gopos;
                    moveInfo.Orientation = goFacing;

                    var goflt = packet.ReadSingle();
                    Console.WriteLine("GO Transport Orientation: " + goflt);
                }
                else if (flags.HasFlag(UpdateFlag.StationaryObject))
                {
                    var ufpos = packet.ReadVector4();
                    Console.WriteLine("Stationary Position: " + ufpos);
                }
            }

            if (flags.HasFlag(UpdateFlag.Unknown1))
            {
                var lguid = packet.ReadInt32();
                Console.WriteLine("Unk Int32: " + lguid);
            }

            if (flags.HasFlag(UpdateFlag.LowGuid))
            {
                var hguid = packet.ReadInt32();
                Console.WriteLine("Low GUID: " + hguid);
            }

            if (flags.HasFlag(UpdateFlag.AttackingTarget))
            {
                var targetGuid = packet.ReadPackedGuid();
                Console.WriteLine("Target GUID: " + targetGuid);
            }

            if (flags.HasFlag(UpdateFlag.Transport))
            {
                var ttime = packet.ReadInt32();
                Console.WriteLine("Transport Creation Time: " + ttime);
            }

            if (flags.HasFlag(UpdateFlag.Vehicle))
            {
                var vehId = packet.ReadInt32();
                Console.WriteLine("Vehicle ID: " + vehId);

                var vehFacing = packet.ReadSingle();
                Console.WriteLine("Vehicle Orientation: " + vehFacing);
                SQLStore.WriteData(SQLStore.CreatureUpdates.GetCommand("VehicleId", guid.GetEntry(), vehId));
            }

            if (flags.HasFlag(UpdateFlag.GORotation))
            {
                var gorot = packet.ReadPackedQuaternion();
                Console.WriteLine("GO Rotation: " + gorot);
            }

            return moveInfo;
        }

        [Parser(Opcode.SMSG_COMPRESSED_UPDATE_OBJECT)]
        public static void HandleCompressedUpdateObject(Packet packet)
        {
            var decompCount = packet.ReadInt32();
            var pkt = packet.Inflate(decompCount);
            HandleUpdateObject(pkt);
            packet.ReadBytes((int)packet.GetLength()); // Prevent errors even if packet is fully read.
        }

        [Parser(Opcode.SMSG_DESTROY_OBJECT)]
        public static void HandleDestroyObject(Packet packet)
        {
            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);

            var anim = packet.ReadBoolean();
            Console.WriteLine("Despawn Animation: " + anim);
        }
    }
}

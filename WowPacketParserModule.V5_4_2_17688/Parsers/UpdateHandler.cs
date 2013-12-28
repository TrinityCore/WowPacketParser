using System;
using System.Collections;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using Guid = WowPacketParser.Misc.Guid;
using UpdateFields = WowPacketParser.Enums.Version.UpdateFields;

namespace WowPacketParserModule.V5_4_2_17688.Parsers
{
    public static class UpdateHandler
    {
        [HasSniffData] // in ReadCreateObjectBlock
        [Parser(Opcode.SMSG_UPDATE_OBJECT)]
        public static void HandleUpdateObject(Packet packet)
        {
            uint map = packet.ReadUInt16("Map");
            var count = packet.ReadUInt32("Count");

            for (var i = 0; i < count; i++)
            {
                var type = packet.ReadByte();
                var typeString = ((UpdateTypeCataclysm)type).ToString();

                packet.WriteLine("[" + i + "] UpdateType: " + typeString);
                switch (typeString)
                {
                    case "Values":
                    {
                        var guid = packet.ReadPackedGuid("GUID", i);

                        WoWObject obj;
                        var updates = CoreParsers.UpdateHandler.ReadValuesUpdateBlock(ref packet, guid.GetObjectType(), i, false);

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
                        var guid = packet.ReadPackedGuid("GUID", i);
                        ReadCreateObjectBlock(ref packet, guid, map, i);
                        break;
                    }
                    case "DestroyObjects":
                    {
                        CoreParsers.UpdateHandler.ReadObjectsBlock(ref packet, i);
                        break;
                    }
                }
            }
        }

        private static void ReadCreateObjectBlock(ref Packet packet, Guid guid, uint map, int index)
        {
            var objType = packet.ReadEnum<ObjectType>("Object Type", TypeCode.Byte, index);
            var moves = ReadMovementUpdateBlock(ref packet, guid, index);
            var updates = CoreParsers.UpdateHandler.ReadValuesUpdateBlock(ref packet, objType, index, true);

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

        private static MovementInfo ReadMovementUpdateBlock(ref Packet packet, Guid guid, int index)
        {
            var moveInfo = new MovementInfo();

            var hasVehicleData = packet.ReadBit("Has Vehicle Data", index);
            var isSelf = packet.ReadBit("Self", index);

            packet.ReadBit(); // fake bit

            var transport = packet.ReadBit("Transport", index);

            packet.ReadBit(); // fake bit
            var byte2A9 = packet.ReadBit("byte2A9", index);

            var hasAttackingTarget = packet.ReadBit("Has Attacking Target", index);
            var hasStationaryPosition = packet.ReadBit("Has Stationary Position", index);

            var isAreaTrigger = packet.ReadBit("Area Trigger", index);

            var byte428 = packet.ReadBit("byte428", index);            
            
            var isSceneObject = packet.ReadBit("Scene Object", index);

            packet.ReadBit(); // fake bit
            var byte32A = packet.ReadBit("byte32A", index);

            var transportFrames = packet.ReadBits(22);
            var living = packet.ReadBit("Living", index);
            var hasGameObjectPosition = packet.ReadBit("Has GameObject Position", index);

            packet.ReadBit(); // fake bit

            var hasGameObjectRotation = packet.ReadBit("Has GameObject Rotation", index);

            var byte2A4 = packet.ReadBit("byte2A4", index);
            var byte414 = packet.ReadBit("byte414", index); 

            var hasAnimKits = packet.ReadBit("Has AnimKits", index);

            if (living)
            {
                // need update
            }

            if (hasGameObjectPosition)
            {
                // need update
            }

            if (hasAnimKits)
            {
                // need update
            }

            if (hasAttackingTarget)
            {
                attackingTargetGuid = packet.StartBitStream(4, 0, 6, 2, 1, 5, 3, 7);
            }

            if (isSceneObject)
            {
                // need update
            }

            packet.ResetBitReader();

            if (living)
            {
                // need update
            }

            if (isSceneObject)
            {
                // need update
            }

            if (hasGameObjectPosition)
            {
                // need update
            }

            if (hasVehicleData)
            {
                // need update
            }

            if (hasAttackingTarget)
            {
                packet.ParseBitStream(attackingTargetGuid, 1, 3, 5, 4, 7, 6, 2, 0);
                packet.WriteGuid("Attacking GUID", attackingTargetGuid, index);
            }

            if (hasGameObjectRotation)
            {
                packet.ReadPackedQuaternion("GameObject Rotation", index);
            }

            if (hasAnimKits)
            {
                // need update
            }

            if (hasStationaryPosition)
            {
                moveInfo.Position.X = packet.ReadSingle();
                moveInfo.Position.Z = packet.ReadSingle();
                moveInfo.Position.Y = packet.ReadSingle();
                moveInfo.Orientation = packet.ReadSingle("Stationary Orientation", index);
                packet.WriteLine("[{0}] Stationary Position: {1}", index, moveInfo.Position);
            }

            if (transport)
            {
                // need update
            }

            return moveInfo;
        }

        [Parser(Opcode.SMSG_DESTROY_OBJECT)]
        public static void HandleDestroyObject(Packet packet)
        {
            // need update
        }
    }
}

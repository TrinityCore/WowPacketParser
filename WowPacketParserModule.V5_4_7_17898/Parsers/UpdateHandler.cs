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

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class UpdateHandler
    {
        [Parser(Opcode.SMSG_DESTROY_OBJECT)]
        public static void HandleDestroyObject(Packet packet)
        {
            var guid = new byte[8];

            guid[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            packet.ReadBit("Despawn Animation");
            guid[6] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            packet.ParseBitStream(guid, 2, 1, 5, 4, 3, 6, 7, 0);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_OBJECT_UPDATE_FAILED)]
        public static void HandleObjectUpdateFailed(Packet packet)
        {
            var guid = packet.StartBitStream(4, 6, 3, 0, 7, 5, 1, 2);
            packet.ParseBitStream(guid, 4, 7, 0, 6, 5, 2, 1, 3);

            packet.WriteGuid("Guid", guid);
        }

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

            var guid1 = new byte[8];
            var transportGuid = new byte[8];
            var goTransportGuid = new byte[8];
            var attackingTargetGuid = new byte[8];
            var guid5 = new byte[8];
            var guid6 = new byte[8];
            var guid7 = new byte[8];

            var hasAnimKit1 = false;
            var hasAnimKit2 = false;
            var hasAnimKit3 = false;
            var hasFullSpline = false;
            var hasSplineStartTime = false;
            var hasSplineVerticalAcceleration = false;
            var hasMovementFlags = false;
            var hasMoveFlagsExtra = false;
            var hasTimestamp = false;
            var hasOrientation = false;
            var hasTransportData = false;
            var hasTransportTime3 = false;
            var hasTransportTime2 = false;
            var hasPitch = false;
            var hasFallData = false;
            var hasFallDirection = false;

            var bit134 = false;
            var bitA8 = false;

            var bits168 = 0u;
            var splineCount = 0u;
            var bits138 = 0u;
            var bits98 = 0u;
            var splineType = SplineType.Stop;

            var bit28D = packet.ReadBit();
            var hasVehicleData = packet.ReadBit();
            var bit3F0 = packet.ReadBit();
            var hasGameObjectRotation = packet.ReadBit();
            packet.ReadBit(); // fake bit
            var living = packet.ReadBit();
            var bit284 = packet.ReadBit();
            var bit208 = packet.ReadBit();
            packet.ReadBit(); // fake bit
            var isSelf = packet.ReadBit("Self", index);
            packet.ReadBit(); // fake bit
            packet.ReadBit(); // fake bit
            var hasTransportPosition = packet.ReadBit();
            var bit3E8 = packet.ReadBit(); // something with scene object
            var hasAnimKits = packet.ReadBit();
            var hasStationaryPosition = packet.ReadBit();
            var hasAttackingTarget = packet.ReadBit();
            var bit28E = packet.ReadBit();
            var transportFrames = packet.ReadBits(22);
            packet.ReadBit();
            packet.ReadBit();

            if (hasTransportPosition)
            {
                packet.StartBitStream(transportGuid, 3, 5, 2, 1, 4);
                hasTransportTime3 = packet.ReadBit();
                hasTransportTime2 = packet.ReadBit();
                packet.StartBitStream(transportGuid, 0, 6, 7);
            }

            if (living)
            {
                hasTransportData = packet.ReadBit();
                packet.ReadBit();
                packet.ReadBit();
                bits168 = packet.ReadBits(19);
                guid1[1] = packet.ReadBit();
                hasMoveFlagsExtra = !packet.ReadBit();
                packet.ReadBit();
                hasPitch = !packet.ReadBit(); // ???
                if (hasMoveFlagsExtra)
                    moveInfo.FlagsExtra = packet.ReadEnum<MovementFlagExtra>("Extra Movement Flags", 13, index);

                hasOrientation = !packet.ReadBit();
                hasTimestamp = !packet.ReadBit();
                hasMovementFlags = !packet.ReadBit();
                bitA8 = !packet.ReadBit();
                guid1[2] = packet.ReadBit();
                guid1[6] = packet.ReadBit();
                hasFallData = packet.ReadBit();
                guid1[5] = packet.ReadBit();
                guid1[4] = packet.ReadBit();
                guid1[0] = packet.ReadBit();

                if (hasMovementFlags)
                    moveInfo.Flags = packet.ReadEnum<MovementFlag>("Movement Flags", 30, index);
                bitA8 = packet.ReadBit();
                if (hasFallData)
                    hasFallDirection = packet.ReadBit();
                bits98 = packet.ReadBits(22);

                guid1[7] = packet.ReadBit();
                moveInfo.HasSplineData = packet.ReadBit();
                guid1[3] = packet.ReadBit();


                if (moveInfo.HasSplineData)
                {
                    hasFullSpline = packet.ReadBit();
                    if (hasFullSpline)
                    {
                        hasSplineVerticalAcceleration = packet.ReadBit();
                        packet.ReadEnum<SplineMode>("Spline Mode", 2, index);
                        splineCount = packet.ReadBits(20);
                        packet.ReadEnum<SplineFlag434>("Spline flags", 25, index);

                        hasSplineStartTime = packet.ReadBit();
                        bit134 = packet.ReadBit();
                        if (bit134)
                        {
                            bits138 = packet.ReadBits(21);
                            packet.ReadBits(2);
                        }
                    }
                }
            }

            if (hasAttackingTarget)
                attackingTargetGuid = packet.StartBitStream(4, 6, 3, 5, 0, 2, 7, 1);


            packet.ResetBitReader();

            if (living)
            {
                if (hasFallData)
                {
                    if (hasFallDirection)
                    {
                        packet.ReadSingle("Jump Sin Angle", index);
                        packet.ReadSingle("Jump Cos Angle", index);
                        packet.ReadSingle("Jump XY Speed", index);
                    }

                    packet.ReadSingle("Jump Z Speed", index);
                    packet.ReadUInt32("Jump Fall Time", index);
                }
                if (moveInfo.HasSplineData)
                {
                    if (hasFullSpline)
                    {
                        packet.ReadSingle("Duration Mod Next", index);
                        for (uint i = 0; i < splineCount; i++)
                        {
                            Vector3 v = new Vector3();
                            v.Z = packet.ReadSingle();
                            v.Y = packet.ReadSingle();
                            v.X = packet.ReadSingle();

                            packet.WriteLine("[{0}] " + v.ToString(), index);
                        }
                        SplineType spType = packet.ReadEnum<SplineType>("Spline Type", 8, index);
                        packet.ReadSingle("Duration Mod", index);

                        //    NYI block here

                        if (splineType == SplineType.FacingSpot)
                            packet.ReadVector3("Facing spot", index);

                        if (hasSplineVerticalAcceleration)
                            packet.ReadSingle("Spline Vertical Acceleration", index);

                        if (splineType == SplineType.FacingAngle)
                            packet.ReadSingle("Facing Angle", index);

                        packet.ReadUInt32("Spline FULL Time", index);
                        if (hasSplineStartTime)
                            packet.ReadUInt32("Spline Start time", index);

                        packet.ReadUInt32("Spline Time", index);
                    }

                    packet.ReadUInt32("Spline ID", index);
                    packet.ReadVector3("Spline Endpoint", index);
                }

                /*for (var i = 0; i < 10; ++i)
                    packet.ReadSingle("unk float");
                if (bits98 > 0)
                    packet.ReadBits((int)bits98);*/
                //for (var i = 0; i < bits98; ++i)
                //    packet.ReadInt32("Int9C", index, i);

                //if (bitA8)
                //    packet.ReadInt32("IntA8", index);

                //if (hasPitch)
                //    packet.ReadSingle("Pitch", index);

                //if (hasSplineElevation)
                //    packet.ReadSingle("Spline Elevation", index);



                moveInfo.Position.Z = packet.ReadSingle();
                moveInfo.Position.Y = packet.ReadSingle();
                packet.ReadSingle("Fly Speed", index);
                packet.ReadXORByte(guid1, 6);
                packet.ReadSingle("Fly Back Speed", index);
                moveInfo.Position.X = packet.ReadSingle();
                packet.WriteLine("[{0}] " + moveInfo.Position.ToString(), index);
                packet.ReadXORByte(guid1, 2);
                packet.ReadSingle("Swim Speed", index);
                packet.ReadXORByte(guid1, 1);
                packet.ReadSingle("Run Back Speed", index);
                packet.ReadSingle("Swim Back Speed", index);
                packet.ReadXORByte(guid1, 5);
                packet.ReadSingle("Turn Speed", index);
                packet.ReadXORByte(guid1, 3);
                moveInfo.RunSpeed = packet.ReadSingle("Run Speed", index) / 7.0f;
                packet.ReadXORByte(guid1, 7);
                moveInfo.WalkSpeed = packet.ReadSingle("Walk Speed", index) / 2.5f;
                packet.ReadSingle("Pitch Speed", index);

                if (hasTimestamp)
                    packet.ReadUInt32("Time?", index);

                packet.ReadXORByte(guid1, 4);
                packet.ReadXORByte(guid1, 0);
                packet.WriteLine("[{0}] GUID2: {1}", index, new Guid(BitConverter.ToUInt64(guid1, 0)));


                if (hasOrientation)
                    moveInfo.Orientation = packet.ReadSingle("Orientation", index);
            }

            if (hasAttackingTarget)
            {
                packet.ParseBitStream(attackingTargetGuid, 5, 1, 2, 0, 3, 4, 6, 7);
                packet.WriteGuid("Attacking Target GUID", attackingTargetGuid, index);
            }

            if (hasAnimKits)
            {
                hasAnimKit1 = !packet.ReadBit();
                hasAnimKit2 = !packet.ReadBit();
                hasAnimKit3 = !packet.ReadBit();
            }

            if (hasStationaryPosition)
            {
                moveInfo.Orientation = packet.ReadSingle("Stationary Orientation", index);
                moveInfo.Position = packet.ReadVector3("Stationary Position", index);
            }

            if (hasTransportData)
            {

                packet.ReadSByte("Transport Seat", index);
                moveInfo.TransportOffset.X = packet.ReadSingle();
                packet.ReadXORByte(transportGuid, 1);
                packet.ReadXORByte(transportGuid, 0);
                packet.ReadXORByte(transportGuid, 2);
                packet.ReadXORByte(transportGuid, 6);
                packet.ReadXORByte(transportGuid, 5);
                packet.ReadXORByte(transportGuid, 4);
                if (hasTransportTime2)
                    packet.ReadUInt32("Transport Time 2", index);
                packet.ReadXORByte(transportGuid, 7);
                moveInfo.TransportOffset.O = packet.ReadSingle();
                moveInfo.TransportOffset.Z = packet.ReadSingle();
                moveInfo.TransportOffset.Y = packet.ReadSingle();
                if (hasTransportTime3)
                    packet.ReadUInt32("Transport Time 3", index);
                packet.ReadXORByte(transportGuid, 3);
                packet.ReadUInt32("Transport Time", index);

                moveInfo.TransportGuid = new Guid(BitConverter.ToUInt64(transportGuid, 0));
                packet.WriteLine("[{0}] Transport GUID {1}", index, moveInfo.TransportGuid);
                packet.WriteLine("[{0}] Transport Position: {1}", index, moveInfo.TransportOffset);
            }

            if (hasGameObjectRotation)
                packet.ReadPackedQuaternion("GameObject Rotation", index);

            if (hasVehicleData)
            {
                moveInfo.VehicleId = packet.ReadUInt32("Vehicle Id", index);
                packet.ReadSingle("Vehicle Orientation", index);
            }

            if (hasAnimKits)
            {
                if (hasAnimKit1)
                    packet.ReadUInt16("Anim Kit 1", index);
                if (hasAnimKit2)
                    packet.ReadUInt16("Anim Kit 2", index);
                if (hasAnimKit3)
                    packet.ReadUInt16("Anim Kit 3", index);
            }

            if (splineType == SplineType.FacingTarget)
            {

                var facingTargetGuid = new byte[8];
                packet.StartBitStream(facingTargetGuid, 6, 7, 3, 0, 5, 1, 4, 2);
                packet.ParseBitStream(facingTargetGuid, 4, 2, 5, 6, 0, 7, 1, 3);
                packet.WriteGuid("Facing Target GUID", facingTargetGuid, index);
            }
            return moveInfo;
        }
    }
}

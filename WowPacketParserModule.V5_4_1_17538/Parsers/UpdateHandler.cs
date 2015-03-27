using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_1_17538.Parsers
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

                packet.AddValue("UpdateType", typeString, i);
                switch (typeString)
                {
                    case "Values":
                    {
                        var guid = packet.ReadPackedGuid("GUID", i);

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
                        var guid = packet.ReadPackedGuid("GUID", i);
                        ReadCreateObjectBlock(packet, guid, map, i);
                        break;
                    }
                    case "DestroyObjects":
                    {
                        CoreParsers.UpdateHandler.ReadObjectsBlock(packet, i);
                        break;
                    }
                }
            }
        }

        private static void ReadCreateObjectBlock(Packet packet, WowGuid guid, uint map, object index)
        {
            var objType = packet.ReadByteE<ObjectType>("Object Type", index);
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

            var guid1 = new byte[8];
            var transportGuid = new byte[8];
            var goTransportGuid = new byte[8];
            var attackingTargetGuid = new byte[8];

            var hasAnimKit1 = false;
            var hasAnimKit2 = false;
            var hasAnimKit3 = false;
            var hasFullSpline = false;
            var hasSplineStartTime = false;
            var hasSplineVerticalAcceleration = false;
            var hasGOTransportTime2 = false;
            var hasGOTransportTime3 = false;
            var hasSplineElevation = false;
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

            var bit130 = false;
            var bitA0 = false;
            var bit23C = false;
            var bit298 = false;
            var bit230 = false;
            var bit284 = false;
            var bit228 = false;
            var bit258 = false;
            var bit218 = false;
            var bit220 = false;

            var bits160 = 0u;
            var splineCount = 0u;
            var bits11C = 0u;
            var bits90 = 0u;
            var bits25C = 0u;
            var bits26C = 0u;
            var bits288 = 0u;
            var bits418 = 0u;
            var bits2AA = 0u;
            SplineType splineType = SplineType.Stop;

            var bit2A9 = packet.ReadBit();
            var bit29C = packet.ReadBit();
            packet.ReadBit(); // fake 1
            var bit1DC = packet.ReadBit();
            var bit2A4 = packet.ReadBit();
            var bit2A8 = packet.ReadBit();
            packet.ReadBit(); // fake 0
            var hasAttackingTarget = packet.ReadBit("Has Attacking Target", index);
            var transportFrames = packet.ReadBits("Transport Frames Count", 22, index);
            var bit414 = packet.ReadBit();
            packet.ReadBit(); // fake 3
            var bit428 = packet.ReadBit();
            var bit32A = packet.ReadBit();
            var hasStationaryPosition = packet.ReadBit("Has Stationary Position", index);
            packet.ReadBit(); // fake 2
            var isLiving = packet.ReadBit("Is Living", index);
            var hasAnimKits = packet.ReadBit("Has Anim Kits", index);
            var hasVehicleData = packet.ReadBit("Has Vehicle Data", index);
            var hasGameObjectPosition = packet.ReadBit("Has GameObject Position", index);
            var hasSceneObjectData = packet.ReadBit("Has Scene Object Data", index);
            var hasGameObjectRotation = packet.ReadBit("Has GameObject Rotation", index);

            if (bit29C)
            {
                bit284 = packet.ReadBit();
                if (bit284)
                {
                    bits25C = packet.ReadBits(21);
                    bits26C = packet.ReadBits(21);
                }

                packet.ReadBit("bit20C", index);
                packet.ReadBit("bit210", index);
                bit23C = packet.ReadBit();
                bit298 = packet.ReadBit();
                packet.ReadBit("bit20F", index);
                packet.ReadBit("bit20E", index);
                bit218 = packet.ReadBit();
                bit220 = packet.ReadBit();
                if (bit298)
                    bits288 = packet.ReadBits(20);

                bit228 = packet.ReadBit();
                packet.ReadBit("bit20D", index);
                bit230 = packet.ReadBit();
                bit258 = packet.ReadBit();
            }

            if (bit428)
                bits418 = packet.ReadBits(22);

            if (isLiving)
            {
                guid1[4] = packet.ReadBit();
                guid1[1] = packet.ReadBit();
                bits160 = packet.ReadBits(19);
                for (var i = 0; i < bits160; ++i)
                    packet.ReadBits("bits164", 2, index, i);

                guid1[5] = packet.ReadBit();
                hasOrientation = !packet.ReadBit();
                guid1[7] = packet.ReadBit();
                bits90 = packet.ReadBits(22);
                moveInfo.HasSplineData = packet.ReadBit();
                hasSplineElevation = !packet.ReadBit();
                if (moveInfo.HasSplineData)
                {
                    hasFullSpline = packet.ReadBit();
                    if (hasFullSpline)
                    {
                        bit130 = packet.ReadBit();
                        hasSplineVerticalAcceleration = packet.ReadBit("has Spline Vertical Acceleration", index);
                        packet.ReadBitsE<SplineMode>("Spline Mode", 2, index);
                        if (bit130)
                        {
                            bits11C = packet.ReadBits(21);
                            packet.ReadBits("bits12C", 2, index);
                        }
                        packet.ReadBitsE<SplineFlag434>("Spline flags", 25, index);
                        hasSplineStartTime = packet.ReadBit("Has Spline Start Time", index);
                        splineCount = packet.ReadBits("SplineWaypointsCount", 20, index);
                    }
                }

                bitA0 = !packet.ReadBit();
                guid1[3] = packet.ReadBit();
                packet.ReadBit("bitA4", index);
                hasMovementFlags = !packet.ReadBit();
                if (hasMovementFlags)
                    moveInfo.Flags = packet.ReadBitsE<MovementFlag>("Movement Flags", 30, index);

                packet.ReadBit("bit8C", index);
                packet.ReadBit("bit8D", index);
                guid1[2] = packet.ReadBit();
                hasTimestamp = !packet.ReadBit();
                guid1[0] = packet.ReadBit();
                hasTransportData = packet.ReadBit();
                if (hasTransportData)
                {
                    packet.StartBitStream(transportGuid, 4, 7, 3, 1, 6);
                    hasTransportTime2 = packet.ReadBit();
                    packet.StartBitStream(transportGuid, 2, 0, 5);
                    hasTransportTime3 = packet.ReadBit();
                }

                guid1[6] = packet.ReadBit();
                hasFallData = packet.ReadBit();
                if (hasFallData)
                    hasFallDirection = packet.ReadBit();

                hasMoveFlagsExtra = !packet.ReadBit();
                hasPitch = !packet.ReadBit();
                if (hasMoveFlagsExtra)
                    moveInfo.FlagsExtra = packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13, index);
            }

            if (hasGameObjectPosition)
            {
                packet.StartBitStream(goTransportGuid, 0, 7);
                hasGOTransportTime3 = packet.ReadBit();
                goTransportGuid[1] = packet.ReadBit();
                hasGOTransportTime2 = packet.ReadBit();
                packet.StartBitStream(goTransportGuid, 6, 5, 4, 3, 2);
            }

            if (bit32A)
                bits2AA = packet.ReadBits(7);

            if (hasAttackingTarget)
                packet.StartBitStream(attackingTargetGuid, 5, 4, 6, 0, 1, 7, 2, 3);

            // Sceneobject data
            var CreatorGUID = new byte[8];
            byte[][] guid358 = null;
            byte[][][] guid358_6 = null;
            uint[] bits358_10 = null;
            uint[][] bits358_6_84 = null;
            uint[][] bits358_6_94 = null;
            uint[][] bits358_6_31 = null;
            uint[][] bits358_6_74 = null;
            byte[][][] bit358_6_78_9 = null;
            byte[][][] bit358_6_88_10 = null;
            bool[][][] bit358_6_88_C = null;
            bool[][][] bit358_6_88_8 = null;
            bool[][] bit358_6_2E = null;
            bool[][] bit358_6_30 = null;
            bool[] bit358_20 = null;
            bool[] bit358_C = null;
            bool[] bit358_24 = null;

            var bit338 = false;
            var bit330 = false;
            var bit341 = false;
            var bit340 = false;
            var bit332 = false;
            var bit33C = false;

            uint[] bits388 = null;
            uint[] bits388_10 = null;
            byte[][] bit388_1_10 = null;
            bool[][] bit388_1_C = null;
            bool[][] bit388_1_8 = null;

            if (hasSceneObjectData)
            {
                guid358 = new byte[2][];
                guid358_6 = new byte[2][][];
                bits358_10 = new uint[2];
                bits358_6_84 = new uint[2][];
                bits358_6_94 = new uint[2][];
                bits358_6_31 = new uint[2][];
                bits358_6_74 = new uint[2][];
                bit358_6_78_9 = new byte[2][][];
                bit358_6_88_10 = new byte[2][][];
                bit358_6_88_C = new bool[2][][];
                bit358_6_88_8 = new bool[2][][];
                bit358_6_2E = new bool[2][];
                bit358_6_30 = new bool[2][];
                bit358_20 = new bool[2];
                bit358_C = new bool[2];
                bit358_24 = new bool[2];

                for (var i = 0; i < 2; ++i)
                {
                    guid358[i] = new byte[8];
                    packet.StartBitStream(guid358[i], 3, 4, 1, 0);
                    bits358_10[i] = packet.ReadBits(2);
                    bit358_20[i] = !packet.ReadBit();

                    guid358_6[i] = new byte[bits358_10[i]][];
                    bits358_6_84[i] = new uint[bits358_10[i]];
                    bits358_6_94[i] = new uint[bits358_10[i]];
                    bits358_6_31[i] = new uint[bits358_10[i]];
                    bits358_6_74[i] = new uint[bits358_10[i]];
                    bit358_6_78_9[i] = new byte[bits358_10[i]][];
                    bit358_6_88_10[i] = new byte[bits358_10[i]][];
                    bit358_6_88_C[i] = new bool[bits358_10[i]][];
                    bit358_6_88_8[i] = new bool[bits358_10[i]][];
                    bit358_6_2E[i] = new bool[bits358_10[i]];
                    bit358_6_30[i] = new bool[bits358_10[i]];

                    for (var j = 0; j < bits358_10[i]; ++j)
                    {
                        guid358_6[i][j] = new byte[8];
                        bits358_6_84[i][j] = packet.ReadBits(21);

                        bit358_6_88_8[i][j] = new bool[bits358_6_84[i][j]];
                        bit358_6_88_C[i][j] = new bool[bits358_6_84[i][j]];
                        bit358_6_88_10[i][j] = new byte[bits358_6_84[i][j]];
                        for (var k = 0; k < bits358_6_84[i][j]; ++k)
                        {
                            bit358_6_88_10[i][j][k] = (byte)(10 - packet.ReadBit());
                            bit358_6_88_C[i][j][k] = !packet.ReadBit();
                            bit358_6_88_8[i][j][k] = !packet.ReadBit();
                        }

                        packet.StartBitStream(guid358_6[i][j], 5, 3, 4, 1, 0, 6, 7);
                        bits358_6_31[i][j] = packet.ReadBits(7);
                        guid358_6[i][j][2] = packet.ReadBit();
                        bit358_6_2E[i][j] = !packet.ReadBit();
                        bits358_6_74[i][j] = packet.ReadBits(20);

                        bit358_6_78_9[i][j] = new byte[bits358_6_74[i][j]];
                        for (var k = 0; k < bits358_6_74[i][j]; ++k)
                            bit358_6_78_9[i][j][k] = (byte)(10 - packet.ReadBit());

                        bit358_6_30[i][j] = packet.ReadBit();
                        bits358_6_94[i][j] = packet.ReadBits(21);
                    }

                    guid358[i][6] = packet.ReadBit();
                    bit358_C[i] = !packet.ReadBit();
                    packet.StartBitStream(guid358[i], 2, 5);
                    bit358_24[i] = !packet.ReadBit();
                    guid358[i][7] = packet.ReadBit();
                }

                bit330 = !packet.ReadBit();
                bit340 = !packet.ReadBit();

                bits388 = new uint[3];
                bits388_10 = new uint[3];
                bit388_1_10 = new byte[3][];
                bit388_1_C = new bool[3][];
                bit388_1_8 = new bool[3][];

                for (var i = 0; i < 3; ++i)
                {
                    bits388_10[i] = packet.ReadBits(21);
                    bits388[i] = packet.ReadBits(21);
                    bit388_1_10[i] = new byte[bits388[i]];
                    bit388_1_C[i] = new bool[bits388[i]];
                    bit388_1_8[i] = new bool[bits388[i]];

                    for (var j = 0; j < bits388[i]; ++j)
                    {
                        bit388_1_8[i][j] = !packet.ReadBit();
                        bit388_1_10[i][j] = (byte)(10 - packet.ReadBit());
                        bit388_1_C[i][j] = !packet.ReadBit();
                    }
                }

                bit33C = !packet.ReadBit();
                bit341 = !packet.ReadBit();
                bit338 = !packet.ReadBit();
                bit332 = !packet.ReadBit();
                packet.ReadBit(); // fake bit
                packet.ReadBit("bit351", index);
                packet.ReadBit("bit350", index);
                packet.StartBitStream(CreatorGUID, 2, 7, 0, 5, 1, 3, 6, 4);
            }

            if (hasAnimKits)
            {
                hasAnimKit3 = !packet.ReadBit();
                hasAnimKit1 = !packet.ReadBit();
                hasAnimKit2 = !packet.ReadBit();
            }

            packet.ResetBitReader();

            for (var i = 0; i < transportFrames; ++i)
                packet.ReadInt32("Transport frame", index, i);

            if (hasSceneObjectData)
            {
                for (var i = 0; i < 2; ++i)
                {
                    for (var j = 0; j < bits358_10[i]; ++j)
                    {
                        for (var k = 0; k < bits358_6_74[i][j]; ++k)
                        {
                            packet.ReadInt32("int358+6+78+0", index, i, j, k);
                            packet.ReadInt16("short358+6+78+3", index, i, j, k);
                            packet.ReadByte("byte358+6+78+8", index, i, j, k);
                            packet.ReadInt16("short358+6+78+2", index, i, j, k);
                            if (bit358_6_78_9[i][j][k] != 9)
                                packet.ReadByte("byte358+6+78+9", index, i, j, k);
                        }

                        packet.ReadXORByte(guid358_6[i][j], 6);
                        for (var k = 0; k < bits358_6_84[i][j]; ++k)
                        {
                            packet.ReadInt32("int358+6+88+4", index, i, j, k);
                            packet.ReadInt32("int358+6+88+0", index, i, j, k);
                            if (bit358_6_88_8[i][j][k])
                                packet.ReadInt32("int358+6+88+8", index, i, j, k);

                            if (bit358_6_88_C[i][j][k])
                                packet.ReadInt32("int358+6+88+C", index, i, j, k);

                            if (bit358_6_88_10[i][j][k] != 9)
                                packet.ReadByte("byte358+6+88+10", index, i, j, k);
                        }

                        packet.ReadInt32("int358+6+28", index, i, j);
                        packet.ReadXORBytes(guid358_6[i][j], 5, 3);
                        packet.ReadInt32("int358+6+24", index, i, j);
                        packet.ReadXORByte(guid358_6[i][j], 1);
                        packet.ReadInt32("Int358+6+8", index, i, j);
                        packet.ReadInt16("short358+6+2C", index, i, j);
                        packet.ReadInt32("int358+6+C", index, i, j);
                        if (!bit358_6_30[i][j])
                            packet.ReadByte("byte358+6+30", index, i, j);

                        for (var k = 0; k < bits358_6_94[i][j]; ++k)
                        {
                            packet.ReadInt32("int358+6+98+4", index, i, j, k);
                            packet.ReadInt32("int358+6+98+0", index, i, j, k);
                        }

                        packet.ReadXORByte(guid358_6[i][j], 0);
                        packet.ReadWoWString("String358+6+31", (int)bits358_6_31[i][j], index, i, j);
                        packet.ReadInt32("int358+6+10", index, i, j);
                        packet.ReadInt16("short358+6+16", index, i, j);
                        packet.ReadInt16("short358+6+14", index, i, j);
                        packet.ReadXORByte(guid358_6[i][j], 2);
                        packet.ReadInt32("int358+6+18", index, i, j);
                        packet.ReadXORByte(guid358_6[i][j], 4);

                        if (bit358_6_2E[i][j])
                            packet.ReadInt16("short358+6+2E", index, i, j);

                        packet.ReadInt32("int358+6+1C", index, i, j);
                        packet.ReadInt32("int358+6+20", index, i, j);
                        packet.ReadXORByte(guid358_6[i][j], 7);

                        packet.WriteGuid("Guid 358_6", guid358_6[i][j], index, i, j);
                    }

                    packet.ReadXORBytes(guid358[i], 0, 7);
                    packet.ReadByte("byte358+25", index, i);
                    if (bit358_24[i])
                        packet.ReadInt32("int358+C", index, i);

                    packet.ReadXORBytes(guid358[i], 5, 4);
                    if (!bit358_20[i])
                        packet.ReadByte("byte358+24", index, i);

                    if (bit358_C[i])
                        packet.ReadInt16("Short358+10", index, i);

                    packet.ReadXORBytes(guid358[i], 1, 2, 3, 6);
                    packet.ReadInt32("int358+8", index, i);
                    packet.WriteGuid("Guid358", guid358[i], index, i);
                }

                for (var i = 0; i < 3; ++i)
                {
                    for (var j = 0; j < bits388_10[i]; ++j)
                    {
                        packet.ReadInt32("int388+6+4", index, i, j);
                        packet.ReadInt32("int388+6+0", index, i, j);
                    }

                    for (var j = 0; j < bits388[i]; ++j)
                    {
                        packet.ReadInt32("int388+1+0", index, i, j);
                        if (bit388_1_C[i][j])
                            packet.ReadInt32("int388+1+C", index, i, j);

                        if (bit388_1_8[i][j])
                            packet.ReadInt32("int388+1+8", index, i, j);

                        if (bit388_1_10[i][j] != 9)
                            packet.ReadByte("byte388+1+10", index, i, j);

                        packet.ReadInt32("int388+1+4", index, i, j);
                    }
                }

                if (bit338)
                    packet.ReadInt32("Int338", index);
                packet.ParseBitStream(CreatorGUID, 3, 5, 4, 2, 7, 6, 0, 1);

                if (bit33C)
                    packet.ReadInt32("Int33C", index);

                if (bit341)
                    packet.ReadByte("Byte341", index);

                if (bit340)
                    packet.ReadByte("Byte340", index);

                packet.ReadInt32("Int334", index);
                if (bit332)
                    packet.ReadInt16("Short332", index);

                if (bit330)
                    packet.ReadInt16("Short318", index);

                packet.WriteGuid("Creator GUID", CreatorGUID, index);
            }

            if (bit29C)
            {
                if (bit258)
                {
                    packet.ReadSingle("Float250", index);
                    packet.ReadSingle("Float254", index);
                    packet.ReadSingle("Float248", index);
                    packet.ReadSingle("Float24C", index);
                    packet.ReadSingle("Float240", index);
                    packet.ReadSingle("Float244", index);
                }

                if (bit284)
                {
                    packet.ReadSingle("Float280", index);
                    for (var i = 0; i < bits26C; ++i)
                    {
                        packet.ReadSingle("Float270+1", index, i);
                        packet.ReadSingle("Float270+0", index, i);
                    }

                    packet.ReadSingle("Float27C", index);
                    for (var i = 0; i < bits25C; ++i)
                    {
                        packet.ReadSingle("Float260+0", index, i);
                        packet.ReadSingle("Float260+1", index, i);
                    }
                }

                if (bit230)
                    packet.ReadInt32("int22C", index);

                if (bit220)
                    packet.ReadInt32("int21C", index);

                if (bit23C)
                {
                    packet.ReadSingle("Float238", index);
                    packet.ReadSingle("Float234", index);
                }

                if (bit298)
                {
                    for (var i = 0; i < bits288; ++i)
                    {
                        packet.ReadSingle("Float28C+0", index, i);
                        packet.ReadSingle("Float28C+2", index, i);
                        packet.ReadSingle("Float28C+1", index, i);
                    }
                }

                packet.ReadInt32("Int208", index);
                if (bit228)
                    packet.ReadInt32("int224", index);

                if (bit218)
                    packet.ReadInt32("int214", index);
            }

            if (isLiving)
            {
                moveInfo.Position.Y = packet.ReadSingle();
                if (moveInfo.HasSplineData)
                {
                    if (hasFullSpline)
                    {
                        for (var i = 0u; i < splineCount; ++i)
                        {
                            var wp = new Vector3
                            {
                                Z = packet.ReadSingle(),
                                X = packet.ReadSingle(),
                                Y = packet.ReadSingle()
                            };

                            packet.AddValue("Spline Waypoint", wp, index, i);
                        }

                        packet.ReadInt32("Spline Time", index); // if need swap with "Spline Full Time"
                        if (hasSplineVerticalAcceleration)
                            packet.ReadSingle("Spline Vertical Acceleration", index);

                        if (bit130)
                        {
                            for (var i = 0; i < bits11C; ++i)
                            {
                                packet.ReadSingle("Float13C+1", index, i);
                                packet.ReadSingle("Float13C+0", index, i);
                            }
                        }

                        packet.ReadInt32("Spline Full Time", index);

                        if (hasSplineStartTime)
                            packet.ReadInt32("Spline Start Time", index);

                        var type = packet.ReadByte();
                        switch (type)
                        {
                            case 1:
                                splineType = SplineType.Normal;
                                break;
                            case 2:
                                splineType = SplineType.FacingSpot;
                                break;
                            case 3:
                                splineType = SplineType.FacingTarget;
                                break;
                            case 4:
                                splineType = SplineType.FacingAngle;
                                break;
                        }

                        packet.ReadSingle("Spline Duration Multiplier", index); // if need swap with "Spline Duration Multiplier Next"
                        packet.ReadSingle("Spline Duration Multiplier Next", index);

                        if (splineType == SplineType.FacingSpot)
                        {
                            var point = new Vector3
                            {
                                Y = packet.ReadSingle(),
                                Z = packet.ReadSingle(),
                                X = packet.ReadSingle()
                            };

                            packet.AddValue("Facing Spot", point, index);
                        }

                        if (splineType == SplineType.FacingAngle)
                            packet.ReadSingle("Facing Angle", index);
                    }

                    moveInfo.Position.Y = packet.ReadSingle();
                    moveInfo.Position.Z = packet.ReadSingle();
                    moveInfo.Position.X = packet.ReadSingle();
                    packet.ReadInt32("Spline Id", index);
                }

                packet.ReadSingle("FloatBC", index);
                packet.ReadSingle("FloatAC", index);
                packet.ReadXORByte(guid1, 4);
                packet.ReadSingle("FloatA8", index);

                if (hasFallData)
                {
                    if (hasFallDirection)
                    {
                        packet.ReadSingle("Jump Velocity", index);
                        packet.ReadSingle("Jump Sin", index);
                        packet.ReadSingle("Jump Cos", index);
                    }

                    packet.ReadUInt32("Time Fallen", index);
                    packet.ReadSingle("Fall Start Velocity", index);
                }

                if (hasTransportData)
                {
                    packet.ReadXORByte(transportGuid, 5);
                    var seat = packet.ReadByte("Transport Seat", index);
                    packet.ReadXORByte(transportGuid, 2);
                    moveInfo.TransportOffset.O = packet.ReadSingle();
                    packet.ReadXORBytes(transportGuid, 4, 7);
                    if (hasTransportTime2)
                        packet.ReadUInt32("Transport Time 2", index);

                    packet.ReadUInt32("Transport Time", index);
                    moveInfo.TransportOffset.Y = packet.ReadSingle();
                    packet.ReadXORBytes(transportGuid, 3, 6);
                    moveInfo.TransportOffset.X = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 0);
                    if (hasTransportTime3)
                        packet.ReadUInt32("Transport Time 3", index);
                    packet.ReadXORByte(transportGuid, 1);
                    moveInfo.TransportOffset.Z = packet.ReadSingle();

                    moveInfo.TransportGuid = new WowGuid64(BitConverter.ToUInt64(transportGuid, 0));
                    packet.AddValue("Transport GUID", moveInfo.TransportGuid, index);
                    packet.AddValue("Transport Position", moveInfo.TransportOffset, index);

                    if (moveInfo.TransportGuid.HasEntry() && moveInfo.TransportGuid.GetHighType() == HighGuidType.Vehicle &&
                        guid.HasEntry() && guid.GetHighType() == HighGuidType.Creature)
                    {
                        var vehicleAccessory = new VehicleTemplateAccessory();
                        vehicleAccessory.AccessoryEntry = guid.GetEntry();
                        vehicleAccessory.SeatId = seat;
                        Storage.VehicleTemplateAccessorys.Add(moveInfo.TransportGuid.GetEntry(), vehicleAccessory, packet.TimeSpan);
                    }
                }

                packet.ReadXORByte(guid1, 5);

                if (hasTimestamp)
                    packet.ReadUInt32("Time", index);

                for (var i = 0; i < bits90; ++i)
                    packet.ReadInt32("Int8C", index, i);

                if (bitA0)
                    packet.ReadInt32("Int98", index);

                packet.ReadXORByte(guid1, 1);
                packet.ReadSingle("FloatB0", index);
                packet.ReadSingle("FloatC0", index);
                packet.ReadXORByte(guid1, 6);
                packet.ReadSingle("FloatC4", index);

                for (var i = 0; i < bits160; ++i)
                {
                    packet.ReadSingle("Float16C+3", index, i);
                    packet.ReadSingle("Float16C+5", index, i);
                    packet.ReadSingle("Float16C+1", index, i);
                    packet.ReadInt32("Int16C+0", index, i);
                    packet.ReadInt32("Int16C+4", index, i);
                    packet.ReadSingle("Float16C+2", index, i);
                }

                moveInfo.Position.X = packet.ReadSingle();
                if (hasOrientation)
                    moveInfo.Orientation = packet.ReadSingle();

                packet.ReadSingle("FloatC8", index);
                packet.ReadSingle("FloatB4", index);
                if (hasSplineElevation)
                    packet.ReadSingle("Spline Elevation", index);

                packet.ReadXORByte(guid1, 3);
                if (hasPitch)
                    packet.ReadSingle("Pitch", index);

                packet.ReadSingle("FloatB8", index);
                packet.ReadXORBytes(guid1, 7, 2);
                moveInfo.Position.Z = packet.ReadSingle();
                packet.ReadXORByte(guid1, 0);

                packet.WriteGuid("GUID1", guid1, index);
                packet.AddValue("Position", moveInfo.Position, index);
                packet.AddValue("Orientation", moveInfo.Orientation, index);
            }

            if (hasStationaryPosition)
            {
                moveInfo.Position.X = packet.ReadSingle();
                moveInfo.Position.Z = packet.ReadSingle();
                moveInfo.Position.Y = packet.ReadSingle();
                moveInfo.Orientation = packet.ReadSingle("Stationary Orientation", index);

                packet.AddValue("Stationary Position", moveInfo.Position, index);
            }

            if (hasGameObjectPosition)
            {
                if (hasGOTransportTime2)
                    packet.ReadUInt32("GO Transport Time 2", index);

                packet.ReadXORBytes(goTransportGuid, 4, 2, 7, 3);
                packet.ReadUInt32("GO Transport Time", index);
                moveInfo.TransportOffset.Y = packet.ReadSingle();
                packet.ReadXORByte(goTransportGuid, 1);
                moveInfo.TransportOffset.Z = packet.ReadSingle();
                packet.ReadSByte("GO Transport Seat", index);
                if (hasGOTransportTime3)
                    packet.ReadUInt32("GO Transport Time 3", index);

                packet.ReadXORByte(goTransportGuid, 6);
                moveInfo.TransportOffset.O = packet.ReadSingle();
                packet.ReadXORBytes(goTransportGuid, 5, 0);
                moveInfo.TransportOffset.X = packet.ReadSingle();

                moveInfo.TransportGuid = new WowGuid64(BitConverter.ToUInt64(goTransportGuid, 0));
                packet.AddValue("GO Transport GUID", moveInfo.TransportGuid, index);
                packet.AddValue("GO Transport Position", moveInfo.TransportOffset, index);
            }

            if (hasGameObjectRotation)
                packet.ReadPackedQuaternion("GameObject Rotation", index);

            if (hasAttackingTarget)
            {
                packet.ParseBitStream(attackingTargetGuid, 4, 6, 3, 0, 7, 1, 2, 5);
                packet.WriteGuid("Attacking GUID", attackingTargetGuid, index);
            }

            if (hasAnimKits)
            {
                if (hasAnimKit3)
                    packet.ReadUInt16("Anim Kit 3", index);
                if (hasAnimKit1)
                    packet.ReadUInt16("Anim Kit 1", index);
                if (hasAnimKit2)
                    packet.ReadUInt16("Anim Kit 2", index);
            }

            if (bit1DC)
                packet.ReadInt32("int1D8", index);

            if (hasVehicleData)
            {
                moveInfo.VehicleId = packet.ReadUInt32("Vehicle Id", index);
                packet.ReadSingle("Vehicle Orientation", index);
            }

            if (bit2A4)
                packet.ReadInt32("int2A0", index);

            if (bit414)
                packet.ReadInt32("int410", index);

            if (bit32A)
                packet.ReadBytes("Bytes", (int)bits2AA);

            if (bit428)
                for (var i = 0; i < bits418; ++i)
                    packet.ReadInt32("Int3F8", index, i);

            if (isLiving && moveInfo.HasSplineData && hasFullSpline && splineType == SplineType.FacingTarget)
            {
                var facingTargetGuid = new byte[8];
                facingTargetGuid = packet.StartBitStream(5, 3, 6, 2, 7, 0, 1, 4);
                packet.ParseBitStream(facingTargetGuid, 7, 0, 1, 4, 2, 5, 3, 6);
                packet.WriteGuid("Facing Target GUID", facingTargetGuid, index);
            }

            return moveInfo;
        }
    }
}

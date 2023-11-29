using System;
using System.Linq;
using Google.Protobuf.WellKnownTypes;
using WowPacketParser.DBC;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.PacketStructures;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using MovementFlag = WowPacketParser.Enums.v4.MovementFlag;
using MovementFlag2 = WowPacketParser.Enums.v4.MovementFlag2;
using SplineFacingType = WowPacketParserModule.V6_0_2_19033.Enums.SplineFacingType;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class MovementHandler
    {
        public static void ReadMonsterSplineFilter(Packet packet, params object[] indexes)
        {
            var count = packet.ReadUInt32("MonsterSplineFilterKey", indexes);
            packet.ReadSingle("BaseSpeed", indexes);
            packet.ReadInt16("StartOffset", indexes);
            packet.ReadSingle("DistToPrevFilterKey", indexes);
            packet.ReadInt16("AddedToStart", indexes);

            for (int i = 0; i < count; i++)
            {
                packet.ReadInt16("IDx", indexes, i);
                packet.ReadUInt16("Speed", indexes, i);
            }

            packet.ResetBitReader();
            packet.ReadBits("FilterFlags", 2, indexes);
        }

        public static void ReadMonsterSplineSpellEffectExtraData(Packet packet, params object[] indexes)
        {
            packet.ReadPackedGuid128("TargetGUID", indexes);
            packet.ReadUInt32("SpellVisualID", indexes);
            packet.ReadUInt32("ProgressCurveID", indexes);
            packet.ReadUInt32("ParabolicCurveID", indexes);
            packet.ReadSingle("JumpGravity", indexes);
        }

        public static SplineJump ReadMonsterSplineJumpExtraData(Packet packet, params object[] indexes)
        {
            SplineJump jump = new();
            jump.Gravity = packet.ReadSingle("JumpGravity", indexes);
            jump.StartTime = packet.ReadUInt32("StartTime", indexes);
            jump.Duration = packet.ReadUInt32("Duration", indexes);
            return jump;
        }

        public static void ReadMovementSpline(Packet packet, Vector3 pos, params object[] indexes)
        {
            PacketMonsterMove monsterMove = packet.Holder.MonsterMove;
            monsterMove.Flags = packet.ReadUInt32E<SplineFlag>("Flags", indexes).ToUniversal();
            if (ClientVersion.RemovedInVersion(ClientType.Shadowlands))
            {
                packet.ReadByte("AnimTier", indexes);
                packet.ReadUInt32("TierTransStartTime", indexes);
            }
            monsterMove.ElapsedTime = packet.ReadInt32("Elapsed", indexes);
            monsterMove.MoveTime = packet.ReadUInt32("MoveTime", indexes);
            packet.ReadUInt32("FadeObjectTime", indexes);

            packet.ReadByte("Mode", indexes);
            if (ClientVersion.RemovedInVersion(ClientType.Shadowlands))
                packet.ReadByte("VehicleExitVoluntary", indexes);

            monsterMove.TransportGuid = packet.ReadPackedGuid128("TransportGUID", indexes);
            monsterMove.VehicleSeat = packet.ReadSByte("VehicleSeat", indexes);

            packet.ResetBitReader();

            var type = packet.ReadBitsE<SplineFacingType>("Face", 2, indexes);
            var pointsCount = packet.ReadBits("PointsCount", 16, indexes);
            if (ClientVersion.AddedInVersion(ClientType.Shadowlands))
            {
                packet.ReadBit("VehicleExitVoluntary", indexes);
                packet.ReadBit("Interpolate", indexes);
            }
            var packedDeltasCount = packet.ReadBits("PackedDeltasCount", 16, indexes);
            var hasSplineFilter = packet.ReadBit("HasSplineFilter", indexes);
            var hasSpellEffectExtraData = packet.ReadBit("HasSpellEffectExtraData", indexes);
            var hasJumpExtraData = packet.ReadBit("HasJumpExtraData", indexes);

            var hasAnimTier = false;
            if (ClientVersion.AddedInVersion(ClientType.Shadowlands))
                hasAnimTier = packet.ReadBit("HasAnimTierTransition", indexes);

            var hasUnk901 = false;
            if (ClientVersion.AddedInVersion(ClientType.Shadowlands) && !ClientVersion.IsClassicClientVersionBuild(ClientVersion.Build))
                hasUnk901 = packet.ReadBit("HasUnknown901", indexes);

            if (hasSplineFilter)
                ReadMonsterSplineFilter(packet, indexes, "MonsterSplineFilter");

            switch (type)
            {
                case SplineFacingType.Spot:
                    monsterMove.LookPosition = packet.ReadVector3("FaceSpot", indexes);
                    break;
                case SplineFacingType.Target:
                    SplineLookTarget lookTarget = monsterMove.LookTarget = new();
                    lookTarget.Orientation = packet.ReadSingle("FaceDirection", indexes);
                    lookTarget.Target = packet.ReadPackedGuid128("FacingGUID", indexes);
                    break;
                case SplineFacingType.Angle:
                    monsterMove.LookOrientation = packet.ReadSingle("FaceDirection", indexes);
                    break;
                default:
                    break;
            }

            Vector3 endpos = new Vector3();

            double overallDist = 0.0f;
            for (int i = 0; i < pointsCount; i++)
            {
                var spot = packet.ReadVector3();

                // euclidean distance
                overallDist += Math.Sqrt(Math.Pow(spot.X - pos.X, 2) + Math.Pow(spot.Y - pos.Y, 2) + Math.Pow(spot.Z - pos.Z, 2));

                // client always taking first point
                if (i == 0)
                    endpos = spot;

                monsterMove.Points.Add(spot);
                packet.AddValue("Points", spot, indexes, i);
            }

            var waypoints = new Vector3[packedDeltasCount];
            for (int i = 0; i < packedDeltasCount; i++)
            {
                var packedDeltas = packet.ReadPackedVector3();
                waypoints[i].X = packedDeltas.X;
                waypoints[i].Y = packedDeltas.Y;
                waypoints[i].Z = packedDeltas.Z;
            }

            if (hasSpellEffectExtraData)
                ReadMonsterSplineSpellEffectExtraData(packet, indexes, "MonsterSplineSpellEffectExtra");

            if (hasJumpExtraData)
                monsterMove.Jump = ReadMonsterSplineJumpExtraData(packet, indexes, "MonsterSplineJumpExtraData");

            if (hasAnimTier)
            {
                packet.ReadInt32("TierTransitionID", indexes);
                packet.ReadUInt32("StartTime", indexes);
                packet.ReadUInt32("EndTime", indexes);
                packet.ReadByte("AnimTier", indexes);
            }

            if (hasUnk901)
            {
                for (var i = 0; i < 16; ++i)
                {
                    packet.ReadInt32("Unknown1", indexes, "Unknown901", i);
                    packet.ReadInt32("Unknown2", indexes, "Unknown901", i);
                    packet.ReadInt32("Unknown3", indexes, "Unknown901", i);
                    packet.ReadInt32("Unknown4", indexes, "Unknown901", i);
                }
            }

            // Calculate mid pos
            var mid = new Vector3
            {
                X = (pos.X + endpos.X) * 0.5f,
                Y = (pos.Y + endpos.Y) * 0.5f,
                Z = (pos.Z + endpos.Z) * 0.5f
            };

            for (var i = 0; i < packedDeltasCount; ++i)
            {
                var vec = new Vector3
                {
                    X = mid.X - waypoints[i].X,
                    Y = mid.Y - waypoints[i].Y,
                    Z = mid.Z - waypoints[i].Z
                };
                monsterMove.PackedPoints.Add(vec);
                packet.AddValue("WayPoints", vec, indexes, i);
            }

            float moveTimeInSec = (float)monsterMove.MoveTime / 1000;
            float speedXY = (float)overallDist / moveTimeInSec;
            packet.AddValue("CalculatedSpeedXY", speedXY, indexes);
        }

        public static void ReadMovementMonsterSpline(Packet packet, Vector3 pos, params object[] indexes)
        {
            PacketMonsterMove monsterMove = packet.Holder.MonsterMove;
            monsterMove.Id = packet.ReadUInt32("Id", indexes);
            monsterMove.Destination = packet.ReadVector3("Destination", indexes);

            packet.ResetBitReader();

            packet.ReadBit("CrzTeleport", indexes);
            packet.ReadBits("StopDistanceTolerance", 3, indexes);

            ReadMovementSpline(packet, pos, indexes, "MovementSpline");
        }

        [Parser(Opcode.SMSG_LOGIN_SET_TIME_SPEED)]
        public static void HandleLoginSetTimeSpeed(Packet packet)
        {
            PacketLoginSetTimeSpeed setTime = packet.Holder.LoginSetTimeSpeed = new();
            setTime.ServerTime = packet.ReadPackedTime("ServerTime").ToUniversalTime().ToTimestamp();
            setTime.GameTime = packet.ReadPackedTime("GameTime").ToUniversalTime().ToTimestamp();
            setTime.NewSpeed = packet.ReadSingle("NewSpeed");
            setTime.ServerTimeHolidayOffset = packet.ReadInt32("ServerTimeHolidayOffset");
            setTime.GameTimeHolidayOffset = packet.ReadInt32("GameTimeHolidayOffset");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD)]
        public static void HandleNewWorld(Packet packet)
        {
            WowPacketParser.Parsing.Parsers.MovementHandler.CurrentMapId = (uint)packet.ReadInt32<MapId>("Map");
            packet.ReadVector4("Position");
            packet.ReadUInt32("Reason");
            packet.ReadVector3("MovementOffset");

            packet.AddSniffData(StoreNameType.Map, (int)WowPacketParser.Parsing.Parsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }

        [Parser(Opcode.SMSG_ON_MONSTER_MOVE, ClientVersionBuild.V3_4_3_51505)]
        public static void HandleOnMonsterMove(Packet packet)
        {
            PacketMonsterMove monsterMove = packet.Holder.MonsterMove = new();
            monsterMove.Mover = packet.ReadPackedGuid128("MoverGUID");
            Vector3 pos = monsterMove.Position = packet.ReadVector3("Position");

            ReadMovementMonsterSpline(packet, pos, "MovementMonsterSpline");
        }
    }
}

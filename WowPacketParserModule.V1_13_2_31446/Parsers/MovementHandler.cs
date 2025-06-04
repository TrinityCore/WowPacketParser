using Google.Protobuf.WellKnownTypes;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.PacketStructures;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using SplineFacingType = WowPacketParserModule.V6_0_2_19033.Enums.SplineFacingType;

namespace WowPacketParserModule.V1_13_2_31446.Parsers
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
            var splineFlag = packet.ReadUInt32E<SplineFlag>("Flags", indexes);
            monsterMove.Flags = splineFlag.ToUniversal();

            monsterMove.ElapsedTime = packet.ReadInt32("Elapsed", indexes);
            monsterMove.MoveTime = packet.ReadUInt32("MoveTime", indexes);
            packet.ReadUInt32("FadeObjectTime", indexes);

            packet.ReadByte("Mode", indexes);
            monsterMove.TransportGuid = packet.ReadPackedGuid128("TransportGUID", indexes);
            monsterMove.VehicleSeat = packet.ReadSByte("VehicleSeat", indexes);

            packet.ResetBitReader();

            var type = packet.ReadBitsE<SplineFacingType>("Face", 2, indexes);
            var pointsCount = packet.ReadBits("PointsCount", 16, indexes);
            packet.ReadBit("VehicleExitVoluntary", indexes);
            packet.ReadBit("Interpolate", indexes);
            var packedDeltasCount = packet.ReadBits("PackedDeltasCount", 16, indexes);
            var hasSplineFilter = packet.ReadBit("HasSplineFilter", indexes);
            var hasSpellEffectExtraData = packet.ReadBit("HasSpellEffectExtraData", indexes);
            var hasJumpExtraData = packet.ReadBit("HasJumpExtraData", indexes);
            var hasAnimTier = packet.ReadBit("HasAnimTierTransition", indexes);

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

            var endpos = new Vector3();
            double distance = 0.0f;
            if (pointsCount > 0)
            {
                var prevpos = pos;
                for (var i = 0; i < pointsCount; ++i)
                {
                    var spot = packet.ReadVector3("Points", indexes, i);
                    monsterMove.Points.Add(spot);
                    distance += Vector3.GetDistance(prevpos, spot);
                    prevpos = spot;

                    // client always taking first point
                    if (i == 0)
                        endpos = spot;
                }
            }

            if (packedDeltasCount > 0)
            {
                // Calculate mid pos
                var mid = (pos + endpos) * 0.5f;

                // ignore distance set by Points array if packed deltas are used
                distance = 0;

                var prevpos = pos;
                for (var i = 0; i < packedDeltasCount; ++i)
                {
                    var vec = mid - packet.ReadPackedVector3();
                    packet.AddValue("WayPoints", vec, indexes, i);
                    monsterMove.PackedPoints.Add(vec);
                    distance += Vector3.GetDistance(prevpos, vec);
                    prevpos = vec;
                }
                distance += Vector3.GetDistance(prevpos, endpos);
            }

            if (hasSpellEffectExtraData)
                ReadMonsterSplineSpellEffectExtraData(packet, indexes, "MonsterSplineSpellEffectExtra");

            if (hasJumpExtraData)
            {
                monsterMove.Jump = ReadMonsterSplineJumpExtraData(packet, indexes, "MonsterSplineJumpExtraData");
            }

            if (hasAnimTier)
            {
                packet.ReadInt32("TierTransitionID", indexes);
                packet.ReadUInt32("StartTime", indexes);
                packet.ReadUInt32("EndTime", indexes);
                packet.ReadByte("AnimTier", indexes);
            }

            if (endpos.X != 0 && endpos.Y != 0 && endpos.Z != 0)
            {
                packet.AddValue("Computed Distance", distance, indexes);
                packet.AddValue("Computed Speed", (distance / monsterMove.MoveTime) * 1000, indexes);
            }
        }

        public static void ReadMovementMonsterSpline(Packet packet, Vector3 pos, params object[] indexes)
        {
            PacketMonsterMove monsterMove = packet.Holder.MonsterMove;
            monsterMove.Id = packet.ReadUInt32("Id", indexes);

            if (ClientVersion.RemovedInVersion(ClientBranch.Classic, ClientVersionBuild.V1_15_0_52146) ||
                ClientVersion.RemovedInVersion(ClientBranch.WotLK, ClientVersionBuild.V3_4_4_59817))
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
        }

        [Parser(Opcode.SMSG_ON_MONSTER_MOVE, ClientBranch.Classic, ClientVersionBuild.V1_15_0_52146)]
        public static void HandleOnMonsterMove(Packet packet)
        {
            PacketMonsterMove monsterMove = packet.Holder.MonsterMove = new();
            monsterMove.Mover = packet.ReadPackedGuid128("MoverGUID");
            Vector3 pos = monsterMove.Position = packet.ReadVector3("Position");

            ReadMovementMonsterSpline(packet, pos, "MovementMonsterSpline");
        }
    }
}

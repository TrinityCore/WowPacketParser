using System;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using WowPacketParser.DBC;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParserModule.V7_0_3_22248.Enums;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using SplineFacingType = WowPacketParserModule.V6_0_2_19033.Enums.SplineFacingType;
using SplineFlag = WowPacketParserModule.V7_0_3_22248.Enums.SplineFlag;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
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
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V10_2_0_52038))
                monsterMove.Destination = packet.ReadVector3("Destination", indexes);

            packet.ResetBitReader();

            packet.ReadBit("CrzTeleport", indexes);
            packet.ReadBits("StopDistanceTolerance", 3, indexes);

            ReadMovementSpline(packet, pos, indexes, "MovementSpline");
        }

        [Parser(Opcode.SMSG_ON_MONSTER_MOVE)]
        public static void HandleOnMonsterMove(Packet packet)
        {
            PacketMonsterMove monsterMove = packet.Holder.MonsterMove = new();
            monsterMove.Mover = packet.ReadPackedGuid128("MoverGUID");
            Vector3 pos = monsterMove.Position = packet.ReadVector3("Position");

            ReadMovementMonsterSpline(packet, pos, "MovementMonsterSpline");
        }

        [Parser(Opcode.SMSG_PHASE_SHIFT_CHANGE)]
        public static void HandlePhaseShift(Packet packet)
        {
            var phaseShift = packet.Holder.PhaseShift = new PacketPhaseShift();
            CoreParsers.MovementHandler.ClearPhases();
            phaseShift.Client = packet.ReadPackedGuid128("Client");
            // PhaseShiftData
            packet.ReadInt32E<PhaseShiftFlags>("PhaseShiftFlags");
            var count = packet.ReadInt32("PhaseShiftCount");
            phaseShift.PersonalGuid = packet.ReadPackedGuid128("PersonalGUID");
            for (var i = 0; i < count; ++i)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_0_52038))
                    packet.ReadUInt32E<PhaseFlags>("PhaseFlags", i);
                else
                    packet.ReadUInt16E<PhaseFlags>("PhaseFlags", i);

                var id = packet.ReadUInt16();
                phaseShift.Phases.Add(id);

                if (Settings.UseDBC && DBC.Phase.ContainsKey(id))
                {
                    packet.WriteLine($"[{i}] ID: {id} ({StoreGetters.GetName(StoreNameType.PhaseId, id, false)}) Flags: {(DBCPhaseFlags)DBC.Phase[id].Flags}");
                }
                else
                    packet.AddValue($"ID", id, i);

                CoreParsers.MovementHandler.ActivePhases.Add(id, true);
            }

            if (DBC.Phases.Any())
            {
                foreach (var phaseGroup in DBC.GetPhaseGroups(CoreParsers.MovementHandler.ActivePhases.Keys))
                    packet.WriteLine($"PhaseGroup: { phaseGroup } Phases: { string.Join(" - ", DBC.Phases[phaseGroup]) }");
            }
            var visibleMapIDsCount = packet.ReadInt32("VisibleMapIDsCount") / 2;
            for (var i = 0; i < visibleMapIDsCount; ++i)
                phaseShift.VisibleMaps.Add((uint)packet.ReadInt16<MapId>("VisibleMapID", i));
            var preloadMapIDCount = packet.ReadInt32("PreloadMapIDsCount") / 2;
            for (var i = 0; i < preloadMapIDCount; ++i)
                phaseShift.PreloadMaps.Add((uint)packet.ReadInt16<MapId>("PreloadMapID", i));
            var uiMapPhaseIdCount = packet.ReadInt32("UiMapPhaseIDsCount") / 2;
            for (var i = 0; i < uiMapPhaseIdCount; ++i)
                phaseShift.UiMapPhase.Add((uint)packet.ReadInt16("UiMapPhaseId", i));

            CoreParsers.MovementHandler.WritePhaseChanges(packet);
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_MOD_MOVEMENT_FORCE_MAGNITUDE)]
        public static void HandleMoveUpdateModMovementForceMagnitude(Packet packet)
        {
            Substructures.MovementHandler.ReadMovementStats(packet, "MovementStats");
            packet.ReadSingle("Speed");
        }
    }
}

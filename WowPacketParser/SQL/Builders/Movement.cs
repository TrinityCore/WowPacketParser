using System.Collections.Generic;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.PacketStructures;
using WowPacketParser.Parsing.Proto;
using WowPacketParser.Proto;

namespace WowPacketParser.SQL.Builders
{
    [ProtoBuilderClass]
    public class Movement : BaseProtoQueryBuilder
    {
        private struct Waypoint
        {
            public Vec3 Position { get; init; }
            public float Orientation { get; init; }
        }

        public enum PathType
        {
            None                    = 0, // pathfinding
            ExactPath               = 1, // sent fully
            ExactPathFlying         = 2, // sent fully + flying flag
            ExactPathFlyingCyclic   = 3, // sent fully + flying flag + cyclic flag
            ExactPathAndJump        = 4, // sent fully + parabolic movement at the end
            Invalid                 = 100,
        }

        private class Path
        {
            public List<Waypoint> Waypoints { get; init; }
            public PathType Type { get; init; }
        }

        private Dictionary<UniversalGuid, List<Path>> pathsPerGuid = new();

        public override bool IsEnabled() => Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature_movement) &&
                                            Settings.TargetedProject is TargetedProject.Cmangos or TargetedProject.TrinityCore;

        private PathType GetMovementType(PacketMonsterMove packet)
        {
            PathType flags = PathType.None;
            if (packet.Flags.HasFlag(UniversalSplineFlag.EnterCycle) || packet.Flags.HasFlag(UniversalSplineFlag.Cyclic))
                flags = PathType.ExactPathFlyingCyclic;
            else if (packet.Flags.HasFlag(UniversalSplineFlag.Flying))
                flags = PathType.ExactPathFlying;
            else if (packet.Flags.HasFlag(UniversalSplineFlag.UncompressedPath))
                flags = PathType.ExactPath;
            else if (packet.LookTarget != null)
                flags = PathType.Invalid;
            else if (packet.Jump != null && packet.Jump.StartTime > 0)
                flags = PathType.ExactPathAndJump;
            return flags;
        }

        protected override VoidType Process(PacketBase basePacket, PacketMonsterMove packet)
        {
            if (packet.PackedPoints.Count == 0)
                return default;

            var movementType = GetMovementType(packet);
            if (movementType == PathType.Invalid)
                return default;

            Path path = new Path()
            {
                Waypoints = new List<Waypoint>(),
                Type = movementType
            };

            for (var index = 0; index < packet.PackedPoints.Count; index++)
            {
                var node = packet.PackedPoints[index];
                bool finalP = packet.Destination == null && packet.PackedPoints.Count - 1 != index;
                float ori = finalP ? (packet.HasLookOrientation ? packet.LookOrientation : 100f) : 100f;
                path.Waypoints.Add(new Waypoint()
                {
                    Position = node,
                    Orientation = ori
                });
            }

            if (packet.Destination != null)
            {
                float ori = packet.HasLookOrientation ? packet.LookOrientation : 100f;
                path.Waypoints.Add(new Waypoint()
                {
                    Position = packet.Destination,
                    Orientation = ori
                });
            }

            if (!pathsPerGuid.TryGetValue(packet.Mover, out var pathList))
            {
                pathList = new List<Path>();
                pathsPerGuid.Add(packet.Mover, pathList);
            }
            pathList.Add(path);

            return default;
        }

        protected override string GenerateQuery()
        {
            StringBuilder output = new();
            output.AppendLine("SET @MOVID = 0;");
            int pathIdCounter = 0;
            foreach (var (mover, paths) in pathsPerGuid)
            {
                foreach (var path in paths)
                {
                    string commentInformation = $"GUID: {mover.ToWowParserString()} PathType: {path.Type}";
                    output.AppendLine("-- " + commentInformation);
                    if (Settings.TargetedProject == TargetedProject.Cmangos)
                    {
                        output.AppendLine($"INSERT INTO waypoint_path_name(PathId, Name) VALUES(@MOVID + {pathIdCounter},'{SQLUtil.EscapeString(StoreGetters.GetName(Utilities.ObjectTypeToStore(mover.Type.ToObjectType()), (int)mover.Entry))}');");
                        output.AppendLine("INSERT INTO waypoint_path (PathId, Point, PositionX, PositionY, PositionZ, Orientation, WaitTime, ScriptId, Comment) VALUES");
                    }
                    else if (Settings.TargetedProject == TargetedProject.TrinityCore)
                    {
                        output.AppendLine("INSERT INTO waypoint_path_node (PathId, NodeId, PositionX, PositionY, PositionZ, Orientation, Delay) VALUES");
                    }

                    int pointIdCounter = 1;
                    foreach (var node in path.Waypoints)
                    {
                        var isLast = pointIdCounter == path.Waypoints.Count;
                        if (Settings.TargetedProject == TargetedProject.Cmangos)
                            output.Append($"(@MOVID + {pathIdCounter}, {pointIdCounter}, {node.Position.X}, {node.Position.Y}, {node.Position.Z}, {node.Orientation}, 0, 0, NULL)");
                        else if (Settings.TargetedProject == TargetedProject.TrinityCore)
                            output.Append($"(@MOVID + {pathIdCounter}, {pointIdCounter}, {node.Position.X}, {node.Position.Y}, {node.Position.Z}, {node.Orientation})");

                        if (!isLast)
                            output.AppendLine(",");
                        else
                            output.AppendLine(";");

                        ++pointIdCounter;
                    }

                    ++pathIdCounter;

                    output.AppendLine();
                }
            }
            return output.ToString();
        }
    }
}

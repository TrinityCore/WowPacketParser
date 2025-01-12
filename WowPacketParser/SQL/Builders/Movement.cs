using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.SQL;
using System.Linq;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class Movement
    {
        [BuilderMethod]
        public static string MovementData()
        {
            if (Storage.CreatureMovement.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature_movement))
                return string.Empty;

            string output = "SET @MOVID = 0;\n";
            int pathIdCounter = 0;
            if (Settings.TargetedProject == TargetedProject.Cmangos)
            {
                string nodeSql = "INSERT INTO waypoint_path (PathId, Point, PositionX, PositionY, PositionZ, Orientation, WaitTime, ScriptId, Comment) VALUES\n";
                foreach (var data in Storage.CreatureMovement.ToList())
                {
                    var creatureMovement = data.Item1;
                    if (creatureMovement.Waypoints.Count == 0)
                        continue;

                    string creatureName = SQLUtil.EscapeString(StoreGetters.GetName(Utilities.ObjectTypeToStore(creatureMovement.GUID.GetObjectType()), (int)creatureMovement.GUID.GetEntry()));
                    output += "-- " + $"GUID: {creatureMovement.GUID} PathType: {creatureMovement.Type}" + "\n" + $"INSERT INTO waypoint_path_name(PathId, Name) VALUES(@MOVID + {pathIdCounter},'{creatureName}');\n" + nodeSql;

                    int pointIdCounter = 1;
                    int actualPoint = 1;
                    foreach (var node in creatureMovement.Waypoints)
                    {
                        bool finalP = creatureMovement.Destination == null && creatureMovement.Waypoints.Count == actualPoint;
                        ++actualPoint;
                        float ori = finalP ? (creatureMovement.FinalOrientation != null ? creatureMovement.FinalOrientation.Value : 100f) : 100f;
                        if (node.Point == false)
                        {
                            if (Settings.SkipIntermediatePoints == true)
                                continue;
                            output += "-- ";
                        }

                        output += $"(@MOVID + {pathIdCounter}, '{pointIdCounter}', '{node.Position.X}', '{node.Position.Y}', '{node.Position.Z}', '{ori}', '0', '0', NULL)";
                        
                        if (!finalP)
                            output += ",\n";
                        else
                            output += ";\n\n";
                        
                        ++pointIdCounter;
                    }

                    if (creatureMovement.Destination != null)
                    {
                        float ori = creatureMovement.FinalOrientation != null ? creatureMovement.FinalOrientation.Value : 100f;
                        output += $"(@MOVID + {pathIdCounter}, '{pointIdCounter}', '{creatureMovement.Destination.Position.X}', '{creatureMovement.Destination.Position.Y}', '{creatureMovement.Destination.Position.Z}', '{ori}', '0', '0', NULL);\n";
                    }

                    ++pathIdCounter;
                }
            }
            else if (Settings.TargetedProject == TargetedProject.TrinityCore)
            {
                string nodeSql = "INSERT INTO waypoint_path_node (PathId, NodeId, PositionX, PositionY, PositionZ, Orientation, Delay) VALUES\n";
                foreach (var data in Storage.CreatureMovement.ToList())
                {
                    var creatureMovement = data.Item1;
                    if (creatureMovement.Waypoints.Count == 0)
                        continue;

                    string commentInformation = $"GUID: {creatureMovement.GUID} PathType: {creatureMovement.Type}";
                    output += "-- " + commentInformation + "\n";
                    output += nodeSql;

                    int pointIdCounter = 1;
                    int actualPoint = 1;
                    foreach (var node in creatureMovement.Waypoints)
                    {
                        bool finalP = creatureMovement.Destination == null && creatureMovement.Waypoints.Count == actualPoint;
                        ++actualPoint;
                        float ori = finalP ? (creatureMovement.FinalOrientation != null ? creatureMovement.FinalOrientation.Value : 100f) : 100f;
                        if (node.Point == false)
                        {
                            if (Settings.SkipIntermediatePoints == true)
                                continue;
                            output += "-- ";
                        }
                        output += $"(@MOVID + {pathIdCounter}, '{pointIdCounter}', '{node.Position.X}', '{node.Position.Y}', '{node.Position.Z}', '{ori}')";

                        if (!finalP)
                            output += ",\n";
                        else
                            output += ";\n\n";

                        ++pointIdCounter;
                    }

                    if (creatureMovement.Destination != null)
                    {
                        float ori = creatureMovement.FinalOrientation != null ? creatureMovement.FinalOrientation.Value : 100f;
                        output += $"(@MOVID + {pathIdCounter}, '{pointIdCounter}', '{creatureMovement.Destination.Position.X}', '{creatureMovement.Destination.Position.Y}', '{creatureMovement.Destination.Position.Z}', '{ori}', '0', '0', NULL);\n\n";
                    }

                    ++pathIdCounter;
                }
            }

            return output;
        }
    }
}

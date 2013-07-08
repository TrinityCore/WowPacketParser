using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.SQL.Builders
{
    public static class Spawns
    {
        public static string Creature(Dictionary<Guid, Unit> units)
        {
            if (units.Count == 0)
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature))
                return string.Empty;

            const string tableName = "creature";

            uint count = 0;
            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var unit in units)
            {
                var row = new QueryBuilder.SQLInsertRow();

                var creature = unit.Value;

                if (Settings.AreaFilters.Length > 0)
                    if (!(creature.Area.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.AreaFilters)))
                        continue;

                UpdateField uf;
                if (!creature.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(ObjectField.OBJECT_FIELD_ENTRY), out uf))
                    continue;   // broken entry, nothing to spawn

                var entry = uf.UInt32Value;

                var movementType = 0;
                var spawnDist = 0;

                if (creature.Movement.HasWpsOrRandMov)
                {
                    movementType = 1;
                    spawnDist = 5;
                }

                row.AddValue("guid", "@CGUID+" + count, noQuotes: true);
                row.AddValue("id", entry);
                row.AddValue("map", !creature.IsOnTransport() ? creature.Map : 0);  // TODO: query transport template for map
                row.AddValue("spawnMask", creature.GetDefaultSpawnMask());
                row.AddValue("phaseMask", creature.PhaseMask);
                if (!creature.IsOnTransport())
                {
                    row.AddValue("position_x", creature.Movement.Position.X);
                    row.AddValue("position_y", creature.Movement.Position.Y);
                    row.AddValue("position_z", creature.Movement.Position.Z);
                    row.AddValue("orientation", creature.Movement.Orientation);
                }
                else
                {
                    row.AddValue("position_x", creature.Movement.TransportOffset.X);
                    row.AddValue("position_y", creature.Movement.TransportOffset.Y);
                    row.AddValue("position_z", creature.Movement.TransportOffset.Z);
                    row.AddValue("orientation", creature.Movement.TransportOffset.O);
                }

                row.AddValue("spawntimesecs", creature.GetDefaultSpawnTime());
                row.AddValue("spawndist", spawnDist);
                row.AddValue("MovementType", movementType);
                row.Comment = StoreGetters.GetName(StoreNameType.Unit, (int)unit.Key.GetEntry(), false);
                row.Comment += " (Area: " + StoreGetters.GetName(StoreNameType.Area, creature.Area, false) + ")";

                if (creature.IsTemporarySpawn())
                {
                    row.CommentOut = true;
                    row.Comment += " - !!! might be temporary spawn !!!";
                }
                else if (creature.IsOnTransport())
                {
                    row.CommentOut = true;
                    row.Comment += " - !!! on transport (NYI) !!!";
                }
                else
                    ++count;

                if (creature.Movement.HasWpsOrRandMov)
                    row.Comment += " (possible waypoints or random movement)";

                rows.Add(row);
            }

            var result = new StringBuilder();

            if (count > 0)
            {
                // delete query for GUIDs
                var delete = new QueryBuilder.SQLDelete(Tuple.Create("@CGUID+0", "@CGUID+" + --count), "guid", tableName);
                result.Append(delete.Build());
            }

            var sql = new QueryBuilder.SQLInsert(tableName, rows, withDelete: false);
            result.Append(sql.Build());
            return result.ToString();
        }

        public static string GameObject(Dictionary<Guid, GameObject> gameObjects)
        {
            if (gameObjects.Count == 0)
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.gameobject))
                return string.Empty;

            const string tableName = "gameobject";

            uint count = 0;
            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var gameobject in gameObjects)
            {
                var row = new QueryBuilder.SQLInsertRow();

                var go = gameobject.Value;

                if (Settings.AreaFilters.Length > 0)
                    if (!(go.Area.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.AreaFilters)))
                        continue;

                uint animprogress = 0;
                uint state = 0;
                UpdateField uf;
                if (!go.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(ObjectField.OBJECT_FIELD_ENTRY), out uf))
                    continue;   // broken entry, nothing to spawn

                var entry = uf.UInt32Value;

                if (go.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(GameObjectField.GAMEOBJECT_BYTES_1), out uf))
                {
                    var bytes = uf.UInt32Value;
                    state = (bytes & 0x000000FF);
                    animprogress = Convert.ToUInt32((bytes & 0xFF000000) >> 24);
                }

                row.AddValue("guid", "@OGUID+" + count, noQuotes: true);
                row.AddValue("id", entry);
                row.AddValue("map", !go.IsOnTransport() ? go.Map : 0);  // TODO: query transport template for map
                row.AddValue("spawnMask", go.GetDefaultSpawnMask());
                row.AddValue("phaseMask", go.PhaseMask);
                if (!go.IsOnTransport())
                {
                    row.AddValue("position_x", go.Movement.Position.X);
                    row.AddValue("position_y", go.Movement.Position.Y);
                    row.AddValue("position_z", go.Movement.Position.Z);
                    row.AddValue("orientation", go.Movement.Orientation);
                }
                else
                {
                    row.AddValue("position_x", go.Movement.TransportOffset.X);
                    row.AddValue("position_y", go.Movement.TransportOffset.Y);
                    row.AddValue("position_z", go.Movement.TransportOffset.Z);
                    row.AddValue("orientation", go.Movement.TransportOffset.O);
                }

                var rotation = go.GetRotation();
                if (rotation != null && rotation.Length == 4)
                {
                    row.AddValue("rotation0", rotation[0]);
                    row.AddValue("rotation1", rotation[1]);
                    row.AddValue("rotation2", rotation[2]);
                    row.AddValue("rotation3", rotation[3]);
                }
                else
                {
                    row.AddValue("rotation0", 0);
                    row.AddValue("rotation1", 0);
                    row.AddValue("rotation2", 0);
                    row.AddValue("rotation3", 0);
                }

                row.AddValue("spawntimesecs", go.GetDefaultSpawnTime());
                row.AddValue("animprogress", animprogress);
                row.AddValue("state", state);
                row.Comment = StoreGetters.GetName(StoreNameType.GameObject, (int)gameobject.Key.GetEntry(), false);
                row.Comment += " (Area: " + StoreGetters.GetName(StoreNameType.Area, go.Area, false) + ")";

                if (go.IsTemporarySpawn())
                {
                    row.CommentOut = true;
                    row.Comment += " - !!! might be temporary spawn !!!";
                }
                else if (go.IsTransport())
                {
                    row.CommentOut = true;
                    row.Comment += " - !!! transport !!!";
                }
                else if (go.IsOnTransport())
                {
                    row.CommentOut = true;
                    row.Comment += " - !!! on transport (NYI) !!!";
                }
                else
                    ++count;

                rows.Add(row);
            }

            var result = new StringBuilder();

            if (count > 0)
            {
                // delete query for GUIDs
                var delete = new QueryBuilder.SQLDelete(Tuple.Create("@OGUID+0", "@OGUID+" + --count), "guid", tableName);
                result.Append(delete.Build());
            }

            var sql = new QueryBuilder.SQLInsert(tableName, rows, withDelete: false);
            result.Append(sql.Build());
            return result.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class Spawns
    {
        private static bool GetTransportMap(WoWObject @object, out int mapId)
        {
            mapId = -1;

            WoWObject transport;
            if (!Storage.Objects.TryGetValue(@object.Movement.TransportGuid, out transport))
                return false;

            UpdateField entry;
            if (transport.UpdateFields == null || !transport.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(ObjectField.OBJECT_FIELD_ENTRY), out entry))
                return false;

            if (SQLConnector.Enabled)
            {
                var transportTemplates = SQLDatabase.GetDict<uint, GameObjectTemplate>(new List<uint> { entry.UInt32Value });
                if (transportTemplates.IsEmpty())
                    return false;

                mapId = transportTemplates[entry.UInt32Value].Item1.Data[6];
            }

            return true;
        }

        [BuilderMethod(Units = true)]
        public static string Creature(Dictionary<WowGuid, Unit> units)
        {
            if (units.Count == 0)
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature))
                return string.Empty;

            const string tableName = "creature";
            const string addonTableName = "creature_addon";

            uint count = 0;
            var rows = new List<QueryBuilder.SQLInsertRow>();
            var addonRows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var unit in units)
            {
                var row = new QueryBuilder.SQLInsertRow();
                var badTransport = false;

                var creature = unit.Value;

                if (Settings.AreaFilters.Length > 0)
                    if (!(creature.Area.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.AreaFilters)))
                        continue;

                if (Settings.MapFilters.Length > 0)
                    if (!(creature.Map.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.MapFilters)))
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
                    spawnDist = 10;
                }


                row.AddValue("guid", "@CGUID+" + count, noQuotes: true);
                row.AddValue("id", entry);
                if (!creature.IsOnTransport())
                {
                    row.AddValue("map", creature.Map);
                }
                else
                {
                    int mapId;
                    badTransport = !GetTransportMap(creature, out mapId);
                    row.AddValue("map", mapId);
                }

                row.AddValue("spawnMask", creature.GetDefaultSpawnMask());
                row.AddValue("phaseMask", creature.PhaseMask);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595) && creature.Phases != null)
                    row.AddValue("phaseId", string.Join(" - ", creature.Phases));

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


                var auras = string.Empty;
                var commentAuras = string.Empty;
                if (creature.Auras != null && creature.Auras.Count() != 0)
                {
                    foreach (var aura in creature.Auras)
                    {
                        if (aura == null)
                            continue;

                        // usually "template auras" do not have caster
                        if (ClientVersion.AddedInVersion(ClientType.MistsOfPandaria) ? !aura.AuraFlags.HasAnyFlag(AuraFlagMoP.NoCaster) : !aura.AuraFlags.HasAnyFlag(AuraFlag.NotCaster))
                            continue;

                        auras += aura.SpellId + " ";
                        commentAuras += aura.SpellId + " - " + StoreGetters.GetName(StoreNameType.Spell, (int)aura.SpellId, false) + ", ";
                    }

                    auras = auras.TrimEnd(' ');
                    commentAuras = commentAuras.TrimEnd(',', ' ');

                    row.Comment += " (Auras: " + commentAuras + ")";
                }

                var addonRow = new QueryBuilder.SQLInsertRow();
                if (Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature_addon))
                {
                    addonRow.AddValue("guid", "@CGUID+" + count, noQuotes: true);
                    addonRow.AddValue("mount", creature.Mount);
                    addonRow.AddValue("bytes1", creature.Bytes1, true);
                    addonRow.AddValue("bytes2", creature.Bytes2, true);
                    addonRow.AddValue("auras", auras);
                    addonRow.Comment += StoreGetters.GetName(StoreNameType.Unit, (int)unit.Key.GetEntry(), false);
                    if (!String.IsNullOrWhiteSpace(auras))
                        addonRow.Comment += " - " + commentAuras;
                    addonRows.Add(addonRow);
                }

                if (creature.IsTemporarySpawn())
                {
                    row.CommentOut = true;
                    row.Comment += " - !!! might be temporary spawn !!!";
                }
                else if (creature.IsOnTransport() && badTransport)
                {
                    row.CommentOut = true;
                    row.Comment += " - !!! on transport - transport template not found !!!";
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
                var sql = new QueryBuilder.SQLInsert(tableName, rows, withDelete: false);
                result.Append(sql.Build());

                if (Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature_addon))
                {
                    var addonDelete = new QueryBuilder.SQLDelete(Tuple.Create("@CGUID+0", "@CGUID+" + count), "guid", addonTableName);
                    result.Append(addonDelete.Build());
                    var addonSql = new QueryBuilder.SQLInsert(addonTableName, addonRows, withDelete: false);
                    result.Append(addonSql.Build());
                }
            }

            return result.ToString();
        }

        [BuilderMethod(Gameobjects = true)]
        public static string GameObject(Dictionary<WowGuid, GameObject> gameObjects)
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

                if (Settings.MapFilters.Length > 0)
                    if (!(go.Map.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.MapFilters)))
                        continue;

                uint animprogress = 0;
                uint state = 0;
                UpdateField uf;
                if (!go.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(ObjectField.OBJECT_FIELD_ENTRY), out uf))
                    continue;   // broken entry, nothing to spawn

                var entry = uf.UInt32Value;
                var badTransport = false;

                if (go.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(GameObjectField.GAMEOBJECT_BYTES_1), out uf))
                {
                    var bytes = uf.UInt32Value;
                    state = (bytes & 0x000000FF);
                    animprogress = Convert.ToUInt32((bytes & 0xFF000000) >> 24);
                }

                row.AddValue("guid", "@OGUID+" + count, noQuotes: true);
                row.AddValue("id", entry);
                if (!go.IsOnTransport())
                {
                    row.AddValue("map", go.Map);
                }
                else
                {
                    int mapId;
                    badTransport = !GetTransportMap(go, out mapId);
                    row.AddValue("map", mapId);
                }

                row.AddValue("spawnMask", go.GetDefaultSpawnMask());
                row.AddValue("phaseMask", go.PhaseMask);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595) && go.Phases != null)
                    row.AddValue("phaseId", string.Join(" - ", go.Phases));

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
                else if (go.IsOnTransport() && badTransport)
                {
                    row.CommentOut = true;
                    row.Comment += " - !!! on transport - transport template not found !!!";
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

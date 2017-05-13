﻿using System;
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
                var transportTemplates = SQLDatabase.Get(new RowList<GameObjectTemplate> { new GameObjectTemplate { Entry = entry.UInt32Value } });
                if (transportTemplates.Count == 0)
                    return false;

                mapId = transportTemplates.First().Data.Data[6].GetValueOrDefault();
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

            uint count = 0;
            var rows = new RowList<Creature>();
            var addonRows = new RowList<CreatureAddon>();
            foreach (var unit in units)
            {
                Row<Creature> row = new Row<Creature>();
                bool badTransport = false;

                Unit creature = unit.Value;

                if (Settings.AreaFilters.Length > 0)
                    if (!(creature.Area.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.AreaFilters)))
                        continue;

                if (Settings.MapFilters.Length > 0)
                    if (!(creature.Map.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.MapFilters)))
                        continue;

                UpdateField uf;
                if (!creature.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(ObjectField.OBJECT_FIELD_ENTRY), out uf))
                    continue;   // broken entry, nothing to spawn

                uint entry = uf.UInt32Value;

                uint movementType = 0;
                int spawnDist = 0;
                row.Data.AreaID = 0;
                row.Data.ZoneID = 0;

                if (creature.Movement.HasWpsOrRandMov)
                {
                    movementType = 1;
                    spawnDist = 10;
                }

                row.Data.GUID = "@CGUID+" + count;

                row.Data.ID = entry;
                if (!creature.IsOnTransport())
                    row.Data.Map = creature.Map;
                else
                {
                    int mapId;
                    badTransport = !GetTransportMap(creature, out mapId);
                    if (mapId != -1)
                        row.Data.Map = (uint)mapId;
                }

                if (creature.Area != -1)
                    row.Data.AreaID = (uint)creature.Area;

                if (creature.Zone != -1)
                    row.Data.ZoneID = (uint)creature.Zone;

                row.Data.SpawnMask = (uint)creature.GetDefaultSpawnMask();
                row.Data.PhaseMask = creature.PhaseMask;

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595) && creature.Phases != null)
                {
                    string data = string.Join(" - ", creature.Phases);
                    if (string.IsNullOrEmpty(data))
                        data = "0";

                    row.Data.PhaseID = data;
                }

                if (!creature.IsOnTransport())
                {
                    row.Data.PositionX = creature.Movement.Position.X;
                    row.Data.PositionY = creature.Movement.Position.Y;
                    row.Data.PositionZ = creature.Movement.Position.Z;
                    row.Data.Orientation = creature.Movement.Orientation;
                }
                else
                {
                    row.Data.PositionX = creature.Movement.TransportOffset.X;
                    row.Data.PositionY = creature.Movement.TransportOffset.Y;
                    row.Data.PositionZ = creature.Movement.TransportOffset.Z;
                    row.Data.Orientation = creature.Movement.TransportOffset.O;
                }

                row.Data.SpawnTimeSecs = creature.GetDefaultSpawnTime(creature.DifficultyID);
                row.Data.SpawnDist = spawnDist;
                row.Data.MovementType = movementType;

                // set some defaults
                row.Data.PhaseGroup = 0;
                row.Data.ModelID = 0;
                row.Data.CurrentWaypoint = 0;
                row.Data.CurHealth = 0;
                row.Data.CurMana = 0;
                row.Data.NpcFlag = 0;
                row.Data.UnitFlag = 0;
                row.Data.DynamicFlag = 0;

                row.Comment = StoreGetters.GetName(StoreNameType.Unit, (int)unit.Key.GetEntry(), false);
                row.Comment += " (Area: " + StoreGetters.GetName(StoreNameType.Area, creature.Area, false) + " - ";
                row.Comment += "Difficulty: " + StoreGetters.GetName(StoreNameType.Difficulty, (int)creature.DifficultyID, false) + ")";

                string auras = string.Empty;
                string commentAuras = string.Empty;
                if (creature.Auras != null && creature.Auras.Count != 0)
                {
                    foreach (Aura aura in creature.Auras)
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

                var addonRow = new Row<CreatureAddon>();
                if (Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature_addon))
                {
                    addonRow.Data.GUID = "@CGUID+" + count;
                    addonRow.Data.PathID = 0;
                    addonRow.Data.Mount = creature.Mount.GetValueOrDefault(0);
                    addonRow.Data.Bytes1 = creature.Bytes1.GetValueOrDefault(0);
                    addonRow.Data.Bytes2 = creature.Bytes2.GetValueOrDefault(0);
                    addonRow.Data.Emote = 0;
                    addonRow.Data.Auras = auras;
                    addonRow.Data.AIAnimKit = creature.AIAnimKit.GetValueOrDefault(0);
                    addonRow.Data.MovementAnimKit = creature.MovementAnimKit.GetValueOrDefault(0);
                    addonRow.Data.MeleeAnimKit = creature.MeleeAnimKit.GetValueOrDefault(0);
                    addonRow.Comment += StoreGetters.GetName(StoreNameType.Unit, (int)unit.Key.GetEntry(), false);
                    if (!string.IsNullOrWhiteSpace(auras))
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

            if (count == 0)
                return string.Empty;

            StringBuilder result = new StringBuilder();
            // delete query for GUIDs
            var delete = new SQLDelete<Creature>(Tuple.Create("@CGUID+0", "@CGUID+" + --count));
            result.Append(delete.Build());
            var sql = new SQLInsert<Creature>(rows, false);
            result.Append(sql.Build());

            if (Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature_addon))
            {
                var addonDelete = new SQLDelete<CreatureAddon>(Tuple.Create("@CGUID+0", "@CGUID+" + count));
                result.Append(addonDelete.Build());
                var addonSql = new SQLInsert<CreatureAddon>(addonRows, false);
                result.Append(addonSql.Build());
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

            uint count = 0;
            var rows = new RowList<GameObjectModel>();
            var addonRows = new RowList<GameObjectAddon>();
            foreach (var gameobject in gameObjects)
            {
                Row<GameObjectModel> row = new Row<GameObjectModel>();

                GameObject go = gameobject.Value;

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

                uint entry = uf.UInt32Value;
                bool badTransport = false;

                if (go.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(GameObjectField.GAMEOBJECT_BYTES_1), out uf))
                {
                    uint bytes = uf.UInt32Value;
                    state = (bytes & 0x000000FF);
                    animprogress = Convert.ToUInt32((bytes & 0xFF000000) >> 24);
                }

                row.Data.GUID = "@OGUID+" + count;

                row.Data.ID = entry;
                if (!go.IsOnTransport())
                    row.Data.Map = go.Map;
                else
                {
                    int mapId;
                    badTransport = !GetTransportMap(go, out mapId);
                    if (mapId != -1)
                        row.Data.Map = (uint)mapId;
                }

                row.Data.ZoneID = 0;
                row.Data.AreaID = 0;

                if (go.Area != -1)
                    row.Data.AreaID = (uint)go.Area;

                if (go.Zone != -1)
                    row.Data.ZoneID = (uint)go.Zone;

                row.Data.SpawnMask = (uint)go.GetDefaultSpawnMask();
                row.Data.PhaseMask = go.PhaseMask;

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595) && go.Phases != null)
                    row.Data.PhaseID = string.Join(" - ", go.Phases);

                if (!go.IsOnTransport())
                {
                    row.Data.PositionX = go.Movement.Position.X;
                    row.Data.PositionY = go.Movement.Position.Y;
                    row.Data.PositionZ = go.Movement.Position.Z;
                    row.Data.Orientation = go.Movement.Orientation;
                }
                else
                {
                    row.Data.PositionX = go.Movement.TransportOffset.X;
                    row.Data.PositionY = go.Movement.TransportOffset.Y;
                    row.Data.PositionZ = go.Movement.TransportOffset.Z;
                    row.Data.Orientation = go.Movement.TransportOffset.O;
                }

                var rotation = go.GetStaticRotation();
                row.Data.Rotation = new float?[] { rotation.X, rotation.Y, rotation.Z, rotation.W };

                bool add = true;
                var addonRow = new Row<GameObjectAddon>();
                if (Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.gameobject_addon))
                {
                    addonRow.Data.GUID = "@OGUID+" + count;

                    var parentRotation = go.GetParentRotation();

                    if (parentRotation != null)
                    {
                        addonRow.Data.parentRot0 = parentRotation[0].GetValueOrDefault(0.0f);
                        addonRow.Data.parentRot1 = parentRotation[1].GetValueOrDefault(0.0f);
                        addonRow.Data.parentRot2 = parentRotation[2].GetValueOrDefault(0.0f);
                        addonRow.Data.parentRot3 = parentRotation[3].GetValueOrDefault(1.0f);

                        if (addonRow.Data.parentRot0 == 0.0f &&
                            addonRow.Data.parentRot1 == 0.0f &&
                            addonRow.Data.parentRot2 == 0.0f &&
                            addonRow.Data.parentRot3 == 1.0f)
                            add = false;
                    }
                    else
                        add = false;

                    addonRow.Comment += StoreGetters.GetName(StoreNameType.GameObject, (int)gameobject.Key.GetEntry(), false);

                    if (add)
                        addonRows.Add(addonRow);
                }

                row.Data.SpawnTimeSecs = go.GetDefaultSpawnTime(go.DifficultyID);
                row.Data.AnimProgress = animprogress;
                row.Data.State = state;

                // set some defaults
                row.Data.PhaseGroup = 0;

                row.Comment = StoreGetters.GetName(StoreNameType.GameObject, (int)gameobject.Key.GetEntry(), false);
                row.Comment += " (Area: " + StoreGetters.GetName(StoreNameType.Area, go.Area, false) + " - ";
                row.Comment += "Difficulty: " + StoreGetters.GetName(StoreNameType.Difficulty, (int)go.DifficultyID, false) + ")";

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

            if (count == 0)
                return String.Empty;

            StringBuilder result = new StringBuilder();
            // delete query for GUIDs
            var delete = new SQLDelete<GameObjectModel>(Tuple.Create("@OGUID+0", "@OGUID+" + --count));
            result.Append(delete.Build());

            var sql = new SQLInsert<GameObjectModel>(rows, false);
            result.Append(sql.Build());

            if (Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.gameobject_addon))
            {
                var addonDelete = new SQLDelete<GameObjectAddon>(Tuple.Create("@OGUID+0", "@OGUID+" + count));
                result.Append(addonDelete.Build());
                var addonSql = new SQLInsert<GameObjectAddon>(addonRows, false);
                result.Append(addonSql.Build());
            }

            return result.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Store.Objects.Comparer;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class AreaTriggers
    {
        [BuilderMethod]
        public static string AreaTriggerTemplateData()
        {
            if (Storage.AreaTriggerTemplates.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.areatrigger_template))
                return string.Empty;

            var templateDb = SQLDatabase.Get(Storage.AreaTriggerTemplates);

            return SQLUtil.Compare(Settings.SQLOrderByKey ? Storage.AreaTriggerTemplates.OrderBy(x => x.Item1.Id).ToArray() : Storage.AreaTriggerTemplates.ToArray(), templateDb, x => string.Empty);
        }

        [BuilderMethod]
        public static string AreaTriggerCreatePropertiesData()
        {
            var createPropertiesList = Storage.Objects.IsEmpty()
                ? new Dictionary<WowGuid, AreaTriggerCreateProperties>()                                        // empty dict if there are no objects
                : Storage.Objects.Where(
                    obj =>
                        obj.Value.Item1.Type == ObjectType.AreaTrigger &&
                        !obj.Value.Item1.IsTemporarySpawn())                                                    // remove temporary spawns
                    .OrderBy(pair => pair.Value.Item2)                                                          // order by spawn time
                    .ToDictionary(obj => obj.Key, obj => obj.Value.Item1 as AreaTriggerCreateProperties);

            if (createPropertiesList.Count == 0)
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.areatrigger_create_properties))
                return string.Empty;

            var createPropertiesData = new DataBag<AreaTriggerCreateProperties>();
            var customRows = new RowList<AreaTriggerCreatePropertiesCustom>();

            foreach (var createProperties in createPropertiesList)
            {
                // CreateProperties from spells
                if (createProperties.Value.IsCustom == 0)
                {
                    if (createProperties.Value.SpellForVisuals > 0 && createProperties.Value.SpellForVisuals == createProperties.Value.spellId)
                        createProperties.Value.SpellForVisuals = null;
                    createPropertiesData.Add(createProperties.Value);
                }
                else
                {
                    createPropertiesList[createProperties.Key].CustomId = $"@ATPROPERTIESID+{customRows.Count}";

                    Row<AreaTriggerCreatePropertiesCustom> row = new();
                    row.Data.AreaTriggerCreatePropertiesId = createProperties.Value.CustomId;
                    row.Data.AreaTriggerId = createProperties.Value.AreaTriggerId;
                    row.Data.IsAreatriggerCustom = createProperties.Value.IsAreatriggerCustom;
                    row.Data.Flags = createProperties.Value.Flags;
                    row.Data.AnimId = createProperties.Value.AnimId;
                    row.Data.AnimKitId = createProperties.Value.AnimKitId;
                    row.Data.DecalPropertiesId = createProperties.Value.DecalPropertiesId;
                    row.Data.SpellForVisuals = createProperties.Value.SpellForVisuals;
                    row.Data.TimeToTarget = createProperties.Value.TimeToTarget;
                    row.Data.TimeToTargetScale = createProperties.Value.TimeToTargetScale;
                    row.Data.Speed = createProperties.Value.Speed;
                    row.Data.Shape = createProperties.Value.Shape;
                    row.Data.ShapeData = createProperties.Value.ShapeData;

                    if (row.Data.SpellForVisuals > 0)
                        row.Comment = $"SpellForVisuals: {StoreGetters.GetName(StoreNameType.Spell, (int)row.Data.SpellForVisuals)}";

                    customRows.Add(row);
                }
            }

            StringBuilder result = new StringBuilder();
            if (customRows.Count > 0) {
                var sql = new SQLInsert<AreaTriggerCreatePropertiesCustom>(customRows, true);
                result.Append(sql.Build());
                result.Append(Environment.NewLine);
            }

            var templateDb = SQLDatabase.Get(createPropertiesData);

            return result.ToString() + SQLUtil.Compare(Settings.SQLOrderByKey ? createPropertiesData.OrderBy(x => x.Item1.AreaTriggerId).ToArray() : createPropertiesData.ToArray(),
                templateDb,
                x =>
                {
                    var comment = $"Spell: {StoreGetters.GetName(StoreNameType.Spell, (int)x.spellId)}";
                    if (x.SpellForVisuals > 0 && x.spellId != x.SpellForVisuals)
                        comment += $" SpellForVisuals: {StoreGetters.GetName(StoreNameType.Spell, (int)x.SpellForVisuals)}";
                    return comment;
                });
        }

        [BuilderMethod]
        public static string AreaTriggerCreatePropertiesOrbitData()
        {
            if (Storage.AreaTriggerCreatePropertiesOrbits.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.areatrigger_create_properties_orbit))
                return string.Empty;

            foreach (var orbit in Storage.AreaTriggerCreatePropertiesOrbits)
            {
                AreaTriggerCreateProperties areaTrigger = orbit.Item1.CreateProperties;

                orbit.Item1.spellId = areaTrigger.spellId;
                orbit.Item1.AreaTriggerCreatePropertiesId = areaTrigger.AreaTriggerCreatePropertiesId;
                orbit.Item1.IsCustom = areaTrigger.IsCustom;
            }

            var templateDb = SQLDatabase.Get(Storage.AreaTriggerCreatePropertiesOrbits);

            return SQLUtil.Compare(Settings.SQLOrderByKey ? Storage.AreaTriggerCreatePropertiesOrbits.OrderBy(x => x.Item1.AreaTriggerCreatePropertiesId).ToArray() : Storage.AreaTriggerCreatePropertiesOrbits.ToArray(),
                templateDb,
                x =>
                {
                    var comment = $"Spell: {StoreGetters.GetName(StoreNameType.Spell, (int)x.spellId)}";
                    return comment;
                });
        }

        [BuilderMethod]
        public static string AreaTriggerCreatePropertiesPolygonVertexData()
        {
            if (Storage.AreaTriggerCreatePropertiesPolygonVertices.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.areatrigger_create_properties_polygon_vertex))
                return string.Empty;

            var createPropertiesPolygonVertexData = new DataBag<AreaTriggerCreatePropertiesPolygonVertex>();
            var customRows = new RowList<AreaTriggerCreatePropertiesPolygonVertexCustom>();

            foreach (var vertex in Storage.AreaTriggerCreatePropertiesPolygonVertices)
            {
                var spellAreaTriggerTuple = Storage.Objects.Where(obj => obj.Key == vertex.Item1.areatriggerGuid).First();
                AreaTriggerCreateProperties createProperties = (AreaTriggerCreateProperties)spellAreaTriggerTuple.Value.Item1;

                vertex.Item1.spellId = createProperties.spellId;
                vertex.Item1.AreaTriggerCreatePropertiesId = createProperties.AreaTriggerCreatePropertiesId;
                vertex.Item1.IsCustom = createProperties.IsCustom;

                // CreateProperties from spells
                if (createProperties.IsCustom == 0)
                    createPropertiesPolygonVertexData.Add(vertex.Item1);
                else
                {
                    Row<AreaTriggerCreatePropertiesPolygonVertexCustom> row = new();
                    row.Data.AreaTriggerCreatePropertiesId = createProperties.CustomId;
                    row.Data.VerticeX = vertex.Item1.VerticeX;
                    row.Data.VerticeY = vertex.Item1.VerticeY;
                    row.Data.VerticeTargetX = vertex.Item1.VerticeTargetX;
                    row.Data.VerticeTargetY = vertex.Item1.VerticeTargetY;
                    customRows.Add(row);
                }
            }

            StringBuilder result = new StringBuilder();
            if (customRows.Count > 0)
            {
                var sql = new SQLInsert<AreaTriggerCreatePropertiesPolygonVertexCustom>(customRows, true);
                result.Append(sql.Build());
                result.Append(Environment.NewLine);
            }

            var templateDb = SQLDatabase.Get(createPropertiesPolygonVertexData);

            return result.ToString() + SQLUtil.Compare(Settings.SQLOrderByKey ? createPropertiesPolygonVertexData.OrderBy(x => x.Item1.AreaTriggerCreatePropertiesId).ToArray() : createPropertiesPolygonVertexData.ToArray(),
                templateDb,
                x =>
                {
                    var comment = $"Spell: {StoreGetters.GetName(StoreNameType.Spell, (int)x.spellId)}";
                    return comment;
                });
        }

        [BuilderMethod]
        public static string AreaTriggerCreatePropertiesSplinePointData()
        {
            if (Storage.AreaTriggerCreatePropertiesSplinePoints.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.areatrigger_create_properties_spline_point))
                return string.Empty;

            foreach (var splinePoint in Storage.AreaTriggerCreatePropertiesSplinePoints)
            {
                var spellAreaTriggerTuple = Storage.Objects.Where(obj => obj.Key == splinePoint.Item1.areatriggerGuid).First();
                AreaTriggerCreateProperties areaTrigger = (AreaTriggerCreateProperties)spellAreaTriggerTuple.Value.Item1;

                splinePoint.Item1.spellId = areaTrigger.spellId;
                splinePoint.Item1.AreaTriggerCreatePropertiesId = areaTrigger.AreaTriggerCreatePropertiesId;
                splinePoint.Item1.IsCustom = areaTrigger.IsCustom;

                // convert points to offsets
                splinePoint.Item1.X -= areaTrigger.Movement.Position.X;
                splinePoint.Item1.Y -= areaTrigger.Movement.Position.Y;
                splinePoint.Item1.Z -= areaTrigger.Movement.Position.Z;

                float areaTriggerO = areaTrigger.Movement.Orientation;
                float inx = splinePoint.Item1.X.Value;
                float iny = splinePoint.Item1.Y.Value;

                splinePoint.Item1.X = (float)((inx + iny * Math.Tan(areaTriggerO)) / (Math.Cos(areaTriggerO) + Math.Sin(areaTriggerO) * Math.Tan(areaTriggerO)));
                splinePoint.Item1.Y = (float)((iny - inx * Math.Tan(areaTriggerO)) / (Math.Cos(areaTriggerO) + Math.Sin(areaTriggerO) * Math.Tan(areaTriggerO)));
            }

            var templateDb = SQLDatabase.Get(Storage.AreaTriggerCreatePropertiesSplinePoints);

            return SQLUtil.Compare(Settings.SQLOrderByKey ? Storage.AreaTriggerCreatePropertiesSplinePoints.OrderBy(x => x.Item1.AreaTriggerCreatePropertiesId).ToArray() : Storage.AreaTriggerCreatePropertiesSplinePoints.ToArray(),
                templateDb,
                x =>
                {
                    var comment = $"Spell: {StoreGetters.GetName(StoreNameType.Spell, (int)x.spellId)}";
                    return comment;
                });
        }

        [BuilderMethod]
        public static string AreaTriggerData()
        {
            var areaTriggers = Storage.Objects.IsEmpty()
                ? new Dictionary<WowGuid, AreaTriggerCreateProperties>()                                        // empty dict if there are no objects
                : Storage.Objects.Where(
                    obj =>
                        obj.Value.Item1.Type == ObjectType.AreaTrigger &&
                        !obj.Value.Item1.IsTemporarySpawn())                                                    // remove temporary spawns
                    .OrderBy(pair => pair.Value.Item2)                                                          // order by spawn time
                    .ToDictionary(obj => obj.Key, obj => obj.Value.Item1 as AreaTriggerCreateProperties);

            if (areaTriggers.Count == 0)
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.areatrigger))
                return string.Empty;

            var rows = new RowList<AreaTrigger>();

            var areaTriggerList = Settings.SkipDuplicateSpawns
                ? areaTriggers.Values.GroupBy(u => u, new SpawnComparer()).Select(x => x.First())
                : areaTriggers.Values.ToList();

            uint count = 0;
            foreach (var at in areaTriggerList)
            {
                if (Settings.AreaFilters.Length > 0)
                    if (!(at.Area.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.AreaFilters)))
                        continue;

                if (Settings.MapFilters.Length > 0)
                    if (!(at.Map.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.MapFilters)))
                        continue;

                if (!Filters.CheckFilter(at.Guid))
                    continue;

                // only spawn ats without spawn spellid
                if (at.spellId != 0)
                    continue;

                Row<AreaTrigger> row = new();

                row.Data.SpawnId = $"@ATSPAWNID+{count}";
                row.Data.AreaTriggerCreatePropertiesId = at.CustomId;
                row.Data.IsCustom = at.IsCustom;

                row.Data.MapId = at.Map;

                if (!at.IsOnTransport())
                {
                    row.Data.PosX = at.Movement.Position.X;
                    row.Data.PosY = at.Movement.Position.Y;
                    row.Data.PosZ = at.Movement.Position.Z;
                    row.Data.Orientation = at.Movement.Orientation;
                }
                else
                {
                    row.Data.PosX = at.Movement.Transport.Offset.X;
                    row.Data.PosY = at.Movement.Transport.Offset.Y;
                    row.Data.PosZ = at.Movement.Transport.Offset.Z;
                    row.Data.Orientation = at.Movement.Transport.Offset.O;
                }

                string difficulties = string.Join(",", at.GetDefaultSpawnDifficulties());
                if (string.IsNullOrEmpty(difficulties))
                    difficulties = "0";

                row.Data.SpawnDifficulties = difficulties;

                string phaseData = string.Join(" - ", at.Phases);
                if (string.IsNullOrEmpty(phaseData) || Settings.ForcePhaseZero)
                    phaseData = "0";
                row.Data.PhaseId = phaseData;
                row.Data.PhaseUseFlags = 0;
                row.Data.PhaseGroup = 0;

                row.Data.Shape = at.Shape;
                row.Data.ShapeData = at.ShapeData;
                if (at.SpellForVisuals > 0)
                    row.Data.SpellForVisuals = at.SpellForVisuals;

                row.Data.Comment = "";
                row.Comment += $"(Area: {StoreGetters.GetName(StoreNameType.Area, at.Area, false)} - ";
                row.Comment += $"Difficulty: {StoreGetters.GetName(StoreNameType.Difficulty, (int)at.DifficultyID, false)})";

                rows.Add(row);

                count++;
            }

            if (count == 0)
                return string.Empty;

            StringBuilder result = new StringBuilder();
            var delete = new SQLDelete<AreaTrigger>(Tuple.Create("@ATSPAWNID+0", "@ATSPAWNID+" + --count));
            result.Append(delete.Build());
            var sql = new SQLInsert<AreaTrigger>(rows, false);
            result.Append(sql.Build());

            return result.ToString();
        }
    }
}

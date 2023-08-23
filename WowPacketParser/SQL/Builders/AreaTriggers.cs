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
            var spellareatriggers = Storage.Objects.IsEmpty()
                ? new Dictionary<WowGuid, AreaTriggerCreateProperties>()                                        // empty dict if there are no objects
                : Storage.Objects.Where(
                    obj =>
                        obj.Value.Item1.Type == ObjectType.AreaTrigger &&
                        !obj.Value.Item1.IsTemporarySpawn())                                                    // remove temporary spawns
                    .OrderBy(pair => pair.Value.Item2)                                                          // order by spawn time
                    .ToDictionary(obj => obj.Key, obj => obj.Value.Item1 as AreaTriggerCreateProperties);

            if (spellareatriggers.Count == 0)
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.areatrigger_create_properties))
                return string.Empty;

            var spellareatriggersData = new DataBag<AreaTriggerCreateProperties>();

            foreach (var spellareatrigger in spellareatriggers)
            {
                if (spellareatrigger.Value.spellId == 0)
                    continue;

                spellareatriggersData.Add(spellareatrigger.Value);
            }

            var templateDb = SQLDatabase.Get(spellareatriggersData);

            return SQLUtil.Compare(Settings.SQLOrderByKey ? spellareatriggersData.OrderBy(x => x.Item1.AreaTriggerId).ToArray() : spellareatriggersData.ToArray(),
                templateDb,
                x =>
                {
                    var comment = $"Spell: {StoreGetters.GetName(StoreNameType.Spell, (int)x.spellId)}";
                    if ((x.AreaTriggerCreatePropertiesId & 0x80000000) != 0)
                        comment += " CANNOT FIND PROPERTIES ID, USED SPELL ID AS KEY (NEEDS MANUAL CORRECTION)";

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
                var spellAreaTriggerTuple = Storage.Objects.Where(obj => obj.Key == orbit.Item1.areatriggerGuid).First();
                AreaTriggerCreateProperties areaTrigger = (AreaTriggerCreateProperties)spellAreaTriggerTuple.Value.Item1;

                orbit.Item1.spellId = areaTrigger.spellId;
                orbit.Item1.AreaTriggerCreatePropertiesId = areaTrigger.AreaTriggerCreatePropertiesId;
            }

            var templateDb = SQLDatabase.Get(Storage.AreaTriggerCreatePropertiesOrbits);

            return SQLUtil.Compare(Settings.SQLOrderByKey ? Storage.AreaTriggerCreatePropertiesOrbits.OrderBy(x => x.Item1.AreaTriggerCreatePropertiesId).ToArray() : Storage.AreaTriggerCreatePropertiesOrbits.ToArray(),
                templateDb,
                x =>
                {
                    var comment = $"Spell: {StoreGetters.GetName(StoreNameType.Spell, (int)x.spellId)}";
                    if ((x.AreaTriggerCreatePropertiesId & 0x80000000) != 0)
                        comment += " CANNOT FIND PROPERTIES ID, USED SPELL ID AS KEY (NEEDS MANUAL CORRECTION)";

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

            foreach (var vertex in Storage.AreaTriggerCreatePropertiesPolygonVertices)
            {
                var spellAreaTriggerTuple = Storage.Objects.Where(obj => obj.Key == vertex.Item1.areatriggerGuid).First();
                AreaTriggerCreateProperties areaTrigger = (AreaTriggerCreateProperties)spellAreaTriggerTuple.Value.Item1;

                vertex.Item1.spellId = areaTrigger.spellId;
                vertex.Item1.AreaTriggerCreatePropertiesId = areaTrigger.AreaTriggerCreatePropertiesId;
            }

            var templateDb = SQLDatabase.Get(Storage.AreaTriggerCreatePropertiesPolygonVertices);

            return SQLUtil.Compare(Settings.SQLOrderByKey ? Storage.AreaTriggerCreatePropertiesPolygonVertices.OrderBy(x => x.Item1.AreaTriggerCreatePropertiesId).ToArray() : Storage.AreaTriggerCreatePropertiesPolygonVertices.ToArray(),
                templateDb,
                x =>
                {
                    var comment = $"Spell: {StoreGetters.GetName(StoreNameType.Spell, (int)x.spellId)}";
                    if ((x.AreaTriggerCreatePropertiesId & 0x80000000) != 0)
                        comment += " CANNOT FIND PROPERTIES ID, USED SPELL ID AS KEY (NEEDS MANUAL CORRECTION)";

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
                    if ((x.AreaTriggerCreatePropertiesId & 0x80000000) != 0)
                        comment += " CANNOT FIND PROPERTIES ID, USED SPELL ID AS KEY (NEEDS MANUAL CORRECTION)";

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
                row.Data.AreaTriggerId = at.Guid.GetEntry();
                row.Data.IsServerSide = 0;

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

                string phaseData = string.Join(" - ", at.Phases);
                if (string.IsNullOrEmpty(phaseData) || Settings.ForcePhaseZero)
                    phaseData = "0";
                row.Data.PhaseId = phaseData;
                row.Data.PhaseUseFlags = 0;
                row.Data.PhaseGroup = 0;

                row.Data.Shape = at.Shape;
                row.Data.ShapeData = at.ShapeData;
                row.Data.SpellForVisuals = at.SpellForVisuals;

                row.Data.Comment = "";
                row.Comment = StoreGetters.GetName(StoreNameType.Spell, (int)at.SpellForVisuals, true);
                row.Comment += " (Area: " + StoreGetters.GetName(StoreNameType.Area, at.Area, false) + " - ";
                row.Comment += "Difficulty: " + StoreGetters.GetName(StoreNameType.Difficulty, (int)at.DifficultyID, false) + ")";

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

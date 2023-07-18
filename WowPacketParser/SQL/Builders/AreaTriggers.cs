using System;
using System.Collections.Generic;
using System.Linq;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

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
    }
}

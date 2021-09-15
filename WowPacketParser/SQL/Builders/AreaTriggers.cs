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
        public static string SpellAreaTriggersData()
        {
            var spellareatriggers = Storage.Objects.IsEmpty()
                ? new Dictionary<WowGuid, SpellAreaTrigger>()                                                   // empty dict if there are no objects
                : Storage.Objects.Where(
                    obj =>
                        obj.Value.Item1.Type == ObjectType.AreaTrigger &&
                        !obj.Value.Item1.IsTemporarySpawn())                                                    // remove temporary spawns
                    .OrderBy(pair => pair.Value.Item2)                                                          // order by spawn time
                    .ToDictionary(obj => obj.Key, obj => obj.Value.Item1 as SpellAreaTrigger);

            if (spellareatriggers.Count == 0)
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.spell_areatrigger))
                return string.Empty;

            var spellareatriggersData = new DataBag<SpellAreaTrigger>();

            foreach (var spellareatrigger in spellareatriggers)
            {
                spellareatriggersData.Add(spellareatrigger.Value);
            }

            var templateDb = SQLDatabase.Get(spellareatriggersData);

            return SQLUtil.Compare(Settings.SQLOrderByKey ? spellareatriggersData.OrderBy(x => x.Item1.AreaTriggerId).ToArray() : spellareatriggersData.ToArray(), templateDb, x => "SpellId : " + x.spellId.ToString());
        }

        [BuilderMethod]
        public static string SpellAreaTriggerSplineData()
        {
            if (Storage.SpellAreaTriggerSplines.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.spell_areatrigger_splines))
                return string.Empty;

            var templateDb = SQLDatabase.Get(Storage.SpellAreaTriggerSplines);

            foreach (var splinePoint in Storage.SpellAreaTriggerSplines)
            {
                var spellAreaTriggerTuple = Storage.Objects.Where(obj => obj.Key == splinePoint.Item1.areatriggerGuid).First();
                SpellAreaTrigger spellAreaTrigger = (SpellAreaTrigger)spellAreaTriggerTuple.Value.Item1;

                splinePoint.Item1.spellId = spellAreaTrigger.spellId;
                splinePoint.Item1.SpellMiscId = spellAreaTrigger.SpellMiscId;

                // convert points to offsets
                splinePoint.Item1.X -= spellAreaTrigger.Movement.Position.X;
                splinePoint.Item1.Y -= spellAreaTrigger.Movement.Position.Y;
                splinePoint.Item1.Z -= spellAreaTrigger.Movement.Position.Z;

                float areaTriggerO = spellAreaTrigger.Movement.Orientation;
                float inx = splinePoint.Item1.X.Value;
                float iny = splinePoint.Item1.Y.Value;

                splinePoint.Item1.X = (float)((inx + iny * Math.Tan(areaTriggerO)) / (Math.Cos(areaTriggerO) + Math.Sin(areaTriggerO) * Math.Tan(areaTriggerO)));
                splinePoint.Item1.Y = (float)((iny - inx * Math.Tan(areaTriggerO)) / (Math.Cos(areaTriggerO) + Math.Sin(areaTriggerO) * Math.Tan(areaTriggerO)));
            }

            return SQLUtil.Compare(Settings.SQLOrderByKey ? Storage.SpellAreaTriggerSplines.OrderBy(x => x.Item1.SpellMiscId).ToArray() : Storage.SpellAreaTriggerSplines.ToArray(), templateDb, x => "SpellId : " + x.spellId.ToString());
        }

        [BuilderMethod]
        public static string SpellAreaTriggerVerticesData()
        {
            if (Storage.SpellAreaTriggerVertices.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.spell_areatrigger_vertices))
                return string.Empty;

            var templateDb = SQLDatabase.Get(Storage.SpellAreaTriggerVertices);

            foreach (var spellAreaTriggerVertice in Storage.SpellAreaTriggerVertices)
            {
                var spellAreaTriggerTuple = Storage.Objects.Where(obj => obj.Key == spellAreaTriggerVertice.Item1.areatriggerGuid).First();
                SpellAreaTrigger spellAreaTrigger = (SpellAreaTrigger)spellAreaTriggerTuple.Value.Item1;

                spellAreaTriggerVertice.Item1.spellId = spellAreaTrigger.spellId;
                spellAreaTriggerVertice.Item1.SpellMiscId = spellAreaTrigger.SpellMiscId;
            }

            return SQLUtil.Compare(Settings.SQLOrderByKey ? Storage.SpellAreaTriggerVertices.OrderBy(x => x.Item1.SpellMiscId).ToArray() : Storage.SpellAreaTriggerVertices.ToArray(), templateDb, x => "SpellId : " + x.spellId.ToString());
        }
    }
}

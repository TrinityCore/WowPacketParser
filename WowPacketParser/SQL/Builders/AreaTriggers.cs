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
        public static string AreaTriggerTemplateVerticesData()
        {
            if (Storage.AreaTriggerTemplatesVertices.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.areatrigger_template_polygon_vertices))
                return string.Empty;

            var templateDb = SQLDatabase.Get(Storage.AreaTriggerTemplatesVertices);

            return SQLUtil.Compare(Settings.SQLOrderByKey ? Storage.AreaTriggerTemplatesVertices.OrderBy(x => x.Item1.AreaTriggerId).ToArray() : Storage.AreaTriggerTemplatesVertices.ToArray(), templateDb, x => string.Empty);
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
    }
}

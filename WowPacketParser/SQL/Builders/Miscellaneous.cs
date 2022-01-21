using System.Collections.Generic;
using System.Linq;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class Miscellaneous
    {
        [BuilderMethod]
        public static string StartInformation()
        {
            string result = string.Empty;

            if (!Storage.StartActions.IsEmpty() && Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.playercreateinfo_action))
            {
                result += SQLUtil.Compare(Storage.StartActions, SQLDatabase.Get(Storage.StartActions), a =>
                {
                    if (a.Type == ActionButtonType.Spell)
                        return StoreGetters.GetName(StoreNameType.Spell, (int)a.Action.GetValueOrDefault(), false);
                    if (a.Type == ActionButtonType.Item)
                        return StoreGetters.GetName(StoreNameType.Item, (int)a.Action.GetValueOrDefault(), false);

                    return string.Empty;
                });

            }

            if (!Storage.StartPositions.IsEmpty() && Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.playercreateinfo))
            {
                var dataDb = SQLDatabase.Get(Storage.StartPositions);

                result += SQLUtil.Compare(Storage.StartPositions, dataDb, StoreNameType.None);
            }

            return result;
        }

        [BuilderMethod(TargetSQLDatabase.WPP)]
        public static string ObjectNames()
        {
            if (Storage.ObjectNames.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.ObjectNames))
                return string.Empty;

            var templateDb = SQLDatabase.Get(Storage.ObjectNames, Settings.WPPDatabase);

            return SQLUtil.Compare(Storage.ObjectNames, templateDb, StoreNameType.None);
        }

        [BuilderMethod(TargetSQLDatabase.WPP)]
        public static string SniffData()
        {
            if (Storage.SniffData.IsEmpty())
                return string.Empty;

            if (Settings.DumpFormat != DumpFormatType.SniffDataOnly)
                if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.SniffData) && !Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.SniffDataOpcodes))
                    return string.Empty;

            var templateDb = SQLDatabase.Get(Storage.SniffData, Settings.WPPDatabase);

            return SQLUtil.Compare(Storage.SniffData, templateDb, x => string.Empty);
        }

        // Non-WDB data but nevertheless data that should be saved to gameobject_template
        /*[BuilderMethod(Gameobjects = true)]
        public static string GameobjectTemplateNonWDB(Dictionary<WowGuid, GameObject> gameobjects)
        {
            if (gameobjects.Count == 0)
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.gameobject_template))
                return string.Empty;

            var templates = new StoreDictionary<uint, GameObjectTemplateNonWDB>();
            foreach (var goT in gameobjects)
            {
                if (templates.ContainsKey(goT.Key.GetEntry()))
                    continue;

                var go = goT.Value;

                if (Settings.AreaFilters.Length > 0)
                    if (!(go.Area.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.AreaFilters)))
                        continue;

                if (Settings.MapFilters.Length > 0)
                    if (!(go.Map.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.MapFilters)))
                        continue;

                var template = new GameObjectTemplateNonWDB
                {
                    Size = go.Size.GetValueOrDefault(1.0f),
                    Faction = go.Faction.GetValueOrDefault(0),
                    Flags = go.Flags.GetValueOrDefault(GameObjectFlag.None)
                };

                if (template.Faction == 1 || template.Faction == 2 || template.Faction == 3 ||
                    template.Faction == 4 || template.Faction == 5 || template.Faction == 6 ||
                    template.Faction == 115 || template.Faction == 116 || template.Faction == 1610 ||
                    template.Faction == 1629 || template.Faction == 2203 || template.Faction == 2204 ||
                    template.Faction == 2395 || template.Faction == 2401 || template.Faction == 2402) // player factions
                    template.Faction = 0;

                template.Flags &= ~GameObjectFlag.Triggered;
                template.Flags &= ~GameObjectFlag.Damaged;
                template.Flags &= ~GameObjectFlag.Destroyed;

                templates.Add(goT.Key.GetEntry(), template);
            }

            var templatesDb = SQLDatabase.GetDict<uint, GameObjectTemplateNonWDB>(templates.Keys());
            return SQLUtil.CompareDicts(templates, templatesDb, StoreNameType.GameObject);
        }*/

        [BuilderMethod]
        public static string WeatherUpdates()
        {
            if (Storage.WeatherUpdates.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.weather_updates))
                return string.Empty;

            var rows = new RowList<WeatherUpdate>();
            foreach (var row in Storage.WeatherUpdates.Select(weatherUpdate => new Row<WeatherUpdate>
            {
                Data = weatherUpdate.Item1,
                Comment = StoreGetters.GetName(StoreNameType.Map, (int)weatherUpdate.Item1.MapId.GetValueOrDefault(), false) +
                          " - " + weatherUpdate.Item1.State + " - " + weatherUpdate.Item1.Grade
            }))
            {
                rows.Add(row);
            }

            return new SQLInsert<WeatherUpdate>(rows, ignore: true, withDelete: false).Build();
        }

        [BuilderMethod]
        public static string SceneTemplates()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.scene_template))
                return string.Empty;

            var sceneTemplatesFromObjects = Storage.Objects.IsEmpty()
                ? new List<(SceneTemplate, System.TimeSpan?)>()
                : Storage.Objects
                    .Where(obj => obj.Value.Item1.Type == ObjectType.SceneObject)
                    .Select(pair => (pair.Value.Item1 as SceneObject, pair.Value.Item2))
                    .Where(obj => obj.Item1.CanBeSaved())
                    .Select(obj => (obj.Item1.CreateSceneTemplate(), obj.Item2))
                    .ToList();

            foreach (var scene in sceneTemplatesFromObjects)
                Storage.Scenes.Add(scene.Item1, scene.Item2);

            if (Storage.Scenes.IsEmpty())
                return string.Empty;

            var templateDb = SQLDatabase.Get(Storage.Scenes, Settings.TDBDatabase);

            return SQLUtil.Compare(Storage.Scenes, templateDb, StoreNameType.None);
        }
    }
}

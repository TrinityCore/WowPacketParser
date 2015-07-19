using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
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
            var result = String.Empty;

            if (!Storage.StartActions.IsEmpty() && Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.playercreateinfo_action))
            {
                var rows = new List<QueryBuilder.SQLInsertRow>();
                foreach (var startActions in Storage.StartActions)
                {
                    var comment = new QueryBuilder.SQLInsertRow
                    {
                        HeaderComment = startActions.Key.Item1 + " - " + startActions.Key.Item2
                    };
                    rows.Add(comment);

                    foreach (var action in startActions.Value.Item1.Actions)
                    {
                        var row = new QueryBuilder.SQLInsertRow();

                        row.AddValue("race", startActions.Key.Item1);
                        row.AddValue("class", startActions.Key.Item2);
                        row.AddValue("button", action.Button);
                        row.AddValue("action", action.Id);
                        row.AddValue("type", action.Type);
                        if (action.Type == ActionButtonType.Spell)
                            row.Comment = StoreGetters.GetName(StoreNameType.Spell, (int)action.Id, false);
                        if (action.Type == ActionButtonType.Item)
                            row.Comment = StoreGetters.GetName(StoreNameType.Item, (int)action.Id, false);

                        rows.Add(row);
                    }
                }

                result += new QueryBuilder.SQLInsert("playercreateinfo_action", rows, 2).Build();
            }

            if (!Storage.StartPositions.IsEmpty() && Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.playercreateinfo))
            {
                var entries = Storage.StartPositions.Keys();
                var dataDb = SQLDatabase.GetDict<Race, Class, StartPosition>(entries, "race", "class");

                result += SQLUtil.CompareDicts(Storage.StartPositions, dataDb, StoreNameType.None, StoreNameType.None, "race", "class");
            }

            if (!Storage.StartSpells.IsEmpty() && Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.playercreateinfo_spell))
            {
                var rows = new List<QueryBuilder.SQLInsertRow>();
                foreach (var startSpells in Storage.StartSpells)
                {
                    var comment = new QueryBuilder.SQLInsertRow
                    {
                        HeaderComment = startSpells.Key.Item1 + " - " + startSpells.Key.Item2
                    };
                    rows.Add(comment);

                    foreach (var spell in startSpells.Value.Item1.Spells)
                    {
                        var row = new QueryBuilder.SQLInsertRow();

                        row.AddValue("race", startSpells.Key.Item1);
                        row.AddValue("class", startSpells.Key.Item2);
                        row.AddValue("Spell", spell);
                        row.AddValue("Note", StoreGetters.GetName(StoreNameType.Spell, (int)spell, false));

                        rows.Add(row);
                    }
                }

                result += new QueryBuilder.SQLInsert("playercreateinfo_spell", rows, 2).Build();
            }

            return result;
        }

        [BuilderMethod]
        public static string ObjectNames()
        {
            if (Storage.ObjectNames.IsEmpty())
                return string.Empty;

            const string tableName = "ObjectNames";

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.ObjectNames))
                return string.Empty;

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var data in Storage.ObjectNames)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("ObjectType", data.Value.Item1.ObjectType.ToString());
                row.AddValue("Id", data.Key);
                row.AddValue("Name", data.Value.Item1.Name);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, rows, 2, ignore: true, withDelete: false).Build();
        }

        [BuilderMethod]
        public static string SniffData()
        {
            if (Storage.SniffData.IsEmpty())
                return String.Empty;

            const string tableName = "SniffData";

            if (Settings.DumpFormat != DumpFormatType.SniffDataOnly)
                if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.SniffData) && !Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.SniffDataOpcodes))
                    return string.Empty;

            var rows = new HashSet<QueryBuilder.SQLInsertRow>((IEqualityComparer<QueryBuilder.SQLInsertRow>)new QueryBuilder.SQLInsertRow());
            foreach (var data in Storage.SniffData)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("Build", ClientVersion.Build);
                row.AddValue("SniffName", data.Item1.FileName);
                row.AddValue("ObjectType", data.Item1.ObjectType.ToString());
                row.AddValue("Id", data.Item1.Id);
                row.AddValue("Data", data.Item1.Data);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, rows.ToList(), ignore: true, withDelete: false).Build();
        }

        // Non-WDB data but nevertheless data that should be saved to gameobject_template
        [BuilderMethod(Gameobjects = true)]
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
                    template.Faction == 1629 || template.Faction == 2203 || template.Faction == 2204) // player factions
                    template.Faction = 0;

                template.Flags &= ~GameObjectFlag.Triggered;
                template.Flags &= ~GameObjectFlag.Damaged;
                template.Flags &= ~GameObjectFlag.Destroyed;

                templates.Add(goT.Key.GetEntry(), template);
            }

            var templatesDb = SQLDatabase.GetDict<uint, GameObjectTemplateNonWDB>(templates.Keys());
            return SQLUtil.CompareDicts(templates, templatesDb, StoreNameType.GameObject);
        }

        [BuilderMethod]
        public static string DefenseMessage()
        {
            if (Storage.DefenseMessages.IsEmpty())
                return String.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.defense_message))
                return string.Empty;

            const string tableName = "defense_message";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var message in Storage.DefenseMessages)
            {
                foreach (var messageValue in message.Value)
                {
                    var row = new QueryBuilder.SQLInsertRow();

                    var query = new StringBuilder(string.Format("SELECT Id FROM {1}.broadcast_text WHERE MaleText='{0}' OR FemaleText='{0}';", MySqlHelper.DoubleQuoteString(messageValue.Item1.Text), Settings.HotfixesDatabase));

                    string broadcastTextId = "";

                    if (Settings.DevMode)
                    {
                        using (var reader = SQLConnector.ExecuteQuery(query.ToString()))
                        {
                            if (reader != null)
                                while (reader.Read())
                                {
                                    var values = new object[1];
                                    var count = reader.GetValues(values);
                                    if (count != 1)
                                        break; // error in query

                                    if (!String.IsNullOrWhiteSpace(broadcastTextId))
                                        broadcastTextId += " - " + Convert.ToInt32(values[0]);
                                    else
                                        broadcastTextId += Convert.ToInt32(values[0]);
                                }
                        }

                    }

                    row.AddValue("ZoneId", message.Key);
                    row.AddValue("Id", "x", false, true);
                    row.AddValue("Text", messageValue.Item1.Text);
                    if (Settings.DevMode)
                        row.AddValue("BroadcastTextId", broadcastTextId);

                    rows.Add(row);
                }
            }

            return new QueryBuilder.SQLInsert(tableName, rows, 1, false).Build();
        }

        [BuilderMethod]
        public static string WeatherUpdates()
        {
            if (Storage.WeatherUpdates.IsEmpty())
                return String.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.weather_updates))
                return string.Empty;

            const string tableName = "weather_updates";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var weatherUpdate in Storage.WeatherUpdates)
            {
                var row = new QueryBuilder.SQLInsertRow();

                var weather = weatherUpdate.Item1;

                row.AddValue("map_id", weather.MapId);
                row.AddValue("zone_id", weather.ZoneId);
                row.AddValue("weather_state", (int)weather.State);
                row.AddValue("timestamp", weatherUpdate.Item2.HasValue ? weatherUpdate.Item2.Value.ToFormattedString() : "null");
                row.AddValue("grade", weather.Grade);
                row.AddValue("unk", weather.Unk);

                row.Comment = StoreGetters.GetName(StoreNameType.Map, (int)weather.MapId, false) +
                    " - " + weather.State + " - " + weather.Grade;
                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, rows, ignore: true, withDelete: false).Build();
        }
    }
}

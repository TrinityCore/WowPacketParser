using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.SQL.Builders
{
    public static class Miscellaneous
    {
        public static string StartInformation()
        {
            var result = String.Empty;

            if (!Storage.StartActions.IsEmpty() && Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.playercreateinfo_action))
            {
                var rows = new List<QueryBuilder.SQLInsertRow>();
                foreach (var startActions in Storage.StartActions)
                {
                    var comment = new QueryBuilder.SQLInsertRow();
                    comment.HeaderComment = startActions.Key.Item1 + " - " + startActions.Key.Item2;
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

                result = new QueryBuilder.SQLInsert("playercreateinfo_action", rows, 2).Build();
            }

            if (!Storage.StartPositions.IsEmpty() && Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.playercreateinfo))
            {
                var rows = new List<QueryBuilder.SQLInsertRow>();
                foreach (KeyValuePair<Tuple<Race, Class>, Tuple<StartPosition, TimeSpan?>> startPosition in Storage.StartPositions)
                {
                    var comment = new QueryBuilder.SQLInsertRow();
                    comment.HeaderComment = startPosition.Key.Item1 + " - " + startPosition.Key.Item2;
                    rows.Add(comment);

                    var row = new QueryBuilder.SQLInsertRow();

                    row.AddValue("race", startPosition.Key.Item1);
                    row.AddValue("class", startPosition.Key.Item2);
                    row.AddValue("map", startPosition.Value.Item1.Map);
                    row.AddValue("zone", startPosition.Value.Item1.Zone);
                    row.AddValue("position_x", startPosition.Value.Item1.Position.X);
                    row.AddValue("position_y", startPosition.Value.Item1.Position.Y);
                    row.AddValue("position_z", startPosition.Value.Item1.Position.Z);

                    row.Comment = StoreGetters.GetName(StoreNameType.Map, startPosition.Value.Item1.Map, false) + " - " +
                                  StoreGetters.GetName(StoreNameType.Zone, startPosition.Value.Item1.Zone, false);

                    rows.Add(row);
                }

                result += new QueryBuilder.SQLInsert("playercreateinfo", rows, 2).Build();
            }

            if (!Storage.StartSpells.IsEmpty() && Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.playercreateinfo_spell))
            {
                var rows = new List<QueryBuilder.SQLInsertRow>();
                foreach (var startSpells in Storage.StartSpells)
                {
                    var comment = new QueryBuilder.SQLInsertRow();
                    comment.HeaderComment = startSpells.Key.Item1 + " - " + startSpells.Key.Item2;
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

                result = new QueryBuilder.SQLInsert("playercreateinfo_spell", rows, 2).Build();
            }

            return result;
        }

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

        public static string SniffData()
        {
            if (Storage.SniffData.IsEmpty())
                return String.Empty;

            const string tableName = "SniffData";

            if (Settings.DumpFormat != DumpFormatType.SniffDataOnly)
                if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.SniffData) && !Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.SniffDataOpcodes))
                    return string.Empty;

            var rows = new List<QueryBuilder.SQLInsertRow>();
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

            return new QueryBuilder.SQLInsert(tableName, rows, ignore: true, withDelete: false).Build();
        }

        // Non-WDB data but nevertheless data that should be saved to gameobject_template
        public static string GameobjectTemplateNonWDB(Dictionary<Guid, GameObject> gameobjects)
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

                templates.Add(goT.Key.GetEntry(), template, null);
            }

            var templatesDb = SQLDatabase.GetDict<uint, GameObjectTemplateNonWDB>(templates.Keys());
            return SQLUtil.CompareDicts(templates, templatesDb, StoreNameType.GameObject);
        }
    }
}

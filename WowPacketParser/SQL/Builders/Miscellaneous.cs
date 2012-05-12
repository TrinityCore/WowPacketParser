using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL.Builders
{
    public static class Miscellaneous
    {
        public static string StartInformation()
        {
            var result = String.Empty;

            if (!Storage.StartActions.IsEmpty())
            {
                var rows = new List<QueryBuilder.SQLInsertRow>();
                foreach (KeyValuePair<Tuple<Race, Class>, Tuple<StartAction, TimeSpan?>> startActions in Storage.StartActions)
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

            if (!Storage.StartPositions.IsEmpty())
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

            if (!Storage.StartSpells.IsEmpty())
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
                return String.Empty;

            const string tableName = "ObjectNames";

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

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var data in Storage.SniffData)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("Build", ClientVersion.Build);
                row.AddValue("SniffName", data.Item1.FileName);
                row.AddValue("TimeStamp", data.Item1.TimeStamp);
                row.AddValue("ObjectType", data.Item1.ObjectType.ToString());
                row.AddValue("Id", data.Item1.Id);
                row.AddValue("Data", data.Item1.Data);
                row.AddValue("Number", data.Item1.Number);

                if (data.Item1.ObjectType == StoreNameType.Opcode)
                    row.Comment = Opcodes.GetOpcodeName(data.Item1.Id);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, rows, ignore: true, withDelete: false).Build();
        }
    }
}

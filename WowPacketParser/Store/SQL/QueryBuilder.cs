using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.SQL
{
    interface ISQLQuery
    {
        string Build();
    }

    static class QueryBuilder
    {
        public class SQLUpdate : ISQLQuery
        {
            private string Table { get; set; }
            private List<SQLUpdateRow> Rows { get; set; }
            private List<string> TableStructure { get; set; }

            public SQLUpdate(List<SQLUpdateRow> rows)
            {
                Rows = rows;
            }

            public string Build()
            {
                var result = new StringBuilder();

                var rowsStrings = Rows.Select(row => row.Build()).ToList();

                foreach (var rowString in rowsStrings)
                    result.Append(rowString);

                result.Append(Environment.NewLine);

                return result.ToString();
            }
        }

        public class SQLUpdateRow : ISQLQuery
        {
            public string Comment { get; set; }
            public string HeaderComment { get; set; }
            public bool CommentOut { get; set; }

            private readonly List<KeyValuePair<string, object>> _values =
            new List<KeyValuePair<string, object>>();

            private readonly List<KeyValuePair<string, object>> _whereClause =
                new List<KeyValuePair<string, object>>();

            public void AddValue(string name, object value)
            {
                _values.Add(new KeyValuePair<string, object>(SQLUtil.AddBackQuotes(name), value));
            }

            public void AddWhere(string name, object value)
            {
                _whereClause.Add(new KeyValuePair<string, object>(SQLUtil.AddBackQuotes(name), value));
            }

            private string _table;

            public string Table
            {
                get { return SQLUtil.AddBackQuotes(_table); }
                set { _table = value; }
            }

            public string Build()
            {

                if (!String.IsNullOrWhiteSpace(HeaderComment))
                    return "-- " + HeaderComment + Environment.NewLine;

                var row = new StringBuilder();
                if (CommentOut)
                    row.Append("-- ");

                // Return empty if there are no values or where clause or no table name set
                if (_values.Count == 0 || _whereClause.Count == 0 || Table == string.Empty)
                    return string.Empty;

                row.Append("UPDATE ");
                row.Append(Table);
                row.Append(" SET ");

                var iter = 0;
                foreach (var values in _values)
                {
                    iter++;
                    row.Append(values.Key);
                    row.Append(" = ");
                    row.Append(values.Value);
                    if (_values.Count != iter)
                        row.Append(SQLUtil.CommaSeparator);
                }

                row.Append(" WHERE ");

                iter = 0;
                foreach (var whereClause in _whereClause)
                {
                    iter++;
                    row.Append(whereClause.Key);
                    row.Append(" = ");
                    row.Append(whereClause.Value);
                    if (_whereClause.Count != iter)
                        row.Append(" AND ");
                }

                row.Append(";");

                if (!String.IsNullOrWhiteSpace(Comment))
                    row.Append(" -- " + Comment);
                row.Append(Environment.NewLine);

                return row.ToString();
            }
        }

        public class SQLInsert : ISQLQuery
        {
            private string Table { get; set; }
            private List<SQLInsertRow> Rows { get; set; }
            private SQLDelete Delete { get; set; }
            private SQLInsertHeader InsertHeader { get; set; }
            private List<string> TableStructure { get; set; }

            // Without delete
            public SQLInsert(string table, List<SQLInsertRow> rows, bool ignore = false)
            {
                Table = table;
                Rows = rows;
                Delete = null;

                TableStructure = new List<string>();
                var firstProperRow = Rows.Find(row => String.IsNullOrWhiteSpace(row.HeaderComment));
                if (firstProperRow != null)
                    TableStructure = firstProperRow.FieldNames;

                InsertHeader = new SQLInsertHeader(Table, TableStructure, ignore);
            }

            // Single primary key
            public SQLInsert(string table, ICollection<uint> values, string primaryKey,
                             List<SQLInsertRow> rows)
            {
                Table = table;
                Rows = rows;

                TableStructure = new List<string>();
                var firstProperRow = Rows.Find(row => String.IsNullOrWhiteSpace(row.HeaderComment));
                if (firstProperRow != null)
                    TableStructure = firstProperRow.FieldNames;

                Delete = new SQLDelete(values, primaryKey, Table);
                InsertHeader = new SQLInsertHeader(Table, TableStructure);
            }

            // Double primary key
            public SQLInsert(string table, ICollection<Tuple<uint, uint>> values, ICollection<string> primaryKeys,
                             List<SQLInsertRow> rows)
            {
                Table = table;
                Rows = rows;

                TableStructure = new List<string>();
                var firstProperRow = Rows.Find(row => String.IsNullOrWhiteSpace(row.HeaderComment));
                if (firstProperRow != null)
                    TableStructure = firstProperRow.FieldNames;

                Delete = new SQLDelete(values, primaryKeys, Table);
                InsertHeader = new SQLInsertHeader(Table, TableStructure);
            }

            public string Build()
            {
                // If we only have rows with comment, do not print any query
                if (Rows.All(row => !String.IsNullOrWhiteSpace(row.HeaderComment)))
                    return "-- " + SQLUtil.AddBackQuotes(Table) + " has empty data." + Environment.NewLine;

                var result = new StringBuilder();

                if (Delete != null)
                    result.Append(Delete.Build());
                result.Append(InsertHeader.Build());

                var rowsStrings = Rows.Select(row => row.Build()).ToList();

                int cnt = 0;
                foreach (var rowString in rowsStrings)
                {
                    if (cnt >= 500)
                    {
                        result.ReplaceLast(',', ';');
                        result.Append(InsertHeader.Build());
                        cnt = 0;
                    }
                    result.Append(rowString);
                    cnt++;
                }

                result.Append(Environment.NewLine);

                return result.ReplaceLast(',', ';').ToString();
            }
        }

        public class SQLInsertRow : ISQLQuery
        {
            private readonly List<string> _row = new List<string>();
            public string Comment { get; set; }
            public string HeaderComment { get; set; }
            public readonly List<string> FieldNames = new List<string>();
            public bool CommentOut { get; set; }

            public void AddValue(string field, object value, bool isFlag = false, bool noQuotes = false)
            {
                if (value == null)
                    return;

                if (!noQuotes && value is string)
                    value = SQLUtil.Stringify(value);

                if (value is bool)
                    value = value.Equals(true) ? 1 : 0;

                if (value is Enum) // A bit hackish but oh well...
                {
                    try
                    {
// ReSharper disable PossibleInvalidCastException
                        value = (int)value;
// ReSharper restore PossibleInvalidCastException
                    }
                    catch (InvalidCastException)
                    {
                        value = (uint)value;
                    }
                }

                if (value is int && isFlag)
                    value = SQLUtil.Hexify((int) value);

                _row.Add(value.ToString());
                FieldNames.Add(field);
            }

            public string Build()
            {
                if (!String.IsNullOrWhiteSpace(HeaderComment))
                    return "-- " + HeaderComment + Environment.NewLine;

                var row = new StringBuilder();
                if (CommentOut)
                    row.Append("-- ");

                row.Append("(");

                var iter = 0;
                foreach (var value in _row)
                {
                    iter++;
                    row.Append(value);

                    row.Append(_row.Count != iter ? SQLUtil.CommaSeparator : "),");
                }
                if (!String.IsNullOrWhiteSpace(Comment))
                    row.Append(" -- " + Comment);
                row.Append(Environment.NewLine);

                return row.ToString();
            }
        }

        public class SQLDelete : ISQLQuery
        {
            // TODO: Support any number of values (only single and double supported atm)
            private readonly bool _double;
            private ICollection<uint> Values { get; set; }
            private ICollection<Tuple<uint, uint>> ValuesDouble { get; set; }
            private string Table { get; set; }
            private ICollection<string> PrimaryKeys { get; set; }
            private readonly bool _between;
            private string Prefix { get; set; }

            public SQLDelete(ICollection<uint> values, string primaryKey, string tableName)
            {
                PrimaryKeys = new[] {primaryKey};
                Table = tableName;

                Values = values;
                _double = false;
                _between = false;
            }

            public SQLDelete(ICollection<Tuple<uint, uint>> values, ICollection<string> primaryKeys, string tableName)
            {
                PrimaryKeys = primaryKeys;
                Table = tableName;

                ValuesDouble = values;
                _double = true;
                _between = false;
            }

            public SQLDelete(Tuple<uint, uint> values, string primaryKey, string tableName, string prefix = null)
            {
                PrimaryKeys = new[] {primaryKey};
                Table = tableName;

                ValuesDouble = new[] {values};
                _double = true;
                _between = true;
                Prefix = prefix;
            }

            public string Build()
            {
                var query = new StringBuilder();

                query.Append("DELETE FROM ");
                query.Append(SQLUtil.AddBackQuotes(Table));
                query.Append(" WHERE ");

                if (_between)
                {
                    var keys = PrimaryKeys.ToArray();
                    var tuple = ValuesDouble.First();
                    query.Append(SQLUtil.AddBackQuotes(keys[0]));
                    query.Append(" BETWEEN ");
                    if (Prefix != null)
                        query.Append(Prefix);
                    query.Append(tuple.Item1);
                    query.Append(" AND ");
                    if (Prefix != null)
                        query.Append(Prefix);
                    query.Append(tuple.Item2);
                    query.Append(";");
                }
                else
                {
                    if (_double)
                    {
                        var iter = 0;
                        var keys = PrimaryKeys.ToArray();
                        foreach (var tuple in ValuesDouble)
                        {
                            iter++;
                            query.Append("(");
                            query.Append(SQLUtil.AddBackQuotes(keys[0]));
                            query.Append("=");
                            query.Append(tuple.Item1);
                            query.Append(" AND ");
                            query.Append(SQLUtil.AddBackQuotes(keys[1]));
                            query.Append("=");
                            query.Append(tuple.Item2);
                            query.Append(")");
                            if (ValuesDouble.Count != iter)
                                query.Append(" OR ");
                        }
                        query.Append(";");
                    }
                    else
                    {
                        query.Append(SQLUtil.AddBackQuotes(PrimaryKeys.First()));
                        query.Append(Values.Count == 1 ? " = " : " IN (");

                        var iter = 0;
                        foreach (var entry in Values)
                        {
                            iter++;
                            query.Append(entry);
                            if (Values.Count != iter)
                                query.Append(SQLUtil.CommaSeparator);
                        }
                        query.Append(");");
                    }
                }

                query.Append(Environment.NewLine);
                return query.ToString();
            }
        }

        private class SQLInsertHeader : ISQLQuery
        {
            private string Table { get; set; }
            private ICollection<string> TableStructure { get; set; }
            private bool Ignore { get; set; }

            public SQLInsertHeader(string table, ICollection<string> tableStructure, bool ignore = false)
            {
                Table = table;
                TableStructure = tableStructure;
                Ignore = ignore;
            }

            public string Build()
            {
                var insertCommand = "INSERT " +
                    (Ignore ? "IGNORE " : string.Empty) + "INTO";

                var result = insertCommand + " " + SQLUtil.AddBackQuotes(Table);

                if (TableStructure.Count != 0)
                {

                    result += " (";

                    var iter = 0;
                    foreach (var column in TableStructure)
                    {
                        iter++;
                        result += SQLUtil.AddBackQuotes(column);
                        if (TableStructure.Count != iter)
                            result += SQLUtil.CommaSeparator;
                    }
                    result += ")";
                }

                result += " VALUES" + Environment.NewLine;

                return result;
            }
        }
    }
}

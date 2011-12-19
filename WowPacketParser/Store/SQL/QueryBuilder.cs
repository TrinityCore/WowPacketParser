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
        public class SQLInsert : ISQLQuery
        {
            private string Table { get; set; }
            private List<SQLInsertRow> Rows { get; set; }
            private SQLDelete Delete { get; set; }
            private SQLInsertHeader InsertHeader { get; set; }
            private List<string> TableStructure { get; set; }

            public SQLInsert(string table, List<SQLInsertRow> rows, bool ignore = false)
            {
                Table = table;
                Rows = rows;
                Delete = null;

                TableStructure = Rows.First().FieldNames;
                InsertHeader = new SQLInsertHeader(Table, TableStructure, ignore);
                
            }

            public SQLInsert(string table, ICollection<Object> values, ICollection<string> primaryKeys, List<SQLInsertRow> rows, bool ignore = false)
            {
                Table = table;
                Rows = rows;

                TableStructure = Rows.First().FieldNames;
                Delete = new SQLDelete(values, primaryKeys, Table);
                InsertHeader = new SQLInsertHeader(Table, TableStructure, ignore);
            }

            public string Build()
            {
                var result = new StringBuilder();

                if (Delete != null)
                    result.Append(Delete.Build());
                result.Append(InsertHeader.Build());

                foreach (var row in Rows)
                    result.Append(row.Build());

                return result.ReplaceLast(',', ';').ToString();
            }
        }

        private class SQLDelete : ISQLQuery
        {
            // TODO: Support any number of values (only single and double supported atm)
            private readonly bool _double;
            private ICollection<uint> Values { get; set; }
            private ICollection<Tuple<uint, uint>> ValuesDouble { get; set; }
            private string Table { get; set; }
            private ICollection<string> PrimaryKeys { get; set; }

            public SQLDelete(ICollection<Object> values, ICollection<string> primaryKeys, string tableName)
            {
                PrimaryKeys = primaryKeys;
                Table = tableName;

                if (values is ICollection<uint> || values is ICollection<int>)
                {
                    Values = (ICollection<uint>) values;
                    _double = false;
                }
                else
                {
                    ValuesDouble = (ICollection<Tuple<uint, uint>>) values;
                    _double = true;
                }
            }

            public string Build()
            {
                var query = new StringBuilder();

                query.Append("DELETE FROM ");
                query.Append(SQLUtil.AddBackQuotes(Table));
                query.Append(" WHERE ");

                if (_double)
                {
                    var iter = 0;
                    foreach (var tuple in ValuesDouble)
                    {
                        iter++;
                        query.Append("(");
                        query.Append(SQLUtil.AddBackQuotes(PrimaryKeys.ToArray()[0]));
                        query.Append("=");
                        query.Append(tuple.Item1);
                        query.Append(" AND ");
                        query.Append(SQLUtil.AddBackQuotes(PrimaryKeys.ToArray()[1]));
                        query.Append("=");
                        query.Append(tuple.Item2);
                        query.Append(")");
                        if (Values.Count != iter)
                            query.Append(" OR ");
                    }
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
                }

                query.Append(");");
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
                    (Ignore ? string.Empty : "IGNORE ") + "INTO";

                var result = insertCommand + " " + SQLUtil.AddBackQuotes(Table) + " (";
                var iter = 0;
                foreach (var column in TableStructure)
                {
                    iter++;
                    result += SQLUtil.AddBackQuotes(column);
                    if (TableStructure.Count != iter)
                        result += SQLUtil.CommaSeparator;
                }
                result += ") VALUES" + Environment.NewLine;

                return result;
            }
        }

        public class SQLInsertRow : ISQLQuery
        {
            private readonly List<string> _row = new List<string>();
            public string Comment { get; set; }
            public readonly List<string> FieldNames = new List<string>();

            public void AddValue(string field, object value, bool isFlag = false)
            {
                if (value == null)
                    return;

                if (value is string)
                    value = SQLUtil.Stringify(value);

                if ((value is int || value is Enum) && isFlag)
                    value = SQLUtil.Hexify((int)value);

                _row.Add(value.ToString());
                FieldNames.Add(field);
            }

            public string Build()
            {
                var row = new StringBuilder();
                row.Append("(");

                foreach (var value in _row)
                {
                    row.Append(value);
                    row.Append(_row.Last() != value ? SQLUtil.CommaSeparator : "),");
                    if (!String.IsNullOrWhiteSpace(Comment))
                        row.Append(" -- " + Comment);
                }
                row.Append(Environment.NewLine);

                return row.ToString();
            }
        }
    }
}

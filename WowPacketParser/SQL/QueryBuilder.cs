using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WowPacketParser.SQL
{
    static class QueryBuilder
    {
        public class SQLUpdate : ISQLQuery
        {
            private List<SQLUpdateRow> Rows { get; set; }

            /// <summary>
            /// Creates multiple update rows
            /// </summary>
            /// <param name="rows">A list of <see cref="SQLUpdateRow"/> rows</param>
            public SQLUpdate(List<SQLUpdateRow> rows)
            {
                Rows = rows;
            }

            /// <summary>
            /// Constructs the actual query
            /// </summary>
            /// <returns>Multiple update rows</returns>
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
            /// <summary>
            /// <para>Comment appended to the values</para>
            /// <code>UPDATE...; -- this comment</code>
            /// </summary>
            public string Comment { private get; set; }

            /// <summary>
            /// <para>The row will be commented out</para>
            /// <code>-- UPDATE..., </code>
            /// </summary>
            public bool CommentOut { get; set; }

            private readonly List<KeyValuePair<string, object>> _values =
            new List<KeyValuePair<string, object>>();

            private readonly List<KeyValuePair<string, uint>> _whereClause =
                new List<KeyValuePair<string, uint>>();

            /// <summary>
            /// Returns the amount of values
            /// </summary>
            public int ValueCount
            {
                get { return _values.Count; }
            }

            /// <summary>
            /// Adds a field-value pair to be updated
            /// </summary>
            /// <param name="field">The field name associated with the value</param>
            /// <param name="value">Any value (string, number, enum, ...)</param>
            /// <param name="isFlag">If set to true the value, "0x" will be append to value</param>
            /// <param name="noQuotes">If value is a string and this is set to true, value will not be 'quoted' (SQL variables)</param>
            public void AddValue(string field, object value, bool isFlag = false, bool noQuotes = false)
            {
                if (value == null)
                    value = "";

                _values.Add(new KeyValuePair<string, object>(SQLUtil.AddBackQuotes(field), SQLUtil.ToSQLValue(value, isFlag, noQuotes)));
            }

            /// <summary>
            /// Adds a field-value pair to be updated. If value is equal to defaultValue, value will NOT be added to this update row
            /// </summary>
            /// <param name="field">The field name associated with the value</param>
            /// <param name="value">Any value (string, number, enum, ...)</param>
            /// <param name="defaultValue">Default value (usually defined in database structure)</param>
            /// <param name="isFlag">If set to true the value, "0x" will be append to value</param>
            /// <param name="noQuotes">If value is a string and this is set to true, value will not be 'quoted' (SQL variables)</param>
            public void AddValue<T>(string field, T value, T defaultValue, bool isFlag = false, bool noQuotes = false)
            {
                // T used because it is compile time safe. We know that value and defaultValue got the same type

// ReSharper disable CompareNonConstrainedGenericWithNull
                if (value == null)
// ReSharper restore CompareNonConstrainedGenericWithNull
                    return;

                if (value is float || value is double)
                {
                    if (Math.Abs(Convert.ToDouble(value) - Convert.ToDouble(defaultValue)) < 0.000001)
                        return;
                }

                if (value.Equals(defaultValue))
                    return;

                _values.Add(new KeyValuePair<string, object>(SQLUtil.AddBackQuotes(field), SQLUtil.ToSQLValue(value, isFlag, noQuotes)));
            }

            /// <summary>
            /// Adds to this row what will be updated
            /// </summary>
            /// <param name="field">The field name associated with the value</param>
            /// <param name="value">The value used in the where clause</param>
            public void AddWhere(string field, uint value)
            {
                _whereClause.Add(new KeyValuePair<string, uint>(SQLUtil.AddBackQuotes(field), value));
            }

            private string _table;

            /// <summary>
            /// The table that will be updated
            /// </summary>
            public string Table
            {
                private get { return SQLUtil.AddBackQuotes(_table); }
                set { _table = value; }
            }

            /// <summary>
            /// Constructs the actual query
            /// </summary>
            /// <returns>A single update query</returns>
            public string Build()
            {
                var row = new StringBuilder();
                if (CommentOut)
                    row.Append("-- ");

                // Return empty if there are no values or where clause or no table name set
                if (_values.Count == 0 || _whereClause.Count == 0 || string.IsNullOrEmpty(Table))
                    return string.Empty;

                row.Append("UPDATE ");
                row.Append(Table);
                row.Append(" SET ");

                var count = 0;
                foreach (var values in _values)
                {
                    count++;
                    row.Append(values.Key);
                    row.Append("=");
                    row.Append(values.Value);
                    if (_values.Count != count)
                        row.Append(SQLUtil.CommaSeparator);
                }

                row.Append(" WHERE ");

                count = 0;
                foreach (var whereClause in _whereClause)
                {
                    count++;
                    row.Append(whereClause.Key);
                    row.Append("=");
                    row.Append(whereClause.Value);
                    if (_whereClause.Count != count)
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
            private string Delete { get; set; }
            private string InsertHeader { get; set; }
            private List<string> TableStructure { get; set; }
            private readonly bool _deleteDuplicates;

            // Add a new insert header every 500 rows
            private const int MaxRowsPerInsert = 500;

            /// <summary>
            /// Creates an insert query including the insert header and its rows
            /// Single primary key (for delete)
            /// </summary>
            /// <param name="tableName">Table name</param>
            /// <param name="rows">A list of <see cref="SQLInsertRow"/> rows</param>
            /// <param name="primaryKeyNumber">The number of primary keys. Only 1 and 2 supported.</param>
            /// <param name="withDelete">If set to false the full query will not include a delete query</param>
            /// <param name="ignore">If set to true the INSERT INTO query will be INSERT IGNORE INTO</param>
            /// <param name="deleteDuplicates">If set to true duplicated rows will be removed from final query</param>
            public SQLInsert(string tableName, List<SQLInsertRow> rows, int primaryKeyNumber = 1, bool withDelete = true, bool ignore = false, bool deleteDuplicates = true)
            {
                Table = tableName;
                Rows = rows;
                _deleteDuplicates = deleteDuplicates;

                if (Rows.Count == 0)
                    return;

                // Get the field names from the first row that is not a comment
                TableStructure = new List<string>();
                var firstProperRow = Rows.Find(row => !row.NoData);
                if (firstProperRow != null)
                    TableStructure = firstProperRow.FieldNames;
                else
                    return; // empty insert

                InsertHeader = new SQLInsertHeader(Table, TableStructure, ignore).Build();

                if (!withDelete)
                {
                    Delete = string.Empty;
                    return;
                }

                if (primaryKeyNumber == 1)
                {
                    ICollection<string> values =
                        rows.FindAll(row => !row.NoData).Select(row => row.GetPrimaryKeysValues(primaryKeyNumber).First()).ToArray();
                    var primaryKey = TableStructure[0];

                    Delete = new SQLDelete(values, primaryKey, Table).Build();
                }
                else if (primaryKeyNumber == 2)
                {
                    ICollection<Tuple<string, string>> values =
                        rows.FindAll(row => !row.NoData).Select(row => row.GetPrimaryKeysValues(primaryKeyNumber)).
                            Select(vals => Tuple.Create(vals[0], vals[1])).ToArray();

                    var primaryKeys = Tuple.Create(TableStructure[0], TableStructure[1]);

                    Delete = new SQLDelete(values, primaryKeys, tableName).Build();
                }
                else
                    throw new ArgumentOutOfRangeException("primaryKeyNumber");
            }

            /// <summary>
            /// Constructs the actual query
            /// </summary>
            /// <returns>Full insert AND delete queries</returns>
            public string Build()
            {
                // If we only have rows with comment, do not print any query
                if (Rows.All(row => row.NoData)) // still true if row count = 0
                    return "-- " + SQLUtil.AddBackQuotes(Table) + " has empty data." + Environment.NewLine;

                var query = new StringBuilder();

                query.Append(Delete); // Can be empty
                query.Append(InsertHeader);

                var count = 0;
                foreach (var row in Rows)
                {
                    if (count >= MaxRowsPerInsert && !_deleteDuplicates)
                    {
                        query.ReplaceLast(',', ';');
                        query.Append(InsertHeader);
                        count = 0;
                    }
                    query.Append(row.Build());
                    count++;
                }

                query.Append(Environment.NewLine);

                // This is easier to implement that comparing raw objects in each row
                // and certainly faster. Imagine comparing 1k rows of <string, int, int, emote, YouGotIt>
                if (_deleteDuplicates)
                {
                    var str = String.Join("\n", query.ToString().Split('\n').Distinct()); // Do not use Enviroment.NewLine
                    query.Clear();
                    query.Append(str);
                }

                query.ReplaceLast(',', ';');

                return query.ToString();
            }
        }

        public class SQLInsertRow : ISQLQuery
        {
            public readonly List<string> FieldNames = new List<string>();
            private readonly List<string> _values = new List<string>();

            // Assuming that the first <count> values will be the primary key
            public List<string> GetPrimaryKeysValues(int count = 1)
            {

                if (_values.Count < count)
                {
                    // If our row does not have the request number
                    // of values, forge some

                    var list = new List<string>();
                    for (var i = 0; i < count; i++)
                        list.Add("Unknown" + i);
                    return list;
                }

                return _values.GetRange(0, count);
            }

            private string _headerComment;

            /// <summary>
            /// <para>Comment appended to the values</para>
            /// <code>(a, ..., z), -- this comment</code>
            /// </summary>
            public string Comment { get; set; }

            /// <summary>
            /// <para>The comment that will replace the values</para>
            /// <code>-- this comment</code>
            /// </summary>
            public string HeaderComment
            {
                set
                {
                    _headerComment = value;
                    NoData = true;
                }
            }

            /// <summary>
            /// Row has no data, it is just a comment
            /// </summary>
            public bool NoData { get; private set; }

            /// <summary>
            /// <para>The row will be commented out</para>
            /// <code>-- (a, ..., z), </code>
            /// </summary>
            public bool CommentOut { get; set; }

            /// <summary>
            /// <para>Adds a value to this row</para>
            /// <remarks>null value will be 0</remarks>
            /// </summary>
            /// <param name="field">The field name associated with the value</param>
            /// <param name="value">Any value (string, number, enum, ...)</param>
            /// <param name="isFlag">If set to true the value, "0x" will be append to value</param>
            /// <param name="noQuotes">If value is a string and this is set to true, value will not be 'quoted' (SQL variables)</param>
            public void AddValue(string field, object value, bool isFlag = false, bool noQuotes = false)
            {
                if (value == null)
                    value = 0;

                _values.Add(SQLUtil.ToSQLValue(value, isFlag, noQuotes).ToString());
                FieldNames.Add(field);
            }

            /// <summary>
            /// Constructs the actual query
            /// </summary>
            /// <returns>Single insert row (data)</returns>
            public string Build()
            {
                if (NoData)
                    return "-- " + _headerComment + Environment.NewLine;

                var row = new StringBuilder();
                if (CommentOut)
                    row.Append("-- ");

                row.Append("(");

                for (var i = 0; i < _values.Count; ++i)
                {
                    row.Append(_values[i]);

                    // Append parenthesis if end of values
                    row.Append(_values.Count - 1 != i ? SQLUtil.CommaSeparator : "),");
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
            private readonly bool _between;

            private ICollection<string> Values { get; set; }
            private ICollection<Tuple<string, string>> ValuesDouble { get; set; }
            private Tuple<string, string> ValuesBetween { get; set; }

            private string Table { get; set; }

            private string PrimaryKey { get; set; }
            private Tuple<string, string> PrimaryKeys { get; set; }

            /// <summary>
            /// <para>Creates a delete query with a single primary key and an arbitrary number of values</para>
            /// <code>DELETE FROM `tableName` WHERE `primaryKey` IN (values[0], ..., values[n]);</code>
            /// <code>DELETE FROM `tableName` WHERE `primaryKey`=values[0];</code>
            /// </summary>
            /// <param name="values">Collection of values to be deleted</param>
            /// <param name="primaryKey">Field used in the WHERE clause</param>
            /// <param name="tableName">Table name</param>
            public SQLDelete(ICollection<string> values, string primaryKey, string tableName)
            {
                PrimaryKey = primaryKey;
                Table = tableName;

                Values = values;
                _double = false;
                _between = false;
            }

            /// <summary>
            /// <para>Creates a delete query that uses two primary keys and an arbitrary number of values</para>
            /// <code>DELETE FROM `tableName` WHERE (`primaryKeys[0]`=values[0][0] AND `primaryKeys[1]`=values[1][0]) AND ...);</code>
            /// </summary>
            /// <param name="values">Collection of pairs of values to be deleted</param>
            /// <param name="primaryKeys">Collection of pairs of fields used in the WHERE clause</param>
            /// <param name="tableName">Table name</param>
            public SQLDelete(ICollection<Tuple<string, string>> values, Tuple<string, string> primaryKeys, string tableName)
            {
                PrimaryKeys = primaryKeys;
                Table = tableName;

                ValuesDouble = values;
                _double = true;
                _between = false;
            }

            /// <summary>
            /// <para>Creates a delete query that deletes between two values</para>
            /// <code>DELETE FROM `tableName` WHERE `primaryKey` BETWEEN values[0] AND values[n];</code>
            /// </summary>
            /// <param name="values">Pair of values</param>
            /// <param name="primaryKey">Field used in the WHERE clause</param>
            /// <param name="tableName">Table name</param>
            public SQLDelete(Tuple<string, string> values, string primaryKey, string tableName)
            {
                PrimaryKey = primaryKey;
                Table = tableName;

                ValuesBetween = values;
                _double = true;
                _between = true;
            }

            /// <summary>
            /// Constructs the actual query
            /// </summary>
            /// <returns>Delete query</returns>
            public string Build()
            {
                var query = new StringBuilder();

                query.Append("DELETE FROM ");
                query.Append(SQLUtil.AddBackQuotes(Table));
                query.Append(" WHERE ");

                if (_between)
                {
                    query.Append(SQLUtil.AddBackQuotes(PrimaryKey));
                    query.Append(" BETWEEN ");
                    query.Append(ValuesBetween.Item1);
                    query.Append(" AND ");
                    query.Append(ValuesBetween.Item2);
                    query.Append(";");
                }
                else
                {
                    if (_double)
                    {
                        var counter = 0;
                        foreach (var tuple in ValuesDouble)
                        {
                            counter++;
                            query.Append("(");
                            query.Append(SQLUtil.AddBackQuotes(PrimaryKeys.Item1));
                            query.Append("=");
                            query.Append(tuple.Item1);
                            query.Append(" AND ");
                            query.Append(SQLUtil.AddBackQuotes(PrimaryKeys.Item2));
                            query.Append("=");
                            query.Append(tuple.Item2);
                            query.Append(")");
                            // Append an OR if not end of items
                            if (ValuesDouble.Count != counter)
                                query.Append(" OR ");
                        }
                        query.Append(";");
                    }
                    else
                    {
                        query.Append(SQLUtil.AddBackQuotes(PrimaryKey));
                        query.Append(Values.Count == 1 ? "=" : " IN (");

                        var counter = 0;
                        foreach (var entry in Values)
                        {
                            counter++;
                            query.Append(entry);
                            // Append comma if not end of items
                            if (Values.Count != counter)
                                query.Append(SQLUtil.CommaSeparator);
                            else if (Values.Count != 1)
                                query.Append(")");
                        }
                        query.Append(";");

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

            /// <summary>
            /// <para>Creates the header of an INSERT query</para>
            /// <code>INSERT INTO `tableName` (fields[0], ..., fields[n]) VALUES</code>
            /// </summary>
            /// <param name="tableName">Table name</param>
            /// <param name="fields">Field names</param>
            /// <param name="ignore">If set to true the INSERT INTO query will be INSERT IGNORE INTO</param>
            public SQLInsertHeader(string tableName, ICollection<string> fields, bool ignore = false)
            {
                Table = tableName;
                TableStructure = fields;
                Ignore = ignore;
            }

            /// <summary>
            /// Constructs the actual query
            /// </summary>
            /// <returns>Insert query header</returns>
            public string Build()
            {
                var query = new StringBuilder();

                query.Append("INSERT ");
                query.Append(Ignore ? "IGNORE " : string.Empty);
                query.Append("INTO ");
                query.Append(SQLUtil.AddBackQuotes(Table));

                if (TableStructure.Count != 0)
                {
                    query.Append(" (");

                    var count = 0;
                    foreach (var column in TableStructure)
                    {
                        count++;
                        query.Append(SQLUtil.AddBackQuotes(column));
                        // Append comma if not end of fields
                        if (TableStructure.Count != count)
                            query.Append(SQLUtil.CommaSeparator);
                    }

                    query.Append(")");
                }

                query.Append(" VALUES" + Environment.NewLine);

                return query.ToString();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowPacketParser.Misc;

namespace WowPacketParser.SQL
{
    static class QueryBuilder
    {
        public class DictionaryComparer : IEqualityComparer<Dictionary<string, object>>
        {
            public bool Equals(Dictionary<string, object> x, Dictionary<string, object> y)
            {
                return x.DictionaryEqual(y);
            }

            public int GetHashCode(Dictionary<string, object> obj)
            {
                // bad impl 2.0
                return obj.Aggregate(0, (current, o) => current ^ o.GetHashCode());
            }
        }

        public class SQLUpdate : ISQLQuery
        {
            private List<SQLUpdateRow> Rows { get; set; }

            /// <summary>
            /// Creates multiple update rows
            /// </summary>
            /// <param name="rows">A list of <see cref="SQLUpdateRow"/> rows</param>
            public SQLUpdate(List<SQLUpdateRow> rows)
            {
                Rows = new List<SQLUpdateRow>();
                if (rows.Count == 0)
                    return;

                Rows.AddRange(rows.Where(row => row.WhereClause.Count != 1));

                var vals = rows
                    .Where(row => row.WhereClause.Count == 1)
                    .GroupBy(row => row.Values, row => row, new DictionaryComparer());

                foreach (IGrouping<Dictionary<string, object>, SQLUpdateRow> grouping in vals)
                {
                    if (!grouping.Any())
                        continue;

                    if (grouping.First().WhereClause.Count == 0)
                        continue;

                    var updateRow = new SQLUpdateRowMultiple1(grouping.First().WhereClause[0].Key)
                    {
                        Table = SQLUtil.RemoveBackQuotes(grouping.First().Table)
                    };

                    foreach (var value in grouping.First().Values)
                        updateRow.AddValue(SQLUtil.RemoveBackQuotes(value.Key), value.Value);

                    foreach (var row in grouping)
                    {
                        foreach (var value in row.WhereClause)
                            updateRow.AddWhere(value.Value, row.Comment);
                    }

                    Rows.Add(updateRow);
                }
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

        /// <summary>
        /// A single SQL UPDATE row with multiple entries, e.g UPDATE `a` SET `b`=X, `c`=Y WHERE `id` IN (1, 2, 3);
        /// </summary>
        public class SQLUpdateRowMultiple1 : SQLUpdateRow
        {
            private readonly string _primaryKey;

            public SQLUpdateRowMultiple1(string primaryKey)
            {
                _primaryKey = primaryKey;
            }

            public void AddWhere(object value, string comment = "")
            {
                WhereClause.Add(new KeyValuePair<string, object>(comment, value));
            }

            public override string Build()
            {
                var row = new StringBuilder();
                if (CommentOut)
                    row.Append("-- ");

                // Return empty if there are no values or where clause or no table name set
                if (Values.Count == 0 || WhereClause.Count == 0 || string.IsNullOrEmpty(Table))
                    return string.Empty;

                row.Append("UPDATE ");
                row.Append(Table);
                row.Append(" SET ");

                var count = 0;
                foreach (var values in Values)
                {
                    count++;
                    row.Append(values.Key);
                    row.Append("=");
                    row.Append(values.Value);
                    if (Values.Count != count)
                        row.Append(SQLUtil.CommaSeparator);
                }

                row.Append(" WHERE ");
                row.Append(_primaryKey);

                if (WhereClause.Count > 1)
                {
                    row.Append(" IN (");

                    count = 0;
                    foreach (var whereClause in WhereClause)
                    {
                        count++;
                        row.Append(whereClause.Value);
                        if (!string.IsNullOrEmpty(whereClause.Key))
                            row.Append(" /*" + whereClause.Key + "*/");
                        if (WhereClause.Count != count)
                            row.Append(", ");
                    }

                    row.Append(")");
                }
                else if (WhereClause.Count == 1)
                {
                    row.Append("=");
                    row.Append(WhereClause[0].Value);
                    Comment = WhereClause[0].Key;
                }

                row.Append(";");

                if (!String.IsNullOrWhiteSpace(Comment))
                    row.Append(" -- " + Comment);
                row.Append(Environment.NewLine);

                return row.ToString();
            }
        }

        public class SQLUpdateRow : ISQLQuery
        {
            /// <summary>
            /// <para>Comment appended to the values</para>
            /// <code>UPDATE...; -- this comment</code>
            /// </summary>
            public string Comment { get; set; }

            /// <summary>
            /// <para>The row will be commented out</para>
            /// <code>-- UPDATE..., </code>
            /// </summary>
            public bool CommentOut { get; set; }

            public readonly Dictionary<string, object> Values =
                new Dictionary<string, object>();

            public readonly List<KeyValuePair<string, object>> WhereClause =
                new List<KeyValuePair<string, object>>();

            /// <summary>
            /// Returns the amount of values
            /// </summary>
            public int ValueCount
            {
                get { return Values.Count; }
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

                Values[SQLUtil.AddBackQuotes(field)] = SQLUtil.ToSQLValue(value, isFlag, noQuotes);
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

                Values[SQLUtil.AddBackQuotes(field)] = SQLUtil.ToSQLValue(value, isFlag, noQuotes);
            }

            /// <summary>
            /// Adds to this row what will be updated
            /// </summary>
            /// <param name="field">The field name associated with the value</param>
            /// <param name="value">The value used in the where clause</param>
            public void AddWhere(string field, object value)
            {
                WhereClause.Add(new KeyValuePair<string, object>(SQLUtil.AddBackQuotes(field), value));
            }

            private string _table;

            /// <summary>
            /// The table that will be updated
            /// </summary>
            public string Table
            {
                get { return SQLUtil.AddBackQuotes(_table); }
                set { _table = value; }
            }

            /// <summary>
            /// Constructs the actual query
            /// </summary>
            /// <returns>A single update query</returns>
            public virtual string Build()
            {
                var row = new StringBuilder();
                if (CommentOut)
                    row.Append("-- ");

                // Return empty if there are no values or where clause or no table name set
                if (Values.Count == 0 || WhereClause.Count == 0 || string.IsNullOrEmpty(Table))
                    return string.Empty;

                row.Append("UPDATE ");
                row.Append(Table);
                row.Append(" SET ");

                var count = 0;
                foreach (var values in Values)
                {
                    count++;
                    row.Append(values.Key);
                    row.Append("=");
                    row.Append(values.Value);
                    if (Values.Count != count)
                        row.Append(SQLUtil.CommaSeparator);
                }

                row.Append(" WHERE ");

                count = 0;
                foreach (var whereClause in WhereClause)
                {
                    count++;
                    row.Append(whereClause.Key);
                    row.Append("=");
                    row.Append(whereClause.Value is string ? SQLUtil.Stringify(whereClause.Value) : whereClause.Value);
                    if (WhereClause.Count != count)
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
            private const int MaxRowsPerInsert = 250;

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

                switch (primaryKeyNumber)
                {
                    case 1:
                    {
                        ICollection<string> values =
                            rows.FindAll(row => !row.NoData).Select(row => row.GetPrimaryKeysValues(primaryKeyNumber).First()).ToArray();
                        var primaryKey = TableStructure[0];

                        Delete = new SQLDelete(values, primaryKey, Table).Build();
                        break;
                    }
                    case 2:
                    {
                        ICollection<Tuple<string, string>> values =
                            rows.FindAll(row => !row.NoData).Select(row => row.GetPrimaryKeysValues(primaryKeyNumber)).
                                Select(vals => Tuple.Create(vals[0], vals[1])).ToArray();

                        var primaryKeys = Tuple.Create(TableStructure[0], TableStructure[1]);

                        Delete = new SQLDelete(values, primaryKeys, tableName).Build();
                        break;
                    }
                    case 3:
                    {
                        ICollection<Tuple<string, string, string>> values =
                            rows.FindAll(row => !row.NoData).Select(row => row.GetPrimaryKeysValues(primaryKeyNumber)).
                                Select(vals => Tuple.Create(vals[0], vals[1], vals[2])).ToArray();

                        var primaryKeys = Tuple.Create(TableStructure[0], TableStructure[1], TableStructure[2]);

                        Delete = new SQLDelete(values, primaryKeys, tableName).Build();
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException("primaryKeyNumber");
                }
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
                HashSet<string> rowStrings = new HashSet<string>();
                foreach (var row in Rows)
                {
                    if (count >= MaxRowsPerInsert && !_deleteDuplicates)
                    {
                        query.ReplaceLast(',', ';');
                        query.Append(Environment.NewLine);
                        query.Append(InsertHeader);
                        count = 0;
                    }
                    string rowString = row.Build();
                    if (_deleteDuplicates && !rowStrings.Add(rowString))
                        continue;

                    query.Append(rowString);
                    count++;
                }

                query.Append(Environment.NewLine);

                query.ReplaceLast(',', ';');

                return query.ToString();
            }
        }

        public class SQLInsertRow : ISQLQuery, IEqualityComparer<SQLInsertRow>
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

            public bool Equals(SQLInsertRow x, SQLInsertRow y)
            {
                return x._values.SequenceEqual(y._values);
            }

            public int GetHashCode(SQLInsertRow obj)
            {
                int hashCode = 0;
                foreach (var value in obj._values)
                    hashCode ^= value.GetHashCode();

                return hashCode;
            }
        }

        public class SQLDelete : ISQLQuery
        {
            private readonly bool _between;
            readonly uint _primaryKeyNumber;

            private ISet<string> Values { get; set; }
            private ISet<Tuple<string, string>> ValuesDouble { get; set; }
            private ISet<Tuple<string, string, string>> ValuesTriple { get; set; }
            private Tuple<string, string> ValuesBetweenDouble { get; set; }
            private Tuple<string, string, string> ValuesBetweenTriple { get; set; }

            private string Table { get; set; }

            private string PrimaryKey { get; set; }
            private Tuple<string, string> PrimaryKeyDouble { get; set; }
            private Tuple<string, string, string> PrimaryKeyTriple { get; set; }

            /// <summary>
            /// <para>Creates a delete query with a single primary key and an arbitrary number of values</para>
            /// <code>DELETE FROM `tableName` WHERE `primaryKey` IN (values[0], ..., values[n]);</code>
            /// <code>DELETE FROM `tableName` WHERE `primaryKey`=values[0];</code>
            /// </summary>
            /// <param name="values">Collection of values to be deleted</param>
            /// <param name="primaryKey">Field used in the WHERE clause</param>
            /// <param name="tableName">Table name</param>
            public SQLDelete(IEnumerable<string> values, string primaryKey, string tableName)
            {
                PrimaryKey = primaryKey;
                Table = tableName;

                Values = new HashSet<string>(values);
                _between = false;
                _primaryKeyNumber = 1;
            }

            /// <summary>
            /// <para>Creates a delete query that uses two primary keys and an arbitrary number of values</para>
            /// <code>DELETE FROM `tableName` WHERE (`primaryKeys[0]`=values[0][0] AND `primaryKeys[1]`=values[1][0]) AND ...);</code>
            /// </summary>
            /// <param name="values">Collection of pairs of values to be deleted</param>
            /// <param name="primaryKeys">Collection of pairs of fields used in the WHERE clause</param>
            /// <param name="tableName">Table name</param>
            public SQLDelete(IEnumerable<Tuple<string, string>> values, Tuple<string, string> primaryKeys, string tableName)
            {
                PrimaryKeyDouble = primaryKeys;
                Table = tableName;

                ValuesDouble = new HashSet<Tuple<string, string>>(values);
                _between = false;
                _primaryKeyNumber = 2;
            }

            /// <summary>
            /// <para>Creates a delete query that uses two primary keys and an arbitrary number of values</para>
            /// <code>DELETE FROM `tableName` WHERE (`primaryKeys[0]`=values[0][0] AND `primaryKeys[1]`=values[2][1][0]) AND ...);</code>
            /// </summary>
            /// <param name="values">Collection of pairs of values to be deleted</param>
            /// <param name="primaryKeys">Collection of pairs of fields used in the WHERE clause</param>
            /// <param name="tableName">Table name</param>
            public SQLDelete(IEnumerable<Tuple<string, string, string>> values, Tuple<string, string, string> primaryKeys, string tableName)
            {
                PrimaryKeyTriple = primaryKeys;
                Table = tableName;

                ValuesTriple = new HashSet<Tuple<string, string, string>>(values);
                _between = false;
                _primaryKeyNumber = 3;
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

                ValuesBetweenDouble = values;
                _between = true;
                _primaryKeyNumber = 2;
            }

            /// <summary>
            /// <para>Creates a delete query that deletes between two values</para>
            /// <code>DELETE FROM `tableName` WHERE `primaryKey` BETWEEN values[0] AND values[n];</code>
            /// </summary>
            /// <param name="values">Pair of values</param>
            /// <param name="primaryKey">Field used in the WHERE clause</param>
            /// <param name="tableName">Table name</param>
            public SQLDelete(Tuple<string, string, string> values, string primaryKey, string tableName)
            {
                PrimaryKey = primaryKey;
                Table = tableName;

                ValuesBetweenTriple = values;
                _between = true;
                _primaryKeyNumber = 3;
            }

            /// <summary>
            /// Constructs the actual query
            /// </summary>
            /// <returns>Delete query</returns>
            public string Build()
            {
                var query = new StringBuilder();

                if (_between)
                {
                    query.Append("DELETE FROM ");
                    query.Append(SQLUtil.AddBackQuotes(Table));
                    query.Append(" WHERE ");

                    switch (_primaryKeyNumber)
                    {
                        case 2:
                            query.Append(SQLUtil.AddBackQuotes(PrimaryKey));
                            query.Append(" BETWEEN ");
                            query.Append(ValuesBetweenDouble.Item1);
                            query.Append(" AND ");
                            query.Append(ValuesBetweenDouble.Item2);
                            query.Append(";");
                            break;
                        case 3:
                            query.Append(SQLUtil.AddBackQuotes(PrimaryKey));
                            query.Append(" BETWEEN ");
                            query.Append(ValuesBetweenTriple.Item1);
                            query.Append(" AND ");
                            query.Append(ValuesBetweenTriple.Item2);
                            query.Append(" AND ");
                            query.Append(ValuesBetweenTriple.Item3);
                            query.Append(";");
                            break;
                    }
                }
                else
                {
                    switch (_primaryKeyNumber)
                    {
                        case 2:
                        {
                            var counter = 0;
                            var rowsPerDelete = 0;

                            query.Append("DELETE FROM ");
                            query.Append(SQLUtil.AddBackQuotes(Table));
                            query.Append(" WHERE ");

                            foreach (var tuple in ValuesDouble)
                            {
                                counter++;
                                rowsPerDelete++;

                                query.Append("(");
                                query.Append(SQLUtil.AddBackQuotes(PrimaryKeyDouble.Item1));
                                query.Append("=");
                                query.Append(tuple.Item1);
                                query.Append(" AND ");
                                query.Append(SQLUtil.AddBackQuotes(PrimaryKeyDouble.Item2));
                                query.Append("=");
                                query.Append(tuple.Item2);
                                query.Append(")");

                                // Append an OR if not end of items
                                if (rowsPerDelete < 25 && ValuesDouble.Count != counter)
                                    query.Append(" OR ");
                                else if (rowsPerDelete == 25)
                                {
                                    rowsPerDelete = 0;
                                    query.Append(";");

                                    if (ValuesDouble.Count != counter)
                                    {
                                        query.Append(Environment.NewLine);
                                        query.Append("DELETE FROM ");
                                        query.Append(SQLUtil.AddBackQuotes(Table));
                                        query.Append(" WHERE ");
                                    }
                                }
                                else if (ValuesDouble.Count == counter)
                                    query.Append(";");
                            }
                            break;
                        }
                        case 3:
                        {
                            var counter = 0;

                            var rowsPerDelete = 0;

                            query.Append("DELETE FROM ");
                            query.Append(SQLUtil.AddBackQuotes(Table));
                            query.Append(" WHERE ");

                            foreach (var tuple in ValuesTriple)
                            {
                                counter++;
                                rowsPerDelete++;

                                query.Append("(");
                                query.Append(SQLUtil.AddBackQuotes(PrimaryKeyTriple.Item1));
                                query.Append("=");
                                query.Append(tuple.Item1);
                                query.Append(" AND ");
                                query.Append(SQLUtil.AddBackQuotes(PrimaryKeyTriple.Item2));
                                query.Append("=");
                                query.Append(tuple.Item2);
                                query.Append(" AND ");
                                query.Append(SQLUtil.AddBackQuotes(PrimaryKeyTriple.Item3));
                                query.Append("=");
                                query.Append(tuple.Item3);
                                query.Append(")");

                                // Append an OR if not end of items
                                if (rowsPerDelete < 25 && ValuesTriple.Count != counter)
                                    query.Append(" OR ");
                                else if (rowsPerDelete == 25)
                                {
                                    rowsPerDelete = 0;
                                    query.Append(";");

                                    if (ValuesTriple.Count != counter)
                                    {
                                        query.Append(Environment.NewLine);
                                        query.Append("DELETE FROM ");
                                        query.Append(SQLUtil.AddBackQuotes(Table));
                                        query.Append(" WHERE ");
                                    }
                                }
                                else if (ValuesTriple.Count == counter)
                                    query.Append(";");
                            }
                            break;
                        }
                        default:
                        {
                            query.Append("DELETE FROM ");
                            query.Append(SQLUtil.AddBackQuotes(Table));
                            query.Append(" WHERE ");
                            query.Append(SQLUtil.AddBackQuotes(PrimaryKey));
                            query.Append(Values.Count == 1 ? "=" : " IN (");

                            var counter = 0;
                            var rowsPerDelete = 0;

                            foreach (var entry in Values)
                            {
                                counter++;
                                rowsPerDelete++;

                                query.Append(entry);
                                // Append comma if not end of items
                                if (Values.Count != counter)
                                    query.Append(SQLUtil.CommaSeparator);
                                else if (Values.Count != 1 && Values.Count != counter)
                                    query.Append(")");
                                else if (rowsPerDelete == 25)
                                {
                                    rowsPerDelete = 0;
                                    query.Append(";");

                                    if (Values.Count != counter)
                                    {
                                        query.Append(Environment.NewLine);
                                        query.Append("DELETE FROM ");
                                        query.Append(SQLUtil.AddBackQuotes(Table));
                                        query.Append(" WHERE ");
                                        query.Append(SQLUtil.AddBackQuotes(PrimaryKey));
                                        query.Append(Values.Count == 1 ? "=" : " IN (");
                                    }
                                }
                                else if (Values.Count == counter)
                                {
                                    if (Values.Count != 1)
                                        query.Append(")");
                                    query.Append(";");
                                }
                            }
                            break;
                        }
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

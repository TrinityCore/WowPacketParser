using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WowPacketParser.Misc;

namespace WowPacketParser.SQL
{
    public class SQLWhere<T> where T : IDataModel, new()
    {
        private readonly RowList<T> _conditions;

        private readonly bool _onlyPrimaryKeys;

        private static readonly List<Tuple<string, FieldInfo, List<DBFieldNameAttribute>>> _databaseFields = SQLUtil.GetFields<T>();
        private static readonly FieldInfo _primaryKeyReflectionField = SQLUtil.GetFirstPrimaryKey<T>();

        public SQLWhere(RowList<T> conditionList, bool onlyPrimaryKeys = false)
        {
            _conditions = conditionList;
            _onlyPrimaryKeys = onlyPrimaryKeys;
        }

        public bool HasConditions => _conditions != null && _conditions.Count != 0;

        public string Build()
        {
            if (_conditions == null || _conditions.Count == 0)
                return string.Empty;

            StringBuilder whereClause = new StringBuilder();

            if (_onlyPrimaryKeys && _conditions.GetPrimaryKeyCount() == 1)
            {
                var field = _databaseFields.Single(f => f.Item2 == _primaryKeyReflectionField);

                whereClause.Append(field.Item1);
                if (_conditions.Count == 1)
                {
                    whereClause.Append("=");
                    whereClause.Append(field.Item2.GetValue(_conditions.First().Data));
                }
                else
                {
                    whereClause.Append(" IN (");

                    foreach (Row<T> condition in _conditions)
                    {
                        object value = field.Item2.GetValue(condition.Data);
                        whereClause.Append(SQLUtil.ToSQLValue(value));

                        if (!string.IsNullOrEmpty(condition.Comment))
                            whereClause.Append(" /*" + condition.Comment + "*/");

                        whereClause.Append(SQLUtil.CommaSeparator);
                    }
                    whereClause.Remove(whereClause.Length - SQLUtil.CommaSeparator.Length, SQLUtil.CommaSeparator.Length); // remove last ", "

                    whereClause.Append(")");
                }
            }
            else
            {
                foreach (Row<T> condition in _conditions)
                {
                    whereClause.Append("(");
                    foreach (var field in _databaseFields)
                    {
                        object value = field.Item2.GetValue(condition.Data);

                        if (value == null ||
                            (_onlyPrimaryKeys &&
                             field.Item3.Any(a => !a.IsPrimaryKey)))
                            continue;

                        whereClause.Append(field.Item1);

                        whereClause.Append("=");
                        whereClause.Append(SQLUtil.ToSQLValue(value));
                        whereClause.Append(" AND ");
                    }

                    whereClause.Remove(whereClause.Length - 5, 5); // remove last " AND "
                    whereClause.Append(")");
                    whereClause.Append(" OR ");
                }
                whereClause.Remove(whereClause.Length - 4, 4); // remove last " OR ";
            }

            return whereClause.ToString();
        }
    }

    /// <summary>
    /// Represents a SQL SELECT statement of the specified data model.
    /// </summary>
    /// <typeparam name="T">The data model</typeparam>
    public class SQLSelect<T> : ISQLQuery where T : IDataModel, new()
    {
        private readonly SQLWhere<T> _whereClause;

        private readonly string _database;

        public SQLSelect(RowList<T> rowList = null, string database = null, bool onlyPrimaryKeys = true)
        {
            _whereClause = new SQLWhere<T>(rowList, onlyPrimaryKeys);
            _database = database;
        }

        public string Build()
        {
            string tableName = SQLUtil.GetTableName<T>();
            var fields = SQLUtil.GetFields<T>();

            StringBuilder fieldNames = new StringBuilder();
            
            foreach (var field in fields)
            {
                fieldNames.Append(field.Item1);
                fieldNames.Append(SQLUtil.CommaSeparator);
            }
            fieldNames.Remove(fieldNames.Length - 2, 2); // remove last ", "

            if (_whereClause.HasConditions)
                return $"SELECT {fieldNames} FROM {_database ?? Settings.TDBDatabase}.{tableName} WHERE {_whereClause.Build()}";

            return $"SELECT {fieldNames} FROM {_database ?? Settings.TDBDatabase}.{tableName}";
        }
    }

    public class SQLUpdate<T> : ISQLQuery where T : IDataModel, new()
    {
        private readonly Dictionary<Row<T>, RowList<T>> _rows2;

        /// <summary>
        /// Creates multiple update rows
        /// </summary>
        /// <param name="rows">A list of <see cref="SQLUpdateRow{T}"/> rows</param>
        public SQLUpdate(Dictionary<Row<T>, RowList<T>> rows)
        {
            _rows2 = rows;
        } 

        /// <summary>
        /// Constructs the actual query
        /// </summary>
        /// <returns>Multiple update rows</returns>
        public string Build()
        {
            StringBuilder result = new StringBuilder();

            var rowsStrings = _rows2.Select(row => new SQLUpdateRow<T>(row.Key, row.Value).Build());

            foreach (string rowString in rowsStrings)
            {
                if (string.IsNullOrEmpty(rowString))
                    continue;
                result.Append(rowString);
                result.Append(Environment.NewLine);
            }

            return result.ToString();
        }
    }

    internal class SQLUpdateRow<T> : ISQLQuery where T : IDataModel, new()
    {
        /// <summary>
        /// <para>The row will be commented out</para>
        /// <code>-- UPDATE..., </code>
        /// </summary>
        public bool CommentOut { get; set; }

        private readonly Row<T> _value;

        protected readonly SQLWhere<T> WhereClause;

        private static readonly List<Tuple<string, FieldInfo, List<DBFieldNameAttribute>>> _databaseFields = SQLUtil.GetFields<T>();

        public SQLUpdateRow(Row<T> value, RowList<T> conditions)
        {
            _value = value;
            WhereClause = new SQLWhere<T>(conditions, true);
        }

        /// <summary>
        /// Constructs the actual query
        /// </summary>
        /// <returns>A single update query</returns>
        public virtual string Build()
        {
            StringBuilder query = new StringBuilder();
            if (CommentOut)
                query.Append("-- ");

            // Return empty if there are no values or where clause or no table name set
            if (!WhereClause.HasConditions)
                return string.Empty;

            query.Append("UPDATE ");
            query.Append(SQLUtil.GetTableName<T>());
            query.Append(" SET ");

            bool hasValues = false;
            foreach (var field in _databaseFields)
            {
                object value = field.Item2.GetValue(_value.Data);

                Array arr = value as Array;
                if (arr != null)
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        object v = arr.GetValue(i);
                        if (v == null)
                            continue;

                        query.Append(SQLUtil.AddBackQuotes(field.Item3.First().Name + (field.Item3.First().StartAtZero ? i : i+1)));
                        query.Append("=");
                        query.Append(SQLUtil.ToSQLValue(v, noQuotes: field.Item3.Any(a => a.NoQuotes)));
                        query.Append(SQLUtil.CommaSeparator);

                        hasValues = true;
                    }
                    continue;
                }

                if (value == null)
                    continue;

                if (field.Item2.Name != "VerifiedBuild" || !Settings.SkipOnlyVerifiedBuildUpdateRows)
                    hasValues = true;

                query.Append(field.Item1);
                query.Append("=");
                query.Append(SQLUtil.ToSQLValue(value, noQuotes: field.Item3.Any(a => a.NoQuotes)));
                query.Append(SQLUtil.CommaSeparator);
            }
            if (!hasValues)
                return string.Empty;

            query.Remove(query.Length - SQLUtil.CommaSeparator.Length, SQLUtil.CommaSeparator.Length); // remove last ", "

            query.Append(" WHERE ");
            query.Append(WhereClause.Build());
            query.Append(";");

            if (!string.IsNullOrWhiteSpace(_value.Comment))
                query.Append(" -- " + _value.Comment);

            return query.ToString();
        }
    }

    public class SQLInsert<T> : ISQLQuery where  T : IDataModel, new()
    {
        private readonly RowList<T> _rows;
        private readonly bool _withDelete;
        private readonly string _insertHeader;

        // Add a new insert header every 250 rows
        private const int MaxRowsPerInsert = 250;

        /// <summary>
        /// Creates an insert query including the insert header and its rows
        /// Single primary key (for delete)
        /// </summary>
        /// <param name="rows">A list of <see cref="SQLInsertRow{T}"/> rows</param>
        /// <param name="withDelete">If set to false the full query will not include a delete query</param>
        /// <param name="ignore">If set to true the INSERT INTO query will be INSERT IGNORE INTO</param>
        public SQLInsert(RowList<T> rows, bool withDelete = true, bool ignore = false)
        {
            _rows = rows;
            _insertHeader = new SQLInsertHeader<T>(ignore).Build();
            _withDelete = withDelete;
        }

        /// <summary>
        /// Constructs the actual query
        /// </summary>
        /// <returns>Full insert AND delete queries</returns>
        public string Build()
        {
            if (_rows.Count == 0)
                return string.Empty;

            StringBuilder query = new StringBuilder();

            if (_withDelete)
                query.Append(new SQLDelete<T>(_rows).Build()); // Can be empty
            query.Append(_insertHeader);

            int count = 0;
            foreach (var row in _rows)
            {
                if (count >= MaxRowsPerInsert)
                {
                    query.ReplaceLast(',', ';');
                    query.Append(Environment.NewLine);
                    query.Append(_insertHeader);
                    count = 0;
                }
                query.Append(new SQLInsertRow<T>(row).Build());
                query.Append(Environment.NewLine);
                count++;
            }
            query.ReplaceLast(',', ';');

            return query.ToString();
        }
    }

    internal class SQLInsertHeader<T> : ISQLQuery where T : IDataModel
    {
        private readonly bool _ignore;

        private static readonly List<Tuple<string, FieldInfo, List<DBFieldNameAttribute>>> _databaseFields = SQLUtil.GetFields<T>();

        /// <summary>
        /// <para>Creates the header of an INSERT query</para>
        /// <code>INSERT INTO `tableName` (fields[0], ..., fields[n]) VALUES</code>
        /// </summary>
        /// <param name="ignore">If set to true the INSERT INTO query will be INSERT IGNORE INTO</param>
        public SQLInsertHeader(bool ignore = false)
        {
            //TableStructure = fields;
            _ignore = ignore;
        }

        /// <summary>
        /// Constructs the actual query
        /// </summary>
        /// <returns>Insert query header</returns>
        public string Build()
        {
            StringBuilder query = new StringBuilder();

            query.Append("INSERT ");
            query.Append(_ignore ? "IGNORE " : string.Empty);
            query.Append("INTO ");
            query.Append(SQLUtil.GetTableName<T>());

            query.Append(" (");
            foreach (var field in _databaseFields)
            {
                query.Append(field.Item1);
                query.Append(SQLUtil.CommaSeparator);
            }
            query.Remove(query.Length - SQLUtil.CommaSeparator.Length, SQLUtil.CommaSeparator.Length); // remove last ", "
            query.Append(")");
            query.Append(" VALUES" + Environment.NewLine);

            return query.ToString();
        }
    }

    internal class SQLInsertRow<T> : ISQLQuery
        where T : IDataModel, new()
    {
        private readonly Row<T> _row; 

        private string _headerComment;

        private static readonly List<Tuple<string, FieldInfo, List<DBFieldNameAttribute>>> _databaseFields = SQLUtil.GetFields<T>();

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

        public SQLInsertRow(Row<T> row)
        {
            _row = row;
        }

        /// <summary>
        /// Constructs the actual query
        /// </summary>
        /// <returns>Single insert row (data)</returns>
        public string Build()
        {
            if (NoData)
                return "-- " + _headerComment + Environment.NewLine;

            StringBuilder query = new StringBuilder();
            if (_row.CommentOut)
                query.Append("-- ");

            query.Append("(");

            foreach (var field in _databaseFields)
            {
                object value = field.Item2.GetValue(_row.Data);
                if (value == null)
                {
                    if (field.Item3.Any(a => a.Nullable))
                    {
                        query.Append("NULL");
                        query.Append(SQLUtil.CommaSeparator);
                    }
                    else
                    {
                        query.Append("UNKNOWN");
                        query.Append(SQLUtil.CommaSeparator);
                    }
                }
                else
                {
                    Array arr = value as Array;
                    if (arr != null)
                    {
                        foreach (object v in arr)
                        {
                            if (v == null)
                            {
                                if (field.Item3.Any(a => a.Nullable))
                                    query.Append("NULL");
                                else
                                    query.Append("UNKNOWN");
                            }
                            else
                                query.Append(SQLUtil.ToSQLValue(v, noQuotes: field.Item3.Any(a => a.NoQuotes)));

                            query.Append(SQLUtil.CommaSeparator);
                        }
                    }
                    else
                    {
                        query.Append(SQLUtil.ToSQLValue(value, noQuotes: field.Item3.Any(a => a.NoQuotes == true)));
                        query.Append(SQLUtil.CommaSeparator);
                    }
                }
            }
            query.Remove(query.Length - SQLUtil.CommaSeparator.Length, SQLUtil.CommaSeparator.Length); // remove last ", "
            query.Append("),");

            if (!string.IsNullOrWhiteSpace(_row.Comment))
                query.Append(" -- " + _row.Comment);

            return query.ToString();
        }
    }

    public class SQLDelete<T> : ISQLQuery where T: IDataModel, new()
    {
        private readonly bool _between;

        private readonly RowList<T> _rows;
        private readonly Tuple<string, string> _valuesBetweenDouble;

        private static readonly List<Tuple<string, FieldInfo, List<DBFieldNameAttribute>>> _databaseFields = SQLUtil.GetFields<T>();
        private static readonly FieldInfo _primaryKeyReflectionField = SQLUtil.GetFirstPrimaryKey<T>();

        /// <summary>
        /// <para>Creates a delete query with a single primary key and an arbitrary number of values</para>
        /// <code>DELETE FROM `tableName` WHERE `primaryKey` IN (values[0], ..., values[n]);</code>
        /// <code>DELETE FROM `tableName` WHERE `primaryKey`=values[0];</code>
        /// </summary>
        /// <param name="values">Collection of values to be deleted</param>
        public SQLDelete(RowList<T> values )
        {
            _rows = values;
            _between = false;
        }

        /// <summary>
        /// <para>Creates a delete query that deletes between two values</para>
        /// <code>DELETE FROM `tableName` WHERE `primaryKey` BETWEEN values[0] AND values[n];</code>
        /// </summary>
        /// <param name="values">Pair of values</param>
        public SQLDelete(Tuple<string, string> values)
        {
            _valuesBetweenDouble = values;
            _between = true;
        }

        /// <summary>
        /// Constructs the actual query
        /// </summary>
        /// <returns>Delete query</returns>
        public string Build()
        {
            StringBuilder query = new StringBuilder();

            if (_between)
            {
                var pk = _databaseFields.Single(f => f.Item2 == _primaryKeyReflectionField);

                query.Append("DELETE FROM ");
                query.Append(SQLUtil.GetTableName<T>());
                query.Append(" WHERE ");

                query.Append(pk.Item1);
                query.Append(" BETWEEN ");
                query.Append(_valuesBetweenDouble.Item1);
                query.Append(" AND ");
                query.Append(_valuesBetweenDouble.Item2);
                query.Append(";");
            }
            else
            {
                query.Append("DELETE FROM ");
                query.Append(SQLUtil.GetTableName<T>());
                query.Append(" WHERE ");
                query.Append(new SQLWhere<T>(_rows, true).Build());
                query.Append(";");
            }

            query.Append(Environment.NewLine);
            return query.ToString();
        }
    }
}

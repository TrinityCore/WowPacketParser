using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace WowPacketParser.SQL
{
    public record Row<T> where T : IDataModel, new()
    {
        public Row()
        { }

        public Row(T data)
        {
            Data = data;
        }

        public T Data { get; set; } = new T();
        public string Comment { get; set; } = string.Empty;

        public bool CommentOut = false;
    }

    /// <summary>
    /// This represents a list of IDataModel which act as conditions in e.g WHERE clauses.
    /// </summary>
    /// <typeparam name="T">The <see cref="IDataModel" /></typeparam>
    public class RowList<T> : IEnumerable<Row<T>> where T : IDataModel, new()
    {
        private readonly List<Row<T>> _rows = new List<Row<T>>();
        private readonly Dictionary<string, int> _pkDict = new Dictionary<string, int>();

        private static readonly FieldInfo[] _fieldInfos = typeof(T).GetFields();
        private static readonly IEnumerable<FieldInfo> _primaryKeyFieldInfos = typeof(T).GetFields().Where(SQLUtil.IsPrimaryKey).ToList();
        private static readonly List<Tuple<string, FieldInfo, List<DBFieldNameAttribute>>> _dbFields = SQLUtil.GetFields<T>();

        /// <summary>
        /// Gets the number of conditions in the <see cref="RowList{T}" />.
        /// </summary>
        public int Count => _rows.Count;

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="RowList{T}" />
        /// </summary>
        /// <returns>A <see cref="IEnumerator{T}" /> for the <see cref="RowList{T}" /></returns>
        public IEnumerator<Row<T>> GetEnumerator()
        {
            return _rows.GetEnumerator();
        }

        public List<Row<T>> GetRows()
        {
            return _rows;
        }

        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public RowList<T> Add(T data)
        {
            return Add(new Row<T>(data));
        }

        /// <summary>
        /// Adds a Row to the list and performs some checks.
        /// </summary>
        /// <param name="row">The Row which should be added.</param>
        public RowList<T> Add(Row<T> row)
        {
            if (_fieldInfos.All(f => f.GetValue(row.Data) == null))
                return this; // got empty Row. Do not add to list

            if (ContainsKey(row))
                return this;

            string pkString = _primaryKeyFieldInfos.Aggregate(string.Empty, (current, field) => current + (field.GetValue(row.Data) + "--"));

            _pkDict.Add(pkString, _rows.Count);
            _rows.Add(row);

            return this;
        }

        public RowList<T> AddRange(IEnumerable<T> range)
        {
            foreach (T c in range)
                Add(c);

            return this;
        }

        public void Clear()
        {
            _rows.Clear();
            _pkDict.Clear();
        }

        public int GetPrimaryKeyCount()
        {
            return _dbFields.Count(f => f.Item3.Any(g => g.IsPrimaryKey));
        }

        public bool ContainsKey(T key)
        {
            string pkString = _primaryKeyFieldInfos.Aggregate(string.Empty, (current, field) => current + (field.GetValue(key) + "--"));

            return _pkDict.ContainsKey(pkString);
        }

        public bool ContainsKey(Row<T> key)
        {
            return ContainsKey(key.Data);
        }

        public Row<T> this[T key]
        {
            get
            {
                if (!ContainsKey(key))
                    return null;

                int index;
                string pkString = _primaryKeyFieldInfos.Aggregate(string.Empty, (current, field) => current + (field.GetValue(key) + "--"));
                _pkDict.TryGetValue(pkString, out index);

                return _rows[index];
            }
        }

        public Row<T> this[Row<T> key] => this[key.Data];
    }
}

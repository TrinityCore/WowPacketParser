using System.Collections.Generic;

namespace WowPacketParser.SQL.Builder
{
    class SQLUpdate : ISQLCommand
    {
        private readonly List<KeyValuePair<string, object>> _values =
            new List<KeyValuePair<string, object>>();

        private readonly List<KeyValuePair<string, object>> _whereClause =
            new List<KeyValuePair<string, object>>();

        public void AddValue(string name, object value)
        {
            _values.Add(new KeyValuePair<string, object>(SQLUtilities.AddQuotes(name), value));
        }

        public void AddWhere(string name, object value)
        {
            _whereClause.Add(new KeyValuePair<string, object>(SQLUtilities.AddQuotes(name), value));
        }

        private string _table;

        public string Table
        {
            get { return SQLUtilities.AddQuotes(_table); }
            set { _table = value; }
        }

        public string Build()
        {
            // UPDATE `creature_template` SET `faction`=1, `VehicleId`=123 WHERE `entry`=456;

            // Return empty if there are no values or where clause or no table name set
            if (_values.Count == 0 || _whereClause.Count == 0 || Table == string.Empty)
                return string.Empty;

            string result = string.Empty;

            result += "UPDATE " + Table + " SET ";

            foreach (var values in _values)
            {
                result += values.Key + "=" + values.Value;
                if (_values.IndexOf(values) + 1 != _values.Count)
                    result += ", ";
            }

            result += " WHERE ";

            foreach (var whereClause in _whereClause)
            {
                result += whereClause.Key + "=" + whereClause.Value;
                if (_whereClause.IndexOf(whereClause) + 1 != _whereClause.Count)
                    result += " AND ";
            }

            result += ";";

            return result;
        }
    }
}

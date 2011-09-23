using System;
using System.Collections.Generic;

namespace WowPacketParser.SQL.Builder
{
    class SQLInsert : ISQLCommand
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
            /* DELETE FROM `quest_poi_points` WHERE `questId` IN (13153,13204);
               INSERT INTO `quest_poi_points` (`questId`,`id`,`x`,`y`) VALUES 
               (13153,0,4494,2412),
               (13153,0,4524,2964),
               (13204,1,378,-927),
               (13204,1,403,-925);
            */

            // Return empty if there are no values or where clause or no table name set
            if (_values.Count == 0 || _whereClause.Count == 0 || Table == string.Empty)
                return string.Empty;

            string result = string.Empty;

            result += "DELETE FROM " + Table + " WHERE ";
            foreach (var whereClause in _whereClause)
            {
                if (_whereClause.Count == 1)
                    result += whereClause.Key + "=" + whereClause.Value;
                else
                {
                    result += whereClause.Key + " IN (" + whereClause.Value + ")";
                    if (_whereClause.IndexOf(whereClause) + 1 != _whereClause.Count)
                        result += " AND ";
                }
            }
            result += Environment.NewLine;

            result += "INSERT INTO " + Table + " (";
            foreach (var keys in _values)
            {
                result += keys.Key;
                if (_values.IndexOf(keys) + 1 != _values.Count)
                    result += ",";
            }
            result += ") VALUES" + Environment.NewLine;

            // TODO needs a list of lists
            // Atm we can't have an insert with more than one row. THAT NEEDS TO CHANGE.
            result += "(";
            foreach (var values in _values)
            {
                if (values.Value is string)
                    result += SQLUtilities.EscapeString(values.Value.ToString());
                else
                    result += values.Value;
                if (_values.IndexOf(values) + 1 != _values.Count)
                    result += ",";
            }
            result += ");";

            return result;
        }
    }
}

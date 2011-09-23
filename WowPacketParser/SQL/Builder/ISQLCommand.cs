using System.Collections.Generic;

namespace WowPacketParser.SQL.Builder
{
    interface ISQLCommand
    {
        string Table { get; set; }
        void AddValue(string name, object value);
        void AddWhere(string name, object value);
        string Build();
    }
}

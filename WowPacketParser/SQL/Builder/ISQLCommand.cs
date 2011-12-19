namespace WowPacketParser.SQL.Builder
{
    interface ISQLCommand
    {
        // TODO: Delete this file, use QueryBuilder.cs instead
        string Table { get; set; }
        void AddValue(string name, object value);
        void AddWhere(string name, object value);
        string Build();
    }
}

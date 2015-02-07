namespace WowPacketParser.Enums
{
    public enum DumpFormatType
    {
        None,           // No dump at all
        Text,
        Pkt,
        PktSplit,
        SqlOnly,
        SniffDataOnly,
        StatisticsPreParse,
        PktSessionSplit,
        CompressSniff,
        SniffVersionSplit,
        HexOnly,
        PktDirectionSplit,
        ConnectionIndexes
    }
}

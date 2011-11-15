namespace WowPacketParser.Enums
{
    public enum DumpFormatType
    {
        None,           // No dump at all
        Text,
        Bin,
        Pkt,
        TextHeader,     // Dump only packet headers
        SummaryHeader   // Dump only header of first packet found of same type (Opcode + Direction)
    }
}

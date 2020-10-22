namespace WowPacketParser.Enums
{
    public enum HotfixStatus : byte
    {
        Valid        = 1,
        Invalid      = 2, // removed record
        Unavailable  = 3  // conditional or non-existing record
    }
}

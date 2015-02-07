namespace WowPacketParser.Enums
{
    public enum WardenServerOpcode
    {
        ModuleInfo  = 0,
        ModuleChunk = 1,
        CheatChecks = 2,
        Data        = 3,
        Seed        = 5
    }

    public enum WardenClientOpcode
    {
        ModuleLoadFailed  = 0,
        ModuleLoaded      = 1,
        CheatCheckResults = 2,
        TransformedSeed   = 4
    }
}

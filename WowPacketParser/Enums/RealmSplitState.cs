namespace WowPacketParser.Enums
{
    // From GlueParent.lua
    public enum ClientSplitState
    {
        Uninitialized = -1,
        NoChoice      = 0,
        Realm1        = 1,
        Realm2        = 2
    }

    public enum PendingSplitState
    {
        Uninitialized       = -1,
        NoServerSplit       = 0,
        ServerSplitChoice   = 1,
        ServerSplitNoChoice = 2
    }
}

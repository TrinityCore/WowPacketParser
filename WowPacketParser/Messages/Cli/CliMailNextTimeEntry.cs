using WowPacketParser.Messages.Player;

namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliMailNextTimeEntry
    {
        public ulong SenderGUID;
        public PlayerGuidLookupHint SenderHint;
        public float TimeLeft;
        public int AltSenderID;
        public byte AltSenderType;
        public int StationeryID;
    }
}

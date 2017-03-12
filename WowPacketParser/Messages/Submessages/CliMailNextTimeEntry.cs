using WowPacketParser.Messages.Player;

namespace WowPacketParser.Messages.Submessages
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

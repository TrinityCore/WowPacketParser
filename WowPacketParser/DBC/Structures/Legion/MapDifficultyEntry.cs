namespace WowPacketParser.DBC.Structures.Legion
{
    [DBFile("MapDifficulty")]

    public class MapDifficultyEntry
    {
        public string Message;
        public byte DifficultyID;
        public byte ResetInterval;
        public byte MaxPlayers;
        public byte LockID;
        public byte Flags;
        public byte ItemContext;
        public uint ItemContextPickerID;
        public ushort MapID;
    }
}

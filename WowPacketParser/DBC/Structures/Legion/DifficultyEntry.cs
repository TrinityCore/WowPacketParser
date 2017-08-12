using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    [DBFile("Difficulty")]

    public sealed class DifficultyEntry
    {
        public string Name;
        public ushort GroupSizeHealthCurveID;
        public ushort GroupSizeDmgCurveID;
        public ushort GroupSizeSpellPointsCurveID;
        public byte FallbackDifficultyID;
        public byte InstanceType;
        public byte MinPlayers;
        public byte MaxPlayers;
        public sbyte OldEnumValue;
        public byte Flags;
        public byte ToggleDifficultyID;
        public byte ItemBonusTreeModID;
        public byte OrderIndex;
    }
}

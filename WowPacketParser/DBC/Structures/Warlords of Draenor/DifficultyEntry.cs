using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    [DBFileName("Difficulty")]

    public sealed class DifficultyEntry
    {
        public string Name;
        public byte FallbackDifficultyID;
        public byte InstanceType;
        public byte MinPlayers;
        public byte MaxPlayers;
        public sbyte OldEnumValue;
        public byte Flags;
        public byte ToggleDifficultyID;
        public byte GroupSizeHealthCurveID;
        public byte GroupSizeDmgCurveID;
        public byte GroupSizeSpellPointsCurveID;
        public byte ItemBonusTreeModID;
        public byte OrderIndex;
    }
}

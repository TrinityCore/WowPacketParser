using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.TheWarWithin
{
    [DBFile("Difficulty")]
    public sealed class DifficultyEntry
    {
        [Index(true)]
        public uint ID;
        public string Name;
        public byte InstanceType;
        public byte OrderIndex;
        public sbyte OldEnumValue;
        public byte FallbackDifficultyID;
        public byte MinPlayers;
        public byte MaxPlayers;
        public ushort Flags;
        public byte ItemContext;
        public byte ToggleDifficultyID;
        public uint GroupSizeHealthCurveID;
        public uint GroupSizeDmgCurveID;
        public uint GroupSizeSpellPointsCurveID;
        public int Unknown1105;
    }
}

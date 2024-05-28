using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_visual_kit")]
    public sealed record SpellVisualKitHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("FallbackSpellVisualKitID")]
        public uint? FallbackSpellVisualKitID;

        [DBFieldName("DelayMin")]
        public ushort? DelayMin;

        [DBFieldName("DelayMax")]
        public ushort? DelayMax;

        [DBFieldName("FallbackPriority")]
        public float? FallbackPriority;

        [DBFieldName("Flags", 2)]
        public int?[] Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}

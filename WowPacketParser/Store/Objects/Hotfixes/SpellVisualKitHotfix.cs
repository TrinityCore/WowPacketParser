using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_visual_kit")]
    public sealed record SpellVisualKitHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("FallbackPriority")]
        public sbyte? FallbackPriority;

        [DBFieldName("FallbackSpellVisualKitId")]
        public int? FallbackSpellVisualKitId;

        [DBFieldName("DelayMin")]
        public ushort? DelayMin;

        [DBFieldName("DelayMax")]
        public ushort? DelayMax;

        [DBFieldName("Flags", 2)]
        public int?[] Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}

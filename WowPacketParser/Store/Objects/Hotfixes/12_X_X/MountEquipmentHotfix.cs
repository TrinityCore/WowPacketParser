using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("mount_equipment")]
    public sealed record MountEquipmentHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Item")]
        public int? Item;

        [DBFieldName("BuffSpell")]
        public int? BuffSpell;

        [DBFieldName("Unknown820")]
        public int? Unknown820;

        [DBFieldName("LearnedBySpell")]
        public uint? LearnedBySpell;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}

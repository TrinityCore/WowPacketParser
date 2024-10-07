using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_context_picker_entry")]
    public sealed record ItemContextPickerEntryHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemCreationContext")]
        public byte? ItemCreationContext;

        [DBFieldName("OrderIndex")]
        public byte? OrderIndex;

        [DBFieldName("PVal")]
        public int? PVal;

        [DBFieldName("LabelID")]
        public int? LabelID;

        [DBFieldName("Flags")]
        public uint? Flags;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("ItemContextPickerID")]
        public uint? ItemContextPickerID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}

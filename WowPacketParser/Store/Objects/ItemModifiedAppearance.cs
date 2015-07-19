using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("item_modified_appearance")]
    public sealed class ItemModifiedAppearance
    {
        [DBFieldName("ItemID")]
        public uint ItemID;

        [DBFieldName("AppearanceModID")]
        public uint AppearanceModID;

        [DBFieldName("AppearanceID")]
        public uint AppearanceID;

        [DBFieldName("IconFileDataID")]
        public uint IconFileDataID;

        [DBFieldName("Index")]
        public uint Index;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}

using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_equip_template")]
    public sealed record CreatureEquipment : IDataModel
    {
        [DBFieldName("CreatureID", true)]
        public uint? CreatureID;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemID1")]
        public uint? ItemID1;

        [DBFieldName("AppearanceModID1", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.WotlkClassic)]
        public ushort? AppearanceModID1;

        [DBFieldName("ItemVisual1", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.WotlkClassic)]
        public ushort? ItemVisual1;

        [DBFieldName("ItemID2")]
        public uint? ItemID2;

        [DBFieldName("AppearanceModID2", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.WotlkClassic)]
        public ushort? AppearanceModID2;

        [DBFieldName("ItemVisual2", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.WotlkClassic)]
        public ushort? ItemVisual2;

        [DBFieldName("ItemID3")]
        public uint? ItemID3;

        [DBFieldName("AppearanceModID3", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.WotlkClassic)]
        public ushort? AppearanceModID3;

        [DBFieldName("ItemVisual3", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.WotlkClassic)]
        public ushort? ItemVisual3;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;

        public bool EquipEqual(CreatureEquipment other)
        {
            if (ItemID1 != other.ItemID1 ||
                ItemID2 != other.ItemID2 ||
                ItemID3 != other.ItemID3 ||
                AppearanceModID1 != other.AppearanceModID1 ||
                AppearanceModID2 != other.AppearanceModID2 ||
                AppearanceModID3 != other.AppearanceModID3 ||
                ItemVisual1 != other.ItemVisual1 ||
                ItemVisual2 != other.ItemVisual2 ||
                ItemVisual3 != other.ItemVisual3)
                return false;
            return true;
        }

        public bool EquipEqual(IVisibleItem[] other)
        {
            if (ItemID1 != other[0].ItemID ||
                ItemID2 != other[1].ItemID ||
                ItemID3 != other[2].ItemID ||
                AppearanceModID1 != other[0].ItemAppearanceModID ||
                AppearanceModID2 != other[1].ItemAppearanceModID ||
                AppearanceModID3 != other[2].ItemAppearanceModID ||
                ItemVisual1 != other[0].ItemVisual ||
                ItemVisual2 != other[1].ItemVisual ||
                ItemVisual3 != other[2].ItemVisual)
                return false;
            return true;
        }
    }
}

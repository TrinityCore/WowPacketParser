using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

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

        [DBFieldName("AppearanceModID1", TargetedDatabase.Legion)]
        public ushort? AppearanceModID1;

        [DBFieldName("ItemVisual1", TargetedDatabase.Legion)]
        public ushort? ItemVisual1;

        [DBFieldName("ItemID2")]
        public uint? ItemID2;

        [DBFieldName("AppearanceModID2", TargetedDatabase.Legion)]
        public ushort? AppearanceModID2;

        [DBFieldName("ItemVisual2", TargetedDatabase.Legion)]
        public ushort? ItemVisual2;

        [DBFieldName("ItemID3")]
        public uint? ItemID3;

        [DBFieldName("AppearanceModID3", TargetedDatabase.Legion)]
        public ushort? AppearanceModID3;

        [DBFieldName("ItemVisual3", TargetedDatabase.Legion)]
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
    }
}

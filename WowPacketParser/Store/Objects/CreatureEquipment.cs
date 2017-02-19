using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_equip_template")]
    public sealed class CreatureEquipment : IDataModel
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

    }
}

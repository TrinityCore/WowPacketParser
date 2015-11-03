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

        [DBFieldName("ItemID2")]
        public uint? ItemID2;

        [DBFieldName("ItemID3")]
        public uint? ItemID3;
    }
}

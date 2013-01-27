using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_equip_template")]
    public sealed class CreatureEquipment
    {
        [DBFieldName("itemEntry1")]
        public uint ItemEntry1;

        [DBFieldName("itemEntry2")]
        public uint ItemEntry2;

        [DBFieldName("itemEntry3")]
        public uint ItemEntry3;
    }
}

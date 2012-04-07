using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_template")]
    public struct SpellsX
    {
        [DBFieldName("spell", Count = 8)]
        public uint[] Spells;
    }
}

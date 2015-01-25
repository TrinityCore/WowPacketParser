using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_template", WPPDatabase.World)]
    public struct SpellsX
    {
        [DBFieldName("spell", 8)]
        public uint[] Spells;
    }
}

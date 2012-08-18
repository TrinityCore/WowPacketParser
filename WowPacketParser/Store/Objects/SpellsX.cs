using PacketParser.SQL;

namespace PacketDumper.DataStructures
{
    [DBTableName("creature_template")]
    public struct SpellsX
    {
        [DBFieldName("spell", Count = 8)]
        public uint[] Spells;
    }
}

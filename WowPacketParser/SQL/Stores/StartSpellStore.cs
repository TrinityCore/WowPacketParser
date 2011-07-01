using WowPacketParser.Enums;


namespace WowPacketParser.SQL.Stores
{
    public sealed class StartSpellStore
    {
        public string GetCommand(Race race, Class clss, int spellId)
        {
            var builder = new SQLCommandBuilder("playercreateinfo_spell");

            builder.AddColumnValue("race", (int)race);
            builder.AddColumnValue("class", (int)clss);
            builder.AddColumnValue("Spell", spellId);
            builder.AddColumnValue("Note", string.Empty);

            return builder.BuildInsert();
        }
    }
}

using WowPacketParser.Enums;

namespace WowPacketParser.SQL.SQLStore.Stores
{
    public sealed class StartSpellStore
    {
        public string GetCommand(Race race, Class clss, int spellId)
        {
            var builder = new CommandBuilder("playercreateinfo_spell");

            builder.AddColumnValue("race", (int)race);
            builder.AddColumnValue("class", (int)clss);
            builder.AddColumnValue("Spell", spellId);
            builder.AddColumnValue("Note", string.Empty);

            return builder.BuildInsert();
        }
    }
}

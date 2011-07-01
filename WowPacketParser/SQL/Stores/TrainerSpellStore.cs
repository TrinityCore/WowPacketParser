

namespace WowPacketParser.SQL.Stores
{
    public sealed class TrainerSpellStore
    {
        public string GetCommand(uint entry, int spellId, int cost, int reqLevel, int reqSkill,
            int reqSkLvl)
        {
            var builder = new SQLCommandBuilder("npc_trainer");

            builder.AddColumnValue("entry", entry);
            builder.AddColumnValue("spell", spellId);
            builder.AddColumnValue("spellcost", cost);
            builder.AddColumnValue("reqskill", reqSkill);
            builder.AddColumnValue("reqskillvalue", reqSkLvl);
            builder.AddColumnValue("reqlevel", reqLevel);

            return builder.BuildInsert();
        }
    }
}

using WowPacketParser.Enums;


namespace WowPacketParser.SQL.Stores
{
    public sealed class StartActionStore
    {
        public string GetCommand(Race race, Class clss, int action, int button, ActionButtonType type)
        {
            var builder = new SQLCommandBuilder("playercreateinfo_action");

            builder.AddColumnValue("race", (int)race);
            builder.AddColumnValue("class", (int)clss);
            builder.AddColumnValue("button", button);
            builder.AddColumnValue("action", action);
            builder.AddColumnValue("type", (int)type);

            return builder.BuildInsert();
        }
    }
}

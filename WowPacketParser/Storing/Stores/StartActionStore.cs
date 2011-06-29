using WowPacketParser.Enums;

namespace WowPacketParser.Storing.Stores
{
    public sealed class StartActionStore
    {
        public string GetCommand(Race race, Class clss, int action, int button, ActionButtonType type)
        {
            var builder = new CommandBuilder("playercreateinfo_action");

            builder.AddColumnValue("race", (int)race);
            builder.AddColumnValue("class", (int)clss);
            builder.AddColumnValue("button", button);
            builder.AddColumnValue("action", action);
            builder.AddColumnValue("type", (int)type);

            return builder.BuildInsert(true);
        }
    }
}

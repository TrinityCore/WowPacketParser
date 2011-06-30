namespace WowPacketParser.SQL.SQLStore.Stores
{
    public sealed class PageTextStore
    {
        public string GetCommand(int entry, string pageText, int nextPage)
        {
            var cmd = new CommandBuilder("page_text");

            cmd.AddColumnValue("entry", entry);
            cmd.AddColumnValue("text", pageText);
            cmd.AddColumnValue("next_page", nextPage);

            return cmd.BuildInsert();
        }
    }
}

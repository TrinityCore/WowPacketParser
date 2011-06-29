using WowPacketParser.Enums;

namespace WowPacketParser.Storing.Stores
{
    public sealed class NpcTextStore
    {
        public string GetCommand(int entry, float[] prob, string[] text1, string[] text2,
            Language[] lang, int[][] emDelay, int[][] emEmote)
        {
            var builder = new CommandBuilder("npc_text");

            builder.AddColumnValue("ID", entry);

            for (var i = 0; i < 8; i++)
            {
                builder.AddColumnValue("text" + i + "_0", text1[i]);
                builder.AddColumnValue("text" + i + "_1", text2[i]);
                builder.AddColumnValue("lang" + i, (int)lang[i]);
                builder.AddColumnValue("prob" + i, prob[i]);

                var k = 0;
                for (var j = 0; j < 3; j++)
                {
                    builder.AddColumnValue("em" + i + "_" + k, emDelay[i][j]);
                    k++;
                    builder.AddColumnValue("em" + i + "_" + k, emEmote[i][j]);
                    k++;
                }
            }

            return builder.BuildInsert(true);
        }
    }
}

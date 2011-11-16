using System;
using System.Text;

namespace WowPacketParser.Misc
{
    public static class Statistics
    {
        public static DateTime StartTime { get; set; }
        public static DateTime EndTime { get; set; }

        public static uint Total { get; set; }

        public static uint PacketsSuccessfullyParsed { get; set; }
        public static uint PacketsParsedWithErrors { get; set; }
        public static uint PacketsNotParsed { get; set; }

        public static TimeSpan TimeToParse()
        {
            return EndTime.Subtract(StartTime);
        }

        public static string Stats(string fileName)
        {
            var res = new StringBuilder();
            var span = TimeToParse();

            res.AppendFormat("{0}: Finished parsing in {1} Minutes, {2} Seconds and {3} Milliseconds.",
                fileName, span.Minutes, span.Seconds, span.Milliseconds);

            res.AppendLine();

            res.AppendFormat("{0}: Parsed {1:F1}% packets successfully, {2:F1}% with errors and skipped {3:F1}%.",
                fileName, (double)PacketsSuccessfullyParsed / Total * 100, (double)PacketsParsedWithErrors / Total * 100, (double)PacketsNotParsed / Total * 100);

            return res.ToString();
        }

        public static void Reset()
        {
            StartTime = new DateTime(0);
            EndTime = new DateTime(0);
            Total = 0;
            PacketsSuccessfullyParsed = 0;
            PacketsParsedWithErrors = 0;
            PacketsNotParsed = 0;
        }
    }
}

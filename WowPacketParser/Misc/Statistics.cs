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

        public static string Stats()
        {
            var res = new StringBuilder();
            var span = TimeToParse();

            res.AppendFormat("Finished parsing in {0} Minutes, {1} Seconds and {2} Milliseconds.",
                span.Minutes, span.Seconds, span.Milliseconds);

            res.AppendLine();

            res.AppendFormat("Parsed {0:F1}% packets successfully, {1:F1}% with errors and skipped {2:F1}%.",
                (double)PacketsSuccessfullyParsed / Total * 100, (double)PacketsParsedWithErrors / Total * 100, (double)PacketsNotParsed / Total * 100);

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

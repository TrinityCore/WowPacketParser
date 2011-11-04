using System;
using System.Text;

namespace WowPacketParser.Misc
{
    public sealed class Statistics
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public uint Total { get; set; }

        public uint PacketsSuccessfullyParsed { get; set; }
        public uint PacketsParsedWithErrors { get; set; }
        public uint PacketsNotParsed { get; set; }

        public TimeSpan TimeToParse()
        {
            return EndTime.Subtract(StartTime);
        }

        public string Stats()
        {
            StringBuilder res = new StringBuilder();
            var span = TimeToParse();

            res.AppendFormat("Finished parsing in {0} Minutes, {1} Seconds and {2} Milliseconds.",
                span.Minutes, span.Seconds, span.Milliseconds);

            res.AppendLine();

            res.AppendFormat("Parsed {0:F1}% packets successfully, {1:F1}% with errors and skipped {2:F1}%.",
                (double)PacketsSuccessfullyParsed / Total * 100, (double)PacketsParsedWithErrors / Total * 100, (double)PacketsNotParsed / Total * 100);

            return res.ToString();
        }
    }
}

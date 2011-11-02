using System;

namespace WowPacketParser.Misc
{
    public sealed class Statistics
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        
        public uint Total
        {
            get { return PacketsParsed + PacketsNotParsed; }
        }

        public uint PacketsParsed { get; set; }

        public uint PacketsParsedWithErrors { get; set; }

        public uint PacketsNotParsed { get; set; }

        public double PercentageParsedPackets()
        {
            var result = (1 - (((double)Total - PacketsParsed) / Total)) * 100;
            return Math.Round(result);
        }

        public double PercentageParsedPacketsWithErrors()
        {
            var result = (1 - (((double)Total - PacketsParsedWithErrors) / Total)) * 100;
            return Math.Round(result);
        }

        public TimeSpan TimeToParse()
        {
            return EndTime.Subtract(StartTime);
        }

        public string Stats()
        {
            var span = TimeToParse();

            string res = "Finished parsing in - " + span.Minutes + " Minutes, " + span.Seconds + " Seconds and " +
                         span.Milliseconds + " Milliseconds.";
            res += Environment.NewLine;
            res += Total + " packets, " + PercentageParsedPackets() + "% parsed (" + PercentageParsedPacketsWithErrors() + "% with errors).";

            return res;
        }
    }
}

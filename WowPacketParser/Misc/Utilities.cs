using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace WowPacketParser.Misc
{
    public static class Utilities
    {
        public static DateTime GetDateTimeFromUnixTime(int unixTime)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(unixTime);
        }

        public static double GetUnixTimeFromDateTime(DateTime time)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan span = (time - epoch);
            return span.TotalSeconds;
        }

        public static byte[] HexStringToBinary(string data)
        {
            var bytes = new List<byte>();
            for (var i = 0; i < data.Length; i += 2)
                bytes.Add(Byte.Parse(data.Substring(i, 2), NumberStyles.HexNumber));

            return bytes.ToArray();
        }

        public static string ByteArrayToHexString(byte[] data)
        {
            var str = string.Empty;
            for (var i = 0; i < data.Length; ++i)
                str += data[i].ToString("X2", CultureInfo.InvariantCulture);

            return str;
        }

        public static DateTime GetDateTimeFromGameTime(int packedDate)
        {
            var minute = packedDate & 0x3F;
            var hour = (packedDate >> 6) & 0x1F;
            var day = (packedDate >> 14) & 0x3F;
            var month = (packedDate >> 20) & 0xF;
            var year = (packedDate >> 24) & 0x1F;

            return new DateTime(year + 2000, month + 1, day + 1, hour, minute, 0);
        }
    }
}

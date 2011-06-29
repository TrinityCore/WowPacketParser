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

        public static string DumpPacketAsHex(Packet packet)
        {
            var n = Environment.NewLine;
            var hexDump = new StringBuilder();
            var length = packet.GetLength();
            var stream = packet.GetStream(0);

            var header = "|-------------------------------------------------|------------------" +
                "---------------|" + n +
                "| 00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F | 0 1 2 3 4 5 6 7 8 9 A B C D E F |" +
                n + "|-------------------------------------------------|------------------" + 
                "---------------|" + n;

            hexDump.Append(header);

            var end = length;
            for (var i = 0; i < end; i += 16)
            {
                var text = new StringBuilder();
                var hex = new StringBuilder();
                hex.Append("| ");

                for (var j = 0; j < 16; j++)
                {
                    if (j + i < end)
                    {
                        var val = stream[j + i];
                        hex.Append(stream[j + i].ToString("X2"));
                        hex.Append(" ");

                        if (val >= 32 && val <= 127)
                            text.Append((char)val);
                        else
                            text.Append(".");

                        text.Append(" ");
                    }
                    else
                    {
                        hex.Append("   ");
                        text.Append("  ");
                    }
                }

                hex.Append("| ");
                hex.Append(text + "|");
                hex.Append(n);
                hexDump.Append(hex.ToString());
            }

            hexDump.Append("|-------------------------------------------------|------------------" +
                "---------------|");

            return hexDump.ToString();
        }

        public static string GetPathFromFullPath(string fullPath)
        {
            return !fullPath.Contains("\\") ? Directory.GetCurrentDirectory() :
                fullPath.Substring(0, fullPath.LastIndexOf("\\") + 1);
        }
    }
}

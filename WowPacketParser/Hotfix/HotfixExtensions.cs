using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowPacketParser.Hotfix
{
    public static class HotfixExtensions
    {
        /// <summary>
        /// Escapes the provided string's double quotes.
        /// 
        /// This <b>is</b> (and <b>should</b>) only be used by hotfix serializers.
        /// </summary>
        public static void Append(this List<string> writer, string line)
        {
            writer.Add($"\"{line.Replace("\"", "\\\"")}\"");
        }

        public static void Append(this List<string> writer, long line) => writer.Add(line.ToString());
        public static void Append(this List<string> writer, ulong line) => writer.Add(line.ToString());
        public static void Append(this List<string> writer, int line) => writer.Add(line.ToString());
        public static void Append(this List<string> writer, uint line) => writer.Add(line.ToString());
        public static void Append(this List<string> writer, short line) => writer.Add(line.ToString());
        public static void Append(this List<string> writer, ushort line) => writer.Add(line.ToString());
        public static void Append(this List<string> writer, byte line) => writer.Add(line.ToString());
        public static void Append(this List<string> writer, sbyte line) => writer.Add(line.ToString());
        public static void Append(this List<string> writer, float line) => writer.Add(line.ToString(CultureInfo.InvariantCulture));
        public static void Append(this List<string> writer, double line) => writer.Add(line.ToString(CultureInfo.InvariantCulture));

        public static void AppendArray(this List<string> writer, string[] lines)
        {
            foreach (var line in lines)
                writer.Append(line);
        }

        public static void AppendArray(this List<string> writer, long[] lines)
        {
            foreach (var line in lines)
                writer.Append(line);
        }

        public static void AppendArray(this List<string> writer, ulong[] lines)
        {
            foreach (var line in lines)
                writer.Append(line);
        }

        public static void AppendArray(this List<string> writer, int[] lines)
        {
            foreach (var line in lines)
                writer.Append(line);
        }

        public static void AppendArray(this List<string> writer, uint[] lines)
        {
            foreach (var line in lines)
                writer.Append(line);
        }

        public static void AppendArray(this List<string> writer, short[] lines)
        {
            foreach (var line in lines)
                writer.Append(line);
        }

        public static void AppendArray(this List<string> writer, ushort[] lines)
        {
            foreach (var line in lines)
                writer.Append(line);
        }

        public static void AppendArray(this List<string> writer, byte[] lines)
        {
            foreach (var line in lines)
                writer.Append(line);
        }

        public static void AppendArray(this List<string> writer, sbyte[] lines)
        {
            foreach (var line in lines)
                writer.Append(line);
        }

        public static void AppendArray(this List<string> writer, float[] lines)
        {
            foreach (var line in lines)
                writer.Append(line);
        }

        public static void AppendArray(this List<string> writer, double[] lines)
        {
            foreach (var line in lines)
                writer.Append(line);
        }
    }
}

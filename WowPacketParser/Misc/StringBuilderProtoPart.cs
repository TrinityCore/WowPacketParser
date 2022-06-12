using System.Text;
using WowPacketParser.Enums;
#nullable enable

namespace WowPacketParser.Misc
{
    public readonly struct StringBuilderProtoPart
    {
        private readonly StringBuilder? stringBuilder;
        private readonly int startLength;

        public StringBuilderProtoPart(StringBuilder? stringBuilder)
        {
            this.stringBuilder = stringBuilder;
            startLength = stringBuilder?.Length ?? 0;
        }

        public int StartOffset => startLength;

        public int Length => stringBuilder?.Length - startLength ?? 0;

        public string Text
        {
            get
            {
                if (Settings.DumpFormat == DumpFormatType.UniversalProtoWithText)
                    return stringBuilder?.ToString(StartOffset, Length) ?? "";
                return "";
            }
        }
    }
}

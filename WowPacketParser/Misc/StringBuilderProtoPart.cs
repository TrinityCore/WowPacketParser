using System.Text;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public readonly struct StringBuilderProtoPart
    {
        private readonly StringBuilder stringBuilder;
        private readonly int startLength;

        public StringBuilderProtoPart(StringBuilder stringBuilder)
        {
            this.stringBuilder = stringBuilder;
            startLength = stringBuilder?.Length ?? 0;
        }

        public string Text
        {
            get
            {
                if (Settings.DumpFormat == DumpFormatType.UniversalProtoWithText)
                    return stringBuilder?.ToString(startLength, stringBuilder.Length - startLength) ?? "";
                return "";
            }
        }
    }
}

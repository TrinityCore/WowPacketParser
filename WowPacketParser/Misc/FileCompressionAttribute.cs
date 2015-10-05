using System;

namespace WowPacketParser.Misc
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class FileCompressionAttribute : Attribute
    {
        public string Extension { get; private set; }

        public FileCompressionAttribute(string extension)
        {
            Extension = extension;
        }
    }
}

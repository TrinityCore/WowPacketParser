using WowPacketParser.Misc;

namespace WowPacketParser.Enums
{
    public enum FileCompression
    {
        None = 0,
        [FileCompression(".gz")]
        GZip = 1
    }
}

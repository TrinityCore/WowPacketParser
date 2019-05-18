using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Movie, HasIndexInData = false)]
    public class MovieEntry
    {
        public byte Volume { get; set; }
        public byte KeyID { get; set; }
        public uint AudioFileDataID { get; set; }
        public uint SubtitleFileDataID { get; set; }
    }
}

using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.MovieFileData, HasIndexInData = false)]
    public class MovieFileDataEntry
    {
        public ushort Resolution { get; set; }
    }
}

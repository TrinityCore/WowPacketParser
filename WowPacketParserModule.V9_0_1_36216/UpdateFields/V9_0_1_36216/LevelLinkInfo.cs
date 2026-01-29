using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_1_36216
{
    public class LevelLinkInfo : ILevelLinkInfo
    {
        public WowGuid TargetGUID { get; set; }
        public int Field_10 { get; set; }
    }
}


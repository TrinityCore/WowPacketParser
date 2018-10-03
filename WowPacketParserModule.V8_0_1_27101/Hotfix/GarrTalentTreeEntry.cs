using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrTalentTree, HasIndexInData = false)]
    public class GarrTalentTreeEntry
    {
        public string Title { get; set; }
        public int GarrTypeID { get; set; }
        public int ClassID { get; set; }
        public sbyte MaxTiers { get; set; }
        public sbyte UiOrder { get; set; }
        public sbyte Flags { get; set; }
        public ushort UiTextureKitID { get; set; }
    }
}

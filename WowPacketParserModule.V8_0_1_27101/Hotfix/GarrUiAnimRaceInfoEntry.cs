using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrUiAnimRaceInfo, HasIndexInData = false)]
    public class GarrUiAnimRaceInfoEntry
    {
        public byte GarrFollRaceID { get; set; }
        public float MaleScale { get; set; }
        public float MaleHeight { get; set; }
        public float FemaleScale { get; set; }
        public float FemaleHeight { get; set; }
        public float MaleSingleModelScale { get; set; }
        public float MaleSingleModelHeight { get; set; }
        public float FemaleSingleModelScale { get; set; }
        public float FemaleSingleModelHeight { get; set; }
        public float MaleFollowerPageScale { get; set; }
        public float MaleFollowerPageHeight { get; set; }
        public float FemaleFollowerPageScale { get; set; }
        public float FemaleFollowerPageHeight { get; set; }
    }
}

using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.InvasionClientData)]
    public class InvasionClientDataEntry
    {
        public string Name { get; set; }
        [HotfixArray(2)]
        public float[] IconLocation { get; set; }
        public int ID { get; set; }
        public int WorldStateID { get; set; }
        public int UiTextureAtlasMemberID { get; set; }
        public int ScenarioID { get; set; }
        public uint WorldQuestID { get; set; }
        public int WorldStateValue { get; set; }
        public int InvasionEnabledWorldStateID { get; set; }
        public int AreaTableID { get; set; }
    }
}

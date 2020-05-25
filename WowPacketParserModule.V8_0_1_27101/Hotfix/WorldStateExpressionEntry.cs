using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.WorldStateExpression, HasIndexInData = false)]
    public class WorldStateExpressionEntry
    {
        public string Expression { get; set; }
    }
}

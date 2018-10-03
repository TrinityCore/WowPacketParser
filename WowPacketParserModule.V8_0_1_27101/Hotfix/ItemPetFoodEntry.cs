using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemPetFood, HasIndexInData = false)]
    public class ItemPetFoodEntry
    {
        public string Name { get; set; }
    }
}

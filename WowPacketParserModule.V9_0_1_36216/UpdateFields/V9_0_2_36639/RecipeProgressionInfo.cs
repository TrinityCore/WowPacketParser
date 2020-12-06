using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_2_36639
{
    public class RecipeProgressionInfo : IRecipeProgressionInfo
    {
        public ushort RecipeProgressionGroupID { get; set; }
        public ushort Experience { get; set; }
    }
}


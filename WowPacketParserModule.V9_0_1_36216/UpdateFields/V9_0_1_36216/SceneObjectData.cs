using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_1_36216
{
    public class SceneObjectData : ISceneObjectData
    {
        public int? ScriptPackageID { get; set; }
        public uint RndSeedVal { get; set; }
        public WowGuid CreatedBy { get; set; }
        public uint? SceneType { get; set; }
    }
}


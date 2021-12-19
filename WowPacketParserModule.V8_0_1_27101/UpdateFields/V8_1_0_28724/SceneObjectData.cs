using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_1_0_28724
{
    public class SceneObjectData : ISceneObjectData
    {
        public int? ScriptPackageID { get; set; }
        public uint RndSeedVal { get; set; }
        public WowGuid CreatedBy { get; set; }
        public uint? SceneType { get; set; }
    }
}


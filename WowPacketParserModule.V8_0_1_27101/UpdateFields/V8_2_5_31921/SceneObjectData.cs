using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_2_5_31921
{
    public class SceneObjectData : ISceneObjectData
    {
        public int? ScriptPackageID { get; set; }
        public uint RndSeedVal { get; set; }
        public WowGuid CreatedBy { get; set; }
        public uint? SceneType { get; set; }
    }
}


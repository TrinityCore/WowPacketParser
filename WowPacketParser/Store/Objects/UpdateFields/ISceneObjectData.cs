
namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface ISceneObjectData
    {
        int? ScriptPackageID { get; }
        uint? SceneType { get; }
    }
}

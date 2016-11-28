using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("scenes")]
    public sealed class Scene : IDataModel
    {
        [DBFieldName("Id", true)]
        public uint? SceneID;

        [DBFieldName("Flags")]
        public uint? Flags;

        [DBFieldName("ScriptPackageID", true)]
        public uint? PackageID;
    }
}
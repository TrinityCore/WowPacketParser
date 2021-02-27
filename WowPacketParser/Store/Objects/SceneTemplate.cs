using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("scene_template")]
    public sealed class SceneTemplate : IDataModel
    {
        [DBFieldName("SceneId", true)]
        public uint? SceneID;

        [DBFieldName("Flags")]
        public uint? Flags;

        [DBFieldName("ScriptPackageID", true)]
        public uint? ScriptPackageID;

        [DBFieldName("Encrypted", TargetedDatabase.Shadowlands)]
        public bool? Encrypted;
    }
}

using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("scene_script")]
    public sealed record SceneScriptHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("FirstSceneScriptID")]
        public ushort? FirstSceneScriptID;

        [DBFieldName("NextSceneScriptID")]
        public ushort? NextSceneScriptID;

        [DBFieldName("Unknown915")]
        public int? Unknown915;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("scene_script")]
    public sealed record SceneScriptHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("FirstSceneScriptID")]
        public ushort? FirstSceneScriptID;

        [DBFieldName("NextSceneScriptID")]
        public ushort? NextSceneScriptID;

        [DBFieldName("Unknown915")]
        public int? Unknown915;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}

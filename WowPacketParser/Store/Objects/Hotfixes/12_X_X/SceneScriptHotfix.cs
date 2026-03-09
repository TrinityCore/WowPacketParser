using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("scene_script")]
    public sealed record SceneScriptHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("FirstSceneScriptID")]
        public uint? FirstSceneScriptID;

        [DBFieldName("NextSceneScriptID")]
        public uint? NextSceneScriptID;

        [DBFieldName("Unknown915")]
        public int? Unknown915;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}

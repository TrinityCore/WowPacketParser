using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("scene_script")]
    public sealed class SceneScript
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Script")]
        public string Script;

        [DBFieldName("PreviousSceneScriptPart")]
        public uint PreviousSceneScriptPart;

        [DBFieldName("NextSceneScriptPart")]
        public uint NextSceneScriptPart;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}

using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_template_addon")]
    public sealed class CreatureTemplateAddon : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("path_id")]
        public uint? PathID;

        [DBFieldName("mount")]
        public uint? MountID;

        [DBFieldName("bytes1")]
        public uint? Bytes1;

        [DBFieldName("bytes2")]
        public uint? Bytes2;

        [DBFieldName("emote")]
        public uint? Emote;

        [DBFieldName("auras")]
        public string Auras;

        public string CommentAuras;
    }
}

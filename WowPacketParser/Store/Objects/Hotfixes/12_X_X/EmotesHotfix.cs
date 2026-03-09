using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("emotes")]
    public sealed record EmotesHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RaceMask")]
        public long? RaceMask;

        [DBFieldName("EmoteSlashCommand")]
        public string EmoteSlashCommand;

        [DBFieldName("AnimID")]
        public short? AnimID;

        [DBFieldName("EmoteFlags")]
        public int? EmoteFlags;

        [DBFieldName("EmoteSpecProc")]
        public int? EmoteSpecProc;

        [DBFieldName("EmoteSpecProcParam")]
        public uint? EmoteSpecProcParam;

        [DBFieldName("EventSoundID")]
        public uint? EventSoundID;

        [DBFieldName("SpellVisualKitID")]
        public uint? SpellVisualKitID;

        [DBFieldName("ClassMask")]
        public int? ClassMask;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}

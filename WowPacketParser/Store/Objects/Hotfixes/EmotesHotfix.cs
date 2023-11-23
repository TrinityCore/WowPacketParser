using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("emotes")]
    public sealed record EmotesHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RaceMask")]
        public long? RaceMask;

        [DBFieldName("EmoteSlashCommand")]
        public string EmoteSlashCommand;

        [DBFieldName("AnimID")]
        public int? AnimID;

        [DBFieldName("EmoteFlags")]
        public uint? EmoteFlags;

        [DBFieldName("EmoteSpecProc")]
        public byte? EmoteSpecProc;

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

    [Hotfix]
    [DBTableName("emotes")]
    public sealed record EmotesHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RaceMask")]
        public long? RaceMask;

        [DBFieldName("EmoteSlashCommand")]
        public string EmoteSlashCommand;

        [DBFieldName("AnimID")]
        public int? AnimID;

        [DBFieldName("EmoteFlags")]
        public uint? EmoteFlags;

        [DBFieldName("EmoteSpecProc")]
        public byte? EmoteSpecProc;

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

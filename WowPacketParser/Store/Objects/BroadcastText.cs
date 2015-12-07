using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("broadcast_text")]
    public sealed class BroadcastText : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Language")]
        public int? Language;

        [DBFieldName("MaleText", LocaleConstant.enUS)]
        public string MaleText;

        [DBFieldName("FemaleText", LocaleConstant.enUS)]
        public string FemaleText;

        [DBFieldName("EmoteID", 3)]
        public uint?[] EmoteID;

        [DBFieldName("EmoteDelay", 3)]
        public uint?[] EmoteDelay;

        [DBFieldName("SoundId")]
        public uint? SoundId;

        [DBFieldName("UnkEmoteID")]
        public uint? UnkEmoteId;

        [DBFieldName("Type")]
        public uint? Type;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}

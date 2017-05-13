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

        [DBFieldName("MaleText", LocaleConstant.enUS)]
        public string MaleText;

        [DBFieldName("FemaleText", LocaleConstant.enUS)]
        public string FemaleText;

        [DBFieldName("EmoteID", 3)]
        public ushort?[] EmoteID;

        [DBFieldName("EmoteDelay", 3)]
        public ushort?[] EmoteDelay;

        [DBFieldName("UnkEmoteID")]
        public ushort? UnkEmoteID;

        [DBFieldName("Language")]
        public byte? Language;

        [DBFieldName("Type")]
        public byte? Type;

        [DBFieldName("SoundID", 2)]
        public uint?[] SoundID;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
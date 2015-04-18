using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("broadcast_text")]
    public class BroadcastText
    {
        [DBFieldName("Language")]
        public int Language;
        [DBFieldName("MaleText", LocaleConstant.enUS)]
        public string MaleText;
        [DBFieldName("FemaleText", LocaleConstant.enUS)]
        public string FemaleText;
        [DBFieldName("EmoteID", 3)]
        public uint[] EmoteID = new uint[3];
        [DBFieldName("EmoteDelay", 3)]
        public uint[] EmoteDelay = new uint[3];
        [DBFieldName("SoundId")]
        public uint SoundId;
        [DBFieldName("UnkEmoteID")]
        public uint UnkEmoteId;
        [DBFieldName("Type")]
        public uint Type;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}

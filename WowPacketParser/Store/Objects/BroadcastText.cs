using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("broadcast_text")]
    public sealed record BroadcastText : IDataModel
    {
        public ushort?[] EmoteID;
        public ushort?[] EmoteDelay;
        public uint?[] SoundEntriesID;

        public void ConvertToDBStruct()
        {
            EmoteID1 = EmoteID[0];
            EmoteID2 = EmoteID[1];
            EmoteID3 = EmoteID[2];

            EmoteDelay1 = EmoteDelay[0];
            EmoteDelay2 = EmoteDelay[1];
            EmoteDelay3 = EmoteDelay[2];

            SoundEntriesID1 = SoundEntriesID[0];
            SoundEntriesID2 = SoundEntriesID[1];
        }

        [DBFieldName("Text", LocaleConstant.enUS)]
        public string Text;

        [DBFieldName("Text1", LocaleConstant.enUS)]
        public string Text1;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("LanguageID")]
        public int? LanguageID;

        [DBFieldName("ConditionID")]
        public uint? ConditionID;

        [DBFieldName("EmotesID")]
        public ushort? EmotesID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("ChatBubbleDurationMs", TargetedDatabaseFlag.SinceBattleForAzeroth | TargetedDatabaseFlag.WotlkClassic)]
        public uint? ChatBubbleDurationMs;

        [DBFieldName("VoiceOverPriorityID", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.WotlkClassic)]
        public uint? VoiceOverPriorityID;

        [DBFieldName("SoundEntriesID1", TargetedDatabaseFlag.TillBattleForAzeroth)]
        [DBFieldName("SoundKitID1", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.WotlkClassic)]
        public uint? SoundEntriesID1;

        [DBFieldName("SoundEntriesID2", TargetedDatabaseFlag.TillBattleForAzeroth)]
        [DBFieldName("SoundKitID2", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.WotlkClassic)]
        public uint? SoundEntriesID2;

        [DBFieldName("EmoteID1")]
        public ushort? EmoteID1;

        [DBFieldName("EmoteID2")]
        public ushort? EmoteID2;

        [DBFieldName("EmoteID3")]
        public ushort? EmoteID3;

        [DBFieldName("EmoteDelay1")]
        public ushort? EmoteDelay1;

        [DBFieldName("EmoteDelay2")]
        public ushort? EmoteDelay2;

        [DBFieldName("EmoteDelay3")]
        public ushort? EmoteDelay3;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
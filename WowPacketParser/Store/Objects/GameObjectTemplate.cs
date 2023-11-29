using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gameobject_template")]
    public sealed record GameObjectTemplate : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("type")]
        public GameObjectType? Type;

        [DBFieldName("displayId")]
        public uint? DisplayID;

        [DBFieldName("name", LocaleConstant.enUS)] // ToDo: Add locale support
        public string Name;

        [DBFieldName("IconName")]
        public string IconName;

        [DBFieldName("castBarCaption", LocaleConstant.enUS)] // ToDo: Add locale support
        public string CastCaption;

        [DBFieldName("unk1")]
        public string UnkString;

        [DBFieldName("size")]
        public float? Size;

        //TODO: move to gameobject_questitem
        public uint?[] QuestItems;

        [DBFieldName("Data", TargetedDatabaseFlag.TillWrathOfTheLichKing, 24, true)]
        [DBFieldName("Data", TargetedDatabaseFlag.Cataclysm, 32, true)]
        [DBFieldName("Data", TargetedDatabaseFlag.WarlordsOfDraenor | TargetedDatabaseFlag.Legion, 33, true)]
        [DBFieldName("Data", TargetedDatabaseFlag.BattleForAzeroth, 34, true)]
        [DBFieldName("Data", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.WotlkClassic, 35, true)]
        public int?[] Data;

        [DBFieldName("RequiredLevel", TargetedDatabaseFlag.FromCataclysmTillBattleForAzeroth)]
        public int? RequiredLevel;

        [DBFieldName("ContentTuningId", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.WotlkClassic)]
        public int? ContentTuningId;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("gameobject_questitem")]
    public sealed record GameObjectTemplateQuestItem : IDataModel
    {
        [DBFieldName("GameObjectEntry", true)]
        public uint? GameObjectEntry;

        [DBFieldName("Idx", true)]
        public uint? Idx;

        [DBFieldName("ItemId")]
        public uint? ItemId;

        [DBFieldName("VerifiedBuild", TargetedDatabaseFlag.SinceWarlordsOfDraenor | TargetedDatabaseFlag.WotlkClassic)]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}

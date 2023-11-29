using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("area_trigger")]
    public sealed record AreaTriggerHotfix1000: IDataModel
    {
        [DBFieldName("PosX")]
        public float? PosX;

        [DBFieldName("PosY")]
        public float? PosY;

        [DBFieldName("PosZ")]
        public float? PosZ;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ContinentID")]
        public short? ContinentID;

        [DBFieldName("PhaseUseFlags")]
        public sbyte? PhaseUseFlags;

        [DBFieldName("PhaseID")]
        public short? PhaseID;

        [DBFieldName("PhaseGroupID")]
        public short? PhaseGroupID;

        [DBFieldName("Radius")]
        public float? Radius;

        [DBFieldName("BoxLength")]
        public float? BoxLength;

        [DBFieldName("BoxWidth")]
        public float? BoxWidth;

        [DBFieldName("BoxHeight")]
        public float? BoxHeight;

        [DBFieldName("BoxYaw")]
        public float? BoxYaw;

        [DBFieldName("ShapeType")]
        public sbyte? ShapeType;

        [DBFieldName("ShapeID")]
        public short? ShapeID;

        [DBFieldName("AreaTriggerActionSetID")]
        public short? AreaTriggerActionSetID;

        [DBFieldName("Flags")]
        public sbyte? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("area_trigger")]
    public sealed record AreaTriggerHotfix1007 : IDataModel
    {
        [DBFieldName("PosX")]
        public float? PosX;

        [DBFieldName("PosY")]
        public float? PosY;

        [DBFieldName("PosZ")]
        public float? PosZ;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ContinentID")]
        public short? ContinentID;

        [DBFieldName("PhaseUseFlags")]
        public sbyte? PhaseUseFlags;

        [DBFieldName("PhaseID")]
        public short? PhaseID;

        [DBFieldName("PhaseGroupID")]
        public short? PhaseGroupID;

        [DBFieldName("Radius")]
        public float? Radius;

        [DBFieldName("BoxLength")]
        public float? BoxLength;

        [DBFieldName("BoxWidth")]
        public float? BoxWidth;

        [DBFieldName("BoxHeight")]
        public float? BoxHeight;

        [DBFieldName("BoxYaw")]
        public float? BoxYaw;

        [DBFieldName("ShapeType")]
        public sbyte? ShapeType;

        [DBFieldName("ShapeID")]
        public short? ShapeID;

        [DBFieldName("AreaTriggerActionSetID")]
        public int? AreaTriggerActionSetID;

        [DBFieldName("Flags")]
        public sbyte? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("area_trigger")]
    public sealed record AreaTriggerHotfix1020 : IDataModel
    {
        [DBFieldName("PosX")]
        public float? PosX;

        [DBFieldName("PosY")]
        public float? PosY;

        [DBFieldName("PosZ")]
        public float? PosZ;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ContinentID")]
        public short? ContinentID;

        [DBFieldName("PhaseUseFlags")]
        public int? PhaseUseFlags;

        [DBFieldName("PhaseID")]
        public short? PhaseID;

        [DBFieldName("PhaseGroupID")]
        public short? PhaseGroupID;

        [DBFieldName("Radius")]
        public float? Radius;

        [DBFieldName("BoxLength")]
        public float? BoxLength;

        [DBFieldName("BoxWidth")]
        public float? BoxWidth;

        [DBFieldName("BoxHeight")]
        public float? BoxHeight;

        [DBFieldName("BoxYaw")]
        public float? BoxYaw;

        [DBFieldName("ShapeType")]
        public sbyte? ShapeType;

        [DBFieldName("ShapeID")]
        public short? ShapeID;

        [DBFieldName("AreaTriggerActionSetID")]
        public int? AreaTriggerActionSetID;

        [DBFieldName("Flags")]
        public sbyte? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("area_trigger")]
    public sealed record AreaTriggerHotfix340: IDataModel
    {
        [DBFieldName("Message")]
        public string Message;

        [DBFieldName("PosX")]
        public float? PosX;

        [DBFieldName("PosY")]
        public float? PosY;

        [DBFieldName("PosZ")]
        public float? PosZ;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ContinentID")]
        public short? ContinentID;

        [DBFieldName("PhaseUseFlags")]
        public sbyte? PhaseUseFlags;

        [DBFieldName("PhaseID")]
        public short? PhaseID;

        [DBFieldName("PhaseGroupID")]
        public short? PhaseGroupID;

        [DBFieldName("Radius")]
        public float? Radius;

        [DBFieldName("BoxLength")]
        public float? BoxLength;

        [DBFieldName("BoxWidth")]
        public float? BoxWidth;

        [DBFieldName("BoxHeight")]
        public float? BoxHeight;

        [DBFieldName("BoxYaw")]
        public float? BoxYaw;

        [DBFieldName("ShapeType")]
        public sbyte? ShapeType;

        [DBFieldName("ShapeID")]
        public short? ShapeID;

        [DBFieldName("AreaTriggerActionSetID")]
        public short? AreaTriggerActionSetID;

        [DBFieldName("Flags")]
        public sbyte? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("area_trigger_locale")]
    public sealed record AreaTriggerLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Message_lang")]
        public string MessageLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}

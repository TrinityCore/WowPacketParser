using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("mount")]
    public sealed class Mount : IDataModel
    {
        [DBFieldName("Id", true)]
        public uint? ID;

        [DBFieldName("MountTypeId")]
        public uint? MountTypeID;

        [DBFieldName("DisplayId")]
        public uint? DisplayID;

        [DBFieldName("Flags")]
        public uint? Flags;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("SourceDescription")]
        public string SourceDescription;

        [DBFieldName("Source")]
        public uint? Source;

        [DBFieldName("SpellId")]
        public uint? SpellID;

        [DBFieldName("PlayerConditionId")]
        public uint? PlayerConditionID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}

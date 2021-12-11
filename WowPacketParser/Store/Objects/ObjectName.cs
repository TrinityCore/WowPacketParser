using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("object_names")]
    public sealed record ObjectName : IDataModel
    {
        [DBFieldName("ObjectType", true)]
        public StoreNameType? ObjectType;

        [DBFieldName("Id", true)]
        public int? ID;

        [DBFieldName("Name")]
        public string Name;
    }
}

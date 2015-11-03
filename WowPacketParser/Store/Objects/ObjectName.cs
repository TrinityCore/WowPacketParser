using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("objectnames")]
    public sealed class ObjectName : IDataModel
    {
        [DBFieldName("ObjectType")]
        public ObjectType? ObjectType;

        [DBFieldName("Id")]
        public int? ID;

        [DBFieldName("Name")]
        public string Name;
    }
}

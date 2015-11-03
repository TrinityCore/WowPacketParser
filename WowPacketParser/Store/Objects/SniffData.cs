using System;
using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("sniffdata")]
    public sealed class SniffData : IDataModel
    {
        [DBFieldName("Build")]
        public int Build;

        [DBFieldName("SniffName")]
        public string SniffName;

        [DBFieldName("ObjectType")]
        public StoreNameType ObjectType;

        [DBFieldName("Id")]
        public int Id;

        [DBFieldName("Data")]
        public string Data;
    }
}

using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_template")]
    public class CreatureDifficulty
    {
        [DBFieldName("minlevel")]
        public int MinLevel;
        [DBFieldName("maxlevel")]
        public int MaxLevel;
        [DBFieldName("exp")]
        public int Expansion;
        [DBFieldName("faction")]
        public uint faction;

        //[DBFieldName("Unk 1")] public uint unk1;
        //[DBFieldName("Unk 2")] public uint unk2;
        //[DBFieldName("Unk 3")] public uint unk3;
        //[DBFieldName("Unk 4")] public uint unk4;
        //[DBFieldName("Unk 5")] public uint unk5;
    }
}

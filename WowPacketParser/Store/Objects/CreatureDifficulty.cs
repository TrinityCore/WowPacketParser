using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_template")]
    public class CreatureDifficulty
    {
        public uint faction;

        public void ConvertToDBStruct()
        {
            FactionA = faction;
            FactionH = faction;
        }

        [DBFieldName("minlevel")]
        public uint MinLevel;
        [DBFieldName("maxlevel")]
        public uint MaxLevel;
        [DBFieldName("exp")]
        public int Expansion;
        [DBFieldName("faction_A")]
        public uint FactionA;
        [DBFieldName("faction_H")]
        public uint FactionH;

        //[DBFieldName("Unk 1")] public uint unk1;
        //[DBFieldName("Unk 2")] public uint unk2;
        //[DBFieldName("Unk 3")] public uint unk3;
        //[DBFieldName("Unk 4")] public uint unk4;
        //[DBFieldName("Unk 5")] public uint unk5;
    }
}

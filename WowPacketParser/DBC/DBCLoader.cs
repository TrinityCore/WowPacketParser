using WowPacketParser.DBC.DBCStore;
using WowPacketParser.DBC.DBCStructures;

namespace WowPacketParser.DBC
{
    public class DBCLoader
    {
        public DBCLoader()
        {
            DBCStore.DBC.Spell = DBCReader.ReadDBC<SpellEntry>(DBC.DBCStore.DBC.SpellStrings);
        }
    }
}

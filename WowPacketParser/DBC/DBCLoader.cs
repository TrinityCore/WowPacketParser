using System;
using System.IO;
using WowPacketParser.DBC.DBCStore;
using WowPacketParser.DBC.DBCStructures;

namespace WowPacketParser.DBC
{
    public class DBCLoader
    {
        public DBCLoader()
        {
            try
            {
                DBCStore.DBC.Spell = DBCReader.ReadDBC<SpellEntry>(DBC.DBCStore.DBC.SpellStrings);
                DBCStore.DBC.Map = DBCReader.ReadDBC<MapEntry>(DBC.DBCStore.DBC.MapStrings);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

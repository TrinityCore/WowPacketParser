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
                DBCStore.DBC.Spell = DBCReader.ReadDBC<SpellEntry>(DBCStore.DBC.SpellStrings);
                DBCStore.DBC.Map = DBCReader.ReadDBC<MapEntry>(DBCStore.DBC.MapStrings);
                DBCStore.DBC.LFGDungeons = DBCReader.ReadDBC<LFGDungeonsEntry>(DBCStore.DBC.LFGDungeonsStrings);
                DBCStore.DBC.BattlemasterList = DBCReader.ReadDBC<BattlemasterListEntry>(DBCStore.DBC.BattlemasterListStrings);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                DBCStore.DBC.Enabled = false;
            }
        }
    }
}

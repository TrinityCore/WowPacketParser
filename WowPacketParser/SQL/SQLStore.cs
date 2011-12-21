using System.Collections.Generic;
using System.IO;
using WowPacketParser.SQL.Stores;

namespace WowPacketParser.SQL
{
    public class SQLStore
    {
        private StreamWriter _file;

        private readonly List<string> Sqls = new List<string>();

        public SQLStore(string file)
        {
            File.Delete(file);
            _file = new StreamWriter(file, true);
        }

        public void WriteData(string sql)
        {
            if (_file == null)
                return;

            if (Sqls.BinarySearch(sql) > -1)
                return;

            Sqls.Add(sql);
        }

        public void WriteToFile()
        {
            if (_file == null)
                return;

            Sqls.Sort();

            foreach (var sql in Sqls)
                _file.WriteLine(sql);

            Flush();
        }

        private void Flush()
        {
            _file.Flush();
            _file.Close();
            _file = null;
        }

        public readonly CreatureSpawnUpdateStore CreatureSpawnUpdates = new CreatureSpawnUpdateStore();

        public readonly CreatureUpdateStore CreatureUpdates = new CreatureUpdateStore();

        public readonly GameObjectSpawnStore GameObjectSpawns = new GameObjectSpawnStore();

        public readonly GameObjectSpawnUpdateStore GameObjectSpawnUpdates = new GameObjectSpawnUpdateStore();

        public readonly GameObjectUpdateStore GameObjectUpdates = new GameObjectUpdateStore();

        public readonly ItemStore Items = new ItemStore();
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using WowPacketParser.SQL.Stores;

namespace WowPacketParser.SQL
{
    public static class SQLStore
    {
        private static StreamWriter _file;

        private static readonly List<string> Sqls = new List<string>();

        public static void Initialize(string file, bool output)
        {
            if (output)
                return;

            File.Delete(file);
            _file = new StreamWriter(file, true);
        }

        public static void WriteData(string sql)
        {
            if (_file == null)
                return;

            if (Sqls.BinarySearch(sql) > -1)
                return;

            Sqls.Add(sql);
        }

        public static void WriteToFile()
        {
            if (_file == null)
                return;

            Sqls.Sort();

            foreach (var sql in Sqls)
                _file.WriteLine(sql);

            Flush();
        }

        private static void Flush()
        {
            _file.Flush();
            _file.Close();
            _file = null;
        }

        public static readonly CreatureStore Creatures = new CreatureStore();

        public static readonly CreatureSpawnStore CreatureSpawns = new CreatureSpawnStore();

        public static readonly CreatureSpawnUpdateStore CreatureSpawnUpdates = new CreatureSpawnUpdateStore();

        public static readonly CreatureUpdateStore CreatureUpdates = new CreatureUpdateStore();

        public static readonly GameObjectStore GameObjects = new GameObjectStore();

        public static readonly GameObjectSpawnStore GameObjectSpawns = new GameObjectSpawnStore();

        public static readonly GameObjectSpawnUpdateStore GameObjectSpawnUpdates = new GameObjectSpawnUpdateStore();

        public static readonly GameObjectUpdateStore GameObjectUpdates = new GameObjectUpdateStore();

        public static readonly ItemStore Items = new ItemStore();

        public static readonly NpcTextStore NpcTexts = new NpcTextStore();

        public static readonly PageTextStore PageTexts = new PageTextStore();

        public static readonly QuestPoiPointStore QuestPoiPoints = new QuestPoiPointStore();

        public static readonly QuestPoiStore QuestPois = new QuestPoiStore();

        public static readonly QuestStore Quests = new QuestStore();

        public static readonly StartActionStore StartActions = new StartActionStore();

        public static readonly StartPositionStore StartPositions = new StartPositionStore();

        public static readonly StartSpellStore StartSpells = new StartSpellStore();

        public static readonly TrainerSpellStore TrainerSpells = new TrainerSpellStore();

        public static readonly VendorItemStore VendorItems = new VendorItemStore();
    }
}

using System.Collections.Generic;
using System.Linq;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using System;
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
            if (!output)
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

        /// <summary>
        /// Since this function must do a lookup to all of its elements, it needs to be written when all of them are already parsed/loaded into the ConcurrentDictionary
        /// </summary>
        public static void WriteGossipMenus()
        {
            foreach (KeyValuePair<Tuple<uint, uint>, GossipMenu> kvp in Stuffing.Gossips)
            {
                _file.WriteLine("DELETE FROM gossip_menu WHERE entry = {0} AND text_id = {1};", kvp.Key.Item2, kvp.Value.NpcTextId);
                _file.WriteLine("INSERT INTO gossip_menu(entry,text_id) VAlUES ({0},{1});",kvp.Key.Item2,kvp.Value.NpcTextId);

                foreach (GossipOption go in kvp.Value.GossipOptions)
                {
                    _file.WriteLine("DELETE FROM gossip_menu_option WHERE menu_id = {0} AND id = {1};", kvp.Key.Item2, go.Index);
                    _file.WriteLine("INSERT INTO gossip_menu_option(menu_id, id, option_icon, option_text, option_id, npc_option_npcflag, action_menu_id, box_coded, box_money, box_text) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}');", kvp.Key.Item2, go.Index, go.OptionIcon, go.OptionText, 1, 1, (Stuffing.Gossips.Count(k => k.Key.Item1 == kvp.Key.Item1 && k.Key.Item2 == kvp.Key.Item2 + 1) == 1 && kvp.Value.GossipOptions.Count == 1) ? Stuffing.Gossips.Where(k => k.Key.Item1 == kvp.Key.Item1 && k.Key.Item2 == kvp.Key.Item2 + 1).First().Key.Item2 : 0, go.Box ? 1 : 0, go.RequiredMoney, go.BoxText);
                }
            }
        }

        public static void WriteToFile()
        {
            if (_file == null)
                return;

            Sqls.Sort();

            foreach (var sql in Sqls)
                _file.WriteLine(sql);
            
            WriteGossipMenus();

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

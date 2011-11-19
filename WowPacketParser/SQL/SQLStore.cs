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
            String deletemenus = String.Empty;
            String insertmenus = String.Empty;
            String deleteoptions = String.Empty;
            String insertoptions = String.Empty;

            if (Stuffing.Gossips.Count > 0)
            {
                deletemenus = "DELETE FROM gossip_menu WHERE entry IN (";
                insertmenus = "INSERT INTO gossip_menu(entry,text_id) VALUES " + Environment.NewLine;
                deleteoptions = "DELETE FROM gossip_menu_option WHERE menu_id IN (";
                insertoptions = "INSERT INTO gossip_menu_option(menu_id, id, option_icon, option_text, option_id, npc_option_npcflag, action_menu_id, box_coded, box_money, box_text) VALUES " + Environment.NewLine;
            }

            foreach (KeyValuePair<Tuple<uint, uint>, GossipMenu> kvp in Stuffing.Gossips)
            {
                deletemenus += kvp.Key.Item2 + ",";
                insertmenus += String.Format("({0},{1}),",kvp.Key.Item2,kvp.Value.NpcTextId) + Environment.NewLine;
                deleteoptions += kvp.Key.Item2 + ",";

                foreach (GossipOption go in kvp.Value.GossipOptions)
                {
                    insertoptions += String.Format("('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}'),", kvp.Key.Item2, go.Index, go.OptionIcon, go.OptionText, 1, 1, (Stuffing.Gossips.Count(k => k.Key.Item1 == kvp.Key.Item1 && k.Key.Item2 == kvp.Key.Item2 + 1) == 1 && kvp.Value.GossipOptions.Count == 1) ? Stuffing.Gossips.Where(k => k.Key.Item1 == kvp.Key.Item1 && k.Key.Item2 == kvp.Key.Item2 + 1).First().Key.Item2 : 0, go.Box ? 1 : 0, go.RequiredMoney, go.BoxText) + Environment.NewLine;
                }
            }

            deletemenus = deletemenus.Substring(0,deletemenus.Length - 1); // Remove extra comma
            deletemenus += ");";

            insertmenus = insertmenus.Substring(0,insertmenus.Length - 3); // Remove extra comma
            insertmenus += ";";

            deleteoptions = deleteoptions.Substring(0,deleteoptions.Length - 1); // Remove extra comma
            deleteoptions += ");";

            insertoptions = insertoptions.Substring(0, insertoptions.Length - 3); // Remove extra comma
            insertoptions += ";";

            _file.WriteLine(deletemenus);
            _file.WriteLine(deleteoptions);
            _file.WriteLine(insertmenus);
            _file.WriteLine(insertoptions);
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using Wintellect.PowerCollections;

namespace WowPacketParser.Store
{
    public abstract class Store
    {
        public static SQLOutputFlags Flags { protected get; set; }
        public string Type { get; protected set; }

        protected static bool ProcessFlags(string type, SQLOutputFlags flags)
        {
            switch (type)
            {
                case "Objects":
                    return flags.HasAnyFlag(SQLOutputFlags.CreatureSpawns) ||
                           flags.HasAnyFlag(SQLOutputFlags.GameObjectSpawns) ||
                           flags.HasAnyFlag(SQLOutputFlags.CreatureMovement) ||
                           flags.HasAnyFlag(SQLOutputFlags.CreatureEquip) ||
                           flags.HasAnyFlag(SQLOutputFlags.StartInformation);
                case "GameObjectTemplates":
                    return flags.HasAnyFlag(SQLOutputFlags.GameObjectTemplate);
                case "ItemTemplates":
                    return false; //return flags.HasAnyFlag(SQLOutputFlags.ItemTemplate;
                case "QuestTemplates":
                case "QuestOffers":
                case "QuestRewards":
                    return flags.HasAnyFlag(SQLOutputFlags.QuestTemplate);
                case "Gossips":
                    return flags.HasAnyFlag(SQLOutputFlags.Gossip);
                case "Loots":
                    return flags.HasAnyFlag(SQLOutputFlags.Loot);
                case "UnitTemplates":
                case "SpellsX":
                case "CreatureTexts":
                case "Emotes":
                case "Sounds":
                    return flags.HasAnyFlag(SQLOutputFlags.CreatureTemplate);
                case "NpcTrainers":
                    return flags.HasAnyFlag(SQLOutputFlags.NpcTrainer);
                case "NpcVendors":
                    return flags.HasAnyFlag(SQLOutputFlags.NpcVendor);
                case "PageTexts":
                    return flags.HasAnyFlag(SQLOutputFlags.PageText);
                case "NpcTexts":
                    return flags.HasAnyFlag(SQLOutputFlags.NpcText);
                case "StartActions":
                case "StartSpells":
                case "StartPositions":
                    return flags.HasAnyFlag(SQLOutputFlags.StartInformation);
                case "QuestPOIs":
                    return flags.HasAnyFlag(SQLOutputFlags.QuestPOI);
                case "ObjectNames":
                    return flags.HasAnyFlag(SQLOutputFlags.ObjectNames);
                case "SniffData":
                    return flags.HasAnyFlag(SQLOutputFlags.SniffData) ||
                           flags.HasAnyFlag(SQLOutputFlags.SniffDataOpcodes);
                default:
                    throw new ArgumentException("type is not assigned to any SQLOutput flag", "type");
            }
        }

        public abstract void Clear();
        public abstract bool IsEmpty();

        protected bool Enabled;
    }

    public class StoreDictionary<T, TK> : Store, IEnumerable<KeyValuePair<T, Tuple<TK, TimeSpan?>>>
    {
        private readonly Dictionary<T, Tuple<TK, TimeSpan?>> _dictionary;

        public StoreDictionary()
        {
            Type = "None";
            Enabled = true;
            _dictionary = new Dictionary<T, Tuple<TK, TimeSpan?>>();
        }

        public StoreDictionary(string type)
        {
            Type = type;
            Enabled = ProcessFlags(Type, Flags);
            _dictionary = Enabled ? new Dictionary<T, Tuple<TK, TimeSpan?>>() : null;
        }

        public StoreDictionary(Dictionary<T, TK> dict)
        {
            _dictionary = new Dictionary<T, Tuple<TK, TimeSpan?>>();

            foreach (var pair in dict)
                _dictionary.Add(pair.Key, new Tuple<TK, TimeSpan?>(pair.Value, null));

            Type = string.Empty;
            Enabled = true;
        }

        public void Add(T key, TK value, TimeSpan? time)
        {
            if (!Enabled)
                return;

            if (_dictionary.ContainsKey(key))
                return;

            _dictionary.Add(key, new Tuple<TK, TimeSpan?>(value, time));
        }

        public bool ContainsKey(T key)
        {
            if (Enabled)
                return _dictionary.ContainsKey(key);
            return false;
        }

        public bool TryGetValue(T key, out TK value)
        {
            if (Enabled)
            {
                Tuple<TK, TimeSpan?> tuple;
                if (_dictionary.TryGetValue(key, out tuple))
                {
                    value = tuple.Item1;
                    return true;
                }
            }

            value = default(TK);
            return false;
        }

        public bool TryGetValue(T key, out Tuple<TK, TimeSpan?> value)
        {
            if (Enabled)
                return _dictionary.TryGetValue(key, out value);
            value = null;
            return false;
        }

        public Tuple<TK, TimeSpan?> this[T key]
        {
            get
            {
                if (Enabled)
                    return _dictionary[key];
                return null;
            }

            set
            {
                if (Enabled)
                    _dictionary[key] = value;
            }
        }

        public override void Clear()
        {
            if (Enabled)
                _dictionary.Clear();
        }

        public override bool IsEmpty()
        {
            if (Enabled)
                return _dictionary.Count == 0;
            return true;
        }

        public IEnumerator<KeyValuePair<T, Tuple<TK, TimeSpan?>>> GetEnumerator()
        {
            if (Enabled)
                return _dictionary.GetEnumerator();
            return null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public List<T> Keys()
        {
            if (Enabled)
                return _dictionary.Keys.ToList();
            return null;
        }
    }

    public class StoreMulti<T, TK> : Store, IEnumerable<KeyValuePair<T, ICollection<Tuple<TK, TimeSpan?>>>>
    {
        private readonly MultiDictionary<T, Tuple<TK, TimeSpan?>> _dictionary;

        public StoreMulti()
        {
            Type = "None";
            Enabled = true;
            _dictionary = new MultiDictionary<T, Tuple<TK, TimeSpan?>>(true);
        }

        public StoreMulti(string type)
        {
            Type = type;
            Enabled = ProcessFlags(Type, Flags);
            _dictionary = Enabled ? new MultiDictionary<T, Tuple<TK, TimeSpan?>>(true) : null;
        }

        public StoreMulti(MultiDictionary<T, TK> dict)
        {
            _dictionary = new MultiDictionary<T, Tuple<TK, TimeSpan?>>(true);

            foreach (var pair in dict)
                foreach (var k in pair.Value)
                    _dictionary.Add(pair.Key, new Tuple<TK, TimeSpan?>(k, null));


            Type = string.Empty;
            Enabled = true;
        }

        public void Add(T key, TK value, TimeSpan? time)
        {
            if (!Enabled)
                return;

            _dictionary.Add(key, new Tuple<TK, TimeSpan?>(value, time));
        }

        public override void Clear()
        {
            if (Enabled)
                _dictionary.Clear();
        }

        public override bool IsEmpty()
        {
            if (Enabled)
                return _dictionary.Count == 0;
            return true;
        }

        public IEnumerator<KeyValuePair<T, ICollection<Tuple<TK, TimeSpan?>>>> GetEnumerator()
        {
            if (Enabled)
                return _dictionary.GetEnumerator();
            return null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public List<T> Keys()
        {
            if (Enabled)
                return _dictionary.Keys.ToList();
            return null;
        }

        public bool ContainsKey(T key)
        {
            if (Enabled)
                return _dictionary.ContainsKey(key);
            return false;
        }

        public ICollection<Tuple<TK, TimeSpan?>> this[T key]
        {
            get
            {
                if (Enabled)
                    return _dictionary[key];
                return null;
            }

            set
            {
                if (Enabled)
                    _dictionary[key] = value;
            }
        }
    }

    public class StoreBag<T> : Store, IEnumerable<Tuple<T, TimeSpan?>>
    {
        private readonly Bag<Tuple<T, TimeSpan?>> _bag;

        public StoreBag(string type)
        {
            Type = type;
            Enabled = ProcessFlags(Type, Flags);
            _bag = Enabled ? new Bag<Tuple<T, TimeSpan?>>() : null;
        }

        public void Add(T item, TimeSpan? time)
        {
            if (Enabled)
                _bag.Add(new Tuple<T, TimeSpan?>(item, time));
        }

        public override void Clear()
        {
            if (Enabled)
                _bag.Clear();
        }

        public override bool IsEmpty()
        {
            if (Enabled)
                return _bag.Count == 0;
            return true;
        }

        public IEnumerator<Tuple<T, TimeSpan?>> GetEnumerator()
        {
            if (Enabled)
                return _bag.GetEnumerator();
            return null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

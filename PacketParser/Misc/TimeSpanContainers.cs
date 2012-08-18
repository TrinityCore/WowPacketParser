using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PacketParser.Enums;
using PacketParser.Misc;
using Wintellect.PowerCollections;

namespace PacketParser.Misc
{
    public abstract class TimeSpanContainer
    {
        public abstract void Clear();
        public abstract bool IsEmpty();
    }

    public class TimeSpanDictionary<T, TK> : TimeSpanContainer, IEnumerable<KeyValuePair<T, Tuple<TK, TimeSpan?>>>
    {
        private readonly Dictionary<T, Tuple<TK, TimeSpan?>> _dictionary;

        public TimeSpanDictionary()
        {
            _dictionary = new Dictionary<T, Tuple<TK, TimeSpan?>>();
        }

        public TimeSpanDictionary(Dictionary<T, TK> dict)
        {
            _dictionary = new Dictionary<T, Tuple<TK, TimeSpan?>>();

            foreach (var pair in dict)
            {
                _dictionary.Add(pair.Key, new Tuple<TK, TimeSpan?>(pair.Value, null));
            }
        }

        public void Add(T key, TK value, TimeSpan? time)
        {
            if (_dictionary.ContainsKey(key))
                return;

            _dictionary.Add(key, new Tuple<TK, TimeSpan?>(value, time));
        }

        public bool ContainsKey(T key)
        {
            return _dictionary.ContainsKey(key);
        }

        public bool TryGetValue(T key, out TK value)
        {
            Tuple<TK, TimeSpan?> tuple;
            if (_dictionary.TryGetValue(key, out tuple))
            {
                value = tuple.Item1;
                return true;
            }
            value = default(TK);
            return false;
        }

        public bool TryGetValue(T key, out Tuple<TK, TimeSpan?> value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        public Tuple <TK, TimeSpan?> this[T key]
        {
            get
            {
                return _dictionary[key];
            }

            set
            {
                _dictionary[key] = value;
            }
        }

        public override void Clear()
        {
            _dictionary.Clear();
        }

        public override bool IsEmpty()
        {
            return _dictionary.Count == 0;
        }

        public IEnumerator<KeyValuePair<T, Tuple<TK, TimeSpan?>>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public List<T> Keys()
        {
            return _dictionary.Keys.ToList();
        }
    }

    public class TimeSpanMultiDictionary<T, TK> : TimeSpanContainer, IEnumerable<KeyValuePair<T, ICollection<Tuple<TK, TimeSpan?>>>>
    {
        private readonly MultiDictionary<T, Tuple<TK, TimeSpan?>> _dictionary;

        public TimeSpanMultiDictionary()
        {
            _dictionary = new MultiDictionary<T, Tuple<TK, TimeSpan?>>(true);
        }

        public TimeSpanMultiDictionary(MultiDictionary<T, TK> dict)
        {
            _dictionary = new MultiDictionary<T, Tuple<TK, TimeSpan?>>(true);

            foreach (var pair in dict)
                foreach (var k in pair.Value)
                    _dictionary.Add(pair.Key, new Tuple<TK, TimeSpan?>(k, null));
        }

        public void Add(T key, TK value, TimeSpan? time)
        {
            _dictionary.Add(key, new Tuple<TK, TimeSpan?>(value, time));
        }

        public override void Clear()
        {
            _dictionary.Clear();
        }

        public override bool IsEmpty()
        {
            return _dictionary.Count == 0;
        }

        public IEnumerator<KeyValuePair<T, ICollection<Tuple<TK, TimeSpan?>>>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public List<T> Keys()
        {
            return _dictionary.Keys.ToList();
        }

        public bool ContainsKey(T key)
        {
            return _dictionary.ContainsKey(key);
        }

        public ICollection<Tuple<TK, TimeSpan?>> this[T key]
        {
            get
            {
                return _dictionary[key];
            }

            set
            {
                _dictionary[key] = value;
            }
        }
    }

    public class TimeSpanBag<T> : TimeSpanContainer, IEnumerable<Tuple<T, TimeSpan?>>
    {
        private readonly Bag<Tuple<T, TimeSpan?>> _bag;

        public TimeSpanBag()
        {
            _bag = new Bag<Tuple<T, TimeSpan?>>();
        }

        public void Add(T item, TimeSpan? time)
        {
            _bag.Add(new Tuple<T, TimeSpan?>(item, time));
        }

        public override void Clear()
        {
            _bag.Clear();
        }

        public override bool IsEmpty()
        {
            return _bag.Count == 0;
        }

        public IEnumerator<Tuple<T, TimeSpan?>> GetEnumerator()
        {
            return _bag.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

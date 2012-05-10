using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace WowPacketParser.Misc
{
    // By Jon Skeet (Stack Overflow), modified
    public class BiDictionary<TFirst, TSecond> : IDictionary<TFirst, TSecond>
    {
        readonly IDictionary<TFirst, TSecond> _firstToSecond = new Dictionary<TFirst, TSecond>();
        readonly IDictionary<TSecond, TFirst> _secondToFirst = new Dictionary<TSecond, TFirst>();

        public bool ContainsKey(TFirst key)
        {
            return _firstToSecond.ContainsKey(key);
        }

        public bool ContainsValue(TSecond value)
        {
            return _secondToFirst.ContainsKey(value);
        }

        public bool ContainsKey(TSecond key)
        {
            return _secondToFirst.ContainsKey(key);
        }

        public bool ContainsValue(TFirst value)
        {
            return _firstToSecond.ContainsKey(value);
        }

        public void Add(TFirst first, TSecond second)
        {
            TFirst frst;
            TSecond snd;

            if (_firstToSecond.TryGetValue(first, out snd))
            {
                Trace.WriteLine(string.Format("BiDictionary already contains <{0}, {1}>", second, first));
                return;
            }

            if (_secondToFirst.TryGetValue(second, out frst))
            {
                Trace.WriteLine(string.Format("BiDictionary already contains <{0}, {1}>", first, second));
                return;
            }

            _firstToSecond.Add(first, second);
            _secondToFirst.Add(second, first);
        }

        public void Add(KeyValuePair<TFirst, TSecond> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(KeyValuePair<TSecond,  TFirst> item)
        {
            Add(item.Value, item.Key);
        }

        public bool Remove(TFirst key)
        {
            if (_firstToSecond.Remove(key))
            {
                foreach (var pair in _secondToFirst.Where(pair => Equals(pair.Value, key)))
                {
                    _secondToFirst.Remove(pair.Key);
                    return true;
                }
            }

            return false;
        }

        public bool Remove(TSecond key)
        {
            if (_secondToFirst.Remove(key))
            {
                foreach (var pair in _firstToSecond.Where(pair => Equals(pair.Value, key)))
                {
                    _firstToSecond.Remove(pair.Key);
                    return true;
                }
            }

            return false;
        }

        public bool TryGetValue(TFirst key, out TSecond value)
        {
            return TryGetByFirst(key, out value);
        }

        public bool TryGetValue(TSecond key, out TFirst value)
        {
            return TryGetBySecond(key, out value);
        }

        TSecond IDictionary<TFirst, TSecond>.this[TFirst key]
        {
            get { return GetByFirst(key); }
            set { _firstToSecond[key] = value; }
        }

        public ICollection<TFirst> Keys
        {
            get { return _firstToSecond.Keys; }
        }

        public ICollection<TSecond> Values
        {
            get { return _firstToSecond.Values; }
        }

        public bool Remove(KeyValuePair<TFirst, TSecond> item)
        {
            var invertedPair = new KeyValuePair<TSecond, TFirst>(item.Value, item.Key);

            return _firstToSecond.Remove(item) && _secondToFirst.Remove(invertedPair);
        }

        public bool Remove(KeyValuePair<TSecond, TFirst> item)
        {
            var invertedPair = new KeyValuePair<TFirst, TSecond>(item.Value, item.Key);

            return _secondToFirst.Remove(item) && _firstToSecond.Remove(invertedPair);
        }

        public int Count
        {
            get { return _firstToSecond.Count; }
        }

        public bool IsReadOnly
        {
            get { return _firstToSecond.IsReadOnly; }
        }

        public void Clear()
        {
            _firstToSecond.Clear();
            _secondToFirst.Clear();
        }

        public bool Contains(KeyValuePair<TFirst, TSecond> item)
        {
            return _firstToSecond.Contains(item);
        }

        public bool Contains(KeyValuePair<TSecond, TFirst> item)
        {
            return _secondToFirst.Contains(item);
        }

        public void CopyTo(KeyValuePair<TFirst, TSecond>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public TSecond this[TFirst first]
        {
            get { return GetByFirst(first); }
            set { SetByFirst(first, value); }
        }

        public TFirst this[TSecond second]
        {
            get { return GetBySecond(second); }
            set { SetBySecond(second, value); }
        }

        public bool TryGetByFirst(TFirst first, out TSecond second)
        {
            return _firstToSecond.TryGetValue(first, out second);
        }

        public bool TryGetBySecond(TSecond second, out TFirst first)
        {
            return _secondToFirst.TryGetValue(second, out first);
        }

        public TSecond GetByFirst(TFirst first)
        {
            TSecond value;
            if (_firstToSecond.TryGetValue(first, out value))
                return value;

            return default(TSecond);
        }

        public TFirst GetBySecond(TSecond second)
        {
            TFirst value;
            if (_secondToFirst.TryGetValue(second, out value))
                return value;

            return default(TFirst);
        }

        public void SetByFirst(TFirst key, TSecond value)
        {
            var oldVal = _firstToSecond[key];

            if (!typeof(TSecond).IsValueType && oldVal == null)
                return;

            _firstToSecond[key] = value;

            if (_secondToFirst.Remove(oldVal))
                _secondToFirst.Add(value, key);
        }

        public void SetBySecond(TSecond key, TFirst value)
        {
            SetByFirst(value, key);
        }

        public IEnumerator<KeyValuePair<TFirst, TSecond>> GetEnumerator()
        {
            return _firstToSecond.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

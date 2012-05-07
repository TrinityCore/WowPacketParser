using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace WowPacketParser.Misc
{
    // By Jon Skeet (Stack Overflow), modified
    public class BiDictionary<TFirst, TSecond> : IEnumerable<KeyValuePair<TFirst, TSecond>>
    {
        readonly IDictionary<TFirst, TSecond> _firstToSecond = new Dictionary<TFirst, TSecond>();
        readonly IDictionary<TSecond, TFirst> _secondToFirst = new Dictionary<TSecond, TFirst>();

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

        public int Count
        {
            get { return _firstToSecond.Count; }
        }

        public void Clear()
        {
            _firstToSecond.Clear();
            _secondToFirst.Clear();
        }

        // Note potential ambiguity using indexers (e.g. mapping from int to int)
        // Hence the methods as well...
        public TSecond this[TFirst first]
        {
            get { return GetByFirst(first); }
        }

        public TFirst this[TSecond second]
        {
            get { return GetBySecond(second); }
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

using System;
using System.Collections;
using System.Collections.Generic;

namespace WowPacketParser.Misc
{
    // By Jon Skeet (Stack Overflow), modified
    class BiDictionary<TFirst, TSecond> : IEnumerable<KeyValuePair<TFirst, TSecond>>
    {
        readonly IDictionary<TFirst, TSecond> _firstToSecond = new Dictionary<TFirst, TSecond>();
        readonly IDictionary<TSecond, TFirst> _secondToFirst = new Dictionary<TSecond, TFirst>();

        private static readonly TFirst EmptyFirstValue = default(TFirst);
        private static readonly TSecond EmptySecondValue = default(TSecond);

        public void Add(TFirst first, TSecond second)
        {
            TFirst frst;
            TSecond snd;

            if (_firstToSecond.TryGetValue(first, out snd))
            {
                Console.WriteLine("BiDictionary already contains <{0},{1}>", second, first);
                return;
            }

            if (_secondToFirst.TryGetValue(second, out frst))
            {
                Console.WriteLine("BiDictionary already contains <{0},{1}>", first, second);
                return;
            }

            _firstToSecond.Add(first, second);
            _secondToFirst.Add(second, first);
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

        public TSecond GetByFirst(TFirst first)
        {
            TSecond value;
            return !_firstToSecond.TryGetValue(first, out value) ? EmptySecondValue : value;
        }

        public TFirst GetBySecond(TSecond second)
        {
            TFirst value;
            return !_secondToFirst.TryGetValue(second, out value) ? EmptyFirstValue : value;
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

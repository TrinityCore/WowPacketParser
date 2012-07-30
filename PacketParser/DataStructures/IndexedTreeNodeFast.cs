using System;
using System.Collections.Generic;
using System.Collections;

namespace PacketParser.DataStructures
{
    public class IndexedTreeNodeFast : IDictionary<int, NamedTreeNode>, IndexedTreeNode
    {
        public IndexedTreeNodeFast()
        {
            Count = 0;
            _list = new List<NamedTreeNode>();
        }

        public IndexedTreeNodeFast(int capacity)
        {
            Count = 0;
            _list = new List<NamedTreeNode>(capacity);
        }

        private List<NamedTreeNode> _list;

        public ICollection<int> Keys
        {
            get
            {
                int count = _list.Count;
                List<int> keys = new List<int>(count);
                for (int i = 0; i < count; ++i)
                {
                    if (_list[i] != null)
                        keys.Add(i);
                }
                return keys;
            }
        }
        public int Count { get; private set; }
        public bool IsReadOnly { get { return false; } }
        public ICollection<NamedTreeNode> Values
        {
            get
            {
                int count = _list.Count;
                List<NamedTreeNode> vals = new List<NamedTreeNode>(count);
                for (int i = 0; i < count; ++i)
                {
                    if (_list[i] != null)
                        vals.Add(_list[i]);
                }
                return vals;
            }
        }

        public NamedTreeNode this[int key]
        {
            get
            {
                if (key >= _list.Count || _list[key] == null)
                    throw new System.Collections.Generic.KeyNotFoundException();
                return _list[key];
            }
            set
            {
                Add(key, value);
            }
        }
        public void CopyTo(KeyValuePair<int, NamedTreeNode>[] array, int arrayIndex)
        {
            if (array == null)
                throw new System.ArgumentNullException();
            if (arrayIndex < 0 || ((array.Length - arrayIndex) < Count))
                throw new System.ArgumentOutOfRangeException();

            int count = _list.Count;
            int insertPos = arrayIndex;
            List<NamedTreeNode> vals = new List<NamedTreeNode>(count);
            for (int i = 0; i < count; ++i)
            {
                if (_list[i] != null)
                {
                    array[insertPos] = new KeyValuePair<int, NamedTreeNode>(i, _list[i]);
                    insertPos++;
                }
            }
        }
        public void Add(int key, NamedTreeNode value)
        {
            if (key >= _list.Count - 1)
            {
                _list.AddRange(new NamedTreeNode[key - (_list.Count - 1) ]);
            }
            if (_list[key] == null)
            {
                Count++;
            }
            _list[key] = value;
        }
        public void Add(KeyValuePair<int, NamedTreeNode> pair)
        {
            Add(pair.Key, pair.Value);
        }
        public bool ContainsKey(int key)
        {
            if (key >= _list.Count)
                return false;
            return _list[key] != null;
        }
        public bool Contains(KeyValuePair<int, NamedTreeNode> pair)
        {
            if (pair.Key >= _list.Count)
                return false;
            return _list[pair.Key] == pair.Value;
        }
        public bool Remove(int key)
        {
            if (key >= _list.Count)
                return false;
            if (_list[key] == null)
                return false;
            _list[key] = null;
            Count--;
            return false;
        }
        public bool Remove(KeyValuePair<int, NamedTreeNode> pair)
        {
            if (Contains(pair))
                return Remove(pair.Key);
            return false;
        }
        public bool TryGetValue(int key, out NamedTreeNode value)
        {
            if (key >= _list.Count || _list[key] == null)
            {
                value = null;
                return false;
            }
            value = _list[key];
            return true;
        }
        public void Clear()
        {
            _list.Clear();
            Count = 0;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        IEnumerator<KeyValuePair<int, NamedTreeNode>> IEnumerable<KeyValuePair<int, NamedTreeNode>>.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public class Enumerator : IEnumerator<KeyValuePair<int, NamedTreeNode>>, IDisposable, IDictionaryEnumerator, IEnumerator
        {
            private int _key;
            private int _count;
            private IndexedTreeNodeFast _node;
            public Enumerator(IndexedTreeNodeFast node)
            {
                _node = node;
                Reset();
            }
            public KeyValuePair<int, NamedTreeNode> Current
            {
                get
                {
                    return new KeyValuePair<int,NamedTreeNode>(_key, _node[_key]);
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            object IDictionaryEnumerator.Key
            {
                get { return Current.Key; }
            }

            public DictionaryEntry Entry
            {
                get { return new DictionaryEntry(Current.Key, Current.Value); }
            }

            object IDictionaryEnumerator.Value
            {
                get { return Current.Value; }
            }

            public bool MoveNext()
            {
                do
                {
                    ++_key;
                    if (_key >= _count)
                        return false;
                    if (_node.ContainsKey(_key))
                        return true;
                }
                while (true);
            }
            public void Reset()
            {
                _key = -1;
                _count = _node.Count;
            }

            public void Dispose()
            {
                _node = null;
            }
        }

        public TreeNodeEnumerator GetTreeEnumerator()
        {
            return new TreeNodeEnumerator(this);
        }
        public NodeType GetNode<NodeType>(params string[] address)
        {
            NodeType ret;
            if (TryGetNode<NodeType>(out ret, address))
                return ret;
            throw new Exception(String.Format("Could not receive object of type {0} from address{1}", typeof(NodeType), address));
        }
        public bool TryGetNode<NodeType>(out NodeType ret, params string[] address)
        {
            return TryGetNode<NodeType>(out ret, address, 0);
        }
        public bool TryGetNode<NodeType>(out NodeType ret, string[] address, int addrIndex)
        {
            if (address.Length == addrIndex)
            {
                try
                {
                    ret = (NodeType)((Object)this);
                    return true;
                }
                catch
                {
                    ret = default(NodeType);
                    return false;
                }
            }
            NamedTreeNode node;
            if (this.TryGetValue(Int32.Parse(address[addrIndex]), out node))
                return node.TryGetNode<NodeType>(out ret, address, addrIndex + 1);
            ret = default(NodeType);
            return false;
        }
    }
}
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace PacketParser.DataStructures
{
    public class TreeNodeEnumerator : IEnumerator
    {
        public class EnumeratedNode
        {
            public Type type;
            public Object obj;
            public string name;
            public EnumeratedNode(Type _type, Object _obj, string _name)
            {
                type= _type;
                obj= _obj;
                name = _name;
            }
        }
        public class EnumeratedNamedNode : EnumeratedNode
        {
            public IDictionaryEnumerator iter;
            public EnumeratedNamedNode(Type _type, Object _obj, string _name, IDictionaryEnumerator _iter)
                : base(_type, _obj, _name)
            {
                iter = _iter;
            }
        }
        public class EnumeratedIndexedNode : EnumeratedNode
        {
            public IEnumerator<KeyValuePair<int, NamedTreeNode>> iter;
            public EnumeratedIndexedNode(Type _type, Object _obj, string _name, IEnumerator<KeyValuePair<int, NamedTreeNode>> _iter)
                : base(_type, _obj, _name)
            {
                iter = _iter;
            }
        }
        private readonly Object _initial;
        public readonly Stack<EnumeratedNode> CurrentNodes = new Stack<EnumeratedNode>();
        public readonly List<EnumeratedNode> CurrentClosedNodes = new List<EnumeratedNode>(); 

        private bool _started;
        private object _current;
        public object Current
        {
            get
            {
                return _current;
            }

            private set
            {
                _current = value;
                if (_current != null)
                    Type = _current.GetType();
                else
                    Type = typeof(Object);
            }
        }
        public Type Type { get; private set; }
        public string Name { get; private set; }
        public int? Index { get; private set; }

        public int CurrentDepth { get { return CurrentNodes.Count; } }
        
        public TreeNodeEnumerator(NamedTreeNode node)
        {
            _initial = node;
            Reset();
        }

        public TreeNodeEnumerator(Packet packet)
        {
            _initial = packet;
            Reset();
        }

        public TreeNodeEnumerator(IndexedTreeNode node)
        {
            _initial = node;
            Reset();
        }

        public void Reset()
        {
            CurrentNodes.Clear();
            CurrentClosedNodes.Clear();
            Current = null;
            Name = null;
            _started = false;
            Index = null;
        }

        public bool MoveOver()
        {
            return MoveNext(true);
        }
        public bool MoveNext()
        {
            return MoveNext(false);
        }
        private bool MoveNext(bool dontGoDeeper)
        {
            // handle beginning of iteration
            if (!_started)
            {
                Current = _initial;
                Name = null;
                Index = null;
                _started = true;
                return true;
            }
            CurrentClosedNodes.Clear();
            if (!dontGoDeeper)
            {
                // maybe we need to go deeper
                if (Type == typeof(NamedTreeNode))
                {
                    var itr = ((NamedTreeNode)Current).GetEnumerator();
                    // advance itr so we get to the element directly
                    if (itr.MoveNext())
                    {
                        CurrentNodes.Push(new EnumeratedNamedNode(Type, Current, Name, itr));
                        var entry = itr.Entry;
                        Current = entry.Value;
                        Name = (string)entry.Key;
                        Index = null;
                        return true;
                    }
                    else if (((NamedTreeNode)Current).Count == 0)
                    {
                        CurrentClosedNodes.Add(new EnumeratedNamedNode(Type, Current, Name, itr));
                    }
                    // fall threw, we had empty list and couldn't get inside, let's go to the next el of parent iterator
                }
                else if (Type == typeof(Packet))
                {
                    var itr = ((Packet)Current).GetData().GetEnumerator();
                    // advance itr so we get to the element directly
                    if (itr.MoveNext())
                    {
                        CurrentNodes.Push(new EnumeratedNamedNode(Type, Current, Name, itr));
                        var entry = itr.Entry;
                        Current = entry.Value;
                        Name = (string)entry.Key;
                        Index = null;
                        return true;
                    }
                    else if (((Packet)Current).GetData().Count == 0)
                    {
                        CurrentClosedNodes.Add(new EnumeratedNamedNode(Type, Current, Name, itr));
                    }
                    // fall threw, we had empty list and couldn't get inside, let's go to the next el of parent iterator
                }
                else if (Type == typeof(IndexedTreeNode))
                {
                    var itr = ((IndexedTreeNode)Current).GetEnumerator();
                    // advance itr so we get to the element directly
                    if (itr.MoveNext())
                    {
                        CurrentNodes.Push(new EnumeratedIndexedNode(Type, Current, Name, itr));
                        Current = itr.Current.Value;
                        Index = itr.Current.Key;
                        return true;
                    }
                    else if (((IndexedTreeNode)Current).Count == 0)
                    {
                        CurrentClosedNodes.Add(new EnumeratedIndexedNode(Type, Current, Name, itr));
                    }
                    // fall threw, we had empty list and couldn't get inside, let's go to the next el of parent iterator
                }
            }

            while(CurrentNodes.Count > 0)
            {
                var top = CurrentNodes.Peek();
                // we don't need to go deeper, let's go nearby then
                if (top.type == typeof(NamedTreeNode) || top.type == typeof(Packet))
                {
                    var itr = ((EnumeratedNamedNode)top).iter;
                    if (itr.MoveNext())
                    {
                        var entry = itr.Entry;
                        Current = entry.Value;
                        Name = (string)entry.Key;
                        Index = null;
                        return true;
                    }
                }
                else if (top.type == typeof(IndexedTreeNode))
                {
                    var itr = ((EnumeratedIndexedNode)top).iter;
                    if (itr.MoveNext())
                    {
                        Current = itr.Current.Value;
                        Index = itr.Current.Key;
                        Name = top.name;
                        return true;
                    }
                }
                CurrentClosedNodes.Add(CurrentNodes.Pop());
            }
            return false;
        }
    }
}

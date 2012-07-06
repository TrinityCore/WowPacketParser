using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;

namespace PacketParser.DataStructures
{
    public class IndexedTreeNode : Dictionary<int, NamedTreeNode>, ITreeNode
    {
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;

namespace WowPacketParser.Misc
{
    public class IndexedTreeNode : Dictionary<int, NamedTreeNode>, ITreeNode
    {
        public TreeNodeEnumerator GetTreeEnumerator()
        {
            return new TreeNodeEnumerator(this);
        }
        public NodeType GetNode<NodeType>(string[] address)
        {
            NodeType ret;
            if (TryGetNode<NodeType>(address, out ret))
                return ret;
            throw new Exception(String.Format("Could not receive object of type {0} from address{1}", typeof(NodeType), address));
        }
        public bool TryGetNode<NodeType>(string[] address, out NodeType ret, int addrIndex = 0)
        {
            if (address.Length == addrIndex - 1)
            {
                if (this is NodeType)
                {
                    ret = (NodeType)((Object)this);
                    return true;
                }
                ret = default(NodeType);
                return false;
            }
            NamedTreeNode node;
            if (this.TryGetValue(Int32.Parse(address[addrIndex]), out node))
                return node.TryGetNode<NodeType>(address, out ret, addrIndex+1);
            ret = default(NodeType);
            return false;
        }
    }
}

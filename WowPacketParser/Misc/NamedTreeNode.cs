using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Wintellect.PowerCollections;

namespace WowPacketParser.Misc
{
    public class NamedTreeNode : OrderedDictionary, ITreeNode
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
            if (address.Length - 1 == addrIndex)
            {
                if (this is NodeType)
                {
                    ret = (NodeType)((Object)this);
                    return true;
                }
                ret = default(NodeType);
                return false;
            }
            if (this.Contains(address[addrIndex]))
            {
                var node = this[address[addrIndex]];
                if (address.Length == addrIndex)
                {
                    if (node is NodeType)
                    {
                        ret = (NodeType)node;
                        return true;
                    }
                    ret = default(NodeType);
                    return false;
                }
                var nodeObj = node as ITreeNode;
                if (nodeObj != null)
                {
                    return nodeObj.TryGetNode<NodeType>(out ret, address, addrIndex + 1);
                }
            }
            ret = default(NodeType);
            return false;
        }
    }
}

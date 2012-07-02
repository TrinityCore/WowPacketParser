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
                    return nodeObj.TryGetNode<NodeType>(address, out ret, addrIndex + 1);
                }
            }
            ret = default(NodeType);
            return false;
        }
    }
}

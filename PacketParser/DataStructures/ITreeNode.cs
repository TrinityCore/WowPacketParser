using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketParser.DataStructures
{
    public interface ITreeNode
    {
        NodeType GetNode<NodeType>(params string[] address);
        bool TryGetNode<NodeType>(out NodeType ret, params string[] address);
        bool TryGetNode<NodeType>(out NodeType ret, string[] address, int addIndex=0);
        TreeNodeEnumerator GetTreeEnumerator();
    }
}

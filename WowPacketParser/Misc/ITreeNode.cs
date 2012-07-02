using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WowPacketParser.Misc
{
    public interface ITreeNode
    {
        NodeType GetNode<NodeType>(string[] address);
        bool TryGetNode<NodeType>(string[] address, out NodeType ret, int addrIndex = 0);
        TreeNodeEnumerator GetTreeEnumerator();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WowPacketParser.Misc
{
    class XMLPacketFormatter : IPacketFormatter
    {
        private PacketNode _node;
        private bool _inCollection = false;

        public void AppendItem(string itemName, params object[] args)
        {
            var newNode = new PacketNode(itemName);
            if (args != null && args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if ((string)args[i] != "")
                    {
                        var pair = new KeyValuePair<string, string>((string)args[i], (string)args[i + 1]);
                        newNode.Attributes.Add(pair);
                        i++;
                    }
                }
            }
            if (_inCollection)
                _node.InnerNodes.Add(newNode);
            else if (itemName != "")
                _node = newNode;
            else
                _node = new PacketNode("packet");
        }

        public void AppendItemWithContent(string itemName, string itemContent, params object[] args)
        {
            AppendItem(itemName, args);
            _node.InnerContent = itemContent;
        }

        public void OpenCollection(string collectionName, params object[] args)
        {
            var newNode = new PacketNode(collectionName);
            foreach (var arg in args)
                newNode.Attributes.Add((KeyValuePair<string, string>)arg);

            _node = newNode;
            _inCollection = true;

        }

        public PacketNode ToNode()
        {
            return _node;
        }

        public void CloseCollection(string collectionName)
        {
            _inCollection = false;
        }

        public void CloseItem()
        {
            _node = null;
        }

       
    }
}

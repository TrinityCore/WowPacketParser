using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;

namespace WowPacketParser.Misc
{
    /// <summary>
    /// This class contains the representation of a packet in xml.
    /// This class is currently under development and not working!
    /// <seealso cref="IPacketFormatter">
    /// This implementation of IPacketFormatter basically contains a new  
    /// xml-based output format. </seealso>
    /// </summary>
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



        public string AppendHeaders(Direction direction, int opcode, long length, int connectionIndex, IPEndPoint endPoint, DateTime time, int number, bool isMultiple)
        {
            var node = new PacketNode("packet");
            var dir = new KeyValuePair<string, string>("direction", direction.ToString());
            var len = new KeyValuePair<string, string>("length", length.ToString());
            var conn = new KeyValuePair<string, string>("connIdx", connectionIndex.ToString());
            var t = new KeyValuePair<string, string>("time", time.ToString("MM/dd/yyyy HH:mm:ss.fff"));
            var num = new KeyValuePair<string, string>("number", number.ToString());
            var mul = new KeyValuePair<string, string>("multiple", isMultiple ? "true" : "false");
            if(endPoint != null)
            {
                var ep = new KeyValuePair<string, string>("endPoint",  endPoint.ToString());
                node.Attributes.Add(ep);
            }
            node.Attributes.Add(dir);
            node.Attributes.Add(len);
            node.Attributes.Add(conn);
            node.Attributes.Add(t);
            node.Attributes.Add(num);
            node.Attributes.Add(mul);

            var opc = new PacketNode("opcode");
            var codename = new KeyValuePair<string, string>("name", Opcodes.GetOpcodeName(opcode, direction, false));
            var codeid = new KeyValuePair<string, string>("id", "0x" + opcode.ToString("X4"));
            opc.Attributes.Add(codename);
            opc.Attributes.Add(codeid);

            node.InnerNodes.Add(opc);
            _node = node;
            return node.ToString();
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading
{
    public sealed class SniffitztReader : IPacketReader
    {
        private readonly IEnumerator<XElement> _element;
        private bool _canRead;
        public SniffitztReader(string file)
        {
            string uri = new Uri(Path.GetFullPath(file)).ToString();
            XDocument xdoc =  new XDocument(uri);
            var elements =  xdoc.XPathSelectElements("*/packet");
            _element = elements.GetEnumerator();
            _canRead = _element.MoveNext();
        }

        public bool CanRead()
        {
            return _canRead;
        }

        public Packet Read(int number, string fileName)
        {
            var element = _element.Current;
            var opcode = (int)element.Attribute("opcode");
            var direction = (string)element.Attribute("direction") == "C2S" ?
                Direction.ClientToServer : Direction.ServerToClient;
            var data = Utilities.HexStringToBinary(element.Value);

            _canRead = _element.MoveNext();

            return new Packet(data, opcode, DateTime.Now, direction, number, fileName);
        }

        public void Dispose()
        {
            _element.Dispose();
        }
    }
}

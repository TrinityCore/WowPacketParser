using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using PacketParser.Enums;
using PacketParser.Misc;
using PacketParser.DataStructures;

namespace PacketParser.Loading
{
    public sealed class SniffitztReader : IPacketReader
    {
        private readonly IEnumerator<XElement> _element;
        private bool _canRead;
        private uint _count;
        private uint _num;
        public SniffitztReader(string file)
        {
            string uri = new Uri(Path.GetFullPath(file)).ToString();
            XDocument xdoc =  new XDocument(uri);
            IEnumerable<XElement> elements =  xdoc.XPathSelectElements("*/packet");
            _element = elements.GetEnumerator();
            _canRead = _element.MoveNext();
            var a = (ICollection<XElement>)elements;
            _count = (uint)a.Count;
            _num = 0;
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
            _num = (uint)number;
            return new Packet(data, opcode, DateTime.Now, direction, number, fileName);
        }

        public void Dispose()
        {
            _element.Dispose();
        }

        public ClientVersionBuild GetBuild()
        {
            return ClientVersion.GetVersion(PeekDateTime());
        }

        public DateTime PeekDateTime()
        {
            var old = _element.Current;
            var p = Read(0, "");
            _element.Reset();
            while(old != _element.Current)
                _canRead = _element.MoveNext();
            return p.Time;
        }

        public uint GetProgress()
        {
            if (_count != 0)
                return (uint)(_num * 100 / _count);
            return 100;
        }
    }
}

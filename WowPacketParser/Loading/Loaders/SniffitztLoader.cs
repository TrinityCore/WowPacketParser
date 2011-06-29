using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading.Loaders
{
    [Loader("sniffitzt")]
    public sealed class SniffitztLoader : Loader
    {
        public SniffitztLoader(string file)
            : base(file)
        {
        }

        public override IEnumerable<Packet> ParseFile()
        {
            var packets = new List<Packet>();
            var uri = new Uri(Path.GetFullPath(FileToParse)).ToString();
            var xdoc = new XDocument(uri);

            foreach (var element in xdoc.XPathSelectElements("*/packet"))
            {
                var opcode = (Opcode)(uint)element.Attribute("opcode");
                var direction = (string)element.Attribute("direction") == "C2S" ?
                    Direction.ClientToServer : Direction.ServerToClient;
                var data = Utilities.HexStringToBinary(element.Value);

                var packet = new Packet(data, opcode, DateTime.Now, direction);
                packets.Add(packet);
            }

            return packets;
        }
    }
}

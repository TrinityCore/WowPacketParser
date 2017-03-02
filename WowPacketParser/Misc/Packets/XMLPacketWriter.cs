using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WowPacketParser.Misc.Packets_Management
{
    class XMLPacketWriter : IPacketWriter
    {
        private XmlDocument _doc;

        public XMLPacketWriter()
        {
            _doc = new XmlDocument();
        }

        public void WriteItem(string format, params object[] args)
        {
            //XmlElement el = (XmlElement)doc.AppendChild(doc.CreateElement("Foo"));
            //el.SetAttribute("Bar", "some & value");
            //el.AppendChild(doc.CreateElement("Nested")).InnerText = "data";
            throw new NotImplementedException();
        }
        
        public void CloseItem()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}

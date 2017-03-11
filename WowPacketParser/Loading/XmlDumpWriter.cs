using System;
using System.IO;
using System.Xml;

namespace WowPacketParser.Loading
{
    public class XmlDumpWriter : IWritingStrategy
    {
        private StreamWriter _writer;
        private XmlDocument _document;
        
        public XmlDumpWriter(string outFileName)
        {
            _document = new XmlDocument();

            if(!File.Exists(outFileName))
            {
                XmlTextWriter textWritter = new XmlTextWriter(outFileName, null);
                textWritter.WriteStartDocument();
                textWritter.WriteStartElement("packets");
                textWritter.WriteEndElement();

                textWritter.Close();
            }

            _document.Load(outFileName);
            
            
            //_writer = new StreamWriter(Console.OpenStandardOutput());
            _writer = new StreamWriter(outFileName, true);
        } 

        public void Write(object input)
        {
            XmlElement elem = _document.CreateElement("line");
            elem.InnerText = input.ToString();
            _document.GetElementsByTagName("packets")[0].AppendChild(elem);
        }

        public void Flush()
        {
            _writer.Flush();
        }

        public void Dispose()
        {
            _document.Save(_writer);
            _writer.Dispose();
            //_reader.Dispose();
        }
    }
}
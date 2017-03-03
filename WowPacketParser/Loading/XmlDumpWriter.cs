

using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading
{
    public class XmlDumpWriter : IDumpWriter
    {
        private XmlWriter _writer;

        public XmlDumpWriter(string fileName)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            settings.Encoding = Encoding.UTF8;
            _writer = XmlWriter.Create(fileName, settings);
        }
        public void WriteItem(Packet packet)
        {
            try
            {
                var formatter = (XMLPacketFormatter)packet.Formatter;
                var node = formatter.ToNode();
                _writer.WriteStartElement(sanitizeString(node.Name));
                if (node.InnerContent != "")
                    _writer.WriteString(sanitizeString(node.InnerContent));

                //_writer.WriteCData(objContacts[i].Address1);

                _writer.WriteEndElement();
            }catch(Exception e)
            {

            }
        }

        public void WriteHeader(string headers)
        {
            try
            {
                var s = "# TrinityCore - WowPacketParser" + Environment.NewLine +
                       "# File name: " + Path.GetFileName(headers) + Environment.NewLine +
                       "# Detected build: " + ClientVersion.Build + Environment.NewLine +
                       "# Detected locale: " + BinaryPacketReader.GetClientLocale() + Environment.NewLine +
                       "# Targeted database: " + Settings.TargetedDatabase + Environment.NewLine +
                       "# Parsing date: " + DateTime.Now.ToString(CultureInfo.InvariantCulture) + Environment.NewLine;
                _writer.WriteStartElement("headers");
                _writer.WriteString(sanitizeString(s));
                _writer.WriteEndElement();
            }catch(Exception e)
            {
                _writer.WriteStartElement("exception");
                _writer.WriteString(e.GetType() + e.Message);
                _writer.WriteEndElement();
            }
        }

        private string sanitizeString(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("xml");
            }

            StringBuilder buffer = new StringBuilder(input.Length);

            foreach (char c in input)
            {
                if (isLegalXmlChar(c))
                {
                    buffer.Append(c);
                }
            }

            return buffer.ToString();
        }

        /// <summary>
        /// Whether a given character is allowed by XML 1.0.
        /// </summary>
        private bool isLegalXmlChar(int character)
        {
            return
            (
                 character == 0x9 /* == '\t' == 9   */          ||
                 character == 0xA /* == '\n' == 10  */          ||
                 character == 0xD /* == '\r' == 13  */          ||
                (character >= 0x20 && character <= 0xD7FF) ||
                (character >= 0xE000 && character <= 0xFFFD) ||
                (character >= 0x10000 && character <= 0x10FFFF)
            );
        }
        public void Dispose()
        {
            _writer.Close();
            _writer = null;
        }
    }
}
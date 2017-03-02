using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading
{
    class TextDumpWriter : IDumpWriter
    {
        private StreamWriter _writer;

        public TextDumpWriter(string fileName)
        {
            _writer = new StreamWriter(fileName, true);
        }

        public void WriteHeader(string headers)
        {
            _writer.WriteLine(headers);
        }

        public void WriteItem(Packet packet)
        {
            _writer.WriteLine(packet.Writer);
            _writer.Flush();
        }

        public void Dispose()
        {

            throw new NotImplementedException();
        }

       
    }
}

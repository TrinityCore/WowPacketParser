using System;
using System.IO;

namespace WowPacketParser.Loading
{
    public class TextDumpWriter : IWritingStrategy
    {
        private StreamWriter _writer;

        public TextDumpWriter(string outFileName)
        {
            _writer = new StreamWriter(outFileName, true);
        }

        public void Write(object input)
        {
            _writer.WriteLine(input.ToString());
        }

        public void Flush()
        {
            _writer.Flush();
        }

        public void Dispose()
        {
            _writer.Dispose();
        }
    }
}

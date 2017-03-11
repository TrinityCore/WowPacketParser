using System;
using System.IO;

namespace WowPacketParser.Loading
{
    public class TextOutputWriter : IWritingStrategy
    {
        private StreamWriter _writer;

        public TextOutputWriter(string outFileName)
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

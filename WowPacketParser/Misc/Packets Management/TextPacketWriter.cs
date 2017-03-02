using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowPacketParser.Misc
{
    public class TextPacketWriter : IPacketWriter
    {
        private StringBuilder _writer;

        public TextPacketWriter()
        {
            _writer = new StringBuilder();
        }

        public void WriteLine(string format, params object[] args)
        {
            _writer.AppendLine(string.Format(format, args));
        }

        public void Clear()
        {
            _writer.Clear();
        }

        public void WriteLine(string value)
        {
            throw new NotImplementedException();
        }

        public override String ToString()
        {
            return _writer.ToString();
        }
    }
}

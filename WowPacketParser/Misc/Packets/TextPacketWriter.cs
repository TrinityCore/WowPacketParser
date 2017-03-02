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

        public void WriteItem(string format, params object[] args)
        {
            _writer.AppendLine(string.Format(format, args));
        }

        //Workaround method, will be removed asap
        public void WriteItem(string format, bool noNewLine, params object[] args)
        {
            if (noNewLine)
                _writer.Append(string.Format(format, args));
        }

        public void CloseItem()
        {
            _writer.Clear();
        }

        public override string ToString()
        {
            return _writer.ToString();
        }
    }
}

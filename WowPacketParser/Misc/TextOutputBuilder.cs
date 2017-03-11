using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowPacketParser.Misc
{
    class TextOutputBuilder : IOutputBuilder
    {
        private StringBuilder _builder;

        public TextOutputBuilder()
        {
            _builder = new StringBuilder();
        }

        public void Append(string value)
        {
            _builder.Append(value);
        }

        public override string ToString()
        {
            return _builder.ToString();
        }

        public void Clear()
        {
            _builder.Clear();
        }
    }
}

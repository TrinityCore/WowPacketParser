using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowPacketParser.Misc
{
    public sealed class Blob
    {
        public byte[] Data { get; }

        public Blob(byte[] data)
        {
            this.Data = data;
        }
    }
}

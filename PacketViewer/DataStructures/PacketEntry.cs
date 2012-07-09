using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketViewer.DataStructures
{
    public class PacketEntry
    {
        public uint Number;
        public ushort Opcode;
        public string OpcodeString;
        public DateTime Time;
        public uint Sec;
        public ushort Length;
    }
}

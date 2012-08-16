using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketViewer.DataStructures
{
    [Serializable]
    public class PacketEntry
    {
        public uint Number;
        public int SubPacketNumber;
        public int UID;
        public string Dir;
        public ushort Opcode;
        public string OpcodeString;
        public DateTime Time;
        public uint Sec;
        public ushort Length;
        public string Status;
    }
}

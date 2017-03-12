using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientUpdateAccountData
    {
        public ulong Player;
        public UnixTime Time;
        public uint Size;
        public byte DataType;
        public Data CompressedData;
    }
}

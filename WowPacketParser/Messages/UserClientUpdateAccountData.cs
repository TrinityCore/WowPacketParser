using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientUpdateAccountData
    {
        public UnixTime Time;
        public uint Size;
        public byte DataType;
        public Data CompressedData;
    }
}

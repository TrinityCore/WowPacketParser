using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientComplaintResult
    {
        public uint ComplaintType;
        public byte Result;
    }
}

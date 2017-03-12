using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliStructMailAttachment
    {
        public ulong ItemGUID;
        public byte AttachPosition;
    }
}

using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct RuneData
    {
        public byte Start;
        public byte Count;
        public List<byte> Cooldowns;
    }
}

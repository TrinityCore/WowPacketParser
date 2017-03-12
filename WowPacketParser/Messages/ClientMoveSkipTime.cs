using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveSkipTime
    {
        public ulong MoverGUID;
        public uint TimeSkipped;
    }
}

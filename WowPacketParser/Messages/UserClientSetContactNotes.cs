using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientSetContactNotes
    {
        public QualifiedGUID Player;
        public string Notes;
    }
}

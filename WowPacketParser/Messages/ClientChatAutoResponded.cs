using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientChatAutoResponded
    {
        public bool IsDND;
        public uint RealmAddress;
        public string AfkMessage;
    }
}

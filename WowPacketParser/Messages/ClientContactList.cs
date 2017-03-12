using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientContactList
    {
        public List<ContactInfo> Contacts;
        public uint Flags;
    }
}

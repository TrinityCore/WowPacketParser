using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientContactList
    {
        public List<ContactInfo> Contacts;
        public uint Flags;
    }
}

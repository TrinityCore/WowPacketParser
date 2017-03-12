using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCalendarEventUpdatedAlert
    {
        public byte EventType;
        public uint OriginalDate;
        public bool ClearPending;
        public ulong EventID;
        public string Description;
        public uint Flags;
        public int TextureID;
        public string EventName;
        public uint LockDate;
        public uint Date;
    }
}

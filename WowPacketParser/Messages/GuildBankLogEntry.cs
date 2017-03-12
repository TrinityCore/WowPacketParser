using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GuildBankLogEntry
    {
        public ulong PlayerGUID;
        public uint TimeOffset;
        public sbyte EntryType;
        public ulong? Money; // Optional
        public int? ItemID; // Optional
        public int? Count; // Optional
        public sbyte? OtherTab; // Optional
    }
}

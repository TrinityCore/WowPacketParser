using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildEventEntry
    {
        public ulong PlayerGUID;
        public ulong OtherGUID;
        public uint TransactionDate;
        public byte TransactionType;
        public byte RankID;
    }
}

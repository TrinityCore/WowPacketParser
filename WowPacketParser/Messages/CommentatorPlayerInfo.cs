using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CommentatorPlayerInfo
    {
        public ulong PlayerGUID;
        public uint DamageDone;
        public uint DamageTaken;
        public uint HealingDone;
        public uint HealingTaken;
        public ushort Kills;
        public ushort Deaths;
        public sbyte Faction;
    }
}

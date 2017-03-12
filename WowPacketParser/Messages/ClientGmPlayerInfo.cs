using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGmPlayerInfo
    {
        public string CharName;
        public uint MapID;
        public uint AreaID;
        public Vector3 Position;
        public float Facing;
        public string AccountName;
        public uint Seconds;
        public uint Hours;
        public uint Minutes;
        public uint Level;
        public uint Race;
        public string GuildName;
        public uint Cls;
        public string FullName;
        public ulong CharGUID;
    }
}

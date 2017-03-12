using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserRouterClientAuthSession
    {
        public uint SiteID;
        public sbyte LoginServerType;
        public sbyte BuildType;
        public uint RealmID;
        public ushort Build;
        public uint LocalChallenge;
        public int LoginServerID;
        public uint RegionID;
        public ulong DosResponse;
        public fixed byte Digest[20];
        public string Account;
        public bool UseIPv6;
        public Data AddonInfo;
    }
}

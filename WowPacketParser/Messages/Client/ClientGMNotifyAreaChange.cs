using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGMNotifyAreaChange
    {
        public List<RealmName> OtherRealms;
        public string AreaShareInternalName;
        public RealmName CurrentRealm;
        public byte AreaShareID;
        public uint AreaID;
        public uint CurrentRealmAddress;
    }
}

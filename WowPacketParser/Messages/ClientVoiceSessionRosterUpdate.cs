using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientVoiceSessionRosterUpdate
    {
        public ushort ComsatPort;
        public ulong LocalMemberGUID;
        public ushort SessionNetworkID;
        public byte SessionType;
        public byte LocalFlags;
        public string ChannelName;
        public uint ComsatAddress;
        public ulong ClientGUID;
        public byte LocalNetworkID;
        public List<ClientVoiceSessionRosterUpdateMember> Members;
        public fixed byte Digest[16];
    }
}

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CommentatorInstance
    {
        public uint MapID;
        public ServerSpec WorldServer;
        public ulong InstanceID;
        public uint Status;
        public CommentatorTeam[/*2*/] Teams;
    }
}

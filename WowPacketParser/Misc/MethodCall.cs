namespace WowPacketParser.Misc
{
    public struct MethodCall
    {
        public MethodCall(ulong type, ulong objectId, uint token)
        {
            Type = type;
            ObjectId = objectId;
            Token = token;
        }

        public ulong Type;

        public ulong ObjectId;

        public uint Token;

        public uint GetServiceHash() { return (uint)(Type >> 32); }

        public uint GetMethodId() { return (uint)(Type & 0xFFFFFFFF); }
    }
}

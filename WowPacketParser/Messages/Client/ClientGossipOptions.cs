namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGossipOptions
    {
        public int ClientOption;
        public byte OptionNPC;
        public sbyte OptionFlags;
        public int OptionCost;
        public string Text;
        public string Confirm;
    }
}

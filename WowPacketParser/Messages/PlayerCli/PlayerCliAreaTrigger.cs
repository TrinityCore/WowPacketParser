namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliAreaTrigger
    {
        public bool Entered;
        public bool FromClient;
        public int AreaTriggerID;
    }
}

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliTotemDestroyed
    {
        public ulong TotemGUID;
        public byte Slot;
    }
}

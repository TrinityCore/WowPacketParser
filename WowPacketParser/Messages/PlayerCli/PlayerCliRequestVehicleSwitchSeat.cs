namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliRequestVehicleSwitchSeat
    {
        public ulong Vehicle;
        public byte SeatIndex;
    }
}

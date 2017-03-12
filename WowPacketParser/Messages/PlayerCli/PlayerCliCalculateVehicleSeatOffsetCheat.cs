using WowPacketParser.Misc;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliCalculateVehicleSeatOffsetCheat
    {
        public ulong PassengerGUID;
        public Vector3 PassengerRawPos;
    }
}

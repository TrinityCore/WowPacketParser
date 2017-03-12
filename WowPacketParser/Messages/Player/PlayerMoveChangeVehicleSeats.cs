using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveChangeVehicleSeats
    {
        public ulong DstVehicle;
        public CliMovementStatus Status;
        public byte DstSeatIndex;
    }
}

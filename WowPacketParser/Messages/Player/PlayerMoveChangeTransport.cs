using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveChangeTransport
    {
        public CliMovementStatus Status;
    }
}

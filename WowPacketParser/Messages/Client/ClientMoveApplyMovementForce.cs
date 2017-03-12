using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveApplyMovementForce
    {
        public CliMovementForce Force;
        public ulong MoverGUID;
        public uint SequenceIndex;
    }
}

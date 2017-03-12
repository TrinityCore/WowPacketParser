using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAreaTriggerMovementUpdate // FIXME: No handlers
    {
        public Vector3 Start;
        public Vector3 End;
    }
}

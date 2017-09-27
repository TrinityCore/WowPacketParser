using WowPacketParser.Enums;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct Tutorial
    {
        public TutorialAction Action;
        public uint TutorialBit;
    }
}

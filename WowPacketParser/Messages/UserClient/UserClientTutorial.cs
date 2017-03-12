using WowPacketParser.Enums;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientTutorial
    {
        public TutorialAction Action;
        public uint TutorialBit;
    }
}

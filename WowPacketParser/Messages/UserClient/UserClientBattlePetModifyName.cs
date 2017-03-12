using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientBattlePetModifyName
    {
        public string Name;
        public DeclinedBattlePetNames? DeclinedNames; // Optional
        public ulong BattlePetGUID;
    }
}

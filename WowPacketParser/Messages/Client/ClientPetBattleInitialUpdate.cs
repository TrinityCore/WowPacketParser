using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientPetBattleInitialUpdate
    {
        public PetBattleFullUpdate MsgData;
    }
}

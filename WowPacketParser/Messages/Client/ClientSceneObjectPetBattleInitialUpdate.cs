using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSceneObjectPetBattleInitialUpdate
    {
        public PetBattleFullUpdate MsgData;
        public ulong SceneObjectGUID;
    }
}

using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSceneObjectPetBattleRoundResult
    {
        public PetBattleRoundResult MsgData;
        public ulong SceneObjectGUID;
    }
}

using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSceneObjectPetBattleFirstRound
    {
        public PetBattleRoundResult MsgData;
        public ulong SceneObjectGUID;
    }
}

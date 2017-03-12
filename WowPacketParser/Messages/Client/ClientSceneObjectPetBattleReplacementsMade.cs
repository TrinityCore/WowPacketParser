using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSceneObjectPetBattleReplacementsMade
    {
        public PetBattleRoundResult MsgData;
        public ulong SceneObjectGUID;
    }
}

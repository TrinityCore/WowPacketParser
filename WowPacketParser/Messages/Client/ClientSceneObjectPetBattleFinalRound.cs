using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSceneObjectPetBattleFinalRound
    {
        public ulong SceneObjectGUID;
        public PetBattleFinalRound MsgData;
    }
}

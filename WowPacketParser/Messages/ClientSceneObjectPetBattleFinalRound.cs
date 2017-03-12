using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSceneObjectPetBattleFinalRound
    {
        public ulong SceneObjectGUID;
        public PetBattleFinalRound MsgData;
    }
}

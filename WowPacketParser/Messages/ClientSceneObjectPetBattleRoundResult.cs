using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSceneObjectPetBattleRoundResult
    {
        public PetBattleRoundResult MsgData;
        public ulong SceneObjectGUID;
    }
}

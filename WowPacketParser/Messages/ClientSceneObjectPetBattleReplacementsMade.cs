using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSceneObjectPetBattleReplacementsMade
    {
        public PetBattleRoundResult MsgData;
        public ulong SceneObjectGUID;
    }
}

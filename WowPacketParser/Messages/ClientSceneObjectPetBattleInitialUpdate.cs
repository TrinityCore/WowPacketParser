using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSceneObjectPetBattleInitialUpdate
    {
        public PetBattleFullUpdate MsgData;
        public ulong SceneObjectGUID;
    }
}

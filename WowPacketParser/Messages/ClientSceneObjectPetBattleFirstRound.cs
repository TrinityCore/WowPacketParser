using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSceneObjectPetBattleFirstRound
    {
        public PetBattleRoundResult MsgData;
        public ulong SceneObjectGUID;
    }
}

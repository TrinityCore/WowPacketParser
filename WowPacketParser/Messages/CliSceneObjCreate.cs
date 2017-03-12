using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliSceneObjCreate
    {
        public CliSceneLocalScriptData? LocalScriptData; // Optional
        public PetBattleFullUpdate? PetBattleFullUpdate; // Optional
    }
}

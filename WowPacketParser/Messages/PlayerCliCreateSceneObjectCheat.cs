using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliCreateSceneObjectCheat
    {
        public int EntryID;
        public CliSceneLocalScriptData ScriptData;
    }
}

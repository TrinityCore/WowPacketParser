using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientVoiceSessionEnable
    {
        public bool EnableVoiceChat;
        public bool EnableMicrophone;
    }
}

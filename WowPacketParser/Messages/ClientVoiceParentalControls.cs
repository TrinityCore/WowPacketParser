using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientVoiceParentalControls
    {
        public bool EnableMicrophone;
        public bool EnableVoiceChat;
    }
}

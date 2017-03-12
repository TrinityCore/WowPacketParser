using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSendTextEmote
    {
        public ulong Target;
        public int EmoteID;
        public int SoundIndex;
    }
}

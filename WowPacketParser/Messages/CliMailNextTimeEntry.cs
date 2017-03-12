using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliMailNextTimeEntry
    {
        public ulong SenderGUID;
        public PlayerGuidLookupHint SenderHint;
        public float TimeLeft;
        public int AltSenderID;
        public byte AltSenderType;
        public int StationeryID;
    }
}

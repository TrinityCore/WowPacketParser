using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct BattlegroundCapturePointInfo
    {
        public ulong Guid;
        public Vector2 Pos;
        public sbyte State;
        public UnixTime CaptureTime;
        public uint CaptureTotalDuration;
    }
}

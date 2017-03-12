using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct BattlegroundPlayerPosition
    {
        public ulong Guid;
        public Vector2 Pos;
        public sbyte IconID;
        public sbyte ArenaSlot;
    }
}

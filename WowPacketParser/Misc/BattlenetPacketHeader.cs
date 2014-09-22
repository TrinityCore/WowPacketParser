using WowPacketParser.Enums;
using WowPacketParser.Enums.Battlenet;

namespace WowPacketParser.Misc
{
    public sealed class BattlenetPacketHeader
    {
        public ushort Opcode { get; set; }
        public BattlenetChannel Channel { get; set; }
        public Direction Direction { get; set; }

        public override bool Equals(object obj)
        {
            var that = obj as BattlenetPacketHeader;
            if (that == null)
                 return false;

            if (Opcode != that.Opcode)
                return false;
            if (Channel != that.Channel)
                return false;

            return Direction == that.Direction;
        }

        public override int GetHashCode()
        {
            return new { Opcode, Channel, Direction }.GetHashCode();
        }
    }
}

using WowPacketParser.Enums;
using WowPacketParser.Enums.Battlenet;

namespace WowPacketParser.Misc
{
    public sealed class BattlenetPacketHeader
    {
        public BattlenetPacketHeader(ushort opcode, BattlenetChannel channel, Direction direction)
        {
            Opcode = opcode;
            Channel = channel;
            Direction = direction;
        }

        public ushort Opcode { get; }
        public BattlenetChannel Channel { get; }
        public Direction Direction { get; }

        public override bool Equals(object obj)
        {
            BattlenetPacketHeader that = obj as BattlenetPacketHeader;

            if (Opcode != that?.Opcode)
                return false;
            if (Channel != that.Channel)
                return false;

            return Direction == that.Direction;
        }

        public override int GetHashCode()
        {
            return new {Opcode, Channel, Direction}.GetHashCode();
        }
    }
}

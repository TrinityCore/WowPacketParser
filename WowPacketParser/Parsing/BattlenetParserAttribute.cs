using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Battlenet;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class BattlenetParserAttribute : Attribute
    {
        public BattlenetParserAttribute(BattlenetOpcode opcode, BattlenetChannel channel, Direction direction)
        {
            Header = new BattlenetPacketHeader();
            Header.Opcode = (ushort)opcode;
            Header.Channel = channel;
            Header.Direction = direction;
        }

        public BattlenetPacketHeader Header { get; set; }
    }
}

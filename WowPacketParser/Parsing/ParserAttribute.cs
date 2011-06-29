using System;
using WowPacketParser.Enums;

namespace WowPacketParser.Parsing
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class ParserAttribute : Attribute
    {
        public ParserAttribute(Opcode opcode)
        {
            Opcode = opcode;
        }

        public Opcode Opcode { get; private set; }
    }
}

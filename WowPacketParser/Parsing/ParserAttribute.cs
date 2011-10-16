using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;

namespace WowPacketParser.Parsing
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class ParserAttribute : Attribute
    {
        public ParserAttribute(Opcode opcode)
        {
            Opcode = Opcodes.GetOpcode(opcode);
        }

        public int Opcode { get; private set; }
    }
}

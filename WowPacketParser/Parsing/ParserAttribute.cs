using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class ParserAttribute : Attribute
    {
        public ParserAttribute(Opcode opcode)
        {
            Opcode = Opcodes.GetOpcode(opcode);
        }

        public ParserAttribute(Opcode opcode, ClientVersionBuild minBuild)
        {
            if (ClientVersion.Version >= minBuild)
                Opcode = Opcodes.GetOpcode(opcode);
        }

        public ParserAttribute(Opcode opcode, ClientVersionBuild minBuild, ClientVersionBuild maxBuild)
        {
            if (ClientVersion.Version >= minBuild && ClientVersion.Version <= maxBuild)
                Opcode = Opcodes.GetOpcode(opcode);
        }

        public int Opcode { get; private set; }
    }
}

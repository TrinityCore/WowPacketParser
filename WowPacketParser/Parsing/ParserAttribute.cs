using System;
using PacketParser.Enums;
using PacketParser.Enums.Version;
using PacketParser.Misc;

namespace PacketParser.Parsing
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class ParserAttribute : Attribute
    {
        public ParserAttribute(Opcode opcode)
        {
            Opcode = Opcodes.GetOpcode(opcode);
        }

        // [addedInVersion, +inf[
        public ParserAttribute(Opcode opcode, ClientVersionBuild addedInVersion)
        {
            if (ClientVersion.AddedInVersion(addedInVersion))
                Opcode = Opcodes.GetOpcode(opcode);
            else
                Opcode = 0;
        }

        // [addedInVersion, removedInVersion[
        public ParserAttribute(Opcode opcode, ClientVersionBuild addedInVersion, ClientVersionBuild removedInVersion)
        {
            if (ClientVersion.AddedInVersion(addedInVersion) && ClientVersion.RemovedInVersion(removedInVersion))
                Opcode = Opcodes.GetOpcode(opcode);
            else
                Opcode = 0;
        }

        public ParserAttribute(int opcode)
        {
            Opcode = opcode;
        }

        public int Opcode { get; private set; }
    }
}

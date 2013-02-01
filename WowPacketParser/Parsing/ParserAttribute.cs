using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class ParserAttribute : Attribute
    {
        // versionless
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

        // versionless
        public ParserAttribute(int opcode)
        {
            Opcode = opcode;
        }

        // [addedInVersion, +inf[
        public ParserAttribute(int opcode, ClientVersionBuild addedInVersion)
        {
            if (ClientVersion.AddedInVersion(addedInVersion))
                Opcode = opcode;
            else
                Opcode = 0;
        }

        // [addedInVersion, removedInVersion[
        public ParserAttribute(int opcode, ClientVersionBuild addedInVersion, ClientVersionBuild removedInVersion)
        {
            if (ClientVersion.AddedInVersion(addedInVersion) && ClientVersion.RemovedInVersion(removedInVersion))
                Opcode = opcode;
            else
                Opcode = 0;
        }

        public int Opcode { get; private set; }
    }
}

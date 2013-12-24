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
            Opcode = opcode;
            Direction = Direction.Both;
        }

        public ParserAttribute(Opcode opcode, Direction direction)
        {
            Opcode = opcode;
            Direction = direction;
        }

        // [addedInVersion, +inf[
        public ParserAttribute(Opcode opcode, ClientVersionBuild addedInVersion)
        {
            if (ClientVersion.AddedInVersion(addedInVersion))
                Opcode = opcode;
            Direction = Direction.Both;
        }

        public ParserAttribute(Opcode opcode, ClientVersionBuild addedInVersion, Direction direction)
        {
            if (ClientVersion.AddedInVersion(addedInVersion))
                Opcode = opcode;
            Direction = direction;
        }

        // [addedInVersion, removedInVersion[
        public ParserAttribute(Opcode opcode, ClientVersionBuild addedInVersion, ClientVersionBuild removedInVersion)
        {
            if (ClientVersion.AddedInVersion(addedInVersion) && ClientVersion.RemovedInVersion(removedInVersion))
                Opcode = opcode;
            Direction = Direction.Both;
        }

        public ParserAttribute(Opcode opcode, ClientVersionBuild addedInVersion, ClientVersionBuild removedInVersion, Direction direction)
        {
            if (ClientVersion.AddedInVersion(addedInVersion) && ClientVersion.RemovedInVersion(removedInVersion))
                Opcode = opcode;
            Direction = direction;
        }

        // versionless
        public ParserAttribute(int opcode)
        {
            Opcode = Opcodes.GetOpcode(opcode);
            Direction = Direction.Both;
        }

        public ParserAttribute(int opcode, Direction direction)
        {
            Opcode = Opcodes.GetOpcode(opcode, direction);
            Direction = direction;
        }

        // [addedInVersion, +inf[
        public ParserAttribute(int opcode, ClientVersionBuild addedInVersion)
        {
            if (ClientVersion.AddedInVersion(addedInVersion))
                Opcodes.GetOpcode(opcode);
            Direction = Direction.Both;
        }

        public ParserAttribute(int opcode, ClientVersionBuild addedInVersion, Direction direction)
        {
            if (ClientVersion.AddedInVersion(addedInVersion))
                Opcodes.GetOpcode(opcode, direction);
            Direction = direction;
        }

        // [addedInVersion, removedInVersion[
        public ParserAttribute(int opcode, ClientVersionBuild addedInVersion, ClientVersionBuild removedInVersion)
        {
            if (ClientVersion.AddedInVersion(addedInVersion) && ClientVersion.RemovedInVersion(removedInVersion))
                Opcode = Opcodes.GetOpcode(opcode);
            Direction = Direction.Both;
        }

        public ParserAttribute(int opcode, ClientVersionBuild addedInVersion, ClientVersionBuild removedInVersion, Direction direction)
        {
            if (ClientVersion.AddedInVersion(addedInVersion) && ClientVersion.RemovedInVersion(removedInVersion))
                Opcode = Opcodes.GetOpcode(opcode, direction);
            Direction = direction;
        }

        public Opcode Opcode { get; private set; }
        public Direction Direction { get; private set; }
    }
}

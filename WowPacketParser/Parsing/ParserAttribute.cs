using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class ParserAttribute : Attribute
    {
        public ParserAttribute(Opcode opcode)
        {
            Opcode = opcode;
        }

        /// <summary>
        /// [addedInVersion, +inf[
        /// </summary>
        /// <param name="opcode"></param>
        /// <param name="addedInVersion"></param>
        public ParserAttribute(Opcode opcode, ClientVersionBuild addedInVersion)
        {
            if (ClientVersion.AddedInVersion(addedInVersion))
                Opcode = opcode;
        }

        /// <summary>
        /// [addedInVersion, +inf[
        /// </summary>
        /// <param name="opcode"></param>
        /// <param name="addedInVersion"></param>
        public ParserAttribute(Opcode opcode, ClientBranch branch)
        {
            if (ClientVersion.Branch == branch)
                Opcode = opcode;
        }

        /// <summary>
        /// [addedInVersion, +inf[
        /// </summary>
        /// <param name="opcode"></param>
        /// <param name="branch"></param>
        /// <param name="addedInVersion"></param>
        public ParserAttribute(Opcode opcode, ClientBranch branch, ClientVersionBuild addedInVersion)
        {
            if (ClientVersion.AddedInVersion(branch, addedInVersion))
                Opcode = opcode;
        }

        /// <summary>
        /// [addedInVersion, removedInVersion[
        /// </summary>
        /// <param name="opcode"></param>
        /// <param name="addedInVersion"></param>
        /// <param name="removedInVersion"></param>
        public ParserAttribute(Opcode opcode, ClientVersionBuild addedInVersion, ClientVersionBuild removedInVersion)
        {
            if (ClientVersion.InVersion(addedInVersion, removedInVersion))
                Opcode = opcode;
        }

        /// <summary>
        /// [addedInVersion, removedInVersion[
        /// </summary>
        /// <param name="opcode"></param>
        /// <param name="branch"></param>
        /// <param name="addedInVersion"></param>
        /// <param name="removedInVersion"></param>
        public ParserAttribute(Opcode opcode, ClientBranch branch, ClientVersionBuild addedInVersion, ClientVersionBuild removedInVersion)
        {
            if (ClientVersion.InVersion(branch, addedInVersion, removedInVersion))
                Opcode = opcode;
        }

        public Opcode Opcode { get; private set; }
    }
}

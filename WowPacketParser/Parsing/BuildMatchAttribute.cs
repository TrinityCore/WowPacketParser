using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class BuildMatchAttribute : Attribute
    {        
        /// <param name="buildVersion"></param>
        public BuildMatchAttribute(ClientVersionBuild buildVersion)
        {
            if (ClientVersion.Build == buildVersion)
                BuildVersion = buildVersion;
            else
                BuildVersion = ClientVersionBuild.None;
        }

        public ClientVersionBuild BuildVersion { get; private set; }
    }
}

using System;

namespace WowPacketParser.SQL.Builders
{
    /// <summary>
    /// Attribute to mark SQL output generating methods. Return value of a marked method must be a <see cref="string"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class BuilderMethodAttribute : Attribute
    {
        public BuilderMethodAttribute()
        {
        }

        public BuilderMethodAttribute(bool checkVersionMissmatch)
        {
            CheckVersionMismatch = checkVersionMissmatch;
        }
        /// <summary>
        /// True if mismatch between targeted database and sniff version should be checked
        /// </summary>
        public bool CheckVersionMismatch { get; private set; }

        /// <summary>
        /// If true unit list will be included as method invocation parameter
        /// </summary>
        public bool Units { get; set; }

        /// <summary>
        /// If true gameobject list will be included as method invocation parameter
        /// </summary>
        public bool Gameobjects { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class BuilderClassAttribute : Attribute
    {
    }
}

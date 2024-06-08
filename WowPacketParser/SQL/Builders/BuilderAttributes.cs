using System;
using WowPacketParser.Enums;

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
            Database = TargetSQLDatabase.World;
        }
        public BuilderMethodAttribute(TargetSQLDatabase database)
        {
            Database = database;
        }

        public BuilderMethodAttribute(bool checkVersionMissmatch)
        {
            Database = TargetSQLDatabase.World;
            CheckVersionMismatch = checkVersionMissmatch;
        }

        public BuilderMethodAttribute(bool checkVersionMissmatch, TargetSQLDatabase database)
        {
            Database = database;
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

        // <summary>
        // Defines the targeted database
        // </summary>
        public TargetSQLDatabase Database;
    }

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class BuilderClassAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ProtoBuilderClassAttribute : Attribute
    {
        public ProtoBuilderClassAttribute()
        {
            Database = TargetSQLDatabase.World;
        }
        public ProtoBuilderClassAttribute(TargetSQLDatabase database)
        {
            Database = database;
        }

        public ProtoBuilderClassAttribute(bool checkVersionMissmatch)
        {
            Database = TargetSQLDatabase.World;
            CheckVersionMismatch = checkVersionMissmatch;
        }

        public ProtoBuilderClassAttribute(bool checkVersionMissmatch, TargetSQLDatabase database)
        {
            Database = database;
            CheckVersionMismatch = checkVersionMissmatch;
        }

        /// <summary>
        /// True if mismatch between targeted database and sniff version should be checked
        /// </summary>
        public bool CheckVersionMismatch { get; private set; }

        // <summary>
        // Defines the targeted database
        // </summary>
        public TargetSQLDatabase Database;
    }
}

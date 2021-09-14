using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.SQL
{
    /// <summary>
    /// Table name in database
    /// Only usuable with structs or classes
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    public sealed class DBTableNameAttribute : Attribute
    {
        /// <summary>
        /// Table name
        /// </summary>
        public readonly string Name;

        private readonly TargetedDatabase? _addedInVersion;

        private readonly TargetedDatabase? _removedInVersion;

        /// <summary>
        /// </summary>
        /// <param name="name">table name</param>
        public DBTableNameAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// </summary>
        /// <param name="name">table name</param>
        /// <param name="addedInVersion">initial version</param>
        public DBTableNameAttribute(string name, TargetedDatabase addedInVersion)
            : this(name)
        {
            _addedInVersion = addedInVersion;
        }

        /// <summary>
        /// </summary>
        /// <param name="name">table name</param>
        /// <param name="addedInVersion">initial version</param>
        /// <param name="removedInVersion">final version</param>
        public DBTableNameAttribute(string name, TargetedDatabase addedInVersion, TargetedDatabase removedInVersion)
            : this(name, addedInVersion)
        {
            _removedInVersion = removedInVersion;
        }

        public bool IsVisible()
        {
            TargetedDatabase target = Settings.TargetedDatabase;

            if (_addedInVersion.HasValue)
                if (_addedInVersion.Value < target)
                    return false;

            if (_removedInVersion.HasValue)
                if (target >= _removedInVersion.Value)
                    return false;

            return true;
        }
    }
}

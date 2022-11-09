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

        private readonly TargetedDatabaseFlag _validForVersion;

        /// <summary>
        /// </summary>
        /// <param name="name">table name</param>
        public DBTableNameAttribute(string name)
        {
            Name = name;
            _validForVersion = TargetedDatabaseFlag.Any;
        }

        /// <summary>
        /// </summary>
        /// <param name="name">table name</param>
        /// <param name="addedInVersion">initial version</param>
        public DBTableNameAttribute(string name, TargetedDatabaseFlag validForVersion)
            : this(name)
        {
            _validForVersion = validForVersion;
        }

        public bool IsVisible()
        {
            int target = (int)Settings.TargetedDatabase;
            TargetedDatabaseFlag targetFlag = (TargetedDatabaseFlag)(1 << target);

            if (_validForVersion.HasFlag(targetFlag))
                return true;

            return false;
        }
    }
}

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
        public DBTableNameAttribute(string name, TargetedDatabase addedInVersion = TargetedDatabase.Zero, TargetedDatabase removedInVersion = TargetedDatabase.Zero)
        {
            Name = name;
        }

        public bool IsVisible()
        {
            TargetedDatabase target = Settings.TargetedDatabase;

            if (_addedInVersion.HasValue && !_removedInVersion.HasValue)
                return target >= _addedInVersion.Value;

            if (_addedInVersion.HasValue && _removedInVersion.HasValue)
                return target >= _addedInVersion.Value && target < _removedInVersion.Value;

            return true;
        }
    }
}

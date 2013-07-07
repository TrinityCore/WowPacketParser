using System;

namespace WowPacketParser.SQL
{
    /// <summary>
    /// Table name in database
    /// Only usuable with structs or classes
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    sealed public class DBTableNameAttribute : Attribute
    {
        /// <summary>
        /// Table name
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// </summary>
        /// <param name="name">table name</param>
        public DBTableNameAttribute(string name)
        {
            Name = name;
        }
    }
}
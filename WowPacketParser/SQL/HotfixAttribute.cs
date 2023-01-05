using System;

namespace WowPacketParser.SQL
{
    /// <summary>
    /// Marks if class belongs to hotfix table
    /// Only usuable with structs or classes
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    public sealed class HotfixAttribute : Attribute { }
}

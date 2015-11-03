using System;

namespace WowPacketParser.SQL.Builders
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class BuilderMethodAttribute : Attribute
    {
        public bool Units { get; set; }
        public bool Gameobjects { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class BuilderClassAttribute : Attribute
    {
    }
}

using System;

namespace WowPacketParser.SQL.Builders
{
    [AttributeUsage(AttributeTargets.Method)]
    sealed public class BuilderMethodAttribute : Attribute
    {
        public bool Units { get; set; }
        public bool Gameobjects { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    sealed public class BuilderClassAttribute : Attribute
    {
    }
}

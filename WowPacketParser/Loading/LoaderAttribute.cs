using System;

namespace WowPacketParser.Loading
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class LoaderAttribute : Attribute
    {
        public LoaderAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}

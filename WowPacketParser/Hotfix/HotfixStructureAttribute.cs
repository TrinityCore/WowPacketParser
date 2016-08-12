using System;
using WowPacketParser.Enums;

namespace WowPacketParser.Hotfix
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class HotfixStructureAttribute : Attribute
    {
        public DB2Hash Hash { get; }
        public bool HasIndexInData { get; set; } = true;

        public HotfixStructureAttribute(DB2Hash hash)
        {
            Hash = hash;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class HotfixArrayAttribute : Attribute
    {
        public int Size { get; }

        public HotfixArrayAttribute(int arraySize)
        {
            Size = arraySize;
        }
    }
    
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class HotfixSerializerAttribute : Attribute
    {
        
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class HotfixVersionAttribute : Attribute
    {
        public ClientVersionBuild Build { get; set; }
        public bool RemovedInVersion { get; set; }

        public HotfixVersionAttribute(ClientVersionBuild build, bool removedInVersion)
        {
            Build = build;
            RemovedInVersion = removedInVersion;
        }
    }
}

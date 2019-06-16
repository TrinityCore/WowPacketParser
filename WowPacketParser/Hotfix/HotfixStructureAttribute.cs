using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

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

        public HotfixStructureAttribute(DB2Hash hash, ClientVersionBuild addedInVersio)
        {
            if (ClientVersion.AddedInVersion(addedInVersio))
                Hash = hash;
        }

        public HotfixStructureAttribute(DB2Hash hash, ClientVersionBuild addedInVersion, ClientVersionBuild removedInVersion)
        {
            if (ClientVersion.InVersion(addedInVersion, removedInVersion))
                Hash = hash;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class HotfixArrayAttribute : Attribute
    {
        public int Size { get; }
        public bool IsPosition { get; }

        public HotfixArrayAttribute(int arraySize)
        {
            Size = arraySize;
            IsPosition = false;
        }

        public HotfixArrayAttribute(int arraySize, bool isPosition)
        {
            Size = arraySize;
            IsPosition = isPosition;
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

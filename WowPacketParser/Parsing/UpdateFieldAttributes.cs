using System;
using WowPacketParser.Enums;

namespace WowPacketParser.Parsing
{
    [Flags]
    public enum UpdateFieldCreateFlag
    {
        None = 0,
        Unk1 = 0x1,
        Unk2 = 0x2,
        Unk4 = 0x4,
        Unk8 = 0x8,
        Unk16 = 0x10,
        Unk32 = 0x20,
        Unk64 = 0x40,
        Unk128 = 0x80,
        Unk256 = 0x100,
    }
    public enum UpdateFieldType
    {
        Default, // Old formatting - Uint/Float - Supports variable length
        Guid, // Must be 64-bit or 128-bit
        Quaternion, // 4x float
        PackedQuaternion, // ulong
        Byte,
        Ushort,
        Short,
        Uint, // Supports variable length
        Int, // Supports variable length
        Float, // Supports variable length
        Ulong,
        Long,
        Bytes,  // Supports variable length
        DynamicUint,
        Custom
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    public sealed class UpdateFieldAttribute : Attribute
    {
        public UpdateFieldAttribute(UpdateFieldType attrib)
        {
            UFAttribute = attrib;
            Version = ClientVersionBuild.Zero;
            ArrayGroup = 0;
            IsDynamicCounter = false;
            Flag = UpdateFieldCreateFlag.None;
        }

        public UpdateFieldAttribute(UpdateFieldType attrib, UpdateFieldCreateFlag flag)
        {
            UFAttribute = attrib;
            Version = ClientVersionBuild.Zero;
            ArrayGroup = 0;
            IsDynamicCounter = false;
            Flag = flag;
        }

        public UpdateFieldAttribute(UpdateFieldType attrib, int arrayGroup)
        {
            UFAttribute = attrib;
            Version = ClientVersionBuild.Zero;
            ArrayGroup = arrayGroup;
            IsDynamicCounter = false;
            Flag = UpdateFieldCreateFlag.None;
        }

        public UpdateFieldAttribute(UpdateFieldType attrib, int arrayGroup, UpdateFieldCreateFlag flag)
        {
            UFAttribute = attrib;
            Version = ClientVersionBuild.Zero;
            ArrayGroup = arrayGroup;
            IsDynamicCounter = false;
            Flag = flag;
        }

        public UpdateFieldAttribute(UpdateFieldType attrib, bool isDynamicCounter)
        {
            UFAttribute = attrib;
            Version = ClientVersionBuild.Zero;
            ArrayGroup = 0;
            IsDynamicCounter = isDynamicCounter;
            Flag = UpdateFieldCreateFlag.None;
        }

        public UpdateFieldAttribute(UpdateFieldType attrib, ClientVersionBuild fromVersion)
        {
            UFAttribute = attrib;
            Version = fromVersion;
            ArrayGroup = 0;
            IsDynamicCounter = false;
            Flag = UpdateFieldCreateFlag.None;
        }

        public UpdateFieldAttribute(UpdateFieldType attrib, int arrayGroup, ClientVersionBuild fromVersion)
        {
            UFAttribute = attrib;
            Version = fromVersion;
            ArrayGroup = arrayGroup;
            IsDynamicCounter = false;
            Flag = UpdateFieldCreateFlag.None;
        }

        public UpdateFieldAttribute(UpdateFieldType attrib, bool isDynamicCounter, ClientVersionBuild fromVersion)
        {
            UFAttribute = attrib;
            Version = fromVersion;
            ArrayGroup = 0;
            IsDynamicCounter = isDynamicCounter;
            Flag = UpdateFieldCreateFlag.None;
        }

        public UpdateFieldAttribute(UpdateFieldType attrib, int arrayGroup, bool isDynamicCounter, ClientVersionBuild fromVersion)
        {
            UFAttribute = attrib;
            Version = fromVersion;
            ArrayGroup = arrayGroup;
            IsDynamicCounter = isDynamicCounter;
            Flag = UpdateFieldCreateFlag.None;
        }

        public UpdateFieldType UFAttribute { get; private set; }
        public ClientVersionBuild Version { get; private set; }
        public int ArrayGroup { get; private set; }
        public bool IsDynamicCounter { get; private set; }
        public UpdateFieldCreateFlag Flag { get; private set; }
    }
}
using System;
using WowPacketParser.Enums;

namespace WowPacketParser.Parsing
{
    public enum UpdateFieldType
    {
        Default, // Old formatting - Uint/Float - Supports variable length
        Guid, // Must be 64-bit or 128-bit
        Quaternion, // 4x float
        PackedQuaternion, // ulong
        Uint, // Supports variable length
        Int, // Supports variable length
        Float, // Supports variable length
        Bytes,  // Supports variable length
        Short,
        Enum,
        Custom
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    public sealed class UpdateFieldAttribute : Attribute
    {
        public UpdateFieldAttribute(UpdateFieldType attrib)
        {
            UFAttribute = attrib;
            Version = ClientVersionBuild.Zero;
        }

        public UpdateFieldAttribute(UpdateFieldType attrib, ClientVersionBuild fromVersion)
        {
            UFAttribute = attrib;
            Version = fromVersion;
        }

        public UpdateFieldAttribute(UpdateFieldType attrib, Type enumType)
        {
            UFAttribute = attrib;
            Version = ClientVersionBuild.Zero;
            EnumType = enumType;
        }

        public UpdateFieldAttribute(UpdateFieldType attrib, ClientVersionBuild fromVersion, Type enumType)
        {
            UFAttribute = attrib;
            Version = fromVersion;
            EnumType = enumType;
        }

        public UpdateFieldType UFAttribute { get; private set; }
        public ClientVersionBuild Version { get; private set; }
        public Type EnumType { get; private set; }
    }
}
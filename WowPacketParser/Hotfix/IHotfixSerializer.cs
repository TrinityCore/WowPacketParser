using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Sigil;
using WowPacketParser.Loading;
using WowPacketParser.Misc;

namespace WowPacketParser.Hotfix
{
    public abstract class IHotfixSerializer<T> where T : class, new()
    {
        // ReSharper disable once StaticMemberInGenericType
        protected static Dictionary<TypeCode, MethodInfo> _binaryReaders { get; } = new Dictionary<TypeCode, MethodInfo>
        {
            { TypeCode.String, typeof (Packet).GetMethod("ReadCString", new [] { typeof(string), typeof(object[]) }) }
        };

        static IHotfixSerializer()
        {
            foreach (var methodInfo in typeof(Packet).GetMethods(BindingFlags.Public | BindingFlags.Instance).Where(mi =>
               mi.GetGenericArguments().Length == 0 && mi.GetParameters().Length == 2 && mi.GetParameters()[0].ParameterType == typeof(string)))
            {
                var returnCode = Type.GetTypeCode(methodInfo.ReturnType);
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (returnCode)
                {
                    case TypeCode.SByte:
                    case TypeCode.Byte:
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                    case TypeCode.Single:
                    case TypeCode.Double:
                        _binaryReaders[returnCode] = methodInfo;
                        break;
                }
            }
        }

        protected Func<Packet, T> _deserializer;
        protected Action<T, List<string>> _serializer;

        public void GenerateSerializer()
        {
            try
            {
                var serializationEmitter = Emit<Action<T, List<string>>>.NewDynamicMethod();

                foreach (var propInfo in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (propInfo.GetGetMethod() == null || propInfo.GetSetMethod() == null || !ShouldRead(propInfo))
                        continue;

                    serializationEmitter.LoadArgument(1);
                    serializationEmitter.LoadArgument(0);

                    serializationEmitter.CallVirtual(propInfo.GetGetMethod());
                    serializationEmitter.Call(
                        typeof(HotfixExtensions).GetMethod(propInfo.PropertyType.IsArray ? "AppendArray" : "Append",
                            new[] { typeof(List<string>), propInfo.PropertyType }));
                }
                serializationEmitter.Return();
                _serializer = serializationEmitter.CreateDelegate();
            }
            catch (SigilVerificationException sve)
            {
                Console.WriteLine(sve);
            }
        }
        public abstract void GenerateDeserializer();

        protected bool ShouldRead(PropertyInfo propInfo)
        {
            var removedAttr = propInfo.GetCustomAttributes<HotfixVersionAttribute>();

            foreach (var attr in removedAttr)
            {
                if (attr.RemovedInVersion)
                {
                    if (!ClientVersion.RemovedInVersion(attr.Build))
                        return false;
                }
                else if (!ClientVersion.AddedInVersion(attr.Build))
                    return false;
            }

            return true;
        }

        public T Deserialize(Packet packet) => _deserializer(packet);

        public void SerializeStore(HotfixStore<T> store, StringBuilder hotfixBuilder, StringBuilder localeBuilder)
        {
            if (store.Records.Count == 0)
                return;

            var hotfixStructureAttribute = typeof (T).GetCustomAttribute<HotfixStructureAttribute>();
            Debug.Assert(hotfixStructureAttribute != null);

            var propertiesInfos = typeof (T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var tableName = Regex.Replace(hotfixStructureAttribute.Hash.ToString(), @"((?<=.)[A-Z][a-zA-Z]*)|((?<=[a-zA-Z])\d+)",
                @"_$1$2", RegexOptions.Compiled).ToLower();

            if (localeBuilder != null && propertiesInfos.Any(
                propInfo => propInfo.PropertyType == typeof (string) || propInfo.PropertyType == typeof (string[])))
            {
                localeBuilder.AppendLine("SET NAMES 'utf8';");
                localeBuilder.Append($"INSERT INTO `{tableName}_locale` (");
                if (!hotfixStructureAttribute.HasIndexInData)
                    localeBuilder.Append("`ID`, ");
                localeBuilder.Append("`locale`, ");
            }

            hotfixBuilder.AppendLine("SET NAMES 'utf8';");
            hotfixBuilder.Append($"INSERT INTO `{tableName}` (");
            if (!hotfixStructureAttribute.HasIndexInData)
                hotfixBuilder.Append("`ID`, ");

            foreach (var propInfo in propertiesInfos)
            {
                if (propInfo.PropertyType.IsArray)
                {
                    var isString = propInfo.PropertyType.GetElementType() == typeof (string);

                    var arraySizeAttribute = propInfo.GetCustomAttribute<HotfixArrayAttribute>();
                    for (var i = 0; i < arraySizeAttribute.Size; ++i)
                    {
                        hotfixBuilder.Append($"`{propInfo.Name}{i}`, ");
                        if (localeBuilder != null && isString)
                            localeBuilder.Append($"{propInfo.Name}{i}_lang`, ");
                    }
                }
                else
                {
                    hotfixBuilder.Append($"`{propInfo.Name}`, ");
                    if (localeBuilder != null && propInfo.PropertyType == typeof (string))
                        localeBuilder.Append($"`{propInfo.Name}_lang`, ");
                }
            }

            hotfixBuilder.AppendLine("`VerifiedBuild`) VALUES");
            localeBuilder?.AppendLine("`VerifiedBuild`) VALUES");

            var remainingCount = store.Records.Count - 1;

            foreach (var kv in store.Records)
            {
                hotfixBuilder.Append("(");
                localeBuilder?.Append("(");
                if (!hotfixStructureAttribute.HasIndexInData)
                {
                    hotfixBuilder.Append($"{kv.Key}, ");
                    localeBuilder?.Append($"{kv.Key}, ");
                }

                localeBuilder?.Append($"{BinaryPacketReader.GetLocale()}, ");

                var lineTokens = new List<string>();
                _serializer((T)kv.Value, lineTokens);
                hotfixBuilder.Append(string.Join(", ", lineTokens));

                hotfixBuilder.Append($", {ClientVersion.BuildInt}");
                hotfixBuilder.AppendLine(remainingCount > 0 ? ")," : ");");

                localeBuilder?.Append(string.Join(", ", lineTokens.Where(l => l[0] == '"')));
                localeBuilder?.Append($", {ClientVersion.BuildInt}");
                localeBuilder?.AppendLine(remainingCount > 0 ? ")," : ");");
                --remainingCount;
            }

            hotfixBuilder.AppendLine();
            localeBuilder?.AppendLine();
        }
    }
}

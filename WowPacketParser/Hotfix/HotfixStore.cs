using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Hotfix
{
    public interface IHotfixStore
    {
        void AddRecord(int recordKey, Packet packet);
        bool RemoveRecord(int recordKey);
        object GetRecord(int recordKey);

        void Serialize(StringBuilder hotfixBuilder, StringBuilder localeBuilder);
    }

    public sealed class HotfixStore<T> : IHotfixStore where T : class, new()
    {
        public HotfixStore(IHotfixSerializer<T> serializer)
        {
            Serializer = serializer;
            Serializer.GenerateDeserializer();
            Serializer.GenerateSerializer();
        }

        public Dictionary<int, object> Records { get; } = new Dictionary<int, object>();
        public void Serialize(StringBuilder hotfixBuilder, StringBuilder localeBuilder) => Serializer.SerializeStore(this, hotfixBuilder, localeBuilder);

        private static IHotfixSerializer<T> Serializer { get; set; }

        public void AddRecord(int recordKey, Packet packet) => Records[recordKey] = Serializer.Deserialize(packet);
        public bool RemoveRecord(int recordKey) => Records.Remove(recordKey);
        public object GetRecord(int recordKey) => Records[recordKey];
    }

    public static class HotfixStoreMgr
    {
        public static event Action<DB2Hash, int, bool> OnRecordReceived;

        private static readonly Dictionary<DB2Hash, IHotfixStore> _stores = new Dictionary<DB2Hash, IHotfixStore>();

        public static IHotfixStore GetStore(DB2Hash hash)
        {
            return _stores.ContainsKey(hash) ? _stores[hash] : null;
        }

        public static void AddRecord(DB2Hash fileHash, int recordKey, Packet db2File)
        {
            GetStore(fileHash)?.AddRecord(recordKey, db2File);
            OnRecordReceived?.Invoke(fileHash, recordKey, true);
        }

        public static void RemoveRecord(DB2Hash fileHash, int recordKey)
        {
            GetStore(fileHash)?.RemoveRecord(recordKey);
            OnRecordReceived?.Invoke(fileHash, recordKey, false);
        }

        public static HotfixStore<T> GetStore<T>(DB2Hash hash) where T : class, new() => (HotfixStore<T>) GetStore(hash);

        public static void LoadStores(Assembly asm)
        {
            var types = asm.GetTypes();
            var hotfixSerializers = types.Where(type => type.GetCustomAttributes(typeof(HotfixSerializerAttribute)).Any()).ToList();
            if (hotfixSerializers.Count == 0)
                return;

            var hotfixSerializer = hotfixSerializers.First();

            foreach (var typeInfo in types)
            {
                foreach (var attrInfo in typeInfo.GetCustomAttributes<HotfixStructureAttribute>())
                {
                    _stores[attrInfo.Hash] = (IHotfixStore) Activator.CreateInstance(typeof (HotfixStore<>).MakeGenericType(typeInfo),
                        Activator.CreateInstance(hotfixSerializer.MakeGenericType(typeInfo)));
                }
            }
        }
    }
}

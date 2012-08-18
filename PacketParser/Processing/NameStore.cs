using System;
using System.Collections.Generic;
using PacketParser.Enums;
using PacketParser.SQL;
using Guid = PacketParser.DataStructures.Guid;
using PacketParser.Misc;
using PacketParser.DataStructures;

namespace PacketParser.Processing
{
    public class NameStore : IPacketProcessor
    {
        public bool LoadOnDepend { get { return false; } }
        public Type[] DependsOn { get { return null; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler { get { return null; } }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler { get { return null; } }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return null; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return null; } }

        public readonly Dictionary<Guid, string> PlayerNames = new Dictionary<Guid, string>();
        public readonly TimeSpanDictionary<Tuple<uint, StoreNameType>, string> EntryNames = new TimeSpanDictionary<Tuple<uint, StoreNameType>, string>();

        public bool Init(PacketFileProcessor p) { return true; }
        public void Finish() { }

        public string GetName(StoreNameType type, int entry, bool withEntry = true)
        {
            string name = "";
            {
                var k = new Tuple<uint, StoreNameType>((uint)entry, type);
                if (EntryNames.ContainsKey(k))
                    name = EntryNames[k].Item1;
            }
            if (SQLConnector.Enabled)
            {
                if (SQLDatabase.NameStores.ContainsKey(type))
                    if (!SQLDatabase.NameStores[type].TryGetValue((int)entry, out name))
                        name = "";
            }

            if (!String.IsNullOrEmpty(name))
            {
                if (withEntry)
                    return entry + " (" + name + ")";
                return name;
            }

            return entry.ToString();
        }

        public void AddPlayerName(Guid guid, string name)
        {
            if (PlayerNames.ContainsKey(guid))
                return;

            PlayerNames.Add(guid, name);
        }

        public void AddName(StoreNameType type, Int32 entry, string name, TimeSpan time)
        {
            var k = new Tuple<uint, StoreNameType>((uint)entry, type);
            EntryNames.Add(k, name, time);
        }

        public string GetPlayerName(Guid guid)
        {
            string name;

            if (PlayerNames.TryGetValue(guid, out name))
                return name;

            return null;
        }
    }
}

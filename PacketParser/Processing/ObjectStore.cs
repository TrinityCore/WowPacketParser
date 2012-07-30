using System;
using Guid = PacketParser.DataStructures.Guid;
using PacketParser.DataStructures;
using PacketParser.Misc;

namespace PacketParser.Processing
{
    public class ObjectStore : IPacketProcessor
    {
        public bool LoadOnDepend { get { return false; } }
        public Type[] DependsOn { get { return null; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler { get { return null; } }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler { get { return null; } }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return null; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return null; } }

        public readonly TimeSpanDictionary<Guid, WoWObject> Objects = new TimeSpanDictionary<Guid, WoWObject>();

        public bool Init(PacketFileProcessor p) { return true; }
        public void Finish() { }

        public WoWObject GetObjectIfFound(Guid guid)
        {
            if (Objects.ContainsKey(guid))
                return Objects[guid].Item1;
            return null;
        }
        public void AddObject(Guid guid, WoWObject obj, TimeSpan timeSpan)
        {
            Objects[guid] = new Tuple<WoWObject,TimeSpan?>(obj, timeSpan);
        }
    }
}

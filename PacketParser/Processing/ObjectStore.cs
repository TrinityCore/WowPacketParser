using System;
using Guid = PacketParser.DataStructures.Guid;
using PacketParser.DataStructures;
using PacketParser.Misc;

namespace PacketParser.Processing
{
    public class ObjectStore : IPacketProcessor
    {
        public readonly TimeSpanDictionary<Guid, WoWObject> Objects = new TimeSpanDictionary<Guid, WoWObject>();

        public bool Init(PacketFileProcessor p) { return true; }
        public void ProcessPacket(Packet packet) { }
        public void ProcessData(string name, int? index, Object obj, Type t) { }
        public void ProcessedPacket(Packet packet) { }
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

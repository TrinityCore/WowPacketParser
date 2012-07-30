using System;
using System.Collections.Generic;
using PacketParser.Enums;
using PacketParser.Enums.Version;
using PacketParser.Misc;
using PacketParser.Processing;
using PacketParser.SQL;
using PacketParser.DataStructures;

namespace PacketDumper.Processing.SQLData
{
    public class ItemTemplateStore : IPacketProcessor
    {
        public bool LoadOnDepend { get { return false; } }
        public Type[] DependsOn { get { return null; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler { get { return ProcessPacket; } }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler { get { return null; } }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return null; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return null; } }

        public readonly TimeSpanDictionary<uint, ItemTemplate> ItemTemplates = new TimeSpanDictionary<uint, ItemTemplate>();
        public bool Init(PacketFileProcessor file)
        {
            return false;
        }

        public void ProcessPacket(Packet packet)
        {
            if (packet.Status != ParsedStatus.Success)
                return;

            if (Opcode.SMSG_ITEM_QUERY_SINGLE_RESPONSE == Opcodes.GetOpcode(packet.Opcode))
            {
                var entry = packet.GetData().GetNode<KeyValuePair<int, bool>>("Entry");

                if (entry.Value) // entry is masked
                    return;

                ItemTemplates.Add((uint)entry.Key, packet.GetNode<ItemTemplate>("ItemTemplateObject"), packet.TimeSpan);
            }
        }

        public void Finish()
        {

        }

        public string Build()
        {
            if (ItemTemplates.IsEmpty())
                return String.Empty;

            var entries = ItemTemplates.Keys();
            var tempatesDb = SQLDatabase.GetDict<uint, ItemTemplate>(entries);

            return SQLUtil.CompareDicts(ItemTemplates, tempatesDb, StoreNameType.Item);
        }
    }
}

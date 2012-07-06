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
        public readonly TimeSpanDictionary<uint, ItemTemplate> ItemTemplates = new TimeSpanDictionary<uint, ItemTemplate>();
        public bool Init(PacketFileProcessor file)
        {
            return false;//return Settings.SQLOutput.HasFlag(SQLOutputFlags.ItemTemplate);
        }

        public void ProcessData(string name, int? index, Object obj, Type t)
        {
        }

        public void ProcessPacket(Packet packet)
        {
            if (Opcode.SMSG_ITEM_QUERY_SINGLE_RESPONSE == Opcodes.GetOpcode(packet.Opcode))
            {
                var entry = packet.GetData().GetNode<KeyValuePair<int, bool>>("Entry");

                if (entry.Value) // entry is masked
                    return;

                ItemTemplates.Add((uint)entry.Key, packet.GetNode<ItemTemplate>("ItemTemplateObject"), packet.TimeSpan);
            }
        }
        public void ProcessedPacket(Packet packet)
        {

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

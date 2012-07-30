using System;
using System.Collections.Generic;
using PacketParser.Enums;
using PacketParser.Enums.Version;
using PacketParser.Processing;
using PacketDumper.Enums;
using Guid = PacketParser.DataStructures.Guid;
using PacketParser.DataStructures;
using PacketParser.Misc;
using PacketDumper.Misc;
using PacketParser.SQL;
using System.Linq;

namespace PacketDumper.Processing.SQLData
{
    public class LootStore : IPacketProcessor
    {
        public bool LoadOnDepend { get { return false; } }
        public Type[] DependsOn { get { return null; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler {get {return ProcessPacket;} }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler  {get {return null;} }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return null; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return null; } }

        // Loot (ItemId, LootType)
        public readonly TimeSpanDictionary<Tuple<uint, ObjectType>, Loot> Loots = new TimeSpanDictionary<Tuple<uint, ObjectType>, Loot>();
        public bool Init(PacketFileProcessor file)
        {
            return Settings.SQLOutput.HasFlag(SQLOutputFlags.Loot);
        }

        public void ProcessPacket(Packet packet)
        {
            if (packet.Status != ParsedStatus.Success)
                return;

            if (Opcode.SMSG_LOOT_RESPONSE == Opcodes.GetOpcode(packet.Opcode) && packet.GetNode<LootType>("Loot Type") != LootType.Unk0)
            {
                var guid = packet.GetData().GetNode<Guid>("GUID");
                var loot = packet.GetNode<Loot>("LootObject");
                // Items do not have item id in its guid, we need to query the wowobject store go
                if (guid.GetObjectType() == ObjectType.Item)
                {
                    WoWObject item = PacketFileProcessor.Current.GetProcessor<ObjectStore>().GetObjectIfFound(guid);
                    UpdateField itemEntry;
                    if (item != null)
                        if (item.UpdateFields.TryGetValue((int)UpdateFields.GetUpdateFieldOffset(ObjectField.OBJECT_FIELD_ENTRY), out itemEntry))
                        {
                            Loots.Add(new Tuple<uint, ObjectType>(itemEntry.UInt32Value, guid.GetObjectType()), loot, packet.TimeSpan);
                            return;
                        }
                }

                Loots.Add(new Tuple<uint, ObjectType>(guid.GetEntry(), guid.GetObjectType()), loot, packet.TimeSpan);
            }
        }

        public void Finish()
        {

        }

        public string Build()
        {
            if (Loots.IsEmpty())
                return String.Empty;

            // Not TDB structure
            const string tableName = "LootTemplate";
            var names = PacketFileProcessor.Current.GetProcessor<NameStore>();
            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var loot in Loots)
            {
                var comment = new QueryBuilder.SQLInsertRow();
                comment.HeaderComment =
                    names.GetName(Utilities.ObjectTypeToStore(Loots.Keys().First().Item2), (int)loot.Key.Item1, false) +
                    " (" + loot.Value.Item1.Gold + " gold)";
                rows.Add(comment);
                foreach (var lootItem in loot.Value.Item1.LootItems)
                {
                    var row = new QueryBuilder.SQLInsertRow();
                    row.AddValue("Id", loot.Key.Item1);
                    row.AddValue("Type", loot.Key.Item2);
                    row.AddValue("ItemId", lootItem.ItemId);
                    row.AddValue("Count", lootItem.Count);
                    row.Comment = names.GetName(StoreNameType.Item, (int)lootItem.ItemId, false);

                    rows.Add(row);
                }
            }

            return new QueryBuilder.SQLInsert(tableName, rows, 2).Build();
        }
    }
}

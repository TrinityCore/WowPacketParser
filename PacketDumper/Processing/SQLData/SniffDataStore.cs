using System;
using System.Collections.Generic;
using PacketParser.Misc;
using PacketParser.Enums;
using PacketParser.Enums.Version;
using PacketParser.Processing;
using PacketDumper.Enums;
using Guid = PacketParser.DataStructures.Guid;
using PacketDumper.Misc;
using PacketDumper.DataStructures;
using PacketParser.DataStructures;
using PacketParser.SQL;

namespace PacketDumper.Processing
{
    public class SniffDataStore : IPacketProcessor
    {
        public bool LoadOnDepend { get { return false; } }
        public Type[] DependsOn { get { return null; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler { get { return ProcessPacket; } }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler { get { return null; } }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return null; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return null; } }

        private static readonly bool SniffData = Settings.SQLOutput.HasAnyFlag(SQLOutputFlags.SniffData);
        private static readonly bool SniffDataOpcodes = Settings.SQLOutput.HasAnyFlag(SQLOutputFlags.SniffDataOpcodes);
        public static readonly TimeSpanBag<SniffData> SniffDatas = new TimeSpanBag<SniffData>();
        public bool Init(PacketFileProcessor file)
        {
            return SniffData;
        }

        public void ProcessPacket(Packet packet)
        {
            if (packet.Status != ParsedStatus.Success)
                return;

            switch (Opcodes.GetOpcode(packet.Opcode))
            {
                case Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE:
                    {
                    var entry = packet.GetData().GetNode<KeyValuePair<int, bool>>("Entry");
                    if (!entry.Value)
                        AddSniffData(packet, StoreNameType.GameObject, entry.Key, "QUERY_RESPONSE");
                    break;
                    }
                case Opcode.SMSG_CREATURE_QUERY_RESPONSE:
                    {
                    var entry = packet.GetData().GetNode<KeyValuePair<int, bool>>("Entry");
                    if (!entry.Value)
                        AddSniffData(packet, StoreNameType.Unit, entry.Key, "QUERY_RESPONSE");
                    break;
                    }
                case Opcode.SMSG_UPDATE_OBJECT:
                    {
                        var updates = packet.GetData().GetNode<IndexedTreeNode>("Updates");
                        foreach (var update in updates)
                        {
                            Object typeObj;
                            if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
                                typeObj = update.Value.GetNode<UpdateTypeCataclysm>("UpdateType");
                            else
                                typeObj = update.Value.GetNode<UpdateType>("UpdateType");
                            if (typeObj.ToString().Contains("Create"))
                            {
                                var guid = update.Value.GetNode<Guid>("GUID");
                                var objType = update.Value.GetNode<ObjectType>("Object Type");
                                if (guid.HasEntry() && (objType == ObjectType.Unit || objType == ObjectType.GameObject))
                                    AddSniffData(packet, Utilities.ObjectTypeToStore(objType), (int)guid.GetEntry(), "SPAWN");
                            }
                        }
                    }
                    break;
                case Opcode.SMSG_PAGE_TEXT_QUERY_RESPONSE:
                    {
                    var entry = packet.GetData().GetNode<UInt32>("Entry");
                    AddSniffData(packet, StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
                    break;
                    }
                case Opcode.SMSG_GOSSIP_MESSAGE:
                    {
                    var menuId = packet.GetData().GetNode<UInt32>("Menu Id");
                    var guid = packet.GetData().GetNode<Guid>("GUID");
                    AddSniffData(packet, StoreNameType.Gossip, (int)menuId, guid.GetEntry().ToString());
                    }
                    break;
                case Opcode.SMSG_ITEM_QUERY_SINGLE_RESPONSE:
                    {
                    var entry = packet.GetData().GetNode<KeyValuePair<int, bool>>("Entry");
                    if (!entry.Value)
                        AddSniffData(packet, StoreNameType.Item, entry.Key, "QUERY_RESPONSE");
                    break;
                    }
                case Opcode.SMSG_DB_REPLY:
                    {
                    var itemId = packet.GetData().GetNode<UInt32>("Entry");
                    AddSniffData(packet, StoreNameType.Item, (int)itemId, "DB_REPLY");
                    break;
                    }
                case Opcode.CMSG_LOAD_SCREEN:
                    {
                    var mapId = packet.GetData().GetNode<UInt32>("Map");
                    AddSniffData(packet, StoreNameType.Map, (int)mapId, "LOAD_SCREEN");
                    break;
                    }
                case Opcode.SMSG_NEW_WORLD:
                case Opcode.SMSG_LOGIN_VERIFY_WORLD:
                    {
                    var mapId = packet.GetData().GetNode<UInt32>("Map");
                    AddSniffData(packet, StoreNameType.Map, (int)mapId, "NEW_WORLD");
                    break;
                    }
                case Opcode.SMSG_SET_PHASE_SHIFT:
                    {
                    Int32 phaseMask;
                    if (packet.TryGetNode<Int32>(out phaseMask, "Phase Mask"))
                        AddSniffData(packet, StoreNameType.Phase, phaseMask, "PHASEMASK");
                    break;
                    }
                case Opcode.SMSG_NPC_TEXT_UPDATE:
                    {
                    var entry = packet.GetData().GetNode<KeyValuePair<int, bool>>("Entry");
                    if (!entry.Value)
                        AddSniffData(packet, StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");
                    break;
                    }
                case Opcode.SMSG_QUEST_QUERY_RESPONSE:
                    {
                    var id = packet.GetData().GetNode<KeyValuePair<int, bool>>("Quest ID");
                    if (!id.Value)
                        AddSniffData(packet, StoreNameType.Quest, id.Key, "QUERY_RESPONSE");
                    break;
                    }
                case Opcode.SMSG_AURA_UPDATE_ALL:
                case Opcode.SMSG_AURA_UPDATE:
                    {
                    var auras = packet.GetData().GetNode<IndexedTreeNode>("Auras");
                    foreach (var aura in auras)
                    {
                        var spellId = aura.Value.GetNode<NamedTreeNode>("Aura").GetNode<Int32>("Spell ID");
                        AddSniffData(packet, StoreNameType.Spell, spellId, "AURA_UPDATE");
                    }
                    break;
                    }
                case Opcode.SMSG_SPELL_GO:
                    {
                    var spellId = packet.GetData().GetNode<Int32>("Spell ID");
                    AddSniffData(packet, StoreNameType.Spell, spellId, "SPELL_GO");
                    break;
                    }
            }

            if (SniffDataOpcodes)
            {
                var data = packet.Status == ParsedStatus.Success ? Opcodes.GetOpcodeName(packet.Opcode) : packet.Status.ToString();
                AddSniffData(packet, StoreNameType.Opcode, packet.Opcode, data);
            }
        }

        public void Finish()
        {

        }

        private void AddSniffData(Packet packet, StoreNameType type, int id, string data)
        {
            if (id == 0 && type != StoreNameType.Map)
                return; // Only maps can have id 0

            var item = new DataStructures.SniffData
            {
                FileName = packet.FileName,
                ObjectType = type,
                Id = id,
                Data = data,
            };

            SniffDatas.Add(item, packet.TimeSpan);
        }

        public string Build()
        {
            if (SniffDatas.IsEmpty())
                return String.Empty;

            const string tableName = "SniffData";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var data in SniffDatas)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("Build", ClientVersion.Build);
                row.AddValue("SniffName", data.Item1.FileName);
                row.AddValue("ObjectType", data.Item1.ObjectType.ToString());
                row.AddValue("Id", data.Item1.Id);
                row.AddValue("Data", data.Item1.Data);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, rows, ignore: true, withDelete: false, deleteDuplicates: true).Build();
        }
    }
}

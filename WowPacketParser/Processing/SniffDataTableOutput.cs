using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowPacketParser.Misc;
using WowPacketParser.Enums;
using WowPacketParser.Loading;
using WowPacketParser.Store;
using WowPacketParser.Enums.Version;

namespace WowPacketParser.Processing
{
    public class SniffDataTableOutput : IPacketProcessor
    {
        private static readonly bool SniffData = Settings.SQLOutput.HasAnyFlag(SQLOutputFlags.SniffData);
        private static readonly bool SniffDataOpcodes = Settings.SQLOutput.HasAnyFlag(SQLOutputFlags.SniffDataOpcodes);

        public bool Init(SniffFile file)
        {
            return SniffData;
        }

        public void ProcessData(string name, int? index, Object obj, Type t) 
        {
        }

        public void ProcessPacket(Packet packet)
        {
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
                                var guid = update.Value.GetNode<Misc.Guid>("GUID");
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
                    var guid = packet.GetData().GetNode<Misc.Guid>("GUID");
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
        public void ProcessedPacket(Packet packet)
        {

        }

        public void Finish()
        {

        }

        private void AddSniffData(Packet packet, StoreNameType type, int id, string data)
        {
            if (id == 0 && type != StoreNameType.Map)
                return; // Only maps can have id 0

            var item = new Store.Objects.SniffData
            {
                FileName = packet.FileName,
                TimeStamp = Utilities.GetUnixTimeFromDateTime(packet.Time),
                ObjectType = type,
                Id = id,
                Data = data,
                Number = packet.Number,
            };

            Storage.SniffData.Add(item, packet.TimeSpan);
        }
    }
}

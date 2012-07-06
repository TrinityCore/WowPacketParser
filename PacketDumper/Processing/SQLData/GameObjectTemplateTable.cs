using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Processing;
using WowPacketParser.Store.Objects;

namespace PacketDumper.Processing.SQLData
{
    public class SniffDataTableOutput : IPacketProcessor
    {
        public readonly TimeSpanDictionary<uint, GameObjectTemplate> GameObjectTemplates = new TimeSpanDictionary<uint, GameObjectTemplate>();
        public bool Init(PacketFileProcessor file)
        {
            return Settings.SQLOutput.HasFlag(SQLOutputFlags.GameObjectTemplate);
        }

        public void ProcessData(string name, int? index, Object obj, Type t)
        {
        }

        public void ProcessPacket(Packet packet)
        {
            if (Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE == Opcodes.GetOpcode(packet.Opcode))
            {
                var gameObject = new GameObjectTemplate();
                var entry = packet.GetData().GetNode<KeyValuePair<int, bool>>("Entry");

                if (entry.Value) // entry is masked
                    return;

                gameObject.Type = packet.GetNode<GameObjectType>("Type");
                gameObject.DisplayId = packet.GetNode<UInt32>("Display ID");

                gameObject.Name = packet.GetNode<string>("Names", "0", "Name");

                gameObject.IconName = packet.GetNode<string>("Icon Name");
                gameObject.CastCaption = packet.GetNode<string>("Cast Caption");
                gameObject.UnkString = packet.GetNode<string>("Unk String");

                packet.StoreBeginList("Data");
                var data = packet.GetNode<IndexedTreeNode>("Data");
                gameObject.Data = new int[ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596) ? 32 : 24];
                foreach (var d in data)
                {
                    gameObject.Data[d.Key] = d.Value.GetNode<Int32>("Data");
                }

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056)) // not sure when it was added exactly - did not exist in 2.4.1 sniff
                    gameObject.Size = packet.GetNode<Single>("Size");

                gameObject.QuestItems = new uint[ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192) ? 6 : 4];
                var questItems = packet.GetNode<IndexedTreeNode>("Quest Items");
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                {
                    foreach (var d in questItems)
                    {
                        gameObject.QuestItems[d.Key] = (uint)d.Value.GetNode<Int32>("Quest Item");
                    }
                }

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596))
                    gameObject.UnknownUInt = packet.GetNode<UInt32>("Unknown UInt32");

                GameObjectTemplates.Add((uint)entry.Key, gameObject, packet.TimeSpan);
            }
        }
        public void ProcessedPacket(Packet packet)
        {

        }

        public void Finish()
        {

        }
    }
}

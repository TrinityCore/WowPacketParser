using System;
using System.Collections.Generic;
using PacketParser.Enums;
using PacketParser.Enums.Version;
using PacketParser.Misc;
using PacketParser.Processing;
using PacketParser.SQL;
using PacketDumper.Enums;
using PacketDumper.Misc;
using PacketParser.DataStructures;

namespace PacketDumper.Processing.SQLData
{
    public class GameObjectTemplateStore : IPacketProcessor
    {
        public bool LoadOnDepend { get { return false; } }
        public Type[] DependsOn { get { return null; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler { get { return ProcessPacket; } }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler { get { return null; } }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return null; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return null; } }

        public readonly TimeSpanDictionary<uint, GameObjectTemplate> GameObjectTemplates = new TimeSpanDictionary<uint, GameObjectTemplate>();
        public bool Init(PacketFileProcessor file)
        {
            return Settings.SQLOutput.HasFlag(SQLOutputFlags.GameObjectTemplate);
        }

        public void ProcessPacket(Packet packet)
        {
            if (packet.Status != ParsedStatus.Success)
                return;

            if (Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE == Opcodes.GetOpcode(packet.Opcode))
            {
                var entry = packet.GetData().GetNode<KeyValuePair<int, bool>>("Entry");

                if (entry.Value) // entry is masked
                    return;

                GameObjectTemplates.Add((uint)entry.Key, packet.GetNode<GameObjectTemplate>("GameObjectTemplateObject"), packet.TimeSpan);
            }
        }

        public void Finish()
        {

        }

        public string Build()
        {
            if (GameObjectTemplates.IsEmpty())
                return String.Empty;

            if (!GameObjectTemplates.IsEmpty())
                foreach (var obj in GameObjectTemplates)
                    obj.Value.Item1.WDBVerified = ClientVersion.BuildInt;

            var entries = GameObjectTemplates.Keys();
            var tempatesDb = SQLDatabase.GetDict<uint, GameObjectTemplate>(entries);

            return SQLUtil.CompareDicts(GameObjectTemplates, tempatesDb, StoreNameType.GameObject);
        }
    }
}

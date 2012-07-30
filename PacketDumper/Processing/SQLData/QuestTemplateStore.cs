using System;
using System.Collections.Generic;
using PacketParser.Enums;
using PacketParser.Enums.Version;
using PacketParser.Misc;
using PacketParser.Processing;
using PacketParser.SQL;
using PacketDumper.Enums;
using PacketParser.DataStructures;
using PacketDumper.Misc;

namespace PacketDumper.Processing.SQLData
{
    public class QuestTemplateStore : IPacketProcessor
    {
        public bool LoadOnDepend { get { return false; } }
        public Type[] DependsOn { get { return null; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler { get { return ProcessPacket; } }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler { get { return null; } }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return null; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return null; } }

        public readonly TimeSpanDictionary<uint, QuestTemplate> QuestTemplates = new TimeSpanDictionary<uint, QuestTemplate>();
        public bool Init(PacketFileProcessor file)
        {
            return Settings.SQLOutput.HasFlag(SQLOutputFlags.QuestTemplate);
        }

        public void ProcessPacket(Packet packet)
        {
            if (packet.Status != ParsedStatus.Success)
                return;

            if (Opcode.SMSG_QUEST_QUERY_RESPONSE == Opcodes.GetOpcode(packet.Opcode))
            {
                var entry = packet.GetData().GetNode<KeyValuePair<int, bool>>("Quest ID");

                if (entry.Value) // entry is masked
                    return;

                QuestTemplates.Add((uint)entry.Key, packet.GetNode<QuestTemplate>("QuestTemplateObject"), packet.TimeSpan);
            }
        }

        public void Finish()
        {

        }

        public string Build()
        {
            if (QuestTemplates.IsEmpty())
                return String.Empty;

            if (!QuestTemplates.IsEmpty())
                foreach (var obj in QuestTemplates)
                    obj.Value.Item1.WDBVerified = ClientVersion.BuildInt;

            var entries = QuestTemplates.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, QuestTemplate>(entries, "Id");

            return SQLUtil.CompareDicts(QuestTemplates, templatesDb, StoreNameType.Quest, "Id");
        }
    }
}

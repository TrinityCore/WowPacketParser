using System;
using PacketParser.Misc;
using PacketParser.Enums;
using PacketParser.Enums.Version;
using PacketParser.Processing;
using PacketDumper.Enums;
using PacketParser.SQL;
using PacketDumper.Misc;
using PacketDumper.DataStructures;
using PacketParser.DataStructures;

namespace PacketDumper.Processing.SQLData
{
    public class QuestRewardStore : IPacketProcessor
    {
        public bool LoadOnDepend { get { return false; } }
        public Type[] DependsOn { get { return null; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler { get { return ProcessPacket; } }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler { get { return null; } }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return null; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return null; } }

        public readonly TimeSpanDictionary<uint, QuestReward> QuestRewards = new TimeSpanDictionary<uint, QuestReward>();
        public bool Init(PacketFileProcessor file)
        {
            return Settings.SQLOutput.HasFlag(SQLOutputFlags.QuestTemplate);
        }

        public void ProcessPacket(Packet packet)
        {
            if (packet.Status != ParsedStatus.Success)
                return;

            if (Opcodes.GetOpcode(packet.Opcode) == Opcode.SMSG_QUESTGIVER_REQUEST_ITEMS)
            {
                var entry = packet.GetData().GetNode<UInt32>("Quest ID");

                QuestRewards.Add((uint)entry, new QuestReward { RequestItemsText = packet.GetData().GetNode<String>("Text") }, null);
            }
        }

        public void Finish()
        {

        }

        public string Build()
        {
            if (QuestRewards.IsEmpty())
                return String.Empty;

            var entries = QuestRewards.Keys();
            var rewardDb = SQLDatabase.GetDict<uint, QuestReward>(entries, "Id");

            return SQLUtil.CompareDicts(QuestRewards, rewardDb, StoreNameType.Quest, "Id");
        }
    }
}

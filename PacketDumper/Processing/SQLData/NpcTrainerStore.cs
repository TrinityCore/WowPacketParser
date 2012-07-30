using System;
using System.Collections.Generic;
using PacketParser.Enums;
using PacketParser.Enums.Version;
using PacketParser.Misc;
using PacketParser.Processing;
using PacketDumper.Enums;
using Guid = PacketParser.DataStructures.Guid;
using PacketDumper.Misc;
using PacketParser.DataStructures;
using PacketParser.SQL;

namespace PacketDumper.Processing.SQLData
{
    public class NpcTrainerStore : IPacketProcessor
    {
        public bool LoadOnDepend { get { return false; } }
        public Type[] DependsOn { get { return null; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler { get { return ProcessPacket; } }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler { get { return null; } }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return null; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return null; } }

        public readonly TimeSpanDictionary<uint, NpcTrainer> NpcTrainers = new TimeSpanDictionary<uint, NpcTrainer>();
        public bool Init(PacketFileProcessor file)
        {
            return Settings.SQLOutput.HasFlag(SQLOutputFlags.NpcTrainer);
        }

        public void ProcessPacket(Packet packet)
        {
            if (packet.Status != ParsedStatus.Success)
                return;

            if (Opcode.SMSG_TRAINER_LIST == Opcodes.GetOpcode(packet.Opcode))
            {
                var guid = packet.GetData().GetNode<Guid>("GUID");

                NpcTrainers.Add((uint)guid.GetEntry(), packet.GetNode<NpcTrainer>("NpcTrainerObject"), packet.TimeSpan);
            }
        }

        public void Finish()
        {

        }

        public string Build()
        {
            if (NpcTrainers.IsEmpty())
                return String.Empty;

            const string tableName = "npc_trainer";
            var names = PacketFileProcessor.Current.GetProcessor<NameStore>();
            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var npcTrainer in NpcTrainers)
            {
                var comment = new QueryBuilder.SQLInsertRow();
                comment.HeaderComment = names.GetName(StoreNameType.Unit, (int)npcTrainer.Key, false);
                rows.Add(comment);
                foreach (var trainerSpell in npcTrainer.Value.Item1.TrainerSpells)
                {
                    var row = new QueryBuilder.SQLInsertRow();
                    row.AddValue("entry", npcTrainer.Key);
                    row.AddValue("spell", trainerSpell.Spell);
                    row.AddValue("spellcost", trainerSpell.Cost);
                    row.AddValue("reqskill", trainerSpell.RequiredSkill);
                    row.AddValue("reqskillvalue", trainerSpell.RequiredSkillLevel);
                    row.AddValue("reqlevel", trainerSpell.RequiredLevel);
                    row.Comment = names.GetName(StoreNameType.Spell, (int)trainerSpell.Spell, false);
                    rows.Add(row);
                }
            }

            return new QueryBuilder.SQLInsert(tableName, rows).Build();
        }
    }
}

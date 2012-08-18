using System;
using PacketParser.Misc;
using PacketParser.Enums;
using PacketParser.Enums.Version;
using PacketParser.Processing;
using PacketDumper.Enums;
using Guid = PacketParser.DataStructures.Guid;
using System.Collections.Generic;
using PacketParser.SQL;
using PacketDumper.Misc;
using PacketDumper.DataStructures;
using PacketParser.DataStructures;

namespace PacketDumper.Processing.SQLData
{
    public class CreatureSpellsXStore : IPacketProcessor
    {
        public bool LoadOnDepend { get { return false; } }
        public Type[] DependsOn { get { return null; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler { get { return ProcessPacket; } }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler { get { return null; } }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return null; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return null; } }

        public readonly TimeSpanDictionary<uint, SpellsX> Spells = new TimeSpanDictionary<uint, SpellsX>(); // `creature_template`.`spellsX`
        public bool Init(PacketFileProcessor file)
        {
            return Settings.SQLOutput.HasFlag(SQLOutputFlags.CreatureTemplate);
        }

        public void ProcessPacket(Packet packet)
        {
            if (packet.Status != ParsedStatus.Success)
                return;

            if (Opcode.SMSG_PET_SPELLS == Opcodes.GetOpcode(packet.Opcode))
            {
                var guid = packet.GetData().GetNode<Guid>("GUID");
                if (guid.Full == 0)
                    return;

                // don't store spells for pets
                if (guid.GetHighType() != HighGuidType.Vehicle && guid.GetHighType() != HighGuidType.Unit)
                    return;

                var store = new List<uint>();
                var spells = packet.GetData().GetNode<IndexedTreeNode>("Spells/Actions");
                foreach (var s in spells)
                {
                    StoreEntry spell;
                    if (s.Value.TryGetNode<StoreEntry>(out spell, "Spell"))
                        store.Add((uint)spell._data);
                }

                SpellsX spellsCr;
                spellsCr.Spells = store.ToArray();
                Spells.Add(guid.GetEntry(), spellsCr, packet.TimeSpan);
            }
        }

        public void Finish()
        {

        }

        public string Build()
        {
            if (Spells.IsEmpty())
                return String.Empty;

            var entries = Spells.Keys();
            var spellsXDb = SQLDatabase.GetDict<uint, SpellsX>(entries);

            return SQLUtil.CompareDicts(Spells, spellsXDb, StoreNameType.Unit);
        }
    }
}

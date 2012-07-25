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
    public class CreatureTemplateStore : IPacketProcessor
    {
        public readonly TimeSpanDictionary<uint, UnitTemplate> UnitTemplates = new TimeSpanDictionary<uint, UnitTemplate>();
        public bool Init(PacketFileProcessor file)
        {
            return Settings.SQLOutput.HasFlag(SQLOutputFlags.CreatureTemplate);
        }

        public void ProcessData(string name, int? index, Object obj, Type t, TreeNodeEnumerator constIter)
        {
        }

        public void ProcessPacket(Packet packet)
        {
            if (Opcode.SMSG_CREATURE_QUERY_RESPONSE == Opcodes.GetOpcode(packet.Opcode))
            {
                var entry = packet.GetData().GetNode<KeyValuePair<int, bool>>("Entry");

                if (entry.Value) // entry is masked
                    return;

                UnitTemplates.Add((uint)entry.Key, packet.GetNode<UnitTemplate>("UnitTemplateObject"), packet.TimeSpan);
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
            if (UnitTemplates.IsEmpty())
                return String.Empty;

            if (!UnitTemplates.IsEmpty())
                foreach (var obj in UnitTemplates)
                    obj.Value.Item1.WDBVerified = ClientVersion.BuildInt;

            var entries = UnitTemplates.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, UnitTemplate>(entries);

            return SQLUtil.CompareDicts(UnitTemplates, templatesDb, StoreNameType.Unit);
        }
    }
}

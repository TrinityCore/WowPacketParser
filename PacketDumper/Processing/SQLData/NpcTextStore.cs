using System;
using PacketParser.Misc;
using PacketParser.Enums;
using PacketParser.Enums.Version;
using PacketParser.Processing;
using PacketDumper.Enums;
using System.Collections.Generic;
using PacketParser.DataStructures;
using PacketDumper.Misc;
using PacketParser.SQL;

namespace PacketDumper.Processing.SQLData
{
    public class NpcTextStore : IPacketProcessor
    {
        public bool LoadOnDepend { get { return false; } }
        public Type[] DependsOn { get { return null; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler { get { return ProcessPacket; } }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler { get { return null; } }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return null; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return null; } }

        public readonly TimeSpanDictionary<uint, NpcText> NpcTexts = new TimeSpanDictionary<uint, NpcText>();
        public bool Init(PacketFileProcessor file)
        {
            return Settings.SQLOutput.HasFlag(SQLOutputFlags.NpcText);
        }

        public void ProcessPacket(Packet packet)
        {
            if (packet.Status != ParsedStatus.Success)
                return;

            if (Opcode.SMSG_NPC_TEXT_UPDATE == Opcodes.GetOpcode(packet.Opcode))
            {
                var entry = packet.GetData().GetNode<KeyValuePair<int, bool>>("Entry");

                if (entry.Value) // entry is masked
                    return;

                NpcTexts.Add((uint)entry.Key, packet.GetData().GetNode<NpcText>("NpcTextObject"), packet.TimeSpan);
            }
        }

        public void Finish()
        {

        }

        public string Build()
        {
            if (NpcTexts.IsEmpty())
                return String.Empty;

            if (!NpcTexts.IsEmpty())
                foreach (var obj in NpcTexts)
                    obj.Value.Item1.WDBVerified = ClientVersion.BuildInt;

            foreach (var npcText in NpcTexts)
                npcText.Value.Item1.ConvertToDBStruct();

            var entries = NpcTexts.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, NpcText>(entries, "ID");

            return SQLUtil.CompareDicts(NpcTexts, templatesDb, StoreNameType.NpcText, "ID");
        }
    }
}

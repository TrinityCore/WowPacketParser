using System;
using PacketParser.Misc;
using PacketParser.Enums;
using PacketParser.Enums.Version;
using PacketParser.Processing;
using PacketDumper.Enums;
using PacketParser.SQL;
using PacketDumper.Misc;
using PacketParser.DataStructures;

namespace PacketDumper.Processing.SQLData
{
    public class PageTextStore : IPacketProcessor
    {
        public bool LoadOnDepend { get { return false; } }
        public Type[] DependsOn { get { return null; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler { get { return ProcessPacket; } }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler { get { return null; } }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return null; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return null; } }

        public readonly TimeSpanDictionary<uint, PageText> PageTexts = new TimeSpanDictionary<uint, PageText>();
        public bool Init(PacketFileProcessor file)
        {
            return Settings.SQLOutput.HasFlag(SQLOutputFlags.PageText);
        }

        public void ProcessPacket(Packet packet)
        {
            if (packet.Status != ParsedStatus.Success)
                return;

            if (Opcode.SMSG_PAGE_TEXT_QUERY_RESPONSE == Opcodes.GetOpcode(packet.Opcode))
            {
                var entry = packet.GetData().GetNode<UInt32>("Entry");

                PageTexts.Add(entry, packet.GetData().GetNode<PageText>("PageTextObject"), packet.TimeSpan);
            }
        }

        public void Finish()
        {

        }

        public string Build()
        {
            if (PageTexts.IsEmpty())
                return String.Empty;

            if (!PageTexts.IsEmpty())
                foreach (var obj in PageTexts)
                    obj.Value.Item1.WDBVerified = ClientVersion.BuildInt;

            var entries = PageTexts.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, PageText>(entries);

            return SQLUtil.CompareDicts(PageTexts, templatesDb, StoreNameType.PageText);
        }
    }
}

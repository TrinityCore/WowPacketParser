using System;
using PacketParser.Processing;
using PacketParser.DataStructures;
using PacketViewer.Forms;
using System.Collections.Generic;
using PacketViewer.DataStructures;
using PacketParser.Enums;
using PacketParser.Misc;
using PacketParser.Enums.Version;

namespace PacketViewer.Processing
{
    public class TableOutput : IPacketProcessor
    {
        public bool LoadOnDepend { get { return false; } }
        public Type[] DependsOn { get { return null; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler { get { return ProcessPacket; } }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler { get { return null; } }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return null; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return null; } }

        private PacketFileTab Tab;

        private const int PacketBufferSize = 500;

        public bool Init(PacketFileProcessor proc)
        {
            Tab = ((PacketFileViewer)proc).Tab;
            return true;
        }

        public void Finish()
        {
            AddPackets();
            packets = null;
            Tab = null;
        }

        List<PacketEntry> packets = new List<PacketEntry>(PacketBufferSize);

        public void ProcessPacket(Packet packet)
        {
            var packetEntry = new PacketEntry
            {
                Number = (uint)packet.Number,
                SubPacketNumber = packet.SubPacketNumber,
                UID = packet.ParseID,
                Dir = (packet.Direction == Direction.ClientToServer) ? "C->S" : "S->C",
                Length = (ushort)packet.Length,
                Sec = (uint)packet.TimeSpan.TotalSeconds,
                Time = packet.Time,
                Opcode = (ushort)packet.Opcode,
                OpcodeString = Opcodes.GetOpcodeName(packet.Opcode),
                Status = Enum<ParsedStatus>.ToString(packet.Status)+" "+packet.ErrorMessage,
            };

            packets.Add(packetEntry);
            if (packets.Count == PacketBufferSize + 1)
            {
                AddPackets();
                packets.Clear();
            }
        }

        delegate void AddPacketCallback(List<PacketEntry> packets);

        public void AddPackets()
        {
            if (Tab.InvokeRequired)
            {
                AddPacketCallback d = new AddPacketCallback(Tab.AddPackets);
                Tab.Invoke(d, new object[] { packets });
            }
            else
            {
                Tab.AddPackets(packets);
            }
        }
    }
}

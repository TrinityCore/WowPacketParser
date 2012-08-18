using System;
using PacketParser.Misc;
using PacketParser.Enums;
using PacketParser.Enums.Version;
using PacketParser.Processing;
using PacketDumper.Enums;
using PacketDumper.Misc;
using PacketParser.DataStructures;

namespace PacketDumper.Processing.SQLData
{
    public class NpcSoundStore : IPacketProcessor
    {
        public bool LoadOnDepend { get { return false; } }
        public Type[] DependsOn { get { return null; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler { get { return ProcessPacket; } }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler { get { return null; } }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return null; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return null; } }

        public readonly TimeSpanBag<uint> Sounds = new TimeSpanBag<uint>();
        public bool Init(PacketFileProcessor file)
        {
            return Settings.SQLOutput.HasFlag(SQLOutputFlags.CreatureTemplate);
        }

        public void ProcessPacket(Packet packet)
        {
            if (packet.Status != ParsedStatus.Success)
                return;

            switch(Opcodes.GetOpcode(packet.Opcode))
            {
                case Opcode.SMSG_PLAY_SOUND:
                case Opcode.SMSG_PLAY_MUSIC:
                case Opcode.SMSG_PLAY_OBJECT_SOUND:
                    Sounds.Add(packet.GetData().GetNode<UInt32>("Sound Id"), packet.TimeSpan);
                    break;
            }
        }

        public void Finish()
        {

        }

    }
}

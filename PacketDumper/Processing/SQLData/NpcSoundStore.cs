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
        public readonly TimeSpanBag<uint> Sounds = new TimeSpanBag<uint>();
        public bool Init(PacketFileProcessor file)
        {
            return Settings.SQLOutput.HasFlag(SQLOutputFlags.CreatureTemplate);
        }

        public void ProcessData(string name, int? index, Object obj, Type t, TreeNodeEnumerator constIter)
        {

        }

        public void ProcessPacket(Packet packet)
        {
            switch(Opcodes.GetOpcode(packet.Opcode))
            {
                case Opcode.SMSG_PLAY_SOUND:
                case Opcode.SMSG_PLAY_MUSIC:
                case Opcode.SMSG_PLAY_OBJECT_SOUND:
                    Sounds.Add(packet.GetData().GetNode<UInt32>("Sound Id"), packet.TimeSpan);
                    break;
            }
        }
        public void ProcessedPacket(Packet packet)
        {

        }

        public void Finish()
        {

        }

    }
}

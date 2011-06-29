using System;
using System.Collections.Generic;
using System.IO;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading.Loaders
{
    [Loader("wlp")]
    public sealed class WlpLoader : Loader
    {
        public WlpLoader(string file)
            : base(file)
        {
        }

        public override IEnumerable<Packet> ParseFile()
        {
            var bin = new BinaryReader(new FileStream(FileToParse, FileMode.Open));
            var packets = new List<Packet>();

            bin.ReadBytes(3);
            bin.ReadBytes(2);
            bin.ReadByte();
            bin.ReadInt16();
            bin.ReadBytes(4);
            bin.ReadBytes(20);
            bin.ReadBytes(64);

            while (bin.BaseStream.Position != bin.BaseStream.Length)
            {
                var direction = (bin.ReadByte() != 0xFF ? Direction.ClientToServer :
                    Direction.ServerToClient);
                var time = Utilities.GetDateTimeFromUnixTime(bin.ReadInt32());
                bin.ReadInt32();
                var length = bin.ReadInt32();
                var opcode = (Opcode)(direction == Direction.ClientToServer ? bin.ReadInt32() :
                    bin.ReadInt16());
                var data = bin.ReadBytes(length - (direction == Direction.ClientToServer ? 4 : 2));

                var packet = new Packet(data, opcode, time, direction);
                packets.Add(packet);
            }

            return packets;
        }
    }
}

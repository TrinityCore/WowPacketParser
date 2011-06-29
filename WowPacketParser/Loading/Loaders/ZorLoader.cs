using System.Collections.Generic;
using System.IO;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading.Loaders
{
    [Loader("zor")]
    public sealed class ZorLoader : Loader
    {
        public ZorLoader(string file)
            : base(file)
        {
        }

        public override IEnumerable<Packet> ParseFile()
        {
            var bin = new BinaryReader(new FileStream(FileToParse, FileMode.Open));
            var packets = new List<Packet>();

            while (bin.BaseStream.Position != bin.BaseStream.Length)
            {
                var type = bin.ReadByte();
                var opcode = (Opcode)bin.ReadInt16();
                var length = bin.ReadInt32();
                var direction = bin.ReadBoolean() ? Direction.ClientToServer : Direction.ServerToClient;
                var time = Utilities.GetDateTimeFromUnixTime(bin.ReadInt32());
                var data = bin.ReadBytes(length);

                if (type != 1)
                    continue;

                var packet = new Packet(data, opcode, time, direction);
                packets.Add(packet);
            }

            return packets;
        }
    }
}

using System.Collections.Generic;
using System.IO;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading
{
    public sealed class ZorReader : IPacketReader
    {
        private readonly BinaryReader _reader;

        public ZorReader(string file)
        {
            _reader = new BinaryReader(new FileStream(file, FileMode.Open));
        }

        public bool CanRead()
        {
            return _reader.BaseStream.Position != _reader.BaseStream.Length;
        }

        public Packet Read(int number, string fileName)
        {
            var type = _reader.ReadByte();
            var opcode = _reader.ReadInt16();
            var length = _reader.ReadInt32();
            var direction = _reader.ReadBoolean() ? Direction.ClientToServer : Direction.ServerToClient;
            var time = Utilities.GetDateTimeFromUnixTime(_reader.ReadInt32());
            var data = _reader.ReadBytes(length);

            if (type != 1)
                return null;

            return new Packet(data, opcode, time, direction, number, fileName);
        }

        public void Dispose()
        {
            if (_reader != null)
            {
                _reader.BaseStream.Dispose();
                _reader.Dispose();
            }
        }
    }
}


using System.Collections.Generic;
using System.IO;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading
{
    public sealed class KSnifferZorReader : IPacketReader
    {
        private readonly BinaryReader _reader;

        public KSnifferZorReader(string file)
        {
            _reader = new BinaryReader(new FileStream(file, FileMode.Open));
        }

        public bool CanRead()
        {
            return _reader.BaseStream.Position != _reader.BaseStream.Length;
        }

        public Packet Read(int number, string fileName)
        {
            var opcode = _reader.ReadInt32();
            var length = _reader.ReadInt32();
            var time = Utilities.GetDateTimeFromUnixTime(_reader.ReadInt32());
            var direction = (Direction)_reader.ReadChar();
            var data = _reader.ReadBytes(length);

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


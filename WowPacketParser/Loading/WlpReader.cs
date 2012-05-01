using System;
using System.Collections.Generic;
using System.IO;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading
{
    public sealed class WlpReader : IPacketReader
    {
        private readonly BinaryReader _reader;

        public WlpReader(string file)
        {
            _reader = new BinaryReader(new FileStream(file, FileMode.Open));
            _reader.ReadBytes(3);
            _reader.ReadBytes(2);
            _reader.ReadByte();
            _reader.ReadInt16();
            _reader.ReadBytes(4);
            _reader.ReadBytes(20);
            _reader.ReadBytes(64);
        }

        public bool CanRead()
        {
            return _reader.BaseStream.Position != _reader.BaseStream.Length;
        }

        public Packet Read(int number, string fileName)
        {
            var direction = (_reader.ReadByte() != 0xFF ? Direction.ClientToServer :
                Direction.ServerToClient);
            var time = Utilities.GetDateTimeFromUnixTime(_reader.ReadInt32());
            _reader.ReadInt32();
            var length = _reader.ReadInt32();
            var opcode = (direction == Direction.ClientToServer ? _reader.ReadInt32() :
                _reader.ReadInt16());
            var data = _reader.ReadBytes(length - (direction == Direction.ClientToServer ? 4 : 2));

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


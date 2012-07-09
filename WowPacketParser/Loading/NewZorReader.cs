using System.IO;
using PacketParser.Enums;
using PacketParser.Misc;
using System;
using PacketParser.DataStructures;

namespace PacketParser.Loading
{
    public sealed class NewZorReader : IPacketReader
    {
        private readonly BinaryReader _reader;

        public NewZorReader(string file)
        {
            _reader = new BinaryReader(new FileStream(file, FileMode.Open));
        }

        public bool CanRead()
        {
            return _reader.BaseStream.Position != _reader.BaseStream.Length;
        }

        public Packet Read(int number, string fileName)
        {
            var opcode = _reader.ReadInt16();
            var length = _reader.ReadInt32();
            var direction = (_reader.ReadByte() == 0) ? Direction.ClientToServer : Direction.ServerToClient;
            var time = Utilities.GetDateTimeFromUnixTime(_reader.ReadInt32());
            _reader.ReadInt32();
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

        public ClientVersionBuild GetBuild()
        {
            return ClientVersion.GetVersion(PeekDateTime());
        }

        public DateTime PeekDateTime()
        {
            var oldPos = _reader.BaseStream.Position;
            var p = Read(0, "");
            _reader.BaseStream.Position = oldPos;
            return p.Time;
        }

        public uint GetProgress()
        {
            if (_reader.BaseStream.Length != 0)
                return (uint)(_reader.BaseStream.Position*100 / _reader.BaseStream.Length);
            return 100;
        }
    }
}


using System;
using System.Data.SQLite;
using PacketParser.Misc;
using PacketParser.Enums;
using PacketParser.DataStructures;
using System.IO;

namespace PacketParser.Loading
{
    public sealed class SQLitePacketReader : IPacketReader
    {
        readonly SQLiteConnection _connection;
        SQLiteDataReader _reader;
        ClientVersionBuild _build = ClientVersionBuild.Zero;
        uint _num;
        uint _count;

        public SQLitePacketReader(string fileName)
        {
            _connection = new SQLiteConnection("Data Source=" + fileName);
            _connection.Open();

            // sniff may not contain build
            DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(fileName);
            _build = ClientVersion.GetVersion(lastWriteTimeUtc);
            // tiawps
            // header table (`key` string primary key, value string)
            // packets table (id integer primary key autoincrement, timestamp datetime, direction integer, opcode integer, data blob)
            ReadHeader();

            using (SQLiteCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT count(*) FROM packets;";
                _count = Convert.ToUInt32(command.ExecuteScalar());
            }

            using (SQLiteCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT opcode, timestamp, direction, data FROM packets;";
                _reader = command.ExecuteReader();
            }
            _num = 0;
        }

        void ReadHeader()
        {
            SQLiteCommand command = _connection.CreateCommand();
            command.CommandText = "SELECT key, value FROM header;";
            _reader = command.ExecuteReader();

            while (_reader.Read())
            {
                var key = _reader.GetString(0);
                var value = _reader.GetValue(1);

                if (key.ToLower() == "clientbuild")
                {
                    int build;
                    if (int.TryParse(value.ToString(), out build))
                        SetBuild(build);

                    break;
                }
            }

            _reader.Close();
        }

        public void SetBuild(int build)
        {
            _build = (ClientVersionBuild)build;
        }

        public ClientVersionBuild GetBuild()
        {
            return _build;
        }

        public bool CanRead()
        {
            return _reader.Read();
        }

        public Packet Read(int number, string fileName)
        {
            _num = (uint)number;
            var opcode = _reader.GetInt32(0);
            var time = _reader.GetDateTime(1);
            var direction = (Direction)_reader.GetInt32(2);
            object blob = _reader.GetValue(3);

            if (DBNull.Value.Equals(blob))
                return null;

            var data = (byte[])blob;

            var packet = new Packet(data, opcode, time, direction, number, fileName);
            return packet;
        }

        public void Dispose()
        {
            if (_reader != null)
                _reader.Close();

            if (_connection != null)
                _connection.Close();
        }

        public uint GetProgress()
        {
            if (_count != 0)
                return (uint)(_num * 100 / _count);
            return 100;
        }
    }
}

using System;
using System.Data.SQLite;
using WowPacketParser.Misc;
using WowPacketParser.Enums;

namespace WowPacketParser.Loading
{
    public class SQLitePacketReader : IPacketReader
    {
        readonly SQLiteConnection _connection;
        readonly SQLiteDataReader _reader;

        public SQLitePacketReader(string fileName)
        {
            _connection = new SQLiteConnection("Data Source=" + fileName);
            SQLiteCommand command = _connection.CreateCommand();
            _connection.Open();

            // tiawps
            // header table (`key` string primary key, value string)
            // packets table (id integer primary key autoincrement, timestamp datetime, direction integer, opcode integer, data blob)

            //command.CommandText = "SELECT key, value FROM header;";
            //using (SQLiteDataReader tempReader = command.ExecuteReader())
            //{
            //    while (tempReader.Read())
            //    {
            //        var key = tempReader.GetString(0);
            //        var value = tempReader.GetValue(1);
            //    }
            //}

            command.CommandText = "SELECT opcode, timestamp, direction, data FROM packets;";
            _reader = command.ExecuteReader();
        }

        public bool CanRead()
        {
            return _reader.Read();
        }

        public Packet Read(int number)
        {
            var opcode = _reader.GetInt32(0);
            var time = _reader.GetDateTime(1);
            var direction = (Direction)_reader.GetInt32(2);
            object blob = _reader.GetValue(3);

            if (DBNull.Value.Equals(blob))
                return null;

            var data = (byte[])blob;

            return new Packet(data, opcode, time, direction, number);
        }

        public void Close()
        {
            if (_reader != null)
                _reader.Close();

            if (_connection != null)
                _connection.Close();
        }
    }
}

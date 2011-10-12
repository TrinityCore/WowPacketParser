using System;
using System.Data.SQLite;
using WowPacketParser.Misc;
using WowPacketParser.Enums;

namespace WowPacketParser.Loading
{
    public class SQLitePacketReader : IPacketReader
    {
        SQLiteConnection connection = null;
        SQLiteDataReader reader = null;

        public SQLitePacketReader(string fileName)
        {
            connection = new SQLiteConnection("Data Source=" + fileName);
            SQLiteCommand command = connection.CreateCommand();
            connection.Open();

            command.CommandText = "SELECT opcode, timestamp, direction, data FROM packets;";
            reader = command.ExecuteReader();
        }

        public bool CanRead()
        {
            return reader.Read();
        }

        public Packet Read(int number)
        {
            var opcode = (Opcode)reader.GetInt32(0);
            var time = reader.GetDateTime(1);
            var direction = (Direction)reader.GetInt32(2);
            object blob = reader.GetValue(3);

            if (DBNull.Value.Equals(blob))
                return null;

            var data = (byte[])blob;

            return new Packet(data, opcode, time, direction, number);
        }

        public void Close()
        {
            if (reader != null)
                reader.Close();

            if (connection != null)
                connection.Close();
        }
    }
}

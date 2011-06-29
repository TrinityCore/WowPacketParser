using System;
using System.Collections.Generic;
using System.Data.SQLite;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading.Loaders
{
    [Loader("tiawps")]
    public sealed class TiawpsLoader : Loader
    {
        public TiawpsLoader(string file)
            : base(file)
        {
        }

        public override IEnumerable<Packet> ParseFile()
        {
            var packets = new List<Packet>();
            var conn = new SQLiteConnection("Data Source=" + FileToParse);

            conn.Open();

            var cmd = new SQLiteCommand("SELECT opcode, direction, timestamp, data FROM packets", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var opcode = (Opcode)reader.GetInt32(0);
                var direction = (Direction)reader.GetInt32(1);
                var timestamp = reader.GetDateTime(2);
                var data = (byte[])reader.GetValue(3);

                var packet = new Packet(data, opcode, timestamp, direction);
                packets.Add(packet);
            }

            return packets;
        }
    }
}

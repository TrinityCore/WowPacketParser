using System;
using System.Data.SQLite;
using System.IO;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading
{
    public sealed class SqLitePacketReader : IPacketReader, IDisposable
    {
        #region Fields

        private readonly int _count;

        private int _num;

        private readonly SQLiteConnection _connection;
        private SQLiteDataReader _reader;

        #endregion

        #region Constructors and Destructors

        public SqLitePacketReader(SniffType type, string fileName, Encoding encoding)
        {
            _connection = new SQLiteConnection(new SQLiteConnectionStringBuilder() { DataSource = fileName }.ConnectionString);
            _connection.Open();

            TryReadHeader(fileName);

            _num = 0;

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT count(*) FROM packets;";
                _count = Convert.ToInt32(command.ExecuteScalar());
            }

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT id, opcode, timestamp, direction, data FROM packets;";
                _reader = command.ExecuteReader();
            }
        }

        #endregion

        #region Public Methods and Operators

        public bool CanRead()
        {
            return _reader.Read();
        }

        public long GetCurrentSize()
        {
            return _num;
        }

        public long GetTotalSize()
        {
            return _count;
        }

        public void Dispose()
        {
            if (_reader != null)
            {
                _reader.Close();
                _reader.Dispose();
            }
            _reader = null;
        }

        private void TryReadHeader(string fileName)
        {
            // sniff may not contain build
            var lastWriteTimeUtc = File.GetLastWriteTimeUtc(fileName);

            // non tiawps
            // sqlite_sequnce (name, seq) while seq is the amount of files
            // packets table (id integer primary key autoincrement, sess_id integer/*not used*, timestamp datetime, direction integer, opcode integer, data blob)

            // tiawps
            // header table (`key` string primary key, value string)
            // packets table (id integer primary key autoincrement, timestamp datetime, direction integer, opcode integer, data blob)

            try
            {
                ReadHeader(); // might be non tiawps
            }
            catch (SQLiteException)
            {
                var build = ClientVersion.GetVersion(lastWriteTimeUtc);

                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = "SELECT timestamp FROM packets limit 1";
                    var dateTime = Convert.ToDateTime(command.ExecuteScalar());
                    build = ClientVersion.GetVersion(dateTime);
                }

                ClientVersion.SetVersion((ClientVersionBuild)build);
                ClientLocale.SetLocale("enUS");
            }
        }

        public Packet Read(int number, string fileName)
        {
            _num = number;
            // _reader.GetInt32(0); // Packet ID within file
            var opcode = _reader.GetInt32(1);
            var time = _reader.GetDateTime(2);
            var direction = (Direction)_reader.GetInt32(3);
            var blob = _reader.GetValue(4);

            if (DBNull.Value.Equals(blob))
                return null;

            var data = (byte[])blob;

            var packet = new Packet(data, opcode, time, direction, number, Path.GetFileName(fileName));
            return packet;
        }

        #endregion

        #region Methods

        private void ReadHeader()
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT key, value FROM header;";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var key = reader.GetString(0);
                        var value = reader.GetValue(1);

                        if (string.Compare(key, "clientbuild", StringComparison.InvariantCultureIgnoreCase) == 0)
                        {
                            int build;
                            if (int.TryParse(value.ToString(), out build))
                            {
                                ClientVersion.SetVersion((ClientVersionBuild)build);
                                ClientLocale.SetLocale("enUS");
                            }

                            break;
                        }
                    }

                    reader.Close();
                }
            }
        }

        #endregion
    }
}
